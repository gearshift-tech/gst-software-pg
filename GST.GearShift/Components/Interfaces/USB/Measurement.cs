using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.IO;
using Soko.Common.Common;


namespace GST.Gearshift.Components.Interfaces.USB
{
  /// <remarks>
  /// Class representing the measurement session
  /// </remarks>
  public class Measurement
  {

    private class GearRatioCalculator
    {
      private GearShiftUsb.AIChannel _inputSpeedChannel = null;
      private List<GearShiftUsb.AIChannel> _outputSpeedChannelsList = new List<GearShiftUsb.AIChannel>();
      private GearShiftUsb.AIChannel _gearRatioChannel = null;

      private int _inputSpeedArrayIndex = -1;
      private List<int> _outputSpeedsArrayIndices = new List<int>();
      private int _gearRatioArrayIndex = -1;

      private bool _canCalculateRatios = false;

      public void InitControl(GearboxConfig gbc)
      {
        _inputSpeedChannel = null;
        _outputSpeedChannelsList = new List<GearShiftUsb.AIChannel>();
        _gearRatioChannel = null;
        _inputSpeedArrayIndex = -1;
        _outputSpeedsArrayIndices = new List<int>();
        _gearRatioArrayIndex = -1;
        _canCalculateRatios = false;

        for (int i = 0; i < gbc._analogueInputs.Count; i++)
        {
          GearShiftUsb.AIChannel aic = gbc._analogueInputs[i];
          switch (aic.ValueType)
          {
            case Utilities.MeasurementUnit.ValueType.InputSpeed:
              {
                _inputSpeedChannel = aic;
                _inputSpeedArrayIndex = i;
                break;
              }
            case Utilities.MeasurementUnit.ValueType.OutputSpeed:
              {
                _outputSpeedChannelsList.Add(aic);
                _outputSpeedsArrayIndices.Add(i);
                break;
              }
            case Utilities.MeasurementUnit.ValueType.GearRatio:
              {
                _gearRatioChannel = aic;
                _gearRatioArrayIndex = i;
                break;
              }
          }
        }

        if (_inputSpeedChannel != null && _outputSpeedChannelsList.Count > 0 && _gearRatioChannel != null)
        {
          _canCalculateRatios = true;
        }

      }

      public void CalculateRatios(GearShiftUsb.DeviceAcquiredData data)
      {
        try
        {
          if (_canCalculateRatios)
          {
            // Ratio is assumed to be average output speed over input speed.
            float avgOutputSpd = 0;
            float ratio = 0;
            foreach (int index in _outputSpeedsArrayIndices)
            {
              avgOutputSpd += data.pressures[index];
            }
            avgOutputSpd /= _outputSpeedsArrayIndices.Count;

            float inputSpd = data.pressures[_inputSpeedArrayIndex];
            if (inputSpd > 0)
            {
              ratio = avgOutputSpd / inputSpd;
              if (ratio > 10)
              {
                ratio = 10;
              }
            }
            else
            {
              ratio = 10;
            }

            data.pressures[_gearRatioArrayIndex] = ratio;
          }
          else
          {
            if (_gearRatioArrayIndex > 0)
              data.pressures[_gearRatioArrayIndex] = -1.0f;
          }
        }
        catch
        {
          Console.WriteLine("error calc ratio");
        }
      }

    }

    #region Constants



    #endregion  Constants

    public enum MeasurementState
    {
      Stopped = 0,
      Idle = 1,
      RunningInitialTest = 2,
      RunningManualTest = 3,
      RunningAutomaticTest = 4,
      RunningLoopTest = 5,
      RunningAOsTest = 6,
      RunningReadonlyMode = 7
    }

    public enum LoopTestState
    {
      PreLoopBlock = 1,
      LoopBlock = 2
    }

    public class ProgressBarEventAttributes
    {
      public enum EventType
      {
        StartCounting = 0,
        ResumeCounting = 1,
        StopCounting = 2,
        UpdateMajorPbData = 3
      }

      public EventType mEventType = EventType.StopCounting;
      public Int32 mMajorProgressBarEndMs = 0;
      public Int32 mMinorProgressBarEndMs = 0;
      public Int32 mMinorCountForMs = 0;
      public string mMajorProgressBarLabel = "";
      public string mMinorProgressBarLabel = "";
      public Int32 mMajorCountForMs = 0;
    }

    #region Private fields

    // Current measurement state
    MeasurementState mMode = MeasurementState.Stopped;

    public bool GenerateReport = true;

    // GearShift USB device
    private GearShiftUsb mDevice = null;

    // Zf6 USB interface
    private GST.ZF6.Components.Interfaces.MechShifterUSB.UsbDevice _usbDev_Decoder = null;

    private DAQ_EventManager _EventManager = new DAQ_EventManager();

    private GearRatioCalculator _grc = new GearRatioCalculator();

    // Test script to get measurement data from
    private TestScript mTestScript = null;

    // Report to save data to
    private TestScriptReport mReport = new TestScriptReport();

    // Indices of channels that need to be processed
    private int[] mUsedPressureChannelsIndices = null;
    private int[] mUsedCurrentChannelsIndices = null;

    // The highest ID used in the measurement
    private UInt32 mHighestID = 0;

    // If gearbox has failed the current test
    private bool mCurrentTestFailed = false;

    // Current position in mCriticalIDsList
    private int mCurrScriptLineIndex = 0;

    // Current gear name that stems from mGearsList[mCurrScriptLine]
    private string mCurrGear = string.Empty;

    private int _loopBlockScriptLineStart = 0;
    private int _loopBlockScriptLineEnd = 0;
    private int _loopBlockLengthMs = 0;
    private int _preLoopBlockScriptLineEnd = 0;
    private int _preLoopBlockLengthMs = 0;


    #endregion Private fields


    #region Constructors & finalizer

    /// <summary>
    /// The default constructor
    /// </summary>
    public Measurement()
    {
      mDevice = null;
      mTestScript = new TestScript();
      mUsedCurrentChannelsIndices = new int[0];
      mUsedPressureChannelsIndices = new int[0];

      _EventManager.EventFired += new DAQ_EventManager.EventFiredHandler(_EventManager_EventFired);
    }



    #endregion Constructors & finalizer


    #region Events

    /// <summary>
    /// On measurement stopped event
    /// </summary>
    public event EventHandler MeasurementStoppedEvent;

    /// <summary>
    /// On main measurement started event
    /// </summary>
    public event EventHandler MainTestStartedEvent;

    /// <summary>
    /// On main measurement stopped event
    /// </summary>
    public event EventHandler MainTestStoppedEvent;

    /// <summary>
    /// On initial test started event
    /// </summary>
    public event EventHandler InitialTestStartedEvent;

    /// <summary>
    /// On initial test finished event
    /// </summary>
    public event EventHandler InitialTestFinishedEvent;

    public delegate void GearSwitchedEventHandler(TestScriptFrame frm);

    /// <summary>
    /// On gear switched event
    /// </summary>
    public event GearSwitchedEventHandler GearSwitchedEvent;

    /// <summary>
    /// On prompt message display
    /// </summary>
    public event EventHandler DisplayPromptEvent;

    public event EventHandler GearLockOn;

    public event EventHandler GearLockOff;

    public delegate void ProgressBarEventHandler(ProgressBarEventAttributes attr);

    public event ProgressBarEventHandler ProgressBarFeedbackEvent;

    public delegate void MeasurementStateChangedEventHandler(MeasurementState msrmtState);

    public event MeasurementStateChangedEventHandler MeasurementStateChanged;

    #endregion Events


    #region Properties

    public MeasurementState MsrmntState
    {
      get { return mMode; }
      protected set
      {
        bool valueChanged = (mMode != value);
        mMode = value;
        if (valueChanged && MeasurementStateChanged != null)
        {
          MeasurementStateChanged(mMode);
        }
      }
    }

    /// <summary>
    /// Gets if this measurement is running
    /// </summary>
    public bool IsRunning
    {
      get { return MsrmntState != MeasurementState.Stopped; }
    }

