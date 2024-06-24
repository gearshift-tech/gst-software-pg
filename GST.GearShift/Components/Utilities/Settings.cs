using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

using GST.Gearshift.Components.Utilities;
using GST.Gearshift.Components.Interfaces.USB;

namespace GST.Gearshift.Components.Utilities
{
  public sealed class Settings
  {

    public enum DefTestMode
    {
      Automatic = 0,
      Manual = 1,
      Loop = 2
    }

    [Serializable]
    public class SerializedSettings
    {
      public FormWindowState mainFormWindowState = FormWindowState.Maximized;
      public string[] systemOperators = { "Default operator" };
      public bool autoSelectLastOperator = true;
      public string lastOperatorName = string.Empty;
      public float RPM_AO_multiplier = 0;
      public float Load_AO_multiplier = 0;
      public bool showAutomaticModeWarning = true;
      public bool showLoopModeWarning = true;

      public long oilFilterWearSeconds = 0;
      public double oilFilterLifetimeHours = 1000.0;
      public int oilFilterWearAIIndex = 0;
      public float oilFilterWearAIThreshold = 0.1f;

      public MeasurementUnit.FlowUnit userFlowUnit = MeasurementUnit.FlowUnit.LPM;
      public MeasurementUnit.PressureUnit userPressureUnit = MeasurementUnit.PressureUnit.bar;
      public MeasurementUnit.TemperatureUnit userTemperatureUnit = MeasurementUnit.TemperatureUnit.Celsius;
      public MeasurementUnit.TorqueUnit userTorqueUnit = MeasurementUnit.TorqueUnit.Nm;

      public string CompanyInfoPicture_asString = string.Empty;
    }

    Settings()
    {
    }


    private static Settings instance = null;
    private static readonly object padlock = new object();

    private SerializedSettings serializedSettings = new SerializedSettings();

    public static Settings Instance
    {
      get
      {
        lock (padlock)
        {
          if (instance == null)
          {
            instance = new Settings();
          }
          return instance;
        }
      }
    }

#if (_SOL_TESTER_)
    public static readonly string SoftwareVersion = "1.1";
#elif (_DYNO_SUITE_ || _VB_SUITE_)
    public static readonly string SoftwareVersion = "5.2.8";
#endif

#if (_VB_SUITE_)
    public static readonly string AppSuiteName = "ADVANCED VALVE BODY DIAGNOSTICS SUITE";
#elif (_DYNO_SUITE_)
    public static readonly string AppSuiteName = "ADVANCED DYNO DIAGNOSTICS SUITE";
#elif (_SOL_TESTER_)
    public static readonly string AppSuiteName = "SOLENOID TEST AND CONTROL STUDIO";
#endif
    public static readonly string MainFormTitle = "GearShift   " + AppSuiteName + "   V   " + SoftwareVersion + "   –   GearShift Technologies";

    private readonly string mSettingsFilePath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\GearShift Technologies\\GearShift\\AppSettings.sxml";

    public event EventHandler SystemTimerValueChanged;

    public static String ResourcesDirectory
    {
      get
      {
        return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\GearShift Technologies\\GearShift\\Resource data";
      }
    }

    public static String GearboxesDirectory
    {
      get
      {
        return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\GearShift Technologies\\GearShift\\Installed gearboxes";
      }
    }

    public static String CanTracesDirectory
    {
      get
      {
        return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\GearShift Technologies\\GearShift\\CAN_Traces";
      }
    }

    public static String ReportsDirectory
    {
      get { return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\GearShift Technologies\\GearShift\\Reports"; }
    }

    public static String[] AvailableCanTracesPaths
    {
      get
      {
        return Directory.GetFiles(CanTracesDirectory, "*.ezt", SearchOption.AllDirectories);
      }
    }

    public static String[] AvailableGearboxConfigsPaths
    {
      get
      {
        return Directory.GetFiles(GearboxesDirectory, "*.gcf", SearchOption.AllDirectories);
      }
    }

    public static String[] AvailableTestScriptsPaths
    {
      get
      {
        return Directory.GetFiles(GearboxesDirectory, "*.tsc", SearchOption.AllDirectories);
      }
    }

    public static String[] AvailableReportsPaths
    {
      get
      {
        var files =  Directory.GetFiles(ReportsDirectory, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".ctrf") || s.EndsWith(".trf"));
        return (String[])files.ToArray();
      }
    }

    public FormWindowState MainFormWindowState
    {
      get { return serializedSettings.mainFormWindowState; }
      set { serializedSettings.mainFormWindowState = value; }
    }

    public string[] SystemOperators
    {
      get { return serializedSettings.systemOperators; }
    }

