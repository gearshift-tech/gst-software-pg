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
using Soko.CanCave.Components.Properties;

namespace Soko.CanCave.Components.Forms
{
  public partial class Gm6T40BarePanel : UserControl
  {
    //GST.ZF6.Components.Interfaces.MechShifterUSB.UsbDevice zf6usb = new GST.ZF6.Components.Interfaces.MechShifterUSB.UsbDevice();
    UsbDevice device = null;
    TCUGovernor_GM6Txx _GM6TxxInterface = null;

    public Gm6T40BarePanel(TCUGovernor_GM6Txx GM6TxxInterface)
    {
      InitializeComponent();

      _GM6TxxInterface = GM6TxxInterface;
      device = _GM6TxxInterface.UsbDevice;
      _GM6TxxInterface.SimData_ISSValueChanged += new EventHandler(_GM6TxxInterface_SimData_ISSValueChanged);
      _GM6TxxInterface.SimData_OSSValueChanged += new EventHandler(_GM6TxxInterface_SimData_OSSValueChanged);
      _GM6TxxInterface.SimData_EngineSpeedChanged += _GM6TxxInterface_SimData_EngineSpeedChanged;
      _GM6TxxInterface.SimData_EngineTorqueChanged += _GM6TxxInterface_SimData_EngineTorqueChanged;
      _GM6TxxInterface.SimData_EngineTPSChanged += _GM6TxxInterface_SimData_EngineTPSChanged;

      device.Connected += new EventHandler(device_Connected);
      device.Disconnected += new EventHandler(device_Disconnected);
      device.AutoConnectEnabled = true;
      
      SetGm6TDisplayMode();
    }

    private void _GM6TxxInterface_SimData_EngineTPSChanged(object sender, EventArgs e)
    {
      tpsLabel.Text = _GM6TxxInterface.SimData_TPS.ToString() + " %";
      tpsTB.SetValueWithoutEvent(_GM6TxxInterface.SimData_TPS);
    }

    private void _GM6TxxInterface_SimData_EngineTorqueChanged(object sender, EventArgs e)
    {
      engineTorqueLabel.Text = _GM6TxxInterface.SimData_EngineTorque.ToString() + " Nm";
      engineTorqueTB.SetValueWithoutEvent(_GM6TxxInterface.SimData_EngineTorque);
    }

    private void _GM6TxxInterface_SimData_EngineSpeedChanged(object sender, EventArgs e)
    {
      engineSpeedLabel.Text = _GM6TxxInterface.SimData_EngineSpeed.ToString() + " RPM";
      engineSpeedTB.SetValueWithoutEvent(_GM6TxxInterface.SimData_EngineSpeed);
    }

    void _GM6TxxInterface_SimData_OSSValueChanged(object sender, EventArgs e)
    {
      oss1ValueLabel.Text = "ISS: " + ISSniceTB.Value.ToString() + " RPM";
      OSSniceTB.SetValueWithoutEvent(_GM6TxxInterface.SimData_OSSValue);
    }

    void _GM6TxxInterface_SimData_ISSValueChanged(object sender, EventArgs e)

    {
      oss2ValueLabel.Text = "OSS: " + OSSniceTB.Value.ToString() + " RPM";
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
      usbStatusNiceInd.IsOn = true;
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
      usbStatusNiceInd.IsOn = false;
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
    }


    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
      device.Disconnect();
    }



    #region Shift buttons

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

    #endregion Shift buttons



    #region TCU command buttons


    private void getCodesBtn_Click(object sender, EventArgs e)
    {
      feedbackLabel.Text = "DTC readout started";
      _GM6TxxInterface.ReadDTCs();
    }

    private void clearCodesBtn_Click(object sender, EventArgs e)
    {
      feedbackLabel.Text = "DTC clearing started";
      _GM6TxxInterface.ClearDTCs();
      _GM6TxxInterface.ReadDTCs();
    }

    private void resetTcmBtn_Click(object sender, EventArgs e)
    {
      feedbackLabel.Text = "TCU reset command sent";
      _GM6TxxInterface.ResetTCU();
    }

    private void resetAdaptsBtn_Click(object sender, EventArgs e)
    {
      feedbackLabel.Text = "Reset adapts command sent";
      _GM6TxxInterface.ResetAdapts();
    }



    #endregion TCU command buttons

    private void engineSpeedTB_ValueChanged(object sender, decimal value)
    {
      _GM6TxxInterface.SimData_EngineSpeed = (UInt16)engineSpeedTB.Value;
      engineSpeedLabel.Text = _GM6TxxInterface.SimData_EngineSpeed.ToString() + " RPM";
    }

