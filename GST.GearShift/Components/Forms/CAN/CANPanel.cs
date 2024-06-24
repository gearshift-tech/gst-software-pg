using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Soko.Common.Controls;
using GST.Gearshift.Components.Interfaces.USB;

using XPTable.Models;

namespace GST.Gearshift.Components.Forms.CAN
{
    public partial class CANPanel : UserControl
    {


        #region Constants



        #endregion  Constants


        #region Private fields

        GearboxCANConfig mGearboxCanCfg = new GearboxCANConfig();

        GearShiftUsb mDevice = null;

        private bool mRecBtnBlinkerToggle = false;

        #endregion Private fields


        #region Constructors & finalizer

        public CANPanel()
        {
            InitializeComponent();
            this.SetStyle(
                    ControlStyles.SupportsTransparentBackColor |
                    ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.ResizeRedraw |
                    ControlStyles.UserPaint, true);

            this.GearboxCanCfg = new GearboxCANConfig();

            InitSnifferTable(snifferTable);

            enableCANButton.Enabled = false;
            disableCANButton.Enabled = false;
            ToggleBusTerminationButton.Enabled = false;
            resetSnifferButton.Enabled = false;
            recPbkBtn.Enabled = false;
            playPbkBtn.Enabled = false;
            stopPbkBtn.Enabled = false;
            tracesComboBox.Enabled = false;

        }

        #endregion Constructors & finalizer


        #region Events

        private delegate void DataReceivedEventDelegate();

        #endregion Events


