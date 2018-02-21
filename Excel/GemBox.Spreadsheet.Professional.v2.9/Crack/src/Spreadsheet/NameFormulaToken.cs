namespace GemBox.Spreadsheet
{
    using System;

    /// <summary>
    /// Formula token for holding the index to a NAME/EXTERNNAME record.
    /// </summary>
    internal class NameFormulaToken : FormulaToken
    {
        /// <summary>
        /// One-based index to ExternName record.
        /// </summary>
        private ushort nameIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GemBox.Spreadsheet.NameFormulaToken" /> class.
        /// </summary>
        /// <param name="code">The code.</param>
        public NameFormulaToken(FormulaTokenCode code) : base(code, 5, FormulaTokenType.Operand)
        {
        }

        /// <summary>
        /// Convert formula token to array of byte representation.
        /// </summary>
        /// <returns>formula token' array of byte representation</returns>
        public override byte[] ConvertToBytes()
        {
            byte[] array = base.ConvertToBytes();
            BitConverter.GetBytes(this.nameIndex).CopyTo(array, 1);
            return array;
        }

        /// <summary>
        /// Make custom delay initialize.
        /// </summary>
        /// <param name="data">The data for initialization which is unique for each formula token.</param>
        public override void DelayInitialize(object[] data)
        {
            string str = data[0] as string;
            ExcelWorksheet worksheet = data[1] as ExcelWorksheet;
            this.nameIndex = (ushort) (Array.IndexOf<string>(worksheet.NamedRanges.Names, str) + 1);
        }

        /// <summary>
        /// Initialize formula token by reading input data from array of bytes
        /// </summary>
        /// <param name="rpnBytes">input data, array of bytes</param>
        /// <param name="startIndex">start position for array of bytes to read from</param>
        public override void Read(byte[] rpnBytes, int startIndex)
        {
            this.nameIndex = BitConverter.ToUInt16(rpnBytes, startIndex + 1);
        }

        /// <summary>
        /// Convert formula token to string representation.
        /// </summary>
        /// <returns>formula token string representation</returns>
        public override string ToString()
        {
            return "Name";
        }
    }
}

