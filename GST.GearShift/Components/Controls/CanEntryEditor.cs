using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GST.Gearshift.Components.Interfaces.USB;

namespace GST.Gearshift.Components.Forms
{
  public partial class CanEntryEditor : UserControl
  {

    private CanEntry mCanEntry = new CanEntry();

    private bool mDisableControlsValueEditedEvent = false;

    public CanEntryEditor()
    {
      InitializeComponent();
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public CanEntry CanEntry
    {
      get 
      {
        return mCanEntry; 
      }
      set
      {
        mDisableControlsValueEditedEvent = true;
        SuspendLayout();
        mCanEntry = value;
        NameLabel.Text = mCanEntry.EntryName + " properties:";
        ID_NUD.Value = System.Convert.ToDecimal(mCanEntry.MsgID);
        LSB_NUD.Value = System.Convert.ToDecimal(mCanEntry.LsbIndex);
        MSB_NUD.Value = System.Convert.ToDecimal(mCanEntry.MsbIndex);
        ValMinNUD.Value = System.Convert.ToDecimal(mCanEntry.ValueMin);
        ValMaxNUD.Value = System.Convert.ToDecimal(mCanEntry.ValueMax);
        ValPreOffs.Value = System.Convert.ToDecimal(mCanEntry.ValuePreOffset);
        ValMultNUD.Value = System.Convert.ToDecimal(mCanEntry.ValueMultiplier);
        ValPostOffs.Value = System.Convert.ToDecimal(mCanEntry.ValuePostOffset);
        Console.WriteLine(NameLabel.Text + " " + mCanEntry.MsgID.ToString() + " " + mCanEntry.LsbIndex.ToString() + " " + mCanEntry.MsbIndex.ToString());
        ResumeLayout();
        mDisableControlsValueEditedEvent = false;
      }
    }

    private void SaveControlsToCanEntry(Object Sender, EventArgs e)
    {
      if (mDisableControlsValueEditedEvent)
      {
        return;
      }
      mCanEntry.MsgID = System.Convert.ToUInt32(ID_NUD.Value);
      mCanEntry.LsbIndex = System.Convert.ToUInt32(LSB_NUD.Value);
      mCanEntry.MsbIndex = System.Convert.ToInt32(MSB_NUD.Value);
      mCanEntry.ValueMin = System.Convert.ToDouble(ValMinNUD.Value);
      mCanEntry.ValueMax = System.Convert.ToDouble(ValMaxNUD.Value);
      mCanEntry.ValuePreOffset = System.Convert.ToUInt32(ValPreOffs.Value);
      mCanEntry.ValueMultiplier = System.Convert.ToDouble(ValMultNUD.Value);
      mCanEntry.ValuePostOffset = System.Convert.ToDouble(ValPostOffs.Value);

      if (ValueEdited != null)
      {
        ValueEdited(this, EventArgs.Empty);
      }
    }

    public event EventHandler ValueEdited;

  }
}