        #region Properties

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GearboxCANConfig GearboxCanCfg
        {
            get
            {
                SaveDbgDgControls();
                return mGearboxCanCfg;
            }
            set
            {
                mGearboxCanCfg = value;
                LoadDbgDgControls();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GearShiftUsb Device
        {
            get { return mDevice; }
            set
            {
                mDevice = value;
                canCorder1.Device = value;
                mDevice.DeviceConnectedEvent += new EventHandler(mDevice_DeviceConnectedEvent);
                mDevice.DeviceDisconnecedEvent += new EventHandler(mDevice_DeviceDisconnecedEvent);
                mDevice.CANDataReceivedEvent += new EventHandler(CanMessageReceivedEH);
                mDevice.CAN_EnabledEvent += new EventHandler(deviceCANCommEnabled);
                mDevice.CAN_DisabledEvent += new EventHandler(deviceCANCommDisabled);
                mDevice.CAN_TerminationEnabledEvent += new EventHandler(DeviceCAN_TerminationEnabledEH);
                mDevice.CAN_TerminationDisabledEvent += new EventHandler(DeviceCAN_TerminationDisabledEH);

                mDevice.CAN_CanCordingStarted += new EventHandler(mDevice_CAN_CanCordingStarted);
                mDevice.CAN_CanCordingStopped += new EventHandler(mDevice_CAN_CanCordingStopped);
                mDevice.CAN_CanPlaybackStarted += new EventHandler(mDevice_CAN_CanPlaybackStarted);
                mDevice.CAN_CanPlaybackStopped += new EventHandler(mDevice_CAN_CanPlaybackStopped);
            }
        }


        #endregion Properties


        #region Methods

        public void StartCanTransmission()
        {
            SaveDbgDgControls();
            double err = mDevice.StartCANTransmission(mGearboxCanCfg.mCanBusBaud);
        }

        private void ReceiveCanMessages()
        {
            return;

        }

        /// <summary>
        /// Loads the DbgDg tab controls values from the mGearboxCanCfg
        /// </summary>
        private void LoadDbgDgControls()
        {
            dbg_BaudComboBox.SelectedIndex = -1;
            dbg_BaudComboBox.Text = "Baud_" + (mGearboxCanCfg.mCanBusBaud / 1000.0f).ToString() + "k";

            if (mGearboxCanCfg.mExtendedTypeMessaging)
            {
                dbg_XtdFrameRadioButton.Checked = true;
            }
            else
            {
                dbg_StdFrameRadioButton.Checked = true;
            }

            dbg_GearboxNameTextbox.Text = mGearboxCanCfg.Filename;
            commIntervalNUD.Value = mGearboxCanCfg.mCycleTimeMs;

            drv_OutputGauge.MinValue = (int)mGearboxCanCfg.mSensorsConfig.OutputShaftRPMEntry.ValueMin;
            drv_OutputGauge.MaxValue = (int)mGearboxCanCfg.mSensorsConfig.OutputShaftRPMEntry.ValueMax;
            drv_OutputGauge.ScaleLinesMajorStepValue = (int)Math.Abs((int)drv_OutputGauge.MaxValue - (int)drv_OutputGauge.MinValue) / 20;

            drv_TurbineGauge.MinValue = (int)mGearboxCanCfg.mSensorsConfig.InputShaftRPMEntry.ValueMin;
            drv_TurbineGauge.MaxValue = (int)mGearboxCanCfg.mSensorsConfig.InputShaftRPMEntry.ValueMax;
            drv_TurbineGauge.ScaleLinesMajorStepValue = (int)Math.Abs((int)drv_TurbineGauge.MaxValue - (int)drv_TurbineGauge.MinValue) / 20;


            mGearboxCanCfg.mSensorsConfig.InputShaftRPMEntry.EntryName = "Input shaft RPM";
            InputShaftEntryEditor.CanEntry = mGearboxCanCfg.mSensorsConfig.InputShaftRPMEntry;
            mGearboxCanCfg.mSensorsConfig.OutputShaftRPMEntry.EntryName = "Output shaft RPM";
            OutputShaftEntryEditor.CanEntry = mGearboxCanCfg.mSensorsConfig.OutputShaftRPMEntry;
            mGearboxCanCfg.mSensorsConfig.EngineRPMEntry.EntryName = "Engine RPM";
            EngineRPMCanEntryEditor.CanEntry = mGearboxCanCfg.mSensorsConfig.EngineRPMEntry;
            mGearboxCanCfg.mSensorsConfig.TPSEntry.EntryName = "TPS";
            TPSEntryEditor.CanEntry = mGearboxCanCfg.mSensorsConfig.TPSEntry;
            mGearboxCanCfg.mSensorsConfig.EngineLoadEntry.EntryName = "Engine load";
            EngineLoadCanEntryEditor.CanEntry = mGearboxCanCfg.mSensorsConfig.EngineLoadEntry;
            mGearboxCanCfg.mSensorsConfig.TempEntry.EntryName = "Temperature";
            TempEntryEditor.CanEntry = mGearboxCanCfg.mSensorsConfig.TempEntry;


        }

        /// <summary>
        /// Saves the DbgDg tab controls values to the mGearboxCanCfg
        /// </summary>
        private void SaveDbgDgControls()
        {
            //assign properly the bus speed value
            string busSpeed;
            if (dbg_BaudComboBox.SelectedIndex == -1)
            {
                busSpeed = dbg_BaudComboBox.Text;
            }
            else
            {
                busSpeed = dbg_BaudComboBox.Items[dbg_BaudComboBox.SelectedIndex].ToString();
            }
            busSpeed = busSpeed.Replace("Baud_", string.Empty);
            busSpeed = busSpeed.Replace("k", string.Empty);
            mGearboxCanCfg.mCanBusBaud = Convert.ToUInt32(busSpeed) * 1000;

            mGearboxCanCfg.mExtendedTypeMessaging = dbg_XtdFrameRadioButton.Checked;
            mGearboxCanCfg.mCycleTimeMs = Convert.ToUInt32(commIntervalNUD.Value);

            mGearboxCanCfg.mSensorsConfig.InputShaftRPMEntry = InputShaftEntryEditor.CanEntry;
            mGearboxCanCfg.mSensorsConfig.OutputShaftRPMEntry = OutputShaftEntryEditor.CanEntry;
            mGearboxCanCfg.mSensorsConfig.TempEntry = TempEntryEditor.CanEntry;
            mGearboxCanCfg.mSensorsConfig.TPSEntry = TPSEntryEditor.CanEntry;
            mGearboxCanCfg.mSensorsConfig.EngineLoadEntry = EngineLoadCanEntryEditor.CanEntry;
            mGearboxCanCfg.mSensorsConfig.EngineRPMEntry = EngineRPMCanEntryEditor.CanEntry;
        }



        #endregion Methods


        #region EventHandling

        private void dbg_SaveButton_Click(object sender, EventArgs e)
        {
            if (dbg_GearboxNameTextbox.Text == string.Empty)
                return;
            //get a path to the appropriate folder
            string saveDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            saveDir += "\\GearShift Technologies\\GearShift\\Installed gearboxes\\" + dbg_GearboxNameTextbox.Text;
            //savePath += dbg_GearboxNameTextbox.Text + ".ccf";
            // if directory does not exist, create it
            if (!Directory.Exists(saveDir))
            {
                Directory.CreateDirectory(saveDir);
            }
            SaveDbgDgControls();
            mGearboxCanCfg.SaveXml(saveDir + "\\" + dbg_GearboxNameTextbox.Text + ".ccf");

        }

        private void drv_TurbineGaugeUserEnableRB_CheckedChanged(object sender, EventArgs e)
        {
            drv_TurbineGauge.EnableUserControl = drv_TurbineGaugeUserEnableRB.Checked;
        }

        private void drv_OutputGaugeUserEnableRB_CheckedChanged(object sender, EventArgs e)
        {
            drv_OutputGauge.EnableUserControl = drv_OutputGaugeUserEnableRB.Checked;
        }

        private void drv_TempGaugeUserEnableRB_CheckedChanged(object sender, EventArgs e)
        {
            drv_TempGauge.EnableUserControl = drv_TempGaugeUserEnableRB.Checked;
        }

        private void drv_TPSGaugeUserEnableRB_CheckedChanged(object sender, EventArgs e)
        {
            drv_TPSGauge.EnableUserControl = drv_TPSGaugeUserEnableRB.Checked;
        }

        private void drv_TurbineGauge_UserChoseValueEvent(object sender, EventArgs e)
        {
            //       UsbCANData msg = new UsbCANData();
            //       msg.data = new byte[8];
            //       msg.isXtdFrameType = Convert.ToByte( mGearboxCanCfg.mExtendedTypeMessaging );
            //       msg.remoteID = mGearboxCanCfg.mSensorsConfig.mRevInID;
            //       msg.DLC = 8;
            //       Int32 value = Convert.ToInt32( drv_TurbineGauge.Value );
            //       for ( int i = 0; i < mGearboxCanCfg.mSensorsConfig.mRevInMult; i++ )
            //       {
            //         byte shiftValue = (Byte)( 8 * ( mGearboxCanCfg.mSensorsConfig.mRevInMult - i - 1 ) );
            //         byte val = (byte)( ( value >> shiftValue ) & 0xFF );
            //         msg.data[mGearboxCanCfg.mSensorsConfig.mRevInBit + i] = val;
            //       }
            //       mDevice.CANTxBuffer.Add( msg );
        }

        private void drv_OutputGauge_UserChoseValueEvent(object sender, EventArgs e)
        {
            //       UsbCANData msg = new UsbCANData();
            //       msg.data = new byte[8];
            //       msg.isXtdFrameType = Convert.ToByte( mGearboxCanCfg.mExtendedTypeMessaging );
            //       msg.remoteID = mGearboxCanCfg.mSensorsConfig.mRevOutID;
            //       msg.DLC = 8;
            //       Int32 value = Convert.ToInt32( drv_OutputGauge.Value );
            //       for ( int i = 0; i < mGearboxCanCfg.mSensorsConfig.mRevOutMult; i++ )
            //       {
            //         byte shiftValue = (Byte)( 8 * ( mGearboxCanCfg.mSensorsConfig.mRevOutMult - i - 1 ) );
            //         byte val = (byte)( ( value >> shiftValue ) & 0xFF );
            //         msg.data[mGearboxCanCfg.mSensorsConfig.mRevOutBit + i] = val;
            //       }
            //       mDevice.CANTxBuffer.Add( msg );
        }

        private void drv_TempGauge_UserChoseValueEvent(object sender, EventArgs e)
        {
            //       UsbCANData msg = new UsbCANData();
            //       msg.data = new byte[8];
            //       msg.isXtdFrameType = Convert.ToByte( mGearboxCanCfg.mExtendedTypeMessaging );
            //       msg.remoteID = mGearboxCanCfg.mSensorsConfig.mTempID;
            //       msg.DLC = 8;
            //       msg.data[mGearboxCanCfg.mSensorsConfig.mTempBit] = System.Convert.ToByte( drv_TempGauge.Value );
            //       mDevice.CANTxBuffer.Add( msg );
        }

        private void drv_TPSGauge_UserChoseValueEvent(object sender, EventArgs e)
        {
            //UsbCANData msg = new UsbCANData();
            //msg.data = new byte[8];
            //msg.isXtdFrameType = Convert.ToByte( mGearboxCanCfg.mExtendedTypeMessaging );
            //msg.remoteID = mGearboxCanCfg.mSensorsConfig.mTPSID;
            //msg.DLC = 8;
            //msg.data[mGearboxCanCfg.mSensorsConfig.mTPSIDBit] = System.Convert.ToByte( drv_TPSGauge.Value );
            //mDevice.CANTxBuffer.Add( msg );
        }

        private void drv_LockUpButton_OnStateChangedEvent(object sender, EventArgs e)
        {
            //       UsbCANData msg = new UsbCANData();
            //       msg.data = new byte[8];
            //       msg.isXtdFrameType = Convert.ToByte( mGearboxCanCfg.mExtendedTypeMessaging );
            //       msg.remoteID = mGearboxCanCfg.mSensorsConfig.mLUID;
            //       msg.DLC = 8;
            //
            //       if ( drv_LockUpButton.IsOn )
            //       {
            //         msg.data[mGearboxCanCfg.mSensorsConfig.mLUBit] = System.Convert.ToByte( mGearboxCanCfg.mSensorsConfig.mLUOn );
            //       }
            //       else
            //       {
            //         msg.data[mGearboxCanCfg.mSensorsConfig.mLUBit] = System.Convert.ToByte( mGearboxCanCfg.mSensorsConfig.mLUOff );
            //       }
            //       mDevice.CANTxBuffer.Add( msg );
        }

        private void drv_BrakeButton_OnStateChangedEvent(object sender, EventArgs e)
        {
            //       UsbCANData msg = new UsbCANData();
            //       msg.data = new byte[8];
            //       msg.isXtdFrameType = Convert.ToByte( mGearboxCanCfg.mExtendedTypeMessaging );
            //       msg.remoteID = mGearboxCanCfg.mSensorsConfig.mBrakeID;
            //       msg.DLC = 8;
            //
            //       if ( drv_BrakeButton.IsOn )
            //       {
            //         msg.data[mGearboxCanCfg.mSensorsConfig.mBrakeBit] = System.Convert.ToByte( mGearboxCanCfg.mSensorsConfig.mBrakeOn );
            //       }
            //       else
            //       {
            //         msg.data[mGearboxCanCfg.mSensorsConfig.mBrakeBit] = System.Convert.ToByte( 0x0 );
            //       }
            //       mDevice.CANTxBuffer.Add( msg );
        }

        void mDevice_DeviceDisconnecedEvent(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                EventHandler dg = new EventHandler(mDevice_DeviceDisconnecedEvent);
                BeginInvoke(dg, new object[] { sender, e });
            }
            else
            {
                enableCANButton.Enabled = false;
                disableCANButton.Enabled = false;
                ToggleBusTerminationButton.Enabled = false;
                resetSnifferButton.Enabled = false;
                recPbkBtn.Enabled = false;
                playPbkBtn.Enabled = false;
                stopPbkBtn.Enabled = false;
                tracesComboBox.Enabled = false;
            }
        }

        void mDevice_DeviceConnectedEvent(object sender, EventArgs e)
        {
            enableCANButton.Enabled = true;
            disableCANButton.Enabled = false;
        }

        private void deviceCANCommEnabled(object sender, EventArgs e)
        {
            commTimer.Enabled = true;
            GUI_RefreshTimer.Enabled = true;
            enableCANButton.Enabled = false;
            disableCANButton.Enabled = true;
            ToggleBusTerminationButton.Enabled = true;
            resetSnifferButton.Enabled = true;
            recPbkBtn.Enabled = true;
            playPbkBtn.Enabled = false;
            stopPbkBtn.Enabled = false;
            tracesComboBox.Enabled = true;
        }

        private void deviceCANCommDisabled(object sender, EventArgs e)
        {
            commTimer.Enabled = false;
            GUI_RefreshTimer.Enabled = false;
            enableCANButton.Enabled = true;
            disableCANButton.Enabled = false;
            ToggleBusTerminationButton.Enabled = false;
            resetSnifferButton.Enabled = false;
            recPbkBtn.Enabled = false;
            playPbkBtn.Enabled = false;
            stopPbkBtn.Enabled = false;
        }

        private void CanMessageReceivedEH(object sender, EventArgs e)
        {
            //       if ( true )//InvokeRequired )
            //       {
            //DataReceivedEventDelegate dred = new DataReceivedEventDelegate(ReceiveCanMessages);
            //this.BeginInvoke(dred);
            //       }
            //       else
            //       {
            //         ReceiveCanMessages();
            //       }
        }

        private void commIntervalNUD_ValueChanged(object sender, EventArgs e)
        {
            commTimer.Interval = Convert.ToInt32(commIntervalNUD.Value);
        }

        private void commTimer_Tick(object sender, EventArgs e)
        {
            if (!mDevice.IsConnected)
            {
                return;
            }

            if (drv_TurbineGaugeUserEnableRB.Checked)
            {
                //UsbCANData msg = new UsbCANData();
                //msg.data = new byte[8];
                //msg.isXtdFrameType = Convert.ToByte( mGearboxCanCfg.mExtendedTypeMessaging );
                //msg.remoteID = mGearboxCanCfg.mSensorsConfig.mRevInID;
                //msg.DLC = 8;
                //Int32 value = Convert.ToInt32( drv_TurbineGauge.Value );
                //for ( int i = 0; i < mGearboxCanCfg.mSensorsConfig.mRevInMult; i++ )
                //{
                //  byte shiftValue = (Byte)( 8 * ( mGearboxCanCfg.mSensorsConfig.mRevInMult - i - 1 ) );
                //  byte val = (byte)( ( value >> shiftValue ) & 0xFF );
                //  msg.data[mGearboxCanCfg.mSensorsConfig.mRevInBit + i] = val;
                //}
                //mDevice.CANTxBuffer.Add( msg );
            }

            if (drv_OutputGaugeUserEnableRB.Checked)
            {
                //UsbCANData msg = new UsbCANData();
                //msg.data = new byte[8];
                //msg.isXtdFrameType = Convert.ToByte( mGearboxCanCfg.mExtendedTypeMessaging );
                //msg.remoteID = mGearboxCanCfg.mSensorsConfig.mRevOutID;
                //msg.DLC = 8;
                //Int32 value = Convert.ToInt32( drv_OutputGauge.Value );
                //for ( int i = 0; i < mGearboxCanCfg.mSensorsConfig.mRevOutMult; i++ )
                //{
                //  byte shiftValue = (Byte)( 8 * ( mGearboxCanCfg.mSensorsConfig.mRevOutMult - i - 1 ) );
                //  byte val = (byte)( ( value >> shiftValue ) & 0xFF );
                //  msg.data[mGearboxCanCfg.mSensorsConfig.mRevOutBit + i] = val;
                //}
                //mDevice.CANTxBuffer.Add( msg );
            }

            if (drv_TempGaugeUserEnableRB.Checked)
            {
                //UsbCANData msg = new UsbCANData();
                //msg.data = new byte[8];
                //msg.isXtdFrameType = Convert.ToByte( mGearboxCanCfg.mExtendedTypeMessaging );
                //msg.remoteID = mGearboxCanCfg.mSensorsConfig.mTempID;
                //msg.DLC = 8;
                //msg.data[mGearboxCanCfg.mSensorsConfig.mTempBit] = System.Convert.ToByte( drv_TempGauge.Value );
                //mDevice.CANTxBuffer.Add( msg );
            }

            if (drv_TPSGaugeUserEnableRB.Checked)
            {
                //UsbCANData msg = new UsbCANData();
                //msg.data = new byte[8];
                //msg.isXtdFrameType = Convert.ToByte( mGearboxCanCfg.mExtendedTypeMessaging );
                //msg.remoteID = mGearboxCanCfg.mSensorsConfig.mTPSID;
                //msg.DLC = 8;
                //msg.data[mGearboxCanCfg.mSensorsConfig.mTPSIDBit] = System.Convert.ToByte( drv_TPSGauge.Value );
                //mDevice.CANTxBuffer.Add( msg );
            }

            //if (drv_LUUserEnableRB.Checked)
            {
                //UsbCANData msg = new UsbCANData();
                //msg.data = new byte[8];
                //msg.isXtdFrameType = Convert.ToByte( mGearboxCanCfg.mExtendedTypeMessaging );
                //msg.remoteID = mGearboxCanCfg.mSensorsConfig.mLUID;
                //msg.DLC = 8;

                //if ( drv_LockUpButton.IsOn )
                //{
                //  msg.data[mGearboxCanCfg.mSensorsConfig.mLUBit] = System.Convert.ToByte( mGearboxCanCfg.mSensorsConfig.mLUOn );
                //}
                //else
                //{
                //  msg.data[mGearboxCanCfg.mSensorsConfig.mLUBit] = System.Convert.ToByte( mGearboxCanCfg.mSensorsConfig.mLUOff );
                //}
                //mDevice.CANTxBuffer.Add( msg );
            }

            //if (drv_BrakeUserEnableRB.Checked)
            {
                //UsbCANData msg = new UsbCANData();
                //msg.data = new byte[8];
                //msg.isXtdFrameType = Convert.ToByte( mGearboxCanCfg.mExtendedTypeMessaging );
                //msg.remoteID = mGearboxCanCfg.mSensorsConfig.mBrakeID;
                //msg.DLC = 8;

                //if ( drv_BrakeButton.IsOn )
                //{
                //  msg.data[mGearboxCanCfg.mSensorsConfig.mBrakeBit] = System.Convert.ToByte( mGearboxCanCfg.mSensorsConfig.mBrakeOn );
                //}
                //else
                //{
                //  msg.data[mGearboxCanCfg.mSensorsConfig.mBrakeBit] = System.Convert.ToByte( 0x0 );
                //}
                //mDevice.CANTxBuffer.Add( msg );
            }
        }

        private void GUI_RefreshTimer_Tick(object sender, EventArgs e)
        {
            List<GearShiftUsb.CANFixedTraceRecord> records = mDevice.CAN_Sniffer_FixedTraceRecordsTable;
            SuspendLayout();
            try
            {
                TableModel tableModel = snifferTable.TableModel;
                Row xpRow;

                snifferTable.BeginUpdate();
                // Equalize the number of rows with fixed trace records count
                int recordsCount = records.Count;
                int rowDifference = records.Count - snifferTable.RowCount;//snifferDGV.Rows.Count;
                if (rowDifference > 0)
                {
                    // add rows to DGV
                    for (int i = 0; i < rowDifference; i++)
                    {
                        xpRow = new Row();
                        //xpRow.Font = new Font(xpRow.Font, FontStyle.Bold);
                        for (int r = 0; r < 9; r++)
                        {
                            xpRow.Cells.Add(new Cell(""));
                        }
                        tableModel.Rows.Add(xpRow);
                    }
                }
                if (rowDifference < 0)
                {
                    // delete DGV rows
                    for (int i = 0; i > rowDifference; i--)
                    {
                        tableModel.Rows.RemoveAt(0);
                    }
                }

                for (int r = 0; r < records.Count; r++)
                {
                    GearShiftUsb.CANFixedTraceRecord rec = records[r];
                    Row row = snifferTable.TableModel.Rows[r];
                    string msgData = string.Empty;
                    if (rec.LastMessage.isRTRFrame == 1)
                    {
                        msgData = "Remote Request";
                    }
                    else
                    {
                        for (int i = 0; i < Math.Min((int)rec.LastMessage.DLC, (int)8); i++)
                        {
                            msgData += rec.LastMessage.data[i].ToString("X2") + " ";
                        }
                        if (msgData.Length > 0)
                        {
                            msgData.Remove(msgData.Length - 1);
                        }
                    }
                    // Assign Remote ID
                    if (rec.LastMessage.isXtdFrameType != 0)
                    {
                        row.Cells[0].Text = "0x" + rec.LastMessage.remoteID.ToString("X8");
                    }
                    else
                    {
                        row.Cells[0].Text = "0x" + rec.LastMessage.remoteID.ToString("X3");
                    }
                    row.Cells[1].Text = rec.LastMessage.DLC.ToString();
                    row.Cells[2].Text = msgData;
                    row.Cells[3].Text = rec.MsgCount.ToString();
                    row.Cells[4].Text = rec.LastTimeStamp.ToString("0.0");
                    // Assign instantenous cycle time
                    if (rec.CycleTimeInstMs == -1)
                    {
                        row.Cells[5].Text = "N/A";
                    }
                    else
                    {
                        row.Cells[5].Text = rec.CycleTimeInstMs.ToString("0.0");
                    }
                    // Assign average cycle time
                    if (rec.CycleTimeAvgMs == -1)
                    {
                        row.Cells[6].Text = "N/A";
                    }
                    else
                    {
                        row.Cells[6].Text = rec.CycleTimeAvgMs.ToString("0.0");
                    }
                    // Assign minimal cycle time
                    if (rec.CycleTimeMinMs == -1)
                    {
                        row.Cells[7].Text = "N/A";
                    }
                    else
                    {
                        row.Cells[7].Text = rec.CycleTimeMinMs.ToString("0.0");
                    }
                    // Assign maximal cycle time
                    if (rec.CycleTimeMaxMs == -1)
                    {
                        row.Cells[8].Text = "N/A";
                    }
                    else
                    {
                        row.Cells[8].Text = rec.CycleTimeMaxMs.ToString("0.0");
                    }
                }
                snifferTable.EndUpdate();
            }
            catch (Exception ex)
            {
                Console.WriteLine("CanPanelRefreshTmrErr: " + ex.Message);
            }

            foreach (GearShiftUsb.CANFixedTraceRecord rec in records)
            {

                // check if the message "belongs" to one of the monitored nodes
                // Note that the iteration is not skipped when matching node found,
                // because one node might update few sensors that have data at different positions

                // brake
                //if (rec.LastMessage.remoteID == mGearboxCanCfg.mSensorsConfig.mBrakeID && !drv_BrakeUserEnableRB.Checked)
                //{
                //  if (rec.LastMessage.data[mGearboxCanCfg.mSensorsConfig.mBrakeBit] == mGearboxCanCfg.mSensorsConfig.mBrakeOn)
                //  {
                //    drv_BrakeButton.IsOn = true;
                //  }
                //  else
                //  {
                //    drv_BrakeButton.IsOn = false;
                //  }
                //}
                // gear
                if (rec.LastMessage.remoteID == mGearboxCanCfg.mSensorsConfig.mGearID)
                {

                }
                // lockup
                //if (rec.LastMessage.remoteID == mGearboxCanCfg.mSensorsConfig.mLUID && !drv_LUUserEnableRB.Checked)
                //{
                //  if (rec.LastMessage.data[mGearboxCanCfg.mSensorsConfig.mLUBit] == mGearboxCanCfg.mSensorsConfig.mLUOff)
                //  {
                //    drv_LockUpButton.IsOn = false;
                //  }
                //  if (rec.LastMessage.data[mGearboxCanCfg.mSensorsConfig.mLUBit] == mGearboxCanCfg.mSensorsConfig.mLUOn)
                //  {
                //    drv_LockUpButton.IsOn = true;
                //  }
                //}

                // engine RPM
                if (rec.LastMessage.remoteID == mGearboxCanCfg.mSensorsConfig.EngineRPMEntry.MsgID && !drv_EngineRPMGauge.EnableUserControl)
                {
                    double value = rec.LastMessage.data[mGearboxCanCfg.mSensorsConfig.EngineRPMEntry.LsbIndex] - mGearboxCanCfg.mSensorsConfig.EngineRPMEntry.ValuePreOffset;
                    if (mGearboxCanCfg.mSensorsConfig.EngineRPMEntry.MsbIndex > -1)
                    {
                        value += (UInt32)(rec.LastMessage.data[mGearboxCanCfg.mSensorsConfig.EngineRPMEntry.MsbIndex]) * 256;
                    }
                    value *= mGearboxCanCfg.mSensorsConfig.EngineRPMEntry.ValueMultiplier;
                    value -= mGearboxCanCfg.mSensorsConfig.EngineRPMEntry.ValuePostOffset;
                    drv_EngineRPMGauge.Value = (float)value;
                }
                // engine Load
                if (rec.LastMessage.remoteID == mGearboxCanCfg.mSensorsConfig.EngineLoadEntry.MsgID && !drv_EngineLoadGauge.EnableUserControl)
                {
                    double value = rec.LastMessage.data[mGearboxCanCfg.mSensorsConfig.EngineLoadEntry.LsbIndex] - mGearboxCanCfg.mSensorsConfig.EngineLoadEntry.ValuePreOffset;
                    if (mGearboxCanCfg.mSensorsConfig.EngineLoadEntry.MsbIndex > -1)
                    {
                        value += (UInt32)(rec.LastMessage.data[mGearboxCanCfg.mSensorsConfig.EngineLoadEntry.MsbIndex]) * 256;
                    }
                    value *= mGearboxCanCfg.mSensorsConfig.EngineLoadEntry.ValueMultiplier;
                    value -= mGearboxCanCfg.mSensorsConfig.EngineLoadEntry.ValuePostOffset;
                    drv_EngineLoadGauge.Value = (float)value;
                }
                // input RPM
                if (rec.LastMessage.remoteID == mGearboxCanCfg.mSensorsConfig.InputShaftRPMEntry.MsgID && !drv_TurbineGauge.EnableUserControl)
                {
                    double value = rec.LastMessage.data[mGearboxCanCfg.mSensorsConfig.InputShaftRPMEntry.LsbIndex] - mGearboxCanCfg.mSensorsConfig.InputShaftRPMEntry.ValuePreOffset;
                    if (mGearboxCanCfg.mSensorsConfig.InputShaftRPMEntry.MsbIndex > -1)
                    {
                        value += (UInt32)(rec.LastMessage.data[mGearboxCanCfg.mSensorsConfig.InputShaftRPMEntry.MsbIndex]) * 256;
                    }
                    value *= mGearboxCanCfg.mSensorsConfig.InputShaftRPMEntry.ValueMultiplier;
                    value -= mGearboxCanCfg.mSensorsConfig.InputShaftRPMEntry.ValuePostOffset;
                    drv_TurbineGauge.Value = (float)value;
                }
                // output RPM
                if (rec.LastMessage.remoteID == mGearboxCanCfg.mSensorsConfig.OutputShaftRPMEntry.MsgID && !drv_OutputGauge.EnableUserControl)
                {
                    double value = rec.LastMessage.data[mGearboxCanCfg.mSensorsConfig.OutputShaftRPMEntry.LsbIndex] - mGearboxCanCfg.mSensorsConfig.OutputShaftRPMEntry.ValuePreOffset;
                    if (mGearboxCanCfg.mSensorsConfig.OutputShaftRPMEntry.MsbIndex > -1)
                    {
                        value += (UInt32)(rec.LastMessage.data[mGearboxCanCfg.mSensorsConfig.OutputShaftRPMEntry.MsbIndex]) * 256;
                    }
                    value *= mGearboxCanCfg.mSensorsConfig.OutputShaftRPMEntry.ValueMultiplier;
                    value -= mGearboxCanCfg.mSensorsConfig.OutputShaftRPMEntry.ValuePostOffset;
                    drv_OutputGauge.Value = (float)value;
                }
                // temperature
                if (rec.LastMessage.remoteID == mGearboxCanCfg.mSensorsConfig.TempEntry.MsgID && !drv_TempGauge.EnableUserControl)
                {
                    double value = rec.LastMessage.data[mGearboxCanCfg.mSensorsConfig.TempEntry.LsbIndex] - mGearboxCanCfg.mSensorsConfig.TempEntry.ValuePreOffset;
                    if (mGearboxCanCfg.mSensorsConfig.TempEntry.MsbIndex > -1)
                    {
                        value += (UInt32)(rec.LastMessage.data[mGearboxCanCfg.mSensorsConfig.TempEntry.MsbIndex]) * 256;
                    }
                    value *= mGearboxCanCfg.mSensorsConfig.TempEntry.ValueMultiplier;
                    value -= mGearboxCanCfg.mSensorsConfig.TempEntry.ValuePostOffset;
                    drv_TempGauge.Value = (float)value;
                }
                //if (rec.LastMessage.remoteID == mGearboxCanCfg.mSensorsConfig.mTempID && !drv_TempGauge.EnableUserControl)
                //{
                //  drv_TempGauge.Value = rec.LastMessage.data[mGearboxCanCfg.mSensorsConfig.mTempBit];
                //}

                // throttle position sensor
                if (rec.LastMessage.remoteID == mGearboxCanCfg.mSensorsConfig.TPSEntry.MsgID && !drv_TPSGauge.EnableUserControl)
                {
                    double value = rec.LastMessage.data[mGearboxCanCfg.mSensorsConfig.TPSEntry.LsbIndex] - mGearboxCanCfg.mSensorsConfig.TPSEntry.ValuePreOffset;
                    if (mGearboxCanCfg.mSensorsConfig.TPSEntry.MsbIndex > -1)
                    {
                        value += (UInt32)(rec.LastMessage.data[mGearboxCanCfg.mSensorsConfig.TPSEntry.MsbIndex]) * 256;
                    }
                    value *= mGearboxCanCfg.mSensorsConfig.TPSEntry.ValueMultiplier;
                    value -= mGearboxCanCfg.mSensorsConfig.TPSEntry.ValuePostOffset;
                    drv_TPSGauge.Value = (float)value;
                }

            }
            ResumeLayout();
        }

        private void selectConfigComboBox_DropDown(object sender, EventArgs e)
        {
            try
            {
                selectConfigComboBox.Items.Clear();
                string pathToSeek = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\GearShift Technologies\\GearShift\\Installed gearboxes";
                string[] filePaths = Directory.GetFiles(pathToSeek, "*.ccf", SearchOption.AllDirectories);

                foreach (string filePath in filePaths)
                {

                    NiceComboBoxCustomItem item = new NiceComboBoxCustomItem();
                    item.DisplayedName = Path.GetFileNameWithoutExtension(filePath);
                    item.ObjectToStore = filePath;
                    selectConfigComboBox.Items.Add(item);
                }
            }
            catch (Exception)
            {
                throw new DirectoryNotFoundException("Either the application was improperly installed or the CAN tests directory was removed by user");
            }
        }

        private void selectConfigComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectConfigComboBox.SelectedIndex != -1)
            {
                string filePath = ((String)((NiceComboBoxCustomItem)selectConfigComboBox.SelectedItem).ObjectToStore);
                GearboxCANConfig ccfg = new GearboxCANConfig();
                ccfg = ccfg.OpenXml(filePath);
                this.GearboxCanCfg = ccfg;
                int x = 9;
                int y = x;
            }
        }

