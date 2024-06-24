
#include "main.h"
#define _USB_SOFT_LAYER_C_
#include "USBSoftLayer.h"
#include "current_adc.h"

#include "HardwareProfile.h"
#include "USB/usb_device.h"
#include "USB/usb.h"
#include "USB/usb_function_generic.h"
#include "usb_config.h"
#include "USBPacket.h"
#include "DAQ.h"
#include "pwm.h"
#include "NVMEM.h"

#include "rprintf1.h"

//#include "debug.h"

#include "UI_comm.h"

#include "OBD_comm.h"

#include "SPI2515.h"
#include "CAN_routines.h"

BYTE counter;

int sentPacketzz = 0;

char temppp = 0;

long lolek;

//
// ??
//
void USBCBSuspend(void)
{
    #if 0
        U1EIR = 0xFFFF;
        U1IR = 0xFFFF;
        U1OTGIR = 0xFFFF;
        IFS5bits.USB1IF = 0;
        IEC5bits.USB1IE = 1;
        U1OTGIEbits.ACTVIE = 1;
        U1OTGIRbits.ACTVIF = 1;
        Sleep();
    #endif
}

//
// ??
//
void USBCBWakeFromSuspend(void)
{
   debug( "Wake from suspend\r\n" );
}

//
// ??
//
void USBCB_SOF_Handler(void)
{
}

//
// USB error handling function
//
void USBCBErrorHandler(void)
{
   debug( "Error handler\r\n" );
}

//
// ??
//
void USBCBCheckOtherReq(void)
{
}

//
// ??
//
void USBCBStdSetDscHandler(void)
{
}

//
// USB EndPoint initialization
//
void USBCBInitEP(void)
{
    USBEnableEndpoint( USBGEN_EP_NUM, USB_IN_ENABLED | USB_OUT_ENABLED | USB_HANDSHAKE_ENABLED | USB_DISALLOW_SETUP );
    //USBEnableEndpoint( USBGEN_EP_TX_NUM, USB_IN_ENABLED | USB_OUT_ENABLED | USB_HANDSHAKE_ENABLED | USB_DISALLOW_SETUP );
    usbInHandle = USBGenRead( USBGEN_EP_NUM, ( BYTE* ) &inPacket, USBGEN_EP_SIZE );
}

//
// ??
//
void USBCBSendResume(void)
{
    static WORD delay_count;
    
    USBResumeControl = 1;                // Start RESUME signaling
    
    delay_count = 1800U;                // Set RESUME line for 1-13 ms
    do
    {
        delay_count--;
    }while(delay_count);
    USBResumeControl = 0;
}

//
// USB received data callback
//
BOOL USER_USB_CALLBACK_EVENT_HANDLER(USB_EVENT event, void *pdata, WORD size)
{
    switch(event)
    {
        case EVENT_CONFIGURED: 
            USBCBInitEP();
            break;
        case EVENT_SET_DESCRIPTOR:
            USBCBStdSetDscHandler();
            break;
        case EVENT_EP0_REQUEST:
            USBCBCheckOtherReq();
            break;
        case EVENT_SOF:
            USBCB_SOF_Handler();
            break;
        case EVENT_SUSPEND:
            USBCBSuspend();
            break;
        case EVENT_RESUME:
            USBCBWakeFromSuspend();
            break;
        case EVENT_BUS_ERROR:
            USBCBErrorHandler();
            break;
        case EVENT_TRANSFER:
            Nop();
            break;
        default:
            break;
    }      
    return TRUE; 
}

void usbWaitForData( void ) {
   while( !USBHandleBusy( usbInHandle ) );
}


