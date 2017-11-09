using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eContract.Web.Models.Interface
{
    public class GetSearchKeysResultModel
    {
        public List<RESULTItem> RESULT { get; set; }
    }

    public class GroupBuy
    {
        /// <summary>
        /// 
        /// </summary>
        public int groupBuyId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int regionId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int productId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int sequence { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int finePrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string startDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string endDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int maxCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int groupCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int buyCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int categoryId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int limitQty { get; set; }
    }

    public class SkuInfoDic
    {
    }

    public class CategoryDic
    {
    }

    public class RESULTItem
    {
        /// <summary>
        /// 
        /// </summary>
        public GroupBuy groupBuy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> productImages { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> attributeInfos { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> skuInfos { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> productAccessories { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> accessorieValue { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> relatedProducts { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isRec { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isNow { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isHot { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isLowPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public SkuInfoDic skuInfoDic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public CategoryDic categoryDic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int reviews { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int minPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int maxPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string activeStartDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string activeEndDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string extIsMaster { get; set; }
        /// <summary>
        /// 齐心
        /// </summary>
        public string exBrandName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isStore { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int parentCategoryId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int supplierCategoryId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int customerCategoryId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isFaved { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isCustomer { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int exIsSingleBuy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isSingleBuy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int oldMarketPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int categoryId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int typeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int productId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int brandId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int discountShow { get; set; }
        /// <summary>
        /// 齐心 C5884-5 金齐心高白复印纸80克 A4 5包 白
        /// </summary>
        public string productName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string productCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int supplierId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int regionId { get; set; }
        /// <summary>
        /// 齐心 C5884-5 金齐心高白复印纸80克 A4 5包 白
        /// </summary>
        public string meta_Title { get; set; }
        /// <summary>
        /// 齐心 C5884-5 金齐心高白复印纸80克 A4 5包 白
        /// </summary>
        public string meta_Description { get; set; }
        /// <summary>
        /// 齐心 C5884-5 金齐心高白复印纸80克 A4 5包 白
        /// </summary>
        public string meta_Keywords { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int saleStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string addedDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int vistiCounts { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int saleCounts { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int displaySequence { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int lineId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int marketPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int lowestSalePrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int penetrationStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mainCategoryPath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string extendCategoryPath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hasSKU { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int points { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string imageUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string thumbnailUrl1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int maxQuantity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int minQuantity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int salesType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int restrictionCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string customerCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int jDPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int productDataSource { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int thumbnailCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int limitCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int salePrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int proSalesPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int countDownId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string proSalesEndDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int suppCategoryId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int suppParentCategoryId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int suppParent2CategoryId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int stockNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int tag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isMaster { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string supplierCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int productRetailPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int productSaleUnitPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string createdTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string modifiedTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int customerId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isDefault { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int supplierMarketPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int supplierSalePrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int supplierWeight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int supplierTag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int customerTag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int supplierDisplaySequence { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int customerDisplaySequence { get; set; }
    }

}