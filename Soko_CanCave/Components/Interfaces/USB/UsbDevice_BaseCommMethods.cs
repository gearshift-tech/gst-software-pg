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
using Soko.Common.Interfaces;

namespace Soko.CanCave.Components.Interfaces.CanCaveUsb
{

  public partial class UsbDevice
  {

    /// <summary>
    /// Send USB packet carrying only the command code, must be connected or trying to connect
    /// </summary>
    /// <param name="cmd">Command code to send</param>
    public void UsbSendUnqueuedCommandOnlyPacket(USBPacketCommandCode cmd)
    {
      if (_deviceConnected || _isTryingToConnect)
      {
        ErrorCode ec = ErrorCode.None;
        USBPacket_Generic pkt = new USBPacket_Generic();
        int bytesWritten;
        pkt.CommandCode = cmd;
        ec = _usbEndPointWriter.Write(PacketToByteArray(pkt), mDeviceTransmitTimeoutMs, out bytesWritten);
        if (ec != ErrorCode.None)
        {
          throw new Exception("Send cmd only ex: " + LibUsbDotNet.UsbDevice.LastErrorString);
        }
      }
      else
      {
        throw new InvalidOperationException("Unable to send command packet if not connected or not trying to connect");
      }
    }

    public void UsbSendUpdateCanNodePack(byte[] data, int nodeIndex)
    {
        USBPacket_UPDATE_NODE_CAN pkt = new USBPacket_UPDATE_NODE_CAN();
        switch (nodeIndex)
        {
            case 1:
            default:
                {
                    pkt.CommandCode = USBPacketCommandCode.CMD_CAN_UPDATE_NODE_1;
                    break;
                }
            case 2:
                {
                    pkt.CommandCode = USBPacketCommandCode.CMD_CAN_UPDATE_NODE_2;
                    break;
                }
            case 3:
                {
                    pkt.CommandCode = USBPacketCommandCode.CMD_CAN_UPDATE_NODE_3;
                    break;
                }
        }
        pkt.data.byte0 = data[0];
        pkt.data.byte1 = data[1];
        pkt.data.byte2 = data[2];
        pkt.data.byte3 = data[3];
        pkt.data.byte4 = data[4];
        pkt.data.byte5 = data[5];
        pkt.data.byte6 = data[6];
        pkt.data.byte7 = data[7];
        USB_TxBuffer.Add(PacketToByteArray(pkt));
    }

    public void UsbSendSingleCanMessage(CanMessage msg)
    {
      USBPacket_CANDataPacket pkt = new USBPacket_CANDataPacket
      {
        CommandCode = USBPacketCommandCode.CMD_CAN_DATA,
        msgCount = 1,
        msgs = new UsbCANData[3]
      };
      pkt.msgs[0] = CAN_CVT_CanMsgToUsbData(msg);
        //pkt.msgs[1] = msg.ToUsbCanData();
        //pkt.msgs[2] = msg.ToUsbCanData();
        byte[] bfr = PacketToByteArray(pkt);
        USB_TxBuffer.Add(PacketToByteArray(pkt));
    }

    public void UsbSendSingleStdCanMessage(UInt32 ID, byte DLC, byte[] bfr)
    {
      if (bfr.Length < DLC)
      {
        return;
      }
      UsbSendSingleStdCanMessage(ID, DLC, bfr[0], bfr[1], bfr[2], bfr[3], bfr[4], bfr[5], bfr[6], bfr[7]);
    }

    public void UsbSendSingleStdCanMessage(UInt32 ID, byte DLC, byte D0, byte D1, byte D2, byte D3, byte D4, byte D5, byte D6, byte D7)
    {
      CanMessage msg = new CanMessage
      {
        remoteID = ID,
        isRTRFrame = false,
        isXtdFrameType = false,
        DLC = DLC
      };
      msg.data[0] = D0;
        msg.data[1] = D1;
        msg.data[2] = D2;
        msg.data[3] = D3;
        msg.data[4] = D4;
        msg.data[5] = D5;
        msg.data[6] = D6;
        msg.data[7] = D7;

      USBPacket_CANDataPacket pkt = new USBPacket_CANDataPacket
      {
        CommandCode = USBPacketCommandCode.CMD_CAN_DATA,
        msgCount = 1,
        msgs = new UsbCANData[3]
      };
      pkt.msgs[0] = CAN_CVT_CanMsgToUsbData(msg);
      //pkt.msgs[1] = msg.ToUsbCanData();
      //pkt.msgs[2] = msg.ToUsbCanData();
      byte[] bfr = PacketToByteArray(pkt);
        USB_TxBuffer.Add(PacketToByteArray(pkt));
    }

