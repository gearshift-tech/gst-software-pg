using System;
using System.Collections.Generic;

namespace Soko.Common.Common
{
  public class ListBoxMultiFileItem : Object
  {
    public List<string> mFilePaths = new List<string>();
    public String mItemName = string.Empty;

    public ListBoxMultiFileItem()
    {
    }

    public ListBoxMultiFileItem(string itemName, string filePath)
    {
      mItemName = itemName;
      mFilePaths.Add(filePath);
    }

    public override string ToString()
    {
      return mItemName;
    }
  }
}