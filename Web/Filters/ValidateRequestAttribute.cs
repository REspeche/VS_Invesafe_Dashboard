using BusinessLayer.Classes;
using System;
using System.Net;
using System.Web.Mvc;

namespace Web.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ValidateRequestAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.Result != null) return;

            try
            {
                var context = filterContext.HttpContext;
                var sessionToken = context.Session["token"];
                var signature = context.Request.Headers["X-Authorization"];
                if (sessionToken == null || signature == null) filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                else
                {
                    if (!sessionToken.Equals(signature) || !Token.IsTokenValid(signature, Commons.getIPAddress(context.Request))) filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }
            }
            catch (Exception)
            {
                throw;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}