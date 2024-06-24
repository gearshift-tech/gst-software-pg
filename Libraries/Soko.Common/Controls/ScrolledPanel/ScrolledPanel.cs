using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Runtime;
using System.Runtime.InteropServices;

namespace Soko.Common.Controls
{
  /// <summary>
  /// Class:              ScrolledPanel
  /// Author:             Pawel Pustelnik
  /// Created:            19.11.2007
  /// Last modification:  27.12.2007
  /// 
  /// Description:
  /// ScrolledPanel is a typical Panel with one differnce. It used NiceScrollBar to show vertical scrollbar.
  /// It can be used to work with controls which has a scrollbar with no AutoScroll option, e.g. ListBox, RichTextBox.
  /// </summary>
  public partial class ScrolledPanel : System.Windows.Forms.Panel
  {
    private System.Windows.Forms.Control m_MainControl = null;
    private ScrollInfoStruct m_ScrollInfoStruct;

    private const int WM_HSCROLL = 276;
    private const int WM_VSCROLL = 277;
    private const int SB_THUMBPOSITION = 4;
    private const int SIF_TRACKPOS = 16;
    private const int SIF_RANGE = 1;
    private const int SIF_POS = 4;
    private const int SIF_PAGE = 2;
    private const int SIF_ALL = SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS;



    [DllImport("user32.dll")]
    public static extern bool PostMessageA(
              IntPtr hWnd,      // handle to destination window
              int Msg,       // message
              int wParam,  // first message parameter
              int lParam   // second message parameter
              );



    [DllImport("user32.dll")]
    public static extern int GetScrollInfo(
        IntPtr hWnd,      // handle to destination window
        int n,          //scroll number
        ref ScrollInfoStruct lpScrollInfo //ScrollInfo structure
        );


    /// <summary>
    /// Scroll Info Structure
    /// </summary>
    public struct ScrollInfoStruct
    {
      public int cbSize;
      public int fMask;
      public int nMin;
      public int nMax;
      public int nPage;
      public int nPos;
      public int nTrackPos;
    }

    /// <summary>
    /// Returns NiceScrollBar control
    /// </summary>
    public NiceScrollBar NiceScrollBar
    {
      get
      {
        return m_ScrollBar;
      }
    }

    /// <summary>
    /// Main Constructor
    /// </summary>
    /// <remarks></remarks>
    public ScrolledPanel()
    {
      // This call is required by the Windows Form Designer.
      InitializeComponent();
    }
    /// <summary>
    /// Constructor with default control added
    /// </summary>
    /// <param name="win"></param>
    /// <remarks></remarks>
    public ScrolledPanel(System.Windows.Forms.Control win)
    {
      // This call is required by the Windows Form Designer.
      InitializeComponent();
      m_MainControl = win;
      Controls.Add(win);
      // Fix the fake scrolling control to overlap the real scrollable control
      m_ScrollBar.Size = new System.Drawing.Size(18, m_MainControl.Height);
      this.Size = new System.Drawing.Size(m_MainControl.Width, m_MainControl.Height);
      this.Location = new System.Drawing.Point(m_MainControl.Left, m_MainControl.Top);
      this.Dock = m_MainControl.Dock;
      m_MainControl.Top = 0;
      m_MainControl.Left = 0;
      m_MainControl.SendToBack();
      this.Name = "skin" + m_MainControl.Name;

    }

    /// <summary>
    /// Overriding for wnd proc
    /// </summary>
    /// <param name="m"></param>
    /// <remarks></remarks>
    protected override void WndProc(ref Message m)
    {
      base.WndProc(ref m);
      if (this.DesignMode)
        return;


      if (!this.Parent.CanFocus | (m_MainControl == null))
        return;

      m_ScrollInfoStruct.fMask = SIF_ALL;
      m_ScrollInfoStruct.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(m_ScrollInfoStruct);
      GetScrollInfo(m_MainControl.Handle, 1, ref m_ScrollInfoStruct);
      if ((m_ScrollInfoStruct.nMax + 0) < m_ScrollInfoStruct.nPage)
      {
        m_ScrollBar.Visible = false;
      }
      else
      {
        if (m_ScrollInfoStruct.nPage == 0)
        {
          m_ScrollBar.Visible = false;
          return;
        }

        m_ScrollBar.Visible = true;
        m_ScrollBar.Maximum = m_ScrollInfoStruct.nMax - m_ScrollInfoStruct.nPage + 1;
        m_ScrollBar.Minimum = m_ScrollInfoStruct.nMin;
        m_ScrollBar.SmallChange = 10;
        m_ScrollBar.LargeChange = m_ScrollInfoStruct.nMax / m_ScrollInfoStruct.nPage;
        m_ScrollBar.Value = m_ScrollInfoStruct.nPos;
      }
    }
    /// <summary>
    /// Scroll Bar was scrolled
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <remarks></remarks>
    private void ScrollBarScrolled(object sender, EventArgs e)
    {
      if (m_MainControl != null)
      {
        PostMessageA(m_MainControl.Handle, WM_VSCROLL,
            SB_THUMBPOSITION + 65536 * m_ScrollBar.Value, 0);
      }
    }

    /// <summary>
    /// Overriding for added control
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <remarks></remarks>
    protected override void OnControlAdded(ControlEventArgs e)
    {
      if (Controls.Count == 1)
        return;

      if ((m_MainControl != null))
        return;

      m_MainControl = e.Control;

      m_MainControl.Resize += new EventHandler(ControlResize);

      // Fix the fake scrolling control to overlap the real scrollable control
      m_ScrollBar.Size = new Size(16, m_MainControl.Height);
      this.Size = new Size(m_MainControl.Width, m_MainControl.Height);
      this.Location = new Point(m_MainControl.Left, m_MainControl.Top);

      m_MainControl.Top = 0;
      m_MainControl.Left = 0;
      m_MainControl.SendToBack();

    }

    /// <summary>
    /// Overriding for control resized
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ControlResize(object sender, EventArgs e)
    {
      m_ScrollBar.Size = new System.Drawing.Size(16, m_MainControl.Height);
      this.Size = new System.Drawing.Size(m_MainControl.Width, m_MainControl.Height);
      m_ScrollBar.Left = m_MainControl.Right - 19;
      m_MainControl.Top = 0;
      m_MainControl.Left = 0;
    }


  }
}
