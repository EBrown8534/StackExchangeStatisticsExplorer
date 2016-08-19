using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack_Exchange_Statistics_Explorer.API._1._0.Models
{
    public class SiteHistoryItem : IBaseModel
    {
        public DateTime Gathered { get; set; }
        public double AnswersPerMinute { get; set; }
        public string ApiRevision { get; set; }
        public double BadgesPerMinute { get; set; }
        public int NewActiveUsers { get; set; }
        public double QuestionsPerMinute { get; set; }
        public int TotalAccepted { get; set; }
        public int TotalAnswered { get; set; }
        public int TotalAnswers { get; set; }
        public int TotalBadges { get; set; }
        public int TotalComments { get; set; }
        public int TotalQuestions { get; set; }
        public int TotalUnanswered { get; set; }
        public int TotalUsers { get; set; }
        public int TotalVotes { get; set; }
        public bool Manual { get; set; }
        public double CommentsPerEntity { get; set; }
        public double VotesPerEntity { get; set; }
        public int MinutesSinceLaunch { get; set; }
        public int DaysSinceLaunch { get; set; }
        public double AnswersPerMinuteActual { get; set; }
        public double QuestionsPerMinuteActual { get; set; }
        public double VotesPerMinuteActual { get; set; }
        public double AnswersPerHourActual { get; set; }
        public double QuestionsPerHourActual { get; set; }
        public double VotesPerHourActual { get; set; }
        public double AnswersPerDayActual { get; set; }
        public double QuestionsPerDayActual { get; set; }
        public double VotesPerDayActual { get; set; }
        public double QuestionAnswerAcceptRate { get; set; }
        public double QuestionAnswerRate { get; set; }
        public double AnswerAcceptRate { get; set; }
        public double AnswersPerQuestion { get; set; }
        public double VotesPerUser { get; set; }
    }
}
