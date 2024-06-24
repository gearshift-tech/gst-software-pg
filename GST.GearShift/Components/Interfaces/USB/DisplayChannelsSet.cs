using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;



namespace GST.Gearshift.Components.Interfaces.USB
{
  /// <remarks>
  /// Class describing display channels set properties
  /// </remarks> 
  [Serializable]
  public class DisplayChannelsSet
  {

    #region Constants



    #endregion  Constants



    #region Private fields

    private List<DisplayChannel> mChannels = null;
    //This init value can be buggy if too low
    //(XML deserializer will not be able to add more channels than init value.
    //Later it will be assigned to proper value anyway during deserialization.
    private int mChannelsMaxCount = 1024;
    private bool mChannelsCommonMinMaxValues = false;
    private string mTypeName = string.Empty;



    #endregion Private fields



    #region Constructors & finalizer

    public DisplayChannelsSet()
    {
      mChannels = new List<DisplayChannel>( 0 );
    }

    #endregion Constructors & finalizer



    #region Events



    #endregion Events



    #region Properties

    /// <summary>
    /// Array of DisplayChannel objects, should not be normally used.
    /// It is intended to enable easy XML serialization
    /// Also remark, that you dont have an indexed acess to this array
    /// </summary>  
    public DisplayChannel[] Channels
    {
      get
      {
        DisplayChannel[] temp = new DisplayChannel[mChannels.Count];
        mChannels.CopyTo(temp);
        return temp;
      }
      set
      {
        if (value == null)
          throw new NullReferenceException("[DisplayChannelsSet.Channels.Set] : Argument cannot be null");
        if (value.Length > mChannelsMaxCount)
          throw new ArgumentOutOfRangeException("[DisplayChannelsSet.Channels.Set] : Cannot add more channels than mChannelsMaxCount");
        mChannels.Clear();
        mChannels.AddRange(value);
      }
    }

    /// <summary>
    /// Defines the maximum number of channels that can be held in this class
    /// </summary>  
    public int ChannelsMaxCount
    {
      get { return mChannelsMaxCount; }
      set
      {
        if (value < 0)
          throw new ArgumentException("[DisplayChannelsSet.ChannelsMaxCount.Set] : Max count cannot be less than 0");
        mChannelsMaxCount = value;
      }
    }

    /// <summary>
    /// Defines if all channels stored in this class should have the same Minimum values and Maximum values (not minval == maxval)
    /// </summary>  
    public bool ChannelsCommonMinMaxValues
    {
      get { return mChannelsCommonMinMaxValues; }
      set { mChannelsCommonMinMaxValues = value; }
    }

    /// <summary>
    /// Defines the type name sring i.e. "Pressure gauge"
    /// </summary>  
    public string TypeName
    {
      get { return mTypeName; }
      set
      {
        if (value == null)
          throw new ArgumentNullException("[DisplayChannelsSet.TypeName.Set] : TypeName string cannot be null");
        mTypeName = value;
      }
    }

    /// <summary>
    /// Returns the currently stored channels count
    /// </summary>  
    public int ChannelsCount
    {
      get { return mChannels.Count; }
    }

    #endregion Properties



    #region Methods

    /// <summary>
    /// Adds display channel at specified index
    /// </summary>
    /// <param name="channel"> DisplayChannel object to be added</param>
    /// <param name="index"> index to be added at</param>
    public void AddChannelAt( DisplayChannel channel, int index )
    {
      if (mChannels.Count >= mChannelsMaxCount)
        throw new InvalidOperationException("Cannot add more channels than mChannelsMaxCount");
      if (index < 0)
        throw new ArgumentOutOfRangeException("Cannot add channel at negative index");
      if (channel == null)
        throw new NullReferenceException("Argument cannot be null");
      if (index < mChannels.Count - 1)
        mChannels.Insert(index, channel);
      else mChannels.Add(channel);
    }

    /// <summary>
    /// Removes display channel at specified index
    /// </summary>
    /// <param name="index">index to be removed at</param>
    public void RemoveChannelAt( int index )
    {
      if (index < 0)
        throw new ArgumentOutOfRangeException("Cannot remove channel from negative index");
      if (index >= mChannels.Count)
        throw new ArgumentOutOfRangeException("Index out of bounds (exceeded)");
      mChannels.RemoveAt(index);
    }

    /// <summary>
    /// Replaces display channel at specified index with specified object
    /// </summary>
    /// <param name="channel"> DisplayChannel replacement object</param>
    /// <param name="index"> Index of object to be replaced</param>
    public void ReplaceChannelAt(DisplayChannel channel, int index)
    {
      if (channel == null)
        throw new NullReferenceException("Argument cannot be null");
      if (index > mChannelsMaxCount || index < 0)
        throw new ArgumentOutOfRangeException("Index out of bounds");
      mChannels[index] = channel;
    }

    //public ValuesSource ValuesSource
    //{
    //  get { return _valuesSource; }
    //  set { _valuesSource = value; }
    //}

    #endregion Methods



  }
}

