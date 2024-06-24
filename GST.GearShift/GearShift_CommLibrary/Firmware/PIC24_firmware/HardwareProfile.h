#ifndef __HARDWAREPROFILE_H
#define __HARDWAREPROFILE_

#include <p24fxxxx.h>

#define GetSystemClock()            32000000UL
#define GetPeripheralClock()        (GetSystemClock())
#define GetInstructionClock()       (GetSystemClock() / 2)




#define self_power 1

#define USB_BUS_SENSE       U1OTGSTATbits.SESVD /*PORTEbits.RE5*/
//#define USB_BUS_SENSE       PORTEbits.RE5



#endif
