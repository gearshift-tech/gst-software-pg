﻿
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Serialization;



using GST.Gearshift.Components.Interfaces.USB;
using Soko.Common.Controls;
using GST.Gearshift.Components.Controls;
using GST.Gearshift.Components.Forms;
using GST.Gearshift.Components.Forms.DAQ;
using GST.Gearshift.Components.Forms.CAN;
//using GST.Gearshift.Components.Forms.OBD; Not used
using GST.Gearshift.Components.Utilities;

using GST.ZF6.Components.Interfaces;
using GST.ZF6.Components.Interfaces.MechShifterUSB;

using Soko.Common.Common;

using Syncfusion.WinForms.Controls;
using Syncfusion.Windows.Forms.Tools;
using System.Drawing.Drawing2D;
using Syncfusion.Runtime.Serialization;

namespace GearShift
{
    public partial class FormMain : Syncfusion.Windows.Forms.Tools.RibbonForm
    {

        #region Constants



        #endregion  Constants



        #region Private fields

        // Current gearbox config (used to display controls)
        private GearboxConfig mCurrentGearbox = null;

        // GearShift USB interface
        private GearShiftUsb _usbDev_GearShift = null;

        // Zf6 USB interface
        private GST.ZF6.Components.Interfaces.MechShifterUSB.UsbDevice _usbDev_Decoder = null;

        // Zf6 CANPRO USB interface
        private Soko.CanCave.Components.Interfaces.CanCaveUsb.UsbDevice _usbDev_CanCave = null;

        // GM 6Txx TCU governor
        private Soko.CanCave.Components.Interfaces.TCUGovernor_GM6Txx _GM6TxxTcuGov = null;

        // Nissan RE5 TCU governor
        private Soko.CanCave.Components.Interfaces.TCUGovernor_NissanRE5 _NissanRE5TcuGov = null;

        // Measurement session
        private Measurement mMeasurement = null;

        // Working copy of a currently opened report
        private TestScriptReport mReport = null;

        // Currently open test script
        private TestScript mTestScript = null;

        // Console output
        private Soko.Common.Controls.ConsoleOutput consoleOutput;

        // Background worker to show background user prompts
        private BackgroundWorker mPromptShowingBgWorker = new BackgroundWorker();

        // String holding the path to the last run and saved test report
        private string mLastSavedReportPath = String.Empty;
        

        #endregion Private fields



        #region Constructors & finalizer
        DockingClientPanel panel = new DockingClientPanel();
        //DockingClientPanel panel10 = new DockingClientPanel();
        int count = 0;
        //DockingClientPanel panel10 = new DockingClientPanel();
        private void InitSuiteVersion()
        {

            GST.Gearshift.Components.Forms.HomeScreenPanel homeScreenPanel1 = new GST.Gearshift.Components.Forms.HomeScreenPanel();
            homeScreenPanel1.BackColor = System.Drawing.Color.Transparent;
            homeScreenPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            homeScreenPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            homeScreenPanel1.Location = new System.Drawing.Point(0, 0);
            homeScreenPanel1.Name = "homeScreenPanel1";
            homeScreenPanel1.Size = new System.Drawing.Size(991, 752);
            homeScreenPanel1.TabIndex = 6;
            homeScreenPanel1.viewReportsButtonClicked += new System.EventHandler(homeScreenPanel1_viewReportsButtonClicked);
            homeScreenPanel1.installTestsButtonClicked += new System.EventHandler(homeScreenPanel1_installTestsButtonClicked);
            homeScreenPanel1.systemOptionsButtonClicked += new System.EventHandler(homeScreenPanel1_systemOptionsButtonClicked);
            homeScreenPanel1.canbusUtilsButtonClicked += new System.EventHandler(homeScreenPanel1_canbusUtilsButtonClicked);
            homeScreenPanel1.testManagerButtonClicked += new System.EventHandler(homeScreenPanel1_testManagerButtonClicked);
            homeScreenPanel1.aoUtilsButtonClicked += new System.EventHandler(homeScreenPanel1_aoUtilsButtonClicked);
            homeScreenPanel1.techSupportButtonClicked += new System.EventHandler(homeScreenPanel1_techSupportButtonClicked);
            homeScreenPanel1.resDocsButtonClicked += new System.EventHandler(homeScreenPanel1_resDocsButtonClicked);
            homeScreenPanel1.ExitButtonClicked += new System.EventHandler(homeScreenPanel1_ExitButtonClicked);
            homeScreenPanel1.StartTestButtonClicked += new System.EventHandler(homeScreenPanel1_StartTestButtonClicked);

            GST.Gearshift.Components.Forms.HomeScreenPanel_SolTester homeScreenPanel2 = new GST.Gearshift.Components.Forms.HomeScreenPanel_SolTester();
            homeScreenPanel2.BackColor = System.Drawing.Color.Transparent;
            homeScreenPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            homeScreenPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            homeScreenPanel2.Location = new System.Drawing.Point(0, 0);
            homeScreenPanel2.Name = "homeScreenPanel2";
            homeScreenPanel2.Size = new System.Drawing.Size(991, 752);
            homeScreenPanel2.TabIndex = 6;
            homeScreenPanel2.viewReportsButtonClicked += new System.EventHandler(homeScreenPanel1_viewReportsButtonClicked);
            homeScreenPanel2.installTestsButtonClicked += new System.EventHandler(homeScreenPanel1_installTestsButtonClicked);
            homeScreenPanel2.systemOptionsButtonClicked += new System.EventHandler(homeScreenPanel1_systemOptionsButtonClicked);
            homeScreenPanel2.canbusUtilsButtonClicked += new System.EventHandler(homeScreenPanel1_canbusUtilsButtonClicked);
            homeScreenPanel2.testManagerButtonClicked += new System.EventHandler(homeScreenPanel1_testManagerButtonClicked);
            homeScreenPanel2.aoUtilsButtonClicked += new System.EventHandler(homeScreenPanel1_aoUtilsButtonClicked);
            homeScreenPanel2.techSupportButtonClicked += new System.EventHandler(homeScreenPanel1_techSupportButtonClicked);
            homeScreenPanel2.resDocsButtonClicked += new System.EventHandler(homeScreenPanel1_resDocsButtonClicked);
            homeScreenPanel2.ExitButtonClicked += new System.EventHandler(homeScreenPanel1_ExitButtonClicked);
            homeScreenPanel2.StartTestButtonClicked += new System.EventHandler(homeScreenPanel1_StartTestButtonClicked);

#if _VB_SUITE_
            //this.Controls.Add(homeScreenPanel1);
            this.homeTab.Controls.Add(homeScreenPanel1);
            //this.homeTab.ShowCloseButton = false;
#endif
#if _DYNO_SUITE_
      this.homeTab.Controls.Add(homeScreenPanel1);
#endif
#if _SOL_TESTER_
      this.HomeTabbedDocument.Controls.Add(homeScreenPanel2);
      applicationMenu1.Items.Remove(applicationMenu1.Items[5]);
#endif



        }


