using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace eContract.DDP.UI
{
    public partial class frmMessage : frmBase
    {
        public frmMessage()
        {
            InitializeComponent();
        }

        private void frmMessage_Load(object sender, EventArgs e)
        {
            base.InitTitle(true , "日志查看");
        }

        public void SetInfo(string info)
        {
            this.textBox1.Text = info;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}