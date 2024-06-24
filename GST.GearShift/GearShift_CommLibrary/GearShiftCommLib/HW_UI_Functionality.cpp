#define __HW_UI_Functionality_cpp

#include <windows.h>
#include <string>
#include "../Firmware/PIC24_firmware/USBPacket.h"

#include "HW_UI_Functionality.h"
#include "GearShiftCommLib.h"
#include "GearShiftCommLibPriv.h"

namespace GearShiftCommLib
{

  //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  //________________________ UI BLOCK _____________________________________________________________________________________________/
  //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

  void UsbUIDisplayLcdRow1Msg( char message[17] )
  {
    EnterCriticalSection( &txCmdLock );
    {
      if( devState.dllState == DEV_CONNECTED )
      {
        pktOutCmd.cmd = CMD_UI_UPDATE;
        pktOutCmd.UIDataPacket.lcdUpdateRow1 = 1;
        pktOutCmd.UIDataPacket.lcdUpdateRow2 = 0;
        pktOutCmd.UIDataPacket.bgSetDispMode = 0;
        strcpy_s(pktOutCmd.UIDataPacket.lcdRow1Str, message);
        ResetEvent( hEventTx );
        PostThreadMessage( m_dwTxThreadID, MSG_USB_SEND, sizeof( pktOutCmd.UIDataPacket ), 0 );
        WaitForSingleObjectEx( hEventTx, 5000, false );
      }
    } LeaveCriticalSection( &txCmdLock );
  }

  void UsbUIDisplayLcdRow2Msg( char message[17] )
  {
    EnterCriticalSection( &txCmdLock );
    {
      if( devState.dllState == DEV_CONNECTED )
      {
        pktOutCmd.cmd = CMD_UI_UPDATE;
        pktOutCmd.UIDataPacket.lcdUpdateRow2 = 1;
        pktOutCmd.UIDataPacket.lcdUpdateRow1 = 0;
        pktOutCmd.UIDataPacket.bgSetDispMode = 0;
        strcpy_s(pktOutCmd.UIDataPacket.lcdRow2Str, message);
        ResetEvent( hEventTx );
        PostThreadMessage( m_dwTxThreadID, MSG_USB_SEND, sizeof( pktOutCmd.UIDataPacket ), 0 );
        WaitForSingleObjectEx( hEventTx, 5000, false );
      }
    } LeaveCriticalSection( &txCmdLock );
  }

  void UsbUISetBargraphsMode( unsigned char mode )
  {
    EnterCriticalSection( &txCmdLock );
    {
      if( devState.dllState == DEV_CONNECTED )
      {
        pktOutCmd.cmd = CMD_UI_UPDATE;
        pktOutCmd.UIDataPacket.bgSetDispMode = 1;
        pktOutCmd.UIDataPacket.lcdUpdateRow2 = 0;
        pktOutCmd.UIDataPacket.lcdUpdateRow1 = 0;
        pktOutCmd.UIDataPacket.bgDispMode = mode;
        ResetEvent( hEventTx );
        PostThreadMessage( m_dwTxThreadID, MSG_USB_SEND, sizeof( pktOutCmd.UIDataPacket ), 0 );
        WaitForSingleObjectEx( hEventTx, 5000, false );
      }
    } LeaveCriticalSection( &txCmdLock );
  }


}