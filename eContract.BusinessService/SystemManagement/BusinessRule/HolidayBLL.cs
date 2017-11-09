using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eContract.BusinessService.Common;
using eContract.BusinessService.SystemManagement.Domain;
using eContract.Common.Entity;
using Suzsoft.Smart.Data;
using eContract.DataAccessLayer;
using eContract.Common.Schema;
using System.Data;
using eContract.DataAccessLayer.SystemManagement;
using eContract.Common;
using Suzsoft.Smart.EntityCore;
using eContract.BusinessService.SystemManagement.Service;
using eContract.Common.MVC;
using eContract.BusinessService.SystemManagement.CommonQuery;
using eContract.Common.WebUtils;

namespace eContract.BusinessService.SystemManagement.BusinessRule
{
    public class HolidayBLL : BusinessBase
    {
        public virtual bool Add(string startDate, string endDate, ref string strError)
        {
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                try
                {
                    broker.BeginTransaction();

                    string sqlString = "DELETE FROM CAS_HOLIDAY WHERE HOLIDAY_DATE >= '" + startDate + "' AND HOLIDAY_DATE <= '" + endDate + "'";
                    broker.ExecuteNonQuery(sqlString, null, CommandType.Text);

                    DateTime dtStartDate = DateTime.Parse(startDate);
                    DateTime dtEndDate = DateTime.Parse(endDate);
                    for (DateTime dt = dtStartDate; dt <= dtEndDate; dt = dt.AddDays(1))
                    {
                        CasHolidayEntity casHolidayEntity = new CasHolidayEntity();
                        casHolidayEntity.HolidayId = Guid.NewGuid().ToString();
                        casHolidayEntity.HolidayDate = dt;
                        casHolidayEntity.IsDeleted = false;
                        casHolidayEntity.CreateTime = DateTime.Now;
                        casHolidayEntity.CreatedBy = WebCaching.UserId;
                        casHolidayEntity.LastModifiedTime = DateTime.Now;
                        casHolidayEntity.LastModifiedBy = WebCaching.UserId;

                        Insert(casHolidayEntity, broker);
                    }
                    broker.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    strError = ex.Message;
                    broker.RollBack();
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// 删除非工作日
        /// </summary>
        /// <returns></returns>
        public virtual bool Delete(string startDate, string endDate, ref string strError)
        {
            strError = "";
            try
            {
                string sqlString = "DELETE FROM CAS_HOLIDAY WHERE HOLIDAY_DATE >= '" + startDate + "' AND HOLIDAY_DATE <= '" + endDate + "'";
                DataAccess.ExecuteNoneQuery(sqlString);
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 初始化选中年份所有周六、周日为非工作日
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public virtual bool InitHoliday(string year, ref string strError)
        {
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                try
                {
                    string startDate = year + "-01-01";
                    string endDate = year + "-12-31";
                    broker.BeginTransaction();

                    string sqlString = "DELETE FROM CAS_HOLIDAY WHERE HOLIDAY_DATE >= '" + startDate + "' AND HOLIDAY_DATE <= '" + endDate + "'";
                    broker.ExecuteNonQuery(sqlString, null, CommandType.Text);

                    DateTime dtStartDate = DateTime.Parse(startDate);
                    DateTime dtEndDate = DateTime.Parse(endDate);
                    for (DateTime dt = dtStartDate; dt <= dtEndDate; dt = dt.AddDays(1))
                    {
                        if (((int)dt.DayOfWeek == 0) || ((int)dt.DayOfWeek == 6))
                        {
                            CasHolidayEntity casHolidayEntity = new CasHolidayEntity();
                            casHolidayEntity.HolidayId = Guid.NewGuid().ToString();
                            casHolidayEntity.HolidayDate = dt;
                            casHolidayEntity.IsDeleted = false;
                            casHolidayEntity.CreateTime = DateTime.Now;
                            casHolidayEntity.CreatedBy = WebCaching.UserId;
                            casHolidayEntity.LastModifiedTime = DateTime.Now;
                            casHolidayEntity.LastModifiedBy = WebCaching.UserId;

                            Insert(casHolidayEntity, broker);
                        }   
                    }
                    broker.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    strError = ex.Message;
                    broker.RollBack();
                    return false;
                }
            }
        }

        /// <summary>
        /// 获取所有非工作日
        /// </summary>
        /// <returns></returns>
        public List<CasHolidayEntity> GetAllHoliday()
        {
            eContract.Common.Utils.QueryCondition qCondition = eContract.Common.Utils.QueryCondition.Create();
            return SelectByCondition<CasHolidayEntity>(qCondition);
        }

    }
}
