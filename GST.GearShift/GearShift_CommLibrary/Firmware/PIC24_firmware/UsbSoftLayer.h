#include "USB/usb_device.h"
#include "USB/usb.h"
#include "USB/usb_function_generic.h"
#include "usb_config.h"

#ifdef EXT
#undef EXT
#endif

#ifdef _USB_SOFT_LAYER_C_
#define EXT
#else
#define EXT extern
#endif

EXT unsigned char usbStateVal;
EXT USB_HANDLE usbOutHandle;
EXT USB_HANDLE usbInHandle;
EXT TDataPacket inPacket;
EXT TDataPacket outPacket;
EXT BYTE counter;
EXT unsigned long pktID;
EXT unsigned int USB_LoopCountsWithoutUsbTraffic;

#define USB_LoopCountsWithoutUsbTrffic 5 // approx 500ms


void USBCBSuspend(void);

void USBCBWakeFromSuspend(void);


void USBCB_SOF_Handler(void);


void USBCBErrorHandler(void);


void USBCBCheckOtherReq(void);


void USBCBStdSetDscHandler(void);


void USBCBInitEP(void);


void USBCBSendResume(void);


void ServiceRequests(void);

void USBDisconnected( void );

void USBConnected( void );


unsigned int USER_USB_CALLBACK_EVENT_HANDLER(USB_EVENT event, void *pdata, WORD size);
