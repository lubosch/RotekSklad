namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;
    using System.IO;

    internal class DBCellRecord : XLSRecord
    {
        public AbsXLSRec FirstRow;
        public ArrayList StartingCellsForRows;
        private static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("DBCell");

        public DBCellRecord() : base(staticDescriptor)
        {
            this.StartingCellsForRows = new ArrayList();
            base.InitializeBody((byte[]) null);
        }

        public DBCellRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord, IoOperationInfo operationInfo) : base(staticDescriptor, bodyLength, br)
        {
            this.StartingCellsForRows = new ArrayList();
        }

        protected override int GetVariableArraySize(object[] loadedArgs, object[] varArr, int bodySize)
        {
            return ((this.BodySize - 4) / 2);
        }

        protected override void InitializeDelayed()
        {
            if (this.FirstRow == null)
            {
                base.InitializeDelayed(new object[] { 0, new object[0] });
            }
            else
            {
                uint num = (uint) (base.Address - this.FirstRow.Address);
                AbsXLSRec rec = this;
                int index = this.StartingCellsForRows.Count - 1;
                while (index >= 0)
                {
                    if (this.StartingCellsForRows[index] == null)
                    {
                        this.StartingCellsForRows[index] = rec;
                    }
                    else
                    {
                        rec = (AbsXLSRec) this.StartingCellsForRows[index];
                    }
                    index--;
                }
                object[] objArray = new object[this.StartingCellsForRows.Count];
                int num3 = this.FirstRow.Address + 20;
                for (index = 0; index < this.StartingCellsForRows.Count; index++)
                {
                    int address = ((AbsXLSRec) this.StartingCellsForRows[index]).Address;
                    objArray[index] = (ushort) (address - num3);
                    num3 = address;
                }
                base.InitializeDelayed(new object[] { num, objArray });
            }
        }

        protected override int BodySize
        {
            get
            {
                if (this.Body == null)
                {
                    return (4 + (this.StartingCellsForRows.Count * 2));
                }
                return base.Body.Length;
            }
        }
    }
}

