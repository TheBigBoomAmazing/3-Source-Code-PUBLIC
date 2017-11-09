using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using eContract.DDP.Common;
using eContract.DDP.Server;
using System.Diagnostics;
using eContract.Log;
using System.Text.RegularExpressions;
using System.Net.Mail;
using eContract.DDP.Common.CommonJob;
using Suzsoft.Smart.Data;
using System.Collections;
using System.Data.SqlClient;

namespace eContract.DDP.UI
{
    public partial class frmMain : frmBaseMain
    {
        /// <summary>
        /// 当前job对象
        /// </summary>
        IJob _job = null;


        /// <summary>
        /// 构造函数
        /// </summary>
        public frmMain()
        {
            InitializeComponent();

            try
            {
                SyncService.Current.RunJobFinished += new EventHandler(this.RunJobFinished);
                SyncService.Current.ScheduleChanged += new EventHandler(this.OnScheduleChanged);

               
            }
            catch (SqlException ex)
            {
                MessageBox.Show(this, "数据库连接失败:" + ex.Message, "数据同步");
                this.Close();
                Application.Exit();
            }
        }


        /// <summary>
        /// 时间表发生变化，刷新界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnScheduleChanged(Object sender, EventArgs args)
        {
            this.InitialScheduleList();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Load(object sender, EventArgs e)
        {
          
            // 初始化Job列表
            this.InitialJobList();

            // 初始化Schedule列表
            this.InitialScheduleList();

            // 初始化Log Type
            this.InitialLogCategory();

            // 初始化Log Type
            this.InitialJobCode();

           

            base.InitTitle(true   ,"后台管理-测试版");

            this.picSchedule.Visible = false;
            this.picViewLog.Visible = false;

            Control.CheckForIllegalCrossThreadCalls = false;
            SyncWorker.CurrentWorker.StartWork();
            if (SyncWorker.CurrentWorker.Working)
                this.btnScheduleStart.Enabled = false;
            else
                this.btnScheduleEnd.Enabled = false;

            this.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
        }


        #region Job 相关

        /// <summary>
        /// 初始化Job列表
        /// </summary>
        public void InitialJobList()
        {           

            DataTable dt = new DataTable();
            dt.Columns.Add("Code");
            dt.Columns.Add("Name");
            dt.Columns.Add("AssemblyName");
            dt.Columns.Add("ClassName");
            for (int i = 0; i < SyncService.Current.JobCfgManager.Count; i++)
            {
                JobEntity jobEntity = SyncService.Current.JobCfgManager[i];
                DataRow row = dt.NewRow();
                row["Code"] = jobEntity.Code;
                row["Name"] = jobEntity.Name;
                row["AssemblyName"] = jobEntity.AssemblyName;
                row["ClassName"] = jobEntity.ClassName;
                dt.Rows.Add(row);
            }
            this.dataGridView1.DataSource = dt;

        }


        /// <summary>
        /// 任务完成后移出线程
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void RunJobFinished(object Sender, EventArgs e)
        {
            JobHelper jobHelper = (JobHelper)Sender;

            string runType = (string)jobHelper.Parameters[DDPConst.Param_RunType];

           
            if (jobHelper.IsRunError)
            {
                this.lblInfo.Text = "完成任务：" + jobHelper.JobEntity.Name;
                // 手动运行
                if (runType == DDPConst.RunType_Manual)
                    MessageBox.Show(this, "完成任务：" + jobHelper.JobEntity.Name, "数据同步");
            }
            else
            {
                this.lblInfo.Text = "执行任务：" + jobHelper.JobEntity.Name+"出错！";
                if (runType == DDPConst.RunType_Manual)
                    MessageBox.Show(this, "执行任务：" + jobHelper.JobEntity.Name + "出错！", "数据同步");
            }

            // 刷新日志
            //this.InitialLog();
        }



        #region 右键菜单
        /// <summary>
        /// 修改job信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_job_edit_Click(object sender, EventArgs e)
        {
            this.PicJobParameter_Click(null, null);
        }

        #endregion

        #endregion

        #region schedule相关

        /// <summary>
        /// 初始化Schedule列表
        /// </summary>
        public void InitialScheduleList()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ScheduleID");
            dt.Columns.Add("JobCode");
            dt.Columns.Add("JobName");
            dt.Columns.Add("ScheduleType");
            dt.Columns.Add("StartTime");
            dt.Columns.Add("EveryWeekDay");
            dt.Columns.Add("EveryMonthDay");
            dt.Columns.Add("EndTime");
            dt.Columns.Add("IntervalMinutes");

            for (int i = 0; i < SyncService.Current.ScheduleManager.Count; i++)
            {
                ScheduleEntity entity = SyncService.Current.ScheduleManager[i];
                JobEntity jobentity = SyncService.Current.JobCfgManager.GetJobByCode(entity.JobCode);

                DataRow row = dt.NewRow();
                row["ScheduleID"] = entity.ScheduleID;
                row["JobCode"] = entity.JobCode;
                if (jobentity != null)
                    row["JobName"] = jobentity.Name;
                row["ScheduleType"] = entity.ScheduleType;
                row["StartTime"] = entity.StartTime;
                row["EveryWeekDay"] = entity.EveryWeekDay == 0 ? "" : entity.EveryWeekDay.ToString();
                row["EveryMonthDay"] = entity.EveryMonthDay == 0 ? "" : entity.EveryMonthDay.ToString();
                row["EndTime"] = entity.EndTime;
                row["IntervalMinutes"] = entity.IntervalMinutes == 0 ? "" : entity.IntervalMinutes.ToString();
                dt.Rows.Add(row);
            }
            this.dgv_schedule.DataSource = dt;
        }

