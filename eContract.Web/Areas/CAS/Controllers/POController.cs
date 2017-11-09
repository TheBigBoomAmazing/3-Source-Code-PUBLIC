using eContract.BusinessService.BusinessData.Service;
using eContract.Common;
using eContract.Common.MVC;
using System.Web.Mvc;

namespace eContract.Web.Areas.CAS.Controllers
{
    public class POController : AuthBaseController
    {
        public ActionResult PRIndex(JqGrid grid, FormCollection data)
        {
            grid.ConvertParams(data);
            if (IsPost)
            {
                return Json(AjaxResult.Success(BusinessDataService.POService.ForPRIndexGrid(grid)), JsonRequestBehavior.AllowGet);
            }
            return View();
        }

        public ActionResult PREdit(JqGrid grid, FormCollection data, string id)
        {
            ViewBag.ContractId = id;
            grid.ConvertParams(data);
            if (IsPost)
            {
                grid.keyWord = id;
                grid.QueryField.Remove("id");
                return Json(AjaxResult.Success(BusinessDataService.POService.ForPRGrid(grid)), JsonRequestBehavior.AllowGet);
            }
            return View();
        }

        public JsonResult PRDelete(string deleteKeys)
        {
            string strError = "";
            if (BusinessDataService.POService.DeletePRByIds(deleteKeys, ref strError))
            {
                return Json(AjaxResult.Success(), JsonRequestBehavior.AllowGet);
            }
            return Json(AjaxResult.Error(strError), JsonRequestBehavior.AllowGet);
        }

        public JsonResult PRSave(string contractFilingId, string contractId, string pRNo)
        {
            string strError = "";
            if (BusinessDataService.POService.SavePR(contractFilingId, contractId, pRNo, ref strError))
            {
                return Json(AjaxResult.Success(),JsonRequestBehavior.AllowGet);
            }
            return Json(AjaxResult.Error(strError), JsonRequestBehavior.AllowGet);
        }

        public JsonResult PRSubmit(string contractFilingId, string contractId, string pRNo)
        {
            string strError = "";
            if (BusinessDataService.POService.SubmitPR(contractFilingId, contractId, pRNo, ref strError))
            {
                return Json(AjaxResult.Success(), JsonRequestBehavior.AllowGet);
            }
            return Json(AjaxResult.Error(strError), JsonRequestBehavior.AllowGet);
        }

        public ActionResult POIndex(JqGrid grid, FormCollection data)
        {
            grid.ConvertParams(data);
            if (IsPost)
            {
                return Json(AjaxResult.Success(BusinessDataService.POService.ForPOIndexGrid(grid)), JsonRequestBehavior.AllowGet);
            }
            return View();
        }

        public ActionResult POEdit(JqGrid grid, FormCollection data, string id)
        {
            ViewBag.ContractId = id;
            grid.ConvertParams(data);
            if (IsPost)
            {
                grid.keyWord = id;
                grid.QueryField.Remove("id");
                return Json(AjaxResult.Success(BusinessDataService.POService.ForPOGrid(grid)), JsonRequestBehavior.AllowGet);
            }
            return View();
        }

        public JsonResult POSave(string contractFilingId, string pONo)
        {
            string strError = "";
            if (BusinessDataService.POService.POSave(contractFilingId, pONo, ref strError))
            {
                return Json(AjaxResult.Success(), JsonRequestBehavior.AllowGet);
            }
            return Json(AjaxResult.Error(strError), JsonRequestBehavior.AllowGet);
        }

        public JsonResult POAprove(string contractFilingId, string pONo)
        {
            string strError = "";
            if (BusinessDataService.POService.POAprove(contractFilingId, pONo, ref strError))
            {
                return Json(AjaxResult.Success(), JsonRequestBehavior.AllowGet);
            }
            return Json(AjaxResult.Error(strError), JsonRequestBehavior.AllowGet);
        }

        public JsonResult POReject(string contractFilingId, string remark)
        {
            string strError = "";
            if (BusinessDataService.POService.POReject(contractFilingId, remark, ref strError))
            {
                return Json(AjaxResult.Success(), JsonRequestBehavior.AllowGet);
            }
            return Json(AjaxResult.Error(strError), JsonRequestBehavior.AllowGet);
        }
    }
}