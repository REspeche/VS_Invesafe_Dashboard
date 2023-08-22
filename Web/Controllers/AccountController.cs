using BusinessLayer.Classes;
using BusinessLayer.DTO;
using BusinessLayer.Enum;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web.Filters;

namespace Web.Controllers
{
    public class AccountController : BaseController
    {
        // Autentificacion

        [CompressFilter(Order = 1)]
        [CacheFilter(Duration = 600, Order = 2)]
        public ActionResult Login()
        {
            if ((Boolean)Session["isLogin"]) return RedirectToAction("Home", "Dashboard");
            else return View();
        }

        [CompressFilter(Order = 1)]
        [CacheFilter(Duration = 600, Order = 2)]
        public ActionResult SignUp()
        {
            if ((Boolean)Session["isLogin"]) return RedirectToAction("Home", "Dashboard");
            else return View();
        }

        [CompressFilter(Order = 1)]
        [CacheFilter(Duration = 600, Order = 2)]
        public ActionResult Forgot()
        {
            if ((Boolean)Session["isLogin"]) return RedirectToAction("Home", "Dashboard");
            else return View();
        }

        [CompressFilter(Order = 1)]
        [CacheFilter(Duration = 600, Order = 2)]
        public ActionResult ResetPassword(string c)
        {
            if ((Boolean)Session["isLogin"]) return RedirectToAction("Home", "Dashboard");
            else return View();
        }

        /// <summary>
        /// Returns the session.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CompressFilter(Order = 1)]
        public JsonResult returnSession()
        {
            ResponseLogin response = new ResponseLogin();

            response.code = 0;
            response.id = Convert.ToInt64(Session["id"]);
            response.fullName = (Session["fullName"] == null) ? "" : Session["fullName"].ToString();
            response.locale = (Session["id"] == null) ? "es" : Session["locale"].ToString();
            response.active = Convert.ToBoolean(Session["active"]);

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Closes the session.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CompressFilter(Order = 1)]
        public JsonResult closeSession()
        {
            ResponseMessage response = new ResponseMessage();

            response.code = 0;
            response.action = Rules.actionMessage.RedirectToLogin;
            RemoveCookieCulture();
            Session.Abandon();

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Logins the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        [HttpPost]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> Login(string email, string password)
        {
            ResponseLogin response = new ResponseLogin();
            var accountDto = new AccountDto();
            await Task.Factory.StartNew(() =>
            {
                response = accountDto.ValidateLogin(
                    email, 
                    password, 
                    Commons.getIPAddress(Request)
                    );
                if (response.code == 0) SetSessionVars(response);
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Logins the facebook.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        [HttpPost]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> LoginFacebook(
            string email,
            string id,
            string firstName,
            string lastName,
            string gender,
            string birthday,
            string location)
        {
            ResponseLogin response = new ResponseLogin();
            var accountDto = new AccountDto();
            await Task.Factory.StartNew(() =>
            {
                response = accountDto.ValidateLoginFacebook(
                    email, 
                    id, 
                    firstName, 
                    lastName, 
                    gender, 
                    birthday, 
                    location, 
                    Commons.getIPAddress(Request),
                    false
                    );
                if (response.code == 0) SetSessionVars(response);
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Registers the person.
        /// </summary>
        /// <param name="firstname">The firstname.</param>
        /// <param name="lastname">The lastname.</param>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        [HttpPost]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> RegisterPerson(
            string firstname, 
            string lastname, 
            string email, 
            string password, 
            string idFacebook,
            string gender,
            string birthday,
            string location)
        {
            ResponseLogin response = new ResponseLogin();
            var accountDto = new AccountDto();
            String[] dataCountry = Commons.getClientCountry(Request);
            await Task.Factory.StartNew(() =>
            {
                ParamRegisterPerson paramPerson = new ParamRegisterPerson();
                paramPerson.firstName = firstname;
                paramPerson.lastName = lastname;
                paramPerson.email = email;
                paramPerson.password = password;
                paramPerson.idFacebook = idFacebook;
                paramPerson.location = dataCountry[0];
                paramPerson.language = dataCountry[1];

                response = accountDto.RegisterPerson(
                    paramPerson,
                    Commons.getIPAddress(Request)
                );

                if (response.code == 0) SetSessionVars(response);
                else if (response.code == 317)
                {
                    response = accountDto.ValidateLoginFacebook(
                        email,
                        idFacebook,
                        firstname,
                        lastname,
                        gender,
                        birthday,
                        location,
                        Commons.getIPAddress(Request),
                        false
                        );
                    if (response.code == 0) SetSessionVars(response);
                }
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Send mail for reset password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        [HttpPost]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> ResetPasswordSendMail(string email)
        {
            ResponseMessage response = new ResponseMessage();
            try
            {
                await Task.Factory.StartNew(() =>
                {
                    response = new AccountDto().ResetPassGenerateHash(email);
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
        /// Sets the new password.
        /// </summary>
        /// <param name="passNew">The pass new.</param>
        /// <param name="hash">The hash.</param>
        /// <returns></returns>
        [HttpPost]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> ResetPasswordNewPass(string passNew, string hash)
        {
            var accountDto = new AccountDto();
            ResponseMessage response = new ResponseMessage();
            try
            {
                await Task.Factory.StartNew(() =>
                {
                    response = accountDto.SetNewPassword(hash, passNew);
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
        /// <param name="c"></param>
        /// <returns></returns>
        [CompressFilter(Order = 1)]
        [CacheFilter(Duration = 600, Order = 2)]
        public ActionResult ActiveAccount(string c)
        {
            var accountDto = new AccountDto();
            ResponseMessage response = new ResponseMessage();
            try
            {
                response = accountDto.ActiveAccount(c);
                if (response.code == 0) Session["active"] = true;
            }
            catch (Exception ex)
            {
                response.code = -1;
                response.message = ex.Message;
            }
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        [HttpPost]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> SendEmailActiveAccount()
        {
            var accountDto = new AccountDto();
            ResponseMessage response = new ResponseMessage();
            try
            {
                await Task.Factory.StartNew(() =>
                {
                    response = accountDto.SendEmailActiveAccount(Convert.ToInt64(Session["id"]));
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
        /// <param name="passOld"></param>
        /// <param name="passNew"></param>
        /// <returns></returns>
        [HttpPost]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> ChangePassword(string passOld, string passNew)
        {
            var accountDto = new AccountDto();
            ResponseMessage response = new ResponseMessage();
            try
            {
                await Task.Factory.StartNew(() =>
                {
                    response = accountDto.ChangePassword(Convert.ToInt64(Session["id"]), passOld, passNew);
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
        /// Sets the session vars.
        /// </summary>
        /// <param name="response">The response.</param>
        private void SetSessionVars(ResponseLogin response)
        {
            Session["isLogin"] = true;

            // User's session
            Session["id"] = response.id;
            Session["fullName"] = response.fullName;
            Session["firstName"] = response.firstName;
            Session["email"] = response.email;
            Session["active"] = response.active;
            Session["token"] = response.token;
            Session["phone"] = response.fullPhone;
            Session["authenticationMethod"] = response.authenticationMethod;
            if (!Session["locale"].ToString().Equals(response.locale)) Session["locale"] = response.locale;
        }
    }
}