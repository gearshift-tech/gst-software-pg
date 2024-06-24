#
# Generated Makefile - do not edit!
#
# Edit the Makefile in the project folder instead (../Makefile). Each target
# has a -pre and a -post target defined where you can add customized code.
#
# This makefile implements configuration specific macros and targets.


# Include project Makefile
ifeq "${IGNORE_LOCAL}" "TRUE"
# do not include local makefile. User is passing all local related variables already
else
include Makefile
# Include makefile containing local settings
ifeq "$(wildcard nbproject/Makefile-local-default.mk)" "nbproject/Makefile-local-default.mk"
include nbproject/Makefile-local-default.mk
endif
endif

# Environment
MKDIR=gnumkdir -p
RM=rm -f 
MV=mv 
CP=cp 

# Macros
CND_CONF=default
ifeq ($(TYPE_IMAGE), DEBUG_RUN)
IMAGE_TYPE=debug
OUTPUT_SUFFIX=elf
DEBUGGABLE_SUFFIX=elf
FINAL_IMAGE=dist/${CND_CONF}/${IMAGE_TYPE}/tester.X.${IMAGE_TYPE}.${OUTPUT_SUFFIX}
else
IMAGE_TYPE=production
OUTPUT_SUFFIX=hex
DEBUGGABLE_SUFFIX=elf
FINAL_IMAGE=dist/${CND_CONF}/${IMAGE_TYPE}/tester.X.${IMAGE_TYPE}.${OUTPUT_SUFFIX}
endif

ifeq ($(COMPARE_BUILD), true)
COMPARISON_BUILD=-mafrlcsj
else
COMPARISON_BUILD=
endif

ifdef SUB_IMAGE_ADDRESS
SUB_IMAGE_ADDRESS_COMMAND=--image-address $(SUB_IMAGE_ADDRESS)
else
SUB_IMAGE_ADDRESS_COMMAND=
endif

# Object Directory
OBJECTDIR=build/${CND_CONF}/${IMAGE_TYPE}

# Distribution Directory
DISTDIR=dist/${CND_CONF}/${IMAGE_TYPE}

# Source Files Quoted if spaced
SOURCEFILES_QUOTED_IF_SPACED=../CAN_routines.c ../SPI2515.c ../OBD_comm.c ../USB/usb_device.c ../USB/usb_function_generic.c ../main.c ../rprintf.c ../usb_config.c ../pwm.c ../current_adc.c ../UsbSoftLayer.c ../overcurrent.c ../UART2_console.c ../pressure_adc.c ../device_init.c ../UI_comm.c ../i2c.c ../debug.c ../demoFunctions.c ../NVMEM.c ../rprintf1.c ../DAQ.c

