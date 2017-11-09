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
using eContract.BusinessService.SystemManagement.Service;
using eContract.BusinessService.SystemManagement.Domain;

namespace eContract.BusinessService.BusinessData.BusinessRule
{
    public class ContractFieldBLL:BusinessBase
    {
        public JqGrid ForGrid(JqGrid grid)
        {


            UserDomain userDomain = (UserDomain)WebCaching.CurrentUserDomain;
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("FieldExplain", typeof(String));
            dataTable.Columns.Add("ViewShow", typeof(String));
            dataTable.Columns.Add("Content", typeof(String));
            dataTable.Columns.Add("IsTemplate", typeof(String));
            dataTable.Columns.Add("Explain", typeof(String));
            DataRow dr1 = dataTable.NewRow();
            dr1["FieldExplain"] = "合同名称";
            dr1["ViewShow"] = "Contract Name*";
            dr1["Content"] = "字符，支持英文中文数字";
            dr1["IsTemplate"] = "false";
            dr1["Explain"] = "必填";
            dataTable.Rows.Add(dr1);
            DataRow dr2 = dataTable.NewRow();
            dr2["FieldExplain"] = "本次合同附件";
            dr2["ViewShow"] = "Contract to be reviewed*";
            dr2["Content"] = "必填";
            dr2["IsTemplate"] = "false";
            dr2["Explain"] = "上传至少一个附件，支持多个附件，格式只能为PDF格式";
            dataTable.Rows.Add(dr2);
            DataRow dr3 = dataTable.NewRow();
            dr3["FieldExplain"] = "甲方";
            dr3["ViewShow"] = "Ferrero Entity*";
            dr3["Content"] = "必填";
            dr3["IsTemplate"] = "common";
            dr3["Explain"] = "系统自动将FTS 或FFH作为合同一方[注1]下拉菜单";
            dataTable.Rows.Add(dr3);
            DataRow dr4 = dataTable.NewRow();
            dr4["FieldExplain"] = "乙方（英文）";
            dr4["ViewShow"] = "Counterparty (EN)*";
            dr4["Content"] = "必填";
            dr4["IsTemplate"] = "false";
            dr4["Explain"] = "相对方，可填选多方，英文字符、数字";
            dataTable.Rows.Add(dr4);
            DataRow dr5 = dataTable.NewRow();
            dr5["FieldExplain"] = "乙方（中文）";
            dr5["ViewShow"] = "Counterparty (CH)";
            dr5["Content"] = "非必填";
            dr5["IsTemplate"] = "false";
            dr5["Explain"] = "相对方的中文名称";
            dataTable.Rows.Add(dr5);
            DataRow dr6 = dataTable.NewRow();
            dr6["FieldExplain"] = "合同经办部门";
            dr6["ViewShow"] = "Contract Owner";
            dr6["Content"] = "系统自动生成";
            dr6["IsTemplate"] = "false";
            dr6["Explain"] = "申请人所在部门";
            dataTable.Rows.Add(dr6);
            DataRow dr7 = dataTable.NewRow();
            dr7["FieldExplain"] = "申请人";
            dr7["ViewShow"] = "Contract Initiator";
            dr7["Content"] = "字符，支持英文中文数字";
            dr7["IsTemplate"] = "false";
            dr7["Explain"] = "必填";
            dataTable.Rows.Add(dr7);
            DataRow dr8 = dataTable.NewRow();
            dr8["FieldExplain"] = "合同名称";
            dr8["ViewShow"] = "Contract Name*";
            dr8["Content"] = "系统自动生成";
            dr8["IsTemplate"] = "false";
            dr8["Explain"] = "即申请人";
            dataTable.Rows.Add(dr8);
            DataRow dr9 = dataTable.NewRow();
            dr9["FieldExplain"] = "申请日期";
            dr9["ViewShow"] = "Date of Application";
            dr9["Content"] = "系统自动生成";
            dr9["IsTemplate"] = "common";
            dr9["Explain"] = "提交当日";
            dataTable.Rows.Add(dr9);
            DataRow dr10 = dataTable.NewRow();
            dr10["FieldExplain"] = "生效日期";
            dr10["ViewShow"] = "Effective Date*";
            dr10["Content"] = "必填";
            dr10["IsTemplate"] = "false";
            dr10["Explain"] = "日期格式";
            dataTable.Rows.Add(dr10);
            DataRow dr11 = dataTable.NewRow();
            dr11["FieldExplain"] = "结束日期";
            dr11["ViewShow"] = "Expiration Date*";
            dr11["Content"] = "必填";
            dr11["IsTemplate"] = "false";
            dr11["Explain"] = "日期格式,若合同期限超过365天，自动弹出长期合同政策提醒，点击确定可关闭";
            dataTable.Rows.Add(dr11);
            DataRow dr12 = dataTable.NewRow();
            dr12["FieldExplain"] = "框架合同";
            dr12["ViewShow"] = "Master Agreement or not*";
            dr12["Content"] = "Yes/No";
            dr12["IsTemplate"] = "false";
            dr12["Explain"] = "必填，二选一";
            dataTable.Rows.Add(dr12);
            DataRow dr14 = dataTable.NewRow();
            dr14["FieldExplain"] = "币种";
            dr14["ViewShow"] = "Currency*";
            dr14["Content"] = "必填";
            dr14["IsTemplate"] = "false";
            dr14["Explain"] = "下拉菜单（所有币种+其他）";
            dataTable.Rows.Add(dr14);
            DataRow dr15 = dataTable.NewRow();
            dr15["FieldExplain"] = "固定资产";
            dr15["ViewShow"] = "CAPEX or not*";
            dr15["Content"] = "Yes/No";
            dr15["IsTemplate"] = "false";
            dr15["Explain"] = "必填，二选一";
            dataTable.Rows.Add(dr15);
            DataRow dr16 = dataTable.NewRow();
            dr16["FieldExplain"] = "补充合同";
            dr16["ViewShow"] = "Supplementary or not*";
            dr16["Content"] = "Yes/No";
            dr16["IsTemplate"] = "false";
            dr16["Explain"] = "必填，二选一，下拉菜单";
            dataTable.Rows.Add(dr16);
            DataRow dr17 = dataTable.NewRow();
            dr17["FieldExplain"] = "补充合同附件";
            dr17["ViewShow"] = "Reference Contract*";
            dr17["Content"] = "";
            dr17["IsTemplate"] = "false";
            dr17["Explain"] = "若为补充协议，则必填，二选一，如合同已在系统内，可浏览添加附件；如合同不在系统中，则需要上传附件";
            dataTable.Rows.Add(dr17);
            DataRow dr18 = dataTable.NewRow();
            dr18["FieldExplain"] = "预付金额";
            dr18["ViewShow"] = "Prepayment amount";
            dr18["Content"] = "非必填";
            dr18["IsTemplate"] = "false";
            dr18["Explain"] = "金额";
            dataTable.Rows.Add(dr18);
            DataRow dr19 = dataTable.NewRow();
            dr19["FieldExplain"] = "合同说明";
            dr19["ViewShow"] = "Contract key points*";
            dr19["Content"] = "必填";
            dr19["IsTemplate"] = "false";
            dr19["Explain"] = "字符，只支持英文数字";
            dataTable.Rows.Add(dr19);
            DataRow dr20 = dataTable.NewRow();
            dr20["FieldExplain"] = "预付百分比";
            dr20["ViewShow"] = "Prepayment percentage";
            dr20["Content"] = "非必填";
            dr20["IsTemplate"] = "false";
            dr20["Explain"] = "百分比";
            dataTable.Rows.Add(dr20);
            DataRow dr21 = dataTable.NewRow();
            dr21["FieldExplain"] = "预算类型";
            dr21["ViewShow"] = "Budget type*";
            dr21["Content"] = "Overheads/Non-overheads/Industrial/Other";
            dr21["IsTemplate"] = "false";
            dr21["Explain"] = "下拉菜单";
            dataTable.Rows.Add(dr21);
            DataRow dr22 = dataTable.NewRow();
            dr22["FieldExplain"] = "模板编号";
            dr22["ViewShow"] = "Template No.*";
            dr22["Content"] = "必填";
            dr22["IsTemplate"] = "true";
            dr22["Explain"] = "字符，支持英文数字";
            dataTable.Rows.Add(dr22);
            DataRow dr23 = dataTable.NewRow();
            dr23["FieldExplain"] = "模板名称";
            dr23["ViewShow"] = "Template Name*";
            dr23["Content"] = "必填";
            dr23["IsTemplate"] = "true";
            dr23["Explain"] = "字符，支持英文数字";
            dataTable.Rows.Add(dr23);
            DataRow dr24 = dataTable.NewRow();
            dr24["FieldExplain"] = "模板所属部门";
            dr24["ViewShow"] = "Template Owner";
            dr24["Content"] = "系统自动生成";
            dr24["IsTemplate"] = "true";
            dr24["Explain"] = "申请人所在的部门";
            dataTable.Rows.Add(dr24);
            DataRow dr25 = dataTable.NewRow();
            dr25["FieldExplain"] = "模板申请人";
            dr25["ViewShow"] = "Template Initiator";
            dr25["Content"] = "系统自动生成";
            dr25["IsTemplate"] = "true";
            dr25["Explain"] = "即申请人";
            dataTable.Rows.Add(dr25);
            DataRow dr26 = dataTable.NewRow();
            dr26["FieldExplain"] = "适用范围";
            dr26["ViewShow"] = "Scope of Application*";
            dr26["Content"] = "必填";
            dr26["IsTemplate"] = "true";
            dr26["Explain"] = "字符，支持英文数字";
            dataTable.Rows.Add(dr26);
            DataRow dr27 = dataTable.NewRow();
            //dr27["FieldExplain"] = "审批完成时间";
            //dr27["ViewShow"] = "Online review completion date";
            //dr27["Content"] = "系统自动生成";
            //dr27["IsTemplate"] = "common";
            //dr27["Explain"] = "日期格式";
            dr27["FieldExplain"] = "合同金额";
            dr27["ViewShow"] = "Contract Price*";
            dr27["Content"] = "必填";
            dr27["IsTemplate"] = "false";
            dr27["Explain"] = "Master Agreement 填预估金额，one-off agreement填固定金额，或自定义;For master agreement, please insert estimated price";
            dataTable.Rows.Add(dr27);
            var queryWord = grid.QueryField.ContainsKey("FieldExplain") ? grid.QueryField["FieldExplain"].ToString() : "";
            var queryTep = grid.QueryField.ContainsKey("IsTemplate")? grid.QueryField["IsTemplate"].ToString() : "";
            string query = $" 1=1 ";
            if (!string.IsNullOrWhiteSpace(queryWord))
            {
                query = query + $" AND ( FieldExplain LIKE '%{queryWord}%' OR ViewShow LIKE '%{queryWord}%' )";//模糊查询 
            }
            if (!string.IsNullOrWhiteSpace(queryTep))
            {
                var isTep = queryTep == "1" ? "true" : "false";
                query = query + $" AND IsTemplate = '{isTep}' OR IsTemplate='common' ";
            }
            DataTable dtNew = new DataTable();
            if (!string.IsNullOrWhiteSpace(queryWord)|| !string.IsNullOrWhiteSpace(queryTep))
            {
                DataRow[] drArr = dataTable.Select(query);
                dtNew = dataTable.Clone();
                for (int i = 0; i < drArr.Length; i++)
                {
                    dtNew.ImportRow(drArr[i]);
                }
                DataTable dt = dtNew;
                grid.DataBind(dt, dt.Rows.Count);
            }
            else
            {
                DataTable dt = dataTable;
                grid.DataBind(dt, dt.Rows.Count);
            }
            return grid;
        }
    }
}
