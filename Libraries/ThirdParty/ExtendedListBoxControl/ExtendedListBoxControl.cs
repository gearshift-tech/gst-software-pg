using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using ExtendedListBoxControl.ExtendedListBoxItemClasses;

namespace ExtendedListBoxControl
{
  public class ExtendedListBoxControl : ListBox
  {
    #region Properties and such
    protected BorderTypes _borderType = BorderTypes.none;
    [Category("Item Appearance")]
    [Description("Choose to either have no border, or a border with square or rounded edges")]
    public BorderTypes BorderType
    {
      get { return _borderType; }
      set { _borderType = value; }
    }

    protected string _titleText = String.Empty;
    [Category("Item Appearance")]
    [Description("String displayed when minimied")]
    public string TitleText
    {
      get { return _titleText; }
      set { _titleText = value; }
    }

    protected Color _titleTextColor = Color.Black;
    [Category("Item Appearance")]
    public Color TitleTextColor
    {
      get { return _titleTextColor; }
      set
      {
        _titleTextColor = value;
      }
    }

    protected Font _titleTextFont = new Font("Segoe UI", 16.0f, FontStyle.Bold);
    [Category("Item Appearance")]
    public Font TitleTextFont
    {
      get { return _titleTextFont; }
      set
      {
        _titleTextFont = value;
      }
    }

    protected string _contentText = String.Empty;
    [Category("Item Appearance")]
    [Description("String displayed when item is extended")]
    public string ContentText
    {
      get { return _contentText; }
      set { _contentText = value; }
    }

    protected Color _contentTextColor = Color.Black;
    [Category("Item Appearance")]
    public Color ContentTextColor
    {
      get { return _contentTextColor; }
      set
      {
        _contentTextColor = value;
      }
    }

    protected Font _contentTextFont = new Font("Segoe UI", 12.0f, FontStyle.Italic);
    [Category("Item Appearance")]
    public Font ContentTextFont
    {
      get { return _contentTextFont; }
      set
      {
        _contentTextFont = value;
      }
    }

    protected Color _backColor1 = Color.Gainsboro;
    [Category("Item Appearance")]
    [Description("Linear Gradient Color one")]
    public Color BackColor1
    {
      get { return _backColor1; }
      set { _backColor1 = value; }
    }

    protected Color _backColor2 = Color.LightSlateGray;
    [Category("Item Appearance")]
    [Description("Linear Gradient Color two")]
    public Color BackColor2
    {
      get { return _backColor2; }
      set { _backColor2 = value; }
    }

    protected float _focusAngle = 65f;
    [Category("Item Appearance")]
    [Description("(MSDN) \"The angle, measured in degrees clockwise from the x-axis, of the gradient's orientation line. \"")]
    public float FocusAngle
    {
      get { return _focusAngle; }
      set { _focusAngle = value; }
    }
    #endregion

    public ExtendedListBoxControl()
    {
      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      // 
      // ExtendedListBoxControl
      // 
      this.DrawMode = System.Windows.Forms.DrawMode.Normal;
      this.ItemHeight = 80;
      this.Size = new System.Drawing.Size(120, 84);
      //this.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ExtendedListBoxControl_DrawItem);
      this.ResumeLayout(false);

    }

    protected override void OnSelectedIndexChanged(EventArgs e)
    {
      this.Invalidate();
      base.OnSelectedIndexChanged(e);
    }

    private void ExtendedListBoxControl_DrawItem(object sender, DrawItemEventArgs e)
    {
      DoDrawItem(e.Graphics, e.Bounds);
    }
    private void DoDrawItem(Graphics g, Rectangle bounds)
    {
      RectangleF titleRectangle = new RectangleF(
        bounds.X + bounds.Width * 0.1f,
        bounds.Y,
        bounds.Width * 0.7f,
        bounds.Height / 2.0f);

      RectangleF contentRectangle = new RectangleF(
          bounds.X + bounds.Width * 0.1f,
          bounds.Y + bounds.Height / 2.0f,
          bounds.Width * 0.7f,
          bounds.Height / 2.0f);

      //using (LinearGradientBrush lgb = new LinearGradientBrush(
      //    bounds, _backColor1, _backColor2, _focusAngle, true))
      //{
      //  //Draw border then fill its interior
      //  using (GraphicsPath gp = DrawBorder(e))
      //  {
      //    g.FillPath(lgb, gp);

      //  }
      //}

      StringFormat sf = new StringFormat
      {
        Alignment = StringAlignment.Near,
        LineAlignment = StringAlignment.Center
      };
      g.DrawString(_titleText, _titleTextFont, new SolidBrush(_titleTextColor), titleRectangle, sf);
        g.DrawString(_contentText, _contentTextFont, new SolidBrush(_contentTextColor), contentRectangle);

      //g.DrawString("DUPA", Font, SystemBrushes.WindowText, bounds);
    }

