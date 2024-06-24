using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

using Soko.Common.Common;
using Soko.Common.Controls;
using GST.Gearshift.Components.Interfaces.USB;

using XPTable.Models;

using GST.Gearshift.Components.Utilities;
using GST.ZF6.Components.Interfaces.MechShifterUSB;
namespace GST.Gearshift.Components.Forms.DAQ
{
    [Serializable]
    public partial class TestScriptEditor : UserControl
    {



        #region Constants

        // Duty cycle columns offset
        public readonly int mDCColsOffset = 7;

        public readonly Color mLoopColor = Color.FromArgb(255, 236, 179);

        #endregion  Constants



        #region Private fields

        private TestScript mTestScript = null;

        private bool mTestScriptEdited = false;

        //private Color mLayoutColor1 = Color.Orange;
        //private Color mLayoutColor2 = Color.Peru;

        private int mTransactionStackSize = 0;

        #endregion Private fields



        #region Constructors & finalizer

        /// <summary>
        /// Default constructor
        /// </summary>
        public TestScriptEditor()
        {
            InitializeComponent();

            this.SetStyle(
                          ControlStyles.SupportsTransparentBackColor |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.ResizeRedraw |
                          ControlStyles.UserPaint, true);


            //scriptDGV.Rows.Clear();

            mTestScript = new TestScript();
            ManageDGVColumns();
            LoadTestScriptDGV();

            gearboxConfigPanel.SaveButtonClickedEvent += new EventHandler(GearboxEditorSaveButtonClicked);
            gearboxConfigPanel.EmbeddedModeEnabled = true;
        }

        #endregion Constructors & finalizer



        #region Events



        #endregion Events



        #region Properties

        private bool TestScriptEdited
        {
            get { return mTestScriptEdited; }
            set
            {
                mTestScriptEdited = value;
                if (mTestScriptEdited)
                {
                    //TSMasterDataComboBox.SelectedIndex = -1;
                    //TSMasterDataIndicator.IsOn = false;
                    //mTestScript.HasConsistentMasterData = false;
                }
            }
        }

        #endregion Properties



        #region Methods

        /// <summary>
        /// updates content of gerbox info labels according to the Gearbox Config included in test script
        /// </summary>
        private void UpdateGearboxControls()
        {
            if (mTestScript.GearboxCorrect)
            {
                gearboxNameLabelVar.Text = mTestScript.Gearbox.Name;
                gearboxManufacturerLabelVar.Text = mTestScript.Gearbox.Manufacturer;
                gearboxModelLabelVar.Text = mTestScript.Gearbox.Model;
                gearboxPicturePictureBox.Image = mTestScript.Gearbox.Picture;
            }
            else
            {
                gearboxNameLabelVar.Text = string.Empty;
                gearboxManufacturerLabelVar.Text = string.Empty;
                gearboxModelLabelVar.Text = string.Empty;
                gearboxPicturePictureBox.Image = null;
            }
        }

        private void GearboxEditorSaveButtonClicked(Object sender, EventArgs e)
        {
            Console.WriteLine(mTestScript.Gearbox.Name);
            //LoadGearbox(gearboxConfigPanel.GearboxConfig);

            SaveTestScriptDGV();
            mTestScript.Gearbox = gearboxConfigPanel.GearboxConfig;
            ManageDGVColumns();
            LoadTestScriptDGV();
            UpdateGearboxControls();
        }

