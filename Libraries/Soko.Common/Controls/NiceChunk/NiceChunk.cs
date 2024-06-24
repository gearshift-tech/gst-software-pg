using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;
using System.Windows.Forms;

namespace Soko.Common.Controls
{

  /// <remarks>
  /// Nice panel with gradient background and rounded corners
  /// </remarks>
  public partial class NiceChunk: Panel
  {


    #region Constants

    private readonly int mFocusTimerIntervalMs = 33;
    private readonly int mFocusIntensityMaxValue = 100;
    private readonly int mFocusTimerTicksToFade = 6;

    #endregion  Constants



    #region Private fields


    private Color mTopColorOnFocus = Color.Gainsboro;
    private Color mBottomColorOnFocus = Color.Black;
    private bool mFocused = false;
    private int mFocusHooverIntensity = 0;
    private int mFocusIntensityChangePerTimerTick = 0;

    private System.Timers.Timer mFocusTimer = new System.Timers.Timer();

    private Color mTopColor = Color.FromArgb(191, 197, 206);
    private Color mMiddleColor = Color.FromArgb(180, 187, 197);
    private Color mBottomColor = Color.FromArgb(165, 166, 166);

    private readonly int mRoundingRadius = 3;

    private int mHorizontalMargin = 5;
    private int mVerticalMargin = 5;

    private int mUpperBreakPerc = 18;
    private int mBottomBarHeightPerc = 18;

    private bool mAutoplaceElements = false;

    private String mText = String.Empty;



    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    #endregion Private fields



    #region Constructors & finalizer

    /// <summary>
    /// The default cunstructor
    /// </summary>
    public NiceChunk()
    {
      this.SuspendLayout();
      this.DockChanged += new System.EventHandler(this.NicePanel_DockChanged);
      this.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.NicePanel_ControlAdded);
      this.Resize += new System.EventHandler(this.NicePanel_Resize);
      this.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.NicePanel_ControlRemoved);
      this.ResumeLayout(false);

      this.SetStyle(
                    ControlStyles.SupportsTransparentBackColor |
                    ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.ResizeRedraw |
                    ControlStyles.UserPaint, true);

      this.Click += new EventHandler(ScrollablePanel_Click);

      PlaceControls();

