namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;

    internal abstract class MsoBaseRecord : XLSRecord
    {
        internal ushort atom;
        private MsoBaseRecord mParent;
        protected IoOperationInfo operationInfo;
        protected ushort type;

        public MsoBaseRecord(MsoType type) : base((XLSDescriptor) null)
        {
            this.type = (ushort) type;
        }

        public MsoBaseRecord(XLSDescriptor descriptor, MsoType type) : base(descriptor)
        {
            this.type = (ushort) type;
        }

        public MsoBaseRecord(XLSDescriptor descriptor, BinaryReader br, MsoType type) : base(descriptor, 0, br)
        {
            this.type = (ushort) type;
            this.Read(br);
        }

        public MsoBaseRecord(XLSDescriptor descriptor, int bodyLength, BinaryReader br, MsoType type, IoOperationInfo operationInfo) : base(descriptor, bodyLength, br)
        {
            this.type = (ushort) type;
            this.operationInfo = operationInfo;
            this.Read(br);
        }

        /// <summary>
        /// Converts mso structure' representation to bytes.
        /// </summary>
        public virtual byte[] ConvertToBytes(MsoDelayedRecords delayedRecords)
        {
            OptimizedBuffer buffer = new OptimizedBuffer();
            byte[] bytes = this.SetData(delayedRecords);
            buffer.Write(this.atom, 0);
            buffer.Write(this.type, 2);
            buffer.Write(bytes.Length, 4);
            buffer.Write(bytes, 8, 0, bytes.Length);
            return buffer.Buffer;
        }

        /// <summary>
        /// Reads data from the specified reader.
        /// </summary>
        /// <param name="reader">The source binary reader.</param>
        public virtual int Read(BinaryReader reader)
        {
            return reader.ReadInt32();
        }

        /// <summary>
        /// Sets the inner data( mso structure' data without header: atom, type, length ).
        /// </summary>
        protected abstract byte[] SetData(MsoDelayedRecords delayedRecords);

        /// <summary>
        /// Gets or setes the instance. Depending on the instance a record's contents 
        /// it can have different meanings.
        /// </summary>
        public int Instance
        {
            get
            {
                return ((this.atom & 0xfff0) >> 4);
            }
            set
            {
                this.atom = Utilities.SetBit(this.atom, 0xfff0, (ushort) (value << 4));
            }
        }

        public MsoBaseRecord Parent
        {
            get
            {
                return this.mParent;
            }
            set
            {
                this.mParent = value;
            }
        }

        /// <summary>
        /// Get or sets the version if the record is an atom. 
        /// If the record is a container, this field has a value of 0xFFFF.
        /// </summary>
        public int Version
        {
            get
            {
                return (this.atom & 15);
            }
            set
            {
                this.atom = Utilities.SetBit(this.atom, 15, (ushort) value);
            }
        }
    }
}

