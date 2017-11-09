using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

using System.Web;
using System.IO;


namespace eContract.Common
{
    public  class HtmlToExcel
    {
        private readonly string TEMPLATE_ROOT = HttpContext.Current.Server.MapPath("~/App_Data/");
        private readonly string TEMP_ROOT = HttpContext.Current.Server.MapPath("~/Temp/");



        public static string BaseUrl
        {
            get
            {
                string strBaseUrl = "";
                if (HttpContext.Current.Request.Url.Port.ToString() == "443")
                {
                    strBaseUrl += "https://" + HttpContext.Current.Request.Url.Host;
                }
                else
                {
                    strBaseUrl += "http://" + HttpContext.Current.Request.Url.Host;
                    if (HttpContext.Current.Request.Url.Port.ToString() != "80")
                    {
                        strBaseUrl += ":" + HttpContext.Current.Request.Url.Port;
                    }
                }
                strBaseUrl += HttpContext.Current.Request.ApplicationPath;
                if (HttpContext.Current.Request.ApplicationPath[HttpContext.Current.Request.ApplicationPath.Length - 1] != '/')
                {
                    strBaseUrl += "/";
                }
                return strBaseUrl;
            }
        }


        private string _template_file = string.Empty;
        /// <summary>
        /// xslt模板文件服务器端物理完全路径
        /// </summary>
        public string TemplateFile
        {
            get { return _template_file; }
        }

        public HtmlToExcel() { }

        public HtmlToExcel(string template_file)
        {
            string strTemplateFullPath = TEMPLATE_ROOT + template_file;
            if (System.IO.File.Exists(strTemplateFullPath))
            {
                this._template_file = strTemplateFullPath;
            }
            else
            {
                throw new Exception("Can not find the template file: \"" + strTemplateFullPath + "\"");
            }
        }

        public void ExportExcel(DataTable dt, string displayFileName)
        {
            try
            {
                string newExcelFile = TransformXMLDocmentToExcel(dt, this.TemplateFile);
                if (newExcelFile.Equals(string.Empty))
                    return;

                string newExcelZipFile = newExcelFile + ".zip";
                ZipClass.ZipFile(newExcelFile, newExcelZipFile, displayFileName);

                OutputFile(newExcelZipFile, displayFileName + ".zip");
                DeleteFile(newExcelFile);
                DeleteFile(newExcelZipFile);
            }
            catch
            {
            }
        }

        #region TransformXMLDocmentToExcel
        /// <summary>
        /// 把xmldocument根据xslt转换为excel文件
        /// </summary>
        /// <param name="dt">data source</param>
        /// <param name="xsltTemplateFile">xslt模板文件的服务器端物理全路径</param>
        /// <returns>返回生成的excel文件的服务器端物理全路径</returns>
        private string TransformXMLDocmentToExcel(DataTable dt, string xsltTemplateFile)
        {
            System.IO.StringReader strReader = null;
            XmlTextReader xmlReader = null;
            XPathDocument xpathDoc = null;
            System.IO.FileStream fs = null;
            XmlTextWriter xmlTextWriter = null;
            XslCompiledTransform xslTran = null;
            DataSet ds = new DataSet("NewDataSet");

            try
            {
                DataTable newDT = dt.Copy();
                newDT.TableName = "Table";
                ds.Tables.Add(newDT);
                strReader = new System.IO.StringReader(ds.GetXml());
                xmlReader = new XmlTextReader(strReader);
                xpathDoc = new XPathDocument(xmlReader);

                string newExcelFile = TEMP_ROOT + Guid.NewGuid().ToString() + ".xls";
                fs = new System.IO.FileStream(newExcelFile, System.IO.FileMode.Create);

                // Create an XmlTextWriter for the FileStream
                xmlTextWriter = new XmlTextWriter(fs, Encoding.Unicode);

                // Transform the XML using the stylesheet
                xslTran = new XslCompiledTransform();
                xslTran.Load(xsltTemplateFile);
                xslTran.Transform(xpathDoc, xmlTextWriter);

                return newExcelFile;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
            finally
            {
                if (xmlTextWriter != null)
                    xmlTextWriter.Close();

                if (strReader != null)
                    strReader.Close();

                if (xmlReader != null)
                    xmlReader.Close();

                if (fs != null)
                    fs.Close();

                if (xmlTextWriter != null)
                    xmlTextWriter.Close();

                strReader = null;
                xmlReader = null;
                xpathDoc = null;
                fs = null;
                xmlTextWriter = null;
                xslTran = null;
            }
        }
        #endregion

        #region OutputFile
        /// <summary>
        /// 向浏览器输出文件，提供文件下载
        /// </summary>
        /// <param name="fileName">文件在服务器端的路径</param>
        /// <param name="displayFileName">提供文件下载时显示的文件名</param>
        public static void OutputFile(string fileName, string displayFileName)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(fileName);
            if (fileInfo.Exists)
            {
                const long ChunkSize = 102400; // 100k/times
                byte[] buffer = new byte[ChunkSize];
                HttpContext.Current.Response.Clear();
                System.IO.FileStream iStream = System.IO.File.OpenRead(fileName);
                long dataLengthToRead = iStream.Length; // total size
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(displayFileName));
                while (dataLengthToRead > 0 && HttpContext.Current.Response.IsClientConnected)
                {
                    int lengthRead = iStream.Read(buffer, 0, Convert.ToInt32(ChunkSize)); // read
                    HttpContext.Current.Response.OutputStream.Write(buffer, 0, lengthRead);
                    HttpContext.Current.Response.Flush();
                    dataLengthToRead = dataLengthToRead - lengthRead;
                }
                iStream.Close();
                HttpContext.Current.Response.Close();
            }
        }
        #endregion

