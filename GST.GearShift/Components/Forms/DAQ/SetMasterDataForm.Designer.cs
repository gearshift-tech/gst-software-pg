namespace GST.Gearshift.Components.Forms.DAQ
{
    partial class SetMasterDataForm
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
            this.TSMasterDataComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.saveToDiskCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();

            // 
            // TSMasterDataComboBox
            // 
            this.TSMasterDataComboBox.DropDownHeight = 400;
            this.TSMasterDataComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TSMasterDataComboBox.DropDownWidth = 250;
            this.TSMasterDataComboBox.FormattingEnabled = true;
            this.TSMasterDataComboBox.IntegralHeight = false;
            this.TSMasterDataComboBox.Location = new System.Drawing.Point(44, 77);
            this.TSMasterDataComboBox.Name = "TSMasterDataComboBox";
            this.TSMasterDataComboBox.Size = new System.Drawing.Size(387, 21);
            this.TSMasterDataComboBox.Sorted = true;
            this.TSMasterDataComboBox.TabIndex = 40;
            this.TSMasterDataComboBox.DropDown += new System.EventHandler(this.PopulateMDFilesComboBox);
            this.TSMasterDataComboBox.SelectedIndexChanged += new System.EventHandler(this.TSMasterDataComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(41, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(390, 22);
            this.label1.TabIndex = 41;
            this.label1.Text = "Choose the file to load the master data from:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // saveToDiskCheckBox
            // 
            this.saveToDiskCheckBox.AutoSize = true;
            this.saveToDiskCheckBox.Location = new System.Drawing.Point(106, 112);
            this.saveToDiskCheckBox.Name = "saveToDiskCheckBox";
            this.saveToDiskCheckBox.Size = new System.Drawing.Size(269, 17);
            this.saveToDiskCheckBox.TabIndex = 42;
            this.saveToDiskCheckBox.Text = "Permanently use the new master data (save to disk)";
            this.saveToDiskCheckBox.UseVisualStyleBackColor = true;
            // 
            // SetMasterDataForm
            // 
            
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 156);
            this.Controls.Add(this.saveToDiskCheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TSMasterDataComboBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetMasterDataForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Set new master data";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox TSMasterDataComboBox;
        private System.Windows.Forms.CheckBox saveToDiskCheckBox;
    }
}