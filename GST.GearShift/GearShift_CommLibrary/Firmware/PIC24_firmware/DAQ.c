#ifndef __DAQ_routines_C
#define __DAQ_routines_C

#include <p24fxxxx.h>
#include "main.h"

#include "current_adc.h"
#include "DAQ.h"
#include "pwm.h"
#include "UsbSoftLayer.h"

unsigned char DAQ_readPressures = 0;
unsigned char DAQ_readCurrents = 0;

unsigned long DAQ_actualTimestamp = 0;

unsigned long currDAQfrmID = 0;



// Initializes DAQ mechanism
void DAQ_Init(void)
{
    // Reset the timestamp to 0
    DAQ_actualTimestamp = 0;
    //Timer 1 init
    T1CON = 0x0000;
    T1CONbits.TCKPS = 3;        // set the prescaler to 256
    //timer1 compare value to get 1kHz trigger
    // timer input clock = 16 MHz (32/2), prescaler set to 256
    PR1  = 624;
    TMR1 = 0x0000;
    IFS0bits.T1IF = 0;            // Reset the flag
    IPC0bits.T1IP = 6;            // Set the interrupt priority
    T1CONbits.TON = 0;            // Keep the timer1 disabled until DAQ_Start
    TMR1 = 0;                     // Reset the timer1 counter
    IEC0bits.T1IE = 0;            // Keep the timer1 interrupts disabled until DAQ_Start
    //dataArrivedOld = 0;
}  

void DAQ_Start( void )
{
//   unsigned char num;
   unsigned char * tab;
   unsigned char i;
   // If the mechanism is already running, quit
   if( DAQ_appState != APP_STATE_STOPPED)
   {
      return;
   }

  if (DAQ_RXBFR_BuffFill > 0)
  {
     currDAQfrmID = DAQ_RXBFR_Data[DAQ_RXBFR_ReadPos].packetID;

     tab = DAQ_RXBFR_Data[DAQ_RXBFR_ReadPos].pwmValue;
     for( i = 0; i < PWM_DRV_COUNT; i++ )
     {
        if ( workData.DAQ_PWMManualDriveEnabled[i] )
        {
          workData.DAQ_PWMDutyCycles[ i ] = workData.DAQ_PWMManualDutyCycles[ i ];
        }
        else
        {
          workData.DAQ_PWMDutyCycles[ i ] = tab[ i ];
        }
     }

     workData.DAQ_AO1value = DAQ_RXBFR_Data[DAQ_RXBFR_ReadPos].AO1value;
     workData.DAQ_AO2value = DAQ_RXBFR_Data[DAQ_RXBFR_ReadPos].AO2value;

    // Properly serve the DAQ_RXBFR variables
    DAQ_RXBFR_ReadPos++;
    if (DAQ_RXBFR_ReadPos >= DAQ_RXBUFF_SIZE)
      DAQ_RXBFR_ReadPos = 0;
    DAQ_RXBFR_BuffFill--;
  }
  else
  {
      for( i = 0; i < PWM_DRV_COUNT; i++ )
      {
         workData.DAQ_PWMDutyCycles[ i ] = 0;
      }
      workData.DAQ_AO1value = 0;
      workData.DAQ_AO2value = 0;
   }
   // Enable the PWM outputs
   pwmStart();
   // Set the Application state value properly
   DAQ_appState = APP_STATE_RUNNING;
   // Enable timer1 and interrupts
   TMR1 = 0;
   IEC0bits.T1IE = 1;
   T1CONbits.TON = 1;
   

 /*
   //////////////////////////////////////////////////////////////
   if( buffFill )
   {
      //FIX_ME: Uncomment these two !!!!!!!!!!!!!!!!!! remove the first value from the buffer!!!
      BeginCurrentReadout();
      startIntAD();
      num = inputDataBuffReadPos;
      tab = DAQ_inputDataBuff[ num ].pwmValue;
      for( i = 0; i < PWM_DRV_COUNT; i++ )
      {
         workData.PWMDutyCycle[ i ] = tab[ i ];
      }
      workData.AO1value = DAQ_inputDataBuff[ num ].AO1value;
      workData.AO2value = DAQ_inputDataBuff[ num ].AO2value;
   }
   else
   {
      for( i = 0; i < PWM_DRV_COUNT; i++ )
      {
         workData.PWMDutyCycle[ i ] = 0;
      }
      workData.AO1value = 0;
      workData.AO2value = 0;
   }
   pwmStart();
   appStateVal = APP_STATE_RUNNING;
   IEC0bits.T1IE = 1;*/
}

void DAQ_Stop( void )
{
   // Disable timer, reset counter and disable interrupt
   IEC0bits.T1IE = 0;
   T1CONbits.TON = 0;
   DAQ_appState = APP_STATE_STOPPED;
}

