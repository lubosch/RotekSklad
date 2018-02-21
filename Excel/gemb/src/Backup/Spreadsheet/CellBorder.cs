namespace GemBox.Spreadsheet
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Contains settings for a single cell border.
    /// </summary>
    /// <remarks>
    /// Note that although diagonal-up (<see cref="F:GemBox.Spreadsheet.IndividualBorder.DiagonalUp">IndividualBorder.DiagonalUp</see> 
    /// or <see cref="F:GemBox.Spreadsheet.MultipleBorders.DiagonalUp">MultipleBorders.DiagonalUp</see>) and diagonal-down 
    /// (<see cref="F:GemBox.Spreadsheet.IndividualBorder.DiagonalDown">IndividualBorder.DiagonalDown</see> or
    /// <see cref="F:GemBox.Spreadsheet.MultipleBorders.DiagonalDown">MultipleBorders.DiagonalDown</see>) can be individually set, 
    /// they share the same color and the same line style. This is a Microsoft Excel limitation.
    /// </remarks>
    /// <seealso cref="T:GemBox.Spreadsheet.CellBorders" />
    public sealed class CellBorder
    {
        private readonly IndividualBorder borderId;
        private readonly int borderIndex;
        private readonly CellStyle parent;

        internal CellBorder(CellStyle parent, IndividualBorder borderId)
        {
            this.parent = parent;
            this.borderId = borderId;
            this.borderIndex = IndexFromIndividualBorder(borderId);
        }

        internal static int IndexFromIndividualBorder(IndividualBorder individualBorder)
        {
            int num = (int) individualBorder;
            if (num == 5)
            {
                num = 4;
            }
            return num;
        }

        internal static MultipleBorders MultipleFromIndividualBorder(IndividualBorder individualBorder)
        {
			  return (MultipleBorders)(((int)1) << (int)individualBorder);
        }

        /// <summary>
        /// Sets both border line color and line style.
        /// </summary>
        /// <param name="lineColor">Border line color.</param>
        /// <param name="lineStyle">Border line style.</param>
        /// <remarks>
        /// Note that although diagonal-up (<see cref="F:GemBox.Spreadsheet.IndividualBorder.DiagonalUp">IndividualBorder.DiagonalUp</see> 
        /// or <see cref="F:GemBox.Spreadsheet.MultipleBorders.DiagonalUp">MultipleBorders.DiagonalUp</see>) and diagonal-down 
        /// (<see cref="F:GemBox.Spreadsheet.IndividualBorder.DiagonalDown">IndividualBorder.DiagonalDown</see> or
        /// <see cref="F:GemBox.Spreadsheet.MultipleBorders.DiagonalDown">MultipleBorders.DiagonalDown</see>) can be individually set, 
        /// they share the same color and the same line style. This is a Microsoft Excel limitation.
        /// </remarks>
        public void SetBorder(Color lineColor, GemBox.Spreadsheet.LineStyle lineStyle)
        {
            this.LineColor = lineColor;
            this.LineStyle = lineStyle;
        }

        private void SetUsedIfNotDefault()
        {
            CellStyleData element = this.parent.Element;
            if ((element.BorderStyle[this.borderIndex] != GemBox.Spreadsheet.LineStyle.None) || (element.BorderColor[this.borderIndex].ToArgb() != Color.Black.ToArgb()))
            {
                element.BordersUsed |= MultipleFromIndividualBorder(this.borderId);
            }
        }

        /// <summary>
        /// Gets or sets border line color.
        /// </summary>
        /// <remarks>
        /// Note that although diagonal-up (<see cref="F:GemBox.Spreadsheet.IndividualBorder.DiagonalUp">IndividualBorder.DiagonalUp</see> 
        /// or <see cref="F:GemBox.Spreadsheet.MultipleBorders.DiagonalUp">MultipleBorders.DiagonalUp</see>) and diagonal-down 
        /// (<see cref="F:GemBox.Spreadsheet.IndividualBorder.DiagonalDown">IndividualBorder.DiagonalDown</see> or
        /// <see cref="F:GemBox.Spreadsheet.MultipleBorders.DiagonalDown">MultipleBorders.DiagonalDown</see>) can be individually set, 
        /// they share the same color and the same line style. This is a Microsoft Excel limitation.
        /// </remarks>
        public Color LineColor
        {
            get
            {
                return this.parent.Element.BorderColor[this.borderIndex];
            }
            set
            {
                this.parent.BeforeChange();
                this.parent.Element.BorderColor[this.borderIndex] = value;
                this.SetUsedIfNotDefault();
            }
        }

        /// <summary>
        /// Gets or sets border line style.
        /// </summary>
        /// <remarks>
        /// Note that although diagonal-up (<see cref="F:GemBox.Spreadsheet.IndividualBorder.DiagonalUp">IndividualBorder.DiagonalUp</see> 
        /// or <see cref="F:GemBox.Spreadsheet.MultipleBorders.DiagonalUp">MultipleBorders.DiagonalUp</see>) and diagonal-down 
        /// (<see cref="F:GemBox.Spreadsheet.IndividualBorder.DiagonalDown">IndividualBorder.DiagonalDown</see> or
        /// <see cref="F:GemBox.Spreadsheet.MultipleBorders.DiagonalDown">MultipleBorders.DiagonalDown</see>) can be individually set, 
        /// they share the same color and the same line style. This is a Microsoft Excel limitation.
        /// </remarks>
        public GemBox.Spreadsheet.LineStyle LineStyle
        {
            get
            {
                return this.parent.Element.BorderStyle[this.borderIndex];
            }
            set
            {
                this.parent.BeforeChange();
                this.parent.Element.BorderStyle[this.borderIndex] = value;
                this.SetUsedIfNotDefault();
            }
        }
    }
}

