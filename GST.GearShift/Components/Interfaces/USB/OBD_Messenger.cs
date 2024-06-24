using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GST.Gearshift.Components.Interfaces.USB
{
  public class OBD_Messenger
  {

    #region Constants

    //Mode description: Show current data
    public readonly byte msg_Mode1                          = 0x01;
    //Mode description: Show freeze frame data
    public readonly byte msg_Mode2                          = 0x02;
    //Mode description: Show stored Diagnostic Trouble Codes
    public readonly byte msg_Mode3                          = 0x03;
    //Mode description: Clear Diagnostic Trouble Codes and stored values
    public readonly byte msg_Mode4                          = 0x04;
    //Mode description: Test results, oxygen sensor monitoring (non CAN only)
    public readonly byte msg_Mode5                          = 0x05;
    //Mode description: Test results, other component/system monitoring (Test results, oxygen sensor monitoring for CAN only)
    public readonly byte msg_Mode6                          = 0x06;
    //Mode description: Show pending Diagnostic Trouble Codes (detected during current or last driving cycle)
    public readonly byte msg_Mode7                          = 0x07;
    //Mode description: Control operation of on-board component/system
    public readonly byte msg_Mode8                          = 0x08;
    //Mode description: Request vehicle information
    public readonly byte msg_Mode9                          = 0x09;

    //Command description: Monitor status since DTCs cleared. (Includes malfunction indicator lamp (MIL) status and number of DTCs.)
    public readonly byte msg_Mode1_GetMILandDTCCount        = 0x01;
    //Command description: 	Calculated engine load value
    public readonly byte msg_Mode1_GetEngineLoad            = 0x04;
    //Command description: Engine coolant temperature
    public readonly byte msg_Mode1_GetEngineCoolantTemp     = 0x05;
    //Command description: Engine RPM
    public readonly byte msg_Mode1_GetEgineRPM              = 0x0C;
    //Command description: Vehicle speed
    public readonly byte msg_Mode1_GetVehicleSpeed          = 0x0D;
    //Command description: Throttle position
    public readonly byte msg_Mode1_GetTPS                   = 0x11;
    //Command description: Run time since engine start
    public readonly byte msg_Mode1_GetRTSES                 = 0x1F;
    //Command description: Ambient air temperature
    public readonly byte msg_Mode1_GetAmbientTemp           = 0x46;













    #endregion  Constants


    #region Private fields

    public OBD_TroubleCodesSet mTroubleCodesSet = new OBD_TroubleCodesSet();

    // Device to talk with
    private GearShiftUsb mDevice = null;// = new GearShiftUsb();
    private bool mElmProperlyInitialized = false;


    // Freeze frame data
    public float freezeFrameCoolantTemp = -2048;
    public float freezeFrameEngineSpeed = -2048;
    public float freezeFrameVehicleSpeed = -2048;

    // Live data
    public float absoluteTPS = -2048;
    public float rpm = -2048;
    public float velocity = -2048;
    public float calcLoad = -2048;
    public float ignTimAdvCyl1 = -2048;
    public float fuelSysStat = -2048;
    public float fuelSysStat2 = -2048;

    // Flows and pressures
    public float maf = -2048;
    public float intakePress = -2048;
    public float fuelPress = -2048;

    // Temperatures
    public float coolantTemp = -2048;
    public float intakeTemp = -2048;
    public float ambientTemp = -2048;

    // On board tests
    public int testMisfire            = -1;
    public int testFuelSystem         = -1;
    public int testComponents         = -1;
    public int testReserved           = -1;
    public int testCatalyst           = -1;
    public int testHeatedCatalyst     = -1;
    public int testEvaporative        = -1;
    public int testSecondaryAirSystem = -1;
    public int testACRefrigerant      = -1;
    public int testOxygenSensor       = -1;
    public int testOxygenSensorHeater = -1;
    public int testEGRSystem          = -1;

    // Trouble codes
    public int troubleCodesCount = -1;
    public int milStatus = -1;

    // VIN
    public string VIN = string.Empty;




    #endregion Private fields


    #region Constructors & finalizer

    public OBD_Messenger()
    {
      //Device = new GearShiftUsb();
      // Load the trouble codes from the application directory
      mTroubleCodesSet.LoadDirectory( Environment.GetFolderPath( Environment.SpecialFolder.CommonApplicationData ) + "\\GearShift Technologies\\GearShift\\OBD_Data\\Troublecodes");
    }

    #endregion Constructors & finalizer


    #region Events

    public delegate void SimpleDelegate();

    /// <summary>
    /// Set when Engine load value has been received
    /// </summary>
    public event EventHandler MessageEngineLoadReceived;

    /// <summary>
    /// Set when Engine coolant temperature value has been received
    /// </summary>
    public event EventHandler MessageCoolanTempReceived;

    /// <summary>
    /// Set when  value has been received
    /// </summary>
    public event EventHandler MessageEngineRPMReceived;

    /// <summary>
    /// Set when  value has been received
    /// </summary>
    public event EventHandler MessageVehicleSpeedReceived;

    /// <summary>
    /// Set when  value has been received
    /// </summary>
    public event EventHandler MessageTPSReceived;

    ///// <summary>
    ///// Set when  value has been received
    ///// </summary>
    //public event EventHandler MessageRTSESReceived;

    /// <summary>
    /// Set when  value has been received
    /// </summary>
    public event EventHandler MessageAmbientTempReceived;

    /// <summary>
    /// Set when  value has been received
    /// </summary>
    public event EventHandler MessageVINReceived;

    /// <summary>
    /// Set when  value has been received
    /// </summary>
    public event EventHandler MessageMILStatusReceived;

    /// <summary>
    /// Set when  value has been received
    /// </summary>
    public event EventHandler MessageDTCsCountReceived;

    public event EventHandler MessageStoredTroubleCodesReceived;

    //public event EventHandler MessagePendingTroubleCodesReceived;

    public event EventHandler MessageTestStatusReceived;

    public event EventHandler MessageFreezeFrameDTCReceived;
    public event EventHandler MessageFreezeFrameCoolantTempReceived;
    public event EventHandler MessageFreezeFrameEngineRPMReceived;
    public event EventHandler MessageFreezeFrameVehicleSpeedReceived;

    public event EventHandler VehicleConnected;

    public event EventHandler VehicleDisconnected;



    //public event EventHandler VehicleDisconnected;
    //public event EventHandler VehicleConnected;


    #endregion Events


    #region Properties

    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public GearShiftUsb Device
    {
      get { return mDevice; }
      set
      {
        if ( value == null )
          throw new ArgumentNullException( "OBD_Messenger.Device.Set : The value cannot be null!" );
        mDevice = value;
        mDevice.OBDDataReceivedEvent += new EventHandler( ObdMessageReceived );
      }
    }

//     /// <summary>
//     /// Latest received MIL status
//     /// </summary>
//     public bool LatestMILStatus
//     {
//       get { return mLatestMILStatus; }
//     }
//
//     /// <summary>
//     /// Latest received trouble codes count
//     /// </summary>
//     public int LatestDTCsCount
//     {
//       get { return mLatestDTCsCount; }
//     }
//
//     /// <summary>
//     /// Latest received engine load value
//     /// </summary>
//     public int LatestEngineLoad
//     {
//       get { return mLatestEngineLoad; }
//     }
//
//     /// <summary>
//     /// Latest received engine coolant temperature
//     /// </summary>
//     public int LatestEngineCoolantTemp
//     {
//       get { return mLatestEngineCoolantTemp; }
//     }
//
//     /// <summary>
//     /// Latest received engine rpm
//     /// </summary>
//     public int LatestEngineRPM
//     {
//       get { return mLatestEngineRPM; }
//     }
//
//     /// <summary>
//     /// Latest received vehicle speed
//     /// </summary>
//     public int LatestVehicleSpeed
//     {
//       get { return mLatestVehicleSpeed; }
//     }
//
//     /// <summary>
//     /// Latest received throttle position
//     /// </summary>
//     public int LatestTPS
//     {
//       get { return mLatestTPS; }
//     }
//
//     /// <summary>
//     /// Latest received run time since engine start
//     /// </summary>
//     public int LatestRTSES
//     {
//       get { return mLatestRTSES; }
//     }
//
//     //Latest received ambient temperature
//     public int LatestAmbientTemp
//     {
//       get { return mLatestAmbientTemp; }
//     }

    #endregion Properties


    #region Methods

    public void ClearTroubleCodes()
    {
      if ( !mDevice.IsConnected )
      {
        throw new InvalidOperationException( "The device must be connected first before clearing DTCs" );
      }
      if ( !mElmProperlyInitialized )
      {
        throw new InvalidOperationException( "The ELM327 chip must be properly initialized first before clearing DTCs" );
      }
      mDevice.OBDPutCmdToBfr( "04\n\r" ); // request DTCs clear
    }

    public void RequestVehicleStaticData()
    {
      if (!mDevice.IsConnected)
      {
        throw new InvalidOperationException( "The device must be connected first before requesting any vehicle static data" );
      }
      if (!mElmProperlyInitialized)
      {
        throw new InvalidOperationException( "The ELM327 chip must be properly initialized first before requesting any vehicle static data" );
      }
      milStatus = -1;
      troubleCodesCount = -1;
      mDevice.OBDPutCmdToBfr( "0101\n\r" ); // request MIL status and trouble codes count
      mTroubleCodesSet.mStoredCodes.Clear();
      mDevice.OBDPutCmdToBfr( "03\n\r" ); // request stored trouble codes (no data will be returned if no trouble codes are stored)
      mDevice.OBDPutCmdToBfr( "07\n\r" ); // request pending trouble codes
      mDevice.OBDPutCmdToBfr( "0202\n\r" ); // request freeze frame DTC
      mDevice.OBDPutCmdToBfr( "020C\n\r" ); // Request freeze frame engine RPM
      mDevice.OBDPutCmdToBfr( "020D\n\r" ); // Request freeze frame vehicle speed
      mDevice.OBDPutCmdToBfr( "0205\n\r" ); // Request freeze frame coolant temperature
      VIN = string.Empty;
      mDevice.OBDPutCmdToBfr( "0902\n\r" ); // request vehicle identification number
    }

    public void RequestVehicleDynamicData()
    {
      if ( !mDevice.IsConnected )
      {
        throw new InvalidOperationException( "The device must be connected first before requesting any vehicle dynamic data" );
      }
      if ( !mElmProperlyInitialized )
      {
        throw new InvalidOperationException( "The ELM327 chip must be properly initialized first before requesting any vehicle dynamic data" );
      }
    }

    public void RequestLiveDashboardData()
    {
      if ( !mDevice.IsConnected )
      {
        throw new InvalidOperationException( "The device must be connected first before requesting any vehicle dynamic data" );
      }
      if ( !mElmProperlyInitialized )
      {
        throw new InvalidOperationException( "The ELM327 chip must be properly initialized first before requesting any vehicle dynamic data" );
      }
      mDevice.OBDPutCmdToBfr( "010C\n\r" ); // Request engine RPM
      mDevice.OBDPutCmdToBfr( "010D\n\r" ); // Request vehicle speed
      //mDevice.OBDPutCmdToBfr( "0111\n\r" ); // Request TPS
      mDevice.OBDPutCmdToBfr( "0104\n\r" ); // Request engine load
      mDevice.OBDPutCmdToBfr( "0146\n\r" ); // Request ambient temperature
      mDevice.OBDPutCmdToBfr( "0105\n\r" ); // Request coolant temperature

    }

    public void ConnectToVehicle()
    {
      if ( !mDevice.IsConnected )
      {
        throw new InvalidOperationException( "The device must be connected first before requesting to connect to the vehicle" );
      }
      mElmProperlyInitialized = (!Convert.ToBoolean( mDevice.ConnectToVehicle() ) );
      if (mElmProperlyInitialized)
      {
        if (VehicleConnected != null)
        {
          VehicleConnected( this, EventArgs.Empty );
        }
      }
      else
      {
        if ( VehicleDisconnected != null )
        {
          VehicleDisconnected( this, EventArgs.Empty );
        }
      }
    }

    private void ObdMessageReceived(object sender, EventArgs e)
    {
      // this method very often will rise events modifying controls created on different thread, thus invocation is done by default
      //SimpleDelegate sd = new SimpleDelegate( ProcessOBDRxBuffer );
      //sd();
      ProcessOBDRxBuffer();
    }

    private bool GetBit( byte BF, int index)
    {
      if (index > 7 || index < 0)
      {
        throw new InvalidOperationException( "The index must be between 0 and 7" );
      }
      if ( ( BF & 1 << index ) != 0)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    private void ProcessOBDRxBuffer()
    {
      //int msgToSvc = mDevice.OBDRxBuffer.Count;
      for (int i = 0; i < mDevice.OBDRxBuffer.Count; i++)
      {
        UsbOBDRxData msg = mDevice.OBDRxBuffer[0];
        mDevice.OBDRxBuffer.RemoveAt( 0 );

        Console.Write( "C#  BYTE COUNT: " + msg.bytesCount.ToString() + " // " + msg.respToMode.ToString() + msg.respToPID.ToString() + " // " );
        for ( int v = 0; v < msg.bytesCount; v++ )
        {
          Console.Write( msg.bytes[v].ToString() + " " );
        }
        Console.WriteLine( " " );

        //continue;

        switch ( msg.respToMode )
        {
          case 0x01:
            {
              switch (msg.respToPID)
              {
                case 0x01: //MIL, trouble codes count, tests status
                  {
                    if ( msg.ID == 0x10 || msg.ID == 0x7E8 ) // if this is not engine control modul ignore this response
                    {
                      // There might be more than one ECU. This is a message from ONE of the ECUs, so
                      // if this ECU shows no errors does not mean the others do as well.
                      // mil status and trouble codes count is reset (both to -1 which means invalid value and that the response didn't come yet)
                      // upon the sending request moment. Only the first message can set the values to 0
                      // and is set / incremented depending on the incoming messages.
                      byte MILStat = msg.bytes[0];
                      if ( MILStat > 127 )
                      {
                        milStatus = 1;
                        if ( troubleCodesCount == -1 )// if this is the first response after request
                        {
                          troubleCodesCount = MILStat - 128;
                        }
                        else
                        {
                          troubleCodesCount += MILStat - 128;
                        }
                      }
                      else
                      {
                        if ( milStatus == -1 )
                        {
                          milStatus = 0;
                          troubleCodesCount = 0;
                        }
                      }
                      if ( MessageMILStatusReceived != null )
                      {
                        MessageMILStatusReceived( this, EventArgs.Empty );
                      }
                      if ( MessageDTCsCountReceived != null )
                      {
                        MessageDTCsCountReceived( this, EventArgs.Empty );
                      }
                      #region TESTS
                      // Check tests completion status
                      byte B = msg.bytes[1];
                      byte C = msg.bytes[2];
                      byte D = msg.bytes[3];
                      // Check misfire test availability and completion
                      if ( GetBit( B, 0 ) ) // If test available
                      {
                        if ( GetBit( B, 4 ) ) // If test incomplete
                        {
                          testMisfire = 0; // test incomplete
                        }
                        else
                        {
                          testMisfire = 1; // test ok
                        }
                      }
                      else
                      {
                        testMisfire = -1;
                      }
                      // Check Fuel System test availability and completion
                      if ( GetBit( B, 1 ) ) // If test available
                      {
                        if ( GetBit( B, 5 ) ) // If test incomplete
                        {
                          testFuelSystem = 0; // test incomplete
                        }
                        else
                        {
                          testFuelSystem = 1; // test ok
                        }
                      }
                      else
                      {
                        testFuelSystem = -1;
                      }

                      // Check Components test availability and completion
                      if ( GetBit( B, 2 ) ) // If test available
                      {
                        if ( GetBit( B, 6 ) ) // If test incomplete
                        {
                          testComponents = 0; // test incomplete
                        }
                        else
                        {
                          testComponents = 1; // test ok
                        }
                      }
                      else
                      {
                        testComponents = -1;
                      }

                      // Check Catalyst test availability and completion
                      if ( GetBit( C, 0 ) ) // If test available
                      {
                        if ( GetBit( D, 0 ) ) // If test incomplete
                        {
                          testCatalyst = 0; // test incomplete
                        }
                        else
                        {
                          testCatalyst = 1; // test ok
                        }
                      }
                      else
                      {
                        testCatalyst = -1;
                      }

                      // Check Heated Catalyst test availability and completion
                      if ( GetBit( C, 1 ) ) // If test available
                      {
                        if ( GetBit( D, 1 ) ) // If test incomplete
                        {
                          testHeatedCatalyst = 0; // test incomplete
                        }
                        else
                        {
                          testHeatedCatalyst = 1; // test ok
                        }
                      }
                      else
                      {
                        testHeatedCatalyst = -1;
                      }

                      // Check Evaporative test availability and completion
                      if ( GetBit( C, 2 ) ) // If test available
                      {
                        if ( GetBit( D, 2 ) ) // If test incomplete
                        {
                          testEvaporative = 0; // test incomplete
                        }
                        else
                        {
                          testEvaporative = 1; // test ok
                        }
                      }
                      else
                      {
                        testEvaporative = -1;
                      }

                      // Check Secondary Air System test availability and completion
                      if ( GetBit( C, 3 ) ) // If test available
                      {
                        if ( GetBit( D, 3 ) ) // If test incomplete
                        {
                          testSecondaryAirSystem = 0; // test incomplete
                        }
                        else
                        {
                          testSecondaryAirSystem = 1; // test ok
                        }
                      }
                      else
                      {
                        testSecondaryAirSystem = -1;
                      }

                      // Check AC Refrigerant test availability and completion
                      if ( GetBit( C, 4 ) ) // If test available
                      {
                        if ( GetBit( D, 4 ) ) // If test incomplete
                        {
                          testACRefrigerant = 0; // test incomplete
                        }
                        else
                        {
                          testACRefrigerant = 1; // test ok
                        }
                      }
                      else
                      {
                        testACRefrigerant = -1;
                      }

                      // Check Oxygen Sensor test availability and completion
                      if ( GetBit( C, 5 ) ) // If test available
                      {
                        if ( GetBit( D, 5 ) ) // If test incomplete
                        {
                          testOxygenSensor = 0; // test incomplete
                        }
                        else
                        {
                          testOxygenSensor = 1; // test ok
                        }
                      }
                      else
                      {
                        testOxygenSensor = -1;
                      }

                      // Check Oxygen Sensor Heater test availability and completion
                      if ( GetBit( C, 6 ) ) // If test available
                      {
                        if ( GetBit( D, 6 ) ) // If test incomplete
                        {
                          testOxygenSensorHeater = 0; // test incomplete
                        }
                        else
                        {
                          testOxygenSensorHeater = 1; // test ok
                        }
                      }
                      else
                      {
                        testOxygenSensorHeater = -1;
                      }

                      // Check misfire test availability and completion
                      if ( GetBit( C, 7 ) ) // If test available
                      {
                        if ( GetBit( D, 7 ) ) // If test incomplete
                        {
                          testEGRSystem = 0; // test incomplete
                        }
                        else
                        {
                          testEGRSystem = 1; // test ok
                        }
                      }
                      else
                      {
                        testEGRSystem = -1;
                      }

                      if ( MessageTestStatusReceived != null )
                      {
                        MessageTestStatusReceived( this, EventArgs.Empty );
                      }
                      #endregion TESTS
                    }
                    break;
                  }
                case 0x04: // Engine load
                  {
                    if (msg.bytesCount != 1)
                    {
                      break;
                    }
                    calcLoad = (int)(msg.bytes[0] * 100.0f / 255.0f);
                    if (MessageEngineLoadReceived != null)
                    {
                      MessageEngineLoadReceived( this, EventArgs.Empty );
                    }
                    break;
                  }
                case 0x05: // Coolant temperature
                  {
                    if ( msg.bytesCount != 1 )
                    {
                      break;
                    }
                    coolantTemp = (int)( msg.bytes[0] - 40 );
                    if ( MessageCoolanTempReceived != null )
                    {
                      MessageCoolanTempReceived( this, EventArgs.Empty );
                    }
                    break;
                  }
                case 0x0C: // Engine RPM
                  {
                    if ( msg.bytesCount != 2 )
                    {
                      break;
                    }
                    rpm = (int)( ( (msg.bytes[0] << 8) + msg.bytes[1] ) / 4.0f );
                    if ( MessageEngineRPMReceived != null )
                    {
                      MessageEngineRPMReceived( this, EventArgs.Empty );
                    }
                    break;
                  }
                case 0x0D: // Vehicle speed
                  {
                    if ( msg.bytesCount != 1 )
                    {
                      break;
                    }
                    velocity = (int)( msg.bytes[0] );
                    if ( MessageVehicleSpeedReceived != null )
                    {
                      MessageVehicleSpeedReceived( this, EventArgs.Empty );
                    }
                    break;
                  }
                case 0x11: // Throttle position
                  {
                    if ( msg.bytesCount != 1 )
                    {
                      break;
                    }
                    absoluteTPS = (int)( msg.bytes[0] * 100.0f / 255.0f );
                    if ( MessageTPSReceived != null )
                    {
                      MessageTPSReceived( this, EventArgs.Empty );
                    }
                    break;
                  }
                case 0x46: // Ambient temperature
                  {
                    if ( msg.bytesCount != 1 )
                    {
                      break;
                    }
                    ambientTemp = (int)( msg.bytes[0] - 40 );
                    if ( MessageAmbientTempReceived != null )
                    {
                      MessageAmbientTempReceived( this, EventArgs.Empty );
                    }
                    break;
                  }
              }
              break;
            }
          case 0x02:
            {
              switch ( msg.respToPID )
              {
                case 0x02: // Freeze frame DTC
                  {
                    if ( msg.bytesCount != 2 )
                    {
                      break;
                    }
                    mTroubleCodesSet.SetFreezeFrameCode( msg.bytes[0], msg.bytes[1] );
                    if ( MessageFreezeFrameDTCReceived != null )
                    {
                      MessageFreezeFrameDTCReceived( this, EventArgs.Empty );
                    }
                    break;
                  }
                case 0x05: // Coolant temperature
                  {
                    if ( msg.bytesCount != 1 )
                    {
                      break;
                    }
                    freezeFrameCoolantTemp = (int)( msg.bytes[0] - 40 );
                    if ( MessageFreezeFrameCoolantTempReceived != null )
                    {
                      MessageFreezeFrameCoolantTempReceived( this, EventArgs.Empty );
                    }
                    break;
                  }
                case 0x0C: // Engine RPM
                  {
                    if ( msg.bytesCount != 2 )
                    {
                      break;
                    }
                    int lol = msg.bytes[0] << 8;
                    freezeFrameEngineSpeed = (int)( ( ( msg.bytes[0] << 8 ) + msg.bytes[1] ) / 4.0f );
                    int x = lol;
                    if ( MessageFreezeFrameEngineRPMReceived != null )
                    {
                      MessageFreezeFrameEngineRPMReceived( this, EventArgs.Empty );
                    }
                    break;
                  }
                case 0x0D: // Vehicle speed
                  {
                    if ( msg.bytesCount != 1 )
                    {
                      break;
                    }
                    freezeFrameVehicleSpeed = (int)( msg.bytes[0] );
                    if ( MessageFreezeFrameVehicleSpeedReceived != null )
                    {
                      MessageFreezeFrameVehicleSpeedReceived( this, EventArgs.Empty );
                    }
                    break;
                  }
              }

              break;
            }
          case 0x03:
            {
              // No PIDs here
//               Console.WriteLine( "DTCS. ID " + msg.ID.ToString() + "bcount: " + msg.bytesCount );
//               for ( int k = 0; k < msg.bytesCount; k++ )
//               {
//                 Console.Write( msg.bytes[k].ToString() + " " );
//               }
//               Console.WriteLine( "end" );

              for ( int z = 0; z < msg.bytesCount / 2; z++ )
              {
                //Console.WriteLine( "                                     " + msg.bytes[z * 2].ToString() + " " + msg.bytes[z * 2 + 1].ToString() );
                mTroubleCodesSet.AddStoredCode( msg.bytes[z * 2], msg.bytes[z * 2 + 1] );
              }

              if (MessageStoredTroubleCodesReceived != null)
              {
                MessageStoredTroubleCodesReceived( this, EventArgs.Empty );
              }

//               for ( int z = 0; z < mTroubleCodesSet.mStoredCodes.Count; z++ )
//               {
//                 Console.WriteLine( mTroubleCodesSet.mStoredCodes[z].ToString() );
//               }
                break;
            }
          case 0x04:
            {
              break;
            }
          case 0x05:
            {
              break;
            }
          case 0x06:
            {
              break;
            }
          case 0x07:
            {
              // No PIDs here
              for ( int z = 0; z < msg.bytesCount / 2; z++ )
              {
                //Console.WriteLine( "PDTC                                 " + msg.bytes[z * 2].ToString() + " " + msg.bytes[z * 2 + 1].ToString() );
                mTroubleCodesSet.AddPendingCode( msg.bytes[z * 2], msg.bytes[z * 2 + 1] );
              }
              if ( MessageStoredTroubleCodesReceived != null )
              {
                MessageStoredTroubleCodesReceived( this, EventArgs.Empty );
              }
              break;
            }
          case 0x08:
            {
              break;
            }
          case 0x09:
            {
              switch ( msg.respToPID )
              {
                case 0x02:
                  {
                    char[] vinChr = new char[msg.bytesCount + 1];
                    for ( int z = 0; z < msg.bytesCount; z++ )
                    {
                      vinChr[z] = (char)msg.bytes[z];
                    }
                    vinChr[msg.bytesCount] = '\0';
                    VIN = new string( vinChr );
                    if ( MessageVINReceived != null )
                    {
                      MessageVINReceived( this, EventArgs.Empty );
                    }
                    break;
                  }
              }
              break;
            }
        }
      }
    }

    #endregion Methods


  }
}