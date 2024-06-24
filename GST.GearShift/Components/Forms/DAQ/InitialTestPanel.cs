using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Timers;
using System.Windows.Forms;

using Soko.Common.Common;
using GST.Gearshift.Components.Interfaces.USB;

namespace GST.Gearshift.Components.Forms.DAQ
{
  public partial class ChannelsInitialTest : UserControl
  {


    #region Constants



    #endregion  Constants



    #region Private fields

    private DisplayChannelsSet mChannelsSet = null;

    private TestScriptReport mReport = new TestScriptReport();

    private Measurement mMeasurement = new Measurement();


    #endregion Private fields



    #region Constructors & finalizer

    public ChannelsInitialTest()
    {
      InitializeComponent();
      mChannelsSet = new DisplayChannelsSet();
      resultPane.Collapsed = true;
      promptPane.HidePane();
    }

    #endregion Constructors & finalizer

    #region Events



    #endregion Events


    #region Properties

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Measurement MeasurementSession
    {
      get { return mMeasurement; }
      set
      {
        mMeasurement = value;
        mChannelsSet = mMeasurement.Report.TestScriptRunned.Gearbox.CurrentDisplayChannelsSet;
        ManageCurrentDisplays();
      }
    }

    #endregion Properties

    #region Public Methods

    public void StartInitialTest(TestScriptReport report)
    {
      // Assign the report
      mReport = report;
      testNameTextBox.Text = mReport.TestScriptRunned.Name;
      // Refresh the channels set because it might have changed since the Measurement was initially assigned

      switch (mMeasurement.Report.TestScriptRunned.Gearbox.ControllerType)
      {
        default:
        case GearboxControllerType.NON_MECHATRONIC:
          {
            // If this is an analogue gearbox
            decoderInitPanel.Location = new Point(1000, 1000);
            nissanRE5InitPanel.Location = new Point(1000, 1000);
            gM6TxxInitPanel.Location = new Point(1000, 1000);

            testPaneLeftpanel.Location = new Point(5, 29);
            testPaneLeftpanel.Height = 490;
            testPaneRightPanel.Location = new Point(616, 29);
            testPaneRightPanel.Height = 490;
            mChannelsSet = mMeasurement.Report.TestScriptRunned.Gearbox.CurrentDisplayChannelsSet;
            ManageCurrentDisplays();
            break;
          }
        case GearboxControllerType.ZF_6HPxx_1911E:
        case GearboxControllerType.ZF_6HPxx_1911M:
        case GearboxControllerType.ZF_6HPxx_CE:
        case GearboxControllerType.ZF_6HPxx_CM:
        case GearboxControllerType.ZF_6HPxx_TUCE:
        case GearboxControllerType.ZF_6HPxx_TUCM:
        case GearboxControllerType.ZF_6HPxx_WM:
          {
            // If this is zf6 controlled gearbox
            testPaneLeftpanel.Location = new Point(1000, 1000);
            testPaneRightPanel.Location = new Point(1000, 1000);
            nissanRE5InitPanel.Location = new Point(1000, 1000);
            gM6TxxInitPanel.Location = new Point(1000, 1000);

            decoderInitPanel.Location = new Point(5, 29);
            decoderInitPanel.Size = new System.Drawing.Size(713, 490);
            decoderInitPanel.Zf6USB = mMeasurement.UsbDev_Decoder;
            decoderInitPanel.gearboxType = mMeasurement.Report.TestScriptRunned.Gearbox.ControllerType;
            break;
          }

        case GearboxControllerType.NISSAN_RE5:
          {
            // If this is Nissan RE5 gearbox
            testPaneLeftpanel.Location = new Point(1000, 1000);
            testPaneRightPanel.Location = new Point(1000, 1000);
            decoderInitPanel.Location = new Point(1000, 1000);
            gM6TxxInitPanel.Location = new Point(1000, 1000);

            nissanRE5InitPanel.Location = new Point(5, 29);
            nissanRE5InitPanel.Size = new System.Drawing.Size(713, 490);
            nissanRE5InitPanel.UsbDev_Decoder = mMeasurement.UsbDev_Decoder;

            break;
          }
        case GearboxControllerType.GM6T40:
        case GearboxControllerType.GM6T70:
        case GearboxControllerType.GM6L:
          {
            // If this is GM 6Txx / 6L gearbox
            testPaneLeftpanel.Location = new Point(1000, 1000);
            testPaneRightPanel.Location = new Point(1000, 1000);
            decoderInitPanel.Location = new Point(1000, 1000);
            nissanRE5InitPanel.Location = new Point(1000, 1000);

            gM6TxxInitPanel.Location = new Point(5, 29);
            gM6TxxInitPanel.Size = new System.Drawing.Size(713, 490);
            gM6TxxInitPanel.GM6TGovernor = mMeasurement.GM6TxxGovernor;

            break;
          }
      }

      // Collapse all the panes except the one with prompt form
      promptPane.ShowPane();
      testPane.HidePane();
      resultPane.HidePane();
      promptPane.ShowPane();
    }

