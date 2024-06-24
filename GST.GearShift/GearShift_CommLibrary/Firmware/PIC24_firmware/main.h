#pragma once
#ifndef _MAIN_H_
#define _MAIN_H_

#include <p24fxxxx.h>

#include "USB/usb_device.h"
#include "USB/usb.h"
#include "USB/usb_function_generic.h"
#include "usb_config.h"
#include "USBPacket.h"  


#include "rprintf.h"
#include "debug.h"

#define PRODUCTION


#define MAJOR_VERSION 0x1
#define MINOR_VERSION 0x59
//number of pressure sensors
#define PRESS_SENS_COUNT 14
//number of current sensors
#define CURR_SENS_COUNT 18
//number of current drivers
#define PWM_DRV_COUNT 9
//number of current measurement samples count to average
#define CADC_AVG_SAMPLES 8
//bitshift value for current measurement averaging
#define CADC_AVG_BITSHIFT 3

// Number of output data frames to buffer
#define NUM_OUTPUT_DATA_BUFF     20

#define MAX_TIMER_TICK_COUNT      20

// External oscillator frequency
#define SYSCLK          32000000
//
/* 
 * Signal assignment to remappable pins
 */
#define _DRV1		      19
#define _DRV2		      26
#define _DRV3		      21
#define _DRV4		      27
#define _DRV5		      20
#define _DRV6		      25
#define _DRV7		      22
#define _DRV8		      23
#define _DRV9		      24

#define _SPI1_CLK       11
#define _SPI1_MOSI      3
#define _SPI1_MISO      42
#define _EOC1           36
#define _EOC2           35
#define _SPI2_CLK       10
#define _SPI2_MOSI      17
#define _SPI2_MISO      30

#define _MCP2515_RX0BF  15
#define _MCP2515_RX1BF  4

#define _CAN_nCS        16

#define _TxD            2
#define _RxD            5

#define _CTS            43
#define _OCF            33



/*
 * Signal names and addresses on ports
 */
// PORT C pins
#define _bOV_CURR1      0x0002

// Port D pins
#define _bSPI1_CLK      0x0001
#define _bDRV9          0x0002
#define _bDRV8          0x0004
#define _bDRV7          0x0008
#define _bDRV6          0x0010
#define _bDRV5          0x0020
#define _bADC_nCS1		  0x0040
#define _bADC_nCS2		  0x0080
#define _bTxD           0x0100
#define _bMCP2515_RX1BF 0x0200
#define _bSPI1_MOSI     0x0400
#define _bV_USB_BUS     0x0800
#define _bSPI1_MISO     0x1000
#define _bBUSY          0x4000
#define _bRxD           0x8000

// PORT E pins
#define _bOV_CURR9      0x0001
#define _bOV_CURR8      0x0002
#define _bOV_CURR7      0x0004
#define _bOV_CURR6      0x0008
#define _bOV_CURR5      0x0010
#define _bOV_CURR4      0x0020
#define _bOV_CURR3      0x0040
#define _bOV_CURR2      0x0080
#define _bOCFP          0x0200

// PORT F pins
#define _bLED2          0x0001
#define _bLED3          0x0002
#define _bSPI2_MISO     0x0004
#define _bCAN_nCS       0x0008
#define _bSPI2_CLK      0x0010
#define _bSPI2_MOSI     0x0020
#define _bMCP2515_RX0BF 0x0040

// Port G pins
#define _bCAN_TRM_nEN   0x0001
//#define _bEOC1          0x0001
//#define _bEOC2          0x0002
#define _bDRV3          0x0040
#define _bDRV2          0x0080
#define _bDRV1          0x0100
#define _bDRV4          0x0200

#define _bAO_nCS		0x0002

#define IO_TxD          5
#define IO_nRTS         6

#define IO_SPI1_CLK     8
#define IO_SPI1_MOSI    7
#define IO_SPI1_CS      9

#define IO_SPI2_CLK     11
#define IO_SPI2_MOSI    10
#define IO_SPI2_CS      12

#define IO_DRV1         18
#define IO_DRV2         19
#define IO_DRV3         20
#define IO_DRV4         21
#define IO_DRV5         22
#define IO_DRV6         23
#define IO_DRV7         24
#define IO_DRV8         25
#define IO_DRV9         35

#define CADC_nCS1       LATDbits.LATD6
#define CADC_nCS2       LATDbits.LATD7

