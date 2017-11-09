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
    public class WordToPDFBLL : BusinessBase
    {
        /// <summary>
        /// 获得所有未转化为PDF的word文件
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllNoConvertWord()
        {
            string sql = $@" SELECT * FROM CAS_ATTACHMENT WHERE FILE_SUFFIX IN ('docx','doc') AND CONVERTED='0' ";
            return DataAccess.SelectDataSet(sql).Tables[0];
        }
    }
}
