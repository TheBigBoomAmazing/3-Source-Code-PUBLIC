using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using eContract.Common.Entity;
using eContract.Common.Schema;
using eContract.BusinessService.SystemManagement.BusinessRule;

namespace eContract.BusinessService.SystemManagement.Service
{
    public class SystemService
    {
        static UserBLL userService;
        public static UserBLL UserService
        {
            get
            {
                if (userService == null)
                {
                    userService = new UserBLL();
                }
                return userService;
            }
        }

        static PageBLL pageService;
        public static PageBLL PageService
        {
            get
            {
                if (pageService == null)
                {
                    pageService = new PageBLL();
                }
                return pageService;
            }
        }

        static FunctionRoleBLL functionRoleService;
        public static FunctionRoleBLL FunctionRoleService
        {
            get
            {
                if (functionRoleService == null)
                {
                    functionRoleService = new FunctionRoleBLL();
                }
                return functionRoleService;
            }
        }

        static BusinessRoleBLL businessRoleService;
        public static BusinessRoleBLL BusinessionRoleService
        {
            get
            {
                if (businessRoleService == null)
                {
                    businessRoleService = new BusinessRoleBLL();
                }
                return businessRoleService;
            }
        }

        static LogErrorBLL logErrorService;
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

        static RolePageBLL roleMenuService;
        public static RolePageBLL RoleMenuService
        {
            get
            {
                if (roleMenuService == null)
                {
                    roleMenuService = new RolePageBLL();
                }
                return roleMenuService;
            }
        }

        static SecUserRoleBLL userRoleService;
        public static SecUserRoleBLL UserRoleService
        {
            get
            {
                if (userRoleService == null)
                {
                    userRoleService = new SecUserRoleBLL();
                }
                return userRoleService;
            }
        }

        static RolePageBLL rolePageService;
        public static RolePageBLL RolePageService
        {
            get
            {
                if (rolePageService == null)
                {
                    rolePageService = new RolePageBLL();
                }
                return rolePageService;
            }
        }

        static DepartmentBLL departmentService;
        public static DepartmentBLL DepartmentService
        {
            get
            {
                if (departmentService == null)
                {
                    departmentService = new DepartmentBLL();
                }
                return departmentService;
            }
        }

        static RegionBLL regionService;
        public static RegionBLL RegionService
        {
            get
            {
                if (regionService == null)
                {
                    regionService = new RegionBLL();
                }
                return regionService;
            }
        }

        static HolidayBLL holidayService;
        public static HolidayBLL HolidayService
        {
            get
            {
                if (holidayService == null)
                {
                    holidayService = new HolidayBLL();
                }
                return holidayService;
            }
        }       

    }
}
