using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Soko.CanCave.Components.Interfaces.CanCaveUsb;

namespace Soko.CanCave.Components.Forms
{
  public partial class GM6TxxInitPanel : UserControl
  {

    Soko.CanCave.Components.Interfaces.TCUGovernor_GM6Txx _GM6TGov = null;
    
    private GearboxControllerType _gearboxType = GearboxControllerType.NON_MECHATRONIC;

    public event EventHandler ContinueButtonClicked;
    public event EventHandler CancelButtonClicked;

    /// <summary>
    /// GM6T governor
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Soko.CanCave.Components.Interfaces.TCUGovernor_GM6Txx GM6TGovernor
    {
      get { return _GM6TGov; }
      set
      {
        if (value != null)
        {
          _GM6TGov = value;
          if (_GM6TGov.UsbDeviceIsConnected)
          {
            DeviceConnected();
          }
          else
          {
            DeviceDisconnected();
          }
          _GM6TGov.UsbDeviceConnected += new EventHandler(device_Connected);
          _GM6TGov.UsbDeviceDisconnected += new EventHandler(device_Disconnected);
        }
      }
    }

    private void ClearLabels()
    {
      zf6UsbConnLabel.Text = string.Empty;
      canProUsbConnLabel.Text = string.Empty;
      gbxFoundLabel.Text = string.Empty;
      gbxVinLabel.Text = string.Empty;
      gbxDiagLabel.Text = string.Empty;
      gbxDtcLabel.Text = string.Empty;
      gbxDtcListLabel.Text = string.Empty;
    }

    public void Start()
    {
      if (!InitbgWorker.IsBusy && _GM6TGov != null)
      {
        ClearLabels();
        InitbgWorker.RunWorkerAsync();
        continueButton.Enabled = false;
      }
    }

    public void Stop()
    {
      if (InitbgWorker.IsBusy)
      {
        InitbgWorker.CancelAsync();
      }
      else
      {
        CleanUpBgWorkerCancellation();
      }
      ClearLabels();
      OnCancelButtonClicked();
    }

    public GM6TxxInitPanel()
    {
      InitializeComponent();
      ClearLabels();
    }

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    void device_Disconnected(object sender, EventArgs e)
    {
        if (InvokeRequired)
        {
            this.BeginInvoke(new MethodInvoker(DeviceDisconnected));
        }
        else
        {
            DeviceConnected();
        }
    }

    void device_Connected(object sender, EventArgs e)
    {
        if (InvokeRequired)
        {
            this.BeginInvoke(new MethodInvoker(DeviceConnected));
        }
        else
        {
            DeviceConnected();
        }
    }

    private void DeviceConnected()
    {
    }

    private void DeviceDisconnected()
    {
    }

    private void OnContinueButtonClicked()
    {
      //TODO: Check for DTCs and display a message

      ClearLabels();
      ContinueButtonClicked?.Invoke(this, EventArgs.Empty);
    }

    private void OnCancelButtonClicked()
    {
      ClearLabels();
      CancelButtonClicked?.Invoke(this, EventArgs.Empty);
    }

    private void InitbgWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      ///
      /// DEVICE CONNECTION
      ///
      // _GM6TGov.ResetTCU();
      //System.Threading.Thread.Sleep(200);
      _GM6TGov.InitializeTcu();

      
      for (int d = 0; d < 3; d++)
      {
        this.Invoke((MethodInvoker)delegate { zf6UsbConnLabel.Text += "."; });
        this.Invoke((MethodInvoker)delegate { canProUsbConnLabel.Text += "."; });
        System.Threading.Thread.Sleep(200);
      }
      if (_GM6TGov.UsbDeviceIsConnected)
      {
        this.Invoke((MethodInvoker)delegate { zf6UsbConnLabel.Text = "Connected"; });
        this.Invoke((MethodInvoker)delegate { canProUsbConnLabel.Text = "Connected"; });
      }
      else
      {
        // If device is not connected, assign a label and exit
        this.Invoke((MethodInvoker)delegate { zf6UsbConnLabel.Text = "Not Connected"; });
        this.Invoke((MethodInvoker)delegate { canProUsbConnLabel.Text = "Not connected!"; });
        return;
      }
      if (InitbgWorker.CancellationPending)
      {
        // Check from time to time if this should be cancelled
        e.Cancel = true;
        CleanUpBgWorkerCancellation();
        return;
      }
      ///
      /// FINDING GEARBOX
      ///
      _GM6TGov.ReadVIN();
      for (int d = 0; d < 3; d++)
      {
        this.Invoke((MethodInvoker)delegate { gbxFoundLabel.Text += "."; });
        System.Threading.Thread.Sleep(200);
      }
      
