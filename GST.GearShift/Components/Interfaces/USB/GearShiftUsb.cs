using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;
using System.IO;
using System.Windows.Forms;


using System.ComponentModel;

using GST.Gearshift.Components.Utilities;
using Soko.Common.Common;


namespace GST.Gearshift.Components.Interfaces.USB
{

  /// <remarks>
  /// Physical device interface
  /// </remarks>
  unsafe public partial class GearShiftUsb
  {
    /// <remarks>
    /// PLEASE NOTE:
    /// Device connection state is kept in a separate variables and is triggered on Connect/Disconnect function calls,
    /// otherwise it would cause problems when multiple GearboxTool objects are disposed and one is still to exist and function
    /// </remarks>

    public class DeviceAcquiredData
    {
      public UInt32 ID;
      public UInt32 responseToID;
      public float[] currents = new float[18];
      public float[] pressures = new float[14];
      public string gearName = string.Empty;
    }

    #region Constants

    // Maximum allowed Analogue Input channels
    public static readonly int AIChannelsMaxCount = 14;

    // Nominal DAQ sampling interval
    public readonly int mSampleIntervalMs = 10;
    // Defines how many times slower the USB communication frequency is than device sampling rate
    public readonly int mSamplingRateDivider = 10;
    // Defines the maximum number of retries when connecting to the usb device
    public readonly int mConnectRetriesMaxCount = 10;

    // Pressure ADC gain
    private readonly float mPressureADCGain = 0.010509641873278236914600550964187f;
    // Current ADC gain
    private readonly float mCurrentADCGain = 0.00319574468085106382978723404255f;
    // Old value = 0.0047809278350515463917525773195876f;

    // Packet transmission interval for ODB functionality
    private readonly int mOBDUsbCommInterval = 10;

    #endregion  Constants


    #region Private fields

    DeviceAcquiredData _daqLatestReceivedData = new DeviceAcquiredData();

    // Device general variables //---------------------------------------------------------------------//
    //-------------------------------------------------------------------------------------------------//

    // Device state code
    private UInt32 mDevState = 0;
    // Communication error code
    private UInt32 mDevCommErrCode = 0;
    // Returned overcurrent mask
    private UInt32 mDevOvercurrentMask = 0;
    // If the device is connected
    private bool mIsConnected = false;
    // Device state checking timer
    private System.Timers.Timer mDevStateTimer = null;
    // Decoder USB interface
    private GST.ZF6.Components.Interfaces.MechShifterUSB.UsbDevice _usbDev_Decoder = null;
    // Nissan RE5 TCU governor
    // MUST BE ASSIGNED BEFORE USE!!!
    private Soko.CanCave.Components.Interfaces.TCUGovernor_NissanRE5 _NissanRE5_Interface = null;


    // DAQ buffers and variables //--------------------------------------------------------------------//
    //-------------------------------------------------------------------------------------------------//

    // Timer to serve DAQ communication with the device
    private System.Timers.Timer mDAQRxTxTimer = null;

    // Buffer with DAQ data to transmit (emptied on timer tick)
    private List<UsbDAQTxData> mDAQTxDataBuffer = null;
    // Fixed size buffer with DAQ transmit structures passed to the device driver
    private UsbDAQTxData[] mDAQHwTxBuff = new UsbDAQTxData[0];
    // Fixed size buffer with DAQ receive structures passed to the device driver
    private UsbDAQRxData[] mDAQHwRxBuff = new UsbDAQRxData[0];

    // Device driver DAQ receive buffer size ( receive FROM this module )
    private UInt32 mDAQDevRxBuffSize = 0;
    // Device driver DAQ transmit buffer size ( send TO this module )
    private UInt32 mDAQDevTxBuffSize = 0;

    // Indices of current channels to have the current measured
    private byte[] currReadChannsIndices = new byte[9];
    // Number of current channels to read
    private byte currReadChannsCount = 0;

    // The .inResponseToId field of the latest received frame
    private UInt32 mLastResponsedFrameID = 0;

    // Test script to play
    private TestScript mTestScript = new TestScript();

    // OBD buffers and variables //--------------------------------------------------------------------//
    //-------------------------------------------------------------------------------------------------//

    // Timer to serve OBD communication with the device
    private System.Timers.Timer mOBDRxTxTimer = null;

