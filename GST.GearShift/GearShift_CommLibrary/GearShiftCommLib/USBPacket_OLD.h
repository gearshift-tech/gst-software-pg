#ifndef _USBPACKET_H
#define _USBPACKET_H

enum cmdCodes {
	CMD_GET_VERSION = 0x01,
	CMD_INIT = 0x02,
	CMD_GET_CONFIG = 0x03,
	CMD_SET_DEVICE_DATA = 0x04,
	CMD_GET_DEVICE_DATA = 0x05,
};

#ifdef WIN32
#pragma pack(1)
#define PACK
#define PACK1 __declspec( align( 1 ) ) 
#else
#define PACK __attribute__((__packed__))
#define PACK1
#endif

typedef union {

	unsigned char _bytes[ 64 ];

	struct {
		unsigned char cmd PACK;
	};
	
	struct {
		unsigned char cmd;
		unsigned char numDI;
		unsigned char numDO;
		unsigned char numAI;
		unsigned char numAO;

		// 0 - voltage, 1 - current
		unsigned short aiTypes;
		// 0 - voltage, 1 - current
		unsigned short aoTypes;

	} PACK devConfig;

	struct {
		unsigned char cmd;
		unsigned char major;
		unsigned char minor;
	} PACK fwVersion;

	struct {
		unsigned char cmd;
		unsigned short digitalInputs;
		unsigned short digitalOutputs;
		unsigned short analogInputs[ 8 ];
		unsigned short analogOutputs[ 14 ];
		unsigned short speed;
		unsigned short encPulses;
	} PACK devData;

} TDataPacket;







#endif
