#ifndef __I2C_H /*I2C_H  */
#define __I2C_H

//#ifndef SYSCLK
//#define SYSCLK	16000000  // External oscillator frequency
//#endif
//Communicating as a Master in a Single Master Environment
//1. Assert a Start condition on SDAx and SCLx.
//2. Send the I2C device address byte to the slave with a write indication.
//3. Wait for and verify an Acknowledge from the slave.
//4. Send the serial memory address high byte to the slave.
//5. Wait for and verify an Acknowledge from the slave.
//6. Send the serial memory address low byte to the slave.
//7. Wait for and verify an Acknowledge from the slave.
//8. Assert a Repeated Start condition on SDAx and SCLx.
//9. Send the device address byte to the slave with a read indication.
//10. Wait for and verify an Acknowledge from the slave.
//11. Enable master reception to receive serial memory data.
//12. Generate an ACK or NACK condition at the end of a received byte of data.
//13. Generate a Stop condition on SDAx and SCLx.

#define FSCL 100000 //100kHz


#define FrameSize 10
extern unsigned char SlavesAddress[];
extern unsigned char SlaveAddress;

extern unsigned char dataWriteFrame[FrameSize];
extern unsigned char dataReadFrame[FrameSize];
extern unsigned char dataCount;
extern unsigned char dataCount1;
extern unsigned char dataCounter;
extern unsigned char flag;
extern unsigned char flagR;
extern char readData;
extern unsigned char flagDataReady;
extern unsigned char flagDataSent; 
extern unsigned char flagDataNACK;
extern unsigned char start; 








enum I2C_TYPE {I2Cnr1,I2Cnr2, I2Cnr3};

void i2c_init(enum I2C_TYPE number, unsigned int baudrate);
void i2c_enable(enum I2C_TYPE number, char enable);
unsigned int i2c_baudratereg(unsigned int baudrate);

void i2c_writeData(enum I2C_TYPE number, unsigned char SlaveAddr, unsigned char dataWrFrame[], unsigned char wrDataSize);
void i2c_readData(enum I2C_TYPE number, unsigned char SlaveAddr, unsigned char rdDataSize);
void i2c_readDataRestart(enum I2C_TYPE number, unsigned char SlaveAddr, unsigned char dataWrFrame[], unsigned char wrDataSize, unsigned char rdDataSize);


#endif /*I2C_H  */
