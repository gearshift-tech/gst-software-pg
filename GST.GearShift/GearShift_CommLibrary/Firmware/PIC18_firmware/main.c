#include "p18f44k20.h"
//#include "GenericTypeDefs.h"
//#include <string.h>
//#include <math.h>
//#include <stdlib.h>
#include "i2c.h"
#include "lcd.h"
#include "bargraphs.h"




#pragma code

/*------------------------------------------------------*/
/*				Variables and consts				    */
/*------------------------------------------------------*/
char stringN[30] = "";
#define N 20
/*-------------- Variables and consts end --------------*/


/*------------------------------------------------------*/
/*				My functions						    */
/*------------------------------------------------------*/
////////////////////
// my_strcat start
////////////////////
void my_strcat(char s[], char x)
{
	unsigned char i = 0;
	while (s[i] != '\0')
		i++;
	s[i] = x;
	s[i+1] = '\0';
}
// my_strcat end

////////////////////
// my_strcpy start
////////////////////
void my_strcpy(char* dest, const rom char* src)
{
	while ((*dest++ = *src++) != '\0');
}
// my_strcpy end
/*-------------- My functions end -----------------------*/


/*------------------------------------------------------*/
/*				  Interrupts						    */
/*------------------------------------------------------*/
void InterruptHandlerHigh (void);
void InterruptHandlerLow (void);
/*-------------- Interupts end -----------------------*/


/////////////////////////////
//	Initialize ports start
/////////////////////////////
void InitPorts(void) 
{
	TRISA &= 0x00;
	LATA &= 0x00;

	TRISB &= 0xC0;
	LATB &= 0xC0;

	TRISC &= 0x18;
	LATC &= 0x18; // trzeba zmienic ze wzgledu na i2c - scl, sda - inputs 

	TRISD &= 0x00;
	LATD &= 0x00;

	TRISE &= 0xF8;
	LATE &= 0xF8;
	ANSEL = 0x00;
	ANSELH = 0x00;
	//ADCON1bits.PCFG3 = 1;
	//ADCON1bits.PCFG2 = 1;	
	//ADCON1bits.PCFG1 = 1;
	//ADCON1bits.PCFG0 = 1;
}
//	Initialize ports end

/*------------------------------------------------------*/
/*						  Main				            */
/*------------------------------------------------------*/
void main(void)
{	
	unsigned char i;

	// Oscillator config
	OSCCONbits.SCS1 = 1;
	OSCCONbits.SCS0 = 1;
	OSCCONbits.IRCF0 = 0;
	OSCCONbits.IRCF1 = 1;
	OSCCONbits.IRCF2 = 1;

	// Disable WatchDog Timer
	WDTCONbits.SWDTEN = 0;
	
	// Interrupts settings
	RCONbits.IPEN = 1;
	INTCONbits.GIEH = 1;
	INTCONbits.GIEL = 1;

	InitPorts();

	lcd_init();	
	i2c_init();
	barDisp_timeInit();
			
   //clear lcd
   lcd_clear();
   // Display a initial message
   lcdRow2Buff[0] = ' ';
   lcdRow2Buff[1] = ' ';
   lcdRow2Buff[2] = ' ';
   lcdRow2Buff[3] = 'B';
   lcdRow2Buff[4] = 'O';
   lcdRow2Buff[5] = 'O';
   lcdRow2Buff[6] = 'T';
   lcdRow2Buff[7] = 'I';
   lcdRow2Buff[8] = 'N';
   lcdRow2Buff[9] = 'G';
   lcdRow2Buff[10] = '.';
   lcdRow2Buff[11] = '.';
   lcdRow2Buff[12] = '.';
   lcdRow2Buff[13] = ' ';
   lcdRow2Buff[14] = ' ';
   lcdRow2Buff[15] = ' ';
   lcdRow2Buff[16] = '\0';
   lcdDispRow2 = 1;
   //turn off all bargraphs
	for (i=0; i<9; i++)
		barDisp_updatedispLevel(i ,0);
	
	//barDisp_updatedispLevel(1 ,10);

while (0)
{
	stringN[0] = 'W';
	stringN[1] = 'o';
	stringN[2] = 'r';
	stringN[3] = 'k';
	stringN[4] = 's';
	stringN[5] = '?';
	stringN[6] = '\0';
	lcd_sendMessage(stringN);
}

while (1)
{
   if (lcdDispRow1)
   {
      lcdDispRow1 = 0; 
      while (!wait);
      lcd_retHome();
      while (!wait);
      lcd_sendMessage((char*)lcdRow1Buff);
         
   }
   
   if (lcdDispRow2)
   {
      lcdDispRow2 = 0;  
      while (!wait);
      lcd_retHome();
      while (!wait);
      lcd_nextLine();
      while (!wait);
      lcd_sendMessage(lcdRow2Buff); 
   }     
}

}
/*-------------- Main end ----------------------*/



