// dllmain.cpp : Defines the entry point for the DLL application.
#include <Windows.h>

#include "../Firmware/PIC24_firmware/USBPacket.h"
#include "GearShiftCommLib.h"
#include "GearShiftCommLibPriv.h"
#include <string>
#include <iostream>



SHARED volatile int thrCount;

using namespace std;
using namespace GearShiftCommLib;


BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
					 )
{
	switch ( ul_reason_for_call )
	{
		case DLL_PROCESS_ATTACH:
			if( Init() != ERR_NONE ) {
				return false;
			}
			break;
		case DLL_THREAD_ATTACH:
			break;
		case DLL_THREAD_DETACH:
			break;
		case DLL_PROCESS_DETACH:
			if( DeInit() != ERR_NONE ) {
				return false;
			}
			break;
	}

	return TRUE;
}