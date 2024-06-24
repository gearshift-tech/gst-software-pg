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
    /// <summary>
    /// Only text is displayed, no image
    /// </summary>
    CenterTextNoImage = 4
  }

  public enum ColorSchemeEnum
  {
    None,
    PowerDynDark
  }

  public class NiceButton : UserControl
  {

    #region Constants

    private readonly int mFocusTimerIntervalMs = 33;
    private readonly int mFocusIntensityMaxValue = 255;
    private readonly int mFocusTimerTicksToFade = 6;

    private readonly int mClickedIntensityMaxValue = 80;
    private readonly int mClickedTimerTicksToFade = 3;

    private readonly int mRoundingRadius = 2;

    #endregion  Constants



    #region Enums & Internal classes



    #endregion  Enums & Internal classes



    #region Private fields

    private ColorSchemeEnum _ColorScheme = ColorSchemeEnum.None;

    private Color mBackColorOnFocus1 = Color.FromArgb(20, 255, 255, 255);
    private Color mBackColorOnFocus2 = Color.FromArgb(20, 255, 255, 255);
    private Color mBackColorOnClicked1 = Color.FromArgb(40, 255, 255, 255);
    private Color mBackColorOnClicked2 = Color.FromArgb(40, 255, 255, 255);
    private bool mFocused = false;
    private int mFocusHooverIntensity = 0;
    private int mFocusClickedIntensity = 0;
    private int mFocusIntensityChangePerTimerTick = 0;
    private int mFocusClickedIntensityChangePerTimerTick = 0;
    private bool mMouseIsDown = false;
    private bool mInteractiveAnimationsEnabled = true;
    private Color mBackColor = Color.Transparent;
    private Color mBorderColor = Color.FromArgb(255, 0, 176, 240);
    private Color mBorderColorOnFocus = Color.FromArgb(20, 255, 255, 255);
    private System.Timers.Timer mFocusTimer = new System.Timers.Timer();
    private System.Timers.Timer mFocusClickedTimer = new System.Timers.Timer();

    private bool _SupportTransparentBackgound = false;

    private int mBorderWidth = 1;

    private String mText = "";
    private TextRelation mTextImageRelation = TextRelation.Underneath;
    private int mTextImageSpacing = 0;
    private PointF mImagePosition = new PointF(0.0f, 0.0f);
    private Point mBackgroundImageLocation = new Point(0, 0);

    private bool mDrawBackColorOnFocus = true;
    private bool mDrawBorderOnFocus = true;
    private bool mDrawBackgroundImage = false;

    private Image mImageON = null;
    private Image mImageDisabled = null;

    private Padding mContentPadding = new Padding(0);

    private RectangleF mTextRectangle = new Rectangle(0, 0, 0, 0);
    private RectangleF mImageRectangle = new Rectangle(0, 0, 0, 0);
    GraphicsPath _FullPath = null;
    GraphicsPath _TopHalfPath = null;
    GraphicsPath _BottomHalfPath = null;


    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    #endregion Private fields



    #region Constructors & finalizer

    public NiceButton()
    {
      this.Name = "ImageButton";
      this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);

      SetControlStyle();

      mBackColor = Color.FromArgb(38,38,38);

      mImageON = new Bitmap(16, 16);// Soko.Common.Properties.Resources.NiceButton_DefaultIcon;
      mImageDisabled = new Bitmap(16, 16);//Soko.Common.Properties.Resources.NiceButton_DefaultIconInactive;
      Width = mImageON.Width;
      Height = mImageON.Height;
      EnabledChanged += new EventHandler(ThisEnabledChanged);
      MouseDown += new MouseEventHandler(MouseDownEH);
      MouseUp += new MouseEventHandler(MouseUpEH);
      mFocusTimer.Interval = mFocusTimerIntervalMs;
      mFocusTimer.Enabled = false;
      mFocusTimer.Elapsed += new ElapsedEventHandler(FocusTimerElapsedEventHandler);
      mFocusClickedTimer.Interval = mFocusTimerIntervalMs;
      mFocusClickedTimer.Enabled = false;
      mFocusClickedTimer.Elapsed += new ElapsedEventHandler(FocusClickedTimerElapsedEventHandler);
      ProceedContextRelations();

      CalculateSizeDependandVars();
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

    #endregion Constructors & finalizer



    #region Events & OnEvent Methods

    private void ThisEnabledChanged(object sender, EventArgs e)
    {
      ProceedContextRelations();
      Invalidate();
    }

    protected void OnPaint(object sender, PaintEventArgs e)
    {
      if (e == null)
        return;
      if (e.Graphics == null)
        return;

      //get graphics object
      Graphics g = e.Graphics;
      //get main rectangle
      Rectangle mainRect = new Rectangle(0, 0, this.Width, this.Height);

      //draw border
      //if ( mDrawBorderOnFocus && mBorderWidth > 0 )
      //{
      //  g.DrawRectangle( new Pen( mBorderColor, (float)mBorderWidth ), (float)mBorderWidth / 2.0f, (float)mBorderWidth / 2.0f, mainRect.Width - mBorderWidth - 1, mainRect.Width - mBorderWidth - 1 );
      //}

      //scale the image down and draw enabled or disabled icon
      if (mTextImageRelation != TextRelation.CenterTextNoImage)
      {
        if (this.Enabled)
        {
          RectangleF scaledRect = GetScaledRectangle(mImageON, mImageRectangle);
          g.DrawImage(mImageON, scaledRect);
        }
        else
        {
          RectangleF scaledRect = GetScaledRectangle(mImageDisabled, mImageRectangle);
          g.DrawImage(mImageDisabled, scaledRect);
        }
      }

      //draw the button label
      if (mText != string.Empty)
      {
        StringFormat formatCenter = new StringFormat
        {
          LineAlignment = StringAlignment.Center,
          Alignment = StringAlignment.Center
        };
        g.DrawString(mText, this.Font, new SolidBrush(this.ForeColor), mTextRectangle, formatCenter);
      }

      // Draw the border if its supposed to be drawn in the front
      GraphicsPath path = GetRoundedRect(new Rectangle(0, 0, this.Width - 1, this.Height - 1), mRoundingRadius);
      GraphicsPath topPath = GetTopRoundedRectHalf(new Rectangle(0, 0, this.Width - 1, this.Height - 1), mRoundingRadius);
      GraphicsPath bottomPath = GetBottomRoundedRectHalf(new Rectangle(0, 0, this.Width - 1, this.Height - 1), mRoundingRadius);
      // draw the border if it is supposed to be drawn in the bacgroung
      if (BorderWidth > 0 && mBorderColor != Color.Transparent && DrawBorderOnTop)
      {
        e.Graphics.DrawPath(new Pen(new SolidBrush(mBorderColor), BorderWidth), path);
      }

      // draw the animated backgrounds if active
      if (mFocusHooverIntensity > 0 && mDrawBorderOnFocus && _DrawBorderOnTop)
      {
        e.Graphics.DrawPath(new Pen(new SolidBrush(Color.FromArgb(mFocusHooverIntensity * mBorderColorOnFocus.A / 255, mBorderColorOnFocus)), BorderWidth), path);
      }


    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
      if (e == null)
        return;
      if (e.Graphics == null)
        return;

      //this must have been applied to support pseudo transparency
      if (this.Parent != null)
      {
        if (_SupportTransparentBackgound)
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
      }
      else
      {
        base.OnPaintBackground(e);
      }

      // Draw the background color
      if (mBackColor != Color.Transparent)
      {
        e.Graphics.FillRectangle(new SolidBrush(mBackColor), this.ClientRectangle);
      }
      //else
      //{
      //  e.Graphics.FillRectangle(new SolidBrush(Color.Green), this.ClientRectangle);
      //}

      // Draw the background image if required
      if (mDrawBackgroundImage && this.BackgroundImage != null)
      {
        e.Graphics.DrawImage(this.BackgroundImage, new Rectangle(mBackgroundImageLocation, BackgroundImage.Size));
      }


      // draw the border if it is supposed to be drawn in the bacgroung
      if (BorderWidth > 0 && mBorderColor != Color.Transparent && !_DrawBorderOnTop)
      {
        e.Graphics.DrawPath(new Pen(new SolidBrush(mBorderColor), BorderWidth), _FullPath);
      }

      // draw the animated backgrounds if active
      if (mFocusHooverIntensity > 0)
      {
        LinearGradientBrush brushFill = new LinearGradientBrush(new Rectangle(0, this.Height / 2, this.Width, this.Height), Color.FromArgb(mFocusHooverIntensity * mBackColorOnFocus1.A / 255, mBackColorOnFocus1), Color.FromArgb(mFocusHooverIntensity * mBackColorOnFocus2.A / 255, mBackColorOnFocus2), LinearGradientMode.Vertical);
        brushFill.SetBlendTriangularShape(1.0F);
        e.Graphics.FillPath(new SolidBrush(Color.FromArgb((mFocusHooverIntensity * mBackColorOnFocus2.A) / 255, mBackColorOnFocus2)), _TopHalfPath);
        e.Graphics.FillPath(brushFill, _BottomHalfPath);

        // If the button is clicked, definitely it is hovered
        if (mFocusClickedIntensity > 0)
        {
          e.Graphics.FillPath(new SolidBrush(Color.FromArgb((mFocusClickedIntensity * mBackColorOnClicked1.A) / 255, mBackColorOnClicked1)), _FullPath);
        }

        if (mDrawBorderOnFocus && !_DrawBorderOnTop)
        {
          e.Graphics.DrawPath(new Pen(new SolidBrush(Color.FromArgb(mFocusHooverIntensity * mBorderColorOnFocus.A / 255, mBorderColorOnFocus))), _FullPath);
        }
      }



    }

    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
      mFocusIntensityChangePerTimerTick = mFocusIntensityMaxValue / mFocusTimerTicksToFade;
      mFocused = true;
      mFocusTimer.Enabled = true;
      RefreshMe();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      mFocusIntensityChangePerTimerTick = mFocusIntensityMaxValue / mFocusTimerTicksToFade;
      mFocused = false;
      mFocusTimer.Enabled = true;
      RefreshMe();
    }

    private void MouseDownEH(object sender, MouseEventArgs e)
    {
      mFocusClickedIntensityChangePerTimerTick = mClickedIntensityMaxValue / mClickedTimerTicksToFade;
      mMouseIsDown = true;
      mFocusClickedTimer.Enabled = true;
      RefreshMe();
    }

    private void MouseUpEH(object sender, MouseEventArgs e)
    {
      mFocusClickedIntensityChangePerTimerTick = mClickedIntensityMaxValue / mClickedTimerTicksToFade;
      mMouseIsDown = false;
      mFocusClickedTimer.Enabled = true;
      RefreshMe();
    }




    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
      ProceedContextRelations();
      CalculateSizeDependandVars();
      this.Refresh();
    }

    #endregion Events & OnEvent Methods



    #region Threads & Timers & Methods

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

      RefreshMe();
    }

    private void FocusClickedTimerElapsedEventHandler(object sender, ElapsedEventArgs e)
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
      RefreshMe();
    }

    #endregion Threads & Timers & Methods



    #region Properties

    [System.ComponentModel.Category("Interactive animations")]
    private bool InteractiveAnimationsEnabled
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

    private bool _DrawBorderOnTop = false;
    [System.ComponentModel.Category("Interactive animations")]
    public bool DrawBorderOnTop
    {
      get { return _DrawBorderOnTop; }
      set
      {
        _DrawBorderOnTop = value;
        Invalidate();
      }
    }

    [System.ComponentModel.Category("Appearance")]
    public ColorSchemeEnum ColorScheme
    {
      get { return _ColorScheme; }
      set
      {
        _ColorScheme = value;

        Invalidate();
      }
    }

    [System.ComponentModel.Category("Appearance")]
    public bool SupportTransparentBackground
    {
      get { return _SupportTransparentBackgound; }
      set
      {
        _SupportTransparentBackgound = value;
        SetControlStyle();
        Invalidate();
      }
    }

    [System.ComponentModel.Category("Appearance")]
    public Color BackgroundColor
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

    [System.ComponentModel.Category("Appearance"),
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
        ProceedContextRelations();
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
        ProceedContextRelations();
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
        ProceedContextRelations();
        Invalidate();
      }
    }

    [System.ComponentModel.Category("Appearance")]
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
          mImageON = new Bitmap(16, 16);
        }
        else
        {
          mImageON = value;
        }
        ProceedContextRelations();
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
          mImageDisabled = new Bitmap(16, 16);
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
        ProceedContextRelations();
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
        ProceedContextRelations();
        Invalidate();
      }
    }

    [System.ComponentModel.Category("Appearance")]
    public Padding ContentPadding
    {
      get { return mContentPadding; }
      set
      {
        mContentPadding = value;
        ProceedContextRelations();
        Invalidate();
      }
    }

    #endregion Properties



    #region Private Methods

    private void SetControlStyle()
    {
      if (_SupportTransparentBackgound)
      {
        this.SetStyle(
    ControlStyles.SupportsTransparentBackColor |
    ControlStyles.OptimizedDoubleBuffer |
    ControlStyles.AllPaintingInWmPaint |
    ControlStyles.ResizeRedraw |
    ControlStyles.UserPaint, true);
      }
      else
      {
        this.SetStyle(
    ControlStyles.OptimizedDoubleBuffer |
    ControlStyles.AllPaintingInWmPaint |
    ControlStyles.ResizeRedraw |
    ControlStyles.UserPaint, true);
      }
    }

    private void CalculateSizeDependandVars()
    {
      _FullPath = GetRoundedRect(new Rectangle(0, 0, this.Width - 1, this.Height - 1), mRoundingRadius);
      _TopHalfPath = GetTopRoundedRectHalf(new Rectangle(0, 0, this.Width - 1, this.Height - 1), mRoundingRadius);
      _BottomHalfPath = GetBottomRoundedRectHalf(new Rectangle(0, 0, this.Width - 1, this.Height - 1), mRoundingRadius);
    }

    private void RefreshMe()
    {
      try
      {
        if (IsHandleCreated)
        {
          if (InvokeRequired)
          {
            this.BeginInvoke(new MethodInvoker(RefreshMe));
            return;
          }
          else
          {
            this.Refresh();
          }
        }
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
      arc.X = baseRect.Right - diameter;
      path.AddArc(arc, 270, 90);
      // bottom right arc 
      arc.Y = baseRect.Bottom - diameter;
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

    /// <summary>
    /// Calculates positions of the image and text for painting function
    /// </summary>
    private void ProceedContextRelations()
    {
      Graphics g = this.CreateGraphics();
      SizeF textSize;
      if (mText == string.Empty)
      {
        textSize = new SizeF(0.0f, 0.0f);
      }
      else
      {
        textSize = g.MeasureString(mText, this.Font);
      }
      g.Dispose();

      float contentWidth = 0;
      float contentHeight = 0;

      float imageX = 0;
      float imageY = 0;

      float textX = 0;
      float textY = 0;

      float contentXOffs = 0;
      float contentYOffs = 0;

      switch (mTextImageRelation)
      {
        case TextRelation.Underneath:
          {
            // Calculate content height
            if (mText == string.Empty)
            {
              contentHeight = mImageON.Height;
            }
            else
            {
              contentHeight = mImageON.Height + textSize.Height + mTextImageSpacing;
            }
            // Calculate content width
            contentWidth = Math.Max(mImageON.Width, textSize.Width);
            // Calculate content locations including padding 
            contentXOffs = (this.Width - mContentPadding.Left - mContentPadding.Right - contentWidth) / 2.0f;
            imageX = mContentPadding.Left + contentXOffs + (contentWidth / 2.0f) - (mImageON.Width / 2.0f);
            if (mText == string.Empty)
            {
              imageY = mContentPadding.Top + contentYOffs + (this.Height / 2.0f) - (mImageON.Height / 2.0f);
            }
            else
            {
              imageY = mContentPadding.Top;
            }
            textX = mContentPadding.Left + contentXOffs + (contentWidth / 2.0f) - (textSize.Width / 2.0f);
            textY = mContentPadding.Top + mImageON.Height + mTextImageSpacing;
            // Create appropriate rectangles for painting function
            mImageRectangle = new RectangleF(imageX, imageY, mImageON.Width, mImageON.Height);
            mTextRectangle = new RectangleF(textX, textY, textSize.Width, textSize.Height);
            break;
          }
        case TextRelation.Above:
          {
            // Calculate content height
            if (mText == string.Empty)
            {
              contentHeight = mImageON.Height;
            }
            else
            {
              contentHeight = mImageON.Height + textSize.Height + mTextImageSpacing;
            }
            // Calculate content width
            contentWidth = Math.Max(mImageON.Width, textSize.Width);
            // Calculate content locations including padding 
            contentXOffs = (this.Width - mContentPadding.Left - mContentPadding.Right - contentWidth) / 2.0f;
            imageX = mContentPadding.Left + contentXOffs + (contentWidth / 2.0f) - (mImageON.Width / 2.0f);
            if (mText == string.Empty)
            {
              imageY = mContentPadding.Top + contentYOffs + (this.Height / 2.0f) - (mImageON.Height / 2.0f);
            }
            else
            {
              imageY = this.Height - mContentPadding.Bottom - mImageON.Height;
            }

            textX = mContentPadding.Left + contentXOffs + (contentWidth / 2.0f) - (textSize.Width / 2.0f);
            textY = this.Height - mContentPadding.Bottom - mImageON.Height - TextImageSpacing - textSize.Height;
            // Create appropriate rectangles for painting function
            mImageRectangle = new RectangleF(imageX, imageY, mImageON.Width, mImageON.Height);
            mTextRectangle = new RectangleF(textX, textY, textSize.Width, textSize.Height);
            break;
          }
        case TextRelation.OnLeft:
          {
            // Calculate content height
            contentHeight = Math.Max(mImageON.Height, textSize.Height);
            // Calculate content width
            if (mText == string.Empty)
            {
              contentWidth = mImageON.Width;
            }
            else
            {
              contentWidth = mImageON.Width + textSize.Width + mTextImageSpacing;
            }
            // Calculate content locations including padding 
            contentYOffs = (this.Height - mContentPadding.Top - mContentPadding.Bottom - contentHeight) / 2.0f;
            imageX = this.Width - mContentPadding.Right - mImageON.Width;
            imageY = mContentPadding.Top + contentYOffs + (contentHeight / 2.0f) - (mImageON.Height / 2.0f);
            textX = this.Width - mContentPadding.Right - mImageON.Width - TextImageSpacing - textSize.Width;
            textY = mContentPadding.Top + contentYOffs + (contentHeight / 2.0f) - (textSize.Height / 2.0f); ;
            // Create appropriate rectangles for painting function
            mImageRectangle = new RectangleF(imageX, imageY, mImageON.Width, mImageON.Height);
            mTextRectangle = new RectangleF(textX, textY, textSize.Width, textSize.Height);
            break;
          }
        case TextRelation.OnRight:
          {
            // Calculate content height
            contentHeight = Math.Max(mImageON.Height, textSize.Height);
            // Calculate content width
            if (mText == string.Empty)
            {
              contentWidth = mImageON.Width;
            }
            else
            {
              contentWidth = mImageON.Width + textSize.Width + mTextImageSpacing;
            }
            // Calculate content locations including padding 
            contentYOffs = (this.Height - mContentPadding.Top - mContentPadding.Bottom - contentHeight) / 2.0f;
            imageX = mContentPadding.Left;
            imageY = mContentPadding.Top + contentYOffs + (contentHeight / 2.0f) - (mImageON.Height / 2.0f);
            textX = mContentPadding.Left + mImageON.Width + TextImageSpacing;
            textY = mContentPadding.Top + contentYOffs + (contentHeight / 2.0f) - (textSize.Height / 2.0f); ;
            // Create appropriate rectangles for painting function
            mImageRectangle = new RectangleF(imageX, imageY, mImageON.Width, mImageON.Height);
            mTextRectangle = new RectangleF(textX, textY, textSize.Width, textSize.Height);
            break;
          }
        case TextRelation.CenterTextNoImage:
          {
            mTextRectangle = this.ClientRectangle;
            break;
          }
      }
      //this.Width = (int)(mWidth + 0.6f);
      //this.Height = (int)(mHeight + 0.6f);
    }

    #endregion Private Methods



    #region Public Methods



    #endregion Public Methods














  }
}
