using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;



namespace Soko.Common.Controls
{
    /// <summary>
    /// Class:              ExplorerBarPane
    /// Author:             Pawel Pustelnik
    /// Created:            25.09.2007
    /// Last modification:  26.11.2007
    /// 
    /// Description:
    /// ExplorerBarPane is a control like Navisight Explorer Bar Pane.
    /// </summary>
    public partial class ExplorerBarPane : ContainerControl
    {

        public enum Theme { Blue, Black, Silver, System };

        const int HEADER_SIZE = 22;
        const int MARGIN = 1;
        const int SPACE = 10;
        const int PEN_WIDTH = 1;

        private Color m_HeaderColor1 = System.Drawing.Color.CornflowerBlue;
        private Color m_HeaderColor2 = System.Drawing.Color.CornflowerBlue;
        private Color m_HeaderColor3 = System.Drawing.Color.Black;
        private Color m_HeaderColor4 = System.Drawing.Color.Black;
        private Color m_BgndColor1 = System.Drawing.Color.Lavender;
        private Color m_BgndColor2 = System.Drawing.Color.CornflowerBlue;

        private StringFormat m_Format = new StringFormat();

        //control is in collapse mode or not
        private bool m_Collapsed = false;

        //mouse is over collapse button
        private bool m_OverHideButton = false;

        private int m_MinHeight = 10;
        private int m_NormalHeight = 200;

        private bool m_IsHiding = false;

        private int m_CollapseStep = 5;

        private Timer m_CollapseTimer = new Timer();


        private PointF[] m_FramePoints = new PointF[23];
        private PointF[] m_HeaderPoints = new PointF[15];
        private PointF[] m_RegionPoints = new PointF[20];

        private Rectangle m_CollapseRect = new Rectangle(0, 0, 1, 1);

        private Theme m_Theme = Theme.Black;
        private bool m_ThemesEnabled = false;
        private bool mAllowUserInteraction = true;

        private bool mHideAfterMouseLeave = false;
        private int mHideAfterMouseLeaveDelayMs = 2000;
        private Timer mHideAfterMouseLeaveTimer = new Timer();
        


        #region PUBLIC PROPERTIES


        public bool Collapsed
        {
            get
            {
                return m_Collapsed;
            }
            set
            {
                m_Collapsed = value;
                if ( m_Collapsed )
                {
                  NormalHeight = this.Height;
                  this.Height = m_MinHeight;
                }
                else
                  this.Height = m_NormalHeight;
                Invalidate();
            }
        }

        [System.ComponentModel.Category( "Misc" )]
        public int NormalHeight
        {
          get { return m_NormalHeight; }
          set
          {
            if (value >= m_MinHeight)
            {
              m_NormalHeight = value;
            }
          }
        }

        [System.ComponentModel.Category( "Misc" )]
        public int CollapsedHeight
        {
          get { return m_MinHeight; }
        }

        /// <summary>
        /// Theme - Color Scheme
        /// </summary>
        public Theme ColorScheme
        {
            get
            {
                return m_Theme;
            }
            set
            {
              if ( ColorSchemesEnabled )
              {
                m_Theme = value;
                UpdateTheme();
              }
            }
        }

