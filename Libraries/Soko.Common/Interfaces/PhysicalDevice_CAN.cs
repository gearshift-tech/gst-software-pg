using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soko.Common.Interfaces
{
  class PhysicalDevice_CAN
  {

    #region Constants

    public static int CAN_DevRxBufferSize = 32;
    public static int CAN_DevTxBufferSize = 32; ///TODO: CORRECT THIS VALUE

    #endregion  Constants



    #region Private fields

    // CAN buffers and variables //--------------------------------------------------------------------//
    //-------------------------------------------------------------------------------------------------//

    // Timer to serve CAN communication with the device
    //private System.Timers.Timer mCANRxTxTimer = null;

    // Buffer with CAN data received (filled on timer tick)
    private List<CanMessage> _CAN_DataRxBuffer = null;
    // Buffer with CAN data to transmit (emptied on timer tick)
    private List<CanMessage> mCANTxDataBuffer = null;

    // If the CAN bus termination is enabled or not
    private bool mCANTerminationEnabled = false;


    #endregion Private fields



    #region Events

    /// <summary>
    /// Fired when CAN data was received
    /// </summary>
    public event EventHandler CANDataReceivedEvent;

    // <summary>
    // Fired when CAN is enabled
    // </summary>
    public event EventHandler CAN_EnabledEvent;

    // <summary>
    // Fired when CAN is disabled
    // </summary>
    public event EventHandler CAN_DisabledEvent;

    // <summary>
    // Fired when CAN bus termination is enabled
    // </summary>
    public event EventHandler CAN_TerminationStateChangedEvent;

    // <summary>
    // Fired when CAN bus termination is disabled
    // </summary>
    public event EventHandler CAN_TerminationDisabledEvent;

    public event EventHandler CAN_CanCordingStarted;

    public event EventHandler CAN_CanCordingStopped;

    public event EventHandler CAN_CanPlaybackStarted;

    public event EventHandler CAN_CanPlaybackStopped;

    #endregion Events



    #region Properties

    // CAN buffers and variables //--------------------------------------------------------------------//
    //-------------------------------------------------------------------------------------------------//

    /// <summary>
    /// CAN received messages buffer
    /// </summary>
    public List<CanMessage> CANRxBuffer
    {
      get { return null; }
    }

    /// <summary>
    /// CAN messages to transmit buffer
    /// </summary>
    public List<CanMessage> CANTxBuffer
    {
      get { return mCANTxDataBuffer; }
    }

    virtual public bool CAN_IsTerminationEnabled
    {
      get
      {
        return false;
      }
    }



    #endregion Properties



    #region Methods


    virtual public void CAN_EnableCommunication()
    {
    }

    virtual public void CAN_DisableCommunication()
    {
    }


    virtual public void CAN_EnableTermination()
    {
    }

    virtual public void CAN_DisableTermination()
    {
    }

    #endregion Methods

  }
}
