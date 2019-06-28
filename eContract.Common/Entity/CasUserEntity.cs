using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Schema;

namespace eContract.Common.Entity
{
    [Serializable]
    public partial class CasUserEntity : EntityBase
    {
        public CasUserTable TableSchema
        {
            get
            {
                return CasUserTable.Current;
            }
        }


        public CasUserEntity()
        {
            IsDeleted = false;
        }

        public override TableInfo OringTableSchema
        {
            get
            {
                return CasUserTable.Current;
            }
        }
        #region 属性列表

        public string UserId
        {
            get { return (string)GetData(CasUserTable.C_USER_ID); }
            set { SetData(CasUserTable.C_USER_ID, value); }
        }

        public string UserAccount
        {
            get { return (string)GetData(CasUserTable.C_USER_ACCOUNT); }
            set { SetData(CasUserTable.C_USER_ACCOUNT, value); }
        }

        public string UserCode
        {
            get { return (string)GetData(CasUserTable.C_USER_CODE); }
            set { SetData(CasUserTable.C_USER_CODE, value); }
        }

        public string ChineseName
        {
            get { return (string)GetData(CasUserTable.C_CHINESE_NAME); }
            set { SetData(CasUserTable.C_CHINESE_NAME, value); }
        }

        public string EnglishName
        {
            get { return (string)GetData(CasUserTable.C_ENGLISH_NAME); }
            set { SetData(CasUserTable.C_ENGLISH_NAME, value); }
        }

        public string Password
        {
            get { return (string)GetData(CasUserTable.C_PASSWORD); }
            set { SetData(CasUserTable.C_PASSWORD, value); }
        }

        public string CompanyCode
        {
            get { return (string)GetData(CasUserTable.C_COMPANY_CODE); }
            set { SetData(CasUserTable.C_COMPANY_CODE, value); }
        }

        public int Status
        {
            get
            {
                if (GetData(CasUserTable.C_STATUS) == null)
                {
                    return -999;
                }
                return (int)(GetData(CasUserTable.C_STATUS));
            }
            set { SetData(CasUserTable.C_STATUS, value); }
        }

        public string DeparmentCode
        {
            get { return (string)GetData(CasUserTable.C_DEPARMENT_CODE); }
            set { SetData(CasUserTable.C_DEPARMENT_CODE, value); }
        }

        public string DeparmentName
        {
            get { return (string)GetData(CasUserTable.C_DEPARMENT_NAME); }
            set { SetData(CasUserTable.C_DEPARMENT_NAME, value); }
        }

        public string PositionCode
        {
            get { return (string)GetData(CasUserTable.C_POSITION_CODE); }
            set { SetData(CasUserTable.C_POSITION_CODE, value); }
        }

        public string PositionDescription
        {
            get { return (string)GetData(CasUserTable.C_POSITION_DESCRIPTION); }
            set { SetData(CasUserTable.C_POSITION_DESCRIPTION, value); }
        }

        public string EnglishTitle
        {
            get { return (string)GetData(CasUserTable.C_ENGLISH_TITLE); }
            set { SetData(CasUserTable.C_ENGLISH_TITLE, value); }
        }

        public string ChineseTitle
        {
            get { return (string)GetData(CasUserTable.C_CHINESE_TITLE); }
            set { SetData(CasUserTable.C_CHINESE_TITLE, value); }
        }

        public string OrgUnitCode
        {
            get { return (string)GetData(CasUserTable.C_ORG_UNIT_CODE); }
            set { SetData(CasUserTable.C_ORG_UNIT_CODE, value); }
        }

        public string CostCenterCode
        {
            get { return (string)GetData(CasUserTable.C_COST_CENTER_CODE); }
            set { SetData(CasUserTable.C_COST_CENTER_CODE, value); }
        }

        public string CostCenterName
        {
            get { return (string)GetData(CasUserTable.C_COST_CENTER_NAME); }
            set { SetData(CasUserTable.C_COST_CENTER_NAME, value); }
        }

        public string EnglishLastName
        {
            get { return (string)GetData(CasUserTable.C_ENGLISH_LAST_NAME); }
            set { SetData(CasUserTable.C_ENGLISH_LAST_NAME, value); }
        }

        public string EnglishFirstName
        {
            get { return (string)GetData(CasUserTable.C_ENGLISH_FIRST_NAME); }
            set { SetData(CasUserTable.C_ENGLISH_FIRST_NAME, value); }
        }

        public string ChineseLastName
        {
            get { return (string)GetData(CasUserTable.C_CHINESE_LAST_NAME); }
            set { SetData(CasUserTable.C_CHINESE_LAST_NAME, value); }
        }

        public string ChineseFirstName
        {
            get { return (string)GetData(CasUserTable.C_CHINESE_FIRST_NAME); }
            set { SetData(CasUserTable.C_CHINESE_FIRST_NAME, value); }
        }

