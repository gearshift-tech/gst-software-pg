using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Soko.CanCave.Components.Interfaces.CanCaveUsb;
using Soko.CanCave.Components.Interfaces;
using Soko.Common.Interfaces;

namespace CommLibTest
{
  public partial class MainForm : Form
  {
    //Bluereach.Mechshifter.Components.Interfaces.MechShifterUSB.UsbDevice enigmausb = new Bluereach.Mechshifter.Components.Interfaces.MechShifterUSB.UsbDevice();
    UsbDevice device = null;
    TCUGovernor_NissanRE5 _NissanRE5Interface = null;

    public MainForm()
    {
      InitializeComponent();

      //enigmausb = new Bluereach.Mechshifter.Components.Interfaces.MechShifterUSB.UsbDevice();
      //enigmausb.AutoConnectEnabled = true;

      device = new UsbDevice();
      _NissanRE5Interface = new TCUGovernor_NissanRE5(device);

      device.Connected += new EventHandler(device_Connected);
      device.Disconnected += new EventHandler(device_Disconnected);
      device.AutoConnectEnabled = true;


      //GbxTypeComboBox.Items.Clear();
      // Add all items from the GearboxControllerType enum
      //GbxTypeComboBox.Items.AddRange(Enum.GetNames(typeof(GearboxControllerType)));
      //GbxTypeComboBox.SelectedIndex = 0;
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
      _NissanRE5Interface.InitializeTcu();
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
    }


    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
      device.Disconnect();
    }



    #region Shift buttons

    #endregion Shift buttons



    #region TCU command buttons


    private void getCodesBtn_Click(object sender, EventArgs e)
    {
      //_GM6TxxInterface.Ge
    }

    private void clearCodesBtn_Click(object sender, EventArgs e)
    {
      _NissanRE5Interface.ClearDTCs();
    }

    private void resetTcmBtn_Click(object sender, EventArgs e)
    {
      _NissanRE5Interface.ResetTCU();
    }

    private void resetAdaptsBtn_Click(object sender, EventArgs e)
    {
      _NissanRE5Interface.ResetAdapts();
    }



    #endregion TCU command buttons

    private void engineSpeedTB_ValueChanged(object sender, decimal value)
    {
      _NissanRE5Interface.SimData_EngineSpeed = (UInt16)engineSpeedTB.Value;
      engineSpeedLabel.Text = _NissanRE5Interface.SimData_EngineSpeed.ToString();
    }

    private void tpsTB_ValueChanged(object sender, decimal value)
    {
      _NissanRE5Interface.SimData_TPS = (UInt16)tpsTB.Value;
      tpsLabel.Text = _NissanRE5Interface.SimData_TPS.ToString();
    }

    private void engineTorqueTB_ValueChanged(object sender, decimal value)
    {
      _NissanRE5Interface.SimData_EngineTorque = (UInt16)engineTorqueTB.Value;
      engineTorqueLabel.Text = _NissanRE5Interface.SimData_EngineTorque.ToString();
    }





    private void guiUpdateTimer_Tick(object sender, EventArgs e)
    {
      //int Solenoid_PC1 = 0;
      //int Solenoid_PC2 = 0;
      //int Solenoid_PC3 = 0;
      //int Solenoid_PC4 = 0;
      //int Solenoid_PC5 = 0;
      //int Solenoid_PC6 = 0;
      //bool Solenoid_Shift1 = false;
      //bool Solenoid_Shift2 = false;

      //bool Switch_Pressure1 = false;
      //bool Switch_Pressure2 = false;
      //bool Switch_Pressure3 = false;
      //bool Switch_Pressure4 = false;

      //int Speed_Input = 0;
      //int Speed_Output = 0;
      //int Speed_Slip = 0;

      LineSolenoidLg.Value = _NissanRE5Interface.TCU_Data.Solenoid_PC1;
      S2SolenoidLg.Value = _NissanRE5Interface.TCU_Data.Solenoid_PC2;
      S3SolenoidLg.Value = _NissanRE5Interface.TCU_Data.Solenoid_PC3;
      S4SolenoidLg.Value = _NissanRE5Interface.TCU_Data.Solenoid_PC4;
      S5SolenoidLg.Value = _NissanRE5Interface.TCU_Data.Solenoid_PC5;
      TCCSolenoidLg.Value = _NissanRE5Interface.TCU_Data.Solenoid_PC6;
      shiftS1NI.Enabled = _NissanRE5Interface.TCU_Data.Solenoid_Shift1;
      shiftS2NI.Enabled = _NissanRE5Interface.TCU_Data.Solenoid_Shift2;
    }

    private void select1Btn_Click(object sender, EventArgs e)
    {
      _NissanRE5Interface.SelectGear(1);
    }

    private void select2Btn_Click(object sender, EventArgs e)
    {
      _NissanRE5Interface.SelectGear(2);
    }

    private void select3Btn_Click(object sender, EventArgs e)
    {
      _NissanRE5Interface.SelectGear(3);
    }

    private void select4Btn_Click(object sender, EventArgs e)
    {
      _NissanRE5Interface.SelectGear(4);
    }

    private void select5Btn_Click(object sender, EventArgs e)
    {
      _NissanRE5Interface.SelectGear(5);
    }

    private void select6Btn_Click(object sender, EventArgs e)
    {
      _NissanRE5Interface.SelectGear(6);
    }

    private void tccOffBtn_Click(object sender, EventArgs e)
    {
      _NissanRE5Interface.SetTccOff();
    }

    private void tccOnBtn_Click(object sender, EventArgs e)
    {
      _NissanRE5Interface.SetTccOn();
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        // no smaller than design time size
        this.MinimumSize = new Size(this.Width, this.Height);

        // no larger than screen size
        this.MaximumSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

        this.AutoSize = true;
        this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    }

    private void startBtn_Click(object sender, EventArgs e)
    {
      _NissanRE5Interface.EnableDrive();
    }


  }
}