    //private GraphicsPath DrawBorder(DrawItemEventArgs e)
    //{
    //  Rectangle rct = e.Bounds;
    //  GraphicsPath gp = new GraphicsPath();

    //  rct.Width -= 1;

    //  if (_borderType == BorderTypes.none)
    //    gp.AddRectangle(rct);

    //  if (_borderType == BorderTypes.square)
    //  {
    //    gp.AddRectangle(rct);
    //    e.Graphics.DrawRectangle(new Pen(Color.Black, 1), rct);
    //  }

    //  if (_borderType == BorderTypes.rounded)
    //  {
    //    Rectangle arcRct = new Rectangle(rct.X, rct.Y, ArcWidth, ArcWidth);
    //    Point pt1 = new Point(rct.X + ArcWidth, rct.Y);
    //    Point pt2 = new Point(rct.X + rct.Width - ArcWidth, rct.Y);

    //    gp.AddArc(arcRct, 180, 90);
    //    gp.AddLine(pt1, pt2);

    //    arcRct.Location = pt2;
    //    gp.AddArc(arcRct, 270, 90);

    //    pt1 = new Point(rct.X + rct.Width, rct.Y + ArcWidth);
    //    pt2 = new Point(rct.X + rct.Width, rct.Y + rct.Height - ArcWidth);
    //    gp.AddLine(pt1, pt2);

    //    arcRct.Y = pt2.Y;
    //    gp.AddArc(arcRct, 0, 90);

    //    pt1 = new Point(rct.X + rct.Width - ArcWidth, rct.Y + rct.Height);
    //    pt2 = new Point(rct.X + ArcWidth, rct.Y + rct.Height);
    //    gp.AddLine(pt1, pt2);

    //    arcRct.X = rct.X;
    //    gp.AddArc(arcRct, 90, 90);

    //    gp.CloseFigure();

    //    e.Graphics.DrawPath(new Pen(Color.Black, 1), gp);
    //  }
    //  return gp;
    //}

    Bitmap backgroundImage = null;
    bool isDrawingBackground = false;

    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
      if (isDrawingBackground)
        return;

      if (Parent != null)
      {
        isDrawingBackground = true;
        if (backgroundImage != null)
          backgroundImage.Dispose();
        backgroundImage = new Bitmap(ClientSize.Width, ClientSize.Height);
        Parent.DrawToBitmap(backgroundImage, new Rectangle(Left, Top, ClientSize.Width, ClientSize.Height));
        isDrawingBackground = false;
      }

      base.OnPaintBackground(pevent);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (isDrawingBackground)
        return;

      base.OnPaint(e);

      if (backgroundImage != null)
        e.Graphics.DrawImage(backgroundImage, e.ClipRectangle);


      //if (this.Focused && this.SelectedItem != null)
      //{
      //  //  
      //  Rectangle itemRect = this.GetItemRectangle(this.SelectedIndex);
      //  //e.Graphics.FillRectangle(Brushes.Red, itemRect);  
      //  e.Graphics.FillRectangle(Brushes.LightBlue, itemRect);
      //}
      //for (int i = 0; i < Items.Count; i++)
      //{
      //  //  
      //  //StringFormat strFmt = new System.Drawing.StringFormat();
      //  //strFmt.Alignment = StringAlignment.Center; //  
      //  //strFmt.LineAlignment = StringAlignment.Center; //  
      //  //e.Graphics.DrawString(this.GetItemText(this.Items[i]), this.Font, new SolidBrush(this.ForeColor), this.GetItemRectangle(i), strFmt);

