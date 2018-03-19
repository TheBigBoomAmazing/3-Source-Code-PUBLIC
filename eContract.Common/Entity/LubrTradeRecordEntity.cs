using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Schema;

namespace eContract.Common.Entity
{
    [Serializable]
    public partial class LubrTradeRecordEntity : EntityBase
    {
        public LubrTradeRecordTable TableSchema
        {
            get
            {
                return LubrTradeRecordTable.Current;
            }
        }

        public override TableInfo OringTableSchema
        {
            get
            {
                return LubrTradeRecordTable.Current;
            }
        }

        #region 属性列表
        public string accountid
        {
            get { return (string)GetData(LubrTradeRecordTable.M_accountid); }
            set { SetData(LubrTradeRecordTable.M_accountid, value); }
        }

        public string tradetime
        {
            get { return (string)GetData(LubrTradeRecordTable.M_tradetime); }
            set { SetData(LubrTradeRecordTable.M_tradetime, value); }
        }

        public string tradeaccount
        {
            get { return (string)GetData(LubrTradeRecordTable.M_tradeaccount); }
            set { SetData(LubrTradeRecordTable.M_tradeaccount, value); }
        }

        public string finalgoods
        {
            get { return (string)GetData(LubrTradeRecordTable.M_finalgoods); }
            set { SetData(LubrTradeRecordTable.M_finalgoods, value); }
        }

        public string id
        {
            get { return (string)GetData(LubrTradeRecordTable.M_id); }
            set { SetData(LubrTradeRecordTable.M_id, value); }
        }
        #endregion
    }
}
