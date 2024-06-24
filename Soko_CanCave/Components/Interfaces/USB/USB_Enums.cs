using System.ComponentModel;


namespace Soko.CanCave.Components.Interfaces.CanCaveUsb
{

  /// <summary>
  /// Zf6 gearbox model 
  /// </summary>
  public enum GearboxControllerType
  {
    [Description("Non Mechatronic")]
    NON_MECHATRONIC = 0,

    [Description("ZF 6HPxx C_E")]
    ZF_6HPxx_CE = 1,
    [Description("ZF 6HPxx C_M")]
    ZF_6HPxx_CM = 2,
    [Description("ZF 6HPxx 1911_E")]
    ZF_6HPxx_1911E = 3,
    [Description("ZF 6HPxx 1911_M")]
    ZF_6HPxx_1911M = 4,
    [Description("ZF 6HPxx TU_CE")]
    ZF_6HPxx_TUCE = 5,
    [Description("ZF 6HPxx TU_CM")]
    ZF_6HPxx_TUCM = 6,
    [Description("ZF 6HPxx WM")]
    ZF_6HPxx_WM = 11,

    // DO NOT USE NUMBERS 12-30 AS THEY ARE RESERVED!!

    [Description("Nissan RE5")]
    NISSAN_RE5 = 30
  };


  /// <summary>
  /// Enum denoting the USB packet command
  /// </summary>
  public enum USBPacketCommandCode : byte
  {
    CMD_POLL_DATA = 0x01,             // Poll data packet that carries no data but is sent to keep the connection alive 
    CMD_GET_DEV_INFO = 0x02,          // Device info request
    CMD_GET_DEV_STATUS = 0x03,	      // Device status request
    CMD_USB_COMM_START = 0x04,        // Host initialized and ready to receive application specific data
    CMD_USB_COMM_STOP = 0x05,         // Stop USB communication
    CMD_SET_SERIAL = 0x0E,	          // Set the device's serial number and GUID
    CMD_BLD_ENTER = 0x0F,             // Enter bootloader request

    CMD_UI_UPDATE = 0x11,             // Update user interface request

    CMD_CAN_DISABLE_COMM = 0xA0,
    CMD_CAN_GET_CONFIG = 0xA2,
    CMD_CAN_SET_CONFIG = 0xA3,
    CMD_CAN_ADD_NODES = 0xA4,
    CMD_CAN_DATA = 0xA6,
    CMD_CAN_GET_STATE = 0xA9,
    CMD_CAN_EN_PULLUP = 0xAA,
    CMD_CAN_DSBL_PULLUP = 0xAB,
    CMD_CAN_RST_FXD_TRC = 0xAC,
    CMD_CAN_UPDATE_NODE_1 = 0xAD,
    CMD_CAN_UPDATE_NODE_2 = 0xAE,
    CMD_CAN_UPDATE_NODE_3 = 0xAF,


    CMD_KLINE_TXDATA = 0x21,          // K-Line TX data
    CMD_KLINE_RXDATA = 0x22,          // K-Line RX data
    CMD_KLINE_INIT = 0x23,            // Initialize K-Line request
    CMD_RESET_TMR = 0x24,             // Reset timestamp timer request
    CMD_KLINE_ENABLE = 0x25,          // Enable K-Line interface
    CMD_KLINE_DISABLE = 0x26,         // Disable K-Line interface
    CMD_KLINE_ENABLE_MDFRAME = 0x27,  // Enable MDFrame functionality
    CMD_KLINE_DISABLE_MDFRAME = 0x28, // Disable MDFrame functionality
    CMD_KLINE_SLCT_GBX = 0x29,        // Select gearbox type
    CMD_KLINE_ENABLE_DRIVE = 0x30,    // Start driving the gearbox
    CMD_KLINE_DISABLE_DRIVE = 0x31,   // Stop driving the gearbox
    CMD_KLINE_SLCT_GEAR = 0x32,       // Change selected gear
    CMD_KLINE_SET_EDS = 0x33,          // Change drive values of EDS 5&6 solenoids

    CMD_SSEMU_SetFrequencies = 0x40,
    CMD_TCUGOV_SelectNissanRE5Mode = 0x41,
    CMD_TCUGOV_SelectGM6TMode = 0x42
  };

  /// <summary>
  /// Enum denoting the type of output packet
  /// </summary>
  public enum OUT_EVENT_TYPE : byte
  {
    KLINE_TX = 0x01,              // Out packet carries KLINE data
    PHERIPHERIALS_EVT = 0x02      // Out packet carries pheriperials data
  };

  /// <summary>
  /// Enum denoting the type of pheriperial action to be taken
  /// </summary>
  public enum PHP_EVENT_TYPE : byte
  {
    DO_NOT_CHANGE = 0x00,         // Pheriperial state should not be changed
    TURN_OFF = 0x01,              // Pheriperial should be disabled
    TURN_ON = 0x02                // Pheriperial should be enabled
  };

  /// <summary>
  /// Enum denoting yes/no coding
  /// </summary>
  public enum YESNOENUM : byte
  {
    NO = 0x00,                    // Pheriperial state should not be changed
    YES = 0x01                    // Pheriperial should be disabled
  };

  public enum GearboxConnectionStatus
  {
    [Description("Idle")]
    Idle = 0,
    [Description("Connecting")]
    Connecting = 1,
    [Description("Connected")]
    Connected = 2
  }
}