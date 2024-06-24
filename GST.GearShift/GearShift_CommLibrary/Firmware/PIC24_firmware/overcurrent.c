#include "p24fxxxx.h"

#include "main.h"
#include "overcurrent.h"
#include "USBPacket.h"
#include "pwm.h"
#include "DAQ.h"

#include "debug.h"

//
// Change notification initialization routine
// (used for overcurrent protection)
//
void OVC_Init( void )
{
    // Turn on only used change notification inputs
    CNEN1 = 0x0000;
    CNEN2 = 0x0000;
    CNEN3 = 0x0000;
    CNEN4 = 0x0000;
    CNEN5 = 0x0000;

    CNEN3bits.CN45IE = 1;  // OVC1
    CNEN1bits.CN0IE  = 1;  // OVC2
    CNEN1bits.CN1IE  = 1;  // OVC3
    CNEN4bits.CN63IE = 1;  // OVC4
    CNEN4bits.CN62IE = 1;  // OVC5
    CNEN4bits.CN61IE = 1;  // OVC6
    CNEN4bits.CN60IE = 1;  // OVC7
    CNEN4bits.CN59IE = 1;  // OVC8
    CNEN4bits.CN58IE = 1;  // OVC9

    // Disable pullups and pulldowns
    CNPU1 = 0;
    CNPU2 = 0;
    CNPU3 = 0;
    CNPU4 = 0;
    CNPU5 = 0;
    CNPD1 = 0;
    CNPD2 = 0;
    CNPD3 = 0;
    CNPD4 = 0;
    CNPD5 = 0;

    // Enable interrupt from CN module
    IFS1bits.CNIF = 0; // Reset flag
    IPC4bits.CNIP = 7; // Set priority ( highest available )
    IEC1bits.CNIE = 1; // Enable
}

//
// Change notification interrupt
//
void __attribute__ ((interrupt, no_auto_psv)) _CNInterrupt(void)
{
    // It is assumed that the overcurrent mask can only be set in this interrupt,
    // mask is reset in the appStart function.
    //unsigned int temp;
    //temp = 0;
    workData.overCurrentPorts |= (!OVC_CH1) << 0;
    workData.overCurrentPorts |= (!OVC_CH2) << 1;
    workData.overCurrentPorts |= (!OVC_CH3) << 2;
    workData.overCurrentPorts |= (!OVC_CH4) << 3;
    workData.overCurrentPorts |= (!OVC_CH5) << 4;
    workData.overCurrentPorts |= (!OVC_CH6) << 5;
    workData.overCurrentPorts |= (!OVC_CH7) << 6;
    workData.overCurrentPorts |= (!OVC_CH8) << 7;
    workData.overCurrentPorts |= (!OVC_CH9) << 8;

    if (workData.overCurrentPorts )
    {
        pwmStop(); // Immediately disable PWM outputs to prevent damage
        DAQ_Stop(); // Disable the DAQ mechanism
    }
    //workData.overCurrentPorts |= temp;
    //rprintf("\n\nOVERCURRENT BITCHEZZZ!!! ");
    #ifndef PRODUCTION
    //    rprintf(" %d %d %d %d %d %d %d %d %d \n\n", OVC_CH1, OVC_CH2, OVC_CH3, OVC_CH4, OVC_CH5, OVC_CH6, OVC_CH7, OVC_CH8, OVC_CH9);
    #endif
    IFS1bits.CNIF = 0;
}

