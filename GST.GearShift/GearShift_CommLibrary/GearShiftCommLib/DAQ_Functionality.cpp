#define __DAQ_Functionality_cpp

#include <windows.h>
#include <iostream>
#include <fstream>
#include <iomanip>
#include <Dbt.h>
#include <string>

#include "../Firmware/PIC24_firmware/USBPacket.h"

#include "DAQ_Functionality.h"

#include "GearShiftCommLibPriv.h"

using namespace std;

namespace GearShiftCommLib
{

  void DAQ_InitInternals( void )
  {

    DAQ_ManualDrivesNeedUpdating = false;
    for (int i = 0; i < 9; i++)
    {
      DAQ_ManualDrivesEnabled[ i ] = false;
    }

    DAQ_Event_BfrRstCmd_Complete = CreateEvent(NULL, TRUE, FALSE, (LPCWSTR)"DAQ_Event_BfrRstCmd_Complete");
    SetEvent( DAQ_Event_BfrRstCmd_Complete );
  }

  UINT32 UsbDAQResetBuffersAndCounters( )
  {
    return 0;
  }
  void UsbSetDAQConfig( int pwmFreq, unsigned char currReadChannsCount, unsigned char currReadChannsIndices[9] )
  {
    EnterCriticalSection( &txCmdLock );
    {
      devConfig.pwmFreq = pwmFreq;
      devConfig.currReadChannsCount = currReadChannsCount;
      for (int i = 0; i < currReadChannsCount && i < 9; i++)
      {
        if (currReadChannsIndices[i] >= 0 && currReadChannsIndices[i] < 18)
          devConfig.currReadChannsIndices[i] = currReadChannsIndices[i];
        else
          devConfig.currReadChannsIndices[i] = 0;
      }
      if( devState.dllState == DEV_CONNECTED )
      {
        pktOutCmd.cmd = CMD_SET_CONFIG;
        pktOutCmd.devConfig.frequency = devConfig.pwmFreq;
        pktOutCmd.devConfig.currReadChannsCount = currReadChannsCount;
        for (int i = 0; i < devConfig.currReadChannsCount && i < 9; i++)
        {
          pktOutCmd.devConfig.currReadChannsIndices[i] = devConfig.currReadChannsIndices[i];
        }
        ResetEvent( hEventTx );
        PostThreadMessage( m_dwTxThreadID, MSG_USB_SEND, sizeof( pktOutCmd.devConfig ), 0 );
        WaitForSingleObjectEx( hEventTx, 5000, false );
      }
    } LeaveCriticalSection( &txCmdLock );
  }

  void UsbDAQEnableManualDriverControl( unsigned char index, unsigned char value )
  {
    DAQ_ManualDrivesEnabled[index] = true;
    DAQ_ManualDrivesValues[index] = value;
    DAQ_ManualDrivesNeedUpdating = true;
  }

  void UsbDAQSetManualDriverValue( unsigned char index, unsigned char value )
  {
    DAQ_ManualDrivesEnabled[index] = true;
    DAQ_ManualDrivesValues[index] = value;
    DAQ_ManualDrivesNeedUpdating = true;
  }

  void UsbDAQDisableAllManualDriverControls( void )
  {
    for (int i = 0; i < 9; i++)
    {
      DAQ_ManualDrivesEnabled[i] = false;
      DAQ_ManualDrivesValues[i] = 0;
    }
    DAQ_ManualDrivesNeedUpdating = true;
  }

  void UsbDAQDisableManualDriverControl( unsigned char index )
  {
    DAQ_ManualDrivesEnabled[index] = false;
    DAQ_ManualDrivesValues[index] = 0;
    DAQ_ManualDrivesNeedUpdating = true;
  }

  void UsbGetConfig( unsigned int * numPressSense, unsigned int * numCurrSense, unsigned int * numPWMDrv, unsigned int * freq, unsigned int * currentADCMask )
  {
    EnterCriticalSection( &rxLock );
    {
      if( devState.dllState == DEV_CONNECTED )
      {
        *numPressSense = devConfig.numPressSense;
        *numCurrSense = devConfig.numCurrSense;
        *numPWMDrv = devConfig.numPwmDrv;
        *freq = devConfig.pwmFreq;
        *currentADCMask = devConfig.currentADCMask;
      }
    } LeaveCriticalSection( &rxLock );
  }

