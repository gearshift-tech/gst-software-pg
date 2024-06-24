#include "i2c.h"
#include "p24fxxxx.h"
#include "main.h"

unsigned char SlavesAddress[] = {0x30, 0x01, 0x00};
unsigned char SlaveAddress = 0x00;

unsigned char dataWriteFrame[FrameSize];
unsigned char dataReadFrame[FrameSize];
unsigned char dataCount = 0;
unsigned char dataCount1 = 0;
unsigned char dataCounter;
unsigned char flag = 0;
unsigned char flagR = 0;
char readData;
unsigned char flagDataReady = 0;
unsigned char flagDataSent = 0; 
unsigned char flagDataNACK = 0;
unsigned char start = 1; 


extern void __attribute__((interrupt, no_auto_psv)) _MI2C3Interrupt(void);

void i2c_writeData(enum I2C_TYPE number, unsigned char SlaveAddr, unsigned char dataWrFrame[], unsigned char wrDataSize)
{
	int i;
	switch (number)
	{
		case 0:
			//while (!I2C1STATbits.P);
			for (i=0;i<wrDataSize;i++)
				dataWriteFrame[i] = dataWrFrame[i];
			dataCount = wrDataSize;
			SlaveAddress = SlaveAddr;
			flag = 0;
			start = 1;
			flagDataReady = 0;
			flagDataSent = 0;
			dataCounter = 0;
			I2C1CONbits.SEN = 1;
			break;
		case 1:
			//while (!I2C2STATbits.P);
			for (i=0;i<wrDataSize;i++)
				dataWriteFrame[i] = dataWrFrame[i];
			dataCount = wrDataSize;
			SlaveAddress = SlaveAddr;
			flag = 0;
			start = 1;
			flagDataReady = 0;
			flagDataSent = 0;
			dataCounter = 0;
			I2C2CONbits.SEN = 1;
			break;
		case 2:
			//while (!I2C2STATbits.P);
			for (i=0;i<wrDataSize;i++)
				dataWriteFrame[i] = dataWrFrame[i];
			dataCount = wrDataSize;
			SlaveAddress = SlaveAddr;
			flag = 0;
			start = 1;
			flagDataReady = 0;
			flagDataSent = 0;
			dataCounter = 0;
			I2C3CONbits.SEN = 1;
			break;
		default:
			break;
	}
}

void i2c_readData(enum I2C_TYPE number, unsigned char SlaveAddr, unsigned char rdDataSize)
{
	switch (number)
	{
		case 0:
			while (!I2C1STATbits.P);
			dataCount = rdDataSize;
			SlaveAddress = SlaveAddr;
			flag = 1;
			start = 1;
			flagDataReady = 0;
			flagDataSent = 0;
			dataCounter = 0;
			I2C1CONbits.SEN = 1;
			break;
		case 1:
			while (!I2C2STATbits.P);
			dataCount = rdDataSize;
			SlaveAddress = SlaveAddr;
			flag = 1;
			start = 1;
			flagDataReady = 0;
			flagDataSent = 0;
			dataCounter = 0;
			I2C2CONbits.SEN = 1;
			break;
		default:
			break;
	}
}
void i2c_readDataRestart(enum I2C_TYPE number, unsigned char SlaveAddr, unsigned char dataWrFrame[], unsigned char wrDataSize, unsigned char rdDataSize)
{
	switch (number)
	{
		int i;
		case 0:
			break;
		case 1:
			while (!I2C2STATbits.P);
			dataCount = wrDataSize;
			dataCount1 = rdDataSize;
			SlaveAddress = SlaveAddr;
			flagR = 1;
			flag = 0;			
			start = 1;
			for (i=0;i<wrDataSize;i++)
				dataWriteFrame[i] = dataWrFrame[i];
			flagDataReady = 0;
			flagDataSent = 0;
			dataCounter = 0;
			I2C2CONbits.SEN = 1;
			break;
		default:
			break;
	}

}

unsigned int i2c_baudratereg(unsigned int baudrate)
{
	unsigned int baudratereg = ((SYSCLK/baudrate - SYSCLK/10000000)-1)/3;
	return baudratereg;
}

void i2c_enable(enum I2C_TYPE number, char enable)
{
	switch (number)
	{
		case 0:
			I2C1CONbits.I2CEN = (enable==0) ? 0 : 1;
			break;
		case 1:
			I2C2CONbits.I2CEN = (enable==0) ? 0 : 1;
			break;
      case 2:
			I2C3CONbits.I2CEN = (enable==0) ? 0 : 1;
			break;
		default: 
			break;
	}
}

void i2c_init(enum I2C_TYPE number, unsigned int baudrate)
{
	switch (number)
	{
		case 0:
			//Master I2C2 Event Interrupt Priority bits
			//IPC4bits.MI2C1P2 = 1;
			//IPC4bits.MI2C1P1 = 0;
			//IPC4bits.MI2C1P0 = 0;
			I2C1BRG = i2c_baudratereg(baudrate);
			//Master I2C2 Event Interrupt Enable bit
			IEC1bits.MI2C1IE = 1;
			i2c_enable(number, 1);
			break;
		case 1:
			//Master I2C2 Event Interrupt Priority bits
			//IPC12bits.MI2C2P2 = 1;
			//IPC12bits.MI2C2P1 = 0;
			//IPC12bits.MI2C2P0 = 0;
			I2C2BRG = i2c_baudratereg(baudrate);
			IFS3bits.MI2C2IF = 0;
			//Master I2C2 Event Interrupt Enable bit
			IEC3bits.MI2C2IE = 1;
			i2c_enable(number, 1);
			break;
		case 2:
			//Master I2C2 Event Interrupt Priority bits
			IPC21bits.MI2C3P2 = 1;
			IPC21bits.MI2C3P1 = 1;
			IPC21bits.MI2C3P0 = 1;
			I2C3BRG = i2c_baudratereg(baudrate);
			IFS5bits.MI2C3IF = 0;
			//Master I2C2 Event Interrupt Enable bit
			IEC5bits.MI2C3IE = 1;
			i2c_enable(number, 1);
			break;
		default:
			break;
	}
	//init flags
	flagDataSent = 0;
   flagDataReady = 0;
   flagDataNACK = 0;
}