        private void ManageScriptTableCols()
        {
            ///////////////////////////////////////////////////////////////////////////////////
            //Do job with scriptDGV columns
            scriptDGV.Columns.Clear();
            // Add step number column
            Soko.Common.Common.DataGridNumericTextBoxColumn stepNumberColumn = new Soko.Common.Common.DataGridNumericTextBoxColumn();
            stepNumberColumn.Name = "Step";
            stepNumberColumn.HeaderText = "Step";
            stepNumberColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            stepNumberColumn.ReadOnly = true;
            stepNumberColumn.Width = 50;
            scriptDGV.Columns.Add(stepNumberColumn);
            // Add Gear number column
            DataGridViewColumn gearColumn = new DataGridViewColumn();
            gearColumn.Name = "Gear";
            gearColumn.HeaderText = "Gear";
            gearColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            gearColumn.Width = 60;
            gearColumn.CellTemplate = new DataGridViewTextBoxCell();
            scriptDGV.Columns.Add(gearColumn);
            // Add duration length column
            DataGridViewNumericUpDownColumn durationColumn = new DataGridViewNumericUpDownColumn();
            durationColumn.DecimalPlaces = 0;
            durationColumn.Increment = 1;
            durationColumn.Minimum = 10;
            durationColumn.Maximum = 100000;
            durationColumn.Name = "Duration";
            durationColumn.HeaderText = "Duration";
            durationColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            durationColumn.ReadOnly = false;
            durationColumn.Width = 50;
            scriptDGV.Columns.Add(durationColumn);
            DataGridViewCheckBoxColumn ackDataColumn = new DataGridViewCheckBoxColumn();
            ackDataColumn.Name = "AckData";
            ackDataColumn.HeaderText = "AckData";
            DataGridViewCheckBoxCell ackCell = new DataGridViewCheckBoxCell();
            ackCell.Value = false;
            ackDataColumn.CellTemplate = ackCell;
            ackDataColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            ackDataColumn.Width = 28;
            scriptDGV.Columns.Add(ackDataColumn);
            DataGridViewCheckBoxColumn PassColumn = new DataGridViewCheckBoxColumn();
            PassColumn.Name = "PassColumn";
            PassColumn.HeaderText = "Pass";
            DataGridViewCheckBoxCell passCell = new DataGridViewCheckBoxCell();
            passCell.Value = false;
            PassColumn.CellTemplate = passCell;
            PassColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            PassColumn.Width = 28;
            PassColumn.DividerWidth = 1;
            scriptDGV.Columns.Add(PassColumn);

            // Add Analog Output 1 column
            DataGridViewNumericUpDownColumn AO1Column = new DataGridViewNumericUpDownColumn();

#if _DYNO_SUITE_
      AO1Column.Name = "RPMColumn";
      AO1Column.HeaderText = "Dyno RPM";
#endif
#if _VB_SUITE_
            AO1Column.Name = "FlowColumn";
            AO1Column.HeaderText = "Pressure [%]";
#endif
            AO1Column.Width = 70;
            AO1Column.DecimalPlaces = 0;
            AO1Column.Increment = Convert.ToDecimal(0.1f);
            AO1Column.Minimum = 0;
            AO1Column.Maximum = 10000;
            scriptDGV.Columns.Add(AO1Column);

            // Add Analog Output 2 column
            DataGridViewNumericUpDownColumn AO2Column = new DataGridViewNumericUpDownColumn();

#if _DYNO_SUITE_
      AO2Column.Name = "LoadColumn";
      AO2Column.HeaderText = "Dyno Load";
#endif
#if _VB_SUITE_
            AO2Column.Name = "PumpColumn";
            AO2Column.HeaderText = "Selector Position [%]";
#endif
            AO2Column.Width = 70;
            AO2Column.DecimalPlaces = 1;
            AO2Column.Increment = Convert.ToDecimal(0.1f);
            AO2Column.Minimum = 0;
            AO2Column.Maximum = 10000;
            AO2Column.DividerWidth = 2;
            scriptDGV.Columns.Add(AO2Column);

            // Up to this point the columns are the same for both Non-mechatronic and Zf6 6HP gearboxes
            switch (mTestScript.Gearbox.ControllerType)
            {
                default:
                case GearboxControllerType.NON_MECHATRONIC:
                    {
                        // If this and old type DAQ, non-mechatronix gearbox add the number of columns corresponding to the number of configured drivers
                        for (int i = 0; i < mTestScript.Gearbox.CurrentDisplayChannelsCount; i++)
                        {
                            DataGridViewNumericUpDownColumn dgvnuc = new DataGridViewNumericUpDownColumn();
                            DisplayChannel dchan = (DisplayChannel)mTestScript.Gearbox.CurrentDisplayChannelsSet.Channels[i];
                            dgvnuc.DecimalPlaces = 1;
                            dgvnuc.Increment = System.Convert.ToDecimal(0.1);
                            dgvnuc.Minimum = 0;
                            dgvnuc.Maximum = 5000;
                            dgvnuc.HeaderText = dchan.Label;//"Solenoid " + i.ToString();
                            dgvnuc.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                            dgvnuc.DividerWidth = 1;
                            dgvnuc.MinimumWidth = 45;
                            scriptDGV.Columns.Add(dgvnuc);
                        }
                        break;
                    }
                case GearboxControllerType.ZF_6HPxx_1911E:
                case GearboxControllerType.ZF_6HPxx_1911M:
                case GearboxControllerType.ZF_6HPxx_CE:
                case GearboxControllerType.ZF_6HPxx_CM:
                case GearboxControllerType.ZF_6HPxx_TUCE:
                case GearboxControllerType.ZF_6HPxx_TUCM:
                case GearboxControllerType.ZF_6HPxx_WM:
                    {
                        // If this is a Zf6 controlled gearbox, add just one column that will be used to store the gear number to be selected
                        // 0.5 increment allows to denote the change between the gears and provides compatibility for up/down manual shifting.
                        DataGridViewNumericUpDownColumn dgvnuc = new DataGridViewNumericUpDownColumn();
                        dgvnuc.DecimalPlaces = 1;
                        dgvnuc.Increment = System.Convert.ToDecimal(0.5);
                        // -1 is assumed to be reverse, 0 neutral
                        dgvnuc.Minimum = -1;
                        // 6 as the highest gear.
                        dgvnuc.Maximum = 6;
                        dgvnuc.HeaderText = "Selected gear";
                        dgvnuc.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        dgvnuc.DividerWidth = 1;
                        dgvnuc.MinimumWidth = 45;
                        dgvnuc.Width = 60;
                        scriptDGV.Columns.Add(dgvnuc);
                        // Add column for ED55 control
                        dgvnuc = new DataGridViewNumericUpDownColumn();
                        dgvnuc.DecimalPlaces = 0;
                        dgvnuc.Increment = System.Convert.ToDecimal(1);
                        dgvnuc.Minimum = 0;
                        // 100% max
                        dgvnuc.Maximum = 100;
                        dgvnuc.HeaderText = "EDS5";
                        dgvnuc.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        dgvnuc.DividerWidth = 1;
                        dgvnuc.MinimumWidth = 45;
                        dgvnuc.Width = 60;
                        scriptDGV.Columns.Add(dgvnuc);
                        // Add column for ED56 control
                        dgvnuc = new DataGridViewNumericUpDownColumn();
                        dgvnuc.DecimalPlaces = 0;
                        dgvnuc.Increment = System.Convert.ToDecimal(1);
                        dgvnuc.Minimum = 0;
                        // 100% max
                        dgvnuc.Maximum = 100;
                        dgvnuc.HeaderText = "EDS6";
                        dgvnuc.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        dgvnuc.DividerWidth = 1;
                        dgvnuc.MinimumWidth = 45;
                        dgvnuc.Width = 60;
                        scriptDGV.Columns.Add(dgvnuc);
                        break;
                    }

                case GearboxControllerType.NISSAN_RE5:
                    {
                        // If this is Nissan RE5 gearbox add 7 columns, 1 for each solenoid
                        DataGridViewNumericUpDownColumn dgvnuc;
                        // L/U solenoid
                        dgvnuc = new DataGridViewNumericUpDownColumn();
                        dgvnuc.DecimalPlaces = 0;
                        dgvnuc.Increment = System.Convert.ToDecimal(1);
                        dgvnuc.Minimum = 0;
                        dgvnuc.Maximum = 100;
                        dgvnuc.HeaderText = "L/U";
                        dgvnuc.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dgvnuc.DividerWidth = 1;
                        dgvnuc.MinimumWidth = 45;
                        scriptDGV.Columns.Add(dgvnuc);
                        // PL solenoid
                        dgvnuc = new DataGridViewNumericUpDownColumn();
                        dgvnuc.DecimalPlaces = 0;
                        dgvnuc.Increment = System.Convert.ToDecimal(1);
                        dgvnuc.Minimum = 0;
                        dgvnuc.Maximum = 100;
                        dgvnuc.HeaderText = "PL";
                        dgvnuc.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dgvnuc.DividerWidth = 1;
                        dgvnuc.MinimumWidth = 45;
                        scriptDGV.Columns.Add(dgvnuc);
                        // I/C solenoid
                        dgvnuc = new DataGridViewNumericUpDownColumn();
                        dgvnuc.DecimalPlaces = 0;
                        dgvnuc.Increment = System.Convert.ToDecimal(1);
                        dgvnuc.Minimum = 0;
                        dgvnuc.Maximum = 100;
                        dgvnuc.HeaderText = "I/C";
                        dgvnuc.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dgvnuc.DividerWidth = 1;
                        dgvnuc.MinimumWidth = 45;
                        scriptDGV.Columns.Add(dgvnuc);
                        // FR/B solenoid
                        dgvnuc = new DataGridViewNumericUpDownColumn();
                        dgvnuc.DecimalPlaces = 0;
                        dgvnuc.Increment = System.Convert.ToDecimal(1);
                        dgvnuc.Minimum = 0;
                        dgvnuc.Maximum = 100;
                        dgvnuc.HeaderText = "FR/B";
                        dgvnuc.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dgvnuc.DividerWidth = 1;
                        dgvnuc.MinimumWidth = 45;
                        scriptDGV.Columns.Add(dgvnuc);
                        // D/C solenoid
                        dgvnuc = new DataGridViewNumericUpDownColumn();
                        dgvnuc.DecimalPlaces = 0;
                        dgvnuc.Increment = System.Convert.ToDecimal(1);
                        dgvnuc.Minimum = 0;
                        dgvnuc.Maximum = 100;
                        dgvnuc.HeaderText = "D/C";
                        dgvnuc.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dgvnuc.DividerWidth = 1;
                        dgvnuc.MinimumWidth = 45;
                        scriptDGV.Columns.Add(dgvnuc);
                        // H&LR/C solenoid
                        dgvnuc = new DataGridViewNumericUpDownColumn();
                        dgvnuc.DecimalPlaces = 0;
                        dgvnuc.Increment = System.Convert.ToDecimal(1);
                        dgvnuc.Minimum = 0;
                        dgvnuc.Maximum = 100;
                        dgvnuc.HeaderText = "H&LR/C";
                        dgvnuc.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dgvnuc.DividerWidth = 1;
                        dgvnuc.MinimumWidth = 45;
                        scriptDGV.Columns.Add(dgvnuc);
                        // LC/B solenoid
                        dgvnuc = new DataGridViewNumericUpDownColumn();
                        dgvnuc.DecimalPlaces = 0;
                        dgvnuc.Increment = System.Convert.ToDecimal(100);
                        dgvnuc.Minimum = 0;
                        dgvnuc.Maximum = 100;
                        dgvnuc.HeaderText = "LC/B";
                        dgvnuc.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dgvnuc.DividerWidth = 1;
                        dgvnuc.MinimumWidth = 45;
                        scriptDGV.Columns.Add(dgvnuc);

                        break;
                    }
                case GearboxControllerType.GM6T40:
                case GearboxControllerType.GM6T70:
                case GearboxControllerType.GM6L:
                    {
                        // CANBUS engine speed value column
                        DataGridViewNumericUpDownColumn dgvnuc = new DataGridViewNumericUpDownColumn();
                        dgvnuc.DecimalPlaces = 0;
                        dgvnuc.Increment = System.Convert.ToDecimal(1);
                        dgvnuc.Minimum = 0;
                        dgvnuc.Maximum = 6000;
                        dgvnuc.HeaderText = "CANBUS Engine RPM";
                        dgvnuc.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        dgvnuc.DividerWidth = 1;
                        dgvnuc.MinimumWidth = 45;
                        dgvnuc.Width = 60;
                        scriptDGV.Columns.Add(dgvnuc);
                        // CANBUS TPS value column
                        dgvnuc = new DataGridViewNumericUpDownColumn();
                        dgvnuc.DecimalPlaces = 0;
                        dgvnuc.Increment = System.Convert.ToDecimal(1);
                        dgvnuc.Minimum = 0;
                        dgvnuc.Maximum = 100;
                        dgvnuc.HeaderText = "CANBUS TPS [%]";
                        dgvnuc.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        dgvnuc.DividerWidth = 1;
                        dgvnuc.MinimumWidth = 45;
                        dgvnuc.Width = 60;
                        scriptDGV.Columns.Add(dgvnuc);
                        // CANBUS Torque column
                        dgvnuc = new DataGridViewNumericUpDownColumn();
                        dgvnuc.DecimalPlaces = 0;
                        dgvnuc.Increment = System.Convert.ToDecimal(1);
                        dgvnuc.Minimum = 0;
                        dgvnuc.Maximum = 100;
                        dgvnuc.HeaderText = "CANBUS Torque";
                        dgvnuc.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        dgvnuc.DividerWidth = 1;
                        dgvnuc.MinimumWidth = 45;
                        dgvnuc.Width = 60;
                        scriptDGV.Columns.Add(dgvnuc);
                        // SSEMU Input speed value
                        dgvnuc = new DataGridViewNumericUpDownColumn();
                        dgvnuc.DecimalPlaces = 0;
                        dgvnuc.Increment = System.Convert.ToDecimal(1);
                        // -1 is assumed to be reverse, 0 neutral
                        dgvnuc.Minimum = 0;
                        // 6 as the highest gear.
                        dgvnuc.Maximum = 6000;
                        dgvnuc.HeaderText = "SSEMU Input speed";
                        dgvnuc.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        dgvnuc.DividerWidth = 1;
                        dgvnuc.MinimumWidth = 45;
                        dgvnuc.Width = 60;
                        scriptDGV.Columns.Add(dgvnuc);

                        // SSEMU Onput speed value
                        dgvnuc = new DataGridViewNumericUpDownColumn();
                        dgvnuc.DecimalPlaces = 0;
                        dgvnuc.Increment = System.Convert.ToDecimal(1);
                        dgvnuc.Minimum = 0;
                        dgvnuc.Maximum = 3000;
                        dgvnuc.HeaderText = "SSEMU Onput speed";
                        dgvnuc.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        dgvnuc.DividerWidth = 1;
                        dgvnuc.MinimumWidth = 45;
                        dgvnuc.Width = 60;
                        scriptDGV.Columns.Add(dgvnuc);

                        // If this is GM 6Txx gearbox add 1 column just for TCC on/off
                        // If this is a Zf6 controlled gearbox, add just one column that will be used to store the gear number to be selected
                        // 0.5 increment allows to denote the change between the gears and provides compatibility for up/down manual shifting.
                        dgvnuc = new DataGridViewNumericUpDownColumn();
                        dgvnuc.DecimalPlaces = 1;
                        dgvnuc.Increment = System.Convert.ToDecimal(0.5);
                        // -1 is assumed to be reverse, 0 neutral
                        dgvnuc.Minimum = -1;
                        // 6 as the highest gear.
                        dgvnuc.Maximum = 6;
                        dgvnuc.HeaderText = "Selected gear";
                        dgvnuc.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        dgvnuc.DividerWidth = 1;
                        dgvnuc.MinimumWidth = 45;
                        dgvnuc.Width = 60;
                        scriptDGV.Columns.Add(dgvnuc);
                        // L/U solenoid
                        dgvnuc = new DataGridViewNumericUpDownColumn();
                        dgvnuc.DecimalPlaces = 0;
                        dgvnuc.Increment = System.Convert.ToDecimal(1);
                        dgvnuc.Minimum = 0;
                        dgvnuc.Maximum = 1;
                        dgvnuc.HeaderText = "TCC";
                        dgvnuc.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dgvnuc.DividerWidth = 1;
                        dgvnuc.MinimumWidth = 45;
                        scriptDGV.Columns.Add(dgvnuc);
                        break;
                    }
            }
            // Add a single row for a start
            scriptDGV.Rows.Add();
        }