# Object Files Quoted if spaced
OBJECTFILES_QUOTED_IF_SPACED=${OBJECTDIR}/_ext/1472/CAN_routines.o ${OBJECTDIR}/_ext/1472/SPI2515.o ${OBJECTDIR}/_ext/1472/OBD_comm.o ${OBJECTDIR}/_ext/1360907413/usb_device.o ${OBJECTDIR}/_ext/1360907413/usb_function_generic.o ${OBJECTDIR}/_ext/1472/main.o ${OBJECTDIR}/_ext/1472/rprintf.o ${OBJECTDIR}/_ext/1472/usb_config.o ${OBJECTDIR}/_ext/1472/pwm.o ${OBJECTDIR}/_ext/1472/current_adc.o ${OBJECTDIR}/_ext/1472/UsbSoftLayer.o ${OBJECTDIR}/_ext/1472/overcurrent.o ${OBJECTDIR}/_ext/1472/UART2_console.o ${OBJECTDIR}/_ext/1472/pressure_adc.o ${OBJECTDIR}/_ext/1472/device_init.o ${OBJECTDIR}/_ext/1472/UI_comm.o ${OBJECTDIR}/_ext/1472/i2c.o ${OBJECTDIR}/_ext/1472/debug.o ${OBJECTDIR}/_ext/1472/demoFunctions.o ${OBJECTDIR}/_ext/1472/NVMEM.o ${OBJECTDIR}/_ext/1472/rprintf1.o ${OBJECTDIR}/_ext/1472/DAQ.o
POSSIBLE_DEPFILES=${OBJECTDIR}/_ext/1472/CAN_routines.o.d ${OBJECTDIR}/_ext/1472/SPI2515.o.d ${OBJECTDIR}/_ext/1472/OBD_comm.o.d ${OBJECTDIR}/_ext/1360907413/usb_device.o.d ${OBJECTDIR}/_ext/1360907413/usb_function_generic.o.d ${OBJECTDIR}/_ext/1472/main.o.d ${OBJECTDIR}/_ext/1472/rprintf.o.d ${OBJECTDIR}/_ext/1472/usb_config.o.d ${OBJECTDIR}/_ext/1472/pwm.o.d ${OBJECTDIR}/_ext/1472/current_adc.o.d ${OBJECTDIR}/_ext/1472/UsbSoftLayer.o.d ${OBJECTDIR}/_ext/1472/overcurrent.o.d ${OBJECTDIR}/_ext/1472/UART2_console.o.d ${OBJECTDIR}/_ext/1472/pressure_adc.o.d ${OBJECTDIR}/_ext/1472/device_init.o.d ${OBJECTDIR}/_ext/1472/UI_comm.o.d ${OBJECTDIR}/_ext/1472/i2c.o.d ${OBJECTDIR}/_ext/1472/debug.o.d ${OBJECTDIR}/_ext/1472/demoFunctions.o.d ${OBJECTDIR}/_ext/1472/NVMEM.o.d ${OBJECTDIR}/_ext/1472/rprintf1.o.d ${OBJECTDIR}/_ext/1472/DAQ.o.d

# Object Files
OBJECTFILES=${OBJECTDIR}/_ext/1472/CAN_routines.o ${OBJECTDIR}/_ext/1472/SPI2515.o ${OBJECTDIR}/_ext/1472/OBD_comm.o ${OBJECTDIR}/_ext/1360907413/usb_device.o ${OBJECTDIR}/_ext/1360907413/usb_function_generic.o ${OBJECTDIR}/_ext/1472/main.o ${OBJECTDIR}/_ext/1472/rprintf.o ${OBJECTDIR}/_ext/1472/usb_config.o ${OBJECTDIR}/_ext/1472/pwm.o ${OBJECTDIR}/_ext/1472/current_adc.o ${OBJECTDIR}/_ext/1472/UsbSoftLayer.o ${OBJECTDIR}/_ext/1472/overcurrent.o ${OBJECTDIR}/_ext/1472/UART2_console.o ${OBJECTDIR}/_ext/1472/pressure_adc.o ${OBJECTDIR}/_ext/1472/device_init.o ${OBJECTDIR}/_ext/1472/UI_comm.o ${OBJECTDIR}/_ext/1472/i2c.o ${OBJECTDIR}/_ext/1472/debug.o ${OBJECTDIR}/_ext/1472/demoFunctions.o ${OBJECTDIR}/_ext/1472/NVMEM.o ${OBJECTDIR}/_ext/1472/rprintf1.o ${OBJECTDIR}/_ext/1472/DAQ.o

# Source Files
SOURCEFILES=../CAN_routines.c ../SPI2515.c ../OBD_comm.c ../USB/usb_device.c ../USB/usb_function_generic.c ../main.c ../rprintf.c ../usb_config.c ../pwm.c ../current_adc.c ../UsbSoftLayer.c ../overcurrent.c ../UART2_console.c ../pressure_adc.c ../device_init.c ../UI_comm.c ../i2c.c ../debug.c ../demoFunctions.c ../NVMEM.c ../rprintf1.c ../DAQ.c


CFLAGS=
ASFLAGS=
LDLIBSOPTIONS=

############# Tool locations ##########################################
# If you copy a project from one host to another, the path where the  #
# compiler is installed may be different.                             #
# If you open this project with MPLAB X in the new host, this         #
# makefile will be regenerated and the paths will be corrected.       #
#######################################################################
# fixDeps replaces a bunch of sed/cat/printf statements that slow down the build
FIXDEPS=fixDeps

