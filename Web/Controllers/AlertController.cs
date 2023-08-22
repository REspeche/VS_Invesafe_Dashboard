using BusinessLayer.Classes;
using BusinessLayer.DTO;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web.Filters;

namespace Web.Controllers
{
    public class AlertController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateRequest]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> GetAlerts()
        {
            ResponseAlerts response = new ResponseAlerts();
            AlertDto alertDto = new AlertDto();
            await Task.Factory.StartNew(() =>
            {
                response = alertDto.GetAlerts(
                    Convert.ToInt64(Session["id"])
                    );
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateRequest]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> VerifyAlert(long[] listAle)
        {
            ResponseAlerts response = new ResponseAlerts();
            AlertDto alertDto = new AlertDto();
            await Task.Factory.StartNew(() =>
            {
                response = alertDto.GetAlertById(
                    Convert.ToInt64(Session["id"]),
                    listAle
                    );
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}