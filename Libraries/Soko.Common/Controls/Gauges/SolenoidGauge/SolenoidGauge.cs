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
  public partial class SolenoidGauge : UserControl
  {


    #region Constants

    //private readonly UInt32 mZeroingDelayTimerLoadVal_ms = 200;

    #endregion  Constants


    #region Private fields

    private float mCurrentValue = 0.0f;

    private float mCurrentMax = 3.0f;

    //private int prevSliderVal = 0;

    private DisplayChannel mDispChannel = new DisplayChannel();

    #endregion Private fields


    #region Constructors & finalizer

    public SolenoidGauge()
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
          throw new ArgumentNullException( "SolenoidGauge.DispChannel value must not be set to null" );
        }
        else
        {
          mDispChannel = value;
          mCurrentMax = mDispChannel.MaxValue;
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
      currentGaugeBar.FillRatio = mCurrentValue / mCurrentMax;
      currentValueLabel.Text = mCurrentValue.ToString("0.00")+"[A]";
    }

    public void jebalciepies()
    {
      UpdateValue();
    }

    protected override void OnResize(EventArgs e)
    {
      float margin = this.Width * 0.0444444444444f;

      currentCaptionLabel.Left = currentGaugeBar.Left = currentValueLabel.Left = (int)margin;
      driveCaptionLabel.Left = driveTrackbar.Left = driveValueLabel.Left = this.Width - (int)margin - driveTrackbar.Width;
      currentValueLabel.Top = driveValueLabel.Top = this.Height - currentValueLabel.Height;
      currentGaugeBar.Height = driveTrackbar.Height = driveValueLabel.Top - driveCaptionLabel.Bottom;
      currentCaptionLabel.Width = currentGaugeBar.Width = currentValueLabel.Width = driveTrackbar.Left - (int)(2 * margin);
      base.OnResize(e);
    }

    private void driveTrackbar_ValueChanged(object sender, decimal value)
    {
      driveValueLabel.Text = value.ToString() + "[%]";
      mDispChannel.SliderControlValue = (int)value;
      if (value > 0)
      {      
        mDispChannel.IsSliderControlled = true;
      }
      else
      {
        disableDelayTmr.Enabled = true;
      }
    }

    #endregion Methods

    private void disableDelayTmr_Tick(object sender, EventArgs e)
    {
      disableDelayTmr.Enabled = false;
      mDispChannel.IsSliderControlled = false;
    }




  }
}
