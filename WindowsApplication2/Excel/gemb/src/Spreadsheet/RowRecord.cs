namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;

    internal class RowRecord : ByteArrRec
    {
        protected static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("Row");

        public RowRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord, IoOperationInfo operationInfo) : base(bodyLength, br, previousRecord, operationInfo)
        {
        }

        public RowRecord(ushort rowIndex, ushort firstColumn, ushort columnAfterLast, ushort rowHeight, RowOptions options, ushort styleIndex)
        {
            int index = 0;
            base.AddBytes(BitConverter.GetBytes(rowIndex), ref index);
            base.AddBytes(BitConverter.GetBytes(firstColumn), ref index);
            base.AddBytes(BitConverter.GetBytes(columnAfterLast), ref index);
            base.AddBytes(BitConverter.GetBytes(rowHeight), ref index);
            base.AddBytes(new byte[4], ref index);
            base.AddBytes(BitConverter.GetBytes((ushort) options), ref index);
            base.AddBytes(BitConverter.GetBytes(styleIndex), ref index);
        }

        protected override XLSDescriptor GetDescriptor()
        {
            return staticDescriptor;
        }

        public ushort ColumnAfterLast
        {
            get
            {
                return BitConverter.ToUInt16(base.data, 4);
            }
        }

        public ushort FirstColumn
        {
            get
            {
                return BitConverter.ToUInt16(base.data, 2);
            }
        }

        public override string FormattedBody
        {
            get
            {
                return string.Concat(new object[] { "RowIndex:", this.RowIndex, " FirstColumn:", this.FirstColumn, " ColumnAfterLast:", this.ColumnAfterLast, " RowHeight:", this.RowHeight, " Options:", this.Options, " StyleIndex:", this.StyleIndex });
            }
        }

        public RowOptions Options
        {
            get
            {
                return (RowOptions) BitConverter.ToUInt16(base.data, 12);
            }
        }

        public ushort RowHeight
        {
            get
            {
                return BitConverter.ToUInt16(base.data, 6);
            }
        }

        public ushort RowIndex
        {
            get
            {
                return BitConverter.ToUInt16(base.data, 0);
            }
        }

        public int StyleIndex
        {
            get
            {
                return (BitConverter.ToUInt16(base.data, 14) & 0xfff);
            }
        }
    }
}

