using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Soko.Common.Controls
{

  /// <remarks>
  /// Nice panel with gradient background and rounded corners
  /// </remarks>
  public partial class NicePanel : Panel
  {

    public enum GradientType
    {
      None = 0x00,
      HorizontalEdges = 0x01,
      LinearHorizontalUpper = 0x02,
      LinearHorizontalMiddle = 0x03,
      SigmaBellHorizontalUpper = 0x04,
      SigmaBellHorizontalMiddle = 0x05
    }

    private enum SB_Type
    {
      SB_LINEUP = 0,
      SB_LINEDOWN = 1,
      SB_PAGEUP = 2,
      SB_PAGEDOWN = 3,
      SB_THUMBPOSITION = 4,
      SB_THUMBTRACK = 5,
      SB_TOP = 6,
      SB_BOTTOM = 7,
      SB_ENDSCROLL = 8,
    }

    private enum WM_Type
    {
      WM_HSCROLL = 0x114,
      WM_VSCROLL = 0x115,
      WM_MOUSEWHEEL = 0x020A,
      WM_NCCALCSIZE = 0x0083,
      WM_PAINT = 0x000F,
      WM_SIZE = 0x0005
    }

    private enum MK_Type
    {
      MK_LBUTTON = 0x01,
      MK_RBUTTON = 0x02,
      MK_SHIFT = 0x04,
      MK_CONTROL = 0x08,
      MK_MBUTTON = 0x10,
      MK_XBUTTON1 = 0x0020,
      MK_XBUTTON2 = 0x0040
    }

    #region Constants

    private readonly uint SB_HORZ = 0;
    private readonly uint SB_VERT = 1;
    //private readonly uint SB_CTL = 2; 
    //private readonly uint SB_BOTH = 3; 

    private readonly uint ESB_DISABLE_BOTH = 0x3;
    private readonly uint ESB_ENABLE_BOTH = 0x0;

    #endregion  Constants



    #region Private fields

    private Color mBackgroundColor1 = Color.WhiteSmoke;
    private Color mBackgroundColor2 = Color.Wheat;
    private int mRoundingRadius = 0;

    public int mHorizontalMargin = 0;
    public int mVerticalMargin = 0;
    public bool mAutoplaceElements = false;
    public bool mAutoSizeElements = false;

    GradientType mGradient = GradientType.None;

    private bool enableAutoHorizontal = false;
    private bool enableAutoVertical = false;
    private bool visibleAutoHorizontal = false;
    private bool visibleAutoVertical = false;

    private int autoScrollHorizontalMinimum = 0;
    private int autoScrollHorizontalMaximum = 100;

    private int autoScrollVerticalMinimum = 0;
    private int autoScrollVerticalMaximum = 100;

    private bool mDrawBackImage = false;

    private Color mBorderColor = Color.FromArgb(255, 0, 176, 240);
    private int mBorderWidth = 1;

    private bool _SupportTransparentBackgound = false;

    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    #endregion Private fields



    #region Constructors & finalizer

    /// <summary>
    /// The default cunstructor
    /// </summary>
    public NicePanel()
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
                    ControlStyles.UserPaint, true );

      this.Click += new EventHandler( ScrollablePanel_Click );
      //this.AutoScroll = true;
      //mAutoplaceElements = true;
      //for (int i = 0; i < 24; i++)
      //{
      //  Button btn = new Button();
      //  btn.Width = 100;
      //  btn.Height = 30;
      //  this.Controls.Add(btn);
      //}

      PlaceControls();

    }

    #endregion Constructors & finalizer



    #region Events

    public event System.Windows.Forms.ScrollEventHandler ScrollHorizontal;
    public event System.Windows.Forms.ScrollEventHandler ScrollVertical;
    public event System.Windows.Forms.MouseEventHandler ScrollMouseWheel;

    #endregion Events



    #region Properties

    [System.ComponentModel.Category("Appearance")]
    public bool SupportTransparentBackground
    {
      get { return _SupportTransparentBackgound; }
      set
      {
        _SupportTransparentBackgound = value;
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

    public Color backgroundColor1
    {
      get { return mBackgroundColor1; }
      set
      {
        mBackgroundColor1 = value;
        Invalidate();
      }
    }

    public Color backgroundColor2
    {
      get { return mBackgroundColor2; }
      set
      {
        mBackgroundColor2 = value;
        Invalidate();
      }
    }

    public int roundingRadius
    {
      get { return mRoundingRadius; }
      set
      {
        mRoundingRadius = value;
        Invalidate();
      }
    }

    public GradientType Gradient
    {
      get { return mGradient; }
      set
      {
        mGradient = value;
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
    /// gets/sets if elements on panel should be zised automatically;
    /// </summary>    
    public bool AutoSizeElements
    {
      get { return mAutoSizeElements; }
      set
      {
        mAutoSizeElements = value;
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
        if ( value < 0 )
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
        if ( value < 0 )
          mVerticalMargin = 0;
        else
          mVerticalMargin = value;
        PlaceControls();
      }
    }


    [System.ComponentModel.Category( "Appearance" )]
    public int AutoScrollHPos
    {
      get { return GetScrollPos( this.Handle, (int)SB_HORZ ); }
      set { SetScrollPos( this.Handle, (int)SB_HORZ, value, true ); }
    }

    [System.ComponentModel.Category( "Appearance" )]
    public int AutoScrollVPos
    {
      get { return GetScrollPos( this.Handle, (int)SB_VERT ); }
      set { SetScrollPos( this.Handle, (int)SB_VERT, value, true ); }
    }

    [System.ComponentModel.Category( "Appearance" )]
    public int AutoScrollHorizontalMinimum
    {
      get { return this.autoScrollHorizontalMinimum; }
      set
      {
        this.autoScrollHorizontalMinimum = value;
        SetScrollRange( this.Handle, (int)SB_HORZ, autoScrollHorizontalMinimum, autoScrollHorizontalMaximum, true );
      }
    }

    [System.ComponentModel.Category( "Appearance" )]
    public int AutoScrollHorizontalMaximum
    {
      get { return this.autoScrollHorizontalMaximum; }
      set
      {
        this.autoScrollHorizontalMaximum = value;
        SetScrollRange( this.Handle, (int)SB_HORZ, autoScrollHorizontalMinimum, autoScrollHorizontalMaximum, true );
      }
    }

    [System.ComponentModel.Category( "Appearance" )]
    public int AutoScrollVerticalMinimum
    {
      get { return this.autoScrollVerticalMinimum; }
      set
      {
        this.autoScrollVerticalMinimum = value;
        SetScrollRange( this.Handle, (int)SB_VERT, autoScrollHorizontalMinimum, autoScrollHorizontalMaximum, true );
      }
    }

    [System.ComponentModel.Category( "Appearance" )]
    public int AutoScrollVerticalMaximum
    {
      get { return this.autoScrollVerticalMaximum; }
      set
      {
        this.autoScrollVerticalMaximum = value;
        SetScrollRange( this.Handle, (int)SB_VERT, autoScrollHorizontalMinimum, autoScrollHorizontalMaximum, true );
      }
    }

    [System.ComponentModel.Category( "Appearance" )]
    public bool EnableAutoScrollHorizontal
    {
      get { return this.enableAutoHorizontal; }
      set
      {
        this.enableAutoHorizontal = value;
        if ( value )
          EnableScrollBar( this.Handle, SB_HORZ, ESB_ENABLE_BOTH );
        else
          EnableScrollBar( this.Handle, SB_HORZ, ESB_DISABLE_BOTH );
      }
    }

    [System.ComponentModel.Category( "Appearance" )]
    public bool EnableAutoScrollVertical
    {
      get { return this.enableAutoVertical; }
      set
      {
        this.enableAutoVertical = value;
        if ( value )
          EnableScrollBar( this.Handle, SB_VERT, ESB_ENABLE_BOTH );
        else
          EnableScrollBar( this.Handle, SB_VERT, ESB_DISABLE_BOTH );
      }
    }

    [System.ComponentModel.Category( "Appearance" )]
    public bool VisibleAutoScrollHorizontal
    {
      get { return this.visibleAutoHorizontal; }
      set
      {
        this.visibleAutoHorizontal = value;
        ShowScrollBar( this.Handle, (int)SB_HORZ, value );
      }
    }

    [System.ComponentModel.Category( "Appearance" )]
    public bool VisibleAutoScrollVertical
    {
      get { return this.visibleAutoVertical; }
      set
      {
        this.visibleAutoVertical = value;
        ShowScrollBar( this.Handle, (int)SB_VERT, value );
      }
    }

    [System.ComponentModel.Category("Appearance")]
    public bool DrawBackImage
    {
      get { return mDrawBackImage; }
      set
      {
        mDrawBackImage = value;
        this.Refresh();
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
      if ( !mAutoplaceElements )
        return;
      //exit if there's nothing to place
      if (Controls.Count <= 0 )
        return;
      float xPadding = 0;
      float yPadding = 0;
      //calculate the rectangle dimensions of controls grid
      int minGridWidth = 1;
      int minGridHeight = 1;

      //int maxGridWidth = 1;
      int maxGridHeight = 1;

      //// THIS WILL WORK FOR THE OBJECTS OF THE SAME SIZE !!!!!!!!
      //if (mAutoSizeElements)
      //{
      //  // Get the minimum width and height required to fit all of the controls
      //  int minWidth = 0;
      //  int minHeight = 0;
      //  foreach (Control ctrl in Controls)
      //  {
      //    if (ctrl.AutoSize)
      //    {
      //      minWidth += ctrl.MinimumSize.Width;
      //      minHeight = Math.Max(minHeight, ctrl.MinimumSize.Height);
      //    }
      //    else
      //    {
      //      minWidth += ctrl.Size.Width;
      //      minHeight = Math.Max(minHeight, ctrl.Size.Height);
      //    }
      //  }
      //  // Find the minimum number of rows to fit the controls
      //  int minRowCount = (int)Math.Ceiling( (float)minWidth / (float)this.Width );
      //  // Calculate the grid size
      //  gridWidth = (int)(((float)this.Width * minRowCount) / (float)this.Controls.Count);
      //  gridHeight = minHeight;

      //  foreach (Control ctrl in Controls)
      //  {
      //    if (ctrl.AutoSize)
      //    {
      //      ctrl.Size = new Size(gridWidth, gridHeight);
      //    }
      //  }      
      //}

      if (mAutoSizeElements)
      {
        foreach (Control ctrl in Controls)
        {
          if (ctrl.AutoSize)
          {
            minGridWidth = Math.Max(minGridWidth, ctrl.MinimumSize.Width);
            minGridHeight = Math.Max(minGridHeight, ctrl.MinimumSize.Height);
            maxGridHeight = Math.Max(maxGridHeight, ctrl.MaximumSize.Height);
          }
          else
          {
            minGridWidth = Math.Max(minGridWidth, ctrl.Width);
            minGridHeight = Math.Max(minGridHeight, ctrl.Height);
            maxGridHeight = Math.Max(maxGridHeight, ctrl.Height);
          }
        }
      }
      else
      {
        foreach (Control ctrl in Controls)
        {
          minGridWidth = Math.Max(minGridWidth, ctrl.Width);
          minGridHeight = Math.Max(minGridHeight, ctrl.Height);
          maxGridHeight = Math.Max(maxGridHeight, ctrl.Height);
        }
      }


      //calculate number of rows and columns
      int gridColumnsCount = ( this.Width  ) / ( minGridWidth );
      //in case there are fewer controls than can fit the row
      if ( gridColumnsCount > Controls.Count )
        gridColumnsCount = Controls.Count;
      //in case no controls can fit the column
      if ( gridColumnsCount == 0 )
        gridColumnsCount = 1;
      int gridRowCount = (int)Math.Ceiling((float)Controls.Count / gridColumnsCount);

      if (mAutoSizeElements)
      {
        // We have the minimum possible grid size now. Check if possible to enlarde grid size
        int optimalHeight = (int)(minGridHeight + 0.3 * (maxGridHeight - minGridHeight));
        int maxRowCount = (int)Math.Floor((float)this.Height / optimalHeight);

        // Decide if to to do this or not
        gridRowCount = (int)Math.Max((float)maxRowCount, gridRowCount);

        // Calculate the new number of columns
        gridColumnsCount = (int)Math.Ceiling((float)Controls.Count / gridRowCount);

        // Delete the unneeded rows
        gridRowCount = (int)Math.Ceiling((float)this.Controls.Count / gridColumnsCount);

        // Calculate the new grid size
        minGridWidth = (int)Math.Max((float)this.Width / gridColumnsCount, minGridWidth);
        minGridHeight = (int)Math.Max((float)this.Height / gridRowCount, minGridHeight);

        // Resize all of the controls to maximum size of the grid minus the control padding
        foreach (Control ctrl in Controls)
        {
          ctrl.Size = new Size(minGridWidth - ctrl.Padding.Left - ctrl.Padding.Right, minGridHeight - ctrl.Padding.Top - ctrl.Padding.Bottom);
        }
      }
      else
      {
        // Calculate the new grid size
        minGridWidth = (int)Math.Max((float)this.Width / gridColumnsCount, minGridWidth);
        minGridHeight = (int)Math.Max((float)this.Height / gridRowCount, minGridHeight);
      }

      //calculate new padding values
      xPadding = ( ( this.Width - mHorizontalMargin * 2 ) - ( (float)minGridWidth * (float)gridColumnsCount ) ) / (float)gridColumnsCount;
      yPadding = ( ( this.Height - mVerticalMargin * 2 ) - ( (float)minGridHeight * (float)gridRowCount ) ) / (float)gridRowCount;
      //if this is too small to fit controls
      if ( xPadding < 0 )
        xPadding = 0;
      if ( yPadding < 0 )
        yPadding = 0;
      //controls must be placed centered
      int xBasePos = 0;// mHorizontalMargin + xPadding / 2.0f;
      int yBasePos = 0;// mVerticalMargin + yPadding / 2.0f;

      //place each control
      for ( int y = 0; y < gridRowCount; y++ )
      {
        yBasePos = y * minGridHeight;
        for ( int x = 0; x < gridColumnsCount; x++ )
        {
          xBasePos = x * minGridWidth;

          int index = y * gridColumnsCount + x;
          if ( index < Controls.Count )
          {
            Control ctrl = this.Controls[index];
            int internalXOffs = (minGridWidth - ctrl.Width) / 2;
            int internalYOffs = (minGridHeight - ctrl.Height) / 2;
            Controls[index].Location = new Point( xBasePos + internalXOffs, yBasePos + internalYOffs );
          }
        }
      }
    }

    protected override void OnControlAdded(ControlEventArgs e)
    {
      base.OnControlAdded(e);
      PlaceControls();
    }

    protected override void OnControlRemoved(ControlEventArgs e)
    {
      base.OnControlRemoved(e);
      PlaceControls();
    }

    /// <summary>
    /// On background paint
    /// </summary>
    /// <param name="e"></param>
    protected override void OnPaintBackground( PaintEventArgs e )
    {

      
      if ( e == null )
        return;
      if ( e.Graphics == null )
        return;

      #region this must have been applied to support pseudo transparency
      if ( this.Parent != null )
      {
        if (_SupportTransparentBackgound) // If pseudo transparency is supported, draw the background
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
        base.OnPaintBackground( e );
      #endregion


        Rectangle mainRect = new Rectangle(BorderWidth, BorderWidth, this.Width-(BorderWidth*2), this.Height- (BorderWidth * 2));
        if (mainRect.Height > 0 && mainRect.Width > 0)
        {
          Graphics g = e.Graphics;
        #region rectangles
        Rectangle upperRoundRect = new Rectangle
        {
          X = mainRect.X,
          Width = mainRect.Width,
          Y = mainRect.Y,
          Height = mRoundingRadius
        };
        Rectangle lowerRoundRect = new Rectangle
        {
          X = mainRect.X,
          Width = mainRect.Width,
          Y = mainRect.Height - mRoundingRadius,
          Height = mRoundingRadius
        };
        Rectangle unroundedRect = new Rectangle
        {
          X = mainRect.X,
          Width = mainRect.Width,
          Y = mRoundingRadius,
          Height = mainRect.Height - 2 * mRoundingRadius
        };
        #endregion

        switch (mGradient)
          {
            case GradientType.None:
              {
                //SolidBrush brush = new SolidBrush(mBackgroundColor1);
                //g.FillRectangle(brush, mainRect);
                if (this.BackColor != Color.Transparent)
                {
                  SolidBrush brush = new SolidBrush(this.BackColor);
                  g.FillRectangle(brush, mainRect);
                }
                break;
              }
            case GradientType.HorizontalEdges:
              {
                LinearGradientBrush brushFill;
                brushFill = new LinearGradientBrush(upperRoundRect, mBackgroundColor1, mBackgroundColor2, LinearGradientMode.Vertical);
                brushFill.SetBlendTriangularShape(0.0F);
                brushFill.WrapMode = WrapMode.TileFlipXY;
                GraphicsPath path = GetTopRoundedRect(upperRoundRect, mRoundingRadius);
                g.FillPath(brushFill, path);
                brushFill = new LinearGradientBrush(lowerRoundRect, mBackgroundColor1, mBackgroundColor2, LinearGradientMode.Vertical);
                brushFill.SetBlendTriangularShape(1.0F);
                brushFill.WrapMode = WrapMode.TileFlipXY;
                path = GetBottomRoundedRect(lowerRoundRect, mRoundingRadius);
                g.FillPath(brushFill, path);
                SolidBrush solidbrush = new SolidBrush(mBackgroundColor1);
                g.FillRectangle(solidbrush, unroundedRect);
                break;
              }
            case GradientType.LinearHorizontalUpper:
              {
                LinearGradientBrush brushFill;
                brushFill = new LinearGradientBrush(mainRect, mBackgroundColor1, mBackgroundColor2, LinearGradientMode.Vertical);
                brushFill.SetBlendTriangularShape(0.0F);
                brushFill.WrapMode = WrapMode.TileFlipXY;
                GraphicsPath path = GetRoundedRect(mainRect, mRoundingRadius);
                g.FillPath(brushFill, path);
                break;
              }
            case GradientType.LinearHorizontalMiddle:
              {
                LinearGradientBrush brushFill;
                brushFill = new LinearGradientBrush(mainRect, mBackgroundColor1, mBackgroundColor2, LinearGradientMode.Vertical);
                brushFill.SetBlendTriangularShape(0.5F);
                brushFill.WrapMode = WrapMode.TileFlipXY;
                GraphicsPath path = GetRoundedRect(mainRect, mRoundingRadius);
                g.FillPath(brushFill, path);
                break;
              }
            case GradientType.SigmaBellHorizontalUpper:
              {
                LinearGradientBrush brushFill;
                brushFill = new LinearGradientBrush(mainRect, mBackgroundColor1, mBackgroundColor2, LinearGradientMode.Vertical);
                brushFill.SetSigmaBellShape(0.0F);
                brushFill.WrapMode = WrapMode.TileFlipXY;
                GraphicsPath path = GetRoundedRect(mainRect, mRoundingRadius);
                g.FillPath(brushFill, path);
                break;
              }
            case GradientType.SigmaBellHorizontalMiddle:
              {
                LinearGradientBrush brushFill;
                brushFill = new LinearGradientBrush(mainRect, mBackgroundColor1, mBackgroundColor2, LinearGradientMode.Vertical);
                brushFill.SetSigmaBellShape(0.5F);
                brushFill.WrapMode = WrapMode.TileFlipXY;
                GraphicsPath path = GetRoundedRect(mainRect, mRoundingRadius);
                g.FillPath(brushFill, path);
                break;
              }
          }

        }
        if (this.BackgroundImage != null && mDrawBackImage)
        {
          switch (this.BackgroundImageLayout)
          {
            case ImageLayout.Zoom:
              {
                var ratioX = (double)this.Width / this.BackgroundImage.Width;
                var ratioY = (double)this.Height / this.BackgroundImage.Height;
                var ratio = Math.Min(ratioX, ratioY);

                var newWidth = (int)(this.BackgroundImage.Width * ratio);
                var newHeight = (int)(this.BackgroundImage.Height * ratio);

                var newImage = new Bitmap(newWidth, newHeight);
                e.Graphics.DrawImage(this.BackgroundImage, (this.Width - newWidth) / 2.0f, (this.Height - newHeight) / 2.0f, newWidth, newHeight);
                break;
              }
            case ImageLayout.Stretch:
              {
                e.Graphics.DrawImage(this.BackgroundImage, 0.0f, 0.0f, this.Width, this.Height);
                break;
              }
            default:
              {
                e.Graphics.DrawImageUnscaled(this.BackgroundImage, new Point(0, 0));
                break;
              }
          }

          
        }

      // draw the border if it is supposed to be drawn in the bacgroung
      if (BorderWidth > 0 && mBorderColor != Color.Transparent)
      {
        e.Graphics.DrawPath(new Pen(new SolidBrush(mBorderColor), BorderWidth), GetRoundedRect(mainRect, mRoundingRadius));
      }

    }

    /// <summary>
    /// On panel resize
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NicePanel_Resize( object sender, EventArgs e )
    {
      PlaceControls();
    }

    /// <summary>
    /// On control added
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NicePanel_ControlAdded( object sender, ControlEventArgs e )
    {
      PlaceControls();
    }

    /// <summary>
    /// On control removed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NicePanel_ControlRemoved( object sender, ControlEventArgs e )
    {
      PlaceControls();
    }

    /// <summary>
    /// On dock change
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NicePanel_DockChanged( object sender, EventArgs e )
    {
      PlaceControls();
    }

    /// <summary>
    /// Returns rounded rectangle graphics path
    /// </summary>
    /// <param name="baseRect"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    private GraphicsPath GetRoundedRect( RectangleF baseRect, float radius )
    {
      // if corner radius is less than or equal to zero, 
      // return the original rectangle 
      if ( radius <= 0.0F )
      {
        GraphicsPath mPath = new GraphicsPath();
        mPath.AddRectangle( baseRect );
        mPath.CloseFigure();
        return mPath;
      }
      // if the corner radius is greater than or equal to 
      // half the width, or height (whichever is shorter) 
      // then return a capsule instead of a lozenge 
      if ( radius >= ( Math.Min( baseRect.Width, baseRect.Height ) ) / 2.0 )
        return GetCapsule( baseRect );
      // create the arc for the rectangle sides and declare 
      // a graphics path object for the drawing 
      float diameter = radius * 2.0F;
      SizeF sizeF = new SizeF( diameter, diameter );
      RectangleF arc = new RectangleF( baseRect.Location, sizeF );
      GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
      // top left arc 
      path.AddArc( arc, 180, 90 );
      // top right arc 
      arc.X = baseRect.Right - diameter;
      path.AddArc( arc, 270, 90 );
      // bottom right arc 
      arc.Y = baseRect.Bottom - diameter;
      path.AddArc( arc, 0, 90 );
      // bottom left arc
      arc.X = baseRect.Left;
      path.AddArc( arc, 90, 90 );
      path.CloseFigure();
      return path;
    }

    /// <summary>
    /// Returns top rounded rectangle graphics path
    /// </summary>
    /// <param name="baseRect"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    private GraphicsPath GetTopRoundedRect( RectangleF baseRect, float radius )
    {
      // if corner radius is less than or equal to zero, 
      // return the original rectangle 
      if ( radius <= 0.0F )
      {
        GraphicsPath mPath = new GraphicsPath();
        mPath.AddRectangle( baseRect );
        mPath.CloseFigure();
        return mPath;
      }
      // if the corner radius is greater than 
      // the width, or height (whichever is shorter) 
      // then return a capsule instead of a lozenge 
      if ( radius > ( Math.Min( baseRect.Width, baseRect.Height ) ) )
        return GetCapsule( baseRect );
      // create the arc for the rectangle sides and declare 
      // a graphics path object for the drawing 
      float diameter = radius * 2.0F;
      SizeF sizeF = new SizeF( diameter, diameter );
      RectangleF arc = new RectangleF( baseRect.Location, sizeF );
      RectangleF ZeroArc = new RectangleF( baseRect.Location, new SizeF( 0.0001f, 0.0001f ) );
      GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
      // top left arc 
      path.AddArc( arc, 180, 90 );
      // top right arc 
      arc.X = baseRect.Right - diameter;
      path.AddArc( arc, 270, 90 );
      path.CloseFigure();
      return path;
    }

    /// <summary>
    /// Returns bottom rounded rectangle graphics path
    /// </summary>
    /// <param name="baseRect"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    private GraphicsPath GetBottomRoundedRect( RectangleF baseRect, float radius )
    {
      // if corner radius is less than or equal to zero, 
      // return the original rectangle 
      if ( radius <= 0.0F )
      {
        GraphicsPath mPath = new GraphicsPath();
        mPath.AddRectangle( baseRect );
        mPath.CloseFigure();
        return mPath;
      }
      // if the corner radius is greater than 
      // the width, or height (whichever is shorter) 
      // then return a capsule instead of a lozenge 
      if ( radius > ( Math.Min( baseRect.Width, baseRect.Height ) ) )
        return GetCapsule( baseRect );
      // create the arc for the rectangle sides and declare 
      // a graphics path object for the drawing 
      float diameter = radius * 2.0F;
      SizeF sizeF = new SizeF( diameter, diameter );
      RectangleF arc = new RectangleF( baseRect.Location, sizeF );
      RectangleF ZeroArc = new RectangleF( baseRect.Location, new SizeF( 0.0001f, 0.0001f ) );
      GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
      // bottom right arc 
      arc.X = baseRect.Right - diameter;
      arc.Y = baseRect.Bottom - diameter;
      path.AddArc( arc, 0, 90 );
      // bottom left arc
      arc.X = baseRect.Left;
      path.AddArc( arc, 90, 90 );
      path.CloseFigure();
      return path;
    }

    /// <summary>
    /// Returns capsule graphics path within specified rectangle
    /// </summary>
    /// <param name="baseRect"></param>
    /// <returns></returns>
    private GraphicsPath GetCapsule( RectangleF baseRect )
    {
      float diameter;
      RectangleF arc;
      GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
      try
      {
        if ( baseRect.Width > baseRect.Height )
        {
          // return horizontal capsule 
          diameter = baseRect.Height;
          SizeF sizeF = new SizeF( diameter, diameter );
          arc = new RectangleF( baseRect.Location, sizeF );
          path.AddArc( arc, 90, 180 );
          arc.X = baseRect.Right - diameter;
          path.AddArc( arc, 270, 180 );
        }
        else if ( baseRect.Width < baseRect.Height )
        {
          // return vertical capsule 
          diameter = baseRect.Width;
          SizeF sizeF = new SizeF( diameter, diameter );
          arc = new RectangleF( baseRect.Location, sizeF );
          path.AddArc( arc, 180, 180 );
          arc.Y = baseRect.Bottom - diameter;
          path.AddArc( arc, 0, 180 );
        }
        else
        {
          // return circle 
          path.AddEllipse( baseRect );
        }
      }
      catch ( Exception )
      {
        path.AddEllipse( baseRect );
      }
      finally
      {
        path.CloseFigure();
      }
      return path;
    }


    [DllImport( "user32.dll", CharSet = CharSet.Auto )]
    static public extern int GetSystemMetrics( int code );

    [DllImport( "user32.dll" )]
    static public extern bool EnableScrollBar( System.IntPtr hWnd, uint wSBflags, uint wArrows );

    [DllImport( "user32.dll" )]
    static public extern int SetScrollRange( System.IntPtr hWnd, int nBar, int nMinPos, int nMaxPos, bool bRedraw );

    [DllImport( "user32.dll" )]
    static public extern int SetScrollPos( System.IntPtr hWnd, int nBar, int nPos, bool bRedraw );

    [DllImport( "user32.dll" )]
    static public extern int GetScrollPos( System.IntPtr hWnd, int nBar );

    [DllImport( "user32.dll" )]
    static public extern bool ShowScrollBar( System.IntPtr hWnd, int wBar, bool bShow );

    [DllImport( "user32.dll" )]
    static extern IntPtr SendMessage( IntPtr hWnd, uint Msg, UIntPtr wParam, IntPtr lParam );

    [DllImport( "user32.dll" )]
    static extern int HIWORD( System.IntPtr wParam );

    private int getSBFromScrollEventType( ScrollEventType type )
    {
      int res = -1;
      switch ( type )
      {
        case ScrollEventType.SmallDecrement:
          res = (int)SB_Type.SB_LINEUP;
          break;
        case ScrollEventType.SmallIncrement:
          res = (int)SB_Type.SB_LINEDOWN;
          break;
        case ScrollEventType.LargeDecrement:
          res = (int)SB_Type.SB_PAGEUP;
          break;
        case ScrollEventType.LargeIncrement:
          res = (int)SB_Type.SB_PAGEDOWN;
          break;
        case ScrollEventType.ThumbTrack:
          res = (int)SB_Type.SB_THUMBTRACK;
          break;
        case ScrollEventType.First:
          res = (int)SB_Type.SB_TOP;
          break;
        case ScrollEventType.Last:
          res = (int)SB_Type.SB_BOTTOM;
          break;
        case ScrollEventType.ThumbPosition:
          res = (int)SB_Type.SB_THUMBPOSITION;
          break;
        case ScrollEventType.EndScroll:
          res = (int)SB_Type.SB_ENDSCROLL;
          break;
        default:
          break;
      }
      return res;
    }

    private ScrollEventType getScrollEventType( System.IntPtr wParam )
    {
      ScrollEventType res = 0;
      switch ( LoWord( (int)wParam ) )
      {
        case (int)SB_Type.SB_LINEUP:
          res = ScrollEventType.SmallDecrement;
          break;
        case (int)SB_Type.SB_LINEDOWN:
          res = ScrollEventType.SmallIncrement;
          break;
        case (int)SB_Type.SB_PAGEUP:
          res = ScrollEventType.LargeDecrement;
          break;
        case (int)SB_Type.SB_PAGEDOWN:
          res = ScrollEventType.LargeIncrement;
          break;
        case (int)SB_Type.SB_THUMBTRACK:
          res = ScrollEventType.ThumbTrack;
          break;
        case (int)SB_Type.SB_TOP:
          res = ScrollEventType.First;
          break;
        case (int)SB_Type.SB_BOTTOM:
          res = ScrollEventType.Last;
          break;
        case (int)SB_Type.SB_THUMBPOSITION:
          res = ScrollEventType.ThumbPosition;
          break;
        case (int)SB_Type.SB_ENDSCROLL:
          res = ScrollEventType.EndScroll;
          break;
        default:
          res = ScrollEventType.EndScroll;
          break;
      }
      return res;
    }

    protected override void WndProc( ref Message msg )
    {
      base.WndProc( ref msg );
      if ( msg.HWnd != this.Handle )
        return;
      switch ( msg.Msg )
      {
        case (int)WM_Type.WM_MOUSEWHEEL:
          if ( !this.VisibleAutoScrollVertical )
            return;
          try
          {
            int zDelta = HiWord( (int)msg.WParam );
            int y = HiWord( (int)msg.LParam );
            int x = LoWord( (int)msg.LParam );
            System.Windows.Forms.MouseButtons butt;
            switch ( LoWord( (int)msg.WParam ) )
            {
              case (int)MK_Type.MK_LBUTTON:
                butt = System.Windows.Forms.MouseButtons.Left;
                break;
              case (int)MK_Type.MK_MBUTTON:
                butt = System.Windows.Forms.MouseButtons.Middle;
                break;
              case (int)MK_Type.MK_RBUTTON:
                butt = System.Windows.Forms.MouseButtons.Right;
                break;
              case (int)MK_Type.MK_XBUTTON1:
                butt = System.Windows.Forms.MouseButtons.XButton1;
                break;
              case (int)MK_Type.MK_XBUTTON2:
                butt = System.Windows.Forms.MouseButtons.XButton2;
                break;
              default:
                butt = System.Windows.Forms.MouseButtons.None;
                break;
            }
            System.Windows.Forms.MouseEventArgs arg0 = new System.Windows.Forms.MouseEventArgs( butt, 1, x, y, zDelta );
            this.ScrollMouseWheel( this, arg0 );
          }
          catch ( Exception ) { }

          break;

        case (int)WM_Type.WM_VSCROLL:

          try
          {
            ScrollEventType type = getScrollEventType( msg.WParam );
            ScrollEventArgs arg = new ScrollEventArgs( type, GetScrollPos( this.Handle, (int)SB_VERT ) );
            this.ScrollVertical( this, arg );
          }
          catch ( Exception ) { }

          break;

        case (int)WM_Type.WM_HSCROLL:

          try
          {
            ScrollEventType type = getScrollEventType( msg.WParam );
            ScrollEventArgs arg = new ScrollEventArgs( type, GetScrollPos( this.Handle, (int)SB_HORZ ) );
            this.ScrollHorizontal( this, arg );
          }
          catch ( Exception ) { }

          break;

        default:
          break;
      }
    }

    public void performScrollHorizontal( ScrollEventType type )
    {
      int param = getSBFromScrollEventType( type );
      if ( param == -1 )
        return;
      SendMessage( this.Handle, (uint)(int)WM_Type.WM_HSCROLL, (System.UIntPtr)param, (System.IntPtr)0 );
    }

    public void performScrollVertical( ScrollEventType type )
    {
      int param = getSBFromScrollEventType( type );
      if ( param == -1 )
        return;
      SendMessage( this.Handle, (uint)(int)WM_Type.WM_VSCROLL, (System.UIntPtr)param, (System.IntPtr)0 );
    }

    private void ScrollablePanel_Click( object sender, EventArgs e )
    {
      this.Focus();
    }

    static int MakeLong( int LoWord, int HiWord )
    {
      return ( HiWord << 16 ) | ( LoWord & 0xffff );
    }

    static IntPtr MakeLParam( int LoWord, int HiWord )
    {
      return (IntPtr)( ( HiWord << 16 ) | ( LoWord & 0xffff ) );
    }

    static int HiWord( int number )
    {
      if ( ( number & 0x80000000 ) == 0x80000000 )
        return ( number >> 16 );
      else
        return ( number >> 16 ) & 0xffff;
    }

    static int LoWord( int number )
    {
      return number & 0xffff;
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

    #endregion Methods

  }
}
