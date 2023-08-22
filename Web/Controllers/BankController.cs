using BusinessLayer.Classes;
using BusinessLayer.DTO;
using BusinessLayer.Helpers;
using Resource;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Web.Filters;
using Web.Helpers;

namespace Web.Controllers
{
    public class BankController : BaseController
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateRequest]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> GetReferenceCode()
        {
            ResponseReferenceCode response = new ResponseReferenceCode();
            BankAccountDto bankAccountDto = new BankAccountDto();
            await Task.Factory.StartNew(() =>
            {
                response = bankAccountDto.GetReferenceCode(
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
        public async Task<JsonResult> GetBankAccounts()
        {
            ResponseBankAccounts response = new ResponseBankAccounts();
            BankAccountDto bankAccountDto = new BankAccountDto();
            await Task.Factory.StartNew(() =>
            {
                response = bankAccountDto.GetBankAccounts(
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
        public async Task<JsonResult> GetBankAccountsValidated()
        {
            ResponseBankAccounts response = new ResponseBankAccounts();
            BankAccountDto bankAccountDto = new BankAccountDto();
            await Task.Factory.StartNew(() =>
            {
                response = bankAccountDto.GetBankAccountsValidated(
                    Convert.ToInt64(Session["id"])
                    );
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tacId"></param>
        /// <param name="holder"></param>
        /// <param name="accountNbr"></param>
        /// <param name="bankName"></param>
        /// <param name="cbu"></param>
        /// <returns></returns>
        [HttpPost]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> SaveAddAccount()
        {
            String data = Request.Form["data"];
            HttpPostedFileBase[] files = new HttpPostedFileBase[Request.Files.Count];
            for (int i = 0; i < Request.Files.Count; i++)
            {
                files[i] = Request.Files[i];
            }
            ResponseMessage response = new ResponseMessage();

            await Task.Factory.StartNew(() =>
            {
                ReturnUpload returnUpload = UploadBlobHelper.UploadFiles(files);

                if (returnUpload.code == 0 && returnUpload.files.Count == 1)
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    RequestBankAccount dataParameters = js.Deserialize<RequestBankAccount>(data);

                    BankAccountDto bankAccountDto = new BankAccountDto();

                    CommonDto commonDto = new CommonDto();
                    response = bankAccountDto.SaveAddAccount(
                        Convert.ToInt64(Session["id"]),
                        Convert.ToInt16(dataParameters.tacId),
                        dataParameters.holder,
                        dataParameters.accountNbr,
                        dataParameters.bankName,
                        dataParameters.cbu,
                        commonDto.InsertBlob(
                            returnUpload.files[0].newGuid,
                            returnUpload.files[0].fileName,
                            returnUpload.files[0].fileExtension,
                            returnUpload.files[0].fileWidth,
                            returnUpload.files[0].fileHeight,
                            returnUpload.files[0].container
                        )
                    );
                }
                else
                {
                    response.code = 306;
                    response.message = Resource.Messages.Error.errorUploadDocuments;
                }
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="banId"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateRequest]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> RemoveAccount(
            string banId
        )
        {
            ResponseMessage response = new ResponseMessage();
            BankAccountDto bankAccountDto = new BankAccountDto();
            await Task.Factory.StartNew(() =>
            {
                response = bankAccountDto.RemoveAccount(
                    Convert.ToInt64(Session["id"]),
                    Convert.ToInt64(banId)
                    );
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="topId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateRequest]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> GetBankMovements
            (
            String topId,
            String startDate,
            String endDate
            )
        {
            ResponseBankMovements response = new ResponseBankMovements();
            BankAccountDto bankAccountDto = new BankAccountDto();
            await Task.Factory.StartNew(() =>
            {
                response = bankAccountDto.GetBankMovements(
                    Convert.ToInt64(Session["id"]),
                    (topId!=null && !topId.Equals(""))? Convert.ToInt16(topId) : 0,
                    (startDate != null && !startDate.Equals("")) ? Convert.ToDouble(startDate) : 0,
                    (endDate != null && !endDate.Equals("")) ? Convert.ToDouble(endDate) : 0
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
        public async Task<JsonResult> GetBalanceAccount()
        {
            ResponseBalanceAccount response = new ResponseBalanceAccount();
            BankAccountDto bankAccountDto = new BankAccountDto();
            await Task.Factory.StartNew(() =>
            {
                response = bankAccountDto.GetBalanceAccount(
                    Convert.ToInt64(Session["id"])
                    );
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="returnFull"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateRequest]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> GetPatrimonialSituation(string returnFull)
        {
            ResponsePatrimonialSituation response = new ResponsePatrimonialSituation();
            BankAccountDto bankAccountDto = new BankAccountDto();
            await Task.Factory.StartNew(() =>
            {
                response = bankAccountDto.GetPatrimonialSituation(
                    Convert.ToInt64(Session["id"]),
                    returnFull.Equals("1")?true:false
                    );
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <param name="concept"></param>
        /// <param name="ammount"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateRequest]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> RetireFunds(
            string account,
            string concept,
            string ammount
        )
        {
            ResponseMessage response = new ResponseMessage();

            BankAccountDto bankAccountDto = new BankAccountDto();
            await Task.Factory.StartNew(() =>
            {
                response = bankAccountDto.RetireFunds(
                    Convert.ToInt64(Session["id"]),
                    Convert.ToInt16(account),
                    Convert.ToDouble(ammount),
                    concept
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
        public async Task<JsonResult> GetOwnBankAccount()
        {
            ResponseProjectBankAccount response = new ResponseProjectBankAccount();
            BankAccountDto bankAccountDto = new BankAccountDto();
            await Task.Factory.StartNew(() =>
            {
                response = CommonHelper.getBankAccount();
                response.referenceCode = bankAccountDto.GetReferenceCode(
                    Convert.ToInt64(Session["id"])
                    ).reference;
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="proId"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateRequest]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> GetProjectBankAccount(String proId)
        {
            ResponseProjectBankAccount response = new ResponseProjectBankAccount();
            BankAccountDto bankAccountDto = new BankAccountDto();
            await Task.Factory.StartNew(() =>
            {
                response = bankAccountDto.getProjectBankAccount(
                    Convert.ToInt64(proId)
                    );
                response.referenceCode = bankAccountDto.GetReferenceCode(
                    Convert.ToInt64(Session["id"])
                    ).reference;
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        // View Add Bank Account

        [SessionVerify]
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult AddAccount()
        {
            return View();
        }
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _AddAccount()
        {
            return PartialView("AddAccount");
        }

    }

}