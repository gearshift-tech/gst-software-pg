using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Soko.CanCave.Components.Interfaces
{
  public class TCU_StatusData_GM6Txx
  {
    public enum GearLeverPositionEnum
    {
      [Description("Between Ranges")]
      BetweenRanges = 0x0,

      [Description("Park Range")]
      ParkRange = 0x1,

      [Description("Reverse Range")]
      ReverseRange = 0x2,

      [Description("Neutral Range")]
      NeutralRange = 0x3,

      [Description("Drive 1 Range")]
      Drive1Range = 0x4,

      [Description("Drive 2 Range")]
      Drive2Range = 0x5,

      [Description("Drive 3 Range")]
      Drive3Range = 0x6,

      [Description("Unknown position")]
      Unknown = 0xF
    }

    public enum CommandedGearEnum
    {
      [Description("NotSupported")]
      NotSupported = 0x0,

      [Description("First")]
      First = 0x1,

      [Description("Second")]
      Second = 0x2,

      [Description("Third")]
      Third = 0x3,

      [Description("Fourth")]
      Fourth = 0x4,

      [Description("Fifth")]
      Fifth = 0x5,

      [Description("Sixth")]
      Sixth = 0x6,

      [Description("Seventh")]
      Seventh = 0x7,

      [Description("Eighth")]
      Eighth = 0x8,

      [Description("Nineth")]
      Nineth = 0x9,

      [Description("EVT Mode 1")]
      EVTMode1 = 0xA,

      [Description("EVT Mode 2")]
      EVTMode2 = 0xB,

      [Description("CVT Forward")]
      CVTForward = 0xC,

      [Description("Neutral")]
      Neutral = 0xD,

      [Description("Reverse")]
      Reverse = 0xE,

      [Description("Park")]
      Park = 0xF
    }


    public double Solenoid_PC1 = 0;
    public double Solenoid_PC2 = 0;
    public double Solenoid_PC3 = 0;
    public double Solenoid_PC4 = 0;
    public double Solenoid_PC5 = 0;
    public double Solenoid_PC6 = 0;
    public bool Solenoid_Shift1 = false;
    public bool Solenoid_Shift2 = false;

    public bool Switch_Pressure1 = false;
    public bool Switch_Pressure3 = false;
    public bool Switch_Pressure4 = false;
    public bool Switch_Pressure5 = false;
    public bool Switch_Brake = false;

    public double Speed_Input = 0;
    public double Speed_Output = 0;
    public double Speed_Slip = 0;
    public double GearRatio = 0.0f;

    public double TcuTemperature = 0;
    public double FluidTemperature = 0;
    public double EngineCoolandTemp = 85;
    public double IgnitionVoltage = 0;
    public double vehicleSpeed = 0;
    public double diagTccSlipSpeed = 0;
    public double diagCalculatedTPS = 0;

    public GearLeverPositionEnum GearLeverPosition = GearLeverPositionEnum.Unknown;
    public CommandedGearEnum CommandedGear_Actual = CommandedGearEnum.NotSupported;
    public CommandedGearEnum CommandedGear_Desired = CommandedGearEnum.NotSupported;
  }
}
