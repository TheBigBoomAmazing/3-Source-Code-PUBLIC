using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using System.Reflection;
using System.ComponentModel;
using eContract.Common;
using eContract.Common.WebUtils;

namespace eContract.Common.Const
{
    public class SysConst
    {
        public const string ICON_VIEW = "/App_Themes/POS/images/icon/view.png";
        public const string ICON_SELECT = "/App_Themes/POS/images/icon/check.png";
        public const string ICON_EDIT = "/App_Themes/POS/images/icon/edit.png";
        public const string ICON_AUDIT = "/App_Themes/POS/images/icon/issue.png";
        public const string ICON_DELETE = "/App_Themes/POS/images/icon/delete.png";
        public const string ICON_USER = "/App_Themes/POS/images/icon/user.png";
        public const string ICON_CONFIG = "/App_Themes/POS/images/icon/config.png";
        public const string ICON_POWER = "/App_Themes/POS/images/icon/power.png";
        public const string ICON_START = "/App_Themes/POS/images/icon/start.png";
        public const string ICON_END = "/App_Themes/POS/images/icon/end.png";
        public const string ICON_QY = "/App_Themes/POS/images/icon/qy.jpg";
        public const string ICON_APPROVE = "/App_Themes/POS/images/icon/approval.png";
        public const string ICON_STOP = "/App_Themes/POS/images/icon/useless.gif";
        public const string ICON_MAKECARD = "/App_Themes/POS/images/icon/make_card.png";
        public const string ICON_SCXF = "/App_Themes/POS/images/icon/goods_tongbu_kucun.png";
        public const string ICON_YUE = "/App_Themes/POS/images/icon/state_ok.png";
        public const string ICON_RECHECK = "/App_Themes/POS/images/icon/recheck.png";
        public const string ICON_CONFIRM = "/App_Themes/POS/images/icon/acceptance.png";
        public const string ICON_EXECUTE = "/App_Themes/POS/images/icon/execute.png";
        public const string ICON_PERVIEW = "/App_Themes/POS/images/icon/PerView.png";
        public const string ICON_ROLE = "/App_Themes/POS/images/icon/users.png";
        public const string ICON_PASSWORD = "/App_Themes/POS/images/icon/pass_edit.png";
        public const string ICON_YK = "/App_Themes/POS/images/icon/yk.png";
        public const string ICON_JS = "/App_Themes/POS/images/icon/huifu.jpg";
        public const string ICON_FD = "/App_Themes/POS/images/icon/instead_fee.png";
        public const string ICON_WC = "/App_Themes/POS/images/icon/qr.png";
        public const string ICON_LOCK = "/App_Themes/POS/images/icon/lock.png";
        public const string ICON_GS = "/App_Themes/POS/images/icon/gs.png";
        public const string ICON_TYYC = "/App_Themes/POS/images/icon/tyyc.jpg";
        public const string ICON_HK = "/App_Themes/POS/images/icon/hk.png";
        public const string ICON_VPSJ = "/App_Themes/POS/images/icon/vip_up.png";
        public const string ICON_VPJJ = "/App_Themes/POS/images/icon/goods_down.png";
        public const string ICON_XM = "/App_Themes/POS/images/icon/xm.png";
        public const string ICON_HTMLVIEW = "/App_Themes/POS/images/icon/HtmlView.png";
        public const string ICON_EDITPIC = "/App_Themes/POS/images/icon/eidt_pic.png";
        public const string ICON_PLUS = "/App_Themes/POS/images/plus.gif";

        #region 参数
        public const string C_Param_bi_auto_new_set = "bi_auto_new_set";//引进日期为新品
        public const string C_Param_bi_auto_new_set_pi = "bi_auto_new_set_pi";//入库日期为新品
        public const string C_Param_bi_oos_limit = "bi_oos_limit";//商品子码库存低于再订货点时为断色断码
        public const string C_Param_bi_oos_zero = "bi_oos_zero";//商品子码库存为0时为断色断码
        public const string C_Parem_g_branch_no = "g_branch_no";//默认门店编号
        public const string C_Parem_g_stock_no = "g_stock_no";//默认仓库
        #endregion

        #region 编码项目
        public const string C_bianma_wu = "0";  //无项目
        public const string C_bianma_ymd = "1"; //日期-yymmdd
        public const string C_bianma_ym = "2";  //日期-yymm
        public const string C_bianma_yy = "3";  //日期-yy
        #endregion

