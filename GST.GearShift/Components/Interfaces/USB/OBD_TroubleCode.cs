using System;
using System.Collections.Generic;
using System.Text;

namespace GST.Gearshift.Components.Interfaces.USB
{
  public class OBD_TroubleCode
  {
    public enum CodeType :int
    {
      Powertrain = 0x00,
      Body       = 0x01, 
      Chassis    = 0x02, 
      Undefined  = 0x03
    };
    public int mType                    = 3;
    public UInt32 mNumber               = 0;
    public string mDefinition           = string.Empty;
    public int mLibraryRefNumber        = 0;

    public override string  ToString()
    {
      string str = string.Empty;
      switch ( mType )
      {
        case 0x00:
          {
            str = "P";
            break;
          }
        case 0x01:
          {
            str = "C";
            break;
          }
        case 0x02:
          {
            str = "B";
            break;
          }
        case 0x03:
          {
            str = "U";
            break;
          }
      }
      str += mNumber.ToString( "0000" );
      return str;
    }
  }
}
