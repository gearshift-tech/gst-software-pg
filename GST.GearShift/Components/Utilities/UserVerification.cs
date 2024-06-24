using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;
using System.IO;

namespace SlarkStuff.HardwareIdExtractor
{
  public static class MyHardwareIdExtractor
  {
    #region HardwareIDExtractorC.dll

    public enum OsMemType : byte // OSMemType used by SystemMemStatus
    {
      MMemoryLoad = 1, // total memory used in percents (%)
      MTotalPhys = 2, // total physical memory in bytes
      MAvailPhys = 3, // available physical memory (bytes)
      MTotalPageFile = 4, // total page file in (bytes)
      MAvailPageFile = 5, // available page file (bytes)
      MTotalVirtual = 6, // total virtual memory in bytes
      MAvailVirtual = 7 // available virtual memory (bytes)
    }

    public enum ProcMemType : byte // ProcMemType used by ProcessMemStatus
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
    }


    private static class Native //! NATIVE
    {
      private const string DllFolder = "libs\\libHardwareIdExtractor\\";
      private const string DllName = "HardwareIDExtractorC.dll";
      private const string DllPath = /*DllFolder +*/ DllName;



      //! CPU
      [DllImport(DllPath, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "CPUFamily")]
      internal static extern IntPtr CPUFamily(); // get CPUU identifier from the windows registry

      [DllImport(DllPath, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "GetCPUVendor")]
      internal static extern IntPtr GetCPUVendor();

      [DllImport(DllPath, CallingConvention = CallingConvention.StdCall, EntryPoint = "GetCPUSpeed")] //! free
      internal static extern double GetCPUSpeed(int speed = 200); // the higher the delay, the accurate the result (default = 200ms)

      [DllImport(DllPath, CallingConvention = CallingConvention.StdCall, EntryPoint = "IsIntel64BitCPU")] //! free
      internal static extern bool IsIntel64BitCPU(); // detects IA64 processors

      [DllImport(DllPath, CallingConvention = CallingConvention.StdCall, EntryPoint = "GetCpuTheoreticSpeed")]
      internal static extern int GetCpuTheoreticSpeed(); // get CPU speed (in MHz)

      [DllImport(DllPath, CallingConvention = CallingConvention.StdCall, EntryPoint = "IsCPUIDAvailable")] //! free
      internal static extern bool IsCPUIDAvailable();

      [DllImport(DllPath, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "GetCPUID")]
      internal static extern IntPtr GetCPUID(ushort coreMask); // get the ID of the specified phisical core, max coreMask = GetCPUCount()

      [DllImport(DllPath, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "GetCpuIdNow")]
      internal static extern IntPtr GetCpuIdNow(); // get the ID of the first available core

      [DllImport(DllPath, CallingConvention = CallingConvention.StdCall, EntryPoint = "GetCPUCount")] //! free
      internal static extern int GetCPUCount(); // the number of LOGICAL processors in the current group


      //! SYSTEM RAM
      [DllImport(DllPath, CallingConvention = CallingConvention.StdCall, EntryPoint = "SystemMemStatus")] //! free
      internal static extern uint SystemMemStatus(OsMemType osMemType); // in Bytes, Limited by the capacity of the OS (32bits OSs will report max 2 or 3GB)

      [DllImport(DllPath, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "SystemMemStatus_KB")]
      internal static extern IntPtr SystemMemStatus_KB(OsMemType osMemType); // in KB

      [DllImport(DllPath, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "SystemMemStatus_MB")]
      internal static extern IntPtr SystemMemStatus_MB(OsMemType osMemType); // in MB


      //! PROCESS RAM
      [DllImport(DllPath, CallingConvention = CallingConvention.StdCall, EntryPoint = "ProcessMemStatus")]
      internal static extern uint ProcessMemStatus(ProcMemType procMemType = ProcMemType.PWorkingSetSize); // returns data about the memory used of the current process

      [DllImport(DllPath, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "ProcessPeakMem")] //! free
      internal static extern IntPtr ProcessPeakMem(); // showsthe highest amount of memory this program ever occupied

      [DllImport(DllPath, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "ProcessCurrentMem")] //! free
      internal static extern IntPtr ProcessCurrentMem();


      //! VIRTUAL RAM
      [DllImport(DllPath, CallingConvention = CallingConvention.StdCall, EntryPoint = "GetPageSize")] //! free
      internal static extern uint GetPageSize(); // the page size and the granularity of page protection and commitment; 
      // this is the page size used by the VirtualAlloc function

      [DllImport(DllPath, CallingConvention = CallingConvention.StdCall, EntryPoint = "GetMemGranularity")] //! free
      internal static extern int GetMemGranularity(); // granularity with which virtual memory is allocated (in KB)