  void UsbWriteData( TUsbTxData * data, unsigned int num, unsigned int * wrNum )
  {
    unsigned int i;
    ofstream myfile;
    myfile.open ("c:/wdupie.txt", ios::out || ios::app);

    EnterCriticalSection( &txLock ); {
      if( txFill + num > NUM_TX_BUFF ) {
        *wrNum = NUM_TX_BUFF - txFill;
      } else {
        *wrNum = num;
      }
      num = *wrNum;

      for( i = 0; i < num; i++ )
	  {
        usbTxData[ txWritePos ] = data[ i ];
		    myfile << usbTxData[ txWritePos ].AO1 << endl;
        txWritePos++;
        txFill++;
        if( txWritePos >= NUM_TX_BUFF ) {
          txWritePos = 0;
        }
      }
    } LeaveCriticalSection( &txLock );
    myfile.close();
  }

  void UsbReadData( TUsbRxData * data, unsigned int num, unsigned int * rdNum )
  {
    unsigned int i,j;
    TUsbRxData * pRxData = usbRxData;
    EnterCriticalSection( &rxLock );
    *rdNum = 0;
    if( rxFill == 0 )
    {
      LeaveCriticalSection( &rxLock );
      return;
    }

    if( num > (unsigned int)rxFill )
    {
      num = rxFill;
    }
    else
    {
      num = rxFill;
    }
    *rdNum = num;
    for( i = 0; i < num; i++ )
    {
      data[ i ] = usbRxData[ rxReadPos ];
      rxReadPos++;
      rxFill--;
      if( rxReadPos >= NUM_RX_BUFF )
      {
        rxReadPos = 0;
      }
    }
    LeaveCriticalSection( &rxLock );
  }


  void UsbPwmStart( void ) {
    EnterCriticalSection( &txCmdLock ); {
      if( devState.dllState == DEV_CONNECTED ) {
        pktOutCmd.cmd = CMD_START;
        ResetEvent( hEventTx );
        PostThreadMessage( m_dwTxThreadID, MSG_USB_SEND, sizeof( pktOutCmd.cmd ), 0 );
        WaitForSingleObjectEx( hEventTx, 5000, false );
      }
    } LeaveCriticalSection( &txCmdLock );
  }

  void UsbPwmStop( void ) {
    EnterCriticalSection( &txCmdLock ); {
      if( devState.dllState == DEV_CONNECTED ) {
        pktOutCmd.cmd = CMD_STOP;
        ResetEvent( hEventTx );
        PostThreadMessage( m_dwTxThreadID, MSG_USB_SEND, sizeof( pktOutCmd.cmd ), 0 );
        WaitForSingleObjectEx( hEventTx, 5000, false );
      }
    } LeaveCriticalSection( &txCmdLock );
  }

  void UsbResetBuffer( void )
  {
    EnterCriticalSection( &txLock );
    txWritePos = 0;
    txReadPos = 0;
    txFill = 0;
    txInProgress = 0;
    LeaveCriticalSection( &txLock );

    EnterCriticalSection( &rxLock );
    rxWritePos = 0;
    rxReadPos = 0;
    rxFill = 0;
    LeaveCriticalSection( &rxLock );

  }


  UINT32 UsbGetDevRxBuffFill( void )
  {
    return rxFill;
  }

  UINT32 UsbGetDevRxBuffSize( void )
  {
    return NUM_RX_BUFF;
  }

  UINT32 UsbGetDevTxBuffFill( void )
  {
    return txFill;
  }

  UINT32 UsbGetDevTxBuffSize( void )
  {
    return NUM_TX_BUFF;
  }




