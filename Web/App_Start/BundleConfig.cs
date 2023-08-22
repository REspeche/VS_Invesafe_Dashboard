using System.Linq;
using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;
            var version = System.Reflection.Assembly.GetAssembly(typeof(Controllers.DashboardController)).GetName().Version.ToString();
            var cdnUrl = "/{0}?v=" + version;

            /*  App */

            string[] cssApp = new string[] 
            {
                "~/content/modules/mdb/css/bootstrap.css",
                "~/content/modules/mdb/css/mdb.css",
                "~/content/assets/css/partition/alert.css",
                "~/content/assets/css/partition/form.css",
                "~/content/assets/css/partition/menu.css",
                "~/content/assets/css/partition/card.css",
                "~/content/assets/css/partition/title.css",
                "~/content/assets/css/partition/tooltip.css",
                "~/content/assets/css/partition/tab.css",
                "~/content/assets/css/partition/text.css",
                "~/content/assets/css/partition/modal.css",
                "~/content/assets/css/partition/timeline.css",
                "~/content/assets/css/partition/table.css",
                "~/content/assets/css/partition/scroll.css",
                "~/content/assets/css/style.css"
            };
            string[] jsApp = new string[]
            {
                "~/content/modules/jquery/jquery-3.3.1.js", //JQuery
                "~/content/modules/tether/tether.js", //Bootstrap tooltips
                "~/content/modules/toastr/toastr.js",
                "~/content/modules/mdb/js/bootstrap.js", //Bootstrap core JavaScript
                "~/content/modules/mdb/js/mdb.js", //MDB core JavaScript
                "~/content/modules/angular/angular.js", //Angular
                "~/content/modules/angular/angular-cookies.js",
                "~/content/modules/angular/angular-idle.js",
                "~/content/modules/angular-translate/angular-translate.min.js",
                "~/content/modules/angular-translate-loader-static-files/angular-translate-loader-static-files.min.js",
                "~/content/modules/angular-translate-storage-cookie/angular-translate-storage-cookie.js",
                "~/content/modules/angular-translate-storage-local/angular-translate-storage-local.js",
                "~/content/modules/date/date.js",
                "~/content/modules/file-upload/ng-file-upload.js",
                "~/content/modules/flip/jquery.flip.js",
                "~/content/assets/js/app.js",
                "~/content/assets/js/appConfig.js",
                "~/content/assets/js/appRun.js",
                "~/content/assets/js/commonfunctions.js"
            };

            /*  Dashboard */

            bundles.Add(new StyleBundle("~/css/layoutDashboard", string.Format(cdnUrl, "css/layoutDashboard"))
                    .Include(
                        ConcatArrays(cssApp, new string[]
                        {
                            "~/content/assets/css/shared/_layoutDashboard.css",
                            "~/content/assets/css/dashboard/opportunities.css",
                            "~/content/assets/css/dashboard/personaldata.css",
                            "~/content/assets/css/dashboard/typeaccount.css",
                            "~/content/assets/css/dashboard/changepassword.css",
                            "~/content/assets/css/dashboard/authenticationmethod.css",
                            "~/content/assets/css/dashboard/patrimonialsituation.css",
                            "~/content/assets/css/dashboard/myinvestments.css",
                            "~/content/assets/css/dashboard/bankaccounts.css",
                            "~/content/assets/css/dashboard/entryfunds.css",
                            "~/content/assets/css/dashboard/information.css",
                            "~/content/assets/css/dashboard/invesafereports.css",
                            "~/content/assets/css/dashboard/retirefunds.css",
                            "~/content/assets/css/dashboard/notifications.css",
                            "~/content/assets/css/account/login.css",
                            "~/content/directives/ngidle/ngidle.css",
                            "~/content/directives/ngdigitcode/ngdigitcode.css"
                        })));

            bundles.Add(new ScriptBundle("~/js/layoutDashboard", string.Format(cdnUrl, "js/layoutDashboard"))
                    .Include(
                        ConcatArrays(jsApp, new string[]
                        {
                            "~/content/directives/ngidle/ngidle.js",
                            "~/content/directives/ngdigitcode/ngdigitcode.js",
                            "~/content/directives/fallbackSrc/fallbackSrc.js",
                            "~/content/directives/fileinput/fileinput.js",
                            "~/content/directives/fixDecimal/fixDecimal.js",
                            "~/content/assets/js/shared/_layoutDashboard.js",
                            "~/content/assets/js/dashboard/home.js",
                            "~/content/assets/js/dashboard/opportunities.js",
                            "~/content/assets/js/dashboard/personaldata.js",
                            "~/content/assets/js/dashboard/aditionaldata.js",
                            "~/content/assets/js/dashboard/typeaccount.js",
                            "~/content/assets/js/dashboard/changepassword.js",
                            "~/content/assets/js/dashboard/authenticationmethod.js",
                            "~/content/assets/js/dashboard/patrimonialsituation.js",
                            "~/content/assets/js/dashboard/myinvestments.js",
                            "~/content/assets/js/dashboard/bankactivity.js",
                            "~/content/assets/js/dashboard/fiscalreports.js",
                            "~/content/assets/js/dashboard/legaldocuments.js",
                            "~/content/assets/js/dashboard/generalmembermeetings.js",
                            "~/content/assets/js/dashboard/support.js",
                            "~/content/assets/js/dashboard/bankaccounts.js",
                            "~/content/assets/js/dashboard/entryfunds.js",
                            "~/content/assets/js/dashboard/invesafereports.js",
                            "~/content/assets/js/dashboard/retirefunds.js",
                            "~/content/assets/js/dashboard/notifications.js",
                            "~/content/assets/js/dashboard/marketplace.js",
                            "~/content/assets/js/dashboard/activeaccount.js",
                            "~/content/assets/js/account/login.js"
                        })));

            /*  Error */

            bundles.Add(new StyleBundle("~/css/error", string.Format(cdnUrl, "css/error"))
                    .Include(
                        new string[]
                        {
                            "~/content/modules/mdb/css/bootstrap.css"
                        }));

            bundles.Add(new ScriptBundle("~/js/error", string.Format(cdnUrl, "js/error"))
                    .Include(
                        new string[]
                        {
                            "~/content/modules/jquery/jquery-3.3.1.js",
                            "~/content/modules/tether/tether.js", //Bootstrap tooltips
                            "~/content/modules/mdb/js/bootstrap.js",
                            "~/content/assets/js/error/modernizr.custom.js"
                        }));

#if !DEBUG
            BundleTable.EnableOptimizations = true;
#else
            BundleTable.EnableOptimizations = false;
#endif
        }

        private static T[] ConcatArrays<T>(params T[][] list)
        {
            var result = new T[list.Sum(a => a.Length)];
            int offset = 0;
            for (int x = 0; x < list.Length; x++)
            {
                list[x].CopyTo(result, offset);
                offset += list[x].Length;
            }
            return result;
        }
    }
}
