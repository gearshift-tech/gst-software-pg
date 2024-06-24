#ifndef __CAN2515_SPI_C
#define __CAN2515_SPI_C


////#include <p18cxxx.h>
#include "spi.h"
//#include "io_cfg.h"             // I/O pin mapping
#include "main.h"
//#include "REGS2515.h"
#include "SPI2515.h"
#include "CAN_routines.h"

/** V A R I A B L E S ********************************************************/
#pragma udata
unsigned char dummy;

/** P R I V A T E  P R O T O T Y P E S ***************************************/
// See BusMon.h for Public Prototypes
void Delay_ms(unsigned char num_ms);
char SPIReadStatus(void);

/** D E C L A R A T I O N S **************************************************/

#define Mode00  0
#define Mode11  1

#define	ON	1
#define	OFF	0

#define HIGH  1
#define LOW   0


//
// MCP2515 RX0BF hardware interrupt
//
void __attribute__ ((interrupt, no_auto_psv)) _INT1Interrupt( void ) 
{
  //my_putc1('a');
   CAN2515_GetRXB0Messages();
   IFS1bits.INT1IF = 0;  
}

//
// MCP2515 RX1BF hardware interrupt
//
void __attribute__ ((interrupt, no_auto_psv)) _INT4Interrupt( void ) 
{
   //my_putc1('b');
   CAN2515_GetRXB1Messages();
   IFS3bits.INT4IF = 0;     
}



//
// MCP2515 RX1BF hardware interrupt
//
//void __attribute__ ((interrupt, no_auto_psv)) _INT4Interrupt( void ) 
//{
   //IFS3bits.INT4IF = 0;  
   //rprintf(" b");
   //CAN2515_GetRXB1Messages();

//  CS_2515_LOW();
//  CAN2515_WriteSPI(CAN_BIT_MODIFY); // CAN bit modify command
//  CAN2515_ReadSPI();
//  CAN2515_WriteSPI(CANCTRL); // address to write
//  CAN2515_ReadSPI();  
//  CAN2515_WriteSPI(0b11100000); // mask 
//  CAN2515_ReadSPI(); 
//  CAN2515_WriteSPI(0x0); //data to write
//  CAN2515_ReadSPI(); 
//  CS_2515_HIGH();
//}  

//-----------------------------------------------------------------------------
// Writes to the SPI1 buffer
//-----------------------------------------------------------------------------
void CAN2515_WriteSPI1( char data ) 
{  
   SPI1BUF = data;
}

//-----------------------------------------------------------------------------
// Reads from the SPI2 buffer
//-----------------------------------------------------------------------------
char CAN2515_ReadSPI1( void ) 
{
   return SPI1BUF;
}   

//-----------------------------------------------------------------------------
// Writes to the SPI2 buffer
//----------------------------------------------------------------------------- 
void CAN2515_WriteSPI2( char data ) 
{  
   while( SPI2STATbits.SPITBF );
   SPI2BUF = data;
}

//-----------------------------------------------------------------------------
// Reads from the SPI2 buffer
//-----------------------------------------------------------------------------
char CAN2515_ReadSPI2( void ) 
{
   while( !SPI2STATbits.SPIRBF );
   return SPI2BUF;
} 

