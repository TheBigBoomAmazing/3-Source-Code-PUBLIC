using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Suzsoft.Smart.Data;
using Suzsoft.Smart.EntityCore;
using eContract.BusinessService.Common;
using eContract.Common;
using eContract.Common.Entity;
using eContract.Common.MVC;
using eContract.Common.WebUtils;
using eContract.BusinessService.BusinessData.CommonQuery;
using eContract.BusinessService.BusinessData.Service;
using eContract.BusinessService.SystemManagement.Service;

namespace eContract.BusinessService.BusinessData.BusinessRule
{
    public class POBLL : BusinessBase
    {
        public JqGrid ForPRIndexGrid(JqGrid grid)
        {
            var query = new PRIndexQuery();
            query.CONTRACT_TYPE_NAME = grid.QueryField.ContainsKey("CONTRACT_TYPE_NAME") ? grid.QueryField["CONTRACT_TYPE_NAME"] : "";
            grid.QueryField.Remove("CONTRACT_TYPE_NAME");

            grid = QueryTableHelper.QueryGrid<CasContractEntity>(query, grid);
            return grid;
        }
        public JqGrid ForPOIndexGrid(JqGrid grid)
        {
            var query = new POIndexQuery();
            query.CONTRACT_TYPE_NAME = grid.QueryField.ContainsKey("CONTRACT_TYPE_NAME") ? grid.QueryField["CONTRACT_TYPE_NAME"] : "";
            grid.QueryField.Remove("CONTRACT_TYPE_NAME");
            grid = QueryTableHelper.QueryGrid<CasContractEntity>(query, grid);
            return grid;
        }
        public JqGrid ForPRGrid(JqGrid grid)
        {
            var query = new PRQuery();
            query.keyword = grid.keyWord;
            grid = QueryTableHelper.QueryGrid<CasContractFilingEntity>(query, grid);
            return grid;
        }
        public JqGrid ForPOGrid(JqGrid grid)
        {
            var query = new POQuery();
            query.keyword = grid.keyWord;
            grid = QueryTableHelper.QueryGrid<CasContractFilingEntity>(query, grid);
            return grid;
        }

        public bool DeletePRByIds(string deleteKeys, ref string strError)
        {
            List<string> listKeys = deleteKeys.Split(',').ToList();
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                try
                {
                    List<CasContractFilingEntity> listContractFilings = new List<CasContractFilingEntity>();
                    foreach (string id in listKeys)
                    {
                        listContractFilings.Add(GetById<CasContractFilingEntity>(id));
                    }
                    broker.BeginTransaction();
                    DataAccess.Delete<CasContractFilingEntity>(listContractFilings, broker);
                    broker.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    broker.RollBack();
                    return false;
                }
            }
        }

        public bool SavePR(string contractFilingId, string contractId, string pRNo, ref string strError)
        {
            CasContractFilingEntity casContractFilingEntity = null;
            if (!string.IsNullOrEmpty(contractFilingId))
            {
                casContractFilingEntity = GetById<CasContractFilingEntity>(contractFilingId);
                if (casContractFilingEntity == null || string.IsNullOrEmpty(contractFilingId))
                {
                    strError = "PR不存在";
                    return false;
                }
                casContractFilingEntity.PrNo = pRNo;
                casContractFilingEntity.PoNo = null;
                casContractFilingEntity.Status = ContractFilingEnum.Save.GetHashCode();
                casContractFilingEntity.LastModifiedTime = DateTime.Now;
                casContractFilingEntity.LastModifiedBy = WebCaching.UserId;
                return Update(casContractFilingEntity);
            }
            else
            {
                casContractFilingEntity = new CasContractFilingEntity();
                casContractFilingEntity.ContractFilingId = Guid.NewGuid().ToString();
                casContractFilingEntity.ContractId = contractId;
                casContractFilingEntity.Remark = null;
                casContractFilingEntity.IsDeleted = false;
                casContractFilingEntity.CreateTime = DateTime.Now;
                casContractFilingEntity.CreatedBy = WebCaching.UserId;
                casContractFilingEntity.PrNo = pRNo;
                casContractFilingEntity.PoNo = null;
                casContractFilingEntity.Status = ContractFilingEnum.Save.GetHashCode();
                casContractFilingEntity.LastModifiedTime = DateTime.Now;
                casContractFilingEntity.LastModifiedBy = WebCaching.UserId;
                return Insert(casContractFilingEntity);
            }
        }
        public bool SubmitPR(string contractFilingId, string contractId, string pRNo, ref string strError)
        {
            CasContractFilingEntity casContractFilingEntity = null;
            if (!string.IsNullOrEmpty(contractFilingId))
            {
                casContractFilingEntity = GetById<CasContractFilingEntity>(contractFilingId);
                if (casContractFilingEntity == null || string.IsNullOrEmpty(contractFilingId))
                {
                    strError = "PR不存在";
                    return false;
                }
                casContractFilingEntity.PrNo = pRNo;
                casContractFilingEntity.PoNo = null;
                casContractFilingEntity.Status = ContractFilingEnum.Apply.GetHashCode();
                casContractFilingEntity.LastModifiedTime = DateTime.Now;
                casContractFilingEntity.LastModifiedBy = WebCaching.UserId;
                return Update(casContractFilingEntity);
            }
            else
            {
                casContractFilingEntity = new CasContractFilingEntity();
                casContractFilingEntity.ContractFilingId = Guid.NewGuid().ToString();
                casContractFilingEntity.ContractId = contractId;
                casContractFilingEntity.Remark = null;
                casContractFilingEntity.IsDeleted = false;
                casContractFilingEntity.CreateTime = DateTime.Now;
                casContractFilingEntity.CreatedBy = WebCaching.UserId;
                casContractFilingEntity.PrNo = pRNo;
                casContractFilingEntity.PoNo = null;
                casContractFilingEntity.Status = ContractFilingEnum.Apply.GetHashCode();
                casContractFilingEntity.LastModifiedTime = DateTime.Now;
                casContractFilingEntity.LastModifiedBy = WebCaching.UserId;
                return Insert(casContractFilingEntity);
            }
        }

