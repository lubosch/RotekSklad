namespace GemBox.Spreadsheet
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Formula token for holding 3d reference on internal cell range.
    /// </summary>
    internal class Area3dFormulaToken : AreaFormulaToken
    {
        /// <summary>
        /// Regular expression used to determinate whether the input string is 3d cell range( 1t case ) or not
        /// </summary>
        public static readonly Regex IsCellRange3DRegex = new Regex(@"(?<Sheet>[\S ]+)[\!](?<Column1>[\$]?[A-Z]+)(?<Row1>[\$]?\d+):(?<Column2>[\$]?[A-Z]+)(?<Row2>[\$]?\d+)", regexOptions);
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
        /// Initializes a new instance of the <see cref="T:GemBox.Spreadsheet.Area3dFormulaToken" /> class.
        /// </summary>
        /// <param name="code">The FormulaTokenCode code.</param>
        public Area3dFormulaToken(FormulaTokenCode code) : base(code, 11)
        {
        }

        /// <summary>
        /// Convert formula token to array of byte representation.
        /// </summary>
        /// <returns>formula token' array of byte representation</returns>
        public override byte[] ConvertToBytes()
        {
            byte[] sourceArray = base.ConvertToBytes();
            byte[] destinationArray = new byte[this.Size];
            destinationArray[0] = sourceArray[0];
            Array.Copy(sourceArray, 1, destinationArray, 3, 8);
            if (this.sheet != null)
            {
                this.SetSheet(this.sheet);
            }
            BitConverter.GetBytes(this.refIndex).CopyTo(destinationArray, 1);
            return destinationArray;
        }

        /// <summary>
        /// Make custom delay initialize.
        /// </summary>
        /// <param name="data">The data for initialization which is unique for each formula token.</param>
        public override void DelayInitialize(object[] data)
        {
            this.workbook = data[1] as ExcelWorksheetCollection;
            this.SetAred3dCell(data[0] as string);
        }

        /// <summary>
        /// Determines whether is the specified code related to area3d token.
        /// </summary>
        /// <param name="code">The code to be checked.</param>
        /// <returns>
        /// <c>true</c> if the specified code related to area3d token; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsArea3dToken(byte code)
        {
            FormulaTokenCode code2 = (FormulaTokenCode) code;
            if ((code2 != FormulaTokenCode.Area3d1) && (code2 != FormulaTokenCode.Area3d2))
            {
                return (code2 == FormulaTokenCode.Area3d3);
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
            base.firstRow = BitConverter.ToUInt16(rpnBytes, startIndex + 2);
            base.lastRow = BitConverter.ToUInt16(rpnBytes, startIndex + 4);
            base.firstColumn = rpnBytes[startIndex + 6];
            base.firstOptions = rpnBytes[startIndex + 7];
            base.lastColumn = rpnBytes[startIndex + 8];
            base.lastOptions = rpnBytes[startIndex + 9];
        }

        private void SetAred3dCell(string cell)
        {
            Match match = IsCellRange3DRegex.Match(cell);
            this.sheet = match.Groups["Sheet"].Value;
            string str = match.Groups["Column1"].Value;
            string str2 = match.Groups["Row1"].Value;
            string str3 = match.Groups["Column2"].Value;
            string str4 = match.Groups["Row2"].Value;
            base.SetArea(str2, str4, str, str3);
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
            return (this.sheet + "!" + base.ToString());
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