    public void UsbSendUpdateSSEMUDataPack(UInt16 ss1, UInt16 ss2)
    {
      USBPacket_SSEmuData pkt = new USBPacket_SSEmuData
      {
        CommandCode = USBPacketCommandCode.CMD_SSEMU_SetFrequencies,
        SS1Freq = ss1,
        SS2Freq = ss2
      };
      USB_TxBuffer.Add(PacketToByteArray(pkt));
    }

    private void UsbSendUnqueuedResetTimestampCmd()
    {
      ErrorCode ec = ErrorCode.None;
      USBPacket_Generic pkt = new USBPacket_Generic();
      int bytesWritten;
      if (_deviceConnected)
      {
        try
        {
          pkt.CommandCode = USBPacketCommandCode.CMD_RESET_TMR;
          ec = _usbEndPointWriter.Write(PacketToByteArray(pkt), mDeviceTransmitTimeoutMs, out bytesWritten);
          if (ec != ErrorCode.None)
          {
            EventLogger.DeviceError("USB_ResetTimeStampTimer err: " + LibUsbDotNet.UsbDevice.LastErrorString);
          }
        }
        catch (Exception e)
        {
          EventLogger.DeviceError("USB_ResetTimeStampTimer ex: " + e.Message);
        }
      }
    }

    private void SaveKlineDataToCSVWbdelPriIF()
    {
      int maxrowsCount = 0;

      foreach (List<string> pf in primaryFrames)
      {
        if (pf.Count > maxrowsCount) maxrowsCount = pf.Count;
      }
      foreach (List<string> sf in secondaryFrames)
      {
        if (sf.Count > maxrowsCount) maxrowsCount = sf.Count;
      }

      List<string> rows = new List<string>();
      for (int j = 0; j < maxrowsCount; j++)
      {
        rows.Add(string.Empty);
      }

      for (int i = 0; i < primaryFrames.Count; i++)
      {
        List<string> cpf = primaryFrames[i];
        List<string> csf;
        if (i < secondaryFrames.Count)
        {
          csf = secondaryFrames[i];
        }
        else
        {
          csf = new List<string>();
        }

        for (int j = 0; j < maxrowsCount; j++)
        {
          if (j < cpf.Count)
          {
            rows[j] += cpf[j] + ",";
          }
          else
          {
            rows[j] += ",";
          }

          if (j < csf.Count)
          {
            rows[j] += ",";
          }
          else
          {
            rows[j] += ",";
          }
        }
      }

      System.IO.File.WriteAllLines(@"C:WithDelayPrimIF.csv", rows.ToArray());
    }

    private void SaveKlineDataToCSVWbdelSecIF()
    {
      int maxrowsCount = 0;

      foreach (List<string> pf in primaryFrames)
      {
        if (pf.Count > maxrowsCount) maxrowsCount = pf.Count;
      }
      foreach (List<string> sf in secondaryFrames)
      {
        if (sf.Count > maxrowsCount) maxrowsCount = sf.Count;
      }

      List<string> rows = new List<string>();
      for (int j = 0; j < maxrowsCount; j++)
      {
        rows.Add(string.Empty);
      }

      for (int i = 0; i < primaryFrames.Count; i++)
      {
        List<string> cpf = primaryFrames[i];
        List<string> csf;
        if (i < secondaryFrames.Count)
        {
          csf = secondaryFrames[i];
        }
        else
        {
          csf = new List<string>();
        }

        for (int j = 0; j < maxrowsCount; j++)
        {
          if (j < cpf.Count)
          {
            rows[j] += ",";
          }
          else
          {
            rows[j] += ",";
          }

          if (j < csf.Count)
          {
            rows[j] += csf[j] + ",";
          }
          else
          {
            rows[j] += ",";
          }
        }
      }

      System.IO.File.WriteAllLines(@"C:WithDelaySecIF.csv", rows.ToArray());
    }

