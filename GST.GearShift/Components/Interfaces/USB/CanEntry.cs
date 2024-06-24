using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GST.Gearshift.Components.Interfaces.USB//GST.Gearshift.Components.Interfaces.USB
{
  [Serializable]
  public class CanEntry
  {
    public string EntryName = "";

    public UInt32 MsgID = 0x0;

    public UInt32 LsbIndex = 0x0;
    public Int32 MsbIndex = 0x0;

    public double ValueMin = 0x0;
    public double ValueMax = 0x100;
    public UInt32 ValuePreOffset = 0x0;
    public double ValueMultiplier = 0x1;
    public double ValuePostOffset = 0x0;
  }
}
