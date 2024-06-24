using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace GST.Gearshift.Components.Interfaces.USB//GST.Gearshift.Components.Interfaces.USB
{

  /// <remarks>
  /// Class containing the CAN trace data
  /// </remarks>
  [Serializable]
  public class CANTrace
  {



    #region Constants



    #endregion  Constants



    #region Private fields

    private UsbCANData[] mTraceData = new UsbCANData[0];

    #endregion Private fields



    #region Constructors & finalizer



    #endregion Constructors & finalizer



    #region Events



    #endregion Events



    #region Properties

    public UsbCANData[] TraceData
    {
      get { return mTraceData; }
      set { mTraceData = value; }
    }

    #endregion Properties



    #region Methods

    /// <summary>
    /// Returns new object with data loaded from file.
    /// </summary>
    /// <param name="filename">File path</param>
    /// <returns>Object with content loaded from specified file</returns>
    public CANTrace OpenXml(string filename)
    {
      CANTrace ctr;
      using (StreamReader myReader = new StreamReader(filename))
      {
        XmlSerializer myXML = new XmlSerializer(typeof(CANTrace));
        ctr = (CANTrace)myXML.Deserialize(myReader);
      }
      return ctr;
    }

    /// <summary>
    /// Saves this object to XML file under specified path
    /// </summary>
    /// <param name="filename">File path</param>
    public void SaveXml(string filename)
    {
      // write the class to the file  
      using (StreamWriter myWriter = new StreamWriter(filename))
      {
        XmlSerializer myXML = new XmlSerializer(typeof(CANTrace));
        myXML.Serialize(myWriter, this);
      }
    }

    #endregion Methods



  }
}
