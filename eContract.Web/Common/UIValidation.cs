using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace eContract.Web
{
    /// <summary>
    /// 验证类
    /// </summary>
    public class UIValidation
    {
        public UIValidation()
        { 
        }
        //public UIValidation(string displayName)
        //    : this()
        //{
        //    DisplayName = displayName.Trim();
        //}
        //public UIValidation(string displayName,bool _required)
        //    : this(displayName)
        //{
        //    required = _required;
        //}
        //public UIValidation(string displayName,bool _required, string _type)
        //    : this(displayName,_required)
        //{
        //    type = _type;
        //}
        //public UIValidation(string displayName,bool _required, string _type, int _minlength)
        //    : this( displayName,_required, _type)
        //{
        //    minlength = _minlength;
        //}
        //public UIValidation(string displayName,bool _required, string _type, int _minlength, int _maxlength)
        //    : this( displayName,_required, _type, _minlength)
        //{
        //    maxlength = _maxlength;
        //}
        //public UIValidation(string displayName,bool _required, string _type, int _minlength, int _maxlength, int _min)
        //    : this( displayName,_required, _type, _minlength, _maxlength)
        //{
        //    min = _min;
        //}
        //public UIValidation(string displayName,bool _required, string _type, int _minlength, int _maxlength, int _min, int _max)
        //    : this( displayName,_required, _type, _minlength, _maxlength, _min)
        //{
        //    max = _max;
        //}
        public string DisplayName = "";

        /// <summary>
        /// 是否必填
        /// </summary>
        public bool required = false;
        /// <summary>
        /// 最小长度
        /// </summary>
        public int? minlength {get;set;}
        /// <summary>
        /// 最大长度
        /// </summary>
        public int? maxlength { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        public int? min { get; set; }
        /// <summary>
        /// 最大值
        /// </summary>
        public int? max { get; set; }

        public string equalTo = "";

        /// <summary>
        /// 数据验证类型
        /// </summary>
        public string vaildKey = "";

        /// <summary>
        /// 默认选中的值，主要应用与选则列表
        /// </summary>
        public string DefaultValue = "";

        public bool disabled = false;

        /// <summary>
        /// 值是否转换为资源值 默认是false
        /// </summary>
        public bool ValueTranslate = false;

        public int LabelLength = 2;
        /// <summary>
        /// 数据类型
        /// </summary>
        public string type="";

        public string ToJson() {
            StringBuilder json = new StringBuilder();
            json.Append("validate=\"{");
            json.Append("required:" + required.ToString().ToLower());
            if (!string.IsNullOrEmpty(type))
            {
                json.Append(",type:'" + type + "'");
            }
            if (!string.IsNullOrEmpty(vaildKey))
            {
                json.Append("," + vaildKey+":true");
            }
            if (minlength != null)
                json.Append(",minlength:" + minlength.ToString());
            if (minlength != null)
                json.Append(",maxlength:" + maxlength.ToString());
            if (min != null)
                json.Append(",min:" + min.ToString());
            if (max != null)
                json.Append(",max:" + max.ToString());
            if (!string.IsNullOrEmpty(equalTo))
            {
                json.Append(",equalTo:'" + equalTo.Trim()+"'");
            }
            json.Append("}\"");
            return json.ToString();
        }

        public string ToJson(object rules)
        {
            IDictionary<string, object> rule = rules != null ? (IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(rules) : null;
            return ToJson(rule);
        }
        public string ToJson(IDictionary<string, object> rules)
        {
            StringBuilder json = new StringBuilder();
            json.Append("validate=\"{");
            json.Append("required:" + required.ToString().ToLower());
            if (rules != null && rules.Count > 0)
            {
                foreach (var key in rules.Keys)
                {
                    if (!string.IsNullOrEmpty(key))
                    {
                        if (rules[key].GetType().Name.Equals("Boolean")
                            || rules[key].GetType().Name.Equals("Int32")
                             || rules[key].GetType().Name.Equals("Decimal")
                            )
                        {
                            json.Append("," + key + ":" + rules[key].ToString().ToLower().Trim() + "");
                        }
                        else
                            json.Append("," + key + ":'" + rules[key].ToString() + "'");
                       
                    }
                }
            }
            if (!string.IsNullOrEmpty(type))
            {
                json.Append(",type:'" + type + "'");
            }
            if (!string.IsNullOrEmpty(vaildKey))
            {
                json.Append("," + vaildKey + ":true");
            }
            if (minlength != null)
                json.Append(",minlength:" + minlength.ToString());
            if (minlength != null)
                json.Append(",maxlength:" + maxlength.ToString());
            if (min != null)
                json.Append(",min:" + min.ToString());
            if (max != null)
                json.Append(",max:" + max.ToString());
            if (!string.IsNullOrEmpty(equalTo))
            {
                json.Append(",equalTo:'" + equalTo.Trim() + "'");
            }
            json.Append("}\"");
            return json.ToString();
        }

        public int ColCount = 4;


    }
}