    /// <summary>
    /// Gets if this measurement is the initial test
    /// </summary>
    public bool IsRunningInitialTest
    {
      get { return MsrmntState == MeasurementState.RunningInitialTest; }
    }

    /// <summary>
    /// Gets if this measurement is ready to start
    /// </summary>
    public bool IsReady
    {
      //////////////////////////////////////////////////////////////////////
      //this must be changed !!
      get { return true; }
    }

    /// <summary>
    /// Gets if this measurement has been failed
    /// </summary>
    public bool TestFailed
    {
      get { return mCurrentTestFailed; }
    }

    /// <summary>
    /// Zf6 USB interface
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GST.ZF6.Components.Interfaces.MechShifterUSB.UsbDevice UsbDev_Decoder
    {
      get { return _usbDev_Decoder; }
      set
      {
        if (value != null)
        {
          _usbDev_Decoder = value;
        }
      }
    }

    Soko.CanCave.Components.Interfaces.TCUGovernor_GM6Txx _GM6TxxGov = null;
    /// <summary>
    /// GM 6Txx governor
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Soko.CanCave.Components.Interfaces.TCUGovernor_GM6Txx GM6TxxGovernor
    {
      get { return _GM6TxxGov; }
      set
      {
        if (value != null)
        {
          _GM6TxxGov = value;
        }
      }
    }

    Soko.CanCave.Components.Interfaces.TCUGovernor_NissanRE5 _NissanRE5xxGov = null;
    /// <summary>
    /// GM 6Txx governor
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Soko.CanCave.Components.Interfaces.TCUGovernor_NissanRE5 NissanRE5xxGov
    {
      get { return _NissanRE5xxGov; }
      set
      {
        if (value != null)
        {
          _NissanRE5xxGov = value;
        }
      }
    }

    /// <summary>
    /// The device to talk with
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GearShiftUsb Device
    {
      get { return mDevice; }
      set
      {
        if (value == null)
          throw new ArgumentNullException("[Measurement.Device.Set] : The value cannot be null");
        mDevice = value;
        mDevice.DAQDataReceivedEvent += new GearShiftUsb.MyEventHandler(mDevice_DAQDataReceivedEvent);
        mDevice.DeviceDisconnecedEvent += new EventHandler(mDevice_DeviceDisconnecedEvent);
      }
    }

    /// <summary>
    /// The script to get data
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public TestScriptReport Report
    {
      get { return mReport; }
      set
      {
        if (IsRunning)
          throw new InvalidOperationException("[Measurement.TestScript.Set] : This value cannot be changed while the test is running");
        if (value == null)
          throw new ArgumentNullException("[Measurement.TestScript.Set] : The value cannot be null");

        mReport = value;
        mTestScript = mReport.TestScriptRunned;
        //prepare channels mapping
        GearboxConfig currCfg = mTestScript.Gearbox;
        mDevice.TestScript_ = mTestScript;
        _grc.InitControl(mTestScript.Gearbox);
        List<GearShiftUsb.AIChannel> aics = currCfg._analogueInputs;
        mUsedPressureChannelsIndices = new int[aics.Count];
        for (int i = 0; i < aics.Count; i++)
        {
          mUsedPressureChannelsIndices[i] = aics[i].InputIndex;
        }
        DisplayChannelsSet currChans = currCfg.CurrentDisplayChannelsSet;
        mUsedCurrentChannelsIndices = new int[currCfg.CurrentDisplayChannelsCount];
        for (int i = 0; i < currCfg.CurrentDisplayChannelsCount; i++)
        {
          DisplayChannel cChan = (DisplayChannel)currChans.Channels[i];
          mUsedCurrentChannelsIndices[i] = cChan.InputChannelIndex;
          cChan.ManualDriveStatusChanged += new EventHandler(DAQManualDriveStatusChanged);
          cChan.ManualDriveValueChanged += new EventHandler(DAQManualDriveValueChanged);
        }
        //PrepareData();
      }
    }

    #endregion Properties


    #region Methods

    /// <summary>
    /// Should be called when user accepts the prompt
    /// </summary>
    public void AcceptPrompt()
    {
      // It is assumed that prompts are showed ONLY when switching gears upwards
      switch (mMode)
      {
        case MeasurementState.RunningAutomaticTest:
          {
            SwitchGearUp();
            break;
          }
      }

    }

    /// <summary>
    /// Sends the frame switching off all solenoids.
    /// </summary>
    public void DisableAllDrivers()
    {
      if (mDevice.IsConnected)
      {
        //throw new InvalidOperationException( "Cannot send zero-frame when device is not connected." );
        //prepare the tx buffer with single frame (zero values)
        //mDevice.TxDataBuffer.Clear();
        //mDevice.RxDataBuffer.Clear();
        mDevice.DAQManualDriveDisableAll();
        UsbDAQTxData outPacket = new UsbDAQTxData();
        outPacket.Pwm = new byte[9];
        for (int i = 0; i < 9; i++)
        {
          outPacket.Pwm[i] = 0;
        }
        mHighestID++;
        outPacket.ID = mHighestID;
        mDevice.TxDataBuffer.Add(outPacket);
        mDevice.TxDataBuffer.Add(outPacket);
        mDevice.TxDataBuffer.Add(outPacket);
        mDevice.TxDataBuffer.Add(outPacket);
        //mDevice.SetDeviceDAQConfig( (UInt32)mTestScript.Gearbox.PwmFrequencyHz, mTestScript.Gearbox.CurrentDisplayChannelsSet );
        //Start the transmission and wait for the operator to press the stop key
      }
      //mDevice.StartDAQTransmission();

    }

    public void SetAOsValuesPerc(UInt32 AO1, UInt32 AO2)
    {
      if (!mDevice.IsConnected)
        throw new InvalidOperationException("Cannot set analog outputs, must be connected first");
      if (MsrmntState != MeasurementState.RunningAOsTest)
        throw new InvalidOperationException("Cannot set analog outputs test while not running AOs test");
      //prepare the tx buffer with single frame - PWMs disabled, AOs set to specified values
      mHighestID++;
      UsbDAQTxData outPacket = new UsbDAQTxData();
      outPacket.Pwm = new byte[9];
      for (int i = 0; i < 9; i++)
      {
        outPacket.Pwm[i] = 0;
      }
      outPacket.AO1 = (UInt16)(AO1 * 10.24f);
      outPacket.AO2 = (UInt16)(AO2 * 10.24f);
      outPacket.ID = mHighestID;
      mDevice.TxDataBuffer.Add(outPacket);
    }

    /// <summary>
    /// Starts the Analog outputs manual test
    /// </summary>
    public void StartAOsTest()
    {
        if (!mDevice.IsConnected)
        {         //throw new InvalidOperationException("Cannot start analog outputs test, must be connected first");
            Soko.Common.Forms.MessageBox.ShowInfo("GearShift",
            "Connection Error",
            "Cannot start analog outputs test, must be connected first",
            Soko.Common.Forms.MessageBoxButtons.OK);
        }
        if (MsrmntState != MeasurementState.Stopped)
        // throw new InvalidOperationException("Cannot start analog outputs test while other test is running");
        {
            Soko.Common.Forms.MessageBox.ShowInfo("GearShift",
            "Test Running",
            "Cannot start analog outputs test while other test is running",
            Soko.Common.Forms.MessageBoxButtons.OK);
        }
      //prepare the tx buffer with single frame - PWMs disabled, AOs disabled.
      mDevice.TxDataBuffer.Clear();
      mHighestID = 0;
      for (int i = 0; i < 32; i++)
      {
        UsbDAQTxData outPacket = new UsbDAQTxData();
        outPacket.Pwm = new byte[9];
        for (int j = 0; j < 9; j++)
        {
          outPacket.Pwm[j] = 0;
        }
        outPacket.AO1 = 0;
        outPacket.AO2 = 0;
        outPacket.ID = 0;
        mDevice.TxDataBuffer.Add(outPacket);
      }
      mDevice.SetDeviceDAQConfig((UInt32)mTestScript.Gearbox.PwmFrequencyHz, mTestScript.Gearbox.CurrentDisplayChannelsSet);
      // Start the transmission and wait for the operator to end the AOs test
      mDevice.StartDAQTransmission();
      // Set the proper measurement state value
      MsrmntState = MeasurementState.RunningAOsTest;
    }

