namespace GemBox.Spreadsheet
{
    using System;

    /// <summary>
    /// Excel column contains column options and cell range with column cells.
    /// </summary>
    /// <seealso cref="T:GemBox.Spreadsheet.ExcelRow" />
    public sealed class ExcelColumn : ExcelColumnRowBase
    {
        private CellRange cells;
        private int width;

        internal ExcelColumn(ExcelColumnCollection parent, ExcelColumn sourceColumn) : base(parent, sourceColumn)
        {
            this.width = -1;
            this.width = sourceColumn.width;
        }

        internal ExcelColumn(ExcelColumnCollection parent, int index) : base(parent, index)
        {
            this.width = -1;
        }

        /// <summary>
        /// Automatically fits column width to the minimum size required for all data in the column to be visible.
        /// </summary>
        /// <remarks>
        /// <p>Auto-fit column width is a MS Excel feature and not a XLS file format feature. When columns are
        /// auto-fit in MS Excel user interface, MS Excel will calculate current character widths and 
        /// set column width to the new numeric value.</p>
        /// <p>This method will set <see cref="P:GemBox.Spreadsheet.ExcelColumn.Width">Width</see> to the approximate 
        /// value (maxCharCount * 340).</p>
        /// </remarks>
        public void AutoFit()
        {
            int length = 2;
            CellRangeEnumerator readEnumerator = this.Cells.GetReadEnumerator();
            while (readEnumerator.MoveNext())
            {
                object obj2 = readEnumerator.CurrentCell.Value;
                if (obj2 != null)
                {
                    string str = obj2.ToString();
                    if ((str != null) && (str.Length > length))
                    {
                        length = str.Length;
                    }
                }
            }
            this.width = length * 340;
        }

        /// <summary>
        /// Deletes this column from the worksheet.
        /// </summary>
        public void Delete()
        {
            foreach (ExcelRow row in base.Parent.Parent.Rows)
            {
                row.AllocatedCells.Delete(base.Index);
            }
            ((ExcelRowColumnCollectionBase) base.Parent).DeleteInternal(base.Index);
        }

        /// <summary>
        /// Inserts specified number of copied columns before the current column.
        /// </summary>
        /// <param name="columnCount">Number of columns to insert.</param>
        /// <param name="sourceColumn">Source column to copy.</param>
        public void InsertCopy(int columnCount, ExcelColumn sourceColumn)
        {
            ExcelRowCollection rows = sourceColumn.Parent.Parent.Rows;
            int num = 0;
            foreach (ExcelRow row in base.Parent.Parent.Rows)
            {
                ExcelCellCollection allocatedCells = rows[num].AllocatedCells;
                ExcelCell sourceCell = null;
                if (allocatedCells.Count > sourceColumn.Index)
                {
                    sourceCell = allocatedCells[sourceColumn.Index];
                }
                row.AllocatedCells.Insert(base.Index, columnCount, sourceCell);
                num++;
            }
            ((ExcelColumnCollection) base.Parent).InsertInternal(base.Index, columnCount, sourceColumn);
        }

        /// <summary>
        /// Inserts specified number of empty columns before the current column.
        /// </summary>
        /// <param name="columnCount">Number of columns to insert.</param>
        public void InsertEmpty(int columnCount)
        {
            foreach (ExcelRow row in base.Parent.Parent.Rows)
            {
                row.AllocatedCells.Insert(base.Index, columnCount, null);
            }
            ((ExcelColumnCollection) base.Parent).InsertInternal(base.Index, columnCount, null);
        }

        /// <summary>
        /// Gets cell range with column cells.
        /// </summary>
        /// <example> Look at following code for cell referencing examples:
        /// <code lang="Visual Basic">
        /// Dim ws As ExcelWorksheet = excelFile.Worksheets.ActiveWorksheet
        /// 
        /// ws.Cells("B2").Value = "Cell B2."
        /// ws.Cells(6, 0).Value = "Cell in row 7 and column A."
        /// 
        /// ws.Rows(2).Cells(0).Value = "Cell in row 3 and column A."
        /// ws.Rows("4").Cells("B").Value = "Cell in row 4 and column B."
        /// 
        /// ws.Columns(2).Cells(4).Value = "Cell in column C and row 5."
        /// ws.Columns("AA").Cells("6").Value = "Cell in AA column and row 6."
        /// </code>
        /// <code lang="C#">
        /// ExcelWorksheet ws = excelFile.Worksheets.ActiveWorksheet;
        /// 
        /// ws.Cells["B2"].Value = "Cell B2.";
        /// ws.Cells[6,0].Value = "Cell in row 7 and column A.";
        /// 
        /// ws.Rows[2].Cells[0].Value = "Cell in row 3 and column A.";
        /// ws.Rows["4"].Cells["B"].Value = "Cell in row 4 and column B.";
        /// 
        /// ws.Columns[2].Cells[4].Value = "Cell in column C and row 5.";
        /// ws.Columns["AA"].Cells["6"].Value = "Cell in AA column and row 6.";
        /// </code>
        /// </example>
        /// <seealso cref="T:GemBox.Spreadsheet.ExcelCell" />
        public CellRange Cells
        {
            get
            {
                if (this.cells == null)
                {
                    this.cells = new CellRange(base.Parent.Parent, 0, base.Index, 0xfffff, base.Index);
                }
                return this.cells;
            }
        }

        /// <summary>
        /// Gets or sets column width.
        /// </summary>
        /// <remarks>
        /// Unit is 1/256th of the width of the zero character in default font.
        /// </remarks>
        /// <seealso cref="P:GemBox.Spreadsheet.ExcelWorksheet.DefaultColumnWidth" />
        public int Width
        {
            get
            {
                if (this.width != -1)
                {
                    return this.width;
                }
                return base.Parent.Parent.DefaultColumnWidth;
            }
            set
            {
                this.width = value;
            }
        }
    }
}

