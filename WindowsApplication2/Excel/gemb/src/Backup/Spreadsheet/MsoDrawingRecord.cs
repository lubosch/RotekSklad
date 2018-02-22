namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;

    internal class MsoDrawingRecord : XLSRecord
    {
        internal object[] arguments;
        private byte[] cashedData;
        private byte[] data;
        private MsoContainerRecord parent;
        private static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("MSODRAWING");

        public MsoDrawingRecord() : base(staticDescriptor)
        {
            base.InitializeBody((byte[]) null);
        }

        public MsoDrawingRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord, IoOperationInfo operationInfo) : base(staticDescriptor, bodyLength, br)
        {
            this.arguments = base.GetArguments();
            this.Parent = this.arguments[0] as MsoContainerRecord;
        }

        private void SetBody()
        {
            if (this.cashedData == null)
            {
                this.cashedData = this.data;
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

        public byte[] Data
        {
            get
            {
                return this.data;
            }
            set
            {
                this.data = value;
            }
        }

        internal MsoContainerRecord Parent
        {
            get
            {
                return this.parent;
            }
            set
            {
                this.parent = value;
            }
        }
    }
}

