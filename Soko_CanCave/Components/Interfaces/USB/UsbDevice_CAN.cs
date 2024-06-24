using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;

using Soko.Common.Interfaces;


namespace Soko.CanCave.Components.Interfaces.CanCaveUsb
{

  /// <remarks>
  /// Physical device interface
  /// THIS FILE TREATS ONLY THE CAN PART OF THE COMMUNICATION!
  /// </remarks>
  public partial class UsbDevice
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


    public void PeriodicMessage_Add(int bufferIndex, int DLC, int remoteID, byte[] defaultData, int periodMs)
    {
    }

    public void PeriodicMessage_Remove(int bufferIndex)
    {
    }

    public void PeriodicMessage_EnableTransmission()
  {
  }

    public void PeriodicMessage_DisableTransmission()
    {
    }


    public double StartCANTransmission( UInt32 busBaud )
    {
        if (!_deviceConnected)
      {
        throw new InvalidOperationException("Cannot start CAN transmission when device is not connected!");
      }
      if ( busBaud < 50000 || busBaud > 1000000 )
      {
        throw new InvalidOperationException( "CAN bus baud value must be contained between 50000 and 1000000" );
      }
      double err = 0;
      if ( false )//UsbCANSetConfig( busBaud, ref err ) == 0 )
      {
        //DisableCANBusTermination();
        CAN_EnabledEvent?.Invoke(this, EventArgs.Empty);
        mCANRxTxTimer.Enabled = true;
      }
      return err;
    }

    public void StopCANTransmission()
    {
        if (!_deviceConnected)
      {
        throw new InvalidOperationException( "Cannot stop CAN transmission when device is not connected!" );
      }
      
      if ( false) //UsbCANDisableComm() == 0)
      {
        CAN_DisabledEvent?.Invoke(this, EventArgs.Empty);
        mCANRxTxTimer.Enabled = false;
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
        buffFill = 0;// UsbCANGetDevTxBuffFill();

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
        //uint rslt = UsbCANResetFixedTrace();
        //CANRxBuffer.Clear();
        //CAN_Sniffer_FixedTraceRecords.Clear();
        //return rslt;
          return 1;
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
        CAN_CanCordingStarted?.Invoke(this, EventArgs.Empty);
      }
    }

    public void StopCANCording()
    {
      if (mCANDataRecordingEnabled)
      {
        mCANDataRecordingEnabled = false;
        CAN_CanCordingStopped?.Invoke(this, EventArgs.Empty);
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
        CAN_CanPlaybackStarted?.Invoke(this, EventArgs.Empty);
      }
    }

    public void StopCANPlayback()
    {
      if (mCANDataPlaybackEnabled)
      {
        mCANDataPlaybackEnabled = false;
        CAN_CanPlaybackStopped?.Invoke(this, EventArgs.Empty);
      }
    }

    private void UsbCANReadData(UsbCANData[] data, UInt32 num, ref UInt32 rdNum)
    {
    }

    private void UsbCANWriteData(UsbCANData[] data, UInt32 num, ref UInt32 wrNum)
    {
    }

    private UsbCANData CAN_CVT_CanMsgToUsbData(CanMessage msg)
    {
      // For reference on how the buffer is layed out read DS70353C page 29
      UsbCANData tmp = new UsbCANData
      {
        timestamp = msg.timestamp,
        // SID, SRR, IDE
        EcanBufferContent0 = (UInt16)(msg.remoteID << 2)
      };
      tmp.EcanBufferContent0 &= 0x1FFF;
      if (msg.isRTRFrame && msg.isXtdFrameType)
        tmp.EcanBufferContent0 += 2;
      if (msg.isXtdFrameType)
        tmp.EcanBufferContent0 += 1;
      // EIDH
      tmp.EcanBufferContent1 = (UInt16)(msg.remoteID >> 17);
      tmp.EcanBufferContent1 &= 0x0FFF;
      // EIDL, RTR, DLC
      tmp.EcanBufferContent2 = (UInt16)(msg.remoteID >> 1);
      tmp.EcanBufferContent2 &= 0xFC00;
      if (msg.isRTRFrame && !msg.isXtdFrameType)
        tmp.EcanBufferContent2 += 1 << 9;
      tmp.EcanBufferContent2 |= (UInt16)(msg.DLC & 0x0F);
      // Data 0-7
      tmp.EcanBufferContent3 = (UInt16)((msg.data[1] << 8) + msg.data[0]);
      tmp.EcanBufferContent4 = (UInt16)((msg.data[3] << 8) + msg.data[2]);
      tmp.EcanBufferContent5 = (UInt16)((msg.data[5] << 8) + msg.data[4]);
      tmp.EcanBufferContent6 = (UInt16)((msg.data[7] << 8) + msg.data[6]);
      return tmp;
    }

    private CanMessage CAN_CVT_UsbDataToCanMsg(UsbCANData src)
    {
      CanMessage tmp = new CanMessage();

      // First EcanBufferContent0 bit set denotes 1st CAN interface
      if ((src.EcanBufferContent0 & 0x8000) > 0)
      {
        tmp.physicalInterfaceNumber = 1;
      }
      else
      {
        // Second EcanBufferContent0 bit set denotes 2nd CAN interface
        if ((src.EcanBufferContent0 & 0x4000) > 0)
        {
          tmp.physicalInterfaceNumber = 2;
        }
        else
        {
          // If none of the bits is set, set it as a phy1 and write error msg on console
          tmp.physicalInterfaceNumber = 1;
          Soko.Common.Common.EventLogger.DeviceError("No interface assigned for RX can message");
        }
      }

      // Third EcanBufferContent0 bit set denotes 1st CAN interface
      if ((src.EcanBufferContent0 & 0x2000) > 0)
        tmp.messageWasForwarded = true;

      // For reference on how the buffer is layed out read DS70353C page 29
      tmp.timestamp = src.timestamp;
      // SID, SRR, IDE

      tmp.isXtdFrameType = ((src.EcanBufferContent0 & 0x0001) > 0); // IDE bit
      if (!tmp.isXtdFrameType && ((src.EcanBufferContent0 & 0x0002) > 0)) // SSR bit
        tmp.isRTRFrame = true;
      if (tmp.isXtdFrameType && ((src.EcanBufferContent2 & 0x0200) > 0)) // RTR bit
        tmp.isRTRFrame = true;

      UInt32 SID = (UInt32)((src.EcanBufferContent0 & 0x1FFF) >> 2);
      UInt32 EID = (UInt32)((src.EcanBufferContent1 & 0x0FFF) << 6); // 6 more bits in word 2
      EID |= (UInt32)((src.EcanBufferContent2 & 0xFC00) >> 10);

      if (tmp.isXtdFrameType)
        tmp.remoteID = (SID << 18) | EID;
      else
        tmp.remoteID = SID;

      tmp.DLC = (byte)(src.EcanBufferContent2 & 0x000F);

      tmp.data[0] = (byte)(src.EcanBufferContent3 & 0x00FF);
      tmp.data[1] = (byte)(src.EcanBufferContent3 >> 8);
      tmp.data[2] = (byte)(src.EcanBufferContent4 & 0x00FF);
      tmp.data[3] = (byte)(src.EcanBufferContent4 >> 8);
      tmp.data[4] = (byte)(src.EcanBufferContent5 & 0x00FF);
      tmp.data[5] = (byte)(src.EcanBufferContent5 >> 8);
      tmp.data[6] = (byte)(src.EcanBufferContent6 & 0x00FF);
      tmp.data[7] = (byte)(src.EcanBufferContent6 >> 8);

      return tmp;
    }




    #endregion Methods


  }

}
