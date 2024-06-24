using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GST.Gearshift.Components.Interfaces;
using GST.Gearshift.Components.Utilities;
using Syncfusion.Windows.Forms.Tools;

namespace GST.Gearshift.Components.Forms
{
    public partial class SettingsEditor : UserControl
    {
        GST.Gearshift.Components.Utilities.Settings appSettings = GST.Gearshift.Components.Utilities.Settings.Instance;

        private bool fileLoading = false;
        //DockingClientPanel panel = new DockingClientPanel();
        public SettingsEditor()
        {
            InitializeComponent();
            LoadSettings();            
            //this.Controls.Add(this.panel); // add empty doccking panel to form
            //this.panel.Controls.Add(this.tabControl1); // add tabControl to docking panel
            //this.viewModeTabControl.Dock = DockStyle.Fill;
        }

        private void LoadSettings()
        {
            // Set the flag so that the file wont get autosaved when setting the controls state
            fileLoading = true;

            operatorsListBox.Items.Clear();
            operatorsListBox.Items.AddRange(appSettings.SystemOperators);
            switch (appSettings.MainFormWindowState)
            {
                case FormWindowState.Minimized:
                    {
                        minimizedStateRB.Checked = true;
                        break;
                    }
                case FormWindowState.Normal:
                    {
                        normalStateRB.Checked = true;
                        break;
                    }
                case FormWindowState.Maximized:
                    {
                        maximizedStateRB.Checked = true;
                        break;
                    }
            }
            loopModeWarningCheckbox.Checked = appSettings.ShowLoopModeWarning;
            autoModeWarningCheckBox.Checked = appSettings.ShowAutomaticModeWarning;

            #region USER UNITS LOADING
            switch (appSettings.UserFlowUnit)
            {
                case MeasurementUnit.FlowUnit.CFM:
                    {
                        cfmUnitRB.Checked = true;
                        break;
                    }
                case MeasurementUnit.FlowUnit.cmph:
                    {
                        mphUnitRB.Checked = true;
                        break;
                    }
                case MeasurementUnit.FlowUnit.GPH:
                    {
                        gphUnitRB.Checked = true;
                        break;
                    }
                case MeasurementUnit.FlowUnit.GPM:
                    {
                        gpmUnitRB.Checked = true;
                        break;
                    }
                case MeasurementUnit.FlowUnit.LPM:
                    {
                        lpmUnitRB.Checked = true;
                        break;
                    }
            }
            switch (appSettings.UserPressureUnit)
            {
                case MeasurementUnit.PressureUnit.at:
                    {
                        atUnitRB.Checked = true;
                        break;
                    }
                case MeasurementUnit.PressureUnit.bar:
                    {
                        barUnitRB.Checked = true;
                        break;
                    }
                case MeasurementUnit.PressureUnit.kPa:
                    {
                        kPaUnitRB.Checked = true;
                        break;
                    }
                case MeasurementUnit.PressureUnit.PSI:
                    {
                        PsiUnitRB.Checked = true;
                        break;
                    }
            }
            switch (appSettings.UserTemperatureUnit)
            {
                case MeasurementUnit.TemperatureUnit.Celsius:
                    {
                        cUnitRB.Checked = true;
                        break;
                    }
                case MeasurementUnit.TemperatureUnit.Fahrenheit:
                    {
                        fUnitRB.Checked = true;
                        break;
                    }
                case MeasurementUnit.TemperatureUnit.Kelvin:
                    {
                        kUnitRB.Checked = true;
                        break;
                    }
            }
            switch (appSettings.UserTorqueUnit)
            {
                case MeasurementUnit.TorqueUnit.ft_lbf:
                    {
                        ftlbfUnitRB.Checked = true;
                        break;
                    }
                case MeasurementUnit.TorqueUnit.Nm:
                    {
                        nmUnitRB.Checked = true;
                        break;
                    }
            }
            #endregion



            int oilFilterWearHrs = (int)(appSettings.OilFilterWearSeconds / 3600);
            int oilFilterWearMins = (int)(appSettings.OilFilterWearSeconds - (oilFilterWearHrs * 3600)) / 60;
            int oilFilterWearSecs = (int)(appSettings.OilFilterWearSeconds - (oilFilterWearHrs * 3600) - (oilFilterWearMins * 60));

            oilFilterWearHoursNUD.Value = oilFilterWearHrs;
            oilFilterWearMinsNUD.Value = oilFilterWearMins;
            oilFilterWearSecsNUD.Value = oilFilterWearSecs;
            oilFilterLifetimeNUD.Value = (decimal)appSettings.OilFilterLifetimeHours;
            oilFilterThresholdNUD.Value = (decimal)(appSettings.OilFilterWearAIThreshold * 100.0f);
            oilFilterChannelNUD.Value = (decimal)(appSettings.OilFilterWearAIIndex + 1);

            CompanyInfoPictureBox.Image = appSettings.CompanyInfoPicture;

            RefreshOilFilterWearPB();

            // Reset the flag
            fileLoading = false;
        }

