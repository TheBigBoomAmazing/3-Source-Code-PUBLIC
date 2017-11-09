using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace eContract.DDP.Common.CommonJob
{
    public partial class JobRunControl : UserControl
    {
        public JobRunControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 返回参数
        /// </summary>
        /// <returns></returns>
        public virtual Hashtable GetParamters()
        {
            return null;
        }
    }
}
