using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;
using eContract.DDP.Common.CommonJob;

namespace eContract.DDP.Common.Command
{
    public class BaseCommand:ICommand
    {
        #region 成员变量

        /// <summary>
        /// 初始化时传入的参数
        /// </summary>
        public Hashtable InitialParameters = null;

        /// <summary>
        /// 命令节点
        /// </summary>
        public XmlNode CommandNode = null;

        /// <summary>
        /// 宿主任务代码，放在参数时传进来
        /// </summary>
        public string JobCode = "";

        public string ThreadName = "";

        #endregion

        #region ICommand Members

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="paramters">输入参数</param>
        /// <param name="node">命令节点</param>
        public virtual void Initialize(Hashtable paramters, XmlNode node)
        {
            this.InitialParameters = paramters;
            this.CommandNode = node;

            // 从参数中取出任务代码，用来写日志
            this.JobCode = paramters[DDPConst.C_JobCode].ToString();
            if (paramters.ContainsKey(DDPConst.C_ThreadName) == true)
                this.ThreadName = paramters[DDPConst.C_ThreadName].ToString();
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="paramters">参数，在各个命令中参数可能会增加</param>
        /// <param name="error">出错信息</param>
        /// <returns>是否成功</returns>
        public virtual ResultCode Execute(ref Hashtable paramters, out string error)
        {
            error = "";

            return ResultCode.Success;
        }



        #endregion

        #region 静态函数

        /// <summary>
        /// 替换参数
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="text"></param>
        public static string ReplaceParameters(Hashtable parameters, string text)
        {
            foreach (DictionaryEntry parameter in parameters)
            {
                if (parameter.Value != null && parameter.Value is string)
                    text = text.Replace(parameter.Key.ToString(), parameter.Value.ToString());
            }

            return text;
        }
        /// <summary>
        /// 生成where语句
        /// </summary>
        /// <param name="fieldNames"></param>
        /// <param name="dataIDList"></param>
        /// <param name="dataSplitOperator"></param>
        /// <returns></returns>
        public static string MakeDataIDWhereSql(string fieldNames, List<string> dataIDList, string dataSplitOperator)
        {
            string whereSql = "";

            // 配置的多个主键必须以逗号分隔
            string[] fieldList = fieldNames.Split(new char[] { ',' });


            // 多条数据以or拼起来
            for (int i = 0; i < dataIDList.Count; i++)
            {
                if (whereSql != "")
                    whereSql += " or ";

                // 多个主键以and拼起来
                string sql2 = "";
                string values = dataIDList[i];
                string[] valueList = new string[1];
                if (dataSplitOperator.Length > 0)
                    valueList = values.Split(new char[] { dataSplitOperator[0] });
                else
                    valueList[0] = values;
                for (int j = 0; j < valueList.Length; j++)
                {
                    if (sql2 != "")
                        sql2 += " and ";
                    sql2 += fieldList[j] + "='" + valueList[j] + "' ";
                }

                if (sql2 != "")
                    whereSql += "(" + sql2 + ")";
            }

            // 查询sql语句
            if (whereSql != "")
                whereSql = " where " + whereSql;

            return whereSql;
        }

        #endregion
    }
}
