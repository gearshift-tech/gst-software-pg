using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Soko.CanCave.Components.Controls
{



  public partial class SimpleTempGauge : UserControl
  {

    #region Constants



    #endregion  Constants

    #region Private fields


    private int mTextImageSpacing = 5;
    private PointF mImagePosition = new PointF(0.0f, 0.0f);

    private Image _Image = new Bitmap(1, 1);

    private RectangleF mTextRectangle = new Rectangle(0, 0, 0, 0);

    private double _Value = 0;

    #endregion Private fields

    #region Constructors & finalizer

    public SimpleTempGauge()
    {
      InitializeComponent();

      this.SetStyle(
          ControlStyles.SupportsTransparentBackColor |
          ControlStyles.OptimizedDoubleBuffer |
          ControlStyles.AllPaintingInWmPaint |
          ControlStyles.ResizeRedraw |
          ControlStyles.UserPaint, true);
    }

    #endregion Constructors & finalizer

    #region Events

    public event EventHandler OnStateChangedEvent;

    #endregion Events

    #region Properties

    public double Value
    {
      get { return _Value; }
      set
      {
        _Value = value;
        Invalidate();
      }
    }

    public Image Image
    {
      get { return _Image; }
      set 
      { 
        _Image = value;
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

      g.DrawImageUnscaled(_Image, 0, 0);

      RectangleF barRect = new RectangleF(6, 6, 38, 57);
      float normalisedValue = (float)(this.Value / 150.0f);
      if (normalisedValue > 1) normalisedValue = 1;
      float barHeight = barRect.Height * normalisedValue;

      g.FillRectangle(new SolidBrush(Color.FromArgb(0,176,80)), new RectangleF(barRect.Left, barRect.Bottom - barHeight, barRect.Width, barHeight));

      //draw the text
      string strToDraw = Value.ToString();

      mTextRectangle = new RectangleF(this.Width / 6.0f, this.Height * 0.4f, this.Width * 0.66f, 10);
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

    #endregion Methods



  }
}
