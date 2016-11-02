using System.Runtime.InteropServices;
using System.Web;
using System.Web.Optimization;

namespace Magazin
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/dialogControls").Include(
                "~/Scripts/app/dialogControl.js",
                "~/Scripts/app/UiApi.js"
                ));


            bundles.Add(new ScriptBundle("~/kendo").Include(
                "~/Scripts/vendor/jszip.min.js",
                "~/Scripts/vendor/kendo.all.min.js",
                "~/Scripts/vendor/kendo.aspnetmvc.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/vendor/jquery.min.js"));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));



            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/site-css.css",
                "~/Content/fonts/fira/fira.css",
                "~/Content/fonts/roboto/roboto.css",
                "~/Content/fonts/glyphicons/glyphicons-regular.css",
                "~/Content/fonts/glyphicons/glyphicons-halflings-regular.css",
                "~/Content/fonts/glyphicons/glyphicons-filetypes-regular.css",
                "~/Content/fonts/glyphicons/glyphicons-social-regular.css",
                "~/Content/fonts/fontello/fontello.css",
                "~/Content/fonts/mdi/materialdesignicons.css",
                "~/Content/fonts/font-awesome/font-awesome.css",
                "~/Content/vendor/kendo/kendo.common.min.css"
                ));

            bundles.Add(new StyleBundle("~/Content/black-theme").Include(
                "~/Content/vendor/kendo/kendo.black.min.css"
                ));

            bundles.Add(new StyleBundle("~/Content/main-theme").Include(
                "~/Content/vendor/kendo/kendo.metro.min.css"
                ));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                "~/Content/bootstrap.css"
                ));

            bundles.Add(new StyleBundle("~/Content/admin-css").Include(
                "~/Content/admin-site.css",
                "~/Content/admin-layout.css",
                "~/Content/site-css.css"
                ));

        }
    }
}
