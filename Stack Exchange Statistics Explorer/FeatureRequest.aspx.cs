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
    public partial class FeatureRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var requests = new List<Models.FeatureRequest>();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                requests = Models.FeatureRequest.LoadAllFromDatabase(connection).Where(x => x.Display).OrderByDescending(x => x.Priority).ToList();
            }

            Requests.DataSource = requests;
            Requests.DataBind();
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            var description = Description.Text;
            var proposedBy = ProposedBy.Text;
            var area = Area.Text;

            if (string.IsNullOrWhiteSpace(description))
            {
                Message.Text = "Error: You did not enter a description of the request/issue.";
                return;
            }

            if (area?.Length > 64)
            {
                Message.Text = "Error: The 'Area' must not be longer than 64 characters.";
                return;
            }

            if (proposedBy?.Length > 64)
            {
                Message.Text = "Error: The 'ProposedBy' must not be longer than 64 characters.";
                return;
            }

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                using (var command = new SqlCommand("INSERT INTO SE.FeatureRequests (ProposedBy, Area, Description) VALUES (@ProposedBy, @Area, @Description)", connection))
                {
                    command.Parameters.Add("@ProposedBy", SqlDbType.VarChar, 64).Value = (object)proposedBy ?? DBNull.Value;
                    command.Parameters.Add("@Area", SqlDbType.VarChar, 64).Value = (object)area ?? DBNull.Value;
                    command.Parameters.Add("@Description", SqlDbType.VarChar).Value = (object)description ?? DBNull.Value;

                    command.ExecuteNonQuery();
                }
            }

            Message.Text = "Your information was submitted successfully.";
        }
    }
}