    #endregion Public Methods

    #region Private Methods

    private void ManageCurrentDisplays()
    {
      testPaneLeftpanel.Controls.Clear();
      for (int i = 0; i < mChannelsSet.ChannelsCount; i++)
      {
        InitialTestDisplay itd = new InitialTestDisplay();
        itd.Channel = mChannelsSet.Channels[i];
        testPaneLeftpanel.Controls.Add(itd);
      }
      Invalidate(true);
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
      #region this must have been applied to support pseudo transparency
      if (e == null)
        return;
      if (e.Graphics == null)
        return;

      if (this.Parent != null)
      {
        GraphicsContainer cstate = e.Graphics.BeginContainer();
        e.Graphics.TranslateTransform(-this.Left, -this.Top);
        Rectangle clip = e.ClipRectangle;
        clip.Offset(this.Left, this.Top);
        PaintEventArgs pe = new PaintEventArgs(e.Graphics, clip);

        //paint the container's bg
        InvokePaintBackground(this.Parent, pe);
        //paints the container fg
        InvokePaint(this.Parent, pe);
        //restores graphics to its original state
        e.Graphics.EndContainer(cstate);
      }
      else
        base.OnPaintBackground(e);
      #endregion
    }

    private void promptPane_AbortTestButton_Click( object sender, EventArgs e )
    {
      // Clean the values for the next test to be runned
      operatorNameComboBox.Text = string.Empty;
      serialNoTextBox.Text = string.Empty;
      commentTextBox.Text = string.Empty;
      // Finish initial test even though it was not started yet.
      mMeasurement.StopInitialTest();
      // Hide all panes
      promptPane.HidePane();
      testPane.HidePane();
      resultPane.HidePane();
    }

    private void promptPane_operatorNameComboBox_DropDown(object sender, EventArgs e)
    {
      operatorNameComboBox.Items.Clear();
      operatorNameComboBox.Items.AddRange(GST.Gearshift.Components.Utilities.Settings.Instance.SystemOperators);
    }

    private void promptPane_ContinueInDevModeButton_Click(object sender, EventArgs e)
    {
      mReport.OperatorName = operatorNameComboBox.Text;
      mReport.GearboxSerialNumber = serialNoTextBox.Text;
      mReport.TimePerformed = DateTime.Now;
      mReport.Comment = commentTextBox.Text;
      mMeasurement.GenerateReport = false;
      if (mMeasurement.Device.IsConnected && !(mMeasurement.IsRunningInitialTest || mMeasurement.IsRunning))
      {
        mMeasurement.StartInitialTest();
      }
      else
      {
        // TODO: DISPLAY MESSAGE HERE!!!
      }
      operatorNameComboBox.Text = string.Empty;
      serialNoTextBox.Text = string.Empty;
      commentTextBox.Text = string.Empty;
      switch (mMeasurement.Report.TestScriptRunned.Gearbox.ControllerType)
      {
        default:
        case GearboxControllerType.NON_MECHATRONIC:
          {
            // If this is an analogue gearbox
            currentsRefreshTimer.Enabled = true;
            break;
          }
        case GearboxControllerType.ZF_6HPxx_1911E:
        case GearboxControllerType.ZF_6HPxx_1911M:
        case GearboxControllerType.ZF_6HPxx_CE:
        case GearboxControllerType.ZF_6HPxx_CM:
        case GearboxControllerType.ZF_6HPxx_TUCE:
        case GearboxControllerType.ZF_6HPxx_TUCM:
        case GearboxControllerType.ZF_6HPxx_WM:
          {
            // If this is zf6 controlled gearbox
            decoderInitPanel.Start();
            break;
          }

        case GearboxControllerType.NISSAN_RE5:
          {
            // If this is Nissan RE5 gearbox
            nissanRE5InitPanel.Start();
            break;
          }

        case GearboxControllerType.GM6T40:
        case GearboxControllerType.GM6T70:
        case GearboxControllerType.GM6L:
          {
            // If this is GM 6Txx gearbox
            gM6TxxInitPanel.Start();
            break;
          }
      }
      promptPane.HidePane();
      testPane.ShowPane();
      resultPane.HidePane();
    }

