#include <p24fxxxx.h>
#define _CURRENT_ADC_C_
#include "main.h"
#include "current_adc.h"
#include "debug.h"

unsigned char ADC1currentState; // Current ADC1 conversion state 
unsigned char ADC1ConversionStep; // Current ADC1 conversion step
unsigned char ADC2currentState; // Current ADC1 conversion state
unsigned char ADC2ConversionStep; // Current ADC1 conversion step

unsigned char AOcurrentState; /* Dodane */

__attribute__((far)) unsigned short avgBuffer[18]; // buffer where current samples are averaged
unsigned char currAvgStep; // current step of averaging process

__attribute__((far)) unsigned short CADC_Offsets[18]; // offset values

unsigned char currentReadoutFinished = 1; // flag if current measurement full routine has finished

unsigned int i;

unsigned char AO1_proccessed = 0;
unsigned char AO2_proccessed = 0;

#define CurrentMaxLostMeasurements 5

//
// Starts the current measurement
//
void BeginCurrentReadout( void )
{
   if ( !currentReadoutFinished ) // if previous conversion is still not completed
   {
      workData.currAdcLostFramesCount++; // increase the number of lost frames
   }    
   currentReadoutFinished = 0;
   ADC1currentState = STATE_INIT;
   currentADC1SR();
}      


//
// Initializes the current measurement system
//
void currentADCInit( void ) {
   
   
   CADC_nCS1_HIGH();// = 1;
   CADC_nCS2 = 1;
   workData.currAdcLostFramesCount = 0;
   currentReadoutFinished = 1;
   currAvgStep = 0;
   ADC1currentState = STATE_INIT;
   ADC2currentState = STATE_INIT;   
  
   //Timer 1 init
   T1CON = 0x0000;
	T1CONbits.TCKPS = 0x00;        // set the prescaler to 256
	TMR1 = 0x0000;
   IFS0bits.T1IF = 0;            // Reset the flag
   IPC0bits.T1IP = 6;            // Set the interrupt priority
   IEC0bits.T1IE = 1;            // Enable the interrupt
   
   INTCON2bits.INT2EP = 0;
   INTCON2bits.INT3EP = 0;
   
   IFS1bits.INT2IF = 0;
   IFS3bits.INT3IF = 0;
   
   IPC7bits.INT2IP = 5;
   IPC13bits.INT3IP = 5;
   
   IEC1bits.INT2IE = 1;
   IEC3bits.INT3IE = 1;
   initSPI();
}

//
// Reads the current ADC offsets
// PWM drivers must be disabled before call !
//
void CADC_GetOffsets()
{
 //   rprintf("\n\n\nReading offsets: \n");
  int waitMaxCount = 3000;
  int delay;
  int i;
  
  BeginCurrentReadout();
  
  while ( !currentReadoutFinished && waitMaxCount)
  {
    waitMaxCount--;
    for (delay = 0; delay < 500; delay++);
  }  
  
  if (currentReadoutFinished)
  {
    //rprintf("\n\nOffsets read: \n");
    for (i = 0; i < 18; i++)
    {
      CADC_Offsets[i] = workData.ADCReadOutValues[i];
    //  rprintf("Channel %d: %d\n", i, CADC_Offsets[i]);
    }
  }  
}  

//
// First ADC EndOfConversion interrupt
//
void __attribute__ ((interrupt, no_auto_psv)) _INT2Interrupt( void ) {
   
      IFS1bits.INT2IF = 0;  
      //CADC_nCS1_LOW();
      //for (kkk = 0; kkk < 2; kkk++);
      //writeSPI( 8 << 12 );
   //debug( "INT2\r\n" );
   currentADC1SR();

}

//
// Second ADC EndOfConversion interrupt
//
void __attribute__ ((interrupt, no_auto_psv)) _INT3Interrupt( void ) {
   
   //debug( "INT3\r\n" );
   currentADC2SR();
   IFS3bits.INT3IF = 0;
}

//
// SPI1 interrupt routine (when data from ADC comes)
//
void __attribute__ ((interrupt, no_auto_psv)) _SPI1Interrupt( void ) 
{
  //rprintf("s");
   CADC_nCS1_HIGH();// = 1;
   CADC_nCS2 = 1;
   AO_nCS = 1;

   /* Dodane */
   if ((AOcurrentState == STATE_AO2) || (AOcurrentState == STATE_END))
   	 updateAOvalue();


   IFS0bits.SPI1IF = 0;
//   debug( "SPI Int\r\n" );
}

