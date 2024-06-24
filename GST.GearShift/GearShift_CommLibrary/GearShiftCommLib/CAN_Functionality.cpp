#define __CAN_Functionality_cpp

#include <windows.h>
#include <stdio.h>
#include <iostream>


#include "../Firmware/PIC24_firmware/USBPacket.h"

#include "CAN_Functionality.h"

#include "RingBuffer.h"

#include "GearShiftCommLibPriv.h"


using namespace std;

namespace GearShiftCommLib
{

  // Configures the device and enables CAN communication
  UINT32 UsbCANSetConfig( UINT32 busBaud, double *baudError )
  {
    //CAN_Sniffer_FixedTraceRecords.clear();

    if( devState.dllState != DEV_CONNECTED )
    {
      return 1; // return error
    }

    long double FOSC = 16000000; // [Hz]
    long double TOSC = 1 / FOSC; // [s]

    long double BAUD = busBaud;  // [bps]
    long double Tbit = 1 / BAUD; // [spb]


    // Find the least error settings
    int leastErrTQc = 0;
    int leastErrBRP = 0;
    long double leastErr = 100;
    for (int tqc = 8; tqc < 26; tqc++)
    {
      long double TQ = Tbit / tqc;
      long double BRP = ( TQ * FOSC ) / 2.0;

      if ( (int)BRP > 64 ) // disallowed (BRP is only 6bit long
        continue;
      // Calculate real baud rate for this BRP value
      long double realBaud = ( BAUD * tqc * 2 * (int)BRP / (long double)FOSC ) * BAUD;
      // Calculate real baud rate error for this BRP setting
      double err = ( BAUD - realBaud ) * 100.0 / BAUD;
      if (err < 0) // get absolute value
        err *= -1.0;
      *baudError = err;
      cout << err << "%" << endl;

      // Do "find least error" assignments
      if (err < leastErr)
      {
        leastErr = err;
        leastErrTQc = tqc;
        leastErrBRP = (int)BRP - 1; // -1 because MCP2515 register value 0x0 represents the 0x1 prescaler
        if (err == 0.0) // if zero error found, no need to continue through the loop
          break;
      }
    }
    //#define CAN_PROP_TQ_PER_BIT     2/8.0
    //#define CAN_PHASE1_TQ_PER_BIT   2/8.0
    //#define CAN_PHASE2_TQ_PER_BIT   3/8.0
    //#define CAN_SJW_TQ_PER_BIT      2/8.0

    UINT16 CAN_PropSeg_TQs = 0;
    UINT16 CAN_PS1_TQs = 0;
    UINT16 CAN_PS2_TQs = 0;
    UINT16 CAN_SJW_TQs = 0;

    CAN_PropSeg_TQs = (UINT16)(leastErrTQc * CAN_PROP_TQ_PER_BIT);
    CAN_PS2_TQs = (UINT16)(leastErrTQc * CAN_PHASE2_TQ_PER_BIT);
    CAN_PS1_TQs = leastErrTQc - CAN_PS2_TQs - CAN_PropSeg_TQs - 1; // (-1 because of fixed sync TQ)
    CAN_SJW_TQs = (UINT16)(leastErrTQc * CAN_SJW_TQ_PER_BIT);


    ResetEvent(CAN_Event_SetConfigCmd_Complete);

    EnterCriticalSection( &txCmdLock );
    {
      if( devState.dllState == DEV_CONNECTED )
      {
        pktOutCmd.cmd = CMD_CAN_SET_CONFIG;
        pktOutCmd.CANConfigPacket.MCP2515_BRPl  = (unsigned char)leastErrBRP;
        pktOutCmd.CANConfigPacket.MCP2515_PRSEG = (unsigned char)CAN_PropSeg_TQs;
        pktOutCmd.CANConfigPacket.MCP2515_PS1   = (unsigned char)CAN_PS1_TQs;
        pktOutCmd.CANConfigPacket.MCP2515_PS2   = (unsigned char)CAN_PS2_TQs;
        pktOutCmd.CANConfigPacket.MCP2515_SJW   = (unsigned char)CAN_SJW_TQs;
        ResetEvent( hEventTx );
        PostThreadMessage( m_dwTxThreadID, MSG_USB_SEND, sizeof( pktOutCmd.CANConfigPacket ), 0 );
        WaitForSingleObjectEx( hEventTx, 5000, false );
      }
    } LeaveCriticalSection( &txCmdLock );

    if ( 0!= WaitForSingleObject(CAN_Event_SetConfigCmd_Complete, CAN_INIT_MSG_TIMEOUT_MS ) )
    {
      return 1;
    }
    else
    {
      return 0;
    }
  }


