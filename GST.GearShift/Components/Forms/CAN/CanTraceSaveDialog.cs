using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GST.Gearshift.Components.Interfaces.USB;

namespace GST.Gearshift.Components.Forms.CAN
{
    public partial class CanTraceSaveDialog : Form
    {

        GearShiftUsb mDevice = null;
        CANTrace mTrace = null;

        public CanTraceSaveDialog(GearShiftUsb dev, CANTrace trc)
        {
            InitializeComponent();
            mDevice = dev;
            mTrace = trc;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (filenameTextbox.Text != String.Empty)
            {
                // Check if file with the selected name already exists
                if (System.IO.File.Exists(GST.Gearshift.Components.Utilities.Settings.CanTracesDirectory + "\\" + filenameTextbox.Text + ".ezt"))
                {
                    // If exists check if it should be overwritten
                    DialogResult rslt = global::Soko.Common.Forms.MessageBox.ShowWarning("GearShift",
                                                            "Data loss warning",
                                                            "The file with selected name already exists. Do you want to overwrite it?",
                                                            Soko.Common.Forms.MessageBoxButtons.YesNo);
                    if (rslt == DialogResult.No)
                    {
                        // Exit this function and return to the form
                        return;
                    }
                }

                mTrace.TraceData = mDevice.CANRecordBuffer.ToArray();
                mTrace.SaveXml(GST.Gearshift.Components.Utilities.Settings.CanTracesDirectory + "\\" + filenameTextbox.Text + ".ezt");
                this.Close();
            }
        }
    }
}
