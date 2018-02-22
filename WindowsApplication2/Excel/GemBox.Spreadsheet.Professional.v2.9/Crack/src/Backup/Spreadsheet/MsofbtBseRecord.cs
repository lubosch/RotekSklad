namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;

    internal class MsofbtBseRecord : MsoBaseRecord
    {
        private MsoBitmapPictureRecord picture;
        private byte requiredWin;
        private static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("MSOFBTBSE");

        public MsofbtBseRecord() : base(staticDescriptor, MsoType.BSE)
        {
            base.InitializeBody((byte[]) null);
        }

        public MsofbtBseRecord(BinaryReader reader, ushort atom) : base(staticDescriptor, reader, MsoType.BSE)
        {
            base.atom = atom;
        }

        /// <summary>
        /// Reads data from the specified reader.
        /// </summary>
        /// <param name="reader">The source binary reader.</param>
        public override int Read(BinaryReader reader)
        {
            int num = base.Read(reader);
            if (num == 0x24)
            {
                reader.ReadBytes(0x24);
                return num;
            }
            this.requiredWin = reader.ReadByte();
            reader.ReadBytes(0x13);
            reader.ReadUInt32();
            reader.ReadUInt32();
            reader.ReadUInt32();
            reader.ReadBytes(4);
            reader.ReadUInt16();
            reader.ReadUInt16();
            this.picture = new MsoBitmapPictureRecord();
            this.picture.Read(reader);
            return num;
        }

        /// <summary>
        /// Sets the inner data( mso structure' data without header: atom, type, length ).
        /// </summary>
        protected override byte[] SetData(MsoDelayedRecords delayedRecords)
        {
            OptimizedBuffer buffer = new OptimizedBuffer();
            byte[] bytes = this.Picture.ConvertToBytes(delayedRecords);
            if ((base.Instance == 5) && this.Picture.LoadedFromExcel)
            {
                base.Instance = 6;
            }
            buffer.Write(this.requiredWin, 0);
            byte data = 0;
            for (int i = 0; i < 0x13; i++)
            {
                buffer.Write(data, 1 + i);
            }
            buffer.Write((uint) bytes.Length, 20);
            buffer.Write((uint) 1, 0x18);
            for (int j = 0; j < 8; j++)
            {
                buffer.Write(data, 0x1c + j);
            }
            buffer.Write(bytes, 0x24, 0, bytes.Length);
            return buffer.Buffer;
        }

        public MsoBlipType BlipType
        {
            get
            {
                return (MsoBlipType) base.Instance;
            }
            set
            {
                base.Instance = (int) value;
            }
        }

        public MsoBitmapPictureRecord Picture
        {
            get
            {
                return this.picture;
            }
            set
            {
                this.picture = value;
            }
        }

        public byte RequiredWin
        {
            get
            {
                return this.requiredWin;
            }
            set
            {
                this.requiredWin = value;
            }
        }
    }
}

