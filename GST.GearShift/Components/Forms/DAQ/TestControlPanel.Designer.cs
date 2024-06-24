namespace GST.Gearshift.Components.Forms.DAQ
{
  partial class TestControlPanel
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestControlPanel));
      this.gearLockTimer = new System.Windows.Forms.Timer(this.components);
      this.mainPanel = new Soko.Common.Controls.NicePanel();
      this.zf6StatusPane = new Soko.Common.Controls.ExplorerBarPane();
      this.zf6UsbConnLabel = new System.Windows.Forms.Label();
      this.zf6TcuTempLabel = new System.Windows.Forms.Label();
      this.zf6TcuCommandedGearLabel = new System.Windows.Forms.Label();
      this.zf6TcuConnectionLabel = new System.Windows.Forms.Label();
      this.gearInfoPane = new Soko.Common.Controls.ExplorerBarPane();
      this.testStateButton = new Soko.Common.Controls.NiceButton();
      this.minorProgressBar = new System.Windows.Forms.ProgressBar();
      this.minorProgressTextLabel = new System.Windows.Forms.Label();
      this.majorProgressBar = new System.Windows.Forms.ProgressBar();
      this.majorProgressTextLabel = new System.Windows.Forms.Label();
      this.minorProgressBarLabel = new System.Windows.Forms.Label();
      this.majorProgressBarLabel = new System.Windows.Forms.Label();
      this.gearLabel = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.startStopPane = new Soko.Common.Controls.ExplorerBarPane();
      this.stopButton = new Soko.Common.Controls.NiceButton();
      this.manualControlPane = new Soko.Common.Controls.ExplorerBarPane();
      this.gearUpButton = new Soko.Common.Controls.NiceButton();
      this.gearDownButton = new Soko.Common.Controls.NiceButton();
      this.zf6UpdateTimer = new System.Windows.Forms.Timer(this.components);
      this.zf6ActualGear = new System.Windows.Forms.Label();
      this.zf6SelectorPosLabel = new System.Windows.Forms.Label();
      this.zf6FluidTempLabel = new System.Windows.Forms.Label();
      this.mainPanel.SuspendLayout();
      this.zf6StatusPane.SuspendLayout();
      this.gearInfoPane.SuspendLayout();
      this.startStopPane.SuspendLayout();
      this.manualControlPane.SuspendLayout();
      this.SuspendLayout();
      // 
      // gearLockTimer
      // 
      this.gearLockTimer.Interval = 50;
      this.gearLockTimer.Tick += new System.EventHandler(this.gearLockTimer_Tick);
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
      this.mainPanel.backgroundColor1 = System.Drawing.Color.Silver;
      this.mainPanel.backgroundColor2 = System.Drawing.Color.LightGray;
      this.mainPanel.Controls.Add(this.zf6StatusPane);
      this.mainPanel.Controls.Add(this.gearInfoPane);
      this.mainPanel.Controls.Add(this.startStopPane);
      this.mainPanel.Controls.Add(this.manualControlPane);
      this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.mainPanel.DrawBackImage = false;
      this.mainPanel.EnableAutoScrollHorizontal = false;
      this.mainPanel.EnableAutoScrollVertical = false;
      this.mainPanel.Gradient = Soko.Common.Controls.NicePanel.GradientType.None;
      this.mainPanel.HorizontalMargin = 0;
      this.mainPanel.Location = new System.Drawing.Point(0, 0);
      this.mainPanel.Name = "mainPanel";
      this.mainPanel.roundingRadius = 10;
      this.mainPanel.Size = new System.Drawing.Size(199, 950);
      this.mainPanel.TabIndex = 0;
      this.mainPanel.VerticalMargin = 0;
      this.mainPanel.VisibleAutoScrollHorizontal = false;
      this.mainPanel.VisibleAutoScrollVertical = false;
      // 
      // zf6StatusPane
      // 
      this.zf6StatusPane.AllowUserInteraction = false;
      this.zf6StatusPane.BgndColor1 = System.Drawing.Color.DimGray;
      this.zf6StatusPane.BgndColor2 = System.Drawing.Color.Black;
      this.zf6StatusPane.Collapsed = false;
      this.zf6StatusPane.ColorScheme = Soko.Common.Controls.ExplorerBarPane.Theme.Black;
      this.zf6StatusPane.ColorSchemesEnabled = false;
      this.zf6StatusPane.Controls.Add(this.zf6FluidTempLabel);
      this.zf6StatusPane.Controls.Add(this.zf6SelectorPosLabel);
      this.zf6StatusPane.Controls.Add(this.zf6ActualGear);
      this.zf6StatusPane.Controls.Add(this.zf6UsbConnLabel);
      this.zf6StatusPane.Controls.Add(this.zf6TcuTempLabel);
      this.zf6StatusPane.Controls.Add(this.zf6TcuCommandedGearLabel);
      this.zf6StatusPane.Controls.Add(this.zf6TcuConnectionLabel);
      this.zf6StatusPane.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.zf6StatusPane.ForeColor = System.Drawing.Color.White;
      this.zf6StatusPane.HeaderColor1 = System.Drawing.Color.Gray;
      this.zf6StatusPane.HeaderColor2 = System.Drawing.Color.LightBlue;
      this.zf6StatusPane.HeaderColor3 = System.Drawing.Color.Transparent;
      this.zf6StatusPane.HideAfterMouseLeave = false;
      this.zf6StatusPane.HideAfterMouseLeaveDelayMs = 2000;
      this.zf6StatusPane.Location = new System.Drawing.Point(3, 702);
      this.zf6StatusPane.MaximumSize = new System.Drawing.Size(193, 400);
      this.zf6StatusPane.Name = "zf6StatusPane";
      this.zf6StatusPane.NormalHeight = 193;
      this.zf6StatusPane.Size = new System.Drawing.Size(193, 193);
      this.zf6StatusPane.TabIndex = 7;
      this.zf6StatusPane.Text = "Zf6 status:";
      // 
      // zf6UsbConnLabel
      // 
      this.zf6UsbConnLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.zf6UsbConnLabel.Location = new System.Drawing.Point(3, 27);
      this.zf6UsbConnLabel.Name = "zf6UsbConnLabel";
      this.zf6UsbConnLabel.Size = new System.Drawing.Size(187, 23);
      this.zf6UsbConnLabel.TabIndex = 17;
      this.zf6UsbConnLabel.Text = "USB connection:";
      this.zf6UsbConnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // zf6TcuTempLabel
      // 
      this.zf6TcuTempLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.zf6TcuTempLabel.Location = new System.Drawing.Point(1, 134);
      this.zf6TcuTempLabel.Name = "zf6TcuTempLabel";
      this.zf6TcuTempLabel.Size = new System.Drawing.Size(187, 23);
      this.zf6TcuTempLabel.TabIndex = 16;
      this.zf6TcuTempLabel.Text = "TCU temperature:";
      this.zf6TcuTempLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // zf6TcuCommandedGearLabel
      // 
      this.zf6TcuCommandedGearLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.zf6TcuCommandedGearLabel.Location = new System.Drawing.Point(3, 65);
      this.zf6TcuCommandedGearLabel.Name = "zf6TcuCommandedGearLabel";
      this.zf6TcuCommandedGearLabel.Size = new System.Drawing.Size(187, 23);
      this.zf6TcuCommandedGearLabel.TabIndex = 15;
      this.zf6TcuCommandedGearLabel.Text = "TCU commanded gear:";
      this.zf6TcuCommandedGearLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // zf6TcuConnectionLabel
      // 
      this.zf6TcuConnectionLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.zf6TcuConnectionLabel.Location = new System.Drawing.Point(3, 46);
      this.zf6TcuConnectionLabel.Name = "zf6TcuConnectionLabel";
      this.zf6TcuConnectionLabel.Size = new System.Drawing.Size(187, 23);
      this.zf6TcuConnectionLabel.TabIndex = 13;
      this.zf6TcuConnectionLabel.Text = "TCU connection:";
      this.zf6TcuConnectionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // gearInfoPane
      // 
      this.gearInfoPane.AllowUserInteraction = false;
      this.gearInfoPane.BgndColor1 = System.Drawing.Color.DimGray;
      this.gearInfoPane.BgndColor2 = System.Drawing.Color.Black;
      this.gearInfoPane.Collapsed = false;
      this.gearInfoPane.ColorScheme = Soko.Common.Controls.ExplorerBarPane.Theme.Black;
      this.gearInfoPane.ColorSchemesEnabled = false;
      this.gearInfoPane.Controls.Add(this.testStateButton);
      this.gearInfoPane.Controls.Add(this.minorProgressBar);
      this.gearInfoPane.Controls.Add(this.minorProgressTextLabel);
      this.gearInfoPane.Controls.Add(this.majorProgressBar);
      this.gearInfoPane.Controls.Add(this.majorProgressTextLabel);
      this.gearInfoPane.Controls.Add(this.minorProgressBarLabel);
      this.gearInfoPane.Controls.Add(this.majorProgressBarLabel);
      this.gearInfoPane.Controls.Add(this.gearLabel);
      this.gearInfoPane.Controls.Add(this.label2);
      this.gearInfoPane.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.gearInfoPane.ForeColor = System.Drawing.Color.White;
      this.gearInfoPane.HeaderColor1 = System.Drawing.Color.Gray;
      this.gearInfoPane.HeaderColor2 = System.Drawing.Color.LightBlue;
      this.gearInfoPane.HeaderColor3 = System.Drawing.Color.Transparent;
      this.gearInfoPane.HideAfterMouseLeave = false;
      this.gearInfoPane.HideAfterMouseLeaveDelayMs = 2000;
      this.gearInfoPane.Location = new System.Drawing.Point(3, 328);
      this.gearInfoPane.MaximumSize = new System.Drawing.Size(193, 500);
      this.gearInfoPane.Name = "gearInfoPane";
      this.gearInfoPane.NormalHeight = 368;
      this.gearInfoPane.Size = new System.Drawing.Size(193, 368);
      this.gearInfoPane.TabIndex = 6;
      this.gearInfoPane.Text = "Test status:";
      this.gearInfoPane.Move += new System.EventHandler(this.gearInfoPane_Move);
      this.gearInfoPane.Resize += new System.EventHandler(this.gearInfoPane_Resize);
      // 
      // testStateButton
      // 
      this.testStateButton.BackColorOnClicked1 = System.Drawing.Color.Orange;
      this.testStateButton.BackColorOnClicked2 = System.Drawing.Color.Orange;
      this.testStateButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(217)))), ((int)(((byte)(72)))));
      this.testStateButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(238)))), ((int)(((byte)(176)))));
      this.testStateButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
      this.testStateButton.BorderColor = System.Drawing.Color.Transparent;
      this.testStateButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(217)))), ((int)(((byte)(72)))));
      this.testStateButton.BorderWidth = 1;
      this.testStateButton.ContentPadding = new System.Windows.Forms.Padding(0);
      this.testStateButton.DrawBackColorOnFocus = false;
      this.testStateButton.DrawBackgroundImage = false;
      this.testStateButton.DrawBorderOnFocus = false;
      this.testStateButton.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.testStateButton.Image = global::GST.Gearshift.Components.Properties.Resources.ControlPanel_Stopped_96x96;
      this.testStateButton.ImageDisabled = ((System.Drawing.Image)(resources.GetObject("testStateButton.ImageDisabled")));
      this.testStateButton.Location = new System.Drawing.Point(5, 29);
      this.testStateButton.Name = "testStateButton";
      this.testStateButton.Size = new System.Drawing.Size(185, 124);
      this.testStateButton.TabIndex = 15;
      this.testStateButton.Text = "Measurement stopped";
      this.testStateButton.TextImageRelation = Soko.Common.Controls.TextRelation.Above;
      this.testStateButton.TextImageSpacing = 5;
      // 
      // minorProgressBar
      // 
      this.minorProgressBar.Location = new System.Drawing.Point(6, 308);
      this.minorProgressBar.Name = "minorProgressBar";
      this.minorProgressBar.Size = new System.Drawing.Size(183, 19);
      this.minorProgressBar.TabIndex = 11;
      // 
      // minorProgressTextLabel
      // 
      this.minorProgressTextLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.minorProgressTextLabel.Location = new System.Drawing.Point(2, 324);
      this.minorProgressTextLabel.Name = "minorProgressTextLabel";
      this.minorProgressTextLabel.Size = new System.Drawing.Size(187, 23);
      this.minorProgressTextLabel.TabIndex = 14;
      this.minorProgressTextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // majorProgressBar
      // 
      this.majorProgressBar.Location = new System.Drawing.Point(5, 240);
      this.majorProgressBar.Name = "majorProgressBar";
      this.majorProgressBar.Size = new System.Drawing.Size(183, 19);
      this.majorProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
      this.majorProgressBar.TabIndex = 9;
      // 
      // majorProgressTextLabel
      // 
      this.majorProgressTextLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.majorProgressTextLabel.Location = new System.Drawing.Point(2, 255);
      this.majorProgressTextLabel.Name = "majorProgressTextLabel";
      this.majorProgressTextLabel.Size = new System.Drawing.Size(187, 23);
      this.majorProgressTextLabel.TabIndex = 13;
      this.majorProgressTextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // minorProgressBarLabel
      // 
      this.minorProgressBarLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.minorProgressBarLabel.Location = new System.Drawing.Point(3, 288);
      this.minorProgressBarLabel.Name = "minorProgressBarLabel";
      this.minorProgressBarLabel.Size = new System.Drawing.Size(187, 23);
      this.minorProgressBarLabel.TabIndex = 12;
      this.minorProgressBarLabel.Text = "Gear change progress:";
      this.minorProgressBarLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // majorProgressBarLabel
      // 
      this.majorProgressBarLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.majorProgressBarLabel.Location = new System.Drawing.Point(3, 220);
      this.majorProgressBarLabel.Name = "majorProgressBarLabel";
      this.majorProgressBarLabel.Size = new System.Drawing.Size(187, 23);
      this.majorProgressBarLabel.TabIndex = 10;
      this.majorProgressBarLabel.Text = "Overall test progress:";
      this.majorProgressBarLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // gearLabel
      // 
      this.gearLabel.Font = new System.Drawing.Font("Calibri", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.gearLabel.Location = new System.Drawing.Point(6, 186);
      this.gearLabel.Name = "gearLabel";
      this.gearLabel.Size = new System.Drawing.Size(183, 34);
      this.gearLabel.TabIndex = 5;
      this.gearLabel.Text = "N/A";
      this.gearLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label2
      // 
      this.label2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.label2.Location = new System.Drawing.Point(3, 168);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(187, 23);
      this.label2.TabIndex = 8;
      this.label2.Text = "Current script line:";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // startStopPane
      // 
      this.startStopPane.AllowUserInteraction = false;
      this.startStopPane.BgndColor1 = System.Drawing.Color.DimGray;
      this.startStopPane.BgndColor2 = System.Drawing.Color.Black;
      this.startStopPane.Collapsed = false;
      this.startStopPane.ColorScheme = Soko.Common.Controls.ExplorerBarPane.Theme.Black;
      this.startStopPane.ColorSchemesEnabled = false;
      this.startStopPane.Controls.Add(this.stopButton);
      this.startStopPane.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.startStopPane.ForeColor = System.Drawing.Color.White;
      this.startStopPane.HeaderColor1 = System.Drawing.Color.Gray;
      this.startStopPane.HeaderColor2 = System.Drawing.Color.LightBlue;
      this.startStopPane.HeaderColor3 = System.Drawing.Color.Transparent;
      this.startStopPane.HideAfterMouseLeave = false;
      this.startStopPane.HideAfterMouseLeaveDelayMs = 2000;
      this.startStopPane.Location = new System.Drawing.Point(3, 4);
      this.startStopPane.MaximumSize = new System.Drawing.Size(193, 209);
      this.startStopPane.Name = "startStopPane";
      this.startStopPane.NormalHeight = 126;
      this.startStopPane.Size = new System.Drawing.Size(193, 126);
      this.startStopPane.TabIndex = 4;
      this.startStopPane.Text = "Process control";
      this.startStopPane.Move += new System.EventHandler(this.startStopPane_Move);
      this.startStopPane.Resize += new System.EventHandler(this.startStopPane_Resize);
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
      this.stopButton.ContentPadding = new System.Windows.Forms.Padding(0);
      this.stopButton.DrawBackColorOnFocus = true;
      this.stopButton.DrawBackgroundImage = false;
      this.stopButton.DrawBorderOnFocus = true;
      this.stopButton.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.stopButton.Image = global::GST.Gearshift.Components.Properties.Resources.ControlPanel_Stop_icon_64;
      this.stopButton.ImageDisabled = global::GST.Gearshift.Components.Properties.Resources.ControlPanel_Stop_icon_64_BW;
      this.stopButton.Location = new System.Drawing.Point(14, 30);
      this.stopButton.Name = "stopButton";
      this.stopButton.Size = new System.Drawing.Size(170, 83);
      this.stopButton.TabIndex = 2;
      this.stopButton.Text = "End the test";
      this.stopButton.TextImageRelation = Soko.Common.Controls.TextRelation.OnRight;
      this.stopButton.TextImageSpacing = 5;
      this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
      // 
      // manualControlPane
      // 
      this.manualControlPane.AllowUserInteraction = false;
      this.manualControlPane.BgndColor1 = System.Drawing.Color.DimGray;
      this.manualControlPane.BgndColor2 = System.Drawing.Color.Black;
      this.manualControlPane.Collapsed = false;
      this.manualControlPane.ColorScheme = Soko.Common.Controls.ExplorerBarPane.Theme.Black;
      this.manualControlPane.ColorSchemesEnabled = false;
      this.manualControlPane.Controls.Add(this.gearUpButton);
      this.manualControlPane.Controls.Add(this.gearDownButton);
      this.manualControlPane.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.manualControlPane.ForeColor = System.Drawing.Color.White;
      this.manualControlPane.HeaderColor1 = System.Drawing.Color.Gray;
      this.manualControlPane.HeaderColor2 = System.Drawing.Color.LightBlue;
      this.manualControlPane.HeaderColor3 = System.Drawing.Color.Transparent;
      this.manualControlPane.HideAfterMouseLeave = false;
      this.manualControlPane.HideAfterMouseLeaveDelayMs = 2000;
      this.manualControlPane.Location = new System.Drawing.Point(3, 133);
      this.manualControlPane.MaximumSize = new System.Drawing.Size(193, 230);
      this.manualControlPane.Name = "manualControlPane";
      this.manualControlPane.NormalHeight = 192;
      this.manualControlPane.Size = new System.Drawing.Size(193, 192);
      this.manualControlPane.TabIndex = 3;
      this.manualControlPane.Text = "Gear control";
      this.manualControlPane.Move += new System.EventHandler(this.manualControlPane_Move);
      this.manualControlPane.Resize += new System.EventHandler(this.manualControlPane_Resize);
      // 
      // gearUpButton
      // 
      this.gearUpButton.BackColorOnClicked1 = System.Drawing.Color.Orange;
      this.gearUpButton.BackColorOnClicked2 = System.Drawing.Color.Orange;
      this.gearUpButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(217)))), ((int)(((byte)(72)))));
      this.gearUpButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(238)))), ((int)(((byte)(176)))));
      this.gearUpButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
      this.gearUpButton.BorderColor = System.Drawing.Color.Transparent;
      this.gearUpButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(217)))), ((int)(((byte)(72)))));
      this.gearUpButton.BorderWidth = 1;
      this.gearUpButton.ContentPadding = new System.Windows.Forms.Padding(0);
      this.gearUpButton.DrawBackColorOnFocus = true;
      this.gearUpButton.DrawBackgroundImage = false;
      this.gearUpButton.DrawBorderOnFocus = true;
      this.gearUpButton.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.gearUpButton.Image = global::GST.Gearshift.Components.Properties.Resources.ControlPanel_Up_icon_64;
      this.gearUpButton.ImageDisabled = global::GST.Gearshift.Components.Properties.Resources.ControlPanel_Up_icon_BW_64;
      this.gearUpButton.Location = new System.Drawing.Point(14, 39);
      this.gearUpButton.Margin = new System.Windows.Forms.Padding(0);
      this.gearUpButton.Name = "gearUpButton";
      this.gearUpButton.Size = new System.Drawing.Size(170, 64);
      this.gearUpButton.TabIndex = 3;
      this.gearUpButton.Text = "Gear Up";
      this.gearUpButton.TextImageRelation = Soko.Common.Controls.TextRelation.OnRight;
      this.gearUpButton.TextImageSpacing = 5;
      this.gearUpButton.Click += new System.EventHandler(this.gearUpButton_Click);
      // 
      // gearDownButton
      // 
      this.gearDownButton.BackColorOnClicked1 = System.Drawing.Color.Orange;
      this.gearDownButton.BackColorOnClicked2 = System.Drawing.Color.Orange;
      this.gearDownButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(217)))), ((int)(((byte)(72)))));
      this.gearDownButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(238)))), ((int)(((byte)(176)))));
      this.gearDownButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
      this.gearDownButton.BorderColor = System.Drawing.Color.Transparent;
      this.gearDownButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(217)))), ((int)(((byte)(72)))));
      this.gearDownButton.BorderWidth = 1;
      this.gearDownButton.ContentPadding = new System.Windows.Forms.Padding(0);
      this.gearDownButton.DrawBackColorOnFocus = true;
      this.gearDownButton.DrawBackgroundImage = false;
      this.gearDownButton.DrawBorderOnFocus = true;
      this.gearDownButton.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.gearDownButton.Image = global::GST.Gearshift.Components.Properties.Resources.ControlPanel_Down_icon_64;
      this.gearDownButton.ImageDisabled = global::GST.Gearshift.Components.Properties.Resources.ControlPanel_Down_icon_BW_64;
      this.gearDownButton.Location = new System.Drawing.Point(14, 111);
      this.gearDownButton.Margin = new System.Windows.Forms.Padding(0);
      this.gearDownButton.Name = "gearDownButton";
      this.gearDownButton.Size = new System.Drawing.Size(170, 64);
      this.gearDownButton.TabIndex = 4;
      this.gearDownButton.Text = "Gear Down";
      this.gearDownButton.TextImageRelation = Soko.Common.Controls.TextRelation.OnRight;
      this.gearDownButton.TextImageSpacing = 5;
      this.gearDownButton.Click += new System.EventHandler(this.gearDownButton_Click);
      // 
      // zf6UpdateTimer
      // 
      this.zf6UpdateTimer.Enabled = true;
      this.zf6UpdateTimer.Interval = 500;
      this.zf6UpdateTimer.Tick += new System.EventHandler(this.zf6UpdateTimer_Tick);
      // 
      // zf6ActualGear
      // 
      this.zf6ActualGear.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.zf6ActualGear.Location = new System.Drawing.Point(3, 88);
      this.zf6ActualGear.Name = "zf6ActualGear";
      this.zf6ActualGear.Size = new System.Drawing.Size(187, 23);
      this.zf6ActualGear.TabIndex = 18;
      this.zf6ActualGear.Text = "TCU actual gear:";
      this.zf6ActualGear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // zf6SelectorPosLabel
      // 
      this.zf6SelectorPosLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.zf6SelectorPosLabel.Location = new System.Drawing.Point(3, 111);
      this.zf6SelectorPosLabel.Name = "zf6SelectorPosLabel";
      this.zf6SelectorPosLabel.Size = new System.Drawing.Size(187, 23);
      this.zf6SelectorPosLabel.TabIndex = 19;
      this.zf6SelectorPosLabel.Text = "Selector:";
      this.zf6SelectorPosLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // zf6FluidTempLabel
      // 
      this.zf6FluidTempLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.zf6FluidTempLabel.Location = new System.Drawing.Point(1, 157);
      this.zf6FluidTempLabel.Name = "zf6FluidTempLabel";
      this.zf6FluidTempLabel.Size = new System.Drawing.Size(187, 23);
      this.zf6FluidTempLabel.TabIndex = 20;
      this.zf6FluidTempLabel.Text = "Fluid temperature:";
      this.zf6FluidTempLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // TestControlPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.Transparent;
      this.Controls.Add(this.mainPanel);
      this.MinimumSize = new System.Drawing.Size(199, 549);
      this.Name = "TestControlPanel";
      this.Size = new System.Drawing.Size(199, 950);
      this.mainPanel.ResumeLayout(false);
      this.zf6StatusPane.ResumeLayout(false);
      this.gearInfoPane.ResumeLayout(false);
      this.startStopPane.ResumeLayout(false);
      this.manualControlPane.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private Soko.Common.Controls.NicePanel mainPanel;
    private Soko.Common.Controls.NiceButton stopButton;
    private Soko.Common.Controls.NiceButton gearDownButton;
    private Soko.Common.Controls.NiceButton gearUpButton;
    private System.Windows.Forms.Label gearLabel;
    private Soko.Common.Controls.ExplorerBarPane manualControlPane;
    private Soko.Common.Controls.ExplorerBarPane startStopPane;
    private Soko.Common.Controls.ExplorerBarPane gearInfoPane;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ProgressBar majorProgressBar;
    private System.Windows.Forms.Timer gearLockTimer;
    private System.Windows.Forms.ProgressBar minorProgressBar;
    private System.Windows.Forms.Label majorProgressBarLabel;
    private System.Windows.Forms.Label minorProgressBarLabel;
    private System.Windows.Forms.Label majorProgressTextLabel;
    private System.Windows.Forms.Label minorProgressTextLabel;
    private Soko.Common.Controls.NiceButton testStateButton;
    private Soko.Common.Controls.ExplorerBarPane zf6StatusPane;
    private System.Windows.Forms.Label zf6TcuTempLabel;
    private System.Windows.Forms.Label zf6TcuCommandedGearLabel;
    private System.Windows.Forms.Label zf6TcuConnectionLabel;
    private System.Windows.Forms.Label zf6UsbConnLabel;
    private System.Windows.Forms.Timer zf6UpdateTimer;
    private System.Windows.Forms.Label zf6FluidTempLabel;
    private System.Windows.Forms.Label zf6SelectorPosLabel;
    private System.Windows.Forms.Label zf6ActualGear;
    //private System.Windows.Forms.Button button1;
  }
}
