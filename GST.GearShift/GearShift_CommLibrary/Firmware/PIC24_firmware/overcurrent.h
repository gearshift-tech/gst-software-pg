

//
// Change notification initialization routine
// (used for overcurrent protection)
//
void OVC_Init( void );

//
// Change notification interrupt
//
void __attribute__ ((interrupt, no_auto_psv)) _CNInterrupt(void);