    public bool SystemOperatorAdd(string name)
    {
      List<string> operators = new List<string>(serializedSettings.systemOperators);
      // Check if the operator does not already exist
      if (operators.Contains(name))
      {
        // If so, return false
        return false;
      }
      else
      {
        // If not, add it and return true
        operators.Add(name);
        serializedSettings.systemOperators = operators.ToArray();
        return true;
      }
    }

    public bool SystemOperatorRemove(string name)
    {
      List<string> operators = new List<string>(serializedSettings.systemOperators);
      // Check if the operator already exists
      if (operators.Contains(name))
      {
        // If so, remove it and return true
        operators.Remove(name);
        serializedSettings.systemOperators = operators.ToArray();
        return true;
      }
      else
      {
        // If not, return false
        return false;
      }
    }

    public float RPM_AO_multiplier
    {
      get { return serializedSettings.RPM_AO_multiplier; }
      set { serializedSettings.RPM_AO_multiplier = value; }
    }

    public float Load_AO_multiplier
    {
      get { return serializedSettings.Load_AO_multiplier; }
      set { serializedSettings.Load_AO_multiplier = value; }
    }

    public bool ShowAutomaticModeWarning
    {
      get { return serializedSettings.showAutomaticModeWarning; }
      set { serializedSettings.showAutomaticModeWarning = value; }
    }

    public bool ShowLoopModeWarning
    {
      get { return serializedSettings.showLoopModeWarning; }
      set { serializedSettings.showLoopModeWarning = value; }
    }

    public long OilFilterWearSeconds
    {
      get { return serializedSettings.oilFilterWearSeconds; }
      set { serializedSettings.oilFilterWearSeconds = value; }
    }

    public double OilFilterLifetimeHours
    {
      get { return serializedSettings.oilFilterLifetimeHours; }
      set { serializedSettings.oilFilterLifetimeHours = value; }
    }

    public int OilFilterWearAIIndex
    {
      get { return serializedSettings.oilFilterWearAIIndex; }
      set { serializedSettings.oilFilterWearAIIndex = value; }
    }

    public float OilFilterWearAIThreshold
    {
      get { return serializedSettings.oilFilterWearAIThreshold; }
      set { serializedSettings.oilFilterWearAIThreshold = value; }
    }

    /// <summary>
    /// Company information image
    /// </summary>
    public Image CompanyInfoPicture
    {
      //used only for XML serialization
      get
      {
        try
        {
          MemoryStream imageStream = new MemoryStream(Convert.FromBase64String(XmlConvert.DecodeName(serializedSettings.CompanyInfoPicture_asString)));
          return new Bitmap(imageStream);
        }
        catch
        {
          return new Bitmap(1, 1);
        }
      }
      set
      {
        using (MemoryStream imageStream = new MemoryStream())
        {
          value.Save(imageStream, System.Drawing.Imaging.ImageFormat.Png);
          imageStream.Position = 0;
          serializedSettings.CompanyInfoPicture_asString = Convert.ToBase64String(imageStream.ToArray(), Base64FormattingOptions.None);
        }
      }
    }

    public MeasurementUnit.FlowUnit UserFlowUnit
    {
      get { return serializedSettings.userFlowUnit; }
      set { serializedSettings.userFlowUnit = value; }
    }
    public MeasurementUnit.PressureUnit UserPressureUnit
    {
      get { return serializedSettings.userPressureUnit; }
      set { serializedSettings.userPressureUnit = value; }
    }
    public MeasurementUnit.TemperatureUnit UserTemperatureUnit
    {
      get { return serializedSettings.userTemperatureUnit; }
      set { serializedSettings.userTemperatureUnit = value; }
    }
    public MeasurementUnit.TorqueUnit UserTorqueUnit
    {
      get { return serializedSettings.userTorqueUnit; }
      set { serializedSettings.userTorqueUnit = value; }
    }

    public bool LoadSettingsFromDisk()
    {
      try
      {
        using (StreamReader myReader = new StreamReader(mSettingsFilePath))
        {
          XmlSerializer myXML = new XmlSerializer(typeof(SerializedSettings));
          serializedSettings = (SerializedSettings)myXML.Deserialize(myReader);
        }
        return true;
      }
      catch (Exception ex)
      {
        Console.WriteLine("READ SETTINGS FILE EXCEPTION: " + ex.Message);
        serializedSettings = new SerializedSettings();
        return false;
      }
    }

    public bool SaveSettingsToDisk()
    {
      try
      {
        using (StreamWriter myWriter = new StreamWriter(mSettingsFilePath))
        {
          XmlSerializer myXML = new XmlSerializer(typeof(SerializedSettings));
          myXML.Serialize(myWriter, serializedSettings);
        }
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }

  }
}
