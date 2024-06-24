using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace Soko.Common.Common
{
  public class DTC_Database
  {
    List<string> _DtcCodeList = new List<string>();
    List<string> _DtcDescList = new List<string>();

    //TextReader TR;
    public DTC_Database ()
    {
      var assembly = Assembly.GetExecutingAssembly();
      var resourceName = "DTC_GM.txt";
      var bullshit = assembly.GetManifestResourceStream(resourceName);

      TextReader TR = new StreamReader( new MemoryStream(Encoding.UTF8.GetBytes(Soko.Common.Properties.Resources.DTC_GM)));

      
      string line = "";
      while ((line = TR.ReadLine()) != null)
      {
        char[] dtcCodeBytes = new char[5];
        line.CopyTo(0, dtcCodeBytes, 0, 5);
        char[] dtcDescBytes = new char[line.Length - 7];
        line.CopyTo(7, dtcDescBytes, 0, line.Length - 7);

        _DtcCodeList.Add(new string(dtcCodeBytes));
        _DtcDescList.Add(new string(dtcDescBytes));
      }

    }

    public string GetDtcDescription(string DTC)
    {

      int index = _DtcCodeList.FindIndex(delegate (string s) { return s == DTC; });

      if (index != -1)
        return _DtcDescList[index];
      else
        return "";
    }


  }


}
