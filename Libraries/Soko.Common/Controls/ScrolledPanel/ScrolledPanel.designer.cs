
namespace Soko.Common.Controls
{
  public partial class ScrolledPanel : System.Windows.Forms.Panel
  {

    //UserControl overrides dispose to clean up the component list.
    [System.Diagnostics.DebuggerNonUserCode()]
    protected override void Dispose(bool disposing)
    {
      if (disposing && components != null)
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    //Required by the Windows Form Designer
    private System.ComponentModel.IContainer components;

    //NOTE: The following procedure is required by the Windows Form Designer
    //It can be modified using the Windows Form Designer. 
    //Do not modify it using the code editor.
    [System.Diagnostics.DebuggerStepThrough()]
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();

      m_ScrollBar = new NiceScrollBar();
      this.SuspendLayout();
      //
      //m_ScrollBar
      //
      m_ScrollBar.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right);
      m_ScrollBar.LargeChange = 1;
      m_ScrollBar.Location = new System.Drawing.Point(145, 0);
      m_ScrollBar.Maximum = 10;
      m_ScrollBar.Minimum = 0;
      m_ScrollBar.MinimumSize = new System.Drawing.Size(19, 15);
      m_ScrollBar.Name = "m_ScrollBar";
      m_ScrollBar.Size = new System.Drawing.Size(19, 127);
      m_ScrollBar.SmallChange = 1;
      m_ScrollBar.TabIndex = 0;
      m_ScrollBar.Value = 10;
      m_ScrollBar.Visible = true;
      m_ScrollBar.Scroll += new System.EventHandler(ScrollBarScrolled);
      m_ScrollBar.ColorScheme = NiceScrollBar.Theme.Black;
      //
      //ScrolledPanel
      //
      this.AutoScroll = false;
      this.BackColor = System.Drawing.Color.Gainsboro;
      this.Controls.Add(m_ScrollBar);
      this.Size = new System.Drawing.Size(164, 127);
      this.ResumeLayout(false);

    }

    private NiceScrollBar m_ScrollBar;

  }
}