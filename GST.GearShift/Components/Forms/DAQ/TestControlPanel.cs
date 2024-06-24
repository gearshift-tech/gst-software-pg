using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using GST.Gearshift.Components.Interfaces.USB;
using GST.ZF6.Components.Interfaces.MechShifterUSB;
using Soko.Common.Common;

namespace GST.Gearshift.Components.Forms.DAQ
{
  public partial class TestControlPanel : UserControl
  {



    #region Constants



    #endregion  Constants



    #region Private fields

    //Measurement object
    private Measurement mMeasurement = null;

    System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

    private Int32 mMajorProgressBarBaseValueMs = 0;
    private Int32 mMajorProgressBarHoldCountingAt = 0;
    private bool mMajorProgressBarCountingUp = true;

    private bool _lastZf6UsbConnStat = false;
    private GearboxConnectionStatus _lastZf6TcuConnStat = GearboxConnectionStatus.Idle;
    private int _lastZf6TcuCommandedGear = -5;
    private float _lastZf6TcuReadTemp = 0;



    #endregion Private fields



    #region Constructors & finalizer

    /// <summary>
    /// Default constructor
    /// </summary>
    public TestControlPanel()
    {
      InitializeComponent();
      //mMeasurement = new Measurement();
      //You MUST provide the measurement object externally, otherwise
      //the designer where u'll use this control will crash. SIC!
    }

    #endregion Constructors & finalizer



    #region Events

    #endregion Events



    #region Properties

    /// <summary>
    /// Measurement object
    /// </summary>
    public Measurement MeasurementSession
    {
      get { return mMeasurement; }
      set
      {
        //null is allowed here
        mMeasurement = value;
        ManagePanelsPresence();
        if (mMeasurement != null)
        {
          mMeasurement.Device.DeviceConnectedEvent += new System.EventHandler(ManagePanelsPresenceEH);
          mMeasurement.Device.DeviceDisconnecedEvent += new System.EventHandler(ManagePanelsPresenceEH);
          mMeasurement.GearSwitchedEvent += new Measurement.GearSwitchedEventHandler(GearSwitched);
          mMeasurement.MeasurementStoppedEvent += new System.EventHandler(MeasurementStopped);
          mMeasurement.GearLockOn += new System.EventHandler(mMeasurement_GearLockOn);
          mMeasurement.GearLockOff += new System.EventHandler(mMeasurement_GearLockOff);
          mMeasurement.ProgressBarFeedbackEvent += new Measurement.ProgressBarEventHandler(mMeasurement_ProgressBarFeedbackEvent);
          mMeasurement.MeasurementStateChanged += new Measurement.MeasurementStateChangedEventHandler(mMeasurement_MeasurementStateChanged);
        }
      }
    }

    #endregion Properties



    #region Methods


    /// <summary>
    /// Stop button click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void stopButton_Click(object sender, EventArgs e)
    {
      if (mMeasurement.IsRunning)
      {
        mMeasurement.StopMeasurement();
      }
    }

    public void ManagePanelsPresenceEH(object sender, EventArgs e)
    {
      if (InvokeRequired)
      {
        this.BeginInvoke(new MethodInvoker(ManagePanelsPresence));
      }
      else
      {
        ManagePanelsPresence();
      }
    }

    public void ManagePanelsPresence()
    {
      if (mMeasurement == null)
        return;
      if (mMeasurement.Device.IsConnected)
      {
        // Make sure the pane with start/stop button is visible
        startStopPane.ShowPane();

        switch (mMeasurement.MsrmntState)
        {
          case Measurement.MeasurementState.Stopped:
          case Measurement.MeasurementState.Idle:
          case Measurement.MeasurementState.RunningInitialTest:
          case Measurement.MeasurementState.RunningAutomaticTest:
          case Measurement.MeasurementState.RunningLoopTest:
            {
              // In this state the gear up/down button pane should not be visible.
              manualControlPane.HidePane();
              break;
            }

          case Measurement.MeasurementState.RunningManualTest:
            {
              // In this state gear up/down pane must be available
              manualControlPane.ShowPane();
              break;
            }
        }

        // Make sure the status pane is visible
        gearInfoPane.ShowPane();


        switch (mMeasurement.Report.TestScriptRunned.Gearbox.ControllerType)
        {
          default:
          case GearboxControllerType.NON_MECHATRONIC:
            {
              // If this is an analogue gearbox
              zf6StatusPane.HidePane();
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
              zf6StatusPane.ShowPane();
              break;
            }

          case GearboxControllerType.NISSAN_RE5:
            {
              // If this is Nissan RE5 gearbox
              // TODO: Do sth for Nissan!
              zf6StatusPane.ShowPane();
              break;
            }
          case GearboxControllerType.GM6T40:
          case GearboxControllerType.GM6T70:
          case GearboxControllerType.GM6L:
            {
              // If this is GM 6Txx or 6L gearbox
              zf6StatusPane.ShowPane();
              break;
            }
        }

      }
      else
      {
        startStopPane.HidePane();
        manualControlPane.HidePane();
        gearInfoPane.HidePane();
        zf6StatusPane.HidePane();
      }
    }



