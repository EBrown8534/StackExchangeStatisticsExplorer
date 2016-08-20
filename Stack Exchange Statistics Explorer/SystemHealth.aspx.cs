using Stack_Exchange_Statistics_Explorer.Utilities.Extensions;
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
    public partial class SystemHealth : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var logResults = new List<ApiBatchLog>();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ApiDataConnection"].ConnectionString))
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                logResults = ApiBatchLog.LoadAllFromDatabase(connection);
            }

            var apiLogResults = new List<ApiBatchLog>();

            foreach (var logResult in logResults.OrderBy(x => x.StartDateTime))
            {
                if (apiLogResults.Where(x => x.StartDateTime.Date == logResult.StartDateTime.Date).Count() == 0)
                {
                    apiLogResults.Add(logResult);
                }
            }

            apiLogResults = apiLogResults.OrderByDescending(x => x.StartDateTime).ToList();

            ApiLogResults.DataSource = apiLogResults;
            ApiLogResults.DataBind();
        }

        protected class ApiBatchLog
        {
            public Guid Id { get; set; }
            public DateTime StartDateTime { get; set; }
            public DateTime EndDateTime { get; set; }
            public DateTime AddedDateTime { get; set; }
            public string RequestedBy { get; set; }
            public int RequestCount { get; set; }
            public int BackoffCount { get; set; }
            public int TotalBackoff { get; set; }
            public int HasMoreCount { get; set; }
            public int QuotaMax { get; set; }
            public int StartQuotaRemaining { get; set; }
            public int EndQuotaRemaining { get; set; }
            public int SiteCount { get; set; }

            public double RequestsPerSecond => RequestCount / (EndDateTime - StartDateTime).TotalSeconds;
            public TimeSpan TimeTaken => EndDateTime - StartDateTime;
            public double MillisecondsPerSite => (EndDateTime - StartDateTime).TotalMilliseconds / SiteCount;

            public static ApiBatchLog LoadFromReader(SqlDataReader reader) => new ApiBatchLog
            {
                Id = reader.GetItem<Guid>(nameof(Id)),
                StartDateTime = reader.GetItem<DateTime>(nameof(StartDateTime)),
                EndDateTime = reader.GetItem<DateTime>(nameof(EndDateTime)),
                AddedDateTime = reader.GetItem<DateTime>(nameof(AddedDateTime)),
                RequestedBy = reader.GetItem<string>(nameof(RequestedBy)),
                RequestCount = reader.GetItem<int>(nameof(RequestCount)),
                BackoffCount = reader.GetItem<int>(nameof(BackoffCount)),
                TotalBackoff = reader.GetItem<int>(nameof(TotalBackoff)),
                HasMoreCount = reader.GetItem<int>(nameof(HasMoreCount)),
                QuotaMax = reader.GetItem<int>(nameof(QuotaMax)),
                StartQuotaRemaining = reader.GetItem<int>(nameof(StartQuotaRemaining)),
                EndQuotaRemaining = reader.GetItem<int>(nameof(EndQuotaRemaining)),
                SiteCount = reader.GetItem<int>(nameof(SiteCount))
            };

            public static List<ApiBatchLog> LoadAllFromDatabase(SqlConnection connection)
            {
                var results = new List<ApiBatchLog>();

                using (var command = new SqlCommand(@"SELECT
    b.*
    ,[ItemCount] AS[SiteCount]
FROM
    SE.ApiBatchLog b
INNER JOIN
    SE.ApiLog
ON
    Id = BatchId
    AND
    StartDateTime = RequestDateTime
WHERE
    b.RequestedBy <> 'BADBATCH'
    AND
    b.RequestedBy <> 'INCOMPLETEBATCH'
ORDER BY
    EndDateTime DESC", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(LoadFromReader(reader));
                    }
                }

                return results;
            }
        }
    }
}