        /// <summary>
        /// Schedule开始运行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnScheduleStart_Click(object sender, EventArgs e)
        {
            if (SyncWorker.CurrentWorker.Working == true)
            {
                MessageBox.Show(this, "任务已经开始工作了", "数据同步");
                return;
            }
            SyncWorker.CurrentWorker.StartWork();
            this.btnScheduleStart.Enabled = false  ;
            this.btnScheduleEnd.Enabled = true;
            MessageBox.Show(this, "任务计划启动成功", "数据同步");
        }

        /// <summary>
        /// Schedule结束运行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnScheduleEnd_Click(object sender, EventArgs e)
        {
            SyncWorker.CurrentWorker.EndWork();
            this.btnScheduleEnd.Enabled = false;
            this.btnScheduleStart.Enabled = true ;
            MessageBox.Show(this, "任务计划结束成功", "数据同步");

        }

        /// <summary>
        /// 新建schedule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_schedule_new_Click(object sender, EventArgs e)
        {
            frmSchedule dlg = new frmSchedule(SyncService.Current,null);
            dlg.StartPosition = FormStartPosition.CenterScreen;
            dlg.ShowDialog();
            if (dlg.DialogResult == DialogResult.OK)
            {
                // 刷新界面Schedule列表
                this.InitialScheduleList();
            }
        }

        /// <summary>
        /// 修改Schedule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_schedule_edit_Click(object sender, EventArgs e)
        {
            if (this.dgv_schedule.SelectedRows.Count == 0)
            {
                MessageBox.Show(this, "尚未选择要编辑的事项", "数据同步");
                return;
            }

            DataGridViewRow row = this.dgv_schedule.SelectedRows[0];
            string scheduleID = row.Cells[0].Value.ToString();
            ScheduleEntity scheduleEntity = SyncService.Current.ScheduleManager.GetScheduleEntityByID(scheduleID);

            frmSchedule dlg = new frmSchedule(SyncService.Current,scheduleEntity);
            dlg.StartPosition = FormStartPosition.CenterScreen;
            dlg.ShowDialog();
            if (dlg.DialogResult == DialogResult.OK)
            {
                // 刷新界面Schedule列表
                this.InitialScheduleList();
            }
        }

