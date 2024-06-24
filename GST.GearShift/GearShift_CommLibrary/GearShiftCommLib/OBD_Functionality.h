#pragma once

#define __OBD_Functionality_h

#include <Windows.h>
#include "RingBuffer.h"
#include <fstream>

using namespace std;


namespace GearShiftCommLib 
{

#ifdef __OBD_Functionality_cpp
#define EXT_OBDIF 
#else
#define EXT_OBDIF extern
#endif


#define OBD_TX_BUFF_SIZE		    1024
#define OBD_RX_BUFF_SIZE		    1024
#define OBD_RX_TMP_BUFF_SIZE		255

#define OBD_INIT_MSG_TIMEOUT_MS 5000 //INFINITE     //2000


  // OBD functionality USB communication structures //-----------------------------------------------//
  //-------------------------------------------------------------------------------------------------//

  enum Elm327Protocol 
  {
    ELM327_PROTO_AUTO                    = 0x00,
    ELM327_PROTO_SAE_J1850_PWM           = 0x01,
    ELM327_PROTO_SAE_J1850_VPW           = 0x02,
    ELM327_PROTO_ISO_91412               = 0x03,
    ELM327_PROTO_ISO_142304_KWP_5BI      = 0x04,
    ELM327_PROTO_ISO_142304_KWP_FI       = 0x05,
    ELM327_PROTO_ISO_157654_CAN_11B500K  = 0x06,
    ELM327_PROTO_ISO_157654_CAN_29B500K  = 0x07,
    ELM327_PROTO_ISO_157654_CAN_11B250K  = 0x08, 
    ELM327_PROTO_ISO_157654_CAN_29B250K  = 0x09,
    ELM327_PROTO_SAE_J1939_CAN_29B250K   = 0x0A
  };

  enum Elm327State
  {
    ELM327STATE_UNINITIALIZED           = 0x00,
    ELM327STATE_INIT_MSG_RCVD           = 0x01, 
    ELM327STATE_PROTOCOL_DEFINED        = 0x02, 
    ELM327STATE_READY_TO_TALK           = 0x10,
    ELM327STATE_INIT_TIMEOUT_ERR        = 0xF0,
    ELM327STATE_ECU_NOT_PRESENT         = 0xFF
  };

  /// <summary>
  /// USB OBD TX data (sent to the device)
  /// </summary>
//__declspec(align(1))
#pragma pack(1)
  typedef struct 
  {
    unsigned char charCount;
    char chars[50];
  } TUsbOBDTxData;

  /// <summary>
  /// USB OBD RX data (received from the device
  /// </summary>
//__declspec(align(1))
#pragma pack(1)
  typedef struct 
  {
    UINT32        srcAddr;
    byte          respToMode;
    byte          respToPID;
    byte          bytesCount;
    unsigned char bytes[50];
  } TUsbOBDRxData;

  // OBD messages transmit buffer
  EXT_OBDIF RingBfr::RingBuffer<TUsbOBDTxData> OBD_TxBuffer;
  // OBD messages receive buffer
  EXT_OBDIF RingBfr::RingBuffer<TUsbOBDRxData> OBD_RxBuffer;

  // OBD messages receive temporary buffer
  EXT_OBDIF RingBfr::RingBuffer<char> OBD_RxTmpBuffer;

  EXT_OBDIF UINT32 OBD_DevTxBuffSize;
  EXT_OBDIF UINT32 OBD_DevTxBuffFill;

  EXT_OBDIF UINT32 OBD_DevRxBuffSize;
  EXT_OBDIF UINT32 OBD_DevRxBuffFill;

  EXT_OBDIF int OBD_Elm327State;

  EXT_OBDIF int OBD_Elm327CurrentProtocol;

  EXT_OBDIF HANDLE OBD_Event_ELM_BFRS_INIT_COMPLETE;

  EXT_OBDIF HANDLE OBD_Event_StartupMessageReceived;
  EXT_OBDIF bool OBD_StartupMessageRequested;

  EXT_OBDIF HANDLE OBD_Event_ATSP0RespReceived;
  EXT_OBDIF bool OBD_Event_ATSP0Requested;

  EXT_OBDIF HANDLE OBD_Event_ProtocolNumberReceived;
  EXT_OBDIF bool OBD_Event_ProtocolNumberRequested;

  EXT_OBDIF HANDLE OBD_Event_ATH1CmdRespReceived;
  EXT_OBDIF bool OBD_ATH1CmdRequested;

  EXT_OBDIF HANDLE OBD_Event_0101CmdRespReceived;
  EXT_OBDIF bool OBD_0101CmdRequested;

  //EXT_OBDIF ofstream dump;


  //------------------------------------------------------------------------------------------------------------//
  
  extern "C" __declspec( dllexport )   void UsbOBDProcessSingleRawResponse(char* msg, int charCount);


                                       void OBD_InitInternals( void );

                                       void UsbOBDProcessTmpRxBuffer( void );

                                       int  OBD_ProcessTx( void );

  extern "C" __declspec( dllexport ) UINT32 UsbOBDGetDevTxBuffFill( void );

  extern "C" __declspec( dllexport ) UINT32 UsbOBDGetDevTxBuffSize( void );

  extern "C" __declspec( dllexport ) UINT32 UsbOBDGetDevRxBuffFill( void );

  extern "C" __declspec( dllexport ) UINT32 UsbOBDGetDevRxBuffSize( void );

  extern "C" __declspec( dllexport )   void UsbOBDWriteData( TUsbOBDTxData * data, unsigned int num, unsigned int * wrNum );

                                       void UsbOBDInitOnlySendCmd( char *cmd );

  extern "C" __declspec( dllexport )   void UsbOBDSendCmd( char *cmd );

  extern "C" __declspec( dllexport )   void UsbOBDReadData( TUsbOBDRxData * data, unsigned int num, unsigned int * rdNum );

  extern "C" __declspec( dllexport )    int UsbOBDElm327Init( void );

}
