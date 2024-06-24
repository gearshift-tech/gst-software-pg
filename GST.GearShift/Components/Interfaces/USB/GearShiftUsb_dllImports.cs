using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;
using System.IO;

namespace GST.Gearshift.Components.Interfaces.USB//GST.Gearshift.Components.Interfaces.USB
{

  /// <remarks>
  /// Physical device interface
  /// </remarks>
  unsafe public partial class GearShiftUsb
  {
    /// <remarks>
    /// PLEASE NOTE:
    /// This only contains functions to communicate over the USB through VC++ dll
    /// </remarks>

    #region Imported functions

    [DllImport( "user32.dll", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    static extern uint RegisterWindowMessage( string lpString );

    // Device general functionality //-----------------------------------------------------------------//
    //-------------------------------------------------------------------------------------------------//

    /// <summary>
    /// Connects to the USB device. should be ran before any other operation.
    /// </summary>
    /// <param name="result">	On success returns ERR_NONE, on any other error ERR_XXX, 
    /// where XXX is one of the defined error codes in errorCodes enum </param>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern int UsbConnect();

    /// <summary>
    /// Disconnects from the USB device. should be ran on program close
    /// </summary>
    /// <param name="result">pointer to connection status enum, where result will be written</param>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern int UsbDisconnect();

    /// <summary>
    /// Currently not implemented, checked in DLL for correctness.
    /// </summary>
    /// <param name="result">pointer to connection status enum, where result will be written</param>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern FirmwareVersion UsbGetDeviceVersion();

    /// <summary>
    /// Returns the TDeviceState structure with device state and an eventual
    /// error code. If an error is reported the combination od usbDisconnect(); usbConnect();
    /// should be used
    /// </summary>
    /// <param name="state">pointer to int variable // check appStateCodes enum for more information</param>
    /// <param name="errorCode">pointer to int variable // check appErrorCodes enum for more information</param>
    /// <param name="overCurrentPorts">pointer to int variable, denotes the current outputs with overcurrent</param>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern void UsbGetDeviceState( ref UInt32 state, ref UInt32 errorCode, ref UInt32 overCurrentPorts );

    /// <summary>
    /// Sends a reset to device. This includes clearing all buffer information in the DLL and device.
    /// </summary>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern void UsbResetDevice();


    // Device DAQ functionality //---------------------------------------------------------------------//
    //-------------------------------------------------------------------------------------------------//

    /// <summary>
    /// Sets device configuration. If a device is connected new values of pwmFrequency 
    /// and current ADC mask are sent to it. Any automatic data transmission being in progress is interrupted,
    /// all PWMs are turned off. usbStartTransmission has to be issued to restart automatic data transmission. 
    /// </summary>
    /// <param name="pwmFreq">int variable // PWM frequency</param>
    /// <param name="currentADCMask">int variable // Tels which channels have to be sampled durring device operation.</param>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern void UsbSetDAQConfig( int pwmFreq, byte currReadChannsCount, byte[] currReadChannsIndices );

    /// <summary>
    /// Returns device configuration.
    /// </summary>
    /// <param name="numPressSense">pointer to int variable // number of pressure sensors channels</param>
    /// <param name="numCurrSense">pointer to int variable // number of current channels (numPWMDrv << 1)</param>
    /// <param name="numPWMDrv">pointer to int variable // number of PWM driver channels</param>
    /// <param name="freq">pointer to int variable // actual PWM frequency</param>
    /// <param name="currentADCMask">pointer to int variable // actual current ADC sampling mask</param>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern void UsbGetConfig( ref UInt32 numPressSense, ref UInt32 numCurrSense, ref UInt32 numPWMDrv, ref UInt32 freq, ref UInt32 currentADCMask );

    /// <summary>
    /// Writes data packets to internal buffer.
    /// </summary>
    /// <param name="data">pointer to TUsbTxData array // data array to be written to internal buffer</param>
    /// <param name="num">int variable // number of data packets to write</param>
    /// <param name="wrNum">pointer to int variable // number of data packets written to internal buffer</param>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern void UsbWriteData( UsbDAQTxData[] data, UInt32 num, ref UInt32 wrNum );

    /// <summary>
    /// Reads data from internal buffer.
    /// </summary>
    /// <param name="data">data - pointer to TUsbRxData array // array to which data from internal buffer should be written</param>
    /// <param name="num">num - int variable // number of bytes that can be written to the "data" array.</param>
    /// <param name="rdNum">rdNum - pointer to int variable // number of bytes that has been written to the "data" array</param>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern void UsbReadData( [In, Out] UsbDAQRxData[] data, UInt32 num, ref UInt32 rdNum );

    /// <summary>
    /// Starts the PWM drivers. usbWriteData() should be issued first to preload the PWM data.
    /// Data is being automatically collected by the device and sent to PC.
    /// </summary>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern void UsbPwmStart();

    /// <summary>
    /// Stops the PWM drivers and data aquisition.
    /// </summary>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern void UsbPwmStop();

    /// <summary>
    /// Gets the number of frames in the RX buffer (from this module)
    /// </summary>
    /// <param name="fill"></param>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern UInt32 UsbGetDevRxBuffFill();

    /// <summary>
    /// Gets the RX buffer size (to receive from this module)
    /// </summary>
    /// <param name="size"></param>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern UInt32 UsbGetDevRxBuffSize();

    /// <summary>
    /// Gets the number of frames in the TX buffer (to be sent to this module)
    /// </summary>
    /// <param name="fill"></param>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern UInt32 UsbGetDevTxBuffFill();

    /// <summary>
    /// Gets the TX buffer size (to transmit to this module)
    /// </summary>
    /// <param name="size"></param>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern UInt32 UsbGetDevTxBuffSize();

    /// <summary>
    /// Disables manual drive on all channels
    /// </summary>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern void UsbDAQDisableAllManualDriverControls();

    /// <summary>
    /// Enables manual drive on specified channel with specified value
    /// </summary>
    /// <param name="index"> channel to enable index</param>
    /// <param name="value"> channel manual drive value</param>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern void UsbDAQEnableManualDriverControl( byte index, byte value );

    /// <summary>
    /// Sets the manual drive value of specified channel to specified value
    /// </summary>
    /// <param name="index">channel to set index</param>
    /// <param name="value">channel manual drive value</param>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern void UsbDAQSetManualDriverValue( byte index, byte value );

    /// <summary>
    /// Disables the manual drive on specified channel
    /// </summary>
    /// <param name="index">channel to disable index</param>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern void UsbDAQDisableManualDriverControl( byte index );


    // Device hardware UI functionality //-------------------------------------------------------------//
    //-------------------------------------------------------------------------------------------------//

    /// <summary>
    /// Displays a message on first row of UI LCD
    /// </summary>
    /// <param name="message">16-characters message to be displayed</param>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern void UsbUIDisplayLcdRow1Msg( char[] message );

    /// <summary>
    /// Displays a message on second row of UI LCD
    /// </summary>
    /// <param name="message">16-characters message to be displayed</param>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern void UsbUIDisplayLcdRow2Msg( char[] message );

    /// <summary>
    /// Sets the UI bargraphs display mode
    /// </summary>
    /// <param name="mode">Display mode: 1-current, 0-PWMs</param>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern void UsbUISetBargraphsMode( byte mode );

    // Device CAN functionality //---------------------------------------------------------------------//
    //-------------------------------------------------------------------------------------------------//

    /// <summary>
    /// Gets the number of frames in the CAN TX buffer (to be sent to this module)
    /// </summary>
    /// <param name="fill"></param>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern UInt32 UsbCANGetDevTxBuffFill( );

    /// <summary>
    /// Gets the CAN TX buffer size (to transmit to this module)
    /// </summary>
    /// <param name="size"></param>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern UInt32 UsbCANGetDevTxBuffSize( );

    /// <summary>
    /// Gets the number of frames in the CAN RX buffer (from this module)
    /// </summary>
    /// <param name="fill"></param>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern UInt32 UsbCANGetDevRxBuffFill( );

    /// <summary>
    /// Gets the CAN RX buffer size (to receive from this module)
    /// </summary>
    /// <param name="size"></param>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern UInt32 UsbCANGetDevRxBuffSize( );

    /// <summary>
    /// Gets the number of frames in the DEVICE's CAN TX buffer
    /// </summary>
    /// <param name="fill"></param>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    public static extern UInt32 UsbCANGetUsbDevTxBuffFill();

    /// <summary>
    /// Gets the DEVICE's CAN TX buffer size
    /// </summary>
    /// <param name="size"></param>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    public static extern UInt32 UsbCANGetUsbDevTxBuffSize();

    /// <summary>
    /// Gets the number of frames in the DEVICE's CAN RX buffer
    /// </summary>
    /// <param name="fill"></param>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    public static extern UInt32 UsbCANGetUsbDevRxBuffFill();

    /// <summary>
    /// Gets the DEVICE's CAN RX buffer size
    /// </summary>
    /// <param name="size"></param>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    public static extern UInt32 UsbCANGetUsbDevRxBuffSize();

    /// <summary>
    /// Writes CAN data packets to internal buffer.
    /// </summary>
    /// <param name="data">pointer to TUsbCANData array // data array to be written to internal buffer</param>
    /// <param name="num">int variable // number of data packets to write</param>
    /// <param name="wrNum">pointer to int variable // number of data packets written to internal buffer</param>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern void UsbCANWriteData( [In, Out] UsbCANData[] data, UInt32 num, ref UInt32 wrNum );

    /// <summary>
    /// Reads CAN data from internal buffer.
    /// </summary>
    /// <param name="data">data - pointer to TUsbCANRxData array // array to which data from internal buffer should be written</param>
    /// <param name="num">num - int variable // number of bytes that can be written to the "data" array.</param>
    /// <param name="rdNum">rdNum - pointer to int variable // number of bytes that has been written to the "data" array</param>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern void UsbCANReadData( [In, Out] UsbCANData[] data, UInt32 num, ref UInt32 rdNum );

    /// <summary>
    /// Enables CAN pull-up.
    /// </summary>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern UInt32 UsbCanEnablePullUp();

    /// <summary>
    /// Disables CAN pull-up.
    /// </summary>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern UInt32 UsbCanDisablePullUp();

    /// <summary>
    /// Sets CAN config
    /// </summary>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern UInt32 UsbCANSetConfig( UInt32 busBaud, ref Double baudError);

    /// <summary>
    /// Disables the CAN on the device
    /// </summary>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern UInt32 UsbCANDisableComm();

    /// <summary>
    /// Reads the fixed trace recurds table
    /// </summary>
    /// <returns>Number of records written</returns>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern UInt32 UsbCANGetFixedTraceTable(  [In, Out] CANFixedTraceRecord[] data, UInt32 num );

    /// <summary>
    /// Resets the fixed trace timer and table
    /// </summary>
    /// <returns>0- success, 1- failed</returns>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern UInt32  UsbCANResetFixedTrace();

    // Device OBD functionality //---------------------------------------------------------------------//
    //-------------------------------------------------------------------------------------------------//

    /// <summary>
    /// Gets the number of frames in the OBD TX buffer (to be sent to this module)
    /// </summary>
    /// <param name="fill"></param>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern UInt32 UsbOBDGetDevTxBuffFill();

    /// <summary>
    /// Gets the OBD TX buffer size (to transmit to this module)
    /// </summary>
    /// <param name="size"></param>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern UInt32 UsbOBDGetDevTxBuffSize();

    /// <summary>
    /// Gets the number of frames in the OBD RX buffer (from this module)
    /// </summary>
    /// <param name="fill"></param>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern UInt32 UsbOBDGetDevRxBuffFill();

    /// <summary>
    /// Gets the OBD RX buffer size (to receive from this module)
    /// </summary>
    /// <param name="size"></param>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern UInt32 UsbOBDGetDevRxBuffSize();

    /// <summary>
    /// Writes OBD data packets to internal buffer.
    /// </summary>
    /// <param name="data">pointer to TUsbCANData array // data array to be written to internal buffer</param>
    /// <param name="num">int variable // number of data packets to write</param>
    /// <param name="wrNum">pointer to int variable // number of data packets written to internal buffer</param>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern void UsbOBDWriteData( [In, Out] UsbOBDTxData[] data, UInt32 num, ref UInt32 wrNum );
       
    /// <summary>
    /// Reads OBD data from internal buffer.
    /// </summary>
    /// <param name="data">data - pointer to TUsbCANRxData array // array to which data from internal buffer should be written</param>
    /// <param name="num">num - int variable // number of bytes that can be written to the "data" array.</param>
    /// <param name="rdNum">rdNum - pointer to int variable // number of bytes that has been written to the "data" array</param>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern void UsbOBDReadData( [In, Out] UsbOBDRxData[] data, UInt32 num, ref UInt32 rdNum );

    /// <summary>
    /// Sends a single command to the ELM327.
    /// Make sure to call UsbOBDElm327Init() first
    /// </summary>
    /// <param name="cmd">command to send</param>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern void UsbOBDSendCmd( char[] cmd );

    /// <summary>
    /// Initializes the ELM327 and returns status operation
    /// </summary>
    /// <returns>0 - init OK, 1 - init failed</returns>
    [DllImport("GearShiftCommLib.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
    private static extern int UsbOBDElm327Init();




    #endregion Imported functions



  }

}
