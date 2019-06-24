using eContract.BusinessService.Common;
using eContract.BusinessService.SystemManagement.Service;
using eContract.Common;
using eContract.Common.Entity;
using Suzsoft.Smart.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace eContract.BusinessService.BusinessData.BusinessRule
{
    public class LubrRegisterBLL : BusinessBase
    {
        /// <summary>
        /// 用户开始注册，生成邮箱的验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="userName"></param>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public string NewUserSentVerificationCode(string phone, string userName, string emailAddress)
        {
            Random rd = new Random();
            var aa=rd.Next(100000, 999999);
            string verificationCode = aa.ToString();
            emailAddress = emailAddress.TrimStart().TrimEnd();
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                broker.BeginTransaction();
                try
                {
                    string sql = @"  INSERT INTO dbo.[User]( userid ,username ,password ,age ,realname ,idcard ,userclass ,createtime ,endtime ,logintime ,emailaddress ,phonenumber,VerificationCode)  VALUES  ( NEWID() ,'" + userName + "' ,'' ,0 ,'"+ userName + "' ,'' ,0 ,GETDATE() ,GETDATE() ,GETDATE() ,'"+ emailAddress + "' ,'"+ phone + "','"+ verificationCode + "')";

                    var success = broker.ExecuteSQL(sql);
                    broker.Commit();
                }
                catch (Exception e)
                {
                }
            }
            var reciever = emailAddress;
            var cc = "";
            var title = "用户注册";
            var content = "感谢您注册磐石系统账号，您的验证码是:" + verificationCode;
            LubrSentMail lubrmial = new LubrSentMail();
            lubrmial.SendEmail(reciever, title, content, null);
            return "";
        }
        /// <summary>
        /// 判断是否改邮箱已经注册过
        /// </summary>
        /// <param name="email">邮箱地址</param>
        /// <returns>如果返回0，说明没有注册过，如果返回1说明注册过</returns>
        public string AdjustExistEmailCode(string email)
        {
            var resultValue = "0";
            email = email.Trim();
            string sql= @" SELECT * FROM [eContract].[dbo].[User] WHERE emailaddress='"+ email + "'";
            DataTable dt = DataAccess.SelectDataSet(sql.ToString()).Tables[0];
            if (dt.Rows.Count>0)
            {
                resultValue = "1";
            }
            return resultValue;
        }
        /// <summary>
        /// 注册时候根据用户的手机，姓名，邮件地址获得注册验证码
        /// </summary>
        /// <param name="phone">手机</param>
        /// <param name="name">用户名</param>
        /// <param name="email">邮箱地址</param>
        /// <returns></returns>
        public DataTable GetUSerVerificationCode(string phone,string name,string email)
        { 
            DataTable dt = new DataTable();
            string sql = @"  SELECT VerificationCode as CODE FROM [eContract].[dbo].[User] WHERE username='" + name + "' AND phonenumber='"+ phone + "' AND emailaddress='"+ email + "'";
            dt = DataAccess.SelectDataSet(sql.ToString()).Tables[0];
            return dt;
        }
        /// <summary>
        /// 注册新的用户
        /// </summary>
        /// <param name="userEntity"></param>
        public void RegisterNewUser(LubrUserEntity userEntity)
        {
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                broker.BeginTransaction();
                CasUserEntity user = new CasUserEntity();
                user.UserId = Guid.NewGuid().ToString();
                user.UserAccount = userEntity.username;
                user.UserCode= userEntity.username;
                user.ChineseName = userEntity.username;
                user.EnglishName = userEntity.username;
                var password = Encryption.Encrypt(userEntity.password);
                user.Password = password;
                user.Status = 0;
                user.IsDeleted = false;
                user.CreatedBy = "self";
                user.CreateTime = DateTime.Now;
                user.LastModifiedBy = "self";
                user.LastModifiedTime = DateTime.Now;
                DataAccess.Insert(user, broker);
                broker.Commit();
            }
        }
        /// <summary>
        /// 用户开始注册，生成手机短信的验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string NewUserSentSMSCode(string phone, string userName)
        {
            Random rd = new Random();
            var aa = rd.Next(1000, 10000);
            string SMSCode = aa.ToString();
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                broker.BeginTransaction();
                try
                {
                    string sql = @"  INSERT INTO [eContract].[dbo].[UserPhoneSMSCode](phonenumber,sendtime,expiretime,smscode,spare2,SMSId)  VALUES  ( '" + phone + "', GETDATE() , 20 ,'" + SMSCode + "','',NEWID())";

                    var success = broker.ExecuteSQL(sql);
                    broker.Commit();
                }
                catch (Exception e)
                {
                }
            }
            /*var reciever = emailAddress;
            var cc = "";
            var title = "用户注册";
            var content = "感谢您注册磐石系统账号，您的验证码是:" + verificationCode;
            LubrSentMail lubrmial = new LubrSentMail();
            lubrmial.SendEmail(reciever, title, content, null);*/
            return SMSCode;
        }
        /// <summary>
        /// 注册时候根据用户的手机获得注册验证码
        /// </summary>
        /// <param name="phone">手机</param>
        /// <returns></returns>
        public DataTable GetUSerSMSCode(string phone, string name)
        {
            DataTable dt = new DataTable();
            string sql = @"  SELECT smscode as SMSCODE FROM [eContract].[dbo].[UserPhoneSMSCode] WHERE phonenumber='" + phone + "' order by sendtime desc";
            dt = DataAccess.SelectDataSet(sql.ToString()).Tables[0];
            return dt;
        }
        /// <summary>
        /// 发送验证码时候判断之前验证码是否过期
        /// </summary>
        /// <param name="phone">手机</param>
        /// <returns></returns>
        public DataTable GetUSerSMSCodeExpireTime(string phone)
        {
            DataTable dt = new DataTable();
            string sql = @"  SELECT TOP 1 sendtime as SENDTIME,expiretime as EXPIRETIME FROM [eContract].[dbo].[UserPhoneSMSCode] WHERE phonenumber='" + phone + "' order by sendtime desc";
            dt = DataAccess.SelectDataSet(sql.ToString()).Tables[0];
            return dt;
        }
        /// <summary>
        /// 判断是否该手机号已经注册过
        /// </summary>
        /// <param name="phone">邮箱地址</param>
        /// <returns>如果返回0，说明没有注册过，如果返回1说明注册过</returns>
        public string AdjustExistPhoneCode(string phone)
        {
            var resultValue = "0";
            phone = phone.Trim();
            string sql = @" SELECT * FROM [eContract].[dbo].[User] WHERE phonenumber='" + phone + "'";
            DataTable dt = DataAccess.SelectDataSet(sql.ToString()).Tables[0];
            if (dt.Rows.Count > 0)
            {
                resultValue = "1";
            }
            return resultValue;
        }
    }
}
