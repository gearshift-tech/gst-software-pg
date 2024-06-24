using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Timers;
using System.Windows.Forms;

namespace Soko.Common.Controls
{

  public class NiceGraphicalLabel : UserControl
  {

    #region Constants

    private readonly int mFocusTimerIntervalMs = 33;
    private readonly int mFocusIntensityMaxValue = 255;
    private readonly int mFocusTimerTicksToFade = 6;

    private readonly int mClickedIntensityMaxValue = 80;
    private readonly int mClickedTimerTicksToFade = 3;

    private readonly int mRoundingRadius = 5;

    #endregion  Constants

    #region Private fields

    private Color mBackColorOnFocus1 = Color.FromArgb(255, 255, 217, 72);
    private Color mBackColorOnFocus2 = Color.FromArgb(255, 255, 238, 176);
    private Color mBackColorOnClicked1 = Color.Orange;
    private Color mBackColorOnClicked2 = Color.Orange;
    private bool mFocused = false;
    private int mFocusHooverIntensity = 0;
    private int mFocusClickedIntensity = 0;
    private int mFocusIntensityChangePerTimerTick = 0;
    private int mFocusClickedIntensityChangePerTimerTick = 0;
    private bool mMouseIsDown = false;
    private bool mInteractiveAnimationsEnabled = true;
    private Color mBackColor = Color.Transparent;
    private Color mBorderColor = Color.Transparent;
    private Color mBorderColorOnFocus = Color.FromArgb(255, 255, 217, 72);
    private System.Timers.Timer mFocusTimer = new System.Timers.Timer();
    private System.Timers.Timer mFocusClickedTimer = new System.Timers.Timer();

    private int mBorderWidth = 1;

    private String mText = "";
    private TextRelation mTextImageRelation = TextRelation.Underneath;
    private int mTextImageSpacing = 5;
    private PointF mImagePosition = new PointF(0.0f, 0.0f);
    private Point mBackgroundImageLocation = new Point(0, 0);

    private bool mDrawBackColorOnFocus = false;
    //private bool mDrawImageOnFocus = false;
    //private bool mDrawImageDisabled = false;
    private bool mDrawBorderOnFocus = false;
    private bool mDrawBackgroundImage = false;

    private Image mImageON = null;
    //private Image mImageOFF = null;
    private Image mImageDisabled = null;

    private Padding mContentPadding = new Padding(0);

    private RectangleF mTextRectangle = new Rectangle(0, 0, 0, 0);
    private RectangleF mImageRectangle = new Rectangle(0, 0, 0, 0);


    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    #endregion Private fields

    #region Constructors & finalizer

    public NiceGraphicalLabel()
    {
      this.Name = "NiceGraphicalLabel";
      this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);    

      this.SetStyle(
          ControlStyles.SupportsTransparentBackColor |
          ControlStyles.OptimizedDoubleBuffer |
          ControlStyles.AllPaintingInWmPaint |
          ControlStyles.ResizeRedraw |
          ControlStyles.UserPaint, true);
      mImageON = Soko.Common.Properties.Resources.NiceButton_DefaultIcon;
      mImageDisabled = Soko.Common.Properties.Resources.NiceButton_DefaultIconInactive;
      Width = mImageON.Width;
      Height = mImageON.Height;
      EnabledChanged += new EventHandler( ThisEnabledChanged );
      MouseDown += new MouseEventHandler(MouseDownEH);
      MouseUp += new MouseEventHandler(MouseUpEH);
      mFocusTimer.Interval = mFocusTimerIntervalMs;
      mFocusTimer.Enabled = false;
      mFocusTimer.Elapsed += new ElapsedEventHandler( FocusTimerElapsedEventHandler );
      mFocusClickedTimer.Interval = mFocusTimerIntervalMs;
      mFocusClickedTimer.Enabled = false;
      mFocusClickedTimer.Elapsed += new ElapsedEventHandler(FocusClickedTimerElapsedEventHandler);
    }

