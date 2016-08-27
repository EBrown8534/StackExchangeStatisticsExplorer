using System;

namespace Stack_Exchange_Statistics_Explorer.Models
{
    [Serializable]
    public class SiteInfo
    {
        [ApiDescription("Reported number of answers per minute of site activity.")]
        public decimal AnswersPerMinute { get; set; }

        [ApiDescription("Version of the Stack Exchange API.")]
        public string ApiRevision { get; set; }

        [ApiDescription("Reported number of badges per minute of site activity.")]
        public decimal BadgesPerMinute { get; set; }

        [ApiDescription("Reported number of new users active on the site.")]
        public int NewActiveUsers { get; set; }

        [ApiDescription("Reported number of questions per minute of site activity.")]
        public decimal QuestionsPerMinute { get; set; }

        [ApiDescription("Total number of accepted answers/questions.")]
        public int TotalAccepted { get; set; }

        [ApiDescription("Total number of answers.")]
        public int TotalAnswers { get; set; }
        
        [ApiDescription("Total number of badges.")]
        public int TotalBadges { get; set; }

        [ApiDescription("Total number of comments.")]
        public int TotalComments { get; set; }

        [ApiDescription("Total number of questions.")]
        public int TotalQuestions { get; set; }

        [ApiDescription("Total number of unanswered questions.")]
        public int TotalUnanswered { get; set; }

        [ApiDescription("Total number of users.")]
        public int TotalUsers { get; set; }
        
        [ApiDescription("Total number of votes.")]
        public int TotalVotes { get; set; }
    }
}