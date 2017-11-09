using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using eContract.DDP.Common;
using System.Collections;

namespace eContract.DDP.Common.Command
{
    /// <summary>
    /// 命令接口
    /// </summary>
    public interface ICommand
    {

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="paramters">参数</param>
        /// <param name="node">命令节点</param>
        void Initialize(Hashtable paramters, XmlNode node);


        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="paramters">参数，在各个命令中参数可能会增加</param>
        /// <param name="error">出错信息</param>
        /// <returns>是否成功</returns>
        ResultCode Execute(ref Hashtable paramters, out string error);
    }


    public enum ResultCode
    {
        Success=0,
        Error=1,
        Break,
    }
}
