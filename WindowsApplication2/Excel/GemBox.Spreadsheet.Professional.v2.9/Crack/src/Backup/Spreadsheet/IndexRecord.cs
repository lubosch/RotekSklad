namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;
    using System.IO;

    internal class IndexRecord : XLSRecord
    {
        public ArrayList DBCells;
        public int FirstRow;
        public int LastRowPlusOne;
        private static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("Index");

        public IndexRecord() : base(staticDescriptor)
        {
            this.FirstRow = -1;
            this.LastRowPlusOne = -1;
            this.DBCells = new ArrayList();
            base.InitializeBody((byte[]) null);
        }

        public IndexRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord, IoOperationInfo operationInfo) : base(staticDescriptor, bodyLength, br)
        {
            this.FirstRow = -1;
            this.LastRowPlusOne = -1;
            this.DBCells = new ArrayList();
        }

        protected override int GetVariableArraySize(object[] loadedArgs, object[] varArr, int bodySize)
        {
            return ((this.BodySize - 0x10) / 4);
        }

        protected override void InitializeDelayed()
        {
            object[] objArray = new object[this.DBCells.Count];
            for (int i = 0; i < this.DBCells.Count; i++)
            {
                objArray[i] = (uint) ((DBCellRecord) this.DBCells[i]).Address;
            }
            base.InitializeDelayed(new object[] { (uint) this.FirstRow, (uint) this.LastRowPlusOne, objArray });
        }

        protected override int BodySize
        {
            get
            {
                if (this.Body == null)
                {
                    return (0x10 + (this.DBCells.Count * 4));
                }
                return this.Body.Length;
            }
        }
    }
}

