using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Helpers
{
    public static class CustomHelper
    {
        public static MvcHtmlString Attr(this HtmlHelper helper, string name, string value, bool condition)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value))
            {
                return MvcHtmlString.Empty;
            }

            return condition ?
                new MvcHtmlString(string.Format("{0}=\"{1}\"", name, HttpUtility.HtmlAttributeEncode(value))) :
                MvcHtmlString.Empty;
        }
    }
}