#include "p24fxxxx.h"
#include "GenericTypeDefs.h"
#include <string.h>
#include "i2c.h"
#include "pwm.h"
#include "serial.h"
#include "adc.h"
#include "spi.h"
#include "int.h"
#include "rfm12.h"

_CONFIG1( JTAGEN_OFF & GCP_OFF & GWRP_OFF & COE_OFF & FWDTEN_OFF & ICS_PGx2 ) 
_CONFIG2( IESO_OFF & FCKSM_CSDCMD & OSCIOFNC_OFF & POSCMOD_XT &  FNOSC_FRCPLL & PLLDIV_DIV2 & IOL1WAY_OFF & IESO_ON )
_CONFIG3( WPCFG_WPCFGDIS & WPDIS_WPDIS )


#pragma code

/*------------------------------------------------------*/
/*				Variables and consts				    */
/*------------------------------------------------------*/
char stringN[30] = "";
// ADC i2c
char ADCodczyt = 0;
char ADCzapis = 0;
char ADCdata = 0x80;
// SPI
#define LED_nLATCH 		LATBbits.LATB11
#define LED_nOE			LATBbits.LATB3
unsigned short LED1 = 0xAAAA;
unsigned short LED2 = 0xAAAA;
/*-------------- Variables and consts end --------------*/


/*------------------------------------------------------*/
/*				My functions						    */
/*------------------------------------------------------*/
////////////////////
// my_strcat start
////////////////////
void my_strcat(char s[], char x)
{
	int i = 0;
	while (s[i] != '\0')
		i++;
	s[i] = x;
	s[i+1] = '\0';
}
// my_strcat end
/*-------------- My functions end -----------------------*/


/*------------------------------------------------------*/
/*				  Interrupts						    */
/*------------------------------------------------------*/
/////////////////////////////
//	External interrupts start
/////////////////////////////
void __attribute__ ((__interrupt__)) _INT1Interrupt(void)
{
	IFS1bits.INT1IF = 0;
	uart_puts(UARTnr1,"Przerwanie INT1\n\r");
}
//	External interrupts end

/////////////////////////////
//	SPI interrupts start
/////////////////////////////
void __attribute__((interrupt, no_auto_psv)) _SPI1Interrupt(void)
{
	unsigned short tmp;
   	unsigned char i;
	IFS0bits.SPI1IF = 0;
   	if( SPI1TxCnt ) 
	{
   	   return;
   	}
	
	for (i=0; i<100; i++);
	switch( SPI1State ) 
	{
		case SPI1_STATE_WRITE:
			tmp = SPI1BUF;
			tmp = SPI1BUF;
 			LED_nLATCH = 1;
         	LED_nLATCH = 0;
	
	        SPI1State = SPI1_STATE_NONE;
	        break;
		default:
			break;
	}
}
//	SPI interrupts end

/////////////////////////////
//	ADC interrupts start
/////////////////////////////
void __attribute__ ((__interrupt__)) _ADC1Interrupt(void)
{
	uart_puts(UARTnr1,"Przerwanie ADC\n\r");	
	int i;
	ADCValue = 0;
	ADC16Ptr = &ADC1BUF0;
	adc_stopSample();
	for (i = 0; i < 16; i++) // average the 16 ADC value
	{
		ADCValue = ADCValue + *ADC16Ptr++;
	}
	ADCValue = ADCValue >> 4;
	if (ADCValue > 500)
		uart_puts(UARTnr1,"ok\n\r");
	else 
		uart_puts(UARTnr1,"dupa\n\r");	
	uart_puts(UARTnr1,"koniec\n\r");	
	IFS0bits.AD1IF = 0; // Clear A/D conversion interrupt.
}
//	ADC interrupts end

