#pragma once

#ifndef _USBPACKET_H
#define _USBPACKET_H


#ifdef WIN32
#pragma pack(1)
#define PACK
#else
#define PACK __attribute__((__packed__))
#endif

#define USBPACKET_VERSION 5 // max 255

//#ifdef WIN32
//  #pragma pack(1)
//  #define PACK
//  #define ALIGN_W32 __declspec( align( 1 ) ) 
//#else
//  #define PACK __attribute__((__packed__))
//  #define ALIGN_W32
//#endif

#include "CANPacket.h"

#define PWM_DRV_MAX_COUNT					   9
#define CURRENT_SENSE_MAX_COUNT				18
#define PRESSURE_SENSE_MAX_COUNT			   14


enum cmdCodes 
{

	CMD_GET_VERSION       = 0x01,
	CMD_GET_CONFIG        = 0x02,
	CMD_SET_CONFIG        = 0x03,
	CMD_START             = 0x04,
	CMD_STOP              = 0x05,
	CMD_DEV_DATA          = 0x06,
	CMD_PWM_DATA          = 0x08,
	CMD_GET_BUFF_FILL     = 0x09,
	CMD_GET_STATE         = 0x0A,
	CMD_POLL_DATA         = 0x0B,
	CMD_SET_SERIAL        = 0x0C,
	CMD_GET_SERIAL        = 0x0D,



	CMD_CAN_DISABLE_COMM  = 0xA0,	
	CMD_CAN_GET_CONFIG    = 0xA2,
	CMD_CAN_SET_CONFIG    = 0xA3,
	CMD_CAN_ADD_NODES     = 0xA4,	
	CMD_CAN_DATA          = 0xA6,	
	CMD_CAN_GET_STATE     = 0xA9,
	CMD_CAN_EN_PULLUP     = 0xAA,
	CMD_CAN_DSBL_PULLUP   = 0xAB,
	CMD_CAN_RST_FXD_TRC   = 0xAC,

	CMD_UI_UPDATE         = 0xB0,

	CMD_OBD_ENABLE_COMM   = 0xC0,	
	CMD_OBD_GET_CONFIG    = 0xC2,
	CMD_OBD_SET_CONFIG    = 0xC3,
	CMD_OBD_ADD_NODES     = 0xC4,	
	CMD_OBD_DATA          = 0xC6,	
	CMD_OBD_GET_STATE     = 0xC9,

	CMD_BLD_ENTER         = 0xD0

};

enum appStateCodes 
{
	APP_STATE_DISCONNECTED = 0x00,
	APP_STATE_WAIT_FOR_INIT = 0x01,
	APP_STATE_WAIT_FOR_CONFIG = 0x02,
	APP_STATE_STOPPED = 0x03,
	APP_STATE_RUNNING = 0x04,
	APP_STATE_ERROR = 0x05,
};

enum appErrorCodes 
{
	APP_ERR_NONE = 0x00,
	APP_ERR_BUFFER_OVERFLOW = 0x01,
	APP_ERR_OVER_CURRENT = 0x02,
	APP_ERR_COMM = 0x03,
};

enum CANErrorCodes 
{
	CAN_ERR_NONE = 0x00,
	CAN_ERR_RX_BUFF_OVF = 0x01,
	CAN_ERR_TX_BUFF_OVF = 0x02,
};

enum UIBgDispModeCodes 
{
	UI_BG_DISP_PWM = 0x00,
	UI_BG_DISP_CURR = 0x01
};



