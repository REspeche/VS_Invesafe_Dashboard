using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml;

namespace BusinessLayer.Classes
{
    public static class Commons
    {
        private static Random random = new Random();

        /// <summary>
        /// Gets the hash sha256.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string getHashSha256(string text)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }

        /// <summary>
        /// Gets the new unique identifier.
        /// </summary>
        /// <returns></returns>
        public static Guid getNewGuid()
        {
            return Guid.NewGuid();
        }

        /// <summary>
        /// Serialize/Deserialize JSON  
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static string serialize<T>(T obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, obj);
            string retVal = Encoding.UTF8.GetString(ms.ToArray());
            return retVal;
        }

        /// <summary>
        /// Deserializes the specified json.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">The json.</param>
        /// <returns></returns>
        public static T deserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            obj = (T)serializer.ReadObject(ms);
            ms.Close();
            return obj;
        }

        /// <summary>
        /// Converts the date.
        /// </summary>
        /// <param name="strDate">The string date.</param>
        /// <returns></returns>
        public static DateTime convertDate(string strDate)
        {
            DateTime outputDateTimeValue;
            if (DateTime.TryParseExact(strDate, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out outputDateTimeValue))
            {
                return outputDateTimeValue;
            }
            else
            {
                return new DateTime();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static String[] getClientCountry(HttpRequestBase request)
        {
            String[] strReturnVal = new String[2];
            string ipResponse = Methods.IPRequestHelper("http://ip-api.com/xml/" + getIPAddress(request));

            //return ipResponse;
            XmlDocument ipInfoXML = new XmlDocument();
            ipInfoXML.LoadXml(ipResponse);
            XmlNodeList responseXML = ipInfoXML.GetElementsByTagName("query");

            NameValueCollection dataXML = new NameValueCollection();

            dataXML.Add(responseXML.Item(0).ChildNodes[2].InnerText, responseXML.Item(0).ChildNodes[2].Value);

            strReturnVal[0] = responseXML.Item(0).ChildNodes[1].InnerText.ToString(); // Contry
            strReturnVal[1] = responseXML.Item(0).ChildNodes[2].InnerText.ToString();  // Contry Code 

            return strReturnVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string getIPAddress(HttpRequestBase request)
        {
            string ipAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return request.ServerVariables["REMOTE_ADDR"];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static double DateTimeToUnixTimestamp(DateTime? dateTime)
        {
            return (TimeZoneInfo.ConvertTimeToUtc(dateTime ?? DateTime.Now) -
                   new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string RandomReferenceCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
