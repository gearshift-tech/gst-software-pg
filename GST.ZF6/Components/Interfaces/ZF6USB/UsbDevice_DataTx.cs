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

namespace GST.ZF6.Components.Interfaces.MechShifterUSB
{

  public partial class UsbDevice
  {
    private static readonly double Const_DeviceStatusRefreshInterval = 30; // Time in miliseconds between device status requests
    private static readonly int    Const_DeviceStatusRequestMaxUnrepliedCount = 300; // Maximum number of 'timeouts' before we assume that the device has disconnected

    private List<byte[]> USB_TxBuffer = new List<byte[]>();

    private bool mDeviceStatusRequested = false;
    private bool mDeviceStatusLastRequestReplied = true;
    private int mDeviceStatusUnrepliedRequestsCount = 0;
    private int mDeviceMaxPacketsToBeSentUntilNewStatus = 0; // Number of packets that can be safely sent until the next status update is received

    System.Timers.Timer mDeviceStatusRequesterTimer = new System.Timers.Timer(Const_DeviceStatusRefreshInterval);


    void mDeviceStatusRequesterTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
      if (!mDeviceStatusLastRequestReplied)
      {
        // This means that since the last status request was not replied properly
        mDeviceStatusUnrepliedRequestsCount++;
        if (mDeviceStatusUnrepliedRequestsCount >= Const_DeviceStatusRequestMaxUnrepliedCount)
        {
          Disconnect();
          Console.WriteLine("Device unresponsive!!");
          mDeviceStatusUnrepliedRequestsCount = 0;
        }
      }
      else
      {
        mDeviceStatusLastRequestReplied = false;
        mDeviceStatusUnrepliedRequestsCount = 0;
      }  
      mDeviceStatusRequested = true;
    }


    /// <summary>
    /// Thread manages the data transfer to the device
    /// </summary>
    private static Thread USBDeviceTxThread = null;


    private void USBDeviceTxThreadFunction()
    {
      ErrorCode ec = ErrorCode.None;
      USBPacket_Generic pkt = new USBPacket_Generic();
      int bytesWritten;
      while (_deviceConnected)
      {
        try
        {
          Thread.Sleep(1); // Sleep 1ms if the buffer is empty

          while (USB_TxBuffer.Count > 0 )//&& mDeviceMaxPacketsToBeSentUntilNewStatus > 0)
          {
            ec = _usbEndPointWriter.Write(USB_TxBuffer[0], mDeviceTransmitTimeoutMs, out bytesWritten);
            if (ec != ErrorCode.None)
            {
              EventLogger.DeviceError("Txdthd err: " + LibUsbDotNet.UsbDevice.LastErrorString);
            }
            else
            {
              USB_TxBuffer.RemoveAt(0);
            }
            mDeviceMaxPacketsToBeSentUntilNewStatus--;
          }

          if (mDeviceStatusRequested)
          {
            pkt.CommandCode = USBPacketCommandCode.CMD_GET_DEV_STATUS;
            ec = _usbEndPointWriter.Write(PacketToByteArray(pkt), mDeviceTransmitTimeoutMs, out bytesWritten);
            if (ec != ErrorCode.None)
            {
              EventLogger.DeviceError("Txd: " + LibUsbDotNet.UsbDevice.LastErrorString + " at POLL data TX");
            }
            else
            {
              mDeviceStatusRequested = false;
            }
          }
        }
        catch (Exception e)
        {
          EventLogger.DeviceError("Txd: Exception: " + e.Message);
        }
      }
      EventLogger.DeviceDebug("Tx thread finished.");
    }

  }
}