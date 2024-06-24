using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GST.Gearshift.Components.Utilities
{
  public class MeasurementUnit
  {

    public enum ValueType
    {
      Pressure = 0x00,
      Flow = 0x01,
      Temperature = 0x02,
      Torque = 0x03,
      InputSpeed = 0x04,
      OutputSpeed = 0x05,
      GearRatio = 0x06,
      PressureSwitch = 0x07
    }


    public static float ConvertAIValueBaseUnitToUserUnit(float value, MeasurementUnit.ValueType vt)
    {
      switch (vt)
      {
        case MeasurementUnit.ValueType.Pressure:
        default:
          {
            switch (GST.Gearshift.Components.Utilities.Settings.Instance.UserPressureUnit)
            {
              case MeasurementUnit.PressureUnit.at:
                {
                  return Pressure_bar_to_at(value);
                }
              case MeasurementUnit.PressureUnit.bar:
              default:
                {
                  return value;
                }
              case MeasurementUnit.PressureUnit.kPa:
                {
                  return Pressure_bar_to_kPa(value);
                }
              case MeasurementUnit.PressureUnit.PSI:
                {
                  return Pressure_bar_to_PSI(value);
                }
            }
          }
        case MeasurementUnit.ValueType.Flow:
          {
            switch (GST.Gearshift.Components.Utilities.Settings.Instance.UserFlowUnit)
            {
              case MeasurementUnit.FlowUnit.CFM:
                {
                  return Flow_CMH_to_CFM(value);
                }
              case MeasurementUnit.FlowUnit.cmph:
              default:
                {
                  return value;
                }
              case MeasurementUnit.FlowUnit.GPH:
                {
                  return Flow_CMH_to_GPH(value);
                }
              case MeasurementUnit.FlowUnit.GPM:
                {
                  return Flow_CMH_to_GPM(value);
                }
              case MeasurementUnit.FlowUnit.LPM:
                {
                  return Flow_CMH_to_lpm(value);
                }
            }
          }
        case MeasurementUnit.ValueType.Temperature:
          {
            switch (GST.Gearshift.Components.Utilities.Settings.Instance.UserTemperatureUnit)
            {
              case MeasurementUnit.TemperatureUnit.Celsius:
              default:
                {
                  return value;
                }
              case MeasurementUnit.TemperatureUnit.Fahrenheit:
                {
                  return Temperature_C_to_F(value);
                }
              case MeasurementUnit.TemperatureUnit.Kelvin:
                {
                  return Temperature_C_to_K(value);
                }
            }
          }
        case MeasurementUnit.ValueType.Torque:
          {
            switch (GST.Gearshift.Components.Utilities.Settings.Instance.UserTorqueUnit)
            {
              case MeasurementUnit.TorqueUnit.Nm:
              default:
                {
                  return value;
                }
              case MeasurementUnit.TorqueUnit.ft_lbf:
                {
                  return Torque_Nm_to_ftlbf(value);
                }
            }
          }
        case MeasurementUnit.ValueType.InputSpeed:
          {
            return value;
          }
        case MeasurementUnit.ValueType.OutputSpeed:
          {
            return value;
          }
        case MeasurementUnit.ValueType.GearRatio:
          {
            return value;
          }
      }
    }

    public static float ConvertAIValueUserUnitToBaseUnit(float value, MeasurementUnit.ValueType vt)
    {
      switch (vt)
      {
        case MeasurementUnit.ValueType.Pressure:
        default:
          {
            switch (GST.Gearshift.Components.Utilities.Settings.Instance.UserPressureUnit)
            {
              case MeasurementUnit.PressureUnit.at:
                {
                  return Pressure_at_to_bar(value);
                }
              case MeasurementUnit.PressureUnit.bar:
              default:
                {
                  return value;
                }
              case MeasurementUnit.PressureUnit.kPa:
                {
                  return Pressure_kPa_to_bar(value);
                }
              case MeasurementUnit.PressureUnit.PSI:
                {
                  return Pressure_PSI_to_bar(value);
                }
            }
          }
        case MeasurementUnit.ValueType.Flow:
          {
            switch (GST.Gearshift.Components.Utilities.Settings.Instance.UserFlowUnit)
            {
              case MeasurementUnit.FlowUnit.CFM:
                {
                  return Flow_CFM_to_CMH(value);
                }
              case MeasurementUnit.FlowUnit.cmph:
              default:
                {
                  return value;
                }
              case MeasurementUnit.FlowUnit.GPH:
                {
                  return Flow_GPH_to_CMH(value);
                }
              case MeasurementUnit.FlowUnit.GPM:
                {
                  return Flow_GPM_to_CMH(value);
                }
              case MeasurementUnit.FlowUnit.LPM:
                {
                  return Flow_lpm_to_CMH(value);
                }
            }
          }
        case MeasurementUnit.ValueType.Temperature:
          {
            switch (GST.Gearshift.Components.Utilities.Settings.Instance.UserTemperatureUnit)
            {
              case MeasurementUnit.TemperatureUnit.Celsius:
              default:
                {
                  return value;
                }
              case MeasurementUnit.TemperatureUnit.Fahrenheit:
                {
                  return Temperature_F_to_C(value);
                }
              case MeasurementUnit.TemperatureUnit.Kelvin:
                {
                  return Temperature_K_to_C(value);
                }
            }
          }
        case MeasurementUnit.ValueType.Torque:
          {
            switch (GST.Gearshift.Components.Utilities.Settings.Instance.UserTorqueUnit)
            {
              case MeasurementUnit.TorqueUnit.Nm:
              default:
                {
                  return value;
                }
              case MeasurementUnit.TorqueUnit.ft_lbf:
                {
                  return Torque_ftlbf_to_Nm(value);
                }
            }
          }
        case MeasurementUnit.ValueType.InputSpeed:
          {
            return value;
          }
        case MeasurementUnit.ValueType.OutputSpeed:
          {
            return value;
          }
        case MeasurementUnit.ValueType.GearRatio:
          {
            return value;
          }
      }
    }


    public static string GetFlowUserUnitString()
    {
      switch (Utilities.Settings.Instance.UserFlowUnit)
      {
        case MeasurementUnit.FlowUnit.CFM:
          {
            return "CFM";
          }
        case MeasurementUnit.FlowUnit.cmph:
          {
            return "m^3/h";
          }
        case MeasurementUnit.FlowUnit.GPH:
          {
            return "GPH";
          }
        case MeasurementUnit.FlowUnit.GPM:
          {
            return "GPM";
          }
        case MeasurementUnit.FlowUnit.LPM:
          {
            return "l/min";
          }
        default:
          {
            return "?";
          }
      }
    }
    public static string GetPressureUserUnitString()
    {
      switch (Utilities.Settings.Instance.UserPressureUnit)
      {
        case MeasurementUnit.PressureUnit.at:
          {
            return "at";
          }
        case MeasurementUnit.PressureUnit.bar:
          {
            return "bar";
          }
        case MeasurementUnit.PressureUnit.kPa:
          {
            return "kPa";
          }
        case MeasurementUnit.PressureUnit.PSI:
          {
            return "PSI";
          }
        default:
          {
            return "?";
          }
      }
    }
    public static string GetTemperatureUserUnitString()
    {
      switch (Utilities.Settings.Instance.UserTemperatureUnit)
      {
        case MeasurementUnit.TemperatureUnit.Celsius:
          {
            return "°C";
          }
        case MeasurementUnit.TemperatureUnit.Fahrenheit:
          {
            return "°F";
          }
        case MeasurementUnit.TemperatureUnit.Kelvin:
          {
            return "K";
          }
        default:
          {
            return "?";
          }
      }
    }
    public static string GetTorqueUserUnitString()
    {
      switch (Utilities.Settings.Instance.UserTorqueUnit)
      {
        case MeasurementUnit.TorqueUnit.ft_lbf:
          {
            return "ftlb";
          }
        case MeasurementUnit.TorqueUnit.Nm:
          {
            return "Nm";
          }
        default:
          {
            return "?";
          }
      }
    }
    public static string GetSpeedUserUnitString()
    {
      return "RPM";
    }
    #region PRESSURE

    public enum PressureUnit
    {
      bar = 0x00,
      PSI = 0x01,
      kPa = 0x02,
      at = 0x03
    }

    public static float Pressure_bar_to_at(float value)
    {
      return value * 1.0197f;
    }
    public static float Pressure_at_to_bar(float value)
    {
      return value / 1.0197f;
    }

    public static float Pressure_bar_to_kPa(float value)
    {
      return value * 100.0f;
    }
    public static float Pressure_kPa_to_bar(float value)
    {
      return value / 100.0f;
    }

    public static float Pressure_bar_to_PSI(float value)
    {
      return value * 14.50377f;
    }
    public static float Pressure_PSI_to_bar(float value)
    {
      return value / 14.50377f;
    }
    #endregion

    #region TEMPERATURE

    public enum TemperatureUnit
    {
      Celsius = 0x00,
      Kelvin = 0x01,
      Fahrenheit = 0x02
    }

    public static float Temperature_C_to_K(float value)
    {
      return value + 273.0f;
    }
    public static float Temperature_K_to_C(float value)
    {
      return value - 273.0f;
    }

    public static float Temperature_C_to_F(float value)
    {
      return value * 1.8f + 32.0f;
    }
    public static float Temperature_F_to_C(float value)
    {
      return (value - 32.0f) / 1.8f;
    }
    #endregion

    #region TORQUE

    public enum TorqueUnit
    {
      Nm = 0x00,
      ft_lbf = 0x02
    }

    public static float Torque_Nm_to_ftlbf(float value)
    {
      return value * 1.3558179483314004f;
    }
    public static float Torque_ftlbf_to_Nm(float value)
    {
      return value / 1.3558179483314004f;
    }
    #endregion

    #region FLOW

    public enum FlowUnit
    {
      LPM = 0x00,
      cmph = 0x01,
      CFM = 0x02,
      GPM = 0x03,
      GPH = 0x04
    }

    public static float Flow_CMH_to_lpm(float value)
    {
      return (value * 100.0f) / 6.0f;
    }
    public static float Flow_lpm_to_CMH(float value)
    {
      return (value * 6.0f) / 100.0f;
    }

    public static float Flow_CMH_to_CFM(float value)
    {
      return value * 0.588577779f;
    }
    public static float Flow_CFM_to_CMH(float value)
    {
      return value / 0.588577779f;
    }

    public static float Flow_CMH_to_GPM(float value)
    {
      return value * 4.402867539f;
    }
    public static float Flow_GPM_to_CMH(float value)
    {
      return value / 4.402867539f;
    }

    public static float Flow_CMH_to_GPH(float value)
    {
      return value * 264.172052358f;
    }
    public static float Flow_GPH_to_CMH(float value)
    {
      return value / 264.172052358f;
    }
    #endregion


  }
}