      //! RAM - Advanced stuff
      [DllImport(DllPath, CallingConvention = CallingConvention.StdCall, EntryPoint = "GetLowAddr")] //! free
      internal static extern uint GetLowAddr(); // lowest RAM memory address accessible to applications (this is the RAM address, not virtual memory address)

      [DllImport(DllPath, CallingConvention = CallingConvention.StdCall, EntryPoint = "GetHiAddr")] //! free
      internal static extern uint GetHiAddr(); // lowest RAM memory address accessible to applications

      [DllImport(DllPath, CallingConvention = CallingConvention.StdCall, EntryPoint = "TrimWorkingSet")]
      internal static extern void TrimWorkingSet(); // minimizes the amount to RAM used by application by swapping the unused pages back to disk


      //! HDD
      [DllImport(DllPath, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "GetPartitionID")]
      internal static extern IntPtr GetPartitionID(string partition); // get the ID of the specified partition; example of parameter: 'C:\\'

      [DllImport(DllPath, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "GetIDESerialNumber")]
      internal static extern IntPtr GetIDESerialNumber(byte driveNumber); // driveNumber is from 0 to 4


      //! BIOS (NEW!)
      [DllImport(DllPath, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "BiosDate")]
      internal static extern IntPtr BiosDate();

      [DllImport(DllPath, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "BiosVersion")]
      internal static extern IntPtr BiosVersion(); // could be something like: TOSQCI - 6040000 Ver 1.00PARTTBL. 
      // TOS is from Toshiba, Q is comming from product series (Qosmio)

      [DllImport(DllPath, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "BiosProductID")]
      internal static extern IntPtr BiosProductID(); //manufacturer product (laptop, PC) ID - Could be something like: Toshiba_PQX33U-01G00H

      [DllImport(DllPath, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "BiosVideo")]
      internal static extern IntPtr BiosVideo();

      //! UTILS
      [DllImport(DllPath, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "BiosVideo")]
      internal static extern IntPtr GenerateHardwareReport(); // before calling this a valid Serial Number must be entered

      [DllImport(DllPath, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "FormatBytes")]
      internal static extern IntPtr FormatBytes(Int64 size, byte decimals); // format bytes to KB, MB, GB, TB

      [DllImport(DllPath, CallingConvention = CallingConvention.StdCall, EntryPoint = "BinToInt")]
      internal static extern uint BinToInt(string binaryString);

      [DllImport(DllPath, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "IntToBin")] //! free
      internal static extern IntPtr IntToBin(uint value, byte digits);

      //- SOURCE: http://www.soft.tahionic.com/download-hdd_id/hardware%20id%20programming%20source%20code/CPU_mask.html
      [DllImport(DllPath, CallingConvention = CallingConvention.StdCall, EntryPoint = "CoreNumber2CoreMask")] //!free
      internal static extern ushort CoreNumber2CoreMask(ushort cpuCore); // steps: 1, 2, 4, 8, 16, 32, 64, 128 (for 8  cores)

      [DllImport(DllPath, CallingConvention = CallingConvention.StdCall, EntryPoint = "ReleaseMemory")] //! free
      internal static extern void ReleaseMemory(IntPtr p);

      [DllImport(DllPath, CallingConvention = CallingConvention.StdCall, EntryPoint = "GetDllVersion")] //! free
      internal static extern double GetDllVersion();

      //[DllImport(DllPath, CallingConvention = CallingConvention.StdCall, EntryPoint = "ChangeByteOrder")] //! free
      //internal static extern void ChangeByteOrder(UInt64 data, uint size); //Available only to Delphi users

      //[DllImport(DllPath, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "WindowsProductID")] //! free
      //internal static extern IntPtr WindowsProductID(); //! Available only to Delphi users


      //! INTERNAL
      [DllImport(DllPath, CallingConvention = CallingConvention.StdCall, EntryPoint = "EnterKey")]
      internal static extern bool EnterKey(int key);
    }

    private static class Internal //! INTERNAL
    {
      internal static bool EnterKey(int key = 0)
      {
        var myKey = key; // get key parameter
        if (myKey == 0) myKey = 1234; // if default key parameter, set valid key
        if (Native.EnterKey(myKey)) return true;

        //MyErrorReporting.ReportError(MyErrorReporting.ErrorId.WrongFingerKey, true);
        return false;

      }
    }

    public static class Cpu //! CPU
    {
      public static string CpuFamily()
      {
        if (!Internal.EnterKey()) return null;
        var ptr = Native.CPUFamily();
        var cpuid = Marshal.PtrToStringAnsi(ptr);
        Native.ReleaseMemory(ptr);
        return cpuid;
      }

