#define __GearShiftCommLib_cpp

// GearShiftCommLib.cpp : Defines the exported functions for the DLL application.
//
#include <windows.h>
#include <setupapi.h>
#include <iostream>
#include <fstream>
#include <iomanip>
#include <Dbt.h>
#include <string>
#include <wctype.h>
#include <algorithm>
#include <vector>

#include <Setupapi.h>
#include <Winusb.h>
#include <strsafe.h>

#include "../Firmware/PIC24_firmware/USBPacket.h"

#include "GearShiftCommLib.h"
#include "GearShiftCommLibPriv.h"
#include "OBD_Functionality.h"
#include "CAN_Functionality.h"
#include "DAQ_Functionality.h"
#include "HW_UI_Functionality.h"
#include "Licensing.h"



#define MY_DEVICE_ID  "vid_04d8&pid_fd64"

//#define debug


using namespace std;
namespace GearShiftCommLib
{

#ifdef UNICODE
#define	Seeifdef	Unicode
#else
#define Seeifdef	Ansi
#endif

	//  Variables that need to have wide scope.



	// All global variables are declared in GearShiftCommLibPriv !!!!!



	int Init( void )
	{
		CAN_InitInternals();
		OBD_InitInternals();
		DAQ_InitInternals();

		MyDeviceHandle = INVALID_HANDLE_VALUE;
		txFill = 0;
		rxFill = 0;
		devState.appState = APP_STATE_DISCONNECTED;
		devState.dllState = DEV_DISCONNECTED;
		devState.dllErrorCode = ERR_NONE;
		devState.appErrorCode = APP_ERR_NONE;
		devConfig.pwmFreq = 1000;
		devConfig.currentADCMask = 0;

		InitializeCriticalSection( &txLock );
		InitializeCriticalSection( &txCmdLock );
		InitializeCriticalSection( &rxLock );

		thrInitialized = 0;
		thrTxInitialized = 0;
		thrRxInitialized = 0;
		hTimer = 0;

		initialized = 0;
		thrRetCode = 0;
		txInProgress = 0;
		stateWaitCounter = 0;
		rcvServiced = 0;
		rcvNonServicedCnt = 0;

		hThread = CreateThread( 0, 0, ThreadFunc, NULL, 0, &m_dwThreadID );
		hTxThread = CreateThread( 0, 0, TxThreadFunc, NULL, 0, &m_dwTxThreadID );
		hRxThread = CreateThread( 0, 0, rxThreadFunc, NULL, 0, &m_dwRxThreadID );

		if( !hThread || !hTxThread || !hRxThread )
		{
			return ERR_CREATE_THR;
		}

		hLib = NULL;

		hEvent = CreateEvent(NULL, TRUE, FALSE, (LPCWSTR)"hEvent");
		hEventVersion = CreateEvent(NULL, TRUE, FALSE, (LPCWSTR)"hEventVersion");
		hEventState = CreateEvent(NULL, TRUE, FALSE, (LPCWSTR)"hEventState");
		hEventConfig = CreateEvent(NULL, TRUE, FALSE, (LPCWSTR)"hEventConfig");
		hEventTx = CreateEvent(NULL, TRUE, FALSE, (LPCWSTR)"hEventTx");
		EnterBootloaderCmdRespReceived = CreateEvent(NULL, TRUE, FALSE, (LPCWSTR)"EnterBootloaderCmdRespReceived");
		GetSerialCmdRespReceived = CreateEvent(NULL, TRUE, FALSE, (LPCWSTR)"GetSerialCmdRespReceived");

		SetEvent( hEvent );
		SetEvent( hEventVersion );
		SetEvent( hEventState );
		SetEvent( hEventConfig );
		SetEvent( hEventTx );
		SetEvent( EnterBootloaderCmdRespReceived );
		SetEvent( GetSerialCmdRespReceived );

		if( !( hEvent && hEventVersion && hEventState && hEventConfig && hEventTx ) )
		{
			cout << "ERR_CREATE_THR\n";
			return ERR_CREATE_THR;
		}
		initialized = 1;
		return ERR_NONE;

	}

	int DeInit( void )
	{
		TerminateThread( hThread, ERR_NONE );
		TerminateThread( hTxThread, ERR_NONE );
		TerminateThread( hRxThread, ERR_NONE );
		CloseHandle( hThread );
		CloseHandle( hTxThread );
		CloseHandle( hRxThread );
		UsbClose();
		return 0;
	}


