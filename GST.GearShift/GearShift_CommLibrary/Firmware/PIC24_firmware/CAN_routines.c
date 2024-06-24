#ifndef __CAN_routines_C
#define __CAN_routines_C

#include <p24fxxxx.h>
#include "main.h"
//#include "current_adc.h"
#include "debug.h"
#include "CAN_routines.h"
#include "SPI2515.h"

// Initializes CAN interface
void CAN_Init(void)
{
  CAN2515_InitSPI();
  CAN_Disable();
  CAN_DisablePullUp();
  
  // initialice the lockup flag
  //CAN_MCP2515_Locked = 0;
  
  // initialize interrupts from MCP2515 receive buffers
  INTCON2bits.INT1EP = 1; 
  IFS1bits.INT1IF = 0;
  IPC5bits.INT1IP = 5;   
  IEC1bits.INT1IE = 1;
  
  INTCON2bits.INT4EP = 1; 
  IFS3bits.INT4IF = 0;
  IPC13bits.INT4IP = 5;   
  IEC3bits.INT4IE = 1;
 
  CAN_TimestampTimerValue = 0; 
  CAN_FirstTxBfrTimestampValue = 0;
  
  CAN_MCP2515_Locked = 0;
  CAN_MCP2515_RX0BF_unread = 0;
  CAN_MCP2515_RX1BF_unread = 0;
    
    T3CON = 0x0000;
    T3CONbits.TCKPS = 1;           // set the prescaler to 8
    PR3  = 199;                   // set timer3 compare value to get 10kHz trigger
    TMR3 = 0x0000;
    IFS0bits.T3IF = 0;            // Reset the flag
    IPC2bits.T3IP = 6;            // Set the interrupt priority
    T3CONbits.TON = 1;
    TMR3 = 0;
    IEC0bits.T3IE = 0;
}  

//
// Timer3 interrupt routine ( timestamp timer )
// This should execute every 0.1ms
//
void __attribute__((__interrupt__, __no_auto_psv__)) _T3Interrupt( void ) 
{
   CAN_TimestampTimerValue++;
   // Clear timer compare flag
   IFS0bits.T3IF = 0;
}  

// Enables CAN communication
void CAN_Enable(unsigned char _BRP, unsigned char _PS1, unsigned char _PS2, unsigned char _PRSEG, unsigned char _SJW)
{
  // reset 2515 and disable USB communication to make sure there's no data flow
  CAN2515_Reset();
  CAN2515_Reset();
  CAN_CommmEnabled = 0; 
  // clear both data buffers
  CAN_TXBFR_Clear();
  CAN_RXBFR_Clear();
  // reset MCP control flags
  CAN_MCP2515_Locked = 0;
  CAN_MCP2515_RX0BF_unread = 0;
  CAN_MCP2515_RX1BF_unread = 0;
  // configure and enable the operation of MCP2515
  CAN2515_SetBitTiming(_BRP, _PS1, _PS2, _PRSEG, _SJW);
  CAN2515_ConfigureRXTX(); 
  CAN2515_SetNormalOperationMode();
  // set the flag
  // it is used to check if CAN data can be transferred over the USB
  CAN_CommmEnabled = 1; 
  
  // Reset and start the timestamp timer
  CAN_TimestampTimerValue = 0; 
  IEC0bits.T3IE = 1;
  

  
  // this MUST be called, otherwise the MCP2515 interrupt flags (pins) will not be reset and interrupt will never be called.
  CAN2515_GetMessages();
  

}
  
// Disables CAN communication
void CAN_Disable(void)
{
  // Reset and stop the timestamp timer
  IEC0bits.T3IE = 0;
  CAN_TimestampTimerValue = 0; 
  
  CAN_DisablePullUp();
  // reset 2515 and disable USB communication to make sure there's no data flow
  CAN2515_Reset();
  CAN2515_Reset();
  CAN_CommmEnabled = 0; 
  // clear both data buffers
  CAN_TXBFR_Clear();
  CAN_RXBFR_Clear();
}  

// Enables CAN pullup
void CAN_EnablePullUp( void )
{
  CAN_TRM_nEN = 0;
  CAN_PullUpEnabled = 1;
}  

// Disables CAN pullup
void CAN_DisablePullUp( void )
{
  CAN_TRM_nEN = 1;
  CAN_PullUpEnabled = 0;
}  

//------------------------------------------------------------------------------------------------
// CAN RX RING BUFFER
//------------------------------------------------------------------------------------------------

