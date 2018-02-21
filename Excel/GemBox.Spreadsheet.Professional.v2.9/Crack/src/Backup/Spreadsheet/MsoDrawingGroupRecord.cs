namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;

    internal class MsoDrawingGroupRecord : XLSRecord
    {
        internal object[] arguments;
        private byte[] cashedData;
        private int continueTotalSize;
        private MsoContainerRecord parent;
        private static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("MSODRAWINGGROUP");

        public MsoDrawingGroupRecord() : base(staticDescriptor)
        {
            base.InitializeBody((byte[]) null);
        }

        public MsoDrawingGroupRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord, IoOperationInfo operationInfo) : base(staticDescriptor, bodyLength, br)
        {
            this.Read(bodyLength, br);
        }

        private void Read(int bodyLength, BinaryReader br)
        {
            if (bodyLength == 0x2020)
            {
                this.SkipContinueRecordsAndFillBody(br);
            }
            this.arguments = base.GetArguments();
            this.parent = this.arguments[0] as MsoContainerRecord;
        }

        private void SetBody()
        {
            if (this.cashedData == null)
            {
                MsoDelayedRecords delayedRecords = new MsoDelayedRecords();
                byte[] bytesWithoutContinueRecords = this.parent.ConvertToBytes(delayedRecords);
                if (bytesWithoutContinueRecords.Length < 0x2020)
                {
                    this.cashedData = bytesWithoutContinueRecords;
                }
                else
                {
                    this.SetBodyWithContinueRecords(bytesWithoutContinueRecords.Length, bytesWithoutContinueRecords);
                }
            }
        }

        private void SetBodyWithContinueRecords(int length, byte[] bytesWithoutContinueRecords)
        {
            int pos = 0;
            int offset = 0;
            int num3 = 0;
            OptimizedBuffer buffer = new OptimizedBuffer();
            while (length > 0x2020)
            {
                length -= 0x2020;
                buffer.Write(bytesWithoutContinueRecords, offset, pos, 0x2020);
                pos += 0x2020;
                offset += 0x2020;
                ushort data = (num3 == 0) ? ((ushort) 0xeb) : ((ushort) 3);
                buffer.Write(data, offset);
                offset += 2;
                ushort num5 = (length > 0x2020) ? ((ushort) 0x2020) : ((ushort) length);
                buffer.Write(num5, offset);
                offset += 2;
                num3++;
            }
            buffer.Write(bytesWithoutContinueRecords, offset, pos, length);
            this.cashedData = buffer.Buffer;
        }

        private void SkipContinueRecordsAndFillBody(BinaryReader br)
        {
            OptimizedBuffer buffer = new OptimizedBuffer();
            int offset = 0;
            buffer.Write(base.body, 0, 0, 0x2020);
            offset += 0x2020;
            int length = 0x2020;
            do
            {
                ushort num3 = br.ReadUInt16();
                ushort num4 = br.ReadUInt16();
                this.continueTotalSize += 4;
                length = ((num3 == 60) || (num3 == 0xeb)) ? num4 : 0;
                buffer.Write(br.ReadBytes(length), offset, 0, length);
                offset += length;
            }
            while (length == 0x2020);
            base.body = buffer.Buffer;
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
                return (this.Body.Length + this.continueTotalSize);
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

