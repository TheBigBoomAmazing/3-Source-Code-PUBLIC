using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace eContract.DDP.Common.CommonJob
{
    public partial class JobCfgForm : frmBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public JobCfgForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// job实体
        /// </summary>
        protected JobEntity _jobEntity = null;

        /// <summary>
        /// 设置job实体
        /// </summary>
        /// <param name="entity"></param>
        public virtual void SetJobEntity(JobEntity entity)
        {
            this._jobEntity = entity;        
        }

        /// <summary>
        /// 获得job实体
        /// </summary>
        /// <returns></returns>
        public virtual string GetJobXml()
        {
            if (this._jobEntity != null)
                return this._jobEntity.JobXml;

            return "";
        }
    }
}
