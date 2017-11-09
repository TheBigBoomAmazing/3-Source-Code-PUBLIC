using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace eContract.Common
{
    /// <summary>
    /// 将检索规则 翻译成 where sql 语句,并生成相应的参数列表
    /// 如果遇到{CurrentUserID}这种，翻译成对应的参数
    /// </summary>
    public class FilterTranslator
    {
        //几个前缀/后缀
        protected char leftToken = '[';
        protected char paramPrefixToken = ':';
        protected char rightToken = ']';
        protected char groupLeftToken = '(';
        protected char groupRightToken = ')';
        protected char likeToken = '%';
        /// <summary>
        /// 参数计数器
        /// </summary>
        private int paramCounter = 0;

        //几个主要的属性
        public FilterGroup Group { get; set; }
        public string CommandText { get; private set; }
        public IList<FilterParam> Parms { get; private set; }

        public FilterTranslator()
            : this(null)
        {
        }

        public FilterTranslator(FilterGroup group)
        {
            this.Group = group;
            this.Parms = new List<FilterParam>();
        }
        public void Translate(ref List<FilterParam> filterParams)
        {
            StringBuilder bulider = new StringBuilder();
            if (filterParams == null)
            {
                filterParams = new List<FilterParam>();
            }
            this.CommandText = Translate(this.Group, bulider, ref filterParams);
        }

        public string Translate(FilterGroup group, StringBuilder bulider, ref List<FilterParam> filterParams)
        {
          
            if (group == null) return " 1=1 ";
            var appended = false;
            bulider.Append(groupLeftToken);
            if (group.rules != null)
            {
                foreach (var rule in group.rules)
                {
                    if (appended)
                        bulider.Append(GetOperatorQueryText(group.op));
                    bulider.Append(TranslateRule(rule,ref filterParams));
                    appended = true;
                }
            }
            if (group.groups != null)
            {
                foreach (var subgroup in group.groups)
                {
                    if (appended)
                        bulider.Append(GetOperatorQueryText(group.op));
                    bulider.Append(Translate(subgroup, bulider, ref filterParams));
                    appended = true;
                }
            }
            bulider.Append(groupRightToken);
            if (appended == false) return " 1=1 ";
            return bulider.ToString();
        }


        public string TranslateRule(FilterRule rule, ref List<FilterParam> filterParams)
        {
            StringBuilder bulider = new StringBuilder();
            if (rule == null) return " 1=1 ";

                bulider.Append(leftToken + rule.field + rightToken);
            //操作符
            bulider.Append(GetOperatorQueryText(rule.op));

            var op = rule.op.ToLower();
            if (op == "like" || op == "endwith")
            {
                var value = rule.value.ToString();
                if (!value.StartsWith(this.likeToken.ToString()))
                {
                    rule.value = this.likeToken + value;
                }
            }
            if (op == "like" || op == "startwith")
            {
                var value = rule.value.ToString();
                if (!value.EndsWith(this.likeToken.ToString()))
                {
                    rule.value = value + this.likeToken;
                }
            }
            if (op == "in" || op == "notin")
            {
                var values = rule.value.ToString().Split(',');
                var appended = false;
                bulider.Append("(");
                foreach (var value in values)
                {
                    if (appended) bulider.Append(",");

                    bulider.Append(paramPrefixToken + CreateFilterParam(value, rule.type,ref filterParams)); 

                    appended = true;
                }
                bulider.Append(")");
            } 
            //is null 和 is not null 不需要值
            else if (op != "isnull" && op != "isnotnull")
            {
                bulider.Append(paramPrefixToken + CreateFilterParam(rule.value, rule.type,ref filterParams));

            } 
            return bulider.ToString();
        }

        private string CreateFilterParam(object value, string type,ref List<FilterParam> filterParams)
        {
            string paramName = "p" + ++paramCounter;
            object val = value;
            if (type.ToString().Equals("int") || type.ToString().Equals("digits"))
                val = Convert.ToInt32(val);
            else if (type.ToString().Equals("float") || type.ToString().Equals("number"))
                val = Convert.ToDecimal(val);
            FilterParam param = new FilterParam(paramName, val);
            filterParams.Add(param);
            return paramName;
        }


        public override string ToString()
        {
            StringBuilder bulider = new StringBuilder();
            bulider.Append("CommandText:");
            bulider.Append(this.CommandText);
            bulider.AppendLine();
            bulider.AppendLine("Parms:");
            foreach (var parm in this.Parms)
            {
                bulider.AppendLine(string.Format("{0}:{1}", parm.Name, parm.Value));
            }
            return bulider.ToString();
        }

        #region 公共工具方法
        /// <summary>
        /// 获取操作符的SQL Text
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns> 
        public static string GetOperatorQueryText(string op)
        {
            switch (op.ToLower())
            {
                case "add":
                    return " + ";
                case "bitwiseand":
                    return " & ";
                case "bitwisenot":
                    return " ~ ";
                case "bitwiseor":
                    return " | ";
                case "bitwisexor":
                    return " ^ ";
                case "divide":
                    return " / ";
                case "equal":
                    return " = ";
                case "greater":
                    return " > ";
                case "greaterorequal":
                    return " >= ";
                case "isnull":
                    return " is null ";
                case "isnotnull":
                    return " is not null ";
                case "less":
                    return " < ";
                case "lessorequal":
                    return " <= ";
                case "like":
                    return " like ";
                case "startwith":
                    return " like ";
                case "endwith":
                    return " like ";
                case "modulo":
                    return " % ";
                case "multiply":
                    return " * ";
                case "notequal":
                    return " <> ";
                case "subtract":
                    return " - ";
                case "and":
                    return " and ";
                case "or":
                    return " or ";
                case "in":
                    return " in ";
                case "notin":
                    return " not in ";
                default:
                    return " = ";
            }
        }
        #endregion

    }
}
