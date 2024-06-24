using System;
using System.Collections.Generic;
using System.Text;

namespace GST.Gearshift.Components.Interfaces.USB
{

  /// <remarks>
  /// Class describing single display channel properties
  /// </remarks>  
  [Serializable]
  public class DisplayChannel
  {



    #region Constants



    #endregion  Constants



    #region Private fields

    private string mLabel = "Channel";
    private float mMinValue = 0.0f;
    private float mMaxValue = 1.0f;
    private float mValue = 0.0f;
    private string mUnitName = "[Unit]";
    private int mInputChannelIndex = 0;
    private bool mIsUpperChannel = false;

    private int mInitTestDutyCycle = 0;
    private float mNominalCurrent = 1.0f;
    private uint mNominalCurrentTolerance = 20;
    private float mInitTestReadoutValue = 0;
    private bool mInitTestFailed = false;

    private bool mIsSliderControlled = false;
    private int mSliderControlValue = 0;

    private bool mIsTempGauge = false;
    private bool mIsFlowGauge = false;
    private bool mIsTorqueGauge = false;
    private bool _IsPressureSwitchGauge;
    private bool _IsPressureSwitchInvertedLogic;

    #endregion Private fields



    #region Constructors & finalizer



    #endregion Constructors & finalizer



    #region Events

    /// <summary>
    /// Fired when manual drive has been enabled/disabled
    /// </summary>
    public event EventHandler ManualDriveStatusChanged;

    /// <summary>
    /// Fired if manual drive is enabled when its value has been changed
    /// </summary>
    public event EventHandler ManualDriveValueChanged;

    #endregion Events



    #region Properties

    /// <summary>
    /// Label of the channel
    /// </summary>  
    public string Label
    {
      get { return mLabel; }
      set
      {
        if (value == null)
        {
          throw new NullReferenceException("[DisplayChannel.Label.Set] : The label string cannot be null");
        }
        mLabel = value;
      }
    }

    /// <summary>
    /// The minimal value the channel will display
    /// </summary>  
    public float MinValue
    {
      get { return mMinValue; }
      set
      {
//         if (value >= mMaxValue)
//         {
//           throw new ArgumentException("[DisplayChannel.MinValue.Set] : MinValue cannot be greater than or equal to MaxValue");
//         }
//         else
//         {
          mMinValue = value;
          mValue = mMinValue;
//        }
      }
    }

    /// <summary>
    /// The maximal value the channel will display
    /// </summary>  
    public float MaxValue
    {
      get { return mMaxValue; }
      set
      {
//         if (value <= mMinValue)
//         {
//           throw new ArgumentException("[DisplayChannel.MaxValue.Set] : MaxValue cannot be less than or equal to MinValue");
//         }
//         else
//        {
          mMaxValue = value;
          mValue = mMinValue;
//        }
      }
    }

    /// <summary>
    /// Current value channel should display
    /// </summary>  
    public float Value
    {
      get { return mValue; }
      set
      {
        if (value < mMinValue)
        {
          mValue = mMinValue;
          return;
        }
        if (value > mMaxValue)
        {
          mValue = mMaxValue;
          return;
        }
        mValue = value;
        //if (ValueChangedEvent != null)
        //  ValueChangedEvent(this, EventArgs.Empty);
      }
    }

    /// <summary>
    /// The unit name i.e. [A] [Bar] etc
    /// </summary>  
    public string UnitName
    {
      get { return mUnitName; }
      set
      {
        if (value == null)
        {
          throw new NullReferenceException("[DisplayChannel.UnitName.Set] : The unit name string cannot be null");
        }
        mUnitName = value;
      }
    }

    /// <summary>
    /// Index describing the source channel number
    /// </summary>  
    public int InputChannelIndex
    {
      get { return mInputChannelIndex; }
      set
      {
        if (value < 0)
        {
          throw new ArgumentException("[DisplayChannel.InputChannelIndex.Set] : Input channel index cannot be less than zero");
        }
        mInputChannelIndex = value;
      }
    }

    /// <summary>
    /// Gets/sets if the channel is upper solenoid
    /// </summary>
    public bool IsUpperChannel
    {
      get { return mIsUpperChannel; }
      set {mIsUpperChannel = value;}
    }

    /// <summary>
    /// Initial solenoid test duty cycle to apply
    /// </summary>
    public int InitTestDutyCycle
    {
      get { return mInitTestDutyCycle; }
      set 
      {
        if (value < 0 && value > 100)
          throw new ArgumentException("[DisplayChannel.InitTestDutyCycle.Set] : Duty cycle must be between 0 and 100%");
        mInitTestDutyCycle = value; 
      }
    }

    /// <summary>
    /// Initial solenoid test minimal expected current value
    /// </summary>
    public float NominalCurrent
    {
      get { return mNominalCurrent; }
      set 
      {
        mNominalCurrent = value; 
      }
    }

    /// <summary>
    /// Initial solenoid test maximal expected current value
    /// </summary>
    public uint NominalCurrentTolerance
    {
      get { return mNominalCurrentTolerance; }
      set 
      {
        mNominalCurrentTolerance = value; 
      }
    }

    /// <summary>
    /// Initial solenoid test read out value
    /// </summary>
    public float InitTestReadoutValue
    {
      get { return mInitTestReadoutValue; }
      set { mInitTestReadoutValue = value; }
    }

    /// <summary>
    /// Initial solenoid test failure flag
    /// </summary>
    public bool InitTestFailed
    {
      get { return mInitTestFailed; }
      set { mInitTestFailed = value; }
    }

    /// <summary>
    /// If this solenoid is controlled with slider in manual mode
    /// </summary>
    public bool IsSliderControlled
    {
      get { return mIsSliderControlled; }
      set 
      {
        if (mIsSliderControlled != value)
        {
          mIsSliderControlled = value;
          if (ManualDriveStatusChanged != null)
          {
            ManualDriveStatusChanged(this, EventArgs.Empty);
          }
        }
      }
    }

    /// <summary>
    /// Manual control value
    /// </summary>
    public int SliderControlValue
    {
      get { return mSliderControlValue; }
      set 
      { 
        mSliderControlValue = value;
        if (ManualDriveValueChanged != null)
        {
          ManualDriveValueChanged(this, EventArgs.Empty);
        }
      }
    }

    public bool IsTempGauge
    {
      get { return mIsTempGauge; }
      set
      {
        if (value == true)
        {
          mIsFlowGauge = false;
          mIsTorqueGauge = false;
        }
        mIsTempGauge = value;
      }
    }

    public bool IsFlowGauge
    {
      get { return mIsFlowGauge; }
      set
      {
        if (value == true)
        {
          mIsTempGauge = false;
          mIsTorqueGauge = false;
        }
        mIsFlowGauge = value;
      }
    }

    public bool IsTorqueGauge
    {
      get { return mIsTorqueGauge; }
      set
      {
        if (value == true)
        {
          mIsTempGauge = false;
          mIsFlowGauge = false;
        }
        mIsTorqueGauge = value;
      }
    }

    #endregion Properties



    #region Methods



    #endregion Methods



  }
}
