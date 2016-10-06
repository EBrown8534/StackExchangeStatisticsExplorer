using Stack_Exchange_Statistics_Explorer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
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

            var allProperties = typeof(SiteStatsCalculated).GetProperties().Where(x => x.IsDefined(typeof(SiteCompareAttribute), true));

            foreach (var column in GetColumns(allProperties).OrderBy(x => x.Order))
            {
                var values = new List<string>();

                foreach (var siteStat in siteStats)
                {
                    var propertyParts = column.Property.Split('.');

                    object value = siteStat;
                    var subProperties = allProperties;
                    foreach (var propertyPart in propertyParts)
                    {
                        value = subProperties?.Where(x => x.Name == propertyPart).First().GetValue(value);
                        subProperties = value?.GetType().GetProperties();
                    }
                    values.Add(string.Format($"{{0:{column.Format}}}", value));
                }

                var item = new MainListViewItem();
                item.Header = column.Display;
                item.Items = values;
                items.Add(item);
            }

            MainListView.DataSource = items;
            MainListView.DataBind();
        }

        private List<ColumnFilter> GetColumns(IEnumerable<PropertyInfo> properties, string rootProperty = null)
        {
            var columns = new List<ColumnFilter>();

            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttributes<SiteCompareAttribute>().First();

                if (attribute.Traverse)
                {
                    columns.AddRange(GetColumns(property.PropertyType.GetProperties().Where(x => x.IsDefined(typeof(SiteCompareAttribute), true)), (rootProperty != null ? rootProperty + "." : "") + property.Name));
                }
                else
                {
                    columns.Add(new ColumnFilter
                    {
                        Display = attribute.Name,
                        Format = attribute.Format,
                        Property = (rootProperty != null ? rootProperty + "." : "") + property.Name,
                        Order = attribute.Order,
                        Summary = attribute.Summary,
                    });
                }
            }

            return columns;
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
            public string Display { get; set; }
            public string Property { get; set; }
            public string Format { get; set; }
            public int Order { get; set; }
            public string Summary { get; set; }
        }
    }
}