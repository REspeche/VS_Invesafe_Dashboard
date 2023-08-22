using BusinessLayer.Classes;
using BusinessLayer.DTO;
using BusinessLayer.Helpers;
using Resource;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web.Filters;
using static BusinessLayer.Enum.Tables;

namespace Web.Controllers
{
    public class ProjectController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> GetProjects()
        {
            ResponseProjects response = new ResponseProjects();
            ProjectDto projectDto = new ProjectDto();
            await Task.Factory.StartNew(() =>
            {
                response = projectDto.GetProjects(
                    Convert.ToInt64(Session["id"]),
                    0
                    );
                response.handlerPath = CommonHelper.getHandlerPath();
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> GetLastProjects()
        {
            ResponseProjects response = new ResponseProjects();
            ProjectDto projectDto = new ProjectDto();
            await Task.Factory.StartNew(() =>
            {
                response = projectDto.GetProjects(
                    Convert.ToInt64(Session["id"]),
                    2 //2 last projects
                    );
                response.handlerPath = CommonHelper.getHandlerPath();
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="proId"></param>
        /// <returns></returns>
        [HttpPost]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> GetProject(String proId)
        {
            ResponseProject response = new ResponseProject();
            if (proId!="")
            {
                ProjectDto projectDto = new ProjectDto();
                await Task.Factory.StartNew(() =>
                {
                    response = projectDto.GetProject(
                        Convert.ToInt64(Session["id"]),
                        Convert.ToInt64(proId)
                        );
                    response.handlerPath = CommonHelper.getHandlerPath();
                });
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateRequest]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> GetDocuments(String proId)
        {
            ResponseDocuments response = new ResponseDocuments();
            ProjectDto projectDto = new ProjectDto();
            await Task.Factory.StartNew(() =>
            {
                response = projectDto.GetDocuments(
                    Convert.ToInt64(Session["id"]),
                    Convert.ToInt64(proId)
                    );
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> GetDocumentsSite(String proId)
        {
            ResponseDocuments response = new ResponseDocuments();
            ProjectDto projectDto = new ProjectDto();
            await Task.Factory.StartNew(() =>
            {
                response = projectDto.GetDocumentsSite(
                    Convert.ToInt64(Session["id"]),
                    Convert.ToInt64(proId)
                    );
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="proId"></param>
        /// <param name="amount"></param>
        /// <param name="payment"></param>
        /// <param name="agree"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateRequest]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> SaveInvest(
            string proId,
            string amount,
            string payment,
            string agree,
            string code
        )
        {
            ResponseMessage response = new ResponseMessage();
            bool isValid = false;
            switch (Session["authenticationMethod"])
            {
                case 1:
                    var accountDto = new AccountDto();
                    isValid = accountDto.ValidateTwoFactorAuthenticator(
                        Convert.ToInt64(Session["id"]),
                        code);
                    break;
                case 2:
                    isValid = Code.IsCodeValid(Session["code"].ToString(), code);
                    break;
            }
            if (isValid)
            {
                //Clean code in session
                Session["code"] = null;

                ProjectDto projectDto = new ProjectDto();
                await Task.Factory.StartNew(() =>
                {
                    response = projectDto.SaveInvest(
                        Convert.ToInt64(Session["id"]),
                        Convert.ToInt64(proId),
                        Convert.ToInt64(amount),
                        (paymentMethod)System.Enum.ToObject(typeof(paymentMethod), Convert.ToInt16(payment)),
                        (yesNo)System.Enum.ToObject(typeof(yesNo), Convert.ToInt16(agree)),
                        code
                        );
                });
            }
            else
            {
                response.code = 301;
                response.message = Resource.Messages.Error.errorCodeInvalid;
            }


            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CompressFilter(Order = 1)]
        [CacheFilter(Duration = 5, Order = 2)]
        public async Task<JsonResult> GetListProjects()
        {
            ResponseItemCombo response = new ResponseItemCombo();
            ProjectDto projectDto = new ProjectDto();
            await Task.Factory.StartNew(() =>
            {
                response = projectDto.GetListProjects();
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        // View Project Detail

        [SessionVerify]
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult Detail()
        {
            return View();
        }
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _Detail()
        {
            return PartialView("Detail");
        }

        // View To Invest
        [SessionVerify]
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult Invest()
        {
            return View();
        }
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _Invest()
        {
            return PartialView("Invest");
        }

        // Entry Funds
        [SessionVerify]
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult EntryFunds()
        {
            return View();
        }
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _EntryFunds()
        {
            return PartialView("EntryFunds");
        }

    }

}