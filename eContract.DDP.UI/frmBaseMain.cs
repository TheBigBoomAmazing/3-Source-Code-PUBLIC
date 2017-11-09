using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace eContract.DDP.UI
{
    public partial class frmBaseMain : Form
    {
        public frmBaseMain()
        {
            InitializeComponent();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
        public  void InitTitle(bool bShow,string strTitle)
        {
            //if (bShow)
            //    this.lblTitle.Text = "--" + strTitle;
            //else
            //    this.lblTitle.Text =  strTitle;
        }



        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if ((!(ActiveControl is Button)) && (keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Enter))
            {
                if (keyData == Keys.Enter)
                {
                    System.Windows.Forms.SendKeys.Send("{TAB}");
                    return true;
                }
                if (keyData == Keys.Down)
                    System.Windows.Forms.SendKeys.Send("{TAB}");

                SendKeys.Send("+{Tab}");
                return true;
            }
            else
            {
                if (keyData == Keys.Escape)
                {
                    this.Close();
                    return true;
                }
                else
                    return base.ProcessCmdKey(ref msg, keyData);
            }

        }

        private void picMin_Click(object sender, EventArgs e)
        {
            this.WindowState=FormWindowState.Minimized;
        }

    }
}
