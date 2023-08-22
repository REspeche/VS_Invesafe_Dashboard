using BusinessLayer.Classes;
using BusinessLayer.Enum;
using BusinessLayer.Helpers;
using com.Messente.Omnichannel.Api;
using com.Messente.Omnichannel.Model;
using Google.Authenticator;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using Web.Helpers;
using static BusinessLayer.Enum.Tables;

namespace BusinessLayer.DTO
{
    public class AccountDto
    {
        private invesafeEntities bdContext = new invesafeEntities();

        /// <summary>
        /// Validates the login.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public ResponseLogin ValidateLogin(
            string email,
            string password,
            string ip)
        {
            ResponseLogin response = new ResponseLogin();
            try
            {
                cli_client objCli = bdContext.cli_client.FirstOrDefault((c) => c.cli_email == email);

                if (objCli != null)
                {
                    if (objCli.cli_enable)
                    {
                        if (Commons.getHashSha256(password).Equals(objCli.cli_password))
                        {
                            bool outPeriod = false;
                            if (!objCli.cli_active)
                            {
                                double daysDiff = Math.Floor((DateTime.Now - objCli.cli_dateCreate).TotalDays);
                                if (daysDiff > Int32.Parse(CommonHelper.getParamCache().daysToActivate)) outPeriod = true;
                            }
                            if (!outPeriod)
                            {
                                response = GetLoginData(objCli);
                                response.token = Token.GenerateToken(email, password, ip, DateTime.UtcNow.Ticks); //generate a token
                            }
                            else
                            {
                                response.code = 316;
                                response.message = Resource.Messages.Error.loginError9;
                            }
                        }
                        else
                        {
                            response.code = 310;
                            response.message = Resource.Messages.Error.loginError1;
                        }
                    }
                    else
                    {
                        response.code = 311;
                        response.message = Resource.Messages.Error.loginError10;
                    }
                }
                else
                {
                    response.code = 312;
                    response.message = Resource.Messages.Error.loginError2;
                }
            }
            catch (Exception ex)
            {
                response.code = -1;
                response.message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Removes the account.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ResponseMessage RemoveAccount(long id)
        {
            ResponseLogin response = new ResponseLogin();
            try
            {
                cli_client objCli = bdContext.cli_client.FirstOrDefault((c) => c.cli_id == id);

                if (objCli != null)
                {
                    objCli.cli_active = false;
                    objCli.cli_enable = false;
                    bdContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                response.code = -1;
                response.message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Validates the register login facebook.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="gender">The gender.</param>
        /// <returns></returns>
        public ResponseLogin ValidateLoginFacebook(
            string email,
            string idFacebook,
            string firstName,
            string lastName,
            string gender,
            string birthday,
            string location,
            string ip,
            bool register = true)
        {
            ResponseLogin response = new ResponseLogin();
            try
            {
                if (idFacebook != null && email != null && !idFacebook.Equals("") && !email.Equals(""))
                {
                    cli_client objCli = bdContext.cli_client.FirstOrDefault((c) => c.cli_email == email);

                    if (objCli != null)
                    {
                        if (objCli.cli_enable)
                        {
                            if (objCli.cli_idFacebook != null)
                            {
                                if (objCli.cli_idFacebook == idFacebook)
                                {
                                    response = GetLoginData(objCli);
                                    response.token = Token.GenerateToken(email, idFacebook, ip, DateTime.UtcNow.Ticks); //generate a token
                                }
                                else
                                {
                                    response.code = 310;
                                    response.message = Resource.Messages.Error.loginError1;
                                }
                            }
                            else
                            {
                                ParamRegisterPerson paramFace = new ParamRegisterPerson();
                                paramFace.firstName = firstName;
                                paramFace.lastName = lastName;
                                paramFace.email = email;
                                paramFace.password = null;
                                paramFace.gender = gender;
                                paramFace.birthday = birthday;
                                paramFace.idFacebook = idFacebook;
                                response = RegisterPerson(paramFace, ip);
                            }
                        }
                        else
                        {
                            response.code = 311;
                            response.message = Resource.Messages.Error.loginError10;
                        }
                    }
                    else if (register)
                    {
                        ParamRegisterPerson paramFace = new ParamRegisterPerson();
                        paramFace.firstName = firstName;
                        paramFace.lastName = lastName;
                        paramFace.email = email;
                        paramFace.password = null;
                        paramFace.gender = gender;
                        paramFace.birthday = birthday;
                        paramFace.idFacebook = (idFacebook.Equals("app")) ? null : idFacebook;
                        response = RegisterPerson(paramFace, ip);
                    }
                    else
                    {
                        response.code = 212;
                        response.message = Resource.Messages.Warning.loginError8;
                    }
                }
                else
                {
                    response.code = 310;
                    response.message = Resource.Messages.Error.loginError1;
                }
            }
            catch (Exception ex)
            {
                response.code = -1;
                response.message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Registers the client.
        /// </summary>
        /// <param name="paramFace">The parameter face.</param>
        /// <returns></returns>
        public ResponseLogin RegisterPerson(
            ParamRegisterPerson paramFace,
            string ip
            )
        {
            ResponseLogin response = new ResponseLogin();
            try
            {
                bdContext.Configuration.ValidateOnSaveEnabled = false; //Disabling Automatic Validation

                cli_client objCli = (paramFace.idFacebook != null && paramFace.email == null) ?
                    bdContext.cli_client.FirstOrDefault((c) => c.cli_idFacebook == paramFace.idFacebook && c.cli_enable == true) :
                    bdContext.cli_client.FirstOrDefault((c) => c.cli_email == paramFace.email && c.cli_enable == true);

                if (objCli == null)
                {
                    objCli = new cli_client();

                    cou_country objCou = null;
                    lan_language objLan = null;
                    if (paramFace.location != null && !paramFace.location.Equals(""))
                        objCou = bdContext.cou_country.FirstOrDefault((c) => paramFace.location.Contains(c.cou_countryName));
                    if (paramFace.language != null && !paramFace.language.Equals(""))
                        objLan = bdContext.lan_language.FirstOrDefault((l) => paramFace.language.Contains(l.lan_languageCode));

                    //Verify null
                    if (objLan == null) objLan = bdContext.lan_language.FirstOrDefault((l) => Tables.language.Spanish.ToString().Contains(l.lan_languageCode));

                    objCli.cli_firstname = paramFace.firstName;
                    objCli.cli_lastname = paramFace.lastName;
                    if (paramFace.email != null) objCli.cli_email = paramFace.email;
                    if (paramFace.password != null) objCli.cli_password = Commons.getHashSha256(paramFace.password);
                    if (objCou != null) objCli.cou_idNationality = objCou.cou_id;
                    if (objLan != null) objCli.lan_idContact = objLan.lan_id;
                    if (paramFace.gender != null) objCli.cli_gender = (paramFace.gender == "male") ? (int?)Tables.gender.Male : (int?)Tables.gender.Female;
                    else objCli.cli_gender = (int?)Tables.gender.Undefined;
                    if (paramFace.birthday != null && !paramFace.birthday.Equals("")) objCli.cli_bornDate = Commons.convertDate(paramFace.birthday).ToUniversalTime();
                    if (paramFace.idFacebook != null) objCli.cli_idFacebook = paramFace.idFacebook;
                    objCli.cli_hash = paramFace.hash;
                    objCli.cli_enable = true;
                    objCli.cli_active = false;
                    objCli.cli_dateCreate = DateTime.UtcNow;
                    bdContext.cli_client.Add(objCli);
                    bdContext.SaveChanges();

                    // Save alerts
                    AlertDto alertDto = new AlertDto();
                    alertDto.CreateAlerts(objCli.cli_id, new long[] { 2, 3, 4, 5, 6, 7 });

                    response.code = 0;
                    if (response.code == 0)
                    {
                        if (bdContext.SP_GENERATE_HASH(objCli.cli_id).First() == 0)
                        {
                            //Send welcome message for mail
                            MailHelper.SendMail("Welcome", new MailDto().WelcomeMessage(objCli.cli_id));
                        }

                        response = GetLoginData(objCli);
                        response.token = Token.GenerateToken(paramFace.email, paramFace.password, ip, DateTime.UtcNow.Ticks); //generate a token                  
                    }
                }
                else
                {
                    if (paramFace.idFacebook != "")
                    {
                        response.code = 317;
                        response.message = Resource.Messages.Error.errorUserExist;
                    }
                    else
                    {
                        response.code = 309;
                        response.message = Resource.Messages.Error.errorUserExist;
                    }
                }
            }
            catch (Exception ex)
            {
                response.code = -1;
                response.message = ex.Message;
            }
            return response;
        }


        /// <summary>
        /// Sets the new password.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <param name="passNew">The pass new.</param>
        /// <returns></returns>
        public ResponseMail SetNewPassword(string hash, string passNew)
        {
            ResponseMail response = new ResponseMail();
            try
            {
                cli_client objCli = bdContext.cli_client.FirstOrDefault((c) => c.cli_hash == hash);
                if (objCli != null)
                {
                    if (!Commons.getHashSha256(passNew).Equals(objCli.cli_password))
                    {
                        objCli.cli_password = Commons.getHashSha256(passNew);
                        objCli.cli_dateModify = DateTime.UtcNow;
                        objCli.cli_hash = null;
                        bdContext.SaveChanges();

                        response.code = 0;
                    }
                    else
                    {
                        response.code = 211;
                        response.message = Resource.Messages.Warning.loginError6;
                    }
                }
                else
                {
                    response.code = 314;
                    response.message = Resource.Messages.Error.loginError4;
                }
            }
            catch (Exception ex)
            {
                response.code = -1;
                response.message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        public ResponseMail ActiveAccount(string hash)
        {
            ResponseMail response = new ResponseMail();
            try
            {
                cli_client objCli = bdContext.cli_client.FirstOrDefault((c) => c.cli_hash == hash);
                if (objCli != null)
                {
                    objCli.cli_active = true;
                    objCli.cli_dateModify = DateTime.UtcNow;
                    objCli.cli_hash = null;

                    axc_alexcli objAxc = bdContext.axc_alexcli.FirstOrDefault((a) => a.cli_id == objCli.cli_id && a.ale_id == 6);
                    if (objAxc != null) objAxc.axc_visible = false;

                    bdContext.SaveChanges();

                    // Save alerts
                    AlertDto alertDto = new AlertDto();
                    alertDto.ChangeState(objCli.cli_id, 6, alertState.Approved);

                    response.code = 0;
                }
                else
                {
                    response.code = 314;
                    response.message = Resource.Messages.Error.loginError4;
                }
            }
            catch (Exception ex)
            {
                response.code = -1;
                response.message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cliId"></param>
        /// <returns></returns>
        public ResponseMail SendEmailActiveAccount(long id)
        {
            ResponseMail response = new ResponseMail();
            try
            {
                cli_client objCli = bdContext.cli_client.FirstOrDefault((c) => c.cli_id == id);
                if (objCli != null)
                {
                    if (bdContext.SP_GENERATE_HASH(objCli.cli_id).First() == 0)
                    {
                        //Send welcome message for mail
                        MailHelper.SendMail("ActiveAccount", new MailDto().ActiveAccount(objCli.cli_id));
                    }

                    response.code = 0;
                }
                else
                {
                    response.code = 314;
                    response.message = Resource.Messages.Error.loginError4;
                }
            }
            catch (Exception ex)
            {
                response.code = -1;
                response.message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Change password.
        /// </summary>
        /// <param name="id">User id</param>
        /// <param name="passOld">The pass old.</param>
        /// <param name="passNew">The pass new.</param>
        /// <returns></returns>
        public ResponseMail ChangePassword(long id, string passOld, string passNew)
        {
            ResponseMail response = new ResponseMail();
            try
            {
                cli_client objCli = bdContext.cli_client.FirstOrDefault((c) => c.cli_id == id);
                if (objCli != null)
                {
                    if (Commons.getHashSha256(passOld).Equals(objCli.cli_password))
                    {
                        if (!passNew.Equals(passOld))
                        {
                            objCli.cli_password = Commons.getHashSha256(passNew);
                            objCli.cli_dateModify = DateTime.UtcNow;
                            objCli.cli_hash = null;
                            bdContext.SaveChanges();

                            response.code = 0;
                        }
                        else
                        {
                            response.code = 211;
                            response.message = Resource.Messages.Warning.loginError6;
                        }
                    }
                    else
                    {
                        response.code = 210;
                        response.message = Resource.Messages.Warning.loginError5;
                    }
                }
                else
                {
                    response.code = 315;
                    response.message = Resource.Messages.Error.loginError7;
                }
            }
            catch (Exception ex)
            {
                response.code = -1;
                response.message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public ResponseMessage ResetPassGenerateHash(string email)
        {
            ResponseMessage response = new ResponseMessage();
            try
            {
                cli_client objCli = bdContext.cli_client.FirstOrDefault((c) => c.cli_email == email);
                if (objCli != null)
                {
                    response.code = bdContext.SP_GENERATE_HASH(objCli.cli_id).First() ?? 7;
                    if (response.code == 0)
                    {
                        //Send reset pass message mail
                        MailHelper.SendMail("ResetPass", new MailDto().PasswordReset(objCli.cli_id));
                    }
                    else
                    {
                        response.code = 315;
                        response.message = Resource.Messages.Error.loginError7;
                    }
                }
                else
                {
                    response.code = 312;
                    response.message = Resource.Messages.Error.loginError2;
                }
            }
            catch (Exception ex)
            {
                response.code = -1;
                response.message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objCli"></param>
        /// <returns></returns>
        private ResponseLogin GetLoginData(cli_client objCli)
        {
            ResponseLogin response = new ResponseLogin();

            if (objCli.lan_language != null) response.locale = objCli.lan_language.lan_languageCode;
            else response.locale = Thread.CurrentThread.CurrentUICulture.ToString().Substring(0, 2);
            response.id = objCli.cli_id;
            response.email = objCli.cli_email;
            response.firstName = objCli.cli_firstname;
            response.lastName = objCli.cli_lastname;
            response.active = objCli.cli_active;
            response.codePhone = objCli.cli_codePhone;
            response.phone = objCli.cli_phone;
            response.authenticationMethod = objCli.cli_authenticationMethod;

            response.code = 0;

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ResponseTwoFactorAuthenticator GetTwoFactorAuthenticator(String email)
        {
            ResponseTwoFactorAuthenticator response = new ResponseTwoFactorAuthenticator();

            String appName = ConfigurationManager.AppSettings["authentication:appName"].ToString();
            String superSecretKey = ConfigurationManager.AppSettings["authentication:superSecretKey"].ToString();
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            var setupInfo = tfa.GenerateSetupCode(appName, email, superSecretKey, false, 6); //2 pixels give ~100x100px QRCode

            response.qrCodeImageUrl = setupInfo.QrCodeSetupImageUrl;
            response.manualEntrySetupCode = setupInfo.ManualEntryKey;

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Boolean ValidateTwoFactorAuthenticator(long id, String code)
        {
            String superSecretKey = ConfigurationManager.AppSettings["authentication:superSecretKey"].ToString();
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            Boolean isCorrectPIN = tfa.ValidateTwoFactorPIN(superSecretKey, code);

            if (isCorrectPIN)
            {
                cli_client objCli = bdContext.cli_client.FirstOrDefault((c) => c.cli_id == id);
                if (objCli != null)
                {
                    objCli.cli_authenticationMethod = 1;
                    bdContext.SaveChanges();

                    // Save alerts
                    AlertDto alertDto = new AlertDto();
                    alertDto.ChangeState(id, 7, alertState.Approved);
                }
            }

            return isCorrectPIN;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public void EnabledSMSAuthenticator(long id)
        {
            cli_client objCli = bdContext.cli_client.FirstOrDefault((c) => c.cli_id == id);
            if (objCli != null)
            {
                objCli.cli_authenticationMethod = 2;
                bdContext.SaveChanges();

                // Save alerts
                AlertDto alertDto = new AlertDto();
                alertDto.ChangeState(id, 7, alertState.Approved);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ResponseMessage SendCodeSMS(String fullPhone, String codeSMS)
        {
            ResponseMessage response = new ResponseMessage();

            // Configure HTTP basic authorization: basicAuth
            com.Messente.Omnichannel.Client.Configuration.Default.Username = ConfigurationManager.AppSettings["messente:userName"].ToString();
            com.Messente.Omnichannel.Client.Configuration.Default.Password = ConfigurationManager.AppSettings["messente:password"].ToString();

            List<object> messages = new List<object>();
            var sms = new SMS(sender: ConfigurationManager.AppSettings["messente:sender"].ToString(), text: "Tu código Invesafe es " + codeSMS);

            messages.Add(sms);

            var apiInstance = new OmnimessageApi();
            var omnimessage = new Omnimessage(to: "+549" + fullPhone, messages: messages);

            try
            {
                OmniMessageCreateSuccessResponse result = apiInstance.SendOmnimessage(omnimessage);
            }
            catch (Exception ex)
            {
                response.code = -1;
                response.message = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ResponseAuthenticationEnable EnableAuthenticationMethod(long id)
        {
            ResponseAuthenticationEnable response = new ResponseAuthenticationEnable();
            cli_client objCli = bdContext.cli_client.FirstOrDefault((c) => c.cli_id == id);
            if (objCli != null)
            {
                response.authenticationMethod = objCli.cli_authenticationMethod;
                response.codePhone = objCli.cli_codePhone;
                response.phone = objCli.cli_phone;
            }

            return response;
        }

        /// <summary>
        /// Removes the account.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ResponseMessage UpdatePhone(long id, String codePhone, String phone)
        {
            ResponseLogin response = new ResponseLogin();
            try
            {
                cli_client objCli = bdContext.cli_client.FirstOrDefault((c) => c.cli_id == id);

                if (objCli != null)
                {
                    objCli.cli_codePhone = codePhone;
                    objCli.cli_phone = phone;
                    bdContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                response.code = -1;
                response.message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ResponseAuthenticationEnable CancelAuthenticationMethod(long id)
        {
            ResponseAuthenticationEnable response = new ResponseAuthenticationEnable();
            cli_client objCli = bdContext.cli_client.FirstOrDefault((c) => c.cli_id == id);
            if (objCli != null)
            {
                objCli.cli_authenticationMethod = 0;
                bdContext.SaveChanges();

                response.authenticationMethod = 0;
            }

            return response;
        }
    }
}
