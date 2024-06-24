using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Soko.Common.Interfaces
{
  /// <summary>
  /// CAN message in a managed format
  /// </summary>
  public class CanMessage
  {
    /// <summary>
    /// Message ID is 11bits for standard frame or 29 bits for extended frame
    /// </summary>
    public UInt32 remoteID = 0;

    /// <summary>
    /// Marks if the message is extended ID frame or standard frame
    /// </summary>
    public bool isXtdFrameType = false;

    /// <summary>
    /// Marks if the message is Remote Transmission Request,
    /// Frame of this type carries no data
    /// </summary>
    public bool isRTRFrame = false;

    /// <summary>
    /// Number of data bytes in this message
    /// </summary>
    public Byte DLC = 0;

    /// <summary>
    /// Message buffer with data bytes
    /// </summary>
    public byte[] data = new byte[8];

    /// <summary>
    /// Timestamp of this message
    /// </summary>
    public UInt32 timestamp = 0;

    /// <summary>
    /// Number of source/destionation CAN interface (1 / 2)
    /// </summary>
    public int physicalInterfaceNumber = 1;

    /// <summary>
    /// If the message was forwarded to another interface via hardware gateway
    /// </summary>
    public bool messageWasForwarded = false;

    public CanMessage()
    {
    }

    public override string ToString()
    {
      string tmp = isXtdFrameType ? "X" : " ";
      tmp += isRTRFrame ? "R" : " ";
      tmp += Convert.ToString(remoteID, 16).PadLeft(8, ' ') + "  ";
      tmp += DLC.ToString() + "  ";
      for (int i = 0; i < DLC; i++)
      {
        tmp += Convert.ToString(data[i], 16).PadLeft(2, '0') + " ";
      }
      return tmp;
    }


  }






}
