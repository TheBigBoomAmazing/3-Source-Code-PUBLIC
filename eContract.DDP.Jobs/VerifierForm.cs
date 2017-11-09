//  
//   Rebex Sample Code License
// 
//   Copyright (c) 2009, Rebex CR s.r.o. www.rebex.net, 
//   All rights reserved.
// 
//   Permission to use, copy, modify, and/or distribute this software for any
//   purpose with or without fee is hereby granted
// 
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//   EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
//   OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//   NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
//   HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
//   WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
//   FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
//   OTHER DEALINGS IN THE SOFTWARE.
// 

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace eContract.DDP.Jobs
{
	/// <summary>
	/// Summary description for Verifier.
	/// </summary>
	public class VerifierForm : System.Windows.Forms.Form
    {
        private IContainer components;

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label lblProblem;
		private System.Windows.Forms.Label lblSubject;
		private System.Windows.Forms.Label lblIssuer;
		private System.Windows.Forms.Label lblValidFrom;
		private System.Windows.Forms.Label lblValidTo;
		private System.Windows.Forms.Button btnReject;
		private System.Windows.Forms.Button btnAccept;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label lblHostname;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button btnOkAndTrust;

		private bool _accepted = false;
        private Timer timer1;
		private bool _addIssuerCertificateAuthothorityToTrustedCaStore = false;

		public string Hostname
		{
			set { lblHostname.Text = value; }
		}

		public string Subject
		{
			set { lblSubject.Text = value; }
		}

		public string Issuer
		{
			set { lblIssuer.Text = value; }
		}

		public string ValidFrom
		{
			set { lblValidFrom.Text = value; }
		}

		public string ValidTo
		{
			set { lblValidTo.Text = value; }
		}

		public string Problem
		{
			set { lblProblem.Text = value; }
		}

		public bool Accepted
		{
			get { return _accepted; }
		}

		public bool AddIssuerCertificateAuthothorityToTrustedCaStore
		{
			get { return _addIssuerCertificateAuthothorityToTrustedCaStore; }
		}

		public bool ShowAddIssuerToTrustedCaStoreButton
		{
			get { return btnOkAndTrust.Visible; }
			set { btnOkAndTrust.Visible = value; }
		}

		public VerifierForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
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
            this.btnReject = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblHostname = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblValidTo = new System.Windows.Forms.Label();
            this.lblValidFrom = new System.Windows.Forms.Label();
            this.lblIssuer = new System.Windows.Forms.Label();
            this.lblSubject = new System.Windows.Forms.Label();
            this.lblProblem = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOkAndTrust = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReject
            // 
            this.btnReject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReject.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnReject.Location = new System.Drawing.Point(301, 336);
            this.btnReject.Name = "btnReject";
            this.btnReject.Size = new System.Drawing.Size(87, 25);
            this.btnReject.TabIndex = 2;
            this.btnReject.Text = "Reject";
            this.btnReject.Click += new System.EventHandler(this.btnReject_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAccept.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAccept.Location = new System.Drawing.Point(397, 336);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(87, 25);
            this.btnAccept.TabIndex = 1;
            this.btnAccept.Text = "Accept";
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.Controls.Add(this.lblHostname);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.lblValidTo);
            this.panel1.Controls.Add(this.lblValidFrom);
            this.panel1.Controls.Add(this.lblIssuer);
            this.panel1.Controls.Add(this.lblSubject);
            this.panel1.Controls.Add(this.lblProblem);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(10, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(480, 318);
            this.panel1.TabIndex = 3;
            // 
            // lblHostname
            // 
            this.lblHostname.Location = new System.Drawing.Point(96, 34);
            this.lblHostname.Name = "lblHostname";
            this.lblHostname.Size = new System.Drawing.Size(374, 18);
            this.lblHostname.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(10, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 25);
            this.label8.TabIndex = 12;
            this.label8.Text = "Hostname:";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(10, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(259, 24);
            this.label6.TabIndex = 11;
            this.label6.Text = "Certificate details:";
            // 
            // lblValidTo
            // 
            this.lblValidTo.Location = new System.Drawing.Point(96, 190);
            this.lblValidTo.Name = "lblValidTo";
            this.lblValidTo.Size = new System.Drawing.Size(269, 24);
            this.lblValidTo.TabIndex = 10;
            // 
            // lblValidFrom
            // 
            this.lblValidFrom.Location = new System.Drawing.Point(96, 164);
            this.lblValidFrom.Name = "lblValidFrom";
            this.lblValidFrom.Size = new System.Drawing.Size(269, 24);
            this.lblValidFrom.TabIndex = 9;
            // 
            // lblIssuer
            // 
            this.lblIssuer.Location = new System.Drawing.Point(96, 112);
            this.lblIssuer.Name = "lblIssuer";
            this.lblIssuer.Size = new System.Drawing.Size(374, 43);
            this.lblIssuer.TabIndex = 8;
            // 
            // lblSubject
            // 
            this.lblSubject.Location = new System.Drawing.Point(96, 60);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(374, 43);
            this.lblSubject.TabIndex = 7;
            // 
            // lblProblem
            // 
            this.lblProblem.ForeColor = System.Drawing.Color.Red;
            this.lblProblem.Location = new System.Drawing.Point(10, 233);
            this.lblProblem.Name = "lblProblem";
            this.lblProblem.Size = new System.Drawing.Size(460, 77);
            this.lblProblem.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel2.Location = new System.Drawing.Point(10, 215);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(460, 4);
            this.panel2.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(10, 190);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 24);
            this.label5.TabIndex = 4;
            this.label5.Text = "Valid to:";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(10, 164);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 24);
            this.label4.TabIndex = 3;
            this.label4.Text = "Valid from:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(10, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Issuer:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(10, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Subject:";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(256, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "CERTIFICATE INFORMATION:";
            // 
            // btnOkAndTrust
            // 
            this.btnOkAndTrust.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOkAndTrust.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOkAndTrust.Location = new System.Drawing.Point(10, 336);
            this.btnOkAndTrust.Name = "btnOkAndTrust";
            this.btnOkAndTrust.Size = new System.Drawing.Size(279, 25);
            this.btnOkAndTrust.TabIndex = 5;
            this.btnOkAndTrust.Text = "OK && Always &Trust This Authority";
            this.btnOkAndTrust.Visible = false;
            this.btnOkAndTrust.Click += new System.EventHandler(this.btnOkAndTrust_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // VerifierForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(496, 370);
            this.ControlBox = false;
            this.Controls.Add(this.btnOkAndTrust);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.btnReject);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "VerifierForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Certificate";
            this.Load += new System.EventHandler(this.VerifierForm_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void btnAccept_Click(object sender, System.EventArgs e)
		{
			_accepted = true;
			this.Close();
		}

		private void btnReject_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnOkAndTrust_Click(object sender, System.EventArgs e)
		{
			_accepted = true;
			_addIssuerCertificateAuthothorityToTrustedCaStore = true;

			this.Close();
		}

        private void VerifierForm_Load(object sender, EventArgs e)
        {
            this.timer1.Enabled = true;
        }

        int iTimes = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (iTimes == 3)
            {
                btnAccept_Click(null, null);
            }

            iTimes++;
        }

	}
}
