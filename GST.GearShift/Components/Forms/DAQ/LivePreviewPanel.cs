using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GST.Gearshift.Components.Interfaces.USB;

using ZedGraph;

namespace GST.Gearshift.Components.Forms.DAQ
{
  public partial class LivePreviewPanel : UserControl
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
          if (p is LivePreviewPanel)
          {
            (p as LivePreviewPanel).UpdateLegendVisibility();
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

    public enum PlaybackMode
    {
      LivePlayback = 0,
      ReportViewer = 1
    }

    public enum PlaybackState
    {
      PlaybackStopped = 0,
      PlaybackPaused = 1,
      PlaybackRunning = 2
    }

    #region Constants

    private readonly Color[] _lineColorsArray = new Color[] 
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

    #endregion  Constants


    #region Private fields

    private PlaybackState _playbackState = PlaybackState.PlaybackStopped;

    TestScriptReport _report = new TestScriptReport();

    GearboxConfig _gearbox = new GearboxConfig();

    private int _sampleWindowSizeCurrent = 1000;



    private System.Diagnostics.Stopwatch _stopwatch = new System.Diagnostics.Stopwatch();

    private int _playbackFrameProgress = 0;

    private int _uploadedFramesCount = 0;

    #endregion Private fields



    #region Constructors & finalizer

    public LivePreviewPanel()
    {
      InitializeComponent();
    }

    #endregion Constructors & finalizer



    #region Events



    #endregion Events



    #region Properties

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public TestScriptReport Report
    {
      get { return _report; }
      set
      {
        if (value == null)
        {
          _report = new TestScriptReport();
          _gearbox = new GearboxConfig();
        }
        else
        {
          _report = value;
          _gearbox = _report.TestScriptRunned.Gearbox;
        }
        SetupGraph();
        SetLegendPanelItemsHeight();
        SetupPlayback();
        DrawGraph();
      }
    }

    private int _sampleWindowSizeUserSelected = 1000;
    /// <summary>
    /// Sample count to be displayed at once on a graph (100 frames is one second)
    /// </summary>
    public int SampleWindowSize
    {
      get { return _sampleWindowSizeUserSelected; }
      set
      {
        _sampleWindowSizeUserSelected = value;
        SetupGraph();
        DrawGraph();
      }
    }

    private PlaybackMode _playbackMode = PlaybackMode.LivePlayback;
    public PlaybackMode DataPlaybackMode
    {
      get { return _playbackMode; }
      set 
      {
        _playbackMode = value;
        if (_playbackMode == PlaybackMode.ReportViewer)
        {
          _playbackState = PlaybackState.PlaybackStopped;
        }
        SetupGraph();
        SetupPlayback();
        DrawGraph();
      }
    }

    #endregion Properties



    #region Methods

    #region Graph Drawing

    private void SetupGraph()
    {
      SuspendLayout();

      switch (_playbackMode)
      {
        default:
        case PlaybackMode.ReportViewer:
          {
            progressScrollbar.Enabled = true;
            progressScrollbar.Minimum = 0;
            progressScrollbar.Maximum = _report.LivePlaybackData.Count;
            playButton.Enabled = true;
            pauseButton.Enabled = true;
            stopButton.Enabled = true;
            break;
          }
        case PlaybackMode.LivePlayback:
          {
            progressScrollbar.Enabled = false;
            playButton.Enabled = false;
            pauseButton.Enabled = false;
            stopButton.Enabled = false;
            break;
          }
      }


      // Set the sample window size depending on the playback mode and state
      switch (_playbackMode)
      {
        case PlaybackMode.LivePlayback:
          {
            _sampleWindowSizeCurrent = _sampleWindowSizeUserSelected;
            break;
          }
        case PlaybackMode.ReportViewer:
          {
            switch (_playbackState)
            {
              case PlaybackState.PlaybackPaused:
              case PlaybackState.PlaybackRunning:
                {
                  _sampleWindowSizeCurrent = _sampleWindowSizeUserSelected;
                  break;
                }
              case PlaybackState.PlaybackStopped:
                {
                  _sampleWindowSizeCurrent = _report.LivePlaybackData.Count;
                  break;
                }
            }
            break;
          }
      }

      GraphPane myPane = graphicalPressureZedGraph.GraphPane;
      myPane.CurveList.Clear();
      legendPanel.Controls.Clear();

      // Set the titles and axis labels
      //myPane.Title.Text = _gearbox.Name + " test report\n" + _TestScriptReport.TimePerformed.ToShortDateString() + " " + _TestScriptReport.TimePerformed.ToLongTimeString();
      //myPane.Title.FontSpec.Family = "Arial";
      //myPane.Title.FontSpec.Size = 12.0f;

      // Setup X axis
      myPane.XAxis.Title.Text = "Time (1 tick one second)";
      myPane.XAxis.Title.FontSpec.Family = "Segoe UI";
      myPane.XAxis.Title.FontSpec.FontColor = Color.FromArgb(72, 72, 72);
      myPane.XAxis.Title.FontSpec.IsBold = false;
      myPane.XAxis.Scale.FontSpec.Size = 10.0f;
      myPane.XAxis.Scale.FontSpec.IsAntiAlias = true;
      myPane.XAxis.Type = AxisType.Text;
      myPane.XAxis.Scale.Min = 0;
      myPane.XAxis.Scale.Max = _sampleWindowSizeCurrent;
      myPane.XAxis.Scale.MinorStep = 50;
      myPane.XAxis.Scale.MajorStep = 100; // 1 second
      myPane.XAxis.Scale.FontSpec.Angle = 90.0f;
      myPane.XAxis.MajorGrid.IsVisible = true;
      myPane.XAxis.MajorGrid.Color = Color.Black;
      myPane.XAxis.MinorGrid.IsVisible = false;
      myPane.XAxis.CrossAuto = true;

      // Setup Y axis
      myPane.YAxis.Title.Text = "Analogue input value";
      myPane.YAxis.Title.FontSpec.Family = "Segoe UI";
      myPane.YAxis.Title.FontSpec.FontColor = Color.FromArgb(72, 72, 72);
      myPane.YAxis.Title.FontSpec.IsBold = false;
      myPane.YAxis.Scale.FontSpec.Size = 10.0f;
      myPane.YAxis.Scale.FontSpec.IsAntiAlias = true;
      myPane.YAxis.MajorGrid.IsVisible = true;
      myPane.YAxis.MajorGrid.Color = Color.Black;

      

      // Fill the axis background with a color gradient
      myPane.Chart.Fill = new Fill(Color.White, Color.FromArgb(220, 220, 255), 45F);

      // Fill the pane background with a color gradient
      myPane.Fill = new Fill(Color.LightGray, Color.Gainsboro, 90F);

      // Setup legend
      myPane.Legend.Fill = new Fill(Color.White);
      myPane.Legend.FontSpec.Family = "Consolas";
      myPane.Legend.FontSpec.Size = 7.25f;
      myPane.Legend.IsVisible = true;
      myPane.Legend.Position = LegendPos.BottomCenter;

      allOnOffCb.Checked = true;

      //add curves to the plot
      for (int i = 0; i < _gearbox._analogueInputs.Count; i++)
      {
        GearShiftUsb.AIChannel aic = _gearbox._analogueInputs[i];
        RollingPointPairList rdList = new RollingPointPairList(_sampleWindowSizeCurrent);
        RollingPointPairList refList = new RollingPointPairList(_sampleWindowSizeCurrent);

        // Add curves to the pane
        LineItem rdCurve = myPane.AddCurve(aic.Label, rdList, _lineColorsArray[i], SymbolType.None);
        rdCurve.Line.IsSmooth = false;
        rdCurve.Line.SmoothTension = 0.3f;
        rdCurve.Line.Width = 2.0f;
        rdCurve.Line.IsAntiAlias = true;
        rdCurve.IsVisible = true;

        // Add checkboxes to the legend
        ZGCheckbox cb = new ZGCheckbox(rdCurve, rdCurve, graphicalPressureZedGraph);
        cb.Appearance = System.Windows.Forms.Appearance.Button;
        cb.BackColor = System.Drawing.Color.Transparent;
        cb.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
        cb.Font = new Font(SystemFonts.DefaultFont, FontStyle.Bold);
        cb.Location = new System.Drawing.Point(3, 3);
        cb.Size = new System.Drawing.Size(160, 20);
        cb.MinimumSize = new System.Drawing.Size(160, 15);
        cb.MaximumSize = new System.Drawing.Size(160, 45);
        cb.TabIndex = 0;
        cb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        cb.UseVisualStyleBackColor = false;
        cb.Text = aic.Label;
        cb.ForeColor = _lineColorsArray[i];
        cb.Checked = true;

        legendPanel.Controls.Add(cb);
      }

      // Scale the axes
      graphicalPressureZedGraph.AxisChange();

      ResumeLayout();
    }

    private void DrawGraph()
    {
      SuspendLayout();

      // Refresh playback progress
      progressScrollbar.Value = _playbackFrameProgress;

      // save the buffer size locally because the items are added to the buffer on a different thread and this might change during execution
      int bufferFramesCount = _report.LivePlaybackData.Count;

      // In normal situation, there shoul be more data in the buffer than the currently needed for playback
      // Protect against accessing over the buffer size
      int framesToAdd = 0;
      if (_playbackFrameProgress <= bufferFramesCount)
      {
        framesToAdd = _playbackFrameProgress - _uploadedFramesCount;
      }
      else
      {
        framesToAdd = bufferFramesCount - _uploadedFramesCount;
        //Console.WriteLine("LP BUFFER UNDERRUN");
      }
      //Console.WriteLine(_playbackFrameProgress.ToString() + "   " + bufferFramesCount.ToString());

      // Add each frame to the plot
      for (int i = 0; i < framesToAdd; i++)
      {
        for (int j = 0; j < _gearbox._analogueInputs.Count; j++)
        {
          // Get the first CurveItem in the graph
          LineItem curve = graphicalPressureZedGraph.GraphPane.CurveList[j] as LineItem;
          if (curve != null)
          {

            // Get the PointPairList
            IPointListEdit list = curve.Points as IPointListEdit;
            // If this is null, it means the reference at curve.Points does not
            // support IPointListEdit, so we won't be able to modify it
            if (list != null)
            {
              list.Add(_uploadedFramesCount, _report.LivePlaybackData[_uploadedFramesCount].RecordedData[j]);
            }
          }
        }
        _uploadedFramesCount++;
      }


      // Display values in legend
      float[] lastFrame;
      if (_report.LivePlaybackData.Count > 0 && _uploadedFramesCount < _report.LivePlaybackData.Count)
      {
        lastFrame = _report.LivePlaybackData[_uploadedFramesCount].RecordedData;
      }
      else
      {
        lastFrame = new float[14];
      }

      for (int i = 0; i < _gearbox._analogueInputs.Count; i++)
      {
        GearShiftUsb.AIChannel aic = _gearbox._analogueInputs[i];
        CurveItem curve = graphicalPressureZedGraph.GraphPane.CurveList[i];
        curve.Label.Text = aic.Label + "\n" + lastFrame[i].ToString("0.00").PadLeft(6) + " " + aic.UnitText;
      }

      // Make sure the Y axis is rescaled to accommodate actual data
      graphicalPressureZedGraph.AxisChange();
      // Force a redraw
      graphicalPressureZedGraph.Invalidate();

      ResumeLayout();
    }

    private void UpdateLegendVisibility()
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
      graphicalPressureZedGraph.GraphPane.Legend.IsVisible = count > 0;
    }

