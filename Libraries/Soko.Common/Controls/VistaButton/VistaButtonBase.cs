// File:               VistaButtonBase.cs
// Author:             Pawel Pustelnik
// Created:            11.02.2008
// Last modification:  11.02.2008

using System;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;

namespace Soko.Common.Controls
{
    /// <summary>
    /// Base Class for Vista Button
    /// </summary>
    public class VistaButtonBase : System.Windows.Forms.Button
    {
        public VistaButtonBase()
            : base()
        {
            this.DoubleBuffered = true;
            this.FlatStyle = FlatStyle.Flat;
        }

        /// <summary>
        /// Button State
        /// </summary>
        public enum States
        {
            Normal,
            MouseOver,
            Pushed
        }

        protected States state = States.Normal;

        /// <summary>
        /// Returns graphic path which describes button region
        /// </summary>
        /// <param name="width">Button Width</param>
        /// <param name="height">Button Height</param>
        /// <returns></returns>
        protected GraphicsPath GetPath(int width, int height)
        {
            int X = width;
            int Y = height;

            Point[] points = {
								 new Point(1, 0),
								 new Point(X-1, 0),
								 new Point(X-1, 1),
								 new Point(X, 1),
								 new Point(X, Y-1),
								 new Point(X-1, Y-1),
								 new Point(X-1, Y),
								 new Point(1, Y),
								 new Point(1, Y-1),
								 new Point(0, Y-1),
								 new Point(0, 1),
								 new Point(1, 1)
                             };

            GraphicsPath path = new GraphicsPath();
            path.AddLines(points);

            return path;
        }

        /// <summary>
        /// Sets new region of control
        /// </summary>
        private void SetRegion()
        {
            this.Region = new Region(GetPath(this.Width, this.Height));
        }

        protected override void OnParentChanged(EventArgs e)
        {
            if (Parent == null) return;

            SetRegion();
            base.OnParentChanged(e);
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            state = States.MouseOver;
            this.Invalidate();
            base.OnMouseEnter(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            state = States.Normal;
            this.Invalidate();
            base.OnMouseLeave(e);
        }
        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) != MouseButtons.Left) return;

            state = States.Pushed;