        #region 单据编号
        public const string C_danju_lqsmjhd = "danju_lqsmjhd "; // 礼券扫描激活单";
        public const string C_danju_tjrjld = "danju_tjrjld"; // 推荐人积分奖励策略单";
        public const string C_danju_dybm = "danju_dybm"; // 店员编码";
        public const string C_danju_gkhfjld = "danju_gkhfjld"; // 顾客回访记录单";
        public const string C_danju_jpxsd = "danju_jpxsd"; // 竞品销售单";
        public const string C_danju_zzfwd = "danju_zzfwd"; // 增值服务单";
        public const string C_danju_bgsqd = "danju_bgsqd"; // 顾客资料变更申请单";
        public const string C_danju_czktzd = "danju_czktzd"; // 储值卡余额调整单";
        public const string C_danju_vpcztzd = "danju_vpcztzd"; // VIP卡充值调整单";
        public const string C_danju_vpzzsj = "danju_vpzzsj"; // VIP卡自主升级";
        public const string C_danju_sms = "danju_sms"; // 自编短信设置单";
        public const string C_danju_sryhd = "danju_sryhd"; // VIP生日优惠策略单";
        public const string C_danju_jfdxd = "danju_jfdxd"; // VIP积分抵现策略单";
        public const string C_danju_zdczktkd = "danju_zdczktkd"; // 储值卡退款单";
        public const string C_danju_zdczkxsd = "danju_zdczkxsd"; // 储值卡销售单";
        public const string C_danju_zdykd = "danju_zdykd"; // 终端移库单";
        public const string C_danju_zdcyd = "danju_zdcyd"; // 终端差异单";
        public const string C_danju_vipfq = "danju_vipfq"; // VIP批量发券单";
        public const string C_danju_spwxd = "danju_spwxd"; // 商品维修单";
        public const string C_danju_ydzbd = "danju_ydzbd"; // 终端月度指标设置单";
        public const string C_danju_jfdhd = "danju_jfdhd"; // VIP积分兑换设置单";
        public const string C_danju_sjgzd = "danju_sjgzd"; // VIP卡升级规则设置单";
        public const string C_danju_fkgzd = "danju_fkgzd"; // VIP卡发卡规则设置单";
        public const string C_danju_fwtzd = "danju_fwtzd"; // VIP卡适用范围调整单";
        public const string C_danju_vptfd = "danju_vptfd"; // VIP卡二次投放单";
        public const string C_danju_zdz = "danju_zdz"; // 友好终端组";
        public const string C_danju_cbtzd = "danju_cbtzd"; // 成本调整单";
        public const string C_danju_zdfzd = "danju_zdfzd"; // 终端封帐单";
        public const string C_danju_zdjhd = "danju_zdjhd"; // 终端进货单";
        public const string C_danju_zdjrd = "danju_zdjrd"; // 终端要货单";
        public const string C_danju_zdjhtzd = "danju_zdjhtzd"; // 终端要货单";
        public const string C_danju_pjd = "danju_pjd"; // 盘点计划单";
        public const string C_danju_lsd = "danju_lsd"; // 终端零售单";
        public const string C_danju_czkxsd = "danju_czkxsd"; // 储值卡销售单";
        public const string C_danju_zdtzd = "danju_zdtzd"; // 终端调整单";
        public const string C_danju_qtsy = "danju_qtsy"; // 前台收银";
        public const string C_danju_czkzcd = "danju_czkzcd"; // 储值卡制成投放单";
        public const string C_danju_jftzd = "danju_jftzd"; // 积分调整单";
        public const string C_danju_vpzcd = "danju_vpzcd"; // VIP制成单主表";
        public const string C_danju_prd = "danju_prd"; // 盘点任务单";
        public const string C_danju_ltd = "danju_ltd"; // 终端零退单";
        public const string C_danju_czkczd = "danju_czkczd"; // 储值卡充值单";
        public const string C_danju_gk = "danju_gk"; // 顾客资料";
        public const string C_danju_zdpdd = "danju_zdpdd"; // 终端盘点单";
        public const string C_danju_lsdxd = "danju_lsdxd"; // 终端零售代销单";
        public const string C_danju_ltdxd = "danju_ltdxd"; // 终端零退代销单";

      
        public const string C_danju_zdcx = "danju_zdcx"; // 整单促销单";
        public const string C_danju_kbcx = "danju_kbcx"; // 捆绑促销单";
       
        public const string C_danju_zyd = "danju_zyd"; // VIP卡转移单";
        public const string C_danju_xszb = "danju_xszb"; // 远程终端销售指标设置";
        public const string C_danju_vpczd = "danju_vpczd"; // VIP充值单";
        public const string C_danju_gksyd = "danju_gksyd"; //顾客试衣单
        public const string C_danju_gktsd = "danju_gktsd"; //顾客投诉单

        public const string C_danju_zdthtzd = "danju_zdthtzd"; // 终端退货通知单";
        public const string C_danju_zdthd = "danju_zdthd"; // 终端退货单";
        public const string C_danju_zdthsqd = "danju_zdthsqd"; // 终端申请单";

