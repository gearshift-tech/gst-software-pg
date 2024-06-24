using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;

namespace Soko.Common.Controls
{
    /// <summary>
    /// Class:              NiceScrollBar
    /// Author:             Pawel Pustelnik
    /// Created:            15.11.2007
    /// Last modification:  20.11.2007
    /// 
    /// Description:
    /// NiceScrollBar is a nice scrollbar
    /// </summary>
     

    public class NiceScrollBar : Control
    {

        private const int MIN_BUTTON_SIZE = 50;
        private const int MIN_UP_DOWN_SIZE = 18;

        private Theme m_ColorStyle = Theme.Black;

        private Image m_ImgUpNormal = Soko.Common.Properties.Resources.NiceScrollbar_up_arrow;
        private Image m_ImgUpOver = Soko.Common.Properties.Resources.NiceScrollbar_up_arrow_over;
        private Image m_ImgUpClicked = Soko.Common.Properties.Resources.NiceScrollbar_up_arrow_clicked;

        private Image m_ImgDownNormal = Soko.Common.Properties.Resources.NiceScrollbar_down_arrow;
        private Image m_ImgDownOver = Soko.Common.Properties.Resources.NiceScrollbar_down_arrow_over;
        private Image m_ImgDownClicked = Soko.Common.Properties.Resources.NiceScrollbar_down_arrow_clicked;

        private Rectangle m_UpButtonRect = new Rectangle(0, 0, 1, 1);
        private Rectangle m_DownButtonRect = new Rectangle(0, 0, 1, 1);
        private Rectangle m_ScrollButtonRect = new Rectangle(0, 0, 1, 1);

        private int m_ScrollButtonLocation = 50;
        private int m_MouseDownStartLocation = 0;

        private bool m_UpButtonClicked = false;
        private bool m_DownButtonClicked = false;
        private bool m_ScrollButtonClicked = false;

        private bool m_UpButtonOver = false;
        private bool m_DownButtonOver = false;
        private bool m_ScrollButtonOver = false;

        private int m_ScrollButtonHeight = MIN_BUTTON_SIZE;

        private Color m_ScrollButtonColor1 = Color.LightGray;//gradient color 1
        private Color m_ScrollButtonColor2 = Color.DarkGray;//gradient color 2
        private Color m_ScrollButtonColor3 = Color.DimGray;//frame of thumb
        private Color m_ScrollButtonColor4 = Color.White;//shadow inside the thumb

        private Color m_TrackColor1 = Color.LightGray;
        private Color m_TrackColor2 = Color.MintCream;

        protected int m_LargeChange = 10;
        protected int m_SmallChange = 1;
        protected int m_Minimum = 0;
        protected int m_Maximum = 100;
        protected int m_Value = 0;

        public event EventHandler Scroll = null;
        public event EventHandler ValueChanged = null;


