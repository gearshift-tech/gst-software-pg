#include "p24fxxxx.h"
#include "device_init.h"
#include "main.h"
#include "UsbSoftLayer.h"
#include "pwm.h"
#include "current_adc.h"
#include "overcurrent.h"
#include "DAQ.h"
#include "i2c.h"

//
// Initializes the memory
//
void initMemory( void ) 
{
   unsigned char i;
   for( i = 0; i < 14; i++ ) 
   {
      intADBuff[ i ] = 0;
   }
   
   for( i = 0; i < PWM_DRV_MAX_COUNT; i++ )
   {
     workData.DAQ_PWMManualDutyCycles[ i ] = 0;
     workData.DAQ_PWMManualDriveEnabled[ i ] = 0;
   }
   
   DAQ_appState = APP_STATE_WAIT_FOR_INIT;
   usbStateVal = USB_STATE_DETACHED;
   DAQ_errCode = APP_ERR_NONE;

   adFinished = 0;
   usbOutHandle = 0;
   usbInHandle = 0;
}

void InitUART2(void)
{
	// configure U2MODE
	
	U2BRG = 25;
	U2STA = 0x0000;
	U2MODE = 0x8000;
	// Clear the Transmit Interrupt Flag
	IFS1bits.U2TXIF = 0;
	
	// Enable Transmit Interrupts
	IEC1bits.U2TXIE = 0;
	
	// Clear the Recieve Interrupt Flag	
	IFS1bits.U2RXIF = 0;
	
	// receive interrupt mode set to int when any character is received
  //U2STAbits.URXISEL = 0x0;	
	
	// Enable Recieve Interrupts
	IEC1bits.U2RXIE = 1;
	
	// And turn the peripheral on
	//U2MODEbits.UARTEN = 1;

	// Enable transmitter
	U2STAbits.UTXEN = 1;
}

void InitUART1(void)
{
	// configure U2MODE
	
	U1BRG = 25;
	U1STA = 0x0000;
	U1MODE = 0x8000;
	// Clear the Transmit Interrupt Flag
	IFS0bits.U1TXIF = 0;
	
	// Enable Transmit Interrupts
	IEC0bits.U1TXIE = 0;
	
	// Clear the Recieve Interrupt Flag	
	IFS0bits.U1RXIF = 0;
	
	// receive interrupt mode set to int when any character is received
  //U2STAbits.URXISEL = 0x0;	
	
	// Enable Recieve Interrupts
	IEC1bits.U2RXIE = 1;
	
	// And turn the peripheral on
	//U1MODEbits.UARTEN = 1;

	// Enable transmitter
	U1STAbits.UTXEN = 1;
}

//
// Initial device full initialization
//
void deviceInit( void )
{
    RCONbits.SWDTEN = 0;

    initMemory();
    InitPeripheralPins();
    InitAD();

    InitPorts();
    pwmInit();

    i2c_init(I2Cnr3, (unsigned int)FSCL);

    OVC_Init();

    USBDeviceInit();
    currentADCInit();
    DAQ_Init();


    InitUART2();
    //InitUART1();
}   

//
// I/O ports initialization routine
//
void InitPorts( void )
{
    // Unlock registers
    __builtin_write_OSCCONL(OSCCON & 0xbf);

    TRISB = 0xFFFF;
    TRISD = 0xFFFF;
    TRISE = 0xFFFF;
    TRISF = 0xFFFF;
    TRISG = 0xFFFF;
    TRISD &= ~( _bSPI1_CLK | _bDRV9 | _bDRV8 | _bDRV7 | _bDRV6 | _bDRV5 | _bADC_nCS1 |
                _bADC_nCS2 | _bTxD  | _bSPI1_MOSI );
    TRISE &= ~( _bOCFP );
    TRISF &= ~( _bLED2 | _bLED3 | _bCAN_nCS | _bSPI2_CLK | _bSPI2_MOSI );
    TRISG &= ~( _bAO_nCS | _bCAN_TRM_nEN | _bDRV1 | _bDRV2 | _bDRV3 | _bDRV4 );

    __builtin_write_OSCCONL(OSCCON | 0x40);
}

//
// I/O pins mapping routine
//
void InitPeripheralPins( void )
{
    // Unlock registers
    __builtin_write_OSCCONL(OSCCON & 0xbf);

    // Configure Input Functions **********************

    // UART 2 inputs
    RPINR19bits.U2RXR = _RxD;
    RPINR19bits.U2CTSR = _CTS;

    // SPI interface 1 and 2 inputs
    RPINR20bits.SDI1R = _SPI1_MISO;
    RPINR22bits.SDI2R = _SPI2_MISO;

    // External interrupt
    RPINR0bits.INT1R = _MCP2515_RX0BF;
    RPINR2bits.INT4R = _MCP2515_RX1BF;


    RPINR1bits.INT2R = _EOC1;
    RPINR1bits.INT3R = _EOC2;
    RPINR11bits.OCFAR = _OCF;
    RPINR11bits.OCFBR = _OCF;

    // Configure Output Functions *********************

    // UART 2 output
    RPOR1bits.RP2R = IO_TxD;
    RPOR2bits.RP4R = IO_nRTS;


    // TLV1543 ADC SPI interface
    RPOR5bits.RP11R = IO_SPI1_CLK;
    RPOR1bits.RP3R = IO_SPI1_MOSI;
    // MCP2515 SPI interface
    RPOR5bits.RP10R = IO_SPI2_CLK;
    RPOR8bits.RP17R = IO_SPI2_MOSI;

    // PWM outputs
    RPOR9bits.RP19R  = IO_DRV1;
    RPOR10bits.RP20R = IO_DRV9;
    RPOR10bits.RP21R = IO_DRV3;
    RPOR11bits.RP22R = IO_DRV7;
    RPOR11bits.RP23R = IO_DRV6;
    RPOR12bits.RP24R = IO_DRV5;
    RPOR12bits.RP25R = IO_DRV8;
    RPOR13bits.RP26R = IO_DRV2;
    RPOR13bits.RP27R = IO_DRV4;

    // Lock Registers
    __builtin_write_OSCCONL( OSCCON | 0x40 );
}

