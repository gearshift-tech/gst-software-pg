#pragma once

#ifndef _CANPACKET_H
#define _CANPACKET_H



typedef union 
{
   unsigned char bytes[17];
   
   struct
   {
      unsigned char SIDH;   
      unsigned char SIDL;
      unsigned char EID8;
      unsigned char EID0;
      unsigned char DLC;
      unsigned char data[8];
      
      #ifdef WIN32
        UINT32 timestamp; 
      #else
        unsigned long timestamp;
      #endif
      //unsigned char IS_XTD;
   } PACK data;  
} CAN_PACKET;


#endif //_CANPACKET_H

