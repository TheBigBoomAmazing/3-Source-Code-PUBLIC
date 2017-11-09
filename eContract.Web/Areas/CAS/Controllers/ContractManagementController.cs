using eContract.BusinessService.BusinessData.Service;
using eContract.Common;
using eContract.Common.Entity;
using eContract.Common.MVC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NPOI;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using eContract.Common.WebUtils;

namespace eContract.Web.Areas.CAS.Controllers
{
    public class ContractManagementController : AuthBaseController
    {
        // GET: CAS/ContractManagement
        public ActionResult Index(JqGrid grid, FormCollection data)
        {
            grid.ConvertParams(data);
            grid.QueryField.Remove("ExportTypeData");//查询的时候不要这个键值对
            if (IsPost)
            {
                return Json(AjaxResult.Success(BusinessDataService.ContractManagementService.ForGrid(grid)));
            }
            return View();
        }
        public ActionResult ExportFileList()
        {
            CasContractEntity entity = new CasContractEntity();
            entity.ExportTypeData = Request["ExportTypeData"];//导出的时候区分网页用
            entity.ContractName = Request["CONTRACT_NAME"];
            entity.ContractNo = Request["CONTRACT_NO"];
            entity.CounterpartyCn = Request["OUNTERPARTY_CN"];
            entity.FerreroEntity = Request["FERRERO_ENTITY"];
            entity.ContractInitiator = Request["CONTRACT_INITIATOR"];
            entity.PO = Request["PO"];
            entity.PR = Request["PR"];
            entity.EffectiveDate = Request["EFFECTIVE_DATE"]!=""?Convert.ToDateTime(Request["EFFECTIVE_DATE"]): DateTime.Parse("1970-01-01 00:00:00");
            entity.ExpirationDate = Request["EXPIRATION_DATE"]!=""? Convert.ToDateTime(Request["EXPIRATION_DATE"]): DateTime.Parse("1970-01-01 00:00:00"); 
            entity.Status = Request["STATUS"]!=""?Convert.ToInt32(Request["STATUS"]):0;
            entity.ContractGroup = Request["CONTRACT_GROUP"]!=""? Convert.ToInt32(Request["CONTRACT_GROUP"]):0;
            DataTable contractList = BusinessDataService.ContractManagementService.ExportData(entity);

            DataTable dt = contractList;//获取需要导出的datatable数据  
            //创建Excel文件的对象  
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet  
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            //给sheet1添加第一行的头部标题  
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            //row1.RowStyle.FillBackgroundColor = "";  
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                //if (dt.Columns[i].DataType == typeof(Boolean))
                //{
                //    dt.Columns[i].DataType = typeof(String);
                //}
                row1.CreateCell(i).SetCellValue(dt.Columns[i].ColumnName);
                row1.Cells[i].CellStyle.Alignment = HorizontalAlignment.CENTER;
            }
            //将数据逐步写入sheet1各个行  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    //if (dt.Rows[i][j].ToString().Trim()=="True")
                    //{
                    //    dt.Rows[i][j] = "是";
                    //}
                    //if (dt.Rows[i][j].ToString().Trim() == "False")
                    //{
                    //    dt.Rows[i][j] = "否";
                    //}
                    rowtemp.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString().Trim());
                }
            }
            string strdate = DateTime.Now.ToString("yyyyMMddhhmmss");//获取当前时间  
            // 写入到客户端   
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", strdate + "Excel.xls");
        }


        /// <summary>
        /// 查看合同的审批列表
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="data"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ContractApprovalForm(JqGrid grid, FormCollection data,string id ,string conid)
        {
            var entity = BusinessDataService.ContractManagementService.GetById<CasContractEntity>(id);
            ViewData["CONTRACT_ID"] = entity.ContractId;
            ViewData["FERRERO_ENTITY"] = entity.FerreroEntity;
            ViewData["CONTRACT_GROUP"] = entity.ContractGroup;
            ViewData["CONTRACT_NAME"] = entity.ContractName;
            ViewData["CONTRACT_INITIATOR"] = entity.ContractInitiator;
            ViewData["CONTRACT_OWNER"] = !string.IsNullOrWhiteSpace(entity.ContractOwner.ToString())? entity.ContractOwner: entity.TemplateOwner;
            ViewData["COUNTERPARTY_CN"] = entity.CounterpartyCn;
            ViewData["CONTRACT_PRICE"] = entity.ContractPrice;
            ViewData["EFFECTIVE_DATE"] = entity.EffectiveDate;
            ViewData["EXPIRATION_DATE"] = entity.ExpirationDate;
            ViewData["IsTemplateContract"] = entity.IsTemplateContract;
            ViewData["Status"] = entity.Status;
            grid.ConvertParams(data);
            if (IsPost)
            {
                grid.keyWord = conid;
                return Json(AjaxResult.Success(BusinessDataService.ContractManagementService.GetContractApprolvalRes(grid)));
            }
            return View();
        }

        public ActionResult ContractAttachmentFiles(JqGrid grid, FormCollection data, string id)
        {
            var entity = BusinessDataService.ContractManagementService.GetById<CasContractEntity>(id);
            ViewBag.tempalteid = id;
            ViewBag.contractName = !string.IsNullOrWhiteSpace(entity.ContractName)? entity.ContractName: entity.TemplateName;
            if (IsPost)
            {
                //grid.keyWord = id;
                grid.ConvertParams(data);
                return Json(AjaxResult.Success(BusinessDataService.ContractManagementService.GetContractAttachment(grid)));
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
            if (entity.FileSuffix== "pdf")
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

        /// <summary>
        /// 我审批的合同
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult ContractApprovalByMe(JqGrid grid, FormCollection data)
        {
            grid.ConvertParams(data);
            grid.QueryField.Remove("ExportTypeData");//查询的时候不要这个键值对
            if (IsPost)
            {
                return Json(AjaxResult.Success(BusinessDataService.ContractManagementService.ContractApprovalByMe(grid)));
            }
            return View();
        }

        /// <summary>
        /// 我的部门合同
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult MyDepartmentContract(JqGrid grid, FormCollection data)
        {
            grid.ConvertParams(data);
            grid.QueryField.Remove("ExportTypeData");//查询的时候不要这个键值对
            if (IsPost)
            {
                return Json(AjaxResult.Success(BusinessDataService.ContractManagementService.MyDepContract(grid)));
            }
            return View();
        }

        /// <summary>
        /// 我支持的合同
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult ISupportedContract(JqGrid grid, FormCollection data)
        {
            grid.ConvertParams(data);
            grid.QueryField.Remove("ExportTypeData");//查询的时候不要这个键值对
            if (IsPost)
            {
                return Json(AjaxResult.Success(BusinessDataService.ContractManagementService.ISupportedContract(grid)));
            }
            return View();
        }
        /// <summary>
        /// 全部合同（FTS）
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult AllContractFTS(JqGrid grid, FormCollection data)
        {
            grid.ConvertParams(data);
            grid.QueryField.Remove("ExportTypeData");//查询的时候不要这个键值对
            if (IsPost)
            {
                return Json(AjaxResult.Success(BusinessDataService.ContractManagementService.AllContractFTS(grid)));
            }
            ViewBag.IsAdmin = WebCaching.IsAdmin;//超级管理员的地方需要具体问下的
            return View();
        }
        /// <summary>
        /// 全部合同（FFH）
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult AllContractFFH(JqGrid grid, FormCollection data)
        {
            grid.ConvertParams(data);
            grid.QueryField.Remove("ExportTypeData");//查询的时候不要这个键值对
            if (IsPost)
            {
                return Json(AjaxResult.Success(BusinessDataService.ContractManagementService.AllContractFFH(grid)));
            }
            ViewBag.IsAdmin = WebCaching.IsAdmin;//超级管理员的地方需要具体问下的
            return View();
        }

        public ActionResult CloseContractByAdminFFH(CasContractEntity entity, string id)
        {
            var a = BusinessDataService.ContractManagementService.CloseContractByAdmin(id);
            return Json(AjaxResult.Success());
        }
        public ActionResult CloseContractByAdminFTS(CasContractEntity entity, string id)
        {
            var a = BusinessDataService.ContractManagementService.CloseContractByAdmin(id);
            return Json(AjaxResult.Success());
        }
    }
}