using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Data;
using eContract.Common.Schema;

namespace eContract.Common
{
    public class XmlHelper
    {
        /// <summary>
        /// 读取XML 文件
        /// </summary>
        public static DataSet ReadXml(string fileNam)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(fileNam);
            return ds;
        }

        /// <summary>
        /// 将字符串写入文件XML
        /// </summary>
        /// <param name="strinfo"></param>
        /// <param name="AddressXml"></param>
        public static void OutPutXML(string strinfo, string AddressXml)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(strinfo);
            FileStream fs = new FileStream(AddressXml, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding(("utf-8")));
            sw.Write(sb);
            sw.Flush();
            sw.Close();
        }

        /// <summary>
        /// 生成带有明细的XML文件
        /// </summary>
        public static void OutPutXML_Detail(DataTable dtMain, DataTable dtDetail, string AddressXml)
        {
            //dt.WriteXml(AddressXml);
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            sb.Append("<BCInvoice>");
            foreach (DataRow row in dtMain.Rows)
            {
                sb.Append("<Invoice ");

                for (int i = 0; i < dtMain.Columns.Count; i++)
                {
                    //sb.Append("<" + dt.Columns[i].ColumnName + ">" + row[i].ToString() + "</" + dt.Columns[i].ColumnName + ">");
                    if (dtMain.Columns[i].ColumnName != "ImpDataID")
                    {
                        sb.Append(dtMain.Columns[i].ColumnName + "=\"" + row[i].ToString() + "\" ");
                    }
                }
                sb.Append(">");
                for (int j = 0; j < dtDetail.Rows.Count; j++)
                {
                    if (dtDetail.Rows[j]["Invoice_Id"].ToString() == row["ImpDataID"].ToString())
                    {
                        sb.Append("<Products ");
                        for (int k = 0; k < dtDetail.Columns.Count; k++)
                        {
                            sb.Append(dtDetail.Columns[k].ColumnName + "=\"" + dtDetail.Rows[j][k].ToString() + "\" ");
                        }
                        sb.Append("></Products>");

                    }
                }
                sb.Append("</Invoice>");
            }
            sb.Append("</BCInvoice>");


            FileStream fs = new FileStream(AddressXml, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding(("utf-8")));
            sw.Write(sb);
            sw.Flush();
            sw.Close();
        }

        /// <summary>
        /// 生成不带明细的XML文件
        /// </summary>
        public static string GetOutPutXML(DataTable dtMain)
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            sb.Append("<Stores>");
            foreach (DataRow row in dtMain.Rows)
            {
                sb.Append("<Store>");
                for (int i = 0; i < dtMain.Columns.Count; i++)
                {

                    sb.Append("<" + dtMain.Columns[i].ColumnName + ">" + row[i].ToString() + "</" + dtMain.Columns[i].ColumnName + ">");
                }
                sb.Append("</Store>");
            }
            sb.Append("</Stores>");
            return sb.ToString();
        }


        public static void OutPutXML(DataTable dtMain, string AddressXml)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            sb.Append("<BCInvoice>");
            foreach (DataRow row in dtMain.Rows)
            {
                sb.Append("<Invoice ");

                for (int i = 0; i < dtMain.Columns.Count; i++)
                {
                    sb.Append(dtMain.Columns[i].ColumnName + "=\"" + row[i].ToString() + "\" ");
                }
                sb.Append(">");
                sb.Append("</Invoice>");
            }
            sb.Append("</BCInvoice>");
            FileStream fs = new FileStream(AddressXml, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding(("utf-8")));
            sw.Write(sb);
            sw.Flush();
            sw.Close();
        }
        /// <summary>
        /// 生成不带明细的CSV文件
        /// </summary>
        public static void OutPutCSV(DataTable dt, string Address)
        {
            FileStream fs = new FileStream(Address, FileMode.Create, FileAccess.Write);
            System.IO.StringWriter sw = new System.IO.StringWriter();
            int iColCount = dt.Columns.Count;
            for (int i = 0; i < iColCount; i++)
            {
                //sw.Write("\"" + dt.Columns[i] + "\"");
                //if (i < iColCount - 1)
                //{
                //    sw.Write(",");
                //}
                sw.Write(dt.Columns[i]);
                if (i < iColCount - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < iColCount; i++)
                {
                    //if (!Convert.IsDBNull(dr[i]))
                    //{
                    //    sw.Write("\"" + dr[i].ToString() + "\"");
                    //}
                    //else
                    //{
                    //    sw.Write("\"\"");
                    //}

                    //if (i < iColCount - 1)
                    //{
                    //    sw.Write(",");
                    //}
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        sw.Write(dr[i].ToString());
                    }
                    else
                    {
                        sw.Write("");
                    }

                    if (i < iColCount - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
            StreamWriter sw1 = new StreamWriter(fs, System.Text.Encoding.GetEncoding(("utf-8")));
            sw1.Write(sw);
            sw1.Flush();
            sw1.Close();
            //CommonMethod.OpenFile(Address);
        }

        /// <summary>
        /// 生成不带明细的TXT文件
        /// </summary>
        public static void OutPutTXT(DataTable dt, string Address)
        {
            FileStream fs = new FileStream(Address, FileMode.Create, FileAccess.Write);
            System.IO.StringWriter sw = new System.IO.StringWriter();
            int iColCount = dt.Columns.Count;
            for (int i = 0; i < iColCount; i++)
            {
                sw.Write("\"" + dt.Columns[i] + "\"");
                if (i < iColCount - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dt.Rows)
            {
                string aa = "";
                for (int i = 0; i < iColCount; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        //sw.Write("\"" + dr[i].ToString() + "\"");
                        //aa += "\"" + dr[i].ToString() + "\"";
                        aa += dr[i].ToString();
                    }
                    else
                    {
                        //sw.Write("\"\"");
                        //aa += "\"\"";
                        aa += "";
                    }

                    if (i < iColCount - 1)
                    {
                        //sw.Write(",");
                        aa += ",";
                    }
                }
                //sw.Write(sw.NewLine);
                sw.WriteLine(aa);
            }
            sw.Close();
            StreamWriter sw1 = new StreamWriter(fs, System.Text.Encoding.GetEncoding(("utf-8")));
            sw1.Write(sw);
            sw1.Flush();
            sw1.Close();
            //CommonMethod.OpenFile(Address);
        }

        /// <summary>
        /// 生成TXT文件
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="Address"></param>
        public static void OutPutTXT(string txt, string Address)
        {
            FileStream fs = new FileStream(Address, FileMode.Create, FileAccess.Write);
            System.IO.StringWriter sw = new System.IO.StringWriter();
            sw.Write(txt);
            sw.Close();
            StreamWriter sw1 = new StreamWriter(fs, System.Text.Encoding.GetEncoding(("utf-8")));
            sw1.Write(sw);
            sw1.Flush();
            sw1.Close();
        }
    }
}
