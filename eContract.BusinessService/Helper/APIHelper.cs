using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Security;
using System.IO;
using ComixSDK.EDI.Utils;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace eContract.BusinessService.Helper
{
    public class APIHelper
    {
        /// <summary>
        /// 生成token
        /// </summary>
        /// <param name="sortParams"></param>
        /// <returns></returns>
        public static string GetToken(SortedDictionary<string, string> sortParams)
        {
            string strToken = "";
            foreach (var item in sortParams)
            {
                if (!string.IsNullOrWhiteSpace(item.Value))
                {
                    if (!item.Key.ToLower().Equals("sign") && !item.Key.ToLower().Equals("token"))
                    {
                        if (!string.IsNullOrEmpty(strToken))
                        {
                            strToken += "&";
                        }
                        strToken += item.Key + "=" + item.Value;
                    }
                }

            }
            return GetToken(strToken);
        }


        public static string GetToken(string strToken)
        {
            return GetMD5(strToken + ConfigHelper.GetConfigString("System_MD5Key"), "utf-8");
        }

        public static string GetMD5(string s, string _input_charset)
        {
            byte[] buffer = new MD5CryptoServiceProvider().ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(s));
            StringBuilder builder = new StringBuilder(0x20);
            for (int i = 0; i < buffer.Length; i++)
            {
                builder.Append(buffer[i].ToString("x").PadLeft(2, '0'));
            }
            return builder.ToString();
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
    }
}