   // Disables CAN messages receiption and transmission
   UINT32 UsbCANDisableComm( void )
   {
      ResetEvent(CAN_Event_CommDisableCmd_Complete);
      EnterCriticalSection( &txCmdLock );
      {
        if( devState.dllState != DEV_CONNECTED )
        {
          return 1; // return error
        }
        if( devState.dllState == DEV_CONNECTED )
        {
          pktOutCmd.cmd = CMD_CAN_DISABLE_COMM;
          ResetEvent( hEventTx );
          PostThreadMessage( m_dwTxThreadID, MSG_USB_SEND, sizeof( unsigned char ), 0 );
          WaitForSingleObjectEx( hEventTx, 5000, false );
        }
      } LeaveCriticalSection( &txCmdLock );

      if ( 0!= WaitForSingleObject(CAN_Event_CommDisableCmd_Complete, CAN_INIT_MSG_TIMEOUT_MS ) )
      {
        //CAN_Sniffer_FixedTraceRecords.clear();
        return 1;
      }
      else
     {
       //CAN_Sniffer_FixedTraceRecords.clear();
       return 0;
     }
   }

  // Enables CAN pull-up
  UINT32 UsbCanEnablePullUp( void )
  {
    ResetEvent(CAN_Event_PullUpEnabledRespRcvd);
    EnterCriticalSection( &txCmdLock );
    {
      if( devState.dllState != DEV_CONNECTED )
      {
        return 1; // return error
      }
      if( devState.dllState == DEV_CONNECTED )
      {
        pktOutCmd.cmd = CMD_CAN_EN_PULLUP;
        ResetEvent( hEventTx );
        PostThreadMessage( m_dwTxThreadID, MSG_USB_SEND, sizeof( unsigned char ), 0 );
        WaitForSingleObjectEx( hEventTx, 5000, false );
      }
    } LeaveCriticalSection( &txCmdLock );

    if ( 0!= WaitForSingleObject(CAN_Event_PullUpEnabledRespRcvd, CAN_INIT_MSG_TIMEOUT_MS ) )
    {
      return 1;
    }
    else
    {
      return 0;
    }
  }

  // Disables CAN pull-up
  UINT32 UsbCanDisablePullUp( void )
  {
    ResetEvent(CAN_Event_PullUpDisabledRespRcvd);
    EnterCriticalSection( &txCmdLock );
    {
      if( devState.dllState != DEV_CONNECTED )
      {
        return 1; // return error
      }
      if( devState.dllState == DEV_CONNECTED )
      {
        pktOutCmd.cmd = CMD_CAN_DSBL_PULLUP;
        ResetEvent( hEventTx );
        PostThreadMessage( m_dwTxThreadID, MSG_USB_SEND, sizeof( unsigned char ), 0 );
        WaitForSingleObjectEx( hEventTx, 5000, false );
      }
    } LeaveCriticalSection( &txCmdLock );

    if ( 0!= WaitForSingleObject(CAN_Event_PullUpDisabledRespRcvd, CAN_INIT_MSG_TIMEOUT_MS ) )
    {
      return 1;
    }
    else
    {
      return 0;
    }
  }
  // Initializes the CAN internal variables
  void CAN_InitInternals( void )
  {

    //CAN_Sniffer_FixedTraceModeEnabled = true;
    //CAN_Sniffer_RollingTraceModeEnabled = false;

    CAN_Event_CommDisableCmd_Complete = CreateEvent(NULL, TRUE, FALSE, (LPCWSTR)"CAN_Event_CommEnableCmd_Complete");
    SetEvent( CAN_Event_CommDisableCmd_Complete );

    CAN_Event_SetConfigCmd_Complete = CreateEvent(NULL, TRUE, FALSE, (LPCWSTR)"CAN_Event_SetConfigCmd_Complete");
    SetEvent( CAN_Event_SetConfigCmd_Complete );

    CAN_Event_PullUpEnabledRespRcvd = CreateEvent(NULL, TRUE, FALSE, (LPCWSTR)"CAN_Event_PullUpDisabledRespRcvd");
    SetEvent( CAN_Event_PullUpEnabledRespRcvd );

    CAN_Event_PullUpDisabledRespRcvd = CreateEvent(NULL, TRUE, FALSE, (LPCWSTR)"CAN_Event_PullUpDisabledRespRcvd");
    SetEvent( CAN_Event_PullUpDisabledRespRcvd );

    CAN_Event_RstFxdTrcRespRcvd = CreateEvent(NULL, TRUE, FALSE, (LPCWSTR)"CAN_Event_RstFxdTrcRespRcvd");
    SetEvent( CAN_Event_RstFxdTrcRespRcvd );
  }