void CAN_RXBFR_Clear()
{
  CAN_RXBFR_WritePos = 0;
  CAN_RXBFR_ReadPos = 0;
  CAN_RXBFR_BuffFill = 0;
}

char CAN_RXBFR_PutData(CAN_PACKET data)
{
  unsigned char i;  
  if (CAN_RXBFR_BuffFill >= CAN_RXBUFF_SIZE)
  {
      return 1;
  }  
  else
  {
    data.data.timestamp = CAN_TimestampTimerValue;
    for (i = 0; i < sizeof(CAN_PACKET); i++)
    {
        CAN_RXBFR_Data[CAN_RXBFR_WritePos].bytes[i] = data.bytes[i];
    }
    CAN_RXBFR_WritePos++;
    if (CAN_RXBFR_WritePos >= CAN_RXBUFF_SIZE)
      CAN_RXBFR_WritePos = 0;
    CAN_RXBFR_BuffFill++;
    return 0;
  }
}

char CAN_RXBFR_GetData(CAN_PACKET *data)
{
  unsigned char i;
  if (CAN_RXBFR_BuffFill <= 0)
    return 1;
  else
  {
    for (i = 0; i < sizeof(CAN_PACKET); i++)
    {
        data->bytes[i] = CAN_RXBFR_Data[CAN_RXBFR_ReadPos].bytes[i];
    }
    //*data = CAN_RXBFR_Data[CAN_RXBFR_ReadPos];
    CAN_RXBFR_ReadPos++;
    if (CAN_RXBFR_ReadPos >= CAN_RXBUFF_SIZE)
      CAN_RXBFR_ReadPos = 0;
    CAN_RXBFR_BuffFill--;
    return 0;
  }
}

int CAN_RXBFR_GetBuffFill()
{
  return CAN_RXBFR_BuffFill;
}


//------------------------------------------------------------------------------------------------
// CAN TX RING BUFFER
//------------------------------------------------------------------------------------------------

void CAN_TXBFR_Clear()
{
  CAN_TXBFR_WritePos = 0;
  CAN_TXBFR_ReadPos = 0;
  CAN_TXBFR_BuffFill = 0;
  
  CAN_FirstTxBfrTimestampValue = 0;
}

char CAN_TXBFR_PutData(CAN_PACKET data)
{
  unsigned char i;  
  if (CAN_TXBFR_BuffFill >= CAN_TXBUFF_SIZE)
    return 1;
  else
  {
    if (CAN_TXBFR_BuffFill == 0)
    {
      // If this is the first message added to the buffer, assign the value here
      CAN_FirstTxBfrTimestampValue = data.data.timestamp;
    }  
    for (i = 0; i < sizeof(CAN_PACKET); i++)
    {
        CAN_TXBFR_Data[CAN_TXBFR_WritePos].bytes[i] = data.bytes[i];
    }
    CAN_TXBFR_WritePos++;
    if (CAN_TXBFR_WritePos >= CAN_TXBUFF_SIZE)
      CAN_TXBFR_WritePos = 0;
    CAN_TXBFR_BuffFill++;
    return 0;
  }
}

char CAN_TXBFR_GetData(CAN_PACKET *data)
{
  unsigned char i;
  if (CAN_TXBFR_BuffFill <= 0)
    return 1;
  else
  {
    for (i = 0; i < sizeof(CAN_PACKET); i++)
    {
        data->bytes[i] = CAN_TXBFR_Data[CAN_TXBFR_ReadPos].bytes[i];
    }
    //*data = CAN_TXBFR_Data[CAN_TXBFR_ReadPos];
    CAN_TXBFR_ReadPos++;
    if (CAN_TXBFR_ReadPos >= CAN_TXBUFF_SIZE)
      CAN_TXBFR_ReadPos = 0;
    CAN_TXBFR_BuffFill--;
    
    // Take care of the CAN_TXBFR_BuffFill value
    if (CAN_TXBFR_BuffFill == 0)
    {
      // If this was the last message in the buffer, zero the value
      CAN_TXBFR_BuffFill = 0;
    }  
    else
    {
      // Otherwise assign the value from the first message in the buffer
      CAN_TXBFR_BuffFill = CAN_TXBFR_Data[CAN_TXBFR_ReadPos].data.timestamp;
    }  
    return 0;
  }
}

int CAN_TXBFR_GetBuffFill()
{
  return CAN_TXBFR_BuffFill;
}


#endif //__CAN_routines_C
