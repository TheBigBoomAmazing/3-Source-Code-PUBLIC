using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Schema;

namespace eContract.Common.Entity
{
    [Serializable]
    public partial class CasProxyApprovalEntity : EntityBase
    {
        public CasProxyApprovalTable TableSchema
        {
            get
            {
                return CasProxyApprovalTable.Current;
            }
        }


        public CasProxyApprovalEntity()
        {
            IsDeleted = false;
        }

        public override TableInfo OringTableSchema
        {
            get
            {
                return CasProxyApprovalTable.Current;
            }
        }
        #region 属性列表

        public string ProxyApprovalId
        {
            get { return (string)GetData(CasProxyApprovalTable.C_PROXY_APPROVAL_ID); }
            set { SetData(CasProxyApprovalTable.C_PROXY_APPROVAL_ID, value); }
        }

        public string AuthorizedUserId
        {
            get { return (string)GetData(CasProxyApprovalTable.C_AUTHORIZED_USER_ID); }
            set { SetData(CasProxyApprovalTable.C_AUTHORIZED_USER_ID, value); }
        }

        public string AgentUserId
        {
            get { return (string)GetData(CasProxyApprovalTable.C_AGENT_USER_ID); }
            set { SetData(CasProxyApprovalTable.C_AGENT_USER_ID, value); }
        }

        public DateTime? BeginTime
        {
            get { return (DateTime?)(GetData(CasProxyApprovalTable.C_BEGIN_TIME)); }
            set { SetData(CasProxyApprovalTable.C_BEGIN_TIME, value); }
        }

        public DateTime? EndTime
        {
            get { return (DateTime?)(GetData(CasProxyApprovalTable.C_END_TIME)); }
            set { SetData(CasProxyApprovalTable.C_END_TIME, value); }
        }

        public bool? IsDeleted
        {
            get { return (bool?)(GetData(CasProxyApprovalTable.C_IS_DELETED)); }
            set { SetData(CasProxyApprovalTable.C_IS_DELETED, value); }
        }

        public string CreatedBy
        {
            get { return (string)GetData(CasProxyApprovalTable.C_CREATED_BY); }
            set { SetData(CasProxyApprovalTable.C_CREATED_BY, value); }
        }

        public DateTime? CreateTime
        {
            get { return (DateTime?)(GetData(CasProxyApprovalTable.C_CREATE_TIME)); }
            set { SetData(CasProxyApprovalTable.C_CREATE_TIME, value); }
        }

        public string LastModifiedBy
        {
            get { return (string)GetData(CasProxyApprovalTable.C_LAST_MODIFIED_BY); }
            set { SetData(CasProxyApprovalTable.C_LAST_MODIFIED_BY, value); }
        }

        public DateTime? LastModifiedTime
        {
            get { return (DateTime?)(GetData(CasProxyApprovalTable.C_LAST_MODIFIED_TIME)); }
            set { SetData(CasProxyApprovalTable.C_LAST_MODIFIED_TIME, value); }
        }

        public DateTime? TerminationTime
        {
            get { return (DateTime?)(GetData(CasProxyApprovalTable.C_TERMINATION_TIME)); }
            set { SetData(CasProxyApprovalTable.C_TERMINATION_TIME, value); }
        }


        #endregion
    }
}
