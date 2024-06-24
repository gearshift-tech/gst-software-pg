using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GST.Gearshift.Components.Forms.CAN
{
    public partial class GaugeDataEditor : Form
    {
        public GaugeDataEditor(GST.Gearshift.Components.Interfaces.USB.CanEntry canEntry)
        {
            InitializeComponent();
            canEntryEditor1.CanEntry = canEntry;
        }
    }
}
