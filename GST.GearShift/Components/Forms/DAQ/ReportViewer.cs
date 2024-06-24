using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using ZedGraph;
using GST.Gearshift.Components.Interfaces.USB;
using GST.Gearshift.Components.Utilities.ReportPrinting;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

using GST.Gearshift.Components.Utilities;
using GST.Gearshift.Components.Properties;

using XPTable.Models;

namespace GST.Gearshift.Components.Forms.DAQ
{
    public partial class ReportViewer : UserControl
    {
        internal class ZGCheckbox : CheckBox
        {
            LineItem readCurve = null;
            LineItem referenceCurve = null;
            ZedGraphControl baseGraph;
            bool drawRef = true;

            public LineItem ReadCurve { get { return readCurve; } }

            public ZGCheckbox(LineItem rdCurve, LineItem refCurve, ZedGraphControl graph)
            {
                readCurve = rdCurve;
                referenceCurve = refCurve;
                baseGraph = graph;
                drawRef = true;
            }

            public void ReferenceOn()
            {
                if (referenceCurve != null)
                {
                    drawRef = true;
                    if (this.Checked)
                    {
                        referenceCurve.IsVisible = true;
                    }
                }
                if (baseGraph != null)
                {
                    baseGraph.Invalidate();
                }
            }

            public void ReferenceOff()
            {
                if (referenceCurve != null)
                {
                    drawRef = false;
                    referenceCurve.IsVisible = false;
                }
                if (baseGraph != null)
                {
                    baseGraph.Invalidate();
                }
            }

            protected override void OnCheckedChanged(EventArgs e)
            {
                if (readCurve != null)
                {
                    readCurve.IsVisible = this.Checked;
                    readCurve.Label.IsVisible = this.Checked;
                }
                if (referenceCurve != null)
                {
                    if (drawRef)
                    {
                        referenceCurve.IsVisible = this.Checked;
                    }
                    else
                    {
                        referenceCurve.IsVisible = false;
                    }
                }

                Control p = this.Parent;
                while (p != null)
                {
                    if (p is ReportViewer)
                    {
                        (p as ReportViewer).UpdateLegendVisibility();
                        break;
                    }
                    p = p.Parent;
                }
                if (baseGraph != null)
                {
                    baseGraph.Invalidate();
                }
                base.OnCheckedChanged(e);
            }
        }

        #region Constants

        private readonly Color[] mLineColorsArray = new Color[]
        {
      // Colors are from http://colorbrewer2.org/
      Color.FromArgb(27, 158, 119),
      Color.FromArgb(217, 95, 2),
      Color.FromArgb(117, 112, 179),
      Color.FromArgb(231, 41, 138),
      Color.FromArgb(102, 166, 30),
      Color.FromArgb(230, 171, 2),
      Color.FromArgb(166, 118, 29),
      Color.FromArgb(102, 194, 165),
      Color.FromArgb(252, 141, 98),
      Color.FromArgb(141, 160, 203),
      Color.FromArgb(231, 138, 195),
      Color.FromArgb(166, 216, 84),
      Color.FromArgb(255, 217, 47),
      Color.FromArgb(229, 196, 148),
        };

        private readonly Color passedCellBgColor = Color.FromArgb(180, 255, 180);
        private readonly Color failedCellBgColor = Color.FromArgb(255, 180, 180);
        private readonly Color neutralCellBgColor = Color.FromArgb(220, 240, 255);
        #endregion  Constants


        #region Private fields

        private TestScript mTestScript;
        private TestScriptReport mTestScriptReport;

        PrintDocument printDocument1 = new System.Drawing.Printing.PrintDocument();

        private bool _printTableData = false;

        private bool mCurrentsDGVPrinted = false;
        private bool mPressuresDGVPrinted = false;
        private bool mTitlePagePrinted = false;
        private bool mGraphPrinted = false;
        private int mPrintedGraphs = 0;
        private int mPrintedGraphPages = 0;
        private bool mPrinting = false;

        private int mPageGraphSpan = 1;

        PrintingDataGridViewProvider DGVPressuresPrintProvider;
        PrintingDataGridViewProvider DGVCurrentsPrintProvider;

        #endregion Private fields



        #region Constructors & finalizer

        public ReportViewer()
        {
            InitializeComponent();
            //this.viewModeTabControl.SelectedIndex = 3;
        }

        #endregion Constructors & finalizer



        #region Events



        #endregion Events



