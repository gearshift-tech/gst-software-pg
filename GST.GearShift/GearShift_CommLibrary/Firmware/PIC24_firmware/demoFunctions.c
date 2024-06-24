//Bargraphs demo functions set

#include "demoFunctions.h"
#include "UI_comm.h"
#include "i2c.h"


unsigned char UiBgDemoPyramideLeftEng(unsigned char arg)
{
   if (arg <= 9)
      return 0;
	if (arg <= 29)
	{
   	return (arg - 9);
   }
   else
   {
      if (arg <= 49)
      {
         return (49 - arg); 
      }
      else
      {
         return 0;
      }   
   }  	
}


void UiBgDemoPyramideLeft(void)
{
   int i = 0;
   unsigned long int  k = 0;
   for ( i = 0; i < 60; i++)
   {
      for ( k = 0; k < 9; k++)
      {
         UI_bgVals[k+1] = UiBgDemoPyramideLeftEng( i + k );
      }   
      UI_bgVals[0] = UI_i2c_CmdBgVals;
      i2c_writeData(I2Cnr3, 0x01, (unsigned char*)UI_bgVals, 10);
      for (k = 0; k < 30000; k++)
      {
      }   
   }   
}   


void UiBgDemoAllON(void)
{
   int i = 0;
   unsigned long int  k = 0;
   for ( i = 0; i < 100; i++)
   {
      for ( k = 0; k < 9; k++)
      {
         UI_bgVals[k+1] = 20;
      }   
      UI_bgVals[0] = UI_i2c_CmdBgVals;
      i2c_writeData(I2Cnr3, 0x01, (unsigned char*)UI_bgVals, 10);
      for (k = 0; k < 30000; k++)
      {
      }   
   }   
}
