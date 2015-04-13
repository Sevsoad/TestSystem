using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace TestSystem
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jsScripts").Include(
                        "~/Scripts/jquery.min.js",
                        "~/Scripts/jquery.jqplot.min.js",
                        "~/Scripts/excanvas.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jquery.validate").Include(
            //            "~/Scripts/jquery.validate.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jquery.unobtrusive").Include(
            //            "~/Scripts/jquery.validate.unobtrusive.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Scripts/jquery.jqplot.min.css"));
        }
    }
}