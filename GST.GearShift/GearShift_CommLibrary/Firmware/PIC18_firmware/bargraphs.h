#ifndef __bargraphs_H /* bargraphs_H  */
#define __bargraphs_H



#define BARDISP_COL				LATE // LATE2 LATE1 LATE0 
#define BARDISP_COL9			   LATBbits.LATB5
#define BARDISP_ROW1_8			LATD
#define BARDISP_ROW9_16			LATA
#define BARDISP_ROW17			LATCbits.LATC0
#define BARDISP_ROW18			LATCbits.LATC1
#define BARDISP_ROW19			LATCbits.LATC2
#define BARDISP_ROW20			LATCbits.LATC5
//#define BARDISP_ROW17_20		LATC // LATC5 LATC2 LATC1 LATC0

extern unsigned char barDispLevel[9];
extern unsigned char barDispNumber;

void barDisp_timeInit(void);
void barDisp_dispLevel(void);
void barDisp_updatedispLevel(unsigned char number ,unsigned char level);

#endif /* bargraphs_H  */
