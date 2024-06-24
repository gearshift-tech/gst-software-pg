#pragma once

#ifndef __KLINE_H
#define __KLINE_H


#define EXT__KLINE_H 




enum OUT_EVENT_TYPE                   // Enum denoting the type of output packet
{
  KLINE_TX_EVT				  = 0x01,       // Out packet carries KLINE data
  PHERIPHERIALS_EVT		  = 0x02        // Out packet carries pheriperials data
};

typedef struct                        // Struct that contains data received from the K-LINE
{
  unsigned short    data;             // Received byte
  unsigned long     timestamp;        // Timestamp at which this byte was received
  unsigned short    U2STAreg;         // Value of the UART status register (to detect bus errors)
} KLINE_RXDATA;

typedef union                         // Union of a output event structures, it can either be KLINE data portion or pheriperial event
{
	struct                              // Struct that contains data to be sent with K-LINE
	{
	  unsigned long   timestamp;        // Timestamp at which bytes from this packet should be put into the TX queue
	  unsigned char   eventType;        // Type of action that is to be taken, for this struct it must match OUT_EVENT_TYPE.KLINE_TX
	  unsigned char   dataLength;       // Number of bytes carried in this package
	  unsigned char   data[32];         // Array of bytes to be sent
	} KLINE_TX;

	struct                              // Struct that contains encoded pheriperial actions to be taken
	{
	  unsigned long   timestamp;        // Timestamp at which this actions should be taken
	  unsigned char   eventType;        // Type of action that is to be taken, for this struct it must match OUT_EVENT_TYPE.PHERIPHERIALS_EVT
	  unsigned char   setBrg;           // If baudrate should be changed, 0 - no, 1 - yes
	  unsigned short  BRG;              // Baud rate register value to be set
	  unsigned char   resetTimestamp;   // If timestamp timer should be reset, 0 - no, 1 - yes
	  unsigned char   KLINE_PullDown;   // 0 - do not change, 1 - turn off, 2 - turn on
	  unsigned char   relayState;       // 0 - do not change, 1 - turn off, 2 - turn on
	  unsigned char   wakeUpSignal;     // 0 - do not change, 1 - turn off, 2 - turn on
	} PHERIPERIALS_EVT;

} OUTPUT_DATA;

EXT__KLINE_H volatile unsigned long KLINE_TimeStamp;

EXT__KLINE_H volatile unsigned char KLINE_WaitInit;

// Flag denoting if OBD communication is enabled
EXT__KLINE_H volatile unsigned char KLINE_IsEnabled;

// Flag denoting if OBD communication is enabled
EXT__KLINE_H volatile unsigned long KLINE_SelectedBaud;

// Initializes the K-Line functionality
void KLINE_Init(void);

// Disables the OBD functionality
void KLINE_Enable(void);

// Enables the OBD functionality
void KLINE_Disable(void);


#define KLINE_RXBUFF_SIZE 512
   
EXT__KLINE_H volatile KLINE_RXDATA KLINE_RXBFR_Data[ KLINE_RXBUFF_SIZE ];
EXT__KLINE_H volatile unsigned int KLINE_RXBFR_WritePos;
EXT__KLINE_H volatile KLINE_RXDATA *KLINE_RXBFR_WritePtr;
EXT__KLINE_H volatile unsigned int KLINE_RXBFR_ReadPos;
EXT__KLINE_H volatile KLINE_RXDATA *KLINE_RXBFR_ReadPtr;
EXT__KLINE_H volatile unsigned int KLINE_RXBFR_BuffFill;
EXT__KLINE_H volatile unsigned int KLINE_RXBFR_OvfCount;

void KLINE_RXBFR_Clear();

char KLINE_RXBFR_PutData(KLINE_RXDATA data);

char KLINE_RXBFR_GetData(KLINE_RXDATA *data);

int  KLINE_RXBFR_GetBuffFill();


//----------------------------------------------------------------------------------------------------------

#define KLINE_TXBUFF_SIZE 96

EXT__KLINE_H volatile OUTPUT_DATA   KLINE_TXBFR_Data[ KLINE_TXBUFF_SIZE ];
EXT__KLINE_H volatile OUTPUT_DATA   *KLINE_TXBFR_DataEnd;
//EXT__KLINE_H volatile unsigned int  KLINE_TXBFR_WritePos;
EXT__KLINE_H volatile OUTPUT_DATA   *KLINE_TXBFR_WritePtr;
//EXT__KLINE_H volatile unsigned int  KLINE_TXBFR_ReadPos;
EXT__KLINE_H volatile OUTPUT_DATA   *KLINE_TXBFR_ReadPtr;
EXT__KLINE_H volatile unsigned int  KLINE_TXBFR_BuffFill;
EXT__KLINE_H volatile unsigned int  KLINE_TXBFR_OvfCount;

void KLINE_TXBFR_Clear();

char KLINE_TXBFR_PutData(OUTPUT_DATA data);

char KLINE_TXBFR_GetData(OUTPUT_DATA *data);

int  KLINE_TXBFR_GetBuffFill();


//----------------------------------------------------------------------------------------------------------

#define KLINE_TXQUEUE_SIZE 128

EXT__KLINE_H volatile char          KLINE_TXQUEUE_Data[ KLINE_TXQUEUE_SIZE ];
EXT__KLINE_H volatile char          *KLINE_TXQUEUE_DataEnd;
EXT__KLINE_H volatile char          *KLINE_TXQUEUE_WritePtr;
EXT__KLINE_H volatile char          *KLINE_TXQUEUE_ReadPtr;
EXT__KLINE_H volatile unsigned int  KLINE_TXQUEUE_BuffFill;
EXT__KLINE_H volatile unsigned int  KLINE_TXQUEUE_OvfCount;

void KLINE_TXQUEUE_Clear();

char KLINE_TXQUEUE_PutData(char data);

char KLINE_TXQUEUE_GetData(char *data);

int  KLINE_TXQUEUE_GetBuffFill();


#endif //_KLINE_H