      for (int d = 0; d < 10; d++)
      {
        this.Invoke((MethodInvoker)delegate { gbxFoundLabel.Text += "."; });
        System.Threading.Thread.Sleep(200);
        if (_GM6TGov.TcuPresent)
        {
          this.Invoke((MethodInvoker)delegate { gbxFoundLabel.Text = "TCU Present"; });
          // At this point start driving the gearbox
          _GM6TGov.EnableDrive();
          break;
        }
      }
      // If TCU still wad not found, abort.
      if (!_GM6TGov.TcuPresent)
      {
        this.Invoke((MethodInvoker)delegate { gbxFoundLabel.Text = "Failed to find"; });
        this.Invoke((MethodInvoker)delegate { gbxVinLabel.Text = "Failed to read"; });
        //CleanUpBgWorkerCancellation();
        return;
      }

      this.Invoke((MethodInvoker)delegate { gbxVinLabel.Text = _GM6TGov.TcuData_VIN; });
      for (int d = 0; d < 10; d++)
      {
        System.Threading.Thread.Sleep(200);
        if (_GM6TGov.TcuData_VIN.Contains("WAIT"))
        {
          this.Invoke((MethodInvoker)delegate { gbxVinLabel.Text += "."; });
        }
        else
        {
          this.Invoke((MethodInvoker)delegate { gbxVinLabel.Text = _GM6TGov.TcuData_VIN; });
          break;
        }
      } // If VIN still not read, abort
      if(_GM6TGov.TcuData_VIN.Contains("WAIT"))
      {
        this.Invoke((MethodInvoker)delegate { gbxVinLabel.Text = "Failed to read"; });
        //CleanUpBgWorkerCancellation();
        return;
      }


      if (InitbgWorker.CancellationPending)
      {
        // Check from time to time if this should be cancelled
        e.Cancel = true;
        CleanUpBgWorkerCancellation();
        return;
      }

      ///
      /// Reading diagnostics
      ///
      for (int d = 0; d < 3; d++)
      {
        this.Invoke((MethodInvoker)delegate { gbxDiagLabel.Text += "."; });
        System.Threading.Thread.Sleep(500);
      }
      this.Invoke((MethodInvoker)delegate { gbxDiagLabel.Text = "OK"; });

      ///
      /// Reading trouble codes
      ///
      _GM6TGov.ReadDTCs();
      for (int d = 0; d < 4; d++)
      {
        this.Invoke((MethodInvoker)delegate { gbxDtcLabel.Text += "."; });
        System.Threading.Thread.Sleep(500);
      }
      if (_GM6TGov._tcuData_DTCsRead.Count > 0)
      {
        string dtcTmp = string.Empty;
        foreach (string dtc in _GM6TGov._tcuData_DTCsRead)
          dtcTmp += dtc + " ";
        this.Invoke((MethodInvoker)delegate { gbxDtcLabel.Text = _GM6TGov._tcuData_DTCsRead.Count.ToString() + " found"; });
        this.Invoke((MethodInvoker)delegate { gbxDtcListLabel.Text = dtcTmp; });
        this.Invoke((MethodInvoker)delegate { continueButton.Enabled = false; });
        this.Invoke((MethodInvoker)delegate { clearDtcButton.Enabled = true; });
      }
      else
      {
        this.Invoke((MethodInvoker)delegate { gbxDtcLabel.Text = "0 found"; });
        this.Invoke((MethodInvoker)delegate { clearDtcButton.Enabled = false; });
        this.Invoke((MethodInvoker)delegate { continueButton.Enabled = true; });
      }
      // If went this far without errors, let the user continue
      
    }

    private void CleanUpBgWorkerCancellation()
    {
      if (InvokeRequired)
      {
        this.Invoke((MethodInvoker)delegate { CleanUpBgWorkerCancellation(); });
        return;
        }
      ClearLabels();
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      if (InitbgWorker.IsBusy)
      {
        InitbgWorker.CancelAsync();
      }
      else
      {
        CleanUpBgWorkerCancellation();
      }
      OnCancelButtonClicked();
    }

    private void continueButton_Click(object sender, EventArgs e)
    {
      OnContinueButtonClicked();
    }

    private void clearDtcButton_Click(object sender, EventArgs e)
    {
      clearDtcButton.Enabled = false;
      _GM6TGov.ClearDTCs();
      this.Start();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      _GM6TGov.NullTps();
    }
  }
}
