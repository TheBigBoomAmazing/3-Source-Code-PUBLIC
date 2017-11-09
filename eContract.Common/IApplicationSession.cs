using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eContract.Common
{
    /// <summary>
    /// 当期登录用户的环境变量接口
    /// </summary>
    public interface IApplicationSession
    {
        /// <summary>
        /// 是否为web应用程序
        /// </summary>
        bool IsWeb
        {
            get;
            set;
        }

        /// <summary>
        /// 用户Id
        /// </summary>
        string UserId
        {
            get;
        }
       
        /// <summary>
        /// 用户登录账号
        /// </summary>
        string UserAccount
        {
            get;
        }
        /// <summary>
        /// 当期登录的IP
        /// </summary>
        string IPAddress
        {
            get;
            set;
        }
        /// <summary>
        /// 页面的URL
        /// </summary>
        string FormAddress
        {
            get;
            set;
        }
        /// <summary>
        /// 服务器的地址加网站的地址
        /// </summary>
        string ServerAddress
        {
            get;
            set;
        }

    }

}
