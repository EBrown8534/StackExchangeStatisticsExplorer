using Stack_Exchange_Statistics_Explorer.API._1._0.Models;
using Stack_Exchange_Statistics_Explorer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Stack_Exchange_Statistics_Explorer.API._1._0.Requests
{
    public class SingleSiteFieldRequest : Request<SingleSiteFieldResponseItem>
    {
        public string Field { get; }
        public string Site { get; }
        public string FieldFormat { get; }
        public string DateFormat { get; }

        public static string DefaultDateFormat => "yyyy-MM-dd HH:mm:ss.fffffff";

        public SingleSiteFieldRequest(HttpContext context)
            : base(context)
        {
            Field = context.Request.QueryString[nameof(Field)];
            Site = context.Request.QueryString[nameof(Site)];
            DateFormat = context.Request.QueryString[nameof(DateFormat)] ?? DefaultDateFormat;
            FieldFormat = context.Request.QueryString[nameof(FieldFormat)];
        }

        protected override IEnumerable<SingleSiteFieldResponseItem> DoWork()
        {
            if (string.IsNullOrWhiteSpace(Site))
            {
                throw new ArgumentException($"The '{nameof(Site)}' parameter is required and cannot be blank.", nameof(Site));
            }

            if (!(typeof(SiteStatsCalculated).GetProperties().Where(p => p.Name == Field)?.Count() > 0))
            {
                throw new ArgumentException($"The '{nameof(Field)}' parameter is required and must be a valid property.", nameof(Field));
            }

            Guid siteId;
            if (!Guid.TryParse(Site, out siteId))
            {
                throw new ArgumentException($"The '{nameof(Site)}' parameter must be a valid Guid.", nameof(Site));
            }

            var site = new Site();
            var sitesStats = new List<SiteStatsCalculated>();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString))
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                var siteMerge = SiteMerge.LoadWithOriginalId(connection, siteId);
                if (siteMerge != null)
                {
                    siteId = siteMerge.NewSiteId;
                }

                site = Stack_Exchange_Statistics_Explorer.Models.Site.LoadFromDatabase(connection, siteId);

                if (site == null)
                {
                    throw new ArgumentException($"The '{nameof(Site)}' parameter did not match any site in the database.", nameof(Site));
                }

                sitesStats = SiteStatsCalculated.LoadFromDatabase(connection, site);
            }

            var result = new List<SingleSiteFieldResponseItem>();

            var property = typeof(SiteStatsCalculated).GetProperties().Where(p => p.Name == Field).First();

            SiteStatsCalculated.LinkToNext(sitesStats.Where(x => x.Manual == false).OrderByDescending(x => x.Gathered).ToList());
            sitesStats = sitesStats.OrderBy(x => x.Gathered).ToList();

            var firstValueAdded = false;
            foreach (var sitesStat in sitesStats)
            {
                var propValue = property.GetValue(sitesStat);

                if (firstValueAdded || propValue != null)
                {
                    firstValueAdded = true;

                    var gathered = sitesStat.Gathered.ToString(DateFormat);

                    if (result.Where(x => x.Gathered == gathered).Count() == 0)
                    {
                        result.Add(new SingleSiteFieldResponseItem
                        {
                            FieldValue = string.Format($"{{0:{FieldFormat}}}", propValue),
                            Gathered = gathered
                        });
                    }
                }
            }

            return result;
        }
    }
}
