namespace Soko.CanCave.Components.Forms
{
  partial class Gm6T40BarePanel
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Gm6T40BarePanel));
            this.guiUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.tcmTempSTG = new Soko.CanCave.Components.Controls.SimpleTempGauge();
            this.fluidTempSTG = new Soko.CanCave.Components.Controls.SimpleTempGauge();
            this.engineTempSTG = new Soko.CanCave.Components.Controls.SimpleTempGauge();
            this.tcmTypeComboBox = new Soko.Common.Controls.NiceComboBox();
            this.S4SolenoidLg = new Soko.Common.Controls.Gauges.LinearGauge();
            this.ss1SolenoidLg = new Soko.Common.Controls.Gauges.LinearGauge();
            this.ss2SolenoidLg = new Soko.Common.Controls.Gauges.LinearGauge();
            this.engineTorqueLabel = new System.Windows.Forms.Label();
            this.tpsLabel = new System.Windows.Forms.Label();
            this.engineSpeedLabel = new System.Windows.Forms.Label();
            this.engineTorqueLabelLabel = new System.Windows.Forms.Label();
            this.tpsLabelLabel = new System.Windows.Forms.Label();
            this.engineSpeedLabelLabel = new System.Windows.Forms.Label();
            this.engineTorqueTB = new Soko.Common.Controls.NiceTrackBar();
            this.tpsTB = new Soko.Common.Controls.NiceTrackBar();
            this.engineSpeedTB = new Soko.Common.Controls.NiceTrackBar();
            this.TFPSW4NI = new Soko.Common.Controls.NiceIndicator();
            this.TFPSW3NI = new Soko.Common.Controls.NiceIndicator();
            this.TFPSW5NI = new Soko.Common.Controls.NiceIndicator();
            this.TFPSW1NI = new Soko.Common.Controls.NiceIndicator();
            this.TCCSolenoidLg = new Soko.Common.Controls.Gauges.LinearGauge();
            this.S5SolenoidLg = new Soko.Common.Controls.Gauges.LinearGauge();
            this.S3SolenoidLg = new Soko.Common.Controls.Gauges.LinearGauge();
            this.S2SolenoidLg = new Soko.Common.Controls.Gauges.LinearGauge();
            this.LineSolenoidLg = new Soko.Common.Controls.Gauges.LinearGauge();
            this.slipSpeedAG = new Soko.Common.Controls.AnalogueGauge();
            this.outputSpeedAG = new Soko.Common.Controls.AnalogueGauge();
            this.inputSpeedAG = new Soko.Common.Controls.AnalogueGauge();
            this.panel2 = new System.Windows.Forms.Panel();
            this.readVinBtn = new System.Windows.Forms.Button();
            this.OSSniceTB = new Soko.Common.Controls.NiceTrackBar();
            this.ISSniceTB = new Soko.Common.Controls.NiceTrackBar();
            this.oss2ValueLabel = new System.Windows.Forms.Label();
            this.oss1ValueLabel = new System.Windows.Forms.Label();
            this.feedbackLabel = new System.Windows.Forms.Label();
            this.tccOffBtn = new System.Windows.Forms.Button();
            this.tccOnBtn = new System.Windows.Forms.Button();
            this.resetAdaptsBtn = new System.Windows.Forms.Button();
            this.resetTcmBtn = new System.Windows.Forms.Button();
            this.clearCodesBtn = new System.Windows.Forms.Button();
            this.getCodesBtn = new System.Windows.Forms.Button();
            this.select6Btn = new System.Windows.Forms.Button();
            this.select5Btn = new System.Windows.Forms.Button();
            this.select4Btn = new System.Windows.Forms.Button();
            this.select3Btn = new System.Windows.Forms.Button();
            this.select2Btn = new System.Windows.Forms.Button();
            this.select1Btn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.gearLeverPositionLabel = new System.Windows.Forms.Label();
            this.actualGearLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.desiredGearLabel = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dtcLabel = new System.Windows.Forms.Label();
            this.guiUpdateSlowTimer = new System.Windows.Forms.Timer(this.components);
            this.vinLabelLabel = new System.Windows.Forms.Label();
            this.vinLabel = new System.Windows.Forms.Label();
            this.usbStatusNiceInd = new Soko.Common.Controls.NiceIndicator();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // guiUpdateTimer
            // 
            this.guiUpdateTimer.Enabled = true;
            this.guiUpdateTimer.Interval = 50;
            this.guiUpdateTimer.Tick += new System.EventHandler(this.guiUpdateTimer_Tick);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tcmTempSTG);
            this.panel1.Controls.Add(this.fluidTempSTG);
            this.panel1.Controls.Add(this.engineTempSTG);
            this.panel1.Controls.Add(this.tcmTypeComboBox);
            this.panel1.Controls.Add(this.S4SolenoidLg);
            this.panel1.Controls.Add(this.ss1SolenoidLg);
            this.panel1.Controls.Add(this.ss2SolenoidLg);
            this.panel1.Controls.Add(this.engineTorqueLabel);
            this.panel1.Controls.Add(this.tpsLabel);
            this.panel1.Controls.Add(this.engineSpeedLabel);
            this.panel1.Controls.Add(this.engineTorqueLabelLabel);
            this.panel1.Controls.Add(this.tpsLabelLabel);
            this.panel1.Controls.Add(this.engineSpeedLabelLabel);
            this.panel1.Controls.Add(this.engineTorqueTB);
            this.panel1.Controls.Add(this.tpsTB);
            this.panel1.Controls.Add(this.engineSpeedTB);
            this.panel1.Controls.Add(this.TFPSW4NI);
            this.panel1.Controls.Add(this.TFPSW3NI);
            this.panel1.Controls.Add(this.TFPSW5NI);
            this.panel1.Controls.Add(this.TFPSW1NI);
            this.panel1.Controls.Add(this.TCCSolenoidLg);
            this.panel1.Controls.Add(this.S5SolenoidLg);
            this.panel1.Controls.Add(this.S3SolenoidLg);
            this.panel1.Controls.Add(this.S2SolenoidLg);
            this.panel1.Controls.Add(this.LineSolenoidLg);
            this.panel1.Controls.Add(this.slipSpeedAG);
            this.panel1.Controls.Add(this.outputSpeedAG);
            this.panel1.Controls.Add(this.inputSpeedAG);
            this.panel1.Location = new System.Drawing.Point(260, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(791, 635);
            this.panel1.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 26);
            this.label1.TabIndex = 52;
            this.label1.Text = "Select TCM type:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tcmTempSTG
            // 
            this.tcmTempSTG.Image = global::Soko.CanCave.Components.Properties.Resources.TCM_OFF;
            this.tcmTempSTG.Location = new System.Drawing.Point(233, 128);
            this.tcmTempSTG.Name = "tcmTempSTG";
            this.tcmTempSTG.Size = new System.Drawing.Size(50, 110);
            this.tcmTempSTG.TabIndex = 50;
            this.tcmTempSTG.Value = 0D;
            // 
            // fluidTempSTG
            // 
            this.fluidTempSTG.Image = global::Soko.CanCave.Components.Properties.Resources.FLUID_OFF;
            this.fluidTempSTG.Location = new System.Drawing.Point(172, 128);
            this.fluidTempSTG.Name = "fluidTempSTG";
            this.fluidTempSTG.Size = new System.Drawing.Size(50, 110);
            this.fluidTempSTG.TabIndex = 49;
            this.fluidTempSTG.Value = 0D;
            // 
            // engineTempSTG
            // 
            this.engineTempSTG.Image = global::Soko.CanCave.Components.Properties.Resources.ENGINE_OFF;
            this.engineTempSTG.Location = new System.Drawing.Point(111, 128);
            this.engineTempSTG.Name = "engineTempSTG";
            this.engineTempSTG.Size = new System.Drawing.Size(50, 110);
            this.engineTempSTG.TabIndex = 48;
            this.engineTempSTG.Value = 0D;
            // 
            // tcmTypeComboBox
            // 
            this.tcmTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tcmTypeComboBox.FormattingEnabled = true;
            this.tcmTypeComboBox.GotFocusBorderColor = System.Drawing.Color.Black;
            this.tcmTypeComboBox.GotFocusBorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.tcmTypeComboBox.GotFocusDropDownButtonState = Soko.Common.Controls.NiceComboBox.EugenisButtonState.Original;
            this.tcmTypeComboBox.Items.AddRange(new object[] {
            "GM6T 35/40",
            "GM6L / 6T70"});
            this.tcmTypeComboBox.Location = new System.Drawing.Point(19, 58);
            this.tcmTypeComboBox.LostFocusBorderColor = System.Drawing.Color.Gray;
            this.tcmTypeComboBox.LostFocusBorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.tcmTypeComboBox.LostFocusDropDownButtonState = Soko.Common.Controls.NiceComboBox.EugenisButtonState.Flat;
            this.tcmTypeComboBox.Name = "tcmTypeComboBox";
            this.tcmTypeComboBox.Size = new System.Drawing.Size(203, 21);
            this.tcmTypeComboBox.TabIndex = 47;
            this.tcmTypeComboBox.UseGotFocusStyle = false;
            this.tcmTypeComboBox.UseLostFocusStyle = false;
            this.tcmTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.tcmTypeComboBox_SelectedIndexChanged);
            // 
            // S4SolenoidLg
            // 
            this.S4SolenoidLg.BackColor = System.Drawing.Color.Transparent;
            this.S4SolenoidLg.Location = new System.Drawing.Point(672, 3);
            this.S4SolenoidLg.MajorGrid = 5;
            this.S4SolenoidLg.MaxValue = 600D;
            this.S4SolenoidLg.MinorGrid = 2;
            this.S4SolenoidLg.Name = "S4SolenoidLg";
            this.S4SolenoidLg.Size = new System.Drawing.Size(64, 266);
            this.S4SolenoidLg.TabIndex = 27;
            this.S4SolenoidLg.Text = "PCS4";
            this.S4SolenoidLg.Unit = "";
            // 
            // ss1SolenoidLg
            // 
            this.ss1SolenoidLg.BackColor = System.Drawing.Color.Transparent;
            this.ss1SolenoidLg.Location = new System.Drawing.Point(318, 3);
            this.ss1SolenoidLg.MajorGrid = 5;
            this.ss1SolenoidLg.MaxValue = 600D;
            this.ss1SolenoidLg.MinorGrid = 2;
            this.ss1SolenoidLg.Name = "ss1SolenoidLg";
            this.ss1SolenoidLg.Size = new System.Drawing.Size(64, 266);
            this.ss1SolenoidLg.TabIndex = 46;
            this.ss1SolenoidLg.Text = "SS1";
            this.ss1SolenoidLg.Unit = "";
            // 
            // ss2SolenoidLg
            // 
            this.ss2SolenoidLg.BackColor = System.Drawing.Color.Transparent;
            this.ss2SolenoidLg.Location = new System.Drawing.Point(377, 3);
            this.ss2SolenoidLg.MajorGrid = 5;
            this.ss2SolenoidLg.MaxValue = 600D;
            this.ss2SolenoidLg.MinorGrid = 2;
            this.ss2SolenoidLg.Name = "ss2SolenoidLg";
            this.ss2SolenoidLg.Size = new System.Drawing.Size(64, 266);
            this.ss2SolenoidLg.TabIndex = 45;
            this.ss2SolenoidLg.Text = "SS2";
            this.ss2SolenoidLg.Unit = "";
            // 
            // engineTorqueLabel
            // 
            this.engineTorqueLabel.AutoSize = true;
            this.engineTorqueLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.engineTorqueLabel.Location = new System.Drawing.Point(687, 608);
            this.engineTorqueLabel.Name = "engineTorqueLabel";
            this.engineTorqueLabel.Size = new System.Drawing.Size(49, 21);
            this.engineTorqueLabel.TabIndex = 44;
            this.engineTorqueLabel.Text = "0 Nm";
            // 
            // tpsLabel
            // 
            this.tpsLabel.AutoSize = true;
            this.tpsLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpsLabel.Location = new System.Drawing.Point(687, 556);
            this.tpsLabel.Name = "tpsLabel";
            this.tpsLabel.Size = new System.Drawing.Size(36, 21);
            this.tpsLabel.TabIndex = 43;
            this.tpsLabel.Text = "0 %";
            // 
            // engineSpeedLabel
            // 
            this.engineSpeedLabel.AutoSize = true;
            this.engineSpeedLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.engineSpeedLabel.Location = new System.Drawing.Point(687, 502);
            this.engineSpeedLabel.Name = "engineSpeedLabel";
            this.engineSpeedLabel.Size = new System.Drawing.Size(57, 21);
            this.engineSpeedLabel.TabIndex = 42;
            this.engineSpeedLabel.Text = "0 RPM";
            // 
            // engineTorqueLabelLabel
            // 
            this.engineTorqueLabelLabel.AutoSize = true;
            this.engineTorqueLabelLabel.Location = new System.Drawing.Point(687, 587);
            this.engineTorqueLabelLabel.Name = "engineTorqueLabelLabel";
            this.engineTorqueLabelLabel.Size = new System.Drawing.Size(77, 13);
            this.engineTorqueLabelLabel.TabIndex = 41;
            this.engineTorqueLabelLabel.Text = "Engine Torque";
            // 
            // tpsLabelLabel
            // 
            this.tpsLabelLabel.AutoSize = true;
            this.tpsLabelLabel.Location = new System.Drawing.Point(685, 533);
            this.tpsLabelLabel.Name = "tpsLabelLabel";
            this.tpsLabelLabel.Size = new System.Drawing.Size(28, 13);
            this.tpsLabelLabel.TabIndex = 40;
            this.tpsLabelLabel.Text = "TPS";
            // 
            // engineSpeedLabelLabel
            // 
            this.engineSpeedLabelLabel.AutoSize = true;
            this.engineSpeedLabelLabel.Location = new System.Drawing.Point(687, 481);
            this.engineSpeedLabelLabel.Name = "engineSpeedLabelLabel";
            this.engineSpeedLabelLabel.Size = new System.Drawing.Size(72, 13);
            this.engineSpeedLabelLabel.TabIndex = 39;
            this.engineSpeedLabelLabel.Text = "Engine speed";
            // 
            // engineTorqueTB
            // 
            this.engineTorqueTB.BackColor = System.Drawing.Color.Transparent;
            this.engineTorqueTB.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.engineTorqueTB.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.engineTorqueTB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(125)))), ((int)(((byte)(123)))));
            this.engineTorqueTB.IndentHeight = 6;
            this.engineTorqueTB.Location = new System.Drawing.Point(30, 587);
            this.engineTorqueTB.Maximum = 400;
            this.engineTorqueTB.Minimum = 0;
            this.engineTorqueTB.Name = "engineTorqueTB";
            this.engineTorqueTB.Size = new System.Drawing.Size(652, 47);
            this.engineTorqueTB.TabIndex = 38;
            this.engineTorqueTB.TickColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(146)))), ((int)(((byte)(148)))));
            this.engineTorqueTB.TickFrequency = 50;
            this.engineTorqueTB.TickHeight = 4;
            this.engineTorqueTB.TrackerColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(130)))), ((int)(((byte)(198)))));
            this.engineTorqueTB.TrackerSize = new System.Drawing.Size(16, 16);
            this.engineTorqueTB.TrackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(93)))), ((int)(((byte)(90)))));
            this.engineTorqueTB.TrackLineHeight = 3;
            this.engineTorqueTB.Value = 0;
            this.engineTorqueTB.ValueChanged += new Soko.Common.Controls.ValueChangedHandler(this.engineTorqueTB_ValueChanged);
            // 
            // tpsTB
            // 
            this.tpsTB.BackColor = System.Drawing.Color.Transparent;
            this.tpsTB.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.tpsTB.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpsTB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(125)))), ((int)(((byte)(123)))));
            this.tpsTB.IndentHeight = 6;
            this.tpsTB.Location = new System.Drawing.Point(30, 534);
            this.tpsTB.Maximum = 100;
            this.tpsTB.Minimum = 0;
            this.tpsTB.Name = "tpsTB";
            this.tpsTB.Size = new System.Drawing.Size(650, 47);
            this.tpsTB.TabIndex = 37;
            this.tpsTB.TickColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(146)))), ((int)(((byte)(148)))));
            this.tpsTB.TickFrequency = 10;
            this.tpsTB.TickHeight = 4;
            this.tpsTB.TrackerColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(130)))), ((int)(((byte)(198)))));
            this.tpsTB.TrackerSize = new System.Drawing.Size(16, 16);
            this.tpsTB.TrackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(93)))), ((int)(((byte)(90)))));
            this.tpsTB.TrackLineHeight = 3;
            this.tpsTB.Value = 0;
            this.tpsTB.ValueChanged += new Soko.Common.Controls.ValueChangedHandler(this.tpsTB_ValueChanged);
            // 
            // engineSpeedTB
            // 
            this.engineSpeedTB.BackColor = System.Drawing.Color.Transparent;
            this.engineSpeedTB.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.engineSpeedTB.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.engineSpeedTB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(125)))), ((int)(((byte)(123)))));
            this.engineSpeedTB.IndentHeight = 6;
            this.engineSpeedTB.LargeChange = 1000;
            this.engineSpeedTB.Location = new System.Drawing.Point(30, 481);
            this.engineSpeedTB.Maximum = 6000;
            this.engineSpeedTB.Minimum = 0;
            this.engineSpeedTB.Name = "engineSpeedTB";
            this.engineSpeedTB.Size = new System.Drawing.Size(652, 47);
            this.engineSpeedTB.SmallChange = 100;
            this.engineSpeedTB.TabIndex = 36;
            this.engineSpeedTB.TickColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(146)))), ((int)(((byte)(148)))));
            this.engineSpeedTB.TickFrequency = 1000;
            this.engineSpeedTB.TickHeight = 4;
            this.engineSpeedTB.TrackerColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(130)))), ((int)(((byte)(198)))));
            this.engineSpeedTB.TrackerSize = new System.Drawing.Size(16, 16);
            this.engineSpeedTB.TrackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(93)))), ((int)(((byte)(90)))));
            this.engineSpeedTB.TrackLineHeight = 3;
            this.engineSpeedTB.Value = 0;
            this.engineSpeedTB.ValueChanged += new Soko.Common.Controls.ValueChangedHandler(this.engineSpeedTB_ValueChanged);
            // 
            // TFPSW4NI
            // 
            this.TFPSW4NI.BackColorOnFocus = System.Drawing.Color.Transparent;
            this.TFPSW4NI.BorderColor = System.Drawing.Color.Black;
            this.TFPSW4NI.BorderWidth = 1;
            this.TFPSW4NI.DrawBackColorOnFocus = false;
            this.TFPSW4NI.DrawBorder = false;
            this.TFPSW4NI.DrawImageDisabled = false;
            this.TFPSW4NI.DrawImageOnFocus = false;
            this.TFPSW4NI.ImageOFF = ((System.Drawing.Image)(resources.GetObject("TFPSW4NI.ImageOFF")));
            this.TFPSW4NI.ImageON = ((System.Drawing.Image)(resources.GetObject("TFPSW4NI.ImageON")));
            this.TFPSW4NI.IsOn = false;
            this.TFPSW4NI.Location = new System.Drawing.Point(19, 268);
            this.TFPSW4NI.Name = "TFPSW4NI";
            this.TFPSW4NI.Size = new System.Drawing.Size(50, 64);
            this.TFPSW4NI.TabIndex = 33;
            this.TFPSW4NI.TextDisabled = "";
            this.TFPSW4NI.TextImageRelation = Soko.Common.Controls.NiceIndicator.TextRelation.OnRight;
            this.TFPSW4NI.TextImageSpacing = 0;
            this.TFPSW4NI.TextOFF = "";
            this.TFPSW4NI.TextON = "";
            // 
            // TFPSW3NI
            // 
            this.TFPSW3NI.BackColorOnFocus = System.Drawing.Color.Transparent;
            this.TFPSW3NI.BorderColor = System.Drawing.Color.Black;
            this.TFPSW3NI.BorderWidth = 1;
            this.TFPSW3NI.DrawBackColorOnFocus = false;
            this.TFPSW3NI.DrawBorder = false;
            this.TFPSW3NI.DrawImageDisabled = false;
            this.TFPSW3NI.DrawImageOnFocus = false;
            this.TFPSW3NI.ImageOFF = ((System.Drawing.Image)(resources.GetObject("TFPSW3NI.ImageOFF")));
            this.TFPSW3NI.ImageON = ((System.Drawing.Image)(resources.GetObject("TFPSW3NI.ImageON")));
            this.TFPSW3NI.IsOn = false;
            this.TFPSW3NI.Location = new System.Drawing.Point(19, 198);
            this.TFPSW3NI.Name = "TFPSW3NI";
            this.TFPSW3NI.Size = new System.Drawing.Size(50, 64);
            this.TFPSW3NI.TabIndex = 32;
            this.TFPSW3NI.TextDisabled = "";
            this.TFPSW3NI.TextImageRelation = Soko.Common.Controls.NiceIndicator.TextRelation.OnRight;
            this.TFPSW3NI.TextImageSpacing = 0;
            this.TFPSW3NI.TextOFF = "";
            this.TFPSW3NI.TextON = "";
            // 
            // TFPSW5NI
            // 
            this.TFPSW5NI.BackColorOnFocus = System.Drawing.Color.Transparent;
            this.TFPSW5NI.BorderColor = System.Drawing.Color.Black;
            this.TFPSW5NI.BorderWidth = 1;
            this.TFPSW5NI.DrawBackColorOnFocus = false;
            this.TFPSW5NI.DrawBorder = false;
            this.TFPSW5NI.DrawImageDisabled = false;
            this.TFPSW5NI.DrawImageOnFocus = false;
            this.TFPSW5NI.ImageOFF = ((System.Drawing.Image)(resources.GetObject("TFPSW5NI.ImageOFF")));
            this.TFPSW5NI.ImageON = ((System.Drawing.Image)(resources.GetObject("TFPSW5NI.ImageON")));
            this.TFPSW5NI.IsOn = false;
            this.TFPSW5NI.Location = new System.Drawing.Point(19, 338);
            this.TFPSW5NI.Name = "TFPSW5NI";
            this.TFPSW5NI.Size = new System.Drawing.Size(50, 64);
            this.TFPSW5NI.TabIndex = 31;
            this.TFPSW5NI.TextDisabled = "";
            this.TFPSW5NI.TextImageRelation = Soko.Common.Controls.NiceIndicator.TextRelation.OnRight;
            this.TFPSW5NI.TextImageSpacing = 0;
            this.TFPSW5NI.TextOFF = "";
            this.TFPSW5NI.TextON = "";
            // 
            // TFPSW1NI
            // 
            this.TFPSW1NI.BackColorOnFocus = System.Drawing.Color.Transparent;
            this.TFPSW1NI.BorderColor = System.Drawing.Color.Black;
            this.TFPSW1NI.BorderWidth = 1;
            this.TFPSW1NI.DrawBackColorOnFocus = false;
            this.TFPSW1NI.DrawBorder = false;
            this.TFPSW1NI.DrawImageDisabled = false;
            this.TFPSW1NI.DrawImageOnFocus = false;
            this.TFPSW1NI.ImageOFF = ((System.Drawing.Image)(resources.GetObject("TFPSW1NI.ImageOFF")));
            this.TFPSW1NI.ImageON = ((System.Drawing.Image)(resources.GetObject("TFPSW1NI.ImageON")));
            this.TFPSW1NI.IsOn = false;
            this.TFPSW1NI.Location = new System.Drawing.Point(19, 128);
            this.TFPSW1NI.Name = "TFPSW1NI";
            this.TFPSW1NI.Size = new System.Drawing.Size(50, 64);
            this.TFPSW1NI.TabIndex = 30;
            this.TFPSW1NI.TextDisabled = "";
            this.TFPSW1NI.TextImageRelation = Soko.Common.Controls.NiceIndicator.TextRelation.OnRight;
            this.TFPSW1NI.TextImageSpacing = 0;
            this.TFPSW1NI.TextOFF = "";
            this.TFPSW1NI.TextON = "";
            // 
            // TCCSolenoidLg
            // 
            this.TCCSolenoidLg.BackColor = System.Drawing.Color.Transparent;
            this.TCCSolenoidLg.Location = new System.Drawing.Point(613, 3);
            this.TCCSolenoidLg.MajorGrid = 5;
            this.TCCSolenoidLg.MaxValue = 600D;
            this.TCCSolenoidLg.MinorGrid = 2;
            this.TCCSolenoidLg.Name = "TCCSolenoidLg";
            this.TCCSolenoidLg.Size = new System.Drawing.Size(64, 266);
            this.TCCSolenoidLg.TabIndex = 29;
            this.TCCSolenoidLg.Text = "TCC";
            this.TCCSolenoidLg.Unit = "";
            // 
            // S5SolenoidLg
            // 
            this.S5SolenoidLg.BackColor = System.Drawing.Color.Transparent;
            this.S5SolenoidLg.Location = new System.Drawing.Point(732, 3);
            this.S5SolenoidLg.MajorGrid = 5;
            this.S5SolenoidLg.MaxValue = 600D;
            this.S5SolenoidLg.MinorGrid = 2;
            this.S5SolenoidLg.Name = "S5SolenoidLg";
            this.S5SolenoidLg.Size = new System.Drawing.Size(64, 266);
            this.S5SolenoidLg.TabIndex = 28;
            this.S5SolenoidLg.Text = "PCS5";
            this.S5SolenoidLg.Unit = "";
            // 
            // S3SolenoidLg
            // 
            this.S3SolenoidLg.BackColor = System.Drawing.Color.Transparent;
            this.S3SolenoidLg.Location = new System.Drawing.Point(495, 3);
            this.S3SolenoidLg.MajorGrid = 5;
            this.S3SolenoidLg.MaxValue = 600D;
            this.S3SolenoidLg.MinorGrid = 2;
            this.S3SolenoidLg.Name = "S3SolenoidLg";
            this.S3SolenoidLg.Size = new System.Drawing.Size(64, 266);
            this.S3SolenoidLg.TabIndex = 26;
            this.S3SolenoidLg.Text = "PCS3";
            this.S3SolenoidLg.Unit = "";
            // 
            // S2SolenoidLg
            // 
            this.S2SolenoidLg.BackColor = System.Drawing.Color.Transparent;
            this.S2SolenoidLg.Location = new System.Drawing.Point(436, 3);
            this.S2SolenoidLg.MajorGrid = 5;
            this.S2SolenoidLg.MaxValue = 600D;
            this.S2SolenoidLg.MinorGrid = 2;
            this.S2SolenoidLg.Name = "S2SolenoidLg";
            this.S2SolenoidLg.Size = new System.Drawing.Size(64, 266);
            this.S2SolenoidLg.TabIndex = 25;
            this.S2SolenoidLg.Text = "PCS2";
            this.S2SolenoidLg.Unit = "";
            // 
            // LineSolenoidLg
            // 
            this.LineSolenoidLg.BackColor = System.Drawing.Color.Transparent;
            this.LineSolenoidLg.Location = new System.Drawing.Point(554, 3);
            this.LineSolenoidLg.MajorGrid = 5;
            this.LineSolenoidLg.MaxValue = 2400D;
            this.LineSolenoidLg.MinorGrid = 2;
            this.LineSolenoidLg.Name = "LineSolenoidLg";
            this.LineSolenoidLg.Size = new System.Drawing.Size(64, 266);
            this.LineSolenoidLg.TabIndex = 24;
            this.LineSolenoidLg.Text = "EPC";
            this.LineSolenoidLg.Unit = "";
            // 
            // slipSpeedAG
            // 
            this.slipSpeedAG.BackColor = System.Drawing.Color.Transparent;
            this.slipSpeedAG.Location = new System.Drawing.Point(584, 274);
            this.slipSpeedAG.MaxValue = 3000D;
            this.slipSpeedAG.MinValue = -3000D;
            this.slipSpeedAG.Name = "slipSpeedAG";
            this.slipSpeedAG.Size = new System.Drawing.Size(200, 200);
            this.slipSpeedAG.TabIndex = 23;
            this.slipSpeedAG.Text = "Slip";
            this.slipSpeedAG.UnitName = "RPM";
            // 
            // outputSpeedAG
            // 
            this.outputSpeedAG.BackColor = System.Drawing.Color.Transparent;
            this.outputSpeedAG.Location = new System.Drawing.Point(382, 274);
            this.outputSpeedAG.MaxValue = 5000D;
            this.outputSpeedAG.Name = "outputSpeedAG";
            this.outputSpeedAG.Size = new System.Drawing.Size(200, 200);
            this.outputSpeedAG.TabIndex = 22;
            this.outputSpeedAG.Text = "Output";
            this.outputSpeedAG.UnitName = "RPM";
            // 
            // inputSpeedAG
            // 
            this.inputSpeedAG.BackColor = System.Drawing.Color.Transparent;
            this.inputSpeedAG.Location = new System.Drawing.Point(181, 274);
            this.inputSpeedAG.MaxValue = 5000D;
            this.inputSpeedAG.Name = "inputSpeedAG";
            this.inputSpeedAG.Size = new System.Drawing.Size(200, 200);
            this.inputSpeedAG.TabIndex = 21;
            this.inputSpeedAG.Text = "Input";
            this.inputSpeedAG.UnitName = "RPM";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.readVinBtn);
            this.panel2.Controls.Add(this.OSSniceTB);
            this.panel2.Controls.Add(this.ISSniceTB);
            this.panel2.Controls.Add(this.oss2ValueLabel);
            this.panel2.Controls.Add(this.oss1ValueLabel);
            this.panel2.Controls.Add(this.feedbackLabel);
            this.panel2.Controls.Add(this.tccOffBtn);
            this.panel2.Controls.Add(this.tccOnBtn);
            this.panel2.Controls.Add(this.resetAdaptsBtn);
            this.panel2.Controls.Add(this.resetTcmBtn);
            this.panel2.Controls.Add(this.clearCodesBtn);
            this.panel2.Controls.Add(this.getCodesBtn);
            this.panel2.Controls.Add(this.select6Btn);
            this.panel2.Controls.Add(this.select5Btn);
            this.panel2.Controls.Add(this.select4Btn);
            this.panel2.Controls.Add(this.select3Btn);
            this.panel2.Controls.Add(this.select2Btn);
            this.panel2.Controls.Add(this.select1Btn);
            this.panel2.Location = new System.Drawing.Point(1063, 21);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(311, 635);
            this.panel2.TabIndex = 21;
            // 
            // readVinBtn
            // 
            this.readVinBtn.Location = new System.Drawing.Point(121, 172);
            this.readVinBtn.Name = "readVinBtn";
            this.readVinBtn.Size = new System.Drawing.Size(133, 23);
            this.readVinBtn.TabIndex = 51;
            this.readVinBtn.Text = "Read VIN";
            this.readVinBtn.UseVisualStyleBackColor = true;
            this.readVinBtn.Click += new System.EventHandler(this.readVinBtn_Click);
            // 
            // OSSniceTB
            // 
            this.OSSniceTB.BackColor = System.Drawing.Color.Transparent;
            this.OSSniceTB.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.OSSniceTB.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OSSniceTB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(125)))), ((int)(((byte)(123)))));
            this.OSSniceTB.IndentHeight = 6;
            this.OSSniceTB.LargeChange = 500;
            this.OSSniceTB.Location = new System.Drawing.Point(151, 311);
            this.OSSniceTB.Maximum = 2500;
            this.OSSniceTB.Minimum = 0;
            this.OSSniceTB.Name = "OSSniceTB";
            this.OSSniceTB.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.OSSniceTB.Size = new System.Drawing.Size(68, 319);
            this.OSSniceTB.SmallChange = 50;
            this.OSSniceTB.TabIndex = 50;
            this.OSSniceTB.TickColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(146)))), ((int)(((byte)(148)))));
            this.OSSniceTB.TickFrequency = 500;
            this.OSSniceTB.TickHeight = 4;
            this.OSSniceTB.TrackerColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(130)))), ((int)(((byte)(198)))));
            this.OSSniceTB.TrackerSize = new System.Drawing.Size(16, 16);
            this.OSSniceTB.TrackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(93)))), ((int)(((byte)(90)))));
            this.OSSniceTB.TrackLineHeight = 3;
            this.OSSniceTB.Value = 0;
            this.OSSniceTB.ValueChanged += new Soko.Common.Controls.ValueChangedHandler(this.OSSniceTB_ValueChanged);
            // 
            // ISSniceTB
            // 
            this.ISSniceTB.BackColor = System.Drawing.Color.Transparent;
            this.ISSniceTB.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.ISSniceTB.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ISSniceTB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(125)))), ((int)(((byte)(123)))));
            this.ISSniceTB.IndentHeight = 6;
            this.ISSniceTB.LargeChange = 500;
            this.ISSniceTB.Location = new System.Drawing.Point(46, 311);
            this.ISSniceTB.Maximum = 2500;
            this.ISSniceTB.Minimum = 0;
            this.ISSniceTB.Name = "ISSniceTB";
            this.ISSniceTB.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.ISSniceTB.Size = new System.Drawing.Size(68, 319);
            this.ISSniceTB.SmallChange = 50;
            this.ISSniceTB.TabIndex = 49;
            this.ISSniceTB.TickColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(146)))), ((int)(((byte)(148)))));
            this.ISSniceTB.TickFrequency = 500;
            this.ISSniceTB.TickHeight = 4;
            this.ISSniceTB.TrackerColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(130)))), ((int)(((byte)(198)))));
            this.ISSniceTB.TrackerSize = new System.Drawing.Size(16, 16);
            this.ISSniceTB.TrackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(93)))), ((int)(((byte)(90)))));
            this.ISSniceTB.TrackLineHeight = 3;
            this.ISSniceTB.Value = 0;
            this.ISSniceTB.ValueChanged += new Soko.Common.Controls.ValueChangedHandler(this.ISSniceTB_ValueChanged);
            // 
            // oss2ValueLabel
            // 
            this.oss2ValueLabel.Location = new System.Drawing.Point(141, 289);
            this.oss2ValueLabel.Name = "oss2ValueLabel";
            this.oss2ValueLabel.Size = new System.Drawing.Size(88, 15);
            this.oss2ValueLabel.TabIndex = 48;
            this.oss2ValueLabel.Text = "OSS: 0 RPM";
            this.oss2ValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // oss1ValueLabel
            // 
            this.oss1ValueLabel.Location = new System.Drawing.Point(36, 289);
            this.oss1ValueLabel.Name = "oss1ValueLabel";
            this.oss1ValueLabel.Size = new System.Drawing.Size(88, 15);
            this.oss1ValueLabel.TabIndex = 47;
            this.oss1ValueLabel.Text = "ISS: 0 RPM";
            this.oss1ValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // feedbackLabel
            // 
            this.feedbackLabel.Location = new System.Drawing.Point(121, 27);
            this.feedbackLabel.Name = "feedbackLabel";
            this.feedbackLabel.Size = new System.Drawing.Size(133, 26);
            this.feedbackLabel.TabIndex = 46;
            this.feedbackLabel.Text = "Feedback";
            this.feedbackLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tccOffBtn
            // 
            this.tccOffBtn.Location = new System.Drawing.Point(16, 239);
            this.tccOffBtn.Name = "tccOffBtn";
            this.tccOffBtn.Size = new System.Drawing.Size(75, 23);
            this.tccOffBtn.TabIndex = 16;
            this.tccOffBtn.Text = "TCC OFF";
            this.tccOffBtn.UseVisualStyleBackColor = true;
            this.tccOffBtn.Click += new System.EventHandler(this.tccOffBtn_Click);
            // 
            // tccOnBtn
            // 
            this.tccOnBtn.Location = new System.Drawing.Point(16, 210);
            this.tccOnBtn.Name = "tccOnBtn";
            this.tccOnBtn.Size = new System.Drawing.Size(75, 23);
            this.tccOnBtn.TabIndex = 15;
            this.tccOnBtn.Text = "TCC On";
            this.tccOnBtn.UseVisualStyleBackColor = true;
            this.tccOnBtn.Click += new System.EventHandler(this.tccOnBtn_Click);
            // 
            // resetAdaptsBtn
            // 
            this.resetAdaptsBtn.Location = new System.Drawing.Point(121, 143);
            this.resetAdaptsBtn.Name = "resetAdaptsBtn";
            this.resetAdaptsBtn.Size = new System.Drawing.Size(133, 23);
            this.resetAdaptsBtn.TabIndex = 14;
            this.resetAdaptsBtn.Text = "Reset adapts";
            this.resetAdaptsBtn.UseVisualStyleBackColor = true;
            this.resetAdaptsBtn.Click += new System.EventHandler(this.resetAdaptsBtn_Click);
            // 
            // resetTcmBtn
            // 
            this.resetTcmBtn.Location = new System.Drawing.Point(121, 114);
            this.resetTcmBtn.Name = "resetTcmBtn";
            this.resetTcmBtn.Size = new System.Drawing.Size(133, 23);
            this.resetTcmBtn.TabIndex = 13;
            this.resetTcmBtn.Text = "Reset TCM";
            this.resetTcmBtn.UseVisualStyleBackColor = true;
            this.resetTcmBtn.Click += new System.EventHandler(this.resetTcmBtn_Click);
            // 
            // clearCodesBtn
            // 
            this.clearCodesBtn.Location = new System.Drawing.Point(121, 85);
            this.clearCodesBtn.Name = "clearCodesBtn";
            this.clearCodesBtn.Size = new System.Drawing.Size(133, 23);
            this.clearCodesBtn.TabIndex = 12;
            this.clearCodesBtn.Text = "Clear codes";
            this.clearCodesBtn.UseVisualStyleBackColor = true;
            this.clearCodesBtn.Click += new System.EventHandler(this.clearCodesBtn_Click);
            // 
            // getCodesBtn
            // 
            this.getCodesBtn.Location = new System.Drawing.Point(121, 56);
            this.getCodesBtn.Name = "getCodesBtn";
            this.getCodesBtn.Size = new System.Drawing.Size(133, 23);
            this.getCodesBtn.TabIndex = 11;
            this.getCodesBtn.Text = "Get codes";
            this.getCodesBtn.UseVisualStyleBackColor = true;
            this.getCodesBtn.Click += new System.EventHandler(this.getCodesBtn_Click);
            // 
            // select6Btn
            // 
            this.select6Btn.Location = new System.Drawing.Point(16, 172);
            this.select6Btn.Name = "select6Btn";
            this.select6Btn.Size = new System.Drawing.Size(75, 23);
            this.select6Btn.TabIndex = 5;
            this.select6Btn.Text = "Select 6";
            this.select6Btn.UseVisualStyleBackColor = true;
            this.select6Btn.Click += new System.EventHandler(this.select6Btn_Click);
            // 
            // select5Btn
            // 
            this.select5Btn.Location = new System.Drawing.Point(16, 143);
            this.select5Btn.Name = "select5Btn";
            this.select5Btn.Size = new System.Drawing.Size(75, 23);
            this.select5Btn.TabIndex = 4;
            this.select5Btn.Text = "Select 5";
            this.select5Btn.UseVisualStyleBackColor = true;
            this.select5Btn.Click += new System.EventHandler(this.select5Btn_Click);
            // 
            // select4Btn
            // 
            this.select4Btn.Location = new System.Drawing.Point(16, 114);
            this.select4Btn.Name = "select4Btn";
            this.select4Btn.Size = new System.Drawing.Size(75, 23);
            this.select4Btn.TabIndex = 3;
            this.select4Btn.Text = "Select 4";
            this.select4Btn.UseVisualStyleBackColor = true;
            this.select4Btn.Click += new System.EventHandler(this.select4Btn_Click);
            // 
            // select3Btn
            // 
            this.select3Btn.Location = new System.Drawing.Point(16, 85);
            this.select3Btn.Name = "select3Btn";
            this.select3Btn.Size = new System.Drawing.Size(75, 23);
            this.select3Btn.TabIndex = 2;
            this.select3Btn.Text = "Select 3";
            this.select3Btn.UseVisualStyleBackColor = true;
            this.select3Btn.Click += new System.EventHandler(this.select3Btn_Click);
            // 
            // select2Btn
            // 
            this.select2Btn.Location = new System.Drawing.Point(16, 56);
            this.select2Btn.Name = "select2Btn";
            this.select2Btn.Size = new System.Drawing.Size(75, 23);
            this.select2Btn.TabIndex = 1;
            this.select2Btn.Text = "Select 2";
            this.select2Btn.UseVisualStyleBackColor = true;
            this.select2Btn.Click += new System.EventHandler(this.select2Btn_Click);
            // 
            // select1Btn
            // 
            this.select1Btn.Location = new System.Drawing.Point(16, 27);
            this.select1Btn.Name = "select1Btn";
            this.select1Btn.Size = new System.Drawing.Size(75, 23);
            this.select1Btn.TabIndex = 0;
            this.select1Btn.Text = "Select 1";
            this.select1Btn.UseVisualStyleBackColor = true;
            this.select1Btn.Click += new System.EventHandler(this.select1Btn_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 13);
            this.label6.TabIndex = 52;
            this.label6.Text = "Gear lever position";
            // 
            // gearLeverPositionLabel
            // 
            this.gearLeverPositionLabel.AutoSize = true;
            this.gearLeverPositionLabel.Location = new System.Drawing.Point(9, 83);
            this.gearLeverPositionLabel.Name = "gearLeverPositionLabel";
            this.gearLeverPositionLabel.Size = new System.Drawing.Size(35, 13);
            this.gearLeverPositionLabel.TabIndex = 53;
            this.gearLeverPositionLabel.Text = "label7";
            // 
            // actualGearLabel
            // 
            this.actualGearLabel.AutoSize = true;
            this.actualGearLabel.Location = new System.Drawing.Point(9, 118);
            this.actualGearLabel.Name = "actualGearLabel";
            this.actualGearLabel.Size = new System.Drawing.Size(35, 13);
            this.actualGearLabel.TabIndex = 55;
            this.actualGearLabel.Text = "label8";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 105);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 13);
            this.label9.TabIndex = 54;
            this.label9.Text = "ActualGear";
            // 
            // desiredGearLabel
            // 
            this.desiredGearLabel.AutoSize = true;
            this.desiredGearLabel.Location = new System.Drawing.Point(9, 153);
            this.desiredGearLabel.Name = "desiredGearLabel";
            this.desiredGearLabel.Size = new System.Drawing.Size(41, 13);
            this.desiredGearLabel.TabIndex = 57;
            this.desiredGearLabel.Text = "label10";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 140);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 13);
            this.label11.TabIndex = 56;
            this.label11.Text = "DesiredGear";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 210);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 52;
            this.label7.Text = "DTCs:";
            // 
            // dtcLabel
            // 
            this.dtcLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dtcLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtcLabel.Location = new System.Drawing.Point(9, 223);
            this.dtcLabel.Name = "dtcLabel";
            this.dtcLabel.Size = new System.Drawing.Size(231, 715);
            this.dtcLabel.TabIndex = 58;
            this.dtcLabel.Text = "Not read";
            // 
            // guiUpdateSlowTimer
            // 
            this.guiUpdateSlowTimer.Enabled = true;
            this.guiUpdateSlowTimer.Interval = 500;
            this.guiUpdateSlowTimer.Tick += new System.EventHandler(this.guiUpdateSlowTimer_Tick);
            // 
            // vinLabelLabel
            // 
            this.vinLabelLabel.AutoSize = true;
            this.vinLabelLabel.Location = new System.Drawing.Point(9, 175);
            this.vinLabelLabel.Name = "vinLabelLabel";
            this.vinLabelLabel.Size = new System.Drawing.Size(57, 13);
            this.vinLabelLabel.TabIndex = 56;
            this.vinLabelLabel.Text = "Read VIN:";
            // 
            // vinLabel
            // 
            this.vinLabel.AutoSize = true;
            this.vinLabel.Location = new System.Drawing.Point(9, 188);
            this.vinLabel.Name = "vinLabel";
            this.vinLabel.Size = new System.Drawing.Size(41, 13);
            this.vinLabel.TabIndex = 57;
            this.vinLabel.Text = "label10";
            // 
            // usbStatusNiceInd
            // 
            this.usbStatusNiceInd.BackColorOnFocus = System.Drawing.Color.Transparent;
            this.usbStatusNiceInd.BorderColor = System.Drawing.Color.Black;
            this.usbStatusNiceInd.BorderWidth = 1;
            this.usbStatusNiceInd.DrawBackColorOnFocus = false;
            this.usbStatusNiceInd.DrawBorder = false;
            this.usbStatusNiceInd.DrawImageDisabled = false;
            this.usbStatusNiceInd.DrawImageOnFocus = false;
            this.usbStatusNiceInd.ImageOFF = global::Soko.CanCave.Components.Properties.Resources.UsbLogo_Red_40x20;
            this.usbStatusNiceInd.ImageON = global::Soko.CanCave.Components.Properties.Resources.UsbLogo_Green_40x20;
            this.usbStatusNiceInd.IsOn = false;
            this.usbStatusNiceInd.Location = new System.Drawing.Point(12, 21);
            this.usbStatusNiceInd.Name = "usbStatusNiceInd";
            this.usbStatusNiceInd.Size = new System.Drawing.Size(99, 39);
            this.usbStatusNiceInd.TabIndex = 59;
            this.usbStatusNiceInd.TextDisabled = "";
            this.usbStatusNiceInd.TextImageRelation = Soko.Common.Controls.NiceIndicator.TextRelation.Underneath;
            this.usbStatusNiceInd.TextImageSpacing = 5;
            this.usbStatusNiceInd.TextOFF = "USB Disconnected";
            this.usbStatusNiceInd.TextON = "USB Connected";
            // 
            // Gm6T40BarePanel
            // 
            this.Controls.Add(this.usbStatusNiceInd);
            this.Controls.Add(this.dtcLabel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.vinLabel);
            this.Controls.Add(this.desiredGearLabel);
            this.Controls.Add(this.vinLabelLabel);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.actualGearLabel);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.gearLeverPositionLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Gm6T40BarePanel";
            this.Size = new System.Drawing.Size(1565, 1066);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.Timer guiUpdateTimer;
    private System.Windows.Forms.Panel panel1;
    private Soko.Common.Controls.NiceIndicator TFPSW1NI;
    private Soko.Common.Controls.Gauges.LinearGauge TCCSolenoidLg;
    private Soko.Common.Controls.Gauges.LinearGauge S5SolenoidLg;
    private Soko.Common.Controls.Gauges.LinearGauge S4SolenoidLg;
    private Soko.Common.Controls.Gauges.LinearGauge S3SolenoidLg;
    private Soko.Common.Controls.Gauges.LinearGauge S2SolenoidLg;
    private Soko.Common.Controls.Gauges.LinearGauge LineSolenoidLg;
    private Soko.Common.Controls.AnalogueGauge slipSpeedAG;
    private Soko.Common.Controls.AnalogueGauge outputSpeedAG;
    private Soko.Common.Controls.AnalogueGauge inputSpeedAG;
    private Soko.Common.Controls.NiceIndicator TFPSW4NI;
    private Soko.Common.Controls.NiceIndicator TFPSW3NI;
    private Soko.Common.Controls.NiceIndicator TFPSW5NI;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Button tccOffBtn;
    private System.Windows.Forms.Button tccOnBtn;
    private System.Windows.Forms.Button resetAdaptsBtn;
    private System.Windows.Forms.Button resetTcmBtn;
    private System.Windows.Forms.Button clearCodesBtn;
    private System.Windows.Forms.Button getCodesBtn;
    private System.Windows.Forms.Button select2Btn;
    private System.Windows.Forms.Button select1Btn;
    private System.Windows.Forms.Label engineTorqueLabel;
    private System.Windows.Forms.Label tpsLabel;
    private System.Windows.Forms.Label engineSpeedLabel;
    private System.Windows.Forms.Label engineTorqueLabelLabel;
    private System.Windows.Forms.Label tpsLabelLabel;
    private System.Windows.Forms.Label engineSpeedLabelLabel;
    private Soko.Common.Controls.NiceTrackBar engineTorqueTB;
    private Soko.Common.Controls.NiceTrackBar tpsTB;
    private Soko.Common.Controls.NiceTrackBar engineSpeedTB;
    private System.Windows.Forms.Label oss2ValueLabel;
    private System.Windows.Forms.Label oss1ValueLabel;
    private System.Windows.Forms.Button select6Btn;
    private System.Windows.Forms.Button select5Btn;
    private System.Windows.Forms.Button select4Btn;
    private System.Windows.Forms.Button select3Btn;
    private Soko.Common.Controls.NiceTrackBar ISSniceTB;
    private Soko.Common.Controls.NiceTrackBar OSSniceTB;
    private System.Windows.Forms.Button readVinBtn;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label gearLeverPositionLabel;
    private System.Windows.Forms.Label actualGearLabel;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label desiredGearLabel;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label dtcLabel;
    private System.Windows.Forms.Timer guiUpdateSlowTimer;
    private System.Windows.Forms.Label vinLabelLabel;
    private System.Windows.Forms.Label vinLabel;
    private System.Windows.Forms.Label feedbackLabel;
    private Soko.Common.Controls.NiceIndicator usbStatusNiceInd;
    private Soko.Common.Controls.Gauges.LinearGauge ss1SolenoidLg;
    private Soko.Common.Controls.Gauges.LinearGauge ss2SolenoidLg;
    private Soko.Common.Controls.NiceComboBox tcmTypeComboBox;
    private Controls.SimpleTempGauge engineTempSTG;
    private Controls.SimpleTempGauge tcmTempSTG;
    private Controls.SimpleTempGauge fluidTempSTG;
    private System.Windows.Forms.Label label1;
  }
}