/////////////////////////////
//	Timers interrupts start
/////////////////////////////
void __attribute__((__interrupt__, __shadow__)) _T5Interrupt(void)
{
	IFS1bits.T5IF = 0;
	SPI1Buff[ 0 ] = LED1;
    SPI1Buff[ 1 ] = LED2;
    SPI1TxCnt = 2;
	SPI1State = SPI1_STATE_WRITE;
	spi_write(SPInr1, SPI1Buff, SPI1TxCnt);
}

void __attribute__((interrupt, no_auto_psv)) _OC1Interrupt(void)
{
	IFS0bits.OC1IF = 0;
}

void __attribute__((interrupt, no_auto_psv)) _OC3Interrupt(void)
{
	IFS1bits.OC3IF = 0;
}
//	Timers interrupts end

/////////////////////////////
//	UARTs interrupts start
/////////////////////////////
void __attribute__((interrupt, no_auto_psv)) _U1RXInterrupt(void)
{
	char c = uart_getc(UARTnr1);
	//uart_putc(UARTnr1,c);
	if (c != 'q')
		my_strcat(stringN, c);
	else
	{
		//uart_puts(UARTnr1, stringN ) ;
		stringN[0] = '\0';
	}
	// Clear the Recieve Interrupt Flag	
	IFS0bits.U1RXIF = 0;
}
//	UARTs interrupts end