    #endregion Constructors & finalizer

    #region Events


    #endregion Events

    #region Properties

    private String _titleText = "Title";
    public String TitleText
    {
      get
      {
        return _titleText;
      }
      set
      {
        _titleText = value;
        this.Invalidate();
      }
    }

    private String _detailsText = "Details";
    public String DetailsText
    {
      get
      {
        return _detailsText;
      }
      set
      {
        _detailsText = value;
        this.Invalidate();
      }
    }



    [System.ComponentModel.Category("Interactive animations")]
    public bool InteractiveAnimationsEnabled
    {
      get
      {
        return mInteractiveAnimationsEnabled;
      }
      set
      {
        mInteractiveAnimationsEnabled = value;
      }
    }

    [System.ComponentModel.Category("Interactive animations")]
    public Color BackColorOnFocus1
    {
      get { return mBackColorOnFocus1; }
      set 
      { 
        mBackColorOnFocus1 = value;
        Invalidate();
      }
    }

    [System.ComponentModel.Category("Interactive animations")]
    public Color BackColorOnFocus2
    {
      get { return mBackColorOnFocus2; }
      set
      {
        mBackColorOnFocus2 = value;
        Invalidate();
      }
    }

    [System.ComponentModel.Category("Interactive animations")]
    public Color BackColorOnClicked1
    {
      get { return mBackColorOnClicked1; }
      set
      {
        mBackColorOnClicked1 = value;
        Invalidate();
      }
    }

    [System.ComponentModel.Category("Interactive animations")]
    public Color BackColorOnClicked2
    {
      get { return mBackColorOnClicked2; }
      set
      {
        mBackColorOnClicked2 = value;
        Invalidate();
      }
    }

    [System.ComponentModel.Category("Interactive animations")]
    public bool DrawBorderOnFocus
    {
      get { return mDrawBorderOnFocus; }
      set
      {
        mDrawBorderOnFocus = value;
        Invalidate();
      }
    }

    [System.ComponentModel.Category("Interactive animations")]
    public Color BorderColorOnFocus
    {
      get { return mBorderColorOnFocus; }
      set
      {
        mBorderColorOnFocus = value;
        Invalidate();
      }
    }

    [System.ComponentModel.Category("Appearance")]
    new public Color BackColor
    {
      get { return mBackColor; }
      set 
      { 
        mBackColor = value;
        Invalidate();
      }
    }

    [System.ComponentModel.Category("Appearance")]
    public Color BorderColor
    {
      get { return mBorderColor; }
      set 
      { 
        mBorderColor = value;
        Invalidate();
      }
    }



    [System.ComponentModel.Category("Appearance")]
    public int BorderWidth
    {
      get { return mBorderWidth; }
      set 
      {
        mBorderWidth = value;
        Invalidate();
      }
    }

    [System.ComponentModel.Category( "Appearance" ),
    EditorBrowsable(EditorBrowsableState.Always), 
    Browsable(true),
    DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
    Bindable(true)]
    public override String Text
    {
      get { return mText; }
      set 
      { 
        mText = value;
        Invalidate();
      }
    }

    [System.ComponentModel.Category("Appearance"),
    EditorBrowsable(EditorBrowsableState.Always),
    Browsable(true),
    DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
    Bindable(true)]
    public override Font Font
    {
      get { return base.Font; }
      set
      {
        base.Font = value;
        Invalidate();
      }
    }

    [System.ComponentModel.Category("Appearance")]
    public TextRelation TextImageRelation
    {
      get { return mTextImageRelation; }
      set
      {
        mTextImageRelation = value;
        Invalidate();
      }
    }

    [System.ComponentModel.Category("Appearance")]
    public bool DrawBackColorOnFocus
    {
      get { return mDrawBackColorOnFocus; }
      set 
      { 
        mDrawBackColorOnFocus = value;
        Invalidate();
      }
    }

