#ifndef __OBD_comm_C
#define __OBD_comm_C

#include <p24fxxxx.h>
#include "rprintf.h"
#include "main.h"
//#include "current_adc.h"
//#include "debug.h"
#include "OBD_comm.h"
//#include "SPI2515.h"

// Initializes the OBD functionality
void OBD_Init(void)
{
  // set the flag to ensure no data flow
  OBD_CommmEnabled = 0; 
  // clear both buffers
  OBD_RXBFR_Clear();
  OBD_TXBFR_Clear();

  // Configure ELM327 and disable further communication
  OBD_ELM_Prompting = 1;
  OBD_ELM_Init();    
  OBD_ELM_Prompting = 0;
}  

// Disables the OBD functionality
void OBD_Enable(void)
{
}  

// Enables the OBD functionality
void OBD_Disable(void)
{
}  

//------------------------------------------------------------------------------------------------
// ELM327 init routine
//------------------------------------------------------------------------------------------------
void OBD_ELM_Init()
{
  
  // Disable echoing       
  while ( !OBD_ELM_Prompting );
  OBD_ELM_Prompting = 0;
  rprintf("AT E1   \n\r");
//
  // Enable Adaptive Timing Option 1
  while ( !OBD_ELM_Prompting );
  OBD_ELM_Prompting = 0;  
  rprintf("AT AT1   \n\r");

  // Enable CAN Auto Formatting
  while ( !OBD_ELM_Prompting );
  OBD_ELM_Prompting = 0;  
  rprintf("AT CAF1  \n\r");

  // Enable CAN flow control
  while ( !OBD_ELM_Prompting );
  OBD_ELM_Prompting = 0;  
  rprintf("AT CFC1  \n\r");

  // Display DLC off(0) on(1)
  while ( !OBD_ELM_Prompting );
  OBD_ELM_Prompting = 0;
  rprintf("AT D0    \n\r");
}

//------------------------------------------------------------------------------------------------
// Takes a single command from TX ringbuffer and sends it to ELM327
//------------------------------------------------------------------------------------------------
void OBD_SendELMCmd()
{
  //int x = OBD_TXBFR_GetBuffFill();
  static char c;
  //printf("\n %d \n", x );
  if ( OBD_ELM_Prompting && OBD_TXBFR_BuffFill )
  {
    OBD_ELM_Prompting = 0;
    while ( !OBD_TXBFR_GetData( &c ) )
    {
      //OBD_TXBFR_GetData( &c );
      while( !U2STAbits.TRMT );
      U2TXREG = c;
      if (c == '\r')
      {
        return;
      }
    }  
  }  
}  

//------------------------------------------------------------------------------------------------
// UART2 data received interrupt
//------------------------------------------------------------------------------------------------
void __attribute__ ((interrupt, no_auto_psv)) _U2RXInterrupt( void ) 
{
  //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
  //return;
		char rd = U2RXREG;
    if ( rd != 0 )
    {
      if ( rd == '>' )
		  {
  		  OBD_ELM_Prompting = 1;
      }	
      OBD_RXBFR_PutData(rd);	
    }  
    IFS1bits.U2RXIF = 0;  
}


//------------------------------------------------------------------------------------------------
// OBD RX RING BUFFER
//------------------------------------------------------------------------------------------------

void OBD_RXBFR_Clear()
{
  OBD_RXBFR_WritePos = 0;
  OBD_RXBFR_ReadPos = 0;
  OBD_RXBFR_BuffFill = 0;
}

char OBD_RXBFR_PutData(char data)
{ 
  if (OBD_RXBFR_BuffFill >= OBD_RXBUFF_SIZE)
    {
      return 1;
    }  
  else
  {
    OBD_RXBFR_Data[OBD_RXBFR_WritePos] = data;
    OBD_RXBFR_WritePos++;
    if (OBD_RXBFR_WritePos >= OBD_RXBUFF_SIZE)
      OBD_RXBFR_WritePos = 0;
    OBD_RXBFR_BuffFill++;
    return 0;
  }
}

char OBD_RXBFR_GetData(char *data)
{
  if (OBD_RXBFR_BuffFill <= 0)
    {
      return 1;
    }  
  else
  {
    *data = OBD_RXBFR_Data[OBD_RXBFR_ReadPos];
    OBD_RXBFR_ReadPos++;
    if (OBD_RXBFR_ReadPos >= OBD_RXBUFF_SIZE)
      OBD_RXBFR_ReadPos = 0;
    OBD_RXBFR_BuffFill--;
    return 0;
  }
}

int OBD_RXBFR_GetBuffFill()
{
  return OBD_RXBFR_BuffFill;
}


//------------------------------------------------------------------------------------------------
// OBD TX RING BUFFER
//------------------------------------------------------------------------------------------------

void OBD_TXBFR_Clear()
{
  OBD_TXBFR_WritePos = 0;
  OBD_TXBFR_ReadPos = 0;
  OBD_TXBFR_BuffFill = 0;
}

inline char OBD_TXBFR_PutData(char data)
{
  if (OBD_TXBFR_BuffFill >= OBD_TXBUFF_SIZE)
    return 1;
  else
  {
    OBD_TXBFR_Data[OBD_TXBFR_WritePos] = data;
    OBD_TXBFR_WritePos++;
    if (OBD_TXBFR_WritePos >= OBD_TXBUFF_SIZE)
      OBD_TXBFR_WritePos = 0;
    OBD_TXBFR_BuffFill++;
    return 0;
  }
}

inline char OBD_TXBFR_GetData(char *data)
{
  if (OBD_TXBFR_BuffFill <= 0)
    return 1;
  else
  {
    *data = OBD_TXBFR_Data[OBD_TXBFR_ReadPos];
    OBD_TXBFR_ReadPos++;
    if (OBD_TXBFR_ReadPos >= OBD_TXBUFF_SIZE)
      OBD_TXBFR_ReadPos = 0;
    OBD_TXBFR_BuffFill--;
    return 0;
  }
}

int OBD_TXBFR_GetBuffFill()
{
  return OBD_TXBFR_BuffFill;
}


#endif //__OBD_comm_C
