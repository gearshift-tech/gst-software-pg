#include "lcd.h"
#include "p18f44k20.h"

unsigned short timerTime = 0;
unsigned char wait = 0;


//volatile unsigned  char lcd_dispMsg = 0x0;
volatile char lcd_msgBuff[34]; 

void delay_ms(unsigned short time)
{
	T0CONbits.TMR0ON = 0;
	TMR0L = 193;
	T0CONbits.T0CS = 0;
	T0CONbits.PSA = 0;
	T0CONbits.T0PS2 = 1;
	T0CONbits.T0PS1 = 0;
	T0CONbits.T0PS0 = 0;
	timerTime = time;
	INTCON2bits.TMR0IP = 0;
	INTCONbits.TMR0IF = 0;
	INTCONbits.TMR0IE = 1;
	T0CONbits.TMR0ON = 1;

}

void lcd_sendInstr(unsigned char data)
{ 
	LCD_RS = 0;
	LCD_E = 1;
	LCD_DATA &= 0xF0;
	LCD_DATA |= ((data & 0xF0) >> 4);// | (LCD_DATA & 0xF0);
	LCD_E = 0;
	LCD_E = 1;	
	LCD_DATA &= 0xF0;
	LCD_DATA |= (data & 0x0F); //| (LCD_DATA & 0xF0);
	LCD_E = 0;
	
	wait = 0;

	delay_ms(1);
	while (!wait);
}

void lcd_sendData(unsigned char data)
{
 
	LCD_RS = 1;
	LCD_E = 1;
	LCD_DATA &= 0xF0;
	LCD_DATA |= ((data & 0xF0) >> 4) ;//| (LCD_DATA & 0xF0);
	LCD_E = 0;
	LCD_E = 1;
	LCD_DATA &= 0xF0;
	LCD_DATA |= (data & 0x0F); //| (LCD_DATA & 0xF0);
	LCD_E = 0;

	wait = 0;

	delay_ms(1);
		while (!wait);
}

void lcd_clear(void)
{
	lcd_sendInstr(0x01);
}

void lcd_nextLine(void)
{
	lcd_sendInstr(0xC0);
}

void lcd_retHome(void)
{
	lcd_sendInstr(0x02);
}

void lcd_init(void)
{
	int i;
	LCD_RW = 0;
	LCD_E = 0;
	LCD_RS = 0;
	wait = 0;
	delay_ms(50);
	while (!wait);
	for(i = 0; i < 3; i++)
  	{	
		lcd_sendInstr(0x28);
  	}
	lcd_sendInstr(0x0C);
	lcd_sendInstr(0x01);
	lcd_sendInstr(0x06);
}

void lcd_sendMessage(char s[])
{
	while (*s)
		lcd_sendData(*s++);
}