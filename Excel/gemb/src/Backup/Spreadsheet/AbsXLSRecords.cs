namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Reflection;

    internal class AbsXLSRecords : IEnumerable
    {
        private ArrayList records = new ArrayList();

        public int Add(AbsXLSRec record)
        {
            return this.records.Add(record);
        }

        public IEnumerator GetEnumerator()
        {
            return this.records.GetEnumerator();
        }

        public void Read(BinaryReader br, IoOperationInfo operationInfo)
        {
            AbsXLSRec previousRecord = null;
            while (br.PeekChar() != -1)
            {
                previousRecord = AbsXLSRec.Read(br, previousRecord, operationInfo);
                if (previousRecord == null)
                {
                    return;
                }
                this.Add(previousRecord);
            }
        }

        public void SetRecordAddresses()
        {
            int num = 0;
            foreach (AbsXLSRec rec in this)
            {
                rec.Address = num;
                int recordCode = rec.RecordCode;
                num += rec.Size;
            }
        }

        public void Write(BinaryWriter bw)
        {
            foreach (AbsXLSRec rec in this)
            {
                rec.Write(bw);
            }
        }

        public AbsXLSRec this[int index]
        {
            get
            {
                return (AbsXLSRec) this.records[index];
            }
        }
    }
}

