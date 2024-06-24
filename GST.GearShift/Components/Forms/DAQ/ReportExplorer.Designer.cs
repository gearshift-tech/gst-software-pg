namespace GST.Gearshift.Components.Forms.DAQ
{
  partial class ReportExplorer
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
      XPTable.Models.DataSourceColumnBinder dataSourceColumnBinder1 = new XPTable.Models.DataSourceColumnBinder();
      XPTable.Renderers.DragDropRenderer dragDropRenderer1 = new XPTable.Renderers.DragDropRenderer();
      this.mainPanel = new Soko.Common.Controls.NicePanel();
      this.floatingPanel = new Soko.Common.Controls.NicePanel();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.reportsComboBox = new Soko.Common.Controls.NiceComboBox();
      this.searchParamsGroupBox = new System.Windows.Forms.GroupBox();
      this.bgw_pb = new System.Windows.Forms.ProgressBar();
      this.renameButton = new Soko.Common.Controls.NiceButton();
      this.deleteButton = new Soko.Common.Controls.NiceButton();
      this.refreshButton = new Soko.Common.Controls.NiceButton();
      this.rsltTable = new XPTable.Models.Table();
      this.rsltTableColumnModel = new XPTable.Models.ColumnModel();
      this.fileNameCol = new XPTable.Models.TextColumn();
      this.operatorCol = new XPTable.Models.TextColumn();
      this.serialCol = new XPTable.Models.TextColumn();
      this.nameCol = new XPTable.Models.TextColumn();
      this.dateCol = new XPTable.Models.TextColumn();
      this.rsltTableTableModel = new XPTable.Models.TableModel();
      this.dateComboBox = new System.Windows.Forms.ComboBox();
      this.testNameComboBox = new System.Windows.Forms.ComboBox();
      this.serialComboBox = new System.Windows.Forms.ComboBox();
      this.operatorComboBox = new System.Windows.Forms.ComboBox();
      this.dateRadioButton = new System.Windows.Forms.RadioButton();
      this.testNameRadioButton = new System.Windows.Forms.RadioButton();
      this.serialRadioButton = new System.Windows.Forms.RadioButton();
      this.operatorRadioButton = new System.Windows.Forms.RadioButton();
      this.bgw = new System.ComponentModel.BackgroundWorker();
      this.mainPanel.SuspendLayout();
      this.floatingPanel.SuspendLayout();
      this.searchParamsGroupBox.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.rsltTable)).BeginInit();
      this.SuspendLayout();
      // 
      // mainPanel
      // 
      this.mainPanel.AutoplaceElements = true;
      this.mainPanel.AutoScrollHorizontalMaximum = 100;
      this.mainPanel.AutoScrollHorizontalMinimum = 0;
      this.mainPanel.AutoScrollHPos = 0;
      this.mainPanel.AutoScrollVerticalMaximum = 100;
      this.mainPanel.AutoScrollVerticalMinimum = 0;
      this.mainPanel.AutoScrollVPos = 0;
      this.mainPanel.AutoSizeElements = false;
      this.mainPanel.BackColor = System.Drawing.Color.Transparent;
      this.mainPanel.backgroundColor1 = System.Drawing.Color.LightGray;
      this.mainPanel.backgroundColor2 = System.Drawing.Color.DarkGray;
      this.mainPanel.Controls.Add(this.floatingPanel);
      this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.mainPanel.DrawBackImage = false;
      this.mainPanel.EnableAutoScrollHorizontal = false;
      this.mainPanel.EnableAutoScrollVertical = false;
      this.mainPanel.Gradient = Soko.Common.Controls.NicePanel.GradientType.SigmaBellHorizontalUpper;
      this.mainPanel.HorizontalMargin = 0;
      this.mainPanel.Location = new System.Drawing.Point(0, 0);
      this.mainPanel.Name = "mainPanel";
      this.mainPanel.roundingRadius = 10;
      this.mainPanel.Size = new System.Drawing.Size(1048, 670);
      this.mainPanel.TabIndex = 1;
      this.mainPanel.VerticalMargin = 0;
      this.mainPanel.VisibleAutoScrollHorizontal = false;
      this.mainPanel.VisibleAutoScrollVertical = false;
      // 
      // floatingPanel
      // 
      this.floatingPanel.AutoplaceElements = false;
      this.floatingPanel.AutoScrollHorizontalMaximum = 100;
      this.floatingPanel.AutoScrollHorizontalMinimum = 0;
      this.floatingPanel.AutoScrollHPos = 0;
      this.floatingPanel.AutoScrollVerticalMaximum = 100;
      this.floatingPanel.AutoScrollVerticalMinimum = 0;
      this.floatingPanel.AutoScrollVPos = 0;
      this.floatingPanel.AutoSizeElements = false;
      this.floatingPanel.backgroundColor1 = System.Drawing.Color.LightGray;
      this.floatingPanel.backgroundColor2 = System.Drawing.Color.Silver;
      this.floatingPanel.Controls.Add(this.label2);
      this.floatingPanel.Controls.Add(this.label1);
      this.floatingPanel.Controls.Add(this.reportsComboBox);
      this.floatingPanel.Controls.Add(this.searchParamsGroupBox);
      this.floatingPanel.DrawBackImage = false;
      this.floatingPanel.EnableAutoScrollHorizontal = false;
      this.floatingPanel.EnableAutoScrollVertical = false;
      this.floatingPanel.Gradient = Soko.Common.Controls.NicePanel.GradientType.SigmaBellHorizontalUpper;
      this.floatingPanel.HorizontalMargin = 0;
      this.floatingPanel.Location = new System.Drawing.Point(13, 33);
      this.floatingPanel.Name = "floatingPanel";
      this.floatingPanel.roundingRadius = 10;
      this.floatingPanel.Size = new System.Drawing.Size(1022, 604);
      this.floatingPanel.TabIndex = 3;
      this.floatingPanel.VerticalMargin = 0;
      this.floatingPanel.VisibleAutoScrollHorizontal = false;
      this.floatingPanel.VisibleAutoScrollVertical = false;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.label2.ForeColor = System.Drawing.Color.Black;
      this.label2.Location = new System.Drawing.Point(12, 81);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(186, 20);
      this.label2.TabIndex = 5;
      this.label2.Text = "Or use the search facility:";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.label1.ForeColor = System.Drawing.Color.Black;
      this.label1.Location = new System.Drawing.Point(12, 21);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(193, 20);
      this.label1.TabIndex = 4;
      this.label1.Text = "Choose the report to view:";
      // 
      // reportsComboBox
      // 
      this.reportsComboBox.BackColor = System.Drawing.Color.Gainsboro;
      this.reportsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.reportsComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.reportsComboBox.FormattingEnabled = true;
      this.reportsComboBox.GotFocusBorderColor = System.Drawing.Color.Black;
      this.reportsComboBox.GotFocusBorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
      this.reportsComboBox.GotFocusDropDownButtonState = Soko.Common.Controls.NiceComboBox.EugenisButtonState.Original;
      this.reportsComboBox.Location = new System.Drawing.Point(16, 44);
      this.reportsComboBox.LostFocusBorderColor = System.Drawing.Color.Gray;
      this.reportsComboBox.LostFocusBorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
      this.reportsComboBox.LostFocusDropDownButtonState = Soko.Common.Controls.NiceComboBox.EugenisButtonState.Flat;
      this.reportsComboBox.Name = "reportsComboBox";
      this.reportsComboBox.Size = new System.Drawing.Size(992, 28);
      this.reportsComboBox.Sorted = true;
      this.reportsComboBox.TabIndex = 3;
      this.reportsComboBox.UseGotFocusStyle = false;
      this.reportsComboBox.UseLostFocusStyle = false;
      this.reportsComboBox.SelectedIndexChanged += new System.EventHandler(this.reportsComboBox_SelectedIndexChanged);
      // 
      // searchParamsGroupBox
      // 
      this.searchParamsGroupBox.BackColor = System.Drawing.Color.Transparent;
      this.searchParamsGroupBox.Controls.Add(this.bgw_pb);
      this.searchParamsGroupBox.Controls.Add(this.renameButton);
      this.searchParamsGroupBox.Controls.Add(this.deleteButton);
      this.searchParamsGroupBox.Controls.Add(this.refreshButton);
      this.searchParamsGroupBox.Controls.Add(this.rsltTable);
      this.searchParamsGroupBox.Controls.Add(this.dateComboBox);
      this.searchParamsGroupBox.Controls.Add(this.testNameComboBox);
      this.searchParamsGroupBox.Controls.Add(this.serialComboBox);
      this.searchParamsGroupBox.Controls.Add(this.operatorComboBox);
      this.searchParamsGroupBox.Controls.Add(this.dateRadioButton);
      this.searchParamsGroupBox.Controls.Add(this.testNameRadioButton);
      this.searchParamsGroupBox.Controls.Add(this.serialRadioButton);
      this.searchParamsGroupBox.Controls.Add(this.operatorRadioButton);
      this.searchParamsGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.searchParamsGroupBox.Location = new System.Drawing.Point(12, 103);
      this.searchParamsGroupBox.Name = "searchParamsGroupBox";
      this.searchParamsGroupBox.Size = new System.Drawing.Size(1003, 474);
      this.searchParamsGroupBox.TabIndex = 0;
      this.searchParamsGroupBox.TabStop = false;
      this.searchParamsGroupBox.Text = "Look for:";
      // 
      // bgw_pb
      // 
      this.bgw_pb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.bgw_pb.Location = new System.Drawing.Point(93, 445);
      this.bgw_pb.Name = "bgw_pb";
      this.bgw_pb.Size = new System.Drawing.Size(903, 23);
      this.bgw_pb.TabIndex = 6;
      // 
      // renameButton
      // 
      this.renameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.renameButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
      this.renameButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
      this.renameButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
      this.renameButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
      this.renameButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
      this.renameButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
      this.renameButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
      this.renameButton.BorderWidth = 1;
      this.renameButton.ContentPadding = new System.Windows.Forms.Padding(0);
      this.renameButton.DrawBackColorOnFocus = true;
      this.renameButton.DrawBackgroundImage = false;
      this.renameButton.DrawBorderOnFocus = true;
      this.renameButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.renameButton.Image = global::GST.Gearshift.Components.Properties.Resources.ReportViewerPanel_rename_32x32;
      this.renameButton.ImageDisabled = global::GST.Gearshift.Components.Properties.Resources.ReportViewerPanel_rename_32x32;
      this.renameButton.Location = new System.Drawing.Point(845, 415);
      this.renameButton.Name = "renameButton";
      this.renameButton.Size = new System.Drawing.Size(151, 32);
      this.renameButton.TabIndex = 7;
      this.renameButton.Text = "Rename selected";
      this.renameButton.TextImageRelation = Soko.Common.Controls.TextRelation.OnRight;
      this.renameButton.TextImageSpacing = 4;
      this.renameButton.Click += new System.EventHandler(this.renameButton_Click);
      // 
      // deleteButton
      // 
      this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.deleteButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
      this.deleteButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
      this.deleteButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
      this.deleteButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
      this.deleteButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
      this.deleteButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
      this.deleteButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
      this.deleteButton.BorderWidth = 1;
      this.deleteButton.ContentPadding = new System.Windows.Forms.Padding(0);
      this.deleteButton.DrawBackColorOnFocus = true;
      this.deleteButton.DrawBackgroundImage = false;
      this.deleteButton.DrawBorderOnFocus = true;
      this.deleteButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.deleteButton.Image = global::GST.Gearshift.Components.Properties.Resources.ReportViewerPanel_delete_32x32;
      this.deleteButton.ImageDisabled = global::GST.Gearshift.Components.Properties.Resources.ReportViewerPanel_delete_32x32;
      this.deleteButton.Location = new System.Drawing.Point(688, 415);
      this.deleteButton.Name = "deleteButton";
      this.deleteButton.Size = new System.Drawing.Size(151, 32);
      this.deleteButton.TabIndex = 7;
      this.deleteButton.Text = "Delete selection";
      this.deleteButton.TextImageRelation = Soko.Common.Controls.TextRelation.OnRight;
      this.deleteButton.TextImageSpacing = 4;
      this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
      // 
      // refreshButton
      // 
      this.refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.refreshButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
      this.refreshButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
      this.refreshButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
      this.refreshButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
      this.refreshButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
      this.refreshButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
      this.refreshButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
      this.refreshButton.BorderWidth = 1;
      this.refreshButton.ContentPadding = new System.Windows.Forms.Padding(0);
      this.refreshButton.DrawBackColorOnFocus = true;
      this.refreshButton.DrawBackgroundImage = false;
      this.refreshButton.DrawBorderOnFocus = true;
      this.refreshButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.refreshButton.Image = global::GST.Gearshift.Components.Properties.Resources.ReportViewerPanel_Refresh_32;
      this.refreshButton.ImageDisabled = global::GST.Gearshift.Components.Properties.Resources.ReportViewerPanel_Refresh_32;
      this.refreshButton.Location = new System.Drawing.Point(6, 444);
      this.refreshButton.Name = "refreshButton";
      this.refreshButton.Size = new System.Drawing.Size(91, 24);
      this.refreshButton.TabIndex = 7;
      this.refreshButton.Text = "Refresh";
      this.refreshButton.TextImageRelation = Soko.Common.Controls.TextRelation.OnRight;
      this.refreshButton.TextImageSpacing = 4;
      this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
      // 
      // rsltTable
      // 
      this.rsltTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.rsltTable.BorderColor = System.Drawing.Color.Black;
      this.rsltTable.ColumnModel = this.rsltTableColumnModel;
      this.rsltTable.DataMember = null;
      this.rsltTable.DataSourceColumnBinder = dataSourceColumnBinder1;
      dragDropRenderer1.ForeColor = System.Drawing.Color.Red;
      this.rsltTable.DragDropRenderer = dragDropRenderer1;
      this.rsltTable.EnableToolTips = true;
      this.rsltTable.FullRowSelect = true;
      this.rsltTable.GridLinesContrainedToData = false;
      this.rsltTable.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.rsltTable.Location = new System.Drawing.Point(6, 86);
      this.rsltTable.MultiSelect = true;
      this.rsltTable.Name = "rsltTable";
      this.rsltTable.NoItemsText = "There are no items to display. Please select one of the options.";
      this.rsltTable.Size = new System.Drawing.Size(990, 329);
      this.rsltTable.TabIndex = 8;
      this.rsltTable.TableModel = this.rsltTableTableModel;
      this.rsltTable.Text = "table1";
      this.rsltTable.UnfocusedBorderColor = System.Drawing.Color.Black;
      this.rsltTable.CellDoubleClick += new XPTable.Events.CellMouseEventHandler(this.rsltTable_CellDoubleClick);
      this.rsltTable.DoubleClick += new System.EventHandler(this.rsltTable_DoubleClick);
      // 
      // rsltTableColumnModel
      // 
      this.rsltTableColumnModel.Columns.AddRange(new XPTable.Models.Column[] {
            this.fileNameCol,
            this.operatorCol,
            this.serialCol,
            this.nameCol,
            this.dateCol});
      // 
      // fileNameCol
      // 
      this.fileNameCol.AutoResizeMode = XPTable.Models.ColumnAutoResizeMode.Grow;
      this.fileNameCol.Editable = false;
      this.fileNameCol.IsTextTrimmed = false;
      this.fileNameCol.Text = "File name";
      this.fileNameCol.Width = 250;
      // 
      // operatorCol
      // 
      this.operatorCol.Alignment = XPTable.Models.ColumnAlignment.Center;
      this.operatorCol.Editable = false;
      this.operatorCol.IsTextTrimmed = false;
      this.operatorCol.Text = "Operator";
      this.operatorCol.Width = 180;
      // 
      // serialCol
      // 
      this.serialCol.Alignment = XPTable.Models.ColumnAlignment.Center;
      this.serialCol.Editable = false;
      this.serialCol.IsTextTrimmed = false;
      this.serialCol.Text = "Gearbox serial number";
      this.serialCol.Width = 180;
      // 
      // nameCol
      // 
      this.nameCol.Alignment = XPTable.Models.ColumnAlignment.Center;
      this.nameCol.Editable = false;
      this.nameCol.IsTextTrimmed = false;
      this.nameCol.Text = "Test name";
      this.nameCol.Width = 180;
      // 
      // dateCol
      // 
      this.dateCol.Alignment = XPTable.Models.ColumnAlignment.Center;
      this.dateCol.Editable = false;
      this.dateCol.IsTextTrimmed = false;
      this.dateCol.Text = "Date";
      this.dateCol.Width = 180;
      // 
      // dateComboBox
      // 
      this.dateComboBox.BackColor = System.Drawing.Color.Gainsboro;
      this.dateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.dateComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.dateComboBox.ForeColor = System.Drawing.Color.Black;
      this.dateComboBox.FormattingEnabled = true;
      this.dateComboBox.Location = new System.Drawing.Point(753, 41);
      this.dateComboBox.Name = "dateComboBox";
      this.dateComboBox.Size = new System.Drawing.Size(243, 28);
      this.dateComboBox.Sorted = true;
      this.dateComboBox.TabIndex = 10;
      this.dateComboBox.DropDown += new System.EventHandler(this.dateComboBox_DropDown);
      this.dateComboBox.SelectedIndexChanged += new System.EventHandler(this.populateResultsTable);
      // 
      // testNameComboBox
      // 
      this.testNameComboBox.BackColor = System.Drawing.Color.Gainsboro;
      this.testNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.testNameComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.testNameComboBox.ForeColor = System.Drawing.Color.Black;
      this.testNameComboBox.FormattingEnabled = true;
      this.testNameComboBox.Location = new System.Drawing.Point(504, 41);
      this.testNameComboBox.Name = "testNameComboBox";
      this.testNameComboBox.Size = new System.Drawing.Size(243, 28);
      this.testNameComboBox.Sorted = true;
      this.testNameComboBox.TabIndex = 10;
      this.testNameComboBox.DropDown += new System.EventHandler(this.testNameComboBox_DropDown);
      this.testNameComboBox.SelectedIndexChanged += new System.EventHandler(this.populateResultsTable);
      // 
      // serialComboBox
      // 
      this.serialComboBox.BackColor = System.Drawing.Color.Gainsboro;
      this.serialComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.serialComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.serialComboBox.ForeColor = System.Drawing.Color.Black;
      this.serialComboBox.FormattingEnabled = true;
      this.serialComboBox.Location = new System.Drawing.Point(255, 41);
      this.serialComboBox.Name = "serialComboBox";
      this.serialComboBox.Size = new System.Drawing.Size(243, 28);
      this.serialComboBox.Sorted = true;
      this.serialComboBox.TabIndex = 9;
      this.serialComboBox.DropDown += new System.EventHandler(this.serialComboBox_DropDown);
      this.serialComboBox.SelectedIndexChanged += new System.EventHandler(this.populateResultsTable);
      // 
      // operatorComboBox
      // 
      this.operatorComboBox.BackColor = System.Drawing.Color.Gainsboro;
      this.operatorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.operatorComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.operatorComboBox.ForeColor = System.Drawing.Color.Black;
      this.operatorComboBox.FormattingEnabled = true;
      this.operatorComboBox.Location = new System.Drawing.Point(6, 41);
      this.operatorComboBox.Name = "operatorComboBox";
      this.operatorComboBox.Size = new System.Drawing.Size(243, 28);
      this.operatorComboBox.Sorted = true;
      this.operatorComboBox.TabIndex = 8;
      this.operatorComboBox.DropDown += new System.EventHandler(this.operatorComboBox_DropDown);
      this.operatorComboBox.SelectedIndexChanged += new System.EventHandler(this.populateResultsTable);
      // 
      // dateRadioButton
      // 
      this.dateRadioButton.AutoSize = true;
      this.dateRadioButton.Location = new System.Drawing.Point(842, 15);
      this.dateRadioButton.Name = "dateRadioButton";
      this.dateRadioButton.Size = new System.Drawing.Size(55, 20);
      this.dateRadioButton.TabIndex = 2;
      this.dateRadioButton.Text = "Date";
      this.dateRadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.dateRadioButton.UseVisualStyleBackColor = true;
      this.dateRadioButton.CheckedChanged += new System.EventHandler(this.dateRadioButton_CheckedChanged);
      // 
      // testNameRadioButton
      // 
      this.testNameRadioButton.AutoSize = true;
      this.testNameRadioButton.Location = new System.Drawing.Point(577, 15);
      this.testNameRadioButton.Name = "testNameRadioButton";
      this.testNameRadioButton.Size = new System.Drawing.Size(90, 20);
      this.testNameRadioButton.TabIndex = 2;
      this.testNameRadioButton.Text = "Test name";
      this.testNameRadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.testNameRadioButton.UseVisualStyleBackColor = true;
      this.testNameRadioButton.CheckedChanged += new System.EventHandler(this.testNameRadioButton_CheckedChanged);
      // 
      // serialRadioButton
      // 
      this.serialRadioButton.AutoSize = true;
      this.serialRadioButton.Location = new System.Drawing.Point(294, 15);
      this.serialRadioButton.Name = "serialRadioButton";
      this.serialRadioButton.Size = new System.Drawing.Size(162, 20);
      this.serialRadioButton.TabIndex = 1;
      this.serialRadioButton.Text = "Gearbox serial number";
      this.serialRadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.serialRadioButton.UseVisualStyleBackColor = true;
      this.serialRadioButton.CheckedChanged += new System.EventHandler(this.serialRadioButton_CheckedChanged);
      // 
      // operatorRadioButton
      // 
      this.operatorRadioButton.AutoSize = true;
      this.operatorRadioButton.Checked = true;
      this.operatorRadioButton.Location = new System.Drawing.Point(92, 15);
      this.operatorRadioButton.Name = "operatorRadioButton";
      this.operatorRadioButton.Size = new System.Drawing.Size(79, 20);
      this.operatorRadioButton.TabIndex = 0;
      this.operatorRadioButton.TabStop = true;
      this.operatorRadioButton.Text = "Operator";
      this.operatorRadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.operatorRadioButton.UseVisualStyleBackColor = true;
      this.operatorRadioButton.CheckedChanged += new System.EventHandler(this.operatorRadioButton_CheckedChanged);
      // 
      // bgw
      // 
      this.bgw.WorkerReportsProgress = true;
      this.bgw.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_DoWork);
      this.bgw.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgw_ProgressChanged);
      this.bgw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_RunWorkerCompleted);
      // 
      // ReportExplorer
      // 
      this.BackColor = System.Drawing.Color.Transparent;
      this.Controls.Add(this.mainPanel);
      this.Name = "ReportExplorer";
      this.Size = new System.Drawing.Size(1048, 670);
      this.mainPanel.ResumeLayout(false);
      this.floatingPanel.ResumeLayout(false);
      this.floatingPanel.PerformLayout();
      this.searchParamsGroupBox.ResumeLayout(false);
      this.searchParamsGroupBox.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.rsltTable)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private Soko.Common.Controls.NicePanel mainPanel;
    private Soko.Common.Controls.NicePanel floatingPanel;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private Soko.Common.Controls.NiceComboBox reportsComboBox;
    private System.Windows.Forms.GroupBox searchParamsGroupBox;
    private System.Windows.Forms.RadioButton testNameRadioButton;
    private System.Windows.Forms.RadioButton serialRadioButton;
    private System.Windows.Forms.RadioButton operatorRadioButton;
    private System.Windows.Forms.ComboBox testNameComboBox;
    private System.Windows.Forms.ComboBox serialComboBox;
    private System.Windows.Forms.ComboBox operatorComboBox;
    private XPTable.Models.Table rsltTable;
    private XPTable.Models.ColumnModel rsltTableColumnModel;
    private XPTable.Models.TextColumn operatorCol;
    private XPTable.Models.TextColumn serialCol;
    private XPTable.Models.TextColumn nameCol;
    private XPTable.Models.TextColumn dateCol;
    private XPTable.Models.TableModel rsltTableTableModel;
    private System.Windows.Forms.ComboBox dateComboBox;
    private System.Windows.Forms.RadioButton dateRadioButton;
    private XPTable.Models.TextColumn fileNameCol;
    private System.ComponentModel.BackgroundWorker bgw;
    private System.Windows.Forms.ProgressBar bgw_pb;
    private Soko.Common.Controls.NiceButton refreshButton;
    private Soko.Common.Controls.NiceButton renameButton;
    private Soko.Common.Controls.NiceButton deleteButton;

  }
}
