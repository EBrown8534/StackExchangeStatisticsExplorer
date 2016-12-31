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
        [SiteCompare("Answered Rate", "0.00%", 13)]
        public double AnsweredRate => 1 - UnansweredRate;

        [ApiDescription("Change in <code>" + nameof(AnsweredRate) + "</code> from the previous object.", Nullable = true)]
        [SiteCompare("Answered Rate Change", "0.00%", 14)]
        public double? AnsweredRateChange => AnsweredRate - Previous?.AnsweredRate;

        [ApiDescription("Percentage of questions that have not been answered.")]
        [SiteCompare("Unanswered Rate", "0.00%", 15)]
        public double UnansweredRate => (double)TotalUnanswered / TotalQuestions;

        [ApiDescription("Change in <code>" + nameof(UnansweredRate) + "</code> from the previous object.", Nullable = true)]
        [SiteCompare("Unanswered Rate Change", "0.00%", 17)]
        public double? UnansweredRateChange => UnansweredRate - Previous?.UnansweredRate;

        [ApiDescription("Number of questions posted for each day since the earliest of: <code>" + nameof(Models.Site.ClosedBetaDateTime) + "</code>, <code>" + nameof(Models.Site.OpenBetaDateTime) + "</code> or <code>" + nameof(Models.Site.LaunchDateTime) + "</code>.")]
        [SiteCompare("Questions per Day", "0.00", 16)]
        public double QuestionsPerDay => (double)TotalQuestions / (Gathered - (Site.ClosedBetaDateTime ?? Site.OpenBetaDateTime ?? Site.LaunchDateTime))?.Days ?? 1;

        [ApiDescription("Number of answers posted for each day since the earliest of: <code>" + nameof(Models.Site.ClosedBetaDateTime) + "</code>, <code>" + nameof(Models.Site.OpenBetaDateTime) + "</code> or <code>" + nameof(Models.Site.LaunchDateTime) + "</code>.")]
        [SiteCompare("Answers per Day", "0.00", 18)]
        public double AnswersPerDay => (double)TotalAnswers / (Gathered - (Site.ClosedBetaDateTime ?? Site.OpenBetaDateTime ?? Site.LaunchDateTime))?.Days ?? 1;

        [ApiDescription("Change in <code>" + nameof(TotalAccepted) + "</code> from the previous object.", Nullable = true)]
        [SiteCompare("Total Accepted Change", "0", 19)]
        public int? TotalAcceptedChange => TotalAccepted - Previous?.TotalAccepted;

        [ApiDescription("Change in <code>" + nameof(TotalAnswered) + "</code> from the previous object.", Nullable = true)]
        [SiteCompare("Total Answered Change", "0", 20)]
        public int? TotalAnsweredChange => TotalAnswered - Previous?.TotalAnswered;

        [ApiDescription("Change in <code>" + nameof(TotalAnswers) + "</code> from the previous object.", Nullable = true)]
        [SiteCompare("Total Answers Change", "0", 21)]
        public int? TotalAnswersChange => TotalAnswers - Previous?.TotalAnswers;
        
        [ApiDescription("Change in <code>" + nameof(TotalBadges) + "</code> from the previous object.", Nullable = true)]
        [SiteCompare("Total Badges Change", "0", 22)]
        public int? TotalBadgesChange => TotalBadges - Previous?.TotalBadges;

        [ApiDescription("Change in <code>" + nameof(TotalComments) + "</code> from the previous object.", Nullable = true)]
        [SiteCompare("Total Comments Change", "0", 23)]
        public int? TotalCommentsChange => TotalComments - Previous?.TotalComments;

        [ApiDescription("Change in <code>" + nameof(TotalQuestions) + "</code> from the previous object.", Nullable = true)]
        [SiteCompare("Total Questions Change", "0", 24)]
        public int? TotalQuestionsChange => TotalQuestions - Previous?.TotalQuestions;

        [ApiDescription("Change in <code>" + nameof(TotalUnanswered) + "</code> from the previous object.", Nullable = true)]
        [SiteCompare("Total Unanswered Change", "0", 25)]
        public int? TotalUnansweredChange => TotalUnanswered - Previous?.TotalUnanswered;

        [ApiDescription("Change in <code>" + nameof(TotalUsers) + "</code> from the previous object.", Nullable = true)]
        [SiteCompare("Total Users Change", "0", 26)]
        public int? TotalUsersChange => TotalUsers - Previous?.TotalUsers;

        [ApiDescription("Change in <code>" + nameof(TotalVotes) + "</code> from the previous object.", Nullable = true)]
        [SiteCompare("Total Votes Change", "0", 27)]
        public int? TotalVotesChange => TotalVotes - Previous?.TotalVotes;

        [ApiDescription("Change in <code>" + nameof(UsersAbove150Rep) + "</code> from the previous object.", Nullable = true)]
        [SiteCompare("Users Above 150 Rep Change", "0", 28)]
        public int? UsersAbove150RepChange => UsersAbove150Rep - Previous?.UsersAbove150Rep;

        [ApiDescription("Change in <code>" + nameof(UsersAbove200Rep) + "</code> from the previous object.", Nullable = true)]
        [SiteCompare("Users Above 200 Rep Change", "0", 29)]
        public int? UsersAbove200RepChange => UsersAbove200Rep - Previous?.UsersAbove200Rep;

        [ApiDescription("The percentage of users that have more than 150 rep.", Nullable = true)]
        [SiteCompare("Percent Users Above 150 Rep", "0.000%", 30)]
        public double? PercentUsersAbove150Rep => UsersAbove150Rep != null ? (double?)UsersAbove150Rep / TotalUsers : null;

        [ApiDescription("The percentage of users that have more than 200 rep.", Nullable = true)]
        [SiteCompare("Percent Users Above 200 Rep", "0.000%", 31)]
        public double? PercentUsersAbove200Rep => UsersAbove200Rep != null ? (double?)UsersAbove200Rep / TotalUsers : null;

        [ApiDescription("The percentage of users that have more than 1000 rep.", Nullable = true)]
        [SiteCompare("Percent Users Above 1000 Rep", "0.000%", 32)]
        public double? PercentUsersAbove1000Rep => UsersAbove1000Rep != null ? (double?)UsersAbove1000Rep / TotalUsers : null;

        [ApiDescription("The percentage of users that have more than 10000 rep.", Nullable = true)]
        [SiteCompare("Percent Users Above 10000 Rep", "0.000%", 33)]
        public double? PercentUsersAbove10000Rep => UsersAbove1000Rep != null ? (double?)UsersAbove10000Rep / TotalUsers : null;

        [ApiDescription("The percentage of users that have more than 20000 rep.", Nullable = true)]
        [SiteCompare("Percent Users Above 20000 Rep", "0.000%", 34)]
        public double? PercentUsersAbove20000Rep => UsersAbove1000Rep != null ? (double?)UsersAbove20000Rep / TotalUsers : null;

        [ApiDescription("The ratio of answers to questions.")]
        [SiteCompare("Answer Ratio", "0.00", 14)]
        public double AnswerRatio => (double)TotalAnswers / TotalQuestions;

        [ApiDescription("The percentage of answered questions with accepted answers.")]
        [SiteCompare("Answered Accept Rate", "0.00%", 15)]
        public double AnsweredAcceptRate => (double)TotalAccepted / TotalAnswered;

        [ApiDescription("The percentage of questions with accepted answers.")]
        [SiteCompare("Questions Answer Accept Rate", "0.00%", 16)]
        public double QuestionAcceptRate => (double)TotalAccepted / TotalQuestions;

        [ApiDescription("The percentage of answers that are accepted.")]
        [SiteCompare("Answer Accept Rate", "0.00%", 17)]
        public double AnswerAcceptRate => (double)TotalAccepted / TotalAnswers;

        [ApiDescription("The percentage of answered questions with accepted answers.")]
        [SiteCompare("Question Answer Accept Rate", "0.00%", 18)]
        public double QuestionAnswerAcceptRate => (double)TotalAccepted / TotalAnswered;

        [ApiDescription("The average number of votes cast per day by users of the site.")]
        [SiteCompare("Votes Per Day", "0.00", 35)]
        public double VotesPerDay => (double)TotalVotes / (Gathered - (Site.ClosedBetaDateTime ?? Site.OpenBetaDateTime ?? Site.LaunchDateTime))?.Days ?? 1;

        [ApiDescription("The average number of votes cast per user above 150 rep.")]
        [SiteCompare("Votes Per User > 150 Rep", "0.00", 36)]
        public double? VotesPerUserAbove150Rep => ((double)TotalVotes / UsersAbove150Rep) ?? null;

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