            this.Invalidate();
            base.OnMouseDown(e);
        }
        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                state = States.MouseOver;
            this.Invalidate();
            base.OnMouseUp(e);
        }
        protected override void OnEnter(System.EventArgs e)
        {
            this.Invalidate();
            base.OnEnter(e);
        }
        protected override void OnLeave(System.EventArgs e)
        {
            this.Invalidate();
            base.OnLeave(e);
        }
        protected override void OnClick(System.EventArgs e)
        {
            if (state == States.Pushed)
            {
                state = States.Normal;
                this.Invalidate();
            }
            base.OnClick(e);
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            SetRegion();
            this.Invalidate();
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs pevent)
        {
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            //////////////////////////////////////////////////////////////////

            LinearGradientBrush innerBorderFill = new LinearGradientBrush(new Point(0, 0), new Point(0, this.Height), Color.White, Color.White);
      ColorBlend borderBlend = new ColorBlend
      {
        Colors = new Color[] { Color.FromArgb(255, 255, 251), Color.FromArgb(255, 249, 227), Color.FromArgb(255, 242, 201), Color.FromArgb(255, 231, 150) },
        Positions = new float[] { 0, 0.5F, 0.5F + 0.00000001F, 1 }
      };
      innerBorderFill.InterpolationColors = borderBlend;
            innerBorderFill.GammaCorrection = false;

            pevent.Graphics.FillRectangle(innerBorderFill, rect);

            innerBorderFill.Dispose();
            //////////////////////////////////////////////////////////////////
            LinearGradientBrush borderFill = new LinearGradientBrush(new Point(0, 0), new Point(0, this.Height), Color.FromArgb(246, 241, 228), Color.FromArgb(228, 218, 197));
            Pen border = new Pen(borderFill);
            pevent.Graphics.DrawPath(border, GetPath(this.Width - 1, this.Height - 1));
            border.Dispose();
            borderFill.Dispose();
            //////////////////////////////////////////////////////////////////
            // Fill upper part
            rect.Inflate(-2, -2);
            rect.Height = rect.Height / 2;

            LinearGradientBrush innerFill = new LinearGradientBrush(rect, Color.FromArgb(255, 254, 227), Color.FromArgb(255, 231, 151), LinearGradientMode.Vertical);
            pevent.Graphics.FillRectangle(innerFill, rect);
            innerFill.Dispose();
            //////////////////////////////////////////////////////////////////
            // Fill lower part
            Rectangle rectangle1 = new Rectangle(0, this.Height, this.Width, this.Height / 2);

            int radius = (int)Math.Sqrt((double)((rectangle1.Width * rectangle1.Width) + (rectangle1.Height * rectangle1.Height)));
            GraphicsPath path2 = new GraphicsPath();
            path2.AddEllipse(rectangle1.X - (radius - rectangle1.Width), rectangle1.Y - ((radius - rectangle1.Height) / 2), radius, radius);

            SolidBrush brush1 = new SolidBrush(Color.FromArgb(254, 214, 101));
            rect.Y += rect.Height;
            pevent.Graphics.FillRectangle(brush1, rect);
            brush1.Dispose();

      PathGradientBrush brush3 = new PathGradientBrush(path2)
      {
        CenterColor = Color.FromArgb(255, 239, 179),
        SurroundColors = new Color[] { Color.FromArgb(254, 214, 101) }
      };

      pevent.Graphics.FillRectangle(brush3, rect);
            brush3.Dispose();
            path2.Dispose();
            //////////////////////////////////////////////////////////////////
            StringFormat sf = GetAlignment();
            sf.Trimming = StringTrimming.EllipsisCharacter;

            RectangleF layoutRect = new RectangleF(4, 4, Width - 4, Height - 4);

            SolidBrush textBrush = new SolidBrush(this.ForeColor);
            pevent.Graphics.DrawString(this.Text, this.Font, textBrush, layoutRect, sf);
            textBrush.Dispose();
            //////////////////////////////////////////////////////////////////

            if (this.Focused)
            {
                Rectangle focusRect = new Rectangle(3, 3, Width - 3, Height - 3);
                ControlPaint.DrawFocusRectangle(pevent.Graphics, focusRect);
            }
        }

        /// <summary>
        /// Returns text alignment
        /// </summary>
        /// <returns></returns>
        protected StringFormat GetAlignment()
        {
            StringFormat sf = new StringFormat();

            switch (this.TextAlign)
            {
                case ContentAlignment.BottomCenter:
                    sf.LineAlignment = StringAlignment.Far;
                    sf.Alignment = StringAlignment.Center;
                    break;

                case ContentAlignment.BottomLeft:
                    sf.LineAlignment = StringAlignment.Far;
                    sf.Alignment = StringAlignment.Near;
                    break;

                case ContentAlignment.BottomRight:
                    sf.LineAlignment = StringAlignment.Far;
                    sf.Alignment = StringAlignment.Far;
                    break;


                case ContentAlignment.MiddleCenter:
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;
                    break;

                case ContentAlignment.MiddleLeft:
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Near;
                    break;

                case ContentAlignment.MiddleRight:
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Far;
                    break;


                case ContentAlignment.TopCenter:
                    sf.LineAlignment = StringAlignment.Near;
                    sf.Alignment = StringAlignment.Center;
                    break;

                case ContentAlignment.TopLeft:
                    sf.LineAlignment = StringAlignment.Near;
                    sf.Alignment = StringAlignment.Near;
                    break;

                case ContentAlignment.TopRight:
                    sf.LineAlignment = StringAlignment.Near;
                    sf.Alignment = StringAlignment.Far;
                    break;
            }

            return sf;
        }

    }
}