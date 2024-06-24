namespace Bluereach.Components.Controls
{
  partial class SolenoidGauge
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
      this.components = new System.ComponentModel.Container();
      this.mainPanel = new Bluereach.Components.Controls.NicePanel();
      this.currentGaugeBar = new Bluereach.Components.Controls.GaugeBar();
      this.driveTrackbar = new Bluereach.Components.Controls.NiceTrackBar();
      this.currentCaptionLabel = new System.Windows.Forms.Label();
      this.driveCaptionLabel = new System.Windows.Forms.Label();
      this.channelLabel = new System.Windows.Forms.Label();
      this.currentValueLabel = new System.Windows.Forms.Label();
      this.driveValueLabel = new System.Windows.Forms.Label();
      this.disableDelayTmr = new System.Windows.Forms.Timer(this.components);
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
      this.mainPanel.Controls.Add(this.currentGaugeBar);
      this.mainPanel.Controls.Add(this.driveTrackbar);
      this.mainPanel.Controls.Add(this.currentCaptionLabel);
      this.mainPanel.Controls.Add(this.driveCaptionLabel);
      this.mainPanel.Controls.Add(this.channelLabel);
      this.mainPanel.Controls.Add(this.currentValueLabel);
      this.mainPanel.Controls.Add(this.driveValueLabel);
      this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.mainPanel.DrawBackImage = false;
      this.mainPanel.EnableAutoScrollHorizontal = false;
      this.mainPanel.EnableAutoScrollVertical = false;
      this.mainPanel.Gradient = Bluereach.Components.Controls.NicePanel.GradientType.SigmaBellHorizontalUpper;
      this.mainPanel.HorizontalMargin = 0;
      this.mainPanel.Location = new System.Drawing.Point(0, 0);
      this.mainPanel.Name = "mainPanel";
      this.mainPanel.roundingRadius = 10;
      this.mainPanel.Size = new System.Drawing.Size(135, 335);
      this.mainPanel.TabIndex = 0;
      this.mainPanel.VerticalMargin = 0;
      this.mainPanel.VisibleAutoScrollHorizontal = false;
      this.mainPanel.VisibleAutoScrollVertical = false;
      // 
      // currentGaugeBar
      // 
      this.currentGaugeBar.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(152)))), ((int)(((byte)(232)))));
      this.currentGaugeBar.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(86)))), ((int)(((byte)(135)))));
      this.currentGaugeBar.barColor1 = System.Drawing.Color.Gainsboro;
      this.currentGaugeBar.barColor2 = System.Drawing.Color.WhiteSmoke;
      this.currentGaugeBar.FillRatio = 0F;
      this.currentGaugeBar.Location = new System.Drawing.Point(6, 52);
      this.currentGaugeBar.Name = "currentGaugeBar";
      this.currentGaugeBar.ScaleLinesColor = System.Drawing.Color.DarkGray;
      this.currentGaugeBar.ScaleLinesCount = ((byte)(10));
      this.currentGaugeBar.ScaleLinesWidth = ((byte)(1));
      this.currentGaugeBar.Size = new System.Drawing.Size(72, 256);
      this.currentGaugeBar.TabIndex = 0;
      // 
      // driveTrackbar
      // 
      this.driveTrackbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
      this.driveTrackbar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
      this.driveTrackbar.BorderStyle = Bluereach.Components.Controls.NiceBorderStyle.Solid;
      this.driveTrackbar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.driveTrackbar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
      this.driveTrackbar.IndentHeight = 0;
      this.driveTrackbar.IndentWidth = 2;
      this.driveTrackbar.LargeChange = 5;
      this.driveTrackbar.Location = new System.Drawing.Point(85, 52);
      this.driveTrackbar.Maximum = 100;
      this.driveTrackbar.Minimum = 0;
      this.driveTrackbar.Name = "driveTrackbar";
      this.driveTrackbar.Orientation = System.Windows.Forms.Orientation.Vertical;
      this.driveTrackbar.Padding = new System.Windows.Forms.Padding(5);
      this.driveTrackbar.Size = new System.Drawing.Size(44, 256);
      this.driveTrackbar.TabIndex = 10;
      this.driveTrackbar.TickColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(146)))), ((int)(((byte)(148)))));
      this.driveTrackbar.TickFrequency = 25;
      this.driveTrackbar.TickHeight = 1;
      this.driveTrackbar.TrackerColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(130)))), ((int)(((byte)(198)))));
      this.driveTrackbar.TrackerSize = new System.Drawing.Size(16, 16);
      this.driveTrackbar.TrackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(93)))), ((int)(((byte)(90)))));
      this.driveTrackbar.TrackLineHeight = 3;
      this.driveTrackbar.Value = 0;
      this.driveTrackbar.ValueChanged += new Bluereach.Components.Controls.ValueChangedHandler(this.driveTrackbar_ValueChanged);
      // 
      // currentCaptionLabel
      // 
      this.currentCaptionLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.currentCaptionLabel.ForeColor = System.Drawing.Color.Black;
      this.currentCaptionLabel.Location = new System.Drawing.Point(7, 37);
      this.currentCaptionLabel.Name = "currentCaptionLabel";
      this.currentCaptionLabel.Size = new System.Drawing.Size(66, 13);
      this.currentCaptionLabel.TabIndex = 14;
      this.currentCaptionLabel.Text = "CURRENT";
      this.currentCaptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // driveCaptionLabel
      // 
      this.driveCaptionLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.driveCaptionLabel.ForeColor = System.Drawing.Color.Black;
      this.driveCaptionLabel.Location = new System.Drawing.Point(89, 37);
      this.driveCaptionLabel.Name = "driveCaptionLabel";
      this.driveCaptionLabel.Size = new System.Drawing.Size(47, 13);
      this.driveCaptionLabel.TabIndex = 13;
      this.driveCaptionLabel.Text = "DRIVE";
      this.driveCaptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // channelLabel
      // 
      this.channelLabel.Dock = System.Windows.Forms.DockStyle.Top;
      this.channelLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.channelLabel.ForeColor = System.Drawing.Color.Black;
      this.channelLabel.Location = new System.Drawing.Point(0, 0);
      this.channelLabel.Name = "channelLabel";
      this.channelLabel.Size = new System.Drawing.Size(135, 36);
      this.channelLabel.TabIndex = 12;
      this.channelLabel.Text = "NAME";
      this.channelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // currentValueLabel
      // 
      this.currentValueLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.currentValueLabel.ForeColor = System.Drawing.Color.Black;
      this.currentValueLabel.Location = new System.Drawing.Point(7, 310);
      this.currentValueLabel.Name = "currentValueLabel";
      this.currentValueLabel.Size = new System.Drawing.Size(69, 25);
      this.currentValueLabel.TabIndex = 11;
      this.currentValueLabel.Text = "0.00[A]";
      this.currentValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // driveValueLabel
      // 
      this.driveValueLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.driveValueLabel.ForeColor = System.Drawing.Color.Black;
      this.driveValueLabel.Location = new System.Drawing.Point(85, 310);
      this.driveValueLabel.Name = "driveValueLabel";
      this.driveValueLabel.Size = new System.Drawing.Size(44, 25);
      this.driveValueLabel.TabIndex = 11;
      this.driveValueLabel.Text = "100%";
      this.driveValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // disableDelayTmr
      // 
      this.disableDelayTmr.Tick += new System.EventHandler(this.disableDelayTmr_Tick);
      // 
      // SolenoidGauge
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.Transparent;
      this.Controls.Add(this.mainPanel);
      this.Name = "SolenoidGauge";
      this.Size = new System.Drawing.Size(135, 335);
      this.mainPanel.ResumeLayout(false);
      this.mainPanel.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private NicePanel mainPanel;
    private GaugeBar currentGaugeBar;
    private NiceTrackBar driveTrackbar;
    private System.Windows.Forms.Label currentValueLabel;
    private System.Windows.Forms.Label driveValueLabel;
    private System.Windows.Forms.Label channelLabel;
    private System.Windows.Forms.Label currentCaptionLabel;
    private System.Windows.Forms.Label driveCaptionLabel;
    private System.Windows.Forms.Timer disableDelayTmr;
  }
}