	DWORD WINAPI TxThreadFunc( LPVOID pvParam )
	{

		unsigned int k = 0;
		unsigned int counter = 0;

		TDataPacket *pPkt;
		TDeviceState *pDevState = &devState;

		pPkt = &pktIn;

#ifdef debug
		cout << "txThreadFunc();" << endl;
		//dump << "txThreadFunc();" << endl;
#endif
		tx = 0;

		thrTxInitialized = 1;
		while( 1 )
		{
			//cout << "DUPNY CHU!!!! " << (int)devState.dllState << endl ;
			msgReturn = PeekMessage( &msg, NULL, MSG_USB_SEND, MSG_USB_SEND, 1 );

			if( devState.dllState == DEV_ERROR ||
				devState.dllState == DEV_DISCONNECTED ||
				//devState.dllState == DEV_CONNECTING_PHASE_1 ||
				devState.dllState == DEV_DISCONNECTING )
			{
				Sleep( 10 );
				continue;
			}

			EnterCriticalSection( &txLock );
			{

				if( msgReturn && msg.message == MSG_USB_SEND )
				{
					retCode = UsbWrite( &pktOutCmd, msg.wParam, 10 );
					tx = 1;
					if( retCode != ERR_NONE )
					{
						devState.dllState = DEV_ERROR;
						devState.dllErrorCode = ERR_TX;
					}
					SetEvent( hEventTx );
				}
				else
				{
#pragma region PLACE_TO_SEND_DATA_PACKET

					// Send CAN data
					if ( CAN_ProcessTx() )
					{
						// If function returns an error, quit this function
						return 0;
					}

					// Send DAQ data
					if ( DAQ_ProcessTx() )
					{
						// If function returns an error, quit this function
						return 0;
					}

					// Send OBD data
					if ( OBD_ProcessTx() )
					{
						// If function returns an error, quit this function
						return 0;
					}

#pragma endregion PLACE_TO_SEND_DATA_PACKET
				}
				// if all buffers were empty, send poll data
				if( !tx )
				{
					pktOut.cmd = CMD_POLL_DATA;
					//MPUSBWrite( outPipe, ( void * ) &pktOut, sizeof( pktOut.cmd ), &lnWr, 10 );
					WinUsb_WritePipe(MyWinUSBInterfaceHandle, 0x01, (PUCHAR)&pktOut, sizeof( pktOut.cmd ), &lnWr, NULL);
				}
				tx = 0;
			} LeaveCriticalSection( &txLock );


			WinUsb_ReadPipe(MyWinUSBInterfaceHandle, 0x81, (PUCHAR)&pktIn, 64, &lnRd, NULL);
			//cout << "WTF2\n";
			//cout << 'h';

			if( lnRd ) // if there was anything read
			{
				switch( pktIn.cmd )
				{

				case CMD_CAN_DATA:
					{
						//continue;
						if( lnRd != sizeof( pktIn.CANDataPacket ) )
						{
							continue;
						}
						rcvServiced = 1;
						//assign davice buffer usage data
						//CAN_DevTxBuffSize = pktIn.CANDataPacket.txBuffSize;
						CAN_DevTxBuffFill = pktIn.CANDataPacket.txBuffFill;
						//CAN_DevRxBuffSize = pktIn.CANDataPacket.rxBuffSize;
						CAN_DevRxBuffFill = pktIn.CANDataPacket.rxBuffFill;

						int msgsToSvc = pktIn.CANDataPacket.msgCount;
						//cout << "RCVD: " << (int)msgsToSvc << endl;
						for (int i = 0; i < msgsToSvc; i++)
						{
							TUsbCANData msg;

							//             cout << " SIDH: " << (int)pktIn.CANDataPacket.msgs[i].data.SIDH << " SIDL: " << (int)pktIn.CANDataPacket.msgs[i].data.SIDL
							//               << " EIDH: " << (int)pktIn.CANDataPacket.msgs[i].data.EID8 << " EIDL: " << (int)pktIn.CANDataPacket.msgs[i].data.EID0 << endl;

							msg.DLC = pktIn.CANDataPacket.msgs[i].data.DLC & 0x0F; // & 00001111 - last four bits of DLC byte denote the actual DLC, more significant bits are some flags
							if (pktIn.CANDataPacket.msgs[i].data.DLC & 0x40) // &01000000 - 7th bit in DLC byte flags the remote request frame
							{
								msg.isRTRFrame = 1;
							}
							else
							{
								msg.isRTRFrame = 0;
							}
							msg.timestamp = pktIn.CANDataPacket.msgs[i].data.timestamp;
							if ( pktIn.CANDataPacket.msgs[i].data.SIDL & 0x08) // if this is extended frame
							{
								//assign properly the ID ( see datasheet to understand bitshifts )
								ULONG32 ID = ( (byte)(pktIn.CANDataPacket.msgs[i].data.SIDH) ) << 21;
								ID += ( pktIn.CANDataPacket.msgs[i].data.SIDL & 0xE0 ) << 13;
								ID += ( pktIn.CANDataPacket.msgs[i].data.SIDL & 0x03 ) << 16;
								ID += pktIn.CANDataPacket.msgs[i].data.EID8 << 8;
								ID += pktIn.CANDataPacket.msgs[i].data.EID0;
								//cout << "ID: " << ID << endl;
								msg.isXtdFrameType = true;
								msg.remoteID = ID;
							}
							else // if this is standard frame
							{
								ULONG32 ID = pktIn.CANDataPacket.msgs[i].data.SIDH << 3;
								ID += pktIn.CANDataPacket.msgs[i].data.SIDL >> 5;
								msg.isXtdFrameType = false;
								msg.remoteID = ID;
								//cout << "ID: " << ID << endl;
							}
							for (int j = 0; j < 8; j++)
							{
								if ( j < msg.DLC)
									msg.data[j] = pktIn.CANDataPacket.msgs[i].data.data[j];
								else
									msg.data[j] = 0;
							}
							//                          cout << " SIDH: " << (int)pktIn.CANDataPacket.msgs[i].data.SIDH << " SIDL: " << (int)pktIn.CANDataPacket.msgs[i].data.SIDL
							//                            << " EIDH: " << (int)pktIn.CANDataPacket.msgs[i].data.EID8 << " EIDL: " << (int)pktIn.CANDataPacket.msgs[i].data.EID0 << endl;
							//                           for (int j = 0; j < msg.DLC; j++)
							//                           {
							//                             msg.data[j] = pktIn.CANDataPacket.msgs[i].data.data[j];
							//                           }
							//                           cout << "LIB: " << endl;
							//                           cout << "ID: " << msg.remoteID;
							//                           if (msg.isXtdFrameType)
							//                             cout << " XTD ";
							//                           else
							//                             cout << " STD ";
							//                           cout << "DLC: " << (int)msg.DLC;
							//
							//                           cout << endl;

							// Check whether the rolling trace mode is enabled, if so, each received message must be put into the software buffer
							//if (CAN_Sniffer_RollingTraceModeEnabled)
							{
								CAN_RxBuffer.PutData(msg);
							}
						}



						//SetEvent( hEventVersion );
						rcvServiced = 1;
						break;
					}


				case CMD_GET_STATE:
					{
						if( lnRd != sizeof( pktIn.devState ) )
						{
							continue;
						}
						//cout << "CMD GET STATE RCVD " << (int)pktIn.devState.state << "!!!!!!!!!\n";
						rcvServiced = 1;
						stateWaitCounter = 0;
						devState.appState = pktIn.devState.state;
						devState.appErrorCode = pktIn.devState.errorCode;
						devState.overCurrentPorts = pktIn.devState.overCurrentPorts;

						DAQ_DevTxBuffFill = pktIn.devState.DAQ_txBuffFill;
						DAQ_DevRxBuffFill = pktIn.devState.DAQ_rxBuffFill;
						CAN_DevTxBuffFill = pktIn.devState.CAN_txBuffFill;
						CAN_DevRxBuffFill = pktIn.devState.CAN_rxBuffFill;
						OBD_DevTxBuffFill = pktIn.devState.OBD_txBuffFill;
						OBD_DevRxBuffFill = pktIn.devState.OBD_rxBuffFill;
						//cout << OBD_DevTxBuffFill << endl;

						SetEvent( hEventState );

						if( devState.appState == APP_STATE_ERROR )
						{
							devState.dllState = DEV_ERROR;
							switch( devState.appErrorCode )
							{
							case APP_ERR_OVER_CURRENT:
								devState.dllErrorCode = ERR_OVERCURRENT;
								break;

							case APP_ERR_BUFFER_OVERFLOW:
								devState.dllErrorCode = ERR_BUFFER_OVERFLOW;
								break;

							default:
								devState.dllErrorCode = ERR_TRANSMISSION_FAILURE;
							}

						}
						rcvServiced = 1;
						break;
					}

				case CMD_OBD_DATA:
					{
						if( lnRd != sizeof( pktIn.OBDDataPacket ) )
						{
							continue;
						}
						rcvServiced = 1;
						//assign davice buffer usage data
						OBD_DevTxBuffSize = pktIn.OBDDataPacket.txBuffSize;
						OBD_DevTxBuffFill = pktIn.OBDDataPacket.txBuffFill;
						OBD_DevRxBuffSize = pktIn.OBDDataPacket.rxBuffSize;
						OBD_DevRxBuffFill = pktIn.OBDDataPacket.rxBuffFill;
						//            cout << "TXBF:                       " << OBD_DevTxBuffFill << endl;
						//            cout << "RXBF:                       " << OBD_DevRxBuffFill << endl;

						int msgsToSvc = pktIn.OBDDataPacket.charCount;
						SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), 14);
						//cout << "                           count: " << msgsToSvc << endl << endl;
						//dump.open("c:\\dumppp.txt", ios::out|ios::app);
						//dump << "\n\nMESSAGE DLC: " << msgsToSvc << " ->\r\n";
						for (int i = 0; i < 50; i++)
						{
							if (i == msgsToSvc)
							{
								//dump << "!@!@";
							}
							//if ( (char)pktIn.OBDDataPacket.chars[i] != '\r' )
							{
								//dump << (char)pktIn.OBDDataPacket.chars[i];// << ' ';
							}
						}
						//cout << "msgsToSvc: " << msgsToSvc << endl;
						for (int i = 0; i < msgsToSvc; i++)
						{
							// copy to temp buffer all received characters but carriage returns
							//               if ( (char)pktIn.OBDDataPacket.chars[i] == '>' )
							//               {
							//                 cout << "\n\nEOC BIJACZ!!!\n\n";
							//               }

							if ( (char)pktIn.OBDDataPacket.chars[i] != '\r' )
							{
								cout << (int)pktIn.OBDDataPacket.chars[i];
								if ( (int)pktIn.OBDDataPacket.chars[i] > 32 )
								{
									cout << " " << (char)pktIn.OBDDataPacket.chars[i] << endl;
								}
								OBD_RxTmpBuffer.PutData((char)pktIn.OBDDataPacket.chars[i]);
							}
							// cout << (int)pktIn.OBDDataPacket.chars[i] << ' ';
						}
						//dump.close();
						//cout << "\r\n";
						//              for (int i = 0; i < msgsToSvc; i++)
						//              {
						//                if ( (char)pktIn.OBDDataPacket.chars[i] == '\r' )
						//                {
						//                  cout << 'r';
						//                }
						//                else
						//                {
						//                  if ( (char)pktIn.OBDDataPacket.chars[i] == '\n' )
						//                  {
						//                    cout << 'n';
						//                  }
						//                  else
						//                  {
						//                    cout << (char)pktIn.OBDDataPacket.chars[i];
						//                  }
						//                }
						//              }
						//cout << endl << "\rcount: " << msgsToSvc << endl ;
						//cout << endl ;
						SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), 15);

						UsbOBDProcessTmpRxBuffer();
						//SetEvent( hEventVersion );
						rcvServiced = 1;
						break;
					}