.build-conf:  ${BUILD_SUBPROJECTS}
ifneq ($(INFORMATION_MESSAGE), )
	@echo $(INFORMATION_MESSAGE)
endif
	${MAKE}  -f nbproject/Makefile-default.mk dist/${CND_CONF}/${IMAGE_TYPE}/tester.X.${IMAGE_TYPE}.${OUTPUT_SUFFIX}

MP_PROCESSOR_OPTION=24FJ64GB108
MP_LINKER_FILE_OPTION=,--script="..\app_hid_boot_p24FJ64GB108.gld"
# ------------------------------------------------------------------------------------
# Rules for buildStep: compile
ifeq ($(TYPE_IMAGE), DEBUG_RUN)
${OBJECTDIR}/_ext/1472/CAN_routines.o: ../CAN_routines.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/CAN_routines.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/CAN_routines.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../CAN_routines.c  -o ${OBJECTDIR}/_ext/1472/CAN_routines.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/CAN_routines.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_PK3=1    -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/CAN_routines.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/SPI2515.o: ../SPI2515.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/SPI2515.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/SPI2515.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../SPI2515.c  -o ${OBJECTDIR}/_ext/1472/SPI2515.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/SPI2515.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_PK3=1    -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/SPI2515.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/OBD_comm.o: ../OBD_comm.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/OBD_comm.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/OBD_comm.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../OBD_comm.c  -o ${OBJECTDIR}/_ext/1472/OBD_comm.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/OBD_comm.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_PK3=1    -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/OBD_comm.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1360907413/usb_device.o: ../USB/usb_device.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1360907413" 
	@${RM} ${OBJECTDIR}/_ext/1360907413/usb_device.o.d 
	@${RM} ${OBJECTDIR}/_ext/1360907413/usb_device.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../USB/usb_device.c  -o ${OBJECTDIR}/_ext/1360907413/usb_device.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1360907413/usb_device.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_PK3=1    -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1360907413/usb_device.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1360907413/usb_function_generic.o: ../USB/usb_function_generic.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1360907413" 
	@${RM} ${OBJECTDIR}/_ext/1360907413/usb_function_generic.o.d 
	@${RM} ${OBJECTDIR}/_ext/1360907413/usb_function_generic.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../USB/usb_function_generic.c  -o ${OBJECTDIR}/_ext/1360907413/usb_function_generic.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1360907413/usb_function_generic.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_PK3=1    -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1360907413/usb_function_generic.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/main.o: ../main.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/main.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/main.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../main.c  -o ${OBJECTDIR}/_ext/1472/main.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/main.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_PK3=1    -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/main.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/rprintf.o: ../rprintf.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/rprintf.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/rprintf.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../rprintf.c  -o ${OBJECTDIR}/_ext/1472/rprintf.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/rprintf.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_PK3=1    -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/rprintf.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/usb_config.o: ../usb_config.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/usb_config.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/usb_config.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../usb_config.c  -o ${OBJECTDIR}/_ext/1472/usb_config.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/usb_config.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_PK3=1    -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/usb_config.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/pwm.o: ../pwm.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/pwm.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/pwm.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../pwm.c  -o ${OBJECTDIR}/_ext/1472/pwm.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/pwm.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_PK3=1    -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/pwm.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/current_adc.o: ../current_adc.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/current_adc.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/current_adc.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../current_adc.c  -o ${OBJECTDIR}/_ext/1472/current_adc.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/current_adc.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_PK3=1    -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/current_adc.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/UsbSoftLayer.o: ../UsbSoftLayer.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/UsbSoftLayer.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/UsbSoftLayer.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../UsbSoftLayer.c  -o ${OBJECTDIR}/_ext/1472/UsbSoftLayer.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/UsbSoftLayer.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_PK3=1    -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/UsbSoftLayer.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/overcurrent.o: ../overcurrent.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/overcurrent.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/overcurrent.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../overcurrent.c  -o ${OBJECTDIR}/_ext/1472/overcurrent.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/overcurrent.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_PK3=1    -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/overcurrent.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/UART2_console.o: ../UART2_console.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/UART2_console.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/UART2_console.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../UART2_console.c  -o ${OBJECTDIR}/_ext/1472/UART2_console.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/UART2_console.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_PK3=1    -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/UART2_console.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/pressure_adc.o: ../pressure_adc.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/pressure_adc.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/pressure_adc.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../pressure_adc.c  -o ${OBJECTDIR}/_ext/1472/pressure_adc.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/pressure_adc.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_PK3=1    -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/pressure_adc.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/device_init.o: ../device_init.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/device_init.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/device_init.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../device_init.c  -o ${OBJECTDIR}/_ext/1472/device_init.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/device_init.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_PK3=1    -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/device_init.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/UI_comm.o: ../UI_comm.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/UI_comm.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/UI_comm.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../UI_comm.c  -o ${OBJECTDIR}/_ext/1472/UI_comm.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/UI_comm.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_PK3=1    -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/UI_comm.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/i2c.o: ../i2c.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/i2c.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/i2c.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../i2c.c  -o ${OBJECTDIR}/_ext/1472/i2c.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/i2c.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_PK3=1    -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/i2c.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/debug.o: ../debug.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/debug.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/debug.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../debug.c  -o ${OBJECTDIR}/_ext/1472/debug.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/debug.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_PK3=1    -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/debug.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/demoFunctions.o: ../demoFunctions.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/demoFunctions.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/demoFunctions.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../demoFunctions.c  -o ${OBJECTDIR}/_ext/1472/demoFunctions.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/demoFunctions.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_PK3=1    -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/demoFunctions.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/NVMEM.o: ../NVMEM.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/NVMEM.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/NVMEM.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../NVMEM.c  -o ${OBJECTDIR}/_ext/1472/NVMEM.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/NVMEM.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_PK3=1    -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/NVMEM.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/rprintf1.o: ../rprintf1.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/rprintf1.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/rprintf1.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../rprintf1.c  -o ${OBJECTDIR}/_ext/1472/rprintf1.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/rprintf1.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_PK3=1    -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/rprintf1.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/DAQ.o: ../DAQ.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/DAQ.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/DAQ.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../DAQ.c  -o ${OBJECTDIR}/_ext/1472/DAQ.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/DAQ.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_PK3=1    -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/DAQ.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
