namespace Soko.Common.Controls
{
  partial class DraggablePanel
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
      this.innerPanel = new System.Windows.Forms.FlowLayoutPanel();
      this.SuspendLayout();
      // 
      // innerPanel
      // 
      this.innerPanel.AutoScroll = true;
      this.innerPanel.BackColor = System.Drawing.Color.Black;
      this.innerPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
      this.innerPanel.Location = new System.Drawing.Point(0, 0);
      this.innerPanel.Name = "innerPanel";
      this.innerPanel.Size = new System.Drawing.Size(430, 342);
      this.innerPanel.TabIndex = 0;
      this.innerPanel.WrapContents = false;
      // 
      // DraggablePanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.Black;
      this.Controls.Add(this.innerPanel);
      this.Name = "DraggablePanel";
      this.Size = new System.Drawing.Size(631, 450);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.FlowLayoutPanel innerPanel;
  }
}
