﻿namespace TestRig
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
      this.gM6TxxLiveDataPanel1 = new Soko.CanCave.Components.Forms.GM6TxxLiveDataPanel();
      this.SuspendLayout();
      // 
      // gM6TxxLiveDataPanel1
      // 
      this.gM6TxxLiveDataPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.gM6TxxLiveDataPanel1.BackColor = System.Drawing.Color.Black;
      this.gM6TxxLiveDataPanel1.Location = new System.Drawing.Point(12, 12);
      this.gM6TxxLiveDataPanel1.Name = "gM6TxxLiveDataPanel1";
      this.gM6TxxLiveDataPanel1.Size = new System.Drawing.Size(923, 430);
      this.gM6TxxLiveDataPanel1.TabIndex = 0;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(947, 458);
      this.Controls.Add(this.gM6TxxLiveDataPanel1);
      this.Name = "Form1";
      this.Text = "Form1";
      this.ResumeLayout(false);

    }

    #endregion

    private Soko.CanCave.Components.Forms.GM6TxxLiveDataPanel gM6TxxLiveDataPanel1;
  }
}

