using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using eContract.BusinessService.SystemManagement.Service;
using eContract.Common.Entity;
using eContract.Common.Schema;
using eContract.Web.Common;
using eContract.Common.MVC;
using eContract.BusinessService.BusinessData.Service;

namespace eContract.Web.Controllers
{
    public class HomeController : BaseController
    {
        public const string SYSTEM_NAME = "MDM";

        public ActionResult Index(JqGrid grid, FormCollection data)
        {
            //if (CurrentUser == null || CurrentUser.CasUserEntity == null || string.IsNullOrEmpty(CurrentUser.CasUserEntity.UserId))
            //{
            //    return RedirectToAction("Login", "Account");
            //}
            string strError = "";
            //string strResult = ComixSDK.EDI.Utils.CacheHelper.Instance.Get<string>("QQ_Notice_New");
            //if (string.IsNullOrWhiteSpace(strResult))
            //{

            //    QQExMailAPIHelper.SendRequstPost("http://news.gtimg.cn/notice_more.php?q=sz002301", new SortedDictionary<string, string>(), "utf-8", ref strResult);
            //    if (!string.IsNullOrWhiteSpace(strResult))
            //    {
            //        strResult = ComixSDK.EDI.Utils.JSONHelper.ToJson(ComixSDK.EDI.Utils.JSONHelper.FromJson<Dictionary<string, object>>(strResult.Replace("var finance_notice =", "").Replace("]};", "]}")));
            //        ComixSDK.EDI.Utils.CacheHelper.Instance.Set("QQ_Notice_New", strResult);
            //    }

            //}
            #region 页面首页带出详细数据
            List<LubrFinancialGoodsEntity> goodsList = BusinessDataService.LubrProductsShowBLLService.GetAllProducts();
            ViewBag.goods = goodsList;
            #endregion
            ViewBag.IsHome = true;
            //ViewBag.QQ_Notice_New = strResult;

            grid.ConvertParams(data);
            grid.QueryField.Remove("ExportTypeData");//查询的时候不要这个键值对
            if (IsPost)
            {
                grid.QueryField.Add("HomeQuery", "True");
                return Json(eContract.Common.AjaxResult.Success(BusinessDataService.ContractManagementService.ForGrid(grid)));
            }

            return View(this.CurrentUser);
        }

        /// <summary>
        /// 主要显示信息披露页面
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public JsonResult GetMsg(int page = 1)
        {
            string url = "http://www.cninfo.com.cn/cninfo-new/announcement/query";
            string paramData =
                $"stock=002301&searchkey=&category=&pageNum={page}&pageSize=30&column=szse_sme&tabName=fulltext&sortName=&sortType=&limit=&seDate=";
            string strResponse = HttpPost(url, paramData);
            var jsonObj = (JObject)JsonConvert.DeserializeObject(strResponse);
            int totalCount = int.Parse(jsonObj["totalRecordNum"].ToString());//总条数
            JArray arrayData = (JArray)jsonObj["announcements"];
            List<string> resultList = new List<string>();
            foreach (var jItem in arrayData)
            {
                string[] fullUrl = jItem["adjunctUrl"].ToString().Split('/');
                string announceTime = fullUrl[1];//日期
                string announcementId = jItem["announcementId"].ToString();
                string announcementTitle = jItem["announcementTitle"].ToString();//标题
                string strView = $@"http://www.cninfo.com.cn/cninfo-new/disclosure/szse_sme/bulletin_detail/true/{announcementId}?announceTime={announceTime}";
                string strDownload = $@"http://www.cninfo.com.cn/cninfo-new/disclosure/szse_sme/download/{announcementId}?announceTime={announceTime}";

                string strHrml = $@"<li>
                                        <label>{announceTime}</label>
                                        <a href='{strView}'
                                            target = '_blank' >
                                             {announcementTitle}
                                         </a>
                                         <a href = '{strView}'
                                       target = '_blank' >
                                        查看
                                    </a>
                                    <a href = '{strDownload}'
                                       target = '_blank'>
                                        下载
                                    </a>
                                </li> ";
                resultList.Add(strHrml);
            }
            return Json(resultList, JsonRequestBehavior.AllowGet);
        }


        public PartialViewResult PartialAd(string viewName = "_PartialAd")
        {
            return PartialView();
        }

        public PartialViewResult PartialSecKill(string viewName = "_PartialSecKill")
        {
            try
            {
                string responseJson = "";
                string callingJson = "";
                string reqUrl = eContract.Common.ConfigHelper.GetSetString("B2BUrl") + "/API/AppInterface.aspx?Action=FLASHBUY&UserId=";
                System.Net.WebClient client = new System.Net.WebClient();
                client.Headers["Content-Type"] = "application/json";
                client.Encoding = System.Text.Encoding.UTF8;
                responseJson = client.UploadString(reqUrl, callingJson);
                Models.Interface.B2BSecKillModel secModel = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Interface.B2BSecKillModel>(responseJson);

                DateTime currTime = DateTime.Now;
                ViewBag.Year = currTime.Year;
                ViewBag.Month = currTime.Month - 1;
                ViewBag.Day = currTime.Day;
                ViewBag.Hour = currTime.Hour;
                ViewBag.Minute = currTime.Minute;
                ViewBag.Second = currTime.Second;

                return PartialView(viewName, secModel);
            }
            catch (Exception ex)
            {
                return PartialView(viewName);
            }

        }

        [HttpPost]
        public void SaveSearchKeyword(FormCollection fc)
        {
            string keyword = fc["Keyword"].ToString();
            //if (HttpContext.User.Identity.IsAuthenticated && CurrentUser != null && Session["IsLogin"] != null)
            //{
            //    LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, keyword, UserCategory.Web.ToString(), UserModule.ProductSearch.ToString());
            //}
            //else
            //{
            //    LogHelp.AddUserLog("", "", keyword, UserCategory.Web.ToString(), UserModule.ProductSearch.ToString());
            //}
        }

        [HttpPost]
        public void GetSearchKeysResult(string Key)
        {
            string strResult = "";
            SortedDictionary<string, string> paras = new SortedDictionary<string, string>();
            paras.Add("Key", Key);
            QQExMailAPIHelper.SendRequstPost(eContract.Common.ConfigHelper.GetSetString("B2CUrl") + "/Product/GetSearchKeysResult", paras, "utf-8", ref strResult);
            Response.Write(strResult.ToString());
        }

        public string HttpPost(string Url, string postDataStr, CookieContainer cookieContainer = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            if (cookieContainer != null)
                request.CookieContainer = cookieContainer;
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
            myStreamWriter.Write(postDataStr);
            myStreamWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

    }
}