    /// <summary>
    /// Starts the main measurement in automatic mode
    /// </summary>
    public void StartAutomaticTest()
    {
      if (!mDevice.IsConnected)
        throw new InvalidOperationException("Cannot start the main test, must be connected first!");

      MsrmntState = MeasurementState.RunningAutomaticTest;

      PrepareData();

      if (MainTestStartedEvent != null)
        MainTestStartedEvent(this, EventArgs.Empty);
    }

    /// <summary>
    /// Starts the readonly mode
    /// </summary>
    public void StarReadonlyMode()
    {
      if (!mDevice.IsConnected)
        throw new InvalidOperationException("Cannot start readonly mode, must be connected first");
      if (MsrmntState != MeasurementState.Stopped)
        throw new InvalidOperationException("Cannot start readonly mode while other test is running");
      //prepare the tx buffer with single zero frame
      mDevice.TxDataBuffer.Clear();
      mDevice.DAQClearVariables();
      mHighestID = 0;
      // Add more than one frame for initial test, sometimes a single frame gets lost somewhere.
      for (int j = 0; j < 20; j++)
      {
        UsbDAQTxData outPacket = new UsbDAQTxData();
        outPacket.Pwm = new byte[9];
        for (int i = 0; i < mTestScript.Gearbox.CurrentDisplayChannelsSet.ChannelsCount; i++)
        {
          DisplayChannel chan = mTestScript.Gearbox.CurrentDisplayChannelsSet.Channels[i];
          int chanPwmIndex = chan.InputChannelIndex;
          if (chanPwmIndex > 8)
            chanPwmIndex -= 9;
          outPacket.Pwm[chanPwmIndex] = 0;
        }
        outPacket.ID = 1;
        //Console.WriteLine("Added ID: " + outPacket.ID.ToString());
        mDevice.TxDataBuffer.Add(outPacket);
      }
      mDevice.SetDeviceDAQConfig((UInt32)mTestScript.Gearbox.PwmFrequencyHz, mTestScript.Gearbox.CurrentDisplayChannelsSet);
      //Start the transmission and wait for the operator to press the stop key
      mDevice.StartDAQTransmission();

      MsrmntState = MeasurementState.RunningReadonlyMode;

      if (MainTestStartedEvent != null)
        MainTestStartedEvent(this, EventArgs.Empty);
    }

    /// <summary>
    /// Starts the initial solenoid test
    /// </summary>
    public void StartInitialTest()
    {
      if (!mDevice.IsConnected)
        throw new InvalidOperationException("Cannot start initial test, must be connected first");
      if (MsrmntState != MeasurementState.Stopped)
        throw new InvalidOperationException("Cannot start analog outputs test while other test is running");
      //prepare the tx buffer with single frame (values taken from gearbox config channel setting)
      mDevice.TxDataBuffer.Clear();
      //mDevice.RxDataBuffer.Clear();
      mDevice.DAQClearVariables();
      mHighestID = 0;
      // Add more than one frame for initial test, sometimes a single frame gets lost somewhere.
      for (int j = 0; j < 20; j++)
      {
        UsbDAQTxData outPacket = new UsbDAQTxData();
        outPacket.Pwm = new byte[9];
        for (int i = 0; i < mTestScript.Gearbox.CurrentDisplayChannelsSet.ChannelsCount; i++)
        {
          DisplayChannel chan = mTestScript.Gearbox.CurrentDisplayChannelsSet.Channels[i];
          int chanPwmIndex = chan.InputChannelIndex;
          if (chanPwmIndex > 8)
            chanPwmIndex -= 9;
          outPacket.Pwm[chanPwmIndex] = (byte)chan.InitTestDutyCycle;
        }
        outPacket.ID = 1;
        //Console.WriteLine("Added ID: " + outPacket.ID.ToString());
        mDevice.TxDataBuffer.Add(outPacket);
      }
      mDevice.SetDeviceDAQConfig((UInt32)mTestScript.Gearbox.PwmFrequencyHz, mTestScript.Gearbox.CurrentDisplayChannelsSet);
      //Start the transmission and wait for the operator to press the stop key
      mDevice.StartDAQTransmission();

      if (mTestScript.Gearbox.ControllerType == GearboxControllerType.NISSAN_RE5)
      {
        mDevice.NissanRE5_Interface.InitializeTcu();
        mDevice.NissanRE5_Interface.EnableDrive();
      }

      MsrmntState = MeasurementState.RunningInitialTest;

      if (InitialTestStartedEvent != null)
        InitialTestStartedEvent(this, EventArgs.Empty);
    }

    public void StartLoopTest()
    {
      //if (!mDevice.IsConnected)
      //  throw new InvalidOperationException("Cannot start the main test, must be connected first!");

      //IsManualMode = false;
      //MsrmntState = MeasurementState.RunningLoopTest;

      //PrepareData();

      //if (MainTestStartedEvent != null)
      //  MainTestStartedEvent(this, EventArgs.Empty);

      if (!mDevice.IsConnected)
        throw new InvalidOperationException("Cannot start the main test, must be connected first!");

      //buffer MUST be loaded first
      MsrmntState = MeasurementState.RunningLoopTest;
      //PrepareLoop();
      PrepareData();
      if (MainTestStartedEvent != null)
        MainTestStartedEvent(this, EventArgs.Empty);
    }

    /// <summary>
    /// Starts the main measurement in manual mode
    /// </summary>
    public void StartManualTest()
    {
      if (!mDevice.IsConnected)
        throw new InvalidOperationException("Cannot start the main test, must be connected first!");

      MsrmntState = MeasurementState.RunningManualTest;

      PrepareData();

      if (MainTestStartedEvent != null)
        MainTestStartedEvent(this, EventArgs.Empty);
    }

    /// <summary>
    /// Stops the manual AOs test
    /// </summary>
    public unsafe void StopAOsTest()
    {
      if (MsrmntState == MeasurementState.RunningAOsTest)
      {
        // empty the transmit buffer
        mDevice.TxDataBuffer.Clear();
        // prepare a frame turning off all solenoids and AOs
        UsbDAQTxData outPacket = new UsbDAQTxData();
        outPacket.Pwm = new byte[9];
        for (int i = 0; i < 9; i++)
        {
          outPacket.Pwm[i] = 0;
        }
        outPacket.AO1 = 0;
        outPacket.AO2 = 0;
        mHighestID++;
        outPacket.ID = mHighestID;
        // Send the frame several times to ensure the outputs will be off
        mDevice.TxDataBuffer.Add(outPacket);
        mDevice.TxDataBuffer.Add(outPacket);
        mDevice.TxDataBuffer.Add(outPacket);
        mDevice.TxDataBuffer.Add(outPacket);
        mDevice.TxDataBuffer.Add(outPacket);
        // wait 100ms (after this time the frame MUST have been sent and applied)
        System.Threading.Thread.Sleep(101);
        // disable DAQ transmission with the device
        mDevice.StopDAQTransmission();
        // mark this test as not running
        MsrmntState = MeasurementState.Stopped;

        CleanUp();
      }
    }

    /// <summary>
    /// Stops the initial solenoid test
    /// </summary>
    public void StopInitialTest()
    {
      if (MsrmntState == MeasurementState.RunningInitialTest)
      //throw new InvalidOperationException("Cannot stop the initial test as it's not running!");
      {
        MsrmntState = MeasurementState.Stopped;
      }

      if (InitialTestFinishedEvent != null)
        InitialTestFinishedEvent(this, EventArgs.Empty);
    }

