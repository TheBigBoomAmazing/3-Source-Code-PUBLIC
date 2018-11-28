using eContract.BusinessService.Common;
using eContract.BusinessService.SystemManagement.Service;
using eContract.Common;
using eContract.Common.Entity;
using Suzsoft.Smart.Data;
using System;
using System.Collections.Generic;
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
    }
}
