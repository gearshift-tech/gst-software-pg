using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using System.IO;

using Soko.Common.Controls;
using GST.Gearshift.Components.Forms.DAQ;

namespace GearShift
{
    public partial class GearShiftEditorForm : Form
    {

        #region Constants



        #endregion  Constants



        #region Private fields



        #endregion Private fields



        #region Constructors & finalizer

        public GearShiftEditorForm()
        {
            // Before doing anything else, load the singleton settings file
            GST.Gearshift.Components.Utilities.Settings appSettings = GST.Gearshift.Components.Utilities.Settings.Instance;
            appSettings.LoadSettingsFromDisk();

            string lol = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            InitializeComponent();

            // Set the main window state basing on the app settings
            this.WindowState = appSettings.MainFormWindowState;

            // Set the window title to have the version number
            this.Text = "GearShift Editor for " + GST.Gearshift.Components.Utilities.Settings.MainFormTitle;
            

            //HideAllDocuments();


        }


        #endregion Constructors & finalizer



        #region Events


        #endregion Events



        #region Properties



        #endregion Properties



        #region Methods



        #endregion Methods


    }
}
