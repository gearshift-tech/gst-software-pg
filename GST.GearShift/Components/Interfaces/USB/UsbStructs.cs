using System;
using System.Runtime.InteropServices;


namespace GST.Gearshift.Components.Interfaces.USB//GST.Gearshift.Components.Interfaces.USB
{


  // Common USB communication structures //----------------------------------------------------------//
  //-------------------------------------------------------------------------------------------------//

    /// <summary>
    /// Error and state codes returned by the usbOpen and usbDeviceState functions
    /// </summary>
    enum errorCodes
    {
        ERR_NONE = 0,
        ERR_REG_WND_CLASS = 1,
        ERR_CREATE_WINDOW = 2,
        ERR_REG_DEV_NOTIFY = 3,
        ERR_GET_MESSAGE = 4,
        ERR_CREATE_THR = 5,
        ERR_ALLREADY_CONNECTED = 6,
        ERR_DEVICE_DISCONNECTED = 7,
        ERR_NO_DEVICES_TO_CONNECT = 8,
        ERR_UNABLE_TO_LOAD_USBAPI_LIBRARY = 9,
        ERR_USB_OPEN = 10,
        ERR_TX = 11,
        ERR_RX = 12,
        ERR_CONN_CONFIG = 13,
        ERR_WRONG_FIRMWARE = 14,
        ERR_DEVICE_INIT = 15,
        ERR_MEM_ALLOC = 16,
        ERR_UNABLE_TO_START_TIMER = 17,
        ERR_RX_BUFF_OVERFLOW = 18,
        ERR_TRANSMISSION_FAILURE = 19,
        ERR_TOO_MANY_USB_DEVICES = 20,
        ERR_DEVICE_NOT_RESPONDING = 21
    };

    /// <summary>
    /// Device state codes
    /// </summary>
    enum DeviceStates
    {
        DEV_DISCONNECTED = 0,
        DEV_CONNECTED = 3,
        DEV_DISCONNECTING = 4,
        DEV_ERROR = 5,
    };

//     /// <summary>
//     /// Device window messages list
//     /// </summary>
//     [StructLayout( LayoutKind.Sequential )]
//     public struct DeviceWMsList
//     {
//       public Int32 mDevice_WM_Error = 0;
//       public Int32 mDevice_WM_Disconnected = 0;
//       public Int32 mDevice_WM_Overcurrent = 0;
//       public Int32 mDevice_WM_OBDDataRcvd = 0;
//       public Int32 mDevice_WM_CanDataRcvd = 0;
//     }

    /// <summary>
    /// Device state structure
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DeviceState
    {
        public UInt16 appState;
        public UInt16 appErrorCode;
        public UInt16 dllState;
        public UInt16 dllErrorCode;
        public UInt16 buffFill;
        public UInt16 overCurrentPorts;
    }

    /// <summary>
    /// Firmware version structure
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct FirmwareVersion
    {
        public byte major;
        public byte minor;
    }

    /// <remarks>
    /// Connection status code 
    /// </remarks>
    enum CONN_STAT
    {
        CONN_OK = 0x01, //device is connected
        CONN_DISCONNECTED = 0x02, // device not connected
        CONN_ERR = 0x03 //device connection error
    };

    /// <remarks>
    /// Error status code
    /// </remarks>
    enum ERR_STAT
    {
        ERR_CONN = 0x01, // connection error
        ERR_NONE = 0x02, // generic error
        ERR_OVC = 0x03, // overcurrent on channels
        ERR_BUFF_OVF = 0x04 // output buffer overflow
    };


    // DAQ functionality USB communication structures //-----------------------------------------------//
    //-------------------------------------------------------------------------------------------------//

    /// <summary>
    /// USB DAQ TX data (sent to the device)
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = 21)]
    public struct UsbDAQTxData
    {
      [MarshalAs(UnmanagedType.U4)]
      public UInt32 ID;
      [MarshalAs(UnmanagedType.U4)]
      public UInt32 AO1;
      [MarshalAs(UnmanagedType.U4)]
      public UInt32 AO2;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
      public byte[] Pwm;
    }

    /// <summary>
    /// USB DAQ RX data (received from the device
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = 72)]
    unsafe public struct UsbDAQRxData
    {
      [MarshalAs(UnmanagedType.U4)]
      public UInt32 ID;
      [MarshalAs(UnmanagedType.U4)]
      public UInt32 responseToID;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 18)]
      public UInt16[] current;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
      public UInt16[] pressure;
    }


    // CAN functionality USB communication structures //-----------------------------------------------//
    //-------------------------------------------------------------------------------------------------//

    ///// <summary>
    ///// USB CAN TX data (sent to the device)
    ///// </summary>
    //[StructLayout( LayoutKind.Sequential, Size = 14, Pack = 1 )]
    //public struct UsbCANData
    //{
    //  [MarshalAs(UnmanagedType.U4)]
    //  public UInt32 remoteID;
    //  [MarshalAs(UnmanagedType.U1)]
    //  public Byte isXtdFrameType;
    //  [MarshalAs(UnmanagedType.U1)]
    //  public Byte DLC;
    //  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    //  public byte[] data;
    //}

    /// <summary>
    /// USB CAN data
    /// </summary>
    [StructLayout( LayoutKind.Sequential, Size = 19, Pack = 1 )]
    unsafe public struct UsbCANData
    {
      [MarshalAs(UnmanagedType.U4)]
      public UInt32 remoteID;
      [MarshalAs(UnmanagedType.U1)]
      public Byte isXtdFrameType;
      [MarshalAs(UnmanagedType.U1)]
      public Byte isRTRFrame;
      [MarshalAs(UnmanagedType.U1)]
      public Byte DLC;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
      public byte[] data;
      [MarshalAs( UnmanagedType.U4 )]
      public UInt32 timestamp;
    }

    /// <summary>
    /// USB CAN fixed trace record
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = 42, Pack = 1)]
    unsafe public struct UsbCANFixedTraceRecord
    {
      [MarshalAs(UnmanagedType.U4)]
      public UInt32 Count;
      public UsbCANData LastMessage;
      [MarshalAs(UnmanagedType.U4)]
      public UInt32 TimeStamp;
      [MarshalAs(UnmanagedType.U4)]
      public UInt32 CycleTimeInst;
      [MarshalAs(UnmanagedType.U4)]
      public UInt32 CycleTimeMin;
      [MarshalAs(UnmanagedType.U4)]
      public UInt32 CycleTimeMax;
      [MarshalAs(UnmanagedType.U4)]
      public UInt32 CycleTimeAvg;
    }


    // OBD functionality USB communication structures //-----------------------------------------------//
    //-------------------------------------------------------------------------------------------------//

    /// <summary>
    /// USB OBD TX data (sent to the device)
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = 51, Pack = 1)]
    public struct UsbOBDTxData
    {
      [MarshalAs(UnmanagedType.U1)]
      public byte charCount;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
      public char[] chars;
    }

    /// <summary>
    /// USB OBD RX data (received from the device
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = 57, Pack = 1)]
    unsafe public struct UsbOBDRxData
    {
      [MarshalAs(UnmanagedType.U4)]
      public UInt32 ID;
      [MarshalAs( UnmanagedType.U1 )]
      public byte          respToMode;
      [MarshalAs( UnmanagedType.U1 )]
      public byte          respToPID;
      [MarshalAs( UnmanagedType.U1 )]
      public byte          bytesCount;
      [MarshalAs( UnmanagedType.ByValArray, SizeConst = 50 )]
      public byte[]        bytes;

    }


}