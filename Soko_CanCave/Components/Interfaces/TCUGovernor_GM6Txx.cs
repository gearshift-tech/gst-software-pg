using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Soko.Common.Interfaces;
using Soko.CanCave.Components.Interfaces.CanCaveUsb;

namespace Soko.CanCave.Components.Interfaces
{
  public class TCUGovernor_GM6Txx : Soko.Common.Interfaces.TCUGovernor
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




    #endregion Private fields

    private byte[] _tcuVinAsemblyBfr = new byte[19];// A buffer in which TCU VIN is assembled

    #region Constructors & finalizer

    public TCUGovernor_GM6Txx(UsbDevice usbIF)
    {
      _usbDevice = usbIF;
      _usbDevice.UsbCanDataReceived += new UsbDevice.UsbCanDataReceivedHandler(_usbDevice_UsbCanDataReceived);
      _GearCommandTransmitterTimer.Interval = 500;
      _GearCommandTransmitterTimer.Enabled = false;
      _GearCommandTransmitterTimer.Elapsed += new System.Timers.ElapsedEventHandler(_GearCommandTransmitterTimer_Elapsed);
      // TODO Add event handlers

      NissanDiagnosticsMimick_DiagDataBfr_Init();
    }

    public static string RemoveSpecialCharacters(string str)
    {
      StringBuilder sb = new StringBuilder();
      foreach (char c in str)
      {
        if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
        {
          sb.Append(c);
        }
      }
      return sb.ToString();
    }

    public void DecodeDTC(byte b0, byte b1)
    {
      if (b0 == 0 && b1 == 0)
      {
        // P0000 is sent as a last message in DTC readout. If it's the only message - no DTCs present.
        TcuData_ReadDtcFinished?.Invoke(this, EventArgs.Empty);
        return;
      }

      byte tmp = 0;
      string tmpDtc;

      tmp = (byte)(b0 >> 6); // Process first 2 bits that are first letter of DTC
      switch (tmp)
      {
        default:
        {
          tmpDtc = "?";
          break;
        }
        case 0:
        {
          tmpDtc = "P";
          break;
        }
        case 1:
        {
          tmpDtc = "C";
          break;
        }
        case 2:
        {
          tmpDtc = "B";
          break;
        }
        case 3:
        {
          tmpDtc = "U";
          break;
        }
      }
      tmp = (byte)( (b0 >> 4) & 0x03); // Process next 2 bits that are second letter of DTC
      tmpDtc += tmp.ToString();
      tmp = (byte)(b0 & 0x0F); // Process next 4 bis that are second letter of DTC
      tmpDtc += tmp.ToString("X");
      tmp = (byte)((b1 >> 4) & 0x0F); // Process next 4 bis that are second letter of DTC
      tmpDtc += tmp.ToString("X");
      tmp = (byte)(b1 & 0x0F); // Process next 4 bis that are second letter of DTC
      tmpDtc += tmp.ToString("X");
      _tcuData_DTCsRead.Add(tmpDtc);
      TcuData_ReadDtcOneFound?.Invoke(tmpDtc, EventArgs.Empty);
    }

    List<byte[]> NissanDiagnosticsMimick_DiagDataBfr = new List<byte[]>();

