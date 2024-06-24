#ifndef __NVMEM_H
#define __NVMEM_H

#include <p24fxxxx.h>
#include "main.h"

#define NVMEM_SIZE 512

#define NVMEM_GUID_POS 10
#define NVMEM_GUID_LEN 37
#define NVMEM_SERIAL_POS 50
#define NVMEM_SERIAL_LEN 15

#ifdef __NVMEM_C
  //int __attribute__((space(prog),section("myEEData"), address(0x1400))) my_eedata[512];
  volatile unsigned int my_eedata_bfr[512];
#else
  extern volatile unsigned int my_eedata_bfr[512];
#endif

int PIC_Flash_Write_NVMEM();//volatile unsigned int * buff, unsigned int size );

void PIC_Flash_Read_NVMEM();//volatile unsigned int * read_buff, unsigned int size);

#endif
