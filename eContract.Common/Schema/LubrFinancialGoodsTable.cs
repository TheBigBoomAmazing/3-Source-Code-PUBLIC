using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;

namespace eContract.Common.Schema
{
    [Serializable]
    public partial class LubrFinancialGoodsTable : TableInfo
    {
        public const string M_TableName = "FinancialGoods";

        public const string M_goodsid = "goodsid";

        public const string M_startdate = "startdate";

        public const string M_enddate = "enddate";

        public const string M_comment = "comment";

        public const string M_goodsname = "goodsname";

        public const string M_period = "period";

        public const string M_annualrate = "annualrate";

        public const string M_principal = "principal";

        public const string M_isvalid = "isvalid";

        public const string M_picture = "picture";

        public LubrFinancialGoodsTable()
        {
            _tableName = "FinancialGoods";
        }

        protected static LubrFinancialGoodsTable _current;

        public static LubrFinancialGoodsTable Current
        {
            get
            {
                if (_current == null)
                {
                    Initial();
                }
                return _current;
            }
        }
        private static void Initial()
        {
            _current = new LubrFinancialGoodsTable();

            _current.Add(M_goodsid, new ColumnInfo(M_goodsid, "goodsid", true, typeof(string)));

            _current.Add(M_startdate, new ColumnInfo(M_startdate, "startdate", true, typeof(string)));

            _current.Add(M_enddate, new ColumnInfo(M_enddate, "enddate", true, typeof(string)));

            _current.Add(M_comment, new ColumnInfo(M_comment, "comment", true, typeof(string)));

            _current.Add(M_goodsname, new ColumnInfo(M_goodsname, "goodsname", true, typeof(string)));

            _current.Add(M_period, new ColumnInfo(M_period, "period", true, typeof(string)));

            _current.Add(M_annualrate, new ColumnInfo(M_annualrate, "annualrate", true, typeof(string)));

            _current.Add(M_principal, new ColumnInfo(M_principal, "principal", true, typeof(string)));

            _current.Add(M_isvalid, new ColumnInfo(M_isvalid, "isvalid", true, typeof(string))); 

            _current.Add(M_picture, new ColumnInfo(M_picture, "picture", true, typeof(string)));

        }


        public ColumnInfo goodsid
        {
            get { return this[M_goodsid]; }
        }
        public ColumnInfo startdate
        {
            get { return this[M_startdate]; }
        }
        public ColumnInfo enddate
        {
            get { return this[M_enddate]; }
        }
        public ColumnInfo comment
        {
            get { return this[M_comment]; }
        }
        public ColumnInfo goodsname
        {
            get { return this[M_goodsname]; }
        }
        public ColumnInfo period
        {
            get { return this[M_period]; }
        }
        public ColumnInfo annualrate
        {
            get { return this[M_annualrate]; }
        }
        public ColumnInfo principal
        {
            get { return this[M_principal]; }
        }
        public ColumnInfo isvalid
        {
            get { return this[M_isvalid]; }
        }
        public ColumnInfo picture
        {
            get { return this[M_picture]; }
        }
    }
}
