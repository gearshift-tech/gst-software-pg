// PLCSigGenDLLTest.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <windows.h>
#include <stdio.h>
#include <string>
#include <iostream>
#include <iomanip>

#include "../GearShiftCommLib/GearShiftCommLib.h"
#include "../GearShiftCommLib/DAQ_Functionality.h"
#include "../GearShiftCommLib/HW_UI_Functionality.h"
#include "../GearShiftCommLib/CAN_Functionality.h"
#include "../GearShiftCommLib/OBD_Functionality.h"
#include <fstream>
#include "../GearShiftCommLib/RingBuffer.h"
//#include "RingBuffer.cpp"

#include "hr_time.h"


using namespace std;
using namespace GearShiftCommLib;
using namespace RingBfr;

TUsbTxData txData[ 4096 ];
TUsbRxData rxData[ 1024 ];


//TCanFixedTraceRecord rxCANFXDTRCE[ 2048 ];
//UINT32 rxCANFXDTRCECOUNT = 0;
TUsbCANData txCANData[ 4096 ];

TUsbCANData rxCANData[ 4096 ];

int _tmain(int argc, _TCHAR* argv[])
{

  for (int lol = 0; lol < 255; lol++)
  {
    txCANData[lol].DLC = 1;
    txCANData[lol].isXtdFrameType = true;
    txCANData[lol].remoteID = 536867345;
    txCANData[lol].data[0] = lol;
  }

	string str;
	unsigned int num;
//	unsigned int i, j;
	int idx;
	unsigned int state, errorCode, overCurrentPorts;

  char ll;

  UsbConnect();

  while (0)
  {
    cin >> ll;
    UsbConnect();
  }
  int x = 0;
  //cout << "USBCRSLT: " << UsbConnect();

  // goto PWM;
//    UsbEnterBootloader();
//    while(1);

  char serial[15] = "00010103000013";
  char GUID[37] = "35160BB3-D823-4822-9EA0-3D44E041DE05";

  //cout << "FSDFSDFSDFSDFSDF" << serial;

  //UsbSetSerial(serial, GUID);
  //UsbGetSerial();

  //UsbConnect();
  //UsbUIDisplayLcdRow1Msg(" USB BOOTLOADER ");
  //UsbUIDisplayLcdRow2Msg("      MODE      ");
  //Sleep(500);
  //UsbEnterBootloader();
  //Sleep(500);
  //return 0;

  //while(1);

  //UsbOBDElm327Init( );
  //while (1)
  //{
  //  cin >> x;
  //  UsbOBDElm327Init( );
  //}
  //cin >> x;
  //UsbDisconnect();
  //while(1);

//goto PWM;


 //int x = UsbOBDElm327Init();


  UINT32 written = 0;
  UINT32 CANFill = 0;

  double baudError = 0;
  //cout << "CSC: " << UsbCANSetConfig( 500000, &baudError ) << endl;
  //int rslt = UsbCanEnableComm();
  //cout << "CAN INIT: " << rslt << endl;

  //cin >> x;
  //UsbCANWriteData( txCANData, 255, &written );
  //cout << written << " sent\n";

  //UsbCANGetDevTxBuffFill( &CANFill );
  //cout << CANFill << endl;

PWM:

  int tmp = 0;
  for( int i = 0; i < 4096; i++ )
  {
    txData[ i ].ID = i + 1;
	  txData[i].AO1 = i;
	  txData[i].AO2 = (i+ 50 ) % 100;
	  for( int j = 0; j < 9; j++ )
    {
			txData[ i ].Pwm[ j ] = 50;//(i+ j*10 ) % 100;
		}
    tmp += 1;
    if (tmp > 100)
      tmp = 90;
	}


  unsigned char currChansCount = 9;
  unsigned char currChansIndicesNeg[9] = { 0, 1, 2, 3, 4, 5, 6, 7, 8};
  unsigned char currChansIndicesPos[9] = { 9, 10, 11, 12, 13, 14, 15, 16, 17};

  unsigned char* currChansIndices;
  currChansIndices = currChansIndicesNeg;
  currChansIndices = currChansIndicesPos;

  UsbSetDAQConfig(1200, currChansCount, currChansIndices);

  UsbUIDisplayLcdRow1Msg("    TESTING     ");
  UsbUIDisplayLcdRow2Msg("  OVERCURRENTS  ");

  UsbPwmStart();

	idx = 0;


  while (0)
  {
    UsbReadData( rxData, 2048, &num );
    UsbGetDeviceState( &state, &errorCode, &overCurrentPorts );
    cout << overCurrentPorts << endl;
    Sleep(200);

  }


	while( 1 )
  {

		Sleep( 100 );

// 		if( idx + 30 > 2047 )
//     {
// 			idx = 0;
//       UsbUIDisplayLcdRow1Msg("blablabla 2     ");
//       UsbUIDisplayLcdRow2Msg("blablabla 3     ");
//       UsbUISetBargraphsMode( 1 );
// 		}
    //UsbUIDisplayLcdRow1Msg("blablabla 2     ");

		UsbWriteData( &txData[ idx ], 100, &num );
		idx += num;
    if (idx > 2048)
      idx = 0;

		UsbReadData( rxData, 2048, &num );
		UsbGetDeviceState( &state, &errorCode, &overCurrentPorts );
		if( state == DEV_ERROR )
    {
			cout << "Error: " << errorCode << endl;
			while( 1 )
      {
				Sleep( 100 );
			}
		}


 		cout << "Read: " << num << endl;

 		for( int i = num-1; i < num; i++ ) {
 			cout << "Packet: " << rxData[ i ].ID << endl;
 			cout << "In response to: " << rxData[ i ].responseToID << endl;
 			cout << "Pressures: ";
 			for( int j = 0; j < 14; j++ ) {
 				cout << rxData[ i ].pressure[ j ] << ", ";
 			}
 			cout << endl;
 			cout << "Currents: ";
 			for( int j = 0; j < 18; j++ ) {
 				cout << rxData[ i ].current[ j ] << ", ";
 			}
 			cout << endl << endl;


		}

	}


	return 0;
}

