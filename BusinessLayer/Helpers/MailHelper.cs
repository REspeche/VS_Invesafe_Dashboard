using System;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Reflection;
using System.Resources;
using System.Globalization;
using System.Web.Hosting;
using System.IO;
using BusinessLayer.Helpers;
using System.Threading.Tasks;

namespace Web.Helpers
{
    public class MailHelper
    {
        static string resource;
        static string[] parse;

        public static void SendMail(string v1, string[] v2)
        {
            resource = v1;
            parse = v2;
            Execute().Wait();
        }

        static async Task Execute()
        {
            try
            {
                String sMessage_HTML = String.Empty;
                String sMessage_TXT = String.Empty;

                //Get resource
                Assembly localisationAssembly = Assembly.Load("Resource");
                ResourceManager rm = new ResourceManager("Resource.Mail_Template." + resource, localisationAssembly);
                String template = rm.GetString("template", CultureInfo.CurrentCulture);
                String messageHtml = rm.GetString("messageHtml", CultureInfo.CurrentCulture).Replace("{link}", CommonHelper.getDashboardPath());
                String messageTxt = rm.GetString("messageTxt", CultureInfo.CurrentCulture).Replace("{link}", CommonHelper.getDashboardPath());
                String subjectStr = rm.GetString("subject", CultureInfo.CurrentCulture);
                String fromMail = rm.GetString("fromMail", CultureInfo.CurrentCulture);
                String fromName = rm.GetString("fromName", CultureInfo.CurrentCulture);
                String toMail = String.Format(rm.GetString("toMail", CultureInfo.CurrentCulture), parse);
                String toName = String.Format(rm.GetString("toName", CultureInfo.CurrentCulture), parse);

                //Read template file from the App_Data folder
                string pathTem = HostingEnvironment.MapPath("/App_Code/Templates/");
                using (var sr = new StreamReader(String.Concat(pathTem, template)))
                {
                    sMessage_HTML = String.Format(sr.ReadToEnd().ToString(), new string[] {
                        String.Format(messageHtml, parse) //Main menssage with parse params - formar Html
                    });
                    sMessage_TXT = String.Format(sr.ReadToEnd().ToString(), new string[] {
                        String.Format(messageTxt, parse) //Main menssage with parse params - format Txt
                    });
                }

                var apiKey = CommonHelper.getSendgridKey();
                var client = new SendGridClient(apiKey);

                // To
                var to = new EmailAddress(toMail, toName);

                // From
                var from = new EmailAddress(fromMail, fromName);

                // Subject and multipart/alternative Body
                var subject = String.Format(subjectStr, parse);

                // Body
                var plainTextContent = sMessage_TXT;
                var htmlContent = sMessage_HTML;

                var msg = SendGrid.Helpers.Mail.MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.InnerException.Message);
                throw ex;
            }
        }
    }
}