using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eContract.Common;

namespace eContract.Web.Controllers
{

    public class UploadController : BaseController
    {
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult Welcome()
        {
            return View();
        }

        public void TopClient()
        {
            try
            {
                string filePath = Server.MapPath("\\DownLoad\\TopClient\\"+ ConfigurationManager.AppSettings["TopClientFullName"]);
                if (System.IO.File.Exists(filePath))
                {
                    FileInfo info = new FileInfo(filePath);
                    long fileSize = info.Length;
                    HttpContext.Response.Clear();

                    //指定Http Mime格式为压缩包
                    HttpContext.Response.ContentType = "application/x-zip-compressed";

                    // Http 协议中有专门的指令来告知浏览器, 本次响应的是一个需要下载的文件. 格式如下:
                    // Content-Disposition: attachment;filename=filename.txt
                    HttpContext.Response.AddHeader("Content-Disposition",
                        "attachment;filename=" + Server.UrlEncode(info.Name));
                    //不指明Content-Length用Flush的话不会显示下载进度   
                    HttpContext.Response.AddHeader("Content-Length", fileSize.ToString());
                    HttpContext.Response.TransmitFile(filePath, 0, fileSize);
                    HttpContext.Response.Flush();
                }
            }
            catch (Exception e)
            {
                // ignored
            }
            finally
            {
                HttpContext.Response.Close();
            }

        }


    }
}
