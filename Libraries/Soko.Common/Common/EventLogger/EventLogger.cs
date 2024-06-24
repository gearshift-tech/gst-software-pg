using System;
using System.Collections.Generic;
using System.Text;

namespace Bluereach.Components.Common
{
  public static class EventLogger
  {
    public static void DeviceDebug(string msg)
    {
      Console.WriteLine(msg);
    }
    public static void DeviceInfo(string msg)
    {
      Console.WriteLine(msg);
    }
    public static void DeviceError(string msg)
    {
      Console.WriteLine(msg);
    }
  }
}
