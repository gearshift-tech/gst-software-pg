// Test.cpp : Defines the entry point for the console application.
//

//#include <iostream>
//#include <string>
#include <stdio.h>
#include <time.h>

#include "stdafx.h"
#include "KLINE.h"

//using namespace std;

void PrintStatus()
{
  printf(" TXBFR %u %u\n", KLINE_TXBFR_BuffFill, KLINE_TXBFR_OvfCount);

}


int _tmain(int argc, _TCHAR* argv[])
{
  int i,j,k;
  while (1)
  {
    for (i = 0; i < 65534; i++)
    {
      for ( j = 0; i < 65534; i++)
      {
        for ( k = 0; i < 65534; i++)
        {

        }
      }
    }
    
    OUTPUT_DATA tmp;
    for (i = 0; i < 1; i++)
    {      
      tmp.KLINE_TX.timestamp = 3000;
  	  tmp.KLINE_TX.eventType = PHERIPHERIALS_EVT;
  	  //tmp.KLINE_TX.eventType = KLINE_TX_EVT;
  	  tmp.KLINE_TX.dataLength = 32;   
  	  
  	  tmp.PHERIPERIALS_EVT.setBrg = 1;
  	  tmp.PHERIPERIALS_EVT.KLINE_PullDown = 2;   // 0 - do not change, 1 - turn off, 2 - turn on
	    tmp.PHERIPERIALS_EVT.relayState = 2;       // 0 - do not change, 1 - turn off, 2 - turn on
	    tmp.PHERIPERIALS_EVT.wakeUpSignal = 2;     // 0 - do not change, 1 - turn off, 2 - turn on
  	  
  	  KLINE_TXBFR_PutData(tmp); 
    }

    PrintStatus();
  }
	return 0;
}



//------------------------------------------------------------------------------------------------------------------
// Timer1 interrupt routine
//
// This interrupt execution time was optimized and measured to be:
// 0.32 us (3.13MHz) for empty KLINE_TXBFR
// 1.56 us (641kHz) for KLINE_TXBFR with data but timestamp not due
// 5.06 us (198kHz) for KLINE_TX event with data lenght = 1
// 55.2 us (18.1kHz) for KLINE_TX event with data lenght = 32 (max)  
// 3.32 us (301kHz) for PHERIPERIALS event with no work to do
// 5.06 us (198kHz) for PHERIPERIALS event with full work to do
//------------------------------------------------------------------------------------------------------------------
void  _T1Interrupt( void )
{
  int l = 0;
  // Increase the actual timestamp
  KLINE_TimeStamp += 1;
    
    KLINE_TXBFR_BuffFill = 10;
  // It is assumed that if the buffer is empty then KLINE_TXBFR_ReadPtr->KLINE_TX.timestamp is always equal to 0xFFFFFFFF - max possible value
  if( KLINE_TXBFR_BuffFill && KLINE_TXBFR_ReadPtr->KLINE_TX.timestamp <= KLINE_TimeStamp)
  {
    // If the first data in the buffer is due time, process it and remove it from the buffer
    // It must be processed first because we're using a pointer and we don't want the data to be overwritten in the meantime

    if (KLINE_TXBFR_ReadPtr->KLINE_TX.eventType == PHERIPHERIALS_EVT)
    {
      // If the first data in buffer is a pheriperials event

      if (KLINE_TXBFR_ReadPtr->PHERIPERIALS_EVT.setBrg)
      {
        //U2BRG = KLINE_TXBFR_ReadPtr->PHERIPERIALS_EVT.BRG;
      }

      if (KLINE_TXBFR_ReadPtr->PHERIPERIALS_EVT.KLINE_PullDown != 0)
      {
        // If evtp is not 0, it means it will be either 1 or 2
        //if (KLINE_TXBFR_ReadPtr->PHERIPERIALS_EVT.KLINE_PullDown == 1)
          //KLINE_PULLDOWN_OFF(); // Disable command
        //else
          //KLINE_PULLDOWN_ON(); // Enable command
      }

      if (KLINE_TXBFR_ReadPtr->PHERIPERIALS_EVT.relayState != 0)
      {
        // If evtp is not 0, it means it will be either 1 or 2
        //if (KLINE_TXBFR_ReadPtr->PHERIPERIALS_EVT.relayState == 1)
          //RELAY_OFF(); // Disable command
        //else
          //RELAY_ON(); // Enable command
      }

      if (KLINE_TXBFR_ReadPtr->PHERIPERIALS_EVT.wakeUpSignal != 0)
      {
        // If it is not 0, it means it will be either 1 or 2
        //if (KLINE_TXBFR_ReadPtr->PHERIPERIALS_EVT.wakeUpSignal == 1)
        //  WAKEUP_SIG_OFF(); // Disable command
        //else
       //   WAKEUP_SIG_ON(); // Enable command
      }

    }
    else
    {
      // If the first data in the buffer is KLINE TX data

      volatile unsigned char *dataArr  = KLINE_TXBFR_ReadPtr->KLINE_TX.data;
      int dataLength = KLINE_TXBFR_ReadPtr->KLINE_TX.dataLength;
      for (l = 0; l < dataLength; l++)
      {
        // We DO NOT care about the buffer overflowing as it is too time expensive
        // In normal operation it should not happen
        *KLINE_TXQUEUE_WritePtr = *dataArr++;

        if (++KLINE_TXQUEUE_WritePtr >= KLINE_TXQUEUE_DataEnd)
        {
          KLINE_TXQUEUE_WritePtr = KLINE_TXQUEUE_Data;
        }
        KLINE_TXQUEUE_BuffFill++;
      }

      // Remove the data from the buffer
      if (++KLINE_TXBFR_ReadPtr >= KLINE_TXBFR_DataEnd)
      {
        KLINE_TXBFR_ReadPtr = KLINE_TXBFR_Data;
      }
      KLINE_TXBFR_BuffFill--;
    }
  }

}




