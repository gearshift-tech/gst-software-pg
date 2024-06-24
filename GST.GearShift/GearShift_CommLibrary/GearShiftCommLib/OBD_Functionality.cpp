#define __OBD_Functionality_cpp

#include <iostream>
#include <iomanip>
#include <windows.h>

#include <string>
#include <vector>


#include "HW_UI_Functionality.h"
#include "GearShiftCommLibPriv.h"
#include "RingBuffer.h"
#include "OBD_Functionality.h"

using namespace std;

namespace GearShiftCommLib
{
  extern int UsbObdElm327BfrsInit( void );

/// <summary>
/// Initializes the OBD internal variables
/// </summary>
void OBD_InitInternals( void )
{
  OBD_Event_ELM_BFRS_INIT_COMPLETE = CreateEvent(NULL, TRUE, FALSE, (LPCWSTR)"ELM_BFRS_INIT_COMPLETE");
  OBD_Event_StartupMessageReceived = CreateEvent(NULL, TRUE, FALSE, (LPCWSTR)"OBD_Event_StartupMessageReceived");
  OBD_Event_ATSP0RespReceived = CreateEvent(NULL, TRUE, FALSE, (LPCWSTR)"OBD_Event_ATSP0RespReceived");
  OBD_Event_ProtocolNumberReceived = CreateEvent(NULL, TRUE, FALSE, (LPCWSTR)"OBD_Event_ProtocolNumberReceived");
  OBD_Event_ATH1CmdRespReceived = CreateEvent(NULL, TRUE, FALSE, (LPCWSTR)"OBD_Event_ATH1CmdRespReceived");
  OBD_Event_0101CmdRespReceived = CreateEvent(NULL, TRUE, FALSE, (LPCWSTR)"OBD_Event_0101CmdRespReceived");

  SetEvent( OBD_Event_ELM_BFRS_INIT_COMPLETE );
  SetEvent( OBD_Event_StartupMessageReceived );
  SetEvent( OBD_Event_ATSP0RespReceived );
  SetEvent( OBD_Event_ProtocolNumberReceived );
  SetEvent( OBD_Event_ATH1CmdRespReceived );
  SetEvent( OBD_Event_0101CmdRespReceived );

  OBD_StartupMessageRequested = false;
  OBD_Event_ATSP0Requested = false;
  OBD_Event_ProtocolNumberRequested = false;
  OBD_ATH1CmdRequested = false;
  OBD_0101CmdRequested = false;

  //dump.open("c:\\dumppp.txt", ios::out );
  //dump.close();
}

int OBD_ProcessTx( void )
{
  // SEND OBD DATA
  if ( devState.dllState == DEV_CONNECTED )
  {
    //Aim to have the buffer half-empty
    int charsToWrite = ( ( OBD_DevTxBuffSize * 3) / 6 ) - OBD_DevTxBuffFill;
    //cout << "OBD_DevTxBuffSize: " << OBD_DevTxBuffSize << " OBD_DevTxBuffFill: " << OBD_DevTxBuffFill << endl;
    //cout << OBD_DevTxBuffFill << endl;
    //if (OBD_TxBuffer.GetBuffFill() < charsToWrite)
    //{
    //  charsToWrite = OBD_TxBuffer.GetBuffFill();
    //}
    if (charsToWrite > 50)
    {
      charsToWrite = 50; // only 50 characters fit in 1 USB packet
    }
    if (charsToWrite > 0 && OBD_TxBuffer.GetBuffFill())
    {
      //cout << "BUFF FILL: " << OBD_TxBuffer.GetBuffFill() << endl;
      //cout << "TX\n";
      //cout << pktsToWrite << endl;
      //cout << "devtbfll: " << CAN_DevTxBuffFill << endl;
      //cout << (int)pktIn.CANDataPacket.msgCount << endl;
      //cout << CAN_RxBuffer.GetBuffFill()+1 << endl;
      pktOut.cmd = CMD_OBD_DATA;
      pktOut.OBDDataPacket.charCount = charsToWrite;
      int charsWritten = 0;
      while (1)
      {
        TUsbOBDTxData msg;
        OBD_TxBuffer.GetDataWithoutRemoval(&msg);
        //cout << "get msg...  ";
        //                SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), 13);
        if ( (charsWritten + msg.charCount < charsToWrite) && (OBD_TxBuffer.GetBuffFill() > 0) )
        {
          //cout << "\nPKOUT:\n";
          for ( int z = 0; z < msg.charCount; z++)
          {
            pktOut.OBDDataPacket.chars [ charsWritten + z ] = msg.chars [ z ];
            //cout << msg.chars[ z ];// << endl;

          }
          charsWritten += msg.charCount;
          //cout << "\\PKOUT\n";
          //cout << "Add " << (int)msg.charCount << "characters";
          OBD_TxBuffer.RemoveFirst();
          //cout << "remove msg...  \n";
        }
        else
        {
          pktOut.OBDDataPacket.charCount = charsWritten;
          //cout << "WRITTEN TOTAL " << charsWritten << endl;
          //cout << "BUFF FILL: " << OBD_TxBuffer.GetBuffFill() << endl;
          break;
        }
        //                SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), 15);
      }
      //MPUSBWrite( outPipe, ( void * ) &pktOut, sizeof( pktOut.OBDDataPacket ), &lnWr, 7 );
	  //pktOut.OBDDataPacket.charCount = 0;
      WinUsb_WritePipe(MyWinUSBInterfaceHandle, 0x01, (PUCHAR)&pktOut, sizeof( pktOut.OBDDataPacket ), &lnWr, NULL);
	  //cout << "WRITTEN OBD " << (int)pktOut.OBDDataPacket.charCount << " chars written\n";
      tx = 1;
    }
  }
  return 0;
}

/// <summary>
/// Extracts the uints from the string with HEX values separated by space characters
/// If invalid character is found in the string the function returns error (1)
/// </summary>
int ExtractUIntsFromHexString( std::string str, std::vector<unsigned int> *output )
{
  int tmp = 0;
  int strSize = str.size();
   // to protect against space character at the beginning of the string
   if (strSize > 0)
   {
     if (str[0] == ' ')
     {
       str.erase(0, 1);
       strSize = str.size();
     }
   }
  output->clear();
  for (int i = 0; i < strSize; i++)
  {
    char chr = str[i];
    if ( chr == ' ' )
    {
      output->push_back(tmp);
      tmp = 0;
    }
    else
    {
      bool validChar = false;
      // get integer value of a particular character
      char intVal = 0;
      if ( chr >= 'A' && chr <= 'F')
      {
        intVal = ( (unsigned char)chr ) - 'A' + 10;
        validChar = true;
      }
      else
      {
        if (chr >= '0' && chr <= '9')
        {
          intVal = ( (unsigned char)chr ) - '0';
          validChar = true;
        }
      }
      // if invalid character was found treat the whole string as invalid and return error
      if (!validChar)
      {
        output->clear();
        return 1;
      }
      //process current value
      tmp *= 16;
      tmp += intVal;

      // if this is a last character
      if (i == ( strSize - 1) )
      {
        output->push_back(tmp);
      }
    }
  }
  return 0;

}

/// <summary>
/// Initializes the ELM327
/// Function returns 0 if succeded, 1 if error occured
/// </summary>
int UsbOBDElm327Init( void )
{
  //cout << "ELM327INIT\r\n";
  // Clear the buffer and disable buffer filling
  OBD_Elm327State = ELM327STATE_UNINITIALIZED;
  OBD_TxBuffer.Clear();

  if ( UsbObdElm327BfrsInit() )
  {
    return 1;//if buffers init failed return error
  }

  UsbUIDisplayLcdRow1Msg("    OBD INIT    ");
  UsbUIDisplayLcdRow2Msg("                ");
  // Reset device and wait for the init message
  ResetEvent(OBD_Event_StartupMessageReceived);
  UsbOBDInitOnlySendCmd("ATZ\n\r");
  OBD_StartupMessageRequested = true;
  if ( 0!= WaitForSingleObject(OBD_Event_StartupMessageReceived, OBD_INIT_MSG_TIMEOUT_MS ) )
  {
    OBD_Elm327State = ELM327STATE_INIT_TIMEOUT_ERR;
    cout << "timeout @ startup msg\n";
    UsbUIDisplayLcdRow2Msg("ELM NOT FOUND!! ");
    return 1;
  }
  else
  {
    OBD_Elm327State = ELM327STATE_INIT_MSG_RCVD;
    UsbUIDisplayLcdRow2Msg("#               ");
    Sleep(50);
    UsbUIDisplayLcdRow2Msg("##              ");
    Sleep(50);
    UsbUIDisplayLcdRow2Msg("###             ");

    Sleep(200);
    //cout << "init rcvd" << endl;
  }

  // If init message received, set protocol to auto
  ResetEvent( OBD_Event_ATSP0RespReceived );
  UsbOBDInitOnlySendCmd("ATSP0\n\r");
   OBD_Event_ATSP0Requested = true;
   if ( 0!= WaitForSingleObject( OBD_Event_ATSP0RespReceived, OBD_INIT_MSG_TIMEOUT_MS ) )
   {
     cout << "timeout @ ATSP0 \n";
     UsbUIDisplayLcdRow2Msg("ELM INIT ERROR!");
     OBD_Elm327State = ELM327STATE_INIT_TIMEOUT_ERR;
     return 1;
   }
   else
   {
     OBD_Event_ATSP0Requested = false;
     UsbUIDisplayLcdRow2Msg("####            ");
     Sleep(50);
     UsbUIDisplayLcdRow2Msg("#####           ");
     Sleep(50);
     UsbUIDisplayLcdRow2Msg("######          ");

     Sleep(200);
     //cout << "ATSP0 rcvd" << endl;
   }

  // If protocol set to auto turn headers on
  ResetEvent( OBD_Event_ATH1CmdRespReceived );
  UsbOBDInitOnlySendCmd("ATH1\n\r");
  OBD_ATH1CmdRequested = true;
  if ( 0!= WaitForSingleObject( OBD_Event_ATH1CmdRespReceived, OBD_INIT_MSG_TIMEOUT_MS ) )
  {
    cout << "timeout @ headers on \n";
    UsbUIDisplayLcdRow2Msg("ELM INIT ERROR!");
    OBD_Elm327State = ELM327STATE_INIT_TIMEOUT_ERR;
    return 1;
  }
  else
  {
    //OBD_Elm327State = ELM327STATE_PROTOCOL_DEFINED;
    OBD_ATH1CmdRequested = false;
    UsbUIDisplayLcdRow2Msg("#######       ");
    Sleep(50);
    UsbUIDisplayLcdRow2Msg("########      ");
    Sleep(50);
    UsbUIDisplayLcdRow2Msg("#########     ");
    Sleep(200);
    //cout << "headers on rcvd" << endl;
  }

  // If headers on request cmd 0101 information
  ResetEvent( OBD_Event_0101CmdRespReceived );
  UsbOBDInitOnlySendCmd("0101\n\r");
  OBD_0101CmdRequested = true;
  if ( 0!= WaitForSingleObject( OBD_Event_0101CmdRespReceived , OBD_INIT_MSG_TIMEOUT_MS ) )
  {
    cout << "timeout @ 0101 # \n";
    UsbUIDisplayLcdRow2Msg("ELM INIT ERROR!");
    OBD_Elm327State = ELM327STATE_INIT_TIMEOUT_ERR;
    return 1;
  }
  else
  {
    OBD_0101CmdRequested = false;
    UsbUIDisplayLcdRow2Msg("##########    ");
    Sleep(50);
    UsbUIDisplayLcdRow2Msg("###########   ");
    Sleep(50);
    UsbUIDisplayLcdRow2Msg("############  ");
    Sleep(200);
    //cout << "0101 rcvd" << endl;
  }

  // If init message received request protocol information
  ResetEvent( OBD_Event_ProtocolNumberReceived );
  UsbOBDInitOnlySendCmd("ATDPN\n\r");
  OBD_Event_ProtocolNumberRequested = true;
  if ( 0!= WaitForSingleObject( OBD_Event_ProtocolNumberReceived, OBD_INIT_MSG_TIMEOUT_MS ) )
  {
    cout << "timeout @ protocol # \n";
    UsbUIDisplayLcdRow2Msg("ELM INIT ERROR!");
    OBD_Elm327State = ELM327STATE_INIT_TIMEOUT_ERR;
    return 1;
  }
  else
  {
    OBD_Elm327State = ELM327STATE_PROTOCOL_DEFINED;
    OBD_Event_ProtocolNumberRequested = false;
    UsbUIDisplayLcdRow2Msg("############# ");
    Sleep(50);
    UsbUIDisplayLcdRow2Msg("##############");
    Sleep(200);
    //UsbUIDisplayLcdRow2Msg("                ");

    cout << "protocol rcvd" << endl;
  }
  UsbUIDisplayLcdRow1Msg("    VEHICLE     ");
  UsbUIDisplayLcdRow2Msg("   CONNECTED!   ");
  OBD_Elm327State = ELM327STATE_READY_TO_TALK;
  return 0;
}

/// <summary>
/// Processes a raw response from ELM327, extracts
/// Make sure that passed response is complete
/// Params:
/// char* msg        : response to process
/// int charCount    : length of the response
/// </summary>
void UsbOBDProcessSingleRawResponse(char* msg, int charCount)
{
  // CUT THIS BLOCK !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
//  OBD_Elm327CurrentProtocol = 0x01;


  //msg = "86 F1 11 41 02 00 00 C9 \n 86 F1 18 7F 01 12 1E";

//  msg  = " 87 F1 11 49 02 01 00 00 00 57 2C\n 87 F1 11 49 02 02 30 4C 30 54 D6\n 87 F1 11 49 02 03 47 46 34 38 D0\n 87 F1 11 49 02 04 33 35 31 38 A9\n 87 F1 11 49 02 05 32 30 37 33 A5\n 83 F1 18 7F 09 12 26 ";
//
//
//
//

  //charCount = 45;
  //msg = jacieniepierdole;
  //charCount = strlen (msg);
  //msg = "Searching...\n7E8 10 14 49 02 01 4F 5A 45\n7E8 21 4E 20 45 4C 45 4B 54\n7E8 22 52 4F 20 32 2E 30 31";
  //msg = "7E8 06 41 01 00 07 E5 00";
  //msg = "7E8 02 43 00";
  //charCount = strlen (msg);

  // ---------------------------------------------------------------------------------------
  // Decompose message into separate lines
  // ---------------------------------------------------------------------------------------
  vector<string> lines;
  string line("");
//   for (int i = 0; i < charCount; i++)
//   {
//     if (msg[i] == '\n')
//       cout << '%';
//     //else
//       cout << msg[i];
//   }
  for (int i = 0; i < charCount; i++)
  {
    char chr = msg[i];
    if (chr == '\n')
    {
      if (line.length() != 0)
      {
        lines.push_back(line);
        //cout << "LINE: " << line << endl;
        line = string("");
      }
    }
    else
    {
      line.append(&chr, 1);
      if (i == (charCount - 1) )
      {
        if (line.length() != 0)
        {
          lines.push_back(line);
          //cout << "LINE: " << line << endl;
          line = string("");
        }
      }
    }
    if ( (i == charCount - 1) && (line.length() != 0) )
    {
      lines.push_back(line);
    }
  }

  // ---------------------------------------------------------------------------------------
  // process AT commands first
  // ---------------------------------------------------------------------------------------
  if ( OBD_StartupMessageRequested )
  {
    string expectedResponse("ELM327 v1.3");
    if ( (lines.size() > 1) && ( lines[1].find(expectedResponse) != string::npos ) )
    {
      // set the event (UsbOBDElm327Init waits for this)
      SetEvent(OBD_Event_StartupMessageReceived);
      return;
    }
  }

  // check if ATSP0 response has arrived
  if ( OBD_Event_ATSP0Requested )
  {
    string expectedResponse("ATSP0");
    if ( (lines.size() > 1) && ( lines[0].find(expectedResponse) != string::npos ) )
    {
      if ( ( lines[1].find("OK") != string::npos ) )
      {
        SetEvent(OBD_Event_ATSP0RespReceived);
      }
      return;
    }
  }

  // check if headers on response has arrived
  if ( OBD_ATH1CmdRequested )
  {
    string expectedResponse("ATH1");
    if ( (lines.size() > 1) && ( lines[0].find(expectedResponse) != string::npos ) )
    {
      if ( ( lines[1].find("OK") != string::npos ) )
      {
        SetEvent(OBD_Event_ATH1CmdRespReceived);
      }
      return;
    }
  }

  // check if 0101 response has arrived
  if ( OBD_0101CmdRequested )
  {
    string expectedResponse("0101");
    if ( (lines.size() > 1) && ( lines[0].find(expectedResponse) != string::npos ) )
    {
      for (int i = 0; i < (int)lines.size(); i++)
      {
        if ( lines[i].find("41 01") != string::npos )
        {
          SetEvent(OBD_Event_0101CmdRespReceived);
          return;
        }
      }
      return;
    }
  }

  // check if protocol request response has arrived
  if ( OBD_Event_ProtocolNumberRequested )
  {
    string expectedResponse("ATDPN");
    if ( (lines.size() > 1) && ( lines[0].find(expectedResponse) != string::npos ) )
    {
      vector<unsigned int> uInts;
      ExtractUIntsFromHexString( lines[1], &uInts );
      OBD_Elm327CurrentProtocol = uInts[0];
      // set the event (UsbOBDElm327Init waits for this)
      SetEvent(OBD_Event_ProtocolNumberReceived);
      return;
    }
  }

  // ---------------------------------------------------------------------------------------
  // Process OBD commands
  // ---------------------------------------------------------------------------------------

  // OBD message response is a set of lines of hex numbers.
  // Convert lines to vectors of uInts
  // If line contains invalid characters the whole line is omitted
  vector< vector<unsigned int> > linesInts;
  for (int i = 0; i < (int)lines.size(); i++)
  {
    vector<unsigned int> uInts;
    if ( !ExtractUIntsFromHexString( lines[i], &uInts ) )
    {
      linesInts.push_back( uInts );
    }
  }
  if (linesInts.size() == 0)
  {
    // this is not a valid OBD message
    //(probably some AT non-processed command)
    return;
  }

  // Process the responded lines depending on the current protocol

  if ( OBD_Elm327CurrentProtocol == ELM327_PROTO_ISO_157654_CAN_11B500K ||
       OBD_Elm327CurrentProtocol == ELM327_PROTO_ISO_157654_CAN_11B500K + 0xA0 ||
       OBD_Elm327CurrentProtocol == ELM327_PROTO_ISO_157654_CAN_11B250K ||
       OBD_Elm327CurrentProtocol == ELM327_PROTO_ISO_157654_CAN_11B250K + 0xA0
     )
  //________ 11 BIT CAN MESSAGE ___________________________________________________
  {
    cout << "11bit CAN MSG\n";
    vector< TUsbOBDRxData > MLMsgs; // multi-line messages
    vector< vector<unsigned int> > MLMsgsData; // multi-line messages data bfr
    for (int i = 1; i < (int)linesInts.size(); i++)
    {
      // the second byte defines if this line is a single message or a part of multi-line response
      if( linesInts[i][1] < 0x10 ) // if it is a single line response...
      {
        TUsbOBDRxData pkt;
        pkt.srcAddr = linesInts[i][0];
        pkt.respToMode = linesInts[i][2] - 0x40;


        if (pkt.respToMode == 0x04) // if this is a response to clear trouble codes
        {
          // This kind of response must be processed separately because it contains
          // no data and no PID
          pkt.bytesCount = 0;
          pkt.respToPID = 0;
        }
        else
        {
          if ( (pkt.respToMode == 0x03) || (pkt.respToMode == 0x07)) // if this is trouble code response (stored / pending)
          {
            // This kind of response must be processed separately because it does not
            // carry PID information
            pkt.bytesCount = linesInts[i][1] - 0x01; // subtract 2 used for MID ONLY! (no PID in 03 mode)
            pkt.respToPID = 0x00;
            for (int z = 3; z < (int)linesInts[i].size(); z++)
            {
              pkt.bytes[z - 3] = linesInts[i][z];
            }
          }
          else
          {
            if ( pkt.respToMode == 0x02) // if this is freeze frame response
            {
              // Basically this mode puts a zero-byte at the beginning of data block which has to be removed.
              pkt.bytesCount = linesInts[i][1] - 0x03; // subtract 2 used for MID and PID plus zero-byte
              pkt.respToPID = linesInts[i][3];
              for (int z = 5; z < (int)linesInts[i].size(); z++)
              {
                pkt.bytes[z - 5] = linesInts[i][z];
              }
            }
            else
            {
              pkt.bytesCount = linesInts[i][1] - 0x02; // subtract 2 used for MID and PID
              pkt.respToPID = linesInts[i][3];
              for (int z = 4; z < (int)linesInts[i].size(); z++)
              {
                pkt.bytes[z - 4] = linesInts[i][z];
              }
            }
          }
        }
//        cout << "ADD DATA SL________________________\r\n";
        OBD_RxBuffer.PutData(pkt);
      }
      else // if it is a multi-line response
      {
        //cout << "i: " << i << " i[1]: " << hex << linesInts[i][1] << endl;
        if( linesInts[i][1] == 0x10 ) // if this is the first line of the response
        {
          //cout << "FIRSTLINE!!\n";
          TUsbOBDRxData pkt;
          pkt.srcAddr = linesInts[i][0];
          pkt.respToMode = linesInts[i][3] - 0x40;
          if ( (pkt.respToMode == 0x03) || (pkt.respToMode == 0x07) ) // if this is trouble code response (stored / pending)
          {
            //in this mode response no PID is included
            pkt.bytesCount = linesInts[i][2] - 0x02; // remove 2 for MID & CRI
            pkt.respToPID = 0;//linesInts[i][4];
            MLMsgs.push_back(pkt);
            vector< unsigned int > dt;
            for ( int j = 5; j < (int)linesInts[i].size(); j++)
            {
              dt.push_back(linesInts[i][j]);
            }
            MLMsgsData.push_back(dt);
          }
          else
          {
            pkt.bytesCount = linesInts[i][2] - 0x03; // remove 3 for MID and PID and CRI
            pkt.respToPID = linesInts[i][4];
            MLMsgs.push_back(pkt);
            vector< unsigned int > dt;
            for ( int j = 6; j < (int)linesInts[i].size(); j++)
            {
              dt.push_back(linesInts[i][j]);
            }
            MLMsgsData.push_back(dt);
          }

        }
        else // this is a second or further line of response
        {
          //cout << "NEXTLINE!!\n";
          int currSrcAddr = linesInts[i][0];
          int currIndex = -1;
          for (int j = 0; j < (int)MLMsgs.size(); j++)
          {
            if (currSrcAddr == MLMsgs[j].srcAddr)
            {
              currIndex = j;
              //cout << "FOUND MATCHING ID AT INDEX " << j << endl;
              break;
            }
          }
          if (currIndex == -1)
          {
            return; // this is inacceptable situation that there's no matching packet for this line
          }
          for ( int j = 2; j < (int)linesInts[i].size(); j++)
          {
            MLMsgsData[currIndex].push_back(linesInts[i][j]);
            //cout << "Added: " << hex << (int)linesInts[i][j] << endl;
          }
        }
      }
    }
    for (int i = 0; i < (int)MLMsgs.size(); i++)
    {
      //int dupa = (int)MLMsgs[i].bytesCount;
      //cout << "\n\nPKT VECTOR SIZE: " << (int)MLMsgsData[i].size() << " dlc: " << dupa << " _\n\n";
      for (int j = 0; j < (int)MLMsgsData[i].size(); j++)
      {
        MLMsgs[i].bytes[j] = MLMsgsData[i][j];
      }
      //cout << "ADD DATA ML________________________\r\n";
      OBD_RxBuffer.PutData( MLMsgs[i] );
    }
  }

  // If 29 bit CAN
  if ( OBD_Elm327CurrentProtocol == ELM327_PROTO_ISO_157654_CAN_29B500K ||
    OBD_Elm327CurrentProtocol == ELM327_PROTO_ISO_157654_CAN_29B500K + 0xA0 ||
    OBD_Elm327CurrentProtocol == ELM327_PROTO_ISO_157654_CAN_29B250K ||
    OBD_Elm327CurrentProtocol == ELM327_PROTO_ISO_157654_CAN_29B250K + 0xA0 ||
    OBD_Elm327CurrentProtocol == ELM327_PROTO_SAE_J1939_CAN_29B250K ||
    OBD_Elm327CurrentProtocol == ELM327_PROTO_SAE_J1939_CAN_29B250K + 0xA0
    )
  //________ 29 BIT CAN MESSAGE ___________________________________________________
  {
    cout << "29bit CAN MSG\n";

    vector< TUsbOBDRxData > MLMsgs; // multi-line messages
    vector< vector<unsigned int> > MLMsgsData; // multi-line messages data bfr
    //cout << "lc: " << (int)linesInts.size() << endl;
    for (int i = 1; i < (int)linesInts.size(); i++)
    {
      // the fourth byte defines if this line is a single message or a part of multi-line response
      if( linesInts[i][4] < 0x10 ) // if it is a single line response...
      {
        //cout << "            SINGLELINE!!\r\n";
        TUsbOBDRxData pkt;
        pkt.srcAddr = linesInts[i][3];
        pkt.respToMode = linesInts[i][5] - 0x40;

        if (pkt.respToMode == 0x04) // if this is a response to clear trouble codes
        {
          // This kind of response must be processed separately because it contains
          // no data and no PID
          pkt.bytesCount = 0;
          pkt.respToPID = 0;
        }
        else
        {
           if ( (pkt.respToMode == 0x03) || (pkt.respToMode == 0x07) ) // if this is trouble code response (stored / pending)
           {
             // This kind of response must be processed separately because it does not
             // carry PID information
             pkt.bytesCount = linesInts[i][4] - 0x02; // subtract 1 used for MID + dumy byte instead of PID (no valid PID in 03 mode)
             pkt.respToPID = 0x00;
             for (int z = 7; z < (int)linesInts[i].size(); z++)
             {
               pkt.bytes[z - 7] = linesInts[i][z];
             }
           }
           else
           {
             if ( pkt.respToMode == 0x02) // if this is freeze frame response
             {
               //cout << "MODE02\n";
               // Basically this mode puts a zero-byte at the beginning of data block which has to be removed.
               pkt.bytesCount = linesInts[i][4] - 0x03; // subtract 2 used for MID and PID zero-byte
               pkt.respToPID = linesInts[i][6];
               for (int z = 8; z < (int)linesInts[i].size(); z++)
               {
                 pkt.bytes[z - 8] = linesInts[i][z];
               }
             }
             else
             {
               pkt.bytesCount = linesInts[i][4] - 0x02; // subtract 2 used for MID and PID
               pkt.respToPID = linesInts[i][6];
               for (int z = 7; z < (int)linesInts[i].size(); z++)
               {
                 pkt.bytes[z - 7] = linesInts[i][z];
               }
             }
           }
        }
        //cout << "ADD DATA SL________________________\r\n";
        OBD_RxBuffer.PutData(pkt);
        //int kkk = 9;
      }
      else // if it is a multi-line response
      {
        //cout << "i: " << i << " i[1]: " << hex << linesInts[i][1] << endl;
        if( linesInts[i][4] == 0x10 ) // if this is the first line of the response
        {
          //cout << "FIRSTLINE!!\n";
          TUsbOBDRxData pkt;
          pkt.srcAddr = linesInts[i][3];
          pkt.respToMode = linesInts[i][6] - 0x40;
          if ( (pkt.respToMode == 0x03) || (pkt.respToMode == 0x07) ) // if this is trouble code response (stored / pending)
          {
            //in this mode response no PID is included
            pkt.bytesCount = linesInts[i][5] - 0x02; // remove 2 for MID & CRI
            pkt.respToPID = 0;//linesInts[i][4];
            MLMsgs.push_back(pkt);
            vector< unsigned int > dt;
            for ( int j = 8; j < (int)linesInts[i].size(); j++)
            {
              dt.push_back(linesInts[i][j]);
            }
            MLMsgsData.push_back(dt);
          }
          else
          {
            //cout << "\r\nDLC: " << hex << (int)linesInts[i][5] << endl;
            pkt.bytesCount = linesInts[i][5] - 0x03; // remove 3 for MID and PID and CRI
            //cout << "\r\nDLC: " << hex << (int)pkt.bytesCount << endl;
            pkt.respToPID = linesInts[i][7];
            MLMsgs.push_back(pkt);
            vector< unsigned int > dt;
            for ( int j = 9; j < (int)linesInts[i].size(); j++)
            {
              dt.push_back(linesInts[i][j]);
            }
            MLMsgsData.push_back(dt);
          }

        }
        else // this is a second or further line of response
        {
          //cout << "NEXTLINE!!\n";
          int currSrcAddr = linesInts[i][3];
          int currIndex = -1;
          for (int j = 0; j < (int)MLMsgs.size(); j++)
          {
            if ( (unsigned int)currSrcAddr == MLMsgs[j].srcAddr)
            {
              currIndex = j;
              //cout << "FOUND MATCHING ID AT INDEX " << j << endl;
              break;
            }
          }
          if (currIndex == -1)
          {
            return; // this is inacceptable situation that there's no matching packet for this line
          }
          for ( int j = 5; j < (int)linesInts[i].size(); j++)
          {
            MLMsgsData[currIndex].push_back(linesInts[i][j]);
            //cout << "Added: " << hex << (int)linesInts[i][j] << endl;
          }
        }
      }
        // second line here!
    }
    for (int i = 0; i < (int)MLMsgs.size(); i++)
    {
      int dupa = (int)MLMsgs[i].bytesCount;
      //cout << "\n\nPKT VECTOR SIZE: " << (int)MLMsgsData[i].size() << " dlc: " << dupa << " _\n\n";
      for (int j = 0; j < (int)MLMsgsData[i].size(); j++)
      {
        MLMsgs[i].bytes[j] = (unsigned char)MLMsgsData[i][j];
      }
      //cout << "ADD DATA ML________________________\r\n";
      OBD_RxBuffer.PutData( MLMsgs[i] );
    }
  }

  // If SAE / ISO91412 / KWP

  if ( (OBD_Elm327CurrentProtocol >= 0x01 && OBD_Elm327CurrentProtocol <= 0x05) ||
       (OBD_Elm327CurrentProtocol >= 0xA1 && OBD_Elm327CurrentProtocol <= 0xA5)
     )
  //________ SAE / ISO91412 / KWP MESSAGE __________________________________________
  {
    cout << "SAE / ISO91412 / KWP MSG\n";
    vector< TUsbOBDRxData > msgs; // multi-line messages
    vector< vector<unsigned int> > msgsData; // multi-line messages data bfr
    vector< bool > msgsIsFirstLine;
    int lloll = linesInts.size();
    for (int i = 0; i < (int)linesInts.size(); i++)
    {
      int lol = linesInts[i].size();

      // Each line must carry at least 5 bytes - P, TA, SA, MID, Checksum
      if( linesInts[i].size() > 4) // if it is a correct line...
      {
        TUsbOBDRxData pkt;
        // Third byte is a source address
        pkt.srcAddr = linesInts[i][2];
        // Fourth byte should be response mode
        pkt.respToMode = linesInts[i][3] - 0x40;

        if (pkt.respToMode == 0x04) // if this is a response to clear trouble codes
        {
          // This kind of response must be processed separately because it contains
          // no data and no PID
          pkt.bytesCount = 0;
          pkt.respToPID = 0;
        }
        else
        {
          if (  ( (pkt.respToMode == 0x03) || (pkt.respToMode == 0x07)) && (linesInts[i].size() >= 5)  ) // if this is trouble code response (stored / pending)
          {
            // This kind of response must be processed separately because it does not
            // carry PID information
            pkt.bytesCount = linesInts[i].size() - 0x05;
            pkt.respToPID = 0x00;
            for (int z = 4; z < (int)linesInts[i].size() - 1; z++)
            {
              pkt.bytes[z - 4] = linesInts[i][z];
            }
          }
          else
          {
            if ( linesInts[i].size() >= 6  ) // if this is standard message  P, TA, SA, MID, PID, (data), Checksum
            {
              pkt.bytesCount = linesInts[i].size() - 0x06;
              pkt.respToPID = linesInts[i][4];
              for (int z = 5; z < (int)linesInts[i].size() - 1; z++)
              {
                pkt.bytes[z - 5] = linesInts[i][z];
              }
            }
          }
        }
        //        cout << "ADD DATA SL________________________\r\n";

        // Check if this message is not a part of a multi-line response,
       //cout << "NEXTLINE!!\n";
       int currSrcAddr = pkt.srcAddr;
       int currIndex = -1;
       for (int j = 0; j < (int)msgs.size(); j++)
       {
         if (currSrcAddr == msgs[j].srcAddr)
         {
           currIndex = j;
           //cout << "FOUND MATCHING ID AT INDEX " << j << endl;
           break;
         }
       }
       if (currIndex == -1)
       {
         // There's no matching packet for this line, thus this is a single line response or a first line of a multi-line resp.
         // Packet should be added to the list

         msgs.push_back(pkt);
         vector< unsigned int > bytes;
         for (int x = 0; x < pkt.bytesCount; x++)
         {
           bytes.push_back(pkt.bytes[x]);
         }
         msgsData.push_back(bytes);
         msgsIsFirstLine.push_back(true);

       }
       else
       {
         // There was found a preceding part of this multi-line response

         // if this is a second line of a multi line response the first data byte from the first line must be removed
         // (in M-L messages the first data byte is the line number)
         if ( msgsIsFirstLine[currIndex] == true )
         {
            msgsIsFirstLine[currIndex] = false;
            msgsData[currIndex].erase(msgsData[currIndex].begin());
         }
         //int startingIndex = msgs[currIndex].bytesCount - 1;
         //msgs[currIndex].bytesCount += pkt.bytesCount;
         // ommit the first byte as it is a line number
         for (int k = 1; k < pkt.bytesCount; k++)
         {
           msgsData[currIndex].push_back( pkt.bytes[k] );
           /////
           //msgs[currIndex].bytes[ msgs[currIndex].bytesCount ] = pkt.bytes[i];
           //msgs[currIndex].bytesCount ++;
         }
        int x = 0;
       }

      }

    }

    for (int i = 0; i < (int)msgs.size(); i++)
    {
      //int dupa = (int)MLMsgs[i].bytesCount;
      //cout << "\n\nPKT VECTOR SIZE: " << (int)MLMsgsData[i].size() << " dlc: " << dupa << " _\n\n";
      //cout << "ADD DATA ML________________________\r\n";
      msgs[i].bytesCount = msgsData[i].size();
      for (int x = 0; x < msgs[i].bytesCount; x++)
      {
        msgs[i].bytes[x] = msgsData[i][x];
      }
      OBD_RxBuffer.PutData( msgs[i] );
    }

    int x = 0;
    int y = x;

  }

  //cout << "Lines count: " << linesCount << endl;
}

/// <summary>
/// Processes a temporary ELM327 Rx buffer, extracts complete responses (ended by '>')
/// and calls UsbOBDProcessSingleRawResponse for each complete response
/// </summary>
  void UsbOBDProcessTmpRxBuffer()
  {
    int currMsgPos = 0;
    int currMsgCharCount = 0;
    char chr;
    char msg[OBD_RX_BUFF_SIZE];

    int charsToSweepCount = OBD_RxTmpBuffer.GetBuffFill();
    char* bfrdmp = new char[charsToSweepCount];

    OBD_RxTmpBuffer.GetBfrDmp( bfrdmp );

    int charsToRemove = 0;
    for (int i = 0; i < charsToSweepCount; i++) // extract complete messages
    {
      chr = bfrdmp[i];
	  cout << "\"" << chr << "\"";

      if (chr != '>') // if this is not prompt character
      {
        msg[currMsgPos] = chr;
        currMsgPos++;
        currMsgCharCount++;
      }
      else // if this is prompt character
      {
        if (currMsgCharCount > 0)
        {
          msg[currMsgPos] = '\0';
          //cout << msg;
          UsbOBDProcessSingleRawResponse(msg, currMsgCharCount);
        }
        charsToRemove += currMsgCharCount + 1;
        currMsgPos = 0;
        currMsgCharCount = 0;
      }
    } // EO extract complete messages

    for (int j = 0; j < charsToRemove; j++)
    {
      OBD_RxTmpBuffer.RemoveFirst();
    }
  }

