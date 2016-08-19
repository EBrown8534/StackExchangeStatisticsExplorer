using Stack_Exchange_Statistics_Explorer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Stack_Exchange_Statistics_Explorer.Sites
{
    public partial class Detail : System.Web.UI.Page
    {
        protected SiteStatsCalculated Stats { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            var siteId = Request.QueryString["SiteId"];
            var apiSiteParameter = Request.QueryString["ApiSiteParameter"];

            var siteIdGuid = Guid.NewGuid();
            var validGuid = Guid.TryParse(siteId, out siteIdGuid);
            if (!validGuid)
            {
                if (apiSiteParameter == null)
                {
                    Response.Redirect("/Sites/Default");
                }
            }

            var site = new Site();
            var sitesStats = new List<SiteStatsCalculated>();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString))
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                if (validGuid)
                {
                    site = Models.Site.LoadFromDatabase(connection, siteIdGuid);
                }
                else
                {
                    site = Models.Site.LoadFromDatabase(connection, apiSiteParameter);
                }
                sitesStats = SiteStatsCalculated.LoadFromDatabase(connection, site);
            }

            sitesStats = sitesStats.Where(x => x.Manual == false).OrderByDescending(x => x.Gathered).ToList();

            SiteStatsCalculated.LinkToNext(sitesStats);

            Title = site.Name;

            Stats = sitesStats[0];

            SiteStats.DataSource = sitesStats;
            SiteStats.DataBind();
        }
    }
}