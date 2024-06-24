#ifndef __NVMEM_C
#define __NVMEM_C

#include <p24fxxxx.h>
#include "main.h"
#include "NVMEM.h"

int PIC_Flash_Write_NVMEM()//volatile unsigned int * buff, unsigned int size )
{
	unsigned int offset;
	unsigned int i;
	// Set up a pointer to the EEPROM location to be written
	TBLPAG = 0;//__builtin_tblpage(&my_eedata);
	offset = 0x1400;//__builtin_tbloffset(&my_eedata);

	__builtin_tblwtl(offset, 0x0000); // Set base address of erase block with dummy latch write
	__builtin_tblwth(offset, 0x0000); // Set base address of erase block with dummy latch write
	NVMCON = 0x4042; // Initialize NVMCON
	asm("DISI #5"); // Block all interrupts with priority <7 for next 5 instructions
	__builtin_write_NVM(); // C30 function to perform unlock sequence and set WR
	for (i = 0; i < 2000; i++);
	for (i = 0; i < NVMEM_SIZE; i++)
	{

		// oczekiwanie na koniec operacji na pamieci
		while (NVMCONbits.WR == 1);
		// Set up NVMCON to write one word of data EEPROM
		NVMCON = 0x4003;

		// Write Data Value To Holding Latch
		__builtin_tblwtl(offset, my_eedata_bfr[i]);
		
		// Disable Interrupts For 5 Instructions
		asm volatile ("disi #5");
		// Issue Unlock Sequence & Start Write Cycle
		__builtin_write_NVM();

		while (NVMCONbits.WR == 1);
		if (NVMCONbits.WRERR == 1)
			return 0;
		offset +=2;
	}
	return 1;

}
void PIC_Flash_Read_NVMEM()//volatile unsigned int * read_buff, unsigned int size)
{
	unsigned int offset;
	unsigned int i;

//	int a = *((int)my_eedata_addr);
	// Set up a pointer to the EEPROM location to be read
	TBLPAG = 0;//__builtin_tblpage(&my_eedata);
	offset = 0x1400;//__builtin_tbloffset(&my_eedata);
	
	//rprintf("pg: %u, offs: %u\n", TBLPAG, offset); 
	
	for (i = 0; i < NVMEM_SIZE; i++)
	{
		// oczekiwanie na koniec operacji na pamieci
		while (NVMCONbits.WR == 1);

		// Read the EEPROM data
		my_eedata_bfr[i] = __builtin_tblrdl(offset);
		offset +=2;
	}
}	

#endif
