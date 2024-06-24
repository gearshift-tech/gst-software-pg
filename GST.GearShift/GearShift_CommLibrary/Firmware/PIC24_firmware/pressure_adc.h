
//
// pressure A/D converter initialization
//
void InitAD( void );

//
// Starts pressure readout routine
//
void startIntAD( void )

//
// ADC1 interrupt routine (pressure )
//
void __attribute__ ((interrupt, no_auto_psv)) _ADC1Interrupt( void );