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
    public partial class ImportGearboxConfigForm : Form
    {

        TestScript _scriptToImport = null;

        public event EventHandler GearboxConfigImported;

        public ImportGearboxConfigForm(TestScript importDestinationScript)
        {
            _scriptToImport = importDestinationScript;
            InitializeComponent();
        }

        private void PopulateGearboxFilesComboBox()
        {
            try
            {
                gearboxFilesComboBox.Items.Clear();
                string[] filePaths = GST.Gearshift.Components.Utilities.Settings.AvailableGearboxConfigsPaths;

                foreach (string filePath in filePaths)
                {
                    gearboxFilesComboBox.Items.Add(new ListBoxFileItem(filePath, Path.GetFileNameWithoutExtension(filePath)));
                }
            }
            catch (Exception)
            {
                throw new DirectoryNotFoundException("Either the application was improperly installed or the gearboxes definitions directory was removed by user");
            }
        }

        private void PopulateScriptFilesComboBox()
        {
            try
            {
                testScriptFilesComboBox.Items.Clear();
                string[] filePaths = GST.Gearshift.Components.Utilities.Settings.AvailableTestScriptsPaths;

                foreach (string filePath in filePaths)
                {
                    testScriptFilesComboBox.Items.Add(new ListBoxFileItem(filePath, Path.GetFileNameWithoutExtension(filePath)));
                }
            }
            catch (Exception)
            {
                throw new DirectoryNotFoundException("Either the application was improperly installed or the gearboxes definitions directory was removed by user");
            }
        }

        private void gearboxFilesComboBox_DropDown(object sender, EventArgs e)
        {
            PopulateGearboxFilesComboBox();
        }

        private void testScriptFilesComboBox_DropDown(object sender, EventArgs e)
        {
            PopulateScriptFilesComboBox();
        }

        private void gearboxFilesComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (gearboxFilesComboBox.SelectedIndex != -1)
                {
                    ListBoxFileItem selected = (ListBoxFileItem)gearboxFilesComboBox.SelectedItem;
                    GearboxConfig gc = new GearboxConfig();
                    _scriptToImport.Gearbox = gc.OpenXml(selected.mFilePath);
                    if (GearboxConfigImported != null)
                    {
                        GearboxConfigImported(this, EventArgs.Empty);
                    }
                    Soko.Common.Forms.MessageBox.ShowInfo("GearShift", "Operation successfull",
                                                              "The gearbox config has been succesfully imported",
                                                              Soko.Common.Forms.MessageBoxButtons.OK);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Soko.Common.Forms.MessageBox.ShowCritical("GearShift",
                      "Import error",
                      "Failed to import the data due to the following error:\n" + ex.Message,
                      Soko.Common.Forms.MessageBoxButtons.OK);
                this.Close();
            }
        }

        private void testScriptFilesComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (testScriptFilesComboBox.SelectedIndex != -1)
                {
                    ListBoxFileItem selected = (ListBoxFileItem)testScriptFilesComboBox.SelectedItem;
                    TestScript ts = TestScript.OpenXml(selected.mFilePath);
                    _scriptToImport.Gearbox = ts.Gearbox;
                    if (GearboxConfigImported != null)
                    {
                        GearboxConfigImported(this, EventArgs.Empty);
                    }
                    Soko.Common.Forms.MessageBox.ShowInfo("GearShift", "Operation successfull",
                                                              "The gearbox config has been succesfully imported",
                                                              Soko.Common.Forms.MessageBoxButtons.OK);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Soko.Common.Forms.MessageBox.ShowCritical("GearShift",
                      "Import error",
                      "Failed to import the data due to the following error:\n" + ex.Message,
                      Soko.Common.Forms.MessageBoxButtons.OK);
                this.Close();
            }
        }

        private void ImportGearboxConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
