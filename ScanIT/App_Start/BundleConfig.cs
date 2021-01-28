using System.Web;
using System.Web.Optimization;

namespace ScanIT
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/nodes").Include(
            "~/Resources/JavaScript/aos.js",
                        "~/Resources/JavaScript/Loader.js",
        "~/Resources/JavaScript/nodes.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                       "~/Resources/CSS/aos.css",
                      "~/Resources/CSS/main.css",
                                           "~/Resources/CSS/DataTables.css",
                      "~/Content/site.css"));
        }
    }
}