    private delegate void UpdateGearLabel(string msg);

    private void OnUpdate(string msg)
    {
      gearLabel.Text = msg;
    }

    private void GearSwitched(TestScriptFrame frame)
    {
      try
      {
        if (frame != null)
        {
          UpdateGearLabel ulb = new UpdateGearLabel(this.OnUpdate);
          gearLabel.BeginInvoke(ulb, new object[] { frame.FrameName });
        }
      }
      catch (Exception) { }
    }

    private void MeasurementStopped(object sender, EventArgs e)
    {
      UpdateGearLabel ulb = new UpdateGearLabel(this.OnUpdate);
      gearLabel.BeginInvoke(ulb, new object[] { string.Empty });
    }

    private void gearUpButton_Click(object sender, EventArgs e)
    {
      mMeasurement.SwitchGearUp();
    }

    private void gearDownButton_Click(object sender, EventArgs e)
    {
      mMeasurement.SwitchGearDown();
    }



    #region Pane auto-location

    private void startStopPane_Resize(object sender, EventArgs e)
    {
      manualControlPane.Location = new Point(startStopPane.Location.X, startStopPane.Location.Y + startStopPane.Height + 0);
    }

    private void startStopPane_Move(object sender, EventArgs e)
    {
      manualControlPane.Location = new Point(startStopPane.Location.X, startStopPane.Location.Y + startStopPane.Height + 0);
    }

    private void manualControlPane_Resize(object sender, EventArgs e)
    {
      gearInfoPane.Location = new Point(manualControlPane.Left, manualControlPane.Bottom);
    }

    private void manualControlPane_Move(object sender, EventArgs e)
    {
      gearInfoPane.Location = new Point(manualControlPane.Left, manualControlPane.Bottom);
    }

    private void gearInfoPane_Move(object sender, EventArgs e)
    {
      zf6StatusPane.Location = new Point(gearInfoPane.Left, gearInfoPane.Bottom);
    }

    private void gearInfoPane_Resize(object sender, EventArgs e)
    {
      zf6StatusPane.Location = new Point(gearInfoPane.Left, gearInfoPane.Bottom);
    }

    #endregion Pane auto-location

    #endregion Methods

