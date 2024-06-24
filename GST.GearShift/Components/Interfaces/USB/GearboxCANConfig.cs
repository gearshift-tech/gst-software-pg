using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace GST.Gearshift.Components.Interfaces.USB
{
  [Serializable]
  public class GearboxCANConfig
  {
    #region Constants



    #endregion  Constants


    #region Private fields

    public CAN_GearboxSensorsConfig mSensorsConfig = new CAN_GearboxSensorsConfig();

    public UInt32 mCanBusBaud = 500000;

    public UInt32 mCycleTimeMs = 100;

    public bool mExtendedTypeMessaging = true;

    private string mFilename = string.Empty;

    #endregion Private fields


    #region Constructors & finalizer



    #endregion Constructors & finalizer


    #region Events



    #endregion Events


    #region Properties

    public string Filename
    {
      get
      {
        return mFilename;
      }
    }

    #endregion Properties


    #region Methods


    /// <summary>
    /// Loads the new class from XML file. This object is not changed !
    /// </summary>
    /// <param name="filename">File path</param>
    /// <returns>Object with content loaded from specified file</returns>
    public GearboxCANConfig OpenXml( string filename )
    {
      try
      {
        GearboxCANConfig ccf;
        using ( StreamReader myReader = new StreamReader( filename ) )
        {
          XmlSerializer myXML = new XmlSerializer( typeof( GearboxCANConfig ) );
          ccf = (GearboxCANConfig)myXML.Deserialize( myReader );
        }
        ccf.mFilename = Path.GetFileNameWithoutExtension(filename);
        return ccf;
      }
      catch (Exception e)
      {
        throw e;
      }
    }

    /// <summary>
    /// Saves this class to XML file
    /// </summary>
    /// <param name="filename">File path</param>
    public void SaveXml( string filename )
    {
      try
      {
        // write the class to the file  
        using ( StreamWriter myWriter = new StreamWriter( filename ) )
        {
          XmlSerializer myXML = new XmlSerializer( typeof( GearboxCANConfig ) );
          myXML.Serialize( myWriter, this );
          mFilename = Path.GetFileNameWithoutExtension( filename );
        }
      }
      catch (Exception e)
      {
        throw e;
      }
    }


    #endregion Methods




  }
}
