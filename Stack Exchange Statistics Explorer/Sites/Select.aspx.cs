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
    public partial class Select : System.Web.UI.Page
    {
        protected int SiteCount { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            var siteStats = new List<SiteStatsCalculated>();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString))
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                siteStats = Models.Site.LoadAllWithStatsFromDatabase(connection).OrderBy(x => x.Site.FirstUpdate).ToList();
            }

            SiteDisplay.DataSource = siteStats;
            SiteDisplay.DataBind();

            SiteCount = siteStats.Count;
        }

        protected void CompareSites_Click(object sender, EventArgs e)
        {
            var keys = Request.Form.AllKeys.Where(x => x.StartsWith("select-"));

            var values = new List<string>();

            foreach (var key in keys)
            {
                if (Request.Form[key] == "on")
                {
                    values.Add(key.Substring(7));
                }
            }

            Response.Redirect($"/Sites/Compare?Sites={string.Join(",", values)}");
        }
    }
}