        /// <summary>
        /// First header component color
        /// </summary>
        public Color HeaderColor1
        {
            get
            {
                return m_HeaderColor1;
            }
            set
            {
                m_HeaderColor1 = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Second header component color
        /// </summary>
        public Color HeaderColor2
        {
            get
            {
                return m_HeaderColor2;
            }
            set
            {
                m_HeaderColor2 = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Third header component color
        /// </summary>
        public Color HeaderColor3
        {
            get
            {
                return m_HeaderColor3;
            }
            set
            {
                m_HeaderColor3 = value;
                Invalidate();
            }
        }

        /// <summary>
        /// First background color
        /// </summary>
        public Color BgndColor1
        {
            get
            {
                return m_BgndColor1;
            }
            set
            {
                m_BgndColor1 = value;
                Invalidate();
            }
        }

        /// <summary>
        /// First background color
        /// </summary>
        public Color BgndColor2
        {
            get
            {
                return m_BgndColor2;
            }
            set
            {
                m_BgndColor2 = value;
                Invalidate();
            }
        }

        public bool ColorSchemesEnabled
        {
          get { return m_ThemesEnabled; }
          set { m_ThemesEnabled = value; }
        }

        public bool HideAfterMouseLeave
        {
          get { return mHideAfterMouseLeave; }
          set
          {
            mHideAfterMouseLeave = value;
          }
        }

        public int HideAfterMouseLeaveDelayMs
        {
          get { return mHideAfterMouseLeaveDelayMs; }
          set
          {
            mHideAfterMouseLeaveDelayMs = value;
            mHideAfterMouseLeaveTimer.Interval = value;
          }
        }

        #endregion



        #region CONSTRUCTOR

        /// <summary>
        /// Main Constructor
        /// </summary>
        public ExplorerBarPane()
        {
            InitializeComponent();

            SetStyle(ControlStyles.ContainerControl, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);


            m_CollapseTimer.Tick += new EventHandler(CollapseTimerTick);
            m_CollapseTimer.Interval = 30;

            mHideAfterMouseLeaveTimer.Tick += new EventHandler(HideAfterMouseLeaveTimerTick);
            mHideAfterMouseLeaveTimer.Interval = 1000;


            //this.Padding = new Padding(10, HEADER_SIZE + 12, 10, 20);

            m_Format.Alignment = StringAlignment.Center;
            m_Format.LineAlignment = StringAlignment.Center;
            m_Format.FormatFlags = StringFormatFlags.LineLimit;

            this.Text = "Pane Title";

            m_MinHeight = HEADER_SIZE + 1;
        }

        #endregion


        public event EventHandler UserClickedPaneRestore;

        public event EventHandler UserClickedPaneCollapse;

        #region PRIVATE METHODS


        /// <summary>
        /// Updates colors to actually selected theme
        /// </summary>
        private void UpdateTheme()
        {
            if (m_Theme == Theme.System)
            {
                m_BgndColor1 = GetModifiedColor(SystemColors.InactiveCaption, 30, 0);
                m_BgndColor2 = GetModifiedColor(SystemColors.InactiveCaption, 10, 0);
                this.ForeColor = SystemColors.ControlText;
                m_HeaderColor1 = SystemColors.GradientActiveCaption;
                m_HeaderColor2 = System.Drawing.Color.LightBlue;
                m_HeaderColor3 = SystemColors.ControlText;
                m_HeaderColor4 = SystemColors.ControlText;
            }
            else if (m_Theme == Theme.Blue)
            {
                m_BgndColor1 = System.Drawing.Color.LightSteelBlue; 
                m_BgndColor2 = System.Drawing.Color.CornflowerBlue;
                this.ForeColor = System.Drawing.Color.White;
                m_HeaderColor1 = System.Drawing.Color.CornflowerBlue;
                m_HeaderColor2 = System.Drawing.Color.LightBlue;
                m_HeaderColor3 = System.Drawing.Color.Transparent;
                m_HeaderColor4 = System.Drawing.Color.Black;
            }
            else if (m_Theme == Theme.Black)
            {
                m_BgndColor1 = System.Drawing.Color.Silver;
                m_BgndColor2 = System.Drawing.Color.Gray;
                this.ForeColor = System.Drawing.Color.White;
                m_HeaderColor1 = System.Drawing.Color.Gray;
                m_HeaderColor2 = System.Drawing.Color.LightBlue;
                m_HeaderColor3 = System.Drawing.Color.Transparent;
                m_HeaderColor4 = System.Drawing.Color.Black;
            }
            else if (m_Theme == Theme.Silver)
            {
                m_BgndColor1 = System.Drawing.Color.Gainsboro;
                m_BgndColor2 = System.Drawing.Color.Gray;
                this.ForeColor = System.Drawing.Color.Black;
                m_HeaderColor1 = System.Drawing.Color.LightGray;
                m_HeaderColor2 = System.Drawing.Color.LightBlue;
                m_HeaderColor3 = System.Drawing.Color.Transparent;
                m_HeaderColor4 = System.Drawing.Color.Black;
            }

            Invalidate();
        }

        /// <summary>
        /// Overriding CreateControl Method
        /// </summary>
        protected override void OnCreateControl()
        {
            //this.Padding = new Padding(10, HEADER_SIZE + 12, 10, 20);
        }

        /// <summary>
        /// Overriding Paint Method
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            PaintOn(e.Graphics, e);
        }

        /// <summary>
        /// Overriding Text Changed Method
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTextChanged(EventArgs e)
        {
            Invalidate(new Rectangle(0, 1, this.Width - 1, HEADER_SIZE - 2));
            base.OnTextChanged(e);
        }

        /// <summary>
        /// Occurs when mouse is down
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && AllowUserInteraction)
            {
              if (m_Collapsed)
              {
          UserClickedPaneRestore?.Invoke(this, EventArgs.Empty);
        }
              else
              {
          UserClickedPaneCollapse?.Invoke(this, EventArgs.Empty);
        }
              StartCollapseProcess(e.Location);
            }
            base.OnMouseDown(e);
        }

        /// <summary>
        /// Occurs when mouse is up
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }

