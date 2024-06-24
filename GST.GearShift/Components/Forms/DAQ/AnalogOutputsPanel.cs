using Syncfusion.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GST.Gearshift.Components.Interfaces.USB;

namespace GST.Gearshift.Components.Forms.DAQ
{
  public partial class AOsPanel : Form
  {



    #region Constants

    private int mNUDsActiveYPos = 312;
    private int mNUDsInactiveYPos = 500;

    #endregion  Constants



    #region Private fields

    private Measurement mMeasurement = new Measurement();

    private bool mAOsValueChanged = false;

    private int mCalibrationStage = 0;

    #endregion Private fields



    #region Constructors & finalizer

    public AOsPanel(Measurement msrmnt)
    {
      InitializeComponent();

      mMeasurement = msrmnt;

      mMeasurement.StartAOsTest();

      updateTmr.Enabled = true;

    }

    #endregion Constructors & finalizer



    #region Events



    #endregion Events



    #region Properties



    #endregion Properties



    #region Methods

    private void updateTmr_Tick(object sender, EventArgs e)
    {
      if (mAOsValueChanged)
      {
        mAOsValueChanged = false;
        mMeasurement.SetAOsValuesPerc((UInt32)AO1Trackbar.Value, (UInt32)AO2Trackbar.Value);
      }
    }

    private void AO1Trackbar_ValueChanged(object sender, decimal value)
    {
      mAOsValueChanged = true;

      AO1PercValueLabel.Text = AO1Trackbar.Value.ToString() + " %";
      if (GST.Gearshift.Components.Utilities.Settings.Instance.RPM_AO_multiplier > 0)
      {
        AO1ValueLabel.Text = ((10.24f / GST.Gearshift.Components.Utilities.Settings.Instance.RPM_AO_multiplier) * (float)AO1Trackbar.Value).ToString("0000.0 RPM");
      }
      else
      {
        AO1ValueLabel.Text = "? RPM";
      }
      //AO1ValueLabel.Text = 
    }

    private void AO2Trackbar_ValueChanged(object sender, decimal value)
    {
      mAOsValueChanged = true;

      AO2PercValueLabel.Text = AO2Trackbar.Value.ToString() + " %";
      if (GST.Gearshift.Components.Utilities.Settings.Instance.Load_AO_multiplier > 0)
      {
        AO2ValueLabel.Text = ((10.24f / GST.Gearshift.Components.Utilities.Settings.Instance.Load_AO_multiplier) * (float)AO2Trackbar.Value).ToString("00.00 A");
      }
      else
      {
        AO2ValueLabel.Text = "? A";
      }
    }

    #endregion Methods

    private void AOsPanel_FormClosing(object sender, FormClosingEventArgs e)
    {
      updateTmr.Enabled = false;
      mMeasurement.StopAOsTest();
    }

