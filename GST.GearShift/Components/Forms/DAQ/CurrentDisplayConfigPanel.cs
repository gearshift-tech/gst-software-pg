using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

using GST.Gearshift.Components.Interfaces.USB;

namespace GST.Gearshift.Components.Forms.DAQ
{
  public partial class CurrentDisplayConfigPanel : UserControl
  {


    internal class ComboBoxChannelItem
    {
      public int ChannelIndex = 0;

      public override string ToString()
      {
        return "Channel " + ChannelIndex.ToString();
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

    private DisplayChannelsSet mDisplaysSet = null;

    private bool mDisplayConfigModified = false;
    private bool mSelectedDisplayModified = false;

    private int mLastSelectedGaugeIndex = -1;

    private List<int> mAvailableChannels = new List<int>();

    #endregion Private fields



    #region Constructors & finalizer

    public CurrentDisplayConfigPanel()
    {
      InitializeComponent();
      this.SetStyle(
          ControlStyles.SupportsTransparentBackColor |
          ControlStyles.OptimizedDoubleBuffer |
          ControlStyles.AllPaintingInWmPaint |
          ControlStyles.ResizeRedraw |
          ControlStyles.UserPaint, true);

      mDisplaysSet = new DisplayChannelsSet();
      mDisplaysSet.ChannelsMaxCount = 9;

      inputChannelComboBox.Items.Clear();
      for ( int i = 0; i < 9; i++ )
        inputChannelComboBox.Items.Add( "Channel " + i.ToString() );

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
    public DisplayChannelsSet DisplaysSet
    {
      get { return mDisplaysSet; }
      set 
      {
       if (value == null)
         throw new ArgumentNullException("The DisplaysSet value cannot be null");
       mDisplaysSet = value;
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
      for (int i = 0; i < mDisplaysSet.ChannelsCount; i++)
      {
        channelsListBox.Items.Add(mDisplaysSet.Channels[i].Label);
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
        inputChannelComboBox.SelectedIndex = -1;
        initialTestDutyCycleNUD.Value = 0;
        nominalCurrentToleranceNUD.Value = 0;
        nominalCurrentValueNUD.Value = 0;
        upperDriveRB.Checked = false;
        lowerDriveRB.Checked = true;
        inputChannelComboBox.Items.Clear();
      }
      else
      {
        DisplayChannel dispChan = mDisplaysSet.Channels[selectedGauge];
        labelTextBox.Text = dispChan.Label;
        //if (dispChan.UnitName == "[Unit]")
        //{
        //  unitNameTextBox.Text = "[A]";
        //}
        //else
        //{
        //  unitNameTextBox.Text = dispChan.UnitName;
        //}
        //minValNUD.Value = Convert.ToDecimal(dispChan.MinValue);
        //maxValNUD.Value = Convert.ToDecimal(dispChan.MaxValue);
        //Console.WriteLine("min: " + dispChan.MinValue.ToString() + " max: " + dispChan.MaxValue.ToString() );
        minValNUD.ReadOnly = true;
        maxValNUD.ReadOnly = true;
        UpdateAvailableChannelsList();
        inputChannelComboBox.Items.Clear();
        for (int i = 0; i < mAvailableChannels.Count / 2; i++)
        {
          inputChannelComboBox.Items.Add(new ComboBoxChannelItem(mAvailableChannels[i]));
        }
        ComboBoxChannelItem selectedItem;
        if (dispChan.InputChannelIndex > 8)
        {
          selectedItem = new ComboBoxChannelItem(dispChan.InputChannelIndex - 9);
        }
        else
        {
          selectedItem = new ComboBoxChannelItem(dispChan.InputChannelIndex);
        }
        inputChannelComboBox.Items.Add(selectedItem);
        inputChannelComboBox.Sorted = true;
        inputChannelComboBox.SelectedIndex = inputChannelComboBox.Items.IndexOf(selectedItem);

        initialTestDutyCycleNUD.Value = dispChan.InitTestDutyCycle;
        nominalCurrentToleranceNUD.Value = dispChan.NominalCurrentTolerance;
        nominalCurrentValueNUD.Value = Convert.ToDecimal(dispChan.NominalCurrent);
        upperDriveRB.Checked = dispChan.IsUpperChannel;
        lowerDriveRB.Checked = !upperDriveRB.Checked;
      }
    }

    private void SetAddDelButtonsState()
    {
      if (mDisplaysSet.ChannelsCount < mDisplaysSet.ChannelsMaxCount)
        gearboxGaugeAddButton.Enabled = true;
      else
        gearboxGaugeAddButton.Enabled = false;

      if (channelsListBox.SelectedIndex > -1 &&
        channelsListBox.SelectedIndex < mDisplaysSet.ChannelsCount)
        gearboxGaugeDeleteButton.Enabled = true;
      else
        gearboxGaugeDeleteButton.Enabled = false;
    }

    private void UpdateAvailableChannelsList()
    {
      mAvailableChannels.Clear();
      for (int i = 0; i < 18; i++)
      {
        mAvailableChannels.Add(i);
      }
      for (int i = 0; i < mDisplaysSet.ChannelsCount; i++)
      {
        int currIndex = mDisplaysSet.Channels[i].InputChannelIndex;
        if (currIndex < 9)
        {
          mAvailableChannels.Remove(currIndex);
          mAvailableChannels.Remove(currIndex + 9);
        }
        else
        {
          mAvailableChannels.Remove(currIndex - 9);
          mAvailableChannels.Remove(currIndex);
        }    
      }
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
        DisplayChannel dispChan = new DisplayChannel();
        dispChan.InputChannelIndex = mAvailableChannels[0];
        dispChan.Label = "Channel " + dispChan.InputChannelIndex.ToString();
        mDisplaysSet.AddChannelAt(dispChan, mDisplaysSet.ChannelsCount);
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
            + mDisplaysSet.ChannelsMaxCount.ToString() + ")",
            Soko.Common.Forms.MessageBoxButtons.OK);
      }
      SetAddDelButtonsState();
      ResumeLayout();
    }

    private void deleteButton_Click(object sender, EventArgs e)
    {
      if (channelsListBox.SelectedIndex > -1 && channelsListBox.SelectedIndex < mDisplaysSet.ChannelsCount)
      {
        SuspendLayout();
        // Remove the channel
        mDisplaysSet.RemoveChannelAt(channelsListBox.SelectedIndex);
        // Update controls state
        LoadPanel1ControlsState();
        LoadPanel2ControlsState();
        SetAddDelButtonsState();
        ResumeLayout();
      }
    }

    private void driverCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      SelectedGaugeModified = true;
    }

