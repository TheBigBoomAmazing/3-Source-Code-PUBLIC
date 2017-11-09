using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;
using System.Windows.Forms;

namespace eContract.DDP.Common.CommonJob
{
    public interface IJob
    {
        string JobCode { get;set;}
        string ThreadName { get;set;}

        void RunSafe(Hashtable parameters, JobEntity jobEntity, string threadName);

        /// <summary>
        /// 得到配置窗体
        /// </summary>
        /// <returns></returns>
        JobCfgForm GetCfgForm();

        /// <summary>
        /// 获取参数事件
        /// </summary>
        event GetParamsEventHandle GetParameters;


        /// <summary>
        /// 得到运行UI
        /// </summary>
        /// <returns></returns>
        JobRunControl GetRunControl();


    }

    public delegate void GetParamsEventHandle(object sender, GetParamsEventArge e);
    public class GetParamsEventArge
    {
        public Hashtable Parameters = new Hashtable();
    }

}
