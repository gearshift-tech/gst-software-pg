#include <windows.h>
#include <winbase.h>
#include <iostream>


enum OsMemType  // OSMemType used by SystemMemStatus
{
	MMemoryLoad = 1, // total memory used in percents (%)
	MTotalPhys = 2, // total physical memory in bytes
	MAvailPhys = 3, // available physical memory (bytes)
	MTotalPageFile = 4, // total page file in (bytes)
	MAvailPageFile = 5, // available page file (bytes)
	MTotalVirtual = 6, // total virtual memory in bytes
	MAvailVirtual = 7 // available virtual memory (bytes)
};

enum ProcMemType // ProcMemType used by ProcessMemStatus
{
	PWorkingSetSize = 1, // the current working set size, in bytes
	PPageFaultCount = 2, // the number of page faults
	PPeakWorkingSetSize = 3, // the peak working set size, in bytes
	PQuotaPeakPagedPoolUsage = 4, // the peak paged pool usage, in bytes
	PQuotaPagedPoolUsage = 5, // the current paged pool usage, in bytes
	PQuotaPeakNonPagedPool = 6, // the peak nonpaged pool usage, in bytes
	PQuotaNonPagedPoolUsg = 7, // the current nonpaged pool usage, in bytes
	PPageFileUsage = 8, // the current space allocated for the pagefile, in bytes; those pages may or may not be in memory
	PPeakPagefileUsage = 9, // the peak space allocated for the pagefile, in bytes
};



//private const string DllFolder = "libs\\libHardwareIdExtractor\\";
//private const string DllName = "HardwareIDExtractorC.dll";
//private const string DllPath = /*DllFolder +*/ DllName;



//! CPU
//[DllImport(DllPath, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "CPUFamily")]
__declspec(dllimport) int* CPUFamily(); // get CPUU identifier from the windows registry

//[DllImport(DllPath, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "GetCPUVendor")]
__declspec(dllimport) int* GetCPUVendor();

//[DllImport(DllPath, CallingConvention = CallingConvention.StdCall, EntryPoint = "GetCPUSpeed")] //! free
__declspec(dllimport) double GetCPUSpeed(int speed = 200); // the higher the delay, the accurate the result (default = 200ms)

//[DllImport(DllPath, CallingConvention = CallingConvention.StdCall, EntryPoint = "IsIntel64BitCPU")] //! free
__declspec(dllimport) bool IsIntel64BitCPU(); // detects IA64 processors

//[DllImport(DllPath, CallingConvention = CallingConvention.StdCall, EntryPoint = "GetCpuTheoreticSpeed")]
__declspec(dllimport) int GetCpuTheoreticSpeed(); // get CPU speed (in MHz)

//[DllImport(DllPath, CallingConvention = CallingConvention.StdCall, EntryPoint = "IsCPUIDAvailable")] //! free
__declspec(dllimport) bool IsCPUIDAvailable();

//[DllImport(DllPath, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "GetCPUID")]
__declspec(dllimport) int* GetCPUID(unsigned short coreMask); // get the ID of the specified phisical core, max coreMask = GetCPUCount()

//[DllImport(DllPath, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "GetCpuIdNow")]
__declspec(dllimport) int* GetCpuIdNow(); // get the ID of the first available core

//[DllImport(DllPath, CallingConvention = CallingConvention.StdCall, EntryPoint = "GetCPUCount")] //! free
__declspec(dllimport) int GetCPUCount(); // the number of LOGICAL processors in the current group

//! HDD
//[DllImport(DllPath, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "GetPartitionID")]
__declspec(dllimport) int* GetPartitionID(char* partition); // get the ID of the specified partition; example of parameter: 'C:\\'

//[DllImport(DllPath, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "GetIDESerialNumber")]
__declspec(dllimport) int* GetIDESerialNumber(unsigned char driveNumber); // driveNumber is from 0 to 4


//! BIOS (NEW!)
//[DllImport(DllPath, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "BiosProductID")]
__declspec(dllimport) int* BiosProductID(); //manufacturer product (laptop, PC) ID - Could be something like: Toshiba_PQX33U-01G00H

//[DllImport(DllPath, CallingConvention = CallingConvention.StdCall, EntryPoint = "ReleaseMemory")] //! free
__declspec(dllimport) void ReleaseMemory(int* p);

//[DllImport(DllPath, CallingConvention = CallingConvention.StdCall, EntryPoint = "GetDllVersion")] //! free
__declspec(dllimport) double GetDllVersion();