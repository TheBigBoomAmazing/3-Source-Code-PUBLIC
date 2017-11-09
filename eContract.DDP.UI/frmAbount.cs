using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace eContract.DDP.UI
{
    public partial class frmAbount : frmBase
    {
        public frmAbount(string version)
        {
            InitializeComponent();

            this.label_version.Text = "软件版本 " + version;
        }

        private void frmAbount_Load(object sender, EventArgs e)
        {
            base.InitTitle(true,"关于");
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
