namespace GST.Gearshift.Components.Forms.CAN
{
  partial class CanCorderPanel
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.recordingTimer = new System.Windows.Forms.Timer(this.components);
      this.table = new XPTable.Models.Table();
      this.nicePanel1 = new Soko.Common.Controls.NicePanel();
      this.sentCountLabel = new System.Windows.Forms.Label();
      this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
      this.totalMsgLabel = new System.Windows.Forms.Label();
      this.currTimeLabel = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.currMsgLabel = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.progressBar3 = new System.Windows.Forms.ProgressBar();
      this.progressBar2 = new System.Windows.Forms.ProgressBar();
      this.progressBar1 = new System.Windows.Forms.ProgressBar();
      this.playbackTimer = new System.Windows.Forms.Timer(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.table)).BeginInit();
      this.nicePanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
      this.SuspendLayout();
      // 
      // recordingTimer
      // 
      this.recordingTimer.Tick += new System.EventHandler(this.recordingTimer_Tick);
      // 
      // table
      // 
      this.table.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.table.BackColor = System.Drawing.Color.WhiteSmoke;
      this.table.EnableToolTips = true;
      this.table.Location = new System.Drawing.Point(0, 77);
      this.table.Name = "table";
      this.table.Size = new System.Drawing.Size(745, 390);
      this.table.TabIndex = 0;
      this.table.Text = "table1";
      this.table.ToolTipInitialDelay = 500;
      this.table.ToolTipShowAlways = true;
      // 
      // nicePanel1
      // 
      this.nicePanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.nicePanel1.AutoplaceElements = false;
      this.nicePanel1.AutoScrollHorizontalMaximum = 100;
      this.nicePanel1.AutoScrollHorizontalMinimum = 0;
      this.nicePanel1.AutoScrollHPos = 0;
      this.nicePanel1.AutoScrollVerticalMaximum = 100;
      this.nicePanel1.AutoScrollVerticalMinimum = 0;
      this.nicePanel1.AutoScrollVPos = 0;
      this.nicePanel1.AutoSizeElements = false;
      this.nicePanel1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.nicePanel1.backgroundColor1 = System.Drawing.Color.WhiteSmoke;
      this.nicePanel1.backgroundColor2 = System.Drawing.Color.Wheat;
      this.nicePanel1.Controls.Add(this.sentCountLabel);
      this.nicePanel1.Controls.Add(this.totalMsgLabel);
      this.nicePanel1.Controls.Add(this.currTimeLabel);
      this.nicePanel1.Controls.Add(this.label6);
      this.nicePanel1.Controls.Add(this.currMsgLabel);
      this.nicePanel1.Controls.Add(this.label4);
      this.nicePanel1.Controls.Add(this.label3);
      this.nicePanel1.Controls.Add(this.label2);
      this.nicePanel1.Controls.Add(this.label1);
      this.nicePanel1.Controls.Add(this.progressBar3);
      this.nicePanel1.Controls.Add(this.progressBar2);
      this.nicePanel1.Controls.Add(this.progressBar1);
      this.nicePanel1.Controls.Add(this.numericUpDown1);
      this.nicePanel1.DrawBackImage = false;
      this.nicePanel1.EnableAutoScrollHorizontal = false;
      this.nicePanel1.EnableAutoScrollVertical = false;
      this.nicePanel1.Gradient = Soko.Common.Controls.NicePanel.GradientType.None;
      this.nicePanel1.HorizontalMargin = 0;
      this.nicePanel1.Location = new System.Drawing.Point(0, 0);
      this.nicePanel1.Name = "nicePanel1";
      this.nicePanel1.roundingRadius = 10;
      this.nicePanel1.Size = new System.Drawing.Size(745, 77);
      this.nicePanel1.TabIndex = 1;
      this.nicePanel1.VerticalMargin = 0;
      this.nicePanel1.VisibleAutoScrollHorizontal = false;
      this.nicePanel1.VisibleAutoScrollVertical = false;
      // 
      // sentCountLabel
      // 
      this.sentCountLabel.AutoSize = true;
      this.sentCountLabel.Location = new System.Drawing.Point(169, -20);
      this.sentCountLabel.Name = "sentCountLabel";
      this.sentCountLabel.Size = new System.Drawing.Size(35, 13);
      this.sentCountLabel.TabIndex = 5;
      this.sentCountLabel.Text = "label5";
      // 
      // numericUpDown1
      // 
      this.numericUpDown1.Location = new System.Drawing.Point(172, -20);
      this.numericUpDown1.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
      this.numericUpDown1.Name = "numericUpDown1";
      this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
      this.numericUpDown1.TabIndex = 4;
      // 
      // totalMsgLabel
      // 
      this.totalMsgLabel.AutoSize = true;
      this.totalMsgLabel.Location = new System.Drawing.Point(99, 36);
      this.totalMsgLabel.Name = "totalMsgLabel";
      this.totalMsgLabel.Size = new System.Drawing.Size(13, 13);
      this.totalMsgLabel.TabIndex = 3;
      this.totalMsgLabel.Text = "0";
      // 
      // currTimeLabel
      // 
      this.currTimeLabel.AutoSize = true;
      this.currTimeLabel.Location = new System.Drawing.Point(99, 8);
      this.currTimeLabel.Name = "currTimeLabel";
      this.currTimeLabel.Size = new System.Drawing.Size(45, 13);
      this.currTimeLabel.TabIndex = 2;
      this.currTimeLabel.Text = "0.0000s";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(3, 22);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(90, 13);
      this.label6.TabIndex = 2;
      this.label6.Text = "Current Message:";
      // 
      // currMsgLabel
      // 
      this.currMsgLabel.AutoSize = true;
      this.currMsgLabel.Location = new System.Drawing.Point(99, 22);
      this.currMsgLabel.Name = "currMsgLabel";
      this.currMsgLabel.Size = new System.Drawing.Size(13, 13);
      this.currMsgLabel.TabIndex = 2;
      this.currMsgLabel.Text = "0";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(3, 36);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(84, 13);
      this.label4.TabIndex = 2;
      this.label4.Text = "Total messages:";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(3, 8);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(66, 13);
      this.label3.TabIndex = 2;
      this.label3.Text = "Current time:";
      // 
      // label2
      // 
      this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(449, 10);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(87, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "CAN Rx buffer fill";
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(449, 33);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(86, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "CAN Tx buffer fill";
      // 
      // progressBar3
      // 
      this.progressBar3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.progressBar3.Location = new System.Drawing.Point(6, 56);
      this.progressBar3.Name = "progressBar3";
      this.progressBar3.Size = new System.Drawing.Size(732, 15);
      this.progressBar3.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
      this.progressBar3.TabIndex = 0;
      // 
      // progressBar2
      // 
      this.progressBar2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.progressBar2.Location = new System.Drawing.Point(542, 32);
      this.progressBar2.Name = "progressBar2";
      this.progressBar2.Size = new System.Drawing.Size(196, 15);
      this.progressBar2.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
      this.progressBar2.TabIndex = 0;
      // 
      // progressBar1
      // 
      this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.progressBar1.Location = new System.Drawing.Point(542, 8);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new System.Drawing.Size(196, 15);
      this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
      this.progressBar1.TabIndex = 0;
      // 
      // playbackTimer
      // 
      this.playbackTimer.Tick += new System.EventHandler(this.playbackTimer_Tick);
      // 
      // CanCorderPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.nicePanel1);
      this.Controls.Add(this.table);
      this.Name = "CANCorder";
      this.Size = new System.Drawing.Size(745, 467);
      this.Load += new System.EventHandler(this.CANCorder_Load);
      ((System.ComponentModel.ISupportInitialize)(this.table)).EndInit();
      this.nicePanel1.ResumeLayout(false);
      this.nicePanel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Timer recordingTimer;
    private XPTable.Models.Table table;
    private Soko.Common.Controls.NicePanel nicePanel1;
    private System.Windows.Forms.ProgressBar progressBar1;
    private System.Windows.Forms.Label totalMsgLabel;
    private System.Windows.Forms.Label currTimeLabel;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label currMsgLabel;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ProgressBar progressBar3;
    private System.Windows.Forms.ProgressBar progressBar2;
    private System.Windows.Forms.Label sentCountLabel;
    private System.Windows.Forms.NumericUpDown numericUpDown1;
    private System.Windows.Forms.Timer playbackTimer;
  }
}