//-----------------------------------------------------------------------------
// Initializes the SPI2
//-----------------------------------------------------------------------------
void CAN2515_InitSPI2( void ) 
   {

   SPI2STATbits.SPIEN 		= 0;	// disable SPI port
   SPI2STATbits.SPISIDL 	= 0; 	// Continue module operation in Idle mode
    
   SPI2BUF 				      = 0;   	// clear SPI buffer
    
   IFS2bits.SPI2IF         = 0;	// clear interrupt flag
   IEC2bits.SPI2IE         = 0;	// disable interrupt
   IPC8bits.SPI2IP         = 1;
    
   SPI2CON1bits.SSEN       = 0;
   SPI2CON1bits.DISSCK     = 0;	// Internal SPIx clock is enabled
   SPI2CON1bits.DISSDO     = 0;	// SDOx pin is controlled by the module
   SPI2CON1bits.MODE16     = 0;	// set in 16-bit mode, clear in 8-bit mode
   
   SPI2CON1bits.SMP        = 1;	// Input data sampled at middle of data output time
   SPI2CON1bits.CKP        = 0;	// CKP and CKE is subject to change ...
   SPI2CON1bits.CKE        = 1;	// ... based on your communication mode.
   
	SPI2CON1bits.MSTEN      = 1; 	// 1 =  Master mode; 0 =  Slave mode
	SPI2CON1bits.SPRE       = 6; 	// Secondary Prescaler
	SPI2CON1bits.PPRE       = 2; 	// Primary Prescaler

   SPI2CON2                = 0;	// non-framed mode
	SPI2CON2bits.SPIFE      = 1;
	
	SPI2CON2bits.SPIBEN     = 0;  // enhanced buffer mode (1 enable)
	
	SPI2STATbits.SISEL      = 5;  // interrupt after last bit is shifted out (complete transfer 

   IFS2bits.SPI2IF         = 0;  // reset the interrupt flag
   IEC2bits.SPI2IE         = 0;	// enable interrupt   
   SPI2STATbits.SPIEN      = 1; 	// enable SPI port, clear status
}

//-----------------------------------------------------------------------------
// Initializes the SPI1
//-----------------------------------------------------------------------------
void CAN2515_InitSPI1( void ) 
   {

   SPI1STATbits.SPIEN 		= 0;	// disable SPI port
   SPI1STATbits.SPISIDL 	= 0; 	// Continue module operation in Idle mode
    
   SPI1BUF 				      = 0;   	// clear SPI buffer
    
   IFS0bits.SPI1IF         = 0;	// clear interrupt flag
   IEC0bits.SPI1IE         = 0;	// disable interrupt
   IPC2bits.SPI1IP         = 1;
    
   SPI1CON1bits.SSEN       = 0;
   SPI1CON1bits.DISSCK     = 0;	// Internal SPIx clock is enabled
   SPI1CON1bits.DISSDO     = 0;	// SDOx pin is controlled by the module
   SPI1CON1bits.MODE16     = 0;	// set in 16-bit mode, clear in 8-bit mode
   
   SPI1CON1bits.SMP        = 1;	// Input data sampled at middle of data output time
   SPI1CON1bits.CKP        = 0;	// CKP and CKE is subject to change ...
   SPI1CON1bits.CKE        = 1;	// ... based on your communication mode.
   
	SPI1CON1bits.MSTEN      = 1; 	// 1 =  Master mode; 0 =  Slave mode
	SPI1CON1bits.SPRE       = 1; 	// Secondary Prescaler
	SPI1CON1bits.PPRE       = 3; 	// Primary Prescaler

   SPI1CON2                = 0;	// non-framed mode
	SPI1CON2bits.SPIFE      = 1;
	
	SPI1CON2bits.SPIBEN     = 1;  // enhanced buffer mode (1 enable)
	
	SPI1STATbits.SISEL      = 5;  // interrupt after last bit is shifted out (complete transfer 

   IFS0bits.SPI1IF         = 0;  // reset the interrupt flag
   IEC0bits.SPI1IE         = 0;	// enable interrupt   
   SPI1STATbits.SPIEN      = 1; 	// enable SPI port, clear status
}

//-----------------------------------------------------------------------------
//  CAN2515_Reset(void)
//-----------------------------------------------------------------------------
void CAN2515_Reset(void)
{
  CS_2515_LOW();
  CAN2515_WriteSPI(CAN_RESET);
  CAN2515_ReadSPI();
  CS_2515_HIGH();
  for(dummy=0; dummy<255; dummy++);
  CS_2515_LOW();
  CAN2515_WriteSPI(CAN_RESET);
  CAN2515_ReadSPI();
  CS_2515_HIGH();
  for(dummy=0; dummy<255; dummy++);
}

//-------------------------------------------------------------------------
//  CAN2515_ByteWrite(unsigned char addr, unsigned char value )
//-------------------------------------------------------------------------
void CAN2515_ByteWrite(unsigned char addr, unsigned char value )
{
  CS_2515_LOW();
  CAN2515_WriteSPI(CAN_WRITE);
  CAN2515_ReadSPI();
  CAN2515_WriteSPI(addr);
  CAN2515_ReadSPI();
  CAN2515_WriteSPI(value);
  CAN2515_ReadSPI();
  CS_2515_HIGH();
}