    private void tpsTB_ValueChanged(object sender, decimal value)
    {
      _GM6TxxInterface.SimData_TPS = (UInt16)tpsTB.Value;
      tpsLabel.Text = _GM6TxxInterface.SimData_TPS.ToString() + " %";
    }

    private void engineTorqueTB_ValueChanged(object sender, decimal value)
    {
      _GM6TxxInterface.SimData_EngineTorque = (UInt16)engineTorqueTB.Value;
      engineTorqueLabel.Text = _GM6TxxInterface.SimData_EngineTorque.ToString() + " Nm";
    }

    private void HideAdvancedFeatures()
    {

      ISSniceTB.Visible = false;
      oss1ValueLabel.Visible = false;

      OSSniceTB.Visible = false;
      oss2ValueLabel.Visible = false;

      engineSpeedLabel.Visible = false;
      engineSpeedLabelLabel.Visible = false;
      engineSpeedTB.Visible = false;
      tpsLabel.Visible = false;
      tpsLabelLabel.Visible = false;
      tpsTB.Visible = false;
      engineTorqueLabel.Visible = false;
      engineTorqueLabelLabel.Visible = false;
      engineTorqueTB.Visible = false;
    }

    private void ShowAdvancedFeatures()
    {
    }


    private void guiUpdateSlowTimer_Tick(object sender, EventArgs e)
    {
      SuspendLayout();
      try
      {
        label7.Text = "DTC: " + _GM6TxxInterface._tcuData_DTCsRead.Count.ToString();
        if (_GM6TxxInterface._tcuData_DTCsRead.Count > 0)
        {
          _GM6TxxInterface._tcuData_DTCsRead.Sort();
          dtcLabel.Text = "";
          foreach (string dtc in _GM6TxxInterface._tcuData_DTCsRead)
          {
            dtcLabel.Text += dtc + "     " + _GM6TxxInterface.UsbDevice._DtcDatabase.GetDtcDescription(dtc) + "\n";
          }
        }
        else
        {
          dtcLabel.Text = "Clear";
        }
      }
      catch (Exception) { }
      ResumeLayout();

      vinLabel.Text = _GM6TxxInterface.TcuData_VIN;
    }

    private void guiUpdateTimer_Tick(object sender, EventArgs e)
    {

      LineSolenoidLg.Value = _GM6TxxInterface.TCU_Data.Solenoid_PC1;
      S2SolenoidLg.Value = _GM6TxxInterface.TCU_Data.Solenoid_PC2;
      S3SolenoidLg.Value = _GM6TxxInterface.TCU_Data.Solenoid_PC3;
      S4SolenoidLg.Value = _GM6TxxInterface.TCU_Data.Solenoid_PC4;
      S5SolenoidLg.Value = _GM6TxxInterface.TCU_Data.Solenoid_PC5;
      TCCSolenoidLg.Value = _GM6TxxInterface.TCU_Data.Solenoid_PC6;
      if (_GM6TxxInterface.TCU_Data.Solenoid_Shift1)
        ss1SolenoidLg.Value = ss1SolenoidLg.MaxValue;
      else
        ss1SolenoidLg.Value = ss1SolenoidLg.MinValue;
      if (_GM6TxxInterface.TCU_Data.Solenoid_Shift2)
        ss2SolenoidLg.Value = ss2SolenoidLg.MaxValue;
      else
        ss2SolenoidLg.Value = ss2SolenoidLg.MinValue;
      TFPSW1NI.IsOn = _GM6TxxInterface.TCU_Data.Switch_Pressure1;
      TFPSW3NI.IsOn = _GM6TxxInterface.TCU_Data.Switch_Pressure3;
      TFPSW4NI.IsOn = _GM6TxxInterface.TCU_Data.Switch_Pressure4;
      TFPSW5NI.IsOn = _GM6TxxInterface.TCU_Data.Switch_Pressure5;

      inputSpeedAG.Value = _GM6TxxInterface.TCU_Data.Speed_Input;
      outputSpeedAG.Value = _GM6TxxInterface.TCU_Data.Speed_Output;
      slipSpeedAG.Value = _GM6TxxInterface.TCU_Data.Speed_Slip;

      engineTempSTG.Value = _GM6TxxInterface.TCU_Data.EngineCoolandTemp;
      tcmTempSTG.Value = _GM6TxxInterface.TCU_Data.TcuTemperature;
      fluidTempSTG.Value = _GM6TxxInterface.TCU_Data.FluidTemperature;

      gearLeverPositionLabel.Text = Enum.GetName(typeof(TCU_StatusData_GM6Txx.GearLeverPositionEnum), _GM6TxxInterface.TCU_Data.GearLeverPosition);
      actualGearLabel.Text = Enum.GetName(typeof(TCU_StatusData_GM6Txx.CommandedGearEnum), _GM6TxxInterface.TCU_Data.CommandedGear_Actual);
      desiredGearLabel.Text = Enum.GetName(typeof(TCU_StatusData_GM6Txx.CommandedGearEnum), _GM6TxxInterface.TCU_Data.CommandedGear_Desired);
    }



