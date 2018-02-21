namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;

    /// <summary>
    /// Formula token for holding error value.
    /// </summary>
    internal class ErrFormulaToken : FormulaToken
    {
        public static readonly Hashtable CodesToStrings = new Hashtable();
        public static readonly ArrayList ErrorsList = new ArrayList();
        public static readonly Hashtable StringsToCodes = new Hashtable();
        private byte value;

        /// <summary>
        /// Initializes the <see cref="T:GemBox.Spreadsheet.ErrFormulaToken" /> class.
        /// </summary>
        static ErrFormulaToken()
        {
            CodesToStrings[(byte) 0] = "#NULL!";
            CodesToStrings[(byte) 7] = "#DIV/0!";
            CodesToStrings[(byte) 15] = "#VALUE!";
            CodesToStrings[(byte) 0x17] = "#REF!";
            CodesToStrings[(byte) 0x1d] = "#NAME?";
            CodesToStrings[(byte) 0x24] = "#NUM!";
            CodesToStrings[(byte) 0x2a] = "#N/A!";
            StringsToCodes["#NULL!"] = (byte) 0;
            StringsToCodes["#DIV/0!"] = (byte) 7;
            StringsToCodes["#VALUE!"] = (byte) 15;
            StringsToCodes["#REF!"] = (byte) 0x17;
            StringsToCodes["#NAME?"] = (byte) 0x1d;
            StringsToCodes["#NUM!"] = (byte) 0x24;
            StringsToCodes["#N/A!"] = (byte) 0x2a;
            ErrorsList.AddRange(new string[] { "#NULL!", "#DIV/0!", "#VALUE!", "#REF!", "#NAME?", "#NUM!", "#N/A!" });
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GemBox.Spreadsheet.ErrFormulaToken" /> class.
        /// </summary>
        public ErrFormulaToken() : base(FormulaTokenCode.Err, 2, FormulaTokenType.Operand)
        {
        }

        /// <summary>
        /// Convert formula token to array of byte representation.
        /// </summary>
        /// <returns>formula token' array of byte representation</returns>
        public override byte[] ConvertToBytes()
        {
            byte[] buffer = base.ConvertToBytes();
            buffer[1] = this.value;
            return buffer;
        }

        /// <summary>
        /// Make custom delay initialize.
        /// </summary>
        /// <param name="data">The data for initialization which is unique for each formula token.</param>
        public override void DelayInitialize(object[] data)
        {
            this.value = (byte) StringsToCodes[(string) data[0]];
        }

        /// <summary>
        /// Initialize formula token by reading input data from array of bytes
        /// </summary>
        /// <param name="rpnBytes">input data, array of bytes</param>
        /// <param name="startIndex">start position for array of bytes to read from</param>
        public override void Read(byte[] rpnBytes, int startIndex)
        {
            this.value = rpnBytes[startIndex];
        }

        /// <summary>
        /// Convert formula token to string representation.
        /// </summary>
        /// <returns>formula token string representation</returns>
        public override string ToString()
        {
            return (CodesToStrings[this.value] as string);
        }

        public byte Value
        {
            get
            {
                return this.value;
            }
        }
    }
}