        /// <summary>
        /// Color style used to draw control
        /// </summary>
        public enum Theme { Blue, Black, Silver, System };


        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Behavior"), Description("LargeChange")]
        public int LargeChange
        {
            get { return m_LargeChange; }
            set
            {
                if (m_LargeChange != value)
                {
                    m_LargeChange = value;
                    UpdateSizes();
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Behavior"), Description("SmallChange")]
        public int SmallChange
        {
            get { return m_SmallChange; }
            set
            {
                if (m_SmallChange != value)
                {
                    m_SmallChange = value;
                    UpdateSizes();
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Behavior"), Description("Minimum")]
        public int Minimum
        {
            get { return m_Minimum; }
            set
            {
                if (m_Minimum != value)
                {
                    m_Minimum = value;
                    UpdateSizes();
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Behavior"), Description("Maximum")]
        public int Maximum
        {
            get { return m_Maximum; }
            set
            {
                if (m_Maximum != value)
                {
                    m_Maximum = value;
                    UpdateSizes();
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Behavior"), Description("Value")]
        public int Value
        {
            get { return m_Value; }
            set
            {
                if (m_Value != value)
                {
                    m_Value = value;

                    if (m_Value > m_Maximum)
                        m_Value = m_Maximum;

                    if (m_Value < m_Minimum)
                        m_Value = m_Minimum;

                    UpdateSizes();
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Appearance"), Description("Scroll Thumb First Color")]
        public Color ThumbColor1
        {
            get { return m_ScrollButtonColor1; }
            set
            {
                m_ScrollButtonColor1 = value;
                Invalidate();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Appearance"), Description("Scroll Thumb Second Color")]
        public Color ThumbColor2
        {
            get { return m_ScrollButtonColor2; }
            set
            {
                m_ScrollButtonColor2 = value;
                Invalidate();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Appearance"), Description("Scroll Thumb Shadow Color")]
        public Color ThumbColor3
        {
            get { return m_ScrollButtonColor4; }
            set
            {
                m_ScrollButtonColor4 = value;
                Invalidate();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Appearance"), Description("Scroll Thumb Frame Color")]
        public Color ThumbFrameColor
        {
            get { return m_ScrollButtonColor3; }
            set
            {
                m_ScrollButtonColor3 = value;
                Invalidate();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Appearance"), Description("Background Track First Color")]
        public Color TrackColor1
        {
            get { return m_TrackColor1; }
            set
            {
                m_TrackColor1 = value;
                Invalidate();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Appearance"), Description("Background Track Second Color")]
        public Color TrackColor2
        {
            get { return m_TrackColor2; }
            set
            {
                m_TrackColor2 = value;
                Invalidate();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Appearance"), Description("Color Style")]
        public Theme ColorScheme
        {
            get
            {
                return m_ColorStyle;
            }
            set
            {
                if (m_ColorStyle != value)
                {
                    m_ColorStyle = value;
                    if (m_ColorStyle == Theme.System)
                    {
                        m_ScrollButtonColor1 = Color.LightGray;//gradient color 1
                        m_ScrollButtonColor2 = Color.DarkGray;//gradient color 2
                        m_ScrollButtonColor3 = Color.DimGray;//frame of thumb
                        m_ScrollButtonColor4 = Color.White;//shadow inside the thumb
                        m_TrackColor1 = Color.LightGray;
                        m_TrackColor2 = Color.MintCream;
                        m_ImgUpNormal = Soko.Common.Properties.Resources.NiceScrollbar_up_arrow;
                        m_ImgUpOver = Soko.Common.Properties.Resources.NiceScrollbar_up_arrow_over;
                        m_ImgUpClicked = Soko.Common.Properties.Resources.NiceScrollbar_up_arrow_clicked;

                        m_ImgDownNormal = Soko.Common.Properties.Resources.NiceScrollbar_down_arrow;
                        m_ImgDownOver = Soko.Common.Properties.Resources.NiceScrollbar_down_arrow_over;
                        m_ImgDownClicked = Soko.Common.Properties.Resources.NiceScrollbar_down_arrow_clicked;
                    }
                    else if (m_ColorStyle == Theme.Blue)
                    {
                        m_ScrollButtonColor1 = Color.DodgerBlue;//gradient color 1
                        m_ScrollButtonColor2 = Color.LightSkyBlue;//gradient color 2
                        m_ScrollButtonColor3 = Color.MediumBlue;//frame of thumb
                        m_ScrollButtonColor4 = Color.LightBlue;//shadow inside the thumb
                        m_TrackColor1 = Color.PaleTurquoise;
                        m_TrackColor2 = Color.MintCream;

                        m_ImgUpNormal = Soko.Common.Properties.Resources.NiceScrollbar_up_arrow_BLUE;
                        m_ImgUpOver = Soko.Common.Properties.Resources.NiceScrollbar_up_arrow_over_BLUE;
                        m_ImgUpClicked = Soko.Common.Properties.Resources.NiceScrollbar_up_arrow_clicked_BLUE;

                        m_ImgDownNormal = Soko.Common.Properties.Resources.NiceScrollbar_down_arrow_BLUE;
                        m_ImgDownOver = Soko.Common.Properties.Resources.NiceScrollbar_down_arrow_over_BLUE;
                        m_ImgDownClicked = Soko.Common.Properties.Resources.NiceScrollbar_down_arrow_clicked_BLUE;
                    }
                    else if (m_ColorStyle == Theme.Black)
                    {
                        m_ScrollButtonColor1 = Color.DimGray;//gradient color 1
                        m_ScrollButtonColor2 = Color.Black;//gradient color 2
                        m_ScrollButtonColor3 = Color.Gray;//frame of thumb
                        m_ScrollButtonColor4 = Color.White;//shadow inside the thumb
                        m_TrackColor1 = Color.DimGray;
                        m_TrackColor2 = Color.Black;

                        m_ImgUpNormal = Soko.Common.Properties.Resources.NiceScrollbar_up_arrow_BLACK;
                        m_ImgUpOver = Soko.Common.Properties.Resources.NiceScrollbar_up_arrow_over_BLACK;
                        m_ImgUpClicked = Soko.Common.Properties.Resources.NiceScrollbar_up_arrow_clicked_BLACK;

                        m_ImgDownNormal = Soko.Common.Properties.Resources.NiceScrollbar_down_arrow_BLACK;
                        m_ImgDownOver = Soko.Common.Properties.Resources.NiceScrollbar_down_arrow_over_BLACK;
                        m_ImgDownClicked = Soko.Common.Properties.Resources.NiceScrollbar_down_arrow_clicked_BLACK;
                    }
                    else if (m_ColorStyle == Theme.Silver)
                    {
                        m_ScrollButtonColor1 = Color.LightGray;//gradient color 1
                        m_ScrollButtonColor2 = Color.DarkGray;//gradient color 2
                        m_ScrollButtonColor3 = Color.DimGray;//frame of thumb
                        m_ScrollButtonColor4 = Color.Gray;//shadow inside the thumb
                        m_TrackColor1 = Color.LightGray;
                        m_TrackColor2 = Color.MintCream;

                        m_ImgUpNormal = Soko.Common.Properties.Resources.NiceScrollbar_up_arrow_SILVER;
                        m_ImgUpOver = Soko.Common.Properties.Resources.NiceScrollbar_up_arrow_over_SILVER;
                        m_ImgUpClicked = Soko.Common.Properties.Resources.NiceScrollbar_up_arrow_clicked_SILVER;

                        m_ImgDownNormal = Soko.Common.Properties.Resources.NiceScrollbar_down_arrow_SILVER;
                        m_ImgDownOver = Soko.Common.Properties.Resources.NiceScrollbar_down_arrow_over_SILVER;
                        m_ImgDownClicked = Soko.Common.Properties.Resources.NiceScrollbar_down_arrow_clicked_SILVER;
                    }

                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Main Constructor
        /// </summary>
        public NiceScrollBar()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Updates Thumb sizes
        /// </summary>
        private void UpdateSizes()
        {
            int nTrackHeight = (this.Height - 2 * MIN_UP_DOWN_SIZE);
            float fThumbHeight = ((float)LargeChange / (float)Maximum) * nTrackHeight;
            int nThumbHeight = (int)fThumbHeight;

            if (nThumbHeight > nTrackHeight)
            {
                nThumbHeight = nTrackHeight;
                fThumbHeight = nTrackHeight;
            }
            if (nThumbHeight < MIN_BUTTON_SIZE)
            {
                nThumbHeight = MIN_BUTTON_SIZE;
                fThumbHeight = MIN_BUTTON_SIZE;
            }

            m_ScrollButtonHeight = nThumbHeight;

            //calculate new scroll button size value
            int nPixelRange = nTrackHeight - nThumbHeight;
            int nRealRange = (Maximum - Minimum);// -LargeChange;
            float fPerc = 0.0f;
            if (nRealRange != 0)
            {
                fPerc = (float)m_Value / (float)nRealRange;
            }

            float fTop = fPerc * nPixelRange + MIN_UP_DOWN_SIZE - 1;
            m_ScrollButtonLocation = (int)fTop;

            //make sure it is in the range
            CheckScrollButtonLocation();

            Invalidate();
        }


        /// <summary>
        /// Draws scroll bar background area
        /// </summary>
        /// <param name="g"></param>
        private void DrawScrollBgnd(Graphics g)
        {
            //calculate sizes
            int nH = this.Height;
            int nW = this.Width;

            //draw background track
            Rectangle trackRect = new Rectangle(0, 0, nW - 1, nH - 1);
            LinearGradientBrush br = new LinearGradientBrush(trackRect, m_TrackColor2, m_TrackColor1, 0, false);
            br.SetBlendTriangularShape(0.5f, 1.0f);
            g.FillRectangle(br, trackRect);
            br.Dispose();

            Pen trackPen = new Pen(m_ScrollButtonColor3, 1);
            g.DrawRectangle(trackPen, trackRect);
            trackPen.Dispose();


            /*Image img = Soko.Common.Properties.Resources.NiceScrollbar_scroll_bgnd;
           int step = 2;
           for (int j = 0; j < nH; j += step)
               g.DrawImage(img, new Rectangle(0, j,  nW, 2));*/
        }

        /// <summary>
        /// Draws scroll bar arrows
        /// </summary>
        /// <param name="g"></param>
        private void DrawScrollArrows(Graphics g)
        {
            //calculate sizes
            int nH = this.Height;
            int nW = this.Width;

            //draw up button arrow
            Image img = m_ImgUpNormal;
            if (m_UpButtonClicked)
                img = m_ImgUpClicked;
            else if (m_UpButtonOver)
                img = m_ImgUpOver;

            g.DrawImage(img, new Rectangle(-1, -1, nW + 2, img.Height + 2));

            //draw down button arrow
            img = m_ImgDownNormal;
            if (m_DownButtonClicked)
                img = m_ImgDownClicked;
            else if (m_DownButtonOver)
                img = m_ImgDownOver;

            g.DrawImage(img, new Rectangle(-1, nH - img.Height - 1, nW + 2, img.Height + 2));
        }


        private void DrawScrollThumb(Graphics g)
        {
            //calculate sizes
            int nH = this.Height;
            int nW = this.Width;

            Point[] points = new Point[8];
            points[0] = new Point(m_ScrollButtonRect.X, m_ScrollButtonRect.Y + 2);
            points[1] = new Point(m_ScrollButtonRect.X + 2, m_ScrollButtonRect.Y);
            points[2] = new Point(m_ScrollButtonRect.Right - 2, m_ScrollButtonRect.Y);
            points[3] = new Point(m_ScrollButtonRect.Right, m_ScrollButtonRect.Y + 2);
            points[4] = new Point(m_ScrollButtonRect.Right, m_ScrollButtonRect.Bottom - 2);
            points[5] = new Point(m_ScrollButtonRect.Right - 2, m_ScrollButtonRect.Bottom);
            points[6] = new Point(m_ScrollButtonRect.X + 2, m_ScrollButtonRect.Bottom);
            points[7] = new Point(m_ScrollButtonRect.X , m_ScrollButtonRect.Bottom - 2);

            //// DRAWING SCROLL THUMB ////////////
            g.SmoothingMode = SmoothingMode.HighQuality;

            LinearGradientBrush brush = null;

            if (m_ScrollButtonOver && !m_ScrollButtonClicked)
            {
                brush = new LinearGradientBrush(m_ScrollButtonRect,
                     m_ScrollButtonColor1, m_ScrollButtonColor2, 0, false);
            }
            else
            {
                brush = new LinearGradientBrush(m_ScrollButtonRect,
                     m_ScrollButtonColor2, m_ScrollButtonColor1, 0, false);
            }

            brush.SetBlendTriangularShape(0.5f, 1.0f);
            g.FillPolygon(brush, points, FillMode.Winding);
            //g.FillRectangle(brush, m_ScrollButtonRect);
            brush.Dispose();

      Pen thumbPen = new Pen(m_ScrollButtonColor3, 1)
      {
        EndCap = LineCap.Round,
        DashCap = DashCap.Round
      };
      g.DrawPolygon(thumbPen, points);
            thumbPen.Dispose();

            g.SmoothingMode = SmoothingMode.Default;

            Color addColor = Color.Orange;

            Pen framePen = new Pen(addColor, 1);


            /////// DRAWING SHADOWS INSIDE THUMB ///////
            int diff = 3;
            int y = m_ScrollButtonRect.Y + m_ScrollButtonRect.Height / 2 - 3;

            if (m_ScrollButtonClicked)
                g.TranslateTransform(0, 1);

            for (int i = 0; i < 4; ++i)
            {
                Pen penLight = new Pen(m_ScrollButtonColor4, 1);
                Pen penDark = new Pen(m_ScrollButtonColor3, 1);

                //g.DrawRectangle(penLight, 4, y + i * diff, 1, 1);
                //g.DrawRectangle(penDark, 5, y + i * diff - 1, 1, 1);

                //g.DrawRectangle(penLight, nW - 6, y + i * diff, 1, 1);
                //g.DrawRectangle(penDark, nW - 5, y + i * diff - 1, 1, 1);

                g.DrawLine(penLight, new Point(7, y + i * diff),
                           new Point(nW - 8, y + i * diff));

                g.DrawLine(penDark, new Point(7, y + i * diff + 1),
                           new Point(nW - 8, y + i * diff + 1));

                penDark.Dispose();
                penLight.Dispose();
            }


            if (m_ScrollButtonClicked)
                g.TranslateTransform(0, -1);

            framePen.Dispose();
        }

        /// <summary>
        /// Overriding for Paint 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //calculate sizes
            int nH = this.Height;
            int nW = this.Width;

            //background
            DrawScrollBgnd(g);

            //arrows
            DrawScrollArrows(g);

            //thumb
            DrawScrollThumb(g);
        }

        /// <summary>
        /// Checks Thumb Location and corrects it if necessary
        /// </summary>
        private void CheckScrollButtonLocation()
        {
            if (m_ScrollButtonLocation < MIN_UP_DOWN_SIZE)
                m_ScrollButtonLocation = MIN_UP_DOWN_SIZE;

            if ((m_ScrollButtonLocation + m_ScrollButtonHeight) > this.Height - MIN_UP_DOWN_SIZE)
                m_ScrollButtonLocation = this.Height - MIN_UP_DOWN_SIZE - m_ScrollButtonHeight - 1;

            m_ScrollButtonRect = new Rectangle(0, m_ScrollButtonLocation, this.Width-1, m_ScrollButtonHeight);
        }

        /// <summary>
        /// Overriding for Size
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            m_UpButtonRect = new Rectangle(0, 0, this.Width - 1, MIN_UP_DOWN_SIZE);

            m_DownButtonRect = new Rectangle(0, this.Height - MIN_UP_DOWN_SIZE, 
                                            this.Width - 1, MIN_UP_DOWN_SIZE);

            CheckScrollButtonLocation();

            base.OnSizeChanged(e);
        }

        /// <summary>
        /// Overriding for Mouse Move
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            m_DownButtonOver = false;
            m_UpButtonOver = false;
            m_ScrollButtonOver = false;

            if (m_ScrollButtonClicked)
            {
                m_ScrollButtonLocation = m_ScrollButtonLocation - (m_MouseDownStartLocation - e.Location.Y);
                CheckScrollButtonLocation();
                Invalidate();
                m_MouseDownStartLocation = e.Location.Y;

                //real height in which scroll button can be moved
                int nH = this.Height - 2 * MIN_UP_DOWN_SIZE - m_ScrollButtonRect.Height - 1;
                //real scroll button position in nH
                int nY = m_ScrollButtonLocation - MIN_UP_DOWN_SIZE;

                m_Value = (int)((1.0F * nY / nH) * (m_Maximum ));

        ValueChanged?.Invoke(this, new EventArgs());

        Scroll?.Invoke(this, new EventArgs());

        return;
            }

            if (m_UpButtonRect.Contains(e.Location))
            {
                m_UpButtonOver = true;
                Invalidate();
                return;
            }

            if (m_DownButtonRect.Contains(e.Location))
            {
                m_DownButtonOver = true;
                Invalidate();
                return;
            }

            if (m_ScrollButtonRect.Contains(e.Location))
            {
                m_ScrollButtonOver = true;
                Invalidate();
                return;
            }

            Invalidate();
            base.OnMouseMove(e);
        }

        /// <summary>
        /// Overriding for Mouse Up
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            m_DownButtonClicked = false;
            m_UpButtonClicked = false;
            m_ScrollButtonClicked = false;
            m_ScrollButtonClicked = false;

            Invalidate();

            base.OnMouseUp(e);
        }

        /// <summary>
        /// Overriding for Mouse Down
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {

            //check for up button clicked
            if (m_UpButtonRect.Contains(e.Location))
            {
                m_UpButtonClicked = true;
                Invalidate(m_UpButtonRect);

                this.Value = m_Value - m_SmallChange;
                CheckScrollButtonLocation();

        ValueChanged?.Invoke(this, new EventArgs());

        Scroll?.Invoke(this, new EventArgs());

        return;
            }

            //check for down button clicked
            if (m_DownButtonRect.Contains(e.Location))
            {
                m_DownButtonClicked = true;
                Invalidate(m_DownButtonRect);

                this.Value = m_Value + m_SmallChange;
                CheckScrollButtonLocation();

        ValueChanged?.Invoke(this, new EventArgs());

        Scroll?.Invoke(this, new EventArgs());

        return;
            }

            //check for scroll button clicked
            if (m_ScrollButtonRect.Contains(e.Location))
            {
                m_MouseDownStartLocation = e.Location.Y;
                m_ScrollButtonClicked = true;
                Invalidate(m_ScrollButtonRect);
                return;
            }
        }

        /// <summary>
        /// Overriding for Mouse Enter
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(EventArgs e)
        {
           
            base.OnMouseEnter(e);
        }

        /// <summary>
        /// Overriding for Mouse Leave
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            m_DownButtonOver = false;
            m_UpButtonOver = false;
            m_ScrollButtonOver = false;
            m_DownButtonClicked = false;
            m_UpButtonClicked = false;
            m_ScrollButtonClicked = false;
            m_ScrollButtonClicked = false;
            
            Invalidate();

            base.OnMouseLeave(e);
        }

        /// <summary>
        /// Initialized the control
        /// </summary>
        private void InitializeComponent()
        {
            this.Width = 16;
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            m_ScrollButtonLocation = 0;
            CheckScrollButtonLocation();

            m_ScrollButtonHeight = MIN_BUTTON_SIZE;
        }

    }

    
}
