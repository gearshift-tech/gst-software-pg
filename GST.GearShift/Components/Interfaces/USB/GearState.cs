using System;
using System.Collections.Generic;
using System.Text;

namespace GST.Gearshift.Components.Interfaces.USB
{

  /// <sremarks>
  /// Class representing the gear state
  /// </remarks>
  public class GearState
  {



    #region Constants



    #endregion  Constants



    #region Private fields

    //Current gear number
    private int mGear = 1;
    //If gear is transmitted up
    private bool mIsTransitionUp = false;
    //If gear is transmitted down
    private bool mIsTransitionDown = false;

    #endregion Private fields



    #region Constructors & finalizer



    #endregion Constructors & finalizer



    #region Events



    #endregion Events



    #region Properties

    /// <summary>
    /// Gets the gear number
    /// </summary>
    public int Gear
    {
      get { return mGear; }
      set { mGear = value; }
    }

    /// <summary>
    /// Gets if the gear is transited upwards
    /// </summary>
    public bool IsTransitionUp
    {
      get { return mIsTransitionUp; }
      set { mIsTransitionUp = value; }
    }

    /// <summary>
    /// Gets if the gear is transited downwards
    /// </summary>
    public bool IsTransitionDown
    {
      get { return mIsTransitionDown; }
      set { mIsTransitionDown = value; }
    }

    #endregion Properties



    #region Methods



    #endregion Methods



  }

}
