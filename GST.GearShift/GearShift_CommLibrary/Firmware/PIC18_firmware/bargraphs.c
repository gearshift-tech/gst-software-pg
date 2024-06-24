#include "bargraphs.h"
#include "p18f44k20.h"

unsigned char barDispLevel[9] = {0, 0, 0, 0, 0, 0, 0, 0, 0};//{2,4,6,8,10,12,16,18,20};
unsigned char barDispNumber = 0;

unsigned char ghostingCycle = 0;

void barDisp_timeInit(void)
{
	T1CONbits.TMR1ON = 0; // Timer1 On bit
	T1CONbits.RD16 = 1; // 16-Bit Read/Write Mode Enable bit
	TMR1H = 0xFF;
	TMR1L = 180;//180
	T1CONbits.TMR1CS = 0; // Timer1 Clock Source Select bit
	T1CONbits.T1CKPS1 = 1; // Timer1 Input Clock Prescale Select bits
	T1CONbits.T1CKPS0 = 0;
	IPR1bits.TMR1IP = 1;
	PIR1bits.TMR1IF = 0;
	PIE1bits.TMR1IE = 1;
	T1CONbits.TMR1ON = 1;
}
void barDisp_dispLevel()//unsigned char number)
{   
	unsigned char i;

    BARDISP_ROW1_8 = 0x00;
  	BARDISP_ROW9_16 = 0x00;
  	BARDISP_ROW17 = 0;
  	BARDISP_ROW18 = 0;
  	BARDISP_ROW19 = 0;
  	BARDISP_ROW20 = 0;

// To remove the ghosting effect on the bargraphs a deadtime must have been added.
// Cycles 0 and 2 have rows data zeroed, column is selected at cycle 0

switch (ghostingCycle)
{
  case 0:
  {
    ghostingCycle = 1;
    // Select the column
    BARDISP_COL9 = (barDispNumber < 8)? 1: 0; 
  	if (barDispNumber < 8)
  		BARDISP_COL= barDispNumber | (BARDISP_COL & 0xF8) ;
  	else
  		BARDISP_COL&= 0xF8;
    return;
  }
  case 1:
  {
    ghostingCycle = 2;
    goto setRowsData;
    break;
  }
  case 2:
  {
    ghostingCycle = 0;
    return;
  }      
}  


setRowsData:

// Get the local copy of the current column value from the buffer
i = barDispLevel[barDispNumber];	
		
	if (i <= 8)
	{
		BARDISP_ROW1_8 = (1 << i) - 1;
		BARDISP_ROW9_16 = 0x00;
		BARDISP_ROW17 = 0;
		BARDISP_ROW18 = 0;
		BARDISP_ROW19 = 0;
		BARDISP_ROW20 = 0;
	      	//BARDISP_ROW17_20 &= 0xF0;
	}
	else if ((i > 8) && (i <= 16))
	{
		BARDISP_ROW1_8 = 0xFF;
		BARDISP_ROW9_16 = ( 1 << (i-8) ) - 1;
		BARDISP_ROW17 = 0;
		BARDISP_ROW18 = 0;
		BARDISP_ROW19 = 0;
		BARDISP_ROW20 = 0;
	      	//BARDISP_ROW17_20&= 0xF0;
	}
	else if ((i > 16) && (i <= 20))
	{
		BARDISP_ROW1_8 = 0xFF;
		BARDISP_ROW9_16 = 0xFF;
		
		      //BARDISP_ROW17_20 = ((BARDISP_ROW17_20 & 0xF0) | ((1 << (i-16)) - 1));
		switch (i)
		{
			case 17:
				BARDISP_ROW17 = 1;
				BARDISP_ROW18 = 0;
				BARDISP_ROW19 = 0;
				BARDISP_ROW20 = 0;
				break;
			case 18:
				BARDISP_ROW17 = 1;
				BARDISP_ROW18 = 1;
				BARDISP_ROW19 = 0;
				BARDISP_ROW20 = 0;
				break;
			case 19:
				BARDISP_ROW17 = 1;
				BARDISP_ROW18 = 1;
				BARDISP_ROW19 = 1;
				BARDISP_ROW20 = 0;
				break;
			case 20:
				BARDISP_ROW17 = 1;
				BARDISP_ROW18 = 1;
				BARDISP_ROW19 = 1;
				BARDISP_ROW20 = 1;
				break;
		}
	}
	
	// Go to the next column
			barDispNumber++;
		if (barDispNumber == 9) 
			barDispNumber = 0;

}
void barDisp_updatedispLevel(unsigned char number ,unsigned char level)
{
	barDispLevel[number] = level;
}