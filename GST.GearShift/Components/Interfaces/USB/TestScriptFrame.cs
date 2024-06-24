using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Collections;
using System.Data;
using System.Text;
using System.Drawing.Drawing2D;
using System.IO;
using System.Xml;
using System.Xml.Serialization;


namespace GST.Gearshift.Components.Interfaces.USB
{

  /// <summary>
  /// Class representing a single frame of the test script
  /// </summary>
  [Serializable]
  public partial class TestScriptFrame
  {



    #region Constants



    #endregion  Constants



    #region Private fields

    // Duration of this frame in ms
    private int _duration = 0;

    // Array of solenoid drive values
    private List<Int32> _outputsDriveValues = new List<Int32>();
    //Name o this frame (gear name)
    private string mFrameName = string.Empty;

    // If data should be acquired after this frame
    private bool mAcquireData = false;

    private int mFrameIndex = 0;

    // If this frame should be passed through in manual mode
    private bool mIsPassThrough = false;

    // ID of the last packet from this frame, must be set externally
    private UInt32 mCriticalID = 0;

    // Text to be displayed as a user prompt
    private string mUserPrompt = string.Empty;

    // If user prompt should be displayed on this frame
    private bool mUserPromptEnabled = false;

    // If pass/fail conditions should be checked at the end of this frame (is set internally basing on mPressure min/max Values)
    private bool mCheckPassFailConditions = false;

    // Array of pressure read out values at the end of this frame
    private List<float> _pressureReadValues = new List<float>();

    // Array of current read out values at the end of this frame
    private List<float> _currentReadValues = new List<float>();

    // Array of master pressure values at the end of this frame
    private List<float> _pressureMasterValues = new List<float>();
    
    // Analog output 1 value for this frame. 0-10.00V
    private float mAnalogOutput1Value = 0.0f;

    // Analog output 2 value for this frame. 0-10.00V
    private float mAnalogOutput2Value = 0.0f;

    // Dyno motor RPM value for this frame
    private float mDynoMotorRPM = 0.0f;

    // Dyno load current value for this frame
    private float mDynoLoadCurrent = 0.0f;

    // If this line should be played in a loop mode
    private bool mIsPartOfTheLoop = false;

    // The number of the gear that Zf6 should switch to. -1 to 6 are allowed with 0.5 increment to denote the in-between-gears state.
    private float _zf6GearNumber = 0.0f;

    // EDS5 value for this frame
    private int _zf6EDS5Value = 0;

    // EDS5 value for this frame
    private int _zf6EDS6Value = 0;

    private float mEnigmaGearNumber = 0.0f;

    #endregion Private fields



        #region Constructors & finalizer

        /// <summary>
        /// The default constructor
        /// </summary>
        public TestScriptFrame()
    {
      _outputsDriveValues = new List<Int32>( new int[9] );
      _currentReadValues = new List<float>(new float[18]);
      _pressureReadValues = new List<float>(new float[14]);
      _pressureMasterValues = new List<float>(new float[14]);
    }

    #endregion Constructors & finalizer



    #region Events



    #endregion Events



    #region Properties

    public int FrameIndex
    {
      get { return mFrameIndex; }
      set { mFrameIndex = value; }
    }

    /// <summary>
    /// Gets/Sets the duration of this frame in ms
    /// </summary>
    public int Duration
    {
      get { return _duration; }
      set
      {
        if ( value < 0 )
          throw new ArgumentOutOfRangeException( "[TestScriptFrame.Duration.Set]: The value cannot be negative" );
        _duration = value;
      }
    }

    /// <summary>
    /// Text to be displayed as a user prompt
    /// </summary>
    public string UserPrompt
    {
      get { return mUserPrompt; }
      set
      {
        mUserPrompt = value;
      }
    }

    /// <summary>
    /// If user prompt should be displayed on this frame
    /// </summary>
    public bool UserPromptEnabled
    {
      get { return mUserPromptEnabled; }
      set
      {
        mUserPromptEnabled = value;
      }
    }

