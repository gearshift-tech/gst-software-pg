
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Soko.Common.Controls.NiceTooltip
{
  /// <summary>
  /// Custom tooltip.
  /// </summary>
  public class NiceToolTip : System.ComponentModel.Component, IExtenderProvider
  {
    #region "Public Events"
    /// <summary>
    /// Event before tooltip is displayed.  This event is raised when OwnerDraw property is set to true.
    /// </summary>
    public event PopupEventHandler Popup;
    public delegate void PopupEventHandler(object sender, PopupEventArgs e);
    /// <summary>
    /// Event when the tooltip surface is drawn.  This event is raised when OwnerDraw property is set to true.
    /// </summary>
    public event DrawEventHandler Draw;
    public delegate void DrawEventHandler(object sender, DrawEventArgs e);
    /// <summary>
    /// Event when the tooltip background is drawn.  This event is raised when OwnerDrawBackground property is set to true.
    /// </summary>
    public event DrawBackgroundEventHandler DrawBackground;
    public delegate void DrawBackgroundEventHandler(object sender, DrawEventArgs e);
    #endregion
    #region "Declarations"
    // Owner of this tooltip
    private System.Windows.Forms.Control _parent = null;
    private System.Windows.Forms.Control _control = null;
    // In milliseconds, interval to fade - in / out
    private int _animationSpeed = 20;
    private bool _showShadow = true;
    // Form to display the tooltip
    private TooltipForm _form;
    // In milliseconds, tooltip will automatically closed if this period passed
    private int _autoClose = 3000;
    private bool _enableAutoClose = true;
    private bool _ownerDraw = false;
    private bool _ownerDrawBackground = false;
    private ToolTipLocation _location = ToolTipLocation.Auto;
    private Point _customLocation = new Point(0, 0);
    // Support for IExtenderProvider
    private Hashtable _texts;
    private Hashtable _titles;
    #endregion
    private Hashtable _images;
    #region "Public Methods"
    /// <summary>
    /// Constructor of the tooltip with an owner control specified.
    /// </summary>
    public NiceToolTip(System.Windows.Forms.Control parent)
    {
      _parent = parent;
      _texts = new Hashtable();
      _titles = new Hashtable();
      _images = new Hashtable();
      _ownerDraw = true;
    }
    /// <summary>
    /// ToolTip constructor.
    /// </summary>
    public NiceToolTip()
    {
      _texts = new Hashtable();
      _titles = new Hashtable();
      _images = new Hashtable();
      _ownerDraw = false;
    }
    /// <summary>
    /// Show ToolTip with specified control.
    /// </summary>
    public void show(System.Windows.Forms.Control control)
    {
      TooltipForm._showShadow = _showShadow;
      _control = control;
      if ((_form != null))
        _form.invokeClose();
      Size tooltipSize = default(Size);
      if (_ownerDraw | _ownerDrawBackground)
      {
        PopupEventArgs e = null;
        e = new PopupEventArgs();
        Popup?.Invoke(this, e);
        tooltipSize = e.Size;
      }
      else
      {
        string tTitle = GetToolTipTitle(_control);
        string tText = GetToolTip(_control);
        Image tImage = GetToolTipImage(_control);
        tooltipSize = NiceToolTip.measureSize(tTitle, tText, tImage);
      }
      _form = new TooltipForm(this, tooltipSize);
    }
    /// <summary>
    /// Show ToolTip with specified control and location.  The ToolTip location is relative to the control.
    /// </summary>
    public void show(System.Windows.Forms.Control control, Point location)
    {
      TooltipForm._showShadow = _showShadow;
      _control = control;
      if ((_form != null))
        _form.invokeClose();
      Size tooltipSize = default(Size);
      if (_ownerDraw | _ownerDrawBackground)
      {
        PopupEventArgs e = null;
        e = new PopupEventArgs();
        Popup?.Invoke(this, e);
        tooltipSize = e.Size;
      }
      else
      {
        string tTitle = GetToolTipTitle(_control);
        string tText = GetToolTip(_control);
        Image tImage = GetToolTipImage(_control);
        tooltipSize = NiceToolTip.measureSize(tTitle, tText, tImage);
      }
      _form = new TooltipForm(this, tooltipSize, location);
    }
    /// <summary>
    /// Show ToolTip with specified control and rectangle area.  This area is where the tooltip must avoid to cover.
    /// </summary>
    public void show(System.Windows.Forms.Control control, Rectangle rect)
    {
      TooltipForm._showShadow = _showShadow;
      _control = control;
      if ((_form != null))
        _form.invokeClose();
      Size tooltipSize = default(Size);
      if (_ownerDraw | _ownerDrawBackground)
      {
        PopupEventArgs e = null;
        e = new PopupEventArgs();
        Popup?.Invoke(this, e);
        tooltipSize = e.Size;
      }
      else
      {
        string tTitle = GetToolTipTitle(_control);
        string tText = GetToolTip(_control);
        Image tImage = GetToolTipImage(_control);
        tooltipSize = NiceToolTip.measureSize(tTitle, tText, tImage);
      }
      _form = new TooltipForm(this, tooltipSize, rect);
    }
    /// <summary>
    /// Hide the ToolTip.
    /// </summary>
    public void hide()
    {
      try
      {
        if (_form != null)
            _form.DoClose();
      }
      catch (Exception)
      {
      }
    }
    // Extended property for ToolTip property.
    //[EditorAttribute(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor)), DefaultValue("")]
    public string GetToolTip(object obj)
    {
      string tText = Convert.ToString(_texts[obj]);
      if (tText == null)
      {
        tText = string.Empty;
      }
      return tText;
    }
    //[EditorAttribute(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public void SetToolTip(object obj, string value)
    {
      if (value == null)
      {
        value = string.Empty;
      }
      if (value.Length == 0)
      {
        _texts.Remove(obj);
      }
      else
      {
        _texts[obj] = value;
      }
      if (hasToolTip((System.Windows.Forms.Control)obj))
      {
        if ((obj) is System.Windows.Forms.Control)
        {
          System.Windows.Forms.Control ctrl = (System.Windows.Forms.Control)obj;
          ctrl.MouseEnter += ctrlMouseEnter;
          ctrl.MouseLeave += ctrlMouseLeave;
          ctrl.MouseDown += ctrlMouseDown;
        }
        else if ((obj) is ToolStripItem)
        {
          ToolStripItem anItem = (ToolStripItem)obj;
          anItem.MouseEnter += tsiMouseEnter;
          anItem.MouseLeave += ctrlMouseLeave;
          anItem.MouseDown += ctrlMouseDown;
        }
      }
      else
      {
        if ((obj) is System.Windows.Forms.Control)
        {
          System.Windows.Forms.Control ctrl = (System.Windows.Forms.Control)obj;
          ctrl.MouseEnter -= ctrlMouseEnter;
          ctrl.MouseLeave -= ctrlMouseLeave;
          ctrl.MouseDown -= ctrlMouseDown;
        }
        else if ((obj) is ToolStripItem)
        {
          ToolStripItem anItem = (ToolStripItem)obj;
          anItem.MouseEnter -= tsiMouseEnter;
          anItem.MouseLeave -= ctrlMouseLeave;
          anItem.MouseDown -= ctrlMouseDown;
        }
      }
    }
    [DefaultValue("")]
    public string GetToolTipTitle(System.Windows.Forms.Control ctrl)
    {
      string tTitle = Convert.ToString(_titles[ctrl]);
      if (tTitle == null)
      {
        tTitle = string.Empty;
      }
      return tTitle;
    }
    public void SetToolTipTitle(object obj, string value)
    {
      if (value == null)
      {
        value = string.Empty;
      }
      if (value.Length == 0)
      {
        _titles.Remove(obj);
      }
      else
      {
        _titles[obj] = value;
      }
      if (hasToolTip((System.Windows.Forms.Control)obj))
      {
        if ((obj) is System.Windows.Forms.Control)
        {
          System.Windows.Forms.Control ctrl = (System.Windows.Forms.Control)obj;
          ctrl.MouseEnter += ctrlMouseEnter;
          ctrl.MouseLeave += ctrlMouseLeave;
          ctrl.MouseDown += ctrlMouseDown;
        }
        else if ((obj) is ToolStripItem)
        {
          ToolStripItem anItem = (ToolStripItem)obj;
          anItem.MouseEnter += tsiMouseEnter;
          anItem.MouseLeave += ctrlMouseLeave;
          anItem.MouseDown += ctrlMouseDown;
        }
      }
      else
      {
        if ((obj) is System.Windows.Forms.Control)
        {
          System.Windows.Forms.Control ctrl = (System.Windows.Forms.Control)obj;
          ctrl.MouseEnter -= ctrlMouseEnter;
          ctrl.MouseLeave -= ctrlMouseLeave;
          ctrl.MouseDown -= ctrlMouseDown;
        }
        else if ((obj) is ToolStripItem)
        {
          ToolStripItem anItem = (ToolStripItem)obj;
          anItem.MouseEnter -= tsiMouseEnter;
          anItem.MouseLeave -= ctrlMouseLeave;
          anItem.MouseDown -= ctrlMouseDown;
        }
      }
    }
    //[EditorAttribute(typeof(System.Drawing.Design.ImageEditor), typeof(System.Drawing.Design.UITypeEditor)), DefaultValue(typeof(Image), "Nothing")]
    public Image GetToolTipImage(System.Windows.Forms.Control ctrl)
    {
      return (Image)_images[ctrl];
    }
    //[EditorAttribute(typeof(System.Drawing.Design.ImageEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public void SetToolTipImage(object obj, Image value)
    {
      if (value == null)
      {
        _images.Remove(obj);
      }
      else
      {
        _images[obj] = value;
      }
      if (hasToolTip((System.Windows.Forms.Control)obj))
      {
        if ((obj) is System.Windows.Forms.Control)
        {
          System.Windows.Forms.Control ctrl = (System.Windows.Forms.Control)obj;
          ctrl.MouseEnter += ctrlMouseEnter;
          ctrl.MouseLeave += ctrlMouseLeave;
          ctrl.MouseDown += ctrlMouseDown;
        }
        else if ((obj) is ToolStripItem)
        {
          ToolStripItem anItem = (ToolStripItem)obj;
          anItem.MouseEnter += tsiMouseEnter;
          anItem.MouseLeave += ctrlMouseLeave;
          anItem.MouseDown += ctrlMouseDown;
        }
      }
      else
      {
        if ((obj) is System.Windows.Forms.Control)
        {
          System.Windows.Forms.Control ctrl = (System.Windows.Forms.Control)obj;
          ctrl.MouseEnter -= ctrlMouseEnter;
          ctrl.MouseLeave -= ctrlMouseLeave;
          ctrl.MouseDown -= ctrlMouseDown;
        }
        else if ((obj) is ToolStripItem)
        {
          ToolStripItem anItem = (ToolStripItem)obj;
          anItem.MouseEnter -= tsiMouseEnter;
          anItem.MouseLeave -= ctrlMouseLeave;
          anItem.MouseDown -= ctrlMouseDown;
        }
      }
    }
    public bool CanExtend(object extendee)
    {
      if ((extendee) is System.Windows.Forms.Control)
      {
        if ((extendee) is System.Windows.Forms.Form)
        {
          return false;
        }
        else
        {
          return true;
        }
      }
      if ((extendee) is ToolStripItem)
        return true;
      return false;
    }
    // Disposing components
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        // Clear all resources
        _texts.Clear();
        _titles.Clear();
        _images.Clear();
        if (_form != null)
        {
          _form.DoClose();
        }
      }
      base.Dispose(disposing);
    }
    #endregion
    // This class, API calls, I got this from an article in CodeProject about layered window (but I forgot)
    #region "Private Class"
    #region "Windows API"
    private struct BLENDFUNCTION
    {
      public byte BlendOp;
      public byte BlendFlags;
      public byte SourceConstantAlpha;
      public byte AlphaFormat;
    }
    [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    private static extern bool UpdateLayeredWindow(IntPtr hWnd, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pptSrc, int crKey, ref BLENDFUNCTION pBlend, int dwFlags);
    [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    private static extern IntPtr GetDC(IntPtr hWnd);
    [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
    [DllImport("gdi32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    private static extern IntPtr CreateCompatibleDC(IntPtr hDC);
    [DllImport("gdi32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    private static extern bool DeleteDC(IntPtr hDC);
    [DllImport("gdi32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    private static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
    [DllImport("gdi32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    private static extern bool DeleteObject(IntPtr hObject);
    [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    private static extern int ShowWindow(IntPtr hWnd, int nCmdShow);
    [DllImport("user32.dll", EntryPoint = "SendMessageA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
    [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    private static extern void ReleaseCapture();
    private const int WS_EX_LAYERED = 0x80000;
    private const int ULW_ALPHA = 0x2;
    private const byte AC_SRC_OVER = 0x0;
    private const byte AC_SRC_ALPHA = 0x1;
    #endregion
    private const long WS_EX_TRANSPARENT = 0x20L;
    private class TooltipForm : System.Windows.Forms.Form
    {
      public static bool _showShadow;
      private bool _closing = false;
      const int BORDER_MARGIN = 1;
      //Rectangle _rect;
      GraphicsPath _path;
      Bitmap bgBitmap;
      Bitmap tBitmap;
      private Timer _timer;
      private Timer _tmrClose;
      private Point mNormalPos;
      //private Rectangle mCurrentBounds;
      private NiceToolTip mPopup;
      private DateTime mTimerStarted;
      //private double mProgress;
      private const int CS_DROPSHADOW = 0x20000;
      private const int SW_NOACTIVATE = 4;
      private const long WS_EX_TOOLWINDOW = 0x80L;
      private const int SWP_NOSIZE = 0x1;
      private const int SWP_NOMOVE = 0x2;
      private const int SWP_NOACTIVATE = 0x10;
      private const long WS_POPUP = 0x80000000;
      private IntPtr HWND_TOPMOST = new IntPtr(-1);
      //int mx;
      //int _my;
      int _alpha = 100;
      //private static Image mBackgroundImage;
      [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
      private static extern int SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, int flags);
      public TooltipForm(NiceToolTip popup, System.Drawing.Size size)
      {
        System.Windows.Forms.Padding aPadding = default(System.Windows.Forms.Padding);
        mPopup = popup;
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        StartPosition = FormStartPosition.Manual;
        this.ShowInTaskbar = false;
        this.DockPadding.All = BORDER_MARGIN;
        aPadding.All = 3;
        if (mPopup._parent != null)
        {
          Form parentForm = mPopup._parent.FindForm();
          if ((parentForm != null))
          {
            parentForm.AddOwnedForm(this);
          }
        }
        else
        {
          if (mPopup._control != null)
          {
            Form parentForm = mPopup._control.FindForm();
            if ((parentForm != null))
            {
              parentForm.AddOwnedForm(this);
            }
          }
        }
        this.Padding = aPadding;
        if (mPopup._showShadow)
        {
          size.Width = size.Width + 10;
          size.Height = size.Height + 10;
        }
        else
        {
          size.Width = size.Width + 6;
          size.Height = size.Height + 6;
        }
        this.MaximumSize = size;
        this.MinimumSize = size;
        bgBitmap = new Bitmap(size.Width, size.Height);
        tBitmap = new Bitmap(size.Width, size.Height);
        ReLocate();
        // Initialize the animation
        Rectangle aRect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
        _path = Drawing.roundedRectangle(aRect, 2, 2, 2, 2);
        this.Location = mNormalPos;
        _timer = new Timer();
        _tmrClose = new Timer
        {
          Interval = mPopup._autoClose
        };
        _tmrClose.Tick += AutoClosing;
        drawBitmap();
        if (mPopup._animationSpeed > 0)
        {
          _alpha = 0;
          // I always aim 25 images per seconds.. seems to be a good value
          // it looks smooth enough on fast computers and do not drain slower one
          _timer.Interval = mPopup._animationSpeed;
          mTimerStarted = System.DateTime.Now;
          _timer.Tick += Showing;
          _timer.Start();
          Showing(null, null);
        }
        else
        {
          setBitmap(bgBitmap);
        }
        //If mPopup.mDialog Then
        //    ShowDialog()
        //Else
        //    Show()
        //End If
        NiceToolTip.ShowWindow(this.Handle, SW_NOACTIVATE);
        SetWindowPos(this.Handle, HWND_TOPMOST, this.Left, this.Top, this.Width, this.Height, SWP_NOSIZE | SWP_NOMOVE | SWP_NOACTIVATE);
        if (mPopup._enableAutoClose)
          _tmrClose.Start();
      }
      public TooltipForm(NiceToolTip popup, System.Drawing.Size size, Point location)
      {
        System.Windows.Forms.Padding aPadding = default(System.Windows.Forms.Padding);
        mPopup = popup;
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        StartPosition = FormStartPosition.Manual;
        this.ShowInTaskbar = false;
        this.DockPadding.All = BORDER_MARGIN;
        aPadding.All = 3;
        if (mPopup._parent != null)
        {
          Form parentForm = mPopup._parent.FindForm();
          if ((parentForm != null))
          {
            parentForm.AddOwnedForm(this);
          }
        }
        else
        {
          if (mPopup._control != null)
          {
            Form parentForm = mPopup._control.FindForm();
            if ((parentForm != null))
            {
              parentForm.AddOwnedForm(this);
            }
          }
        }
        this.Padding = aPadding;
        if (mPopup._showShadow)
        {
          size.Width = size.Width + 10;
          size.Height = size.Height + 10;
        }
        else
        {
          size.Width = size.Width + 6;
          size.Height = size.Height + 6;
        }
        this.MaximumSize = size;
        this.MinimumSize = size;
        bgBitmap = new Bitmap(size.Width, size.Height);
        tBitmap = new Bitmap(size.Width, size.Height);
        ReLocate(location);
        // Initialize the animation
        Rectangle aRect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
        _path = Drawing.roundedRectangle(aRect, 2, 2, 2, 2);
        this.Location = mNormalPos;
        _timer = new Timer();
        _tmrClose = new Timer
        {
          Interval = mPopup._autoClose
        };
        _tmrClose.Tick += AutoClosing;
        drawBitmap();
        if (mPopup._animationSpeed > 0)
        {
          _alpha = 0;
          // I always aim 25 images per seconds.. seems to be a good value
          // it looks smooth enough on fast computers and do not drain slower one
          _timer.Interval = mPopup._animationSpeed;
          mTimerStarted = System.DateTime.Now;
          _timer.Tick += Showing;
          _timer.Start();
          Showing(null, null);
        }
        else
        {
          setBitmap(bgBitmap);
        }
        //If mPopup.mDialog Then
        //    ShowDialog()
        //Else
        //    Show()
        //End If
        NiceToolTip.ShowWindow(this.Handle, SW_NOACTIVATE);
        SetWindowPos(this.Handle, HWND_TOPMOST, this.Left, this.Top, this.Width, this.Height, SWP_NOSIZE | SWP_NOMOVE | SWP_NOACTIVATE);
        if (mPopup._enableAutoClose)
          _tmrClose.Start();
      }
      public TooltipForm(NiceToolTip popup, System.Drawing.Size size, Rectangle rect)
      {
        System.Windows.Forms.Padding aPadding = default(System.Windows.Forms.Padding);
        mPopup = popup;
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        StartPosition = FormStartPosition.Manual;
        this.ShowInTaskbar = false;
        this.DockPadding.All = BORDER_MARGIN;
        aPadding.All = 3;
        if (mPopup._parent != null)
        {
          Form parentForm = mPopup._parent.FindForm();
          if ((parentForm != null))
          {
            parentForm.AddOwnedForm(this);
          }
        }
        else
        {
          if (mPopup._control != null)
          {
            Form parentForm = mPopup._control.FindForm();
            if ((parentForm != null))
            {
              parentForm.AddOwnedForm(this);
            }
          }
        }
        this.Padding = aPadding;
        if (mPopup._showShadow)
        {
          size.Width = size.Width + 10;
          size.Height = size.Height + 10;
        }
        else
        {
          size.Width = size.Width + 6;
          size.Height = size.Height + 6;
        }
        this.MaximumSize = size;
        this.MinimumSize = size;
        bgBitmap = new Bitmap(size.Width, size.Height);
        tBitmap = new Bitmap(size.Width, size.Height);
        ReLocate(rect);
        // Initialize the animation
        Rectangle aRect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
        _path = Drawing.roundedRectangle(aRect, 2, 2, 2, 2);
        this.Location = mNormalPos;
        _timer = new Timer();
        _tmrClose = new Timer
        {
          Interval = mPopup._autoClose
        };
        _tmrClose.Tick += AutoClosing;
        drawBitmap();
        if (mPopup._animationSpeed > 0)
        {
          _alpha = 0;
          // I always aim 25 images per seconds.. seems to be a good value
          // it looks smooth enough on fast computers and do not drain slower one
          _timer.Interval = mPopup._animationSpeed;
          mTimerStarted = System.DateTime.Now;
          _timer.Tick += Showing;
          _timer.Start();
          Showing(null, null);
        }
        else
        {
          setBitmap(bgBitmap);
        }
        //If mPopup.mDialog Then
        //    ShowDialog()
        //Else
        //    Show()
        //End If
        NiceToolTip.ShowWindow(this.Handle, SW_NOACTIVATE);
        SetWindowPos(this.Handle, HWND_TOPMOST, this.Left, this.Top, this.Width, this.Height, SWP_NOSIZE | SWP_NOMOVE | SWP_NOACTIVATE);
        if (mPopup._enableAutoClose)
          _tmrClose.Start();
      }
      private void drawTransparentBitmap()
      {
        Graphics g = Graphics.FromImage(tBitmap);
        int y = 0;
        int x = 0;
        Color aColor = default(Color);
        Color tColor = default(Color);
        g.Clear(Color.Transparent);
        g.Dispose();
        y = 0;
        while (y < bgBitmap.Height)
        {
          x = 0;
          while (x < bgBitmap.Width)
          {
            aColor = bgBitmap.GetPixel(x, y);
            tColor = Color.FromArgb(_alpha * aColor.A / 100, aColor.R, aColor.G, aColor.B);
            tBitmap.SetPixel(x, y, tColor);
            x = x + 1;
          }
          y = y + 1;
        }
      }
      private void drawBackground(Graphics g)
      {
        if (!mPopup._ownerDrawBackground)
        {
          if (mPopup._showShadow)
          {
            System.Drawing.Drawing2D.LinearGradientBrush bgBrush = default(System.Drawing.Drawing2D.LinearGradientBrush);
            GraphicsPath aPath = default(GraphicsPath);
            Rectangle aRect = new Rectangle(0, 0, this.Width - 4, this.Height - 4);
            Rectangle rectShadow = new Rectangle(4, 4, this.Width - 4, this.Height - 4);
            GraphicsPath pathShadow = Drawing.roundedRectangle(rectShadow, 4, 4, 4, 4);
            PathGradientBrush shadowBrush = new PathGradientBrush(pathShadow);
            Color[] sColor = new Color[4];
            float[] sPos = new float[4];
            ColorBlend sBlend = new ColorBlend();
            sColor[0] = Color.FromArgb(0, 0, 0, 0);
            sColor[1] = Color.FromArgb(16, 0, 0, 0);
            sColor[2] = Color.FromArgb(32, 0, 0, 0);
            sColor[3] = Color.FromArgb(128, 0, 0, 0);
            if (rectShadow.Width > rectShadow.Height)
            {
              sPos[0] = 0f;
              sPos[1] = 4 / rectShadow.Width;
              sPos[2] = 8 / rectShadow.Width;
              sPos[3] = 1f;
            }
            else
            {
              if (rectShadow.Width < rectShadow.Height)
              {
                sPos[0] = 0f;
                sPos[1] = 4 / rectShadow.Height;
                sPos[2] = 8 / rectShadow.Height;
                sPos[3] = 1f;
              }
              else
              {
                sPos[0] = 0f;
                sPos[1] = 4 / rectShadow.Width;
                sPos[2] = 8 / rectShadow.Width;
                sPos[3] = 1f;
              }
            }
            sBlend.Colors = sColor;
            sBlend.Positions = sPos;
            shadowBrush.InterpolationColors = sBlend;
            if (rectShadow.Width > rectShadow.Height)
            {
              shadowBrush.CenterPoint = new Point(rectShadow.X + (rectShadow.Width / 2), rectShadow.Bottom - (rectShadow.Width / 2));
            }
            else
            {
              if (rectShadow.Width == rectShadow.Height)
              {
                shadowBrush.CenterPoint = new Point(rectShadow.X + (rectShadow.Width / 2), rectShadow.Y + (rectShadow.Height / 2));
              }
              else
              {
                shadowBrush.CenterPoint = new Point(rectShadow.Right - (rectShadow.Height / 2), rectShadow.Y + (rectShadow.Height / 2));
              }
            }
            aPath = Drawing.roundedRectangle(aRect, 2, 2, 2, 2);
            bgBrush = new System.Drawing.Drawing2D.LinearGradientBrush(aRect, Color.FromArgb(255, 255, 255), Color.FromArgb(201, 217, 239), System.Drawing.Drawing2D.LinearGradientMode.Vertical);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.Transparent);
            g.FillPath(shadowBrush, pathShadow);
            g.FillPath(bgBrush, aPath);
            g.DrawPath(new Pen(Color.FromArgb(118, 118, 118)), aPath);
            bgBrush.Dispose();
            aPath.Dispose();
            pathShadow.Dispose();
            shadowBrush.Dispose();
          }
          else
          {
            System.Drawing.Drawing2D.LinearGradientBrush bgBrush = default(System.Drawing.Drawing2D.LinearGradientBrush);
            GraphicsPath aPath = default(GraphicsPath);
            Rectangle aRect = new Rectangle(0, 0, this.Width, this.Height);
            aPath = Drawing.roundedRectangle(aRect, 2, 2, 2, 2);
            bgBrush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, this.Width, this.Height), Color.FromArgb(255, 255, 255), Color.FromArgb(201, 217, 239), System.Drawing.Drawing2D.LinearGradientMode.Vertical);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.Transparent);
            g.FillPath(bgBrush, aPath);
            g.DrawPath(new Pen(Color.FromArgb(118, 118, 118)), aPath);
            bgBrush.Dispose();
            aPath.Dispose();
          }
        }
        else
        {
          g.Clear(Color.Transparent);
          mPopup.invokeDrawBackground(g, new Rectangle(0, 0, this.Width - 1, this.Height - 1));
        }
      }
      private void drawBitmap()
      {
        Graphics g = Graphics.FromImage(bgBitmap);
        Rectangle rect = default(Rectangle);
        drawBackground(g);
        if (!mPopup.OwnerDrawBackground)
        {
          if (mPopup._showShadow)
          {
            rect.X = 3;
            rect.Y = 3;
            rect.Width = this.Width - 10;
            rect.Height = this.Height - 10;
          }
          else
          {
            rect.X = 3;
            rect.Y = 3;
            rect.Width = this.Width - 6;
            rect.Height = this.Height - 6;
          }
        }
        else
        {
          rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
        }
        mPopup.invokeDraw(g, rect);
        g.Dispose();
      }
      private void setBitmap(Bitmap aBmp)
      {
        IntPtr screenDC = NiceToolTip.GetDC(IntPtr.Zero);
        IntPtr memDC = NiceToolTip.CreateCompatibleDC(screenDC);
        IntPtr hBitmap = IntPtr.Zero;
        IntPtr oldBitmap = IntPtr.Zero;
        try
        {
          hBitmap = aBmp.GetHbitmap(Color.FromArgb(0));
          oldBitmap = NiceToolTip.SelectObject(memDC, hBitmap);

          Size size = new Size(aBmp.Width, aBmp.Height);
          Point pointSource = new Point(0, 0);
          Point topPos = new Point(this.Left, this.Top);
          BLENDFUNCTION blend = new BLENDFUNCTION
          {
            BlendOp = NiceToolTip.AC_SRC_OVER,
            BlendFlags = 0,
            SourceConstantAlpha = 255,
            AlphaFormat = NiceToolTip.AC_SRC_ALPHA
          };
          NiceToolTip.UpdateLayeredWindow(this.Handle, screenDC, ref topPos, ref size, memDC, ref pointSource, 0, ref blend, NiceToolTip.ULW_ALPHA);
        }
        catch (Exception)
        {
        }
        finally
        {
          NiceToolTip.ReleaseDC(IntPtr.Zero, screenDC);
          if (hBitmap != IntPtr.Zero)
          {
            NiceToolTip.SelectObject(memDC, oldBitmap);
            NiceToolTip.DeleteObject(hBitmap);
          }
          NiceToolTip.DeleteDC(memDC);
        }
      }
      protected override System.Windows.Forms.CreateParams CreateParams
      {
        get
        {
          CreateParams cp = base.CreateParams;
          cp.ExStyle = cp.ExStyle | NiceToolTip.WS_EX_LAYERED;
          return cp;
        }
      }
      protected override void Dispose(bool disposing)
      {
        if (disposing)
        {
          if (_tmrClose != null)
          {
            _tmrClose.Dispose();
          }
          if (_timer != null)
          {
            _timer.Dispose();
          }
          if (bgBitmap != null)
          {
            bgBitmap.Dispose();
          }
          if (tBitmap != null)
          {
            tBitmap.Dispose();
          }
        }
        base.Dispose(disposing);
      }
      private void ReLocate()
      {
        int rW = 0;
        int rH = 0;
        Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
        Cursor mCursor = mPopup._control.Cursor;
        rW = this.Width;
        rH = this.Height;
        mNormalPos = System.Windows.Forms.Control.MousePosition;
        mNormalPos.X = mNormalPos.X + mCursor.Size.Width;
        mNormalPos.Y = mNormalPos.Y + mCursor.Size.Height;
        if (mNormalPos.X + rW > workingArea.Width)
        {
          mNormalPos.X = mNormalPos.X - rW;
        }
        if (mNormalPos.Y + rH > workingArea.Height)
        {
          mNormalPos.Y = mNormalPos.Y - (rH + mCursor.Size.Height);
        }
      }
      private void ReLocate(Point location)
      {
        int rW = 0;
        int rH = 0;
        Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
        rW = this.Width;
        rH = this.Height;
        mNormalPos = mPopup._control.PointToScreen(location);
        if (mNormalPos.X + rW > workingArea.Width)
        {
          mNormalPos.X = mNormalPos.X - rW;
        }
        if (mNormalPos.Y + rH > workingArea.Height)
        {
          mNormalPos.Y = mNormalPos.Y - rH;
        }
      }
      private void ReLocate(Rectangle rect)
      {
        int rW = 0;
        int rH = 0;
        Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
        Point askedLoc = default(Point);
        askedLoc.X = rect.X;
        askedLoc.Y = rect.Bottom + 5;
        rW = this.Width;
        rH = this.Height;
        mNormalPos = mPopup._control.PointToScreen(askedLoc);
        if (mNormalPos.X + rW > workingArea.Width)
        {
          mNormalPos.X = mNormalPos.X - (rW - rect.Width);
        }
        if (mNormalPos.Y + rH > workingArea.Height)
        {
          mNormalPos.Y = mNormalPos.Y - (rH + rect.Height + 10);
        }
      }
      private void Showing(object sender, EventArgs e)
      {
        if (!_closing)
        {
          if (_alpha == 100)
          {
            _timer.Stop();
          }
          else
          {
            try
            {
              _alpha = _alpha + 10;
              drawTransparentBitmap();
              setBitmap(tBitmap);
            }
            catch (Exception)
            {
              _timer.Stop();
            }
          }
        }
        else
        {
          if (_alpha == 0)
          {
            _timer.Stop();
            _timer.Tick -= Showing;
            invokeClose();
          }
          else
          {
            try
            {
              _alpha = _alpha - 10;
              drawTransparentBitmap();
              setBitmap(tBitmap);
            }
            catch (Exception)
            {
              _timer.Stop();
            }
          }
        }
      }
      internal void DoClose()
      {
        if (mPopup._animationSpeed > 0)
        {
          _closing = true;
          _timer.Start();
        }
        else
        {
          invokeClose();
        }
      }
      internal void invokeClose()
      {
        var _with1 = mPopup;
        try
        {
        }
        finally
        {
          _with1._form.Close();
          _with1._form = null;
          if (mPopup._parent != null)
          {
            Form parentForm = mPopup._parent.FindForm();
            if ((parentForm != null))
            {
              parentForm.RemoveOwnedForm(this);
            }
          }
          else
          {
            if (mPopup._control != null)
            {
              Form parentForm = mPopup._control.FindForm();
              if ((parentForm != null))
              {
                parentForm.RemoveOwnedForm(this);
              }
            }
          }
          //parentForm.Focus()
          Close();
        }
      }
      private void AutoClosing(object sender, EventArgs e)
      {
        DoClose();
      }
    }
    #endregion
    #region "Private Methods"
    private void invokeDraw(Graphics g, Rectangle rect)
    {
      if (_ownerDraw | _ownerDrawBackground)
      {
        DrawEventArgs e = null;
        e = new DrawEventArgs(g, rect);
        Draw?.Invoke(this, e);
      }
      else
      {
        string tTitle = GetToolTipTitle(_control);
        string tText = GetToolTip(_control);
        Image tImage = GetToolTipImage(_control);
        NiceToolTip.drawToolTip(tTitle, tText, tImage, g, rect);
      }
    }
    private void invokeDrawBackground(Graphics g, Rectangle rect)
    {
      DrawEventArgs e = new DrawEventArgs(g, rect);
      DrawBackground?.Invoke(this, e);
    }
    private bool hasToolTip(System.Windows.Forms.Control ctrl)
    {
      string tText = GetToolTip(ctrl);
      string tTitle = GetToolTipTitle(ctrl);
      Image tImage = GetToolTipImage(ctrl);
      return NiceToolTip.containsToolTip(tTitle, tText, tImage);
    }
    // Control's MouseEnter and MouseLeave event handler
    private void ctrlMouseEnter(object sender, EventArgs e)
    {
      _control = (System.Windows.Forms.Control)sender;
      switch (_location)
      {
        case ToolTipLocation.Auto:
          Rectangle ctrlRect = new Rectangle(0, 0, _control.Bounds.Width, _control.Bounds.Height);
          show(_control, ctrlRect);
          break;
        case ToolTipLocation.MousePointer:
          show(_control);
          break;
        case ToolTipLocation.CustomClient:
          show(_control, _customLocation);
          break;
        case ToolTipLocation.CustomScreen:
          Point clientLocation = _control.PointToClient(_customLocation);
          show(_control, clientLocation);
          break;
      }
    }
    private void ctrlMouseLeave(object sender, EventArgs e)
    {
      if (object.ReferenceEquals(sender, _control))
      {
        _control = null;
        hide();
      }
    }
    private void ctrlMouseDown(object sender, MouseEventArgs e)
    {
      hide();
    }
    // ToolStripItem's MouseEnter and MouseLeave event handler
    private void tsiMouseEnter(object sender, EventArgs e)
    {
      ToolStripItem anItem = (ToolStripItem)sender;
      _control = anItem.GetCurrentParent();
      switch (_location)
      {
        case ToolTipLocation.Auto:
          Rectangle itemRect = new Rectangle(anItem.Bounds.X, 0, anItem.Bounds.Width, _control.Height - 2);
          show(_control, itemRect);
          break;
        case ToolTipLocation.MousePointer:
          show(_control);
          break;
        case ToolTipLocation.CustomClient:
          show(_control, _customLocation);
          break;
        case ToolTipLocation.CustomScreen:
          Point clientLocation = _control.PointToClient(_customLocation);
          show(_control, clientLocation);
          break;
      }
    }
    #endregion
    #region "Public Properties"
    /// <summary>
    /// Specifies fade effect period when the tooltip is displayed or hiden, in milliseconds.
    /// </summary>
    [DefaultValue(20)]
    public int AnimationSpeed
    {
      get { return _animationSpeed; }
      set { _animationSpeed = value; }
    }
    /// <summary>
    /// Show the shadow effect of the tooltip.  This property is ignored when OwnerDrawBackground property is set to true.
    /// </summary>
    [DefaultValue(true)]
    public bool ShowShadow
    {
      get { return _showShadow; }
      set { _showShadow = value; }
    }
    /// <summary>
    /// Period of time the ToolTip is displayed, in milliseconds.
    /// </summary>
    [DefaultValue(3000)]
    public int AutoClose
    {
      get { return _autoClose; }
      set { _autoClose = value; }
    }
    /// <summary>
    /// Automatically close the ToolTip when the specified time in AutoClose property has been passed.
    /// </summary>
    [DefaultValue(true)]
    public bool EnableAutoClose
    {
      get { return _enableAutoClose; }
      set { _enableAutoClose = value; }
    }
    /// <summary>
    /// ToolTip surface will be manually drawn by your code.
    /// </summary>
    [DefaultValue(false)]
    public bool OwnerDraw
    {
      get { return _ownerDraw; }
      set { _ownerDraw = value; }
    }
    /// <summary>
    /// ToolTip background will be manually drawn by your code.
    /// If this property is set to true, the Draw and Popup event will be raised as well, 
    /// and the whole ToolTip will be drawn by your code.
    /// </summary>
    [DefaultValue(false)]
    public bool OwnerDrawBackground
    {
      get { return _ownerDrawBackground; }
      set { _ownerDrawBackground = value; }
    }
    /// <summary>
    /// Determine how the ToolTip will be located.
    /// </summary>
    [DefaultValue(typeof(ToolTipLocation), "Auto")]
    public ToolTipLocation Location
    {
      get { return _location; }
      set { _location = value; }
    }
    /// <summary>
    /// Custom location where the ToolTip will be displayed.
    /// Used when the Location property is set CustomScreen or CustomClient.
    /// </summary>
    [DefaultValue(typeof(Point), "0,0")]
    public Point CustomLocation
    {
      get { return _customLocation; }
      set { _customLocation = value; }
    }
    #endregion

    public static Font TitleFont = new Font("Segoe UI", 8, FontStyle.Bold);
    #region "Enumerations"
    public static Font TextFont = new Font("Segoe UI", 8, FontStyle.Regular);
    /// <summary>
    /// Describing the content of a tooltip information.
    /// Tooltip information has 3 component, title, text, and image.
    /// </summary>
    public enum Content
    {
      TitleOnly,
      TitleAndText,
      TitleAndImage,
      All,
      ImageOnly,
      ImageAndText,
      TextOnly,
      Empty
    }
    #endregion
    /// <summary>
    /// A brush for drawing a string in tooltip.
    /// </summary>
    /// <returns>Brush.</returns>
    public static Brush TextBrush
    {
      get { return new SolidBrush(Color.FromArgb(118, 118, 118)); }
    }
    /// <summary>
    /// A pen for drawing line separator in tooltip.
    /// </summary>
    /// <returns>Pen.</returns>
    public static Pen SeparatorPen
    {
      get { return new Pen(Color.FromArgb(158, 187, 221)); }
    }
    /// <summary>
    /// Get the content of the tooltip information.
    /// </summary>
    /// <param name="title">Tooltip title.</param>
    /// <param name="text">Tooltip text.</param>
    /// <param name="image">Tooltip image.</param>
    /// <returns><seealso cref="Content"/></returns>
    public static Content getContent(string title, string text, Image image)
    {
      if (!string.IsNullOrEmpty(title) & !string.IsNullOrEmpty(text) & image != null)
      {
        return Content.All;
      }
      else
      {
        if (!string.IsNullOrEmpty(title))
        {
          if (image != null)
          {
            return Content.TitleAndImage;
          }
          else
          {
            if (!string.IsNullOrEmpty(text))
            {
              return Content.TitleAndText;
            }
            else
            {
              return Content.TitleOnly;
            }
          }
        }
        else
        {
          if (image != null)
          {
            if (!string.IsNullOrEmpty(text))
            {
              return Content.ImageAndText;
            }
            else
            {
              return Content.ImageOnly;
            }
          }
          else
          {
            if (!string.IsNullOrEmpty(text))
            {
              return Content.TextOnly;
            }
          }
        }
      }
      return Content.Empty;
    }
    /// <summary>
    /// Determine if a tooltip information isnot empty.
    /// </summary>
    /// <param name="title">Tooltip title.</param>
    /// <param name="text">Tooltip text.</param>
    /// <param name="img">Tooltip image.</param>
    /// <returns>Boolean.</returns>
    public static bool containsToolTip(string title, string text, Image img)
    {
      return (!string.IsNullOrEmpty(title)) | (!string.IsNullOrEmpty(text)) | (img != null);
    }
    /// <summary>
    /// Measure the size of a tooltip based on its contents.
    /// </summary>
    /// <param name="title">Tooltip title.</param>
    /// <param name="text">Tooltip text.</param>
    /// <param name="img">Tooltip image.</param>
    /// <returns>Size.</returns>
    public static Size measureSize(string title, string text, Image img)
    {
      Size result = default(Size);
      int lText = 0;
      Size tSize = new Size(0, 0);
      int y = 0;
      switch (getContent(title, text, img))
      {
        case Content.All:
          tSize = TextRenderer.MeasureText(title, TitleFont);
          result.Width = tSize.Width + 8;
          result.Height = tSize.Height + 16 + img.Height;
          y = tSize.Height + 12;
          lText = img.Width + 8;
          tSize = TextRenderer.MeasureText(text, TextFont);
          if (result.Height < y + tSize.Height + 4)
          {
            result.Height = y + tSize.Height + 4;
          }
          if (result.Width < lText + tSize.Width + 4)
          {
            result.Width = lText + tSize.Width + 4;
          }
          break;
        case Content.TitleAndImage:
          result.Height = img.Height + 8;
          tSize = TextRenderer.MeasureText(title, TitleFont);
          if (result.Height < tSize.Height + 8)
          {
            result.Height = tSize.Height + 8;
          }
          result.Width = 12 + img.Width + tSize.Width;
          break;
        case Content.TitleAndText:
          tSize = TextRenderer.MeasureText(title, TitleFont);
          result.Height = tSize.Height + 12;
          result.Width = tSize.Width + 8;
          y = tSize.Height + 12;
          tSize = TextRenderer.MeasureText(text, TextFont);
          if (result.Width < tSize.Width + 8)
          {
            result.Width = tSize.Width + 8;
          }
          result.Height = y + tSize.Height + 4;
          break;
        case Content.TitleOnly:
          tSize = TextRenderer.MeasureText(title, TitleFont);
          result.Height = tSize.Height + 8;
          result.Width = tSize.Width + 8;
          break;
        case Content.ImageAndText:
          result.Height = img.Height + 8;
          tSize = TextRenderer.MeasureText(text, TextFont);
          if (result.Height < tSize.Height + 8)
          {
            result.Height = tSize.Height + 8;
          }
          result.Width = 12 + img.Width + tSize.Width;
          break;
        case Content.ImageOnly:
          result.Width = img.Width + 8;
          result.Height = img.Height + 8;
          break;
        case Content.TextOnly:
          tSize = TextRenderer.MeasureText(text, TextFont);
          result.Height = tSize.Height + 8;
          result.Width = tSize.Width + 8;
          break;
      }
      return result;
    }
    /// <summary>
    /// Draw tooltip information on a tooltip window.
    /// </summary>
    /// <param name="title">Tooltip title.</param>
    /// <param name="text">Tooltip text.</param>
    /// <param name="img">Tooltip image.</param>
    /// <param name="g">Graphics object used to paint.</param>
    /// <param name="rect">Bounding rectangle where tooltip information to be drawn.</param>
    public static void drawToolTip(string title, string text, Image img, Graphics g, Rectangle rect)
    {
      SizeF tSize = default(SizeF);
      float y = 0;
      var _with1 = rect;
      switch (getContent(title, text, img))
      {
        case Content.All:
          g.DrawString(title, TitleFont, TextBrush, _with1.X + 4, _with1.Y + 4);
          tSize = g.MeasureString(title, TitleFont);
          y = 8 + tSize.Height;
          g.DrawLine(SeparatorPen, _with1.X + 4, y, _with1.Right - 4, y);
          g.DrawLine(new Pen(Color.FromArgb(255, 255, 255)), _with1.X + 4, y + 1, _with1.Right - 4, y + 1);
          y = y + 4;
          g.DrawImage(img, _with1.X + 4, y, img.Width, img.Height);
          g.DrawString(text, TextFont, TextBrush, _with1.X + img.Width + 8, y);
          break;
        case Content.TitleAndImage:
          g.DrawImage(img, _with1.X + 4, _with1.Y + 4, img.Width, img.Height);
          g.DrawString(title, TitleFont, TextBrush, _with1.X + 8 + img.Width, _with1.Y + 4);
          break;
        case Content.TitleAndText:
          g.DrawString(title, TitleFont, TextBrush, _with1.X + 4, _with1.Y + 4);
          tSize = g.MeasureString(title, TitleFont);
          y = 8 + tSize.Height;
          g.DrawLine(SeparatorPen, _with1.X + 4, y, _with1.Right - 4, y);
          g.DrawLine(new Pen(Color.FromArgb(255, 255, 255)), _with1.X + 4, y + 1, _with1.Right - 4, y + 1);
          y = y + 4;
          g.DrawString(text, TextFont, TextBrush, _with1.X + 4, y);
          break;
        case Content.TitleOnly:
          g.DrawString(title, TitleFont, TextBrush, _with1.X + 4, _with1.Y + 4);
          break;
        case Content.ImageAndText:
          g.DrawImage(img, _with1.X + 4, _with1.Y + 4, img.Width, img.Height);
          g.DrawString(text, TextFont, TextBrush, _with1.X + 8 + img.Width, _with1.Y + 4);
          break;
        case Content.ImageOnly:
          g.DrawImage(img, _with1.X + 4, _with1.Y + 4, img.Width, img.Height);
          break;
        case Content.TextOnly:
          g.DrawString(text, TextFont, TextBrush, _with1.X + 4, _with1.Y + 4);
          break;
      }
    }
  }
  public class PopupEventArgs : EventArgs
  {
    System.Drawing.Size _size;
    public PopupEventArgs()
      : base()
    {
    }
    public System.Drawing.Size Size
    {
      get { return _size; }
      set { _size = value; }
    }
  }
  public class DrawEventArgs : EventArgs
  {
    System.Drawing.Graphics _g;
    Rectangle _rect;
    public DrawEventArgs(System.Drawing.Graphics g, System.Drawing.Rectangle rect)
      : base()
    {
      _g = g;
      _rect = rect;
    }
    public System.Drawing.Graphics Graphics
    {
      get { return _g; }
    }
    public System.Drawing.Rectangle Rectangle
    {
      get { return _rect; }
    }
  }
  /// <summary>
  /// Specifies the location of the tooltip will be shown.
  /// </summary>
  public enum ToolTipLocation
  {
    Auto,
    // Tooltip location will automatically calculated based on caller(Control, ToolStripItem) bounds, usually under the caller.
    MousePointer,
    // Tooltip will be shown around mouse pointer.
    CustomScreen,
    // Tooltip will be shown on a location in the screen specified by CustomLocation
    CustomClient
    // Tooltip will be shown on a location relative to the client area on the caller.
  }
}
