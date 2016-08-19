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
        private string _field;
        private string _site;
        private string _fieldFormat;
        private string _dateFormat;

        public static string DefaultDateFormat => "yyyy-MM-dd HH:mm:ss.fffffff";

        public SingleSiteFieldRequest(HttpContext context)
            : base(context)
        {
            _field = context.Request.QueryString["Field"];
            _site = context.Request.QueryString["Site"];
            _dateFormat = context.Request.QueryString["DateFormat"] ?? DefaultDateFormat;
            _fieldFormat = context.Request.QueryString["FieldFormat"];
        }

        protected override IEnumerable<SingleSiteFieldResponseItem> DoWork()
        {
            if (string.IsNullOrWhiteSpace(_site))
            {
                throw new ArgumentException("The 'Site' parameter is required and cannot be blank.", "Site");
            }

            if (!(typeof(SiteStatsCalculated).GetProperties().Where(p => p.Name == _field)?.Count() > 0))
            {
                throw new ArgumentException("The 'Field' parameter is required and must be a valid property.", "Field");
            }

            Guid siteId;
            if (!Guid.TryParse(_site, out siteId))
            {
                throw new ArgumentException("The 'Site' parameter must be a valid Guid.", "Site");
            }

            var site = new Site();
            var sitesStats = new List<SiteStatsCalculated>();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString))
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                site = Site.LoadFromDatabase(connection, siteId);

                if (site == null)
                {
                    throw new ArgumentException("The 'Site' parameter did not match any site in the database.", "Site");
                }

                sitesStats = SiteStatsCalculated.LoadFromDatabase(connection, site);
            }

            var result = new List<SingleSiteFieldResponseItem>();

            var property = typeof(SiteStatsCalculated).GetProperties().Where(p => p.Name == _field).First();

            SiteStatsCalculated.LinkToNext(sitesStats.Where(x => x.Manual == false).OrderByDescending(x => x.Gathered).ToList());
            sitesStats = sitesStats.OrderBy(x => x.Gathered).ToList();

            var firstValueAdded = false;
            foreach (var sitesStat in sitesStats)
            {
                var propValue = property.GetValue(sitesStat);

                if (firstValueAdded || propValue != null)
                {
                    firstValueAdded = true;

                    var gathered = sitesStat.Gathered.ToString(_dateFormat);

                    if (result.Where(x => x.Gathered == gathered).Count() == 0)
                    {
                        result.Add(new SingleSiteFieldResponseItem
                        {
                            FieldValue = string.Format($"{{0:{_fieldFormat}}}", propValue),
                            Gathered = gathered
                        });
                    }
                }
            }

            return result;
        }
    }
}