else
${OBJECTDIR}/_ext/1472/CAN_routines.o: ../CAN_routines.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/CAN_routines.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/CAN_routines.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../CAN_routines.c  -o ${OBJECTDIR}/_ext/1472/CAN_routines.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/CAN_routines.o.d"        -g -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/CAN_routines.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/SPI2515.o: ../SPI2515.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/SPI2515.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/SPI2515.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../SPI2515.c  -o ${OBJECTDIR}/_ext/1472/SPI2515.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/SPI2515.o.d"        -g -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/SPI2515.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/OBD_comm.o: ../OBD_comm.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/OBD_comm.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/OBD_comm.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../OBD_comm.c  -o ${OBJECTDIR}/_ext/1472/OBD_comm.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/OBD_comm.o.d"        -g -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/OBD_comm.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1360907413/usb_device.o: ../USB/usb_device.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1360907413" 
	@${RM} ${OBJECTDIR}/_ext/1360907413/usb_device.o.d 
	@${RM} ${OBJECTDIR}/_ext/1360907413/usb_device.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../USB/usb_device.c  -o ${OBJECTDIR}/_ext/1360907413/usb_device.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1360907413/usb_device.o.d"        -g -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1360907413/usb_device.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1360907413/usb_function_generic.o: ../USB/usb_function_generic.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1360907413" 
	@${RM} ${OBJECTDIR}/_ext/1360907413/usb_function_generic.o.d 
	@${RM} ${OBJECTDIR}/_ext/1360907413/usb_function_generic.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../USB/usb_function_generic.c  -o ${OBJECTDIR}/_ext/1360907413/usb_function_generic.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1360907413/usb_function_generic.o.d"        -g -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1360907413/usb_function_generic.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/main.o: ../main.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/main.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/main.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../main.c  -o ${OBJECTDIR}/_ext/1472/main.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/main.o.d"        -g -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/main.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/rprintf.o: ../rprintf.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/rprintf.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/rprintf.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../rprintf.c  -o ${OBJECTDIR}/_ext/1472/rprintf.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/rprintf.o.d"        -g -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/rprintf.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/usb_config.o: ../usb_config.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/usb_config.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/usb_config.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../usb_config.c  -o ${OBJECTDIR}/_ext/1472/usb_config.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/usb_config.o.d"        -g -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/usb_config.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/pwm.o: ../pwm.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/pwm.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/pwm.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../pwm.c  -o ${OBJECTDIR}/_ext/1472/pwm.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/pwm.o.d"        -g -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/pwm.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/current_adc.o: ../current_adc.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/current_adc.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/current_adc.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../current_adc.c  -o ${OBJECTDIR}/_ext/1472/current_adc.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/current_adc.o.d"        -g -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/current_adc.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/UsbSoftLayer.o: ../UsbSoftLayer.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/UsbSoftLayer.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/UsbSoftLayer.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../UsbSoftLayer.c  -o ${OBJECTDIR}/_ext/1472/UsbSoftLayer.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/UsbSoftLayer.o.d"        -g -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/UsbSoftLayer.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/overcurrent.o: ../overcurrent.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/overcurrent.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/overcurrent.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../overcurrent.c  -o ${OBJECTDIR}/_ext/1472/overcurrent.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/overcurrent.o.d"        -g -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/overcurrent.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/UART2_console.o: ../UART2_console.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/UART2_console.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/UART2_console.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../UART2_console.c  -o ${OBJECTDIR}/_ext/1472/UART2_console.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/UART2_console.o.d"        -g -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/UART2_console.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/pressure_adc.o: ../pressure_adc.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/pressure_adc.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/pressure_adc.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../pressure_adc.c  -o ${OBJECTDIR}/_ext/1472/pressure_adc.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/pressure_adc.o.d"        -g -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/pressure_adc.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/device_init.o: ../device_init.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/device_init.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/device_init.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../device_init.c  -o ${OBJECTDIR}/_ext/1472/device_init.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/device_init.o.d"        -g -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/device_init.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/UI_comm.o: ../UI_comm.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/UI_comm.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/UI_comm.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../UI_comm.c  -o ${OBJECTDIR}/_ext/1472/UI_comm.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/UI_comm.o.d"        -g -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/UI_comm.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/i2c.o: ../i2c.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/i2c.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/i2c.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../i2c.c  -o ${OBJECTDIR}/_ext/1472/i2c.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/i2c.o.d"        -g -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/i2c.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/debug.o: ../debug.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/debug.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/debug.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../debug.c  -o ${OBJECTDIR}/_ext/1472/debug.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/debug.o.d"        -g -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/debug.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/demoFunctions.o: ../demoFunctions.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/demoFunctions.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/demoFunctions.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../demoFunctions.c  -o ${OBJECTDIR}/_ext/1472/demoFunctions.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/demoFunctions.o.d"        -g -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/demoFunctions.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/NVMEM.o: ../NVMEM.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/NVMEM.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/NVMEM.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../NVMEM.c  -o ${OBJECTDIR}/_ext/1472/NVMEM.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/NVMEM.o.d"        -g -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/NVMEM.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/rprintf1.o: ../rprintf1.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/rprintf1.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/rprintf1.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../rprintf1.c  -o ${OBJECTDIR}/_ext/1472/rprintf1.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/rprintf1.o.d"        -g -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/rprintf1.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