    private void gearLockTimer_Tick(object sender, EventArgs e)
    {
      //try
      //{
      int elapsedMs = (int)stopwatch.ElapsedMilliseconds;

      // Deal with the major progress bar
      if (mMajorProgressBarCountingUp)
      {
        // If this is switching the gear up
        int majorPBExpVal = mMajorProgressBarBaseValueMs + elapsedMs;
        if (majorPBExpVal >= mMajorProgressBarHoldCountingAt)
        {
          if (mMajorProgressBarHoldCountingAt > majorProgressBar.Maximum) mMajorProgressBarHoldCountingAt = majorProgressBar.Maximum;
          majorProgressBar.Value = mMajorProgressBarHoldCountingAt;
          if (majorProgressBar.Value > 0)
          {
            majorProgressBar.Value -= 1;
            majorProgressBar.Value += 1;
          }
          if (minorProgressBar.Value == minorProgressBar.Maximum)
          {
            stopwatch.Stop();
            gearLockTimer.Enabled = false;
          }
        }
        else
        {
          if (majorPBExpVal <= majorProgressBar.Maximum)
          {
            majorProgressBar.Value = majorPBExpVal;
            if (majorProgressBar.Value > 0)
            {
              majorProgressBar.Value -= 1;
              majorProgressBar.Value += 1;
            }
          }
          else
          {
            majorProgressBar.Value = majorProgressBar.Maximum;
            if (majorProgressBar.Value > 0)
            {
              majorProgressBar.Value -= 1;
              majorProgressBar.Value += 1;
            }
          }
        }
      }
      else
      {
        // If this is switching the gear down

        int majorPBExpVal = mMajorProgressBarBaseValueMs - elapsedMs;
        if (majorPBExpVal <= mMajorProgressBarHoldCountingAt)
        {
          majorProgressBar.Value = mMajorProgressBarHoldCountingAt;
          if (minorProgressBar.Value == minorProgressBar.Maximum)
          {
            stopwatch.Stop();
            gearLockTimer.Enabled = false;
          }
        }
        else
        {
          if (majorPBExpVal >= majorProgressBar.Minimum)
          {
            majorProgressBar.Value = majorPBExpVal;
          }
          else
          {
            majorProgressBar.Value = majorProgressBar.Minimum;
          }
        }
        //Console.WriteLine("PBVAL: " + majorProgressBar.Value.ToString());
      }
      majorProgressTextLabel.Text = (majorProgressBar.Value / 1000.0f).ToString("0.0") + " of " + (majorProgressBar.Maximum / 1000.0f).ToString("0.0") + "s";

      // Deal with the minor progress bar
      if (elapsedMs > minorProgressBar.Maximum)
      {
        minorProgressBar.Value = minorProgressBar.Maximum;
        if (minorProgressBar.Value > 0)
        {
          minorProgressBar.Value -= 1;
          minorProgressBar.Value += 1;
        }
        //if (majorProgressBar.Value == mMajorProgressBarHoldCountingAt)
        //{
        //  stopwatch.Stop();
        //  gearLockTimer.Enabled = false;
        //}
      }
      else
      {
        minorProgressBar.Value = elapsedMs;
      }
      minorProgressTextLabel.Text = (minorProgressBar.Value / 1000.0f).ToString("0.0") + " of " + (minorProgressBar.Maximum / 1000.0f).ToString("0.0") + "s";
      //}
      //catch (Exception ex)
      //{
      //  Console.WriteLine("Exception in ControlPanel timer: " + ex.Message);
      //}
    }

    public void ClearObsoleteData()
    {
      if (InvokeRequired)
      {
        this.BeginInvoke(new MethodInvoker(ClearObsoleteData));
        return;
      }
      gearLockTimer.Enabled = false;
      gearLabel.Text = "N/A";
      majorProgressBar.Value = majorProgressBar.Minimum;
      majorProgressTextLabel.Text = string.Empty;
      minorProgressBar.Value = minorProgressBar.Minimum;
      minorProgressTextLabel.Text = string.Empty;

      ManagePanelsPresence();
    }

    void mMeasurement_MeasurementStateChanged(Measurement.MeasurementState msrmtState)
    {
      switch (msrmtState)
      {
        case Measurement.MeasurementState.Stopped:
          {
            // Set the controls to the 'Clean state' at the test end.
            testStateButton.Image = GST.Gearshift.Components.Properties.Resources.ControlPanel_Stopped_96x96;
            testStateButton.Text = "Measurement stopped";
            ClearObsoleteData();
            break;
          }
        case Measurement.MeasurementState.Idle:
          {
            testStateButton.Image = GST.Gearshift.Components.Properties.Resources.ControlPanel_Idle_96x96;
            testStateButton.Text = "Measurement idle";
            ClearObsoleteData();
            break;
          }
        case Measurement.MeasurementState.RunningInitialTest:
          {
            testStateButton.Image = GST.Gearshift.Components.Properties.Resources.ControlPanel_Initial_96x96;
            testStateButton.Text = "Running initial test";
            break;
          }
        case Measurement.MeasurementState.RunningManualTest:
          {
            testStateButton.Image = GST.Gearshift.Components.Properties.Resources.ControlPanel_ManualMode_96x96;
            testStateButton.Text = "Running manual test";
            break;
          }
        case Measurement.MeasurementState.RunningAutomaticTest:
          {
            testStateButton.Image = GST.Gearshift.Components.Properties.Resources.ControlPanel_AutoMode_96x96;
            testStateButton.Text = "Running automatic test";
            break;
          }
        case Measurement.MeasurementState.RunningLoopTest:
          {
            testStateButton.Image = GST.Gearshift.Components.Properties.Resources.ControlPanel_LoopMode_96x96;
            testStateButton.Text = "Running loop test";
            break;
          }
      }
      ManagePanelsPresence();
    }

