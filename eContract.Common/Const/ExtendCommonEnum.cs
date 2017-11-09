using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace eContract.Common.Const
{
    public class ExtendCommonEnum
    {
        /// <summary>
        /// 结算方式
        /// </summary>
        public enum PayWay
        {

            [Description("临时指定")]
            TempDesignation = 0,
            [Description("指定账期")]
            SpecifiedAccount = 1,
            [Description("临时日期")]
            TempDate = 2,
            [Description("货到付款")]
            CashOnDelivery = 3

        }

        /// <summary>
        /// 客户类型
        /// </summary>
        public enum CustomerType
        {
            [Description("供应商")]
            S = 0,
            [Description("客户")]
            C = 1
        }

        /// <summary>
        /// 账户状态
        /// </summary>
        public enum AccountType
        {
            [Description("正常")]
            Normal = 0,
            [Description("冻结")]
            Frozen = 1
        }

        /// <summary>
        /// 默认价格
        /// </summary>
        public enum AccLevel
        {
            [Description("当前成本价")]
            CurrentCost = 1,
            [Description("会员价")]
            MemberPrice = 2,
            [Description("价格为零")]
            PriceZero = 3,
            [Description("进货价")]
            PurchasePrice = 4,
            [Description("零售价")]
            RetailPrice = 5,
            [Description("批发价")]
            WholesalePrice = 6,
            [Description("批发价1")]
            WholesalePrice1 = 7,
            [Description("批发价2")]
            WholesalePrice2 = 8,
            [Description("批发价3")]
            WholesalePrice3 = 9,
            [Description("批发价4")]
            WholesalePrice4 = 10,
            [Description("批发价5")]
            WholesalePrice5 = 11,
            [Description("批发价6")]
            WholesalePrice6 = 12,
            [Description("批发价7")]
            WholesalePrice7 = 13,
            [Description("批发价8")]
            WholesalePrice8 = 14,
            [Description("约定售价")]
            AgreedPrice = 15,
            [Description("最近进货价")]
            RecentPurchasePrice = 16,
            [Description("最近售价")]
            RecentRetailPrice = 17
        }

        /// <summary>
        /// 批量调价被调价
        /// </summary>
        public enum BulkPriceTo
        {
            [Description("最低售价")]
            Sale_Min_Price = 0,
            [Description("零售价")]
            Sale_Price = 1,
            [Description("进货价")]
            Price = 2,
            [Description("批发价")]
            Base_Price = 3,
            [Description("批发价1")]
            Base_Price1 = 4,
            [Description("批发价2")]
            Base_Price2 = 5,
            [Description("批发价3")]
            Base_Price3 = 6,
            [Description("批发价4")]
            Base_Price4 = 7,
            [Description("批发价5")]
            Base_Price5 = 8,
            [Description("批发价6")]
            Base_Price6 = 9,
            [Description("批发价7")]
            Base_Price7 = 10,
            [Description("批发价8")]
            Base_Price8 = 11,
            [Description("配送价")]
            Trans_Price = 12
        }
        /// <summary>
        /// 批量调价从调价
        /// </summary>
        public enum BulkPriceFrom
        {
            [Description("最低售价")]
            Sale_Min_Price = 0,
            [Description("零售价")]
            Sale_Price = 1,
            [Description("进货价")]
            Price = 2,
            [Description("批发价")]
            Base_Price = 3,
            [Description("配送价")]
            Trans_Price = 4,
            [Description("会员价")]
            Vip_Price = 5
        }
        /// <summary>
        /// 账务代码
        /// </summary>
        public enum AccountCode
        {
            [Description("前台付款方式")]
            ReceptionPayment = 0,
            [Description("非交易收入")]
            PurchasePrice = 1,
            [Description("后台付款方式")]
            BackstagePayment = 2,
            [Description("币种")]
            Currency = 3,
            [Description("往来费用")]
            TransactionCosts = 4,
            [Description("现金账户付款方式")]
            CashPayment = 5
        }

        /// <summary>
        /// 分店仓库属性
        /// </summary>
        public enum BranchProperty
        {
            [Description("外部机构")]
            ExternalInstitution = 0,
            [Description("本地机构")]
            LocalAgency = 1,
            [Description("本地仓库")]
            LocalStorage = 2,
            [Description("外部仓库")]
            ExternalStorage = 3
        }

        /// <summary>
        /// 配送价
        /// </summary>
        public enum PriceOption
        {
            [Description("进价")]
            PurchasePrice = 1,
            [Description("成本价")]
            CostPrice = 2,
            [Description("配送价")]
            DeliveryPrice = 3
        }

        /// <summary>
        /// 商品异常
        /// </summary>
        public enum ItemException
        {
            [Description("商品档案异常")]
            Archives = 0,
            [Description("异常成本")]
            Cost = 1,
            [Description("异常毛利")]
            Profit = 2,
            [Description("负库存商品")]
            NegativeInventory = 3,
            [Description("淘汰商品")]
            Eliminated = 4
        }

        /// <summary>
        /// 商品价格
        /// </summary>
        public enum ItemPriceType
        {
            [Description("零售价")]
            RetailPrice = 0,
            [Description("进货价")]
            PurchasePrice = 1,
            [Description("批发价")]
            WholesalePrice = 2,
            [Description("会员价")]
            MemberPrice = 3
        }

        /// <summary>
        /// 商品状态
        /// </summary>
        public enum ItemType
        {
            [Description("正常")]
            Normal = 1,
            [Description("淘汰")]
            Eliminated = 2
        }
        /// <summary>
        /// 库管维护
        /// </summary>
        public enum ItemStockType
        {
            [Description("库管维护")]
            ItemStock = 0,
            [Description("非库管维护")]
            NoneItemStock = 1
        }
        /// <summary>
        /// 柜台，营业员价格
        /// </summary>
        public enum CounterOrSaleManStatue
        {
            [Description("正常")]
            Normal = 0,
            [Description("禁用")]
            Disable = 1
        }
        /// <summary>
        /// 流水类型
        /// </summary>
        public enum FlowType
        {
            [Description("商品销售流水")]
            SaleFlow = 0,
            [Description("收银流水")]
            CashFlow = 1,
            [Description("非交易收入")]
            NonTradingIncome = 2,
            [Description("临时客户销售流水")]
            TempcustSaleFlow = 3,
            [Description("订金汇款")]
            DepositMoney = 4,
            [Description("订金查询")]
            DepositQuery = 5,
            [Description("订货查询")]
            OrderInquiry = 6
        }

        /// <summary>
        /// 销售方式
        /// </summary>
        public enum SaleWay
        {
            [Description("销售")]
            A = 0,
            [Description("退货")]
            B = 1,
            [Description("赠送")]
            C = 2,
            [Description("找零")]
            D = 3

        }

        /// <summary>
        /// 汇总类型
        /// </summary>
        public enum ItemSaleType
        {
            [Description("商品汇总")]
            ProductSummary = 0,
            [Description("商品-分部汇总")]
            CommodityDivisionSummary = 1,
            [Description("类别汇总")]
            ClassSummary = 2,
            [Description("类别-分布汇总")]
            CategoryDistributionSummary = 3,
            [Description("新品汇总")]
            NewProductSummary = 4,
            [Description("商品汇总(分布)")]
            CommodityDistributionSummary = 5,
            [Description("大类汇总")]
            LargeClassSummary = 6
        }

        /// <summary>
        /// 对账类型
        /// </summary>
        public enum ReconciliationType
        {
            [Description("收银员对账")]
            CashierCheck = 0,
            [Description("收银员让利")]
            CashierRangli = 1,
            [Description("营业员对账")]
            SaleCheck = 2,
            [Description("收银日报")]
            CashierDaily = 3
        }

        /// <summary>
        /// 毛利分析类型
        /// </summary>
        public enum MargiAnalysisType
        {
            [Description("商品汇总")]
            ProducSummary = 0,
            [Description("类别汇总")]
            ClassSummary = 1,
            [Description("商品毛利汇总(按机构)")]
            CommodityGrosSummary = 2,
            [Description("日毛利汇总")]
            SummaryOnMaori = 3,
            [Description("大类毛利汇总")]
            CategoriesSummaryMaori = 4,
            [Description("毛利分析(按税率)")]
            MargiAnalysis = 5,
            [Description("商品毛利汇总(按仓库)")]
            CommodityGrossSummary = 6
        }



        /// <summary>
        /// 客单分析类型
        /// </summary>
        public enum CustomerAnalysisType
        {
            [Description("消费群体分析(按性别)")]
            BySex = 0,
            [Description("消费群体分析(按年龄)")]
            ByAge = 1,
            [Description("历史客单分析")]
            ByHistory = 2
        }

        /// <summary>
        /// 毛利分析类型
        /// </summary>
        public enum WholesaleSummaryType
        {
            [Description("商品汇总")]
            Product = 0,
            [Description("类别汇总")]
            Class = 1,
            [Description("销售明细")]
            SalesDetail = 2,
            [Description("业务员汇总")]
            Business = 3,
            [Description("区域汇总")]
            Area = 4,
            [Description("新品汇总")]
            NewProduct = 5,
            [Description("批发销售尺码横排")]
            SalesSize = 6,
            [Description("批发销售单列横排")]
            SalesCol = 7,
            [Description("批发款式明细")]
            StyleDetail = 8,
            [Description("批发款式汇总")]
            StyleSummary = 9
        }

        /// <summary>
        /// 业务员销售提成
        /// </summary>
        public enum SalesmanSalesCommissionType
        {
            [Description("业务员销售提成汇总")]
            Summary = 0,
            [Description("业务员销售提成明细")]
            Detaile = 1,
            [Description("业务员品牌销售汇总")]
            BrandSummary = 2
        }

        /// <summary>
        ///  账款类型
        /// </summary>
        public enum AccountsReceivableType
        {
            [Description("到期帐款")]
            AccountDue = 0,
            [Description("应收帐款(汇总)")]
            AccountSummary = 1,
            [Description("应收帐款(明细)")]
            AccountDetail = 2,
            [Description("历史往来帐款")]
            AccountHistory = 3,
            [Description("已收帐款明细")]
            ReceivedAccountDetail = 4
        }



        /// <summary>
        /// 子码规则
        /// </summary>
        public enum BarCodeRules
        {
            [Description("款号+花色+尺码")]
            SizeOne = 1,
            [Description("款号+尺码+花色")]
            SizeTwo = 2,
            [Description("款号+尺码")]
            SizeThree = 3
        }


        /// <summary>
        /// 提成比率
        /// </summary>
        public enum FMType
        {
            [Description("按比率")]
            ForPersent = 0,
            [Description("按金额")]
            ForMoney = 1

        }


        /// <summary>
        /// 商品类型
        /// </summary>
        public enum CombineSta
        {
            [Description("普通商品")]
            GeneralMerchandise = 0,
            [Description("捆绑商品")]
            BundleCommodity = 1,
            [Description("制单拆分")]
            SingleSplit = 2,
            [Description("制单组合")]
            SingleCombination = 3

        }

        /// <summary>
        /// 采购范围   
        /// </summary>
        public enum Direct
        {
            [Description("总部配购")]
            Direct1 = 0,
            [Description("门店采购")]
            Direct2 = 1,
            [Description("不限")]
            Direct3 = 2,
            [Description("总订店收")]
            Direct4 = 3,
            [Description("自产")]
            Direct5 = 4

        }


        /// <summary>
        /// 盘点范围  
        /// </summary>
        public enum Inventoryrange
        {
            [Description("全场盘点")]
            Direct1 = 0,
            [Description("单品盘点")]
            Direct2 = 1,
            [Description("类别盘点")]
            Direct3 = 2,
            [Description("品牌盘点")]
            Direct4 = 3,
            [Description("柜组盘点")]
            Direct5 = 4

        }

        /// <summary>
        /// 单位
        /// </summary>
        public enum UnitNo
        {
            [Description("件")]
            UnitOne = 1,
            [Description("条")]
            UnitTwo = 2,
            [Description("套")]
            UnitThree = 3,
            [Description("双")]
            UnitFour = 4,
            [Description("个")]
            UnitFive = 5,
            [Description("副")]
            UnitSix = 6,
            [Description("其它")]
            UnitSeven = 7
        }

        public enum ApproveFlag
        {
            [Description("未审核")]
            NotApproved = 0,
            [Description("已审核")]
            Approved = 1
        }

        /// <summary>
        /// 订单进货状态
        /// </summary>
        public enum OrderStockStatus
        {
            [Description("未进货")]
            None = 0,
            [Description("部分进货")]
            Partly = 1,
            [Description("完全进货")]
            Complete = 2,
        }

        /// <summary>
        /// 商品特性查询
        /// </summary>
        public enum ClothingFeature
        {
            [Description("新品上市")]
            NewProduct = 0,
            [Description("断色断码")]
            OOSProduct = 1,
            [Description("新品销售")]
            NewProductSales = 3

        }

        /// <summary>
        /// 会员类型
        /// </summary>
        public enum VipType
        {
            [Description("会员卡")]
            MemberShipCard = 0,
            [Description("折扣卡")]
            DiscountCard = 1
        }

        /// <summary>
        /// 订单的有效性
        /// </summary>
        public enum ValidStatus
        {
            [Description("过期")]
            Expired,
            [Description("有效")]
            Valid
        }

        /// <summary>
        /// 文本位置
        /// </summary>
        public enum EnumTextAlign
        {
            [Description("居左")]
            Left = 1,
            [Description("居中")]
            Center = 2,
            [Description("居右")]
            Right = 3
        }

        /// <summary>
        /// 订单进货状态
        /// </summary>
        public enum OrderSalesStatus
        {
            [Description("未销售")]
            None = 0,
            [Description("部分销售")]
            Partly = 1,
            [Description("完全销售")]
            Complete = 2,
        }

        /// <summary>
        /// 收款类型
        /// </summary>
        public enum DiscountType
        {
            [Description("按折扣")]
            ByDiscount = 0,
            [Description("按金额")]
            ByAmount = 1
        }

        /// <summary>
        /// 性别
        /// </summary>
        public enum Sex
        {
            [Description("女")]
            Woman = 0,
            [Description("男")]
            Man = 1
        }

        /// <summary>
        /// 婚姻要求
        /// </summary>
        public enum Marriage
        {
            [Description("未婚")]
            UnMarriage = 0,
            [Description("已婚")]
            Marriage = 1,
            [Description("离异")]
            Divorce = 2
        }

        /// <summary>
        /// 积分业务查询
        /// </summary>
        public enum VipIntegralReportType
        {
            [Description("会员积分查询")]
            IntegralSearch = 0,
            [Description("礼品兑换查询")]
            GiftChangeSearch = 1,
            [Description("冲减积分明细")]
            WashSearch = 2,
            [Description("奖励积分明细")]
            AwardIntegralSearch = 3,
            [Description("积分转储值")]
            IntegralChangeCZSearch = 4
        }

        /// <summary>
        /// 储值卡查询
        /// </summary>
        public enum VipStoreReportType
        {
            [Description("储值卡信息")]
            StoreCardInfo = 0,
            [Description("充值记录")]
            RechargeNote = 1
        }

        /// <summary>
        /// 储值卡消费查询
        /// </summary>
        public enum VipCardConsumReportType
        {
            [Description("付款记录")]
            PaymentNote = 0,
            [Description("商品明细")]
            ProductList = 1,
            [Description("会员让利")]
            VipChangeMoney = 2,
            [Description("类别汇总")]
            TypeLetOn = 3,
            [Description("按发卡人汇总")]
            CardGather = 4,
            [Description("储值消费汇总")]
            SvalueConsumption = 5,
            [Description("消费金额排行")]
            ConsumAmountRand = 6,
            [Description("储值卡消费汇总")]
            CardConsumLetOn = 7
        }


        /// <summary>
        /// 促销方案类型
        /// </summary>
        public enum CamCXType
        {
            [Description("捆绑促销")]
            kbcx = 0,
            [Description("整单促销")]
            zdcx = 1
        }


        /// <summary>
        /// 成功还是失败
        /// </summary>
        public enum TrueFalseType
        {
            T = 1,//成功
            F = 0,//失败
            W = 2//等待
        }

        /// <summary>
        /// 获取的订单状态的类型
        /// </summary>
        /// <remarks>订单组合状态 1 等待付款   | 2 等待处理 | 3 取消订单 | 4 订单锁定 | 5 等待付款确认 | 6 正在处理 |7 配货中  |8 已发货 |9  已完成 </remarks>
        public enum OrderMainStatus
        {
            None = -1,
            /// <summary>
            /// 等待付款
            /// </summary>
            Paying = 1,

            /// <summary>
            /// 等待处理
            /// </summary>
            PreHandle = 2,

            /// <summary>
            /// 取消订单
            /// </summary>
            Cancel = 3,

            /// <summary>
            /// 订单锁定
            /// </summary>
            Locking = 4,

            /// <summary>
            /// 等待付款确认
            /// </summary>
            PreConfirm = 5,

            /// <summary>
            /// 正在处理
            /// </summary>
            Handling = 6,

            /// <summary>
            /// 配货中
            /// </summary>
            Shipping = 7,

            /// <summary>
            /// 已发货
            /// </summary>
            Shiped = 8,
            /// <summary>
            /// 已完成
            /// </summary>
            Complete = 9
        }

        /// <summary>
        /// 获取的配送状态的类型    //  配送状态 0 未发货 | 1 打包中 | 2 已发货 | 3 已确认收货 | 4 拒收退货中 | 5 拒收已退货
        /// </summary>
        public enum ShippingStatus
        {
            None = -1,
            /// <summary>
            /// 未发货
            /// </summary>
            UnShipped = 0,
            /// <summary>
            /// 打包中
            /// </summary>
            Packing = 1,

            /// <summary>
            /// 已发货
            /// </summary>
            Shipped = 2,

            /// <summary>
            /// 已确认收货
            /// </summary>
            ConfirmShip = 3,

            /// <summary>
            /// 拒收退货中
            /// </summary>
            RejectedReturning = 4,

            /// <summary>
            /// 拒收已退货
            /// </summary>
            RejectedReturned = 5,

            ///<summary>
            ///部分配货
            ///</summary>
            PartPacking = 6,

            ///<summary>
            ///部分发货
            ///</summary>
            PartShipped = 7
        }

        /// <summary>
        /// 支付状态    //  支付状态 0 未支付 | 1 等待确认 | 2 已支付 | 3 处理中 | 4 支付异常 
        /// </summary>
        public enum PaymentStatus
        {
            None = -1,
            /// <summary>
            /// 未支付
            /// </summary>
            Unpaid = 0,
            /// <summary>
            /// 等待确认
            /// </summary>
            PreConfirm = 1,

            /// <summary>
            /// 已支付
            /// </summary>
            Paid = 2,

            /// <summary>
            /// 处理中
            /// </summary>
            Handling = 3,

            /// <summary>
            /// 支付异常
            /// </summary>
            PayException = 4

        }

        /// <summary>
        /// 订单状态    -4 系统锁定   | -3 后台锁定 | -2 用户锁定 | -1 死单（取消） | 0 未处理 | 1 进行中 |2 已完成 
        /// </summary>
        public enum OrderStatus
        {
            /// <summary>
            /// 系统锁定
            /// </summary>
            SystemLock = -4,
            /// <summary>
            /// 后台锁定
            /// </summary>
            AdminLock = -3,

            /// <summary>
            /// 用户锁定
            /// </summary>
            UserLock = -2,

            /// <summary>
            /// 死单
            /// </summary>
            Cancel = -1,

            /// <summary>
            /// 未处理
            /// </summary>
            UnHandle = 0,
            /// <summary>
            /// 进行中
            /// </summary>
            Handling = 1,

            /// <summary>
            /// 已完成
            /// </summary>
             Complete = 2        

        }
    }
}
