//
//
//       CHECK RELEASE/DEBUG CONFIG AT THE BEGINING OF MAIN.H  
//
//

#define BRICKME

#include "p24fxxxx.h"

#define _MAIN_C_

#include "main.h"
#include "pwm.h"
#include "current_adc.h"
#include "overcurrent.h"
#include "current_adc.h"
#include "uart_232.h"
#include "UI_comm.h"
#include "DAQ.h"

//#include <GenericTypeDefs>
#include <string.h>
#include <math.h>
#include <stdlib.h>

#include "UsbSoftLayer.h"
#include "device_init.h"
#include "current_adc.h"


//#include "SPI2515.h"
#include "CAN_routines.h"
#include "SPI2515.h"

#include "OBD_comm.h"

#include "GenericTypeDefs.h"
#include "HardwareProfile.h"

#include "rprintf.h"
#include "rprintf1.h"

#include "i2c.h"
#include "UI_comm.h"
#include "demoFunctions.h"
#include "NVMEM.h"

#define UI_I2C_MSG_FREQ_DIV 10

_CONFIG1( JTAGEN_OFF & GCP_OFF & GWRP_OFF & COE_OFF & FWDTEN_OFF & ICS_PGx2 )
//FIX_ME: change PLLDIV_DIV to PLLDIV_DIV4
_CONFIG2( IESO_OFF & FCKSM_CSDCMD & OSCIOFNC_ON & POSCMOD_HS & FNOSC_PRIPLL & PLLDIV_DIV4 & IOL1WAY_OFF & 0xF7FF )
_CONFIG3( WPCFG_WPCFGDIS & WPDIS_WPDIS )

unsigned char temp = 0;

#pragma udata

#define TRUE	1
#define FALSE	0


#pragma code

char Pending = 0;

extern unsigned char ADC1currentState;
extern unsigned char ADC2currentState;

unsigned long szit = 0;

//////////////////////////////////////////////////////////////////////////////////////
//                                                                                  //
//     Main function                                                                //
//                                                                                  //
//////////////////////////////////////////////////////////////////////////////////////

