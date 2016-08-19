using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Stack_Exchange_Statistics_Explorer.API
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var versions = Directory.GetDirectories(Server.MapPath("~/API"));

            var versionObjects = new List<Version>();

            foreach (var version in versions)
            {
                versionObjects.Add(new Version { Name = version.Substring(version.LastIndexOf('\\') + 1) });
            }

            ApiVersions.DataSource = versionObjects;
            ApiVersions.DataBind();
        }

        protected class Version
        {
            public string Name { get; set; }
        }
    }
}