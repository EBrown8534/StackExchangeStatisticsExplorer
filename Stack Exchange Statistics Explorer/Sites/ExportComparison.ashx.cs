using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stack_Exchange_Statistics_Explorer.Sites
{
    /// <summary>
    /// Summary description for ExportComparison
    /// </summary>
    public class ExportComparison : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/csv";
            context.Response.AddHeader("Content-Disposition", "attachment; filename=\"comparison.csv\"");
            
            context.Response.Write("Hello World");
        }

        public bool IsReusable => false;
    }
}