using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Soko.Common.Controls
{
    public partial class TransparentListBox: ListBox
    {
        public class Item
        {
            public string Title = string.Empty;
            public string Details = string.Empty;
            public Object Tag = null;

            public Item() {}
            public Item(string title) { Title = title; }
            public Item(string title, string details) { Title = title; Details = details; }
        }

        #region Properties and such

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


        public TransparentListBox()
        {
            //this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(
                //ControlStyles.SupportsTransparentBackColor |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint |
                //ControlStyles.ResizeRedraw |
                ControlStyles.AllPaintingInWmPaint
                , true);

            this.DoubleBuffered = true;

            InitializeComponent();
        }

        private void DisposeLocalResources()
        {
            if (m_BackgroundImage != null)
            {
                m_BackgroundImage.Dispose();
                m_BackgroundImage = null;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (m_DrawingBackgroundImage)
                return;

            if (m_BackgroundImage != null)
            {
                e.Graphics.DrawImage(m_BackgroundImage,
                    new Rectangle(0, 0, ClientSize.Width, ClientSize.Height),
                    0, 0, ClientSize.Width, ClientSize.Height, GraphicsUnit.Pixel);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (m_DrawingBackgroundImage)
                return;

            DoPaint(e);
        }

        void DoPaint(PaintEventArgs e)
        {
            if (Items.Count == 0)
                return;

            for (var i = TopIndex; i < Items.Count; ++i)
                DoDrawItem(e.Graphics, i);
        }

        void DoDrawItem(Graphics _g, int i)
        {
            var itemRect = GetItemRectangle(i);
            if (itemRect.Top > ClientSize.Height)
                return;

            using (Bitmap buffer = new Bitmap(itemRect.Width, itemRect.Height))
            using (Graphics g = Graphics.FromImage(buffer))
            {
                var bounds = new Rectangle(0, 0, itemRect.Width, itemRect.Height);

                if (m_BackgroundImage != null)
                {
                    var topRect = GetItemRectangle(TopIndex);

                    int y = itemRect.Y - topRect.Y;

                    g.DrawImage(m_BackgroundImage,
                        new Rectangle(0, 0, bounds.Width, bounds.Height),
                        0, y, bounds.Width, bounds.Height, GraphicsUnit.Pixel);
                }

                RectangleF titleRectangle = new RectangleF(
                  bounds.X + bounds.Width * 0.1f,
                  bounds.Y,
                  bounds.Width * 0.9f,
                  bounds.Height / 2.0f);

                RectangleF contentRectangle = new RectangleF(
                    bounds.X + bounds.Width * 0.1f,
                    bounds.Y + bounds.Height / 2.0f,
                    bounds.Width * 0.9f,
                    bounds.Height / 2.0f);

                var color0 = _backColor1;
                var color1 = _backColor2;

                if (SelectedIndex == i)
                  color0 = Color.DodgerBlue;

                color0 = Color.FromArgb(255, color0);
                color1 = Color.FromArgb(192, color1);

                using (LinearGradientBrush lgb = new LinearGradientBrush(bounds, color0, color1, _focusAngle, true))
                    g.FillRectangle(lgb, bounds);

                using (StringFormat sf = new StringFormat())
                {
                    Item item = Items[i] as Item;
                    if (null == item)
                        return;

                    sf.Alignment = StringAlignment.Near;
                    sf.FormatFlags = StringFormatFlags.NoWrap;
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Trimming = StringTrimming.EllipsisCharacter;

                    using (var titleTextBrush = new SolidBrush(_titleTextColor))
                        g.DrawString(item.Title, _titleTextFont, titleTextBrush, titleRectangle, sf);

                    sf.LineAlignment = StringAlignment.Near;
                    using (var contentTextBrush = new SolidBrush(_contentTextColor))
                        g.DrawString(item.Details, _contentTextFont, contentTextBrush, contentRectangle, sf);
                }
                                
                _g.DrawImage(buffer, itemRect.Location);
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0014/*WM_ERASEBKGND*/)
                UpdateBackgroundImage();

            base.WndProc(ref m);

            if (m_DrawingBackgroundImage)
                return;

            if (m.Msg == 0x0115/*WM_VSCROLL*/ || m.Msg == 0x020A/*WM_MOUSEWHEEL*/)
                Refresh();
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            DoDrawItem(e.Graphics, e.Index);
        }

        private void OnResize(object sender, EventArgs e)
        {
            InvalidateBackgroundImage();
        }

        private void OnMove(object sender, EventArgs e)
        {
            InvalidateBackgroundImage();
        }

        void UpdateBackgroundImage()
        {
            if (Parent == null || DesignMode || m_DrawingBackgroundImage)
                return;

            if (m_InvalidateBackgroundImage)
            {
                if (m_BackgroundImage != null)
                {
                    m_BackgroundImage.Dispose();
                    m_BackgroundImage = null;
                }

                m_InvalidateBackgroundImage = false;
            }

            if (m_BackgroundImage == null)
            {
                bool lastUserPaintState = this.GetStyle(ControlStyles.UserPaint);
                this.SetStyle(ControlStyles.UserPaint, true);

                m_DrawingBackgroundImage = true;

                using (Bitmap parentContent = new Bitmap(Parent.Width, Parent.Height))
                {
                    Parent.DrawToBitmap(parentContent, new Rectangle(0, 0, Parent.Width, Parent.Height));

                    var absoluteParentTopLeft = TopLevelControl.Location;
                    var absoluteParentClientTopLeft = Parent.PointToScreen(Point.Empty);

                    var offset = new Point(
                        absoluteParentClientTopLeft.X - absoluteParentTopLeft.X,
                        absoluteParentClientTopLeft.Y - absoluteParentTopLeft.Y);

                    m_BackgroundImage = new Bitmap(Width, Height);
                    using (Graphics g = Graphics.FromImage(m_BackgroundImage))
                    {
                        var origin = Parent.PointToClient(PointToScreen(new Point(0, 0)));
                        g.DrawImage(parentContent,
                            new Rectangle(0, 0, ClientSize.Width, ClientSize.Height),
                            offset.X + origin.X, offset.Y + origin.Y, ClientSize.Width, ClientSize.Height,
                            GraphicsUnit.Pixel);
                    }
                }

                m_DrawingBackgroundImage = false;

                this.SetStyle(ControlStyles.UserPaint, lastUserPaintState);

                //this.BackgroundImage = m_BackgroundImage;
            }
        }

        private void CustomListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Invalidate();
            //RefreshItem(SelectedIndex);
        }

        void InvalidateBackgroundImage()
        {
            m_InvalidateBackgroundImage = true;
            Invalidate();
        }

        Bitmap m_BackgroundImage = null;
        bool m_DrawingBackgroundImage = false;
        bool m_InvalidateBackgroundImage = false;
    }
}