        /// <summary>
        /// 删除schedule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_schedule_delete_Click(object sender, EventArgs e)
        {
            if (this.dgv_schedule.SelectedRows.Count ==0)
            {
                MessageBox.Show(this, "尚未选择要删除的事项","数据同步");
                return;
            }

            DataGridViewRow row = this.dgv_schedule.SelectedRows[0];
            string scheduleID = row.Cells[0].Value.ToString();
            string scheduleName = row.Cells[2].Value.ToString();
            ScheduleEntity scheduleEntity = SyncService.Current.ScheduleManager.GetScheduleEntityByID(scheduleID);

            // 询问确认删除吗
            DialogResult result = MessageBox.Show(this,
                "确认删除\"" + scheduleName + "\"的执行时间设置吗?",
                "数据同步",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (result != DialogResult.Yes)
                return;

            // 删除
            SyncService.Current.ScheduleManager.DeleteSchedule(scheduleEntity);

            // 刷新界面Schedule列表
            this.InitialScheduleList();
        }

        /// <summary>
        /// 双击Schedule进入到编辑界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvSchedule_DoubleClick(object sender, EventArgs e)
        {
            if (this.dgv_schedule.SelectedRows.Count == 0)
                return;

            DataGridViewRow row = this.dgv_schedule.SelectedRows[0];
            string scheduleID = row.Cells[0].Value.ToString();

            ScheduleEntity scheduleEntity = SyncService.Current.ScheduleManager.GetScheduleEntityByID(scheduleID);
            frmSchedule dlg = new frmSchedule(SyncService.Current,scheduleEntity);
            dlg.StartPosition = FormStartPosition.CenterScreen;
            dlg.ShowDialog();
            if (dlg.DialogResult == DialogResult.OK)
            {
                // 刷新界面Schedule列表
                this.InitialScheduleList();
            }
        }

        #endregion

        #region 日志相关

        /// <summary>
        /// 初始化日志分类
        /// </summary>
        private void InitialLogCategory()
        {
            this.cbLogType.Items.Clear();
            this.cbLogType.Items.Add("全部");
            this.cbLogType.Items.Add(LogConst.LOG_CATEGORY_COMMON_STRING);
            this.cbLogType.Items.Add(LogConst.LOG_CATEGORY_ERROR_STRING);
            this.cbLogType.Items.Add(LogConst.LOG_CATEGORY_EXCEPTION_STRING);
            this.cbLogType.SelectedIndex = 0;
        }


        public void InitialJobCode()
        {
            this.cmbJobCode.DataSource = null;
            DataTable dt = new DataTable();
            dt.Columns.Add("JobCode");
            dt.Columns.Add("JobName");

            DataRow dr = dt.NewRow();
            dr[0] = "";
            dr[1] = "";
            dt.Rows.Add(dr);

            for (int i = 0; i < SyncService.Current.JobCfgManager.Count; i++)
            {
                JobEntity jobEntity = SyncService.Current.JobCfgManager[i];
                dr = dt.NewRow();
                dr[0] = jobEntity.Code;
                dr[1] = jobEntity.Name;
                dt.Rows.Add(dr);
            }

            this.cmbJobCode.DataSource = dt;
            this.cmbJobCode.DisplayMember = "JobName";
            this.cmbJobCode.ValueMember = "JobCode";

            this.cmbJobCode.SelectedIndex = 0;
        }

        /// <summary>
        /// Tab切换到job页面时，初始化日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "tabPageLog")
            {
                this.InitialLog();
            }
        }

        /// <summary>
        /// 初始化日志
        /// </summary>
        public void InitialLog()
        {
            string logCategory = this.cbLogType.Text.Trim();
            int nCategory = LogEntity.CategoryString2Int(logCategory);

            string jobCode = this.cmbJobCode.SelectedValue.ToString().Trim();

            this.Cursor = Cursors.WaitCursor;
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Time");
                dt.Columns.Add("CategoryString");
                dt.Columns.Add("JobCode");
                dt.Columns.Add("JobName");
                dt.Columns.Add("Description");
                dt.Columns.Add("ExceptionType");
                dt.Columns.Add("ExceptionMessage");
                dt.Columns.Add("ThreadName");

                List<LogEntity> entityList = LogManager.Current.GetLogs(nCategory, jobCode);
                for (int i = 0; i < entityList.Count; i++)
                {
                    LogEntity entity = entityList[i];
                    DataRow row = dt.NewRow();
                    row["Time"] = entity.Time;
                    row["CategoryString"] = entity.CategoryString;
                    row["JobCode"] = entity.JobCode;
                    if (!string.IsNullOrEmpty(entity.JobCode))
                    {
                        JobEntity jobentity = SyncService.Current.JobCfgManager.GetJobByCode(entity.JobCode);
                        if (jobentity != null)
                            row["JobName"] = jobentity.Name;
                    }
                    row["Description"] = entity.Description;
                    row["ExceptionType"] = entity.ExceptionType;
                    row["ExceptionMessage"] = entity.ExceptionMessage;
                    row["ThreadName"] = entity.ThreadName;

                    dt.Rows.Add(row);
                }

                this.dgv_log.DataSource = dt;
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
        }


        /// <summary>
        /// 刷新日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefreshLog_Click(object sender, EventArgs e)
        {
            this.InitialLog();
        }

        /// <summary>
        /// 日志类型切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbLogType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "tabPageLog")
                this.InitialLog();
        }

        private void dgv_log_DoubleClick(object sender, EventArgs e)
        {
            frmMessage dlg = new frmMessage();
            dlg.StartPosition = FormStartPosition.CenterScreen;

            DataGridViewRow row = this.dgv_log.SelectedRows[0];
            string info = row.Cells[0].Value + "\r\n"
                + row.Cells[1].Value + "\r\n"
                + row.Cells[2].Value + "\r\n"
                + row.Cells[3].Value + "\r\n"
                + row.Cells[4].Value + "\r\n"
                + row.Cells[5].Value + "\r\n"
                + row.Cells[6].Value + "\r\n"
                + row.Cells[7].Value;

            dlg.SetInfo(info);
            dlg.ShowDialog(this);
        }


