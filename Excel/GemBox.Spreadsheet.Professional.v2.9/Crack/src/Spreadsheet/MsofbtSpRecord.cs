namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;

    internal class MsofbtSpRecord : MsoBaseRecord
    {
        private bool isTopMost;
        private uint shapeId;
        private static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("MSOFBTSP");

        public MsofbtSpRecord() : base(staticDescriptor, MsoType.Sp)
        {
            base.Version = 2;
            base.InitializeBody((byte[]) null);
        }

        public MsofbtSpRecord(BinaryReader reader) : base(staticDescriptor, reader, MsoType.Sp)
        {
            base.Version = 2;
        }

        /// <summary>
        /// Reads data from the specified reader.
        /// </summary>
        /// <param name="reader">The source binary reader.</param>
        public override int Read(BinaryReader reader)
        {
            int num = base.Read(reader);
            this.shapeId = reader.ReadUInt32();
            uint num2 = reader.ReadUInt32();
            this.isTopMost = (num2 & 4) == 1;
            return num;
        }

        /// <summary>
        /// Sets the inner data( mso structure' data without header: atom, type, length ).
        /// </summary>
        protected override byte[] SetData(MsoDelayedRecords delayedRecords)
        {
            byte[] array = new byte[8];
            BitConverter.GetBytes(this.shapeId).CopyTo(array, 0);
            array[4] = this.isTopMost ? ((byte) 4) : ((byte) 0);
            array[5] = 10;
            array[6] = 0;
            array[7] = 0;
            return array;
        }

        public bool IsTopMost
        {
            get
            {
                return this.isTopMost;
            }
            set
            {
                this.isTopMost = value;
            }
        }

        public uint ShapeId
        {
            get
            {
                return this.shapeId;
            }
            set
            {
                this.shapeId = value;
            }
        }
    }
}