        private void LoadPassFailTableCols()
        {
            passFailTable.BeginUpdate();
            passFailTable.EnableWordWrap = true;    // If false, then Cell.WordWrap is ignored
            passFailTable.ColumnResizing = true;
            passFailTable.SelectionStyle = SelectionStyle.Grid;
            passFailTable.GridLines = GridLines.Both;
            passFailTable.AllowSelection = true;

            passFailTable.ColumnModel = new ColumnModel();
            passFailTable.ColumnModel.HeaderHeight = 40;
            passFailTable.ColumnModel.Columns.Clear();
            // Add step number column
            TextColumn col0 = new TextColumn("Step", 50);
            col0.ToolTipText = "Script line number";
            col0.Editable = false;
            col0.Alignment = ColumnAlignment.Right;
            col0.AutoResizeMode = ColumnAutoResizeMode.Grow;
            passFailTable.ColumnModel.Columns.Add(col0);
            // Add Gear name column
            TextColumn col1 = new TextColumn("Gear", 100);
            col1.ToolTipText = "Gear name";
            col1.Editable = false;
            col1.Alignment = ColumnAlignment.Center;
            col1.AutoResizeMode = ColumnAutoResizeMode.Grow;
            passFailTable.ColumnModel.Columns.Add(col1);

            for (int i = 0; i < mTestScript.Gearbox._analogueInputs.Count; i++)
            {
                GearShiftUsb.AIChannel aic = mTestScript.Gearbox._analogueInputs[i];
                //DisplayChannel dchan = (DisplayChannel)mTestScript.Gearbox.PressureDisplayChannelsSet.Channels[i];
                NumberColumn colL = new NumberColumn(aic.Label, 50);
                colL.Increment = (decimal)0.1;
                colL.Maximum = (decimal)aic.MaxValueUserUnit;
                colL.Minimum = (decimal)aic.MinValueUserUnit;
                colL.Sortable = false;
                colL.Editable = true;
                colL.AutoResizeMode = ColumnAutoResizeMode.Grow;
                colL.ToolTipText = "Expected " + aic.Label + " value accepted for this line";
                passFailTable.ColumnModel.Columns.Add(colL);
            }
            passFailTable.TableModel = new TableModel();
            passFailTable.EndUpdate();
        }

        private void LoadPromptsTableCols()
        {
            promptsTable.BeginUpdate();
            promptsTable.EnableWordWrap = true;    // If false, then Cell.WordWrap is ignored
            promptsTable.ColumnResizing = true;
            promptsTable.SelectionStyle = SelectionStyle.Grid;
            promptsTable.GridLines = GridLines.Both;
            promptsTable.AllowSelection = true;

            promptsTable.ColumnModel = new ColumnModel();
            promptsTable.ColumnModel.HeaderHeight = 40;
            promptsTable.ColumnModel.Columns.Clear();
            // Add step number column
            TextColumn col0 = new TextColumn("Step", 50);
            col0.ToolTipText = "Script line number";
            col0.Editable = false;
            col0.Alignment = ColumnAlignment.Right;
            col0.AutoResizeMode = ColumnAutoResizeMode.Grow;
            promptsTable.ColumnModel.Columns.Add(col0);
            // Add Gear name column
            TextColumn col1 = new TextColumn("Gear", 100);
            col1.ToolTipText = "Gear name";
            col1.Editable = false;
            col1.Alignment = ColumnAlignment.Center;
            col1.AutoResizeMode = ColumnAutoResizeMode.Grow;
            promptsTable.ColumnModel.Columns.Add(col1);
            // Add show checkbox column
            CheckBoxColumn col2 = new CheckBoxColumn("Show prompt", 80);
            col2.ToolTipText = "If the user prompt should be shown after this line";
            col2.Alignment = ColumnAlignment.Center;
            col2.CheckStyle = CheckBoxColumnStyle.CheckBox;
            promptsTable.ColumnModel.Columns.Add(col2);
            TextColumn col3 = new TextColumn("Message text", 300);
            col3.ToolTipText = "User prompt message text";
            promptsTable.ColumnModel.Columns.Add(col3);

            promptsTable.TableModel = new TableModel();

            promptsTable.EndUpdate();
        }

        private void ManageDGVColumns()
        {

            ManageScriptTableCols();

            LoadPassFailTableCols();

            LoadPromptsTableCols();
        }

