using eContract.BusinessService.Common;
using eContract.Common.Entity;
using Suzsoft.Smart.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eContract.BusinessService.BusinessData.BusinessRule
{
    public class LubrPhoneSMSCodeBLL : BusinessBase
    {
        /// <summary>
        /// 通过phone获得验证码相关信息
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public LubrUserPhoneSMSCodeEntity GetRecentPhoneSMSCodeByPhone(string phone)
        {
            string sql = $@"SELECT * FROM UserPhoneSMSCode WHERE phonenumber={phone} ORDER BY sendtime DESC";
            LubrUserPhoneSMSCodeEntity PhoneSMSInfo = DataAccess.Select<LubrUserPhoneSMSCodeEntity>(sql).FirstOrDefault();
            return PhoneSMSInfo;
        }
        /// <summary>
        /// 通过ID获得发送验证码的详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LubrUserPhoneSMSCodeEntity GetRecentPhoneSMSCodeByPK(string id)
        {
            string sql = $@"SELECT * FROM UserPhoneSMSCode WHERE SMSId={id} ORDER BY sendtime DESC";
            LubrUserPhoneSMSCodeEntity PhoneSMSInfo = DataAccess.Select<LubrUserPhoneSMSCodeEntity>(sql).FirstOrDefault();
            return PhoneSMSInfo;
        }
    }
}
