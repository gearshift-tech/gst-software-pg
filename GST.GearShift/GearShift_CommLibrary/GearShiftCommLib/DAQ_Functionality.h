#pragma once

#define __DAQ_Functionality_h

namespace GearShiftCommLib 
{

#ifdef __DAQ_Functionality_cpp
#define EXT_DAQIF 
#else
#define EXT_DAQIF extern 
#endif

#define NUM_TX_BUFF		256
#define NUM_RX_BUFF		256

__declspec(align(1))
  typedef struct 
  {
    unsigned int ID;
	  unsigned int AO1;
	  unsigned int AO2;
    unsigned char Pwm[ 9 ];
  } TUsbTxData;

__declspec(align(1))
  typedef struct 
  {
    unsigned int ID;
    unsigned int responseToID;
    unsigned short current[ 18 ];
    unsigned short pressure[ 14 ];
  } TUsbRxData;


  EXT_DAQIF TUsbTxData usbTxData[ NUM_TX_BUFF ];
  EXT_DAQIF TUsbRxData usbRxData[ NUM_RX_BUFF ];

  EXT_DAQIF int txWritePos;
  EXT_DAQIF int txReadPos;
  EXT_DAQIF int rxWritePos;
  EXT_DAQIF int rxReadPos;

	EXT_DAQIF UINT32 DAQ_DevTxBuffSize;
  EXT_DAQIF UINT32 DAQ_DevTxBuffFill;
  EXT_DAQIF UINT32 DAQ_DevRxBuffSize;
  EXT_DAQIF UINT32 DAQ_DevRxBuffFill;

  EXT_DAQIF unsigned int txFill;
  EXT_DAQIF int rxFill;

  //EXT_DAQIF unsigned char DAQ_LastSentValues[ 9 ];
  EXT_DAQIF unsigned char DAQ_ManualDrivesValues[ 9 ];
  EXT_DAQIF bool DAQ_ManualDrivesEnabled [ 9 ];
  EXT_DAQIF bool DAQ_ManualDrivesNeedUpdating;

  // Event set when DAQ buffers init response is received from the uC
  EXT_DAQIF HANDLE DAQ_Event_BfrRstCmd_Complete;

  void DAQ_InitInternals( void );

  int DAQ_ProcessTx();
  int DAQ_ProcessRx();

	extern "C" __declspec( dllexport ) void UsbSetDAQConfig( int pwmFreq, unsigned char currReadChannsCount, unsigned char currReadChannsIndices[9] );

  extern "C" __declspec( dllexport ) void UsbDAQDisableAllManualDriverControls( void );

  extern "C" __declspec( dllexport ) void UsbDAQEnableManualDriverControl( unsigned char index, unsigned char value );

  extern "C" __declspec( dllexport ) void UsbDAQSetManualDriverValue( unsigned char index, unsigned char value );

  extern "C" __declspec( dllexport ) void UsbDAQDisableManualDriverControl( unsigned char index );

  extern "C" __declspec( dllexport ) UINT32 UsbDAQResetBuffersAndCounters( );

	/*
	 * usbGetConfig()
	 * Returns device configuration.
	 * Arguments:
	 * numPressSense - pointer to int variable // number of pressure sensors channels
	 * numCurrSense - pointer to int variable // number of current channels (numPWMDrv << 1)
	 * numPWMDrv - pointer to int variable // number of PWM driver channels
	 * freq - pointer to int variable // actual PWM frequency
	 * currentADCMask - pointer to int variable // actual current ADC sampling mask
	 */
	extern "C" __declspec( dllexport ) void UsbGetConfig( unsigned int * numPressSense, unsigned int * numCurrSense, unsigned int * numPWMDrv, unsigned int * freq, unsigned int * currentADCMask );

	/*
	 * usbWriteData()
	 * Writes data packets to internal buffer.
	 * Arguments:
	 * data - pointer to TUsbTxData array // data array to be written to internal buffer
	 * num - int variable // number of data packets to write
	 * wrNum - pointer to int variable // number of data packets written to internal buffer
	 * Return value:
	 * <none>
	 */
	extern "C" __declspec( dllexport ) void UsbWriteData( TUsbTxData * data, unsigned int num, unsigned int * wrNum );

	/*
	 * usbReadData()
	 * Reads data from internal buffer.
	 * Arguments:
	 * data - pointer to TUsbRxData array // array to which data from internal buffer should be written
	 * num - int variable // number of bytes that can be written to the "data" array.
	 * rdNum - pointer to int variable // number of bytes that has been written to the "data" array
	 * Return value:
	 * <none>
	 */
	extern "C" __declspec( dllexport ) void UsbReadData( TUsbRxData * data, unsigned int num, unsigned int * rdNum );

	/*
	 * usbPwmStart()
	 * Starts the PWM drivers. usbWriteData() should be issued first to preload the PWM data.
	 * Data is being automatically collected by the device and sent to PC.
	 * Arguments:
	 * <none>
	 * Return value:
	 * <none>
	 */
	extern "C" __declspec( dllexport ) void UsbPwmStart( void );

	/*
	 * usbPwmStop()
	 * Stops the PWM drivers and data acquisition.
	 * Arguments:
	 * <none>
	 * Return value:
	 * <none>
	 */
	extern "C" __declspec( dllexport ) void UsbPwmStop( void );

  extern "C" __declspec( dllexport ) UINT32 UsbGetDevTxBuffFill( void );

  extern "C" __declspec( dllexport ) UINT32 UsbGetDevTxBuffSize( void );

  extern "C" __declspec( dllexport ) UINT32 UsbGetDevRxBuffFill( void );

  extern "C" __declspec( dllexport ) UINT32 UsbGetDevRxBuffSize( void );



}