    private ZGCheckbox[] CollectZGCheckboxes()
    {
      List<ZGCheckbox> ctrls = new List<ZGCheckbox>();
      foreach (Control subCtrl in legendPanel.Controls)
        if (subCtrl is ZGCheckbox)
          ctrls.Add(subCtrl as ZGCheckbox);
      return ctrls.ToArray();
    }

    private void refreshTimer_Tick(object sender, EventArgs e)
    {
      //Soko.Common.Common.HPTimer tmr = new Common.Common.HPTimer();
      //tmr.Start();
      _playbackFrameProgress = (int)(_stopwatch.ElapsedMilliseconds / 10);
      DrawGraph();
      //tmr.Stop();
      //Console.WriteLine((tmr.Duration*1000).ToString("00.0000"));
    }

    private void showRefCb_CheckedChanged(object sender, EventArgs e)
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

    private void allOnOffCb_CheckedChanged(object sender, EventArgs e)
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

    #endregion Graph Drawing

    public void StartPlayback()
    {
      switch (_playbackMode)
      {
        default:
        case PlaybackMode.ReportViewer:
          {
            switch (_playbackState)
            {
              default:
              case PlaybackState.PlaybackStopped:
                {
                  SetupGraph();
                  _playbackFrameProgress = 0;
                  _stopwatch.Reset();
                  _stopwatch.Start();
                  _uploadedFramesCount = 0;
                  refreshTimer.Enabled = true;
                  _playbackState = PlaybackState.PlaybackRunning;
                  break;
                }
              case PlaybackState.PlaybackRunning:
                {
                  // do nothing in this case
                  break;
                }
              case PlaybackState.PlaybackPaused:
                {
                  _stopwatch.Start(); // just resume time counting
                  refreshTimer.Enabled = true; // and enable GUI update
                  _playbackState = PlaybackState.PlaybackRunning;
                  break;
                }
            }
            break;
          }
        case PlaybackMode.LivePlayback:
          {
            _playbackFrameProgress = 0;
            _stopwatch.Reset();
            _stopwatch.Start();
            _uploadedFramesCount = 0;
            refreshTimer.Enabled = true;
            break;
          }
      }
    }

