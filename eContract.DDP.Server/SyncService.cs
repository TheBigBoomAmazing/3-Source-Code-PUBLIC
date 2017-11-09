using System;
using eContract.DDP.Common;
using System.Collections.Generic;
using eContract.Log;
using System.Xml;
using System.IO;
using eContract.DDP.Common.CommonJob;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using Amib.Threading;

namespace eContract.DDP.Server
{
    public class SyncService
    {
        /// <summary>
        /// 应用程序目录
        /// </summary>
        public string AppDir = "";

        /// <summary>
        /// 数据目录，存放任务执行过程中产生的数据文件用
        /// 原来只有一个AppDir，数据文件也直接放在AppDir里，但对web不行，
        /// web的AppDir是bin目录，如果产生文件，就会引起session失效，所以加了一个DataDir
        /// 如果DataDir为null或空字符串，则将数据放在AppDir
        /// </summary>
        public string DataDir = "";

     
        public string LogDir = "";

        public string MailDir = "";

        public string UpLoadDir = "";

        //SFTP参数
        public string SFTPTESTUpLoadDir = "";

        public string SFTPUpLoadDir = "";

        public string SFTPTESTUpLoadXMLDir = "";

        public string SFTPUpLoadXMLDir = "";
 
        //Email参数
        public string strEmailServer = "";
        public string strEmailFromAddress = "";
        public string strEmailUser = "";
        public string strEmailPassword = "";

        public string CurrentVersion = "";

        /// <summary>
        /// Schedule发生变化
        /// </summary>
        public event EventHandler ScheduleChanged;

        /// <summary>
        /// 当前Worker
        /// </summary>
        public static SyncService _current = null;
        public static SyncService Current
        {
            get
            {
                if (_current == null)
                {
                    // 设置日志文件名称
                    string strFullyQualifiedName = Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName;
                    string appDir = Path.GetDirectoryName(strFullyQualifiedName);

                    _current = new SyncService(appDir);
                }
                return _current;
            }
        }


        /// <summary>
        /// Job配置文件管理对象
        /// </summary>
        private JobCfgManager _jobCfgManager = null;

        /// <summary>
        /// 时间表管理对象
        /// </summary>
        private ScheduleManager _scheduleManager = null;

        private object _objLock = new object();

        private string  GetXmlNodeText(XmlDocument dom, string strNode)
        {
            XmlNode nodeResouceDir = dom.DocumentElement.SelectSingleNode(strNode);
            if (nodeResouceDir != null)
                return nodeResouceDir.InnerText.Trim();
            else
                return "";
        }


