using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;


namespace GearShift
{
  static class Program
  {
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("kernel32.dll")]
    static extern bool AttachConsole(int dwProcessId);
    private const int ATTACH_PARENT_PROCESS = -1;


    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        //Register Syncfusion license
	   // Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTg1MTc5QDMxMzkyZTM0MmUzMEp3ZTFjVjEvdktLVFkwNVAwMmNqVmFIdFBSWndXcUI3dVNKT0VVR0lBek09");
        //Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTg1MTgxQDMxMzgyZTMyMmUzMFpqN1EzYnBqVzFuRHliRVpkMlRsbWtpQUpnMmhGaHd0UDB2czJUU3hvOWM9");
            // redirect console output to parent process;
            // must be before any calls to Console.WriteLine()

            AttachConsole(ATTACH_PARENT_PROCESS);

      // Before starting the application it must be checked first if there are any other instances opened
      bool createdNew = true;
      using (Mutex mutex = new Mutex(true, "GEARSHIFT_DE13590F-2B27-4777-85FF-7851CB944A1B", out createdNew))
      {
        if (createdNew)
        {
          // If the mutex was just created it means no instances were running, so start the app normally
          Application.EnableVisualStyles();
          Application.SetCompatibleTextRenderingDefault(false);

          Application.Run(new FormMain());

        }
        //else
        //{
        //  // If mutex failed to create it means other instances exist. Try to bring the focus to the window of the other instance of this application
        //  Process current = Process.GetCurrentProcess();
        //  foreach (Process process in Process.GetProcessesByName(current.ProcessName))
        //  {
        //    if (process.Id != current.Id)
        //    {
        //      SetForegroundWindow(process.MainWindowHandle);
        //      break;
        //    }
        //  }
        //}
      }
    }

    static void ShowSplash()
    {
      //SplashForm splshfrm = new SplashForm();
      //Application.Run(splshfrm);
    }
  }
}




