//#include "UART2_console.h"
#include "p24fxxxx.h"


//
// UART2 character transmit routine
//
void my_putc( char c )
{
  //u1putc(c);
  //return;
	while( !U2STAbits.TRMT );
	U2TXREG = c;
}

void u1putc( char c )
{
  //return;
	while( !U1STAbits.TRMT );
	U1TXREG = c;
}


//
// UART2 initialization routine
//
/*
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
*/

