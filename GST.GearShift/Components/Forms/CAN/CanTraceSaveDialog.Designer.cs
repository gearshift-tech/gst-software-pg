namespace GST.Gearshift.Components.Forms.CAN
{
    partial class CanTraceSaveDialog
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
            
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CanTraceSaveDialog));
            
            this.label1 = new System.Windows.Forms.Label();
            this.filenameTextbox = new System.Windows.Forms.TextBox();
            this.cancelButton = new Soko.Common.Controls.NiceButton();
            this.saveButton = new Soko.Common.Controls.NiceButton();
            this.SuspendLayout();
            
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Gainsboro;
            this.label1.Location = new System.Drawing.Point(67, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter the name for the saved trace:";
            // 
            // filenameTextbox
            // 
            this.filenameTextbox.BackColor = System.Drawing.Color.Silver;
            this.filenameTextbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.filenameTextbox.ForeColor = System.Drawing.Color.Black;
            this.filenameTextbox.Location = new System.Drawing.Point(70, 74);
            this.filenameTextbox.Name = "filenameTextbox";
            this.filenameTextbox.Size = new System.Drawing.Size(280, 20);
            this.filenameTextbox.TabIndex = 1;
            // 
            // cancelButton
            // 
            this.cancelButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cancelButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cancelButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cancelButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cancelButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.cancelButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cancelButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cancelButton.BorderWidth = 1;
            this.cancelButton.ContentPadding = new System.Windows.Forms.Padding(1, 7, 0, 0);
            this.cancelButton.DrawBackColorOnFocus = false;
            this.cancelButton.DrawBackgroundImage = false;
            this.cancelButton.DrawBorderOnFocus = false;
            this.cancelButton.Image = GST.Gearshift.Components.Properties.Resources.ReportViewerPanel_button_cancel_48x48;
            this.cancelButton.ImageDisabled = GST.Gearshift.Components.Properties.Resources.ReportViewerPanel_button_cancel_48x48;
            this.cancelButton.Location = new System.Drawing.Point(288, 113);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(62, 62);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.TextImageRelation = Soko.Common.Controls.TextRelation.Underneath;
            this.cancelButton.TextImageSpacing = 0;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.saveButton.BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.saveButton.BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.saveButton.BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.saveButton.BackgroundImageLocation = new System.Drawing.Point(0, 0);
            this.saveButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.saveButton.BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.saveButton.BorderWidth = 1;
            this.saveButton.ContentPadding = new System.Windows.Forms.Padding(1, 7, 0, 0);
            this.saveButton.DrawBackColorOnFocus = false;
            this.saveButton.DrawBackgroundImage = false;
            this.saveButton.DrawBorderOnFocus = false;
            this.saveButton.Image = GST.Gearshift.Components.Properties.Resources.ReportViewerPanel_Save_48x48;
            this.saveButton.ImageDisabled = GST.Gearshift.Components.Properties.Resources.ReportViewerPanel_Save_48x48;
            this.saveButton.Location = new System.Drawing.Point(70, 113);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(62, 62);
            this.saveButton.TabIndex = 2;
            this.saveButton.TextImageRelation = Soko.Common.Controls.TextRelation.Underneath;
            this.saveButton.TextImageSpacing = 0;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // CanTraceSaveDialog
            // 
            
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(425, 186);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.filenameTextbox);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.Black;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CanTraceSaveDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        
        private Soko.Common.Controls.NiceButton cancelButton;
        private Soko.Common.Controls.NiceButton saveButton;
        private System.Windows.Forms.TextBox filenameTextbox;
        private System.Windows.Forms.Label label1;
    }
}