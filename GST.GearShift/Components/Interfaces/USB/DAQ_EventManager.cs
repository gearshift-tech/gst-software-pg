using System;
using System.Collections.Generic;
using System.Text;


namespace GST.Gearshift.Components.Interfaces.USB
{

  /// <summary>
  /// Class representing a CriticalID
  /// </summary>
  [Serializable]
  public class DAQ_EventManager
  {

    private List<DAQ_Event> _EventList = new List<DAQ_Event>();

    private int _MaxIDDelay = 10;


    /// <summary>
    /// Wipes clean all the stored data and prepares for the new script to be managed
    /// </summary>
    public void ClearManagerData()
    {
      _EventList.Clear();
    }

    public void AddEvent(DAQ_Event evt)
    {
      _EventList.Add(evt);
    }

    public delegate void EventFiredHandler(DAQ_Event FiredEvent, GearShiftUsb.DeviceAcquiredData data);

    public event EventFiredHandler EventFired;

    public void ProcessNewData(GearShiftUsb.DeviceAcquiredData data)
    {
      if (_EventList.Count > 0)
      {
        //Console.WriteLine(data.responseToID.ToString());
        if (data.responseToID >= _EventList[0].mCriticalID)
        {
          // FIRE THE EVENT YO!
          //Console.WriteLine("COUNT: " + _EventList.Count.ToString() + "EVT REACHED: " + data.responseToID.ToString() + " exp " + _EventList[0].mCriticalID.ToString());

          if ((data.responseToID - _EventList[0].mCriticalID) <= _MaxIDDelay)
          {
            if (EventFired != null)
            {
              EventFired(_EventList[0], data);
            }

            // Remove the currently fired event from the list so it doesnt get fired again
            if (_EventList.Count > 0)
              _EventList.RemoveAt(0);
          }
        }
      }
    }

  }
}
