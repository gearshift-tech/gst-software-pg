using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using LibUsbDotNet;
using LibUsbDotNet.DeviceNotify;
using LibUsbDotNet.Main;

namespace GST.Gearshift.Components.Forms
{
    public partial class FirmwareUpdateForm : Form
    {
        // IDeviceNotifier used to detect the device plug in/out to the PC's USB port
        private static IDeviceNotifier devNotifier;

        private static Int32 _bootloaderVID = 0x04D8;
        private static Int32 _bootloaderPID = 0x003C;

        private static Int32 _deviceVID = 0x04D8;
        private static Int32 _devicePID = 0xFD64;

        public FirmwareUpdateForm()
        {
            InitializeComponent();

            // Initialize and handle events for detection of the device in bootloader mode
            devNotifier = DeviceNotifier.OpenDeviceNotifier();
            devNotifier.OnDeviceNotify += onDeviceNotify;

            if (IsDevicePresent()) DevicePresent(); else DeviceNotPresent();
        }

        /// <summary>
        /// Executed when a WMDeviceChange arrives
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onDeviceNotify(object sender, DeviceNotifyEventArgs e)
        {
            if (e.Device.IdVendor == _bootloaderVID && e.Device.IdProduct == _bootloaderPID)
            {
                if (e.EventType == EventType.DeviceArrival)
                {
                    Console.WriteLine("BL PLUGGED");
                    BootloaderPresent();
                }
                if (e.EventType == EventType.DeviceRemoveComplete)
                {
                    Console.WriteLine("BL UNPLUGGED");
                    BootloaderNotPresent();
                }
            }
            if (e.Device.IdVendor == _deviceVID && e.Device.IdProduct == _devicePID)
            {
                IsDevicePresent();
                if (e.EventType == EventType.DeviceArrival)
                {
                    Console.WriteLine("EZS PLUGGED");
                    DevicePresent();
                }
                if (e.EventType == EventType.DeviceRemoveComplete)
                {
                    Console.WriteLine("EZS UNPLUGGED");
                    DeviceNotPresent();
                }
            }
        }

        private void DevicePresent()
        {
            devicePresenceButton.Text = "Device present";
            devicePresenceButton.ImageDisabled = Components.Properties.Resources.FirmwareUpdateForm_ThumbsUp_256x256;
        }

        private void DeviceNotPresent()
        {
            devicePresenceButton.Text = "Device not present";
            devicePresenceButton.ImageDisabled = Components.Properties.Resources.FirmwareUpdateForm_Warning_256x256;
        }

        private void BootloaderPresent()
        {
            bootloaderPresenceButton.Text = "Bootloader present";
            bootloaderPresenceButton.ImageDisabled = Components.Properties.Resources.FirmwareUpdateForm_ThumbsUp_256x256;
            devicePresenceButton.Text = "Device not present";
            devicePresenceButton.ImageDisabled = Components.Properties.Resources.FirmwareUpdateForm_ThumbsUp_256x256;

            System.Diagnostics.Process.Start("GearShift Firmware Updater.exe");
        }

        private void BootloaderNotPresent()
        {
            bootloaderPresenceButton.Text = "Bootloader not present";
            bootloaderPresenceButton.ImageDisabled = Components.Properties.Resources.FirmwareUpdateForm_Warning_256x256;
        }

        private bool IsDevicePresent()
        {
            // Find and open the usb device.
            UsbDeviceFinder MyUsbFinder = new UsbDeviceFinder(_deviceVID, _devicePID);
            LibUsbDotNet.UsbDevice MyUsbDevice = LibUsbDotNet.UsbDevice.OpenUsbDevice(MyUsbFinder);

            if (MyUsbDevice != null)
            {
                Console.WriteLine("Device present");
                MyUsbDevice.Close();
                return true;
            }
            else
            {
                Console.WriteLine("Device not present");
                return false;
            }
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("EBL.exe");
        }

    }
}
