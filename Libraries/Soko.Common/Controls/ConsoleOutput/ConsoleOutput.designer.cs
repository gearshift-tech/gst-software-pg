using Soko.Common.Controls;

namespace Soko.Common.Controls
{
  partial class ConsoleOutput
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.m_scrollPanel = new ScrolledPanel();
      this.m_TextBoxConsole = new System.Windows.Forms.RichTextBox();
      this.m_scrollPanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // m_scrollPanel
      // 
      this.m_scrollPanel.BackColor = System.Drawing.Color.Gainsboro;
      this.m_scrollPanel.Controls.Add(this.m_TextBoxConsole);
      this.m_scrollPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_scrollPanel.Location = new System.Drawing.Point(0, 0);
      this.m_scrollPanel.Name = "m_scrollPanel";
      this.m_scrollPanel.Size = new System.Drawing.Size(388, 324);
      this.m_scrollPanel.TabIndex = 0;
      // 
      // m_TextBoxConsole
      // 
      this.m_TextBoxConsole.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_TextBoxConsole.Location = new System.Drawing.Point(0, 0);
      this.m_TextBoxConsole.Name = "m_TextBoxConsole";
      this.m_TextBoxConsole.Size = new System.Drawing.Size(388, 324);
      this.m_TextBoxConsole.TabIndex = 1;
      this.m_TextBoxConsole.Text = "";
      // 
      // ConsoleOutput
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.m_scrollPanel);
      this.Name = "ConsoleOutput";
      this.Size = new System.Drawing.Size(388, 324);
      this.m_scrollPanel.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private ScrolledPanel m_scrollPanel;
    private System.Windows.Forms.RichTextBox m_TextBoxConsole;
  }
}