    // Buffer with OBD data received (filled on timer tick)
    private List<UsbOBDRxData> mOBDRxDataBuffer = null;
    // Buffer with OBD data to transmit (emptied on timer tick)
    private List<UsbOBDTxData> mOBDTxDataBuffer = null;
    // Fixed size buffer with OBD transmit structures passed to the device driver
    private UsbOBDTxData[] mOBDHwTxBuff = new UsbOBDTxData[0];
    // Fixed size buffer with OBD receive structures passed to the device driver
    private UsbOBDRxData[] mOBDHwRxBuff = new UsbOBDRxData[0];

    // Device driver OBD receive buffer size ( receive FROM this module )
    private UInt32 mOBDDevRxBuffSize = 0;
    // Device driver OBD transmit buffer size ( send TO this module )
    private UInt32 mOBDDevTxBuffSize = 0;

    #endregion Private fields



    #region Constructors & finalizer

    unsafe public GearShiftUsb(bool flag)
    {
      mDevStateTimer = new System.Timers.Timer();
      mDevStateTimer.Interval = 100;
      mDevStateTimer.Elapsed += new ElapsedEventHandler(DevStatTimerTick);
      mDevStateTimer.Enabled = true;

      //----------------------------------------------------------------------//
      // DAQ part
      mDAQTxDataBuffer = new List<UsbDAQTxData>(0);

      mDAQRxTxTimer = new System.Timers.Timer();
      mDAQRxTxTimer.Interval = mSampleIntervalMs * mSamplingRateDivider;
      mDAQRxTxTimer.Elapsed += new ElapsedEventHandler(DAQRxTxTimerTick);
      mDAQRxTxTimer.Enabled = false;

      //----------------------------------------------------------------------//
      // CAN part
      mCANRxDataBuffer = new List<UsbCANData>(0);
      mCANTxDataBuffer = new List<UsbCANData>(0);

      mCANRxTxTimer = new System.Timers.Timer();
      mCANRxTxTimer.Interval = mCANUsbCommInterval;
      mCANRxTxTimer.Elapsed += new ElapsedEventHandler(CANRxTxTimerTick);
      mCANRxTxTimer.Enabled = false;

      //----------------------------------------------------------------------//
      // OBD part
      mOBDRxDataBuffer = new List<UsbOBDRxData>(0);
      mOBDTxDataBuffer = new List<UsbOBDTxData>(0);

      mOBDRxTxTimer = new System.Timers.Timer();
      mOBDRxTxTimer.Interval = mOBDUsbCommInterval;
      mOBDRxTxTimer.Elapsed += new ElapsedEventHandler(OBDRxTxTimerTick);
      mOBDRxTxTimer.Enabled = false;
    }

    unsafe ~GearShiftUsb()
    {
      if (mIsConnected)
      {
        UsbPwmStop();
        UsbGetDeviceState(ref mDevState, ref mDevCommErrCode, ref mDevOvercurrentMask);
        while (mDevState != (UInt16)DeviceStates.DEV_DISCONNECTED)
          if (mDevState == (UInt16)DeviceStates.DEV_CONNECTED)
          {
            System.Threading.Thread.Sleep(100);
            UsbGetDeviceState(ref mDevState, ref mDevCommErrCode, ref mDevOvercurrentMask);
            UsbDisconnect();
            int x = 7;
            int y = x;
          }
      }
      else
      {

      }
    }

    #endregion Constructors & finalizer



    #region Events

    /// <summary>
    /// On device connected event
    /// </summary>
    public event EventHandler DeviceConnectedEvent;

    /// <summary>
    /// On device disconnected event
    /// </summary>
    public event EventHandler DeviceDisconnecedEvent;

    public delegate void MyEventHandler(DeviceAcquiredData data);

    /// <summary>
    /// Fired when DAQ data was received
    /// </summary>
    public event MyEventHandler DAQDataReceivedEvent;

    /// <summary>
    /// Fired when OBD data was received
    /// </summary>
    public event EventHandler OBDDataReceivedEvent;

    public event EventHandler DAQ_OvercurrentDetectedEvent;

    #endregion Events



    #region Properties



    /// <summary>
    /// If the device is connected
    /// </summary>
    public bool IsConnected
    {
      get
      {
        return mIsConnected;
      }
    }


    // DAQ buffers and variables //--------------------------------------------------------------------//
    //-------------------------------------------------------------------------------------------------//

    public UInt32 LastResponsedFrameID
    {
      get { return mLastResponsedFrameID; }
    }

    /// <summary>
    /// Output data buffer (transmission to device)
    /// </summary>
    public List<UsbDAQTxData> TxDataBuffer
    {
      get { return mDAQTxDataBuffer; }
    }


