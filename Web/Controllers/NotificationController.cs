using BusinessLayer.Classes;
using BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Web.Filters;

namespace Web.Controllers
{
    public class NotificationController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateRequest]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> GetNotifications()
        {
            ResponseNotifications response = new ResponseNotifications();
            SettingDto settingDto = new SettingDto();
            await Task.Factory.StartNew(() =>
            {
                response = settingDto.GetNotifications(
                    Convert.ToInt64(Session["id"])
                    );
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateRequest]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> SaveNotifications(string data)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            RequestNotifications dataParameters = new RequestNotifications();
            dataParameters.items = js.Deserialize<List<NotificationItem>>(data);

            ResponseMessage response = new ResponseMessage();
            SettingDto settingDto = new SettingDto();
            await Task.Factory.StartNew(() =>
            {
                response = settingDto.SaveNotifications(
                    Convert.ToInt64(Session["id"]),
                    dataParameters
                    );
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}