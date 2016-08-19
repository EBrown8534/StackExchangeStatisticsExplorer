using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Stack_Exchange_Statistics_Explorer
{
    public partial class _Default : Page
    {
        protected int SiteCount { get; private set; }
        protected int NonMetaSiteCount { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString))
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                using (var command = new SqlCommand("SELECT COUNT(*) FROM SE.Sites", connection))
                {
                    SiteCount = (int)command.ExecuteScalar();
                }

                using (var command = new SqlCommand("SELECT COUNT(*) FROM SE.Sites WHERE SiteType <> 'meta_site'", connection))
                {
                    NonMetaSiteCount = (int)command.ExecuteScalar();
                }
            }
        }
    }
}