    /// <summary>
    /// Test script file used only to save data acquired at the end od a script line
    /// </summary>
    public TestScript TestScript_
    {
      get { return mTestScript; }
      set { mTestScript = value; }
    }

    /// <summary>
    /// Zf6 USB interface
    /// </summary>
    public GST.ZF6.Components.Interfaces.MechShifterUSB.UsbDevice UsbDev_Decoder
    {
      get { return _usbDev_Decoder; }
      set
      {
        if (value != null)
        {
          _usbDev_Decoder = value;
          //_NissanRE5_Interface.Zf6UsbDevice = value;
        }
      }
    }

    /// <summary>
    /// Nissan RE5 interface
    /// </summary>
    public Soko.CanCave.Components.Interfaces.TCUGovernor_NissanRE5 NissanRE5_Interface
    {
      get { return _NissanRE5_Interface; }
      //set { _NissanRE5_Interface = value; }
    }
    public void SetNissanRE5_Interface (Soko.CanCave.Components.Interfaces.TCUGovernor_NissanRE5 NissanRE5_IF)
    {
      _NissanRE5_Interface = NissanRE5_IF;
      //set { _NissanRE5_Interface = value; }
    }

    // OBD buffers and variables //--------------------------------------------------------------------//
    //-------------------------------------------------------------------------------------------------//

    /// <summary>
    /// OBD received messages buffer
    /// </summary>
    public List<UsbOBDRxData> OBDRxBuffer
    {
      get { return mOBDRxDataBuffer; }
    }

    /// <summary>
    /// OBD messages to transmit buffer
    /// </summary>
    public List<UsbOBDTxData> OBDTxBuffer
    {
      get { return mOBDTxDataBuffer; }
    }

    #endregion Properties



    #region Imported functions

    // See the GearboxToolDevice_dllImports.cs

    #endregion Imported functions



    #region Methods

    private void OnDeviceDisconnected()
    {
      if (DeviceDisconnecedEvent != null)
        DeviceDisconnecedEvent(this, EventArgs.Empty);

      if (mCANDataRecordingEnabled && CAN_CanCordingStopped != null)
      {
        CAN_CanCordingStopped(this, EventArgs.Empty);
      }
    }

    /// <summary>
    /// Connects to the USB device
    /// </summary>
    public unsafe void Connect()
    {
      try
      {
        //call connect routine
        int rslt = UsbConnect();
        //check if already connected
        UsbGetDeviceState(ref mDevState, ref mDevCommErrCode, ref mDevOvercurrentMask);
        //if not, try mConnectRetriesMaxCount times more
        System.Threading.Thread.Sleep(300);
        uint connRetries = 0;
        while (mDevState != (UInt16)DeviceStates.DEV_CONNECTED && connRetries < mConnectRetriesMaxCount)
        {
          System.Threading.Thread.Sleep(100);
          UsbGetDeviceState(ref mDevState, ref mDevCommErrCode, ref mDevOvercurrentMask);
          connRetries++;
        }
        // if still not connected, throw exception.
        if (mDevState != (UInt16)DeviceStates.DEV_CONNECTED)
        {
          throw new TimeoutException("USB connection timeout");
        }

        mDAQDevRxBuffSize = UsbGetDevRxBuffSize();
        mDAQDevTxBuffSize = UsbGetDevTxBuffSize();
        mCANDevRxBuffSize = UsbCANGetDevRxBuffSize();
        mCANDevTxBuffSize = UsbCANGetDevTxBuffSize();
        mOBDDevRxBuffSize = UsbOBDGetDevRxBuffSize();
        mOBDDevTxBuffSize = UsbOBDGetDevTxBuffSize();

        // buffers initialization
        mDAQHwRxBuff = new UsbDAQRxData[mDAQDevTxBuffSize];
        for (int j = 0; j < mDAQDevTxBuffSize; j++)
        {
          mDAQHwRxBuff[j].current = new UInt16[18];
          mDAQHwRxBuff[j].pressure = new UInt16[14];
        }

        mDAQHwTxBuff = new UsbDAQTxData[mDAQDevRxBuffSize];
        for (int i = 0; i < mDAQDevRxBuffSize; i++)
        {
          for (int j = 0; j < 9; j++)
          {
            mDAQHwTxBuff[i].Pwm = new byte[9];
            mDAQHwTxBuff[i].Pwm[j] = (byte)j;
            mDAQHwTxBuff[i].ID = (uint)i + 1;
          }
        }

        mCANHwRxBuff = new UsbCANData[mCANDevRxBuffSize];
        for (int j = 0; j < mCANDevRxBuffSize; j++)
        {
          mCANHwRxBuff[j].data = new byte[8];
        }

        mCANHwTxBuff = new UsbCANData[mCANDevTxBuffSize];
        for (int j = 0; j < mDAQDevTxBuffSize; j++)
        {
          mCANHwTxBuff[j].data = new byte[8];
        }

        mOBDHwRxBuff = new UsbOBDRxData[mOBDDevRxBuffSize];
        for (int j = 0; j < mOBDDevRxBuffSize; j++)
        {
          mOBDHwRxBuff[j].bytes = new byte[50];
        }

        mOBDHwTxBuff = new UsbOBDTxData[mOBDDevTxBuffSize];
        for (int j = 0; j < mOBDDevTxBuffSize; j++)
        {
          mOBDHwTxBuff[j].chars = new char[50];
        }

        mIsConnected = true;
        if (DeviceConnectedEvent != null)
          DeviceConnectedEvent(this, EventArgs.Empty);
      }
      catch (Exception Ex)
      {
        mIsConnected = false;
        //Console.WriteLine("Connect: USB device library error while connecting: " + Ex.Message);
        throw new IOException("USB device library error while connecting: " + Ex.Message);
      }
    }