        private void enableCANButton_Click(object sender, EventArgs e)
        {
            if (!mDevice.IsConnected)
            {
                return;
            }
            StartCanTransmission();
        }

        private void disableCANButton_Click(object sender, EventArgs e)
        {
            if (!mDevice.IsConnected)
            {
                return;
            }
            mDevice.StopCANTransmission();
        }

        private void ToggleBusTerminationButton_Click(object sender, EventArgs e)
        {
            if (!mDevice.IsConnected)
            {
                return;
            }
            if (mDevice.CANTerminationEnabled)
            {
                mDevice.DisableCANBusTermination();
            }
            else
            {
                mDevice.EnableCANBusTermination();
            }
        }

        private void resetSnifferButton_Click(object sender, EventArgs e)
        {
            mDevice.CANResetFixedTrace();
        }

        private void newConfigButton_Click(object sender, EventArgs e)
        {
            // TODO Warn if there's unsaved config being edited
            this.GearboxCanCfg = new GearboxCANConfig();
        }

        private void DeviceCAN_TerminationEnabledEH(object sender, EventArgs e)
        {
            ToggleBusTerminationButton.Image = GST.Gearshift.Components.Properties.Resources.CanPanel_TRM_ENBLD_48x48;
        }

        private void DeviceCAN_TerminationDisabledEH(object sender, EventArgs e)
        {
            ToggleBusTerminationButton.Image = GST.Gearshift.Components.Properties.Resources.CanPanel_TRM_DSBLD_48x48;
        }

