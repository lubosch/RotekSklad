namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;
    using System.IO;

    internal class ObjRecord : XLSRecord
    {
        private byte[] cashedData;
        private ArrayList items;
        private static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("OBJ");

        public ObjRecord() : base(staticDescriptor)
        {
            this.items = new ArrayList();
            base.InitializeBody((byte[]) null);
        }

        public ObjRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord, IoOperationInfo operationInfo) : base(staticDescriptor, bodyLength, br)
        {
            this.items = new ArrayList();
        }

        public void Add(ObjSubRecord subRecord)
        {
            this.items.Add(subRecord);
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
                int offset = 0;
                foreach (ObjSubRecord record in this.items)
                {
                    byte[] bytes = record.ConvertToBytes();
                    buffer.Write(bytes, offset);
                    offset += bytes.Length;
                }
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
    }
}

