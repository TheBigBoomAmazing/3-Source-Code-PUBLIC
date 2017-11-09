using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace eContract.DDP.Common
{
     
    /// <summary>
    /// 邮件发送状态
    /// </summary>
    public enum MailSendState
    {
        MailState_Pre = 0,//未发送的,待处理的邮件
        MailState_Success = 1,//发送成功的邮件
        MailState_Error = 2//发送失败的邮件
    }

    public enum DataStatus
    {
        Yes = 1,//有效数据
        No = -1 //无效数据
    }

    public enum ClientStatus
    {
        NoInstaller = 0,//未安装
        Test = 1 ,//测试状态
        Normal=2,//正式
        Deleted=-1//删除
    }

    public enum SysParameter
    { 
        // 产品编码规则
        ArticleNoRule = 1011,

        //数据上传截止日期
        StartUpLoadDate = 1021,

        //邮件用户名
        EmailServerUser = 1031,
        //邮件密码
        EmailServerPassword = 1032,
        //邮件服务器
        EmailServer = 1033,
        //邮件附件大小限制
        EmailAttachmentLength = 1034,
        //代理
        EmailProxy = 1035,

        //发件人地址
        EmailFromAddress = 1036,

        //SFTP 密码
        SFTPPwd = 1042,
        //SFTP 用户
        SFTPUser = 1043,
        //SFTP 地址
        SFTPAddress = 1044,
        //SFTP端口
        SFTPPort = 1041

    }

    public enum SysParameterType
    {
        EmailToUser,

        SystemEmailToUser,

        EmailCatalog,

        SFTPCatalog 

    }

    public enum ErrorDataType
    {
        ErrorData = -1,

        ClientNoUpLoad = 0,

        NoStore = 2,

        ArticleError = 3,

        Overdue = 4,

        Success = 5 
    }

    public enum FieldsType
    {
        [Description("INT")]
        Int = 0,
        [Description("DECIMAL")]
        Decimal = 1,
        [Description("NVARCHAR")]
        Nvarchar = 2,
        [Description("DATETIME")]
        DateTime = 3
    }

    public enum UserType
    {
        SystemAdmin=0,
        BiUser=1,
        RegionUser=2,
        CustomerUser=3,
        SFUser=4
    }

}




