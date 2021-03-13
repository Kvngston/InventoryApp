using System.Web;
using System.Web.Optimization;

namespace inventoryAppWebUi
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Content/Assets/js/jquery-3.2.1.min.js",
                "~/Content/Assets/js/popper.min.js",
                "~/Content/Assets/js/bootstrap.min.js",
                "~/Content/Assets/js/custom.js"));


            bundles.Add(new StyleBundle("~/Content/template/css").Include(
                      "~/Content/Assets/css/bootstrap.min.css",
                      "~/Content/Assets/css/themify-icons.css",
                      "~/Content/Assets/css/animate.css",
                      "~/Content/Assets/css/styles.css",
                      "~/Content/Assets/css/responsive.css",
                      "~/Content/Assets/css/morris.css",
                      "~/Content/Assets/css/jquery-jvectormap.css"));
        }
    }
}