    /// <summary>
    /// Stops the main measurement
    /// </summary>
    public void StopMeasurement()
    {
      Console.WriteLine("StpMmt " + MsrmntState.ToString());
      switch (MsrmntState)
      {
        case MeasurementState.RunningInitialTest:
          {
            // empty the transmit buffer
            mDevice.TxDataBuffer.Clear();
            //mDevice.RxDataBuffer.Clear();
            DisableAllDrivers();
            // send a frame turning off all solenoids
            UsbDAQTxData outPacket = new UsbDAQTxData();
            outPacket.Pwm = new byte[9];
            for (int i = 0; i < 9; i++)
            {
              outPacket.Pwm[i] = 0;
            }
            mHighestID++;
            outPacket.ID = mHighestID;
            mDevice.TxDataBuffer.Add(outPacket);
            // wait 500ms (after this time the frame MUST have been sent)
            System.Threading.Thread.Sleep(501);

            StopInitialTest();
            // mark this test as not running
            MsrmntState = MeasurementState.Stopped;
            // fire msrmt test stopped event
            if (MeasurementStoppedEvent != null)
              MeasurementStoppedEvent(this, EventArgs.Empty);
            break;
          }
        case MeasurementState.RunningManualTest:
        case MeasurementState.RunningAutomaticTest:
        case MeasurementState.RunningReadonlyMode:
          {
            // mark this test as not running
            MsrmntState = MeasurementState.Stopped;
            // empty the transmit buffer
            mDevice.TxDataBuffer.Clear();
            //mDevice.RxDataBuffer.Clear();
            DisableAllDrivers();
            // send a frame turning off all solenoids
            UsbDAQTxData outPacket = new UsbDAQTxData();
            outPacket.Pwm = new byte[9];
            for (int i = 0; i < 9; i++)
            {
              outPacket.Pwm[i] = 0;
            }
            mHighestID++;
            outPacket.ID = mHighestID;
            mDevice.TxDataBuffer.Add(outPacket);
            // wait 500ms (after this time the frame MUST have been sent)
            System.Threading.Thread.Sleep(501);
            // disable DAQ transmission with the device
            mDevice.StopDAQTransmission();
            // Clear the buffers again
            mDevice.TxDataBuffer.Clear();

            Console.WriteLine("FRAMES SENT");

            switch (mTestScript.Gearbox.ControllerType)
            {
              case GearboxControllerType.ZF_6HPxx_1911E:
              case GearboxControllerType.ZF_6HPxx_1911M:
              case GearboxControllerType.ZF_6HPxx_CE:
              case GearboxControllerType.ZF_6HPxx_CM:
              case GearboxControllerType.ZF_6HPxx_TUCE:
              case GearboxControllerType.ZF_6HPxx_TUCM:
              case GearboxControllerType.ZF_6HPxx_WM:
                {
                  // If this is zf6 controlled gearbox
                  UsbDev_Decoder.DisableGearboxDrive();
                  break;
                }

              case GearboxControllerType.NISSAN_RE5:
                {
                  // If this is Nissan RE5 gearbox
                  _NissanRE5xxGov.DisableDrive();
                  break;
                }

              case GearboxControllerType.GM6T40:
              case GearboxControllerType.GM6T70:
              case GearboxControllerType.GM6L:
                {
                  // If this is Nissan RE5 gearbox
                  _GM6TxxGov.DisableDrive();
                  break;
                }
            }

            // Set the highest received ID to 0
            mHighestID = 0;
            // mark this test as not running
            MsrmntState = MeasurementState.Stopped;
            // fire main test stopped event
            if (MainTestStoppedEvent != null)
              MainTestStoppedEvent(this, EventArgs.Empty);
            if (MeasurementStoppedEvent != null)
              MeasurementStoppedEvent(this, EventArgs.Empty);
            break;
          }
        case MeasurementState.RunningLoopTest:
          {
            // empty the transmit buffer
            mDevice.TxDataBuffer.Clear();
            DisableAllDrivers();
            // send a frame turning off all solenoids
            UsbDAQTxData outPacket = new UsbDAQTxData();
            outPacket.Pwm = new byte[9];
            for (int i = 0; i < 9; i++)
            {
              outPacket.Pwm[i] = 0;
            }
            mHighestID++;
            outPacket.ID = mHighestID;
            mDevice.TxDataBuffer.Add(outPacket);
            // wait 500ms (after this time the frame MUST have been sent)
            System.Threading.Thread.Sleep(501);
            // disable DAQ transmission with the device
            mDevice.StopDAQTransmission();
            // Clear the buffers again
            mDevice.TxDataBuffer.Clear();
            //mDevice.RxDataBuffer.Clear();

            switch (mTestScript.Gearbox.ControllerType)
            {
              case GearboxControllerType.ZF_6HPxx_1911E:
              case GearboxControllerType.ZF_6HPxx_1911M:
              case GearboxControllerType.ZF_6HPxx_CE:
              case GearboxControllerType.ZF_6HPxx_CM:
              case GearboxControllerType.ZF_6HPxx_TUCE:
              case GearboxControllerType.ZF_6HPxx_TUCM:
              case GearboxControllerType.ZF_6HPxx_WM:
                {
                  // If this is zf6 controlled gearbox
                  UsbDev_Decoder.DisableGearboxDrive();
                  break;
                }

              case GearboxControllerType.NISSAN_RE5:
                {
                  // If this is Nissan RE5 gearbox
                  _NissanRE5xxGov.DisableDrive();
                  break;
                }

              case GearboxControllerType.GM6T40:
              case GearboxControllerType.GM6T70:
              case GearboxControllerType.GM6L:
                {
                  // If this is Nissan RE5 gearbox
                  _GM6TxxGov.DisableDrive();
                  break;
                }
            }

            // Set the highest received ID to 0
            mHighestID = 0;
            // mark this test as not running
            MsrmntState = MeasurementState.Stopped;
            // fire main test stopped event
            if (MainTestStoppedEvent != null)
              MainTestStoppedEvent(this, EventArgs.Empty);
            if (MeasurementStoppedEvent != null)
              MeasurementStoppedEvent(this, EventArgs.Empty);
            break;
          }
      }
    }