        private void LoadPassFailTableRows()
        {
            passFailTable.BeginUpdate();
            passFailTable.TableModel.Rows.Clear();
            List<GearShiftUsb.AIChannel> ais = mTestScript.Gearbox._analogueInputs;
            for (int i = 0; i < mTestScript.FrameSet.Count; i++)
            {
                TestScriptFrame frame = (TestScriptFrame)mTestScript.FrameSet[i];
                Row row = new Row();
                row.Cells.Add(new Cell((i + 1).ToString()));
                row.Cells.Add(new Cell(frame.FrameName));
                for (int j = 0; j < ais.Count; j++)
                {
                    // Limit the precision to 2 digits
                    float cellValue = MeasurementUnit.ConvertAIValueBaseUnitToUserUnit(frame.MasterPressureReadValues[j], ais[j].ValueType);
                    row.Cells.Add(new Cell(Convert.ToDecimal(Math.Round(cellValue, 2))));
                }
                passFailTable.TableModel.Rows.Add(row);
            }
            passFailTable.EndUpdate();
        }

        private Row GetNewPassFailTableRow()
        {
            Row row = new Row();
            row.Cells.Add(new Cell());
            row.Cells.Add(new Cell());
            for (int j = 0; j < mTestScript.Gearbox._analogueInputs.Count; j++)
            {
                row.Cells.Add(new Cell(Convert.ToDecimal(0.0)));
            }
            return row;
        }

        private void LoadPromptsTableRows()
        {
            promptsTable.BeginUpdate();
            promptsTable.TableModel.Rows.Clear();
            for (int i = 0; i < mTestScript.FrameSet.Count; i++)
            {
                TestScriptFrame frame = (TestScriptFrame)mTestScript.FrameSet[i];
                Row row = new Row();
                row.Cells.Add(new Cell((i + 1).ToString()));
                row.Cells.Add(new Cell(frame.FrameName));
                row.Cells.Add(new Cell("", frame.UserPromptEnabled));
                row.Cells.Add(new Cell(frame.UserPrompt));
                promptsTable.TableModel.Rows.Add(row);
            }
            promptsTable.EndUpdate();
        }

        private Row GetNewPromptsTableRow()
        {
            Row row = new Row();
            row.Cells.Add(new Cell());
            row.Cells.Add(new Cell());
            row.Cells.Add(new Cell("", false));
            row.Cells.Add(new Cell());
            return row;
        }

        /// <summary>
        /// loads the datagridview content from internal TestScript class
        /// </summary>
        private void LoadTestScriptDGV()
        {
            SuspendLayout();



            ///////////////////////////////////////////////////////////////////////////////////
            //Do job with scriptDGV rows
            scriptDGV.Rows.Clear();
            scriptDGV.RowTemplate.Height = 19;
            for (int i = 0; i < mTestScript.FrameSet.Count; i++)
            {
                //add appropriate number of rows
                scriptDGV.Rows.Add();
            }
            for (int i = 0; i < mTestScript.FrameSet.Count; i++)
            {
                DataGridViewRow row = scriptDGV.Rows[i];
                TestScriptFrame frame = (TestScriptFrame)mTestScript.FrameSet[i];
                DataGridViewTextBoxCell gcc = (DataGridViewTextBoxCell)row.Cells[1];
                gcc.Value = frame.FrameName;
                if (frame.IsPartOfTheLoop)
                {
                    row.DefaultCellStyle.BackColor = mLoopColor;
                }

                row.Cells[2].Value = frame.Duration.ToString();
                row.Cells[3].Value = frame.AcquireData;
                row.Cells[4].Value = frame.IsPassThrough;
                row.Cells[5].Value = frame.DynoMotorRPM;
                row.Cells[6].Value = frame.DynoLoadCurrent;
                
                switch (mTestScript.Gearbox.ControllerType)
                {
                    default:
                    case GearboxControllerType.NON_MECHATRONIC:
                        {
                            // If this is an old DAQ type script, assign the cells with solenoid drive data
                            for (int j = 0; j < mTestScript.Gearbox.CurrentDisplayChannelsCount; j++)
                            {
                                DisplayChannel dchan = (DisplayChannel)mTestScript.Gearbox.CurrentDisplayChannelsSet.Channels[j];
                                int index = dchan.InputChannelIndex;
                                if (index >= 9)
                                    index -= 9;
                                row.Cells[j + mDCColsOffset].Value = frame.ChannelDrives[index];
                            }
                            break;
                        }
                    case GearboxControllerType.ZF_6HPxx_1911E:
                    case GearboxControllerType.ZF_6HPxx_1911M:
                    case GearboxControllerType.ZF_6HPxx_CE:
                    case GearboxControllerType.ZF_6HPxx_CM:
                    case GearboxControllerType.ZF_6HPxx_TUCE:
                    case GearboxControllerType.ZF_6HPxx_TUCM:
                    case GearboxControllerType.ZF_6HPxx_WM:
                        {
                            // If this is an Zf6 switched script, fill the gear number cell
                            //row.Cells[mDCColsOffset].Value = frame.Zf6GearNumber;
                            row.Cells[mDCColsOffset].Value = frame.EnigmaGearNumber;
                            // Fill EDS5 cell value
                            row.Cells[mDCColsOffset + 1].Value = frame.EnigmaEDS5Value;
                            // Fill EDS6 cell value
                            row.Cells[mDCColsOffset + 2].Value = frame.EnigmaEDS6Value;
                            break;
                        }
                    case GearboxControllerType.NISSAN_RE5:
                        {
                            // If this is Nissan RE5 gearbox put the CAN solenoid drive values instead of DAQ values
                            for (int j = 0; j < 7; j++)
                            {
                                row.Cells[j + mDCColsOffset].Value = frame.ChannelDrives[j];
                            }
                            break;
                        }
                    case GearboxControllerType.GM6T40:
                    case GearboxControllerType.GM6T70:
                    case GearboxControllerType.GM6L:
                        {
                            row.Cells[mDCColsOffset + 0].Value = frame.CANBUS_EngineSpeedValue;
                            row.Cells[mDCColsOffset + 1].Value = frame.CANBUS_TPS;
                            row.Cells[mDCColsOffset + 2].Value = frame.CANBUS_TorqueValue;
                            row.Cells[mDCColsOffset + 3].Value = frame.SSEMU_InputSpeedValue;
                            row.Cells[mDCColsOffset + 4].Value = frame.SSEMU_OutputSpeedValue;
                            // If this is GM 6Txx gearbox put the commanded gear value and CAN TCC drive value instead of DAQ values
                            // If this is an Zf6 switched script, fill the gear number cell
                            //row.Cells[mDCColsOffset + 5].Value = frame.Zf6GearNumber;
                            row.Cells[mDCColsOffset + 5].Value = frame.EnigmaGearNumber;
                            // Fill TCC cell value
                            row.Cells[mDCColsOffset + 6].Value = frame.EnigmaEDS6Value;
                            break;
                        }
                }
            }
            //add one extra row (otherwise the last one will be deleted while resaving)
            scriptDGV.Rows.Add();

            LoadPassFailTableRows();
            LoadPromptsTableRows();

            ManageDGVRowsHeight();
            ResumeLayout(false);
        }