        public FormMain()
        {
            // Before doing anything else, load the singleton settings file
            GST.Gearshift.Components.Utilities.Settings appSettings = GST.Gearshift.Components.Utilities.Settings.Instance;
            appSettings.LoadSettingsFromDisk();

            // Initialize form components
            InitializeComponent();
            
            // Sort out suite version mods
            InitSuiteVersion();
            
            this.dockingManager1.LoadDesignerDockState();
            this.dockingManager1.DragProviderStyle = DragProviderStyle.VS2012;
            this.homeTab.BorderStyle = BorderStyle.Fixed3D;
            ///this.Controls.Add(this.panel);
            
            this.panel.Controls.Add(this.tabControlDocs);
            this.tabControlDocs.Dock = DockStyle.Fill;
            this.dockingManager1.DockAsDocument(tabControlDocs);
            
            //this.homeTab.ShowCloseButton = true;

            //this.panel.Controls.Add(this.tabControlDocs);


            //this.dockingManager1.DragProviderStyle = DragProviderStyle.VS2012;
            //this.homeTab.BorderStyle = BorderStyle.Fixed3D;
            //this.Controls.Add(this.panel);
            //this.panel.Controls.Add(this.tabControlDocs);
            //this.tabControlDocs.Dock = DockStyle.Fill;
            //this.homeTab.ShowCloseButton = true;

            this.tabControlDocs.DrawItem += new DrawTabEventHandler(tabControlDocs_DrawItem);
            this.tabControlDocs.MouseDown += new MouseEventHandler(tabControlDocs_MouseDown);
            //this.homeTab.Closed += new System.EventHandler(this.tabControlDocs_Closed);
            this.dockingManager1.SetCloseButtonVisibility(this.panel5, false);

            this.dockingManager1.SetDockVisibility(this.panel5, false);

            this.dockingManager1.SetDockVisibility(this.consoleWindow, false);
            this.dockingManager1.NewDockStateEndLoad += new EventHandler(dockingManager1_NewDockStateEndLoad);
            // Check if the application was properly installed and has the application data folder okay
            try
            {
                string lol = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                OBD_TroubleCodesSet tcs = new OBD_TroubleCodesSet();
                tcs.LoadDirectory(lol + "\\GearShift Technologies\\GearShift\\OBD_Data\\Troublecodes");
            }
            catch (Exception)
            {
                Soko.Common.Forms.MessageBox.ShowCritical("GearShift",
                   "Application data error",
                   "The application seems to be improperly installed or has a problem accessing the application data. Please contact the GearShift Technologies support for help. It will now close.",
                   Soko.Common.Forms.MessageBoxButtons.OK);
                this.Close();
                return;
            }

            // Set the main window state basing on the app settings
            this.WindowState = appSettings.MainFormWindowState;
            //this.WindowState = FormWindowState.Normal;
            //this.Size = new Size(1024, 768);

            // Set the window title to have the version number
            this.Text = GST.Gearshift.Components.Utilities.Settings.MainFormTitle;

            consoleOutput = new Soko.Common.Controls.ConsoleOutput();
            consoleOutput.AutoScroll = true;
            consoleOutput.Dock = DockStyle.Fill;
            consoleWindow.Controls.Add(consoleOutput);
            AddConsoleLog("Application started successfully.");
            gaugesPanel.Dock = DockStyle.Fill;
            this.gaugesDocPanel.Controls.Add(gaugesPanel);
            // Initialize Zf6 USB
            _usbDev_Decoder = new GST.ZF6.Components.Interfaces.MechShifterUSB.UsbDevice();
            _usbDev_Decoder.Connected += new EventHandler(_zf6USB_Connected);
            _usbDev_Decoder.Disconnected += new EventHandler(_zf6USB_Disconnected);
            _usbDev_Decoder.AutoConnectEnabled = true;

            // Initialize CANPRO USB interface
            _usbDev_CanCave = new Soko.CanCave.Components.Interfaces.CanCaveUsb.UsbDevice();
            _usbDev_CanCave.Connected += new EventHandler(_CanProUSB_Connected);
            _usbDev_CanCave.Disconnected += new System.EventHandler(_CanProUSB_Disconnected);
            _usbDev_CanCave.AutoConnectEnabled = true;
            // Initialize GM 6Txx TCU governor
            _GM6TxxTcuGov = new Soko.CanCave.Components.Interfaces.TCUGovernor_GM6Txx(_usbDev_CanCave);
            // Initialize Nissan RE5 TCU governor
            _NissanRE5TcuGov = new Soko.CanCave.Components.Interfaces.TCUGovernor_NissanRE5(_usbDev_CanCave);
            // initialize the GM6T40 bare panel document
            Soko.CanCave.Components.Forms.Gm6T40BarePanel gm6T40BarePanel1 = new Soko.CanCave.Components.Forms.Gm6T40BarePanel(_GM6TxxTcuGov);
            this.Gm6T40BarePanelDocument.Controls.Add(gm6T40BarePanel1);
            gm6T40BarePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            //gm6T40BarePanel1.Location = new System.Drawing.Point(0, 0);
            gm6T40BarePanel1.Name = "gm6T40BarePanel1";
            //this.gm6T40BarePanel1.Size = new System.Drawing.Size(991, 752);
            gm6T40BarePanel1.TabIndex = 0;


            // Initialize GearShiftUSB
            _usbDev_GearShift = new GearShiftUsb(true);
            _usbDev_GearShift.DeviceConnectedEvent += new EventHandler(_usbDev_GearShift_DeviceConnectedEvent);
            _usbDev_GearShift.DeviceDisconnecedEvent += new EventHandler(_usbDev_GearShift_DeviceDisconnecedEvent);
            _usbDev_GearShift.DAQ_OvercurrentDetectedEvent += new EventHandler(DAQ_OvercurrentDetected);
            _usbDev_GearShift.UsbDev_Decoder = _usbDev_Decoder;
            

            mTestScript = new TestScript();
            mReport = new TestScriptReport();
            mMeasurement = new Measurement();
            mMeasurement.Device = _usbDev_GearShift;
            mMeasurement.UsbDev_Decoder = _usbDev_Decoder;
            mMeasurement.GM6TxxGovernor = _GM6TxxTcuGov;
            mMeasurement.NissanRE5xxGov = _NissanRE5TcuGov;
            _usbDev_GearShift.SetNissanRE5_Interface(_NissanRE5TcuGov);
            canPanel1.Device = _usbDev_GearShift;
            //obdPanel1.Device = _usbDev_GearShift;
            testControlPanel.MeasurementSession = mMeasurement;
            initialTest.MeasurementSession = mMeasurement;
            mMeasurement.MainTestStartedEvent += new EventHandler(MainMeasurementStartedEH);
            mMeasurement.MainTestStoppedEvent += new EventHandler(MainMeasurementStoppedEH);
            mMeasurement.InitialTestStartedEvent += new EventHandler(InitialTestStartedEH);
            mMeasurement.InitialTestFinishedEvent += new EventHandler(InitialTestStoppedEH);
            mMeasurement.DisplayPromptEvent += new EventHandler(ShowPrompt);
            mMeasurement.GearSwitchedEvent += new Measurement.GearSwitchedEventHandler(mMeasurement_GearSwitchedEvent);
            
			HideAllDocuments();
            this.dockingManager1.EnableContextMenu = false;

            //this.testControlPanel.ContextMenu.Show(false);

            InitializeControls();
            DisplayCurrentValues();
            DisplayPressureValues();

            mMeasurement.Report = mReport;
            //resultsViewer.TestScript_ = mTestScript;
            CurrentGearbox = mTestScript.Gearbox;

#if (!_SOL_TESTER_)
            if (!Properties.QuickSettings.Default.WasWhatsNewDisplayed)
            {
                System.Diagnostics.Process.Start("CHANGELOG.txt");
                Properties.QuickSettings.Default.WasWhatsNewDisplayed = true;
                Properties.QuickSettings.Default.Save();
            }
#endif


        }
        //this.tabPageAdv1.Closing += new TabPageAdvClosingEventHandler(this.tabPageAdv1_Closing);
        //this.tabPageAdv1.Closed += new System.EventHandler(this.tabPageAdv1_Closed);

        //private void tabPageAdv1_Closing(object sender, TabPageAdvClosingEventArgs args)
        //        {
        //            ...
        //}

        //private void tabControlDocs_Closed(object sender, EventArgs e)
        //{
            
        //}

        private void dockingManager1_NewDockStateEndLoad(object sender, EventArgs e)
        {
            //this.dockingManager1.DocumentWindowSettings.AllowDragging = false;
            //this.dockingManager1.EnableDocumentMode = true;
            if (count <= 0)
            {
                this.dockingManager1.SetCloseButtonVisibility(this.panel5, false);

                this.dockingManager1.SetDockVisibility(this.panel5, false);

                this.dockingManager1.SetDockVisibility(this.consoleWindow, false);
            }
            this.dockingManager1.DockAsDocument(tabControlDocs);
            //this.dockingManager1.DockAsDocument(panel6);
            //this.dockingManager1.DockAsDocument(panel7);
            //this.dockingManager1.DockAsDocument(panel8);
            //this.dockingManager1.SetDockVisibility(this.panel9, true);
            //this.dockingManager1.SetDockVisibility(this.panel6, false);
            this.dockingManager1.DockAsDocument(this.gaugesDocPanel);
            this.dockingManager1.DockAsDocument(this.livePreviewDocPanel);
            this.dockingManager1.DockAsDocument(this.currentsDocPanel);
            
            

            //this.dockingManager1.DockAsDocument(this.panel);






        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }


        #endregion Constructors & finalizer



        #region Events


        #endregion Events



        #region Properties

        /// <summary>
        /// Currently displayed gearbox
        /// </summary>
        private GearboxConfig CurrentGearbox
        {
            get { return mCurrentGearbox; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("The value cannot be null!");
                mCurrentGearbox = value;
                LoadAIDisplays(CurrentGearbox);
                LoadCurrentDisplays(CurrentGearbox);
                //testControlPanel.UpdateSlidersConfig();
                //initialTest.Channels = mCurrentGearbox.CurrentDisplayChannelsSet;

            }
        }

        #endregion Properties



        #region Methods & delegates

        #region Menu and general UI

