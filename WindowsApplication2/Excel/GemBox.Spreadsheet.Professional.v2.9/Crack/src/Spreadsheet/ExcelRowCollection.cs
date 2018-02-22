namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;
    using System.Reflection;

    /// <summary>
    /// Collection of excel rows (<see cref="T:GemBox.Spreadsheet.ExcelRow">ExcelRow</see>).
    /// </summary>
    /// <seealso cref="T:GemBox.Spreadsheet.ExcelRow" />
    public sealed class ExcelRowCollection : ExcelRowColumnCollectionBase
    {
        internal ExcelRowCollection(ExcelWorksheet parent) : base(parent)
        {
        }

        internal ExcelRowCollection(ExcelWorksheet parent, ExcelRowCollection sourceRows) : base(parent)
        {
            base.MaxOutlineLevel = sourceRows.MaxOutlineLevel;
            foreach (ExcelRow row in sourceRows)
            {
                base.Items.Add(new ExcelRow(this, row));
            }
        }

        private void AdjustArraySize(int index)
        {
            if (index > (base.Items.Count - 1))
            {
                ExceptionIfRowOutOfRange(index);
                int num2 = index - (base.Items.Count - 1);
                for (int i = 0; i < num2; i++)
                {
                    base.Items.Add(new ExcelRow(this, base.Items.Count));
                }
            }
        }

        internal static void ExceptionIfRowOutOfRange(int index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", index, "Index can't be negative.");
            }
            int num = 0xfffff;
            if (index > num)
            {
                throw new ArgumentOutOfRangeException("index", index, "Index can't be larger than maximum row index (" + num + ").");
            }
        }

        internal override void FixMergedRegionsIndexes(int index, int offset)
        {
            MergedCellRanges mergedRanges = base.Parent.MergedRanges;
            MergedCellRanges ranges2 = new MergedCellRanges(base.Parent);
            foreach (MergedCellRange range in mergedRanges.Values)
            {
                range.FixRowIndexes(index, offset);
                if ((range.Height >= 1) && ((range.Height != 1) || (range.Width != 1)))
                {
                    ranges2.AddInternal(range);
                }
            }
            base.Parent.MergedRanges = ranges2;
        }

        internal override void FixPageBreaksIndexes(int index, int offset)
        {
            HorizontalPageBreakCollection horizontalPageBreaks = base.Parent.horizontalPageBreaks;
            HorizontalPageBreakCollection breaks2 = new HorizontalPageBreakCollection();
            foreach (HorizontalPageBreak @break in horizontalPageBreaks)
            {
                if (@break.FixRowIndexes(index, offset))
                {
                    breaks2.Add(@break);
                }
            }
            base.Parent.horizontalPageBreaks = breaks2;
        }

        internal void InsertInternal(int rowIndex, int rowCount, ExcelRow sourceRow)
        {
            ArrayList list = new ArrayList();
            if (sourceRow != null)
            {
                foreach (ExcelCell cell in sourceRow.AllocatedCells)
                {
                    CellRange mergedRange = cell.MergedRange;
                    if (((mergedRange != null) && (mergedRange.FirstRowIndex == sourceRow.Index)) && (mergedRange.LastRowIndex == sourceRow.Index))
                    {
                        list.Add(mergedRange);
                    }
                }
            }
            base.FixAllIndexes(rowIndex, rowCount);
            for (int i = rowCount - 1; i >= 0; i--)
            {
                ExcelRow row;
                if (sourceRow != null)
                {
                    row = new ExcelRow(this, sourceRow);
                    row.Index = rowIndex + i;
                    foreach (CellRange range2 in list)
                    {
                        row.Cells.GetSubrangeAbsolute(row.Index, range2.FirstColumnIndex, row.Index, range2.LastColumnIndex).Merged = true;
                    }
                }
                else
                {
                    row = new ExcelRow(this, rowIndex + i);
                }
                base.Items.Insert(rowIndex, row);
            }
        }

        /// <summary>
        /// Converts row index (0, 1, ...) to row name ("1", "2", ...).
        /// </summary>
        /// <param name="rowIndex">Row index.</param>
        public static string RowIndexToName(int rowIndex)
        {
            int num = rowIndex + 1;
            return num.ToString();
        }

        /// <summary>
        /// Converts row name ("1", "2", ...) to row index (0, 1, ...).
        /// </summary>
        /// <param name="name">Row name.</param>
        public static int RowNameToIndex(string name)
        {
            return (int.Parse(name) - 1);
        }

        /// <summary>
        /// Gets the row with the specified name.
        /// </summary>
        /// <param name="name">The name of the row.</param>
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
        public ExcelRow this[string name]
        {
            get
            {
                return this[RowNameToIndex(name)];
            }
        }

        /// <overloads>Gets the row with the specified index or name.</overloads>
        /// <summary>
        /// Gets the row with the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the row.</param>
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
        public ExcelRow this[int index]
        {
            get
            {
                this.AdjustArraySize(index);
                return (ExcelRow) base.Items[index];
            }
        }
    }
}

