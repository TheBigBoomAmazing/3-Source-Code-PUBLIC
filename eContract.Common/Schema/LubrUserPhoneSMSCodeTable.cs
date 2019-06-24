using Suzsoft.Smart.EntityCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eContract.Common.Schema
{
    [Serializable]
    public partial class LubrUserPhoneSMSCodeTable : TableInfo
    {
        public const string M_TableName = "UserPhoneSMSCode";

        public const string M_SMSId = "SMSId";

        public const string M_phonenumber = "phonenumber";

        public const string M_sendtime = "sendtime";

        public const string M_expiretime = "expiretime";

        public const string M_smscode = "smscode";

        public const string M_spare2 = "spare2";

        public LubrUserPhoneSMSCodeTable()
        {
            _tableName = "UserPhoneSMSCode";
        }

        protected static LubrUserPhoneSMSCodeTable _current;

        public static LubrUserPhoneSMSCodeTable Current
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
            _current = new LubrUserPhoneSMSCodeTable();

            _current.Add(M_SMSId, new ColumnInfo(M_SMSId, "SMSId", true, typeof(string)));

            _current.Add(M_phonenumber, new ColumnInfo(M_phonenumber, "phonenumber", true, typeof(string)));

            _current.Add(M_sendtime, new ColumnInfo(M_sendtime, "sendtime", true, typeof(string)));

            _current.Add(M_expiretime, new ColumnInfo(M_expiretime, "expiretime", true, typeof(string)));

            _current.Add(M_smscode, new ColumnInfo(M_smscode, "smscode", true, typeof(string)));

            _current.Add(M_spare2, new ColumnInfo(M_spare2, "spare2", true, typeof(string)));
        }

        public ColumnInfo SMSId
        {
            get { return this[M_SMSId]; }
        }
        public ColumnInfo phonenumber
        {
            get { return this[M_phonenumber]; }
        }
        public ColumnInfo sendtime
        {
            get { return this[M_sendtime]; }
        }
        public ColumnInfo expiretime
        {
            get { return this[M_expiretime]; }
        }
        public ColumnInfo smscode
        {
            get { return this[M_smscode]; }
        }
        public ColumnInfo spare2
        {
            get { return this[M_spare2]; }
        }
    }
}