    [System.ComponentModel.Category("Appearance")]
    public Image Image
    {
      get { return mImageON; }
      set 
      {
        if (value == null)
        {
          mImageON = new Bitmap(0, 0);
        }
        else
        {
          mImageON = value;
        }
        Invalidate();
      }
    }

    //public Image ImageOnFocus
//     {
//       get { return mImageOFF; }
//       set 
//       { 
//         mImageOFF = value;
//         Invalidate();
//       }
//     }

    [System.ComponentModel.Category("Appearance")]
    public Image ImageDisabled
    {
      get { return mImageDisabled; }
      set 
      {
        if (value == null)
        {
          mImageDisabled = new Bitmap(0, 0);
        }
        else
        {
          mImageDisabled = value;
        }
        Invalidate();
      }
    }

    [System.ComponentModel.Category("Appearance")]
    public bool DrawBackgroundImage
    {
      get { return mDrawBackgroundImage; }
      set
      {
        mDrawBackgroundImage = value;
        Invalidate();
      }
    }

    [System.ComponentModel.Category("Appearance")]
    public Point BackgroundImageLocation
    {
      get { return mBackgroundImageLocation; }
      set
      {
        mBackgroundImageLocation = value;
        Invalidate();
      }
    }

    #endregion Properties

    #region Methods

    private void FocusTimerElapsedEventHandler(object sender, ElapsedEventArgs e)
    {
      if (mFocused)
      {
        if (mFocusHooverIntensity + mFocusIntensityChangePerTimerTick >= mFocusIntensityMaxValue)
        {
          mFocusHooverIntensity = mFocusIntensityMaxValue;
          mFocusTimer.Enabled = false;
        }
        else
        {
          mFocusHooverIntensity += mFocusIntensityChangePerTimerTick;
        }
      }
      else
      {
        if (mFocusHooverIntensity - mFocusIntensityChangePerTimerTick <= 0)
        {
          mFocusHooverIntensity = 0;
          mFocusTimer.Enabled = false;
        }
        else
        {
          mFocusHooverIntensity -= mFocusIntensityChangePerTimerTick;
        }
      }
      //Console.WriteLine(mFocusHooverIntensity.ToString() + " " + mFocused.ToString());
     
      try
      {
        this.BeginInvoke(new MethodInvoker(RefreshMe));
      }
      catch
      {
      }
    }

    private void FocusClickedTimerElapsedEventHandler( object sender, ElapsedEventArgs e )
    {
      if (mMouseIsDown)
      {
        if (mFocusClickedIntensity + mFocusClickedIntensityChangePerTimerTick >= mClickedIntensityMaxValue)
        {
          mFocusClickedIntensity = mClickedIntensityMaxValue;
            mFocusClickedTimer.Enabled = false;
        }
        else
        {
          mFocusClickedIntensity += mFocusClickedIntensityChangePerTimerTick;
        }
      }
      else
      {
        if (mFocusClickedIntensity - mFocusClickedIntensityChangePerTimerTick <= 0)
        {
          mFocusClickedIntensity = 0;
            mFocusClickedTimer.Enabled = false;
        }
        else
        {
          mFocusClickedIntensity -= mFocusClickedIntensityChangePerTimerTick;
        }
      }
      //Console.WriteLine("CLICK: " + mFocusClickedIntensity.ToString() + " " + mMouseIsDown.ToString());
      try
      {
        this.BeginInvoke(new MethodInvoker(RefreshMe));
      }
      catch
      {
      }
      
    }

