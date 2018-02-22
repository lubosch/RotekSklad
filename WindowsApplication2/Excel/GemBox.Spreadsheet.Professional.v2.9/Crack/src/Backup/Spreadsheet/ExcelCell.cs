namespace GemBox.Spreadsheet
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Excel cell provides access to a single worksheet cell or to a merged range if the cell is merged.
    /// </summary>
    /// <remarks>
    /// <p>Merged range is created by using <see cref="P:GemBox.Spreadsheet.CellRange.Merged">CellRange.Merged</see> property. 
    /// See the property documentation for more information on merging.</p>
    /// </remarks>
    /// <seealso cref="P:GemBox.Spreadsheet.CellRange.Merged">CellRange.Merged</seealso>
    /// <seealso cref="P:GemBox.Spreadsheet.ExcelCell.MergedRange" />
    public sealed class ExcelCell : AbstractRange
    {
        private object cellValue;
        private ExcelComment comment;
        private CellStyle style;

        internal ExcelCell(ExcelWorksheet parent) : base(parent)
        {
        }

        internal ExcelCell(ExcelWorksheet parent, ExcelCell sourceCell) : base(parent)
        {
            this.cellValue = sourceCell.ValueInternal;
            this.Style = sourceCell.Style;
        }

        internal void AddToMergedRange(MergedCellRange mergedRange)
        {
            if ((mergedRange.Value == null) && (this.cellValue != null))
            {
                mergedRange.ValueInternal = this.cellValue;
                if ((this.style != null) && !this.style.IsDefault)
                {
                    mergedRange.Style = this.style;
                }
            }
            this.AddToMergedRangeInternal(mergedRange);
        }

        internal void AddToMergedRangeInternal(MergedCellRange mergedRange)
        {
            this.cellValue = mergedRange;
        }

        /// <summary>
        /// Converts Excel floating-point number to <see cref="T:System.DateTime">DateTime</see> structure.
        /// </summary>
        /// <remarks>
        /// <p>
        /// Excel file format doesn't have a separate data type for date and time. 
        /// <see cref="T:System.DateTime">DateTime</see> value is
        /// stored as IEEE number encoded in a special way. When reading Excel file, 
        /// <see cref="P:GemBox.Spreadsheet.CellStyle.NumberFormat">CellStyle.NumberFormat</see> is examined and if it matches 
        /// some of date/time number formats cell value is interpreted as <see cref="T:System.DateTime">DateTime</see>.</p>
        /// <p>However, if some non-standard date/time number format is used, cell value will not be recognized 
        /// as <see cref="T:System.DateTime">DateTime</see> but as ordinary number. In such cases (when you know that
        /// specific cell holds <see cref="T:System.DateTime">DateTime</see> value but you get a number when reading
        /// Excel file) use this method to convert IEEE number to <see cref="T:System.DateTime">DateTime</see> 
        /// structure.</p>
        /// </remarks>
        /// <param name="num">Excel floating-point number.</param>
        /// <param name="use1904DateSystem">True to use 1904 date system.</param>
        /// <returns>Converted DateTime structure.</returns>
        public static DateTime ConvertExcelNumberToDateTime(double num, bool use1904DateSystem)
        {
            DateTime startDate = ExcelFile.GetStartDate(use1904DateSystem);
            if (double.IsNaN(num) || double.IsInfinity(num))
            {
                return startDate;
            }
            long num2 = (long) num;
            long num3 = (long) Math.Round((double) ((num - num2) * 86400.0));
            return startDate.AddDays((double) (num2 - 2L)).AddSeconds((double) num3);
        }

        internal void RemoveFromMergedRange()
        {
            MergedCellRange cellValue = this.cellValue as MergedCellRange;
            if (cellValue == null)
            {
                throw new Exception("Internal error: cell is not merged.");
            }
            this.cellValue = cellValue.ValueInternal;
            this.style = cellValue.Style;
        }

        /// <summary>
        /// Sets borders on this cell or on merged range if this cell is merged.
        /// </summary>
        /// <param name="multipleBorders">Borders to set.</param>
        /// <param name="lineColor">Line color.</param>
        /// <param name="lineStyle">Line style.</param>
        /// <seealso cref="P:GemBox.Spreadsheet.CellRange.Merged">CellRange.Merged</seealso>
        /// <seealso cref="P:GemBox.Spreadsheet.ExcelCell.MergedRange" />
        public override void SetBorders(MultipleBorders multipleBorders, Color lineColor, LineStyle lineStyle)
        {
            this.Style.Borders.SetBorders(multipleBorders, lineColor, lineStyle);
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
        /// Gets or sets cell formula string.
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
        /// <seealso cref="M:GemBox.Spreadsheet.NamedRangeCollection.Add(System.String,GemBox.Spreadsheet.CellRange)">
        /// NamedRangeCollection.Add</seealso>
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

        internal bool HasComment
        {
            get
            {
                return (this.comment != null);
            }
        }

        /// <summary>
        /// Returns <b>true</b> if style is default; otherwise, <b>false</b>.
        /// </summary>
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

        /// <summary>
        /// Returns associated merged range if the cell is merged; otherwise, <b>null</b>.
        /// </summary>
        /// <seealso cref="P:GemBox.Spreadsheet.CellRange.Merged">CellRange.Merged</seealso>
        public CellRange MergedRange
        {
            get
            {
                if (this.cellValue is MergedCellRange)
                {
                    return (MergedCellRange) this.cellValue;
                }
                return null;
            }
        }

        /// <summary>
        /// Gets or sets cell style (<see cref="T:GemBox.Spreadsheet.CellStyle">CellStyle</see>) of this cell or 
        /// of merged range if this cell is merged.
        /// </summary>
        /// <remarks>
        /// Unset style properties will be inherited from corresponding row or column. See
        /// <see cref="P:GemBox.Spreadsheet.ExcelFile.RowColumnResolutionMethod">ExcelFile.RowColumnResolutionMethod</see>
        /// for more details.
        /// </remarks>
        /// <seealso cref="P:GemBox.Spreadsheet.CellRange.Merged">CellRange.Merged</seealso>
        /// <seealso cref="P:GemBox.Spreadsheet.ExcelCell.MergedRange" />
        /// <seealso cref="P:GemBox.Spreadsheet.ExcelFile.RowColumnResolutionMethod">ExcelFile.RowColumnResolutionMethod</seealso>
        public override CellStyle Style
        {
            get
            {
                if (this.cellValue is MergedCellRange)
                {
                    return ((MergedCellRange) this.cellValue).Style;
                }
                if (this.style == null)
                {
                    this.style = new CellStyle(base.Parent.ParentExcelFile.CellStyleCache);
                }
                return this.style;
            }
            set
            {
                if (this.cellValue is MergedCellRange)
                {
                    ((MergedCellRange) this.cellValue).Style = value;
                }
                else
                {
                    this.style = new CellStyle(value, base.Parent.ParentExcelFile.CellStyleCache);
                }
            }
        }

        /// <summary>
        /// Gets or sets value of this cell or of merged range if this cell is merged.
        /// </summary>
        /// <remarks>
        /// <p>Exception is thrown if value for the set is not of supported type (See 
        /// <see cref="M:GemBox.Spreadsheet.ExcelFile.SupportsType(System.Type)">ExcelFile.SupportsType</see> for details).</p>
        /// <p>Note that the fact some type is supported doesn't mean it is written to Excel file in the native format. As
        /// Microsoft Excel has just few basic types, the object of supported type will be converted to a similar excel type. 
        /// If similar excel type doesn't exist, value is written as a string value.</p>
        /// <p>If the value of this property is of <see cref="T:System.DateTime">DateTime</see> type and 
        /// <see cref="P:GemBox.Spreadsheet.ExcelCell.Style">Style</see> number format is not set, ISO date/time 
        /// format will be used as <see cref="P:GemBox.Spreadsheet.CellStyle.NumberFormat">CellStyle.NumberFormat</see> 
        /// value.</p>
        /// </remarks>
        /// <exception cref="T:System.NotSupportedException">Thrown for unsupported types.</exception>
        /// <seealso cref="P:GemBox.Spreadsheet.CellRange.Merged">CellRange.Merged</seealso>
        /// <seealso cref="P:GemBox.Spreadsheet.ExcelCell.MergedRange" />
        /// <seealso cref="M:GemBox.Spreadsheet.ExcelFile.SupportsType(System.Type)">ExcelFile.SupportsType</seealso>
        /// <seealso cref="P:GemBox.Spreadsheet.CellStyle.NumberFormat">CellStyle.NumberFormat</seealso>
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
                if (this.cellValue is MergedCellRange)
                {
                    return ((MergedCellRange) this.cellValue).ValueInternal;
                }
                return this.cellValue;
            }
            set
            {
                if (this.cellValue is MergedCellRange)
                {
                    ((MergedCellRange) this.cellValue).ValueInternal = value;
                }
                else
                {
                    this.cellValue = value;
                }
            }
        }
    }
}

