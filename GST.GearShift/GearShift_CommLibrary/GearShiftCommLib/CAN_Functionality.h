#pragma once

#define __CAN_Functionality_h

#include <Windows.h>
#include "RingBuffer.h"
#include <vector>

using namespace std;

namespace GearShiftCommLib
{

#ifdef __CAN_Functionality_cpp
#define EXT_CANIF
#else
#define EXT_CANIF extern
#endif


#define CAN_TX_BUFF_SIZE		      1024
#define CAN_RX_BUFF_SIZE		      1024

#define CAN_INIT_MSG_TIMEOUT_MS   1000
#define CAN_RST_FXD_TRC_TIMEOUT_MS 500

#define CAN_PROP_TQ_PER_BIT       3/8.0
#define CAN_PHASE1_TQ_PER_BIT     2/8.0
#define CAN_PHASE2_TQ_PER_BIT     2/8.0
#define CAN_SJW_TQ_PER_BIT        1/8.0

  // CAN functionality USB communication structures //-----------------------------------------------//
  //-------------------------------------------------------------------------------------------------//

  /// <summary>
  /// USB CAN data
  /// </summary>
  //#pragma pack(1)
  __declspec( align(1) )
  typedef struct
  {
    unsigned __int32      remoteID;
    unsigned __int8       isXtdFrameType;
    unsigned __int8       isRTRFrame;
    unsigned __int8       DLC;
    unsigned __int8       data[8];
    unsigned __int32      timestamp;

  } TUsbCANData;

  // CAN messages transmit buffer
  EXT_CANIF RingBfr::RingBuffer<TUsbCANData> CAN_TxBuffer;
  // CAN messages receive buffer
  EXT_CANIF RingBfr::RingBuffer<TUsbCANData> CAN_RxBuffer;

  EXT_CANIF UINT32 CAN_DevTxBuffSize;
  EXT_CANIF UINT32 CAN_DevTxBuffFill;

  EXT_CANIF UINT32 CAN_DevRxBuffSize;
  EXT_CANIF UINT32 CAN_DevRxBuffFill;

  // Event set when CAN communication enable response is received from the uC
  EXT_CANIF HANDLE CAN_Event_CommDisableCmd_Complete;
  // Event set when CAN configuration packet response is received from the uC
  EXT_CANIF HANDLE CAN_Event_SetConfigCmd_Complete;
  // Event set when pullup enable command response is received from the uC
  EXT_CANIF HANDLE CAN_Event_PullUpEnabledRespRcvd;
  // Event set when pullup disable command response is received from the uC
  EXT_CANIF HANDLE CAN_Event_PullUpDisabledRespRcvd;
  // Event set when fixed trace reset command response is received from the uC
  EXT_CANIF HANDLE CAN_Event_RstFxdTrcRespRcvd;

  //------------------------------------------------------------------------------------------------------------//

  void CAN_InitInternals( void );


//------------------------------------------------------------------------------------------------------------//

                                     void     CAN_InitInternals( void );

                                     int      CAN_ProcessTx( void );


  extern "C" __declspec( dllexport ) UINT32   UsbCANGetDevTxBuffFill( void );

  extern "C" __declspec( dllexport ) UINT32   UsbCANGetDevTxBuffSize( void );

  extern "C" __declspec( dllexport ) UINT32   UsbCANGetDevRxBuffFill( void );

  extern "C" __declspec( dllexport ) UINT32   UsbCANGetDevRxBuffSize( void );

  extern "C" __declspec( dllexport ) UINT32   UsbCANGetUsbDevTxBuffFill( void );

  extern "C" __declspec( dllexport ) UINT32   UsbCANGetUsbDevTxBuffSize( void );

  extern "C" __declspec( dllexport ) UINT32   UsbCANGetUsbDevRxBuffFill( void );

  extern "C" __declspec( dllexport ) UINT32   UsbCANGetUsbDevRxBuffSize( void );

  extern "C" __declspec( dllexport ) void     UsbCANWriteData( TUsbCANData * data, UINT32 num, UINT32 *wrNum );

  extern "C" __declspec( dllexport ) void     UsbCANReadData( TUsbCANData * data, UINT32 num, UINT32 *rdNum );

  extern "C" __declspec( dllexport ) UINT32   UsbCANSetConfig( UINT32 busBaud, double *baudError  );

  extern "C" __declspec( dllexport ) UINT32   UsbCANDisableComm( void );

  // Enables CAN pull-up
  extern "C" __declspec( dllexport ) UINT32   UsbCanEnablePullUp( void );

  // Disables CAN pull-up
  extern "C" __declspec( dllexport ) UINT32   UsbCanDisablePullUp( void );

  extern "C" __declspec( dllexport ) UINT32   UsbCANResetFixedTrace(void);


}

