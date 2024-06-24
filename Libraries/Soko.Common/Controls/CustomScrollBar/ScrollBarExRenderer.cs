namespace CustomScrollBar
{
   using System;
   using System.Drawing;
   using System.Drawing.Drawing2D;
   using System.Drawing.Imaging;

   /// <summary>
   /// The scrollbar renderer class.
   /// </summary>
   internal static class ScrollBarExRenderer
   {
      #region fields

      /// <summary>
      /// The colors of the thumb in the 3 states.
      /// </summary>
      private static Color[,] thumbColors = new Color[3, 8];

      /// <summary>
      /// The arrow colors in the three states.
      /// </summary>
      private static Color[,] arrowColors = new Color[3, 8];

      /// <summary>
      /// The arrow border colors.
      /// </summary>
      private static Color[] arrowBorderColors = new Color[4];

      /// <summary>
      /// The background colors.
      /// </summary>
      private static Color[] backgroundColors = new Color[5];

      /// <summary>
      /// The track colors.
      /// </summary>
      private static Color[] trackColors = new Color[2];

      #endregion

      #region constructor

      /// <summary>
      /// Initializes static members of the <see cref="ScrollBarExRenderer"/> class.
      /// </summary>
      static ScrollBarExRenderer()
      {
         // hot state
         thumbColors[0, 0] = Color.DodgerBlue; // border color
         thumbColors[0, 1] = Color.FromArgb(232, 233, 233); // left/top start color
         thumbColors[0, 2] = Color.FromArgb(230, 233, 241); // left/top end color
         thumbColors[0, 3] = Color.FromArgb(233, 237, 242); // right/bottom line color
         thumbColors[0, 4] = Color.FromArgb(209, 218, 228); // right/bottom start color
         thumbColors[0, 5] = Color.FromArgb(218, 227, 235); // right/bottom end color
         thumbColors[0, 6] = Color.FromArgb(190, 202, 219); // right/bottom middle color
         thumbColors[0, 7] = Color.FromArgb(96, 11, 148); // left/top line color

         // over state
         thumbColors[1, 0] = Color.FromArgb(67, 150, 227);
         thumbColors[1, 1] = Color.FromArgb(187, 204, 228);
         thumbColors[1, 2] = Color.FromArgb(205, 227, 254);
         thumbColors[1, 3] = Color.FromArgb(252, 253, 255);
         thumbColors[1, 4] = Color.FromArgb(170, 207, 247);
         thumbColors[1, 5] = Color.FromArgb(219, 232, 251);
         thumbColors[1, 6] = Color.FromArgb(190, 202, 219);
         thumbColors[1, 7] = Color.FromArgb(233, 233, 235);

         // pressed state
         thumbColors[2, 0] = Color.FromArgb(23, 73, 138);
         thumbColors[2, 1] = Color.FromArgb(154, 184, 225);
         thumbColors[2, 2] = Color.FromArgb(166, 202, 250);
         thumbColors[2, 3] = Color.FromArgb(221, 235, 251);
         thumbColors[2, 4] = Color.FromArgb(110, 166, 240);
         thumbColors[2, 5] = Color.FromArgb(194, 218, 248);
         thumbColors[2, 6] = Color.FromArgb(190, 202, 219);
         thumbColors[2, 7] = Color.FromArgb(194, 211, 231);

         /* picture of colors and indices
          *(0,0)
          * -----------------------------------------------
          * |                                             |
          * | |-----------------------------------------| |
          * | |                  (2)                    | |
          * | | |-------------------------------------| | |
          * | | |                (0)                  | | |
          * | | |                                     | | |
          * | | |                                     | | |
          * | |3|                (1)                  |3| |
          * | |6|                (4)                  |6| |
          * | | |                                     | | |
          * | | |                (5)                  | | |
          * | | |-------------------------------------| | |
          * | |                  (12)                   | |
          * | |-----------------------------------------| |
          * |                                             |
          * ----------------------------------------------- (15,17)
          */

         // hot state
         arrowColors[0, 0] = Color.FromArgb(223, 236, 252);
         arrowColors[0, 1] = Color.FromArgb(207, 225, 248);
         arrowColors[0, 2] = Color.FromArgb(245, 249, 255);
         arrowColors[0, 3] = Color.FromArgb(237, 244, 252);
         arrowColors[0, 4] = Color.FromArgb(244, 249, 255);
         arrowColors[0, 5] = Color.FromArgb(244, 249, 255);
         arrowColors[0, 6] = Color.FromArgb(251, 253, 255);
         arrowColors[0, 7] = Color.FromArgb(251, 253, 255);

         // over state
         arrowColors[1, 0] = Color.FromArgb(205, 222, 243);
         arrowColors[1, 1] = Color.FromArgb(186, 208, 235);
         arrowColors[1, 2] = Color.FromArgb(238, 244, 252);
         arrowColors[1, 3] = Color.FromArgb(229, 237, 247);
         arrowColors[1, 4] = Color.FromArgb(223, 234, 247);
         arrowColors[1, 5] = Color.FromArgb(241, 246, 254);
         arrowColors[1, 6] = Color.FromArgb(243, 247, 252);
         arrowColors[1, 7] = Color.FromArgb(250, 252, 255);

         // pressed state
         arrowColors[2, 0] = Color.FromArgb(215, 220, 225);
         arrowColors[2, 1] = Color.FromArgb(195, 202, 210);
         arrowColors[2, 2] = Color.FromArgb(242, 244, 245);
         arrowColors[2, 3] = Color.FromArgb(232, 235, 238);
         arrowColors[2, 4] = Color.FromArgb(226, 228, 230);
         arrowColors[2, 5] = Color.FromArgb(230, 233, 236);
         arrowColors[2, 6] = Color.FromArgb(244, 245, 245);
         arrowColors[2, 7] = Color.FromArgb(245, 247, 248);

         // background colors
         backgroundColors[0] = Color.FromArgb(235, 237, 239);
         backgroundColors[1] = Color.FromArgb(252, 252, 252);
         backgroundColors[2] = Color.FromArgb(247, 247, 247);
         backgroundColors[3] = Color.FromArgb(238, 238, 238);
         backgroundColors[4] = Color.FromArgb(240, 240, 240);

         // track colors
         trackColors[0] = Color.FromArgb(204, 204, 204);
         trackColors[1] = Color.FromArgb(220, 220, 220);

         // arrow border colors
         arrowBorderColors[0] = Color.FromArgb(135, 146, 160);
         arrowBorderColors[1] = Color.FromArgb(140, 151, 165);
         arrowBorderColors[2] = Color.FromArgb(128, 139, 153);
         arrowBorderColors[3] = Color.FromArgb(99, 110, 125);
      }

      #endregion

      #region methods

      #region public methods

      /// <summary>
      /// Draws the background.
      /// </summary>
      /// <param name="g">The <see cref="Graphics"/> used to paint.</param>
      /// <param name="rect">The rectangle in which to paint.</param>
      /// <param name="orientation">The <see cref="ScrollBarOrientation"/>.</param>
      public static void DrawBackground(
         Graphics g,
         Rectangle rect,
         ScrollBarOrientation orientation)
      {
         if (g == null)
         {
            throw new ArgumentNullException("g");
         }

         if (rect.IsEmpty || g.IsVisibleClipEmpty
            || !g.VisibleClipBounds.IntersectsWith(rect))
         {
            return;
         }

         if (orientation == ScrollBarOrientation.Vertical)
         {
            DrawBackgroundVertical(g, rect);
         }
         else
         {
            DrawBackgroundHorizontal(g, rect);
         }
      }

      /// <summary>
      /// Draws the channel ( or track ).
      /// </summary>
      /// <param name="g">The <see cref="Graphics"/> used to paint.</param>
      /// <param name="rect">The rectangle in which to paint.</param>
      /// <param name="state">The scrollbar state.</param>
      /// <param name="orientation">The <see cref="ScrollBarOrientation"/>.</param>
      public static void DrawTrack(
         Graphics g,
         Rectangle rect,
         ScrollBarState state,
         ScrollBarOrientation orientation)
      {
         if (g == null)
         {
            throw new ArgumentNullException("g");
         }

         if (rect.Width <= 0 || rect.Height <= 0
            || state != ScrollBarState.Pressed || g.IsVisibleClipEmpty
            || !g.VisibleClipBounds.IntersectsWith(rect))
         {
            return;
         }

         if (orientation == ScrollBarOrientation.Vertical)
         {
            DrawTrackVertical(g, rect);
         }
         else
         {
            DrawTrackHorizontal(g, rect);
         }
      }

      /// <summary>
      /// Draws the thumb.
      /// </summary>
      /// <param name="g">The <see cref="Graphics"/> used to paint.</param>
      /// <param name="rect">The rectangle in which to paint.</param>
      /// <param name="state">The <see cref="ScrollBarState"/> of the thumb.</param>
      /// <param name="orientation">The <see cref="ScrollBarOrientation"/>.</param>
      public static void DrawThumb(
         Graphics g,
         Rectangle rect,
         ScrollBarState state,
         ScrollBarOrientation orientation)
      {
         if (g == null)
         {
            throw new ArgumentNullException("g");
         }

         if (rect.IsEmpty || g.IsVisibleClipEmpty
            || !g.VisibleClipBounds.IntersectsWith(rect)
            || state == ScrollBarState.Disabled)
         {
            return;
         }

         if (orientation == ScrollBarOrientation.Vertical)
         {
            DrawThumbVertical(g, rect, state);
         }
         else
         {
            DrawThumbHorizontal(g, rect, state);
         }
      }

      #endregion

      #region private methods

      /// <summary>
      /// Draws the background.
      /// </summary>
      /// <param name="g">The <see cref="Graphics"/> used to paint.</param>
      /// <param name="rect">The rectangle in which to paint.</param>
      private static void DrawBackgroundVertical(Graphics g, Rectangle rect)
      {

        //////////////////////////////////////////////////////////
         float radius = rect.Width / 2.0f;
         // create the arc for the rectangle sides and declare 
         // a graphics path object for the drawing 
         float diameter = radius * 2.0F;
         SizeF sizeF = new SizeF(diameter, diameter);
         RectangleF arc = new RectangleF(rect.Location, sizeF);
         //RectangleF ZeroArc = new RectangleF(baseRect.Location, new SizeF(0.0001f, 0.0001f));
         GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
         // top left arc 
         path.AddArc(arc, 180, 90);
         // top right arc 
         arc.X = rect.Right - diameter;
         path.AddArc(arc, 270, 90);
         // bottom right arc 
         arc.X = rect.Right - diameter;
         arc.Y = rect.Bottom - diameter;
         path.AddArc(arc, 0, 90);
         // bottom left arc
         arc.X = rect.Left;
         path.AddArc(arc, 90, 90);
         //path.AddArc(arc, 270, 90);
         path.CloseFigure();
         
        //////////////////////////////////////////////////////////


         using (LinearGradientBrush brush = new LinearGradientBrush(
            rect,
            backgroundColors[2],
            backgroundColors[3],
            LinearGradientMode.Horizontal))
         {
            g.FillPath(brush, path);
         }

      }

      /// <summary>
      /// Draws the background.
      /// </summary>
      /// <param name="g">The <see cref="Graphics"/> used to paint.</param>
      /// <param name="rect">The rectangle in which to paint.</param>
      private static void DrawBackgroundHorizontal(Graphics g, Rectangle rect)
      {

        //////////////////////////////////////////////////////////
        float radius = rect.Height / 2.0f;
        // create the arc for the rectangle sides and declare 
        // a graphics path object for the drawing 
        float diameter = radius * 2.0F;
        SizeF sizeF = new SizeF(diameter, diameter);
        RectangleF arc = new RectangleF(rect.Location, sizeF);
        //RectangleF ZeroArc = new RectangleF(baseRect.Location, new SizeF(0.0001f, 0.0001f));
        GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
        // top left arc 
        path.AddArc(arc, 180, 90);
        // top right arc 
        arc.X = rect.Right - diameter;
        path.AddArc(arc, 270, 90);
        // bottom right arc 
        arc.X = rect.Right - diameter;
        arc.Y = rect.Bottom - diameter;
        path.AddArc(arc, 0, 90);
        // bottom left arc
        arc.X = rect.Left;
        path.AddArc(arc, 90, 90);
        //path.AddArc(arc, 270, 90);
        path.CloseFigure();
        //////////////////////////////////////////////////////////

        using (LinearGradientBrush brush = new LinearGradientBrush(
           rect,
           backgroundColors[2],
           backgroundColors[3],
           LinearGradientMode.Horizontal))
        {
          g.FillPath(brush, path);
        }
      }

      /// <summary>
      /// Draws the channel ( or track ).
      /// </summary>
      /// <param name="g">The <see cref="Graphics"/> used to paint.</param>
      /// <param name="rect">The rectangle in which to paint.</param>
      private static void DrawTrackVertical(Graphics g, Rectangle rect)
      {
         Rectangle innerRect = new Rectangle(rect.Left + 1, rect.Top, 15, rect.Height);

         using (LinearGradientBrush brush = new LinearGradientBrush(
            innerRect,
            trackColors[0],
            trackColors[1],
            LinearGradientMode.Horizontal))
         {
            g.FillRectangle(brush, innerRect);
         }
      }

      /// <summary>
      /// Draws the channel ( or track ).
      /// </summary>
      /// <param name="g">The <see cref="Graphics"/> used to paint.</param>
      /// <param name="rect">The rectangle in which to paint.</param>
      private static void DrawTrackHorizontal(Graphics g, Rectangle rect)
      {
         Rectangle innerRect = new Rectangle(rect.Left, rect.Top + 1, rect.Width, 15);

         using (LinearGradientBrush brush = new LinearGradientBrush(
            innerRect,
            trackColors[0],
            trackColors[1],
            LinearGradientMode.Vertical))
         {
            g.FillRectangle(brush, innerRect);
         }
      }

      /// <summary>
      /// Draws the thumb.
      /// </summary>
      /// <param name="g">The <see cref="Graphics"/> used to paint.</param>
      /// <param name="rect">The rectangle in which to paint.</param>
      /// <param name="state">The <see cref="ScrollBarState"/> of the thumb.</param>
      private static void DrawThumbVertical(
         Graphics g,
         Rectangle rect,
         ScrollBarState state)
      {
         //Color thumbColor = Color.FromArgb(67, 160, 227); Blue
         Color thumbColor = Color.FromArgb(0, 135, 64); //Color changed to green

            Rectangle innerRect = rect;

         //////////////////////////////////////////////////////////
         float radius = innerRect.Width / 2.0f;
         // create the arc for the rectangle sides and declare 
         // a graphics path object for the drawing 
         float diameter = radius * 2.0F;
         SizeF sizeF = new SizeF(diameter, diameter);
         RectangleF arc = new RectangleF(innerRect.Location, sizeF);
         //RectangleF ZeroArc = new RectangleF(baseRect.Location, new SizeF(0.0001f, 0.0001f));
         GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
         // top left arc 
         path.AddArc(arc, 180, 90);
         // top right arc 
         arc.X = innerRect.Right - diameter;
         path.AddArc(arc, 270, 90);
         // bottom right arc 
         arc.X = innerRect.Right - diameter;
         arc.Y = innerRect.Bottom - diameter;
         path.AddArc(arc, 0, 90);
         // bottom left arc
         arc.X = innerRect.Left;
         path.AddArc(arc, 90, 90);
         //path.AddArc(arc, 270, 90);
         path.CloseFigure();

         //////////////////////////////////////////////////////////
         using (Brush brush = new SolidBrush(thumbColor) )
         {
            g.FillPath(brush, path);
         }
      }

      /// <summary>
      /// Draws the thumb.
      /// </summary>
      /// <param name="g">The <see cref="Graphics"/> used to paint.</param>
      /// <param name="rect">The rectangle in which to paint.</param>
      /// <param name="state">The <see cref="ScrollBarState"/> of the thumb.</param>
      private static void DrawThumbHorizontal(
         Graphics g,
         Rectangle rect,
         ScrollBarState state)
      {
            Color thumbColor = Color.FromArgb(67, 160, 227);
            //Color thumbColor = Color.FromArgb(0, 135, 64);
        Rectangle innerRect = rect;

        //////////////////////////////////////////////////////////
        float radius = innerRect.Height / 2.0f;
        // create the arc for the rectangle sides and declare 
        // a graphics path object for the drawing 
        float diameter = radius * 2.0F;
        SizeF sizeF = new SizeF(diameter, diameter);
        RectangleF arc = new RectangleF(innerRect.Location, sizeF);
        //RectangleF ZeroArc = new RectangleF(baseRect.Location, new SizeF(0.0001f, 0.0001f));
        GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
        // top left arc 
        path.AddArc(arc, 180, 90);
        // top right arc 
        arc.X = innerRect.Right - diameter;
        path.AddArc(arc, 270, 90);
        // bottom right arc 
        arc.X = innerRect.Right - diameter;
        arc.Y = innerRect.Bottom - diameter;
        path.AddArc(arc, 0, 90);
        // bottom left arc
        arc.X = innerRect.Left;
        path.AddArc(arc, 90, 90);
        //path.AddArc(arc, 270, 90);
        path.CloseFigure();
        //////////////////////////////////////////////////////////
        using (Brush brush = new SolidBrush(thumbColor))
        {
          g.FillPath(brush, path);
        }
      }

      /// <summary>
      /// Creates a rounded rectangle.
      /// </summary>
      /// <param name="r">The rectangle to create the rounded rectangle from.</param>
      /// <param name="radiusX">The x-radius.</param>
      /// <param name="radiusY">The y-radius.</param>
      /// <returns>A <see cref="GraphicsPath"/> object representing the rounded rectangle.</returns>
      private static GraphicsPath CreateRoundPath(
         Rectangle r,
         float radiusX,
         float radiusY)
      {
         // create new graphics path object
         GraphicsPath path = new GraphicsPath();

         // calculate radius of edges
         PointF d = new PointF(Math.Min(radiusX * 2, r.Width), Math.Min(radiusY * 2, r.Height));

         // make sure radius is valid
         d.X = Math.Max(1, d.X);
         d.Y = Math.Max(1, d.Y);

         // add top left arc
         path.AddArc(r.X, r.Y, d.X, d.Y, 180, 90);

         // add top right arc
         path.AddArc(r.Right - d.X, r.Y, d.X, d.Y, 270, 90);

         // add bottom right arc
         path.AddArc(r.Right - d.X, r.Bottom - d.Y, d.X, d.Y, 0, 90);

         // add bottom left arc
         path.AddArc(r.X, r.Bottom - d.Y, d.X, d.Y, 90, 90);

         // close path
         path.CloseFigure();

         return path;
      }

      #endregion

      #endregion
   }
}
