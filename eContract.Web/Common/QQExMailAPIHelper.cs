using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace eContract.Web.Common
{
    public class QQExMailAPIHelper
    {
        public static string client_secret = "cf2a744c71230ce4215f768884371457";

        public static string client_id = "Comix-IT";

        public static string APIUrl = "http://openapi.exmail.qq.com:12211/openapi/mail";

        public static string Charset = "UTF-8";
        public static bool GetToken(ref string token,ref string strError)
        {
            SortedDictionary<string, string> dicParam = new SortedDictionary<string, string>();
            dicParam.Add("client_id", client_id);  //管理帐号
            dicParam.Add("grant_type", "client_credentials");  //授权类型
            dicParam.Add("client_secret", client_secret);  //接口key
           
            if (!SendRequstPost("https://exmail.qq.com/cgi-bin/token", dicParam, Charset, ref token))
            {
              
                strError = token;
                return false;
            }
            if (!string.IsNullOrWhiteSpace(token))
            {
               var result= ComixSDK.EDI.Utils.JSONHelper.FromJson<Dictionary<string, string>>(token);
               if (result != null && result.ContainsKey("access_token"))
               {
                   token= result["access_token"];
               }
                
            }
            return true;
        }

        public static int GetNewMailCount(string email,  ref string strError)
        {
          
            string token = "";
            if (!GetToken(ref token, ref strError))
            {
                return 0;
            }
            SortedDictionary<string, string> dicParam = new SortedDictionary<string, string>();
            dicParam.Add("alias", email);  //管理帐号
            dicParam.Add("access_token", token);  //授权类型
            string strResult = "";

            if (SendRequstPost(APIUrl+"/newcount", dicParam, Charset, ref strResult))
            {
                if (!string.IsNullOrWhiteSpace(strResult))
                {
                    Dictionary<string, string> result = ComixSDK.EDI.Utils.JSONHelper.FromJson<Dictionary<string, string>>(strResult);
                    if (result != null && result.ContainsKey("NewCount"))
                    {
                        return Convert.ToInt32(result["NewCount"]);
                    }
                }
            }
            return 0;
        }

        public static string GetAuthKey(string email, ref string strError)
        {
            string token = "";
            if (!GetToken(ref token, ref strError))
            {
                return "";
            }
            SortedDictionary<string, string> dicParam = new SortedDictionary<string, string>();
            dicParam.Add("alias", email);  //管理帐号
            dicParam.Add("access_token", token);  //授权类型
            string strResult = "";

            if (SendRequstPost(APIUrl+"/authkey", dicParam, Charset, ref strResult))
            {
                if (!string.IsNullOrWhiteSpace(strResult))
                {
                    Dictionary<string, string> result = ComixSDK.EDI.Utils.JSONHelper.FromJson<Dictionary<string, string>>(strResult);
                    if (result != null && result.ContainsKey("auth_key"))
                    {
                        return result["auth_key"];
                    }
                }
            }
            return "";
        }
        public static string GetSSOKey(string email, ref string strError)
        {

            string authkey = GetAuthKey(email, ref strError);
            if (string.IsNullOrWhiteSpace(authkey))
            {
                return "";
            }
            SortedDictionary<string, string> dicParam = new SortedDictionary<string, string>();
            dicParam.Add("user", email);  //用户账号
            dicParam.Add("ticket", authkey);  //授权类型
            dicParam.Add("agent", client_id);  //管理帐号
            dicParam.Add("user", email);  //用户账号
            string strResult = "";

            if (SendRequstPost("https://exmail.qq.com/cgi-bin/login", dicParam, Charset, ref strResult))
            {
                if (!string.IsNullOrWhiteSpace(strResult))
                {
                     return strResult;
                }
            }
            return "";
        }
        /// <summary>
        /// 发送post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <param name="charset"></param>
        /// <param name="strResult"></param>
        /// <returns></returns>
        public static bool SendRequstPost(string url, SortedDictionary<string, string> parameters, string charset, ref string strResult)
        {
            HttpWebRequest request = null;
            //HTTPSQ请求
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            request = WebRequest.Create(url) as HttpWebRequest;
            request.ProtocolVersion = HttpVersion.Version10;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Timeout = 60000;
            //如果需要POST数据   
            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                    }
                    i++;
                }
                byte[] data = Encoding.GetEncoding(charset).GetBytes(buffer.ToString());
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            try
            {
                var response = request.GetResponse() as HttpWebResponse;
                //打印返回值
                Stream stream1 = response.GetResponseStream();   //获取响应的字符串流
                StreamReader sr = new StreamReader(stream1); //创建一个stream读取流
                strResult = sr.ReadToEnd();   //从头读到尾，放到字符串html
                return true;
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }
            return false;
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受   
        }
        /// <summary>
        /// 获取通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public static SortedDictionary<string, string> GetSortRequestPost()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            var request = System.Web.HttpContext.Current.Request;
            coll = System.Web.HttpContext.Current.Request.Form;
            bool isRequest = false;
            if (coll == null || coll.AllKeys.Length <= 0)
            {
                coll = System.Web.HttpContext.Current.Request.QueryString;
                isRequest = true;
            }
            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;
            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], isRequest ? request.QueryString[requestItem[i]] : request.Form[requestItem[i]]);

            }

            return sArray;
        }

        /// <summary>
        /// 获取通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public static Dictionary<string, string> GetDicRequestPost()
        {
            int i = 0;
            Dictionary<string, string> sArray = new Dictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            var request = System.Web.HttpContext.Current.Request;
            coll = System.Web.HttpContext.Current.Request.Form;
            bool isRequest = false;
            if (coll == null || coll.AllKeys.Length <= 0)
            {
                coll = System.Web.HttpContext.Current.Request.QueryString;
                isRequest = true;
            }
            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;
            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], isRequest ? request.QueryString[requestItem[i]] : request.Form[requestItem[i]]);

            }
            return sArray;
        }

        /// <summary>
        /// 获取请求参数
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string RequestValue(string keys, string defaultValue = "")
        {
            if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.QueryString[keys]))
            {
                return System.Web.HttpContext.Current.Request.QueryString[keys];
            }
            else if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Form[keys]))
            {
                return System.Web.HttpContext.Current.Request.Form[keys];
            }
            return defaultValue;
        }

        public static string RequestValue(SortedDictionary<string, string> request, string keys, string defaultValue = "")
        {
            if (request.ContainsKey(keys))
            {
                if (!string.IsNullOrEmpty(request[keys]))
                    return request[keys];
            }
            return defaultValue;
        }
    }
}