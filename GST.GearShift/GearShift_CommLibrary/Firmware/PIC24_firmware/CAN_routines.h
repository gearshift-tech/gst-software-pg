#pragma once

#ifndef __CAN_routines_H
#define __CAN_routines_H

#include "SPI2515.h"
#include "CANPacket.h"



#ifdef __CAN_routines_C
#define EXT__CAN_routines_H 
#else
#define EXT__CAN_routines_H extern
#endif




#define CAN_RXBUFF_SIZE 250

// Initializes CAN interface
void CAN_Init(void);
// Enables CAN communication
void CAN_Enable(unsigned char _BRP, unsigned char _PS1, unsigned char _PS2, unsigned char _PRSEG, unsigned char _SJW);
// Disables CAN communication
void CAN_Disable(void);  

// Enables CAN pullup
void CAN_EnablePullUp( void );
// Disables CAN pullup
void CAN_DisablePullUp( void );    

// Flag denoting if CAN pullup is enabled
EXT__CAN_routines_H volatile unsigned char CAN_PullUpEnabled;
// Flag denoting if CAN communication is enabled   
EXT__CAN_routines_H volatile unsigned char CAN_CommmEnabled;
// Flag denoting if MCP2515 is locked (some function talks to it via spi)
EXT__CAN_routines_H volatile unsigned char CAN_MCP2515_Locked;
// Flag denoting if MCP2515 RX0 is full and could not be read in interrupt because of the locked MCP
EXT__CAN_routines_H volatile unsigned char CAN_MCP2515_RX0BF_unread;
// Flag denoting if MCP2515 RX1 is full and could not be read in interrupt because of the locked MCP
EXT__CAN_routines_H volatile unsigned char CAN_MCP2515_RX1BF_unread;

// CAN timer, each tick means 100us
EXT__CAN_routines_H volatile unsigned long CAN_TimestampTimerValue;

// CAN Tx buffer's first message's timestamp value
EXT__CAN_routines_H volatile unsigned long CAN_FirstTxBfrTimestampValue;
 
EXT__CAN_routines_H CAN_PACKET CAN_RXBFR_Data[ CAN_RXBUFF_SIZE ] __attribute__((far));
EXT__CAN_routines_H unsigned int CAN_RXBFR_WritePos;
EXT__CAN_routines_H unsigned int CAN_RXBFR_ReadPos;
EXT__CAN_routines_H unsigned int CAN_RXBFR_BuffFill;

void CAN_RXBFR_Clear();

char CAN_RXBFR_PutData(CAN_PACKET data);

char CAN_RXBFR_GetData(CAN_PACKET *data);

int CAN_RXBFR_GetBuffFill();

//----------------------------------------------------------------------------------------------------------

#define CAN_TXBUFF_SIZE 160


EXT__CAN_routines_H CAN_PACKET CAN_TXBFR_Data[ CAN_TXBUFF_SIZE ] __attribute__((far));
EXT__CAN_routines_H unsigned int CAN_TXBFR_WritePos;
EXT__CAN_routines_H unsigned int CAN_TXBFR_ReadPos;
EXT__CAN_routines_H unsigned int CAN_TXBFR_BuffFill;

void CAN_TXBFR_Clear();

char CAN_TXBFR_PutData(CAN_PACKET data);

char CAN_TXBFR_GetData(CAN_PACKET *data);

int CAN_TXBFR_GetBuffFill();


#endif //_CAN_routines_H
