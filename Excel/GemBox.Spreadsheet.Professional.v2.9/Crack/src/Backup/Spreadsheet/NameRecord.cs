namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Name record for holding information about name which can be used in named cell\range
    /// </summary>
    internal class NameRecord : XLSRecord
    {
        private byte nameLength;
        private string nameValue;
        private object[] options;
        private CellRange range;
        private object[] rpnBytes;
        private ushort sheetIndex;
        private static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("NAME");
        private ExcelWorksheet worksheet;
        private ExcelWorksheetCollection worksheets;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GemBox.Spreadsheet.NameRecord" /> class.
        /// </summary>
        /// <param name="worksheet">The worksheet.</param>
        public NameRecord(ExcelWorksheet worksheet) : base(staticDescriptor)
        {
            this.worksheet = worksheet;
            base.InitializeBody((byte[]) null);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GemBox.Spreadsheet.NameRecord" /> class.
        /// </summary>
        /// <param name="bodyLength">Length of the body.</param>
        /// <param name="br">The binary readed to read from.</param>
        /// <param name="previousRecord">The previous record.</param>
        /// <param name="operationInfo">Current operation information.</param>
        public NameRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord, IoOperationInfo operationInfo) : base(staticDescriptor, bodyLength, br)
        {
        }

        /// <summary>
        /// Converts the name record range to RPN bytes.
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="range">The range to be converted.</param>
        /// <param name="sheetName">Sheet' name.</param>
        /// <param name="worksheets">The worksheets collection.</param>
        public static byte[] ConvertNameRecordRangeToRpnBytes(ExcelWorksheet sheet, CellRange range, string sheetName, ExcelWorksheetCollection worksheets)
        {
            FormulaToken token = null;
            string str = string.Empty;
            if ((range.Width == 1) && (range.Height == 1))
            {
                Match match = RefFormulaToken.IsCellRegex.Match(range.ToString());
                string str2 = ConvertToAbsolute(match.Groups["Row"].Value);
                string str3 = ConvertToAbsolute(match.Groups["Column"].Value);
                str = sheetName + "!" + str3 + str2;
                token = new Ref3dFormulaToken(sheet, FormulaTokenCode.Ref3d1);
            }
            else
            {
                Match match2 = AreaFormulaToken.IsCellRangeRegex.Match(range.ToString());
                string str4 = ConvertToAbsolute(match2.Groups["Row1"].Value);
                string str5 = ConvertToAbsolute(match2.Groups["Column1"].Value);
                string str6 = ConvertToAbsolute(match2.Groups["Row2"].Value);
                string str7 = ConvertToAbsolute(match2.Groups["Column2"].Value);
                str = sheetName + "!" + str5 + str4 + ":" + str7 + str6;
                token = new Area3dFormulaToken(FormulaTokenCode.Area3d1);
            }
            token.DelayInitialize(new object[] { str, worksheets });
            return token.ConvertToBytes();
        }

        private static string ConvertToAbsolute(string data)
        {
            if (data[0] != RefFormulaToken.AbsoluteCellMark)
            {
                data = data.Insert(0, RefFormulaToken.AbsoluteCellMark.ToString());
            }
            return data;
        }

        protected override byte GetStringLength(object[] loadedArgs)
        {
            return (byte) loadedArgs[1];
        }

        protected override int GetVariableArraySize(object[] loadedArgs, object[] varArr, int bodySize)
        {
            if (loadedArgs[1] != null)
            {
                return (ushort) loadedArgs[2];
            }
            return 3;
        }

        protected override void InitializeDelayed()
        {
            if ((this.options == null) || (this.options.Length == 0))
            {
                this.options = new object[] { (byte) 0, (byte) 0, (byte) 0 };
            }
            base.InitializeDelayed(new object[] { this.options, this.nameLength, (ushort) this.rpnBytes.Length, this.SheetIndex, new ExcelStringWithoutLength(this.nameValue), this.rpnBytes });
        }

        protected override int BodySize
        {
            get
            {
                if (this.Body != null)
                {
                    return this.Body.Length;
                }
                return ((15 + (this.nameValue.Length * 2)) + this.rpnBytes.Length);
            }
        }

        /// <summary>
        /// Gets or sets the name value.
        /// </summary>
        /// <value>The name value.</value>
        public string NameValue
        {
            get
            {
                return this.nameValue;
            }
            set
            {
                this.nameValue = value;
                this.nameLength = (byte) value.Length;
            }
        }

        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        /// <value>The options.</value>
        public object[] Options
        {
            get
            {
                return this.options;
            }
            set
            {
                this.options = value;
            }
        }

        /// <summary>
        /// Gets or sets the range to be associated with the user-defined name.
        /// </summary>
        /// <value>The range to be associated with the user-defined name.</value>
        public CellRange Range
        {
            get
            {
                return this.range;
            }
            set
            {
                this.range = value;
            }
        }

        /// <summary>
        /// Gets or sets the RPN bytes of formula used for referencing 3d cell or area.
        /// </summary>
        /// <value>The RPN bytes of formula used for referencing 3d cell or area.</value>
        public object[] RpnBytes
        {
            get
            {
                return this.rpnBytes;
            }
            set
            {
                this.rpnBytes = value;
            }
        }

        /// <summary>
        /// Gets or sets the index for the sheet which contain named cell\range.
        /// </summary>
        /// <value>The index for the sheet which contain named cell\range.</value>
        public ushort SheetIndex
        {
            get
            {
                return this.sheetIndex;
            }
            set
            {
                this.sheetIndex = value;
            }
        }

        /// <summary>
        /// Gets or sets the workbook\worksheets collection.
        /// </summary>
        /// <value>The workbook\worksheets collection.</value>
        public ExcelWorksheetCollection Worksheets
        {
            get
            {
                return this.worksheets;
            }
            set
            {
                this.worksheets = value;
            }
        }
    }
}