    /// <summary>
    /// Disconnects from the USB device
    /// </summary>
    public void Disconnect()
    {
      try
      {
        if (mIsConnected)
          UsbDisconnect();

        mIsConnected = false;
        OnDeviceDisconnected();
      }
      catch (Exception e)
      {
        throw new IOException("USB device library error while disconnecting: " + e.Message);
      }

    }

    /// <summary>
    /// Device status updater routine
    /// </summary>
    /// <param name="source"></param>
    /// <param name="eea"></param>
    private void DevStatTimerTick(object source, ElapsedEventArgs eea)
    {
      // Save current values, get new ones from the device
      UInt32 devPrevState = mDevState;
      UInt32 devPrevOVCMask = mDevOvercurrentMask;
      UsbGetDeviceState(ref mDevState, ref mDevCommErrCode, ref mDevOvercurrentMask);

      // Detect changes and take care of the connection
      if (mDevState != devPrevState)
      {

        // We should check here only if there's been an error occurence.
        if (mDevState == (UInt16)DeviceStates.DEV_ERROR)
        {
          Console.WriteLine("DEV ERR ");
          // Check what error occured
          switch (mDevCommErrCode)
          {
            case (UInt32)errorCodes.ERR_DEVICE_NOT_RESPONDING:
              {
                Console.WriteLine("DEV NOT RESPONDING");
                Disconnect();
                break;
              }
          }
        }
      }
      //Console.WriteLine( "OVERCURRENT " + Convert.ToString( mDevOvercurrentMask, 2 ) );
      // See if overcurrent was detected
      if (devPrevOVCMask != mDevOvercurrentMask && mDevOvercurrentMask != 0)
      {
        Console.WriteLine("OVERCURRENT " + Convert.ToString(mDevOvercurrentMask, 2));
        if (DAQ_OvercurrentDetectedEvent != null)
        {
          DAQ_OvercurrentDetectedEvent( this, EventArgs.Empty );
        }
      }

      //}
    }

    private bool GetConnectedStatus()
    {
      UsbGetDeviceState(ref mDevState, ref mDevCommErrCode, ref mDevOvercurrentMask);
      if (mDevState == (UInt16)DeviceStates.DEV_CONNECTED)
      {
        return true;
      }
      else
      {
        return false;
      }
    }


    // UI functions set //-----------------------------------------------------------------------------//
    //-------------------------------------------------------------------------------------------------//

    /// <summary>
    /// Sets the UI bargraphs display mode
    /// </summary>
    /// <param name="mode">Display mode: 1-current, 0-PWMs</param>
    public void SetUIBargraphsDisplayMode(byte mode)
    {
      UsbUISetBargraphsMode(mode);
    }

    /// <summary>
    /// Displays a message on first row of UI LCD
    /// </summary>
    /// <param name="message">16-characters message to be displayed</param>
    public void DisplayLcdRow1Message(string message)
    {
      char[] msgbuff = new char[17];
      int charcount = message.Length;
      if (charcount > 16)
        charcount = 16;
      message.CopyTo(0, msgbuff, 0, charcount);
      UsbUIDisplayLcdRow1Msg(msgbuff);
    }