        #region Properties
        [XmlIgnoreAttribute,
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TestScriptReport TestScriptReport
        {
            get { return mTestScriptReport; }
            set
            {
                if (value == null)
                    value = new TestScriptReport();
                mTestScriptReport = value;
                mTestScript = mTestScriptReport.TestScriptRunned;
                livePreviewPanel1.Report = mTestScriptReport;
                //livePreviewPanel1.StartPlayback();
                LoadControls();
                UpdateSpan();
            }
        }

        #endregion Properties



        #region Methods

        #region Report Printing

        public void PrintCurrentReport()
        {
            printDocument1 = new System.Drawing.Printing.PrintDocument();
            printDocument1.PrintPage += new PrintPageEventHandler(PrintSinglePage);
            printDocument1.BeginPrint += new PrintEventHandler(BeginPrinting);
            printDocument1.EndPrint += new PrintEventHandler(EndPrinting);
            PageSetupDialog pageSetupDialog1 = new PageSetupDialog();
            PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();
            pageSetupDialog1.Document = printDocument1;
            printPreviewDialog1.Document = printDocument1;

            pageSetupDialog1.EnableMetric = false;
            pageSetupDialog1.PageSettings.Landscape = true;

            if (pageSetupDialog1.ShowDialog() == DialogResult.OK)
            {
                printPreviewDialog1.ShowDialog();
            }

        }

        void EndPrinting(object sender, PrintEventArgs e)
        {
            mPrinting = false;
            UpdateLegendVisibility();
            UpdateSpan();
        }

        private DataGridView GridFromTable(Table table)
        {
            int columnIndex = 0;
            DataGridView gridView = new DataGridView();
            //gridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            //gridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            foreach (XPTable.Models.Column column in table.ColumnModel.Columns)
            {
                DataGridViewTextBoxColumn outColumn = new DataGridViewTextBoxColumn();
                //outColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                outColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                outColumn.HeaderText = column.Text;
                outColumn.Name = column.Text;

                outColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                outColumn.HeaderCell.Style.WrapMode = DataGridViewTriState.False;

                switch (column.Alignment)
                {
                    case ColumnAlignment.Left:
                        outColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        break;

                    case ColumnAlignment.Center:
                        outColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;

                    case ColumnAlignment.Right:
                        outColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;
                }

                outColumn.DefaultCellStyle.Font = new Font(table.Font.FontFamily, SystemFonts.DefaultFont.Size);

                gridView.Columns.Add(outColumn);

                columnIndex++;
            }
            gridView.Rows.Add(passFailTable.RowCount);

            int rowIndex = 0;
            foreach (XPTable.Models.Row row in table.TableModel.Rows)
            {
                var outRow = gridView.Rows[rowIndex++];

                int cellIndex = 0;
                foreach (XPTable.Models.Cell cell in row.Cells)
                {
                    DataGridViewTextBoxCell outCell = outRow.Cells[cellIndex++] as DataGridViewTextBoxCell;
                    outCell.Value = cell.Text;
                    outCell.Style.WrapMode = DataGridViewTriState.False;
                }
            }

            gridView.AutoSize = true;
            gridView.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            gridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;

            foreach (DataGridViewColumn column in gridView.Columns)
            {
                int width = column.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, false);
                column.Width = width * 3;
            }

            return gridView;
        }

        private void BeginPrinting(object sender, PrintEventArgs e)
        {
            mCurrentsDGVPrinted = false;
            mPressuresDGVPrinted = false;
            mGraphPrinted = false;
            mTitlePagePrinted = false;
            mPrintedGraphs = 0;
            mPrintedGraphPages = 0;
            mPrinting = true;

            // Prepare for pressures printing
            GST.Gearshift.Components.Utilities.ReportPrinting.ResultsViewerHeaderPrintBlock headerBlock = new GST.Gearshift.Components.Utilities.ReportPrinting.ResultsViewerHeaderPrintBlock();
            GST.Gearshift.Components.Utilities.ReportPrinting.ResultsViewerFooterPrintBlock footerBlock = new GST.Gearshift.Components.Utilities.ReportPrinting.ResultsViewerFooterPrintBlock();
            footerBlock.FooterText = "Pressure pass/fail data";
            TitlePrintBlock titleBlock = new TitlePrintBlock();
            titleBlock.Title = "Test pressure data in table form";
            titleBlock.ForeColor = Color.Black;
            titleBlock.Font = new Font("Times New Roman", 14);

            var passFailGrid = GridFromTable(passFailTable);
            DGVPressuresPrintProvider = GST.Gearshift.Components.Utilities.ReportPrinting.PrintingDataGridViewProvider.Create(printDocument1, passFailGrid, true, true, false, titleBlock, headerBlock, footerBlock);

            // Prepare for solenoids printing
            headerBlock = new GST.Gearshift.Components.Utilities.ReportPrinting.ResultsViewerHeaderPrintBlock();
            footerBlock = new GST.Gearshift.Components.Utilities.ReportPrinting.ResultsViewerFooterPrintBlock();
            footerBlock.FooterText = "Pressure pass/fail data";
            titleBlock = new TitlePrintBlock();
            titleBlock.Title = "Solenoid drive and current data";
            titleBlock.ForeColor = Color.Black;
            titleBlock.Font = new Font("Times New Roman", 14);

            var driveGrid = GridFromTable(driveTable);
            DGVCurrentsPrintProvider = GST.Gearshift.Components.Utilities.ReportPrinting.PrintingDataGridViewProvider.Create(printDocument1, driveGrid, true, true, false, titleBlock, headerBlock, footerBlock);
        }

        #region Title Page Drawing
        [Flags]
        public enum TextAlign
        {
            Left = 0x0001,
            Right = 0x0002,
            Top = 0x0004,
            Bottom = 0x0008,

            TopLeft = Top | Left,
            TopRight = Top | Right,
            BottomLeft = Bottom | Left,
            BottomRight = Bottom | Right,

            Default = 0,
        }

        static public void DrawAlignedText(Graphics graphics, string text, Font font, Brush brush, TextAlign align, float x, float y, float width)
        {
            SizeF size;
            if (width > 0.0f)
                size = graphics.MeasureString(text, font, new SizeF(width, 0), StringFormat.GenericTypographic);
            else
                size = graphics.MeasureString(text, font, new PointF(x, y), StringFormat.GenericTypographic);

            if ((align & TextAlign.Right) == TextAlign.Right)
                x -= size.Width;
            else if ((align & TextAlign.Left) != TextAlign.Left)
                x -= size.Width * 0.5f;

            if ((align & TextAlign.Bottom) == TextAlign.Bottom)
                y -= size.Height;
            else if ((align & TextAlign.Top) != TextAlign.Top)
                y -= size.Height * 0.5f;

            if (width > 0.0f)
            {
                //graphics.DrawRectangle(Pens.Red, x, y, size.Width, size.Height);
                graphics.DrawString(text, font, brush, new RectangleF(x, y, size.Width, size.Height), StringFormat.GenericTypographic);
            }
            else
                graphics.DrawString(text, font, brush, new PointF(x, y), StringFormat.GenericTypographic);
        }

        static public void DrawAlignedText(Graphics graphics, string text, Font font, Brush brush, TextAlign align, float x, float y)
        {
            DrawAlignedText(graphics, text, font, brush, align, x, y, -1.0f);
        }

