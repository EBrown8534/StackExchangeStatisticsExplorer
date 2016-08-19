using Evbpc.Framework.Utilities;
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

namespace Stack_Exchange_Statistics_Explorer.API._1._0
{
    public partial class SingleSiteFieldSummary : System.Web.UI.Page
    {
        protected Guid CodeReviewId { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString))
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                CodeReviewId = Stack_Exchange_Statistics_Explorer.Models.Site.LoadFromDatabase(connection, "codereview").Id;
            }

            var properties = typeof(SiteStatsCalculated).GetProperties().OrderBy(x => x.Name);

            ValidProperties.DataSource = properties;
            ValidProperties.DataBind();
        }

        protected string FormatAsSimpleType(string type)
        {
            var esb = new ExtendedStringBuilder();

            var isNullabe = type.Contains("System.Nullable");

            foreach (char c in type.Reverse())
            {
                if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                {
                    esb.Append(c);
                }
                
                if (c == '.')
                {
                    break;
                }
            }

            var esb2 = new ExtendedStringBuilder(esb.Length);

            foreach (char c in esb.ToString().Reverse())
            {
                esb2.Append(c);
            }

            var result = esb2.ToString();

            switch (result)
            {
                case "Int":
                case "Short":
                case "Long":
                case "Byte":
                case "Double":
                case "Decimal":
                case "String":
                case "Float":
                case "Char":
                    result = result.ToLower();
                    break;
                case "Boolean":
                    result = "bool";
                    break;
            }

            if (isNullabe)
            {
                result += '?';
            }

            return result;
        }
    }
}