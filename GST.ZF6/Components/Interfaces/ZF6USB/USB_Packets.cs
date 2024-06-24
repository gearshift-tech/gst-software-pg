using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;


using LibUsbDotNet;
using LibUsbDotNet.Main;



namespace GST.ZF6.Components.Interfaces.MechShifterUSB
{

  //-----------------------------------------------------------------USBPacket_Generic------------------------------------------------------------
  //----------------------------------------------------------------------------------------------------------------------------------------------
  /// <summary>
  /// Generic USB packet structure
  /// </summary>
  [StructLayout(LayoutKind.Sequential, Size = 1, Pack = 1)]
  public struct USBPacket_Generic
  {
    /// <summary>
    /// Byte describing the packet command code
    /// </summary>
    [MarshalAs(UnmanagedType.U1)]
    public USBPacketCommandCode CommandCode;
  }

  //-----------------------------------------------------------------USBPacket_INPUT_DATA_KLINE---------------------------------------------------
  //----------------------------------------------------------------------------------------------------------------------------------------------
  /// <summary>
  /// K-LINE RX data USB packet structure
  /// </summary>
  [StructLayout(LayoutKind.Sequential, Size = 58, Pack = 1)]
  public struct USBPacket_INPUT_DATA_KLINE
  {
    [MarshalAs(UnmanagedType.U1)]
    public USBPacketCommandCode CommandCode;

    //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
    [MarshalAs(UnmanagedType.Struct)]
    public INPUT_DATA_KLINE data;
  }

  //-----------------------------------------------------------------USBPacket_OUTPUT_DATA_KLINE--------------------------------------------------
  //----------------------------------------------------------------------------------------------------------------------------------------------
  /// <summary>
  /// Device output K-Line data USB packet structure
  /// </summary>
  [StructLayout(LayoutKind.Sequential, Size = 39, Pack = 1)]
  public struct USBPacket_OUTPUT_DATA_KLINE
  {
    [MarshalAs(UnmanagedType.U1)]
    public USBPacketCommandCode CommandCode;

    [MarshalAs(UnmanagedType.Struct)]
    public OUTPUT_DATA_KLINE data;
  }

  //-----------------------------------------------------------------USBPacket_OUTPUT_DATA_PHERIPERIALS-------------------------------------------
  //----------------------------------------------------------------------------------------------------------------------------------------------
  /// <summary>
  /// Device output Pheriperials data USB packet structure
  /// </summary>
  [StructLayout(LayoutKind.Sequential, Size = 39, Pack = 1)]
  public struct USBPacket_OUTPUT_DATA_PHERIPERIALS
  {
    [MarshalAs(UnmanagedType.U1)]
    public USBPacketCommandCode CommandCode;

    [MarshalAs(UnmanagedType.Struct)]
    public OUTPUT_DATA_PHERIPERIALS data;
  }

  //-----------------------------------------------------------------USBPacket_GEAR_SELECTION-----------------------------------------------------
  //----------------------------------------------------------------------------------------------------------------------------------------------
  /// <summary>
  /// Gear selection USB packet structure
  /// </summary>
  [StructLayout(LayoutKind.Sequential, Size = 2, Pack = 1)]
  public struct USBPacket_GEAR_SELECT
  {
    [MarshalAs(UnmanagedType.U1)]
    public USBPacketCommandCode CommandCode;

    [MarshalAs(UnmanagedType.U1)]
    public byte GearCode;
  }

  //-----------------------------------------------------------------USBPacket_EDS_DATA-----------------------------------------------------------
  //----------------------------------------------------------------------------------------------------------------------------------------------
  /// <summary>
  /// Gear selection USB packet structure
  /// </summary>
  [StructLayout(LayoutKind.Sequential, Size = 3, Pack = 1)]
  public struct USBPacket_EDS_DATA
  {
    [MarshalAs(UnmanagedType.U1)]
    public USBPacketCommandCode CommandCode;

    [MarshalAs(UnmanagedType.U1)]
    public byte EDS5Val;

    [MarshalAs(UnmanagedType.U1)]
    public byte EDS6Val;
  }

  //-----------------------------------------------------------------USBPacket_GEARBOX_SELECTION-----------------------------------------------------
  //----------------------------------------------------------------------------------------------------------------------------------------------
  /// <summary>
  /// Gear selection USB packet structure
  /// </summary>
  [StructLayout(LayoutKind.Sequential, Size = 2, Pack = 1)]
  public struct USBPacket_GEARBOX_SELECT
  {
    [MarshalAs(UnmanagedType.U1)]
    public USBPacketCommandCode CommandCode;

    [MarshalAs(UnmanagedType.U1)]
    public byte GearboxCode;
  }

  //-----------------------------------------------------------------USBPacket_UI_UPDATE----------------------------------------------------------
  //----------------------------------------------------------------------------------------------------------------------------------------------
  /// <summary>
  /// Device impersonation data USB packet structure
  /// </summary>
  [StructLayout(LayoutKind.Sequential, Size = 6, Pack = 1)]
  public struct USBPacket_UI_UPDATE
  {
    [MarshalAs(UnmanagedType.U1)]
    public USBPacketCommandCode CommandCode;