    /// <summary>
    /// Displays a message on second row of UI LCD
    /// </summary>
    /// <param name="message">16-characters message to be displayed</param>
    public void DisplayLcdRow2Message(string message)
    {
      char[] msgbuff = new char[17];
      int charcount = message.Length;
      if (charcount > 16)
        charcount = 16;
      message.CopyTo(0, msgbuff, 0, charcount);
      UsbUIDisplayLcdRow2Msg(msgbuff);
    }


    // DAQ functions set //----------------------------------------------------------------------------//
    //-------------------------------------------------------------------------------------------------//

    /// <summary>
    /// Sets the device configuration
    /// </summary>
    /// <param name="mPwmFrequency">PWM frequency for the solenoid drive</param>
    /// <param name="mCurrentADCMask"></param>
    public void SetDeviceDAQConfig(UInt32 mPwmFrequency, DisplayChannelsSet currentChannelsSet)
    {

      //check if the PWM frequency is valid
      if (mPwmFrequency < 100 || mPwmFrequency > 2000)
        throw new ArgumentException("The PWM frequency must be between 100 and 2000");

      if (currentChannelsSet.ChannelsCount > 9)
      {
        currReadChannsCount = 0;
        throw new ArgumentException("There can be only 9 current channels measurements active at the same time");
      }
      else
      {
        currReadChannsCount = (byte)currentChannelsSet.ChannelsCount;
      }

      for (int i = 0; i < currReadChannsCount; i++)
      {
        currReadChannsIndices[i] = (byte)currentChannelsSet.Channels[i].InputChannelIndex;
      }

      try
      {
        UsbSetDAQConfig((Int32)mPwmFrequency, currReadChannsCount, currReadChannsIndices);
      }
      catch (Exception e)
      {
        throw new IOException("USB device library error while setting the device config: " + e.Message);
      }
    }

        /// <summary>
        /// Starts the transmission with the device
        /// Note that InterFace Tx buffer should be properly filled up with data
        /// </summary>
    public void StartDAQTransmission()
    {
      try
      {
        UsbGetDeviceState(ref mDevState, ref mDevCommErrCode, ref mDevOvercurrentMask);
        //throw new InvalidOperationException("Cannot start analog outputs test, must be connected first");
        if (mDevState != (UInt16)DeviceStates.DEV_CONNECTED)
        {
            Soko.Common.Forms.MessageBox.ShowInfo("GearShift",
            "Connection Error",
            "Cannot start analog outputs test, must be connected first",
            Soko.Common.Forms.MessageBoxButtons.OK);
        }
      }
      //if (mDevState != (UInt16)DeviceStates.DEV_CONNECTED)
      catch (Exception e)
      {
        throw new InvalidOperationException("Cannot start transmission while the device is not connected!!" + e.Message);
      }

      uint num = 0;

      if (mDAQTxDataBuffer.Count <= 0)
        throw new InvalidOperationException("Cannot start transmission, Tx buffer cannot be empty!");

      //Data transmit part
      lock (this)
      {
        ////check the buffer emptiness
        num = UsbGetDevRxBuffFill();
        UInt32 buffEmptiness = mDAQDevRxBuffSize - num;
        UInt32 framesToSend = 0;
        // if more than a half of buffer is empty, fill it up
        if (buffEmptiness > mDAQDevRxBuffSize / 2)
        {
          if (buffEmptiness < mDAQTxDataBuffer.Count)
            framesToSend = buffEmptiness;
          else
            framesToSend = (uint)mDAQTxDataBuffer.Count;
          //UInt32 framesToSend = buffEmptiness - ( mDevRxBuffSize );
          //if (framesToSend > mIfTxDataBuffer.Count)//in case of semi-empty buffer
          //  framesToSend = (UInt32)mIfTxDataBuffer.Count;

          //prepare the buffer for the dll to copy from
          UsbDAQTxData[] buffer = new UsbDAQTxData[framesToSend];
          mDAQTxDataBuffer.CopyTo(0, buffer, 0, (int)framesToSend);
          //Assign proper IDs
          mDAQTxDataBuffer.RemoveRange(0, (int)framesToSend);
          //for (int i = 0; i < framesToSend; i++ )
          //{
          //  buffer[i].ID = mLastSentDAQFrameID + 1;
          //  //Console.WriteLine(buffer[i].ID.ToString());
          //  mLastSentDAQFrameID++;
          //}
          UInt32 framesSent = 0;
          UsbWriteData(buffer, framesToSend, ref framesSent);
          if (framesSent != framesToSend)
          {
            //throw new ExternalException("Could not write to the device (" + framesSent.ToString()
            //  + " frames were sent instead of " + framesToSend.ToString() + ").");
          }
        }

        //Start the PWM inside the device
        System.Threading.Thread.Sleep(1000);
        //there must be this long delay here (the device tends to fail otherwise)
        UsbPwmStart();
        //start the transmission timer
        mDAQRxTxTimer.Enabled = true;
      }
    }

