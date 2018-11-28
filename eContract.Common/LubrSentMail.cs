using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace eContract.Common
{
    public class LubrSentMail
    {
        #region  自定义发送邮件功能
        /// <summary>
        /// 自定义发送邮件功能
        /// </summary>
        /// <param name="strSendUser">收件人</param>
        /// <param name="strSubject">主题</param>
        /// <param name="strBody">内容</param>
        /// <param name="strAttrFileName">附件地址</param>
        /// <returns></returns>
        public bool SendEmail(string strSendUser, string strSubject, string strBody, string strAttrFileName)
        {
            bool IsSue = true;
            Attachment myAttachment = null;
            string[] querrySQLSendUser = null;
            try
            {
                if (!string.IsNullOrEmpty(strSendUser))
                {
                    querrySQLSendUser = strSendUser.Split(';');
                }
                if (!string.IsNullOrEmpty(strAttrFileName))
                {
                    myAttachment = new Attachment(strAttrFileName, System.Net.Mime.MediaTypeNames.Application.Octet);
                }
                SmtpClient client = new SmtpClient("192.168.80.6");
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("ultimus@top-china.com", "ultimus");
                //client.Credentials = new System.Net.NetworkCredential("bpm@top-china.com", "top123");
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                MailMessage message = new MailMessage();
                message.From = new MailAddress("ultimus@top-china.com");
                string strLog = string.Empty;
                for (int i = 0; i < querrySQLSendUser.Length; i++)
                {
                    message.To.Add(querrySQLSendUser[i].ToString());
                    strLog += querrySQLSendUser[i].ToString() + ";";
                }
                message.Subject = strSubject;
                strLog += strSubject;

                message.BodyEncoding = System.Text.Encoding.Default;
                message.IsBodyHtml = true;
                message.Priority = MailPriority.High;
                message.Body = strBody;
                if (myAttachment != null)
                {
                    message.Attachments.Add(myAttachment);
                }
                if (!string.IsNullOrEmpty(strBody))
                {
                    client.Send(message);
                }
                //clsDB.wteLog("", strLog);
                return IsSue;
            }
            catch (Exception ex)
            {
                IsSue = false;
                //LogUtil.Error("附件错误：" + ex + " | " + strBody);
                return IsSue;
            }
        }
        #endregion
    }
}
