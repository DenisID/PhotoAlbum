using System.Web;
using System.Web.Optimization;

namespace PhotoAlbum.Client
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js")
                        .IncludeDirectory("~/Scripts/jqueryui", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap/myAlias").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js")
                      .IncludeDirectory("~/Scripts/bootstrap", "*.js", true));
        
            bundles.Add(new StyleBundle("~/Content/css/myAlias").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css")
                      .IncludeDirectory("~/Content/bootstrap", "*.css", true)
                      .IncludeDirectory("~/Content/jqueryui", "*.css", true));

            bundles.Add(new ScriptBundle("~/bundles/rateyo/myAlias")
                            .IncludeDirectory("~/Scripts/rateyo", "*.js", true));

            bundles.Add(new StyleBundle("~/Content/rateyo/myAlias")
                            .IncludeDirectory("~/Content/rateyo", "*.css", true));

            bundles.Add(new ScriptBundle("~/Scripts/myModal")
                            .Include("~/Scripts/bootstrap/modal.js"));
        }
    }
}
