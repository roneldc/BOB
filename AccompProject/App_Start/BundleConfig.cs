using System.Web;
using System.Web.Optimization;

namespace AccompProject
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"
            //          ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-{version}.js",
                       "~/Scripts/jquery-ui-{version}.js",
                       "~/Scripts/jquery.contextMenu.min.js",
                        "~/Scripts/jquery.ui.position.js",
                       "~/Scripts/jquery.dataTables.min.js",
                       "~/Scripts/dataTables.bootstrap.min.js",
                       "~/Scripts/jquery.validate*",
                       "~/Scripts/jssor.js",
                       "~/Scripts/jssor.slider.js",
                       "~/Scripts/jtv.js",
                        "~/Scripts/custom-form.js",
                         "~/Scripts/bootstrap-datepicker.js",
                         "~/Scripts/jquery.timepicker.min.js",
                           "~/Scripts/dataTables.fixedColumns.min.js"));

            

            //bundles.Add(new ScriptBundle("~/bundles/signalr").Include(
            //     "~/Scripts/ui/jquery.ui.core.js",
            //      "~/Scripts/ui/jquery.ui.widget.js",
            //       "~/Scripts/ui/jquery.ui.mouse.js",
            //        "~/Scripts/ui/jquery.ui.draggable.js",
            //         "~/Scripts/ui/jquery.ui.resizable.js",
            //    "~/Scripts/jquery.signalR-2.2.0.js",
            //            "~/signalr/hubs",
            //              "~/Scripts/Chat.js"));


            bundles.Add(new ScriptBundle("~/bundles/modalform").Include("~/Scripts/modalform.js"));

            bundles.Add(new ScriptBundle("~/bundles/imageslider").Include("~/Scripts/ImageSliders.js"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",                     
                      "~/Scripts/respond.js",
                       "~/Scripts/lightbox.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/ChatStyle.css",
                        "~/Content/themes/base/base/jquery.ui.all.css",
                        "~/Content/jquery-ui.css",
                       "~/Content/tabs.css",
                       "~/Content/lightbox.css",                                     
                        "~/Content/Site.css",
                          "~/Content/jquery.contextMenu.css",
                        "~/Content/bootstrap-datepicker.css",
                         "~/Content/jquery.timepicker.min.css",
                         "~/Content/jquery.dataTables.min.css",
                         "~/Content/custom_table.css",
                         "~/Content/bootstrap.css"));

        }
    }
}

//"~/Content/datepicker.css", 
