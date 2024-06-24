using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace GST.Gearshift.Components.Controls.Gauges
{
  public partial class GaugeBar : UserControl
  {
    public GaugeBar()
    {
      this.SetStyle(
                    ControlStyles.SupportsTransparentBackColor |
                    ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.ResizeRedraw |
                    ControlStyles.UserPaint, true);
      InitializeComponent();
    }

    Color mBackColor1 = Color.AliceBlue;
    Color mBackColor2 = Color.Blue;

    Color mBarColor1 = Color.Orange;
    Color mBarColor2 = Color.Yellow;

    float mFillRatio = 0.5f;

    byte mScaleLinesCount = 5;
    Color mScaleLinesColor = Color.DarkGray;
    byte mScaleLinesWidth = 2;

    protected override void OnPaint(PaintEventArgs e)
    {
      Graphics g = e.Graphics;

      LinearGradientBrush brushFill = null;

      Rectangle rect = this.ClientRectangle;
      
      #region Draw the background
      if (this.Width > 0 && this.Height > 0)
      {
        //SolidBrush brush = new SolidBrush(layoutColor1);
        brushFill = new LinearGradientBrush(rect, mBackColor1, mBackColor2, LinearGradientMode.Vertical);
        brushFill.SetSigmaBellShape(0.0F);
        g.FillRectangle(brushFill, rect);

        //GraphicsPath path = GetTopRoundedRect(upperRoundRect, roundingRadius);
        //g.FillPath(brushFill, path);
        //brushFill = new LinearGradientBrush(lowerRoundRect, mLayoutColor1, mLayoutColor2, LinearGradientMode.Vertical);
        //brushFill.SetSigmaBellShape(1.0F);
        //path = GetBottomRoundedRect(lowerRoundRect, roundingRadius);
        //g.FillPath(brushFill, path);
        //SolidBrush solidbrush = new SolidBrush(mLayoutColor1);
        //g.FillRectangle(solidbrush, unroundedRect);
        //FillRoundRectangle(g, brushFill, 0, 0, mainRect.Width, mainRect.Height, 10.0f);
      }
      #endregion

      #region Draw the scale lines
      float scaleLineCentersDistance = this.Height / (mScaleLinesCount + 1);
      SolidBrush scalesBrush = new SolidBrush(mScaleLinesColor);
      for (int i = 1; i <= mScaleLinesCount; i++)
      {
        g.FillRectangle(scalesBrush, 0, i * scaleLineCentersDistance - ScaleLinesWidth / 2, this.Width, mScaleLinesWidth);
      }
      #endregion

        #region Draw the current bar
      float barSpacing = 4.0f;
      brushFill = new LinearGradientBrush(this.ClientRectangle, mBarColor1, mBarColor2, LinearGradientMode.Horizontal);
      brushFill.SetSigmaBellShape(0.5F);
      float barHeight = this.Height * mFillRatio;
      g.FillRectangle(brushFill, barSpacing, this.Height - barHeight, this.Width - 2*barSpacing, barHeight);
        //int currBarHeight = (int)((mCurrentBarRect.Height * mCurrentValue) / mCurrentMax);
      //RectangleF fullBar = new RectangleF( barHShift, barHeight + upperBorder, barWidth, barHeight );
      //Rectangle currentBar = new Rectangle(mCurrentBarRect.X, mCurrentBarRect.Bottom - currBarHeight, mCurrentBarRect.Width, currBarHeight);
      //brushFill = new LinearGradientBrush(mCurrentBarRect, mBarColor1, mBarColor2, LinearGradientMode.Vertical);
      //brushFill.SetBlendTriangularShape(0.0f);
      //brushFill.WrapMode = WrapMode.TileFlipX;
      //g.FillRectangle(brushFill, currentBar);
      //brushFill.Dispose();
      #endregion


    }

    /// <summary>
    /// Gets/Sets bar color 1
    /// </summary>
    [System.ComponentModel.Category("Appearance"),
    EditorBrowsable(EditorBrowsableState.Always),
    Browsable(true),
    DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
    Bindable(true)]
    public Color barColor1
    {
      get
      {
        return mBarColor1;
      }
      set
      {
        mBarColor1 = value;
        Invalidate();
      }
    }

    /// <summary>
    /// Gets/Sets bar color 2
    /// </summary>
    [System.ComponentModel.Category("Appearance"),
    EditorBrowsable(EditorBrowsableState.Always),
    Browsable(true),
    DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
    Bindable(true)]
    public Color barColor2
    {
      get
      {
        return mBarColor2;
      }
      set
      {
        mBarColor2 = value;
        Invalidate();
      }
    }

    /// <summary>
    /// Gets/Sets background color 1
    /// </summary>
    [System.ComponentModel.Category("Appearance"),
    EditorBrowsable(EditorBrowsableState.Always),
    Browsable(true),
    DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
    Bindable(true)]    
    public Color BackColor1
    {
      get
      {
        return mBackColor1;
      }
      set
      {
        mBackColor1 = value;
        Invalidate();
      }
    }

    /// <summary>
    /// Gets/Sets background color 2
    /// </summary>
    [System.ComponentModel.Category("Appearance"),
    EditorBrowsable(EditorBrowsableState.Always),
    Browsable(true),
    DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
    Bindable(true)]
    public Color BackColor2
    {
      get
      {
        return mBackColor2;
      }
      set
      {
        mBackColor2 = value;
        Invalidate();
      }
    }

    [System.ComponentModel.Category("Appearance"),
    EditorBrowsable(EditorBrowsableState.Always),
    Browsable(true),
    DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
    Bindable(true)]
    public float FillRatio
    {
      get {return mFillRatio;}
      set
      {
        float oldValue = mFillRatio;
        if (value >= 0.0f)
        {
          if (value <= 1.0f)
          {
            mFillRatio = value;
          }
          else
          {
            mFillRatio = 1.0f;
          }
        }
        else
        {
          mFillRatio = 0;
        }
        if (mFillRatio != oldValue)
        {
          this.Invalidate();
        }
      }
    }

    [System.ComponentModel.Category("Appearance"),
    EditorBrowsable(EditorBrowsableState.Always),
    Browsable(true),
    DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
    Bindable(true)]
    public byte ScaleLinesCount
    {
        get { return mScaleLinesCount; }
        set
        {
            if (mScaleLinesCount != value)
            {
                mScaleLinesCount = value;
                this.Invalidate();
            }
        }
    }

    [System.ComponentModel.Category("Appearance"),
    EditorBrowsable(EditorBrowsableState.Always),
    Browsable(true),
    DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
    Bindable(true)]
    public Color ScaleLinesColor
    {
        get { return mScaleLinesColor; }
        set
        {
            if (mScaleLinesColor != value)
            {
                mScaleLinesColor = value;
                this.Invalidate();
            }
        }
    }

    [System.ComponentModel.Category("Appearance"),
    EditorBrowsable(EditorBrowsableState.Always),
    Browsable(true),
    DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
    Bindable(true)]
    public byte ScaleLinesWidth
    {
      get { return mScaleLinesWidth; }
      set
      {
        if (mScaleLinesWidth != value)
        {
          mScaleLinesWidth = value;
          this.Invalidate();
        }
      }
    }

  }
}
