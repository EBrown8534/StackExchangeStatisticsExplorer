using Stack_Exchange_Statistics_Explorer.API._1._0.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stack_Exchange_Statistics_Explorer.API._1._0
{
    /// <summary>
    /// Summary description for SingleSiteField
    /// </summary>
    public class SingleSiteField : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var request = new SingleSiteFieldRequest(context);
            var response = request.ProcessRequest();
            
            context.Response.ContentType = "text/plain";
            context.Response.Write(response);
        }

        public bool IsReusable => false;
    }
}