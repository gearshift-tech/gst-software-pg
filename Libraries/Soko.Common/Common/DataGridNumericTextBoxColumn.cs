using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Soko.Common.Common
{
  /// <remarks>
  /// This class implements DataGridView column containing DataGridNumericTextBoxCell cells
  /// </remarks>
  public class DataGridNumericTextBoxColumn : DataGridViewColumn
  {
    /// <summary>
    /// Default constructor
    /// </summary>
    public DataGridNumericTextBoxColumn()
    {
      //Set the cell template
      this.CellTemplate = new DataGridNumericTextboxCell();
    }
  }
}