    private void promptPane_ContinueInReportModeButton_Click(object sender, EventArgs e)
    {

      mReport.OperatorName = operatorNameComboBox.Text;
      mReport.GearboxSerialNumber = serialNoTextBox.Text;
      mReport.TimePerformed = DateTime.Now;
      mReport.Comment = commentTextBox.Text;
      mMeasurement.GenerateReport = true;
      if (mMeasurement.Device.IsConnected && !(mMeasurement.IsRunningInitialTest || mMeasurement.IsRunning))
      {
        mMeasurement.StartInitialTest();
      }
      else
      {
        // TODO: DISPLAY MESSAGE HERE!!!
      }
      operatorNameComboBox.Text = string.Empty;
      serialNoTextBox.Text = string.Empty;
      commentTextBox.Text = string.Empty;

      switch (mMeasurement.Report.TestScriptRunned.Gearbox.ControllerType)
      {
        default:
        case GearboxControllerType.NON_MECHATRONIC:
          {
            // If this is an analogue gearbox
            currentsRefreshTimer.Enabled = true;
            break;
          }
        case GearboxControllerType.ZF_6HPxx_1911E:
        case GearboxControllerType.ZF_6HPxx_1911M:
        case GearboxControllerType.ZF_6HPxx_CE:
        case GearboxControllerType.ZF_6HPxx_CM:
        case GearboxControllerType.ZF_6HPxx_TUCE:
        case GearboxControllerType.ZF_6HPxx_TUCM:
        case GearboxControllerType.ZF_6HPxx_WM:
          {
            // If this is zf6 controlled gearbox
            decoderInitPanel.Start();
            break;
          }

        case GearboxControllerType.NISSAN_RE5:
          {
            // If this is Nissan RE5 gearbox
            nissanRE5InitPanel.Start();
            break;
          }
        case GearboxControllerType.GM6T40:
        case GearboxControllerType.GM6T70:
        case GearboxControllerType.GM6L:
          {
            // If this is GM 6Txx gearbox
            gM6TxxInitPanel.Start();
            break;
          }
      }
      promptPane.HidePane();
      testPane.ShowPane();
      resultPane.HidePane();
    }

    private void ProceedToFinalScreen(object sender, EventArgs e)
    {
      // Disable the GUI refreshing
      currentsRefreshTimer.Enabled = false;
      // Find out which channels have problems and display proper messages
      List<string> failedChannels = new List<string>();
      foreach (Control ctrl in testPaneLeftpanel.Controls)
      {
        if (ctrl is InitialTestDisplay)
        {
          if (((InitialTestDisplay)ctrl).TestFailed)
          {
            failedChannels.Add(((InitialTestDisplay)ctrl).Channel.Label);
          }
        }
      }

      if (failedChannels.Count == 0) // if test passed
      {
        testPassLabel.Text = "INITIAL TEST PASSED";
        infoLabel1.Text = "You can safely continue the test. Choose what to do next:\n";
      }
      else // if test failed
      {
        testPassLabel.Text = "INITIAL TEST FAILED";
        infoLabel1.Text = "There were problems detected on the following channels:\n\n";
        foreach (string txt in failedChannels)
        {
          infoLabel1.Text += (txt + "  ");
        }
        infoLabel1.Text += "\n\nIt is highly recommended that you first solve the issues before proceeding";
      }

      // Disable all drivers as the user will need to choose which way to go
      // The initial test is not ended here on purpose
      mMeasurement.DisableAllDrivers();

      mReport.TestScriptRunned.SaveXml(mReport.TestScriptRunned.Filename);

      //ResumeLayout();
      testPane.HidePane();
      resultPane.ShowPane();
    }

    private void RefreshCurrentValues(object sender, EventArgs e)
    {
      foreach (Control ctrl in testPaneLeftpanel.Controls)
      {
        if (ctrl is InitialTestDisplay)
          ((InitialTestDisplay)ctrl).RefreshValues();
      }
    }

    private void ReplacePanes(object sender, EventArgs e)
    {
      int ypos = (this.Height - (promptPane.Size.Height + testPane.Size.Height + resultPane.Size.Height)) / 2;
      int xpos = (this.Width - testPane.Size.Width) / 2;
      promptPane.Location = new Point(xpos, ypos);
      testPane.Location = new Point(xpos, ypos + promptPane.Size.Height);
      resultPane.Location = new Point(xpos, testPane.Location.Y + testPane.Size.Height);
    }