    private void SaveKlineDataToCSVWobdelPriIF()
    {
      int maxrowsCount = 0;

      foreach (List<string> pf in primaryFrames)
      {
        if (pf.Count > maxrowsCount) maxrowsCount = pf.Count;
      }
      List<string> rows = new List<string>();
      for (int j = 0; j < maxrowsCount; j++)
      {
        rows.Add(string.Empty);
      }

      for (int i = 0; i < primaryFrames.Count; i++)
      {
        List<string> cpf = primaryFrames[i];
        for (int j = 0; j < maxrowsCount; j++)
        {
          if (j < cpf.Count)
          {
            rows[j] += cpf[j].Remove(0, 5) + ",";
          }
          else
          {
            rows[j] += ",";
          }
        }
      }

      System.IO.File.WriteAllLines(@"C:WithoutDelayPriIF.csv", rows.ToArray());
    }

    private void SaveKlineDataToCSVWobdelSecIF()
    {
      int maxrowsCount = 0;

      foreach (List<string> sf in secondaryFrames)
      {
        if (sf.Count > maxrowsCount) maxrowsCount = sf.Count;
      }
      List<string> rows = new List<string>();
      for (int j = 0; j < maxrowsCount; j++)
      {
        rows.Add(string.Empty);
      }

      for (int i = 0; i < primaryFrames.Count; i++)
      {
        List<string> spf = secondaryFrames[i];
        for (int j = 0; j < maxrowsCount; j++)
        {
          if (j < spf.Count)
          {
            rows[j] += spf[j].Remove(0, 5) + ",";
          }
          else
          {
            rows[j] += ",";
          }
        }
      }

      System.IO.File.WriteAllLines(@"C:WithoutDelaySecIF.csv", rows.ToArray());
    }

    #region K-Line interface

    /// <summary>
    /// Enables K-Line interface on the device
    /// Data is added to USB TX queue, use with caution in real-time operations
    /// </summary>
    public void EnableKline()
    {
      USBPacket_Generic pkt = new USBPacket_Generic
      {
        CommandCode = USBPacketCommandCode.CMD_KLINE_ENABLE
      };
      USB_TxBuffer.Add(PacketToByteArray(pkt));

      primaryFrames.Clear();
      secondaryFrames.Clear();
      primaryFrame.Clear();
      secondaryFrame.Clear();
    }

    /// <summary>
    /// Disables K-Line interface on the device
    /// Data is added to USB TX queue, use with caution in real-time operations
    /// </summary>
    public void DisableKline()
    {
      USBPacket_Generic pkt = new USBPacket_Generic
      {
        CommandCode = USBPacketCommandCode.CMD_KLINE_ENABLE
      };
      USB_TxBuffer.Add(PacketToByteArray(pkt));
    }

    public void EnterBootloader()
    {
      USBPacket_Generic pkt = new USBPacket_Generic
      {
        CommandCode = USBPacketCommandCode.CMD_BLD_ENTER
      };
      USB_TxBuffer.Add(PacketToByteArray(pkt));
    }

    public void SaveDumpData()
    {
      SaveKlineDataToCSVWbdelPriIF();
      SaveKlineDataToCSVWbdelSecIF();
      SaveKlineDataToCSVWobdelPriIF();
      SaveKlineDataToCSVWobdelSecIF();
    }

    #endregion K-Line interface

    #region Gearbox control

    /// <summary>
    /// Selects gearbox type
    /// Data is added to USB TX queue, use with caution in real-time operations
    /// </summary>
    /// <param name="type"></param>
    public void SelectGearboxType(GearboxControllerType type)
    {
      if (IsConnected)
      {
        USBPacket_GEARBOX_SELECT pkt = new USBPacket_GEARBOX_SELECT
        {
          CommandCode = USBPacketCommandCode.CMD_KLINE_SLCT_GBX, // Make sure the command code is right
          GearboxCode = (byte)type
        };

        USB_TxBuffer.Add(PacketToByteArray(pkt));
      }
    }