    private void MainForm_Load(object sender, EventArgs e)
    {

    }

    private void ISSniceTB_ValueChanged(object sender, decimal value)
    {
      oss1ValueLabel.Text = "ISS: " + ISSniceTB.Value.ToString() + " RPM";
      _GM6TxxInterface.SimData_ISSValue = (UInt16)ISSniceTB.Value;
    }

    private void OSSniceTB_ValueChanged(object sender, decimal value)
    {
      oss2ValueLabel.Text = "OSS: " + OSSniceTB.Value.ToString() + " RPM";
      _GM6TxxInterface.SimData_OSSValue = (UInt16)OSSniceTB.Value;
    }

    private void startBtn_Click(object sender, EventArgs e)
    {
      _GM6TxxInterface.EnableDrive();
      _GM6TxxInterface.InitializeTcu();
      _GM6TxxInterface.EnableDrive();
    }

    private void readVinBtn_Click(object sender, EventArgs e)
    {
      _GM6TxxInterface.ReadVIN();
    }


    private void SetGm6TDisplayMode()
    {
      this.TFPSW1NI.ImageOFF = ((System.Drawing.Image)(Resources.TFP_1_OFF));
      this.TFPSW1NI.ImageON = ((System.Drawing.Image)(Resources.TFP_1_ON));
      this.TFPSW3NI.ImageOFF = ((System.Drawing.Image)(Resources.TFP_2_OFF));
      this.TFPSW3NI.ImageON = ((System.Drawing.Image)(Resources.TFP_2_ON));
      this.TFPSW4NI.ImageOFF = ((System.Drawing.Image)(Resources.TFP_3_OFF));
      this.TFPSW4NI.ImageON = ((System.Drawing.Image)(Resources.TFP_3_ON));
      this.TFPSW5NI.ImageOFF = ((System.Drawing.Image)(Resources.TFP_4_OFF));
      this.TFPSW5NI.ImageON = ((System.Drawing.Image)(Resources.TFP_4_ON));
      ss2SolenoidLg.Visible = false;
      ss1SolenoidLg.Location = new Point(ss2SolenoidLg.Location.X, ss2SolenoidLg.Location.Y);

    }

    private void SetGm6LDisplayMode()
    {
      this.TFPSW1NI.ImageOFF = ((System.Drawing.Image)(Resources.TFP_1_OFF));
      this.TFPSW1NI.ImageON = ((System.Drawing.Image)(Resources.TFP_1_ON));
      this.TFPSW3NI.ImageOFF = ((System.Drawing.Image)(Resources.TFP_3_OFF));
      this.TFPSW3NI.ImageON = ((System.Drawing.Image)(Resources.TFP_3_ON));
      this.TFPSW4NI.ImageOFF = ((System.Drawing.Image)(Resources.TFP_4_OFF));
      this.TFPSW4NI.ImageON = ((System.Drawing.Image)(Resources.TFP_4_ON));
      this.TFPSW5NI.ImageOFF = ((System.Drawing.Image)(Resources.TFP_5_OFF));
      this.TFPSW5NI.ImageON = ((System.Drawing.Image)(Resources.TFP_5_ON));
      ss2SolenoidLg.Visible = true;
      ss1SolenoidLg.Location = new Point(ss2SolenoidLg.Location.X - 59, ss2SolenoidLg.Location.Y);
    }

    private void tcmTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      switch (tcmTypeComboBox.SelectedIndex)
      {
        case 0:
          {
            SetGm6TDisplayMode();
            _GM6TxxInterface.EnableDrive();
            _GM6TxxInterface.InitializeTcu();
            _GM6TxxInterface.EnableDrive();
            break;
          }
        case 1:
          {
            SetGm6LDisplayMode();
            _GM6TxxInterface.EnableDrive();
            _GM6TxxInterface.InitializeTcu();
            _GM6TxxInterface.EnableDrive();
            break;
          }
      }
    }
  }
}

