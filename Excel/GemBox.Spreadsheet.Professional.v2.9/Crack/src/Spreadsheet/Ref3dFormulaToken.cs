namespace GemBox.Spreadsheet
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Formula token for holding 3d reference on internal cell.
    /// </summary>
    internal class Ref3dFormulaToken : RefFormulaToken
    {
        private ExcelWorksheet excelSheet;
        /// <summary>
        /// Regular expression used to determinate whether the input string is 3d cell or not
        /// </summary>
        public static readonly Regex IsCell3DRegex = new Regex(@"(?<Sheet>[\S ]+)[\!](?<Column>[\$]?[A-Z]+)(?<Row>[\$]?\d+)", regexOptions);
        /// <summary>
        /// REF entry' index on EXTERNSHEET record( see the Link Table ).
        /// </summary>
        private ushort refIndex;
        /// <summary>
        /// Regular expression default options
        /// </summary>
        private static RegexOptions regexOptions = RegexOptions.Compiled;
        private string sheet;
        private ExcelWorksheetCollection workbook;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GemBox.Spreadsheet.Ref3dFormulaToken" /> class.
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="code">The code.</param>
        public Ref3dFormulaToken(ExcelWorksheet sheet, FormulaTokenCode code) : base(code, 7)
        {
            this.excelSheet = sheet;
        }

        /// <summary>
        /// Convert formula token to array of byte representation.
        /// </summary>
        /// <returns>formula token' array of byte representation</returns>
        public override byte[] ConvertToBytes()
        {
            byte[] array = new byte[this.Size];
            array[0] = base.Code;
            this.SetSheet(this.sheet);
            BitConverter.GetBytes(this.refIndex).CopyTo(array, 1);
            BitConverter.GetBytes(base.row).CopyTo(array, 3);
            array[5] = base.Column;
            array[6] = base.options;
            return array;
        }

        /// <summary>
        /// Make custom delay initialize.
        /// </summary>
        /// <param name="data">The data for initialization which is unique for each formula token.</param>
        public override void DelayInitialize(object[] data)
        {
            this.workbook = data[1] as ExcelWorksheetCollection;
            this.Set3dCell(data[0] as string);
        }

        /// <summary>
        /// Determines whether is the specified code related to ref3d token.
        /// </summary>
        /// <param name="code">The code to be checked.</param>
        /// <returns>
        /// <c>true</c> if the specified code related to ref3d token; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsRef3dToken(byte code)
        {
            FormulaTokenCode code2 = (FormulaTokenCode) code;
            if ((code2 != FormulaTokenCode.Ref3d1) && (code2 != FormulaTokenCode.Ref3d2))
            {
                return (code2 == FormulaTokenCode.Ref3d3);
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
            this.refIndex = BitConverter.ToUInt16(rpnBytes, startIndex);
            base.row = BitConverter.ToUInt16(rpnBytes, startIndex + 2);
            base.column = rpnBytes[startIndex + 4];
            base.options = rpnBytes[startIndex + 5];
        }

        private void Set3dCell(string cell)
        {
            Match match = IsCell3DRegex.Match(cell);
            this.sheet = match.Groups["Sheet"].Value;
            string row = match.Groups["Row"].Value;
            string column = match.Groups["Column"].Value;
            base.SetCell(row, column);
            this.SetSheet(this.sheet);
        }

        private void SetSheet(string sheet)
        {
            this.refIndex = this.workbook.AddSheetReference(sheet);
        }

        /// <summary>
        /// Convert formula token to string representation.
        /// </summary>
        /// <returns>formula token string representation</returns>
        public override string ToString()
        {
            this.workbook = (this.workbook == null) ? this.excelSheet.Parent : this.workbook;
            return (this.workbook.SheetNames[this.ExternsheetIndex + 1] + "!" + base.ToString());
        }

        internal ushort ExternsheetIndex
        {
            get
            {
                return this.refIndex;
            }
        }
    }
}

