using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Soko.CanCave.Components.Interfaces;
using Soko.CanCave.Components.Interfaces.CanCaveUsb;

namespace Soko.CanCave.Components.Interfaces
{
  public class TCUGovernor_NissanRE5 : Soko.Common.Interfaces.TCUGovernor
  {



    #region Constants



    #endregion  Constants



    #region Private fields

    private UsbDevice _usbDevice = null;

    private int _LastSelectedGear = 0;
    private bool _LastTCCState = false;

    private bool _TcuIsConnected = false;
    private bool _TcuIsDriving = false;

    System.Timers.Timer _GearCommandTransmitterTimer = new System.Timers.Timer();

    private Byte _SolendoidValue_LU = 0;
    private Byte _SolendoidValue_PL = 0;
    private Byte _SolendoidValue_IC = 0;
    private Byte _SolendoidValue_FRB = 0;
    private Byte _SolendoidValue_DC = 0;
    private Byte _SolendoidValue_HLRC = 0;
    private Byte _SolendoidValue_LCB = 0;


    #endregion Private fields



    #region Constructors & finalizer

    public TCUGovernor_NissanRE5(UsbDevice usbIF)
    {
      _usbDevice = usbIF;
      _GearCommandTransmitterTimer.Interval = 500;
      _GearCommandTransmitterTimer.Enabled = false;
      _GearCommandTransmitterTimer.Elapsed += new System.Timers.ElapsedEventHandler(_GearCommandTransmitterTimer_Elapsed);
      // TODO Add event handlers
    }

    void _GearCommandTransmitterTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
      SendSelectGearCommand(_LastSelectedGear);
      SendTesterPresentMessage();
    }

    #endregion Constructors & finalizer



    #region Events

    public event EventHandler SimData_EngineSpeedChanged;
    public event EventHandler SimData_EngineTPSChanged;
    public event EventHandler SimData_EngineTorqueChanged;
    public event EventHandler SimData_ISSValueChanged;
    public event EventHandler SimData_OSSValueChanged;


    #endregion Events



    #region Properties

    public override bool UsbDeviceIsConnected
    {
      get { return _usbDevice.IsConnected; }
    }
    public override bool TcuIsConnected
    {
      get { return _TcuIsConnected; }
    }
    public override bool TcuIsDriving
    {
      get { return _TcuIsDriving; }
    }

    private bool _TcuPresentFlag = false;
    public bool TcuPresent
    {
      get { return _TcuPresentFlag; }
    }

    private string _TcuVIN = string.Empty;
    public string TcuVIN
    {
      get { return _TcuVIN; }
    }

    public TCU_StatusData_GM6Txx TCU_Data = new TCU_StatusData_GM6Txx();

    private UInt16 _SimData_EngineTorque = 94; // Initialize to 94
    public UInt16 SimData_EngineTorque
    {
      get { return _SimData_EngineTorque; }
      set
      {
        _SimData_EngineTorque = value;
        //UpdateCANNode1();
        //if (SimData_EngineTorqueChanged != null) SimData_EngineTorqueChanged(this, EventArgs.Empty);
      }
    }

    private UInt16 _SimData_TPS = 26;
    public UInt16 SimData_TPS
    {
      get { return _SimData_TPS; }
      set
      {
        _SimData_TPS = value;
        //UpdateCANNode1();
        //UpdateCANNode2(); // TPS is sent in 2 nodes
        //if (SimData_EngineTPSChanged != null) SimData_EngineTPSChanged(this, EventArgs.Empty);
      }
    }

    private UInt16 _SimData_EngineSpeed = 1230;
    public UInt16 SimData_EngineSpeed
    {
      get { return _SimData_EngineSpeed; }
      set
      {
        _SimData_EngineSpeed = value;
        //UpdateCANNode2();
        //if (SimData_EngineSpeedChanged != null) SimData_EngineSpeedChanged(this, EventArgs.Empty);
      }
    }

    private UInt16 _SimData_ISSValue = 1200;
    public UInt16 SimData_ISSValue
    {
      get { return _SimData_ISSValue; }
      set
      {
        _SimData_ISSValue = value;
        _usbDevice.UsbSendUpdateSSEMUDataPack((UInt16)(0.6 * _SimData_ISSValue), (UInt16)(0.6 * _SimData_OSSValue));
        SimData_ISSValueChanged?.Invoke(this, EventArgs.Empty);
      }
    }

    private UInt16 _SimData_OSSValue = 50;
    public UInt16 SimData_OSSValue
    {
      get { return _SimData_OSSValue; }
      set
      {
        _SimData_OSSValue = value;
        _usbDevice.UsbSendUpdateSSEMUDataPack((UInt16)(0.6 * _SimData_ISSValue), (UInt16)(0.6 * _SimData_OSSValue));
        SimData_OSSValueChanged?.Invoke(this, EventArgs.Empty);
      }
    }

