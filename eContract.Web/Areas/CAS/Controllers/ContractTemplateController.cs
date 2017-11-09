using eContract.BusinessService.BusinessData.Service;
using eContract.Common;
using eContract.Common.Entity;
using eContract.Common.MVC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

namespace eContract.Web.Areas.CAS.Controllers
{
    public class ContractTemplateController : AuthBaseController
    {
        // GET: CAS/ContractTemplate
        public ActionResult Index(JqGrid grid, FormCollection data)
        {
            grid.ConvertParams(data);
            if (IsPost)
            {
                return Json(AjaxResult.Success(BusinessDataService.ContractTemplateService.ForGrid(grid)));
            }
            return View();
        }
        public ActionResult Edit(CasContractTemplateEntity entity, string id, string type)
        {
            if (type == "check")
            {
                ViewBag.Type = "3";
            }
            string strError = "";
            if (!IsPost)
            {
                entity = BusinessDataService.ContractTemplateService.CreateContractTemplateEntity("MDM");
                if (!string.IsNullOrEmpty(id))
                {

                    entity = BusinessDataService.ContractTemplateService.GetById<CasContractTemplateEntity>(id);
                    //编辑
                    ViewBag.EditType = "0";
                }
                else
                {
                    //新增
                    ViewBag.EditType = "1";
                }
            }
            else
            {
                if (BusinessDataService.ContractTemplateService.SaveContractTemplate(entity, ref strError))
                {
                   
                    return Json(AjaxResult.Success());
                }
                else {
                    strError = "Update failed";
                    return Json(AjaxResult.Success(strError));
                }
            }
            ViewBag.strError = strError;
            return View(entity);
        }
        public ActionResult Delet(CasContractTemplateEntity entity, string id)
        {
            var a = BusinessDataService.ContractTemplateService.DeletecontractTemplate(id);
            return Json(AjaxResult.Success());
        }
        /// <summary>
        /// 获取合同模板附件
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public JsonResult GetUploadFiles(string contractId)
        {
            var selectedValue = BusinessDataService.ContractTemplateService.GetUploadFiles(contractId);
            return Json(selectedValue, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DownloadContractTepPage(JqGrid grid, FormCollection data)
        {
            grid.ConvertParams(data);
            if (IsPost)
            {
                //关于下载页面的查询条件后台还没有做限制
                return Json(AjaxResult.Success(BusinessDataService.ContractTemplateService.DownloadContractTep(grid)));
            }
            return View();
        }

        /// <summary>
        /// 查看合同模板下载文件
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CheckDownloadFiles(JqGrid grid, FormCollection data, string id)
        {
            var entity = BusinessDataService.ContractTemplateService.GetById<CasContractTemplateEntity>(id);
            ViewBag.tempalteid = id;
            ViewBag.company = entity.Company;
            ViewBag.contractName = entity.ContractTemplateName;
            grid.ConvertParams(data);
            if (IsPost)
            {
                return Json(AjaxResult.Success(BusinessDataService.ContractTemplateService.GetContractAttachmentList(grid)));
            }
            return View();
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="id"></param>
        public ActionResult DownFiles(string attachmentid)
        {
            var entity = BusinessDataService.CommonHelperService.GetById<CasAttachmentEntity>(attachmentid);

            string fileName = entity.FileName;//客户端保存的文件名 
            string filePath = "";//服务器文件路径
            if (entity.FileSuffix == "pdf")
            {
                filePath = Server.MapPath(entity.PdfFilePath);
            }
            else
            {
                filePath = Server.MapPath(entity.FilePath);
            }

            FileInfo fileInfo = new FileInfo(filePath);
            if (fileInfo.Exists)//如果文件存在则下载
            {
                FileExists(fileInfo, fileName);
            }
            else
            {
                Response.Write("<script>alert('文件已过期或文件不存在');window.opener = null;window.open('', '_self');window.close();</script>");
            }
            return View();
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="fileName"></param>
        public void FileExists(FileInfo fileInfo, string fileName)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
            Response.AddHeader("Content-Length", fileInfo.Length.ToString());
            Response.AddHeader("Content-Transfer-Encoding", "binary");
            Response.ContentType = "application/octet-stream";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
            Response.WriteFile(fileInfo.FullName);
            Response.Flush();
            Response.End();
        }

        public ActionResult QueryAttachment(string attachmentid)
        {
            var entity = BusinessDataService.CommonHelperService.GetById<CasAttachmentEntity>(attachmentid);
            //ViewBag.filePath = entity.FilePath;
            return RedirectToAction("GetPreviewPdf", "CommonPDF", new { path = Session[entity.FilePath] });
        }
    }
}