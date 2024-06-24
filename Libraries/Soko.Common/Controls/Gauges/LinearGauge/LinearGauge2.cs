using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Globalization;
using System.Drawing.Imaging;

namespace Soko.Common.Controls.Gauges
{
  public class LinearGauge2 : Control
  {

    public LinearGauge2()
    {
      // Set the value of the double-buffering style bits to true.
      this.DoubleBuffered = true;
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.ResizeRedraw, true);
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.BackColor = Color.Transparent;
      this.UpdateStyles();

      // Generate digit.
      //GnerateDigit();
    }


    private Image _BackgriundBarImage = new Bitmap(1,1);
    private Image _FrontOverlayImage = new Bitmap(1, 1);
    Rectangle valueBarAreaRect = new Rectangle(0, 0, 1, 1);
    Rectangle valueBarRect = new Rectangle(0, 0, 1, 1);
    Rectangle _LabelRectangle = new Rectangle(0, 0, 1, 1);

    # region Properties

    public object Tag1 = null;
    public object Tag2 = null;


    public Orientation _Orientation = Orientation.Vertical;
    [Category("Attributes")]
    [Description("Orientation of the gauge.")]
    [DefaultValue(Orientation.Vertical)]
    public Orientation Orientation
    {
      set
      {
        if (value != _Orientation)
        {
          _Orientation = value;
          this.Size = new Size(this.Height, this.Width);
          GetRectangles();
          GetBitmaps();
          this.Invalidate();
        }
      }
      get
      {
        return _Orientation;
      }
    }


    [Bindable(true)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override string Text
    {
      set
      {
        base.Text = value;
        this.Invalidate();
      }
      get
      {
        return base.Text;
      }
    }


    [Category("Attributes")]
    [Description("Currently displayed value.")]
    [DefaultValue(0.0)]
    public double Value
    {
      set
      {
        if (value < MinValue)
          value = MinValue;
        else if (value > MaxValue)
          value = MaxValue;

        if (value != mValue)
        {
          mValue = value;
          GetRectangles();
          this.Invalidate();
        }
      }
      get
      {
        return mValue;
      }
    }

    [Category("Attributes")]
    [Description("Minimum scale value.")]
    [DefaultValue(0.0)]
    public double MinValue
    {
      set
      {
        if (mMinimumValue != value)
        {
          mMinimumValue = value;

          if (value > MaxValue)
            MaxValue = value + 1;
          GetRectangles();
          this.Invalidate();
        }
      }
      get
      {
        return mMinimumValue;
      }
    }

    [Category("Attributes")]
    [Description("Maximum scale value.")]
    [DefaultValue(100.0)]
    public double MaxValue
    {
      set
      {
        if (mMaximumValue != value)
        {
          mMaximumValue = value;

          if (value < MinValue)
            MinValue = value - 1;

          GetRectangles();
          this.Invalidate();
        }
      }
      get
      {
        return mMaximumValue;
      }
    }

    # endregion

    protected override CreateParams CreateParams
    {
      get
      {
        CreateParams createParams = base.CreateParams;
        createParams.ExStyle |= 0x20;
        return createParams;
      }
    }

    private const double _ValueBarTopMargin =    0.04395604;
    private const double _ValueBarBottomMargin = 0.03296703;

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);

      Graphics graphics = e.Graphics;

      graphics.SmoothingMode = SmoothingMode.HighQuality;
      graphics.CompositingQuality = CompositingQuality.HighQuality;
      graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
      graphics.InterpolationMode = InterpolationMode.High;
      graphics.PixelOffsetMode = PixelOffsetMode.Half;


      graphics.DrawImage(_BackgriundBarImage, ClientRectangle);
      /// DRAW THE BAR HERE
      graphics.FillRectangle(new SolidBrush(Color.FromArgb(81, 148, 229)), valueBarRect);
      graphics.DrawImage(_FrontOverlayImage, ClientRectangle);