        /// <summary>
        /// saves the datagridview content to internal TestScript class
        /// </summary>
        private void SaveTestScriptDGV()
        {
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            //__Save drive and pass/fail values to the TestScriptFrames_____________________________________________//
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (mTestScript.FrameSet.Count != scriptDGV.RowCount - 1)
            {
                mTestScript.FrameSet.Clear();
                for (int i = 0; i < scriptDGV.Rows.Count - 1; i++)
                {
                    mTestScript.FrameSet.Add(new TestScriptFrame());
                }
            }

            // ommit the last row as it is ALWAYS empty
            for (int i = 0; i < scriptDGV.Rows.Count - 1; i++)
            {
                DataGridViewRow row = scriptDGV.Rows[i];
                {
                    TestScriptFrame frame = mTestScript.FrameSet[i];
                    DataGridViewTextBoxCell gearcell = (DataGridViewTextBoxCell)row.Cells[1];
                    frame.IsPartOfTheLoop = (row.DefaultCellStyle.BackColor == mLoopColor);
                    frame.FrameName = (string)gearcell.Value;
                    frame.Duration = Convert.ToInt32(row.Cells[2].Value);
                    object value = row.Cells[3].Value;
                    frame.AcquireData = (value != null && (Boolean)value);
                    value = row.Cells[4].Value;
                    frame.IsPassThrough = (value != null && (Boolean)value);
                    frame.DynoMotorRPM = Convert.ToSingle(row.Cells[5].Value);
                    frame.DynoLoadCurrent = Convert.ToSingle(row.Cells[6].Value);
                    frame.ChannelDrives.Clear();
                    for (int j = 0; j < 9; j++)
                    {
                        frame.ChannelDrives.Add(0);
                    }

                    switch (mTestScript.Gearbox.ControllerType)
                    {
                        default:
                        case GearboxControllerType.NON_MECHATRONIC:
                            {
                                // If shis is an old type DAQ script, copy the drive values into the structure
                                for (int j = 0; j < mTestScript.Gearbox.CurrentDisplayChannelsCount; j++)
                                {
                                    DisplayChannel dchan = (DisplayChannel)mTestScript.Gearbox.CurrentDisplayChannelsSet.Channels[j];
                                    int index = dchan.InputChannelIndex;
                                    if (index >= 9)
                                        index -= 9;
                                    frame.ChannelDrives[index] = (int)Convert.ToSingle(row.Cells[j + mDCColsOffset].Value);
                                }
                                break;
                            }
                        case GearboxControllerType.ZF_6HPxx_1911E:
                        case GearboxControllerType.ZF_6HPxx_1911M:
                        case GearboxControllerType.ZF_6HPxx_CE:
                        case GearboxControllerType.ZF_6HPxx_CM:
                        case GearboxControllerType.ZF_6HPxx_TUCE:
                        case GearboxControllerType.ZF_6HPxx_TUCM:
                        case GearboxControllerType.ZF_6HPxx_WM:
                            {
                                // If this is Zf6 switched gearbox, set the value accordingly from the first dynamic column
                                //frame.Zf6GearNumber = (float)Convert.ToSingle(row.Cells[mDCColsOffset].Value);
                                frame.EnigmaGearNumber = (float)Convert.ToSingle(row.Cells[mDCColsOffset].Value);
                                // Save EDS5 value
                                frame.EnigmaEDS5Value = (int)Convert.ToInt32(row.Cells[mDCColsOffset + 1].Value);
                                // Save EDS6 value
                                frame.EnigmaEDS6Value = (int)Convert.ToInt32(row.Cells[mDCColsOffset + 2].Value);
                                break;
                            }

                        case GearboxControllerType.NISSAN_RE5:
                            {
                                // If this is Nissan RE5 gearbox put the solenoid drive values instead of DAQ values
                                for (int j = 0; j < 7; j++)
                                {
                                    frame.ChannelDrives[j] = (int)Convert.ToSingle(row.Cells[j + mDCColsOffset].Value);
                                }
                                break;
                            }
                        case GearboxControllerType.GM6T40:
                        case GearboxControllerType.GM6T70:
                        case GearboxControllerType.GM6L:
                            {
                                frame.CANBUS_EngineSpeedValue = Convert.ToUInt16(row.Cells[mDCColsOffset + 0].Value);
                                frame.CANBUS_TPS = Convert.ToUInt16(row.Cells[mDCColsOffset + 1].Value);
                                frame.CANBUS_TorqueValue = Convert.ToUInt16(row.Cells[mDCColsOffset + 2].Value);
                                frame.SSEMU_InputSpeedValue = Convert.ToUInt16(row.Cells[mDCColsOffset + 3].Value);
                                frame.SSEMU_OutputSpeedValue = Convert.ToUInt16(row.Cells[mDCColsOffset + 4].Value);
                                // If this is GM 6Txx CANPRO switched gearbox, set the value accordingly from the first dynamic column
                                // Save commanded gear value
                                //frame.Zf6GearNumber = (float)Convert.ToSingle(row.Cells[mDCColsOffset + 5].Value);
                                frame.EnigmaGearNumber = (float)Convert.ToSingle(row.Cells[mDCColsOffset + 5].Value);
                                // Save TCC value
                                frame.EnigmaEDS6Value = (int)Convert.ToInt32(row.Cells[mDCColsOffset + 6].Value);
                                break;
                            }
                    }

                }
            }

            // Save the pass/fail table contents
            List<GearShiftUsb.AIChannel> aics = mTestScript.Gearbox._analogueInputs;
            for (int i = 0; i < passFailTable.TableModel.Rows.Count; i++)
            {
                Row row = passFailTable.TableModel.Rows[i];
                TestScriptFrame frame = (TestScriptFrame)mTestScript.FrameSet[i];
                for (int j = 0; j < aics.Count; j++)
                {
                    float cellValue = Convert.ToSingle(row.Cells[j + 2].Data);
                    // Save a rounded base unit value to the report
                    frame.MasterPressureReadValues[j] = (float)Math.Round(MeasurementUnit.ConvertAIValueUserUnitToBaseUnit(cellValue, aics[j].ValueType), 2);
                }
            }

            // Save the prompts table contents
            for (int i = 0; i < promptsTable.TableModel.Rows.Count; i++)
            {
                Row row = promptsTable.TableModel.Rows[i];
                TestScriptFrame frame = (TestScriptFrame)mTestScript.FrameSet[i];
                // Make sure no prompt is added on a pass-through line
                if (!frame.IsPassThrough)
                {
                    frame.UserPromptEnabled = Convert.ToBoolean(row.Cells[2].Checked);
                }
                frame.UserPrompt = row.Cells[3].Text;
            }

            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            //__Set additional properties for each TestScriptFrame__________________________________________________//
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            UInt32 mMaxID = 0;
            for (int i = 0; i < mTestScript.FrameSet.Count; i++)
            {
                TestScriptFrame frame = (TestScriptFrame)mTestScript.FrameSet[i];
                mMaxID += (UInt32)frame.Duration / 10;
                frame.CriticalID = mMaxID;
            }
            // The last frame in collection MUST NOT be pass through, fix it
            if (mTestScript.FrameSet.Count > 0)
            {
                TestScriptFrame frame = (TestScriptFrame)mTestScript.FrameSet[mTestScript.FrameSet.Count - 1];
                frame.IsPassThrough = false;
            }

        }

        /// <summary>
        /// Creates new test script and refreshes the form. Opens the confirmation window if
        /// current script has been modified
        /// </summary>
        private void CreateNewTestScript()
        {
            if (TestScriptEdited)
            {
                DialogResult dr = Soko.Common.Forms.MessageBox.ShowWarning("GearShift",
                      "Data loss warning",
                      "The test script has been modified, the changes made will be lost.\n"
                      + "Do you really want to open new file?",
                      Soko.Common.Forms.MessageBoxButtons.OKCancel);
                if (dr != DialogResult.OK)
                {
                    return;
                }
            }
            mTestScript = new TestScript();
            ManageDGVColumns();
            LoadTestScriptDGV();
            UpdateGearboxControls();
            nameTextBox.Text = mTestScript.Name;
            gearboxConfigPanel.GearboxConfig = Soko.Common.Common.ObjectCopier.Clone(mTestScript.Gearbox);
        }

        private void PopulateMDFilesComboBox(object sender, EventArgs e)
        {
            try
            {
                TSMasterDataComboBox.Items.Clear();
                string[] filePaths = GST.Gearshift.Components.Utilities.Settings.AvailableReportsPaths;

                foreach (string filePath in filePaths)
                {
                    TSMasterDataComboBox.Items.Add(new ListBoxFileItem(filePath, Path.GetFileNameWithoutExtension(filePath)));
                }
            }
            catch (Exception)
            {
                throw new DirectoryNotFoundException("Either the application was improperly installed or the DAQ reports directory was removed by user");
            }
        }

