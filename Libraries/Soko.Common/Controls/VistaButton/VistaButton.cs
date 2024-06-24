// File:               VistaButton.cs
// Author:             Pawel Pustelnik
// Created:            11.02.2008
// Last modification:  11.02.2008

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Soko.Common.Controls
{
    /// <summary>
    /// Vista (like) Button
    /// </summary>
    public class VistaButton : VistaButtonBase
    {
        public ButtonColors NormalColors;
        public ButtonColors HoverColors;
        public ButtonColors PressedColors;

        /// <summary>
        /// Main Constructor
        /// </summary>
        public VistaButton()
            : base()
        {
            NormalColors.BorderColor1 = Color.FromArgb(142, 143, 143);
            NormalColors.BorderColor2 = Color.FromArgb(142, 143, 143);

            NormalColors.FillColor1 = Color.FromArgb(240, 240, 240);
            NormalColors.FillColor2 = Color.FromArgb(235, 235, 235);
            NormalColors.FillColor3 = Color.FromArgb(224, 224, 224);
            NormalColors.FillColor4 = Color.FromArgb(215, 215, 215);

            //////////////////////////////////////////////////////////////
            HoverColors.BorderColor1 = Color.FromArgb(146, 141, 128);
            HoverColors.BorderColor2 = Color.FromArgb(128, 118, 97);
            HoverColors.FillColor1 = Color.FromArgb(255, 254, 227);
            HoverColors.FillColor2 = Color.FromArgb(255, 231, 151);
            HoverColors.FillColor3 = Color.FromArgb(254, 214, 101);
            HoverColors.FillColor4 = Color.FromArgb(255, 239, 179);

            //////////////////////////////////////////////////////////////
            PressedColors.BorderColor1 = Color.FromArgb(60, 127, 177);
            PressedColors.BorderColor2 = Color.FromArgb(60, 127, 177);
            PressedColors.FillColor1 = Color.FromArgb(221, 242, 252);
            PressedColors.FillColor2 = Color.FromArgb(215, 239, 252);
            PressedColors.FillColor3 = Color.FromArgb(189, 228, 250);
            PressedColors.FillColor4 = Color.FromArgb(171, 221, 248);
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;
            ButtonColors clr = NormalColors;

            if (state == States.MouseOver)
                clr = HoverColors;
            if (state == States.Pushed)
                clr = PressedColors;

            Rectangle baseRect = new Rectangle(0, 0, this.Width, this.Height);
            Rectangle rect = baseRect;
            //////////////////////////////////////////////////////////////////

            LinearGradientBrush innerBorderFill = new LinearGradientBrush(new Point(0, 0), new Point(0, this.Height), Color.White, Color.White);
            ColorBlend borderBlend = new ColorBlend();
            int alpha = 220;
            borderBlend.Colors = new Color[] { Color.FromArgb(alpha, clr.FillColor1), Color.FromArgb(alpha, clr.FillColor2), Color.FromArgb(alpha, clr.FillColor3), Color.FromArgb(alpha, clr.FillColor4) };
            borderBlend.Positions = new float[] { 0, 0.5F, 0.5F + 0.00000001F, 1 };
            innerBorderFill.InterpolationColors = borderBlend;

            pevent.Graphics.FillRectangle(innerBorderFill, rect);

            innerBorderFill.Dispose();
            //////////////////////////////////////////////////////////////////
            LinearGradientBrush borderFill = new LinearGradientBrush(new Point(0, 0), new Point(0, this.Height), clr.BorderColor1, clr.BorderColor2);
            Pen border = new Pen(borderFill);
            pevent.Graphics.DrawPath(border, GetPath(this.Width - 1, this.Height - 1));
            border.Dispose();
            borderFill.Dispose();
            //////////////////////////////////////////////////////////////////
            // Fill upper part
            rect.Inflate(-2, -2);
            rect.Height = rect.Height / 2;

            LinearGradientBrush innerFill = new LinearGradientBrush(rect, clr.FillColor1, clr.FillColor2, LinearGradientMode.Vertical);
            pevent.Graphics.FillRectangle(innerFill, rect);
            innerFill.Dispose();
            //////////////////////////////////////////////////////////////////
            // Fill lower part
            Rectangle rectangle1 = new Rectangle(0, this.Height, this.Width, this.Height / 2);

            int radius = (int)Math.Sqrt((double)((rectangle1.Width * rectangle1.Width) + (rectangle1.Height * rectangle1.Height)));
            GraphicsPath path2 = new GraphicsPath();
            path2.AddEllipse(rectangle1.X - (radius - rectangle1.Width), rectangle1.Y - ((radius - rectangle1.Height) / 2), radius, radius);

            SolidBrush brush1 = new SolidBrush(clr.FillColor3);
            rect.Y += rect.Height;
            pevent.Graphics.FillRectangle(brush1, rect);
            brush1.Dispose();

      PathGradientBrush brush3 = new PathGradientBrush(path2)
      {
        FocusScales = new PointF(0.2F, 0.2F),
        CenterColor = clr.FillColor4,
        SurroundColors = new Color[] { clr.FillColor3 }
      };

      pevent.Graphics.FillRectangle(brush3, rect);
            brush3.Dispose();
            path2.Dispose();
            //////////////////////////////////////////////////////////////////
            StringFormat sf = GetAlignment();
            sf.Trimming = StringTrimming.EllipsisCharacter;
            sf.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;

            RectangleF layoutRect = baseRect;
            layoutRect.Inflate(-4, -4);

            SolidBrush textBrush = new SolidBrush(this.ForeColor);
            pevent.Graphics.DrawString(this.Text, this.Font, textBrush, layoutRect, sf);
            textBrush.Dispose();
            //////////////////////////////////////////////////////////////////

            if (this.Focused)
            {
                Rectangle focusRect = baseRect;
                focusRect.Inflate(-3, -3);
                ControlPaint.DrawFocusRectangle(pevent.Graphics, focusRect);
            }
        }
    }
}
