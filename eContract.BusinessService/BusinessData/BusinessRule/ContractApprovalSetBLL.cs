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
using System.Web.Script.Serialization;

namespace eContract.BusinessService.BusinessData.BusinessRule
{
    public class ContractApprovalSetBLL : BusinessBase
    {
        public JqGrid ForGrid(JqGrid grid)
        {
            var query = new ContractApprovalSetQuery();
            query.keyWord = grid.keyWord;
            query.keyWord = grid.QueryField.ContainsKey("CONTRACT_TYPE_NAME") ? grid.QueryField["CONTRACT_TYPE_NAME"] : "";
            grid.QueryField.Remove("CONTRACT_TYPE_NAME");
            grid = QueryTableHelper.QueryGrid<CasContractApprovalStepEntity>(query, grid);
            return grid;
        }

        public virtual CasContractApprovalStepEntity CreateContractApprovalSetEntity(string systemName = "MDM")
        {
            return new CasContractApprovalStepEntity();
        }

        public virtual bool SaveContractApprovalSetEntity(CasContractApprovalStepEntity contractApprovalSetEntity, ref string strError)
        {
            contractApprovalSetEntity.LastModifiedBy = WebCaching.UserAccount;
            contractApprovalSetEntity.LastModifiedTime = DateTime.Now;
            try
            {
                //var isExist = IsExist(contractApprovalSetEntity, ref strError);
                if (!string.IsNullOrEmpty(contractApprovalSetEntity.ContractApprovalStepId))
                {
                    //更新
                    if (UpdateApprovalEntites(contractApprovalSetEntity))
                    {
                        return true;
                    }
                }
                else
                {
                    //var stepExist = IsExistStep(contractApprovalSetEntity, ref strError);
                    var applierExist = IsExistApplier(contractApprovalSetEntity, ref strError);
                    if (!applierExist)
                    {
                        //新增
                        contractApprovalSetEntity.ContractApprovalStepId = Guid.NewGuid().ToString();
                        contractApprovalSetEntity.CreatedBy = WebCaching.UserAccount;
                        contractApprovalSetEntity.CreateTime = DateTime.Now;
                        contractApprovalSetEntity.IsDeleted = false;
                        //if (Insert(contractApprovalSetEntity))
                        if (InsertApprovalEntites(contractApprovalSetEntity))
                        {
                            return true;
                        }
                    }

                }
            }
            catch (Exception exception)
            {
                strError = exception.Message;
            }
            return false;
        }
        /// <summary>
        /// 合同审批设置查询审批用户
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public JqGrid QueryApprovalUser(JqGrid grid)
        {
            var query = new ContractApprovalUserQuery();
            //query.keyWord = grid.keyWord;
            query.keyWord = grid.QueryField.ContainsKey("appSetid") ? grid.QueryField["appSetid"] : "";
            grid.QueryField.Remove("appSetid");
            grid = QueryTableHelper.QueryGrid<CasUserEntity>(query, grid);
            return grid;
        }



        ///// <summary>
        ///// 判断申请步骤是否重复
        ///// </summary>
        ///// <param name="contractApprovalSetEntity"></param>
        ///// <param name="strError"></param>
        ///// <returns></returns>
        //public bool IsExistStep(CasContractApprovalStepEntity contractApprovalSetEntity, ref string strError)
        //{
        //    var strsql = new StringBuilder();
        //    strsql.AppendFormat("SELECT 1 FROM CAS_CONTRACT_APPROVAL_STEP WHERE CONTRACT_TYPE_ID= {0} AND STEP='{1}'",
        //        Utils.ToSQLStr(contractApprovalSetEntity.ContractTypeId).Trim(), contractApprovalSetEntity.Step);
        //    var val = DataAccess.SelectScalar(strsql.ToString());
        //    if (string.IsNullOrEmpty(val) || val != "1") return false;
        //    strError = "该合同类型的该审批步骤已经存在";
        //    return true;
        //}


