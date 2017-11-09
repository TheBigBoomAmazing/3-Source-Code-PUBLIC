using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComixSDK.EDI.Utils;
using eContract.Common.WebUtils;
using eContract.Common;
using System.IO;

namespace eContract.Web.Areas.Admin.Controllers
{
    public class ExportExcelController : Controller
    {
        public ActionResult Index()
        {
            string cacheKey = WebCaching.UserId + "_" + ExcelHelper.EXPORT_EXCEL_CONTEXT;
            string strHtml = "";
            string fileName = "";
            string exportFilePath = Server.MapPath("~/DownLoad/ExportFiles/");
            if (!Directory.Exists(exportFilePath))
            {
                Directory.CreateDirectory(exportFilePath);
            }
            string exportFile = exportFilePath + WebCaching.UserAccount + "_Export_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            var obj = CacheHelper.Instance.Get(cacheKey);
            if (obj != null)
            {
                if (obj is List<ExcelConvertHelper.ExcelContext>)
                {
                    List<ExcelConvertHelper.ExcelContext> excelContexts = obj as List<ExcelConvertHelper.ExcelContext>;
                    ExcelHelper helper = new ExcelHelper();
                    helper.ExportExcel(excelContexts, exportFile);
                    fileName = excelContexts[0].FileName;
                }
                else
                {
                    ExcelConvertHelper.ExcelContext excelContext = obj as ExcelConvertHelper.ExcelContext;
                    ExcelHelper helper = new ExcelHelper();
                    helper.ExportExcel(excelContext, exportFile);
                    fileName = excelContext.FileName;
                }
                CacheHelper.Instance.Remove(cacheKey);
            }
            if (System.IO.File.Exists(exportFile))
            {
                byte[] fileContent = System.IO.File.ReadAllBytes(exportFile);
                System.IO.File.Delete(exportFile);
                Response.Clear();
                Response.ClearHeaders();
                Response.Buffer = false;
                Response.AppendHeader("Content-Type", "application/x-msdownload");
                Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                Response.ContentType = "application/octet-stream";
                Response.BinaryWrite(fileContent);
                Response.Flush();
                Response.End();
            }
            return View();
        }

        public ActionResult IndexOld()
        {
            string cacheKey = WebCaching.UserId + "_" + ExcelHelper.EXPORT_EXCEL_CONTEXT;
            string strHtml = "";
            string fileName = "";
            var obj = CacheHelper.Instance.Get(cacheKey);
            if (obj != null)
            {
                if (obj is List<ExcelConvertHelper.ExcelContext>)
                {

                    List<ExcelConvertHelper.ExcelContext> excelContexts = obj as List<ExcelConvertHelper.ExcelContext>;
                    ExcelHelper.Export(excelContexts, ref strHtml);
                    fileName = excelContexts[0].FileName;

                }
                else
                {
                    ExcelConvertHelper.ExcelContext excelContext = obj as ExcelConvertHelper.ExcelContext;
                    ExcelHelper.Export(excelContext, ref strHtml);
                    fileName = excelContext.FileName;
                }
                CacheHelper.Instance.Remove(cacheKey);
            }
            if (!string.IsNullOrEmpty(strHtml))
            {
                Response.Clear();
                Response.BufferOutput = false;
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                Response.ContentType = "application/ms-excel";
                Response.Write(strHtml);
                Response.Close();
            }
            return View();
        }
    }
}
