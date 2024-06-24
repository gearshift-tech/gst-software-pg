using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace GST.Gearshift.Components.Interfaces.USB
{
  /// <summary>
  /// Contains a header of a script report file, used mainly to speed up report browsing in Report Explorer form
  /// </summary>
  public class TestReportHeader
  {

    #region Constants



    #endregion  Constants



    #region Private fields


    private DateTime mTimePerformed = DateTime.Now;

    private string mOperatorName = string.Empty;

    private string mGearboxSerialNumber = string.Empty;

    private string mTestName = string.Empty;

    private string mFilename = string.Empty;

    #endregion Private fields



    #region Constructors & finalizer



    #endregion Constructors & finalizer



    #region Events



    #endregion Events



    #region Properties

    /// <summary>
    /// Time the report was generated
    /// </summary>
    public DateTime TimePerformed
    {
      get { return mTimePerformed; }
      set {mTimePerformed = value;}
    }

    /// <summary>
    /// Operator name from the report
    /// </summary>
    public string OperatorName
    {
      get { return mOperatorName; }
      set { mOperatorName = value; }
    }

    /// <summary>
    /// Gearbox serial number from the report
    /// </summary>
    public string GearboxSerialNumber
    {
      get { return mGearboxSerialNumber; }
      set { mGearboxSerialNumber = value; }
    }

    /// <summary>
    /// Full path to the report
    /// </summary>
    public string Filename
    {
      get { return mFilename; }
      set { mFilename = value; }
    }

    /// <summary>
    /// Runned test name
    /// </summary>
    public string TestName
    {
      get { return mTestName; }
      set { mTestName = value; }
    }

    #endregion Properties



    #region Methods

    /// <summary>
    /// Loads the report header from specified file
    /// </summary>
    /// <param name="filename">File path</param>
    /// <returns>TestReportHeader object with content loaded from specified file</returns>
    static public TestReportHeader LoadFromFile( string filename )
    {
      if (Path.GetExtension(filename) == ".trf")
      {
        // If this is old, uncompressed format, we need to deserialize the whole file which is slow
        TestScriptReport tsr = TestScriptReport.OpenFile(filename);
        if (tsr != null)
        {
          TestReportHeader hdr = new TestReportHeader();
          hdr.Filename = filename;
          hdr.GearboxSerialNumber = tsr.GearboxSerialNumber;
          hdr.OperatorName = tsr.OperatorName;
          hdr.TestName = tsr.TestScriptRunned.Name;
          hdr.TimePerformed = tsr.TimePerformed;

          return hdr;
        }
        else
        {
          return null;
        }
      }
      else
      {
        // With new file format we can read the header alone
        using (BinaryReader rdr = new BinaryReader(File.Open(filename, FileMode.Open)))
        {
          TestReportHeader hdr = new TestReportHeader();
          hdr.Filename = filename;
          double fileversion = rdr.ReadDouble();
          //writer.Write(timePerformed.Ticks);
          hdr.TimePerformed = new DateTime(rdr.ReadInt64());
          //writer.Write(operatorName);
          hdr.OperatorName = rdr.ReadString();
          //writer.Write(gearboxSerialNumber);
          hdr.GearboxSerialNumber = rdr.ReadString();
          //writer.Write(testName);
          hdr.TestName = rdr.ReadString();

          return hdr;
        }
      }
    }
    #endregion Methods
  }
}
