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
    public partial class frmBase : Form
    {
        public frmBase()
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

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        private void panelBase_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
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

    }
}