        private void TitlePage_PrintPage(object sender, PrintPageEventArgs e)
        {
            PrintDocument pd = sender as PrintDocument;

            TestScriptReport report = mTestScriptReport;

            int pxWidth = e.PageBounds.Width;
            int pxHeight = e.PageBounds.Height;

            if (pxWidth > pxHeight)
            {
                pxWidth += pxHeight;
                pxHeight = pxWidth - pxHeight;
                pxWidth -= pxHeight;
            }

            float pageWidth = 420.0f;
            float pageHeight = 594.0f;

            float hAspect = pxWidth / pageWidth;
            float vAspect = pxHeight / pageHeight;

            e.Graphics.PageScale = Math.Min(hAspect, vAspect);

            if (e.PageSettings.Landscape)
            {
                e.Graphics.RotateTransform(-90.0f);
                e.Graphics.TranslateTransform(-pageWidth, 0.0f);
            }

            float offsetX = (pxWidth - e.Graphics.PageScale * pageWidth) * 0.5f / e.Graphics.PageScale;
            float offsetY = (pxHeight - e.Graphics.PageScale * pageHeight) * 0.5f / e.Graphics.PageScale;

            e.Graphics.TranslateTransform(offsetX, offsetY);

            // It is assumed that each of the logos must fit into the 200x200px rectangle.

            // Draw report logo in the top right corner
            using (Image image = Resources.GST_ReportLogo)
            {
                RectangleF imageDestRect = new RectangleF(210.0f, 10.0f, 200.0f, 150.0f);
                float imageWidth = 120.0f;
                float imageHeight = imageWidth / image.Width * image.Height;
                RectangleF imageRect = new RectangleF(imageDestRect.Left + (imageDestRect.Width - imageWidth) / 2.0f, imageDestRect.Top + (imageDestRect.Height - imageHeight) / 2.0f, imageWidth, imageHeight);

                e.Graphics.DrawImage(image, imageRect);
            }

            // Draw customer company info logo in the top left corner
            using (Image image = Settings.Instance.CompanyInfoPicture)
            {
                RectangleF imageDestRect = new RectangleF(10.0f, 10.0f, 200.0f, 150.0f);
                float imageWidth = 120.0f;
                float imageHeight = imageWidth / image.Width * image.Height;
                RectangleF imageRect = new RectangleF(imageDestRect.Left + (imageDestRect.Width - imageWidth) / 2.0f, imageDestRect.Top + (imageDestRect.Height - imageHeight) / 2.0f, imageWidth, imageHeight);

                e.Graphics.DrawImage(image, imageRect);
            }


            Font titleFont = new Font("Arial", 36.0f * e.Graphics.PageScale, GraphicsUnit.Point);
            Font labelFont = new Font("Arial", 14.0f * e.Graphics.PageScale, FontStyle.Bold, GraphicsUnit.Point);
            Font valueFont = new Font("Arial", 14.0f * e.Graphics.PageScale, GraphicsUnit.Point);

            DrawAlignedText(e.Graphics, "Test Report", titleFont, Brushes.Black, TextAlign.Default, pageWidth / 2.0f, 200.0f);

            float horizontalLabelOffset = -40.0f;
            float horizontalPosition = pageWidth / 2.0f + horizontalLabelOffset;
            float horizontalValuePosition = pageWidth / 2.0f - 20.0f;
            float verticalPosition = 250.0f;
            float lineHeight = labelFont.GetHeight() / e.Graphics.PageScale;
            float lineSpace = lineHeight * 2.0f;



            DrawAlignedText(e.Graphics, "Name:", labelFont, Brushes.Black, TextAlign.Right, horizontalPosition, verticalPosition);
            DrawAlignedText(e.Graphics, report.TestScriptRunned.Name, valueFont, Brushes.Black, TextAlign.Left, horizontalValuePosition, verticalPosition);
            verticalPosition += lineSpace;
            DrawAlignedText(e.Graphics, "Date:", labelFont, Brushes.Black, TextAlign.Right, horizontalPosition, verticalPosition);
            DrawAlignedText(e.Graphics, report.TimePerformed.ToString(), valueFont, Brushes.Black, TextAlign.Left, horizontalValuePosition, verticalPosition);
            verticalPosition += lineSpace;
            DrawAlignedText(e.Graphics, "Operator:", labelFont, Brushes.Black, TextAlign.Right, horizontalPosition, verticalPosition);
            DrawAlignedText(e.Graphics, report.OperatorName, valueFont, Brushes.Black, TextAlign.Left, horizontalValuePosition, verticalPosition);
            verticalPosition += lineSpace;
            DrawAlignedText(e.Graphics, "Serial:", labelFont, Brushes.Black, TextAlign.Right, horizontalPosition, verticalPosition);
            DrawAlignedText(e.Graphics, report.GearboxSerialNumber, valueFont, Brushes.Black, TextAlign.Left, horizontalValuePosition, verticalPosition);
            verticalPosition += lineSpace;
            DrawAlignedText(e.Graphics, "Comment:", labelFont, Brushes.Black, TextAlign.Right, horizontalPosition, verticalPosition);
            DrawAlignedText(e.Graphics, report.Comment, valueFont, Brushes.Black, TextAlign.TopLeft,
              horizontalValuePosition, verticalPosition - lineHeight * 0.5f, pageWidth / 2.0f);

            titleFont.Dispose();
            labelFont.Dispose();
            valueFont.Dispose();
        }
        #endregion