      mFocusTimer.Interval = mFocusTimerIntervalMs;
      mFocusTimer.Enabled = false;
      mFocusTimer.Elapsed += new ElapsedEventHandler(FocusTimerElapsedEventHandler);

    }

    #endregion Constructors & finalizer



    #region Events


    #endregion Events



    #region Properties

    [System.ComponentModel.Category("Appearance")]
    public Color TopColor
    {
      get { return mTopColor; }
      set
      {
        mTopColor = value;
        Invalidate();
      }
    }

    [System.ComponentModel.Category("Appearance")]
    public Color MiddleColor
    {
      get { return mMiddleColor; }
      set
      {
        mMiddleColor = value;
        Invalidate();
      }
    }

    [System.ComponentModel.Category("Appearance")]
    public Color BottomColor
    {
      get { return mBottomColor; }
      set
      {
        mBottomColor = value;
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
        if (value == null)
        {
          mText = String.Empty;
        }
        else
        {
          mText = value;
        }
        Invalidate();
      }
    }

    /// <summary>
    /// gets/sets if elements on panel should be placed automatically;
    /// </summary>    
    public bool AutoplaceElements
    {
      get { return mAutoplaceElements; }
      set
      {
        mAutoplaceElements = value;
        PlaceControls();
      }
    }


    /// <summary>
    /// 
    /// </summary>
    public int HorizontalMargin
    {
      get { return mHorizontalMargin; }
      set
      {
        if (value < 0)
          mHorizontalMargin = 0;
        else
          mHorizontalMargin = value;
        PlaceControls();
      }
    }

    /// <summary>
    /// 
    /// </summary>
    public int VerticalMargin
    {
      get { return mVerticalMargin; }
      set
      {
        if (value < 0)
          mVerticalMargin = 0;
        else
          mVerticalMargin = value;
        PlaceControls();
      }
    }


    #endregion Properties



    #region Methods

    /// <summary>
    /// Automatically places controls on the panel
    /// </summary>
    private void PlaceControls()
    {
      //exit if elements should not be placed automatically
      if (!mAutoplaceElements)
        return;
      //exit if there's nothing to place
      if (Controls.Count <= 0)
        return;
      float xPadding = 0;
      float yPadding = 0;
      //calculate the rectangle dimensions of controls grid
      int maxControlWidth = 1;
      int maxControlHeight = 1;
      foreach (Control ctrl in Controls)
      {
        maxControlWidth = Math.Max(maxControlWidth, ctrl.Width);
        maxControlHeight = Math.Max(maxControlHeight, ctrl.Height);
      }
      //calculate number of rows and columns
      int gridColumnsCount = (this.Width - mHorizontalMargin * 2) / (maxControlWidth + (int)xPadding);
      //in case there are fewer controls than can fit the row
      if (gridColumnsCount > Controls.Count)
        gridColumnsCount = Controls.Count;
      //in case no controls can fit the column
      if (gridColumnsCount == 0)
        gridColumnsCount = 1;
      int gridRowCount = Controls.Count / gridColumnsCount;
      if (Controls.Count % gridColumnsCount != 0)
        gridRowCount += 1;
      //calculate new padding values
      xPadding = ((this.Width - mHorizontalMargin * 2) - ((float)maxControlWidth * (float)gridColumnsCount)) / (float)gridColumnsCount;
      yPadding = ((this.Height - mVerticalMargin * 2) - ((float)maxControlHeight * (float)gridRowCount)) / (float)gridRowCount;
      //if this is too small to fit controls
      if (xPadding < 0)
        xPadding = 0;
      if (yPadding < 0)
        yPadding = 0;
      //controls must be placed centered
      float xPos = mHorizontalMargin + xPadding / 2.0f;
      float yPos = mVerticalMargin + yPadding / 2.0f;

      //place each control
      for (int y = 0; y < gridRowCount; y++)
      {
        xPos = mHorizontalMargin + xPadding / 2.0f;
        for (int x = 0; x < gridColumnsCount; x++)
        {
          int index = y * gridColumnsCount + x;
          if (index < Controls.Count)
          {
            Controls[index].Location = new Point((int)xPos, (int)yPos);
          }
          xPos += maxControlWidth + xPadding;
        }
        yPos += maxControlHeight + yPadding;
      }
    }

    /// <summary>
    /// On background paint
    /// </summary>
    /// <param name="e"></param>
    protected override void OnPaintBackground(PaintEventArgs e)
    {

      #region this must have been applied to support pseudo transparency
      if (e == null)
        return;
      if (e.Graphics == null)
        return;

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
        base.OnPaintBackground(e);
      #endregion


      Rectangle mainRect = new Rectangle(0, 0, this.Width, this.Height);
      if (mainRect.Height > 0 && mainRect.Width > 0)
      {
        Graphics g = e.Graphics;

        // Fill top lighter bar
        LinearGradientBrush brushFill;
        float upperBarHeight = (mUpperBreakPerc / 100.0f) * this.Height;
        brushFill = new LinearGradientBrush(new RectangleF(0, 0, this.Width, upperBarHeight), mTopColor, ControlPaint.LightLight(mTopColor), LinearGradientMode.Vertical);
        brushFill.SetSigmaBellShape(0.0F);
        brushFill.WrapMode = WrapMode.TileFlipXY;
        g.FillPath(brushFill, GetTopLighterBar());

        // Fill the middle
        float middleBottom = ((100 - (float)mBottomBarHeightPerc) / 100.0f) * this.Height;
        RectangleF middleRect = new RectangleF(0, upperBarHeight - 1, this.Width, middleBottom - upperBarHeight);
        brushFill = new LinearGradientBrush(middleRect, ControlPaint.Light(mMiddleColor), mMiddleColor, LinearGradientMode.Vertical);
        brushFill.SetSigmaBellShape(0.0F);
        brushFill.WrapMode = WrapMode.TileFlipXY;
        g.FillRectangle(brushFill, middleRect);

        // Fill bottom bar
        RectangleF bottomRect = new RectangleF(0, middleBottom, this.Width, (this.Height - middleBottom) * 2);
        brushFill = new LinearGradientBrush(bottomRect, ControlPaint.Dark(mBottomColor), mBottomColor, LinearGradientMode.Vertical);
        brushFill.SetSigmaBellShape(0.0F);
        brushFill.WrapMode = WrapMode.TileFlipXY;
        g.FillPath(brushFill, GetBottomBarPath());

        // Draw the border
        g.DrawPath(new Pen(new SolidBrush(mBottomColor)), GetControlPath());

        // Draw mouse-over overlays
        if (mFocusHooverIntensity > 0)
        {
          SolidBrush topBrush = new SolidBrush(Color.FromArgb(mFocusHooverIntensity, mTopColorOnFocus));
          SolidBrush bottomBrush = new SolidBrush(Color.FromArgb(mFocusHooverIntensity, mBottomColorOnFocus));
          g.FillPath(topBrush, GetTopLighterBar());
          g.FillRectangle(topBrush, middleRect);
          g.FillPath(bottomBrush, GetBottomBarPath());
          g.DrawPath(new Pen(bottomBrush), GetControlPath());
        }

        // Draw the side lines
        g.DrawLine(new Pen(new SolidBrush(Color.White)), 1, mRoundingRadius, 1, bottomRect.Top-2);
        g.DrawLine(new Pen(new SolidBrush(Color.White)), this.Width - 2, mRoundingRadius, this.Width - 2, bottomRect.Top-2);

        // Draw bottom bar text
        bottomRect = new RectangleF(0, middleBottom, this.Width, this.Height - middleBottom);
        StringFormat sformat = new StringFormat
        {
          Alignment = StringAlignment.Center
        };
        g.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), bottomRect, sformat);
      }

    }

    /// <summary>
    /// On panel resize
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NicePanel_Resize(object sender, EventArgs e)
    {
      PlaceControls();
    }

    /// <summary>
    /// On control added
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NicePanel_ControlAdded(object sender, ControlEventArgs e)
    {
      PlaceControls();
    }

    /// <summary>
    /// On control removed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NicePanel_ControlRemoved(object sender, ControlEventArgs e)
    {
      PlaceControls();
    }

    /// <summary>
    /// On dock change
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NicePanel_DockChanged(object sender, EventArgs e)
    {
      PlaceControls();
    }

    /// <summary>
    /// Returns rounded rectangle graphics path
    /// </summary>
    /// <param name="baseRect"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Returns top rounded rectangle graphics path
    /// </summary>
    /// <param name="baseRect"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    private GraphicsPath GetTopRoundedRect(RectangleF baseRect, float radius)
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
      path.CloseFigure();
      return path;
    }

    /// <summary>
    /// Returns bottom rounded rectangle graphics path
    /// </summary>
    /// <param name="baseRect"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    private GraphicsPath GetBottomRoundedRect(RectangleF baseRect, float radius)
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
    /// Returns capsule graphics path within specified rectangle
    /// </summary>
    /// <param name="baseRect"></param>
    /// <returns></returns>
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


    private void ScrollablePanel_Click(object sender, EventArgs e)
    {
      this.Focus();
    }

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }


    /// <summary>
    /// Returns top rounded rectangle graphics path
    /// </summary>
    /// <param name="baseRect"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    private GraphicsPath GetTopLighterBar()
    {
      float height = (mUpperBreakPerc / 100.0f) * this.Height;
      // if corner radius is less than or equal to zero, 
      // return the original rectangle 
      if (mRoundingRadius <= 0.0F)
      {
        GraphicsPath mPath = new GraphicsPath();
        mPath.AddRectangle(new Rectangle(0, 0, this.Width, (int)height));
        mPath.CloseFigure();
        return mPath;
      }
      // if the corner radius is greater than 
      // the width, or height (whichever is shorter) 
      // then return a capsule instead of a lozenge 
      if (mRoundingRadius > (Math.Min(this.Width, height)))
        return GetCapsule(new Rectangle(0, 0, this.Width, (int)height));
      // create the arc for the rectangle sides and declare 
      // a graphics path object for the drawing 
      float diameter = mRoundingRadius * 2.0F;
      SizeF sizeF = new SizeF(diameter, diameter);
      RectangleF arc = new RectangleF(new Point(0,0), sizeF);
      GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
      // top left arc 
      path.AddArc(arc, 180, 90);
      // top right arc 
      arc.X = this.Width - diameter - 1;
      path.AddArc(arc, 270, 90);
      path.AddLine(new Point(this.Width, (int)height), new Point(0, (int)height));
      path.CloseFigure();
      return path;
    }

    private GraphicsPath GetBottomBarPath()
    {
      int height = (int)((((float)mBottomBarHeightPerc) / 100.0f) * this.Height);
      int heightPt = (int)(((100 - (float)mBottomBarHeightPerc) / 100.0f) * this.Height);
      // if corner radius is less than or equal to zero, 
      // return the original rectangle 
      if (mRoundingRadius <= 0.0F)
      {
        GraphicsPath mPath = new GraphicsPath();
        mPath.AddRectangle(new Rectangle(0, heightPt, this.Width, height));
        mPath.CloseFigure();
        return mPath;
      }
      // if the corner radius is greater than 
      // the width, or height (whichever is shorter) 
      // then return a capsule instead of a lozenge 
      if (mRoundingRadius > (Math.Min(this.Width, this.Height)))
        return GetCapsule(new Rectangle(0, heightPt, this.Width, height));
      // create the arc for the rectangle sides and declare 
      // a graphics path object for the drawing 
      float diameter = mRoundingRadius * 2.0F;
      SizeF sizeF = new SizeF(diameter, diameter);
      RectangleF arc = new RectangleF(new Point(0,0), sizeF);
      GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
      path.AddLine(new Point(0, heightPt), new Point(this.Width, heightPt));
      // bottom right arc 
      arc.X = this.ClientRectangle.Right - diameter -1;
      arc.Y = ClientRectangle.Bottom - diameter -1;
      path.AddArc(arc, 0, 90);
      // bottom left arc
      arc.X = ClientRectangle.Left;
      path.AddArc(arc, 90, 90);
      path.CloseFigure();
      return path;
    }

    private GraphicsPath GetControlPath()
    {
      // if corner radius is less than or equal to zero, 
      // return the original rectangle 
      if (mRoundingRadius <= 0.0F)
      {
        GraphicsPath mPath = new GraphicsPath();
        mPath.AddRectangle(new Rectangle(0, 0, this.Width, this.Height));
        mPath.CloseFigure();
        return mPath;
      }
      // if the corner radius is greater than 
      // the width, or height (whichever is shorter) 
      // then return a capsule instead of a lozenge 
      if (mRoundingRadius > (Math.Min(this.Width, this.Height)))
        return GetCapsule(new Rectangle(0, 0, this.Width, this.Height));
      // create the arc for the rectangle sides and declare 
      // a graphics path object for the drawing 
      float diameter = mRoundingRadius * 2.0F;
      SizeF sizeF = new SizeF(diameter, diameter);
      RectangleF arc = new RectangleF(new Point(0, 0), sizeF);
      GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
      // top left arc 
      path.AddArc(arc, 180, 90);
      // top right arc 
      arc.X = this.Width - diameter - 1;
      path.AddArc(arc, 270, 90);
      // bottom right arc 
      arc.Y = this.Height - diameter - 1;
      path.AddArc(arc, 0, 90);
      // bottom left arc
      arc.X = 0;
      path.AddArc(arc, 90, 90);
      path.CloseFigure();
      return path;
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
      mFocusIntensityChangePerTimerTick = mFocusIntensityMaxValue / mFocusTimerTicksToFade;
      mFocused = true;
      mFocusTimer.Enabled = true;
      this.BeginInvoke(new MethodInvoker(RefreshMe));

    }

    protected override void OnMouseLeave(EventArgs e)
    {
      if (this.ClientRectangle.Contains(this.PointToClient(Control.MousePosition)))
      {
        return;
      }
      else
      {

        base.OnMouseLeave(e);
        mFocusIntensityChangePerTimerTick = mFocusIntensityMaxValue / mFocusTimerTicksToFade;
        mFocused = false;
        mFocusTimer.Enabled = true;
        this.BeginInvoke(new MethodInvoker(RefreshMe));
      }
    }

    protected override void OnControlAdded(ControlEventArgs e)
    {
      base.OnControlAdded(e);
      e.Control.MouseEnter += new EventHandler(ChildMouseEnterEH);
      e.Control.MouseLeave += new EventHandler(ChildMouseLeaveEH);
    }


    private void ChildMouseEnterEH(object sender, EventArgs e)
    {
      OnMouseEnter(e);
    }

    private void ChildMouseLeaveEH(object sender, EventArgs e)
    {
      OnMouseLeave(e);
    }

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

      this.BeginInvoke(new MethodInvoker(RefreshMe));
    }

    private void RefreshMe()
    {
      this.Refresh();
    }

    #endregion Methods

  }
}
