using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Soko.Common.Common;
using GST.Gearshift.Components.Interfaces.USB;

namespace GST.Gearshift.Components.Forms.DAQ
{
    public partial class ReportExplorerRenameDialog : Form
    {

        string reportFilePath = string.Empty;

        public ReportExplorerRenameDialog(string rFP)
        {
            InitializeComponent();
            reportFilePath = rFP;
            label1.Text = "Enter the new file name for " + Path.GetFileNameWithoutExtension(rFP) + " report:";
        }



        private void renameButton_Click(object sender, EventArgs e)
        {
            string newFilename = fileNameTextBox.Text;

            // Check if the source file exists
            if (!File.Exists(reportFilePath))
            {
                Soko.Common.Forms.MessageBox.ShowCritical("GearShift",
                                                              "Invalid file name",
                                                              "The selected file could not be located on disk. Please try again",
                                                              Soko.Common.Forms.MessageBoxButtons.OK);
                this.Close();
                return;
            }

            // Check if the new file name doesnt contain any invalid characters
            if (newFilename.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                Soko.Common.Forms.MessageBox.ShowCritical("GearShift",
                                                              "Invalid file name",
                                                              "The new file name contains some invalid characters. \n Correct it and try again",
                                                              Soko.Common.Forms.MessageBoxButtons.OK);
                return;
            }

            // Check if new file exists
            if (File.Exists(newFilename))
            {
                DialogResult dr = Soko.Common.Forms.MessageBox.ShowCritical("GearShift",
                                                                                "Data loss warning",
                                                                                "A report with the new file name already exists. It will be overritten and the data will be lost.\n Do you want to continue?",
                                                                                Soko.Common.Forms.MessageBoxButtons.YesNo);
                if (dr != System.Windows.Forms.DialogResult.Yes)
                {
                    return;
                }
            }

            // Rename the file
            try
            {
                string nfn = GST.Gearshift.Components.Utilities.Settings.ReportsDirectory + "\\" + newFilename + ".ctrf";
                File.Move(reportFilePath, nfn);
                Soko.Common.Forms.MessageBox.ShowInfo("GearShift",
                                                                        "Operation successfull",
                                                                        "File renamed successfully",
                                                                        Soko.Common.Forms.MessageBoxButtons.OK);
                this.Close();
            }
            catch (Exception ex)
            {
                Soko.Common.Forms.MessageBox.ShowCritical("GearShift",
                                                                                "Operation failure",
                                                                                "Failed to rename the file: " + ex.Message,
                                                                                Soko.Common.Forms.MessageBoxButtons.OK);
            }

        }

    }
}
