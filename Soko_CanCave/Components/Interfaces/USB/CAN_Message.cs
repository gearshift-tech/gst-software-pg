using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Bluereach.CANPRO.Components.Interfaces.CANPROUSB
{
  /// <summary>
  /// CAN message in a managed format
  /// </summary>
  public class CanMessage
  {
    /// <summary>
    /// Message ID is 11bits for standard frame or 29 bits for extended frame
    /// </summary>
    public UInt32 remoteID;

    /// <summary>
    /// Marks if the message is extended ID frame or standard frame
    /// </summary>
    public bool isXtdFrameType;

    /// <summary>
    /// Marks if the message is Remote Transmission Request,
    /// Frame of this type carries no data
    /// </summary>
    public bool isRTRFrame;

    /// <summary>
    /// Number of data bytes in this message
    /// </summary>
    public Byte DLC;

    /// <summary>
    /// Message buffer with data bytes
    /// </summary>
    public byte[] data = new byte[8];

    /// <summary>
    /// Timestamp of this message
    /// </summary>
    public UInt32 timestamp;

    public CanMessage()
    {
    }

    public CanMessage(UsbCANData src)
    {
      LoadUsbCanData(src);
    }

    public void PrintMsg()
    {
      string tmp;
      if (remoteID == 0x7E2)
      {
        tmp = Convert.ToString(remoteID, 16).PadLeft(8, ' ') + "  ";
        tmp += DLC.ToString() + "  ";
        for (int i = 0; i < 8; i++)
        {
          tmp += Convert.ToString(data[i], 16).PadLeft(2, '0') + " ";
        }
        Console.WriteLine(tmp);
      }

      if (remoteID == 0x7EA)
      {
        tmp = "   " + Convert.ToString(remoteID, 16).PadLeft(8, ' ') + "  ";
        tmp += DLC.ToString() + "  ";
        for (int i = 0; i < 8; i++)
        {
          tmp += Convert.ToString(data[i], 16).PadLeft(2, '0') + " ";
        }
        Console.WriteLine(tmp);
      }
    }

    public UsbCANData ToUsbCanData()
    {
      // For reference on how the buffer is layed out read DS70353C page 29
      UsbCANData tmp = new UsbCANData
      {
        timestamp = timestamp,
        // SID, SRR, IDE
        EcanBufferContent0 = (UInt16)(remoteID << 2)
      };
      tmp.EcanBufferContent0 &= 0x1FFF;
      if (isRTRFrame && isXtdFrameType)
        tmp.EcanBufferContent0 += 2;
      if (isXtdFrameType)
        tmp.EcanBufferContent0 += 1;
      // EIDH
      tmp.EcanBufferContent1 = (UInt16)(remoteID >> 17);
      tmp.EcanBufferContent1 &= 0x0FFF;
      // EIDL, RTR, DLC
      tmp.EcanBufferContent2 = (UInt16)(remoteID >> 1);
      tmp.EcanBufferContent2 &= 0xFC00;
      if (isRTRFrame && !isXtdFrameType)
        tmp.EcanBufferContent2 += 1 << 9;
      tmp.EcanBufferContent2 |= (UInt16)(DLC & 0x0F);
      // Data 0-7
      tmp.EcanBufferContent3 = (UInt16)((data[1] << 8) + data[0]);
      tmp.EcanBufferContent4 = (UInt16)((data[3] << 8) + data[2]);
      tmp.EcanBufferContent5 = (UInt16)((data[5] << 8) + data[4]);
      tmp.EcanBufferContent6 = (UInt16)((data[7] << 8) + data[6]);
      return tmp;
    }

    public void LoadUsbCanData(UsbCANData src)
    {
      //string tmp;
      //tmp = Convert.ToString(src.EcanBufferContent0, 2);
      //Console.WriteLine(tmp.PadLeft(16, '0'));  


      // For reference on how the buffer is layed out read DS70353C page 29
      this.timestamp = src.timestamp;
      // SID, SRR, IDE

      isXtdFrameType = ((src.EcanBufferContent0 & 0x0001) > 0); // IDE bit
      if (!isXtdFrameType && ((src.EcanBufferContent0 & 0x0002) > 0)) // SSR bit
        isRTRFrame = true;
      if (isXtdFrameType && ((src.EcanBufferContent2 & 0x0200) > 0)) // RTR bit
        isRTRFrame = true;

      UInt32 SID = (UInt32)((src.EcanBufferContent0 & 0x1FFF) >> 2);
      UInt32 EID = (UInt32)((src.EcanBufferContent1 & 0x0FFF) << 6); // 6 more bits in word 2
      EID |= (UInt32)((src.EcanBufferContent2 & 0xFC00) >> 10);

      if (isXtdFrameType)
        remoteID = (SID << 18) | EID;
      else
        remoteID = SID;

      DLC = (byte)(src.EcanBufferContent2 & 0x000F);

      data[0] = (byte)(src.EcanBufferContent3 & 0x00FF);
      data[1] = (byte)(src.EcanBufferContent3 >> 8);
      data[2] = (byte)(src.EcanBufferContent4 & 0x00FF);
      data[3] = (byte)(src.EcanBufferContent4 >> 8);
      data[4] = (byte)(src.EcanBufferContent5 & 0x00FF);
      data[5] = (byte)(src.EcanBufferContent5 >> 8);
      data[6] = (byte)(src.EcanBufferContent6 & 0x00FF);
      data[7] = (byte)(src.EcanBufferContent6 >> 8);
      //if (isRTRFrame && isXtdFrameType)
      //  tmp.EcanBufferContent0 += 2;
      //if (isXtdFrameType)
      //  tmp.EcanBufferContent0 += 1;
      //// EIDH
      //tmp.EcanBufferContent1 = (UInt16)(remoteID >> 17);
      //tmp.EcanBufferContent1 &= 0x0FFF;
      //// EIDL, RTR, DLC
      //tmp.EcanBufferContent2 = (UInt16)(remoteID >> 1);
      //tmp.EcanBufferContent2 &= 0xFC00;
      //if (isRTRFrame && !isXtdFrameType)
      //  tmp.EcanBufferContent2 += 1 << 9;
      //tmp.EcanBufferContent2 |= (UInt16)(DLC & 0x0F);
      //// Data 0-7
      //tmp.EcanBufferContent3 = (UInt16)((data[1] << 8) + data[0]);
      //tmp.EcanBufferContent4 = (UInt16)((data[3] << 8) + data[2]);
      //tmp.EcanBufferContent5 = (UInt16)((data[5] << 8) + data[4]);
      //tmp.EcanBufferContent6 = (UInt16)((data[7] << 8) + data[6]);
      //return tmp;
    }
  }


  /// <summary>
  /// USB CAN data
  /// First 4 bytes are a timestamp
  /// Following bytes are a proper ECAN message buffer content (14 bytes)
  /// This reduces embedded CPU load
  /// </summary>
  [StructLayout(LayoutKind.Sequential, Size = 18, Pack = 1)]
  public struct UsbCANData
  {
    [MarshalAs(UnmanagedType.U4)]
    public UInt32 timestamp;

    //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
    //public UInt16[] EcanBufferContent;
    public UInt16 EcanBufferContent0;
    public UInt16 EcanBufferContent1;
    public UInt16 EcanBufferContent2;
    public UInt16 EcanBufferContent3;
    public UInt16 EcanBufferContent4;
    public UInt16 EcanBufferContent5;
    public UInt16 EcanBufferContent6;
  }



}
