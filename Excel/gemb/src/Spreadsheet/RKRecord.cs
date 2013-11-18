namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;

    internal class RKRecord : AbsXLSRec
    {
        public CellRecordHeader Header;
        private static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("RK");
        public uint Val;

        public RKRecord(CellRecordHeader header, uint val)
        {
            this.Header = header;
            this.Val = val;
        }

        public RKRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord, IoOperationInfo operationInfo)
        {
            this.Header = new CellRecordHeader(br);
            this.Val = br.ReadUInt32();
        }

        protected override void WriteBody(BinaryWriter bw)
        {
            this.Header.Write(bw);
            bw.Write(this.Val);
        }

        protected override int BodySize
        {
            get
            {
                return staticDescriptor.BodySize;
            }
        }

        public override string FormattedBody
        {
            get
            {
                return (this.Header.ToString() + " Val:" + this.Val);
            }
        }

        public override string Name
        {
            get
            {
                return staticDescriptor.Name;
            }
        }

        internal override int RecordCode
        {
            get
            {
                return staticDescriptor.Code;
            }
        }
    }
}

