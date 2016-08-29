using Stack_Exchange_Statistics_Explorer.API._1._0.Models;
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
    public class SitesRequest : Request<ApiSite>
    {
        public SitesRequest(HttpContext context)
            : base(context)
        {

        }

        protected override IEnumerable<ApiSite> DoWork()
        {
            var sites = new List<ApiSite>();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString))
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                sites = ApiSite.LoadAllFromDatabase(connection);
            }

            return sites;
        }
    }
}