//-----------------------------------------------------------------------------
//  CAN2515_ByteRead(unsigned char addr)
//-----------------------------------------------------------------------------
unsigned char CAN2515_ByteRead(unsigned char addr)
{
  unsigned char tempdata;
  CS_2515_LOW();
  CAN2515_WriteSPI(CAN_READ);
  CAN2515_ReadSPI();
  CAN2515_WriteSPI(addr);
  CAN2515_ReadSPI();
  CAN2515_WriteSPI(0x0); // write sth to force SPI clock  
  tempdata = CAN2515_ReadSPI();
  CS_2515_HIGH();
  return tempdata;
}

//-----------------------------------------------------------------------------
//  CAN52515_ReadStatus(void)
//  Performs Read Status command and returns result
//-----------------------------------------------------------------------------
char CAN2515_ReadStatus(void)
{
  unsigned char result; 
  CS_2515_LOW();
  CAN2515_WriteSPI(CAN_RD_STATUS); // send read status command
  CAN2515_ReadSPI(); 
  CAN2515_WriteSPI(0x00); //dummy byte fo force SPI clock
  result = CAN2515_ReadSPI(); 
  CS_2515_HIGH();   
  return result;
}

//-----------------------------------------------------------------------------
//  CAN2515_SetNormalOperationMode(void)
//  Sets the MCP2515 operation mode to normal operation mode
//-----------------------------------------------------------------------------
void CAN2515_SetNormalOperationMode()
{
  CS_2515_LOW();
  CAN2515_WriteSPI(CAN_BIT_MODIFY); // CAN bit modify command
  CAN2515_ReadSPI();
  CAN2515_WriteSPI(CANCTRL); // address to write
  CAN2515_ReadSPI();  
  CAN2515_WriteSPI(0b11100000); // mask 
  CAN2515_ReadSPI(); 
  CAN2515_WriteSPI(0x0); //data to write
  CAN2515_ReadSPI(); 
  CS_2515_HIGH();
  CAN2515_ByteWrite(0x0C, 0xFF );
}   

//-----------------------------------------------------------------------------
//  CAN2515_SetConfigurationOperationMode(void)
//  Sets the MCP2515 operation mode to Configuration operation mode
//-----------------------------------------------------------------------------
void CAN2515_SetConfigurationOperationMode()
{
  CS_2515_LOW();
  CAN2515_WriteSPI(CAN_BIT_MODIFY); // CAN bit modify command
  CAN2515_ReadSPI();
  CAN2515_WriteSPI(CANCTRL); // address to write
  CAN2515_ReadSPI();  
  CAN2515_WriteSPI(0b11100000); // mask 
  CAN2515_ReadSPI(); 
  CAN2515_WriteSPI(0b10000000); //data to write
  CAN2515_ReadSPI(); 
  CS_2515_HIGH();
} 

//-----------------------------------------------------------------------------
//  CAN2515_GetOperationMode(void)
//  Gets the MCP2515 operation mode 
//-----------------------------------------------------------------------------
unsigned char CAN2515_GetOperationMode()
{
  unsigned char mode;  
  CS_2515_LOW();
  CAN2515_WriteSPI(CAN_READ); // CAN read byte command
  CAN2515_ReadSPI();
  CAN2515_WriteSPI(CANCTRL); // address to read
  CAN2515_ReadSPI();  
  CAN2515_WriteSPI(0x0); // dummy 
  mode = CAN2515_ReadSPI(); 
  CS_2515_HIGH(); 
  debug("mode: %d\n", mode);
  return mode;
}   

