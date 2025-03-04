﻿using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using XPTable.Models;

namespace XPTable.Events
{
    /// <summary>
    /// Provides data for the CellToolTipPopup event.
    /// </summary>
	public class CellToolTipEventArgs : CancelEventArgs
    {
        /// <summary>
        /// Creates a CellToolTipEventArgs using the values from args.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="location"></param>
        public CellToolTipEventArgs(Cell cell, Point location)
            : base(false)
        {
            Cell = cell;
            Location = location;
            ToolTipText = cell.ToolTipText;
        }

        private string _toolTipText = string.Empty;

        /// <summary>
        /// Gets or sets the text to be shown as a tooltip. By default this is only set if the text in the cell
        /// has been truncated.
        /// </summary>
        public string ToolTipText
        {
            get => _toolTipText;
            set => _toolTipText = value;
        }

        private Point _location;

        /// <summary>
        /// Gets or sets the location of the mouse when the tooltip was triggered.
        /// </summary>
        public Point Location
        {
            get => _location;
            set => _location = value;
        }

        private Cell _cell = null;
        /// <summary>
        /// Gets or sets the cell that this tooltip is for.
        /// </summary>
        public Cell Cell
        {
            get => _cell;
            set => _cell = value;
        }
    }

    /// <summary>
    /// Provides data for the HeaderToolTipPopup event.
    /// </summary>
    public class HeaderToolTipEventArgs : CancelEventArgs
    {
        /// <summary>
        /// Creates a HeaderToolTipEventArgs using the values from args.
        /// </summary>
		/// <param name="column"></param>
		/// <param name="location"></param>
		public HeaderToolTipEventArgs(Column column, Point location)
            : base(false)
        {
            Column = column;
            Location = location;
            ToolTipText = column.ToolTipText;
        }

        private string _toolTipText = string.Empty;

        /// <summary>
        /// Gets or sets the text to be shown as a tooltip. By default this is only set if the text in the cell
        /// has been truncated.
        /// </summary>
        public string ToolTipText
        {
            get => _toolTipText;
            set => _toolTipText = value;
        }

        private Point _location;

        /// <summary>
        /// Gets or sets the location of the mouse when the tooltip was triggered.
        /// </summary>
        public Point Location
        {
            get => _location;
            set => _location = value;
        }

        private Column _column = null;
        /// <summary>
        /// Gets or sets the cell that this tooltip is for.
        /// </summary>
        public Column Column
        {
            get => _column;
            set => _column = value;
        }
    }

    /// <summary>
    /// Represents the method that will handle the CellToolTipPopup event.
    /// </summary>
    public delegate void CellToolTipEventHandler(object sender, CellToolTipEventArgs e);

    /// <summary>
    /// Represents the method that will handle the HeaderToolTipPopup event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void HeaderToolTipEventHandler(object sender, HeaderToolTipEventArgs e);

}
