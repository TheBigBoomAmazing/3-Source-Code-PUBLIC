using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eContract.Common;
using eContract.BusinessService;
using eContract.Common.Entity;
using eContract.BusinessService.SystemManagement.Service;
using eContract.Common.WebUtils;
using System.Text;
using eContract.Common.MVC;
using Suzsoft.Smart.EntityCore;
using Suzsoft.Smart.Data;
using Newtonsoft.Json;

namespace eContract.Web.Areas.CAS.Controllers
{
    public class RegionController : AuthBaseController
    {
        public ActionResult Index(JqGrid grid, FormCollection data)
        {
            grid.ConvertParams(data);
            if (IsPost)
            {
                return Json(AjaxResult.Success(SystemService.RegionService.ForGrid(grid)));
            }
            return View();
        }

        public ActionResult Edit(CasRegionEntity entity, string id)
        {
            string strError = "";
            if (!IsPost)
            {
                entity = SystemService.RegionService.CreateRegionDomain().CasRegionEntity;
                if (!string.IsNullOrEmpty(id))
                {
                    entity = SystemService.RegionService.GetById<CasRegionEntity>(id);
                    ViewBag.EditType = "0";//编辑
                }
                else
                {
                    ViewBag.EditType = "1";//新增
                }
                //Dictionary<string, string> dicRegionManager = new Dictionary<string, string>();

                //List<CasUserEntity> listAllUsers = SystemService.RegionService.GetAllUsers();
                //if (listAllUsers.Count > 0)
                //{
                //    dicRegionManager = listAllUsers.ToDictionary(x => x.UserId, x => x.ChineseName);
                //}

                //ViewBag.RegionManagerDic = dicRegionManager;
            }
            else
            {
                var domian = SystemService.RegionService.CreateRegionDomain();
                domian.CasRegionEntity = entity;
                if (SystemService.RegionService.Save(domian, ref strError))
                {
                    return Json(AjaxResult.Success());
                }
                else
                {
                    return Json(AjaxResult.Error("Update failed"));
                }
            }
            ViewBag.strError = strError;
            return View(entity);
        }
        /// <summary>
        /// 得到区域总监的信息
        /// </summary>
        /// <param name="deptManagerId"></param>
        /// <returns></returns>
        public JsonResult loadRegionManager(string regionManagerId)
        {
            var selectedValue = SystemService.RegionService.GetRegionManager(regionManagerId);
            JsonSerializerSettings setting = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var ret = JsonConvert.SerializeObject(selectedValue, setting);
            return Json(ret, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(string deleteKeys)
        {
            string strError = "";
            if (SystemService.RegionService.DeleteRegionDomainByIds(deleteKeys, ref strError))
            {
                return Json(AjaxResult.Success());
            }
            return Json(AjaxResult.Error(strError));

        }
    }
}
