using Stack_Exchange_Statistics_Explorer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Stack_Exchange_Statistics_Explorer.Sites
{
    public partial class Detail : Page
    {
        protected SiteStatsCalculated LatestStats { get; private set; }
        protected List<SiteStatsCalculated> AllLatestStats { get; private set; }
        protected Site CurrentSite { get; private set; }
        protected double QuestionsPerDay { get; private set; }
        protected double AnswersPerDay { get; private set; }
        protected SiteMerge Merge { get; private set; }

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

                var siteMerged = SiteMerge.LoadWithOriginalId(connection, CurrentSite.Id);
                if (siteMerged != null)
                {
                    Response.Redirect("/Sites/Detail?SiteId=" + siteMerged.NewSiteId.ToString("d") + "&FromMerge=" + siteMerged.OriginalSiteId.ToString("d"));
                }

                var fromMerge = Request.QueryString["FromMerge"];
                if (!string.IsNullOrWhiteSpace(fromMerge))
                {
                    var fromMergeGuid = Guid.NewGuid();
                    var validFromMergeGuid = Guid.TryParse(fromMerge, out fromMergeGuid);

                    if (validFromMergeGuid)
                    {
                        Merge = SiteMerge.LoadWithBothIds(connection, fromMergeGuid, CurrentSite.Id);
                        Merge.OriginalSite = Models.Site.LoadFromDatabase(connection, Merge.OriginalSiteId);
                    }
                }

                sitesStats = SiteStatsCalculated.LoadFromDatabase(connection, CurrentSite);
                AllLatestStats = Models.Site.LoadAllWithStatsFromDatabase(connection);

                if (CurrentSite.ApiSiteParameter.StartsWith("meta."))
                {
                    var mainSite = Models.Site.LoadFromDatabase(connection, CurrentSite.ApiSiteParameter.Replace("meta.", ""));

                    if (mainSite != null)
                    {
                        MainSite.Visible = true;
                        MainSite.NavigateUrl = "/Sites/Detail?SiteId=" + mainSite.Id.ToString("d");
                    }
                }
                else
                {
                    var metaSite = Models.Site.LoadFromDatabase(connection, "meta." + CurrentSite.ApiSiteParameter);

                    if (metaSite != null)
                    {
                        MetaSite.Visible = true;
                        MetaSite.NavigateUrl = "/Sites/Detail?SiteId=" + metaSite.Id.ToString("d");
                    }
                }
            }

            sitesStats = sitesStats.Where(x => x.Manual == false).OrderByDescending(x => x.Gathered).ToList();

            SiteStatsCalculated.LinkToNext(sitesStats);

            Title = CurrentSite.Name;
            LatestStats = sitesStats[0];

            var lastMonthStats = sitesStats.Take(30);
            var lastMonthFirst = lastMonthStats.First();
            var lastMonthLast = lastMonthStats.Last();
            QuestionsPerDay = (lastMonthLast.TotalQuestions - lastMonthFirst.TotalQuestions) / (lastMonthLast.Gathered - lastMonthFirst.Gathered).TotalDays;
            AnswersPerDay = (lastMonthLast.TotalAnswers - lastMonthFirst.TotalAnswers) / (lastMonthLast.Gathered - lastMonthFirst.Gathered).TotalDays;

            DateTime startDateTime;
            DateTime endDateTime;

            var startDate = Request.QueryString["StartDate"];
            var endDate = Request.QueryString["EndDate"];

            if (!string.IsNullOrWhiteSpace(startDate)
                && !string.IsNullOrWhiteSpace(endDate)
                && DateTime.TryParse(startDate, out startDateTime)
                && DateTime.TryParse(endDate, out endDateTime))
            {
                ErrorPanel.Visible = false;
                SiteStats.DataSource = sitesStats.Where(x => x.Gathered.Date >= startDateTime && x.Gathered.Date <= endDateTime)
                                                 .OrderByDescending(x => x.Gathered)
                                                 .ToList();
            }
            else
            {
                SiteStats.DataSource = sitesStats.Take(7);
            }

            SiteStats.DataBind();
        }

        protected int GetRank<T>(Func<SiteStatsCalculated, T> pred) => AllLatestStats.Where(x => x.Site.SiteType == CurrentSite.SiteType).OrderByDescending(pred).TakeWhile(x => x.SiteId != CurrentSite.Id).Count() + 1;

        protected int GetPercentile<T>(Func<SiteStatsCalculated, T> pred) => (int)(((double)AllLatestStats.Where(x => x.Site.SiteType == CurrentSite.SiteType).Count() - GetRank(pred)) / AllLatestStats.Where(x => x.Site.SiteType == CurrentSite.SiteType).Count() * 100);

        protected void FilterTable_Click(object sender, EventArgs e)
        {
            var siteId = Request.QueryString["SiteId"];
            var apiSiteParameter = Request.QueryString["ApiSiteParameter"];
            var fromMerge = Request.QueryString["FromMerge"];
            var tableStartDate = StartDate.Text;
            var tableEndDate = EndDate.Text;

            var newQueryString = new StringBuilder("?");

            if (!string.IsNullOrWhiteSpace(siteId))
            {
                newQueryString.Append("SiteId=");
                newQueryString.Append(siteId);
            }

            if (!string.IsNullOrWhiteSpace(apiSiteParameter))
            {
                if (newQueryString.Length > 1)
                {
                    newQueryString.Append('&');
                }

                newQueryString.Append("ApiSiteParameter=");
                newQueryString.Append(apiSiteParameter);
            }

            if (!string.IsNullOrWhiteSpace(fromMerge))
            {
                if (newQueryString.Length > 1)
                {
                    newQueryString.Append('&');
                }

                newQueryString.Append("FromMerge=");
                newQueryString.Append(fromMerge);
            }

            DateTime startDateTime;
            DateTime endDateTime;

            if (!string.IsNullOrWhiteSpace(tableStartDate)
                && !string.IsNullOrWhiteSpace(tableEndDate)
                && DateTime.TryParse(tableStartDate, out startDateTime)
                && DateTime.TryParse(tableEndDate, out endDateTime))
            {
                ErrorPanel.Visible = false;

                if (newQueryString.Length > 1)
                {
                    newQueryString.Append('&');
                }

                newQueryString.Append("StartDate=");
                newQueryString.Append(startDateTime.ToString("yyyy-MM-dd"));
                newQueryString.Append('&');
                newQueryString.Append("EndDate=");
                newQueryString.Append(endDateTime.ToString("yyyy-MM-dd"));
                Response.Redirect("~/Sites/Detail" + newQueryString.ToString());
            }
            else
            {
                ErrorPanel.Visible = true;
            }
        }
    }
}