  int DAQ_ProcessTx()
  {
    // SEND DAQ DATA
    if( !tx && devState.dllState == DEV_CONNECTED && ( devState.appState == APP_STATE_STOPPED || devState.appState == APP_STATE_RUNNING ))
    {
      if( txInProgress )
      {
        //cout << "/n RX BUFF FILL: " << DAQ_DevRxBuffFill << " BUFF SIZE " << DAQ_DevRxBuffSize << endl;
        if(  DAQ_DevRxBuffFill > DAQ_DevRxBuffSize - 3 ) // if device DAQ buffer is almost full
        {
          txInProgress = 0;
          //stop TX--------------------------------------------------------------//
        }
        else // if device DAQ buffer is NOT almost full
        {
          if( txFill ) // if there's any data in DAQ TX buffer
          {
            // assign command properly
            pktOut.cmd = CMD_PWM_DATA;
            // copy PWM drive values
            for (int i = 0; i < 9; i++)
            {
              pktOut.devPWMData.pwmData[ i ] = usbTxData[ txReadPos ].Pwm[i];
            }
			// copy AOs
			pktOut.devPWMData.AO1 = usbTxData[ txReadPos ].AO1;
			pktOut.devPWMData.AO2 = usbTxData[ txReadPos ].AO2;
            // set packet ID
            pktOut.devPWMData.packetID = usbTxData[ txReadPos ].ID;
            // set correctness flag
            pktOut.devPWMData.pwmDataCorrect = 1;

            // if manual sliders are enabled and values need to be updated on uC
            if ( DAQ_ManualDrivesNeedUpdating )
            {
              // copy manual values
              for (int i = 0; i < 9; i++)
              {
                pktOut.devPWMData.pwmManuals[i] = DAQ_ManualDrivesValues[ i ];
                pktOut.devPWMData.pwmManualsEnableFlags[ i ] = DAQ_ManualDrivesEnabled [ i ];
                pktOut.devPWMData.pwmManualsCorrect = 1;
              }
              DAQ_ManualDrivesNeedUpdating = false;
            }

            WinUsb_WritePipe(MyWinUSBInterfaceHandle, 0x01, (PUCHAR)&pktOut, sizeof( pktOut.devPWMData ), &lnWr, NULL);

            if( lnWr == sizeof( pktOut.devPWMData ) ) // if the packet was sent successfully
            {
              txReadPos++;
              if( txReadPos >= NUM_TX_BUFF )
              {
                txReadPos = 0;
              }
              txFill--;
            }
            tx = 1;

            if( retCode != ERR_NONE )
            {
              devState.dllState = DEV_ERROR;
              devState.dllErrorCode = ERR_TX;
              return 1;
            }
          }
          else  // if there's no data in DAQ TX buffer
          {
            // if manual sliders are enabled and values need to be updated on uC
            if ( DAQ_ManualDrivesNeedUpdating )
            {
              // assign command properly
              pktOut.cmd = CMD_PWM_DATA;
              // set packet ID
              pktOut.devPWMData.packetID = 0;
              // set correctness flag
              pktOut.devPWMData.pwmDataCorrect = 0;
              // copy manual values
              for (int i = 0; i < 9; i++)
              {
                pktOut.devPWMData.pwmManuals[i] = DAQ_ManualDrivesValues[ i ];
                pktOut.devPWMData.pwmManualsEnableFlags[ i ] = DAQ_ManualDrivesEnabled [ i ];
                pktOut.devPWMData.pwmManualsCorrect = 1;
              }
              DAQ_ManualDrivesNeedUpdating = false;

              WinUsb_WritePipe(MyWinUSBInterfaceHandle, 0x01, (PUCHAR)&pktOut, sizeof( pktOut.devPWMData ), &lnWr, NULL);
            }
          }
        }
      }
      else // if not TX running
      {
        if( DAQ_DevTxBuffFill < (DAQ_DevTxBuffSize * 3) / 4 ) // if more than half of the device DAQ buffer is empty
        {
          txInProgress = 1;
          //start TX--------------------------------------------------------------//
        }
        // if manual sliders are enabled and values need to be updated on uC
        if ( DAQ_ManualDrivesNeedUpdating )
        {
          // assign command properly
          pktOut.cmd = CMD_PWM_DATA;
          // set packet ID
          pktOut.devPWMData.packetID = 0;
          // set correctness flag
          pktOut.devPWMData.pwmDataCorrect = 0;
          // copy manual values
          for (int i = 0; i < 9; i++)
          {
            pktOut.devPWMData.pwmManuals[i] = DAQ_ManualDrivesValues[ i ];
            pktOut.devPWMData.pwmManualsEnableFlags[ i ] = DAQ_ManualDrivesEnabled [ i ];
            pktOut.devPWMData.pwmManualsCorrect = 1;
          }
          DAQ_ManualDrivesNeedUpdating = false;

          WinUsb_WritePipe(MyWinUSBInterfaceHandle, 0x01, (PUCHAR)&pktOut, sizeof( pktOut.devPWMData ), &lnWr, NULL);
        }
      }
    }
    return 0;
  }

