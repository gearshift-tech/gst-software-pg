using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Soko.Common.Common;
using GST.ZF6.Components.Interfaces.MechShifterUSB;

namespace CommLibTest
{
  public partial class MainForm : Form
  {
    UsbDevice device = new UsbDevice();
    public MainForm()
    {
      InitializeComponent();

      enigmaInitPanel1.Zf6USB = device;
      enigmaInitPanel1.gearboxType = GearboxControllerType.NON_MECHATRONIC;

      device.Connected += new EventHandler(device_Connected);
      device.Disconnected += new EventHandler(device_Disconnected);
      device.AutoConnectEnabled = true;

      GbxTypeComboBox.Items.Clear();
      // Add all items from the GearboxControllerType enum
      GbxTypeComboBox.Items.AddRange(Enum.GetNames(typeof(GearboxControllerType)));
      GbxTypeComboBox.SelectedIndex = 0;
    }



    void device_Connected(object sender, EventArgs e)
    {
      if (InvokeRequired)
      {
        BeginInvoke(new MethodInvoker(DeviceConnected));
      }
      else
      {
        DeviceConnected();
      }
    }

    void DeviceConnected()
    {
      usbButton.Text = " USB Connected ";
      // Enable Kline so that data coming from Enigma is visible
      device.EnableKline();
      // Select reverse
      device.SelectGear(0);
    }

    void device_Disconnected(object sender, EventArgs e)
    {
      if (InvokeRequired)
      {
        this.BeginInvoke(new MethodInvoker(DeviceDisconnected));
      }
      else
      {
        DeviceDisconnected();
      }
    }

    private void DeviceDisconnected()
    {
      usbButton.Text = " USB Disconnected ";
    }

    private void button1_Click(object sender, EventArgs e)
    {
      device.ConnectAsync();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      device.Disconnect();
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      shortLabel.Text = device.shortMsg;
      longLabel.Text = device.longMsg;
      msgLenLabel.Text = device.LastFrameLength.ToString();
      connStatLabel.Text = device.GearboxConnectionStatus.ToString();
      label14.Text = device.gbxconnstatraw.ToString("X2");

      curr6Label.Text = device.GetLastCurrentValue(6).ToString();
      curr5Label.Text = device.GetLastCurrentValue(5).ToString();
      curr4Label.Text = device.GetLastCurrentValue(4).ToString();
      curr3Label.Text = device.GetLastCurrentValue(3).ToString();
      curr2Label.Text = device.GetLastCurrentValue(2).ToString();
      curr1Label.Text = device.GetLastCurrentValue(1).ToString();

      switch (device.GearboxPhysicallySelectedGear)
      {
        case 0x0C:
          {
            currGearLabel.Text = "R";
            break;
          }
        default:
        case 0x0A:
          {
            currGearLabel.Text = "N";
            break;
          }
        case 0x01:
        case 0x02:
        case 0x03:
        case 0x04:
        case 0x05:
        case 0x06:
          {
            currGearLabel.Text = device.GearboxPhysicallySelectedGear.ToString();
            break;
          }
      }
      //currGearLabel.Text = device.GearboxPhysicallySelectedGear.ToString("X2");
      //transitionCompleteLabel.Text = device.GearboxSwitchingInProgress.ToString("X2");

      switch (device.GearboxSwitchingInProgress)
      {
        default:
        case 0x20:
          {
            transitionCompleteLabel.Text = "Yes";
            break;
          }
        case 0x24:
          {
            transitionCompleteLabel.Text = "No";
            break;
          }
      }
      deviceStatPackLabel.Text = device.statpack;
    }

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

    private void driveOnBtn_Click(object sender, EventArgs e)
    {
      device.EnableGearboxDrive();
    }

    private void driveOffBtn_Click(object sender, EventArgs e)
    {
      device.DisableGearboxDrive();
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
      device.Disconnect();
    }

    private void startEnigmaInitBtn_Click(object sender, EventArgs e)
    {
      enigmaInitPanel1.Start();
    }

    private void enigmaInitPanel1_ContinueButtonClicked(object sender, EventArgs e)
    {
    }

    private void eds5TrackBar_ValueChanged(object sender, EventArgs e)
    {
      device.SetEDS5Value(eds5TrackBar.Value);
    }

    private void eds6TrackBar_ValueChanged(object sender, EventArgs e)
    {
      device.SetEDS6Value(eds6TrackBar.Value);
    }

    private void saveDumpBtn_Click(object sender, EventArgs e)
    {
      device.SaveDumpData();
    }

    private void bootloaderBtn_Click(object sender, EventArgs e)
    {
      device.EnterBootloader();
    }

    private void GbxTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (GbxTypeComboBox.SelectedIndex >= 0 && GbxTypeComboBox.SelectedItem.ToString() != string.Empty)
      {
        enigmaInitPanel1.gearboxType = (GearboxControllerType)Enum.Parse(typeof(GearboxControllerType), GbxTypeComboBox.SelectedItem.ToString(), true);
        device.SelectGearboxType(enigmaInitPanel1.gearboxType);

        if (enigmaInitPanel1.gearboxType != GearboxControllerType.NON_MECHATRONIC)
        {

          enigmaInitPanel1.Start();
        }
      }
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        // no smaller than design time size
        this.MinimumSize = new Size(this.Width, this.Height);

        // no larger than screen size
        this.MaximumSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

        this.AutoSize = true;
        this.AutoSizeMode = AutoSizeMode.GrowOnly;
    }
  }
}

