namespace GST.Gearshift.Components.Forms.DAQ
{
    partial class ReportViewer
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
            XPTable.Models.DataSourceColumnBinder dataSourceColumnBinder3 = new XPTable.Models.DataSourceColumnBinder();
            XPTable.Renderers.DragDropRenderer dragDropRenderer3 = new XPTable.Renderers.DragDropRenderer();
            XPTable.Models.DataSourceColumnBinder dataSourceColumnBinder4 = new XPTable.Models.DataSourceColumnBinder();
            XPTable.Renderers.DragDropRenderer dragDropRenderer4 = new XPTable.Renderers.DragDropRenderer();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportViewer));
            this.viewModeTabControl = new Syncfusion.Windows.Forms.Tools.TabControlAdv();
            this.graphicalViewTab = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.graphDockPanel = new System.Windows.Forms.Panel();
            this.graphicalPressureZedGraph = new ZedGraph.ZedGraphControl();
            this.nicePanel4 = new Soko.Common.Controls.NicePanel();
            this.pageSpanMinus = new System.Windows.Forms.Button();
            this.pageSpanLabel = new System.Windows.Forms.Label();
            this.pageSpanAdd = new System.Windows.Forms.Button();
            this.fixedPanel = new Soko.Common.Controls.NicePanel();
            this.showRefCb = new System.Windows.Forms.CheckBox();
            this.allOnOffCb = new System.Windows.Forms.CheckBox();
            this.setRefButton = new System.Windows.Forms.Button();
            this.legendPanel = new Soko.Common.Controls.NicePanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.realTimeTab = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.livePreviewPanel1 = new GST.Gearshift.Components.Forms.DAQ.LivePreviewPanel();
            this.driveTableTab = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.driveTable = new XPTable.Models.Table();
            this.passFailTableTab = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.passFailTable = new XPTable.Models.Table();
            this.commentsTab = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.nicePanel2 = new Soko.Common.Controls.NicePanel();
            this.nicePanel3 = new Soko.Common.Controls.NicePanel();
            this.saveCommentsButton = new Soko.Common.Controls.NiceButton();
            this.imageButton1 = new Soko.Common.Controls.NiceButton();
            this.operatorNameLabel = new System.Windows.Forms.Label();
            this.commentLabel = new System.Windows.Forms.Label();
            this.testNameLabel = new System.Windows.Forms.Label();
            this.testTimestampLabel = new System.Windows.Forms.Label();
            this.serialNoLabel = new System.Windows.Forms.Label();
            this.commentTextBox = new Soko.Common.Controls.NiceTextBox();
            this.testNameTextBox = new Soko.Common.Controls.NiceTextBox();
            this.operatorTextBox = new Soko.Common.Controls.NiceTextBox();
            this.timestampTextBox = new Soko.Common.Controls.NiceTextBox();
            this.serialNoTextBox = new Soko.Common.Controls.NiceTextBox();
            this.printMeTab = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.nicePanel1 = new Soko.Common.Controls.NicePanel();
            this.printReportWithTablesButton = new Soko.Common.Controls.NiceButton();
            this.printReportGraphsOnlyButton = new Soko.Common.Controls.NiceButton();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.viewModeTabControl)).BeginInit();
            this.viewModeTabControl.SuspendLayout();
            this.graphicalViewTab.SuspendLayout();
            this.graphDockPanel.SuspendLayout();
            this.nicePanel4.SuspendLayout();
            this.fixedPanel.SuspendLayout();
            this.legendPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.realTimeTab.SuspendLayout();
            this.driveTableTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.driveTable)).BeginInit();
            this.passFailTableTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.passFailTable)).BeginInit();
            this.commentsTab.SuspendLayout();
            this.nicePanel2.SuspendLayout();
            this.nicePanel3.SuspendLayout();
            this.printMeTab.SuspendLayout();
            this.nicePanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // viewModeTabControl
            // 
            this.viewModeTabControl.ActiveTabForeColor = System.Drawing.Color.Empty;
            this.viewModeTabControl.BackColor = System.Drawing.Color.DarkGray;
            this.viewModeTabControl.BeforeTouchSize = new System.Drawing.Size(863, 493);
            this.viewModeTabControl.CloseButtonForeColor = System.Drawing.Color.Empty;
            this.viewModeTabControl.CloseButtonHoverForeColor = System.Drawing.Color.Empty;
            this.viewModeTabControl.CloseButtonPressedForeColor = System.Drawing.Color.Empty;
            this.viewModeTabControl.Controls.Add(this.graphicalViewTab);
            this.viewModeTabControl.Controls.Add(this.realTimeTab);
            this.viewModeTabControl.Controls.Add(this.driveTableTab);
            this.viewModeTabControl.Controls.Add(this.passFailTableTab);
            this.viewModeTabControl.Controls.Add(this.commentsTab);
            this.viewModeTabControl.Controls.Add(this.printMeTab);
            this.viewModeTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewModeTabControl.InActiveTabForeColor = System.Drawing.Color.Empty;
            this.viewModeTabControl.Location = new System.Drawing.Point(0, 0);
            this.viewModeTabControl.Name = "viewModeTabControl";
            this.viewModeTabControl.SeparatorColor = System.Drawing.SystemColors.ControlDark;
            this.viewModeTabControl.ShowSeparator = false;
            this.viewModeTabControl.Size = new System.Drawing.Size(863, 493);
            this.viewModeTabControl.TabIndex = 37;
            this.viewModeTabControl.TabStyle = typeof(Syncfusion.Windows.Forms.Tools.TabRendererDockingWhidbeyBeta);
            // 
            // graphicalViewTab
            // 
            this.graphicalViewTab.BackColor = System.Drawing.Color.DarkGray;
            this.graphicalViewTab.Controls.Add(this.graphDockPanel);
            this.graphicalViewTab.Controls.Add(this.nicePanel4);
            this.graphicalViewTab.Controls.Add(this.fixedPanel);
            this.graphicalViewTab.Controls.Add(this.legendPanel);
            this.graphicalViewTab.Image = global::GST.Gearshift.Components.Properties.Resources.ReportViewerPanel_currents_32x32;
            this.graphicalViewTab.ImageSize = new System.Drawing.Size(16, 16);
            this.graphicalViewTab.Location = new System.Drawing.Point(1, 33);
            this.graphicalViewTab.Name = "graphicalViewTab";
            this.graphicalViewTab.ShowCloseButton = true;
            this.graphicalViewTab.Size = new System.Drawing.Size(860, 458);
            this.graphicalViewTab.TabIndex = 1;
            this.graphicalViewTab.Text = "Graphical view";
            this.graphicalViewTab.ThemesEnabled = false;
            // 
            // graphDockPanel
            // 
            this.graphDockPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphDockPanel.AutoScroll = true;
            this.graphDockPanel.BackColor = System.Drawing.Color.Transparent;
            this.graphDockPanel.Controls.Add(this.graphicalPressureZedGraph);
            this.graphDockPanel.Location = new System.Drawing.Point(0, 0);
            this.graphDockPanel.Name = "graphDockPanel";
            this.graphDockPanel.Size = new System.Drawing.Size(669, 464);
            this.graphDockPanel.TabIndex = 7;
            this.graphDockPanel.Resize += new System.EventHandler(this.OnAdjustGraph);
            // 
            // graphicalPressureZedGraph
            // 
            this.graphicalPressureZedGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphicalPressureZedGraph.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.graphicalPressureZedGraph.IsShowPointValues = true;
            this.graphicalPressureZedGraph.Location = new System.Drawing.Point(0, 0);
            this.graphicalPressureZedGraph.Name = "graphicalPressureZedGraph";
            this.graphicalPressureZedGraph.ScrollGrace = 0D;
            this.graphicalPressureZedGraph.ScrollMaxX = 0D;
            this.graphicalPressureZedGraph.ScrollMaxY = 0D;
            this.graphicalPressureZedGraph.ScrollMaxY2 = 0D;
            this.graphicalPressureZedGraph.ScrollMinX = 0D;
            this.graphicalPressureZedGraph.ScrollMinY = 0D;
            this.graphicalPressureZedGraph.ScrollMinY2 = 0D;
            this.graphicalPressureZedGraph.Size = new System.Drawing.Size(669, 464);
            this.graphicalPressureZedGraph.TabIndex = 3;
            // 
            // nicePanel4
            // 
            this.nicePanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nicePanel4.AutoplaceElements = false;
            this.nicePanel4.AutoScrollHorizontalMaximum = 100;
            this.nicePanel4.AutoScrollHorizontalMinimum = 0;
            this.nicePanel4.AutoScrollHPos = 0;
            this.nicePanel4.AutoScrollVerticalMaximum = 100;
            this.nicePanel4.AutoScrollVerticalMinimum = 0;
            this.nicePanel4.AutoScrollVPos = 0;
            this.nicePanel4.AutoSizeElements = false;
            this.nicePanel4.backgroundColor1 = System.Drawing.Color.DarkGray;
            this.nicePanel4.backgroundColor2 = System.Drawing.Color.Gainsboro;
            this.nicePanel4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
            this.nicePanel4.BorderWidth = 1;
            this.nicePanel4.Controls.Add(this.pageSpanMinus);
            this.nicePanel4.Controls.Add(this.pageSpanLabel);
            this.nicePanel4.Controls.Add(this.pageSpanAdd);
            this.nicePanel4.DrawBackImage = false;
            this.nicePanel4.EnableAutoScrollHorizontal = false;
            this.nicePanel4.EnableAutoScrollVertical = false;
            this.nicePanel4.Gradient = Soko.Common.Controls.NicePanel.GradientType.SigmaBellHorizontalUpper;
            this.nicePanel4.HorizontalMargin = 0;
            this.nicePanel4.Location = new System.Drawing.Point(672, 419);
            this.nicePanel4.Name = "nicePanel4";
            this.nicePanel4.roundingRadius = 10;
            this.nicePanel4.Size = new System.Drawing.Size(183, 45);
            this.nicePanel4.SupportTransparentBackground = false;
            this.nicePanel4.TabIndex = 6;
            this.nicePanel4.VerticalMargin = 0;
            this.nicePanel4.VisibleAutoScrollHorizontal = false;
            this.nicePanel4.VisibleAutoScrollVertical = false;
            // 
            // pageSpanMinus
            // 
            this.pageSpanMinus.Image = global::GST.Gearshift.Components.Properties.Resources.AnalogInputChannelConfigPanel_Minus_32x32;
            this.pageSpanMinus.Location = new System.Drawing.Point(41, 8);
            this.pageSpanMinus.Name = "pageSpanMinus";
            this.pageSpanMinus.Size = new System.Drawing.Size(30, 30);
            this.pageSpanMinus.TabIndex = 3;
            this.pageSpanMinus.UseVisualStyleBackColor = true;
            this.pageSpanMinus.Click += new System.EventHandler(this.pageSpanMinus_Click);
            // 
            // pageSpanLabel
            // 
            this.pageSpanLabel.BackColor = System.Drawing.Color.Transparent;
            this.pageSpanLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pageSpanLabel.Location = new System.Drawing.Point(77, 8);
            this.pageSpanLabel.Name = "pageSpanLabel";
            this.pageSpanLabel.Size = new System.Drawing.Size(95, 30);
            this.pageSpanLabel.TabIndex = 3;
            this.pageSpanLabel.Text = "Span: 1 page";
            this.pageSpanLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pageSpanAdd
            // 
            this.pageSpanAdd.Image = global::GST.Gearshift.Components.Properties.Resources.AnalogInputChannelConfigPanel_Add_32x32;
            this.pageSpanAdd.Location = new System.Drawing.Point(10, 8);
            this.pageSpanAdd.Name = "pageSpanAdd";
            this.pageSpanAdd.Size = new System.Drawing.Size(30, 30);
            this.pageSpanAdd.TabIndex = 2;
            this.pageSpanAdd.UseVisualStyleBackColor = true;
            this.pageSpanAdd.Click += new System.EventHandler(this.pageSpanAdd_Click);
            // 
            // fixedPanel
            // 
            this.fixedPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fixedPanel.AutoplaceElements = false;
            this.fixedPanel.AutoScrollHorizontalMaximum = 100;
            this.fixedPanel.AutoScrollHorizontalMinimum = 0;
            this.fixedPanel.AutoScrollHPos = 0;
            this.fixedPanel.AutoScrollVerticalMaximum = 100;
            this.fixedPanel.AutoScrollVerticalMinimum = 0;
            this.fixedPanel.AutoScrollVPos = 0;
            this.fixedPanel.AutoSizeElements = false;
            this.fixedPanel.backgroundColor1 = System.Drawing.Color.DarkGray;
            this.fixedPanel.backgroundColor2 = System.Drawing.Color.Gainsboro;
            this.fixedPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
            this.fixedPanel.BorderWidth = 1;
            this.fixedPanel.Controls.Add(this.showRefCb);
            this.fixedPanel.Controls.Add(this.allOnOffCb);
            this.fixedPanel.Controls.Add(this.setRefButton);
            this.fixedPanel.DrawBackImage = false;
            this.fixedPanel.EnableAutoScrollHorizontal = false;
            this.fixedPanel.EnableAutoScrollVertical = false;
            this.fixedPanel.Gradient = Soko.Common.Controls.NicePanel.GradientType.SigmaBellHorizontalUpper;
            this.fixedPanel.HorizontalMargin = 0;
            this.fixedPanel.Location = new System.Drawing.Point(672, 0);
            this.fixedPanel.Name = "fixedPanel";
            this.fixedPanel.roundingRadius = 10;
            this.fixedPanel.Size = new System.Drawing.Size(183, 119);
            this.fixedPanel.SupportTransparentBackground = false;
            this.fixedPanel.TabIndex = 5;
            this.fixedPanel.VerticalMargin = 0;
            this.fixedPanel.VisibleAutoScrollHorizontal = false;
            this.fixedPanel.VisibleAutoScrollVertical = false;
            // 
            // showRefCb
            // 
            this.showRefCb.Appearance = System.Windows.Forms.Appearance.Button;
            this.showRefCb.BackColor = System.Drawing.Color.Transparent;
            this.showRefCb.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.showRefCb.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.showRefCb.ForeColor = System.Drawing.Color.Black;
            this.showRefCb.Location = new System.Drawing.Point(10, 44);
            this.showRefCb.Name = "showRefCb";
            this.showRefCb.Size = new System.Drawing.Size(166, 30);
            this.showRefCb.TabIndex = 3;
            this.showRefCb.Text = "Show Reference";
            this.showRefCb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.showRefCb.UseVisualStyleBackColor = false;
            this.showRefCb.CheckedChanged += new System.EventHandler(this.RefOnOffCbCheckedChanged);
            // 
            // allOnOffCb
            // 
            this.allOnOffCb.Appearance = System.Windows.Forms.Appearance.Button;
            this.allOnOffCb.BackColor = System.Drawing.Color.Transparent;
            this.allOnOffCb.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.allOnOffCb.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.allOnOffCb.ForeColor = System.Drawing.Color.Black;
            this.allOnOffCb.Location = new System.Drawing.Point(10, 80);
            this.allOnOffCb.Name = "allOnOffCb";
            this.allOnOffCb.Size = new System.Drawing.Size(166, 30);
            this.allOnOffCb.TabIndex = 2;
            this.allOnOffCb.Text = "Toggle all";
            this.allOnOffCb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.allOnOffCb.UseVisualStyleBackColor = false;
            this.allOnOffCb.CheckedChanged += new System.EventHandler(this.AllOnOffCbCheckedChanged);
            // 
            // setRefButton
            // 
            this.setRefButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.setRefButton.ForeColor = System.Drawing.Color.Black;
            this.setRefButton.Location = new System.Drawing.Point(10, 8);
            this.setRefButton.Name = "setRefButton";
            this.setRefButton.Size = new System.Drawing.Size(166, 30);
            this.setRefButton.TabIndex = 1;
            this.setRefButton.Text = "Set reference";
            this.setRefButton.UseVisualStyleBackColor = true;
            this.setRefButton.Click += new System.EventHandler(this.setRefButton_Click);
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
            this.legendPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
            this.legendPanel.BorderWidth = 1;
            this.legendPanel.Controls.Add(this.panel1);
            this.legendPanel.DrawBackImage = false;
            this.legendPanel.EnableAutoScrollHorizontal = false;
            this.legendPanel.EnableAutoScrollVertical = false;
            this.legendPanel.Gradient = Soko.Common.Controls.NicePanel.GradientType.SigmaBellHorizontalUpper;
            this.legendPanel.HorizontalMargin = 0;
            this.legendPanel.Location = new System.Drawing.Point(672, 119);
            this.legendPanel.Name = "legendPanel";
            this.legendPanel.roundingRadius = 10;
            this.legendPanel.Size = new System.Drawing.Size(183, 295);
            this.legendPanel.SupportTransparentBackground = false;
            this.legendPanel.TabIndex = 4;
            this.legendPanel.VerticalMargin = 0;
            this.legendPanel.VisibleAutoScrollHorizontal = false;
            this.legendPanel.VisibleAutoScrollVertical = false;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.checkBox2);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Location = new System.Drawing.Point(13, 129);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(157, 36);
            this.panel1.TabIndex = 1;
            // 
            // checkBox2
            // 
            this.checkBox2.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox2.Image = global::GST.Gearshift.Components.Properties.Resources.ReportViewerPanel_printer_off;
            this.checkBox2.Location = new System.Drawing.Point(124, 3);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(30, 30);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox1.BackColor = System.Drawing.Color.Transparent;
            this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.checkBox1.ForeColor = System.Drawing.Color.Orange;
            this.checkBox1.Location = new System.Drawing.Point(3, 3);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(120, 30);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox1.UseVisualStyleBackColor = false;
            // 
            // realTimeTab
            // 
            this.realTimeTab.Controls.Add(this.livePreviewPanel1);
            this.realTimeTab.Image = global::GST.Gearshift.Components.Properties.Resources.ReportViewerPanel_currents_32x32;
            this.realTimeTab.ImageSize = new System.Drawing.Size(16, 16);
            this.realTimeTab.Location = new System.Drawing.Point(1, 33);
            this.realTimeTab.Name = "realTimeTab";
            this.realTimeTab.ShowCloseButton = true;
            this.realTimeTab.Size = new System.Drawing.Size(860, 458);
            this.realTimeTab.TabIndex = 5;
            this.realTimeTab.Text = "Real time view";
            this.realTimeTab.ThemesEnabled = false;
            // 
            // livePreviewPanel1
            // 
            this.livePreviewPanel1.DataPlaybackMode = GST.Gearshift.Components.Forms.DAQ.LivePreviewPanel.PlaybackMode.ReportViewer;
            this.livePreviewPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.livePreviewPanel1.Location = new System.Drawing.Point(0, 0);
            this.livePreviewPanel1.Name = "livePreviewPanel1";
            this.livePreviewPanel1.SampleWindowSize = 1000;
            this.livePreviewPanel1.Size = new System.Drawing.Size(860, 458);
            this.livePreviewPanel1.TabIndex = 0;
            // 
            // driveTableTab
            // 
            this.driveTableTab.BackColor = System.Drawing.Color.DarkGray;
            this.driveTableTab.Controls.Add(this.driveTable);
            this.driveTableTab.Image = global::GST.Gearshift.Components.Properties.Resources.ReportViewerPanel_application_table;
            this.driveTableTab.ImageSize = new System.Drawing.Size(16, 16);
            this.driveTableTab.Location = new System.Drawing.Point(1, 33);
            this.driveTableTab.Name = "driveTableTab";
            this.driveTableTab.ShowCloseButton = true;
            this.driveTableTab.Size = new System.Drawing.Size(860, 458);
            this.driveTableTab.TabIndex = 6;
            this.driveTableTab.Text = "Drive table";
            this.driveTableTab.ThemesEnabled = false;
            // 
            // driveTable
            // 
            this.driveTable.AlternatingRowColor = System.Drawing.Color.WhiteSmoke;
            this.driveTable.BackColor = System.Drawing.Color.WhiteSmoke;
            this.driveTable.BorderColor = System.Drawing.Color.Black;
            this.driveTable.DataMember = null;
            this.driveTable.DataSourceColumnBinder = dataSourceColumnBinder3;
            this.driveTable.Dock = System.Windows.Forms.DockStyle.Fill;
            dragDropRenderer3.ForeColor = System.Drawing.Color.Red;
            this.driveTable.DragDropRenderer = dragDropRenderer3;
            this.driveTable.EditStartAction = XPTable.Editors.EditStartAction.SingleClick;
            this.driveTable.EnableToolTips = true;
            this.driveTable.EnableWordWrap = true;
            this.driveTable.Font = new System.Drawing.Font("Consolas", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.driveTable.ForeColor = System.Drawing.Color.Black;
            this.driveTable.FullRowSelect = true;
            this.driveTable.GridColor = System.Drawing.Color.DarkGray;
            this.driveTable.GridLines = XPTable.Models.GridLines.Columns;
            this.driveTable.GridLinesContrainedToData = false;
            this.driveTable.GridLineStyle = XPTable.Models.GridLineStyle.Dot;
            this.driveTable.HeaderAlignWithColumn = true;
            this.driveTable.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.driveTable.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.driveTable.Location = new System.Drawing.Point(0, 0);
            this.driveTable.Name = "driveTable";
            this.driveTable.NoItemsText = "";
            this.driveTable.SelectionStyle = XPTable.Models.SelectionStyle.Grid;
            this.driveTable.Size = new System.Drawing.Size(860, 458);
            this.driveTable.TabIndex = 35;
            this.driveTable.Text = "table1";
            this.driveTable.ToolTipInitialDelay = 500;
            this.driveTable.ToolTipShowAlways = true;
            this.driveTable.UnfocusedBorderColor = System.Drawing.Color.Black;
            // 
            // passFailTableTab
            // 
            this.passFailTableTab.Controls.Add(this.passFailTable);
            this.passFailTableTab.Image = global::GST.Gearshift.Components.Properties.Resources.ReportViewerPanel_check;
            this.passFailTableTab.ImageSize = new System.Drawing.Size(16, 16);
            this.passFailTableTab.Location = new System.Drawing.Point(1, 33);
            this.passFailTableTab.Name = "passFailTableTab";
            this.passFailTableTab.ShowCloseButton = true;
            this.passFailTableTab.Size = new System.Drawing.Size(860, 458);
            this.passFailTableTab.TabIndex = 4;
            this.passFailTableTab.Text = "Limits table";
            this.passFailTableTab.ThemesEnabled = false;
            // 
            // passFailTable
            // 
            this.passFailTable.AutoCalculateRowHeights = true;
            this.passFailTable.BackColor = System.Drawing.Color.WhiteSmoke;
            this.passFailTable.BorderColor = System.Drawing.Color.Black;
            this.passFailTable.DataMember = null;
            this.passFailTable.DataSourceColumnBinder = dataSourceColumnBinder4;
            this.passFailTable.Dock = System.Windows.Forms.DockStyle.Fill;
            dragDropRenderer4.ForeColor = System.Drawing.Color.Red;
            this.passFailTable.DragDropRenderer = dragDropRenderer4;
            this.passFailTable.EditStartAction = XPTable.Editors.EditStartAction.SingleClick;
            this.passFailTable.EnableHeaderContextMenu = false;
            this.passFailTable.EnableToolTips = true;
            this.passFailTable.EnableWordWrap = true;
            this.passFailTable.Font = new System.Drawing.Font("Consolas", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.passFailTable.ForeColor = System.Drawing.Color.Black;
            this.passFailTable.FullRowSelect = true;
            this.passFailTable.GridColor = System.Drawing.Color.DarkGray;
            this.passFailTable.GridLines = XPTable.Models.GridLines.Columns;
            this.passFailTable.GridLinesContrainedToData = false;
            this.passFailTable.GridLineStyle = XPTable.Models.GridLineStyle.Dot;
            this.passFailTable.HeaderAlignWithColumn = true;
            this.passFailTable.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.passFailTable.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.passFailTable.Location = new System.Drawing.Point(0, 0);
            this.passFailTable.Name = "passFailTable";
            this.passFailTable.NoItemsText = "";
            this.passFailTable.SelectionStyle = XPTable.Models.SelectionStyle.Grid;
            this.passFailTable.Size = new System.Drawing.Size(860, 458);
            this.passFailTable.TabIndex = 36;
            this.passFailTable.Text = "table1";
            this.passFailTable.ToolTipInitialDelay = 500;
            this.passFailTable.ToolTipShowAlways = true;
            this.passFailTable.UnfocusedBorderColor = System.Drawing.Color.Black;
            // 
            // commentsTab
            // 
            this.commentsTab.Controls.Add(this.nicePanel2);
            this.commentsTab.Image = global::GST.Gearshift.Components.Properties.Resources.ReportViewerPanel_get_info1;
            this.commentsTab.ImageSize = new System.Drawing.Size(16, 16);
            this.commentsTab.Location = new System.Drawing.Point(1, 33);
            this.commentsTab.Name = "commentsTab";
            this.commentsTab.ShowCloseButton = true;
            this.commentsTab.Size = new System.Drawing.Size(860, 458);
            this.commentsTab.TabIndex = 3;
            this.commentsTab.Text = "Comments";
            this.commentsTab.ThemesEnabled = false;
            // 
            // nicePanel2
            // 
            this.nicePanel2.AutoplaceElements = true;
            this.nicePanel2.AutoScrollHorizontalMaximum = 100;
            this.nicePanel2.AutoScrollHorizontalMinimum = 0;
            this.nicePanel2.AutoScrollHPos = 0;
            this.nicePanel2.AutoScrollVerticalMaximum = 100;
            this.nicePanel2.AutoScrollVerticalMinimum = 0;
            this.nicePanel2.AutoScrollVPos = 0;
            this.nicePanel2.AutoSizeElements = false;
            this.nicePanel2.backgroundColor1 = System.Drawing.Color.Gainsboro;
            this.nicePanel2.backgroundColor2 = System.Drawing.Color.DarkGray;
            this.nicePanel2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
            this.nicePanel2.BorderWidth = 1;
            this.nicePanel2.Controls.Add(this.nicePanel3);
            this.nicePanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nicePanel2.DrawBackImage = false;
            this.nicePanel2.EnableAutoScrollHorizontal = false;
            this.nicePanel2.EnableAutoScrollVertical = false;
            this.nicePanel2.Gradient = Soko.Common.Controls.NicePanel.GradientType.SigmaBellHorizontalUpper;
            this.nicePanel2.HorizontalMargin = 0;
            this.nicePanel2.Location = new System.Drawing.Point(0, 0);
            this.nicePanel2.Name = "nicePanel2";
            this.nicePanel2.roundingRadius = 10;
            this.nicePanel2.Size = new System.Drawing.Size(860, 458);
            this.nicePanel2.SupportTransparentBackground = false;
            this.nicePanel2.TabIndex = 1;
            this.nicePanel2.VerticalMargin = 0;
            this.nicePanel2.VisibleAutoScrollHorizontal = false;
            this.nicePanel2.VisibleAutoScrollVertical = false;
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
            this.nicePanel3.BackColor = System.Drawing.Color.DimGray;
            this.nicePanel3.backgroundColor1 = System.Drawing.Color.WhiteSmoke;
            this.nicePanel3.backgroundColor2 = System.Drawing.Color.Wheat;
            this.nicePanel3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
            this.nicePanel3.BorderWidth = 1;
            this.nicePanel3.Controls.Add(this.saveCommentsButton);
            this.nicePanel3.Controls.Add(this.imageButton1);
            this.nicePanel3.Controls.Add(this.operatorNameLabel);
            this.nicePanel3.Controls.Add(this.commentLabel);
            this.nicePanel3.Controls.Add(this.testNameLabel);
            this.nicePanel3.Controls.Add(this.testTimestampLabel);
            this.nicePanel3.Controls.Add(this.serialNoLabel);
            this.nicePanel3.Controls.Add(this.commentTextBox);
            this.nicePanel3.Controls.Add(this.testNameTextBox);
            this.nicePanel3.Controls.Add(this.operatorTextBox);
            this.nicePanel3.Controls.Add(this.timestampTextBox);
            this.nicePanel3.Controls.Add(this.serialNoTextBox);
            this.nicePanel3.DrawBackImage = false;
            this.nicePanel3.EnableAutoScrollHorizontal = false;
            this.nicePanel3.EnableAutoScrollVertical = false;
            this.nicePanel3.Gradient = Soko.Common.Controls.NicePanel.GradientType.None;
            this.nicePanel3.HorizontalMargin = 0;
            this.nicePanel3.Location = new System.Drawing.Point(27, 29);
            this.nicePanel3.Name = "nicePanel3";
            this.nicePanel3.roundingRadius = 10;
            this.nicePanel3.Size = new System.Drawing.Size(805, 400);
            this.nicePanel3.SupportTransparentBackground = false;
            this.nicePanel3.TabIndex = 0;
            this.nicePanel3.VerticalMargin = 0;
            this.nicePanel3.VisibleAutoScrollHorizontal = false;
            this.nicePanel3.VisibleAutoScrollVertical = false;
            // 
            // saveCommentsButton
            // 
            this.saveCommentsButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.saveCommentsButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.saveCommentsButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.saveCommentsButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.saveCommentsButton.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.saveCommentsButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.saveCommentsButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.saveCommentsButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.saveCommentsButton.BorderWidth = 1;
            this.saveCommentsButton.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.saveCommentsButton.ContentPadding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.saveCommentsButton.DrawBackColorOnFocus = false;
            this.saveCommentsButton.DrawBackgroundImage = false;
            this.saveCommentsButton.DrawBorderOnFocus = false;
            this.saveCommentsButton.DrawBorderOnTop = false;
            this.saveCommentsButton.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.saveCommentsButton.ForeColor = System.Drawing.Color.White;
            this.saveCommentsButton.Image = global::GST.Gearshift.Components.Properties.Resources.ReportViewerPanel_Save_48x48;
            this.saveCommentsButton.ImageDisabled = ((System.Drawing.Image)(resources.GetObject("saveCommentsButton.ImageDisabled")));
            this.saveCommentsButton.Location = new System.Drawing.Point(263, 315);
            this.saveCommentsButton.Name = "saveCommentsButton";
            this.saveCommentsButton.Size = new System.Drawing.Size(245, 60);
            this.saveCommentsButton.SupportTransparentBackground = false;
            this.saveCommentsButton.TabIndex = 20;
            this.saveCommentsButton.Text = "Save edited comments";
            this.saveCommentsButton.TextImageRelation = Soko.Common.Controls.TextRelation.OnRight;
            this.saveCommentsButton.TextImageSpacing = 5;
            this.saveCommentsButton.Click += new System.EventHandler(this.saveCommentsButton_Click);
            // 
            // imageButton1
            // 
            this.imageButton1.BackColorOnClicked1 = System.Drawing.Color.Orange;
            this.imageButton1.BackColorOnClicked2 = System.Drawing.Color.Orange;
            this.imageButton1.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(217)))), ((int)(((byte)(72)))));
            this.imageButton1.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(238)))), ((int)(((byte)(176)))));
            this.imageButton1.BackgroundColor = System.Drawing.Color.DimGray;
            this.imageButton1.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.imageButton1.BorderColor = System.Drawing.Color.Transparent;
            this.imageButton1.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(217)))), ((int)(((byte)(72)))));
            this.imageButton1.BorderWidth = 1;
            this.imageButton1.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.imageButton1.ContentPadding = new System.Windows.Forms.Padding(0);
            this.imageButton1.DrawBackColorOnFocus = false;
            this.imageButton1.DrawBackgroundImage = false;
            this.imageButton1.DrawBorderOnFocus = false;
            this.imageButton1.DrawBorderOnTop = false;
            this.imageButton1.Enabled = false;
            this.imageButton1.Image = global::GST.Gearshift.Components.Properties.Resources.ReportViewerPanel_info;
            this.imageButton1.ImageDisabled = global::GST.Gearshift.Components.Properties.Resources.ReportViewerPanel_info;
            this.imageButton1.Location = new System.Drawing.Point(50, 58);
            this.imageButton1.Name = "imageButton1";
            this.imageButton1.Size = new System.Drawing.Size(128, 128);
            this.imageButton1.SupportTransparentBackground = false;
            this.imageButton1.TabIndex = 19;
            this.imageButton1.TextImageRelation = Soko.Common.Controls.TextRelation.Underneath;
            this.imageButton1.TextImageSpacing = 0;
            // 
            // operatorNameLabel
            // 
            this.operatorNameLabel.AutoSize = true;
            this.operatorNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.operatorNameLabel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.operatorNameLabel.ForeColor = System.Drawing.Color.Black;
            this.operatorNameLabel.Location = new System.Drawing.Point(260, 134);
            this.operatorNameLabel.Name = "operatorNameLabel";
            this.operatorNameLabel.Size = new System.Drawing.Size(111, 19);
            this.operatorNameLabel.TabIndex = 18;
            this.operatorNameLabel.Text = "Operator name:";
            // 
            // commentLabel
            // 
            this.commentLabel.AutoSize = true;
            this.commentLabel.BackColor = System.Drawing.Color.Transparent;
            this.commentLabel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.commentLabel.ForeColor = System.Drawing.Color.Black;
            this.commentLabel.Location = new System.Drawing.Point(260, 208);
            this.commentLabel.Name = "commentLabel";
            this.commentLabel.Size = new System.Drawing.Size(75, 19);
            this.commentLabel.TabIndex = 17;
            this.commentLabel.Text = "Comment:";
            // 
            // testNameLabel
            // 
            this.testNameLabel.AutoSize = true;
            this.testNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.testNameLabel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.testNameLabel.ForeColor = System.Drawing.Color.Black;
            this.testNameLabel.Location = new System.Drawing.Point(260, 58);
            this.testNameLabel.Name = "testNameLabel";
            this.testNameLabel.Size = new System.Drawing.Size(80, 19);
            this.testNameLabel.TabIndex = 16;
            this.testNameLabel.Text = "Test name:";
            // 
            // testTimestampLabel
            // 
            this.testTimestampLabel.AutoSize = true;
            this.testTimestampLabel.BackColor = System.Drawing.Color.Transparent;
            this.testTimestampLabel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.testTimestampLabel.ForeColor = System.Drawing.Color.Black;
            this.testTimestampLabel.Location = new System.Drawing.Point(260, 96);
            this.testTimestampLabel.Name = "testTimestampLabel";
            this.testTimestampLabel.Size = new System.Drawing.Size(113, 19);
            this.testTimestampLabel.TabIndex = 16;
            this.testTimestampLabel.Text = "Test timestamp:";
            // 
            // serialNoLabel
            // 
            this.serialNoLabel.AutoSize = true;
            this.serialNoLabel.BackColor = System.Drawing.Color.Transparent;
            this.serialNoLabel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.serialNoLabel.ForeColor = System.Drawing.Color.Black;
            this.serialNoLabel.Location = new System.Drawing.Point(260, 171);
            this.serialNoLabel.Name = "serialNoLabel";
            this.serialNoLabel.Size = new System.Drawing.Size(160, 19);
            this.serialNoLabel.TabIndex = 15;
            this.serialNoLabel.Text = "Gearbox serial number:";
            // 
            // commentTextBox
            // 
            this.commentTextBox.BackAlpha = 5;
            this.commentTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.commentTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.commentTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.commentTextBox.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commentTextBox.ForeColor = System.Drawing.Color.White;
            this.commentTextBox.Location = new System.Drawing.Point(514, 208);
            this.commentTextBox.Multiline = true;
            this.commentTextBox.Name = "commentTextBox";
            this.commentTextBox.Size = new System.Drawing.Size(252, 167);
            this.commentTextBox.TabIndex = 14;
            // 
            // testNameTextBox
            // 
            this.testNameTextBox.BackAlpha = 5;
            this.testNameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.testNameTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.testNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.testNameTextBox.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.testNameTextBox.ForeColor = System.Drawing.Color.White;
            this.testNameTextBox.Location = new System.Drawing.Point(514, 58);
            this.testNameTextBox.Name = "testNameTextBox";
            this.testNameTextBox.ReadOnly = true;
            this.testNameTextBox.Size = new System.Drawing.Size(252, 20);
            this.testNameTextBox.TabIndex = 12;
            // 
            // operatorTextBox
            // 
            this.operatorTextBox.BackAlpha = 5;
            this.operatorTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.operatorTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.operatorTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.operatorTextBox.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.operatorTextBox.ForeColor = System.Drawing.Color.White;
            this.operatorTextBox.Location = new System.Drawing.Point(514, 134);
            this.operatorTextBox.Name = "operatorTextBox";
            this.operatorTextBox.ReadOnly = true;
            this.operatorTextBox.Size = new System.Drawing.Size(252, 20);
            this.operatorTextBox.TabIndex = 12;
            // 
            // timestampTextBox
            // 
            this.timestampTextBox.BackAlpha = 5;
            this.timestampTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.timestampTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.timestampTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.timestampTextBox.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timestampTextBox.ForeColor = System.Drawing.Color.White;
            this.timestampTextBox.Location = new System.Drawing.Point(514, 96);
            this.timestampTextBox.Name = "timestampTextBox";
            this.timestampTextBox.ReadOnly = true;
            this.timestampTextBox.Size = new System.Drawing.Size(252, 20);
            this.timestampTextBox.TabIndex = 12;
            // 
            // serialNoTextBox
            // 
            this.serialNoTextBox.BackAlpha = 5;
            this.serialNoTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.serialNoTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.serialNoTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.serialNoTextBox.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serialNoTextBox.ForeColor = System.Drawing.Color.White;
            this.serialNoTextBox.Location = new System.Drawing.Point(514, 171);
            this.serialNoTextBox.Name = "serialNoTextBox";
            this.serialNoTextBox.ReadOnly = true;
            this.serialNoTextBox.Size = new System.Drawing.Size(252, 20);
            this.serialNoTextBox.TabIndex = 13;
            // 
            // printMeTab
            // 
            this.printMeTab.Controls.Add(this.nicePanel1);
            this.printMeTab.Image = global::GST.Gearshift.Components.Properties.Resources.ReportViewerPanel_printer_off;
            this.printMeTab.ImageSize = new System.Drawing.Size(16, 16);
            this.printMeTab.Location = new System.Drawing.Point(1, 33);
            this.printMeTab.Name = "printMeTab";
            this.printMeTab.ShowCloseButton = true;
            this.printMeTab.Size = new System.Drawing.Size(860, 458);
            this.printMeTab.TabIndex = 2;
            this.printMeTab.Text = "Print me";
            this.printMeTab.ThemesEnabled = false;
            // 
            // nicePanel1
            // 
            this.nicePanel1.AutoplaceElements = true;
            this.nicePanel1.AutoScrollHorizontalMaximum = 100;
            this.nicePanel1.AutoScrollHorizontalMinimum = 0;
            this.nicePanel1.AutoScrollHPos = 0;
            this.nicePanel1.AutoScrollVerticalMaximum = 100;
            this.nicePanel1.AutoScrollVerticalMinimum = 0;
            this.nicePanel1.AutoScrollVPos = 0;
            this.nicePanel1.AutoSizeElements = false;
            this.nicePanel1.backgroundColor1 = System.Drawing.Color.Gainsboro;
            this.nicePanel1.backgroundColor2 = System.Drawing.Color.DarkGray;
            this.nicePanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
            this.nicePanel1.BorderWidth = 1;
            this.nicePanel1.Controls.Add(this.printReportWithTablesButton);
            this.nicePanel1.Controls.Add(this.printReportGraphsOnlyButton);
            this.nicePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nicePanel1.DrawBackImage = false;
            this.nicePanel1.EnableAutoScrollHorizontal = false;
            this.nicePanel1.EnableAutoScrollVertical = false;
            this.nicePanel1.Gradient = Soko.Common.Controls.NicePanel.GradientType.SigmaBellHorizontalUpper;
            this.nicePanel1.HorizontalMargin = 0;
            this.nicePanel1.Location = new System.Drawing.Point(0, 0);
            this.nicePanel1.Name = "nicePanel1";
            this.nicePanel1.roundingRadius = 10;
            this.nicePanel1.Size = new System.Drawing.Size(860, 458);
            this.nicePanel1.SupportTransparentBackground = false;
            this.nicePanel1.TabIndex = 0;
            this.nicePanel1.VerticalMargin = 0;
            this.nicePanel1.VisibleAutoScrollHorizontal = false;
            this.nicePanel1.VisibleAutoScrollVertical = false;
            // 
            // printReportWithTablesButton
            // 
            this.printReportWithTablesButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.printReportWithTablesButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.printReportWithTablesButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.printReportWithTablesButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.printReportWithTablesButton.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.printReportWithTablesButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.printReportWithTablesButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.printReportWithTablesButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.printReportWithTablesButton.BorderWidth = 1;
            this.printReportWithTablesButton.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.printReportWithTablesButton.ContentPadding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.printReportWithTablesButton.DrawBackColorOnFocus = false;
            this.printReportWithTablesButton.DrawBackgroundImage = false;
            this.printReportWithTablesButton.DrawBorderOnFocus = false;
            this.printReportWithTablesButton.DrawBorderOnTop = false;
            this.printReportWithTablesButton.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.printReportWithTablesButton.ForeColor = System.Drawing.Color.White;
            this.printReportWithTablesButton.Image = global::GST.Gearshift.Components.Properties.Resources.ReportViewerPanel_printer_paper_256x256;
            this.printReportWithTablesButton.ImageDisabled = ((System.Drawing.Image)(resources.GetObject("printReportWithTablesButton.ImageDisabled")));
            this.printReportWithTablesButton.Location = new System.Drawing.Point(202, 1);
            this.printReportWithTablesButton.Name = "printReportWithTablesButton";
            this.printReportWithTablesButton.Size = new System.Drawing.Size(455, 227);
            this.printReportWithTablesButton.SupportTransparentBackground = false;
            this.printReportWithTablesButton.TabIndex = 1;
            this.printReportWithTablesButton.Text = "Print with table data";
            this.printReportWithTablesButton.TextImageRelation = Soko.Common.Controls.TextRelation.OnRight;
            this.printReportWithTablesButton.TextImageSpacing = 5;
            this.printReportWithTablesButton.Click += new System.EventHandler(this.PrintReportWithTablesButton_Click);
            // 
            // printReportGraphsOnlyButton
            // 
            this.printReportGraphsOnlyButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.printReportGraphsOnlyButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.printReportGraphsOnlyButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.printReportGraphsOnlyButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.printReportGraphsOnlyButton.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.printReportGraphsOnlyButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.printReportGraphsOnlyButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.printReportGraphsOnlyButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.printReportGraphsOnlyButton.BorderWidth = 1;
            this.printReportGraphsOnlyButton.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.printReportGraphsOnlyButton.ContentPadding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.printReportGraphsOnlyButton.DrawBackColorOnFocus = false;
            this.printReportGraphsOnlyButton.DrawBackgroundImage = false;
            this.printReportGraphsOnlyButton.DrawBorderOnFocus = false;
            this.printReportGraphsOnlyButton.DrawBorderOnTop = false;
            this.printReportGraphsOnlyButton.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.printReportGraphsOnlyButton.ForeColor = System.Drawing.Color.White;
            this.printReportGraphsOnlyButton.Image = global::GST.Gearshift.Components.Properties.Resources.ReportViewerPanel_printer_paper_256x256;
            this.printReportGraphsOnlyButton.ImageDisabled = ((System.Drawing.Image)(resources.GetObject("printReportGraphsOnlyButton.ImageDisabled")));
            this.printReportGraphsOnlyButton.Location = new System.Drawing.Point(202, 230);
            this.printReportGraphsOnlyButton.Name = "printReportGraphsOnlyButton";
            this.printReportGraphsOnlyButton.Size = new System.Drawing.Size(455, 227);
            this.printReportGraphsOnlyButton.SupportTransparentBackground = false;
            this.printReportGraphsOnlyButton.TabIndex = 0;
            this.printReportGraphsOnlyButton.Text = "Print only graphs";
            this.printReportGraphsOnlyButton.TextImageRelation = Soko.Common.Controls.TextRelation.OnRight;
            this.printReportGraphsOnlyButton.TextImageSpacing = 5;
            this.printReportGraphsOnlyButton.Click += new System.EventHandler(this.printReportGraphsOnlyButton_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DividerWidth = 10;
            this.dataGridViewTextBoxColumn2.HeaderText = "Column2";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Column3";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // ReportViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.viewModeTabControl);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Name = "ReportViewer";
            this.Size = new System.Drawing.Size(863, 493);
            ((System.ComponentModel.ISupportInitialize)(this.viewModeTabControl)).EndInit();
            this.viewModeTabControl.ResumeLayout(false);
            this.graphicalViewTab.ResumeLayout(false);
            this.graphDockPanel.ResumeLayout(false);
            this.nicePanel4.ResumeLayout(false);
            this.fixedPanel.ResumeLayout(false);
            this.legendPanel.ResumeLayout(false);
            this.legendPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.realTimeTab.ResumeLayout(false);
            this.driveTableTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.driveTable)).EndInit();
            this.passFailTableTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.passFailTable)).EndInit();
            this.commentsTab.ResumeLayout(false);
            this.nicePanel2.ResumeLayout(false);
            this.nicePanel3.ResumeLayout(false);
            this.nicePanel3.PerformLayout();
            this.printMeTab.ResumeLayout(false);
            this.nicePanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.TabControlAdv viewModeTabControl; // tabControlAdv 
        private Syncfusion.Windows.Forms.Tools.TabPageAdv driveTableTab; // TabPageAdv
        private Syncfusion.Windows.Forms.Tools.TabPageAdv graphicalViewTab; // TabPageAdv
        private Syncfusion.Windows.Forms.Tools.TabPageAdv printMeTab; // TabPageAdv
        private Syncfusion.Windows.Forms.Tools.TabPageAdv commentsTab; // TabPageAdv
        private Syncfusion.Windows.Forms.Tools.TabPageAdv passFailTableTab; // TabPageAdv
        private Syncfusion.Windows.Forms.Tools.TabPageAdv realTimeTab; // TabPageAdv
        //private TD.SandDock.TabPage driveTableTab;
        //private TD.SandDock.TabPage graphicalViewTab;
        private ZedGraph.ZedGraphControl graphicalPressureZedGraph;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        //private TD.SandDock.TabPage printMeTab;
        private Soko.Common.Controls.NicePanel nicePanel1;
        private Soko.Common.Controls.NiceButton printReportGraphsOnlyButton;
        private Soko.Common.Controls.NicePanel legendPanel;
        private System.Windows.Forms.CheckBox checkBox1;
        private Soko.Common.Controls.NicePanel fixedPanel;
        private System.Windows.Forms.CheckBox showRefCb;
        private System.Windows.Forms.CheckBox allOnOffCb;
        private System.Windows.Forms.Button setRefButton;
       // private TD.SandDock.TabPage commentsTab;
        private Soko.Common.Controls.NicePanel nicePanel2;
        private Soko.Common.Controls.NicePanel nicePanel3;
        private System.Windows.Forms.Label operatorNameLabel;
        private System.Windows.Forms.Label commentLabel;
        private System.Windows.Forms.Label testNameLabel;
        private System.Windows.Forms.Label testTimestampLabel;
        private System.Windows.Forms.Label serialNoLabel;
        private Soko.Common.Controls.NiceTextBox commentTextBox;
        private Soko.Common.Controls.NiceTextBox testNameTextBox;
        private Soko.Common.Controls.NiceTextBox operatorTextBox;
        private Soko.Common.Controls.NiceTextBox timestampTextBox;
        private Soko.Common.Controls.NiceTextBox serialNoTextBox;
        private Soko.Common.Controls.NiceButton imageButton1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBox2;
        private Soko.Common.Controls.NicePanel nicePanel4;
        private System.Windows.Forms.Label pageSpanLabel;
        private System.Windows.Forms.Button pageSpanMinus;
        private System.Windows.Forms.Button pageSpanAdd;
        private System.Windows.Forms.Panel graphDockPanel;
        private Soko.Common.Controls.NiceButton saveCommentsButton;
       // private TD.SandDock.TabPage passFailTableTab;
        private XPTable.Models.Table driveTable;
        private XPTable.Models.Table passFailTable;
        private Soko.Common.Controls.NiceButton printReportWithTablesButton;
        //private TD.SandDock.TabPage realTimeTab;
        private LivePreviewPanel livePreviewPanel1;
    }
}
