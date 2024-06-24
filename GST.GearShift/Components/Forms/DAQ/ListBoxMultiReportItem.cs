using System;
using System.Collections.Generic;

using GST.Gearshift.Components.Interfaces.USB;

namespace Soko.Common.Common
{
  public class ListBoxMultiReportItem : Object
  {
    public List<TestReportHeader> mReportHeaders = new List<TestReportHeader>();
    public String mItemName = string.Empty;

    public ListBoxMultiReportItem()
    {
    }

    public ListBoxMultiReportItem(string itemName, TestReportHeader reportHeader)
    {
      mItemName = itemName;
      mReportHeaders.Add(reportHeader);
    }

    public override string ToString()
    {
      return mItemName;
    }
  }
}