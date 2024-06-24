using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Soko.Common.Controls
{
  public partial class ConsoleOutput : UserControl
  {

    #region Private Properties

    delegate void AddTextCallback(string text);

    #endregion

    #region Constructor

    public ConsoleOutput()
    {
      InitializeComponent();
    }

    #endregion

    #region Public Properties

    public NiceScrollBar ScrollBar
    {
      get
      {
        return m_scrollPanel.NiceScrollBar;
      }
    }

    public Color ConsoleForeColor
    {
      set
      {
        m_TextBoxConsole.ForeColor = value;
      }
    }

    public Color ConsoleBgndColor
    {
      set
      {
        m_TextBoxConsole.BackColor = value;
      }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Add new text to information box
    /// </summary>
    /// <param name="text"></param>
    public void AddText(string message)
    {
      string text = "[" + System.DateTime.Now.ToString() + "] : " + message + "\n";
      if (this.InvokeRequired)
      {
        AddTextCallback callback = new AddTextCallback(AddText);
        this.Invoke(callback, new object[] { text });
      }
      else
      {

        m_TextBoxConsole.AppendText(text);
        m_TextBoxConsole.ScrollToCaret();
      }
    }


    #endregion

  }
}