  /// <summary>
  /// Returns the OBD TX buffer fill
  /// </summary>
  UINT32 UsbOBDGetDevTxBuffFill()
  {
    return OBD_TxBuffer.GetBuffFill();
  }

  /// <summary>
  /// Returns the OBD TX buffer size
  /// </summary>
  UINT32 UsbOBDGetDevTxBuffSize()
  {
    return OBD_RX_BUFF_SIZE;
  }

  /// <summary>
  /// Returns the OBD RX buffer fill
  /// </summary>
  UINT32 UsbOBDGetDevRxBuffFill()
  {
    return OBD_RxBuffer.GetBuffFill();
  }

  /// <summary>
  /// Returns the OBD RX buffer size
  /// </summary>
  UINT32 UsbOBDGetDevRxBuffSize()
  {
    return OBD_RX_BUFF_SIZE;
  }

  /// <summary>
  /// Sends a command to ELM327
  /// This is only for ELM327 initialization purposes !!!
  /// SHOULD NOT BE USED OUTSIDE THE LIBRARY !!!
  /// Params:
  /// char *cmd :    command to send. Must be null terminated
  /// </summary>
  void UsbOBDInitOnlySendCmd( char *cmd )
  {
    TUsbOBDTxData data;
    int cmdLen = 0;
    char *cmdPtr = cmd;
    while ( *cmdPtr != '\0' )
    {
      data.chars[cmdLen] = *cmdPtr ;
      cmdLen++;
      cmdPtr++;
    }
    data.charCount = (unsigned char)cmdLen;
//     for (int i = 0; i < data.charCount; i++)
//     {
//       cout << data.chars[i];
//     }
    if ( OBD_TX_BUFF_SIZE - OBD_TxBuffer.GetBuffFill() > 0)
    {
      OBD_TxBuffer.PutData( data );
    }
  }

