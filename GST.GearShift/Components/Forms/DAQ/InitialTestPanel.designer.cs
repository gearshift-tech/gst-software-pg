namespace GST.Gearshift.Components.Forms.DAQ
{
  partial class ChannelsInitialTest
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
            this.currentsRefreshTimer = new System.Windows.Forms.Timer(this.components);
            this.promptPane = new Soko.Common.Controls.ExplorerBarPane();
            this.operatorNameComboBox = new Soko.Common.Controls.NiceComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.promptAbortTestButton = new Soko.Common.Controls.NiceButton();
            this.promptContinueDevModeButton = new Soko.Common.Controls.NiceButton();
            this.promptContinueRepModeButton = new Soko.Common.Controls.NiceButton();
            this.operatorNameLabel = new System.Windows.Forms.Label();
            this.commentLabel = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.serialNoLabel = new System.Windows.Forms.Label();
            this.commentTextBox = new Soko.Common.Controls.NiceTextBox();
            this.testNameTextBox = new Soko.Common.Controls.NiceTextBox();
            this.serialNoTextBox = new Soko.Common.Controls.NiceTextBox();
            this.testPane = new Soko.Common.Controls.ExplorerBarPane();
            this.gM6TxxInitPanel = new Soko.CanCave.Components.Forms.GM6TxxInitPanel();
            this.nissanRE5InitPanel = new GST.Gearshift.Components.Forms.CAN.NissanRE5InitPanel();
            this.resultPane = new Soko.Common.Controls.ExplorerBarPane();
            this.nicePanel3 = new Soko.Common.Controls.NicePanel();
            this.testPassLabel = new System.Windows.Forms.Label();
            this.infoLabel1 = new System.Windows.Forms.Label();
            this.resultPane_startLoopTestButton = new Soko.Common.Controls.NiceButton();
            this.resultPane_abortTestButton = new Soko.Common.Controls.NiceButton();
            this.resultPane_startManualTestButton = new Soko.Common.Controls.NiceButton();
            this.resultPane_startAutomaticTestButton = new Soko.Common.Controls.NiceButton();
            this.decoderInitPanel = new GST.ZF6.Components.Forms.Zf6InitPanel();
            this.testPaneLeftpanel = new Soko.Common.Controls.NicePanel();
            this.testPaneRightPanel = new Soko.Common.Controls.NicePanel();
            this.imageButton3 = new Soko.Common.Controls.NiceButton();
            this.imageButton2 = new Soko.Common.Controls.NiceButton();
            this.continueButton = new Soko.Common.Controls.NiceButton();
            this.initialTestDisplay1 = new GST.Gearshift.Components.Forms.DAQ.InitialTestDisplay();
            this.promptPane.SuspendLayout();
            this.testPane.SuspendLayout();
            this.resultPane.SuspendLayout();
            this.nicePanel3.SuspendLayout();
            this.testPaneRightPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // currentsRefreshTimer
            // 
            this.currentsRefreshTimer.Tick += new System.EventHandler(this.RefreshCurrentValues);
            // 
            // promptPane
            // 
            this.promptPane.AllowUserInteraction = false;
            this.promptPane.BgndColor1 = System.Drawing.Color.DimGray;
            this.promptPane.BgndColor2 = System.Drawing.Color.Black;
            this.promptPane.Collapsed = true;
            this.promptPane.ColorScheme = Soko.Common.Controls.ExplorerBarPane.Theme.Black;
            this.promptPane.ColorSchemesEnabled = false;
            this.promptPane.Controls.Add(this.operatorNameComboBox);
            this.promptPane.Controls.Add(this.label1);
            this.promptPane.Controls.Add(this.promptAbortTestButton);
            this.promptPane.Controls.Add(this.promptContinueDevModeButton);
            this.promptPane.Controls.Add(this.promptContinueRepModeButton);
            this.promptPane.Controls.Add(this.operatorNameLabel);
            this.promptPane.Controls.Add(this.commentLabel);
            this.promptPane.Controls.Add(this.label10);
            this.promptPane.Controls.Add(this.label8);
            this.promptPane.Controls.Add(this.label9);
            this.promptPane.Controls.Add(this.label5);
            this.promptPane.Controls.Add(this.label7);
            this.promptPane.Controls.Add(this.label6);
            this.promptPane.Controls.Add(this.label4);
            this.promptPane.Controls.Add(this.label3);
            this.promptPane.Controls.Add(this.label2);
            this.promptPane.Controls.Add(this.serialNoLabel);
            this.promptPane.Controls.Add(this.commentTextBox);
            this.promptPane.Controls.Add(this.testNameTextBox);
            this.promptPane.Controls.Add(this.serialNoTextBox);
            this.promptPane.ForeColor = System.Drawing.Color.White;
            this.promptPane.HeaderColor1 = System.Drawing.Color.Gray;
            this.promptPane.HeaderColor2 = System.Drawing.Color.LightBlue;
            this.promptPane.HeaderColor3 = System.Drawing.Color.Transparent;
            this.promptPane.HideAfterMouseLeave = false;
            this.promptPane.HideAfterMouseLeaveDelayMs = 2000;
            this.promptPane.Location = new System.Drawing.Point(3, 3);
            this.promptPane.Name = "promptPane";
            this.promptPane.NormalHeight = 570;
            this.promptPane.Size = new System.Drawing.Size(728, 23);
            this.promptPane.TabIndex = 4;
            this.promptPane.Text = "Prompt";
            this.promptPane.Resize += new System.EventHandler(this.ReplacePanes);
            // 
            // operatorNameComboBox
            // 
            this.operatorNameComboBox.BackColor = System.Drawing.Color.DimGray;
            this.operatorNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.operatorNameComboBox.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.operatorNameComboBox.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.operatorNameComboBox.FormattingEnabled = true;
            this.operatorNameComboBox.GotFocusBorderColor = System.Drawing.Color.Black;
            this.operatorNameComboBox.GotFocusBorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.operatorNameComboBox.GotFocusDropDownButtonState = Soko.Common.Controls.NiceComboBox.EugenisButtonState.Original;
            this.operatorNameComboBox.Location = new System.Drawing.Point(357, 119);
            this.operatorNameComboBox.LostFocusBorderColor = System.Drawing.Color.Gray;
            this.operatorNameComboBox.LostFocusBorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.operatorNameComboBox.LostFocusDropDownButtonState = Soko.Common.Controls.NiceComboBox.EugenisButtonState.Flat;
            this.operatorNameComboBox.Name = "operatorNameComboBox";
            this.operatorNameComboBox.Size = new System.Drawing.Size(253, 21);
            this.operatorNameComboBox.Sorted = true;
            this.operatorNameComboBox.TabIndex = 11;
            this.operatorNameComboBox.UseGotFocusStyle = false;
            this.operatorNameComboBox.UseLostFocusStyle = false;
            this.operatorNameComboBox.DropDown += new System.EventHandler(this.promptPane_operatorNameComboBox_DropDown);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(107, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(503, 22);
            this.label1.TabIndex = 10;
            this.label1.Text = "Please fill the following fields to begin the test:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // promptAbortTestButton
            // 
            this.promptAbortTestButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.promptAbortTestButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.promptAbortTestButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.promptAbortTestButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.promptAbortTestButton.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.promptAbortTestButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.promptAbortTestButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.promptAbortTestButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.promptAbortTestButton.BorderWidth = 1;
            this.promptAbortTestButton.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.promptAbortTestButton.ContentPadding = new System.Windows.Forms.Padding(10);
            this.promptAbortTestButton.DrawBackColorOnFocus = false;
            this.promptAbortTestButton.DrawBackgroundImage = false;
            this.promptAbortTestButton.DrawBorderOnFocus = true;
            this.promptAbortTestButton.DrawBorderOnTop = false;
            this.promptAbortTestButton.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.promptAbortTestButton.Image = global::GST.Gearshift.Components.Properties.Resources.InitialTestPanel_Stop_128x128;
            this.promptAbortTestButton.ImageDisabled = global::GST.Gearshift.Components.Properties.Resources.InitialTestPanel_Stop_128x128;
            this.promptAbortTestButton.Location = new System.Drawing.Point(107, 349);
            this.promptAbortTestButton.Name = "promptAbortTestButton";
            this.promptAbortTestButton.Size = new System.Drawing.Size(148, 148);
            this.promptAbortTestButton.SupportTransparentBackground = false;
            this.promptAbortTestButton.TabIndex = 9;
            this.promptAbortTestButton.TextImageRelation = Soko.Common.Controls.TextRelation.Underneath;
            this.promptAbortTestButton.TextImageSpacing = 0;
            this.promptAbortTestButton.Click += new System.EventHandler(this.promptPane_AbortTestButton_Click);
            // 
            // promptContinueDevModeButton
            // 
            this.promptContinueDevModeButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.promptContinueDevModeButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.promptContinueDevModeButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.promptContinueDevModeButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.promptContinueDevModeButton.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.promptContinueDevModeButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.promptContinueDevModeButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.promptContinueDevModeButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.promptContinueDevModeButton.BorderWidth = 1;
            this.promptContinueDevModeButton.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.promptContinueDevModeButton.ContentPadding = new System.Windows.Forms.Padding(10);
            this.promptContinueDevModeButton.DrawBackColorOnFocus = false;
            this.promptContinueDevModeButton.DrawBackgroundImage = false;
            this.promptContinueDevModeButton.DrawBorderOnFocus = true;
            this.promptContinueDevModeButton.DrawBorderOnTop = false;
            this.promptContinueDevModeButton.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.promptContinueDevModeButton.Image = global::GST.Gearshift.Components.Properties.Resources.InitialTestPanel_Gears_128x128;
            this.promptContinueDevModeButton.ImageDisabled = global::GST.Gearshift.Components.Properties.Resources.InitialTestPanel_Gears_128x128;
            this.promptContinueDevModeButton.Location = new System.Drawing.Point(284, 349);
            this.promptContinueDevModeButton.Name = "promptContinueDevModeButton";
            this.promptContinueDevModeButton.Size = new System.Drawing.Size(148, 148);
            this.promptContinueDevModeButton.SupportTransparentBackground = false;
            this.promptContinueDevModeButton.TabIndex = 8;
            this.promptContinueDevModeButton.TextImageRelation = Soko.Common.Controls.TextRelation.Underneath;
            this.promptContinueDevModeButton.TextImageSpacing = 0;
            this.promptContinueDevModeButton.Click += new System.EventHandler(this.promptPane_ContinueInDevModeButton_Click);
            // 
            // promptContinueRepModeButton
            // 
            this.promptContinueRepModeButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.promptContinueRepModeButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.promptContinueRepModeButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.promptContinueRepModeButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.promptContinueRepModeButton.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.promptContinueRepModeButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.promptContinueRepModeButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.promptContinueRepModeButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.promptContinueRepModeButton.BorderWidth = 1;
            this.promptContinueRepModeButton.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.promptContinueRepModeButton.ContentPadding = new System.Windows.Forms.Padding(10, 15, 10, 10);
            this.promptContinueRepModeButton.DrawBackColorOnFocus = false;
            this.promptContinueRepModeButton.DrawBackgroundImage = false;
            this.promptContinueRepModeButton.DrawBorderOnFocus = true;
            this.promptContinueRepModeButton.DrawBorderOnTop = false;
            this.promptContinueRepModeButton.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.promptContinueRepModeButton.Image = global::GST.Gearshift.Components.Properties.Resources.InitialTestPanel_ArrowRight_128x128;
            this.promptContinueRepModeButton.ImageDisabled = global::GST.Gearshift.Components.Properties.Resources.InitialTestPanel_ArrowRight_128x128;
            this.promptContinueRepModeButton.Location = new System.Drawing.Point(462, 349);
            this.promptContinueRepModeButton.Name = "promptContinueRepModeButton";
            this.promptContinueRepModeButton.Size = new System.Drawing.Size(148, 148);
            this.promptContinueRepModeButton.SupportTransparentBackground = false;
            this.promptContinueRepModeButton.TabIndex = 8;
            this.promptContinueRepModeButton.TextImageRelation = Soko.Common.Controls.TextRelation.Underneath;
            this.promptContinueRepModeButton.TextImageSpacing = 0;
            this.promptContinueRepModeButton.Click += new System.EventHandler(this.promptPane_ContinueInReportModeButton_Click);
            // 
            // operatorNameLabel
            // 
            this.operatorNameLabel.AutoSize = true;
            this.operatorNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.operatorNameLabel.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.operatorNameLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.operatorNameLabel.Location = new System.Drawing.Point(104, 116);
            this.operatorNameLabel.Name = "operatorNameLabel";
            this.operatorNameLabel.Size = new System.Drawing.Size(129, 18);
            this.operatorNameLabel.TabIndex = 7;
            this.operatorNameLabel.Text = "Operator name:";
            // 
            // commentLabel
            // 
            this.commentLabel.AutoSize = true;
            this.commentLabel.BackColor = System.Drawing.Color.Transparent;
            this.commentLabel.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.commentLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.commentLabel.Location = new System.Drawing.Point(104, 192);
            this.commentLabel.Name = "commentLabel";
            this.commentLabel.Size = new System.Drawing.Size(90, 18);
            this.commentLabel.TabIndex = 5;
            this.commentLabel.Text = "Comment:";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label10.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label10.Location = new System.Drawing.Point(462, 536);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(148, 18);
            this.label10.TabIndex = 4;
            this.label10.Text = "mode";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label8.Location = new System.Drawing.Point(462, 518);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(148, 18);
            this.label8.TabIndex = 4;
            this.label8.Text = "reporting";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label9.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label9.Location = new System.Drawing.Point(284, 536);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(148, 18);
            this.label9.TabIndex = 4;
            this.label9.Text = "mode";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label5.Location = new System.Drawing.Point(462, 500);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(148, 18);
            this.label5.TabIndex = 4;
            this.label5.Text = "Continue in";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label7.Location = new System.Drawing.Point(284, 518);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(148, 18);
            this.label7.TabIndex = 4;
            this.label7.Text = "development";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label6.Location = new System.Drawing.Point(107, 518);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(148, 18);
            this.label6.TabIndex = 4;
            this.label6.Text = "test";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label4.Location = new System.Drawing.Point(284, 500);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(148, 18);
            this.label4.TabIndex = 4;
            this.label4.Text = "Continue in";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Location = new System.Drawing.Point(107, 500);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "Cancel the";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(104, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "Test name:";
            // 
            // serialNoLabel
            // 
            this.serialNoLabel.AutoSize = true;
            this.serialNoLabel.BackColor = System.Drawing.Color.Transparent;
            this.serialNoLabel.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.serialNoLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.serialNoLabel.Location = new System.Drawing.Point(104, 155);
            this.serialNoLabel.Name = "serialNoLabel";
            this.serialNoLabel.Size = new System.Drawing.Size(184, 18);
            this.serialNoLabel.TabIndex = 4;
            this.serialNoLabel.Text = "Gearbox serial number:";
            // 
            // commentTextBox
            // 
            this.commentTextBox.BackAlpha = 5;
            this.commentTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.commentTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.commentTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.commentTextBox.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.commentTextBox.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.commentTextBox.Location = new System.Drawing.Point(358, 194);
            this.commentTextBox.Multiline = true;
            this.commentTextBox.Name = "commentTextBox";
            this.commentTextBox.Size = new System.Drawing.Size(252, 126);
            this.commentTextBox.TabIndex = 3;
            // 
            // testNameTextBox
            // 
            this.testNameTextBox.BackAlpha = 5;
            this.testNameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.testNameTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.testNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.testNameTextBox.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.testNameTextBox.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.testNameTextBox.Location = new System.Drawing.Point(358, 82);
            this.testNameTextBox.Name = "testNameTextBox";
            this.testNameTextBox.ReadOnly = true;
            this.testNameTextBox.Size = new System.Drawing.Size(252, 19);
            this.testNameTextBox.TabIndex = 0;
            // 
            // serialNoTextBox
            // 
            this.serialNoTextBox.BackAlpha = 5;
            this.serialNoTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.serialNoTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.serialNoTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.serialNoTextBox.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.serialNoTextBox.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.serialNoTextBox.Location = new System.Drawing.Point(358, 157);
            this.serialNoTextBox.Name = "serialNoTextBox";
            this.serialNoTextBox.Size = new System.Drawing.Size(252, 19);
            this.serialNoTextBox.TabIndex = 0;
            // 
            // testPane
            // 
            this.testPane.AllowUserInteraction = false;
            this.testPane.BgndColor1 = System.Drawing.Color.DimGray;
            this.testPane.BgndColor2 = System.Drawing.Color.Black;
            this.testPane.Collapsed = false;
            this.testPane.ColorScheme = Soko.Common.Controls.ExplorerBarPane.Theme.Black;
            this.testPane.ColorSchemesEnabled = false;
            this.testPane.Controls.Add(this.gM6TxxInitPanel);
            this.testPane.Controls.Add(this.nissanRE5InitPanel);
            this.testPane.Controls.Add(this.decoderInitPanel);
            this.testPane.Controls.Add(this.testPaneLeftpanel);
            this.testPane.Controls.Add(this.testPaneRightPanel);
            this.testPane.ForeColor = System.Drawing.Color.White;
            this.testPane.HeaderColor1 = System.Drawing.Color.Gray;
            this.testPane.HeaderColor2 = System.Drawing.Color.LightBlue;
            this.testPane.HeaderColor3 = System.Drawing.Color.Transparent;
            this.testPane.HideAfterMouseLeave = false;
            this.testPane.HideAfterMouseLeaveDelayMs = 2000;
            this.testPane.Location = new System.Drawing.Point(3, 30);
            this.testPane.Name = "testPane";
            this.testPane.NormalHeight = 527;
            this.testPane.Size = new System.Drawing.Size(728, 527);
            this.testPane.TabIndex = 3;
            this.testPane.Text = "Test";
            this.testPane.Resize += new System.EventHandler(this.ReplacePanes);
            // 
            // gM6TxxInitPanel
            // 
            this.gM6TxxInitPanel.BackColor = System.Drawing.Color.Transparent;
            this.gM6TxxInitPanel.Location = new System.Drawing.Point(5, 334);
            this.gM6TxxInitPanel.Name = "gM6TxxInitPanel";
            this.gM6TxxInitPanel.Size = new System.Drawing.Size(715, 29);
            this.gM6TxxInitPanel.TabIndex = 5;
            this.gM6TxxInitPanel.ContinueButtonClicked += new System.EventHandler(this.gM6TxxInitPanel_ContinueButtonClicked);
            this.gM6TxxInitPanel.CancelButtonClicked += new System.EventHandler(this.gM6TxxInitPanel_CancelButtonClicked);
            // 
            // nissanRE5InitPanel
            // 
            this.nissanRE5InitPanel.BackColor = System.Drawing.Color.Transparent;
            this.nissanRE5InitPanel.Location = new System.Drawing.Point(5, 301);
            this.nissanRE5InitPanel.Name = "nissanRE5InitPanel";
            this.nissanRE5InitPanel.Size = new System.Drawing.Size(715, 27);
            this.nissanRE5InitPanel.TabIndex = 4;
            this.nissanRE5InitPanel.ContinueButtonClicked += new System.EventHandler(this.nissanRE5InitPanel_ContinueButtonClicked);
            this.nissanRE5InitPanel.CancelButtonClicked += new System.EventHandler(this.nissanRE5InitPanel_CancelButtonClicked);
            // 
            // resultPane
            // 
            this.resultPane.AllowUserInteraction = false;
            this.resultPane.BgndColor1 = System.Drawing.Color.DimGray;
            this.resultPane.BgndColor2 = System.Drawing.Color.Black;
            this.resultPane.Collapsed = false;
            this.resultPane.ColorScheme = Soko.Common.Controls.ExplorerBarPane.Theme.Black;
            this.resultPane.ColorSchemesEnabled = false;
            this.resultPane.Controls.Add(this.nicePanel3);
            this.resultPane.Controls.Add(this.resultPane_startLoopTestButton);
            this.resultPane.Controls.Add(this.resultPane_abortTestButton);
            this.resultPane.Controls.Add(this.resultPane_startManualTestButton);
            this.resultPane.Controls.Add(this.resultPane_startAutomaticTestButton);
            this.resultPane.ForeColor = System.Drawing.Color.White;
            this.resultPane.HeaderColor1 = System.Drawing.Color.Gray;
            this.resultPane.HeaderColor2 = System.Drawing.Color.LightBlue;
            this.resultPane.HeaderColor3 = System.Drawing.Color.Transparent;
            this.resultPane.HideAfterMouseLeave = false;
            this.resultPane.HideAfterMouseLeaveDelayMs = 2000;
            this.resultPane.Location = new System.Drawing.Point(0, 29);
            this.resultPane.Name = "resultPane";
            this.resultPane.NormalHeight = 487;
            this.resultPane.Size = new System.Drawing.Size(728, 519);
            this.resultPane.TabIndex = 4;
            this.resultPane.Text = "Result";
            this.resultPane.Resize += new System.EventHandler(this.ReplacePanes);
            // 
            // nicePanel3
            // 
            this.nicePanel3.AutoplaceElements = false;
            this.nicePanel3.AutoScrollHorizontalMaximum = 100;
            this.nicePanel3.AutoScrollHorizontalMinimum = 0;
            this.nicePanel3.AutoScrollHPos = 0;
            this.nicePanel3.AutoScrollVerticalMaximum = 100;
            this.nicePanel3.AutoScrollVerticalMinimum = 0;
            this.nicePanel3.AutoScrollVPos = 0;
            this.nicePanel3.AutoSizeElements = false;
            this.nicePanel3.BackColor = System.Drawing.Color.Transparent;
            this.nicePanel3.backgroundColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.nicePanel3.backgroundColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.nicePanel3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
            this.nicePanel3.BorderWidth = 1;
            this.nicePanel3.Controls.Add(this.testPassLabel);
            this.nicePanel3.Controls.Add(this.infoLabel1);
            this.nicePanel3.DrawBackImage = false;
            this.nicePanel3.EnableAutoScrollHorizontal = false;
            this.nicePanel3.EnableAutoScrollVertical = false;
            this.nicePanel3.Gradient = Soko.Common.Controls.NicePanel.GradientType.None;
            this.nicePanel3.HorizontalMargin = 0;
            this.nicePanel3.Location = new System.Drawing.Point(294, 46);
            this.nicePanel3.Name = "nicePanel3";
            this.nicePanel3.roundingRadius = 10;
            this.nicePanel3.Size = new System.Drawing.Size(407, 283);
            this.nicePanel3.SupportTransparentBackground = false;
            this.nicePanel3.TabIndex = 6;
            this.nicePanel3.VerticalMargin = 0;
            this.nicePanel3.VisibleAutoScrollHorizontal = false;
            this.nicePanel3.VisibleAutoScrollVertical = false;
            // 
            // testPassLabel
            // 
            this.testPassLabel.BackColor = System.Drawing.Color.Black;
            this.testPassLabel.Font = new System.Drawing.Font("Trebuchet MS", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.testPassLabel.Location = new System.Drawing.Point(3, 33);
            this.testPassLabel.Name = "testPassLabel";
            this.testPassLabel.Size = new System.Drawing.Size(401, 42);
            this.testPassLabel.TabIndex = 1;
            this.testPassLabel.Text = "INITIAL TEST PASSED!";
            this.testPassLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // infoLabel1
            // 
            this.infoLabel1.BackColor = System.Drawing.Color.Transparent;
            this.infoLabel1.ForeColor = System.Drawing.Color.White;
            this.infoLabel1.Location = new System.Drawing.Point(27, 125);
            this.infoLabel1.Name = "infoLabel1";
            this.infoLabel1.Size = new System.Drawing.Size(357, 117);
            this.infoLabel1.TabIndex = 3;
            this.infoLabel1.Text = "Go to the main measurement session";
            this.infoLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // resultPane_startLoopTestButton
            // 
            this.resultPane_startLoopTestButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.resultPane_startLoopTestButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.resultPane_startLoopTestButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.resultPane_startLoopTestButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.resultPane_startLoopTestButton.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.resultPane_startLoopTestButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.resultPane_startLoopTestButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.resultPane_startLoopTestButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.resultPane_startLoopTestButton.BorderWidth = 1;
            this.resultPane_startLoopTestButton.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.resultPane_startLoopTestButton.ContentPadding = new System.Windows.Forms.Padding(10);
            this.resultPane_startLoopTestButton.DrawBackColorOnFocus = false;
            this.resultPane_startLoopTestButton.DrawBackgroundImage = false;
            this.resultPane_startLoopTestButton.DrawBorderOnFocus = true;
            this.resultPane_startLoopTestButton.DrawBorderOnTop = false;
            this.resultPane_startLoopTestButton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.resultPane_startLoopTestButton.Image = global::GST.Gearshift.Components.Properties.Resources.InitialTestPanel_LoopMode_96x96;
            this.resultPane_startLoopTestButton.ImageDisabled = global::GST.Gearshift.Components.Properties.Resources.InitialTestPanel_Refresh_48;
            this.resultPane_startLoopTestButton.Location = new System.Drawing.Point(21, 342);
            this.resultPane_startLoopTestButton.Name = "resultPane_startLoopTestButton";
            this.resultPane_startLoopTestButton.Size = new System.Drawing.Size(250, 135);
            this.resultPane_startLoopTestButton.SupportTransparentBackground = false;
            this.resultPane_startLoopTestButton.TabIndex = 5;
            this.resultPane_startLoopTestButton.Text = "Start loop test";
            this.resultPane_startLoopTestButton.TextImageRelation = Soko.Common.Controls.TextRelation.Underneath;
            this.resultPane_startLoopTestButton.TextImageSpacing = 5;
            this.resultPane_startLoopTestButton.Click += new System.EventHandler(this.resultPane_StartLoopTestButton_Click);
            // 
            // resultPane_abortTestButton
            // 
            this.resultPane_abortTestButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.resultPane_abortTestButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.resultPane_abortTestButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.resultPane_abortTestButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.resultPane_abortTestButton.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.resultPane_abortTestButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.resultPane_abortTestButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.resultPane_abortTestButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.resultPane_abortTestButton.BorderWidth = 1;
            this.resultPane_abortTestButton.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.resultPane_abortTestButton.ContentPadding = new System.Windows.Forms.Padding(10);
            this.resultPane_abortTestButton.DrawBackColorOnFocus = true;
            this.resultPane_abortTestButton.DrawBackgroundImage = false;
            this.resultPane_abortTestButton.DrawBorderOnFocus = true;
            this.resultPane_abortTestButton.DrawBorderOnTop = false;
            this.resultPane_abortTestButton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.resultPane_abortTestButton.Image = global::GST.Gearshift.Components.Properties.Resources.InitialTestPanel_Stop_96x96;
            this.resultPane_abortTestButton.ImageDisabled = global::GST.Gearshift.Components.Properties.Resources.InitialTestPanel_Delete_48x48_BW;
            this.resultPane_abortTestButton.Location = new System.Drawing.Point(294, 342);
            this.resultPane_abortTestButton.Name = "resultPane_abortTestButton";
            this.resultPane_abortTestButton.Size = new System.Drawing.Size(407, 135);
            this.resultPane_abortTestButton.SupportTransparentBackground = false;
            this.resultPane_abortTestButton.TabIndex = 2;
            this.resultPane_abortTestButton.Text = "Cancel the test";
            this.resultPane_abortTestButton.TextImageRelation = Soko.Common.Controls.TextRelation.Underneath;
            this.resultPane_abortTestButton.TextImageSpacing = 5;
            this.resultPane_abortTestButton.Click += new System.EventHandler(this.resultPane_AbortTestButton_Click);
            // 
            // resultPane_startManualTestButton
            // 
            this.resultPane_startManualTestButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.resultPane_startManualTestButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.resultPane_startManualTestButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.resultPane_startManualTestButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.resultPane_startManualTestButton.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.resultPane_startManualTestButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.resultPane_startManualTestButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.resultPane_startManualTestButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.resultPane_startManualTestButton.BorderWidth = 1;
            this.resultPane_startManualTestButton.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.resultPane_startManualTestButton.ContentPadding = new System.Windows.Forms.Padding(10);
            this.resultPane_startManualTestButton.DrawBackColorOnFocus = true;
            this.resultPane_startManualTestButton.DrawBackgroundImage = false;
            this.resultPane_startManualTestButton.DrawBorderOnFocus = true;
            this.resultPane_startManualTestButton.DrawBorderOnTop = false;
            this.resultPane_startManualTestButton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.resultPane_startManualTestButton.Image = global::GST.Gearshift.Components.Properties.Resources.InitialTestPanel_ManualMode_96x96;
            this.resultPane_startManualTestButton.ImageDisabled = global::GST.Gearshift.Components.Properties.Resources.InitialTestPanel_manual;
            this.resultPane_startManualTestButton.Location = new System.Drawing.Point(21, 194);
            this.resultPane_startManualTestButton.Name = "resultPane_startManualTestButton";
            this.resultPane_startManualTestButton.Size = new System.Drawing.Size(250, 135);
            this.resultPane_startManualTestButton.SupportTransparentBackground = false;
            this.resultPane_startManualTestButton.TabIndex = 0;
            this.resultPane_startManualTestButton.Text = "Start manual test";
            this.resultPane_startManualTestButton.TextImageRelation = Soko.Common.Controls.TextRelation.Underneath;
            this.resultPane_startManualTestButton.TextImageSpacing = 4;
            this.resultPane_startManualTestButton.Click += new System.EventHandler(this.resultPane_StartManualTestButton_Click);
            // 
            // resultPane_startAutomaticTestButton
            // 
            this.resultPane_startAutomaticTestButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.resultPane_startAutomaticTestButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.resultPane_startAutomaticTestButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.resultPane_startAutomaticTestButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.resultPane_startAutomaticTestButton.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.resultPane_startAutomaticTestButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.resultPane_startAutomaticTestButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.resultPane_startAutomaticTestButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.resultPane_startAutomaticTestButton.BorderWidth = 1;
            this.resultPane_startAutomaticTestButton.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.resultPane_startAutomaticTestButton.ContentPadding = new System.Windows.Forms.Padding(10);
            this.resultPane_startAutomaticTestButton.DrawBackColorOnFocus = true;
            this.resultPane_startAutomaticTestButton.DrawBackgroundImage = false;
            this.resultPane_startAutomaticTestButton.DrawBorderOnFocus = true;
            this.resultPane_startAutomaticTestButton.DrawBorderOnTop = false;
            this.resultPane_startAutomaticTestButton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.resultPane_startAutomaticTestButton.Image = global::GST.Gearshift.Components.Properties.Resources.InitialTestPanel_AutoMode_96x96;
            this.resultPane_startAutomaticTestButton.ImageDisabled = global::GST.Gearshift.Components.Properties.Resources.InitialTestPanel_Services_48x48;
            this.resultPane_startAutomaticTestButton.Location = new System.Drawing.Point(21, 46);
            this.resultPane_startAutomaticTestButton.Name = "resultPane_startAutomaticTestButton";
            this.resultPane_startAutomaticTestButton.Size = new System.Drawing.Size(250, 135);
            this.resultPane_startAutomaticTestButton.SupportTransparentBackground = false;
            this.resultPane_startAutomaticTestButton.TabIndex = 0;
            this.resultPane_startAutomaticTestButton.Text = "Start automatic test";
            this.resultPane_startAutomaticTestButton.TextImageRelation = Soko.Common.Controls.TextRelation.Underneath;
            this.resultPane_startAutomaticTestButton.TextImageSpacing = 4;
            this.resultPane_startAutomaticTestButton.Click += new System.EventHandler(this.resultPane_StartAutomaticTestButton_Click);
            // 
            // decoderInitPanel
            // 
            this.decoderInitPanel.BackColor = System.Drawing.Color.Transparent;
            this.decoderInitPanel.gearboxType = Soko.Common.Common.GearboxControllerType.NON_MECHATRONIC;
            this.decoderInitPanel.Location = new System.Drawing.Point(5, 270);
            this.decoderInitPanel.Name = "decoderInitPanel";
            this.decoderInitPanel.Size = new System.Drawing.Size(715, 25);
            this.decoderInitPanel.TabIndex = 3;
            this.decoderInitPanel.ContinueButtonClicked += new System.EventHandler(this.zf6InitPanel_ContinueButtonClicked);
            this.decoderInitPanel.CancelButtonClicked += new System.EventHandler(this.zf6InitPanel_CancelButtonClicked);
            // 
            // testPaneLeftpanel
            // 
            this.testPaneLeftpanel.AutoplaceElements = true;
            this.testPaneLeftpanel.AutoScrollHorizontalMaximum = 100;
            this.testPaneLeftpanel.AutoScrollHorizontalMinimum = 0;
            this.testPaneLeftpanel.AutoScrollHPos = 0;
            this.testPaneLeftpanel.AutoScrollVerticalMaximum = 100;
            this.testPaneLeftpanel.AutoScrollVerticalMinimum = 0;
            this.testPaneLeftpanel.AutoScrollVPos = 0;
            this.testPaneLeftpanel.AutoSizeElements = false;
            this.testPaneLeftpanel.BackColor = System.Drawing.Color.Transparent;
            this.testPaneLeftpanel.backgroundColor1 = System.Drawing.Color.Transparent;
            this.testPaneLeftpanel.backgroundColor2 = System.Drawing.Color.Transparent;
            this.testPaneLeftpanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
            this.testPaneLeftpanel.BorderWidth = 1;
            this.testPaneLeftpanel.DrawBackImage = false;
            this.testPaneLeftpanel.EnableAutoScrollHorizontal = false;
            this.testPaneLeftpanel.EnableAutoScrollVertical = false;
            this.testPaneLeftpanel.Gradient = Soko.Common.Controls.NicePanel.GradientType.None;
            this.testPaneLeftpanel.HorizontalMargin = 0;
            this.testPaneLeftpanel.Location = new System.Drawing.Point(5, 29);
            this.testPaneLeftpanel.Name = "testPaneLeftpanel";
            this.testPaneLeftpanel.roundingRadius = 10;
            this.testPaneLeftpanel.Size = new System.Drawing.Size(609, 233);
            this.testPaneLeftpanel.SupportTransparentBackground = false;
            this.testPaneLeftpanel.TabIndex = 1;
            this.testPaneLeftpanel.VerticalMargin = 0;
            this.testPaneLeftpanel.VisibleAutoScrollHorizontal = false;
            this.testPaneLeftpanel.VisibleAutoScrollVertical = false;
            // 
            // testPaneRightPanel
            // 
            this.testPaneRightPanel.AutoplaceElements = true;
            this.testPaneRightPanel.AutoScrollHorizontalMaximum = 100;
            this.testPaneRightPanel.AutoScrollHorizontalMinimum = 0;
            this.testPaneRightPanel.AutoScrollHPos = 0;
            this.testPaneRightPanel.AutoScrollVerticalMaximum = 100;
            this.testPaneRightPanel.AutoScrollVerticalMinimum = 0;
            this.testPaneRightPanel.AutoScrollVPos = 0;
            this.testPaneRightPanel.AutoSizeElements = false;
            this.testPaneRightPanel.BackColor = System.Drawing.Color.Transparent;
            this.testPaneRightPanel.backgroundColor1 = System.Drawing.Color.Transparent;
            this.testPaneRightPanel.backgroundColor2 = System.Drawing.Color.Gray;
            this.testPaneRightPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
            this.testPaneRightPanel.BorderWidth = 1;
            this.testPaneRightPanel.Controls.Add(this.imageButton3);
            this.testPaneRightPanel.Controls.Add(this.imageButton2);
            this.testPaneRightPanel.Controls.Add(this.continueButton);
            this.testPaneRightPanel.DrawBackImage = false;
            this.testPaneRightPanel.EnableAutoScrollHorizontal = false;
            this.testPaneRightPanel.EnableAutoScrollVertical = false;
            this.testPaneRightPanel.Gradient = Soko.Common.Controls.NicePanel.GradientType.None;
            this.testPaneRightPanel.HorizontalMargin = 0;
            this.testPaneRightPanel.Location = new System.Drawing.Point(616, 29);
            this.testPaneRightPanel.Name = "testPaneRightPanel";
            this.testPaneRightPanel.roundingRadius = 10;
            this.testPaneRightPanel.Size = new System.Drawing.Size(104, 233);
            this.testPaneRightPanel.SupportTransparentBackground = false;
            this.testPaneRightPanel.TabIndex = 2;
            this.testPaneRightPanel.VerticalMargin = 0;
            this.testPaneRightPanel.VisibleAutoScrollHorizontal = false;
            this.testPaneRightPanel.VisibleAutoScrollVertical = false;
            // 
            // imageButton3
            // 
            this.imageButton3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.imageButton3.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.imageButton3.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.imageButton3.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.imageButton3.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.imageButton3.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.imageButton3.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.imageButton3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.imageButton3.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.imageButton3.BorderWidth = 1;
            this.imageButton3.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.imageButton3.ContentPadding = new System.Windows.Forms.Padding(10, 11, 10, 10);
            this.imageButton3.DrawBackColorOnFocus = false;
            this.imageButton3.DrawBackgroundImage = false;
            this.imageButton3.DrawBorderOnFocus = true;
            this.imageButton3.DrawBorderOnTop = false;
            this.imageButton3.Image = global::GST.Gearshift.Components.Properties.Resources.InitialTestPanel_navigateRight_64x64;
            this.imageButton3.ImageDisabled = global::GST.Gearshift.Components.Properties.Resources.InitialTestPanel_navigateRight_128x128;
            this.imageButton3.Location = new System.Drawing.Point(10, 0);
            this.imageButton3.Name = "imageButton3";
            this.imageButton3.Size = new System.Drawing.Size(84, 84);
            this.imageButton3.SupportTransparentBackground = false;
            this.imageButton3.TabIndex = 0;
            this.imageButton3.TextImageRelation = Soko.Common.Controls.TextRelation.Underneath;
            this.imageButton3.TextImageSpacing = 0;
            this.imageButton3.Click += new System.EventHandler(this.ProceedToFinalScreen);
            // 
            // imageButton2
            // 
            this.imageButton2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.imageButton2.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.imageButton2.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.imageButton2.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.imageButton2.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.imageButton2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.imageButton2.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.imageButton2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.imageButton2.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.imageButton2.BorderWidth = 1;
            this.imageButton2.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.imageButton2.ContentPadding = new System.Windows.Forms.Padding(10, 11, 10, 10);
            this.imageButton2.DrawBackColorOnFocus = false;
            this.imageButton2.DrawBackgroundImage = false;
            this.imageButton2.DrawBorderOnFocus = true;
            this.imageButton2.DrawBorderOnTop = false;
            this.imageButton2.Image = global::GST.Gearshift.Components.Properties.Resources.InitialTestPanel_navigateRight_64x64;
            this.imageButton2.ImageDisabled = global::GST.Gearshift.Components.Properties.Resources.InitialTestPanel_navigateRight_128x128;
            this.imageButton2.Location = new System.Drawing.Point(10, 84);
            this.imageButton2.Name = "imageButton2";
            this.imageButton2.Size = new System.Drawing.Size(84, 84);
            this.imageButton2.SupportTransparentBackground = false;
            this.imageButton2.TabIndex = 0;
            this.imageButton2.TextImageRelation = Soko.Common.Controls.TextRelation.Underneath;
            this.imageButton2.TextImageSpacing = 0;
            this.imageButton2.Click += new System.EventHandler(this.ProceedToFinalScreen);
            // 
            // continueButton
            // 
            this.continueButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.continueButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.continueButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.continueButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.continueButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.continueButton.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.continueButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.continueButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.continueButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.continueButton.BorderWidth = 1;
            this.continueButton.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.continueButton.ContentPadding = new System.Windows.Forms.Padding(10, 11, 10, 10);
            this.continueButton.DrawBackColorOnFocus = false;
            this.continueButton.DrawBackgroundImage = false;
            this.continueButton.DrawBorderOnFocus = true;
            this.continueButton.DrawBorderOnTop = false;
            this.continueButton.Image = global::GST.Gearshift.Components.Properties.Resources.InitialTestPanel_navigateRight_64x64;
            this.continueButton.ImageDisabled = global::GST.Gearshift.Components.Properties.Resources.InitialTestPanel_navigateRight_128x128;
            this.continueButton.Location = new System.Drawing.Point(10, 168);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(84, 84);
            this.continueButton.SupportTransparentBackground = false;
            this.continueButton.TabIndex = 0;
            this.continueButton.TextImageRelation = Soko.Common.Controls.TextRelation.Underneath;
            this.continueButton.TextImageSpacing = 0;
            this.continueButton.Click += new System.EventHandler(this.ProceedToFinalScreen);
            // 
            // initialTestDisplay1
            // 
            this.initialTestDisplay1.BackColor = System.Drawing.Color.Transparent;
            this.initialTestDisplay1.backgroundColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.initialTestDisplay1.backgroundColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.initialTestDisplay1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.initialTestDisplay1.Location = new System.Drawing.Point(86, 221);
            this.initialTestDisplay1.MaximumSize = new System.Drawing.Size(437, 60);
            this.initialTestDisplay1.MinimumSize = new System.Drawing.Size(437, 47);
            this.initialTestDisplay1.Name = "initialTestDisplay1";
            this.initialTestDisplay1.Size = new System.Drawing.Size(437, 47);
            this.initialTestDisplay1.TabIndex = 0;
            // 
            // ChannelsInitialTest
            // 
            this.Controls.Add(this.promptPane);
            this.Controls.Add(this.testPane);
            this.Controls.Add(this.resultPane);
            this.MaximumSize = new System.Drawing.Size(1000, 1000);
            this.MinimumSize = new System.Drawing.Size(734, 660);
            this.Name = "ChannelsInitialTest";
            this.Size = new System.Drawing.Size(734, 660);
            this.Load += new System.EventHandler(this.ReplacePanes);
            this.Move += new System.EventHandler(this.ReplacePanes);
            this.Resize += new System.EventHandler(this.ReplacePanes);
            this.promptPane.ResumeLayout(false);
            this.promptPane.PerformLayout();
            this.testPane.ResumeLayout(false);
            this.resultPane.ResumeLayout(false);
            this.nicePanel3.ResumeLayout(false);
            this.testPaneRightPanel.ResumeLayout(false);
            this.ResumeLayout(false);

    }

    #endregion

    private Soko.Common.Controls.NicePanel testPaneLeftpanel;
    private Soko.Common.Controls.NicePanel testPaneRightPanel;
    private Soko.Common.Controls.NiceButton continueButton;
    private GST.Gearshift.Components.Forms.DAQ.InitialTestDisplay initialTestDisplay1;
    private Soko.Common.Controls.ExplorerBarPane testPane;
    private Soko.Common.Controls.ExplorerBarPane resultPane;
    private Soko.Common.Controls.NiceButton resultPane_startAutomaticTestButton;
    private System.Windows.Forms.Label testPassLabel;
    private Soko.Common.Controls.NiceButton resultPane_abortTestButton;
    private System.Windows.Forms.Label infoLabel1;
    private Soko.Common.Controls.ExplorerBarPane promptPane;
    private Soko.Common.Controls.NiceTextBox serialNoTextBox;
    private Soko.Common.Controls.NiceTextBox commentTextBox;
    private System.Windows.Forms.Label serialNoLabel;
    private System.Windows.Forms.Label operatorNameLabel;
    private System.Windows.Forms.Label commentLabel;
    private Soko.Common.Controls.NiceButton promptContinueRepModeButton;
    private Soko.Common.Controls.NiceButton promptAbortTestButton;
    private System.Windows.Forms.Label label1;
    private Soko.Common.Controls.NiceButton resultPane_startLoopTestButton;
    private Soko.Common.Controls.NicePanel nicePanel3;
    private Soko.Common.Controls.NiceComboBox operatorNameComboBox;
    private System.Windows.Forms.Label label2;
    private Soko.Common.Controls.NiceTextBox testNameTextBox;
    private Soko.Common.Controls.NiceButton promptContinueDevModeButton;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label3;
    private Soko.Common.Controls.NiceButton imageButton3;
    private Soko.Common.Controls.NiceButton imageButton2;
    private Soko.Common.Controls.NiceButton resultPane_startManualTestButton;
    private System.Windows.Forms.Timer currentsRefreshTimer;
    private GST.ZF6.Components.Forms.Zf6InitPanel decoderInitPanel;
    private CAN.NissanRE5InitPanel nissanRE5InitPanel;
    private Soko.CanCave.Components.Forms.GM6TxxInitPanel gM6TxxInitPanel;

  }
}