//
// First ADC measurement routine
//
void currentADC1SR( void ) 
{
   //return;
   unsigned short channelAddress = 0;
   int readoutValueIndex = 0;
   unsigned short temp;
   //unsigned delay;
   //unsigned int j;

   //IEC0bits.SPI1IE         = 1;	// enable interrupt   
   //IFS0bits.SPI1IF         = 0;	// clear interrupt flag

//CADC_nCS1_HIGH();
//for (delay = 0; delay < 3; delay++);
   switch( ADC1currentState ) 
   {    
      case STATE_INIT:
      {   
        //rprintf( "init\n"); 
         i = 0;
         ADC1ConversionStep = 0;

         //address of the first channel
         channelAddress = 0; //ADC1channelsToRead[0] << 8;   
         
         //The value currently being in the SPI buffer is incorrect, thus ignore it.
         readSPI();
           
         //Chip select of the second ADC     
                
         CADC_nCS1_LOW();//= 0;
         //for (delay = 0; delay < 3; delay++);
             
         //address the first channel (thus call the conversion)
         writeSPI( channelAddress );

         ADC1currentState = STATE_ADC_RUNNING;           
         break;            
      }
        
      case STATE_ADC_RUNNING:

      {
        //rprintf( "r%d\n", ADC1ConversionStep); 
         //switch to next convesionStep
         ADC1ConversionStep += 1;
         
         //assign proper address to the variable
         if (ADC1ConversionStep >= ( 9 -1 ) )
            channelAddress = 8 << 12; //the last channel address
         else
            channelAddress = ADC1ConversionStep << 12; //address corresponding to the current step
         
         //figure out which channel value is currently in the buffer
         readoutValueIndex = ADC1ConversionStep - 2;//there's 2 step lag
         
         //read the value currently being in the SPI input buffer.
         //Note that this must be done before the chip select goes active
         //For understanding refer to TL1543 appNote page 15.
         temp = readSPI();
         if ( readoutValueIndex >=0 )
         {   
           //rprintf( "%d\n", temp >> 6); 
           avgBuffer[ readoutValueIndex ] += temp >> 6;        
            //avgBuffer[ readoutValueIndex ] += temp >> 6;
         }
         //if this is the last step (reading last value) go to idle                
         if (ADC1ConversionStep >= 9+1 )
         {
            ADC1currentState = STATE_IDLE;  
//               for (j = 0; j < 18; j++)
//               {
//                  workData.ADCReadOutValues[j] = avgBuffer[j] >> CADC_AVG_BITSHIFT;
//                  avgBuffer[j] = 0;
//               }
//               currentReadoutFinished = 1;          
            ADC2currentState = STATE_INIT;
            currentADC2SR(); 
         }  
         else
         {
                    //Chip select of the first ADC            
         CADC_nCS1_LOW();// = 0;    
         //for (delay = 0; delay < 3; delay++);   
            //address the next channel (thus call the conversion)
            writeSPI( channelAddress );   
         }
       
         break;
      }  
         
      case STATE_IDLE:
      {
        //rprintf( "idle\n");
         readSPI();
         break;
      }
   
      default:
      {
        //rprintf( "def\n");
         readSPI();
         ADC1currentState = STATE_IDLE;
         break;
      }
   }       
}   

