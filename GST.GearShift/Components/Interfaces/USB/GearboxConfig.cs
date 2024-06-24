using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

using GST.Gearshift.Components.Utilities;
using Soko.Common.Common;

namespace GST.Gearshift.Components.Interfaces.USB
{
  /// <remarks>
  /// Class describing the gearbox configuration
  /// </remarks>
  [Serializable]
  public class GearboxConfig
  {

    #region Constants

    private readonly int mCurrentChannelsMaxCount = 18; // current channels count
    private readonly int mPressureChannelsMaxCount = 14; // pressure channels count

    #endregion  Constants


    #region Private fields

    // First ever file format number
    private readonly static UInt16 FileVersionDefault = 0001;
    // Actual file format number 
    private readonly static UInt16 FileVersionCurrent = 0024;

    // File format that's been deserialized from XML, on SaveXml it's set to FileVersionCurrent, so it's possible to distinguish whether the file is in obsolete format
    public UInt16 FileVersionSaved = FileVersionDefault;

    private string mName = "";
    private string mManufacturer = "";
    private string mModel = "";
    private Bitmap mPicture = null;
    private float _pressureVariationTolerance = 0.1f;
    private float _ignorePressureLessThan = 0.2f;

    private string mFilename = "";

    private int mPwmFrequencyHz = 200; // PWM frequency the gearbox should run at

    private DisplayChannelsSet mPressureDisplayChannelsSet = null;
    private DisplayChannelsSet mCurrentDisplayChannelsSet = null;

    private GearboxCANConfig mGearboxCanConfig = new GearboxCANConfig();

    public List<GearShiftUsb.AIChannel> _analogueInputs = new List<GearShiftUsb.AIChannel>();

    private GearboxControllerType _controllerType = GearboxControllerType.NON_MECHATRONIC;


    #endregion Private fields


    #region Constructors & finalizer

    /// <summary>
    /// The default constructor
    /// </summary>
    public GearboxConfig()
    {
      //set the default picture
      mPicture = GST.Gearshift.Components.Properties.Resources.GearboxScaled;

      //create new display channels sets
      mCurrentDisplayChannelsSet = new DisplayChannelsSet();
      mCurrentDisplayChannelsSet.ChannelsMaxCount = mCurrentChannelsMaxCount;
      mCurrentDisplayChannelsSet.ChannelsCommonMinMaxValues = true;

      mPressureDisplayChannelsSet = new DisplayChannelsSet();
      mPressureDisplayChannelsSet.ChannelsMaxCount = mPressureChannelsMaxCount;
      mPressureDisplayChannelsSet.ChannelsCommonMinMaxValues = false;

      // Set the default gearbox controller to non-mechatronic (old DAQ)
      _controllerType = GearboxControllerType.NON_MECHATRONIC;
      // Create the Zf6 current displays. Cannot me modified from the outside
    }

    #endregion Constructors & finalizer


    #region Events



    #endregion Events


    #region Properties

    public float PressureVariationTolerance
    {
      get { return _pressureVariationTolerance; }
      set { _pressureVariationTolerance = value; }
    }

    /// <summary>
    /// The pressure value under which the error is not calculated, value in base unit
    /// </summary>
    public float IgnorePressureLessThan_BaseUnit
    {
      get { return _ignorePressureLessThan; }
      set { _ignorePressureLessThan = value; }
    }

    /// <summary>
    /// The pressure value under which the error is not calculated, value in user selected unit
    /// </summary>
    // Do not serialize this field, the proper value is serialized as a base value.
    [System.Xml.Serialization.XmlIgnore]
    public float IgnorePressureLessThan_UserUnit
    {
      get
      {
        return MeasurementUnit.ConvertAIValueBaseUnitToUserUnit(_ignorePressureLessThan, MeasurementUnit.ValueType.Pressure);
      }
      set
      {
        _ignorePressureLessThan = MeasurementUnit.ConvertAIValueUserUnitToBaseUnit(value, MeasurementUnit.ValueType.Pressure);
      }
    }