    private void NissanDiagnosticsMimick_DiagDataBfr_Init()
    {
      NissanDiagnosticsMimick_DiagDataBfr.Clear();

      NissanDiagnosticsMimick_DiagDataBfr.Add(new byte[] { 0x21, 0x00, 0x17, 0x05, 0x00, 0x15, 0x2A, 0x68 });
      System.Threading.Thread.Sleep(3);
      NissanDiagnosticsMimick_DiagDataBfr.Add(new byte[] { 0x22, 0x85, 0xB2, 0x00, 0x03, 0x15, 0x17, 0x05 });
      System.Threading.Thread.Sleep(3);
      NissanDiagnosticsMimick_DiagDataBfr.Add(new byte[] { 0x23, 0x00, 0x15, 0x1B, 0x5A, 0x36, 0x01, 0x00 });
      System.Threading.Thread.Sleep(3);
      NissanDiagnosticsMimick_DiagDataBfr.Add(new byte[] { 0x24, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
      System.Threading.Thread.Sleep(3);
      NissanDiagnosticsMimick_DiagDataBfr.Add(new byte[] { 0x25, 0x01, 0x40, 0x14, 0x72, 0x20, 0x5A, 0x36 });
      System.Threading.Thread.Sleep(3);
      NissanDiagnosticsMimick_DiagDataBfr.Add(new byte[] { 0x26, 0x01, 0xCE, 0x14, 0x0C, 0x00, 0x00, 0xFE });
      System.Threading.Thread.Sleep(3);
      NissanDiagnosticsMimick_DiagDataBfr.Add(new byte[] { 0x27, 0xFE, 0xFE, 0x00, 0x08, 0x03, 0x7E, 0x03 });
      System.Threading.Thread.Sleep(3);
      NissanDiagnosticsMimick_DiagDataBfr.Add(new byte[] { 0x28, 0x6B, 0x03, 0xE8, 0x00, 0x00, 0x00, 0x00 });
      System.Threading.Thread.Sleep(3);
      NissanDiagnosticsMimick_DiagDataBfr.Add(new byte[] { 0x29, 0x03, 0x7E, 0x03, 0x6D, 0x03, 0xE8, 0x00 });
      System.Threading.Thread.Sleep(3);
      NissanDiagnosticsMimick_DiagDataBfr.Add(new byte[] { 0x2A, 0x00, 0x19, 0x00, 0x81, 0x00, 0x00, 0x00 });
      System.Threading.Thread.Sleep(3);
      NissanDiagnosticsMimick_DiagDataBfr.Add(new byte[] { 0x2B, 0x80, 0x03, 0x00, 0x06, 0x00, 0x00, 0x03 });
      System.Threading.Thread.Sleep(3);
      NissanDiagnosticsMimick_DiagDataBfr.Add(new byte[] { 0x2C, 0x02, 0xD8, 0x80, 0x00, 0xB2, 0x2D, 0x7F });
      System.Threading.Thread.Sleep(3);
      NissanDiagnosticsMimick_DiagDataBfr.Add(new byte[] { 0x2D, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
      System.Threading.Thread.Sleep(3);
      NissanDiagnosticsMimick_DiagDataBfr.Add(new byte[] { 0x2E, 0x5C, 0x04, 0x4D, 0x4A, 0x07, 0x4D, 0x3C });
      System.Threading.Thread.Sleep(3);
      NissanDiagnosticsMimick_DiagDataBfr.Add(new byte[] { 0x2F, 0x1C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
      System.Threading.Thread.Sleep(3);
      NissanDiagnosticsMimick_DiagDataBfr.Add(new byte[] { 0x20, 0x5A, 0x00, 0x00, 0x06, 0x80, 0x0D, 0x00 });
      System.Threading.Thread.Sleep(3);
      NissanDiagnosticsMimick_DiagDataBfr.Add(new byte[] { 0x21, 0x00, 0x40, 0x00, 0x00, 0xAA, 0xAA, 0xAA });
    }

    private void NissanDiagnostickMimick_SendBulk()
    {
      for (int i = 0; i < 17; i++)
      {
        _usbDevice.UsbSendSingleStdCanMessage(0x7E9, 8, NissanDiagnosticsMimick_DiagDataBfr[i]);
      }
    }


    private void NissanDiagnostickMimick(CanMessage msg)
    {
      if (msg.remoteID == 0x7E1 && msg.data[0] == 0x02 && msg.data[1] == 0x10 & msg.data[2] == 0xC0)
      {
        _usbDevice.UsbSendSingleStdCanMessage(0x7E9, 8, 0x02, 0x50, 0xC0, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA);
      }

      if (msg.remoteID == 0x7E1 && msg.data[0] == 0x02 && msg.data[1] == 0x3E & msg.data[2] == 0x01)
      {
        _usbDevice.UsbSendSingleStdCanMessage(0x7E9, 8, 0x01, 0x7E, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA);
      }

      if (msg.remoteID == 0x7E1 && msg.data[0] == 0x02 && msg.data[1] == 0x21 & msg.data[2] == 0x01)
      {
        _usbDevice.UsbSendSingleStdCanMessage(0x7E9, 8, 0x10, 0x7A, 0x61, 0x01, 0x00, 0x00, 0x00, 0x00);
      }

      if (msg.remoteID == 0x7E1 && msg.data[0] == 0x30 && msg.data[1] == 0x00 & msg.data[2] == 0x00)
      {
        NissanDiagnostickMimick_SendBulk();
      }

    }


    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    void _usbDevice_UsbCanDataReceived(CanMessage msg)
    {
      PackPidsDoWork(msg);

     // NissanDiagnostickMimick(msg);

      


      #region TCU VIN READOUT
      if (msg.remoteID == 0x7EA && msg.data[0] == 0x10 && msg.data[1] == 0x13)
      {
        // This is the first message out of three with VIN feedback.
        _usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x30, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00);
        for (int i = 0; i < 4; i++)
        {
          _tcuVinAsemblyBfr[i] = msg.data[4 + i];
        }
        _TcuData_VIN = "WAIT.";
      }
      if (msg.remoteID == 0x7EA && msg.data[0] == 0x21)
      {
        // This is the second message out of three with VIN feedback.
        for (int i = 0; i < 7; i++)
        {
          _tcuVinAsemblyBfr[4+i] = msg.data[1 + i];
        }
        _TcuData_VIN = "WAIT..";
      }
      if (msg.remoteID == 0x7EA && msg.data[0] == 0x22)
      {
        // This is the third message out of three with VIN feedback.
        for (int i = 0; i < 6; i++)
        {
          _tcuVinAsemblyBfr[11 + i] = msg.data[1 + i];
        }
        string vin = System.Text.Encoding.ASCII.GetString(_tcuVinAsemblyBfr);
        _TcuData_VIN = RemoveSpecialCharacters(vin);
        OnTcuData_VinChanged();
        Console.WriteLine("TCU READ VIN: " + _TcuData_VIN);
      }
      #endregion TCU VIN READOUT

      #region TCU DTC READOUT
      if (msg.remoteID == 0x5EA && msg.data[0] == 0x81)
      {
        DecodeDTC(msg.data[1], msg.data[2]);
      }
      #endregion TCU DTC READOUT

      #region TCU LIVE DATA READOUT
      if (msg.remoteID == 0x0F9)
      {
        TCU_Data.Speed_Output = ((msg.data[3] << 8) + msg.data[4]) * 0.25;
        _TcuPresentFlag = true;
      }
      if (msg.remoteID == 0x1F5)
      {
        //Console.WriteLine("GLP");
        TCU_Data.CommandedGear_Actual = (TCU_StatusData_GM6Txx.CommandedGearEnum)Enum.ToObject(typeof(TCU_StatusData_GM6Txx.CommandedGearEnum), msg.data[0] & 0x0F);
        TCU_Data.CommandedGear_Desired = (TCU_StatusData_GM6Txx.CommandedGearEnum)Enum.ToObject(typeof(TCU_StatusData_GM6Txx.CommandedGearEnum), msg.data[1] & 0x0F);
        TCU_Data.GearLeverPosition = (TCU_StatusData_GM6Txx.GearLeverPositionEnum)Enum.ToObject(typeof(TCU_StatusData_GM6Txx.GearLeverPositionEnum), msg.data[3] & 0x0F);
        //Console.WriteLine((msg.data[3] & 0x0F).ToString());
      }
      if (msg.remoteID == 0x19D)
      {
        UInt16 issTmp = (UInt16)(msg.data[5] << 8);
        issTmp += msg.data[6];
        TCU_Data.Speed_Input = issTmp * 0.25f;
        TCU_Data.Speed_Slip = SimData_EngineSpeed - TCU_Data.Speed_Input;
        TCU_Data.GearRatio = msg.data[7] * 0.03125f;
      }
      if (msg.remoteID == 0x5EA && msg.data[0] == 0xFA)
      {
        TCU_Data.Solenoid_Shift1 = ((msg.data[1] & 0x80) > 0);
        TCU_Data.Solenoid_Shift2 = ((msg.data[2] & 0x80) > 0);
        TCU_Data.Switch_Brake = ((msg.data[3] & 0x08) > 0);
        TCU_Data.Switch_Pressure4 = ((msg.data[4] & 0x80) > 0);
        TCU_Data.Switch_Pressure1 = ((msg.data[4] & 0x40) > 0);
        TCU_Data.Switch_Pressure3 = ((msg.data[4] & 0x20) > 0);
        TCU_Data.Switch_Pressure5 = ((msg.data[4] & 0x10) > 0);
      }
      if (msg.remoteID == 0x5EA && msg.data[0] == 0xFB)
      {
        TCU_Data.Solenoid_PC6 = ((msg.data[1] << 8) + msg.data[2]) * 0.009063720703120001;
        TCU_Data.diagTccSlipSpeed = ((msg.data[3] << 8) + msg.data[4]) * 0.125;
        TCU_Data.EngineCoolandTemp = (msg.data[5]) - 40;
        TCU_Data.IgnitionVoltage = (msg.data[6]) * 0.1;
        TCU_Data.vehicleSpeed = (msg.data[7]) * 1;

      }
      if (msg.remoteID == 0x5EA && msg.data[0] == 0xFC)
      {
        TCU_Data.Solenoid_PC3 = ((msg.data[1] << 8) + msg.data[2]) * 0.009063720703120001;
        TCU_Data.Solenoid_PC4 = ((msg.data[3] << 8) + msg.data[4]) * 0.009063720703120001;
        TCU_Data.Solenoid_PC5 = ((msg.data[5] << 8) + msg.data[6]) * 0.009063720703120001;
        TCU_Data.diagCalculatedTPS = (msg.data[7]) * 0.390625;
      }
      if (msg.remoteID == 0x5EA && msg.data[0] == 0xFD)
      {
        // OSS Line PC, PC2, gear ratio
        TCU_Data.Solenoid_PC1 = ((msg.data[1] << 8) + msg.data[2]) * 0.035;
        TCU_Data.Solenoid_PC2 = ((msg.data[3] << 8) + msg.data[4]) * 0.009063720703120001;
        
      }
      if (msg.remoteID == 0x5EA && msg.data[0] == 0xFE)
      {
        // Engine speed torque ISS commanded gear
      }
      if (msg.remoteID == 0x7EA && msg.data[1] == 0x62 && msg.data[2] == 0x28 && msg.data[3] == 0x0D)
      {
        TCU_Data.TcuTemperature = msg.data[4] - 40.0;
      }
      if (msg.remoteID == 0x7EA && msg.data[1] == 0x62 && msg.data[2] == 0x19 && msg.data[3] == 0x40)
      {
        TCU_Data.FluidTemperature = msg.data[4] - 40.0;
      }

      #endregion TCU LIVE DATA READOUT
    }

    void _GearCommandTransmitterTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
      //SendSelectGearCommand(_LastSelectedGear);
      //System.Threading.Thread.Sleep(100);
      SendTesterPresentMessage();
      System.Threading.Thread.Sleep(100);
      RequestFluidTemp();
      System.Threading.Thread.Sleep(100);
      RequestTcmTemp();
    }

    #endregion Constructors & finalizer



    #region Events

    public event EventHandler SimData_EngineSpeedChanged;
    public event EventHandler SimData_EngineTPSChanged;
    public event EventHandler SimData_EngineTorqueChanged;
    public event EventHandler SimData_ISSValueChanged;
    public event EventHandler SimData_OSSValueChanged;

    public event EventHandler TcuData_ReadDtcStarted;
    public event EventHandler TcuData_ReadDtcOneFound;
    public event EventHandler TcuData_ReadDtcFinished;
    public event EventHandler TcuData_ReadDtcFailed;

    public event EventHandler TcuData_ReadVinStarted;
    public event EventHandler TcuData_ReadVinCompleted;
    public event EventHandler TcuData_ReadVinFailed;


    #endregion Events



    #region Properties

    public UsbDevice UsbDevice
    {
      get { return _usbDevice; }
    }

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

    public TCU_StatusData_GM6Txx TCU_Data = new TCU_StatusData_GM6Txx();

    private UInt16 _SimData_EngineTorque = 94; // Initialize to 94
    public UInt16 SimData_EngineTorque
    {
      get { return _SimData_EngineTorque; }
      set
      {
        _SimData_EngineTorque = value;
        UpdateCANNode1();
        SimData_EngineTorqueChanged?.Invoke(this, EventArgs.Empty);
      }
    }

    private UInt16 _SimData_TPS = 26;
    public UInt16 SimData_TPS
    {
      get { return _SimData_TPS; }
      set
      {
        _SimData_TPS =  value;
        UpdateCANNode1();
        UpdateCANNode2(); // TPS is sent in 2 nodes
        SimData_EngineTPSChanged?.Invoke(this, EventArgs.Empty);
      }
    }

    private UInt16 _SimData_EngineSpeed = 1230;
    public UInt16 SimData_EngineSpeed
    {
      get { return _SimData_EngineSpeed; }
      set
      {
        _SimData_EngineSpeed =  value;
        UpdateCANNode2();
        SimData_EngineSpeedChanged?.Invoke(this, EventArgs.Empty);
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

    public void SimData_SetMultipleValues(UInt16 EngineSpeed, UInt16 TPS, UInt16 EngineTorque, UInt16 ISSValue, UInt16 OSSValue)
    {
      _SimData_EngineSpeed = EngineSpeed;
      _SimData_TPS = TPS;
      _SimData_EngineTorque = EngineTorque;
      _SimData_ISSValue = ISSValue;
      _SimData_OSSValue = OSSValue;
      UpdateCANNode1();
      UpdateCANNode2();
      _usbDevice.UsbSendUpdateSSEMUDataPack((UInt16)(0.6 * _SimData_ISSValue), (UInt16)(0.6 * _SimData_OSSValue));

    }

    private void OnTcuData_VinChanged()
    {
    }
    private String _TcuData_VIN;
    public String TcuData_VIN
    {
      get {return _TcuData_VIN;}
    }

    #endregion Properties


    #region Public Methods


    private void PackPidsDoWork(CanMessage msg)
    {
      if (_PidPackingInProgress)
      {
        switch (_PidPackingStage)
        {
          case 0:
          default:
            {
              return;
            }

          case 1:
            {
              if (msg.remoteID == 0x7EA && msg.data[0] == 0x30 && msg.data[1] == 0x00 & msg.data[2] == 0x0A)
              {
                _usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x21, 0x19, 0x41, 0x19, 0x9A, 0xAA, 0xAA, 0xAA);
                _PidPackingStage = 2;
              }
              break;
            }

          case 2:
            {
              if (msg.remoteID == 0x7EA && msg.data[0] == 0x02 && msg.data[1] == 0x6C & msg.data[2] == 0xFE)
              {
                _usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x10, 0x0A, 0x2C, 0xFD, 0x19, 0x42, 0x28, 0x17);
                _PidPackingStage = 3;
              }
              break;
            }
          case 3:
            {
              if (msg.remoteID == 0x7EA && msg.data[0] == 0x30 && msg.data[1] == 0x00 & msg.data[2] == 0x0A)
              {
                _usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x21, 0x28, 0x18, 0x19, 0x5B, 0xAA, 0xAA, 0xAA);
                _PidPackingStage = 4;
              }
              break;
            }
          case 4:
            {
              if (msg.remoteID == 0x7EA && msg.data[0] == 0x03 && msg.data[1] == 0x7F & msg.data[2] == 0x2C)
              {
                _usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x10, 0x0A, 0x2C, 0xFC, 0x28, 0x19, 0x28, 0x1A);
                _PidPackingStage = 5;
              }
              break;
            }
          case 5:
            {
              if (msg.remoteID == 0x7EA && msg.data[0] == 0x30 && msg.data[1] == 0x00 & msg.data[2] == 0x0A)
              {
                 _usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x21, 0x28, 0x1B, 0x28, 0x10, 0xAA, 0xAA, 0xAA);
                _PidPackingStage = 6;
              }
              break;
            }
          case 6:
            {
              if (msg.remoteID == 0x7EA && msg.data[0] == 0x02 && msg.data[1] == 0x6C & msg.data[2] == 0xFC)
              {
                _usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x10, 0x0C, 0x2C, 0xFB, 0x28, 0x1C, 0x19, 0x91);
                _PidPackingStage = 7;
              }
              break;
            }
          case 7:
            {
              if (msg.remoteID == 0x7EA && msg.data[0] == 0x30 && msg.data[1] == 0x00 & msg.data[2] == 0x0A)
              {
                _usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x21, 0x00, 0x05, 0x11, 0x41, 0x00, 0x0d, 0xAA);
                _PidPackingStage = 8;
              }
              break;
            }
          case 8:
            {
              if (msg.remoteID == 0x7EA && msg.data[0] == 0x02 && msg.data[1] == 0x6C & msg.data[2] == 0xFB)
              {
                _usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x10, 0x0a, 0x2c, 0xFA, 0x28, 0x1E, 0x28, 0x1F);
                _PidPackingStage = 9;
              }
              break;
            }
          case 9:
            {
              if (msg.remoteID == 0x7EA && msg.data[0] == 0x30 && msg.data[1] == 0x00 & msg.data[2] == 0x0A)
              {
                _usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x21, 0x11, 0x24, 0x28, 0x1D, 0xAA, 0xAA, 0xAA);
                _PidPackingStage = 10;
              }
              break;
            }
          case 10:
            {
              if (msg.remoteID == 0x7EA && msg.data[0] == 0x02 && msg.data[1] == 0x6C & msg.data[2] == 0xFA)
              {
                _usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x07, 0xAA, 0x04, 0xFE, 0xFD, 0xFC, 0xFB, 0xFA);
                _PidPackingStage = 11;
              }
              break;

            }

          case 11:
            {
              if (msg.remoteID == 0x7EA && msg.data[0] == 0x01 && msg.data[1] == 0x60 & msg.data[2] == 0xAA)
              {
                _usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x03, 0x22, 0x28, 0x0D, 0xAA, 0xAA, 0xAA, 0xAA);
                _PidPackingStage = 12;
                _GearCommandTransmitterTimer.Enabled = true;
              }
              break;

            }


        }
      }

    }

    private bool _PidPackingInProgress = false;
    private int _PidPackingStage = 0;
    public void PackPIDs()
    {
      _PidPackingStage = 0;
      _PidPackingInProgress = true;
      int sleepms = 100;
      SendTesterPresentMessage();
      //System.Threading.Thread.Sleep(sleepms);//was 30
      _usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x10, 0x0A, 0x2C, 0xFE, 0x00, 0x0C, 0x1A, 0x2D);
      _PidPackingStage = 1;
     // System.Threading.Thread.Sleep(sleepms);//was 15
     // _usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x21, 0x19, 0x41, 0x19, 0x9A, 0xAA, 0xAA, 0xAA); s1
     //System.Threading.Thread.Sleep(sleepms);
     // _usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x10, 0x0A, 0x2C, 0xFD, 0x19, 0x42, 0x28, 0x17); s2
     // //System.Threading.Thread.Sleep(sleepms);
     // _usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x21, 0x28, 0x18, 0x19, 0x5B, 0xAA, 0xAA, 0xAA); s3
     // //System.Threading.Thread.Sleep(sleepms);
     // _usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x10, 0x0A, 0x2C, 0xFC, 0x28, 0x19, 0x28, 0x1A); s4
     //// System.Threading.Thread.Sleep(sleepms);
     // _usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x21, 0x28, 0x1B, 0x28, 0x10, 0xAA, 0xAA, 0xAA); s5
     // //System.Threading.Thread.Sleep(sleepms);
     // _usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x10, 0x0C, 0x2C, 0xFB, 0x28, 0x1C, 0x19, 0x91); s6
     //// System.Threading.Thread.Sleep(sleepms);
     // _usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x21, 0x00, 0x05, 0x11, 0x41, 0x00, 0x0d, 0xAA); s7
     // //System.Threading.Thread.Sleep(sleepms);
     // _usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x10, 0x0a, 0x2c, 0xFA, 0x28, 0x1E, 0x28, 0x1F); s8
     // //System.Threading.Thread.Sleep(sleepms);
     // _usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x21, 0x11, 0x24, 0x28, 0x1D, 0xAA, 0xAA, 0xAA); s9

      //System.Threading.Thread.Sleep(sleepms * 2);//was 50
      //_usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x07, 0xAA, 0x04, 0xFE, 0xFD, 0xFC, 0xFB, 0xFA);

      //System.Threading.Thread.Sleep(sleepms * 3); // was 100
      //_usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x03, 0x22, 0x28, 0x0D, 0xAA, 0xAA, 0xAA, 0xAA);
    }

    public override void InitializeTcu()
    {
      _GearCommandTransmitterTimer.Enabled = false;
      System.Threading.Thread.Sleep(200);
      InitializeSimData();
      UpdateCANNode1();
      UpdateCANNode2();
      UpdateCANNode3();

      _usbDevice.UsbSendUnqueuedCommandOnlyPacket(USBPacketCommandCode.CMD_TCUGOV_SelectGM6TMode);

      //SendTesterPresentMessage();
      PackPIDs();

      _GearCommandTransmitterTimer.Enabled = false;

      //// Look for a gearbox (check if periodic data is being received)
      //// Read vin, if succeeded set the  'gearbox present'flag
      //ReadVIN();
      //_TcuPresentFlag = true;  
    }

    public override void EnableDrive()
    {
      //SendTesterPresentMessage();
      _GearCommandTransmitterTimer.Enabled = true;
      UpdateCANNode1();
      UpdateCANNode2();
      UpdateCANNode3();
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
    public override void SelectGear(int gear)
    {
      gear = (gear < 1) ? 1 : gear;
      gear = (gear > 6) ? 6 : gear;
      Console.WriteLine("GM6T Select " + gear.ToString());
      _LastTCCState = false;
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

      //_usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x06, 0xAE, 0x30, gearcode, 0, 0, 0, 0);
      _LastSelectedGear = gear;
      SendSelectGearCommand(_LastSelectedGear);
    }

    public int LastSelectedGear
    {
      get { return _LastSelectedGear; }
    }

    public void SetTccOn()
    {
      _LastTCCState = true;
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
      _LastTCCState = false;
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

    public List<string> _tcuData_DTCsRead = new List<string>();

    public override void ReadDTCs()
    {
      TcuData_ReadDtcStarted?.Invoke(this, null);
      _tcuData_DTCsRead.Clear(); // Clear list of DTCs
      _usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x03, 0xA9, 0x81, 0x52, 0x00, 0x00, 0x00, 0x00);
    }

    public override void ClearDTCs()
    {
      _usbDevice.UsbSendSingleStdCanMessage(0x101, 8, 0xFE, 0x01, 0x04, 0, 0, 0, 0, 0);
    }

    public override void ReadVIN()
    {
      _TcuData_VIN = "WAIT";
      _usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x02, 0x1A, 0x90, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA);
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
      return 1.1f;
    }

    public void NullTps()
    {
      _SimData_TPS = 0;
      _SimData_EngineSpeed = 0;

      UpdateCANNode1();
      UpdateCANNode2();
      UpdateCANNode3();
    }


    #region TCU commands



    #endregion TCU commands

    #region Gear shift commands


    #endregion Gear shift commands




    #endregion Public Methods

    #region Private Methods

    private void UpdateCANNode1() // address 191, Engine torque and TPS
    {
      // TPS x*2.55
      // Torque (x+848)*2
      // speed x*4
      UInt16 rawTorque = (UInt16)((SimData_EngineTorque + 848) * 2);
      byte[] data = new byte[8];
      data[0] = (byte)(((rawTorque >> 8) & 0xF));
      data[1] = (byte)(rawTorque);
      data[2] = (byte)(((rawTorque >> 8) & 0xF));
      data[3] = (byte)(rawTorque);
      data[4] = (byte)(((rawTorque >> 8) & 0xF));
      data[5] = (byte)(rawTorque);
      data[6] = (byte)(_SimData_TPS * 2.56f);
      data[7] = (byte)(_SimData_TPS * 2.56f);
      _usbDevice.UsbSendUpdateCanNodePack(data, 1);
    }

    private void UpdateCANNode2() // address C9, Engine speed, TPS, 
    {
      UInt16 rawEngineSpeed = (UInt16)(SimData_EngineSpeed * 4);
      byte[] data = new byte[8];
      data[0] = 0x80; // 1000 0100 b
      data[1] = (byte)(rawEngineSpeed >> 8);
      data[2] = (byte)(rawEngineSpeed);
      data[3] = 0;
      data[4] = (byte)(_SimData_TPS * 2.56f);
      data[5] = 0;
      data[6] = 0;
      data[7] = 0;
      _usbDevice.UsbSendUpdateCanNodePack(data, 2);
    }

    private void UpdateCANNode3() // nothing but zeroes
    {
      byte[] data = new byte[8];
      data[0] = 0x00;
      data[1] = 0x20;
      data[2] = 0x41;
      data[3] = 0x40;
      data[4] = 0x6C;
      data[5] = 0x55;
      data[6] = 0x42;
      data[7] = 0;
      _usbDevice.UsbSendUpdateCanNodePack(data, 3);
    }

    private void InitializeSimData()
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

    private void RequestTcmTemp()
    {
      _usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x03, 0x22, 0x28, 0x0D, 0xAA, 0xAA, 0xAA, 0xAA);
    }

    private void RequestFluidTemp()
    {
      _usbDevice.UsbSendSingleStdCanMessage(0x7E2, 8, 0x03, 0x22, 0x19, 0x40, 0xAA, 0xAA, 0xAA, 0xAA);
    }

    private void SendTesterPresentMessage()
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

    #endregion Private Methods
  }
}
