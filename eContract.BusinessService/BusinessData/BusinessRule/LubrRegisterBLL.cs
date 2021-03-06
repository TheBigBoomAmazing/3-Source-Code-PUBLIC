﻿using eContract.BusinessService.Common;
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
                user.PhoneNumber = userEntity.phonenumber;
                DataAccess.Insert(user, broker);
                broker.Commit();
            }
        }
        /// <summary>
        /// 跟新用户的登录密码
        /// </summary>
        /// <param name="userEntity"></param>
        public void UpdateUser(LubrUserEntity userEntity)
        {
            //首先我得获取用户的id
            var phone = userEntity.phonenumber.ToString().Trim();
            var account = userEntity.username.ToString().Trim();
            var userID = GetUserIdByPhoneAndAccount(phone,account);
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                broker.BeginTransaction();
                CasUserEntity user = SystemService.UserService.GetById<CasUserEntity>(userID);
                user.Password = Encryption.Encrypt(userEntity.password);
                user.LastModifiedBy = "忘记密码";
                user.LastModifiedTime = DateTime.Now;
                DataAccess.Update(user, broker);
                broker.Commit();
            }            
        }
        /// <summary>
        /// 根据phone和accout获得用户的id信息
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public string GetUserIdByPhoneAndAccount(string phone, string account)
        {
            string id = "";
            DataTable dt = new DataTable();
            string sql = "SELECT USER_ID FROM  CAS_USER WHERE PHONE_NUMBER='" + phone + "' AND USER_ACCOUNT='"+ account + "'";
            dt = DataAccess.SelectDataSet(sql.ToString()).Tables[0];
            if (dt.Rows.Count >0)
            {
                id = dt.Rows[0]["USER_ID"].ToString();
            }
            return id;
        }
        /// <summary>
        /// 用户开始注册，生成手机短信的验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string NewUserSentSMSCode(string phone, string userName,string flagstatus)
        {
            Random rd = new Random();
            var aa = rd.Next(1000, 10000);
            string SMSCode = aa.ToString();
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                broker.BeginTransaction();
                try
                {
                    string sql = @"  INSERT INTO [eContract].[dbo].[UserPhoneSMSCode](phonenumber,sendtime,expiretime,smscode,spare2,SMSId,flagstatus)  VALUES  ( '" + phone + "', GETDATE() , 20 ,'" + SMSCode + "','',NEWID(),'" + flagstatus + "')";

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
        /// 根据用户的手机获得注册验证码
        /// </summary>
        /// <param name="phone">手机</param>
        /// <param name="flagstatus">标志位(注册为1，找回密码为2)</param>
        /// <returns></returns>
        public DataTable GetUSerSMSCode(string phone, string name,string flagstatus)
        {
            DataTable dt = new DataTable();
            string sql = @"  SELECT smscode as SMSCODE FROM [eContract].[dbo].[UserPhoneSMSCode] WHERE phonenumber='" + phone + "' and flagstatus = '"+flagstatus+"' order by sendtime desc";
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
            string sql = @" SELECT * FROM [eContract].[dbo].[CAS_USER] WHERE PHONE_NUMBER='" + phone + "'";
            DataTable dt = DataAccess.SelectDataSet(sql.ToString()).Tables[0];
            if (dt.Rows.Count > 0)
            {
                resultValue = "1";
            }
            return resultValue;
        }
    }
}
