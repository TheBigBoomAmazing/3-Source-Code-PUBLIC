using Suzsoft.Smart.EntityCore;
using eContract.BusinessService.Common;
using eContract.Common;
using eContract.Common.Entity;
using eContract.Common.MVC;
using eContract.Common.WebUtils;
using eContract.BusinessService.BusinessData.CommonQuery;
using eContract.BusinessService.BusinessData.Service;
using eContract.BusinessService.SystemManagement.Service;
using System;
using System.Text;
using Suzsoft.Smart.Data;
using System.Data;
using System.Linq;

namespace eContract.BusinessService.BusinessData.BusinessRule
{
    public class ContractTemplateBLL : BusinessBase
    {
        public JqGrid ForGrid(JqGrid grid)
        {
            var query = new ContractTemplateQuery();
            grid = QueryTableHelper.QueryGrid<CasContractTemplateEntity>(query, grid);
            return grid;
        }
        public JqGrid DownloadContractTep(JqGrid grid)
        {
            var query = new ContractTemplateDownloadQuery();
            grid = QueryTableHelper.QueryGrid<CasContractTemplateEntity>(query, grid);
            return grid;
        }

        public JqGrid GetContractAttachmentList(JqGrid grid)
        {
            var query = new ContractTemplateAttachmentQuery();
            //query.id = grid.QueryField["id"].ToString();  //做什么用的？
            grid = QueryTableHelper.QueryGrid<CasAttachmentEntity>(query,grid);
            return grid;
        }
        public virtual CasContractTemplateEntity CreateContractTemplateEntity(string systemName = "MDM")
        {
            return new CasContractTemplateEntity();
        }

        public virtual bool SaveContractTemplate(CasContractTemplateEntity contractTemplateEntity, ref string strError)
        {
            contractTemplateEntity.LastModifiedBy = WebCaching.UserAccount;
            contractTemplateEntity.LastModifiedTime = DateTime.Now;
            try
            {
                var isExist = IsExist(contractTemplateEntity, ref strError);
                if (isExist)
                {
                    //更新
                    if (Update(contractTemplateEntity))
                    {
                        SaveContractTemplateAttachment(contractTemplateEntity);
                        return true;
                    }
                }
                else
                {
                    //新增
                    contractTemplateEntity.ContractTemplateId = Guid.NewGuid().ToString();
                    contractTemplateEntity.CreatedBy = WebCaching.UserAccount;
                    contractTemplateEntity.CreateTime = DateTime.Now;
                    if (Insert(contractTemplateEntity))
                    {
                        SaveContractTemplateAttachment(contractTemplateEntity);
                        return true;
                    }
                }
            }
            catch (Exception exception)
            {
                strError = exception.Message;
            }
            return false;
        }
        public virtual bool IsExist(CasContractTemplateEntity contractTemplateEntity, ref string strError)
        {
            if (contractTemplateEntity.ContractTemplateId == null) return false;
            var strsql = new StringBuilder();
            strsql.AppendFormat("SELECT 1 FROM CAS_CONTRACT_TEMPLATE WHERE CONTRACT_TEMPLATE_ID = {0}",
                Utils.ToSQLStr(contractTemplateEntity.ContractTemplateId).Trim());
            var val = DataAccess.SelectScalar(strsql.ToString());
            if (string.IsNullOrEmpty(val) || val != "1") return false;
            strError = "该模板合同已经存在";
            return true;
        }

        public virtual bool DeletecontractTemplate(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return false;
            var strsql = new StringBuilder();
            strsql.AppendFormat("DELETE FROM CAS_CONTRACT_TEMPLATE WHERE CONTRACT_TEMPLATE_ID= {0}",
                Utils.ToSQLStr(id).Trim());
            var val = DataAccess.SelectScalar(strsql.ToString());
            return true;
        }
        /// <summary>
        /// 模板合同附件
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public string GetUploadFiles(string contractId)
        {
            var strsql = new StringBuilder();
            if (string.IsNullOrWhiteSpace(contractId))
            {
                contractId = "1";
            }
            strsql.AppendFormat(" SELECT ATTACHMENT_ID FROM CAS_ATTACHMENT WHERE SOURCE_ID='{0}' ", contractId);
            DataSet userDataSet = DataAccess.SelectDataSet(strsql.ToString());
            string[] userAry = Array.ConvertAll<DataRow, string>(userDataSet.Tables[0].Rows.Cast<DataRow>().ToArray(), r => r["ATTACHMENT_ID"].ToString());
            string sqlFileids = "";
            for (int i = 0; i < userAry.Length; i++)
            {
                sqlFileids = sqlFileids + userAry[i] + ",";
            }
            if (sqlFileids.Length > 1)
            {
                sqlFileids = sqlFileids.Substring(0, sqlFileids.Length - 1);
            }
            return sqlFileids;
        }

        /// <summary>
        /// 保存模板合同的附件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool SaveContractTemplateAttachment(CasContractTemplateEntity entity)
        {
            if (!string.IsNullOrWhiteSpace(entity.fileIds))
            {
                string[] filesids = entity.fileIds.ToString().Split(',');
                string sqlFileids = "";
                for (int i = 0; i < filesids.Length; i++) //这个FOR循环就是加单引号
                {
                    filesids[i] = "'" + filesids[i] + "'";
                    sqlFileids = sqlFileids + filesids[i] + ",";
                }
                sqlFileids = sqlFileids.Substring(0, sqlFileids.Length - 1);
                var strsql = new StringBuilder();
                strsql.AppendFormat("UPDATE CAS_ATTACHMENT SET SOURCE_ID='{0}' WHERE ATTACHMENT_ID IN ({1})",
                   entity.ContractTemplateId, sqlFileids);
                var val = DataAccess.ExecuteNoneQuery(strsql.ToString());
                if (val <= 0)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
    }
}
