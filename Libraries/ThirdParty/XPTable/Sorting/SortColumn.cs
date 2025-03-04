﻿using System;
using System.Text;
using System.Windows.Forms;

namespace XPTable.Sorting
{
    /// <summary>
    /// Represents a single sortable column 
    /// </summary>
    public class SortColumn
    {

        #region Class Data
        /// <summary>
        /// Specifies how the Column is to be sorted
        /// </summary>
        private readonly SortOrder sortOrder;

        /// <summary>
        /// The index of the Column to be sorted
        /// </summary>
        private readonly int column;
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the SortColumn class with the specified 
        /// Column index and SortOrder
        /// </summary>
        /// <param name="column">The index of the Column to be sorted</param>
        /// <param name="sortOrder">Specifies how the Column is to be sorted</param>
        public SortColumn(int column, SortOrder sortOrder)
        {
            this.column = column;
            this.sortOrder = sortOrder;
        }

        #endregion

        /// <summary>
        /// Gets the index of the Column to be sorted
        /// </summary>
        public int SortColumnIndex => column;

        /// <summary>
        /// Gets how the Column is to be sorted
        /// </summary>
        public SortOrder SortOrder => sortOrder;
    }
}
