using Stack_Exchange_Statistics_Explorer.API._1._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Stack_Exchange_Statistics_Explorer.API._1._0.Requests
{
    public class SiteHistoryRequest : Request<SiteHistoryResponseItem>
    {
        private const string _defaultFields = "Date,TotalQuestions,TotalAnswers,QuestionAnswerRate,AnswerAcceptRate,AnswersPerQuestion";

        private string _fields;
        private string _site;

        public SiteHistoryRequest(HttpContext context)
            : base(context)
        {
            _fields = context.Request.QueryString["Fields"] ?? _defaultFields;
            _site = context.Request.QueryString["Site"];
        }

        protected override IEnumerable<SiteHistoryResponseItem> DoWork()
        {
            if (string.IsNullOrWhiteSpace(_site))
            {
                throw new ArgumentException("The 'Site' parameter is required and cannot be blank.", "Site");
            }

            return new List<SiteHistoryResponseItem>();
        }
    }
}