    /// <summary>
    /// Returns the max allowed Current channels count
    /// </summary>
    public int CurrentChannelsMaxCount
    {
      get { return mCurrentChannelsMaxCount; }
    }

    /// <summary>
    /// Returns the max allowed pressure channels count
    /// </summary>
    public int PressureChannelsMaxCount
    {
      get { return mPressureChannelsMaxCount; }
    }
  
    /// <summary>
    /// The object containing pressure displays set
    /// </summary>
    public DisplayChannelsSet PressureDisplayChannelsSet
    {
      get { return mPressureDisplayChannelsSet; }
      set
      {
        if (value == null)
          throw new NullReferenceException("[GearboxConfig.PressureDisplayChannelsSet.Set] : Pressure display channels set cannot be null");
        if (value.ChannelsCount > mPressureChannelsMaxCount)
          throw new ArgumentException("[GearboxConfig.PressureDisplayChannelsSet.Set] : Pressure displays count cannot be greater than PressureChannelsMaxCount");
        mPressureDisplayChannelsSet = value;
        mPressureDisplayChannelsSet.ChannelsMaxCount = mPressureChannelsMaxCount;
      }
    }

    /// <summary>
    /// The object containing current displays set
    /// </summary>
    public DisplayChannelsSet CurrentDisplayChannelsSet
    {
      get 
      {
          return mCurrentDisplayChannelsSet;
      }
      set 
      {
        // The channels can only be assigned in non-mechatronic gearbox types (old DAQ)
        if (_controllerType == GearboxControllerType.NON_MECHATRONIC)
        {
          if (value == null)
            throw new NullReferenceException("[GearboxConfig.CurrentDisplayChannelsSet.Set] : Current display channels set cannot be null");
          if (value.ChannelsCount > mCurrentChannelsMaxCount)
            throw new ArgumentException("[GearboxConfig.CurrentDisplayChannelsSet.Set] : Current displays count cannot be greater than CurrentChannelsMaxCount");
          mCurrentDisplayChannelsSet = value;
          mCurrentDisplayChannelsSet.ChannelsMaxCount = mCurrentChannelsMaxCount;
        }
      }
    }

    /// <summary>
    /// The gearbox name string
    /// </summary>
    public string Name
    {
      get { return mName; }
      set
      {
        if (value == null)
          throw new NullReferenceException("[GearboxConfig.Name.Set] : Name string cannot be null");
        mName = value;
      }
    } 

    /// <summary>
    /// The manufacturer name string
    /// </summary>
    public string Manufacturer
    {
      get { return mManufacturer; }
      set
      {
        if (value == null)
          throw new NullReferenceException("[GearboxConfig.Manufacturer.Set] : Manufacturer string cannot be null");
        mManufacturer = value;
      }
    }

    /// <summary>
    /// the gearbox model string
    /// </summary>
    public string Model
    {
      get { return mModel; }
      set
      {
        if (value == null)
          throw new NullReferenceException("[GearboxConfig.Model.Set] : Model string cannot be null");
        mModel = value;
      }
    }
  
    //must be ignored in XML serialization, because image must be serialized do string first (SerializedPictured property does the job)
    /// <summary>
    /// Gearbox picture object.
    /// Must be ignored in XML serialization, because image must be serialized do string first (SerializedPictured property does the job)
    /// </summary>
    [XmlIgnoreAttribute]
    public Bitmap Picture
    {
      get { return mPicture; }
      set 
      {
        if ( value == null )
          throw new NullReferenceException("[GearboxConfig.MPicture.Set] : Picture object cannot be null");
        mPicture = value; 
      }
    }

