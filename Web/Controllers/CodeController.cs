using BusinessLayer.Classes;
using BusinessLayer.DTO;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web.Helpers;
using Web.Filters;

namespace Web.Controllers
{
    public class CodeController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="proId"></param>
        /// <returns></returns>
        public async Task<JsonResult> SendCodeByEmail()
        {
            ResponseMessage response = new ResponseMessage();

            await Task.Factory.StartNew(() =>
            {
                bool isValid = false;
                bool isRegenerate = false;
                if (Session["code"] != null && Session["code"].ToString() != "") {
                    isValid = Code.IsCodeValid(Session["code"].ToString(), null);
                    isRegenerate = true;
                }
                if (!isValid) {
                    // Create code to send
                    String code = Code.GenerateCode(DateTime.UtcNow.Ticks);
                    Session["code"] = code;
                    if (isRegenerate) {
                        response.code = 300;
                        response.message = Resource.Messages.Error.errorCodeExpired;
                    }

                    //Send code by mail
                    string[] parts = Session["code"].ToString().Split(new char[] { ':' });
                    string sessionCode = parts[0];
                    //Send code verification mail
                    MailHelper.SendMail("CodeVerification", new string[] {
                        Session["firstName"].ToString(),
                        Session["email"].ToString(),
                        sessionCode
                    });
                }
            });
            
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="proId"></param>
        /// <returns></returns>
        public async Task<JsonResult> VerifyCodeBySMS(
            String codePhone,
            String phone
            )
        {
            ResponseMessage response = new ResponseMessage();

            await Task.Factory.StartNew(() =>
            {
                bool isValid = false;
                bool isRegenerate = false;
                if (Session["code"] != null && Session["code"].ToString() != "")
                {
                    isValid = Code.IsCodeValid(Session["code"].ToString(), null);
                    isRegenerate = true;
                }
                if (!isValid)
                {
                    // Create code to send
                    String code = Code.GenerateCode(DateTime.UtcNow.Ticks);
                    Session["code"] = code;
                    if (isRegenerate)
                    {
                        response.code = 300;
                        response.message = Resource.Messages.Error.errorCodeExpired;
                    }

                    //Send code by mail
                    string[] parts = Session["code"].ToString().Split(new char[] { ':' });
                    string sessionCode = parts[0];

                    //Send code SMS
                    var accountDto = new AccountDto();
                    response = accountDto.SendCodeSMS(codePhone + phone, sessionCode);
                    if (response.code != 0) Session["code"] = null;
                    else
                    {
                        response = accountDto.UpdatePhone(Convert.ToInt64(Session["id"]), codePhone, phone);
                    }
                }
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="proId"></param>
        /// <returns></returns>
        public async Task<JsonResult> SendCodeBySMS()
        {
            ResponseMessage response = new ResponseMessage();

            await Task.Factory.StartNew(() =>
            {
                bool isValid = false;
                bool isRegenerate = false;
                if (Session["code"] != null && Session["code"].ToString() != "")
                {
                    isValid = Code.IsCodeValid(Session["code"].ToString(), null);
                    isRegenerate = true;
                }
                if (!isValid)
                {
                    // Create code to send
                    String code = Code.GenerateCode(DateTime.UtcNow.Ticks);
                    Session["code"] = code;
                    if (isRegenerate)
                    {
                        response.code = 300;
                        response.message = Resource.Messages.Error.errorCodeExpired;
                    }

                    //Send code by mail
                    string[] parts = Session["code"].ToString().Split(new char[] { ':' });
                    string sessionCode = parts[0];

                    //Send code SMS
                    var accountDto = new AccountDto();
                    response = accountDto.SendCodeSMS(Session["phone"].ToString(), sessionCode);
                    if (response.code != 0) Session["code"] = null;
                }
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<JsonResult> EnabledSMSAuthenticator(
            string code
        )
        {
            ResponseMessage response = new ResponseMessage();

            await Task.Factory.StartNew(() =>
            {
                // is valid code
                bool isValid = Code.IsCodeValid(Session["code"].ToString(), code);
                if (isValid)
                {
                    var accountDto = new AccountDto();
                    accountDto.EnabledSMSAuthenticator(Convert.ToInt64(Session["id"]));
                    Session["code"] = null;
                }
                else
                {
                    response.code = 317;
                    response.message = Resource.Messages.Error.errorCodeInvalidSMS;
                }
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> GetTwoFactorAuthenticator()
        {
            var accountDto = new AccountDto();
            ResponseTwoFactorAuthenticator response = new ResponseTwoFactorAuthenticator();
            try
            {
                await Task.Factory.StartNew(() =>
                {
                    response = accountDto.GetTwoFactorAuthenticator(Session["email"].ToString());
                });
            }
            catch (Exception ex)
            {
                response.code = -1;
                response.message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> ValidateCodeTwoFactor(String code)
        {
            var accountDto = new AccountDto();
            ResponseMessage response = new ResponseMessage();
            try
            {
                await Task.Factory.StartNew(() =>
                {
                    Boolean isCorrectPIN = accountDto.ValidateTwoFactorAuthenticator(
                        Convert.ToInt64(Session["id"]),
                        code);
                    response.code = (isCorrectPIN) ? 0 : 214;
                    response.message = (isCorrectPIN) ? Resource.Messages.Success.notifyOK : Resource.Messages.Warning.wrongCodeTwoFactor;
                });
            }
            catch (Exception ex)
            {
                response.code = -1;
                response.message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<JsonResult> ValidateCodeSMS(
            string code
        )
        {
            ResponseMessage response = new ResponseMessage();

            await Task.Factory.StartNew(() =>
            {
                // is valid code
                bool isValid = Code.IsCodeValid(Session["code"].ToString(), code);
                if (isValid)
                {
                    Session["code"] = null;
                }
                else
                {
                    response.code = 301;
                    response.message = Resource.Messages.Error.errorCodeInvalid;
                }
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> EnableAuthenticationMethod()
        {
            var accountDto = new AccountDto();
            ResponseAuthenticationEnable response = new ResponseAuthenticationEnable();
            try
            {
                await Task.Factory.StartNew(() =>
                {
                    response = accountDto.EnableAuthenticationMethod(
                        Convert.ToInt64(Session["id"]));
                });
            }
            catch (Exception ex)
            {
                response.code = -1;
                response.message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> CancelAuthenticatorMethod()
        {
            var accountDto = new AccountDto();
            ResponseAuthenticationEnable response = new ResponseAuthenticationEnable();
            try
            {
                await Task.Factory.StartNew(() =>
                {
                    response = accountDto.CancelAuthenticationMethod(
                        Convert.ToInt64(Session["id"]));
                });
            }
            catch (Exception ex)
            {
                response.code = -1;
                response.message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}