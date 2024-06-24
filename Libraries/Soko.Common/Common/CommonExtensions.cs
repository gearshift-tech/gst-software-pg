using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Soko.Common.Common
{
  public static class CommonExtensions
  {
    public static int Clamp(int value, int min, int max)
    {
      return (value < min) ? min : (value > max) ? max : value;
    }

    public static float Clamp(float value, float min, float max)
    {
      return (value < min) ? min : (value > max) ? max : value;
    }

    public static double Clamp(double value, double min, double max)
    {
      return (value < min) ? min : (value > max) ? max : value;
    }
  }
}