//------------------------------------------------------------------------------------------------------------------
// OBD RX RING BUFFER
//------------------------------------------------------------------------------------------------------------------

void KLINE_RXBFR_Clear()
{
  KLINE_RXBFR_WritePos = 0;
  KLINE_RXBFR_WritePtr = KLINE_RXBFR_Data;
  KLINE_RXBFR_ReadPos = 0;
  KLINE_RXBFR_ReadPtr = KLINE_RXBFR_Data;
  KLINE_RXBFR_BuffFill = 0;
  KLINE_RXBFR_OvfCount = 0;
}

char KLINE_RXBFR_PutData(KLINE_RXDATA data)
{ 
  if (KLINE_RXBFR_BuffFill >= KLINE_RXBUFF_SIZE)
  {
    KLINE_RXBFR_OvfCount += 1;
    return 1;
  }  
  else
  {
    KLINE_RXBFR_Data[KLINE_RXBFR_WritePos] = data;
    KLINE_RXBFR_WritePos++;
    if (KLINE_RXBFR_WritePos >= KLINE_RXBUFF_SIZE)
      KLINE_RXBFR_WritePos = 0;
    KLINE_RXBFR_BuffFill++;
    return 0;
  }
}

char KLINE_RXBFR_GetData(KLINE_RXDATA *data)
{
  if (KLINE_RXBFR_BuffFill <= 0)
    {
      return 1;
    }  
  else
  {
    *data = KLINE_RXBFR_Data[KLINE_RXBFR_ReadPos];
    KLINE_RXBFR_ReadPos++;
    if (KLINE_RXBFR_ReadPos >= KLINE_RXBUFF_SIZE)
      KLINE_RXBFR_ReadPos = 0;
    KLINE_RXBFR_BuffFill--;
    return 0;
  }
}

