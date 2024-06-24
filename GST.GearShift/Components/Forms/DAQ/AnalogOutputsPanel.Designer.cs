namespace GST.Gearshift.Components.Forms.DAQ
{
    partial class AOsPanel
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.updateTmr = new System.Windows.Forms.Timer(this.components);
            this.mainPanel = new Soko.Common.Controls.NicePanel();
            this.AO2NUD = new System.Windows.Forms.NumericUpDown();
            this.AO1NUD = new System.Windows.Forms.NumericUpDown();
            this.AO2ValueLabel = new System.Windows.Forms.Label();
            this.AO1ValueLabel = new System.Windows.Forms.Label();
            this.calibrateButton = new Soko.Common.Controls.NiceButton();
            this.label2 = new System.Windows.Forms.Label();
            this.AO2Trackbar = new Soko.Common.Controls.NiceTrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.AO2PercValueLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.AO1Trackbar = new Soko.Common.Controls.NiceTrackBar();
            this.driveCaptionLabel = new System.Windows.Forms.Label();
            this.channelLabel = new System.Windows.Forms.Label();
            this.AO1PercValueLabel = new System.Windows.Forms.Label();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AO2NUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AO1NUD)).BeginInit();
            this.SuspendLayout();
            // 
            // updateTmr
            // 
            this.updateTmr.Tick += new System.EventHandler(this.updateTmr_Tick);
            // 
            // mainPanel
            // 
            this.mainPanel.AutoplaceElements = false;
            this.mainPanel.AutoScrollHorizontalMaximum = 100;
            this.mainPanel.AutoScrollHorizontalMinimum = 0;
            this.mainPanel.AutoScrollHPos = 0;
            this.mainPanel.AutoScrollVerticalMaximum = 100;
            this.mainPanel.AutoScrollVerticalMinimum = 0;
            this.mainPanel.AutoScrollVPos = 0;
            this.mainPanel.AutoSizeElements = false;
            this.mainPanel.BackColor = System.Drawing.Color.Transparent;
            this.mainPanel.backgroundColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.mainPanel.backgroundColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.mainPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
            this.mainPanel.BorderWidth = 1;
            this.mainPanel.Controls.Add(this.AO2NUD);
            this.mainPanel.Controls.Add(this.AO1NUD);
            this.mainPanel.Controls.Add(this.AO2ValueLabel);
            this.mainPanel.Controls.Add(this.AO1ValueLabel);
            this.mainPanel.Controls.Add(this.calibrateButton);
            this.mainPanel.Controls.Add(this.label2);
            this.mainPanel.Controls.Add(this.AO2Trackbar);
            this.mainPanel.Controls.Add(this.label3);
            this.mainPanel.Controls.Add(this.AO2PercValueLabel);
            this.mainPanel.Controls.Add(this.label1);
            this.mainPanel.Controls.Add(this.AO1Trackbar);
            this.mainPanel.Controls.Add(this.driveCaptionLabel);
            this.mainPanel.Controls.Add(this.channelLabel);
            this.mainPanel.Controls.Add(this.AO1PercValueLabel);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.DrawBackImage = false;
            this.mainPanel.EnableAutoScrollHorizontal = false;
            this.mainPanel.EnableAutoScrollVertical = false;
            this.mainPanel.Gradient = Soko.Common.Controls.NicePanel.GradientType.SigmaBellHorizontalUpper;
            this.mainPanel.HorizontalMargin = 0;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.roundingRadius = 10;
            this.mainPanel.Size = new System.Drawing.Size(345, 459);
            this.mainPanel.SupportTransparentBackground = false;
            this.mainPanel.TabIndex = 1;
            this.mainPanel.VerticalMargin = 0;
            this.mainPanel.VisibleAutoScrollHorizontal = false;
            this.mainPanel.VisibleAutoScrollVertical = false;
            // 
            // AO2NUD
            // 
            this.AO2NUD.BackColor = System.Drawing.Color.GreenYellow;
            this.AO2NUD.DecimalPlaces = 2;
            this.AO2NUD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold);
            this.AO2NUD.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.AO2NUD.Location = new System.Drawing.Point(188, 500);
            this.AO2NUD.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.AO2NUD.Name = "AO2NUD";
            this.AO2NUD.Size = new System.Drawing.Size(120, 27);
            this.AO2NUD.TabIndex = 35;
            // 
            // AO1NUD
            // 
            this.AO1NUD.BackColor = System.Drawing.Color.GreenYellow;
            this.AO1NUD.DecimalPlaces = 1;
            this.AO1NUD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold);
            this.AO1NUD.Location = new System.Drawing.Point(24, 500);
            this.AO1NUD.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.AO1NUD.Name = "AO1NUD";
            this.AO1NUD.Size = new System.Drawing.Size(120, 27);
            this.AO1NUD.TabIndex = 35;
            // 
            // AO2ValueLabel
            // 
            this.AO2ValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.AO2ValueLabel.ForeColor = System.Drawing.Color.Black;
            this.AO2ValueLabel.Location = new System.Drawing.Point(188, 310);
            this.AO2ValueLabel.Name = "AO2ValueLabel";
            this.AO2ValueLabel.Size = new System.Drawing.Size(120, 25);
            this.AO2ValueLabel.TabIndex = 34;
            this.AO2ValueLabel.Text = "? A";
            this.AO2ValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AO1ValueLabel
            // 
            this.AO1ValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.AO1ValueLabel.ForeColor = System.Drawing.Color.Black;
            this.AO1ValueLabel.Location = new System.Drawing.Point(23, 310);
            this.AO1ValueLabel.Name = "AO1ValueLabel";
            this.AO1ValueLabel.Size = new System.Drawing.Size(120, 25);
            this.AO1ValueLabel.TabIndex = 33;
            this.AO1ValueLabel.Text = "? RPM";
            this.AO1ValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // calibrateButton
            // 
            this.calibrateButton.AutoSize = true;
            this.calibrateButton.BackColor = System.Drawing.Color.Transparent;
            this.calibrateButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.calibrateButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.calibrateButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.calibrateButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.calibrateButton.BackgroundColor = System.Drawing.Color.DarkGray;
            this.calibrateButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.calibrateButton.BorderColor = System.Drawing.Color.LightGray;
            this.calibrateButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.calibrateButton.BorderWidth = 1;
            this.calibrateButton.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.calibrateButton.ContentPadding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.calibrateButton.DrawBackColorOnFocus = false;
            this.calibrateButton.DrawBackgroundImage = false;
            this.calibrateButton.DrawBorderOnFocus = false;
            this.calibrateButton.DrawBorderOnTop = false;
            this.calibrateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.calibrateButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.calibrateButton.Image = global::GST.Gearshift.Components.Properties.Resources.AnalogOutputsPanel_Calibration_64x64;
            this.calibrateButton.ImageDisabled = global::GST.Gearshift.Components.Properties.Resources.AnalogOutputsPanel_Calibration_64x64;
            this.calibrateButton.Location = new System.Drawing.Point(34, 342);
            this.calibrateButton.Name = "calibrateButton";
            this.calibrateButton.Size = new System.Drawing.Size(261, 64);
            this.calibrateButton.SupportTransparentBackground = false;
            this.calibrateButton.TabIndex = 32;
            this.calibrateButton.Text = "Calibrate the system for your dyno   ";
            this.calibrateButton.TextImageRelation = Soko.Common.Controls.TextRelation.OnRight;
            this.calibrateButton.TextImageSpacing = 0;
            this.calibrateButton.Click += new System.EventHandler(this.gearboxFileSaveButton_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(188, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "(Output 2)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AO2Trackbar
            // 
            this.AO2Trackbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.AO2Trackbar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.AO2Trackbar.BorderStyle = Soko.Common.Controls.NiceBorderStyle.Solid;
            this.AO2Trackbar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.AO2Trackbar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.AO2Trackbar.IndentHeight = 0;
            this.AO2Trackbar.IndentWidth = 2;
            this.AO2Trackbar.LargeChange = 5;
            this.AO2Trackbar.Location = new System.Drawing.Point(225, 76);
            this.AO2Trackbar.Maximum = 100;
            this.AO2Trackbar.Minimum = 0;
            this.AO2Trackbar.Name = "AO2Trackbar";
            this.AO2Trackbar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.AO2Trackbar.Padding = new System.Windows.Forms.Padding(5);
            this.AO2Trackbar.Size = new System.Drawing.Size(44, 213);
            this.AO2Trackbar.TabIndex = 15;
            this.AO2Trackbar.TickColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(146)))), ((int)(((byte)(148)))));
            this.AO2Trackbar.TickFrequency = 25;
            this.AO2Trackbar.TickHeight = 1;
            this.AO2Trackbar.TrackerColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(130)))), ((int)(((byte)(198)))));
            this.AO2Trackbar.TrackerSize = new System.Drawing.Size(16, 16);
            this.AO2Trackbar.TrackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(93)))), ((int)(((byte)(90)))));
            this.AO2Trackbar.TrackLineHeight = 3;
            this.AO2Trackbar.Value = 0;
            this.AO2Trackbar.ValueChanged += new Soko.Common.Controls.ValueChangedHandler(this.AO2Trackbar_ValueChanged);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(188, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Load current";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AO2PercValueLabel
            // 
            this.AO2PercValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.AO2PercValueLabel.ForeColor = System.Drawing.Color.Black;
            this.AO2PercValueLabel.Location = new System.Drawing.Point(188, 290);
            this.AO2PercValueLabel.Name = "AO2PercValueLabel";
            this.AO2PercValueLabel.Size = new System.Drawing.Size(120, 25);
            this.AO2PercValueLabel.TabIndex = 16;
            this.AO2PercValueLabel.Text = "0 %";
            this.AO2PercValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(24, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "(Output 1)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AO1Trackbar
            // 
            this.AO1Trackbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.AO1Trackbar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.AO1Trackbar.BorderStyle = Soko.Common.Controls.NiceBorderStyle.Solid;
            this.AO1Trackbar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.AO1Trackbar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.AO1Trackbar.IndentHeight = 0;
            this.AO1Trackbar.IndentWidth = 2;
            this.AO1Trackbar.LargeChange = 5;
            this.AO1Trackbar.Location = new System.Drawing.Point(61, 76);
            this.AO1Trackbar.Maximum = 100;
            this.AO1Trackbar.Minimum = 0;
            this.AO1Trackbar.Name = "AO1Trackbar";
            this.AO1Trackbar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.AO1Trackbar.Padding = new System.Windows.Forms.Padding(5);
            this.AO1Trackbar.Size = new System.Drawing.Size(44, 213);
            this.AO1Trackbar.TabIndex = 10;
            this.AO1Trackbar.TickColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(146)))), ((int)(((byte)(148)))));
            this.AO1Trackbar.TickFrequency = 25;
            this.AO1Trackbar.TickHeight = 1;
            this.AO1Trackbar.TrackerColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(130)))), ((int)(((byte)(198)))));
            this.AO1Trackbar.TrackerSize = new System.Drawing.Size(16, 16);
            this.AO1Trackbar.TrackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(93)))), ((int)(((byte)(90)))));
            this.AO1Trackbar.TrackLineHeight = 3;
            this.AO1Trackbar.Value = 0;
            this.AO1Trackbar.ValueChanged += new Soko.Common.Controls.ValueChangedHandler(this.AO1Trackbar_ValueChanged);
            // 
            // driveCaptionLabel
            // 
            this.driveCaptionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.driveCaptionLabel.ForeColor = System.Drawing.Color.Black;
            this.driveCaptionLabel.Location = new System.Drawing.Point(24, 40);
            this.driveCaptionLabel.Name = "driveCaptionLabel";
            this.driveCaptionLabel.Size = new System.Drawing.Size(120, 13);
            this.driveCaptionLabel.TabIndex = 13;
            this.driveCaptionLabel.Text = "Input motor RPM";
            this.driveCaptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // channelLabel
            // 
            this.channelLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.channelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.channelLabel.ForeColor = System.Drawing.Color.Black;
            this.channelLabel.Location = new System.Drawing.Point(0, 0);
            this.channelLabel.Name = "channelLabel";
            this.channelLabel.Size = new System.Drawing.Size(345, 36);
            this.channelLabel.TabIndex = 12;
            this.channelLabel.Text = "AO drive panel:";
            this.channelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AO1PercValueLabel
            // 
            this.AO1PercValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.AO1PercValueLabel.ForeColor = System.Drawing.Color.Black;
            this.AO1PercValueLabel.Location = new System.Drawing.Point(23, 290);
            this.AO1PercValueLabel.Name = "AO1PercValueLabel";
            this.AO1PercValueLabel.Size = new System.Drawing.Size(120, 25);
            this.AO1PercValueLabel.TabIndex = 11;
            this.AO1PercValueLabel.Text = "0 %";
            this.AO1PercValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AOsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 459);
            this.Controls.Add(this.mainPanel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AOsPanel";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Analog outputs panel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AOsPanel_FormClosing);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AO2NUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AO1NUD)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Soko.Common.Controls.NicePanel mainPanel;
        private System.Windows.Forms.Label label2;
        private Soko.Common.Controls.NiceTrackBar AO2Trackbar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label AO2PercValueLabel;
        private System.Windows.Forms.Label label1;
        private Soko.Common.Controls.NiceTrackBar AO1Trackbar;
        private System.Windows.Forms.Label driveCaptionLabel;
        private System.Windows.Forms.Label channelLabel;
        private System.Windows.Forms.Label AO1PercValueLabel;
        private Soko.Common.Controls.NiceButton calibrateButton;
        private System.Windows.Forms.Label AO2ValueLabel;
        private System.Windows.Forms.Label AO1ValueLabel;
        private System.Windows.Forms.Timer updateTmr;
        private System.Windows.Forms.NumericUpDown AO2NUD;
        private System.Windows.Forms.NumericUpDown AO1NUD;
    }
}