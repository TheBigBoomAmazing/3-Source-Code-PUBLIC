using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.IO;
using System.Web;
using System.Collections.Specialized;
using System.Reflection;
using System.ComponentModel;
using System.Data;
using System.Collections;
using System.Net;

namespace eContract.Common
{
    //public class MyResourceExpressionBuilder : System.Web.Compilation.ExpressionBuilder
    //{
    //    public override System.CodeDom.CodeExpression GetCodeExpression(System.Web.UI.BoundPropertyEntry entry, object parsedData, System.Web.Compilation.ExpressionBuilderContext context)
    //    {
    //        return new System.CodeDom.CodePrimitiveExpression(Ferrero.Common.Resource.VSASYSResource.ResourceManager.GetString(entry.Expression));
    //    }
    //}


    public static class Utils
    {
        #region 判断远程文件是否存在
        /// 判断远程文件是否存在
        public static bool RemoteFileExists(string fileUrl)
        {
            HttpWebRequest re = null;
            HttpWebResponse res = null;
            try
            {
                re = (HttpWebRequest)WebRequest.Create(fileUrl);
                res = (HttpWebResponse)re.GetResponse();
                if (res.ContentLength != 0)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (re != null)
                {
                    re.Abort();//销毁关闭连接
                }
                if (res != null)
                {
                    res.Close();//销毁关闭响应
                }
            }
            return false;
        }
        #endregion

        /// <summary>  
        /// 扩展方法：根据枚举值得到相应的枚举定义字符串  
        /// </summary>  
        /// <param name="value"></param>  
        /// <param name="enumType"></param>  
        /// <returns></returns>  
        public static String ToEnumString(this int value, Type enumType)
        {
            NameValueCollection nvc = GetEnumStringFromEnumValue(enumType);
            return nvc[value.ToString()];
        }

