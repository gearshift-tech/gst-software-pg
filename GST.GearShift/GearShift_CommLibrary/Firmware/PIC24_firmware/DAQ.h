#pragma once

#ifndef __DAQ_routines_H
#define __DAQ_routines_H


#ifdef __DAQ_routines_C
#define EXT__DAQ_routines_H 
#else
#define EXT__DAQ_routines_H extern
#endif




#define DAQ_RXBUFF_SIZE 30

// Initializes CAN interface
void DAQ_Init(void); 

// Initializes CAN interface
void DAQ_Start(void);

// Initializes CAN interface
void DAQ_Stop(void);


// Application state value
EXT__DAQ_routines_H unsigned char DAQ_appState;
// Application error code
EXT__DAQ_routines_H unsigned char DAQ_errCode;

 
EXT__DAQ_routines_H TDAQInputData DAQ_RXBFR_Data[ DAQ_RXBUFF_SIZE ] __attribute__((far));
EXT__DAQ_routines_H unsigned int DAQ_RXBFR_WritePos;
EXT__DAQ_routines_H unsigned int DAQ_RXBFR_ReadPos;
EXT__DAQ_routines_H unsigned int DAQ_RXBFR_BuffFill;

void DAQ_RXBFR_Clear();

char DAQ_RXBFR_PutData(TDAQInputData data);

char DAQ_RXBFR_GetData(TDAQInputData *data);

int DAQ_RXBFR_GetBuffFill();

//----------------------------------------------------------------------------------------------------------

#define DAQ_TXBUFF_SIZE 20


EXT__DAQ_routines_H TDAQInputData DAQ_TXBFR_Data[ DAQ_TXBUFF_SIZE ] __attribute__((far));
EXT__DAQ_routines_H unsigned int DAQ_TXBFR_WritePos;
EXT__DAQ_routines_H unsigned int DAQ_TXBFR_ReadPos;
EXT__DAQ_routines_H unsigned int DAQ_TXBFR_BuffFill;

void DAQ_TXBFR_Clear();

char DAQ_TXBFR_PutData(CAN_PACKET data);

char DAQ_TXBFR_GetData(CAN_PACKET *data);

int DAQ_TXBFR_GetBuffFill();


#endif //_CAN_routines_H