int KLINE_RXBFR_GetBuffFill()
{
  return KLINE_RXBFR_BuffFill;
}


//------------------------------------------------------------------------------------------------------------------
// OBD TX RING BUFFER
//------------------------------------------------------------------------------------------------------------------

void KLINE_TXBFR_Clear()
{
  KLINE_TXBFR_DataEnd = KLINE_TXBFR_Data + KLINE_TXBUFF_SIZE;
  KLINE_TXBFR_WritePtr = KLINE_TXBFR_Data;
  KLINE_TXBFR_ReadPtr = KLINE_TXBFR_Data;
  KLINE_TXBFR_BuffFill = 0;
  KLINE_TXBFR_OvfCount = 0;
}

char KLINE_TXBFR_PutData(OUTPUT_DATA data)
{
  if (KLINE_TXBFR_BuffFill >= KLINE_TXBUFF_SIZE)
  {
    KLINE_TXBFR_OvfCount += 1;
    return 1;
  }
  else
  {
    *KLINE_TXBFR_WritePtr = data;
    if (++KLINE_TXBFR_WritePtr >= KLINE_TXBFR_DataEnd)
    {
      KLINE_TXBFR_WritePtr = KLINE_TXBFR_Data;
    }
    KLINE_TXBFR_BuffFill++;
    return 0;
  }
}

char KLINE_TXBFR_GetData(OUTPUT_DATA *data)
{
  if (KLINE_TXBFR_BuffFill <= 0)
    return 1;
  else
  {
    if (++KLINE_TXBFR_ReadPtr >= KLINE_TXBFR_DataEnd)
    {
      KLINE_TXBFR_ReadPtr = KLINE_TXBFR_Data;
    }
    KLINE_TXBFR_BuffFill--;
    return 0;
  }
}

int KLINE_TXBFR_GetBuffFill()
{
  return KLINE_TXBFR_BuffFill;
}


//------------------------------------------------------------------------------------------------------------------
// K-LINE TX QUEUE (bytes that should be sent immediately)
//------------------------------------------------------------------------------------------------------------------

void KLINE_TXQUEUE_Clear()
{
  KLINE_TXQUEUE_DataEnd = KLINE_TXQUEUE_Data + KLINE_TXQUEUE_SIZE;
  KLINE_TXQUEUE_WritePtr = KLINE_TXQUEUE_Data;
  KLINE_TXQUEUE_ReadPtr = KLINE_TXQUEUE_Data;
  KLINE_TXQUEUE_BuffFill = 0;
  KLINE_TXQUEUE_OvfCount = 0;
}

char KLINE_TXQUEUE_PutData(char data)
{
  if (KLINE_TXQUEUE_BuffFill >= KLINE_TXQUEUE_SIZE)
  {
    KLINE_TXQUEUE_OvfCount += 1;
    return 1;
  }    
  else
  {
    *KLINE_TXQUEUE_WritePtr = data;

    if (++KLINE_TXQUEUE_WritePtr >= KLINE_TXQUEUE_DataEnd)
    {
      KLINE_TXQUEUE_WritePtr = KLINE_TXQUEUE_Data;
    }
    KLINE_TXQUEUE_BuffFill++;
    return 0;
  }
}

char KLINE_TXQUEUE_GetData(char *data)
{
  if (KLINE_TXQUEUE_BuffFill <= 0)
    return 1;
  else
  {
    *data = *KLINE_TXQUEUE_ReadPtr;
    
    if (++KLINE_TXQUEUE_ReadPtr >= KLINE_TXQUEUE_DataEnd)
    {
      KLINE_TXQUEUE_ReadPtr = KLINE_TXQUEUE_Data;
    }    
    KLINE_TXQUEUE_BuffFill--;
    return 0;
  }
}

int KLINE_TXQUEUE_GetBuffFill()
{
  return KLINE_TXQUEUE_BuffFill;
}