        /// <summary>  
        /// 根据枚举类型得到其所有的 值 与 枚举定义字符串 的集合  
        /// </summary>  
        /// <param name="enumType"></param>  
        /// <returns></returns>  
        public static NameValueCollection GetEnumStringFromEnumValue(Type enumType)
        {
            NameValueCollection nvc = new NameValueCollection();
            Type typeDescription = typeof(DescriptionAttribute);
            System.Reflection.FieldInfo[] fields = enumType.GetFields();
            string strText = string.Empty;
            string strValue = string.Empty;
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum)
                {
                    strValue = ((int)enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null)).ToString();
                    nvc.Add(strValue, field.Name);
                }
            }
            return nvc;
        }  

        public static DataTable ToDataTable(this IList list)
        {
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    result.Columns.Add(pi.Name, pi.PropertyType);
                }

                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(list[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }

        public static string ToUrlParameter<T>(T t)
        {
            string result = "";
            Type type = t.GetType();
            System.Reflection.PropertyInfo[] ps = type.GetProperties();
            foreach (PropertyInfo i in ps)
            {
                object obj = i.GetValue(t, null);
                result += i.Name + "=" + obj.ToString() + "&";
            }
            if (!string.IsNullOrEmpty(result))
                result = result.TrimEnd('&');
            return result;
        }

        public static string ToUrlParameter2<T>(T t)
        {
            string result = "";
            Type type = t.GetType();
            System.Reflection.PropertyInfo[] ps = type.GetProperties();
            foreach (PropertyInfo i in ps)
            {
                if (i.Name.Equals("param_json"))
                    continue;
                object obj = i.GetValue(t, null);
                result += i.Name + "=" + obj.ToString() + "&";
            }
            if (!string.IsNullOrEmpty(result))
                result = result.TrimEnd('&');
            return result;
        }

        public static string ForSQL(this string str)
        {
            return Utils.ToSQLStr(str);
        }

        public static bool CheckDecimal(string str)
        {
            decimal result = 0;
            try
            {
                result = Convert.ToDecimal(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static decimal ToDecimal(this string str)
        {
            decimal result = 0;
            if (decimal.TryParse(str, out result))
            {
                return result;
            }
            return 0;
        }

        public static bool CheckInt(string str)
        {
            int result = 0;
            try
            {
                result = Convert.ToInt32(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static int ToInt(this string str)
        {
            int result = 0;
            if (int.TryParse(str, out result))
            {
                return result;
            }
            return 0;
        }

        public static double ToDouble(string str)
        {
            double result = 0;
            if (double.TryParse(str, out result))
            {
                return result;
            }
            return 0;
        }

        public static string ToSQLStr(string str)
        {
            StringBuilder sb = new StringBuilder(str.Trim());
            sb.Replace("'", "''");
            sb.Insert(0, "'", 1);
            sb.Append('\'', 1);
            sb.Replace(@"\", @"\\");

            return sb.ToString();
        }
        
        /// <summary>
        /// 格式化并过滤字符串
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <returns></returns>
        public static string ToSqlLikeStr(string str)
        {
            StringBuilder sb = new StringBuilder(str.Trim());
            sb.Replace("'", "''");
            sb.Insert(0, "'%", 1);
            sb.Append("%\'");
            sb.Replace(@"\", @"\\");

            return sb.ToString();
        }

        public static string ToInString(string str)
        {
            string[] strorgs = str.Split(',');
            StringBuilder strBuilder = new StringBuilder("(");
            foreach (string strorg in strorgs)
            {
                if (!string.IsNullOrEmpty(strorg))
                    strBuilder.Append("'" + strorg + "',");
            }
            strBuilder.Append(")");

            string strReturn = strBuilder.ToString().Replace(",)", ")");
            if (strReturn.Equals("()"))
                return "";
            else
                return strReturn;
        }
        /// <summary>
        /// 组合查询条件
        /// </summary>
        public class QueryCondition
        {
            public static QueryCondition Create()
            {
                return new QueryCondition();
            }

            public override string ToString()
            {
                return ConditionSQL;
            }

            public string TopString()
            {
                return TopSQL;
            }

            protected string ConditionSQL { get; set; }

            protected string TopSQL { get; set; }

            /// <summary>
            /// 是否有Where条件
            /// </summary>
            public bool ExistWhereString { get; protected set; }

            /// <summary>
            /// 是否有Order条件
            /// </summary>
            public bool ExistOrderString { get; protected set; }

            public bool ExistTopString { get; protected set; }

            public QueryCondition()
            {
                ConditionSQL = string.Empty;
                ExistOrderString = false;
                ExistWhereString = false;
                ExistTopString = false;
            }

            string FormatString(string value)
            {
                return value.Replace("'", "''");
            }

            void CheckWhereStart()
            {
                if (ExistWhereString)
                {
                    this.ConditionSQL += " and ";
                }
            }

            void CheckWhereStartOr()
            {
                if (ExistWhereString)
                {
                    this.ConditionSQL += " or ";
                }
            }

            void CheckWhereStartAnd()
            {
                if (ExistWhereString)
                {
                    this.ConditionSQL += " and ( ";
                }
            }

            public QueryCondition Top(int topCount)
            {
                if (topCount > 0)
                {
                    this.TopSQL = " TOP " + topCount + " ";
                }

                return this;
            }

            public QueryCondition AndLike(string property, string value)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    CheckWhereStartAnd();
                    this.ConditionSQL += " UPPER(" + property + ") like '%" + FormatString(value.ToUpper().Trim()) + "%'";
                    ExistWhereString = true;
                }
                return this;
            }

            public QueryCondition OrLike(string property, string value)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    CheckWhereStartOr();
                    this.ConditionSQL += " UPPER(" + property + ") like '%" + FormatString(value.ToUpper().Trim()) + "%'";
                    ExistWhereString = true;
                }
                return this;
            }

            public QueryCondition Like(string property, string value)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    CheckWhereStart();
                    this.ConditionSQL += " UPPER(" + property + ") like '%" + FormatString(value.ToUpper().Trim()) + "%'";
                    ExistWhereString = true;
                }
                return this;
            }

            public QueryCondition Or(string property, string value)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    CheckWhereStartOr();
                    this.ConditionSQL += " UPPER(" + property + ") = '" + FormatString(value.ToUpper().Trim()) + "'";
                    ExistWhereString = true;
                }
                return this;
            }

            public QueryCondition Equals(string property, object value)
            {
                if (value != null)
                {
                    CheckWhereStart();
                    if (value.GetType().IsValueType)
                    {
                        this.ConditionSQL += property + "=" + value;
                    }
                    else
                    {
                        this.ConditionSQL += " UPPER(" + property + ")='" + FormatString(value.ToString().ToUpper().Trim()) + "'";
                    }
                    ExistWhereString = true;
                }
                return this;
            }

            public QueryCondition NotEquals(string property, object value)
            {
                if (value != null)
                {
                    CheckWhereStart();
                    if (value.GetType().IsValueType)
                    {
                        this.ConditionSQL += property + "<>" + value;
                    }
                    else
                    {
                        this.ConditionSQL += " UPPER(" + property + ")<>'" + FormatString(value.ToString().ToUpper().Trim()) + "'";
                    }
                    ExistWhereString = true;
                }
                return this;
            }

            public QueryCondition EqualsNull(string property)
            {

                CheckWhereStart();
                this.ConditionSQL += " (" + property + " is null or " + property + "='' ) ";
                ExistWhereString = true;
                return this;
            }

            public QueryCondition In(string property, params string[] values)
            {
                if (values.Length > 0)
                {
                    CheckWhereStart();
                    string[] vs = new string[values.Length];
                    for (int i = 0; i < values.Length; i++)
                    {
                        vs[i] = FormatString(values[i].ToString().Trim());
                    }
                    this.ConditionSQL += " UPPER(" + property + ") in ('" + string.Join("','", vs).ToUpper() + "')";
                    ExistWhereString = true;
                }
                return this;
            }

            public QueryCondition In(string property, params decimal[] values)
            {
                if (values.Length > 0)
                {
                    CheckWhereStart();
                    string[] vs = new string[values.Length];
                    for (int i = 0; i < values.Length; i++)
                    {
                        vs[i] = values[i].ToString();
                    }
                    this.ConditionSQL += property + " in (" + string.Join(",", vs) + ")";
                    ExistWhereString = true;
                }
                return this;
            }

            public QueryCondition Between(string property, string from, string to)
            {
                if (!string.IsNullOrEmpty(from) && !string.IsNullOrEmpty(to))
                {
                    CheckWhereStart();
                    this.ConditionSQL += property + " between '" + FormatString(from.Trim()) + "' and '" + FormatString(to.Trim()) + "'";
                    ExistWhereString = true;
                }
                return this;
            }

            public QueryCondition Less(string property, object value)
            {
                if (value != null)
                {
                    CheckWhereStart();
                    if (value.GetType().IsValueType)
                    {
                        this.ConditionSQL += property + "<" + value;
                    }
                    else
                    {
                        this.ConditionSQL += " UPPER(" + property + ")<" + FormatString(value.ToString().ToUpper().Trim()) + "'";
                    }
                    ExistWhereString = true;
                }
                return this;
            }
            #region Order
            public QueryCondition Order(params string[] property)
            {
                foreach (string p in property)
                {
                    this.Order(p);
                }
                return this;
            }

            public QueryCondition Order(string property)
            {
                return Order(property, false);
            }

            public QueryCondition Order(string property, bool desc)
            {
                if (!string.IsNullOrEmpty(property))
                {
                    if (!ExistOrderString)
                    {
                        this.ConditionSQL += " ORDER BY " + property;
                        ExistOrderString = true;
                    }
                    else
                    {
                        this.ConditionSQL += "," + property;
                    }
                    if (desc)
                    {
                        this.ConditionSQL += " DESC ";
                    }
                }
                return this;
            }
            #endregion


        }
        /// <summary>
        /// MD5散列值
        /// </summary>
        public static string MD5Encode(string str)
        {
            byte[] byteInput = UTF8Encoding.UTF8.GetBytes(str);
            MD5CryptoServiceProvider objMd5 = new MD5CryptoServiceProvider();
            byte[] byteOutput = objMd5.ComputeHash(byteInput);
            return BitConverter.ToString(byteOutput).Replace("-", "");
        }

        /// <summary>
        /// xml序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize(object obj)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            try
            {
                Type type = obj.GetType();
                XmlSerializer sz = new XmlSerializer(type);
                sz.Serialize(sw, obj);
            }
            finally
            {
                sw.Close();
            }
            return sb.ToString();
        }

        /// <summary>
        /// xml序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static void Serialize(object obj, string filePath)
        {
            Stream sw = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
            try
            {
                Type type = obj.GetType();
                XmlSerializer sz = new XmlSerializer(type);
                sz.Serialize(sw, obj);
            }
            finally
            {
                sw.Close();
            }
        }

        /// <summary>
        /// xml反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                return (T)xs.Deserialize(fs);
            }
            finally
            {
                fs.Close();
            }
        }

        /// <summary>
        /// 获得季度BPM编码
        /// </summary>
        /// <returns></returns>
        public static string GetBPM(DateTime dt)
        {
            string bpmString = string.Empty;
            switch (dt.Month)
            {
                case 1:
                    bpmString = "NDJF";
                    break;
                case 2:
                    bpmString = "NDJF";
                    break;
                case 3:
                    bpmString = "MAMJ";
                    break;
                case 4:
                    bpmString = "MAMJ";
                    break;
                case 5:
                    bpmString = "MAMJ";
                    break;
                case 6:
                    bpmString = "MAMJ";
                    break;
                case 7:
                    bpmString = "JASO";
                    break;
                case 8:
                    bpmString = "JASO";
                    break;
                case 9:
                    bpmString = "JASO";
                    break;
                case 10:
                    bpmString = "JASO";
                    break;
                case 11:
                    bpmString = "NDJF";
                    break;
                case 12:
                    bpmString = "NDJF";
                    break;

            }
            return bpmString;
        }

        /// <summary>
        /// 获得财年FM编码
        /// </summary>
        /// <returns></returns>
        public static string GetFYBySeparator(DateTime dt)
        {
            string fmString = string.Empty;
            if (dt >= (new DateTime(dt.Year, 7, 1)))
            {
                fmString = dt.AddYears(0).Year.ToString().Substring(2, 2) + "/" + dt.AddYears(1).Year.ToString().Substring(2, 2);
            }
            else
            {
                if (dt > DateTime.MinValue.AddYears(1))
                {
                    fmString = dt.AddYears(-1).Year.ToString().Substring(2, 2) + "/" + dt.AddYears(0).Year.ToString().Substring(2, 2);
                }
            }
            return fmString;
        }

        /// <summary>
        /// 获得财年FY编码
        /// </summary>
        /// <returns></returns>
        public static string GetFY(DateTime dt)
        {
            string fmString = string.Empty;
            if (dt >= (new DateTime(dt.Year, 7, 1)))
            {
                fmString = dt.AddYears(0).Year.ToString().Substring(2, 2) + dt.AddYears(1).Year.ToString().Substring(2, 2);
            }
            else
            {
                if (dt > DateTime.MinValue.AddYears(1))
                {
                    fmString = dt.AddYears(-1).Year.ToString().Substring(2, 2) + dt.AddYears(0).Year.ToString().Substring(2, 2);
                }
            }
            return fmString;
        }

        /// <summary>
        /// 从枚举中读取特性与值集合
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns>特性与值集合。若无特性，则默认枚举名称</returns>
        public static NameValueCollection GetNameValueFromEnum(Type enumType)
        {
            string name = string.Empty;
            string value = string.Empty;
            NameValueCollection coll = new NameValueCollection();
            FieldInfo[] fields = enumType.GetFields();
            Type typeDescription = typeof(DescriptionAttribute);

            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum)
                {
                    value = ((int)enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null)).ToString();
                    object[] arr = field.GetCustomAttributes(typeDescription, true);
                    if (arr.Length > 0)
                    {
                        DescriptionAttribute des = arr[0] as DescriptionAttribute;
                        name = des.Description;
                    }
                    else
                    {
                        name = field.Name;
                    }

                    coll.Add(name, value);
                }
            }

            return coll;
        }

        public static bool IsMaxDateTime(string strDatetime)
        {
            if (Convert.ToDateTime(strDatetime).ToString("yyyy-MM-dd") == "9999-12-31")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsMaxDateTime(DateTime strDatetime)
        {
            if (strDatetime.ToString("yyyy-MM-dd") == "9999-12-31")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string DateToHHMM(string strDate)
        {
            try
            {
               return  Convert.ToDateTime(strDate).ToString("yyyy-MM-dd HH:mm");
            }
            catch {
                return "";
            }
        }
        public static string DateToHHMM(DateTime dtDate)
        {
            return dtDate.ToString("yyyy-MM-dd HH:mm");
        }

        public static decimal ChinaRound(decimal value, int decimals)
        {
            if (value < 0)
            {
                return Math.Round(value + 5 / Convert.ToDecimal(Math.Pow(10, decimals + 1)), decimals, MidpointRounding.AwayFromZero);
            }
            else
            {
                return Math.Round(value, decimals, MidpointRounding.AwayFromZero);
            }
        }

        public static string GetDescriptionFromEnumValue(Enum value)
        {
            DescriptionAttribute attribute = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .SingleOrDefault() as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static string RandomNumber(int length, bool sleep)
        {
            if (sleep) System.Threading.Thread.Sleep(3);
            string result = "";
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                result += random.Next(10).ToString();
            }
            return result;
        }

        public static byte[] SHA1Encrypt(string str)
        {
            byte[] bytes = new UnicodeEncoding().GetBytes(str);
            SHA1 sha = new SHA1CryptoServiceProvider();
            return sha.ComputeHash(bytes);
        }

        public static string SecurityCharReplace(char securityChar, string str, int prefixShowCount = 0,
                                                 int suffixShowCount = 0)
        {
            var newStr = string.Empty;
            if (str.Length > prefixShowCount)
            {
                newStr += str.Substring(0, prefixShowCount);
                var charCount = str.Length - prefixShowCount - suffixShowCount;
                if (charCount > 0)
                {
                    for (int i = 0; i < charCount; i++)
                    {
                        newStr += securityChar;
                    }
                }

                if (suffixShowCount > 0)
                {
                    newStr += str.Substring(str.Length - suffixShowCount);
                }
            }
            else
            {
                return str;
            }

            return newStr;
        }

        public static string VarifyMobilePhone(string mobile)
        {
            var rtn = string.Empty;

            var dianxin = @"^1[3578][01379]\d{8}$";
            var dReg = new Regex(dianxin);
            var liantong = @"^1[34578][01256]\d{8}$";
            var tReg = new Regex(liantong);
            var yidong = @"^(134[012345678]\d{7}|1[34578][012356789]\d{8})$";
            var yReg = new Regex(yidong);
            if (!dReg.IsMatch(mobile) && !tReg.IsMatch(mobile) && !yReg.IsMatch(mobile))
            {
                rtn = "号码格式不正确";
            }

            return rtn;
        }

        #region manoen + 2016.03.13 22:16

        /// <summary>
        /// 去除字符串中所有的空格
        /// manoen
        /// </summary>
        public static string AllTrim(string s) {
            return Regex.Replace(s, @"\s", "");
        }

        /// <summary>
        /// 格式化SQL语句
        /// manoen
        /// </summary>
        /// <param name="sqlInsertField"></param>
        /// <returns></returns>
        public static string FormatSqlField(string sqlInsertField) {
            return "@" + sqlInsertField.Replace("[", "").Replace("]", "").Replace(",", ",@");
        }

        /// <summary>
        /// 格式化SQL语句
        /// manoen
        /// </summary>
        /// <param name="sqlField"></param>
        /// <returns></returns>
        public static string FormatSqlUpdateField(string sqlField) {
            StringBuilder sb = new StringBuilder();
            string[] spField = sqlField.Split(',');
            for (int i = 0; i < spField.Length; i++) {
                string b = spField[i].ToString();
                sb.Append(b + "=@" + b + ",");
            }
            return sb.ToString();
        }

        #endregion
    }
}
