using System.ComponentModel;


namespace GST.ZF6.Components.Interfaces.MechShifterUSB
{

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
    CMD_KLINE_SET_EDS = 0x33          // Change drive values of EDS 5&6 solenoids
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