using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soko.Common.Interfaces
{
  public abstract class TCUGovernor
  {



    #region Constants



    #endregion  Constants



    #region Private fields



    #endregion Private fields



    #region Constructors & finalizer



    #endregion Constructors & finalizer



    #region Events

    public event EventHandler UsbDeviceConnected;
    public event EventHandler UsbDeviceDisconnected;

    public event EventHandler OnTcuConfigureStarted;
    public event EventHandler OnTcuConfigureSucceeded;
    public event EventHandler OnTcuConfigureFailed;
    public event EventHandler OnTcuConfigureStatusUpdate;

    public event EventHandler OnTcuInitWaitStarted;
    public event EventHandler OnTcuInitWaitFinished;

    public event EventHandler OnTcuSecurityStarted;
    public event EventHandler OnTcuSecuritySucceeded;
    public event EventHandler OnTcuSecurityFailed;
    public event EventHandler OnTcuSecurityStatusUpdate;

    public event EventHandler onErrorMessage;


    #endregion Events



    #region Properties

    public abstract bool UsbDeviceIsConnected
    {
      get;
    }
    public abstract bool TcuIsConnected
    {
      get;
    }
    public abstract bool TcuIsDriving
    {
      get;
    }

    #endregion Properties



    #region Methods

    public abstract void InitializeTcu();

    public abstract void EnableDrive();
    public abstract void DisableDrive();

    public abstract void SwitchGearUp();
    public abstract void SwitchGearDown();
    public abstract void SelectGear(int gear);
    public abstract float GetLastCurrentValue(int index);

    public abstract void ReadDTCs();
    public abstract void ClearDTCs();
    public abstract void ReadVIN();
    public abstract void ResetTCU();
    public abstract void ResetAdapts();

    #endregion Methods



  }
}
