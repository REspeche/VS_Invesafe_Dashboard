using BusinessLayer.Classes;
using BusinessLayer.DTO;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web.Filters;
using Web.Helpers;

namespace Web.Controllers
{
    public class MailController : BaseController
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CompressFilter(Order = 1)]
        [CacheFilter(Duration = 5, Order = 2)]
        public async Task<JsonResult> Subscribe(
            String name,
            String email,
            String phone,
            String message
        )
        {
            ResponseMessage response = new ResponseMessage();
            MailDto mailDto = new MailDto();
            await Task.Factory.StartNew(() =>
            {
                //Send subscribe mail
                MailHelper.SendMail("Subscribe", new string[] {
                    name,
                    email,
                    phone,
                    message
                });
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost]
        [CompressFilter(Order = 1)]
        [CacheFilter(Duration = 5, Order = 2)]
        public async Task<JsonResult> IAmDeveloper(
           String name,
           String email,
           String phone,
           String message
        )
        {
            ResponseMessage response = new ResponseMessage();
            MailDto mailDto = new MailDto();
            await Task.Factory.StartNew(() =>
            {
                //Send subscribe mail
                MailHelper.SendMail("IAmDeveloper", new string[] {
                    name,
                    email,
                    phone,
                    message
                });
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        [HttpPost]
        [CompressFilter(Order = 1)]
        [CacheFilter(Duration = 5, Order = 2)]
        public async Task<JsonResult> ContactForm(
            String name,
            String email,
            String subject,
            String comment
        )
        {
            ResponseMessage response = new ResponseMessage();
            MailDto mailDto = new MailDto();
            await Task.Factory.StartNew(() =>
            {
                //Send subscribe mail
                MailHelper.SendMail("ContactForm", new string[] {
                    name,
                    email,
                    subject,
                    comment
                });
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}