        #region DeleteFile
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName">文件在服务器端的路径</param>
        private void DeleteFile(string fileName)
        {
            try
            {
                System.IO.File.Delete(fileName);
            }
            catch
            {
            }
        }
        #endregion

        #region ExportExcel
        /// <summary>
        /// 根据表结构和列标是集合动态生成xslt模板，再根据表数据生成excel文件。
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="headers">列标题</param>
        /// <param name="displayFileName">显示给客户端用户的文件名</param>
        public void ExportExcel(DataTable dt, List<Column> columns, string displayFileName)
        {
            try
            {
             
                string xslt_filename = CreateXsltTempFile(columns);
                if (xslt_filename.Equals(string.Empty))
                    return;
                string newExcelFile = TransformXMLDocmentToExcel(dt, xslt_filename);
                if (newExcelFile.Equals(string.Empty))
                    return;

                //string newExcelZipFile = newExcelFile + ".zip";
                //ZipClass.ZipFile(newExcelFile, newExcelZipFile, displayFileName);





                OutputFile(newExcelFile, displayFileName);
                DeleteFile(newExcelFile);
                DeleteFile(xslt_filename);
                //DeleteFile(newExcelZipFile);
            }
            catch { }
        }

        public Hashtable ExportExcels(DataTable dt, List<Column> columns, string displayFileName)
        {
            try
            {
                FileClear();
                string xslt_filename = CreateXsltTempFile(columns);
                if (xslt_filename.Equals(string.Empty))
                    return null;
                string newExcelFile = TransformXMLDocmentToExcel(dt, xslt_filename);
                if (newExcelFile.Equals(string.Empty))
                    return null;

                string newExcelZipFile = newExcelFile + ".zip";
                ZipClass.ZipFile(newExcelFile, newExcelZipFile, displayFileName);


                Hashtable ht = new Hashtable();
                ht.Add("newExcelZipFile", newExcelFile);
                ht.Add("BaseUrl", BaseUrl);
                ht.Add("displayFileName", displayFileName);
               
               
                OutputFile(newExcelZipFile, displayFileName + ".zip");
                DeleteFile(newExcelFile);
                DeleteFile(xslt_filename);
                DeleteFile(newExcelZipFile);
                return ht;
            }
            catch { return null; }
        }


      



        #endregion

