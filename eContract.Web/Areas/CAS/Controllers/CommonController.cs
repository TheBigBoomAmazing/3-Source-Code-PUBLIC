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
using System.Linq;
using System.Data;
using System.Text;

namespace eContract.Web.Areas.CAS.Controllers
{
    public class CommonController : Controller
    {
        public ActionResult UploadDemo()
        {
            return View();
        }

        public JsonResult UploadFiles()
        {
            try
            {
                string msg = string.Empty;
                HttpPostedFileBase fileObject = Request.Files["file"];
                if (fileObject == null)
                {
                    //http请求找不到文件
                    msg = "找不到上传的文件";
                    return Json(AjaxResult.Error(msg));
                }
                string filename = Path.GetFileName(fileObject.FileName);
                if (filename == null)
                {
                    //文件名为空
                    msg = "文件名为空";
                    return Json(AjaxResult.Error(msg));
                }
                string extension = Path.GetExtension(fileObject.FileName);
                if (extension == null)
                {
                    //文件扩展名不合法
                    msg = "文件扩展名不合法";
                    return Json(AjaxResult.Error(msg));
                }
                //if (".pdf.".IndexOf(extension + ".", StringComparison.Ordinal) < 0)
                //{
                //    //本次合同附件，格式只能为PDF格式,20171018
                //    msg = "合同附件格式只能为PDF格式";
                //    return Json(AjaxResult.Error(msg));
                //}
                string directory = "/UpLoadFiles/";

                filename = Request.Form["name"];
                return SaveFile(filename, directory, fileObject, ref msg);
            }
            catch (Exception e)
            {
                return Json(AjaxResult.Error(e.Message));
            }
        }

