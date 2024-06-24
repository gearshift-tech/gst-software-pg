#pragma once

#ifndef __OBD_comm_H
#define __OBD_comm_H


#ifdef __OBD_comm_C
#define EXT__OBD_comm_H 
#else
#define EXT__OBD_comm_H extern
#endif

void OBD_ELM_Init();

// Flag denoting if ELM327 is ready to receive a command
EXT__OBD_comm_H volatile unsigned char OBD_ELM_Prompting;

// Flag denoting if OBD communication is enabled
EXT__OBD_comm_H volatile unsigned char OBD_CommmEnabled;

// Initializes the OBD functionality
void OBD_Init(void);

// Disables the OBD functionality
void OBD_Enable(void);

// Enables the OBD functionality
void OBD_Disable(void);


#define OBD_RXBUFF_SIZE 1024 
   
EXT__OBD_comm_H volatile         char OBD_RXBFR_Data[ OBD_RXBUFF_SIZE ] __attribute__((far));
EXT__OBD_comm_H volatile unsigned int OBD_RXBFR_WritePos;
EXT__OBD_comm_H volatile unsigned int OBD_RXBFR_ReadPos;
EXT__OBD_comm_H volatile unsigned int OBD_RXBFR_BuffFill;

void OBD_RXBFR_Clear();

char OBD_RXBFR_PutData(char data);

char OBD_RXBFR_GetData(char *data);

int  OBD_RXBFR_GetBuffFill();

void OBD_SendELMCmd();



//----------------------------------------------------------------------------------------------------------

#define OBD_TXBUFF_SIZE 255

EXT__OBD_comm_H volatile         char OBD_TXBFR_Data[ OBD_TXBUFF_SIZE ] __attribute__((far));
EXT__OBD_comm_H volatile unsigned int OBD_TXBFR_WritePos;
EXT__OBD_comm_H volatile unsigned int OBD_TXBFR_ReadPos;
EXT__OBD_comm_H volatile unsigned int OBD_TXBFR_BuffFill;

void OBD_TXBFR_Clear();

inline char OBD_TXBFR_PutData(char data);

inline char OBD_TXBFR_GetData(char *data);

int  OBD_TXBFR_GetBuffFill();


#endif //_OBD_comm_H
