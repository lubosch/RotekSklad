namespace GemBox.Spreadsheet
{
    using System;

    /// <summary>
    /// Formula token for holding function with variable arguments count.
    /// </summary>
    internal class FunctionVarFormulaToken : FormulaToken
    {
        private byte argumentsCount;
        private FormulaFunctionInfo function;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GemBox.Spreadsheet.FunctionVarFormulaToken" /> class.
        /// </summary>
        /// <param name="code">The code.</param>
        public FunctionVarFormulaToken(FormulaTokenCode code) : base(code, 4, FormulaTokenType.Function)
        {
        }

        /// <summary>
        /// Convert formula token to array of byte representation.
        /// </summary>
        /// <returns>formula token' array of byte representation</returns>
        public override byte[] ConvertToBytes()
        {
            byte[] array = base.ConvertToBytes();
            array[1] = this.argumentsCount;
            BitConverter.GetBytes(this.function.Code).CopyTo(array, 2);
            return array;
        }

        /// <summary>
        /// Make custom delay initialize.
        /// </summary>
        /// <param name="data">The data for initialization which is unique for each formula token.</param>
        public override void DelayInitialize(object[] data)
        {
            this.function = FormulaFunctionsTable.Instance[data[0] as string];
            this.argumentsCount = (byte) data[1];
        }

        /// <summary>
        /// Initialize formula token by reading input data from array of bytes
        /// </summary>
        /// <param name="rpnBytes">input data, array of bytes</param>
        /// <param name="startIndex">start position for array of bytes to read from</param>
        public override void Read(byte[] rpnBytes, int startIndex)
        {
            this.argumentsCount = rpnBytes[startIndex];
            ushort num = BitConverter.ToUInt16(rpnBytes, startIndex + 1);
            this.function = FormulaFunctionsTable.Instance[num];
        }

        /// <summary>
        /// Convert formula token to string representation.
        /// </summary>
        /// <returns>formula token string representation</returns>
        public override string ToString()
        {
            return this.function.Name.ToUpper();
        }

        public byte ArgumentsCount
        {
            get
            {
                return this.argumentsCount;
            }
        }

        public FormulaFunctionInfo Function
        {
            get
            {
                return this.function;
            }
        }
    }
}