    /// <summary>
    /// Gearbox picture serialized to string (enables easy class XML serialization
    /// </summary>
    public String SerializedPicture
    {
      //used only for XML serialization
      get 
      {
        String encodedPicture;
        using ( MemoryStream imageStream = new MemoryStream() )
        {
          mPicture.Save(imageStream, ImageFormat.Png);
          imageStream.Position = 0;
          encodedPicture = Convert.ToBase64String(imageStream.ToArray(), Base64FormattingOptions.None);
        }
        return encodedPicture;
      }
      set 
      {
        MemoryStream imageStream = new MemoryStream( Convert.FromBase64String( XmlConvert.DecodeName( value ) ) );
        mPicture = new Bitmap( imageStream );
        imageStream.Dispose();
      }
    }

    /// <summary>
    /// Returns the gearbox config file path (if has been opened from or saved to file).
    /// This value is changed only by file saving/opening methods. Empty string means new config object.
    /// </summary>
    public string Filename
    {
      get { return mFilename; }
    }

    /// <summary>
    /// Defines the frequency in [Hz] the gearbox has attributed        
    /// </summary>
    public int PwmFrequencyHz
    {
      get { return mPwmFrequencyHz; }
      set 
      {
       if (value < 0)
         throw new ArgumentException("[GearboxConfig.PwmFrequencyHz.Set] : PwmFrequency must be greater than 0");
         mPwmFrequencyHz = value; 
      }
    }

    /// <summary>
    /// Returns actual number of Current displays in configuration
    /// </summary>
    public int CurrentDisplayChannelsCount
    {
      get { return mCurrentDisplayChannelsSet.ChannelsCount; }
    }

    public GearboxControllerType ControllerType
    {
      get { return _controllerType; }
      set { _controllerType = value; }
    }

    #endregion Properties


    #region Methods

    /// <summary>
    /// Loads the new class from XML file. This object is not changed !
    /// </summary>
    /// <param name="filename">File path</param>
    /// <returns>Object with content loaded from specified file</returns>
    public GearboxConfig OpenXml(string filename)
    {
      GearboxConfig gbc;
      using ( StreamReader myReader = new StreamReader( filename ) )
      {
        XmlSerializer myXML = new XmlSerializer( typeof( GearboxConfig ) );
        gbc = (GearboxConfig)myXML.Deserialize( myReader );
      }
      gbc.mFilename = filename;
      if (gbc.DeserializationMaintenanceTemporaryFix())
      {
        gbc.SaveXml(filename);
      }
      return gbc;
    }

    /// <summary>
    /// Saves this class to XML file
    /// </summary>
    /// <param name="filename">File path</param>
    public void SaveXml(string filename)
    {
      // write the class to the file  
      using (StreamWriter myWriter = new StreamWriter(filename))
      {
        XmlSerializer myXML = new XmlSerializer(typeof(GearboxConfig));
        myXML.Serialize(myWriter, this);
      }
      mFilename = filename;
    }

    ///// <summary>
    ///// Adds specified Pressure channel at specified index
    ///// </summary>
    ///// <param name="displayChannel"> Channel to be added</param>
    ///// <param name="index">Index to be added at</param>
    //public void AddPressureDisplayChannelAt(DisplayChannel displayChannel, int index)
    //{
    //  try //this should be caught in function calling this method
    //  {
    //    mPressureDisplayChannelsSet.AddChannelAt(displayChannel, index);
    //  }
    //  catch
    //  {
    //    throw;
    //  }  
    //}

    /// <summary>
    /// Adds specified Current channel at specified index
    /// </summary>
    /// <param name="displayChannel">Channel to be added</param>
    /// <param name="index">Index to be added at</param>
    public void AddCurrentDisplayChannelAt(DisplayChannel displayChannel, int index)
    {
      try //this should be caught in function calling this method
        {
          mCurrentDisplayChannelsSet.AddChannelAt(displayChannel, index);
        }
      catch
      {
        throw;
      }    
    }