    [MarshalAs(UnmanagedType.U1)]
    public byte GPL1;

    [MarshalAs(UnmanagedType.U1)]
    public byte GPL2;

  }

  //-----------------------------------------------------------------USBPacket_DeviceStatus-------------------------------------------------------
  //----------------------------------------------------------------------------------------------------------------------------------------------
  /// <summary>
  /// Device status USB packet structure
  /// </summary>
  [StructLayout(LayoutKind.Sequential, Size = 10, Pack = 1)]
  public struct USBPacket_DeviceStatus
  {

    /// <summary>
    /// Byte describing the packet command code, see USBPacketCommandCode  declaration for more details
    /// </summary>
    [MarshalAs(UnmanagedType.U1)]
    public USBPacketCommandCode CommandCode;

    /// <summary>
    /// If the K-Line interface is currently enabled
    /// </summary>
    [MarshalAs(UnmanagedType.U1)]
    public byte KLINE_IsEnabled; 

    /// <summary>
    /// Actual Baud rate of the K-Line interface
    /// </summary>
    [MarshalAs(UnmanagedType.U4)]
    public UInt32 KLINE_SelectedBaud;      

    /// <summary>
    /// Messages count in the K-Line interface receive buffer
    /// </summary>
    [MarshalAs(UnmanagedType.U2)]
    public UInt16 KLINE_RxBuffFill;

    /// <summary>
    /// Messages count in the K-Line interface transmit buffer
    /// </summary>
    [MarshalAs(UnmanagedType.U2)]
    public UInt16 KLINE_TxBuffFill;
  }

  //-----------------------------------------------------------------USBPacket_DeviceSerialNo-----------------------------------------------------
  //----------------------------------------------------------------------------------------------------------------------------------------------
  /// <summary>
  /// Device serial number & GUID USB packet structure
  /// </summary>
  [StructLayout(LayoutKind.Sequential, Size = 53, Pack = 1)]
  public struct USBPacket_DeviceSerialNo
  {
    /// <summary>
    /// Byte describing the packet command code, see USBPacketCommandCode  declaration for more details
    /// </summary>
    [MarshalAs(UnmanagedType.U1)]
    public USBPacketCommandCode CommandCode;

    /// <summary>
    /// Array of 15 bytes being the device serial number (14 numbers + null)
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
    public byte[] SerialString;

    /// <summary>
    /// Array of 37 bytes being the device serial GUID (32 GUID chars + 4x '-' + null)
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 37)]
    public byte[] GUIDString;
  }

  //-----------------------------------------------------------------USBPacket_DeviceInfo---------------------------------------------------------
  //----------------------------------------------------------------------------------------------------------------------------------------------
  /// <summary>
  /// Device firmware version USB packet structure
  /// </summary>
  [StructLayout(LayoutKind.Sequential, Size = 32, Pack = 1)]
  public struct USBPacket_DeviceInfo
  {
    /// <summary>
    /// Byte describing the packet command code, see USBPacketCommandCode  declaration for more details
    /// </summary>
    [MarshalAs(UnmanagedType.U1)]
    public USBPacketCommandCode CommandCode;

    /// <summary>
    /// Firmware major version number
    /// </summary>
    [MarshalAs(UnmanagedType.U2)]
    public UInt16 FirmwareVersion_Major;    

    /// <summary>
    /// Firmware minor version number
    /// </summary>
    [MarshalAs(UnmanagedType.U2)]
    public UInt16 FirmwareVersion_Minor;   

    /// <summary>
    /// Firmware build version number
    /// </summary>
    [MarshalAs(UnmanagedType.U2)]
    public UInt16 FirmwareVersion_Build;  
    
    /// <summary>
    /// Hardware major version number
    /// </summary>
    [MarshalAs(UnmanagedType.U2)]
    public UInt16 HardwareVersion_Major;   

    /// <summary>
    /// Hardware major version number
    /// </summary>
    [MarshalAs(UnmanagedType.U2)]
    public UInt16 HardwareVersion_Minor; 

    /// <summary>
    /// Hardware iteration number
    /// </summary>
    [MarshalAs(UnmanagedType.U2)]
    public UInt16 HardwareVersion_Iteration;

    /// <summary>
    /// Array of 15 bytes being the device serial number (14 numbers + null)
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
    public char[] SerialString;

    ///// <summary>
    ///// Array of 37 bytes being the device serial GUID (32 GUID chars + 4x '-' + null)
    ///// </summary>
    //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 37)]
    //public char[] GUIDString;
		
    /// <summary>
    /// Size of the K-Line interface receive buffer
    /// </summary>
    [MarshalAs(UnmanagedType.U2)]
    public UInt16 KLINE_RxBuffSize;

    /// <summary>
    /// Size of the K-Line interface transmit buffer  
    /// </summary>
    [MarshalAs(UnmanagedType.U2)]
    public UInt16 KLINE_TxBuffSize; 

  }




}