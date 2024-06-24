#pragma once

#define __HW_UI_Functionality_h

namespace GearShiftCommLib 
{

  extern "C" __declspec( dllexport ) void UsbUIDisplayLcdRow1Msg( char message[17] );

  extern "C" __declspec( dllexport ) void UsbUIDisplayLcdRow2Msg( char message[17] );

  extern "C" __declspec( dllexport ) void UsbUISetBargraphsMode( unsigned char mode );

}