        private JsonResult SaveFile(string fileName, string directory, HttpPostedFileBase fileObject, ref string msg)
        {
            try
            {
                bool isPdf = true;
                string fileTxt = fileName.Substring(0, fileName.LastIndexOf(".", StringComparison.Ordinal));
                string extension = fileName.Substring(fileName.LastIndexOf(".", StringComparison.Ordinal)).Replace(".", "");
                string sourcePath = "";
                string docFilePath = "";
                string pdfFilePath = "";
                if (".doc.docx.".IndexOf("." + extension + ".", StringComparison.Ordinal) > -1)
                {
                    string docDirectory = directory + "DOC/";//DOC文件路径
                    if (!Directory.Exists(Server.MapPath(docDirectory)))
                    {
                        Directory.CreateDirectory(Server.MapPath(docDirectory));
                    }
                    docFilePath = docDirectory + fileName;
                    sourcePath = docFilePath;
                    isPdf = false;
                }
                else if (".pdf.".IndexOf("." + extension + ".", StringComparison.Ordinal) > -1)
                {
                    string pdfDirectory = directory + "PDF/";//PDF文件路径
                    if (!Directory.Exists(Server.MapPath(pdfDirectory)))
                    {
                        Directory.CreateDirectory(Server.MapPath(pdfDirectory));
                    }
                    pdfFilePath = pdfDirectory + fileName;
                    sourcePath = pdfFilePath;
                    isPdf = true;
                }
                else
                {
                    msg = "Only support PDF & Word file";
                    return Json(AjaxResult.Error(msg), JsonRequestBehavior.AllowGet);
                }
                //保存至磁盘
                fileObject.SaveAs(Server.MapPath(sourcePath));

                //保存至数据库
                UserDomain userDomain = WebCaching.CurrentUserDomain as UserDomain;
                CasAttachmentEntity casAttachmentEntity = new CasAttachmentEntity
                {
                    AttachmentId = Guid.NewGuid().ToString(),
                    CreatedBy = userDomain.CasUserEntity.UserId,
                    CreateTime = DateTime.Now,
                    AttachmentType = -1,
                    FileName = fileObject.FileName,
                    Converted = isPdf,
                    FilePath = docFilePath,
                    PdfFilePath = pdfFilePath,
                    FileSuffix = extension,
                    LastModifiedBy = userDomain.CasUserEntity.UserId,
                    LastModifiedTime = DateTime.Now,
                    SourceId = ""
                };
                bool flag = BusinessDataService.CommonHelperService.SaveFile(casAttachmentEntity);
                return Json(flag ? AjaxResult.Success(casAttachmentEntity.AttachmentId)
                    : AjaxResult.Error(msg), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                msg = e.Message;
                SystemService.LogErrorService.InsertLog(e);
                return Json(AjaxResult.Error(e.Message), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 多文件删除（单个删除）
        /// </summary>
        /// <param name="fileValue">fileId$fileName</param>
        /// <returns></returns>
        public JsonResult DeleteUploaderFile(string id)
        {
            CasAttachmentEntity casAttachmentEntity = new CasAttachmentEntity
            {
                AttachmentId = id
            };
            casAttachmentEntity = DataAccess.SelectSingle(casAttachmentEntity);

            string orginalFilePath = string.Empty;//原始文件路径

            if (casAttachmentEntity == null)
            {
                return Json(AjaxResult.Error("删除失败：数据库没有该文件"), JsonRequestBehavior.AllowGet);
            }
            bool flag = BusinessDataService.CommonHelperService.DeleteFile(id);
            if (flag)
            {
                //删除磁盘文件
                if (!string.IsNullOrEmpty(casAttachmentEntity.FilePath))
                {
                    DeleteFile(Server.MapPath(casAttachmentEntity.FilePath));
                }
                if (string.IsNullOrEmpty(casAttachmentEntity.PdfFilePath))
                {
                    DeleteFile(Server.MapPath(casAttachmentEntity.PdfFilePath));
                }
            }
            return Json(flag ? AjaxResult.Success() : AjaxResult.Error("删除失败"), JsonRequestBehavior.AllowGet);
        }

        private bool DeleteFile(string path)
        {
            try
            {
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                return true;
            }
            catch (Exception e)
            {
                SystemService.LogErrorService.InsertLog(e);
                return false;
            }
        }
        /// <summary>
        /// 获得合同类型列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetContractTypeList()
        {
            List<CasContractTypeEntity> list = BusinessDataService.CommonHelperService.SelectEnabledContractType();
            JsonSerializerSettings setting = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var retList=from ct in list select new { ContractTypeId = ct.ContractTypeId, ContractTypeName = ct.ContractTypeName, FerreroEntity = ct.FerreroEntity };
            var ret = JsonConvert.SerializeObject(retList, setting);
            return Json(ret, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 城市列表
        /// </summary>
        /// <param name="q"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult CityList(string q, string page)
        {
            var cityValue = BusinessDataService.CommonHelperService.GetAllCity();
            var lstRes = new List<City>();
            for (var i = 0; i < cityValue.Rows.Count; i++)
            {
                var oProvince = new City();
                oProvince.id = cityValue.Rows[i]["CITY_CODE"].ToString();
                oProvince.name = cityValue.Rows[i]["CITY_NAME"].ToString();
                lstRes.Add(oProvince);
            }
            if (q != null && q.Trim().Length > 0)
            {
                lstRes = (from r in lstRes where r.name.Contains(q) select r).ToList();
            }
            var lstCurPageRes = string.IsNullOrEmpty(page) ? lstRes.Take(10) : lstRes.Skip(Convert.ToInt32(page) * 10 - 10).Take(10);
            return Json(new { items = lstCurPageRes, total_count = lstRes.Count }, JsonRequestBehavior.AllowGet);
        }
        public string loadSelectRegionValue(string cityCode)
        {
            var usersql = new StringBuilder();
            usersql.AppendFormat("  SELECT REG.REGION_NAME FROM dbo.CAS_CITY CIT LEFT JOIN dbo.CAS_REGION REG ON CIT.REGION_ID = REG.REGION_ID WHERE CIT.CITY_CODE={0} ", Utils.ToSQLStr(cityCode).Trim());
            var regionValue = DataAccess.SelectScalar(usersql.ToString());
            return regionValue;
        }


        /// <summary>
        /// 加载选中的城市
        /// </summary>
        /// <param name="cityCode"></param>
        /// <returns></returns>
        public JsonResult loadSelectCityValue(string cityCode)
        {
            var selectedCity = GetSelectCity(cityCode);
            JsonSerializerSettings setting = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var ret = JsonConvert.SerializeObject(selectedCity, setting);
            return Json(ret, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 查询选中的城市
        /// </summary>
        /// <param name="cityCode"></param>
        /// <returns></returns>
        public string GetSelectCity(string cityCode)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("id", typeof(String));
            dataTable.Columns.Add("name", typeof(String));
            DataRow dr = dataTable.NewRow();
            var usersql = new StringBuilder();
            usersql.AppendFormat(" SELECT CITY_CODE AS id,CITY_NAME AS name FROM CAS_CITY WHERE CITY_CODE={0} ", Utils.ToSQLStr(cityCode).Trim());
            var valueString = DataAccess.SelectDataSet(usersql.ToString());
            if (valueString.Tables[0].Rows.Count == 0)
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                System.Collections.ArrayList dic1 = new System.Collections.ArrayList();
                return jss.Serialize(dic1);
            }
            else
            {
                dr["id"] = valueString.Tables[0].Rows[0]["id"];
                dr["name"] = valueString.Tables[0].Rows[0]["name"];
                dataTable.Rows.Add(dr);
                JavaScriptSerializer jss = new JavaScriptSerializer();
                System.Collections.ArrayList dic = new System.Collections.ArrayList();
                foreach (DataRow drV in dataTable.Rows)
                {
                    System.Collections.Generic.Dictionary<string, object> drow = new System.Collections.Generic.Dictionary<string, object>();
                    foreach (DataColumn dc in dataTable.Columns)
                    {
                        drow.Add(dc.ColumnName, drV[dc.ColumnName]);
                    }
                    dic.Add(drow);

                }
                //序列化  
                return jss.Serialize(dic);
            }

        }

        /// <summary>
        /// 固定合同名称列表
        /// </summary>
        /// <param name="q"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult ContractNameList(string q, string page)
        {
            string[] list = new string[] { "Advertisement Service Agreement", "Amendment", "Car Rental Service Agreement", "Construction Agreement", "Co-packing Agreement",
            "Creative Design Service Agreement", "Customs Service Contract", "Distribution Agreement", "Event Service Agreement", "Framework Agreement on Packaging Materials Procurement",
            "Framework Agreement on Raw Materials Procurement", "General Terms & Conditions for Seafreight", "Goods Purchase Agreement", "House Lease Agreement", "Market Research Service Agreement",
            "Merchandise Agreement", "Office Lease Agreement", "Packaging Design Service Agreement", "Recruitment Service Agreement", "Service Agreement",
            "Software Development Agreement", "Sub-Distribution Agreement", "Supplementary Agreement", "System Maintenance Agreement", "Transportation by Road Agreement",
            "Warehousing & Distribution Service Agreement", "Warehousing Service Agreement", "Waste Disposal Service Agreement"
            };
            List<string> contractNameList = new List<string>();
            contractNameList.AddRange(list);
            var lstRes = new List<ContractName>();
            for (var i = 0; i < contractNameList.Count; i++)
            {
                var cName = new ContractName();
                cName.id = contractNameList[i].ToString();
                cName.name = contractNameList[i].ToString();
                lstRes.Add(cName);
            }
            if (q != null && q.Trim().Length > 0)
            {
                lstRes = (from r in lstRes where r.name.Contains(q) select r).ToList();
            }
            var lstCurPageRes = string.IsNullOrEmpty(page) ? lstRes.Take(10) : lstRes.Skip(Convert.ToInt32(page) * 10 - 10).Take(10);
            return Json(new { items = lstCurPageRes, total_count = lstRes.Count }, JsonRequestBehavior.AllowGet);
        }

        public class ContractName
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class City
        {
            public string id { get; set; }
            public string name { get; set; }
        }
        #region 测试
        public JsonResult GetFilesDemo(string ids)
        {
            List<CasAttachmentEntity> list = BusinessDataService.CommonHelperService.GetFilesDemo(ids);
            JsonSerializerSettings setting = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var ret = JsonConvert.SerializeObject(list, setting);
            return Json(ret, JsonRequestBehavior.AllowGet);
        }
        #endregion


        public JsonResult TimeBack()
        {
            BusinessDataService.ContractApprovalService.TimeBack1Day();
            return Json(AjaxResult.Success());
        }

    }
}