        #endregion


        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SyncWorker._current != null)
            {
                if (SyncService.Current.RunningJobs.Count > 0)
                {
                    string jobs = "";

                    for (int i = 0; i < SyncService.Current.RunningJobs.Count; i++)
                    {
                        if (jobs != "")
                            jobs += "\r\n";

                        jobs += SyncService.Current.RunningJobs[i];
                    }

                    MessageBox.Show(this, "目前下列任务线程在运行，不能关闭程序。\r\n"
                        + jobs + "\r\n"
                        + "如果您确认要关闭程序，请先在时间表页面点击\"任务计划结束\"停止定时器检查，\r\n"
                        + "这样上述任务执行完，您就可以关闭程序了。", "数据同步");
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show(this, "尚未选择要修改Job", "数据同步");
                return;
            }

            DataGridViewRow row = this.dataGridView1.SelectedRows[0];// this.lvJobList.SelectedItems[0];
            string jobCode = row.Cells[0].Value.ToString();// item.Text;
            JobEntity entity = null;// SyncService.Current.JobCfgManager.GetJobByCode(jobCode);

            IJob job = SyncService.Current.GetJobInstance(jobCode, out entity);

            JobCfgForm form = job.GetCfgForm();
            form.SetJobEntity(entity);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog(this);
        }

   

        private void dgv_schedule_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgv_schedule.SelectedRows.Count == 0)
                return;

            DataGridViewRow row = this.dgv_schedule.SelectedRows[0];
            string scheduleID = row.Cells[0].Value.ToString();

            ScheduleEntity scheduleEntity = SyncService.Current.ScheduleManager.GetScheduleEntityByID(scheduleID);
            frmSchedule dlg = new frmSchedule(SyncService.Current, scheduleEntity);
            dlg.StartPosition = FormStartPosition.CenterScreen;
            dlg.ShowDialog();
            if (dlg.DialogResult == DialogResult.OK)
            {
                // 刷新界面Schedule列表
                this.InitialScheduleList();
            }
        }

        #region PictureBox
        private void picButton_Click(object sender, EventArgs e)
        {
            PictureBox pic = sender as PictureBox;
            this.tabControl1.SelectedIndex = int.Parse(pic.Tag.ToString());

            foreach (Control ctr in panel5.Controls)
            {
                if (ctr is PictureBox)
                {
                    if (!ctr.Tag.ToString().Equals(pic.Tag.ToString()))
                    {
                        pic.Visible = false;
                    }
                }
            }

        }

        private void lblButton_Click(object sender, EventArgs e)
        {
            Label lbl = sender as Label;
            this.tabControl1.SelectedIndex = int.Parse(lbl.Tag.ToString());
            foreach (Control ctr in panel5.Controls)
            {
                if (ctr is PictureBox)
                {
                    if (ctr.Tag.ToString().Equals(lbl.Tag.ToString()))
                    {
                        ctr.Visible = true;
                    }
                    else
                        ctr.Visible = false;
                }
            }
        }

      
        #endregion

        private void PicJobParameter_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show(this, "尚未选择要修改Job", "数据同步");
                return;
            }

            DataGridViewRow row = this.dataGridView1.SelectedRows[0];
            string jobCode = row.Cells[0].Value.ToString();
            JobEntity entity = null;

            IJob job = SyncService.Current.GetJobInstance(jobCode, out entity);

            JobCfgForm form = job.GetCfgForm();
            form.SetJobEntity(entity);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog(this);
        }

        private void PicRunJob_Click(object sender, EventArgs e)
        {
            // 运行
            RunJob();
        }

        private void Picabout_Click(object sender, EventArgs e)
        {
            frmAbount frm = new frmAbount(SyncService.Current.CurrentVersion);
            frm.ShowDialog();
        }

        private void RunJob()
        {
            // 运行
            if (this.dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show(this, "尚未选择Job", "数据同步");
                return;
            }


            this.Cursor = Cursors.WaitCursor;

            DataGridViewRow row = this.dataGridView1.SelectedRows[0];
            string jobCode = row.Cells[0].Value.ToString();

            this.lblInfo.Text = "开始运行：" + row.Cells[1].Value.ToString();
            Application.DoEvents();

            Hashtable parameters = new Hashtable();
            parameters[DDPConst.Param_RunType] = DDPConst.RunType_Manual;

            // 运行Job
            JobEntity entity = null;
            this._job = SyncService.Current.GetJobInstance(jobCode, out entity);
            SyncService.Current.RunJob(this._job, entity, parameters);

            this.Cursor = Cursors.Default;
        }

        private void ToolStripMenuItem_job_Run_Click(object sender, EventArgs e)
        {
            RunJob();
        }

        private void cmbJobCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "tabPageLog")
                this.InitialLog();
        }
    }
}