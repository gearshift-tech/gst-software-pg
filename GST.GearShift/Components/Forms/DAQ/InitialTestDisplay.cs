using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

using GST.Gearshift.Components.Interfaces.USB;

namespace GST.Gearshift.Components.Forms.DAQ
{
  public partial class InitialTestDisplay : UserControl
  {


    #region Constants



    #endregion  Constants



    #region Private fields

    private DisplayChannel mChannel = null;

    // Values rectangle point
    static Point vrp = new Point(0, 0);
    static int blkwdth = 66;
    static int blkspcng = 4;
    static int blkffst = blkwdth + blkspcng;

    static Rectangle dccr = new Rectangle(vrp.X + 0, vrp.Y + 0, 66, 12);
    static Rectangle ratedcr = new Rectangle(vrp.X + blkffst, vrp.Y + 0, 66, 12);
    static Rectangle tolerancecr = new Rectangle(vrp.X + blkffst*2, vrp.Y + 0, 66, 12);
    static Rectangle currcr = new Rectangle(vrp.X + blkffst*3, vrp.Y + 0, 66, 12);
    static Rectangle errAbscr = new Rectangle(vrp.X + blkffst*4, vrp.Y + 0, 66, 12);
    static Rectangle errPerccr = new Rectangle(vrp.X + blkffst*5, vrp.Y + 0, 66, 12);

    static Rectangle ls1r = new Rectangle(vrp.X + 143, vrp.Y + 7, 24, 25);
    static Rectangle ls2r = new Rectangle(vrp.X + 220, vrp.Y + 7, 24, 25);

    static Rectangle dcvr = new Rectangle(dccr.Left, dccr.Top + 8, 66, 23);
    static Rectangle ratedvr = new Rectangle(ratedcr.Left, ratedcr.Top + 8, 66, 23);
    static Rectangle tolerancevr = new Rectangle(tolerancecr.Left, tolerancecr.Top + 8, 66, 23);
    static Rectangle currvr = new Rectangle(currcr.Left, currcr.Top + 8, 66, 23);
    static Rectangle errAbsvr = new Rectangle(errAbscr.Left, errAbscr.Top + 8, 66, 23);
    static Rectangle errPercvr = new Rectangle(errPerccr.Left, errPerccr.Top + 8, 66, 23);


    static Rectangle nlr = new Rectangle(dccr.Left, 31, dccr.Width, 12);

