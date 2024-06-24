
namespace GST.Gearshift.Components.Forms.DAQ
{
  partial class GearboxConfigPanel
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
            this.MainPanel = new System.Windows.Forms.Panel();
            this.gearboxMainPanel1 = new Soko.Common.Controls.NicePanel();
            this.importConfigButton = new Soko.Common.Controls.NiceButton();
            this.gearboxFileSaveButton = new Soko.Common.Controls.NiceButton();
            this.gearboxFileNewButton = new Soko.Common.Controls.NiceButton();
            this.currentDisplayConfigPanel = new GST.Gearshift.Components.Forms.DAQ.CurrentDisplayConfigPanel();
            this.gearboxMainPanel2 = new Soko.Common.Controls.NicePanel();
            this.GbxTypeComboBox = new System.Windows.Forms.ComboBox();
            this.ignoreValueLessThanNUD = new System.Windows.Forms.NumericUpDown();
            this.ignoreValueLessThanLabel = new System.Windows.Forms.Label();
            this.pressureVariationToleranceNUD = new System.Windows.Forms.NumericUpDown();
            this.pressureVariationLabel = new System.Windows.Forms.Label();
            this.gearboxFrequencyNUD = new System.Windows.Forms.NumericUpDown();
            this.gearboxFrequencyLabel = new System.Windows.Forms.Label();
            this.gearboxPicturePictureBox = new System.Windows.Forms.PictureBox();
            this.GearboxModelTextbox = new System.Windows.Forms.TextBox();
            this.gearboxNameLabel = new System.Windows.Forms.Label();
            this.GearboxManufacturerTextbox = new System.Windows.Forms.TextBox();
            this.gearboxManufacturerLabel = new System.Windows.Forms.Label();
            this.gearboxModelLabel = new System.Windows.Forms.Label();
            this.gearboxNameTextbox = new System.Windows.Forms.TextBox();
            this.pressureDisplayConfigPanel = new GST.Gearshift.Components.Forms.DAQ.PressureDisplayConfigPanel();
            this.MainPanel.SuspendLayout();
            this.gearboxMainPanel1.SuspendLayout();
            this.gearboxMainPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ignoreValueLessThanNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pressureVariationToleranceNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gearboxFrequencyNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gearboxPicturePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.BackColor = System.Drawing.Color.Transparent;
            this.MainPanel.Controls.Add(this.gearboxMainPanel1);
            this.MainPanel.Controls.Add(this.currentDisplayConfigPanel);
            this.MainPanel.Controls.Add(this.gearboxMainPanel2);
            this.MainPanel.Controls.Add(this.pressureDisplayConfigPanel);
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Margin = new System.Windows.Forms.Padding(0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(751, 601);
            this.MainPanel.TabIndex = 8;
            // 
            // gearboxMainPanel1
            // 
            this.gearboxMainPanel1.AutoplaceElements = false;
            this.gearboxMainPanel1.AutoScrollHorizontalMaximum = 100;
            this.gearboxMainPanel1.AutoScrollHorizontalMinimum = 0;
            this.gearboxMainPanel1.AutoScrollHPos = 0;
            this.gearboxMainPanel1.AutoScrollVerticalMaximum = 100;
            this.gearboxMainPanel1.AutoScrollVerticalMinimum = 0;
            this.gearboxMainPanel1.AutoScrollVPos = 0;
            this.gearboxMainPanel1.AutoSizeElements = false;
            this.gearboxMainPanel1.BackColor = System.Drawing.Color.Transparent;
            this.gearboxMainPanel1.backgroundColor1 = System.Drawing.Color.DimGray;
            this.gearboxMainPanel1.backgroundColor2 = System.Drawing.Color.Silver;
            this.gearboxMainPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
            this.gearboxMainPanel1.BorderWidth = 1;
            this.gearboxMainPanel1.Controls.Add(this.importConfigButton);
            this.gearboxMainPanel1.Controls.Add(this.gearboxFileSaveButton);
            this.gearboxMainPanel1.Controls.Add(this.gearboxFileNewButton);
            this.gearboxMainPanel1.DrawBackImage = false;
            this.gearboxMainPanel1.EnableAutoScrollHorizontal = false;
            this.gearboxMainPanel1.EnableAutoScrollVertical = false;
            this.gearboxMainPanel1.Gradient = Soko.Common.Controls.NicePanel.GradientType.SigmaBellHorizontalUpper;
            this.gearboxMainPanel1.HorizontalMargin = 0;
            this.gearboxMainPanel1.Location = new System.Drawing.Point(1, 0);
            this.gearboxMainPanel1.Name = "gearboxMainPanel1";
            this.gearboxMainPanel1.roundingRadius = 10;
            this.gearboxMainPanel1.Size = new System.Drawing.Size(228, 188);
            this.gearboxMainPanel1.SupportTransparentBackground = false;
            this.gearboxMainPanel1.TabIndex = 5;
            this.gearboxMainPanel1.VerticalMargin = 0;
            this.gearboxMainPanel1.VisibleAutoScrollHorizontal = false;
            this.gearboxMainPanel1.VisibleAutoScrollVertical = false;
            // 
            // importConfigButton
            // 
            this.importConfigButton.AutoSize = true;
            this.importConfigButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.importConfigButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.importConfigButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.importConfigButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.importConfigButton.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.importConfigButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.importConfigButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.importConfigButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.importConfigButton.BorderWidth = 10;
            this.importConfigButton.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.importConfigButton.ContentPadding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.importConfigButton.DrawBackColorOnFocus = false;
            this.importConfigButton.DrawBackgroundImage = false;
            this.importConfigButton.DrawBorderOnFocus = false;
            this.importConfigButton.DrawBorderOnTop = false;
            this.importConfigButton.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.importConfigButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.importConfigButton.Image = global::GST.Gearshift.Components.Properties.Resources.GearboxConfigPanel_Import_48;
            this.importConfigButton.ImageDisabled = global::GST.Gearshift.Components.Properties.Resources.GearboxConfigPanel_Import_48;
            this.importConfigButton.Location = new System.Drawing.Point(6, 124);
            this.importConfigButton.Name = "importConfigButton";
            this.importConfigButton.Size = new System.Drawing.Size(217, 52);
            this.importConfigButton.SupportTransparentBackground = false;
            this.importConfigButton.TabIndex = 34;
            this.importConfigButton.Text = "Import config from disk";
            this.importConfigButton.TextImageRelation = Soko.Common.Controls.TextRelation.OnRight;
            this.importConfigButton.TextImageSpacing = 1;
            this.importConfigButton.Click += new System.EventHandler(this.importConfigButton_Click);
            // 
            // gearboxFileSaveButton
            // 
            this.gearboxFileSaveButton.AutoSize = true;
            this.gearboxFileSaveButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gearboxFileSaveButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gearboxFileSaveButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gearboxFileSaveButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gearboxFileSaveButton.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.gearboxFileSaveButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.gearboxFileSaveButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.gearboxFileSaveButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.gearboxFileSaveButton.BorderWidth = 10;
            this.gearboxFileSaveButton.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.gearboxFileSaveButton.ContentPadding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.gearboxFileSaveButton.DrawBackColorOnFocus = false;
            this.gearboxFileSaveButton.DrawBackgroundImage = false;
            this.gearboxFileSaveButton.DrawBorderOnFocus = false;
            this.gearboxFileSaveButton.DrawBorderOnTop = false;
            this.gearboxFileSaveButton.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gearboxFileSaveButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.gearboxFileSaveButton.Image = global::GST.Gearshift.Components.Properties.Resources.GearboxConfigPanel_Export_48;
            this.gearboxFileSaveButton.ImageDisabled = global::GST.Gearshift.Components.Properties.Resources.GearboxConfigPanel_Export_48;
            this.gearboxFileSaveButton.Location = new System.Drawing.Point(6, 66);
            this.gearboxFileSaveButton.Name = "gearboxFileSaveButton";
            this.gearboxFileSaveButton.Size = new System.Drawing.Size(217, 52);
            this.gearboxFileSaveButton.SupportTransparentBackground = false;
            this.gearboxFileSaveButton.TabIndex = 31;
            this.gearboxFileSaveButton.Text = "Export current config to disk";
            this.gearboxFileSaveButton.TextImageRelation = Soko.Common.Controls.TextRelation.OnRight;
            this.gearboxFileSaveButton.TextImageSpacing = 1;
            this.gearboxFileSaveButton.Click += new System.EventHandler(this.gearboxFileSaveButton_Click);
            // 
            // gearboxFileNewButton
            // 
            this.gearboxFileNewButton.AutoSize = true;
            this.gearboxFileNewButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gearboxFileNewButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gearboxFileNewButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gearboxFileNewButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gearboxFileNewButton.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.gearboxFileNewButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.gearboxFileNewButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.gearboxFileNewButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.gearboxFileNewButton.BorderWidth = 10;
            this.gearboxFileNewButton.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.gearboxFileNewButton.ContentPadding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.gearboxFileNewButton.DrawBackColorOnFocus = false;
            this.gearboxFileNewButton.DrawBackgroundImage = false;
            this.gearboxFileNewButton.DrawBorderOnFocus = false;
            this.gearboxFileNewButton.DrawBorderOnTop = false;
            this.gearboxFileNewButton.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gearboxFileNewButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.gearboxFileNewButton.Image = global::GST.Gearshift.Components.Properties.Resources.GearboxConfigPanel_EmptyPage_48;
            this.gearboxFileNewButton.ImageDisabled = global::GST.Gearshift.Components.Properties.Resources.GearboxConfigPanel_EmptyPage_48;
            this.gearboxFileNewButton.Location = new System.Drawing.Point(6, 8);
            this.gearboxFileNewButton.Name = "gearboxFileNewButton";
            this.gearboxFileNewButton.Size = new System.Drawing.Size(217, 52);
            this.gearboxFileNewButton.SupportTransparentBackground = false;
            this.gearboxFileNewButton.TabIndex = 31;
            this.gearboxFileNewButton.Text = "Erase current configuration";
            this.gearboxFileNewButton.TextImageRelation = Soko.Common.Controls.TextRelation.OnRight;
            this.gearboxFileNewButton.TextImageSpacing = 1;
            this.gearboxFileNewButton.Click += new System.EventHandler(this.gearboxFileNewButton_Click);
            // 
            // currentDisplayConfigPanel
            // 
            this.currentDisplayConfigPanel.BackColor = System.Drawing.Color.Transparent;
            this.currentDisplayConfigPanel.BackgroundColor = System.Drawing.Color.Transparent;
            this.currentDisplayConfigPanel.Location = new System.Drawing.Point(1, 399);
            this.currentDisplayConfigPanel.Name = "currentDisplayConfigPanel";
            this.currentDisplayConfigPanel.PanelsColor1 = System.Drawing.Color.DimGray;
            this.currentDisplayConfigPanel.PanelsColor2 = System.Drawing.Color.Silver;
            this.currentDisplayConfigPanel.Size = new System.Drawing.Size(748, 198);
            this.currentDisplayConfigPanel.TabIndex = 7;
            // 
            // gearboxMainPanel2
            // 
            this.gearboxMainPanel2.AutoplaceElements = false;
            this.gearboxMainPanel2.AutoScrollHorizontalMaximum = 100;
            this.gearboxMainPanel2.AutoScrollHorizontalMinimum = 0;
            this.gearboxMainPanel2.AutoScrollHPos = 0;
            this.gearboxMainPanel2.AutoScrollVerticalMaximum = 100;
            this.gearboxMainPanel2.AutoScrollVerticalMinimum = 0;
            this.gearboxMainPanel2.AutoScrollVPos = 0;
            this.gearboxMainPanel2.AutoSizeElements = false;
            this.gearboxMainPanel2.BackColor = System.Drawing.Color.Transparent;
            this.gearboxMainPanel2.backgroundColor1 = System.Drawing.Color.DimGray;
            this.gearboxMainPanel2.backgroundColor2 = System.Drawing.Color.Silver;
            this.gearboxMainPanel2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
            this.gearboxMainPanel2.BorderWidth = 1;
            this.gearboxMainPanel2.Controls.Add(this.GbxTypeComboBox);
            this.gearboxMainPanel2.Controls.Add(this.ignoreValueLessThanNUD);
            this.gearboxMainPanel2.Controls.Add(this.ignoreValueLessThanLabel);
            this.gearboxMainPanel2.Controls.Add(this.pressureVariationToleranceNUD);
            this.gearboxMainPanel2.Controls.Add(this.pressureVariationLabel);
            this.gearboxMainPanel2.Controls.Add(this.gearboxFrequencyNUD);
            this.gearboxMainPanel2.Controls.Add(this.gearboxFrequencyLabel);
            this.gearboxMainPanel2.Controls.Add(this.gearboxPicturePictureBox);
            this.gearboxMainPanel2.Controls.Add(this.GearboxModelTextbox);
            this.gearboxMainPanel2.Controls.Add(this.gearboxNameLabel);
            this.gearboxMainPanel2.Controls.Add(this.GearboxManufacturerTextbox);
            this.gearboxMainPanel2.Controls.Add(this.gearboxManufacturerLabel);
            this.gearboxMainPanel2.Controls.Add(this.gearboxModelLabel);
            this.gearboxMainPanel2.Controls.Add(this.gearboxNameTextbox);
            this.gearboxMainPanel2.DrawBackImage = false;
            this.gearboxMainPanel2.EnableAutoScrollHorizontal = false;
            this.gearboxMainPanel2.EnableAutoScrollVertical = false;
            this.gearboxMainPanel2.Gradient = Soko.Common.Controls.NicePanel.GradientType.SigmaBellHorizontalUpper;
            this.gearboxMainPanel2.HorizontalMargin = 0;
            this.gearboxMainPanel2.Location = new System.Drawing.Point(230, 0);
            this.gearboxMainPanel2.Name = "gearboxMainPanel2";
            this.gearboxMainPanel2.roundingRadius = 10;
            this.gearboxMainPanel2.Size = new System.Drawing.Size(519, 188);
            this.gearboxMainPanel2.SupportTransparentBackground = false;
            this.gearboxMainPanel2.TabIndex = 3;
            this.gearboxMainPanel2.VerticalMargin = 0;
            this.gearboxMainPanel2.VisibleAutoScrollHorizontal = false;
            this.gearboxMainPanel2.VisibleAutoScrollVertical = false;
            // 
            // GbxTypeComboBox
            // 
            this.GbxTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GbxTypeComboBox.FormattingEnabled = true;
            this.GbxTypeComboBox.Location = new System.Drawing.Point(260, 20);
            this.GbxTypeComboBox.Name = "GbxTypeComboBox";
            this.GbxTypeComboBox.Size = new System.Drawing.Size(250, 21);
            this.GbxTypeComboBox.TabIndex = 13;
            this.GbxTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.GbxTypeComboBox_SelectedIndexChanged);
            // 
            // ignoreValueLessThanNUD
            // 
            this.ignoreValueLessThanNUD.DecimalPlaces = 2;
            this.ignoreValueLessThanNUD.Location = new System.Drawing.Point(176, 149);
            this.ignoreValueLessThanNUD.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.ignoreValueLessThanNUD.Name = "ignoreValueLessThanNUD";
            this.ignoreValueLessThanNUD.Size = new System.Drawing.Size(78, 20);
            this.ignoreValueLessThanNUD.TabIndex = 12;
            this.ignoreValueLessThanNUD.ValueChanged += new System.EventHandler(this.ignoreValueLessThanNUD_ValueChanged);
            // 
            // ignoreValueLessThanLabel
            // 
            this.ignoreValueLessThanLabel.AutoSize = true;
            this.ignoreValueLessThanLabel.BackColor = System.Drawing.Color.Transparent;
            this.ignoreValueLessThanLabel.Location = new System.Drawing.Point(11, 151);
            this.ignoreValueLessThanLabel.Name = "ignoreValueLessThanLabel";
            this.ignoreValueLessThanLabel.Size = new System.Drawing.Size(136, 13);
            this.ignoreValueLessThanLabel.TabIndex = 11;
            this.ignoreValueLessThanLabel.Text = "Ignore pressure below [Bar]";
            // 
            // pressureVariationToleranceNUD
            // 
            this.pressureVariationToleranceNUD.Location = new System.Drawing.Point(176, 123);
            this.pressureVariationToleranceNUD.Name = "pressureVariationToleranceNUD";
            this.pressureVariationToleranceNUD.Size = new System.Drawing.Size(78, 20);
            this.pressureVariationToleranceNUD.TabIndex = 10;
            this.pressureVariationToleranceNUD.ValueChanged += new System.EventHandler(this.pressureVariationToleranceNUD_ValueChanged);
            // 
            // pressureVariationLabel
            // 
            this.pressureVariationLabel.AutoSize = true;
            this.pressureVariationLabel.BackColor = System.Drawing.Color.Transparent;
            this.pressureVariationLabel.Location = new System.Drawing.Point(11, 125);
            this.pressureVariationLabel.Name = "pressureVariationLabel";
            this.pressureVariationLabel.Size = new System.Drawing.Size(155, 13);
            this.pressureVariationLabel.TabIndex = 8;
            this.pressureVariationLabel.Text = "Pressure variation tolerance [%]";
            // 
            // gearboxFrequencyNUD
            // 
            this.gearboxFrequencyNUD.Location = new System.Drawing.Point(88, 97);
            this.gearboxFrequencyNUD.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.gearboxFrequencyNUD.Name = "gearboxFrequencyNUD";
            this.gearboxFrequencyNUD.Size = new System.Drawing.Size(166, 20);
            this.gearboxFrequencyNUD.TabIndex = 10;
            this.gearboxFrequencyNUD.ValueChanged += new System.EventHandler(this.gearboxFrequencyNUD_ValueChanged);
            // 
            // gearboxFrequencyLabel
            // 
            this.gearboxFrequencyLabel.AutoSize = true;
            this.gearboxFrequencyLabel.BackColor = System.Drawing.Color.Transparent;
            this.gearboxFrequencyLabel.Location = new System.Drawing.Point(11, 99);
            this.gearboxFrequencyLabel.Name = "gearboxFrequencyLabel";
            this.gearboxFrequencyLabel.Size = new System.Drawing.Size(60, 13);
            this.gearboxFrequencyLabel.TabIndex = 8;
            this.gearboxFrequencyLabel.Text = "Frequency:";
            // 
            // gearboxPicturePictureBox
            // 
            this.gearboxPicturePictureBox.BackColor = System.Drawing.Color.Transparent;
            this.gearboxPicturePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gearboxPicturePictureBox.Location = new System.Drawing.Point(260, 55);
            this.gearboxPicturePictureBox.Name = "gearboxPicturePictureBox";
            this.gearboxPicturePictureBox.Size = new System.Drawing.Size(250, 114);
            this.gearboxPicturePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.gearboxPicturePictureBox.TabIndex = 5;
            this.gearboxPicturePictureBox.TabStop = false;
            this.gearboxPicturePictureBox.Click += new System.EventHandler(this.gearboxPicturePictureBox_Click);
            // 
            // GearboxModelTextbox
            // 
            this.GearboxModelTextbox.Location = new System.Drawing.Point(88, 71);
            this.GearboxModelTextbox.Name = "GearboxModelTextbox";
            this.GearboxModelTextbox.Size = new System.Drawing.Size(166, 20);
            this.GearboxModelTextbox.TabIndex = 7;
            this.GearboxModelTextbox.TextChanged += new System.EventHandler(this.GearboxModelTextbox_TextChanged);
            // 
            // gearboxNameLabel
            // 
            this.gearboxNameLabel.AutoSize = true;
            this.gearboxNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.gearboxNameLabel.Location = new System.Drawing.Point(11, 23);
            this.gearboxNameLabel.Name = "gearboxNameLabel";
            this.gearboxNameLabel.Size = new System.Drawing.Size(38, 13);
            this.gearboxNameLabel.TabIndex = 0;
            this.gearboxNameLabel.Text = "Name:";
            // 
            // GearboxManufacturerTextbox
            // 
            this.GearboxManufacturerTextbox.Location = new System.Drawing.Point(88, 46);
            this.GearboxManufacturerTextbox.Name = "GearboxManufacturerTextbox";
            this.GearboxManufacturerTextbox.Size = new System.Drawing.Size(166, 20);
            this.GearboxManufacturerTextbox.TabIndex = 6;
            this.GearboxManufacturerTextbox.TextChanged += new System.EventHandler(this.GearboxManufacturerTextbox_TextChanged);
            // 
            // gearboxManufacturerLabel
            // 
            this.gearboxManufacturerLabel.AutoSize = true;
            this.gearboxManufacturerLabel.BackColor = System.Drawing.Color.Transparent;
            this.gearboxManufacturerLabel.Location = new System.Drawing.Point(11, 49);
            this.gearboxManufacturerLabel.Name = "gearboxManufacturerLabel";
            this.gearboxManufacturerLabel.Size = new System.Drawing.Size(73, 13);
            this.gearboxManufacturerLabel.TabIndex = 1;
            this.gearboxManufacturerLabel.Text = "Manufacturer:";
            // 
            // gearboxModelLabel
            // 
            this.gearboxModelLabel.AutoSize = true;
            this.gearboxModelLabel.BackColor = System.Drawing.Color.Transparent;
            this.gearboxModelLabel.Location = new System.Drawing.Point(11, 74);
            this.gearboxModelLabel.Name = "gearboxModelLabel";
            this.gearboxModelLabel.Size = new System.Drawing.Size(39, 13);
            this.gearboxModelLabel.TabIndex = 2;
            this.gearboxModelLabel.Text = "Model:";
            // 
            // gearboxNameTextbox
            // 
            this.gearboxNameTextbox.Location = new System.Drawing.Point(88, 20);
            this.gearboxNameTextbox.Name = "gearboxNameTextbox";
            this.gearboxNameTextbox.Size = new System.Drawing.Size(166, 20);
            this.gearboxNameTextbox.TabIndex = 4;
            this.gearboxNameTextbox.TextChanged += new System.EventHandler(this.gearboxNameTextbox_TextChanged);
            // 
            // pressureDisplayConfigPanel
            // 
            this.pressureDisplayConfigPanel.BackColor = System.Drawing.Color.Transparent;
            this.pressureDisplayConfigPanel.BackgroundColor = System.Drawing.Color.Transparent;
            this.pressureDisplayConfigPanel.Location = new System.Drawing.Point(1, 194);
            this.pressureDisplayConfigPanel.Name = "pressureDisplayConfigPanel";
            this.pressureDisplayConfigPanel.PanelsColor1 = System.Drawing.Color.DimGray;
            this.pressureDisplayConfigPanel.PanelsColor2 = System.Drawing.Color.Silver;
            this.pressureDisplayConfigPanel.Size = new System.Drawing.Size(748, 201);
            this.pressureDisplayConfigPanel.TabIndex = 6;
            // 
            // GearboxConfigPanel
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.MainPanel);
            this.MinimumSize = new System.Drawing.Size(751, 502);
            this.Name = "GearboxConfigPanel";
            this.Size = new System.Drawing.Size(761, 610);
            this.Load += new System.EventHandler(this.GearboxConfigPanel_Load);
            this.Resize += new System.EventHandler(this.GearboxConfigPanel_Resize);
            this.MainPanel.ResumeLayout(false);
            this.gearboxMainPanel1.ResumeLayout(false);
            this.gearboxMainPanel1.PerformLayout();
            this.gearboxMainPanel2.ResumeLayout(false);
            this.gearboxMainPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ignoreValueLessThanNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pressureVariationToleranceNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gearboxFrequencyNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gearboxPicturePictureBox)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    GST.Gearshift.Components.Forms.DAQ.PressureDisplayConfigPanel pressureDisplayConfigPanel;


    private System.Windows.Forms.TextBox gearboxNameTextbox;
    private System.Windows.Forms.Label gearboxModelLabel;
    private System.Windows.Forms.Label gearboxManufacturerLabel;
    private System.Windows.Forms.Label gearboxNameLabel;
    private System.Windows.Forms.TextBox GearboxModelTextbox;
    private System.Windows.Forms.TextBox GearboxManufacturerTextbox;
    private System.Windows.Forms.PictureBox gearboxPicturePictureBox;
    private Soko.Common.Controls.NicePanel gearboxMainPanel2;
    private Soko.Common.Controls.NicePanel gearboxMainPanel1;
    private Soko.Common.Controls.NiceButton gearboxFileSaveButton;
    private Soko.Common.Controls.NiceButton gearboxFileNewButton;
    private System.Windows.Forms.Panel MainPanel;
    private System.Windows.Forms.Label gearboxFrequencyLabel;
    private GST.Gearshift.Components.Forms.DAQ.CurrentDisplayConfigPanel currentDisplayConfigPanel;
    private Soko.Common.Controls.NiceButton importConfigButton;
    private System.Windows.Forms.NumericUpDown gearboxFrequencyNUD;
    private System.Windows.Forms.NumericUpDown pressureVariationToleranceNUD;
    private System.Windows.Forms.Label pressureVariationLabel;
    private System.Windows.Forms.NumericUpDown ignoreValueLessThanNUD;
    private System.Windows.Forms.Label ignoreValueLessThanLabel;
    private System.Windows.Forms.ComboBox GbxTypeComboBox;

  }
}