        private void AssignMasterDataAndSave(string fileName)
        {
            TestScriptReport tsr = TestScriptReport.OpenFile(fileName);

            if (mTestScript.FrameSet.Count != tsr.TestScriptRunned.FrameSet.Count)
            {
                Soko.Common.Forms.MessageBox.ShowCritical("GearShift",
                                                              "Invalid file specified",
                                                              "The specified report file cannot be loaded as a master data for this test script, because it has different script lines count.\n",
                                                              Soko.Common.Forms.MessageBoxButtons.OK);
                TSMasterDataComboBox.SelectedIndex = -1;
                return;
            }

            // Save current DGVs content to the internal class to prevent data loss
            SaveTestScriptDGV();

            for (int i = 0; i < mTestScript.FrameSet.Count; i++)
            {
                mTestScript.FrameSet[i].MasterPressureReadValues.Clear();
                for (int j = 0; j < mTestScript.FrameSet[i].PressureReadValues.Count; j++)
                {
                    //mTestScript.FrameSet[i].MasterPressureReadValues.Add(tsr.TestScriptRunned.FrameSet[i].PressureReadValues[j]);
                    mTestScript.FrameSet[i].MasterPressureReadValues.Add(tsr.TestScriptRunned.FrameSet[i].PressureReadValues[j]);
                }
            }

            // Load the data to the forms to show the new pass/fail
            LoadTestScriptDGV();

            mTestScript.HasConsistentMasterData = true;
            imageButton3.ImageDisabled = GST.Gearshift.Components.Properties.Resources.TestScriptEditorPanel_Check_32x32;
            imageButton3.Text = "Reference curves saved";
            SaveButton_Click(this, EventArgs.Empty);
        }

        private void PopulateTSFilesComboBox(object sender, EventArgs e)
        {
            try
            {
                TSFilesComboBox.Items.Clear();
                string[] filePaths = GST.Gearshift.Components.Utilities.Settings.AvailableTestScriptsPaths;

                foreach (string filePath in filePaths)
                {
                    TSFilesComboBox.Items.Add(new ListBoxFileItem(filePath, Path.GetFileNameWithoutExtension(filePath)));
                }
            }
            catch (Exception)
            {
                throw new DirectoryNotFoundException("Either the application was improperly installed or the DAQ test scripts directory was removed by user");
            }


        }

        private void TSFilesComboBox_SelectedValueChanged(object sender, EventArgs e)
        {

            if (TSFilesComboBox.SelectedIndex != -1)
            {
                if (TestScriptEdited)
                {
                    DialogResult dr = Soko.Common.Forms.MessageBox.ShowWarning("GearShift",
                          "Data loss warning",
                          "The test script has been modified and not saved, the changes made will be lost.\n"
                          + "Do you really want to open new file?",
                          Soko.Common.Forms.MessageBoxButtons.OKCancel);
                    if (dr != DialogResult.OK)
                        return;//user doesn't want to abandon the currently edited file
                }
                ListBoxFileItem selected = (ListBoxFileItem)TSFilesComboBox.SelectedItem;
                OpenTestScriptFile(selected.mFilePath);
            }
        }

        private void OpenTestScriptFile(string path)
        {
            mTestScript = TestScript.OpenXml(path);
            ManageDGVColumns();
            LoadTestScriptDGV();
            UpdateGearboxControls();
            nameTextBox.Text = mTestScript.Name;
            gearboxConfigPanel.GearboxConfig = Soko.Common.Common.ObjectCopier.Clone(mTestScript.Gearbox);
            if (mTestScript.HasConsistentMasterData)
            {
                imageButton3.ImageDisabled = GST.Gearshift.Components.Properties.Resources.TestScriptEditorPanel_Check_32x32;
                imageButton3.Text = "Master data OK";
            }
            else
            {
                imageButton3.ImageDisabled = GST.Gearshift.Components.Properties.Resources.TestScriptEditorPanel_Delete_32x32;
                imageButton3.Text = "No master data loaded";
            }
            mTestScriptEdited = false;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (mTestScript.Gearbox.Name == string.Empty)
            {
                DialogResult dr = global::Soko.Common.Forms.MessageBox.ShowCritical("GearShift",
                                                                    "Empty field error",
                                                                    "The gearbox name field cannot be empty. Please fix the gearbox parameters and try again\n",
               Soko.Common.Forms.MessageBoxButtons.OK);
                return;
            }

            if (mTestScript.Name == string.Empty)
            {
                DialogResult dr = global::Soko.Common.Forms.MessageBox.ShowCritical("GearShift",
                                                                    "Empty field error",
                                                                    "The test script name field cannot be empty. Please enter a proper name and try again\n",
               Soko.Common.Forms.MessageBoxButtons.OK);
                return;
            }

            // Check if the file name is ok
            char[] invalidFileChars = Path.GetInvalidFileNameChars();
            int idx = mTestScript.Name.IndexOfAny(invalidFileChars);
            if (idx != -1)
            {
                DialogResult dr = global::Soko.Common.Forms.MessageBox.ShowCritical("GearShift",
                                                                    "Invalid gearbox name",
                                                                    "The script name field cannot contain characters invalid \nfor a file path, such as: \\ / : * ? \" < > |" +
                                                                    "\nPlease remove them and save again\n",
                                                                    Soko.Common.Forms.MessageBoxButtons.OK);
                return;
            }

            SaveTestScriptDGV();
            string dirToSave = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\GearShift Technologies\\GearShift\\Installed gearboxes" + "\\"
                                                            + mTestScript.Gearbox.Name;
            string pathToSave = dirToSave + "\\" + mTestScript.Name + ".tsc";
            Console.WriteLine(pathToSave);
            Console.WriteLine(mTestScript.Filename);
            if (mTestScript.Filename == pathToSave)
            {
                mTestScript.SaveXml(pathToSave);
                TestScriptEdited = false;
            }
            else
            {
                if (File.Exists(pathToSave))
                {
                    DialogResult dr = global::Soko.Common.Forms.MessageBox.ShowWarning("GearShift", "Data loss warning",
                                                                        "There already exists a test script with the name you specified\n"
                                                                        + "Do you really want to overwrite the existing test script?",
                                                                        Soko.Common.Forms.MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        mTestScript.SaveXml(pathToSave);
                        TestScriptEdited = false;
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
                    mTestScript.SaveXml(pathToSave);
                    TestScriptEdited = false;
                }
            }

            Soko.Common.Forms.MessageBox.ShowInfo("GearShift", "Operation success", "The test script file was saved correctly", Soko.Common.Forms.MessageBoxButtons.OK);
        }

        private void ManageDGVRowsHeight()
        {
            passFailTable.BeginUpdate();
            promptsTable.BeginUpdate();
            for (int i = 0; i < passFailTable.TableModel.Rows.Count; i++)
            {
                DataGridViewRow row = scriptDGV.Rows[i];
                // Manage the pass/fail table rows heights
                object value = row.Cells[3].Value;
                bool shrinkRow = !(value != null && (Boolean)value);
                if (shrinkRow)
                {
                    passFailTable.TableModel.Rows[i].Enabled = false;
                    passFailTable.TableModel.Rows[i].Height = 1;
                }
                else
                {
                    passFailTable.TableModel.Rows[i].Enabled = true;
                    passFailTable.TableModel.Rows[i].Height = 20;
                }
                // Manage the user prompts table rows heights
                // If the frame is pass-through. it cannot have the prompt assigned.
                value = row.Cells[4].Value;
                shrinkRow = (value != null && (Boolean)value);
                if (shrinkRow)
                {
                    promptsTable.TableModel.Rows[i].Enabled = false;
                    promptsTable.TableModel.Rows[i].Height = 1;
                }
                else
                {
                    promptsTable.TableModel.Rows[i].Enabled = true;
                    promptsTable.TableModel.Rows[i].Height = 20;
                }
            }
            passFailTable.EndUpdate();
            promptsTable.EndUpdate();
        }

        /// <summary>
        /// On backgrount painting
        /// </summary>
        /// <param name="e"></param>
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

        /// <summary>
        /// When test script row has been added
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scriptDGV_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DataGridViewRow row = scriptDGV.Rows[e.RowIndex];

            if (row.Cells.Count > 2)//in case an empty row is processed
            {
                if (scriptDGV.RowCount > (mTestScript.FrameSet.Count + 1))
                {
                    mTestScript.HasConsistentMasterData = false;
                    imageButton3.ImageDisabled = GST.Gearshift.Components.Properties.Resources.TestScriptEditorPanel_Delete_32x32;
                    imageButton3.Text = "No master data loaded";
                }
                //Add the step number index
                row.Cells[0].Value = (e.RowIndex + 1).ToString();
            }
        }

        /// <summary>
        /// On main window resize
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestScriptEditor_Resize(object sender, EventArgs e)
        {
            //This form minimal size is defined in properties, so here we dont care about it
            //enlarge GearboxFilePanel
            //gearboxMainPanel2.Width = this.Width - gearboxMainPanel2.Location.X - 3;
            //tabControl.Width = this.Width - 6;
            //tabControl.Height = this.Height - tabControl.Location.Y - 3;

        }

