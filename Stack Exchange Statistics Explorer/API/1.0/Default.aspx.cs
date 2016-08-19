using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Evbpc.Framework.Utilities.Extensions.StringExtensions;

namespace Stack_Exchange_Statistics_Explorer.API._1._0
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var methodPages = Directory.GetFiles(Server.MapPath("~/API/1.0"), "*.aspx");

            var apiFiles = new List<ApiFile>();

            foreach (var methodPage in methodPages)
            {
                var lastSlash = methodPage.LastIndexOf('\\') + 1;
                var file = methodPage.Substring(lastSlash);

                if (file == "Default.aspx")
                {
                    continue;
                }

                apiFiles.Add(new ApiFile
                {
                    File = file,
                    Name = methodPage.Substring(lastSlash, methodPage.LastIndexOf('.') - lastSlash).InsertOnCharacter(CharacterType.UpperLetter, " ")
                });
            }

            VersionMethods.DataSource = apiFiles;
            VersionMethods.DataBind();
        }

        protected class ApiFile
        {
            public string Name { get; set; }
            public string File { get; set; }
        }
    }
}