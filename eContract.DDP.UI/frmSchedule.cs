using eContract.DDP.Common;
using eContract.DDP.Server;
using System;
using System.Data;
using System.Windows.Forms;

namespace eContract.DDP.UI
{
    public partial class frmSchedule : frmBase
    {
        /// <summary>
        /// Schedule实体
        /// </summary>
        private ScheduleEntity _scheduleEntity = null;

        /// <summary>
        /// 是否是新Scheudle
        /// </summary>
        public bool _bNewSchedule = true;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="scheduleEntity"></param>
        public frmSchedule(SyncService syncService, ScheduleEntity scheduleEntity)
        {
            InitializeComponent();


            // 初始化Job code 列表
            this.InitialJobCode();

            // 默认第一项
            this.cbScheduleType.SelectedIndex = 0;

            // 根据Schedule实体初始化界面信息
            if (scheduleEntity != null)
            {
                this._scheduleEntity = scheduleEntity;
                this._bNewSchedule = false;

                // 初始化界面
                this.InitialScheduleInfo();
            }
        }

        /// <summary>
        /// 初始化Job code 列表
        /// </summary>
        private void InitialJobCode()
        {

            this.cbJobCode.DataSource = null;
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

            this.cbJobCode.DataSource = dt;
            this.cbJobCode.DisplayMember = "JobName";
            this.cbJobCode.ValueMember = "JobCode";

            this.cbJobCode.SelectedIndex = 0;
        }

        /// <summary>
        /// 初始化界面信息
        /// </summary>
        private void InitialScheduleInfo()
        {
            this.cbJobCode.SelectedValue = this._scheduleEntity.JobCode;
            this.cbScheduleType.SelectedItem = this._scheduleEntity.ScheduleType;
            if (this._scheduleEntity.StartTime != null && this._scheduleEntity.StartTime.Length > 0)
            {
                this.txtStartTime.Checked = true;
                this.txtStartTime.Text = this._scheduleEntity.StartTime;
            }
            if (this._scheduleEntity.EndTime != null && this._scheduleEntity.EndTime.Length > 0)
            {
                this.txtEndTime.Checked = true;
                this.txtEndTime.Text = this._scheduleEntity.EndTime;
            }

            if (this._scheduleEntity.EveryWeekDay != 0)
                this.nudWeekDay.Value = this._scheduleEntity.EveryWeekDay;

            if (this._scheduleEntity.EveryMonthDay != 0)
                this.nudMonthDay.Value = this._scheduleEntity.EveryMonthDay;

            if (this._scheduleEntity.IntervalMinutes != 0)
                this.txtInterval.Value = this._scheduleEntity.IntervalMinutes;
        }

        /// <summary>
        /// Schedule类型发生变化时，要改变控件的状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbScheduleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbScheduleType.Text == ScheduleEntity.SCHEDULE_TYPE_EVERY_DAY)
            {
                this.lblWeekDay.Visible = false;
                this.nudWeekDay.Visible = false;
                this.nudWeekDay.Enabled = false;

                this.lblMonthDay.Visible = false;
                this.nudMonthDay.Visible = false;
                this.nudMonthDay.Enabled = false;

                this.lblEndTime.Visible = false;
                this.txtEndTime.Visible = false;
                this.lblEndTimeInfo.Visible = false;

                this.lblInterval.Visible = false;
                this.txtInterval.Visible = false;
                this.txtInterval.ReadOnly = true;
            }
            else if (this.cbScheduleType.Text == ScheduleEntity.SCHEDULE_TYPE_EVERY_WEEK)
            {
                this.lblWeekDay.Visible = true;
                this.nudWeekDay.Visible = true;
                this.nudWeekDay.Enabled = true;

                this.lblMonthDay.Visible = false;
                this.nudMonthDay.Visible = false;
                this.nudMonthDay.Enabled = false;

                this.lblEndTime.Visible = false;
                this.txtEndTime.Visible = false;
                this.lblEndTimeInfo.Visible = false;

                this.lblInterval.Visible = false;
                this.txtInterval.Visible = false;
                this.txtInterval.ReadOnly = true;
            }
            else if (this.cbScheduleType.Text == ScheduleEntity.SCHEDULE_TYPE_EVERY_MONTH)
            {
                this.lblWeekDay.Visible = false;
                this.nudWeekDay.Visible = false;
                this.nudWeekDay.Enabled = false;

                this.lblMonthDay.Visible = true;
                this.nudMonthDay.Visible = true;
                this.nudMonthDay.Enabled = true;

                this.lblEndTime.Visible = false;
                this.txtEndTime.Visible = false;
                this.lblEndTimeInfo.Visible = false;

                this.lblInterval.Visible = false;
                this.txtInterval.Visible = false;
                this.txtInterval.ReadOnly = true;
            }
            else if (this.cbScheduleType.Text == ScheduleEntity.SCHEDULE_TYPE_Interval)
            {
                this.lblWeekDay.Visible = false;
                this.nudWeekDay.Visible = false;
                this.nudWeekDay.Enabled = false;

                this.lblMonthDay.Visible = false;
                this.nudMonthDay.Visible = false;
                this.nudMonthDay.Enabled = false;

                this.lblEndTime.Visible = true;
                this.txtEndTime.Visible = true;
                this.lblEndTimeInfo.Visible = true;

                this.lblInterval.Visible = true;
                this.txtInterval.Visible = true;
                this.txtInterval.ReadOnly = false;
            }
        }

