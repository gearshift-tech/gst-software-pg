namespace GST.Gearshift.Components.Forms.DAQ
{
    partial class TestScriptEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestScriptEditor));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            XPTable.Models.DataSourceColumnBinder dataSourceColumnBinder1 = new XPTable.Models.DataSourceColumnBinder();
            XPTable.Renderers.DragDropRenderer dragDropRenderer1 = new XPTable.Renderers.DragDropRenderer();
            XPTable.Models.DataSourceColumnBinder dataSourceColumnBinder2 = new XPTable.Models.DataSourceColumnBinder();
            XPTable.Renderers.DragDropRenderer dragDropRenderer2 = new XPTable.Renderers.DragDropRenderer();
            this.tabControl = new Syncfusion.Windows.Forms.Tools.TabControlAdv();
            this.tabPage1 = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.dataPanel = new Soko.Common.Controls.NicePanel();
            this.cutButton = new Soko.Common.Controls.NiceButton();
            this.pasteButton = new Soko.Common.Controls.NiceButton();
            this.copyButton = new Soko.Common.Controls.NiceButton();
            this.selectLoopSectionButton = new Soko.Common.Controls.NiceButton();
            this.moveSelectionDownButton = new Soko.Common.Controls.NiceButton();
            this.moveSelectionUpButton = new Soko.Common.Controls.NiceButton();
            this.removeScriptRowButton = new Soko.Common.Controls.NiceButton();
            this.insertScriptRowButton = new Soko.Common.Controls.NiceButton();
            this.scriptDGV = new System.Windows.Forms.DataGridView();
            this.imageButton6 = new Soko.Common.Controls.NiceButton();
            this.tabPage2 = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.passFailTable = new XPTable.Models.Table();
            this.tabPage3 = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.promptsTable = new XPTable.Models.Table();
            this.mainTabControl = new Syncfusion.Windows.Forms.Tools.TabControlAdv();
            this.testScriptTabPage = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.nicePanel2 = new Soko.Common.Controls.NicePanel();
            this.TSFilesComboBox = new System.Windows.Forms.ComboBox();
            this.gearboxGearboxLabel = new System.Windows.Forms.Label();
            this.scriptFileNewButton = new Soko.Common.Controls.NiceButton();
            this.fileManagementPanel = new Soko.Common.Controls.NicePanel();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.deleteFileButton = new Soko.Common.Controls.NiceButton();
            this.scriptFileSaveButton = new Soko.Common.Controls.NiceButton();
            this.nicePanel1 = new Soko.Common.Controls.NicePanel();
            this.TSMasterDataComboBox = new System.Windows.Forms.ComboBox();
            this.imageButton3 = new Soko.Common.Controls.NiceButton();
            this.label4 = new System.Windows.Forms.Label();
            this.gearboxMainPanel2 = new Soko.Common.Controls.NicePanel();
            this.label1 = new System.Windows.Forms.Label();
            this.gearboxPicturePictureBox = new System.Windows.Forms.PictureBox();
            this.gearboxModelLabelVar = new System.Windows.Forms.Label();
            this.gearboxNameLabel = new System.Windows.Forms.Label();
            this.gearboxManufacturerLabelVar = new System.Windows.Forms.Label();
            this.gearboxManufacturerLabel = new System.Windows.Forms.Label();
            this.gearboxModelLabel = new System.Windows.Forms.Label();
            this.gearboxNameLabelVar = new System.Windows.Forms.Label();
            this.gearboxPropsTabPage = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.gearboxConfigPanel = new GST.Gearshift.Components.Forms.DAQ.GearboxConfigPanel();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.dataPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scriptDGV)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.passFailTable)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.promptsTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainTabControl)).BeginInit();
            this.mainTabControl.SuspendLayout();
            this.testScriptTabPage.SuspendLayout();
            this.nicePanel2.SuspendLayout();
            this.fileManagementPanel.SuspendLayout();
            this.nicePanel1.SuspendLayout();
            this.gearboxMainPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gearboxPicturePictureBox)).BeginInit();
            this.gearboxPropsTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.ActiveTabForeColor = System.Drawing.Color.Empty;
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.BackColor = System.Drawing.Color.Silver;
            this.tabControl.BeforeTouchSize = new System.Drawing.Size(1356, 662);
            this.tabControl.CloseButtonForeColor = System.Drawing.Color.Empty;
            this.tabControl.CloseButtonHoverForeColor = System.Drawing.Color.Empty;
            this.tabControl.CloseButtonPressedForeColor = System.Drawing.Color.Empty;
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.InActiveTabForeColor = System.Drawing.Color.Empty;
            this.tabControl.Location = new System.Drawing.Point(0, 132);
            this.tabControl.Name = "tabControl";
            this.tabControl.SeparatorColor = System.Drawing.SystemColors.ControlDark;
            this.tabControl.ShowSeparator = false;
            this.tabControl.Size = new System.Drawing.Size(1356, 662);
            this.tabControl.TabIndex = 31;
            this.tabControl.TabStyle = typeof(Syncfusion.Windows.Forms.Tools.TabRendererDockingWhidbeyBeta);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Silver;
            this.tabPage1.Controls.Add(this.dataPanel);
            this.tabPage1.Image = null;
            this.tabPage1.ImageSize = new System.Drawing.Size(16, 16);
            this.tabPage1.Location = new System.Drawing.Point(1, 30);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.ShowCloseButton = true;
            this.tabPage1.Size = new System.Drawing.Size(1353, 630);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "Drive values";
            this.tabPage1.ThemesEnabled = false;
            // 
            // dataPanel
            // 
            this.dataPanel.AutoplaceElements = false;
            this.dataPanel.AutoScrollHorizontalMaximum = 100;
            this.dataPanel.AutoScrollHorizontalMinimum = 0;
            this.dataPanel.AutoScrollHPos = 0;
            this.dataPanel.AutoScrollVerticalMaximum = 100;
            this.dataPanel.AutoScrollVerticalMinimum = 0;
            this.dataPanel.AutoScrollVPos = 0;
            this.dataPanel.AutoSizeElements = false;
            this.dataPanel.backgroundColor1 = System.Drawing.Color.DarkGray;
            this.dataPanel.backgroundColor2 = System.Drawing.Color.Gainsboro;
            this.dataPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
            this.dataPanel.BorderWidth = 1;
            this.dataPanel.Controls.Add(this.cutButton);
            this.dataPanel.Controls.Add(this.pasteButton);
            this.dataPanel.Controls.Add(this.copyButton);
            this.dataPanel.Controls.Add(this.selectLoopSectionButton);
            this.dataPanel.Controls.Add(this.moveSelectionDownButton);
            this.dataPanel.Controls.Add(this.moveSelectionUpButton);
            this.dataPanel.Controls.Add(this.removeScriptRowButton);
            this.dataPanel.Controls.Add(this.insertScriptRowButton);
            this.dataPanel.Controls.Add(this.scriptDGV);
            this.dataPanel.Controls.Add(this.imageButton6);
            this.dataPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataPanel.DrawBackImage = false;
            this.dataPanel.EnableAutoScrollHorizontal = false;
            this.dataPanel.EnableAutoScrollVertical = false;
            this.dataPanel.Gradient = Soko.Common.Controls.NicePanel.GradientType.HorizontalEdges;
            this.dataPanel.HorizontalMargin = 0;
            this.dataPanel.Location = new System.Drawing.Point(0, 0);
            this.dataPanel.Name = "dataPanel";
            this.dataPanel.roundingRadius = 10;
            this.dataPanel.Size = new System.Drawing.Size(1353, 630);
            this.dataPanel.SupportTransparentBackground = false;
            this.dataPanel.TabIndex = 0;
            this.dataPanel.VerticalMargin = 0;
            this.dataPanel.VisibleAutoScrollHorizontal = false;
            this.dataPanel.VisibleAutoScrollVertical = false;
            // 
            // cutButton
            // 
            this.cutButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cutButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cutButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cutButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cutButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cutButton.BackgroundColor = System.Drawing.Color.DarkGray;
            this.cutButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.cutButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.cutButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.cutButton.BorderWidth = 1;
            this.cutButton.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.cutButton.ContentPadding = new System.Windows.Forms.Padding(-1, 2, 0, 0);
            this.cutButton.DrawBackColorOnFocus = false;
            this.cutButton.DrawBackgroundImage = false;
            this.cutButton.DrawBorderOnFocus = false;
            this.cutButton.DrawBorderOnTop = false;
            this.cutButton.Image = global::GST.Gearshift.Components.Properties.Resources.TestScriptEditorPanel_Cut_32x32;
            this.cutButton.ImageDisabled = global::GST.Gearshift.Components.Properties.Resources.TestScriptEditorPanel_Cut_32x32;
            this.cutButton.Location = new System.Drawing.Point(1312, 299);
            this.cutButton.Name = "cutButton";
            this.cutButton.Size = new System.Drawing.Size(38, 38);
            this.cutButton.SupportTransparentBackground = false;
            this.cutButton.TabIndex = 39;
            this.cutButton.TextImageRelation = Soko.Common.Controls.TextRelation.Underneath;
            this.cutButton.TextImageSpacing = 0;
            this.cutButton.Click += new System.EventHandler(this.cutButton_Click);
            // 
            // pasteButton
            // 
            this.pasteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pasteButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pasteButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pasteButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pasteButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pasteButton.BackgroundColor = System.Drawing.Color.DarkGray;
            this.pasteButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.pasteButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.pasteButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.pasteButton.BorderWidth = 1;
            this.pasteButton.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.pasteButton.ContentPadding = new System.Windows.Forms.Padding(-1, 2, 0, 0);
            this.pasteButton.DrawBackColorOnFocus = false;
            this.pasteButton.DrawBackgroundImage = false;
            this.pasteButton.DrawBorderOnFocus = false;
            this.pasteButton.DrawBorderOnTop = false;
            this.pasteButton.Image = global::GST.Gearshift.Components.Properties.Resources.TestScriptEditorPanel_paste_32x32;
            this.pasteButton.ImageDisabled = global::GST.Gearshift.Components.Properties.Resources.TestScriptEditorPanel_paste_32x32;
            this.pasteButton.Location = new System.Drawing.Point(1311, 255);
            this.pasteButton.Name = "pasteButton";
            this.pasteButton.Size = new System.Drawing.Size(38, 38);
            this.pasteButton.SupportTransparentBackground = false;
            this.pasteButton.TabIndex = 38;
            this.pasteButton.TextImageRelation = Soko.Common.Controls.TextRelation.Underneath;
            this.pasteButton.TextImageSpacing = 0;
            this.pasteButton.Click += new System.EventHandler(this.pasteButton_Click);
            // 
            // copyButton
            // 
            this.copyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.copyButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.copyButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.copyButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.copyButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.copyButton.BackgroundColor = System.Drawing.Color.DarkGray;
            this.copyButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.copyButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.copyButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.copyButton.BorderWidth = 1;
            this.copyButton.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.copyButton.ContentPadding = new System.Windows.Forms.Padding(-1, 2, 0, 0);
            this.copyButton.DrawBackColorOnFocus = false;
            this.copyButton.DrawBackgroundImage = false;
            this.copyButton.DrawBorderOnFocus = false;
            this.copyButton.DrawBorderOnTop = false;
            this.copyButton.Image = global::GST.Gearshift.Components.Properties.Resources.TestScriptEditorPanel_copy_32x32;
            this.copyButton.ImageDisabled = global::GST.Gearshift.Components.Properties.Resources.TestScriptEditorPanel_copy_32x32;
            this.copyButton.Location = new System.Drawing.Point(1311, 211);
            this.copyButton.Name = "copyButton";
            this.copyButton.Size = new System.Drawing.Size(38, 38);
            this.copyButton.SupportTransparentBackground = false;
            this.copyButton.TabIndex = 37;
            this.copyButton.TextImageRelation = Soko.Common.Controls.TextRelation.Underneath;
            this.copyButton.TextImageSpacing = 0;
            this.copyButton.Click += new System.EventHandler(this.copyButton_Click);
            // 
            // selectLoopSectionButton
            // 
            this.selectLoopSectionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectLoopSectionButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.selectLoopSectionButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.selectLoopSectionButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.selectLoopSectionButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.selectLoopSectionButton.BackgroundColor = System.Drawing.Color.DarkGray;
            this.selectLoopSectionButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.selectLoopSectionButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.selectLoopSectionButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.selectLoopSectionButton.BorderWidth = 1;
            this.selectLoopSectionButton.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.selectLoopSectionButton.ContentPadding = new System.Windows.Forms.Padding(-1, 2, 0, 0);
            this.selectLoopSectionButton.DrawBackColorOnFocus = false;
            this.selectLoopSectionButton.DrawBackgroundImage = false;
            this.selectLoopSectionButton.DrawBorderOnFocus = false;
            this.selectLoopSectionButton.DrawBorderOnTop = false;
            this.selectLoopSectionButton.Image = global::GST.Gearshift.Components.Properties.Resources.TestScriptEditorPanel_Refresh_32x32;
            this.selectLoopSectionButton.ImageDisabled = global::GST.Gearshift.Components.Properties.Resources.TestScriptEditorPanel_Refresh_32x32;
            this.selectLoopSectionButton.Location = new System.Drawing.Point(1311, 343);
            this.selectLoopSectionButton.Name = "selectLoopSectionButton";
            this.selectLoopSectionButton.Size = new System.Drawing.Size(38, 38);
            this.selectLoopSectionButton.SupportTransparentBackground = false;
            this.selectLoopSectionButton.TabIndex = 36;
            this.selectLoopSectionButton.TextImageRelation = Soko.Common.Controls.TextRelation.Underneath;
            this.selectLoopSectionButton.TextImageSpacing = 0;
            this.selectLoopSectionButton.Click += new System.EventHandler(this.selectLoopSectionButton_Click);
            // 
            // moveSelectionDownButton
            // 
            this.moveSelectionDownButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.moveSelectionDownButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.moveSelectionDownButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.moveSelectionDownButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.moveSelectionDownButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.moveSelectionDownButton.BackgroundColor = System.Drawing.Color.DarkGray;
            this.moveSelectionDownButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.moveSelectionDownButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.moveSelectionDownButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.moveSelectionDownButton.BorderWidth = 1;
            this.moveSelectionDownButton.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.moveSelectionDownButton.ContentPadding = new System.Windows.Forms.Padding(-1, 2, 0, 0);
            this.moveSelectionDownButton.DrawBackColorOnFocus = false;
            this.moveSelectionDownButton.DrawBackgroundImage = false;
            this.moveSelectionDownButton.DrawBorderOnFocus = false;
            this.moveSelectionDownButton.DrawBorderOnTop = false;
            this.moveSelectionDownButton.Image = global::GST.Gearshift.Components.Properties.Resources.TestScriptEditorPanel_Down_32x32;
            this.moveSelectionDownButton.ImageDisabled = global::GST.Gearshift.Components.Properties.Resources.TestScriptEditorPanel_Down_32x32_BW;
            this.moveSelectionDownButton.Location = new System.Drawing.Point(1312, 146);
            this.moveSelectionDownButton.Name = "moveSelectionDownButton";
            this.moveSelectionDownButton.Size = new System.Drawing.Size(38, 38);
            this.moveSelectionDownButton.SupportTransparentBackground = false;
            this.moveSelectionDownButton.TabIndex = 34;
            this.moveSelectionDownButton.TextImageRelation = Soko.Common.Controls.TextRelation.Underneath;
            this.moveSelectionDownButton.TextImageSpacing = 0;
            this.moveSelectionDownButton.Click += new System.EventHandler(this.ScriptDGV_MoveSelectionDown);
            // 
            // moveSelectionUpButton
            // 
            this.moveSelectionUpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.moveSelectionUpButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.moveSelectionUpButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.moveSelectionUpButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.moveSelectionUpButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.moveSelectionUpButton.BackgroundColor = System.Drawing.Color.DarkGray;
            this.moveSelectionUpButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.moveSelectionUpButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.moveSelectionUpButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.moveSelectionUpButton.BorderWidth = 1;
            this.moveSelectionUpButton.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.moveSelectionUpButton.ContentPadding = new System.Windows.Forms.Padding(2, 2, 0, 0);
            this.moveSelectionUpButton.DrawBackColorOnFocus = false;
            this.moveSelectionUpButton.DrawBackgroundImage = false;
            this.moveSelectionUpButton.DrawBorderOnFocus = false;
            this.moveSelectionUpButton.DrawBorderOnTop = false;
            this.moveSelectionUpButton.Image = global::GST.Gearshift.Components.Properties.Resources.TestScriptEditorPanel_Up_32x32;
            this.moveSelectionUpButton.ImageDisabled = ((System.Drawing.Image)(resources.GetObject("moveSelectionUpButton.ImageDisabled")));
            this.moveSelectionUpButton.Location = new System.Drawing.Point(1311, 103);
            this.moveSelectionUpButton.Name = "moveSelectionUpButton";
            this.moveSelectionUpButton.Size = new System.Drawing.Size(38, 38);
            this.moveSelectionUpButton.SupportTransparentBackground = false;
            this.moveSelectionUpButton.TabIndex = 33;
            this.moveSelectionUpButton.TextImageRelation = Soko.Common.Controls.TextRelation.Underneath;
            this.moveSelectionUpButton.TextImageSpacing = 0;
            this.moveSelectionUpButton.Click += new System.EventHandler(this.ScriptDGV_MoveSelectionUp);
            // 
            // removeScriptRowButton
            // 
            this.removeScriptRowButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.removeScriptRowButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.removeScriptRowButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.removeScriptRowButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.removeScriptRowButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.removeScriptRowButton.BackgroundColor = System.Drawing.Color.DarkGray;
            this.removeScriptRowButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.removeScriptRowButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.removeScriptRowButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.removeScriptRowButton.BorderWidth = 1;
            this.removeScriptRowButton.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.removeScriptRowButton.ContentPadding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.removeScriptRowButton.DrawBackColorOnFocus = false;
            this.removeScriptRowButton.DrawBackgroundImage = false;
            this.removeScriptRowButton.DrawBorderOnFocus = false;
            this.removeScriptRowButton.DrawBorderOnTop = false;
            this.removeScriptRowButton.Image = ((System.Drawing.Image)(resources.GetObject("removeScriptRowButton.Image")));
            this.removeScriptRowButton.ImageDisabled = ((System.Drawing.Image)(resources.GetObject("removeScriptRowButton.ImageDisabled")));
            this.removeScriptRowButton.Location = new System.Drawing.Point(1311, 60);
            this.removeScriptRowButton.Name = "removeScriptRowButton";
            this.removeScriptRowButton.Size = new System.Drawing.Size(38, 38);
            this.removeScriptRowButton.SupportTransparentBackground = false;
            this.removeScriptRowButton.TabIndex = 32;
            this.removeScriptRowButton.TextImageRelation = Soko.Common.Controls.TextRelation.Underneath;
            this.removeScriptRowButton.TextImageSpacing = 0;
            this.removeScriptRowButton.Click += new System.EventHandler(this.ScriptDGV_RemoveRow);
            // 
            // insertScriptRowButton
            // 
            this.insertScriptRowButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.insertScriptRowButton.BackColor = System.Drawing.Color.DarkGray;
            this.insertScriptRowButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.insertScriptRowButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.insertScriptRowButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.insertScriptRowButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.insertScriptRowButton.BackgroundColor = System.Drawing.Color.DarkGray;
            this.insertScriptRowButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.insertScriptRowButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.insertScriptRowButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.insertScriptRowButton.BorderWidth = 1;
            this.insertScriptRowButton.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.insertScriptRowButton.ContentPadding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.insertScriptRowButton.DrawBackColorOnFocus = false;
            this.insertScriptRowButton.DrawBackgroundImage = false;
            this.insertScriptRowButton.DrawBorderOnFocus = false;
            this.insertScriptRowButton.DrawBorderOnTop = false;
            this.insertScriptRowButton.Image = ((System.Drawing.Image)(resources.GetObject("insertScriptRowButton.Image")));
            this.insertScriptRowButton.ImageDisabled = ((System.Drawing.Image)(resources.GetObject("insertScriptRowButton.ImageDisabled")));
            this.insertScriptRowButton.Location = new System.Drawing.Point(1311, 17);
            this.insertScriptRowButton.Name = "insertScriptRowButton";
            this.insertScriptRowButton.Size = new System.Drawing.Size(38, 38);
            this.insertScriptRowButton.SupportTransparentBackground = false;
            this.insertScriptRowButton.TabIndex = 31;
            this.insertScriptRowButton.TextImageRelation = Soko.Common.Controls.TextRelation.Underneath;
            this.insertScriptRowButton.TextImageSpacing = 0;
            this.insertScriptRowButton.Click += new System.EventHandler(this.ScriptDGV_InsertRow);
            
            // 
            // scriptDGV
            // 
            this.scriptDGV.AllowUserToAddRows = false;
            this.scriptDGV.AllowUserToDeleteRows = false;
            this.scriptDGV.AllowUserToResizeColumns = false;
            this.scriptDGV.AllowUserToResizeRows = false;
            this.scriptDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scriptDGV.BackgroundColor = System.Drawing.Color.DarkGray;
            this.scriptDGV.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.scriptDGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.scriptDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.scriptDGV.DefaultCellStyle = dataGridViewCellStyle2;
            this.scriptDGV.GridColor = System.Drawing.Color.DarkGray;
            this.scriptDGV.Location = new System.Drawing.Point(5, 9);
            this.scriptDGV.Margin = new System.Windows.Forms.Padding(10, 10, 2, 2);
            this.scriptDGV.Name = "scriptDGV";
            this.scriptDGV.RowHeadersWidth = 20;
            this.scriptDGV.Size = new System.Drawing.Size(1301, 613);
            this.scriptDGV.TabIndex = 0;
            this.scriptDGV.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.scriptDGV_CellBeginEdit);
            this.scriptDGV.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.scriptDGV_CellEndEdit);
            this.scriptDGV.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.scriptDGV_RowsAdded);
            // 
            // imageButton6
            // 
            this.imageButton6.AutoSize = true;
            this.imageButton6.BackColorOnClicked1 = System.Drawing.Color.Orange;
            this.imageButton6.BackColorOnClicked2 = System.Drawing.Color.Orange;
            this.imageButton6.BackColorOnFocus1 = System.Drawing.Color.Transparent;
            this.imageButton6.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(238)))), ((int)(((byte)(176)))));
            this.imageButton6.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.imageButton6.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.imageButton6.BorderColor = System.Drawing.Color.Black;
            this.imageButton6.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(217)))), ((int)(((byte)(72)))));
            this.imageButton6.BorderWidth = 10;
            this.imageButton6.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.imageButton6.ContentPadding = new System.Windows.Forms.Padding(0);
            this.imageButton6.DrawBackColorOnFocus = false;
            this.imageButton6.DrawBackgroundImage = false;
            this.imageButton6.DrawBorderOnFocus = false;
            this.imageButton6.DrawBorderOnTop = false;
            this.imageButton6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.imageButton6.Image = ((System.Drawing.Image)(resources.GetObject("imageButton6.Image")));
            this.imageButton6.ImageDisabled = ((System.Drawing.Image)(resources.GetObject("imageButton6.ImageDisabled")));
            this.imageButton6.Location = new System.Drawing.Point(79, 37);
            this.imageButton6.Name = "imageButton6";
            this.imageButton6.Size = new System.Drawing.Size(32, 47);
            this.imageButton6.SupportTransparentBackground = false;
            this.imageButton6.TabIndex = 30;
            this.imageButton6.TextImageRelation = Soko.Common.Controls.TextRelation.Underneath;
            this.imageButton6.TextImageSpacing = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.passFailTable);
            this.tabPage2.Image = null;
            this.tabPage2.ImageSize = new System.Drawing.Size(16, 16);
            this.tabPage2.Location = new System.Drawing.Point(1, 30);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.ShowCloseButton = true;
            this.tabPage2.Size = new System.Drawing.Size(1353, 630);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Pass/fail params";
            this.tabPage2.ThemesEnabled = false;
            // 
            // passFailTable
            // 
            this.passFailTable.AllowSelection = false;
            this.passFailTable.BackColor = System.Drawing.Color.Gainsboro;
            this.passFailTable.BorderColor = System.Drawing.Color.Black;
            this.passFailTable.DataMember = null;
            this.passFailTable.DataSourceColumnBinder = dataSourceColumnBinder1;
            this.passFailTable.Dock = System.Windows.Forms.DockStyle.Fill;
            dragDropRenderer1.ForeColor = System.Drawing.Color.Red;
            this.passFailTable.DragDropRenderer = dragDropRenderer1;
            this.passFailTable.EditStartAction = XPTable.Editors.EditStartAction.SingleClick;
            this.passFailTable.EnableToolTips = true;
            this.passFailTable.EnableWordWrap = true;
            this.passFailTable.FullRowSelect = true;
            this.passFailTable.GridLinesContrainedToData = false;
            this.passFailTable.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.passFailTable.Location = new System.Drawing.Point(0, 0);
            this.passFailTable.Name = "passFailTable";
            this.passFailTable.NoItemsText = "";
            this.passFailTable.SelectionStyle = XPTable.Models.SelectionStyle.Grid;
            this.passFailTable.Size = new System.Drawing.Size(1353, 630);
            this.passFailTable.TabIndex = 31;
            this.passFailTable.Text = "table1";
            this.passFailTable.ToolTipInitialDelay = 500;
            this.passFailTable.ToolTipShowAlways = true;
            this.passFailTable.UnfocusedBorderColor = System.Drawing.Color.Black;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.promptsTable);
            this.tabPage3.Image = null;
            this.tabPage3.ImageSize = new System.Drawing.Size(16, 16);
            this.tabPage3.Location = new System.Drawing.Point(1, 30);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.ShowCloseButton = true;
            this.tabPage3.Size = new System.Drawing.Size(1353, 630);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "User prompts";
            this.tabPage3.ThemesEnabled = false;
            // 
            // promptsTable
            // 
            this.promptsTable.AllowSelection = false;
            this.promptsTable.BackColor = System.Drawing.Color.Gainsboro;
            this.promptsTable.BorderColor = System.Drawing.Color.Black;
            this.promptsTable.DataMember = null;
            this.promptsTable.DataSourceColumnBinder = dataSourceColumnBinder2;
            this.promptsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            dragDropRenderer2.ForeColor = System.Drawing.Color.Red;
            this.promptsTable.DragDropRenderer = dragDropRenderer2;
            this.promptsTable.EditStartAction = XPTable.Editors.EditStartAction.SingleClick;
            this.promptsTable.EnableToolTips = true;
            this.promptsTable.EnableWordWrap = true;
            this.promptsTable.FullRowSelect = true;
            this.promptsTable.GridLinesContrainedToData = false;
            this.promptsTable.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.promptsTable.Location = new System.Drawing.Point(0, 0);
            this.promptsTable.Name = "promptsTable";
            this.promptsTable.NoItemsText = "";
            this.promptsTable.SelectionStyle = XPTable.Models.SelectionStyle.Grid;
            this.promptsTable.Size = new System.Drawing.Size(1353, 630);
            this.promptsTable.TabIndex = 32;
            this.promptsTable.Text = "table1";
            this.promptsTable.ToolTipInitialDelay = 500;
            this.promptsTable.ToolTipShowAlways = true;
            this.promptsTable.UnfocusedBorderColor = System.Drawing.Color.Black;
            // 
            // mainTabControl
            // 
            this.mainTabControl.ActiveTabForeColor = System.Drawing.Color.Empty;
            this.mainTabControl.BackColor = System.Drawing.Color.Silver;
            this.mainTabControl.BeforeTouchSize = new System.Drawing.Size(1364, 819);
            this.mainTabControl.CloseButtonForeColor = System.Drawing.Color.Empty;
            this.mainTabControl.CloseButtonHoverForeColor = System.Drawing.Color.Empty;
            this.mainTabControl.CloseButtonPressedForeColor = System.Drawing.Color.Empty;
            this.mainTabControl.Controls.Add(this.testScriptTabPage);
            this.mainTabControl.Controls.Add(this.gearboxPropsTabPage);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.InActiveTabForeColor = System.Drawing.Color.Empty;
            this.mainTabControl.Location = new System.Drawing.Point(0, 0);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SeparatorColor = System.Drawing.SystemColors.ControlDark;
            this.mainTabControl.ShowSeparator = false;
            this.mainTabControl.Size = new System.Drawing.Size(1364, 819);
            this.mainTabControl.TabIndex = 34;
            this.mainTabControl.TabStyle = typeof(Syncfusion.Windows.Forms.Tools.TabRendererDockingWhidbeyBeta);
            this.mainTabControl.SelectedIndexChanged += new System.EventHandler(this.mainTabControl_SelectedPageChanged);
            // 
            // testScriptTabPage
            // 
            this.testScriptTabPage.BackColor = System.Drawing.Color.Silver;
            this.testScriptTabPage.Controls.Add(this.nicePanel2);
            this.testScriptTabPage.Controls.Add(this.fileManagementPanel);
            this.testScriptTabPage.Controls.Add(this.nicePanel1);
            this.testScriptTabPage.Controls.Add(this.gearboxMainPanel2);
            this.testScriptTabPage.Controls.Add(this.tabControl);
            this.testScriptTabPage.Image = null;
            this.testScriptTabPage.ImageSize = new System.Drawing.Size(16, 16);
            this.testScriptTabPage.Location = new System.Drawing.Point(1, 30);
            this.testScriptTabPage.Name = "testScriptTabPage";
            this.testScriptTabPage.ShowCloseButton = true;
            this.testScriptTabPage.Size = new System.Drawing.Size(1361, 787);
            this.testScriptTabPage.TabIndex = 1;
            this.testScriptTabPage.Text = "Test script file";
            this.testScriptTabPage.ThemesEnabled = false;
            // 
            // nicePanel2
            // 
            this.nicePanel2.AutoplaceElements = false;
            this.nicePanel2.AutoScrollHorizontalMaximum = 100;
            this.nicePanel2.AutoScrollHorizontalMinimum = 0;
            this.nicePanel2.AutoScrollHPos = 0;
            this.nicePanel2.AutoScrollVerticalMaximum = 100;
            this.nicePanel2.AutoScrollVerticalMinimum = 0;
            this.nicePanel2.AutoScrollVPos = 0;
            this.nicePanel2.AutoSizeElements = false;
            this.nicePanel2.BackColor = System.Drawing.Color.Transparent;
            this.nicePanel2.backgroundColor1 = System.Drawing.Color.DarkGray;
            this.nicePanel2.backgroundColor2 = System.Drawing.Color.Gainsboro;
            this.nicePanel2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
            this.nicePanel2.BorderWidth = 1;
            this.nicePanel2.Controls.Add(this.TSFilesComboBox);
            this.nicePanel2.Controls.Add(this.gearboxGearboxLabel);
            this.nicePanel2.Controls.Add(this.scriptFileNewButton);
            this.nicePanel2.DrawBackImage = false;
            this.nicePanel2.EnableAutoScrollHorizontal = false;
            this.nicePanel2.EnableAutoScrollVertical = false;
            this.nicePanel2.Gradient = Soko.Common.Controls.NicePanel.GradientType.SigmaBellHorizontalUpper;
            this.nicePanel2.HorizontalMargin = 0;
            this.nicePanel2.Location = new System.Drawing.Point(1, 65);
            this.nicePanel2.Name = "nicePanel2";
            this.nicePanel2.roundingRadius = 10;
            this.nicePanel2.Size = new System.Drawing.Size(346, 65);
            this.nicePanel2.SupportTransparentBackground = false;
            this.nicePanel2.TabIndex = 6;
            this.nicePanel2.VerticalMargin = 0;
            this.nicePanel2.VisibleAutoScrollHorizontal = false;
            this.nicePanel2.VisibleAutoScrollVertical = false;
            // 
            // TSFilesComboBox
            // 
            this.TSFilesComboBox.DropDownHeight = 400;
            this.TSFilesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TSFilesComboBox.DropDownWidth = 250;
            this.TSFilesComboBox.FormattingEnabled = true;
            this.TSFilesComboBox.IntegralHeight = false;
            this.TSFilesComboBox.Location = new System.Drawing.Point(8, 34);
            this.TSFilesComboBox.Name = "TSFilesComboBox";
            this.TSFilesComboBox.Size = new System.Drawing.Size(206, 21);
            this.TSFilesComboBox.Sorted = true;
            this.TSFilesComboBox.TabIndex = 34;
            this.TSFilesComboBox.DropDown += new System.EventHandler(this.PopulateTSFilesComboBox);
            this.TSFilesComboBox.SelectedIndexChanged += new System.EventHandler(this.TSFilesComboBox_SelectedValueChanged);
            // 
            // gearboxGearboxLabel
            // 
            this.gearboxGearboxLabel.AutoSize = true;
            this.gearboxGearboxLabel.BackColor = System.Drawing.Color.Transparent;
            this.gearboxGearboxLabel.Location = new System.Drawing.Point(5, 13);
            this.gearboxGearboxLabel.Name = "gearboxGearboxLabel";
            this.gearboxGearboxLabel.Size = new System.Drawing.Size(121, 13);
            this.gearboxGearboxLabel.TabIndex = 1;
            this.gearboxGearboxLabel.Text = "Load test script from file:";
            // 
            // scriptFileNewButton
            // 
            this.scriptFileNewButton.AutoSize = true;
            this.scriptFileNewButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.scriptFileNewButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.scriptFileNewButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.scriptFileNewButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.scriptFileNewButton.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.scriptFileNewButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.scriptFileNewButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.scriptFileNewButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.scriptFileNewButton.BorderWidth = 10;
            this.scriptFileNewButton.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.scriptFileNewButton.ContentPadding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.scriptFileNewButton.DrawBackColorOnFocus = false;
            this.scriptFileNewButton.DrawBackgroundImage = false;
            this.scriptFileNewButton.DrawBorderOnFocus = false;
            this.scriptFileNewButton.DrawBorderOnTop = false;
            this.scriptFileNewButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.scriptFileNewButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.scriptFileNewButton.Image = global::GST.Gearshift.Components.Properties.Resources.TestScriptEditorPanel_Empty_Page_32x32;
            this.scriptFileNewButton.ImageDisabled = global::GST.Gearshift.Components.Properties.Resources.TestScriptEditorPanel_Empty_Page_32x32;
            this.scriptFileNewButton.Location = new System.Drawing.Point(226, 13);
            this.scriptFileNewButton.Name = "scriptFileNewButton";
            this.scriptFileNewButton.Size = new System.Drawing.Size(115, 42);
            this.scriptFileNewButton.SupportTransparentBackground = false;
            this.scriptFileNewButton.TabIndex = 31;
            this.scriptFileNewButton.Text = "New script";
            this.scriptFileNewButton.TextImageRelation = Soko.Common.Controls.TextRelation.OnRight;
            this.scriptFileNewButton.TextImageSpacing = 1;
            this.scriptFileNewButton.Click += new System.EventHandler(this.TestScriptFileNewButton_Click);
            // 
            // fileManagementPanel
            // 
            this.fileManagementPanel.AutoplaceElements = false;
            this.fileManagementPanel.AutoScrollHorizontalMaximum = 100;
            this.fileManagementPanel.AutoScrollHorizontalMinimum = 0;
            this.fileManagementPanel.AutoScrollHPos = 0;
            this.fileManagementPanel.AutoScrollVerticalMaximum = 100;
            this.fileManagementPanel.AutoScrollVerticalMinimum = 0;
            this.fileManagementPanel.AutoScrollVPos = 0;
            this.fileManagementPanel.AutoSizeElements = false;
            this.fileManagementPanel.BackColor = System.Drawing.Color.Transparent;
            this.fileManagementPanel.backgroundColor1 = System.Drawing.Color.DarkGray;
            this.fileManagementPanel.backgroundColor2 = System.Drawing.Color.Gainsboro;
            this.fileManagementPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
            this.fileManagementPanel.BorderWidth = 1;
            this.fileManagementPanel.Controls.Add(this.nameTextBox);
            this.fileManagementPanel.Controls.Add(this.label2);
            this.fileManagementPanel.Controls.Add(this.deleteFileButton);
            this.fileManagementPanel.Controls.Add(this.scriptFileSaveButton);
            this.fileManagementPanel.DrawBackImage = false;
            this.fileManagementPanel.EnableAutoScrollHorizontal = false;
            this.fileManagementPanel.EnableAutoScrollVertical = false;
            this.fileManagementPanel.Gradient = Soko.Common.Controls.NicePanel.GradientType.SigmaBellHorizontalUpper;
            this.fileManagementPanel.HorizontalMargin = 0;
            this.fileManagementPanel.Location = new System.Drawing.Point(1, 1);
            this.fileManagementPanel.Name = "fileManagementPanel";
            this.fileManagementPanel.roundingRadius = 10;
            this.fileManagementPanel.Size = new System.Drawing.Size(346, 65);
            this.fileManagementPanel.SupportTransparentBackground = false;
            this.fileManagementPanel.TabIndex = 6;
            this.fileManagementPanel.VerticalMargin = 0;
            this.fileManagementPanel.VisibleAutoScrollHorizontal = false;
            this.fileManagementPanel.VisibleAutoScrollVertical = false;
            // 
            // nameTextBox
            // 
            this.nameTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.nameTextBox.Location = new System.Drawing.Point(8, 33);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(206, 20);
            this.nameTextBox.TabIndex = 37;
            this.nameTextBox.TextChanged += new System.EventHandler(this.nameTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(5, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 13);
            this.label2.TabIndex = 36;
            this.label2.Text = "Current test script name:";
            // 
            // deleteFileButton
            // 
            this.deleteFileButton.AutoSize = true;
            this.deleteFileButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.deleteFileButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.deleteFileButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.deleteFileButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.deleteFileButton.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.deleteFileButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.deleteFileButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.deleteFileButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.deleteFileButton.BorderWidth = 10;
            this.deleteFileButton.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.deleteFileButton.ContentPadding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.deleteFileButton.DrawBackColorOnFocus = false;
            this.deleteFileButton.DrawBackgroundImage = false;
            this.deleteFileButton.DrawBorderOnFocus = false;
            this.deleteFileButton.DrawBorderOnTop = false;
            this.deleteFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.deleteFileButton.Image = global::GST.Gearshift.Components.Properties.Resources.TestScriptEditorPanel_Delete_48x48;
            this.deleteFileButton.ImageDisabled = global::GST.Gearshift.Components.Properties.Resources.TestScriptEditorPanel_Delete_48x48;
            this.deleteFileButton.Location = new System.Drawing.Point(286, 6);
            this.deleteFileButton.Name = "deleteFileButton";
            this.deleteFileButton.Size = new System.Drawing.Size(54, 52);
            this.deleteFileButton.SupportTransparentBackground = false;
            this.deleteFileButton.TabIndex = 31;
            this.deleteFileButton.TextImageRelation = Soko.Common.Controls.TextRelation.Underneath;
            this.deleteFileButton.TextImageSpacing = 1;
            this.deleteFileButton.Click += new System.EventHandler(this.deleteFileButton_Click);
            // 
            // scriptFileSaveButton
            // 
            this.scriptFileSaveButton.AutoSize = true;
            this.scriptFileSaveButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.scriptFileSaveButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.scriptFileSaveButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.scriptFileSaveButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.scriptFileSaveButton.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.scriptFileSaveButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.scriptFileSaveButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.scriptFileSaveButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.scriptFileSaveButton.BorderWidth = 10;
            this.scriptFileSaveButton.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.scriptFileSaveButton.ContentPadding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.scriptFileSaveButton.DrawBackColorOnFocus = false;
            this.scriptFileSaveButton.DrawBackgroundImage = false;
            this.scriptFileSaveButton.DrawBorderOnFocus = false;
            this.scriptFileSaveButton.DrawBorderOnTop = false;
            this.scriptFileSaveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.scriptFileSaveButton.Image = global::GST.Gearshift.Components.Properties.Resources.TestScriptEditorPanel_Save_48x48;
            this.scriptFileSaveButton.ImageDisabled = global::GST.Gearshift.Components.Properties.Resources.TestScriptEditorPanel_filesave_32x32;
            this.scriptFileSaveButton.Location = new System.Drawing.Point(226, 6);
            this.scriptFileSaveButton.Name = "scriptFileSaveButton";
            this.scriptFileSaveButton.Size = new System.Drawing.Size(54, 52);
            this.scriptFileSaveButton.SupportTransparentBackground = false;
            this.scriptFileSaveButton.TabIndex = 31;
            this.scriptFileSaveButton.TextImageRelation = Soko.Common.Controls.TextRelation.Underneath;
            this.scriptFileSaveButton.TextImageSpacing = 1;
            this.scriptFileSaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // nicePanel1
            // 
            this.nicePanel1.AutoplaceElements = false;
            this.nicePanel1.AutoScrollHorizontalMaximum = 100;
            this.nicePanel1.AutoScrollHorizontalMinimum = 0;
            this.nicePanel1.AutoScrollHPos = 0;
            this.nicePanel1.AutoScrollVerticalMaximum = 100;
            this.nicePanel1.AutoScrollVerticalMinimum = 0;
            this.nicePanel1.AutoScrollVPos = 0;
            this.nicePanel1.AutoSizeElements = false;
            this.nicePanel1.BackColor = System.Drawing.Color.Transparent;
            this.nicePanel1.backgroundColor1 = System.Drawing.Color.DarkGray;
            this.nicePanel1.backgroundColor2 = System.Drawing.Color.Gainsboro;
            this.nicePanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
            this.nicePanel1.BorderWidth = 1;
            this.nicePanel1.Controls.Add(this.TSMasterDataComboBox);
            this.nicePanel1.Controls.Add(this.imageButton3);
            this.nicePanel1.Controls.Add(this.label4);
            this.nicePanel1.DrawBackImage = false;
            this.nicePanel1.EnableAutoScrollHorizontal = false;
            this.nicePanel1.EnableAutoScrollVertical = false;
            this.nicePanel1.Gradient = Soko.Common.Controls.NicePanel.GradientType.SigmaBellHorizontalUpper;
            this.nicePanel1.HorizontalMargin = 0;
            this.nicePanel1.Location = new System.Drawing.Point(352, 1);
            this.nicePanel1.Name = "nicePanel1";
            this.nicePanel1.roundingRadius = 10;
            this.nicePanel1.Size = new System.Drawing.Size(237, 131);
            this.nicePanel1.SupportTransparentBackground = false;
            this.nicePanel1.TabIndex = 33;
            this.nicePanel1.VerticalMargin = 0;
            this.nicePanel1.VisibleAutoScrollHorizontal = false;
            this.nicePanel1.VisibleAutoScrollVertical = false;
            // 
            // TSMasterDataComboBox
            // 
            this.TSMasterDataComboBox.DropDownHeight = 400;
            this.TSMasterDataComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TSMasterDataComboBox.DropDownWidth = 250;
            this.TSMasterDataComboBox.FormattingEnabled = true;
            this.TSMasterDataComboBox.IntegralHeight = false;
            this.TSMasterDataComboBox.Location = new System.Drawing.Point(15, 98);
            this.TSMasterDataComboBox.Name = "TSMasterDataComboBox";
            this.TSMasterDataComboBox.Size = new System.Drawing.Size(203, 21);
            this.TSMasterDataComboBox.Sorted = true;
            this.TSMasterDataComboBox.TabIndex = 39;
            this.TSMasterDataComboBox.DropDown += new System.EventHandler(this.PopulateMDFilesComboBox);
            this.TSMasterDataComboBox.SelectedIndexChanged += new System.EventHandler(this.TSMasterDataComboBox_SelectedIndexChanged);
            // 
            // imageButton3
            // 
            this.imageButton3.AutoSize = true;
            this.imageButton3.BackColorOnClicked1 = System.Drawing.Color.Orange;
            this.imageButton3.BackColorOnClicked2 = System.Drawing.Color.Orange;
            this.imageButton3.BackColorOnFocus1 = System.Drawing.Color.Transparent;
            this.imageButton3.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(238)))), ((int)(((byte)(176)))));
            this.imageButton3.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.imageButton3.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.imageButton3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.imageButton3.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(217)))), ((int)(((byte)(72)))));
            this.imageButton3.BorderWidth = 10;
            this.imageButton3.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.imageButton3.ContentPadding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.imageButton3.DrawBackColorOnFocus = false;
            this.imageButton3.DrawBackgroundImage = false;
            this.imageButton3.DrawBorderOnFocus = false;
            this.imageButton3.DrawBorderOnTop = false;
            this.imageButton3.Enabled = false;
            this.imageButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.imageButton3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.imageButton3.Image = global::GST.Gearshift.Components.Properties.Resources.TestScriptEditorPanel_Delete_32x32;
            this.imageButton3.ImageDisabled = global::GST.Gearshift.Components.Properties.Resources.TestScriptEditorPanel_Delete_32x32;
            this.imageButton3.Location = new System.Drawing.Point(15, 16);
            this.imageButton3.Name = "imageButton3";
            this.imageButton3.Size = new System.Drawing.Size(203, 42);
            this.imageButton3.SupportTransparentBackground = false;
            this.imageButton3.TabIndex = 41;
            this.imageButton3.Text = "No reference curves assigned";
            this.imageButton3.TextImageRelation = Soko.Common.Controls.TextRelation.OnRight;
            this.imageButton3.TextImageSpacing = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(18, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(172, 13);
            this.label4.TabIndex = 38;
            this.label4.Text = "Load the new master data from file:";
            // 
            // gearboxMainPanel2
            // 
            this.gearboxMainPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gearboxMainPanel2.AutoplaceElements = false;
            this.gearboxMainPanel2.AutoScrollHorizontalMaximum = 100;
            this.gearboxMainPanel2.AutoScrollHorizontalMinimum = 0;
            this.gearboxMainPanel2.AutoScrollHPos = 0;
            this.gearboxMainPanel2.AutoScrollVerticalMaximum = 100;
            this.gearboxMainPanel2.AutoScrollVerticalMinimum = 0;
            this.gearboxMainPanel2.AutoScrollVPos = 0;
            this.gearboxMainPanel2.AutoSizeElements = false;
            this.gearboxMainPanel2.BackColor = System.Drawing.Color.Transparent;
            this.gearboxMainPanel2.backgroundColor1 = System.Drawing.Color.DarkGray;
            this.gearboxMainPanel2.backgroundColor2 = System.Drawing.Color.Gainsboro;
            this.gearboxMainPanel2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
            this.gearboxMainPanel2.BorderWidth = 1;
            this.gearboxMainPanel2.Controls.Add(this.label1);
            this.gearboxMainPanel2.Controls.Add(this.gearboxPicturePictureBox);
            this.gearboxMainPanel2.Controls.Add(this.gearboxModelLabelVar);
            this.gearboxMainPanel2.Controls.Add(this.gearboxNameLabel);
            this.gearboxMainPanel2.Controls.Add(this.gearboxManufacturerLabelVar);
            this.gearboxMainPanel2.Controls.Add(this.gearboxManufacturerLabel);
            this.gearboxMainPanel2.Controls.Add(this.gearboxModelLabel);
            this.gearboxMainPanel2.Controls.Add(this.gearboxNameLabelVar);
            this.gearboxMainPanel2.DrawBackImage = false;
            this.gearboxMainPanel2.EnableAutoScrollHorizontal = false;
            this.gearboxMainPanel2.EnableAutoScrollVertical = false;
            this.gearboxMainPanel2.Gradient = Soko.Common.Controls.NicePanel.GradientType.SigmaBellHorizontalUpper;
            this.gearboxMainPanel2.HorizontalMargin = 0;
            this.gearboxMainPanel2.Location = new System.Drawing.Point(594, 1);
            this.gearboxMainPanel2.Name = "gearboxMainPanel2";
            this.gearboxMainPanel2.roundingRadius = 10;
            this.gearboxMainPanel2.Size = new System.Drawing.Size(762, 131);
            this.gearboxMainPanel2.SupportTransparentBackground = false;
            this.gearboxMainPanel2.TabIndex = 7;
            this.gearboxMainPanel2.VerticalMargin = 0;
            this.gearboxMainPanel2.VisibleAutoScrollHorizontal = false;
            this.gearboxMainPanel2.VisibleAutoScrollVertical = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(7, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 13);
            this.label1.TabIndex = 35;
            this.label1.Text = "Loaded gearbox information:";
            // 
            // gearboxPicturePictureBox
            // 
            this.gearboxPicturePictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gearboxPicturePictureBox.BackColor = System.Drawing.Color.Transparent;
            this.gearboxPicturePictureBox.Location = new System.Drawing.Point(544, 10);
            this.gearboxPicturePictureBox.Name = "gearboxPicturePictureBox";
            this.gearboxPicturePictureBox.Size = new System.Drawing.Size(214, 112);
            this.gearboxPicturePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.gearboxPicturePictureBox.TabIndex = 5;
            this.gearboxPicturePictureBox.TabStop = false;
            // 
            // gearboxModelLabelVar
            // 
            this.gearboxModelLabelVar.Location = new System.Drawing.Point(82, 76);
            this.gearboxModelLabelVar.Name = "gearboxModelLabelVar";
            this.gearboxModelLabelVar.Size = new System.Drawing.Size(150, 13);
            this.gearboxModelLabelVar.TabIndex = 7;
            // 
            // gearboxNameLabel
            // 
            this.gearboxNameLabel.AutoSize = true;
            this.gearboxNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.gearboxNameLabel.Location = new System.Drawing.Point(7, 35);
            this.gearboxNameLabel.Name = "gearboxNameLabel";
            this.gearboxNameLabel.Size = new System.Drawing.Size(38, 13);
            this.gearboxNameLabel.TabIndex = 0;
            this.gearboxNameLabel.Text = "Name:";
            // 
            // gearboxManufacturerLabelVar
            // 
            this.gearboxManufacturerLabelVar.Location = new System.Drawing.Point(82, 55);
            this.gearboxManufacturerLabelVar.Name = "gearboxManufacturerLabelVar";
            this.gearboxManufacturerLabelVar.Size = new System.Drawing.Size(150, 13);
            this.gearboxManufacturerLabelVar.TabIndex = 6;
            // 
            // gearboxManufacturerLabel
            // 
            this.gearboxManufacturerLabel.AutoSize = true;
            this.gearboxManufacturerLabel.BackColor = System.Drawing.Color.Transparent;
            this.gearboxManufacturerLabel.Location = new System.Drawing.Point(7, 55);
            this.gearboxManufacturerLabel.Name = "gearboxManufacturerLabel";
            this.gearboxManufacturerLabel.Size = new System.Drawing.Size(73, 13);
            this.gearboxManufacturerLabel.TabIndex = 1;
            this.gearboxManufacturerLabel.Text = "Manufacturer:";
            // 
            // gearboxModelLabel
            // 
            this.gearboxModelLabel.AutoSize = true;
            this.gearboxModelLabel.BackColor = System.Drawing.Color.Transparent;
            this.gearboxModelLabel.Location = new System.Drawing.Point(7, 76);
            this.gearboxModelLabel.Name = "gearboxModelLabel";
            this.gearboxModelLabel.Size = new System.Drawing.Size(39, 13);
            this.gearboxModelLabel.TabIndex = 2;
            this.gearboxModelLabel.Text = "Model:";
            // 
            // gearboxNameLabelVar
            // 
            this.gearboxNameLabelVar.Location = new System.Drawing.Point(82, 35);
            this.gearboxNameLabelVar.Name = "gearboxNameLabelVar";
            this.gearboxNameLabelVar.Size = new System.Drawing.Size(150, 13);
            this.gearboxNameLabelVar.TabIndex = 4;
            // 
            // gearboxPropsTabPage
            // 
            this.gearboxPropsTabPage.Controls.Add(this.gearboxConfigPanel);
            this.gearboxPropsTabPage.Image = null;
            this.gearboxPropsTabPage.ImageSize = new System.Drawing.Size(16, 16);
            this.gearboxPropsTabPage.Location = new System.Drawing.Point(1, 30);
            this.gearboxPropsTabPage.Name = "gearboxPropsTabPage";
            this.gearboxPropsTabPage.ShowCloseButton = true;
            this.gearboxPropsTabPage.Size = new System.Drawing.Size(1361, 787);
            this.gearboxPropsTabPage.TabIndex = 1;
            this.gearboxPropsTabPage.Text = "Gearbox configuration";
            this.gearboxPropsTabPage.ThemesEnabled = false;
            // 
            // gearboxConfigPanel
            // 
            this.gearboxConfigPanel.AllowDrop = true;
            this.gearboxConfigPanel.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.gearboxConfigPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gearboxConfigPanel.EmbeddedModeEnabled = false;
            this.gearboxConfigPanel.ForeColor = System.Drawing.Color.Black;
            this.gearboxConfigPanel.Location = new System.Drawing.Point(0, 0);
            this.gearboxConfigPanel.MinimumSize = new System.Drawing.Size(751, 502);
            this.gearboxConfigPanel.Name = "gearboxConfigPanel";
            this.gearboxConfigPanel.PanelsColor1 = System.Drawing.Color.DarkGray;
            this.gearboxConfigPanel.PanelsColor2 = System.Drawing.Color.Gainsboro;
            this.gearboxConfigPanel.Size = new System.Drawing.Size(1361, 787);
            this.gearboxConfigPanel.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.HeaderText = "Gear";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.FillWeight = 68.02721F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Step";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.FillWeight = 131.9728F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Delay[ms]";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // TestScriptEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.mainTabControl);
            this.Name = "TestScriptEditor";
            this.Size = new System.Drawing.Size(1364, 819);
            this.Resize += new System.EventHandler(this.TestScriptEditor_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.dataPanel.ResumeLayout(false);
            this.dataPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scriptDGV)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.passFailTable)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.promptsTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainTabControl)).EndInit();
            this.mainTabControl.ResumeLayout(false);
            this.testScriptTabPage.ResumeLayout(false);
            this.nicePanel2.ResumeLayout(false);
            this.nicePanel2.PerformLayout();
            this.fileManagementPanel.ResumeLayout(false);
            this.fileManagementPanel.PerformLayout();
            this.nicePanel1.ResumeLayout(false);
            this.nicePanel1.PerformLayout();
            this.gearboxMainPanel2.ResumeLayout(false);
            this.gearboxMainPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gearboxPicturePictureBox)).EndInit();
            this.gearboxPropsTabPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private global::Soko.Common.Controls.NicePanel dataPanel;
        private System.Windows.Forms.DataGridView scriptDGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private global::Soko.Common.Controls.NicePanel fileManagementPanel;
        private global::Soko.Common.Controls.NiceButton scriptFileSaveButton;
        private global::Soko.Common.Controls.NiceButton scriptFileNewButton;
        private System.Windows.Forms.Label gearboxGearboxLabel;
        private global::Soko.Common.Controls.NiceButton imageButton6;
        private Syncfusion.Windows.Forms.Tools.TabControlAdv tabControl; // tabControlAdv 
        private Syncfusion.Windows.Forms.Tools.TabPageAdv tabPage1;
        private Syncfusion.Windows.Forms.Tools.TabPageAdv tabPage2;     
        private global::Soko.Common.Controls.NiceButton moveSelectionDownButton;
        private global::Soko.Common.Controls.NiceButton moveSelectionUpButton;
        private global::Soko.Common.Controls.NiceButton removeScriptRowButton;
        private global::Soko.Common.Controls.NiceButton insertScriptRowButton;
        private System.Windows.Forms.ComboBox TSFilesComboBox;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox TSMasterDataComboBox;
        private Syncfusion.Windows.Forms.Tools.TabPageAdv tabPage3;
        

        private GST.Gearshift.Components.Forms.DAQ.GearboxConfigPanel gearboxConfigPanel;
        private Soko.Common.Controls.NicePanel nicePanel1;
        private System.Windows.Forms.Label label4;
        private Soko.Common.Controls.NiceButton imageButton3;
        private Soko.Common.Controls.NiceButton selectLoopSectionButton;
        private Soko.Common.Controls.NiceButton pasteButton;
        private Soko.Common.Controls.NiceButton copyButton;
        private Soko.Common.Controls.NiceButton cutButton;
        private Syncfusion.Windows.Forms.Tools.TabControlAdv mainTabControl; // tabControlAdv 
        private Syncfusion.Windows.Forms.Tools.TabPageAdv testScriptTabPage;
        private Syncfusion.Windows.Forms.Tools.TabPageAdv gearboxPropsTabPage;                        
        private Soko.Common.Controls.NicePanel nicePanel2;
        private Soko.Common.Controls.NicePanel gearboxMainPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox gearboxPicturePictureBox;
        private System.Windows.Forms.Label gearboxModelLabelVar;
        private System.Windows.Forms.Label gearboxNameLabel;
        private System.Windows.Forms.Label gearboxManufacturerLabelVar;
        private System.Windows.Forms.Label gearboxManufacturerLabel;
        private System.Windows.Forms.Label gearboxModelLabel;
        private System.Windows.Forms.Label gearboxNameLabelVar;
        private XPTable.Models.Table passFailTable;
        private XPTable.Models.Table promptsTable;
        private Soko.Common.Controls.NiceButton deleteFileButton;
    }
}
