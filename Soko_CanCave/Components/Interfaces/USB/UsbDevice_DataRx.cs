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

    private static UInt32 mLastTimestamp = 0;

    public UInt32 timestampTmp = 0;
    public string msgTmp = "";
    public string shortMsg = "";
    public string longMsg = "";
    public string longMsgU1 = "";

    // List holding the currently being received frame
    // After it is confirmed to be correct it will be moved to _lastCorrectFrame
    private List<byte> _frameTemp = new List<byte>();

    // List holding the last correctly received frame
    private List<byte> _lastCorrectFrame = new List<byte>();

    private List<List<string>> primaryFrames = new List<List<string>>();
    private List<List<string>> secondaryFrames = new List<List<string>>();

    private List<string> primaryFrame = new List<string>();
    private List<string> secondaryFrame = new List<string>();

    // Timestamp of the last received message. It is used to determine frames and keeping track of time
    private UInt32 lastMessageTimestamp = 0;

    public delegate void UsbCanDataReceivedHandler(CanMessage msg);
    public event UsbCanDataReceivedHandler UsbCanDataReceived;

    private void OnRxEndPointData(object sender, EndpointDataEventArgs e)
    {
      try
      {
        // This is an unusual and unexpected situation, log it
        if (e.Count > 64)
        {
          EventLogger.DeviceError("RxdThd: Read more than 64 bytes !!");
        }

        // Try to process the packet properly
        USBPacketCommandCode rcvdCmd = (USBPacketCommandCode)e.Buffer[0];
        switch (rcvdCmd)
        {
          case USBPacketCommandCode.CMD_GET_DEV_INFO:
            {
              mDeviceInfo = ByteArrayToPacket<USBPacket_DeviceInfo>(e.Buffer);
              string fwStr = "FW" + mDeviceInfo.FirmwareVersion_Major.ToString() + "." + mDeviceInfo.FirmwareVersion_Minor.ToString() + "." + mDeviceInfo.FirmwareVersion_Build.ToString() + " ";
              string hwStr = "HW" + mDeviceInfo.HardwareVersion_Major.ToString() + "." + mDeviceInfo.HardwareVersion_Minor.ToString() + "." + mDeviceInfo.HardwareVersion_Iteration.ToString() + " ";
              string srlStr = "Serial: " + new string(mDeviceInfo.SerialString) + " ";
              EventLogger.DeviceDebug("RxThd: Device info rcvd: " + fwStr + hwStr + srlStr);
              _connectAsyncDeviceVersionReceived.Set();
              break;
            }
          case USBPacketCommandCode.CMD_GET_DEV_STATUS:
            {
              mDeviceStatus = ByteArrayToPacket<USBPacket_DeviceStatus>(e.Buffer);
              mDeviceMaxPacketsToBeSentUntilNewStatus = (int)(mDeviceInfo.KLINE_TxBuffSize - mDeviceStatus.KLINE_TxBuffFill - 20);
              statpack = "RXB: " + mDeviceStatus.KLINE_RxBuffFill.ToString() + " TXB: " + mDeviceStatus.KLINE_TxBuffFill.ToString();

              mDeviceStatusLastRequestReplied = true;
              _connectAsyncDeviceStatusReceived.Set();
              break;
            }
          case USBPacketCommandCode.CMD_USB_COMM_START:
            {
              _connectAsyncCommStartCmdReceived.Set();
              break;

            }
          case USBPacketCommandCode.CMD_CAN_DATA:
            {
              USBPacket_CANDataPacket temp = ByteArrayToPacket<USBPacket_CANDataPacket>(e.Buffer);
              string tmp;
              for (int i = 0; i < temp.msgCount; i++)
              {
                CanMessage msg = CAN_CVT_UsbDataToCanMsg(temp.msgs[i]);
                UsbCanDataReceived?.Invoke(msg);
                //msg.PrintMsg();
              }
              break;
            }
          case USBPacketCommandCode.CMD_KLINE_RXDATA:
            {
              USBPacket_INPUT_DATA_KLINE temp = ByteArrayToPacket<USBPacket_INPUT_DATA_KLINE>(e.Buffer);
              bool dtldTsPrint = false;
              int errflag = temp.data.U2STA & 0x000C;
              errflag = errflag >> 2;

              if ((temp.data.U2STA >> 8) == 0xFF)
              {
                // If this is UART2 data
                UInt32 delay = temp.data.timestamp - mLastTimestamp;
                if (delay > 300)
                {
                  // It means this is the new message, so
                  if (_frameTemp.Count == 154 || _frameTemp.Count == 142)
                  {
                    // It's assumed that if the frame contains proper amount of bytes it is treated as correct
                    _lastCorrectFrame = _frameTemp;
                  }
                  _frameTemp = new List<byte>();

                  //Console.WriteLine(longMsg);
                  //Console.WriteLine(longMsgU1);
                  primaryFrames.Add(primaryFrame);
                  secondaryFrames.Add(secondaryFrame);

                  longMsg = temp.data.timestamp.ToString("0000000000");
                  longMsgU1 = temp.data.timestamp.ToString("          ");
                  primaryFrame = new List<string>();
                  secondaryFrame = new List<string>();
                  
                  UInt32 msgDelta = temp.data.timestamp - lastMessageTimestamp;
                  lastMessageTimestamp = temp.data.timestamp;
                  longMsg += " " + msgDelta.ToString("000000") + " ";
                  longMsgU1 += "         ";
                  primaryFrame.Add(msgDelta.ToString("000000"));
                  secondaryFrame.Add(msgDelta.ToString("000000"));

                }
                //Console.WriteLine(Convert.ToString((byte)temp.data.U2STA, 2));
                mLastTimestamp = temp.data.timestamp;

                // Add the received byte to the frame temp
                _frameTemp.Add((byte)temp.data.data);

                UInt32 byteDelta = temp.data.timestamp - lastMessageTimestamp;
                //if (dtldTsPrint)
                {
                  //longMsg += errflag.ToString("0") + addren.ToString("0") + "ea ";
                  //longMsg += byteDelta.ToString("0000") + " ";
                }
                //longMsg += temp.data.data.ToString("X4") + " ";
                // Normal print
                primaryFrame.Add(byteDelta.ToString("0000") + " " + ((temp.data.data > 0xFF) ? "A" : " ") + (temp.data.data & 0xFF).ToString("X2"));
                longMsg += (temp.data.data & 0xFF).ToString("X2") + " ";
                //Full timestamp print
                //primaryFrame.Add(temp.data.timestamp.ToString("0000000000") + " " + ((temp.data.data > 0xFF) ? "A" : " ") + (temp.data.data & 0xFF).ToString("X2"));
              }


              if ((temp.data.U2STA >> 8) == 0x00)
              {
                UInt32 byteDelta = temp.data.timestamp - lastMessageTimestamp;
                if (dtldTsPrint)
                {
                  //longMsgU1 += byteDelta.ToString("0000") + " ";
                }
                longMsgU1 += temp.data.data.ToString("X2") + " ";

                // Full print
                secondaryFrame.Add(byteDelta.ToString("0000") + " " + ((temp.data.data > 0xFF) ? "A" : " ") + (temp.data.data & 0xFF).ToString("X2"));
                // Only data print
                //secondaryFrame.Add(((temp.data.data > 0xFF) ? "A" : " ") + (temp.data.data & 0xFF).ToString("X2"));
              }




              //Console.WriteLine(temp.data.timestamp.ToString("0000.0"));
              //        Console.WriteLine(delta.ToString("000000000000.0" + " " + temp.data.timestamp.ToString("0000000000.0")));
              //Console.WriteLine( temp.data.data.ToString("X2"));
              //Console.WriteLine(Convert.ToString((byte)temp.data[z].U2STA,2));
              //Console.WriteLine(Convert.ToString(temp.data[z].U2STA, 2));
              //Console.WriteLine(temp.data[z].U2STA.ToString("X1"));

              //Console.WriteLine(mDeviceStatus.KLINE_RxBuffFill.ToString("00000") + " " + temp.data[0].timestamp.ToString());
              //Console.WriteLine(temp.DataCount.ToString());

              break;
            }
        }
      }
      catch (Exception ex)
      {
        EventLogger.DeviceError("RxdThd: Exception occured: " + ex.Message);
      }
    }

  }
}