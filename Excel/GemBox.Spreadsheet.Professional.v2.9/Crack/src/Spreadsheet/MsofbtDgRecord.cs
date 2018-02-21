namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;

    internal class MsofbtDgRecord : MsoBaseRecord
    {
        private uint lastId;
        private uint shapesNumber;
        private static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("MSOFBTDG");

        public MsofbtDgRecord() : base(staticDescriptor, MsoType.Dg)
        {
            base.Instance = 1;
            base.InitializeBody((byte[]) null);
        }

        public MsofbtDgRecord(BinaryReader reader) : base(staticDescriptor, reader, MsoType.Dg)
        {
            base.Instance = 1;
        }

        /// <summary>
        /// Reads data from the specified reader.
        /// </summary>
        /// <param name="reader">The source binary reader.</param>
        public override int Read(BinaryReader reader)
        {
            int num = base.Read(reader);
            this.shapesNumber = reader.ReadUInt32();
            this.lastId = reader.ReadUInt32();
            return num;
        }

        /// <summary>
        /// Sets the inner data( mso structure' data without header: atom, type, length ).
        /// </summary>
        protected override byte[] SetData(MsoDelayedRecords delayedRecords)
        {
            byte[] array = new byte[8];
            BitConverter.GetBytes(this.shapesNumber).CopyTo(array, 0);
            BitConverter.GetBytes(this.lastId).CopyTo(array, 4);
            return array;
        }

        public uint LastId
        {
            get
            {
                return this.lastId;
            }
            set
            {
                this.lastId = value;
            }
        }

        internal uint ShapesNumber
        {
            get
            {
                return this.shapesNumber;
            }
            set
            {
                this.shapesNumber = value;
            }
        }
    }
}