//
// USB requests handling function
//
void ServiceRequests(void)
{   
 
   int i;
   //unsigned char num;
   unsigned char *ucharptr;
   unsigned short * sTab;
   unsigned char j;
   int current_cpu_ipl;
  
   
    if( USBGetDeviceState() != CONFIGURED_STATE )
   {   
      // If not connected to USB
      //USBDisconnected();
      // If not configured quit the function  
      return;
   }   
   
    // Check to see if data has arrived
   if( !USBHandleBusy( usbInHandle ) ) 
   {    
     
     //my_putc('k');
     
      counter = 0;
      
    USB_LoopCountsWithoutUsbTraffic = 0;

 ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////      
 //______________________________________________________________DATA INPUT PROCESSING__________________________________________________________________________/
 ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
      switch( inPacket.cmd )
      {
 //____________________________________________________________________________________________ CAN DATA COMMAND__________________________________
         case CMD_CAN_DATA:
         {
           // It is not checked whether there's a place in the CAN tx ring buffer or not. The sending USB node should check it first.
            for (i = 0; i < inPacket.CANDataPacket.msgCount; i++)
            {
              CAN_TXBFR_PutData( inPacket.CANDataPacket.msgs[i] );
            }
            //
            //debug("txb: %d \n", CAN_TXBFR_BuffFill);
            // PC needs feedback about buffers status, if there are no CAN messages to be transmitted via USB to pc, send the info packet.
            // If there are any messages to send, the status feedback will be included with messages
            if ( !CAN_RXBFR_BuffFill)
            {
              counter = sizeof( outPacket.devState );
              outPacket.cmd = CMD_GET_STATE;
              outPacket.devState.state = DAQ_appState;
              outPacket.devState.errorCode = DAQ_errCode;
              outPacket.devState.overCurrentPorts = workData.overCurrentPorts;
              outPacket.devState.DAQ_rxBuffFill = DAQ_RXBFR_BuffFill;
              outPacket.devState.DAQ_txBuffFill = DAQ_TXBFR_BuffFill;
              outPacket.devState.CAN_rxBuffFill = CAN_RXBFR_BuffFill;
              outPacket.devState.CAN_txBuffFill = CAN_TXBFR_BuffFill;
              outPacket.devState.OBD_rxBuffFill = OBD_RXBFR_BuffFill;
              outPacket.devState.OBD_txBuffFill = OBD_TXBFR_BuffFill;   
            }
            break;
         }

 //____________________________________________________________________________________________ PWM DATA COMMAND__________________________________
        case CMD_PWM_DATA:
        {
            // Normally I'd use the DAQ_RXBFR_PutData(TDAQInputData data) function, but for performance gain I'm using it's inside code


          if (DAQ_RXBFR_BuffFill >= DAQ_RXBUFF_SIZE)
          {
              DAQ_appState = APP_STATE_ERROR;
              DAQ_errCode = APP_ERR_BUFFER_OVERFLOW;
              DAQ_Stop();
              break;
          }

         // if packet carries a valuable PWM data
         if ( inPacket.devPWMData.pwmDataCorrect )
         {
            //rprintf("ID: %lu\n",inPacket.devPWMData.packetID);
            //rprintf("%u\n",(int)inPacket.devPWMData.packetID);

            ucharptr = inPacket.devPWMData.pwmData;
            for( i = 0; i < PWM_DRV_COUNT; i++ )
            {
               DAQ_RXBFR_Data[DAQ_RXBFR_WritePos].pwmValue[i] = ucharptr[i];
            }
            DAQ_RXBFR_Data[DAQ_RXBFR_WritePos].AO1value = inPacket.devPWMData.AO1;
            DAQ_RXBFR_Data[DAQ_RXBFR_WritePos].AO2value = inPacket.devPWMData.AO2;
            DAQ_RXBFR_Data[DAQ_RXBFR_WritePos].packetID = inPacket.devPWMData.packetID;
         }

         // if packet carries a valuable manual drive values
         if ( inPacket.devPWMData.pwmManualsCorrect )
         {
           //rprintf("\n\n\nDUPA\n");
           //my_putc('D');
           for( i = 0; i < PWM_DRV_MAX_COUNT; i++ )
           {
             workData.DAQ_PWMManualDutyCycles[ i ] = inPacket.devPWMData.pwmManuals[ i ];
             workData.DAQ_PWMManualDriveEnabled[ i ] = inPacket.devPWMData.pwmManualsEnableFlags[ i ];
             if (workData.DAQ_PWMManualDriveEnabled[ i ])
             {
             //  my_putc('1');
             }
             else
             {
             //  my_putc('0');
             }
             
           }
          // my_putc('\n');
           //my_putc('\r');
         }
          
          // Properly serve the DAQ_RXBFR variables
          DAQ_RXBFR_WritePos++;
          if (DAQ_RXBFR_WritePos >= DAQ_RXBUFF_SIZE)
            DAQ_RXBFR_WritePos = 0;
          DAQ_RXBFR_BuffFill++;
          break;
      }

 //____________________________________________________________________________________________ OBD DATA COMMAND__________________________________
         case CMD_OBD_DATA:
         {
           // It is not checked whether there's a place in the OBD tx ring buffer or not. The sending USB node should check it first.
            for (i = 0; i < inPacket.OBDDataPacket.charCount; i++)
            {
              	//while( !U2STAbits.TRMT );
	              //U2TXREG = inPacket.OBDDataPacket.chars[i];
                OBD_TXBFR_PutData( inPacket.OBDDataPacket.chars[i] );
            }
            // PC needs feedback about buffers status, if there are no OBD messages to be transmitted via USB to pc, send the info packet.
            // If there are any messages to send, the status feedback will be included with data messages
            if ( !OBD_RXBFR_BuffFill)
            {
              counter = sizeof( outPacket.devState );
              outPacket.cmd = CMD_GET_STATE;
              outPacket.devState.state = DAQ_appState;
              outPacket.devState.errorCode = DAQ_errCode;
              outPacket.devState.overCurrentPorts = workData.overCurrentPorts;
              outPacket.devState.DAQ_rxBuffFill = DAQ_RXBFR_BuffFill;
              outPacket.devState.DAQ_txBuffFill = DAQ_TXBFR_BuffFill;
              outPacket.devState.CAN_rxBuffFill = CAN_RXBFR_BuffFill;
              outPacket.devState.CAN_txBuffFill = CAN_TXBFR_BuffFill;
              outPacket.devState.OBD_rxBuffFill = OBD_RXBFR_BuffFill;
              outPacket.devState.OBD_txBuffFill = OBD_TXBFR_BuffFill;
            }
            break;
         }

//____________________________________________________________________________________________ POLL DATA COMMAND_________________________________             
         case CMD_POLL_DATA:
         {         
           break;
         }

 //____________________________________________________________________________________________ ENABLE CAN PULLUP COMMAND_________________________             
         case CMD_CAN_EN_PULLUP:
         {         
           CAN_EnablePullUp();
           outPacket.cmd = CMD_CAN_EN_PULLUP;
           counter = sizeof(outPacket.cmd);  
           break;
         }

 //____________________________________________________________________________________________ DISABLE CAN PULLUP COMMAND________________________             
         case CMD_CAN_DSBL_PULLUP:
         {    
           CAN_DisablePullUp();   
           outPacket.cmd = CMD_CAN_DSBL_PULLUP;
           counter = sizeof(outPacket.cmd);  
           break;
         }

 //____________________________________________________________________________________________ ENABLE OBD COMMUNICATION COMMAND__________________
         case CMD_OBD_ENABLE_COMM:
         {   
           OBD_TXBFR_Clear();
           OBD_RXBFR_Clear();
           OBD_ELM_Prompting = 1; 
           OBD_CommmEnabled = 1;   
           outPacket.cmd = CMD_OBD_ENABLE_COMM;
           counter = sizeof(outPacket.cmd);   
           break;
         }
         
  //____________________________________________________________________________________________ ENABLE CAN COMMUNICATION COMMAND__________________
         case CMD_CAN_DISABLE_COMM:
         {   
           CAN_Disable(); 
           outPacket.cmd = CMD_CAN_DISABLE_COMM;
           counter = sizeof(outPacket.cmd);   
           break;
         }  
         
   //____________________________________________________________________________________________ ENABLE CAN COMMUNICATION COMMAND__________________
         case CMD_CAN_RST_FXD_TRC:
         {   
           CAN_TimestampTimerValue = 0;
           CAN_RXBFR_Clear();
           outPacket.cmd = CMD_CAN_RST_FXD_TRC;
           counter = sizeof(outPacket.cmd);   
           break;
         }  
           
  //____________________________________________________________________________________________ SET CAN CONFIGURATION COMMAND____________________
         case CMD_CAN_SET_CONFIG:
         {   
         
           CAN_Enable(  inPacket.CANConfigPacket.MCP2515_BRPl, 
                        inPacket.CANConfigPacket.MCP2515_PS1,
                        inPacket.CANConfigPacket.MCP2515_PS2,
                        inPacket.CANConfigPacket.MCP2515_PRSEG,
                        inPacket.CANConfigPacket.MCP2515_SJW); 
                                  
//          rprintf(": %d\n%d\n%d\n%d\n%d\n", inPacket.CANConfigPacket.MCP2515_BRPl, 
//                                            inPacket.CANConfigPacket.MCP2515_PS1,
//                                            inPacket.CANConfigPacket.MCP2515_PS2,
//                                            inPacket.CANConfigPacket.MCP2515_PRSEG,
//                                            inPacket.CANConfigPacket.MCP2515_SJW);                                 
           outPacket.cmd = CMD_CAN_SET_CONFIG;
           counter = sizeof(outPacket.cmd);   
           break;
         }                 
       
 //____________________________________________________________________________________________ UPDATE UI COMMAND_________________________________
         case CMD_UI_UPDATE:
         {
            if (inPacket.UIDataPacket.lcdUpdateRow1)
            {
               UI_UpdateLcdRow1( inPacket.UIDataPacket.lcdRow1Str );
            }
            if (inPacket.UIDataPacket.lcdUpdateRow2)
            {
               UI_UpdateLcdRow2( inPacket.UIDataPacket.lcdRow2Str );
            }   
            if (inPacket.UIDataPacket.bgSetDispMode)
            {
               UI_bgDisplayCurrent = inPacket.UIDataPacket.bgDispMode;
               UI_updateBgs = 1;
            }     
            //debug( "Start\n" );
            //appStart();
            break;
         }
 
 //____________________________________________________________________________________________ GET STATE COMMAND_________________________________        
         case CMD_GET_STATE:
         {
            counter = sizeof( outPacket.devState );
            outPacket.cmd = CMD_GET_STATE;
            outPacket.devState.state = DAQ_appState;
            outPacket.devState.errorCode = DAQ_errCode;
            outPacket.devState.overCurrentPorts = workData.overCurrentPorts;
            outPacket.devState.DAQ_rxBuffFill = DAQ_RXBFR_BuffFill;
            outPacket.devState.DAQ_txBuffFill = DAQ_TXBFR_BuffFill;
            outPacket.devState.CAN_rxBuffFill = CAN_RXBFR_BuffFill;
            outPacket.devState.CAN_txBuffFill = CAN_TXBFR_BuffFill;
            outPacket.devState.OBD_rxBuffFill = OBD_RXBFR_BuffFill;
            outPacket.devState.OBD_txBuffFill = OBD_TXBFR_BuffFill;
            break;
         } 

 //____________________________________________________________________________________________ GET VERSION COMMAND_______________________________
         case CMD_GET_VERSION:
         {   
           counter = sizeof( outPacket.fwVersion );
           outPacket.fwVersion.cmd = CMD_GET_VERSION;
           outPacket.fwVersion.major = MAJOR_VERSION;
           outPacket.fwVersion.minor = MINOR_VERSION;
           outPacket.fwVersion.USBPacket = USBPACKET_VERSION;
           DAQ_Stop();
           DAQ_appState = APP_STATE_STOPPED;
           DAQ_errCode = APP_ERR_NONE;
           break;
         }

 //____________________________________________________________________________________________ GET DEVICE DAQ CONFIG COMMAND_____________________
         case CMD_GET_CONFIG:
            //rprintf( "Config\n" );
            counter = sizeof( outPacket.devConfig );
            outPacket.cmd = CMD_GET_CONFIG;
            outPacket.devConfig.pressureSensCount = PRESS_SENS_COUNT;
            outPacket.devConfig.pwmDrvCount = PWM_DRV_COUNT;
            outPacket.devConfig.currentSensCount = CURR_SENS_COUNT;

            outPacket.devConfig.DAQ_rxBuffSize = DAQ_RXBUFF_SIZE;
            outPacket.devConfig.DAQ_txBuffSize = DAQ_TXBUFF_SIZE;
            outPacket.devConfig.CAN_rxBuffSize = CAN_RXBUFF_SIZE;
            outPacket.devConfig.CAN_txBuffSize = CAN_TXBUFF_SIZE;
            outPacket.devConfig.OBD_rxBuffSize = OBD_RXBUFF_SIZE;
            outPacket.devConfig.OBD_txBuffSize = OBD_TXBUFF_SIZE;

            outPacket.devConfig.appState = DAQ_appState;
            outPacket.devConfig.frequency = workData.DAQ_PWMFreq;
            USBConnected();
            break;
         
 //____________________________________________________________________________________________ SET DEVICE DAQ CONFIG COMMAND______________________
         case CMD_SET_CONFIG:
            //rprintf( "Set config\n" );
            if( DAQ_appState == APP_STATE_RUNNING || DAQ_appState == APP_STATE_ERROR )
            {
               DAQ_Stop();
            }
            DAQ_appState = APP_STATE_STOPPED;
            DAQ_errCode = APP_ERR_NONE;
            
            for( i = 0; i < NUM_OUTPUT_DATA_BUFF; i++ ) 
            {
               DAQ_outputDataBuff[ i ].used = 0;
            }
            
            //num = 0;

            workData.currReadChannsCount = inPacket.devConfig.currReadChannsCount;
            for ( i = 0; i < inPacket.devConfig.currReadChannsCount; i++)
            {
               workData.currReadChannsIndices[i] = inPacket.devConfig.currReadChannsIndices[i];
               //debug(" indx %d \n", inPacket.devConfig.currReadChannsIndices[i]); 
            }   
            
            if( inPacket.devConfig.frequency >= 100 && inPacket.devConfig.frequency <= 2000 ) 
            {
               workData.DAQ_PWMFreq = inPacket.devConfig.frequency;
               ApplyPWMFreq();
               
            }
            outputDataBuffReadPos = 0;
            outputDataBuffWritePos = 0;
            pktID = 1;

            break;

  //____________________________________________________________________________________________ START DAQ COMMAND_________________________________
         case CMD_START:
            //rprintf( "Start\n" );
            DAQ_Start();
            break;
 
  //____________________________________________________________________________________________ STOP DAQ COMMAND__________________________________           
         case CMD_STOP:
            //rprintf( "Stop\n" );
            DAQ_Stop();
            pktID = 1;
            break;
 
   //____________________________________________________________________________________________ SET SERIAL NUMBER COMMAND________________________           
         case CMD_SET_SERIAL:
            //rprintf( "Set serial\n" );
            //rprintf( "serial: %s\n", outPacket.serialInfo.serialString );
            //rprintf( "GUID: %s\n", outPacket.serialInfo.GUIDString );
            counter = sizeof( outPacket.serialInfo);
            outPacket.serialInfo.cmd = CMD_GET_SERIAL;
            for (i = 0; i < NVMEM_SERIAL_LEN; i++)
            {            
              my_eedata_bfr[ NVMEM_SERIAL_POS + i ] = inPacket.serialInfo.serialString[i];
              //my_putc( my_eedata_bfr[ NVMEM_SERIAL_POS + i ] );
              //rprintf("%u ", my_eedata_bfr[ NVMEM_SERIAL_POS + i ] );
            }      
            //rprintf("\n");       
            for (i = 0; i < NVMEM_GUID_LEN; i++)
            {
              my_eedata_bfr[ NVMEM_GUID_POS + i ] = inPacket.serialInfo.GUIDString[i];
              //my_putc( my_eedata_bfr[ NVMEM_GUID_POS + i ] );
              //rprintf("%u ", my_eedata_bfr[ NVMEM_GUID_POS + i ] );
            }        
            PIC_Flash_Write_NVMEM(); // Write the whole array back to flash to save it permanently      
            break;           
 
   //____________________________________________________________________________________________ GET SERIAL NUMBER COMMAND________________________           
         case CMD_GET_SERIAL:
            //rprintf( "Get serial\n" );
            counter = sizeof( outPacket.serialInfo);
            outPacket.serialInfo.cmd = CMD_GET_SERIAL;
            for (i = 0; i < NVMEM_SERIAL_LEN; i++)
            {            
              outPacket.serialInfo.serialString[i] = my_eedata_bfr[ NVMEM_SERIAL_POS + i ];
            }             
            for (i = 0; i < NVMEM_GUID_LEN; i++)
            {
              outPacket.serialInfo.GUIDString[i] = my_eedata_bfr[ NVMEM_GUID_POS + i ];
            }              
            break; 
           
  //____________________________________________________________________________________________ ENTER BOOTLOADER COMMAND__________________________           
         case  CMD_BLD_ENTER:
            //rprintf( "BLD!!!\n" );
            // Immediately send the acknowledge
            outPacket.cmd = CMD_BLD_ENTER;
            counter = sizeof(outPacket.cmd);               
            usbOutHandle = USBGenWrite( USBGEN_EP_TX_NUM, ( BYTE* ) &outPacket, counter );
            // Jump to BOOTLOADER. This application will be terminated now
           
           // Wait a bit for the USB response to be sent to the PC before disabling the USB module
           for (current_cpu_ipl = 0; current_cpu_ipl < 1000; current_cpu_ipl++);
           
            // When performing write operation the whole page (512 instructions) must be erased first.
           PIC_Flash_Read_NVMEM();//my_eedata_bfr, 512); // Read the whole array from flash to prevent data loss when writing anything back
           // Set the flag that the correct program was uploaded via bootloader
           my_eedata_bfr[1] = 0x4D;
           PIC_Flash_Write_NVMEM();//my_eedata_bfr, 512); // Write the whole array back to flash to prevent data loss 
           
           // Permanently disable all interrupts, otherwise bootloader may be exited when any serviced interrupt comes
           SET_AND_SAVE_CPU_IPL(current_cpu_ipl, 7);
           // Disable USB module
           U1CON = 0x0000;
           
           // Wait a bit so that windows can detect that this device was removed
           for (current_cpu_ipl = 0; current_cpu_ipl < 2000; current_cpu_ipl++);
           
           
              
           // jump to bootloader
           //__asm__("goto 0x0400"); // bootloader resides at that address
           __asm__("goto 0x0000");
            break;          

  //____________________________________________________________________________________________ DEFAULT COMMAND___________________________________
         default:
            debug( "Unknown command: %d\r\n", inPacket.cmd );
            Nop();
           break;
      }
      //read next data packet
      usbInHandle = USBGenRead( USBGEN_EP_NUM, ( BYTE* ) &inPacket, USBGEN_EP_SIZE );
 
 ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////      
 //______________________________________________________________DATA OUTPUT PROCESSING_________________________________________________________________________/
 ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////     
      //num = USBHandleBusy(  usbOutHandle );
      //if(!USBHandleBusy(USBGenericInHandle));
      //rprintf("busy: %d \n", num);
      if( !USBHandleBusy(  usbOutHandle ) ) 
      {
      // If there is some data to be written from the command decode, do it
         if(counter != 0) 
         {
            usbOutHandle = USBGenWrite( USBGEN_EP_TX_NUM, ( BYTE* ) &outPacket, counter );
            counter = 0;
            //rprintf("wrtn\n");
         } 
         else 
         { // else empty the output data buffer
            // If there is something to be sent, send it
            
            if ( CAN_RXBFR_BuffFill && CAN_CommmEnabled)
            {
              outPacket.CANDataPacket.cmd = CMD_CAN_DATA;
              if ( CAN_RXBFR_BuffFill > 3 )
                outPacket.CANDataPacket.msgCount = 3;
              else
                outPacket.CANDataPacket.msgCount = CAN_RXBFR_BuffFill;
//                
//                if ( outPacket.CANDataPacket.msgCount > 1)
                  //outPacket.CANDataPacket.msgCount = 1;
                  //CAN_RXBFR_GetData(outPacket.CANDataPacket.msgs) ;                  
                
              outPacket.CANDataPacket.rxBuffFill = CAN_RXBFR_BuffFill;
              //outPacket.CANDataPacket.rxBuffSize = CAN_RXBUFF_SIZE;
              outPacket.CANDataPacket.txBuffFill = CAN_TXBFR_BuffFill;
              //outPacket.CANDataPacket.txBuffSize = CAN_TXBUFF_SIZE;
              //sentPacketzz++;
              //debug("Sent packets:%d \n", sentPacketzz);
              for ( i = 0; i < outPacket.CANDataPacket.msgCount; i++)
              {
                CAN_RXBFR_GetData(outPacket.CANDataPacket.msgs + i) ;           
              }
            //rprintf("obdp\r\n");
            usbOutHandle = USBGenWrite( USBGEN_EP_TX_NUM, ( BYTE* ) &outPacket, sizeof( outPacket.CANDataPacket ) );    
            
            goto END;                 
            }

            
            if ( OBD_RXBFR_BuffFill)//  &&  OBD_CommmEnabled)
            {
              ///for (lolek = 0; lolek < 1000; lolek++);
              outPacket.OBDDataPacket.cmd = CMD_OBD_DATA;
              if ( OBD_RXBFR_BuffFill > 50 )
                outPacket.OBDDataPacket.charCount = 50;
              else
                outPacket.OBDDataPacket.charCount = OBD_RXBFR_BuffFill; 
                  //if ( outPacket.CANDataPacket.msgCount > 1)
                  //outPacket.CANDataPacket.msgCount = 1;
                  //CAN_RXBFR_GetData(outPacket.CANDataPacket.msgs) ;                  
                
              outPacket.OBDDataPacket.rxBuffFill = OBD_RXBFR_BuffFill;
              //outPacket.OBDDataPacket.rxBuffSize = OBD_RXBUFF_SIZE;
              outPacket.OBDDataPacket.txBuffFill = OBD_TXBFR_BuffFill;
              //outPacket.OBDDataPacket.txBuffSize = OBD_TXBUFF_SIZE;
              //sentPacketzz++;
              //debug("Sent packets:%d \n", sentPacketzz);
              //printf("-)\r\n");
              for ( i = 0; i < outPacket.OBDDataPacket.charCount; i++)
              {
                OBD_RXBFR_GetData(outPacket.OBDDataPacket.chars + i) ; 
                //my_putc1(outPacket.OBDDataPacket.chars[i]);
                 
                //u1putc(outPacket.OBDDataPacket.chars[i]);                 
                  //printf("%d ", outPacket.OBDDataPacket.chars[i]);
                //my_putc( *(outPacket.OBDDataPacket.chars + i) );          
              }
              //printf("(-\r\n");
              //printf("\r\ncount: %d\r\n", outPacket.OBDDataPacket.charCount );
            //rprintf("obdp\r\n");
            usbOutHandle = USBGenWrite( USBGEN_EP_TX_NUM, ( BYTE* ) &outPacket, sizeof( outPacket.OBDDataPacket ) );    
            //rprintf1("%d\r\n", usbOutHandle);
            
            goto END;                 
            }            
            
            if( outputDataBuffReadPos != outputDataBuffWritePos && DAQ_appState == APP_STATE_RUNNING )
            {
               //num = outputDataBuffReadPos;
               // Prepare a data packet with measured data
               outPacket.devData.responseToID = DAQ_outputDataBuff[ outputDataBuffReadPos ].packetRespToID;
               //rprintf("%u\n",(int)outPacket.devData.responseToID);
               outPacket.devData.packetID = pktID++;
               outPacket.devData.cmd = CMD_DEV_DATA;
               outPacket.devData.state = DAQ_appState;
               outPacket.devData.errorCode = DAQ_errCode;
               outPacket.devData.overCurrentPorts = workData.overCurrentPorts;

              outPacket.devData.DAQ_rxBuffFill = DAQ_RXBFR_BuffFill;
              outPacket.devData.DAQ_txBuffFill = DAQ_TXBFR_BuffFill;

               sTab = DAQ_outputDataBuff[ outputDataBuffReadPos ].pressure;
               for( i = 0; i < PRESS_SENS_COUNT; i++ ) 
               {
                  outPacket.devData.pressureSense[ i ] = sTab[ i ];
               }
               sTab = DAQ_outputDataBuff[ outputDataBuffReadPos ].current;
               j = 0;
               
               for( i = 0; i < workData.currReadChannsCount ; i++ ) 
               {
                  outPacket.devData.currentSense[ i ] = sTab[ workData.currReadChannsIndices[i] ];
               }
               DAQ_outputDataBuff[ outputDataBuffReadPos ].used = 0;
               // Send it
               //rprintf("daqp\r\n");
               usbOutHandle = USBGenWrite( USBGEN_EP_TX_NUM, ( BYTE* ) &outPacket, sizeof( outPacket.devData ) );
               
               // Do some postprocessing stuff with indexes
               outputDataBuffReadPos++;
               if( outputDataBuffReadPos >= NUM_OUTPUT_DATA_BUFF ) 
               {
                  outputDataBuffReadPos = 0;
               }
               goto END;
            } ;
            
            // if none of messages was to be sent send device state
            counter = sizeof( outPacket.devState );
            outPacket.cmd = CMD_GET_STATE;
            outPacket.devState.state = DAQ_appState;
            outPacket.devState.errorCode = DAQ_errCode;
            outPacket.devState.overCurrentPorts = workData.overCurrentPorts;
            outPacket.devState.DAQ_rxBuffFill = DAQ_RXBFR_BuffFill;
            outPacket.devState.DAQ_txBuffFill = DAQ_TXBFR_BuffFill;
            outPacket.devState.CAN_rxBuffFill = CAN_RXBFR_BuffFill;
            outPacket.devState.CAN_txBuffFill = CAN_TXBFR_BuffFill;  
            outPacket.devState.OBD_rxBuffFill = OBD_RXBFR_BuffFill;
            outPacket.devState.OBD_txBuffFill = OBD_TXBFR_BuffFill;
            usbOutHandle = USBGenWrite( USBGEN_EP_NUM, ( BYTE* ) &outPacket, counter );
            counter = 0;
         }
      }      
   }
   
   END:
   
   return;
   

   

}

void USBDisconnected( void )
{
  //rprintf("O FUCK!");
     char strTmp[32];  
     int i = 0;
     for (i = 0; i < 9; i++)
     {
       #ifdef PRODUCTION
         workData.DAQ_PWMDutyCycles[ i ] = 0;
         ApplyPWMFreq(); 
       #endif
     }  
  
     
     #ifdef PRODUCTION
        workData.DAQ_AO1value = 0;
        workData.DAQ_AO2value = 0;
        BeginCurrentReadout();
     #endif
         
     if (OBD_CommmEnabled)
     {
       OBD_Disable();
     }  
     if (CAN_CommmEnabled)
     {
       CAN_Disable();
     }
        strcpy ( strTmp, "                "); 
   UI_UpdateLcdRow1(strTmp);
      strcpy ( strTmp, "PC DISCONNECTED ");
   UI_UpdateLcdRow2(strTmp);
}  

void USBConnected( void )
{
  char strTmp[32]; 
  strcpy ( (strTmp), "                "); 
  UI_UpdateLcdRow1(strTmp);
  strcpy ( (strTmp), "  PC CONNECTED  ");
  UI_UpdateLcdRow2(strTmp);
} 