//-----------------------------------------------------------------------------
//  CAN2515_SetBitTiming(
//    unsigned char _BRP,   // BRP register value
//    unsigned char _PS1,   // PS1 register value
//    unsigned char _PS2,   // PS2 register value
//    unsigned char _PRSEG, // PRSEG register value
//     unsigned char _SJW)  // SJW register value
//  Sets the MCP2515 bit timing
//-----------------------------------------------------------------------------
void CAN2515_SetBitTiming(unsigned char _BRP, unsigned char _PS1, unsigned char _PS2, unsigned char _PRSEG, unsigned char _SJW)
{
  unsigned char n;
  // just to be sure, apply AND masks to passed parameters
  _BRP &= 0b00111111; // this is only 6 bit variable
  _PS1 &= 0b00000111; // this is only 3 bit variable
  _PS2 &= 0b00000111; // this is only 3 bit variable 
  _PRSEG  &= 0b00000111; // this is only 3 bit variable
  _SJW &= 0b00000011; // this is only 2 bit variable
  
  //prepare CNF registers values
  unsigned char _CNF1 = (_SJW-1) << 6 | _BRP;
  unsigned char _CNF2 = 1 << 7 | 0 << 6 | (_PS1-1) << 3 | (_PRSEG-1);
  unsigned char _CNF3 = (_PS2-1);
  
  //put the device to configuration mode
  //only then the CNFx registers are writable
  CAN2515_SetConfigurationOperationMode();
  //CAN2515_Reset();
  
  n = 5; // little delay before pulling CS low again 
        
  CS_2515_LOW();
  CAN2515_WriteSPI(CAN_WRITE); // CAN write byte command
  CAN2515_ReadSPI();
  CAN2515_WriteSPI(CNF3); // address to write
  CAN2515_ReadSPI();  
  CAN2515_WriteSPI(_CNF3); // CNF3 data 
  CAN2515_ReadSPI(); 
  CAN2515_WriteSPI(_CNF2); // CNF2 data ( write address is automatically incremented ) 
  CAN2515_ReadSPI(); 
  CAN2515_WriteSPI(_CNF1); // CNF1 data ( write address is automatically incremented ) 
  CAN2515_ReadSPI(); 
  CS_2515_HIGH();
  
  n = 5; // little delay before pulling CS low again 
  
  //put the device back to normal operation mode
  CAN2515_SetNormalOperationMode();
  
  //debug( "%d %d %d\n", _CNF1, _CNF2, _CNF3);
}

//-----------------------------------------------------------------------------
//  CAN2515_GetBitTiming(
//    unsigned char *_CNF1,   // pointer to write to the CNF1 register value
//    unsigned char *_CNF2,   // pointer to write to the CNF2 register value
//    unsigned char *_CNF3)   // pointer to write to the CNF3 register value
//  Sets the MCP2515 bit timing
//-----------------------------------------------------------------------------
void CAN2515_GetBitTiming(unsigned char *_CNF1, unsigned char *_CNF2, unsigned char *_CNF3)
{  
  CS_2515_LOW();
  CAN2515_WriteSPI(CAN_READ); // CAN write byte command
  CAN2515_ReadSPI();
  CAN2515_WriteSPI(CNF3); // address to read
  CAN2515_ReadSPI();  
  CAN2515_WriteSPI(0x0); // dummy data to force SPI clock 
  *_CNF3 = CAN2515_ReadSPI(); 
  CAN2515_WriteSPI(0x0); // dummy data to force SPI clock  
  *_CNF2 = CAN2515_ReadSPI(); 
  CAN2515_WriteSPI(0x0); // dummy data to force SPI clock  
  *_CNF1 = CAN2515_ReadSPI(); 
  CS_2515_HIGH();
  
  //debug( "%d %d %d\n", *_CNF1, *_CNF2, *_CNF3);
}

// This function always sends the message through buffer 0 and does NOT check if the buffer is empty. This can cause some errors. use with caution
void CAN2515_SendMessage( CAN_PACKET pkt )
{
  // If MCP2515 is used by different function, current function MUST be aborted
  // Such a situation may occur when interrupts are enabled
  if (CAN_MCP2515_Locked)
  {
    return;
  }  
  CAN_MCP2515_Locked = 1;
  
   unsigned char n;
   unsigned char bytesToWrite = 5 + pkt.data.DLC; // 5 bytes + data bytes have to be written via spi
 
   CS_2515_LOW();
   CAN2515_WriteSPI( CAN_LD_TXB0_ID);
   CAN2515_ReadSPI();
   for(n = 0; n < bytesToWrite; n++)
   {
      CAN2515_WriteSPI(pkt.bytes[n]);//CAN2515_WriteSPI(pkt.bytes.data[n]);
      CAN2515_ReadSPI();
   }   
   CS_2515_HIGH();
   
   n = 5; // little delay before pulling CS low 
   
   CS_2515_LOW();
   CAN2515_WriteSPI(CAN_RTS_TXB0);
   CAN2515_ReadSPI();
   CS_2515_HIGH();    
   
   // Free the chip
   CAN_MCP2515_Locked = 0;
}   

