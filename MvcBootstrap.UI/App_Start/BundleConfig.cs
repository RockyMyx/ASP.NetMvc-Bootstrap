using System.Web;
using System.Web.Optimization;
using System;

namespace MvcBootstrap.UI
{
    public class BundleConfig
    {
        public static void AddDefaultIgnorePatterns(IgnoreList ignoreList)
        {
            if (ignoreList == null)
                throw new ArgumentNullException("ignoreList");
            ignoreList.Ignore("*.intellisense.js");
            ignoreList.Ignore("*-vsdoc.js");
            ignoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
            //ignoreList.Ignore("*.min.js", OptimizationMode.WhenDisabled);
        }

        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();
            AddDefaultIgnorePatterns(bundles.IgnoreList);

            #region CSS

            //必须使用~/url的形式

            bundles.Add(new StyleBundle("~/Content/css")
                        .Include("~/Content/base/bootstrap/bootstrap.min.css",
                                 "~/Content/base/bootstrap/bootstrap-responsive.min.css",
                                 "~/Content/base/font-awesome/css/font-awesome.css",
                                 "~/Content/theme.css"));

            bundles.Add(new StyleBundle("~/Content/css-login")
                        .Include("~/Content/page/login.css"));

            bundles.Add(new StyleBundle("~/Content/css-paging")
                        .Include("~/Content/common/myx.pagination.css"));

            #endregion

            #region JS

            bundles.Add(new ScriptBundle("~/Content/js")
                        .Include("~/Scripts/jquery-1.8.3.min.js",
                                 "~/Scripts/bs/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/js-login")
                        .Include("~/Scripts/page/login.js"));

            bundles.Add(new ScriptBundle("~/Content/js-manage")
                        .Include("~/Scripts/jquery-1.8.3.min.js",
                                 "~/Scripts/tool/helper.js",
                                 "~/Scripts/bs/bootstrap-modal.js",
                                 "~/Scripts/control/myx.editValidate.js",
                                 "~/Scripts/control/myx.formValidate.js",
                                 "~/Scripts/control/myx.pagination.js",
                                 "~/Scripts/common.js"));

            #endregion

            #region 未使用

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));

            #endregion
        }
    }
}