using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using System.IO;

using Soko.Common.Common;
using Soko.Common.Controls;
using GST.Gearshift.Components.Interfaces.USB;

namespace GST.Gearshift.Components.Forms.DAQ
{

  public partial class ReportExplorer : UserControl
  {


    #region Constants



    #endregion  Constants



    #region Private fields

    private List<string> mReportSearchResult = new List<string>(0);

    private List<ListBoxMultiReportItem> mReportsComboBoxItemsTemp = new List<ListBoxMultiReportItem>();
    private List<ListBoxMultiReportItem> mOperatorComboBoxItemsTemp = new List<ListBoxMultiReportItem>();
    private List<ListBoxMultiReportItem> mSerialComboBoxItemsTemp = new List<ListBoxMultiReportItem>();
    private List<ListBoxMultiReportItem> mTestNameComboBoxItemsTemp = new List<ListBoxMultiReportItem>();
    private List<ListBoxMultiReportItem> mDateComboBoxItemsTemp = new List<ListBoxMultiReportItem>();

    // Flag if disk content needs to be refreshed again
    private bool _refreshDiskContentAgain = false;

    #endregion Private fields



    #region Constructors & finalizer


    public ReportExplorer()
    {
      InitializeComponent();
    }

    #endregion Constructors & finalizer



    #region Events

    public delegate void TestReportChosenDelegate(TestScriptReport ts);

    public event TestReportChosenDelegate TestReportChosen;

    #endregion Events



    #region Properties



    #endregion Properties



    #region Methods

    public void RefreshDiskContent()
    {
      // The background worker will read the data from disk in a separate thread and put it to temporary lists
      // Comboboxes will be filled in an event from bgw that it's finished.
      if (!bgw.IsBusy)
      {
        // Remove the existing items
        operatorComboBox.Items.Clear();
        serialComboBox.Items.Clear();
        testNameComboBox.Items.Clear();
        dateComboBox.Items.Clear();
        reportsComboBox.Items.Clear();
        rsltTable.ClearAllData();
        // Clear the flag for checking it there were any .tsc files and content needs to be refreshed again
        _refreshDiskContentAgain = false;
        // Run background worker
        bgw.RunWorkerAsync();
      }
    }



    private void reportsComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (reportsComboBox.SelectedItem != null && TestReportChosen != null)
      {
        string chosenReportFileName = ((ListBoxMultiReportItem)reportsComboBox.SelectedItem).mReportHeaders[0].Filename;
        TestReportChosen(TestScriptReport.OpenFile(chosenReportFileName));
      }
    }

    #endregion Methods

    private void populateResultsTable(object sender, EventArgs e)
    {
      if (((ComboBox)sender).SelectedItem != null)
      {
        rsltTable.BeginUpdate();
        rsltTableTableModel.Rows.Clear();
        ListBoxMultiReportItem item = (ListBoxMultiReportItem)((ComboBox)sender).SelectedItem;
        foreach (TestReportHeader hdr in item.mReportHeaders)
        {
          XPTable.Models.Row row = new XPTable.Models.Row();
          rsltTableTableModel.Rows.Add(row);
          row.Tag = (object)hdr;
          row.Cells.Add(new XPTable.Models.Cell(System.IO.Path.GetFileNameWithoutExtension(hdr.Filename)));
          row.Cells.Add(new XPTable.Models.Cell(hdr.OperatorName));
          row.Cells.Add(new XPTable.Models.Cell(hdr.GearboxSerialNumber));
          row.Cells.Add(new XPTable.Models.Cell(hdr.TestName));
          row.Cells.Add(new XPTable.Models.Cell(hdr.TimePerformed.ToString("yyyy.MM.dd hh:mm:ss")));
        }
        rsltTable.EndUpdate();
      }
    }

    private void operatorRadioButton_CheckedChanged(object sender, EventArgs e)
    {
      if (operatorRadioButton.Checked == false)
      {
        operatorComboBox.SelectedIndex = -1;
        rsltTableTableModel.Rows.Clear();
      }
    }

