/************************************************************/
/*                                                          */
/*  Course: CIS 350 - Data Structures                       */
/*                                                          */
/*  Project: Ch16Prb2.CSPrj                                */
/*                                                          */
/*  Source File: AboutDialog.CS                             */
/*                                                          */
/*  Programmer: Rocky Lowery                                */
/*                                                          */
/*  Class: AboutDialog                                      */
/*                                                          */
/************************************************************/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

// Begin partial namespace Ch16Prb2
namespace Ch16Prb2
{
	// Begin class AboutDialog
	public class AboutDialog : Form
  {
    private PictureBox picture1Box;
    private PictureBox picture2Box;
    private Label      label1;
    private Label      label2;
    private Label      label3;
    private Label      label4;
    private Label      label5;
    private Button     okButton;

		public AboutDialog()
		{
			InitializeComponent();
		}

		#region Windows Form Designer generated code
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutDialog));
      this.label2 = new System.Windows.Forms.Label();
      this.picture2Box = new System.Windows.Forms.PictureBox();
      this.okButton = new System.Windows.Forms.Button();
      this.label5 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.picture1Box = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.picture2Box)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.picture1Box)).BeginInit();
      this.SuspendLayout();
      // 
      // label2
      // 
      this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F);
      this.label2.ForeColor = System.Drawing.Color.Black;
      this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
      this.label2.Location = new System.Drawing.Point(113, 89);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(328, 18);
      this.label2.TabIndex = 67;
      this.label2.Text = "Copyright © 2015 - Rocky Lowery · All Rights Reserved";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // picture2Box
      // 
      this.picture2Box.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(65)))), ((int)(((byte)(45)))));
      this.picture2Box.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.picture2Box.Image = ((System.Drawing.Image)(resources.GetObject("picture2Box.Image")));
      this.picture2Box.ImeMode = System.Windows.Forms.ImeMode.NoControl;
      this.picture2Box.Location = new System.Drawing.Point(565, 8);
      this.picture2Box.Name = "picture2Box";
      this.picture2Box.Size = new System.Drawing.Size(100, 100);
      this.picture2Box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.picture2Box.TabIndex = 66;
      this.picture2Box.TabStop = false;
      // 
      // okButton
      // 
      this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.okButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
      this.okButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
      this.okButton.Location = new System.Drawing.Point(448, 14);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(44, 23);
      this.okButton.TabIndex = 62;
      this.okButton.Text = "OK";
      // 
      // label5
      // 
      this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
      this.label5.ForeColor = System.Drawing.Color.Black;
      this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
      this.label5.Location = new System.Drawing.Point(112, 26);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(305, 20);
      this.label5.TabIndex = 65;
      this.label5.Text = "Computer Information Science Department";
      this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label4
      // 
      this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
      this.label4.ForeColor = System.Drawing.Color.Black;
      this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
      this.label4.Location = new System.Drawing.Point(112, 7);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(245, 20);
      this.label4.TabIndex = 64;
      this.label4.Text = "Missouri Southern State University";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label3
      // 
      this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
      this.label3.ForeColor = System.Drawing.Color.Black;
      this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
      this.label3.Location = new System.Drawing.Point(112, 45);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(283, 20);
      this.label3.TabIndex = 63;
      this.label3.Text = "CIS 350 – Data Structures";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label1
      // 
      this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
      this.label1.ForeColor = System.Drawing.Color.Black;
      this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
      this.label1.Location = new System.Drawing.Point(112, 64);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(456, 20);
      this.label1.TabIndex = 61;
      this.label1.Text = "Ch16Prb2 – Solve 0,1 Knapsack Problem Using Fathoming (Version 3.0)";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // picture1Box
      // 
      this.picture1Box.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(189)))));
      this.picture1Box.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.picture1Box.Image = ((System.Drawing.Image)(resources.GetObject("picture1Box.Image")));
      this.picture1Box.ImeMode = System.Windows.Forms.ImeMode.NoControl;
      this.picture1Box.Location = new System.Drawing.Point(7, 8);
      this.picture1Box.Name = "picture1Box";
      this.picture1Box.Size = new System.Drawing.Size(100, 100);
      this.picture1Box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.picture1Box.TabIndex = 60;
      this.picture1Box.TabStop = false;
      // 
      // AboutDialog
      // 
      this.AcceptButton = this.okButton;
      this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
      this.CancelButton = this.okButton;
      this.ClientSize = new System.Drawing.Size(670, 115);
      this.ControlBox = false;
      this.Controls.Add(this.label2);
      this.Controls.Add(this.picture2Box);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.picture1Box);
      this.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Location = new System.Drawing.Point(225, 150);
      this.MaximizeBox = false;
      this.Name = "AboutDialog";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "About Ch16Prb2";
      ((System.ComponentModel.ISupportInitialize)(this.picture2Box)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.picture1Box)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion

  }  // End class AboutDialog

}  // End partial namespace Ch16Prb2