    /// <summary>
    /// Switches the gear down (basing on TestScript object and current script line)
    /// </summary>
    public void SwitchGearDown()
    {
      switch (mMode)
      {
        case MeasurementState.RunningManualTest:
          {
            // Switching gear down is only possible in manual mode
            if (mCurrScriptLineIndex == 0 || mTestScript.FrameSet.Count == 0) //if you currently are in the first script line
            {
              return; // return as this is not allowed
            }

            // Declare local variables
            int linesToPass = 0;
            int tempScriptLine = mCurrScriptLineIndex;
            DAQ_Event evt = new DAQ_Event();
            int gearChangeDuration = 0;
            int gearChangeReverseEquivalent = mTestScript.FrameSet[mCurrScriptLineIndex].Duration;


            switch (mTestScript.Gearbox.ControllerType)
            {
              case GearboxControllerType.ZF_6HPxx_1911E:
              case GearboxControllerType.ZF_6HPxx_1911M:
              case GearboxControllerType.ZF_6HPxx_CE:
              case GearboxControllerType.ZF_6HPxx_CM:
              case GearboxControllerType.ZF_6HPxx_TUCE:
              case GearboxControllerType.ZF_6HPxx_TUCM:
              case GearboxControllerType.ZF_6HPxx_WM:
                {
                  // If this is zf6 controlled gearbox
                  //float currGearVal = ((TestScriptFrame)mTestScript.FrameSet[tempScriptLine]).Zf6GearNumber;
                  //float nextGearVal = ((TestScriptFrame)mTestScript.FrameSet[tempScriptLine - 1]).Zf6GearNumber;
                  float currGearVal = ((TestScriptFrame)mTestScript.FrameSet[tempScriptLine]).EnigmaGearNumber;
                  float nextGearVal = ((TestScriptFrame)mTestScript.FrameSet[tempScriptLine - 1]).EnigmaGearNumber;

                  if (nextGearVal > currGearVal)
                  {
                    UsbDev_Decoder.SelectGear((byte)(Math.Ceiling(nextGearVal)));
                  }
                  if (nextGearVal <= currGearVal)
                  {
                    UsbDev_Decoder.SelectGear((byte)(Math.Floor(nextGearVal)));
                  }
                  break;
                }

              case GearboxControllerType.NISSAN_RE5:
                {
                  // If this is Nissan RE5 gearbox
                  // TODO Do sth for Nissan!
                  break;
                }

              case GearboxControllerType.GM6T40:
              case GearboxControllerType.GM6T70:
              case GearboxControllerType.GM6L:
                {
                  // If this is zf6 controlled gearbox
                  //float currGearVal = ((TestScriptFrame)mTestScript.FrameSet[tempScriptLine]).Zf6GearNumber;
                  //float nextGearVal = ((TestScriptFrame)mTestScript.FrameSet[tempScriptLine - 1]).Zf6GearNumber;
                  float currGearVal = ((TestScriptFrame)mTestScript.FrameSet[tempScriptLine]).EnigmaGearNumber;
                  float nextGearVal = ((TestScriptFrame)mTestScript.FrameSet[tempScriptLine - 1]).EnigmaGearNumber;

                  if (nextGearVal > currGearVal)
                  {
                    _GM6TxxGov.SelectGear((byte)(Math.Ceiling(nextGearVal)));
                  }
                  if (nextGearVal < currGearVal)
                  {
                    _GM6TxxGov.SelectGear((byte)(Math.Floor(nextGearVal)));
                  }
                  break;
                }
            }

              // Determine the number of lines to pass and a total duration
              while (tempScriptLine > 0)
              {
                tempScriptLine--;
                linesToPass++;
                TestScriptFrame frame = (TestScriptFrame)mTestScript.FrameSet[tempScriptLine];
                gearChangeDuration += frame.Duration;
                if (frame.IsPassThrough == false)
                {
                  break;
                }
                else
                {
                  gearChangeReverseEquivalent += frame.Duration;
                }
              }

              // Add a gear lock and progress bar feedback for GUI
              ProgressBarEventAttributes fbattr = new ProgressBarEventAttributes();
              fbattr.mEventType = ProgressBarEventAttributes.EventType.ResumeCounting;
              fbattr.mMinorProgressBarEndMs = gearChangeDuration;
              fbattr.mMinorProgressBarLabel = "Gear change progress:";
              fbattr.mMinorCountForMs = gearChangeDuration;
              fbattr.mMajorCountForMs = -gearChangeReverseEquivalent;
              evt = new DAQ_Event();
              evt.mCriticalID = mHighestID + 1;
              evt.mScriptLine = mTestScript.FrameSet[mCurrScriptLineIndex - 1];
              evt.mAcquireData = false;
              evt.mIsGearChange = false;
              evt.mEndOfTheScript = false;
              evt.mDisplayPrompt = false;
              evt.mGearLockOn = true;
              evt.mProgressBarFb = true;
              evt.mProgressBarFbAttr = fbattr;
              _EventManager.AddEvent(evt);

              // Proceed each line to add
              tempScriptLine = mCurrScriptLineIndex - 1;
              for (int i = 0; i < linesToPass; i++)
              {
                TestScriptFrame frame = (TestScriptFrame)mTestScript.FrameSet[tempScriptLine - i];

                // prepare GUI feedback for this frame
                evt = new DAQ_Event();
                evt.mCriticalID = mHighestID + 1;
                evt.mScriptLine = frame;
                evt.mAcquireData = false;
                evt.mIsGearChange = true;
                evt.mEndOfTheScript = false;
                evt.mDisplayPrompt = false;
                _EventManager.AddEvent(evt);

                GST.Gearshift.Components.Utilities.Settings sett = GST.Gearshift.Components.Utilities.Settings.Instance;
                for (int j = 0; j < frame.Duration / mDevice.mSampleIntervalMs; j++)
                {
                  //prepare that many 10ms frames so that their duration will equal original frame duration
                  UsbDAQTxData outPacket = new UsbDAQTxData();
                  outPacket.Pwm = new byte[9];
                  for (int k = 0; k < 9; k++)
                  {
                    outPacket.Pwm[k] = (byte)(int)frame.ChannelDrives[k];
                  }
                  outPacket.AO1 = (UInt16)((float)frame.DynoMotorRPM * sett.RPM_AO_multiplier);//((frame.AnalogOutput1Value * 1024) / 10);
                  outPacket.AO2 = (UInt16)((float)frame.DynoLoadCurrent * sett.Load_AO_multiplier);//((frame.AnalogOutput2Value * 1024) / 10);
                  mHighestID++;
                  outPacket.ID = mHighestID;

                  mDevice.TxDataBuffer.Add(outPacket);
                }

                if (frame.AcquireData || frame.UserPromptEnabled || (i == linesToPass - 1))
                {
                  // prepare acquisition / user prompt / gear lock off criticalID for this frame
                  evt = new DAQ_Event();
                  evt.mCriticalID = mHighestID;
                  evt.mScriptLine = frame;
                  evt.mAcquireData = frame.AcquireData;
                  evt.mIsGearChange = false;
                  evt.mDisplayPrompt = frame.UserPromptEnabled;
                  evt.mGearLockOff = true;
                  _EventManager.AddEvent(evt);
                }
              }//for
              break;
          }
      }
    }

