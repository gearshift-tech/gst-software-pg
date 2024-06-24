using System;
using System.Collections.Generic;
using System.Text;

namespace GST.Gearshift.Components.Interfaces.USB
{
  public class OBD_ECU
  {
    // Freeze frame data
    public float freezeFrameCoolantTemp       = -2048;
    public float freezeFrameEngineSpeed       = -2048;
    public float freezeFrameVehicleSpeed      = -2048;

    // Live data
    public float absoluteTPS                  = -2048;
    public float rpm                          = -2048;
    public float velocity                     = -2048;
    public float calcLoad                     = -2048;
    public float ignTimAdvCyl1                = -2048;
    public float fuelSysStat                  = -2048;
    public float fuelSysStat2                 = -2048;

    // Flows and pressures
    public float maf                          = -2048;
    public float intakePress                  = -2048;
    public float fuelPress                    = -2048;

    // Temperatures
    public float coolantTemp                  = -2048;
    public float intakeTemp                   = -2048;

    // Continous monitoring tests
    public int testMisfire                    = -1;
    public int testFuelSystem                 = -1;
    public int testComponents                 = -1;

    // Non-continous monitoring tests
    public int testEvaporative                = -1;
    public int testCatalyst                   = -1;

    // Trouble codes
    public int troubleCodesCount              = -1;
    public int milStatus                      = -1;
    public List<OBD_TroubleCode> troubleCodes = new List<OBD_TroubleCode>( 0 );

    public string HEADER = string.Empty;
    public bool isCanEcu = true;
    public bool isXtdCan = false;

  }
}