        private void ZedGraph_PrintPage(object sender, PrintPageEventArgs e, bool _isPrintScaleAll, bool _isPrintFillPage, bool _isPrintKeepAspectRatio)
        {
            PrintDocument pd = sender as PrintDocument;

            //graphicalPressureZedGraph.BorderStyle = BorderStyle.None;

            GraphPane myPane = graphicalPressureZedGraph.GraphPane;

            Fill chartFillSave = myPane.Chart.Fill.Clone();
            Fill paneFillSave = myPane.Fill.Clone();

            myPane.Fill = new Fill(Color.White);
            myPane.Chart.Fill = new Fill(Color.White);

            myPane.XAxis.MajorTic.PenWidth /= 2.0f;
            myPane.XAxis.MinorTic.PenWidth /= 2.5f;
            myPane.XAxis.MajorGrid.PenWidth /= 2.0f;
            myPane.XAxis.MinorGrid.PenWidth /= 2.5f;
            myPane.YAxis.MajorTic.PenWidth /= 2.0f;
            myPane.YAxis.MinorTic.PenWidth /= 2.5f;
            myPane.YAxis.MajorGrid.PenWidth /= 2.0f;
            myPane.YAxis.MinorGrid.PenWidth /= 2.5f;

            UpdateLegendVisibility();

            MasterPane mPane = graphicalPressureZedGraph.MasterPane;
            bool[] isPenSave = new bool[mPane.PaneList.Count + 1];
            bool[] isFontSave = new bool[mPane.PaneList.Count + 1];
            //Fill[] paneFillSave = new Fill[mPane.PaneList.Count + 1];
            //Fill fillSave = mPane.Fill.Clone();
            //mPane.Fill.Color = Color.White;
            bool borderSave = mPane.Border.IsVisible;
            mPane.Border.IsVisible = false;
            isPenSave[0] = mPane.IsPenWidthScaled;
            isFontSave[0] = mPane.IsFontsScaled;
            for (int i = 0; i < mPane.PaneList.Count; i++)
            {
                isPenSave[i + 1] = mPane[i].IsPenWidthScaled;
                isFontSave[i + 1] = mPane[i].IsFontsScaled;
                //paneFillSave[i] = mPane[i].Fill.Clone();
                //mPane[i].Fill.Color = Color.White;
                if (_isPrintScaleAll)
                {
                    mPane[i].IsPenWidthScaled = true;
                    mPane[i].IsFontsScaled = true;
                }
                foreach (CurveItem curve in mPane[i].CurveList)
                    if (curve is LineItem)
                        (curve as LineItem).Line.Width /= 2.5f;
            }

            RectangleF saveRect = mPane.Rect;
            SizeF newSize = mPane.Rect.Size;
            if (_isPrintFillPage && _isPrintKeepAspectRatio)
            {
                float xRatio = (float)e.MarginBounds.Width / (float)newSize.Width;
                float yRatio = (float)e.MarginBounds.Height / (float)newSize.Height;
                float ratio = Math.Min(xRatio, yRatio);

                newSize.Width *= ratio;
                newSize.Height *= ratio;
            }
            else if (_isPrintFillPage)
                newSize = e.MarginBounds.Size;

            mPane.ReSize(e.Graphics, new RectangleF(e.MarginBounds.Left,
                e.MarginBounds.Top, newSize.Width, newSize.Height));

            graphicalPressureZedGraph.SetScrollRangeFromData();
            graphicalPressureZedGraph.IsAutoScrollRange = true;
            graphicalPressureZedGraph.ScrollGrace = graphicalPressureZedGraph.ScrollMaxX - graphicalPressureZedGraph.ScrollMaxX;
            graphicalPressureZedGraph.GraphPane.XAxis.Scale.Min = mPrintedGraphPages * graphicalPressureZedGraph.ScrollMaxX / mPageGraphSpan;
            graphicalPressureZedGraph.GraphPane.XAxis.Scale.Max = (mPrintedGraphPages + 1) * graphicalPressureZedGraph.ScrollMaxX / mPageGraphSpan;
            graphicalPressureZedGraph.AxisChange();

            mPane.Draw(e.Graphics);

            using (Graphics g = this.CreateGraphics())
            {
                mPane.ReSize(g, saveRect);
                //g.Dispose();
            }

            mPane.IsPenWidthScaled = isPenSave[0];
            mPane.IsFontsScaled = isFontSave[0];
            for (int i = 0; i < mPane.PaneList.Count; i++)
            {
                foreach (CurveItem curve in mPane[i].CurveList)
                    if (curve is LineItem)
                        (curve as LineItem).Line.Width *= 2.5f;

                //mPane[i].Fill = paneFillSave[i];

                mPane[i].IsPenWidthScaled = isPenSave[i + 1];
                mPane[i].IsFontsScaled = isFontSave[i + 1];
            }

            //mPane.Fill = fillSave;
            mPane.Border.IsVisible = borderSave;

            myPane.Chart.Fill = chartFillSave;
            myPane.Fill = paneFillSave;

            myPane.XAxis.MajorTic.PenWidth *= 2.0f;
            myPane.XAxis.MinorTic.PenWidth *= 2.5f;
            myPane.XAxis.MajorGrid.PenWidth *= 2.0f;
            myPane.XAxis.MinorGrid.PenWidth *= 2.5f;
            myPane.YAxis.MajorTic.PenWidth *= 2.0f;
            myPane.YAxis.MinorTic.PenWidth *= 2.5f;
            myPane.YAxis.MajorGrid.PenWidth *= 2.0f;
            myPane.YAxis.MinorGrid.PenWidth *= 2.5f;

        }

        private void PrintSinglePage(object sender, PrintPageEventArgs e)
        {
            if (!mTitlePagePrinted)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

                TitlePage_PrintPage(sender, e);

                mTitlePagePrinted = true;
                e.HasMorePages = true;
                return;
            }