  UINT32 UsbCANResetFixedTrace(void)
  {
    // disable fixed trace
    //CAN_Sniffer_FixedTraceModeEnabled = 0;

    ResetEvent( CAN_Event_RstFxdTrcRespRcvd );

    EnterCriticalSection( &txCmdLock );
    {
      if( devState.dllState != DEV_CONNECTED )
      {
        return 1; // return error
      }
      if( devState.dllState == DEV_CONNECTED )
      {
        pktOutCmd.cmd = CMD_CAN_RST_FXD_TRC;
        ResetEvent( hEventTx );
        PostThreadMessage( m_dwTxThreadID, MSG_USB_SEND, sizeof( unsigned char ), 0 );
        WaitForSingleObjectEx( hEventTx, 5000, false );
      }
    } LeaveCriticalSection( &txCmdLock );


    if ( 0!= WaitForSingleObject( CAN_Event_RstFxdTrcRespRcvd, CAN_RST_FXD_TRC_TIMEOUT_MS ) )
    {
      cout << "timeout @ reset fixed trace table \n";
      return 1;
    }
    else // if not timeout
    {
      // Clear the traces vector
      //CAN_Sniffer_FixedTraceRecords.clear();
      //CAN_Sniffer_FixedTraceModeEnabled = 1;
      cout << "reset fixed trace table OK" << endl;
      return 0;
    }

  }

  int CAN_ProcessTx( void )
  {
    // SEND CAN DATA
    if ( devState.dllState == DEV_CONNECTED )
    {
      int pktsToWrite = ( ( CAN_DevTxBuffSize * 3) / 4 ) - CAN_DevTxBuffFill;
      //cout << pktsToWrite << endl;
      if (CAN_TxBuffer.GetBuffFill() < pktsToWrite)
      {
        pktsToWrite = CAN_TxBuffer.GetBuffFill();
      }
      if (pktsToWrite > 3)
      {
        pktsToWrite = 3; // only 3 messages fit in 1 USB packet
      }
      if (pktsToWrite > 0 )
      {
        //cout << "TX\n";
        //cout << pktsToWrite << endl;
        //cout << "devtbfll: " << CAN_DevTxBuffFill << endl;
        //cout << (int)pktIn.CANDataPacket.msgCount << endl;
        //cout << CAN_RxBuffer.GetBuffFill()+1 << endl;
        pktOut.cmd = CMD_CAN_DATA;
        pktOut.CANDataPacket.msgCount = pktsToWrite;
        for (int i = 0; i < pktsToWrite; i++)
        {
          TUsbCANData msg;
          CAN_TxBuffer.GetData( &msg );
          if ( msg.isXtdFrameType )
          {
            //assign properly the ID ( see datasheet to understand bitshifts )
            UINT16 SID = msg.remoteID >> 18 & 0x7FF; // 11 SID bits before EID 18 bits
            UINT32 EID = msg.remoteID & 0x3FFFF; // 18 EID least significant bits
            pktOut.CANDataPacket.msgs[i].data.SIDH = ( SID >> 3 );
            pktOut.CANDataPacket.msgs[i].data.SIDL = ( (SID & 0x7) << 5 ) | 0x08 | ( EID >> 16 );
            pktOut.CANDataPacket.msgs[i].data.EID8 = ( (EID >> 8) & 0xFF );
            pktOut.CANDataPacket.msgs[i].data.EID0 = ( EID & 0xFF );
            //cout << " SIDH: " << (int)pktOut.CANDataPacket.msgs[i].data.SIDH << " SIDL: " << (int)pktOut.CANDataPacket.msgs[i].data.SIDL
            //  << " EIDH: " << (int)pktOut.CANDataPacket.msgs[i].data.EID8 << " EIDL: " << (int)pktOut.CANDataPacket.msgs[i].data.EID0 << endl;
          }
          else // if this is standard frame
          {
            pktOut.CANDataPacket.msgs[i].data.SIDH = ( (msg.remoteID >> 3) & 0xFF);
            pktOut.CANDataPacket.msgs[i].data.SIDL = ( (msg.remoteID & 0x07) << 5 );
            pktOut.CANDataPacket.msgs[i].data.EID8 = 0x0;
            pktOut.CANDataPacket.msgs[i].data.EID0 = 0x0;
            //cout << " SIDH: " << (int)pktOut.CANDataPacket.msgs[i].data.SIDH << " SIDL: " << (int)pktOut.CANDataPacket.msgs[i].data.SIDL
            //  << " EIDH: " << (int)pktOut.CANDataPacket.msgs[i].data.EID8 << " EIDL: " << (int)pktOut.CANDataPacket.msgs[i].data.EID0 << endl;
          }
          pktOut.CANDataPacket.msgs[i].data.timestamp = msg.timestamp;
          pktOut.CANDataPacket.msgs[i].data.DLC = msg.DLC & 0x0F; // &00001111 - DLC is only the last four bits of this register)
          for (int j = 0; j < msg.DLC; j++)
          {
            pktOut.CANDataPacket.msgs[i].data.data[j] = msg.data[j];
          }
        }
        //MPUSBWrite( outPipe, ( void * ) &pktOut, sizeof( pktOut.CANDataPacket ), &lnWr, 7 );
        WinUsb_WritePipe(MyWinUSBInterfaceHandle, 0x01, (PUCHAR)&pktOut, sizeof( pktOut.CANDataPacket ), &lnWr, NULL);
        tx = 1;
      }
    }
    return 0;
  }

