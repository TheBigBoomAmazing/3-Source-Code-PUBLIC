using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Schema;

namespace eContract.Common.Entity
{
    [Serializable]
    public partial class LubrAccountEntity: EntityBase
    {
        public LubrAccountTable TableSchema
        {
            get
            {
                return LubrAccountTable.Current;
            }
        }

        public override TableInfo OringTableSchema
        {
            get
            {
                return LubrAccountTable.Current;
            }
        }

        #region 属性列表

        public string accountid
        {
            get { return (string)GetData(LubrAccountTable.M_accountid); }
            set { SetData(LubrAccountTable.M_accountid, value); }
        }
        public string userid
        {
            get { return (string)GetData(LubrAccountTable.M_userid); }
            set { SetData(LubrAccountTable.M_userid, value); }
        }

        public string opentime
        {
            get { return (string)GetData(LubrAccountTable.M_opentime); }
            set { SetData(LubrAccountTable.M_opentime, value); }
        }

        public string isvalid
        {
            get { return (string)GetData(LubrAccountTable.M_isvalid); }
            set { SetData(LubrAccountTable.M_userid, value); }
        }

        public string amount
        {
            get { return (string)GetData(LubrAccountTable.M_amount); }
            set { SetData(LubrAccountTable.M_amount, value); }
        }

        public string accountclass
        {
            get { return (string)GetData(LubrAccountTable.M_accountclass); }
            set { SetData(LubrAccountTable.M_accountclass, value); }
        }

        public string benifit
        {
            get { return (string)GetData(LubrAccountTable.M_benifit); }
            set { SetData(LubrAccountTable.M_benifit, value); }
        }

        #endregion
    }
}
