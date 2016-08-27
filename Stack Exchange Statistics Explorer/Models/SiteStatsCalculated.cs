using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace Stack_Exchange_Statistics_Explorer.Models
{
    [Serializable]
    public class SiteStatsCalculated : SiteStats
    {
        [JsonIgnore]
        [ScriptIgnore]
        [XmlIgnore]
        [ApiDescription("Previous <code>" + nameof(SiteStatsCalculated) + "</code> object when linked.")]
        public SiteStatsCalculated Previous { get; set; }

        [ApiDescription("Percentage of questions that have been answered.")]
        public double AnsweredRate => 1 - UnansweredRate;

        [ApiDescription("Change in <code>" + nameof(AnsweredRate) + "</code> from the previous object.", Nullable = true)]
        public double? AnsweredRateChange => AnsweredRate - Previous?.AnsweredRate;

        [ApiDescription("Percentage of questions that have not been answered.")]
        public double UnansweredRate => (double)TotalUnanswered / TotalQuestions;

        [ApiDescription("Change in <code>" + nameof(UnansweredRate) + "</code> from the previous object.", Nullable = true)]
        public double? UnansweredRateChange => UnansweredRate - Previous?.UnansweredRate;

        [ApiDescription("Number of questions posted for each day since the earliest of: <code>" + nameof(Models.Site.ClosedBetaDateTime) + "</code>, <code>" + nameof(Models.Site.OpenBetaDateTime) + "</code> or <code>" + nameof(Models.Site.LaunchDateTime) + "</code>.")]
        public double QuestionsPerDay => (double)TotalQuestions / (Gathered - (Site.ClosedBetaDateTime ?? Site.OpenBetaDateTime ?? Site.LaunchDateTime))?.Days ?? 1;

        [ApiDescription("Number of answers posted for each day since the earliest of: <code>" + nameof(Models.Site.ClosedBetaDateTime) + "</code>, <code>" + nameof(Models.Site.OpenBetaDateTime) + "</code> or <code>" + nameof(Models.Site.LaunchDateTime) + "</code>.")]
        public double AnswersPerDay => (double)TotalAnswers / (Gathered - (Site.ClosedBetaDateTime ?? Site.OpenBetaDateTime ?? Site.LaunchDateTime))?.Days ?? 1;

        [ApiDescription("Change in <code>" + nameof(TotalAccepted) + "</code> from the previous object.", Nullable = true)]
        public int? TotalAcceptedChange => TotalAccepted - Previous?.TotalAccepted;

        [ApiDescription("Change in <code>" + nameof(TotalAnswered) + "</code> from the previous object.", Nullable = true)]
        public int? TotalAnsweredChange => TotalAnswered - Previous?.TotalAnswered;

        [ApiDescription("Change in <code>" + nameof(TotalAnswers) + "</code> from the previous object.", Nullable = true)]
        public int? TotalAnswersChange => TotalAnswers - Previous?.TotalAnswers;

        [ApiDescription("Change in <code>" + nameof(TotalBadges) + "</code> from the previous object.", Nullable = true)]
        public int? TotalBadgesChange => TotalBadges - Previous?.TotalBadges;

        [ApiDescription("Change in <code>" + nameof(TotalComments) + "</code> from the previous object.", Nullable = true)]
        public int? TotalCommentsChange => TotalComments - Previous?.TotalComments;

        [ApiDescription("Change in <code>" + nameof(TotalQuestions) + "</code> from the previous object.", Nullable = true)]
        public int? TotalQuestionsChange => TotalQuestions - Previous?.TotalQuestions;

        [ApiDescription("Change in <code>" + nameof(TotalUnanswered) + "</code> from the previous object.", Nullable = true)]
        public int? TotalUnansweredChange => TotalUnanswered - Previous?.TotalUnanswered;

        [ApiDescription("Change in <code>" + nameof(TotalUsers) + "</code> from the previous object.", Nullable = true)]
        public int? TotalUsersChange => TotalUsers - Previous?.TotalUsers;

        [ApiDescription("Change in <code>" + nameof(TotalVotes) + "</code> from the previous object.", Nullable = true)]
        public int? TotalVotesChange => TotalVotes - Previous?.TotalVotes;

        [ApiDescription("Change in <code>" + nameof(UsersAbove150Rep) + "</code> from the previous object.", Nullable = true)]
        public int? UsersAbove150RepChange => UsersAbove150Rep - Previous?.UsersAbove150Rep;

        [ApiDescription("Change in <code>" + nameof(UsersAbove200Rep) + "</code> from the previous object.", Nullable = true)]
        public int? UsersAbove200RepChange => UsersAbove200Rep - Previous?.UsersAbove200Rep;

        [ApiDescription("The ratio of answers to questions.")]
        public double AnswerRatio => (double)TotalAnswers / TotalQuestions;

        public static new List<SiteStatsCalculated> LoadFromDatabase(SqlConnection connection, Site site)
        {
            var sitesStatsCalculated = new List<SiteStatsCalculated>();

            using (var command = new SqlCommand("SELECT * FROM SE.SiteStats WHERE SiteId = @SiteId", connection))
            {
                command.Parameters.Add("@SiteId", SqlDbType.UniqueIdentifier).Value = site.Id;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var siteStats = new SiteStatsCalculated();
                        LoadFromReader(reader, siteStats);
                        siteStats.Site = site;
                        sitesStatsCalculated.Add(siteStats);
                    }
                }
            }

            return sitesStatsCalculated;
        }

        public static void LinkToNext(List<SiteStatsCalculated> stats)
        {
            for (int i = 0; i < stats.Count - 1; i++)
            {
                stats[i].Previous = stats[i + 1];
            }
        }
    }
}
