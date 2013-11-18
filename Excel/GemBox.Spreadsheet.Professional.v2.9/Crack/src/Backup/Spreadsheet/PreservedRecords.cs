namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;

    internal class PreservedRecords
    {
        private Hashtable typeGroupedRecords;

        internal PreservedRecords()
        {
            this.typeGroupedRecords = new Hashtable();
        }

        internal PreservedRecords(PreservedRecords source)
        {
            this.typeGroupedRecords = new Hashtable();
            this.typeGroupedRecords = (Hashtable) source.typeGroupedRecords.Clone();
        }

        public void Add(XLSRecord record)
        {
            this.Add(record, record.RecordCode);
        }

        public void Add(XLSRecord record, int recordCode)
        {
            ArrayList list = (ArrayList) this.typeGroupedRecords[recordCode];
            if (list != null)
            {
                list.Add(record);
            }
            else
            {
                list = new ArrayList();
                list.Add(record);
                this.typeGroupedRecords[recordCode] = list;
            }
        }

        public void CopyRecords(PreservedRecords source, int recordCode)
        {
            ArrayList list = (ArrayList) source.typeGroupedRecords[recordCode];
            if (list != null)
            {
                foreach (XLSRecord record in list)
                {
                    this.Add(record, recordCode);
                }
            }
        }

        public void CopyRecords(PreservedRecords source, string recordName)
        {
            int code = XLSDescriptors.GetByName(recordName).Code;
            this.CopyRecords(source, code);
        }

        public void WriteRecords(AbsXLSRecords destination, int recordCode)
        {
            ArrayList list = (ArrayList) this.typeGroupedRecords[recordCode];
            if (list != null)
            {
                foreach (XLSRecord record in list)
                {
                    destination.Add(record);
                }
            }
        }

        public void WriteRecords(AbsXLSRecords destination, string recordName)
        {
            int code = XLSDescriptors.GetByName(recordName).Code;
            this.WriteRecords(destination, code);
        }
    }
}

