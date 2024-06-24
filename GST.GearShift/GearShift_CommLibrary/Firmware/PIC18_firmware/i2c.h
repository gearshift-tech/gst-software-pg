#ifndef __I2C_H //I2C_H  
#define __I2C_H

enum RxCmds
{
   RxCmdNone        =  0x00,
   RxCmdBgVals      =  0x01,
   RxCmdLcdRow1     =  0x02,
   RxCmdLcdRow2     =  0x03
};   

#ifndef __I2C_C

extern unsigned char SlaveAddress;
extern unsigned char DataNumber;
extern unsigned char Data;

extern volatile char lcdRow1Buff[17];
extern volatile char lcdRow2Buff[17];
extern volatile unsigned char lcdDispRow1;
extern volatile unsigned char lcdDispRow2;

#endif //__I2C_C



void i2c_init(void);
void i2c_enable(unsigned char en);
void i2c_reception(unsigned char number, unsigned char data);



#endif //I2C_H