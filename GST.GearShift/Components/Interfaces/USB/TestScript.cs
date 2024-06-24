using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Drawing.Drawing2D;
using System.IO;
using System.Xml;
using System.Xml.Serialization;



namespace GST.Gearshift.Components.Interfaces.USB
{
  [Serializable]
  public class TestScript
  {



    #region Constants



    #endregion  Constants



    #region Private fields

    //[field: NonSerialized]
    private List<TestScriptFrame> mFrameSet = null;

    private GearboxConfig mGearbox = null;

    //the test script MUST have a correct (nonempty) gearbox config assigned
    private bool mGearboxCorrect = false;

    private string mName = "";

    private string mFilename = "";

    // This genuine ID is stored to keep track of the test script master data consistence.
    // Whenever any fields in the test script are changed, new Guid must be generated and current master data must be invalidated.
    private Guid mGuid = Guid.NewGuid();

    private bool mHasConsistentMasterData = false;

    private string mSerializedScreenLayout = "<?xml version=\"1.0\" encoding=\"utf-16\"?> <Layout> <Window Guid=\"17fa7f49-3d14-4e4f-ba28-88f7a6ae8c93\" LastFocused=\"129641873499922440\" DockedSize=\"200\" PopupSize=\"0\" FloatingLocation=\"-1, -1\" FloatingSize=\"550, 400\" LastOpenDockSituation=\"Document\" LastFixedDockSituation=\"Document\" LastFixedDockLocation=\"0\" LastFloatingWindowGuid=\"00000000-0000-0000-0000-000000000000\" LastDockContainerCount=\"0\" LastDockContainerIndex=\"0\" DockedWorkingSize=\"250, 400\" DockedWindowGroupGuid=\"00000000-0000-0000-0000-000000000000\" DockedIndexInWindowGroup=\"0\" DockedSplitPath=\"0\" DocumentWorkingSize=\"550, 380.6344\" DocumentWindowGroupGuid=\"4aec9f1f-5d14-4786-9650-ab9a6dad2c75\" DocumentIndexInWindowGroup=\"0\" DocumentSplitPath=\"0\" FloatingWorkingSize=\"250, 400\" FloatingWindowGroupGuid=\"00000000-0000-0000-0000-000000000000\" FloatingIndexInWindowGroup=\"0\" FloatingSplitPath=\"0\" /> <Window Guid=\"722ec64d-cd54-4e61-9fed-9b8762362a02\" LastFocused=\"129641873392976323\" DockedSize=\"200\" PopupSize=\"0\" FloatingLocation=\"-1, -1\" FloatingSize=\"550, 400\" LastOpenDockSituation=\"Document\" LastFixedDockSituation=\"Document\" LastFixedDockLocation=\"0\" LastFloatingWindowGuid=\"00000000-0000-0000-0000-000000000000\" LastDockContainerCount=\"0\" LastDockContainerIndex=\"0\" DockedWorkingSize=\"250, 400\" DockedWindowGroupGuid=\"00000000-0000-0000-0000-000000000000\" DockedIndexInWindowGroup=\"0\" DockedSplitPath=\"0\" DocumentWorkingSize=\"550, 380.6344\" DocumentWindowGroupGuid=\"4aec9f1f-5d14-4786-9650-ab9a6dad2c75\" DocumentIndexInWindowGroup=\"1\" DocumentSplitPath=\"0\" FloatingWorkingSize=\"250, 400\" FloatingWindowGroupGuid=\"00000000-0000-0000-0000-000000000000\" FloatingIndexInWindowGroup=\"0\" FloatingSplitPath=\"0\" /> <Window Guid=\"49b87529-a5f2-4aa2-9331-1084f33e9ba1\" LastFocused=\"129641873861893143\" DockedSize=\"200\" PopupSize=\"0\" FloatingLocation=\"-1, -1\" FloatingSize=\"550, 400\" LastOpenDockSituation=\"Document\" LastFixedDockSituation=\"Document\" LastFixedDockLocation=\"0\" LastFloatingWindowGuid=\"00000000-0000-0000-0000-000000000000\" LastDockContainerCount=\"0\" LastDockContainerIndex=\"0\" DockedWorkingSize=\"250, 400\" DockedWindowGroupGuid=\"00000000-0000-0000-0000-000000000000\" DockedIndexInWindowGroup=\"0\" DockedSplitPath=\"0\" DocumentWorkingSize=\"550, 380.6344\" DocumentWindowGroupGuid=\"7ceba3bb-cb2a-4e7d-8bf7-313bba0332ff\" DocumentIndexInWindowGroup=\"0\" DocumentSplitPath=\"1\" FloatingWorkingSize=\"250, 400\" FloatingWindowGroupGuid=\"00000000-0000-0000-0000-000000000000\" FloatingIndexInWindowGroup=\"0\" FloatingSplitPath=\"0\" /> <Window Guid=\"380f02a6-0fbe-437b-bf90-d2cff9b9f499\" LastFocused=\"129641874352231189\" DockedSize=\"233\" PopupSize=\"0\" FloatingLocation=\"-1, -1\" FloatingSize=\"200, 650\" LastOpenDockSituation=\"Docked\" LastFixedDockSituation=\"Docked\" LastFixedDockLocation=\"Left\" LastFloatingWindowGuid=\"00000000-0000-0000-0000-000000000000\" LastDockContainerCount=\"1\" LastDockContainerIndex=\"0\" DockedWorkingSize=\"250, 400\" DockedWindowGroupGuid=\"c0746e2d-3471-4994-9e29-96f4e30486b2\" DockedIndexInWindowGroup=\"0\" DockedSplitPath=\"0\" DocumentWorkingSize=\"250, 400\" DocumentWindowGroupGuid=\"00000000-0000-0000-0000-000000000000\" DocumentIndexInWindowGroup=\"0\" DocumentSplitPath=\"0\" FloatingWorkingSize=\"250, 400\" FloatingWindowGroupGuid=\"00000000-0000-0000-0000-000000000000\" FloatingIndexInWindowGroup=\"0\" FloatingSplitPath=\"0\" /> <Window Guid=\"3802c232-2ca9-4963-b193-29df020e58a2\" LastFocused=\"0\" DockedSize=\"98\" PopupSize=\"0\" FloatingLocation=\"-1, -1\" FloatingSize=\"250, 400\" LastOpenDockSituation=\"Docked\" LastFixedDockSituation=\"Docked\" LastFixedDockLocation=\"Bottom\" LastFloatingWindowGuid=\"00000000-0000-0000-0000-000000000000\" LastDockContainerCount=\"0\" LastDockContainerIndex=\"-1\" DockedWorkingSize=\"250, 400\" DockedWindowGroupGuid=\"d16cd9e0-bc63-4fa8-ab9d-33884ca06283\" DockedIndexInWindowGroup=\"0\" DockedSplitPath=\"0\" DocumentWorkingSize=\"250, 400\" DocumentWindowGroupGuid=\"00000000-0000-0000-0000-000000000000\" DocumentIndexInWindowGroup=\"0\" DocumentSplitPath=\"0\" FloatingWorkingSize=\"250, 400\" FloatingWindowGroupGuid=\"00000000-0000-0000-0000-000000000000\" FloatingIndexInWindowGroup=\"0\" FloatingSplitPath=\"0\" /> <Window Guid=\"bfc0300d-18d2-4754-a184-c28c979351bf\" LastFocused=\"129641873860903087\" DockedSize=\"200\" PopupSize=\"0\" FloatingLocation=\"-1, -1\" FloatingSize=\"550, 400\" LastOpenDockSituation=\"Document\" LastFixedDockSituation=\"Document\" LastFixedDockLocation=\"0\" LastFloatingWindowGuid=\"00000000-0000-0000-0000-000000000000\" LastDockContainerCount=\"0\" LastDockContainerIndex=\"0\" DockedWorkingSize=\"250, 400\" DockedWindowGroupGuid=\"00000000-0000-0000-0000-000000000000\" DockedIndexInWindowGroup=\"0\" DockedSplitPath=\"0\" DocumentWorkingSize=\"550, 380.6344\" DocumentWindowGroupGuid=\"4aec9f1f-5d14-4786-9650-ab9a6dad2c75\" DocumentIndexInWindowGroup=\"0\" DocumentSplitPath=\"0\" FloatingWorkingSize=\"250, 400\" FloatingWindowGroupGuid=\"00000000-0000-0000-0000-000000000000\" FloatingIndexInWindowGroup=\"0\" FloatingSplitPath=\"0\" /> <Container Dock=\"3\" ContentSize=\"233\"> <SplitLayoutSystem WorkingSize=\"250, 400\" SplitMode=\"0\"> <ControlLayoutSystem WorkingSize=\"250, 400\" Guid=\"c0746e2d-3471-4994-9e29-96f4e30486b2\" Collapsed=\"0\" SelectedControl=\"380f02a6-0fbe-437b-bf90-d2cff9b9f499\"> <Controls> <Control Guid=\"380f02a6-0fbe-437b-bf90-d2cff9b9f499\" /> </Controls> </ControlLayoutSystem> </SplitLayoutSystem> </Container> <DocumentContainer Dock=\"5\"> <SplitLayoutSystem WorkingSize=\"250, 400\" SplitMode=\"0\"> <ControlLayoutSystem WorkingSize=\"550, 380.6344\" Guid=\"4aec9f1f-5d14-4786-9650-ab9a6dad2c75\" Collapsed=\"0\" SelectedControl=\"bfc0300d-18d2-4754-a184-c28c979351bf\"> <Controls> <Control Guid=\"bfc0300d-18d2-4754-a184-c28c979351bf\" /> <Control Guid=\"722ec64d-cd54-4e61-9fed-9b8762362a02\" /> </Controls> </ControlLayoutSystem> <ControlLayoutSystem WorkingSize=\"550, 380.6344\" Guid=\"7ceba3bb-cb2a-4e7d-8bf7-313bba0332ff\" Collapsed=\"0\" SelectedControl=\"49b87529-a5f2-4aa2-9331-1084f33e9ba1\"> <Controls> <Control Guid=\"49b87529-a5f2-4aa2-9331-1084f33e9ba1\" /> </Controls> </ControlLayoutSystem> </SplitLayoutSystem> </DocumentContainer> </Layout>";

