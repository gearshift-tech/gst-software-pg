using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

using GST.Gearshift.Components.Utilities;
using GST.Gearshift.Components.Interfaces.USB;

namespace GST.Gearshift.Components.Forms.DAQ
{
  public partial class PressureDisplayConfigPanel : UserControl
  {


    internal class ComboBoxChannelItem
    {
      public int ChannelIndex = 0;

      public override string ToString()
      {
        return "Channel " + ChannelIndex.ToString("00");
      }

      public ComboBoxChannelItem(int channelIndex)
      {
        ChannelIndex = channelIndex;
      }

    }


    #region Constants



    #endregion  Constants



    #region Private fields

    private Color mBackgroundColor = Color.Transparent;
    private Color mLayoutColor1 = Color.Orange;
    private Color mLayoutColor2 = Color.Peru;

    //private DisplayChannelsSet mDisplaysSet = null;

    private List<GearShiftUsb.AIChannel> _aiChannels = new List<GearShiftUsb.AIChannel>();

    private bool mDisplayConfigModified = false;
    private bool mSelectedDisplayModified = false;

    private int mLastSelectedGaugeIndex = -1;

    private List<int> mAvailableChannels = new List<int>();

    #endregion Private fields



    #region Constructors & finalizer

    public PressureDisplayConfigPanel()
    {
      InitializeComponent();
      this.SetStyle(
          ControlStyles.SupportsTransparentBackColor |
          ControlStyles.OptimizedDoubleBuffer |
          ControlStyles.AllPaintingInWmPaint |
          ControlStyles.ResizeRedraw |
          ControlStyles.UserPaint, true);

      InitializeControlsState();

    }

    #endregion Constructors & finalizer



    #region Events


    #endregion Events



    #region Properties

    public Color PanelsColor1
    {
      get { return mLayoutColor1; }
      set 
      {
       if (value == null)
         throw new ArgumentNullException("Panel color value cannot be null");
       mLayoutColor1 = gearboxGaugePanel1.backgroundColor1 = gearboxGaugePanel2.backgroundColor1 = value;
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
       mLayoutColor2 = gearboxGaugePanel1.backgroundColor2 = gearboxGaugePanel2.backgroundColor2 = value;
       Invalidate();
      }
    }

