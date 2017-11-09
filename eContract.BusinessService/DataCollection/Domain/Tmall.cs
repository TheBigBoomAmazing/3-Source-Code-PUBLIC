using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TopDATA.BusinessService.DataCollection.Common;

namespace TopDATA.BusinessService.DataCollection.Domain
{
    public class Tmall : IDataCollection
    {
        private readonly DataTable _dtGoods;
        private readonly DataTable _dtTopEnterprise;

        public Tmall(string enterpriseCode)
        {
            if (_dtGoods == null)
                _dtGoods = DataHelper.GetGoodsByEnterpriseCode(enterpriseCode);
            if (_dtTopEnterprise == null)
                _dtTopEnterprise = DataHelper.GetTopEnterpriseByEnterpriseCode(enterpriseCode);
        }


        public DataTable InventoryData(string strBuff)
        {
            #region 分析Json数据
            DataTable dt = new DataTable();
            dt.Columns.Add("ArticleNo");//商品ID
            dt.Columns.Add("StoreCode");//仓库编码
            dt.Columns.Add("StoreName");//仓库名称
            dt.Columns.Add("Inventory");//仓库可用库存
            dt.Columns.Add("Date");//记录日期
            dt.Columns.Add("BarCode");
            dt.Columns.Add("TopStoreCode");
            dt.Columns.Add("TopStoreName");

            var jsonObj = (JObject)JsonConvert.DeserializeObject(strBuff);
            JArray array = (JArray)jsonObj["rows"];
            string aa = "";

            
            foreach (var jItem in array)
            {
                //获取客户编号及客户简称
                string strTopStoreCode = "";
                string strTopStoreName = "";
                DataHelper.GetTopStrore(jItem["storeCodeName"].ToString(), _dtTopEnterprise, ref strTopStoreCode, ref strTopStoreName);

                DataRow dr = dt.NewRow();
                dr[0] = jItem["itemId"].ToString();
                dr[1] = jItem["storeCode"].ToString();
                dr[2] = jItem["storeCodeName"].ToString();
                dr[3] = jItem["sellableInventory"].ToString();
                dr[4] = DateTime.Now.ToString("yyyy-MM-dd");
                dr[5] = jItem["barcode"].ToString();
                dr[6] = strTopStoreCode;
                dr[7] = strTopStoreName;

                dt.Rows.Add(dr);
            }
            #endregion
            return dt;
        }

        public DataTable SalesData(string strBuff)
        {
            #region 分析Json数据
            DataTable dt = new DataTable();
            dt.Columns.Add("ArticleNo");//商品ID
            dt.Columns.Add("StoreCode");//仓库编码
            dt.Columns.Add("StoreName");//仓库名称
            dt.Columns.Add("Sales");//销量
            dt.Columns.Add("Date");//记录日期
            dt.Columns.Add("BarCode");
            dt.Columns.Add("TopStoreCode");
            dt.Columns.Add("TopStoreName");
            var jsonObj = (JObject)JsonConvert.DeserializeObject(strBuff);
            JArray array = (JArray)jsonObj["rows"];
            string aa = "";

            foreach (var jItem in array)
            {
                //获取客户编号及客户简称
                string strTopStoreCode = "";
                string strTopStoreName = "";
                DataHelper.GetTopStrore(jItem["storecode"].ToString(), _dtTopEnterprise, ref strTopStoreCode, ref strTopStoreName);

                DataRow dr = dt.NewRow();
                dr[0] = jItem["itemId"].ToString();
                dr[1] = jItem["storecode"].ToString();
                dr[2] = jItem["storeCodeDesc"].ToString();
                dr[3] = jItem["payOrdItmQty1d001"].ToString();
                dr[4] = DateTime.Now.ToString("yyyy-MM-dd");
                dr[5] = jItem["barcode"].ToString();
                dr[6] = strTopStoreCode;
                dr[7] = strTopStoreName;

                dt.Rows.Add(dr);
            }
            #endregion
            return dt;
        }

        public DataTable OrderData(string strBuff)
        {
            throw new NotImplementedException();
        }
    }
}
