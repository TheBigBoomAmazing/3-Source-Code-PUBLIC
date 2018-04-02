using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Suzsoft.Smart.Data;
using Suzsoft.Smart.EntityCore;
using eContract.BusinessService.Common;
using eContract.Common;
using eContract.Common.Entity;
using eContract.Common.MVC;
using eContract.Common.WebUtils;
using eContract.BusinessService.BusinessData.CommonQuery;
using System.Web.Script.Serialization;
using eContract.BusinessService.SystemManagement.Service;
using eContract.BusinessService.SystemManagement.Domain;

namespace eContract.BusinessService.BusinessData.BusinessRule
{
    public class LubrProductsShowBLL : BusinessBase
    {
        /// <summary>
        /// 获取所有产品的列表
        ///</summary>
        /// <returns></returns>
        public List<LubrFinancialGoodsEntity> GetAllProducts()
        {
            string sql = "SELECT * FROM FinancialGoods";
            List<LubrFinancialGoodsEntity> list = new List<LubrFinancialGoodsEntity>();
            list = DataAccess.Select<LubrFinancialGoodsEntity>(sql);
            return list;
        }
        /// <summary>
        /// 获得单个产品的详细信息
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public LubrFinancialGoodsEntity GetProductsDetailInfo(string id)
        {
            //var strsql = new StringBuilder();
            //strsql.AppendFormat("SELECT * FROM FinancialGoods WHERE goodsid='{0}'",id);  
            string  sql = $@"SELECT * FROM FinancialGoods WHERE goodsid={id}";
            LubrFinancialGoodsEntity productDetail = DataAccess.Select<LubrFinancialGoodsEntity>(sql).FirstOrDefault();
            return productDetail;
        }
    }
}