  int DAQ_ProcessRx()
  {
	  int i;
	        if( lnRd != sizeof( pktIn.devData ) )
            {
              return 1;
            }

            if( rxFill >= NUM_RX_BUFF )
            {
              devState.dllState = DEV_ERROR;
              devState.dllErrorCode = ERR_RX_BUFF_OVERFLOW;
              return 1;
            }

            //rcvServiced = 1;
            EnterCriticalSection( &rxLock );
            {
              //cout << pktIn.devData.responseToID << "  " << pktIn.devData.packetID << endl;
              usbRxData[ rxWritePos ].responseToID = pktIn.devData.responseToID;
              usbRxData[ rxWritePos ].ID = pktIn.devData.packetID;
              memcpy( ( void * ) usbRxData[ rxWritePos ].pressure, ( void * ) pktIn.devData.pressureSense, sizeof( pktIn.devData.pressureSense ) );
			  // Copy the currents to the more complex struct in accordance to the indices conversion
			  for (int i = 0; i < devConfig.currReadChannsCount; i++)
			  {
				  usbRxData[ rxWritePos ].current[devConfig.currReadChannsIndices[i]] = pktIn.devData.currentSense[i];
			  }
              //memcpy( ( void * ) usbRxData[ rxWritePos ].current, ( void * ) pktIn.devData.currentSense, sizeof( pktIn.devData.currentSense ) );

              rxFill++;
              rxWritePos++;
              if( rxWritePos >= NUM_RX_BUFF )
              {
                rxWritePos = 0;
              }
//#ifdef debug
              //cout << "pktId:" << pktIn.devData.packetID << endl;
              //dump << "pktId:" << pktIn.devData.packetID << endl;
              //cout << " press:" << endl;
              //dump << " press:" << endl;
              //for (int n = 0; n < 14; n++)
              //{
                //cout << pktIn.devData.pressureSense[n] << " ";
                //dump << pktIn.devData.pressureSense[n] << " ";
              //}
              //cout << endl << " curr: " << endl ;
              //dump << endl << " curr: " << endl ;
              //for (int n = 0; n < 9; n++)
              //{
              //  pktIn.devData.currentSense[n] = 512;
              //  cout << pktIn.devData.currentSense[n] << " ";
              //  //dump << pktIn.devData.currentSense[n] << " ";
              //}
              //cout << endl << endl;
              //dump << endl << endl;
//#endif
             //DAQ_DevTxBuffSize = pktIn.devData.DAQ_txBuffSize;
             DAQ_DevTxBuffFill = pktIn.devData.DAQ_txBuffFill;
             //DAQ_DevRxBuffSize = pktIn.devData.DAQ_rxBuffSize;
             DAQ_DevRxBuffFill = pktIn.devData.DAQ_rxBuffFill;
						 //cout << "DRX BF: " << DAQ_DevRxBuffFill << " BS " << DAQ_DevRxBuffSize;
						 //cout << "DTX BF: " << DAQ_DevTxBuffFill << " BS " << DAQ_DevTxBuffSize << " PID: " << pktIn.devData.packetID << "RXF:" << rxFill << endl;

              devState.appState = pktIn.devData.state;
              devState.appErrorCode = pktIn.devData.errorCode;
              devState.overCurrentPorts = pktIn.devData.overCurrentPorts;
            } LeaveCriticalSection( &rxLock );

	  return 0;
  }
}