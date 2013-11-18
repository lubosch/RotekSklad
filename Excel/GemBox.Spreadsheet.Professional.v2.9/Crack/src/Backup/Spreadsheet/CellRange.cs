namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Reflection;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Cell range is a rectangular group of worksheet cells.
    /// </summary>
    /// <remarks>
    /// <p>Cell range is determined by its top (<see cref="P:GemBox.Spreadsheet.CellRange.FirstRowIndex">FirstRowIndex</see>), 
    /// left (<see cref="P:GemBox.Spreadsheet.CellRange.FirstColumnIndex">FirstColumnIndex</see>), 
    /// bottom (<see cref="P:GemBox.Spreadsheet.CellRange.LastRowIndex">LastRowIndex</see>) and 
    /// right (<see cref="P:GemBox.Spreadsheet.CellRange.LastColumnIndex">LastColumnIndex</see>) borders. This properties 
    /// are read-only, so if you require different cell range use one of GetSubrange methods 
    /// (<see cref="M:GemBox.Spreadsheet.CellRange.GetSubrangeAbsolute(System.Int32,System.Int32,System.Int32,System.Int32)">GetSubrangeAbsolute</see>, 
    /// <see cref="M:GemBox.Spreadsheet.CellRange.GetSubrangeRelative(System.Int32,System.Int32,System.Int32,System.Int32)">GetSubrangeRelative</see> or 
    /// <see cref="M:GemBox.Spreadsheet.CellRange.GetSubrange(System.String,System.String)">GetSubrange</see>). Specific cell can be accessed in a few 
    /// ways, depending on <see cref="P:GemBox.Spreadsheet.CellRange.IndexingMode">IndexingMode</see>. Cells in the 
    /// range can be merged / unmerged by the use of <see cref="P:GemBox.Spreadsheet.CellRange.Merged">Merged</see> 
    /// property.</p>
    /// <p><see cref="P:GemBox.Spreadsheet.CellRange.Value">Value</see> property set will set value of multiple cells 
    /// or of a merged range. <see cref="P:GemBox.Spreadsheet.CellRange.Value">Value</see> property get has meaning 
    /// only if range is merged; otherwise, exception is thrown.</p>
    /// <p><see cref="P:GemBox.Spreadsheet.CellRange.Style">Style</see> property set will set style of multiple cells 
    /// or of a merged range. <see cref="P:GemBox.Spreadsheet.CellRange.Style">Style</see> property get has meaning 
    /// only if range is merged; otherwise, exception is thrown.</p>
    /// <p> Note that for <see cref="P:GemBox.Spreadsheet.CellRange.Style">Style</see> property set on a cell range that 
    /// is not merged, you can't use the following format:
    /// <code lang="Visual Basic">
    /// Dim cr As CellRange = excelFile.Worksheets(0).Rows(1).Cells
    /// cr.Style.Rotation = 30
    /// </code>
    /// <code lang="C#">
    /// CellRange cr = excelFile.Worksheets[0].Rows[1].Cells;
    /// cr.Style.Rotation = 30;
    /// </code>
    /// because that would first call <see cref="P:GemBox.Spreadsheet.CellRange.Style">Style</see> property get method and that 
    /// will certainly fail because <see cref="P:GemBox.Spreadsheet.CellRange.Style">Style</see> property get is defined only 
    /// for a merged cell range. </p><p>Instead you can use two different code patterns, depending on whether you want to replace or combine the existing 
    /// cell range styles with the new style.</p><p>
    /// If you want to <b>replace</b> cell style on every cell in a cell range use the following code:
    /// <code lang="Visual Basic">
    /// Dim cr As CellRange = excelFile.Worksheets(0).Rows(1).Cells
    /// Dim style As CellStyle = New CellStyle()
    /// style.Rotation = 30
    /// cr.Style = style
    /// </code>
    /// <code lang="C#">
    /// CellRange cr = excelFile.Worksheets[0].Rows[1].Cells;
    /// CellStyle style = new CellStyle();
    /// style.Rotation = 30;
    /// cr.Style = style;
    /// </code>
    /// </p><p>
    /// If you want to <b>set</b> cell style property on every cell in a cell range (other cell style property values will 
    /// remain unchanged) use the following code:
    /// <code lang="Visual Basic">
    /// Dim cell As ExcelCell
    /// For Each cell In excelFile.Worksheets(0).Rows(1).Cells
    /// cell.Style.Rotation = 30
    /// Next
    /// </code>
    /// <code lang="C#">
    /// foreach(ExcelCell cell in excelFile.Worksheets[0].Rows[1].Cells)
    /// cell.Style.Rotation = 30;
    /// </code>
    /// </p>
    /// </remarks>
    /// <example> Following code creates horizontal, vertical and rectangular cell ranges and demonstrates how 
    /// indexing works different in different context. <see cref="M:GemBox.Spreadsheet.CellRange.SetBorders(GemBox.Spreadsheet.MultipleBorders,System.Drawing.Color,GemBox.Spreadsheet.LineStyle)">SetBorders</see> 
    /// method is used to mark outside borders of the rectangular range.
    /// <code lang="Visual Basic">
    /// Dim cr As CellRange = excelFile.Worksheets(0).Rows(1).Cells
    /// 
    /// cr(0).Value = cr.IndexingMode
    /// cr(3).Value = "D2"
    /// cr("B").Value = "B2"
    /// 
    /// cr = excelFile.Worksheets(0).Columns(4).Cells
    /// 
    /// cr(0).Value = cr.IndexingMode
    /// cr(2).Value = "E3"
    /// cr("5").Value = "E5"
    /// 
    /// cr = excelFile.Worksheets(0).Cells.GetSubrange("F2", "J8")
    /// cr.SetBorders(MultipleBorders.Outside, Color.Navy, LineStyle.Dashed)
    /// 
    /// cr("I7").Value = cr.IndexingMode
    /// cr(0, 0).Value = "F2"
    /// cr("G3").Value = "G3"
    /// cr(5).Value = "F3" <font color="Green">' Cell range width is 5 (F G H I J).</font>
    /// </code>
    /// <code lang="C#">
    /// CellRange cr = excelFile.Worksheets[0].Rows[1].Cells;				
    /// 
    /// cr[0].Value = cr.IndexingMode;
    /// cr[3].Value = "D2";
    /// cr["B"].Value = "B2";
    /// 
    /// cr = excelFile.Worksheets[0].Columns[4].Cells;
    /// 
    /// cr[0].Value = cr.IndexingMode;
    /// cr[2].Value = "E3";
    /// cr["5"].Value = "E5";
    /// 
    /// cr = excelFile.Worksheets[0].Cells.GetSubrange("F2", "J8");
    /// cr.SetBorders(MultipleBorders.Outside, Color.Navy, LineStyle.Dashed);
    /// 
    /// cr["I7"].Value = cr.IndexingMode;
    /// cr[0,0].Value = "F2";
    /// cr["G3"].Value = "G3";
    /// cr[5].Value = "F3"; <font color="Green">// Cell range width is 5 (F G H I J).</font>
    /// </code>
    /// </example>
    public class CellRange : AbstractRange, IEnumerable
    {
        private int firstColumn;
        private int firstRow;
        private int lastColumn;
        private int lastRow;

        internal CellRange(ExcelWorksheet parent) : base(parent)
        {
            this.lastRow = 0xfffff;
            this.lastColumn = 0x3fff;
        }

        internal CellRange(ExcelWorksheet parent, int firstRow, int firstColumn, int lastRow, int lastColumn) : base(parent)
        {
            ExcelRowCollection.ExceptionIfRowOutOfRange(firstRow);
            ExcelColumnCollection.ExceptionIfColumnOutOfRange(firstColumn);
            ExcelRowCollection.ExceptionIfRowOutOfRange(lastRow);
            ExcelColumnCollection.ExceptionIfColumnOutOfRange(lastColumn);
            if (lastRow < firstRow)
            {
                throw new ArgumentOutOfRangeException("", lastRow, "Argument lastRow can't be smaller than firstRow.");
            }
            if (lastColumn < firstColumn)
            {
                throw new ArgumentOutOfRangeException("", lastColumn, "Argument lastColumn can't be smaller than firstColumn.");
            }
            this.firstRow = firstRow;
            this.firstColumn = firstColumn;
            this.lastRow = lastRow;
            this.lastColumn = lastColumn;
        }

        /// <summary>
        /// Copies this cell range to another position in the same worksheet.
        /// </summary>
        /// <param name="topLeftCell">Full name of the top-left cell of the destination range.</param>
        /// <remarks>
        /// <p><i>topLeftCell</i> specifies position of the top-left cell of 
        /// the destination cell range.</p>
        /// <p>Destination cell range has the same width and height as this cell range.</p>
        /// <p><see cref="T:System.ArgumentOutOfRangeException" /> is thrown if destination range:
        /// <list type="bullet">
        /// <item><description>breaks Excel worksheet row or column limit,</description></item>
        /// <item><description>overlaps with source range, or</description></item>
        /// <item><description>overlaps with existing merged range.</description></item>
        /// </list></p>
        /// </remarks>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Thrown if destination range is invalid.</exception>
        public void CopyTo(string topLeftCell)
        {
            this.CopyTo(base.Parent, topLeftCell);
        }

        /// <summary>
        /// Copies this cell range to another worksheet.
        /// </summary>
        /// <param name="destinationWorksheet">Destination worksheet.</param>
        /// <param name="topLeftCell">Full name of the top-left cell of the destination range.</param>
        /// <remarks>
        /// <p><i>topLeftCell</i> specifies position of the top-left cell of 
        /// the destination cell range.</p>
        /// <p>Destination cell range has the same width and height as this cell range.</p>
        /// <p><see cref="T:System.ArgumentOutOfRangeException" /> is thrown if destination range:
        /// <list type="bullet">
        /// <item><description>breaks Excel worksheet row or column limit,</description></item>
        /// <item><description>overlaps with source range, or</description></item>
        /// <item><description>overlaps with existing merged range.</description></item>
        /// </list></p>
        /// </remarks>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Thrown if destination range is invalid.</exception>
        public void CopyTo(ExcelWorksheet destinationWorksheet, string topLeftCell)
        {
            int num;
            int num2;
            PositionToRowColumn(topLeftCell, out num, out num2);
            this.CopyTo(destinationWorksheet, num, num2);
        }

        /// <overloads>Copies this cell range to the specified position.</overloads>
        /// <summary>
        /// Copies this cell range to another position in the same worksheet.
        /// </summary>
        /// <param name="absoluteRow">Absolute index of the destination row.</param>
        /// <param name="absoluteColumn">Absolute index of the destination column.</param>
        /// <remarks>
        /// <p><i>absoluteRow</i> and <i>absoluteColumn</i> specify position of the top-left cell of 
        /// the destination cell range.</p>
        /// <p>Destination cell range has the same width and height as this cell range.</p>
        /// <p><see cref="T:System.ArgumentOutOfRangeException" /> is thrown if destination range:
        /// <list type="bullet">
        /// <item><description>breaks Excel worksheet row or column limit,</description></item>
        /// <item><description>overlaps with source range, or</description></item>
        /// <item><description>overlaps with existing merged range.</description></item>
        /// </list></p>
        /// </remarks>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Thrown if destination range is invalid.</exception>
        public void CopyTo(int absoluteRow, int absoluteColumn)
        {
            this.CopyTo(base.Parent, absoluteRow, absoluteColumn);
        }

        /// <summary>
        /// Copies this cell range to another worksheet.
        /// </summary>
        /// <param name="destinationWorksheet">Destination worksheet.</param>
        /// <param name="absoluteRow">Absolute index of the destination row.</param>
        /// <param name="absoluteColumn">Absolute index of the destination column.</param>
        /// <remarks>
        /// <p><i>absoluteRow</i> and <i>absoluteColumn</i> specify position of the top-left cell of 
        /// the destination cell range.</p>
        /// <p>Destination cell range has the same width and height as this cell range.</p>
        /// <p><see cref="T:System.ArgumentOutOfRangeException" /> is thrown if destination range:
        /// <list type="bullet">
        /// <item><description>breaks Excel worksheet row or column limit,</description></item>
        /// <item><description>overlaps with source range, or</description></item>
        /// <item><description>overlaps with existing merged range.</description></item>
        /// </list></p>
        /// </remarks>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Thrown if destination range is invalid.</exception>
        public void CopyTo(ExcelWorksheet destinationWorksheet, int absoluteRow, int absoluteColumn)
        {
            CellRange range;
            try
            {
                range = destinationWorksheet.Cells.GetSubrangeAbsolute(absoluteRow, absoluteColumn, (absoluteRow + this.Height) - 1, (absoluteColumn + this.Width) - 1);
            }
            catch (Exception)
            {
                throw new ArgumentException("Destination range is incorrectly specified.");
            }
            if (this.Overlaps(range))
            {
                throw new ArgumentException("Destination range can't overlap with source range.");
            }
            if (range.IsAnyCellMerged)
            {
                throw new ArgumentException("Destination range can't overlap with existing merged range.");
            }
            int num = absoluteRow - this.firstRow;
            int num2 = absoluteColumn - this.firstColumn;
            Hashtable hashtable = new Hashtable();
            CellRangeEnumerator readEnumerator = this.GetReadEnumerator();
            while (readEnumerator.MoveNext())
            {
                ExcelCell currentCell = readEnumerator.CurrentCell;
                ExcelCell cell2 = destinationWorksheet.Rows[readEnumerator.CurrentRow + num].AllocatedCells[readEnumerator.CurrentColumn + num2];
                cell2.Value = currentCell.Value;
                cell2.Style = currentCell.Style;
                CellRange mergedRange = currentCell.MergedRange;
                if (mergedRange != null)
                {
                    hashtable[mergedRange] = mergedRange;
                }
            }
            foreach (MergedCellRange range3 in hashtable.Values)
            {
                destinationWorksheet.Cells.GetSubrangeAbsolute(range3.firstRow + num, range3.firstColumn + num2, range3.lastRow + num, range3.lastColumn + num2).Merged = true;
            }
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:GemBox.Spreadsheet.CellRange">CellRange</see> is equal 
        /// to the current <see cref="T:GemBox.Spreadsheet.CellRange">CellRange</see>.
        /// </summary>
        /// <param name="obj">Object of <see cref="T:GemBox.Spreadsheet.CellRange">CellRange</see> type.</param>
        /// <returns><b>true</b> if the specified <see cref="T:GemBox.Spreadsheet.CellRange">CellRange</see> is equal 
        /// to the current <see cref="T:GemBox.Spreadsheet.CellRange">CellRange</see>; otherwise, <b>false</b>.</returns>
        public override bool Equals(object obj)
        {
            CellRange range = (CellRange) obj;
            return ((((this.firstRow == range.firstRow) && (this.firstColumn == range.firstColumn)) && (this.lastRow == range.lastRow)) && (this.lastColumn == range.lastColumn));
        }

        internal void ExceptionIfRowColumnOutOfRange(int row, int column)
        {
            if (this.IsRowColumnOutOfRange(row, column))
            {
                throw new ArgumentException("Row or column index is invalid or out of required range.");
            }
        }

        /// <summary>
        /// Finds the first occurrence of the specified text in the current cell range.
        /// </summary>
        /// <param name="text">Test to search.</param>
        /// <param name="matchCase"><b>True</b> to match exact case, <b>false</b> otherwise.</param>
        /// <param name="matchEntireCellContents"><b>True</b> to match entire cell contents, <b>false</b> otherwise.</param>
        /// <param name="row">Index of the row where the text was found or -1 if no text was found.</param>
        /// <param name="column">Index of the column where the text was found or -1 if no text was found.</param>
        /// <returns><b>True</b> if text is found, <b>false</b> otherwise.</returns>
        public bool FindText(string text, bool matchCase, bool matchEntireCellContents, out int row, out int column)
        {
            CellRangeEnumerator readEnumerator = this.GetReadEnumerator();
            if (!matchCase)
            {
                text = text.ToLower();
            }
            while (readEnumerator.MoveNext())
            {
                object obj2 = readEnumerator.CurrentCell.Value;
                if (obj2 != null)
                {
                    string str = obj2.ToString();
                    if (str != null)
                    {
                        if (!matchCase)
                        {
                            str = str.ToLower();
                        }
                        if (matchEntireCellContents)
                        {
                            if (str == text)
                            {
                                row = readEnumerator.CurrentRow;
                                column = readEnumerator.CurrentColumn;
                                return true;
                            }
                        }
                        else if (str.IndexOf(text) != -1)
                        {
                            row = readEnumerator.CurrentRow;
                            column = readEnumerator.CurrentColumn;
                            return true;
                        }
                    }
                }
            }
            row = -1;
            column = -1;
            return false;
        }

        internal void FixFirstColumnIndex(int colIndex, int offset)
        {
            this.firstColumn = Math.Min(Math.Max(this.firstColumn + offset, colIndex), 0x3fff);
        }

        internal void FixFirstRowIndex(int rowIndex, int offset)
        {
            this.firstRow = Math.Min(Math.Max(this.firstRow + offset, rowIndex), 0xfffff);
        }

        internal void FixLastColumnIndex(int colIndex, int offset)
        {
            this.lastColumn = Math.Min(Math.Max((int) (this.lastColumn + offset), (int) (colIndex - 1)), 0x3fff);
        }

        internal void FixLastRowIndex(int rowIndex, int offset)
        {
            this.lastRow = Math.Min(Math.Max((int) (this.lastRow + offset), (int) (rowIndex - 1)), 0xfffff);
        }

        /// <summary>
        /// Returns an enumerator for the <see cref="T:GemBox.Spreadsheet.CellRange">CellRange</see> (all cells).
        /// </summary>
        /// <remarks>
        /// Returns default enumerator that iterates all cells in the range. If you are only reading existing
        /// cells (values or formatting), use more appropriate <see cref="M:GemBox.Spreadsheet.CellRange.GetReadEnumerator" />.
        /// </remarks>
        public IEnumerator GetEnumerator()
        {
            return new CellRangeEnumerator(this, false);
        }

        /// <summary>
        /// Returns the hash code of this object.
        /// </summary>
        /// <returns>Hash code.</returns>
        public override int GetHashCode()
        {
            return (((this.firstRow ^ this.firstColumn) ^ this.lastRow) ^ this.lastColumn);
        }

        /// <summary>
        /// Returns enumerator for the <see cref="T:GemBox.Spreadsheet.CellRange">CellRange</see> (only allocated cells).
        /// </summary>
        /// <remarks>
        /// Returns enumerator that iterates only already allocated cells in the range. If you are only reading existing
        /// cells (values or formatting), use this enumerator as it is faster and doesn't allocate unnecessary cells.
        /// </remarks>
        public CellRangeEnumerator GetReadEnumerator()
        {
            return new CellRangeEnumerator(this, true);
        }

        /// <summary>
        /// Returns new cell range using start and end position.
        /// </summary>
        /// <param name="firstCell">Name of first (top-left) cell.</param>
        /// <param name="lastCell">Name of last (bottom-right) cell.</param>
        /// <remarks>
        /// New cell range must be within this cell range.
        /// </remarks>
        /// <example> Following code creates horizontal, vertical and rectangular cell ranges and demonstrates how 
        /// indexing works different in different context. <see cref="M:GemBox.Spreadsheet.CellRange.SetBorders(GemBox.Spreadsheet.MultipleBorders,System.Drawing.Color,GemBox.Spreadsheet.LineStyle)">SetBorders</see> 
        /// method is used to mark outside borders of the rectangular range.
        /// <code lang="Visual Basic">
        /// Dim cr As CellRange = excelFile.Worksheets(0).Rows(1).Cells
        /// 
        /// cr(0).Value = cr.IndexingMode
        /// cr(3).Value = "D2"
        /// cr("B").Value = "B2"
        /// 
        /// cr = excelFile.Worksheets(0).Columns(4).Cells
        /// 
        /// cr(0).Value = cr.IndexingMode
        /// cr(2).Value = "E3"
        /// cr("5").Value = "E5"
        /// 
        /// cr = excelFile.Worksheets(0).Cells.GetSubrange("F2", "J8")
        /// cr.SetBorders(MultipleBorders.Outside, Color.Navy, LineStyle.Dashed)
        /// 
        /// cr("I7").Value = cr.IndexingMode
        /// cr(0, 0).Value = "F2"
        /// cr("G3").Value = "G3"
        /// cr(5).Value = "F3" <font color="Green">' Cell range width is 5 (F G H I J).</font>
        /// </code>
        /// <code lang="C#">
        /// CellRange cr = excelFile.Worksheets[0].Rows[1].Cells;				
        /// 
        /// cr[0].Value = cr.IndexingMode;
        /// cr[3].Value = "D2";
        /// cr["B"].Value = "B2";
        /// 
        /// cr = excelFile.Worksheets[0].Columns[4].Cells;
        /// 
        /// cr[0].Value = cr.IndexingMode;
        /// cr[2].Value = "E3";
        /// cr["5"].Value = "E5";
        /// 
        /// cr = excelFile.Worksheets[0].Cells.GetSubrange("F2", "J8");
        /// cr.SetBorders(MultipleBorders.Outside, Color.Navy, LineStyle.Dashed);
        /// 
        /// cr["I7"].Value = cr.IndexingMode;
        /// cr[0,0].Value = "F2";
        /// cr["G3"].Value = "G3";
        /// cr[5].Value = "F3"; <font color="Green">// Cell range width is 5 (F G H I J).</font>
        /// </code>
        /// </example>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Thrown if arguments are out of range.</exception>
        /// <seealso cref="P:GemBox.Spreadsheet.CellRange.StartPosition" />
        /// <seealso cref="P:GemBox.Spreadsheet.CellRange.EndPosition" />
        /// <seealso cref="M:GemBox.Spreadsheet.CellRange.GetSubrangeAbsolute(System.Int32,System.Int32,System.Int32,System.Int32)" />
        /// <seealso cref="M:GemBox.Spreadsheet.CellRange.GetSubrangeRelative(System.Int32,System.Int32,System.Int32,System.Int32)" />
        public CellRange GetSubrange(string firstCell, string lastCell)
        {
            int num;
            int num2;
            int num3;
            int num4;
            PositionToRowColumn(firstCell, out num, out num2);
            PositionToRowColumn(lastCell, out num3, out num4);
            return this.GetSubrangeAbsolute(num, num2, num3, num4);
        }

        /// <summary>
        /// Returns new cell range using absolute indexing.
        /// </summary>
        /// <param name="firstRow">Absolute index of the first row.</param>
        /// <param name="firstColumn">Absolute index of the first column.</param>
        /// <param name="lastRow">Absolute index of the last row.</param>
        /// <param name="lastColumn">Absolute index of the last column.</param>
        /// <remarks>
        /// New cell range must be within this cell range.
        /// </remarks>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Thrown if arguments are out of range.</exception>
        /// <seealso cref="P:GemBox.Spreadsheet.CellRange.FirstRowIndex" />
        /// <seealso cref="P:GemBox.Spreadsheet.CellRange.FirstColumnIndex" />
        /// <seealso cref="P:GemBox.Spreadsheet.CellRange.LastRowIndex" />
        /// <seealso cref="P:GemBox.Spreadsheet.CellRange.LastColumnIndex" />
        /// <seealso cref="M:GemBox.Spreadsheet.CellRange.GetSubrangeRelative(System.Int32,System.Int32,System.Int32,System.Int32)" />
        /// <seealso cref="M:GemBox.Spreadsheet.CellRange.GetSubrange(System.String,System.String)" />
        public CellRange GetSubrangeAbsolute(int firstRow, int firstColumn, int lastRow, int lastColumn)
        {
            if (((firstRow < this.firstRow) || (lastRow > this.lastRow)) || ((firstColumn < this.firstColumn) || (lastColumn > this.lastColumn)))
            {
                throw new ArgumentException("Specified subrange must be within this cell range.");
            }
            return new CellRange(base.Parent, firstRow, firstColumn, lastRow, lastColumn);
        }

        /// <summary>
        /// Returns new cell range using relative indexing.
        /// </summary>
        /// <param name="relativeRow">Relative index of the first row.</param>
        /// <param name="relativeColumn">Relative index of the first column.</param>
        /// <param name="width">Width of new cell range in columns.</param>
        /// <param name="height">Height of new cell range in rows.</param>
        /// <remarks>
        /// New cell range must be within this cell range.
        /// </remarks>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Thrown if arguments are out of range.</exception>
        /// <seealso cref="P:GemBox.Spreadsheet.CellRange.Width" />
        /// <seealso cref="P:GemBox.Spreadsheet.CellRange.Height" />
        /// <seealso cref="M:GemBox.Spreadsheet.CellRange.GetSubrangeAbsolute(System.Int32,System.Int32,System.Int32,System.Int32)" />
        /// <seealso cref="M:GemBox.Spreadsheet.CellRange.GetSubrange(System.String,System.String)" />
        public CellRange GetSubrangeRelative(int relativeRow, int relativeColumn, int width, int height)
        {
            int firstRow = this.firstRow + relativeRow;
            int firstColumn = this.firstColumn + relativeColumn;
            return this.GetSubrangeAbsolute(firstRow, firstColumn, (firstRow + height) - 1, (firstColumn + width) - 1);
        }

        internal bool IsRowColumnOutOfRange(int row, int column)
        {
            if (((row >= this.firstRow) && (row <= this.lastRow)) && ((column >= this.firstColumn) && (column <= this.lastColumn)))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if this cell range overlaps with another cell range.
        /// </summary>
        /// <param name="range">Cell range.</param>
        /// <returns><b>true</b> if cell ranges overlap; otherwise, <b>false</b>.</returns>
        public bool Overlaps(CellRange range)
        {
            return (((base.Parent == range.Parent) && this.SectionsOverlap(this.firstColumn, this.lastColumn, range.firstColumn, range.lastColumn)) && this.SectionsOverlap(this.firstRow, this.lastRow, range.firstRow, range.lastRow));
        }

        /// <summary>
        /// Converts position string ("A1", "BN27", etc.) to row and column index.
        /// </summary>
        /// <param name="position">Position string.</param>
        /// <param name="row">Row index.</param>
        /// <param name="column">Column index.</param>
        public static void PositionToRowColumn(string position, out int row, out int column)
        {
            bool flag;
            PositionToRowColumn(position, out row, out column, out flag);
            if (flag)
            {
                throw new ArgumentException("Position is not in format RowNameColumnName.");
            }
        }

        internal static void PositionToRowColumn(string position, out int row, out int column, out bool wrongFormat)
        {
            string name = position.Split(new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' }, 2)[0];
            if ((name.Length == 0) || (name.Length == position.Length))
            {
                row = column = -1;
                wrongFormat = true;
            }
            else
            {
                string str2 = position.Remove(0, name.Length);
                row = ExcelRowCollection.RowNameToIndex(str2);
                column = ExcelColumnCollection.ColumnNameToIndex(name);
                wrongFormat = false;
            }
        }

        /// <summary>
        /// Converts row and column index to position string ("A1", "BN27", etc.).
        /// </summary>
        /// <param name="row">Row index.</param>
        /// <param name="column">Column index.</param>
        /// <returns>Position string.</returns>
        public static string RowColumnToPosition(int row, int column)
        {
            return (ExcelColumnCollection.ColumnIndexToName(column) + ExcelRowCollection.RowIndexToName(row));
        }

        private bool SectionsOverlap(int sectionAStart, int sectionAEnd, int sectionBStart, int sectionBEnd)
        {
            return ((sectionAEnd >= sectionBStart) && (sectionBEnd >= sectionAStart));
        }

        /// <summary>
        /// Sets borders on one or more excel cells, taking cell position into account.
        /// </summary>
        /// <param name="multipleBorders">Borders to set.</param>
        /// <param name="lineColor">Line color.</param>
        /// <param name="lineStyle">Line style.</param>
        /// <example> Following code creates horizontal, vertical and rectangular cell ranges and demonstrates how 
        /// indexing works different in different context. <see cref="M:GemBox.Spreadsheet.CellRange.SetBorders(GemBox.Spreadsheet.MultipleBorders,System.Drawing.Color,GemBox.Spreadsheet.LineStyle)">SetBorders</see> 
        /// method is used to mark outside borders of the rectangular range.
        /// <code lang="Visual Basic">
        /// Dim cr As CellRange = excelFile.Worksheets(0).Rows(1).Cells
        /// 
        /// cr(0).Value = cr.IndexingMode
        /// cr(3).Value = "D2"
        /// cr("B").Value = "B2"
        /// 
        /// cr = excelFile.Worksheets(0).Columns(4).Cells
        /// 
        /// cr(0).Value = cr.IndexingMode
        /// cr(2).Value = "E3"
        /// cr("5").Value = "E5"
        /// 
        /// cr = excelFile.Worksheets(0).Cells.GetSubrange("F2", "J8")
        /// cr.SetBorders(MultipleBorders.Outside, Color.Navy, LineStyle.Dashed)
        /// 
        /// cr("I7").Value = cr.IndexingMode
        /// cr(0, 0).Value = "F2"
        /// cr("G3").Value = "G3"
        /// cr(5).Value = "F3" <font color="Green">' Cell range width is 5 (F G H I J).</font>
        /// </code>
        /// <code lang="C#">
        /// CellRange cr = excelFile.Worksheets[0].Rows[1].Cells;				
        /// 
        /// cr[0].Value = cr.IndexingMode;
        /// cr[3].Value = "D2";
        /// cr["B"].Value = "B2";
        /// 
        /// cr = excelFile.Worksheets[0].Columns[4].Cells;
        /// 
        /// cr[0].Value = cr.IndexingMode;
        /// cr[2].Value = "E3";
        /// cr["5"].Value = "E5";
        /// 
        /// cr = excelFile.Worksheets[0].Cells.GetSubrange("F2", "J8");
        /// cr.SetBorders(MultipleBorders.Outside, Color.Navy, LineStyle.Dashed);
        /// 
        /// cr["I7"].Value = cr.IndexingMode;
        /// cr[0,0].Value = "F2";
        /// cr["G3"].Value = "G3";
        /// cr[5].Value = "F3"; <font color="Green">// Cell range width is 5 (F G H I J).</font>
        /// </code>
        /// </example>
        public override void SetBorders(MultipleBorders multipleBorders, Color lineColor, LineStyle lineStyle)
        {
            MergedCellRange mergedRange = this.MergedRange;
            if (mergedRange != null)
            {
                mergedRange.SetBorders(multipleBorders, lineColor, lineStyle);
            }
            else
            {
                for (int i = this.firstRow; i <= this.lastRow; i++)
                {
                    for (int j = this.firstColumn; j <= this.lastColumn; j++)
                    {
                        MultipleBorders borders = multipleBorders;
                        if (i < this.lastRow)
                        {
                            borders &= ~MultipleBorders.Bottom;
                        }
                        if (i > this.firstRow)
                        {
                            borders &= ~MultipleBorders.Top;
                        }
                        if (j < this.lastColumn)
                        {
                            borders &= ~MultipleBorders.Right;
                        }
                        if (j > this.firstColumn)
                        {
                            borders &= ~MultipleBorders.Left;
                        }
                        base.Parent.Rows[i].AllocatedCells[j].SetBorders(borders, lineColor, lineStyle);
                    }
                }
            }
        }

        /// <summary>
        /// Returns a <see cref="T:System.String" /> that represents the current <see cref="T:GemBox.Spreadsheet.CellRange">CellRange</see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String" /> that represents the current <see cref="T:GemBox.Spreadsheet.CellRange">CellRange</see>.
        /// </returns>
        public override string ToString()
        {
            if ((this.Width <= 1) && (this.Height <= 1))
            {
                return (ExcelColumnCollection.ColumnIndexToName(this.firstColumn) + ExcelRowCollection.RowIndexToName(this.firstRow));
            }
            return (ExcelColumnCollection.ColumnIndexToName(this.firstColumn) + ExcelRowCollection.RowIndexToName(this.firstRow) + ":" + ExcelColumnCollection.ColumnIndexToName(this.lastColumn) + ExcelRowCollection.RowIndexToName(this.lastRow));
        }

        /// <summary>
        /// Gets or sets comment
        /// </summary>
        public override ExcelComment Comment
        {
            get
            {
                throw new InvalidOperationException("Comment can be assigned only to cell.");
            }
            set
            {
                throw new InvalidOperationException("Comment can be assigned only to cell.");
            }
        }

        /// <summary>
        /// Gets name of the last (bottom-right) cell in this cell range.
        /// </summary>
        public string EndPosition
        {
            get
            {
                return RowColumnToPosition(this.lastRow, this.lastColumn);
            }
        }

        /// <summary>
        /// Gets index of the first (leftmost) column.
        /// </summary>
        public int FirstColumnIndex
        {
            get
            {
                return this.firstColumn;
            }
        }

        /// <summary>
        /// Gets index of the first (topmost) row.
        /// </summary>
        public int FirstRowIndex
        {
            get
            {
                return this.firstRow;
            }
        }

        /// <summary>
        /// Gets or sets formula string.
        /// </summary>
        /// <remarks>
        /// <p>GemBox.Spreadsheet can read and write formulas, but cannot calculate formula results. When you 
        /// open a XLS file in MS Excel, formula results will be calculated automatically.</p>
        /// <p>Old XLS format requires all formulas to be parsed and saved to XLS files as special
        /// tokens in RPN (Reverse Polish notation). GemBox.Spreadsheet only knows how to parse limited
        /// set of formulas listed below.</p>
        /// <p>New XLSX (Open XML) format stores formulas as strings and leaves formula parsing to applications
        /// that read XLSX documents. Therefore, ALL formulas are supported when writing/reading XLSX files.</p>
        /// <p>Depending on <see cref="P:GemBox.Spreadsheet.ExcelFile.DelayFormulaParse">
        /// ExcelFile.DelayFormulaParse</see> property, formula string can be parsed when this property is set
        /// or when one of <see cref="M:GemBox.Spreadsheet.ExcelFile.SaveXls(System.String)">
        /// ExcelFile.SaveXls</see> methods is called.</p>
        /// <p>GemBox.Spreadsheet formula parser will use English culture to parse numbers.</p>
        /// <p>Currently supported formula features by GemBox.Spreadsheet formula parser are:
        /// <list type="bullet">
        /// <item><description>Named cell</description></item>
        /// <item><description>Named range</description></item>
        /// <item><description>Absolute cell/range</description></item>
        /// <item><description>Relative cell/range</description></item>
        /// <item><description>Functions( partly, see the list of supported functions below )</description></item>
        /// <item><description>Missed argument</description></item>
        /// <item><description>Unary operator</description></item>
        /// <item><description>Binary operator</description></item>
        /// <item><description>Parentheses</description></item>
        /// <item><description>3d cell reference</description></item>
        /// <item><description>3d cell range reference</description></item>
        /// <item><description>Boolean</description></item>
        /// <item><description>Integer</description></item>
        /// <item><description>Float</description></item>
        /// <item><description>String</description></item>
        /// <item><description>Error</description></item>
        /// </list>
        /// </p>
        /// <p>
        /// Currently unsupported formula features are:
        /// <list type="bullet">
        /// <item><description>Const array</description></item>
        /// <item><description>Array formula</description></item>
        /// <item><description>R1C1 reference</description></item>
        /// </list>
        /// </p>
        /// <p>
        /// Currently supported functions are:
        /// <list type="bullet">
        /// <item><description>NOW</description></item>
        /// <item><description>SECOND</description></item>
        /// <item><description>MINUTE</description></item>
        /// <item><description>HOUR</description></item>
        /// <item><description>WEEKDAY</description></item>
        /// <item><description>YEAR</description></item>
        /// <item><description>MONTH</description></item>
        /// <item><description>DAY</description></item>
        /// <item><description>TIME</description></item>
        /// <item><description>DATE</description></item>
        /// <item><description>RAND</description></item>
        /// <item><description>TEXT</description></item>
        /// <item><description>VAR</description></item>
        /// <item><description>MOD</description></item>
        /// <item><description>NOT</description></item>
        /// <item><description>OR</description></item>
        /// <item><description>AND</description></item>
        /// <item><description>FALSE</description></item>
        /// <item><description>TRUE</description></item>
        /// <item><description>VALUE</description></item>
        /// <item><description>LEN</description></item>
        /// <item><description>MID</description></item>
        /// <item><description>ROUND</description></item>
        /// <item><description>SIGN</description></item>
        /// <item><description>INT</description></item>
        /// <item><description>ABS</description></item>
        /// <item><description>LN</description></item>
        /// <item><description>EXP</description></item>
        /// <item><description>SQRT</description></item>
        /// <item><description>PI</description></item>
        /// <item><description>COS</description></item>
        /// <item><description>SIN</description></item>
        /// <item><description>COLUMN</description></item>
        /// <item><description>ROW</description></item>
        /// <item><description>MAX</description></item>
        /// <item><description>MIN</description></item>
        /// <item><description>AVERAGE</description></item>
        /// <item><description>SUM</description></item>
        /// <item><description>IF</description></item>
        /// <item><description>COUNT</description></item>
        /// <item><description>SUBTOTAL</description></item>
        /// </list>
        /// </p>
        /// <p>
        /// For more information on formulas, consult Microsoft Excel documentation.
        /// </p>
        /// </remarks>
        /// <exception cref="T:System.InvalidOperationException">Thrown if property get is attempted on a cell range 
        /// which is not merged.</exception>
        /// <example>Following code demonstrates how to use formulas and named ranges. It shows next features: 
        /// cell references (both absolute and relative), unary and binary operators, constand operands (integer and floating point),
        /// functions and named cell ranges.
        /// <code lang="Visual Basic">
        /// ws.Cells("A1").Value = 5
        /// ws.Cells("A2").Value = 6
        /// ws.Cells("A3").Value = 10
        /// 
        /// ws.Cells("C1").Formula = "=A1+A2"
        /// ws.Cells("C2").Formula = "=$A$1-A3"
        /// ws.Cells("C3").Formula = "=COUNT(A1:A3)"
        /// ws.Cells("C4").Formula = "=AVERAGE($A$1:$A$3)"
        /// ws.Cells("C5").Formula = "=SUM(A1:A3,2,3)"
        /// ws.Cells("C7").Formula = "= 123 - (-(-(23.5)))"
        /// 
        /// ws.NamedRanges.Add("DataRange", ws.Cells.GetSubrange("A1", "A3"))
        /// ws.Cells("C8").Formula = "=MAX(DataRange)"
        /// 
        /// Dim cr As CellRange = ws.Cells.GetSubrange("B9","C10")
        /// cr.Merged = True
        /// cr.Formula = "=A1*25"
        /// </code>
        /// <code lang="C#">	
        /// ws.Cells["A1"].Value = 5;
        /// ws.Cells["A2"].Value = 6;
        /// ws.Cells["A3"].Value = 10;
        /// 
        /// ws.Cells["C1"].Formula = "=A1+A2";
        /// ws.Cells["C2"].Formula = "=$A$1-A3";
        /// ws.Cells["C3"].Formula = "=COUNT(A1:A3)";
        /// ws.Cells["C4"].Formula = "=AVERAGE($A$1:$A$3)";
        /// ws.Cells["C5"].Formula = "=SUM(A1:A3,2,3)";
        /// ws.Cells["C7"].Formula = "= 123 - (-(-(23.5)))";
        /// 
        /// ws.NamedRanges.Add("DataRange", ws.Cells.GetSubrange("A1", "A3"));
        /// ws.Cells["C8"].Formula = "=MAX(DataRange)";
        /// 
        /// CellRange cr = ws.Cells.GetSubrange("B9", "C10");
        /// cr.Merged = true;
        /// cr.Formula = "=A1*25";
        /// </code>
        /// </example>
        /// <seealso cref="M:GemBox.Spreadsheet.NamedRangeCollection.Add(System.String,GemBox.Spreadsheet.CellRange,System.Boolean)">
        /// NamedRangeCollection.Add</seealso>
        public override string Formula
        {
            get
            {
                MergedCellRange mergedRange = this.MergedRange;
                if (mergedRange == null)
                {
                    throw new InvalidOperationException("Cell range has defined formula only if it is merged.");
                }
                return mergedRange.Formula;
            }
            set
            {
                MergedCellRange mergedRange = this.MergedRange;
                if (mergedRange != null)
                {
                    mergedRange.Formula = value;
                }
                else
                {
                    base.Parent.ParentExcelFile.ExceptionIfOverAffectedCellsLimit(this.Height * this.Width);
                    foreach (ExcelCell cell in this)
                    {
                        cell.Formula = value;
                    }
                }
            }
        }

        /// <summary>
        /// Gets height of this cell range, in rows.
        /// </summary>
        public int Height
        {
            get
            {
                return ((this.lastRow - this.firstRow) + 1);
            }
        }

        /// <summary>
        /// Gets indexing mode used for cell range.
        /// </summary>
        /// <remarks>
        /// <p>If <see cref="P:GemBox.Spreadsheet.CellRange.Height">Height</see> is 1, indexing mode 
        /// is <see cref="F:GemBox.Spreadsheet.RangeIndexingMode.Horizontal">Horizontal</see>.</p>
        /// <p>Otherwise, if <see cref="P:GemBox.Spreadsheet.CellRange.Width">Width</see> is 1, indexing mode 
        /// is <see cref="F:GemBox.Spreadsheet.RangeIndexingMode.Vertical">Vertical</see>.</p>
        /// <p>Otherwise, indexing mode is <see cref="F:GemBox.Spreadsheet.RangeIndexingMode.Rectangular">
        /// Rectangular</see>.</p>
        /// </remarks>
        /// <example> Following code creates horizontal, vertical and rectangular cell ranges and demonstrates how 
        /// indexing works different in different context. <see cref="M:GemBox.Spreadsheet.CellRange.SetBorders(GemBox.Spreadsheet.MultipleBorders,System.Drawing.Color,GemBox.Spreadsheet.LineStyle)">SetBorders</see> 
        /// method is used to mark outside borders of the rectangular range.
        /// <code lang="Visual Basic">
        /// Dim cr As CellRange = excelFile.Worksheets(0).Rows(1).Cells
        /// 
        /// cr(0).Value = cr.IndexingMode
        /// cr(3).Value = "D2"
        /// cr("B").Value = "B2"
        /// 
        /// cr = excelFile.Worksheets(0).Columns(4).Cells
        /// 
        /// cr(0).Value = cr.IndexingMode
        /// cr(2).Value = "E3"
        /// cr("5").Value = "E5"
        /// 
        /// cr = excelFile.Worksheets(0).Cells.GetSubrange("F2", "J8")
        /// cr.SetBorders(MultipleBorders.Outside, Color.Navy, LineStyle.Dashed)
        /// 
        /// cr("I7").Value = cr.IndexingMode
        /// cr(0, 0).Value = "F2"
        /// cr("G3").Value = "G3"
        /// cr(5).Value = "F3" <font color="Green">' Cell range width is 5 (F G H I J).</font>
        /// </code>
        /// <code lang="C#">
        /// CellRange cr = excelFile.Worksheets[0].Rows[1].Cells;				
        /// 
        /// cr[0].Value = cr.IndexingMode;
        /// cr[3].Value = "D2";
        /// cr["B"].Value = "B2";
        /// 
        /// cr = excelFile.Worksheets[0].Columns[4].Cells;
        /// 
        /// cr[0].Value = cr.IndexingMode;
        /// cr[2].Value = "E3";
        /// cr["5"].Value = "E5";
        /// 
        /// cr = excelFile.Worksheets[0].Cells.GetSubrange("F2", "J8");
        /// cr.SetBorders(MultipleBorders.Outside, Color.Navy, LineStyle.Dashed);
        /// 
        /// cr["I7"].Value = cr.IndexingMode;
        /// cr[0,0].Value = "F2";
        /// cr["G3"].Value = "G3";
        /// cr[5].Value = "F3"; <font color="Green">// Cell range width is 5 (F G H I J).</font>
        /// </code>
        /// </example>
        public RangeIndexingMode IndexingMode
        {
            get
            {
                if (this.Height == 1)
                {
                    return RangeIndexingMode.Horizontal;
                }
                if (this.Width == 1)
                {
                    return RangeIndexingMode.Vertical;
                }
                return RangeIndexingMode.Rectangular;
            }
        }

        /// <summary>
        /// Returns <b>true</b> is any cell in this cell range is merged; otherwise, <b>false</b>.
        /// </summary>
        public bool IsAnyCellMerged
        {
            get
            {
                CellRangeEnumerator readEnumerator = this.GetReadEnumerator();
                while (readEnumerator.MoveNext())
                {
                    if (readEnumerator.CurrentCell.MergedRange != null)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Returns <b>true</b> if all cells in cell range or merged range have default 
        /// cell style; otherwise, <b>false</b>.
        /// </summary>
        public override bool IsStyleDefault
        {
            get
            {
                MergedCellRange mergedRange = this.MergedRange;
                if (mergedRange == null)
                {
                    throw new InvalidOperationException("Cell range has defined style only if it is merged.");
                }
                return mergedRange.IsStyleDefault;
            }
        }

        /// <summary>
        /// Gets excel cell with the specified full or partial name.
        /// </summary>
        /// <param name="contextName">Full or partial name of the cell.</param>
        /// <remarks>
        /// <p>If <see cref="P:GemBox.Spreadsheet.CellRange.IndexingMode">IndexingMode</see> is 
        /// <see cref="F:GemBox.Spreadsheet.RangeIndexingMode.Rectangular">RangeIndexingMode.Rectangular</see> full name of 
        /// the cell must be used (for example; "A1", "D7", etc.).</p>
        /// <p>If <see cref="P:GemBox.Spreadsheet.CellRange.IndexingMode">IndexingMode</see> is 
        /// <see cref="F:GemBox.Spreadsheet.RangeIndexingMode.Horizontal">RangeIndexingMode.Horizontal</see> column name  
        /// must be used (for example; "A", "D", etc.).</p>
        /// <p>If <see cref="P:GemBox.Spreadsheet.CellRange.IndexingMode">IndexingMode</see> is 
        /// <see cref="F:GemBox.Spreadsheet.RangeIndexingMode.Vertical">RangeIndexingMode.Vertical</see> row name 
        /// must be used (for example; "1", "7", etc.).</p>
        /// </remarks>
        /// <example> Following code creates horizontal, vertical and rectangular cell ranges and demonstrates how 
        /// indexing works different in different context. <see cref="M:GemBox.Spreadsheet.CellRange.SetBorders(GemBox.Spreadsheet.MultipleBorders,System.Drawing.Color,GemBox.Spreadsheet.LineStyle)">SetBorders</see> 
        /// method is used to mark outside borders of the rectangular range.
        /// <code lang="Visual Basic">
        /// Dim cr As CellRange = excelFile.Worksheets(0).Rows(1).Cells
        /// 
        /// cr(0).Value = cr.IndexingMode
        /// cr(3).Value = "D2"
        /// cr("B").Value = "B2"
        /// 
        /// cr = excelFile.Worksheets(0).Columns(4).Cells
        /// 
        /// cr(0).Value = cr.IndexingMode
        /// cr(2).Value = "E3"
        /// cr("5").Value = "E5"
        /// 
        /// cr = excelFile.Worksheets(0).Cells.GetSubrange("F2", "J8")
        /// cr.SetBorders(MultipleBorders.Outside, Color.Navy, LineStyle.Dashed)
        /// 
        /// cr("I7").Value = cr.IndexingMode
        /// cr(0, 0).Value = "F2"
        /// cr("G3").Value = "G3"
        /// cr(5).Value = "F3" <font color="Green">' Cell range width is 5 (F G H I J).</font>
        /// </code>
        /// <code lang="C#">
        /// CellRange cr = excelFile.Worksheets[0].Rows[1].Cells;				
        /// 
        /// cr[0].Value = cr.IndexingMode;
        /// cr[3].Value = "D2";
        /// cr["B"].Value = "B2";
        /// 
        /// cr = excelFile.Worksheets[0].Columns[4].Cells;
        /// 
        /// cr[0].Value = cr.IndexingMode;
        /// cr[2].Value = "E3";
        /// cr["5"].Value = "E5";
        /// 
        /// cr = excelFile.Worksheets[0].Cells.GetSubrange("F2", "J8");
        /// cr.SetBorders(MultipleBorders.Outside, Color.Navy, LineStyle.Dashed);
        /// 
        /// cr["I7"].Value = cr.IndexingMode;
        /// cr[0,0].Value = "F2";
        /// cr["G3"].Value = "G3";
        /// cr[5].Value = "F3"; <font color="Green">// Cell range width is 5 (F G H I J).</font>
        /// </code>
        /// </example>
        /// <seealso cref="P:GemBox.Spreadsheet.CellRange.IndexingMode" />
        public ExcelCell this[string contextName]
        {
            get
            {
                int firstRow;
                int firstColumn;
                switch (this.IndexingMode)
                {
                    case RangeIndexingMode.Rectangular:
                        PositionToRowColumn(contextName, out firstRow, out firstColumn);
                        break;

                    case RangeIndexingMode.Horizontal:
                        firstRow = this.firstRow;
                        firstColumn = ExcelColumnCollection.ColumnNameToIndex(contextName);
                        break;

                    case RangeIndexingMode.Vertical:
                        firstRow = ExcelRowCollection.RowNameToIndex(contextName);
                        firstColumn = this.firstColumn;
                        break;

                    default:
                        throw new Exception("Internal: Unknown indexing mode.");
                }
                this.ExceptionIfRowColumnOutOfRange(firstRow, firstColumn);
                return base.Parent.Rows[firstRow].AllocatedCells[firstColumn];
            }
        }

        /// <overloads>Gets excel cell with the specified name or at the specified position.</overloads>
        /// <summary>
        /// Gets excel cell at the specified index.
        /// </summary>
        /// <param name="contextIndex">The zero-based context index of the cell.</param>
        /// <remarks>
        /// <p>If <see cref="P:GemBox.Spreadsheet.CellRange.IndexingMode">IndexingMode</see> is 
        /// <see cref="F:GemBox.Spreadsheet.RangeIndexingMode.Horizontal">RangeIndexingMode.Horizontal</see> context index  
        /// is specifying relative column position.</p>
        /// <p>If <see cref="P:GemBox.Spreadsheet.CellRange.IndexingMode">IndexingMode</see> is 
        /// <see cref="F:GemBox.Spreadsheet.RangeIndexingMode.Vertical">RangeIndexingMode.Vertical</see> context index
        /// is specifying relative row position.</p>
        /// <p>If <see cref="P:GemBox.Spreadsheet.CellRange.IndexingMode">IndexingMode</see> is 
        /// <see cref="F:GemBox.Spreadsheet.RangeIndexingMode.Rectangular">RangeIndexingMode.Rectangular</see> context index
        /// is specifying cell index inside cell range. The cell at <see cref="P:GemBox.Spreadsheet.CellRange.StartPosition">
        /// StartPosition</see> has index 0, and the cell at 
        /// <see cref="P:GemBox.Spreadsheet.CellRange.EndPosition">EndPosition</see> has index of 
        /// <see cref="P:GemBox.Spreadsheet.CellRange.Width">Width</see> x 
        /// <see cref="P:GemBox.Spreadsheet.CellRange.Height">Height</see> - 1.</p>
        /// </remarks>
        /// <example> Following code creates horizontal, vertical and rectangular cell ranges and demonstrates how 
        /// indexing works different in different context. <see cref="M:GemBox.Spreadsheet.CellRange.SetBorders(GemBox.Spreadsheet.MultipleBorders,System.Drawing.Color,GemBox.Spreadsheet.LineStyle)">SetBorders</see> 
        /// method is used to mark outside borders of the rectangular range.
        /// <code lang="Visual Basic">
        /// Dim cr As CellRange = excelFile.Worksheets(0).Rows(1).Cells
        /// 
        /// cr(0).Value = cr.IndexingMode
        /// cr(3).Value = "D2"
        /// cr("B").Value = "B2"
        /// 
        /// cr = excelFile.Worksheets(0).Columns(4).Cells
        /// 
        /// cr(0).Value = cr.IndexingMode
        /// cr(2).Value = "E3"
        /// cr("5").Value = "E5"
        /// 
        /// cr = excelFile.Worksheets(0).Cells.GetSubrange("F2", "J8")
        /// cr.SetBorders(MultipleBorders.Outside, Color.Navy, LineStyle.Dashed)
        /// 
        /// cr("I7").Value = cr.IndexingMode
        /// cr(0, 0).Value = "F2"
        /// cr("G3").Value = "G3"
        /// cr(5).Value = "F3" <font color="Green">' Cell range width is 5 (F G H I J).</font>
        /// </code>
        /// <code lang="C#">
        /// CellRange cr = excelFile.Worksheets[0].Rows[1].Cells;				
        /// 
        /// cr[0].Value = cr.IndexingMode;
        /// cr[3].Value = "D2";
        /// cr["B"].Value = "B2";
        /// 
        /// cr = excelFile.Worksheets[0].Columns[4].Cells;
        /// 
        /// cr[0].Value = cr.IndexingMode;
        /// cr[2].Value = "E3";
        /// cr["5"].Value = "E5";
        /// 
        /// cr = excelFile.Worksheets[0].Cells.GetSubrange("F2", "J8");
        /// cr.SetBorders(MultipleBorders.Outside, Color.Navy, LineStyle.Dashed);
        /// 
        /// cr["I7"].Value = cr.IndexingMode;
        /// cr[0,0].Value = "F2";
        /// cr["G3"].Value = "G3";
        /// cr[5].Value = "F3"; <font color="Green">// Cell range width is 5 (F G H I J).</font>
        /// </code>
        /// </example>
        /// <seealso cref="P:GemBox.Spreadsheet.CellRange.IndexingMode" />
        public ExcelCell this[int contextIndex]
        {
            get
            {
                switch (this.IndexingMode)
                {
                    case RangeIndexingMode.Rectangular:
                    {
                        int width = this.Width;
                        int num2 = contextIndex / width;
                        int num3 = contextIndex % width;
                        return this[num2, num3];
                    }
                    case RangeIndexingMode.Horizontal:
                        return this[0, contextIndex];

                    case RangeIndexingMode.Vertical:
                        return this[contextIndex, 0];
                }
                throw new Exception("Internal: Unknown indexing mode.");
            }
        }

        /// <summary>
        /// Gets excel cell at the specified relative position.
        /// </summary>
        /// <param name="relativeRow">The zero-based relative row position.</param>
        /// <param name="relativeColumn">The zero-based relative column position.</param>
        /// <remarks>
        /// Absolute position of excel cell is calculated by adding <i>relativeRow</i> and <i>relativeColumn</i> to
        /// <see cref="P:GemBox.Spreadsheet.CellRange.FirstRowIndex">FirstRowIndex</see> and 
        /// <see cref="P:GemBox.Spreadsheet.CellRange.FirstColumnIndex">FirstColumnIndex</see>.
        /// </remarks>
        /// <example> Following code creates horizontal, vertical and rectangular cell ranges and demonstrates how 
        /// indexing works different in different context. <see cref="M:GemBox.Spreadsheet.CellRange.SetBorders(GemBox.Spreadsheet.MultipleBorders,System.Drawing.Color,GemBox.Spreadsheet.LineStyle)">SetBorders</see> 
        /// method is used to mark outside borders of the rectangular range.
        /// <code lang="Visual Basic">
        /// Dim cr As CellRange = excelFile.Worksheets(0).Rows(1).Cells
        /// 
        /// cr(0).Value = cr.IndexingMode
        /// cr(3).Value = "D2"
        /// cr("B").Value = "B2"
        /// 
        /// cr = excelFile.Worksheets(0).Columns(4).Cells
        /// 
        /// cr(0).Value = cr.IndexingMode
        /// cr(2).Value = "E3"
        /// cr("5").Value = "E5"
        /// 
        /// cr = excelFile.Worksheets(0).Cells.GetSubrange("F2", "J8")
        /// cr.SetBorders(MultipleBorders.Outside, Color.Navy, LineStyle.Dashed)
        /// 
        /// cr("I7").Value = cr.IndexingMode
        /// cr(0, 0).Value = "F2"
        /// cr("G3").Value = "G3"
        /// cr(5).Value = "F3" <font color="Green">' Cell range width is 5 (F G H I J).</font>
        /// </code>
        /// <code lang="C#">
        /// CellRange cr = excelFile.Worksheets[0].Rows[1].Cells;				
        /// 
        /// cr[0].Value = cr.IndexingMode;
        /// cr[3].Value = "D2";
        /// cr["B"].Value = "B2";
        /// 
        /// cr = excelFile.Worksheets[0].Columns[4].Cells;
        /// 
        /// cr[0].Value = cr.IndexingMode;
        /// cr[2].Value = "E3";
        /// cr["5"].Value = "E5";
        /// 
        /// cr = excelFile.Worksheets[0].Cells.GetSubrange("F2", "J8");
        /// cr.SetBorders(MultipleBorders.Outside, Color.Navy, LineStyle.Dashed);
        /// 
        /// cr["I7"].Value = cr.IndexingMode;
        /// cr[0,0].Value = "F2";
        /// cr["G3"].Value = "G3";
        /// cr[5].Value = "F3"; <font color="Green">// Cell range width is 5 (F G H I J).</font>
        /// </code>
        /// </example>
        public ExcelCell this[int relativeRow, int relativeColumn]
        {
            get
            {
                int row = this.firstRow + relativeRow;
                int column = this.firstColumn + relativeColumn;
                this.ExceptionIfRowColumnOutOfRange(row, column);
                return base.Parent.Rows[row].AllocatedCells[column];
            }
        }

        /// <summary>
        /// Gets index of the last (rightmost) column.
        /// </summary>
        public int LastColumnIndex
        {
            get
            {
                return this.lastColumn;
            }
        }

        /// <summary>
        /// Gets index of the last (bottommost) row.
        /// </summary>
        public int LastRowIndex
        {
            get
            {
                return this.lastRow;
            }
        }

        /// <summary>
        /// Gets or sets whether cells in this range are merged.
        /// </summary>
        /// <remarks>
        /// <p>By setting this property to <b>true</b>, you are merging all the cells 
        /// (<see cref="T:GemBox.Spreadsheet.ExcelCell">ExcelCell</see>) in this range. Merging process will fail if any 
        /// of the cells in the range is already merged.</p>
        /// <p>When modifying merged cell, whole merged range is modified. For example, if you set 
        /// <see cref="P:GemBox.Spreadsheet.ExcelCell.Value">ExcelCell.Value</see>, value of merged range will be modified. 
        /// You can find out if the cell is merged by checking if 
        /// <see cref="P:GemBox.Spreadsheet.ExcelCell.MergedRange">ExcelCell.MergedRange</see> property is different 
        /// than <b>null</b>.</p>
        /// </remarks>
        /// <exception cref="T:System.ArgumentException">Thrown when merged range can't be created because some of the cells
        /// in the range are already merged.</exception>
        public virtual bool Merged
        {
            get
            {
                return (this.MergedRange != null);
            }
            set
            {
                if (!this.Merged)
                {
                    if (value)
                    {
                        base.Parent.ParentExcelFile.ExceptionIfOverAffectedCellsLimit(this.Height * this.Width);
                        base.Parent.MergedRanges.Add(new MergedCellRange(this));
                    }
                }
                else if (!value)
                {
                    base.Parent.ParentExcelFile.ExceptionIfOverAffectedCellsLimit(this.Height * this.Width);
                    base.Parent.MergedRanges.Remove(this.MergedRange);
                }
            }
        }

        private MergedCellRange MergedRange
        {
            get
            {
                MergedCellRange mergedRange = (MergedCellRange) this[0, 0].MergedRange;
                if ((mergedRange == null) || ((mergedRange.Width == this.Width) && (mergedRange.Height == this.Height)))
                {
                    return mergedRange;
                }
                return null;
            }
        }

        /// <summary>
        /// Gets name of the first (top-left) cell in this cell range.
        /// </summary>
        public string StartPosition
        {
            get
            {
                return RowColumnToPosition(this.firstRow, this.firstColumn);
            }
        }

        /// <summary>
        /// Gets or sets cell style (<see cref="T:GemBox.Spreadsheet.CellStyle">CellStyle</see>) on one or more excel cells.
        /// </summary>
        /// <remarks>
        /// <p>Property set will set style of multiple cells or of a merged range.</p>
        /// <p>Property get has meaning only if range is <see cref="P:GemBox.Spreadsheet.CellRange.Merged">Merged</see>; 
        /// otherwise, exception is thrown.</p>
        /// <p> Note that for <see cref="P:GemBox.Spreadsheet.CellRange.Style">Style</see> property set on a cell range that 
        /// is not merged, you can't use the following format:
        /// <code lang="Visual Basic">
        /// Dim cr As CellRange = excelFile.Worksheets(0).Rows(1).Cells
        /// cr.Style.Rotation = 30
        /// </code>
        /// <code lang="C#">
        /// CellRange cr = excelFile.Worksheets[0].Rows[1].Cells;
        /// cr.Style.Rotation = 30;
        /// </code>
        /// because that would first call <see cref="P:GemBox.Spreadsheet.CellRange.Style">Style</see> property get method and that 
        /// will certainly fail because <see cref="P:GemBox.Spreadsheet.CellRange.Style">Style</see> property get is defined only 
        /// for a merged cell range. </p><p>Instead you can use two different code patterns, depending on whether you want to replace or combine the existing 
        /// cell range styles with the new style.</p><p>
        /// If you want to <b>replace</b> cell style on every cell in a cell range use the following code:
        /// <code lang="Visual Basic">
        /// Dim cr As CellRange = excelFile.Worksheets(0).Rows(1).Cells
        /// Dim style As CellStyle = New CellStyle()
        /// style.Rotation = 30
        /// cr.Style = style
        /// </code>
        /// <code lang="C#">
        /// CellRange cr = excelFile.Worksheets[0].Rows[1].Cells;
        /// CellStyle style = new CellStyle();
        /// style.Rotation = 30;
        /// cr.Style = style;
        /// </code>
        /// </p><p>
        /// If you want to <b>set</b> cell style property on every cell in a cell range (other cell style property values will 
        /// remain unchanged) use the following code:
        /// <code lang="Visual Basic">
        /// Dim cell As ExcelCell
        /// For Each cell In excelFile.Worksheets(0).Rows(1).Cells
        /// cell.Style.Rotation = 30
        /// Next
        /// </code>
        /// <code lang="C#">
        /// foreach(ExcelCell cell in excelFile.Worksheets[0].Rows[1].Cells)
        /// cell.Style.Rotation = 30;
        /// </code>
        /// </p>
        /// </remarks>
        /// <exception cref="T:System.InvalidOperationException">Thrown if property get is attempted on a cell range 
        /// which is not merged.</exception>
        /// <seealso cref="P:GemBox.Spreadsheet.CellRange.Merged" />
        public override CellStyle Style
        {
            get
            {
                MergedCellRange mergedRange = this.MergedRange;
                if (mergedRange == null)
                {
                    throw new InvalidOperationException("Cell range has defined style only if it is merged.");
                }
                return mergedRange.Style;
            }
            set
            {
                MergedCellRange mergedRange = this.MergedRange;
                if (mergedRange != null)
                {
                    mergedRange.Style = value;
                }
                else
                {
                    base.Parent.ParentExcelFile.ExceptionIfOverAffectedCellsLimit(this.Height * this.Width);
                    foreach (ExcelCell cell in this)
                    {
                        cell.Style = value;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets cell value on one or more excel cells.
        /// </summary>
        /// <remarks>
        /// <p>Property set will set value of multiple cells or of a merged range.</p>
        /// <p>Property get has meaning only if range is <see cref="P:GemBox.Spreadsheet.CellRange.Merged">Merged</see>; 
        /// otherwise, exception is thrown.</p>
        /// </remarks>
        /// <exception cref="T:System.InvalidOperationException">Thrown if property get is attempted on a cell range 
        /// which is not merged.</exception>
        /// <seealso cref="P:GemBox.Spreadsheet.CellRange.Merged" />
        public override object Value
        {
            get
            {
                MergedCellRange mergedRange = this.MergedRange;
                if (mergedRange == null)
                {
                    throw new InvalidOperationException("Cell range has defined value only if it is merged.");
                }
                return mergedRange.Value;
            }
            set
            {
                MergedCellRange mergedRange = this.MergedRange;
                if (mergedRange != null)
                {
                    mergedRange.Value = value;
                }
                else
                {
                    base.Parent.ParentExcelFile.ExceptionIfOverAffectedCellsLimit(this.Height * this.Width);
                    foreach (ExcelCell cell in this)
                    {
                        cell.Value = value;
                    }
                }
            }
        }

        /// <summary>
        /// Gets width of this cell range, in columns.
        /// </summary>
        public int Width
        {
            get
            {
                return ((this.lastColumn - this.firstColumn) + 1);
            }
        }
    }
}

