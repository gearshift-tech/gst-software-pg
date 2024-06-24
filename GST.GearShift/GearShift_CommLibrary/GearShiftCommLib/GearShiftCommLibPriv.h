#pragma once

#define __GearShiftCommLibPriv_h__

#include <winusb.h>
#include "GearShiftCommLib.h"
#include "../Firmware/PIC24_firmware/USBPacket.h"
#include "DAQ_Functionality.h"


namespace GearShiftCommLib
{

#ifdef __GearShiftCommLib_cpp
  #define EXT_ESCL_PRV
#else
  #define EXT_ESCL_PRV extern
#endif


	#define CURR_DRV_MAX_COUNT		9
	#define PACKET_MAX_AO_COUNT		18
	#define NUM_TX_REP				5
	#define NUM_RX_REP				5
	#define	NUM_INIT_REP			5
	#define EP_SIZE					64
	#define TIMER_INTERVAL			10

	#define MSG_USB_CONNECT		WM_APP + 1
	#define MSG_USB_DISCONNECT	WM_APP + 2
	#define MSG_USB_RESET		WM_APP + 3
	#define MSG_USB_START		WM_APP + 4
	#define MSG_USB_STOP		WM_APP + 5
	#define MSG_USB_SET_CONFIG	WM_APP + 6
	#define MSG_USB_SEND		WM_APP + 7

	#define TIMER_EVENT			0x0010


	#define MAX_STATE_WAIT_COUNTER		100
	#define MAX_RCV_NON_SERVICED_CNT	7

	typedef struct
  {
		unsigned int pwmFreq;
		unsigned int currentADCMask;
		unsigned int numPressSense;
		unsigned int numCurrSense;
		unsigned int numPwmDrv;
		unsigned char currReadChannsCount;
		unsigned char currReadChannsIndices[9];
	} TDeviceConfig;

  enum MessageType
  {
    MESSAGE_DAQ =   0x01,
    MESSAGE_CAN =   0x02,
    MESSAGE_OBD =   0x03,
    MESSAGE_HWUI =  0x04
  };



       DWORD WINAPI ThreadFunc( LPVOID pvParam );
       DWORD WINAPI TxThreadFunc( LPVOID pvParam );
       DWORD WINAPI rxThreadFunc( LPVOID pvParam );
       LRESULT CALLBACK MainWndProc( HWND hWnd, UINT msg, WPARAM wParam,LPARAM lParam );

       EXT_ESCL_PRV int initialized;
       EXT_ESCL_PRV int thrRetCode;
       EXT_ESCL_PRV int txInProgress;
       EXT_ESCL_PRV int stateWaitCounter;

       EXT_ESCL_PRV HANDLE hEvent;
       EXT_ESCL_PRV HANDLE hEventVersion;
       EXT_ESCL_PRV HANDLE hEventState;
       EXT_ESCL_PRV HANDLE hEventConfig;
       EXT_ESCL_PRV HANDLE hEventTx;
       EXT_ESCL_PRV unsigned int hTimer;

       EXT_ESCL_PRV TDataPacket pktIn;
       EXT_ESCL_PRV TDataPacket pktOut;
       EXT_ESCL_PRV TDataPacket pktOutCmd;

       EXT_ESCL_PRV TFirmwareVersion fwVersion;
       EXT_ESCL_PRV TDeviceState devState;

       EXT_ESCL_PRV TDeviceConfig devConfig;


       EXT_ESCL_PRV CRITICAL_SECTION txLock;
       EXT_ESCL_PRV CRITICAL_SECTION txCmdLock;
       EXT_ESCL_PRV CRITICAL_SECTION rxLock;

       EXT_ESCL_PRV DWORD m_dwThreadID;
       EXT_ESCL_PRV DWORD m_dwTxThreadID;
       EXT_ESCL_PRV DWORD m_dwRxThreadID;
       EXT_ESCL_PRV HANDLE hThread;
       EXT_ESCL_PRV HANDLE hTxThread;
       EXT_ESCL_PRV HANDLE hRxThread;
       EXT_ESCL_PRV HMODULE hLib;

       EXT_ESCL_PRV HINSTANCE hinst;
       EXT_ESCL_PRV HWND hWnd;
       EXT_ESCL_PRV int thrInitialized;
       EXT_ESCL_PRV int thrTxInitialized;
       EXT_ESCL_PRV int thrRxInitialized;

       EXT_ESCL_PRV unsigned int rcvServiced;
       EXT_ESCL_PRV unsigned int rcvNonServicedCnt;

       EXT_ESCL_PRV BOOL msgReturn;
       EXT_ESCL_PRV MSG msg;
       EXT_ESCL_PRV DWORD lnRd;
       EXT_ESCL_PRV DWORD lnWr;
       EXT_ESCL_PRV WPARAM retCode;
       EXT_ESCL_PRV int tx;

       EXT_ESCL_PRV HANDLE EnterBootloaderCmdRespReceived;

       EXT_ESCL_PRV HANDLE GetSerialCmdRespReceived;


#define DEV_INIT_MSG_TIMEOUT_MS 300

         //EXT_ESCL_PRV HANDLE DEV_INIT_Event_GET_DEV_VERSION;
         //EXT_ESCL_PRV HANDLE DEV_INIT_Event_SET_DEV_CONFIG;


    EXT_ESCL_PRV HANDLE MyDeviceHandle;
    EXT_ESCL_PRV WINUSB_INTERFACE_HANDLE MyWinUSBInterfaceHandle;

	  int initialize( void );

	  int Init( void );
	  int DeInit( void );

    int WinUsbConnect();
    void WinUsbDisconnect();

	  int UsbOpen( void );
	  void UsbClose( void );
	  int UsbWrite( volatile TDataPacket * pkt, int size, unsigned int timeout );
	  int UsbRead( volatile TDataPacket * pkt, int size, unsigned int timeout );

	  void LoadPktData( volatile TDataPacket * pkt );
	  void LoadPktData( TUsbTxData * data, volatile TDataPacket * pkt );
	  void SavePktData( volatile TDataPacket * pkt );

}