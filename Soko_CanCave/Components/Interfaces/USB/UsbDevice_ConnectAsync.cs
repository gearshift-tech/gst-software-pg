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

using Soko.Common.Common;

namespace Soko.CanCave.Components.Interfaces.CanCaveUsb
{

  public partial class UsbDevice
  {

    private static AutoResetEvent _connectAsyncDeviceVersionReceived = new AutoResetEvent(false);
    private static AutoResetEvent _connectAsyncDeviceConfigReceived = new AutoResetEvent(false);
    private static AutoResetEvent _connectAsyncDeviceStatusReceived = new AutoResetEvent(false);
    private static AutoResetEvent _connectAsyncCommStartCmdReceived = new AutoResetEvent(false);

    public event EventHandler Connected;
    public event EventHandler FailedToConnect;

    /// <summary>
    /// True if device connection is being established at the moment
    /// </summary>
    private static bool _isTryingToConnect = false;

    /// <summary>
    /// Fires the FailedToConnect event
    /// </summary>
    protected void OnFailedToConnect()
    {
      FailedToConnect?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Fires the Connected event
    /// </summary>
    protected void OnConnected()
    {
      // Set the value to undefined state to make sure the gear is selected right after connection. See SelectGear() for reference
      _lastGearSelected = 255;
      EventLogger.DeviceInfo("Connect: Device successfully connected !");
      Connected?.Invoke(this, EventArgs.Empty);
    }


    /// <summary>
    /// Thread that tries to connect to USB device
    /// </summary>
    private static Thread ConnectAsyncThread = null;

    /// <summary>
    /// Begins asynchronous USB connecting process.
    /// Watch events to catch when the device is connected
    /// </summary>
    public void ConnectAsync()
    {
      try
      {
        // Check if the device isnt already connected
        if ((MyUsbDevice != null && MyUsbDevice.IsOpen) || _isTryingToConnect)
        {
          EventLogger.DeviceDebug("Connect: Device already connected or busy trying to connect");
          return;
        }
        // Set the flag so that the rest of the code knows that we're trying to establish connection
        _isTryingToConnect = true;
        // Start new thread that will take care of the connection establishment
        // and will end after the connection is established or an error occurs
        ConnectAsyncThread = new Thread(ConnectAsyncThreadMethod)
        {
          IsBackground = true
        };
        ConnectAsyncThread.Start();
      }
      catch (Exception e)
      {
        EventLogger.DeviceError("Connect: Exception occured in ConnectAsync: " + e.Message);
      }
    }


    /// <summary>
    /// Thread method that performs the asynchronous connect process
    /// </summary>
    private void ConnectAsyncThreadMethod()
    {
      try
      {
        EventLogger.DeviceInfo("Connect: Trying to connect to USB...");
        for (int i = 0; i < 20; i++)
        {
          // Find and open the usb device.
          MyUsbDevice = LibUsbDotNet.UsbDevice.OpenUsbDevice(MyUsbFinder);

          if (MyUsbDevice != null)
          {
            EventLogger.DeviceInfo("Connect: USB device found.");
            break;
          }
          else
          {
            System.Threading.Thread.Sleep(50);
          }
        }
        // If the device is open and ready
        if (MyUsbDevice == null)
        {
          throw new TimeoutException("USB device not found in system. Failed to connect");
        }

        

        // If this is a "whole" usb device (libusb-win32, linux libusb)
        // it will have an IUsbDevice interface. If not (WinUSB) the 
        // variable will be null indicating this is an interface of a 
        // device.
        IUsbDevice wholeUsbDevice = MyUsbDevice as IUsbDevice;
        if (!ReferenceEquals(wholeUsbDevice, null))
        {
          // This is a "whole" USB device. Before it can be used, 
          // the desired configuration and interface must be selected.

          // Select config #1
          wholeUsbDevice.SetConfiguration(1);

          // Claim interface #0.
          wholeUsbDevice.ClaimInterface(0);
        }
        EventLogger.DeviceInfo("Connect: SetConfig & Intercace claim OK");

        // open read endpoint 1.
        _usbEndPointReader = MyUsbDevice.OpenEndpointReader(ReadEndpointID.Ep01);

        // open write endpoint 1.
        _usbEndPointWriter = MyUsbDevice.OpenEndpointWriter(WriteEndpointID.Ep01);
        EventLogger.DeviceInfo("Connect: Endpoints OK");

        // Enable the data receiving on reader event
        _usbEndPointReader.DataReceived += (OnRxEndPointData);
        _usbEndPointReader.DataReceivedEnabled = true;

        // At this point we can send and receive config packets between the device and host

        // Request and wait for the device info
        _connectAsyncDeviceVersionReceived.Reset();
        UsbSendUnqueuedCommandOnlyPacket(USBPacketCommandCode.CMD_GET_DEV_INFO);       
        if (!_connectAsyncDeviceVersionReceived.WaitOne(mDeviceConnProcResponseTimeoutMs))
        {
          throw new TimeoutException("Timeout at getting device info");
        }
        Console.WriteLine("Got dev info");
        ///TODO: CHECK THE HARDWARE VERSION!

        ///TODO: GET SERIAL AND GUID

        // Request and wait for the device status info
        _connectAsyncDeviceStatusReceived.Reset();
        UsbSendUnqueuedCommandOnlyPacket(USBPacketCommandCode.CMD_GET_DEV_STATUS);
        if (!_connectAsyncDeviceStatusReceived.WaitOne(mDeviceConnProcResponseTimeoutMs))
        {
          throw new TimeoutException("Timeout at getting device status");
        }
        Console.WriteLine("Got dev status");
        // Send 'init complete' packet to let the device know that we're ready to start normal data exchange
        _connectAsyncCommStartCmdReceived.Reset();
        UsbSendUnqueuedCommandOnlyPacket(USBPacketCommandCode.CMD_USB_COMM_START);
        if (!_connectAsyncCommStartCmdReceived.WaitOne(mDeviceConnProcResponseTimeoutMs))
        {
          throw new TimeoutException("Timeout at getting CommStart response");
        }

        // At this moment the device thinks that PC is connected. PC must frequently send any packets to the device so that 
        // the device can detect the connection loss

        _deviceConnected = true;
        _isTryingToConnect = false;
        // Start TX thread
        USBDeviceTxThread = new Thread(USBDeviceTxThreadFunction)
        {
          IsBackground = true
        };
        USBDeviceTxThread.Start();
        // Start the timer that will periodically ask the device for status (polling)
        mDeviceStatusRequesterTimer.Enabled = true;

        // The device is now considered to be connected, fire the events etc..
        OnConnected();
        //This function is finished
        return;
      }
      catch (Exception ex)
      {
        EventLogger.DeviceError("Connect exception:" + ex.Message);
        // Try to close and free USB device resources
        Disconnect();
        // Fire the event
        OnFailedToConnect();
      }
    }


  }
}