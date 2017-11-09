using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace eContract.DDP.Common
{
    public class ConnectionEntity
    {


        public string Type = "";
        public string Server = "";
        public string UserID = "";
        public string Password = "";
        public string Database = ""; // 当ms sql server时有意义

        /// <summary>
        /// 构造函数
        /// </summary>
        public ConnectionEntity()
        { }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="node"></param>
        public ConnectionEntity(XmlNode node)
        {
            this.Type = XmlUtil.GetAttrValue(node, "Type");
            this.Server = XmlUtil.GetAttrValue(node, "Server");
            this.UserID = XmlUtil.GetAttrValue(node, "UserID");
            this.Password = XmlUtil.GetAttrValue(node, "Password");
            this.Database = XmlUtil.GetAttrValue(node, "Database");
        }

        /*
                <SouceConnection Type="oracle" Server="cms" UserID="cms2" Password="cmseContract" />
                <TargetConnection Type="oracle" Server="oracle9i" UserID="css" Password="css" />
         */
        /// <summary>
        /// 得到实体对应的xml
        /// </summary>
        /// <returns></returns>
        public string GetAttrsXml()
        {
            return "Type='" + this.Type + "'"
                + " Server='" + this.Server + "'"
                + " UserID='" + this.UserID + "'"
                + " Password='" + this.Password + "'"
                + " Database='" + this.Database + "'";
        }


    }
}
