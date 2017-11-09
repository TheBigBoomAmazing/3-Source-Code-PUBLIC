using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using eContract.DDP.Common;

namespace eContract.DDP.Common.CommonJob
{
    public partial class BaseJobCfgForm : JobCfgForm
    {
               
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseJobCfgForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置job实体
        /// </summary>
        /// <param name="entity"></param>
        public override void SetJobEntity(JobEntity entity)
        {
            this._jobEntity = entity;

            this.txtJobXml.Text = this._jobEntity.JobXml;
        }

        /// <summary>
        /// 获得job实体
        /// </summary>
        /// <returns></returns>
        public override string GetJobXml()
        {
            return this.txtJobXml.Text;
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void BaseJobCfgForm_Load(object sender, EventArgs e)
        {
            base.InitTitle(true, "任务属性");
        }

    }
}