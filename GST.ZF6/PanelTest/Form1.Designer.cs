﻿namespace PanelTest
{
  partial class Form1
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
      this.mechShifterPanel1 = new GST.ZF6.Components.Forms.MechShifterPanel();
      this.SuspendLayout();
      // 
      // mechShifterPanel1
      // 
      this.mechShifterPanel1.BackColor = System.Drawing.Color.Transparent;
      this.mechShifterPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.mechShifterPanel1.Location = new System.Drawing.Point(0, 0);
      this.mechShifterPanel1.Name = "mechShifterPanel1";
      this.mechShifterPanel1.Size = new System.Drawing.Size(522, 492);
      this.mechShifterPanel1.TabIndex = 0;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(522, 492);
      this.Controls.Add(this.mechShifterPanel1);
      this.Name = "Form1";
      this.Text = "Form1";
      this.ResumeLayout(false);

    }

    #endregion

    private GST.ZF6.Components.Forms.MechShifterPanel mechShifterPanel1;
  }
}

