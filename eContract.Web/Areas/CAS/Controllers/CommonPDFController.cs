using eContract.BusinessService.BusinessData.Service;
using eContract.BusinessService.SystemManagement.Service;
using eContract.Common;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using eContract.Common.Entity;
using eContract.Common.WebUtils;
using eContract.BusinessService.SystemManagement.Domain;
using Suzsoft.Smart.Data;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace eContract.Web.Areas.CAS.Controllers
{
    public class CommonPDFController : AuthBaseController
    {
        // GET: CAS/CommonPDF
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetPreviewPdf(string attachPath)
        {
            string msg = "";
            bool flag = false;
            if (!string.IsNullOrEmpty(attachPath))
            {
                try
                {
                    string filePath = Server.MapPath(attachPath);
                    //string fileServerPath = System.Web.HttpContext.Current.Server.MapPath("/UpLoadFiles/PDF").Replace(@"\UpLoadFiles\PDF", "");
                    //string filePath = "D:\\WorkSpace\\SDM源码\\SDM\\SDM.Portal\\UpLoadFiles\\PDF\\InforWMS介绍.pdf";
                    // fileServerPath + AttachPath.Replace('/', '\\'); //源文件位置
                    string fileName = Path.GetFileName(filePath);

                    //var fileServerPathHtml = $"http://{System.Web.HttpContext.Current.Request.Url.Authority}/PDF/";
                    //string savePDFPath = fileServerPath + @"\FileFolder\PDF\";
                    //string s = savePDFPath + fileName.Substring(0, fileName.LastIndexOf(".")) + ".pdf";
                    if (System.IO.File.Exists(filePath))
                    {
                        flag = true;
                        msg = attachPath.Replace("~", "");
                        // $"/UpLoadFiles/PDF/{fileName.Substring(0, fileName.LastIndexOf(".", StringComparison.Ordinal))}.pdf";
                    }
                    else //不存在文件
                    {
                        msg = "该文件不存在";
                    }
                }
                catch (Exception e)
                {
                    msg = "出现异常，信息如下：" + "\n" + e.Message;
                }
            }
            if (flag)
            {
                return Json(AjaxResult.Success(msg), JsonRequestBehavior.AllowGet);
            }
            msg = "您查看的PDF文件正在转换，请稍后再试";
            return Json(AjaxResult.Error(msg), JsonRequestBehavior.AllowGet);
        }
    }
}