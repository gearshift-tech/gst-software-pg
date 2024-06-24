#include "p24fxxxx.h"
#include "main.h"
#include "pwm.h"
#include "debug.h"

volatile unsigned int * pOCxR[ 9 ];
volatile unsigned int * pOCxRS[ 9 ];


void __attribute__((__interrupt__, __no_auto_psv__)) _T2Interrupt( void ) 
{
    if( workData.reloadOCRegs )
    {
        OC1R = workData.DAQ_ocCmpRegs[ 0 ];
        OC2R = workData.DAQ_ocCmpRegs[ 1 ];
        OC3R = workData.DAQ_ocCmpRegs[ 2 ];
        OC4R = workData.DAQ_ocCmpRegs[ 3 ];
        OC5R = workData.DAQ_ocCmpRegs[ 4 ];
        OC6R = workData.DAQ_ocCmpRegs[ 5 ];
        OC7R = workData.DAQ_ocCmpRegs[ 6 ];
        OC8R = workData.DAQ_ocCmpRegs[ 7 ];
        OC9R = workData.DAQ_ocCmpRegs[ 8 ];
        workData.reloadOCRegs = 0;
    }
    //reset the interrupt flag
    IFS0bits.T2IF = 0;
}

void pwmInit( void )
{
    int i;

    //for better understanding of this code please
    //refer to Microchip's 39706a.pdf page 16-21

    T2CON = 0x0000;
    T2CONbits.TCKPS = 0x01; //prescale to 8
    TMR2 = 0x0000; //clear current timer value
    IFS0bits.T2IF = 0; //reset the interrupt flag
    IPC1bits.T2IP = 6; //setup timer interrupt
    IEC0bits.T2IE = 1; //enable timer interrupt

    OC1CON1= 0x0000;// Turn off Output Compare 1 Module
    OC2CON1= 0x0000;// Turn off Output Compare 2 Module
    OC3CON1= 0x0000;// Turn off Output Compare 3 Module
    OC4CON1= 0x0000;// Turn off Output Compare 4 Module
    OC5CON1= 0x0000;// Turn off Output Compare 5 Module
    OC6CON1= 0x0000;// Turn off Output Compare 6 Module
    OC7CON1= 0x0000;// Turn off Output Compare 7 Module
    OC8CON1= 0x0000;// Turn off Output Compare 8 Module
    OC9CON1= 0x0000;// Turn off Output Compare 9 Module

    OC1R= 0x0000;// Initialize Compare Register1 with 0x0000 (turn off)
    OC2R= 0x0000;// Initialize Compare Register2 with 0x0000 (turn off)
    OC3R= 0x0000;// Initialize Compare Register3 with 0x0000 (turn off)
    OC4R= 0x0000;// Initialize Compare Register4 with 0x0000 (turn off)
    OC5R= 0x0000;// Initialize Compare Register5 with 0x0000 (turn off)
    OC6R= 0x0000;// Initialize Compare Register6 with 0x0000 (turn off)
    OC7R= 0x0000;// Initialize Compare Register7 with 0x0000 (turn off)
    OC8R= 0x0000;// Initialize Compare Register8 with 0x0000 (turn off)
    OC9R= 0x0000;// Initialize Compare Register9 with 0x0000 (turn off)

    //OCxRS= 0x0000;// Initialize Secondary Compare Registerx with 0x0000
    // not executed as there was no effect at runtime

    OC1CON1= 0x0006;// Load new compare mode to OC1CON
    OC2CON1= 0x0006;// Load new compare mode to OC2CON
    OC3CON1= 0x0006;// Load new compare mode to OC3CON
    OC4CON1= 0x0006;// Load new compare mode to OC4CON
    OC5CON1= 0x0006;// Load new compare mode to OC5CON
    OC6CON1= 0x0006;// Load new compare mode to OC6CON
    OC7CON1= 0x0006;// Load new compare mode to OC7CON
    OC8CON1= 0x0006;// Load new compare mode to OC8CON
    OC9CON1= 0x0006;// Load new compare mode to OC9CON

    PR2 = 0x07D0;// Initialize PR2 with 0x07D0 ( 1kHz )

    T2CONbits.TON = 1;// Start Timer2 with assumed settings

    //reset the PWM values in workdata structure
    for( i = 0; i < 9; i++ )
    {
        workData.DAQ_PWMDutyCycles[ i ] = 0;
    }

    //set to default value
    workData.DAQ_PWMFreq = 1000;
    workData.reloadOCRegs = 0;
}

void CalcOCRegisters( void ) 
{
    unsigned short value;
    unsigned char i;
    unsigned char tmp;

    value = workData.DAQ_ocTmrReg / 100;
    for( i = 0; i < 9; i++ )
    {
      //if ( workData.DAQ_PWMManualDriveEnabled[i] )
      //{
      //  tmp = workData.DAQ_PWMManualDutyCycles[ i ];
      //}
      //else
      //{
        tmp = workData.DAQ_PWMDutyCycles[ i ];
      //}
        if( tmp == 100 )
        {
            workData.DAQ_ocCmpRegs[ i ] = workData.DAQ_ocTmrReg;
        }
        else
        {
            workData.DAQ_ocCmpRegs[ i ] = tmp * value ;
        }
    }
    workData.reloadOCRegs = 1;
}

void ApplyPWMFreq( void ) 
{
    //the frequency must be between 100 and 2000 Hz
    workData.DAQ_ocTmrReg = ( unsigned int ) ( ( unsigned long int ) ( SYSCLK / 2 / 8 ) / ( unsigned long int ) workData.DAQ_PWMFreq );
    // set the proper frequency (it is assumed, that due to the USB communication protocol, that the prequency will only be changed,
    // when PWM is disabled)
    PR2 = workData.DAQ_ocTmrReg;
    // recalculate the OC registers values to keep the correct duty cycle
    CalcOCRegisters();
}

void resetOCFCondition( void )
{
    OCFP = 1;
    OC1CON1bits.OCFLT0 = 0;
    OC2CON1bits.OCFLT0 = 0;
    OC3CON1bits.OCFLT0 = 0;
    OC4CON1bits.OCFLT0 = 0;
    OC5CON1bits.OCFLT0 = 0;
    OC6CON1bits.OCFLT0 = 0;
    OC7CON1bits.OCFLT0 = 0;
    OC8CON1bits.OCFLT0 = 0;
    OC9CON1bits.OCFLT0 = 0;
}   


void pwmStart( void ) 
{
    resetOCFCondition();
    // Reset the overcurrent mask
    workData.overCurrentPorts = 0;
    CalcOCRegisters();
    T2CONbits.TON = 1;
}

void pwmStop( void )
{
    OC1CON1 = 0x0000;
    OC2CON1 = 0x0000;
    OC3CON1 = 0x0000;
    OC4CON1 = 0x0000;
    OC5CON1 = 0x0000;
    OC6CON1 = 0x0000;
    OC7CON1 = 0x0000;
    OC8CON1 = 0x0000;
    OC9CON1 = 0x0000;
    T2CONbits.TON = 0;
}
