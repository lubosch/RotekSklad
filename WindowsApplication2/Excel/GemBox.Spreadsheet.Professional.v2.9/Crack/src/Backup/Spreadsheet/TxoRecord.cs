namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;
    using System.IO;

    internal class TxoRecord : XLSRecord
    {
        private byte[] cashedData;
        private ushort formattingRunsLen;
        private ArrayList items;
        private static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("TXO");
        private ushort textLen;

        public TxoRecord() : base(staticDescriptor)
        {
            this.items = new ArrayList();
            base.InitializeBody((byte[]) null);
        }

        public TxoRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord, IoOperationInfo operationInfo) : base(staticDescriptor, bodyLength, br)
        {
            this.items = new ArrayList();
        }

        protected override int GetVariableArraySize(object[] loadedArgs, object[] varArr, int bodySize)
        {
            return bodySize;
        }

        private void SetBody()
        {
            if (this.cashedData == null)
            {
                OptimizedBuffer buffer = new OptimizedBuffer();
                byte[] bytes = new byte[10];
                buffer.Write(bytes, 0);
                buffer.Write(this.textLen, 10);
                buffer.Write(this.formattingRunsLen, 12);
                byte[] buffer3 = new byte[4];
                buffer.Write(buffer3, 14);
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

        public ushort FormattingRunsLen
        {
            get
            {
                return this.formattingRunsLen;
            }
            set
            {
                this.formattingRunsLen = value;
            }
        }

        public ushort TextLen
        {
            get
            {
                return this.textLen;
            }
            set
            {
                this.textLen = value;
            }
        }
    }
}

