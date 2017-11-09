namespace eContract.DDP.UI
{
    partial class frmSchedule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSchedule));
            this.label1 = new System.Windows.Forms.Label();
            this.cbScheduleType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.lblWeekDay = new System.Windows.Forms.Label();
            this.nudWeekDay = new System.Windows.Forms.NumericUpDown();
            this.nudMonthDay = new System.Windows.Forms.NumericUpDown();
            this.lblMonthDay = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.cbJobCode = new System.Windows.Forms.ComboBox();
            this.lblInterval = new System.Windows.Forms.Label();
            this.lblEndTimeInfo = new System.Windows.Forms.Label();
            this.lblEndTime = new System.Windows.Forms.Label();
            this.txtInterval = new System.Windows.Forms.NumericUpDown();
            this.txtStartTime = new System.Windows.Forms.DateTimePicker();
            this.txtEndTime = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.nudWeekDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMonthDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "任务名称";
            // 
            // cbScheduleType
            // 
            this.cbScheduleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbScheduleType.FormattingEnabled = true;
            this.cbScheduleType.Items.AddRange(new object[] {
            "",
            "EveryDay",
            "EveryWeek",
            "EveryMonth",
            "Interval"});
            this.cbScheduleType.Location = new System.Drawing.Point(120, 86);
            this.cbScheduleType.Name = "cbScheduleType";
            this.cbScheduleType.Size = new System.Drawing.Size(250, 22);
            this.cbScheduleType.TabIndex = 5;
            this.cbScheduleType.SelectedIndexChanged += new System.EventHandler(this.cbScheduleType_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 14);
            this.label5.TabIndex = 4;
            this.label5.Text = "执行计划类型";
            // 
            // lblStartTime
            // 
            this.lblStartTime.AutoSize = true;
            this.lblStartTime.Location = new System.Drawing.Point(18, 132);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(63, 14);
            this.lblStartTime.TabIndex = 6;
            this.lblStartTime.Text = "开始时间";
            // 
            // lblWeekDay
            // 
            this.lblWeekDay.AutoSize = true;
            this.lblWeekDay.Location = new System.Drawing.Point(18, 206);
            this.lblWeekDay.Name = "lblWeekDay";
            this.lblWeekDay.Size = new System.Drawing.Size(42, 14);
            this.lblWeekDay.TabIndex = 12;
            this.lblWeekDay.Text = "每周*";
            // 
            // nudWeekDay
            // 
            this.nudWeekDay.Enabled = false;
            this.nudWeekDay.Location = new System.Drawing.Point(119, 205);
            this.nudWeekDay.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.nudWeekDay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudWeekDay.Name = "nudWeekDay";
            this.nudWeekDay.Size = new System.Drawing.Size(250, 23);
            this.nudWeekDay.TabIndex = 13;
            this.nudWeekDay.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudMonthDay
            // 
            this.nudMonthDay.Enabled = false;
            this.nudMonthDay.Location = new System.Drawing.Point(119, 245);
            this.nudMonthDay.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.nudMonthDay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMonthDay.Name = "nudMonthDay";
            this.nudMonthDay.Size = new System.Drawing.Size(250, 23);
            this.nudMonthDay.TabIndex = 15;
            this.nudMonthDay.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblMonthDay
            // 
            this.lblMonthDay.AutoSize = true;
            this.lblMonthDay.Location = new System.Drawing.Point(18, 246);
            this.lblMonthDay.Name = "lblMonthDay";
            this.lblMonthDay.Size = new System.Drawing.Size(56, 14);
            this.lblMonthDay.TabIndex = 14;
            this.lblMonthDay.Text = "每月*号";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("SimSun", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(256, 129);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(120, 12);
            this.label8.TabIndex = 8;
            this.label8.Text = "小时(**):分钟(**)";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(276, 319);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(99, 23);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOK.Location = new System.Drawing.Point(167, 319);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(99, 23);
            this.btnOK.TabIndex = 20;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cbJobCode
            // 
            this.cbJobCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbJobCode.FormattingEnabled = true;
            this.cbJobCode.Items.AddRange(new object[] {
            "EveryDay",
            "EveryWeek",
            "EveryMonth"});
            this.cbJobCode.Location = new System.Drawing.Point(119, 47);
            this.cbJobCode.Name = "cbJobCode";
            this.cbJobCode.Size = new System.Drawing.Size(250, 22);
            this.cbJobCode.TabIndex = 3;
            // 
            // lblInterval
            // 
            this.lblInterval.AutoSize = true;
            this.lblInterval.Location = new System.Drawing.Point(18, 290);
            this.lblInterval.Name = "lblInterval";
            this.lblInterval.Size = new System.Drawing.Size(63, 14);
            this.lblInterval.TabIndex = 16;
            this.lblInterval.Text = "间隔分钟";
            // 
            // lblEndTimeInfo
            // 
            this.lblEndTimeInfo.AutoSize = true;
            this.lblEndTimeInfo.Font = new System.Drawing.Font("SimSun", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEndTimeInfo.Location = new System.Drawing.Point(256, 174);
            this.lblEndTimeInfo.Name = "lblEndTimeInfo";
            this.lblEndTimeInfo.Size = new System.Drawing.Size(120, 12);
            this.lblEndTimeInfo.TabIndex = 11;
            this.lblEndTimeInfo.Text = "小时(**):分钟(**)";
            // 
            // lblEndTime
            // 
            this.lblEndTime.AutoSize = true;
            this.lblEndTime.Location = new System.Drawing.Point(18, 172);
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size(63, 14);
            this.lblEndTime.TabIndex = 9;
            this.lblEndTime.Text = "结束时间";
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(118, 285);
            this.txtInterval.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.txtInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.ReadOnly = true;
            this.txtInterval.Size = new System.Drawing.Size(250, 23);
            this.txtInterval.TabIndex = 22;
            this.txtInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtStartTime
            // 
            this.txtStartTime.Checked = false;
            this.txtStartTime.CustomFormat = "HH:mm";
            this.txtStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtStartTime.Location = new System.Drawing.Point(120, 125);
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.ShowCheckBox = true;
            this.txtStartTime.ShowUpDown = true;
            this.txtStartTime.Size = new System.Drawing.Size(130, 23);
            this.txtStartTime.TabIndex = 23;
            // 
            // txtEndTime
            // 
            this.txtEndTime.Checked = false;
            this.txtEndTime.CustomFormat = "HH:mm";
            this.txtEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtEndTime.Location = new System.Drawing.Point(120, 165);
            this.txtEndTime.Name = "txtEndTime";
            this.txtEndTime.ShowCheckBox = true;
            this.txtEndTime.ShowUpDown = true;
            this.txtEndTime.Size = new System.Drawing.Size(130, 23);
            this.txtEndTime.TabIndex = 24;
            // 
            // frmSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 354);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbJobCode);
            this.Controls.Add(this.lblWeekDay);
            this.Controls.Add(this.txtEndTime);
            this.Controls.Add(this.txtStartTime);
            this.Controls.Add(this.txtInterval);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblEndTimeInfo);
            this.Controls.Add(this.lblEndTime);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblInterval);
            this.Controls.Add(this.lblStartTime);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblMonthDay);
            this.Controls.Add(this.nudMonthDay);
            this.Controls.Add(this.nudWeekDay);
            this.Controls.Add(this.cbScheduleType);
            this.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "frmSchedule";
            this.Text = "Schedule信息";
            this.Load += new System.EventHandler(this.frmSchedule_Load);
            this.Controls.SetChildIndex(this.cbScheduleType, 0);
            this.Controls.SetChildIndex(this.nudWeekDay, 0);
            this.Controls.SetChildIndex(this.nudMonthDay, 0);
            this.Controls.SetChildIndex(this.lblMonthDay, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.lblStartTime, 0);
            this.Controls.SetChildIndex(this.lblInterval, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.lblEndTime, 0);
            this.Controls.SetChildIndex(this.lblEndTimeInfo, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.txtInterval, 0);
            this.Controls.SetChildIndex(this.txtStartTime, 0);
            this.Controls.SetChildIndex(this.txtEndTime, 0);
            this.Controls.SetChildIndex(this.lblWeekDay, 0);
            this.Controls.SetChildIndex(this.cbJobCode, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.nudWeekDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMonthDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbScheduleType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblStartTime;
        private System.Windows.Forms.Label lblWeekDay;
        private System.Windows.Forms.NumericUpDown nudWeekDay;
        private System.Windows.Forms.NumericUpDown nudMonthDay;
        private System.Windows.Forms.Label lblMonthDay;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ComboBox cbJobCode;
        private System.Windows.Forms.Label lblInterval;
        private System.Windows.Forms.Label lblEndTimeInfo;
        private System.Windows.Forms.Label lblEndTime;
        private System.Windows.Forms.NumericUpDown txtInterval;
        private System.Windows.Forms.DateTimePicker txtStartTime;
        private System.Windows.Forms.DateTimePicker txtEndTime;
    }
}