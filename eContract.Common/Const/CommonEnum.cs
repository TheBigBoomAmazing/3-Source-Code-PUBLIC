using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace eContract.Common
{

    // user status
    public enum UserStatus
    {
        Normal = 0,
        Locked = 1,
    }

    /// <summary>
    /// 用户类型
    /// </summary>
    public enum UserType
    {

        [Description("管理员")]
        SystemAdmin = 0,
        [Description("其它员工")]
        OtherEmployee = 1
    }

    /// <summary>
    /// 日期格式
    /// </summary>
    public enum DateFormat
    {
        yyyy = 1,
        yyyyMM = 2,
        yyyyMMdd = 3
    }

    #region YesNoType
    public enum YesNoType
    {
        [Description("否")]
        No = 0,
        [Description("是")]
        Yes = 1
    }
    #endregion

    #region 角色相关

    /// <summary>
    /// 角色种类，0 功能角色， 1 业务角色
    /// </summary>
    public enum RoleCategory
    {
        FunctionRole = 0,
        BusinessRole = 1
    }

    /// <summary>
    /// 内外部角色,0 内部角色，1外部角色
    /// </summary>
    public enum RoleExternalUser
    {
        InnerRole = 0,
        OutterRole = 1
    }
    /// <summary>
    /// 角色类型 
    /// </summary>
    public enum RoleType
    {
        /// <summary>
        /// 系统管理员
        /// </summary>
        Administrator = 0,

    }
    #endregion

    #region Log
    /// <summary>
    /// 操作类型。 保存至Log表时，使用的是枚举名
    /// </summary>
    public enum OperationType
    {
        UserLogin,
        UserAdd,
        UserEdit,
        UserDelete,
        MobileLog
    }
    #endregion

    /// <summary>
    /// 操作类型
    /// </summary>
    public enum OPERATE
    {
        /// <summary>
        /// 查看
        /// </summary>
        View,
        /// <summary>
        /// 新增
        /// </summary>
        Add,

        /// <summary>
        /// 编辑
        /// </summary>
        Edit,

        /// <summary>
        /// 删除
        /// </summary>
        Delete,

        /// <summary>
        /// 审核
        /// </summary>
        Audit,
        /// <summary>
        /// 终止
        /// </summary>
        Stop,
        /// <summary>
        /// 确认
        /// </summary>
        Confirm
    }


    public enum UpdateLogCategory
    {
        支付方式 = 1,
        秒杀恢复 = 2,
        逾期下单 = 3
    }

    public enum SystemName
    {
        B2B = 1,
        OMS = 2,
        DataPool = 3
    }

    public enum LogCategory
    {
        支付方式 = 1,
        秒杀恢复 = 2,
        逾期下单 = 3
    }

    /// <summary>
    /// Error Type
    /// </summary>
    public enum ErrorType
    {
        [Description("CycleError")]
        CycleError = 1,
        [Description("WarningMessage")]
        WarningMessage = 2,
        [Description("DirtyError")]
        DirtyError = 3
    }

    /// <summary>
    ///1. 激活，2首次登录（不管是否激活） 3. 首次在线支付完成，4 .首次下单完成 
    /// </summary>
    public enum UserActivityType
    {
        Activation = 1,
        FirstLogin = 2,
        FirstOnlinePayment = 3,
        FirstOrderComplete = 4
    }

    /// <summary>
    /// 是否
    /// </summary>
    public enum YesOrNo
    {
        Yes = 1,
        No = 0
    }

    /// <summary>
    /// 优惠券状态
    /// </summary>
    public enum CouponStatus
    {
        Undistributed = 0,//未分配
        Allocated = 1,//已分配
        HasUsed = 2//已使用
    }

    /// <summary>
    /// SyncSystemName
    /// </summary>
    public enum SyncSystemName
    {
        [Description("中国移动")]
        CMCC = 1
    }

    /// <summary>
    /// SyncObjectType
    /// </summary>
    public enum SyncObjectType
    {
        [Description("SKU")]
        SKU = 1,
        [Description("Order")]
        Order = 2,
        [Description("Product")]
        Product = 3
    }

    /// <summary>
    /// SynctType
    /// </summary>
    public enum SyncType
    {
        [Description("产品基本信息")]
        GoodsBasicInfo = 1,
        [Description("产品图片")]
        GoodsPicture = 2,
        [Description("产品价格")]
        GoodsPrice = 3,
        [Description("上架状态")]
        GoodsStatus = 4,
        [Description("产品库存")]
        StockStatus = 5,
        [Description("供应商配货通知")]
        distributeGoodsNotice = 6,
        [Description("交货单妥投通知")]
        deliverGoodsNotice = 7
    }

    /// <summary>
    /// 单据流水号类型
    /// </summary>
    public enum CodeType
    {
        [Description("TrackingLog")]
        TrackingLog = 1,
        [Description("ServiceLog")]
        ServiceLog = 2,
        [Description("SrcFinPayment")]
        SrcFinPayment = 13,
        [Description("PaymentCode")]
        PaymentCode = 14,
        /// <summary>
        /// 统一支付平台
        /// </summary>
        [Description("PayCode")]
        PayCode = 15,
        [Description("MonitorLog")]
        MonitorLog = 18,
        [Description("新版付款流水号")]
        NewPayCode = 20,
        [Description("余单取消单号")]
        CancelRestOrderCode = 21,
        CopLogCode = 22,
        CopOrderCode = 23,
        CopRtnCode = 24,
        CopRtaCode = 25,
        [Description("重筹项目单号")]
        ProjectCode = 26,
        [Description("COP发货单号")]
        DeliveryCode = 27,
        JDLogCode = 28,
        KplLogCode = 29,
        PurchaseOrder = 30,
        InvoiceCode = 31
    }

    public enum TrackingStatus
    {
        待付款 = 5,
        待确认 = 10,
        待审核 = 20,
        已审核 = 30,
        生成交货单 = 40,
        拣配打印 = 44,
        过账 = 60,
        发货完成 = 62,
        到港 = 64,
        客户签收 = 68,
        冲销 = 70,
        作废 = 80,
        冻结 = 90,
        完成 = 100
    }
    public enum OrderErrorType
    {
        [Description("送达方不存在")]
        ShiptoNotExist = 1
    }

    /// <summary>
    /// 消息类型
    /// </summary>
    public enum SAPErrorLogType
    {
        [Description("成功")]
        S = 1,
        [Description("错误")]
        E = 2,
        [Description("警告")]
        W = 3,
        [Description("信息")]
        I = 4,
        [Description("中断")]
        A = 5
    }

    /// <summary>
    /// 数据类型，用户数据权限
    /// </summary>
    public enum DataType
    {
        [Description("B2C供应商")]
        B2CSupplier = 1,
        [Description("B2C客户")]
        B2CCustomer = 2,
        [Description("B2C经销商")]
        B2CDistributor = 3
    }

    public enum LoginDevice
    {
        [Description("电脑")]
        Computer = 1,
        [Description("移动设备")]
        Mobile = 2
    }

    public enum UserLoginType
    {
        [Description("用户名登录")]
        UserNameLogin = 1,
        [Description("短信提醒开启")]
        SmsRemind = 2,
        [Description("邮箱提醒开启")]
        MailRemind = 3,
        [Description("私信提醒开启")]
        MsgRemind = 4,
        [Description("绑定手机")]
        BindMobile = 5,
        [Description("绑定邮箱")]
        BindMail = 6,
        [Description("修改密码")]
        ResetPassword = 7
    }

    public enum PassportOperateType
    {
        [Description("登录")]
        Login = 1,
        [Description("敏感操作")]
        Sensitive = 2
    }

    public enum UserRemindType
    {
        [Description("低")]
        Low = 1,
        [Description("中")]
        Medium = 2,
        [Description("高")]
        High = 3
    }

    public enum BloodType
    {
        [Description("未知")]
        Unknow = 0,
        [Description("A")]
        A = 1,
        [Description("B")]
        B = 2,
        [Description("O")]
        O = 3,
        [Description("AB")]
        AB = 4,
        [Description("其他")]
        Other = 5
    }

    /// <summary>
    /// 其他订单类型 
    ///1001    一般订单
    ///1002    定制订单
    ///1003    退货订单
    ///1004    维修服务订单
    ///1005    借项订单
    ///1006    贷项订单
    ///1007    市场费用核销订单
    ///1008    3M代理订单
    /// </summary>
    public enum OtherOrderType
    {
        一般订单 = 1001,
        定制订单 = 1002,
        退货订单 = 1003,
        维修服务订单 = 1004,
        借项订单 = 1005,
        贷项订单 = 1006,
        市场费用核销订单 = 1007,
        代理3M订单 = 1008
    }


    public enum OrderStatus
    {
        [Description("已提交")]
        Submited,
        [Description("待收货")]
        WaitingRecive,
        [Description("交易完成")]
        Finished
    }

    public enum RetrReasonType
    {
        商品无库存 = 16054001,
        商品库存量不足 = 16054002,
        商品已停产 = 16054003,
        物流无法到达 = 16054004,
        采购单位采购需求取消 = 16054005
    }

    public enum RejectReasonType
    {
        商品已出库 = 16055001,
        商品已送达 = 16055002
    }

    public enum ProductAdjustType
    {
        调整物料编码 = 0,
        调整商城编码 = 1,
        调整物料编码和商城编码对应关系 = 2
    }

    //规则中的比较类型
    public enum RuleCompareTypeEnum
    {
        [Description("字符比较")]
        C = 1,
        [Description("数字比较")]
        N = 2,
        [Description("日期比较")]
        D = 3
    }
    public enum RuleFieldNameEnum
    {
        [Description("Brand")]
        Brand = 1,
        [Description("ProductCategory")]
        ProductCategory = 2,
        [Description("SKU")]
        SKU = 3,
        [Description("PaymentTypeCode")]
        PaymentTypeCode = 4,
        [Description("ActivityCode")]
        ActivityCode = 5,
        [Description("ActivityName")]
        ActivityName = 6,
        [Description("DataSource")]
        DataSource = 7,
        [Description("ItemPriceType")]
        ItemPriceType = 8,
        [Description("PaymentStatus")]
        PaymentStatus = 9,
        [Description("ACTION_DATE")]
        ACTION_DATE = 10,
        [Description("PriceGroupCode")]
        PriceGroupCode = 11,
        [Description("ProductCode")]
        ProductCode = 12
    }

    public enum DistributorRuleFieldNameEnum
    {
        [Description("省")]
        Province = 1,
        [Description("市")]
        City = 2,
        [Description("仓库")]
        Stock = 3
    }

    public enum RuleFieldOperatorEnum
    {
        [Description("SC")]
        SC = 1,
        [Description("IN")]
        IN = 2,
        [Description("NOTIN")]
        NOTIN = 3,
        [Description("CONTAIN")]
        CONTAIN = 4,
        [Description("NOTCONTAIN")]
        NOTCONTAIN = 5
    }

    public enum RuleLevelEnum
    {
        [Description("规则作用于订单")]
        O = 1,
        [Description("规则作用于明细")]
        I = 2,
        [Description("基于其他字段")]
        E = 3
    }

    public enum PromotionMode
    {
        [Description("互斥")]
        Exclusive = 0,
        [Description("共享")]
        Share = 1
    }

    public enum PromotionTypeEnum
    {
        [Description("商品(Product)层级促销")]
        Product = 1, //商品(Product)层级促销
        [Description("订单(商品项)促销")]
        OrderItem = 2,     //订单(商品项)促销
        [Description("订单(金额)促销")]
        OrderValue = 3,   //订单(金额)促销
        [Description("订单（数量)促销")]
        OrderQuantity = 4, //订单（数量)促销
        [Description("满赠优惠")]
        OrderProductFree = 5, //满赠优惠
        [Description("阶梯价")]
        ProductStepPrice = 6 //阶梯价
    }

    public enum ExecuteTypeEnum
    {
        [Description("商品价格优惠")]
        ProductPrice = 1,   // 商品价格优惠
        [Description("订单金额优惠")]
        OrderPrice = 2,   // 订单金额优惠        
        [Description("买赠优惠")]
        ProductFree = 3,   // 买赠优惠    
        [Description("商品组合优惠")]
        GroupSell = 4,    // 商品组合优惠
        [Description("满赠优惠")]
        OrderProductFree = 5, //满赠优惠
        [Description("阶梯价")]
        ProductStepPrice = 6 //阶梯价
    }

    public enum DiscountTypeEnum
    {
        [Description("最终值")]
        FinalValue = 1,      //最终值
        [Description("折扣")]
        PercentValue = 2,      //折扣    
        [Description("优惠值")]
        DiscountValue = 3       //优惠值
    }

    public enum PromotionValueType
    {
        [Description("按倍数调整")]
        RepeatOrderValue = 1, //按倍数调整
        [Description("一次性调整订单值")]
        OnceOrderValue = 2   //一次性调整订单值
    }

    public enum DistributeTypeEnum
    {
        [Description("齐心自营")]
        Comix = 1,
        [Description("第三方人工触发配送")]
        OtherManual = 2,
        [Description("第三方自动配送")]
        OtherAuto = 3,
        [Description("无需配送")]
        NoDistribute = -1
    }

    /// <summary>
    /// 附件类型
    /// </summary>
    public enum AttachmentTypeEnum
    {
        [Description("合同原件")]
        OriginalContract = 1,
        [Description("原合同文件")]
        OldContract = 2,
        [Description("盖章合同")]
        Stampntract = 3
    }

    /// <summary>
    /// 部门类别
    /// </summary>
    public enum DepartmentTypeEnum
    {
        [Description("申请部门")]
        ApplyDepartment = 1,
        [Description("审批部门")]
        ApproveDepartment = 2
    }

    public enum POStatusEnum
    {
        [Description("已保存")]
        Save = 1,
        [Description("已申请")]
        Apply = 2,
        [Description("已审批")]
        Approve = 3,
        [Description("已拒绝")]
        Reject = 4
    }

    public enum ContractFilingEnum
    {
        [Description("已保存")]
        Save = 1,
        [Description("已申请")]
        Apply = 2,
        [Description("PO保存")]
        POSave = 3,
        [Description("已审批")]
        Approve = 4,
        [Description("已拒绝")]
        Reject = 5
    }

    public enum ContractStatusEnum
    {
        [Description("异常状态")]
        Error = 0,
        [Description("未提交")]
        Uncommitted=1,
        [Description("待审批")]
        WaitApproval=2,
        [Description("审批通过")]
        HadApproval=3,
        [Description("审批拒绝")]
        ApprovalReject=4,
        [Description("签署完成")]
        SignedCompleted=5,
        [Description("关闭")]
        Shutdown = 6,
        [Description("后台关闭")]
        BackgroundShutdown = 7,
        [Description("重新提交")]
        Resubmit = 8
    }

    public enum ContractApproverStatusEnum
    {
        [Description("未开始")]
        NotBegin= 1,
        [Description("待审批")]
        WaitApproval = 2,
        [Description("已审批")]
        HadApproval = 3,
        [Description("已过期")]
        OverTime = 4
    }

    public enum ApplyTypeEnum
    {
        [Description("申请部门")]
        Department = 1,
        [Description("申请用户")]
        User = 2,
        [Description("全部部门")]
        AllDepartment = 3
    }

    public enum ApproverTypeEnum
    {
        [Description("领导批注")]
        LeaderComment = 1,
        [Description("大区总监")]
        RegionManager = 2,
        [Description("部门总监")]
        DepartmentManager = 3,
        [Description("审批部门")]
        Department = 4,
        [Description("领导审批")]
        LeaderApprove = 5
    }

    public enum ApproverResultEnum
    {
        [Description("批注")]
        Comment = 1,
        [Description("审批通过Approved")]
        Approved = 2,
        [Description("审批通过NotApplicable")]
        NotApplicable = 3,
        [Description("审批拒绝")]
        ApprovalReject = 4
    }

    public enum ContractGroupEnum
    {
        [Description("普通合同")]
        NormalContract = 1,
        [Description("历史合同")]
        HistoryContract = 2
    }

    public enum SaveTypeEnum
    {
        [Description("保存")]
        Save = 1,
        [Description("提交")]
        Submit = 2
    }
}