    private void inputChannelComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (channelsListBox.SelectedIndex > -1)
        SelectedGaugeModified = true;
    }

    #endregion Methods

    private void channelsListBox_SelectedValueChanged(object sender, EventArgs e)
    {
        mLastSelectedGaugeIndex = channelsListBox.SelectedIndex;
        LoadPanel2ControlsState();
    }

    private void labelTextBox_TextChanged(object sender, EventArgs e)
    {
      if (channelsListBox.SelectedIndex > -1)
      {
        DisplayChannel dispChan = mDisplaysSet.Channels[channelsListBox.SelectedIndex];
        dispChan.Label = labelTextBox.Text;
        // Update the channels listbox content
        LoadPanel1ControlsState();
      }
    }

    private void unitNameTextBox_TextChanged(object sender, EventArgs e)
    {
      if (channelsListBox.SelectedIndex > -1)
      {
        DisplayChannel dispChan = mDisplaysSet.Channels[channelsListBox.SelectedIndex];
        dispChan.UnitName = unitNameTextBox.Text;
      }
    }

    private void inputChannelComboBox_TextChanged(object sender, EventArgs e)
    {
      if (channelsListBox.SelectedIndex > -1)
      {
        DisplayChannel dispChan = mDisplaysSet.Channels[channelsListBox.SelectedIndex];
        if (dispChan.IsUpperChannel)
        {
          dispChan.InputChannelIndex = ((ComboBoxChannelItem)(inputChannelComboBox.SelectedItem)).ChannelIndex + 9;
        }
        else
        {
          dispChan.InputChannelIndex = ((ComboBoxChannelItem)(inputChannelComboBox.SelectedItem)).ChannelIndex;
        }

      }
    }

    private void nominalCurrentToleranceNUD_ValueChanged(object sender, EventArgs e)
    {
      if (channelsListBox.SelectedIndex > -1)
      {
        DisplayChannel dispChan = mDisplaysSet.Channels[channelsListBox.SelectedIndex];
        dispChan.NominalCurrentTolerance = (uint)Convert.ToUInt32(nominalCurrentToleranceNUD.Value);
      }
    }

    private void upperDriveRB_CheckedChanged(object sender, EventArgs e)
    {
      if (channelsListBox.SelectedIndex > -1)
      {
        DisplayChannel dispChan = mDisplaysSet.Channels[channelsListBox.SelectedIndex];
        dispChan.IsUpperChannel = upperDriveRB.Checked;
      }
    }

    private void lowerDriveRB_CheckedChanged(object sender, EventArgs e)
    {
      if (channelsListBox.SelectedIndex > -1)
      {
        DisplayChannel dispChan = mDisplaysSet.Channels[channelsListBox.SelectedIndex];
        dispChan.IsUpperChannel = upperDriveRB.Checked;
      }
    }

    private void initialTestDutyCycleNUD_ValueChanged(object sender, EventArgs e)
    {
      if (channelsListBox.SelectedIndex > -1)
      {
        DisplayChannel dispChan = mDisplaysSet.Channels[channelsListBox.SelectedIndex];
        dispChan.InitTestDutyCycle = Convert.ToInt32(initialTestDutyCycleNUD.Value);
      }
    }

    private void nominalCurrentValueNUD_ValueChanged(object sender, EventArgs e)
    {
      if (channelsListBox.SelectedIndex > -1)
      {
        DisplayChannel dispChan = mDisplaysSet.Channels[channelsListBox.SelectedIndex];
        dispChan.NominalCurrent = Convert.ToSingle(nominalCurrentValueNUD.Value);
      }
    }

    private void minValNUD_ValueChanged(object sender, EventArgs e)
    {
      if (channelsListBox.SelectedIndex > -1)
      {
        DisplayChannel dispChan = mDisplaysSet.Channels[channelsListBox.SelectedIndex];
        dispChan.MinValue = Convert.ToSingle(minValNUD.Value);
      }
    }

    private void maxValNUD_ValueChanged(object sender, EventArgs e)
    {
      if (channelsListBox.SelectedIndex > -1)
      {
        DisplayChannel dispChan = mDisplaysSet.Channels[channelsListBox.SelectedIndex];
        dispChan.MaxValue = Convert.ToSingle(maxValNUD.Value);
      }
    }


  }

}
