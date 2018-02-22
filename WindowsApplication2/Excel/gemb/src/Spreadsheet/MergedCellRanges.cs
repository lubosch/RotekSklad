namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;
    using System.Drawing;

    internal class MergedCellRanges
    {
        private Hashtable items;
        private ExcelWorksheet parent;

        internal MergedCellRanges(ExcelWorksheet parent)
        {
            this.items = new Hashtable();
            this.parent = parent;
        }

        internal MergedCellRanges(ExcelWorksheet parent, MergedCellRanges sourceRanges)
        {
            this.items = new Hashtable();
            this.parent = parent;
            foreach (MergedCellRange range in sourceRanges.Values)
            {
                this.Add(new MergedCellRange(this.parent, range));
            }
        }

        internal void Add(MergedCellRange mergedRange)
        {
            if (mergedRange.IsAnyCellMerged)
            {
                throw new ArgumentException("New merged range can't overlap with existing merged range.");
            }
            this.AddInternal(mergedRange);
            CellStyle bordersStyle = new CellStyle(this.parent.ParentExcelFile);
            ResolveBorder(bordersStyle, mergedRange, IndividualBorder.Top, 0, 0, 0, 1);
            ResolveBorder(bordersStyle, mergedRange, IndividualBorder.Left, 0, 0, 1, 0);
            ResolveBorder(bordersStyle, mergedRange, IndividualBorder.Bottom, mergedRange.Height - 1, 0, 0, 1);
            ResolveBorder(bordersStyle, mergedRange, IndividualBorder.Right, 0, mergedRange.Width - 1, 1, 0);
            bool flag = false;
            CellStyle style = null;
            foreach (ExcelCell cell in mergedRange)
            {
                if (!flag)
                {
                    if (!cell.IsStyleDefault)
                    {
                        style = cell.Style;
                    }
                    flag = true;
                }
                cell.AddToMergedRange(mergedRange);
            }
            if ((mergedRange.Value == null) && (style != null))
            {
                mergedRange.Style = style;
            }
            CellStyleData element = bordersStyle.Element;
            CellStyleData defaultElement = (CellStyleData) this.parent.ParentExcelFile.CellStyleCache.DefaultElement;
            if (!CellStyleData.AreBordersEqual(element.BorderColor, element.BorderStyle, element.BordersUsed, defaultElement.BorderColor, defaultElement.BorderStyle, defaultElement.BordersUsed))
            {
                mergedRange.Style.Borders = bordersStyle.Borders;
            }
        }

        internal void AddInternal(MergedCellRange mergedRange)
        {
            this.items.Add(mergedRange, mergedRange);
        }

        internal void Remove(MergedCellRange mergedRange)
        {
            this.items.Remove(mergedRange);
            foreach (ExcelCell cell in mergedRange)
            {
                cell.RemoveFromMergedRange();
            }
        }

        private static void ResolveBorder(CellStyle bordersStyle, MergedCellRange mergedRange, IndividualBorder borderId, int row, int column, int rowInc, int colInc)
        {
            CellBorder border = bordersStyle.Borders[borderId];
            bool flag = true;
            while ((row < mergedRange.Height) && (column < mergedRange.Width))
            {
                CellBorder border2 = mergedRange[row, column].Style.Borders[borderId];
                if (flag)
                {
                    border.LineStyle = border2.LineStyle;
                    border.LineColor = border2.LineColor;
                    flag = false;
                }
                else if ((border2.LineStyle != border.LineStyle) || (border2.LineColor != border.LineColor))
                {
                    border.LineStyle = LineStyle.None;
                    border.LineColor = Color.Empty;
                    return;
                }
                row += rowInc;
                column += colInc;
            }
        }

        public ICollection Values
        {
            get
            {
                return this.items.Values;
            }
        }
    }
}