    void mMeasurement_ProgressBarFeedbackEvent(Measurement.ProgressBarEventAttributes attr)
    {
      if (InvokeRequired)
      {
        Measurement.ProgressBarEventHandler dg = new Measurement.ProgressBarEventHandler(mMeasurement_ProgressBarFeedbackEvent);
        BeginInvoke(dg, new object[] { attr });
      }
      else
      {
        switch (attr.mEventType)
        {
          case Measurement.ProgressBarEventAttributes.EventType.StartCounting:
            {
              Console.WriteLine("START COUNTING");
              mMajorProgressBarCountingUp = true;
              mMajorProgressBarBaseValueMs = 0;
              mMajorProgressBarHoldCountingAt = attr.mMajorCountForMs;
              majorProgressBar.Minimum = 0;
              majorProgressBar.Value = 0;
              majorProgressBar.Maximum = (int)attr.mMajorProgressBarEndMs;
              majorProgressBarLabel.Text = attr.mMajorProgressBarLabel;
              //majorProgressBarLabel.Text = "0.0s of " + (majorProgressBar.Maximum / 1000.0f).ToString("0.0") + "s";
              minorProgressBar.Minimum = 0;
              minorProgressBar.Value = 0;
              minorProgressBar.Maximum = attr.mMinorProgressBarEndMs;
              minorProgressBarLabel.Text = attr.mMinorProgressBarLabel;
              stopwatch.Reset();
              stopwatch.Start();
              gearLockTimer.Enabled = true;
              gearUpButton.Enabled = false;
              gearDownButton.Enabled = false;
              break;
            }
          case Measurement.ProgressBarEventAttributes.EventType.ResumeCounting:
            {
              //Console.WriteLine(" COUNTER VALUE: " +attr.mCountForMs.ToString());
              if (attr.mMajorCountForMs >= 0)
              {
                mMajorProgressBarBaseValueMs = mMajorProgressBarHoldCountingAt;
                mMajorProgressBarHoldCountingAt = mMajorProgressBarBaseValueMs + attr.mMajorCountForMs;
                mMajorProgressBarCountingUp = true;
                minorProgressBar.Minimum = 0;
                minorProgressBar.Value = 0;
                minorProgressBar.Maximum = attr.mMinorCountForMs;
                stopwatch.Reset();
                stopwatch.Start();
                gearLockTimer.Enabled = true;
              }
              else
              {
                mMajorProgressBarBaseValueMs = mMajorProgressBarHoldCountingAt;
                mMajorProgressBarHoldCountingAt = mMajorProgressBarBaseValueMs + attr.mMajorCountForMs;
                mMajorProgressBarCountingUp = false;
                minorProgressBar.Minimum = 0;
                minorProgressBar.Value = 0;
                minorProgressBar.Maximum = attr.mMinorCountForMs;
                stopwatch.Reset();
                stopwatch.Start();
                gearLockTimer.Enabled = true;
              }
              break;
            }
          case Measurement.ProgressBarEventAttributes.EventType.StopCounting:
            {
              break;
            }
          case Measurement.ProgressBarEventAttributes.EventType.UpdateMajorPbData:
            {
              majorProgressBar.Minimum = 0;
              majorProgressBar.Value = 0;
              majorProgressBar.Maximum = attr.mMajorProgressBarEndMs;
              majorProgressBarLabel.Text = attr.mMajorProgressBarLabel;
              break;
            }
          default:
            {
              break;
            }
        }
      }
    }

    void mMeasurement_GearLockOff(object sender, EventArgs e)
    {
      if (InvokeRequired)
      {
        EventHandler dg = new EventHandler(mMeasurement_GearLockOff);
        BeginInvoke(dg, new object[] { sender, e });
      }
      else
      {
        gearUpButton.Enabled = true;
        gearDownButton.Enabled = true;
      }
    }

    void mMeasurement_GearLockOn(object sender, EventArgs e)
    {
      if (InvokeRequired)
      {
        EventHandler dg = new EventHandler(mMeasurement_GearLockOn);
        BeginInvoke(dg, new object[] { sender, e });
      }
      else
      {
        gearUpButton.Enabled = false;
        gearDownButton.Enabled = false;
      }
    }

