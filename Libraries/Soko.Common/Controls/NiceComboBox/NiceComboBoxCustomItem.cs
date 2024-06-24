using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soko.Common.Controls
{
  public class NiceComboBoxCustomItem : Object
  {
    public String DisplayedName = String.Empty;
    public Object ObjectToStore = new Object();

    public override string ToString()
    {
      return this.DisplayedName;
    }
  }
}
