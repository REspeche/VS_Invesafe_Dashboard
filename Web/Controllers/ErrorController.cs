using System.Web.Mvc;

namespace Web.Controllers
{
    public class ErrorController : BaseController
    {

        [HttpGet]
        public ActionResult Http404(string source)
        {
            Response.StatusCode = 404;
            return View();
        }

        [HttpGet]
        public ActionResult Http500(string source)
        {
            Response.StatusCode = 500;
            return View();
        }

    }
}