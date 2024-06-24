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
    // IDeviceNotifier used to detect the device plug in/out to the PC's USB port
    private static IDeviceNotifier devNotifier;

    /// <summary>
    /// Executed when a WMDeviceChange arrives
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void onDeviceNotify(object sender, DeviceNotifyEventArgs e)
    {
      if (e.Device == null)
      {
        return;
      }

      if (e.Device.IdVendor == DeviceVID && e.Device.IdProduct == DevicePID)
      {
        if (e.EventType == EventType.DeviceArrival)
        {
          EventLogger.DeviceInfo("Device plugged in!");
          if (AutoConnectEnabled)
          {
            ConnectAsync();
          }
          DevicePlugged?.Invoke(this, EventArgs.Empty);
        }
        if (e.EventType == EventType.DeviceRemoveComplete)
        {
          EventLogger.DeviceInfo("Device unplugged!");
          DeviceUnplugged?.Invoke(this, EventArgs.Empty);
          // To make sure the disconnection process occurs after the device is removed
          if (_deviceConnected)
          {
            Disconnect();
          }
        }
      }
    }
  }
}