/*------------------------------------------------------*/
/*				  Interrupts						    */
/*------------------------------------------------------*/
//////////////////////////////////
// Low priority interrupt vector
//////////////////////////////////
#pragma code InterruptVectorLow = 0x18
void InterruptVectorLow (void)
{
  _asm
    goto InterruptHandlerLow //jump to interrupt routine
  _endasm
}
//	Low priority interrupt vector end

//////////////////////////////////
// Low priority interrupt routine
//////////////////////////////////
#pragma code
#pragma interruptlow InterruptHandlerLow

void InterruptHandlerLow ()
{
/////////////////////////////
//	Timer0 interrupts start
/////////////////////////////
	if (INTCONbits.TMR0IF)
	{ 
		T0CONbits.TMR0ON = 0;
		TMR0L = 193;
		wait = 1;
		timerTime--;
		if (timerTime != 0)
		{
			wait = 0;
			T0CONbits.TMR0ON = 1;
		}   
		INTCONbits.TMR0IF = 0;            //clear interrupt flag
    }
//	Timer0 interrupts end
}
// Low priority interrupt routine end

//////////////////////////////////
// High priority interrupt vector
//////////////////////////////////
#pragma code InterruptVectorHigh = 0x08
void InterruptVectorHigh (void)
{
  _asm
    goto InterruptHandlerHigh //jump to interrupt routine
  _endasm
}
//	High priority interrupt vector end

//////////////////////////////////
// High priority interrupt routine
//////////////////////////////////
#pragma code
#pragma interrupt InterruptHandlerHigh

void InterruptHandlerHigh ()
{
	char str[20] = "";
/////////////////////////////
//	Timer1 interrupts start
/////////////////////////////
	if (PIR1bits.TMR1IF)
	{ 
		T1CONbits.TMR1ON = 0;
		TMR1H = 0xFF;
		TMR1L = 180;
 		//LATAbits.LATA3^= 1;
 		
		barDisp_dispLevel();
		
		PIR1bits.TMR1IF = 0;            //clear interrupt flag
		T1CONbits.TMR1ON = 1;
    }
//	Timer1 interrupts end

/////////////////////////////
//	I2C interrupts start
/////////////////////////////
	if (PIR1bits.SSPIF)
	{
 	//barDisp_updatedispLevel(8, 6);
		if (SSPCON1bits.SSPOV == 1)
		{
			//barDisp_updatedispLevel(7, 8);
			SSPCON1bits.SSPOV = 0;
			while (!SSPSTATbits.BF);
			Data = SSPBUF;
		}
		else
		{
			while (!SSPSTATbits.BF);
			//barDisp_updatedispLevel(6, 10);
			Data = SSPBUF;
			if ((SSPSTATbits.R_W == 0) && (SSPSTATbits.D_A == 0))
			   DataNumber = 0;
			if ((SSPSTATbits.R_W == 0) && (SSPSTATbits.D_A == 1))
			{
				//barDisp_updatedispLevel(4, 15);
				
				i2c_reception(DataNumber, Data);
				
				DataNumber++;
			}
			//if (DataNumber == 2)
			//	DataNumber = 0;
		}
		PIR1bits.SSPIF = 0;
		SSPCON1bits.CKP = 1;
	}
//	I2C interrupts end
}
// High priority interrupt routine end

/*-------------- Interupts end -----------------------*/