    /// <summary>
    /// Selects a gear on a mechatronic unit
    /// Data is added to USB TX queue, use with caution in real-time operations
    /// </summary>
    /// <param name="gearCode"></param>
    public void SelectGear(byte gearCode)
    {
      Console.WriteLine("Switch gear: " + _lastGearSelected.ToString() + " & " + gearCode.ToString());
      if (IsConnected && gearCode != _lastGearSelected)
      {
        USBPacket_GEAR_SELECT pkt = new USBPacket_GEAR_SELECT
        {
          CommandCode = USBPacketCommandCode.CMD_KLINE_SLCT_GEAR, // Make sure the command code is right
          GearCode = gearCode
        };
        USB_TxBuffer.Add(PacketToByteArray(pkt));
        _lastGearSelected = gearCode;
      }
      Console.WriteLine("Switch gear 2: " + _lastGearSelected.ToString() + " & " + gearCode.ToString());
    }

    /// <summary>
    /// Starts driving the gearbox
    /// Data is added to USB TX queue, use with caution in real-time operations
    /// </summary>
    /// <param name="gearCode"></param>
    public void EnableGearboxDrive()
    {
      if (IsConnected)
      {
        USBPacket_Generic pkt = new USBPacket_Generic
        {
          CommandCode = USBPacketCommandCode.CMD_KLINE_ENABLE_DRIVE // Make sure the command code is right
        };
        USB_TxBuffer.Add(PacketToByteArray(pkt));
      }
    }

    /// <summary>
    /// Stops driving the gearbox
    /// Data is added to USB TX queue, use with caution in real-time operations
    /// </summary>
    /// <param name="gearCode"></param>
    public void DisableGearboxDrive()
    {
      if (IsConnected)
      {
        USBPacket_Generic pkt = new USBPacket_Generic
        {
          CommandCode = USBPacketCommandCode.CMD_KLINE_DISABLE_DRIVE // Make sure the command code is right
        };
        USB_TxBuffer.Add(PacketToByteArray(pkt));
      }
    }

    /// <summary>
    /// Starts MD Frame impersonation
    /// Data is added to USB TX queue, use with caution in real-time operations
    /// </summary>
    /// <param name="gearCode"></param>
    public void EnableMDFrame()
    {
      if (IsConnected)
      {
        USBPacket_Generic pkt = new USBPacket_Generic
        {
          CommandCode = USBPacketCommandCode.CMD_KLINE_ENABLE_MDFRAME // Make sure the command code is right
        };
        USB_TxBuffer.Add(PacketToByteArray(pkt));
      }
    }

    /// <summary>
    /// Stops MD Frame impersonation
    /// Data is added to USB TX queue, use with caution in real-time operations
    /// </summary>
    /// <param name="gearCode"></param>
    public void DisableMDFrame()
    {
      if (IsConnected)
      {
        USBPacket_Generic pkt = new USBPacket_Generic
        {
          CommandCode = USBPacketCommandCode.CMD_KLINE_DISABLE_MDFRAME // Make sure the command code is right
        };
        USB_TxBuffer.Add(PacketToByteArray(pkt));
      }
    }

    public int LastFrameLength
    {
      get { return _lastCorrectFrame.Count; }
    }

    public int gbxconnstatraw
    {
      get
      {
        if (_lastCorrectFrame.Count == 154)
          return _lastCorrectFrame[115];
        if (_lastCorrectFrame.Count == 142)
          return _lastCorrectFrame[112];

        return 0;
      }
    }