int main(void) 
{
	unsigned int i;//, k;
	//unsigned char dataTmp[10];
	//char lcdMsg[34];
	//unsigned short v;
	unsigned short w;
   unsigned long lol = 0;
	//int k = 0;
	int att = 0;
	int j = 0;//, kapencja = 0;
	//unsigned char dataTmp[100];
	char strTmp[32];  
	
	//
	char i2cMsgFreqDivCtr = 10;

  //delay to get rid of start-up power supply LCD interferences
   for (j = 0; j < 1000; j++)
       for (w = 0; w < 10; w++);

	// Initialize the device    
   deviceInit();
   
   //rprintf1_init();
   
   while (0)
   {
    //#ifndef PRODUCTION
       //my_putc(0xAA);
       OBD_Init();
       for ( i = 0; i < 1000; i++)
         for ( w = 0; w < 100; w++);
      //rprintf("\n\n\nDUPA\n");
    //#endif
   }
   
   
   while(0)
   {
     my_putc1('K');//rprintf1("DUPA\n");
     for ( i = 0; i < 1000; i++)
      for ( w = 0; w < 100; w++);
   }  
   
   // When performing write operation the whole page (512 instructions) must be erased first.
   PIC_Flash_Read_NVMEM();//my_eedata_bfr, 512); // Read the whole array from flash to prevent data loss when writing anything back
   // Set the flag that the correct program was uploaded via bootloader
   if ( my_eedata_bfr[0] == 0 )
   {
     //rprintf("WRITING TO FLASH");
     my_eedata_bfr[0] = 0xFA;
     PIC_Flash_Write_NVMEM();//my_eedata_bfr, 512); // Write the whole array back to flash to prevent data loss 
   }  
   
//   for (i = 0; i < 1000; i++) 
//     rprintf("fpos %d: %u\n", i, my_eedata_bfr[i]);     
//   while(1);
   
/* Test wyjsc napieciowych */
while (0)
{
  //my_putc('K');
	AO_nCS = 0;
	writeSPI( 0xD7FF);
	for (j = 0; j < 1000; j++);
	AO_nCS = 1;
	for (j = 0; j < 1000; j++);
	AO_nCS = 0;
	writeSPI( 0x5FFF);
	for (j = 0; j < 1000; j++);
	AO_nCS = 1;
	for (j = 0; j < 1000; j++);
  //u1putc('K');
}  
/* Koniec */
  CAN_Init();
  OBD_Init();

  CAN2515_Reset();
  CAN2515_Reset();


  //unsigned char lollol;
  //void CAN52515_SetBaudConfig(unsigned char _BRP, unsigned char _PS1, unsigned char _PS2, unsigned char _PRSEG, unsigned char _SJW) 
  //CAN2515_SetBitTiming(                    0,                  2,                  2,                  3,                    1);//  1M 
  //CAN2515_SetBitTiming(                    1,                  2,                  2,                  3,                    1);//  500k   
  //CAN2515_SetBitTiming(                    19,                  2,                  2,                  3,                    1);//  50k
  //CAN2515_SetBitTiming(                    49,                  5,                  4,                  6,                    2);//  10k
//  CAN2515_SetBitTiming(                    9,                  2,                  2,                  3,                    1);//  100k  
   
//  CAN_EnablePullUp();
//  CAN2515_ConfigureRXTX();  
//  CAN2515_SetNormalOperationMode();


   #ifndef PRODUCTION
    rprintf("\n\n\n\n\n MAIN________\n");
   #endif 
   
//            for (i = 0; i < NVMEM_SERIAL_LEN; i++)
//            {            
//              my_putc( my_eedata_bfr[ NVMEM_SERIAL_POS + i ] );
//              //rprintf("%u ", my_eedata_bfr[ NVMEM_SERIAL_POS + i ] );
//            }      
//            rprintf("\n");       
//            for (i = 0; i < NVMEM_GUID_LEN; i++)
//            {
//              my_putc( my_eedata_bfr[ NVMEM_GUID_POS + i ] );
//              //rprintf("%u ", my_eedata_bfr[ NVMEM_GUID_POS + i ] );
//            }     
   
   
//   workData.PWMDutyCycle[ 0 ] = 0;
//   workData.PWMDutyCycle[ 1 ] = 0;
//   workData.PWMDutyCycle[ 2 ] = 0;
//   workData.PWMDutyCycle[ 3 ] = 0;
//   workData.PWMDutyCycle[ 4 ] = 0;
//   workData.PWMDutyCycle[ 5 ] = 0;
//   workData.PWMDutyCycle[ 6 ] = 0;
//   workData.PWMDutyCycle[ 7 ] = 0;
//   workData.PWMDutyCycle[ 8 ] = 0;

  //disable USB message transmission, it will be enabled upon USB connect, 
  //will be auto disabled when usb connection error detected
   //workData.enableUsbMsgTx = 0;
   
   // Get the current ADC offsets
   CADC_GetOffsets();
   
   // Initialize the PWM values and update them on bargraphs
   for (i = 0; i < 9; i++)
   { 
      #ifndef PRODUCTION
        workData.DAQ_PWMDutyCycles[ i ] = 0;
		//workData.PWMDutyCycle[ 8 ] = 5;
      #else
        workData.DAQ_PWMDutyCycles[ i ] = 0;
      #endif
      UI_bgVals[0] = 0x01;
      UI_bgVals[i+1] = workData.DAQ_PWMDutyCycles[ i ] / 5 ;
   }

   workData.DAQ_PWMFreq = 200;
   ApplyPWMFreq();





   // CAUTION !!! - row below shall not be removed, otherwise main loop will hang on I2C3STATbits.P check    
   i2c_writeData(I2Cnr3, 0x01, (unsigned char*)UI_bgVals, 10);
   while ((flagDataSent==0) && (flagDataNACK == 0));

   UiBgDemoPyramideLeft();





   usbOutHandle = 0;
   usbInHandle  = 0;



   // Main program loop, measured execution frequency is 7kHz
    while( 1 )
    {

      lol += 1;
      if (lol > 700) //5000
      {
          lol = 0;
         // This will execute with approx 10Hz frequency
          
          // do check if the connection was not broken
          USB_LoopCountsWithoutUsbTraffic++;
          if (USB_LoopCountsWithoutUsbTraffic > USB_LoopCountsWithoutUsbTrffic)
          {
              USB_LoopCountsWithoutUsbTraffic = 0;
             //rprintf("\n\nTUMNIEWYJEBALO!!\n\n");
             DAQ_Stop();
             // turn off solenoids, disable message transmission over CAN, OBD, USB ( to not to cause problems upon connect)
             USBDisconnected();
      }


      UI_UpdateBgs();
#ifdef BRICKME
      strcpy(strTmp, "                ");
      UI_UpdateLcdRow1(strTmp);
      strcpy(strTmp, "     UNPAID     ");
      UI_UpdateLcdRow2(strTmp);
      UI_updateLcdRow1 = 1;
      UI_updateLcdRow2 = 1;
#endif

   
      } 

      
      //messages are automatically received by interrupt from MCP2515
      if ( !MCP2515_RX0BF )
      {
        CAN2515_GetRXB0Messages();
      }  
      
      if ( !MCP2515_RX1BF )
      {
        CAN2515_GetRXB1Messages();
      } 
      
      // This routine checks if the MCP2515 is not used in one of the two receive buffers interrupts
      // if MCP2515 is free, it gets locked and messages are loaded to TX buffers. 
      // In case any messages should be received (and RX interrupts had quit due to locked MCP) function reads the messages.
      CAN2515_SendBufferedMessage();  


      // serve the UI requests
      if (i2cMsgFreqDivCtr >= UI_I2C_MSG_FREQ_DIV)
      {
         i2cMsgFreqDivCtr = 0;
      }
      else
      {
         i2cMsgFreqDivCtr++;
         goto endi2ctx;
      }       
      if (UI_updateBgs && I2C3STATbits.P )  // if bargraphs should be updated via i2c and previous i2c data has been sent
      {       
         UI_updateBgs = 0;
         i2c_writeData(I2Cnr3, 0x01, (unsigned char*)UI_bgVals, 10);
         goto endi2ctx;
      }   
      if (UI_updateLcdRow1 && I2C3STATbits.P )  // if 1st lcd row should be updated via i2c and previous i2c data has been sent
      {
         UI_updateLcdRow1 = 0;
         i2c_writeData(I2Cnr3, 0x01, (unsigned char*)UI_lcdRow1, 17);
         debug("LCD row1: %s\n", UI_lcdRow1 + 1);
         goto endi2ctx;
      }   
      if (UI_updateLcdRow2 && I2C3STATbits.P )  // if 2nd lcd row should be updated via i2c and previous i2c data has been sent  
      {
         UI_updateLcdRow2 =0;
         i2c_writeData(I2Cnr3, 0x01, (unsigned char*)UI_lcdRow2, 17);
         debug("LCD row2: %s\n", UI_lcdRow2 + 1);         
         goto endi2ctx;
      } 
      endi2ctx: 

      if( USB_BUS_SENSE & !att )
      {
         USBDeviceAttach();
         att = 1;
         //rprintf( "Device attached\r\n" );
      }
      else
      {
         if( (!USB_BUS_SENSE) & att)
         {   
            USBDeviceDetach();
            //rprintf( "Device detached\r\n" );
            att = 0;
         }   
      }

#ifndef BRICKME
      // if HOST is attached serve the USB requests
      if (att)
      {
         ServiceRequests();
      }
#endif
    
	}
	
}






