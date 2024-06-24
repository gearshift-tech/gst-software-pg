using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Data;
using System.Text;
using System.Windows.Forms;

using GST.Gearshift.Components.Interfaces.USB;

namespace GST.Gearshift.Components.Controls.Gauges
{
  public partial class SpeedsGauge : Control
  {


    #region Constants

    #endregion  Constants


    #region Private fields

    private GearShiftUsb.AIChannel _inputSpeedChannel = null;//new GearShiftUsb.AIChannel();
    private List<GearShiftUsb.AIChannel> _outputSpeedChannelsList = new List<GearShiftUsb.AIChannel>();
    private GearShiftUsb.AIChannel _gearRatioChannel = null;//new GearShiftUsb.AIChannel();

    private int _inputSpeedArrayIndex = -1;
    private List<int> _outputSpeedsArrayIndices = new List<int>();
    private int _gearRatioArrayIndex = -1;

    private int _itemsToDisplay = 0;

    public GearShiftUsb device = null;

    #endregion Private fields


    public void InitControl(GearboxConfig gbc, GearShiftUsb usbDev)
    {
      _inputSpeedChannel = null;
      _outputSpeedChannelsList = new List<GearShiftUsb.AIChannel>();
      _gearRatioChannel = null;
      _inputSpeedArrayIndex = -1;
      _outputSpeedsArrayIndices = new List<int>();
      _gearRatioArrayIndex = -1;
      _itemsToDisplay = 0;

      device = usbDev;

      for (int i = 0; i < gbc._analogueInputs.Count; i++)
      {
        GearShiftUsb.AIChannel aic = gbc._analogueInputs[i];
        switch (aic.ValueType)
        {
          case Utilities.MeasurementUnit.ValueType.InputSpeed:
            {
              _inputSpeedChannel = aic;
              _inputSpeedArrayIndex = i;
              _itemsToDisplay++;
              break;
            }
          case Utilities.MeasurementUnit.ValueType.OutputSpeed:
            {
              _outputSpeedChannelsList.Add(aic);
              _outputSpeedsArrayIndices.Add(i);
              _itemsToDisplay++;
              break;
            }
          case Utilities.MeasurementUnit.ValueType.GearRatio:
            {
              _gearRatioChannel = aic;
              _gearRatioArrayIndex = i;
              _itemsToDisplay++;
              break;
            }
        }
      }

    }

    public void UpdateValue()
    {
      //mCurrentValue = mDispChannel.Value;
      //gaugeBar.FillRatio = mCurrentValue / mDispChannel.MaxValue;
      //valueLabel.Text = mCurrentValue.ToString("0.0")+mDispChannel.UnitName;

      this.Invalidate();
    }


    #region Constructors & finalizer

    public SpeedsGauge()
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
      get { return new Size(150, 150); }
      set { }
    }

    #endregion Properties


    #region Methods

    protected override void OnPaint(PaintEventArgs e)
    {
      float itemHeight = this.Height / (float)_itemsToDisplay;
      float itemWidth = this.Width;
      float currentYpos = 0;

      Graphics g = e.Graphics;

      g.SmoothingMode = SmoothingMode.HighQuality;
      g.CompositingQuality = CompositingQuality.HighQuality;
      g.TextRenderingHint = TextRenderingHint.AntiAlias;
      g.InterpolationMode = InterpolationMode.High;
      g.PixelOffsetMode = PixelOffsetMode.Half;

      // Calculate proper font size
      float valuefontSize = itemHeight * 0.6f;
      Font valueFont = new System.Drawing.Font("led16sgmnt", valuefontSize, FontStyle.Italic);
      SizeF textsize = g.MeasureString("00000", valueFont);
      if (textsize.Width > itemWidth * 0.75f)
      {
        valuefontSize *= (itemWidth * 0.75f) / textsize.Width;
      }



      // Draw input speed
      if (_inputSpeedChannel != null)
      {
        double value = device.GetLatestAIValueUserUnit(_inputSpeedChannel, _inputSpeedArrayIndex);
        value = Math.Round(value, 0);
        DrawItem(g, new RectangleF(0, currentYpos, itemWidth, itemHeight), _inputSpeedChannel.Label, value.ToString(), valuefontSize);
        currentYpos += itemHeight;
      }
      // Draw output speed(s)
      for (int i = 0; i < _outputSpeedChannelsList.Count; i++)
      {
        double value = device.GetLatestAIValueUserUnit(_outputSpeedChannelsList[i], _outputSpeedsArrayIndices[i]);
        value = Math.Round(value, 0);
        DrawItem(g, new RectangleF(0, currentYpos, itemWidth, itemHeight), _outputSpeedChannelsList[i].Label, value.ToString(), valuefontSize);
        currentYpos += itemHeight;
      }
      // Draw ratio
      if (_gearRatioChannel != null)
      {
        double value = device.GetLatestAIValueUserUnit(_gearRatioChannel, _gearRatioArrayIndex);
        string strval = value.ToString("0.00");
        if (value >= 10) strval = "INF";
        if (value < 0) strval = "Err";
        DrawItem(g, new RectangleF(0, currentYpos, itemWidth, itemHeight), _gearRatioChannel.Label, strval, valuefontSize);
        currentYpos += itemHeight;
      }
    }

    private void DrawItem(Graphics g, RectangleF rect, string title, string value, float fontsize)
    {
      float valuefontSize = fontsize;
      Font valueFont = new System.Drawing.Font("led16sgmnt", valuefontSize, FontStyle.Italic);
      StringFormat vsf = new StringFormat();
      vsf.Alignment = StringAlignment.Far;
      vsf.LineAlignment = StringAlignment.Far;

      float titlefontSize = fontsize * 0.25f;
      Font titleFont = new System.Drawing.Font("Segoe UI", titlefontSize);
      StringFormat tsf = new StringFormat();
      tsf.Alignment = StringAlignment.Near;
      tsf.LineAlignment = StringAlignment.Near;

      g.DrawString(title, titleFont, new SolidBrush(Color.Black), rect, tsf);
      g.DrawString(value, valueFont, new SolidBrush(Color.Red), rect, vsf);

      g.DrawRectangle(new Pen(new SolidBrush(Color.Black)), new Rectangle((int)rect.Left + 1, (int)rect.Top + 1, (int)rect.Width - 2, (int)rect.Height - 2));
    }

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



    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
    }

    #endregion Methods




  }
}
