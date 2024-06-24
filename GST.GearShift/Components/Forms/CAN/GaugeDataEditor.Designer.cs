namespace GST.Gearshift.Components.Forms.CAN
{
    partial class GaugeDataEditor
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
            this.canEntryEditor1 = new GST.Gearshift.Components.Forms.CanEntryEditor();
            this.SuspendLayout();
            // 
            // canEntryEditor1
            // 
            this.canEntryEditor1.BackColor = System.Drawing.Color.Transparent;
            this.canEntryEditor1.Location = new System.Drawing.Point(7, 2);
            this.canEntryEditor1.Name = "canEntryEditor1";
            this.canEntryEditor1.Size = new System.Drawing.Size(411, 183);
            this.canEntryEditor1.TabIndex = 0;
            // 
            // GaugeDataEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(425, 186);
            this.Controls.Add(this.canEntryEditor1);
            this.ForeColor = System.Drawing.Color.Black;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GaugeDataEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CAN GAUGE EDITOR";
            this.ResumeLayout(false);

        }

        #endregion

        private GST.Gearshift.Components.Forms.CanEntryEditor canEntryEditor1;
        
    }
}