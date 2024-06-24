using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Soko.CanCave.Components.Interfaces.CanCaveUsb;
using Soko.CanCave.Components.Interfaces;
using Soko.Common.Common;

namespace CommLibTest
{
  public partial class MainForm : Form
  {
    UsbDevice device = null;
    TCUGovernor_GM6Txx _GM6TxxInterface = null;


    public MainForm()
    {
      List<UInt32> MsgFilter = new List<uint>();
      //MsgFilter.AddRange(new uint[] { 0x199, 0x19D, 0x4C9, 0x1F5, 0x1AF, 0x77F, 0xC7, 0xF9 }); // Messages sent by trans
      //MsgFilter.AddRange(new uint[] { 0x7E2, 0x7E9 }); // Diagnostics
      //MsgFilter.AddRange(new uint[] { 0x4F1, 0x500, 0x52A, 0x771, 0x772, 0x773, 0x77F, 0x120, 0x138, 0x4D1 }); // Slow bus messages >= 500ms
      //MsgFilter.AddRange(new uint[] { 0x12A, 0x134, 0x17D, 0x3C1, 0x3C9, 0x3D1, 0x3F1, 0x3F9, 0xD0 }); // Medium bus messages >= 100ms
      //MsgFilter.AddRange(new uint[] { 0x2C3, 0x2D1, 0x2D3, 0x334, 0x348, 0x34A, 0x184, 0x1C3, 0x1C7, 0x1CD, 0x1E1 }); // Medium bus messages >= 25ms
      //MsgFilter.AddRange(new uint[] { 0x1E5}); // Fast bus messages >= 10ms

      //MsgFilter.AddRange(new uint[] { 0x2F9, 0xC5, 0xC1, 0x1A1 }); // OUR DATA WHEEL INFO
      //MsgFilter.AddRange(new uint[] { 0x514, 0x4E1 }); // OUR DATA VIN

      //MsgFilter.AddRange(new uint[] { 0x3E9 }); // OUR DATA VEHICLE SPEED

      //MsgFilter.AddRange(new uint[] { 0xC9 }); // OUR DATA ENGINE SPEED
      //MsgFilter.AddRange(new uint[] { 0x191 }); // OUR DATA ENGINE TORQUE

      ////MsgFilter.AddRange(new uint[] { 0x1F3 }); // OUR DATA TAP U/D

      //MsgFilter.AddRange(new uint[] { 0x4E9 }); // OUR DATA Platform
      //MsgFilter.AddRange(new uint[] { 0x1E9 }); // OUR Traction control
      //MsgFilter.AddRange(new uint[] { 0x1F1 }); // System info

      //MsgFilter.AddRange(new uint[] { 0xF1 }); // Brake pedal
      //MsgFilter.AddRange(new uint[] { 0x4C1 }); // Coolant temp


      //FILECONVERTER_VspyToPcan converter = new FILECONVERTER_VspyToPcan();
      //converter.ConvertFiles("d://src.csv", "d://dst.trc", MsgFilter);


      InitializeComponent();
      device = new UsbDevice();
      _GM6TxxInterface = new TCUGovernor_GM6Txx(device);

      Soko.CanCave.Components.Forms.Gm6T40BarePanel barePanel = new Soko.CanCave.Components.Forms.Gm6T40BarePanel(_GM6TxxInterface);
      this.Controls.Add(barePanel);
      barePanel.Dock = DockStyle.Fill;
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      device.Disconnect();
    }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // no smaller than design time size
            this.MinimumSize = new Size(this.Width, this.Height);

            // no larger than screen size
            this.MaximumSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        }
    }
}
