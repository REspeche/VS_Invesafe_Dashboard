using BusinessLayer.Helpers;
using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Web.Filters;

namespace Web.Controllers
{
    [LogFilter(Order = 2)]
    [Localisation]
    public class BaseController : Controller
    {
        /// <summary>
        /// Changes the culture.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void ChangeCulture(string id)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(id);

            HttpCookie _cookie = new HttpCookie(CommonHelper.getParamCache().currentUICulture, id);
            _cookie.Expires = DateTime.Now.AddYears(1);
            HttpContext.Response.SetCookie(_cookie);
        }

        /// <summary>
        /// Removes the cookie culture.
        /// </summary>
        public void RemoveCookieCulture()
        {
            var expiredCookie = new HttpCookie(CommonHelper.getParamCache().currentUICulture) { Expires = DateTime.Now.AddDays(-1) };
            HttpContext.Response.Cookies.Add(expiredCookie);
        }
    }
}