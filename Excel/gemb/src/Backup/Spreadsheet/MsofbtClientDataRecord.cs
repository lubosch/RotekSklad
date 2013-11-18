namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;
    using System.IO;

    internal class MsofbtClientDataRecord : MsoBaseRecord
    {
        private ArrayList items;
        private static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("MSOFBTCLIENTDATA");

        public MsofbtClientDataRecord() : base(staticDescriptor, MsoType.ClientData)
        {
            this.items = new ArrayList();
            base.InitializeBody((byte[]) null);
        }

        public MsofbtClientDataRecord(BinaryReader reader) : base(staticDescriptor, reader, MsoType.ClientData)
        {
            this.items = new ArrayList();
        }

        public void Add(XLSRecord subRecord)
        {
            this.items.Add(subRecord);
        }

        /// <summary>
        /// Converts mso structure' representation to bytes.
        /// </summary>
        public override byte[] ConvertToBytes(MsoDelayedRecords delayedRecords)
        {
            byte[] buffer = base.ConvertToBytes(delayedRecords);
            delayedRecords.DelayedRecords.Add(this.items);
            delayedRecords.Lengths.Add(delayedRecords.Offset);
            return buffer;
        }

        /// <summary>
        /// Reads data from the specified reader.
        /// </summary>
        /// <param name="reader">The source binary reader.</param>
        public override int Read(BinaryReader reader)
        {
            return base.Read(reader);
        }

        /// <summary>
        /// Sets the inner data( mso structure' data without header: atom, type, length ).
        /// </summary>
        protected override byte[] SetData(MsoDelayedRecords delayedRecords)
        {
            return new byte[0];
        }
    }
}

