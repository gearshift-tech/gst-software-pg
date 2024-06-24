using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GST.ZF6.Components.Interfaces.MechShifterUSB;
using Soko.Common.Common;

namespace GST.Gearshift.Components.Forms.CAN
{
  public partial class NissanRE5InitPanel : UserControl
  {

    UsbDevice _usbDev_Decoder = null;

    public event EventHandler ContinueButtonClicked;
    public event EventHandler CancelButtonClicked;

    /// <summary>
    /// Decoder USB interface
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GST.ZF6.Components.Interfaces.MechShifterUSB.UsbDevice UsbDev_Decoder
    {
      get { return _usbDev_Decoder; }
      set
      {
        if (value != null)
        {
          _usbDev_Decoder = value;
          if (_usbDev_Decoder.IsConnected)
          {
            DeviceConnected();
          }
          else
          {
            DeviceDisconnected();
          }
          _usbDev_Decoder.Connected += new EventHandler(device_Connected);
          _usbDev_Decoder.Disconnected += new EventHandler(device_Disconnected);
        }
      }
    }

    public void Start()
    {
      if (!InitbgWorker.IsBusy && _usbDev_Decoder != null)
      {
        usbConnLabel.Text = string.Empty;
        gbxTypeLabel.Text = string.Empty;
        gbxInitLabel.Text = string.Empty;
        readyToGoLabel.Text = string.Empty;
        InitbgWorker.RunWorkerAsync();
        continueButton.Enabled = false;
      }
    }

    public NissanRE5InitPanel()
    {
      InitializeComponent();
      usbConnLabel.Text = string.Empty;
      gbxTypeLabel.Text = string.Empty;
      gbxInitLabel.Text = string.Empty;
      readyToGoLabel.Text = string.Empty;
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
        if (null != _usbDev_Decoder)
        {
            _usbDev_Decoder.Dispose();
            _usbDev_Decoder = null;
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
      if (ContinueButtonClicked != null)
      {
        ContinueButtonClicked(this, EventArgs.Empty);
      }
    }

    private void OnCancelButtonClicked()
    {
      if (CancelButtonClicked != null)
      {
        CancelButtonClicked(this, EventArgs.Empty);
      }
    }

    private void InitbgWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      this.Invoke((MethodInvoker)delegate { usbConnLabel.Text += "."; });
      System.Threading.Thread.Sleep(500);
      this.Invoke((MethodInvoker)delegate { usbConnLabel.Text += "."; });
      System.Threading.Thread.Sleep(500);
      if (_usbDev_Decoder.IsConnected)
      {
        this.Invoke((MethodInvoker)delegate { usbConnLabel.Text = "OK"; });
      }
      else
      {
        // If device is not connected, assign a label and exit
        this.Invoke((MethodInvoker)delegate { usbConnLabel.Text = "Not connected!"; });
        //return;
      }
      if (InitbgWorker.CancellationPending)
      {
        // Check from time to time if this should be cancelled
        e.Cancel = true;
        CleanUpBgWorkerCancellation();
        return;
      }
      // Next step is to enable CAN interface and select type
      this.Invoke((MethodInvoker)delegate { gbxTypeLabel.Text += "."; });
      
      System.Threading.Thread.Sleep(500);
      this.Invoke((MethodInvoker)delegate { gbxTypeLabel.Text += "."; });
      _usbDev_Decoder.EnableMDFrame();
      System.Threading.Thread.Sleep(500);
      this.Invoke((MethodInvoker)delegate { gbxTypeLabel.Text += "."; });
      System.Threading.Thread.Sleep(500);
      this.Invoke((MethodInvoker)delegate { gbxTypeLabel.Text += "."; });
      // Call zf6 to select a gearbox and thus click the relay and enable CAN messages coming through
      _usbDev_Decoder.SelectGearboxType( GearboxControllerType.ZF_6HPxx_CE);
      System.Threading.Thread.Sleep(500);
      this.Invoke((MethodInvoker)delegate { gbxTypeLabel.Text += "."; });
      System.Threading.Thread.Sleep(500);
      this.Invoke((MethodInvoker)delegate { gbxTypeLabel.Text += "."; });
      System.Threading.Thread.Sleep(500);
      this.Invoke((MethodInvoker)delegate { gbxTypeLabel.Text += "."; });
      System.Threading.Thread.Sleep(500);
      this.Invoke((MethodInvoker)delegate { gbxTypeLabel.Text = "Nissan RE5 selected OK"; });

      _usbDev_Decoder.SetGPLeds(true, false);

        if (InitbgWorker.CancellationPending)
        {
          // Check from time to time if this should be cancelled
          e.Cancel = true;
          CleanUpBgWorkerCancellation();
          return;
        }

        // Wait 2 seconds for a gearbox to init
        this.Invoke((MethodInvoker)delegate { gbxInitLabel.Text = ""; });
        for (int del = 0; del < 4; del++)
        {
          if (InitbgWorker.CancellationPending)
          {
            // Check from time to time if this should be cancelled
            e.Cancel = true;
            CleanUpBgWorkerCancellation();
            return;
          }
          this.Invoke((MethodInvoker)delegate { gbxInitLabel.Text += "."; });
          System.Threading.Thread.Sleep(500);
        }
        this.Invoke((MethodInvoker)delegate { gbxInitLabel.Text = "OK"; });
        if (InitbgWorker.CancellationPending)
        {
          // Check from time to time if this should be cancelled
          e.Cancel = true;
          CleanUpBgWorkerCancellation();
          return;
        }

        // TODO NISSAN check if there's a CAN bus data available and only then continue
        // After waiting time try to drive the gearbox
        _usbDev_Decoder.SetGPLeds(false, true);
        this.Invoke((MethodInvoker)delegate { readyToGoLabel.Text = "Connected!"; });
        this.Invoke((MethodInvoker)delegate { continueButton.Enabled = true; });


    }

    private void CleanUpBgWorkerCancellation()
    {
      _usbDev_Decoder.SetGPLeds(false, false);
      this.Invoke((MethodInvoker)delegate { usbConnLabel.Text = ""; });
      this.Invoke((MethodInvoker)delegate { gbxInitLabel.Text = ""; });
      this.Invoke((MethodInvoker)delegate { readyToGoLabel.Text = ""; });
      this.Invoke((MethodInvoker)delegate { gbxTypeLabel.Text = ""; });
      this.Invoke((MethodInvoker)delegate { continueButton.Enabled = false; });
      UsbDev_Decoder.DisableGearboxDrive();

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

  }
}