        // DrawItem Event Handler
        private void tabControlDocs_DrawItem(object sender,DrawTabEventArgs e)
        {
            //Drawing the normal appearences
            e.DrawBackground();
            e.DrawBorders();
            e.DrawInterior();
            if (e.Index == this.tabControlDocs.SelectedIndex)
            {
                if (!this.tabControlDocs.SelectedTab.Name.Equals("homeTab"))
                { 
                    //Getting the bounds for Tab
                    Rectangle tabrect = tabControlDocs.GetTabRect(e.Index);
                    tabrect.Offset(tabrect.Width - 35, 8);
                    tabrect.Width = 8;
                    tabrect.Height = 8;
                    //Initializing Brush and Pen
                    Brush brush = new SolidBrush(Color.Black);
                    Pen pen = new Pen(brush);
                    // Drawing close mar;
                    e.Graphics.DrawLine(pen, tabrect.X, tabrect.Y, tabrect.X + tabrect.Width, tabrect.Y + tabrect.Height);
                    e.Graphics.DrawLine(pen, tabrect.X + tabrect.Width, tabrect.Y, tabrect.X, tabrect.Y + tabrect.Height);
                }
            }




        }
        private void tabControlDocs_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //Getting the current mouse location
            Point point = new Point(e.X, e.Y);
            //Checking all Tab bounds for a match

            
            for (int i = 0; i < tabControlDocs.TabCount; i++)
  {
                Rectangle tabrect = tabControlDocs.GetTabRect(i);
     
                tabrect.Offset(tabrect.Width - 35, 8);
                tabrect.Width = 8;
                tabrect.Height = 8;                
                if (tabrect.Contains(point)  && !tabControlDocs.TabPages[i].Name.Equals("homeTab"))
                {

                    tabControlDocs.TabPages.Remove(tabControlDocs.TabPages[i]);
                    tabControlDocs.SelectedTab = homeTab;

                }


            }
        }
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (null != _usbDev_Decoder)
                _usbDev_Decoder.Disconnect();
            if (null != _usbDev_CanCave)
                _usbDev_CanCave.Disconnect();
        }
        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Control == true && e.KeyCode == Keys.G)
            {
                gmLanTestEnvMenuItem.PerformClick();
            }
        }
        private void SaveScreenLayout()
        {
            // serialize layout to the current open test script
            //mTestScript.SerializedScreenLayout = .GetLayout();

            // save the file to disk
            mTestScript.SaveXml(mTestScript.Filename);
        }

        private void AddConsolePreviewText(string text)
        {
            consolePreviewLabel.Text = text;
        }

        private void AddConsoleLog(string text)
        {
            consoleOutput.AddText(text);
            AddConsolePreviewText("  " + text + "     ");
        }

        private void ShowPromptInBg(string message, Measurement measurement)
        {
            Soko.Common.Forms.MessageBox msgbx = new Soko.Common.Forms.MessageBox("GearShift", "Operator message", message);
            msgbx.RemoveButtons();
            msgbx.AddButton(DialogResult.OK, "OK");
            //msgbx.ButtonsAligment = HorizontalAlignment.Center;
            msgbx.MessageBoxIcon = Soko.Common.Forms.MessageBoxIcon.Information;
            msgbx.ShowDialog();
            measurement.AcceptPrompt();
        }

        private void homeScreenPanel1_viewReportsButtonClicked(object sender, EventArgs e)
        {
            ShowReportExplorerDocument();
            reportExplorer.RefreshDiskContent();
        }

        private void homeScreenPanel1_canbusUtilsButtonClicked(object sender, EventArgs e)
        {
            EnableCANMode();
        }

        private void homeScreenPanel1_ExitButtonClicked(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void homeScreenPanel1_installTestsButtonClicked(object sender, EventArgs e)
        {
            // scriptInstallerDocument.Open();

            if (!this.tabControlDocs.SelectedTab.Name.Equals("scriptInstallerDocument"))
            {


                this.tabControlDocs.SelectedTab = this.scriptInstallerDocument;
                if (!this.tabControlDocs.SelectedTab.Name.Equals("scriptInstallerDocument"))
                {

                    tabControlDocs.TabPages.Add(this.scriptInstallerDocument);
                    this.tabControlDocs.SelectedTab = this.scriptInstallerDocument;
                }
            }

        }

        private void homeScreenPanel1_aoUtilsButtonClicked(object sender, EventArgs e)
        {
            controlAOsMenuItem_Activate(sender, e);
        }

        private void homeScreenPanel1_systemOptionsButtonClicked(object sender, EventArgs e)
        {
            // settingsDocument.Open();
            ShowSettingsDocument();
        }

        private void homeScreenPanel1_resDocsButtonClicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(GST.Gearshift.Components.Utilities.Settings.ResourcesDirectory);
        }

        private void homeScreenPanel1_techSupportButtonClicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:support@gearshifttechnologies.com");
        }

        private void obdToolsMenuItem_Activate(object sender, EventArgs e)
        {
            EnableOBDMode();
        }

        private void homeScreenPanel1_testManagerButtonClicked(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("GearShift Editor.exe");
            }
            catch (Exception)
            {
                Soko.Common.Forms.MessageBox.ShowCritical("GearShift",
                                                      "Operation failed",
                                                      "Cannot locate the GearShift Editor executable",
                                                      Soko.Common.Forms.MessageBoxButtons.OK);
            }
        }

        private void showLivePreviewItem_Activate(object sender, EventArgs e)
        {
            // livePreviewDocument.Open();
            this.mainRibbon.BackStageView.HideBackStage();
            //if (!this.tabControlDocs.SelectedTab.Name.Equals("livePreviewDocument"))
            //{


            //    this.tabControlDocs.SelectedTab = this.livePreviewDocument;
            //    if (!this.tabControlDocs.SelectedTab.Name.Equals("livePreviewDocument"))
            //    {

            //        tabControlDocs.TabPages.Add(this.livePreviewDocument);
            //        this.tabControlDocs.SelectedTab = this.livePreviewDocument;
            //    }
            //}
            ////this.panel.Controls.Add(this.livePreviewDocument);
            this.dockingManager1.DockAsDocument(this.livePreviewDocPanel);
        }

        private void reportsSearchButton_Activate(object sender, EventArgs e)
        {
            this.mainRibbon.BackStageView.HideBackStage();
            reportExplorer.RefreshDiskContent();
            ShowReportExplorerDocument();
        }

        private void printReportButton_Activate(object sender, EventArgs e)
        {
            resultsViewer.PrintCurrentReport();
        }

        private void homeDAQbutton_Click(object sender, EventArgs e)
        {
            EnableDAQMode();
        }

        private void obdStartButton_Activate(object sender, EventArgs e)
        {
            ShowObdIIToolDocument();
        }

        private void consoleMenuItem_Activate(object sender, EventArgs e)
        {
            //consoleWindow.Open();
            this.mainRibbon.BackStageView.HideBackStage();
        }

        private void showConsoleButton_Activate(object sender, EventArgs e)
        {
            // consoleWindow.Open();
            this.dockingManager1.SetDockVisibility(this.consoleWindow, true);
           
        }

        private void installScriptPackButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "(Test script installer)|*.tsi";
            DialogResult rslt = ofd.ShowDialog();
            if (rslt == DialogResult.OK)
            {
                InstallScriptPack(ofd.FileName);
            }
        }

        private void controlAOsMenuItem_Activate(object sender, EventArgs e)
        {
            this.mainRibbon.BackStageView.HideBackStage();
            // If the device is not connected yet, try to automatically connect
            if (!_usbDev_GearShift.IsConnected)
            {
                AddConsoleLog("Trying to connect automatically...");
                try
                {
                    _usbDev_GearShift.Connect();
                }
                catch (System.Exception ex)
                {
                    AddConsoleLog("Error while connecting: " + ex.Message);
                    AddConsoleLog("Cannot control the AOs if the device is not connected.");
                    return;
                }
            }
            if (!_usbDev_GearShift.IsConnected)
            {
                // If not, display the error and quit the function
                AddConsoleLog("Cannot control the AOs if the device is not connected.");
                return;
            }
            // If the measurement is not stopped it is not possible to start the AOs control
            if (mMeasurement.MsrmntState != Measurement.MeasurementState.Stopped)
            {
                AddConsoleLog("Cannot control the AOs if any test is running. Please stop the running test and try again");
                return;
            }
            AOsPanel aop = new AOsPanel(this.mMeasurement);
            aop.ShowDialog();

        }

        private void reportExplorer_TestScriptChosen(TestScriptReport ts)
        {
            this.mainRibbon.BackStageView.HideBackStage();
            ShowReportViewerDocument();
            resultsViewer.TestScriptReport = ts;
        }

        private void showCurrentsMenuItem_Activate(object sender, EventArgs e)
        {
            this.mainRibbon.BackStageView.HideBackStage();
            ShowCurrentsDocument();
        }

        private void showPressuresMenuItem_Activate(object sender, EventArgs e)
        {
            this.mainRibbon.BackStageView.HideBackStage();
            ShowPressuresDocument();
        }

        private void showConsoleMenuItem_Activate(object sender, EventArgs e)
        {
            //consoleWindow.Open();
            this.mainRibbon.BackStageView.HideBackStage();
            this.dockingManager1.SetDockVisibility(this.consoleWindow, true);

        }

        private void systemOptionsMenuItem_Activate(object sender, EventArgs e)
        {
            // settingsDocument.Open();
            this.mainRibbon.BackStageView.HideBackStage();
            ShowSettingsDocument();
        }

        private void firmwareMenuItem_Activate(object sender, EventArgs e)
        {
            this.mainRibbon.BackStageView.HideBackStage();
            GST.Gearshift.Components.Forms.FirmwareUpdateForm fwuf = new FirmwareUpdateForm();
            if (_usbDev_GearShift.IsConnected)
            {
                _usbDev_GearShift.Disconnect();
                System.Threading.Thread.Sleep(1000);
            }
            fwuf.ShowDialog();
        }

        private void ShowPrompt(object sender, EventArgs e)
        {
            this.BeginInvoke(new ShowPromptDG(ShowPromptInBg), new object[2] { (string)sender, mMeasurement });
        }

        private void ChangelogMainMenuItem_Activate(object sender, EventArgs e)
        {
            this.mainRibbon.BackStageView.HideBackStage();
            System.Diagnostics.Process.Start("CHANGELOG.txt");

        }

        private void showControlPanelMainMenuItem_Activate(object sender, EventArgs e)
        {
            this.mainRibbon.BackStageView.HideBackStage();
            switch (mMeasurement.MsrmntState)
            {
                case Measurement.MeasurementState.RunningAutomaticTest:
                case Measurement.MeasurementState.RunningInitialTest:
                case Measurement.MeasurementState.RunningLoopTest:
                case Measurement.MeasurementState.RunningManualTest:
                    {
                        // DockableWindowControlPanel.Open();
                        this.dockingManager1.SetDockVisibility(this.panel5, true);
                        break;
                    }
                default:
                    {
                        Soko.Common.Forms.MessageBox.ShowInfo("GearShift",
                        "Action not available",
                        "Control panel restoration can be used only when the test is running",
                        Soko.Common.Forms.MessageBoxButtons.OK);
                        break;
                    }
            }
            resetState();
        }

        /// <summary>
        /// On connect button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonConnect_Activate(object sender, EventArgs e)
        {
            // If the device is not connected yet, try to automatically connect
            if (!_usbDev_GearShift.IsConnected)
            {
                AddConsoleLog("Trying to connect automatically...");
                try
                {
                    _usbDev_GearShift.Connect();
                }
                catch (System.Exception ex)
                {
                    AddConsoleLog("Error while connecting: " + ex.Message);
                    return;
                }
            }
            else
            {
                AddConsoleLog("Disconnecting...");
                try
                {
                    _usbDev_GearShift.Disconnect();
                    // this.mainRibbon.Header.QuickItems.RemoveAt(0);
                }
                catch (System.Exception ex)
                {
                    AddConsoleLog("Error while disconnecting: " + ex.Message);
                }
            }
            //if (_usbDev_GearShift.IsConnected)
            //{
            //    AddConsoleLog("Disconnecting...");
            //    try
            //    {
            //        _usbDev_GearShift.Disconnect();
            //       // this.mainRibbon.Header.QuickItems.RemoveAt(0);
            //    }
            //    catch (System.Exception ex)
            //    {
            //        AddConsoleLog("Error while disconnecting: " + ex.Message);
            //    }
            //}
            //else
            //{
            //    AddConsoleLog("Trying to connect...");
            //    try
            //    {
            //        _usbDev_GearShift.Connect();
            //       // this.mainRibbon.Header.QuickItems.RemoveAt(0);

            //    }
            //    catch (System.Exception ex)
            //    {
            //        AddConsoleLog("Error while connecting: " + ex.Message);
            //    }
            //}
        }


        #endregion Menu and general UI

        #region DOCUMENTS SHOW/HIDE METHODS

        /// <summary>
        /// Hides all tabbed documents.
        /// </summary>
        private void HideAllDocuments()
        {
            HidePressuresDocument();
            HideCurrentsDocument();
            HideInitialTestScriptDoc();
            HideCanToolDocument();
            HideObdIIToolDocument();
            HideReportViewerDocument();
            reportExplorerDocument.Close();           
            scriptInstallerDocument.Close();
            settingsDocument.Close();
            //livePreviewDocument.Close();
            Gm6T40BarePanelDocument.Close();
        }

        /// <summary>
        /// Hides the tabbed document with CAN tool
        /// </summary>
        private void HideCanToolDocument()
        {
            CANTransControllerDocument.Close();
        }

        /// <summary>
        /// Hides the tabbed document with current displays
        /// </summary>
        private void HideCurrentsDocument()
        {
            //document OnClose action must be set to HideOnly
            //currentDisplaysDocument.Close();
            this.dockingManager1.SetDockVisibility(this.currentsDocPanel, false);
        }

        /// <summary>
        /// Hides the tabbed document with initial test script
        /// </summary>
        private void HideInitialTestScriptDoc()
        {
            //document OnClose action must be set to HideOnly
            initialTestDocument.Close();
        }

        /// <summary>
        /// Hides the tabbed document with CAN tool
        /// </summary>
        private void HideObdIIToolDocument()
        {
           // obdIIToolDocument.Close();
        }

        /// <summary>
        /// Hides the tabbed document with pressure displays
        /// </summary>
        private void HidePressuresDocument()
        {
            //document OnClose action must be set to HideOnly
            //pressureDisplaysDocument.Close();
            this.dockingManager1.SetDockVisibility(this.gaugesDocPanel, false);
        }

        /// <summary>
        /// Hides the tabbed document with report explorer
        /// </summary>
        private void HideReportExplorerDocument()
        {
            reportExplorerDocument.Close();
        }

        /// <summary>
        /// Hides the tabbed document with report viewer
        /// </summary>
        private void HideReportViewerDocument()
        {
            reportViewDocument.Close();
        }



        /// <summary>
        /// Shows the tabbed document with CAN tool
        /// </summary>
        private void ShowCanToolDocument()
        {
            //  CANTransControllerDocument.Open();
            if (!this.tabControlDocs.SelectedTab.Name.Equals("CANTransControllerDocument"))
            {


                this.tabControlDocs.SelectedTab = this.CANTransControllerDocument;
                if (!this.tabControlDocs.SelectedTab.Name.Equals("CANTransControllerDocument"))
                {

                    tabControlDocs.TabPages.Add(this.CANTransControllerDocument);
                    this.tabControlDocs.SelectedTab = this.CANTransControllerDocument;
                }
            }
        }
        private void ShowGm6T40BarePanelDocument()
        {
           
            if (!this.tabControlDocs.SelectedTab.Name.Equals("Gm6T40BarePanelDocument"))
            {


                this.tabControlDocs.SelectedTab = this.Gm6T40BarePanelDocument;
                if (!this.tabControlDocs.SelectedTab.Name.Equals("Gm6T40BarePanelDocument"))
                {

                    tabControlDocs.TabPages.Add(this.Gm6T40BarePanelDocument);
                    this.tabControlDocs.SelectedTab = this.Gm6T40BarePanelDocument;
                }
            }
        }
        /// <summary>
        /// Shows the tabbed document with current displays
        /// </summary>
        private void ShowCurrentsDocument()
        {
            // currentDisplaysDocument.Open();
            //this.dockingManager1.SetDockVisibility(currentDisplaysDocument, true);
            // this.panel6.Controls.Add(currentDisplaysDocument);
            //this.dockingManager1.SetDockVisibility(this.panel6, true);

            //this.dockingManager1.DockControl(this.panel7, panel6, DockingStyle.Tabbed, 1336);
            //this.dockingManager1.DockControl(this.panel8, panel6, DockingStyle.Tabbed, 1336);
            this.dockingManager1.DockAsDocument(this.currentsDocPanel);
            //this.dockingManager1.DockAsDocument(this.panel7);
            //this.dockingManager1.DockAsDocument(this.panel8);

            //this.panel6.PerformLayout();

            // this.dockingManager1.DockControl(this.currentDisplaysDocument, this, DockingStyle.Left,1536);
            //if (!this.tabControlDocs.SelectedTab.Name.Equals("currentDisplaysDocument"))
            //{





            //    this.tabControlDocs.SelectedTab = this.currentDisplaysDocument;
            //    if (!this.tabControlDocs.SelectedTab.Name.Equals("currentDisplaysDocument"))
            //    {

            //        tabControlDocs.TabPages.Add(this.currentDisplaysDocument);
            //        this.tabControlDocs.SelectedTab = this.currentDisplaysDocument;
            //    }
            //}
        }

        /// <summary>
        /// Shows the tabbed document with initial test script
        /// </summary>
        private void ShowInitialTestScriptDoc()
        {
            // initialTestDocument.Open();
            if (!this.tabControlDocs.SelectedTab.Name.Equals("initialTestDocument"))
            {


                this.tabControlDocs.SelectedTab = this.initialTestDocument;
                if (!this.tabControlDocs.SelectedTab.Name.Equals("initialTestDocument"))
                {

                    tabControlDocs.TabPages.Add(this.initialTestDocument);
                    this.tabControlDocs.SelectedTab = this.initialTestDocument;
                }
            }
        }

        /// <summary>
        /// Shows the tabbed document with OBD tool
        /// </summary>
        private void ShowObdIIToolDocument()
        {
            //obdIIToolDocument.Open();
        }

        /// <summary>
        /// Shows the tabbed document with pressure displays
        /// </summary>
        private void ShowPressuresDocument()
        {
            this.dockingManager1.DockAsDocument(this.gaugesDocPanel);
            // pressureDisplaysDocument.Open();
            //this.dockingManager1.DockAsDocument(this.pressureDisplaysDocument);
            //this.dockingManager1.SetDockVisibility()
            //if (!this.tabControlDocs.SelectedTab.Name.Equals("pressureDisplaysDocument"))
            //{


            //    this.tabControlDocs.SelectedTab = this.pressureDisplaysDocument;
            //    if (!this.tabControlDocs.SelectedTab.Name.Equals("pressureDisplaysDocument"))
            //    {

            //        tabControlDocs.TabPages.Add(this.pressureDisplaysDocument);
            //        this.tabControlDocs.SelectedTab = this.pressureDisplaysDocument;
            //    }
            //}
        }

        /// <summary>
        /// Shows the tabbed document with report explorer
        /// </summary>
        private void ShowReportExplorerDocument()
        {
            // reportExplorerDocument.Open();
            if (!this.tabControlDocs.SelectedTab.Name.Equals("reportExplorerDocument"))
            {


                this.tabControlDocs.SelectedTab = this.reportExplorerDocument;
                if (!this.tabControlDocs.SelectedTab.Name.Equals("reportExplorerDocument"))
                {

                    tabControlDocs.TabPages.Add(this.reportExplorerDocument);
                    this.tabControlDocs.SelectedTab = this.reportExplorerDocument;
                }
            }

        }

        /// <summary>
        /// Shows the tabbed document with report viewer
        /// </summary>
        private void ShowReportViewerDocument()
        {
            // reportViewDocument.Open();
            if (!this.tabControlDocs.SelectedTab.Name.Equals("reportViewDocument"))
            {


                this.tabControlDocs.SelectedTab = this.reportViewDocument;
                if (!this.tabControlDocs.SelectedTab.Name.Equals("reportViewDocument"))
                {

                    tabControlDocs.TabPages.Add(this.reportViewDocument);
                    this.tabControlDocs.SelectedTab = this.reportViewDocument;
                }
            }
        }
        /// <summary>
        /// Shows the tabbed document with settings Document 
        /// </summary>
        private void ShowSettingsDocument()
        {
            if (!this.tabControlDocs.SelectedTab.Name.Equals("settingsDocument"))
            {


                this.tabControlDocs.SelectedTab = this.settingsDocument;
                if (!this.tabControlDocs.SelectedTab.Name.Equals("settingsDocument"))
                {

                    tabControlDocs.TabPages.Add(this.settingsDocument);
                    this.tabControlDocs.SelectedTab = this.settingsDocument;
                }
            }
        }

        #endregion DOCUMENTS SHOW/HIDE METHODS

        #region USB devices events handling

        // GearShift USB events

        void _usbDev_GearShift_DeviceDisconnecedEvent(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(GearShiftUSBDisconnected));
            }
            else
            {
                GearShiftUSBDisconnected();
            }
        }

        void _usbDev_GearShift_DeviceConnectedEvent(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(GearShiftUSBConnected));
            }
            else
            {
                GearShiftUSBConnected();
            }
        }

        private void GearShiftUSBConnected()
        {
            SuspendLayout();
            this.mainRibbon.Header.QuickItems.RemoveAt(0);
            controlsUpdateTimer.Enabled = false;
            buttonConnect.Text = "Disconnect";
            buttonConnect.Image = global::GearShift.Properties.Resources.UsbLogo_Green_40x20;
            this.mainRibbon.Header.AddQuickItem(new Syncfusion.Windows.Forms.Tools.QuickButtonReflectable(buttonConnect));
            EZS_ConnectionStatusLabel.Image = Properties.Resources.DeviceStatusConnected_16x16;
            EZS_ConnectionStatusLabel.Text = "      GearShift connected    ";
            AddConsoleLog("GearShift connected on USB!");
            //AddConsolePreviewText("  GearShift connected on USB!  ");
            ResumeLayout();
        }

        private void GearShiftUSBDisconnected()
        {
            SuspendLayout();
            this.mainRibbon.Header.QuickItems.RemoveAt(0);
            controlsUpdateTimer.Enabled = false;
            buttonConnect.Text = "Connect";
            buttonConnect.Image = Properties.Resources.UsbLogo_Red_40x20;
            this.mainRibbon.Header.AddQuickItem(new Syncfusion.Windows.Forms.Tools.QuickButtonReflectable(buttonConnect));
            EZS_ConnectionStatusLabel.Image = Properties.Resources.DeviceStatuDisconnected_16x16;
            EZS_ConnectionStatusLabel.Text = "      GearShift disconnected";
            AddConsoleLog("GearShift discconnected from USB!");
            //AddConsolePreviewText("  GearShift discconnected from USB!  ");
            ResumeLayout();
        }

        // Zf6 USB events

        void _zf6USB_Connected(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(Zf6USBConnected));
            }
            else
            {
                Zf6USBConnected();
            }
        }

        void _zf6USB_Disconnected(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(Zf6USBDisconnected));
            }
            else
            {
                Zf6USBDisconnected();
            }
        }

        private void Zf6USBConnected()
        {
            Zf6_ConnectionStatusLabel.Image = Properties.Resources.DeviceStatusConnected_16x16;
            Zf6_ConnectionStatusLabel.Text = "      Decoder connected    ";
            AddConsoleLog("Decoder connected on USB!");
            //AddConsolePreviewText("  Decoder connected on USB!  ");
        }

        private void Zf6USBDisconnected()
        {
            SuspendLayout();
            Zf6_ConnectionStatusLabel.Image = Properties.Resources.DeviceStatuDisconnected_16x16;
            Zf6_ConnectionStatusLabel.Text = "      Decoder disconnected";
            AddConsoleLog("Decoder discconnected from USB!");
           // AddConsolePreviewText("  Decoder discconnected from USB!  ");
            ResumeLayout();
        }

        // CANPRO USB events

        void _CanProUSB_Connected(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(CanProUSBConnected));
            }
            else
            {
                CanProUSBConnected();
            }
            //throw new NotImplementedException();
        }
        void _CanProUSB_Disconnected(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(CanProUSBDisconnected));
            }
            else
            {
                CanProUSBDisconnected();
            }
            //throw new NotImplementedException();
        }
        void CanProUSBConnected()
        {
            CANcave_ConnectionStatusLabel.Image = Properties.Resources.DeviceStatusConnected_16x16;
            CANcave_ConnectionStatusLabel.Text = "      CAN connected    ";
            AddConsoleLog("CAN communicator connected on USB!");
            //AddConsolePreviewText("CAN communicator connected on USB!");
        }
        void CanProUSBDisconnected()
        {
            SuspendLayout();
            CANcave_ConnectionStatusLabel.Image = Properties.Resources.DeviceStatuDisconnected_16x16;
            CANcave_ConnectionStatusLabel.Text = "      CAN disconnected";
            AddConsoleLog("CAN communicator discconnected from USB!");
           // AddConsolePreviewText("CAN communicator discconnected from USB!");
            ResumeLayout();

        }

        #endregion USB devices events handling

        #region Test running events handling & methods

        /// <summary>
        /// On main measurement started
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMeasurementStartedEH(object sender, EventArgs e)
        {
            //sandDockManager1.SetLayout(mTestScript.SerializedScreenLayout);
            //DockableWindowControlPanel.Show();
            //this.panel.Controls.Remove(this.tabControlDocs);
            //this.Controls.Remove(this.tabControlDocs);

            //this.dockingManager1.EnableDocumentMode = true;
            //this.tabControlDocs.c;


            //this.panel.Controls.Add(this.panel6);


            //this.dockingManager1.EnableDocumentMode = true;




            //this.dockingManager1.SetDockVisibility(this.panel6, true);
            //this.dockingManager1.SetDockVisibility(this.panel7, true);
            //this.dockingManager1.SetDockVisibility(this.panel8, true);
            //this.dockingManager1.DockAsDocument(this.gaugesDocPanel);
            //this.dockingManager1.DockAsDocument(this.currentsDocPanel);

            ShowPressuresDocument();
            this.dockingManager1.DockAsDocument(this.livePreviewDocPanel);
            ShowCurrentsDocument();
            //resetState();
            LoadState();

            this.dockingManager1.SetDockVisibility(this.panel5, true);

            //this.dockingManager1.SetDockVisibility(panel, false);
            //this.dockingManager1.EnableDocumentMode = true;

            //this.dockingManager1.SetDockVisibility(this.panel6, true);
            //this.tabControlDocs.Hide();



            controlsUpdateTimer.Enabled = true;
            livePreviewPanel3.StartPlayback();
            AddConsoleLog("Main measurement started");
            
            //LoadState();
        }

        /// <summary>
        /// On  main measurement stopped
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMeasurementStoppedEH(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                EventHandler dg = new EventHandler(MainMeasurementStoppedEH);
                BeginInvoke(dg, new object[] { sender, e });
            }
            else
            {
                Console.WriteLine("MTS");
                SaveScreenLayout();
                
                //testControlPanel.DisableSliders();
                
                SaveState();
                this.dockingManager1.SetDockVisibility(this.panel5, false);

                
                //this.dockingManager1.DragProviderStyle = DragProviderStyle.VS2012;
                //this.homeTab.BorderStyle = BorderStyle.Fixed3D;
                //this.Controls.Add(this.panel);
               // this.panel.Controls.Add(this.tabControlDocs);

                //this.tabControlDocs.Dock = DockStyle.Fill;
                //this.tabControlDocs.ResumeLayout(false);
                //this.tabControlDocs.PerformLayout();
                //this.dockingManager1.SetDockVisibility(this.panel, true);
                controlsUpdateTimer.Enabled = false;
                HidePressuresDocument();
                this.dockingManager1.SetDockVisibility(this.livePreviewDocPanel, false);
                HideCurrentsDocument();
                //  DockableWindowControlPanel.Close();
                //livePreviewDocument.Close();
                //this.dockingManager1.SetDockVisibility(this.livePreviewDocument, false);
                
                //this.dockingManager1.EnableDocumentMode = false;
                //this.Controls.Remove(this.panel5);

                AddConsoleLog("Main measurement stopped");

                if (mMeasurement.GenerateReport)
                {
                    SaveTestResult();
                    if (mLastSavedReportPath != String.Empty)
                    {
                        ShowReportViewerDocument();
                        TestScriptReport newTestScriptReport = TestScriptReport.OpenFile(mLastSavedReportPath);
                        resultsViewer.TestScriptReport = newTestScriptReport;
                    }
                }
            }
            count++;
        }

        /// <summary>
        /// On initial test started
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InitialTestStartedEH(object sender, EventArgs e)
        {
            controlsUpdateTimer.Enabled = true;
            //this.dockingManager1.SetDockVisibility(this.panel5, true);
            //this.Controls.Add(this.panel5);
            AddConsoleLog("Initial test started");
        }

        /// <summary>
        /// On  initial test stopped
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InitialTestStoppedEH(object sender, EventArgs e)
        {
            Console.WriteLine("ITS");
            if (InvokeRequired)
            {
                EventHandler dg = new EventHandler(InitialTestStoppedEH);
                BeginInvoke(dg, new object[] { sender, e });
            }
            else
            {
                controlsUpdateTimer.Enabled = false;
                HideInitialTestScriptDoc();
                HidePressuresDocument();
                HideCurrentsDocument();
                // DockableWindowControlPanel.Close();

                this.dockingManager1.SetDockVisibility(this.panel5, false);

                
                AddConsoleLog("Initial test finished!");
            }
        }

        void mMeasurement_GearSwitchedEvent(TestScriptFrame frm)
        {
            UpdateGaugesRanges(frm);
        }

        private delegate void ShowPromptDG(string message, Measurement measurement);

        private void homeScreenPanel1_StartTestButtonClicked(object sender, EventArgs e)
        {
            // make sure the device is not running any test now
            if (mMeasurement.IsRunning || mMeasurement.IsRunningInitialTest)
            {
                AddConsoleLog("Cannot change the current test script while test is running!!\nPlease stop the test first.");
            }
            else
            {
                // If the device is not connected yet, try to automatically connect
                if (!_usbDev_GearShift.IsConnected)
                {
                    AddConsoleLog("Trying to connect automatically...");
                    try
                    {
                        _usbDev_GearShift.Connect();
                    }
                    catch (System.Exception ex)
                    {
                        AddConsoleLog("Error while connecting: " + ex.Message);
                        return;
                    }
                }
                // Check if the GearShift is properly connected
                if (!_usbDev_GearShift.IsConnected)
                {
                    // If not, display the error and quit the function
                    AddConsoleLog("CANNOT START THE TEST, please connect to the device first!");
                    return;
                }

                // Open the test script and check if Zf6 is required in the system
                TestScript tsc = TestScript.OpenXml((string)sender);

                /// commented out becuase it prevents futrther tests

                //switch (mMeasurement.Report.TestScriptRunned.Gearbox.ControllerType)
                //{
                //    case GearboxControllerType.ZF_6HPxx_1911E:
                //    case GearboxControllerType.ZF_6HPxx_1911M:
                //    case GearboxControllerType.ZF_6HPxx_CE:
                //    case GearboxControllerType.ZF_6HPxx_CM:
                //    case GearboxControllerType.ZF_6HPxx_TUCE:
                //    case GearboxControllerType.ZF_6HPxx_TUCM:
                //    case GearboxControllerType.ZF_6HPxx_WM:
                //    case GearboxControllerType.NISSAN_RE5:
                //        {
                //            // If this is zf6 controlled gearbox or Nissan RE5 gearbox
                //            AddConsoleLog("CANNOT START THE TEST, Zf6 not present in the system!");
                //            // Quit the function
                //            return;
                //        }
                //}

                // If all prerequisites are met, start the test
                mTestScript = tsc;
                mReport = new TestScriptReport();
                mReport.TestScriptRunned = mTestScript;
                mMeasurement.Report = mReport;
                livePreviewPanel3.Report = mReport;
                CurrentGearbox = mTestScript.Gearbox;
                EnableDAQMode();
                testControlPanel.ClearObsoleteData();
                ShowInitialTestScriptDoc();
                initialTest.StartInitialTest(mReport);
            }
        }

        private void initialTest_AbortButtonClickedEvent(object sender, EventArgs e)
        {
            HideInitialTestScriptDoc();
        }

        private void DAQ_OvercurrentDetected(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                EventHandler dg = new EventHandler(DAQ_OvercurrentDetected);
                BeginInvoke(dg, new object[] { sender, e });
            }
            else
            {
                // Stop the test and disconnect the device
                mMeasurement.StopMeasurement();
                _usbDev_GearShift.Disconnect();
                string message = "Overcurrent has been detected on following channels: \n";
                // Search through all channels
                for (int i = 0; i < 9; i++)
                {
                    // If there's overcurrent
                    if (_usbDev_GearShift.DAQGetOvercurrentStatusAtIndex(i))
                    {
                        message += "Channel " + (i + 1).ToString() + "  ";
                    }
                }
                message += "\nPlease resolve the problem and repeat the test.";
                Soko.Common.Forms.MessageBox msgbx = new Soko.Common.Forms.MessageBox("GearShift", "Overcurrent failure", message);
                msgbx.RemoveButtons();
                msgbx.AddButton(DialogResult.OK, "OK");
                //msgbx.ButtonsAligment = HorizontalAlignment.Center;
                msgbx.MessageBoxIcon = Soko.Common.Forms.MessageBoxIcon.Error;
                msgbx.ShowDialog();
            }
        }

        private void SaveTestResult()
        {
            try
            {
                // Do not add extension at the end of the file name, it will be added/overriden in Report.SaveToFile() method.
                mReport.TestScriptRunned = mTestScript;
                string dirToSave = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\GearShift Technologies\\GearShift\\Reports";
                string pathToSave = dirToSave + "\\" + mReport.TimePerformed.ToString("yyyy.MM.dd_HH.mm.ss ") + mReport.OperatorName + mReport.TestScriptRunned.Name;
                if (!Directory.Exists(dirToSave))
                {
                    Directory.CreateDirectory(dirToSave);
                }
                mReport.SaveToFile(pathToSave);
                // Get the path from the mReport
                mLastSavedReportPath = mReport.Filename;
                AddConsoleLog("This test results have been automatically saved as: " + Path.GetFileNameWithoutExtension(pathToSave));
            }
            catch (Exception e)
            {
                AddConsoleLog("An error occurred during the report saving process. Data might be corrupted :");
                AddConsoleLog("     " + e.Message);
                mLastSavedReportPath = String.Empty;
            }
        }

        #endregion Test running events handling & methods

        #region Controls init and refreshing

        /// <summary>
        /// Initializes controls to the default state
        /// </summary>
        private void InitializeControls()
        {
            //consoleWindow.Close();

            CurrentGearbox = new GearboxConfig();

            LoadAIDisplays(CurrentGearbox);
            LoadCurrentDisplays(CurrentGearbox);
        }

        /// <summary>
        /// Adds pressure gauges basing on specified GearboxConfig
        /// </summary>
        /// <param name="gbc"></param>
        private void LoadAIDisplays(GearboxConfig gbc)
        {
            //remove old controls
            gaugesPanel.Controls.Clear();
            // Flag to know if speeds and ratio gauges is to be added
            bool speedSensorsPresent = false;
            //add new gauge basing on GearboxConfig provided
            for (int i = 0; i < gbc._analogueInputs.Count; i++)
            {
                GearShiftUsb.AIChannel aic = gbc._analogueInputs[i];

                switch (aic.ValueType)
                {
                    case MeasurementUnit.ValueType.Pressure:
                    default:
                        {
                            // If this is a standard pressure input is to be added:
                            Soko.Common.Controls.AnalogueGauge ag = new Soko.Common.Controls.AnalogueGauge();
                            ag.BackColor = System.Drawing.Color.Transparent;
                            ag.Location = new System.Drawing.Point(122, 154);
                            ag.MaxValue = aic.MaxValueUserUnit;
                            ag.MinValue = aic.MinValueUserUnit;
                            ag.DigitalDigitCount = 5;
                            ag.DigitalPrecision = 2;
                            ag.ForeColor = System.Drawing.Color.Black;
                            ag.InnerColor = System.Drawing.Color.WhiteSmoke;
                            ag.MajorGrid = 5;
                            ag.MinorGrid = 10;
                            ag.TresholdMargin = 0;
                            ag.TresholdLeftErrorMargin = 0;
                            ag.TresholdRightErrorMargin = 0;
                            ag.TresholdLeftWarningMargin = 0;
                            ag.TresholdRightWarningMargin = 0;
                            ag.Size = new System.Drawing.Size(100, 100);
                            ag.MinimumSize = new System.Drawing.Size(100, 100);
                            ag.MaximumSize = new System.Drawing.Size(500, 500);
                            ag.AutoSize = true;
                            ag.TabIndex = 1;
                            ag.Value = ag.MinValue;
                            ag.Text = aic.Label;
                            ag.UnitName = aic.UnitText;
                            ag.Tag1 = aic;
                            ag.Tag2 = i;
                            gaugesPanel.Controls.Add(ag);
                            break;
                        }
                    case MeasurementUnit.ValueType.Flow:
                        {
                            Soko.Common.Controls.Gauges.LinearGauge fg = new Soko.Common.Controls.Gauges.LinearGauge();
                            fg.Anchor = System.Windows.Forms.AnchorStyles.Left;
                            fg.BackColor = System.Drawing.Color.Transparent;
                            fg.Font = new System.Drawing.Font("Calibri", 8F);
                            fg.Location = new System.Drawing.Point(200, 5);
                            fg.MaxValue = aic.MaxValueUserUnit;
                            fg.MinValue = aic.MinValueUserUnit;
                            fg.Name = "flowmometer";
                            fg.MaximumSize = new System.Drawing.Size(500, 500);
                            fg.MinimumSize = new System.Drawing.Size(100, 100);
                            fg.Size = new System.Drawing.Size(100, 100);
                            fg.AutoSize = true;
                            fg.TabIndex = 1;
                            fg.Text = aic.Label;
                            fg.Unit = aic.UnitText;
                            fg.Value = fg.MinValue;
                            fg.Tag1 = aic;
                            fg.Tag2 = i;
                            gaugesPanel.Controls.Add(fg);
                            break;
                        }
                    case MeasurementUnit.ValueType.Temperature:
                        {
                            Soko.Common.Controls.Gauges.ThermometerGauge tg = new Soko.Common.Controls.Gauges.ThermometerGauge();
                            tg.Anchor = System.Windows.Forms.AnchorStyles.Left;
                            tg.BackColor = System.Drawing.Color.Transparent;
                            tg.Font = new System.Drawing.Font("Calibri", 8F);
                            tg.Location = new System.Drawing.Point(200, 5);
                            tg.MaxValue = aic.MaxValueUserUnit;
                            tg.MinValue = aic.MinValueUserUnit;
                            tg.Name = "thermometer";
                            tg.MaximumSize = new System.Drawing.Size(500, 500);
                            tg.MinimumSize = new System.Drawing.Size(100, 100);
                            tg.Size = new System.Drawing.Size(100, 100);
                            tg.AutoSize = true;
                            tg.TabIndex = 1;
                            tg.Text = aic.Label;
                            tg.Unit = aic.UnitText;
                            tg.Value = tg.MinValue;
                            tg.Tag1 = aic;
                            tg.Tag2 = i;
                            gaugesPanel.Controls.Add(tg);
                            break;
                        }
                    case MeasurementUnit.ValueType.Torque:
                        {
                            Soko.Common.Controls.Gauges.LinearGauge tg = new Soko.Common.Controls.Gauges.LinearGauge();
                            tg.Anchor = System.Windows.Forms.AnchorStyles.Left;
                            tg.BackColor = System.Drawing.Color.Transparent;
                            tg.Font = new System.Drawing.Font("Calibri", 8F);
                            tg.Location = new System.Drawing.Point(200, 5);
                            tg.MaxValue = aic.MaxValueUserUnit;
                            tg.MinValue = aic.MinValueUserUnit;
                            tg.Name = "thermometer";
                            tg.MaximumSize = new System.Drawing.Size(500, 500);
                            tg.MinimumSize = new System.Drawing.Size(100, 100);
                            tg.Size = new System.Drawing.Size(100, 100);
                            tg.AutoSize = true;
                            tg.TabIndex = 1;
                            tg.Text = aic.Label;
                            tg.Unit = aic.UnitText;
                            tg.Value = tg.MinValue;
                            tg.Tag1 = aic;
                            tg.Tag2 = i;
                            gaugesPanel.Controls.Add(tg);
                            break;
                        }
                    case MeasurementUnit.ValueType.GearRatio:
                    case MeasurementUnit.ValueType.InputSpeed:
                        {
                            Soko.Common.Controls.AnalogueGauge ag = new Soko.Common.Controls.AnalogueGauge();
                            ag.BackColor = System.Drawing.Color.Transparent;
                            ag.Location = new System.Drawing.Point(200, 5);
                            ag.MaxValue = aic.MaxValueUserUnit;
                            ag.MinValue = aic.MinValueUserUnit;
                            ag.ForeColor = System.Drawing.Color.Black;
                            ag.InnerColor = System.Drawing.Color.WhiteSmoke;
                            ag.MajorGrid = 5;
                            ag.MinorGrid = 10;
                            ag.TresholdMargin = 0;
                            ag.TresholdLeftErrorMargin = 0;
                            ag.TresholdRightErrorMargin = 0;
                            ag.TresholdLeftWarningMargin = 0;
                            ag.TresholdRightWarningMargin = 0;
                            ag.Size = new System.Drawing.Size(100, 100);
                            ag.MinimumSize = new System.Drawing.Size(100, 100);
                            ag.MaximumSize = new System.Drawing.Size(500, 500);
                            ag.AutoSize = true;
                            ag.TabIndex = 1;
                            ag.Value = (int)Math.Round(ag.MinValue,0);
                            ag.Text = aic.Label;
                            ag.UnitName = aic.UnitText;                       
                            ag.Tag1 = aic;
                            ag.Tag2 = i;
                            gaugesPanel.Controls.Add(ag);
                            break;
                        }

                    case MeasurementUnit.ValueType.OutputSpeed:

                        {
                            speedSensorsPresent = true;
                            Soko.Common.Controls.AnalogueGauge ag = new Soko.Common.Controls.AnalogueGauge();
                            ag.BackColor = System.Drawing.Color.Transparent;
                            ag.Location = new System.Drawing.Point(200, 5);
                            ag.MaxValue = aic.MaxValueUserUnit;
                            ag.MinValue = aic.MinValueUserUnit;

                            ag.ForeColor = System.Drawing.Color.Black;
                            ag.InnerColor = System.Drawing.Color.WhiteSmoke;
                            ag.MajorGrid = 5;
                            ag.MinorGrid = 10;
                            ag.TresholdMargin = 0;
                            ag.TresholdLeftErrorMargin = 0;
                            ag.TresholdRightErrorMargin = 0;
                            ag.TresholdLeftWarningMargin = 0;
                            ag.TresholdRightWarningMargin = 0;
                            ag.Size = new System.Drawing.Size(100, 100);
                            ag.MinimumSize = new System.Drawing.Size(100, 100);
                            ag.MaximumSize = new System.Drawing.Size(500, 500);
                            ag.AutoSize = true;
                            ag.TabIndex = 1;
                            ag.Value = Math.Round(ag.MinValue, 0);
                            
                            ag.Text = aic.Label;
                            ag.UnitName = aic.UnitText;
                            ag.Tag1 = aic;
                            ag.Tag2 = i;
                            gaugesPanel.Controls.Add(ag);
                            break;
                        }

                    case MeasurementUnit.ValueType.PressureSwitch:
                        {
                            Soko.Common.Controls.NiceIndicator psg = new NiceIndicator();
                            psg.Anchor = System.Windows.Forms.AnchorStyles.Left;
                            psg.BackColor = System.Drawing.Color.Transparent;
                            psg.Font = new System.Drawing.Font("Calibri", 14F);
                            psg.Location = new System.Drawing.Point(200, 5);
                            psg.Name = "pressureSwitch";
                            psg.MaximumSize = new System.Drawing.Size(150, 150);
                            psg.MinimumSize = new System.Drawing.Size(100, 100);
                            psg.Size = new System.Drawing.Size(100, 100);
                            psg.AutoSize = true;
                            psg.TextImageRelation = NiceIndicator.TextRelation.Underneath;
                            psg.TextON = aic.Label;
                            psg.TextOFF = aic.Label;
                            psg.ImageON = Properties.Resources.TFP_ON_96x96;
                            psg.ImageOFF = Properties.Resources.TFP_OFF_96x96;
                            psg.IsOn = false;
                            psg.Tag1 = aic;
                            psg.Tag2 = i;
                            gaugesPanel.Controls.Add(psg);
                            break;
                        }
                }

            }

            // If present, add speeds gauge
            if (speedSensorsPresent)
            {
                //GST.Gearshift.Components.Controls.Gauges.SpeedsGauge spdg = new GST.Gearshift.Components.Controls.Gauges.SpeedsGauge();
                //spdg.Text = "";                
                //spdg.MaximumSize = new System.Drawing.Size(500, 500);
                //spdg.MinimumSize = new System.Drawing.Size(100, 100);
                //spdg.Size = new System.Drawing.Size(100, 100);
                //spdg.AutoSize = true;
                //gaugesPanel.Controls.Add(spdg);

            }
        }

        /// <summary>
        /// Adds current bars basing on specified GearboxConfig
        /// </summary>
        /// <param name="gearboxConfig"></param>
        private void LoadCurrentDisplays(GearboxConfig gbc)
        {
            currBarsPanel.Controls.Clear();
            switch (mMeasurement.Report.TestScriptRunned.Gearbox.ControllerType)
            {
                default:
                case GearboxControllerType.NON_MECHATRONIC:
                case GearboxControllerType.ZF_6HPxx_1911E:
                case GearboxControllerType.ZF_6HPxx_1911M:
                case GearboxControllerType.ZF_6HPxx_CE:
                case GearboxControllerType.ZF_6HPxx_CM:
                case GearboxControllerType.ZF_6HPxx_TUCE:
                case GearboxControllerType.ZF_6HPxx_TUCM:
                case GearboxControllerType.ZF_6HPxx_WM:
                case GearboxControllerType.NISSAN_RE5:
                    {
                        for (int i = 0; i < gbc.CurrentDisplayChannelsSet.ChannelsCount; i++)
                        {
                            switch (mMeasurement.Report.TestScriptRunned.Gearbox.ControllerType)
                            {
                                case GearboxControllerType.ZF_6HPxx_1911E:
                                case GearboxControllerType.ZF_6HPxx_1911M:
                                case GearboxControllerType.ZF_6HPxx_CE:
                                case GearboxControllerType.ZF_6HPxx_CM:
                                case GearboxControllerType.ZF_6HPxx_TUCE:
                                case GearboxControllerType.ZF_6HPxx_TUCM:
                                case GearboxControllerType.ZF_6HPxx_WM:
                                case GearboxControllerType.NISSAN_RE5:
                                    {
                                        // If this is zf6 controlled gearbox
                                        // If this is Nissan RE5 gearbox
                                        // Keep the max values at 1 amp
                                        gbc.CurrentDisplayChannelsSet.Channels[i].MaxValue = 1.0f;
                                        break;
                                    }
                            }

                            GST.Gearshift.Components.Controls.Gauges.SolenoidGauge sg = new GST.Gearshift.Components.Controls.Gauges.SolenoidGauge();
                            sg.MinimumSize = new Size(90, 250);
                            sg.MaximumSize = new Size(150, 350);
                            sg.AutoSize = true;
                            sg.Location = new System.Drawing.Point(142, 97);
                            sg.DispChannel = gbc.CurrentDisplayChannelsSet.Channels[i];
                            currBarsPanel.Controls.Add(sg);
                        }
                        currBarsPanel.AutoSizeElements = true;
                        break;
                    }
                case GearboxControllerType.GM6T40:
                case GearboxControllerType.GM6T70:
                case GearboxControllerType.GM6L:
                    {

                        Soko.CanCave.Components.Forms.GM6TxxLiveDataPanel ldp = new Soko.CanCave.Components.Forms.GM6TxxLiveDataPanel();
                        ldp.GM6TGovernor = _GM6TxxTcuGov;
                        ldp.ShowS2Solenoid = !(mMeasurement.Report.TestScriptRunned.Gearbox.ControllerType == GearboxControllerType.GM6T40);
                        currBarsPanel.Controls.Add(ldp);
                        currBarsPanel.AutoSizeElements = false;
                        ldp.Dock = DockStyle.Fill;
                        break;
                    }
            }
        }

        /// <summary>
        /// Displays pressure values on GUI controls
        /// </summary>
        /// <param name="gbc"></param>
        private void DisplayPressureValues()
        {

            foreach (Control ctrl in gaugesPanel.Controls)
            {
                if (ctrl is AnalogueGauge)
                {
                    AnalogueGauge ag = (AnalogueGauge)ctrl;
                    ag.Value = _usbDev_GearShift.GetLatestAIValueUserUnit((GearShiftUsb.AIChannel)ag.Tag1, (int)ag.Tag2);
                }
                if (ctrl is Soko.Common.Controls.Gauges.LinearGauge)
                {
                    Soko.Common.Controls.Gauges.LinearGauge lg = (Soko.Common.Controls.Gauges.LinearGauge)ctrl;
                    lg.Value = _usbDev_GearShift.GetLatestAIValueUserUnit((GearShiftUsb.AIChannel)lg.Tag1, (int)lg.Tag2);
                }
                if (ctrl is Soko.Common.Controls.Gauges.ThermometerGauge)
                {
                    Soko.Common.Controls.Gauges.ThermometerGauge tg = (Soko.Common.Controls.Gauges.ThermometerGauge)ctrl;
                    tg.Value = _usbDev_GearShift.GetLatestAIValueUserUnit((GearShiftUsb.AIChannel)tg.Tag1, (int)tg.Tag2);
                }
                if (ctrl is GST.Gearshift.Components.Controls.Gauges.SpeedsGauge)
                {
                    GST.Gearshift.Components.Controls.Gauges.SpeedsGauge sg = (GST.Gearshift.Components.Controls.Gauges.SpeedsGauge)ctrl;
                    ctrl.Invalidate();
                }
                if (ctrl is Soko.Common.Controls.NiceIndicator)
                {
                    Soko.Common.Controls.NiceIndicator psg = (Soko.Common.Controls.NiceIndicator)ctrl;
                    psg.IsOn = (_usbDev_GearShift.GetLatestAIValueUserUnit((GearShiftUsb.AIChannel)psg.Tag1, (int)psg.Tag2) < (((GearShiftUsb.AIChannel)psg.Tag1).MaxValueUserUnit / 4));
                }
            }

        }

        void UpdateGaugesRanges(TestScriptFrame frm)
        {
            if (frm.AcquireData)
            {
                SuspendLayout();

                foreach (Control ctrl in gaugesPanel.Controls)
                {
                    if (ctrl is AnalogueGauge)
                    {
                        AnalogueGauge ag = (AnalogueGauge)ctrl;
                        GearShiftUsb.AIChannel aic = (GearShiftUsb.AIChannel)ag.Tag1;
                        float expectedValue = MeasurementUnit.ConvertAIValueBaseUnitToUserUnit(frm.MasterPressureReadValues[(int)ag.Tag2], MeasurementUnit.ValueType.Pressure);
                        float errorMargin = expectedValue * CurrentGearbox.PressureVariationTolerance;
                        if (errorMargin < aic.MaxValueUserUnit / 10) errorMargin = aic.MaxValueUserUnit / 10;
                        ag.SetExpectedValueWithoutInvalidation(expectedValue, errorMargin / 2);
                    }
                }

                ResumeLayout();
            }
        }

        /// <summary>
        /// Displays current values on GUI controls
        /// </summary>
        /// <param name="gearboxConfig"></param>
        private void DisplayCurrentValues()
        {
            switch (mReport.TestScriptRunned.Gearbox.ControllerType)
            {
                default:
                    {
                        foreach (Control ctrl in currBarsPanel.Controls)
                        {
                            if (ctrl is GST.Gearshift.Components.Controls.Gauges.SolenoidGauge)
                            {
                                (ctrl as GST.Gearshift.Components.Controls.Gauges.SolenoidGauge).UpdateValue();
                            }
                        }
                        break;
                    }
                case GearboxControllerType.GM6T40:
                case GearboxControllerType.GM6T70:
                case GearboxControllerType.GM6L:
                    {
                        foreach (Control ctrl in currBarsPanel.Controls)
                        {
                            if (ctrl is Soko.CanCave.Components.Forms.GM6TxxLiveDataPanel)
                            {
                                Soko.CanCave.Components.Forms.GM6TxxLiveDataPanel ldp = (Soko.CanCave.Components.Forms.GM6TxxLiveDataPanel)ctrl;
                                ldp.RefreshDisplayedValues();
                            }
                        }
                        break;
                    }
            }
        }

        private void controlsUpdateTimer_Tick(object sender, EventArgs e)
        {
            DisplayPressureValues();
            _usbDev_GearShift.GetLatestCurrentValues(CurrentGearbox.CurrentDisplayChannelsSet);
            DisplayCurrentValues();
        }

        private void EnableDAQMode()
        {
            //DockableWindowControlPanel.Open();
            // DockableWindowControlPanel.Width = 200;

            //this.homeTab.ShowCloseButton = true;
            this.dockingManager1.SetDockVisibility(this.panel5, true);
            //this.panel5.Dock = DockStyle.Left;
        }

        //private void DisableDAQMode()
        //{
        //  DockableWindowControlPanel.Close();
        //}

        private void EnableCANMode()
        {
            //DockableWindowControlPanel.Open();
            //this.dockingManager1.SetDockVisibility(this.panel5, true);
            if (mMeasurement.IsRunning)
            {
                //
                return;
            }

            //DisableDAQMode();
            ShowCanToolDocument();
        }

        private void DisableCANMode()
        {
            HideCanToolDocument();
        }

        private void EnableOBDMode()
        {
            ShowObdIIToolDocument();
        }

        private void DisableOBDMode()
        {
            HideObdIIToolDocument();
        }

        #endregion Controls init and refreshing

        private void InstallScriptPack(string filename)
        {
            Soko.Common.Forms.MessageBox msgbx;
            try
            {
                using (Ionic.Zip.ZipFile zip = Ionic.Zip.ZipFile.Read(filename))
                {
                    zip.ExtractAll(GST.Gearshift.Components.Utilities.Settings.GearboxesDirectory, Ionic.Zip.ExtractExistingFileAction.OverwriteSilently);
                }
            }
            catch (Exception e)
            {
                msgbx = new Soko.Common.Forms.MessageBox("GearShift", "Script pack installation problem", "The script pack you are trying to install is corrupted, please contact the reseller" + e.Message);
                msgbx.RemoveButtons();
                msgbx.AddButton(DialogResult.OK, "OK");
                //msgbx.ButtonsAligment = HorizontalAlignment.Center;
                msgbx.MessageBoxIcon = Soko.Common.Forms.MessageBoxIcon.Information;
                msgbx.ShowDialog();
                return;
            }
            msgbx = new Soko.Common.Forms.MessageBox("GearShift", "Script pack installation success", "The script pack has been successfully installed");
            msgbx.RemoveButtons();
            msgbx.AddButton(DialogResult.OK, "OK");
            //msgbx.ButtonsAligment = HorizontalAlignment.Center;
            msgbx.MessageBoxIcon = Soko.Common.Forms.MessageBoxIcon.Information;
            msgbx.ShowDialog();
        }

        #endregion Methods & delegates

        private void gmLanTestEnvMenuItem_Activate(object sender, EventArgs e)
        {
            this.mainRibbon.BackStageView.HideBackStage();
            ShowGm6T40BarePanelDocument();
            
            
        }
        private void backStage1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (backStage1.SelectedTab == saveAsbackStageTab)
            //    this.splitContainerAdv1.SplitterDistance = this.treeNavigator2.Width;
        }
        private void closebackStageButton_Click(object sender, EventArgs e)
        {
            this.mainRibbon.BackStageView.HideBackStage();

        }

        private void ExitBtnMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void LoadState()
        {
            string mSettingsFilePath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\GearShift Technologies\\GearShift\\AppState";
            AppStateSerializer serializer = new AppStateSerializer(SerializeMode.XMLFile, mSettingsFilePath);
            this.dockingManager1.LoadDockState(serializer);
            AddConsoleLog("TabGroupStates loaded");
            AddConsoleLog("Serialized controls :" + this.dockingManager1.GetSerializedControls(serializer));
        }

        private void SaveState()
        {
            string mSettingsFilePath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\GearShift Technologies\\GearShift\\AppState";
            AppStateSerializer serializer = new AppStateSerializer(SerializeMode.XMLFile, mSettingsFilePath);
            this.dockingManager1.SaveDockState(serializer);
            serializer.PersistNow();
            AddConsoleLog("TabGroupStates saved");
            AddConsoleLog(("Serialized controls :" + this.dockingManager1.GetSerializedControls(serializer)));
        }
        private void resetState()
        {
            this.dockingManager1.LoadDesignerDockState();
            this.dockingManager1.SetDockVisibility(this.gaugesDocPanel, false);
            this.dockingManager1.SetDockVisibility(this.livePreviewDocPanel, false);
            this.dockingManager1.SetDockVisibility(this.currentsDocPanel, false);
        }
    }
}
