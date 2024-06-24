#ifndef _CAN_H_
#define _CAN_H_

#define CAN_MaxRxNodesCount 10 // maximum number of nodes to listen
#define CAN_MaxTxNodesCount 10 // maximum number of nodes to transfer to

typedef struct 
{
   
   #ifdef _WIN32
      unsigned int MsgID; // message unique identifier (non-CAN layer)
   #else
	  unsigned long MsgID; // message unique identifier (non-CAN layer)
   #endif   
  
  unsigned char NodeID_Index; // index in the destination identifiers array (full identifiers are not sent to save USB transfer) 
  unsigned char IsExtendedFrame; // if the frame is CAN2.0B type (extended)
  unsigned char IsSingleShot; // if this message should be sent in single shot mode
  unsigned char DLC; // Message Data Length Code (0-8 bytes)
  
  unsigned char Msg[8]; // Message content 
}  CANMsg;

void CanInit();
void CanClearBuffers();





#endif