        public const string C_danju_ckjcd = "danju_ckjcd";//仓库结存单
        public const string C_danju_qckcd = "danju_qckcd";//期初库存单

        //采购
        public const string C_danju_cgdd = "danju_cgdd";
        public const string C_danju_cgtzd = "danju_cgtzd";
        public const string C_danju_cgjhd = "danju_cgjhd";
        public const string C_danju_cgthsqd = "danju_cgthsqd";
        public const string C_danju_cgthtzd = "danju_cgthtzd";
        public const string C_danju_cgthd = "danju_cgthd";

        //批发
        public const string C_danju_pfdd = "danju_pfdd";
        public const string C_danju_pftzd = "danju_pftzd";
        public const string C_danju_pfchd = "danju_pfchd";
        public const string C_danju_pfthsqd = "danju_pfthsqd";
        public const string C_danju_pfthtzd = "danju_pfthtzd";
        public const string C_danju_pfthd = "danju_pfthd";

        //调拨
        public const string C_danju_qddbdd = "danju_qddbdd";
        public const string C_danju_qddbtzd = "danju_qddbtzd";
        public const string C_danju_qddbd = "danju_qddbd";
        public const string C_danju_qddbthsqd = "danju_qddbthsqd";
        public const string C_danju_qddbthtzd = "danju_qddbthtzd";
        public const string C_danju_qddbthd = "danju_qddbthd";

        //移仓
        public const string C_danju_spycsqd = "danju_spycsqd";
        public const string C_danju_spyctzd = "danju_spyctzd";
        public const string C_danju_spycd = "danju_spycd";

        //调配
        public const string C_danju_qddpd = "danju_qddpd";

     
        //配货
        public const string C_danju_zdphsqd = "danju_zdphsqd";
        public const string C_danju_zdphtzd = "danju_zdphtzd";
        public const string C_danju_zdphd = "danju_zdphd";
        public const string C_danju_zdphthsqd = "danju_zdphthsqd";
        public const string C_danju_zdphthtzd = "danju_zdphthtzd";
        public const string C_danju_zdphthd = "danju_zdphthd";


        //价格
        public const string C_danju_spdjd = "danju_spdjd";
        public const string C_danju_sptjd = "danju_sptjd";
        public const string C_danju_xjfy = "danju_xjfy";
        public const string C_danju_Qtsr = "danju_Qtsr";
        public const string C_danju_ybfy = "danju_ybfy";
        public const string C_danju_yszj = "danju_yszj";
        public const string C_danju_ysjs = "danju_ysjs";
        public const string C_danju_yfzj = "danju_yfzj";
        public const string C_danju_yfjs = "danju_yfjs";
        public const string C_danju_tzzj = "danju_tzzj";
        public const string C_danju_tzjs = "danju_tzjs";
        public const string C_danju_yfk = "danju_yfk";
        public const string C_danju_ysk = "danju_ysk";
        #endregion

        // 短信模板的宏
        public const string Macro_SMS_ApproveComment = "%ApproveComment%";
        public const string Macro_SMS_UploadDate = "%UploadDate%";
        public const string Macro_SMS_Password = "%Password%";
        public const string Macro_SMS_DownLoadPath = "%DownLoadPath%";

        // 用户导入时缺省密码
        public const string DefaultUserPassword = "123456";

        // 参数类型和名称
        public const string C_PARAM_LocationDiffRange = "LocationDiffRange";
        public const string C_PARAM_SMSTemplate = "SMSTemplate";
        public const string C_PARAM_TelecomOperator = "TelecomOperator";
        public const string C_SMS_LocationReupload = "LocationReupload";
        public const string C_SMS_PhotoReupload = "PhotoReupload";
        public const string C_SMS_NewStoreManager = "NewStoreManager";
        public const string C_SMS_GetPassword = "GetPassword";
        public const string C_Campaign_type = "CampaignType";
        public const string C_Floor_Plan = "FloorPlan";
        public const string C_PASSWORD_EXPIRED = "PassWordExpired";
        public const string C_Android = "Android";

        /// <summary>
        /// 客户端程序发布状态
        /// </summary>
        public const string StatePublished = "Published";
        public const string StateUnPublished = "UnPublished";
        /// <summary>
        /// 客户端上传路径
        /// </summary>
        public static readonly string UploadPath = WebCaching.CurrentContext.Server.MapPath(ConfigurationSettings.AppSettings["UploadPath"].ToString());
        public static readonly string FilePath = ConfigurationSettings.AppSettings["UploadPath"].ToString();


        public static readonly string GoogleMapKey = ConfigurationSettings.AppSettings["GoogleMapKey"].ToString();

        /// <summary>
        /// 限定上传文件类型
        /// </summary>
        //程序文件
        public const string FilterFileTypeProgram = ".zip";
        //文档文件
        public const string FilterFileTypeDocument = ".zip";

