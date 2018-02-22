namespace GemBox.Spreadsheet
{
    using System;
    using System.ComponentModel;
    using System.Drawing;

    /// <summary>
    /// Base class for classes representing one or more excel cells.
    /// </summary>
    public abstract class AbstractRange
    {
        private ExcelWorksheet parent;

        /// <summary>
        /// Internal.
        /// </summary>
        /// <param name="parent"></param>
        /// <exclude />
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected AbstractRange(ExcelWorksheet parent)
        {
            this.parent = parent;
        }

        internal void CheckMultiline(object val)
        {
            string str = val as string;
            if ((str != null) && (str.IndexOf('\n') != -1))
            {
                this.Style.WrapText = true;
            }
        }

        /// <summary>
        /// Sets borders on one or more excel cells, taking cell position into account.
        /// </summary>
        /// <param name="multipleBorders">Borders to set.</param>
        /// <param name="lineColor">Line color.</param>
        /// <param name="lineStyle">Line style.</param>
        public abstract void SetBorders(MultipleBorders multipleBorders, Color lineColor, LineStyle lineStyle);

        /// <summary>
        /// Gets is sets comment
        /// </summary>
        public abstract ExcelComment Comment { get; set; }

        /// <summary>
        /// Gets or sets formula string.
        /// </summary>
        public abstract string Formula { get; set; }

        /// <summary>
        /// Returns <b>true</b> if all cells in <see cref="T:GemBox.Spreadsheet.AbstractRange">AbstractRange</see> have default 
        /// cell style; otherwise, <b>false</b>.
        /// </summary>
        public abstract bool IsStyleDefault { get; }

        internal ExcelWorksheet Parent
        {
            get
            {
                return this.parent;
            }
        }

        /// <summary>
        /// Gets or sets cell style (<see cref="T:GemBox.Spreadsheet.CellStyle">CellStyle</see>) on one or more excel cells.
        /// </summary>
        public abstract CellStyle Style { get; set; }

        /// <summary>
        /// Gets or sets cell value on one or more excel cells.
        /// </summary>
        public abstract object Value { get; set; }
    }
}

