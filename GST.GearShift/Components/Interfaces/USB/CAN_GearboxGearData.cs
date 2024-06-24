using System;
using System.Collections.Generic;
using System.Text;

namespace GST.Gearshift.Components.Interfaces.USB//GST.Gearshift.Components.Interfaces.USB
{
  [Serializable]
  public class CAN_GearboxGearData
  {

    #region Constants



    #endregion  Constants


    #region Public fields

    /// <summary>
    /// List of bytes in the CAN data packet causing a gearbox change the gear to this one
    /// </summary>
    public List<byte> mDataCodes = null;

    /// <summary>
    /// Name of this gear
    /// </summary>
    public string mGearName = string.Empty;

    #endregion Public fields


    #region Constructors & finalizer

    public CAN_GearboxGearData()
    {
      mDataCodes = new List<byte>( 0 );
      for ( int i = 0; i < 8; i++ )
        mDataCodes.Add( 0 );
    }

    #endregion Constructors & finalizer


    #region Events



    #endregion Events


    #region Properties



    #endregion Properties


    #region Methods



    #endregion Methods

  }
}
