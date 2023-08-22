using BusinessLayer.Classes;
using BusinessLayer.DTO;
using BusinessLayer.Enum;
using LazyCache;
using System;
using System.Configuration;
using System.Web.Script.Serialization;
using static BusinessLayer.Enum.Rules;

namespace BusinessLayer.Helpers
{
    public static class CommonHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string getStoragePath()
        {
            return ConfigurationManager.AppSettings["PathStorage"].ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string getHandlerPath()
        {
            return ConfigurationManager.AppSettings["PathHandler"].ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string getSitePath()
        {
            return ConfigurationManager.AppSettings["PathSite"].ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string getDashboardPath()
        {
            return ConfigurationManager.AppSettings["PathDashboard"].ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string getSendgridKey()
        {
            return ConfigurationManager.AppSettings["SendgridKey"].ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerNum"></param>
        /// <returns></returns>
        public static string getContainerName(int containerNum)
        {
            return String.Concat(containerName.GetName(typeof(containerName), containerNum).ToString().ToLower(),"/");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string getImageWidth(Rules.imageSize type)
        {
            string wValue = string.Empty;
            switch (type)
            {
                case Rules.imageSize.Photo: wValue = getParamCache().imageWidthPhoto; break;
                case Rules.imageSize.PhotoFull: wValue = getParamCache().imageWidthPhotoFull; break;
            }
            if (!wValue.Equals("")) wValue = "?w=" + wValue;
            return wValue;
        }

        /// <summary>
        /// Get global params from cache
        /// </summary>
        /// <returns></returns>
        public static CacheParams getParamCache()
        {
            return new CachingService().GetOrAdd("parameters", () => new CommonDto().GetParameters() as CacheParams, DateTimeOffset.Now.AddHours(24)); //24hs se renueva el cache
        }

        public static ResponseProjectBankAccount getBankAccount()
        {
            return new JavaScriptSerializer().Deserialize<ResponseProjectBankAccount>(getParamCache().bankAccount);
        }
    }
}
