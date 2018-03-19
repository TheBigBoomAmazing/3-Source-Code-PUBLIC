using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Schema;

namespace eContract.Common.Entity
{
    [Serializable]
    public partial class LubrFinancialGoodsEntity: EntityBase
    {
        public LubrFinancialGoodsTable TableSchema
        {
            get
            {
                return LubrFinancialGoodsTable.Current;
            }
        }

        //public LubrFinancialGoodsEntity()
        //{
        //    IsDeleted = false;
        //}

        public override TableInfo OringTableSchema
        {
            get
            {
                return LubrFinancialGoodsTable.Current;
            }
        }

        #region 属性列表
        public string goodsid
        {
            get { return (string)GetData(LubrFinancialGoodsTable.M_goodsid); }
            set { SetData(LubrFinancialGoodsTable.M_goodsid, value); }
        }

        public string startdate
        {
            get { return (string)GetData(LubrFinancialGoodsTable.M_startdate); }
            set { SetData(LubrFinancialGoodsTable.M_startdate, value); }
        }
        public string enddate
        {
            get { return (string)GetData(LubrFinancialGoodsTable.M_enddate); }
            set { SetData(LubrFinancialGoodsTable.M_enddate, value); }
        }
        public string comment
        {
            get { return (string)GetData(LubrFinancialGoodsTable.M_comment); }
            set { SetData(LubrFinancialGoodsTable.M_comment, value); }
        }
        public string goodsname
        {
            get { return (string)GetData(LubrFinancialGoodsTable.M_goodsname); }
            set { SetData(LubrFinancialGoodsTable.M_goodsname, value); }
        }
        public string period
        {
            get { return (string)GetData(LubrFinancialGoodsTable.M_period); }
            set { SetData(LubrFinancialGoodsTable.M_period, value); }
        }
        public string annualrate
        {
            get { return (string)GetData(LubrFinancialGoodsTable.M_annualrate); }
            set { SetData(LubrFinancialGoodsTable.M_annualrate, value); }
        }
        public string principal
        {
            get { return (string)GetData(LubrFinancialGoodsTable.M_principal); }
            set { SetData(LubrFinancialGoodsTable.M_principal, value); }
        }
        public string isvalid
        {
            get { return (string)GetData(LubrFinancialGoodsTable.M_isvalid); }
            set { SetData(LubrFinancialGoodsTable.M_isvalid, value); }
        }
        public string picture
        {
            get { return (string)GetData(LubrFinancialGoodsTable.M_picture); }
            set { SetData(LubrFinancialGoodsTable.M_picture, value); }
        }
        #endregion
    }
}