    private void resultPane_AbortTestButton_Click(object sender, EventArgs e)
    {
      mMeasurement.StopInitialTest();

      switch (mMeasurement.Report.TestScriptRunned.Gearbox.ControllerType)
      {
        case GearboxControllerType.ZF_6HPxx_1911E:
        case GearboxControllerType.ZF_6HPxx_1911M:
        case GearboxControllerType.ZF_6HPxx_CE:
        case GearboxControllerType.ZF_6HPxx_CM:
        case GearboxControllerType.ZF_6HPxx_TUCE:
        case GearboxControllerType.ZF_6HPxx_TUCM:
        case GearboxControllerType.ZF_6HPxx_WM:
          {
            // If this is an Zf6 gearbox, maxe sure the drive is turned off
            mMeasurement.UsbDev_Decoder.DisableGearboxDrive();
            break;
          }

        case GearboxControllerType.NISSAN_RE5:
          {
            // If this is Nissan RE5 gearbox
            mMeasurement.Device.NissanRE5_Interface.DisableDrive();
            break;
          }
        case GearboxControllerType.GM6T40:
        case GearboxControllerType.GM6T70:
        case GearboxControllerType.GM6L:
          {
            // If this is GM 6Txx gearbox
            mMeasurement.GM6TxxGovernor.DisableDrive();
            break;
          }
      }
      promptPane.HidePane();
      testPane.HidePane();
      resultPane.HidePane();
    }

    private void resultPane_StartLoopTestButton_Click(object sender, EventArgs e)
    {
      // Stop the initial test
      mMeasurement.StopInitialTest();
      // No matter what user has selected, in loop mode the report should not be created or viewed
      mMeasurement.GenerateReport = false;
      // To be sure check if device connected and try to run the loop test
      if (mMeasurement.Device.IsConnected)
      {
        mMeasurement.StartLoopTest();
      }
      promptPane.HidePane();
      testPane.HidePane();
      resultPane.HidePane();
    }

    private void resultPane_StartManualTestButton_Click(object sender, EventArgs e)
    {
      // Stop the initial test
      mMeasurement.StopInitialTest();
      // To be sure check if device connected and try to run the manual test
      if (mMeasurement.Device.IsConnected)
      {
        mMeasurement.StartManualTest();
      }
      promptPane.HidePane();
      testPane.HidePane();
      resultPane.HidePane();
    }

    private void resultPane_StartAutomaticTestButton_Click(object sender, EventArgs e)
    {
      // Stop the initial test
      mMeasurement.StopInitialTest();
      // To be sure check if device connected and try to run the automatic test
      if (mMeasurement.Device.IsConnected)
      {
        mMeasurement.StartAutomaticTest();
      }
      promptPane.HidePane();
      testPane.HidePane();
      resultPane.HidePane();
    }

    #endregion Private Methods

    private void zf6InitPanel_ContinueButtonClicked(object sender, EventArgs e)
    {

      testPassLabel.Text = "INITIAL TEST PASSED";
      infoLabel1.Text = "You can safely continue the test. Choose what to do next:\n";

      // Disable all drivers as the user will need to choose which way to go
      // The initial test is not ended here on purpose
      mMeasurement.DisableAllDrivers();

      mReport.TestScriptRunned.SaveXml(mReport.TestScriptRunned.Filename);

      testPane.HidePane();
      resultPane.ShowPane();

    }

    private void zf6InitPanel_CancelButtonClicked(object sender, EventArgs e)
    {
      mMeasurement.StopInitialTest();

      switch (mMeasurement.Report.TestScriptRunned.Gearbox.ControllerType)
      {
        case GearboxControllerType.ZF_6HPxx_1911E:
        case GearboxControllerType.ZF_6HPxx_1911M:
        case GearboxControllerType.ZF_6HPxx_CE:
        case GearboxControllerType.ZF_6HPxx_CM:
        case GearboxControllerType.ZF_6HPxx_TUCE:
        case GearboxControllerType.ZF_6HPxx_TUCM:
        case GearboxControllerType.ZF_6HPxx_WM:
          {
            // If this is zf6 controlled gearbox, maxe sure the drive is turned off
            mMeasurement.UsbDev_Decoder.DisableGearboxDrive();
            break;
          }

        case GearboxControllerType.NISSAN_RE5:
          {
            // If this is Nissan RE5 gearbox
            mMeasurement.Device.NissanRE5_Interface.DisableDrive();
            break;
          }
        case GearboxControllerType.GM6T40:
        case GearboxControllerType.GM6T70:
        case GearboxControllerType.GM6L:
          {
            // If this is GM 6Txx gearbox
            mMeasurement.GM6TxxGovernor.DisableDrive();
            break;
          }
      }
      promptPane.HidePane();
      testPane.HidePane();
      resultPane.HidePane();
    }

