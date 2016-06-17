using System.Web.Optimization;

namespace OnlinerNotifier
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/angular")
                .Include("~/scripts/angular.js")
                .Include("~/scripts/angular-route.js")
                .Include("~/scripts/angular-cookies.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                .Include("~/scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/app")
                .Include("~/app/app.js")
                .Include("~/app/controllers/authController.js")
                .Include("~/app/controllers/accountController.js")
                .Include("~/app/controllers/homeController.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrap")
                .Include("~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/font-awesome")
                .Include("~/Content/font-awesome.css"));

            bundles.Add(new StyleBundle("~/Content/site")
                .Include("~/Content/site.css"));
        }
    }
}