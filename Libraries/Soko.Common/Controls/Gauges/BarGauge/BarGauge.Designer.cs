namespace Bluereach.Components.Controls
{
  partial class BarGauge
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose( bool disposing )
    {
      if ( disposing && ( components != null ) )
      {
        components.Dispose();
      }
      base.Dispose( disposing );
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.mainPanel = new Bluereach.Components.Controls.NicePanel();
      this.gaugeBar = new Bluereach.Components.Controls.GaugeBar();
      this.channelLabel = new System.Windows.Forms.Label();
      this.valueLabel = new System.Windows.Forms.Label();
      this.mainPanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // mainPanel
      // 
      this.mainPanel.AutoplaceElements = false;
      this.mainPanel.AutoScrollHorizontalMaximum = 100;
      this.mainPanel.AutoScrollHorizontalMinimum = 0;
      this.mainPanel.AutoScrollHPos = 0;
      this.mainPanel.AutoScrollVerticalMaximum = 100;
      this.mainPanel.AutoScrollVerticalMinimum = 0;
      this.mainPanel.AutoScrollVPos = 0;
      this.mainPanel.AutoSizeElements = false;
      this.mainPanel.BackColor = System.Drawing.Color.Transparent;
      this.mainPanel.backgroundColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
      this.mainPanel.backgroundColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
      this.mainPanel.Controls.Add(this.gaugeBar);
      this.mainPanel.Controls.Add(this.channelLabel);
      this.mainPanel.Controls.Add(this.valueLabel);
      this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.mainPanel.DrawBackImage = false;
      this.mainPanel.EnableAutoScrollHorizontal = false;
      this.mainPanel.EnableAutoScrollVertical = false;
      this.mainPanel.Gradient = Bluereach.Components.Controls.NicePanel.GradientType.SigmaBellHorizontalUpper;
      this.mainPanel.HorizontalMargin = 0;
      this.mainPanel.Location = new System.Drawing.Point(0, 0);
      this.mainPanel.Name = "mainPanel";
      this.mainPanel.roundingRadius = 10;
      this.mainPanel.Size = new System.Drawing.Size(98, 325);
      this.mainPanel.TabIndex = 0;
      this.mainPanel.VerticalMargin = 0;
      this.mainPanel.VisibleAutoScrollHorizontal = false;
      this.mainPanel.VisibleAutoScrollVertical = false;
      // 
      // gaugeBar
      // 
      this.gaugeBar.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(152)))), ((int)(((byte)(232)))));
      this.gaugeBar.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(86)))), ((int)(((byte)(135)))));
      this.gaugeBar.barColor1 = System.Drawing.Color.Gainsboro;
      this.gaugeBar.barColor2 = System.Drawing.Color.WhiteSmoke;
      this.gaugeBar.FillRatio = 0F;
      this.gaugeBar.Location = new System.Drawing.Point(9, 36);
      this.gaugeBar.Name = "gaugeBar";
      this.gaugeBar.ScaleLinesColor = System.Drawing.Color.DarkGray;
      this.gaugeBar.ScaleLinesCount = ((byte)(10));
      this.gaugeBar.ScaleLinesWidth = ((byte)(1));
      this.gaugeBar.Size = new System.Drawing.Size(80, 256);
      this.gaugeBar.TabIndex = 0;
      // 
      // channelLabel
      // 
      this.channelLabel.Dock = System.Windows.Forms.DockStyle.Top;
      this.channelLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.channelLabel.ForeColor = System.Drawing.Color.Black;
      this.channelLabel.Location = new System.Drawing.Point(0, 0);
      this.channelLabel.Name = "channelLabel";
      this.channelLabel.Size = new System.Drawing.Size(98, 36);
      this.channelLabel.TabIndex = 12;
      this.channelLabel.Text = "NAME";
      this.channelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // valueLabel
      // 
      this.valueLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.valueLabel.ForeColor = System.Drawing.Color.Black;
      this.valueLabel.Location = new System.Drawing.Point(0, 294);
      this.valueLabel.Name = "valueLabel";
      this.valueLabel.Size = new System.Drawing.Size(98, 25);
      this.valueLabel.TabIndex = 11;
      this.valueLabel.Text = "0.0[°C]";
      this.valueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // BarGauge
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.Transparent;
      this.Controls.Add(this.mainPanel);
      this.Name = "BarGauge";
      this.Size = new System.Drawing.Size(98, 325);
      this.mainPanel.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private NicePanel mainPanel;
    private GaugeBar gaugeBar;
    private System.Windows.Forms.Label valueLabel;
    private System.Windows.Forms.Label channelLabel;
  }
}
