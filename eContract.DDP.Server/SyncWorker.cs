using System;
using System.Collections.Generic;
using System.Text;
using eContract.DDP.Common;
using System.IO;
//using System.Windows.Forms;
using System.Reflection;
using System.Timers;
using eContract.Log;
using System.Threading;

namespace eContract.DDP.Server
{
    public class SyncWorker
    {
        // timer间隔时间
        public const int TIMER_INTERVAL = 1000 * 60 * 1; //10分钟

        /// <summary>
        /// timer,用来间隔一定时间检查一次
        /// </summary>
        private System.Timers.Timer _timer = null;

        /// <summary>
        /// 是否正在工作
        /// </summary>
        public bool Working = false;

        /// <summary>
        /// 构造函数
        /// </summary>
        public SyncWorker()
        {
            // 设置日志文件名称
            string strFullyQualifiedName = Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName;
            string appDir = Path.GetDirectoryName(strFullyQualifiedName);


            // Timer对象
            this._timer = new System.Timers.Timer();
            this._timer.Interval = SyncWorker.TIMER_INTERVAL;
            this._timer.Elapsed += new ElapsedEventHandler(this.TimerTick);

          
        }

        /// <summary>
        /// 当前Worker
        /// </summary>
        public static SyncWorker _current = null;
        public static SyncWorker CurrentWorker
        {
            get
            {
                if (_current == null)
                {
                    _current = new SyncWorker();
                }
                return _current;
            }
        }

        /// <summary>
        /// 开始工作
        /// </summary>
        public void StartWork()
        {
            if (this.Working == true)
            {
                //MessageBox.Show("SyncWorker已经开始工作了");
                return;
            }

            LogManager.Current.WriteCommonLog("","DDP Service开始工作","");
            this.Working = true;
            this._timer.Enabled = true;

        }

        /// <summary>
        /// 结束工作
        /// </summary>
        public void EndWork()
        {
            // 停止类型为RunWhenStartJob

            this.Working = false;
            this._timer.Enabled = false;
            LogManager.Current.WriteCommonLog("", "DDP Service结束工作","");
        }

        /// <summary>
        /// Timer定时检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerTick(object sender, ElapsedEventArgs e)
        {
            //this._timer.Enabled = false;

            DateTime time = e.SignalTime;// DateTime.Now;

            //LogManager.Current.WriteCommonLog("", "检查时间:" + time.ToString(),"");

            //LogManager.Current.WriteCommonLog("", "检查任务时间", "");

            // 检查是否有到达的任务，如果有同时执行到达的任务
            SyncService.Current.CheckAndExecute(time);

            //if (this.Working == true)
            //{
            //    this._timer.Enabled = true;
            //    LogManager.Current.WriteCommonLog("", "开始自动任务检查", "");
            //}
        }

    }
}
