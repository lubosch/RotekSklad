namespace GemBox.Spreadsheet
{
    using System;

    /// <summary>
    /// Base formula token class for inheritance
    /// </summary>
    internal abstract class FormulaToken
    {
        private int size;
        private FormulaTokenCode token;
        private FormulaTokenTypeEx type;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GemBox.Spreadsheet.FormulaToken" /> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="size">The size.</param>
        public FormulaToken(FormulaTokenCode code, int size) : this(code, size, FormulaTokenType.Empty)
        {
        }

        public FormulaToken(FormulaTokenCode code, int size, FormulaTokenType type)
        {
            this.token = code;
            this.size = size;
            this.type = new FormulaTokenTypeEx(type);
        }

        /// <summary>
        /// Convert formula token to bytes representation.
        /// </summary>
        /// <returns>bytes representation of the formula token</returns>
        public virtual byte[] ConvertToBytes()
        {
            byte[] buffer = new byte[this.Size];
            buffer[0] = (byte) this.Token;
            return buffer;
        }

        /// <summary>
        /// Make custom delay initialize.
        /// </summary>
        /// <param name="data">The data for initialization which is unique for each formula token.</param>
        public virtual void DelayInitialize(object[] data)
        {
        }

        /// <summary>
        /// Initialize formula token by reading input data from array of bytes
        /// </summary>
        /// <param name="rpnBytes">input data, array of bytes</param>
        /// <param name="startIndex">start position for array of bytes to read from</param>
        public virtual void Read(byte[] rpnBytes, int startIndex)
        {
        }

        /// <summary>
        /// Convert formula token to string representation.
        /// </summary>
        /// <returns>formula token string representation</returns>
        public override string ToString()
        {
            return string.Empty;
        }

        public byte Code
        {
            get
            {
                return (byte) this.Token;
            }
        }

        /// <summary>
        /// Gets the size of the formula token.
        /// </summary>
        /// <value>The size of the formula token.</value>
        public virtual int Size
        {
            get
            {
                return this.size;
            }
        }

        /// <summary>
        /// Gets the formula token code.
        /// </summary>
        /// <value>The formula token code.</value>
        protected FormulaTokenCode Token
        {
            get
            {
                return this.token;
            }
        }

        public FormulaTokenTypeEx Type
        {
            get
            {
                return this.type;
            }
        }
    }
}