/////////////////////////////
//	I2C interrupts start
/////////////////////////////
void __attribute__((interrupt, no_auto_psv)) _MI2C2Interrupt(void)
{
	int i;
	while((I2C2CONbits.SEN) || (I2C2CONbits.RSEN) || (I2C2CONbits.PEN) || (I2C2CONbits.RCEN) || (I2C2CONbits.ACKEN));
	//uart_puts(UARTnr1,"0\t");
	// Generating Start Bus Event Interrupt
	if (((I2C2STATbits.S == 1) && (start ==1)) || (flagR==2))
	{
		for (i=0;i<150;i++);
		//uart_puts(UARTnr1,"1\t");
		// Sending Slave Device Address
		I2C2TRN = (SlaveAddress << 1) | flag;
		if (flagR==2)
			flagR=0;
		start = 0;
	}
	// Receiving Data from a Slave Device
	else if ((I2C2STATbits.P == 0) && (I2C2STATbits.RBF == 1) && (flag==1) )
	{
		//uart_puts(UARTnr1,"2\t");
		readData = I2C2RCV;
		dataReadFrame[dataCounter] = readData;
		//uart_putc(UARTnr1,readData);
		//uart_puts(UARTnr1,"\n\r");
		dataCounter++;
		while(I2C2STATbits.RBF);
		// Acknowledge Generation
		if (dataCounter == dataCount)
		{	
			//uart_puts(UARTnr1,"NACK\t");
			I2C2CONbits.ACKDT = 1;
			I2C2CONbits.ACKEN = 1;
		}
		else
		{
			//uart_puts(UARTnr1,"ACK\t");
			I2C2CONbits.ACKDT = 0;
			I2C2CONbits.ACKEN = 1;
		}
					
	}
	// Acknowledge Generation - interrupt after sending ACK
	// Slave Acknowledge Interrupt after sending Slave Device Address
	else if ((I2C2STATbits.P == 0) && (I2C2STATbits.ACKSTAT == 0) && (dataCounter < dataCount) && (flag==1))
	{
		while(I2C2STATbits.TRSTAT);
		//uart_puts(UARTnr1,"3\t");
		//uart_puts(UARTnr1,"ACK read slave\t");
		// Enable Receive
		I2C2CONbits.RCEN = 1;
	}

	// Slave Acknowledge Interrupt
	else if ((I2C2STATbits.P == 0) && (I2C2STATbits.ACKSTAT == 0) && (flag==0))
	{
		//uart_puts(UARTnr1,"4\t");
		if (dataCounter == dataCount)
		{	
			while((I2C2CONbits.SEN) || (I2C2CONbits.RSEN) || (I2C2CONbits.PEN) || (I2C2CONbits.RCEN) || (I2C2CONbits.ACKEN));
			// Generating Repeated Start Bus Event
			if (flagR==1)
			{
				I2C2CONbits.RSEN = 1;
				flagR = 2;
				flag = 1;
				dataCounter = 0;
				dataCount = dataCount1;	
			}
			// Generating Stop Bus Event	
			else
				I2C2CONbits.PEN = 1;
		}
		else
		{
			while(I2C2STATbits.TRSTAT);
			// Sending Data to a Slave Device
			I2C2TRN = dataWriteFrame[dataCounter];
			dataCounter++;
		}					
	}
	// Generating Stop Bus Event Interrupt
	else if (I2C2STATbits.P == 1)
	{
		//uart_puts(UARTnr1,"5\t");
		flagR = 0;
		if (flag==1)
			flagDataReady = 1;
		else 
			flagDataSent = 1;
	}
	// RECEIVING ACKNOWLEDGE FROM THE SLAVE Interrupt - NACK 
	else if (I2C2STATbits.ACKSTAT == 1)
	{
		//uart_puts(UARTnr1,"6\t");
		flagDataNACK = 1;
		while((I2C2CONbits.SEN) || (I2C2CONbits.RSEN) || (I2C2CONbits.PEN) || (I2C2CONbits.RCEN) || (I2C2CONbits.ACKEN));
		// Generating Stop Bus Event
		I2C2CONbits.PEN = 1;
	} 
	// Acknowledge Generation - interrupt after sending NACK
	else if ((dataCounter == dataCount) && (flag==1))
	{	
		//uart_puts(UARTnr1,"7\t");
		while((I2C2CONbits.SEN) || (I2C2CONbits.RSEN) || (I2C2CONbits.PEN) || (I2C2CONbits.RCEN) || (I2C2CONbits.ACKEN));
		// Generating Stop Bus Event
		I2C2CONbits.PEN = 1;
	}
	//uart_puts(UARTnr1,"8\n\r");
	// Clear the Interrupt Flag	
	IFS3bits.MI2C2IF = 0;
}
//	I2C interrupts end
/*-------------- Interupts end -----------------------*/
/////////////////////////////
//	Initialize ports start
/////////////////////////////
void InitPorts(void) 
{
	// Unlock registers
	__builtin_write_OSCCONL(OSCCON & 0xbf);

	TRISB = 0xFFFF;
	TRISB &= 0xF7C7; //SPI; LED_nOE; LED_nLATCH


	TRISD = 0xFFFF;
	TRISD &= 0xFDE0; //PWM; TxD 


	TRISF = 0xFFFF;
	TRISF &= 0xFFDF;// SCL2

	TRISG = 0xFFFF;

	AD1PCFGL = 0xFFFF;
	AD1PCFGH = 0xFFFF;

	__builtin_write_OSCCONL(OSCCON | 0x40);
}
//	Initialize ports end