    private void RefreshMe()
    {
      this.Refresh();
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

    private void ThisEnabledChanged(object sender, EventArgs e)
    {
      Invalidate();
    }

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

      RectangleF titleRectangle = new RectangleF(
  mainRect.X + mainRect.Width * 0.1f,
  mainRect.Y,
  mainRect.Width * 0.9f,
  mainRect.Height / 2.0f);

      RectangleF contentRectangle = new RectangleF(
          mainRect.X + mainRect.Width * 0.1f,
          mainRect.Y + mainRect.Height / 2.0f,
          mainRect.Width * 0.9f,
          mainRect.Height / 2.0f);

      Font detailsFont = new Font(this.Font.FontFamily, this.Font.Size * 0.8f, FontStyle.Italic);


      using (StringFormat sf = new StringFormat())
      {
        sf.Alignment = StringAlignment.Near;
        sf.FormatFlags = StringFormatFlags.NoWrap;
        sf.LineAlignment = StringAlignment.Center;
        sf.Trimming = StringTrimming.EllipsisCharacter;

        using (var titleTextBrush = new SolidBrush(ForeColor))
          g.DrawString(_titleText, this.Font, new SolidBrush(this.ForeColor), titleRectangle, sf);


        using (var contentTextBrush = new SolidBrush(ForeColor))
          g.DrawString(_detailsText, detailsFont, new SolidBrush(this.ForeColor), contentRectangle, sf);
      }

      ////draw the button label
      //StringFormat formatCenter = new StringFormat();
      //formatCenter.LineAlignment = StringAlignment.Center;
      //formatCenter.Alignment = StringAlignment.Center;
      //g.DrawString( mText, this.Font, new SolidBrush( this.ForeColor ), mTextRectangle, formatCenter );

    }

    protected override void OnPaintBackground( PaintEventArgs e )
    {
      if ( e == null )
        return;
      if ( e.Graphics == null )
        return;

      e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

      //this must have been applied to support pseudo transparency
      if (this.Parent != null)
      {
        GraphicsContainer cstate = e.Graphics.BeginContainer();
        e.Graphics.TranslateTransform(-this.Left, -this.Top);
        Rectangle clip = e.ClipRectangle;
        clip.Offset(this.Left, this.Top);
        PaintEventArgs pe = new PaintEventArgs(e.Graphics, clip);

        //paint the container's bg
        InvokePaintBackground(this.Parent, pe);
        //paints the container fg
        InvokePaint(this.Parent, pe);
        //restores graphics to its original state
        e.Graphics.EndContainer(cstate);
      }
      else
      {
        base.OnPaintBackground(e);
      }

      // Draw the background image if required
      if (mDrawBackgroundImage && this.BackgroundImage != null)
      {
        e.Graphics.DrawImage(this.BackgroundImage, new Rectangle(mBackgroundImageLocation, BackgroundImage.Size));
      }

      GraphicsPath path = GetRoundedRect(new Rectangle(0, 0, this.Width-1, this.Height-1), mRoundingRadius);
      GraphicsPath topPath = GetTopRoundedRectHalf(new Rectangle(0, 0, this.Width-1, this.Height-1), mRoundingRadius);
      GraphicsPath bottomPath = GetBottomRoundedRectHalf(new Rectangle(0, 0, this.Width-1, this.Height-1), mRoundingRadius);
      // draw the border
      if (BorderWidth > 0 && mBorderColor != Color.Transparent)
      {
        e.Graphics.DrawPath(new Pen(new SolidBrush(mBorderColor)), path);
      }

      e.Graphics.FillPath(new SolidBrush(BackColor), path);


      // draw the animated backgrounds if active
      if (mFocusHooverIntensity > 0)
      {
        LinearGradientBrush brushFill = new LinearGradientBrush(new Rectangle(0, this.Height / 2, this.Width, this.Height), Color.FromArgb(mFocusHooverIntensity * mBackColorOnFocus1.A / 255, mBackColorOnFocus1), Color.FromArgb(mFocusHooverIntensity * mBackColorOnFocus2.A / 255, mBackColorOnFocus2), LinearGradientMode.Vertical);
        brushFill.SetBlendTriangularShape(1.0F);
        e.Graphics.FillPath(new SolidBrush(Color.FromArgb((mFocusHooverIntensity * mBackColorOnFocus2.A) / 255, mBackColorOnFocus2)), topPath);
        e.Graphics.FillPath(brushFill, bottomPath);

        // If the button is clicked, definitely it is hovered
        if (mFocusClickedIntensity > 0)
        {
          e.Graphics.FillPath(new SolidBrush(Color.FromArgb((mFocusClickedIntensity * mBackColorOnClicked1.A) / 255, mBackColorOnClicked1)), path);
        }

        if (mDrawBorderOnFocus)
        {
          e.Graphics.DrawPath(new Pen(new SolidBrush(Color.FromArgb(mFocusHooverIntensity * mBorderColorOnFocus.A / 255, mBorderColorOnFocus))), path);
        }
      }



    }

