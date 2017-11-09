using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eContract.Common
{
    public static class UploadPath
    {
        public static string GetUpLoadPath(string strAppPath)
        {
            return System.Configuration.ConfigurationSettings.AppSettings["UploadPath"].ToString() + strAppPath;
        }

        #region Web Service 路径

        public const string ServiceUploadPath = "ServiceUploadPath";
        public const string StoreAuditPath = "XmlStoreAudit";
        public const string UploadIssue = "XmlUploadIssue";

        #endregion

        #region DDP 路径

        public const string BackupPath = "BackupPath";
        public const string PhotoPath = "PhotoPath";
        public const string ErrorPath = "ErrorPath";

        #endregion

        #region B2B专题文件路径

        // 专题根目录
        public static string GetTopicPath()
        {
            return System.Configuration.ConfigurationManager.AppSettings["TopicFilePath"].ToString();
        }

        #endregion
    }
}
