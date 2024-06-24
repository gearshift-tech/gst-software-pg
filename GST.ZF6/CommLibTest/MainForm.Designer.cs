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
            this.shortLabel = new System.Windows.Forms.Label();
            this.longLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gear_6_Button = new System.Windows.Forms.RadioButton();
            this.gear_3_Button = new System.Windows.Forms.RadioButton();
            this.gear_5_Button = new System.Windows.Forms.RadioButton();
            this.gear_2_Button = new System.Windows.Forms.RadioButton();
            this.gear_4_Button = new System.Windows.Forms.RadioButton();
            this.gear_1_Button = new System.Windows.Forms.RadioButton();
            this.gear_N_Button = new System.Windows.Forms.RadioButton();
            this.gear_R_Button = new System.Windows.Forms.RadioButton();
            this.ImpersonationDelayTmr = new System.Windows.Forms.Timer(this.components);
            this.timer4 = new System.Windows.Forms.Timer(this.components);
            this.deviceStatPackLabel = new System.Windows.Forms.Label();
            this.driveOnBtn = new System.Windows.Forms.Button();
            this.driveOffBtn = new System.Windows.Forms.Button();
            this.msgLenLabel = new System.Windows.Forms.Label();
            this.connStatLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.curr1Label = new System.Windows.Forms.Label();
            this.curr2Label = new System.Windows.Forms.Label();
            this.curr3Label = new System.Windows.Forms.Label();
            this.curr4Label = new System.Windows.Forms.Label();
            this.curr5Label = new System.Windows.Forms.Label();
            this.curr6Label = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.currGearLabel = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.transitionCompleteLabel = new System.Windows.Forms.Label();
            this.startEnigmaInitBtn = new System.Windows.Forms.Button();
            this.eds5TrackBar = new System.Windows.Forms.TrackBar();
            this.eds6TrackBar = new System.Windows.Forms.TrackBar();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.saveDumpBtn = new System.Windows.Forms.Button();
            this.bootloaderBtn = new System.Windows.Forms.Button();
            this.usbButton = new Soko.Common.Controls.NiceButton();
            this.GbxTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.enigmaInitPanel1 = new GST.ZF6.Components.Forms.Zf6InitPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eds5TrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eds6TrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(123, 409);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Connect USB";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(123, 438);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(111, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Disconnect USB";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // shortLabel
            // 
            this.shortLabel.AutoSize = true;
            this.shortLabel.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.shortLabel.Location = new System.Drawing.Point(39, 478);
            this.shortLabel.Name = "shortLabel";
            this.shortLabel.Size = new System.Drawing.Size(49, 14);
            this.shortLabel.TabIndex = 2;
            this.shortLabel.Text = "label1";
            // 
            // longLabel
            // 
            this.longLabel.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.longLabel.Location = new System.Drawing.Point(39, 494);
            this.longLabel.Name = "longLabel";
            this.longLabel.Size = new System.Drawing.Size(760, 13);
            this.longLabel.TabIndex = 2;
            this.longLabel.Text = "label1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 479);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "short";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 495);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "long";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 3000;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gear_6_Button);
            this.groupBox1.Controls.Add(this.gear_3_Button);
            this.groupBox1.Controls.Add(this.gear_5_Button);
            this.groupBox1.Controls.Add(this.gear_2_Button);
            this.groupBox1.Controls.Add(this.gear_4_Button);
            this.groupBox1.Controls.Add(this.gear_1_Button);
            this.groupBox1.Controls.Add(this.gear_N_Button);
            this.groupBox1.Controls.Add(this.gear_R_Button);
            this.groupBox1.Location = new System.Drawing.Point(565, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(88, 236);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "GEAR SELECTOR";
            // 
            // gear_6_Button
            // 
            this.gear_6_Button.AutoSize = true;
            this.gear_6_Button.Location = new System.Drawing.Point(21, 186);
            this.gear_6_Button.Name = "gear_6_Button";
            this.gear_6_Button.Size = new System.Drawing.Size(31, 17);
            this.gear_6_Button.TabIndex = 0;
            this.gear_6_Button.Text = "6";
            this.gear_6_Button.UseVisualStyleBackColor = true;
            this.gear_6_Button.CheckedChanged += new System.EventHandler(this.gear_6_Button_CheckedChanged);
            // 
            // gear_3_Button
            // 
            this.gear_3_Button.AutoSize = true;
            this.gear_3_Button.Location = new System.Drawing.Point(21, 117);
            this.gear_3_Button.Name = "gear_3_Button";
            this.gear_3_Button.Size = new System.Drawing.Size(31, 17);
            this.gear_3_Button.TabIndex = 0;
            this.gear_3_Button.Text = "3";
            this.gear_3_Button.UseVisualStyleBackColor = true;
            this.gear_3_Button.CheckedChanged += new System.EventHandler(this.gear_3_Button_CheckedChanged);
            // 
            // gear_5_Button
            // 
            this.gear_5_Button.AutoSize = true;
            this.gear_5_Button.Location = new System.Drawing.Point(21, 163);
            this.gear_5_Button.Name = "gear_5_Button";
            this.gear_5_Button.Size = new System.Drawing.Size(31, 17);
            this.gear_5_Button.TabIndex = 0;
            this.gear_5_Button.Text = "5";
            this.gear_5_Button.UseVisualStyleBackColor = true;
            this.gear_5_Button.CheckedChanged += new System.EventHandler(this.gear_5_Button_CheckedChanged);
            // 
            // gear_2_Button
            // 
            this.gear_2_Button.AutoSize = true;
            this.gear_2_Button.Location = new System.Drawing.Point(21, 94);
            this.gear_2_Button.Name = "gear_2_Button";
            this.gear_2_Button.Size = new System.Drawing.Size(31, 17);
            this.gear_2_Button.TabIndex = 0;
            this.gear_2_Button.Text = "2";
            this.gear_2_Button.UseVisualStyleBackColor = true;
            this.gear_2_Button.CheckedChanged += new System.EventHandler(this.gear_2_Button_CheckedChanged);
            // 
            // gear_4_Button
            // 
            this.gear_4_Button.AutoSize = true;
            this.gear_4_Button.Location = new System.Drawing.Point(21, 140);
            this.gear_4_Button.Name = "gear_4_Button";
            this.gear_4_Button.Size = new System.Drawing.Size(31, 17);
            this.gear_4_Button.TabIndex = 0;
            this.gear_4_Button.Text = "4";
            this.gear_4_Button.UseVisualStyleBackColor = true;
            this.gear_4_Button.CheckedChanged += new System.EventHandler(this.gear_4_Button_CheckedChanged);
            // 
            // gear_1_Button
            // 
            this.gear_1_Button.AutoSize = true;
            this.gear_1_Button.Location = new System.Drawing.Point(21, 71);
            this.gear_1_Button.Name = "gear_1_Button";
            this.gear_1_Button.Size = new System.Drawing.Size(31, 17);
            this.gear_1_Button.TabIndex = 0;
            this.gear_1_Button.Text = "1";
            this.gear_1_Button.UseVisualStyleBackColor = true;
            this.gear_1_Button.CheckedChanged += new System.EventHandler(this.gear_1_Button_CheckedChanged);
            // 
            // gear_N_Button
            // 
            this.gear_N_Button.AutoSize = true;
            this.gear_N_Button.Location = new System.Drawing.Point(21, 48);
            this.gear_N_Button.Name = "gear_N_Button";
            this.gear_N_Button.Size = new System.Drawing.Size(33, 17);
            this.gear_N_Button.TabIndex = 0;
            this.gear_N_Button.Text = "N";
            this.gear_N_Button.UseVisualStyleBackColor = true;
            this.gear_N_Button.CheckedChanged += new System.EventHandler(this.gear_N_Button_CheckedChanged);
            // 
            // gear_R_Button
            // 
            this.gear_R_Button.AutoSize = true;
            this.gear_R_Button.Checked = true;
            this.gear_R_Button.Location = new System.Drawing.Point(21, 25);
            this.gear_R_Button.Name = "gear_R_Button";
            this.gear_R_Button.Size = new System.Drawing.Size(33, 17);
            this.gear_R_Button.TabIndex = 0;
            this.gear_R_Button.TabStop = true;
            this.gear_R_Button.Text = "R";
            this.gear_R_Button.UseVisualStyleBackColor = true;
            this.gear_R_Button.CheckedChanged += new System.EventHandler(this.gear_R_Button_CheckedChanged);
            // 
            // deviceStatPackLabel
            // 
            this.deviceStatPackLabel.AutoSize = true;
            this.deviceStatPackLabel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.deviceStatPackLabel.Location = new System.Drawing.Point(62, 465);
            this.deviceStatPackLabel.Name = "deviceStatPackLabel";
            this.deviceStatPackLabel.Size = new System.Drawing.Size(136, 16);
            this.deviceStatPackLabel.TabIndex = 2;
            this.deviceStatPackLabel.Text = "device stat pack";
            // 
            // driveOnBtn
            // 
            this.driveOnBtn.Location = new System.Drawing.Point(240, 409);
            this.driveOnBtn.Name = "driveOnBtn";
            this.driveOnBtn.Size = new System.Drawing.Size(91, 23);
            this.driveOnBtn.TabIndex = 13;
            this.driveOnBtn.Text = "Drive ON";
            this.driveOnBtn.UseVisualStyleBackColor = true;
            this.driveOnBtn.Click += new System.EventHandler(this.driveOnBtn_Click);
            // 
            // driveOffBtn
            // 
            this.driveOffBtn.Location = new System.Drawing.Point(240, 438);
            this.driveOffBtn.Name = "driveOffBtn";
            this.driveOffBtn.Size = new System.Drawing.Size(91, 23);
            this.driveOffBtn.TabIndex = 13;
            this.driveOffBtn.Text = "Drive OFF";
            this.driveOffBtn.UseVisualStyleBackColor = true;
            this.driveOffBtn.Click += new System.EventHandler(this.driveOffBtn_Click);
            // 
            // msgLenLabel
            // 
            this.msgLenLabel.AutoSize = true;
            this.msgLenLabel.Location = new System.Drawing.Point(3, 466);
            this.msgLenLabel.Name = "msgLenLabel";
            this.msgLenLabel.Size = new System.Drawing.Size(35, 13);
            this.msgLenLabel.TabIndex = 14;
            this.msgLenLabel.Text = "label1";
            // 
            // connStatLabel
            // 
            this.connStatLabel.AutoSize = true;
            this.connStatLabel.Location = new System.Drawing.Point(237, 468);
            this.connStatLabel.Name = "connStatLabel";
            this.connStatLabel.Size = new System.Drawing.Size(34, 13);
            this.connStatLabel.TabIndex = 15;
            this.connStatLabel.Text = "curr 1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(606, 369);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "curr 2";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(647, 369);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "curr 3";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(688, 369);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "curr 4";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(729, 369);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "curr 5";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(770, 369);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "curr 6";
            // 
            // curr1Label
            // 
            this.curr1Label.AutoSize = true;
            this.curr1Label.Location = new System.Drawing.Point(565, 383);
            this.curr1Label.Name = "curr1Label";
            this.curr1Label.Size = new System.Drawing.Size(34, 13);
            this.curr1Label.TabIndex = 15;
            this.curr1Label.Text = "curr 1";
            // 
            // curr2Label
            // 
            this.curr2Label.AutoSize = true;
            this.curr2Label.Location = new System.Drawing.Point(606, 383);
            this.curr2Label.Name = "curr2Label";
            this.curr2Label.Size = new System.Drawing.Size(34, 13);
            this.curr2Label.TabIndex = 15;
            this.curr2Label.Text = "curr 2";
            // 
            // curr3Label
            // 
            this.curr3Label.AutoSize = true;
            this.curr3Label.Location = new System.Drawing.Point(647, 383);
            this.curr3Label.Name = "curr3Label";
            this.curr3Label.Size = new System.Drawing.Size(34, 13);
            this.curr3Label.TabIndex = 15;
            this.curr3Label.Text = "curr 3";
            // 
            // curr4Label
            // 
            this.curr4Label.AutoSize = true;
            this.curr4Label.Location = new System.Drawing.Point(688, 383);
            this.curr4Label.Name = "curr4Label";
            this.curr4Label.Size = new System.Drawing.Size(34, 13);
            this.curr4Label.TabIndex = 15;
            this.curr4Label.Text = "curr 4";
            // 
            // curr5Label
            // 
            this.curr5Label.AutoSize = true;
            this.curr5Label.Location = new System.Drawing.Point(729, 383);
            this.curr5Label.Name = "curr5Label";
            this.curr5Label.Size = new System.Drawing.Size(34, 13);
            this.curr5Label.TabIndex = 15;
            this.curr5Label.Text = "curr 5";
            // 
            // curr6Label
            // 
            this.curr6Label.AutoSize = true;
            this.curr6Label.Location = new System.Drawing.Point(770, 383);
            this.curr6Label.Name = "curr6Label";
            this.curr6Label.Size = new System.Drawing.Size(34, 13);
            this.curr6Label.TabIndex = 15;
            this.curr6Label.Text = "curr 6";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(565, 369);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 13);
            this.label13.TabIndex = 15;
            this.label13.Text = "curr 1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(565, 301);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Current gear:";
            // 
            // currGearLabel
            // 
            this.currGearLabel.AutoSize = true;
            this.currGearLabel.Location = new System.Drawing.Point(672, 301);
            this.currGearLabel.Name = "currGearLabel";
            this.currGearLabel.Size = new System.Drawing.Size(34, 13);
            this.currGearLabel.TabIndex = 15;
            this.currGearLabel.Text = "curr 1";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(565, 320);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 13);
            this.label11.TabIndex = 15;
            this.label11.Text = "Gear locked:";
            // 
            // transitionCompleteLabel
            // 
            this.transitionCompleteLabel.AutoSize = true;
            this.transitionCompleteLabel.Location = new System.Drawing.Point(672, 320);
            this.transitionCompleteLabel.Name = "transitionCompleteLabel";
            this.transitionCompleteLabel.Size = new System.Drawing.Size(34, 13);
            this.transitionCompleteLabel.TabIndex = 15;
            this.transitionCompleteLabel.Text = "curr 1";
            // 
            // startEnigmaInitBtn
            // 
            this.startEnigmaInitBtn.Location = new System.Drawing.Point(337, 409);
            this.startEnigmaInitBtn.Name = "startEnigmaInitBtn";
            this.startEnigmaInitBtn.Size = new System.Drawing.Size(75, 23);
            this.startEnigmaInitBtn.TabIndex = 17;
            this.startEnigmaInitBtn.Text = "<-- Start";
            this.startEnigmaInitBtn.UseVisualStyleBackColor = true;
            this.startEnigmaInitBtn.Click += new System.EventHandler(this.startEnigmaInitBtn_Click);
            // 
            // eds5TrackBar
            // 
            this.eds5TrackBar.LargeChange = 10;
            this.eds5TrackBar.Location = new System.Drawing.Point(681, 31);
            this.eds5TrackBar.Maximum = 100;
            this.eds5TrackBar.Name = "eds5TrackBar";
            this.eds5TrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.eds5TrackBar.Size = new System.Drawing.Size(45, 236);
            this.eds5TrackBar.TabIndex = 19;
            this.eds5TrackBar.TickFrequency = 10;
            this.eds5TrackBar.ValueChanged += new System.EventHandler(this.eds5TrackBar_ValueChanged);
            // 
            // eds6TrackBar
            // 
            this.eds6TrackBar.LargeChange = 10;
            this.eds6TrackBar.Location = new System.Drawing.Point(754, 31);
            this.eds6TrackBar.Maximum = 100;
            this.eds6TrackBar.Name = "eds6TrackBar";
            this.eds6TrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.eds6TrackBar.Size = new System.Drawing.Size(45, 236);
            this.eds6TrackBar.TabIndex = 20;
            this.eds6TrackBar.TickFrequency = 10;
            this.eds6TrackBar.ValueChanged += new System.EventHandler(this.eds6TrackBar_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(678, 14);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "EDS5";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(751, 14);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 13);
            this.label12.TabIndex = 22;
            this.label12.Text = "EDS6";
            // 
            // saveDumpBtn
            // 
            this.saveDumpBtn.Location = new System.Drawing.Point(6, 409);
            this.saveDumpBtn.Name = "saveDumpBtn";
            this.saveDumpBtn.Size = new System.Drawing.Size(111, 23);
            this.saveDumpBtn.TabIndex = 23;
            this.saveDumpBtn.Text = "Save dump data";
            this.saveDumpBtn.UseVisualStyleBackColor = true;
            this.saveDumpBtn.Click += new System.EventHandler(this.saveDumpBtn_Click);
            // 
            // bootloaderBtn
            // 
            this.bootloaderBtn.Location = new System.Drawing.Point(6, 438);
            this.bootloaderBtn.Name = "bootloaderBtn";
            this.bootloaderBtn.Size = new System.Drawing.Size(111, 23);
            this.bootloaderBtn.TabIndex = 24;
            this.bootloaderBtn.Text = " BOOTLOADER";
            this.bootloaderBtn.UseVisualStyleBackColor = true;
            this.bootloaderBtn.Click += new System.EventHandler(this.bootloaderBtn_Click);
            // 
            // usbButton
            // 
            this.usbButton.BackColorOnClicked1 = System.Drawing.Color.Orange;
            this.usbButton.BackColorOnClicked2 = System.Drawing.Color.Orange;
            this.usbButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(217)))), ((int)(((byte)(72)))));
            this.usbButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(238)))), ((int)(((byte)(176)))));
            this.usbButton.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.usbButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.usbButton.BorderColor = System.Drawing.Color.Transparent;
            this.usbButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(217)))), ((int)(((byte)(72)))));
            this.usbButton.BorderWidth = 1;
            this.usbButton.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.usbButton.ContentPadding = new System.Windows.Forms.Padding(0);
            this.usbButton.DrawBackColorOnFocus = false;
            this.usbButton.DrawBackgroundImage = false;
            this.usbButton.DrawBorderOnFocus = false;
            this.usbButton.DrawBorderOnTop = false;
            this.usbButton.Enabled = false;
            this.usbButton.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.usbButton.Image = ((System.Drawing.Image)(resources.GetObject("usbButton.Image")));
            this.usbButton.ImageDisabled = ((System.Drawing.Image)(resources.GetObject("usbButton.ImageDisabled")));
            this.usbButton.Location = new System.Drawing.Point(561, 409);
            this.usbButton.Name = "usbButton";
            this.usbButton.Size = new System.Drawing.Size(238, 59);
            this.usbButton.SupportTransparentBackground = false;
            this.usbButton.TabIndex = 10;
            this.usbButton.Text = "USB Disconnected";
            this.usbButton.TextImageRelation = Soko.Common.Controls.TextRelation.OnRight;
            this.usbButton.TextImageSpacing = 15;
            // 
            // GbxTypeComboBox
            // 
            this.GbxTypeComboBox.DropDownHeight = 150;
            this.GbxTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GbxTypeComboBox.FormattingEnabled = true;
            this.GbxTypeComboBox.IntegralHeight = false;
            this.GbxTypeComboBox.Location = new System.Drawing.Point(6, 7);
            this.GbxTypeComboBox.Name = "GbxTypeComboBox";
            this.GbxTypeComboBox.Size = new System.Drawing.Size(549, 21);
            this.GbxTypeComboBox.TabIndex = 25;
            this.GbxTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.GbxTypeComboBox_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(672, 281);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(51, 13);
            this.label14.TabIndex = 26;
            this.label14.Text = "ConnStat";
            // 
            // enigmaInitPanel1
            // 
            this.enigmaInitPanel1.BackColor = System.Drawing.Color.Transparent;
            this.enigmaInitPanel1.gearboxType = Soko.Common.Common.GearboxControllerType.NON_MECHATRONIC;
            this.enigmaInitPanel1.Location = new System.Drawing.Point(2, 30);
            this.enigmaInitPanel1.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.enigmaInitPanel1.Name = "enigmaInitPanel1";
            this.enigmaInitPanel1.Size = new System.Drawing.Size(557, 373);
            this.enigmaInitPanel1.TabIndex = 16;
            this.enigmaInitPanel1.ContinueButtonClicked += new System.EventHandler(this.enigmaInitPanel1_ContinueButtonClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(565, 281);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "TCU connection:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 517);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.driveOnBtn);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.driveOffBtn);
            this.Controls.Add(this.GbxTypeComboBox);
            this.Controls.Add(this.bootloaderBtn);
            this.Controls.Add(this.saveDumpBtn);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.eds6TrackBar);
            this.Controls.Add(this.eds5TrackBar);
            this.Controls.Add(this.startEnigmaInitBtn);
            this.Controls.Add(this.enigmaInitPanel1);
            this.Controls.Add(this.curr6Label);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.curr5Label);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.curr4Label);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.curr3Label);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.curr2Label);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.currGearLabel);
            this.Controls.Add(this.transitionCompleteLabel);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.curr1Label);
            this.Controls.Add(this.connStatLabel);
            this.Controls.Add(this.msgLenLabel);
            this.Controls.Add(this.usbButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.longLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.deviceStatPackLabel);
            this.Controls.Add(this.shortLabel);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label13);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GearShift Technologies ltd.      DECODER BARE";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eds5TrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eds6TrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Label shortLabel;
    private System.Windows.Forms.Label longLabel;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.Timer timer2;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.RadioButton gear_6_Button;
    private System.Windows.Forms.RadioButton gear_3_Button;
    private System.Windows.Forms.RadioButton gear_5_Button;
    private System.Windows.Forms.RadioButton gear_2_Button;
    private System.Windows.Forms.RadioButton gear_4_Button;
    private System.Windows.Forms.RadioButton gear_1_Button;
    private System.Windows.Forms.RadioButton gear_N_Button;
    private System.Windows.Forms.RadioButton gear_R_Button;
    private System.Windows.Forms.Timer ImpersonationDelayTmr;
    private System.Windows.Forms.Timer timer4;
    private System.Windows.Forms.Label deviceStatPackLabel;
    private Soko.Common.Controls.NiceButton usbButton;
    private System.Windows.Forms.Button driveOnBtn;
    private System.Windows.Forms.Button driveOffBtn;
    private System.Windows.Forms.Label msgLenLabel;
    private System.Windows.Forms.Label connStatLabel;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label curr1Label;
    private System.Windows.Forms.Label curr2Label;
    private System.Windows.Forms.Label curr3Label;
    private System.Windows.Forms.Label curr4Label;
    private System.Windows.Forms.Label curr5Label;
    private System.Windows.Forms.Label curr6Label;
    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label currGearLabel;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Label transitionCompleteLabel;
    private GST.ZF6.Components.Forms.Zf6InitPanel enigmaInitPanel1;
    private System.Windows.Forms.Button startEnigmaInitBtn;
    private System.Windows.Forms.TrackBar eds5TrackBar;
    private System.Windows.Forms.TrackBar eds6TrackBar;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.Button saveDumpBtn;
    private System.Windows.Forms.Button bootloaderBtn;
    private System.Windows.Forms.ComboBox GbxTypeComboBox;
    private System.Windows.Forms.Label label14;
    private System.Windows.Forms.Label label2;
  }
}