    private void nissanRE5InitPanel_CancelButtonClicked(object sender, EventArgs e)
    {
      mMeasurement.StopInitialTest();

      switch (mMeasurement.Report.TestScriptRunned.Gearbox.ControllerType)
      {
        case GearboxControllerType.ZF_6HPxx_1911E:
        case GearboxControllerType.ZF_6HPxx_1911M:
        case GearboxControllerType.ZF_6HPxx_CE:
        case GearboxControllerType.ZF_6HPxx_CM:
        case GearboxControllerType.ZF_6HPxx_TUCE:
        case GearboxControllerType.ZF_6HPxx_TUCM:
        case GearboxControllerType.ZF_6HPxx_WM:
          {
            // If this is zf6 controlled gearbox, maxe sure the drive is turned off
            mMeasurement.UsbDev_Decoder.DisableGearboxDrive();
            break;
          }

        case GearboxControllerType.NISSAN_RE5:
          {
            // If this is Nissan RE5 gearbox
            mMeasurement.Device.NissanRE5_Interface.DisableAllSolenoids();
            mMeasurement.Device.NissanRE5_Interface.DisableDrive();
            break;
          }

        case GearboxControllerType.GM6T40:
        case GearboxControllerType.GM6T70:
        case GearboxControllerType.GM6L:
          {
            // If this is GM 6Txx gearbox
            mMeasurement.GM6TxxGovernor.DisableDrive();
            break;
          }
      }
      promptPane.HidePane();
      testPane.HidePane();
      resultPane.HidePane();
    }

    private void nissanRE5InitPanel_ContinueButtonClicked(object sender, EventArgs e)
    {
      testPassLabel.Text = "INITIAL TEST PASSED";
      infoLabel1.Text = "You can safely continue the test. Choose what to do next:\n";

      // Disable all drivers as the user will need to choose which way to go
      // The initial test is not ended here on purpose
      mMeasurement.DisableAllDrivers();
      mMeasurement.Device.NissanRE5_Interface.DisableAllSolenoids();

      mReport.TestScriptRunned.SaveXml(mReport.TestScriptRunned.Filename);

      testPane.HidePane();
      resultPane.ShowPane();
    }

    private void gM6TxxInitPanel_CancelButtonClicked(object sender, EventArgs e)
    {
      mMeasurement.StopInitialTest();

      switch (mMeasurement.Report.TestScriptRunned.Gearbox.ControllerType)
      {
        case GearboxControllerType.ZF_6HPxx_1911E:
        case GearboxControllerType.ZF_6HPxx_1911M:
        case GearboxControllerType.ZF_6HPxx_CE:
        case GearboxControllerType.ZF_6HPxx_CM:
        case GearboxControllerType.ZF_6HPxx_TUCE:
        case GearboxControllerType.ZF_6HPxx_TUCM:
        case GearboxControllerType.ZF_6HPxx_WM:
        {
          // If this is zf6 controlled gearbox, maxe sure the drive is turned off
          mMeasurement.UsbDev_Decoder.DisableGearboxDrive();
          break;
        }

        case GearboxControllerType.NISSAN_RE5:
        {
          // If this is Nissan RE5 gearbox
          mMeasurement.Device.NissanRE5_Interface.DisableAllSolenoids();
          mMeasurement.Device.NissanRE5_Interface.DisableDrive();
          break;
        }

        case GearboxControllerType.GM6T40:
        case GearboxControllerType.GM6T70:
        case GearboxControllerType.GM6L:
        {
          // If this is GM 6Txx gearbox
          mMeasurement.GM6TxxGovernor.DisableDrive();
          break;
        }
      }
      promptPane.HidePane();
      testPane.HidePane();
      resultPane.HidePane();
    }

    private void gM6TxxInitPanel_ContinueButtonClicked(object sender, EventArgs e)
    {
      testPassLabel.Text = "INITIAL TEST PASSED";
      infoLabel1.Text = "You can safely continue the test. Choose what to do next:\n";

      // Disable all drivers as the user will need to choose which way to go
      // The initial test is not ended here on purpose
      mMeasurement.DisableAllDrivers();
      //mMeasurement.Device.NissanRE5_Interface.DisableAllSolenoids();

      mReport.TestScriptRunned.SaveXml(mReport.TestScriptRunned.Filename);

      testPane.HidePane();
      resultPane.ShowPane();
    }


  }
}