void CAN2515_SendBufferedMessage()
{
  // If MCP2515 is used by different function, current function MUST be aborted
  // Such a situation may occur when interrupts are enabled
  if (CAN_MCP2515_Locked)
  {
    return;
  }  
  CAN_MCP2515_Locked = 1;
  
  unsigned char n;
  unsigned char bytesToWrite;
  unsigned char RTS_CMD = 0x0;
  unsigned char devstat = CAN2515_ReadStatus();
  //debug("devstat %d \n", devstat);
  CAN_PACKET pkt;
  //---------------------------------------------------------
  // Fill TXB0
  if ( CAN_TXBFR_BuffFill > 0 && !(devstat & 0b00000100) ) //if ring buffer is not empty and TXB0 is ready
  {
    CAN_TXBFR_GetData( &pkt ); //  
    
    // Check if the timestamp of the first CAN message in the Tx buffer is greater if the current timestamp
    // If so, it means it should be sent immediately
    // The value of this variable is assumed to be properly handled and to always contain the timestamp of the first message in the non-empty CAN Tx buffer
    
    
    bytesToWrite = 5 + pkt.data.DLC; // 5 bytes + data bytes have to be written via spi
     
    CS_2515_LOW();
    CAN2515_WriteSPI( CAN_LD_TXB0_ID);
    CAN2515_ReadSPI();
    for(n = 0; n < bytesToWrite; n++)
    {
      CAN2515_WriteSPI(pkt.bytes[n]);//CAN2515_WriteSPI(pkt.bytes.data[n]);
      CAN2515_ReadSPI();
    }   
    CS_2515_HIGH();
    RTS_CMD |= CAN_RTS_TXB0;
    //rprintf( "T0\n");
    
      //rprintf("\nSIDH: %d || SIDL: %d || EIDH %d || EIDL: %d ||||| DLC: %d \n DATA:\n", pkt.data.SIDH, pkt.data.SIDL, pkt.data.EID8, pkt.data.EID0, pkt.data.DLC);
      //pkt.data.data[n];
  }
  
  
  //---------------------------------------------------------
  // Fill TXB1
  if ( CAN_TXBFR_BuffFill > 0 && !(devstat & 0b00010000) ) //if ring buffer is not empty and TXB1 is ready
  {
    CAN_TXBFR_GetData( &pkt ); //  
    bytesToWrite = 5 + pkt.data.DLC; // 5 bytes + data bytes have to be written via spi
     
    CS_2515_LOW();
    CAN2515_WriteSPI( CAN_LD_TXB1_ID);
    CAN2515_ReadSPI();
    for(n = 0; n < bytesToWrite; n++)
    {
      CAN2515_WriteSPI(pkt.bytes[n]);//CAN2515_WriteSPI(pkt.bytes.data[n]);
      CAN2515_ReadSPI();
    }   
    CS_2515_HIGH(); 
    RTS_CMD |= CAN_RTS_TXB1; 
    //rprintf("\nSIDH: %d || SIDL: %d || EIDH %d || EIDL: %d ||||| DLC: %d \n DATA:\n", pkt.data.SIDH, pkt.data.SIDL, pkt.data.EID8, pkt.data.EID0, pkt.data.DLC);
    //rprintf( "T1\n");
    //for (n = 0; n < 255; n++);
    //for (n = 0; n < 255; n++);
  }
  
  //---------------------------------------------------------
  // Fill TXB2
  if ( CAN_TXBFR_BuffFill > 0 && !(devstat & 0b01000000) ) //if ring buffer is not empty and TXB2 is ready
  {
    CAN_TXBFR_GetData( &pkt ); //  
    bytesToWrite = 5 + pkt.data.DLC; // 5 bytes + data bytes have to be written via spi
     
    CS_2515_LOW();
    CAN2515_WriteSPI( CAN_LD_TXB2_ID);
    CAN2515_ReadSPI();
    for(n = 0; n < bytesToWrite; n++)
    {
      CAN2515_WriteSPI(pkt.bytes[n]);//CAN2515_WriteSPI(pkt.bytes.data[n]);
      CAN2515_ReadSPI();
    }   
    CS_2515_HIGH();
    RTS_CMD |= CAN_RTS_TXB2;  
    //rprintf("\nSIDH: %d || SIDL: %d || EIDH %d || EIDL: %d ||||| DLC: %d \n DATA:\n", pkt.data.SIDH, pkt.data.SIDL, pkt.data.EID8, pkt.data.EID0, pkt.data.DLC);
    //rprintf( "T2 \n");
  }  
  
  n = 5; // little delay before pulling CS low 
 
  if (RTS_CMD)
  {

  CS_2515_LOW();
  CAN2515_WriteSPI(RTS_CMD);
  CAN2515_ReadSPI();
  CS_2515_HIGH();   
  } 
  //debug("%d \n", devstat);
  
  
   // Free the chip
   CAN_MCP2515_Locked = 0;
   
   while ( CAN_MCP2515_RX0BF_unread )
   {
     //CAN2515_GetRXB0Messages();
   }  
   
   while ( CAN_MCP2515_RX1BF_unread )
   {
     //CAN2515_GetRXB1Messages();
   }  
} 