    public GearboxConnectionStatus GearboxConnectionStatus
    {
      get
      {
        if (_lastCorrectFrame.Count > 115)
        {
          short stat = 0;
          if (_lastCorrectFrame.Count == 154)
            stat = _lastCorrectFrame[115];
          if (_lastCorrectFrame.Count == 142)
            stat = _lastCorrectFrame[112];

          switch (stat)
          {
            default:
            case 0:
              {
                return GearboxConnectionStatus.Idle;
              }
            case 0x0C:
            case 0x13:
              {
                return GearboxConnectionStatus.Connecting;
              }
            case 0x0B: // CE / CM
            case 0x12: // CE / CM
            case 0x0A: // TUCE / TUCM
            case 0x18: // TUCE / TUCM
              {
                return GearboxConnectionStatus.Connected;
              }
          }
        }
        else
        {
          return GearboxConnectionStatus.Idle;
        }
      }
    }

    public int GearboxPhysicallySelectedGear
    {
      get
      {
        if (_lastCorrectFrame.Count > 9)
        {
          return _lastCorrectFrame[9];
        }
        else
        {
          return 0;
        }
      }
    }

    public int GearboxSwitchingInProgress
    {
      get
      {
        if (_lastCorrectFrame.Count > 8)
        {
          return _lastCorrectFrame[8];
        }
        else
        {
          return 0;
        }
      }
    }

    public float GetLastCurrentValue(int index)
    {
      switch (index)
      {
        default:
          {
            return 0;
          }
        case 1:
          {
            if (_lastCorrectFrame.Count > 62)
            {
              return (_lastCorrectFrame[61] * 256 + _lastCorrectFrame[62]) / 10000f;
            }
            else
            {
              return 0;
            }
          }
        case 2:
          {
            if (_lastCorrectFrame.Count > 64)
            {
              return (_lastCorrectFrame[63] * 256 + _lastCorrectFrame[64]) / 10000f;
            }
            else
            {
              return 0;
            }
          }
        case 3:
          {
            if (_lastCorrectFrame.Count > 66)
            {
              return (_lastCorrectFrame[65] * 256 + _lastCorrectFrame[66]) / 10000f;
            }
            else
            {
              return 0;
            }
          }
        case 4:
          {
            if (_lastCorrectFrame.Count > 68)
            {
              return (_lastCorrectFrame[67] * 256 + _lastCorrectFrame[68]) / 10000f;
            }
            else
            {
              return 0;
            }
          }
        case 5:
          {
            if (_lastCorrectFrame.Count > 70)
            {
              return (_lastCorrectFrame[69] * 256 + _lastCorrectFrame[70]) / 10000f;
            }
            else
            {
              return 0;
            }
          }
        case 6:
          {
            if (_lastCorrectFrame.Count > 72)
            {
              return (_lastCorrectFrame[71] * 256 + _lastCorrectFrame[72]) / 10000f;
            }
            else
            {
              return 0;
            }
          }
      }
    }

    private int _lastEDS5Value = 0;
    private int _lastEDS6Value = 0;

    public void SetEDS5Value(int value)
    {
      _lastEDS5Value = (int)(value * 2.56f);
      SetEDSValues();
    }

    public void SetEDS6Value(int value)
    {
      _lastEDS6Value = (int)(value * 2.56f);
      SetEDSValues();
    }

    private void SetEDSValues()
    {
      if (IsConnected)
      {
        USBPacket_EDS_DATA pkt = new USBPacket_EDS_DATA
        {
          CommandCode = USBPacketCommandCode.CMD_KLINE_SET_EDS, // Make sure the command code is right
          EDS5Val = (byte)_lastEDS5Value,
          EDS6Val = (byte)_lastEDS6Value
        };
        USB_TxBuffer.Add(PacketToByteArray(pkt));
      }
    }

    public void SetGPLeds(bool GPL1, bool GPL2)
    {
      if (IsConnected)
      {
        USBPacket_UI_UPDATE pkt = new USBPacket_UI_UPDATE
        {
          CommandCode = USBPacketCommandCode.CMD_UI_UPDATE // Make sure the command code is right
        };
        // Set GPL 1
        if (GPL1)
          pkt.GPL1 = 1;
        else
          pkt.GPL1 = 0;
        // Set GPL 2
        if (GPL2)
          pkt.GPL2 = 1;
        else
          pkt.GPL2 = 0;

        USB_TxBuffer.Add(PacketToByteArray(pkt));
      }
    }

    #endregion Gearbox control


  }
}