        /// <summary>
        /// Occurs when mouse moves
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if ( m_CollapseRect.Contains(e.Location))
            {
                if (!m_OverHideButton)
                {
                    m_OverHideButton = true;
                    this.Cursor = Cursors.Hand;
                    Invalidate(m_CollapseRect);
                }
            }
            else
            {
                if (m_OverHideButton)
                {
                    m_OverHideButton = false;
                    this.Cursor = Cursors.Default;
                    Invalidate(m_CollapseRect);
                }
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
          mHideAfterMouseLeaveTimer.Enabled = false;
          base.OnMouseEnter(e);
        }

        /// <summary>
        /// Occures whem mouse leaves
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            if (m_OverHideButton)
            {
                m_OverHideButton = false;
                this.Cursor = Cursors.Default;
                Invalidate();
            }
            if (mHideAfterMouseLeave)
            {
              mHideAfterMouseLeaveTimer.Enabled = true;
            }
            base.OnMouseLeave(e);
        }

        /// <summary>
        /// Returns value in byte range
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private int AbsToColor(int value)
        {
            if (value < 0)
                return 0;
            if (value > 255)
                return 255;
            else
                return value;
        }

        /// <summary>
        /// Returns color modified by adding light and transparency
        /// </summary>
        /// <param name="color"></param>
        /// <param name="light"></param>
        /// <param name="transperency"></param>
        /// <returns></returns>
        private Color GetModifiedColor(Color color, int light, int transperency)
        {
            Color colorOut = Color.FromArgb(AbsToColor(color.A + transperency),
                                       AbsToColor(color.R + light),
                                       AbsToColor(color.G + light),
                                       AbsToColor(color.B + light));

            return colorOut;
        }

