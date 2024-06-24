//#include "UART2_console.h"
#include "p24fxxxx.h"
#include "main.h"
#include "debug.h"
//#include "uart_232.h"


//
// pressure A/D converter initialization
//
void InitAD( void )
{
   AD1CON1bits.ADON = 0;
   AD1CON1bits.ADSIDL = 0;
   AD1CON1bits.FORM = 0;
   AD1CON1bits.SSRC = 0x07;
   AD1CON1bits.ASAM = 0;
   AD1CON1bits.SAMP = 0;
   
   AD1CON2bits.VCFG = 1;
   AD1CON2bits.CSCNA = 1;
   AD1CON2bits.SMPI = 15;
   AD1CON2bits.BUFM = 0;
   AD1CON2bits.ALTS = 0;
   
   AD1CON3bits.ADRC = 0;
   AD1CON3bits.SAMC = 8;
   AD1CON3bits.ADCS = 10;
   
   AD1CHSbits.CH0NA = 0;
   AD1CHSbits.CH0SA = 0;
   AD1PCFG = 0x0000;
   AD1CSSL = 0xFF3F;
   IFS0bits.AD1IF = 0;
   
   IEC0bits.AD1IE = 1;
   AD1CON1bits.ADON = 1;  
}

//
// Starts pressure readout routine
//
void startIntAD( void )
{
   AD1CON1bits.ASAM = 1;
}

//
// ADC1 interrupt routine ( pressure )
//
void __attribute__ ((interrupt, no_auto_psv)) _ADC1Interrupt( void )
{
   unsigned short * pVal;
   unsigned short * pBuff;
   unsigned short i;

   intADBuff[ 5 ] += ADC1BUF0;
   intADBuff[ 4 ] += ADC1BUF1;
   intADBuff[ 3 ] += ADC1BUF2;
   intADBuff[ 2 ] += ADC1BUF3;
   intADBuff[ 1 ] += ADC1BUF4;
   intADBuff[ 0 ] += ADC1BUF5;
   intADBuff[ 6 ] += ADC1BUF6;
   intADBuff[ 7 ] += ADC1BUF7;
   intADBuff[ 8 ] += ADC1BUF8;
   intADBuff[ 9 ] += ADC1BUF9;
   intADBuff[ 10 ] += ADC1BUFA;
   intADBuff[ 11 ] += ADC1BUFB;
   intADBuff[ 12 ] += ADC1BUFC;
   intADBuff[ 13 ] += ADC1BUFD;

   intADCount++;
   if( intADCount == 8 ) 
   {
      AD1CON1bits.ASAM = 0;
      intADCount = 0;
      pVal = intADValues;
      pBuff = intADBuff;
      for( i = 0; i < 14; i++ ) 
      {
         *pVal++ = *pBuff >> 3;
         *pBuff++ = 0;
      }
      adFinished = 1;
   }
   IFS0bits.AD1IF = 0;
}