            // Draw the plot on the first page
            if (!mGraphPrinted)
            {
                SuspendLayout();

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

                int graphsToPrint = 1;
                ZGCheckbox[] checkboxes = CollectZGCheckboxes();
                foreach (ZGCheckbox checkbox in checkboxes)
                    if ((checkbox.Tag as CheckBox).Checked)
                        ++graphsToPrint;

                if (mPrintedGraphs > 0)
                {
                    int graph = mPrintedGraphs - 1;

                    int i = 0;
                    bool[] state = new bool[checkboxes.Length];
                    foreach (ZGCheckbox checkbox in checkboxes)
                    {
                        state[i++] = checkbox.Checked;

                        if ((checkbox.Tag as CheckBox).Checked && (graph-- == 0))
                            checkbox.Checked = true;
                        else
                            checkbox.Checked = false;
                    }

                    ZedGraph_PrintPage(sender, e, true, true, false);

                    i = 0;
                    foreach (ZGCheckbox checkbox in checkboxes)
                        checkbox.Checked = state[i++];
                }
                else
                    ZedGraph_PrintPage(sender, e, true, true, false);

                if (++mPrintedGraphPages >= mPageGraphSpan)
                {
                    mPrintedGraphPages = 0;
                    ++mPrintedGraphs;
                }

                if (mPrintedGraphs >= graphsToPrint)
                {
                    mGraphPrinted = true;
                    if (_printTableData)
                    {
                        e.HasMorePages = true;
                    }
                }
                else
                {
                    e.HasMorePages = true;
                }



                ResumeLayout(false);
                return;
            }

            // Check flag if table data should be printed
            if (_printTableData)
            {
                if (mGraphPrinted && !mPressuresDGVPrinted)
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

                    DGVPressuresPrintProvider.PrintDocument_PrintPage(sender, e);
                    if (e.HasMorePages == false)
                    {
                        e.HasMorePages = true;
                        mPressuresDGVPrinted = true;
                        // Switch to printing currents data
                    }
                    return;
                }

                if (mPressuresDGVPrinted && !mCurrentsDGVPrinted)
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

