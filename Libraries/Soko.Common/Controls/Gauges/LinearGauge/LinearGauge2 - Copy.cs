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

namespace Bluereach.Common.Controls.Gauges
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

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (mCachedOverlay != null)
          mCachedOverlay.Dispose();
      }
      base.Dispose(disposing);
    }

    private Image _BackgriundBarImage = new Bitmap(1,1);
    private Image _FrontOverlayImage = new Bitmap(1, 1);
    Rectangle valueBarAreaRect = new Rectangle(0, 0, 1, 1);
    Rectangle valueBarRect = new Rectangle(0, 0, 1, 1);

    # region Properties

    public object Tag1 = null;
    public object Tag2 = null;

    [Bindable(true)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override string Text
    {
      set
      {
        base.Text = value;
        InvalidateOverlay();
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

          InvalidateOverlay();

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

          InvalidateOverlay();

          this.Invalidate();
        }
      }
      get
      {
        return mMaximumValue;
      }
    }



    [Category("Appearance")]
    [Description("Inner background color.")]
    [DefaultValue(typeof(Color), "Control")]
    public Color InnerColor
    {
      set
      {
        if (this.mInnerColor != value)
        {
          this.mInnerColor = value;
          if (this.Parent != null)
            Parent.Invalidate(this.Bounds, true);

          InvalidateOverlay();
        }
      }
      get
      {
        return this.mInnerColor;
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
      //Rectangle valueBarRect = new Rectangle((int)(ClientRectangle.Width / 3.0f), (int)(ClientRectangle.Height * _ValueBarTopMargin), (int)(ClientRectangle.Width / 3.0f), 20);
      //graphics.DrawRectangle(new Pen(new SolidBrush(Color.Red)), valueBarAreaRect);

      //DrawDigitNumber(graphics);
      //DrawPointer(graphics);
      //DrawOverlay(graphics);

      //DrawValue(graphics);

      //graphics.DrawImage(mCachedOverlay, ClientRectangle);
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

      UpdateOverlay();
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

      if (Width < 10)
        Width = 10;
      if (Height < 64)
        Height = 64;

      GetBitmaps();
      GetRectangles();
      InvalidateOverlay();
    }

    # region Overlay Drawing

    private void GetBitmaps()
    {
      _BackgriundBarImage = Bluereach.Common.Properties.Resources.LinearGauge2_Back;
      _FrontOverlayImage = Bluereach.Common.Properties.Resources.LinearGauge2_Front;

    }
    private void GetRectangles()
    {
      valueBarAreaRect = new Rectangle((int)(ClientRectangle.Width / 3.0f), (int)(ClientRectangle.Height * _ValueBarTopMargin), (int)(ClientRectangle.Width / 3.0f), (int)(ClientRectangle.Height * (1.0f - _ValueBarTopMargin - _ValueBarBottomMargin)));
      double FillValue = (this.Value) / (this.MaxValue - this.MinValue);
      int barHeight = (int)(valueBarAreaRect.Height * FillValue);
      valueBarRect = new Rectangle(valueBarAreaRect.Location.X, valueBarAreaRect.Bottom - barHeight, valueBarAreaRect.Width, barHeight);
    }

    private void DrawOverlay(Graphics graphics, Rectangle drawingRegion)
    {
      DrawOverlay(graphics, new RectangleF(drawingRegion.X, drawingRegion.Y, drawingRegion.Width, drawingRegion.Height));
    }

    private void DrawOverlay(Graphics graphics, RectangleF drawingRegion)
    {
      float mTitleHeight = this.Height * 0.055f;


      mClientRegion = RectangleF.Inflate(drawingRegion, -mClientMargin, -mClientMargin);
      if (mClientRegion.IsEmpty)
        return;

      mGaugeRegion = new RectangleF(
          mClientRegion.X + mGuageLeftMargin, mClientRegion.Y,
          mClientRegion.Width - mGuageLeftMargin - mGuageRightMargin, mClientRegion.Height);

      mTitleRect = new RectangleF(
          mGaugeRegion.X, mClientRegion.Y + mTitleTopMargin,
          mGaugeRegion.Width, mTitleHeight);

      mGaugeRegion.Y += mTitleRect.Height + mTitleTopMargin + mTitleBottomMargin;
      mGaugeRegion.Height -= mTitleRect.Height + mTitleTopMargin + mTitleBottomMargin;

      mValueRect = new RectangleF(
          mGaugeRegion.X, mClientRegion.Bottom - mValueBottomMargin - mValueHeight,
          mGaugeRegion.Width, mValueHeight);

      mGaugeRegion.Height -= mValueRect.Height + mValueTopMargin + mValueBottomMargin;

      mScaleRegion = new RectangleF(
          mGaugeRegion.Right - mScaleWidth, mGaugeRegion.Y, mScaleWidth, mGaugeRegion.Height);

      mGaugeRegion.Width -= mScaleWidth + mScaleOffset;

      mScaleRegion.Y = mGaugeRegion.Y;
      mScaleRegion.Height = mGaugeRegion.Height;

      float mMajorScaleHeight = mGaugeRegion.Height * 0.04f;
      float mMinorScaleHeight = mGaugeRegion.Height * 0.03f;

      // Debug regions
      //graphics.FillRectangle(Brushes.Pink, drawingRegion);
      //graphics.FillRectangle(Brushes.Red, mClientRegion);
      //graphics.FillRectangle(Brushes.Green, mGaugeRegion);
      //graphics.FillRectangle(Brushes.Blue, mTitleRect);
      //graphics.FillRectangle(Brushes.Orange, mValueRect);
      //graphics.FillRectangle(Brushes.Lime, mScaleRegion);


      //
      {
        Brush titleBrush = new SolidBrush(Color.Black);

        StringFormat titleLabelFormat = new StringFormat(StringFormat.GenericTypographic);
        titleLabelFormat.Alignment = StringAlignment.Center;
        titleLabelFormat.LineAlignment = StringAlignment.Center;

        Font titleFont = new Font(this.Font.FontFamily, mTitleHeight, this.Font.Style);

        graphics.DrawString(Text, titleFont, titleBrush,
            mTitleRect.X + mTitleRect.Width * 0.5f,
            mTitleRect.Y + mTitleRect.Height * 0.5f, titleLabelFormat);

        titleLabelFormat.Dispose();
        titleFont.Dispose();
        titleBrush.Dispose();
      }

      Brush scaleBrush = new SolidBrush(Color.Black);

      StringFormat scaleLabelFormat = new StringFormat(StringFormat.GenericTypographic);
      scaleLabelFormat.Alignment = StringAlignment.Center;
      scaleLabelFormat.LineAlignment = StringAlignment.Center;

      Font majorScaleFont = new Font(this.Font.FontFamily, mMajorScaleHeight, this.Font.Style);
      Font minorScaleFont = new Font(this.Font.FontFamily, mMinorScaleHeight, this.Font.Style);



      scaleLabelFormat.Dispose();
      majorScaleFont.Dispose();
      minorScaleFont.Dispose();
      scaleBrush.Dispose();

      /*
      mGaugeRegion = RectangleF.Inflate(drawingRegion, -mMargin, -mMargin);
      if (mGaugeRegion.IsEmpty)
          return;

      mRimWidth = mGaugeRegion.Width * 0.03f;
      mInnerRimWidth = mGaugeRegion.Width * 0.025f;
      mTresholdWidth = mGaugeRegion.Width * 0.020f;

      // Draw Overlay
      using (Brush backgroundBrush = new SolidBrush(InnerColor))
          graphics.FillEllipse(backgroundBrush, RectangleF.Inflate(mGaugeRegion, -0.5f, -0.5f));

      // Draw Rim
      mRimOutlineRect = mGaugeRegion;
      Color rimOutlineColor = Color.FromArgb(100, Color.SlateGray);
      Color rimOutlineThickColor = Color.SlateGray;
      using (Pen rimOutlinePen = new Pen(rimOutlineColor, mRimWidth))
      {
          rimOutlinePen.Alignment = PenAlignment.Inset;
          graphics.DrawEllipse(rimOutlinePen, mRimOutlineRect);
      }
      mRimOutlineThickRect = RectangleF.Inflate(mRimOutlineRect, -mRimWidth * 0.5f, -mRimWidth * 0.5f);
      using (Pen rimOutlineThickPen = new Pen(rimOutlineThickColor))
          graphics.DrawEllipse(rimOutlineThickPen, mRimOutlineThickRect);

      // Draw Inner Rim
      mInnerRimRect = RectangleF.Inflate(mRimOutlineRect, -mRimWidth, -mRimWidth);
      RectangleF innerRimCenterRect = RectangleF.Inflate(mInnerRimRect, -mInnerRimWidth * 0.5f, -mInnerRimWidth * 0.5f);
      Color innerRimColor = Color.FromArgb(190, Color.Gainsboro);
      using (Pen innerRimPen = new Pen(innerRimColor, mInnerRimWidth))
          // Pen alignment does not work with arcs, so we need to adjust bounding rectangle.
          graphics.DrawArc(innerRimPen, innerRimCenterRect, mGaugeAngleStart, mGaugeAngleEnd - mGaugeAngleStart);

      // Draw Threshold
      DrawTreshold(graphics, innerRimCenterRect, mTresholdWidth);

      // Draw Scale
      mScaleGapWidth = mInnerRimRect.Width * 0.005f;
      mScaleRect = RectangleF.Inflate(mInnerRimRect, -(mInnerRimWidth + mScaleGapWidth), -(mInnerRimWidth + mScaleGapWidth));
      DrawScale(graphics, mScaleRect);

      // Draw Text
      DrawText(graphics, mScaleRect);

      // Draw Digital Board
      DrawDigitalBoard(graphics, mScaleRect);
      */
    }


    /*
    private void DrawTreshold(Graphics graphics, RectangleF tresholdRect, float tresholdWidth)
    {
        Color tresholdColor = Color.FromArgb(200, Color.LawnGreen);
        Color tresholdWarningColor = Color.FromArgb(200, Color.Yellow);
        Color tresholdErrorColor = Color.FromArgb(200, Color.Red);
        Pen tresholdPen = new Pen(tresholdColor, tresholdWidth);
        Pen tresholdWarningPen = new Pen(tresholdWarningColor, tresholdWidth);
        Pen tresholdErrorPen = new Pen(tresholdErrorColor, tresholdWidth);

        if (mTresholdMargin > 0.0f)
        {
            float tresholdStartAngle = GetAngleFromValue(mTresholdValue - mTresholdMargin);
            float tresholdEndAngle = GetAngleFromValue(mTresholdValue + mTresholdMargin);
            graphics.DrawArc(tresholdPen, tresholdRect, tresholdStartAngle, tresholdEndAngle - tresholdStartAngle);
        }

        if (mTresholdLeftWarningMargin > 0.0f)
        {
            float tresholdLeftWarningStartAngle = GetAngleFromValue(mTresholdValue - mTresholdMargin - mTresholdLeftWarningMargin);
            float tresholdLeftWarningEndAngle = GetAngleFromValue(mTresholdValue - mTresholdMargin);
            graphics.DrawArc(tresholdWarningPen, tresholdRect, tresholdLeftWarningStartAngle, tresholdLeftWarningEndAngle - tresholdLeftWarningStartAngle);
        }

        if (mTresholdRightWarningMargin > 0.0f)
        {
            float tresholdRightWarningStartAngle = GetAngleFromValue(mTresholdValue + mTresholdMargin);
            float tresholdRightWarningEndAngle = GetAngleFromValue(mTresholdValue + mTresholdMargin + mTresholdRightWarningMargin);
            graphics.DrawArc(tresholdWarningPen, tresholdRect, tresholdRightWarningStartAngle, tresholdRightWarningEndAngle - tresholdRightWarningStartAngle);
        }

        if (mTresholdLeftErrorMargin > 0.0f)
        {
            float tresholdLeftErrorStartAngle = GetAngleFromValue(mTresholdValue - mTresholdMargin - mTresholdLeftWarningMargin - mTresholdLeftErrorMargin);
            float tresholdLeftErrorEndAngle = GetAngleFromValue(mTresholdValue - mTresholdMargin - mTresholdLeftWarningMargin);
            graphics.DrawArc(tresholdErrorPen, tresholdRect, tresholdLeftErrorStartAngle, tresholdLeftErrorEndAngle - tresholdLeftErrorStartAngle);
        }

        if (mTresholdRightWarningMargin > 0.0f)
        {
            float tresholdRightErrorStartAngle = GetAngleFromValue(mTresholdValue + mTresholdMargin + mTresholdRightWarningMargin);
            float tresholdRightErrorEndAngle = GetAngleFromValue(mTresholdValue + mTresholdMargin + mTresholdRightWarningMargin + mTresholdRightErrorMargin);
            graphics.DrawArc(tresholdErrorPen, tresholdRect, tresholdRightErrorStartAngle, tresholdRightErrorEndAngle - tresholdRightErrorStartAngle);
        }

        tresholdPen.Dispose();
        tresholdWarningPen.Dispose();
        tresholdErrorPen.Dispose();
    }
    private void DrawScale(Graphics graphics, RectangleF scaleRect)
    {
        float scaleRadius = scaleRect.Width * 0.5f;
        float majorScaleWidth = scaleRect.Width * 0.025f;
        float majorScaleRadius = scaleRadius - scaleRect.Width * 0.06f;
        float minorScaleWidth = scaleRect.Width * 0.01f;
        float minorScaleRadius = scaleRadius - scaleRect.Width * 0.025f;
        float scaleCenterX = (scaleRect.Right + scaleRect.Left) * 0.5f;
        float scaleCenterY = (scaleRect.Bottom + scaleRect.Top) * 0.5f;
        float scaleLabelRadius = scaleRadius * 0.77f;
        float scaleFontHeight = scaleRadius * 0.10f;

        float majorScaleAngleStep = (mGaugeAngleEnd - mGaugeAngleStart) / MajorGrid;
        float majorScaleValueStep = (float)(MaxValue - MinValue) / MajorGrid;

        Color majorScaleColor = Color.Black;
        Color minorScaleColor = Color.Black;
        Pen majorScalePen = new Pen(majorScaleColor, majorScaleWidth);
        Pen minorScalePen = new Pen(minorScaleColor, minorScaleWidth);

        Brush scaleLabelBrush = new SolidBrush(this.ForeColor);

        StringFormat scaleLabelFormat = new StringFormat(StringFormat.GenericTypographic);
        scaleLabelFormat.Alignment = StringAlignment.Center;
        scaleLabelFormat.LineAlignment = StringAlignment.Center;

        Font scaleFont = new Font(this.Font.FontFamily, scaleFontHeight, this.Font.Style);

        for (int majorScale = 0; majorScale <= MajorGrid; ++majorScale)
        {
            // Draw Major Scale
            float majorAngle = (float)Math.PI * (mGaugeAngleStart + majorScale * majorScaleAngleStep) / 180.0f;
            float majorValue = (float)(MinValue + majorScale * majorScaleValueStep);
            float majorX = (float)Math.Cos(majorAngle);
            float majorY = (float)Math.Sin(majorAngle);

            graphics.DrawLine(majorScalePen,
                scaleCenterX + majorX * scaleRadius,
                scaleCenterY + majorY * scaleRadius,
                scaleCenterX + majorX * majorScaleRadius,
                scaleCenterY + majorY * majorScaleRadius);

            // Draw Minor Scale
            if (majorScale < MajorGrid)
            {
                for (int minorScale = 1; minorScale < MinorGrid; ++minorScale)
                {
                    float minorAngle = majorAngle + (float)Math.PI * (minorScale * majorScaleAngleStep / MinorGrid) / 180.0f;
                    float minorX = (float)Math.Cos(minorAngle);
                    float minorY = (float)Math.Sin(minorAngle);

                    graphics.DrawLine(minorScalePen,
                        scaleCenterX + minorX * scaleRadius,
                        scaleCenterY + minorY * scaleRadius,
                        scaleCenterX + minorX * minorScaleRadius,
                        scaleCenterY + minorY * minorScaleRadius);
                }
            }

            // Draw Label
            graphics.DrawString(Math.Round(majorValue, 0).ToString(), scaleFont, scaleLabelBrush,
                scaleCenterX + majorX * scaleLabelRadius,
                scaleCenterY + majorY * scaleLabelRadius, scaleLabelFormat);
        }

        majorScalePen.Dispose();
        minorScalePen.Dispose();
        scaleLabelBrush.Dispose();
        scaleLabelFormat.Dispose();
        scaleFont.Dispose();
    }
    */
    private void DrawText(Graphics graphics, RectangleF boardRect)
    {
      float textFontSize = (boardRect.Right + boardRect.Left) * 0.05f;
      float textX = (boardRect.Right + boardRect.Left) * 0.5f;
      float textY = (boardRect.Bottom + boardRect.Top) * 0.65f;

      StringFormat textFormat = new StringFormat(StringFormat.GenericTypographic);
      textFormat.Alignment = StringAlignment.Center;
      textFormat.LineAlignment = StringAlignment.Center;

      Font textFont = new Font(this.Font.FontFamily, textFontSize, this.Font.Style);

      graphics.DrawString(Text, textFont, new SolidBrush(this.ForeColor), textX, textY, textFormat);

      textFormat.Dispose();
      textFont.Dispose();
    }
    /*
    private void DrawDigitalBoard(Graphics graphics, RectangleF boardRect)
    {
        mDigitalBoardOverlayRect = RectangleF.Empty;
        mDigitalBoardOverlayRect.X = (boardRect.Right + boardRect.Left) * 0.5f;
        mDigitalBoardOverlayRect.Y = (boardRect.Bottom + boardRect.Top) * 0.835f;
        mDigitalBoardOverlayRect = RectangleF.Inflate(mDigitalBoardOverlayRect,
            boardRect.Width * 0.23f, boardRect.Height * 0.065f);

        Color boardOverlayColor = Color.FromArgb(30, Color.Gray);
        using (Brush boardOverlayBrush = new SolidBrush(boardOverlayColor))
            graphics.FillRectangle(boardOverlayBrush, mDigitalBoardOverlayRect);
    }
    */
    # endregion


    private float GetAngleFromValue(float value)
    {
      return GetAngleFromValue(value, 0.0f);
    }
    private float GetAngleFromValue(float value, float margin)
    {
      float valueRange = (float)(MaxValue - MinValue);
      float angleRange = mGaugeAngleEnd - mGaugeAngleStart;

      if (valueRange <= 0.0f)
        return 0.0f;

      float result = (float)(mGaugeAngleStart + (value - MinValue) * angleRange / valueRange);

      if (result < (mGaugeAngleStart - margin))
        result = mGaugeAngleStart - margin;
      else if (result > (mGaugeAngleEnd + margin))
        result = mGaugeAngleEnd + margin;

      return result;
    }

    private void InvalidateOverlay()
    {
      mInvalidateOverlay = true;
      //mInvalidatePointer = true;
      //mInvalidateOverlay = true;

      this.Invalidate();
    }
    private void UpdateOverlay()
    {
      if (mInvalidateOverlay)
      {
        if (mCachedOverlay != null)
          mCachedOverlay.Dispose();

        mCachedOverlay = new Bitmap(ClientSize.Width, ClientSize.Height);

        using (Graphics graphics = Graphics.FromImage(mCachedOverlay))
        {
          graphics.SmoothingMode = SmoothingMode.HighQuality;
          graphics.CompositingQuality = CompositingQuality.HighQuality;
          graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
          graphics.InterpolationMode = InterpolationMode.High;
          graphics.PixelOffsetMode = PixelOffsetMode.Half;

          DrawOverlay(graphics, new Rectangle(Point.Empty, ClientRectangle.Size));
        }

        mInvalidateOverlay = false;
      }
    }

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

    //private float mTresholdValue = 40.0f;
    //private float mTresholdMargin = 10.0f;

    //private float mTresholdLeftWarningMargin = 10.0f;
    //private float mTresholdRightWarningMargin = 20.0f;
    //private float mTresholdLeftErrorMargin = 5.0f;
    //private float mTresholdRightErrorMargin = 10.0f;
    //private int mDigitalDigitCount = 6;
    //private int mDigitalPrecision = 0;
    private int mMajorGrid = 5;
    private int mMinorGrid = 2;

    private string mUnit = string.Empty;

    private Color mInnerColor = SystemColors.Control;
    # endregion

    # region Overlay Cache
    private Bitmap mCachedOverlay = null;
    private bool mInvalidateOverlay = true;
    # endregion


    # region Cached Metrics
    private const float mMargin = 5.0f;

    //private float mRimWidth;
    //private float mInnerRimWidth;
    //private float mTresholdWidth;
    //private float mScaleGapWidth;

    private RectangleF mClientRegion;
    private RectangleF mGaugeRegion;
    private RectangleF mTitleRect;
    private RectangleF mValueRect;
    private RectangleF mScaleRegion;
    //private RectangleF mGaugeRegion;
    //private RectangleF mRimOutlineRect;
    //private RectangleF mRimOutlineThickRect;
    //private RectangleF mInnerRimRect;
    //private RectangleF mScaleRect;
    //private RectangleF mDigitalBoardOverlayRect;

    private const float mGaugeAngleStart = 135.0f;
    private const float mGaugeAngleEnd = 405.0f;
    # endregion
  }
}
