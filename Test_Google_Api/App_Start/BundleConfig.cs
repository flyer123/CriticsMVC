using System.Web;
using System.Web.Optimization;

namespace Test_Google_Api
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js")
                        .Include("~/Scripts/jquery-migrate-1.2.1.js").Include("~/Scripts/device.min.js").Include("~/Scripts/camera.min.js")
                        .Include("~/Scripts/chosen.jquery.js").Include("~/Scripts/html5shiv.js").Include("~/Scripts/jquery.equalheights.js")
                        .Include("~/Scripts/jquery.mobile.customized.min.js").Include("~/jquery.rd-navbar.js").Include("~/Scripts/jquery.responsive.tabs.js").Include("~/Scripts/jquery.ui.totop.js")
                        .Include("~/Scripts/pointer-events.js").Include("~/Scripts/stacktable.js").Include("~/Scripts/superfish.js")
                        .Include("~/Scripts/tmstickup.js").Include("~/Scripts/wow.js")
                        .Include("~/Scripts/jquery.cookie.js").Include("~/Scripts/jquery-ui-tooltip.js"));
            bundles.Add(new ScriptBundle("~/bundles/owlcarousel").Include("~/Scripts/owl.carousel.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.min.js").Include("~/Scripts/jquery.validate.unobtrusive.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui.js"));
            bundles.Add(new ScriptBundle("~/bundles/alertjs").Include("~/Scripts/alert.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/js/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/js/bootstrap.js",
            //          "~/js/respond.js"));

            //bundles.Add(new StyleBundle("~/css").Include(
            //          "~/css/bootstrap.css",
            //          "~/css/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/dropzonescripts").Include(
                      "~/Scripts/dropzone/dropzone.min.js"));



            //bundles.Add(new ScriptBundle("~/bundles/dropzonescripts").Include(
            //         "~/Scripts/dropzone/dropzone.js"));

            bundles.Add(new StyleBundle("~/Content/dropzonescss").Include(
                     "~/Scripts/dropzone/css/basic.css",
                     "~/Scripts/dropzone/css/dropzone.css"));

            bundles.Add(new StyleBundle("~/css/jquery-ui-css").Include("~/css/jquery-ui.css"));

            //bundles.Add(new StyleBundle("~/Content/dropzonescss").Include(
            //         "~/css/basic.css",
            //         "~/css/dropzone.css"));

            //bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
            //            "~/Content/themes/base/jquery.ui.core.css",
            //            "~/Content/themes/base/jquery.ui.resizable.css",
            //            "~/Content/themes/base/jquery.ui.selectable.css",
            //            "~/Content/themes/base/jquery.ui.accordion.css",
            //            "~/Content/themes/base/jquery.ui.autocomplete.css",
            //            "~/Content/themes/base/jquery.ui.button.css",
            //            "~/Content/themes/base/jquery.ui.dialog.css",
            //            "~/Content/themes/base/jquery.ui.slider.css",
            //            "~/Content/themes/base/jquery.ui.tabs.css",
            //            "~/Content/themes/base/jquery.ui.datepicker.css",
            //            "~/Content/themes/base/jquery.ui.progressbar.css",
            //            "~/Content/themes/base/jquery.ui.theme.css"
            //            ));
            BundleTable.EnableOptimizations = true;
        }
    }
}
