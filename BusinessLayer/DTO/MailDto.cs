using BusinessLayer.Classes;
using Resource;
using System;
using System.Linq;
using Web.Helpers;

namespace BusinessLayer.DTO
{
    public class MailDto
    {
        private invesafeEntities bdContext = new invesafeEntities();

        /// <summary>
        /// Welcome message.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public string[] WelcomeMessage(long cliId)
        {
            string[] parameters = new string[] { };
            try
            {
                SP_MAIL_WELCOME_Result res = (from p in bdContext.SP_MAIL_WELCOME(cliId)
                                              select p).FirstOrDefault();

                parameters = res.GetType()
                    .GetProperties()
                    .Select(p =>
                    {
                        object value = p.GetValue(res, null);
                        return value == null ? null : value.ToString();
                    })
                    .ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
            }
            return parameters;
        }

        /// <summary>
        /// Passwords the reset.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public string[] PasswordReset(long cliId)
        {
            string[] parameters = new string[] { };
            try
            {
                SP_MAIL_RESETPASS_Result res = (from p in bdContext.SP_MAIL_RESETPASS(cliId)
                                                select p).FirstOrDefault();

                parameters = res.GetType()
                    .GetProperties()
                    .Select(p =>
                    {
                        object value = p.GetValue(res, null);
                        return value == null ? null : value.ToString();
                    })
                    .ToArray();
            }
            catch (Exception e) {
                Console.WriteLine(e.InnerException.Message);
            }

            return parameters;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cliId"></param>
        /// <returns></returns>
        public string[] ActiveAccount(long cliId)
        {
            string[] parameters = new string[] { };
            try
            {
                SP_MAIL_WELCOME_Result res = (from p in bdContext.SP_MAIL_WELCOME(cliId)
                                              select p).FirstOrDefault();

                parameters = res.GetType()
                    .GetProperties()
                    .Select(p =>
                    {
                        object value = p.GetValue(res, null);
                        return value == null ? null : value.ToString();
                    })
                    .ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
            }
            return parameters;
        }
    }
}