        /// <summary>
        /// Function inserts a row at selected index in scriptDGV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScriptDGV_InsertRow(object sender, EventArgs e)
        {
            //Add new row if the edited was the last one
            //if (DataG == scriptDGV.Rows.Count - 1)
            //{
            //    scriptDGV.Rows.Add();

            //    // Add pass/fail table row
            //    passFailTable.BeginUpdate();
            //    Row newRow = GetNewPassFailTableRow();
            //    passFailTable.TableModel.Rows.Add(newRow);
            //    newRow.Cells[0].Text = passFailTable.TableModel.Rows.Count.ToString();
            //    passFailTable.EndUpdate();
            //    // Add prompts table row
            //    promptsTable.BeginUpdate();
            //    newRow = GetNewPromptsTableRow();
            //    promptsTable.TableModel.Rows.Add(newRow);
            //    newRow.Cells[0].Text = promptsTable.TableModel.Rows.Count.ToString();
            //    passFailTable.EndUpdate();
            //    // Update the rows heights
            //    ManageDGVRowsHeight();
            //}


            if (scriptDGV.SelectedRows.Count > 0)
            {
                SuspendLayout();

                passFailTable.BeginUpdate();
                promptsTable.BeginUpdate();

                int currSelIndex = scriptDGV.SelectedRows[0].Index;
                //scriptDGV.SelectedRows[0].Selected = false;
                DataGridViewRow newRow = new DataGridViewRow();
                scriptDGV.Rows.Insert(currSelIndex, newRow);
                //newRow.Selected = true;
                passFailTable.TableModel.Rows.Insert(currSelIndex, GetNewPassFailTableRow());
                promptsTable.TableModel.Rows.Insert(currSelIndex, GetNewPromptsTableRow());

                // Update the line numbers on the tables rows
                for (int i = 0; i < scriptDGV.Rows.Count; i++)
                {
                    scriptDGV.Rows[i].Cells[0].Value = (i + 1).ToString();
                }
                for (int i = 0; i < passFailTable.TableModel.Rows.Count; i++)
                {
                    passFailTable.TableModel.Rows[i].Cells[0].Text = (i + 1).ToString();
                    promptsTable.TableModel.Rows[i].Cells[0].Text = (i + 1).ToString();
                }

                passFailTable.EndUpdate();
                promptsTable.EndUpdate();

                ManageDGVRowsHeight();

                TestScriptEdited = true;
                mTestScript.HasConsistentMasterData = false;
                imageButton3.ImageDisabled = GST.Gearshift.Components.Properties.Resources.TestScriptEditorPanel_Delete_32x32;
                imageButton3.Text = "No master data loaded";
                ResumeLayout(false);
            }
        }

        /// <summary>
        /// Function removes selected rows in scriptDGV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScriptDGV_RemoveRow(object sender, EventArgs e)
        {
            if (scriptDGV.SelectedRows.Count > 0)
            {
                SuspendLayout();
                passFailTable.BeginUpdate();
                promptsTable.BeginUpdate();

                // If there's the last row selected, it must be deselected first
                foreach (DataGridViewRow row in scriptDGV.SelectedRows)
                {
                    if (row.Index == scriptDGV.RowCount - 1)
                    {
                        row.Selected = false;
                        break;
                    }
                }

                // Remove all selected rows from all tables
                while (scriptDGV.SelectedRows.Count > 0)
                {
                    int currIndex = scriptDGV.SelectedRows[0].Index;
                    scriptDGV.Rows.RemoveAt(currIndex);
                    passFailTable.TableModel.Rows.RemoveAt(currIndex);
                    promptsTable.TableModel.Rows.RemoveAt(currIndex);
                }

                // Update the line numbers on the tables rows
                for (int i = 0; i < scriptDGV.Rows.Count - 1; i++)
                {
                    scriptDGV.Rows[i].Cells[0].Value = (i + 1).ToString();
                    passFailTable.TableModel.Rows[i].Cells[0].Text = (i + 1).ToString();
                    promptsTable.TableModel.Rows[i].Cells[0].Text = (i + 1).ToString();
                }

                passFailTable.EndUpdate();
                promptsTable.EndUpdate();

                TestScriptEdited = true;
                mTestScript.HasConsistentMasterData = false;
                imageButton3.ImageDisabled = GST.Gearshift.Components.Properties.Resources.TestScriptEditorPanel_Delete_32x32;
                imageButton3.Text = "No master data loaded";
                ResumeLayout(false);
            }
        }

        /// <summary>
        /// Function moves selected rows up in scriptDGV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScriptDGV_MoveSelectionUp(object sender, EventArgs e)
        {
            SuspendLayout();

            // If there's the last row selected, it must be deselected first
            foreach (DataGridViewRow row in scriptDGV.SelectedRows)
            {
                if (row.Index == scriptDGV.RowCount - 1)
                {
                    row.Selected = false;
                    break;
                }
            }

            ArrayList indicesToMove = new ArrayList();
            for (int i = 0; i < scriptDGV.SelectedRows.Count; i++)
            {
                indicesToMove.Add(scriptDGV.SelectedRows[i].Index);
            }
            indicesToMove.Sort();
            if ((int)indicesToMove[0] == 0)
            {
                //means this is the top and cannot move
                return;
            }
            int prevIndex = (int)indicesToMove[0];
            for (int i = 0; i < indicesToMove.Count; i++)
            {
                int currIndex = (int)indicesToMove[i];
                if (currIndex - prevIndex > 1)
                {
                    return;//means there's uncontinous block of data selected
                }
                prevIndex = currIndex;
            }
            int firstIndex = (int)indicesToMove[0];
            int lastIndex = (int)indicesToMove[indicesToMove.Count - 1];

            // Add the row index above the selection to have it updated as well later on
            indicesToMove.Add(firstIndex - 1);

            DataGridViewRow tmp = scriptDGV.Rows[firstIndex - 1];
            scriptDGV.Rows.RemoveAt(firstIndex - 1);
            scriptDGV.Rows.Insert(lastIndex, tmp);

            passFailTable.BeginUpdate();
            promptsTable.BeginUpdate();

            // Swap pass/fail table rows
            Row tr = passFailTable.TableModel.Rows[firstIndex - 1];
            passFailTable.TableModel.Rows.RemoveAt(firstIndex - 1);
            passFailTable.TableModel.Rows.Insert(lastIndex, tr);
            // Swap prompts table rows
            tr = promptsTable.TableModel.Rows[firstIndex - 1];
            promptsTable.TableModel.Rows.RemoveAt(firstIndex - 1);
            promptsTable.TableModel.Rows.Insert(lastIndex, tr);
            // Update tables rows step numbers
            foreach (int index in indicesToMove)
            {
                scriptDGV.Rows[index].Cells[0].Value = (index + 1).ToString();
                passFailTable.TableModel.Rows[index].Cells[0].Text = (index + 1).ToString();
                promptsTable.TableModel.Rows[index].Cells[0].Text = (index + 1).ToString();
            }
            promptsTable.EndUpdate();
            passFailTable.EndUpdate();

            ResumeLayout(false);
        }

