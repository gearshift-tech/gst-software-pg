#ifndef __LCD_H /* LCD_H  */
#define __LCD_H



#define LCD_RS			LATCbits.LATC7
#define LCD_RW			LATCbits.LATC6
#define LCD_E			LATBbits.LATB4
#define LCD_DATA		LATB  // LATB3 LATB2 LATB1 LATB0


extern unsigned short timerTime;
extern unsigned char wait;

extern volatile unsigned  char lcd_dispMsg;
extern volatile unsigned  char lcd_dispRow1;
extern volatile unsigned  char lcd_dispRow2;
extern volatile char lcd_msgBuff[34]; 

void lcd_sendInstr(unsigned char data);
void lcd_sendData(unsigned char data);
void lcd_clear(void);
void lcd_nextLine(void);
void lcd_retHome(void);
void lcd_init(void);
void lcd_sendMessage(char* s);
void delay_ms(unsigned short time);

#endif /* LCD_H  */
