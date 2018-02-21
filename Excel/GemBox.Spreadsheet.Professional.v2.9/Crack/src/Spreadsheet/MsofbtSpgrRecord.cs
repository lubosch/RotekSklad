namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;

    internal class MsofbtSpgrRecord : MsoBaseRecord
    {
        private static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("MSOFBTSPGR");

        public MsofbtSpgrRecord() : base(staticDescriptor, MsoType.Spgr)
        {
            base.Version = 1;
            base.InitializeBody((byte[]) null);
        }

        public MsofbtSpgrRecord(BinaryReader reader) : base(staticDescriptor, reader, MsoType.Spgr)
        {
            base.Version = 1;
        }

        /// <summary>
        /// Reads data from the specified reader.
        /// </summary>
        /// <param name="reader">The source binary reader.</param>
        public override int Read(BinaryReader reader)
        {
            int num = base.Read(reader);
            reader.ReadBytes(0x10);
            return num;
        }

        /// <summary>
        /// Sets the inner data( mso structure' data without header: atom, type, length ).
        /// </summary>
        protected override byte[] SetData(MsoDelayedRecords delayedRecords)
        {
            byte[] array = new byte[0x10];
            int num = 0;
            BitConverter.GetBytes(num).CopyTo(array, 0);
            BitConverter.GetBytes(num).CopyTo(array, 4);
            BitConverter.GetBytes(num).CopyTo(array, 8);
            BitConverter.GetBytes(num).CopyTo(array, 12);
            return array;
        }
    }
}

