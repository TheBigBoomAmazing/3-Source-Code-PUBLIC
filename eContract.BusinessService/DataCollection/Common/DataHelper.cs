using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Suzsoft.Smart.Data;
using Suzsoft.Smart.EntityCore;

namespace TopDATA.BusinessService.DataCollection.Common
{
    public class DataHelper
    {
        public static string Read(string path, Encoding encoding)
        {
            try
            {
                string text = System.IO.File.ReadAllText(path, encoding);
                return text;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        public static DataTable GetGoodsByCustomerCode(string customerCode)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT ARTICLE_NO,BAR_CODE FROM dbo.BSN_GOODS  ");
            sql.AppendFormat("WHERE CUSTOMER_CODE = '{0}'", customerCode);
            DataTable dt = DataAccess.SelectDataSet(sql.ToString()).Tables[0];
            return dt;
        }

        public static DataTable GetTopEnterpriseByCustomerCode(string customerCode)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT STORE_CODE,TOP_STORE_CODE,TOP_STORE_NAME ");
            sql.AppendLine("FROM dbo.BSN_ENTERPRISE_MATCH  ");
            sql.AppendFormat("WHERE CUSTOMER_CODE = '{0}'", customerCode);
            DataTable dt = DataAccess.SelectDataSet(sql.ToString()).Tables[0];
            return dt;
        }

        public static DataTable GetGoodsByEnterpriseCode(string enterpriseCode)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT T1.ARTICLE_NO,T1.BAR_CODE FROM dbo.BSN_GOODS T1 ");
            sql.AppendLine("JOIN dbo.MST_ENTERPRISE T2 ON T1.CUSTOMER_CODE = T2.CUSTOMER_CODE ");
            sql.AppendFormat("WHERE T2.ENTERPRISE_CODE = '{0}'", enterpriseCode);
            //DataAccessParameterCollection dpc = new DataAccessParameterCollection();
            //dpc.AddWithValue("ENTERPRISE_CODE", enterpriseCode);
            DataTable dt = DataAccess.SelectDataSet(sql.ToString()).Tables[0];
            return dt;
        }

        public static DataTable GetTopEnterpriseByEnterpriseCode(string enterpriseCode)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT T1.STORE_CODE,T1.TOP_STORE_CODE,T1.TOP_STORE_NAME ");
            sql.AppendLine("FROM dbo.BSN_ENTERPRISE_MATCH T1 ");
            sql.AppendLine("JOIN dbo.MST_ENTERPRISE T2 ON T1.CUSTOMER_CODE = T2.CUSTOMER_CODE ");
            sql.AppendFormat("WHERE T2.ENTERPRISE_CODE = '{0}'", enterpriseCode);
            //DataAccessParameterCollection dpc = new DataAccessParameterCollection();
            //dpc.AddWithValue("ENTERPRISE_CODE", enterpriseCode);
            DataTable dt = DataAccess.SelectDataSet(sql.ToString()).Tables[0];
            return dt;
        }

        public static string GetBarCode(string articalNo, DataTable dtGoods)
        {
            DataView dv = dtGoods.AsEnumerable().Where(c => c["ARTICLE_NO"].ToString() == articalNo).AsDataView();
            return dv.Count > 0 ? dv[0].Row["BAR_CODE"].ToString() : "";
        }

        public static void GetTopStrore(string storeCode, DataTable dtTopEnterprise, ref string topStoreCode, ref string topStoreName)
        {
            DataView dv = dtTopEnterprise.AsEnumerable().Where(c => c["STORE_CODE"].ToString() == storeCode).AsDataView();
            if (dv.Count <= 0) return;
            topStoreCode = dv[0].Row["TOP_STORE_CODE"].ToString();
            topStoreName = dv[0].Row["TOP_STORE_NAME"].ToString();
        }

        public static string GetArticalNo(string barCode, DataTable dtGoods)
        {
            DataView dv = dtGoods.AsEnumerable().Where(c => c["BAR_CODE"].ToString() == barCode).AsDataView();
            return dv.Count > 0 ? dv[0].Row["ARTICLE_NO"].ToString() : "";
        }
    }
}