      DrawText(graphics);
    }
    protected override void OnPaintBackground(PaintEventArgs e)
    {
      Graphics graphics = e.Graphics;

      graphics.SmoothingMode = SmoothingMode.HighQuality;
      graphics.CompositingQuality = CompositingQuality.HighQuality;
      graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
      graphics.InterpolationMode = InterpolationMode.High;
      graphics.PixelOffsetMode = PixelOffsetMode.Half;

      base.OnPaintBackground(e);
    }
    protected override void OnBackColorChanged(EventArgs e)
    {
      if (this.Parent != null)
        Parent.Invalidate(this.Bounds, true);
      base.OnBackColorChanged(e);
    }
    protected override void OnParentBackColorChanged(EventArgs e)
    {
      this.Invalidate();
      base.OnParentBackColorChanged(e);
    }
    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
      if (_Orientation == System.Windows.Forms.Orientation.Vertical)
      {
        if (Width < 10)
          Width = 10;
        if (Height < 64)
          Height = 64;
      }
      else
      {
        if (Width < 64)
          Width = 64;
        if (Height < 10)
          Height = 10;
      }

      GetBitmaps();
      GetRectangles();
      this.Invalidate();
    }

    # region Overlay Drawing

    private void GetBitmaps()
    {
      if (_Orientation == System.Windows.Forms.Orientation.Vertical)
      {
        _BackgriundBarImage = Soko.Common.Properties.Resources.LinearGauge2_Back;
        _FrontOverlayImage = Soko.Common.Properties.Resources.LinearGauge2_Front;
      }
      else
      {
        _BackgriundBarImage = Soko.Common.Properties.Resources.LinearGauge2_Back_H;
        _FrontOverlayImage = Soko.Common.Properties.Resources.LinearGauge2_Front_H;
      }

    }
    private void GetRectangles()
    {
      if (_Orientation == System.Windows.Forms.Orientation.Vertical)
      {
        valueBarAreaRect = new Rectangle((int)(ClientRectangle.Width / 3.0f), (int)(ClientRectangle.Height * _ValueBarTopMargin), (int)(ClientRectangle.Width / 3.0f), (int)(ClientRectangle.Height * (1.0f - _ValueBarTopMargin - _ValueBarBottomMargin)));
        double FillValue = (this.Value) / (this.MaxValue - this.MinValue);
        int barHeight = (int)(valueBarAreaRect.Height * FillValue);
        valueBarRect = new Rectangle(valueBarAreaRect.Location.X, valueBarAreaRect.Bottom - barHeight, valueBarAreaRect.Width, barHeight);
        _LabelRectangle = new Rectangle((int)(ClientRectangle.Width * 0.65), ClientRectangle.Top, (int)(ClientRectangle.Width / 3.0f), ClientRectangle.Height);
      }
      else
      {
        valueBarAreaRect = new Rectangle((int)(this.Width * _ValueBarBottomMargin), (int)(this.Height / 3.0f), (int)(ClientRectangle.Width * (1.0f - _ValueBarTopMargin - _ValueBarBottomMargin)), (int)(this.Height / 3.0f));
        double FillValue = (this.Value) / (this.MaxValue - this.MinValue);
        int barWidth = (int)(valueBarAreaRect.Width * FillValue);
        valueBarRect = new Rectangle(valueBarAreaRect.Location.X, valueBarAreaRect.Top, barWidth, valueBarAreaRect.Height);
        _LabelRectangle = new Rectangle(ClientRectangle.X, (int)(ClientRectangle.Bottom * 0.66666f), ClientRectangle.Width, (int)(ClientRectangle.Height / 3.0f));
      }


    }

    private void DrawText(Graphics graphics)
    {
      float textFontSize = 0;
      if (Orientation == System.Windows.Forms.Orientation.Horizontal)
      {
        textFontSize = ClientRectangle.Height / 3.0f * 0.6f;
      }
      else
      {
        textFontSize = ClientRectangle.Width / 3.0f * 0.6f;
      }
      //float textX = (boardRect.Right + boardRect.Left) * 0.5f;
      //float textY = (boardRect.Bottom + boardRect.Top) * 0.65f;

      StringFormat textFormat = new StringFormat(StringFormat.GenericTypographic)
      {
        Alignment = StringAlignment.Center,
        LineAlignment = StringAlignment.Center
      };
      if (Orientation == System.Windows.Forms.Orientation.Vertical)
      {
        textFormat.FormatFlags = StringFormatFlags.DirectionVertical;
      }

      Font textFont = new Font(this.Font.FontFamily, textFontSize, this.Font.Style);

      graphics.DrawString(Text, textFont, new SolidBrush(Color.Black), _LabelRectangle, textFormat);

      textFormat.Dispose();
      textFont.Dispose();
    }

    # endregion


    # region Constants
    const int mClientMargin = 0;
    const float mGuageLeftMargin = 8.0f;
    const float mGuageRightMargin = 8.0f;
    const float mMajorGridWidth = 3;
    const float mMinorGridWidth = 2;
    const float mMinorGridMarginPercentage = 0.2f;
    const float mTitleTopMargin = 8;
    const float mTitleBottomMargin = 8;
    const float mValueTopMargin = 8;
    const float mValueBottomMargin = 8;
    const float mValueHeight = 16.0f;
    const float mScaleOffset = 8.0f;
    const float mScaleWidth = 26.0f;
    # endregion

    # region User Defined Properties
    private double mValue = 0.0;
    private double mMinimumValue = 0.0;
    private double mMaximumValue = 100.0;

    private string mUnit = string.Empty;

    private Color mInnerColor = SystemColors.Control;
    # endregion

 
  }
}
