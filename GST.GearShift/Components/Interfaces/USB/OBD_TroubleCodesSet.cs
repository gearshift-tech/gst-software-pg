using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace GST.Gearshift.Components.Interfaces.USB
{
  //[Serializable()]
  public class OBD_TroubleCodesSet
  {
    private List<OBD_TroubleCode> mLibraryCodes = new List<OBD_TroubleCode>( 0 );

    private List<string> mLibNames = new List<string>( 0 );

    public List<OBD_TroubleCode> mStoredCodes = new List<OBD_TroubleCode>( 0 );

    public List<OBD_TroubleCode> mPendingCodes = new List<OBD_TroubleCode>( 0 );

    public OBD_TroubleCode mFreezeFrameCode = new OBD_TroubleCode();

    public void LoadDefinitionsFile( string filename )
    {
      TextReader tr = File.OpenText( filename );
      mLibNames.Add( Path.GetFileNameWithoutExtension( filename ) );
      int currLibRef = mLibNames.Count - 1;
      string line = string.Empty;
      while ( !( (StreamReader)tr).EndOfStream )
      {
        line = tr.ReadLine();
        if (line.Length < 6)
        {
          continue;
        }
        OBD_TroubleCode tc = new OBD_TroubleCode();
        // Assign library reference number (to know from which file this code comes from)
        tc.mLibraryRefNumber = currLibRef;
        // Assign code type value
        switch ( line[0] )
        {
          case 'P':
            {
              tc.mType = 0x00;
              break;
            }
          case 'C':
            {
              tc.mType = 0x01;
              break;
            }
          case 'B':
            {
              tc.mType = 0x02;
              break;
            }
          case 'U':
            {
              tc.mType = 0x03;
              break;
            }
        }
        //tc.mType = line[0];
        // Assign code number value
        string numStr = line.Substring( 1, 4 );
        tc.mNumber = Convert.ToUInt32( numStr );
        // Assign code definition
        if ( line[5] == '.' )
        {
          tc.mDefinition = line.Remove( 0, 9 );
        }
        else
        {
          if ( line[5] == ' ' )
          {
            tc.mDefinition = line.Remove( 0, 6 );
          }
        }

        mLibraryCodes.Add( tc );
      }
    }

    public void LoadDirectory( string dir)
    {
        string[] filenames = Directory.GetFiles(dir);
        foreach (string filename in filenames)
        {
          LoadDefinitionsFile(filename);
        }
    }

    public void AddStoredCode( byte b1, byte b2 )
    {
      if ( ( b1 == 0 ) && ( b2 == 0 ) )
        return; // this is invalid TC
      byte type = (byte)(b1 >> 6);
      UInt32 number = (UInt32)( ( b1 >> 4 ) & 0x03 ) * 1000;
      number += (UInt32)( b1 & 0x0F ) * 100;
      number += (UInt32)( ( b2 >> 4 ) & 0x0F ) * 10;
      number += (UInt32)( b2 & 0x0F );
      int addedCodes = 0;

      for (int i = 0; i < mLibraryCodes.Count; i++)
      {
        if (mLibraryCodes[i].mType == type)
        {
          if (mLibraryCodes[i].mNumber == number)
          {
            addedCodes++;
            mStoredCodes.Add( mLibraryCodes[i] );
          }
        }
      }
      if (addedCodes == 0)
      {
        OBD_TroubleCode tc = new OBD_TroubleCode();
        tc.mType = type;
        tc.mNumber = number;
        tc.mDefinition = "Unknown code";
        tc.mLibraryRefNumber = -1;
        mStoredCodes.Add( tc );
      }
    }

    public void AddPendingCode( byte b1, byte b2 )
    {
      if ( ( b1 == 0 ) && ( b2 == 0 ) )
        return; // this is invalid TC
      byte type = (byte)( b1 >> 6 );
      UInt32 number = (UInt32)( ( b1 >> 4 ) & 0x03 ) * 1000;
      number += (UInt32)( b1 & 0x0F ) * 100;
      number += (UInt32)( ( b2 >> 4 ) & 0x0F ) * 10;
      number += (UInt32)( b2 & 0x0F );
      int addedCodes = 0;

      for ( int i = 0; i < mLibraryCodes.Count; i++ )
      {
        if ( mLibraryCodes[i].mType == type )
        {
          if ( mLibraryCodes[i].mNumber == number )
          {
            addedCodes++;
            mPendingCodes.Add( mLibraryCodes[i] );
          }
        }
      }
      if ( addedCodes == 0 )
      {
        OBD_TroubleCode tc = new OBD_TroubleCode();
        tc.mType = type;
        tc.mNumber = number;
        tc.mDefinition = "Unknown code";
        tc.mLibraryRefNumber = -1;
        mPendingCodes.Add( tc );
      }
    }

    public void SetFreezeFrameCode( byte b1, byte b2 )
    {
      if ( ( b1 == 0 ) && ( b2 == 0 ) )
        return; // this is invalid TC
      byte type = (byte)( b1 >> 6 );
      UInt32 number = (UInt32)( ( b1 >> 4 ) & 0x03 ) * 1000;
      number += (UInt32)( b1 & 0x0F ) * 100;
      number += (UInt32)( ( b2 >> 4 ) & 0x0F ) * 10;
      number += (UInt32)( b2 & 0x0F );
      int addedCodes = 0;

      for ( int i = 0; i < mLibraryCodes.Count; i++ )
      {
        if ( mLibraryCodes[i].mType == type )
        {
          if ( mLibraryCodes[i].mNumber == number )
          {
            addedCodes++;
            mFreezeFrameCode = mLibraryCodes[i];
          }
        }
      }
      if ( addedCodes == 0 )
      {
        OBD_TroubleCode tc = new OBD_TroubleCode();
        tc.mType = type;
        tc.mNumber = number;
        tc.mDefinition = "Unknown code";
        tc.mLibraryRefNumber = -1;
        mFreezeFrameCode = tc;
      }
    }

  }
}
