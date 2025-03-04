﻿/*
 * Copyright © 2005, Mathew Hall
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without modification, 
 * are permitted provided that the following conditions are met:
 *
 *    - Redistributions of source code must retain the above copyright notice, 
 *      this list of conditions and the following disclaimer.
 * 
 *    - Redistributions in binary form must reproduce the above copyright notice, 
 *      this list of conditions and the following disclaimer in the documentation 
 *      and/or other materials provided with the distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
 * IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
 * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, 
 * OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY 
 * OF SUCH DAMAGE.
 */


using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using XPTable.Editors;
using XPTable.Events;
using XPTable.Models.Design;
using XPTable.Renderers;
using XPTable.Sorting;


namespace XPTable.Models
{
    /// <summary>
    /// Represents a Column whose Cells are displayed as a numbers
    /// </summary>
    [DesignTimeVisible(false),
    ToolboxItem(false)]
    public class DoubleColumn : Column
    {
        #region Class Data

        /// <summary>
        /// The value to increment or decrement a Cell when its up or down buttons are clicked
        /// </summary>
        private double increment;

        /// <summary>
        /// The maximum value for a Cell
        /// </summary>
        private double maximum;

        /// <summary>
        /// The minimum value for a Cell
        /// </summary>
        private double minimum;

        /// <summary>
        /// The alignment of the up and down buttons in the Column
        /// </summary>
        private LeftRightAlignment upDownAlignment;

        /// <summary>
        /// Specifies whether the up and down buttons should be drawn
        /// </summary>
        private bool showUpDownButtons;

        #endregion


        #region Constructor

        /// <summary>
        /// Creates a new NumberColumn with default values
        /// </summary>
        public DoubleColumn() : base()
        {
            Init();
        }


        /// <summary>
        /// Creates a new NumberColumn with the specified header text
        /// </summary>
        /// <param name="text">The text displayed in the column's header</param>
        public DoubleColumn(string text) : base(text)
        {
            Init();
        }


        /// <summary>
        /// Creates a new NumberColumn with the specified header text and width
        /// </summary>
        /// <param name="text">The text displayed in the column's header</param>
        /// <param name="width">The column's width</param>
        public DoubleColumn(string text, int width) : base(text, width)
        {
            Init();
        }


        /// <summary>
        /// Creates a new NumberColumn with the specified header text, width and visibility
        /// </summary>
        /// <param name="text">The text displayed in the column's header</param>
        /// <param name="width">The column's width</param>
        /// <param name="visible">Specifies whether the column is visible</param>
        public DoubleColumn(string text, int width, bool visible) : base(text, width, visible)
        {
            Init();
        }


        /// <summary>
        /// Creates a new TextColumn with the specified header text and image
        /// </summary>
        /// <param name="text">The text displayed in the column's header</param>
        /// <param name="image">The image displayed on the column's header</param>
        public DoubleColumn(string text, Image image) : base(text, image)
        {
            Init();
        }


        /// <summary>
        /// Creates a new TextColumn with the specified header text, image and width
        /// </summary>
        /// <param name="text">The text displayed in the column's header</param>
        /// <param name="image">The image displayed on the column's header</param>
        /// <param name="width">The column's width</param>
        public DoubleColumn(string text, Image image, int width) : base(text, image, width)
        {
            Init();
        }


        /// <summary>
        /// Creates a new TextColumn with the specified header text, image, width 
        /// and visibility
        /// </summary>
        /// <param name="text">The text displayed in the column's header</param>
        /// <param name="image">The image displayed on the column's header</param>
        /// <param name="width">The column's width</param>
        /// <param name="visible">Specifies whether the column is visible</param>
        public DoubleColumn(string text, Image image, int width, bool visible) : base(text, image, width, visible)
        {
            Init();
        }


        /// <summary>
        /// Initializes the NumberColumn with default values
        /// </summary>
        private void Init()
        {
            Format = "G";

            maximum = (double)100;
            minimum = (double)0;
            increment = (double)1;

            showUpDownButtons = false;
            upDownAlignment = LeftRightAlignment.Right;
        }

        #endregion


        #region Methods

        /// <summary>
        /// Gets a string that specifies the name of the Column's default CellRenderer
        /// </summary>
        /// <returns>A string that specifies the name of the Column's default 
        /// CellRenderer</returns>
        public override string GetDefaultRendererName()
        {
            return "NUMBER";
        }


        /// <summary>
        /// Gets the Column's default CellRenderer
        /// </summary>
        /// <returns>The Column's default CellRenderer</returns>
        public override ICellRenderer CreateDefaultRenderer()
        {
            return new DoubleCellRenderer();
        }


