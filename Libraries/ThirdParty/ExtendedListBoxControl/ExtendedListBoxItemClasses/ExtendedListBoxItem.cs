using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;

namespace ExtendedListBoxControl.ExtendedListBoxItemClasses
{
  public enum BorderTypes
  {
    none,
    square,
    rounded
  };

  /// <summary>
  /// Summary description for ListBoxItemBase.
  /// </summary>
  public class ExtendedListBoxItem : object
  {
    #region Properties and such

    protected BorderTypes _borderType = BorderTypes.none;
    [Category("Appearance")]
    [Description("Choose to either have no border, or a border with square or rounded edges")]
    public BorderTypes BorderType
    {
      get { return _borderType; }
      set { _borderType = value; }
    }

    protected int _itemHeight = 100;
    [Category("Appearance")]
    [Description("Item size when extended...Does not take effect until item reselected")]
    public int ItemHeight
    {
      get { return _itemHeight; }
      set { _itemHeight = value; }
    }

    protected string _titleText = String.Empty;
    [Category("Appearance")]
    [Description("String displayed when minimied")]
    public string TitleText
    {
      get { return _titleText; }
      set { _titleText = value; }
    }

    protected Color _titleTextColor = Color.Black;
    [Category("Appearance")]
    public Color TitleTextColor
    {
      get { return _titleTextColor; }
      set
      {
        _titleTextColor = value;
      }
    }

    protected Font _titleTextFont = new Font("Segoe UI", 16.0f, FontStyle.Bold);
    [Category("Appearance")]
    public Font TitleTextFont
    {
      get { return _titleTextFont; }
      set
      {
        _titleTextFont = value;
      }
    }

    protected string _contentText = String.Empty;
    [Category("Appearance")]
    [Description("String displayed when item is extended")]
    public string ContentText
    {
      get { return _contentText; }
      set { _contentText = value; }
    }

    protected Color _contentTextColor = Color.Black;
    [Category("Appearance")]
    public Color ContentTextColor
    {
      get { return _contentTextColor; }
      set
      {
        _contentTextColor = value;
      }
    }

    protected Font _contentTextFont = new Font("Segoe UI", 12.0f, FontStyle.Italic);
    [Category("Appearance")]
    public Font ContentTextFont
    {
      get { return _contentTextFont; }
      set
      {
        _contentTextFont = value;
      }
    }

    protected Color _backColor1 = Color.Gainsboro;
    [Category("Appearance")]
    [Description("Linear Gradient Color one")]
    public Color BackColor1
    {
      get { return _backColor1; }
      set { _backColor1 = value; }
    }

    protected Color _backColor2 = Color.LightSlateGray;
    [Category("Appearance")]
    [Description("Linear Gradient Color two")]
    public Color BackColor2
    {
      get { return _backColor2; }
      set { _backColor2 = value; }
    }

    protected float _focusAngle = 65f;
    [Category("Appearance")]
    [Description("(MSDN) \"The angle, measured in degrees clockwise from the x-axis, of the gradient's orientation line. \"")]
    public float FocusAngle
    {
      get { return _focusAngle; }
      set { _focusAngle = value; }
    }

    protected object _tag = null;
    [Description("(MSDN) \"Object to store item specific data \"")]
    public object Tag
    {
      get { return _tag; }
      set { _tag = value; }
    }

    #endregion

    public ExtendedListBoxItem()
    {
    }

    public ExtendedListBoxItem(int height, string title, string content, Color c1, Color c2)
    {
      _itemHeight = height;
      TitleText = title;
      ContentText = content;
      _backColor1 = c1;
      _backColor2 = c2;
    }

    /// <summary>
    /// Draw the item in the extended state
    /// </summary>
    /// <param name="e"></param>
    public virtual void Draw(DrawItemEventArgs e)
    {
      RectangleF titleRectangle = new RectangleF(
          e.Bounds.X + e.Bounds.Width * 0.1f,
          e.Bounds.Y,
          e.Bounds.Width * 0.7f,
          e.Bounds.Height / 2.0f);

      RectangleF contentRectangle = new RectangleF(
          e.Bounds.X + e.Bounds.Width * 0.1f,
          e.Bounds.Y + e.Bounds.Height / 2.0f,
          e.Bounds.Width * 0.7f,
          e.Bounds.Height / 2.0f);

      LinearGradientBrush lgb = new LinearGradientBrush(
          e.Bounds,
          _backColor1,
          _backColor2,
          _focusAngle,
          true);

      //Draw border then fill its interior
      GraphicsPath gp = DrawBorder(e);
      e.Graphics.FillPath(lgb, gp);

      StringFormat sf = new StringFormat
      {
        Alignment = StringAlignment.Near,
        LineAlignment = StringAlignment.Center
      };
      e.Graphics.DrawString(_titleText, _titleTextFont, new SolidBrush(_titleTextColor), titleRectangle, sf);
      e.Graphics.DrawString(_contentText, _contentTextFont, new SolidBrush(_contentTextColor), contentRectangle);

      gp.Dispose();
      lgb.Dispose();
    }

    private const int ArcWidth = 10;
    /// <summary>
    /// Draw the Border around the item
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    protected GraphicsPath DrawBorder(DrawItemEventArgs e)
    {
      Rectangle rct = e.Bounds;
      GraphicsPath gp = new GraphicsPath();

      rct.Width -= 1;

      if (_borderType == BorderTypes.none)
        gp.AddRectangle(rct);

      if (_borderType == BorderTypes.square)
      {
        gp.AddRectangle(rct);
        e.Graphics.DrawRectangle(new Pen(Color.Black, 1), rct);
      }

      if (_borderType == BorderTypes.rounded)
      {
        Rectangle arcRct = new Rectangle(rct.X, rct.Y, ArcWidth, ArcWidth);
        Point pt1 = new Point(rct.X + ArcWidth, rct.Y);
        Point pt2 = new Point(rct.X + rct.Width - ArcWidth, rct.Y);

        gp.AddArc(arcRct, 180, 90);
        gp.AddLine(pt1, pt2);

        arcRct.Location = pt2;
        gp.AddArc(arcRct, 270, 90);

        pt1 = new Point(rct.X + rct.Width, rct.Y + ArcWidth);
        pt2 = new Point(rct.X + rct.Width, rct.Y + rct.Height - ArcWidth);
        gp.AddLine(pt1, pt2);

        arcRct.Y = pt2.Y;
        gp.AddArc(arcRct, 0, 90);

        pt1 = new Point(rct.X + rct.Width - ArcWidth, rct.Y + rct.Height);
        pt2 = new Point(rct.X + ArcWidth, rct.Y + rct.Height);
        gp.AddLine(pt1, pt2);

        arcRct.X = rct.X;
        gp.AddArc(arcRct, 90, 90);

        gp.CloseFigure();

        e.Graphics.DrawPath(new Pen(Color.Black, 1), gp);
      }
      return gp;
    }

  }
}
