namespace Soko.Common.Forms
{
    partial class MessageBox<ReturnType>
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if ( disposing && (components != null) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		private void InitializeComponent()
		{
      this.moreInfoLabel = new System.Windows.Forms.LinkLabel();
      this.tpnlMainMessage = new System.Windows.Forms.TableLayoutPanel();
      this.pbImage = new System.Windows.Forms.PictureBox();
      this.lblMessage = new System.Windows.Forms.Label();
      this.lblHeader = new System.Windows.Forms.Label();
      this.buttonsPanel = new Soko.Common.Controls.NicePanel();
      this.tpnlMainMessage.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
      this.SuspendLayout();
      // 
      // moreInfoLabel
      // 
      this.moreInfoLabel.BackColor = System.Drawing.Color.Transparent;
      this.moreInfoLabel.Dock = System.Windows.Forms.DockStyle.Left;
      this.moreInfoLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.moreInfoLabel.Location = new System.Drawing.Point(0, 0);
      this.moreInfoLabel.Name = "moreInfoLabel";
      this.moreInfoLabel.Size = new System.Drawing.Size(53, 44);
      this.moreInfoLabel.TabIndex = 6;
      this.moreInfoLabel.TabStop = true;
      this.moreInfoLabel.Text = "Details";
      this.moreInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.moreInfoLabel.Visible = false;
      this.moreInfoLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreInfoLabel_LinkClicked);
      // 
      // tpnlMainMessage
      // 
      this.tpnlMainMessage.ColumnCount = 2;
      this.tpnlMainMessage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
      this.tpnlMainMessage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tpnlMainMessage.Controls.Add(this.pbImage, 0, 0);
      this.tpnlMainMessage.Controls.Add(this.lblMessage, 1, 1);
      this.tpnlMainMessage.Controls.Add(this.lblHeader, 1, 0);
      this.tpnlMainMessage.Dock = System.Windows.Forms.DockStyle.Top;
      this.tpnlMainMessage.Location = new System.Drawing.Point(0, 0);
      this.tpnlMainMessage.Name = "tpnlMainMessage";
      this.tpnlMainMessage.Padding = new System.Windows.Forms.Padding(6, 6, 0, 0);
      this.tpnlMainMessage.RowCount = 2;
      this.tpnlMainMessage.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tpnlMainMessage.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tpnlMainMessage.Size = new System.Drawing.Size(471, 151);
      this.tpnlMainMessage.TabIndex = 6;
      // 
      // pbImage
      // 
      this.pbImage.Image = global::Soko.Common.Properties.Resources.MessageBox_critical;
      this.pbImage.Location = new System.Drawing.Point(9, 9);
      this.pbImage.Name = "pbImage";
      this.pbImage.Size = new System.Drawing.Size(36, 36);
      this.pbImage.TabIndex = 2;
      this.pbImage.TabStop = false;
      // 
      // lblMessage
      // 
      this.lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this.lblMessage.BackColor = System.Drawing.Color.Transparent;
      this.lblMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
      this.lblMessage.Location = new System.Drawing.Point(54, 51);
      this.lblMessage.Margin = new System.Windows.Forms.Padding(3);
      this.lblMessage.Name = "lblMessage";
      this.lblMessage.Size = new System.Drawing.Size(414, 96);
      this.lblMessage.TabIndex = 0;
      this.lblMessage.Text = "[message]";
      this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lblHeader
      // 
      this.lblHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this.lblHeader.AutoSize = true;
      this.lblHeader.BackColor = System.Drawing.Color.Transparent;
      this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
      this.lblHeader.Location = new System.Drawing.Point(54, 17);
      this.lblHeader.Name = "lblHeader";
      this.lblHeader.Size = new System.Drawing.Size(414, 20);
      this.lblHeader.TabIndex = 1;
      this.lblHeader.Text = "[header]";
      // 
      // buttonsPanel
      // 
      this.buttonsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonsPanel.AutoplaceElements = true;
      this.buttonsPanel.AutoScrollHorizontalMaximum = 100;
      this.buttonsPanel.AutoScrollHorizontalMinimum = 0;
      this.buttonsPanel.AutoScrollHPos = 0;
      this.buttonsPanel.AutoScrollVerticalMaximum = 100;
      this.buttonsPanel.AutoScrollVerticalMinimum = 0;
      this.buttonsPanel.AutoScrollVPos = 0;
      this.buttonsPanel.AutoSizeElements = false;
      this.buttonsPanel.backgroundColor1 = System.Drawing.Color.WhiteSmoke;
      this.buttonsPanel.backgroundColor2 = System.Drawing.Color.Wheat;
      this.buttonsPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
      this.buttonsPanel.BorderWidth = 0;
      this.buttonsPanel.DrawBackImage = false;
      this.buttonsPanel.EnableAutoScrollHorizontal = false;
      this.buttonsPanel.EnableAutoScrollVertical = false;
      this.buttonsPanel.Gradient = Soko.Common.Controls.NicePanel.GradientType.None;
      this.buttonsPanel.HorizontalMargin = 0;
      this.buttonsPanel.Location = new System.Drawing.Point(0, 157);
      this.buttonsPanel.Name = "buttonsPanel";
      this.buttonsPanel.roundingRadius = 10;
      this.buttonsPanel.Size = new System.Drawing.Size(471, 89);
      this.buttonsPanel.SupportTransparentBackground = false;
      this.buttonsPanel.TabIndex = 7;
      this.buttonsPanel.VerticalMargin = 0;
      this.buttonsPanel.VisibleAutoScrollHorizontal = false;
      this.buttonsPanel.VisibleAutoScrollVertical = false;
      // 
      // MessageBox
      // 
      this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
      this.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(84)))), ((int)(((byte)(144)))));
      this.BorderThickness = 3;
      this.CaptionBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
      this.CaptionBarHeight = 20;
      this.CaptionButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(168)))));
      this.CaptionButtonHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
      this.CaptionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(168)))));
      this.ClientSize = new System.Drawing.Size(471, 247);
      this.Controls.Add(this.buttonsPanel);
      this.Controls.Add(this.tpnlMainMessage);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(350, 220);
      this.Name = "MessageBox";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Shown += new System.EventHandler(this.MessageBox_Shown);
      this.tpnlMainMessage.ResumeLayout(false);
      this.tpnlMainMessage.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
      this.ResumeLayout(false);

		}

		#endregion
        private System.Windows.Forms.TableLayoutPanel tpnlMainMessage;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.LinkLabel moreInfoLabel;
    private Controls.NicePanel buttonsPanel;
  }
}