        /// <summary>
        /// Paints on graphic object
        /// </summary>
        /// <param name="g"></param>
        /// <param name="e"></param>
        private void PaintOn(Graphics g, PaintEventArgs e)
        {
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            //g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;

            Rectangle bounds = new Rectangle(0, 0, this.Width - 1, this.Height - 1);

            Pen framePen = new Pen(m_HeaderColor3, PEN_WIDTH);
            Pen headerPen = new Pen(m_HeaderColor3, 1);

            if (!m_Collapsed || m_CollapseTimer.Enabled)
            {
                //fill bgnd
                LinearGradientBrush brush = new LinearGradientBrush(bounds, m_BgndColor1, m_BgndColor2, LinearGradientMode.Vertical);

                //main control's area
                g.FillPolygon(brush, m_FramePoints, FillMode.Alternate);
                brush.Dispose();

                //main control's frame
                framePen.DashCap = DashCap.Round;
                framePen.DashStyle = DashStyle.Solid;
                framePen.EndCap = LineCap.Round;

                g.DrawPolygon(headerPen, m_FramePoints);

            }

            //HEADER
            Color color1 = m_HeaderColor1;
            Color color3 = m_HeaderColor3;
            Color color4 = m_HeaderColor4;

            if (m_OverHideButton)
            {
                color1 = GetModifiedColor(color1, 30, 0);
                color3 = GetModifiedColor(color3, 30, 0);
            }

            Color color2 = GetModifiedColor(color1, -20, 0);

            SolidBrush brushHeader1 = new SolidBrush(color1);
            SolidBrush brushHeader2 = new SolidBrush(color2);

            g.FillPolygon(brushHeader1, m_HeaderPoints);//top of header
           
            g.DrawPolygon(headerPen, m_HeaderPoints);
            
            g.FillRectangle(brushHeader2, new RectangleF(new PointF(m_HeaderPoints[0].X+1, m_HeaderPoints[0].Y-HEADER_SIZE/2), 
                                          new SizeF(this.Width - 2 * SPACE - 5, HEADER_SIZE/2-2)));//bottom of header


          


            //header title
            int nW = this.Width - 1;
            g.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor),
                         new RectangleF(m_HeaderPoints[0].X + 1, m_HeaderPoints[3].Y + 1, nW - 2 * SPACE - 4, HEADER_SIZE - 2), m_Format);




            //collapse button

            g.SmoothingMode = SmoothingMode.AntiAlias;

            g.TranslateTransform(-15, 1);

            Pen collapsePen = new Pen(color4, 1);

            g.DrawEllipse(collapsePen, nW - 18, 3, 15, 15);
            g.FillEllipse(brushHeader1, new Rectangle(nW - 17, 4, 14, 14));

    
            if (!m_Collapsed)
            {
                g.DrawLine(collapsePen, nW - 10, 11, nW - 13, 14);
                g.DrawLine(collapsePen, nW - 10, 11, nW - 7, 14);
                g.DrawLine(collapsePen, nW - 10, 12, nW - 12, 14);
                g.DrawLine(collapsePen, nW - 10, 12, nW - 8, 14);
                g.DrawLine(collapsePen, nW - 10, 7, nW - 13, 10);
                g.DrawLine(collapsePen, nW - 10, 7, nW - 7, 10);
                g.DrawLine(collapsePen, nW - 10, 8, nW - 12, 10);
                g.DrawLine(collapsePen, nW - 10, 8, nW - 8, 10);
            }
            else
            {
                g.DrawLine(collapsePen, nW - 10, 15, nW - 13, 12);
                g.DrawLine(collapsePen, nW - 10, 15, nW - 7, 12);
                g.DrawLine(collapsePen, nW - 10, 14, nW - 12, 12);
                g.DrawLine(collapsePen, nW - 10, 14, nW - 8, 12);
                g.DrawLine(collapsePen, nW - 10, 11, nW - 13, 8);
                g.DrawLine(collapsePen, nW - 10, 11, nW - 7, 8);
                g.DrawLine(collapsePen, nW - 10, 10, nW - 12, 8);
                g.DrawLine(collapsePen, nW - 10, 10, nW - 8, 8);
            }

            brushHeader1.Dispose();
            brushHeader2.Dispose();
            framePen.Dispose();
            collapsePen.Dispose();
            headerPen.Dispose();

            g.TranslateTransform(15, -1);

        }

        /// <summary>
        /// Occurs on size changed
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            //if (!m_CollapseTimer.Enabled && !m_Collapsed)
                 //m_NormalHeight = this.Height;

            Rectangle bounds = new Rectangle(1, HEADER_SIZE + 1,
                                             this.Width - 2, this.Height - 3 - HEADER_SIZE - MARGIN);

            Rectangle bounds2 = new Rectangle(SPACE, 1, this.Width - 2 * SPACE - 2,
                                              HEADER_SIZE );

            m_CollapseRect = new Rectangle(SPACE, 1, this.Width - 2 * SPACE - 1,
                                              HEADER_SIZE + 1);


            m_HeaderPoints[0] = new PointF(bounds2.Left + 0, bounds2.Bottom + 0);
            m_HeaderPoints[1] = new PointF(bounds2.Left + 0, bounds2.Top + 3);
            m_HeaderPoints[2] = new PointF(bounds2.Left + 0.5F, bounds2.Top + 2);
            m_HeaderPoints[3] = new PointF(bounds2.Left + 1, bounds2.Top + 1);
            m_HeaderPoints[4] = new PointF(bounds2.Left + 2, bounds2.Top + 0.5F);
            m_HeaderPoints[5] = new PointF(bounds2.Left + 3, bounds2.Top + 0);
            m_HeaderPoints[6] = new PointF(bounds2.Left + 4, bounds2.Top);
            m_HeaderPoints[7] = new PointF(bounds2.Right - 4, bounds2.Top + 0);
            m_HeaderPoints[8] = new PointF(bounds2.Right - 3, bounds2.Top + 0.5F);
            m_HeaderPoints[9] = new PointF(bounds2.Right - 2, bounds2.Top + 1);
            m_HeaderPoints[10] = new PointF(bounds2.Right - 1.5F, bounds2.Top + 2);
            m_HeaderPoints[11] = new PointF(bounds2.Right - 1, bounds2.Top + 3);
            m_HeaderPoints[12] = new PointF(bounds2.Right - 0, bounds2.Top + 4);
            m_HeaderPoints[13] = new PointF(bounds2.Right - 0, bounds2.Top + 4);
            m_HeaderPoints[14] = new PointF(bounds2.Right - 0, bounds2.Bottom + 0);


            //left top corner
            m_FramePoints[0] = new PointF(bounds.Left + 0, bounds.Top + 3);
            m_FramePoints[1] = new PointF(bounds.Left + 0.5F, bounds.Top + 2);
            m_FramePoints[2] = new PointF(bounds.Left + 1, bounds.Top + 1);
            m_FramePoints[3] = new PointF(bounds.Left + 2, bounds.Top + 0.5F);
            m_FramePoints[4] = new PointF(bounds.Left + 3, bounds.Top + 0);


            //right top corner
            m_FramePoints[5] = new PointF(bounds.Right - 4, bounds.Top + 0);
            m_FramePoints[6] = new PointF(bounds.Right - 3, bounds.Top + 0.5F);
            m_FramePoints[7] = new PointF(bounds.Right - 2, bounds.Top + 1);
            m_FramePoints[8] = new PointF(bounds.Right - 1.5F, bounds.Top + 2);
            m_FramePoints[9] = new PointF(bounds.Right - 1, bounds.Top + 3);
            m_FramePoints[10] = new PointF(bounds.Right - 0, bounds.Top + 4);


            //right bottom corner
            m_FramePoints[11] = new PointF(bounds.Right - 0, bounds.Bottom - 4);
            m_FramePoints[12] = new PointF(bounds.Right - 0F, bounds.Bottom - 3);
            m_FramePoints[13] = new PointF(bounds.Right - 0.5F, bounds.Bottom - 3);
            m_FramePoints[14] = new PointF(bounds.Right - 1.5F, bounds.Bottom - 2);
            m_FramePoints[15] = new PointF(bounds.Right - 2, bounds.Bottom - 1);
            m_FramePoints[16] = new PointF(bounds.Right - 3, bounds.Bottom - 0.5F);
            m_FramePoints[17] = new PointF(bounds.Right - 4, bounds.Bottom - 0);

            //left bottom corner
            m_FramePoints[18] = new PointF(bounds.Left + 3, bounds.Bottom);
            m_FramePoints[19] = new PointF(bounds.Left + 2, bounds.Bottom - 0.5F);
            m_FramePoints[20] = new PointF(bounds.Left + 1, bounds.Bottom - 1);
            m_FramePoints[21] = new PointF(bounds.Left + 0.5F, bounds.Bottom - 2);
            m_FramePoints[22] = new PointF(bounds.Left + 0, bounds.Bottom - 3);


            Invalidate();
            base.OnSizeChanged(e);

        }


        /// <summary>
        /// Starts Collapsing process
        /// </summary>
        /// <param name="point"></param>
        private void StartCollapseProcess(Point point)
        {
            if (m_CollapseRect.Contains(point))
            {
                m_CollapseStep = NormalHeight / 10;
                m_IsHiding = m_Collapsed;
                m_CollapseTimer.Enabled = true;
            }
        }

        /// <summary>
        /// Occurs when collapsing timer tick gets event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CollapseTimerTick(object sender, EventArgs e)
        {
            if (m_IsHiding)
            {
                if ((this.Height + m_CollapseStep) < m_NormalHeight)
                {
                    this.Height += m_CollapseStep;
                    Invalidate();
                }
                else
                {
                    this.Height = m_NormalHeight;
                    m_Collapsed = false;
                    Invalidate();
                    m_CollapseTimer.Enabled = false;
                }
            }
            else
            {
                if ((this.Height - m_CollapseStep) > m_MinHeight)
                {
                    this.Height -= m_CollapseStep;
                    Invalidate();
                }
                else
                {
                    this.Height = m_MinHeight;
                    m_Collapsed = true;
                    Invalidate();
                    m_CollapseTimer.Enabled = false;
                }
            }
        }

        private void HideAfterMouseLeaveTimerTick(object sender, EventArgs e)
        {
          mHideAfterMouseLeaveTimer.Enabled = false;
          HidePane();
        }

        public bool AllowUserInteraction
        {
          get { return mAllowUserInteraction; }
          set
          {
            mAllowUserInteraction = value;
            //if (!mAllowShowing && m_Collapsed)
            //{
            //  m_IsHiding = m_Collapsed;
            //  m_CollapseTimer.Enabled = true;
            //}
          }
        }
        
        public void ShowPane()
        {
          if (m_Collapsed)
          {
            m_CollapseStep = NormalHeight / 10;
            m_IsHiding = m_Collapsed;
            m_CollapseTimer.Enabled = true;
          }
        }

        public void HidePane()
        {
          if (!m_Collapsed)
          {
            m_CollapseStep = NormalHeight / 10;
            m_IsHiding = m_Collapsed;
            m_CollapseTimer.Enabled = true;
          }
        }

        #endregion

    }
}
