using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Soko.Common.Common;
using GST.Gearshift.Components.Interfaces.USB;
using  GST.Gearshift.Components.Forms;

using GST.ZF6.Components.Interfaces.MechShifterUSB;

namespace GST.Gearshift.Components.Forms.DAQ
{

  /// <remarks>
  /// Class prividing simple graphical interface to GearboxConfig
  /// </remarks>
  public partial class GearboxConfigPanel : UserControl
  {


    #region Constants



    #endregion  Constants



    #region Private fields


    private Color mLayoutColor1 = Color.Orange;
    private Color mLayoutColor2 = Color.Peru;

    private GearboxConfig mGearboxConfig = null;

    //note that this holds only if Panel2 has been modifed
    private bool mGearboxConfigModified = false;

    private bool mEmbeddedModeEnabled = false;

    #endregion Private fields



    #region Constructors & finalizer


    /// <summary>
    /// Default constructor
    /// </summary>
    public GearboxConfigPanel()
    {
      InitializeComponent();
      this.SetStyle(
                    ControlStyles.SupportsTransparentBackColor |
                    ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.ResizeRedraw |
                    ControlStyles.UserPaint, true);
      mGearboxConfig = new GearboxConfig();
      LoadControlsState();
    }


    #endregion Constructors & finalizer



    #region Events

    /// <summary>
    /// Raised only if EmbeddedModeEnabled, after the save button is clicked
    /// </summary>
    public EventHandler SaveButtonClickedEvent;

   #endregion Events



    #region Properties

    public Color PanelsColor1
    {
      get { return mLayoutColor1; }
      set
      {
        if (value == null)
          throw new ArgumentNullException("Panel color value cannot be null");
        mLayoutColor1 = gearboxMainPanel1.backgroundColor1 = gearboxMainPanel2.backgroundColor1
        = currentDisplayConfigPanel.PanelsColor1 = pressureDisplayConfigPanel.PanelsColor1 = value;
        Invalidate();
      }
    }