//
// Timer1 interrupt routine
//
void __attribute__((__interrupt__, __no_auto_psv__)) _T1Interrupt( void )
{
   unsigned char num;
   unsigned char i;
   unsigned char * tab;
   unsigned short * sTab;

   // Clear timer compare flag
   IFS0bits.T1IF = 0;

   // Increase the actual timestamp
   DAQ_actualTimestamp += 1;

   //// do check if the connection was not broken
   //timerTicksWithouIncomingPackets++;
   //if ( timerTicksWithouIncomingPackets > 30 )
   //{
   //  //rprintf("\n\nTUMNIEWYJEBALO!!\n\n");
   //  DAQ_Stop();
   //  // turn off solenoids, disable message transmission over CAN, OBD, USB ( to not to cause problems upon connect)
   //  USBDisconnected();
   //  return;
   //

   if( DAQ_appState == APP_STATE_RUNNING )
   {
       // Normally I'd use the DAQ_RXBFR_GetData(TDAQInputData *data) function, but for performance gain I'm using it's inside code


  // Check if there's any data in the buffer to process
  if (DAQ_RXBFR_BuffFill > 0)
  {

    currDAQfrmID = DAQ_RXBFR_Data[DAQ_RXBFR_ReadPos].packetID;

         tab = DAQ_RXBFR_Data[DAQ_RXBFR_ReadPos].pwmValue;
         for( i = 0; i < PWM_DRV_COUNT; i++ )
         {
            if ( workData.DAQ_PWMManualDriveEnabled[i] )
            {
              workData.DAQ_PWMDutyCycles[ i ] = workData.DAQ_PWMManualDutyCycles[ i ];
            }
            else
            {
              workData.DAQ_PWMDutyCycles[ i ] = tab[ i ];
            }
         }

         workData.DAQ_AO1value = DAQ_RXBFR_Data[DAQ_RXBFR_ReadPos].AO1value;
         workData.DAQ_AO2value = DAQ_RXBFR_Data[DAQ_RXBFR_ReadPos].AO2value;

         CalcOCRegisters();

      
    // Properly serve the DAQ_RXBFR variables
    DAQ_RXBFR_ReadPos++;
    if (DAQ_RXBFR_ReadPos >= DAQ_RXBUFF_SIZE)
      DAQ_RXBFR_ReadPos = 0;
    DAQ_RXBFR_BuffFill--;
  }
  /*
  else
  {
    tab = DAQ_RXBFR_Data[DAQ_RXBFR_ReadPos].pwmValue;
        for( i = 0; i < PWM_DRV_COUNT; i++ )
        {
          //if ( workData.DAQ_PWMManualDriveEnabled[i] )
          //{
          //  workData.DAQ_PWMDutyCycles[ i ] = workData.DAQ_PWMManualDutyCycles[ i ];
          //}
            if ( workData.DAQ_PWMManualDriveEnabled[ i ])
            {
              workData.DAQ_PWMDutyCycles[ i ] = workData.DAQ_PWMManualDutyCycles[ i ];
              my_putc('1');
            }
            else
            {
              //workData.DAQ_PWMDutyCycles[ i ] = tab[ i ];
              my_putc('0');
            }
        }
               my_putc('\n');
           my_putc('\r');
        CalcOCRegisters();
  }
   * */



 /*
      num = inputDataBuffReadPos;

      if( DAQ_inputDataBuff[ num ].used )
      {
         currDAQfrmID = DAQ_inputDataBuff[ num ].packetID;
         tab = DAQ_inputDataBuff[ num ].pwmValue;
         for( i = 0; i < PWM_DRV_COUNT; i++ )
         {
            if ( workData.PWMManualDutyCycleEn[i] )
            {
              workData.PWMDutyCycle[ i ] = workData.PWMManualDutyCycle[ i ];
            }
            else
            {
              workData.PWMDutyCycle[ i ] = tab[ i ];
            }
         }

         workData.AO1value = DAQ_inputDataBuff[ num ].AO1value;
         workData.AO2value = DAQ_inputDataBuff[ num ].AO2value;

         DAQ_inputDataBuff[ num ].used = 0;
         //debug("%d \n", buffID);

         CalcOCRegisters();
         //calcCmpRegisters();
         //workData.reloadTmr = 1;

         inputDataBuffReadPos++;
         if( inputDataBuffReadPos >= NUM_INPUT_DATA_BUFF )
         {
            inputDataBuffReadPos = 0;
         }

         buffFill--;
      }
      else
      {
        for( i = 0; i < PWM_DRV_COUNT; i++ )
        {
          if ( workData.PWMManualDutyCycleEn[i] )
          {
            workData.PWMDutyCycle[ i ] = workData.PWMManualDutyCycle[ i ];
          }
        }
        CalcOCRegisters();
      }*/
      //rprintf("%u\n",(int)packetID);
      num = outputDataBuffWritePos;

      if( !DAQ_outputDataBuff[ num ].used )
      {
         DAQ_outputDataBuff[ num ].packetRespToID = currDAQfrmID;
         sTab = DAQ_outputDataBuff[ num ].pressure;

         for( i = 0; i < PRESS_SENS_COUNT; i++ )
         {
            //sTab[ i ] = workData.newDutyCycle[ j ];//;intADValues[ i ];
            sTab[ i ] = intADValues[ i ];

         }
         sTab = DAQ_outputDataBuff[ num ].current;

         for( i = 0; i < CURR_SENS_COUNT; i++ )
         {
            //sTab[ i ] = workData.newDutyCycle[ j ];//workData.ADCReadOutValues[ i ];
            sTab[ i ] = workData.ADCReadOutValues[ i ];
            //j++;
            //if( j >= PWM_DRV_COUNT )
            //{
            //   j = 0;
            //}
         }
         outputDataBuffWritePos++;
         if( outputDataBuffWritePos >= NUM_OUTPUT_DATA_BUFF )
         {
            outputDataBuffWritePos = 0;
         }
      }

      BeginCurrentReadout();
      startIntAD();
   }
}

