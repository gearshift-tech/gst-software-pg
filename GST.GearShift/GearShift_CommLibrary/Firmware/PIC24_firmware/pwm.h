#ifndef _PWM_H_
#define _PWM_H_


void pwmInit( void );
//void initCmpRegisters( void );
//void loadCmpRegisters( void );
//void loadOCRegisters( void );
void CalcOCRegisters( void );
void ApplyPWMFreq( void );
void resetOCFCondition( void );
void pwmStart( void );
void pwmStop( void );




#endif