      public static string GetCpuVendor()
      {
        if (!Internal.EnterKey()) return null;
        var ptr = Native.GetCPUVendor();
        var cpuid = Marshal.PtrToStringAnsi(ptr);
        Native.ReleaseMemory(ptr);
        return cpuid;
      }

      public static double GetCpuSpeed(int delay = 200)
      {
        return !Internal.EnterKey() ? 0 : Native.GetCPUSpeed();
      }

      public static bool IsIntel64BitCpu()
      {
        return Internal.EnterKey() && Native.IsIntel64BitCPU();
      }

      public static int GetCpuTheoreticSpeed()
      {
        return !Internal.EnterKey() ? 0 : Native.GetCpuTheoreticSpeed();
      }

      public static bool IsCpuIdAvailable()
      {
        return Internal.EnterKey() && Native.IsCPUIDAvailable();
      }

      public static string GetCpuId(ushort cpuCore)
      {
        if (!Internal.EnterKey()) return null;
        if (!IsCpuIdAvailable()) return null; // return null if CPU ID is not available
        var myCpuCore = Native.CoreNumber2CoreMask(cpuCore);
        if (cpuCore <= 0 || cpuCore > GetCpuCount() || myCpuCore == 0)
        {

          //MyErrorReporting.ReportError(MyErrorReporting.ErrorId.CpuCoreOutOfRange, silentReport: false);
          return null; // return null if requested core number is out of range
        }
        var ptr = Native.GetCPUID(myCpuCore); //! ---> only physical cores as parameter
        var cpuId = Marshal.PtrToStringAnsi(ptr);
        Native.ReleaseMemory(ptr);
        return cpuId;
      }

      public static string GetCpuIdNative(ushort cpuCore)
      {
        if (!Internal.EnterKey()) return null;
        if (!IsCpuIdAvailable()) return null; // return null if CPU ID is not available
        var ptr = Native.GetCPUID(cpuCore); // physical cores 
        var cpuId = Marshal.PtrToStringAnsi(ptr);
        Native.ReleaseMemory(ptr);
        return cpuId;
      }

      public static string GetCpuIdNow()
      {
        if (!Internal.EnterKey()) return null;
        if (!IsCpuIdAvailable()) return null; // return null if CPU ID is not available
        var ptr = Native.GetCpuIdNow();
        var cpuIdNow = Marshal.PtrToStringAnsi(ptr);
        Native.ReleaseMemory(ptr);
        return cpuIdNow;
      }

      public static int GetCpuCount()
      {
        return !Internal.EnterKey() ? 0 : Native.GetCPUCount();
      }
    }

    public static class SystemRam //! SYSTEM RAM
    {
      public static uint SystemMemStatus(OsMemType osMemType)
      {
        //! This is a 32bit DLL. It can only 'see' the first 4GB of RAM. A 64 bit version will be available.
        return !Internal.EnterKey() ? 0 : Native.SystemMemStatus(osMemType);
      }

      public static string SystemMemStatus_KB(OsMemType osMemType)
      {
        if (!Internal.EnterKey()) return null;
        var ptr = Native.SystemMemStatus_KB(osMemType);
        var sysMem = Marshal.PtrToStringAnsi(ptr);
        Native.ReleaseMemory(ptr);
        return sysMem;
      }

      public static string SystemMemStatus_MB(OsMemType osMemType)
      {
        if (!Internal.EnterKey()) return null;
        var ptr = Native.SystemMemStatus_MB(osMemType);
        var sysMem = Marshal.PtrToStringAnsi(ptr);
        Native.ReleaseMemory(ptr);
        return sysMem;
      }
    }

    public static class ProcessRam //! PROCESS RAM
    {
      public static uint ProcessMemStatus(ProcMemType procMemType = ProcMemType.PWorkingSetSize)
      {
        //! returns uint in bytes
        return !Internal.EnterKey() ? 0 : Native.ProcessMemStatus(procMemType);
      }

      public static string ProcessPeakMem()
      {
        if (!Internal.EnterKey()) return null;
        var ptr = Native.ProcessPeakMem();
        var sysMem = Marshal.PtrToStringAnsi(ptr);
        Native.ReleaseMemory(ptr);
        //! returns string in MB
        return sysMem;
      }

      public static string ProcessCurrentMem()
      {
        if (!Internal.EnterKey()) return null;
        var ptr = Native.ProcessCurrentMem();
        var sysMem = Marshal.PtrToStringAnsi(ptr);
        Native.ReleaseMemory(ptr);
        //! returns string in MB
        return sysMem;
      }
    }

