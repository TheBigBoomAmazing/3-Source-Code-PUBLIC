using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using eContract.Log;
using eContract.DDP.Common.CommonJob;
using System.Collections;
using eContract.DDP.Common;
using System.Net.Mail;

namespace eContract.DDP.Server
{
    public class JobHelper
    {
        private IJob _job = null;
        public Hashtable Parameters = null;
        public string ThreadName = "";
        public JobEntity JobEntity = null;
        public bool IsRunError = false;

        /// <summary>
        /// 运行完成事件
        /// </summary>
        public event EventHandler RunFinished;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="job"></param>
        /// <param name="parameters"></param>
        /// <param name="threadName"></param>
        /// <param name="jobEntity"></param>
        public JobHelper(IJob job, Hashtable parameters,string threadName,JobEntity jobEntity)
        {
            this._job = job;
            this.Parameters = parameters;
            this.ThreadName = threadName;
            this.JobEntity = jobEntity;
        }
        
        /// <summary>
        /// 运行
        /// </summary>
        public void Run()
        {

            LogManager.Current.WriteCommonLog(this.JobEntity.Code, "运行开始" + this.ThreadName, this.ThreadName);
            try
            {
                // 执行
                this._job.RunSafe(Parameters, this.JobEntity, this.ThreadName);
                IsRunError = true;
            }
            catch 
            {
                IsRunError = false;
                
            }
            finally
            {
                LogManager.Current.WriteCommonLog(this.JobEntity.Code, "运行结束" + this.ThreadName, this.ThreadName);
                // 触发完成事件
                if (this.RunFinished != null)
                {
                    this.RunFinished(this, EventArgs.Empty);
                }
            }
        }

    }
}
