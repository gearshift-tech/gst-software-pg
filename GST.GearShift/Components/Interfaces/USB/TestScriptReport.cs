using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace GST.Gearshift.Components.Interfaces.USB
{
  [Serializable]
  public class TestScriptReport
  {

    [Serializable]
    public class LivePlaybackFrame
    {
      public int TestScriptFrameIndex = -1;
      public float[] RecordedData = new float[14];

      public LivePlaybackFrame(int FrameIndex, float[] AIData)
      {
        TestScriptFrameIndex = FrameIndex;
        RecordedData = AIData;
      }
      public LivePlaybackFrame()
      {
      }
    }


    #region Constants



    #endregion  Constants



    #region Private fields

    private TestScript mTestScriptRunned = new TestScript();

    private DateTime mTimePerformed = DateTime.Now;

    private string mOperatorName = string.Empty;

    private string mGearboxSerialNumber = string.Empty;

    private string mWorkOrderNumber = string.Empty;

    private string mComment = string.Empty;

    private string mFilename = string.Empty;

    private List<LivePlaybackFrame> _livePlaybackData = new List<LivePlaybackFrame>();

    #endregion Private fields



    #region Constructors & finalizer



    #endregion Constructors & finalizer



    #region Events



    #endregion Events



    #region Properties

    public TestScript TestScriptRunned
    {
      get { return mTestScriptRunned; }
      set { mTestScriptRunned = value; }
    }

    public DateTime TimePerformed
    {
      get { return mTimePerformed; }
      set {mTimePerformed = value;}
    }

    public string Comment
    {
      get { return mComment; }
      set { mComment = value; }
    }

    public string OperatorName
    {
      get { return mOperatorName; }
      set { mOperatorName = value; }
    }

    public string GearboxSerialNumber
    {
      get { return mGearboxSerialNumber; }
      set { mGearboxSerialNumber = value; }
    }

    public string WorkOrderNumber
    {
      get { return mWorkOrderNumber; }
      set { mWorkOrderNumber = value;}
    }

    public string Filename
    {
      get { return mFilename; }
    }

    /// <summary>
    /// Gets Live playback data list
    /// Must be ignored in XML serialization, LivePlaybackDataArray does the job
    /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public List<LivePlaybackFrame> LivePlaybackData
    {
      get { return _livePlaybackData; }
    }

    private bool _saveLivePlaybackData = true;
    public bool SaveLivePlaybackData
    {
      get { return _saveLivePlaybackData; }
      set { _saveLivePlaybackData = value; }
    }

    #endregion Properties



    #region Methods


    public void SaveToFile(string filename)
    {
      // New file path with new extension
      mFilename = Path.Combine(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename)) + ".ctrf";
      // Prepare the binary header data
      double fileFormatVersion = 5.0;
      DateTime timePerformed = this.TimePerformed;
      string operatorName = this.OperatorName;
      string gearboxSerialNumber = this.GearboxSerialNumber;
      string testName = this.TestScriptRunned.Name;

      
      using (FileStream ostream = new FileStream(mFilename, FileMode.Create))
      {
        // Write the binary header data
        BinaryWriter writer = new BinaryWriter(ostream);
        writer.Write(fileFormatVersion);
        writer.Write(timePerformed.Ticks);
        writer.Write(operatorName);
        writer.Write(gearboxSerialNumber);
        writer.Write(testName);

        // Create ZIP file, add serialized XML and add it to file stream
        using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
        {
          Stream xmlContent = new MemoryStream();
          Stream zipStream = new MemoryStream();
          StreamWriter sw = new StreamWriter(xmlContent);
          this.SaveXml(sw);
          xmlContent.Position = 0;
          zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
          zip.AddEntry("XML", xmlContent);
          zip.Save(zipStream);   
          // Dotnetzip is a piece of shit, it must be saved to a separate stream and then to file.
          zipStream.Position = 0;
          byte[] buffer = new byte[32768];
          while (true)
          {
            int read = zipStream.Read(buffer, 0, buffer.Length);
            if (read <= 0) break;
            ostream.Write(buffer, 0, read);
          }
        }
        ostream.Close();
      }

    }

    /// <summary>
    /// Opens file on the disk and returns TestScriptReport object
    /// </summary>
    /// <param name="filename">File path</param>
    /// <returns>Object with content loaded from specified file</returns>
    static public TestScriptReport OpenFile( string filename )
    {
      if (Path.GetExtension(filename) == ".trf")
      {
        // If this is an old uncompressed file, open it and save so it's on the disk in the new format
        TestScriptReport tsr;
        tsr = TestScriptReport.OpenXml(filename);
        tsr.SaveToFile(filename);
        // Then add the file to zip archive and chunk it into the backup folder
        using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
        {
          zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
          zip.AddFile(filename, "/");
          if (!Directory.Exists(Components.Utilities.Settings.ReportsDirectory + "\\Backup"))
          {
            Directory.CreateDirectory(Components.Utilities.Settings.ReportsDirectory + "\\Backup");
          }
          string zipfilename = Components.Utilities.Settings.ReportsDirectory + "\\Backup\\" + Path.GetFileNameWithoutExtension(filename) + ".zip";
          zip.Save(zipfilename);
        }
        // Remove the old format file from the disk
        File.Delete(filename);

        return tsr;
      }
      else
      {
        using (FileStream istream = new FileStream(filename, FileMode.Open))
        {
          // Test cript report to be returned
          TestScriptReport tsr = new TestScriptReport();
          BinaryReader rdr = new BinaryReader(istream);
          // With new file format we need to read the header and then unzip the xml
          TestReportHeader hdr = new TestReportHeader();
          double fileversion = rdr.ReadDouble();
          //writer.Write(timePerformed.Ticks);
          hdr.TimePerformed = new DateTime(rdr.ReadInt64());
          //writer.Write(operatorName);
          hdr.OperatorName = rdr.ReadString();
          //writer.Write(gearboxSerialNumber);
          hdr.GearboxSerialNumber = rdr.ReadString();
          //writer.Write(testName);
          hdr.TestName = rdr.ReadString();

          // DotNetZip needs a separate stream copy, because it sets the stream position to 0 before reading.
          Stream zipStream = new MemoryStream();
          //FileStream zipStream = new FileStream("c:/jebaniec.zip", FileMode.Create);
          byte[] buffer = new byte[32768];
          while (true)
          {
            int read = istream.Read(buffer, 0, buffer.Length);
            if (read <= 0) break;
            zipStream.Write(buffer, 0, read);
          }
          zipStream.Position = 0;
          istream.Position = 0;
          //zipStream.Close();

          Ionic.Zip.ZipFile zip = Ionic.Zip.ZipFile.Read(zipStream);
          Stream xmlStream = new MemoryStream();
          foreach (var entry in zip.Entries)
          {
            MemoryStream memoryStream = new MemoryStream();
            entry.Extract(memoryStream);
            memoryStream.Position = 0;
            tsr = OpenXml(new StreamReader(memoryStream));
          }

          return tsr;    
        }
      }
      
    }

    /// <summary>
    /// Opens XML file on the disk and returns TestScriptReport object
    /// </summary>
    /// <param name="filename">File path</param>
    /// <returns>Object with content loaded from specified file</returns>
    static private TestScriptReport OpenXml(string filename)
    {
      TestScriptReport tsr;
      using (StreamReader myReader = new StreamReader(filename))
      {
        XmlSerializer myXML = new XmlSerializer(typeof(TestScriptReport));
        tsr = (TestScriptReport)myXML.Deserialize(myReader);
      }
      tsr.mFilename = filename;

      if (tsr.DeserializationMaintenanceTemporaryFix())
      {
        tsr.SaveToFile(filename);
      }
      return tsr;
    }


    /// <summary>
    /// Opens XML file on the disk and returns TestScriptReport object
    /// </summary>
    /// <param name="filename">File path</param>
    /// <returns>Object with content loaded from specified file</returns>
    static public TestScriptReport OpenXml(StreamReader rdr)
    {
      TestScriptReport tsr;
        XmlSerializer myXML = new XmlSerializer(typeof(TestScriptReport));
        tsr = (TestScriptReport)myXML.Deserialize(rdr);

      return tsr;
    }

    /// <summary>
    /// Saves this object to XML file
    /// </summary>
    /// <param name="filename">File path</param>
    private void SaveXml( string filename )
    {
      // write the class to the file  
      using ( StreamWriter myWriter = new StreamWriter( filename ) )
      {
        XmlSerializer myXML = new XmlSerializer( typeof( TestScriptReport ) );
        myXML.Serialize( myWriter, this );
      }
    }

    /// <summary>
    /// Saves this object as XML file into StreamWriter
    /// </summary>
    /// <param name="stream">Stream to write to</param>
    private void SaveXml(StreamWriter myWriter)
    {
        XmlSerializer myXML = new XmlSerializer(typeof(TestScriptReport));
        myXML.Serialize(myWriter, this);
    }

    private bool DeserializationMaintenanceTemporaryFix()
    {
      bool saveFile = false;

      if (TestScriptRunned.DeserializationMaintenanceTemporaryFix())
      {
        saveFile = true;
      }

      return saveFile;
    }

    #endregion Methods
  }
}