        /// <summary>
        /// Gets a string that specifies the name of the Column's default CellEditor
        /// </summary>
        /// <returns>A string that specifies the name of the Column's default 
        /// CellEditor</returns>
        public override string GetDefaultEditorName()
        {
            return "NUMBER";
        }


        /// <summary>
        /// Gets the Column's default CellEditor
        /// </summary>
        /// <returns>The Column's default CellEditor</returns>
        public override ICellEditor CreateDefaultEditor()
        {
            return new DoubleCellEditor();
        }

        #endregion


        #region Properties

        /// <summary>
        /// Gets or sets the maximum value for Column's Cells
        /// </summary>
        [Category("Appearance"),
        Description("The maximum value for Column's Cells")]
        public double Maximum
        {
            get => maximum;

            set
            {
                maximum = value;

                if (minimum > maximum)
                {
                    minimum = maximum;
                }

                OnPropertyChanged(new ColumnEventArgs(this, ColumnEventType.RendererChanged, null));
            }
        }


        /// <summary>
        /// Specifies whether the Maximum property should be serialized at 
        /// design time
        /// </summary>
        /// <returns>true if the Maximum property should be serialized, 
        /// false otherwise</returns>
        private bool ShouldSerializeMaximum()
        {
            return maximum != (double)100;
        }


        /// <summary>
        /// Gets or sets the minimum value for Column's Cells
        /// </summary>
        [Category("Appearance"),
        Description("The minimum value for Column's Cells")]
        public double Minimum
        {
            get => minimum;

            set
            {
                minimum = value;

                if (minimum > maximum)
                {
                    maximum = value;
                }

                OnPropertyChanged(new ColumnEventArgs(this, ColumnEventType.RendererChanged, null));
            }
        }


        /// <summary>
        /// Specifies whether the Minimum property should be serialized at 
        /// design time
        /// </summary>
        /// <returns>true if the Minimum property should be serialized, 
        /// false otherwise</returns>
        private bool ShouldSerializeMinimum()
        {
            return minimum != (double)0;
        }


        /// <summary>
        /// Gets or sets the value to increment or decrement a Cell when its up or down 
        /// buttons are clicked
        /// </summary>
        [Category("Appearance"),
        Description("The value to increment or decrement a Cell when its up or down buttons are clicked")]
        public double Increment
        {
            get => increment;

            set
            {
                if (value < 0.0)
                {
                    throw new ArgumentException("value must be greater than zero");
                }

                increment = value;
            }
        }


        /// <summary>
        /// Specifies whether the Increment property should be serialized at 
        /// design time
        /// </summary>
        /// <returns>true if the Increment property should be serialized, 
        /// false otherwise</returns>
        private bool ShouldSerializeIncrement()
        {
            return increment != (double)1;
        }


        /// <summary>
        /// Gets or sets whether the Column's Cells should draw up and down buttons
        /// </summary>
        [Category("Appearance"),
        DefaultValue(false),
        Description("Determines whether the Column's Cells draw up and down buttons")]
        public bool ShowUpDownButtons
        {
            get => showUpDownButtons;

            set
            {
                if (showUpDownButtons != value)
                {
                    showUpDownButtons = value;

                    OnPropertyChanged(new ColumnEventArgs(this, ColumnEventType.RendererChanged, null));
                }
            }
        }


        /// <summary>
        /// Gets or sets the alignment of the up and down buttons in the Column
        /// </summary>
        [Category("Appearance"),
        DefaultValue(LeftRightAlignment.Right),
        Description("The alignment of the up and down buttons in the Column"),
        Localizable(true)]
        public LeftRightAlignment UpDownAlign
        {
            get => upDownAlignment;

            set
            {
                if (!Enum.IsDefined(typeof(LeftRightAlignment), value))
                {
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(LeftRightAlignment));
                }

                if (upDownAlignment != value)
                {
                    upDownAlignment = value;

                    OnPropertyChanged(new ColumnEventArgs(this, ColumnEventType.RendererChanged, null));
                }
            }
        }


        /// <summary>
        /// Gets or sets the string that specifies how a Column's Cell contents 
        /// are formatted
        /// </summary>
        [Category("Appearance"),
        DefaultValue("G"),
        Description("A string that specifies how a column's cell contents are formatted."),
        Localizable(true)]
        public override string Format
        {
            get => base.Format;

            set => base.Format = value;
        }


        /// <summary>
        /// Gets the Type of the Comparer used to compare the Column's Cells when 
        /// the Column is sorting
        /// </summary>
        public override Type DefaultComparerType => typeof(NumberComparer);

        #endregion
    }
}
