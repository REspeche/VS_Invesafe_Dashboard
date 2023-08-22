using log4net;
using System;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Web.Filters
{
    public class LogFilterAttribute : ActionFilterAttribute
    {
        private static readonly ILog LoggerAct = LogManager.GetLogger("logAction");
        private const string StopwatchKey = "StopwatchFilter.Value";
        private const string ParamsKey = "Params.Value";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (filterContext.ActionParameters.Count > 0)
                {
                    filterContext.RouteData.Values.Add(ParamsKey, new JavaScriptSerializer().Serialize(filterContext.ActionParameters));
                }
                filterContext.RouteData.Values.Add(StopwatchKey, Stopwatch.StartNew());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                throw;
            }

            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //Log Response
            log4net.LogicalThreadContext.Properties["controller"] = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            log4net.LogicalThreadContext.Properties["action"] = filterContext.ActionDescriptor.ActionName;

            //Check it's JsonResult that we're dealing with
            JsonResult jsonRes = filterContext.Result as JsonResult;
            log4net.LogicalThreadContext.Properties["request"] = String.Empty;
            log4net.LogicalThreadContext.Properties["elapsed"] = String.Empty;
            if (jsonRes != null)
            {
                Stopwatch stopwatch = (Stopwatch)filterContext.RouteData.Values[StopwatchKey];
                stopwatch.Stop();
                if (filterContext.RouteData.Values[ParamsKey] != null)
                {
                    log4net.LogicalThreadContext.Properties["request"] = (String)filterContext.RouteData.Values[ParamsKey];
                }
                log4net.LogicalThreadContext.Properties["elapsed"] = stopwatch.Elapsed.ToString();
                LoggerAct.Info(new JavaScriptSerializer().Serialize(jsonRes.Data));
            }
            else LoggerAct.Info(String.Empty);

            base.OnActionExecuted(filterContext);
        }
    }
}