				case CMD_DEV_DATA:

					{
						if (DAQ_ProcessRx() == 0) rcvServiced = 1;
						break;
					}

				case CMD_GET_VERSION:

					{
						if( lnRd != sizeof( pktIn.fwVersion ) )
						{
							continue;
						}
						rcvServiced = 1;
						fwVersion.major = pktIn.fwVersion.major;
						fwVersion.minor = pktIn.fwVersion.minor;
						SetEvent( hEventVersion );
						rcvServiced = 1;
						break;
					}

				case CMD_GET_CONFIG:

					{
						cout << "\n\n\n\n\n\n\n\n\n\n\n\n\n\n CONFIG!!!!\n\n\n";
						if( lnRd != sizeof( pktIn.devConfig ) )
						{
							continue;
						}
						rcvServiced = 1;

						DAQ_DevTxBuffSize = pktIn.devConfig.DAQ_txBuffSize;
						DAQ_DevRxBuffSize = pktIn.devConfig.DAQ_rxBuffSize;
						CAN_DevTxBuffSize = pktIn.devConfig.CAN_txBuffSize;
						CAN_DevRxBuffSize = pktIn.devConfig.CAN_rxBuffSize;
						OBD_DevTxBuffSize = pktIn.devConfig.OBD_txBuffSize;
						OBD_DevRxBuffSize = pktIn.devConfig.OBD_rxBuffSize;

						devConfig.numCurrSense = pktIn.devConfig.currentSensCount;
						devConfig.numPressSense = pktIn.devConfig.pressureSensCount;
						devConfig.numPwmDrv = pktIn.devConfig.pwmDrvCount;
						devConfig.pwmFreq = pktIn.devConfig.frequency;
						SetEvent( hEventConfig );
						rcvServiced = 1;
						break;
					}

