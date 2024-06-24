using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Soko.Common.Controls.VerticalLabel
{
  public partial class VerticalLabel : Label
  {
    private DrawMode _dm = DrawMode.BottomUp;

    public VerticalLabel()
    {
      InitializeComponent();
    }

      /// <summary>
      /// OnPaint override. This is where the text is rendered vertically.
      /// </summary>
      /// <param name="e">PaintEventArgs</param>
      protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
      {
        float vlblControlWidth;
        float vlblControlHeight;
        float vlblTransformX;
        float vlblTransformY;
        Color controlBackColor = BackColor;
        Pen labelBorderPen = new Pen(controlBackColor, 0);
        SolidBrush labelBackColorBrush = new SolidBrush(controlBackColor);
        SolidBrush labelForeColorBrush = new SolidBrush(base.ForeColor);
        //base.OnPaint(e);
        vlblControlWidth = this.Size.Width;
        vlblControlHeight = this.Size.Height;
        e.Graphics.DrawRectangle(labelBorderPen, 0, 0, vlblControlWidth, vlblControlHeight);
        e.Graphics.FillRectangle(labelBackColorBrush, 0, 0, vlblControlWidth, vlblControlHeight);


        if (this.TextDrawMode == DrawMode.BottomUp)
        {
          vlblTransformX = 0;
          vlblTransformY = vlblControlHeight;
          e.Graphics.TranslateTransform(vlblTransformX, vlblTransformY);
          e.Graphics.RotateTransform(270);
          e.Graphics.DrawString(Text, Font, labelForeColorBrush, 0, 0);
        }
        else
        {
          vlblTransformX = vlblControlWidth;
          vlblTransformY = vlblControlHeight;
          e.Graphics.TranslateTransform(vlblControlWidth, 0);
          e.Graphics.RotateTransform(90);
          e.Graphics.DrawString(Text, Font, labelForeColorBrush, 0, 0, StringFormat.GenericTypographic);
        }
      }

      private void VerticalTextBox_Resize(object sender, System.EventArgs e)
      {
        Invalidate();
      }


      /// <summary>
      /// 
      /// </summary>
      [Category("Properties"), Description("Whether the text will be drawn from Bottom or from Top.")]
      public DrawMode TextDrawMode
      {
        get { return _dm; }
        set { _dm = value; }
      }
    
    /// <summary>
    /// Text Drawing Mode
    /// </summary>
    public enum DrawMode
    {
      /// <summary>
      /// Text is drawn from bottom - up
      /// </summary>
      BottomUp = 1,
      /// <summary>
      /// Text is drawn from top to bottom
      /// </summary>
      TopBottom = 2
    }




  }
}
