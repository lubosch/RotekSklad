namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;

    internal class ExtSSTRecord : XLSRecord
    {
        private int offset;
        private AbsXLSRec sstRecord;
        private static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("ExtSST");
        private int stringsInBucket;

        public ExtSSTRecord(int stringsInBucket, int offset, AbsXLSRec sstRecord) : base(staticDescriptor)
        {
            base.InitializeBody((byte[]) null);
            this.stringsInBucket = stringsInBucket;
            this.offset = offset;
            this.sstRecord = sstRecord;
        }

        public ExtSSTRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord, IoOperationInfo operationInfo) : base(staticDescriptor, bodyLength, br)
        {
        }

        protected override int GetVariableArraySize(object[] loadedArgs, object[] varArr, int bodySize)
        {
            return ((bodySize - 2) / 8);
        }

        protected override void InitializeDelayed()
        {
            base.InitializeDelayed(new object[] { (ushort) this.stringsInBucket, new object[] { (uint) (this.sstRecord.Address + this.offset), (ushort) this.offset } });
        }

        protected override int BodySize
        {
            get
            {
                if (this.Body != null)
                {
                    return this.Body.Length;
                }
                return 10;
            }
        }
    }
}