//
// Second ADC measurement routine
//
void currentADC2SR( void ) {

   unsigned short channelAddress = 0;
   char readoutValueIndex = 0;
   unsigned short temp;
   unsigned int j;

   switch( ADC2currentState ) 
   {    
      case STATE_INIT:
      {   
         i = 0;
         ADC2ConversionStep = 0;

         //address of the first channel
         channelAddress = 0;//ADC2channelsToRead[0] << 8;   
         
         //The value currently being in the SPI buffer is incorrect, thus ignore it.
         readSPI();
           
         //Chip select of the second ADC            
         CADC_nCS2 = 0;
 
         writeSPI( channelAddress );

         ADC2currentState = STATE_ADC_RUNNING;           
         break;            
      }
        
      case STATE_ADC_RUNNING:

      {
         //switch to next convesionStep
         ADC2ConversionStep += 1;
         
         //assign proper address to the variable
         if (ADC2ConversionStep >= ( 9 -1 ) )
            channelAddress = 8 << 12;
         else
            channelAddress = ADC2ConversionStep << 12;
         
         //figure out which channel value is currently in the buffer
         readoutValueIndex = ADC2ConversionStep - 2;//there's 2 step lag
         
         //read the value currently being in the SPI input buffer.
         //Note that this must be done before the chip select goes active
         //For understanding refer to TL1543 appNote page 15.
         temp = readSPI();
         if ( readoutValueIndex >=0 )
         {           
            avgBuffer[ 9 + readoutValueIndex] += temp >> 6;
         }
        
         //Chip select of the first ADC            
         CADC_nCS2 = 0;      
         
 
         //if this is the last step (reading last value) go to idle              
         if (ADC2ConversionStep >= (9 + 1) )
         {
            currAvgStep += 1;
            if (currAvgStep == CADC_AVG_SAMPLES)
            {
               currAvgStep = 0;
               ADC2currentState = STATE_IDLE;
               for (j = 0; j < 18; j++)
               {
                  workData.ADCReadOutValues[j] = ( avgBuffer[j] >> CADC_AVG_BITSHIFT );
//                  if (workData.ADCReadOutValues[j] > CADC_Offsets[j] ) 
//                  {
//                    workData.ADCReadOutValues[j] -= CADC_Offsets[j];
//                  }
//                  else
//                  {
//                    workData.ADCReadOutValues[j] = 0;
//                  }    
                  avgBuffer[j] = 0;
               }
			   /* Zmiana */
               //currentReadoutFinished = 1;
			   AOcurrentState = STATE_AO1;
			   updateAOvalue();
            }
            else
            {
               ADC2currentState = STATE_IDLE;
			   ADC1currentState = STATE_INIT;
               currentADC1SR();              
            }
         } 
         else
         {
            //address the next channel (thus call the conversion)
            writeSPI( channelAddress );  
         }          
         break;
      }  
         
      case STATE_IDLE:
      {
         readSPI();
         break;
      }
   
      default:
      {
         readSPI();
         ADC2currentState = STATE_IDLE;
         break;
      }
   }           
}
/* Dodane */
void updateAOvalue()
{
	unsigned short tmpAO;
    switch( AOcurrentState ) 
    {    
    	case STATE_AO1:
		{
			tmpAO = 0x5000 | ((unsigned short)workData.DAQ_AO1value << 2);
			AO_nCS = 0;
			AOcurrentState = STATE_AO2;
			writeSPI(tmpAO);
			break;
		}
    	case STATE_AO2:
		{
			tmpAO = 0xD000 | ((unsigned short)workData.DAQ_AO2value << 2);
			AO_nCS = 0;
			AOcurrentState = STATE_END;
			writeSPI(tmpAO);
			break;
		}
		case STATE_END:
		{
			currentReadoutFinished = 1;
			break;
		}
		default:
		{
			AOcurrentState = STATE_END;
		   	break;
		}
	}
}   
//
// Writes to the SPI 1 buffer
//
void writeSPI( unsigned short data ) {
   SPI1BUF = data;
}

//
// Reads from the SPI1 buffer
//
unsigned short readSPI( void ) {
   return SPI1BUF;
}   

//
// Initializes the SPI1
//
void initSPI( void ) {

   SPI1STATbits.SPIEN 		= 0;	// disable SPI port
   SPI1STATbits.SPISIDL 	= 0; 	// Continue module operation in Idle mode
    
   SPI1BUF 				      = 0;   	// clear SPI buffer
    
   IFS0bits.SPI1IF         = 0;	// clear interrupt flag
   IEC0bits.SPI1IE         = 0;	// disable interrupt
   IPC2bits.SPI1IP         = 6; // set interrupt priority to 6
    
   SPI1CON1bits.SSEN       = 0;
   SPI1CON1bits.DISSCK     = 0;	// Internal SPIx clock is enabled
   SPI1CON1bits.DISSDO     = 0;	// SDOx pin is controlled by the module
   SPI1CON1bits.MODE16     = 1;	// set in 16-bit mode, clear in 8-bit mode
   SPI1CON1bits.SMP        = 1;	// Input data sampled at middle of data output time
   SPI1CON1bits.CKP        = 0;	// CKP and CKE is subject to change ...
   SPI1CON1bits.CKE        = 1;	// ... based on your communication mode.
	SPI1CON1bits.MSTEN      = 1; 	// 1 =  Master mode; 0 =  Slave mode
	SPI1CON1bits.SPRE       = 5; 	// Secondary Prescaler = 4:1
	SPI1CON1bits.PPRE       = 2; 	// Primary Prescaler = 4:1

   SPI1CON2                = 0;	// non-framed mode
	SPI1CON2bits.SPIFE      = 1;
	
		SPI1CON2bits.SPIBEN      = 1; // enable enhanced buffer mode if you want the interrupts working
	SPI1STATbits.SISEL         = 5; // 101 =Interrupt when the last bit is shifted out of SPIxSR, now the transmit is complete

   SPI1STATbits.SPIEN      = 1; 	// enable SPI port, clear status
   IEC0bits.SPI1IE         = 1;	// enable interrupt   

}