        public bool POSave(string contractFilingId, string pONo, ref string strError)
        {
            CasContractFilingEntity casContractFilingEntity = GetById<CasContractFilingEntity>(contractFilingId);
            if (casContractFilingEntity == null || string.IsNullOrEmpty(contractFilingId))
            {
                strError = "PR不存在";
                return false;
            }
            casContractFilingEntity.PoNo = pONo;
            casContractFilingEntity.Status = ContractFilingEnum.POSave.GetHashCode();
            casContractFilingEntity.LastModifiedTime = DateTime.Now;
            casContractFilingEntity.LastModifiedBy = WebCaching.UserId;
            return Update(casContractFilingEntity);
        }

        public bool POAprove(string contractFilingId, string pONo, ref string strError)
        {
            CasContractFilingEntity casContractFilingEntity = GetById<CasContractFilingEntity>(contractFilingId);
            if (casContractFilingEntity == null || string.IsNullOrEmpty(contractFilingId))
            {
                strError = "PR不存在";
                return false;
            }
            casContractFilingEntity.PoNo = pONo;
            casContractFilingEntity.Status = ContractFilingEnum.Approve.GetHashCode();
            casContractFilingEntity.LastModifiedTime = DateTime.Now;
            casContractFilingEntity.LastModifiedBy = WebCaching.UserId;
            return Update(casContractFilingEntity);
        }

        public bool POReject(string contractFilingId, string remark, ref string strError)
        {
            CasContractFilingEntity casContractFilingEntity = GetById<CasContractFilingEntity>(contractFilingId);
            CasUserEntity userEntity = GetById<CasUserEntity>(casContractFilingEntity.CreatedBy);
            CasContractEntity contractEntity = GetById<CasContractEntity>(casContractFilingEntity.ContractId);
            if (casContractFilingEntity == null || string.IsNullOrEmpty(contractFilingId))
            {
                strError = "PR不存在";
                return false;
            }
            casContractFilingEntity.Remark = remark;
            casContractFilingEntity.Status = ContractFilingEnum.Reject.GetHashCode();
            casContractFilingEntity.LastModifiedTime = DateTime.Now;
            casContractFilingEntity.LastModifiedBy = WebCaching.UserId;

            #region PO审批驳回时候发邮件通知
            //var contractEntity = GetById<CasContractEntity>(casContractFilingEntity.ContractId);
            var title = $@"e-Approval – Contract PR Rejected";
            var content = $@"Dear:{userEntity.EnglishName},</br>尊敬的：{userEntity.ChineseName}</br></br> The following PR is rejected:</br>以下PR被拒绝：</br></br>  Contract Name 合同名称:{contractEntity.ContractName}    {contractEntity.TemplateName}</br>Ferrero Entity  费列罗方:{contractEntity.FerreroEntity}</br> Counter Party  相对方:{contractEntity.CounterpartyEn}  {contractEntity.CounterpartyCn}</br> PR No.  PR号码:{casContractFilingEntity.PrNo} </br>Reject Reason 拒绝理由:{remark}</br></br>Please review your PR and resubmit the request.</br>请检查PR相关内容并重新提交。</br></br>Ferrero China Contract Approval System</br> 费列罗中国合同审批系统";
            var cc = "chinacontractsys@ferrero.com.cn";
            var reciever = GetUserEmail(casContractFilingEntity.CreatedBy);
            SendEmail.Send(reciever,cc, title, content);
            #endregion
            return Update(casContractFilingEntity);
        }
        /// <summary>
        /// 获得用户的邮箱地址
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetUserEmail(string userId)
        {
            string sql = $@"SELECT EMAIL FROM dbo.CAS_USER WHERE USER_ID='{userId}'";
            return DataAccess.SelectScalar(sql);
        }
        public List<CasUserEntity> GetAllHaveApplyPOUsers()
        {
            string sql = $@"SELECT D.*
                              FROM CAS_CONTRACT_FILING A 
                        INNER JOIN CAS_CONTRACT B
                                ON A.CONTRACT_ID = B.CONTRACT_ID
                        INNER JOIN CAS_PO_APPROVAL_SETTINGS C
                                ON B.CONTRACT_TYPE_ID = C.CONTRACT_TYPE_ID 
                        INNER JOIN CAS_USER D
                                ON C.USER_ID = D.USER_ID
                             WHERE A.STATUS = {ContractFilingEnum.Apply.GetHashCode()} ";
            return DataAccess.Select<CasUserEntity>(sql);
        }

        public List<CasContractFilingEntity> GetAllApplyPOs(string userId)
        {
            string sql = $@"SELECT A.* 
                              FROM CAS_CONTRACT_FILING A 
                        INNER JOIN CAS_CONTRACT B
                                ON A.CONTRACT_ID = B.CONTRACT_ID
                        INNER JOIN CAS_PO_APPROVAL_SETTINGS C
                                ON B.CONTRACT_TYPE_ID = C.CONTRACT_TYPE_ID 
                             WHERE A.STATUS = {ContractFilingEnum.Apply.GetHashCode()}
                               AND C.USER_ID = '{userId}'";
            return DataAccess.Select<CasContractFilingEntity>(sql);
        }
    }
}
