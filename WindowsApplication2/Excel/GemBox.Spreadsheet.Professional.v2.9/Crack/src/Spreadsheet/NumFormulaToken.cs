namespace GemBox.Spreadsheet
{
    using System;

    /// <summary>
    /// Formula token for holding integer.
    /// </summary>
    internal class NumFormulaToken : FormulaToken
    {
        private double value;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GemBox.Spreadsheet.NumFormulaToken" /> class.
        /// </summary>
        public NumFormulaToken() : base(FormulaTokenCode.Num, 9, FormulaTokenType.Operand)
        {
        }

        /// <summary>
        /// Convert formula token to array of byte representation.
        /// </summary>
        /// <returns>formula token' array of byte representation</returns>
        public override byte[] ConvertToBytes()
        {
            byte[] array = base.ConvertToBytes();
            BitConverter.GetBytes(this.value).CopyTo(array, 1);
            return array;
        }

        /// <summary>
        /// Make custom delay initialize.
        /// </summary>
        /// <param name="data">The data for initialization which is unique for each formula token.</param>
        public override void DelayInitialize(object[] data)
        {
            this.value = (double) data[0];
        }

        /// <summary>
        /// Initialize formula token by reading input data from array of bytes
        /// </summary>
        /// <param name="rpnBytes">input data, array of bytes</param>
        /// <param name="startIndex">start position for array of bytes to read from</param>
        public override void Read(byte[] rpnBytes, int startIndex)
        {
            this.value = BitConverter.ToDouble(rpnBytes, startIndex);
        }

        /// <summary>
        /// Convert formula token to string representation.
        /// </summary>
        /// <returns>formula token string representation</returns>
        public override string ToString()
        {
            return this.value.ToString();
        }

        public double Value
        {
            get
            {
                return this.value;
            }
        }
    }
}

