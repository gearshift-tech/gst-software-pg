using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;
using System.IO;
using System.Windows.Forms;


using System.ComponentModel;


namespace GST.Gearshift.Components.Interfaces.USB//GST.Gearshift.Components.Interfaces.USB
{

  /// <remarks>
  /// Physical device interface
  /// THIS FILE TREATS ONLY THE CAN PART OF THE COMMUNICATION!
  /// </remarks>
  unsafe public partial class GearShiftUsb
  {

    /// <summary>
    /// USB CAN fixed trace record
    /// </summary>
    public class CANFixedTraceRecord
    {
      public UInt32 ID;
      public UInt32 MsgCount;
      public UsbCANData LastMessage;
      public UInt32 FirstTimeStamp;
      public UInt32 LastTimeStamp;
      public float CycleTimeInstMs;
      public float CycleTimeMinMs;
      public float CycleTimeMaxMs;
      public float CycleTimeAvgMs;
    }


    #region Constants

    // Packet transmission interval for CAN functionality
    private readonly int mCANUsbCommInterval = 10;

    #endregion  Constants



    #region Private fields

    // CAN buffers and variables //--------------------------------------------------------------------//
    //-------------------------------------------------------------------------------------------------//

    // Timer to serve CAN communication with the device
    private System.Timers.Timer mCANRxTxTimer = null;

    // Buffer with CAN data received (filled on timer tick)
    private List<UsbCANData> mCANRxDataBuffer = null;
    // Buffer with CAN data to transmit (emptied on timer tick)
    private List<UsbCANData> mCANTxDataBuffer = null;
    // Fixed size buffer with CAN transmit structures passed to the device driver
    private UsbCANData[] mCANHwTxBuff = new UsbCANData[0];
    // Fixed size buffer with CAN receive structures passed to the device driver
    private UsbCANData[] mCANHwRxBuff = new UsbCANData[0];

    // Buffer with CAN data being recorded
    private List<UsbCANData> mCANRecordBuffer = new List<UsbCANData>();
    // Buffer with CAN data prepared for playback
    private List<UsbCANData> mCANPlaybackBuffer = new List<UsbCANData>();

    // Device driver CAN receive buffer size ( receive FROM this module )
    private UInt32 mCANDevRxBuffSize = 0;
    // Device driver CAN transmit buffer size ( send TO this module )
    private UInt32 mCANDevTxBuffSize = 0;

    // If the CAN bus termination is enabled or not
    private bool mCANTerminationEnabled = false;

    // If the CAN bus data recording functionality is enabled
    private bool mCANDataRecordingEnabled = false;

    // If the CAN bus data playback functionality is enabled
    private bool mCANDataPlaybackEnabled = false;

    private List<CANFixedTraceRecord> CAN_Sniffer_FixedTraceRecords = new List<CANFixedTraceRecord>();

    #endregion Private fields



    #region Events

    /// <summary>
    /// Fired when CAN data was received
    /// </summary>
    public event EventHandler CANDataReceivedEvent;

    /// <summary>
    /// Fired when CAN is enabled
    /// </summary>
    public event EventHandler CAN_EnabledEvent;

    /// <summary>
    /// Fired when CAN is disabled
    /// </summary>
    public event EventHandler CAN_DisabledEvent;

    /// <summary>
    /// Fired when CAN bus termination is enabled
    /// </summary>
    public event EventHandler CAN_TerminationEnabledEvent;

    /// <summary>
    /// Fired when CAN bus termination is disabled
    /// </summary>
    public event EventHandler CAN_TerminationDisabledEvent;

    public event EventHandler CAN_CanCordingStarted;

    public event EventHandler CAN_CanCordingStopped;

    public event EventHandler CAN_CanPlaybackStarted;

    public event EventHandler CAN_CanPlaybackStopped;

    #endregion Events



    #region Properties

    // CAN buffers and variables //--------------------------------------------------------------------//
    //-------------------------------------------------------------------------------------------------//

    /// <summary>
    /// CAN received messages buffer
    /// </summary>
    public List<UsbCANData> CANRxBuffer
    {
      get { return mCANRxDataBuffer; }
    }

    /// <summary>
    /// CAN messages to transmit buffer
    /// </summary>
    public List<UsbCANData> CANTxBuffer
    {
      get { return mCANTxDataBuffer; }
    }

    public bool CANTerminationEnabled
    {
      get
      {
        return mCANTerminationEnabled;
      }
    }

    public List<CANFixedTraceRecord> CAN_Sniffer_FixedTraceRecordsTable
    {
      get { return CAN_Sniffer_FixedTraceRecords; }
    }


    public List<UsbCANData> CANRecordBuffer
    {
      get { return mCANRecordBuffer; }
    }

    public List<UsbCANData> CANPlaybackBuffer
    {
      get { return mCANPlaybackBuffer; }
    }

