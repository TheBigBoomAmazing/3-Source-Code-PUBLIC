using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;

namespace eContract.Common.Schema
{

    [Serializable]
    public partial class LubrAccountTable: TableInfo
    {
        public const string M_TableName = "Account";

        public const string M_accountid = "accountid";

        public const string M_userid = "userid";

        public const string M_opentime = "opentime";

        public const string M_isvalid = "isvalid";

        public const string M_amount = "amount";

        public const string M_accountclass = "accountclass";

        public const string M_benifit = "benifit";

        public LubrAccountTable()
        {
            _tableName = "Account";
        }

        protected static LubrAccountTable _current;

        public static LubrAccountTable Current
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
            _current = new LubrAccountTable();

            _current.Add(M_accountid, new ColumnInfo(M_accountid, "accountid", true, typeof(string)));

            _current.Add(M_userid, new ColumnInfo(M_userid, "userid", true, typeof(string)));

            _current.Add(M_opentime, new ColumnInfo(M_opentime, "opentime", true, typeof(string)));

            _current.Add(M_isvalid, new ColumnInfo(M_isvalid, "isvalid", true, typeof(string)));

            _current.Add(M_amount, new ColumnInfo(M_amount, "amount", true, typeof(string)));

            _current.Add(M_accountclass, new ColumnInfo(M_accountclass, "accountclass", true, typeof(string)));

            _current.Add(M_benifit, new ColumnInfo(M_benifit, "benifit", true, typeof(string)));
        }

        public ColumnInfo accountid
        {
            get { return this[M_accountid]; }
        }
        public ColumnInfo userid
        {
            get { return this[M_userid]; }
        }
        public ColumnInfo opentime
        {
            get { return this[M_opentime]; }
        }
        public ColumnInfo isvalid
        {
            get { return this[M_isvalid]; }
        }
        public ColumnInfo amount
        {
            get { return this[M_amount]; }
        }
        public ColumnInfo accountclass
        {
            get { return this[M_accountclass]; }
        }
        public ColumnInfo benifit
        {
            get { return this[M_benifit]; }
        }
    }
}
