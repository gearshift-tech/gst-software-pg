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
    public partial class SetMasterDataForm : Form
    {

        TestScriptReport baseReport = new TestScriptReport();

        public SetMasterDataForm(TestScriptReport reportToModify)
        {
            baseReport = reportToModify;
            InitializeComponent();
        }

        private void AssignMasterDataAndSave(string fileName)
        {
            TestScriptReport tsr = TestScriptReport.OpenFile(fileName);

            TestScript mTestScript = baseReport.TestScriptRunned;

            if (mTestScript.FrameSet.Count == tsr.TestScriptRunned.FrameSet.Count)
            {
                // If the file is ok, assign the new master data
                for (int i = 0; i < mTestScript.FrameSet.Count; i++)
                {
                    mTestScript.FrameSet[i].MasterPressureReadValues.Clear();
                    for (int j = 0; j < mTestScript.FrameSet[i].PressureReadValues.Count; j++)
                    {
                        mTestScript.FrameSet[i].MasterPressureReadValues.Add(tsr.TestScriptRunned.FrameSet[i].PressureReadValues[j]);
                    }
                }
                mTestScript.HasConsistentMasterData = true;

                // If user wants to permanently store the new master data, save the file.
                if (saveToDiskCheckBox.Checked)
                {
                    baseReport.SaveToFile(baseReport.Filename);
                }

                // Close this window
                this.Close();
            }
            else
            {
                Soko.Common.Forms.MessageBox.ShowWarning("GearShift",
                    "Data incompatibility",
                    "Selected file is not compatible with the currently viewed test report.\nSelect a report based on the same test script as the viewed report",
                    Soko.Common.Forms.MessageBoxButtons.OK);
            }


        }

        private void PopulateMDFilesComboBox(object sender, EventArgs e)
        {
            try
            {
                TSMasterDataComboBox.Items.Clear();
                string[] filePaths = GST.Gearshift.Components.Utilities.Settings.AvailableReportsPaths;

                foreach (string filePath in filePaths)
                {
                    TSMasterDataComboBox.Items.Add(new ListBoxFileItem(filePath, Path.GetFileNameWithoutExtension(filePath)));
                }
            }
            catch (Exception)
            {
                throw new DirectoryNotFoundException("Either the application was improperly installed or the DAQ reports directory was removed by user");
            }
        }

        private void TSMasterDataComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TSMasterDataComboBox.SelectedIndex != -1)
            {
                ListBoxFileItem selected = (ListBoxFileItem)TSMasterDataComboBox.SelectedItem;
                AssignMasterDataAndSave(selected.mFilePath);
            }
        }
    }
}
