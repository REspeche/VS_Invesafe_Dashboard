using System.Web.Mvc;

namespace Web.Filters
{
    public class SessionVerifyAttribute : ActionFilterAttribute
    {
        public const string LangParam = "lang";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["id"] == null)
            {
                // Try getting culture from URL first
                var culture = (string)filterContext.RouteData.Values[LangParam];

                var url = filterContext.HttpContext.Request.RawUrl;
                filterContext.Result = new RedirectResult("/" + culture + "/account/login" + ((url!="/")?"?urlback=" + System.Web.HttpUtility.UrlEncode(url) : ""));
            }

            // Pass on to normal controller processing
            base.OnActionExecuting(filterContext);
        }
    }
}