				case CMD_OBD_ENABLE_COMM:
					{
						SetEvent(OBD_Event_ELM_BFRS_INIT_COMPLETE);
						rcvServiced = 1;
						break;
					}

				case CMD_CAN_SET_CONFIG:
					{
						SetEvent(CAN_Event_SetConfigCmd_Complete);
						rcvServiced = 1;
						break;
					}

				case CMD_CAN_DISABLE_COMM:
					{
						SetEvent(CAN_Event_CommDisableCmd_Complete);
						rcvServiced = 1;
						break;
					}

				case CMD_CAN_EN_PULLUP:
					{
						SetEvent(CAN_Event_PullUpEnabledRespRcvd);
						rcvServiced = 1;
						break;
					}

				case CMD_CAN_DSBL_PULLUP:
					{
						SetEvent(CAN_Event_PullUpDisabledRespRcvd);
						rcvServiced = 1;
						break;
					}

				case CMD_CAN_RST_FXD_TRC:
					{
						SetEvent(CAN_Event_RstFxdTrcRespRcvd);
						rcvServiced = 1;
						break;
					}

				case CMD_BLD_ENTER:
					{
						SetEvent(EnterBootloaderCmdRespReceived);
						rcvServiced = 1;
						break;
					}

				case CMD_GET_SERIAL:
					{
						char serial[15];
						char GUID[37];
						for (int j = 0; j < 15; j++)
						{
							serial[j] = pktIn.serialInfo.serialString[j];
							cout << (char)serial[j];
						}
						serial[14] = '\0';
						for (int j = 0; j < 37; j++)
						{
							GUID[j] = pktIn.serialInfo.GUIDString[j];
							cout << (char)GUID[j];
						}
						GUID[36] = '\0';

						//cout << endl << endl << "SERIAL: " << serial << "||";
						//cout << endl << endl << "GUID: " << GUID << "||" << endl;
						SetEvent(GetSerialCmdRespReceived);
						rcvServiced = 1;
						break;
					}