        #region CreateXsltTempFile
        /// <summary>
        /// 根据表格结构和列标题集合，动态生成xslt文件。
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        private string CreateXsltTempFile(List<Column> columns)
        {
            string xslt_filename = TEMP_ROOT + Guid.NewGuid().ToString() + ".xslt";
            try
            {
                System.IO.StreamWriter sw = System.IO.File.CreateText(xslt_filename);
                sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                sw.WriteLine("<xsl:stylesheet version=\"1.0\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\" xmlns:ms=\"urn:schemas-microsoft-com:xslt\">");
                sw.WriteLine("  <xsl:template match=\"/\">");
                sw.WriteLine("    <html xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\" xmlns=\"http://www.w3.org/TR/REC-html40\">");
                sw.WriteLine("      <head>");
                sw.WriteLine("        <meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\" />");
                sw.WriteLine("        <style>");
                sw.WriteLine("          tr.Header {padding-top:1px; padding-right:1px; padding-left:1px; color:black; font-size:11.0pt; font-weight:400; font-style:normal; text-decoration:none; font-family:宋体; text-align:general; vertical-align:middle; white-space:nowrap;}");
                sw.WriteLine("          tr.Header td {padding-top:1px; padding-right:4px; padding-left:4px; color:windowtext; font-size:12.0pt; font-weight:700; font-style:normal; text-decoration:none; font-family:Arial, sans-serif; text-align:general; vertical-align:middle; border:.5pt solid windowtext; background:#00CCFF; mso-pattern:black none; white-space:normal;}");
                sw.WriteLine("          tr.Item td {padding-top:1px; padding-right:1px; padding-left:1px; color:black; font-size:11.0pt; font-weight:400; font-style:normal; text-decoration:none; font-family:宋体; text-align:general; vertical-align:middle; border:.5pt solid windowtext; border-top:none; }");
                sw.WriteLine("        </style>");
                sw.WriteLine("      </head>");
                sw.WriteLine("      <body>");
                sw.WriteLine("        <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"table-layout: fixed;\">");
                sw.WriteLine("          <tr class=\"Header\" height=\"40\">");
                //sw.WriteLine("            <td>Agency</td>");
                //sw.WriteLine("            <td style=\"border-left: none;\">Sequence</td>");
                //sw.WriteLine("            <td style=\"border-left: none;\">Action No.</td>");
                //sw.WriteLine("            <td style=\"border-left: none;\">Province</td>");

                int i_header = 0;
                if (columns.Count > 1)
                {
                    sw.WriteLine("            <td>" + columns[0].Caption + "</td>");
                    i_header++;
                }
                for (int i = i_header; i < columns.Count; i++)
                {
                    sw.WriteLine("            <td style=\"border-left: none;\">" + columns[i].Caption + "</td>");
                }

                sw.WriteLine("          </tr>");
                sw.WriteLine("          <xsl:for-each select=\"NewDataSet/Table\">");
                sw.WriteLine("          <tr class=\"Item\">");
                //sw.WriteLine("              <td>");
                //sw.WriteLine("                <xsl:value-of select=\"Agency\"/>");
                //sw.WriteLine("              </td>");
                //sw.WriteLine("              <td style=\"border-left: none\">");
                //sw.WriteLine("                <xsl:value-of select=\"Agency\"/>");
                //sw.WriteLine("              </td>");
                //sw.WriteLine("              <td style=\"border-left: none\">");
                //sw.WriteLine("                <xsl:value-of select=\"ms:format-date(Conviction_Report_Upload_Date, 'yyyy-MM-dd')\"/>");
                //sw.WriteLine("              </td>");

                int i_item = 0;
                if (columns.Count > 1)
                {
                    switch (columns[0].Type)
                    {
                        case ColumnType.Date:
                        //sw.WriteLine("            <td><xsl:value-of select=\"ms:format-date(" + columns[0].Name + ", 'yyyy-MM-dd')\"/></td>");
                        //break;
                        case ColumnType.Currency:
                        //sw.WriteLine("            <td><xsl:value-of select=\"format-number(" + columns[0].Name + ",'#.00')\"/></td>");
                        //break;
                        case ColumnType.Text:
                        default:
                            sw.WriteLine("            <td><xsl:value-of select=\"" + columns[0].Name + "\"/></td>");
                            break;
                    }
                    i_item++;
                }
                for (int i = i_item; i < columns.Count; i++)
                {
                    switch (columns[i].Type)
                    {
                        case ColumnType.Date:
                        //sw.WriteLine("            <td style=\"border-left: none;\"><xsl:value-of select=\"ms:format-date(" + columns[i].Name + ", 'yyyy-MM-dd')\"/></td>");
                        //break;
                        case ColumnType.Currency:
                        //sw.WriteLine("            <td style=\"border-left: none;\"><xsl:value-of select=\"ms:format-number(" + columns[i].Name + ",'#.00')\"/></td>");
                        //break;
                        case ColumnType.Text:
                        default:
                            sw.WriteLine("            <td style=\"border-left: none;\"><xsl:value-of select=\"" + columns[i].Name + "\"/></td>");
                            break;
                    }
                }

                sw.WriteLine("          </tr>");
                sw.WriteLine("          </xsl:for-each>");
                sw.WriteLine("        </table>");
                sw.WriteLine("      </body>");
                sw.WriteLine("    </html>");
                sw.WriteLine("  </xsl:template>");
                sw.WriteLine("</xsl:stylesheet>");
                sw.Close();

                return xslt_filename;
            }
            catch
            {
                return string.Empty;
            }

        }
        #endregion

        private void FileClear()
        {
            // 删除过期文件,约定为当前天以前的所有文件
            if (Directory.Exists(TEMP_ROOT))
            {
                string[] files = Directory.GetFiles(TEMP_ROOT);

                foreach (string filename in files)
                {
                    TimeSpan ts = DateTime.Now - File.GetCreationTime(filename);
                    if (ts.Days >= 1)
                    {
                        DeleteFile(filename);
                    }
                }
            }
        }

        #region Column
        /// <summary>
        /// the schema of column,
        /// include the name of datacolumn, and caption of datacolumn
        /// </summary>
        public struct Column
        {
            public string Name;
            public string Caption;
            public ColumnType Type;

            public Column(string column_name, string column_caption, ColumnType column_type)
            {
                this.Name = column_name;
                this.Caption = column_caption;
                this.Type = column_type;
            }

        }

        public enum ColumnType
        {
            Text, Date, Currency
        }
        #endregion
    }
}
