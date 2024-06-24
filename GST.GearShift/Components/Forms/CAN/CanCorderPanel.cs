using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GST.Gearshift.Components.Interfaces.USB;

using XPTable.Models;

namespace GST.Gearshift.Components.Forms.CAN
{
  public partial class CanCorderPanel : UserControl
  {

    private enum CorderMode
    {
      Recording
    }

    #region Constants



    #endregion  Constants



    #region Private fields

    GearShiftUsb mDevice = null;

    Int32 mPrevRecordsCount = 0;

    System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

    int sentcnt = 0;

    #endregion Private fields



    #region Constructors & finalizer

    public CanCorderPanel()
    {
      InitializeComponent();
      InitTable();
    }

    #endregion Constructors & finalizer



    #region Events



    #endregion Events



    #region Properties

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GearShiftUsb Device
    {
      get { return mDevice; }
      set
      {
        mDevice = value;
        mDevice.CAN_CanCordingStarted += new EventHandler(mDevice_CAN_CanCordingStarted);
        mDevice.CAN_CanCordingStopped += new EventHandler(mDevice_CAN_CanCordingStopped);
        mDevice.CAN_CanPlaybackStarted += new EventHandler(mDevice_CAN_CanPlaybackStarted);
        mDevice.CAN_CanPlaybackStopped += new EventHandler(mDevice_CAN_CanPlaybackStopped);
      }
    }


    #endregion Properties



    #region Methods

    private void CANCorder_Load(object sender, EventArgs e)
    {

    }

    void mDevice_CAN_CanPlaybackStopped(object sender, EventArgs e)
    {
      stopwatch.Stop();
      playbackTimer.Enabled = false;
    }

    void mDevice_CAN_CanPlaybackStarted(object sender, EventArgs e)
    {
      stopwatch.Reset();
      stopwatch.Start();

      progressBar3.Maximum = (int)mDevice.CANPlaybackBuffer[mDevice.CANPlaybackBuffer.Count - 1].timestamp / 10;

      playbackTimer.Enabled = true;
    }

    void mDevice_CAN_CanCordingStopped(object sender, EventArgs e)
    {
      stopwatch.Stop();
      recordingTimer.Enabled = false;
    }

    void mDevice_CAN_CanCordingStarted(object sender, EventArgs e)
    {
      stopwatch.Start();
      table.TableModel.Rows.Clear();
      mPrevRecordsCount = 0;
      progressBar1.Maximum = (int)GearShiftUsb.UsbCANGetUsbDevRxBuffSize();
      progressBar2.Maximum = (int)GearShiftUsb.UsbCANGetUsbDevTxBuffSize();
      recordingTimer.Enabled = true;
    }

    public void LoadPlaybackData()
    {
      table.BeginUpdate();
      table.TableModel.Rows.Clear();
      foreach (UsbCANData msg in mDevice.CANPlaybackBuffer)
      {
        Row row = new Row();
        // Add msg number column
        row.Cells.Add(new Cell(table.RowCount.ToString()));
        // Add timestamp cell
        row.Cells.Add(new Cell((msg.timestamp / 10000.0f).ToString("0.00000")));
        // Add ID cell
        if (msg.isXtdFrameType != 0)
        {
          row.Cells.Add(new Cell("0x" + msg.remoteID.ToString("X8")));
        }
        else
        {
          row.Cells.Add(new Cell("0x" + msg.remoteID.ToString("X3")));
        }
        // Add DLC cell
        row.Cells.Add(new Cell(msg.DLC.ToString()));
        // Add Data cell
        string msgData = string.Empty;
        if (msg.DLC == 64)
        {
          // If this is a Remote Request frame..
          msgData = "RTR";
        }
        else
        {
          // If this is a normal frame carrying some data..
          for (int j = 0; j < Math.Min((int)msg.DLC, (int)8); j++)
          {
            msgData += msg.data[j].ToString("X2") + " ";
          }
          if (msgData.Length > 0)
          {
            msgData.Remove(msgData.Length - 1);
          }
        }
        row.Cells.Add(new Cell(msgData));

        table.TableModel.Rows.Add(row);
      }
      table.EndUpdate();
      table.EnsureVisible(0, 0);

      currTimeLabel.Text = "0.000";
      totalMsgLabel.Text = mDevice.CANPlaybackBuffer.Count.ToString();
      currMsgLabel.Text = "0";
    }