				default:
					{
						break;
					}
				}
			}

			if( !rcvServiced )
			{
				rcvNonServicedCnt++;
				if( (rcvNonServicedCnt >= MAX_RCV_NON_SERVICED_CNT))//  &&  (devState.dllState != DEV_CONNECTED) )
				{
					devState.dllState = DEV_ERROR;
					devState.dllErrorCode = ERR_DEVICE_NOT_RESPONDING;
				}
			}
			else
			{
				rcvServiced = 0;
				rcvNonServicedCnt = 0;
			}
		}
	}

	DWORD WINAPI rxThreadFunc( LPVOID pvParam )
	{
		thrRxInitialized = 1;
#ifdef debug
		cout << "rxThreadFunc();" << endl;
#endif
		while( 1 )
		{
			Sleep( 10000 );
		}
	}


	DWORD WINAPI ThreadFunc(LPVOID pvParam)
	{
		int bRet;
		MSG msg;
		WNDCLASS wnd;

		DWORD exCode = 0;

		hinst = GetModuleHandle( NULL );

		ZeroMemory( &wnd, sizeof( WNDCLASS ) );

		//dbgdmp.open("c:\\dbgdmp.txt", ios::app);


		wnd.hInstance = hinst;
		wnd.lpfnWndProc = MainWndProc;
		wnd.hIcon = (HICON) LoadImage( hinst, MAKEINTRESOURCE(5), IMAGE_ICON, GetSystemMetrics(SM_CXSMICON), GetSystemMetrics(SM_CYSMICON), LR_DEFAULTCOLOR );
		wnd.cbClsExtra = 0;
		wnd.cbWndExtra = 0;
		wnd.hCursor = LoadCursor( NULL, IDC_ARROW );
		wnd.lpszClassName = TEXT( "MainWClass" );
		wnd.lpszMenuName = TEXT( "MainMenu" );
		wnd.style = CS_HREDRAW | CS_VREDRAW;
		wnd.hbrBackground = (HBRUSH) GetStockObject(WHITE_BRUSH);


		bRet = RegisterClass( & wnd );
		if( bRet == 0 )
		{
			return ERR_REG_WND_CLASS;
		}

		hWnd = CreateWindow( TEXT( "MainWClass" ), TEXT( "Sample" ), WS_OVERLAPPEDWINDOW, 0, 0, CW_USEDEFAULT, CW_USEDEFAULT, (HWND) NULL, (HMENU) NULL, hinst, (LPVOID) NULL );
		if( hWnd == NULL )
		{
			return ERR_CREATE_WINDOW;
		}

		GUID interfaceClassGuid = {0xa5dcbf10, 0x6530, 0x11d2, 0x90, 0x1F, 0x00, 0xC0, 0x4F, 0xB9, 0x51, 0xED}; //Globally Unique Identifier (GUID) for USB peripheral devices
		DEV_BROADCAST_DEVICEINTERFACE myDeviceBroadcastHeader;

		myDeviceBroadcastHeader.dbcc_devicetype = DBT_DEVTYP_DEVICEINTERFACE;
		myDeviceBroadcastHeader.dbcc_size = sizeof( DEV_BROADCAST_DEVICEINTERFACE );
		myDeviceBroadcastHeader.dbcc_reserved = 0;
		myDeviceBroadcastHeader.dbcc_classguid = interfaceClassGuid;
		bRet = ( int ) RegisterDeviceNotification( hWnd, &myDeviceBroadcastHeader, DEVICE_NOTIFY_WINDOW_HANDLE );

		if( bRet == NULL )
		{
			return ERR_REG_DEV_NOTIFY;
		}

		thrInitialized = 1;


		while( ( bRet = GetMessage( &msg, NULL, 0, 0 ) ) != 0 )
		{
			if( bRet == -1 )
			{
				cout << "Error !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!" << endl;
				return ERR_GET_MESSAGE;
			}
			else
			{
				TranslateMessage( &msg );
				DispatchMessage( &msg );
			}
		}

		return ERR_NONE;
	}

	LRESULT CALLBACK MainWndProc( HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam )
	{
		int retCode;

		int devInitCounter = NUM_INIT_REP;

		switch( msg )
		{
		case WM_DEVICECHANGE:
			{
				// DEVICE PRESENCE CHECK TO BE DONE HERE
				cout << "WMDEVICECHANGE!!!!!!!!!!!!!\n\n\n\n\n";
				break;
			}

		case WM_TIMER:
			{
				break;
			}

		case MSG_USB_CONNECT:
			{
				break;
			}

		case MSG_USB_DISCONNECT:
			{
				break;
			}

		case WM_QUIT:
			{
				UsbClose();
				devState.dllState = DEV_DISCONNECTED;
				break;
			}
		default:
			{
				return( DefWindowProc( hWnd, msg, wParam, lParam ) );
				break;
			}
		}
		return 0;
	}


	int UsbOpen( void )
	{
		if ( WinUsbConnect() )
		{
			// if not connected
			cout << "ERR_USB_OPEN\n";
			return ERR_USB_OPEN;
		}
		else
		{
			// if connected properly
			//UsbObdElm327BfrsInit();
			cout << "USB_OPEN_OK\n";
			return ERR_NONE;
		}
	}

	void UsbClose( void )
	{
		EnterCriticalSection( &txCmdLock );
		{
			EnterCriticalSection( &rxLock );
			{
				EnterCriticalSection( &txLock );
				{
					KillTimer( hWnd, hTimer );
					WinUsbDisconnect();
					hTimer = 0;
					devState.dllState = APP_STATE_DISCONNECTED;
					devState.dllErrorCode = APP_ERR_NONE;
				}
				LeaveCriticalSection( &txLock );
			}
			LeaveCriticalSection( &rxLock );
		}
		LeaveCriticalSection( &txCmdLock );
	}

	int UsbWrite( volatile TDataPacket * pkt, int size, unsigned int timeout )
	{
		int cnt = NUM_TX_REP;
		DWORD lnWr = 0;

		while( size != lnWr ) {
			if( cnt == 0 ) {
				return ERR_TX;
			}
			cnt--;
			EnterCriticalSection( &txLock );
			{
				WinUsb_WritePipe(MyWinUSBInterfaceHandle, 0x01, (PUCHAR)pkt, size, &lnWr, NULL);
			} LeaveCriticalSection( &txLock );
		}

		return ERR_NONE;
	}

	int UsbRead( volatile TDataPacket * pkt, int size, unsigned int timeout )
	{
		DWORD lnRd = 0;
		int cnt = NUM_RX_REP;

		while( lnRd != size )
		{
			WinUsb_ReadPipe(MyWinUSBInterfaceHandle, 0x81, (PUCHAR)pkt, size, &lnRd, NULL);
			if( cnt == 0 && size != lnRd ) {
				return ERR_RX;
			}
			cnt--;
		}
		return ERR_NONE;
	}


	int UsbConnect( void )
	{
		int retCode;
		DWORD exCode;

		// Wait until the parallel threads are initialized
		while( !(thrInitialized && thrTxInitialized && thrRxInitialized ) )
		{
			retCode = GetExitCodeThread( hThread, &exCode );

			if( retCode && exCode != STILL_ACTIVE )
			{
				return 0x1001;//exCode;
			}

			Sleep( 100 );
		}

		// If the funtion is called when device is connected or being connected to, exit the function with and error
		//if( devState.dllState > DEV_DISCONNECTED )
		//{
		//  return 0x1002;//ERR_ALLREADY_CONNECTED;
		//}

		// Stop application internals
		rxReadPos = 0;
		rxWritePos = 0;
		txReadPos = 0;
		txWritePos = 0;
		EnterCriticalSection( &txCmdLock );
		{
			EnterCriticalSection( &rxLock );
			{
				KillTimer( hWnd, hTimer );
				hTimer = 0;
				devState.dllErrorCode = APP_ERR_NONE;
			} LeaveCriticalSection( &rxLock );
		} LeaveCriticalSection( &txCmdLock );

		// Reset flags
		txInProgress = 0;
		stateWaitCounter = 0;
		rcvServiced = 0;
		rcvNonServicedCnt = 0;

		// Try to open the USB device communication channel
		devState.dllState = DEV_OPENING_USB;
		retCode = UsbOpen();
		if( retCode != ERR_NONE )
		{
			UsbClose();
			devState.dllState = DEV_ERROR;
			devState.dllErrorCode = retCode;
			return 0x1003;
		}

		// Try to get the hardware info from the device
		devState.dllState = DEV_GETTING_HW_INFO;
		int devInitCounter = NUM_INIT_REP;
		// Try the defined number of times to get the device talking
		while( devInitCounter )
		{

			devInitCounter--;
			if( !devInitCounter )
			{
				// If number of trials is exceeded, return with an error
				UsbClose();
				devState.dllState = DEV_ERROR;
				devState.dllErrorCode = ERR_DEVICE_INIT;
				return 0x1004;
			}
			EnterCriticalSection( &txCmdLock );
			{
				//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
				pktOutCmd.cmd = CMD_GET_VERSION;
				//pktOutCmd.cmd = CMD_BLD_ENTER;
				ResetEvent( hEventTx );
				PostThreadMessage( m_dwTxThreadID, MSG_USB_SEND, sizeof( pktOutCmd.cmd ), 0 );
				WaitForSingleObjectEx( hEventTx, 200, false );
			} LeaveCriticalSection( &txCmdLock );

			if ( 0!= WaitForSingleObject(hEventTx, DEV_INIT_MSG_TIMEOUT_MS ) )
			{
				cout << " INIT: FAILED TO GET DEVICE VERSION\n";
				UsbClose();
				return 0x1005;
			}
			devInitCounter = 0;
		}

		// Try to send the init message twice
		EnterCriticalSection( &txCmdLock );
		{
			ResetEvent( hEventConfig );
			pktOutCmd.cmd = CMD_GET_CONFIG;
			ResetEvent( hEventTx );
			PostThreadMessage( m_dwTxThreadID, MSG_USB_SEND, sizeof( pktOutCmd.cmd ), 0 );
			WaitForSingleObjectEx( hEventTx, 200, false );
		} LeaveCriticalSection( &txCmdLock );

		bool retryInit = false;
		if( 0 != WaitForSingleObject( hEventConfig, DEV_INIT_MSG_TIMEOUT_MS ) )
		{
			retryInit = true;
		}

		if (retryInit)
		{
			EnterCriticalSection( &txCmdLock );
			{
				ResetEvent( hEventConfig );
				pktOutCmd.cmd = CMD_GET_CONFIG;
				ResetEvent( hEventTx );
				PostThreadMessage( m_dwTxThreadID, MSG_USB_SEND, sizeof( pktOutCmd.cmd ), 0 );
				WaitForSingleObjectEx( hEventTx, 200, false );
			} LeaveCriticalSection( &txCmdLock );
			if( 0 != WaitForSingleObject( hEventConfig, DEV_INIT_MSG_TIMEOUT_MS ) )
			{
				cout << " INIT: FAILED TO GET DEVICE CONFIG\n";
				UsbClose();
				devState.dllState = DEV_ERROR;
				devState.dllErrorCode = ERR_DEVICE_INIT;
				return 0x1006;
			}
		}

		//while(1);
		// If successfully received the device version..
		// Get the device state
		EnterCriticalSection( &txCmdLock );
		{
			ResetEvent( hEventState );
			pktOutCmd.cmd = CMD_GET_STATE;
			ResetEvent( hEventTx );
			PostThreadMessage( m_dwTxThreadID, MSG_USB_SEND, sizeof( pktOutCmd.cmd ), 0 );
			WaitForSingleObjectEx( hEventTx, 200, false );
		} LeaveCriticalSection( &txCmdLock );

		if( 0 != WaitForSingleObject( hEventState, DEV_INIT_MSG_TIMEOUT_MS ) )
		{
			cout << " INIT: FAILED TO GET DEVICE STATE\n";
			UsbClose();
			devState.dllState = DEV_ERROR;
			devState.dllErrorCode = ERR_DEVICE_INIT;
			return 0x1007;
		}


		if( devState.appState != APP_STATE_STOPPED )
		{
			cout << "INIT: APP STATE ERROR\n";
			devState.dllState = DEV_ERROR;
			devState.dllErrorCode = ERR_DEVICE_INIT;
			return 1;
		}
		hTimer = 0;

		cout << "CONNECTED!!\n";
		devState.dllState = DEV_CONNECTED;
		thrRetCode = ERR_NONE;
		//----------------------------------------------------------------------

		return 0;//ERR_NONE;
	}

	int UsbDisconnect( void )
	{
		if( devState.dllState != DEV_DISCONNECTED )
		{
			devState.dllState = DEV_DISCONNECTING;
			devState.appState = APP_STATE_DISCONNECTED;
			UsbClose();
			devState.dllState = DEV_DISCONNECTED;
			devState.dllErrorCode = ERR_NONE;
		}
		return 0;
	}

	void UsbGetDeviceState( unsigned int* state, unsigned int* errorCode, unsigned int* overCurrentPorts )
	{
		EnterCriticalSection( &rxLock );
		*state = devState.dllState;
		*errorCode = devState.dllErrorCode;
		*overCurrentPorts = devState.overCurrentPorts;
		LeaveCriticalSection( &rxLock );
	}


	int UsbObdElm327BfrsInit( void )
	{
		if( devState.dllState != DEV_CONNECTED )
		{
			return 1; // return error
		}
		ResetEvent(OBD_Event_ELM_BFRS_INIT_COMPLETE);
		EnterCriticalSection( &txCmdLock );
		{
			if( devState.dllState == DEV_CONNECTED )
			{
				pktOutCmd.cmd = CMD_OBD_ENABLE_COMM;
				ResetEvent( hEventTx );
				PostThreadMessage( m_dwTxThreadID, MSG_USB_SEND, sizeof( unsigned char ), 0 );
				WaitForSingleObjectEx( hEventTx, 5000, false );
			}
		} LeaveCriticalSection( &txCmdLock );

		if ( 0!= WaitForSingleObject(OBD_Event_ELM_BFRS_INIT_COMPLETE, OBD_INIT_MSG_TIMEOUT_MS ) )
		{
			cout << " BUFFERS FAILED TO INITIALIZE\n\n";
			return 1;
		}
		else
		{
			cout << " BUFFERS INITIALIZED\n\n";
			return 0;
		}
	}


	int UsbEnterBootloader()
	{
		if( devState.dllState != DEV_CONNECTED )
		{
			return 1; // return error
		}
		ResetEvent(EnterBootloaderCmdRespReceived);
		EnterCriticalSection( &txCmdLock );
		{
			if( devState.dllState == DEV_CONNECTED )
			{
				pktOutCmd.cmd = CMD_BLD_ENTER;
				ResetEvent( hEventTx );
				PostThreadMessage( m_dwTxThreadID, MSG_USB_SEND, sizeof( unsigned char ), 0 );
				WaitForSingleObjectEx( hEventTx, 5000, false );
			}
		} LeaveCriticalSection( &txCmdLock );

		if ( 0!= WaitForSingleObject(EnterBootloaderCmdRespReceived, 3000 ) )
		{
			cout << " NO RESPONSE FOR ENTER BL CMD\n\n";
			return 1;
		}
		else
		{
			cout << " RESPONSE FOR ENTER BL CMD\n\n";
			return 0;
		}
	}

	void WinUsbDisconnect()
	{
		CloseHandle(MyDeviceHandle);
	}

	int WinUsbConnect()
	{
		GUID InterfaceClassGuid = {0x58D07210, 0x28A2, 0x11DD, 0xBD, 0x0B, 0x11, 0x00, 0x20, 0xAA, 0x8A, 0x66};

		HDEVINFO DeviceInfoTable = INVALID_HANDLE_VALUE;
		PSP_DEVICE_INTERFACE_DATA InterfaceDataStructure = new SP_DEVICE_INTERFACE_DATA;
		PSP_DEVICE_INTERFACE_DETAIL_DATA DetailedInterfaceDataStructure = new SP_DEVICE_INTERFACE_DETAIL_DATA;
		SP_DEVINFO_DATA DevInfoData;

		DWORD InterfaceIndex = 0;
		DWORD StatusLastError = 0;
		DWORD dwRegType;
		DWORD dwRegSize;
		DWORD StructureSize = 0;
		PBYTE PropertyValueBuffer;
		bool MatchFound = false;
		DWORD ErrorStatus;
		BOOL BoolStatus = FALSE;

		//First populate a list of plugged in devices (by specifying "DIGCF_PRESENT"), which are of the specified class GUID.
		DeviceInfoTable = SetupDiGetClassDevs(&InterfaceClassGuid, NULL, NULL, DIGCF_PRESENT | DIGCF_DEVICEINTERFACE);

		//Now look through the list we just populated.  We are trying to see if any of them match our device.
		while(true)
		{
			InterfaceDataStructure->cbSize = sizeof(SP_DEVICE_INTERFACE_DATA);
			if(SetupDiEnumDeviceInterfaces(DeviceInfoTable, NULL, &InterfaceClassGuid, InterfaceIndex, InterfaceDataStructure))
			{
				ErrorStatus = GetLastError();
				if(ERROR_NO_MORE_ITEMS == ErrorStatus)	//Did we reach the end of the list of matching devices in the DeviceInfoTable?
				{	//Cound not find the device.  Must not have been attached.
					SetupDiDestroyDeviceInfoList(DeviceInfoTable);	//Clean up the old structure we no longer need.
					return 1;
				}
			}
			else	//Else some other kind of unknown error ocurred...
			{
				ErrorStatus = GetLastError();
				SetupDiDestroyDeviceInfoList(DeviceInfoTable);	//Clean up the old structure we no longer need.
				return 1;
			}

			//Now retrieve the hardware ID from the registry.  The hardware ID contains the VID and PID, which we will then
			//check to see if it is the correct device or not.

			//Initialize an appropriate SP_DEVINFO_DATA structure.  We need this structure for SetupDiGetDeviceRegistryProperty().
			DevInfoData.cbSize = sizeof(SP_DEVINFO_DATA);
			SetupDiEnumDeviceInfo(DeviceInfoTable, InterfaceIndex, &DevInfoData);

			//First query for the size of the hardware ID, so we can know how big a buffer to allocate for the data.
			SetupDiGetDeviceRegistryProperty(DeviceInfoTable, &DevInfoData, SPDRP_HARDWAREID, &dwRegType, NULL, 0, &dwRegSize);

			//Allocate a buffer for the hardware ID.
			PropertyValueBuffer = (BYTE *) malloc (dwRegSize);
			if(PropertyValueBuffer == NULL)	//if null, error, couldn't allocate enough memory
			{	//Can't really recover from this situation, just exit instead.
				SetupDiDestroyDeviceInfoList(DeviceInfoTable);	//Clean up the old structure we no longer need.
				return 1;
			}

			//Retrieve the hardware IDs for the current device we are looking at.  PropertyValueBuffer gets filled with a
			//REG_MULTI_SZ (array of null terminated strings).  To find a device, we only care about the very first string in the
			//buffer, which will be the "device ID".  The device ID is a string which contains the VID and PID, in the example
			//format "Vid_04d8&Pid_003f".
			SetupDiGetDeviceRegistryProperty(DeviceInfoTable, &DevInfoData, SPDRP_HARDWAREID, &dwRegType, PropertyValueBuffer, dwRegSize, NULL);

			//Now check if the first string in the hardware ID matches the device ID of my USB device.
#ifdef UNICODE
			wstring DeviceIDFromRegistry((wchar_t *)PropertyValueBuffer);
			string DevIdToFindA(MY_DEVICE_ID);
			wstring DevIdToFind(DevIdToFindA.size(),  L' ');
			std::copy(DevIdToFindA.begin(), DevIdToFindA.end(), DevIdToFind.begin());
			std::transform(DeviceIDFromRegistry.begin(),DeviceIDFromRegistry.end(),DeviceIDFromRegistry.begin(), tolower);
			std::transform(DevIdToFind.begin(),DevIdToFind.end(),DevIdToFind.begin(), tolower);
			//Now check if the hardware ID we are looking at contains the correct VID/PID
			MatchFound = ( DeviceIDFromRegistry.find( DevIdToFind ) != wstring::npos );
#else
			string DeviceIDFromRegistry((char *)PropertyValueBuffer);
			string DevIdToFind(MY_DEVICE_ID);
			std::transform(DeviceIDFromRegistry.begin(),DeviceIDFromRegistry.end(),DeviceIDFromRegistry.begin(), tolower);
			std::transform(DevIdToFind.begin(),DevIdToFind.end(),DevIdToFind.begin(), tolower);
			MatchFound = ( DeviceIDFromRegistry.find( MY_DEVICE_ID ) != string::npos );
#endif

			free(PropertyValueBuffer);		//No longer need the PropertyValueBuffer, free the memory to prevent potential memory leaks


			if(MatchFound == true)
			{
				//Device must have been found.  Open WinUSB interface handle now.  In order to do this, we will need the actual device path first.
				//We can get the path by calling SetupDiGetDeviceInterfaceDetail(), however, we have to call this function twice:  The first
				//time to get the size of the required structure/buffer to hold the detailed interface data, then a second time to actually
				//get the structure (after we have allocated enough memory for the structure.)
				DetailedInterfaceDataStructure->cbSize = sizeof(SP_DEVICE_INTERFACE_DETAIL_DATA);
				//First call populates "StructureSize" with the correct value
				SetupDiGetDeviceInterfaceDetail(DeviceInfoTable, InterfaceDataStructure, NULL, NULL, &StructureSize, NULL);
				DetailedInterfaceDataStructure = (PSP_DEVICE_INTERFACE_DETAIL_DATA)(malloc(StructureSize));		//Allocate enough memory
				if(DetailedInterfaceDataStructure == NULL)	//if null, error, couldn't allocate enough memory
				{	//Can't really recover from this situation, just exit instead.
					SetupDiDestroyDeviceInfoList(DeviceInfoTable);	//Clean up the old structure we no longer need.
					return 1;
				}
				DetailedInterfaceDataStructure->cbSize = sizeof(SP_DEVICE_INTERFACE_DETAIL_DATA);
				//Now call SetupDiGetDeviceInterfaceDetail() a second time to receive the goods.
				SetupDiGetDeviceInterfaceDetail(DeviceInfoTable, InterfaceDataStructure, DetailedInterfaceDataStructure, StructureSize, NULL, NULL);
				//We now have the proper device path, and we can finally open a device handle to the device.
				//WinUSB requires the device handle to be opened with the FILE_FLAG_OVERLAPPED attribute.
				ErrorStatus = GetLastError();
				MyDeviceHandle = CreateFile((DetailedInterfaceDataStructure->DevicePath), GENERIC_READ | GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE, NULL, OPEN_EXISTING, FILE_FLAG_OVERLAPPED, 0);

				ErrorStatus = GetLastError();
				if(ErrorStatus == ERROR_SUCCESS)
				{
					//Now get the WinUSB interface handle by calling WinUsb_Initialize() and providing the device handle.
					BoolStatus = 0;
					BoolStatus = WinUsb_Initialize(MyDeviceHandle, &MyWinUSBInterfaceHandle);
					//If BoolStatus == TRUE the "MyWinUSBInterfaceHandle" was initialized successfully.
					//May begin using the MyWinUSBInterfaceHandle handle in WinUsb_WritePipe() and
					//WinUsb_ReadPipe() function calls now.  Those are the functions for writing/reading to
					//the USB device's endpoints.
				}

				SetupDiDestroyDeviceInfoList(DeviceInfoTable);	//Clean up the old structure we no longer need.
				return !(int)BoolStatus;
			}

			InterfaceIndex++;
			//Keep looping until we either find a device with matching VID and PID, or until we run out of items.
		}//end of while(true)

	}



	int UsbSetSerial(char serial[15], char GUID[37])
	{
		if( devState.dllState != DEV_CONNECTED )
		{
			return 1; // return error
		}
		ResetEvent(GetSerialCmdRespReceived);
		EnterCriticalSection( &txCmdLock );
		{
			pktOutCmd.cmd = CMD_SET_SERIAL;
			for (int j = 0; j < 14; j++)
			{
				pktOutCmd.serialInfo.serialString[j] = serial[j];
			}
			pktOutCmd.serialInfo.serialString[14] = '\0';
			for (int j = 0; j < 37; j++)
			{
				pktOutCmd.serialInfo.GUIDString[j] = GUID[j];
			}
			pktOutCmd.serialInfo.GUIDString[36] = '\0';
			ResetEvent( hEventTx );
			PostThreadMessage( m_dwTxThreadID, MSG_USB_SEND, sizeof( pktOutCmd.serialInfo ), 0 );
			WaitForSingleObjectEx( hEventTx, 5000, false );

		} LeaveCriticalSection( &txCmdLock );

		if ( 0!= WaitForSingleObject(GetSerialCmdRespReceived, 3000 ) )
		{
			cout << " SETTING SERIAL NUMBER FAILED\n\n";
			return 1;
		}
		else
		{
			cout << " SETTING SERIAL NUMBER SUCCEEDED\n\n";
			return 0;
		}
	}



	int UsbGetSerial()
	{
		if( devState.dllState != DEV_CONNECTED )
		{
			return 1; // return error
		}
		ResetEvent(GetSerialCmdRespReceived);
		EnterCriticalSection( &txCmdLock );
		{
			if( devState.dllState == DEV_CONNECTED )
			{
				pktOutCmd.cmd = CMD_GET_SERIAL;
				ResetEvent( hEventTx );
				PostThreadMessage( m_dwTxThreadID, MSG_USB_SEND, sizeof( unsigned char ), 0 );
				WaitForSingleObjectEx( hEventTx, 5000, false );
			}
		} LeaveCriticalSection( &txCmdLock );

		if ( 0!= WaitForSingleObject(GetSerialCmdRespReceived, 3000 ) )
		{
			cout << " NO RESPONSE FOR SERIAL NUMBER QUERY\n\n";
			return 1;
		}
		else
		{
			cout << " RESPONSE FOR SERIAL NUMBER QUERY\n\n";
			return 0;
		}
	}


}