        /// <summary>
        /// 判断申请人是否重复
        /// </summary>
        /// <param name="contractApprovalSetEntity"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        public bool IsExistApplier(CasContractApprovalStepEntity contractApprovalSetEntity, ref string strError)
        {
            string strSql = "";
            if (contractApprovalSetEntity.ApprovalRole == ApplyTypeEnum.Department.GetHashCode())
            {
                string deptIds = "'" + contractApprovalSetEntity.ApprovalDepValue.Replace(",", "','") + "'";
                strSql = $@"SELECT * FROM 
                                (
                                    SELECT CAS_CONTRACT_APPROVAL_STEP_APPLY_DEPT.DEPT_ID USER_ID
                                      FROM CAS_CONTRACT_APPROVAL_STEP_APPLY_DEPT
                                INNER JOIN CAS_CONTRACT_APPROVAL_STEP
	                                    ON CAS_CONTRACT_APPROVAL_STEP_APPLY_DEPT.CONTRACT_APPROVAL_STEP_ID 
                                           = CAS_CONTRACT_APPROVAL_STEP.CONTRACT_APPROVAL_STEP_ID
                                     WHERE CAS_CONTRACT_APPROVAL_STEP.CONTRACT_TYPE_ID = '{contractApprovalSetEntity.ContractTypeId}'
                                       AND CAS_CONTRACT_APPROVAL_STEP.STEP = {contractApprovalSetEntity.Step}
                                       AND APPLY_TYPE = {ApplyTypeEnum.User.GetHashCode()}
                                UNION
                                    SELECT CAS_DEPT_USER.USER_ID
                                      FROM CAS_CONTRACT_APPROVAL_STEP_APPLY_DEPT
                                INNER JOIN CAS_DEPT_USER ON CAS_CONTRACT_APPROVAL_STEP_APPLY_DEPT.DEPT_ID =CAS_DEPT_USER.DEPT_ID
                                INNER JOIN CAS_CONTRACT_APPROVAL_STEP
	                                    ON CAS_CONTRACT_APPROVAL_STEP_APPLY_DEPT.CONTRACT_APPROVAL_STEP_ID 
                                           = CAS_CONTRACT_APPROVAL_STEP.CONTRACT_APPROVAL_STEP_ID
                                     WHERE CAS_CONTRACT_APPROVAL_STEP.CONTRACT_TYPE_ID = '{contractApprovalSetEntity.ContractTypeId}'
                                       AND CAS_CONTRACT_APPROVAL_STEP.STEP = {contractApprovalSetEntity.Step}
                                       AND APPLY_TYPE = {ApplyTypeEnum.Department.GetHashCode()}
                                ) TEMP
                                WHERE USER_ID IN 
                                (SELECT USER_ID FROM CAS_DEPT_USER WHERE DEPT_ID IN({deptIds}))";
            }
            else if (contractApprovalSetEntity.ApprovalRole == ApplyTypeEnum.User.GetHashCode())
            {
                string userIds = "'" + contractApprovalSetEntity.ApprovalUserValue.Replace(",", "','") + "'";
                strSql = $@"SELECT * FROM 
                                (
                                    SELECT CAS_CONTRACT_APPROVAL_STEP_APPLY_DEPT.DEPT_ID USER_ID
                                      FROM CAS_CONTRACT_APPROVAL_STEP_APPLY_DEPT
                                INNER JOIN CAS_CONTRACT_APPROVAL_STEP
	                                    ON CAS_CONTRACT_APPROVAL_STEP_APPLY_DEPT.CONTRACT_APPROVAL_STEP_ID 
                                           = CAS_CONTRACT_APPROVAL_STEP.CONTRACT_APPROVAL_STEP_ID
                                     WHERE CAS_CONTRACT_APPROVAL_STEP.CONTRACT_TYPE_ID = '{contractApprovalSetEntity.ContractTypeId}'
                                       AND CAS_CONTRACT_APPROVAL_STEP.STEP = {contractApprovalSetEntity.Step}
                                       AND APPLY_TYPE = {ApplyTypeEnum.User.GetHashCode()}
                                UNION
                                    SELECT CAS_DEPT_USER.USER_ID
                                      FROM CAS_CONTRACT_APPROVAL_STEP_APPLY_DEPT
                                INNER JOIN CAS_DEPT_USER ON CAS_CONTRACT_APPROVAL_STEP_APPLY_DEPT.DEPT_ID =CAS_DEPT_USER.DEPT_ID
                                INNER JOIN CAS_CONTRACT_APPROVAL_STEP
	                                    ON CAS_CONTRACT_APPROVAL_STEP_APPLY_DEPT.CONTRACT_APPROVAL_STEP_ID 
                                           = CAS_CONTRACT_APPROVAL_STEP.CONTRACT_APPROVAL_STEP_ID
                                     WHERE CAS_CONTRACT_APPROVAL_STEP.CONTRACT_TYPE_ID = '{contractApprovalSetEntity.ContractTypeId}'
                                       AND CAS_CONTRACT_APPROVAL_STEP.STEP = {contractApprovalSetEntity.Step}
                                       AND APPLY_TYPE = {ApplyTypeEnum.Department.GetHashCode()}
                                ) TEMP
                                WHERE USER_ID IN ({userIds})";
            }
            else
            {
                strSql = $@"SELECT * FROM CAS_CONTRACT_APPROVAL_STEP_APPLY_DEPT
                               INNER JOIN CAS_CONTRACT_APPROVAL_STEP
	                                   ON CAS_CONTRACT_APPROVAL_STEP_APPLY_DEPT.CONTRACT_APPROVAL_STEP_ID 
                                          = CAS_CONTRACT_APPROVAL_STEP.CONTRACT_APPROVAL_STEP_ID
                                    WHERE CAS_CONTRACT_APPROVAL_STEP.CONTRACT_TYPE_ID = '{contractApprovalSetEntity.ContractTypeId}'
                                      AND CAS_CONTRACT_APPROVAL_STEP.STEP = {contractApprovalSetEntity.Step}";
            }
            var val = DataAccess.SelectScalar(strSql.ToString());
            if (!string.IsNullOrEmpty(val))
            {
                strError = "该合同类型的申请部门和已有申请部门重复";
                return true;
            }
            return false;
        }
        /// <summary>
        /// 判断合同类型是否有审批中的合同
        /// </summary>
        /// <param name="approvalStepId"></param>
        /// <returns>没有返回true，存在返回false</returns>
        public bool CheckInProcessContract(string approvalStepId)
        {
            var strsql =new StringBuilder();
            strsql.AppendFormat("SELECT * FROM CAS_CONTRACT CON LEFT JOIN dbo.CAS_CONTRACT_APPROVAL_STEP CAS ON CON.CONTRACT_TYPE_ID=CAS.CONTRACT_TYPE_ID WHERE CAS.CONTRACT_APPROVAL_STEP_ID = {0} AND CON.STATUS  IN('1', '2','4', '8')", Utils.ToSQLStr(approvalStepId).Trim());
            var val = DataAccess.SelectScalar(strsql.ToString());
            if (string.IsNullOrEmpty(val))
            {
                return true;
            }
            else {
                return false;
            }
        }

