using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Soko.Common.Controls
{
  public partial class NicelyAnimatedStringList : UserControl
  {

    #region Constants



    #endregion  Constants



    #region Enums & Internal classes



    #endregion  Enums & Internal classes



    #region Private fields

    private double _TargetScrollPosition = 0;
    private List<double> _ItemLocationsRaw = new List<double>();
    private float _ItemSize = 1;

    private System.Windows.Forms.Timer _GuiRefreshTimer = new Timer();

    #endregion Private fields



    #region Constructors & finalizer

    public NicelyAnimatedStringList()
    {
      InitializeComponent();

      this.SetStyle(
              ControlStyles.OptimizedDoubleBuffer |
              ControlStyles.AllPaintingInWmPaint |
              ControlStyles.ResizeRedraw |
              ControlStyles.UserPaint, true);



      _GuiRefreshTimer.Interval = 20;
      _GuiRefreshTimer.Enabled = true;
      _GuiRefreshTimer.Tick += _GuiRefreshTimer_Tick;

      //for (int i = 0; i < 20; i++)
      //{
      //  _Items.Add("STR " + i.ToString());
      //}

      SetItemsLocationsRaw();
    }

    private double _ScrollQuanta = 0.01;

    private void _GuiRefreshTimer_Tick(object sender, EventArgs e)
    {
      if (_TargetScrollPosition > ScrollPosition)
      {
        ScrollPosition += Soko.Common.Common.CommonExtensions.Clamp(_TargetScrollPosition - ScrollPosition, 0, _ScrollQuanta);
      }

      if (_TargetScrollPosition < ScrollPosition)
      {
        ScrollPosition -= Soko.Common.Common.CommonExtensions.Clamp(ScrollPosition -_TargetScrollPosition, 0, _ScrollQuanta);
      }
    }

    #endregion Constructors & finalizer



    #region Events & OnEvent Methods

    protected override void OnPaint(PaintEventArgs e)
    {
      if (e == null)
        return;
      if (e.Graphics == null)
        return;

      StringFormat format = new StringFormat();
      format.LineAlignment = StringAlignment.Center;
      format.Alignment = StringAlignment.Center;

      Brush brush = new SolidBrush(this.ForeColor);

      //base.OnPaint(e);

      // Draw the shit on gear control
      //e.Graphics.FillRectangle(new SolidBrush(_RectanglesBgColor), _ScriptControlBoxRect);
      //e.Graphics.DrawRectangle(new Pen(_RectanglesColor, 2), _ScriptControlBoxRect);


      int xoffs = 0;


      for (int i = 0; i < _Items.Count; i++)
      {
        double raw = _ItemLocationsRaw[i];
        

        float size = (float)GetSizeMultiplier(raw) * _ItemSize;
        float y = (float)GetScreenLocation(raw);
        float ytop = y - size / 2.0f;

        Font font =  new Font(this.Font.FontFamily, this.Font.Size * (float)GetSizeMultiplier(raw), GraphicsUnit.Pixel);

        RectangleF itemRect = new RectangleF(0, ytop, this.Width, size);

        e.Graphics.DrawString(_Items[i], font, brush, itemRect, format);


        //e.Graphics.DrawLine(new Pen(Color.Red, 1), 0, y, 50, y);

        //e.Graphics.DrawRectangle(new Pen(Color.Blue, 1), xoffs, ytop, this.Width, size);
        xoffs += 5;
      }

      //e.Graphics.DrawLine(new Pen(_RectanglesColor, 2), _ScriptControlBox_LinePoint_TL, _ScriptControlBox_LinePoint_TR);
      //e.Graphics.DrawLine(new Pen(_RectanglesColor, 2), _ScriptControlBox_LinePoint_BL, _ScriptControlBox_LinePoint_BR);

      //// Draw the script control box
      //e.Graphics.FillRectangle(new SolidBrush(_RectanglesBgColor), _ScriptStatusBoxRect);
      //e.Graphics.DrawRectangle(new Pen(_RectanglesColor, 2), _ScriptStatusBoxRect);

      //// Draw separator line on the right
      //e.Graphics.DrawLine(new Pen(_RectanglesColor, 2), this.Right - 2, this.Top + 20, this.Right - 2, this.Bottom - 20);
    }

    #endregion Events & OnEvent Methods



    #region Threads & Timers & Methods



    #endregion Threads & Timers & Methods



    #region Properties

    

    private double _ScrollPosition = 0;
    public double ScrollPosition
    {
      get { return _ScrollPosition; }

      set
      {
        if (value < 0 || value > 1)
          return;

        _ScrollPosition = value;
        SetItemsLocationsRaw();
        this.Invalidate();
      }
    }

    private List<string> _Items = new List<string>();
    public List<string> Items
    {
      get { return _Items; }
      set
      {
        if (value == null)
        {
          return;
        }
        _Items = value;
        SetItemsLocationsRaw();
        Invalidate();
      }
    }


    private float _FocusedToNormalSizeRatio = 3;
    public float FocusedToNormalSizeRatio
    {
      get { return _FocusedToNormalSizeRatio; }
      set
      {
        if (value < 1)
          return;
        _FocusedToNormalSizeRatio = value;
        Invalidate();
      }
    }

   public float ItemSize
    {
      get { return _ItemSize; }
      set
      {
        if (value < 1)
          return;

        _ItemSize = value;
        Invalidate();
      }
    }

    #endregion Properties



    #region Private Methods

    private void SetItemsLocationsRaw()
    {
      if (_ItemLocationsRaw.Count != _Items.Count)
      {
        _ItemLocationsRaw.Clear();
        for (int i = 0; i < _Items.Count; i++)
        {
          _ItemLocationsRaw.Add(0.0);
        }
      }

      double span = _Items.Count - 1;

      double scrollOfset = span * _ScrollPosition;

      for (int i = 0; i < _Items.Count; i++)
      {
        _ItemLocationsRaw[i] = i - scrollOfset;
      }
    }

    private double GetScreenLocation(double rawLocation)
    {
      double centerOffset = this.Height / 2;

      //return (this.Height / 2.0) + rawLocation * GetLocationMultiplier(rawLocation) * this.Height / DisplayedItemsCount;

      if (Math.Abs(rawLocation) <= 1.0)
      {
        return centerOffset + rawLocation * _FocusedToNormalSizeRatio * _ItemSize;
      }
      else
      {
        if (rawLocation < 0)
        {
          return centerOffset + (rawLocation - _FocusedToNormalSizeRatio + 1) * _ItemSize;
        }
        else
        {
          return centerOffset + (rawLocation + _FocusedToNormalSizeRatio - 1) * _ItemSize;
        }
      }
    }

    private double GetLocationMultiplier(double rawLocation)
    {
      double abs = Math.Abs(rawLocation);
      if (abs <= 1.0)
      {
        return abs * _FocusedToNormalSizeRatio;
      }
      else
      {
        return (abs + _FocusedToNormalSizeRatio);
      }
    }

    private double GetSizeMultiplier(double rawLocation)
    {
      double abs = Math.Abs(rawLocation);
      if (abs <= 1.0)
      {
        return 1 + (1 - abs) * (_FocusedToNormalSizeRatio * 1.4);
      }
      else
      {
        return 1;// (abs + FocusedToNormalSizeRatio - 1);
      }
    }

    private double GetScreenSize(double rawLocation)
    {
      //double itemSize = this.Height / _DisplayedItemsCount;

      return _ItemSize * GetLocationMultiplier(rawLocation);
    }

    #endregion Private Methods



    #region Public Methods

    public void SetFocusedIndex(int index, bool animated)
    {
      if (index < 0) index = 0;
      if (index >= _Items.Count) index = _Items.Count - 1;

      if (animated)
      {
        _TargetScrollPosition = GetScrollPositionFromIndex(index);
      }
      else
      {
        ScrollPosition = GetScrollPositionFromIndex(index);
      }
    }

    private double GetScrollPositionFromIndex(int index)
    {
      return (double)index / (_Items.Count - 1.0) ;
    }

    #endregion Public Methods






  }
}