    /// <summary>
    /// Switches the gear up (basing on TestScript object and current script line)
    /// </summary>
    public void SwitchGearUp()
    {
      switch (mMode)
      {
        case MeasurementState.RunningManualTest:
        case MeasurementState.RunningAutomaticTest:
        case MeasurementState.RunningLoopTest:
          {
            // Detect if you are in the loop block end in loop mode
            if (mMode == MeasurementState.RunningLoopTest && mCurrScriptLineIndex == _loopBlockScriptLineEnd)
            {
              // If yes - go to one line before the loop start.
              // The switching will begin from the next line (loop start)
              mCurrScriptLineIndex = _loopBlockScriptLineStart - 1;
            }

            if (mCurrScriptLineIndex == mTestScript.FrameSet.Count - 1 || mTestScript.FrameSet.Count == 0) //if you currently are in the last script line
            {
              return; // return as this is not allowed
            }

            // Declare local variables
            int linesToPass = 0;
            int tempScriptLine = mCurrScriptLineIndex;
            DAQ_Event evt = new DAQ_Event();
            int gearChangeDuration = 0;

            // POSSIBLE COCKUP !

            switch (mTestScript.Gearbox.ControllerType)
            {
              case GearboxControllerType.ZF_6HPxx_1911E:
              case GearboxControllerType.ZF_6HPxx_1911M:
              case GearboxControllerType.ZF_6HPxx_CE:
              case GearboxControllerType.ZF_6HPxx_CM:
              case GearboxControllerType.ZF_6HPxx_TUCE:
              case GearboxControllerType.ZF_6HPxx_TUCM:
              case GearboxControllerType.ZF_6HPxx_WM:
                {
                  // If this is zf6 controlled gearbox
                  //float currGearVal = ((TestScriptFrame)mTestScript.FrameSet[tempScriptLine]).Zf6GearNumber;
                  //float nextGearVal = ((TestScriptFrame)mTestScript.FrameSet[tempScriptLine + 1]).Zf6GearNumber;
                  float currGearVal = ((TestScriptFrame)mTestScript.FrameSet[tempScriptLine]).EnigmaGearNumber;
                  float nextGearVal = ((TestScriptFrame)mTestScript.FrameSet[tempScriptLine + 1]).EnigmaGearNumber;
                  if (nextGearVal > currGearVal)
                  {
                    UsbDev_Decoder.SelectGear((byte)(Math.Ceiling(nextGearVal + 1)));
                  }
                  if (nextGearVal < currGearVal)
                  {
                    UsbDev_Decoder.SelectGear((byte)(Math.Floor(nextGearVal + 1)));
                  }
                  break;
                }

              case GearboxControllerType.NISSAN_RE5:
                {
                  // If this is Nissan RE5 gearbox
                  // TODO Do sth for Nissan!
                  break;
                }

              case GearboxControllerType.GM6T40:
              case GearboxControllerType.GM6T70:
              case GearboxControllerType.GM6L:
                {
                  // If this is zf6 controlled gearbox
                  //float currGearVal = ((TestScriptFrame)mTestScript.FrameSet[tempScriptLine]).Zf6GearNumber;
                  //float nextGearVal = ((TestScriptFrame)mTestScript.FrameSet[tempScriptLine + 1]).Zf6GearNumber;
                  float currGearVal = ((TestScriptFrame)mTestScript.FrameSet[tempScriptLine]).EnigmaGearNumber;
                  float nextGearVal = ((TestScriptFrame)mTestScript.FrameSet[tempScriptLine + 1]).EnigmaGearNumber;
                  if (nextGearVal > currGearVal)
                  {
                    _GM6TxxGov.SelectGear((byte)(Math.Ceiling(nextGearVal)));
                  }
                  if (nextGearVal <= currGearVal)
                  {
                    _GM6TxxGov.SelectGear((byte)(Math.Floor(nextGearVal)));
                  }
                  break;
                }
            }

            // Determine the number of lines to pass and a total duration
            while (true)
            {
              tempScriptLine++;
              linesToPass++;
              TestScriptFrame frame = (TestScriptFrame)mTestScript.FrameSet[tempScriptLine];
              gearChangeDuration += frame.Duration;
              if (frame.IsPassThrough == false)
              {
                // Check if the last frame in the block is the first loop mode frame
                if (frame.IsPartOfTheLoop && mMode == MeasurementState.RunningLoopTest && linesToPass > 1)
                {
                  // If so, in the loop mode do not play it.
                  linesToPass--;
                  gearChangeDuration -= frame.Duration;
                }
                break;
              }
            }

            if (mMode == MeasurementState.RunningLoopTest && mCurrScriptLineIndex == _loopBlockScriptLineStart - 1)
            {
              // If this is the first line in a loop block - add a proper major progressbar feedback
              // Add the progress bar feedback critical ID
              ProgressBarEventAttributes fbattr = new ProgressBarEventAttributes();
              fbattr.mEventType = ProgressBarEventAttributes.EventType.StartCounting;
              fbattr.mMajorProgressBarLabel = "Loop progress:";
              fbattr.mMajorProgressBarEndMs = _loopBlockLengthMs;
              fbattr.mMinorProgressBarEndMs = gearChangeDuration;
              fbattr.mMinorProgressBarLabel = "Gear change progress:";
              fbattr.mMajorCountForMs = gearChangeDuration;
              fbattr.mMinorCountForMs = gearChangeDuration;

              evt = new DAQ_Event();
              evt.mCriticalID = mHighestID + 1; // +1 because the data hasnt been yet created and added to device buffer
              evt.mScriptLine = mTestScript.FrameSet[0];
              evt.mAcquireData = false; // Do not acquire data on the first frame in the script
              evt.mIsGearChange = true; // Provide immediate gear change feedback at the beginning of the script
              evt.mDisplayPrompt = false;
              evt.mEndOfTheScript = false;
              evt.mGearLockOn = true;
              evt.mProgressBarFbAttr = fbattr;
              evt.mProgressBarFb = true;
              _EventManager.AddEvent(evt);

            }
            else
            {
              // Add a gear lock and progress bar feedback for GUI
              ProgressBarEventAttributes fbattr = new ProgressBarEventAttributes();
              fbattr.mEventType = ProgressBarEventAttributes.EventType.ResumeCounting;
              fbattr.mMinorProgressBarEndMs = gearChangeDuration;
              fbattr.mMinorProgressBarLabel = "Gear change progress:";
              fbattr.mMinorCountForMs = gearChangeDuration;
              fbattr.mMajorCountForMs = gearChangeDuration;
              evt = new DAQ_Event();
              evt.mCriticalID = mHighestID + 1;
              evt.mScriptLine = mTestScript.FrameSet[mCurrScriptLineIndex + 1];
              evt.mAcquireData = false;
              evt.mIsGearChange = false;
              evt.mEndOfTheScript = false;
              evt.mDisplayPrompt = false;
              evt.mGearLockOn = true;
              evt.mProgressBarFb = true;
              evt.mProgressBarFbAttr = fbattr;
              _EventManager.AddEvent(evt);
            }

            // Proceed each line to add
            tempScriptLine = mCurrScriptLineIndex + 1;
            for (int i = 0; i < linesToPass; i++)
            {
              TestScriptFrame frame = (TestScriptFrame)mTestScript.FrameSet[tempScriptLine + i];

              // prepare GUI feedback for this frame
              evt = new DAQ_Event();
              evt.mCriticalID = mHighestID + 1;
              evt.mScriptLine = frame;
              evt.mAcquireData = false;
              evt.mIsGearChange = true;
              evt.mEndOfTheScript = false;
              evt.mDisplayPrompt = false;
              _EventManager.AddEvent(evt);


              GST.Gearshift.Components.Utilities.Settings sett = GST.Gearshift.Components.Utilities.Settings.Instance;
              for (int j = 0; j < frame.Duration / mDevice.mSampleIntervalMs; j++)
              {
                //prepare that many 10ms frames so that their duration will equal original frame duration
                UsbDAQTxData outPacket = new UsbDAQTxData();
                outPacket.Pwm = new byte[9];
                for (int k = 0; k < 9; k++)
                {
                  outPacket.Pwm[k] = (byte)(int)frame.ChannelDrives[k];
                }
                outPacket.AO1 = (UInt16)((float)frame.DynoMotorRPM * sett.RPM_AO_multiplier);//((frame.AnalogOutput1Value * 1024) / 10);
                outPacket.AO2 = (UInt16)((float)frame.DynoLoadCurrent * sett.Load_AO_multiplier);//((frame.AnalogOutput2Value * 1024) / 10);
                mHighestID++;
                outPacket.ID = mHighestID;

                mDevice.TxDataBuffer.Add(outPacket);
              }

              if (frame.AcquireData)
              {
                evt = new DAQ_Event();
                evt.mCriticalID = mHighestID;
                evt.mScriptLine = frame;
                evt.mAcquireData = frame.AcquireData;
                evt.mIsGearChange = false;
                evt.mDisplayPrompt = false;
                evt.mGearLockOff = false;
                _EventManager.AddEvent(evt);
              }

              // In the last line of current data block: unlock the gear change buttons and check if gear is to be switched and prompt displayed
              // This is a solid gear (non pass-through)
              if (i == linesToPass - 1)
              {
                evt = new DAQ_Event();
                evt.mCriticalID = mHighestID;
                evt.mScriptLine = frame;
                evt.mAcquireData = false;
                evt.mIsGearChange = false;
                evt.mDisplayPrompt = frame.UserPromptEnabled;
                evt.mGearLockOff = true; // Unlock gear button
                switch (MsrmntState)
                {
                  case MeasurementState.RunningAutomaticTest:
                    {
                      // If this is the end of the script - add proper event.
                      if (tempScriptLine + i == (mTestScript.FrameSet.Count - 1))
                      {
                        evt.mEndOfTheScript = true;
                      }
                      // If user prompt is enabled then the gear must be switched as the user accepts prompt.
                      // Otherwise switch it up
                      if (!frame.UserPromptEnabled)
                      {
                        evt.mSwitchGearUp = true;
                      }
                      break;
                    }
                  // SORT THE LOOP MODE!!!!
                  case MeasurementState.RunningLoopTest:
                    {
                      // If user prompt is enabled then the gear must be switched as the user accepts prompt.
                      // Otherwise switch it up
                      if (!frame.UserPromptEnabled)
                      {
                        evt.mSwitchGearUp = true;
                      }
                      break;
                    }
                }

                _EventManager.AddEvent(evt);
              }

            }//for
            break;
          }

        //case MeasurementState.RunningLoopTest:
        //  {
        //    break;
        //  }
      }

    }