        private void AdjustControlsSizes(object sender, EventArgs e)
        {
            tabControl.Size = new Size(this.Width, this.Height - tabControl.Top);
            nicePanel2.Width = this.Width - nicePanel2.Left;
            niceChunk4.Width = this.Width - niceChunk4.Left - 3;

        }

        private void InputShaftEntryEditor_ValueEdited(object sender, EventArgs e)
        {
            drv_TurbineGauge.MinValue = (int)mGearboxCanCfg.mSensorsConfig.InputShaftRPMEntry.ValueMin;
            drv_TurbineGauge.MaxValue = (int)mGearboxCanCfg.mSensorsConfig.InputShaftRPMEntry.ValueMax;
            drv_TurbineGauge.ScaleLinesMajorStepValue = (int)Math.Abs((int)drv_TurbineGauge.MaxValue - (int)drv_TurbineGauge.MinValue) / 20;
        }

        private void OutputShaftEntryEditor_ValueEdited(object sender, EventArgs e)
        {
            drv_OutputGauge.MinValue = (int)mGearboxCanCfg.mSensorsConfig.OutputShaftRPMEntry.ValueMin;
            drv_OutputGauge.MaxValue = (int)mGearboxCanCfg.mSensorsConfig.OutputShaftRPMEntry.ValueMax;
            drv_OutputGauge.ScaleLinesMajorStepValue = (int)Math.Abs((int)drv_OutputGauge.MaxValue - (int)drv_OutputGauge.MinValue) / 20;
        }

