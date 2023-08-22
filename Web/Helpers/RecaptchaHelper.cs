﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;

namespace Web.Helpers
{
    public class RecaptchaHelper
    {
        public static bool Validate(string EncodedResponse)
        {
            var client = new System.Net.WebClient();
            string PrivateKey = ConfigurationManager.AppSettings["ReCaptchaSecret"].ToString();

            var GoogleReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", PrivateKey, EncodedResponse));
            var captchaResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<RecaptchaHelper>(GoogleReply);

            return (captchaResponse.Success.ToLower()== "true") ?true:false;
        }

        [JsonProperty("success")]
        public string Success
        {
            get { return m_Success; }
            set { m_Success = value; }
        }

        private string m_Success;
        [JsonProperty("error-codes")]
        public List<string> ErrorCodes
        {
            get { return m_ErrorCodes; }
            set { m_ErrorCodes = value; }
        }


        private List<string> m_ErrorCodes;
    }
}