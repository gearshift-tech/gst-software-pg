#ifndef _UI_COMM_H_
#define _UI_COMM_H_

// defines
#define UI_LCD_W 16 // LCD width
#define UI_LCD_H 2 // LCD height

// enums
//
enum UI_i2cCmd
{
   UI_i2c_CmdNone        =  0x00,
   UI_i2c_CmdBgVals      =  0x01,
   UI_i2c_CmdLcdRow1      =  0x02,
   UI_i2c_CmdLcdRow2      =  0x03
}; 

#ifndef _UI_COMM_C_
   // buffers
   extern volatile unsigned char UI_bgVals[9];      // array of 9 bargraph values (0 - 20)
   extern volatile char UI_lcdRow1[17];    // buffer for the first row of LCD display
   extern volatile char UI_lcdRow2[17];    // buffer for the second row of LCD display
   
   // flags
   extern volatile unsigned char UI_updateBgs;  // flag if bargraphs should be updated via i2c
   extern volatile unsigned char UI_bgDisplayCurrent; // flag if bargraphs should display currents (1) or PWM values (0)
   extern volatile unsigned char UI_updateLcdRow1;  // flag if 1st lcd row should be updated via i2c
   extern volatile unsigned char UI_updateLcdRow2;  // flag if 2nd lcd row should be updated via i2c
   extern volatile unsigned char UI_i2cCommErr; // flag if UI communication error occured
   
   //methods
   
   extern void UI_UpdateLcdRow1( char *str );
   extern void UI_UpdateLcdRow2( char * str );
   extern void UI_UpdateBgs( void );
   
#endif //_UI_COMM_C_

#endif //_UI_COMM_H_

