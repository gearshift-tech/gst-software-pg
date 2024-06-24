namespace GST.Gearshift.Components.Forms.DAQ
{
  partial class LivePreviewPanel
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
      this.components = new System.ComponentModel.Container();
      this.refreshTimer = new System.Windows.Forms.Timer(this.components);
      this.graphicalPressureZedGraph = new ZedGraph.ZedGraphControl();
      this.nicePanel1 = new Soko.Common.Controls.NicePanel();
      this.progressScrollbar = new CustomScrollBar.ScrollBarEx();
      this.fixedPanel = new Soko.Common.Controls.NicePanel();
      this.showRefCb = new System.Windows.Forms.CheckBox();
      this.allOnOffCb = new System.Windows.Forms.CheckBox();
      this.panel2 = new System.Windows.Forms.Panel();
      this.legendPanel = new Soko.Common.Controls.NicePanel();
      this.panel1 = new System.Windows.Forms.Panel();
      this.stopButton = new Soko.Common.Controls.NiceButton();
      this.pauseButton = new Soko.Common.Controls.NiceButton();
      this.playButton = new Soko.Common.Controls.NiceButton();
      this.nicePanel1.SuspendLayout();
      this.fixedPanel.SuspendLayout();
      this.legendPanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // refreshTimer
      // 
      this.refreshTimer.Interval = 40;
      this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
      // 
      // graphicalPressureZedGraph
      // 
      this.graphicalPressureZedGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.graphicalPressureZedGraph.Location = new System.Drawing.Point(0, 0);
      this.graphicalPressureZedGraph.Name = "graphicalPressureZedGraph";
      this.graphicalPressureZedGraph.ScrollGrace = 0D;
      this.graphicalPressureZedGraph.ScrollMaxX = 0D;
      this.graphicalPressureZedGraph.ScrollMaxY = 0D;
      this.graphicalPressureZedGraph.ScrollMaxY2 = 0D;
      this.graphicalPressureZedGraph.ScrollMinX = 0D;
      this.graphicalPressureZedGraph.ScrollMinY = 0D;
      this.graphicalPressureZedGraph.ScrollMinY2 = 0D;
      this.graphicalPressureZedGraph.Size = new System.Drawing.Size(836, 532);
      this.graphicalPressureZedGraph.TabIndex = 0;
      // 
      // nicePanel1
      // 
      this.nicePanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.nicePanel1.AutoplaceElements = false;
      this.nicePanel1.AutoScrollHorizontalMaximum = 100;
      this.nicePanel1.AutoScrollHorizontalMinimum = 0;
      this.nicePanel1.AutoScrollHPos = 0;
      this.nicePanel1.AutoScrollVerticalMaximum = 100;
      this.nicePanel1.AutoScrollVerticalMinimum = 0;
      this.nicePanel1.AutoScrollVPos = 0;
      this.nicePanel1.AutoSizeElements = false;
      this.nicePanel1.BackColor = System.Drawing.Color.Transparent;
      this.nicePanel1.backgroundColor1 = System.Drawing.Color.LightGray;
      this.nicePanel1.backgroundColor2 = System.Drawing.Color.DarkGray;
      this.nicePanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.nicePanel1.Controls.Add(this.progressScrollbar);
      this.nicePanel1.Controls.Add(this.stopButton);
      this.nicePanel1.Controls.Add(this.pauseButton);
      this.nicePanel1.Controls.Add(this.playButton);
      this.nicePanel1.DrawBackImage = false;
      this.nicePanel1.EnableAutoScrollHorizontal = false;
      this.nicePanel1.EnableAutoScrollVertical = false;
      this.nicePanel1.Gradient = Soko.Common.Controls.NicePanel.GradientType.LinearHorizontalMiddle;
      this.nicePanel1.HorizontalMargin = 0;
      this.nicePanel1.Location = new System.Drawing.Point(0, 531);
      this.nicePanel1.Name = "nicePanel1";
      this.nicePanel1.roundingRadius = 5;
      this.nicePanel1.Size = new System.Drawing.Size(836, 27);
      this.nicePanel1.TabIndex = 7;
      this.nicePanel1.VerticalMargin = 0;
      this.nicePanel1.VisibleAutoScrollHorizontal = false;
      this.nicePanel1.VisibleAutoScrollVertical = false;
      // 
      // progressScrollbar
      // 
      this.progressScrollbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.progressScrollbar.BorderColor = System.Drawing.Color.Transparent;
      this.progressScrollbar.DisabledBorderColor = System.Drawing.Color.Transparent;
      this.progressScrollbar.Location = new System.Drawing.Point(76, 3);
      this.progressScrollbar.Name = "progressScrollbar";
      this.progressScrollbar.Orientation = CustomScrollBar.ScrollBarOrientation.Horizontal;
      this.progressScrollbar.Size = new System.Drawing.Size(759, 19);
      this.progressScrollbar.TabIndex = 5;
      // 
      // fixedPanel
      // 
      this.fixedPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.fixedPanel.AutoplaceElements = true;
      this.fixedPanel.AutoScrollHorizontalMaximum = 130;
      this.fixedPanel.AutoScrollHorizontalMinimum = 0;
      this.fixedPanel.AutoScrollHPos = 0;
      this.fixedPanel.AutoScrollVerticalMaximum = 130;
      this.fixedPanel.AutoScrollVerticalMinimum = 0;
      this.fixedPanel.AutoScrollVPos = 0;
      this.fixedPanel.AutoSizeElements = false;
      this.fixedPanel.backgroundColor1 = System.Drawing.Color.Gainsboro;
      this.fixedPanel.backgroundColor2 = System.Drawing.Color.DarkGray;
      this.fixedPanel.Controls.Add(this.showRefCb);
      this.fixedPanel.Controls.Add(this.allOnOffCb);
      this.fixedPanel.Controls.Add(this.panel2);
      this.fixedPanel.DrawBackImage = false;
      this.fixedPanel.EnableAutoScrollHorizontal = false;
      this.fixedPanel.EnableAutoScrollVertical = false;
      this.fixedPanel.Gradient = Soko.Common.Controls.NicePanel.GradientType.SigmaBellHorizontalUpper;
      this.fixedPanel.HorizontalMargin = 0;
      this.fixedPanel.Location = new System.Drawing.Point(838, 1);
      this.fixedPanel.Name = "fixedPanel";
      this.fixedPanel.roundingRadius = 10;
      this.fixedPanel.Size = new System.Drawing.Size(171, 64);
      this.fixedPanel.TabIndex = 6;
      this.fixedPanel.VerticalMargin = 0;
      this.fixedPanel.VisibleAutoScrollHorizontal = false;
      this.fixedPanel.VisibleAutoScrollVertical = false;
      // 
      // showRefCb
      // 
      this.showRefCb.Appearance = System.Windows.Forms.Appearance.Button;
      this.showRefCb.BackColor = System.Drawing.Color.Transparent;
      this.showRefCb.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.showRefCb.Enabled = false;
      this.showRefCb.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
      this.showRefCb.ForeColor = System.Drawing.Color.Black;
      this.showRefCb.Location = new System.Drawing.Point(2, 0);
      this.showRefCb.Name = "showRefCb";
      this.showRefCb.Size = new System.Drawing.Size(166, 30);
      this.showRefCb.TabIndex = 5;
      this.showRefCb.Text = "Show Reference";
      this.showRefCb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.showRefCb.UseVisualStyleBackColor = false;
      this.showRefCb.CheckedChanged += new System.EventHandler(this.showRefCb_CheckedChanged);
      // 
      // allOnOffCb
      // 
      this.allOnOffCb.Appearance = System.Windows.Forms.Appearance.Button;
      this.allOnOffCb.BackColor = System.Drawing.Color.Transparent;
      this.allOnOffCb.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.allOnOffCb.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
      this.allOnOffCb.ForeColor = System.Drawing.Color.Black;
      this.allOnOffCb.Location = new System.Drawing.Point(2, 30);
      this.allOnOffCb.Name = "allOnOffCb";
      this.allOnOffCb.Size = new System.Drawing.Size(166, 30);
      this.allOnOffCb.TabIndex = 4;
      this.allOnOffCb.Text = "Toggle all";
      this.allOnOffCb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.allOnOffCb.UseVisualStyleBackColor = false;
      this.allOnOffCb.CheckedChanged += new System.EventHandler(this.allOnOffCb_CheckedChanged);
      // 
      // panel2
      // 
      this.panel2.AutoSize = true;
      this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.panel2.Location = new System.Drawing.Point(85, 75);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(0, 0);
      this.panel2.TabIndex = 1;
      // 
      // legendPanel
      // 
      this.legendPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.legendPanel.AutoplaceElements = true;
      this.legendPanel.AutoScrollHorizontalMaximum = 130;
      this.legendPanel.AutoScrollHorizontalMinimum = 0;
      this.legendPanel.AutoScrollHPos = 0;
      this.legendPanel.AutoScrollVerticalMaximum = 130;
      this.legendPanel.AutoScrollVerticalMinimum = 0;
      this.legendPanel.AutoScrollVPos = 0;
      this.legendPanel.AutoSizeElements = false;
      this.legendPanel.backgroundColor1 = System.Drawing.Color.Gainsboro;
      this.legendPanel.backgroundColor2 = System.Drawing.Color.DarkGray;
      this.legendPanel.Controls.Add(this.panel1);
      this.legendPanel.DrawBackImage = false;
      this.legendPanel.EnableAutoScrollHorizontal = false;
      this.legendPanel.EnableAutoScrollVertical = true;
      this.legendPanel.Gradient = Soko.Common.Controls.NicePanel.GradientType.SigmaBellHorizontalUpper;
      this.legendPanel.HorizontalMargin = 0;
      this.legendPanel.Location = new System.Drawing.Point(838, 67);
      this.legendPanel.Name = "legendPanel";
      this.legendPanel.roundingRadius = 10;
      this.legendPanel.Size = new System.Drawing.Size(171, 491);
      this.legendPanel.TabIndex = 5;
      this.legendPanel.VerticalMargin = 0;
      this.legendPanel.VisibleAutoScrollHorizontal = false;
      this.legendPanel.VisibleAutoScrollVertical = false;
      this.legendPanel.Resize += new System.EventHandler(this.legendPanel_Resize);
      // 
      // panel1
      // 
      this.panel1.AutoSize = true;
      this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.panel1.Location = new System.Drawing.Point(85, 245);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(0, 0);
      this.panel1.TabIndex = 1;
      // 
      // stopButton
      // 
      this.stopButton.BackColorOnClicked1 = System.Drawing.Color.Orange;
      this.stopButton.BackColorOnClicked2 = System.Drawing.Color.Orange;
      this.stopButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(217)))), ((int)(((byte)(72)))));
      this.stopButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(238)))), ((int)(((byte)(176)))));
      this.stopButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
      this.stopButton.BorderColor = System.Drawing.Color.Transparent;
      this.stopButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(217)))), ((int)(((byte)(72)))));
      this.stopButton.BorderWidth = 1;
      this.stopButton.ContentPadding = new System.Windows.Forms.Padding(4, 0, 0, 0);
      this.stopButton.DrawBackColorOnFocus = false;
      this.stopButton.DrawBackgroundImage = false;
      this.stopButton.DrawBorderOnFocus = false;
      this.stopButton.Image = global::GST.Gearshift.Components.Properties.Resources.LivePreviewPanel_Stop_16x16;
      this.stopButton.ImageDisabled = global::GST.Gearshift.Components.Properties.Resources.LivePreviewPanel_Stop_16x16_BW;
      this.stopButton.Location = new System.Drawing.Point(51, 1);
      this.stopButton.Name = "stopButton";
      this.stopButton.Size = new System.Drawing.Size(25, 25);
      this.stopButton.TabIndex = 4;
      this.stopButton.TextImageRelation = Soko.Common.Controls.TextRelation.OnRight;
      this.stopButton.TextImageSpacing = 0;
      this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
      // 
      // pauseButton
      // 
      this.pauseButton.BackColorOnClicked1 = System.Drawing.Color.Orange;
      this.pauseButton.BackColorOnClicked2 = System.Drawing.Color.Orange;
      this.pauseButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(217)))), ((int)(((byte)(72)))));
      this.pauseButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(238)))), ((int)(((byte)(176)))));
      this.pauseButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
      this.pauseButton.BorderColor = System.Drawing.Color.Transparent;
      this.pauseButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(217)))), ((int)(((byte)(72)))));
      this.pauseButton.BorderWidth = 1;
      this.pauseButton.ContentPadding = new System.Windows.Forms.Padding(4, 0, 0, 0);
      this.pauseButton.DrawBackColorOnFocus = false;
      this.pauseButton.DrawBackgroundImage = false;
      this.pauseButton.DrawBorderOnFocus = false;
      this.pauseButton.Image = global::GST.Gearshift.Components.Properties.Resources.LivePreviewPanel_Pause_16x16;
      this.pauseButton.ImageDisabled = global::GST.Gearshift.Components.Properties.Resources.LivePreviewPanel_Pause_16x16_BW;
      this.pauseButton.Location = new System.Drawing.Point(26, 1);
      this.pauseButton.Name = "pauseButton";
      this.pauseButton.Size = new System.Drawing.Size(25, 25);
      this.pauseButton.TabIndex = 3;
      this.pauseButton.TextImageRelation = Soko.Common.Controls.TextRelation.OnRight;
      this.pauseButton.TextImageSpacing = 0;
      this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
      // 
      // playButton
      // 
      this.playButton.BackColorOnClicked1 = System.Drawing.Color.Orange;
      this.playButton.BackColorOnClicked2 = System.Drawing.Color.Orange;
      this.playButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(217)))), ((int)(((byte)(72)))));
      this.playButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(238)))), ((int)(((byte)(176)))));
      this.playButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
      this.playButton.BorderColor = System.Drawing.Color.Transparent;
      this.playButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(217)))), ((int)(((byte)(72)))));
      this.playButton.BorderWidth = 1;
      this.playButton.ContentPadding = new System.Windows.Forms.Padding(4, 0, 0, 0);
      this.playButton.DrawBackColorOnFocus = false;
      this.playButton.DrawBackgroundImage = false;
      this.playButton.DrawBorderOnFocus = false;
      this.playButton.Image = global::GST.Gearshift.Components.Properties.Resources.LivePreviewPanel_Play_16x16;
      this.playButton.ImageDisabled = global::GST.Gearshift.Components.Properties.Resources.LivePreviewPanel_Play_16x16_BW;
      this.playButton.Location = new System.Drawing.Point(1, 1);
      this.playButton.Name = "playButton";
      this.playButton.Size = new System.Drawing.Size(25, 25);
      this.playButton.TabIndex = 2;
      this.playButton.TextImageRelation = Soko.Common.Controls.TextRelation.OnRight;
      this.playButton.TextImageSpacing = 0;
      this.playButton.Click += new System.EventHandler(this.playButton_Click);
      // 
      // LivePreviewPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.nicePanel1);
      this.Controls.Add(this.fixedPanel);
      this.Controls.Add(this.legendPanel);
      this.Controls.Add(this.graphicalPressureZedGraph);
      this.Name = "LivePreviewPanel";
      this.Size = new System.Drawing.Size(1012, 558);
      this.nicePanel1.ResumeLayout(false);
      this.fixedPanel.ResumeLayout(false);
      this.fixedPanel.PerformLayout();
      this.legendPanel.ResumeLayout(false);
      this.legendPanel.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private ZedGraph.ZedGraphControl graphicalPressureZedGraph;
    private System.Windows.Forms.Timer refreshTimer;
    private Soko.Common.Controls.NicePanel legendPanel;
    private System.Windows.Forms.Panel panel1;
    private Soko.Common.Controls.NicePanel fixedPanel;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.CheckBox showRefCb;
    private System.Windows.Forms.CheckBox allOnOffCb;
    private Soko.Common.Controls.NicePanel nicePanel1;
    private Soko.Common.Controls.NiceButton playButton;
    private Soko.Common.Controls.NiceButton stopButton;
    private Soko.Common.Controls.NiceButton pauseButton;
    private CustomScrollBar.ScrollBarEx progressScrollbar;
  }
}
