using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Soko.Common.Common
{
  public static class GraphicsExtensions
  {
    public static void DrawCircle(Graphics g, Pen pen,
                                  float centerX, float centerY, float radius)
    {
      g.DrawEllipse(pen, centerX - radius, centerY - radius,
                    radius + radius, radius + radius);
    }

    public static void FillCircle(Graphics g, Brush brush,
                                  float centerX, float centerY, float radius)
    {
      g.FillEllipse(brush, centerX - radius, centerY - radius,
                    radius + radius, radius + radius);
    }
  }
}
