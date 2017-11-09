using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Schema;

namespace eContract.Common.Entity
{
    [Serializable]
    public partial class SecLogErrorEntity : EntityBase
    {
        public SecLogErrorTable TableSchema
        {
            get
            {
                return SecLogErrorTable.Current;
            }
        }


        public SecLogErrorEntity()
        {
            IsDeleted = false;
        }

        public override TableInfo OringTableSchema
        {
            get
            {
                return SecLogErrorTable.Current;
            }
        }
        #region 属性列表

        public string LogErrorId
        {
            get { return (string)GetData(SecLogErrorTable.C_LOG_ERROR_ID); }
            set { SetData(SecLogErrorTable.C_LOG_ERROR_ID, value); }
        }

        public string Message
        {
            get { return (string)GetData(SecLogErrorTable.C_MESSAGE); }
            set { SetData(SecLogErrorTable.C_MESSAGE, value); }
        }

        public string StackTrace
        {
            get { return (string)GetData(SecLogErrorTable.C_STACK_TRACE); }
            set { SetData(SecLogErrorTable.C_STACK_TRACE, value); }
        }

        public string MachineName
        {
            get { return (string)GetData(SecLogErrorTable.C_MACHINE_NAME); }
            set { SetData(SecLogErrorTable.C_MACHINE_NAME, value); }
        }

        public string Ip
        {
            get { return (string)GetData(SecLogErrorTable.C_IP); }
            set { SetData(SecLogErrorTable.C_IP, value); }
        }

        public DateTime? LogTime
        {
            get { return (DateTime?)(GetData(SecLogErrorTable.C_LOG_TIME)); }
            set { SetData(SecLogErrorTable.C_LOG_TIME, value); }
        }

        public string Pagename
        {
            get { return (string)GetData(SecLogErrorTable.C_PAGENAME); }
            set { SetData(SecLogErrorTable.C_PAGENAME, value); }
        }

        public bool? IsDeleted
        {
            get { return (bool?)(GetData(SecLogErrorTable.C_IS_DELETED)); }
            set { SetData(SecLogErrorTable.C_IS_DELETED, value); }
        }

        public string CreatedBy
        {
            get { return (string)GetData(SecLogErrorTable.C_CREATED_BY); }
            set { SetData(SecLogErrorTable.C_CREATED_BY, value); }
        }

        public DateTime? CreateTime
        {
            get { return (DateTime?)(GetData(SecLogErrorTable.C_CREATE_TIME)); }
            set { SetData(SecLogErrorTable.C_CREATE_TIME, value); }
        }

        public string LastModifiedBy
        {
            get { return (string)GetData(SecLogErrorTable.C_LAST_MODIFIED_BY); }
            set { SetData(SecLogErrorTable.C_LAST_MODIFIED_BY, value); }
        }

        public DateTime? LastModifiedTime
        {
            get { return (DateTime?)(GetData(SecLogErrorTable.C_LAST_MODIFIED_TIME)); }
            set { SetData(SecLogErrorTable.C_LAST_MODIFIED_TIME, value); }
        }

        #endregion
    }
}