    Font captionsFont = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
    Font valuesFont = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));

    StringFormat sf = new StringFormat();

    Brush foreColorBrush = new SolidBrush(Color.LightGray);
    Brush failColorBrush = new SolidBrush(Color.FromArgb(255, 189, 12, 12));
    Brush passColorBrush = new SolidBrush(Color.FromArgb(255, 62, 213, 81));
    Brush nameBrush = new SolidBrush(Color.LightGray);

    Color mBackgroundColor1 = Color.FromArgb(1, 255, 255, 255);
    Color mBackgroundColor2 = Color.FromArgb(5, 255, 255, 255);
    Pen borderColorPen = new Pen(Color.FromArgb(25, 255, 255, 255));
    
    #endregion Private fields



    #region Constructors & finalizer

    public InitialTestDisplay()
    {
      this.SetStyle(
                    ControlStyles.SupportsTransparentBackColor |
                    ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.ResizeRedraw |
                    ControlStyles.UserPaint, true);

      InitializeComponent();
      mChannel = new DisplayChannel();
      sf.Alignment = StringAlignment.Center;
    }

    #endregion Constructors & finalizer



    #region Events



    #endregion Events



    #region Properties

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

    //[XmlIgnoreAttribute]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DisplayChannel Channel
    {
      get { return mChannel; }
      set 
      {
        mChannel = value;
        RefreshValues();
      }
    }

    public bool TestFailed
    {
      get { return !testStatusButton.Enabled; }
    }

    #endregion Properties



    #region Methods


    delegate void RefreshValuesCallback();

    private bool ChannelReadValuesOK()
    {
      if (Math.Abs(GetErrorValue()) > (mChannel.NominalCurrentTolerance / 100.0f) * (float)mChannel.NominalCurrent)
      {
        return false;
      }
      else
      {
        return true;
      }
    }

    private float GetErrorValue()
    {
      return mChannel.Value - mChannel.NominalCurrent;
    }

    public void RefreshValues()
    {
      if (InvokeRequired)
      {
        RefreshValuesCallback cb = new RefreshValuesCallback(RefreshValues);
        Invoke(cb, new object[0]);
      }
      else
      {

        if (ChannelReadValuesOK())
        {
          testStatusButton.Text = "Pass";
          testStatusButton.Enabled = true;
        }
        else
        {
          testStatusButton.Text = "Fail";
          testStatusButton.Enabled = false;
        }

        this.Invalidate();
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

      Graphics g = e.Graphics;

      // Draw background
      LinearGradientBrush brushFill;
      brushFill = new LinearGradientBrush(this.ClientRectangle, mBackgroundColor1, mBackgroundColor2, LinearGradientMode.Vertical);
      brushFill.SetSigmaBellShape(0.0F);
      brushFill.WrapMode = WrapMode.TileFlipXY;
      GraphicsPath path = GetRoundedRect(this.ClientRectangle, 4);
      g.FillPath(brushFill, path);
      g.DrawPath(borderColorPen, path);

      // Draw captions
      g.DrawString("Duty cycle[%]", captionsFont, foreColorBrush, dccr, sf);
      g.DrawString("Nominal [A]:", captionsFont, foreColorBrush, ratedcr, sf);
      g.DrawString("Tolerance [%]:", captionsFont, foreColorBrush, tolerancecr, sf);
      g.DrawString("Actual: [A]", captionsFont, foreColorBrush, currcr, sf);
      g.DrawString("Error [A}:", captionsFont, foreColorBrush, errAbscr, sf);
      g.DrawString("Error [%]:", captionsFont, foreColorBrush, errPerccr, sf);
      // Draw values
      g.DrawString(mChannel.InitTestDutyCycle.ToString("0"), valuesFont, foreColorBrush, dcvr, sf);
      float error = GetErrorValue();
      int errorPerc = (int)(error / mChannel.NominalCurrent * 100);
      if (ChannelReadValuesOK())
      {
        // If the values are okay, draw everything in normal fore color
        g.DrawString(mChannel.NominalCurrent.ToString("0.00"), valuesFont, foreColorBrush, ratedvr, sf);
        g.DrawString(mChannel.NominalCurrentTolerance.ToString(), valuesFont, foreColorBrush, tolerancevr, sf);
        g.DrawString(mChannel.Value.ToString("0.00"), valuesFont, passColorBrush, currvr, sf);
        g.DrawString(error.ToString("0.00"), valuesFont, passColorBrush, errAbsvr, sf);
        g.DrawString(errorPerc.ToString("0.00"), valuesFont, passColorBrush, errPercvr, sf);
      }
      else
      {
        // If tolerance was exceeded draw the error label in error color
        g.DrawString(mChannel.NominalCurrent.ToString("0.00"), valuesFont, foreColorBrush, ratedvr, sf);
        g.DrawString(mChannel.NominalCurrentTolerance.ToString(), valuesFont, foreColorBrush, tolerancevr, sf);
        g.DrawString(mChannel.Value.ToString("0.00"), valuesFont, failColorBrush, currvr, sf);
        g.DrawString(error.ToString("0.00"), valuesFont, failColorBrush, errAbsvr, sf);
        g.DrawString((errorPerc).ToString("0"), valuesFont, failColorBrush, errPercvr, sf);
      }
      // Draw channel name
      g.DrawString(mChannel.Label, captionsFont, nameBrush, nlr, sf);
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
      arc.X = baseRect.Right - diameter - 1;
      path.AddArc(arc, 270, 90);
      // bottom right arc 
      arc.Y = baseRect.Bottom - diameter - 1;
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

    #endregion Methods

    private void nominalUp_Click(object sender, EventArgs e)
    {
      mChannel.NominalCurrent += 0.05f;
    }

    private void NominalDown_Click(object sender, EventArgs e)
    {
      mChannel.NominalCurrent -= 0.05f;
    }

    private void ToleranceUp_Click(object sender, EventArgs e)
    {
      mChannel.NominalCurrentTolerance += 1;
      Console.WriteLine("UP");
    }

    private void ToleranceDown_Click(object sender, EventArgs e)
    {
      mChannel.NominalCurrentTolerance -= 1;
      Console.WriteLine("DOWN");
    }
  }
}
