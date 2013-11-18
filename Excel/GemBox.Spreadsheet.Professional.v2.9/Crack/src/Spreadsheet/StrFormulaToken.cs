namespace GemBox.Spreadsheet
{
    using System;
    using System.Text;

    /// <summary>
    /// Formula token for holding string.
    /// </summary>
    internal class StrFormulaToken : FormulaToken
    {
        private bool isCompressed;
        public const char StartMark = '"';
        private string value;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GemBox.Spreadsheet.StrFormulaToken" /> class.
        /// </summary>
        public StrFormulaToken() : base(FormulaTokenCode.Str, 9, FormulaTokenType.Operand)
        {
        }

        /// <summary>
        /// Convert formula token to array of byte representation.
        /// </summary>
        /// <returns>formula token' array of byte representation</returns>
        public override byte[] ConvertToBytes()
        {
            byte[] array = new byte[(this.value.Length * 2) + 3];
            array[0] = base.Code;
            array[1] = (byte) this.value.Length;
            array[2] = 1;
            Encoding.Unicode.GetBytes(this.value).CopyTo(array, 3);
            return array;
        }

        /// <summary>
        /// Make custom delay initialize.
        /// </summary>
        /// <param name="data">The data for initialization which is unique for each formula token.</param>
        public override void DelayInitialize(object[] data)
        {
            this.value = (string) data[0];
        }

        /// <summary>
        /// Initialize formula token by reading input data from array of bytes
        /// </summary>
        /// <param name="rpnBytes">input data, array of bytes</param>
        /// <param name="startIndex">start position for array of bytes to read from</param>
        public override void Read(byte[] rpnBytes, int startIndex)
        {
            this.isCompressed = rpnBytes[startIndex + 1] == 1;
            byte length = rpnBytes[startIndex];
            this.value = Utilities.ReadString(this.isCompressed, rpnBytes, startIndex + 2, length);
        }

        /// <summary>
        /// Convert formula token to string representation.
        /// </summary>
        /// <returns>formula token string representation</returns>
        public override string ToString()
        {
            return this.value;
        }

        public bool IsCompressed
        {
            get
            {
                return this.isCompressed;
            }
        }

        public override int Size
        {
            get
            {
                int length = this.value.Length;
                if (!this.isCompressed)
                {
                    return (length + 3);
                }
                return ((length * 2) + 3);
            }
        }

        public string Value
        {
            get
            {
                return this.value;
            }
        }
    }
}

