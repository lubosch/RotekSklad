namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;

    internal class NoteRecord : XLSRecord
    {
        private byte[] cashedData;
        private ushort column;
        private ushort id;
        private bool isVisible;
        private ushort row;
        private static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("NOTE");

        public NoteRecord() : base(staticDescriptor)
        {
            base.InitializeBody((byte[]) null);
        }

        public NoteRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord, IoOperationInfo operationInfo) : base(staticDescriptor, bodyLength, br)
        {
        }

        private void SetBody()
        {
            if (this.cashedData == null)
            {
                OptimizedBuffer buffer = new OptimizedBuffer();
                ushort data = this.isVisible ? ((ushort) 2) : ((ushort) 0);
                buffer.Write(this.row, 0);
                buffer.Write(this.column, 2);
                buffer.Write(data, 4);
                buffer.Write(this.id, 6);
                buffer.Write((ushort) 0, 8);
                this.cashedData = buffer.Buffer;
            }
        }

        protected override void WriteBody(BinaryWriter bw)
        {
            this.SetBody();
            bw.Write(this.cashedData);
        }

        protected override int BodySize
        {
            get
            {
                if (this.Body == null)
                {
                    this.SetBody();
                    return this.cashedData.Length;
                }
                return this.Body.Length;
            }
        }

        public ushort Column
        {
            get
            {
                return this.column;
            }
            set
            {
                this.column = value;
            }
        }

        public ushort Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public bool IsVisible
        {
            get
            {
                return this.isVisible;
            }
            set
            {
                this.isVisible = value;
            }
        }

        public ushort Row
        {
            get
            {
                return this.row;
            }
            set
            {
                this.row = value;
            }
        }
    }
}

