namespace GST.ZF6.Components.Forms
{
  partial class Zf6InitPanel
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.InitbgWorker = new System.ComponentModel.BackgroundWorker();
            this.mainPanel = new Soko.Common.Controls.NicePanel();
            this.cancelButton = new Soko.Common.Controls.NiceButton();
            this.continueButton = new Soko.Common.Controls.NiceButton();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.readyToGoLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.gbxInitLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.gbxTypeLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.usbConnLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bgPanel = new Soko.Common.Controls.NicePanel();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.bgPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // InitbgWorker
            // 
            this.InitbgWorker.WorkerSupportsCancellation = true;
            this.InitbgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.InitbgWorker_DoWork);
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
            this.mainPanel.backgroundColor1 = System.Drawing.Color.WhiteSmoke;
            this.mainPanel.backgroundColor2 = System.Drawing.Color.Wheat;
            this.mainPanel.BorderColor = System.Drawing.Color.White;
            this.mainPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mainPanel.BorderWidth = 1;
            this.mainPanel.Controls.Add(this.cancelButton);
            this.mainPanel.Controls.Add(this.continueButton);
            this.mainPanel.Controls.Add(this.pictureBox4);
            this.mainPanel.Controls.Add(this.pictureBox3);
            this.mainPanel.Controls.Add(this.pictureBox2);
            this.mainPanel.Controls.Add(this.pictureBox1);
            this.mainPanel.Controls.Add(this.readyToGoLabel);
            this.mainPanel.Controls.Add(this.label4);
            this.mainPanel.Controls.Add(this.gbxInitLabel);
            this.mainPanel.Controls.Add(this.label3);
            this.mainPanel.Controls.Add(this.gbxTypeLabel);
            this.mainPanel.Controls.Add(this.label2);
            this.mainPanel.Controls.Add(this.usbConnLabel);
            this.mainPanel.Controls.Add(this.label1);
            this.mainPanel.DrawBackImage = false;
            this.mainPanel.EnableAutoScrollHorizontal = false;
            this.mainPanel.EnableAutoScrollVertical = false;
            this.mainPanel.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.mainPanel.Gradient = Soko.Common.Controls.NicePanel.GradientType.None;
            this.mainPanel.HorizontalMargin = 0;
            this.mainPanel.Location = new System.Drawing.Point(282, 35);
            this.mainPanel.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.roundingRadius = 0;
            this.mainPanel.Size = new System.Drawing.Size(1460, 872);
            this.mainPanel.SupportTransparentBackground = false;
            this.mainPanel.TabIndex = 0;
            this.mainPanel.VerticalMargin = 0;
            this.mainPanel.VisibleAutoScrollHorizontal = false;
            this.mainPanel.VisibleAutoScrollVertical = false;
            // 
            // cancelButton
            // 
            this.cancelButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cancelButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cancelButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cancelButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cancelButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cancelButton.BackgroundColor = System.Drawing.Color.DimGray;
            this.cancelButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.cancelButton.BorderColor = System.Drawing.Color.White;
            this.cancelButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cancelButton.BorderWidth = 1;
            this.cancelButton.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.cancelButton.ContentPadding = new System.Windows.Forms.Padding(10, 11, 10, 10);
            this.cancelButton.DrawBackColorOnFocus = true;
            this.cancelButton.DrawBackgroundImage = false;
            this.cancelButton.DrawBorderOnFocus = true;
            this.cancelButton.DrawBorderOnTop = false;
            this.cancelButton.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cancelButton.Image = global::GST.ZF6.Components.Properties.Resources.Cancel_64x64;
            this.cancelButton.ImageDisabled = global::GST.ZF6.Components.Properties.Resources.Cancel_64x64;
            this.cancelButton.Location = new System.Drawing.Point(115, 570);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(224, 243);
            this.cancelButton.SupportTransparentBackground = false;
            this.cancelButton.TabIndex = 13;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.TextImageRelation = Soko.Common.Controls.TextRelation.Underneath;
            this.cancelButton.TextImageSpacing = 7;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // continueButton
            // 
            this.continueButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.continueButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.continueButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.continueButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.continueButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.continueButton.BackgroundColor = System.Drawing.Color.DimGray;
            this.continueButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.continueButton.BorderColor = System.Drawing.Color.White;
            this.continueButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.continueButton.BorderWidth = 1;
            this.continueButton.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.continueButton.ContentPadding = new System.Windows.Forms.Padding(10, 11, 10, 10);
            this.continueButton.DrawBackColorOnFocus = true;
            this.continueButton.DrawBackgroundImage = false;
            this.continueButton.DrawBorderOnFocus = true;
            this.continueButton.DrawBorderOnTop = false;
            this.continueButton.Enabled = false;
            this.continueButton.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.continueButton.Image = global::GST.ZF6.Components.Properties.Resources.Continue_64x64;
            this.continueButton.ImageDisabled = global::GST.ZF6.Components.Properties.Resources.Continue_BW_64x64;
            this.continueButton.Location = new System.Drawing.Point(1173, 570);
            this.continueButton.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(224, 243);
            this.continueButton.SupportTransparentBackground = false;
            this.continueButton.TabIndex = 13;
            this.continueButton.Text = "Continue";
            this.continueButton.TextImageRelation = Soko.Common.Controls.TextRelation.Underneath;
            this.continueButton.TextImageSpacing = 7;
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::GST.ZF6.Components.Properties.Resources.CrackCode_48x48;
            this.pictureBox4.Location = new System.Drawing.Point(115, 403);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(128, 114);
            this.pictureBox4.TabIndex = 11;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::GST.ZF6.Components.Properties.Resources.Waiting_48x48;
            this.pictureBox3.Location = new System.Drawing.Point(115, 274);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(128, 114);
            this.pictureBox3.TabIndex = 11;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::GST.ZF6.Components.Properties.Resources.Configure_48x48;
            this.pictureBox2.Location = new System.Drawing.Point(115, 162);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(128, 114);
            this.pictureBox2.TabIndex = 11;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::GST.ZF6.Components.Properties.Resources.Usb_48x48;
            this.pictureBox1.Location = new System.Drawing.Point(115, 33);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 114);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // readyToGoLabel
            // 
            this.readyToGoLabel.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.readyToGoLabel.Location = new System.Drawing.Point(920, 448);
            this.readyToGoLabel.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.readyToGoLabel.Name = "readyToGoLabel";
            this.readyToGoLabel.Size = new System.Drawing.Size(491, 45);
            this.readyToGoLabel.TabIndex = 10;
            this.readyToGoLabel.Text = "label1";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(259, 448);
            this.label4.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(645, 45);
            this.label4.TabIndex = 10;
            this.label4.Text = "Negotiating with host TCU :";
            // 
            // gbxInitLabel
            // 
            this.gbxInitLabel.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gbxInitLabel.Location = new System.Drawing.Point(920, 315);
            this.gbxInitLabel.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.gbxInitLabel.Name = "gbxInitLabel";
            this.gbxInitLabel.Size = new System.Drawing.Size(477, 45);
            this.gbxInitLabel.TabIndex = 10;
            this.gbxInitLabel.Text = "label1";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(259, 320);
            this.label3.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(645, 45);
            this.label3.TabIndex = 10;
            this.label3.Text = "Waiting for gearbox init :";
            // 
            // gbxTypeLabel
            // 
            this.gbxTypeLabel.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gbxTypeLabel.Location = new System.Drawing.Point(920, 203);
            this.gbxTypeLabel.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.gbxTypeLabel.Name = "gbxTypeLabel";
            this.gbxTypeLabel.Size = new System.Drawing.Size(477, 45);
            this.gbxTypeLabel.TabIndex = 10;
            this.gbxTypeLabel.Text = "label1";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(259, 207);
            this.label2.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(645, 45);
            this.label2.TabIndex = 10;
            this.label2.Text = "Selecting gearbox type :";
            // 
            // usbConnLabel
            // 
            this.usbConnLabel.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.usbConnLabel.Location = new System.Drawing.Point(920, 74);
            this.usbConnLabel.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.usbConnLabel.Name = "usbConnLabel";
            this.usbConnLabel.Size = new System.Drawing.Size(477, 45);
            this.usbConnLabel.TabIndex = 10;
            this.usbConnLabel.Text = "label1";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(259, 79);
            this.label1.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(645, 45);
            this.label1.TabIndex = 10;
            this.label1.Text = "Checking Zf6 USB connection :";
            // 
            // bgPanel
            // 
            this.bgPanel.AutoplaceElements = true;
            this.bgPanel.AutoScrollHorizontalMaximum = 100;
            this.bgPanel.AutoScrollHorizontalMinimum = 0;
            this.bgPanel.AutoScrollHPos = 0;
            this.bgPanel.AutoScrollVerticalMaximum = 100;
            this.bgPanel.AutoScrollVerticalMinimum = 0;
            this.bgPanel.AutoScrollVPos = 0;
            this.bgPanel.AutoSizeElements = false;
            this.bgPanel.BackColor = System.Drawing.Color.DimGray;
            this.bgPanel.backgroundColor1 = System.Drawing.Color.WhiteSmoke;
            this.bgPanel.backgroundColor2 = System.Drawing.Color.Wheat;
            this.bgPanel.BorderColor = System.Drawing.Color.DimGray;
            this.bgPanel.BorderWidth = 1;
            this.bgPanel.Controls.Add(this.mainPanel);
            this.bgPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bgPanel.DrawBackImage = false;
            this.bgPanel.EnableAutoScrollHorizontal = false;
            this.bgPanel.EnableAutoScrollVertical = false;
            this.bgPanel.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.bgPanel.Gradient = Soko.Common.Controls.NicePanel.GradientType.None;
            this.bgPanel.HorizontalMargin = 0;
            this.bgPanel.Location = new System.Drawing.Point(0, 0);
            this.bgPanel.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.bgPanel.Name = "bgPanel";
            this.bgPanel.roundingRadius = 10;
            this.bgPanel.Size = new System.Drawing.Size(1912, 975);
            this.bgPanel.SupportTransparentBackground = false;
            this.bgPanel.TabIndex = 0;
            this.bgPanel.VerticalMargin = 0;
            this.bgPanel.VisibleAutoScrollHorizontal = false;
            this.bgPanel.VisibleAutoScrollVertical = false;
            // 
            // Zf6InitPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.bgPanel);
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.Name = "Zf6InitPanel";
            this.Size = new System.Drawing.Size(1912, 975);
            this.mainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.bgPanel.ResumeLayout(false);
            this.ResumeLayout(false);

    }

    #endregion
    private System.ComponentModel.BackgroundWorker InitbgWorker;
        private Soko.Common.Controls.NicePanel mainPanel;
        private Soko.Common.Controls.NiceButton cancelButton;
        private Soko.Common.Controls.NiceButton continueButton;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label readyToGoLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label gbxInitLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label gbxTypeLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label usbConnLabel;
        private System.Windows.Forms.Label label1;
        private Soko.Common.Controls.NicePanel bgPanel;
    }
}
