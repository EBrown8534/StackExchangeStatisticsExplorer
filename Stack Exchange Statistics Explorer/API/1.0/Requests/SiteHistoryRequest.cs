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
    public class SiteHistoryRequest : Request<SiteHistoryResponseItem>
    {
        private string _site;

        public SiteHistoryRequest(HttpContext context)
            : base(context)
        {
            _site = context.Request.QueryString["Site"];
        }

        protected override IEnumerable<SiteHistoryResponseItem> DoWork()
        {
            if (string.IsNullOrWhiteSpace(_site))
            {
                throw new ArgumentException("The 'Site' parameter is required and cannot be blank.", "Site");
            }

            Guid siteId;
            if (!Guid.TryParse(_site, out siteId))
            {
                throw new ArgumentException("The 'Site' parameter must be a valid Guid.", "Site");
            }

            var site = new Site();
            var sitesStats = new List<SiteHistoryResponseItem>();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString))
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                site = Site.LoadFromDatabase(connection, siteId);
                sitesStats = SiteHistoryResponseItem.LoadFromDatabase(connection, site);
            }

            SiteHistoryResponseItem.LinkToNext(sitesStats.Where(x => x.Manual == false).OrderByDescending(x => x.Gathered).ToList());
            sitesStats = sitesStats.OrderBy(x => x.Gathered).ToList();

            var result = new List<SiteHistoryResponseItem>();
            
            foreach (var sitesStat in sitesStats)
            {
                var gathered = sitesStat.Gathered;

                if (result.Where(x => x.Gathered.Date == gathered.Date).Count() == 0)
                {
                    result.Add(sitesStat);
                }
            }

            return result;
        }
    }
}