    #endregion Properties



    #region Methods

    public double StartCANTransmission( UInt32 busBaud )
    {
      if (! mIsConnected)
      {
        throw new InvalidOperationException("Cannot start CAN transmission when device is not connected!");
      }
      if ( busBaud < 50000 || busBaud > 1000000 )
      {
        throw new InvalidOperationException( "CAN bus baud value must be contained between 50000 and 1000000" );
      }
      double err = 0;
      if ( UsbCANSetConfig( busBaud, ref err ) == 0 )
      {
        DisableCANBusTermination();
        if ( CAN_EnabledEvent != null )
        { 
          CAN_EnabledEvent( this, EventArgs.Empty );
        }
        mCANRxTxTimer.Enabled = true;
      }
      return err;
    }

    public void StopCANTransmission()
    {
      if ( !mIsConnected )
      {
        throw new InvalidOperationException( "Cannot stop CAN transmission when device is not connected!" );
      }
      
      if ( UsbCANDisableComm() == 0)
      {
        DisableCANBusTermination();
        if ( CAN_DisabledEvent != null )
        {
          CAN_DisabledEvent( this, EventArgs.Empty );
        }
        mCANRxTxTimer.Enabled = false;
      }
    }

    public bool EnableCANBusTermination()
    {
      if ( !mIsConnected )
      {
        throw new InvalidOperationException( "Cannot enable CAN termination when device is not connected!" );
      }
      if ( UsbCanEnablePullUp() == 0 )
      {
        mCANTerminationEnabled = true;
        if (CAN_TerminationEnabledEvent != null)
        {
          CAN_TerminationEnabledEvent( this, EventArgs.Empty );
        }
        return false;
      }
      else
      {
        return true;
      }
    }

    public bool DisableCANBusTermination()
    {
      if ( !mIsConnected )
      {
        throw new InvalidOperationException( "Cannot disable CAN termination when device is not connected!" );
      }
      if (UsbCanDisablePullUp() == 0 )
      {
        mCANTerminationEnabled = false;
        if (CAN_TerminationDisabledEvent != null)
        {
          CAN_TerminationDisabledEvent( this, EventArgs.Empty );
        }
        return false;
      }
      else 
      {
        return true;
      }
    }

    /// <summary>
    /// Tick of the CAN transmission timer
    /// </summary>
    /// <param name="source"></param>
    /// <param name="eea"></param>
    private void CANRxTxTimerTick(object source, ElapsedEventArgs eea)
    {
      ////Data transmit part
      lock ( this )
      {
        //check the buffer emptiness
        UInt32 buffFill = 0;
        buffFill = UsbCANGetDevTxBuffFill();

        UInt32 framesToSend = mCANDevTxBuffSize - buffFill;
        if ( framesToSend > mCANTxDataBuffer.Count )//in case of empty buffer
          framesToSend = (UInt32)mCANTxDataBuffer.Count;
        //prepare the buffer for the dll to copy from
        mCANTxDataBuffer.CopyTo( 0, mCANHwTxBuff, 0, (int)framesToSend );
        mCANTxDataBuffer.RemoveRange( 0, (int)framesToSend );
        UInt32 framesSent = 0;
        UsbCANWriteData( mCANHwTxBuff, framesToSend, ref framesSent );
        if ( framesSent != framesToSend )
          throw new ExternalException( "Could not write CAN data to the device (" + framesSent.ToString()
            + " messages were sent instead of " + framesToSend.ToString() + ")." );
      }

      //Data receive part
      UInt32 ReadFramesCount = 0;
      lock ( this )
      {
        UsbCANReadData( mCANHwRxBuff, (UInt32)mCANDevRxBuffSize, ref ReadFramesCount );
        //if (ReadFramesCount > 0)
        //  Console.WriteLine("RFC: " + ReadFramesCount.ToString());
        for ( int i = 0; i < ReadFramesCount; i++ )
        {
          UsbCANData msg = mCANHwRxBuff[i];
          // If data recording is enabled put the rcvd msg to the buffer
          if (mCANDataRecordingEnabled)
          {
            mCANRecordBuffer.Add(msg);
          }

          // Process the fixed trace records

          // Check whether the fixed trace mode is enabled, if so, process the data list
          if (true)//CAN_Sniffer_FixedTraceModeEnabled)
          {
            bool recordFound = false;
            for (int v = 0; v < CAN_Sniffer_FixedTraceRecords.Count; v++)
            {
              if (msg.remoteID == CAN_Sniffer_FixedTraceRecords[v].ID)
              {
                CANFixedTraceRecord rec = CAN_Sniffer_FixedTraceRecords[v];
                rec.MsgCount++;
                rec.LastMessage = msg;
                //if (msg.timestamp >= CAN_Sniffer_FixedTraceRecords[v].TimeStamp)
                {
                  rec.CycleTimeInstMs = (msg.timestamp - rec.LastTimeStamp) / 10.0f;
                  rec.LastTimeStamp = msg.timestamp;
                  rec.CycleTimeAvgMs = ((rec.LastTimeStamp - rec.FirstTimeStamp) / (rec.MsgCount - 1)) / 10.0f;
                  if (rec.CycleTimeInstMs > rec.CycleTimeMaxMs)
                  {
                    rec.CycleTimeMaxMs = rec.CycleTimeInstMs;
                  }
                  if (rec.CycleTimeInstMs > 0)
                  {
                    if (rec.CycleTimeMinMs < 0)
                    {
                      rec.CycleTimeMinMs = rec.CycleTimeInstMs;
                    }
                    else
                    {
                      if (rec.CycleTimeInstMs < rec.CycleTimeMinMs)
                      {
                        rec.CycleTimeMinMs = rec.CycleTimeInstMs;
                      }
                    }
                  }

                }
                recordFound = true;
              }
            }
            if (!recordFound)
            {
              CANFixedTraceRecord rcd = new CANFixedTraceRecord();
              rcd.ID = msg.remoteID;
              rcd.MsgCount = 1;
              rcd.LastMessage = msg;
              rcd.LastTimeStamp = msg.timestamp;
              rcd.FirstTimeStamp = msg.timestamp;
              rcd.CycleTimeAvgMs = -1;
              rcd.CycleTimeInstMs = -1;
              rcd.CycleTimeMaxMs = -1;
              rcd.CycleTimeMinMs = -1;
              CAN_Sniffer_FixedTraceRecords.Add(rcd);
              //Console.WriteLine("NewRecord!!!!!!!!!!!!!!!!!!!!!!");
            }
          }

          mCANRxDataBuffer.Add( mCANHwRxBuff[i] );
          //UsbCANRxData rxd = mCANHwRxBuff[i];
          //Console.Write( " ID: 0x" + rxd.remoteID.ToString( "X" ) + " DLC: " + rxd.DLC.ToString() + "Data: " );
          //for (int z = 0; (z < rxd.DLC) & (z < 8); z++)
          //{
          //  Console.Write( rxd.data[z].ToString( "X2" ) + " " );
          //}
          //Console.WriteLine( "" );
        }

      }
      if ( ReadFramesCount > 0 && CANDataReceivedEvent != null )
        CANDataReceivedEvent( this, EventArgs.Empty );
      //Console.WriteLine( "ctc" );
    }