    /// <summary>
    /// Stops the transmission with the USB device
    /// </summary>
    public void StopDAQTransmission()
    {
      try
      {
        //TODO:
        //Clear the TX buffer, wait until device sends the frame "with response to" value same as last sent frame ID
        mDAQRxTxTimer.Enabled = false;
        UsbPwmStop();
      }
      catch (Exception)
      {
        throw new IOException("USB device library error while disconnecting");
      }
    }

    /// <summary>
    /// Writes the latest read pressure values to specified DisplayChannelsSet
    /// </summary>
    /// <param name="channels">DisplayChannelsSet to write to</param>
    public float GetLatestAIValueUserUnit(GearShiftUsb.AIChannel input)
    {
      return MeasurementUnit.ConvertAIValueBaseUnitToUserUnit(_daqLatestReceivedData.pressures[input.InputIndex], input.ValueType);
        //return _daqLatestReceivedData.pressures[input.InputIndex];
    }

    /// <summary>
    /// Writes the latest read pressure values to specified DisplayChannelsSet, overrides input channel
    /// </summary>
    /// <param name="channels">DisplayChannelsSet to write to</param>
    public float GetLatestAIValueUserUnit(GearShiftUsb.AIChannel input, int index)
    {
      return MeasurementUnit.ConvertAIValueBaseUnitToUserUnit(_daqLatestReceivedData.pressures[index], input.ValueType);
      //return _daqLatestReceivedData.pressures[input.InputIndex];
    }

