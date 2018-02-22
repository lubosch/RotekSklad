namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;

    /// <summary>
    /// Externsheet record for holding information REF' structures
    /// </summary>
    internal class ExternsheetRecord : XLSRecord
    {
        private ushort[] sheetIndexes;
        private static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("EXTERNSHEET");

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GemBox.Spreadsheet.ExternsheetRecord" /> class.
        /// </summary>
        public ExternsheetRecord() : base(staticDescriptor)
        {
            base.InitializeBody((byte[]) null);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GemBox.Spreadsheet.ExternsheetRecord" /> class.
        /// </summary>
        /// <param name="bodyLength">Length of the body.</param>
        /// <param name="br">The binary reader to read from.</param>
        /// <param name="previousRecord">The previous record.</param>
        /// <param name="operationInfo">Current operation information.</param>
        public ExternsheetRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord, IoOperationInfo operationInfo) : base(staticDescriptor, bodyLength, br)
        {
        }

        protected override int GetVariableArraySize(object[] loadedArgs, object[] varArr, int bodySize)
        {
            return (ushort) loadedArgs[0];
        }

        protected override void InitializeDelayed()
        {
            GemBox.Spreadsheet.SheetIndexes[] indexesArray = new GemBox.Spreadsheet.SheetIndexes[this.sheetIndexes.Length];
            for (int i = 0; i < this.sheetIndexes.Length; i++)
            {
                indexesArray[i] = new GemBox.Spreadsheet.SheetIndexes(this.sheetIndexes[i]);
            }
            base.InitializeDelayed(new object[] { (ushort) this.sheetIndexes.Length, indexesArray });
        }

        protected override int BodySize
        {
            get
            {
                if (this.Body != null)
                {
                    return this.Body.Length;
                }
                return ((this.sheetIndexes.Length * 6) + 2);
            }
        }

        /// <summary>
        /// Gets the sheet indexes.
        /// </summary>
        /// <value>The sheet indexes.</value>
        internal ushort[] SheetIndexes
        {
            get
            {
                return this.sheetIndexes;
            }
            set
            {
                this.sheetIndexes = value;
            }
        }
    }
}