//------------------------------------------------------------------------------------------------
// DAQ RX RING BUFFER
//------------------------------------------------------------------------------------------------

void DAQ_RXBFR_Clear()
{
  DAQ_RXBFR_WritePos = 0;
  DAQ_RXBFR_ReadPos = 0;
  DAQ_RXBFR_BuffFill = 0;
}

char DAQ_RXBFR_PutData(TDAQInputData data)
{
  if (DAQ_RXBFR_BuffFill >= DAQ_RXBUFF_SIZE)
  {
      return 1;
  }  
  else
  {
    // COPY DATA HERE
    DAQ_RXBFR_WritePos++;
    if (DAQ_RXBFR_WritePos >= DAQ_RXBUFF_SIZE)
      DAQ_RXBFR_WritePos = 0;
    DAQ_RXBFR_BuffFill++;
    return 0;
  }
}

char DAQ_RXBFR_GetData(TDAQInputData *data)
{
  if (DAQ_RXBFR_BuffFill <= 0)
    return 1;
  else
  {
    // COPY DATA HERE
    DAQ_RXBFR_ReadPos++;
    if (DAQ_RXBFR_ReadPos >= DAQ_RXBUFF_SIZE)
      DAQ_RXBFR_ReadPos = 0;
    DAQ_RXBFR_BuffFill--;
    return 0;
  }
}

int DAQ_RXBFR_GetBuffFill()
{
  return DAQ_RXBFR_BuffFill;
}

/*
//------------------------------------------------------------------------------------------------
// DAQ TX RING BUFFER
//------------------------------------------------------------------------------------------------

void DAQ_TXBFR_Clear()
{
  DAQ_TXBFR_WritePos = 0;
  DAQ_TXBFR_ReadPos = 0;
  DAQ_TXBFR_BuffFill = 0;
  
  DAQ_FirstTxBfrTimestampValue = 0;
}

char DAQ_TXBFR_PutData(CAN_PACKET data)
{
  unsigned char i;  
  if (DAQ_TXBFR_BuffFill >= DAQ_TXBUFF_SIZE)
    return 1;
  else
  {
    if (DAQ_TXBFR_BuffFill == 0)
    {
      // If this is the first message added to the buffer, assign the value here
      DAQ_FirstTxBfrTimestampValue = data.data.timestamp;
    }  
    for (i = 0; i < sizeof(CAN_PACKET); i++)
    {
        DAQ_TXBFR_Data[CAN_TXBFR_WritePos].bytes[i] = data.bytes[i];
    }
    DAQ_TXBFR_WritePos++;
    if (DAQ_TXBFR_WritePos >= DAQ_TXBUFF_SIZE)
      DAQ_TXBFR_WritePos = 0;
    DAQ_TXBFR_BuffFill++;
    return 0;
  }
}

char DAQ_TXBFR_GetData(CAN_PACKET *data)
{
  unsigned char i;
  if (DAQ_TXBFR_BuffFill <= 0)
    return 1;
  else
  {
    for (i = 0; i < sizeof(DAQ_PACKET); i++)
    {
        data->bytes[i] = DAQ_TXBFR_Data[CAN_TXBFR_ReadPos].bytes[i];
    }
    // *data = CAN_TXBFR_Data[CAN_TXBFR_ReadPos];
    DAQ_TXBFR_ReadPos++;
    if (DAQ_TXBFR_ReadPos >= DAQ_TXBUFF_SIZE)
      DAQ_TXBFR_ReadPos = 0;
    DAQ_TXBFR_BuffFill--;
    
    // Take care of the CAN_TXBFR_BuffFill value
    if (DAQ_TXBFR_BuffFill == 0)
    {
      // If this was the last message in the buffer, zero the value
      DAQ_TXBFR_BuffFill = 0;
    }  
    else
    {
      // Otherwise assign the value from the first message in the buffer
      DAQ_TXBFR_BuffFill = CAN_TXBFR_Data[CAN_TXBFR_ReadPos].data.timestamp;
    }  
    return 0;
  }
}

int DAQ_TXBFR_GetBuffFill()
{
  return DAQ_TXBFR_BuffFill;
}
*/

#endif //__CAN_routines_C


