namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;
    using System.Reflection;

    /// <summary>
    /// Collection of excel columns (<see cref="T:GemBox.Spreadsheet.ExcelColumn">ExcelColumn</see>).
    /// </summary>
    /// <seealso cref="T:GemBox.Spreadsheet.ExcelColumn" />
    public sealed class ExcelColumnCollection : ExcelRowColumnCollectionBase
    {
        internal ExcelColumnCollection(ExcelWorksheet parent) : base(parent)
        {
        }

        internal ExcelColumnCollection(ExcelWorksheet parent, ExcelColumnCollection sourceColumns) : base(parent)
        {
            base.MaxOutlineLevel = sourceColumns.MaxOutlineLevel;
            foreach (ExcelColumn column in sourceColumns)
            {
                base.Items.Add(new ExcelColumn(this, column));
            }
        }

        private void AdjustArraySize(int index)
        {
            if (index > (base.Items.Count - 1))
            {
                ExceptionIfColumnOutOfRange(index);
                int num2 = index - (base.Items.Count - 1);
                for (int i = 0; i < num2; i++)
                {
                    base.Items.Add(new ExcelColumn(this, base.Items.Count));
                }
            }
        }

        /// <summary>
        /// Converts column index (0, 1, ...) to column name ("A", "B", ...).
        /// </summary>
        /// <param name="columnIndex">Column index.</param>
        public static string ColumnIndexToName(int columnIndex)
        {
            string str = "";
            do
            {
                str = ((char) (0x41 + (columnIndex % 0x1a))).ToString() + str;
                columnIndex = (columnIndex / 0x1a) - 1;
            }
            while (columnIndex >= 0);
            return str;
        }

        /// <summary>
        /// Converts column name ("A", "B", ...) to column index (0, 1, ...).
        /// </summary>
        /// <param name="name">Column name.</param>
        public static int ColumnNameToIndex(string name)
        {
            int letterIndex = -1;
            if (name.Length == 1)
            {
                letterIndex = GetLetterIndex(name[0]);
            }
            else if (name.Length == 2)
            {
                letterIndex = ((GetLetterIndex(name[0]) + 1) * 0x1a) + GetLetterIndex(name[1]);
            }
            if ((letterIndex < 0) || (letterIndex > 0xff))
            {
                throw new ArgumentOutOfRangeException("name", name, "Column name must be one-letter or two-letter name from A to IV.");
            }
            return letterIndex;
        }

        internal static void ExceptionIfColumnOutOfRange(int index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", index, "Index can't be negative.");
            }
            int num = 0x3fff;
            if (index > num)
            {
                throw new ArgumentOutOfRangeException("index", index, "Index can't be lager than maximum column index (" + num + ").");
            }
        }

        internal override void FixMergedRegionsIndexes(int index, int offset)
        {
            MergedCellRanges mergedRanges = base.Parent.MergedRanges;
            MergedCellRanges ranges2 = new MergedCellRanges(base.Parent);
            foreach (MergedCellRange range in mergedRanges.Values)
            {
                range.FixColumnIndexes(index, offset);
                if ((range.Width >= 1) && ((range.Height != 1) || (range.Width != 1)))
                {
                    ranges2.AddInternal(range);
                }
            }
            base.Parent.MergedRanges = ranges2;
        }

        internal override void FixPageBreaksIndexes(int index, int offset)
        {
            VerticalPageBreakCollection verticalPageBreaks = base.Parent.verticalPageBreaks;
            VerticalPageBreakCollection breaks2 = new VerticalPageBreakCollection();
            foreach (VerticalPageBreak @break in verticalPageBreaks)
            {
                if (@break.FixColumnIndexes(index, offset))
                {
                    breaks2.Add(@break);
                }
            }
            base.Parent.verticalPageBreaks = breaks2;
        }

        private static int GetLetterIndex(char letter)
        {
            int num = char.ToUpper(letter) - 'A';
            if ((num < 0) || (num > 0x19))
            {
                throw new ArgumentOutOfRangeException("letter", letter, "Column name must be made from valid letters of English alphabet.");
            }
            return num;
        }

        internal void InsertInternal(int colIndex, int columnCount, ExcelColumn sourceColumn)
        {
            ArrayList list = new ArrayList();
            if (sourceColumn != null)
            {
                foreach (MergedCellRange range in base.Parent.MergedRanges.Values)
                {
                    if ((range.FirstColumnIndex == sourceColumn.Index) && (range.LastColumnIndex == sourceColumn.Index))
                    {
                        list.Add(range);
                    }
                }
            }
            base.FixAllIndexes(colIndex, columnCount);
            for (int i = columnCount - 1; i >= 0; i--)
            {
                ExcelColumn column;
                if (sourceColumn != null)
                {
                    column = new ExcelColumn(this, sourceColumn);
                    column.Index = colIndex + i;
                    foreach (CellRange range2 in list)
                    {
                        column.Cells.GetSubrangeAbsolute(range2.FirstRowIndex, column.Index, range2.LastRowIndex, column.Index).Merged = true;
                    }
                }
                else
                {
                    column = new ExcelColumn(this, colIndex + i);
                }
                base.Items.Insert(colIndex, column);
            }
        }

        /// <summary>
        /// Gets the column with the specified name.
        /// </summary>
        /// <param name="name">The name of the column.</param>
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
        public ExcelColumn this[string name]
        {
            get
            {
                return this[ColumnNameToIndex(name)];
            }
        }

        /// <overloads>Gets the column with the specified index or name.</overloads>
        /// <summary>
        /// Gets the column with the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the column.</param>
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
        public ExcelColumn this[int index]
        {
            get
            {
                this.AdjustArraySize(index);
                return (ExcelColumn) base.Items[index];
            }
        }
    }
}

