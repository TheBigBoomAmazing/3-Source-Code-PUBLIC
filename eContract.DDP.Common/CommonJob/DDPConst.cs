using System;
using System.Collections.Generic;
using System.Text;

namespace eContract.DDP.Common.CommonJob
{
    public class DDPConst
    {
        /// <summary>
        /// 参数常量
        /// </summary>
        public const string Param_AppDir = "%AppDir%";
        public const string Param_DataDir = "%DataDir%";
        public const string Param_LogDir = "%LogDir%";
        public const string Param_MailDir = "%MailDir%";

        /// <summary>
        /// SFTP类型常量
        /// </summary>
        public const string Param_UpLoadDir = "%UpLoadDir%";
        public const string Param_SFTPTESTUpLoadDir = "%SFTPTESTUpLoadDir%";
        public const string Param_SFTPUpLoadDir = "%SFTPUpLoadDir%";
        public const string Param_SFTPTESTUpLoadXMLDir = "%SFTPTESTUpLoadXMLDir%";
        public const string Param_SFTPUpLoadXMLDir = "%SFTPUpLoadXMLDir%";


        public const string Param_CurTime = "%CurTime%";
        public const string Param_CurDate = "%CurDate%";
        public const string Param_MaxCount = "%MaxCount%";
        public const string Param_RunType = "%RunType%";

        // 数据库命令相关
        public const string Param_DataIDList = "%DataIDList%";
        public const string Param_HasDataIDList = "%HasDataIDList%";
        public const string Param_HasDataFile = "%HasDataFile%";
        public const string Param_DataAccessCfg = "%DataAccessCfg%";
        public const string Param_PrimaryKeys = "%PrimaryKeys%";
        public const string Param_SplitOperator = "%SplitOperator%";
        public const string Param_RDCName = "%RDC%";
        public const string Param_Info = "%Info%";
        public const string Param_ErrorCount = "%ErrorCount%";

      

        /// <summary>
        /// Zip类型常量
        /// </summary>
        public const string Zip_Type_Compress = "Compress";
        public const string Zip_Type_Decompress = "Decompress";

        /// <summary>
        /// Ftp类型常量
        /// </summary>
        public const string Ftp_Type_Upload = "Upload";
        public const string Ftp_Type_Download = "Download";
        public const string Ftp_Type_Rename = "Rename";
        public const string Ftp_Type_Move = "Move";

        /// <summary>
        /// 文件类型常量
        /// </summary>
        public const string File_Type_Rename = "Rename";

        public const int TIMER_INTERVAL = 1000 * 60 * 1; //1分钟

        /// <summary>
        /// 连接类型常量
        /// </summary>
        public const string Conn_TYPE_ORACLE = "Oracle";
        public const string Conn_TYPE_SQLSERVER = "MS Sql Server";
        public const string Conn_TYPE_TEXT = "Text";


        public const string C_WinService = "WinService";
        public const string C_JobCode = "JobCode";
        public const string C_ValueSplitOperator = "|";
        public const string C_ThreadName = "ThreadName";

        /// <summary>
        /// 运行类型
        /// </summary>
        public const string RunType_Auto = "auto";
        public const string RunType_Manual = "manual";
        public const string RunType_Server = "server";

    }
}
