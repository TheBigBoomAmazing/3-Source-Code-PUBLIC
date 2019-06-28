using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Schema;

namespace eContract.Common.Entity
{
    [Serializable]
    public partial class LubrUserEntity : EntityBase
    {
        public LubrUserTable TableSchema
        {
            get
            {
                return LubrUserTable.Current;
            }
        }
        public override TableInfo OringTableSchema
        {
            get
            {
                return LubrUserTable.Current;
            }
        }
        #region 属性列表
        public string userid
        {
            get { return (string)GetData(LubrUserTable.M_userid); }
            set { SetData(LubrUserTable.M_userid, value); }
        }

        public string username
        {
            get { return (string)GetData(LubrUserTable.M_username); }
            set { SetData(LubrUserTable.M_username, value); }
        }

        public string password
        {
            get { return (string)GetData(LubrUserTable.M_password); }
            set { SetData(LubrUserTable.M_password, value); }
        }

        public string age
        {
            get { return (string)GetData(LubrUserTable.M_age); }
            set { SetData(LubrUserTable.M_age, value); }
        }

        public string realname
        {
            get { return (string)GetData(LubrUserTable.M_realname); }
            set { SetData(LubrUserTable.M_realname, value); }
        }

        public string idcard
        {
            get { return (string)GetData(LubrUserTable.M_idcard); }
            set { SetData(LubrUserTable.M_idcard, value); }
        }

        public string userclass
        {
            get { return (string)GetData(LubrUserTable.M_userclass); }
            set { SetData(LubrUserTable.M_userclass, value); }
        }

        public string createtime
        {
            get { return (string)GetData(LubrUserTable.M_createtime); }
            set { SetData(LubrUserTable.M_createtime, value); }
        }

        public string endtime
        {
            get { return (string)GetData(LubrUserTable.M_endtime); }
            set { SetData(LubrUserTable.M_endtime, value); }
        }

        public string logintime
        {
            get { return (string)GetData(LubrUserTable.M_logintime); }
            set { SetData(LubrUserTable.M_logintime, value); }
        }

        public string emailaddress
        {
            get { return (string)GetData(LubrUserTable.M_emailaddress); }
            set { SetData(LubrUserTable.M_emailaddress, value); }
        }

        public string phonenumber
        {
            get { return (string)GetData(LubrUserTable.M_phonenumber); }
            set { SetData(LubrUserTable.M_phonenumber, value); }
        }

        public string VerificationCode
        {
            get { return (string)GetData(LubrUserTable.M_VerificationCode); }
            set { SetData(LubrUserTable.M_VerificationCode, value); }
        }

        #endregion
    }
}