        private void TempEntryEditor_ValueEdited(object sender, EventArgs e)
        {

        }

        private void TPSEntryEditor_ValueEdited(object sender, EventArgs e)
        {

        }

        private void EngineRPMCanEntryEditor_ValueEdited(object sender, EventArgs e)
        {
            drv_EngineRPMGauge.MinValue = (int)mGearboxCanCfg.mSensorsConfig.EngineRPMEntry.ValueMin;
            drv_EngineRPMGauge.MaxValue = (int)mGearboxCanCfg.mSensorsConfig.EngineRPMEntry.ValueMax;
            drv_EngineRPMGauge.ScaleLinesMajorStepValue = (int)Math.Abs((int)drv_EngineRPMGauge.MaxValue - (int)drv_EngineRPMGauge.MinValue) / 20;
        }

        private void EngineLoadCanEntryEditor_ValueEdited(object sender, EventArgs e)
        {

        }

        void mDevice_CAN_CanPlaybackStopped(object sender, EventArgs e)
        {
            resetSnifferButton.Enabled = true;
            recPbkBtn.Enabled = true;
            stopPbkBtn.Enabled = false;
            tracesComboBox.Enabled = true;
            playPbkBtn.Enabled = true;
        }

        void mDevice_CAN_CanPlaybackStarted(object sender, EventArgs e)
        {
            resetSnifferButton.Enabled = false;
            recPbkBtn.Enabled = false;
            stopPbkBtn.Enabled = false;
            tracesComboBox.Enabled = false;
            playPbkBtn.Enabled = false;
        }