${OBJECTDIR}/_ext/1472/DAQ.o: ../DAQ.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/DAQ.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/DAQ.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../DAQ.c  -o ${OBJECTDIR}/_ext/1472/DAQ.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/DAQ.o.d"        -g -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -O0 -I"/C:/Program Files (x86)/Microchip/MPLAB C30/support/generic/h" -I"/C:/Program Files (x86)/Microchip/MPLAB C30/include" -I".." -I"../USB" -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/DAQ.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
endif

# ------------------------------------------------------------------------------------
# Rules for buildStep: assemble
ifeq ($(TYPE_IMAGE), DEBUG_RUN)
else
endif

# ------------------------------------------------------------------------------------
# Rules for buildStep: assemblePreproc
ifeq ($(TYPE_IMAGE), DEBUG_RUN)
else
endif

# ------------------------------------------------------------------------------------
# Rules for buildStep: link
ifeq ($(TYPE_IMAGE), DEBUG_RUN)
dist/${CND_CONF}/${IMAGE_TYPE}/tester.X.${IMAGE_TYPE}.${OUTPUT_SUFFIX}: ${OBJECTFILES}  nbproject/Makefile-${CND_CONF}.mk    ../app_hid_boot_p24FJ64GB108.gld
	@${MKDIR} dist/${CND_CONF}/${IMAGE_TYPE} 
	${MP_CC} $(MP_EXTRA_LD_PRE)  -o dist/${CND_CONF}/${IMAGE_TYPE}/tester.X.${IMAGE_TYPE}.${OUTPUT_SUFFIX}  ${OBJECTFILES_QUOTED_IF_SPACED}      -mcpu=$(MP_PROCESSOR_OPTION)        -D__DEBUG -D__MPLAB_DEBUGGER_PK3=1  -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)   -mreserve=data@0x800:0x81F -mreserve=data@0x820:0x821 -mreserve=data@0x822:0x823 -mreserve=data@0x824:0x825 -mreserve=data@0x826:0x84F   -Wl,,,--defsym=__MPLAB_BUILD=1,--defsym=__MPLAB_DEBUG=1,--defsym=__DEBUG=1,--defsym=__MPLAB_DEBUGGER_PK3=1,$(MP_LINKER_FILE_OPTION),--heap=1024,--stack=1024,--check-sections,--data-init,--pack-data,--handles,--isr,--no-gc-sections,--fill-upper=0,--stackguard=16,--library-path="..",--no-force-link,--smart-io,-Map="${DISTDIR}/tester.X.${IMAGE_TYPE}.map",--report-mem,--memorysummary,dist/${CND_CONF}/${IMAGE_TYPE}/memoryfile.xml$(MP_EXTRA_LD_POST) 
	