    private void serialRadioButton_CheckedChanged(object sender, EventArgs e)
    {
      if (serialRadioButton.Checked == false)
      {
        serialComboBox.SelectedIndex = -1;
        rsltTableTableModel.Rows.Clear();
      }
    }

    private void testNameRadioButton_CheckedChanged(object sender, EventArgs e)
    {
      if (testNameRadioButton.Checked == false)
      {
        testNameComboBox.SelectedIndex = -1;
        rsltTableTableModel.Rows.Clear();
      }
    }

    private void dateRadioButton_CheckedChanged(object sender, EventArgs e)
    {
      if (dateRadioButton.Checked == false)
      {
        dateComboBox.SelectedIndex = -1;
        rsltTableTableModel.Rows.Clear();
      }
    }

    private void operatorComboBox_DropDown(object sender, EventArgs e)
    {
      operatorRadioButton.Checked = true;
    }

    private void serialComboBox_DropDown(object sender, EventArgs e)
    {
      serialRadioButton.Checked = true;
    }

    private void testNameComboBox_DropDown(object sender, EventArgs e)
    {
      testNameRadioButton.Checked = true;
    }

    private void dateComboBox_DropDown(object sender, EventArgs e)
    {
      dateRadioButton.Checked = true;
    }

    private void bgw_DoWork(object sender, DoWorkEventArgs e)
    {
      // Open each report on the disk and fill the comboboxes temp lists
      //try
      {
        mReportsComboBoxItemsTemp.Clear();
        mOperatorComboBoxItemsTemp.Clear();
        mSerialComboBoxItemsTemp.Clear();
        mTestNameComboBoxItemsTemp.Clear();
        mDateComboBoxItemsTemp.Clear();

        string[] filePaths = GST.Gearshift.Components.Utilities.Settings.AvailableReportsPaths;
        int neededIterationsCount = filePaths.Length;
        int currIteration = 0;
        foreach (string path in filePaths)
        {
          bgw.ReportProgress((int)(((float)currIteration / (float)neededIterationsCount) * 100.0f));

          // Mark the flag if another refreshing is required 
          if (Path.GetExtension(path) == ".trf")
          {
            // Display a messagebox with a warning and make sure it's displayed only once
            if (_refreshDiskContentAgain == false)
            {
              Soko.Common.Forms.MessageBox.ShowInfo("GearShift",
                                                        "This might take a while...",
                                                        "Some old format reports were found on your disk and they are converted no a new, fast one. This might take a while depending on the number of your old files.",
                                                        Soko.Common.Forms.MessageBoxButtons.OK);
            }
            _refreshDiskContentAgain = true;
          }

          // Open the file
          TestReportHeader trh = TestReportHeader.LoadFromFile(path);

          // If the file could not be opened, jump to next iteration
          if (trh == null)
          {
            continue;
          }

          // Add each existing file to the reports combobox
          mReportsComboBoxItemsTemp.Add(new ListBoxMultiReportItem(System.IO.Path.GetFileNameWithoutExtension(path), trh));

          // Process the Date combobox
          bool dateItemFound = false;
          string date = trh.TimePerformed.Date.ToString("yyyy-MM-dd");
          foreach (ListBoxMultiReportItem item in mDateComboBoxItemsTemp)
          {
            if (item.mItemName == date)
            {
              dateItemFound = true;
              item.mReportHeaders.Add(trh);
              break;
            }
          }
          if (!dateItemFound)
          {
            mDateComboBoxItemsTemp.Add(new ListBoxMultiReportItem(date, trh));
          }

          // Process the test name combobox
          bool testNameItemFound = false;
          string testName = trh.TestName;
          foreach (ListBoxMultiReportItem item in mTestNameComboBoxItemsTemp)
          {
            if (item.mItemName == testName)
            {
              testNameItemFound = true;
              item.mReportHeaders.Add(trh);
              break;
            }
          }
          if (!testNameItemFound)
          {
            mTestNameComboBoxItemsTemp.Add(new ListBoxMultiReportItem(testName, trh));
          }

          // Process the serial number combobox
          bool serialItemFound = false;
          string serial = trh.GearboxSerialNumber;
          foreach (ListBoxMultiReportItem item in mSerialComboBoxItemsTemp)
          {
            if (item.mItemName == serial)
            {
              serialItemFound = true;
              item.mReportHeaders.Add(trh);
              break;
            }
          }
          if (!serialItemFound)
          {
            mSerialComboBoxItemsTemp.Add(new ListBoxMultiReportItem(serial, trh));
          }

          // Process the Operator combobox
          bool operatorItemFound = false;
          string op = trh.OperatorName;
          foreach (ListBoxMultiReportItem item in mOperatorComboBoxItemsTemp)
          {
            if (item.mItemName == op)
            {
              operatorItemFound = true;
              item.mReportHeaders.Add(trh);
              break;
            }
          }
          if (!operatorItemFound)
          {
            mOperatorComboBoxItemsTemp.Add(new ListBoxMultiReportItem(op, trh));
          }
          currIteration++;
        }
      }
      //catch (Exception)
      {
      //  throw new DirectoryNotFoundException("Either the application was improperly installed or the DAQ reports directory was removed by user");
      }
    }