    #endregion Properties


    #region Public Methods



    public override void InitializeTcu()
    {
      // If CAN not enabled, enable it
      // Look for a gearbox (check if periodic data is being received)
      // Read vin, if succeeded set the  'gearbox present'flag
      _usbDevice.UsbSendUnqueuedCommandOnlyPacket(USBPacketCommandCode.CMD_TCUGOV_SelectNissanRE5Mode);
      UpdateCANNode1();
      _TcuPresentFlag = true;
      _TcuVIN = "1GNCS18Z3M0115561";
      Console.WriteLine("RE5 INIT TCU");
    }


    public void SetChannelDriveValues(List<UInt16> ChannelDrives)
    {
      if (ChannelDrives == null || ChannelDrives.Count < 7)
      {
        return;
      }
      for (int i = 0; i < 7; i++)
      {
        SetChannelDriveValue(i, ChannelDrives[i]);
      }
    }

    public void SetChannelDriveValues(List<int> ChannelDrives)
    {
      if (ChannelDrives == null || ChannelDrives.Count < 7)
      {
        return;
      }
      for (int i = 0; i < 7; i++)
      {
        SetChannelDriveValue(i, ChannelDrives[i]);
      }
    }

    public void SetChannelDriveValue(int index, int value)
    {
      switch (index)
      {
        default:
        {
          return;
        }
        case (0):
        {
          _SolendoidValue_LU = (byte)(value * 2.55f);
          break;
        }
        case (1):
        {
          _SolendoidValue_PL = (byte)(value * 2.55f);
          break;
        }
        case (2):
        {
          _SolendoidValue_IC = (byte)(value * 2.55f);
          break;
        }
        case (3):
        {
          _SolendoidValue_FRB = (byte)(value * 2.55f);
          break;
        }
        case (4):
        {
          _SolendoidValue_DC = (byte)(value * 2.55f);
          break;
        }
        case (5):
        {
          _SolendoidValue_HLRC = (byte)(value * 2.55f);
          break;
        }
        case (6):
        {
          _SolendoidValue_LCB = (byte)(value * 2.55f);
          break;
        }
      }
      UpdateCANNode1();
    }

    public void DisableAllSolenoids()
    {
      _SolendoidValue_LU = 128;
      _SolendoidValue_PL = 255;
      _SolendoidValue_IC = 255;
      _SolendoidValue_FRB = 255;
      _SolendoidValue_DC = 255;
      _SolendoidValue_HLRC = 255;
      _SolendoidValue_LCB = 0x80; // 1000 0000 bin

      UpdateCANNode1();
    }

    public override void EnableDrive()
    {
      SendTesterPresentMessage();
      _GearCommandTransmitterTimer.Enabled = true;
      UpdateCANNode1();
      //UpdateCANNode2();
      //UpdateCANNode3();
    }
    public override void DisableDrive()
    {
      _GearCommandTransmitterTimer.Enabled = false;
    }

    public override void SwitchGearUp()
    {
    }
    public override void SwitchGearDown()
    {
    }