//-----------------------------------------------------------------------------
//  CAN2515_GetMessages(void)
//  Gets the MCP2515 received messages and puts them to the circular buffer
//-----------------------------------------------------------------------------
void CAN2515_GetMessages()
{
    return;
  // If MCP2515 is used by different function, current function MUST be aborted
  // Such a situation may occur when interrupts are enabled
  if (CAN_MCP2515_Locked)
  {
    return;
  }  
  CAN_MCP2515_Locked = 1;
    
  unsigned char i;
  //CAN2515_GetOperationMode();
  unsigned char devStat = CAN2515_ReadStatus();
//  if (devStat > 0)
//  {
//    rprintf("devstat %d \n", devStat);
//  }  
  CAN_PACKET pkt;
  

  if ( devStat & 0b00000001) // if RX0 is full
  {
    CS_2515_LOW();
    CAN2515_WriteSPI(CAN_RD_START_RXB0SIDH); // read RXB0 command
    CAN2515_ReadSPI();
    for (i = 0; i < 13; i++)
    {
      CAN2515_WriteSPI(0x0); // dummy byte to force clock
      pkt.bytes[i] = CAN2515_ReadSPI();
    }
    CS_2515_HIGH();
    CAN_RXBFR_PutData(pkt); 
  }  

  if ( devStat & 0b00000010) // if RX1 is full
  {
    CS_2515_LOW();
    CAN2515_WriteSPI(CAN_RD_START_RXB1SIDH); // read RXB0 command
    CAN2515_ReadSPI();
    for (i = 0; i < 13; i++)
    {
      CAN2515_WriteSPI(0x0); // dummy byte to force clock
      pkt.bytes[i] = CAN2515_ReadSPI();
    }
    CS_2515_HIGH();  
    CAN_RXBFR_PutData(pkt);   
  }
  
  // Free the chip
  CAN_MCP2515_Locked = 0;
} 

//-----------------------------------------------------------------------------
//  CAN2515_GetRXB0Messages()
//  Gets the MCP2515 received messages and puts them to the circular buffer
//
//  WARNING!
//  Please make sure there's a valid data in RXB0 (either with SPI command or pin state),
//  otherwise some trash will be but into circular buffer
//-----------------------------------------------------------------------------
void CAN2515_GetRXB0Messages()
{
    return;
  if (MCP2515_RX0BF)
  {
    //rprintf("CHUJ0");
    return;
  }  
  // If MCP2515 is used by different function, current function MUST be aborted
  // Such a situation may occur when interrupts are enabled
  if (CAN_MCP2515_Locked)
  {
    CAN_MCP2515_RX0BF_unread = 1;
    return;
  }  
  CAN_MCP2515_Locked = 1;
    
  unsigned char i;
  CAN_PACKET pkt;
  CS_2515_LOW();
  CAN2515_WriteSPI(CAN_RD_START_RXB0SIDH); // read RXB0 command
  CAN2515_ReadSPI();
  for (i = 0; i < 13; i++)
  {
    CAN2515_WriteSPI(0x0); // dummy byte to force clock
    pkt.bytes[i] = CAN2515_ReadSPI();
  }
  CS_2515_HIGH();
  CAN_RXBFR_PutData((CAN_PACKET)pkt); 
  
  CAN_MCP2515_RX0BF_unread = 0;
  // Free the chip
   CAN_MCP2515_Locked = 0;
} 

