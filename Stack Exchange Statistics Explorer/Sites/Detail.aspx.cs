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
        protected SiteStatsCalculated LatestStats { get; private set; }
        protected List<SiteStatsCalculated> AllLatestStats { get; private set; }
        protected Site CurrentSite { get; set; }

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
            
            var sitesStats = new List<SiteStatsCalculated>();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString))
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                if (validGuid)
                {
                    CurrentSite = Models.Site.LoadFromDatabase(connection, siteIdGuid);
                }
                else
                {
                    CurrentSite = Models.Site.LoadFromDatabase(connection, apiSiteParameter);
                }

                sitesStats = SiteStatsCalculated.LoadFromDatabase(connection, CurrentSite);
                AllLatestStats = Models.Site.LoadAllWithStatsFromDatabase(connection);
            }

            sitesStats = sitesStats.Where(x => x.Manual == false).OrderByDescending(x => x.Gathered).ToList();

            SiteStatsCalculated.LinkToNext(sitesStats);

            Title = CurrentSite.Name;
            LatestStats = sitesStats[0];

            SiteStats.DataSource = sitesStats.Take(7);
            SiteStats.DataBind();
        }

        protected int GetRank<T>(Func<SiteStatsCalculated, T> pred) => AllLatestStats.Where(x => x.Site.SiteType == CurrentSite.SiteType).OrderByDescending(pred).TakeWhile(x => x.SiteId != CurrentSite.Id).Count() + 1;

        protected int GetPercentile<T>(Func<SiteStatsCalculated, T> pred) => (int)(((double)AllLatestStats.Where(x => x.Site.SiteType == CurrentSite.SiteType).Count() - GetRank(pred)) / AllLatestStats.Where(x => x.Site.SiteType == CurrentSite.SiteType).Count() * 100);
    }
}