    ///// <summary>
    ///// Removes the pressure channel at specified index
    ///// </summary>
    ///// <param name="index">Index of channel to be removed</param>
    //public void RemovePressureChannelAt(int index)
    //{
    //  try //this should be caught in function calling this method
    //  {
    //    mPressureDisplayChannelsSet.RemoveChannelAt(index);
    //  }
    //  catch
    //  {
    //    throw;
    //  }
    //}

    /// <summary>
    /// Removes the current channel at specified index
    /// </summary>
    /// <param name="index">Index of channel to be removed</param>
    public void RemoveCurrentChannelAt(int index)
    {
      try //this should be caught in function calling this method
      {
        mCurrentDisplayChannelsSet.RemoveChannelAt(index);
      }
      catch
      {
        throw;
      }
    }

    /// <summary>
    /// The function performs some maintenance on the data due to file format change etc
    /// </summary>
    /// <returns>bool if the data is changed and the file should be resaved on the disk</returns>
    public bool DeserializationMaintenanceTemporaryFix()
    {
      bool saveFile = false;

      try
      {
        foreach (DisplayChannel dc in CurrentDisplayChannelsSet.Channels)
        {

          // FIX 13, applied 25.09.2012, current display range set to 0-4 Amps, unit name fixed
          if (dc.MinValue != 0.0f)
          {
            dc.MinValue = 0.0f;
            saveFile = true;
          }
          if (dc.MaxValue != 4.0f)
          {
            dc.MaxValue = 4.0f;
            saveFile = true;
          }
          if (dc.UnitName.Contains("[") || dc.UnitName.Contains("]"))
          {
            dc.UnitName = dc.UnitName.Replace("[", "");
            dc.UnitName = dc.UnitName.Replace("]", "");
            saveFile = true;
          }
        }

        // If there are any old pressure channels in this file, transform them all into new structure
        if (mPressureDisplayChannelsSet.ChannelsCount > 0)
        {
          // Clear the existing channels
          _analogueInputs.Clear();
          foreach (DisplayChannel dc in mPressureDisplayChannelsSet.Channels)
          {
            GearShiftUsb.AIChannel aic = new GearShiftUsb.AIChannel();

            // Assign the input type
            if (dc.IsFlowGauge)
            {
              aic.ValueType = MeasurementUnit.ValueType.Flow;
            }
            else
            {
              if (dc.IsTorqueGauge)
              {
                aic.ValueType = MeasurementUnit.ValueType.Torque;
              }
              else
              {
                if (dc.IsTempGauge)
                {
                  aic.ValueType = MeasurementUnit.ValueType.Temperature;
                }
                else
                {
                  aic.ValueType = MeasurementUnit.ValueType.Pressure;
                }
              }
            }

            // Assign the label
            aic.Label = dc.Label;

            // Assign the device's input channel index
            aic.InputIndex = dc.InputChannelIndex;

            // Convert the min/max value to base unit
            if (dc.UnitName.ToLower().Contains("psi"))
            {
              aic.MinValueBaseUnit = Utilities.MeasurementUnit.Pressure_PSI_to_bar(dc.MinValue);
              aic.MaxValueBaseUnit = Utilities.MeasurementUnit.Pressure_PSI_to_bar(dc.MaxValue);
            }
            else
            {
              // I assume that temp/torque/flow gauges were not frequently used so far, so I dont give a fuck about converting their units. L.S.
              aic.MinValueBaseUnit = dc.MinValue;
              aic.MaxValueBaseUnit = dc.MaxValue;
            }
            _analogueInputs.Add(aic);
          }
          // Delete the old structure
          mPressureDisplayChannelsSet = new DisplayChannelsSet();
          //for (int i = 0; i < mPressureDisplayChannelsSet.ChannelsCount; i++ )
          //{
          //  mPressureDisplayChannelsSet.RemoveChannelAt(0);
          //}
          saveFile = true;
        }
      }
      catch (Exception)
      {
      }

      return saveFile;
    }

    #endregion Methods


  }
}