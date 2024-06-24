#pragma once

#ifndef __CAN2515_SPI_H
#define __CAN2515_SPI_H

/* PIC24FJ64GB108 SPI peripheral library header */

#include "REGS2515.h"
//#include "USBPacket.h"

//#define CAN2515_USE_SPI1
#define CAN2515_USE_SPI2


//-------------MCP2515 SPI commands------------------------
#define CAN_RESET       0xC0  //Reset
#define CAN_READ        0x03  //Read
#define CAN_WRITE       0x02  //Write
#define CAN_RTS         0x80  //Request to Send
#define CAN_RTS_TXB0    0x81  //RTS TX buffer 0
#define CAN_RTS_TXB1    0x82  //RTS TX buffer 1
#define CAN_RTS_TXB2    0x84  //RTS TX buffer 2
#define CAN_RD_STATUS   0xA0  //Read Status
#define CAN_BIT_MODIFY  0x05  //Bit modify  
#define CAN_RX_STATUS   0xB0  //Receive status 

#define CAN_RD_RX_BUFF        0x90  //Base command; requires pointer to RX buffer location
#define CAN_RD_START_RXB0SIDH 0x90  //Starts read at RXB0SIDH
#define CAN_RD_START_RXB0D0   0x92  //Starts read at RXB0D0
#define CAN_RD_START_RXB1SIDH 0x94  //Starts read at RXB1SIDH
#define CAN_RD_START_RXB1D0   0x96  //Starts read at RXB0D1

#define CAN_LOAD_TX     0xFF  //Used to let the function pick the buffer to load
#define CAN_LD_TXB0_ID  0x40  //Points to TXB0SIDH register
#define CAN_LD_TXB0_D0  0x41  //Points to TXB0D0 register
#define CAN_LD_TXB1_ID  0x42  //Points to TXB1SIDH register
#define CAN_LD_TXB1_D0  0x43  //Points to TXB1D0 register
#define CAN_LD_TXB2_ID  0x44  //Points to TXB2SIDH register
#define CAN_LD_TXB2_D0  0x45  //Points to TXB2D0 register

/*  25Cxxx EEPROM instruction set */
#define   SPI_WREN          6              // write enable latch
#define   SPI_WRDI          4              // reset the write enable latch
#define   SPI_RDSR          5              // read status register
#define   SPI_WRSR          1              // write status register
#define   SPI_READ          3              // read data from memory
#define   SPI_WRITE         2              // write data to memory

/*  Bits within status register of 25Cxxx */
#define   WIP           0              // write in progress status
#define   WEL           1              // write enable latch status
#define   BP0           2              // block protection bit status
#define   BP1           3              // block protection bit status






#ifdef __CAN2515_SPI_C
#define EXT__CAN2515_SPI_H 
#else
#define EXT__CAN2515_SPI_H extern
#endif


EXT__CAN2515_SPI_H char CAN2515_GM();


/* FUNCTION PROTOTYPES */
//-----------------------------------------------------------------------------
// Initializes the SPI1
//-----------------------------------------------------------------------------
EXT__CAN2515_SPI_H void CAN2515_InitSPI1( void ); 
//-----------------------------------------------------------------------------
// Initializes the SPI2
//-----------------------------------------------------------------------------
EXT__CAN2515_SPI_H void CAN2515_InitSPI2( void );

EXT__CAN2515_SPI_H void CAN2515_WriteSPI1(char byte);
EXT__CAN2515_SPI_H void CAN2515_WriteSPI2(char byte);
EXT__CAN2515_SPI_H char CAN2515_ReadSPI1(void);
EXT__CAN2515_SPI_H char CAN2515_ReadSPI2(void);

#ifdef CAN2515_USE_SPI2

   #define CAN2515_WriteSPI CAN2515_WriteSPI2
   #define CAN2515_ReadSPI CAN2515_ReadSPI2
   #define CAN2515_InitSPI CAN2515_InitSPI2

#endif //CAN2515_USE_SPI2

#ifdef CAN2515_USE_SPI1

   #define CAN2515_WriteSPI CAN2515_WriteSPI1
   #define CAN2515_ReadSPI CAN2515_ReadSPI1
   #define CAN2515_InitSPI CAN2515_InitSPI1

#endif //CAN2515_USE_SPI1



//-----------------------------------------------------------------------------
//  CAN2515_Reset(void)
//-----------------------------------------------------------------------------
EXT__CAN2515_SPI_H void CAN2515_Reset(void);


//-------------------------------------------------------------------------
//  CAN2515_ByteWrite(unsigned char addr, unsigned char value )
//-------------------------------------------------------------------------
EXT__CAN2515_SPI_H void CAN2515_ByteWrite(unsigned char addr, unsigned char value );


//-------------------------------------------------------------------------
//     Function Name:    CAN2515_SeqWrite                                
//     Return Value:     None                                        
//     Parameters:       Starting address, numbytes, and pointer
//                       to an array.               
//     Description:      This routine performs a sequential write.         
//-------------------------------------------------------------------------
EXT__CAN2515_SPI_H void CAN2515_SeqWrite(unsigned char startaddr, unsigned char numbytes, char *data);


