namespace GST.Gearshift.Components.Forms.DAQ
{
  partial class PressureDisplayConfigPanel
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
            this.gearboxGaugePanel2 = new Soko.Common.Controls.NicePanel();
            this.maxValNUD = new System.Windows.Forms.NumericUpDown();
            this.minValNUD = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pressureSwitchRB = new System.Windows.Forms.RadioButton();
            this.gearRatioRB = new System.Windows.Forms.RadioButton();
            this.outputSpeedRB = new System.Windows.Forms.RadioButton();
            this.inputSpeedRB = new System.Windows.Forms.RadioButton();
            this.torqueRB = new System.Windows.Forms.RadioButton();
            this.flowRB = new System.Windows.Forms.RadioButton();
            this.tempRB = new System.Windows.Forms.RadioButton();
            this.pressRB = new System.Windows.Forms.RadioButton();
            this.inputChannelLabel = new System.Windows.Forms.Label();
            this.labelTextBox = new System.Windows.Forms.TextBox();
            this.unitnameLabel = new System.Windows.Forms.Label();
            this.inputChannelComboBox = new System.Windows.Forms.ComboBox();
            this.unitNameTextBox = new System.Windows.Forms.TextBox();
            this.maxvalLabel = new System.Windows.Forms.Label();
            this.minvalLabel = new System.Windows.Forms.Label();
            this.labelLabel = new System.Windows.Forms.Label();
            this.gearboxGaugePanel1 = new Soko.Common.Controls.NicePanel();
            this.channelsListBox = new System.Windows.Forms.ListBox();
            this.gearboxGaugeAddButton = new Soko.Common.Controls.NiceButton();
            this.gearboxGaugeDeleteButton = new Soko.Common.Controls.NiceButton();
            this.gaugeLabel = new System.Windows.Forms.Label();
            this.gearboxGaugePanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxValNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minValNUD)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.gearboxGaugePanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gearboxGaugePanel2
            // 
            this.gearboxGaugePanel2.AutoplaceElements = false;
            this.gearboxGaugePanel2.AutoScrollHorizontalMaximum = 100;
            this.gearboxGaugePanel2.AutoScrollHorizontalMinimum = 0;
            this.gearboxGaugePanel2.AutoScrollHPos = 0;
            this.gearboxGaugePanel2.AutoScrollVerticalMaximum = 100;
            this.gearboxGaugePanel2.AutoScrollVerticalMinimum = 0;
            this.gearboxGaugePanel2.AutoScrollVPos = 0;
            this.gearboxGaugePanel2.AutoSizeElements = false;
            this.gearboxGaugePanel2.BackColor = System.Drawing.Color.Transparent;
            this.gearboxGaugePanel2.backgroundColor1 = System.Drawing.Color.DarkGray;
            this.gearboxGaugePanel2.backgroundColor2 = System.Drawing.Color.Gainsboro;
            this.gearboxGaugePanel2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
            this.gearboxGaugePanel2.BorderWidth = 1;
            this.gearboxGaugePanel2.Controls.Add(this.maxValNUD);
            this.gearboxGaugePanel2.Controls.Add(this.minValNUD);
            this.gearboxGaugePanel2.Controls.Add(this.groupBox1);
            this.gearboxGaugePanel2.Controls.Add(this.inputChannelLabel);
            this.gearboxGaugePanel2.Controls.Add(this.labelTextBox);
            this.gearboxGaugePanel2.Controls.Add(this.unitnameLabel);
            this.gearboxGaugePanel2.Controls.Add(this.inputChannelComboBox);
            this.gearboxGaugePanel2.Controls.Add(this.unitNameTextBox);
            this.gearboxGaugePanel2.Controls.Add(this.maxvalLabel);
            this.gearboxGaugePanel2.Controls.Add(this.minvalLabel);
            this.gearboxGaugePanel2.Controls.Add(this.labelLabel);
            this.gearboxGaugePanel2.DrawBackImage = false;
            this.gearboxGaugePanel2.EnableAutoScrollHorizontal = false;
            this.gearboxGaugePanel2.EnableAutoScrollVertical = false;
            this.gearboxGaugePanel2.Gradient = Soko.Common.Controls.NicePanel.GradientType.SigmaBellHorizontalUpper;
            this.gearboxGaugePanel2.HorizontalMargin = 0;
            this.gearboxGaugePanel2.Location = new System.Drawing.Point(229, 0);
            this.gearboxGaugePanel2.MinimumSize = new System.Drawing.Size(518, 140);
            this.gearboxGaugePanel2.Name = "gearboxGaugePanel2";
            this.gearboxGaugePanel2.roundingRadius = 10;
            this.gearboxGaugePanel2.Size = new System.Drawing.Size(518, 199);
            this.gearboxGaugePanel2.SupportTransparentBackground = false;
            this.gearboxGaugePanel2.TabIndex = 18;
            this.gearboxGaugePanel2.VerticalMargin = 0;
            this.gearboxGaugePanel2.VisibleAutoScrollHorizontal = false;
            this.gearboxGaugePanel2.VisibleAutoScrollVertical = false;
            // 
            // maxValNUD
            // 
            this.maxValNUD.DecimalPlaces = 1;
            this.maxValNUD.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.maxValNUD.Location = new System.Drawing.Point(88, 112);
            this.maxValNUD.Maximum = new decimal(new int[] {
            -1593835521,
            466537709,
            54210,
            0});
            this.maxValNUD.Name = "maxValNUD";
            this.maxValNUD.Size = new System.Drawing.Size(166, 20);
            this.maxValNUD.TabIndex = 29;
            this.maxValNUD.ValueChanged += new System.EventHandler(this.maxValNUD_ValueChanged);
            // 
            // minValNUD
            // 
            this.minValNUD.DecimalPlaces = 1;
            this.minValNUD.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.minValNUD.Location = new System.Drawing.Point(88, 86);
            this.minValNUD.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.minValNUD.Name = "minValNUD";
            this.minValNUD.Size = new System.Drawing.Size(166, 20);
            this.minValNUD.TabIndex = 28;
            this.minValNUD.ValueChanged += new System.EventHandler(this.minValNUD_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pressureSwitchRB);
            this.groupBox1.Controls.Add(this.gearRatioRB);
            this.groupBox1.Controls.Add(this.outputSpeedRB);
            this.groupBox1.Controls.Add(this.inputSpeedRB);
            this.groupBox1.Controls.Add(this.torqueRB);
            this.groupBox1.Controls.Add(this.flowRB);
            this.groupBox1.Controls.Add(this.tempRB);
            this.groupBox1.Controls.Add(this.pressRB);
            this.groupBox1.Location = new System.Drawing.Point(272, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(233, 186);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Analog input type :";
            // 
            // pressureSwitchRB
            // 
            this.pressureSwitchRB.AutoSize = true;
            this.pressureSwitchRB.Location = new System.Drawing.Point(24, 162);
            this.pressureSwitchRB.Name = "pressureSwitchRB";
            this.pressureSwitchRB.Size = new System.Drawing.Size(99, 17);
            this.pressureSwitchRB.TabIndex = 7;
            this.pressureSwitchRB.TabStop = true;
            this.pressureSwitchRB.Text = "Pressure switch";
            this.pressureSwitchRB.UseVisualStyleBackColor = true;
            this.pressureSwitchRB.CheckedChanged += new System.EventHandler(this.pressureSwitchRB_CheckedChanged);
            // 
            // gearRatioRB
            // 
            this.gearRatioRB.AutoSize = true;
            this.gearRatioRB.Location = new System.Drawing.Point(24, 142);
            this.gearRatioRB.Name = "gearRatioRB";
            this.gearRatioRB.Size = new System.Drawing.Size(71, 17);
            this.gearRatioRB.TabIndex = 6;
            this.gearRatioRB.TabStop = true;
            this.gearRatioRB.Text = "Gear ratio";
            this.gearRatioRB.UseVisualStyleBackColor = true;
            this.gearRatioRB.CheckedChanged += new System.EventHandler(this.gearRatioRB_CheckedChanged);
            // 
            // outputSpeedRB
            // 
            this.outputSpeedRB.AutoSize = true;
            this.outputSpeedRB.Location = new System.Drawing.Point(24, 122);
            this.outputSpeedRB.Name = "outputSpeedRB";
            this.outputSpeedRB.Size = new System.Drawing.Size(89, 17);
            this.outputSpeedRB.TabIndex = 5;
            this.outputSpeedRB.TabStop = true;
            this.outputSpeedRB.Text = "Output speed";
            this.outputSpeedRB.UseVisualStyleBackColor = true;
            this.outputSpeedRB.CheckedChanged += new System.EventHandler(this.outputSpeedRB_CheckedChanged);
            // 
            // inputSpeedRB
            // 
            this.inputSpeedRB.AutoSize = true;
            this.inputSpeedRB.Location = new System.Drawing.Point(24, 102);
            this.inputSpeedRB.Name = "inputSpeedRB";
            this.inputSpeedRB.Size = new System.Drawing.Size(81, 17);
            this.inputSpeedRB.TabIndex = 4;
            this.inputSpeedRB.TabStop = true;
            this.inputSpeedRB.Text = "Input speed";
            this.inputSpeedRB.UseVisualStyleBackColor = true;
            this.inputSpeedRB.CheckedChanged += new System.EventHandler(this.inputSpeedRB_CheckedChanged);
            // 
            // torqueRB
            // 
            this.torqueRB.AutoSize = true;
            this.torqueRB.Location = new System.Drawing.Point(24, 82);
            this.torqueRB.Name = "torqueRB";
            this.torqueRB.Size = new System.Drawing.Size(92, 17);
            this.torqueRB.TabIndex = 3;
            this.torqueRB.TabStop = true;
            this.torqueRB.Text = "Torque gauge";
            this.torqueRB.UseVisualStyleBackColor = true;
            this.torqueRB.CheckedChanged += new System.EventHandler(this.torqueRB_CheckedChanged);
            // 
            // flowRB
            // 
            this.flowRB.AutoSize = true;
            this.flowRB.Location = new System.Drawing.Point(24, 62);
            this.flowRB.Name = "flowRB";
            this.flowRB.Size = new System.Drawing.Size(109, 17);
            this.flowRB.TabIndex = 2;
            this.flowRB.TabStop = true;
            this.flowRB.Text = "Flow meter gauge";
            this.flowRB.UseVisualStyleBackColor = true;
            this.flowRB.CheckedChanged += new System.EventHandler(this.flowRB_CheckedChanged);
            // 
            // tempRB
            // 
            this.tempRB.AutoSize = true;
            this.tempRB.Location = new System.Drawing.Point(24, 42);
            this.tempRB.Name = "tempRB";
            this.tempRB.Size = new System.Drawing.Size(118, 17);
            this.tempRB.TabIndex = 1;
            this.tempRB.TabStop = true;
            this.tempRB.Text = "Temperature gauge";
            this.tempRB.UseVisualStyleBackColor = true;
            this.tempRB.CheckedChanged += new System.EventHandler(this.tempRB_CheckedChanged);
            // 
            // pressRB
            // 
            this.pressRB.AutoSize = true;
            this.pressRB.Location = new System.Drawing.Point(24, 22);
            this.pressRB.Name = "pressRB";
            this.pressRB.Size = new System.Drawing.Size(99, 17);
            this.pressRB.TabIndex = 0;
            this.pressRB.TabStop = true;
            this.pressRB.Text = "Pressure gauge";
            this.pressRB.UseVisualStyleBackColor = true;
            this.pressRB.CheckedChanged += new System.EventHandler(this.pressRB_CheckedChanged);
            // 
            // inputChannelLabel
            // 
            this.inputChannelLabel.AutoSize = true;
            this.inputChannelLabel.BackColor = System.Drawing.Color.Transparent;
            this.inputChannelLabel.Location = new System.Drawing.Point(7, 140);
            this.inputChannelLabel.Name = "inputChannelLabel";
            this.inputChannelLabel.Size = new System.Drawing.Size(75, 13);
            this.inputChannelLabel.TabIndex = 17;
            this.inputChannelLabel.Text = "Input channel:";
            // 
            // labelTextBox
            // 
            this.labelTextBox.BackColor = System.Drawing.Color.White;
            this.labelTextBox.Location = new System.Drawing.Point(88, 33);
            this.labelTextBox.Name = "labelTextBox";
            this.labelTextBox.Size = new System.Drawing.Size(166, 20);
            this.labelTextBox.TabIndex = 9;
            this.labelTextBox.TextChanged += new System.EventHandler(this.labelTextBox_TextChanged);
            // 
            // unitnameLabel
            // 
            this.unitnameLabel.AutoSize = true;
            this.unitnameLabel.BackColor = System.Drawing.Color.Transparent;
            this.unitnameLabel.Location = new System.Drawing.Point(7, 62);
            this.unitnameLabel.Name = "unitnameLabel";
            this.unitnameLabel.Size = new System.Drawing.Size(58, 13);
            this.unitnameLabel.TabIndex = 11;
            this.unitnameLabel.Text = "Unit name:";
            // 
            // inputChannelComboBox
            // 
            this.inputChannelComboBox.FormattingEnabled = true;
            this.inputChannelComboBox.Location = new System.Drawing.Point(88, 137);
            this.inputChannelComboBox.Name = "inputChannelComboBox";
            this.inputChannelComboBox.Size = new System.Drawing.Size(166, 21);
            this.inputChannelComboBox.TabIndex = 16;
            this.inputChannelComboBox.SelectedIndexChanged += new System.EventHandler(this.inputChannelComboBox_SelectedIndexChanged);
            // 
            // unitNameTextBox
            // 
            this.unitNameTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.unitNameTextBox.Enabled = false;
            this.unitNameTextBox.Location = new System.Drawing.Point(88, 59);
            this.unitNameTextBox.Name = "unitNameTextBox";
            this.unitNameTextBox.Size = new System.Drawing.Size(166, 20);
            this.unitNameTextBox.TabIndex = 10;
            // 
            // maxvalLabel
            // 
            this.maxvalLabel.AutoSize = true;
            this.maxvalLabel.BackColor = System.Drawing.Color.Transparent;
            this.maxvalLabel.Location = new System.Drawing.Point(7, 114);
            this.maxvalLabel.Name = "maxvalLabel";
            this.maxvalLabel.Size = new System.Drawing.Size(74, 13);
            this.maxvalLabel.TabIndex = 15;
            this.maxvalLabel.Text = "Maximal value";
            // 
            // minvalLabel
            // 
            this.minvalLabel.AutoSize = true;
            this.minvalLabel.BackColor = System.Drawing.Color.Transparent;
            this.minvalLabel.Location = new System.Drawing.Point(7, 88);
            this.minvalLabel.Name = "minvalLabel";
            this.minvalLabel.Size = new System.Drawing.Size(74, 13);
            this.minvalLabel.TabIndex = 13;
            this.minvalLabel.Text = "Minimal value:";
            // 
            // labelLabel
            // 
            this.labelLabel.AutoSize = true;
            this.labelLabel.BackColor = System.Drawing.Color.Transparent;
            this.labelLabel.Location = new System.Drawing.Point(7, 36);
            this.labelLabel.Name = "labelLabel";
            this.labelLabel.Size = new System.Drawing.Size(74, 13);
            this.labelLabel.TabIndex = 2;
            this.labelLabel.Text = "Channel label:";
            // 
            // gearboxGaugePanel1
            // 
            this.gearboxGaugePanel1.AutoplaceElements = false;
            this.gearboxGaugePanel1.AutoScrollHorizontalMaximum = 100;
            this.gearboxGaugePanel1.AutoScrollHorizontalMinimum = 0;
            this.gearboxGaugePanel1.AutoScrollHPos = 0;
            this.gearboxGaugePanel1.AutoScrollVerticalMaximum = 100;
            this.gearboxGaugePanel1.AutoScrollVerticalMinimum = 0;
            this.gearboxGaugePanel1.AutoScrollVPos = 0;
            this.gearboxGaugePanel1.AutoSizeElements = false;
            this.gearboxGaugePanel1.BackColor = System.Drawing.Color.Transparent;
            this.gearboxGaugePanel1.backgroundColor1 = System.Drawing.Color.DarkGray;
            this.gearboxGaugePanel1.backgroundColor2 = System.Drawing.Color.Gainsboro;
            this.gearboxGaugePanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
            this.gearboxGaugePanel1.BorderWidth = 1;
            this.gearboxGaugePanel1.Controls.Add(this.channelsListBox);
            this.gearboxGaugePanel1.Controls.Add(this.gearboxGaugeAddButton);
            this.gearboxGaugePanel1.Controls.Add(this.gearboxGaugeDeleteButton);
            this.gearboxGaugePanel1.Controls.Add(this.gaugeLabel);
            this.gearboxGaugePanel1.DrawBackImage = false;
            this.gearboxGaugePanel1.EnableAutoScrollHorizontal = false;
            this.gearboxGaugePanel1.EnableAutoScrollVertical = false;
            this.gearboxGaugePanel1.Gradient = Soko.Common.Controls.NicePanel.GradientType.SigmaBellHorizontalUpper;
            this.gearboxGaugePanel1.HorizontalMargin = 0;
            this.gearboxGaugePanel1.Location = new System.Drawing.Point(0, 0);
            this.gearboxGaugePanel1.Name = "gearboxGaugePanel1";
            this.gearboxGaugePanel1.roundingRadius = 10;
            this.gearboxGaugePanel1.Size = new System.Drawing.Size(228, 199);
            this.gearboxGaugePanel1.SupportTransparentBackground = false;
            this.gearboxGaugePanel1.TabIndex = 4;
            this.gearboxGaugePanel1.VerticalMargin = 0;
            this.gearboxGaugePanel1.VisibleAutoScrollHorizontal = false;
            this.gearboxGaugePanel1.VisibleAutoScrollVertical = false;
            // 
            // channelsListBox
            // 
            this.channelsListBox.BackColor = System.Drawing.Color.Gainsboro;
            this.channelsListBox.FormattingEnabled = true;
            this.channelsListBox.Location = new System.Drawing.Point(6, 26);
            this.channelsListBox.Name = "channelsListBox";
            this.channelsListBox.Size = new System.Drawing.Size(173, 160);
            this.channelsListBox.TabIndex = 34;
            this.channelsListBox.SelectedIndexChanged += new System.EventHandler(this.channelsListBox_SelectedIndexChanged);
            // 
            // gearboxGaugeAddButton
            // 
            this.gearboxGaugeAddButton.AutoSize = true;
            this.gearboxGaugeAddButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gearboxGaugeAddButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gearboxGaugeAddButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gearboxGaugeAddButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gearboxGaugeAddButton.BackgroundColor = System.Drawing.Color.DarkGray;
            this.gearboxGaugeAddButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.gearboxGaugeAddButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.gearboxGaugeAddButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.gearboxGaugeAddButton.BorderWidth = 0;
            this.gearboxGaugeAddButton.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.gearboxGaugeAddButton.ContentPadding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.gearboxGaugeAddButton.DrawBackColorOnFocus = false;
            this.gearboxGaugeAddButton.DrawBackgroundImage = false;
            this.gearboxGaugeAddButton.DrawBorderOnFocus = false;
            this.gearboxGaugeAddButton.DrawBorderOnTop = false;
            this.gearboxGaugeAddButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gearboxGaugeAddButton.Image = global::GST.Gearshift.Components.Properties.Resources.AnalogInputChannelConfigPanel_Add_32x32;
            this.gearboxGaugeAddButton.ImageDisabled = global::GST.Gearshift.Components.Properties.Resources.AnalogInputChannelConfigPanel_Add_32x32_BW;
            this.gearboxGaugeAddButton.Location = new System.Drawing.Point(185, 107);
            this.gearboxGaugeAddButton.Name = "gearboxGaugeAddButton";
            this.gearboxGaugeAddButton.Size = new System.Drawing.Size(36, 36);
            this.gearboxGaugeAddButton.SupportTransparentBackground = false;
            this.gearboxGaugeAddButton.TabIndex = 31;
            this.gearboxGaugeAddButton.Text = "Add";
            this.gearboxGaugeAddButton.TextImageRelation = Soko.Common.Controls.TextRelation.Underneath;
            this.gearboxGaugeAddButton.TextImageSpacing = 1;
            this.gearboxGaugeAddButton.Click += new System.EventHandler(this.gaugeAddButton_Click);
            // 
            // gearboxGaugeDeleteButton
            // 
            this.gearboxGaugeDeleteButton.AutoSize = true;
            this.gearboxGaugeDeleteButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gearboxGaugeDeleteButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gearboxGaugeDeleteButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gearboxGaugeDeleteButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gearboxGaugeDeleteButton.BackgroundColor = System.Drawing.Color.DarkGray;
            this.gearboxGaugeDeleteButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.gearboxGaugeDeleteButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.gearboxGaugeDeleteButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.gearboxGaugeDeleteButton.BorderWidth = 0;
            this.gearboxGaugeDeleteButton.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.gearboxGaugeDeleteButton.ContentPadding = new System.Windows.Forms.Padding(-2, 1, 0, 0);
            this.gearboxGaugeDeleteButton.DrawBackColorOnFocus = false;
            this.gearboxGaugeDeleteButton.DrawBackgroundImage = false;
            this.gearboxGaugeDeleteButton.DrawBorderOnFocus = false;
            this.gearboxGaugeDeleteButton.DrawBorderOnTop = false;
            this.gearboxGaugeDeleteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gearboxGaugeDeleteButton.Image = global::GST.Gearshift.Components.Properties.Resources.AnalogInputChannelConfigPanel_Delete_32x32;
            this.gearboxGaugeDeleteButton.ImageDisabled = global::GST.Gearshift.Components.Properties.Resources.AnalogInputChannelConfigPanel_Delete_32x32_BW;
            this.gearboxGaugeDeleteButton.Location = new System.Drawing.Point(185, 149);
            this.gearboxGaugeDeleteButton.Name = "gearboxGaugeDeleteButton";
            this.gearboxGaugeDeleteButton.Size = new System.Drawing.Size(36, 36);
            this.gearboxGaugeDeleteButton.SupportTransparentBackground = false;
            this.gearboxGaugeDeleteButton.TabIndex = 32;
            this.gearboxGaugeDeleteButton.Text = "Delete";
            this.gearboxGaugeDeleteButton.TextImageRelation = Soko.Common.Controls.TextRelation.Underneath;
            this.gearboxGaugeDeleteButton.TextImageSpacing = 1;
            this.gearboxGaugeDeleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // gaugeLabel
            // 
            this.gaugeLabel.AutoSize = true;
            this.gaugeLabel.BackColor = System.Drawing.Color.Transparent;
            this.gaugeLabel.Location = new System.Drawing.Point(8, 8);
            this.gaugeLabel.Name = "gaugeLabel";
            this.gaugeLabel.Size = new System.Drawing.Size(123, 13);
            this.gaugeLabel.TabIndex = 1;
            this.gaugeLabel.Text = "Analogue Input channel:";
            // 
            // PressureDisplayConfigPanel
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.gearboxGaugePanel2);
            this.Controls.Add(this.gearboxGaugePanel1);
            this.Name = "PressureDisplayConfigPanel";
            this.Size = new System.Drawing.Size(748, 201);
            this.gearboxGaugePanel2.ResumeLayout(false);
            this.gearboxGaugePanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxValNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minValNUD)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gearboxGaugePanel1.ResumeLayout(false);
            this.gearboxGaugePanel1.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private Soko.Common.Controls.NicePanel gearboxGaugePanel1;
    private System.Windows.Forms.Label inputChannelLabel;
    private System.Windows.Forms.Label gaugeLabel;
    private System.Windows.Forms.ComboBox inputChannelComboBox;
    private System.Windows.Forms.Label maxvalLabel;
    private System.Windows.Forms.Label labelLabel;
    private System.Windows.Forms.TextBox labelTextBox;
    private System.Windows.Forms.Label minvalLabel;
    private System.Windows.Forms.TextBox unitNameTextBox;
    private System.Windows.Forms.Label unitnameLabel;
    private Soko.Common.Controls.NicePanel gearboxGaugePanel2;
    private Soko.Common.Controls.NiceButton gearboxGaugeDeleteButton;
    private Soko.Common.Controls.NiceButton gearboxGaugeAddButton;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.RadioButton flowRB;
    private System.Windows.Forms.RadioButton tempRB;
    private System.Windows.Forms.RadioButton pressRB;
    private System.Windows.Forms.RadioButton torqueRB;
    private System.Windows.Forms.ListBox channelsListBox;
    private System.Windows.Forms.NumericUpDown maxValNUD;
    private System.Windows.Forms.NumericUpDown minValNUD;
    private System.Windows.Forms.RadioButton outputSpeedRB;
    private System.Windows.Forms.RadioButton inputSpeedRB;
    private System.Windows.Forms.RadioButton gearRatioRB;
    private System.Windows.Forms.RadioButton pressureSwitchRB;
  }
}
