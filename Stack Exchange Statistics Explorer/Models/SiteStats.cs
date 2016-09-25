using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Stack_Exchange_Statistics_Explorer.Utilities.Extensions.SqlDataReaderExtensions;
using static Stack_Exchange_Statistics_Explorer.Utilities.Extensions.DateTimeExtensions;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace Stack_Exchange_Statistics_Explorer.Models
{
    [Serializable]
    public class SiteStats : SiteInfo
    {
        [JsonIgnore]
        [ScriptIgnore]
        [XmlIgnore]
        [ApiDescription("The <code>" + nameof(Models.Site) + "</code> object for this object.")]
        [SiteCompare(null, Traverse = true)]
        public Site Site { get; set; }

        [ApiDescription("The <code>" + nameof(Models.Site.Id) + "</code> for the <code>" + nameof(Site) + "</code>.")]
        [SiteCompare("ID", null, 0)]
        public Guid SiteId { get; set; }

        [ApiDescription("The date and time the data was collected.")]
        public DateTime Gathered { get; set; }

        [ApiDescription("Whether or not this was a manual entry in the database.")]
        public bool Manual { get; set; }

        [ApiDescription("The number of users over 150 reputation.", Nullable = true)]
        [SiteCompare("Users > 150 Rep", null, 8)]
        public int? UsersAbove150Rep { get; set; }

        [ApiDescription("The number of users over 200 reputation.", Nullable = true)]
        [SiteCompare("Users > 200 Rep", null, 9)]
        public int? UsersAbove200Rep { get; set; }

        [ApiDescription("The number of questions posted less the number of unanswered questions.")]
        [SiteCompare("Answered", null, 5)]
        public int TotalAnswered => TotalQuestions - TotalUnanswered;

        public static void LoadFromReader(SqlDataReader reader, SiteStats siteStats)
        {
            siteStats.AnswersPerMinute = reader.GetItem<decimal>(nameof(AnswersPerMinute));
            siteStats.ApiRevision = reader.GetItem<string>(nameof(ApiRevision));
            siteStats.BadgesPerMinute = reader.GetItem<decimal>(nameof(BadgesPerMinute));
            siteStats.Gathered = reader.GetItem<DateTime>(nameof(Gathered));
            siteStats.Manual = reader.GetItem<bool>(nameof(Manual));
            siteStats.NewActiveUsers = reader.GetItem<int>(nameof(NewActiveUsers));
            siteStats.QuestionsPerMinute = reader.GetItem<decimal>(nameof(QuestionsPerMinute));
            siteStats.SiteId = reader.GetItem<Guid>(nameof(SiteId));
            siteStats.TotalAccepted = reader.GetItem<int>(nameof(TotalAccepted));
            siteStats.TotalAnswers = reader.GetItem<int>(nameof(TotalAnswers));
            siteStats.TotalBadges = reader.GetItem<int>(nameof(TotalBadges));
            siteStats.TotalComments = reader.GetItem<int>(nameof(TotalComments));
            siteStats.TotalQuestions = reader.GetItem<int>(nameof(TotalQuestions));
            siteStats.TotalUnanswered = reader.GetItem<int>(nameof(TotalUnanswered));
            siteStats.TotalUsers = reader.GetItem<int>(nameof(TotalUsers));
            siteStats.TotalVotes = reader.GetItem<int>(nameof(TotalVotes));
            siteStats.UsersAbove150Rep = reader.GetItem<int?>(nameof(UsersAbove150Rep));
            siteStats.UsersAbove200Rep = reader.GetItem<int?>(nameof(UsersAbove200Rep));
        }

        public static List<SiteStats> LoadFromDatabase(SqlConnection connection, Site site)
        {
            var sitesStats = new List<SiteStats>();

            using (var command = new SqlCommand("SELECT * FROM SE.SiteStats WHERE SiteId = @SiteId", connection))
            {
                command.Parameters.Add("@SiteId", SqlDbType.UniqueIdentifier).Value = site.Id;
                
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var siteStats = new SiteStats();
                        LoadFromReader(reader, siteStats);
                        siteStats.Site = site;
                        sitesStats.Add(siteStats);
                    }
                }
            }

            return sitesStats;
        }
    }
}
