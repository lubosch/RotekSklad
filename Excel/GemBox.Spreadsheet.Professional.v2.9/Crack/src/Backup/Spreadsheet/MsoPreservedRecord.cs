namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;

    internal class MsoPreservedRecord : MsoBaseRecord
    {
        private OptimizedBuffer bytes;
        private int index;
        private static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("MSODRAWINGGROUP");

        public MsoPreservedRecord(MsoType type, BinaryReader reader) : base(staticDescriptor, reader, type)
        {
            this.bytes = new OptimizedBuffer();
        }

        /// <summary>
        /// Reads data from the specified reader.
        /// </summary>
        /// <param name="reader">The source binary reader.</param>
        public override int Read(BinaryReader reader)
        {
            int num = base.Read(reader);
            if (base.type == 0xf11e)
            {
                for (int i = 0; i < 4; i++)
                {
                    this.bytes.Write(reader.ReadInt32(), this.index);
                    this.index += 4;
                }
            }
            return num;
        }

        /// <summary>
        /// Sets the inner data( mso structure' data without header: atom, type, length ).
        /// </summary>
        protected override byte[] SetData(MsoDelayedRecords delayedRecords)
        {
            return this.bytes.Buffer;
        }
    }
}

