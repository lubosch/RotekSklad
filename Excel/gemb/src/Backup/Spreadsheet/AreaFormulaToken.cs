namespace GemBox.Spreadsheet
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Formula token for holding reference on cell range.
    /// </summary>
    internal class AreaFormulaToken : FormulaToken
    {
        protected byte firstColumn;
        protected byte firstOptions;
        /// <summary>
        /// first row.
        /// </summary>
        protected ushort firstRow;
        /// <summary>
        /// Regula expression used to determinate whether the input string is cell range( area ) or not
        /// </summary>
        public static readonly Regex IsCellRangeRegex = new Regex(@"(?<Column1>[\$]?[A-Z][A-Z]?)(?<Row1>[\$]?\d+):(?<Column2>[\$]?[A-Z][A-Z]?)(?<Row2>[\$]?\d+)", regexOptions);
        protected byte lastColumn;
        protected byte lastOptions;
        protected ushort lastRow;
        /// <summary>
        /// Regular expression default settings
        /// </summary>
        private static RegexOptions regexOptions = RegexOptions.Compiled;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GemBox.Spreadsheet.AreaFormulaToken" /> class.
        /// </summary>
        /// <param name="code">The FormulaTokenCode code.</param>
        public AreaFormulaToken(FormulaTokenCode code) : base(code, 9, FormulaTokenType.Operand)
        {
        }

        protected AreaFormulaToken(FormulaTokenCode code, int size) : base(code, size, FormulaTokenType.Operand)
        {
        }

        /// <summary>
        /// Convert formula token to array of byte representation.
        /// </summary>
        /// <returns>formula token' array of byte representation</returns>
        public override byte[] ConvertToBytes()
        {
            byte[] array = base.ConvertToBytes();
            BitConverter.GetBytes(this.FirstRow).CopyTo(array, 1);
            BitConverter.GetBytes(this.lastRow).CopyTo(array, 3);
            array[5] = this.FirstColumn;
            array[6] = this.firstOptions;
            array[7] = this.lastColumn;
            array[8] = this.lastOptions;
            return array;
        }

        /// <summary>
        /// Make custom delay initialize.
        /// </summary>
        /// <param name="data">The data for initialization which is unique for each formula token.</param>
        public override void DelayInitialize(object[] data)
        {
            this.SetArea(data[0] as string);
        }

        public static bool IsAreaToken(ushort codeValue)
        {
            FormulaTokenCode code = (FormulaTokenCode) ((byte) codeValue);
            if ((code != FormulaTokenCode.Area1) && (code != FormulaTokenCode.Area2))
            {
                return (code == FormulaTokenCode.Area3);
            }
            return true;
        }

        /// <summary>
        /// Initialize formula token by reading input data from array of bytes
        /// </summary>
        /// <param name="rpnBytes">input data, array of bytes</param>
        /// <param name="startIndex">start position for array of bytes to read from</param>
        public override void Read(byte[] rpnBytes, int startIndex)
        {
            this.firstRow = BitConverter.ToUInt16(rpnBytes, startIndex);
            this.lastRow = BitConverter.ToUInt16(rpnBytes, startIndex + 2);
            this.firstColumn = rpnBytes[startIndex + 4];
            this.firstOptions = rpnBytes[startIndex + 5];
            this.lastColumn = rpnBytes[startIndex + 6];
            this.lastOptions = rpnBytes[startIndex + 7];
        }

        private void SetArea(string cell)
        {
            Match match = IsCellRangeRegex.Match(cell);
            string str = match.Groups["Row1"].Value;
            string str2 = match.Groups["Column1"].Value;
            string str3 = match.Groups["Row2"].Value;
            string str4 = match.Groups["Column2"].Value;
            this.SetArea(str, str3, str2, str4);
        }

        protected void SetArea(string row1, string row2, string column1, string column2)
        {
            this.SetFirstRow(row1);
            this.SetLastRow(row2);
            this.SetFirstColumn(column1);
            this.SetLastColumn(column2);
        }

        public static void SetAreaColumns(string value, out byte firstColumn, out byte lastColumn)
        {
            firstColumn = 0;
            lastColumn = 0;
            Match match = IsCellRangeRegex.Match(value);
            firstColumn = (byte) ExcelColumnCollection.ColumnNameToIndex(match.Groups["Column1"].Value);
            lastColumn = (byte) ExcelColumnCollection.ColumnNameToIndex(match.Groups["Column2"].Value);
        }

        public static void SetAreaRows(string value, out ushort firstRow, out ushort lastRow)
        {
            firstRow = 0;
            lastRow = 0;
            Match match = IsCellRangeRegex.Match(value);
            firstRow = (ushort) ExcelRowCollection.RowNameToIndex(match.Groups["Row1"].Value);
            lastRow = (ushort) ExcelRowCollection.RowNameToIndex(match.Groups["Row2"].Value);
        }

        private void SetFirstColumn(string column)
        {
            if (column[0] == RefFormulaToken.AbsoluteCellMark)
            {
                this.IsFirstColumnRelative = false;
                column = column.Substring(1);
            }
            else
            {
                this.IsFirstColumnRelative = true;
            }
            this.firstColumn = (byte) ExcelColumnCollection.ColumnNameToIndex(column);
        }

        private void SetFirstRow(string row)
        {
            if (row[0] == RefFormulaToken.AbsoluteCellMark)
            {
                this.IsFirstRowRelative = false;
                row = row.Substring(1);
            }
            else
            {
                this.IsFirstRowRelative = true;
            }
            this.firstRow = (ushort) ExcelRowCollection.RowNameToIndex(row);
        }

        private void SetLastColumn(string column)
        {
            if (column[0] == RefFormulaToken.AbsoluteCellMark)
            {
                this.IsLastColumnAbsolute = false;
                column = column.Substring(1);
            }
            else
            {
                this.IsLastColumnAbsolute = true;
            }
            this.lastColumn = (byte) ExcelColumnCollection.ColumnNameToIndex(column);
        }

        private void SetLastRow(string row)
        {
            if (row[0] == RefFormulaToken.AbsoluteCellMark)
            {
                this.IsLastRowRelative = false;
                row = row.Substring(1);
            }
            else
            {
                this.IsLastRowRelative = true;
            }
            this.lastRow = (ushort) ExcelRowCollection.RowNameToIndex(row);
        }

        /// <summary>
        /// Convert formula token to string representation.
        /// </summary>
        /// <returns>formula token string representation</returns>
        public override string ToString()
        {
            string str = this.IsFirstRowRelative ? ExcelRowCollection.RowIndexToName(this.FirstRow) : (RefFormulaToken.AbsoluteCellMark + ExcelRowCollection.RowIndexToName(this.FirstRow));
            string str2 = this.IsLastRowRelative ? ExcelRowCollection.RowIndexToName(this.LastRow) : (RefFormulaToken.AbsoluteCellMark + ExcelRowCollection.RowIndexToName(this.LastRow));
            string str3 = this.IsFirstColumnRelative ? ExcelColumnCollection.ColumnIndexToName(this.FirstColumn) : (RefFormulaToken.AbsoluteCellMark + ExcelColumnCollection.ColumnIndexToName(this.FirstColumn));
            string str4 = this.IsLastColumnAbsolute ? ExcelColumnCollection.ColumnIndexToName(this.LastColumn) : (RefFormulaToken.AbsoluteCellMark + ExcelColumnCollection.ColumnIndexToName(this.LastColumn));
            return (str3 + str + ":" + str4 + str2);
        }

        public byte FirstColumn
        {
            get
            {
                return this.firstColumn;
            }
        }

        /// <summary>
        /// Gets the first row.
        /// </summary>
        /// <value>The first row.</value>
        public ushort FirstRow
        {
            get
            {
                return this.firstRow;
            }
        }

        public bool IsFirstColumnRelative
        {
            get
            {
                return Utilities.IsBitSet((ushort) this.firstOptions, (ushort) 0x40);
            }
            set
            {
                this.firstOptions = Utilities.SetBit(this.firstOptions, 0x40, value);
            }
        }

        public bool IsFirstRowRelative
        {
            get
            {
                return Utilities.IsBitSet((ushort) this.firstOptions, (ushort) 0x80);
            }
            set
            {
                this.firstOptions = Utilities.SetBit(this.firstOptions, 0x80, value);
            }
        }

        public bool IsLastColumnAbsolute
        {
            get
            {
                return Utilities.IsBitSet((ushort) this.lastOptions, (ushort) 0x40);
            }
            set
            {
                this.lastOptions = Utilities.SetBit(this.lastOptions, 0x40, value);
            }
        }

        public bool IsLastRowRelative
        {
            get
            {
                return Utilities.IsBitSet((ushort) this.lastOptions, (ushort) 0x80);
            }
            set
            {
                this.lastOptions = Utilities.SetBit(this.lastOptions, 0x80, value);
            }
        }

        public byte LastColumn
        {
            get
            {
                return this.lastColumn;
            }
        }

        public ushort LastRow
        {
            get
            {
                return this.lastRow;
            }
        }
    }
}

