using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GST.Gearshift.Components;
using GST.Gearshift.Components.Utilities;

namespace GST.Gearshift.Components.Interfaces.USB
{
  partial class GearShiftUsb
  {
    /// <summary>
    /// Definition of analogue input channel class
    /// </summary>
    [Serializable]
    public class AIChannel
    {

      private MeasurementUnit.ValueType _inputValueType = MeasurementUnit.ValueType.Pressure;
      private string _label = string.Empty;
      private float _minValue = 0.0f;
      private float _maxValue = 0.0f;
      private int _inputIndex = 0;


      public MeasurementUnit.ValueType ValueType
      {
        get { return _inputValueType; }
        set { _inputValueType = value; }
      }

      public int InputIndex
      {
        get { return _inputIndex; }
        set { _inputIndex = value; }
      }

      public string Label
      {
        get { return _label; }
        set { _label = value; }
      }

      public string UnitText
      {
        get
        {
          switch (_inputValueType)
          {
            case MeasurementUnit.ValueType.Flow:
              {
                return MeasurementUnit.GetFlowUserUnitString();
              }
            case MeasurementUnit.ValueType.Pressure:
              {
                return MeasurementUnit.GetPressureUserUnitString();
              }
            case MeasurementUnit.ValueType.Temperature:
              {
                return MeasurementUnit.GetTemperatureUserUnitString();
              }
            case MeasurementUnit.ValueType.Torque:
              {
                return MeasurementUnit.GetTorqueUserUnitString();
              }
            case MeasurementUnit.ValueType.InputSpeed:
            case MeasurementUnit.ValueType.OutputSpeed:
              {
                return MeasurementUnit.GetSpeedUserUnitString();
              }
            default:
              {
                return string.Empty;
              }
          }
        }
      }

      public float MinValueBaseUnit
      {
        get { return _minValue; }
        set { _minValue = value; }
      }

      public float MaxValueBaseUnit
      {
        get { return _maxValue; }
        set { _maxValue = value; }
      }

      // Do not serialize this field, the proper value is serialized as a base value.
      [System.Xml.Serialization.XmlIgnore]
      public float MinValueUserUnit
      {
        get 
        {
          return MeasurementUnit.ConvertAIValueBaseUnitToUserUnit(MinValueBaseUnit, this.ValueType);
        }
        set 
        {
          _minValue = MeasurementUnit.ConvertAIValueUserUnitToBaseUnit(value, this.ValueType);
        }
      }

      // Do not serialize this field, the proper value is serialized as a base value.
      [System.Xml.Serialization.XmlIgnore]
      public float MaxValueUserUnit
      {
        get
        {
          return MeasurementUnit.ConvertAIValueBaseUnitToUserUnit(MaxValueBaseUnit, this.ValueType);
        }
        set 
        {
          _maxValue = MeasurementUnit.ConvertAIValueUserUnitToBaseUnit(value, this.ValueType);
        }
      }





    }
  }
}
