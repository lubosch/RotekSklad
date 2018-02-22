namespace GemBox.Spreadsheet
{
    using System;
    using System.Drawing;
    using System.Reflection;

    /// <summary>
    /// Collection of cell borders (<see cref="T:GemBox.Spreadsheet.CellBorder">CellBorder</see>).
    /// </summary>
    /// <seealso cref="T:GemBox.Spreadsheet.CellBorder" />
    public sealed class CellBorders
    {
        private CellStyle parent;

        internal CellBorders(CellStyle parent)
        {
            this.parent = parent;
        }

        internal void CopyTo(CellStyle destination)
        {
            CellStyleData element = destination.Element;
            CellStyleData data2 = this.parent.Element;
            for (int i = 0; i < 5; i++)
            {
                element.BorderColor[i] = data2.BorderColor[i];
                element.BorderStyle[i] = data2.BorderStyle[i];
            }
            element.BordersUsed = data2.BordersUsed;
        }

        /// <summary>
        /// Sets specific line color and line style on multiple borders.
        /// </summary>
        /// <param name="multipleBorders">Borders to set.</param>
        /// <param name="lineColor">Border line color.</param>
        /// <param name="lineStyle">Border line style.</param>
        public void SetBorders(MultipleBorders multipleBorders, Color lineColor, LineStyle lineStyle)
        {
            for (int i = 0; i < 6; i++)
            {
                IndividualBorder individualBorder = (IndividualBorder) i;
                if ((multipleBorders & CellBorder.MultipleFromIndividualBorder(individualBorder)) != MultipleBorders.None)
                {
                    this[individualBorder].SetBorder(lineColor, lineStyle);
                }
            }
        }

        /// <summary>
        /// Gets specific border.
        /// </summary>
        /// <param name="individualBorder">Border to get.</param>
        public CellBorder this[IndividualBorder individualBorder]
        {
            get
            {
                return new CellBorder(this.parent, individualBorder);
            }
        }
    }
}

