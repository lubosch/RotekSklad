namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;

    internal abstract class ByteArrRec : AbsXLSRec
    {
        protected byte[] data;

        public ByteArrRec()
        {
            this.data = new byte[this.GetDescriptor().BodySize];
        }

        public ByteArrRec(int bodyLength, BinaryReader br, AbsXLSRec previousRecord, IoOperationInfo operationInfo)
        {
            this.data = br.ReadBytes(this.GetDescriptor().BodySize);
        }

        protected void AddBytes(byte[] bytes, ref int index)
        {
            foreach (byte num in bytes)
            {
                this.data[index] = num;
                index++;
            }
        }

        protected abstract XLSDescriptor GetDescriptor();
        protected override void WriteBody(BinaryWriter bw)
        {
            bw.Write(this.data);
        }

        protected override int BodySize
        {
            get
            {
                return this.GetDescriptor().BodySize;
            }
        }

        public override string Name
        {
            get
            {
                return this.GetDescriptor().Name;
            }
        }

        internal override int RecordCode
        {
            get
            {
                return this.GetDescriptor().Code;
            }
        }
    }
}