    private void zf6UpdateTimer_Tick(object sender, EventArgs e)
    {
      if (mMeasurement != null)
      {
        // Sort USB connection label
        if (mMeasurement.Device.UsbDev_Decoder.IsConnected != _lastZf6UsbConnStat)
        {
          _lastZf6UsbConnStat = mMeasurement.Device.UsbDev_Decoder.IsConnected;
          if (_lastZf6UsbConnStat)
            zf6UsbConnLabel.Text = "USB Connection: Yes";
          else
            zf6UsbConnLabel.Text = "USB Connection: No";
        }

        switch (mMeasurement.Report.TestScriptRunned.Gearbox.ControllerType)
        {
          default:
          case GearboxControllerType.NON_MECHATRONIC:
            {
              zf6TcuConnectionLabel.Text = "TCU Connection: Idle";
              zf6TcuCommandedGearLabel.Text = "TCU commanded gear: ?";
              zf6ActualGear.Text = "TCU actual gear: ?";
              zf6TcuTempLabel.Text = "TCU Temperature: ?";
              zf6FluidTempLabel.Text = "Fluid temperature: ?";
                zf6SelectorPosLabel.Text = "Selector: ?";
              break;
            }
          case GearboxControllerType.GM6T40:
          case GearboxControllerType.GM6T70:
          case GearboxControllerType.GM6L:
            {
              if (mMeasurement.GM6TxxGovernor.TcuPresent)
              {
                zf6TcuConnectionLabel.Text = "TCU Connection: Connected";
              }
              else
              {
                zf6TcuConnectionLabel.Text = "TCU Connection: Idle";
              }

              zf6TcuCommandedGearLabel.Text = "TCU commanded gear: " + mMeasurement.GM6TxxGovernor.TCU_Data.CommandedGear_Desired.ToString();
              zf6ActualGear.Text = "TCU actual gear: " + mMeasurement.GM6TxxGovernor.TCU_Data.CommandedGear_Actual.ToString();
              zf6TcuTempLabel.Text = "TCU Temperature: " + mMeasurement.GM6TxxGovernor.TCU_Data.TcuTemperature.ToString() + "°C";
              zf6FluidTempLabel.Text = "Fluid temperature: " + mMeasurement.GM6TxxGovernor.TCU_Data.FluidTemperature.ToString() + "°C";
              zf6SelectorPosLabel.Text = "Selector: " + mMeasurement.GM6TxxGovernor.TCU_Data.GearLeverPosition.ToString();
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
              zf6ActualGear.Text = "TCU actual gear: ?";
              zf6TcuTempLabel.Text = "TCU Temperature: ?";
              zf6FluidTempLabel.Text = "Fluid temperature: ?";
              zf6SelectorPosLabel.Text = "Selector: ?";
              // Sort TCU connection label
              if (mMeasurement.Device.UsbDev_Decoder.GearboxConnectionStatus != _lastZf6TcuConnStat)
              {
                _lastZf6TcuConnStat = mMeasurement.Device.UsbDev_Decoder.GearboxConnectionStatus;
                switch (_lastZf6TcuConnStat)
                {
                  case GearboxConnectionStatus.Connected:
                    {
                      zf6TcuConnectionLabel.Text = "TCU Connection: Connected";
                      break;
                    }
                  case GearboxConnectionStatus.Connecting:
                    {
                      zf6TcuConnectionLabel.Text = "TCU Connection: Connecting";
                      break;
                    }
                  case GearboxConnectionStatus.Idle:
                    {
                      zf6TcuConnectionLabel.Text = "TCU Connection: Idle";
                      break;
                    }
                }
              }

              // Sort TCU selected gear label
              if (mMeasurement.Device.UsbDev_Decoder.GearboxPhysicallySelectedGear != _lastZf6TcuCommandedGear)
              {
                _lastZf6TcuCommandedGear = mMeasurement.Device.UsbDev_Decoder.GearboxPhysicallySelectedGear;

                switch (_lastZf6TcuCommandedGear)
                {
                  case 0x0C:
                    {
                      zf6TcuCommandedGearLabel.Text = "TCU commanded gear: R";
                      break;
                    }
                  case 10:
                    {
                      zf6TcuCommandedGearLabel.Text = "TCU commanded gear: N";
                      break;
                    }
                  case 1:
                  case 2:
                  case 3:
                  case 4:
                  case 5:
                    {
                      zf6TcuCommandedGearLabel.Text = "TCU commanded gear: " + _lastZf6TcuCommandedGear.ToString();
                      break;
                    }
                  default:
                    {
                      zf6TcuCommandedGearLabel.Text = "TCU commanded gear: N";
                      break;
                    }
                }
              }
              break;
            }

        }


      }
    }







  }
}
