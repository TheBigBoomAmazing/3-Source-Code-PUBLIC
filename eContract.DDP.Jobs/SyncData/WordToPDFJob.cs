using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eContract.DDP.Common.CommonJob;
using System.Collections;
using eContract.Log;
using System.IO;
using System.Reflection;
using System.Data;
using System.Net.Mail;
using System.Net;
using Suzsoft.Smart.Data;
using eContract.BusinessService.BusinessData.BusinessRule;
using Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices;

namespace eContract.DDP.Jobs
{
    //Word转PDF的Job
    public class WordToPDFJob : BaseJob
    {
        public override void Run(Hashtable parameters)
        {
            WordToPDFBLL toPDFBll = new WordToPDFBLL();
            System.Data.DataTable allWord = toPDFBll.GetAllNoConvertWord();
            foreach (DataRow item in allWord.Rows)
            {
                //string wordFilePath = "E:/fileTest/o_1bppr6m6cp0g1ipea53tbf11is8.docx";
                //string PDFFilePath = "E:/o_1bppr6m6cp0g1ipea53tbf11is8.pdf";
                string wordFilePath = item["FILE_PATH"].ToString();
                string PDFFilePath = wordFilePath.Replace("/DOC/", "/PDF/").Replace("docx","pdf").Replace("doc","pdf");
                bool isConvert = ConvertWord2Pdf(wordFilePath, PDFFilePath);
                if (isConvert)
                {
                    var strsql = new StringBuilder();
                    strsql.AppendFormat($@"  UPDATE CAS_ATTACHMENT SET CONVERTED = '1',PDF_FILE_PATH = '{PDFFilePath}' WHERE ATTACHMENT_ID = '{item["ATTACHMENT_ID"]}' ");
                    var val = DataAccess.ExecuteNoneQuery(strsql.ToString());
                }
            }
        }
        /// <summary>
        /// 将word转化成PDF
        /// </summary>
        /// <param name="sourcePath">word文件路径</param>
        /// <param name="targetPath"></param>
        /// <returns></returns>
        public static bool ConvertWord2Pdf(string sourcePath, string targetPath)
        {
            bool result;
            WdExportFormat exportFormat = WdExportFormat.wdExportFormatPDF;
            object paramMissing = Type.Missing;
            Application wordApplication = new Application();
            Document wordDocument = null;
            try
            {
                object paramSourceDocPath = "E:/eContract" + sourcePath;
                string paramExportFilePath = "E:/eContract" + targetPath;
                WdExportFormat paramExportFormat = exportFormat;
                WdExportOptimizeFor paramExportOptimizeFor =
                        WdExportOptimizeFor.wdExportOptimizeForPrint;
                WdExportRange paramExportRange = WdExportRange.wdExportAllDocument;
                int paramStartPage = 0;
                int paramEndPage = 0;
                WdExportItem paramExportItem = WdExportItem.wdExportDocumentContent;
                WdExportCreateBookmarks paramCreateBookmarks =
                        WdExportCreateBookmarks.wdExportCreateWordBookmarks;

                wordDocument = wordApplication.Documents.Open(
                        ref paramSourceDocPath, ref paramMissing, ref paramMissing,
                        ref paramMissing, ref paramMissing, ref paramMissing,
                        ref paramMissing, ref paramMissing, ref paramMissing,
                        ref paramMissing, ref paramMissing, ref paramMissing,
                        ref paramMissing, ref paramMissing, ref paramMissing,
                        ref paramMissing);
                if (wordDocument != null)
                    wordDocument.ExportAsFixedFormat(paramExportFilePath,
                            paramExportFormat, false,
                            paramExportOptimizeFor, paramExportRange, paramStartPage,
                            paramEndPage, paramExportItem, true,
                            true, paramCreateBookmarks, true,
                            true, false,
                            ref paramMissing);
                result = true;
            }
            catch (Exception ex)
            {
                //其他日志操作;
                return false;
            }
            finally
            {
                if (wordDocument != null)
                {
                    wordDocument.Close(ref paramMissing, ref paramMissing, ref paramMissing);
                    wordDocument = null;
                }
                if (wordApplication != null)
                {
                    wordApplication.Quit(ref paramMissing, ref paramMissing, ref paramMissing);
                    wordApplication = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            return result;
        }
    }
}