    private void gearboxFileSaveButton_Click(object sender, EventArgs e)
    {
      switch (mCalibrationStage)
      {
        case 0:
          {
            string message = "You are about to calibrate the Analog Outputs multipliers.\n\nPlease make sure you know what you are doing. If you have any doubts, please contact GearShift Technologies support for further informations\n\n\n Are you sure you want to continue?";
            Soko.Common.Forms.MessageBox msgbx = new Soko.Common.Forms.MessageBox("GearShift", "Warning", message);
            msgbx.RemoveButtons();
            msgbx.AddButton(DialogResult.OK, "Continue");
            msgbx.AddButton(DialogResult.Cancel, "Cancel");
            //msgbx.ButtonsAligment = HorizontalAlignment.Center;
            msgbx.MessageBoxIcon = Soko.Common.Forms.MessageBoxIcon.Warning;
            if (msgbx.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
              message = "Use sliders to set both motor RPM and Load Current to the maximal practically used value. Normally it would be around 6000 RPM and 4 A, but this may vary depending on your setup";
              message += "\n\nPlease note that this usually will NOT be 100% of the slider. Please contact GearShift Technologies support if you have any problems and DO NOT continue unless you know what you are doing.";
              msgbx = new Soko.Common.Forms.MessageBox("GearShift", "Prepare", message);
              msgbx.RemoveButtons();
              msgbx.AddButton(DialogResult.OK, "Continue");
              msgbx.AddButton(DialogResult.Cancel, "Cancel");
              //msgbx.ButtonsAligment = HorizontalAlignment.Center;
              msgbx.MessageBoxIcon = Soko.Common.Forms.MessageBoxIcon.Information;
              if (msgbx.ShowDialog() == System.Windows.Forms.DialogResult.OK)
              {
                calibrateButton.Image = GST.Gearshift.Components.Properties.Resources.AnalogOutputsPanel_clean;
                calibrateButton.Text = "Continue to the next step";
                mCalibrationStage = 1;
              }
            }
            break;
          }
        case 1:
          {
            string message = "Read the motor speed and load current and put these values to the corresponding green fields";
            Soko.Common.Forms.MessageBox msgbx = new Soko.Common.Forms.MessageBox("GearShift", "Prepare", message);
            msgbx.RemoveButtons();
            msgbx.AddButton(DialogResult.OK, "Continue");
            msgbx.AddButton(DialogResult.Cancel, "Cancel");
            //msgbx.ButtonsAligment = HorizontalAlignment.Center;
            msgbx.MessageBoxIcon = Soko.Common.Forms.MessageBoxIcon.Warning;
            if (msgbx.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
              mCalibrationStage = 2;
              AO1NUD.Location = new Point(AO1NUD.Location.X, mNUDsActiveYPos);
              AO2NUD.Location = new Point(AO2NUD.Location.X, mNUDsActiveYPos);
              calibrateButton.Image = GST.Gearshift.Components.Properties.Resources.AnalogOutputsPanel_clean;
              calibrateButton.Text = "I'm ready, let's finish!";
              AO1Trackbar.Enabled = false;
              AO2Trackbar.Enabled = false;
            }
            else
            {
              mCalibrationStage = 0;
              AO1NUD.Location = new Point(AO1NUD.Location.X, mNUDsInactiveYPos);
              AO2NUD.Location = new Point(AO2NUD.Location.X, mNUDsInactiveYPos);
              calibrateButton.Image = GST.Gearshift.Components.Properties.Resources.AnalogOutputsPanel_Calibration_64x64;
              calibrateButton.Text = "Calibrate the system for your dyno";
              AO1Trackbar.Enabled = true;
              AO2Trackbar.Enabled = true;
            }
            break;
          }
        case 2:
          {
            GST.Gearshift.Components.Utilities.Settings appSettings = GST.Gearshift.Components.Utilities.Settings.Instance;
            appSettings.RPM_AO_multiplier = ((float)AO1Trackbar.Value / (float)AO1NUD.Value) * 10.24f;
            appSettings.Load_AO_multiplier = ((float)AO2Trackbar.Value / (float)AO2NUD.Value) * 10.24f;
            if (!DesignMode) appSettings.SaveSettingsToDisk();

            string message = "You have successfully configured AOs multipliers: ";
            message += "\n Motor RPM:    " + appSettings.RPM_AO_multiplier.ToString("0.000000");
            message += "\n Load current: " + appSettings.Load_AO_multiplier.ToString("0.000000");
            message += "\n\nYou can adjust these properties in application settings.";
            Soko.Common.Forms.MessageBox msgbx = new Soko.Common.Forms.MessageBox("GearShift", "Finished!", message);
            msgbx.RemoveButtons();
            msgbx.AddButton(DialogResult.OK, "Finish");
            //msgbx.ButtonsAligment = HorizontalAlignment.Center;
            msgbx.MessageBoxIcon = Soko.Common.Forms.MessageBoxIcon.Information;
            msgbx.ShowDialog();

            mCalibrationStage = 0;
            AO1NUD.Location = new Point(AO1NUD.Location.X, mNUDsInactiveYPos);
            AO2NUD.Location = new Point(AO2NUD.Location.X, mNUDsInactiveYPos);
            calibrateButton.Image = GST.Gearshift.Components.Properties.Resources.AnalogOutputsPanel_Calibration_64x64;
            calibrateButton.Text = "Calibrate the system for your dyno";
            AO1Trackbar.Enabled = true;
            AO2Trackbar.Enabled = true;
            break;
          }
      }
    }




  }
}
