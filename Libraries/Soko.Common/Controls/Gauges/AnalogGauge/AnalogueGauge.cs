
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

namespace Soko.Common.Controls
{
  public partial class AnalogueGauge : Control
  {
    public AnalogueGauge()
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
      GnerateDigit();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (mCachedBackground != null)
          mCachedBackground.Dispose();
        if (mPointerShapeBrush != null)
          mPointerShapeBrush.Dispose();
        if (mPointerOverlayBrush != null)
          mPointerOverlayBrush.Dispose();
        if (mOverlayBrush != null)
          mOverlayBrush.Dispose();
        if (mOverlayInnerBrush != null)
          mOverlayInnerBrush.Dispose();
      }
      base.Dispose(disposing);
    }

    # region Properties

    public object Tag1 = null;
    public object Tag2 = null;

    [Category("Appearance")]
    [Description("Text displayed on gauge.")]
    [DefaultValue("")]
    [Bindable(true)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override string Text
    {
      set
      {
        base.Text = value;
        InvalidateBackground();
      }
      get
      {
        return base.Text;
      }
    }

    [Category("Appearance")]
    [Description("Displayed unit name.")]
    [DefaultValue("")]
    [Bindable(true)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public string UnitName
    {
      set
      {
        mUnitName = value;
        InvalidateBackground();
      }
      get
      {
        return mUnitName;
      }
    }

    [Category("Attributes")]
    [Description("Current displayed value.")]
    [DefaultValue(0.0)]
    [Bindable(true)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
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
          this.Invalidate();
        }
        else
        {
          if (_InvalidationRequestFromRangeUpdate)
          {
            _InvalidationRequestFromRangeUpdate = false;
            this.Invalidate();
          }
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
    [Bindable(true)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public double MinValue
    {
      set
      {
        if (mMinimumValue != value)
        {
          mMinimumValue = value;

          if (value > MaxValue)
            MaxValue = value + 1;
          InvalidateBackground();
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
    [Bindable(true)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public double MaxValue
    {
      set
      {
        if (mMaximumValue != value)
        {
          mMaximumValue = value;

          if (value < MinValue)
            MinValue = value - 1;
          InvalidateBackground();
          this.Invalidate();
        }
      }
      get
      {
        return mMaximumValue;
      }
    }

    [Category("Threshold")]
    [DisplayName("Recommended Value")]
    [Description("Recommended value the indication should be around.")]
    [DefaultValue(0.0)]
    [Bindable(true)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public double TresholdValue
    {
      set
      {
        if (mTresholdValue != value)
        {
          mTresholdValue = (float)value;
          InvalidateBackground();
        }
      }
      get
      {
        return mTresholdValue;
      }

    }

    [Category("Threshold")]
    [DisplayName("Value Range")]
    [Description("Span size around the recommended value.")]
    [DefaultValue(0.0)]
    [Bindable(true)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public double TresholdMargin
    {
      set
      {
        if (value < 0)
          throw new Exception("Invalid value.");

        if (mTresholdMargin != value)
        {
          mTresholdMargin = (float)value;
          InvalidateBackground();
        }
      }
      get
      {
        return mTresholdMargin;
      }
    }

    [Category("Threshold")]
    [DisplayName("Warning Left Range")]
    [Description("Span size around the warning value.")]
    [DefaultValue(0.0)]
    [Bindable(true)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public double TresholdLeftWarningMargin
    {
      set
      {
        if (value < 0)
          throw new Exception("Invalid value.");

        if (mTresholdLeftWarningMargin != value)
        {
          mTresholdLeftWarningMargin = (float)value;
          InvalidateBackground();
        }
      }
      get { return mTresholdLeftWarningMargin; }
    }

    [Category("Threshold")]
    [DisplayName("Warning Right Range")]
    [Description("Span size around the warning value.")]
    [DefaultValue(0.0)]
    [Bindable(true)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public double TresholdRightWarningMargin
    {
      set
      {
        if (value < 0)
          throw new Exception("Invalid value.");

        if (mTresholdRightWarningMargin != value)
        {
          mTresholdRightWarningMargin = (float)value;
          InvalidateBackground();
        }
      }
      get { return mTresholdRightWarningMargin; }
    }

    [Category("Threshold")]
    [DisplayName("Error Left Range")]
    [Description("Span size around the error value.")]
    [DefaultValue(0.0)]
    [Bindable(true)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public double TresholdLeftErrorMargin
    {
      set
      {
        if (value < 0)
          throw new Exception("Invalid value.");

        if (mTresholdLeftErrorMargin != value)
        {
          mTresholdLeftErrorMargin = (float)value;
          InvalidateBackground();
        }
      }
      get { return mTresholdLeftErrorMargin; }
    }

    [Category("Threshold")]
    [DisplayName("Error Right Range")]
    [Description("Span size around the error value.")]
    [DefaultValue(0.0)]
    [Bindable(true)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public double TresholdRightErrorMargin
    {
      set
      {
        if (value < 0)
          throw new Exception("Invalid value.");

        if (mTresholdRightErrorMargin != value)
        {
          mTresholdRightErrorMargin = (float)value;
          InvalidateBackground();
        }
      }
      get { return mTresholdRightErrorMargin; }
    }

    [Category("Digital Display")]
    [DisplayName("Digits")]
    [Description("Number of digits displayed on digital display under the gauge.")]
    [DefaultValue(6)]
    [Bindable(true)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int DigitalDigitCount
    {
      set
      {
        if (value < 0)
          throw new Exception("Invalid value.");

        if (mDigitalDigitCount != value)
        {
          mDigitalDigitCount = value;
          this.Invalidate();
        }
      }
      get { return mDigitalDigitCount; }
    }

    [Category("Digital Display")]
    [DisplayName("Precision")]
    [Description("Precision of displayed number on digital display under the gauge.")]
    [DefaultValue(0)]
    [Bindable(true)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int DigitalPrecision
    {
      set
      {
        if (value < 0)
          throw new Exception("Invalid value.");

        if (mDigitalPrecision != value)
        {
          mDigitalPrecision = value;
          this.Invalidate();
        }
      }
      get { return mDigitalPrecision; }
    }

    [Category("Scale")]
    [DisplayName("Major")]
    [Description("Number of major divisions on the scale.")]
    [DefaultValue(10)]
    [Bindable(true)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int MajorGrid
    {
      set
      {
        if (value < 1)
          throw new Exception("Invalid value.");

        if (mMajorGrid != value)
        {
          mMajorGrid = value;
          InvalidateBackground();
        }
      }
      get { return mMajorGrid; }
    }

    [Category("Scale")]
    [DisplayName("Minor")]
    [Description("Number of minor divisions on the scale.")]
    [DefaultValue(5)]
    [Bindable(true)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int MinorGrid
    {
      set
      {
        if (value < 1)
          throw new Exception("Invalid value.");

        if (mMinorGrid != value)
        {
          mMinorGrid = value;
          InvalidateBackground();
        }
      }
      get { return mMinorGrid; }
    }

    [Category("Appearance")]
    [Description("Inner background color.")]
    [DefaultValue(typeof(Color), "Control")]
    [Bindable(true)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color InnerColor
    {
      set
      {
        if (this.mInnerColor != value)
        {
          this.mInnerColor = value;
          if (this.Parent != null)
            Parent.Invalidate(this.Bounds, true);

          InvalidateBackground();
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

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);

      Graphics graphics = e.Graphics;

      graphics.SmoothingMode = SmoothingMode.HighQuality;
      graphics.CompositingQuality = CompositingQuality.HighQuality;
      graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
      graphics.InterpolationMode = InterpolationMode.High;
      graphics.PixelOffsetMode = PixelOffsetMode.Half;

      DrawDigitNumber(graphics);
      DrawPointer(graphics);
      DrawOverlay(graphics);
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

      UpdateBackground();

      graphics.DrawImage(mCachedBackground, ClientRectangle);
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

      int size = Math.Min(Width, Height);
      if (size < 64)
        size = 64;

      Width = size;
      Height = size;

      InvalidateBackground();
    }

    public void SetExpectedValueWithoutInvalidation(double expValue, double validRange)
    {
      SetExpectedValueWithoutInvalidation((float)expValue, (float)validRange);
    }

    private bool _InvalidationRequestFromRangeUpdate = false;

    public void SetExpectedValueWithoutInvalidation(float expValue, float validRange)
    {
      mTresholdValue = expValue;
      mTresholdMargin = validRange * 0.4f;
      mTresholdLeftWarningMargin = validRange * 0.3f;
      mTresholdRightWarningMargin = validRange * 0.3f;
      mTresholdLeftErrorMargin = (float)mMaximumValue;
      mTresholdRightErrorMargin = (float)mMaximumValue;

      mInvalidateBackground = true;
      mInvalidatePointer = true;
      mInvalidateOverlay = true;
      _InvalidationRequestFromRangeUpdate = true;
    }

    public void SetExpectedValue(float expValue, float validRange)
    {

      SetExpectedValueWithoutInvalidation(expValue, validRange);
      this.Invalidate();
    }

    # region Background Drawing
    private void DrawBackground(Graphics graphics, Rectangle drawingRegion)
    {
      DrawBackground(graphics, new RectangleF(drawingRegion.X, drawingRegion.Y, drawingRegion.Width, drawingRegion.Height));
    }
    private void DrawBackground(Graphics graphics, RectangleF drawingRegion)
    {
      mGaugeRegion = RectangleF.Inflate(drawingRegion, -mMargin, -mMargin);
      if (mGaugeRegion.IsEmpty)
        return;

      mRimWidth = mGaugeRegion.Width * 0.03f;
      mInnerRimWidth = mGaugeRegion.Width * 0.025f;
      mTresholdWidth = mGaugeRegion.Width * 0.020f;

      // Draw Background
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

      // Draw Unit
      DrawUnit(graphics, mScaleRect);

      // Draw Digital Board
      DrawDigitalBoard(graphics, mScaleRect);
    }
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

      float majorScaleAngleStep = (mGaugeAngleEnd - mGaugeAngleStart) / mMajorGrid;
      float majorScaleValueStep = (float)(MaxValue - MinValue) / mMajorGrid;

      Color majorScaleColor = Color.Black;
      Color minorScaleColor = Color.Black;
      Pen majorScalePen = new Pen(majorScaleColor, majorScaleWidth);
      Pen minorScalePen = new Pen(minorScaleColor, minorScaleWidth);

      Brush scaleLabelBrush = new SolidBrush(this.ForeColor);

      StringFormat scaleLabelFormat = new StringFormat(StringFormat.GenericTypographic)
      {
        Alignment = StringAlignment.Center,
        LineAlignment = StringAlignment.Center
      };

      Font scaleFont = new Font(this.Font.FontFamily, scaleFontHeight, this.Font.Style);

      for (int majorScale = 0; majorScale <= mMajorGrid; ++majorScale)
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
        if (majorScale < mMajorGrid)
        {
          for (int minorScale = 1; minorScale < mMinorGrid; ++minorScale)
          {
            float minorAngle = majorAngle + (float)Math.PI * (minorScale * majorScaleAngleStep / mMinorGrid) / 180.0f;
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
    private void DrawText(Graphics graphics, RectangleF boardRect)
    {
      float textFontSize = (boardRect.Right + boardRect.Left) * 0.05f;
      float textX = (boardRect.Right + boardRect.Left) * 0.5f;
      float textY = (boardRect.Bottom + boardRect.Top) * 0.72f;

      StringFormat textFormat = new StringFormat(StringFormat.GenericTypographic)
      {
        Alignment = StringAlignment.Center,
        LineAlignment = StringAlignment.Center
      };

      Font textFont = new Font(this.Font.FontFamily, textFontSize, this.Font.Style);

      graphics.DrawString(Text, textFont, new SolidBrush(this.ForeColor), textX, textY, textFormat);

      textFormat.Dispose();
      textFont.Dispose();
    }
    private void DrawUnit(Graphics graphics, RectangleF boardRect)
    {
      float textFontSize = (boardRect.Right + boardRect.Left) * 0.035f;
      float textX = (boardRect.Right + boardRect.Left) * 0.5f;
      float textY = (boardRect.Bottom + boardRect.Top) * 0.65f;

      StringFormat textFormat = new StringFormat(StringFormat.GenericTypographic)
      {
        Alignment = StringAlignment.Center,
        LineAlignment = StringAlignment.Center
      };

      Font textFont = new Font(this.Font.FontFamily, textFontSize, this.Font.Style);

      graphics.DrawString(UnitName, textFont, new SolidBrush(this.ForeColor), textX, textY, textFormat);

      textFormat.Dispose();
      textFont.Dispose();
    }

    private void DrawDigitalBoard(Graphics graphics, RectangleF boardRect)
    {
      mDigitalBoardBackgroundRect = RectangleF.Empty;
      mDigitalBoardBackgroundRect.X = (boardRect.Right + boardRect.Left) * 0.5f;
      mDigitalBoardBackgroundRect.Y = (boardRect.Bottom + boardRect.Top) * 0.835f;
      mDigitalBoardBackgroundRect = RectangleF.Inflate(mDigitalBoardBackgroundRect,
          boardRect.Width * 0.23f, boardRect.Height * 0.065f);

      Color boardBackgroundColor = Color.FromArgb(30, Color.Gray);
      using (Brush boardBackgroundBrush = new SolidBrush(boardBackgroundColor))
        graphics.FillRectangle(boardBackgroundBrush, mDigitalBoardBackgroundRect);
    }
    # endregion

    # region Digital Number Drawing
    private void GnerateDigit()
    {
      float gridWidth = 43.0f;
      float gridHeight = 80.0f;
      float elementWidth = 8.0f;
      float halfHeight = gridHeight / 2;
      float halfWidth = elementWidth / 2;

      mDigitSegments = new PointF[][]
            {
                new PointF[]
                {
                    new PointF(elementWidth + 1, 0),
                    new PointF(gridWidth - elementWidth - 1, 0),
                    new PointF(gridWidth - halfWidth - 1, halfWidth),
                    new PointF(gridWidth - elementWidth - 1, elementWidth),
                    new PointF(elementWidth + 1, elementWidth),
                    new PointF(halfWidth + 1, halfWidth),
                },

                new PointF[]
                {
                    new PointF(gridWidth - elementWidth, elementWidth + 1),
                    new PointF(gridWidth - halfWidth, halfWidth + 1),
                    new PointF(gridWidth, elementWidth + 1),
                    new PointF(gridWidth, halfHeight - 1),
                    new PointF(gridWidth - 4, halfHeight - 1),
                    new PointF(gridWidth - elementWidth, halfHeight - halfWidth - 1),
                },

                new PointF[]
                {
                    new PointF(gridWidth - elementWidth, halfHeight + halfWidth + 1),
                    new PointF(gridWidth - 4, halfHeight + 1),
                    new PointF(gridWidth, halfHeight + 1),
                    new PointF(gridWidth, gridHeight - elementWidth - 1),
                    new PointF(gridWidth - halfWidth, gridHeight - halfWidth - 1),
                    new PointF(gridWidth - elementWidth, gridHeight - elementWidth - 1),
                },

                new PointF[]
                {
                    new PointF(elementWidth + 1, gridHeight - elementWidth),
                    new PointF(gridWidth - elementWidth - 1, gridHeight - elementWidth),
                    new PointF(gridWidth - halfWidth - 1, gridHeight - halfWidth),
                    new PointF(gridWidth - elementWidth - 1, gridHeight),
                    new PointF(elementWidth + 1, gridHeight),
                    new PointF(halfWidth + 1, gridHeight - halfWidth),
                },

                new PointF[]
                {
                    new PointF(0, halfHeight + 1),
                    new PointF(4, halfHeight + 1),
                    new PointF(elementWidth, halfHeight + halfWidth + 1),
                    new PointF(elementWidth, gridHeight - elementWidth - 1),
                    new PointF(halfWidth, gridHeight - halfWidth - 1),
                    new PointF(0, gridHeight - elementWidth - 1),
                },

                new PointF[]
                {
                    new PointF(0, elementWidth + 1),
                    new PointF(halfWidth, halfWidth + 1),
                    new PointF(elementWidth, elementWidth + 1),
                    new PointF(elementWidth, halfHeight - halfWidth - 1),
                    new PointF(4, halfHeight - 1),
                    new PointF(0, halfHeight - 1),
                },

                new PointF[]
                {
                    new PointF(elementWidth + 1, halfHeight - halfWidth),
                    new PointF(gridWidth - elementWidth - 1, halfHeight - halfWidth),
                    new PointF(gridWidth - 5, halfHeight),
                    new PointF(gridWidth - elementWidth - 1, halfHeight + halfWidth),
                    new PointF(elementWidth + 1, halfHeight + halfWidth),
                    new PointF(5, halfHeight),
                },

                new PointF[]
                {
                    new PointF(gridWidth, gridHeight - 4),
                    new PointF(gridWidth + elementWidth, gridHeight - 4),
                    new PointF(gridWidth + elementWidth, gridHeight + elementWidth - 4),
                    new PointF(gridWidth, gridHeight + elementWidth - 4),
                }
            };

      float minX = float.MaxValue, minY = float.MaxValue, maxX = -float.MaxValue, maxY = -float.MaxValue;
      foreach (PointF[] segment in mDigitSegments)
      {
        foreach (PointF point in segment)
        {
          minX = Math.Min(minX, point.X);
          minY = Math.Min(minY, point.Y);
          maxX = Math.Max(maxX, point.X);
          maxY = Math.Max(maxY, point.Y);
        }
      }

      float width = maxX - minX;
      float height = maxY - minY;
      float scale = height;
      float shear = 0.1f;

      for (int segmentIndex = 0; segmentIndex < mDigitSegments.Length; ++segmentIndex)
      {
        for (int pointIndex = 0; pointIndex < mDigitSegments[segmentIndex].Length; ++pointIndex)
        {
          PointF point = mDigitSegments[segmentIndex][pointIndex];

          point.X = ((point.X - minX) - width * 0.5f) / scale;
          point.Y = ((point.Y - minY)) / scale;

          point.X -= (point.Y - 0.5f) * shear;
          point.Y -= 1.0f;

          mDigitSegments[segmentIndex][pointIndex] = point;
        }
      }

      mDigitWidth = (maxX - minY) / scale;
    }
    private void DrawDigit(Graphics graphics, int segments, float x, float y, float height, float stretchX)
    {
      Matrix transform = graphics.Transform;

      graphics.TranslateTransform(x, y);
      graphics.ScaleTransform(height * stretchX, height);

      using (Brush brush = new SolidBrush(ForeColor))
        for (int i = 0; i < mDigitSegments.Length; ++i)
          if ((segments & (1 << i)) != 0)
            graphics.FillPolygon(brush, mDigitSegments[i]);

      graphics.Transform = transform;
    }
    private void DrawDigitNumber(Graphics graphics)
    {
      float margin = mDigitalBoardBackgroundRect.Height * 0.1f;
      RectangleF displayArea = RectangleF.Inflate(mDigitalBoardBackgroundRect, -margin, -margin);

      float digitWidth = mDigitWidth * displayArea.Height;
      float digitSpace = digitWidth * 0.15f;
      float digitOffset = digitWidth + digitSpace;
      float digitAreaWidth = digitOffset * mDigitalDigitCount - digitSpace;
      float digitStrech = 1.0f;

      if ((digitAreaWidth - displayArea.Width) > 0.0f)
        // Required display area is greater than available space.
        // We need to shrink the digits.
        digitStrech = displayArea.Width / digitAreaWidth;
      else
        // Available space is greater than required, so, center displayed value.
        displayArea = RectangleF.Inflate(displayArea, (digitAreaWidth - displayArea.Width) * 0.5f, 0.0f);

      string valueFormat = "0";
      if (mDigitalPrecision > 0)
        valueFormat += "." + new string('0', mDigitalPrecision);
      string valueText = Math.Round(Value, mDigitalPrecision).ToString(valueFormat, CultureInfo.InvariantCulture);
      int valueCodeIndex = valueText.Length - 1;
      bool showDot = false;

      float digitX = displayArea.Right - 0.5f * digitWidth * digitStrech;
      float digitY = displayArea.Bottom;
      for (int digitIndex = 0; digitIndex < mDigitalDigitCount; --valueCodeIndex)
      {
        int codePoint = (valueCodeIndex >= 0) ? valueText[valueCodeIndex] : 0;
        int code = 0;

        if (codePoint == '.')
        {
          showDot = true;
          continue;
        }

        switch (codePoint)
        {
          case '-': code = 0x40; break;
          case '0': code = 0x3F; break;
          case '1': code = 0x06; break;
          case '2': code = 0x5B; break;
          case '3': code = 0x4F; break;
          case '4': code = 0x66; break;
          case '5': code = 0x6D; break;
          case '6': code = 0x7D; break;
          case '7': code = 0x07; break;
          case '8': code = 0x7F; break;
          case '9': code = 0x6F; break;
        }

        if (showDot)
        {
          code |= 0x80;
          showDot = false;
        }

        if (code == 0)
          break;

        DrawDigit(graphics, code, digitX, digitY, displayArea.Height, digitStrech);

        ++digitIndex;
        digitX -= digitOffset * digitStrech;
      }
    }
    # endregion

    # region Pointer Drawing
    private void DrawPointer(Graphics graphics)
    {
      float pointerMargin = mScaleRect.Width * 0.075f;
      RectangleF displayRect = RectangleF.Inflate(mScaleRect, -pointerMargin, -pointerMargin);

      if (mInvalidatePointer)
      {
        float radius = displayRect.Width * 0.5f;
        float midRadius = radius * 0.24f;
        float tipWidth = radius * 0.020f;
        float midWidth = radius * 0.085f;

        mPointerShape = new PointF[]
                {
                    new PointF(radius, -tipWidth * 0.5f),
                    new PointF(radius, tipWidth * 0.5f),
                    new PointF(midRadius, midWidth),
                    new PointF(0.0f, 0.0f),
                    new PointF(midRadius, -midWidth),
                };

        mPointerOverlay = new PointF[]
                {
                    new PointF(radius, tipWidth * 0.5f),
                    new PointF(midRadius, midWidth),
                    new PointF(0.0f, 0.0f),
                };

        if (mPointerShapeBrush != null)
          mPointerShapeBrush.Dispose();
        if (mPointerOverlayBrush != null)
          mPointerOverlayBrush.Dispose();

        mPointerShapeBrush = new SolidBrush(Color.Black);
        mPointerOverlayBrush = new LinearGradientBrush(mPointerOverlay[0], mPointerOverlay[2],
            Color.SlateGray, Color.Black);

        mInvalidatePointer = false;
      }

      Matrix transform = graphics.Transform;

      graphics.TranslateTransform(
          (displayRect.Right + displayRect.Left) * 0.5f,
          (displayRect.Bottom + displayRect.Top) * 0.5f);

      graphics.RotateTransform(GetAngleFromValue((float)Value, 5.0f));

      graphics.FillPolygon(mPointerShapeBrush, mPointerShape);
      graphics.FillPolygon(mPointerOverlayBrush, mPointerOverlay);

      graphics.Transform = transform;
    }
    # endregion

    # region Overlay Drawing
    private void DrawOverlay(Graphics graphics)
    {
      float centerX = (mScaleRect.Right + mScaleRect.Left) * 0.5f;
      float centerY = (mScaleRect.Bottom + mScaleRect.Top) * 0.5f;

      if (mInvalidateOverlay)
      {
        float overlayRadius = mScaleRect.Width * 0.5f * 0.24f;
        float overlayInnerRadius = mScaleRect.Width * 0.5f * 0.17f;

        RectangleF brushRect = mScaleRect;
        brushRect.Offset(-centerX, -centerY);

        mOverlayRect = RectangleF.Inflate(RectangleF.Empty, overlayRadius, overlayRadius);
        mOverlayInnerRect = RectangleF.Inflate(RectangleF.Empty, overlayInnerRadius, overlayInnerRadius);

        if (mOverlayBrush != null)
          mOverlayBrush.Dispose();
        if (mOverlayInnerBrush != null)
          mOverlayInnerBrush.Dispose();

        mOverlayBrush = new LinearGradientBrush(brushRect, Color.Black, Color.FromArgb(100, this.BackColor), LinearGradientMode.Vertical);
        mOverlayInnerBrush = new LinearGradientBrush(brushRect, Color.SlateGray, Color.Black, LinearGradientMode.ForwardDiagonal);

        mInvalidateOverlay = false;
      }

      Matrix transform = graphics.Transform;
      graphics.TranslateTransform(centerX, centerY);
      graphics.FillEllipse(mOverlayBrush, mOverlayRect);
      graphics.FillEllipse(mOverlayInnerBrush, mOverlayInnerRect);
      graphics.Transform = transform;
    }
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

    private void InvalidateBackground()
    {
      mInvalidateBackground = true;
      mInvalidatePointer = true;
      mInvalidateOverlay = true;

      this.Invalidate();
    }
    private void UpdateBackground()
    {
      if (mInvalidateBackground)
      {
        if (mCachedBackground != null)
          mCachedBackground.Dispose();

        mCachedBackground = new Bitmap(ClientSize.Width, ClientSize.Height);

        using (Graphics graphics = Graphics.FromImage(mCachedBackground))
        {
          graphics.SmoothingMode = SmoothingMode.HighQuality;
          graphics.CompositingQuality = CompositingQuality.HighQuality;
          graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
          graphics.InterpolationMode = InterpolationMode.High;
          graphics.PixelOffsetMode = PixelOffsetMode.Half;

          DrawBackground(graphics, new Rectangle(Point.Empty, ClientRectangle.Size));
        }

        mInvalidateBackground = false;
      }
    }

    # region User Defined Properties
    private double mValue = 0.0;
    private double mMinimumValue = 0.0;
    private double mMaximumValue = 100.0;

    private float mTresholdValue = 0.0f;
    private float mTresholdMargin = 0.0f;

    private float mTresholdLeftWarningMargin = 0.0f;
    private float mTresholdRightWarningMargin = 0.0f;
    private float mTresholdLeftErrorMargin = 0.0f;
    private float mTresholdRightErrorMargin = 0.0f;
    private int mDigitalDigitCount = 6;
    private int mDigitalPrecision = 0;
    private int mMajorGrid = 10;
    private int mMinorGrid = 5;
    private string mUnitName = "Bar";

    private Color mInnerColor = SystemColors.Control;
    # endregion

    # region Background Cache
    private Bitmap mCachedBackground = null;
    private bool mInvalidateBackground = true;
    # endregion

    # region Digital Display Segments
    PointF[][] mDigitSegments = null;
    float mDigitWidth;
    # endregion

    # region Pointer Cache
    private PointF[] mPointerShape;
    private PointF[] mPointerOverlay;
    private Brush mPointerShapeBrush;
    private Brush mPointerOverlayBrush;
    private bool mInvalidatePointer = true;
    # endregion

    # region Overlay Cache
    private RectangleF mOverlayRect;
    private RectangleF mOverlayInnerRect;
    private Brush mOverlayBrush;
    private Brush mOverlayInnerBrush;
    private bool mInvalidateOverlay = true;
    # endregion

    # region Cached Metrics
    private const float mMargin = 5.0f;

    private float mRimWidth;
    private float mInnerRimWidth;
    private float mTresholdWidth;
    private float mScaleGapWidth;

    private RectangleF mGaugeRegion;
    private RectangleF mRimOutlineRect;
    private RectangleF mRimOutlineThickRect;
    private RectangleF mInnerRimRect;
    private RectangleF mScaleRect;
    private RectangleF mDigitalBoardBackgroundRect;

    private const float mGaugeAngleStart = 135.0f;
    private const float mGaugeAngleEnd = 405.0f;
    # endregion
  }
}
