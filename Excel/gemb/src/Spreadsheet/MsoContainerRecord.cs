namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;
    using System.IO;

    internal class MsoContainerRecord : MsoBaseRecord
    {
        protected ArrayList items;

        public MsoContainerRecord(XLSDescriptor descriptor, MsoType type) : base(descriptor, type)
        {
            this.items = new ArrayList();
            this.SetInstanceAndVersion(type);
            base.InitializeBody((byte[]) null);
        }

        public MsoContainerRecord(XLSDescriptor descriptor, BinaryReader reader, MsoType type) : base(descriptor, reader, type)
        {
            this.items = new ArrayList();
            this.SetInstanceAndVersion(type);
        }

        /// <summary>
        /// Adds the specified item to container.
        /// </summary>
        /// <param name="item">The item to be added.</param>
        public void Add(MsoBaseRecord item)
        {
            this.items.Add(item);
        }

        /// <summary>
        /// Reads data from the specified reader.
        /// </summary>
        /// <param name="reader">The source binary reader.</param>
        public override int Read(BinaryReader reader)
        {
            int num = base.Read(reader);
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                MsoBaseRecord item = ImageRecordsFactory.CreateFromReader(reader);
                this.Add(item);
            }
            return num;
        }

        /// <summary>
        /// Sets the inner data( mso structure' data without header: atom, type, length ).
        /// </summary>
        protected override byte[] SetData(MsoDelayedRecords delayedRecords)
        {
            OptimizedBuffer buffer = new OptimizedBuffer();
            int length = 0;
            int offset = 0;
            int num3 = delayedRecords.Offset;
            for (int i = 0; i < this.items.Count; i++)
            {
                delayedRecords.Offset = (num3 + offset) + 8;
                byte[] bytes = (this.items[i] as MsoBaseRecord).ConvertToBytes(delayedRecords);
                buffer.Write(bytes, offset, 0, bytes.Length);
                length = bytes.Length;
                offset += length;
            }
            return buffer.Buffer;
        }

        private void SetInstanceAndVersion(MsoType type)
        {
            base.Version = 15;
            if (type == MsoType.BstoreContainer)
            {
                base.Instance = 1;
            }
        }

        internal ArrayList Items
        {
            get
            {
                return this.items;
            }
        }
    }
}

