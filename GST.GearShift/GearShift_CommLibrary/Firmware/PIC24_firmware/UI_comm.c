#ifndef _UI_COMM_C_
#define _UI_COMM_C_

// includes
#include "i2c.h"
#include "UI_comm.h"
#include "main.h"

#include <string.h>

// buffers
volatile unsigned char UI_bgVals[10];      // array of 9 bargraph values (0 - 20)
volatile char UI_lcdRow1[18];    // buffer for the first row of LCD display
volatile char UI_lcdRow2[18];    // buffer for the second row of LCD display

// flags
volatile unsigned char UI_updateBgs = 0;  // flag if bargraphs should be updated via i2c
volatile unsigned char UI_bgDisplayCurrent = 0; // flag if bargraphs should display currents (1) or PWM values (0)
volatile unsigned char UI_updateLcdRow1 = 0;  // flag if 1st lcd row should be updated via i2c
volatile unsigned char UI_updateLcdRow2 = 0;  // flag if 2nd lcd row should be updated via i2c
volatile unsigned char UI_i2cCommErr = 0; // flag if UI communication error occured

void UI_UpdateLcdRow1( char *str )
{
   str[ UI_LCD_W] = '\0';
   strcpy ( (char*)(UI_lcdRow1 + 1), str);
   UI_lcdRow1[0] = UI_i2c_CmdLcdRow1;
   UI_updateLcdRow1 = 1; 
}

void UI_UpdateLcdRow2( char * str )
{
   str[ UI_LCD_W] = '\0';
   strcpy ( (char*)(UI_lcdRow2 + 1), str);
   UI_lcdRow2[0] = UI_i2c_CmdLcdRow2;   
   UI_updateLcdRow2 = 1;   
}

void UI_UpdateBgs( void )
{
   unsigned char i;
   unsigned char index;
   if ( UI_bgDisplayCurrent ) // if current to be displayed
   {
      for (i = 0; i < 9; i++)
      {
         UI_bgVals[i+1] = 0;
      } 
      for (i = 0; i < workData.currReadChannsCount; i++)
      {
         index = workData.currReadChannsIndices[i];
         if (index > 8)
            index -= 9;
         UI_bgVals[index + 1] = ( 250 + workData.ADCReadOutValues[ workData.currReadChannsIndices[i] ] * 20 ) >> 9 ; // /512
      }        
             
      UI_updateBgs = 1;
   }
   else // if DutyCycles to be displayed
   {
      for (i = 0; i < 9; i++)
      {
         UI_bgVals[i+1] = workData.DAQ_PWMDutyCycles[ i ] / 5;
      }   
      UI_updateBgs = 1;      
   } 
   UI_bgVals[0] = UI_i2c_CmdBgVals;  
}         


/////////////////////////////
//	I2C 3 interrupts start
/////////////////////////////
void __attribute__((interrupt, no_auto_psv)) _MI2C3Interrupt(void)
{
	int i;
	while((I2C3CONbits.SEN) || (I2C3CONbits.RSEN) || (I2C3CONbits.PEN) || (I2C3CONbits.RCEN) || (I2C3CONbits.ACKEN));
	//uart_puts(UARTnr1,"0\t");
	// Generating Start Bus Event Interrupt
	if (((I2C3STATbits.S == 1) && (start ==1)) || (flagR==2))
	{
		for (i=0;i<150;i++);
		//uart_puts(UARTnr1,"1\t");
		// Sending Slave Device Address
		I2C3TRN = (SlaveAddress << 1) | flag;
		if (flagR==2)
			flagR=0;
		start = 0;
	}
	// Receiving Data from a Slave Device
	else if ((I2C3STATbits.P == 0) && (I2C3STATbits.RBF == 1) && (flag==1) )
	{
		//uart_puts(UARTnr1,"2\t");
		readData = I2C3RCV;
		dataReadFrame[dataCounter] = readData;
		//uart_putc(UARTnr1,readData);
		//uart_puts(UARTnr1,"\n\r");
		dataCounter++;
		while(I2C3STATbits.RBF);
		// Acknowledge Generation
		if (dataCounter == dataCount)
		{	
			//uart_puts(UARTnr1,"NACK\t");
			I2C3CONbits.ACKDT = 1;
			I2C3CONbits.ACKEN = 1;
		}
		else
		{
			//uart_puts(UARTnr1,"ACK\t");
			I2C3CONbits.ACKDT = 0;
			I2C3CONbits.ACKEN = 1;
		}
					
	}
	// Acknowledge Generation - interrupt after sending ACK
	// Slave Acknowledge Interrupt after sending Slave Device Address
	else if ((I2C3STATbits.P == 0) && (I2C3STATbits.ACKSTAT == 0) && (dataCounter < dataCount) && (flag==1))
	{
		while(I2C3STATbits.TRSTAT);
		//uart_puts(UARTnr1,"3\t");
		//uart_puts(UARTnr1,"ACK read slave\t");
		// Enable Receive
		I2C3CONbits.RCEN = 1;
	}

	// Slave Acknowledge Interrupt
	else if ((I2C3STATbits.P == 0) && (I2C3STATbits.ACKSTAT == 0) && (flag==0))
	{
		//uart_puts(UARTnr1,"4\t");
		if (dataCounter == dataCount)
		{	
			while((I2C3CONbits.SEN) || (I2C3CONbits.RSEN) || (I2C3CONbits.PEN) || (I2C3CONbits.RCEN) || (I2C3CONbits.ACKEN));
			// Generating Repeated Start Bus Event
			if (flagR==1)
			{
				I2C3CONbits.RSEN = 1;
				flagR = 2;
				flag = 1;
				dataCounter = 0;
				dataCount = dataCount1;	
			}
			// Generating Stop Bus Event	
			else
				I2C3CONbits.PEN = 1;
		}
		else
		{
			while(I2C3STATbits.TRSTAT);
			// Sending Data to a Slave Device
			I2C3TRN = dataWriteFrame[dataCounter];
			dataCounter++;
		}					
	}
	// Generating Stop Bus Event Interrupt
	else if (I2C3STATbits.P == 1)
	{
		//uart_puts(UARTnr1,"5\t");
		flagR = 0;
		if (flag==1)
			flagDataReady = 1;
		else if (flagDataNACK == 0)
			flagDataSent = 1;
	}
	// RECEIVING ACKNOWLEDGE FROM THE SLAVE Interrupt - NACK 
	else if (I2C3STATbits.ACKSTAT == 1)
	{
		//uart_puts(UARTnr1,"6\t");
		flagDataNACK = 1;
		while((I2C3CONbits.SEN) || (I2C3CONbits.RSEN) || (I2C3CONbits.PEN) || (I2C3CONbits.RCEN) || (I2C3CONbits.ACKEN));
		// Generating Stop Bus Event
		I2C3CONbits.PEN = 1;
	} 
	// Acknowledge Generation - interrupt after sending NACK
	else if ((dataCounter == dataCount) && (flag==1))
	{	
		//uart_puts(UARTnr1,"7\t");
		while((I2C3CONbits.SEN) || (I2C3CONbits.RSEN) || (I2C3CONbits.PEN) || (I2C3CONbits.RCEN) || (I2C3CONbits.ACKEN));
		// Generating Stop Bus Event
		I2C3CONbits.PEN = 1;
	}
	//uart_puts(UARTnr1,"8\n\r");
	// Clear the Interrupt Flag	
	IFS5bits.MI2C3IF = 0;
}
//	I2C interrupts end





#endif //_UI_COMM_H_