    //public void CANGetFixedTraceTable( List<UsbCANFixedTraceRecord> table )
    //{
    //  if ( IsConnected )
    //  {
    //    //CANFixedTraceRecord[] records = new CANFixedTraceRecord[1024];
    //    //UInt32 count = UsbCANGetFixedTraceTable( records, (UInt32)records.Length );
    //    //table.Clear();
    //    //for (int i = 0; i < count; i++)
    //    //{
    //    //  table.Add( records[i]);
    //    //}
    //  }
    //}

    public UInt32 CANResetFixedTrace()
    {
      if ( IsConnected )
      {
        uint rslt = UsbCANResetFixedTrace();
        CANRxBuffer.Clear();
        CAN_Sniffer_FixedTraceRecords.Clear();
        return rslt;
      }
      else
      {
        return 1;
      }
    }

    public void StartCANCording()
    {
      if (!mCANDataRecordingEnabled && !mCANDataPlaybackEnabled)
      {
        CANResetFixedTrace();
        CANRecordBuffer.Clear();
        mCANDataRecordingEnabled = true;
        if (CAN_CanCordingStarted != null)
        {
          CAN_CanCordingStarted(this, EventArgs.Empty);
        }
      }
    }

    public void StopCANCording()
    {
      if (mCANDataRecordingEnabled)
      {
        mCANDataRecordingEnabled = false;
        if (CAN_CanCordingStopped != null)
        {
          CAN_CanCordingStopped(this, EventArgs.Empty);
        }
      }
    }

    public void StartCANPlayback()
    {
      if (!mCANDataRecordingEnabled && !mCANDataPlaybackEnabled)
      {
        CANResetFixedTrace();
        CANTxBuffer.Clear();
        CANTxBuffer.AddRange(mCANPlaybackBuffer);
        mCANDataPlaybackEnabled = true;
        if (CAN_CanPlaybackStarted != null)
        {
          CAN_CanPlaybackStarted(this, EventArgs.Empty);
        }
      }
    }

    public void StopCANPlayback()
    {
      if (mCANDataPlaybackEnabled)
      {
        mCANDataPlaybackEnabled = false;
        if (CAN_CanPlaybackStopped != null)
        {
          CAN_CanPlaybackStopped(this, EventArgs.Empty);
        }
      }
    }

    #endregion Methods


  }

}
