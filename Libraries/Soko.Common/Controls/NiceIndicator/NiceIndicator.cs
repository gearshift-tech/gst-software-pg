using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Soko.Common.Controls
{



  public partial class NiceIndicator : UserControl
  {

    /// <summary>
    /// possible text relations
    /// </summary>
    public enum TextRelation
    {
      /// <summary>
      /// The text is underneath the image.
      /// </summary>		
      Underneath = 0,
      /// <summary>
      /// The text is above the image.
      /// </summary>
      Above = 1,
      /// <summary>
      /// The text is on the right side of the image.
      /// </summary>
      OnRight = 2,
      /// <summary>
      /// The text is on the left side of the image.
      /// </summary>
      OnLeft = 3,
    }

    #region Constants



    #endregion  Constants

    #region Private fields

    private Color mBackColorOnFocus = Color.Transparent;
    private Color mBackColor = Color.Transparent;
    private Color mBorderColor = Color.Black;

    private bool mIsOn = false;

    private int mBorderWidth = 1;

    private String mTextDisabled = string.Empty;
    private String mTextON = string.Empty;
    private String mTextOFF = string.Empty;
    private TextRelation mTextImageRelation = TextRelation.Underneath;
    private int mTextImageSpacing = 5;
    private PointF mImagePosition = new PointF(0.0f, 0.0f);

    private bool mDrawBackColorOnFocus = false;
    private bool mDrawImageOnFocus = false;
    private bool mDrawImageDisabled = false;
    private bool mDrawBorder = false;

    private Image mImageON = null;
    private Image mImageOFF = null;

    private RectangleF mTextRectangle = new Rectangle(0, 0, 0, 0);
    private RectangleF mImageRectangle = new Rectangle(0, 0, 0, 0);

    #endregion Private fields

    #region Constructors & finalizer

    public NiceIndicator()
    {
      InitializeComponent();

      this.SetStyle(
          ControlStyles.SupportsTransparentBackColor |
          ControlStyles.OptimizedDoubleBuffer |
          ControlStyles.AllPaintingInWmPaint |
          ControlStyles.ResizeRedraw |
          ControlStyles.UserPaint, true);
      mImageON = new Bitmap(1, 1);
      mImageOFF = new Bitmap(1, 1); 
      Width = mImageON.Width;
      Height = mImageON.Height;
      base.Click += new EventHandler( ThisOnClick );
    }

    #endregion Constructors & finalizer

    #region Events

    public event EventHandler OnStateChangedEvent;

    #endregion Events

    #region Properties

    public Object Tag1 = null;
    public Object Tag2 = null;

    public bool IsOn
    {
      get { return mIsOn; }
      set
      {
        mIsOn = value;
        ProceedContextRelations();
        Invalidate();
      }
    }

    public Color BackColorOnFocus
    {
      get { return mBackColorOnFocus; }
      set 
      { 
        mBackColorOnFocus = value;
        Invalidate();
      }
    }

    new public Color BackColor
    {
      get { return mBackColor; }
      set 
      { 
        mBackColor = value;
        Invalidate();
      }
    }

    public Color BorderColor
    {
      get { return mBorderColor; }
      set 
      { 
        mBorderColor = value;
        Invalidate();
      }
    }

    public int BorderWidth
    {
      get { return mBorderWidth; }
      set 
      {
        mBorderWidth = value;
        Invalidate();
      }
    }

    public String TextDisabled
    {
      get { return mTextDisabled; }
      set 
      { 
        mTextDisabled = value;
        ProceedContextRelations();
        Invalidate();
      }
    }

    public String TextON
    {
      get { return mTextON; }
      set
      {
        mTextON = value;
        ProceedContextRelations();
        Invalidate();
      }
    }

    public String TextOFF
    {
      get { return mTextOFF; }
      set
      {
        mTextOFF = value;
        ProceedContextRelations();
        Invalidate();
      }
    }

    public TextRelation TextImageRelation
    {
      get { return mTextImageRelation; }
      set
      {
        mTextImageRelation = value;
        ProceedContextRelations();
        Invalidate();
      }
    }

    public int TextImageSpacing
    {
      get { return mTextImageSpacing; }
      set
      { 
        mTextImageSpacing = value;
        ProceedContextRelations();
        Invalidate();
      }
    }

    public bool DrawBackColorOnFocus
    {
      get { return mDrawBackColorOnFocus; }
      set 
      { 
        mDrawBackColorOnFocus = value;
        Invalidate();
      }
    }

    public bool DrawImageOnFocus
    {
      get { return mDrawImageOnFocus; }
      set 
      { 
        mDrawImageOnFocus = value;
        Invalidate();
      }
    }

    public bool DrawImageDisabled
    {
      get { return mDrawImageDisabled; }
      set 
      { 
        mDrawImageDisabled = value;
        Invalidate();
      }
    }

    public bool DrawBorder
    {
      get { return mDrawBorder; }
      set 
      {
        mDrawBorder = value;
        Invalidate();
      }
    }

    public Image ImageON
    {
      get { return mImageON; }
      set 
      { 
        mImageON = value;
        if (mImageON.Width > this.Width)
          this.Width = mImageON.Width;
        if (mImageON.Height > this.Height)
          this.Height = mImageON.Height;
        ProceedContextRelations();
        Invalidate();
      }
    }

    public Image ImageOFF
    {
      get { return mImageOFF; }
      set 
      { 
        mImageOFF = value;
        Invalidate();
      }
    }

    #endregion Properties

    #region Methods

    protected void OnPaint( object sender, PaintEventArgs e )
    {
      if ( e == null )
        return;
      if ( e.Graphics == null )
        return;

      //get graphics object
      Graphics g = e.Graphics;
      //get main rectangle
      Rectangle mainRect = new Rectangle( 0, 0, this.Width, this.Height );
      //draw border
      if ( mDrawBorder && mBorderWidth > 0 )
      {
        g.DrawRectangle( new Pen( mBorderColor, (float)mBorderWidth ), (float)mBorderWidth / 2.0f, (float)mBorderWidth / 2.0f, mainRect.Width - mBorderWidth - 1, mainRect.Width - mBorderWidth - 1 );
      }

      //scale the image down and draw enabled or disabled icon
        if (IsOn)
        {
          RectangleF scaledRect = GetScaledRectangle( mImageON, mImageRectangle );
          g.DrawImage( mImageON, scaledRect );
        }
        else // if is off
        {
          RectangleF scaledRect = GetScaledRectangle( mImageOFF, mImageRectangle );
          g.DrawImage( mImageOFF, scaledRect );
        }

      //draw the text
      string strToDraw = string.Empty;
        if (IsOn)
        {
          strToDraw = mTextON;
        }
        else
        {
          strToDraw = mTextOFF;
        }
      StringFormat formatCenter = new StringFormat
      {
        LineAlignment = StringAlignment.Center,
        Alignment = StringAlignment.Center
      };
      g.DrawString( strToDraw, this.Font, new SolidBrush( this.ForeColor ), mTextRectangle, formatCenter );

    }

    protected override void OnPaintBackground( PaintEventArgs e )
    {
      //this must have been applied to support pseudo transparency
      if ( e == null )
        return;
      if ( e.Graphics == null )
        return;

      if ( this.Parent != null )
      {
        GraphicsContainer cstate = e.Graphics.BeginContainer();
        e.Graphics.TranslateTransform( -this.Left, -this.Top );
        Rectangle clip = e.ClipRectangle;
        clip.Offset( this.Left, this.Top );
        PaintEventArgs pe = new PaintEventArgs( e.Graphics, clip );

        //paint the container's bg
        InvokePaintBackground( this.Parent, pe );
        //paints the container fg
        InvokePaint( this.Parent, pe );
        //restores graphics to its original state
        e.Graphics.EndContainer( cstate );
      }
      else
        base.OnPaintBackground( e );
    }

    private void ThisOnClick( object sender, EventArgs e)
    {
      if (Enabled)
      {
        if ( IsOn )
        {
          IsOn = false;
        }
        else
        {
          IsOn = true;
        }
        OnStateChangedEvent?.Invoke(this, EventArgs.Empty);
      }
    }


    private static RectangleF GetScaledRectangle(Image img, RectangleF thumbRect)
    {
      if (img.Width < thumbRect.Width && img.Height < thumbRect.Height)
        return new RectangleF(thumbRect.X + ((thumbRect.Width - img.Width) / 2), thumbRect.Y + ((thumbRect.Height - img.Height) / 2), img.Width, img.Height);

      int sourceWidth = img.Width;
      int sourceHeight = img.Height;

      float nPercent = 0;
      float nPercentW = 0;
      float nPercentH = 0;

      nPercentW = ((float)thumbRect.Width / (float)sourceWidth);
      nPercentH = ((float)thumbRect.Height / (float)sourceHeight);

      if (nPercentH < nPercentW)
        nPercent = nPercentH;
      else
        nPercent = nPercentW;

      int destWidth = (int)(sourceWidth * nPercent);
      int destHeight = (int)(sourceHeight * nPercent);

      if (destWidth.Equals(0))
        destWidth = 1;
      if (destHeight.Equals(0))
        destHeight = 1;

      RectangleF retRect = new RectangleF(thumbRect.X, thumbRect.Y, destWidth, destHeight);

      if (retRect.Height < thumbRect.Height)
        retRect.Y = retRect.Y + ((float)thumbRect.Height - (float)retRect.Height) / (float)2;

      if (retRect.Width < thumbRect.Width)
        retRect.X = retRect.X + ((float)thumbRect.Width - (float)retRect.Width) / (float)2;

      return retRect;
    }

    private void ProceedContextRelations()
    {
      string strToDraw = string.Empty;
      if ( IsOn )
      {
        strToDraw = mTextON;
      }
      else
      {
        strToDraw = mTextOFF;
      }



      Graphics g = this.CreateGraphics();
      SizeF textSize;
      if ( strToDraw == string.Empty )
        textSize = new SizeF(0.0f, 0.0f);
      else
        textSize = g.MeasureString( strToDraw, this.Font );     
      g.Dispose();
      float mWidth = 0;
      float mHeight = 0;
      PointF mTextPoint;
      switch (mTextImageRelation)
      {
        case TextRelation.Underneath:
          {
            mHeight = mImageON.Height + textSize.Height + mTextImageSpacing;
            if (mImageON.Width < textSize.Width)
            {
              mWidth = textSize.Width;
              mImagePosition = new PointF((mWidth - mImageON.Width) / 2.0f, 0.0f);
              mImageRectangle = new RectangleF(mImagePosition, mImageON.Size);
              mTextPoint = new PointF(0.0f, mImageON.Height + mTextImageSpacing);
              mTextRectangle = new RectangleF(mTextPoint, textSize);
            }
            else
            {
              mWidth = mImageON.Width;
              mImagePosition = new PointF(0.0f, 0.0f);
              mImageRectangle = new RectangleF(mImagePosition, mImageON.Size);
              mTextPoint = new PointF( (mWidth - textSize.Width) / 2.0f, mImageON.Height + mTextImageSpacing);
              mTextRectangle = new RectangleF(mTextPoint, textSize);
            }
            break;
          }
        case TextRelation.Above:
          {
            mHeight = mImageON.Height + textSize.Height + mTextImageSpacing;
            if (mImageON.Width < textSize.Width)
            {
              mWidth = textSize.Width;
              mImagePosition = new PointF((mWidth - mImageON.Width) / 2.0f, textSize.Height + mTextImageSpacing);
              mImageRectangle = new RectangleF(mImagePosition, mImageON.Size);
              mTextPoint = new PointF(0.0f, 0.0f);
              mTextRectangle = new RectangleF(mTextPoint, textSize);
            }
            else
            {
              mWidth = mImageON.Width;
              mImagePosition = new PointF(textSize.Height + TextImageSpacing, (mWidth - mImageON.Width) / 2.0f);
              mImageRectangle = new RectangleF(mImagePosition, mImageON.Size);
              mTextPoint = new PointF((mWidth - textSize.Width) / 2.0f, 0.0f);
              mTextRectangle = new RectangleF(mTextPoint, textSize);
            }
            break;
          }
        case TextRelation.OnLeft:
          {
            mWidth = mImageON.Width + textSize.Width + mTextImageSpacing;
            if (mImageON.Height < textSize.Height)
            {
              mHeight = textSize.Height;
              mImagePosition = new PointF(textSize.Width + TextImageSpacing, (mHeight - mImageON.Height) / 2.0f);
              mImageRectangle = new RectangleF(mImagePosition, mImageON.Size);
              mTextPoint = new PointF(0.0f, 0.0f);
              mTextRectangle = new RectangleF(mTextPoint, textSize);
            }
            else
            {
              mHeight = mImageON.Height;
              mImagePosition = new PointF(textSize.Width + TextImageSpacing, 0.0f);
              mImageRectangle = new RectangleF(mImagePosition, mImageON.Size);
              mTextPoint = new PointF(0.0f, (mHeight - textSize.Height) / 2.0f);
              mTextRectangle = new RectangleF(mTextPoint, textSize);
            }
            break;
          }
        case TextRelation.OnRight:
          {
            mWidth = mImageON.Width + textSize.Width + mTextImageSpacing;
            if (mImageON.Height < textSize.Height)
            {
              mHeight = textSize.Height;
              mImagePosition = new PointF(0.0f, (mHeight - mImageON.Height) / 2.0f);
              mImageRectangle = new RectangleF(mImagePosition, mImageON.Size);
              mTextPoint = new PointF(mImageON.Width + TextImageSpacing, 0.0f);
              mTextRectangle = new RectangleF(mTextPoint, textSize);
            }
            else
            {
              mHeight = mImageON.Height;
              mImagePosition = new PointF(0.0f, 0.0f);
              mImageRectangle = new RectangleF(mImagePosition, mImageON.Size);
              mTextPoint = new PointF(mImageON.Width + TextImageSpacing, (mHeight - textSize.Height) / 2.0f);
              mTextRectangle = new RectangleF(mTextPoint, textSize);
            }
            break;
          }
      }
      this.Width = (int)(mWidth + 0.6f);
      this.Height = (int)(mHeight + 0.6f);
    }

    private void ImageButton_EnabledChanged(object sender, EventArgs e)
    {
      Invalidate();
    }

    #endregion Methods



  }
}
