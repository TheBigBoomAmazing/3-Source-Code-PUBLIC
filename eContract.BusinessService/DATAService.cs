using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eContract.BusinessService;
using eContract.BusinessService.SystemManagement.BusinessRule;

namespace eContract.BusinessService
{
    public class DATAService
    {
        #region 系统
        private static BusinessRoleBLL roleService;
        public static BusinessRoleBLL RoleService
        {
            get
            {
                if (roleService == null)
                {
                    roleService = new BusinessRoleBLL();
                }
                return roleService;
            }
        }

        private static LogErrorBLL logErrorService;
        public static LogErrorBLL LogErrorService
        {
            get
            {
                if (logErrorService == null)
                {
                    logErrorService = new LogErrorBLL();
                }
                return logErrorService;
            }
        }
        #endregion
    }
}
