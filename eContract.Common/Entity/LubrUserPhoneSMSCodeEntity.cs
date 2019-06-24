using eContract.Common.Schema;
using Suzsoft.Smart.EntityCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eContract.Common.Entity
{
    public partial class LubrUserPhoneSMSCodeEntity : EntityBase
    {
        public LubrUserPhoneSMSCodeTable TableSchema
        {
            get
            {
                return LubrUserPhoneSMSCodeTable.Current;
            }
        }
        public override TableInfo OringTableSchema
        {
            get
            {
                return LubrUserPhoneSMSCodeTable.Current;
            }
        }

        #region 属性列表
        public string SMSId
        {
            get { return (string)GetData(LubrUserPhoneSMSCodeTable.M_SMSId); }
            set { SetData(LubrUserPhoneSMSCodeTable.M_SMSId, value); }
        }

        public string phonenumber
        {
            get { return (string)GetData(LubrUserPhoneSMSCodeTable.M_phonenumber); }
            set { SetData(LubrUserPhoneSMSCodeTable.M_phonenumber, value); }
        }

        public string sendtime
        {
            get { return (string)GetData(LubrUserPhoneSMSCodeTable.M_sendtime); }
            set { SetData(LubrUserPhoneSMSCodeTable.M_sendtime, value); }
        }

        public string expiretime
        {
            get { return (string)GetData(LubrUserPhoneSMSCodeTable.M_expiretime); }
            set { SetData(LubrUserPhoneSMSCodeTable.M_expiretime, value); }
        }

        public string smscode
        {
            get { return (string)GetData(LubrUserPhoneSMSCodeTable.M_smscode); }
            set { SetData(LubrUserPhoneSMSCodeTable.M_smscode, value); }
        }

        public string spare2
        {
            get { return (string)GetData(LubrUserPhoneSMSCodeTable.M_spare2); }
            set { SetData(LubrUserPhoneSMSCodeTable.M_spare2, value); }
        }
        #endregion
    }
}