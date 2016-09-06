﻿using System.Web;
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


            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                "~/Content/vendor/jszip/jszip.js",
                "~/Content/vendor/kendo/js/kendo.all.js",
                "~/Content/vendor/kendo/js/kendo.aspnetmvc.js",
                "~/Content/vendor/kendo/js/kendo.culture.ru.custom.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/fonts/fira/fira.css",
                "~/Content/fonts/roboto/roboto.css",
                "~/Content/fonts/glyphicons/glyphicons-regular.css",
                "~/Content/fonts/glyphicons/glyphicons-halflings-regular.css",
                "~/Content/fonts/glyphicons/glyphicons-filetypes-regular.css",
                "~/Content/fonts/glyphicons/glyphicons-social-regular.css",
                "~/Content/fonts/fontello/fontello.css",
                "~/Content/fonts/mdi/materialdesignicons.css",
                "~/Content/fonts/font-awesome/font-awesome.css", 
                "~/Content/bootstrap.css",
                "~/Content/Layout.css",
                "~/Content/vendor/kendo/styles/kendo.common.min.css",
                "~/Content/vendor/kendo/styles/kendo.common-material.min.css",
                "~/Content/vendor/kendo/styles/kendo.material.min.css",
                "~/Content/vendor/kendo/styles/kendo.mobile.switch.css",
                "~/Content/less/kendo.overrides.css",
                "~/Content/site.css"));
        }
    }
}