      //  //e.Graphics.DrawString(this.GetItemText(this.Items[i]), this.Font, new SolidBrush(this.ForeColor), this.GetItemRectangle(i));  
      //  DoDrawItem(e.Graphics, this.GetItemRectangle(i));
      //}

      
    }
  }  


  ///// <summary>
  ///// ExtendedListBox Control class
  ///// </summary>
  //public class ExtendedListBoxControl : System.Windows.Forms.ListBox
  //{
  //  #region Variables, properties and such

  //  /// <summary>
  //  /// Previous index - Used when we are updating thelist items.
  //  /// </summary>
  //  private int _previousIndex = -1;

  //  /// <summary>
  //  /// Currently selected item
  //  /// </summary>
  //  private int _currentIndex = -1;

  //  /// <summary>
  //  /// Is the variable for determining if collapsed or not
  //  /// </summary>
  //  private bool isCollapsed = true;

  //  /// <summary>
  //  ///Holds custom WWExtendedListItem objects
  //  /// </summary>
  //  private ArrayList itemCache = new ArrayList();

  //  public delegate void ListItemClickEventHandler(object sender, XLBIEventArgs e);
  //  public event ListItemClickEventHandler ListItemClick;

  //  #endregion

  //  public ExtendedListBoxControl()
  //  {
  //  //  this.SetStyle(
  //  //ControlStyles.SupportsTransparentBackColor |
  //  //ControlStyles.OptimizedDoubleBuffer |
  //  ////ControlStyles.AllPaintingInWamPaint |
  //  //ControlStyles.ResizeRedraw
  //  ////| ControlStyles.UserPaint
  //  //, true);

  //    this.SetStyle( ControlStyles.SupportsTransparentBackColor, true);
  //  }

          


  //  #region ArrayList methods

  //  /// <summary>
  //  /// Adds an XLBC item to the item cache
  //  /// </summary>
  //  /// <param name="xlbi">ExtendedListBoxItem to be added</param>
  //  public void AddItem(ExtendedListBoxItem xlbi)
  //  {
  //    itemCache.Add(xlbi);
  //    //NOTE Adding a dummy as a placeholder here for the object I'm
  //    //	going to draw!
  //    this.Items.Add(" ");
  //  }

  //  public void RemoveItem()
  //  {
  //    if ((_currentIndex < 0) || (_currentIndex >= itemCache.Count))
  //      return;

  //    itemCache.RemoveAt(_currentIndex);
  //    //NOTE We have to remove item at correct index!
  //    this.Items.RemoveAt(_currentIndex);

  //    //Now set the List as not having a selected item.
  //    _currentIndex = -1;
  //  }

  //  public void ClearItems()
  //  {
  //    itemCache.Clear();
  //    this.Items.Clear();

  //    //Now set the List as not having a selected item.
  //    _currentIndex = -1;
  //  }

  //  #endregion

  //  #region Owner Drawn overrides

  //  protected override void OnPaintBackground(PaintEventArgs e)
  //  {
  //    if (e == null)
  //      return;
  //    if (e.Graphics == null)
  //      return;

  //    //this must have been applied to support pseudo transparency
  //    //if (this.Parent != null)
  //    //{
  //    //  GraphicsContainer cstate = e.Graphics.BeginContainer();
  //    //  e.Graphics.TranslateTransform(-this.Left, -this.Top);
  //    //  Rectangle clip = e.ClipRectangle;
  //    //  clip.Offset(this.Left, this.Top);
  //    //  PaintEventArgs pe = new PaintEventArgs(e.Graphics, clip);

  //    //  //paint the container's bg
  //    //  InvokePaintBackground(this.Parent, pe);
  //    //  //paints the container fg
  //    //  //InvokePaint(this.Parent, pe);
  //    //  //restores graphics to its original state
  //    //  e.Graphics.EndContainer(cstate);
  //    //}
  //    //else
  //    {
  //      base.OnPaintBackground(e);
  //    }
  //  }

  //  /// <summary>
  //  /// Called when the item needs to be drawn
  //  /// </summary>
  //  /// <param name="e">DrawItemEventArgs</param>
  //  //protected override void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e)
  //  //{
  //  //  base.OnDrawItem(e);

  //  //  //If not a valid index just ignore
  //  //  if ((e.Index < 0) || (e.Index >= itemCache.Count))
  //  //    return;

  //  //  ExtendedListBoxItem xlbi = (ExtendedListBoxItem)itemCache[e.Index];

  //  //  //Smooth drawing shapes!  Without this shapes are drawn with ragged edges,
  //  //  //  especially arcs.
  //  //  e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

  //  //  xlbi.Draw(e);
  //  //}

  //  /// <summary>
  //  /// Called when invalidated to find the item bounds.  Since we are only concerned
  //  ///		with the height we only need to set this value and leave the bounds.width
  //  ///		as is!
  //  /// </summary>
  //  /// <param name="e">MeasureItemEventArgs</param>
  //  //protected override void OnMeasureItem(System.Windows.Forms.MeasureItemEventArgs e)
  //  //{
  //  //  base.OnMeasureItem(e);

  //  //  if ((e.Index < 0) || (e.Index >= itemCache.Count))
  //  //    return;

  //  //  ExtendedListBoxItem xlbi = (ExtendedListBoxItem)itemCache[e.Index];

  //  //  e.ItemHeight = xlbi.ItemHeight;

  //  //  e.ItemWidth = this.Width;
  //  //}

  //  /// <summary>
  //  /// We use MouseDown instead of SelectedIndexChanged because the SelectedIndexChanged
  //  ///		handler is not called unless we choose an item other than the current one and
  //  ///		since we want to toggle the state we use this handler instead!
  //  /// </summary>
  //  /// <remarks>
  //  /// A special note here......
  //  /// To have the ListBox update the previous and current items in the list we must remove
  //  ///		and then re-add the items.  If this is not done the MeasureItem handler does not
  //  ///		get called and the ListBox does not get updated.
  //  /// </remarks>
  //  /// <param name="e">MouseEventArgs</param>
  //  //protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
  //  //{
  //  //  base.OnMouseDown(e);

  //  //  //      bool hit = false;

  //  //  //_previousIndex = _currentIndex;
  //  //  //_currentIndex = this.SelectedIndex;

  //  //  //      if ((_currentIndex >= 0) && (_currentIndex < itemCache.Count))
  //  //  //          hit = ((ExtendedListBoxItem)itemCache[_currentIndex]).HitCheck(e.Location);

  //  //  //      //If current index is selected toggle state
  //  //  ////	else just expand
  //  //  //if (_previousIndex == _currentIndex)
  //  //  //  isCollapsed = !isCollapsed;
  //  //  //else
  //  //  //{
  //  //  //  if ((_currentIndex >= 0) && (_currentIndex < itemCache.Count))
  //  //  //    isCollapsed = false;
  //  //  //  else 
  //  //  //    isCollapsed = true;
  //  //  //}

  //  //  ////Update previous selection
  //  //  //      InvalidateItem(_previousIndex);

  //  //  ////Update current selection
  //  //  //      InvalidateItem(_currentIndex);

  //  //  //      if ((ListItemClick != null) && (_currentIndex >= 0))
  //  //  //           ListItemClick(this, new XLBIEventArgs((ExtendedListBoxItem)itemCache[_currentIndex], hit));
  //  //}

  //  /// <summary>
  //  /// Invalidates the item at index
  //  /// </summary>
  //  /// <param name="index"></param>
  //  public void InvalidateItem(int index)
  //  {
  //    if ((index < 0) || (index >= itemCache.Count))
  //      return;

  //    //All we need to do here is make sure we get the correct item index
  //    //  since it is just a place holder!
  //    this.Items.RemoveAt(index);
  //    this.Items.Insert(index, " ");
  //  }

  //  #endregion

  //  private void InitializeComponent()
  //  {
  //    this.SuspendLayout();
  //    // 
  //    // ExtendedListBoxControl
  //    // 
  //    this.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
  //    this.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.DoDrawItem);
  //    this.ResumeLayout(false);

  //  }

  //  private void DoDrawItem(object sender, DrawItemEventArgs e)
  //  {
  //    //If not a valid index just ignore
  //    if ((e.Index < 0) || (e.Index >= itemCache.Count))
  //      return;

  //    ExtendedListBoxItem xlbi = (ExtendedListBoxItem)itemCache[e.Index];

  //    //Smooth drawing shapes!  Without this shapes are drawn with ragged edges,
  //    //  especially arcs.
  //    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

  //    xlbi.Draw(e);
  //  }
  //}

  ///// <summary>
  ///// Event Argumemnts for the this class. Maintains a field for selected item.
  ///// </summary>
  //public class XLBIEventArgs : EventArgs
  //{
  //  public ExtendedListBoxItem entry = null;
  //  public bool ctrlHit = false;

  //  public XLBIEventArgs() { }

  //  public XLBIEventArgs(ExtendedListBoxItem obj, bool hit)
  //  {
  //    entry = obj;
  //    ctrlHit = hit;
  //  }
  //}
}
