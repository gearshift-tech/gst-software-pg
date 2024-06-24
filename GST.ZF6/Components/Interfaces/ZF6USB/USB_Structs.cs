using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;


namespace GST.ZF6.Components.Interfaces.MechShifterUSB
{
  //-----------------------------------------------------------------INPUT_DATA_KLINE-------------------------------------------------------------
  //----------------------------------------------------------------------------------------------------------------------------------------------
  /// <summary>
  /// K-LINE received data structure
  /// </summary>
  [StructLayout(LayoutKind.Sequential, Size = 8, Pack = 1)]
  public struct INPUT_DATA_KLINE
  {
    [MarshalAs(UnmanagedType.U2)]
    public UInt16 data;
    [MarshalAs(UnmanagedType.U4)]
    public UInt32 timestamp;
    [MarshalAs(UnmanagedType.U2)]
    public UInt16 U2STA;
  }

  //-----------------------------------------------------------------OUTPUT_DATA_PHERIPERIALS-----------------------------------------------------
  //----------------------------------------------------------------------------------------------------------------------------------------------
  /// <summary>
  /// Device output data, PHERIPERIALS EVENT type
  /// </summary>
  [StructLayout(LayoutKind.Sequential, Size = 13, Pack = 1)]
  public struct OUTPUT_DATA_PHERIPERIALS
  {
    // Timestamp at which this actions should be taken
    [MarshalAs(UnmanagedType.U4)]
    public UInt32 Timestamp;

    // Type of action that is to be taken, for this struct it must match OUT_EVENT_TYPE.PHERIPHERIALS_EVT
    [MarshalAs(UnmanagedType.U1)]
    public OUT_EVENT_TYPE EventType;

    // If baudrate should be changed, 0 - no, 1 - yes
    [MarshalAs(UnmanagedType.U1)]
    public YESNOENUM setBrg;

    // Baud rate register value to be set
    [MarshalAs(UnmanagedType.U2)]
    public UInt16 BRG;

    // If timestamp timer should be reset, 0 - no, 1 - yes
    [MarshalAs(UnmanagedType.U1)]
    public YESNOENUM resetTimestamp;

    // KLINE pulldown signal action
    [MarshalAs(UnmanagedType.U1)]
    public PHP_EVENT_TYPE KLINE_PullDown;

    // Relay enable signal action
    [MarshalAs(UnmanagedType.U1)]
    public PHP_EVENT_TYPE relayState;

    // WakeUp signal action
    [MarshalAs(UnmanagedType.U1)]
    public PHP_EVENT_TYPE wakeUpSignal;

    // Timer freezing action
    [MarshalAs(UnmanagedType.U1)]
    public PHP_EVENT_TYPE timerFreezer;
  }

  //-----------------------------------------------------------------OUTPUT_DATA_KLINE------------------------------------------------------------
  //----------------------------------------------------------------------------------------------------------------------------------------------
  /// <summary>
  /// Device output data, K-LINE TX type
  /// </summary>
  [StructLayout(LayoutKind.Sequential, Size = 38, Pack = 1)]
  public struct OUTPUT_DATA_KLINE
  {
    [MarshalAs(UnmanagedType.U4)]
    public UInt32 Timestamp;

    [MarshalAs(UnmanagedType.U1)]
    public OUT_EVENT_TYPE EventType;

    [MarshalAs(UnmanagedType.U1)]
    public byte dataLength;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
    public UInt16[] data;
  }
}