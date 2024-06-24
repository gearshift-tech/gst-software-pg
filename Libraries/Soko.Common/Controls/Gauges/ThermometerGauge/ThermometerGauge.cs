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
    public class ThermometerGauge : Control
    {
        public ThermometerGauge()
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
                //if (mPointerShapeBrush != null)
                //    mPointerShapeBrush.Dispose();
                //if (mPointerOverlayBrush != null)
                //    mPointerOverlayBrush.Dispose();
                //if (mOverlayBrush != null)
                //    mOverlayBrush.Dispose();
                //if (mOverlayInnerBrush != null)
                //    mOverlayInnerBrush.Dispose();
            }
            base.Dispose(disposing);
        }

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
        [Description("Name of the unit.")]
        [DefaultValue(0.0)]
        public string Unit
        {
            set
            {
                mUnit = value;
                //InvalidateOverlay();
                this.Invalidate();
            }
            get
            {
                return mUnit;
            }
        }

        [Category("Attributes")]
        [Description("Current displayed value.")]
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

        /*
        [Category("Threshold")]
        [DisplayName("Recommended Value")]
        [Description("Recommended value the indication should be around.")]
        [DefaultValue(0.0)]
        public double TresholdValue
        {
            set
            {
                if (mTresholdValue != value)
                {
                    mTresholdValue = (float)value;
                    InvalidateOverlay();
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
        public double TresholdMargin
        {
            set
            {
                if (value < 0)
                    throw new Exception("Invalid value.");

                if (mTresholdMargin != value)
                {
                    mTresholdMargin = (float)value;
                    InvalidateOverlay();
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
        public double TresholdLeftWarningMargin
        {
            set
            {
                if (value < 0)
                    throw new Exception("Invalid value.");

                if (mTresholdLeftWarningMargin != value)
                {
                    mTresholdLeftWarningMargin = (float)value;
                    InvalidateOverlay();
                }
            }
            get { return mTresholdLeftWarningMargin; }
        }
        
        [Category("Threshold")]
        [DisplayName("Warning Right Range")]
        [Description("Span size around the warning value.")]
        [DefaultValue(0.0)]
        public double TresholdRightWarningMargin
        {
            set
            {
                if (value < 0)
                    throw new Exception("Invalid value.");

                if (mTresholdRightWarningMargin != value)
                {
                    mTresholdRightWarningMargin = (float)value;
                    InvalidateOverlay();
                }
            }
            get { return mTresholdRightWarningMargin; }
        }
        
        [Category("Threshold")]
        [DisplayName("Error Left Range")]
        [Description("Span size around the error value.")]
        [DefaultValue(0.0)]
        public double TresholdLeftErrorMargin
        {
            set
            {
                if (value < 0)
                    throw new Exception("Invalid value.");

                if (mTresholdLeftErrorMargin != value)
                {
                    mTresholdLeftErrorMargin = (float)value;
                    InvalidateOverlay();
                }
            }
            get { return mTresholdLeftErrorMargin; }
        }

        [Category("Threshold")]
        [DisplayName("Error Right Range")]
        [Description("Span size around the error value.")]
        [DefaultValue(0.0)]
        public double TresholdRightErrorMargin
        {
            set
            {
                if (value < 0)
                    throw new Exception("Invalid value.");

                if (mTresholdRightErrorMargin != value)
                {
                    mTresholdRightErrorMargin = (float)value;
                    InvalidateOverlay();
                }
            }
            get { return mTresholdRightErrorMargin; }
        }

        [Category("Digital Display")]
        [DisplayName("Digits")]
        [Description("Number of digits displayed on digital display under the gauge.")]
        [DefaultValue(6)]
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
        */

        [Category("Scale")]
        [DisplayName("Major")]
        [Description("Number of major divisions on the scale.")]
        [DefaultValue(10)]
        public int MajorGrid
        {
            set
            {
                if (value < 1)
                    throw new Exception("Invalid value.");

                if (mMajorGrid != value)
                {
                    mMajorGrid = value;
                    InvalidateOverlay();
                }
            }
            get { return mMajorGrid; }
        }

        [Category("Scale")]
        [DisplayName("Minor")]
        [Description("Number of minor divisions on the scale.")]
        [DefaultValue(5)]
        public int MinorGrid
        {
            set
            {
                if (value < 1)
                    throw new Exception("Invalid value.");

                if (mMinorGrid != value)
                {
                    mMinorGrid = value;
                    InvalidateOverlay();
                }
            }
            get { return mMinorGrid; }
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

        [Category("Appearance")]
        [Description("If control content should keep constant aspect ratio.")]
        [DefaultValue(true)]
        public bool KeepContentAspect
        {
          set
          {
            if (mKeepContentAspect != value)
            {
              mKeepContentAspect = value;
              UpdateContentRegion();
              if (this.Parent != null)
                Parent.Invalidate(this.Bounds, true);

              InvalidateOverlay();
            }
          }
          get
          {
            return mKeepContentAspect;
          }
        }

        [Category("Appearance")]
        [Description("Control's content aspect ratio")]
        [DefaultValue(0.4f)]
        public float ContentAspectRatio
        {
          set
          {
            if (mContentAspectRatio != value)
            {
              mContentAspectRatio = value;
              UpdateContentRegion();
              if (this.Parent != null)
                Parent.Invalidate(this.Bounds, true);

              InvalidateOverlay();
            }
          }
          get
          {
            return mContentAspectRatio;
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

            //DrawDigitNumber(graphics);
            //DrawPointer(graphics);
            //DrawOverlay(graphics);

            DrawValue(graphics);

            graphics.DrawImage(mCachedOverlay, ClientRectangle);
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

            if (Width < 80)
                Width = 80;
            if (Height < 64)
                Height = 64;
            UpdateContentRegion();
            InvalidateOverlay();
        }

        # region Overlay Drawing
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

            float mMajorScaleHeight = mGaugeRegion.Height * 0.04f;
            float mMinorScaleHeight = mGaugeRegion.Height * 0.03f;

            mTitleRect = new RectangleF(
                mGaugeRegion.X, mClientRegion.Y + mTitleTopMargin,
                mGaugeRegion.Width, mTitleHeight);

            float bottomDiagonal = (mGaugeRegion.Width - 2.0f * (mScaleWidth + mScaleOffset));
            float bulbDiameter = bottomDiagonal * 0.8f;
            float gaugeWidth = bottomDiagonal * 0.5f;
            float topDistance = gaugeWidth * 0.8f;

            mGaugeRegion.Y += topDistance;
            mGaugeRegion.Height -= bottomDiagonal + topDistance;

            mGaugeRegion.Y += mTitleRect.Height + mTitleTopMargin + mTitleBottomMargin;
            mGaugeRegion.Height -= mTitleRect.Height + mTitleTopMargin + mTitleBottomMargin;

            mValueRect = new RectangleF(
                mGaugeRegion.X, mClientRegion.Bottom - mValueBottomMargin - mValueHeight,
                mGaugeRegion.Width, mValueHeight);
            
            mGaugeRegion.Height -= mValueRect.Height + mValueTopMargin + mValueBottomMargin;

            mLeftScaleRegion = new RectangleF(
                mGaugeRegion.Left, mGaugeRegion.Y, mScaleWidth, mGaugeRegion.Height);

            mGaugeRegion.X += mScaleWidth + mScaleOffset;
            mGaugeRegion.Width -= mScaleWidth + mScaleOffset;

            mRightScaleRegion = new RectangleF(
                mGaugeRegion.Right - mScaleWidth, mGaugeRegion.Y, mScaleWidth, mGaugeRegion.Height);

            mGaugeRegion.Width -= mScaleWidth + mScaleOffset;

            mRightScaleRegion.Y = mGaugeRegion.Y;
            mRightScaleRegion.Height = mGaugeRegion.Height;

            mGaugeRegion.X += (mGaugeRegion.Width - gaugeWidth) * 0.5f;
            mGaugeRegion.Width -= (mGaugeRegion.Width - gaugeWidth) * 0.5f;

            mTopRegion = new RectangleF(
                mGaugeRegion.X, mGaugeRegion.Y - mGaugeRegion.Width * 0.5f,
                mGaugeRegion.Width, mGaugeRegion.Width);
            PointF bulbCenter = new PointF(
                mGaugeRegion.X + mGaugeRegion.Width * 0.5f,
                mGaugeRegion.Bottom + mGaugeRegion.Width * 0.5f);
            mBulbRegion = new RectangleF(
                bulbCenter.X - mGaugeRegion.Width * 0.70710678f,
                bulbCenter.Y - mGaugeRegion.Width * 0.70710678f,
                mGaugeRegion.Width * 0.70710678f * 2.0f,
                mGaugeRegion.Width * 0.70710678f * 2.0f);
            mBulbJoinRegion = new RectangleF(
                mGaugeRegion.X, mGaugeRegion.Bottom - mGaugeRegion.Width * (1.0f - 0.70710678f) * 0.5f,
                mGaugeRegion.Width, mGaugeRegion.Width * (1.0f - 0.70710678f) * 0.5f);

            mGaugeRegion.Height -= mBulbJoinRegion.Height;
            mLeftScaleRegion.Height -= mBulbJoinRegion.Height;
            mRightScaleRegion.Height -= mBulbJoinRegion.Height;

            
            // Debug regions
            //graphics.FillRectangle(Brushes.Pink, drawingRegion);
            //graphics.FillRectangle(Brushes.Red, mClientRegion);
            //graphics.FillRectangle(Brushes.Green, mGaugeRegion);
            //graphics.FillRectangle(Brushes.Blue, mTitleRect);
            //graphics.FillRectangle(Brushes.Orange, mValueRect);
            //graphics.FillRectangle(Brushes.Lime, mRightScaleRegion);

            //
            {
                Brush titleBrush = new SolidBrush(Color.Black);

        StringFormat titleLabelFormat = new StringFormat(StringFormat.GenericTypographic)
        {
          Alignment = StringAlignment.Center,
          LineAlignment = StringAlignment.Center
        };

        Font titleFont = new Font(this.Font.FontFamily, mTitleHeight, this.Font.Style);

                graphics.DrawString(Text, titleFont, titleBrush,
                    mTitleRect.X + mTitleRect.Width * 0.5f,
                    mTitleRect.Y + mTitleRect.Height * 0.5f, titleLabelFormat);

                titleLabelFormat.Dispose();
                titleFont.Dispose();
                titleBrush.Dispose();
            }

            Brush scaleBrush = new SolidBrush(Color.Black);

      StringFormat scaleLabelFormat = new StringFormat(StringFormat.GenericTypographic)
      {
        Alignment = StringAlignment.Near,
        LineAlignment = StringAlignment.Center
      };

      Font majorScaleFont = new Font(this.Font.FontFamily, mMajorScaleHeight, this.Font.Style);
            Font minorScaleFont = new Font(this.Font.FontFamily, mMinorScaleHeight, this.Font.Style);

            // Draw grid.
            Pen majorGridPen = new Pen(Color.Gray);
            Pen minorGridPen = new Pen(Color.LightGray);

            majorGridPen.Width = mMajorGridWidth;
            minorGridPen.Width = mMinorGridWidth;

            majorGridPen.StartCap = LineCap.Round;
            minorGridPen.StartCap = LineCap.Round;
            majorGridPen.EndCap = LineCap.Round;
            minorGridPen.EndCap = LineCap.Round;

            float majorGridStep = (MajorGrid > 0) ? (mGaugeRegion.Height / MajorGrid) : 0.0f;
            float minorGridMargin = mMinorGridMarginPercentage * mGaugeRegion.Width;
            for (int i = 0; i <= MajorGrid; ++i)
            {
                float majorGridPosition = mGaugeRegion.Top + majorGridStep * i;
                graphics.DrawLine(majorGridPen,
                    mGaugeRegion.Left, majorGridPosition,
                    mGaugeRegion.Left + mGaugeRegion.Width * 0.35f, majorGridPosition);
                graphics.DrawLine(majorGridPen,
                    mGaugeRegion.Right, majorGridPosition,
                    mGaugeRegion.Right - mGaugeRegion.Width * 0.35f, majorGridPosition);

                float fadeOut = 0.0f;// 1.0f - fadeOut;
                
                float labelPosition = majorGridPosition;
                float labelTopMargin = 0.0f;
                float labelBottomMargin = 0.0f;
                if (i == 0)
                {
                    labelPosition += mMajorScaleHeight * 0.5f * fadeOut;
                    labelBottomMargin += mMajorScaleHeight * 0.5f * fadeOut;
                }
                if (i == MajorGrid)
                    labelPosition -= mMajorScaleHeight * 0.5f * fadeOut;
                if (i == MajorGrid - 1)
                    labelBottomMargin += mMajorScaleHeight * 0.5f * fadeOut;
                graphics.DrawString(Math.Round(GetValueAtPosition(majorGridPosition), 0).ToString(),
                    majorScaleFont, scaleBrush, mRightScaleRegion.X, labelPosition, scaleLabelFormat);

                if (i < MajorGrid)
                {
                    float minorGridSize = majorGridStep - labelTopMargin - labelBottomMargin;

                    float minorGridStep = (MinorGrid > 0) ? (majorGridStep / MinorGrid) : 0.0f;
                    float minorGridLabelScale = minorGridSize / majorGridStep;
                    for (int j = 1; j < MinorGrid; ++j)
                    {
                        float minorGridPosition = majorGridPosition + minorGridStep * j;
                        float minorGridLabelPosition = labelPosition + labelTopMargin + (minorGridPosition - majorGridPosition) * minorGridLabelScale;

                        graphics.DrawLine(minorGridPen,
                            mGaugeRegion.Left, minorGridPosition,
                            mGaugeRegion.Left + mGaugeRegion.Width * 0.25f, minorGridPosition);

                        graphics.DrawLine(minorGridPen,
                            mGaugeRegion.Right, minorGridPosition,
                            mGaugeRegion.Right - mGaugeRegion.Width * 0.25f, minorGridPosition);

                        
                        //graphics.DrawLine(minorGridPen,
                        //    mGaugeRegion.Left + minorGridMargin, minorGridPosition,
                        //    mGaugeRegion.Right - minorGridMargin, minorGridPosition);

                        graphics.DrawString(Math.Round(GetValueAtPosition(minorGridPosition), 0).ToString(), minorScaleFont, scaleBrush,
                            mRightScaleRegion.X, minorGridLabelPosition, scaleLabelFormat);
                    }
                }
            }

            majorGridPen.Alignment = PenAlignment.Center;
            graphics.DrawArc(majorGridPen, mTopRegion, -180, 180);
            graphics.DrawLine(majorGridPen,
                mGaugeRegion.X, mGaugeRegion.Y,
                mGaugeRegion.X, mBulbJoinRegion.Bottom);
            graphics.DrawLine(majorGridPen,
                mGaugeRegion.Right, mGaugeRegion.Y,
                mGaugeRegion.Right, mBulbJoinRegion.Bottom);
            graphics.DrawArc(majorGridPen, mBulbRegion, -45, 270);
            majorGridPen.Alignment = PenAlignment.Center;

            minorGridPen.Dispose();
            majorGridPen.Dispose();

            scaleLabelFormat.Dispose();
            majorScaleFont.Dispose();
            minorScaleFont.Dispose();
            scaleBrush.Dispose();
        }
        
        # endregion

        # region Digital Number Drawing
        /*
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
            float margin = mDigitalBoardOverlayRect.Height * 0.1f;
            RectangleF displayArea = RectangleF.Inflate(mDigitalBoardOverlayRect, -margin, -margin);

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
                displayArea = RectangleF.Inflate(displayArea, (digitAreaWidth - displayArea.Width)  * 0.5f, 0.0f);

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
        */
        private void DrawValue(Graphics graphics)
        {
          float mValueHeight = mContentRegion.Height * 0.06f;
            Brush valueLabelBrush = new SolidBrush(Color.Black);

      StringFormat valueLabelFormat = new StringFormat(StringFormat.GenericTypographic)
      {
        Alignment = StringAlignment.Center,
        LineAlignment = StringAlignment.Center
      };

      Font valueLabelFont = new Font(this.Font.FontFamily, mValueHeight, this.Font.Style);

            string valueString = Math.Round(mValue, 0).ToString();
            if (!string.IsNullOrEmpty(Unit))
                valueString += " " + Unit;
            graphics.DrawString(valueString, valueLabelFont, valueLabelBrush,
                mValueRect.X + mValueRect.Width * 0.5f,
                mValueRect.Y + mValueRect.Height * 0.5f, valueLabelFormat);

            valueLabelFormat.Dispose();
            valueLabelFont.Dispose();
            valueLabelBrush.Dispose();


            double startPosition = GetPositionAtValue(mValue);
            double endPosition = GetPositionAtValue(0.0f);
            //RectangleF valueRect = mGaugeRegion;

            LinearGradientBrush valueBrush = new LinearGradientBrush(new RectangleF(-1.0f, -1.0f, 2.0f, 2.0f), Color.Red, Color.DarkRed, 10.0f, false);
            valueBrush.ScaleTransform(mGaugeRegion.Width * 1.0f, -mGaugeRegion.Width * 1.0f);
            valueBrush.WrapMode = WrapMode.TileFlipXY;

            RectangleF valueRect = new RectangleF(
                mGaugeRegion.Left, (float)startPosition, mGaugeRegion.Width, (float)(endPosition - startPosition));

            graphics.FillEllipse(valueBrush, mBulbRegion);
            graphics.FillRectangle(valueBrush, mBulbJoinRegion);
            if (!valueRect.IsEmpty)
                graphics.FillRectangle(valueBrush, valueRect);
            valueBrush.Dispose();
        }
        # endregion

        # region Pointer Drawing
        /*
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
        */
        # endregion

        # region Overlay Drawing
        /*
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
        */
        # endregion

        private double GetValueAtPosition(double position)
        {
            double value = (position - mGaugeRegion.Top);
            if (mGaugeRegion.Height > 0.0f)
                value /= mGaugeRegion.Height;

            value = 1.0 - value;

            double width = mMaximumValue - mMinimumValue;

            value *= width;

            if (value > mMaximumValue)
                value = (float)mMaximumValue;
            if (value < mMinimumValue)
                value = (float)mMinimumValue;

            return (float)value;
        }

        private double GetPositionAtValue(double value)
        {
            double width = mMaximumValue - mMinimumValue;
            if (value > mMaximumValue)
                value = (float)mMaximumValue;
            if (value < mMinimumValue)
                value = (float)mMinimumValue;

            if (width > 0.0)
                value /= width;

            value = 1.0 - value;

            return (float)(mGaugeRegion.Top + mGaugeRegion.Height * value);
        }

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

                    DrawOverlay(graphics, mContentRegion);
                }

                mInvalidateOverlay = false;
            }
        }

        private void UpdateContentRegion()
        {
          if (mKeepContentAspect)
          {
            // If control should keep the content aspect ratio constant
            float contentHeight = 0;
            float contentWidth = 0;
            if (mContentAspectRatio <= 1)
            {
              // If control is taller than wide
              contentHeight = this.ClientRectangle.Height;
              contentWidth = contentHeight * mContentAspectRatio;
            }
            else
            {
              // If control is wider than tall
              contentWidth = this.ClientRectangle.Width;
              contentHeight = contentWidth / mContentAspectRatio;
            }
            if (contentWidth < 95)
            {
              contentWidth = 95;
            }
            Console.WriteLine(contentWidth.ToString());
            mContentRegion = new RectangleF((this.ClientRectangle.Width - contentWidth) / 2.0f, (this.ClientRectangle.Height - contentHeight) / 2.0f, contentWidth, contentHeight);
          }
          else
          {
            // If control should't keep the content aspect ratio constant
            mContentRegion = this.ClientRectangle;
          }

        }

        # region Constants
            const int mClientMargin = 0;
            const float mGuageLeftMargin = 8.0f;
            const float mGuageRightMargin = 8.0f;
            const float mMajorGridWidth = 3;
            const float mMinorGridWidth = 2;
            const float mMinorGridMarginPercentage = 0.2f;
            const float mTitleTopMargin = 2;
            const float mTitleBottomMargin = 2;
            //const float mTitleHeight = 18.0f;
            const float mValueTopMargin = 2;
            const float mValueBottomMargin = 2;
            const float mValueHeight = 16.0f;
            const float mScaleOffset = 8.0f;
            const float mScaleWidth = 26.0f;
            //const float mMajorScaleHeight = 10.0f;
            //const float mMinorScaleHeight = 8.0f;
        # endregion
        
        # region User Defined Properties
        private double mValue = 0.0;
        private double mMinimumValue = 0.0;
        private double mMaximumValue = 100.0;

        private bool mKeepContentAspect = true;
        private float mContentAspectRatio = 0.4f;

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

        private RectangleF mClientRegion;
        private RectangleF mGaugeRegion;
        private RectangleF mTitleRect;
        private RectangleF mValueRect;
        private RectangleF mLeftScaleRegion;
        private RectangleF mRightScaleRegion;
        private RectangleF mBulbRegion;
        private RectangleF mBulbJoinRegion;
        private RectangleF mTopRegion;

        private RectangleF mContentRegion;

        private const float mGaugeAngleStart = 135.0f;
        private const float mGaugeAngleEnd = 405.0f;
        # endregion
    }
}