  UINT32 UsbCANGetDevTxBuffFill( void )
  {
    return CAN_TxBuffer.GetBuffFill();
  }

  UINT32 UsbCANGetDevTxBuffSize( void )
  {
    return CAN_RX_BUFF_SIZE;
  }

  UINT32 UsbCANGetDevRxBuffFill( void )
  {
    return CAN_RxBuffer.GetBuffFill();
  }

  UINT32 UsbCANGetDevRxBuffSize( void )
  {
    return CAN_RX_BUFF_SIZE;
  }

  UINT32 UsbCANGetUsbDevTxBuffFill( void )
  {
    return CAN_DevTxBuffFill;
  }

  UINT32 UsbCANGetUsbDevTxBuffSize( void )
  {
    return CAN_DevTxBuffSize;
  }

  UINT32 UsbCANGetUsbDevRxBuffFill( void )
  {
    return CAN_DevRxBuffFill;
  }

  UINT32 UsbCANGetUsbDevRxBuffSize( void )
  {
    return CAN_DevRxBuffSize;
  }

  void UsbCANWriteData( TUsbCANData * data, UINT32 num, UINT32 * wrNum )
  {
    UINT32 framesToWrite = CAN_TX_BUFF_SIZE - CAN_TxBuffer.GetBuffFill();
    if (framesToWrite > num)
      framesToWrite = num;
    for (UINT32 i = 0; i < framesToWrite; i++)
    {
      CAN_TxBuffer.PutData( data[i] );
    }
    *wrNum = framesToWrite;
  }

  void UsbCANReadData( TUsbCANData * data, UINT32 num, UINT32 * rdNum )
  {
    UINT32 framesToRead = CAN_RxBuffer.GetBuffFill();
    if (framesToRead > num)
      framesToRead = num;
    for (UINT32 i = 0; i < framesToRead; i++)
    {
      CAN_RxBuffer.GetData( &data[i] );
    }
    *rdNum = framesToRead;
  }

  //UINT32 UsbCANGetFixedTraceTable(  TCanFixedTraceRecord * data, UINT32 num)
  //{
  //  UINT32 framesToRead = CAN_Sniffer_FixedTraceRecords.size();
  //  if (framesToRead > num)
  //    framesToRead = num;
  //  for (UINT32 i = 0; i < framesToRead; i++)
  //  {
  //    data[i] = CAN_Sniffer_FixedTraceRecords[i];
  //  }
  //  return framesToRead;
  //}

}