//-----------------------------------------------------------------------------
//  CAN2515_GetRXB1Messages()
//  Gets the MCP2515 received messages and puts them to the circular buffer
//
//  WARNING!
//  Please make sure there's a valid data in RXB0 (either with SPI command or pin state),
//  otherwise some trash will be but into circular buffer
//-----------------------------------------------------------------------------
void CAN2515_GetRXB1Messages()
{
    return;
  if (MCP2515_RX1BF)
  {
    return;
  }  
  // If MCP2515 is used by different function, current function MUST be aborted
  // Such a situation may occur when interrupts are enabled
  if (CAN_MCP2515_Locked)
  {
    CAN_MCP2515_RX1BF_unread = 1;
    return;
  }  
  CAN_MCP2515_Locked = 1;
    
  unsigned char i;
  CAN_PACKET pkt;   
  CS_2515_LOW();
  CAN2515_WriteSPI(CAN_RD_START_RXB1SIDH); // read RXB0 command
  CAN2515_ReadSPI();
  for (i = 0; i < 13; i++)
  {
    CAN2515_WriteSPI(0x0); // dummy byte to force clock
    pkt.bytes[i] = CAN2515_ReadSPI();
  }
  CS_2515_HIGH();  
  CAN_RXBFR_PutData((CAN_PACKET)pkt);   
  
  CAN_MCP2515_RX1BF_unread = 0;
  // Free the chip
   CAN_MCP2515_Locked = 0;
} 

//-----------------------------------------------------------------------------
//  CAN2515_ConfigureRX(void)
//  Confgures the MCP2515 message receiption functionality
//-----------------------------------------------------------------------------
void CAN2515_ConfigureRXTX()
{
  unsigned char n;
  // Disable all MCP2515 interrupt functions apart of TX0 TX1 TX2 buffers empty interrupts;
  CS_2515_LOW();
  CAN2515_WriteSPI(CAN_WRITE); // write command
  CAN2515_ReadSPI();
  CAN2515_WriteSPI(CANINTE); // address to write
  CAN2515_ReadSPI();
  CAN2515_WriteSPI(0b00000100); // byte to write
  CAN2515_ReadSPI();     
  CS_2515_HIGH(); 
 
   n = 5; // little delay before pulling CS low  
  
  // Configure RX0 to receive all messages ( SID+EID, without filters, enable rollover)   
  CS_2515_LOW();
  CAN2515_WriteSPI(CAN_BIT_MODIFY); // bit modify command
  CAN2515_ReadSPI();
  CAN2515_WriteSPI(RXB0CTRL); // address to modify
  CAN2515_ReadSPI();
  CAN2515_WriteSPI(0b01100100); // mask  
  CAN2515_ReadSPI();
  CAN2515_WriteSPI(0b01100100); // byte to write
  CAN2515_ReadSPI();     
  CS_2515_HIGH();    
  
   n = 5; // little delay before pulling CS low  
  
  // Configure RX1 to receive all messages ( SID+EID, without filters)   
  CS_2515_LOW();
  CAN2515_WriteSPI(CAN_BIT_MODIFY); // bit modify command
  CAN2515_ReadSPI();
  CAN2515_WriteSPI(RXB1CTRL); // address to modify
  CAN2515_ReadSPI();
  CAN2515_WriteSPI(0b01100000); // mask  
  CAN2515_ReadSPI();
  CAN2515_WriteSPI(0b01100000); // byte to write
  CAN2515_ReadSPI();     
  CS_2515_HIGH();      
}  


#endif //__CAN2515_SPI_C
