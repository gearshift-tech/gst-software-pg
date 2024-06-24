using System;
using System.Collections.Generic;
using System.Text;

namespace GST.Gearshift.Components.Interfaces.USB//GST.Gearshift.Components.Interfaces.USB
{
  [Serializable]
  public class CAN_GearboxSensorsConfig
  {

    #region Constants



    #endregion  Constants


    #region Public fields

    // Engine RPM
    public CanEntry EngineRPMEntry = new CanEntry();

    // Gearbox input shaft RPM
    public CanEntry InputShaftRPMEntry = new CanEntry();

    // Gearbox output shaft RPM
    public CanEntry OutputShaftRPMEntry = new CanEntry();

    // Throttle position sensor
    public CanEntry TPSEntry = new CanEntry();

    public CanEntry EngineLoadEntry = new CanEntry();

    // Temperature
    public CanEntry TempEntry = new CanEntry();

    // LockUp
    public UInt32 mLUID = 0;
    public UInt32 mLUBit = 0;
    public UInt32 mLUOn = 0;
    public UInt32 mLUOff = 0;

    // Brake
    public UInt32 mBrakeID = 0;
    public UInt32 mBrakeBit = 0;
    public UInt32 mBrakeOn = 0;

    // Gear
    public UInt32 mGearID = 0;

    #endregion Public fields


    #region Constructors & finalizer

    public CAN_GearboxSensorsConfig()
    {
      InputShaftRPMEntry.EntryName = "Input shaft RPM";
      OutputShaftRPMEntry.EntryName = "Output shaft RPM";
      TPSEntry.EntryName = "TPS";
      TempEntry.EntryName = "Temperature";
    }

    #endregion Constructors & finalizer


    #region Events



    #endregion Events


    #region Properties



    #endregion Properties


    #region Methods



    #endregion Methods

  }
}
