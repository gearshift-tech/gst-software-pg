namespace CommLibTest
{
  partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.guiUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.engineTorqueLabel = new System.Windows.Forms.Label();
            this.tpsLabel = new System.Windows.Forms.Label();
            this.engineSpeedLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.engineTorqueTB = new Soko.Common.Controls.NiceTrackBar();
            this.tpsTB = new Soko.Common.Controls.NiceTrackBar();
            this.engineSpeedTB = new Soko.Common.Controls.NiceTrackBar();
            this.shiftS2NI = new Soko.Common.Controls.NiceIndicator();
            this.shiftS1NI = new Soko.Common.Controls.NiceIndicator();
            this.TFPSW4NI = new Soko.Common.Controls.NiceIndicator();
            this.TFPSW3NI = new Soko.Common.Controls.NiceIndicator();
            this.TFPSW5NI = new Soko.Common.Controls.NiceIndicator();
            this.TFPSW1NI = new Soko.Common.Controls.NiceIndicator();
            this.TCCSolenoidLg = new Soko.Common.Controls.Gauges.LinearGauge();
            this.S5SolenoidLg = new Soko.Common.Controls.Gauges.LinearGauge();
            this.S4SolenoidLg = new Soko.Common.Controls.Gauges.LinearGauge();
            this.S3SolenoidLg = new Soko.Common.Controls.Gauges.LinearGauge();
            this.S2SolenoidLg = new Soko.Common.Controls.Gauges.LinearGauge();
            this.LineSolenoidLg = new Soko.Common.Controls.Gauges.LinearGauge();
            this.slipSpeedAG = new Soko.Common.Controls.AnalogueGauge();
            this.outputSpeedAG = new Soko.Common.Controls.AnalogueGauge();
            this.inputSpeedAG = new Soko.Common.Controls.AnalogueGauge();
            this.tcmTempTg = new Soko.Common.Controls.Gauges.ThermometerGauge();
            this.engineTempTg = new Soko.Common.Controls.Gauges.ThermometerGauge();
            this.transFluidTempTg = new Soko.Common.Controls.Gauges.ThermometerGauge();
            this.panel2 = new System.Windows.Forms.Panel();
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
            this.usbButton = new Soko.Common.Controls.NiceButton();
            this.startBtn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(2, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Connect USB";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(4, 51);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(111, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Disconnect USB";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // guiUpdateTimer
            // 
            this.guiUpdateTimer.Interval = 50;
            this.guiUpdateTimer.Tick += new System.EventHandler(this.guiUpdateTimer_Tick);
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.engineTorqueLabel);
            this.panel1.Controls.Add(this.tpsLabel);
            this.panel1.Controls.Add(this.engineSpeedLabel);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.engineTorqueTB);
            this.panel1.Controls.Add(this.tpsTB);
            this.panel1.Controls.Add(this.engineSpeedTB);
            this.panel1.Controls.Add(this.shiftS2NI);
            this.panel1.Controls.Add(this.shiftS1NI);
            this.panel1.Controls.Add(this.TFPSW4NI);
            this.panel1.Controls.Add(this.TFPSW3NI);
            this.panel1.Controls.Add(this.TFPSW5NI);
            this.panel1.Controls.Add(this.TFPSW1NI);
            this.panel1.Controls.Add(this.TCCSolenoidLg);
            this.panel1.Controls.Add(this.S5SolenoidLg);
            this.panel1.Controls.Add(this.S4SolenoidLg);
            this.panel1.Controls.Add(this.S3SolenoidLg);
            this.panel1.Controls.Add(this.S2SolenoidLg);
            this.panel1.Controls.Add(this.LineSolenoidLg);
            this.panel1.Controls.Add(this.slipSpeedAG);
            this.panel1.Controls.Add(this.outputSpeedAG);
            this.panel1.Controls.Add(this.inputSpeedAG);
            this.panel1.Controls.Add(this.tcmTempTg);
            this.panel1.Controls.Add(this.engineTempTg);
            this.panel1.Controls.Add(this.transFluidTempTg);
            this.panel1.Location = new System.Drawing.Point(121, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(802, 639);
            this.panel1.TabIndex = 20;
            // 
            // engineTorqueLabel
            // 
            this.engineTorqueLabel.AutoSize = true;
            this.engineTorqueLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.engineTorqueLabel.Location = new System.Drawing.Point(687, 608);
            this.engineTorqueLabel.Name = "engineTorqueLabel";
            this.engineTorqueLabel.Size = new System.Drawing.Size(54, 21);
            this.engineTorqueLabel.TabIndex = 44;
            this.engineTorqueLabel.Text = "label6";
            // 
            // tpsLabel
            // 
            this.tpsLabel.AutoSize = true;
            this.tpsLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpsLabel.Location = new System.Drawing.Point(687, 556);
            this.tpsLabel.Name = "tpsLabel";
            this.tpsLabel.Size = new System.Drawing.Size(54, 21);
            this.tpsLabel.TabIndex = 43;
            this.tpsLabel.Text = "label5";
            // 
            // engineSpeedLabel
            // 
            this.engineSpeedLabel.AutoSize = true;
            this.engineSpeedLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.engineSpeedLabel.Location = new System.Drawing.Point(687, 502);
            this.engineSpeedLabel.Name = "engineSpeedLabel";
            this.engineSpeedLabel.Size = new System.Drawing.Size(54, 21);
            this.engineSpeedLabel.TabIndex = 42;
            this.engineSpeedLabel.Text = "label4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(687, 587);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 41;
            this.label3.Text = "Engine Torque";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(685, 533);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 40;
            this.label2.Text = "TPS";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(687, 481);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 39;
            this.label1.Text = "Engine speed";
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
            // shiftS2NI
            // 
            this.shiftS2NI.BackColorOnFocus = System.Drawing.Color.Transparent;
            this.shiftS2NI.BorderColor = System.Drawing.Color.Black;
            this.shiftS2NI.BorderWidth = 1;
            this.shiftS2NI.DrawBackColorOnFocus = false;
            this.shiftS2NI.DrawBorder = false;
            this.shiftS2NI.DrawImageDisabled = false;
            this.shiftS2NI.DrawImageOnFocus = false;
            this.shiftS2NI.ImageOFF = ((System.Drawing.Image)(resources.GetObject("shiftS2NI.ImageOFF")));
            this.shiftS2NI.ImageON = ((System.Drawing.Image)(resources.GetObject("shiftS2NI.ImageON")));
            this.shiftS2NI.IsOn = false;
            this.shiftS2NI.Location = new System.Drawing.Point(17, 91);
            this.shiftS2NI.Name = "shiftS2NI";
            this.shiftS2NI.Size = new System.Drawing.Size(42, 27);
            this.shiftS2NI.TabIndex = 35;
            this.shiftS2NI.TextDisabled = "";
            this.shiftS2NI.TextImageRelation = Soko.Common.Controls.NiceIndicator.TextRelation.OnRight;
            this.shiftS2NI.TextImageSpacing = 5;
            this.shiftS2NI.TextOFF = "Shift S2";
            this.shiftS2NI.TextON = "Shift S2";
            // 
            // shiftS1NI
            // 
            this.shiftS1NI.BackColorOnFocus = System.Drawing.Color.Transparent;
            this.shiftS1NI.BorderColor = System.Drawing.Color.Black;
            this.shiftS1NI.BorderWidth = 1;
            this.shiftS1NI.DrawBackColorOnFocus = false;
            this.shiftS1NI.DrawBorder = false;
            this.shiftS1NI.DrawImageDisabled = false;
            this.shiftS1NI.DrawImageOnFocus = false;
            this.shiftS1NI.ImageOFF = ((System.Drawing.Image)(resources.GetObject("shiftS1NI.ImageOFF")));
            this.shiftS1NI.ImageON = ((System.Drawing.Image)(resources.GetObject("shiftS1NI.ImageON")));
            this.shiftS1NI.IsOn = false;
            this.shiftS1NI.Location = new System.Drawing.Point(17, 27);
            this.shiftS1NI.Name = "shiftS1NI";
            this.shiftS1NI.Size = new System.Drawing.Size(42, 27);
            this.shiftS1NI.TabIndex = 34;
            this.shiftS1NI.TextDisabled = "";
            this.shiftS1NI.TextImageRelation = Soko.Common.Controls.NiceIndicator.TextRelation.OnRight;
            this.shiftS1NI.TextImageSpacing = 5;
            this.shiftS1NI.TextOFF = "Shift S1";
            this.shiftS1NI.TextON = "Shift S1";
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
            this.TFPSW4NI.Location = new System.Drawing.Point(17, 283);
            this.TFPSW4NI.Name = "TFPSW4NI";
            this.TFPSW4NI.Size = new System.Drawing.Size(46, 27);
            this.TFPSW4NI.TabIndex = 33;
            this.TFPSW4NI.TextDisabled = "";
            this.TFPSW4NI.TextImageRelation = Soko.Common.Controls.NiceIndicator.TextRelation.OnRight;
            this.TFPSW4NI.TextImageSpacing = 5;
            this.TFPSW4NI.TextOFF = "TFP SW4";
            this.TFPSW4NI.TextON = "TFP SW4";
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
            this.TFPSW3NI.Location = new System.Drawing.Point(17, 219);
            this.TFPSW3NI.Name = "TFPSW3NI";
            this.TFPSW3NI.Size = new System.Drawing.Size(46, 27);
            this.TFPSW3NI.TabIndex = 32;
            this.TFPSW3NI.TextDisabled = "";
            this.TFPSW3NI.TextImageRelation = Soko.Common.Controls.NiceIndicator.TextRelation.OnRight;
            this.TFPSW3NI.TextImageSpacing = 5;
            this.TFPSW3NI.TextOFF = "TFP SW3";
            this.TFPSW3NI.TextON = "TFP SW3";
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
            this.TFPSW5NI.Location = new System.Drawing.Point(17, 347);
            this.TFPSW5NI.Name = "TFPSW5NI";
            this.TFPSW5NI.Size = new System.Drawing.Size(46, 27);
            this.TFPSW5NI.TabIndex = 31;
            this.TFPSW5NI.TextDisabled = "";
            this.TFPSW5NI.TextImageRelation = Soko.Common.Controls.NiceIndicator.TextRelation.OnRight;
            this.TFPSW5NI.TextImageSpacing = 5;
            this.TFPSW5NI.TextOFF = "TFP SW5";
            this.TFPSW5NI.TextON = "TFP SW5";
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
            this.TFPSW1NI.Location = new System.Drawing.Point(17, 155);
            this.TFPSW1NI.Name = "TFPSW1NI";
            this.TFPSW1NI.Size = new System.Drawing.Size(46, 27);
            this.TFPSW1NI.TabIndex = 30;
            this.TFPSW1NI.TextDisabled = "";
            this.TFPSW1NI.TextImageRelation = Soko.Common.Controls.NiceIndicator.TextRelation.OnRight;
            this.TFPSW1NI.TextImageSpacing = 5;
            this.TFPSW1NI.TextOFF = "TFP SW1";
            this.TFPSW1NI.TextON = "TFP SW1";
            // 
            // TCCSolenoidLg
            // 
            this.TCCSolenoidLg.BackColor = System.Drawing.Color.Transparent;
            this.TCCSolenoidLg.Location = new System.Drawing.Point(733, 3);
            this.TCCSolenoidLg.MajorGrid = 5;
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
            this.S5SolenoidLg.Location = new System.Drawing.Point(675, 3);
            this.S5SolenoidLg.MajorGrid = 5;
            this.S5SolenoidLg.MinorGrid = 2;
            this.S5SolenoidLg.Name = "S5SolenoidLg";
            this.S5SolenoidLg.Size = new System.Drawing.Size(64, 266);
            this.S5SolenoidLg.TabIndex = 28;
            this.S5SolenoidLg.Text = "S5";
            this.S5SolenoidLg.Unit = "";
            // 
            // S4SolenoidLg
            // 
            this.S4SolenoidLg.BackColor = System.Drawing.Color.Transparent;
            this.S4SolenoidLg.Location = new System.Drawing.Point(618, 3);
            this.S4SolenoidLg.MajorGrid = 5;
            this.S4SolenoidLg.MinorGrid = 2;
            this.S4SolenoidLg.Name = "S4SolenoidLg";
            this.S4SolenoidLg.Size = new System.Drawing.Size(64, 266);
            this.S4SolenoidLg.TabIndex = 27;
            this.S4SolenoidLg.Text = "S4";
            this.S4SolenoidLg.Unit = "";
            // 
            // S3SolenoidLg
            // 
            this.S3SolenoidLg.BackColor = System.Drawing.Color.Transparent;
            this.S3SolenoidLg.Location = new System.Drawing.Point(560, 3);
            this.S3SolenoidLg.MajorGrid = 5;
            this.S3SolenoidLg.MinorGrid = 2;
            this.S3SolenoidLg.Name = "S3SolenoidLg";
            this.S3SolenoidLg.Size = new System.Drawing.Size(64, 266);
            this.S3SolenoidLg.TabIndex = 26;
            this.S3SolenoidLg.Text = "S3";
            this.S3SolenoidLg.Unit = "";
            // 
            // S2SolenoidLg
            // 
            this.S2SolenoidLg.BackColor = System.Drawing.Color.Transparent;
            this.S2SolenoidLg.Location = new System.Drawing.Point(503, 3);
            this.S2SolenoidLg.MajorGrid = 5;
            this.S2SolenoidLg.MinorGrid = 2;
            this.S2SolenoidLg.Name = "S2SolenoidLg";
            this.S2SolenoidLg.Size = new System.Drawing.Size(64, 266);
            this.S2SolenoidLg.TabIndex = 25;
            this.S2SolenoidLg.Text = "S2";
            this.S2SolenoidLg.Unit = "";
            // 
            // LineSolenoidLg
            // 
            this.LineSolenoidLg.BackColor = System.Drawing.Color.Transparent;
            this.LineSolenoidLg.Location = new System.Drawing.Point(446, 3);
            this.LineSolenoidLg.MajorGrid = 5;
            this.LineSolenoidLg.MinorGrid = 2;
            this.LineSolenoidLg.Name = "LineSolenoidLg";
            this.LineSolenoidLg.Size = new System.Drawing.Size(64, 266);
            this.LineSolenoidLg.TabIndex = 24;
            this.LineSolenoidLg.Text = "Line";
            this.LineSolenoidLg.Unit = "";
            // 
            // slipSpeedAG
            // 
            this.slipSpeedAG.BackColor = System.Drawing.Color.Transparent;
            this.slipSpeedAG.Location = new System.Drawing.Point(584, 274);
            this.slipSpeedAG.MaxValue = 5000D;
            this.slipSpeedAG.Name = "slipSpeedAG";
            this.slipSpeedAG.Size = new System.Drawing.Size(179, 179);
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
            this.outputSpeedAG.Size = new System.Drawing.Size(179, 179);
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
            this.inputSpeedAG.Size = new System.Drawing.Size(179, 179);
            this.inputSpeedAG.TabIndex = 21;
            this.inputSpeedAG.Text = "Input";
            this.inputSpeedAG.UnitName = "RPM";
            // 
            // tcmTempTg
            // 
            this.tcmTempTg.BackColor = System.Drawing.Color.Transparent;
            this.tcmTempTg.Location = new System.Drawing.Point(245, 9);
            this.tcmTempTg.MajorGrid = 5;
            this.tcmTempTg.MaxValue = 200D;
            this.tcmTempTg.MinorGrid = 2;
            this.tcmTempTg.Name = "tcmTempTg";
            this.tcmTempTg.Size = new System.Drawing.Size(93, 260);
            this.tcmTempTg.TabIndex = 19;
            this.tcmTempTg.Text = "TCM";
            this.tcmTempTg.Unit = "";
            // 
            // engineTempTg
            // 
            this.engineTempTg.BackColor = System.Drawing.Color.Transparent;
            this.engineTempTg.Location = new System.Drawing.Point(158, 9);
            this.engineTempTg.MajorGrid = 5;
            this.engineTempTg.MaxValue = 200D;
            this.engineTempTg.MinorGrid = 2;
            this.engineTempTg.Name = "engineTempTg";
            this.engineTempTg.Size = new System.Drawing.Size(93, 260);
            this.engineTempTg.TabIndex = 17;
            this.engineTempTg.Text = "Engine";
            this.engineTempTg.Unit = "";
            // 
            // transFluidTempTg
            // 
            this.transFluidTempTg.BackColor = System.Drawing.Color.Transparent;
            this.transFluidTempTg.Location = new System.Drawing.Point(333, 9);
            this.transFluidTempTg.MajorGrid = 5;
            this.transFluidTempTg.MaxValue = 200D;
            this.transFluidTempTg.MinorGrid = 2;
            this.transFluidTempTg.Name = "transFluidTempTg";
            this.transFluidTempTg.Size = new System.Drawing.Size(93, 260);
            this.transFluidTempTg.TabIndex = 18;
            this.transFluidTempTg.Text = "T. fluid";
            this.transFluidTempTg.Unit = "";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            this.panel2.Location = new System.Drawing.Point(925, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(275, 639);
            this.panel2.TabIndex = 21;
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
            this.resetAdaptsBtn.Location = new System.Drawing.Point(121, 114);
            this.resetAdaptsBtn.Name = "resetAdaptsBtn";
            this.resetAdaptsBtn.Size = new System.Drawing.Size(133, 23);
            this.resetAdaptsBtn.TabIndex = 14;
            this.resetAdaptsBtn.Text = "Reset adapts";
            this.resetAdaptsBtn.UseVisualStyleBackColor = true;
            this.resetAdaptsBtn.Click += new System.EventHandler(this.resetAdaptsBtn_Click);
            // 
            // resetTcmBtn
            // 
            this.resetTcmBtn.Location = new System.Drawing.Point(121, 85);
            this.resetTcmBtn.Name = "resetTcmBtn";
            this.resetTcmBtn.Size = new System.Drawing.Size(133, 23);
            this.resetTcmBtn.TabIndex = 13;
            this.resetTcmBtn.Text = "Reset TCM";
            this.resetTcmBtn.UseVisualStyleBackColor = true;
            this.resetTcmBtn.Click += new System.EventHandler(this.resetTcmBtn_Click);
            // 
            // clearCodesBtn
            // 
            this.clearCodesBtn.Location = new System.Drawing.Point(121, 56);
            this.clearCodesBtn.Name = "clearCodesBtn";
            this.clearCodesBtn.Size = new System.Drawing.Size(133, 23);
            this.clearCodesBtn.TabIndex = 12;
            this.clearCodesBtn.Text = "Clear codes";
            this.clearCodesBtn.UseVisualStyleBackColor = true;
            this.clearCodesBtn.Click += new System.EventHandler(this.clearCodesBtn_Click);
            // 
            // getCodesBtn
            // 
            this.getCodesBtn.Location = new System.Drawing.Point(121, 27);
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
            // usbButton
            // 
            this.usbButton.BackColor = System.Drawing.SystemColors.Control;
            this.usbButton.BackColorOnClicked1 = System.Drawing.Color.Orange;
            this.usbButton.BackColorOnClicked2 = System.Drawing.Color.Orange;
            this.usbButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(217)))), ((int)(((byte)(72)))));
            this.usbButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(238)))), ((int)(((byte)(176)))));
            this.usbButton.BackgroundColor = System.Drawing.SystemColors.Control;
            this.usbButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.usbButton.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.usbButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(217)))), ((int)(((byte)(72)))));
            this.usbButton.BorderWidth = 1;
            this.usbButton.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.usbButton.ContentPadding = new System.Windows.Forms.Padding(0);
            this.usbButton.DrawBackColorOnFocus = false;
            this.usbButton.DrawBackgroundImage = false;
            this.usbButton.DrawBorderOnFocus = false;
            this.usbButton.DrawBorderOnTop = false;
            this.usbButton.Enabled = false;
            this.usbButton.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.usbButton.Image = ((System.Drawing.Image)(resources.GetObject("usbButton.Image")));
            this.usbButton.ImageDisabled = ((System.Drawing.Image)(resources.GetObject("usbButton.ImageDisabled")));
            this.usbButton.Location = new System.Drawing.Point(-1, 80);
            this.usbButton.Name = "usbButton";
            this.usbButton.Size = new System.Drawing.Size(111, 99);
            this.usbButton.SupportTransparentBackground = false;
            this.usbButton.TabIndex = 10;
            this.usbButton.Text = "USB Disconnected";
            this.usbButton.TextImageRelation = Soko.Common.Controls.TextRelation.Underneath;
            this.usbButton.TextImageSpacing = 15;
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(12, 197);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(75, 23);
            this.startBtn.TabIndex = 51;
            this.startBtn.Text = "Start";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1026, 725);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.usbButton);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GearShift Technologies  CAN Communicator    Nissan RE5 bare";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Timer guiUpdateTimer;
    private Soko.Common.Controls.NiceButton usbButton;
    //private Bluereach.CANPRO.Components.Forms.EnigmaInitPanel enigmaInitPanel1;
    private Soko.Common.Controls.Gauges.ThermometerGauge engineTempTg;
    private Soko.Common.Controls.Gauges.ThermometerGauge transFluidTempTg;
    private Soko.Common.Controls.Gauges.ThermometerGauge tcmTempTg;
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
    private Soko.Common.Controls.NiceIndicator shiftS2NI;
    private Soko.Common.Controls.NiceIndicator shiftS1NI;
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
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private Soko.Common.Controls.NiceTrackBar engineTorqueTB;
    private Soko.Common.Controls.NiceTrackBar tpsTB;
    private Soko.Common.Controls.NiceTrackBar engineSpeedTB;
    private System.Windows.Forms.Button select6Btn;
    private System.Windows.Forms.Button select5Btn;
    private System.Windows.Forms.Button select4Btn;
    private System.Windows.Forms.Button select3Btn;
    private System.Windows.Forms.Button startBtn;
  }
}