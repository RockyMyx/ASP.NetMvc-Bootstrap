using System.Web;
using System.Web.Optimization;
using System;

namespace MvcBootstrapManage
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
                        .Include("~/Content/my/bootstrap/bootstrap.min.css",
                                 "~/Content/my/bootstrap/bootstrap-responsive.min.css",
                                 "~/Content/my/theme.css",
                                 "~/Content/my/font-awesome/css/font-awesome.css"));

            bundles.Add(new StyleBundle("~/Content/css-login")
                        .Include("~/Content/my/login.css"));

            bundles.Add(new StyleBundle("~/Content/css-paging")
                        .Include("~/Content/my/myx.pagination.css"));

            #endregion

            #region JS

            bundles.Add(new ScriptBundle("~/Content/js")
                        .Include("~/Scripts/jquery-1.8.3.min.js",
                                 "~/Scripts/my/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/js-login")
                        .Include("~/Scripts/my/login.js"));

            bundles.Add(new ScriptBundle("~/Content/js-manage")
                        .Include("~/Scripts/jquery-1.8.3.min.js",
                                 "~/Scripts/my/myx.validate.js",
                                 "~/Scripts/my/myx.formValidate.js",
                                 "~/Scripts/my/bootstrap-modal.js",
                                 "~/Scripts/my/myx.pagination.js",
                                 "~/Scripts/my/myx.page.js"));

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