    #endregion Private fields



    #region Constructors & finalizer

    /// <summary>
    /// Default constructor
    /// </summary>
    public TestScript()
    {
      mFrameSet = new List<TestScriptFrame>(0);
      mGearbox = new GearboxConfig();
    }

    #endregion Constructors & finalizer



    #region Events



    #endregion Events



    #region Properties

    /// <summary>
    /// Gets/sets the gearbox configuration embedded in the test script
    /// </summary>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public GearboxConfig Gearbox
    {
      get { return mGearbox; }
      set
      {
        //if (value == null)
        //  throw new ArgumentNullException("[TestScript.Gearbox.Set]: The value cannot be null");
        mGearbox = value;
        mGearboxCorrect = true;
      }
    }

    /// <summary>
    /// Gets/sets the Frame set
    /// Must be ignored in XML serialization, FrameSetArray does the job
    /// </summary>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public List<TestScriptFrame> FrameSet
    {
      get { return mFrameSet; }
      set {mFrameSet = value; }
    }

    public bool GearboxCorrect
    {
      get { return mGearboxCorrect; }
    }

    /// <summary>
    /// The test script genuine ID
    /// </summary>
    public Guid Guid
    {
      get { return mGuid; }
      set
      {
        if ( value == null )
          throw new NullReferenceException( "[TestScript.Guid.Set] : Assigned GUID cannot be null" );
        mGuid = value;
      }
    }