    public Color PanelsColor2
    {
      get { return mLayoutColor2; }
      set
      {
        if (value == null)
          throw new ArgumentNullException("Panel color value cannot be null");
        mLayoutColor2 = gearboxMainPanel1.backgroundColor2 = gearboxMainPanel2.backgroundColor2
        = currentDisplayConfigPanel.PanelsColor2 = pressureDisplayConfigPanel.PanelsColor2 = value;
        Invalidate();
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GearboxConfig GearboxConfig
    {
      get { return mGearboxConfig; }
      set
      {
        mGearboxConfig = Soko.Common.Common.ObjectCopier.Clone(value);
        LoadControlsState();
      }
    }

    /// <summary>
    /// Gets if current GearboxConfig has been modified
    /// </summary>
    /// <returns></returns>
    public bool GearboxConfigModified()
    {
      return mGearboxConfigModified;
    }

    public bool EmbeddedModeEnabled
    {
      get {return mEmbeddedModeEnabled;}
      set
      {
        mEmbeddedModeEnabled = value;
        if (mEmbeddedModeEnabled == true)
        {

        }
      }

    }

    #endregion Properties



    #region Methods


    /// <summary>
    /// Loads controls state from current gearbox config
    /// </summary>
    private void LoadControlsState()
    {
        SuspendLayout();
        LoadGbxTypeComboBox();
        gearboxNameTextbox.Text = mGearboxConfig.Name;
        GearboxManufacturerTextbox.Text = mGearboxConfig.Manufacturer;
        GearboxModelTextbox.Text = mGearboxConfig.Model;
        gearboxPicturePictureBox.Image = mGearboxConfig.Picture;
        pressureDisplayConfigPanel.AIChannels = mGearboxConfig._analogueInputs;
        currentDisplayConfigPanel.DisplaysSet = mGearboxConfig.CurrentDisplayChannelsSet;
        gearboxFrequencyNUD.Value = mGearboxConfig.PwmFrequencyHz;
        pressureVariationToleranceNUD.Value = System.Convert.ToDecimal(mGearboxConfig.PressureVariationTolerance * 100);
        ignoreValueLessThanNUD.Value = System.Convert.ToDecimal(Math.Round(mGearboxConfig.IgnorePressureLessThan_UserUnit, 2));
        ignoreValueLessThanLabel.Text = "Ignore pressure below [" + GST.Gearshift.Components.Utilities.MeasurementUnit.GetPressureUserUnitString() + "]";

        int sltIdxTmp = GbxTypeComboBox.FindString(Enum.GetName(typeof(GearboxControllerType), mGearboxConfig.ControllerType));
        GbxTypeComboBox.SelectedIndex = sltIdxTmp;
            
        mGearboxConfigModified = false;
        ResumeLayout( false );
    }

    private void LoadGbxTypeComboBox()
    {
      GbxTypeComboBox.Items.Clear();
      // Add all items from the GearboxControllerType enum
      GbxTypeComboBox.Items.AddRange(Enum.GetNames(typeof(GearboxControllerType)));
    }

    public bool IsModified()
    {
      return (pressureDisplayConfigPanel.DisplayConfigModified ||
              currentDisplayConfigPanel.DisplayConfigModified ||
              mGearboxConfigModified);
    }

    public void RequestInternalSave()
    {
      if (mGearboxConfig.Name == "")
      {
        DialogResult dr = Soko.Common.Forms.MessageBox.ShowWarning("GearShift",
                                                              "Empty field error",
                                                              "The gearbox name field cannot be empty. Please correct this before editing test script, gearbox configuration was not loaded to script!",
         Soko.Common.Forms.MessageBoxButtons.OK);
        return;
      }


      // Check if the file name is ok
      char[] invalidFileChars = Path.GetInvalidFileNameChars();
      int idx = mGearboxConfig.Name.IndexOfAny(invalidFileChars);
      if (idx != -1)
      {
        DialogResult dr = Soko.Common.Forms.MessageBox.ShowWarning("GearShift",
                                                            "Invalid gearbox name",
                                                            "The gearbox name field cannot contain characters invalid \nfor a file path, such as: \\ / : * ? \" < > | " +
                                                            "\nPlease correct this before editing test script, gearbox configuration was not loaded to script!",
                                                            Soko.Common.Forms.MessageBoxButtons.OK);
        return;
      }

      if (SaveButtonClickedEvent != null )
        {
          SaveButtonClickedEvent(this, EventArgs.Empty);
        }
      }


    private void PlaceMainPanel()
    {
      int xpos;
      int ypos;
      if (this.Width > MainPanel.Width)// && this.Height > MainPanel.Height)
        xpos = (this.Width - MainPanel.Width) / 2;
      else
        xpos = 0;
     if (this.Height > MainPanel.Height)
        ypos = (this.Height - MainPanel.Height) / 2;
      else
        ypos = 0;
     MainPanel.Location = new Point(xpos, ypos);
    }
    /// <summary>
    /// When background is painted
    /// </summary>
    /// <param name="e"></param>
    protected override void OnPaintBackground( PaintEventArgs e )
    {

      #region this must have been applied to support pseudo transparency
      if ( e == null )
        return;
      if ( e.Graphics == null )
        return;

      if ( this.Parent != null )
      {
        GraphicsContainer cstate = e.Graphics.BeginContainer();
        e.Graphics.TranslateTransform( -this.Left, -this.Top );
        Rectangle clip = e.ClipRectangle;
        clip.Offset( this.Left, this.Top );
        PaintEventArgs pe = new PaintEventArgs( e.Graphics, clip );

        //paint the container's bg
        InvokePaintBackground( this.Parent, pe );
        //paints the container fg
        InvokePaint( this.Parent, pe );
        //restores graphics to its original state
        e.Graphics.EndContainer( cstate );
      }
      else
        base.OnPaintBackground( e );
      #endregion
    }

    /// <summary>
    /// When gearbox name changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void gearboxNameTextbox_TextChanged( object sender, EventArgs e )
    {
      mGearboxConfig.Name = gearboxNameTextbox.Text;
      mGearboxConfigModified = true;
    }

    /// <summary>
    /// When gearbox manufacturer changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void GearboxManufacturerTextbox_TextChanged( object sender, EventArgs e )
    {
      mGearboxConfig.Manufacturer = GearboxManufacturerTextbox.Text;
      mGearboxConfigModified = true;
    }

    /// <summary>
    /// When gearbox model changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void GearboxModelTextbox_TextChanged( object sender, EventArgs e )
    {
      mGearboxConfig.Model = GearboxModelTextbox.Text;
      mGearboxConfigModified = true;
    }

    /// <summary>
    /// When picture clicked
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void gearboxPicturePictureBox_Click( object sender, EventArgs e )
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.DefaultExt = "PNG";
      openFileDialog.Filter = "PNG pictures (*.PNG)|*.PNG";
      if ( openFileDialog.ShowDialog() == DialogResult.OK )
      {
        if ( openFileDialog.CheckPathExists )
        {
          Bitmap newPicture = new Bitmap( openFileDialog.FileName );
          mGearboxConfig.Picture = newPicture;
          LoadControlsState();
        }
      }
    }

    /// <summary>
    /// When new config is being created
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void gearboxFileNewButton_Click( object sender, EventArgs e )
    {
        DialogResult dr = global::Soko.Common.Forms.MessageBox.ShowWarning("GearShift",
       "Data loss warning",
       "If you continue, all the current gearbox config will be wiped out.\n"
       + "Are you sure you want to continue?",
       Soko.Common.Forms.MessageBoxButtons.OKCancel);
        if (dr == DialogResult.OK)
        {
          mGearboxConfig = new GearboxConfig();
          LoadControlsState();
        }
    }

    /// <summary>
    /// When current config is saved
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void gearboxFileSaveButton_Click(object sender, EventArgs e)
    {
      if (mGearboxConfig.Name == string.Empty)
      {
        DialogResult dr = Soko.Common.Forms.MessageBox.ShowCritical("GearShift",
                                                            "Empty field error",
                                                            "The gearbox name field cannot be empty. Please fill it and save again\n",
                                                            Soko.Common.Forms.MessageBoxButtons.OK);
        return;
      }

      // Check if the file name is ok
      char[] invalidFileChars = Path.GetInvalidFileNameChars();
      int idx = mGearboxConfig.Name.IndexOfAny(invalidFileChars);
      if (idx != -1)
      {
        DialogResult dr = Soko.Common.Forms.MessageBox.ShowWarning("GearShift",
                                                            "Invalid gearbox name",
                                                            "The gearbox name field cannot contain characters invalid \nfor a file path, such as: \\ / : * ? \" < > | " +
                                                            "\nPlease remove them and save again\n",
                                                            Soko.Common.Forms.MessageBoxButtons.OK);
        return;
      }


      string dirToSave = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\GearShift Technologies\\GearShift\\Installed gearboxes" + "\\"
                                                      + mGearboxConfig.Name;
      string pathToSave = dirToSave + "\\" + mGearboxConfig.Name + ".gcf";
      if (mGearboxConfig.Filename == pathToSave)
      {
        mGearboxConfig.SaveXml(pathToSave);
        mGearboxConfigModified = false;
      }
      else
      {
        if (File.Exists(pathToSave))
        {
          DialogResult dr = global::Soko.Common.Forms.MessageBox.ShowWarning("GearShift", "Data loss warning",
                                                              "There already exists a gearbox with the name you specified\n"
                                                              + "Do you really want to overwrite the existing gearbox?",
                                                              Soko.Common.Forms.MessageBoxButtons.YesNo);
          if (dr == DialogResult.Yes)
          {
            mGearboxConfig.SaveXml(pathToSave);
            mGearboxConfigModified = false;
          }
          else
          {
            return;
          }
        }
        else
        {
          if (!Directory.Exists(dirToSave))
          {
            Directory.CreateDirectory(dirToSave);
          }
          mGearboxConfig.SaveXml(pathToSave);
          mGearboxConfigModified = false;
          Soko.Common.Forms.MessageBox.ShowInfo("GearShift", "Operation successfull",
                                                              "The gearbox config has been succesfully exported",
                                                              Soko.Common.Forms.MessageBoxButtons.OK);
        }
      }

    }


    /// <summary>
    /// When panel is loaded
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param> 
    private void GearboxConfigPanel_Load( object sender, EventArgs e )
    {
      PlaceMainPanel();
    }

    /// <summary>
    /// When panel is resized
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void GearboxConfigPanel_Resize( object sender, EventArgs e )
    {
      PlaceMainPanel();
    }

    private void importConfigButton_Click(object sender, EventArgs e)
    {
      TestScript ts = new TestScript();
      ts.Gearbox = null;
      ImportGearboxConfigForm igcf = new ImportGearboxConfigForm(ts);
      igcf.ShowDialog();
      if (ts.GearboxCorrect)
      {
        mGearboxConfig = ts.Gearbox;
        LoadControlsState();
      }

    }

    #endregion Methods

    private void gearboxFrequencyNUD_ValueChanged(object sender, EventArgs e)
    {
      mGearboxConfig.PwmFrequencyHz = System.Convert.ToInt32(gearboxFrequencyNUD.Value);
      mGearboxConfigModified = true;
    }

    private void pressureVariationToleranceNUD_ValueChanged(object sender, EventArgs e)
    {
      mGearboxConfig.PressureVariationTolerance = System.Convert.ToSingle(pressureVariationToleranceNUD.Value / 100);
      mGearboxConfigModified = true;
    }

    private void ignoreValueLessThanNUD_ValueChanged(object sender, EventArgs e)
    {
      mGearboxConfig.IgnorePressureLessThan_UserUnit = (float)Math.Round(System.Convert.ToSingle(ignoreValueLessThanNUD.Value), 2);
      mGearboxConfigModified = true;
    }

    private void GbxTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      string chuj = GbxTypeComboBox.SelectedItem.ToString();
      if (GbxTypeComboBox.SelectedIndex >= 0 && GbxTypeComboBox.SelectedItem.ToString() != string.Empty)
      {
        mGearboxConfig.ControllerType = (GearboxControllerType)Enum.Parse(typeof(GearboxControllerType), GbxTypeComboBox.SelectedItem.ToString(), true);
        mGearboxConfigModified = true;
      }
    }



  }
}
