using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;

using FP.Managers.GearboxManager;

namespace Bluereach.Components.Controls
{
  public partial class BarGauge : UserControl
  {


    #region Constants

    #endregion  Constants


    #region Private fields

    private float mCurrentValue = 0.0f;

    private DisplayChannel mDispChannel = new DisplayChannel();

    #endregion Private fields


    #region Constructors & finalizer

    public BarGauge()
    {
      this.SetStyle(
                    ControlStyles.SupportsTransparentBackColor |
                    ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.ResizeRedraw |
                    ControlStyles.UserPaint, true );
      InitializeComponent();
    }

    #endregion Constructors & finalizer


    #region Events

    #endregion Events


    #region Properties

    public override Size MinimumSize
    {
      get { return new Size(90, 150); }
      set { }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DisplayChannel DispChannel
    {
      get { return mDispChannel; }
      set
      {
        if (value == null)
        {
          throw new ArgumentNullException( "BarGauge.DispChannel: value must not be set to null" );
        }
        else
        {
          if (!(value.IsTempGauge || value.IsFlowGauge))
          {
            throw new ArgumentNullException("BarGauge.DispChannel: the channel must be flowmeter or thermometer ");
          }
          mDispChannel = value;
          channelLabel.Text = mDispChannel.Label;
          UpdateValue();
        }
      }
    }

    #endregion Properties


    #region Methods

    protected override void OnPaintBackground( PaintEventArgs e )
    {

      #region this must have been applied to support pseudo transparency
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
      #endregion
      //base.OnPaintBackground( e );
    }

    public void UpdateValue()
    {
      mCurrentValue = mDispChannel.Value;
      gaugeBar.FillRatio = mCurrentValue / mDispChannel.MaxValue;
      valueLabel.Text = mCurrentValue.ToString("0.0")+mDispChannel.UnitName;
    }

    public void jebalciepies()
    {
      UpdateValue();
    }

    protected override void OnResize(EventArgs e)
    {
      float margin = this.Width * 0.092f;

      gaugeBar.Left = valueLabel.Left = (int)margin;
      valueLabel.Top = this.Height - valueLabel.Height;
      gaugeBar.Height = valueLabel.Top - channelLabel.Bottom; 
      gaugeBar.Width = valueLabel.Width = this.Width - (int)(2 * margin);
      base.OnResize(e);
    }

    #endregion Methods




  }
}