    public void StopPlayback()
    {
      _stopwatch.Stop();
      refreshTimer.Enabled = false;
      switch (_playbackMode)
      {
        default:
        case PlaybackMode.ReportViewer:
          {
            _playbackState = PlaybackState.PlaybackStopped;
            _uploadedFramesCount = 0;
            _playbackFrameProgress = _report.LivePlaybackData.Count; // Set the time to the end of report
            DrawGraph();
            break;
          }
        case PlaybackMode.LivePlayback:
          {
            _playbackFrameProgress = 0; // Reset the time
            break;
          }
      }
    }

    public void PausePlayback()
    {
      switch (_playbackMode)
      {
        default:
        case PlaybackMode.ReportViewer:
          {
            switch (_playbackState)
            {
              default:
              case PlaybackState.PlaybackStopped:
                {
                  // do nothing in this case
                  break;
                }
              case PlaybackState.PlaybackRunning:
                {
                  _stopwatch.Stop(); // just stop time counting
                  refreshTimer.Enabled = false; // and disable GUI update
                  _playbackState = PlaybackState.PlaybackPaused;
                  break;
                }
              case PlaybackState.PlaybackPaused:
                {
                  // do nothing in this case
                  break;
                }
            }
            break;
          }
        case PlaybackMode.LivePlayback:
          {
            _playbackFrameProgress = 0;
            _stopwatch.Reset();
            _stopwatch.Start();
            _uploadedFramesCount = 0;
            refreshTimer.Enabled = true;
            break;
          }
      }
    }