        /// <summary>
        /// 更新Schedule
        /// </summary>
        public void UpdateSchedule()
        {
            if (this._scheduleEntity == null)
            {
                this._scheduleEntity = new ScheduleEntity();
                this._scheduleEntity.ScheduleID = Guid.NewGuid().ToString();
            }

            this._scheduleEntity.JobCode = this.cbJobCode.SelectedValue.ToString().Trim();
            this._scheduleEntity.StartTime = this.txtStartTime.Checked && this.txtStartTime.Visible ? this.txtStartTime.Text.Trim() : "";
            if (this._scheduleEntity.StartTime.Length > 0)
            {
                this._scheduleEntity._startHour = Convert.ToInt32(this._scheduleEntity.StartTime.Substring(0, 2));
                this._scheduleEntity._startMinute = Convert.ToInt32(this._scheduleEntity.StartTime.Substring(3, 2));
            }
            this._scheduleEntity.EndTime = this.txtEndTime.Checked && this.txtEndTime.Visible ? this.txtEndTime.Text.Trim() : "";
            if (this._scheduleEntity.EndTime.Length > 0)
            {
                this._scheduleEntity._endHour = Convert.ToInt32(this._scheduleEntity.EndTime.Substring(0, 2));
                this._scheduleEntity._endMinute = Convert.ToInt32(this._scheduleEntity.EndTime.Substring(3, 2));
            }
            this._scheduleEntity.ScheduleType = this.cbScheduleType.Text;
            this._scheduleEntity.IntervalMinutes = this.txtInterval.Visible ? Convert.ToInt32(this.txtInterval.Text.Trim()) : 0;
            if (this._scheduleEntity.ScheduleType == ScheduleEntity.SCHEDULE_TYPE_EVERY_WEEK)
            {
                this._scheduleEntity.EveryWeekDay = Convert.ToInt32(this.nudWeekDay.Value);
                this._scheduleEntity.EveryMonthDay = 0;
                this._scheduleEntity.IntervalMinutes = 0;
            }
            else if (this._scheduleEntity.ScheduleType == ScheduleEntity.SCHEDULE_TYPE_EVERY_MONTH)
            {
                this._scheduleEntity.EveryMonthDay = Convert.ToInt32(this.nudMonthDay.Value);
                this._scheduleEntity.EveryWeekDay = 0;
                this._scheduleEntity.IntervalMinutes = 0;
            }
            else if (this._scheduleEntity.ScheduleType == ScheduleEntity.SCHEDULE_TYPE_Interval)
            {
                this._scheduleEntity.IntervalMinutes = Convert.ToInt32(this.txtInterval.Text.Trim());
                this._scheduleEntity.EveryWeekDay = 0;
                this._scheduleEntity.EveryMonthDay = 0;
            }
            else
            {
                this._scheduleEntity.EveryMonthDay = 0;
                this._scheduleEntity.EveryWeekDay = 0;
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.cbJobCode.Text.Trim().Length == 0)
                {
                    MessageBox.Show("请选择任务名称！");
                    return;
                }

                if (this.cbScheduleType.SelectedIndex == 0)
                {
                    MessageBox.Show("请选择执行计划类型！");
                    return;
                }


                // 检查输入值
                if (this.cbScheduleType.Text != ScheduleEntity.SCHEDULE_TYPE_Interval && !this.txtStartTime.Checked)
                {
                    MessageBox.Show("请选择开始时间！");
                    return;
                }

                if (this.txtEndTime.Visible && this.txtStartTime.Visible && this.txtEndTime.Checked && this.txtStartTime.Checked && this.txtStartTime.Value >= this.txtEndTime.Value)
                {
                    MessageBox.Show("开始时间应该小于结束时间！");
                    return;
                }
                if (this.txtEndTime.Visible && this.txtStartTime.Visible && this.txtEndTime.Checked && this.txtStartTime.Checked)
                {
                    TimeSpan time = this.txtEndTime.Value - this.txtStartTime.Value;
                    if ((time.Hours * 60 + time.Minutes) < Convert.ToInt32(this.txtInterval.Value))
                    {
                        MessageBox.Show("时间间隔应该小于起始时间和结束时间之差！");
                        return;
                    }
                }
                // 先更新Job
                this.UpdateSchedule();

                // 新增
                if (this._bNewSchedule == true)
                    SyncService.Current.ScheduleManager.NewSchedule(this._scheduleEntity);
                else
                    SyncService.Current.ScheduleManager.EditSchedule(this._scheduleEntity);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(this, "无操作权限，被操作文件可能是只读属性，" + ex.Message);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void frmSchedule_Load(object sender, EventArgs e)
        {
            base.InitTitle(true, "任务执行计划");
        }
    }
}