    private void InitTable()
    {
      // The Table control on a form - already initialised
      table.BeginUpdate();
      table.EnableWordWrap = true;    // If false, then Cell.WordWrap is ignored
      table.SelectionStyle = SelectionStyle.Grid;
      table.GridLines = GridLines.Rows;

      TextColumn col0 = new TextColumn("No.", 100);
      col0.ToolTipText = "Message number";
      TextColumn col1 = new TextColumn("Time [sec]", 200);
      col1.ToolTipText = "Delay in seconds from the beginning of recording to the moment when the message was received";
      col1.Alignment = ColumnAlignment.Center;
      TextColumn col2 = new TextColumn("Remote ID", 100);
      col2.ToolTipText = "CAN address (ID) of the node this message was addressed to";
      TextColumn col3 = new TextColumn("DLC", 60);
      col3.ToolTipText = "Data Length Code - the number of data bytes this message carries";
      TextColumn col4 = new TextColumn("Message", 400);
      col4.ToolTipText = "Data bytes carried by this message";

      table.ColumnModel = new ColumnModel(new Column[] { col0, col1, col2, col3, col4 });

      TableModel model = new TableModel();
      table.TableModel = model;

      table.EndUpdate();
    }

    #endregion Methods

    private void recordingTimer_Tick(object sender, EventArgs e)
    {
      int cnt = (int)numericUpDown1.Value;
      for (int q = 0; q < cnt; q++)
      {
        UsbCANData msg = new UsbCANData();
        msg.data = new byte[8];
        msg.isXtdFrameType = 0; ;
        msg.remoteID = (uint)q;
        msg.DLC = 64;
        for (int i = 0; i < 8; i++)
        {
          msg.data[i] = (byte)(i * 4);
        }
        mDevice.CANTxBuffer.Add(msg);
        sentcnt++;
      }

      sentCountLabel.Text = sentcnt.ToString();

      // Assign actual values to the labels
      currTimeLabel.Text = (stopwatch.ElapsedMilliseconds / 1000.0f).ToString("0.000");
      totalMsgLabel.Text = mDevice.CANRecordBuffer.Count.ToString();
      currMsgLabel.Text = mDevice.CANRecordBuffer.Count.ToString();

      // Refresh the progress bars
      try
      {
        progressBar1.Value = (int)GearShiftUsb.UsbCANGetUsbDevRxBuffFill();
        progressBar2.Value = (int)GearShiftUsb.UsbCANGetUsbDevTxBuffFill();
      }
      catch (Exception) { }


      // Calculate how many rows should be added (this approach is much more efficient than removing & adding all each time
      Int32 currRecCnt = mDevice.CANRecordBuffer.Count;
      if (currRecCnt > mPrevRecordsCount)
      {
        table.BeginUpdate();
        int rowsToAdd = currRecCnt - table.RowCount;
        for (int i = 0; i < rowsToAdd; i++)
        {
          UsbCANData msg = mDevice.CANRecordBuffer[mPrevRecordsCount + i];
          Row row = new Row();
          // Add msg number column
          row.Cells.Add(new Cell(table.RowCount.ToString()));
          // Add timestamp cell
          row.Cells.Add(new Cell((msg.timestamp / 10000.0f).ToString("0.00000")));
          // Add ID cell
          if (msg.isXtdFrameType != 0)
          {
            row.Cells.Add(new Cell("0x" + msg.remoteID.ToString("X8")));
          }
          else
          {
            row.Cells.Add(new Cell("0x" + msg.remoteID.ToString("X3")));
          }
          // Add DLC cell
          row.Cells.Add(new Cell(msg.DLC.ToString()));
          // Add Data cell
          string msgData = string.Empty;
          if (msg.isRTRFrame == 1)
          {
            // If this is a Remote Request frame..
            msgData = "Remote request";
          }
          else
          {
            // If this is a normal frame carrying some data..
            for (int j = 0; j < Math.Min((int)msg.DLC, (int)8); j++)
            {
              msgData += msg.data[j].ToString("X2") + " ";
            }
            if (msgData.Length > 0)
            {
              msgData.Remove(msgData.Length - 1);
            }
          }
          row.Cells.Add(new Cell(msgData));

          table.TableModel.Rows.Add(row);
        }
        table.EndUpdate();
        table.EnsureVisible(table.RowCount - 1, 0);
      }

      mPrevRecordsCount = currRecCnt;
    }

    private void playbackTimer_Tick(object sender, EventArgs e)
    {
      // Assign actual values to the labels
      currTimeLabel.Text = (stopwatch.ElapsedMilliseconds / 1000.0f).ToString("0.000");
      //totalMsgLabel.Text = mDevice.CANRecordBuffer.Count.ToString();
      //currMsgLabel.Text = mDevice.CANRecordBuffer.Count.ToString();

      // Refresh the progress bars
      try
      {
        progressBar1.Value = (int)GearShiftUsb.UsbCANGetUsbDevRxBuffFill();
        progressBar2.Value = (int)GearShiftUsb.UsbCANGetUsbDevTxBuffFill();
        if (stopwatch.ElapsedMilliseconds <= progressBar3.Maximum)
        {
          progressBar3.Value = (int)stopwatch.ElapsedMilliseconds;
        }
        else
        {
          progressBar3.Value = progressBar3.Maximum;
          mDevice.StopCANPlayback();
        }
        
      }
      catch (Exception) { }
    }
  }
}
