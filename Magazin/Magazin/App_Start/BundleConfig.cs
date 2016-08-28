using System.Web;
using System.Web.Optimization;

namespace Magazin
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/Common").Include(new[]
            {
                    "~/Content/vendor/jszip/jszip.js",
                "~/Content/vendor/kendo/js/kendo.all.js",
                "~/Content/vendor/kendo/js/kendo.aspnetmvc.js",
                "~/Content/vendor/kendo/js/kendo.culture.ru.custom.js",
            }));

            bundles.Add(new StyleBundle("~/CommonStyle").Include(new []
            {
                "~/Content/vendor/kendo/styles/kendo.common.min.css",
                "~/Content/vendor/kendo/styles/kendo.common-material.min.css",
                "~/Content/vendor/kendo/styles/kendo.material.min.css",
                "~/Content/vendor/kendo/styles/kendo.mobile.switch.css",
                "~/Content/less/kendo.overrides.css",
            }));
        }
    }
}