    /// <summary>
    /// Prepares the measurement data set
    /// basing on the TestScript object and measurement mode
    /// </summary>
    private void PrepareData()
    {
      if (mTestScript.FrameSet.Count == 0)
        return;//disallowed state

      mDevice.TxDataBuffer.Clear();

      //Prepare only the first script frame and put it into the device Tx buffer
      mDevice.TxDataBuffer.Clear();
      // set the initial ID for the transmission to 10 (1 is reserved for initial script)
      mHighestID = 10;

      // Wipe clean the event manager data 
      _EventManager.ClearManagerData();

      // Temporary event instance
      DAQ_Event evt = new DAQ_Event();

      // Only in Loop mode additional data preparation is required
      if (MsrmntState == MeasurementState.RunningLoopTest)
      {
        // Calculate where loop starts, ends and what's the block length
        bool loopReachedYet = false;
        _loopBlockScriptLineStart = 0;
          _loopBlockLengthMs = 0;
          _loopBlockScriptLineEnd = 0;
        for (int i = 0; i < mTestScript.FrameSet.Count; i++)
        {
          if (mTestScript.FrameSet[i].IsPartOfTheLoop && !loopReachedYet)
          {
            loopReachedYet = true;
            _loopBlockScriptLineStart = i;
          }
          if (loopReachedYet)
          {
            if (mTestScript.FrameSet[i].IsPartOfTheLoop)
            {
              _loopBlockLengthMs += mTestScript.FrameSet[i].Duration;
            }
            else
            {
              _loopBlockScriptLineEnd = i - 1;
              break;
            }
          }        
        }
        // Calculate the pre-loop block start, end and length
        _preLoopBlockScriptLineEnd = 0;
        _preLoopBlockLengthMs = 0;
        for (int i = 0; i < mTestScript.FrameSet.Count; i++)
        {
          if (mTestScript.FrameSet[i].IsPartOfTheLoop)
          {
            // Once the first loop frame is detected the 'for' must be quit
            _preLoopBlockScriptLineEnd = i - 1;
            break;
          }
          else
          {
            _preLoopBlockLengthMs += mTestScript.FrameSet[i].Duration;
          }
        }
      }

      // Figure out the length of the major progress bar
      Int32 majorProgressBarLength = 0;
      switch (MsrmntState)
      {
        case MeasurementState.RunningManualTest:
        case MeasurementState.RunningAutomaticTest:
          {
            // In manual and automatic mode a major progress bar length is simply a sum of all lines lengths
            for (int i = 0; i < mTestScript.FrameSet.Count; i++)
            {
              majorProgressBarLength += mTestScript.FrameSet[i].Duration;
            }
            break;
          }
        case MeasurementState.RunningLoopTest:
          {
            // Set the pre-loop block length as a major progressbar length
            // A proper loop length feedback will be set at the end of a PLB when a Reloop() function is called
            majorProgressBarLength = _preLoopBlockLengthMs;
            break;
          }
      }

      // Process only the first line of the test script
      // Rest of the lines will be played by using the _EventManager feedbacks depending on manual/auto/loop mode
      TestScriptFrame frame = (TestScriptFrame)mTestScript.FrameSet[0];
      int neededFramesCount = frame.Duration / mDevice.mSampleIntervalMs;

      // Add the progress bar feedback critical ID
      ProgressBarEventAttributes fbattr = new ProgressBarEventAttributes();
      fbattr.mEventType = ProgressBarEventAttributes.EventType.StartCounting;
      if (MsrmntState == MeasurementState.RunningLoopTest)
      {
        // Add a pre-loop block feedback name.
        fbattr.mMajorProgressBarLabel = "Pre-loop block progress:";
      }
      else
      {
        fbattr.mMajorProgressBarLabel = "Total test progress:";
      }
      fbattr.mMajorProgressBarEndMs = majorProgressBarLength;
      fbattr.mMinorProgressBarEndMs = frame.Duration;
      fbattr.mMinorProgressBarLabel = "Gear change progress:";
      fbattr.mMajorCountForMs = frame.Duration;
      fbattr.mMinorCountForMs = frame.Duration;

      evt = new DAQ_Event();
      evt.mCriticalID = mHighestID + 1; // +1 because the data hasnt been yet created and added to device buffer
      evt.mScriptLine = mTestScript.FrameSet[0];
      evt.mAcquireData = false; // Do not acquire data on the first frame in the script
      evt.mIsGearChange = true; // Provide immediate gear change feedback at the beginning of the script
      evt.mDisplayPrompt = false;
      evt.mEndOfTheScript = false;
      evt.mGearLockOn = true;
      evt.mProgressBarFbAttr = fbattr;
      evt.mProgressBarFb = true;
      _EventManager.AddEvent(evt);
      
      GST.Gearshift.Components.Utilities.Settings sett = GST.Gearshift.Components.Utilities.Settings.Instance;

      // prepare that many 10ms frames so that their duration will equal original frame duration
      for (int j = 0; j < neededFramesCount; j++)
      {
        UsbDAQTxData outPacket = new UsbDAQTxData();
        outPacket.Pwm = new byte[9];
        for (int k = 0; k < 9; k++)
        {
          outPacket.Pwm[k] = (byte)(int)frame.ChannelDrives[k];
        }
        outPacket.AO1 = (UInt16)((float)frame.DynoMotorRPM * sett.RPM_AO_multiplier);
        outPacket.AO2 = (UInt16)((float)frame.DynoLoadCurrent * sett.Load_AO_multiplier);
        mHighestID++;
        outPacket.ID = (UInt32)mHighestID;
        lock (mDevice)
          mDevice.TxDataBuffer.Add(outPacket);
      }

      // prepare event at the end of the line
      evt = new DAQ_Event();
      evt.mCriticalID = mHighestID;
      evt.mScriptLine = frame;
      evt.mAcquireData = frame.AcquireData;
      evt.mIsGearChange = false;
      evt.mDisplayPrompt = frame.UserPromptEnabled;

      switch (MsrmntState)
      {
        case MeasurementState.RunningManualTest:
          {
            // In manual mode this is the moment when a gear lock is off (gear up/down buttons will be enabled)
            evt.mGearLockOff = true;
            break;
          }
        case MeasurementState.RunningAutomaticTest:
          {
            // If this is an automatic mode, a next gear transition should be executed at this moment, 
            // as long as there's no user prompt to be displayed. Otherwise the next gear transition will begin after prompt will be accepted
            if (!frame.UserPromptEnabled && mTestScript.FrameSet.Count > 1)
            {
              evt.mSwitchGearUp = true;
            }
            break;
          }
        case MeasurementState.RunningLoopTest:
          {
            // If this is a loop mode, a next gear transition should be executed at this moment, assuming we're in a pre-loop block. If the next line is a part of the loop, the reloop should be called.
            // as long as there's no user prompt to be displayed. Otherwise the next gear transition will begin after prompt will be accepted
            // This is very unlikely to happen, but it is possible that the script will be one line in a loop mode
            // If this is the case, a reloop should be called after prompt is accepted
            if (mTestScript.FrameSet.Count > 1)
            {
                // Switch gear up, SwitchGearUp() will properly handle the loop beginning/end
                evt.mSwitchGearUp = true;
            }
            break;
          }
      }
      _EventManager.AddEvent(evt);
    }

    /// <summary>
    /// Clears all buffers and prepared data
    /// </summary>
    private void CleanUp()
    {
      if (MsrmntState != MeasurementState.Stopped)
        mDevice.StopDAQTransmission();
      mCurrentTestFailed = false;
      //mDevice.RxDataBuffer.Clear();
      mDevice.TxDataBuffer.Clear();
      _EventManager.ClearManagerData();
    }