/*------------------------------------------------------*/
/*						  Main				            */
/*------------------------------------------------------*/
int main(void)
{
	unsigned char dataTmp[4];
	int i,j, pwmTmp;
	// Disable WatchDog Timer
	RCONbits.SWDTEN = 0;
	CLKDIVbits.RCDIV = 0x00;
	InitPorts();
	uart_initPeripheralPins(UARTnr1);
	uart_init(UARTnr1, 19200);
	i2c_init(I2Cnr2, (unsigned int)FSCL);
	pwm_initPeripheralPins(PWMnr1);
	pwm_initPeripheralPins(PWMnr2);
	pwm_initPeripheralPins(PWMnr3);
	pwm_initPeripheralPins(PWMnr4);
	pwm_initPeripheralPins(PWMnr5);
	pwm_init(PWMnr1, 250, 100);
	pwm_init(PWMnr2, 250, 50);
	pwm_init(PWMnr3, 4000, 3000);
	pwm_init(PWMnr4, 4000, 1000);
	pwm_init(PWMnr5, 25, 12.5);
	uart_puts(UARTnr1,"\n\r");
	uart_puts(UARTnr1,"RFM12\n\r");
	adc_init(0x7FFF);
	int_initPeripheralPins(INTnr1);
	int_init(INTnr1, 1);
	spi_initPeripheralPins(SPInr1);
	spi_init(SPInr1);
	spi_enable(SPInr1, 1);
	SPI1State = SPI1_STATE_WRITE;
	flagDataSent = 0;
	flagDataReady = 0;
	flagDataNACK = 0;


	LED_nOE = 0;

////////////////////////////
//  Przetwornik
////////////////////////////
//// 0.25 Vref	
	uart_puts(UARTnr1,"Vaout: 0x40\n\r");	
	dataTmp[0] = 0x40;
	dataTmp[1] = 0x40;
	i2c_writeData(I2Cnr2, SlavesAddress[1], dataTmp, 2);
	while ((flagDataSent==0) && (flagDataNACK == 0));
	if (flagDataSent)
	{	
		flagDataSent = 0;
		uart_puts(UARTnr1,"Vauot1 set\n\r");
	}
	else
	{	
		flagDataNACK = 0;
		uart_puts(UARTnr1,"Error\n\r");
	}
	for (i=0; i < 4000; i++) 
		for (j=0; j < 4000; j++); 
/// 0.5 Vref
	uart_puts(UARTnr1,"Vaout: 0x80\n\r");
	dataTmp[0] = 0x40;
	dataTmp[1] = 0x80;
	while (!I2C2STATbits.P);
	i2c_writeData(I2Cnr2, SlavesAddress[1], dataTmp, 2);
	while ((flagDataSent==0) && (flagDataNACK == 0));
	if (flagDataSent)
	{	
		flagDataSent = 0;
		uart_puts(UARTnr1,"Vauot2 set\n\r");
	}
	else
	{	
		flagDataNACK = 0;
		uart_puts(UARTnr1,"Error\n\r");
	}
	for (i=0; i < 4000; i++) 
		for (j=0; j <4000; j++); 
//// 0.75 Vref
	uart_puts(UARTnr1,"Vaout: 0xC0\n\r");
	dataTmp[0] = 0x40;
	dataTmp[1] = 0xC0;
	while (!I2C2STATbits.P);
	i2c_writeData(I2Cnr2, SlavesAddress[1], dataTmp, 2);
	while ((flagDataSent==0) && (flagDataNACK == 0));
	if (flagDataSent)
	{	
		flagDataSent = 0;
		uart_puts(UARTnr1,"Vauot3 set\n\r");
	}
	else
	{	
		flagDataNACK = 0;
		uart_puts(UARTnr1,"Error\n\r");
	}
	for (i=0; i < 4000; i++) 
		for (j=0; j < 4000; j++);

//// 0.25 0.5 0.75 Vref
/*
	uart_puts(UARTnr1,"Vaout: 0x40; 0x80; 0xC0");
	dataTmp[0] = 0x40;
	dataTmp[1] = 0x40;
	dataTmp[2] = 0x80;
	dataTmp[3] = 0xC0;	
	i2c_writeData(I2Cnr2, SlavesAddress[1], dataTmp, 4);
	while ((flagDataSent==0) && (flagDataNACK == 0));
	if (flagDataSent)
	{	
		flagDataSent = 0;
		uart_puts(UARTnr1,"Vauot set\n\r");
	}
	else
	{	
		flagDataNACK = 0;
		uart_puts(UARTnr1,"Error\n\r");
	}
	for (i=0; i < 5000; i++) 
		for (j=0; j < 5000; j++); 
*/
//// read ADC result

while (1)
{
	if (strstr(stringN, "odczyt")!=NULL)
	{
		ADCodczyt = 0;
		stringN[0] = '\0';
		uart_puts(UARTnr1,"ADC channel 0 read\n\r");
		for (i=0; i<FrameSize; i++)
			dataReadFrame[i]=0;
		//i2c_readData(I2Cnr2, SlavesAddress[1], 3);
		dataTmp[0] = 0x40;
		i2c_readDataRestart(I2Cnr2, SlavesAddress[1], dataTmp, 1,3);
		while ((flagDataReady==0) && (flagDataNACK == 0));
		if (flagDataReady)
		{	
			flagDataReady = 0;
			uart_puts(UARTnr1,"Results:\n\r");
			for (i=0; i<3; i++)
			{
				uart_putc(UARTnr1,dataReadFrame[i]);
				uart_putc(UARTnr1,'\n');
				uart_putc(UARTnr1,'\r');
			}
			for (i=0; i < 3000; i++) 
				for (j=0; j < 5000; j++);
		}
		else
		{	
			flagDataNACK = 0;
			uart_puts(UARTnr1,"Error\n\r");
		}
	}
	if (strstr(stringN, "zapis")!=NULL)
	{
		ADCzapis = 0;
		stringN[0] = '\0';
		uart_puts(UARTnr1,"ADC write\n\r");
		dataTmp[0] = 0x40;
		dataTmp[1] = ADCdata;
		while (!I2C2STATbits.P);
		i2c_writeData(I2Cnr2, SlavesAddress[1], dataTmp, 2);
		while ((flagDataSent==0) && (flagDataNACK == 0));
		if (flagDataSent)
		{	
			flagDataSent = 0;
			uart_puts(UARTnr1,"Vauot set\n\r");
		}
		else
		{	
			flagDataNACK = 0;
			uart_puts(UARTnr1,"Error\n\r");
		}
	}
	if (strstr(stringN, "inc")!=NULL)
	{
		stringN[0] = '\0';
		uart_puts(UARTnr1,"ADCdata inc\n\r");
		ADCdata+=30;
	}
	if (strstr(stringN, "dec")!=NULL)
	{
		stringN[0] = '\0';
		uart_puts(UARTnr1,"ADCdata dec\n\r");
		ADCdata-=30;
	}
	if (strstr(stringN, "seria")!=NULL)
	{
		stringN[0] = '\0';
		uart_puts(UARTnr1,"Vaout: 0x40; 0x80; 0xC0");
		dataTmp[0] = 0x40;
		dataTmp[1] = 0x80;
		dataTmp[2] = 0xC0;
		dataTmp[3] = 0x40;	
		i2c_writeData(I2Cnr2, SlavesAddress[1], dataTmp, 4);
		while ((flagDataSent==0) && (flagDataNACK == 0));
		if (flagDataSent)
		{	
			flagDataSent = 0;
			uart_puts(UARTnr1,"Vauot set\n\r");
		}
		else
		{	
			flagDataNACK = 0;
			uart_puts(UARTnr1,"Error\n\r");
		}
	}
	if (strstr(stringN, "up")!=NULL)
	{
		stringN[0] = '\0';
		uart_puts(UARTnr1,"Duty Cycle inc\n\r");
		pwmTmp = 4000;
		OC3R = (pwmTmp > OC3R)?(OC3R + 20):pwmTmp;
		if (OC3R > pwmTmp) OC3R = pwmTmp;
	}
	if (strstr(stringN, "down")!=NULL)
	{
		stringN[0] = '\0';
		uart_puts(UARTnr1,"Duty Cycle dec\n\r");
		OC3R = (OC3R > 2000)?(OC3R - 20):2000;
		if (OC3R <= 2000) OC3R = 2000;
	}
	if (strstr(stringN, "sample")!=NULL)
	{
		stringN[0] = '\0';
		uart_puts(UARTnr1,"sample adc\n\r");
		adc_startSample();
	}
	if (strstr(stringN, "diody")!=NULL)
	{
		stringN[0] = '\0';
		LED1 = ~LED1;
		LED2 = ~LED2;
		uart_puts(UARTnr1,"diody\n\r");
	}

}//while

	return 0;
}
/*-------------- Main end ----------------------*/