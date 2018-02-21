namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;

    /// <summary>
    /// SupBookRecord record is used to provide information about internal 3d references	
    /// </summary>
    internal class SupBookRecord : XLSRecord
    {
        private ushort sheetsCount;
        private static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("SUPBOOK");

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GemBox.Spreadsheet.SupBookRecord" /> class.
        /// </summary>
        public SupBookRecord() : base(staticDescriptor)
        {
            base.InitializeBody((byte[]) null);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GemBox.Spreadsheet.SupBookRecord" /> class.
        /// </summary>
        /// <param name="bodyLength">Length of the body.</param>
        /// <param name="br">The binary reader to read from.</param>
        /// <param name="previousRecord">The previous record.</param>
        /// <param name="operationInfo">Current operation information.</param>
        public SupBookRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord, IoOperationInfo operationInfo) : base(staticDescriptor, bodyLength, br)
        {
        }

        protected override void InitializeDelayed()
        {
            base.InitializeDelayed(new object[] { this.sheetsCount });
        }

        /// <summary>
        /// Gets or sets the sheets count in current workbook.
        /// </summary>
        /// <value>The sheets count in current workbook.</value>
        public ushort SheetsCount
        {
            get
            {
                return this.sheetsCount;
            }
            set
            {
                this.sheetsCount = value;
            }
        }
    }
}

