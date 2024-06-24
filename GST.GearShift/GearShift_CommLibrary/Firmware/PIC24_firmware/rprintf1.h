
#ifndef __PRINTF1_H
#define __PRINTF1_H

//extern void rputs( char const *txt );

#ifndef __RPRINTF1_C
extern void my_putc1( char c );
extern void myputchar1(unsigned char c);

#endif

extern void rprintf1( char const *fmt0, ... );

extern void rprintf1_init();



#endif