//-----------------------------------------------------------------------------
//  CAN2515_ByteRead(unsigned char addr)
//-----------------------------------------------------------------------------
EXT__CAN2515_SPI_H unsigned char CAN2515_ByteRead(unsigned char addr);


//-----------------------------------------------------------------------------
//	 CAN2515_SeqRead(unsigned char startaddr, unsigned char numbytes, char *data)
//     Return Value:     None (puts read data in string)                                        
//     Parameters:       Starting address, numbytes, and pointer
//                       to an array.               
//     Description:      Sequential read from the MCP2515         
//-----------------------------------------------------------------------------
EXT__CAN2515_SPI_H void CAN2515_SeqRead(unsigned char startaddr, unsigned char numbytes, char *data);


//-----------------------------------------------------------------------------
//  CAN2515_ReadRX(unsigned char opcode, unsigned char numbytes, char *data)
//	 opcode = SPI opcode and pointer to RXBn register location// (see datasheet)
//  numbytes = number of bytes to read
//  *data = pointer to a string of data
//	
//-----------------------------------------------------------------------------
EXT__CAN2515_SPI_H void CAN2515_ReadRX(unsigned char opcode, unsigned char numbytes, char *data);


//-----------------------------------------------------------------------------
//  CAN2515_LoadTX(unsigned char numbytes, char *data)
//  opcode = SPI opcode and pointer to TXBn register location// (see datasheet)
//  numbytes = number of bytes to write
//  *data = pointer to a string of data
//	
//-----------------------------------------------------------------------------
EXT__CAN2515_SPI_H char CAN2515_LoadTX(unsigned char numbytes, char *data);


//-----------------------------------------------------------------------------
//  CAN2515_RTS(unsigned char buffer)
//	buffer = CAN_RTS_TXBn; where 'n' = 0, 1, 2
//  OR
//	buffer = CAN_RTS; followed by | 0 - 7 (e.g., "CAN_RTS | 7" sends TX0 and TX1)
//  OR
//	buffer = CAN_RTS_TXBn | CAN_RTS_TXBn | CAN_RTS_TXBn; where 'n' = 0, 1, 2
//-----------------------------------------------------------------------------
EXT__CAN2515_SPI_H void CAN2515_RTS(unsigned char buffer);


//-----------------------------------------------------------------------------
//  CAN2515_ReadStatus(void)
//  Performs Read Status command and returns result
//-----------------------------------------------------------------------------
EXT__CAN2515_SPI_H char CAN2515_ReadStatus(void);


EXT__CAN2515_SPI_H void CAN2515_SetNormalOperationMode();


EXT__CAN2515_SPI_H unsigned char CAN2515_GetOperationMode();
  


EXT__CAN2515_SPI_H void CAN2515_SetBitTiming(unsigned char _BRP, unsigned char _PS1, unsigned char _PS2, unsigned char _PRSEG, unsigned char _SJW);


EXT__CAN2515_SPI_H void CAN2515_GetBitTiming(unsigned char *_CNF1, unsigned char *_CNF2, unsigned char *_CNF3);


EXT__CAN2515_SPI_H void CAN2515_SendBufferedMessage();

//-----------------------------------------------------------------------------
//  CAN2515_GetMessages(void)
//  Gets the MCP2515 received messages and puts them to the circular buffer
//-----------------------------------------------------------------------------
EXT__CAN2515_SPI_H void CAN2515_GetMessages();

//-----------------------------------------------------------------------------
//  CAN2515_GetMessages(void)
//  Gets the MCP2515 RX0B received messages and puts them to the circular buffer
//-----------------------------------------------------------------------------
EXT__CAN2515_SPI_H void CAN2515_GetRXB0Messages();

//-----------------------------------------------------------------------------
//  CAN2515_GetMessages(void)
//  Gets the MCP2515 RX1B received messages and puts them to the circular buffer
//-----------------------------------------------------------------------------
EXT__CAN2515_SPI_H void CAN2515_GetRXB1Messages();

//-----------------------------------------------------------------------------
//  CAN2515_ConfigureRX(void)
//  Confgures the MCP2515 message receiption functionality
//-----------------------------------------------------------------------------
EXT__CAN2515_SPI_H void CAN2515_ConfigureRXTX();






//enum CAN2515_OpCodes 
//{
//   
//	CMD_GET_VERSION = 0x01,
//	CMD_GET_CONFIG = 0x02,
//	CMD_SET_CONFIG = 0x03,
//	CMD_START = 0x04,
//	CMD_STOP = 0x05,
//	CMD_DEV_DATA = 0x06,
//	CMD_PWM_DATA = 0x08,
//	CMD_GET_BUFF_FILL = 0x09,
//	CMD_GET_STATE = 0x0A,
//	CMD_POLL_DATA = 0x0B,
//
//	
//	CMD_CAN_GET_CONFIG = 0xA2,
//	CMD_CAN_SET_CONFIG = 0xA3,
//	CMD_CAN_ADD_NODES = 0xA4,
//	
//	CMD_CAN_RX_DATA = 0xA6,	
//	CMD_CAN_TX_DATA = 0xA8,
//	CMD_CAN_GET_STATE = 0xA9,
//	
//	CMD_UI_UPDATE = 0xB0
//
//};



#endif  //__CAN2515_SPI_H

