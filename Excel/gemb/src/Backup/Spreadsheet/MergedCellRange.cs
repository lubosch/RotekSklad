namespace GemBox.Spreadsheet
{
    using System;
    using System.Drawing;

    internal class MergedCellRange : CellRange
    {
        private object cellValue;
        private ExcelComment comment;
        private CellStyle style;

        internal MergedCellRange(CellRange cellRange) : base(cellRange.Parent, cellRange.FirstRowIndex, cellRange.FirstColumnIndex, cellRange.LastRowIndex, cellRange.LastColumnIndex)
        {
        }

        internal MergedCellRange(ExcelWorksheet parent, MergedCellRange sourceMRange) : base(parent, sourceMRange.FirstRowIndex, sourceMRange.FirstColumnIndex, sourceMRange.LastRowIndex, sourceMRange.LastColumnIndex)
        {
            this.cellValue = sourceMRange.ValueInternal;
            this.Style = sourceMRange.Style;
        }

        internal void FixColumnIndexes(int columnIndex, int offset)
        {
            if (base.FirstColumnIndex >= columnIndex)
            {
                base.FixFirstColumnIndex(columnIndex, offset);
                base.FixLastColumnIndex(columnIndex, offset);
            }
            else if (base.LastColumnIndex >= columnIndex)
            {
                base.FixLastColumnIndex(columnIndex, offset);
                if (offset > 0)
                {
                    for (int i = 0; i < offset; i++)
                    {
                        for (int j = base.FirstRowIndex; j <= base.LastRowIndex; j++)
                        {
                            base.Parent.Rows[j].AllocatedCells[columnIndex + i].AddToMergedRangeInternal(this);
                        }
                    }
                }
            }
        }

        internal void FixRowIndexes(int rowIndex, int offset)
        {
            if (base.FirstRowIndex >= rowIndex)
            {
                base.FixFirstRowIndex(rowIndex, offset);
                base.FixLastRowIndex(rowIndex, offset);
            }
            else if (base.LastRowIndex >= rowIndex)
            {
                base.FixLastRowIndex(rowIndex, offset);
                if (offset > 0)
                {
                    for (int i = 0; i < offset; i++)
                    {
                        for (int j = base.FirstColumnIndex; j <= base.LastColumnIndex; j++)
                        {
                            base.Parent.Rows[rowIndex + i].AllocatedCells[j].AddToMergedRangeInternal(this);
                        }
                    }
                }
            }
        }

        public override void SetBorders(MultipleBorders multipleBorders, Color lineColor, LineStyle lineStyle)
        {
            this.Style.Borders.SetBorders(multipleBorders, lineColor, lineStyle);
        }

        internal CellStyle StyleResolved(int row, int column)
        {
            if (this.IsStyleDefault)
            {
                return null;
            }
            IndividualBorder[] borderArray = new IndividualBorder[4];
            int num = 0;
            CellBorder border = this.style.Borders[IndividualBorder.Bottom];
            if ((border.LineStyle != LineStyle.None) && (row < base.LastRowIndex))
            {
                borderArray[num++] = IndividualBorder.Bottom;
            }
            border = this.style.Borders[IndividualBorder.Top];
            if ((border.LineStyle != LineStyle.None) && (row > base.FirstRowIndex))
            {
                borderArray[num++] = IndividualBorder.Top;
            }
            border = this.style.Borders[IndividualBorder.Right];
            if ((border.LineStyle != LineStyle.None) && (column < base.LastColumnIndex))
            {
                borderArray[num++] = IndividualBorder.Right;
            }
            border = this.style.Borders[IndividualBorder.Left];
            if ((border.LineStyle != LineStyle.None) && (column > base.FirstColumnIndex))
            {
                borderArray[num++] = IndividualBorder.Left;
            }
            if (num == 0)
            {
                return this.style;
            }
            CellStyle style = new CellStyle(this.style, this.style.Element.ParentCollection);
            for (int i = 0; i < num; i++)
            {
                style.Borders[borderArray[i]].SetBorder(Color.Empty, LineStyle.None);
            }
            return style;
        }

        /// <summary>
        /// Gets or sets cell comment.
        /// </summary>
        /// <remarks>
        /// <p>
        /// You can set comment text, set whether comment will be visible during loading xls file or not.
        /// Additinally you can get column or row of the excel cell to which this comment is assigned.
        /// </p>
        /// </remarks>
        /// <example>
        /// Following code demonstrates how to use comments. It shows next features: 
        /// <list type="number">
        /// <item> comment text setting </item>
        /// <item> comment' IsVisible property in action </item>
        /// </list>
        /// <code lang="Visual Basic">		
        /// excelFile.Worksheets(0).Cells(0, 0).Comment.Text = "comment1" 
        /// excelFile.Worksheets(0).Cells(0, 0).Comment.IsVisible = False
        /// </code>
        /// <code lang="C#">
        /// excelFile.Worksheets[ 0 ].Cells[ 0, 0 ].Comment.Text = "comment1";
        /// excelFile.Worksheets[ 0 ].Cells[ 0, 0 ].Comment.IsVisible = false;
        /// </code>
        /// </example>
        public override ExcelComment Comment
        {
            get
            {
                if (this.comment == null)
                {
                    this.comment = new ExcelComment();
                    CommentShape shape = base.Parent.Shapes.Add(this.comment);
                    this.comment.Shape = shape;
                }
                return this.comment;
            }
            set
            {
                if ((value == null) && (this.comment != null))
                {
                    base.Parent.Shapes.DeleteInternal(this.comment.Shape);
                }
                this.comment = value;
            }
        }

        /// <summary>
        /// Gets or sets merged range formula string.
        /// </summary>
        public override string Formula
        {
            get
            {
                CellFormula valueInternal = this.ValueInternal as CellFormula;
                if (valueInternal != null)
                {
                    return valueInternal.Formula;
                }
                return null;
            }
            set
            {
                if ((value == null) || (value == ""))
                {
                    this.ValueInternal = null;
                }
                else
                {
                    this.ValueInternal = new CellFormula(value, base.Parent);
                }
            }
        }

        internal CellFormula FormulaInternal
        {
            get
            {
                object valueInternal = this.ValueInternal;
                if (valueInternal is CellFormula)
                {
                    return (CellFormula) valueInternal;
                }
                return null;
            }
            set
            {
                if (value != null)
                {
                    this.ValueInternal = value;
                }
                else
                {
                    this.ValueInternal = this.Value;
                }
            }
        }

        public override bool IsStyleDefault
        {
            get
            {
                if (this.style != null)
                {
                    return this.style.IsDefault;
                }
                return true;
            }
        }

        public override bool Merged
        {
            get
            {
                return true;
            }
            set
            {
                throw new InvalidOperationException("MergedCellRange is always merged.");
            }
        }

        public override CellStyle Style
        {
            get
            {
                if (this.style == null)
                {
                    this.style = new CellStyle(base.Parent.ParentExcelFile.CellStyleCache);
                }
                return this.style;
            }
            set
            {
                this.style = new CellStyle(value, base.Parent.ParentExcelFile.CellStyleCache);
            }
        }

        public override object Value
        {
            get
            {
                object valueInternal = this.ValueInternal;
                if (valueInternal is CellFormula)
                {
                    valueInternal = ((CellFormula) valueInternal).Value;
                }
                return valueInternal;
            }
            set
            {
                if (value != null)
                {
                    ExcelFile.ThrowExceptionForUnsupportedType(value.GetType());
                }
                object valueInternal = this.ValueInternal;
                if (valueInternal is CellFormula)
                {
                    ((CellFormula) valueInternal).Value = value;
                }
                else
                {
                    this.ValueInternal = value;
                    base.CheckMultiline(value);
                }
            }
        }

        internal object ValueInternal
        {
            get
            {
                return this.cellValue;
            }
            set
            {
                this.cellValue = value;
            }
        }
    }
}

