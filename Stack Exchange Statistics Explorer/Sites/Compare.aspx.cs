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
    public partial class Compare : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Request.QueryString["Sites"]))
            {
                Response.Redirect("~/Sites/Select/");
            }

            var items = new List<MainListViewItem>();
            var siteStats = new List<SiteStatsCalculated>();
            var sites = Request.QueryString["Sites"].Split(',');

            foreach (var site in sites)
            {
                var testGuid = Guid.NewGuid();

                if (!Guid.TryParse(site, out testGuid))
                {
                    Response.Redirect("~/Sites/Select/");
                }
            }

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString))
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                foreach (var site in sites)
                {
                    var siteObject = Models.Site.LoadFromDatabase(connection, Guid.Parse(site));

                    var thisSiteStats = SiteStatsCalculated.LoadFromDatabase(connection, siteObject);
                    thisSiteStats = thisSiteStats.Where(x => x.Manual == false).OrderByDescending(x => x.Gathered).ToList();
                    SiteStatsCalculated.LinkToNext(thisSiteStats);
                    siteStats.Add(thisSiteStats[0]);
                }
            }

            var allProperties = typeof(SiteStatsCalculated).GetProperties();
            var columns = new List<ColumnFilter>
            {
                new ColumnFilter { Name = "", Property = nameof(Models.Site) + "." + nameof(Models.Site.Name) },
                new ColumnFilter { Name = "ID", Property = nameof(Models.Site) + "." + nameof(Models.Site.Id) },
                new ColumnFilter { Name = "API Parameter", Property = nameof(Models.Site) + "." + nameof(Models.Site.ApiSiteParameter) },
                new ColumnFilter { Name = "Questions", Property = nameof(SiteStatsCalculated.TotalQuestions) },
                new ColumnFilter { Name = "Answers", Property = nameof(SiteStatsCalculated.TotalAnswers) },
                new ColumnFilter { Name = "Answered", Property = nameof(SiteStatsCalculated.TotalAnswered) },
                new ColumnFilter { Name = "Users", Property = nameof(SiteStatsCalculated.TotalUsers) },
                new ColumnFilter { Name = "Users > 150 Rep", Property = nameof(SiteStatsCalculated.UsersAbove150Rep) },
                new ColumnFilter { Name = "Users > 200 Rep", Property = nameof(SiteStatsCalculated.UsersAbove200Rep) },
                new ColumnFilter { Name = "Accepted Answers", Property = nameof(SiteStatsCalculated.TotalAccepted) },
                new ColumnFilter { Name = "State", Property = nameof(Models.Site) + "." + nameof(Models.Site.HumanizeState) },
                new ColumnFilter { Name = "Type", Property = nameof(Models.Site) + "." + nameof(Models.Site.HumanizeType) },
                new ColumnFilter { Name = "Answered Rate", Property = nameof(SiteStatsCalculated.AnsweredRate), Format = "0.00%" },
                new ColumnFilter { Name = "Unanswered Rate", Property = nameof(SiteStatsCalculated.UnansweredRate), Format = "0.00%" },
                new ColumnFilter { Name = "Answer Ratio", Property = nameof(SiteStatsCalculated.AnswerRatio), Format = "0.00" },
                new ColumnFilter { Name = "Questions Answer Accept Rate", Property = nameof(SiteStatsCalculated.QuestionAcceptRate), Format = "0.00%" },
                new ColumnFilter { Name = "Answered Accept Rate", Property = nameof(SiteStatsCalculated.AnsweredAcceptRate), Format = "0.00%" },
                new ColumnFilter { Name = "Answer Accept Rate", Property = nameof(SiteStatsCalculated.AnswerAcceptRate), Format = "0.00%" },
                new ColumnFilter { Name = "Questions Per Day", Property = nameof(SiteStatsCalculated.QuestionsPerDay), Format = "0.00" },
            };

            foreach (var column in columns)
            {
                var values = new List<string>();

                foreach (var siteStat in siteStats)
                {
                    var propertyParts = column.Property.Split('.');

                    object value = siteStat;
                    var subProperties = allProperties;
                    foreach (var propertyPart in propertyParts)
                    {
                        value = subProperties.Where(x => x.Name == propertyPart).First().GetValue(value);
                        subProperties = value.GetType().GetProperties();
                    }
                    values.Add(string.Format($"{{0:{column.Format}}}", value));
                }

                var item = new MainListViewItem();
                item.Header = column.Name;
                item.Items = values;
                items.Add(item);
            }

            MainListView.DataSource = items;
            MainListView.DataBind();
        }

        protected void MainListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            var repeater = (Repeater)e.Item.FindControl("Columns");
            repeater.DataSource = ((MainListViewItem)e.Item.DataItem).Items;
            repeater.DataBind();
        }

        protected class MainListViewItem
        {
            public string Header { get; set; }
            public IEnumerable<string> Items { get; set; }
        }

        private class ColumnFilter
        {
            public string Name { get; set; }
            public string Property { get; set; }
            public string Format { get; set; }
        }
    }
}