namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;

    internal class LabelSSTRecord : ByteArrRec
    {
        protected static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("LabelSST");

        public LabelSSTRecord(CellRecordHeader header, uint sstIndex)
        {
            int index = 0;
            base.AddBytes(BitConverter.GetBytes(header.Row), ref index);
            base.AddBytes(BitConverter.GetBytes(header.Column), ref index);
            base.AddBytes(BitConverter.GetBytes(header.StyleIndex), ref index);
            base.AddBytes(BitConverter.GetBytes(sstIndex), ref index);
        }

        public LabelSSTRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord, IoOperationInfo operationInfo) : base(bodyLength, br, previousRecord, operationInfo)
        {
        }

        protected override XLSDescriptor GetDescriptor()
        {
            return staticDescriptor;
        }

        public override string FormattedBody
        {
            get
            {
                return (this.Header.ToString() + " SSTIndex:" + this.SSTIndex);
            }
        }

        public CellRecordHeader Header
        {
            get
            {
                return new CellRecordHeader(BitConverter.ToUInt16(base.data, 0), BitConverter.ToUInt16(base.data, 2), BitConverter.ToUInt16(base.data, 4));
            }
        }

        public uint SSTIndex
        {
            get
            {
                return BitConverter.ToUInt32(base.data, 6);
            }
        }
    }
}