    /// <summary>
    /// Writes the latest read current values to specified DisplayChannelsSet
    /// </summary>
    /// <param name="channels">DisplayChannelsSet to write to</param>
    public void GetLatestCurrentValues(DisplayChannelsSet channels)
    {
      switch (mTestScript.Gearbox.ControllerType)
      {
        default:
        case GearboxControllerType.NON_MECHATRONIC:
          {
            // If this is an analogue gearbox, get the data from Gearshift USB
            for (int i = 0; i < currReadChannsCount; i++)
            {
              DisplayChannel channel = channels.Channels[i];
              channel.Value = _daqLatestReceivedData.currents[channel.InputChannelIndex];
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
            // For 6HP gearbox, get the data from Zf6
            if (_usbDev_Decoder != null)
            {
              for (int i = 0; i < currReadChannsCount; i++)
              {
                DisplayChannel channel = channels.Channels[i];
                channel.Value = _usbDev_Decoder.GetLastCurrentValue(channel.InputChannelIndex);
              }
            }
            break;
          }

        case GearboxControllerType.NISSAN_RE5:
          {
            // If this is Nissan RE5 gearbox
            // TODO NISSAN Check if current values are ok
            if ( _NissanRE5_Interface != null)
            {
              for (int i = 0; i < currReadChannsCount; i++)
              {
                DisplayChannel channel = channels.Channels[i];
                channel.Value = _NissanRE5_Interface.GetLastCurrentValue(channel.InputChannelIndex);
              }
            }
            break;
          }
        case GearboxControllerType.GM6T40:
        case GearboxControllerType.GM6T70:
        case GearboxControllerType.GM6L:
          {
            // If this is GM 6Txx gearbox
            // TODO: Should I get some values for GM6T?
              for (int i = 0; i < currReadChannsCount; i++)
              {
                DisplayChannel channel = channels.Channels[i];
                channel.Value = 0.0f;
              }
            break;
          }
      }

    }

    /// <summary>
    /// Tick of the DAQ transmission timer
    /// </summary>
    /// <param name="source"></param>
    /// <param name="eea"></param>
    private void DAQRxTxTimerTick(object source, ElapsedEventArgs eea)
    {

      // Data transmit part
      lock (this)
      {
        //check the buffer emptiness
        UInt32 buffFill = 0;
        buffFill = UsbGetDevTxBuffFill();

        UInt32 framesToSend = mDAQDevTxBuffSize - buffFill;
        if (framesToSend > mDAQTxDataBuffer.Count)//in case of empty buffer
          framesToSend = (UInt32)mDAQTxDataBuffer.Count;
        //prepare the buffer for the dll to copy from
        mDAQTxDataBuffer.CopyTo(0, mDAQHwTxBuff, 0, (int)framesToSend);
        mDAQTxDataBuffer.RemoveRange(0, (int)framesToSend);
        UInt32 framesSent = 0;
        UsbWriteData(mDAQHwTxBuff, framesToSend, ref framesSent);
        if (framesSent != framesToSend)
        {
          throw new ExternalException("Could not write to the device (" + framesSent.ToString()
                      + " frames were sent instead of " + framesToSend.ToString() + ").");
        }
      }

      UInt32 ReadFramesCount = 0;

      // Read data from the device
      UsbReadData(mDAQHwRxBuff, (UInt32)mSamplingRateDivider * 2, ref ReadFramesCount);
      // Process each received frame
      for (int i = 0; i < ReadFramesCount; i++)
      {
        // Create the DeviceAcquiredData struct and fill
        UsbDAQRxData pkt = mDAQHwRxBuff[i];
        DeviceAcquiredData data = new DeviceAcquiredData();
        data.ID = pkt.ID;
        data.responseToID = pkt.responseToID;
        for (int j = 0; j < mTestScript.Gearbox._analogueInputs.Count; j++)
        {
          // Save the rounded value of pressure to 2 decimal points (rest is insignificant)
          AIChannel aic = mTestScript.Gearbox._analogueInputs[j];
          float pureValueVoltsByTen = pkt.pressure[aic.InputIndex] * mPressureADCGain / 10.0f;
          float unroundedValueBaseUnit = aic.MinValueBaseUnit + pureValueVoltsByTen * (aic.MaxValueBaseUnit - aic.MinValueBaseUnit);
          data.pressures[j] = (float)Math.Round(unroundedValueBaseUnit, 2);
        }
        //foreach (AIChannel aic in mTestScript.Gearbox._analogueInputs)
        //{
        //  //Save the rounded value of pressure to 2 decimal points (rest is insignificant)
        //  float pureValueVoltsByTen = pkt.pressure[aic.InputIndex] * mPressureADCGain / 10.0f;
        //  float unroundedValueBaseUnit = aic.MinValueBaseUnit + pureValueVoltsByTen * (aic.MaxValueBaseUnit - aic.MinValueBaseUnit);
        //  data.pressures[aic.InputIndex] = (float)Math.Round(unroundedValueBaseUnit, 2);
        //}

        for (int j = 0; j < 18; j++)
        {
          // Save the rounded value of pressure to 2 decimal points (rest is insignificant)
          data.currents[j] = (float)Math.Round(mCurrentADCGain * pkt.current[j], 2);
        }

        // Store this data so the GetLatestPressures/currents functions will have the data ready
        _daqLatestReceivedData = data;

        // if there was some data read, fire DAQDataReceivedEvent event
        if (DAQDataReceivedEvent != null)
          DAQDataReceivedEvent(data);

      }

      // Print the latest received data only in debug mode
#if DEBUG
      //Console.Write("\nCurr: ");
      //for (int j = 0; j < 18; j++)
      //{
      //  Console.Write(_daqLatestReceivedData.currents[j].ToString("0.00") + " ");
      //}

      //Console.Write(" Press: ");
      //for (int j = 0; j < 14; j++)
      //{
      //  Console.Write(_daqLatestReceivedData.pressures[j].ToString("0.00") + " ");
      //}
#endif


    }

    public void DAQManualDriveSetValue(byte index, byte value)
    {
      if (index >= 18)
      {
        throw new IndexOutOfRangeException("The index exceeds the number of drivers available");
      }
      if (index >= 9) // if upper channel index given
      {
        index -= 9;
      }
      UsbDAQSetManualDriverValue(index, value);
    }

    public void DAQManualDriveEnable(byte index, byte value)
    {
      if (index >= 18)
      {
        throw new IndexOutOfRangeException("The index exceeds the number of drivers available");
      }
      if (index >= 9) // if upper channel index given
      {
        index -= 9;
      }
      UsbDAQEnableManualDriverControl(index, value);
    }

    public void DAQManualDriveDisable(byte index)
    {
      if (index >= 18)
      {
        throw new IndexOutOfRangeException("The index exceeds the number of drivers available");
      }
      if (index >= 9) // if upper channel index given
      {
        index -= 9;
      }
      UsbDAQDisableManualDriverControl(index);
    }

    public void DAQManualDriveDisableAll()
    {
      UsbDAQDisableAllManualDriverControls();
    }

    public bool DAQGetOvercurrentStatusAtIndex(int index)
    {
      if (index > 8 || index < 0)
      {
        throw new ArgumentOutOfRangeException("The channel index must be between 0 and 8");
      }
      return (mDevOvercurrentMask & (1 << index)) > 0;
    }

    public void DAQClearVariables()
    {
      mLastResponsedFrameID = 0;
    }

    // OBD functions set //----------------------------------------------------------------------------//
    //-------------------------------------------------------------------------------------------------//

    public void SetDeviceOBDConfig()
    {

    }

    /// <summary>
    /// Connects to the vehicle via ELM327
    /// Device must be connected before this call
    /// </summary>
    /// <returns> 0 if succesful, 1 if failed</returns>
    public int ConnectToVehicle()
    {
      if (!mIsConnected)
      {
        throw new InvalidOperationException("The device must be connected first before requesting to connect to the vehicle");
      }
      int retVal = UsbOBDElm327Init();
      if (retVal == 0)
      {
        mOBDRxTxTimer.Enabled = true;
      }
      //Console.WriteLine( "CONNECTED" );
      return retVal;
    }

    public void DisconnectTheVehicle()
    {
      mOBDRxTxTimer.Enabled = false;
    }

    /// <summary>
    /// Tick of the OBD transmission timer
    /// </summary>
    /// <param name="source"></param>
    /// <param name="eea"></param>
    private void OBDRxTxTimerTick(object source, ElapsedEventArgs eea)
    {
      ////Data transmit part
      lock (this)
      {
        UInt32 framesToSend = UsbOBDGetDevTxBuffSize() - UsbOBDGetDevTxBuffFill();
        if (framesToSend > mOBDTxDataBuffer.Count)//in case of empty buffer
          framesToSend = (UInt32)mOBDTxDataBuffer.Count;
        //prepare the buffer for the dll to copy from
        if (framesToSend > 0)
        {
          mOBDTxDataBuffer.CopyTo(0, mOBDHwTxBuff, 0, (int)framesToSend);
          mOBDTxDataBuffer.RemoveRange(0, (int)framesToSend);
          UInt32 framesSent = 0;
          UsbOBDWriteData(mOBDHwTxBuff, framesToSend, ref framesSent);
          //Console.WriteLine( " PACKET WRITTEN YO BITCH, FTS: " + framesToSend.ToString() + "FS: " + framesSent.ToString() + "\n\n");
          //Console.WriteLine("DEV TX BF: " + UsbOBDGetDevTxBuffFill().ToString("00000") + " SWBF: " + mOBDTxDataBuffer.Count.ToString("00000"));
          if (framesSent != framesToSend)
            throw new ExternalException("Could not write OBD data to the device (" + framesSent.ToString()
              + " messages were sent instead of " + framesToSend.ToString() + ").");
        }

      }

      //Data receive part
      UInt32 ReadFramesCount = 0;
      lock (this)
      {
        UsbOBDReadData(mOBDHwRxBuff, (UInt32)mOBDDevRxBuffSize, ref ReadFramesCount);
        //         if ( ReadFramesCount > 0 )
        //           Console.WriteLine( ReadFramesCount.ToString() );
        for (int i = 0; i < ReadFramesCount; i++)
        {
          mOBDRxDataBuffer.Add(mOBDHwRxBuff[i]);
        }

      }
      if (ReadFramesCount > 0 && OBDDataReceivedEvent != null)
        OBDDataReceivedEvent(this, EventArgs.Empty);
    }

    /// <summary>
    /// Puts a command into OBD TX buffer
    /// </summary>
    /// <param name="cmd">command to send</param>
    /// <returns>false if succeeded, true if failed</returns>
    public bool OBDPutCmdToBfr(String cmd)
    {
      if (OBDTxBuffer.Count < 1000)
      {
        UsbOBDTxData pkt;
        pkt.chars = new char[50];
        pkt.charCount = Convert.ToByte(cmd.Length);
        cmd.CopyTo(0, pkt.chars, 0, cmd.Length);
        OBDTxBuffer.Add(pkt);
        return false;
      }
      else
      {
        return true;
      }

    }

    #endregion Methods


  }

}
