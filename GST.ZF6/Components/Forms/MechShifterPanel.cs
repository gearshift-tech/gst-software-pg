using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GST.ZF6.Components.Interfaces.MechShifterUSB;

namespace GST.ZF6.Components.Forms
{
  public partial class MechShifterPanel : UserControl
  {

    public event EventHandler StartButtonClicked;
    public event EventHandler StopButtonClicked;

    public MechShifterPanel()
    {
      InitializeComponent();
      device = new UsbDevice();
      device.Connected += new EventHandler(device_Connected);
      device.Disconnected += new EventHandler(device_Disconnected);
    }

    void device_Disconnected(object sender, EventArgs e)
    {
        if (InvokeRequired)
        {
            this.BeginInvoke(new MethodInvoker(DeviceDisconnected));
        }
        else
        {
            DeviceConnected();
        }
    }

    void device_Connected(object sender, EventArgs e)
    {
        if (InvokeRequired)
        {
            this.BeginInvoke(new MethodInvoker(DeviceConnected));
        }
        else
        {
            DeviceConnected();
        }
    }

    private void DeviceConnected()
    {
        usbButton.ImageDisabled = Properties.Resources.USB_64x64;
        usbButton.Text = " USB Connected ";

        stopButton_Click(this, EventArgs.Empty);
    }

    private void DeviceDisconnected()
    {
        usbButton.ImageDisabled = Properties.Resources.USB_64x64_BW;
        usbButton.Text = " USB Disconnected ";
        startButton.Enabled = false;
        stopButton.Enabled = false;
        gearSelectorGroupBox.Enabled = false;
        OnStopButtonClicked();
    }

    UsbDevice device = null;

    private void gear_R_Button_CheckedChanged(object sender, EventArgs e)
    {
      if (gear_R_Button.Checked)
      {
        device.SelectGear(0);
      }
    }

    private void gear_N_Button_CheckedChanged(object sender, EventArgs e)
    {
      if (gear_N_Button.Checked)
      {
        device.SelectGear(1);
      }
    }

    private void gear_1_Button_CheckedChanged(object sender, EventArgs e)
    {
      if (gear_1_Button.Checked)
      {
        device.SelectGear(2);
      }
    }

    private void gear_2_Button_CheckedChanged(object sender, EventArgs e)
    {
      if (gear_2_Button.Checked)
      {
        device.SelectGear(3);
      }
    }

    private void gear_3_Button_CheckedChanged(object sender, EventArgs e)
    {
      if (gear_3_Button.Checked)
      {
        device.SelectGear(4);
      }
    }

    private void gear_4_Button_CheckedChanged(object sender, EventArgs e)
    {
      if (gear_4_Button.Checked)
      {
        device.SelectGear(5);
      }
    }

    private void gear_5_Button_CheckedChanged(object sender, EventArgs e)
    {
      if (gear_5_Button.Checked)
      {
        device.SelectGear(6);
      }
    }

    private void gear_6_Button_CheckedChanged(object sender, EventArgs e)
    {
      if (gear_6_Button.Checked)
      {
        device.SelectGear(7);
      }
    }

    private void startButton_Click(object sender, EventArgs e)
    {
      gear_R_Button.Checked = true;
      startButton.Enabled = false;
      stopButton.Enabled = true;
      gearSelectorGroupBox.Enabled = true;
        OnStartButtonClicked();
    }

    private void stopButton_Click(object sender, EventArgs e)
    {
      gear_R_Button.Checked = true;
      stopButton.Enabled = false;
      startButton.Enabled = true;
      gearSelectorGroupBox.Enabled = false;
        OnStopButtonClicked();
    }

    private void OnStartButtonClicked()
    {
      StartButtonClicked?.Invoke(this, EventArgs.Empty);
    }

    private void OnStopButtonClicked()
    {
      StopButtonClicked?.Invoke(this, EventArgs.Empty);
    }

  }
}