        /// <summary>
        /// Function moves selected rows down in scriptDGV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScriptDGV_MoveSelectionDown(object sender, EventArgs e)
        {
            SuspendLayout();

            // If there's the last row selected, it must be deselected first
            foreach (DataGridViewRow row in scriptDGV.SelectedRows)
            {
                if (row.Index == scriptDGV.RowCount - 1)
                {
                    row.Selected = false;
                    break;
                }
            }

            ArrayList indicesToMove = new ArrayList();
            for (int i = 0; i < scriptDGV.SelectedRows.Count; i++)
            {
                indicesToMove.Add(scriptDGV.SelectedRows[i].Index);
            }
            indicesToMove.Sort();
            if ((int)indicesToMove[indicesToMove.Count - 1] == scriptDGV.Rows.Count - 2)
            {
                //means this is the bottom and cannot move
                return;
            }
            int prevIndex = (int)indicesToMove[0];
            for (int i = 0; i < indicesToMove.Count; i++)
            {
                int currIndex = (int)indicesToMove[i];
                if (currIndex - prevIndex > 1)
                {
                    return;//means there's uncontinous block of data selected
                }
                prevIndex = currIndex;
            }
            int firstIndex = (int)indicesToMove[0];
            int lastIndex = (int)indicesToMove[indicesToMove.Count - 1];

            // Add the row index below the selection to have it updated as well later on
            indicesToMove.Add(lastIndex + 1);

            passFailTable.BeginUpdate();
            promptsTable.BeginUpdate();

            DataGridViewRow tmp = scriptDGV.Rows[lastIndex + 1];
            scriptDGV.Rows.RemoveAt(lastIndex + 1);
            scriptDGV.Rows.Insert(firstIndex, tmp);

            // Swap pass/fail table rows
            Row tr = passFailTable.TableModel.Rows[lastIndex + 1];
            passFailTable.TableModel.Rows.RemoveAt(lastIndex + 1);
            passFailTable.TableModel.Rows.Insert(firstIndex, tr);
            // Swap prompts table rows
            tr = promptsTable.TableModel.Rows[lastIndex + 1];
            promptsTable.TableModel.Rows.RemoveAt(lastIndex + 1);
            promptsTable.TableModel.Rows.Insert(firstIndex, tr);

            // Update tables rows step numbers
            foreach (int index in indicesToMove)
            {
                scriptDGV.Rows[index].Cells[0].Value = (index + 1).ToString();
                passFailTable.TableModel.Rows[index].Cells[0].Text = (index + 1).ToString();
                promptsTable.TableModel.Rows[index].Cells[0].Text = (index + 1).ToString();
            }
            promptsTable.EndUpdate();
            passFailTable.EndUpdate();

            ResumeLayout(false);
        }

        private void TestScriptFileNewButton_Click(object sender, EventArgs e)
        {
            CreateNewTestScript();
        }

        private void scriptDGV_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //Add new row if the edited was the last one
            if (e.RowIndex == scriptDGV.Rows.Count - 1)
            {
                scriptDGV.Rows.Add();

                // Add pass/fail table row
                passFailTable.BeginUpdate();
                Row newRow = GetNewPassFailTableRow();
                passFailTable.TableModel.Rows.Add(newRow);
                newRow.Cells[0].Text = passFailTable.TableModel.Rows.Count.ToString();
                passFailTable.EndUpdate();
                // Add prompts table row
                promptsTable.BeginUpdate();
                newRow = GetNewPromptsTableRow();
                promptsTable.TableModel.Rows.Add(newRow);
                newRow.Cells[0].Text = promptsTable.TableModel.Rows.Count.ToString();
                passFailTable.EndUpdate();
                // Update the rows heights
                ManageDGVRowsHeight();
            }
            TestScriptEdited = true;
        }

        private void scriptDGV_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 1:
                    {
                        passFailTable.TableModel.Rows[e.RowIndex].Cells[1].Text = (string)scriptDGV.Rows[e.RowIndex].Cells[1].Value;
                        promptsTable.TableModel.Rows[e.RowIndex].Cells[1].Text = (string)scriptDGV.Rows[e.RowIndex].Cells[1].Value;
                        break;
                    }
                case 3:
                case 4:
                    {
                        ManageDGVRowsHeight();
                        break;
                    }
            }


        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            mTestScript.Name = nameTextBox.Text;
            TestScriptEdited = true;
        }

        private void passFailDGV_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            TestScriptEdited = true;
        }

        private void TSMasterDataComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TSMasterDataComboBox.SelectedIndex != -1)
            {
                ListBoxFileItem selected = (ListBoxFileItem)TSMasterDataComboBox.SelectedItem;
                AssignMasterDataAndSave(selected.mFilePath);
            }
        }

        private void BeginTransaction()
        {
            ++mTransactionStackSize;
        }

        private void EndTransaction()
        {
            if (--mTransactionStackSize == 0)
                LoadTestScriptDGV();
        }

        private List<TestScriptFrame> CollectSelectedFrames()
        {
            List<TestScriptFrame> list = new List<TestScriptFrame>();
            if (scriptDGV.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in scriptDGV.SelectedRows)
                {
                    // "new" row at the end of DGV is virtual and does
                    // not correspond to the existing frame.
                    if (row.Index >= mTestScript.FrameSet.Count)
                        continue;

                    list.Add(mTestScript.FrameSet[row.Index]);
                }
            }
            else if (scriptDGV.SelectedCells.Count > 0)
            {
                foreach (DataGridViewCell cell in scriptDGV.SelectedCells)
                {
                    // "new" row at the end of DGV is virtual and does
                    // not correspond to the existing frame.
                    if (cell.RowIndex >= mTestScript.FrameSet.Count)
                        continue;

                    var frame = mTestScript.FrameSet[cell.RowIndex];
                    if (!list.Contains(frame))
                        list.Add(frame);
                }
            }

            list.Sort(delegate (TestScriptFrame left, TestScriptFrame right)
            {
                return mTestScript.FrameSet.IndexOf(left) - mTestScript.FrameSet.IndexOf(right);
            });

            return list;
        }

        private void CutSelection()
        {
            BeginTransaction();
            CopySelection();
            DeleteSelection();
            EndTransaction();
        }

        private void CopySelection()
        {
            var frames = CollectSelectedFrames();
            if (frames.Count > 0)
                Clipboard.SetData("TestScriptFrames", frames);
        }

        private void PasteSelection()
        {
            List<TestScriptFrame> frames = Clipboard.GetData("TestScriptFrames") as List<TestScriptFrame>;
            if (null == frames)
                return;

            BeginTransaction();

            var selection = CollectSelectedFrames();

            if (mTestScript.FrameSet.Count >= 0 && selection.Count > 0)
            {
                int startIndex = mTestScript.FrameSet.IndexOf(selection[0]);

                foreach (TestScriptFrame frame in frames)
                    mTestScript.FrameSet.Insert(startIndex++, frame);
            }
            else
                mTestScript.FrameSet.AddRange(frames);

            EndTransaction();
        }

        private void DeleteSelection()
        {
            BeginTransaction();
            var frames = CollectSelectedFrames();
            if (frames.Count > 0)
                foreach (var frame in frames)
                    mTestScript.FrameSet.Remove(frame);
            EndTransaction();
        }

        private void selectLoopSectionButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in scriptDGV.Rows)
            {
                if (row.DefaultCellStyle.BackColor == mLoopColor)
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
            }
            foreach (DataGridViewRow row in scriptDGV.SelectedRows)
            {
                row.DefaultCellStyle.BackColor = mLoopColor;
                row.Selected = false;
            }
        }

        private void cutButton_Click(object sender, EventArgs e)
        {
            CutSelection();
        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            CopySelection();
        }

        private void pasteButton_Click(object sender, EventArgs e)
        {
            PasteSelection();
        }

        #endregion Methods

        private void mainTabControl_SelectedPageChanged(object sender, EventArgs e)
        {
            if (mainTabControl.SelectedIndex == 0)
            {
                gearboxConfigPanel.RequestInternalSave();
            }
        }

        private void deleteFileButton_Click(object sender, EventArgs e)
        {

            if (!System.IO.File.Exists(mTestScript.Filename))
            {
                global::Soko.Common.Forms.MessageBox.ShowCritical("GearShift",
                                                                    "Invalid parameter error",
                                                                    "File to be deleted not found. Please open the script from the list and try again\n",
                                                                    Soko.Common.Forms.MessageBoxButtons.OK);
                return;
            }

            DialogResult dr = global::Soko.Common.Forms.MessageBox.ShowWarning("GearShift", "Data loss warning",
                                                                "You are about to delete the currently opened script from the disk\n"
                                                                + "Do you really want to continue?",
                                                                Soko.Common.Forms.MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    System.IO.File.Delete(mTestScript.Filename);
                }
                catch (Exception ex)
                {
                    global::Soko.Common.Forms.MessageBox.ShowCritical("GearShift",
                                                          "Operation error",
                                                          "Failed to delete the file: \n " + ex.Message,
                                                          Soko.Common.Forms.MessageBoxButtons.OK);
                }
            }
            else
            {
                return;
            }


            Soko.Common.Forms.MessageBox.ShowInfo("GearShift", "Operation success", "The test script file was deleted correctly", Soko.Common.Forms.MessageBoxButtons.OK);

        }
    }


}
