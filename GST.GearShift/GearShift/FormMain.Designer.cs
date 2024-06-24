
using Syncfusion.Windows.Forms.Tools;
using System.Drawing;

namespace GearShift
{
    partial class FormMain
    {
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            Syncfusion.Windows.Forms.Tools.CaptionButtonsCollection ccbpanel5 = new Syncfusion.Windows.Forms.Tools.CaptionButtonsCollection();
            Syncfusion.Windows.Forms.Tools.CaptionButtonsCollection ccbgaugesDocPanel = new Syncfusion.Windows.Forms.Tools.CaptionButtonsCollection();
            Syncfusion.Windows.Forms.Tools.CaptionButtonsCollection ccbconsoleWindow = new Syncfusion.Windows.Forms.Tools.CaptionButtonsCollection();
            Syncfusion.Windows.Forms.Tools.Office2016ColorTable office2016ColorTable1 = new Syncfusion.Windows.Forms.Tools.Office2016ColorTable();
            this.dockingManager1 = new Syncfusion.Windows.Forms.Tools.DockingManager(this.components);
            this.panel5 = new System.Windows.Forms.Panel();
            this.testControlPanel = new GST.Gearshift.Components.Forms.DAQ.TestControlPanel();
            this.gaugesDocPanel = new System.Windows.Forms.Panel();
            this.gaugesPanel = new Soko.Common.Controls.NicePanel();
            this.consoleWindow = new System.Windows.Forms.Panel();
            this.currentsDocPanel = new System.Windows.Forms.Panel();
            this.currBarsPanel = new Soko.Common.Controls.NicePanel();
            this.solenoidGauge1 = new GST.Gearshift.Components.Controls.Gauges.SolenoidGauge();
            this.livePreviewDocPanel = new System.Windows.Forms.Panel();
            this.livePreviewPanel3 = new GST.Gearshift.Components.Forms.DAQ.LivePreviewPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.gmLanTestEnvMenuItem = new System.Windows.Forms.Button();
            this.pluginsDesLabel = new System.Windows.Forms.Label();
            this.pluginTitleLabel = new System.Windows.Forms.Label();
            this.mainRibbon = new Syncfusion.Windows.Forms.Tools.RibbonControlAdv();
            this.backStageView1 = new Syncfusion.Windows.Forms.BackStageView(this.components);
            this.backStage1 = new Syncfusion.Windows.Forms.BackStage();
            this.infobackStageTab = new Syncfusion.Windows.Forms.BackStageTab();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gstLabel = new System.Windows.Forms.Label();
            this.reportsMenuItem = new Syncfusion.Windows.Forms.BackStageButton();
            this.viewBackStageTab = new Syncfusion.Windows.Forms.BackStageTab();
            this.panel3 = new System.Windows.Forms.Panel();
            this.showLivePreviewItem = new System.Windows.Forms.Button();
            this.showCurrentsMenuItem = new System.Windows.Forms.Button();
            this.showPressuresMenuItem = new System.Windows.Forms.Button();
            this.showConsoleMenuItem = new System.Windows.Forms.Button();
            this.viewDesLabel = new System.Windows.Forms.Label();
            this.viewTitleLabel = new System.Windows.Forms.Label();
            this.controlAOsMenuItem = new Syncfusion.Windows.Forms.BackStageButton();
            this.systemOptionsMenuItem = new Syncfusion.Windows.Forms.BackStageButton();
            this.firmwareMenuItem = new Syncfusion.Windows.Forms.BackStageButton();
            this.pluginsBackStageTab = new Syncfusion.Windows.Forms.BackStageTab();
            this.ChangelogMainMenuItem = new Syncfusion.Windows.Forms.BackStageButton();
            this.showControlPanelMainMenuItem = new Syncfusion.Windows.Forms.BackStageButton();
            this.closebackStageButton = new Syncfusion.Windows.Forms.BackStageButton();
            this.ExitBtnMenuItem = new Syncfusion.Windows.Forms.BackStageButton();
            this.toolStripTabItemHome = new Syncfusion.Windows.Forms.Tools.ToolStripTabItem();
            this.buttonConnect = new System.Windows.Forms.ToolStripButton();
            this.controlsUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.settingsPanel = new Soko.Common.Controls.NicePanel();
            this.settingsEditor1 = new GST.Gearshift.Components.Forms.SettingsEditor();
            this.scriptInstallerPanel = new Soko.Common.Controls.NicePanel();
            this.installScriptPackButton = new Soko.Common.Controls.NiceButton();
            this.tooltipsPanel = new Soko.Common.Controls.NicePanel();
            this.reportExplorer = new GST.Gearshift.Components.Forms.DAQ.ReportExplorer();
            this.initialTest = new GST.Gearshift.Components.Forms.DAQ.ChannelsInitialTest();
            this.canPanel1 = new GST.Gearshift.Components.Forms.CAN.CANPanel();
            this.statusBarExt1 = new Syncfusion.Windows.Forms.Tools.Controls.StatusBar.StatusBarExt();
            this.EZS_ConnectionStatusLabel = new Syncfusion.Windows.Forms.Tools.GradientLabel();
            this.Zf6_ConnectionStatusLabel = new Syncfusion.Windows.Forms.Tools.GradientLabel();
            this.CANcave_ConnectionStatusLabel = new Syncfusion.Windows.Forms.Tools.GradientLabel();
            this.showConsoleButton = new System.Windows.Forms.Button();
            this.consolePreviewLabel = new Syncfusion.Windows.Forms.Tools.GradientLabel();
            this.progBarTitleLabel = new Syncfusion.Windows.Forms.Tools.GradientLabel();
            this.oilFilterProgressBar = new Syncfusion.Windows.Forms.Tools.ProgressBarAdv();
            this.tabControlDocs = new Syncfusion.Windows.Forms.Tools.TabControlAdv();
            this.homeTab = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.reportExplorerDocument = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.scriptInstallerDocument = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.settingsDocument = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.CANTransControllerDocument = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.initialTestDocument = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.reportViewDocument = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.resultsViewer = new GST.Gearshift.Components.Forms.DAQ.ReportViewer();
            this.Gm6T40BarePanelDocument = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.pressureDisplaysDocument = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.currentDisplaysDocument = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.livePreviewDocument = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.superToolTip1 = new Syncfusion.Windows.Forms.Tools.SuperToolTip(this);
            ((System.ComponentModel.ISupportInitialize)(this.dockingManager1)).BeginInit();
            this.panel5.SuspendLayout();
            this.gaugesDocPanel.SuspendLayout();
            this.currentsDocPanel.SuspendLayout();
            this.currBarsPanel.SuspendLayout();
            this.livePreviewDocPanel.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainRibbon)).BeginInit();
            this.mainRibbon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.backStage1)).BeginInit();
            this.backStage1.SuspendLayout();
            this.infobackStageTab.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.viewBackStageTab.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pluginsBackStageTab.SuspendLayout();
            this.settingsPanel.SuspendLayout();
            this.scriptInstallerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarExt1)).BeginInit();
            this.statusBarExt1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.oilFilterProgressBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControlDocs)).BeginInit();
            this.tabControlDocs.SuspendLayout();
            this.reportExplorerDocument.SuspendLayout();
            this.scriptInstallerDocument.SuspendLayout();
            this.settingsDocument.SuspendLayout();
            this.CANTransControllerDocument.SuspendLayout();
            this.initialTestDocument.SuspendLayout();
            this.reportViewDocument.SuspendLayout();
            this.SuspendLayout();
            // 
            // dockingManager1
            // 
            this.dockingManager1.ActiveCaptionBackground = new Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.PatternStyle.Percent20, System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(114)))), ((int)(((byte)(16))))), System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(114)))), ((int)(((byte)(16))))));
            this.dockingManager1.AnimateAutoHiddenWindow = true;
            this.dockingManager1.AutoHideTabForeColor = System.Drawing.Color.Empty;
            this.dockingManager1.CloseTabOnMiddleClick = false;
            this.dockingManager1.DockBehavior = Syncfusion.Windows.Forms.Tools.DockBehavior.VS2010;
            this.dockingManager1.DockLayoutStream = ((System.IO.MemoryStream)(resources.GetObject("dockingManager1.DockLayoutStream")));
            this.dockingManager1.DockTabAlignment = Syncfusion.Windows.Forms.Tools.DockTabAlignmentStyle.Top;
            this.dockingManager1.DragProviderStyle = Syncfusion.Windows.Forms.Tools.DragProviderStyle.Office2016Colorful;
            this.dockingManager1.EnableDocumentMode = true;
            this.dockingManager1.HostControl = this;
            this.dockingManager1.MetroButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dockingManager1.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(158)))), ((int)(((byte)(218)))));
            this.dockingManager1.MetroSplitterBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(159)))), ((int)(((byte)(183)))));
            this.dockingManager1.PersistState = true;
            this.dockingManager1.ReduceFlickeringInRtl = false;
            this.dockingManager1.ThemeName = "Office2016Colorful";
            this.dockingManager1.VisualStyle = Syncfusion.Windows.Forms.VisualStyle.Office2016Colorful;
            this.dockingManager1.CaptionButtons.Add(new Syncfusion.Windows.Forms.Tools.CaptionButton(Syncfusion.Windows.Forms.Tools.CaptionButtonType.Close, "CloseButton"));
            this.dockingManager1.CaptionButtons.Add(new Syncfusion.Windows.Forms.Tools.CaptionButton(Syncfusion.Windows.Forms.Tools.CaptionButtonType.Pin, "PinButton"));
            this.dockingManager1.CaptionButtons.Add(new Syncfusion.Windows.Forms.Tools.CaptionButton(Syncfusion.Windows.Forms.Tools.CaptionButtonType.Maximize, "MaximizeButton"));
            this.dockingManager1.CaptionButtons.Add(new Syncfusion.Windows.Forms.Tools.CaptionButton(Syncfusion.Windows.Forms.Tools.CaptionButtonType.Restore, "RestoreButton"));
            this.dockingManager1.CaptionButtons.Add(new Syncfusion.Windows.Forms.Tools.CaptionButton(Syncfusion.Windows.Forms.Tools.CaptionButtonType.Menu, "MenuButton"));
            this.dockingManager1.SetDockLabel(this.panel5, "Control Panel");
            this.dockingManager1.SetEnableDocking(this.panel5, true);
            this.dockingManager1.SetDockAbility(this.panel5, "Right, Vertical");
            this.dockingManager1.SetOuterDockAbility(this.panel5, "Left, Right, Vertical");
            ccbpanel5.MergeWith(this.dockingManager1.CaptionButtons, false);
            this.dockingManager1.SetCustomCaptionButtons(this.panel5, ccbpanel5);
            this.dockingManager1.SetDockLabel(this.gaugesDocPanel, "Guages");
            this.dockingManager1.SetEnableDocking(this.gaugesDocPanel, true);
            ccbgaugesDocPanel.MergeWith(this.dockingManager1.CaptionButtons, false);
            this.dockingManager1.SetCustomCaptionButtons(this.gaugesDocPanel, ccbgaugesDocPanel);
            this.dockingManager1.SetDockLabel(this.consoleWindow, "Console Output");
            this.dockingManager1.SetDockIcon(this.consoleWindow, 1);
            this.dockingManager1.SetEnableDocking(this.consoleWindow, true);
            this.dockingManager1.SetDockAbility(this.consoleWindow, "Left, Right, Vertical");
            this.dockingManager1.SetOuterDockAbility(this.consoleWindow, "Left, Right, Vertical");
            ccbconsoleWindow.MergeWith(this.dockingManager1.CaptionButtons, false);
            this.dockingManager1.SetCustomCaptionButtons(this.consoleWindow, ccbconsoleWindow);
            // 
            // panel5
            // 
            this.panel5.AutoSize = true;
            this.panel5.Controls.Add(this.testControlPanel);
            this.panel5.Location = new System.Drawing.Point(1, 24);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(218, 956);
            this.panel5.TabIndex = 19;
            // 
            // testControlPanel
            // 
            this.testControlPanel.BackColor = System.Drawing.Color.Transparent;
            this.testControlPanel.Location = new System.Drawing.Point(4, 3);
            this.testControlPanel.MeasurementSession = null;
            this.testControlPanel.MinimumSize = new System.Drawing.Size(171, 549);
            this.testControlPanel.Name = "testControlPanel";
            this.testControlPanel.Size = new System.Drawing.Size(199, 950);
            this.testControlPanel.TabIndex = 0;
            // 
            // gaugesDocPanel
            // 
            this.gaugesDocPanel.AutoSize = true;
            this.gaugesDocPanel.Controls.Add(this.gaugesPanel);
            this.gaugesDocPanel.Location = new System.Drawing.Point(171, 24);
            this.gaugesDocPanel.Name = "Guages";
            this.gaugesDocPanel.Size = new System.Drawing.Size(1556, 950);
            this.gaugesDocPanel.TabIndex = 0;
            // 
            // gaugesPanel
            // 
            this.gaugesPanel.AutoplaceElements = true;
            this.gaugesPanel.AutoScrollHorizontalMaximum = 100;
            this.gaugesPanel.AutoScrollHorizontalMinimum = 0;
            this.gaugesPanel.AutoScrollHPos = 0;
            this.gaugesPanel.AutoScrollVerticalMaximum = 100;
            this.gaugesPanel.AutoScrollVerticalMinimum = 0;
            this.gaugesPanel.AutoScrollVPos = 0;
            this.gaugesPanel.AutoSizeElements = true;
            this.gaugesPanel.BackColor = System.Drawing.Color.Transparent;
            this.gaugesPanel.backgroundColor1 = System.Drawing.Color.Gainsboro;
            this.gaugesPanel.backgroundColor2 = System.Drawing.Color.DarkGray;
            this.gaugesPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
            this.gaugesPanel.BorderWidth = 1;
            this.gaugesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gaugesPanel.DrawBackImage = false;
            this.gaugesPanel.EnableAutoScrollHorizontal = false;
            this.gaugesPanel.EnableAutoScrollVertical = false;
            this.gaugesPanel.Gradient = Soko.Common.Controls.NicePanel.GradientType.SigmaBellHorizontalUpper;
            this.gaugesPanel.HorizontalMargin = 10;
            this.gaugesPanel.Location = new System.Drawing.Point(0, 0);
            this.gaugesPanel.Name = "gaugesPanel";
            this.gaugesPanel.roundingRadius = 10;
            this.gaugesPanel.Size = new System.Drawing.Size(1556, 950);
            this.gaugesPanel.SupportTransparentBackground = false;
            this.gaugesPanel.TabIndex = 48;
            this.gaugesPanel.VerticalMargin = 0;
            this.gaugesPanel.VisibleAutoScrollHorizontal = false;
            this.gaugesPanel.VisibleAutoScrollVertical = false;
            // 
            // consoleWindow
            // 
            this.consoleWindow.Location = new System.Drawing.Point(1, 24);
            this.consoleWindow.Name = "consoleWindow";
            this.consoleWindow.Size = new System.Drawing.Size(1330, 108);
            this.consoleWindow.TabIndex = 6;
            // 
            // currentsDocPanel
            // 
            this.currentsDocPanel.AutoSize = true;
            this.currentsDocPanel.Controls.Add(this.currBarsPanel);
            this.currentsDocPanel.Location = new System.Drawing.Point(1, 24);
            this.currentsDocPanel.Name = "Currents";
            this.currentsDocPanel.Size = new System.Drawing.Size(1556, 950);
            this.currentsDocPanel.TabIndex = 0;
            // 
            // currBarsPanel
            // 
            this.currBarsPanel.AutoplaceElements = true;
            this.currBarsPanel.AutoScrollHorizontalMaximum = 100;
            this.currBarsPanel.AutoScrollHorizontalMinimum = 0;
            this.currBarsPanel.AutoScrollHPos = 0;
            this.currBarsPanel.AutoScrollVerticalMaximum = 100;
            this.currBarsPanel.AutoScrollVerticalMinimum = 0;
            this.currBarsPanel.AutoScrollVPos = 0;
            this.currBarsPanel.AutoSizeElements = false;
            this.currBarsPanel.BackColor = System.Drawing.Color.Transparent;
            this.currBarsPanel.backgroundColor1 = System.Drawing.Color.Gainsboro;
            this.currBarsPanel.backgroundColor2 = System.Drawing.Color.DarkGray;
            this.currBarsPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
            this.currBarsPanel.BorderWidth = 1;
            this.currBarsPanel.Controls.Add(this.solenoidGauge1);
            this.currBarsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currBarsPanel.DrawBackImage = false;
            this.currBarsPanel.EnableAutoScrollHorizontal = false;
            this.currBarsPanel.EnableAutoScrollVertical = false;
            this.currBarsPanel.Gradient = Soko.Common.Controls.NicePanel.GradientType.SigmaBellHorizontalUpper;
            this.currBarsPanel.HorizontalMargin = 0;
            this.currBarsPanel.Location = new System.Drawing.Point(0, 0);
            this.currBarsPanel.Name = "currBarsPanel";
            this.currBarsPanel.roundingRadius = 10;
            this.currBarsPanel.Size = new System.Drawing.Size(1556, 950);
            this.currBarsPanel.SupportTransparentBackground = false;
            this.currBarsPanel.TabIndex = 1;
            this.currBarsPanel.VerticalMargin = 0;
            this.currBarsPanel.VisibleAutoScrollHorizontal = false;
            this.currBarsPanel.VisibleAutoScrollVertical = false;
            // 
            // solenoidGauge1
            // 
            this.solenoidGauge1.BackColor = System.Drawing.Color.Transparent;
            this.solenoidGauge1.Location = new System.Drawing.Point(708, 307);
            this.solenoidGauge1.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.solenoidGauge1.MinimumSize = new System.Drawing.Size(90, 150);
            this.solenoidGauge1.Name = "solenoidGauge1";
            this.solenoidGauge1.Size = new System.Drawing.Size(139, 336);
            this.solenoidGauge1.TabIndex = 0;
            // 
            // livePreviewDocPanel
            // 
            this.livePreviewDocPanel.AutoSize = true;
            this.livePreviewDocPanel.Controls.Add(this.livePreviewPanel3);
            this.livePreviewDocPanel.Location = new System.Drawing.Point(1, 24);
            this.livePreviewDocPanel.Name = "Live Preview";
            this.livePreviewDocPanel.Size = new System.Drawing.Size(1556, 950);
            this.livePreviewDocPanel.TabIndex = 0;
            // 
            // livePreviewPanel3
            // 
            this.livePreviewPanel3.DataPlaybackMode = GST.Gearshift.Components.Forms.DAQ.LivePreviewPanel.PlaybackMode.LivePlayback;
            this.livePreviewPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.livePreviewPanel3.Location = new System.Drawing.Point(0, 0);
            this.livePreviewPanel3.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.livePreviewPanel3.Name = "livePreviewPanel3";
            this.livePreviewPanel3.SampleWindowSize = 1000;
            this.livePreviewPanel3.Size = new System.Drawing.Size(1556, 950);
            this.livePreviewPanel3.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.gmLanTestEnvMenuItem);
            this.panel4.Controls.Add(this.pluginsDesLabel);
            this.panel4.Controls.Add(this.pluginTitleLabel);
            this.panel4.Location = new System.Drawing.Point(6, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(311, 321);
            this.panel4.TabIndex = 1;
            // 
            // gmLanTestEnvMenuItem
            // 
            this.gmLanTestEnvMenuItem.Image = global::GearShift.Properties.Resources.CAN_Communicator;
            this.gmLanTestEnvMenuItem.Location = new System.Drawing.Point(18, 73);
            this.gmLanTestEnvMenuItem.Name = "gmLanTestEnvMenuItem";
            this.gmLanTestEnvMenuItem.Size = new System.Drawing.Size(232, 52);
            this.gmLanTestEnvMenuItem.TabIndex = 2;
            this.gmLanTestEnvMenuItem.Text = "GMLAN 6T40 Test environment";
            this.gmLanTestEnvMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.gmLanTestEnvMenuItem.UseVisualStyleBackColor = true;
            this.gmLanTestEnvMenuItem.Click += new System.EventHandler(this.gmLanTestEnvMenuItem_Activate);
            // 
            // pluginsDesLabel
            // 
            this.pluginsDesLabel.AutoSize = true;
            this.pluginsDesLabel.Location = new System.Drawing.Point(15, 46);
            this.pluginsDesLabel.Name = "pluginsDesLabel";
            this.pluginsDesLabel.Size = new System.Drawing.Size(150, 13);
            this.pluginsDesLabel.TabIndex = 1;
            this.pluginsDesLabel.Text = "Gearshift System Extensions";
            // 
            // pluginTitleLabel
            // 
            this.pluginTitleLabel.AutoSize = true;
            this.pluginTitleLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pluginTitleLabel.Location = new System.Drawing.Point(13, 10);
            this.pluginTitleLabel.Name = "pluginTitleLabel";
            this.pluginTitleLabel.Size = new System.Drawing.Size(86, 30);
            this.pluginTitleLabel.TabIndex = 0;
            this.pluginTitleLabel.Text = "Plugins";
            // 
            // mainRibbon
            // 
            this.mainRibbon.BackStageView = this.backStageView1;
            this.mainRibbon.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.mainRibbon.Header.AddMainItem(toolStripTabItemHome);
            this.mainRibbon.Header.AddQuickItem(new Syncfusion.Windows.Forms.Tools.QuickButtonReflectable(buttonConnect));
            this.mainRibbon.LauncherStyle = Syncfusion.Windows.Forms.Tools.LauncherStyle.Office12;
            this.mainRibbon.Location = new System.Drawing.Point(1, 0);
            this.mainRibbon.Margin = new System.Windows.Forms.Padding(2);
            this.mainRibbon.MenuButtonAutoSize = true;
            this.mainRibbon.MenuButtonFont = new System.Drawing.Font("Segoe UI", 8.25F);
            this.mainRibbon.MenuButtonText = "Menu";
            this.mainRibbon.MenuButtonWidth = 56;
            this.mainRibbon.MenuColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(42)))));
            this.mainRibbon.Name = "mainRibbon";
            office2016ColorTable1.BackStageItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(143)))), ((int)(((byte)(66)))));
            office2016ColorTable1.BackStageItemSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(128)))), ((int)(((byte)(85)))));
            office2016ColorTable1.CheckedTabForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(34)))));
            office2016ColorTable1.DisabledControlBoxForeColor = System.Drawing.Color.White;
            office2016ColorTable1.GalleryItemSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(175)))), ((int)(((byte)(175)))));
            office2016ColorTable1.HeaderColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(114)))), ((int)(((byte)(16)))));
            office2016ColorTable1.HoverCollapsedDropDownButtonForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            office2016ColorTable1.QATDropDownForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            office2016ColorTable1.QuickDropDownPressedcolor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(181)))), ((int)(((byte)(83)))));
            office2016ColorTable1.QuickDropDownSelectedcolor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(223)))), ((int)(((byte)(184)))));
            office2016ColorTable1.SelectedTabColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(135)))), ((int)(((byte)(64)))));
            office2016ColorTable1.SystemButtonBackground = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(223)))), ((int)(((byte)(184)))));
            office2016ColorTable1.TabBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(114)))), ((int)(((byte)(16)))));
            office2016ColorTable1.TabGroupBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(77)))), ((int)(((byte)(51)))));
            office2016ColorTable1.ToolStripItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(87)))), ((int)(((byte)(87)))));
            this.mainRibbon.Office2016ColorTable.Add(office2016ColorTable1);
            this.mainRibbon.OfficeColorScheme = Syncfusion.Windows.Forms.Tools.ToolStripEx.ColorScheme.Managed;
            // 
            // mainRibbon.OfficeMenu
            // 
            this.mainRibbon.OfficeMenu.Name = "OfficeMenu";
            this.mainRibbon.OfficeMenu.ShowItemToolTips = true;
            this.mainRibbon.OfficeMenu.Size = new System.Drawing.Size(12, 65);
            this.mainRibbon.QuickPanelImageLayout = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mainRibbon.RibbonHeaderImage = Syncfusion.Windows.Forms.Tools.RibbonHeaderImage.None;
            this.mainRibbon.RibbonStyle = Syncfusion.Windows.Forms.Tools.RibbonStyle.Office2016;
            this.mainRibbon.RibbonTitleButtonSize = new System.Drawing.Size(100, 100);
            this.mainRibbon.SelectedTab = this.toolStripTabItemHome;
            this.mainRibbon.ShowQuickItemsDropDownButton = false;
            this.mainRibbon.ShowRibbonDisplayOptionButton = false;
            this.mainRibbon.Size = new System.Drawing.Size(1558, 85);
            this.mainRibbon.SystemText.QuickAccessDialogDropDownName = "Start menu";
            this.mainRibbon.SystemText.RenameDisplayLabelText = "&Display Name:";
            this.mainRibbon.TabIndex = 0;
            this.mainRibbon.Text = "ribbonControlAdv1";
            this.mainRibbon.ThemeName = "Office2016";
            this.mainRibbon.TitleColor = System.Drawing.Color.Black;
            // 
            // backStageView1
            // 
            this.backStageView1.BackStage = this.backStage1;
            this.backStageView1.HostControl = null;
            this.backStageView1.HostForm = this;
            // 
            // backStage1
            // 
            this.backStage1.AllowDrop = true;
            this.backStage1.BackStagePanelWidth = 130;
            this.backStage1.BeforeTouchSize = new System.Drawing.Size(1554, 832);
            this.backStage1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.backStage1.ChildItemSize = new System.Drawing.Size(80, 140);
            this.backStage1.Controls.Add(this.infobackStageTab);
            this.backStage1.Controls.Add(this.reportsMenuItem);
            this.backStage1.Controls.Add(this.viewBackStageTab);
            this.backStage1.Controls.Add(this.controlAOsMenuItem);
            this.backStage1.Controls.Add(this.systemOptionsMenuItem);
            this.backStage1.Controls.Add(this.firmwareMenuItem);
            this.backStage1.Controls.Add(this.pluginsBackStageTab);
            this.backStage1.Controls.Add(this.ChangelogMainMenuItem);
            this.backStage1.Controls.Add(this.showControlPanelMainMenuItem);
            this.backStage1.Controls.Add(this.closebackStageButton);
            this.backStage1.Controls.Add(this.ExitBtnMenuItem);
            this.backStage1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.backStage1.ItemSize = new System.Drawing.Size(152, 40);
            this.backStage1.Location = new System.Drawing.Point(0, 0);
            this.backStage1.Margin = new System.Windows.Forms.Padding(2);
            this.backStage1.MinimumSize = new System.Drawing.Size(100, 143);
            this.backStage1.Name = "backStage1";
            this.backStage1.OfficeColorScheme = Syncfusion.Windows.Forms.Tools.ToolStripEx.ColorScheme.Managed;
            this.backStage1.Size = new System.Drawing.Size(1554, 832);
            this.backStage1.TabIndex = 2;
            this.backStage1.ThemeName = "BackStageRenderer";
            this.backStage1.Visible = false;
            this.backStage1.SelectedIndexChanged += new System.EventHandler(this.backStage1_SelectedIndexChanged);
            // 
            // infobackStageTab
            // 
            this.infobackStageTab.Accelerator = "";
            this.infobackStageTab.BackColor = System.Drawing.Color.White;
            this.infobackStageTab.Controls.Add(this.panel1);
            this.infobackStageTab.Image = null;
            this.infobackStageTab.ImageSize = new System.Drawing.Size(16, 16);
            this.infobackStageTab.Location = new System.Drawing.Point(151, 0);
            this.infobackStageTab.Margin = new System.Windows.Forms.Padding(2);
            this.infobackStageTab.Name = "infobackStageTab";
            this.infobackStageTab.Placement = Syncfusion.Windows.Forms.BackStageItemPlacement.Top;
            this.infobackStageTab.Position = new System.Drawing.Point(11, 51);
            this.infobackStageTab.ShowCloseButton = true;
            this.infobackStageTab.Size = new System.Drawing.Size(1403, 832);
            this.infobackStageTab.TabIndex = 3;
            this.infobackStageTab.Text = "GST ";
            this.infobackStageTab.ThemesEnabled = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1403, 832);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.gstLabel);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(372, 349);
            this.panel2.TabIndex = 0;
            // 
            // gstLabel
            // 
            this.gstLabel.AutoSize = true;
            this.gstLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.gstLabel.Location = new System.Drawing.Point(42, 13);
            this.gstLabel.Name = "gstLabel";
            this.gstLabel.Size = new System.Drawing.Size(296, 30);
            this.gstLabel.TabIndex = 0;
            this.gstLabel.Text = "GearShift Technologies Ltd.";
            // 
            // reportsMenuItem
            // 
            this.reportsMenuItem.Accelerator = "";
            this.reportsMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.reportsMenuItem.BorderStyleAdv = Syncfusion.Windows.Forms.ButtonAdvBorderStyle.Etched;
            this.reportsMenuItem.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.reportsMenuItem.FlatAppearance.BorderSize = 5;
            this.reportsMenuItem.Location = new System.Drawing.Point(10, 51);
            this.reportsMenuItem.MinimumSize = new System.Drawing.Size(100, 0);
            this.reportsMenuItem.Name = "reportsMenuItem";
            this.reportsMenuItem.Placement = Syncfusion.Windows.Forms.BackStageItemPlacement.Top;
            this.reportsMenuItem.Size = new System.Drawing.Size(110, 25);
            this.reportsMenuItem.TabIndex = 10;
            this.reportsMenuItem.Text = "Reports";
            this.reportsMenuItem.Click += new System.EventHandler(this.reportsSearchButton_Activate);
            // 
            // viewBackStageTab
            // 
            this.viewBackStageTab.Accelerator = "";
            this.viewBackStageTab.BackColor = System.Drawing.Color.White;
            this.viewBackStageTab.Controls.Add(this.panel3);
            this.viewBackStageTab.Image = null;
            this.viewBackStageTab.ImageSize = new System.Drawing.Size(16, 16);
            this.viewBackStageTab.Location = new System.Drawing.Point(151, 0);
            this.viewBackStageTab.Name = "viewBackStageTab";
            this.viewBackStageTab.Placement = Syncfusion.Windows.Forms.BackStageItemPlacement.Top;
            this.viewBackStageTab.Position = new System.Drawing.Point(52, 92);
            this.viewBackStageTab.ShowCloseButton = true;
            this.viewBackStageTab.Size = new System.Drawing.Size(1403, 832);
            this.viewBackStageTab.TabIndex = 12;
            this.viewBackStageTab.Text = "View";
            this.viewBackStageTab.ThemesEnabled = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.showLivePreviewItem);
            this.panel3.Controls.Add(this.showCurrentsMenuItem);
            this.panel3.Controls.Add(this.showPressuresMenuItem);
            this.panel3.Controls.Add(this.showConsoleMenuItem);
            this.panel3.Controls.Add(this.viewDesLabel);
            this.panel3.Controls.Add(this.viewTitleLabel);
            this.panel3.Location = new System.Drawing.Point(6, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(311, 321);
            this.panel3.TabIndex = 0;
            // 
            // showLivePreviewItem
            // 
            this.showLivePreviewItem.Image = global::GearShift.Properties.Resources.menuItem_LivePreview_32x32;
            this.showLivePreviewItem.Location = new System.Drawing.Point(18, 247);
            this.showLivePreviewItem.Name = "showLivePreviewItem";
            this.showLivePreviewItem.Size = new System.Drawing.Size(165, 52);
            this.showLivePreviewItem.TabIndex = 5;
            this.showLivePreviewItem.Text = "Show Live Preview Tab";
            this.showLivePreviewItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.showLivePreviewItem.UseVisualStyleBackColor = true;
            this.showLivePreviewItem.Click += new System.EventHandler(this.showLivePreviewItem_Activate);
            // 
            // showCurrentsMenuItem
            // 
            this.showCurrentsMenuItem.Image = global::GearShift.Properties.Resources.currents_32x32;
            this.showCurrentsMenuItem.Location = new System.Drawing.Point(18, 189);
            this.showCurrentsMenuItem.Name = "showCurrentsMenuItem";
            this.showCurrentsMenuItem.Size = new System.Drawing.Size(165, 52);
            this.showCurrentsMenuItem.TabIndex = 4;
            this.showCurrentsMenuItem.Text = "Show Currents Tab";
            this.showCurrentsMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.showCurrentsMenuItem.UseVisualStyleBackColor = true;
            this.showCurrentsMenuItem.Click += new System.EventHandler(this.showCurrentsMenuItem_Activate);
            // 
            // showPressuresMenuItem
            // 
            this.showPressuresMenuItem.Image = global::GearShift.Properties.Resources.Pressures_32x32;
            this.showPressuresMenuItem.Location = new System.Drawing.Point(18, 131);
            this.showPressuresMenuItem.Name = "showPressuresMenuItem";
            this.showPressuresMenuItem.Size = new System.Drawing.Size(165, 52);
            this.showPressuresMenuItem.TabIndex = 3;
            this.showPressuresMenuItem.Text = "Show Pressure Tab";
            this.showPressuresMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.showPressuresMenuItem.UseVisualStyleBackColor = true;
            this.showPressuresMenuItem.Click += new System.EventHandler(this.showPressuresMenuItem_Activate);
            // 
            // showConsoleMenuItem
            // 
            this.showConsoleMenuItem.Image = global::GearShift.Properties.Resources.Document_Console_32;
            this.showConsoleMenuItem.Location = new System.Drawing.Point(18, 73);
            this.showConsoleMenuItem.Name = "showConsoleMenuItem";
            this.showConsoleMenuItem.Size = new System.Drawing.Size(165, 52);
            this.showConsoleMenuItem.TabIndex = 2;
            this.showConsoleMenuItem.Text = "Show Console";
            this.showConsoleMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.showConsoleMenuItem.UseVisualStyleBackColor = true;
            this.showConsoleMenuItem.Click += new System.EventHandler(this.showConsoleMenuItem_Activate);
            // 
            // viewDesLabel
            // 
            this.viewDesLabel.AutoSize = true;
            this.viewDesLabel.Location = new System.Drawing.Point(15, 43);
            this.viewDesLabel.Name = "viewDesLabel";
            this.viewDesLabel.Size = new System.Drawing.Size(244, 13);
            this.viewDesLabel.TabIndex = 1;
            this.viewDesLabel.Text = "Show Console Log, Pressure Tab, Currents Tab";
            // 
            // viewTitleLabel
            // 
            this.viewTitleLabel.AutoSize = true;
            this.viewTitleLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewTitleLabel.Location = new System.Drawing.Point(13, 10);
            this.viewTitleLabel.Name = "viewTitleLabel";
            this.viewTitleLabel.Size = new System.Drawing.Size(61, 30);
            this.viewTitleLabel.TabIndex = 0;
            this.viewTitleLabel.Text = "View";
            // 
            // controlAOsMenuItem
            // 
            this.controlAOsMenuItem.Accelerator = "";
            this.controlAOsMenuItem.AutoSize = true;
            this.controlAOsMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.controlAOsMenuItem.Location = new System.Drawing.Point(10, 117);
            this.controlAOsMenuItem.Name = "controlAOsMenuItem";
            this.controlAOsMenuItem.Placement = Syncfusion.Windows.Forms.BackStageItemPlacement.Top;
            this.controlAOsMenuItem.Size = new System.Drawing.Size(142, 25);
            this.controlAOsMenuItem.TabIndex = 13;
            this.controlAOsMenuItem.Text = "Control Analog Outputs";
            this.controlAOsMenuItem.Click += new System.EventHandler(this.controlAOsMenuItem_Activate);
            // 
            // systemOptionsMenuItem
            // 
            this.systemOptionsMenuItem.Accelerator = "";
            this.systemOptionsMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.systemOptionsMenuItem.Location = new System.Drawing.Point(10, 142);
            this.systemOptionsMenuItem.Name = "systemOptionsMenuItem";
            this.systemOptionsMenuItem.Placement = Syncfusion.Windows.Forms.BackStageItemPlacement.Top;
            this.systemOptionsMenuItem.Size = new System.Drawing.Size(110, 25);
            this.systemOptionsMenuItem.TabIndex = 14;
            this.systemOptionsMenuItem.Text = "System Options";
            this.systemOptionsMenuItem.Click += new System.EventHandler(this.systemOptionsMenuItem_Activate);
            // 
            // firmwareMenuItem
            // 
            this.firmwareMenuItem.Accelerator = "";
            this.firmwareMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.firmwareMenuItem.Location = new System.Drawing.Point(10, 167);
            this.firmwareMenuItem.Name = "firmwareMenuItem";
            this.firmwareMenuItem.Placement = Syncfusion.Windows.Forms.BackStageItemPlacement.Top;
            this.firmwareMenuItem.Size = new System.Drawing.Size(110, 25);
            this.firmwareMenuItem.TabIndex = 15;
            this.firmwareMenuItem.Text = "Update Firmware";
            this.firmwareMenuItem.Click += new System.EventHandler(this.firmwareMenuItem_Activate);
            // 
            // pluginsBackStageTab
            // 
            this.pluginsBackStageTab.Accelerator = "";
            this.pluginsBackStageTab.BackColor = System.Drawing.Color.White;
            this.pluginsBackStageTab.Controls.Add(this.panel4);
            this.pluginsBackStageTab.Image = null;
            this.pluginsBackStageTab.ImageSize = new System.Drawing.Size(16, 16);
            this.pluginsBackStageTab.Location = new System.Drawing.Point(151, 0);
            this.pluginsBackStageTab.Name = "pluginsBackStageTab";
            this.pluginsBackStageTab.Placement = Syncfusion.Windows.Forms.BackStageItemPlacement.Top;
            this.pluginsBackStageTab.Position = new System.Drawing.Point(93, 133);
            this.pluginsBackStageTab.ShowCloseButton = true;
            this.pluginsBackStageTab.Size = new System.Drawing.Size(1403, 832);
            this.pluginsBackStageTab.TabIndex = 16;
            this.pluginsBackStageTab.Text = "Plugins";
            this.pluginsBackStageTab.ThemesEnabled = false;
            // 
            // ChangelogMainMenuItem
            // 
            this.ChangelogMainMenuItem.Accelerator = "";
            this.ChangelogMainMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.ChangelogMainMenuItem.Location = new System.Drawing.Point(10, 233);
            this.ChangelogMainMenuItem.Name = "ChangelogMainMenuItem";
            this.ChangelogMainMenuItem.Placement = Syncfusion.Windows.Forms.BackStageItemPlacement.Top;
            this.ChangelogMainMenuItem.Size = new System.Drawing.Size(110, 25);
            this.ChangelogMainMenuItem.TabIndex = 17;
            this.ChangelogMainMenuItem.Text = "Changelog";
            this.ChangelogMainMenuItem.Click += new System.EventHandler(this.ChangelogMainMenuItem_Activate);
            // 
            // showControlPanelMainMenuItem
            // 
            this.showControlPanelMainMenuItem.Accelerator = "";
            this.showControlPanelMainMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.showControlPanelMainMenuItem.Location = new System.Drawing.Point(10, 258);
            this.showControlPanelMainMenuItem.Name = "showControlPanelMainMenuItem";
            this.showControlPanelMainMenuItem.Placement = Syncfusion.Windows.Forms.BackStageItemPlacement.Top;
            this.showControlPanelMainMenuItem.Size = new System.Drawing.Size(110, 25);
            this.showControlPanelMainMenuItem.TabIndex = 18;
            this.showControlPanelMainMenuItem.Text = "Restore Control Panel";
            this.showControlPanelMainMenuItem.Click += new System.EventHandler(this.showControlPanelMainMenuItem_Activate);
            // 
            // closebackStageButton
            // 
            this.closebackStageButton.Accelerator = "";
            this.closebackStageButton.BackColor = System.Drawing.Color.Transparent;
            this.closebackStageButton.Location = new System.Drawing.Point(10, 283);
            this.closebackStageButton.Name = "closebackStageButton";
            this.closebackStageButton.Placement = Syncfusion.Windows.Forms.BackStageItemPlacement.Top;
            this.closebackStageButton.Size = new System.Drawing.Size(110, 25);
            this.closebackStageButton.TabIndex = 9;
            this.closebackStageButton.Text = "Close";
            this.closebackStageButton.Click += new System.EventHandler(this.closebackStageButton_Click);
            // 
            // ExitBtnMenuItem
            // 
            this.ExitBtnMenuItem.Accelerator = "";
            this.ExitBtnMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.ExitBtnMenuItem.Location = new System.Drawing.Point(10, 308);
            this.ExitBtnMenuItem.Name = "ExitBtnMenuItem";
            this.ExitBtnMenuItem.Placement = Syncfusion.Windows.Forms.BackStageItemPlacement.Top;
            this.ExitBtnMenuItem.Size = new System.Drawing.Size(110, 25);
            this.ExitBtnMenuItem.TabIndex = 11;
            this.ExitBtnMenuItem.Text = "Exit";
            this.ExitBtnMenuItem.Click += new System.EventHandler(this.ExitBtnMenuItem_Click);
            // 
            // toolStripTabItemHome
            // 
            this.toolStripTabItemHome.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.toolStripTabItemHome.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolStripTabItemHome.Name = "toolStripTabItemHome";
            // 
            // mainRibbon.ribbonPanel1
            // 
            this.toolStripTabItemHome.Panel.BackColor = System.Drawing.Color.Red;
            this.toolStripTabItemHome.Panel.Name = "ribbonPanel1";
            this.toolStripTabItemHome.Panel.ScrollPosition = 0;
            this.toolStripTabItemHome.Panel.TabIndex = 2;
            this.toolStripTabItemHome.Panel.Text = "Home";
            this.toolStripTabItemHome.Position = 0;
            this.toolStripTabItemHome.Size = new System.Drawing.Size(53, 30);
            this.toolStripTabItemHome.Tag = "1";
            this.toolStripTabItemHome.Text = "Home";
            this.toolStripTabItemHome.ToolTipText = "Home Tab";
            // 
            // buttonConnect
            // 
            this.buttonConnect.Image = global::GearShift.Properties.Resources.UsbLogo_Red_40x20;
            this.buttonConnect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Padding = new System.Windows.Forms.Padding(10, 2, 0, 0);
            this.buttonConnect.Size = new System.Drawing.Size(75, 23);
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Activate);
            // 
            // controlsUpdateTimer
            // 
            this.controlsUpdateTimer.Interval = 20;
            this.controlsUpdateTimer.Tick += new System.EventHandler(this.controlsUpdateTimer_Tick);
            // 
            // settingsPanel
            // 
            this.settingsPanel.AutoplaceElements = true;
            this.settingsPanel.AutoScrollHorizontalMaximum = 100;
            this.settingsPanel.AutoScrollHorizontalMinimum = 0;
            this.settingsPanel.AutoScrollHPos = 0;
            this.settingsPanel.AutoScrollVerticalMaximum = 100;
            this.settingsPanel.AutoScrollVerticalMinimum = 0;
            this.settingsPanel.AutoScrollVPos = 0;
            this.settingsPanel.AutoSizeElements = false;
            this.settingsPanel.BackColor = System.Drawing.Color.Transparent;
            this.settingsPanel.backgroundColor1 = System.Drawing.Color.LightGray;
            this.settingsPanel.backgroundColor2 = System.Drawing.Color.DarkGray;
            this.settingsPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
            this.settingsPanel.BorderWidth = 1;
            this.settingsPanel.Controls.Add(this.settingsEditor1);
            this.settingsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsPanel.DrawBackImage = false;
            this.settingsPanel.EnableAutoScrollHorizontal = false;
            this.settingsPanel.EnableAutoScrollVertical = false;
            this.settingsPanel.Gradient = Soko.Common.Controls.NicePanel.GradientType.SigmaBellHorizontalUpper;
            this.settingsPanel.HorizontalMargin = 0;
            this.settingsPanel.Location = new System.Drawing.Point(0, 0);
            this.settingsPanel.Name = "settingsPanel";
            this.settingsPanel.roundingRadius = 10;
            this.settingsPanel.Size = new System.Drawing.Size(1336, 655);
            this.settingsPanel.SupportTransparentBackground = false;
            this.settingsPanel.TabIndex = 2;
            this.settingsPanel.VerticalMargin = 0;
            this.settingsPanel.VisibleAutoScrollHorizontal = false;
            this.settingsPanel.VisibleAutoScrollVertical = false;
            // 
            // settingsEditor1
            // 
            this.settingsEditor1.BackColor = System.Drawing.Color.LightGray;
            this.settingsEditor1.ForeColor = System.Drawing.Color.Black;
            this.settingsEditor1.Location = new System.Drawing.Point(351, 97);
            this.settingsEditor1.Name = "settingsEditor1";
            this.settingsEditor1.Size = new System.Drawing.Size(633, 461);
            this.settingsEditor1.TabIndex = 0;
            // 
            // scriptInstallerPanel
            // 
            this.scriptInstallerPanel.AutoplaceElements = true;
            this.scriptInstallerPanel.AutoScrollHorizontalMaximum = 100;
            this.scriptInstallerPanel.AutoScrollHorizontalMinimum = 0;
            this.scriptInstallerPanel.AutoScrollHPos = 0;
            this.scriptInstallerPanel.AutoScrollVerticalMaximum = 100;
            this.scriptInstallerPanel.AutoScrollVerticalMinimum = 0;
            this.scriptInstallerPanel.AutoScrollVPos = 0;
            this.scriptInstallerPanel.AutoSizeElements = false;
            this.scriptInstallerPanel.BackColor = System.Drawing.Color.Transparent;
            this.scriptInstallerPanel.backgroundColor1 = System.Drawing.Color.LightGray;
            this.scriptInstallerPanel.backgroundColor2 = System.Drawing.Color.DarkGray;
            this.scriptInstallerPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
            this.scriptInstallerPanel.BorderWidth = 1;
            this.scriptInstallerPanel.Controls.Add(this.installScriptPackButton);
            this.scriptInstallerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scriptInstallerPanel.DrawBackImage = false;
            this.scriptInstallerPanel.EnableAutoScrollHorizontal = false;
            this.scriptInstallerPanel.EnableAutoScrollVertical = false;
            this.scriptInstallerPanel.Gradient = Soko.Common.Controls.NicePanel.GradientType.SigmaBellHorizontalUpper;
            this.scriptInstallerPanel.HorizontalMargin = 0;
            this.scriptInstallerPanel.Location = new System.Drawing.Point(0, 0);
            this.scriptInstallerPanel.Name = "scriptInstallerPanel";
            this.scriptInstallerPanel.roundingRadius = 10;
            this.scriptInstallerPanel.Size = new System.Drawing.Size(1336, 655);
            this.scriptInstallerPanel.SupportTransparentBackground = false;
            this.scriptInstallerPanel.TabIndex = 1;
            this.scriptInstallerPanel.VerticalMargin = 0;
            this.scriptInstallerPanel.VisibleAutoScrollHorizontal = false;
            this.scriptInstallerPanel.VisibleAutoScrollVertical = false;
            // 
            // installScriptPackButton
            // 
            this.installScriptPackButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.installScriptPackButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.installScriptPackButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.installScriptPackButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.installScriptPackButton.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.installScriptPackButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.installScriptPackButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
            this.installScriptPackButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.installScriptPackButton.BorderWidth = 1;
            this.installScriptPackButton.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.installScriptPackButton.ContentPadding = new System.Windows.Forms.Padding(0);
            this.installScriptPackButton.DrawBackColorOnFocus = true;
            this.installScriptPackButton.DrawBackgroundImage = false;
            this.installScriptPackButton.DrawBorderOnFocus = true;
            this.installScriptPackButton.DrawBorderOnTop = false;
            this.installScriptPackButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.installScriptPackButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.installScriptPackButton.Image = ((System.Drawing.Image)(resources.GetObject("installScriptPackButton.Image")));
            this.installScriptPackButton.ImageDisabled = ((System.Drawing.Image)(resources.GetObject("installScriptPackButton.ImageDisabled")));
            this.installScriptPackButton.Location = new System.Drawing.Point(487, 259);
            this.installScriptPackButton.Name = "installScriptPackButton";
            this.installScriptPackButton.Size = new System.Drawing.Size(362, 136);
            this.installScriptPackButton.SupportTransparentBackground = false;
            this.installScriptPackButton.TabIndex = 0;
            this.installScriptPackButton.Text = "Browse for script pack";
            this.installScriptPackButton.TextImageRelation = Soko.Common.Controls.TextRelation.OnRight;
            this.installScriptPackButton.TextImageSpacing = 0;
            this.installScriptPackButton.Click += new System.EventHandler(this.installScriptPackButton_Click);
            // 
            // tooltipsPanel
            // 
            this.tooltipsPanel.AutoplaceElements = false;
            this.tooltipsPanel.AutoScrollHorizontalMaximum = 100;
            this.tooltipsPanel.AutoScrollHorizontalMinimum = 0;
            this.tooltipsPanel.AutoScrollHPos = 0;
            this.tooltipsPanel.AutoScrollVerticalMaximum = 100;
            this.tooltipsPanel.AutoScrollVerticalMinimum = 0;
            this.tooltipsPanel.AutoScrollVPos = 0;
            this.tooltipsPanel.AutoSizeElements = false;
            this.tooltipsPanel.backgroundColor1 = System.Drawing.Color.WhiteSmoke;
            this.tooltipsPanel.backgroundColor2 = System.Drawing.Color.Wheat;
            this.tooltipsPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
            this.tooltipsPanel.BorderWidth = 1;
            this.tooltipsPanel.DrawBackImage = false;
            this.tooltipsPanel.EnableAutoScrollHorizontal = false;
            this.tooltipsPanel.EnableAutoScrollVertical = false;
            this.tooltipsPanel.Gradient = Soko.Common.Controls.NicePanel.GradientType.None;
            this.tooltipsPanel.HorizontalMargin = 0;
            this.tooltipsPanel.Location = new System.Drawing.Point(186, 49);
            this.tooltipsPanel.Name = "tooltipsPanel";
            this.tooltipsPanel.roundingRadius = 10;
            this.tooltipsPanel.Size = new System.Drawing.Size(352, 275);
            this.tooltipsPanel.SupportTransparentBackground = false;
            this.tooltipsPanel.TabIndex = 0;
            this.tooltipsPanel.VerticalMargin = 0;
            this.tooltipsPanel.VisibleAutoScrollHorizontal = false;
            this.tooltipsPanel.VisibleAutoScrollVertical = false;
            // 
            // reportExplorer
            // 
            this.reportExplorer.BackColor = System.Drawing.Color.Transparent;
            this.reportExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportExplorer.Location = new System.Drawing.Point(0, 0);
            this.reportExplorer.Name = "reportExplorer";
            this.reportExplorer.Size = new System.Drawing.Size(1336, 655);
            this.reportExplorer.TabIndex = 0;
            this.reportExplorer.TestReportChosen += new GST.Gearshift.Components.Forms.DAQ.ReportExplorer.TestReportChosenDelegate(this.reportExplorer_TestScriptChosen);
            // 
            // initialTest
            // 
            this.initialTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.initialTest.Location = new System.Drawing.Point(0, 0);
            this.initialTest.MaximumSize = new System.Drawing.Size(2000, 1500);
            this.initialTest.MinimumSize = new System.Drawing.Size(561, 450);
            this.initialTest.Name = "initialTest";
            this.initialTest.Size = new System.Drawing.Size(1336, 655);
            this.initialTest.TabIndex = 0;
            // 
            // canPanel1
            // 
            this.canPanel1.BackColor = System.Drawing.Color.Gray;
            this.canPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canPanel1.Location = new System.Drawing.Point(0, 0);
            this.canPanel1.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.canPanel1.Name = "canPanel1";
            this.canPanel1.Size = new System.Drawing.Size(1336, 655);
            this.canPanel1.TabIndex = 0;
            // 
            // statusBarExt1
            // 
            this.statusBarExt1.BeforeTouchSize = new System.Drawing.Size(1556, 26);
            this.statusBarExt1.BorderColor = System.Drawing.SystemColors.Control;
            this.statusBarExt1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statusBarExt1.Controls.Add(this.EZS_ConnectionStatusLabel);
            this.statusBarExt1.Controls.Add(this.Zf6_ConnectionStatusLabel);
            this.statusBarExt1.Controls.Add(this.CANcave_ConnectionStatusLabel);
            this.statusBarExt1.Controls.Add(this.showConsoleButton);
            this.statusBarExt1.Controls.Add(this.consolePreviewLabel);
            this.statusBarExt1.Controls.Add(this.progBarTitleLabel);
            this.statusBarExt1.Controls.Add(this.oilFilterProgressBar);
            this.statusBarExt1.CustomLayoutBounds = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.statusBarExt1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusBarExt1.Location = new System.Drawing.Point(0, 859);
            this.statusBarExt1.Name = "statusBarExt1";
            this.statusBarExt1.Padding = new System.Windows.Forms.Padding(3);
            this.statusBarExt1.Size = new System.Drawing.Size(1556, 26);
            this.statusBarExt1.Spacing = new System.Drawing.Size(2, 2);
            this.statusBarExt1.TabIndex = 3;
            this.statusBarExt1.ThemesEnabled = true;
            this.statusBarExt1.VisualStyle = Syncfusion.Windows.Forms.Tools.Controls.StatusBar.VisualStyle.Default;
            // 
            // EZS_ConnectionStatusLabel
            // 
            this.EZS_ConnectionStatusLabel.BackgroundColor = new Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.Vertical, System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(240)))), ((int)(((byte)(247))))), System.Drawing.Color.LightCyan);
            this.EZS_ConnectionStatusLabel.BeforeTouchSize = new System.Drawing.Size(159, 20);
            this.EZS_ConnectionStatusLabel.BorderSides = ((System.Windows.Forms.Border3DSide)((((System.Windows.Forms.Border3DSide.Left | System.Windows.Forms.Border3DSide.Top) 
            | System.Windows.Forms.Border3DSide.Right) 
            | System.Windows.Forms.Border3DSide.Bottom)));
            this.EZS_ConnectionStatusLabel.Image = global::GearShift.Properties.Resources.DeviceStatuDisconnected_16x16;
            this.EZS_ConnectionStatusLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.EZS_ConnectionStatusLabel.Location = new System.Drawing.Point(0, 2);
            this.EZS_ConnectionStatusLabel.Name = "EZS_ConnectionStatusLabel";
            this.EZS_ConnectionStatusLabel.Size = new System.Drawing.Size(159, 20);
            this.EZS_ConnectionStatusLabel.TabIndex = 3;
            this.EZS_ConnectionStatusLabel.Text = "      Gearshift Disconnected";
            this.EZS_ConnectionStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Zf6_ConnectionStatusLabel
            // 
            this.Zf6_ConnectionStatusLabel.BackgroundColor = new Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.Vertical, System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(240)))), ((int)(((byte)(247))))), System.Drawing.Color.LightCyan);
            this.Zf6_ConnectionStatusLabel.BeforeTouchSize = new System.Drawing.Size(159, 20);
            this.Zf6_ConnectionStatusLabel.BorderSides = ((System.Windows.Forms.Border3DSide)((((System.Windows.Forms.Border3DSide.Left | System.Windows.Forms.Border3DSide.Top) 
            | System.Windows.Forms.Border3DSide.Right) 
            | System.Windows.Forms.Border3DSide.Bottom)));
            this.Zf6_ConnectionStatusLabel.Image = global::GearShift.Properties.Resources.DeviceStatuDisconnected_16x16;
            this.Zf6_ConnectionStatusLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Zf6_ConnectionStatusLabel.Location = new System.Drawing.Point(161, 2);
            this.Zf6_ConnectionStatusLabel.Name = "Zf6_ConnectionStatusLabel";
            this.Zf6_ConnectionStatusLabel.Size = new System.Drawing.Size(159, 20);
            this.Zf6_ConnectionStatusLabel.TabIndex = 2;
            this.Zf6_ConnectionStatusLabel.Text = "      Decoder Disconnected";
            this.Zf6_ConnectionStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CANcave_ConnectionStatusLabel
            // 
            this.CANcave_ConnectionStatusLabel.BackgroundColor = new Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.Vertical, System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(240)))), ((int)(((byte)(247))))), System.Drawing.Color.LightCyan);
            this.CANcave_ConnectionStatusLabel.BeforeTouchSize = new System.Drawing.Size(153, 20);
            this.CANcave_ConnectionStatusLabel.BorderSides = ((System.Windows.Forms.Border3DSide)((((System.Windows.Forms.Border3DSide.Left | System.Windows.Forms.Border3DSide.Top) 
            | System.Windows.Forms.Border3DSide.Right) 
            | System.Windows.Forms.Border3DSide.Bottom)));
            this.CANcave_ConnectionStatusLabel.Image = global::GearShift.Properties.Resources.DeviceStatuDisconnected_16x16;
            this.CANcave_ConnectionStatusLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CANcave_ConnectionStatusLabel.Location = new System.Drawing.Point(322, 2);
            this.CANcave_ConnectionStatusLabel.Name = "CANcave_ConnectionStatusLabel";
            this.CANcave_ConnectionStatusLabel.Size = new System.Drawing.Size(153, 20);
            this.CANcave_ConnectionStatusLabel.TabIndex = 1;
            this.CANcave_ConnectionStatusLabel.Text = "      CAN Disconnected";
            this.CANcave_ConnectionStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // showConsoleButton
            // 
            this.showConsoleButton.Image = global::GearShift.Properties.Resources.ShowConsole_17x16;
            this.showConsoleButton.Location = new System.Drawing.Point(477, 2);
            this.showConsoleButton.Name = "showConsoleButton";
            this.showConsoleButton.Size = new System.Drawing.Size(50, 20);
            this.showConsoleButton.TabIndex = 4;
            this.showConsoleButton.UseVisualStyleBackColor = true;
            this.showConsoleButton.Click += new System.EventHandler(this.showConsoleButton_Activate);
            // 
            // consolePreviewLabel
            // 
            this.consolePreviewLabel.AutoSize = true;
            this.consolePreviewLabel.BackgroundColor = new Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.Vertical, System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(240)))), ((int)(((byte)(247))))), System.Drawing.Color.LightCyan);
            this.consolePreviewLabel.BeforeTouchSize = new System.Drawing.Size(95, 20);
            this.consolePreviewLabel.BorderSides = ((System.Windows.Forms.Border3DSide)((((System.Windows.Forms.Border3DSide.Left | System.Windows.Forms.Border3DSide.Top) 
            | System.Windows.Forms.Border3DSide.Right) 
            | System.Windows.Forms.Border3DSide.Bottom)));
            this.consolePreviewLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.consolePreviewLabel.Location = new System.Drawing.Point(529, 2);
            this.consolePreviewLabel.MinimumSize = new System.Drawing.Size(0, 20);
            this.consolePreviewLabel.Name = "consolePreviewLabel";
            this.consolePreviewLabel.Size = new System.Drawing.Size(95, 20);
            this.consolePreviewLabel.TabIndex = 5;
            this.consolePreviewLabel.Text = "Console Preview...";
            this.consolePreviewLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.consolePreviewLabel.UseMnemonic = false;
            // 
            // progBarTitleLabel
            // 
            this.progBarTitleLabel.BackgroundColor = new Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.Vertical, System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(240)))), ((int)(((byte)(247))))), System.Drawing.Color.LightCyan);
            this.progBarTitleLabel.BeforeTouchSize = new System.Drawing.Size(75, 20);
            this.progBarTitleLabel.BorderSides = ((System.Windows.Forms.Border3DSide)((((System.Windows.Forms.Border3DSide.Left | System.Windows.Forms.Border3DSide.Top) 
            | System.Windows.Forms.Border3DSide.Right) 
            | System.Windows.Forms.Border3DSide.Bottom)));
            this.progBarTitleLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.progBarTitleLabel.Location = new System.Drawing.Point(626, 2);
            this.progBarTitleLabel.Name = "progBarTitleLabel";
            this.progBarTitleLabel.Size = new System.Drawing.Size(75, 20);
            this.progBarTitleLabel.TabIndex = 7;
            this.progBarTitleLabel.Text = "Oil Filter Wear";
            this.progBarTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // oilFilterProgressBar
            // 
            this.oilFilterProgressBar.BackgroundStyle = Syncfusion.Windows.Forms.Tools.ProgressBarBackgroundStyles.Gradient;
            this.oilFilterProgressBar.BackMultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.oilFilterProgressBar.BackSegments = false;
            this.oilFilterProgressBar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(149)))), ((int)(((byte)(152)))));
            this.oilFilterProgressBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.oilFilterProgressBar.CustomText = null;
            this.oilFilterProgressBar.CustomWaitingRender = false;
            this.oilFilterProgressBar.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.oilFilterProgressBar.ForegroundImage = null;
            this.oilFilterProgressBar.GradientEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(42)))));
            this.oilFilterProgressBar.GradientStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(42)))));
            this.oilFilterProgressBar.Location = new System.Drawing.Point(703, 2);
            this.oilFilterProgressBar.MultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.oilFilterProgressBar.Name = "oilFilterProgressBar";
            this.oilFilterProgressBar.ProgressStyle = Syncfusion.Windows.Forms.Tools.ProgressBarStyles.Metro;
            this.oilFilterProgressBar.SegmentWidth = 12;
            this.oilFilterProgressBar.Size = new System.Drawing.Size(133, 20);
            this.oilFilterProgressBar.TabIndex = 6;
            this.oilFilterProgressBar.Text = "Oil Filter";
            this.oilFilterProgressBar.TextStyle = Syncfusion.Windows.Forms.Tools.ProgressBarTextStyles.Custom;
            this.oilFilterProgressBar.ThemeName = "Metro";
            this.oilFilterProgressBar.Value = 5;
            this.oilFilterProgressBar.WaitingGradientWidth = 400;
            // 
            // tabControlDocs
            // 
            this.tabControlDocs.ActiveTabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.tabControlDocs.BeforeTouchSize = new System.Drawing.Size(1336, 684);
            this.tabControlDocs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tabControlDocs.BorderWidth = 0;
            this.tabControlDocs.Controls.Add(this.homeTab);
            this.tabControlDocs.Controls.Add(this.reportExplorerDocument);
            this.tabControlDocs.Controls.Add(this.scriptInstallerDocument);
            this.tabControlDocs.Controls.Add(this.settingsDocument);
            this.tabControlDocs.Controls.Add(this.CANTransControllerDocument);
            this.tabControlDocs.Controls.Add(this.initialTestDocument);
            this.tabControlDocs.Controls.Add(this.reportViewDocument);
            this.tabControlDocs.Controls.Add(this.Gm6T40BarePanelDocument);
            this.tabControlDocs.FocusOnTabClick = false;
            this.tabControlDocs.InactiveCloseButtonForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(178)))), ((int)(((byte)(178)))));
            this.tabControlDocs.Location = new System.Drawing.Point(218, 61);
            this.tabControlDocs.Multiline = true;
            this.tabControlDocs.Name = "Home ";
            this.tabControlDocs.ShowSuperToolTips = true;
            this.tabControlDocs.Size = new System.Drawing.Size(1336, 684);
            this.tabControlDocs.SizeMode = Syncfusion.Windows.Forms.Tools.TabSizeMode.FillToRight;
            this.tabControlDocs.TabIndex = 11;
            this.tabControlDocs.TabStyle = typeof(Syncfusion.Windows.Forms.Tools.TabRendererDockingWhidbeyBeta);
            this.tabControlDocs.ThemeName = "TabRendererDockingWhidbeyBeta";
            // 
            // homeTab
            // 
            this.homeTab.BackColor = System.Drawing.Color.DimGray;
            this.homeTab.ForeColor = System.Drawing.SystemColors.ControlText;
            this.homeTab.Image = null;
            this.homeTab.ImageSize = new System.Drawing.Size(16, 16);
            this.homeTab.Location = new System.Drawing.Point(0, 29);
            this.homeTab.Name = "homeTab";
            this.homeTab.ShowCloseButton = false;
            this.homeTab.Size = new System.Drawing.Size(1336, 655);
            this.homeTab.TabIndex = 1;
            this.homeTab.Text = "Home     ";
            this.homeTab.ThemesEnabled = false;
            // 
            // reportExplorerDocument
            // 
            this.reportExplorerDocument.BackColor = System.Drawing.Color.DimGray;
            this.reportExplorerDocument.Controls.Add(this.reportExplorer);
            this.reportExplorerDocument.ForeColor = System.Drawing.SystemColors.ControlText;
            this.reportExplorerDocument.Image = null;
            this.reportExplorerDocument.ImageSize = new System.Drawing.Size(16, 16);
            this.reportExplorerDocument.Location = new System.Drawing.Point(0, 29);
            this.reportExplorerDocument.Name = "reportExplorerDocument";
            this.reportExplorerDocument.ShowCloseButton = true;
            this.reportExplorerDocument.Size = new System.Drawing.Size(1336, 655);
            this.reportExplorerDocument.TabIndex = 1;
            this.reportExplorerDocument.Text = "Report explorer     ";
            this.reportExplorerDocument.ThemesEnabled = false;
            // 
            // scriptInstallerDocument
            // 
            this.scriptInstallerDocument.BackColor = System.Drawing.Color.DimGray;
            this.scriptInstallerDocument.Controls.Add(this.scriptInstallerPanel);
            this.scriptInstallerDocument.ForeColor = System.Drawing.SystemColors.ControlText;
            this.scriptInstallerDocument.Image = null;
            this.scriptInstallerDocument.ImageSize = new System.Drawing.Size(16, 16);
            this.scriptInstallerDocument.Location = new System.Drawing.Point(0, 29);
            this.scriptInstallerDocument.Name = "scriptInstallerDocument";
            this.scriptInstallerDocument.ShowCloseButton = true;
            this.scriptInstallerDocument.Size = new System.Drawing.Size(1336, 655);
            this.scriptInstallerDocument.TabIndex = 2;
            this.scriptInstallerDocument.Text = "Script Installer     ";
            this.scriptInstallerDocument.ThemesEnabled = false;
            // 
            // settingsDocument
            // 
            this.settingsDocument.BackColor = System.Drawing.Color.DimGray;
            this.settingsDocument.Controls.Add(this.settingsPanel);
            this.settingsDocument.ForeColor = System.Drawing.SystemColors.ControlText;
            this.settingsDocument.Image = null;
            this.settingsDocument.ImageSize = new System.Drawing.Size(16, 16);
            this.settingsDocument.Location = new System.Drawing.Point(0, 29);
            this.settingsDocument.Name = "settingsDocument";
            this.settingsDocument.ShowCloseButton = true;
            this.settingsDocument.Size = new System.Drawing.Size(1336, 655);
            this.settingsDocument.TabIndex = 3;
            this.settingsDocument.Text = "Settings     ";
            this.settingsDocument.ThemesEnabled = false;
            // 
            // CANTransControllerDocument
            // 
            this.CANTransControllerDocument.BackColor = System.Drawing.Color.DimGray;
            this.CANTransControllerDocument.Controls.Add(this.canPanel1);
            this.CANTransControllerDocument.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CANTransControllerDocument.Image = null;
            this.CANTransControllerDocument.ImageSize = new System.Drawing.Size(16, 16);
            this.CANTransControllerDocument.Location = new System.Drawing.Point(0, 29);
            this.CANTransControllerDocument.Name = "CANTransControllerDocument";
            this.CANTransControllerDocument.ShowCloseButton = true;
            this.CANTransControllerDocument.Size = new System.Drawing.Size(1336, 655);
            this.CANTransControllerDocument.TabIndex = 4;
            this.CANTransControllerDocument.Text = "CAN Transmission Controller     ";
            this.CANTransControllerDocument.ThemesEnabled = false;
            // 
            // initialTestDocument
            // 
            this.initialTestDocument.BackColor = System.Drawing.Color.DimGray;
            this.initialTestDocument.Controls.Add(this.initialTest);
            this.initialTestDocument.ForeColor = System.Drawing.SystemColors.ControlText;
            this.initialTestDocument.Image = null;
            this.initialTestDocument.ImageSize = new System.Drawing.Size(16, 16);
            this.initialTestDocument.Location = new System.Drawing.Point(0, 29);
            this.initialTestDocument.Name = "initialTestDocument";
            this.initialTestDocument.ShowCloseButton = true;
            this.initialTestDocument.Size = new System.Drawing.Size(1336, 655);
            this.initialTestDocument.TabIndex = 1;
            this.initialTestDocument.Text = "Initial Test     ";
            this.initialTestDocument.ThemesEnabled = false;
            // 
            // reportViewDocument
            // 
            this.reportViewDocument.BackColor = System.Drawing.Color.DimGray;
            this.reportViewDocument.Controls.Add(this.resultsViewer);
            this.reportViewDocument.ForeColor = System.Drawing.SystemColors.ControlText;
            this.reportViewDocument.Image = null;
            this.reportViewDocument.ImageSize = new System.Drawing.Size(16, 16);
            this.reportViewDocument.Location = new System.Drawing.Point(0, 29);
            this.reportViewDocument.Name = "reportViewDocument";
            this.reportViewDocument.ShowCloseButton = true;
            this.reportViewDocument.Size = new System.Drawing.Size(1336, 655);
            this.reportViewDocument.TabIndex = 1;
            this.reportViewDocument.Text = "Report Viewer     ";
            this.reportViewDocument.ThemesEnabled = false;
            // 
            // resultsViewer
            // 
            this.resultsViewer.BackColor = System.Drawing.Color.Transparent;
            this.resultsViewer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.resultsViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsViewer.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.resultsViewer.Location = new System.Drawing.Point(0, 0);
            this.resultsViewer.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.resultsViewer.Name = "resultsViewer";
            this.resultsViewer.Size = new System.Drawing.Size(1336, 655);
            this.resultsViewer.TabIndex = 1;
            // 
            // Gm6T40BarePanelDocument
            // 
            this.Gm6T40BarePanelDocument.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Gm6T40BarePanelDocument.Image = null;
            this.Gm6T40BarePanelDocument.ImageSize = new System.Drawing.Size(16, 16);
            this.Gm6T40BarePanelDocument.Location = new System.Drawing.Point(0, 29);
            this.Gm6T40BarePanelDocument.Name = "Gm6T40BarePanelDocument";
            this.Gm6T40BarePanelDocument.ShowCloseButton = true;
            this.Gm6T40BarePanelDocument.Size = new System.Drawing.Size(1336, 655);
            this.Gm6T40BarePanelDocument.TabIndex = 14;
            this.Gm6T40BarePanelDocument.Text = "GMLAN 6T40     ";
            this.Gm6T40BarePanelDocument.ThemesEnabled = false;
            // 
            // pressureDisplaysDocument
            // 
            this.pressureDisplaysDocument.BackColor = System.Drawing.Color.DimGray;
            this.pressureDisplaysDocument.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pressureDisplaysDocument.Image = null;
            this.pressureDisplaysDocument.ImageSize = new System.Drawing.Size(16, 16);
            this.pressureDisplaysDocument.Location = new System.Drawing.Point(0, 29);
            this.pressureDisplaysDocument.Name = "pressureDisplaysDocument";
            this.pressureDisplaysDocument.ShowCloseButton = true;
            this.pressureDisplaysDocument.Size = new System.Drawing.Size(1336, 655);
            this.pressureDisplaysDocument.TabFont = null;
            this.pressureDisplaysDocument.TabIndex = 1;
            this.pressureDisplaysDocument.Text = "Gauges     ";
            this.pressureDisplaysDocument.ThemesEnabled = false;
            // 
            // currentDisplaysDocument
            // 
            this.currentDisplaysDocument.BackColor = System.Drawing.Color.DimGray;
            this.currentDisplaysDocument.ForeColor = System.Drawing.SystemColors.ControlText;
            this.currentDisplaysDocument.Image = null;
            this.currentDisplaysDocument.ImageSize = new System.Drawing.Size(16, 16);
            this.currentDisplaysDocument.Location = new System.Drawing.Point(0, 29);
            this.currentDisplaysDocument.Name = "currentDisplaysDocument";
            this.currentDisplaysDocument.ShowCloseButton = true;
            this.currentDisplaysDocument.Size = new System.Drawing.Size(1336, 655);
            this.currentDisplaysDocument.TabFont = null;
            this.currentDisplaysDocument.TabIndex = 1;
            this.currentDisplaysDocument.Text = "Currents     ";
            this.currentDisplaysDocument.ThemesEnabled = false;
            // 
            // livePreviewDocument
            // 
            this.livePreviewDocument.BackColor = System.Drawing.Color.DimGray;
            this.livePreviewDocument.ForeColor = System.Drawing.SystemColors.ControlText;
            this.livePreviewDocument.Image = null;
            this.livePreviewDocument.ImageSize = new System.Drawing.Size(16, 16);
            this.livePreviewDocument.Location = new System.Drawing.Point(0, 29);
            this.livePreviewDocument.Name = "livePreviewDocument";
            this.livePreviewDocument.ShowCloseButton = true;
            this.livePreviewDocument.Size = new System.Drawing.Size(1336, 655);
            this.livePreviewDocument.TabFont = null;
            this.livePreviewDocument.TabIndex = 1;
            this.livePreviewDocument.Text = "Live preview     ";
            this.livePreviewDocument.ThemesEnabled = false;
            // 
            // superToolTip1
            // 
            this.superToolTip1.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(158)))), ((int)(((byte)(218)))));
            this.superToolTip1.ThemeName = "Office2016Colorful";
            this.superToolTip1.ToolTipDuration = 5;
            this.superToolTip1.VisualStyle = Syncfusion.Windows.Forms.Tools.SuperToolTip.Appearance.Office2016Colorful;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1556, 884);
            this.Controls.Add(this.backStage1);
            this.Controls.Add(this.statusBarExt1);
            this.Controls.Add(this.mainRibbon);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(1024, 748);
            this.Name = "FormMain";
            this.Padding = new System.Windows.Forms.Padding(1, 0, 1, 1);
            this.ShowApplicationIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GearShift";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dockingManager1)).EndInit();
            this.panel5.ResumeLayout(false);
            this.gaugesDocPanel.ResumeLayout(false);
            this.currentsDocPanel.ResumeLayout(false);
            this.currBarsPanel.ResumeLayout(false);
            this.livePreviewDocPanel.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainRibbon)).EndInit();
            this.mainRibbon.ResumeLayout(false);
            this.mainRibbon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.backStage1)).EndInit();
            this.backStage1.ResumeLayout(false);
            this.backStage1.PerformLayout();
            this.infobackStageTab.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.viewBackStageTab.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pluginsBackStageTab.ResumeLayout(false);
            this.settingsPanel.ResumeLayout(false);
            this.scriptInstallerPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.statusBarExt1)).EndInit();
            this.statusBarExt1.ResumeLayout(false);
            this.statusBarExt1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.oilFilterProgressBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControlDocs)).EndInit();
            this.tabControlDocs.ResumeLayout(false);
            this.reportExplorerDocument.ResumeLayout(false);
            this.scriptInstallerDocument.ResumeLayout(false);
            this.settingsDocument.ResumeLayout(false);
            this.CANTransControllerDocument.ResumeLayout(false);
            this.initialTestDocument.ResumeLayout(false);
            this.reportViewDocument.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Syncfusion.Windows.Forms.Tools.RibbonControlAdv mainRibbon;
        private Syncfusion.Windows.Forms.BackStageView backStageView1;
        private Syncfusion.Windows.Forms.BackStage backStage1;
        private Syncfusion.Windows.Forms.BackStageTab infobackStageTab;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Syncfusion.Windows.Forms.BackStageButton closebackStageButton;
        private GST.Gearshift.Components.Forms.DAQ.TestControlPanel testControlPanel;
        private System.Windows.Forms.Timer controlsUpdateTimer;
        private Soko.Common.Controls.NiceButton startTestButton;
        private Soko.Common.Controls.NicePanel tooltipsPanel;
        private Soko.Common.Controls.NicePanel gaugesPanel;        
        private Soko.Common.Controls.NicePanel currBarsPanel;
        private GST.Gearshift.Components.Controls.Gauges.SolenoidGauge solenoidGauge1;
        private GST.Gearshift.Components.Forms.DAQ.ChannelsInitialTest initialTest;
        //private TD.SandDock.TabbedDocument CANTransControllerDocument;
        private GST.Gearshift.Components.Forms.CAN.CANPanel canPanel1;
        //private TD.SandDock.TabbedDocument obdIIToolDocument;
        //private GST.Gearshift.Components.Forms.OBD.OBDPanel obdPanel1;
        private GST.Gearshift.Components.Forms.DAQ.ReportViewer resultsViewer;
        private GST.Gearshift.Components.Forms.DAQ.ReportExplorer reportExplorer;
        private Soko.Common.Controls.NicePanel scriptInstallerPanel;
        private Soko.Common.Controls.NicePanel settingsPanel;
        private GST.Gearshift.Components.Forms.SettingsEditor settingsEditor1;        
        private GST.Gearshift.Components.Forms.DAQ.LivePreviewPanel livePreviewPanel3;
        private Syncfusion.Windows.Forms.Tools.ToolStripTabItem toolStripTabItemHome;
        private Syncfusion.Windows.Forms.BackStageButton reportsMenuItem;
        private Syncfusion.Windows.Forms.BackStageButton ExitBtnMenuItem;
        private Syncfusion.Windows.Forms.BackStageTab viewBackStageTab;
        private Syncfusion.Windows.Forms.BackStageButton controlAOsMenuItem;
        private Syncfusion.Windows.Forms.BackStageButton systemOptionsMenuItem;
        private Syncfusion.Windows.Forms.BackStageButton firmwareMenuItem;
        private Syncfusion.Windows.Forms.BackStageTab pluginsBackStageTab;
        private Syncfusion.Windows.Forms.BackStageButton ChangelogMainMenuItem;
        private Syncfusion.Windows.Forms.BackStageButton showControlPanelMainMenuItem;
        private System.Windows.Forms.ToolStripButton buttonConnect;
        private System.Windows.Forms.Label gstLabel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label viewTitleLabel;
        private System.Windows.Forms.Button showLivePreviewItem;
        private System.Windows.Forms.Button showCurrentsMenuItem;
        private System.Windows.Forms.Button showPressuresMenuItem;
        private System.Windows.Forms.Button showConsoleMenuItem;
        private System.Windows.Forms.Label viewDesLabel;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel gaugesDocPanel;
        private System.Windows.Forms.Panel currentsDocPanel;
        private System.Windows.Forms.Panel livePreviewDocPanel;
        private System.Windows.Forms.Button gmLanTestEnvMenuItem;
        private System.Windows.Forms.Label pluginsDesLabel;
        private System.Windows.Forms.Label pluginTitleLabel;
        private Syncfusion.Windows.Forms.Tools.Controls.StatusBar.StatusBarExt statusBarExt1;
        private GradientLabel CANcave_ConnectionStatusLabel;
        private GradientLabel EZS_ConnectionStatusLabel;
        private GradientLabel Zf6_ConnectionStatusLabel;
        private System.Windows.Forms.Button showConsoleButton;
        private GradientLabel consolePreviewLabel;
        private GradientLabel progBarTitleLabel;
        private ProgressBarAdv oilFilterProgressBar;
        private Syncfusion.Windows.Forms.Tools.TabControlAdv tabControlDocs;
        private Syncfusion.Windows.Forms.Tools.TabPageAdv homeTab;
        private Syncfusion.Windows.Forms.Tools.TabPageAdv reportExplorerDocument;
        private Syncfusion.Windows.Forms.Tools.TabPageAdv scriptInstallerDocument;
        private Syncfusion.Windows.Forms.Tools.TabPageAdv settingsDocument;
        private Syncfusion.Windows.Forms.Tools.TabPageAdv CANTransControllerDocument;
        private Syncfusion.Windows.Forms.Tools.TabPageAdv Gm6T40BarePanelDocument;
        private Syncfusion.Windows.Forms.Tools.TabPageAdv initialTestDocument;
        private Syncfusion.Windows.Forms.Tools.TabPageAdv pressureDisplaysDocument;
        private Syncfusion.Windows.Forms.Tools.TabPageAdv currentDisplaysDocument;
        private Syncfusion.Windows.Forms.Tools.TabPageAdv livePreviewDocument;
        private Syncfusion.Windows.Forms.Tools.TabPageAdv reportViewDocument;
        private DockingManager dockingManager1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel consoleWindow;
        private SuperToolTip superToolTip1;
        private Soko.Common.Controls.NiceButton installScriptPackButton;


        // private  GST.Gearshift.Components.Forms.OBDPanel obdPanel;
    }
}