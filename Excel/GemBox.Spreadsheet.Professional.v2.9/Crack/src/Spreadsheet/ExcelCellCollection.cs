namespace GemBox.Spreadsheet
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Collection of excel cells (<see cref="T:GemBox.Spreadsheet.ExcelCell">ExcelCell</see>).
    /// </summary>
    /// <seealso cref="T:GemBox.Spreadsheet.ExcelCell" />
    public class ExcelCellCollection : ExcelRowColumnCellCollectionBase
    {
        internal ExcelCellCollection(ExcelWorksheet parent) : base(parent)
        {
        }

        internal ExcelCellCollection(ExcelWorksheet parent, ExcelCellCollection sourceCells) : base(parent)
        {
            foreach (ExcelCell cell in sourceCells)
            {
                base.Items.Add(new ExcelCell(base.Parent, cell));
            }
        }

        private void AdjustArraySize(int index)
        {
            if (index > (base.Items.Count - 1))
            {
                ExcelColumnCollection.ExceptionIfColumnOutOfRange(index);
                int num2 = index - (base.Items.Count - 1);
                for (int i = 0; i < num2; i++)
                {
                    base.Items.Add(new ExcelCell(base.Parent));
                }
            }
        }

        internal void Delete(int index)
        {
            if (base.Count > index)
            {
                base.Items.RemoveAt(index);
            }
        }

        internal void Insert(int index, int count, ExcelCell sourceCell)
        {
            if (sourceCell != null)
            {
                this.AdjustArraySize(index);
            }
            if (base.Count > index)
            {
                for (int i = 0; i < count; i++)
                {
                    ExcelCell cell;
                    if (sourceCell == null)
                    {
                        cell = new ExcelCell(base.Parent);
                    }
                    else
                    {
                        cell = new ExcelCell(base.Parent, sourceCell);
                    }
                    base.Items.Insert(index, cell);
                }
            }
        }

        /// <summary>
        /// Gets the cell with the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the cell.</param>
        public ExcelCell this[int index]
        {
            get
            {
                this.AdjustArraySize(index);
                return (ExcelCell) base.Items[index];
            }
        }
    }
}

