using System;
namespace Soko.Common.Common
{
  public class ListBoxFileItem : Object
  {
    public String mFilePath = string.Empty;
    public String mFileName = string.Empty;
    public String mStringTag = string.Empty;

    public ListBoxFileItem()
    {
    }

    public ListBoxFileItem(String filePath, String fileName)
    {
      mFileName = fileName;
      mFilePath = filePath;
    }

    public ListBoxFileItem(String filePath, String fileName, String stringTag)
    {
      mFileName = fileName;
      mFilePath = filePath;
      mStringTag = stringTag;
    }

    public override string ToString()
    {
      return mFileName;
    }
  }
}