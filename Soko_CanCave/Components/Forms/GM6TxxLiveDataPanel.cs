using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Soko.CanCave.Components.Forms
{
  public partial class GM6TxxLiveDataPanel : UserControl
  {
    public GM6TxxLiveDataPanel()
    {
      InitializeComponent();
      //this.BackgroundImage = Properties.Resources.carbonBgnd;
      TFPS1NiceInd.ImageON = Properties.Resources.TFP_ON;
      TFPS1NiceInd.ImageOFF = Properties.Resources.TFP_OFF;
      TFPS1NiceInd.Size = new System.Drawing.Size(150, 60);
      TFPS5NiceInd.ImageON = Properties.Resources.TFP_ON;
      TFPS5NiceInd.ImageOFF = Properties.Resources.TFP_OFF;
      TFPS5NiceInd.Size = new System.Drawing.Size(150, 60);
      TFPS3NiceInd.ImageON = Properties.Resources.TFP_ON;
      TFPS3NiceInd.ImageOFF = Properties.Resources.TFP_OFF;
      TFPS3NiceInd.Size = new System.Drawing.Size(150, 60);
      TFPS4NiceInd.ImageON = Properties.Resources.TFP_ON;
      TFPS4NiceInd.ImageOFF = Properties.Resources.TFP_OFF;
      TFPS4NiceInd.Size = new System.Drawing.Size(150, 60);
      brakeSwNiceInd.ImageON = Properties.Resources.TFP_ON;
      brakeSwNiceInd.ImageOFF = Properties.Resources.TFP_OFF;
      brakeSwNiceInd.Size = new System.Drawing.Size(150, 60);
    }

    public void RefreshDisplayedValues()
    {
    }

    private bool _showS2Solenoid = false;
    public bool ShowS2Solenoid
    {
      get { return _showS2Solenoid; }
      set
      {
        _showS2Solenoid = value;
        GM6TxxLiveDataPanel_Resize(this, EventArgs.Empty);
      }
    }

  Soko.CanCave.Components.Interfaces.TCUGovernor_GM6Txx _GM6TGov = null;
    /// <summary>
    /// GM6T governor
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Soko.CanCave.Components.Interfaces.TCUGovernor_GM6Txx GM6TGovernor
    {
      get { return _GM6TGov; }
      set
      {
        if (value != null)
        {
          _GM6TGov = value;
          if (_GM6TGov.UsbDeviceIsConnected)
          {
            //DeviceConnected();
          }
          else
          {
            //DeviceDisconnected();
          }
          //_GM6TGov.UsbDeviceConnected += new EventHandler(device_Connected);
          //_GM6TGov.UsbDeviceDisconnected += new EventHandler(device_Disconnected);
        }
      }
    }

    private void GM6TxxLiveDataPanel_Resize(object sender, EventArgs e)
    {
      SuspendLayout();

      int gaugeCount = (_showS2Solenoid) ? 8 : 7;

      float gaugesAreaWidth = this.Width - 200;
      int desiredGaugeWidth = (int)((gaugesAreaWidth / gaugeCount) * 0.7);
      if (desiredGaugeWidth > 120)
        desiredGaugeWidth = 120;
      float emptyWidth = gaugesAreaWidth - gaugeCount * lg1.Width;
      float spacing = emptyWidth / gaugeCount;
      float xpos = spacing / 2;

      lg1.Left = (int)xpos;
      lg1.Width = desiredGaugeWidth;
      xpos += gaugesAreaWidth / gaugeCount;
      lg2.Left = (int)xpos;
      lg2.Width = desiredGaugeWidth;
      xpos += gaugesAreaWidth / gaugeCount;
      lg3.Left = (int)xpos;
      lg3.Width = desiredGaugeWidth;
      xpos += gaugesAreaWidth / gaugeCount;
      lg4.Left = (int)xpos;
      lg4.Width = desiredGaugeWidth;
      xpos += gaugesAreaWidth / gaugeCount;
      lg5.Left = (int)xpos;
      lg5.Width = desiredGaugeWidth;
      xpos += gaugesAreaWidth / gaugeCount;
      lg6.Left = (int)xpos;
      lg6.Width = desiredGaugeWidth;
      xpos += gaugesAreaWidth / gaugeCount;
      lg7.Left = (int)xpos;
      lg7.Width = desiredGaugeWidth;
      xpos += gaugesAreaWidth / gaugeCount;
      lg8.Left = (_showS2Solenoid) ? (int)xpos : -1000;
      lg8.Width = desiredGaugeWidth;

      ResumeLayout();
    }

    private void guiUpdateTimer_Tick(object sender, EventArgs e)
    {
      if (_GM6TGov == null)
        return;
      SuspendLayout();
      TFPS1NiceInd.IsOn = _GM6TGov.TCU_Data.Switch_Pressure1;
      TFPS3NiceInd.IsOn = _GM6TGov.TCU_Data.Switch_Pressure3;
      TFPS4NiceInd.IsOn = _GM6TGov.TCU_Data.Switch_Pressure4;
      TFPS5NiceInd.IsOn = _GM6TGov.TCU_Data.Switch_Pressure5;
      brakeSwNiceInd.IsOn = _GM6TGov.TCU_Data.Switch_Brake;


      lg1.Value = _GM6TGov.TCU_Data.Solenoid_PC1;
      lg1.Text = "Line PC Solenoid: " + lg1.Value.ToString("0.0") + " psi";
      lg2.Value = _GM6TGov.TCU_Data.Solenoid_PC2;
      lg2.Text = "PC Solenoid 2: " + lg2.Value.ToString("0.0") + " psi";
      lg3.Value = _GM6TGov.TCU_Data.Solenoid_PC3;
      lg3.Text = "PC Solenoid 3: " + lg3.Value.ToString("0.0") + " psi";
      lg4.Value = _GM6TGov.TCU_Data.Solenoid_PC4;
      lg4.Text = "PC Solenoid 4: " + lg4.Value.ToString("0.0") + " psi";
      lg5.Value = _GM6TGov.TCU_Data.Solenoid_PC5;
      lg5.Text = "PC Solenoid 5: " + lg5.Value.ToString("0.0") + " psi";
      lg6.Value = _GM6TGov.TCU_Data.Solenoid_PC6;
      lg6.Text = "TCC PC Solenoid: " + lg6.Value.ToString("0.0") + " psi";
      if (_GM6TGov.TCU_Data.Solenoid_Shift1)
      {
        lg7.Value = lg7.MaxValue;
        lg7.Text = "Shift Solenoid 1: ON";
      }
      else
      {
        lg7.Value = lg7.MinValue;
        lg7.Text = "Shift Solenoid 1: OFF";
      }
      if (_GM6TGov.TCU_Data.Solenoid_Shift2)
      {
        lg8.Value = lg7.MaxValue;
        lg8.Text = "Shift Solenoid 2: ON";
      }
      else
      {
        lg8.Value = lg7.MinValue;
        lg8.Text = "Shift Solenoid 2: OFF";
      }

      ResumeLayout();
    }

  }

}