    protected override void OnMouseEnter( EventArgs e)
    {
      base.OnMouseEnter( e );
      mFocusIntensityChangePerTimerTick = mFocusIntensityMaxValue / mFocusTimerTicksToFade;
      mFocused = true;
      mFocusTimer.Enabled = true;
      this.BeginInvoke(new MethodInvoker(RefreshMe));
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave( e );
      mFocusIntensityChangePerTimerTick = mFocusIntensityMaxValue / mFocusTimerTicksToFade;
      mFocused = false;
      mFocusTimer.Enabled = true;
      this.BeginInvoke(new MethodInvoker(RefreshMe));
    }

    private void MouseDownEH(object sender, MouseEventArgs e)
    {
      mFocusClickedIntensityChangePerTimerTick = mClickedIntensityMaxValue / mClickedTimerTicksToFade;
      mMouseIsDown = true;
      mFocusClickedTimer.Enabled = true;
      this.BeginInvoke(new MethodInvoker(RefreshMe));
    }

    private void MouseUpEH(object sender, MouseEventArgs e)
    {
      mFocusClickedIntensityChangePerTimerTick = mClickedIntensityMaxValue / mClickedTimerTicksToFade;
      mMouseIsDown = false;
      mFocusClickedTimer.Enabled = true;
      try
      {
        this.BeginInvoke(new MethodInvoker(RefreshMe));
      }
      catch
      {
      }

    }

    private GraphicsPath GetRoundedRect(RectangleF baseRect, float radius)
    {
      // if corner radius is less than or equal to zero, 
      // return the original rectangle 
      if (radius <= 0.0F)
      {
        GraphicsPath mPath = new GraphicsPath();
        mPath.AddRectangle(baseRect);
        mPath.CloseFigure();
        return mPath;
      }
      // if the corner radius is greater than or equal to 
      // half the width, or height (whichever is shorter) 
      // then return a capsule instead of a lozenge 
      if (radius >= (Math.Min(baseRect.Width, baseRect.Height)) / 2.0)
        return GetCapsule(baseRect);
      // create the arc for the rectangle sides and declare 
      // a graphics path object for the drawing 
      float diameter = radius * 2.0F;
      SizeF sizeF = new SizeF(diameter, diameter);
      RectangleF arc = new RectangleF(baseRect.Location, sizeF);
      GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
      // top left arc 
      path.AddArc(arc, 180, 90);
      // top right arc 
      arc.X = baseRect.Right - diameter ;
      path.AddArc(arc, 270, 90);
      // bottom right arc 
      arc.Y = baseRect.Bottom - diameter ;
      path.AddArc(arc, 0, 90);
      // bottom left arc
      arc.X = baseRect.Left;
      path.AddArc(arc, 90, 90);
      path.CloseFigure();
      return path;
    }

    private GraphicsPath GetCapsule(RectangleF baseRect)
    {
      float diameter;
      RectangleF arc;
      GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
      try
      {
        if (baseRect.Width > baseRect.Height)
        {
          // return horizontal capsule 
          diameter = baseRect.Height;
          SizeF sizeF = new SizeF(diameter, diameter);
          arc = new RectangleF(baseRect.Location, sizeF);
          path.AddArc(arc, 90, 180);
          arc.X = baseRect.Right - diameter;
          path.AddArc(arc, 270, 180);
        }
        else if (baseRect.Width < baseRect.Height)
        {
          // return vertical capsule 
          diameter = baseRect.Width;
          SizeF sizeF = new SizeF(diameter, diameter);
          arc = new RectangleF(baseRect.Location, sizeF);
          path.AddArc(arc, 180, 180);
          arc.Y = baseRect.Bottom - diameter;
          path.AddArc(arc, 0, 180);
        }
        else
        {
          // return circle 
          path.AddEllipse(baseRect);
        }
      }
      catch (Exception)
      {
        path.AddEllipse(baseRect);
      }
      finally
      {
        path.CloseFigure();
      }
      return path;
    }

