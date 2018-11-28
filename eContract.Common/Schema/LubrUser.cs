using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suzsoft.Smart.EntityCore;

namespace eContract.Common.Schema
{
    [Serializable]
    public partial class LubrUserTable:TableInfo
    {
        public const string M_TableName = "User";

        public const string M_username = "username";

        public const string M_password = "password";

        public const string M_userid = "userid";

        public const string M_age = "age";

        public const string M_realname = "realname";

        public const string M_idcard = "idcard";

        public const string M_userclass = "userclass";

        public const string M_createtime = "createtime";

        public const string M_endtime = "endtime";

        public const string M_logintime = "logintime";

        public const string M_emailaddress = "emailaddress";

        public const string M_phonenumber = "phonenumber";

        public const string M_VerificationCode = "VerificationCode";

        public LubrUserTable()
        {
            _tableName = "user";
        }

        protected static LubrUserTable _current;
        public static LubrUserTable Current
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
            _current = new LubrUserTable();

            _current.Add(M_userid, new ColumnInfo(M_userid, "userid", true, typeof(string)));

            _current.Add(M_username, new ColumnInfo(M_username, "username", true, typeof(string)));

            _current.Add(M_password, new ColumnInfo(M_password, "password", true, typeof(string)));

            _current.Add(M_age, new ColumnInfo(M_age, "age", true, typeof(string)));

            _current.Add(M_realname, new ColumnInfo(M_realname, "realname", true, typeof(string)));

            _current.Add(M_idcard, new ColumnInfo(M_idcard, "idcard", true, typeof(string)));

            _current.Add(M_userclass, new ColumnInfo(M_userclass, "userclass", true, typeof(string)));

            _current.Add(M_createtime, new ColumnInfo(M_createtime, "createtime", true, typeof(string)));

            _current.Add(M_endtime, new ColumnInfo(M_endtime, "endtime", true, typeof(string)));

            _current.Add(M_logintime, new ColumnInfo(M_logintime, "logintime", true, typeof(string)));

            _current.Add(M_emailaddress, new ColumnInfo(M_emailaddress, "emailaddress", true, typeof(string)));

            _current.Add(M_phonenumber, new ColumnInfo(M_phonenumber, "phonenumber", true, typeof(string)));

            _current.Add(M_VerificationCode, new ColumnInfo(M_VerificationCode, "VerificationCode", true, typeof(string)));
        }

        public ColumnInfo userid
        {
            get { return this[M_userid]; }
        }
        public ColumnInfo username
        {
            get { return this[M_username]; }
        }
        public ColumnInfo password
        {
            get { return this[M_password]; }
        }
        public ColumnInfo age
        {
            get { return this[M_age]; }
        }
        public ColumnInfo realname
        {
            get { return this[M_realname]; }
        }
        public ColumnInfo idcard
        {
            get { return this[M_idcard]; }
        }
        public ColumnInfo userclass
        {
            get { return this[M_userclass]; }
        }
        public ColumnInfo createtime
        {
            get { return this[M_createtime]; }
        }
        public ColumnInfo endtime
        {
            get { return this[M_endtime]; }
        }
        public ColumnInfo logintime
        {
            get { return this[M_logintime]; }
        }
        public ColumnInfo emailaddress
        {
            get { return this[M_emailaddress]; }
        }
        public ColumnInfo phonenumber
        {
            get { return this[M_phonenumber]; }
        }
        public ColumnInfo VerificationCode
        {
            get { return this[M_VerificationCode]; }
        }
    }
}