    private void SetupPlayback()
    {
      switch (_playbackMode)
      {
        default:
        case PlaybackMode.ReportViewer:
          {
            _playbackState = PlaybackState.PlaybackStopped;
            _uploadedFramesCount = 0;
            _playbackFrameProgress = _report.LivePlaybackData.Count; // Set the time to the end of report
            break;
          }
        case PlaybackMode.LivePlayback:
          {
            _playbackFrameProgress = 0; // Reset the time
            break;
          }
      }
    }

    #endregion Methods

    private void legendPanel_Resize(object sender, EventArgs e)
    {
      SetLegendPanelItemsHeight();
    }

    private void SetLegendPanelItemsHeight()
    {
      float legendUsableHeight = (legendPanel.Height * 0.9f);
      int newHeight = (int)(legendUsableHeight / _gearbox._analogueInputs.Count);

      foreach (ZGCheckbox zgc in CollectZGCheckboxes())
      {
        // max tem size is limited by the maxsize property
        zgc.Size = new Size(160, newHeight);
      }
      legendPanel.AutoplaceElements = true;
    }

    private void playButton_Click(object sender, EventArgs e)
    {
      if (_playbackMode == PlaybackMode.ReportViewer) StartPlayback();
    }

    private void pauseButton_Click(object sender, EventArgs e)
    {
      if (_playbackMode == PlaybackMode.ReportViewer) PausePlayback();
    }

    private void stopButton_Click(object sender, EventArgs e)
    {
      if (_playbackMode == PlaybackMode.ReportViewer) StopPlayback();
    }








  }
}
