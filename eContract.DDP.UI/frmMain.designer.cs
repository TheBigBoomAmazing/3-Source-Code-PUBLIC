namespace eContract.DDP.UI
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnScheduleEnd = new System.Windows.Forms.Button();
            this.contextMenuStrip_job = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_job_Run = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_job_edit = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageJob = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new eContract.DDP.UI.MyDataGridViewNoRow();
            this.col_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_dll = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_class = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageSchedule = new System.Windows.Forms.TabPage();
            this.btnScheduleStart = new System.Windows.Forms.Button();
            this.dgv_schedule = new eContract.DDP.UI.MyDataGridViewNoRow();
            this.col_sch_ScheduleID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_sch_jobcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_sch_jobName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_scheduletype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_starttime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_everyWeekDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_everyMonthDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_EndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_IntervalMinutes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip_schedule = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_schedule_new = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_schedule_edit = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_schedule_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageLog = new System.Windows.Forms.TabPage();
            this.cmbJobCode = new System.Windows.Forms.ComboBox();
            this.dgv_log = new eContract.DDP.UI.MyDataGridViewNoRow();
            this.col_log_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_log_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_log_jobcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_log_jobName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_log_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_log_ExceptionType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_log_ExceptionMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_log_threadName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRefreshLog = new System.Windows.Forms.Button();
            this.cbLogType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblInfo = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Picabout = new System.Windows.Forms.PictureBox();
            this.PicRunJob = new System.Windows.Forms.PictureBox();
            this.PicJobParameter = new System.Windows.Forms.PictureBox();
            this.picViewLog = new System.Windows.Forms.PictureBox();
            this.picJobManage = new System.Windows.Forms.PictureBox();
            this.picSchedule = new System.Windows.Forms.PictureBox();
            this.lblJobManange = new System.Windows.Forms.Label();
            this.lblViewLog = new System.Windows.Forms.Label();
            this.lblSchedule = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.contextMenuStrip_job.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageJob.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPageSchedule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_schedule)).BeginInit();
            this.contextMenuStrip_schedule.SuspendLayout();
            this.tabPageLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_log)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Picabout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicRunJob)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicJobParameter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picViewLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picJobManage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSchedule)).BeginInit();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnScheduleEnd
            // 
            this.btnScheduleEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnScheduleEnd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnScheduleEnd.BackgroundImage")));
            this.btnScheduleEnd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnScheduleEnd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnScheduleEnd.Location = new System.Drawing.Point(646, 268);
            this.btnScheduleEnd.Name = "btnScheduleEnd";
            this.btnScheduleEnd.Size = new System.Drawing.Size(99, 23);
            this.btnScheduleEnd.TabIndex = 3;
            this.btnScheduleEnd.Text = "任务计划结束";
            this.btnScheduleEnd.UseVisualStyleBackColor = true;
            this.btnScheduleEnd.Click += new System.EventHandler(this.btnScheduleEnd_Click);
            // 
            // contextMenuStrip_job
            // 
            this.contextMenuStrip_job.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_job_Run,
            this.ToolStripMenuItem_job_edit});
            this.contextMenuStrip_job.Name = "contextMenuStrip_job";
            this.contextMenuStrip_job.Size = new System.Drawing.Size(123, 48);
            // 
            // ToolStripMenuItem_job_Run
            // 
            this.ToolStripMenuItem_job_Run.Name = "ToolStripMenuItem_job_Run";
            this.ToolStripMenuItem_job_Run.Size = new System.Drawing.Size(122, 22);
            this.ToolStripMenuItem_job_Run.Text = "运行";
            this.ToolStripMenuItem_job_Run.Click += new System.EventHandler(this.ToolStripMenuItem_job_Run_Click);
            // 
            // ToolStripMenuItem_job_edit
            // 
            this.ToolStripMenuItem_job_edit.Name = "ToolStripMenuItem_job_edit";
            this.ToolStripMenuItem_job_edit.Size = new System.Drawing.Size(122, 22);
            this.ToolStripMenuItem_job_edit.Text = "查看属性";
            this.ToolStripMenuItem_job_edit.Click += new System.EventHandler(this.ToolStripMenuItem_job_edit_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageJob);
            this.tabControl1.Controls.Add(this.tabPageSchedule);
            this.tabControl1.Controls.Add(this.tabPageLog);
            this.tabControl1.ItemSize = new System.Drawing.Size(86, 28);
            this.tabControl1.Location = new System.Drawing.Point(0, 86);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(768, 337);
            this.tabControl1.TabIndex = 9;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPageJob
            // 
            this.tabPageJob.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(246)))), ((int)(((byte)(242)))));
            this.tabPageJob.Controls.Add(this.dataGridView1);
            this.tabPageJob.Location = new System.Drawing.Point(4, 32);
            this.tabPageJob.Name = "tabPageJob";
            this.tabPageJob.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageJob.Size = new System.Drawing.Size(760, 301);
            this.tabPageJob.TabIndex = 0;
            this.tabPageJob.Text = "任务";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_code,
            this.col_name,
            this.col_dll,
            this.col_class});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip_job;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.Size = new System.Drawing.Size(754, 295);
            this.dataGridView1.TabIndex = 9;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // col_code
            // 
            this.col_code.DataPropertyName = "Code";
            this.col_code.HeaderText = "任务代码";
            this.col_code.Name = "col_code";
            // 
            // col_name
            // 
            this.col_name.DataPropertyName = "Name";
            this.col_name.HeaderText = "任务名称";
            this.col_name.Name = "col_name";
            // 
            // col_dll
            // 
            this.col_dll.DataPropertyName = "AssemblyName";
            this.col_dll.HeaderText = "程序集名";
            this.col_dll.Name = "col_dll";
            // 
            // col_class
            // 
            this.col_class.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_class.DataPropertyName = "ClassName";
            this.col_class.HeaderText = "类名";
            this.col_class.Name = "col_class";
            // 
            // tabPageSchedule
            // 
            this.tabPageSchedule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(246)))), ((int)(((byte)(242)))));
            this.tabPageSchedule.Controls.Add(this.btnScheduleStart);
            this.tabPageSchedule.Controls.Add(this.dgv_schedule);
            this.tabPageSchedule.Controls.Add(this.btnScheduleEnd);
            this.tabPageSchedule.Location = new System.Drawing.Point(4, 32);
            this.tabPageSchedule.Name = "tabPageSchedule";
            this.tabPageSchedule.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSchedule.Size = new System.Drawing.Size(760, 301);
            this.tabPageSchedule.TabIndex = 1;
            this.tabPageSchedule.Text = "时间表";
            // 
            // btnScheduleStart
            // 
            this.btnScheduleStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnScheduleStart.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnScheduleStart.BackgroundImage")));
            this.btnScheduleStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnScheduleStart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnScheduleStart.Location = new System.Drawing.Point(541, 268);
            this.btnScheduleStart.Name = "btnScheduleStart";
            this.btnScheduleStart.Size = new System.Drawing.Size(99, 23);
            this.btnScheduleStart.TabIndex = 15;
            this.btnScheduleStart.Text = "任务计划开始";
            this.btnScheduleStart.UseVisualStyleBackColor = true;
            this.btnScheduleStart.Click += new System.EventHandler(this.btnScheduleStart_Click);
            // 
            // dgv_schedule
            // 
            this.dgv_schedule.AllowUserToAddRows = false;
            this.dgv_schedule.AllowUserToDeleteRows = false;
            this.dgv_schedule.AllowUserToOrderColumns = true;
            this.dgv_schedule.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_schedule.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_schedule.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_schedule.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgv_schedule.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_schedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_schedule.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_sch_ScheduleID,
            this.col_sch_jobcode,
            this.col_sch_jobName,
            this.col_scheduletype,
            this.col_starttime,
            this.col_everyWeekDay,
            this.col_everyMonthDay,
            this.col_EndTime,
            this.col_IntervalMinutes});
            this.dgv_schedule.ContextMenuStrip = this.contextMenuStrip_schedule;
            this.dgv_schedule.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgv_schedule.Location = new System.Drawing.Point(3, 3);
            this.dgv_schedule.Name = "dgv_schedule";
            this.dgv_schedule.ReadOnly = true;
            this.dgv_schedule.RowHeadersVisible = false;
            this.dgv_schedule.RowTemplate.Height = 23;
            this.dgv_schedule.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_schedule.Size = new System.Drawing.Size(754, 257);
            this.dgv_schedule.TabIndex = 14;
            this.dgv_schedule.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_schedule_CellDoubleClick);
            // 
            // col_sch_ScheduleID
            // 
            this.col_sch_ScheduleID.DataPropertyName = "ScheduleID";
            this.col_sch_ScheduleID.HeaderText = "ID";
            this.col_sch_ScheduleID.Name = "col_sch_ScheduleID";
            this.col_sch_ScheduleID.ReadOnly = true;
            this.col_sch_ScheduleID.Visible = false;
            // 
            // col_sch_jobcode
            // 
            this.col_sch_jobcode.DataPropertyName = "JobCode";
            this.col_sch_jobcode.FillWeight = 149.4288F;
            this.col_sch_jobcode.HeaderText = "任务代码";
            this.col_sch_jobcode.Name = "col_sch_jobcode";
            this.col_sch_jobcode.ReadOnly = true;
            // 
            // col_sch_jobName
            // 
            this.col_sch_jobName.DataPropertyName = "JobName";
            this.col_sch_jobName.FillWeight = 200F;
            this.col_sch_jobName.HeaderText = "任务名称";
            this.col_sch_jobName.Name = "col_sch_jobName";
            this.col_sch_jobName.ReadOnly = true;
            // 
            // col_scheduletype
            // 
            this.col_scheduletype.DataPropertyName = "ScheduleType";
            this.col_scheduletype.HeaderText = "执行计划类型";
            this.col_scheduletype.Name = "col_scheduletype";
            this.col_scheduletype.ReadOnly = true;
            // 
            // col_starttime
            // 
            this.col_starttime.DataPropertyName = "StartTime";
            this.col_starttime.HeaderText = "开始时间";
            this.col_starttime.Name = "col_starttime";
            this.col_starttime.ReadOnly = true;
            // 
            // col_everyWeekDay
            // 
            this.col_everyWeekDay.DataPropertyName = "EveryWeekDay";
            this.col_everyWeekDay.HeaderText = "每周*";
            this.col_everyWeekDay.Name = "col_everyWeekDay";
            this.col_everyWeekDay.ReadOnly = true;
            // 
            // col_everyMonthDay
            // 
            this.col_everyMonthDay.DataPropertyName = "EveryMonthDay";
            this.col_everyMonthDay.HeaderText = "每月*号";
            this.col_everyMonthDay.Name = "col_everyMonthDay";
            this.col_everyMonthDay.ReadOnly = true;
            // 
            // col_EndTime
            // 
            this.col_EndTime.DataPropertyName = "EndTime";
            this.col_EndTime.HeaderText = "结束时间";
            this.col_EndTime.Name = "col_EndTime";
            this.col_EndTime.ReadOnly = true;
            // 
            // col_IntervalMinutes
            // 
            this.col_IntervalMinutes.DataPropertyName = "IntervalMinutes";
            this.col_IntervalMinutes.HeaderText = "间隔分钟";
            this.col_IntervalMinutes.Name = "col_IntervalMinutes";
            this.col_IntervalMinutes.ReadOnly = true;
            // 
            // contextMenuStrip_schedule
            // 
            this.contextMenuStrip_schedule.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_schedule_new,
            this.ToolStripMenuItem_schedule_edit,
            this.ToolStripMenuItem_schedule_delete});
            this.contextMenuStrip_schedule.Name = "contextMenuStrip_schedule";
            this.contextMenuStrip_schedule.Size = new System.Drawing.Size(114, 70);
            // 
            // ToolStripMenuItem_schedule_new
            // 
            this.ToolStripMenuItem_schedule_new.Name = "ToolStripMenuItem_schedule_new";
            this.ToolStripMenuItem_schedule_new.Size = new System.Drawing.Size(113, 22);
            this.ToolStripMenuItem_schedule_new.Text = "新增(&N)";
            this.ToolStripMenuItem_schedule_new.Click += new System.EventHandler(this.ToolStripMenuItem_schedule_new_Click);
            // 
            // ToolStripMenuItem_schedule_edit
            // 
            this.ToolStripMenuItem_schedule_edit.Name = "ToolStripMenuItem_schedule_edit";
            this.ToolStripMenuItem_schedule_edit.Size = new System.Drawing.Size(113, 22);
            this.ToolStripMenuItem_schedule_edit.Text = "修改(&E)";
            this.ToolStripMenuItem_schedule_edit.Click += new System.EventHandler(this.ToolStripMenuItem_schedule_edit_Click);
            // 
            // ToolStripMenuItem_schedule_delete
            // 
            this.ToolStripMenuItem_schedule_delete.Name = "ToolStripMenuItem_schedule_delete";
            this.ToolStripMenuItem_schedule_delete.Size = new System.Drawing.Size(113, 22);
            this.ToolStripMenuItem_schedule_delete.Text = "删除(&D)";
            this.ToolStripMenuItem_schedule_delete.Click += new System.EventHandler(this.ToolStripMenuItem_schedule_delete_Click);
            // 
            // tabPageLog
            // 
            this.tabPageLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(246)))), ((int)(((byte)(242)))));
            this.tabPageLog.Controls.Add(this.cmbJobCode);
            this.tabPageLog.Controls.Add(this.dgv_log);
            this.tabPageLog.Controls.Add(this.label2);
            this.tabPageLog.Controls.Add(this.btnRefreshLog);
            this.tabPageLog.Controls.Add(this.cbLogType);
            this.tabPageLog.Controls.Add(this.label1);
            this.tabPageLog.Location = new System.Drawing.Point(4, 32);
            this.tabPageLog.Name = "tabPageLog";
            this.tabPageLog.Size = new System.Drawing.Size(760, 301);
            this.tabPageLog.TabIndex = 2;
            this.tabPageLog.Text = "JOB运行日志";
            // 
            // cmbJobCode
            // 
            this.cmbJobCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJobCode.FormattingEnabled = true;
            this.cmbJobCode.Location = new System.Drawing.Point(368, 11);
            this.cmbJobCode.Name = "cmbJobCode";
            this.cmbJobCode.Size = new System.Drawing.Size(216, 22);
            this.cmbJobCode.TabIndex = 14;
            this.cmbJobCode.SelectedIndexChanged += new System.EventHandler(this.cmbJobCode_SelectedIndexChanged);
            // 
            // dgv_log
            // 
            this.dgv_log.AllowUserToAddRows = false;
            this.dgv_log.AllowUserToDeleteRows = false;
            this.dgv_log.AllowUserToOrderColumns = true;
            this.dgv_log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_log.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_log.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_log.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgv_log.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_log.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_log.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_log_time,
            this.col_log_type,
            this.col_log_jobcode,
            this.col_log_jobName,
            this.col_log_desc,
            this.col_log_ExceptionType,
            this.col_log_ExceptionMessage,
            this.col_log_threadName});
            this.dgv_log.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.dgv_log.Location = new System.Drawing.Point(1, 40);
            this.dgv_log.Name = "dgv_log";
            this.dgv_log.ReadOnly = true;
            this.dgv_log.RowHeadersVisible = false;
            this.dgv_log.RowTemplate.Height = 23;
            this.dgv_log.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_log.Size = new System.Drawing.Size(756, 256);
            this.dgv_log.TabIndex = 13;
            this.dgv_log.DoubleClick += new System.EventHandler(this.dgv_log_DoubleClick);
            // 
            // col_log_time
            // 
            this.col_log_time.DataPropertyName = "Time";
            this.col_log_time.HeaderText = "时间";
            this.col_log_time.Name = "col_log_time";
            this.col_log_time.ReadOnly = true;
            // 
            // col_log_type
            // 
            this.col_log_type.DataPropertyName = "CategoryString";
            this.col_log_type.HeaderText = "类型";
            this.col_log_type.Name = "col_log_type";
            this.col_log_type.ReadOnly = true;
            // 
            // col_log_jobcode
            // 
            this.col_log_jobcode.DataPropertyName = "JobCode";
            this.col_log_jobcode.HeaderText = "任务代码";
            this.col_log_jobcode.Name = "col_log_jobcode";
            this.col_log_jobcode.ReadOnly = true;
            // 
            // col_log_jobName
            // 
            this.col_log_jobName.DataPropertyName = "JobName";
            this.col_log_jobName.HeaderText = "任务名称";
            this.col_log_jobName.Name = "col_log_jobName";
            this.col_log_jobName.ReadOnly = true;
            // 
            // col_log_desc
            // 
            this.col_log_desc.DataPropertyName = "Description";
            this.col_log_desc.HeaderText = "描述";
            this.col_log_desc.Name = "col_log_desc";
            this.col_log_desc.ReadOnly = true;
            // 
            // col_log_ExceptionType
            // 
            this.col_log_ExceptionType.DataPropertyName = "ExceptionType";
            this.col_log_ExceptionType.HeaderText = "异常类型";
            this.col_log_ExceptionType.Name = "col_log_ExceptionType";
            this.col_log_ExceptionType.ReadOnly = true;
            // 
            // col_log_ExceptionMessage
            // 
            this.col_log_ExceptionMessage.DataPropertyName = "ExceptionMessage";
            this.col_log_ExceptionMessage.HeaderText = "异常信息";
            this.col_log_ExceptionMessage.Name = "col_log_ExceptionMessage";
            this.col_log_ExceptionMessage.ReadOnly = true;
            // 
            // col_log_threadName
            // 
            this.col_log_threadName.DataPropertyName = "ThreadName";
            this.col_log_threadName.HeaderText = "唯一代号";
            this.col_log_threadName.Name = "col_log_threadName";
            this.col_log_threadName.ReadOnly = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(299, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 11;
            this.label2.Text = "任务名称";
            // 
            // btnRefreshLog
            // 
            this.btnRefreshLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshLog.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRefreshLog.BackgroundImage")));
            this.btnRefreshLog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefreshLog.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRefreshLog.Location = new System.Drawing.Point(647, 7);
            this.btnRefreshLog.Name = "btnRefreshLog";
            this.btnRefreshLog.Size = new System.Drawing.Size(99, 23);
            this.btnRefreshLog.TabIndex = 10;
            this.btnRefreshLog.Text = "刷新日志";
            this.btnRefreshLog.UseVisualStyleBackColor = true;
            this.btnRefreshLog.Click += new System.EventHandler(this.btnRefreshLog_Click);
            // 
            // cbLogType
            // 
            this.cbLogType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLogType.FormattingEnabled = true;
            this.cbLogType.Location = new System.Drawing.Point(77, 10);
            this.cbLogType.Name = "cbLogType";
            this.cbLogType.Size = new System.Drawing.Size(216, 22);
            this.cbLogType.TabIndex = 1;
            this.cbLogType.SelectedIndexChanged += new System.EventHandler(this.cbLogType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "日志类型";
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.Controls.Add(this.lblInfo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(1, 421);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(766, 29);
            this.panel2.TabIndex = 16;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.Location = new System.Drawing.Point(5, 5);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(41, 20);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "就绪";
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Controls.Add(this.Picabout);
            this.panel1.Controls.Add(this.PicRunJob);
            this.panel1.Controls.Add(this.PicJobParameter);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(1, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(766, 50);
            this.panel1.TabIndex = 17;
            // 
            // Picabout
            // 
            this.Picabout.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Picabout.BackgroundImage")));
            this.Picabout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Picabout.Location = new System.Drawing.Point(244, 0);
            this.Picabout.Name = "Picabout";
            this.Picabout.Size = new System.Drawing.Size(120, 50);
            this.Picabout.TabIndex = 4;
            this.Picabout.TabStop = false;
            this.Picabout.Click += new System.EventHandler(this.Picabout_Click);
            // 
            // PicRunJob
            // 
            this.PicRunJob.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PicRunJob.BackgroundImage")));
            this.PicRunJob.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PicRunJob.Location = new System.Drawing.Point(124, 0);
            this.PicRunJob.Name = "PicRunJob";
            this.PicRunJob.Size = new System.Drawing.Size(120, 50);
            this.PicRunJob.TabIndex = 1;
            this.PicRunJob.TabStop = false;
            this.PicRunJob.Click += new System.EventHandler(this.PicRunJob_Click);
            // 
            // PicJobParameter
            // 
            this.PicJobParameter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PicJobParameter.BackgroundImage")));
            this.PicJobParameter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PicJobParameter.Location = new System.Drawing.Point(4, 0);
            this.PicJobParameter.Name = "PicJobParameter";
            this.PicJobParameter.Size = new System.Drawing.Size(120, 50);
            this.PicJobParameter.TabIndex = 0;
            this.PicJobParameter.TabStop = false;
            this.PicJobParameter.Click += new System.EventHandler(this.PicJobParameter_Click);
            // 
            // picViewLog
            // 
            this.picViewLog.BackColor = System.Drawing.Color.Transparent;
            this.picViewLog.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picViewLog.BackgroundImage")));
            this.picViewLog.Location = new System.Drawing.Point(211, 7);
            this.picViewLog.Name = "picViewLog";
            this.picViewLog.Size = new System.Drawing.Size(80, 28);
            this.picViewLog.TabIndex = 3;
            this.picViewLog.TabStop = false;
            this.picViewLog.Tag = "2";
            this.picViewLog.Click += new System.EventHandler(this.picButton_Click);
            // 
            // picJobManage
            // 
            this.picJobManage.BackColor = System.Drawing.Color.Transparent;
            this.picJobManage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picJobManage.BackgroundImage")));
            this.picJobManage.Location = new System.Drawing.Point(11, 7);
            this.picJobManage.Name = "picJobManage";
            this.picJobManage.Size = new System.Drawing.Size(80, 28);
            this.picJobManage.TabIndex = 4;
            this.picJobManage.TabStop = false;
            this.picJobManage.Tag = "0";
            this.picJobManage.Click += new System.EventHandler(this.picButton_Click);
            // 
            // picSchedule
            // 
            this.picSchedule.BackColor = System.Drawing.Color.Transparent;
            this.picSchedule.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picSchedule.BackgroundImage")));
            this.picSchedule.Location = new System.Drawing.Point(91, 7);
            this.picSchedule.Name = "picSchedule";
            this.picSchedule.Size = new System.Drawing.Size(120, 28);
            this.picSchedule.TabIndex = 5;
            this.picSchedule.TabStop = false;
            this.picSchedule.Tag = "1";
            this.picSchedule.Click += new System.EventHandler(this.picButton_Click);
            // 
            // lblJobManange
            // 
            this.lblJobManange.AutoSize = true;
            this.lblJobManange.BackColor = System.Drawing.Color.Transparent;
            this.lblJobManange.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJobManange.Location = new System.Drawing.Point(24, 12);
            this.lblJobManange.Name = "lblJobManange";
            this.lblJobManange.Size = new System.Drawing.Size(55, 15);
            this.lblJobManange.TabIndex = 8;
            this.lblJobManange.Tag = "0";
            this.lblJobManange.Text = "任务管理";
            this.lblJobManange.Click += new System.EventHandler(this.lblButton_Click);
            // 
            // lblViewLog
            // 
            this.lblViewLog.AutoSize = true;
            this.lblViewLog.BackColor = System.Drawing.Color.Transparent;
            this.lblViewLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblViewLog.Location = new System.Drawing.Point(221, 12);
            this.lblViewLog.Name = "lblViewLog";
            this.lblViewLog.Size = new System.Drawing.Size(55, 15);
            this.lblViewLog.TabIndex = 7;
            this.lblViewLog.Tag = "2";
            this.lblViewLog.Text = "查看日志";
            this.lblViewLog.Click += new System.EventHandler(this.lblButton_Click);
            // 
            // lblSchedule
            // 
            this.lblSchedule.AutoSize = true;
            this.lblSchedule.BackColor = System.Drawing.Color.Transparent;
            this.lblSchedule.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSchedule.Location = new System.Drawing.Point(107, 12);
            this.lblSchedule.Name = "lblSchedule";
            this.lblSchedule.Size = new System.Drawing.Size(91, 15);
            this.lblSchedule.TabIndex = 9;
            this.lblSchedule.Tag = "1";
            this.lblSchedule.Text = "自动运行时间表";
            this.lblSchedule.Click += new System.EventHandler(this.lblButton_Click);
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel5.BackgroundImage")));
            this.panel5.Controls.Add(this.picSchedule);
            this.panel5.Controls.Add(this.picJobManage);
            this.panel5.Controls.Add(this.picViewLog);
            this.panel5.Controls.Add(this.lblJobManange);
            this.panel5.Controls.Add(this.lblSchedule);
            this.panel5.Controls.Add(this.lblViewLog);
            this.panel5.Location = new System.Drawing.Point(1, 83);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(766, 34);
            this.panel5.TabIndex = 18;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 451);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "frmMain";
            this.ShowInTaskbar = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "后台管理系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.Controls.SetChildIndex(this.panel5, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.contextMenuStrip_job.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageJob.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPageSchedule.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_schedule)).EndInit();
            this.contextMenuStrip_schedule.ResumeLayout(false);
            this.tabPageLog.ResumeLayout(false);
            this.tabPageLog.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_log)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Picabout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicRunJob)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicJobParameter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picViewLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picJobManage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSchedule)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnScheduleEnd;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageJob;
        private System.Windows.Forms.TabPage tabPageSchedule;
        private System.Windows.Forms.TabPage tabPageLog;
        private System.Windows.Forms.ComboBox cbLogType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_schedule;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_schedule_new;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_schedule_edit;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_schedule_delete;
        private System.Windows.Forms.Button btnRefreshLog;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_job;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_job_edit;
        private System.Windows.Forms.Label label2;
        private eContract.DDP.UI.MyDataGridViewNoRow dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_dll;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_class;
        private eContract.DDP.UI.MyDataGridViewNoRow dgv_schedule;
        private eContract.DDP.UI.MyDataGridViewNoRow dgv_log;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox Picabout;
        private System.Windows.Forms.PictureBox PicRunJob;
        private System.Windows.Forms.PictureBox PicJobParameter;
        private System.Windows.Forms.PictureBox picViewLog;
        private System.Windows.Forms.PictureBox picJobManage;
        private System.Windows.Forms.PictureBox picSchedule;
        private System.Windows.Forms.Label lblJobManange;
        private System.Windows.Forms.Label lblViewLog;
        private System.Windows.Forms.Label lblSchedule;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnScheduleStart;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_job_Run;
        private System.Windows.Forms.ComboBox cmbJobCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_log_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_log_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_log_jobcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_log_jobName;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_log_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_log_ExceptionType;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_log_ExceptionMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_log_threadName;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_sch_ScheduleID;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_sch_jobcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_sch_jobName;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_scheduletype;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_starttime;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_everyWeekDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_everyMonthDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_EndTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_IntervalMinutes;
    }
}

