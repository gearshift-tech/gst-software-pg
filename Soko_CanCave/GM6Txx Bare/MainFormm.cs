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
  public partial class MainFormm : Form
  {
    //Bluereach.Mechshifter.Components.Interfaces.MechShifterUSB.UsbDevice enigmausb = new Bluereach.Mechshifter.Components.Interfaces.MechShifterUSB.UsbDevice();
    UsbDevice device = null;
    TCUGovernor_GM6Txx _GM6TxxInterface = null;

    public MainFormm()
    {
      InitializeComponent();

      //enigmausb = new Bluereach.Mechshifter.Components.Interfaces.MechShifterUSB.UsbDevice();
      //enigmausb.AutoConnectEnabled = true;

      device = new UsbDevice();
      _GM6TxxInterface = new TCUGovernor_GM6Txx(device);
      _GM6TxxInterface.SimData_ISSValueChanged += new EventHandler(_GM6TxxInterface_SimData_ISSValueChanged);
      _GM6TxxInterface.SimData_OSSValueChanged += new EventHandler(_GM6TxxInterface_SimData_OSSValueChanged);

      device.Connected += new EventHandler(device_Connected);
      device.Disconnected += new EventHandler(device_Disconnected);
      device.AutoConnectEnabled = true;


      //GbxTypeComboBox.Items.Clear();
      // Add all items from the GearboxControllerType enum
      //GbxTypeComboBox.Items.AddRange(Enum.GetNames(typeof(GearboxControllerType)));
      //GbxTypeComboBox.SelectedIndex = 0;
    }

    void _GM6TxxInterface_SimData_OSSValueChanged(object sender, EventArgs e)
    {
      OSSniceTB.SetValueWithoutEvent(_GM6TxxInterface.SimData_OSSValue);
    }

    void _GM6TxxInterface_SimData_ISSValueChanged(object sender, EventArgs e)
    {
      ISSniceTB.SetValueWithoutEvent(_GM6TxxInterface.SimData_ISSValue);
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
      _GM6TxxInterface.InitializeTcu();
      _GM6TxxInterface.EnableDrive();
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
      _GM6TxxInterface.ReadDTCs();
    }

    private void clearCodesBtn_Click(object sender, EventArgs e)
    {
      _GM6TxxInterface.ClearDTCs();
    }

    private void resetTcmBtn_Click(object sender, EventArgs e)
    {
      _GM6TxxInterface.ResetTCU();
    }

    private void resetAdaptsBtn_Click(object sender, EventArgs e)
    {
      _GM6TxxInterface.ResetAdapts();
    }



    #endregion TCU command buttons

    private void engineSpeedTB_ValueChanged(object sender, decimal value)
    {
      _GM6TxxInterface.SimData_EngineSpeed = (UInt16)engineSpeedTB.Value;
      engineSpeedLabel.Text = _GM6TxxInterface.SimData_EngineSpeed.ToString();
    }

    private void tpsTB_ValueChanged(object sender, decimal value)
    {
      _GM6TxxInterface.SimData_TPS = (UInt16)tpsTB.Value;
      tpsLabel.Text = _GM6TxxInterface.SimData_TPS.ToString();
    }

    private void engineTorqueTB_ValueChanged(object sender, decimal value)
    {
      _GM6TxxInterface.SimData_EngineTorque = (UInt16)engineTorqueTB.Value;
      engineTorqueLabel.Text = _GM6TxxInterface.SimData_EngineTorque.ToString();
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

      LineSolenoidLg.Value = _GM6TxxInterface.TCU_Data.Solenoid_PC1;
      S2SolenoidLg.Value = _GM6TxxInterface.TCU_Data.Solenoid_PC2;
      S3SolenoidLg.Value = _GM6TxxInterface.TCU_Data.Solenoid_PC3;
      S4SolenoidLg.Value = _GM6TxxInterface.TCU_Data.Solenoid_PC4;
      S5SolenoidLg.Value = _GM6TxxInterface.TCU_Data.Solenoid_PC5;
      TCCSolenoidLg.Value = _GM6TxxInterface.TCU_Data.Solenoid_PC6;
      shiftS1NI.IsOn = _GM6TxxInterface.TCU_Data.Solenoid_Shift1;
      shiftS2NI.IsOn = _GM6TxxInterface.TCU_Data.Solenoid_Shift2;
      TFPSW1NI.IsOn = _GM6TxxInterface.TCU_Data.Switch_Pressure1;
      TFPSW3NI.IsOn = _GM6TxxInterface.TCU_Data.Switch_Pressure3;
      TFPSW4NI.IsOn = _GM6TxxInterface.TCU_Data.Switch_Pressure4;
      TFPSW5NI.IsOn = _GM6TxxInterface.TCU_Data.Switch_Pressure5;

      inputSpeedAG.Value = _GM6TxxInterface.TCU_Data.Speed_Input;
      outputSpeedAG.Value = _GM6TxxInterface.TCU_Data.Speed_Output;
      slipSpeedAG.Value = _GM6TxxInterface.TCU_Data.Speed_Slip;

      engineTempTg.Value = _GM6TxxInterface.TCU_Data.EngineCoolandTemp;
      tcmTempTg.Value = _GM6TxxInterface.TCU_Data.TcuTemperature;
      transFluidTempTg.Value = _GM6TxxInterface.TCU_Data.FluidTemperature;

      gearLeverPositionLabel.Text = Enum.GetName(typeof(TCU_StatusData_GM6Txx.GearLeverPositionEnum), _GM6TxxInterface.TCU_Data.GearLeverPosition);
      actualGearLabel.Text = Enum.GetName(typeof(TCU_StatusData_GM6Txx.CommandedGearEnum), _GM6TxxInterface.TCU_Data.CommandedGear_Actual);
      desiredGearLabel.Text = Enum.GetName(typeof(TCU_StatusData_GM6Txx.CommandedGearEnum), _GM6TxxInterface.TCU_Data.CommandedGear_Desired);
    }

    private void x1button_Click(object sender, EventArgs e)
    {
      //enigmausb.SetGPLeds(true, true);
      CanMessage msg = new CanMessage();
      msg.remoteID = 0x17FA95FD;
      msg.isXtdFrameType = true;
      msg.isRTRFrame = false;
      msg.DLC = 8;
      for (int i = 0; i < 8; i++)
      {
        msg.data[i] = (byte)i;
      }
      device.UsbSendSingleCanMessage(msg);
    }

    private void x10button_Click(object sender, EventArgs e)
    {
      _GM6TxxInterface.PackPIDs();
    }

    private void select1Btn_Click(object sender, EventArgs e)
    {
      _GM6TxxInterface.SelectGear(1);
    }

    private void select2Btn_Click(object sender, EventArgs e)
    {
      _GM6TxxInterface.SelectGear(2);
    }

    private void select3Btn_Click(object sender, EventArgs e)
    {
      _GM6TxxInterface.SelectGear(3);
    }

    private void select4Btn_Click(object sender, EventArgs e)
    {
      _GM6TxxInterface.SelectGear(4);
    }

    private void select5Btn_Click(object sender, EventArgs e)
    {
      _GM6TxxInterface.SelectGear(5);
    }

    private void select6Btn_Click(object sender, EventArgs e)
    {
      _GM6TxxInterface.SelectGear(6);
    }

    private void tccOffBtn_Click(object sender, EventArgs e)
    {
      _GM6TxxInterface.SetTccOff();
    }

    private void tccOnBtn_Click(object sender, EventArgs e)
    {
      _GM6TxxInterface.SetTccOn();
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

    private void ISSniceTB_ValueChanged(object sender, decimal value)
    {
      oss1ValueLabel.Text = ISSniceTB.Value.ToString();
      _GM6TxxInterface.SimData_ISSValue = (UInt16)ISSniceTB.Value;
    }

    private void OSSniceTB_ValueChanged(object sender, decimal value)
    {
      oss2ValueLabel.Text = OSSniceTB.Value.ToString();
      _GM6TxxInterface.SimData_OSSValue = (UInt16)OSSniceTB.Value;
    }

    private void startBtn_Click(object sender, EventArgs e)
    {
      _GM6TxxInterface.EnableDrive();
    }

    private void readVinBtn_Click(object sender, EventArgs e)
    {
      _GM6TxxInterface.ReadVIN();
    }


  }
}

