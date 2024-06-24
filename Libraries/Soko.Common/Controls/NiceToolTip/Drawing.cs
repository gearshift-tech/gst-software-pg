using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Soko.Common.Controls.NiceTooltip
{
  public class Drawing
  {
    #region "Enumerations"

    /// <summary>
    /// Color theme used for rendering objects.
    /// </summary>
    public enum ColorTheme
    {
      Blue = 0,
      BlackBlue = 1
    }
    /// <summary>
    /// Enumeration used to determine contents of a given tooltip parameter.
    /// </summary>
    /// <remarks></remarks>
    public enum ToolTipContent
    {
      TitleOnly,
      TitleAndText,
      TitleAndImage,
      All,
      ImageOnly,
      ImageAndText,
      TextOnly,
      Empty
    }
    /// <summary>
    /// Enumeration used to determine starting point of glowing light.
    /// </summary>
    /// <remarks><seelaso cref="getGlowingPath"/></remarks>
    public enum LightingGlowPoint
    {
      TopLeft,
      TopCenter,
      TopRight,
      MiddleLeft,
      MiddleCenter,
      MiddleRight,
      BottomLeft,
      BottomCenter,
      BottomRight,
      Custom
    }
    /// <summary>
    /// Enumeration used to determine the shadow location.
    /// </summary>
    /// <remarks><seealso cref="getInnerShadowPath"/></remarks>
    public enum ShadowPoint
    {
      Top,
      TopLeft,
      TopRight,
      Left,
      Right,
      Bottom,
      BottomLeft,
      BottomRight
    }
    /// <summary>
    /// Enumeration used to determine the direction of a triangle.
    /// </summary>
    /// <remarks><seealso cref="drawTriangle"/></remarks>
    public enum TriangleDirection
    {
      Up,
      Left,
      Right,
      Down,
      UpLeft,
      UpRight,
      DownLeft,
      DownRight
    }
    public enum GripMode
    {
      Left,
      Right
    }
    #endregion
    #region "Drawing Path"
    /// <summary>
    /// Create a rounded corner rectangle.
    /// </summary>
    /// <param name="rect">The rectangle to be rounded.</param>
    /// <param name="topLeft">Range of the top left corner from the rectangle to be rounded.</param>
    /// <param name="topRight">Range of the top right corner from the rectangle to be rounded.</param>
    /// <param name="bottomLeft">Range of the bottom left corner from the rectangle to be rounded.</param>
    /// <param name="bottomRight">Range of the bottom right corner from the rectangle to be rounded.</param>
    /// <returns>A GraphicsPath object that represent a rectangle that have its corners rounded.</returns>
    /// <remarks>The <c>range</c> must be greater than or equal with zero, and must be less then or equal with a half of its rectangle's width or height.
    /// If the <c>range</c> value less than zero, then its return the rect parameter.
    /// If rectangle width greater than its height, then maximum value of <c>range</c> is a half of rectangle height.
    /// There are optionally rounded on its four corner.</remarks>
    public static GraphicsPath roundedRectangle(Rectangle rect, int topLeft = 0, int topRight = 0, int bottomLeft = 0, int bottomRight = 0)
    {
      GraphicsPath result = new GraphicsPath();
      if (rect.Width > 0 & rect.Height > 0)
      {
        int maxAllowed = 0;
        if (rect.Height < rect.Width)
        {
          maxAllowed = (int)Math.Floor(rect.Height / 2.0f);
        }
        else
        {
          maxAllowed = (int)Math.Floor(rect.Width / 2.0f);
        }
        PointF startPoint = default(PointF);
        PointF endPoint = default(PointF);
        var _with1 = rect;
        if (topLeft > 0 & topLeft < maxAllowed)
        {
          result.AddArc(_with1.X, _with1.Y, topLeft * 2, topLeft * 2, 180, 90);
          startPoint = new PointF(_with1.X + topLeft, _with1.Y);
          endPoint = new PointF(_with1.X, _with1.Y + topLeft);
        }
        else
        {
          startPoint = new PointF(_with1.X, _with1.Y);
          endPoint = new PointF(_with1.X, _with1.Y);
        }
        if (topRight > 0 & topRight < maxAllowed)
        {
          result.AddLine(startPoint.X, startPoint.Y, _with1.Right - (topRight + 1), _with1.Y);
          result.AddArc(_with1.Right - ((topRight * 2) + 1), _with1.Y, topRight * 2, topRight * 2, 270, 90);
          startPoint = new PointF(_with1.Right - 1, _with1.Y + topRight);
        }
        else
        {
          result.AddLine(startPoint.X, startPoint.Y, _with1.Right - 1, _with1.Y);
          startPoint = new PointF(_with1.Right - 1, _with1.Y);
        }
        if (bottomRight > 0 & bottomRight < maxAllowed)
        {
          result.AddLine(startPoint.X, startPoint.Y, startPoint.X, _with1.Bottom - (bottomRight + 1));
          result.AddArc(_with1.Right - ((bottomRight * 2) + 1), _with1.Bottom - ((bottomRight * 2) + 1), bottomRight * 2, bottomRight * 2, 0, 90);
          startPoint = new PointF(_with1.Right - (bottomRight + 1), _with1.Bottom - 1);
        }
        else
        {
          result.AddLine(startPoint.X, startPoint.Y, startPoint.X, _with1.Bottom - 1);
          startPoint = new PointF(_with1.Right - 1, _with1.Bottom - 1);
        }
        if (bottomLeft > 0 & bottomLeft < maxAllowed)
        {
          result.AddLine(startPoint.X, startPoint.Y, _with1.X + bottomLeft, startPoint.Y);
          result.AddArc(_with1.X, _with1.Bottom - ((bottomLeft * 2) + 1), bottomLeft * 2, bottomLeft * 2, 90, 90);
          startPoint = new PointF(_with1.X, _with1.Bottom - (bottomLeft + 1));
        }
        else
        {
          result.AddLine(startPoint.X, startPoint.Y, _with1.X, startPoint.Y);
          startPoint = new PointF(_with1.X, startPoint.Y);
        }
        result.AddLine(startPoint, endPoint);
        result.CloseFigure();
        return result;
      }
      // Return the rect param.
      result.AddRectangle(rect);
      return result;
    }
    /// <summary>
    /// Create a lighting glow path from a rectangle.
    /// </summary>
    /// <returns>A GraphicsPath object that represent a lighting glow.</returns>
    /// <param name="rect">The rectangle where lighting glow path to be created.</param>
    /// <param name="glowPoint">One of <see cref="LightingGlowPoint">LightingGlowPoint</see> enumeration value.  Determine where the light starts.</param>
    /// <param name="percentWidth">Percentage of rectangle's width used to create the path.</param>
    /// <param name="percentHeight">Percentage of rectangle's height used to create the path.</param>
    /// <param name="customX">X location where the light starts.  Used when glowPoint value is LightingGlowPoint.Custom.</param>
    /// <param name="customY">Y location where the light starts.  Used when glowPoint value is LightingGlowPoint.Custom.</param>
    public static GraphicsPath getGlowingPath(Rectangle rect, LightingGlowPoint glowPoint = LightingGlowPoint.BottomCenter, int percentWidth = 100, int percentHeight = 100, int customX = 0, int customY = 0)
    {
      Rectangle arcRect = default(Rectangle);
      GraphicsPath ePath = new GraphicsPath();
      switch (glowPoint)
      {
        case LightingGlowPoint.TopLeft:
          arcRect = new Rectangle(rect.X - (rect.Width * percentWidth / 100), rect.Y - (rect.Height * percentHeight / 100), rect.Width * percentWidth * 2 / 100, rect.Height * percentHeight * 2 / 100);
          ePath.AddLine(rect.X, rect.Y, Convert.ToInt32(rect.X + (rect.Width * percentWidth / 100)), rect.Y);
          ePath.AddArc(arcRect, 0, 90);
          ePath.AddLine(rect.X, rect.Y + Convert.ToInt32(rect.Height * percentHeight / 100), rect.X, rect.Y);
          break;
        case LightingGlowPoint.TopCenter:
          arcRect = new Rectangle((rect.X + (rect.Width / 2)) - (rect.Width * percentWidth / 200), rect.Y - (rect.Height * percentHeight / 100), rect.Width * percentWidth / 100, rect.Height * percentHeight * 2 / 100);
          ePath.AddLine(rect.X + Convert.ToInt32(rect.Width * (100 - percentWidth) / 200), rect.Y, rect.Right - Convert.ToInt32(rect.Width * (100 - percentWidth) / 200), rect.Y);
          ePath.AddArc(arcRect, 0, 180);
          break;
        case LightingGlowPoint.TopRight:
          arcRect = new Rectangle(rect.Right - (rect.Width * percentWidth / 100), rect.Y - (rect.Height * percentHeight / 100), rect.Width * percentWidth * 2 / 100, rect.Height * percentHeight * 2 / 100);
          ePath.AddLine(rect.Right - Convert.ToInt32(rect.Width * percentWidth / 100), rect.Y, rect.Right, rect.Y);
          ePath.AddLine(rect.Right, rect.Y, rect.Right, rect.Y + Convert.ToInt32(rect.Height * percentHeight / 100));
          ePath.AddArc(arcRect, 90, 90);
          break;
        case LightingGlowPoint.MiddleLeft:
          arcRect = new Rectangle(rect.X - (rect.Width * percentWidth / 100), (rect.Y + (rect.Height / 2)) - (rect.Height * percentHeight / 200), rect.Width * percentWidth * 2 / 100, rect.Height * percentHeight / 100);
          ePath.AddArc(arcRect, 270, 180);
          ePath.AddLine(rect.X, rect.Bottom - Convert.ToInt32(rect.Height * (100 - percentHeight) / 200), rect.X, rect.Y + Convert.ToInt32(rect.Height * (100 - percentHeight) / 200));
          break;
        case LightingGlowPoint.MiddleCenter:
          arcRect = new Rectangle((rect.X + (rect.Width / 2)) - (rect.Width * percentWidth / 200), (rect.Y + (rect.Height / 2)) - (rect.Height * percentHeight / 200), rect.Width * percentWidth / 100, rect.Height * percentHeight / 100);
          ePath.AddEllipse(arcRect);
          break;
        case LightingGlowPoint.MiddleRight:
          arcRect = new Rectangle(rect.Right - (rect.Width * percentWidth / 100), (rect.Y + (rect.Height / 2)) - (rect.Height * percentHeight / 200), rect.Width * percentWidth * 2 / 100, rect.Height * percentHeight / 100);
          ePath.AddLine(rect.Right, rect.Bottom - Convert.ToInt32(rect.Height * (100 - percentHeight) / 200), rect.Right, rect.Y + Convert.ToInt32(rect.Height * (100 - percentHeight) / 200));
          ePath.AddArc(arcRect, 90, 180);
          break;
        case LightingGlowPoint.BottomLeft:
          arcRect = new Rectangle(rect.X - (rect.Width * percentWidth / 100), rect.Bottom - (rect.Height * percentHeight / 100), rect.Width * percentWidth * 2 / 100, rect.Height * percentHeight * 2 / 100);
          ePath.AddArc(arcRect, 270, 90);
          ePath.AddLine(Convert.ToInt32(rect.X + (rect.Width * percentWidth / 100)), rect.Bottom, rect.X, rect.Bottom);
          ePath.AddLine(rect.X, rect.Bottom, rect.X, rect.Bottom - Convert.ToInt32(rect.Height * percentHeight / 100));
          break;
        case LightingGlowPoint.BottomCenter:
          arcRect = new Rectangle((rect.X + (rect.Width / 2)) - (rect.Width * percentWidth / 200), rect.Bottom - (rect.Height * percentHeight / 100), rect.Width * percentWidth / 100, rect.Height * percentHeight * 2 / 100);
          ePath.AddArc(arcRect, 180, 180);
          ePath.AddLine(rect.X + Convert.ToInt32(rect.Width * (100 - percentWidth) / 200), rect.Bottom, rect.Right - Convert.ToInt32(rect.Width * (100 - percentWidth) / 200), rect.Bottom);
          break;
        case LightingGlowPoint.BottomRight:
          arcRect = new Rectangle(rect.Right - (rect.Width * percentWidth / 100), rect.Bottom - (rect.Height * percentHeight / 100), rect.Width * percentWidth * 2 / 100, rect.Height * percentHeight * 2 / 100);
          ePath.AddArc(arcRect, 180, 90);
          ePath.AddLine(rect.Right, rect.Bottom - Convert.ToInt32(rect.Height * percentHeight / 100), rect.Right, rect.Bottom);
          ePath.AddLine(rect.Right, rect.Bottom, rect.Right - Convert.ToInt32(rect.Width * percentWidth / 100), rect.Bottom);
          break;
        case LightingGlowPoint.Custom:
          arcRect = new Rectangle((rect.X + customX) - (rect.Width * percentWidth / 200), (rect.Y + customY) - (rect.Height * percentHeight / 200), rect.Width * percentWidth / 100, rect.Height * percentHeight / 100);
          ePath.AddEllipse(arcRect);
          break;
      }
      ePath.CloseFigure();
      return ePath;
    }
    /// <summary>
    /// Create a GraphicsPath object represent an inner shadow of a rectangle.
    /// </summary>
    /// <returns>A GraphicsPath object that represent an inner shadow.</returns>
    /// <param name="rect">The rectangle where shadow path to be created.</param>
    /// <param name="shadow">One of <see cref="ShadowPoint">ShadowPoint</see> enumeration value.  Determine the place of the shadow inside the rectangle.</param>
    /// <param name="verticalRange">Shadow height, calculated from top or bottom of the rectange.</param>
    /// <param name="horizontalRange">Shadow width, calculated from left or right of the rectangle.</param>
    /// <param name="topLeft">Rounded range of the rectangle's top left corner.</param>
    /// <param name="topRight">Rounded range of the rectangle's top right corner.</param>
    /// <param name="bottomLeft">Rounded range of the rectangle's bottom left corner.</param>
    /// <param name="bottomRight">Rounded range of the rectangle's bottom right corner.</param>
    /// <remarks><seealso cref="ShadowPoint"/></remarks>
    public static GraphicsPath getInnerShadowPath(Rectangle rect, ShadowPoint shadow = ShadowPoint.Top, int verticalRange = 2, int horizontalRange = 2, int topLeft = 0, int topRight = 0, int bottomLeft = 0, int bottomRight = 0)
    {
      GraphicsPath result = new GraphicsPath();
      if (rect.Width > 0 & rect.Height > 0)
      {
        int maxAllowed = 0;
        if (rect.Height < rect.Width)
        {
          maxAllowed = (int)Math.Floor(rect.Height / 2.0f);
        }
        else
        {
          maxAllowed = (int)Math.Floor(rect.Width / 2.0f);
        }
        if (verticalRange < (int)Math.Floor(rect.Height / 2.0f) & horizontalRange < (int)Math.Floor(rect.Width / 2.0f))
        {
          // Building shadow
          var _with2 = rect;
          switch (shadow)
          {
            case ShadowPoint.Top:
            case ShadowPoint.TopLeft:
            case ShadowPoint.TopRight:
              {
                // Shadow from top
                PointF startPoint = default(PointF);
                PointF endPoint = default(PointF);
                if (topLeft > 0 & topLeft < maxAllowed)
                {
                  result.AddArc(_with2.X, _with2.Y, topLeft * 2, topLeft * 2, 180, 90);
                  startPoint = new PointF(_with2.X + topLeft, _with2.Y);
                  endPoint = new PointF(_with2.X, _with2.Y + topLeft);
                }
                else
                {
                  startPoint = new PointF(_with2.X, _with2.Y);
                  endPoint = new PointF(_with2.X, _with2.Y);
                }
                if (topRight > 0 & topRight < maxAllowed)
                {
                  result.AddLine(startPoint.X, startPoint.Y, _with2.Right - (topRight + 1), _with2.Y);
                  result.AddArc(_with2.Right - ((topRight * 2) + 1), _with2.Y, topRight * 2, topRight * 2, 270, 90);
                  startPoint = new PointF(_with2.Right - 1, _with2.Y + topRight);
                }
                else
                {
                  result.AddLine(startPoint.X, startPoint.Y, _with2.Right - 1, _with2.Y);
                  startPoint = new PointF(_with2.Right - 1, _with2.Y);
                }
                if (shadow == ShadowPoint.TopRight)
                {
                  if (bottomRight > 0 & bottomRight < maxAllowed)
                  {
                    result.AddLine(startPoint.X, startPoint.Y, startPoint.X, _with2.Bottom - (bottomRight + 1));
                    result.AddArc(_with2.Right - ((bottomRight * 2) + 1), _with2.Bottom - ((bottomRight * 2) + 1), bottomRight * 2, bottomRight * 2, 0, 90);
                    startPoint = new PointF(_with2.Right - (bottomRight + 1), _with2.Bottom - 1);
                  }
                  else
                  {
                    result.AddLine(startPoint.X, startPoint.Y, startPoint.X, _with2.Bottom - 1);
                    startPoint = new PointF(startPoint.X, _with2.Bottom - 1);
                  }
                  result.AddLine(startPoint.X, startPoint.Y, startPoint.X - horizontalRange, startPoint.Y);
                  startPoint = new PointF(startPoint.X - horizontalRange, startPoint.Y);
                  if (bottomRight > 0 & bottomRight < maxAllowed)
                  {
                    result.AddArc(startPoint.X - bottomRight, _with2.Bottom - ((bottomRight * 2) + 1), bottomRight * 2, bottomRight * 2, 90, -90);
                    startPoint = new PointF(startPoint.X + bottomRight, _with2.Bottom - (bottomRight + 1));
                  }
                  if (topRight > 0 & topRight < maxAllowed)
                  {
                    result.AddLine(startPoint.X, startPoint.Y, startPoint.X, _with2.Y + topRight + verticalRange);
                    result.AddArc(_with2.Right - (horizontalRange + (topRight * 2) + 1), _with2.Y + verticalRange, topRight * 2, topRight * 2, 0, -90);
                    startPoint = new PointF(_with2.Right - (horizontalRange + topRight + 1), _with2.Y + verticalRange);
                  }
                  else
                  {
                    result.AddLine(startPoint.X, startPoint.Y, startPoint.X, _with2.Y + verticalRange);
                    startPoint = new PointF(_with2.Right - (horizontalRange + 1), _with2.Y + verticalRange);
                  }
                }
                else
                {
                  result.AddLine(startPoint.X, startPoint.Y, startPoint.X, startPoint.Y + verticalRange);
                  startPoint = new PointF(startPoint.X, startPoint.Y + verticalRange);
                  if (topRight > 0 & topRight < maxAllowed)
                  {
                    result.AddArc(_with2.Right - ((topRight * 2) + 1), startPoint.Y - topRight, topRight * 2, topRight * 2, 0, -90);
                    startPoint = new PointF(_with2.Right - 1, startPoint.Y - topRight);
                  }
                }
                if (shadow == ShadowPoint.TopLeft)
                {
                  if (topLeft > 0 & topLeft < maxAllowed)
                  {
                    result.AddLine(startPoint, new PointF(_with2.X + horizontalRange + topLeft, startPoint.Y));
                    result.AddArc(_with2.X + horizontalRange, _with2.Y + verticalRange, topLeft * 2, topLeft * 2, 270, -90);
                    startPoint = new PointF(_with2.X + horizontalRange, _with2.Y + verticalRange + topLeft);
                  }
                  else
                  {
                    result.AddLine(startPoint, new PointF(_with2.X + horizontalRange, startPoint.Y));
                    startPoint = new PointF(_with2.X + horizontalRange, _with2.Y + verticalRange);
                  }
                  if (bottomLeft > 0 & bottomLeft < maxAllowed)
                  {
                    result.AddLine(startPoint, new PointF(startPoint.X, _with2.Bottom - (bottomLeft + 1)));
                    result.AddArc(_with2.X + horizontalRange, _with2.Bottom - ((bottomLeft * 2) + 1), bottomLeft * 2, bottomLeft * 2, 180, -90);
                    result.AddLine(_with2.X + horizontalRange + bottomLeft, _with2.Bottom - 1, _with2.X + bottomLeft, _with2.Bottom - 1);
                    result.AddArc(_with2.X, _with2.Bottom - ((bottomLeft * 2) - 1), bottomLeft * 2, bottomLeft * 2, 90, 90);
                    startPoint = new PointF(_with2.X, _with2.Bottom - (bottomLeft + 1));
                  }
                  else
                  {
                    result.AddLine(startPoint, new PointF(startPoint.X, _with2.Bottom - 1));
                    result.AddLine(startPoint.X, _with2.Bottom - 1, _with2.X, _with2.Bottom - 1);
                    startPoint = new PointF(_with2.X, _with2.Bottom - 1);
                  }
                }
                else
                {
                  if (topLeft > 0 & topLeft < maxAllowed)
                  {
                    result.AddLine(startPoint.X, startPoint.Y, _with2.X + topLeft, startPoint.Y);
                    result.AddArc(_with2.X, startPoint.Y, topLeft * 2, topLeft * 2, 270, -90);
                    startPoint = new PointF(_with2.X, startPoint.Y + topLeft);
                  }
                  else
                  {
                    result.AddLine(startPoint.X, startPoint.Y, _with2.X, startPoint.Y);
                    startPoint = new PointF(_with2.X, startPoint.Y);
                  }
                }
                result.AddLine(startPoint, endPoint);
                result.CloseFigure();
                return result;
              }
            case ShadowPoint.Bottom:
            case ShadowPoint.BottomLeft:
            case ShadowPoint.BottomRight:
              {
                // Shadow from bottom
                PointF startPoint = default(PointF);
                PointF endPoint = default(PointF);
                if (bottomLeft > 0 & bottomLeft < maxAllowed)
                {
                  result.AddArc(_with2.X, _with2.Bottom - ((bottomLeft * 2) + 1), bottomLeft * 2, bottomLeft * 2, 180, -90);
                  startPoint = new PointF(_with2.X + bottomLeft, _with2.Bottom - 1);
                  endPoint = new PointF(_with2.X, _with2.Bottom - (bottomLeft + 1));
                }
                else
                {
                  startPoint = new PointF(_with2.X, _with2.Bottom - 1);
                  endPoint = new PointF(_with2.X, _with2.Bottom - 1);
                }
                if (bottomRight > 0 & bottomRight < maxAllowed)
                {
                  result.AddLine(startPoint, new PointF(_with2.Right - (bottomRight + 1), _with2.Bottom - 1));
                  result.AddArc(_with2.Right - ((bottomRight * 2) + 1), _with2.Bottom - ((bottomRight * 2) + 1), bottomRight * 2, bottomRight * 2, 90, -90);
                  startPoint = new PointF(_with2.Right - 1, _with2.Bottom - (bottomRight + 1));
                }
                else
                {
                  result.AddLine(startPoint, new PointF(_with2.Right - 1, _with2.Bottom - 1));
                  startPoint = new PointF(_with2.Right - 1, _with2.Bottom - 1);
                }
                if (shadow == ShadowPoint.BottomRight)
                {
                  if (topRight > 0 & topRight < maxAllowed)
                  {
                    result.AddLine(startPoint, new PointF(startPoint.X, _with2.Y + topRight + 1));
                    result.AddArc(_with2.Right - ((topRight * 2) + 1), _with2.Y, topRight * 2, topRight * 2, 0, -90);
                    startPoint = new PointF(_with2.Right - (topRight + 1), _with2.Y);
                  }
                  else
                  {
                    result.AddLine(startPoint, new PointF(_with2.Right - 1, _with2.Y));
                    startPoint = new PointF(_with2.Right - 1, _with2.Y);
                  }
                  result.AddLine(startPoint, new PointF(startPoint.X - horizontalRange, _with2.Y));
                  startPoint = new PointF(startPoint.X - horizontalRange, _with2.Y);
                  if (topRight > 0 & topRight < maxAllowed)
                  {
                    result.AddArc(startPoint.X - topRight, _with2.Y, topRight * 2, topRight * 2, 270, 90);
                    startPoint = new PointF(startPoint.X + topRight, _with2.Y + topRight);
                  }
                  if (bottomRight > 0 & bottomRight < maxAllowed)
                  {
                    result.AddLine(startPoint, new PointF(startPoint.X, _with2.Bottom - (bottomRight + verticalRange + 1)));
                    result.AddArc(_with2.Right - (horizontalRange + (bottomRight * 2) + 1), _with2.Bottom - (verticalRange + (bottomRight * 2) + 1), bottomRight * 2, bottomRight * 2, 0, 90);
                    startPoint = new PointF(_with2.Right - (horizontalRange + bottomRight + 1), _with2.Bottom - (verticalRange + 1));
                  }
                  else
                  {
                    result.AddLine(startPoint, new PointF(startPoint.X, _with2.Bottom - (verticalRange + 1)));
                    startPoint = new PointF(startPoint.X, _with2.Bottom - (verticalRange + 1));
                  }
                }
                else
                {
                  result.AddLine(startPoint, new PointF(startPoint.X, startPoint.Y - verticalRange));
                  startPoint = new PointF(startPoint.X, startPoint.Y - verticalRange);
                  if (bottomRight > 0 & bottomRight < maxAllowed)
                  {
                    result.AddArc(_with2.Right - ((bottomRight * 2) + 1), startPoint.Y - bottomRight, bottomRight * 2, bottomRight * 2, 0, 90);
                    startPoint = new PointF(_with2.Right - (bottomRight + 1), startPoint.Y + bottomRight);
                  }
                }
                if (shadow == ShadowPoint.BottomLeft)
                {
                  if (bottomLeft > 0 & bottomLeft < maxAllowed)
                  {
                    result.AddLine(startPoint, new PointF(_with2.X + horizontalRange + bottomLeft, startPoint.Y));
                    result.AddArc(_with2.X + horizontalRange, _with2.Bottom - (verticalRange + (bottomLeft * 2) + 1), bottomLeft * 2, bottomLeft * 2, 90, 90);
                    startPoint = new PointF(_with2.X + horizontalRange, _with2.Bottom - (verticalRange + bottomLeft + 1));
                  }
                  else
                  {
                    result.AddLine(startPoint, new PointF(_with2.X + horizontalRange, startPoint.Y));
                    startPoint = new PointF(_with2.X + horizontalRange, _with2.Bottom - (verticalRange + 1));
                  }
                  if (topLeft > 0 & topLeft < maxAllowed)
                  {
                    result.AddLine(startPoint, new PointF(startPoint.X, _with2.Y + topLeft));
                    result.AddArc(_with2.X + horizontalRange, _with2.Y, topLeft * 2, topLeft * 2, 180, 90);
                    result.AddLine(_with2.X + horizontalRange + topLeft, _with2.Y, _with2.X + topLeft, _with2.Y);
                    result.AddArc(_with2.X, _with2.Y, topLeft * 2, topLeft * 2, 270, -90);
                    startPoint = new PointF(_with2.X, _with2.Y + topLeft);
                  }
                  else
                  {
                    result.AddLine(startPoint, new PointF(startPoint.X, _with2.Y));
                    result.AddLine(startPoint.X, _with2.Y, _with2.X, _with2.Y);
                    startPoint = new PointF(_with2.X, _with2.Y);
                  }
                }
                else
                {
                  if (bottomLeft > 0 & bottomLeft < maxAllowed)
                  {
                    result.AddLine(startPoint, new PointF(_with2.X + bottomLeft, startPoint.Y));
                    result.AddArc(_with2.X, _with2.Bottom - (verticalRange + (bottomLeft * 2) + 1), bottomLeft * 2, bottomLeft * 2, 90, 90);
                    startPoint = new PointF(_with2.X, _with2.Bottom - (verticalRange + bottomLeft + 1));
                  }
                  else
                  {
                    result.AddLine(startPoint, new PointF(_with2.X, startPoint.Y));
                    startPoint = new PointF(_with2.X, startPoint.Y);
                  }
                }
                result.AddLine(startPoint, endPoint);
                result.CloseFigure();
                return result;
              }
            case ShadowPoint.Left:
              {
                // Left only shadow
                PointF startPoint = default(PointF);
                PointF endPoint = default(PointF);
                if (topLeft > 0 & topLeft < maxAllowed)
                {
                  endPoint = new PointF(_with2.X, _with2.Y + topLeft);
                  result.AddArc(_with2.X, _with2.Y, topLeft * 2, topLeft * 2, 180, 90);
                  result.AddLine(_with2.X + topLeft, _with2.Y, _with2.X + horizontalRange + topLeft, _with2.Y);
                  result.AddArc(_with2.X + horizontalRange, _with2.Y, topLeft * 2, topLeft * 2, 270, -90);
                  startPoint = new PointF(_with2.X + horizontalRange, _with2.Y + topLeft);
                }
                else
                {
                  endPoint = new PointF(_with2.X, _with2.Y);
                  result.AddLine(_with2.X, _with2.Y, _with2.X + horizontalRange, _with2.Y);
                  startPoint = new PointF(_with2.X + horizontalRange, _with2.Y);
                }
                if (bottomLeft > 0 & bottomLeft < maxAllowed)
                {
                  result.AddLine(startPoint, new PointF(_with2.X + horizontalRange, _with2.Bottom - (bottomLeft + 1)));
                  result.AddArc(_with2.X + horizontalRange, _with2.Bottom - ((bottomLeft * 2) + 1), bottomLeft * 2, bottomLeft * 2, 180, -90);
                  result.AddLine(_with2.X + horizontalRange + bottomLeft, _with2.Bottom - 1, _with2.X + bottomLeft, _with2.Bottom - 1);
                  result.AddArc(_with2.X, _with2.Bottom - ((bottomLeft * 2) + 1), bottomLeft * 2, bottomLeft * 2, 90, -90);
                  startPoint = new PointF(_with2.X, _with2.Bottom - (bottomLeft + 1));
                }
                else
                {
                  result.AddLine(startPoint, new PointF(_with2.X + horizontalRange, _with2.Bottom - 1));
                  result.AddLine(_with2.X + horizontalRange, _with2.Bottom - 1, _with2.X, _with2.Bottom - 1);
                  startPoint = new PointF(_with2.X, _with2.Bottom - 1);
                }
                result.AddLine(startPoint, endPoint);
                result.CloseFigure();
                return result;
              }
            case ShadowPoint.Right:
              {
                // Right only shadow
                PointF startPoint = default(PointF);
                PointF endPoint = default(PointF);
                if (topRight > 0 & topRight < maxAllowed)
                {
                  endPoint = new PointF(_with2.Right - (horizontalRange + 1), _with2.Y + topLeft);
                  result.AddArc(_with2.Right - ((topRight * 2) + horizontalRange + 1), _with2.Y, topRight * 2, topRight * 2, 0, -90);
                  result.AddLine(_with2.Right - (topRight + horizontalRange + 1), _with2.Y, _with2.Right - (topRight + 1), _with2.Y);
                  result.AddArc(_with2.Right - ((topRight * 2) + 1), _with2.Y, topRight * 2, topRight * 2, 270, 90);
                  startPoint = new PointF(_with2.Right - 1, _with2.Y + topRight);
                }
                else
                {
                  endPoint = new PointF(_with2.Right - (horizontalRange + 1), _with2.Y);
                  result.AddLine(endPoint, new PointF(_with2.Right - 1, _with2.Y));
                  startPoint = new PointF(_with2.Right - 1, _with2.Y);
                }
                if (bottomRight > 0 & bottomRight < maxAllowed)
                {
                  result.AddLine(startPoint, new PointF(_with2.Right - 1, _with2.Bottom - (bottomRight + 1)));
                  result.AddArc(_with2.Right - ((bottomRight * 2) + 1), _with2.Bottom - ((bottomRight * 2) + 1), bottomRight * 2, bottomRight * 2, 0, 90);
                  result.AddLine(_with2.Right - (bottomRight + 1), _with2.Bottom - 1, _with2.Right - (bottomRight + horizontalRange + 1), _with2.Bottom - 1);
                  result.AddArc(_with2.Right - ((bottomRight * 2) + horizontalRange + 1), _with2.Bottom - ((bottomRight * 2) + 1), bottomRight * 2, bottomRight * 2, 90, -90);
                  startPoint = new PointF(_with2.Right - (horizontalRange + 1), _with2.Bottom - (bottomRight + 1));
                }
                else
                {
                  result.AddLine(startPoint, new PointF(_with2.Right - 1, _with2.Bottom - 1));
                  result.AddLine(_with2.Right - 1, _with2.Bottom - 1, _with2.Right - (horizontalRange + 1), _with2.Bottom - 1);
                  startPoint = new PointF(_with2.Right - (horizontalRange + 1), _with2.Bottom - 1);
                }
                result.AddLine(startPoint, endPoint);
                result.CloseFigure();
                return result;
              }
          }
        }
      }
      result.AddRectangle(rect);
      return result;
    }
    #endregion

  }
}