        /// <summary>
        /// 更新合同审批
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateApprovalEntites(CasContractApprovalStepEntity entity)
        {
            if (string.IsNullOrWhiteSpace(entity.ContractApprovalStepId)) return false;
            var strsql = new StringBuilder();
            strsql.AppendFormat("DELETE FROM CAS_CONTRACT_APPROVAL_STEP_APPROVAL_DEPT WHERE CONTRACT_APPROVAL_STEP_ID= {0}",
                Utils.ToSQLStr(entity.ContractApprovalStepId).Trim());
            var val = DataAccess.SelectScalar(strsql.ToString());
            var approvalEntites = new List<CasContractApprovalStepApprovalDeptEntity>();
            string[] approvalD = new string[] { };
            if (!string.IsNullOrWhiteSpace(entity.ExaminationValue))
            {
                approvalD=entity.ExaminationValue.ToString().Split(',');
            }
            for (int j = 0; j < approvalD.Length; j++)
            {
                var approvalEntity = new CasContractApprovalStepApprovalDeptEntity();
                approvalEntity.ContractApprovalStepApprovalDeptId = Guid.NewGuid().ToString();
                approvalEntity.ContractApprovalStepId = entity.ContractApprovalStepId;
                approvalEntity.DeptId = approvalD[j];
                approvalEntity.CreatedBy = WebCaching.UserId;
                approvalEntity.CreateTime = DateTime.Now;
                approvalEntity.LastModifiedBy = WebCaching.UserId;
                approvalEntity.LastModifiedTime = DateTime.Now;
                approvalEntites.Add(approvalEntity);
            }
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                try
                {
                    broker.BeginTransaction();
                    DataAccess.Insert<CasContractApprovalStepApprovalDeptEntity>(approvalEntites, broker);
                    DataAccess.Update(entity, broker);
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


        /// <summary>
        /// 保存新增数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool InsertApprovalEntites(CasContractApprovalStepEntity entity)
        {
            var applyEntites = new List<CasContractApprovalStepApplyDeptEntity>();
            var approvalEntites = new List<CasContractApprovalStepApprovalDeptEntity>();
            string[] applyID;
            if (entity.ApprovalRole == ApplyTypeEnum.AllDepartment.GetHashCode())
            {
                applyID = GetAllDepartmentID();
                entity.ApprovalRole = ApplyTypeEnum.Department.GetHashCode();
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(entity.ApprovalDepValue) && entity.ApprovalRole == ApplyTypeEnum.Department.GetHashCode())
                {
                    applyID = entity.ApprovalDepValue.ToString().Split(',');
                }
                else
                {
                    applyID = entity.ApprovalUserValue.ToString().Split(',');
                }
            }
            string[] approvalD = new string[] { };
            if (!string.IsNullOrWhiteSpace(entity.ExaminationValue))
            {
                approvalD = entity.ExaminationValue.ToString().Split(',');
            }
            for (int i = 0; i < applyID.Length; i++)
            {
                var applyEntity = new CasContractApprovalStepApplyDeptEntity();
                applyEntity.ContractApprovalStepApplyDeptId = Guid.NewGuid().ToString();
                applyEntity.ContractApprovalStepId = entity.ContractApprovalStepId;
                applyEntity.ApplyType = (int)entity.ApprovalRole;
                applyEntity.DeptId = applyID[i];
                applyEntity.CreatedBy = WebCaching.UserId;
                applyEntity.CreateTime = DateTime.Now;
                applyEntity.LastModifiedBy = WebCaching.UserId;
                applyEntity.LastModifiedTime = DateTime.Now;
                applyEntites.Add(applyEntity);
            }
            for (int j = 0; j < approvalD.Length; j++)
            {
                var approvalEntity = new CasContractApprovalStepApprovalDeptEntity();
                approvalEntity.ContractApprovalStepApprovalDeptId = Guid.NewGuid().ToString();
                approvalEntity.ContractApprovalStepId = entity.ContractApprovalStepId;
                approvalEntity.DeptId = approvalD[j];
                approvalEntity.CreatedBy = WebCaching.UserId;
                approvalEntity.CreateTime = DateTime.Now;
                approvalEntity.LastModifiedBy = WebCaching.UserId;
                approvalEntity.LastModifiedTime = DateTime.Now;
                approvalEntites.Add(approvalEntity);
            }

            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                try
                {
                    broker.BeginTransaction();
                    DataAccess.Insert<CasContractApprovalStepApplyDeptEntity>(applyEntites, broker);
                    DataAccess.Insert<CasContractApprovalStepApprovalDeptEntity>(approvalEntites, broker);
                    DataAccess.Insert(entity, broker);
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
        /// <summary>
        /// 获得素有的部门ID
        /// </summary>
        /// <returns></returns>
        public string[] GetAllDepartmentID()
        {
            var strsql = new StringBuilder();
            strsql.AppendFormat(" SELECT DEPT_ID FROM CAS_DEPARTMENT WHERE DEPT_TYPE='1' ");
            DataSet dataSet = DataAccess.SelectDataSet(strsql.ToString());
            string[] ary = Array.ConvertAll<DataRow, string>(dataSet.Tables[0].Rows.Cast<DataRow>().ToArray(), r => r["DEPT_ID"].ToString());
            return ary;
        }

        ///// <summary>
        ///// 判断是否有重复
        ///// </summary>
        ///// <param name="contractApprovalSetEntity"></param>
        ///// <param name="strError"></param>
        ///// <returns></returns>
        //public virtual bool IsExist(CasContractApprovalStepEntity contractApprovalSetEntity, ref string strError)
        //{
        //    if (contractApprovalSetEntity.ContractApprovalStepId == null) return false;
        //    var strsql = new StringBuilder();
        //    strsql.AppendFormat("SELECT 1 FROM CAS_CONTRACT_APPROVAL_STEP WHERE CONTRACT_APPROVAL_STEP_ID = {0}",
        //        Utils.ToSQLStr(contractApprovalSetEntity.ContractApprovalStepId).Trim());
        //    var val = DataAccess.SelectScalar(strsql.ToString());
        //    if (string.IsNullOrEmpty(val) || val != "1") return false;
        //    strError = "该合同审批设置已经存在";
        //    return true;
        //}

        /// <summary>
        /// 选择存在合同类型的用户
        /// </summary>
        /// <param name="ContractTpeId"></param>
        /// <returns></returns>
        public string GetContractTypeApplyUser(string ContractTpeId, int? role)
        {
            var strsql = new StringBuilder();            
            strsql.AppendFormat(" SELECT  DISTINCT( T2.DEPT_ID) FROM CAS_CONTRACT_APPROVAL_STEP T1 INNER JOIN CAS_CONTRACT_APPROVAL_STEP_APPLY_DEPT T2 ON T1.CONTRACT_APPROVAL_STEP_ID = T2.CONTRACT_APPROVAL_STEP_ID WHERE T1.CONTRACT_TYPE_ID = '{0}' AND T1.APPROVAL_ROLE='{1}' ", ContractTpeId, role);
            DataSet userDataSet = DataAccess.SelectDataSet(strsql.ToString());
            string[] userAry = Array.ConvertAll<DataRow, string>(userDataSet.Tables[0].Rows.Cast<DataRow>().ToArray(), r => r["DEPT_ID"].ToString());
            string userString = UserOrDep(userAry, role);
            return userString;
        }
        /// <summary>
        /// 根据合同类型ID获得已经存在的审批步骤
        /// </summary>
        /// <param name="contractTypeId">合同类型ID</param>
        /// <returns></returns>
        public string GetExistApprovalStepByContractTypeId(string contractTypeId)
        {
            var strsql = new StringBuilder();
            strsql.AppendFormat(" SELECT * FROM CAS_CONTRACT_APPROVAL_STEP WHERE CONTRACT_TYPE_ID='{0}'", contractTypeId);
            DataSet userDataSet = DataAccess.SelectDataSet(strsql.ToString());
            string steps = "";
            if (userDataSet.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < userDataSet.Tables[0].Rows.Count; i++)
                {
                    steps = steps + userDataSet.Tables[0].Rows[i]["STEP"].ToString() + ",";
                }
            }
            if (!string.IsNullOrWhiteSpace(steps))
            {
                steps = steps.Substring(0, steps.Length - 1);
            }
            //string[] userAry = Array.ConvertAll<DataRow, string>(userDataSet.Tables[0].Rows.Cast<DataRow>().ToArray(), r => r["STEP"].ToString());
            return steps;
        }


        public string GetContractTypeApprovalDep(string ApprovalStepId, string ContractTpeId, int? role)
        {
            var strsql = new StringBuilder();
            strsql.AppendFormat(" SELECT T2.DEPT_ID FROM CAS_CONTRACT_APPROVAL_STEP T1 INNER JOIN  CAS_CONTRACT_APPROVAL_STEP_APPROVAL_DEPT T2 ON T1.CONTRACT_APPROVAL_STEP_ID=T2.CONTRACT_APPROVAL_STEP_ID WHERE T1.CONTRACT_TYPE_ID='{0}' AND T1.CONTRACT_APPROVAL_STEP_ID ='{1}'", ContractTpeId, ApprovalStepId);
            DataSet userDataSet = DataAccess.SelectDataSet(strsql.ToString());
            string[] userAry = Array.ConvertAll<DataRow, string>(userDataSet.Tables[0].Rows.Cast<DataRow>().ToArray(), r => r["DEPT_ID"].ToString());
            string userString = UserOrDep(userAry, 1);
            return userString;
        }

        public DataTable GetContractTypeRole(string contractTypeId)
        {
            var strsql = new StringBuilder();
            strsql.AppendFormat(" SELECT APPROVAL_ROLE FROM CAS_CONTRACT_APPROVAL_STEP WHERE CONTRACT_TYPE_ID='{0}'", contractTypeId);
            DataSet roleDataSet = DataAccess.SelectDataSet(strsql.ToString());
            DataTable roleData = roleDataSet.Tables[0];
            return roleData;
        }


        public string UserOrDep(string[] userOrDep, int? role)
        {
            string tableName = "CAS_DEPARTMENT";
            string colName = "DEPT_ID";
            string colName1 = "DEPT_NAME";
            string colNameQ = "DEPT_ID";
            if (role == 2)
            {
                colName = "USER_ID";
                colName1 = "ENGLISH_NAME"; //CHINESE_NAME
                tableName = "CAS_USER";
                colNameQ = "USER_ID";
            }
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("id", typeof(String));
            dataTable.Columns.Add("name", typeof(String));
            for (int i = 0; i < userOrDep.Length; i++)
            {
                DataRow dr = dataTable.NewRow();
                var usersql = new StringBuilder();
                usersql.AppendFormat(" SELECT {0} as id,{1} as name FROM {2} WHERE {3}={4}", colName, colName1, tableName, colNameQ, Utils.ToSQLStr(userOrDep[i]).Trim());
                var valueString = DataAccess.SelectDataSet(usersql.ToString());
                if (valueString.Tables[0].Rows.Count>0)
                {
                    dr["id"] = valueString.Tables[0].Rows[0]["id"];
                    dr["name"] = valueString.Tables[0].Rows[0]["name"];
                }
                dataTable.Rows.Add(dr);
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            System.Collections.ArrayList dic = new System.Collections.ArrayList();
            foreach (DataRow dr in dataTable.Rows)
            {
                System.Collections.Generic.Dictionary<string, object> drow = new System.Collections.Generic.Dictionary<string, object>();
                foreach (DataColumn dc in dataTable.Columns)
                {
                    drow.Add(dc.ColumnName, dr[dc.ColumnName]);
                }
                dic.Add(drow);

            }
            //序列化  
            return jss.Serialize(dic);
        }

        public virtual bool DeleteApprovalStep(string ApprovalStepID)
        {
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                try
                {
                    List<CasContractApprovalStepApplyDeptEntity> applyDeptEntity = new List<CasContractApprovalStepApplyDeptEntity>();
                    List<CasContractApprovalStepApprovalDeptEntity> approvalDeptEntity = new List<CasContractApprovalStepApprovalDeptEntity>();
                    var entity = GetById<CasContractApprovalStepEntity>(ApprovalStepID);

                    var strsql = new StringBuilder();
                    strsql.AppendFormat(" SELECT CONTRACT_APPROVAL_STEP_APPLY_DEPT_ID FROM CAS_CONTRACT_APPROVAL_STEP_APPLY_DEPT WHERE CONTRACT_APPROVAL_STEP_ID='{0}'", ApprovalStepID);
                    DataSet dataSet = DataAccess.SelectDataSet(strsql.ToString());
                    string[] ary = Array.ConvertAll<DataRow, string>(dataSet.Tables[0].Rows.Cast<DataRow>().ToArray(), r => r["CONTRACT_APPROVAL_STEP_APPLY_DEPT_ID"].ToString());

                    var strsqlAppro = new StringBuilder();
                    strsqlAppro.AppendFormat(" SELECT CONTRACT_APPROVAL_STEP_APPROVAL_DEPT_ID FROM CAS_CONTRACT_APPROVAL_STEP_APPROVAL_DEPT WHERE CONTRACT_APPROVAL_STEP_ID='{0}'", ApprovalStepID);
                    DataSet dataSetAppro = DataAccess.SelectDataSet(strsqlAppro.ToString());
                    string[] aryAppro = Array.ConvertAll<DataRow, string>(dataSetAppro.Tables[0].Rows.Cast<DataRow>().ToArray(), r => r["CONTRACT_APPROVAL_STEP_APPROVAL_DEPT_ID"].ToString());

                    foreach (string id in ary)
                    {
                        applyDeptEntity.Add(GetById<CasContractApprovalStepApplyDeptEntity>(id));
                    }
                    foreach (string id in aryAppro)
                    {
                        approvalDeptEntity.Add(GetById<CasContractApprovalStepApprovalDeptEntity>(id));
                    }
                    broker.BeginTransaction();
                    DataAccess.Delete<CasContractApprovalStepApplyDeptEntity>(applyDeptEntity, broker);
                    DataAccess.Delete<CasContractApprovalStepApprovalDeptEntity>(approvalDeptEntity, broker);
                    Delete(entity);
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

    }
}
