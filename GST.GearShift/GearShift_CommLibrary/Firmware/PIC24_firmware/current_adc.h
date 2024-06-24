#ifndef _CURRENT_ADC_H_
#define _CURRENT_ADC_H_

#ifdef _CURRENT_ADC_C_

#define AVG_RATIO          4
#define AVG_RATIO_SHIFT    2

enum CURRENT_ADC_STATE_CODES {
   STATE_IDLE = 0x00,
   STATE_INIT = 0x01,
   STATE_ADC_RUNNING = 0x02,
   //STATE_INIT = 0x03,
   //STATE_ADC_WAIT_1ST_CONVERSION = 0x04,
   
   //STATE_ADC1_GET_LAST_RESULT = 0x03,
};
enum AO_STATE_CODES {
   STATE_END = 0x00,
   STATE_AO1 = 0x01,
   STATE_AO2 = 0x02,
};
#endif

#ifdef EXT
#undef EXT
#endif

#ifndef _CURRENT_ADC_
#define EXT extern
#else
#define EXT
#endif


//
// Starts the current measurement
//
void BeginCurrentReadout( void );

//
// Initializes the current measurement system
//
void currentADCInit( void );

//
// First ADC measurement routine
//
void currentADC1SR( void );

//
// Second ADC measurement routine
//
void currentADC2SR( void ); 

void updateAOvalue( void );

//
// Writes to the SPI 1 buffer
//
void writeSPI( unsigned short data );

//
// Reads from the SPI1 buffer
//
unsigned short readSPI( void );

//
// Initializes the SPI1
//
void initSPI( void );

//
// Reads the current ADC offsets
// PWM drivers must be disabled before call !
//
void CADC_GetOffsets( void );


#endif
