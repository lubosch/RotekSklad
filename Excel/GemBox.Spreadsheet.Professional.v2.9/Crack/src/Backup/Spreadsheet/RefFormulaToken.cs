namespace GemBox.Spreadsheet
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Formula token for holding reference.
    /// </summary>
    internal class RefFormulaToken : FormulaToken
    {
        /// <summary>
        /// Absolute preffix row\height symbol
        /// </summary>
        public static readonly char AbsoluteCellMark = '$';
        protected byte column;
        /// <summary>
        /// Bit mask for column options.
        /// </summary>
        public const byte ColumnBitMask = 0x40;
        /// <summary>
        /// Regular expression used to determinate whether the input string is cell or not
        /// </summary>
        public static readonly Regex IsCellRegex = new Regex(@"(?<Column>[\$]?[A-Z][A-Z]?)(?<Row>[\$]?\d+)", regexOptions);
        /// <summary>
        /// Regular expression used to determinate whether the input string is column or not
        /// </summary>
        public static readonly Regex IsColumnRegex = new Regex(@"(?<Column>[\$]?[A-Z][A-Z]?)", regexOptions);
        protected byte options;
        /// <summary>
        /// Regular expression default options
        /// </summary>
        private static RegexOptions regexOptions = RegexOptions.Compiled;
        protected ushort row;
        /// <summary>
        /// Bit mask for row options.
        /// </summary>
        public const byte RowBitMask = 0x80;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GemBox.Spreadsheet.RefFormulaToken" /> class.
        /// </summary>
        /// <param name="code">The code.</param>
        public RefFormulaToken(FormulaTokenCode code) : base(code, 5, FormulaTokenType.Operand)
        {
        }

        protected RefFormulaToken(FormulaTokenCode code, int size) : base(code, size, FormulaTokenType.Operand)
        {
        }

        public static byte CellToColumn(string value)
        {
            return (byte) ExcelColumnCollection.ColumnNameToIndex(IsCellRegex.Match(value).Groups["Column"].Value);
        }

        public static ushort CellToRow(string value)
        {
            return (ushort) ExcelRowCollection.RowNameToIndex(IsCellRegex.Match(value).Groups["Row"].Value);
        }

        /// <summary>
        /// Convert formula token to array of byte representation.
        /// </summary>
        /// <returns>formula token' array of byte representation</returns>
        public override byte[] ConvertToBytes()
        {
            byte[] array = base.ConvertToBytes();
            BitConverter.GetBytes(this.row).CopyTo(array, 1);
            array[3] = this.column;
            array[4] = this.options;
            return array;
        }

        /// <summary>
        /// Make custom delay initialize.
        /// </summary>
        /// <param name="data">The data for initialization which is unique for each formula token.</param>
        public override void DelayInitialize(object[] data)
        {
            this.SetCell(data[0] as string);
        }

        public static bool IsCell(string match)
        {
            bool flag = false;
            Match regexMatch = IsCellRegex.Match(match);
            if (regexMatch.Success && (regexMatch.Value == match))
            {
                flag = IsValidCell(regexMatch);
            }
            return flag;
        }

        public static bool IsCellRange(string match)
        {
            bool flag = false;
            Match match2 = AreaFormulaToken.IsCellRangeRegex.Match(match);
            if (match2.Success)
            {
                flag = IsColumnValid(match2.Groups["Column1"].Value);
                flag = flag ? IsColumnValid(match2.Groups["Column2"].Value) : flag;
            }
            return flag;
        }

        public static bool IsColumnValid(int match)
        {
            return ((match >= 0) && (match <= 0xff));
        }

        public static bool IsColumnValid(string match)
        {
            return IsColumnRegex.Match(match).Success;
        }

        public static bool IsRowValid(int match)
        {
            return ((match >= 0) && (match <= 0x10000));
        }

        public static bool IsRowValid(string match)
        {
            return IsRowValid(NumbersParser.StrToInt(match));
        }

        private static bool IsValidCell(Match regexMatch)
        {
            string name = regexMatch.Groups["Row"].Value;
            if (name[0] == AbsoluteCellMark)
            {
                name = name.Remove(0, 1);
            }
            string str2 = regexMatch.Groups["Column"].Value;
            if (str2[0] == AbsoluteCellMark)
            {
                str2 = str2.Remove(0, 1);
            }
            return (IsColumnValid(ExcelColumnCollection.ColumnNameToIndex(str2)) && IsRowValid(ExcelRowCollection.RowNameToIndex(name)));
        }

        /// <summary>
        /// Initialize formula token by reading input data from array of bytes
        /// </summary>
        /// <param name="rpnBytes">input data, array of bytes</param>
        /// <param name="startIndex">start position for array of bytes to read from</param>
        public override void Read(byte[] rpnBytes, int startIndex)
        {
            this.row = BitConverter.ToUInt16(rpnBytes, startIndex);
            this.column = rpnBytes[startIndex + 2];
            this.options = rpnBytes[startIndex + 3];
        }

        private void SetCell(string cell)
        {
            Match match = IsCellRegex.Match(cell);
            string row = match.Groups["Row"].Value;
            string column = match.Groups["Column"].Value;
            this.SetCell(row, column);
        }

        protected void SetCell(string row, string column)
        {
            this.SetRow(row);
            this.SetColumn(column);
        }

        private void SetColumn(string column)
        {
            if (column[0] == AbsoluteCellMark)
            {
                this.IsColumnRelative = false;
                column = column.Substring(1);
            }
            else
            {
                this.IsColumnRelative = true;
            }
            this.column = (byte) ExcelColumnCollection.ColumnNameToIndex(column);
        }

        private void SetRow(string row)
        {
            if (row[0] == AbsoluteCellMark)
            {
                this.IsRowRelative = false;
                row = row.Substring(1);
            }
            else
            {
                this.IsRowRelative = true;
            }
            this.row = (ushort) ExcelRowCollection.RowNameToIndex(row);
        }

        /// <summary>
        /// Convert formula token to string representation.
        /// </summary>
        /// <returns>formula token string representation</returns>
        public override string ToString()
        {
            string str = this.IsRowRelative ? ExcelRowCollection.RowIndexToName(this.row) : (AbsoluteCellMark + ExcelRowCollection.RowIndexToName(this.row));
            string str2 = this.IsColumnRelative ? ExcelColumnCollection.ColumnIndexToName(this.column) : (AbsoluteCellMark + ExcelColumnCollection.ColumnIndexToName(this.column));
            return (str2 + str);
        }

        public byte Column
        {
            get
            {
                return this.column;
            }
        }

        public bool IsColumnRelative
        {
            get
            {
                return Utilities.IsBitSet((ushort) this.options, (ushort) 0x40);
            }
            set
            {
                this.options = Utilities.SetBit(this.options, 0x40, value);
            }
        }

        public bool IsRowRelative
        {
            get
            {
                return Utilities.IsBitSet((ushort) this.options, (ushort) 0x80);
            }
            set
            {
                this.options = Utilities.SetBit(this.options, 0x80, value);
            }
        }

        public ushort Row
        {
            get
            {
                return this.row;
            }
        }
    }
}

