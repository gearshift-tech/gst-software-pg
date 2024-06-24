using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Soko.Common.Common
{

  /// <remarks>
  /// Class has a basic parent's functionality, prepared for extension.
  /// It will be added later. Still has one bug to be fixed.
  /// </remarks>
  public class DataGridNumericTextboxCell : DataGridViewTextBoxCell
  {


    #region Constants



    #endregion  Constants



    #region Private fields

    //cell minimum value
    private int mCellMinValue = 0;
    //cell maximum value
    private int mCellMaxValue = 100;
    //if number was entered
    bool mNumberEntered = false;
    //if cell allows decimal point numbers
    bool mAllowDecimal = false;

    #endregion Private fields



    #region Constructors & finalizer

    /// <summary>
    /// Default constructor
    /// </summary>
    public DataGridNumericTextboxCell()
    {
      this.Value = "0";
    }

    #endregion Constructors & finalizer



    #region Events

    /// <summary>
    /// Processes the OnKeyDown event
    /// </summary>
    /// <param name="e"></param>
    /// <param name="rowIndex"></param>
    protected override void OnKeyDown(KeyEventArgs e, int rowIndex)
    {
      //string currentValue = (string)this.Value;
      //mNumberEntered = CheckIfNumericKey(e.KeyCode, currentValue.Contains("."));
      base.OnKeyDown(e, rowIndex);
    }

    /// <summary>
    /// Processes the OnKeyPress event
    /// </summary>
    /// <param name="e"></param>
    /// <param name="rowIndex"></param>
    protected override void OnKeyPress(KeyPressEventArgs e, int rowIndex)
    {
      // Check for the flag being set in the KeyDown event.
      if (mNumberEntered == false)
      {
        // Stop the character from being entered into the control since it is non-numerical.
        e.Handled = true;
      }
      //base.OnKeyPress(e, rowIndex);
    }

    #endregion Events



    #region Properties

    /// <summary>
    /// Gets/sets the minimal cell value
    /// </summary>
    public int CellMinValue
    {
      get { return mCellMinValue; }
      set 
      {
       if (value > mCellMaxValue)
         throw new ArgumentOutOfRangeException("[DataGridNumericTextboxCell.CellMinValue.Set] : Min value cannot be grater than Max");
        mCellMinValue = value; 
      }
    }

    /// <summary>
    /// Gets/sets the maximal cell value
    /// </summary>
    public int CellMaxValue
    {
      get { return mCellMaxValue; }
      set 
      {
       if (value < mCellMinValue)
         throw new ArgumentOutOfRangeException("[DataGridNumericTextboxCell.CellMaxValue.Set] : Max value cannot be smaller than Min");
        mCellMinValue = value; 
      }
    }

    /// <summary>
    /// Gets/sets if cell accepts decimal point numbers
    /// </summary>
    public bool AllowDecimal
    {
      get { return mAllowDecimal; }
      set { mAllowDecimal = value; }
    }

    #endregion Properties



    #region Methods

    /// <summary>
    /// Checks if the key entered was numeric key
    /// </summary>
    /// <param name="K">key code</param>
    /// <param name="isDecimalPoint">if decimal point already is in the text</param>
    /// <returns>If key is numeric key</returns>
    private bool CheckIfNumericKey(Keys K, bool isDecimalPoint)
    {
      if (K == Keys.Back) //backspace?
        return true;
      else if (K == Keys.OemPeriod || K == Keys.Decimal) //decimal point?
      {
        if (mAllowDecimal)
          return false;//if decimal numbers not allowed
        else
          return isDecimalPoint ? false : true; //or: return !isDecimalPoint
      }
      else if ((K >= Keys.D0) && (K <= Keys.D9)) //digit from top of keyboard?
        return true;
      else if ((K >= Keys.NumPad0) && (K <= Keys.NumPad9)) //digit from keypad?
        return true;
      else
        return false; //no "numeric" key
    }

    #endregion Methods


  }
}
