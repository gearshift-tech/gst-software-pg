#define __I2C_C

#include "i2c.h"
#include "bargraphs.h"
#include "lcd.h"
#include "p18f44k20.h"

unsigned char SlaveAddress = 0x02;
unsigned char DataNumber = 0;
unsigned char Data;

volatile char lcdRow1Buff[17];
volatile char lcdRow2Buff[17];
volatile unsigned char lcdDispRow1 = 0;
volatile unsigned char lcdDispRow2 = 0;

unsigned char CurrRxCmd = 0x0;

void i2c_reception(unsigned char number, unsigned char data)
{
   if ( number == 0) // if command selection byte was sent
   {
      CurrRxCmd = data;
      return;
   }
   else // if data byte was sent (after cmd selection)
   {             
      switch (CurrRxCmd)
      {
         case RxCmdBgVals:
         {  
            if (number <= 9)
               barDisp_updatedispLevel(9 - number, data);
            break;
         }   
         case RxCmdLcdRow1:
         {
            if (number < 16)
            {
               lcdRow1Buff[number-1] = data;
            }   
            else
            {
               lcdRow1Buff[15] = data;
               lcdRow1Buff[16] = '\0';
               lcdDispRow1 = 1;
            }   
            break;
         }
         case RxCmdLcdRow2:
         {
            if (number < 16)
            {
               lcdRow2Buff[number-1] = data;
            }   
            else
            {
               lcdRow2Buff[15] = data;
               lcdRow2Buff[16] = '\0';
               lcdDispRow2 = 1;
            }   
            break;
         }         
         default:
         {
            break;
         }   
      }
         
   }       
}
void i2c_enable(unsigned char enable)
{
	SSPCON1bits.SSPEN = enable; //Master Synchronous Serial Port Enable bit
}

void i2c_init(void)
{
	IPR1bits.SSPIP = 1; //Master Synchronous Serial Port Interrupt Priority bit
	PIR1bits.SSPIF = 0; //Master Synchronous Serial Port Interrupt Flag bit

	SSPCON1bits.SSPM3 =	1; //Master Synchronous Serial Port Mode Select bits
	SSPCON1bits.SSPM2 =	0;
	SSPCON1bits.SSPM1 =	0;
	SSPCON1bits.SSPM0 =	1;
	SSPADD = 0xF0;	//Master Synchronous Serial Port Address Register
	
	SSPCON1bits.SSPM3 =	0; //Master Synchronous Serial Port Mode Select bits
	SSPCON1bits.SSPM2 =	1;
	SSPCON1bits.SSPM1 =	1;
	SSPCON1bits.SSPM0 =	0;	
	SSPCON1bits.SSPEN = 0; //Master Synchronous Serial Port Enable bit
	
	SSPADD = SlaveAddress;	//Master Synchronous Serial Port Address Register
	SSPCON1bits.CKP = 1; //SCK Release Control bit
	
	SSPCON2bits.GCEN = 0; //General Call Enable bit
	SSPCON2bits.SEN = 1; //Stretch Enable bit
	SSPCON1bits.SSPOV = 0;
	Data = SSPBUF;
	PIE1bits.SSPIE = 1; // Master Synchronous Serial Port Interrupt Enable bit
	i2c_enable(1);
}