typedef union 
{

	unsigned char _bytes[ 64 ];

	struct 
	{
		unsigned char cmd ;//PACK;
	};



	//______________________________________________________________________________	
	// Device configuration packet
#ifdef WIN32
#pragma pack(1)
#endif
	struct 
	{
		unsigned char cmd;
		unsigned char pressureSensCount; //generally not used
		unsigned char pwmDrvCount; //generally not used
		unsigned char currentSensCount; //generally not used
		unsigned char appState;
		unsigned short frequency;
		unsigned char currReadChannsCount;
		unsigned char currReadChannsIndices[9];

		unsigned char DAQ_txBuffSize;
		unsigned char DAQ_rxBuffSize;
		unsigned char CAN_txBuffSize;
		unsigned char CAN_rxBuffSize;

#ifdef _WIN32
		unsigned short OBD_txBuffSize;
		unsigned short OBD_rxBuffSize;
#else
		unsigned int OBD_txBuffSize;
		unsigned int OBD_rxBuffSize;
#endif
	} PACK devConfig;

	//______________________________________________________________________________
	//  Serial number and GUID
#ifdef WIN32
#pragma pack(1)
#endif
	struct 
	{
		unsigned char cmd;
		char serialString[15]; // 14 characters + '\0'
		char GUIDString[37]; // 32 characters + 4x '-' + '\0'
	} PACK serialInfo;


	//______________________________________________________________________________
	//  Firmware version
#ifdef WIN32
#pragma pack(1)
#endif
	struct 
	{
		unsigned char cmd;
		unsigned char major;
		unsigned char minor;
		unsigned char USBPacket;
	} PACK fwVersion;


	//______________________________________________________________________________   
	// Transmission data
#ifdef WIN32
#pragma pack(1)
#endif
	struct 
	{
		unsigned char cmd;
		unsigned long packetID;
#ifdef _WIN32
		unsigned int responseToID;
#else
		unsigned long responseToID;
#endif
		unsigned short pressureSense[ PRESSURE_SENSE_MAX_COUNT ];
		unsigned short currentSense[ CURRENT_SENSE_MAX_COUNT / 2 ];
		unsigned char state;
		unsigned char errorCode;

		unsigned char DAQ_txBuffFill;
		unsigned char DAQ_rxBuffFill;

		unsigned short overCurrentPorts;
	} PACK devData;

	//______________________________________________________________________________  
	// PWM Duty cycle data
#ifdef WIN32
#pragma pack(1)
#endif
	struct 
	{
		unsigned char cmd;
#ifdef _WIN32
		unsigned int packetID;
#else
		unsigned long packetID;
#endif
		// PWM drive values array
		unsigned char pwmData[ PWM_DRV_MAX_COUNT ];
		// if PWM drive values array has meaningful data, also if packedID is useful
		unsigned char pwmDataCorrect;
		// PWM manual drive values array
		unsigned char pwmManuals[ PWM_DRV_MAX_COUNT ];
		// if manual drive values and enable flags are meaningful
		unsigned char pwmManualsCorrect;
		// array with flags denoting which channels have manual control enabled
		unsigned char pwmManualsEnableFlags[ PWM_DRV_MAX_COUNT ];   
		// Analog output 1 value
		unsigned short AO1;
		// Analog output 2 value
		unsigned short AO2;   
	} PACK devPWMData;

	//______________________________________________________________________________
	// Device state    
#ifdef WIN32
#pragma pack(1)
#endif
	struct 
	{
		unsigned char cmd;
		unsigned char state;
		unsigned char errorCode;
		unsigned char DAQ_txBuffFill;
		unsigned char DAQ_rxBuffFill;
		unsigned short overCurrentPorts;
		unsigned char CAN_txBuffFill;
		unsigned char CAN_rxBuffFill;

#ifdef _WIN32
		unsigned short OBD_txBuffFill;
		unsigned short OBD_rxBuffFill;
#else
		unsigned int OBD_txBuffFill;
		unsigned int OBD_rxBuffFill;
#endif    

	} PACK devState;


	///////////////////////////////////////////////////////////////////////////////////
	/////__CAN__///////////////////////////////////////////////////////////////////////
	///////////////////////////////////////////////////////////////////////////////////

	//______________________________________________________________________________ 
	// CAN data packet
#ifdef WIN32
#pragma pack(1)
#endif
	struct 
	{
		unsigned char cmd;
		unsigned char txBuffFill;
		//unsigned char txBuffSize;
		unsigned char rxBuffFill;
		//unsigned char rxBuffSize;
		unsigned char msgCount; // max 3 per packet
		CAN_PACKET msgs[3]; // 3 packets 17 bytes each
		//CANMsg messages[3];      
	} PACK CANDataPacket;

	//______________________________________________________________________________ 
	// CAN config packet
#ifdef WIN32
#pragma pack(1)
#endif
	struct 
	{
		unsigned char cmd;
		unsigned char MCP2515_BRPl;
		unsigned char MCP2515_PS1;
		unsigned char MCP2515_PS2;
		unsigned char MCP2515_PRSEG;
		unsigned char MCP2515_SJW;

	} PACK CANConfigPacket;   

	//   //______________________________________________________________________________
	//   // CAN state packet
	//   struct 
	//   {
	//      
	//      unsigned char CANState;
	//      unsigned char CanErrorCode;
	//      unsigned char CANTxBuffFill;
	//      unsigned char CANRxBuffFill;
	//   } PACK CANState;

	///////////////////////////////////////////////////////////////////////////////////
	/////__UI__////////////////////////////////////////////////////////////////////////
	///////////////////////////////////////////////////////////////////////////////////

	//______________________________________________________________________________	
	// UI display data
#ifdef WIN32
#pragma pack(1)
#endif
	struct
	{
		unsigned char cmd;
		char lcdRow1Str[17]; // first lcd row data
		unsigned char lcdUpdateRow1;  // if first row should be updated    
		char lcdRow2Str[17]; // second lcd row data
		unsigned char lcdUpdateRow2;  // if second row should be updated      
		unsigned char bgSetDispMode;  // if bargraphs display mode should be updated
		unsigned char bgDispMode;     // bargraphs display mode (1-currents, 0-duty cycles) 
	} PACK UIDataPacket;



	///////////////////////////////////////////////////////////////////////////////////
	/////__OBD__///////////////////////////////////////////////////////////////////////
	///////////////////////////////////////////////////////////////////////////////////

	//______________________________________________________________________________ 
	// OBD data packet
#ifdef WIN32
#pragma pack(1)
#endif
	struct 
	{
		unsigned char cmd;
		unsigned short txBuffFill;
		unsigned short txBuffSize;
		unsigned short rxBuffFill;
		unsigned short rxBuffSize;
		unsigned char charCount; // max 56 per packet
		char          chars[50];   


	} PACK OBDDataPacket;

} TDataPacket;

#endif //_USBPACKET_H