    public static class VirtualRam //! VIRTUAL RAM
    {
      public static uint GetPageSizeNative()
      {
        //! returns uint in MB
        return Native.GetPageSize();
      }

      public static int GetMemGranularityNative()
      {
        //! returns int in KB
        return Native.GetMemGranularity();
      }
    }

    public static class AdvancedRam //! RAM - Advanced stuff
    {
      public static uint GetLowAddrNative()
      {
        return Native.GetLowAddr();
      }

      public static uint GetHiAddrNative()
      {
        return Native.GetHiAddr();
      }

      public static void TrimWorkingSetNative()
      {
        //! EXTREMELY USEFUL FUNCTION
        if (!Internal.EnterKey()) return;
        Native.TrimWorkingSet();
      }
    }

    public static class HardDiskDrive //! HDD
    {
      public static string GetPartitionID(string partition = "C:\\")
      {
        if (!Internal.EnterKey()) return null;
        var ptr = Native.GetPartitionID(partition);
        var partId = Marshal.PtrToStringAnsi(ptr);
        Native.ReleaseMemory(ptr);
        //! returns string formatted Win32_LogicalDisk VolumeSerialNumber
        return partId;
      }

      public static string GetIdeSerialNumber(byte driveNumber)
      {
        if (!Internal.EnterKey()) return null;
        var ptr = Native.GetIDESerialNumber(driveNumber);
        var ideSerial = Marshal.PtrToStringAnsi(ptr);
        Native.ReleaseMemory(ptr);
        //! returns formatted hard drive unique ID:
        return ideSerial;
      }
    }

    public static class Bios //! BIOS (NEW!)
    {
      public static string BiosDate()
      {
        if (!Internal.EnterKey()) return null;
        var ptr = Native.BiosDate();
        var biosDate = Marshal.PtrToStringAnsi(ptr);
        Native.ReleaseMemory(ptr);
        //! returns a formatted string 02/05/10 (mm/dd/yy)
        return biosDate;
      }

      public static string BiosVersion()
      {
        if (!Internal.EnterKey()) return null;
        var ptr = Native.BiosVersion();
        var biosVersion = Marshal.PtrToStringAnsi(ptr);
        Native.ReleaseMemory(ptr);
        //! returns a formatted string 02/05/10 (mm/dd/yy)
        return biosVersion;
      }

      public static string BiosProductId()
      {
        if (!Internal.EnterKey()) return null;
        var ptr = Native.BiosProductID();
        var biosProductId = Marshal.PtrToStringAnsi(ptr);
        Native.ReleaseMemory(ptr);
        //! returns "System Version"
        return biosProductId;
      }

      public static string BiosVideo()
      {
        if (!Internal.EnterKey()) return null;
        var ptr = Native.BiosVideo();
        var biosVideo = Marshal.PtrToStringAnsi(ptr);
        Native.ReleaseMemory(ptr);
        //! returns a formatted string 02/05/10 (mm/dd/yy)
        return biosVideo;
      }
    }

    public static class Utils //! UTILS
    {
      public static string GenerateHardwareReport()
      {
        if (!Internal.EnterKey()) return null;
        var ptr = Native.GenerateHardwareReport();
        var hardwareReport = Marshal.PtrToStringAnsi(ptr);
        Native.ReleaseMemory(ptr);
        //! returns a formatted hardware report (just like the free utility does)
        return hardwareReport;
      }

      public static string FormatBytes(Int64 size, byte decimals)
      {
        if (!Internal.EnterKey()) return null;
        var ptr = Native.FormatBytes(size, decimals);
        var formattedBytes = Marshal.PtrToStringAnsi(ptr);
        Native.ReleaseMemory(ptr);
        //! returns a formatted string (e.g. 7014 returns 6,85 KB); due to a bug in v2.0 only works up to 1023 MB (1072693248 bytes). To be fixed
        return formattedBytes;
      }

      public static uint BinToIntNative(string binaryString)
      {
        return !Internal.EnterKey() ? 0 : Native.BinToInt(binaryString);
      }

      public static string IntToBinNative(uint value, byte digits)
      {
        if (!Internal.EnterKey()) return null;
        var ptr = Native.IntToBin(value, digits);
        var intToBin = Marshal.PtrToStringAnsi(ptr);
        Native.ReleaseMemory(ptr);
        return intToBin;
      }

      public static ushort CoreNumber2CoreMaskNative(ushort cpuCore)
      {
        return Native.CoreNumber2CoreMask(cpuCore);
      }

      public static void ReleaseMemoryNative(IntPtr p)
      {
        Native.ReleaseMemory(p);
      }

      public static double GetDllVersionNative()
      {
        return Native.GetDllVersion();
      }

    }
    #endregion //HardwareIDExtractorC.dll
  }
}