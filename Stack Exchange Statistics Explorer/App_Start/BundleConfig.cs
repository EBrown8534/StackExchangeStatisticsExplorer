using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;

namespace Stack_Exchange_Statistics_Explorer
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkID=303951
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/WebFormsJs").Include(
                            "~/Scripts/WebForms/WebForms.js",
                            "~/Scripts/WebForms/WebUIValidation.js",
                            "~/Scripts/WebForms/MenuStandards.js",
                            "~/Scripts/WebForms/Focus.js",
                            "~/Scripts/WebForms/GridView.js",
                            "~/Scripts/WebForms/DetailsView.js",
                            "~/Scripts/WebForms/TreeView.js",
                            "~/Scripts/WebForms/WebParts.js"));

            // Order is very important for these files to work, they have explicit dependencies
            bundles.Add(new ScriptBundle("~/bundles/MsAjaxJs").Include(
                            "~/Scripts/WebForms/MsAjax/MicrosoftAjax.js",
                            "~/Scripts/WebForms/MsAjax/MicrosoftAjaxApplicationServices.js",
                            "~/Scripts/WebForms/MsAjax/MicrosoftAjaxTimer.js",
                            "~/Scripts/WebForms/MsAjax/MicrosoftAjaxWebForms.js"));

            // Use the Development version of Modernizr to develop with and learn from. Then, when you’re
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                            "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/d3").Include(
                            "~/Scripts/d3/d3.js",
                            "~/Scripts/buildBasicChart.js"));

            /*bundles.Add(new ScriptBundle("~/bundles/list").IncludeDirectory(
                            "~/Scripts/list/", "*.js", true));*/
            bundles.Add(new ScriptBundle("~/bundles/list").Include(
                            "~/Scripts/list/dist/list.js"));

            bundles.Add(new ScriptBundle("~/bundles/foundation").Include(
                            "~/Scripts/foundation/foundation.js",
                            "~/Scripts/foundation/foundation.tab.js",
                            "~/Scripts/foundation/foundation.topbar.js",
                            "~/Scripts/foundation/foundation.tooltip.js",
                            "~/Scripts/foundation/foundation.equalizer.js"));

            ScriptManager.ScriptResourceMapping.AddDefinition(
                "respond",
                new ScriptResourceDefinition
                {
                    Path = "~/Scripts/respond.min.js",
                    DebugPath = "~/Scripts/respond.js",
                });
        }
    }
}