using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using ComixSDK.EDI.Utils;

namespace eContract.Web.Common
{
    /// <summary>
    /// JSON序列化和反序列化辅助类
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// JSON序列化
        /// </summary>
        public static string JsonSerializer<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }

        /// <summary>
        /// JSON反序列化
        /// </summary>
        public static T JsonDeserialize<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }

        /// <summary>
        /// GetTokenError
        /// </summary>
        /// <returns></returns>
        public static string GetTokenError()
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(new JsonMsg() { Code = 401, Msg = "请求验证失败。" });
        }

        public static string GetDataError()
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(new JsonMsg() { Code = 500, Msg = "请求数据错误。" });

        }

        public static string GetErrorMsg(string errMsg, string fileName = "")
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(new MyJsonMsg() { Code = 500, Msg = errMsg, FileName = fileName });

        }

        public static string GetErrorMsg(int iCode, string errMsg, string fileName = "")
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(new MyJsonMsg() { Code = iCode, Msg = errMsg, FileName = fileName });

        }

        public static string GetFirstMsg(string errMsg)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(new JsonMsg() { Code = 200, Msg = errMsg });

        }

        public static string GetJson(object obj)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(obj);
        }

        public static string GetSuccessMsg()
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(new JsonMsg() { Code = 200, Msg = "" });
        }

        public static string GetSuccessMsg(string strMsg, string fileName = "")
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(new MyJsonMsg() { Code = 200, Msg = strMsg, FileName = fileName });
        }


        public static string DataTableToJson(DataTable dt)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    result.Add(dc.ColumnName, dr[dc]);
                }
                list.Add(result);
            }
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(list);
        }

        public static string DataTableListToJSON(Dictionary<string, DataTable> dicData)
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值

            Dictionary<string, ArrayList> dic = new Dictionary<string, ArrayList>();

            foreach (string str in dicData.Keys)
            {
                ArrayList arrayList = new ArrayList();
                DataTable dt = dicData[str];
                foreach (DataRow dataRow in dt.Rows)
                {
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();  //实例化一个参数集合
                    foreach (DataColumn dataColumn in dt.Columns)
                    {
                        dictionary.Add(dataColumn.ColumnName, dataRow[dataColumn.ColumnName].ToString());
                    }
                    arrayList.Add(dictionary); //ArrayList集合中添加键值
                }
                dic.Add(str, arrayList);
            }
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(dic);
        }

        /// <summary>
        /// json格式转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static T FromJson<T>(string strJson) where T : class
        {
            return new JavaScriptSerializer().Deserialize<T>(strJson);
        }

        /// <summary>
        /// 将json转换为DataTable
        /// </summary>
        /// <param name="strJson">得到的json</param>
        /// <returns></returns>
        public static DataTable JsonToDataTable(string strJson)
        {
            //转换json格式
            strJson = strJson.Replace(",\"", "*\"").Replace("\":", "\"#");
            //取出表名   
            var rg = new Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
            //string strName = rg.Match(strJson).Value;
            DataTable tb = null;
            //去除表名   
            strJson = strJson.Substring(strJson.IndexOf("[") + 1);
            strJson = strJson.Substring(0, strJson.IndexOf("]"));

            //获取数据   
            rg = new Regex(@"(?<={)[^}]+(?=})");
            MatchCollection mc = rg.Matches(strJson);
            for (int i = 0; i < mc.Count; i++)
            {
                string strRow = mc[i].Value;
                string[] strRows = strRow.Split('*');

                //创建表   
                if (tb == null)
                {
                    tb = new DataTable();
                    //tb.TableName = strName;
                    foreach (string str in strRows)
                    {
                        var dc = new DataColumn();
                        string[] strCell = str.Split('#');

                        if (strCell[0].Substring(0, 1) == "\"")
                        {
                            int a = strCell[0].Length;
                            dc.ColumnName = strCell[0].Substring(1, a - 2);
                        }
                        else
                        {
                            dc.ColumnName = strCell[0];
                        }
                        tb.Columns.Add(dc);
                    }
                    tb.AcceptChanges();
                }

                //增加内容   
                DataRow dr = tb.NewRow();
                for (int r = 0; r < strRows.Length; r++)
                {
                    dr[r] = strRows[r].Split('#')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "").Replace("null", "").Replace("?", "*");
                }
                tb.Rows.Add(dr);
                tb.AcceptChanges();
            }

            return tb;
        }

        public static void Write(string msg)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            string directory = $"{basePath}Log";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            string path = $"{directory}\\Log{DateTime.Now.ToString("yyyyMMdd")}.txt";
            if (!File.Exists(path))
            {
                FileStream fs1 = new FileStream(path, FileMode.Create, FileAccess.Write);//创建写入文件 
                lock (fs1)
                {
                    StreamWriter sw = new StreamWriter(fs1, Encoding.Default);//转码
                    sw.WriteLine(msg);//开始写入值
                    sw.Close();
                    fs1.Close();
                }
            }
            else
            {
                FileStream fs = new FileStream(path, FileMode.Append);//文本加入不覆盖
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);//转码
                sw.WriteLine(msg);//开始写入值
                sw.Close();
                fs.Close();
            }
        }
    }

    public class MyJsonMsg
    {
        public int Code { get; set; }
        public string Msg { get; set; }
        public string FileName { get; set; }

    }
}