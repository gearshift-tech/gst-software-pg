using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Soko.Common.Common;
using GST.ZF6.Components.Interfaces.MechShifterUSB;

namespace GST.ZF6.Components.Forms
{
  public partial class Zf6InitPanel : UserControl
  {

    UsbDevice _zf6USB = null;
    private GearboxControllerType _gearboxType = GearboxControllerType.NON_MECHATRONIC;

    public event EventHandler ContinueButtonClicked;
    public event EventHandler CancelButtonClicked;


    /// <summary>
    /// Zf6 gearbox type
    /// </summary>
    public GearboxControllerType gearboxType
    {
      get { return _gearboxType; }
      set
      {
        _gearboxType = value;
      }
    }

    /// <summary>
    /// Zf6 USB interface
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GST.ZF6.Components.Interfaces.MechShifterUSB.UsbDevice Zf6USB
    {
      get { return _zf6USB; }
      set
      {
        if (value != null)
        {
          _zf6USB = value;
          if (_zf6USB.IsConnected)
          {
            DeviceConnected();
          }
          else
          {
            DeviceDisconnected();
          }
          _zf6USB.Connected += new EventHandler(device_Connected);
          _zf6USB.Disconnected += new EventHandler(device_Disconnected);
        }
      }
    }

    public void Start()
    {
      if (!InitbgWorker.IsBusy && _zf6USB != null)
      {
        usbConnLabel.Text = string.Empty;
        gbxTypeLabel.Text = string.Empty;
        gbxInitLabel.Text = string.Empty;
        readyToGoLabel.Text = string.Empty;
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
      OnCancelButtonClicked();
    }

    public Zf6InitPanel()
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
        if (null != _zf6USB)
        {
            _zf6USB.Dispose();
            _zf6USB = null;
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
      ContinueButtonClicked?.Invoke(this, EventArgs.Empty);
    }

    private void OnCancelButtonClicked()
    {
      CancelButtonClicked?.Invoke(this, EventArgs.Empty);
    }

    private void InitbgWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      this.Invoke((MethodInvoker)delegate { usbConnLabel.Text += "."; });
      System.Threading.Thread.Sleep(500);
      this.Invoke((MethodInvoker)delegate { usbConnLabel.Text += "."; });
      System.Threading.Thread.Sleep(500);
      if (_zf6USB.IsConnected)
      {
        this.Invoke((MethodInvoker)delegate { usbConnLabel.Text = "OK"; });
      }
      else
      {
        // If device is not connected, assign a label and exit
        this.Invoke((MethodInvoker)delegate { usbConnLabel.Text = "Not connected!"; });
        return;
      }
      if (InitbgWorker.CancellationPending)
      {
        // Check from time to time if this should be cancelled
        e.Cancel = true;
        CleanUpBgWorkerCancellation();
        return;
      }
      // Next step is to enable Kline interface, MDFrame and select type
      this.Invoke((MethodInvoker)delegate { gbxTypeLabel.Text += "."; });
      _zf6USB.EnableKline();
      System.Threading.Thread.Sleep(500);
      this.Invoke((MethodInvoker)delegate { gbxTypeLabel.Text += "."; });
      _zf6USB.EnableMDFrame();
      System.Threading.Thread.Sleep(500);
      this.Invoke((MethodInvoker)delegate { gbxTypeLabel.Text += "."; });
      System.Threading.Thread.Sleep(500);
      this.Invoke((MethodInvoker)delegate { gbxTypeLabel.Text += "."; });
      _zf6USB.SelectGearboxType(_gearboxType);
      System.Threading.Thread.Sleep(500);
      this.Invoke((MethodInvoker)delegate { gbxTypeLabel.Text += "."; });
      System.Threading.Thread.Sleep(500);
      this.Invoke((MethodInvoker)delegate { gbxTypeLabel.Text += "."; });
      System.Threading.Thread.Sleep(500);
      this.Invoke((MethodInvoker)delegate { gbxTypeLabel.Text += "."; });
      System.Threading.Thread.Sleep(500);
      this.Invoke((MethodInvoker)delegate { gbxTypeLabel.Text = Enum.GetName(typeof(GearboxControllerType), _gearboxType) + " selected OK"; });

      _zf6USB.SetGPLeds(false, false);

      // Try couple times to connect to the gearbox
      for (int i = 0; i < 3; i++)
      {
        if (InitbgWorker.CancellationPending)
        {
          // Check from time to time if this should be cancelled
          e.Cancel = true;
          CleanUpBgWorkerCancellation();
          return;
        }

        // Wait 7 seconds for a gearbox to init
        this.Invoke((MethodInvoker)delegate { gbxInitLabel.Text = ""; });
        for (int del = 0; del < 14; del++)
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
        // Set the 'negotiation LED' on
        _zf6USB.SetGPLeds(true, false);
        // After waiting time try to drive the gearbox
        _zf6USB.EnableGearboxDrive();
        // Repeatedly check if the gearbox is driven properly, max 30 seconds allowed for this
        this.Invoke((MethodInvoker)delegate { readyToGoLabel.Text = ""; });
        for (int del = 0; del < 60; del++)
        {
          if (InitbgWorker.CancellationPending)
          {
            // Check from time to time if this should be cancelled
            e.Cancel = true;
            CleanUpBgWorkerCancellation();
            return;
          }
          this.Invoke((MethodInvoker)delegate { readyToGoLabel.Text += "."; });
          System.Threading.Thread.Sleep(500);
          if (_zf6USB.GearboxConnectionStatus == GearboxConnectionStatus.Connected)
          {
            break;
          }
        }
        // Check if gearbox is connected
        if (_zf6USB.GearboxConnectionStatus == GearboxConnectionStatus.Connected)
        {
          break;
        }
        else
        {
          _zf6USB.DisableGearboxDrive();
          this.Invoke((MethodInvoker)delegate { readyToGoLabel.Text = "Retrying.."; });
        }
      }
      // Check if gearbox is connected
      if (_zf6USB.GearboxConnectionStatus == GearboxConnectionStatus.Connected)
      {
        _zf6USB.SetGPLeds(false, true);
        this.Invoke((MethodInvoker)delegate { readyToGoLabel.Text = "Connected!"; });
        this.Invoke((MethodInvoker)delegate { continueButton.Enabled = true; });
      }
      else
      {
        _zf6USB.SetGPLeds(false, false);
        this.Invoke((MethodInvoker)delegate { readyToGoLabel.Text = "Failed to connect!"; });
      }

    }

    private void CleanUpBgWorkerCancellation()
    {
      _zf6USB.SetGPLeds(false, false);
      this.Invoke((MethodInvoker)delegate { usbConnLabel.Text = ""; });
      this.Invoke((MethodInvoker)delegate { gbxInitLabel.Text = ""; });
      this.Invoke((MethodInvoker)delegate { readyToGoLabel.Text = ""; });
      this.Invoke((MethodInvoker)delegate { gbxTypeLabel.Text = ""; });
      this.Invoke((MethodInvoker)delegate { continueButton.Enabled = false; });
      Zf6USB.DisableGearboxDrive();

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
