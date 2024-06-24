namespace GST.ZF6.Components.Forms
{
  partial class MechShifterPanel
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
      this.bgPanel = new Soko.Common.Controls.NicePanel();
      this.mainPanel = new Soko.Common.Controls.NicePanel();
      this.stopButton = new Soko.Common.Controls.NiceButton();
      this.startButton = new Soko.Common.Controls.NiceButton();
      this.usbButton = new Soko.Common.Controls.NiceButton();
      this.gearSelectorGroupBox = new System.Windows.Forms.GroupBox();
      this.gear_6_Button = new System.Windows.Forms.RadioButton();
      this.gear_3_Button = new System.Windows.Forms.RadioButton();
      this.gear_5_Button = new System.Windows.Forms.RadioButton();
      this.gear_2_Button = new System.Windows.Forms.RadioButton();
      this.gear_4_Button = new System.Windows.Forms.RadioButton();
      this.gear_1_Button = new System.Windows.Forms.RadioButton();
      this.gear_N_Button = new System.Windows.Forms.RadioButton();
      this.gear_R_Button = new System.Windows.Forms.RadioButton();
      this.bgPanel.SuspendLayout();
      this.mainPanel.SuspendLayout();
      this.gearSelectorGroupBox.SuspendLayout();
      this.SuspendLayout();
      // 
      // bgPanel
      // 
      this.bgPanel.AutoplaceElements = true;
      this.bgPanel.AutoScrollHorizontalMaximum = 100;
      this.bgPanel.AutoScrollHorizontalMinimum = 0;
      this.bgPanel.AutoScrollHPos = 0;
      this.bgPanel.AutoScrollVerticalMaximum = 100;
      this.bgPanel.AutoScrollVerticalMinimum = 0;
      this.bgPanel.AutoScrollVPos = 0;
      this.bgPanel.AutoSizeElements = false;
      this.bgPanel.backgroundColor1 = System.Drawing.Color.WhiteSmoke;
      this.bgPanel.backgroundColor2 = System.Drawing.Color.Wheat;
      this.bgPanel.Controls.Add(this.mainPanel);
      this.bgPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.bgPanel.DrawBackImage = false;
      this.bgPanel.EnableAutoScrollHorizontal = false;
      this.bgPanel.EnableAutoScrollVertical = false;
      this.bgPanel.Gradient = Soko.Common.Controls.NicePanel.GradientType.None;
      this.bgPanel.HorizontalMargin = 0;
      this.bgPanel.Location = new System.Drawing.Point(0, 0);
      this.bgPanel.Name = "bgPanel";
      this.bgPanel.roundingRadius = 10;
      this.bgPanel.Size = new System.Drawing.Size(824, 737);
      this.bgPanel.TabIndex = 0;
      this.bgPanel.VerticalMargin = 0;
      this.bgPanel.VisibleAutoScrollHorizontal = false;
      this.bgPanel.VisibleAutoScrollVertical = false;
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
      this.mainPanel.backgroundColor1 = System.Drawing.Color.WhiteSmoke;
      this.mainPanel.backgroundColor2 = System.Drawing.Color.Wheat;
      this.mainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.mainPanel.Controls.Add(this.stopButton);
      this.mainPanel.Controls.Add(this.startButton);
      this.mainPanel.Controls.Add(this.usbButton);
      this.mainPanel.Controls.Add(this.gearSelectorGroupBox);
      this.mainPanel.DrawBackImage = false;
      this.mainPanel.EnableAutoScrollHorizontal = false;
      this.mainPanel.EnableAutoScrollVertical = false;
      this.mainPanel.Gradient = Soko.Common.Controls.NicePanel.GradientType.None;
      this.mainPanel.HorizontalMargin = 0;
      this.mainPanel.Location = new System.Drawing.Point(140, 105);
      this.mainPanel.Name = "mainPanel";
      this.mainPanel.roundingRadius = 10;
      this.mainPanel.Size = new System.Drawing.Size(544, 526);
      this.mainPanel.TabIndex = 0;
      this.mainPanel.VerticalMargin = 0;
      this.mainPanel.VisibleAutoScrollHorizontal = false;
      this.mainPanel.VisibleAutoScrollVertical = false;
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
      this.stopButton.DrawBackColorOnFocus = false;
      this.stopButton.DrawBackgroundImage = false;
      this.stopButton.DrawBorderOnFocus = false;
      this.stopButton.Enabled = false;
      this.stopButton.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.stopButton.Image = global::GST.ZF6.Components.Properties.Resources.STOP_64x64;
      this.stopButton.ImageDisabled = global::GST.ZF6.Components.Properties.Resources.SLEEP_64x64;
      this.stopButton.Location = new System.Drawing.Point(293, 141);
      this.stopButton.Name = "stopButton";
      this.stopButton.Size = new System.Drawing.Size(107, 100);
      this.stopButton.TabIndex = 9;
      this.stopButton.Text = "Stop";
      this.stopButton.TextImageRelation = Soko.Common.Controls.TextRelation.Underneath;
      this.stopButton.TextImageSpacing = 0;
      this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
      // 
      // startButton
      // 
      this.startButton.BackColorOnClicked1 = System.Drawing.Color.Orange;
      this.startButton.BackColorOnClicked2 = System.Drawing.Color.Orange;
      this.startButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(217)))), ((int)(((byte)(72)))));
      this.startButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(238)))), ((int)(((byte)(176)))));
      this.startButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
      this.startButton.BorderColor = System.Drawing.Color.Transparent;
      this.startButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(217)))), ((int)(((byte)(72)))));
      this.startButton.BorderWidth = 1;
      this.startButton.ContentPadding = new System.Windows.Forms.Padding(0);
      this.startButton.DrawBackColorOnFocus = false;
      this.startButton.DrawBackgroundImage = false;
      this.startButton.DrawBorderOnFocus = false;
      this.startButton.Enabled = false;
      this.startButton.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.startButton.Image = global::GST.ZF6.Components.Properties.Resources.START_64x64;
      this.startButton.ImageDisabled = global::GST.ZF6.Components.Properties.Resources.SLEEP_64x64;
      this.startButton.Location = new System.Drawing.Point(113, 141);
      this.startButton.Name = "startButton";
      this.startButton.Size = new System.Drawing.Size(107, 100);
      this.startButton.TabIndex = 9;
      this.startButton.Text = "Start";
      this.startButton.TextImageRelation = Soko.Common.Controls.TextRelation.Underneath;
      this.startButton.TextImageSpacing = 0;
      this.startButton.Click += new System.EventHandler(this.startButton_Click);
      // 
      // usbButton
      // 
      this.usbButton.BackColorOnClicked1 = System.Drawing.Color.Orange;
      this.usbButton.BackColorOnClicked2 = System.Drawing.Color.Orange;
      this.usbButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(217)))), ((int)(((byte)(72)))));
      this.usbButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(238)))), ((int)(((byte)(176)))));
      this.usbButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
      this.usbButton.BorderColor = System.Drawing.Color.Transparent;
      this.usbButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(217)))), ((int)(((byte)(72)))));
      this.usbButton.BorderWidth = 1;
      this.usbButton.ContentPadding = new System.Windows.Forms.Padding(0);
      this.usbButton.DrawBackColorOnFocus = false;
      this.usbButton.DrawBackgroundImage = false;
      this.usbButton.DrawBorderOnFocus = false;
      this.usbButton.Enabled = false;
      this.usbButton.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.usbButton.Image = global::GST.ZF6.Components.Properties.Resources.USB_64x64_BW;
      this.usbButton.ImageDisabled = global::GST.ZF6.Components.Properties.Resources.USB_64x64_BW;
      this.usbButton.Location = new System.Drawing.Point(45, 22);
      this.usbButton.Name = "usbButton";
      this.usbButton.Size = new System.Drawing.Size(423, 100);
      this.usbButton.TabIndex = 9;
      this.usbButton.Text = "USB Disconnected";
      this.usbButton.TextImageRelation = Soko.Common.Controls.TextRelation.OnRight;
      this.usbButton.TextImageSpacing = 15;
      // 
      // gearSelectorGroupBox
      // 
      this.gearSelectorGroupBox.Controls.Add(this.gear_6_Button);
      this.gearSelectorGroupBox.Controls.Add(this.gear_3_Button);
      this.gearSelectorGroupBox.Controls.Add(this.gear_5_Button);
      this.gearSelectorGroupBox.Controls.Add(this.gear_2_Button);
      this.gearSelectorGroupBox.Controls.Add(this.gear_4_Button);
      this.gearSelectorGroupBox.Controls.Add(this.gear_1_Button);
      this.gearSelectorGroupBox.Controls.Add(this.gear_N_Button);
      this.gearSelectorGroupBox.Controls.Add(this.gear_R_Button);
      this.gearSelectorGroupBox.Enabled = false;
      this.gearSelectorGroupBox.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.gearSelectorGroupBox.Location = new System.Drawing.Point(45, 272);
      this.gearSelectorGroupBox.Name = "gearSelectorGroupBox";
      this.gearSelectorGroupBox.Size = new System.Drawing.Size(423, 220);
      this.gearSelectorGroupBox.TabIndex = 8;
      this.gearSelectorGroupBox.TabStop = false;
      this.gearSelectorGroupBox.Text = "GEAR SELECTOR";
      // 
      // gear_6_Button
      // 
      this.gear_6_Button.AutoSize = true;
      this.gear_6_Button.Location = new System.Drawing.Point(52, 186);
      this.gear_6_Button.Name = "gear_6_Button";
      this.gear_6_Button.Size = new System.Drawing.Size(38, 27);
      this.gear_6_Button.TabIndex = 0;
      this.gear_6_Button.Text = "6";
      this.gear_6_Button.UseVisualStyleBackColor = true;
      this.gear_6_Button.CheckedChanged += new System.EventHandler(this.gear_6_Button_CheckedChanged);
      // 
      // gear_3_Button
      // 
      this.gear_3_Button.AutoSize = true;
      this.gear_3_Button.Location = new System.Drawing.Point(52, 117);
      this.gear_3_Button.Name = "gear_3_Button";
      this.gear_3_Button.Size = new System.Drawing.Size(38, 27);
      this.gear_3_Button.TabIndex = 0;
      this.gear_3_Button.Text = "3";
      this.gear_3_Button.UseVisualStyleBackColor = true;
      this.gear_3_Button.CheckedChanged += new System.EventHandler(this.gear_3_Button_CheckedChanged);
      // 
      // gear_5_Button
      // 
      this.gear_5_Button.AutoSize = true;
      this.gear_5_Button.Location = new System.Drawing.Point(52, 163);
      this.gear_5_Button.Name = "gear_5_Button";
      this.gear_5_Button.Size = new System.Drawing.Size(38, 27);
      this.gear_5_Button.TabIndex = 0;
      this.gear_5_Button.Text = "5";
      this.gear_5_Button.UseVisualStyleBackColor = true;
      this.gear_5_Button.CheckedChanged += new System.EventHandler(this.gear_5_Button_CheckedChanged);
      // 
      // gear_2_Button
      // 
      this.gear_2_Button.AutoSize = true;
      this.gear_2_Button.Location = new System.Drawing.Point(52, 94);
      this.gear_2_Button.Name = "gear_2_Button";
      this.gear_2_Button.Size = new System.Drawing.Size(38, 27);
      this.gear_2_Button.TabIndex = 0;
      this.gear_2_Button.Text = "2";
      this.gear_2_Button.UseVisualStyleBackColor = true;
      this.gear_2_Button.CheckedChanged += new System.EventHandler(this.gear_2_Button_CheckedChanged);
      // 
      // gear_4_Button
      // 
      this.gear_4_Button.AutoSize = true;
      this.gear_4_Button.Location = new System.Drawing.Point(52, 140);
      this.gear_4_Button.Name = "gear_4_Button";
      this.gear_4_Button.Size = new System.Drawing.Size(38, 27);
      this.gear_4_Button.TabIndex = 0;
      this.gear_4_Button.Text = "4";
      this.gear_4_Button.UseVisualStyleBackColor = true;
      this.gear_4_Button.CheckedChanged += new System.EventHandler(this.gear_4_Button_CheckedChanged);
      // 
      // gear_1_Button
      // 
      this.gear_1_Button.AutoSize = true;
      this.gear_1_Button.Location = new System.Drawing.Point(52, 71);
      this.gear_1_Button.Name = "gear_1_Button";
      this.gear_1_Button.Size = new System.Drawing.Size(38, 27);
      this.gear_1_Button.TabIndex = 0;
      this.gear_1_Button.Text = "1";
      this.gear_1_Button.UseVisualStyleBackColor = true;
      this.gear_1_Button.CheckedChanged += new System.EventHandler(this.gear_1_Button_CheckedChanged);
      // 
      // gear_N_Button
      // 
      this.gear_N_Button.AutoSize = true;
      this.gear_N_Button.Location = new System.Drawing.Point(52, 48);
      this.gear_N_Button.Name = "gear_N_Button";
      this.gear_N_Button.Size = new System.Drawing.Size(40, 27);
      this.gear_N_Button.TabIndex = 0;
      this.gear_N_Button.Text = "N";
      this.gear_N_Button.UseVisualStyleBackColor = true;
      this.gear_N_Button.CheckedChanged += new System.EventHandler(this.gear_N_Button_CheckedChanged);
      // 
      // gear_R_Button
      // 
      this.gear_R_Button.AutoSize = true;
      this.gear_R_Button.Checked = true;
      this.gear_R_Button.Location = new System.Drawing.Point(52, 25);
      this.gear_R_Button.Name = "gear_R_Button";
      this.gear_R_Button.Size = new System.Drawing.Size(38, 27);
      this.gear_R_Button.TabIndex = 0;
      this.gear_R_Button.TabStop = true;
      this.gear_R_Button.Text = "R";
      this.gear_R_Button.UseVisualStyleBackColor = true;
      this.gear_R_Button.CheckedChanged += new System.EventHandler(this.gear_R_Button_CheckedChanged);
      // 
      // MechShifterPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.Transparent;
      this.Controls.Add(this.bgPanel);
      this.Name = "MechShifterPanel";
      this.Size = new System.Drawing.Size(824, 737);
      this.bgPanel.ResumeLayout(false);
      this.mainPanel.ResumeLayout(false);
      this.gearSelectorGroupBox.ResumeLayout(false);
      this.gearSelectorGroupBox.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private Soko.Common.Controls.NicePanel bgPanel;
    private Soko.Common.Controls.NicePanel mainPanel;
    private System.Windows.Forms.GroupBox gearSelectorGroupBox;
    private System.Windows.Forms.RadioButton gear_6_Button;
    private System.Windows.Forms.RadioButton gear_3_Button;
    private System.Windows.Forms.RadioButton gear_5_Button;
    private System.Windows.Forms.RadioButton gear_2_Button;
    private System.Windows.Forms.RadioButton gear_4_Button;
    private System.Windows.Forms.RadioButton gear_1_Button;
    private System.Windows.Forms.RadioButton gear_N_Button;
    private System.Windows.Forms.RadioButton gear_R_Button;
    private Soko.Common.Controls.NiceButton usbButton;
    private Soko.Common.Controls.NiceButton stopButton;
    private Soko.Common.Controls.NiceButton startButton;
  }
}