    public Color BackgroundColor
    {
      get { return mBackgroundColor; }
      set 
      {
       if (value == null)
         throw new ArgumentNullException("Background color value cannot be null");
       mBackgroundColor = value;
       Invalidate();
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public List<GearShiftUsb.AIChannel> AIChannels
    {
      get { return _aiChannels; }
      set 
      {
       if (value == null)
         throw new ArgumentNullException("The DisplaysSet value cannot be null");
       _aiChannels = value;
       mDisplayConfigModified = false;
       InitializeControlsState();
       Invalidate();
      }
    }

    public bool DisplayConfigModified
    {
      get { return mDisplayConfigModified; }
    }

    private bool SelectedGaugeModified
    {
      get { return mSelectedDisplayModified; }
      set
      {
        mSelectedDisplayModified = value;
        mDisplayConfigModified |= value; // set if true
      }
    }

    #endregion Properties



    #region Methods

    private void InitializeControlsState()
    {
      LoadPanel1ControlsState();

      UpdateAvailableChannelsList();

      //call functions to load controls state in Panel2 
      LoadPanel2ControlsState();
      if (channelsListBox.Items.Count > 0)
      {
        channelsListBox.SelectedIndex = 0;
      }

      mDisplayConfigModified = false;
      mSelectedDisplayModified = false;
      SetAddDelButtonsState();
    }

    private void LoadPanel1ControlsState()
    {
      channelsListBox.Items.Clear();
      foreach (GearShiftUsb.AIChannel aic in _aiChannels)
      {
        channelsListBox.Items.Add(aic.Label);
      }
      // In case the list is empty, set the last selected channel to -1
      if (channelsListBox.Items.Count == 0)
      {
        mLastSelectedGaugeIndex = -1;
      }
      // In case the last selected channel was removed, keep the topmost channel selected
      if (mLastSelectedGaugeIndex >= channelsListBox.Items.Count)
      {
        mLastSelectedGaugeIndex = channelsListBox.Items.Count - 1;
      }
      // Set the selected index of the control
      channelsListBox.SelectedIndex = mLastSelectedGaugeIndex;
    }

    private void LoadPanel2ControlsState()
    {
      int selectedGauge = channelsListBox.SelectedIndex;
      if (selectedGauge == -1)
      {
        labelTextBox.Text = "";
        unitNameTextBox.Text = "";
        minValNUD.Value = 0;
        maxValNUD.Value = 0;
        inputChannelComboBox.Items.Clear();
      }
      else
      {
        GearShiftUsb.AIChannel aic = _aiChannels[selectedGauge];
        labelTextBox.Text = aic.Label;
        unitNameTextBox.Text = aic.UnitText;
        minValNUD.Value = Convert.ToDecimal(aic.MinValueUserUnit);
        maxValNUD.Value = Convert.ToDecimal(aic.MaxValueUserUnit);

        UpdateAvailableChannelsList();

        inputChannelComboBox.Items.Clear();
        for (int i = 0; i < mAvailableChannels.Count; i++)
        {
          inputChannelComboBox.Items.Add(new ComboBoxChannelItem(mAvailableChannels[i]) );
        }
        ComboBoxChannelItem selectedItem = new ComboBoxChannelItem(aic.InputIndex);
        inputChannelComboBox.Items.Add( selectedItem );
        inputChannelComboBox.Sorted = true;
        inputChannelComboBox.SelectedIndex = inputChannelComboBox.Items.IndexOf(selectedItem);

        switch (aic.ValueType)
        {
          case MeasurementUnit.ValueType.Flow:
            {
              flowRB.Checked = true;
              break;
            }
          case MeasurementUnit.ValueType.Pressure:
            {
              pressRB.Checked = true;
              break;
            }
          case MeasurementUnit.ValueType.Temperature:
            {
              tempRB.Checked = true;
              break;
            }
          case MeasurementUnit.ValueType.Torque:
            {
              torqueRB.Checked = true;
              break;
            }
          case MeasurementUnit.ValueType.InputSpeed:
            {
              inputSpeedRB.Checked = true;
              break;
            }
          case MeasurementUnit.ValueType.OutputSpeed:
            {
              outputSpeedRB.Checked = true;
              break;
            }
          case MeasurementUnit.ValueType.GearRatio:
            {
              gearRatioRB.Checked = true;
              break;
            }
          case MeasurementUnit.ValueType.PressureSwitch:
            {
              pressureSwitchRB.Checked = true;
              break;
            }
        }

      }
      SelectedGaugeModified = false;
    }

    private void UpdateAvailableChannelsList()
    {
      mAvailableChannels.Clear();
      for (int i = 0; i < GearShiftUsb.AIChannelsMaxCount; i++)
      {
        mAvailableChannels.Add(i);
      }
      for (int i = 0; i < _aiChannels.Count; i++)
      {
        mAvailableChannels.Remove(_aiChannels[i].InputIndex);//   mDisplaysSet.Channels[i].InputChannelIndex);
      }
    }

    private void SetAddDelButtonsState()
    {
      if (_aiChannels.Count < GearShiftUsb.AIChannelsMaxCount)
        gearboxGaugeAddButton.Enabled = true;
      else
        gearboxGaugeAddButton.Enabled = false;

      if (channelsListBox.SelectedIndex > -1 && channelsListBox.SelectedIndex < _aiChannels.Count)
        gearboxGaugeDeleteButton.Enabled = true;
      else
        gearboxGaugeDeleteButton.Enabled = false;
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {

      #region this must have been applied to support pseudo transparency
      if (e == null)
        return;
      if (e.Graphics == null)
        return;

      if (this.Parent != null)
      {
        GraphicsContainer cstate = e.Graphics.BeginContainer();
        e.Graphics.TranslateTransform(-this.Left, -this.Top);
        Rectangle clip = e.ClipRectangle;
        clip.Offset(this.Left, this.Top);
        PaintEventArgs pe = new PaintEventArgs(e.Graphics, clip);

        //paint the container's bg
        InvokePaintBackground(this.Parent, pe);
        //paints the container fg
        InvokePaint(this.Parent, pe);
        //restores graphics to its original state
        e.Graphics.EndContainer(cstate);
      }
      else
        base.OnPaintBackground(e);
      #endregion
    }

    private void gaugeAddButton_Click(object sender, EventArgs e)
    {
      SuspendLayout();
      UpdateAvailableChannelsList();
      // Check if any channels available
      if (mAvailableChannels.Count > 0)
      {
        // Add one more channel at the end of the list
        //DisplayChannel dispChan = new DisplayChannel();
        GearShiftUsb.AIChannel aic = new GearShiftUsb.AIChannel();
        aic.InputIndex = mAvailableChannels[0];
        aic.Label = "Channel " + aic.InputIndex.ToString("00");
        _aiChannels.Add(aic);
        //mDisplaysSet.AddChannelAt(dispChan, mDisplaysSet.ChannelsCount);
        // Update controls state
        LoadPanel1ControlsState();
        // Select the added channel
        channelsListBox.SelectedIndex = channelsListBox.Items.Count - 1;
      }
      else
      {
        // If no more channels available display error
        global::Soko.Common.Forms.MessageBox.ShowWarning("GearShift",
            "Invalid operation warning",
            "You cannot add more displays than input channels avaliable ("
            + GearShiftUsb.AIChannelsMaxCount.ToString() + ")",
            Soko.Common.Forms.MessageBoxButtons.OK);
      }
      SetAddDelButtonsState();
      ResumeLayout();
    }

    private void deleteButton_Click(object sender, EventArgs e)
    {
      if (channelsListBox.SelectedIndex > -1 && channelsListBox.SelectedIndex < _aiChannels.Count)
      {
        SuspendLayout();
        // Remove the channel
        _aiChannels.RemoveAt(channelsListBox.SelectedIndex);
        //mDisplaysSet.RemoveChannelAt(channelsListBox.SelectedIndex);
        // Update controls state
        LoadPanel1ControlsState();
        LoadPanel2ControlsState();
        SetAddDelButtonsState();
        ResumeLayout();
      }
    }


    #endregion Methods

    private void labelTextBox_TextChanged(object sender, EventArgs e)
    {
      if (channelsListBox.SelectedIndex > -1)
      {
        _aiChannels[channelsListBox.SelectedIndex].Label = labelTextBox.Text;
        // Update the channels listbox content
        LoadPanel1ControlsState();
      }
    }

    private void inputChannelComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (channelsListBox.SelectedIndex > -1)
      {
        _aiChannels[channelsListBox.SelectedIndex].InputIndex = ((ComboBoxChannelItem)(inputChannelComboBox.SelectedItem)).ChannelIndex;
      }
    }

    private void pressRB_CheckedChanged(object sender, EventArgs e)
    {
      if (channelsListBox.SelectedIndex > -1 && pressRB.Checked)
      {
        _aiChannels[channelsListBox.SelectedIndex].ValueType = MeasurementUnit.ValueType.Pressure;
      }
    }

    private void tempRB_CheckedChanged(object sender, EventArgs e)
    {
      if (channelsListBox.SelectedIndex > -1 && tempRB.Checked)
      {
        _aiChannels[channelsListBox.SelectedIndex].ValueType = MeasurementUnit.ValueType.Temperature;
      }
    }

    private void flowRB_CheckedChanged(object sender, EventArgs e)
    {
      if (channelsListBox.SelectedIndex > -1 && flowRB.Checked)
      {
        _aiChannels[channelsListBox.SelectedIndex].ValueType = MeasurementUnit.ValueType.Flow;
      }
    }

    private void torqueRB_CheckedChanged(object sender, EventArgs e)
    {
      if (channelsListBox.SelectedIndex > -1 && torqueRB.Checked)
      {
        _aiChannels[channelsListBox.SelectedIndex].ValueType = MeasurementUnit.ValueType.Torque;
      }
    }

    private void inputSpeedRB_CheckedChanged(object sender, EventArgs e)
    {
      if (channelsListBox.SelectedIndex > -1 && inputSpeedRB.Checked)
      {
        int inputSpeedChannelsCount = 0;
        foreach (GearShiftUsb.AIChannel aic in _aiChannels)
        {
          if (aic.ValueType == MeasurementUnit.ValueType.InputSpeed)
            inputSpeedChannelsCount++;
        }
        if (inputSpeedChannelsCount > 0 && _aiChannels[channelsListBox.SelectedIndex].ValueType != MeasurementUnit.ValueType.InputSpeed)
        {
          // If at least one input speed already defined display error
          global::Soko.Common.Forms.MessageBox.ShowWarning("GearShift",
              "Invalid operation warning",
              "You cannot define more than one input speed channel",
              Soko.Common.Forms.MessageBoxButtons.OK);
          // Set back the default value
          pressRB.Checked = true;
        }
        else
        {
          _aiChannels[channelsListBox.SelectedIndex].ValueType = MeasurementUnit.ValueType.InputSpeed;
        }
      }
    }

    private void outputSpeedRB_CheckedChanged(object sender, EventArgs e)
    {
      if (channelsListBox.SelectedIndex > -1 && outputSpeedRB.Checked)
      {
        int outputSpeedChannelsCount = 0;
        foreach (GearShiftUsb.AIChannel aic in _aiChannels)
        {
          if (aic.ValueType == MeasurementUnit.ValueType.OutputSpeed)
            outputSpeedChannelsCount++;
        }
        if (outputSpeedChannelsCount >= 3 && _aiChannels[channelsListBox.SelectedIndex].ValueType != MeasurementUnit.ValueType.OutputSpeed)
        {
          // If at least one input speed already defined display error
          global::Soko.Common.Forms.MessageBox.ShowWarning("GearShift",
              "Invalid operation warning",
              "You cannot define more than three output speed channels",
              Soko.Common.Forms.MessageBoxButtons.OK);
          // Set back the default value
          pressRB.Checked = true;
        }
        else
        {
          _aiChannels[channelsListBox.SelectedIndex].ValueType = MeasurementUnit.ValueType.OutputSpeed;
        }
      }
    }

    private void gearRatioRB_CheckedChanged(object sender, EventArgs e)
    {
      if (channelsListBox.SelectedIndex > -1 && gearRatioRB.Checked)
      {
        int GearRatioChannelsCount = 0;
        foreach (GearShiftUsb.AIChannel aic in _aiChannels)
        {
          if (aic.ValueType == MeasurementUnit.ValueType.GearRatio)
            GearRatioChannelsCount++;
        }
        if (GearRatioChannelsCount > 0 && _aiChannels[channelsListBox.SelectedIndex].ValueType != MeasurementUnit.ValueType.GearRatio)
        {
          // If at least one input speed already defined display error
          global::Soko.Common.Forms.MessageBox.ShowWarning("GearShift",
              "Invalid operation warning",
              "You cannot define more than one gear ratio channel",
              Soko.Common.Forms.MessageBoxButtons.OK);
          // Set back the default value
          pressRB.Checked = true;
        }
        else
        {
          _aiChannels[channelsListBox.SelectedIndex].ValueType = MeasurementUnit.ValueType.GearRatio;
        }
        
      }
    }

    private void pressureSwitchRB_CheckedChanged(object sender, EventArgs e)
    {
      if (channelsListBox.SelectedIndex > -1 && pressureSwitchRB.Checked)
      {
        _aiChannels[channelsListBox.SelectedIndex].ValueType = MeasurementUnit.ValueType.PressureSwitch;

      }
    }

    private void channelsListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      mLastSelectedGaugeIndex = channelsListBox.SelectedIndex;
      LoadPanel2ControlsState();
    }

    private void maxValNUD_ValueChanged(object sender, EventArgs e)
    {
      if (channelsListBox.SelectedIndex > -1)
      {
        _aiChannels[channelsListBox.SelectedIndex].MaxValueUserUnit = Convert.ToSingle(maxValNUD.Value);
      }
    }

    private void minValNUD_ValueChanged(object sender, EventArgs e)
    {
      if (channelsListBox.SelectedIndex > -1)
      {
        _aiChannels[channelsListBox.SelectedIndex].MinValueUserUnit = Convert.ToSingle(minValNUD.Value);
      }
    }


  }
}
