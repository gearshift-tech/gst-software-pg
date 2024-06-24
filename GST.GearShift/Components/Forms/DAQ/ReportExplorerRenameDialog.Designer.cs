namespace GST.Gearshift.Components.Forms.DAQ
{
    partial class ReportExplorerRenameDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.fileNameTextBox = new System.Windows.Forms.TextBox();
            this.renameButton = new Soko.Common.Controls.NiceButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(41, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(390, 22);
            this.label1.TabIndex = 41;
            this.label1.Text = "Enter the report new file name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fileNameTextBox
            // 
            this.fileNameTextBox.Location = new System.Drawing.Point(44, 76);
            this.fileNameTextBox.Name = "fileNameTextBox";
            this.fileNameTextBox.Size = new System.Drawing.Size(387, 20);
            this.fileNameTextBox.TabIndex = 42;
            // 
            // renameButton
            // 
            this.renameButton.AutoSize = true;
            this.renameButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.renameButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.renameButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.renameButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.renameButton.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.renameButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.renameButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.renameButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.renameButton.BorderWidth = 10;
            this.renameButton.ColorScheme = Soko.Common.Controls.ColorSchemeEnum.None;
            this.renameButton.ContentPadding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.renameButton.DrawBackColorOnFocus = false;
            this.renameButton.DrawBackgroundImage = false;
            this.renameButton.DrawBorderOnFocus = false;
            this.renameButton.DrawBorderOnTop = false;
            this.renameButton.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.renameButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.renameButton.Image = global::GST.Gearshift.Components.Properties.Resources.GearboxConfigPanel_Export_48;
            this.renameButton.ImageDisabled = global::GST.Gearshift.Components.Properties.Resources.GearboxConfigPanel_Export_48;
            this.renameButton.Location = new System.Drawing.Point(141, 102);
            this.renameButton.Name = "renameButton";
            this.renameButton.Size = new System.Drawing.Size(167, 52);
            this.renameButton.SupportTransparentBackground = false;
            this.renameButton.TabIndex = 43;
            this.renameButton.Text = "Rename report";
            this.renameButton.TextImageRelation = Soko.Common.Controls.TextRelation.OnRight;
            this.renameButton.TextImageSpacing = 1;
            this.renameButton.Click += new System.EventHandler(this.renameButton_Click);
            // 
            // ReportExplorerRenameDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 186);
            this.Controls.Add(this.renameButton);
            this.Controls.Add(this.fileNameTextBox);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReportExplorerRenameDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox fileNameTextBox;
        private Soko.Common.Controls.NiceButton renameButton;
    }
}