    void _EventManager_EventFired(DAQ_Event evt, GearShiftUsb.DeviceAcquiredData data)
    {
      // Save data to report if required
      if (evt.mAcquireData)
      {

            TestScriptFrame repFrame = evt.mScriptLine;
            // Assign pressure values
            List<GearShiftUsb.AIChannel> aics = mTestScript.Gearbox._analogueInputs;
            
            for (int j = 0; j < aics.Count; j++)
            {
              repFrame.PressureReadValues[j] = data.pressures[j];
            }
            // Assign current values
            DisplayChannelsSet cChannels = mTestScript.Gearbox.CurrentDisplayChannelsSet;
            for (int j = 0; j < cChannels.ChannelsCount; j++)
            {
              repFrame.CurrentReadValues[j] = data.currents[j];
            }
            //Console.WriteLine("ACK " + aics.Count.ToString() + "PRESS & " + cChannels.ChannelsCount.ToString());
      }

      switch (mTestScript.Gearbox.ControllerType)
      {
        case GearboxControllerType.ZF_6HPxx_1911E:
        case GearboxControllerType.ZF_6HPxx_1911M:
        case GearboxControllerType.ZF_6HPxx_CE:
        case GearboxControllerType.ZF_6HPxx_CM:
        case GearboxControllerType.ZF_6HPxx_TUCE:
        case GearboxControllerType.ZF_6HPxx_TUCM:
        case GearboxControllerType.ZF_6HPxx_WM:
          {
            // If this is zf6 controlled gearbox
            if (evt.mScriptLine != null) // To make sure it doesnt crash
            {
              _usbDev_Decoder.SetEDS5Value(evt.mScriptLine.EnigmaEDS5Value);
              _usbDev_Decoder.SetEDS6Value(evt.mScriptLine.EnigmaEDS6Value);
            }
            break;
          }

        case GearboxControllerType.NISSAN_RE5:
          {
            // If this is Nissan RE5 gearbox
            // TODO Do sth for Nissan!
            mDevice.NissanRE5_Interface.SetChannelDriveValues(evt.mScriptLine.ChannelDrives);
            break;
          }
        case GearboxControllerType.GM6T40:
        case GearboxControllerType.GM6T70:
        case GearboxControllerType.GM6L:
          {
            if (evt.mScriptLine.EnigmaEDS6Value > 0)
            {
              _GM6TxxGov.SetTccOn();
            }
            else
            {
              _GM6TxxGov.SetTccOff();
            }
            _GM6TxxGov.SimData_SetMultipleValues(evt.mScriptLine.CANBUS_EngineSpeedValue, evt.mScriptLine.CANBUS_TPS, evt.mScriptLine.CANBUS_TorqueValue, evt.mScriptLine.SSEMU_InputSpeedValue, evt.mScriptLine.SSEMU_OutputSpeedValue);
            break;
          }
      }

      if (evt.mGearLockOn && GearLockOn != null)
      {
        GearLockOn(this, EventArgs.Empty);
      }

      // Unlock the gear change buttons only in manual mode
      if (evt.mGearLockOff && GearLockOff != null && mMode == MeasurementState.RunningManualTest)
      {
        GearLockOff(this, EventArgs.Empty);
      }

      if (evt.mProgressBarFb && ProgressBarFeedbackEvent != null)
      {
        ProgressBarFeedbackEvent(evt.mProgressBarFbAttr);
      }

      // check if passed object is correct
      if (evt.mScriptLine == null)
      {
        return;
      }

      mCurrScriptLineIndex = evt.mScriptLine.FrameIndex;
      mCurrGear = evt.mScriptLine.FrameName;

      // End the test only if the script is ran in auto mode
      if (evt.mEndOfTheScript && mMode == MeasurementState.RunningAutomaticTest)
      {
        // in automatic mode it means the measurement is finished.
        mCurrScriptLineIndex = 0;
        mCurrGear = string.Empty;

        if (GearSwitchedEvent != null)
        {
          GearSwitchedEvent(evt.mScriptLine);
        }

        StopMeasurement();
      }

      if (evt.mIsGearChange) // if this is gear change moment
      {
        if (GearSwitchedEvent != null)
        {
          GearSwitchedEvent(evt.mScriptLine);
        }
      }

      if (evt.mDisplayPrompt && mMode != MeasurementState.RunningLoopTest) // if prompt should be displayed
      {
        if (DisplayPromptEvent != null)
        {
          DisplayPromptEvent(((TestScriptFrame)mTestScript.FrameSet[mCurrScriptLineIndex]).UserPrompt, EventArgs.Empty);
        }
      }

      if (evt.mSwitchGearUp)
      {
        SwitchGearUp();
      }

    }

    private void DAQManualDriveStatusChanged(object source, EventArgs e)
    {
      DisplayChannel chan = source as DisplayChannel;
      if (IsRunning)
      {
        switch (mTestScript.Gearbox.ControllerType)
        {
          default:
          case GearboxControllerType.NON_MECHATRONIC:
            {
              // If this is a DAQ GearShift controlled gearbox
          if (chan.IsSliderControlled)
          {
            mDevice.DAQManualDriveEnable((byte)chan.InputChannelIndex, (byte)chan.SliderControlValue);
          }
          else
          {
            mDevice.DAQManualDriveDisable((byte)chan.InputChannelIndex);
          }
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
              if (chan.InputChannelIndex == 5)
              {
                _usbDev_Decoder.SetEDS5Value(chan.SliderControlValue);
              }
              if (chan.InputChannelIndex == 6)
              {
                _usbDev_Decoder.SetEDS6Value(chan.SliderControlValue);
              }
              break;
            }

          case GearboxControllerType.NISSAN_RE5:
            {
              // If this is Nissan RE5 gearbox
              if (chan.InputChannelIndex < 7)
              {
                mDevice.NissanRE5_Interface.SetChannelDriveValue(chan.InputChannelIndex, chan.SliderControlValue);
              }
              break;
            }

          case GearboxControllerType.GM6T40:
          case GearboxControllerType.GM6T70:
          case GearboxControllerType.GM6L:
            {
              // Manual drive not possible for GM 6T transmissions
              break;
            }
        }
      }
    }

    private void DAQManualDriveValueChanged(object source, EventArgs e)
    {
      DisplayChannel chan = source as DisplayChannel;
      if (IsRunning)
      {
        switch (mTestScript.Gearbox.ControllerType)
        {
          default:
          case GearboxControllerType.NON_MECHATRONIC:
            {
              // If this is an analogue gearbox
              mDevice.DAQManualDriveSetValue((byte)chan.InputChannelIndex, (byte)chan.SliderControlValue);
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
              // Only channels 5 and 6 can be manually controlled
              if (chan.InputChannelIndex == 5)
              {
                _usbDev_Decoder.SetEDS5Value(chan.SliderControlValue);
              }
              if (chan.InputChannelIndex == 6)
              {
                _usbDev_Decoder.SetEDS6Value(chan.SliderControlValue);
              }
              break;
            }

          case GearboxControllerType.NISSAN_RE5:
            {
              // If this is Nissan RE5 gearbox
              if (chan.InputChannelIndex < 7)
              {
                mDevice.NissanRE5_Interface.SetChannelDriveValue(chan.InputChannelIndex, chan.SliderControlValue);
              }
              break;
            }

          case GearboxControllerType.GM6T40:
          case GearboxControllerType.GM6T70:
          case GearboxControllerType.GM6L:
            {
              // Manual drive not possible for GM 6T transmissions
              break;
            }
        }
      }
    }

    void mDevice_DAQDataReceivedEvent(GearShiftUsb.DeviceAcquiredData data)
    {
      // Dump the data to file
      //String dump = "Resp: " + data.responseToID.ToString() + " ID: " + data.ID.ToString() + System.Environment.NewLine;
      //System.IO.File.AppendAllText("!OutputDump.txt", dump);

      // Calculate gear ratio
      _grc.CalculateRatios(data);

      // Check if there's any event to fire:
      _EventManager.ProcessNewData(data);
      switch (MsrmntState)
      {
        case MeasurementState.RunningAutomaticTest:
        case MeasurementState.RunningLoopTest:
        case MeasurementState.RunningManualTest:
          {
            data.gearName = mCurrGear;
            mReport.LivePlaybackData.Add(new TestScriptReport.LivePlaybackFrame((int)data.responseToID, data.pressures));
            break;
          }
      }
    }

    void mDevice_DeviceDisconnecedEvent(object sender, EventArgs e)
    {
      if (IsRunning)
      {
        Soko.Common.Forms.MessageBox.ShowCritical("GearShift",
                                                     "Hardware error",
                                                     "Lost connection with the device during the test. Make sure your power supply has sufficient current rating",
                                                     Soko.Common.Forms.MessageBoxButtons.OK);
        StopMeasurement();
      }
    }

    #endregion Methods


  }
}