        public const int FileNameMaxLength = 50;
        /// <summary>
        /// 上传文件的最大值
        /// </summary>
        public static int UploadFileMaxLength
        {
            get
            {
                System.Configuration.Configuration configuration = WebConfigurationManager.OpenWebConfiguration("/Web.config");
                SystemWebSectionGroup systemWeb = (SystemWebSectionGroup)configuration.GetSectionGroup("system.web");
                HttpRuntimeSection runtimeSection = systemWeb.HttpRuntime;
                return runtimeSection.MaxRequestLength;
            }
        }


        /// <summary>
        /// 管理员角色名称
        /// </summary>
        public const string C_Admin = "Admin";

        public const string C_Boutique = "Boutique";

        public const string C_role_zongbu = "1";
        public const string C_role_zhongduan = "2";
        public const string C_role_admin = "0";

        public const string C_usertype_zongbu = "1";
        public const string C_usertype_zhongduan = "2";
        public const string C_usertype_admin = "0";

        public const string C_yyzt_NotStart = "0";
        public const string C_yyzt_Start = "1";
        public const string C_yyzt_End = "2";

        // 订单状态 -  -1 取消  2 完成  add by Jason.liu 20150624
        public const string C_order_status_cancel = "-1";
        public const string C_order_status_complete = "2";

        // 订单支付状态 -  0 未支付  2 已支付  add by Jason.liu 20150624
        public const string C_payment_status_unpaid = "0";
        public const string C_payment_status_paid = "2";

        /// <summary>
        /// 根层的ParentID,SpendingPool和Orgnazation会用到
        /// </summary>
        public const string C_Root_Parent_ID = "0";


        public const string C_RoleType = "RoleType";

        public const string CONTROLDATEFORMAT = "yyyy-MM-dd";
        public const string TIMEFORMAT = "yyyy-MM-dd HH:mm";
        public const string DATE_FORMATE = "yyyy-MM-dd";

        public const string INTFORMAT = "{0:N}";
        public const string DECIMALFORMAT = "{0:N2}";
        public const string PERCENTAGEFORMAT = "{0:0.00%}";

        public static string FormatDecimal(string strDate)
        {
            if (strDate.Length == 0)
                return "";

            return string.Format(DECIMALFORMAT, Utils.ToDecimal(strDate));
        }

        public static string FormatDecimal(decimal decDate)
        {

            return string.Format(DECIMALFORMAT, decDate);
        }

        public static string FormatPercentage(string strDate)
        {
            if (strDate.Length == 0)
                return "";

            return string.Format(PERCENTAGEFORMAT, Utils.ToDecimal(strDate));
        }

        public static string FormatPercentage(decimal decDate)
        {

            return string.Format(PERCENTAGEFORMAT, decDate);
        }

        public static string FormatInt(string strDate)
        {
            if (strDate.Length == 0)
                return "";

            return string.Format(INTFORMAT, Utils.ToDecimal(strDate));
        }



        /// <summary>
        /// 日期控件显示
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string FormatControlDate(object objdate)
        {
            if (objdate == null || objdate.ToString().Trim().Length == 0)
                return "";

            try
            {
                return DateTime.Parse(objdate.ToString()).ToString(CONTROLDATEFORMAT);
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 时间格式
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string FormatControlTime(object objdate)
        {
            if (objdate == null || objdate.ToString().Trim().Length == 0)
                return "";

            try
            {
                return DateTime.Parse(objdate.ToString()).ToString(TIMEFORMAT);
            }
            catch
            {
                return "";
            }

        }

        /// <summary>
        /// 日期控件显示
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string FormatControlDate(DateTime objdate)
        {

            try
            {
                return objdate.ToString(CONTROLDATEFORMAT);
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 时间格式
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string FormatControlTime(DateTime objdate)
        {
            if (objdate == null || objdate.ToString().Trim().Length == 0)
                return "";

            try
            {
                return objdate.ToString(TIMEFORMAT);
            }
            catch
            {
                return "";
            }

        }

        public static string GetEnumDescript(int iEnumHasCode, Type enumType)
        {
            FieldInfo[] fieldinfos = enumType.GetFields();
            if (fieldinfos != null)
            {
                foreach (FieldInfo field in fieldinfos)
                {
                    Object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

                    if (objs != null && objs.Length > 0)
                    {
                        if (Enum.Parse(enumType, field.Name).GetHashCode() == iEnumHasCode)
                        {
                            DescriptionAttribute da = (DescriptionAttribute)objs[0];
                            return da.Description;
                        }
                    }
                }
            }

            return "";
        }

        public const string C_COMIXB2B = "ComixB2B";

        public const string SSO_MD5_SALT = "ComixSSOSalt";
    }
}
