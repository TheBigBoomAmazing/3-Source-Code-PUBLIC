using System;
using System.Data;
using System.Reflection;
using System.IO;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace eContract.DDP.Common
{
    public class XmlUtil
    {
        #region Xml相关函数

        /// <summary>
        /// 得到XmlNode节点的指定属性的值
        /// </summary>
        /// <param name="node">XmlNode节点</param>
        /// <param name="strAttrName">属性名称</param>
        /// <returns>返回属性值，注：如何未找到指定的属性节点，返回""</returns>
        public static string GetAttrValue(XmlNode node,
            string strAttrName)
        {
            if (node == null)
                return "";
            if (strAttrName == null || strAttrName == "")
                return "";

            XmlAttribute attr = node.Attributes[strAttrName];
            if (attr != null)
                return attr.Value;

            return "";
        }

        /// <summary>
        /// 获得缩进的XML源代码
        /// </summary>
        /// <param name="dom"></param>
        /// <returns></returns>
        public static string GetIndentXml(XmlDocument dom)
        {
            // 
            MemoryStream m = new MemoryStream();

            XmlTextWriter w = new XmlTextWriter(m, Encoding.UTF8);
            w.Formatting = Formatting.Indented;
            w.Indentation = 4;
           
            dom.Save(w);
            w.Flush();


            m.Seek(0, SeekOrigin.Begin);

            StreamReader sr = new StreamReader(m, Encoding.UTF8);
            string strText = sr.ReadToEnd();
            sr.Close();

            w.Close();

            return strText;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static string GetIndentXml(XmlNode node)
        {
            // 
            MemoryStream m = new MemoryStream();

            XmlTextWriter w = new XmlTextWriter(m, Encoding.UTF8);
            w.Formatting = Formatting.Indented;
            w.Indentation = 4;
            node.WriteTo(w);
            w.Flush();                       

            m.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(m, Encoding.UTF8);
            string strText = sr.ReadToEnd();
            sr.Close();               
            w.Close();

            return strText;
        }

        // 得到node节点的第一个文本节点的值,相当于GetNodeFirstText()
        // parameter:
        //		node    XmlNode节点
        // result:
        //		node的第一个文本节点的字符串，不去空白
        //      注：如果node下级不存在文本节点，返回"";
        // 编写者：任延华
        public static string GetNodeText(XmlNode node)
        {
            Debug.Assert(node != null, "GetNodeText()调用出错，node参数值不能为null。");

            XmlNode nodeText = node.SelectSingleNode("text()");
            if (nodeText == null)
                return "";
            else
                return nodeText.Value;
        }

        // 设置XmlNode节点指定属性的值
        // parameters:
        //      node            XmlNode节点
        //      strAttrName     属性名称
        //      strAttrValue    属性值,可以为""或null,如果==null,表示删除这个属性
        // return:
        //      void
        // 编写者：任延华
        // ???找属性使用的属性集合的方式，与SelectctSingleNode()函数比较使用时间，哪个快。
        public static void SetAttr(XmlNode node,
            string strAttrName,
            string strAttrValue)
        {
            Debug.Assert(node != null, "SetAttr()调用错误，node参数值不能为null。");
            Debug.Assert(strAttrName != null && strAttrName != "", "SetAttr()调用错误，strAttrName参数值不能为null或空字符串。");

            XmlAttributeCollection listAttr = node.Attributes;
            XmlAttribute attrFound = listAttr[strAttrName];

            if (attrFound == null)
            {
                if (strAttrValue == null)
                    return;	// 本来就不存在

                XmlElement element = (XmlElement)node;
                element.SetAttribute(strAttrName, strAttrValue);
            }
            else
            {
                if (strAttrValue == null)
                    node.Attributes.Remove(attrFound);
                else
                    attrFound.Value = strAttrValue;
            }
        }

        #endregion
    }
}
