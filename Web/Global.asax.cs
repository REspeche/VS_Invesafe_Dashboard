using System;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using log4net;
using System.Web.SessionState;

namespace Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly ILog Logger = LogManager.GetLogger("logError");

        protected void Application_Start()
        {
            ConfigureViewEngines();
            ConfigureAntiForgeryTokens();

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest()
        {
            if (Request.Headers["Origin"] != null && 
                Request.HttpMethod == "OPTIONS")
            {
                Response.End();
            }
        }

        protected void Session_Start()
        {
            Session["sessionId"] = new SessionIDManager().CreateSessionID(Context);
            Session["isLogin"] = false;
            // User's session
            Session["id"] = null;
            Session["fullName"] = string.Empty;
            Session["locale"] = String.Empty;
            Session["active"] = false;
        }

        /// <summary>
        /// Configures the view engines. By default, Asp.Net MVC includes the Web Forms (WebFormsViewEngine) and 
        /// Razor (RazorViewEngine) view engines. You can remove view engines you are not using here for better
        /// performance.
        /// </summary>
        private static void ConfigureViewEngines()
        {
            // Only use the RazorViewEngine for cshtml files.
            ViewEngines.Engines.Clear();
            IViewEngine razorEngine = new RazorViewEngine() { FileExtensions = new string[] { "cshtml" } };
            ViewEngines.Engines.Add(razorEngine);
        }

        /// <summary>
        /// Configures the anti-forgery tokens. See 
        /// http://www.asp.net/mvc/overview/security/xsrfcsrf-prevention-in-aspnet-mvc-and-web-pages
        /// </summary>
        private static void ConfigureAntiForgeryTokens()
        {
            // Rename the Anti-Forgery cookie from "__RequestVerificationToken" to "f". This adds a little security 
            // through obscurity and also saves sending a few characters over the wire. Sadly there is no way to change 
            // the form input name which is hard coded in the @Html.AntiForgeryToken helper and the 
            // ValidationAntiforgeryTokenAttribute to  __RequestVerificationToken.
            // <input name="__RequestVerificationToken" type="hidden" value="..." />
            AntiForgeryConfig.CookieName = "f";

            // If you have enabled SSL. Uncomment this line to ensure that the Anti-Forgery 
            // cookie requires SSL to be sent across the wire. 
            // AntiForgeryConfig.RequireSsl = true;
        }

        protected void Application_Error(object sender, EventArgs e)
        {
#if !DEBUG
            var error = Server.GetLastError();
            var code = (error is HttpException) ? (error as HttpException).GetHttpCode() : 500;

            if (code.Equals(500)) Logger.Fatal(error.Message, error);
            else Logger.Warn(error.Message, error);

            Response.Clear();
            Server.ClearError();

            var httpContext = HttpContext.Current;
            httpContext.Server.TransferRequest(string.Format("~/Error/Http{0}", code), true);
#endif
        }

    }
}