        public string PinyinLastName
        {
            get { return (string)GetData(CasUserTable.C_PINYIN_LAST_NAME); }
            set { SetData(CasUserTable.C_PINYIN_LAST_NAME, value); }
        }

        public string PinyinFirstName
        {
            get { return (string)GetData(CasUserTable.C_PINYIN_FIRST_NAME); }
            set { SetData(CasUserTable.C_PINYIN_FIRST_NAME, value); }
        }

        public int Gender
        {
            get {
                if (GetData(CasUserTable.C_GENDER) == null) {
                    return -999;
                }
                return (int)(GetData(CasUserTable.C_GENDER)); }
            set { SetData(CasUserTable.C_GENDER, value); }
        }

        public string ContactNo
        {
            get { return (string)GetData(CasUserTable.C_CONTACT_NO); }
            set { SetData(CasUserTable.C_CONTACT_NO, value); }
        }

        public string LineManagerId
        {
            get { return (string)GetData(CasUserTable.C_LINE_MANAGER_ID); }
            set { SetData(CasUserTable.C_LINE_MANAGER_ID, value); }
        }

        public string Email
        {
            get { return (string)GetData(CasUserTable.C_EMAIL); }
            set { SetData(CasUserTable.C_EMAIL, value); }
        }

        public string CityCode
        {
            get { return (string)GetData(CasUserTable.C_CITY_CODE); }
            set { SetData(CasUserTable.C_CITY_CODE, value); }
        }

        public DateTime OnboardingDate
        {
            get {
                if (GetData(CasUserTable.C_ONBOARDING_DATE) == null) {
                    return DateTime.MinValue;
                }
                return (DateTime)(GetData(CasUserTable.C_ONBOARDING_DATE)); }
            set { SetData(CasUserTable.C_ONBOARDING_DATE, value); }
        }

        public DateTime LastWorkDate
        {
            get {
                if (GetData(CasUserTable.C_LAST_WORK_DATE) == null) {
                    return DateTime.MinValue;
                }
                return (DateTime)(GetData(CasUserTable.C_LAST_WORK_DATE)); }
            set { SetData(CasUserTable.C_LAST_WORK_DATE, value); }
        }

        public string EmployeeVendor
        {
            get { return (string)GetData(CasUserTable.C_EMPLOYEE_VENDOR); }
            set { SetData(CasUserTable.C_EMPLOYEE_VENDOR, value); }
        }

        public string Grade
        {
            get { return (string)GetData(CasUserTable.C_GRADE); }
            set { SetData(CasUserTable.C_GRADE, value); }
        }

        public string Remark
        {
            get { return (string)GetData(CasUserTable.C_REMARK); }
            set { SetData(CasUserTable.C_REMARK, value); }
        }

        public bool IsAdmin
        {
            get {
                if (GetData(CasUserTable.C_IS_ADMIN) == null) {
                    return false;
                }
                return (bool)(GetData(CasUserTable.C_IS_ADMIN)); }
            set { SetData(CasUserTable.C_IS_ADMIN, value); }
        }

        public bool IsLock
        {
            get {
                if (GetData(CasUserTable.C_IS_LOCK) == null) {
                    return false;
                }
                return (bool)(GetData(CasUserTable.C_IS_LOCK)); }
            set { SetData(CasUserTable.C_IS_LOCK, value); }
        }

        public bool IsDeleted
        {
            get {
                if (GetData(CasUserTable.C_IS_DELETED) == null) {
                    return false;
                }
                return (bool)(GetData(CasUserTable.C_IS_DELETED)); }
            set { SetData(CasUserTable.C_IS_DELETED, value); }
        }

        public string CreatedBy
        {
            get { return (string)GetData(CasUserTable.C_CREATED_BY); }
            set { SetData(CasUserTable.C_CREATED_BY, value); }
        }

        public DateTime CreateTime
        {
            get {
                if (GetData(CasUserTable.C_CREATE_TIME) == null) {
                    return DateTime.MinValue;
                }
                return (DateTime)(GetData(CasUserTable.C_CREATE_TIME)); }
            set { SetData(CasUserTable.C_CREATE_TIME, value); }
        }

        public string LastModifiedBy
        {
            get { return (string)GetData(CasUserTable.C_LAST_MODIFIED_BY); }
            set { SetData(CasUserTable.C_LAST_MODIFIED_BY, value); }
        }

        public DateTime LastModifiedTime
        {
            get {
                if (GetData(CasUserTable.C_LAST_MODIFIED_TIME) == null) {
                    return DateTime.MinValue;
                }
                return (DateTime)(GetData(CasUserTable.C_LAST_MODIFIED_TIME)); }
            set { SetData(CasUserTable.C_LAST_MODIFIED_TIME, value); }
        }
         public string OwnDepValue
        {
            get;set;
        }
        public string PhoneNumber
        {
            get { return (string)GetData(CasUserTable.C_PHONE_NUMBER); }
            set { SetData(CasUserTable.C_PHONE_NUMBER, value); }
        }
        #endregion
    }
}