    /// <summary>
    /// If this test script has proper Master data assigned
    /// </summary>
    public bool HasConsistentMasterData
    {
      get { return mHasConsistentMasterData;  }
      set { mHasConsistentMasterData = value; }
    }

    /// <summary>
    /// The test script name string
    /// </summary>
    public string Name
    {
      get { return mName; }
      set
      {
        if ( value == null )
          throw new NullReferenceException( "[TestScript.Name.Set] : Name string cannot be null" );
        mName = value;
      }
    }

    /// <summary>
    /// Contains XML serialized screen layout
    /// </summary>
    public string SerializedScreenLayout
    {
      get { return mSerializedScreenLayout; }
      set { mSerializedScreenLayout = value;}
    }

    /// <summary>
    /// Returns the gearbox config file path (if has been opened from or saved to file).
    /// This value is changed only by file saving/opening methods. Empty string means new config object.
    /// </summary>
    public string Filename
    {
      get { return mFilename; }
    }

    #endregion Properties



    #region Methods

    /// <summary>
    /// Loads the new class from XML file. This object is not changed !
    /// </summary>
    /// <param name="filename">File path</param>
    /// <returns>Object with content loaded from specified file</returns>
    public static TestScript OpenXml(string filename)
    {
      TestScript ts;
      using (StreamReader myReader = new StreamReader(filename))
      {
        XmlSerializer myXML = new XmlSerializer(typeof(TestScript));
        ts = (TestScript)myXML.Deserialize(myReader);
      }
      ts.mFilename = filename;

      if (ts.DeserializationMaintenanceTemporaryFix())
      {
        ts.SaveXml(ts.Filename);
      }
      return ts;
    }

    /// <summary>
    /// Saves this class to XML file
    /// </summary>
    /// <param name="filename">File path</param>
    public void SaveXml(string filename)
    {
      // Mark the bug as fixed
      for (int i = 0; i < FrameSet.Count; i++)
      {
        FrameSet[i].FrameIndex = i;
      }
      // write the class to the file  
      using (StreamWriter myWriter = new StreamWriter(filename))
      {
        XmlSerializer myXML = new XmlSerializer(typeof(TestScript));
        myXML.Serialize(myWriter, this);
      }
      mFilename = filename;
    }

    public bool DeserializationMaintenanceTemporaryFix()
    {
      bool saveFile = false;

      for (int i = 0; i < FrameSet.Count; i++)
      {
        FrameSet[i].FrameIndex = i;
        // Fix the frame duration problem
        if (FrameSet[i].Duration < 10)
        {
          FrameSet[i].Duration = 10;
          saveFile = true;
        }
        // Fix the empty master data problem
        if (FrameSet[i].MasterPressureReadValues.Count == 0)
        {
          FrameSet[i].MasterPressureReadValues = new List<float>(new float[14]);
          saveFile = true;
        }
      }

      if (Gearbox.DeserializationMaintenanceTemporaryFix())
      {
        saveFile = true;
      }

      return saveFile;
    }

    #endregion Methods



  }
}