namespace GearShift
{
    partial class GearShiftEditorForm
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

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GearShiftEditorForm));
            this.testScriptEditor1 = new GST.Gearshift.Components.Forms.DAQ.TestScriptEditor();
            this.SuspendLayout();
            // 
            // testScriptEditor1
            // 
            this.testScriptEditor1.BackColor = System.Drawing.Color.Transparent;
            this.testScriptEditor1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.testScriptEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testScriptEditor1.Location = new System.Drawing.Point(3, 55);
            this.testScriptEditor1.Name = "testScriptEditor1";
            this.testScriptEditor1.Size = new System.Drawing.Size(1356, 821);
            this.testScriptEditor1.TabIndex = 2;
            // 
            // GearShiftEditorForm
            //             
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1362, 879);
            this.Controls.Add(this.testScriptEditor1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1020, 748);
            this.Name = "GearShiftEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Text = "GearShift Editor";
            this.ResumeLayout(false);

        }

        #endregion
        private GST.Gearshift.Components.Forms.DAQ.TestScriptEditor testScriptEditor1;
        //private  GST.Gearshift.Components.Forms.OBDPanel obdPanel;
    }
}