        private string ReplaceDir(string strAppDir,string strDir)
        {
            if (strDir.Length == 0)
            {
                return strAppDir;
            }
            else if (strDir.Length > 2
              && (strDir.Substring(0, 2) == ".\\" || strDir.Substring(0, 2) == "./"))
            {
                return strAppDir + strDir.Substring(1);
            }
            else
                return strDir;
                
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SyncService(string appDir)
        {
            // 应用程序目录
            this.AppDir = appDir;

            // 资源目录
            string resourceDir = "";
            string cfgFile = appDir + "\\" + "cfg.xml";
            if (File.Exists(cfgFile) == true)
            {
                XmlDocument dom = new XmlDocument();
                dom.Load(cfgFile);

                resourceDir = GetXmlNodeText(dom, "ResouceDir");
                this.DataDir = GetXmlNodeText(dom, "DataDir");
          
                this.LogDir = GetXmlNodeText(dom, "LogDir");
                this.UpLoadDir = GetXmlNodeText(dom, "UpLoadDir");
                this.MailDir = GetXmlNodeText(dom, "MailDir");

                this.SFTPTESTUpLoadDir = GetXmlNodeText(dom, "SFTPTESTUpLoadLocalDir");
                this.SFTPUpLoadDir = GetXmlNodeText(dom, "SFTPUpLoadLocalDir");
                this.SFTPTESTUpLoadXMLDir = GetXmlNodeText(dom, "SFTPTESTUpLoadXMLLocalDir");
                this.SFTPUpLoadXMLDir = GetXmlNodeText(dom, "SFTPUpLoadXMLLocalDir");

                //Email
                this.strEmailServer = GetXmlNodeText(dom, "EmailServer");
                this.strEmailFromAddress = GetXmlNodeText(dom, "EmailFromAddress");
                this.strEmailPassword = GetXmlNodeText(dom, "EmailPassword");
            }

            resourceDir = ReplaceDir(this.AppDir, resourceDir);
            this.DataDir = ReplaceDir(this.AppDir, this.DataDir);
            this.LogDir = ReplaceDir(this.AppDir, this.LogDir);
            this.UpLoadDir = ReplaceDir(this.AppDir, this.UpLoadDir);
            this.MailDir = ReplaceDir(this.AppDir, this.MailDir);

            // 自动创建数据目录
            if (Directory.Exists(this.DataDir) == false)
                Directory.CreateDirectory(this.DataDir);

            if (Directory.Exists(this.UpLoadDir) == false)
                Directory.CreateDirectory(this.UpLoadDir);

            if (Directory.Exists(this.LogDir) == false)
                Directory.CreateDirectory(this.LogDir);

            if (Directory.Exists(this.MailDir) == false)
                Directory.CreateDirectory(this.MailDir);

            if (Directory.Exists( this.UpLoadDir+"\\"+this.SFTPTESTUpLoadDir) == false)
                Directory.CreateDirectory(this.UpLoadDir + "\\" + this.SFTPTESTUpLoadDir);

            if (Directory.Exists(this.UpLoadDir + "\\" + this.SFTPUpLoadDir) == false)
                Directory.CreateDirectory(this.UpLoadDir + "\\" + this.SFTPUpLoadDir);

            if (Directory.Exists(this.UpLoadDir + "\\" + this.SFTPTESTUpLoadXMLDir) == false)
                Directory.CreateDirectory(this.UpLoadDir + "\\" + this.SFTPTESTUpLoadXMLDir);

            if (Directory.Exists(this.UpLoadDir + "\\" + this.SFTPUpLoadXMLDir) == false)
                Directory.CreateDirectory(this.UpLoadDir + "\\" + this.SFTPUpLoadXMLDir);

            this._scheduleManager = new ScheduleManager(resourceDir);
            this._scheduleManager.ScheduleChanged += new EventHandler(this.OnScheduleChanged);

            // 初始化任务管理对象
            this._jobCfgManager = new JobCfgManager(resourceDir, this._scheduleManager);


            // 线程池
            STPStartInfo stpStartInfo = new STPStartInfo();
            stpStartInfo.IdleTimeout = 1000;
            stpStartInfo.MaxWorkerThreads = Convert.ToInt32(50);
            stpStartInfo.MinWorkerThreads = Convert.ToInt32(1);
            stpStartInfo.PerformanceCounterInstanceName = "SmartThreadPool";
            stpStartInfo.DisposeOfStateObjects = true;
          

            this._smartThreadPool = new SmartThreadPool(stpStartInfo);

            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(this.AppDir + "\\" + "eContract.SyncData.DDP.UI.exe");
            this.CurrentVersion = versionInfo.FileVersion;
        }

     

        /// <summary>
        /// 初始化内部参数
        /// </summary>
        private void SetInternalParameters(ref Hashtable parameters)
        {
            if (parameters == null)
                parameters = new Hashtable();

            // 应用程序目录
            parameters[DDPConst.Param_AppDir] = this.AppDir;
            parameters[DDPConst.Param_DataDir] = this.DataDir;
            parameters[DDPConst.Param_LogDir] = this.LogDir;
            parameters[DDPConst.Param_UpLoadDir] = this.UpLoadDir;
            parameters[DDPConst.Param_MailDir] = this.MailDir;

            //SFTP参数
            parameters[DDPConst.Param_SFTPUpLoadDir] = this.SFTPUpLoadDir;
            parameters[DDPConst.Param_SFTPTESTUpLoadDir] = this.SFTPTESTUpLoadDir;
            parameters[DDPConst.Param_SFTPTESTUpLoadXMLDir] = this.SFTPTESTUpLoadXMLDir;
            parameters[DDPConst.Param_SFTPUpLoadXMLDir] = this.SFTPUpLoadXMLDir;

            //Email信息
            parameters["EmailServer"] = this.strEmailServer;
            parameters["EmailFromAddress"] = this.strEmailFromAddress;
            parameters["EmailPassword"] = this.strEmailPassword;

            // 当前时间
            parameters[DDPConst.Param_CurTime] = DateTime.Now.ToString("yyyyMMddHHmmss");
            parameters[DDPConst.Param_CurDate] = DateTime.Now.ToString("yyyyMMdd");
        }

        #region 属性

        /// <summary>
        /// Job管理对象
        /// </summary>
        public JobCfgManager JobCfgManager
        {
            get { return _jobCfgManager; }
            set { _jobCfgManager = value; }
        }

        /// <summary>
        /// Schedule管理对象
        /// </summary>
        public ScheduleManager ScheduleManager
        {
            get { return _scheduleManager; }
        }

        #endregion

        /// <summary>
        /// 时间表发生变化，触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnScheduleChanged(Object sender, EventArgs args)
        {
            if (this.ScheduleChanged != null)
            {
                this.ScheduleChanged(this, args);
            }
        }

        /// <summary>
        /// 根据Job code得到Job实例
        /// </summary>
        /// <param name="jobCode"></param>
        /// <returns></returns>
        public IJob GetJobInstance(string jobCode, out JobEntity entity)
        {
            // 创建Job            
            entity = this._jobCfgManager.GetJobByCode(jobCode);
            if (entity == null)
                throw new InvalidOperationException("未找到任务" + jobCode + "对应的配置信息");

            // 先从缓冲中找
            IJob job = this.GetJobInstance(jobCode);


            if (job == null)
            {
                job = this.CreateInstance(entity);

                // 加到缓冲里
                this.JobList.Add(job);
            }

            return job;
        }

        /// <summary>
        /// Job对象集合
        /// </summary>
        public List<IJob> JobList = new List<IJob>();
        public IJob GetJobInstance(string jobCode)
        {
            for (int i = 0; i < JobList.Count; i++)
            {
                IJob job = JobList[i];
                if (job.JobCode == jobCode)
                    return job;
            }

            return null;
        }

       

        public IJob CreateInstance(JobEntity entity)
        {
            string fullAssemblyName = "";
            if (entity.AssemblyName != "")
                fullAssemblyName = this.AppDir + "\\" + entity.AssemblyName;
            IJob job = JobFactory.CreateJobInstance(fullAssemblyName, entity.ClassName);
            if (job == null)
                throw new InvalidOperationException("未能创建任务" + entity.Code + "对象");
            job.JobCode = entity.Code;

            return job;
        }

        /// <summary>
        /// 运行Job
        /// </summary>
        /// <param name="jobCode"></param>
        public void RunJob(string jobCode)
        {
            JobEntity entity = null;
            IJob job = this.GetJobInstance(jobCode,out entity);

            this.RunJob(job, entity,null);
        }


        /// <summary>
        /// 运行Job
        /// </summary>
        /// <param name="jobCode"></param>
        public void RunJob(string jobCode,Hashtable parameters)
        {
            JobEntity entity = null;
            IJob job = this.GetJobInstance(jobCode, out entity);

            this.RunJob(job, entity, parameters);
        }

        //public Hashtable JobThreads = new Hashtable();

        /// <summary>
        /// 运行完成事件
        /// </summary>
        public event EventHandler RunJobFinished;


        public SmartThreadPool _smartThreadPool = null;
        //public Hashtable JobResultList = new Hashtable();

        public List<string> RunningJobs = new List<string>();

        /// <summary>
        /// 运行指定的Job
        /// </summary>
        /// <param name="job"></param>
        public void RunJob(IJob job, JobEntity entity, Hashtable parameters)
        {

            // 设置全局参数
            this.SetInternalParameters(ref parameters);


            // 采用多线程运行
            string threadName = entity.Code + "~" + DateTime.Now.ToString("yyyyMMddHHmmss");
            JobHelper jobHelper = new JobHelper(job, parameters, threadName, entity);
            jobHelper.RunFinished -= new EventHandler(this.JobFinished);
            jobHelper.RunFinished += new EventHandler(this.JobFinished);

            //如果在下一个任务开始之前前一个任务还没有执行完成，重启线程
            List<string> DeleteJob = new List<string>();
            foreach (string strJob in RunningJobs)
            {
                if (strJob.Contains(entity.Code))
                {
                    DeleteJob.Add(strJob);
                    _smartThreadPool.Shutdown();
                }
            }

            foreach (string strJobs in DeleteJob)
            {
                if (RunningJobs.Contains(strJobs))
                    RunningJobs.Remove(strJobs);
            }

            // 加到任务集合里
            lock (this._objLock)
            {
                if (!RunningJobs.Contains(jobHelper.ThreadName))
                {
                    LogManager.Current.WriteCommonLog(jobHelper.JobEntity.Code, "传入任务" + jobHelper.ThreadName, jobHelper.ThreadName);
                    RunningJobs.Add(jobHelper.ThreadName);
                }
            }

            IWorkItemResult result = this._smartThreadPool.QueueWorkItem(jobHelper.Run);
          
        }

        /// <summary>
        /// 任务完成后移出线程
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void JobFinished(object Sender, EventArgs e)
        {
            lock (this._objLock)
            {
                JobHelper jobHelper = (JobHelper)Sender;
                if (this.RunningJobs.IndexOf(jobHelper.ThreadName) == -1)
                {
                    LogManager.Current.WriteErrorLog(jobHelper.JobEntity.Code, "任务异常，运行的任务集合中未发现" + jobHelper.ThreadName, jobHelper.ThreadName);
                    return;
                }


                // 从集合中移出
                bool bRet = this.RunningJobs.Remove(jobHelper.ThreadName);
                if (bRet == true)
                    LogManager.Current.WriteCommonLog(jobHelper.JobEntity.Code, "移出任务" + jobHelper.ThreadName,jobHelper.ThreadName);
                else
                    LogManager.Current.WriteCommonLog(jobHelper.JobEntity.Code, "失败：移出任务" + jobHelper.ThreadName, jobHelper.ThreadName);
                if (this.RunningJobs.IndexOf(jobHelper.ThreadName) != -1)
                {
                    LogManager.Current.WriteErrorLog(jobHelper.JobEntity.Code, "任务异常，运行的任务集合中已移出" + jobHelper.ThreadName + "，但还存在",jobHelper.ThreadName);

                    // 从集合中移出
                    bool bRet1 = this.RunningJobs.Remove(jobHelper.ThreadName);
                    if (bRet1 == true)
                        LogManager.Current.WriteCommonLog(jobHelper.JobEntity.Code, "强制移出任务" + jobHelper.ThreadName, jobHelper.ThreadName);
                    else
                        LogManager.Current.WriteCommonLog(jobHelper.JobEntity.Code, "失败：强制移出任务" + jobHelper.ThreadName, jobHelper.ThreadName);

                }

                // 触发完成事件
                if (this.RunJobFinished != null)
                    this.RunJobFinished(jobHelper, EventArgs.Empty);
            }
        }

       
        public void RunJobNoThread(string jobCode, Hashtable parameters)
        {
            // 创建Job            
            JobEntity entity = this._jobCfgManager.GetJobByCode(jobCode);
            if (entity == null)
                throw new InvalidOperationException("未找到任务" + jobCode + "对应的配置信息");

            // 创建一个新对象
            IJob job = this.CreateInstance(entity);

            // 运行任务
            this.SetInternalParameters(ref parameters);
            job.RunSafe(parameters, entity,entity.Code);

        }

        #region 管理正在运行的job

        /*
        public List<string> RunningJobList = new List<string>();

        /// <summary>
        /// 检查job是否正在运行
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private bool CheckJobIsRun(string jobCode)
        {
            if (this.RunningJobList.IndexOf(jobCode) == -1)
                return false;
            return true;
        }

        /// <summary>
        /// 设置Job正在运行
        /// </summary>
        /// <param name="p"></param>
        private void SetJobRun(string jobCode)
        {
            this.RunningJobList.Add(jobCode);            
        }

        /// <summary>
        /// 设置job非运行
        /// </summary>
        /// <param name="p"></param>
        public void RemoveJobRun(string jobCode)
        {
            this.RunningJobList.Remove(jobCode);
        }
        */
        #endregion


        /// <summary>
        /// 检查当前时刻是否有到达的Job，如果存在，则依次执行他们
        /// </summary>
        public void CheckAndExecute(DateTime time)
        {
            // 获取到达的任务
            List<ScheduleEntity> scheduleList = this._scheduleManager.GetArriveJob(time);

            // 没有到达的批次
            if (scheduleList.Count == 0)
            {
                // 写日志
                //LogManager.Current.WriteCommonLog("", "找到0个到达时间点的任务","");
                return;
            }

            // 写日志
            LogManager.Current.WriteCommonLog("", "找到" + scheduleList.Count.ToString() + "个到达时间点的任务","");
            Hashtable parameters = new Hashtable();
            parameters[DDPConst.Param_RunType] = DDPConst.RunType_Auto;


            // 依次执行每个任务
            for (int i = 0; i < scheduleList.Count; i++)
            {

                ScheduleEntity scheduleEntity = scheduleList[i];
                try
                {
                    this.RunJob(scheduleEntity.JobCode,parameters);
                }
                catch (Exception ex)
                {
                    // 写日志
                    LogManager.Current.WriteExceptionLog(scheduleEntity.JobCode, "执行时异常", ex,"*");               

                    // 继续下一个任务
                    continue;
                }
              

                // 设批次最后执行时间
                DateTime curTime = DateTime.Now;
                if (String.Compare(scheduleEntity._lastRunTime.ToString(), curTime.ToString()) < 0)
                {
                    //日志
                    //LogManager.Current.WriteCommonLog(scheduleEntity.JobCode, "最后执行时间'" + scheduleEntity._lastRunTime.ToString() + "'小于当前时间'" + curTime.ToString() + "'，重设最后运行时间");

                    scheduleEntity._lastRunTime = curTime;
                }
            }
        }


     

    }
}