    /// <summary>
    /// Gets/Sets the solenoid drive values array
    /// It must be ignored in XML serialization
    /// </summary>
    [XmlIgnoreAttribute,
    DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public List<Int32> ChannelDrives
    {
      get { return _outputsDriveValues; }
      set
      {
        if ( value == null )
          throw new ArgumentNullException( "[TestScriptFrame.Channels.Set]: The value cannot be null" );
        _outputsDriveValues = value;
      }
    }

    /// <summary>
    /// Gets/Sets the solenoid drive values array
    /// This shall not be used for data access
    /// This is added only to provide XML synchrinization
    /// </summary>
    /// [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Int32[] ChannelDrivesArray
    {
      get
      {
        Int32[] arr = new Int32[_outputsDriveValues.Count];
        _outputsDriveValues.CopyTo( arr, 0 );
        return arr;
      }
      set
      {
        if ( value == null )
          throw new ArgumentNullException( "[TestScriptFrame.ChannelDrivesArray.Set]: The value cannot be null" );
        _outputsDriveValues.Clear();
        _outputsDriveValues.AddRange( value );
      }
    }

    /// <summary>
    /// Gets/Sets the pressure channels maximal values that are accepted in this frame
    /// It must be ignored in XML serialization
    /// </summary>
    [XmlIgnoreAttribute]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public List<float> PressureReadValues
    {
      get { return _pressureReadValues; }
      set
      {
        if ( value == null )
          throw new ArgumentNullException( "[TestScriptFrame.PressureReadValues.Set]: The value cannot be null" );
        _pressureReadValues = value;
      }
    }

    /// <summary>
    /// Gets/Sets the pressure channels maximal values that are accepted in this frame
    /// This shall not be used for data access
    /// This is added only to provide XML synchrinization
    /// </summary>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public float[] PressureReadValuesArr
    {
      get
      {
        float[] arr = new float[_pressureReadValues.Count];
        _pressureReadValues.CopyTo( arr, 0 );
        return arr;
      }
      set
      {
        if ( value == null )
          throw new ArgumentNullException( "[TestScriptFrame.PressureReadValuesArr.Set]: The value cannot be null" );
        _pressureReadValues.Clear();
        _pressureReadValues.AddRange( value );
      }
    }

    /// <summary>
    /// Gets/Sets the pressure channels maximal values that are accepted in this frame
    /// It must be ignored in XML serialization
    /// </summary>
    [XmlIgnoreAttribute,
    DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public List<float> CurrentReadValues
    {
      get { return _currentReadValues; }
      set
      {
        if ( value == null )
          throw new ArgumentNullException( "[TestScriptFrame.CurrentReadValues.Set]: The value cannot be null" );
        _currentReadValues = value;
      }
    }

    /// <summary>
    /// Gets/Sets the pressure channels maximal values that are accepted in this frame
    /// This shall not be used for data access
    /// This is added only to provide XML synchronization
    /// </summary>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public float[] CurrentReadValuesArr
    {
      get
      {
        float[] arr = new float[_currentReadValues.Count];
        _currentReadValues.CopyTo( arr, 0 );
        return arr;
      }
      set
      {
        if ( value == null )
          throw new ArgumentNullException( "[TestScriptFrame.CurrentReadValuesArr.Set]: The value cannot be null" );
        _currentReadValues.Clear();
        _currentReadValues.AddRange( value );
      }
    }

    /// <summary>
    /// Gets/Sets the pressure channels master data
    /// It must be ignored in XML serialization
    /// </summary>
    [XmlIgnoreAttribute,
    DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public List<float> MasterPressureReadValues
    {
      get { return _pressureMasterValues; }
      set
      {
        if ( value == null )
          throw new ArgumentNullException( "[TestScriptFrame.MasterPressureReadValues.Set]: The value cannot be null" );
        _pressureMasterValues = value;
      }
    }

    /// <summary>
    /// Gets/Sets the pressure channels master data
    /// This shall not be used for data access
    /// This is added only to provide XML synchronization
    /// </summary>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public float[] MasterPressureReadValuesArr
    {
      get
      {
        float[] arr = new float[_pressureMasterValues.Count];
        _pressureMasterValues.CopyTo( arr, 0 );
        return arr;
      }
      set
      {
        if ( value == null )
          throw new ArgumentNullException( "[TestScriptFrame.MasterPressureReadValuesArr.Set]: The value cannot be null" );
        _pressureMasterValues.Clear();
        _pressureMasterValues.AddRange( value );
      }
    }

    /// <summary>
    /// Gets/sets the name of this frame (gear name)
    /// </summary>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public string FrameName
    {
      get { return mFrameName; }
      set
      {
        if ( value == null )
          value = string.Empty;// throw new ArgumentNullException("Gear name value cannot be null");
        mFrameName = value;
      }
    }

    /// <summary>
    /// Gets/Sets if DAQ data should be collected after this frame
    /// </summary>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public bool AcquireData
    {
      get { return mAcquireData; }
      set
      {
        mAcquireData = value;
      }
    }

    /// <summary>
    /// Gets/Sets if this frame should be stepped through in manual mode
    /// </summary>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public bool IsPassThrough
    {
      get { return mIsPassThrough; }
      set
      {
        mIsPassThrough = value;
      }
    }

    /// <summary>
    /// Gets/Sets the ID of the last packet from this frame, must be set externally
    /// </summary>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public UInt32 CriticalID
    {
      get { return mCriticalID; }
      set
      {
        mCriticalID = value;
      }
    }

    /// <summary>
    /// Gets/Sets if pass/fail conditions should be checked at the end of this frame (is set internally basing on mPressure min/max Values)
    /// </summary>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public bool CheckPassFailConditions
    {
      get { return mCheckPassFailConditions; }
    }

    //Analog output 1 value for this frame. 0-10.00V
    public float AnalogOutput1Value
    {
      get { return mAnalogOutput1Value; }
      set
      {
        if (value < 0)
        {
          value = 0;
        }
        if (value > 10.0f)
        {
          value = 10.0f;
        }
        mAnalogOutput1Value = value;
      }
    }

    //Analog output 2 value for this frame. 0-10.00V
    public float AnalogOutput2Value
    {
      get { return mAnalogOutput2Value; }
      set
      {
        if (value < 0)
        {
          value = 0;
        }
        if (value > 10.0f)
        {
          value = 10.0f;
        }
        mAnalogOutput2Value = value;
      }
    }

    //Dyno motor RPM value for this frame
    public float DynoMotorRPM
    {
      get { return mDynoMotorRPM; }
      set { mDynoMotorRPM = value; }
    }

    //Dyno load current value for this frame
    public float DynoLoadCurrent
    {
      get { return mDynoLoadCurrent; }
      set { mDynoLoadCurrent = value; }
    }

    /// <summary>
    /// Marker if this is a part of the loop
    /// </summary>
    public bool IsPartOfTheLoop
    {
      get { return mIsPartOfTheLoop; }
      set
      {
        mIsPartOfTheLoop = value;
      }
    }

    /// <summary>
    /// Gear number for Zf6 switched gearboxes. 
    /// -1 to 6 are allowed with 0.5 increment to denote the in-between-gears state.
    /// </summary>
    public float Zf6GearNumber
    {
      get { return _zf6GearNumber; }
      set
      {
        _zf6GearNumber = value;
        if (_zf6GearNumber < -1.0f)
        {
          _zf6GearNumber = -1;
        }
        if (_zf6GearNumber > 6.0f)
        {
          _zf6GearNumber = 6;
        }
      }
    }

    public float EnigmaGearNumber
        {
        get { return mEnigmaGearNumber; }
        set
        {
                mEnigmaGearNumber = value;
            if (mEnigmaGearNumber < -1.0f)
            {
                    mEnigmaGearNumber = -1;
            }
            if (mEnigmaGearNumber > 6.0f)
            {
                    mEnigmaGearNumber = 6;
            }
        }
    }

        /// <summary>
        /// Zf6 EDS5 value for this frame
        /// </summary>
        public int EnigmaEDS5Value
    {
      get { return _zf6EDS5Value; }
      set { _zf6EDS5Value = value; }
    }

    /// <summary>
    /// Zf6 EDS6 value for this frame
    /// </summary>
    public int EnigmaEDS6Value
    {
      get { return _zf6EDS6Value; }
      set { _zf6EDS6Value = value; }
    }

    public UInt16 CANBUS_EngineSpeedValue = 0;

    public UInt16 CANBUS_TPS = 0;

    public UInt16 SSEMU_InputSpeedValue = 0;

    public UInt16 SSEMU_OutputSpeedValue = 0;

    public UInt16 CANBUS_TorqueValue = 0;

    #endregion Properties



    #region Methods



    #endregion Methods



  }

}