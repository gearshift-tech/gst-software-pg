using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Soko.Common.Controls
{
  [ToolboxBitmap(typeof(System.Windows.Forms.ComboBox))]
  public partial class NiceComboBox : System.Windows.Forms.ComboBox
  {

    internal class ComboInfoHelper
    {
      [DllImport("user32")]
      private static extern bool GetComboBoxInfo(IntPtr hwndCombo, ref ComboBoxInfo info);

      [StructLayout(LayoutKind.Sequential)]
      private struct RECT
      {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
      }

      [StructLayout(LayoutKind.Sequential)]
      private struct ComboBoxInfo
      {
        public int cbSize;
        public RECT rcItem;
        public RECT rcButton;
        public IntPtr stateButton;
        public IntPtr hwndCombo;
        public IntPtr hwndEdit;
        public IntPtr hwndList;
      }

      public static int GetDropDownWidth()
      {
        ComboBox cb = new ComboBox();
        int width = GetDropDownWidth(cb.Handle);
        cb.Dispose();
        return width;
      }

      public static int GetDropDownWidth(IntPtr handle)
      {
        ComboBoxInfo cbi = new ComboBoxInfo();
        cbi.cbSize = Marshal.SizeOf(cbi);
        GetComboBoxInfo(handle, ref cbi);
        int width = cbi.rcButton.Right - cbi.rcButton.Left;
        return width;
      }

    }



    private const int WM_ERASEBKGND = 0x14;
    private const int WM_PAINT = 0xF;
    private const int WM_NC_PAINT = 0x85;
    private const int WM_PRINTCLIENT = 0x318;
    private const int WM_MOUSEHOVER = 0x2A1;
    private const int WM_MOUSELEAVE = 0x2A3;
    private static int DropDownButtonWidth;
    private bool _isLostFocusChangeStyle;
    private bool _isGotFocusChangeStyle;
    private Color _lostFocusBorderColor;
    private Color _gotFocusBorderColor;
    private ButtonBorderStyle _lostFocusBorderStyle;
    private ButtonBorderStyle _gotFocusBorderStyle;
    private EugenisButtonState _lostFocusDropDownButtonState;
    private EugenisButtonState _gotFocusDropDownButtonState;
    private IntPtr hDC;
    private Graphics gdc;
    private Rectangle rectBorder;
    private Rectangle rectDropDownButton;



    public enum EugenisButtonState
    {
      Original = 1,
      Normal = ButtonState.Normal,
      Inactive = ButtonState.Inactive,
      Pushed = ButtonState.Pushed,
      Checked = ButtonState.Checked,
      Flat = ButtonState.Flat,
      All = ButtonState.All,
    }

    [Category("Custom")]
    [Description("Proceed with the user-defined styling change on lost focus.")]
    public bool UseLostFocusStyle
    {
      get
      {
        return _isLostFocusChangeStyle;
      }
      set
      {
        _isLostFocusChangeStyle = value;
        Invalidate();
      }
    }
    

    
    [Category("Custom")]
    [Description("Proceed with the user-defined styling change on got focus.")]
    public bool UseGotFocusStyle
    {
      get
      {
        return _isGotFocusChangeStyle;
      }
      set
      {
        _isGotFocusChangeStyle = value;
        Invalidate();
      }
    }



    [Category("Custom-LostFocusStyle")]
    [Description("User-defined border color on lost focus.")]
    public Color LostFocusBorderColor
    {
      get
      {
        return _lostFocusBorderColor;
      }
      set
      {
        _lostFocusBorderColor = value;
        Invalidate();
      }
    }



    [Category("Custom-LostFocusStyle")]
    [Description("User-defined border style on lost focus.")]
    public ButtonBorderStyle LostFocusBorderStyle
    {
      get
      {
        return _lostFocusBorderStyle;
      }
      set
      {
        _lostFocusBorderStyle = value;
        Invalidate();
      }
    }



    [Category("Custom-LostFocusStyle")]
    [Description("User-defined drop down button state on lost focus.")]
    public EugenisButtonState LostFocusDropDownButtonState
    {
      get
      {
        return _lostFocusDropDownButtonState;
      }
      set
      {
        _lostFocusDropDownButtonState = value;
        Invalidate();
      }
    }



    [Category("Custom-GotFocusStyle")]
    [Description("User-defined border color on got focus.")]
    public Color GotFocusBorderColor
    {
      get
      {
        return _gotFocusBorderColor;
      }
      set
      {
        _gotFocusBorderColor = value;
      }
    }



    [Category("Custom-GotFocusStyle")]
    [Description("User-defined border style on got focus.")]
    public ButtonBorderStyle GotFocusBorderStyle
    {
      get
      {
        return _gotFocusBorderStyle;
      }
      set
      {
        _gotFocusBorderStyle = value;
      }
    }



    [Category("Custom-GotFocusStyle")]
    [Description("User-defined drop down button state on got focus.")]
    public EugenisButtonState GotFocusDropDownButtonState
    {
      get
      {
        return _gotFocusDropDownButtonState;
      }
      set
      {
        _gotFocusDropDownButtonState = value;
        Invalidate();
      }
    }



    static NiceComboBox()
    {
      DropDownButtonWidth = ComboInfoHelper.GetDropDownWidth() + 2;
    }



    public NiceComboBox() : base()
    {
      _isLostFocusChangeStyle = false;
      _isGotFocusChangeStyle = false;
      _lostFocusBorderColor = Color.Gray;
      _gotFocusBorderColor = Color.Black;
      _lostFocusBorderStyle = ButtonBorderStyle.Solid;
      _gotFocusBorderStyle = ButtonBorderStyle.Solid;
      _lostFocusDropDownButtonState = EugenisButtonState.Flat;
      _gotFocusDropDownButtonState = EugenisButtonState.Original;
      hDC = GetDC(Handle);
      gdc = Graphics.FromHdc(hDC);
      rectBorder = new Rectangle(0, 0, Width, Height);
      rectDropDownButton = new Rectangle(Width - DropDownButtonWidth, 0, DropDownButtonWidth, Height);

      SetStyle(ControlStyles.DoubleBuffer, true);
      SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      SetStyle(ControlStyles.UserPaint, false);
    }







    protected override void OnGotFocus(System.EventArgs e)
    {
      Invalidate();
      base.OnGotFocus(e);
    }



    protected override void OnLostFocus(System.EventArgs e)
    {
      Invalidate();
      base.OnLostFocus(e);
    }



    protected override void OnResize(EventArgs e)
    {
      Invalidate();
      base.OnResize(e);
    }



    protected override void OnSelectedValueChanged(EventArgs e)
    {
      Invalidate();
      base.OnSelectedValueChanged(e);
    }



    protected override void WndProc(ref Message m)
    {
      if (this.DropDownStyle == ComboBoxStyle.Simple)
      {
        base.WndProc(ref m);
        return;
      }

      switch (m.Msg)
      {
        case WM_NC_PAINT:
          break;
        case WM_PAINT:
          base.WndProc(ref m);
          PaintControlDropDownButton();
          PaintControlBorder();
          break;
        default:
          base.WndProc(ref m);
          break;
      }
    }



    [DllImport("user32")]
    private static extern IntPtr GetWindowDC(IntPtr hWnd);

    [DllImport("user32")]
    private static extern IntPtr GetDC(IntPtr hWnd);

    [DllImport("user32")]
    private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);


    private void PaintControlBorder()
    {
      if (Focused && _isGotFocusChangeStyle)
        ControlPaint.DrawBorder(gdc, rectBorder, _gotFocusBorderColor, _gotFocusBorderStyle);
      else if (!Focused && _isLostFocusChangeStyle)
        ControlPaint.DrawBorder(gdc, rectBorder, _lostFocusBorderColor, _lostFocusBorderStyle);
      else { }
    }



    private void PaintControlDropDownButton()
    {
      if (Focused && _isGotFocusChangeStyle && _gotFocusDropDownButtonState != EugenisButtonState.Original)
        ControlPaint.DrawComboButton(gdc, rectDropDownButton, (ButtonState)_gotFocusDropDownButtonState);
      else if (!Focused && _isLostFocusChangeStyle && _lostFocusDropDownButtonState != EugenisButtonState.Original)
        ControlPaint.DrawComboButton(gdc, rectDropDownButton, (ButtonState)_lostFocusDropDownButtonState);
      else { }
    }


  }
  
}
