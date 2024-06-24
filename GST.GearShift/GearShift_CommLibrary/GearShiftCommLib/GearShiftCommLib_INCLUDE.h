
#pragma once

#include "CAN_Functionality.h"
#include "OBD_Functionality.h"
#include "DAQ_Functionality.h"
#include "HW_UI_Functionality.h"

namespace GearShiftCommLib 
{

	enum errorCodes {
		ERR_REG_WND_CLASS							= 1,
		ERR_CREATE_WINDOW							= 2,
		ERR_REG_DEV_NOTIFY							= 3,
		ERR_GET_MESSAGE								= 4,
		ERR_CREATE_THR								= 5,
		ERR_ALLREADY_CONNECTED						= 6,
		ERR_DEVICE_DISCONNECTED						= 7,
		ERR_NO_DEVICES_TO_CONNECT					= 8,
		ERR_UNABLE_TO_LOAD_USBAPI_LIBRARY			= 9,
		ERR_USB_OPEN								= 10,
		ERR_TX										= 11,
		ERR_RX										= 12,
		ERR_CONN_CONFIG								= 13,
		ERR_WRONG_FIRMWARE							= 14,
		ERR_DEVICE_INIT								= 15,
		ERR_MEM_ALLOC								= 16,
		ERR_UNABLE_TO_START_TIMER					= 17,
		ERR_RX_BUFF_OVERFLOW						= 18,
		ERR_TRANSMISSION_FAILURE					= 19,
		ERR_TOO_MANY_USB_DEVICES					= 20,
		ERR_DEVICE_NOT_RESPONDING					= 21,
		ERR_OVERCURRENT								= 22,
		ERR_BUFFER_OVERFLOW							= 23,
		ERR_NONE									= 0
	};

	enum stateCodes {
		STATE_DISCONNECTED				= 0,
		STATE_CONNECTING_PHASE_1		= 1,
		STATE_CONNECTING_PHASE_2		= 2,
		STATE_CONNECTED					= 3,
		STATE_DISCONNECTING				= 4,
		STATE_ERROR						= 5,
	};

	//enum aiTypes {
	//	AI_PRESSURE = 0x00,
	//	AI_CURRENT = 0x01,
	//};


	// Device state and firmware version structures
	#pragma pack(1)
	typedef struct 
  {
		unsigned int appState;
		unsigned int appErrorCode;
		unsigned int dllState;
		unsigned int dllErrorCode;
		unsigned int buffFill;
		unsigned int overCurrentPorts;
	} TDeviceState;
	
	#pragma pack(1)
	typedef struct 
  {
		unsigned char major;
		unsigned char minor;
	} TFirmwareVersion;


// #pragma pack(1)
//   typedef struct 
//   {
//     unsigned int ID;
//     unsigned char Pwm[ 9 ];
//   } TUsbTxData;
// 
// #pragma pack(1)
//   typedef struct 
//   {
//     unsigned int ID;
//     unsigned int responseToID;
//     unsigned short current[ 18 ];
//     unsigned short pressure[ 14 ];
//   } TUsbRxData;





	/* Device handling functions */
	/*
	 * usbConnect() - should be ran before any other operation.
	 * On success returns ERR_NONE, on any other error ERR_XXX, 
	 * where XXX is one of the defined error codes in errorCodes enum
	 */
	extern "C" __declspec( dllexport ) int UsbConnect( void );

	/*
	 * usbDisconnect() - should be ran on program close
	 */
	extern "C" __declspec( dllexport ) int UsbDisconnect( void );

	/*
	 * usbGetDeviceSerialNumber()
	 * Currently not implemented.
	 */
	extern "C" __declspec( dllexport ) int UsbGetDeviceSerialNumber( void );
	
	/*
	 * usbGetDeviceVersion()
	 * Currently not implemented, checked in DLL for correctness.
	 */
	extern "C" __declspec( dllexport ) TFirmwareVersion UsbGetDeviceVersion( void );

	/*
	 * usbGetDeviceState()
	 * Returns the TDeviceState structure with device state and an eventual
	 * error code. If an error is reported the combination od usbDisconnect(); usbConnect();
	 * should be used
	 * Arguments:
	 * state - pointer to int variable // check appStateCodes enum for more information
	 * errorCode - pointer to int variable // check appErrorCodes enum for more information
	 * Return value:
	 * <none>
	 */
	extern "C" __declspec( dllexport ) void UsbGetDeviceState( unsigned int * state, unsigned int * errorCode, unsigned int * overCurrentPorts );
	
	/*
	 * usbResetDevice()
	 * Sends a reset to device. This includes clearing all buffer information in the DLL and device.
	 */
	extern "C" __declspec( dllexport ) void UsbResetDevice( void );





}

