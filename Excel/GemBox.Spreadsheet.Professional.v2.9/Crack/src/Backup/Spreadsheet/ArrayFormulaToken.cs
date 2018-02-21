namespace GemBox.Spreadsheet
{
    using System;

    /// <summary>
    /// Formula token for holding array.
    /// </summary>
    internal class ArrayFormulaToken : FormulaToken
    {
        private byte columnsAmount;
        private ushort rowsAmount;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GemBox.Spreadsheet.ArrayFormulaToken" /> class.
        /// </summary>
        /// <param name="code">The FormulaTokenCode code.</param>
        public ArrayFormulaToken(FormulaTokenCode code) : base(code, 8, FormulaTokenType.Operand)
        {
        }

        /// <summary>
        /// Convert formula token to array of byte representation.
        /// </summary>
        /// <returns>formula token' array of byte representation</returns>
        public override byte[] ConvertToBytes()
        {
            byte[] buffer = base.ConvertToBytes();
            buffer[1] = 0;
            return buffer;
        }

        /// <summary>
        /// Initialize formula token by reading input data from array of bytes
        /// </summary>
        /// <param name="rpnBytes">input data, array of bytes</param>
        /// <param name="startIndex">start position for array of bytes to read from</param>
        public override void Read(byte[] rpnBytes, int startIndex)
        {
            this.columnsAmount = rpnBytes[startIndex];
            this.rowsAmount = BitConverter.ToUInt16(rpnBytes, startIndex + 1);
        }

        /// <summary>
        /// Convert formula token to string representation.
        /// </summary>
        /// <returns>formula token string representation</returns>
        public override string ToString()
        {
            return string.Empty;
        }

        public byte ColumnsAmount
        {
            get
            {
                return this.columnsAmount;
            }
        }

        public ushort RowsAmount
        {
            get
            {
                return this.rowsAmount;
            }
        }
    }
}