    /// <summary>
    /// Returns top rounded rectangle graphics path
    /// </summary>
    /// <param name="baseRect"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    private GraphicsPath GetTopRoundedRectHalf(RectangleF baseRect, float radius)
    {
      // if corner radius is less than or equal to zero, 
      // return the original rectangle 
      if (radius <= 0.0F)
      {
        GraphicsPath mPath = new GraphicsPath();
        mPath.AddRectangle(baseRect);
        mPath.CloseFigure();
        return mPath;
      }
      // if the corner radius is greater than 
      // the width, or height (whichever is shorter) 
      // then return a capsule instead of a lozenge 
      if (radius > (Math.Min(baseRect.Width, baseRect.Height)))
        return GetCapsule(baseRect);
      // create the arc for the rectangle sides and declare 
      // a graphics path object for the drawing 
      float diameter = radius * 2.0F;
      SizeF sizeF = new SizeF(diameter, diameter);
      RectangleF arc = new RectangleF(baseRect.Location, sizeF);
      RectangleF ZeroArc = new RectangleF(baseRect.Location, new SizeF(0.0001f, 0.0001f));
      GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
      // top left arc 
      path.AddArc(arc, 180, 90);
      // top right arc 
      arc.X = baseRect.Right - diameter;
      path.AddArc(arc, 270, 90);
      // bottom edge
      path.AddLine(baseRect.Width, (baseRect.Height / 2.0f), 0, (baseRect.Height / 2.0f));
      path.CloseFigure();
      return path;
    }

    /// <summary>
    /// Returns bottom rounded rectangle graphics path
    /// </summary>
    /// <param name="baseRect"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    private GraphicsPath GetBottomRoundedRectHalf(RectangleF baseRect, float radius)
    {
      // if corner radius is less than or equal to zero, 
      // return the original rectangle 
      if (radius <= 0.0F)
      {
        GraphicsPath mPath = new GraphicsPath();
        mPath.AddRectangle(baseRect);
        mPath.CloseFigure();
        return mPath;
      }
      // if the corner radius is greater than 
      // the width, or height (whichever is shorter) 
      // then return a capsule instead of a lozenge 
      if (radius > (Math.Min(baseRect.Width, baseRect.Height)))
        return GetCapsule(baseRect);
      // create the arc for the rectangle sides and declare 
      // a graphics path object for the drawing 
      float diameter = radius * 2.0F;
      SizeF sizeF = new SizeF(diameter, diameter);
      RectangleF arc = new RectangleF(baseRect.Location, sizeF);
      RectangleF ZeroArc = new RectangleF(baseRect.Location, new SizeF(0.0001f, 0.0001f));
      GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
      // top edge
      path.AddLine(0, (baseRect.Height / 2.0f), baseRect.Width, (baseRect.Height / 2.0f));
      // bottom right arc 
      arc.X = baseRect.Right - diameter;
      arc.Y = baseRect.Bottom - diameter;
      path.AddArc(arc, 0, 90);
      // bottom left arc
      arc.X = baseRect.Left;
      path.AddArc(arc, 90, 90);
      path.CloseFigure();
      return path;
    }
    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      try
      {
        if (disposing && (components != null))
        {
          components.Dispose();
        }
        base.Dispose(disposing);
      }
      catch (Exception)
      {

      }
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
      this.Refresh();
    }

    #endregion Methods



  }
}