    void SendTesterPresentMessage()
    {
      _usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x01, 0x3E, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA);  
    }

    private void SendSelectGearCommand(int gear)
    {
      int gearcode = 0;
      switch (gear)
      {
        case 1:
        default:
        {
          gearcode = _LastTCCState ? 0x97 : 0x90;
          break;
        }
        case 2:
        {
          gearcode = _LastTCCState ? 0xA7 : 0xA0;
          break;
        }
        case 3:
        {
          gearcode = _LastTCCState ? 0xB7 : 0xB0;
          break;
        }
        case 4:
        {
          gearcode = _LastTCCState ? 0xC7 : 0xC0;
          break;
        }
        case 5:
        {
          gearcode = _LastTCCState ? 0xD7 : 0xD0;
          break;
        }
        case 6:
        {
          gearcode = _LastTCCState ? 0xE7 : 0xE0;
          break;
        }
      }
      _usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x06, 0xAE, 0x30, (byte)gearcode, 0, 0, 0, 0);  
      //Console.Write(gearcode.ToString("X2" + " "));
    }

    public override void SelectGear(int gear)
    {
      gear = (gear < 1) ? 1 : gear;
      gear = (gear > 6) ? 6 : gear;
      Console.WriteLine("GM6T Select " + gear.ToString());
      byte gearcode = 0;
      switch (gear)
      {
        case 1:
        default:
        {
          SimData_ISSValue = 1230;
          SimData_OSSValue = 50;
          gearcode = 0x90;
          break;
        }
        case 2:
        {
          SimData_ISSValue = 1230;
          SimData_OSSValue = 300;
          gearcode = 0xA0;
          break;
        }
        case 3:
        {
          SimData_ISSValue = 1230;
          SimData_OSSValue = 500;
          gearcode = 0xB0;
          break;
        }
        case 4:
        {
          SimData_ISSValue = 1230;
          SimData_OSSValue = 550;
          gearcode = 0xC0;
          break;
        }
        case 5:
        {
          SimData_ISSValue = 1230;
          SimData_OSSValue = 750;
          gearcode = 0xD0;
          break;
        }
        case 6:
        {
          SimData_ISSValue = 1230;
          SimData_OSSValue = 1200;
          gearcode = 0xE0;
          break;
        }
      }

      _usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x06, 0xAE, 0x30, gearcode, 0, 0, 0, 0);
      _LastSelectedGear = gear;
    }

    public int LastSelectedGear
    {
      get { return _LastSelectedGear; }
    }

    public void SetTccOn()
    {
      byte tccOnCmd = 0;
      switch (_LastSelectedGear)
      {
        case 1:
        default:
        {
          tccOnCmd = 0x97;
          break;
        }
        case 2:
        {
          tccOnCmd = 0xA7;
          break;
        }
        case 3:
        {
          tccOnCmd = 0xB7;
          break;
        }
        case 4:
        {
          tccOnCmd = 0xC7;
          break;
        }
        case 5:
        {
          tccOnCmd = 0xD7;
          break;
        }
        case 6:
        {
          tccOnCmd = 0xE7;
          break;
        }
      }
      _usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x06, 0xAE, 0x30, tccOnCmd, 0, 0, 0, 0);
      //_usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x06, 0xAE, 0x30, 0x97, 0, 0, 0, 0);
      //_usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x06, 0xAE, 0x30, 0xE7, 0, 0, 0, 0);
    }

    public void SetTccOff()
    {
      byte tccOffCmd = 0;
      switch (_LastSelectedGear)
      {
        case 1:
        default:
        {
          tccOffCmd = 0x95;
          break;
        }
        case 2:
        {
          tccOffCmd = 0xA5;
          break;
        }
        case 3:
        {
          tccOffCmd = 0xB5;
          break;
        }
        case 4:
        {
          tccOffCmd = 0xC5;
          break;
        }
        case 5:
        {
          tccOffCmd = 0xD5;
          break;
        }
        case 6:
        {
          tccOffCmd = 0xE5;
          break;
        }
      }
      _usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x06, 0xAE, 0x30, tccOffCmd, 0, 0, 0, 0);
      //_usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x06, 0xAE, 0x30, 0x95, 0, 0, 0, 0);
    }

    public override void ReadDTCs()
    {
    }
    public override void ClearDTCs()
    {
      _usbDevice.UsbSendSingleStdCanMessage(0x101, 8, 0xFE, 0x01, 0x04, 0, 0, 0, 0, 0);
    }
    public override void ReadVIN()
    {
    }
    public override void ResetTCU()
    {
      _usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x07, 0xAE, 0x28, 0xFF, 0, 0, 0, 0);
    }
    public override void ResetAdapts()
    {
      _usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x07, 0xAE, 0x30, 0x00, 0, 0, 0, 0);
    }

    public override float GetLastCurrentValue(int index)
    {
      switch (index)
      {
        default:
        {
          return 0.0f;
        }
        case (0):
        {
          return (float)_SolendoidValue_LU / 255.0f;
        }
        case (1):
        {
          return (float)_SolendoidValue_PL / 255.0f;
        }
        case (2):
        {
          return (float)_SolendoidValue_IC / 255.0f;
        }
        case (3):
        {
          return (float)_SolendoidValue_FRB / 255.0f;
        }
        case (4):
        {
          return (float)_SolendoidValue_DC / 255.0f;
        }
        case (5):
        {
          return (float)_SolendoidValue_HLRC / 255.0f;
        }
        case (6):
        {
          return (float)_SolendoidValue_LCB / 255.0f;
        }
      }
    }


    private void UpdateCANNode1() // Control data
    {
      byte[] data = new byte[8];
      data[0] = _SolendoidValue_LU;
      data[1] = _SolendoidValue_PL;
      data[2] = _SolendoidValue_IC;
      data[3] = _SolendoidValue_FRB;
      data[4] = _SolendoidValue_DC;
      data[5] = _SolendoidValue_HLRC;
      data[6] = _SolendoidValue_LCB;
      data[7] = 0x10;// Set pressure control, External solenoid control
      _usbDevice.UsbSendUpdateCanNodePack(data, 1);
    }


    public void InitializeSimData()
    {
      SimData_ISSValue = 1200;
      SimData_OSSValue = 50;
      SimData_EngineSpeed = 1230;
      SimData_EngineTorque = 95;
      SimData_TPS = 26;

      //UpdateCANNode1();
      //UpdateCANNode2();
      //UpdateCANNode3();

    }

    #region TCU commands



    #endregion TCU commands

    #region Gear shift commands


    #endregion Gear shift commands




    #endregion Public Methods

    #region Private Methods



    #endregion Private Methods
  }
}
