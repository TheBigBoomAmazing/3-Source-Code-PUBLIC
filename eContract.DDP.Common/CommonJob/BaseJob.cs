using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;
using eContract.DDP.Common.Command;
using eContract.DDP.Common.CommonJob;
using eContract.DDP.Common;
using System.Threading;
using System.Windows.Forms;
using System.Net.Mail;
using eContract.Log;

namespace eContract.DDP.Common.CommonJob
{
    public class BaseJob:IJob
    {
        /// <summary>
        /// Job实体
        /// </summary>
        public JobEntity JobEntity = null;

        /// <summary>
        /// 命令集合
        /// </summary>
        public List<ICommand> CommandList = new List<ICommand>();

        private string _jobCode = "";
        /// <summary>
        /// 任务代码
        /// </summary>
        public string JobCode
        {
            get
            {
                return this._jobCode;
            }
            set
            {
                this._jobCode = value;
            }
        }

        private string _threadName = "";
        public string ThreadName
        {
            get
            {
                return this._threadName;
            }
            set
            {
                LogManager.Current.WriteCommonLog(this.JobCode, "设_threadName开始,原:" + this._threadName + " 新:" + value, value);

                this._threadName = value;

                LogManager.Current.WriteCommonLog(this.JobCode, "设_threadName结束", value);

            }
        }

        /// <summary>
        /// 安全方式下的运行
        /// </summary>
        /// <param name="parameters"></param>
        public virtual void RunSafe(Hashtable parameters, JobEntity jobEntity,string threadName)
        {
            lock (this)
            {
                try
                {
                    LogManager.Current.WriteCommonLog(this.JobCode, "RunSafe开始", threadName);


                    this.ThreadName = threadName;

                    this.Initialize(parameters, jobEntity);


                    this.Run(parameters);

                    LogManager.Current.WriteCommonLog(this.JobCode, "RunSafe结束", threadName);

                }
                catch (Exception ex)
                {
                    // 写日志
                    LogManager.Current.WriteExceptionLog(this.JobEntity.Code, "执行时异常", ex, this.ThreadName);

                    throw ex;
                }
                //catch
                //{
                //    // 写错误日志
                //    LogManager.Current.WriteErrorLog(this.JobEntity.Code, "未识别的异常", this.ThreadName);
                //}
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="jobEntity"></param>
        public virtual void Initialize(Hashtable internalParameters, JobEntity jobEntity)
        {
            // 获取外部参数
            Hashtable parameters = new Hashtable();
            this.JobEntity = jobEntity;

            XmlNodeList parameterList = jobEntity.JobNode.SelectNodes("Parameters/Parameter");
            for (int i = 0; i < parameterList.Count; i++)
            {
                XmlNode parameterNode = parameterList[i];
                string paramName = XmlUtil.GetAttrValue(parameterNode,"Name");
                if (internalParameters.ContainsKey(paramName) == true)
                    continue;

                string paramValue = XmlUtil.GetAttrValue(parameterNode,"Value");

                // 先替换掉内部的参数
                paramValue = BaseCommand.ReplaceParameters(internalParameters, paramValue);
                // 再替换一下外部前面定义的参数
                paramValue = BaseCommand.ReplaceParameters(parameters, paramValue);

                parameters[paramName] = paramValue;
            }

            // 传入命令的参数，要包括内部参数和外部参数
            foreach (DictionaryEntry parameter in internalParameters)
            {
                parameters[parameter.Key.ToString()] =parameter.Value.ToString();
            }

            CommandList.Clear();
            string appDir = (string)parameters[DDPConst.Param_AppDir];
            parameters[DDPConst.C_JobCode] = this.JobEntity.Code;
            parameters[DDPConst.C_ThreadName] = this.ThreadName;
            XmlNode commandsNode = jobEntity.JobNode.SelectSingleNode("Commands");
            if (commandsNode != null)
            {
                for (int i = 0; i < commandsNode.ChildNodes.Count; i++)
                {
                    XmlNode node = commandsNode.ChildNodes[i];
                    if (!(node is XmlElement))
                        continue;

                    ICommand command = CommandFactory.CreateCommand(appDir,node);                 
                    if (command != null)
                    {                       
                        // 初始化
                        command.Initialize(parameters, node);

                        // 加入命令列表
                        this.CommandList.Add(command);
                    }
                }
            }

            LogManager.Current.WriteCommonLog(this.JobCode, "初始化完成", this.ThreadName);
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="parameters"></param>
        public virtual void Run(Hashtable parameters)
        {
            string error = "";
            ResultCode retCode = ResultCode.Success;

            if (this.CommandList != null)
            {
                for (int i = 0; i < this.CommandList.Count; i++)
                {
                    ICommand command = this.CommandList[i];

                    //// 写日志
                    LogManager.Current.WriteCommonLog(this.JobCode, command.ToString() + " 开始", this.ThreadName);


                    retCode = command.Execute(ref parameters, out error);

                    //// 写日志
                    LogManager.Current.WriteCommonLog(this.JobCode, command.ToString() +" 完成", this.ThreadName);


                    if (retCode == ResultCode.Error)
                    {
                        // 写日志
                        LogManager.Current.WriteErrorLog(JobEntity.Code, command.ToString() + "命令出错:" + error, this.ThreadName);
                        return;
                    }
                    else if (retCode == ResultCode.Break)
                    {
                        // 写日志
                        LogManager.Current.WriteCommonLog(JobEntity.Code, "不需继续处理。", this.ThreadName);
                        continue;
                    }
                }
            }


        }




        #region IJob Members


        public virtual JobCfgForm GetCfgForm()
        {
            return new BaseJobCfgForm();
        }

        public virtual JobRunControl GetRunControl()
        {
            return new JobRunControl();
        }

        public event GetParamsEventHandle GetParameters;

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="info"></param>
        public void OnGetParameters(Hashtable parameters)
        {
            if (GetParameters != null)
            {
                GetParamsEventArge e = new GetParamsEventArge();
                e.Parameters = parameters;
                this.GetParameters(this, e);
            }
        }
        #endregion
    }
}
