using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;

namespace eContract.Common
{
    public static class SendEmail
    {
        private static MailMessage mMailMessage;   //主要处理发送邮件的内容（如：收发人地址、标题、主体、图片等等）
        private static SmtpClient mSmtpClient; //主要处理用smtp方式发送此邮件的配置信息（如：邮件服务器、发送端口号、验证方式等等）
        private static int mSenderPort;   //发送邮件所用的端口号（htmp协议默认为25）
        private static string mSenderServerHost;    //发件箱的邮件服务器地址（IP形式或字符串形式均可）
        private static string mSenderPassword;    //发件箱的密码
        private static string mSenderUsername;   //发件箱的用户名（即@符号前面的字符串，例如：hello@163.com，用户名为：hello）
        private static bool mEnableSsl;    //是否对邮件内容进行socket层加密传输
        private static bool mEnablePwdAuthentication;  //是否对发件人邮箱进行密码验证

        private static string fromMail;


        static SendEmail()
        {
            fromMail = ConfigurationManager.AppSettings["FromMail"];
            mSenderServerHost = ConfigurationManager.AppSettings["SenderServerHost"];//使用163代理邮箱服务器（也可是使用qq的代理邮箱服务器，但需要与具体邮箱对相应）
            mSenderUsername = ConfigurationManager.AppSettings["SenderUsername"]; ////登录邮箱的用户名
            mSenderPassword = ConfigurationManager.AppSettings["SenderPassword"]; //对应的登录邮箱的第三方密码（你的邮箱不论是163还是qq邮箱，都需要自行开通stmp服务）
            mSenderPort = 25;                      //发送邮箱的端口号
            mEnableSsl = false;
            mEnablePwdAuthentication = false;
        }


        public static bool Send(string reciver, string subject, string emailBody)
        {
            return Send(reciver, "", subject, emailBody);
        }
        public static bool Send(string reciver, string cc, string subject, string emailBody)
        {
            return Send(new List<string> { reciver }, new List<string> { cc }, subject, emailBody);
        }
        public static bool Send(List<string> recivers, string subject, string emailBody)
        {
            return Send(recivers, new List<string>(), subject, emailBody);
        }
        public static bool Send(List<string> recivers, List<string> ccs, string subject, string emailBody)
        {
            try
            {
                string toMail = string.Join(",", recivers);
                string cc = "";
                if (ccs.Count > 0)
                {
                    cc= string.Join(",", ccs);
                }
                mMailMessage = new MailMessage();
                mMailMessage.To.Add(toMail);
                if (!string.IsNullOrWhiteSpace(cc))
                {
                    mMailMessage.CC.Add(cc);
                }
                mMailMessage.From = new MailAddress(fromMail);
                mMailMessage.Subject = subject;
                mMailMessage.Body = emailBody;
                mMailMessage.IsBodyHtml = true;
                mMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                mMailMessage.Priority = MailPriority.Normal;
                if (mMailMessage != null)
                {
                    mSmtpClient = new SmtpClient();
                    mSmtpClient.EnableSsl = mEnableSsl;
                    mSmtpClient.UseDefaultCredentials = false;
                    mSmtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;//指定邮件发送方式
                                                                                            //mSmtpClient.Host = "smtp." + mMailMessage.From.Host;
                    mSmtpClient.Host = mSenderServerHost;
                    mSmtpClient.Port = mSenderPort;


                    if (mEnablePwdAuthentication)
                    {
                        System.Net.NetworkCredential nc = new System.Net.NetworkCredential(mSenderUsername, mSenderPassword);
                        //mSmtpClient.Credentials = new System.Net.NetworkCredential(this.mSenderUsername, this.mSenderPassword);
                        //NTLM: Secure Password Authentication in Microsoft Outlook Express
                        mSmtpClient.Credentials = nc.GetCredential(mSmtpClient.Host, mSmtpClient.Port, "NTLM");
                    }
                    else
                    {
                        mSmtpClient.Credentials = new System.Net.NetworkCredential(mSenderUsername, mSenderPassword);
                    }
                    mSmtpClient.Send(mMailMessage);
                }
            }
            catch (InvalidCastException e)
            {
                return false;
            }
            return true;
        }

        ///<summary>
        /// 添加附件
        ///</summary>
        ///<param name="attachmentsPath">附件的路径集合，以分号分隔</param>
        private static void AddAttachments(string attachmentsPath)
        {
            try
            {
                string[] path = attachmentsPath.Split(';'); //以什么符号分隔可以自定义
                Attachment data;
                ContentDisposition disposition;
                for (int i = 0; i < path.Length; i++)
                {
                    data = new Attachment(path[i], MediaTypeNames.Application.Octet);
                    disposition = data.ContentDisposition;
                    disposition.CreationDate = File.GetCreationTime(path[i]);
                    disposition.ModificationDate = File.GetLastWriteTime(path[i]);
                    disposition.ReadDate = File.GetLastAccessTime(path[i]);
                    mMailMessage.Attachments.Add(data);
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //Console.WriteLine(ex.ToString());
            }
        }

    }
}
