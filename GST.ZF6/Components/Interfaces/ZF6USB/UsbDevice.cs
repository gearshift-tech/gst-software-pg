using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;
using System.Runtime.InteropServices;


using LibUsbDotNet;
using LibUsbDotNet.DeviceNotify;
using LibUsbDotNet.Main;


namespace GST.ZF6.Components.Interfaces.MechShifterUSB
{

  public partial class UsbDevice: IDisposable
  {




    #region Constants

    private static Int32 DeviceVID = 0x04D8;
    private static Int32 DevicePID = 0xFD65;

    /// <summary>
    /// Maximum time [ms] allowed to wait for the USB packet to be sent to the device
    /// </summary>
    private static readonly int mDeviceTransmitTimeoutMs = 300;

    /// <summary>
    /// Maximum time [ms] allowed to wait for the command response in connecting process
    /// </summary>
    private static readonly int mDeviceConnProcResponseTimeoutMs = 1000;

    #endregion  Constants



    #region Private fields

    // UsbDeviceFinder used to check if the device is present in the system
    private static UsbDeviceFinder MyUsbFinder = new UsbDeviceFinder(DeviceVID, DevicePID);

    private static LibUsbDotNet.UsbDevice MyUsbDevice = null;

    private static UsbEndpointReader _usbEndPointReader = null;

    private static UsbEndpointWriter _usbEndPointWriter = null;

    private static USBDeviceInfo mUsbDevInfo = new USBDeviceInfo();

    // If the class should automatically connect to USB device when available
    private bool _autoConnectEnabled = false;
    
    private static bool _deviceConnected = false;

    public string statpack = "";


    private static USBPacket_DeviceInfo mDeviceInfo = new USBPacket_DeviceInfo();

    public USBPacket_DeviceStatus mDeviceStatus = new USBPacket_DeviceStatus();

    public List<byte[]> mOutputBuffer = new List<byte[]>();

    // Last selected gear on Zf6 USB device. Value of 255 ensures the proper selection after init. See SelectGear() for reference
    private Byte _lastGearSelected = 255;

    #endregion Private fields



    #region Constructors & finalizer

    public UsbDevice()
    {
      devNotifier = DeviceNotifier.OpenDeviceNotifier();
      devNotifier.OnDeviceNotify += onDeviceNotify;

      mDeviceStatusRequesterTimer.Elapsed += new ElapsedEventHandler(mDeviceStatusRequesterTimer_Elapsed);

      if (_autoConnectEnabled)
      {
        ConnectAsync();
      }
    }

    public void Dispose()
    {
        Disconnect();
    }

    #endregion Constructors & finalizer
    


    #region Events


    public event EventHandler Disconnected;

    public event EventHandler DevicePlugged;
    public event EventHandler DeviceUnplugged;

    #endregion Events

    #region Threads


    #endregion Threads



    #region Properties

    public bool IsConnected
    {
      get { return _deviceConnected; }
    }

    /// <summary>
    /// Gets/Sets the USB auto connection functionality
    /// </summary>
    public bool AutoConnectEnabled
    {
      get { return _autoConnectEnabled; }
      set
      {
        _autoConnectEnabled = value;
        if (_autoConnectEnabled && !(IsConnected || _isTryingToConnect))
        {
          ConnectAsync();
        }
      }
    }

    #endregion Properties

    #region Private methods







    private void FreeUsbResources()
    {
      if (MyUsbDevice != null)
      {
        if (MyUsbDevice.IsOpen)
        {
          // If this is a "whole" usb device (libusb-win32, linux libusb-1.0)
          // it exposes an IUsbDevice interface. If not (WinUSB) the 
          // 'wholeUsbDevice' variable will be null indicating this is 
          // an interface of a device; it does not require or support 
          // configuration and interface selection.
          IUsbDevice wholeUsbDevice = MyUsbDevice as IUsbDevice;
          if (!ReferenceEquals(wholeUsbDevice, null))
          {
            // Release interface #0.
            wholeUsbDevice.ReleaseInterface(0);
          }
          MyUsbDevice.Close();
        }
        MyUsbDevice = null;
        // Free usb resources
        LibUsbDotNet.UsbDevice.Exit();
      }
    }

    private static byte[] PacketToByteArray(object packet)
    {

      int len = Marshal.SizeOf(packet);

      byte[] arr = new byte[len];

      IntPtr ptr = Marshal.AllocHGlobal(len);

      Marshal.StructureToPtr(packet, ptr, true);

      Marshal.Copy(ptr, arr, 0, len);
      Marshal.FreeHGlobal(ptr);

      return arr;

    }

    private static T ByteArrayToPacket<T>(byte[] bytes) where T : struct
    {
      GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
      T stuff = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
      handle.Free();
      return stuff;
    }


    #endregion Private methods

    #region Public methods





    /// <summary>
    /// Disconnects from the device and releases all USB resources.
    /// </summary>
    public void Disconnect()
    {
      try
      {
        bool wasConnected = _deviceConnected;
        // Set the flags
        _isTryingToConnect = false;
        _deviceConnected = false;
        // Unhook the data received event
        if (_usbEndPointReader != null && _usbEndPointReader.DataReceivedEnabled)
        {
          _usbEndPointReader.DataReceivedEnabled = false;
          _usbEndPointReader.DataReceived -= (OnRxEndPointData);
        }
        // Kill the status requester timer
        mDeviceStatusRequesterTimer.Enabled = false;
        // Kill the TX thread
        if (USBDeviceTxThread != null)
        {
          USBDeviceTxThread.Abort();
          USBDeviceTxThread = null;
        }
        // Release all the USB resources
        FreeUsbResources();
        // Fire the event only if the device was connected when disconnect function was called
        if (wasConnected && Disconnected != null)
        {
          Disconnected(this, EventArgs.Empty);
        }
        
      }
      catch (Exception ex)
      {
        Soko.Common.Common.EventLogger.DeviceError("Disconnect: " + ex.Message);
      }
    }

    #endregion Public methods
  }
}