                    DGVCurrentsPrintProvider.PrintDocument_PrintPage(sender, e);
                    if (e.HasMorePages == false)
                    {
                        //e.HasMorePages = true;
                        mCurrentsDGVPrinted = true;
                    }
                    return;
                }
            }
        }

        #endregion Report Printing



        private void DrawPlot()
        {
            SuspendLayout();
            int ackLinesCount = 0;
            for (int i = 0; i < mTestScript.FrameSet.Count; i++)
            {
                if (((TestScriptFrame)(mTestScript.FrameSet[i])).AcquireData)
                {
                    ackLinesCount++;
                    //Console.Write("READVALS: ");
                    //for (int z = 0; z < 14; z++)
                    //{
                    //  Console.Write(((TestScriptFrame)(mTestScript.FrameSet[i])).PressureReadValues[z].ToString("00.00") + " ");
                    //}
                    //Console.WriteLine();
                }
            }
            TestScriptFrame[] ackLines = new TestScriptFrame[ackLinesCount];
            int ackLinesArrPos = 0;
            for (int i = 0; i < mTestScript.FrameSet.Count; i++)
            {
                if (((TestScriptFrame)(mTestScript.FrameSet[i])).AcquireData)
                {
                    ackLines[ackLinesArrPos] = (TestScriptFrame)mTestScript.FrameSet[i];
                    ackLinesArrPos++;
                }
            }

            string[] xLabels = new string[ackLinesCount];
            for (int i = 0; i < ackLinesCount; i++)
            {
                xLabels[i] = ackLines[i].FrameName;
            }

            GraphPane myPane = graphicalPressureZedGraph.GraphPane;
            myPane.CurveList.Clear();

            // Set the titles and axis labels
            myPane.Title.Text = mTestScript.Gearbox.Name + " test report\n" + mTestScriptReport.TimePerformed.ToShortDateString() + " " + mTestScriptReport.TimePerformed.ToLongTimeString();
            myPane.Title.FontSpec.Family = "Arial";
            myPane.Title.FontSpec.Size = 12.0f;
            myPane.XAxis.Title.Text = "Gear";
            myPane.XAxis.Title.FontSpec.FontColor = Color.FromArgb(72, 72, 72);
            myPane.XAxis.Title.FontSpec.IsBold = false;
            myPane.XAxis.Scale.FontSpec.Size = 10.0f;
            myPane.XAxis.Scale.FontSpec.IsAntiAlias = true;
            myPane.XAxis.Scale.MajorStepAuto = true;
            myPane.XAxis.Type = AxisType.Text;
            myPane.XAxis.Scale.FontSpec.Angle = 90.0f;
            myPane.YAxis.Title.Text = "Analogue input value";
            myPane.YAxis.Title.FontSpec.FontColor = Color.FromArgb(72, 72, 72);
            myPane.YAxis.Title.FontSpec.IsBold = false;
            myPane.YAxis.Scale.FontSpec.Size = 10.0f;
            myPane.YAxis.Scale.FontSpec.IsAntiAlias = true;


            // Clear & prepare the legend
            legendPanel.Controls.Clear();
            allOnOffCb.Checked = true;
            showRefCb.Checked = true;

            //add curves to the plot
            for (int i = 0; i < mTestScript.Gearbox._analogueInputs.Count; i++)
            {
                GearShiftUsb.AIChannel aic = mTestScript.Gearbox._analogueInputs[i];
                PointPairList rdList = new PointPairList();
                PointPairList refList = new PointPairList();
                foreach (TestScriptFrame frame in ackLines)
                {
                    // Add a read value with precision limited to 2 decimal points
                    float readVal = MeasurementUnit.ConvertAIValueBaseUnitToUserUnit(frame.PressureReadValues[i], aic.ValueType);
                    //float readVal =  MeasurementUnit.ConvertAIValueBaseUnitToUserUnit(frame.PressureReadValues[aic.InputIndex], aic.ValueType);
                    rdList.Add(i, Math.Round(readVal, 2));
                    if (frame.MasterPressureReadValues.Count > i)
                    {
                        // Add a master value with precision limited to 2 decimal points
                        float masterVal = MeasurementUnit.ConvertAIValueBaseUnitToUserUnit(frame.MasterPressureReadValues[i], aic.ValueType);
                        //float masterVal = MeasurementUnit.ConvertAIValueBaseUnitToUserUnit(frame.MasterPressureReadValues[aic.InputIndex], aic.ValueType);
                        refList.Add(i, Math.Round(masterVal, 2));
                    }
                }
                // To have the data points shown, use the commented line below:
                LineItem rdCurve = myPane.AddCurve(aic.Label, rdList, mLineColorsArray[i], SymbolType.None);
                rdCurve.Line.IsSmooth = true;
                rdCurve.Line.SmoothTension = 0.3f;
                rdCurve.Line.Width = 2.0f;
                rdCurve.Line.IsAntiAlias = true;
                rdCurve.IsVisible = true;

                LineItem refCurve = myPane.AddCurve(aic.Label + " Master", refList, Color.FromArgb(
                  (int)mLineColorsArray[i].R * 4 / 5,
                  (int)mLineColorsArray[i].G * 4 / 5,
                  (int)mLineColorsArray[i].B * 4 / 5), SymbolType.None);
                refCurve.Line.Style = System.Drawing.Drawing2D.DashStyle.Dash;
                refCurve.Line.IsSmooth = true;
                refCurve.Line.SmoothTension = 0.3f;
                refCurve.Line.Width = 1.5f;
                refCurve.Line.IsAntiAlias = true;
                refCurve.IsVisible = true;
                refCurve.Label.IsVisible = false;

                // Add checkboxes to the legend
                ZGCheckbox cb = new ZGCheckbox(rdCurve, refCurve, graphicalPressureZedGraph);
                cb.Appearance = System.Windows.Forms.Appearance.Button;
                cb.BackColor = System.Drawing.Color.Transparent;
                cb.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
                cb.Font = new Font(SystemFonts.DefaultFont, FontStyle.Bold);
                cb.Location = new System.Drawing.Point(3, 3);
                cb.Size = new System.Drawing.Size(120, 30);
                cb.TabIndex = 0;
                cb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                cb.UseVisualStyleBackColor = false;
                cb.Text = aic.Label;
                cb.ForeColor = mLineColorsArray[i];
                cb.Checked = true;

                CheckBox printCheckbox = new CheckBox();
                printCheckbox.Appearance = System.Windows.Forms.Appearance.Button;
                printCheckbox.Image = global::GST.Gearshift.Components.Properties.Resources.ReportViewerPanel_printer_off;
                printCheckbox.Location = new System.Drawing.Point(124, 3);
                printCheckbox.Name = "checkBox2";
                printCheckbox.Size = new System.Drawing.Size(30, 30);
                printCheckbox.UseVisualStyleBackColor = true;
                printCheckbox.TabIndex = 1;
                cb.Tag = printCheckbox;

                Panel panel = new Panel();
                panel.SuspendLayout();
                panel.Controls.Add(printCheckbox);
                panel.Controls.Add(cb);
                panel.Location = new System.Drawing.Point(12, 22);
                panel.Name = "panel1";
                panel.Size = new System.Drawing.Size(157, 36);
                panel.BackColor = Color.Transparent;
                panel.ResumeLayout(false);

                legendPanel.Controls.Add(panel);

                ResumeLayout();
            }


            myPane.XAxis.Scale.TextLabels = xLabels;
            myPane.XAxis.CrossAuto = true;

            // Fill the axis background with a color gradient
            myPane.Chart.Fill = new Fill(Color.White, Color.FromArgb(220, 220, 255), 45F);

            // Fill the pane background with a color gradient
            myPane.Fill = new Fill(Color.LightGray, Color.Gainsboro, 90F);

            myPane.Legend.Fill = new Fill(Color.White);
            myPane.Legend.FontSpec.Family = "Arial";
            myPane.Legend.FontSpec.Size = 7.25f;
            myPane.Legend.IsVisible = true;
            myPane.Legend.Position = LegendPos.BottomCenter;

            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;
            myPane.XAxis.MajorGrid.Color = Color.Black;
            myPane.YAxis.MajorGrid.Color = Color.Black;

            // Calculate the Axis Scale Ranges
            graphicalPressureZedGraph.AxisChange();
            // Call the graph to redraw
            graphicalPressureZedGraph.Invalidate();
        }

        ZGCheckbox[] CollectZGCheckboxes()
        {
            List<ZGCheckbox> ctrls = new List<ZGCheckbox>();
            foreach (Control panel in legendPanel.Controls)
            {
                if (!(panel is Panel))
                    continue;

                foreach (Control subCtrl in panel.Controls)
                    if (subCtrl is ZGCheckbox)
                        ctrls.Add(subCtrl as ZGCheckbox);
            }
            return ctrls.ToArray();
        }

        void UpdateLegendVisibility()
        {
            ZGCheckbox visible = null;
            int count = 0;
            foreach (ZGCheckbox cb in CollectZGCheckboxes())
            {
                if (cb.ReadCurve.IsVisible)
                {
                    ++count;
                    visible = cb;
                }
            }

            graphicalPressureZedGraph.GraphPane.Legend.IsVisible = count > 1;

            string page = "";
            if (mPrinting)
                page = " (page " + (mPrintedGraphPages + 1).ToString() + "/" + mPageGraphSpan.ToString() + ")";


            if (count == 1)
            {
                graphicalPressureZedGraph.GraphPane.Title.Text = mTestScript.Gearbox.Name + " test report on "
                  + mTestScriptReport.TimePerformed.ToShortDateString() + " " + mTestScriptReport.TimePerformed.ToLongTimeString() + page + "\n"
                  + visible.ReadCurve.Label.Text;
            }
            else
            {
                graphicalPressureZedGraph.GraphPane.Title.Text = mTestScript.Gearbox.Name + " test report" + page + "\n"
                  + mTestScriptReport.TimePerformed.ToShortDateString() + " " + mTestScriptReport.TimePerformed.ToLongTimeString();
            }
        }

        void AllOnOffCbCheckedChanged(object sender, EventArgs e)
        {
            SuspendLayout();
            foreach (Object obj in CollectZGCheckboxes())
            {
                if (obj is ZGCheckbox)
                {
                    ZGCheckbox cb = (ZGCheckbox)obj;
                    cb.Checked = ((CheckBox)sender).Checked;
                }
            }
            ResumeLayout();
        }

        void RefOnOffCbCheckedChanged(object sender, EventArgs e)
        {
            SuspendLayout();
            foreach (Object obj in CollectZGCheckboxes())
            {
                if (obj is ZGCheckbox)
                {
                    ZGCheckbox cb = (ZGCheckbox)obj;
                    if (((CheckBox)sender).Checked)
                    {
                        cb.ReferenceOn();
                    }
                    else
                    {
                        cb.ReferenceOff();
                    }
                }
            }
            ResumeLayout();
        }

        private void LoadDriveValuesTable()
        {
            ///////////////////////////////////////////////////////////////////////////////////
            // Sort out columns

            driveTable.ColumnModel = new ColumnModel();
            driveTable.ColumnModel.HeaderHeight = 40;
            driveTable.ColumnModel.Columns.Clear();

            // Add Gear name column
            TextColumn col1 = new TextColumn("Gear", 100);
            col1.ToolTipText = "Gear name";
            col1.Editable = false;
            col1.Sortable = false;
            col1.Alignment = ColumnAlignment.Center;
            col1.AutoResizeMode = ColumnAutoResizeMode.Grow;
            driveTable.ColumnModel.Columns.Add(col1);

            for (int i = 0; i < mTestScript.Gearbox.CurrentDisplayChannelsCount; i++)
            {
                DisplayChannel dchan = mTestScript.Gearbox.CurrentDisplayChannelsSet.Channels[i];

                TextColumn drvDCCol = new TextColumn(dchan.Label, 120);
                drvDCCol.Editable = false;
                drvDCCol.Sortable = false;
                drvDCCol.Alignment = ColumnAlignment.Center;
                drvDCCol.AutoResizeMode = ColumnAutoResizeMode.Grow;
                drvDCCol.ToolTipText = dchan.Label + " % drive and reported current for this script line";
                driveTable.ColumnModel.Columns.Add(drvDCCol);

            }
            driveTable.TableModel = new TableModel();
            driveTable.EndUpdate();
            driveTable.AutoResizeColumnWidths();


            //////////////////////////////////////////////////////////////////////////
            //Do job with rows

            for (int i = 0; i < mTestScript.FrameSet.Count; i++)
            {
                if (mTestScript.FrameSet[i].AcquireData)
                {
                    Row row = new Row();
                    TestScriptFrame frame = mTestScript.FrameSet[i];
                    row.Cells.Add(new Cell(frame.FrameName));

                    for (int j = 0; j < mTestScript.Gearbox.CurrentDisplayChannelsCount; j++)
                    {
                        DisplayChannel dchan = (DisplayChannel)mTestScript.Gearbox.CurrentDisplayChannelsSet.Channels[j];
                        int index = dchan.InputChannelIndex;
                        if (index >= 9)
                            index -= 9;
                        string drive = string.Format("{0, 3}", frame.ChannelDrives[index]) + "%";
                        string curr = string.Format("{0, 4}", (frame.CurrentReadValues[index]).ToString("0.00")) + "A";
                        row.Cells.Add(new Cell(drive + "  " + curr));
                        //row.Cells.Add(new Cell(string.Format("{0, 4}", (frame.CurrentReadValues[index]).ToString("0.00")) + "A"));
                    }
                    driveTable.TableModel.Rows.Add(row);
                }
            }
        }

        private void LoadPassFailTable()
        {
            passFailTable.BeginUpdate();
            ///////////////////////////////////////////////////////////////////////////////////
            // Sort out columns

            passFailTable.ColumnModel = new ColumnModel();
            passFailTable.TableModel = new TableModel();
            passFailTable.ColumnModel.HeaderHeight = 40;
            passFailTable.ColumnModel.Columns.Clear();

            // Add Gear name column
            TextColumn col1 = new TextColumn("Gear", 100);
            col1.ToolTipText = "Gear name";
            col1.Editable = false;
            col1.Sortable = false;
            col1.Alignment = ColumnAlignment.Center;
            col1.AutoResizeMode = ColumnAutoResizeMode.Grow;
            passFailTable.ColumnModel.Columns.Add(col1);

            for (int i = 0; i < mTestScript.Gearbox._analogueInputs.Count; i++)
            {
                GearShiftUsb.AIChannel aic = mTestScript.Gearbox._analogueInputs[i];
                Console.WriteLine(aic.Label);
                TextColumn drvDCCol = new TextColumn(aic.Label + " " + aic.UnitText);
                passFailTable.ColumnModel.Columns.Add(drvDCCol);
                drvDCCol.AutoResizeMode = ColumnAutoResizeMode.Grow;
                drvDCCol.ContentWidth = 220;
                //drvDCCol.Width = 220;
                drvDCCol.Editable = false;
                drvDCCol.Sortable = false;
                drvDCCol.Alignment = ColumnAlignment.Center;
                drvDCCol.Resizable = true;
            }

            //////////////////////////////////////////////////////////////////////////
            //Do job with rows

            for (int i = 0; i < mTestScript.FrameSet.Count; i++)
            {
                if (mTestScript.FrameSet[i].AcquireData)
                {

                    Row row = new Row();
                    passFailTable.TableModel.Rows.Add(row);

                    TestScriptFrame frame = mTestScript.FrameSet[i];
                    row.Cells.Add(new Cell(frame.FrameName));

                    for (int j = 0; j < mTestScript.Gearbox._analogueInputs.Count; j++)
                    {
                        GearShiftUsb.AIChannel aic = mTestScript.Gearbox._analogueInputs[j];
                        int inputChanIndex = aic.InputIndex;

                        if (frame.PressureReadValues[j] < mTestScript.Gearbox.IgnorePressureLessThan_BaseUnit && aic.ValueType == MeasurementUnit.ValueType.Pressure)
                        {
                            // If the pressure is lower than the minimum value then it should be ignored and must not be marked as error value
                            float readValue = MeasurementUnit.ConvertAIValueBaseUnitToUserUnit(frame.PressureReadValues[j], aic.ValueType);
                            float masterValue = MeasurementUnit.ConvertAIValueBaseUnitToUserUnit(frame.MasterPressureReadValues[j], aic.ValueType);
                            string cellVal = string.Format("{0 , 6}", readValue.ToString("0.00")) + " ";
                            cellVal += "(" + string.Format("{0 , 6}", masterValue.ToString("0.00")) + ")";
                            Cell cell = new Cell(cellVal);
                            row.Cells.Add(cell);
                            cell.BackColor = neutralCellBgColor;
                        }
                        else
                        {
                            // If the pressure is above the minimum value, apply the pass/fail criterion
                            float readValue = MeasurementUnit.ConvertAIValueBaseUnitToUserUnit(frame.PressureReadValues[j], aic.ValueType);
                            float masterValue = MeasurementUnit.ConvertAIValueBaseUnitToUserUnit(frame.MasterPressureReadValues[j], aic.ValueType);
                            float err = readValue - masterValue;
                            float errAbsVal = Math.Abs(err);
                            float errperc = errAbsVal * 100.0f / masterValue;
                            float maxErr = masterValue * mTestScript.Gearbox.PressureVariationTolerance;
                            string errSign, cellVal;
                            errSign = ((err >= 0) ? "+" : "-");
                            cellVal = string.Format("{0 , 6}", readValue.ToString("0.00")) + " ";
                            cellVal += "(" + string.Format("{0 , 6}", masterValue.ToString("0.00")) + "" + errSign + "";
                            cellVal += string.Format("{0 , 5}", errperc.ToString("0.0")) + "%)";
                            Cell cell = new Cell(cellVal);
                            row.Cells.Add(cell);
                            if (errAbsVal > maxErr)
                            {
                                cell.BackColor = failedCellBgColor;
                            }
                            else
                            {
                                cell.BackColor = passedCellBgColor;
                            }
                        }
                    }
                }
            }

            passFailTable.EndUpdate();
            passFailTable.AutoResizeColumnWidths();
        }

        void LoadControls()
        {
            testNameTextBox.Text = mTestScriptReport.TestScriptRunned.Name;
            timestampTextBox.Text = mTestScriptReport.TimePerformed.ToString();
            operatorTextBox.Text = mTestScriptReport.OperatorName;
            serialNoTextBox.Text = mTestScriptReport.GearboxSerialNumber;
            commentTextBox.Text = mTestScriptReport.Comment;

            // Draw plots
            DrawPlot();

            // Load drive values table
            LoadDriveValuesTable();

            //Load pressure report table
            LoadPassFailTable();
        }

        #endregion Methods

        private void setRefButton_Click(object sender, EventArgs e)
        {
            SetMasterDataForm smdf = new SetMasterDataForm(this.mTestScriptReport);
            smdf.ShowDialog();

            // The test script could have been edited.
            mTestScript = mTestScriptReport.TestScriptRunned;
            LoadControls();
        }

        private void pageSpanAdd_Click(object sender, EventArgs e)
        {
            ++mPageGraphSpan;
            UpdateSpan();
        }

        private void pageSpanMinus_Click(object sender, EventArgs e)
        {
            if (mPageGraphSpan > 1)
                --mPageGraphSpan;

            UpdateSpan();
        }

        private void OnAdjustGraph(object sender, EventArgs e)
        {
            UpdateSpan();
        }

        private void UpdateSpan()
        {
            if (mPageGraphSpan == 1)
                pageSpanLabel.Text = "Span: 1 page";
            else
                pageSpanLabel.Text = "Span: " + mPageGraphSpan.ToString() + " pages";

            Size graphSize = graphDockPanel.ClientSize;

            //graphicalPressureZedGraph.Size = graphSize;

            graphicalPressureZedGraph.SetScrollRangeFromData();
            graphicalPressureZedGraph.IsAutoScrollRange = true;
            graphicalPressureZedGraph.ScrollGrace = graphicalPressureZedGraph.ScrollMaxX - graphicalPressureZedGraph.ScrollMaxX;
            graphicalPressureZedGraph.IsShowHScrollBar = mPageGraphSpan > 1;

            graphicalPressureZedGraph.GraphPane.XAxis.Scale.Max = graphicalPressureZedGraph.ScrollMaxX / mPageGraphSpan;

            graphicalPressureZedGraph.AxisChange();
        }

        private void saveCommentsButton_Click(object sender, EventArgs e)
        {
            try
            {
                mTestScriptReport.Comment = commentTextBox.Text;
                mTestScriptReport.SaveToFile(mTestScriptReport.Filename);
                Soko.Common.Forms.MessageBox.ShowInfo("GearShift", "Operation successfull",
                                                        "The comments have been succesfully saved",
                                                        Soko.Common.Forms.MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                Soko.Common.Forms.MessageBox.ShowCritical("GearShift",
                                                                "File saving error",
                                                                "Saving the comments failed due to the following error:\n" + ex.Message,
                                                                Soko.Common.Forms.MessageBoxButtons.OK);
            }
        }

        private void PrintReportWithTablesButton_Click(object sender, EventArgs e)
        {
            _printTableData = true;
            this.PrintCurrentReport();
        }

        private void printReportGraphsOnlyButton_Click(object sender, EventArgs e)
        {
            _printTableData = false;
            this.PrintCurrentReport();
        }


    }
}
