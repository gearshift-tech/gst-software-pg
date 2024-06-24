
#ifndef __PRINTF_H
#define __PRINTF_H

//extern void rputs( char const *txt );

#ifndef __RPRINTF_C
extern void my_putc( char c );
extern void myputchar(unsigned char c);

#endif

extern void rprintf( char const *fmt0, ... );



#endif
