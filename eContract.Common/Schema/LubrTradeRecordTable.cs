using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;

namespace eContract.Common.Schema
{
    [Serializable]
    public partial class LubrTradeRecordTable: TableInfo
    {
        public const string M_TableName = "TradeRecord";

        public const string M_accountid = "accountid";

        public const string M_tradetime = "tradetime";

        public const string M_tradeaccount = "tradeaccount";

        public const string M_finalgoods = "finalgoods";

        public const string M_id = "id";

        public LubrTradeRecordTable()
        {
            _tableName = "TradeRecord";
        }

        protected static LubrTradeRecordTable _current;

        public static LubrTradeRecordTable Current
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
            _current = new LubrTradeRecordTable();

            _current.Add(M_accountid, new ColumnInfo(M_accountid, "accountid", true, typeof(string)));

            _current.Add(M_tradetime, new ColumnInfo(M_tradetime, "tradetime", true, typeof(string)));

            _current.Add(M_tradeaccount, new ColumnInfo(M_tradeaccount, "tradeaccount", true, typeof(string)));

            _current.Add(M_finalgoods, new ColumnInfo(M_finalgoods, "finalgoods", true, typeof(string)));

            _current.Add(M_id, new ColumnInfo(M_id, "id", true, typeof(string)));
        }

        public ColumnInfo accountid
        {
            get { return this[M_accountid]; }
        }

        public ColumnInfo tradetime
        {
            get { return this[M_tradetime]; }
        }

        public ColumnInfo tradeaccount
        {
            get { return this[M_tradeaccount]; }
        }

        public ColumnInfo finalgoods
        {
            get { return this[M_finalgoods]; }
        }

        public ColumnInfo id
        {
            get { return this[M_id]; }
        }
    }
}