#define CADC_nCS1_LOW()   CADC_nCS1 = 0
#define CADC_nCS1_HIGH()   CADC_nCS1 = 1

#define AO_nCS			LATGbits.LATG1

#define EOC1            LATGbits.LATG0
#define EOC2            LATGbits.LATG1

#define MCP2515_RX0BF   PORTFbits.RF8
#define MCP2515_RX1BF   PORTDbits.RD9

#define CAN_TRM_nEN     LATGbits.LATG0
#define SPI2_CS         LATFbits.LATF3
#define CS_2515_LOW()   SPI2_CS = 0;
#define CS_2515_HIGH()  SPI2_CS = 1;

#define OVC_CH1         PORTCbits.RC1
#define OVC_CH2         PORTCbits.RC14
#define OVC_CH3         PORTCbits.RC13
#define OVC_CH4         PORTEbits.RE5
#define OVC_CH5         PORTEbits.RE4
#define OVC_CH6         PORTEbits.RE3
#define OVC_CH7         PORTEbits.RE2
#define OVC_CH8         PORTEbits.RE1
#define OVC_CH9         PORTEbits.RE0

#define OCFP             LATEbits.LATE9

//#define NUM_DI	      	0
//#define NUM_DO	      	0
//#define NUM_AI	      	9
//#define NUM_AO	      	14

// 0 - voltage, 1 - current
#define TYPE_AI		0xF0
#define TYPE_AO		0xF0


// Enum codes for USB status

enum USB_STATE_CODES 
{
   USB_STATE_DETACHED,
   USB_STATE_ATTACHED,
};

typedef struct TWorkData
{
   // Actual PWM frequency
   unsigned short DAQ_PWMFreq;
   // Actual PWM outputs values
   unsigned short DAQ_PWMDutyCycles[ PWM_DRV_COUNT ];
   // Actual output compare registers values
   unsigned short DAQ_ocCmpRegs[ PWM_DRV_COUNT ];
   // Actual output compare timer value
   unsigned short DAQ_ocTmrReg;
   // Actual manual PWM outputs values
   unsigned short DAQ_PWMManualDutyCycles[ PWM_DRV_MAX_COUNT ];
   // Actual flags if the manual value drive is enabled
   unsigned char DAQ_PWMManualDriveEnabled[ PWM_DRV_MAX_COUNT ];

   // Actual Analog output 1 value
   unsigned int DAQ_AO1value;
   // Actual Analog output 2 value
   unsigned int DAQ_AO2value; 

   // If output compare registers need reloading in next DAQ timer tick
   unsigned char reloadOCRegs;
   // Actual bitfield denoting which outputs have triggered the overcurrent protection
   unsigned long overCurrentPorts;
 
   unsigned char currReadChannsCount;
   unsigned char currReadChannsIndices[9];  
   //unsigned long long int currentADCMask;
   unsigned short ADCReadOutValues[CURR_SENS_COUNT];
   unsigned int currAdcLostFramesCount;


} TWorkData;

typedef struct TDAQOutputData 
{
   unsigned short used;
   unsigned long packetRespToID;
   unsigned short pressure[ PRESS_SENS_COUNT ];
   unsigned short current[ CURR_SENS_COUNT ];
} TDAQOutputData ;

typedef struct TDAQInputData
{
   unsigned short used;
   unsigned long packetID;
   unsigned char pwmValue[ 9 ];
   unsigned int AO1value;
   unsigned int AO2value;
} TDAQInputData;

unsigned short write_spi1( unsigned short data );
unsigned short write_spi2( unsigned short data );


void InitUART2(void);
void InitAD( void );
void InitCN( void );
void InitPorts( void );
void InitPeripheralPins( void );
void startIntAD( void );
void initMemory( void );
void deviceInit( void );

#ifdef _MAIN_C_
#define EXT
#else
#define EXT extern
#endif


//EXT unsigned long szit;

EXT volatile TWorkData workData;

EXT unsigned short intADValues[ PRESS_SENS_COUNT ];
EXT unsigned short intADBuff[ PRESS_SENS_COUNT ];
EXT unsigned char intADCount;
EXT unsigned char adFinished;

//EXT unsigned char event;

EXT TDAQOutputData DAQ_outputDataBuff[ NUM_OUTPUT_DATA_BUFF ];

EXT unsigned char outputDataBuffReadPos;
EXT unsigned char outputDataBuffWritePos;
EXT unsigned long outPacketID;


#endif