    private void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      bgw_pb.Value = e.ProgressPercentage;
    }

    private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (_refreshDiskContentAgain)
      {
        // If disk content needs to be refreshed again
        RefreshDiskContent();
        return;
      }
      bgw_pb.Value = bgw_pb.Maximum;
      reportsComboBox.Items.AddRange(mReportsComboBoxItemsTemp.ToArray());
      operatorComboBox.Items.AddRange(mOperatorComboBoxItemsTemp.ToArray());
      serialComboBox.Items.AddRange(mSerialComboBoxItemsTemp.ToArray());
      testNameComboBox.Items.AddRange(mTestNameComboBoxItemsTemp.ToArray());
      dateComboBox.Items.AddRange(mDateComboBoxItemsTemp.ToArray());
    }

    private void rsltTable_DoubleClick(object sender, EventArgs e)
    {
      //rsltTable.
    }

    private void refreshButton_Click(object sender, EventArgs e)
    {
      RefreshDiskContent();
    }

    private void rsltTable_CellDoubleClick(object sender, XPTable.Events.CellMouseEventArgs e)
    {
      if (TestReportChosen != null)
      {
        TestReportChosen(TestScriptReport.OpenFile(((TestReportHeader)rsltTableTableModel.Rows[e.Row].Tag).Filename));
      }
    }

    private void deleteButton_Click(object sender, EventArgs e)
    {
      try
      {
        if (rsltTable.SelectedItems.Length > 0)
        {
          DialogResult dr = Soko.Common.Forms.MessageBox.ShowWarning("GearShift",
                                                                    "Data loss warning",
                                                                    "Are you sure you want to permanently delete the selected reports from disk?",
                                                                    Soko.Common.Forms.MessageBoxButtons.YesNo);
          if (dr == DialogResult.Yes)
          {
            foreach (XPTable.Models.Row row in rsltTable.SelectedItems)
            {
              System.IO.File.Delete(((TestReportHeader)row.Tag).Filename);
            }
            RefreshDiskContent();
          }
        }
      }
      catch (Exception ex)
      {
        Soko.Common.Forms.MessageBox.ShowCritical("GearShift",
                                                                    "Operation failed",
                                                                    "Deleting the files did not succeed:\n" + ex.Message,
                                                                    Soko.Common.Forms.MessageBoxButtons.OK);
      }
      
    }


    private void renameButton_Click(object sender, EventArgs e)
    {
        if (rsltTable.SelectedItems.Length > 0)
        {
            foreach (XPTable.Models.Row row in rsltTable.SelectedItems)
            {
              //ReportExplorerRenameDialog rerd = new ReportExplorerRenameDialog(((TestReportHeader)row.Tag).Filename);
             // rerd.ShowDialog();
            }
            RefreshDiskContent();
        }
    }

  }

}