        void mDevice_CAN_CanCordingStopped(object sender, EventArgs e)
        {
            recPbkBtn.Image = GST.Gearshift.Components.Properties.Resources.CanPanel_Gnome_Media_Record_48;
            recordBtnBlinkerTimer.Enabled = false;
            resetSnifferButton.Enabled = true;
            stopPbkBtn.Enabled = false;
            tracesComboBox.Enabled = true;

        }

        void mDevice_CAN_CanCordingStarted(object sender, EventArgs e)
        {
            resetSnifferButton.Enabled = false;
            stopPbkBtn.Enabled = true;
            mRecBtnBlinkerToggle = false;
            recordBtnBlinkerTimer.Enabled = true;
            tracesComboBox.Enabled = false;

        }

        #region Double click gauge to edit CAN properties

        private void drv_TurbineGauge_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                GaugeDataEditor gde = new GaugeDataEditor(mGearboxCanCfg.mSensorsConfig.InputShaftRPMEntry);
                gde.ShowDialog();
                drv_TurbineGauge.MinValue = (int)mGearboxCanCfg.mSensorsConfig.InputShaftRPMEntry.ValueMin;
                drv_TurbineGauge.MaxValue = (int)mGearboxCanCfg.mSensorsConfig.InputShaftRPMEntry.ValueMax;
                drv_TurbineGauge.ScaleLinesMajorStepValue = (int)Math.Abs((int)drv_TurbineGauge.MaxValue - (int)drv_TurbineGauge.MinValue) / 20;
                InputShaftEntryEditor.CanEntry = mGearboxCanCfg.mSensorsConfig.InputShaftRPMEntry;
            }
        }

        private void drv_EngineRPMGauge_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                GaugeDataEditor gde = new GaugeDataEditor(mGearboxCanCfg.mSensorsConfig.EngineRPMEntry);
                gde.ShowDialog();
                drv_EngineRPMGauge.MinValue = (int)mGearboxCanCfg.mSensorsConfig.EngineRPMEntry.ValueMin;
                drv_EngineRPMGauge.MaxValue = (int)mGearboxCanCfg.mSensorsConfig.EngineRPMEntry.ValueMax;
                drv_EngineRPMGauge.ScaleLinesMajorStepValue = (int)Math.Abs((int)drv_EngineRPMGauge.MaxValue - (int)drv_EngineRPMGauge.MinValue) / 20;
                EngineRPMCanEntryEditor.CanEntry = mGearboxCanCfg.mSensorsConfig.EngineRPMEntry;
            }
        }

        private void drv_TPSGauge_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                GaugeDataEditor gde = new GaugeDataEditor(mGearboxCanCfg.mSensorsConfig.TPSEntry);
                gde.ShowDialog();
                drv_TPSGauge.MinValue = (int)mGearboxCanCfg.mSensorsConfig.TPSEntry.ValueMin;
                drv_TPSGauge.MaxValue = (int)mGearboxCanCfg.mSensorsConfig.TPSEntry.ValueMax;
                drv_TPSGauge.ScaleLinesMajorStepValue = (int)Math.Abs((int)drv_TPSGauge.MaxValue - (int)drv_TPSGauge.MinValue) / 20;
                TPSEntryEditor.CanEntry = mGearboxCanCfg.mSensorsConfig.TPSEntry;
            }
        }

        private void drv_OutputGauge_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                GaugeDataEditor gde = new GaugeDataEditor(mGearboxCanCfg.mSensorsConfig.OutputShaftRPMEntry);
                gde.ShowDialog();
                drv_OutputGauge.MinValue = (int)mGearboxCanCfg.mSensorsConfig.OutputShaftRPMEntry.ValueMin;
                drv_OutputGauge.MaxValue = (int)mGearboxCanCfg.mSensorsConfig.OutputShaftRPMEntry.ValueMax;
                drv_OutputGauge.ScaleLinesMajorStepValue = (int)Math.Abs((int)drv_OutputGauge.MaxValue - (int)drv_OutputGauge.MinValue) / 20;
                OutputShaftEntryEditor.CanEntry = mGearboxCanCfg.mSensorsConfig.OutputShaftRPMEntry;
            }
        }

        private void drv_EngineLoadGauge_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                GaugeDataEditor gde = new GaugeDataEditor(mGearboxCanCfg.mSensorsConfig.EngineLoadEntry);
                gde.ShowDialog();
                drv_EngineLoadGauge.MinValue = (int)mGearboxCanCfg.mSensorsConfig.EngineLoadEntry.ValueMin;
                drv_EngineLoadGauge.MaxValue = (int)mGearboxCanCfg.mSensorsConfig.EngineLoadEntry.ValueMax;
                drv_EngineLoadGauge.ScaleLinesMajorStepValue = (int)Math.Abs((int)drv_EngineLoadGauge.MaxValue - (int)drv_EngineLoadGauge.MinValue) / 20;
                EngineLoadCanEntryEditor.CanEntry = mGearboxCanCfg.mSensorsConfig.EngineLoadEntry;
            }
        }

        private void drv_TempGauge_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                GaugeDataEditor gde = new GaugeDataEditor(mGearboxCanCfg.mSensorsConfig.TempEntry);
                gde.ShowDialog();
                drv_TempGauge.MinValue = (int)mGearboxCanCfg.mSensorsConfig.TempEntry.ValueMin;
                drv_TempGauge.MaxValue = (int)mGearboxCanCfg.mSensorsConfig.TempEntry.ValueMax;
                drv_TempGauge.ScaleLinesMajorStepValue = (int)Math.Abs((int)drv_TempGauge.MaxValue - (int)drv_TempGauge.MinValue) / 4;
                TempEntryEditor.CanEntry = mGearboxCanCfg.mSensorsConfig.TempEntry;
            }
        }

        #endregion Double click gauge to edit CAN properties

        #endregion EventHandling



        private void recPbkBtn_Click(object sender, EventArgs e)
        {
            mDevice.StartCANCording();
        }

        private void sniff_TabPage_Load(object sender, EventArgs e)
        {
            //InitSnifferTable(snifferTable);
        }

        private void InitSnifferTable(XPTable.Models.Table table)
        {
            // The Table control on a form - already initialised
            table.BeginUpdate();
            table.EnableWordWrap = true;    // If false, then Cell.WordWrap is ignored
            table.SelectionStyle = SelectionStyle.Grid;
            table.GridLines = GridLines.Rows;

            TextColumn col1 = new TextColumn("Remote ID", 100);
            col1.ToolTipText = "Remote ID - The CAN bus address of the destination node";
            col1.Alignment = ColumnAlignment.Center;
            TextColumn col2 = new TextColumn("DLC", 35);
            col2.ToolTipText = "Data Length Code - Number of bytes carried in this message";
            col2.Alignment = ColumnAlignment.Center;
            TextColumn col3 = new TextColumn("Message", 200);
            col3.ToolTipText = "Last received message content";
            col3.Alignment = ColumnAlignment.Center;
            TextColumn col4 = new TextColumn("Count", 100);
            col4.ToolTipText = "Number of messages received with ID specific for a row";
            col4.Alignment = ColumnAlignment.Right;
            TextColumn col5 = new TextColumn("Timestamp [ms]", 100);
            col5.ToolTipText = "Timestamp of the last received message in miliseconds";
            col5.Alignment = ColumnAlignment.Right;
            TextColumn col6 = new TextColumn("Cycle time inst [ms]", 110);
            col6.ToolTipText = "Instantenous message cycle time in miliseconds";
            col6.Alignment = ColumnAlignment.Right;
            TextColumn col7 = new TextColumn("Cycle time avg [ms]", 110);
            col7.ToolTipText = "Average message cycle time in miliseconds";
            col7.Alignment = ColumnAlignment.Right;
            TextColumn col8 = new TextColumn("Cycle time min [ms]", 110);
            col8.ToolTipText = "Minimal observed message cycle time";
            col8.Alignment = ColumnAlignment.Right;
            TextColumn col9 = new TextColumn("Cycle time max [ms]", 110);
            col9.ToolTipText = "Maximal observed message cycle time";
            col9.Alignment = ColumnAlignment.Right;

            table.ColumnModel = new ColumnModel(new Column[] { col1, col2, col3, col4, col5, col6, col7, col8, col9 });

            // Add a single row to prevent crashes
            TableModel model = new TableModel();
            Row row;
            row = new Row();
            for (int r = 0; r < 9; r++)
            {
                row.Cells.Add(new Cell(""));
            }
            //model.Rows.Add(row);
            table.TableModel = model;

            table.EndUpdate();
        }

        private void recordBtnBlinkerTimer_Tick(object sender, EventArgs e)
        {
            if (mRecBtnBlinkerToggle)
            {
                recPbkBtn.Image = GST.Gearshift.Components.Properties.Resources.CanPanel_Gnome_Media_Record_BW_Light_48;
            }
            else
            {
                recPbkBtn.Image = GST.Gearshift.Components.Properties.Resources.CanPanel_Gnome_Media_Record_48;
            }
            mRecBtnBlinkerToggle = !mRecBtnBlinkerToggle;
        }

        private void stopPbkBtn_Click(object sender, EventArgs e)
        {
            mDevice.StopCANCording();
            CanTraceSaveDialog svd = new CanTraceSaveDialog(mDevice, new CANTrace());
            svd.ShowDialog();
        }

        private void tracesComboBox_DropDown(object sender, EventArgs e)
        {
            try
            {
                tracesComboBox.Items.Clear();
                string[] filePaths = GST.Gearshift.Components.Utilities.Settings.AvailableCanTracesPaths;

                foreach (string filePath in filePaths)
                {

                    NiceComboBoxCustomItem item = new NiceComboBoxCustomItem();
                    item.DisplayedName = Path.GetFileNameWithoutExtension(filePath);
                    item.ObjectToStore = filePath;
                    tracesComboBox.Items.Add(item);
                }
            }
            catch (Exception)
            {
                throw new DirectoryNotFoundException("Either the application was improperly installed or the CAN traces directory was removed by user");
            }
        }

        private void tracesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filePath = ((String)((NiceComboBoxCustomItem)tracesComboBox.SelectedItem).ObjectToStore);
            CANTrace ctr = new CANTrace();
            ctr = ctr.OpenXml(filePath);

            mDevice.CANPlaybackBuffer.AddRange(ctr.TraceData);
            canCorder1.LoadPlaybackData();

            playPbkBtn.Enabled = true;
        }

        private void playPbkBtn_Click(object sender, EventArgs e)
        {
            mDevice.StartCANPlayback();
        }

    }

}