  /// <summary>
  /// Sends a command to ELM327
  /// Params:
  /// char *cmd :    command to send. Must be null terminated
  /// </summary>
  void UsbOBDSendCmd( char *cmd )
  {
    // Make sure the buffer is not filled until the ELM327 is properly initialized and connected
    if ( OBD_Elm327State != ELM327STATE_READY_TO_TALK )
    {
      return;
    }
    TUsbOBDTxData data;
    int cmdLen = 0;
    char *cmdPtr = cmd;
    while ( *cmdPtr != '\0' )
    {
      data.chars[cmdLen] = *cmdPtr ;
      cmdLen++;
      cmdPtr++;
    }
    data.charCount = (unsigned char)cmdLen;
//     for (int i = 0; i < data.charCount; i++)
//     {
//       cout << data.chars[i];
//     }
    if ( OBD_TX_BUFF_SIZE - OBD_TxBuffer.GetBuffFill() > 0)
    {
      OBD_TxBuffer.PutData( data );
    }
  }

  /// <summary>
  /// Copies a commands set to ELM327 transmit buffer
  /// Params:
  /// TUsbOBDTxData * data :    Array of commands (TUsbOBDTxData structures )
  /// UINT32 num :              Commands to copy count
  /// UINT32 * wrNum :          Elements copied count (returned value)
  /// </summary>
  void UsbOBDWriteData( TUsbOBDTxData * data, UINT32 num, UINT32 * wrNum )
  {
    //cout << " \n\nBUFF FILL before write: " << OBD_TxBuffer.GetBuffFill();
    // Make sure the buffer is not filled until the ELM327 is properly initialized and connected
    if ( OBD_Elm327State != ELM327STATE_READY_TO_TALK )
    {
      *wrNum = 0;
      cout << "NOT READY TO TALK";
      return;
    }
    UINT32 framesToWrite = OBD_TX_BUFF_SIZE - OBD_TxBuffer.GetBuffFill();
    if (framesToWrite > num)
      framesToWrite = num;
    for (UINT32 i = 0; i < framesToWrite; i++)
    {
      OBD_TxBuffer.PutData( data[i] );
    }
    *wrNum = framesToWrite;
    //cout << "\n BUFF FILL after write: " << OBD_TxBuffer.GetBuffFill() << endl;
  }

  /// <summary>
  /// Copies a received pre-processed messages set from ELM327 receive buffer
  /// Params:
  /// TUsbOBDRxData * data :    Array of messages to write to (TUsbOBDRxData structures )
  /// UINT32 num :              Commands to copy max count
  /// UINT32 * wrNum :          Elements copied count(returned value)
  /// </summary>
  void UsbOBDReadData( TUsbOBDRxData * data, UINT32 num, UINT32 * rdNum )
  {
    UINT32 framesToWrite = OBD_RxBuffer.GetBuffFill();
    if (framesToWrite > num)
      framesToWrite = num;
    for (UINT32 i = 0; i < framesToWrite; i++)
    {
      OBD_RxBuffer.GetData( &data[i] );
      //cout << "PKT BYTE COUNT: " << (int)data[i].bytesCount << " // " << (int) data[i].respToMode << (int) data[i].respToPID << " // ";
      for (int v = 0; v < data[i].bytesCount; v++)
      {
        //cout << (int)data[i].bytes[v] << " ";
      }
      //cout << endl;
    }
    *rdNum = framesToWrite;
  }



}