using System;
using System.Data;
using System.Configuration;
using System.Xml;
using System.Diagnostics;
using eContract.DDP.Common.CommonJob;

namespace eContract.DDP.Common
{
    /// <summary>
    /// Summary description for Job.
    /// </summary>
    public class JobEntity : IComparable
    {
        /// <summary>
        /// 基础信息
        /// </summary>
        private string _code;
        public string Code
        {
            get { return this._code; }
            set { this._code = value; }
        }

        private string _name;
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        private string _assemblyName;
        public string AssemblyName
        {
            get { return this._assemblyName; }
            set { this._assemblyName = value; }
        }

        private string _className;
        public string ClassName
        {
            get { return this._className; }
            set { this._className = value; }
        }

        private bool _active = false;
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        /// <summary>
        /// 任务节点
        /// </summary>
        public XmlNode JobNode = null;

        public string JobXml = "";


        public JobCfgManager Container = null;


        /// <summary>
        /// 构造函数
        /// </summary>
        public JobEntity()
        { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="jobFileName"></param>
        public JobEntity(JobCfgManager container, string jobXml)
        {
            this.Container = container;

            XmlDocument dom = new XmlDocument();
            dom.LoadXml(jobXml);
            this.JobXml = XmlUtil.GetIndentXml(dom.DocumentElement);//.OuterXml;

            this.JobNode = dom.DocumentElement;

            // 基础信息
            XmlNode nodeProperty = this.JobNode.SelectSingleNode("Property");
            Debug.Assert(nodeProperty != null, "配置文件不合法，不存在<Property>节点");

            this.Code = XmlUtil.GetAttrValue(nodeProperty, "Code").Trim();
            this.Name = XmlUtil.GetAttrValue(nodeProperty, "Name").Trim();
            this.AssemblyName = XmlUtil.GetAttrValue(nodeProperty, "AssemblyName").Trim();
            this.ClassName = XmlUtil.GetAttrValue(nodeProperty, "ClassName").Trim();
            string strActive = XmlUtil.GetAttrValue(nodeProperty, "Active").Trim();
            if (strActive.Length > 0)
                _active = bool.Parse(strActive);
        }




        #region IComparable Members

        public int CompareTo(object obj)
        {
            JobEntity ety2 = (JobEntity)obj;
            return this.Code.CompareTo(ety2.Code);
        }

        #endregion
    }
}
