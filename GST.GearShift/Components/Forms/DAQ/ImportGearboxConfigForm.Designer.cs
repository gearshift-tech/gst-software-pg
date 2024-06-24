namespace GST.Gearshift.Components.Forms.DAQ
{
    partial class ImportGearboxConfigForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.gearboxFilesComboBox = new System.Windows.Forms.ComboBox();
            this.testScriptFilesComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(124, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "You have two options to choose from:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(70, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(243, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "1. Import gearbox config from exported file:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(70, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(295, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "2. Import the gearbox config from existing test script:";
            // 
            // gearboxFilesComboBox
            // 
            this.gearboxFilesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gearboxFilesComboBox.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gearboxFilesComboBox.FormattingEnabled = true;
            this.gearboxFilesComboBox.Location = new System.Drawing.Point(67, 118);
            this.gearboxFilesComboBox.Name = "gearboxFilesComboBox";
            this.gearboxFilesComboBox.Size = new System.Drawing.Size(348, 23);
            this.gearboxFilesComboBox.Sorted = true;
            this.gearboxFilesComboBox.TabIndex = 34;
            this.gearboxFilesComboBox.DropDown += new System.EventHandler(this.gearboxFilesComboBox_DropDown);
            this.gearboxFilesComboBox.SelectedValueChanged += new System.EventHandler(this.gearboxFilesComboBox_SelectedValueChanged);
            // 
            // testScriptFilesComboBox
            // 
            this.testScriptFilesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.testScriptFilesComboBox.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.testScriptFilesComboBox.FormattingEnabled = true;
            this.testScriptFilesComboBox.Location = new System.Drawing.Point(67, 171);
            this.testScriptFilesComboBox.Name = "testScriptFilesComboBox";
            this.testScriptFilesComboBox.Size = new System.Drawing.Size(348, 23);
            this.testScriptFilesComboBox.Sorted = true;
            this.testScriptFilesComboBox.TabIndex = 35;
            this.testScriptFilesComboBox.DropDown += new System.EventHandler(this.testScriptFilesComboBox_DropDown);
            this.testScriptFilesComboBox.SelectedValueChanged += new System.EventHandler(this.testScriptFilesComboBox_SelectedValueChanged);
            // 
            // ImportGearboxConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 244);
            this.Controls.Add(this.testScriptFilesComboBox);
            this.Controls.Add(this.gearboxFilesComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportGearboxConfigForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import gearbox configuration from disk";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImportGearboxConfigForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox testScriptFilesComboBox;
        private System.Windows.Forms.ComboBox gearboxFilesComboBox;
    }
}