else
dist/${CND_CONF}/${IMAGE_TYPE}/tester.X.${IMAGE_TYPE}.${OUTPUT_SUFFIX}: ${OBJECTFILES}  nbproject/Makefile-${CND_CONF}.mk   ../app_hid_boot_p24FJ64GB108.gld
	@${MKDIR} dist/${CND_CONF}/${IMAGE_TYPE} 
	${MP_CC} $(MP_EXTRA_LD_PRE)  -o dist/${CND_CONF}/${IMAGE_TYPE}/tester.X.${IMAGE_TYPE}.${DEBUGGABLE_SUFFIX}  ${OBJECTFILES_QUOTED_IF_SPACED}      -mcpu=$(MP_PROCESSOR_OPTION)        -omf=elf -DXPRJ_default=$(CND_CONF)  -no-legacy-libc  $(COMPARISON_BUILD)  -Wl,,,--defsym=__MPLAB_BUILD=1,$(MP_LINKER_FILE_OPTION),--heap=1024,--stack=1024,--check-sections,--data-init,--pack-data,--handles,--isr,--no-gc-sections,--fill-upper=0,--stackguard=16,--library-path="..",--no-force-link,--smart-io,-Map="${DISTDIR}/tester.X.${IMAGE_TYPE}.map",--report-mem,--memorysummary,dist/${CND_CONF}/${IMAGE_TYPE}/memoryfile.xml$(MP_EXTRA_LD_POST) 
	${MP_CC_DIR}\\xc16-bin2hex dist/${CND_CONF}/${IMAGE_TYPE}/tester.X.${IMAGE_TYPE}.${DEBUGGABLE_SUFFIX} -a  -omf=elf  
	
endif


# Subprojects
.build-subprojects:


# Subprojects
.clean-subprojects:

# Clean Targets
.clean-conf: ${CLEAN_SUBPROJECTS}
	${RM} -r build/default
	${RM} -r dist/default

# Enable dependency checking
.dep.inc: .depcheck-impl

DEPFILES=$(shell mplabwildcard ${POSSIBLE_DEPFILES})
ifneq (${DEPFILES},)
include ${DEPFILES}
endif