        private void newOperatorButton_Click(object sender, EventArgs e)
        {
            if (newOperatorTextBox.Text != string.Empty)
            {
                appSettings.SystemOperatorAdd(newOperatorTextBox.Text);
                if (!DesignMode) appSettings.SaveSettingsToDisk();
                newOperatorTextBox.Text = string.Empty;
                LoadSettings();
            }

        }

        private void removeOperatorButton_Click(object sender, EventArgs e)
        {
            if (operatorsListBox.SelectedItem != null)
            {
                appSettings.SystemOperatorRemove(operatorsListBox.SelectedItem.ToString());
                if (!DesignMode) appSettings.SaveSettingsToDisk();
                LoadSettings();
            }
        }

        private void autoModeWarningCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            appSettings.ShowAutomaticModeWarning = autoModeWarningCheckBox.Checked;
            if (!DesignMode) appSettings.SaveSettingsToDisk();
        }

        private void loopModeWarningCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            appSettings.ShowLoopModeWarning = loopModeWarningCheckbox.Checked;
            if (!DesignMode) appSettings.SaveSettingsToDisk();
        }

        private void maximizedStateRB_CheckedChanged(object sender, EventArgs e)
        {
            if (maximizedStateRB.Checked)
            {
                appSettings.MainFormWindowState = FormWindowState.Maximized;
                if (!DesignMode) appSettings.SaveSettingsToDisk();
            }
        }

        private void normalStateRB_CheckedChanged(object sender, EventArgs e)
        {
            if (normalStateRB.Checked)
            {
                appSettings.MainFormWindowState = FormWindowState.Normal;
                if (!DesignMode) appSettings.SaveSettingsToDisk();
            }
        }

        private void minimizedStateRB_CheckedChanged(object sender, EventArgs e)
        {
            if (minimizedStateRB.Checked)
            {
                appSettings.MainFormWindowState = FormWindowState.Minimized;
                if (!DesignMode) appSettings.SaveSettingsToDisk();
            }
        }

        private void pressureUnitRB_CheckedChanged(object sender, EventArgs e)
        {
            // Make sure the settings file will not be written to in design mode or while the settings file is loaded from the disk
            if (DesignMode || fileLoading || !((RadioButton)sender).Checked)
            {
                return;
            }
            if (atUnitRB.Checked)
            {
                appSettings.UserPressureUnit = MeasurementUnit.PressureUnit.at;
                if (!DesignMode) appSettings.SaveSettingsToDisk();
                return;
            }
            if (barUnitRB.Checked)
            {
                appSettings.UserPressureUnit = MeasurementUnit.PressureUnit.bar;
                if (!DesignMode) appSettings.SaveSettingsToDisk();
                return;
            }
            if (kPaUnitRB.Checked)
            {
                appSettings.UserPressureUnit = MeasurementUnit.PressureUnit.kPa;
                if (!DesignMode) appSettings.SaveSettingsToDisk();
                return;
            }
            if (PsiUnitRB.Checked)
            {
                appSettings.UserPressureUnit = MeasurementUnit.PressureUnit.PSI;
                if (!DesignMode) appSettings.SaveSettingsToDisk();
                return;
            }
        }

        private void flowUnitRB_CheckedChanged(object sender, EventArgs e)
        {
            // Make sure the settings file will not be written to in design mode or while the settings file is loaded from the disk
            if (DesignMode || fileLoading || !((RadioButton)sender).Checked)
            {
                return;
            }
            if (cfmUnitRB.Checked)
            {
                appSettings.UserFlowUnit = MeasurementUnit.FlowUnit.CFM;
                if (!DesignMode) appSettings.SaveSettingsToDisk();
                return;
            }
            if (mphUnitRB.Checked)
            {
                appSettings.UserFlowUnit = MeasurementUnit.FlowUnit.cmph;
                if (!DesignMode) appSettings.SaveSettingsToDisk();
                return;
            }
            if (gphUnitRB.Checked)
            {
                appSettings.UserFlowUnit = MeasurementUnit.FlowUnit.GPH;
                if (!DesignMode) appSettings.SaveSettingsToDisk();
                return;
            }
            if (gpmUnitRB.Checked)
            {
                appSettings.UserFlowUnit = MeasurementUnit.FlowUnit.GPM;
                if (!DesignMode) appSettings.SaveSettingsToDisk();
                return;
            }
            if (lpmUnitRB.Checked)
            {
                appSettings.UserFlowUnit = MeasurementUnit.FlowUnit.LPM;
                if (!DesignMode) appSettings.SaveSettingsToDisk();
                return;
            }
        }

        private void torqueUnitRB_CheckedChanged(object sender, EventArgs e)
        {
            // Make sure the settings file will not be written to in design mode or while the settings file is loaded from the disk
            if (DesignMode || fileLoading || !((RadioButton)sender).Checked)
            {
                return;
            }
            if (nmUnitRB.Checked)
            {
                appSettings.UserTorqueUnit = MeasurementUnit.TorqueUnit.Nm;
                if (!DesignMode) appSettings.SaveSettingsToDisk();
                return;
            }
            if (ftlbfUnitRB.Checked)
            {
                appSettings.UserTorqueUnit = MeasurementUnit.TorqueUnit.ft_lbf;
                if (!DesignMode) appSettings.SaveSettingsToDisk();
                return;
            }
        }

        private void temperatureUnitRB_CheckedChanged(object sender, EventArgs e)
        {
            // Make sure the settings file will not be written to in design mode or while the settings file is loaded from the disk
            if (DesignMode || fileLoading || !((RadioButton)sender).Checked)
            {
                return;
            }
            if (cUnitRB.Checked)
            {
                appSettings.UserTemperatureUnit = MeasurementUnit.TemperatureUnit.Celsius;
                if (!DesignMode) appSettings.SaveSettingsToDisk();
                return;
            }
            if (kUnitRB.Checked)
            {
                appSettings.UserTemperatureUnit = MeasurementUnit.TemperatureUnit.Kelvin;
                if (!DesignMode) appSettings.SaveSettingsToDisk();
                return;
            }
            if (fUnitRB.Checked)
            {
                appSettings.UserTemperatureUnit = MeasurementUnit.TemperatureUnit.Fahrenheit;
                if (!DesignMode) appSettings.SaveSettingsToDisk();
                return;
            }
        }

        private void oilFilterWearSetButton_Click(object sender, EventArgs e)
        {
            // Make sure the settings file will not be written to in design mode or while the settings file is loaded from the disk
            if (DesignMode || fileLoading)
            {
                return;
            }
            appSettings.OilFilterWearSeconds = (int)(oilFilterWearHoursNUD.Value) * 3600 + ((int)oilFilterWearMinsNUD.Value) * 60 + (int)oilFilterWearSecsNUD.Value;
            appSettings.SaveSettingsToDisk();

        }

        private void RefreshOilFilterWearPB()
        {
            float filterWearHours = (float)(oilFilterWearHoursNUD.Value) + ((float)oilFilterWearMinsNUD.Value) / 60.0f + ((float)oilFilterWearSecsNUD.Value) / 3600.0f;
            float filterWear = filterWearHours / (float)oilFilterLifetimeNUD.Value;
            if (filterWear > 1.0f) filterWear = 1.0f;
            oilFilterWearProgressBar.Value = (int)(oilFilterWearProgressBar.Maximum * filterWear);
        }

        private void oilFilterWearNUD_ValueChanged(object sender, EventArgs e)
        {
            RefreshOilFilterWearPB();
        }

        private void oilFilterLifetimeNUD_ValueChanged(object sender, EventArgs e)
        {
            RefreshOilFilterWearPB();
            // Make sure the settings file will not be written to in design mode or while the settings file is loaded from the disk
            if (DesignMode || fileLoading)
            {
                return;
            }
            appSettings.OilFilterLifetimeHours = (float)oilFilterLifetimeNUD.Value;
            appSettings.SaveSettingsToDisk();
        }

        private void oilFilterThresholdNUD_ValueChanged(object sender, EventArgs e)
        {
            // Make sure the settings file will not be written to in design mode or while the settings file is loaded from the disk
            if (DesignMode || fileLoading)
            {
                return;
            }
            appSettings.OilFilterWearAIThreshold = ((float)oilFilterThresholdNUD.Value) / 100.0f;
            appSettings.SaveSettingsToDisk();
        }

        private void oilFilterChannelNUD_ValueChanged(object sender, EventArgs e)
        {
            // Make sure the settings file will not be written to in design mode or while the settings file is loaded from the disk
            if (DesignMode || fileLoading)
            {
                return;
            }
            appSettings.OilFilterWearAIThreshold = (int)oilFilterChannelNUD.Value - 1;
            appSettings.SaveSettingsToDisk();
        }

        private void CompanyInfoPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.CheckFileExists = true;
                ofd.Multiselect = false;
                ofd.Title = "Choose a new image file to be saved";
                ofd.ValidateNames = true;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    Settings.Instance.CompanyInfoPicture = new Bitmap(ofd.FileName);
                    Settings.Instance.SaveSettingsToDisk();
                }

                CompanyInfoPictureBox.Image = Settings.Instance.CompanyInfoPicture;
            }
            catch (Exception)
            {
            }

        }

    }
}
