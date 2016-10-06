using System;

namespace Stack_Exchange_Statistics_Explorer.Models
{
    [Serializable]
    public class SiteInfo
    {
        [ApiDescription("Reported number of answers per minute of site activity.")]
        [SiteCompare("Answers Per Minute", "0.00", 8)]
        public decimal AnswersPerMinute { get; set; }

        [ApiDescription("Version of the Stack Exchange API.")]
        public string ApiRevision { get; set; }

        [ApiDescription("Reported number of badges per minute of site activity.")]
        [SiteCompare("Badges Per Minute", "0.00", 9)]
        public decimal BadgesPerMinute { get; set; }

        [ApiDescription("Reported number of new users active on the site.")]
        public int NewActiveUsers { get; set; }

        [ApiDescription("Reported number of questions per minute of site activity.")]
        [SiteCompare("Questions Per Minute", "0.00", 10)]
        public decimal QuestionsPerMinute { get; set; }

        [ApiDescription("Total number of accepted answers/questions.")]
        [SiteCompare("Accepted", null, 6)]
        public int TotalAccepted { get; set; }

        [ApiDescription("Total number of answers.")]
        [SiteCompare("Answers", null, 4)]
        public int TotalAnswers { get; set; }
        
        [ApiDescription("Total number of badges.")]
        public int TotalBadges { get; set; }

        [ApiDescription("Total number of comments.")]
        public int TotalComments { get; set; }

        [ApiDescription("Total number of questions.")]
        [SiteCompare("Questions", null, 3)]
        public int TotalQuestions { get; set; }

        [ApiDescription("Total number of unanswered questions.")]
        [SiteCompare("Unanswered", null, 5)]
        public int TotalUnanswered { get; set; }

        [ApiDescription("Total number of users.")]
        [SiteCompare("Users", null, 7)]
        public int TotalUsers { get; set; }
        
        [ApiDescription("Total number of votes.")]
        public int TotalVotes { get; set; }
    }
}