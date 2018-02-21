namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;
    using System.Text;

    internal class ContinueRecord : SSTRelated
    {
        private static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("Continue");

        public ContinueRecord(ExcelLongStrings excelStrings) : base(staticDescriptor)
        {
            base.ExcelStrings = excelStrings;
            base.InitializeDelayed(new object[] { excelStrings });
        }

        public ContinueRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord, IoOperationInfo operationInfo) : base(staticDescriptor, bodyLength, br)
        {
            SSTRelated related = previousRecord as SSTRelated;
            if ((related != null) && (related.ExcelStrings != null))
            {
                using (MemoryStream stream = new MemoryStream(this.Body))
                {
                    using (BinaryReader reader = new BinaryReader(stream, new UnicodeEncoding()))
                    {
                        base.ExcelStrings = new ExcelLongStrings(reader, bodyLength, related.ExcelStrings);
                    }
                }
            }
        }

        public override string FormattedBody
        {
            get
            {
                if (base.ExcelStrings != null)
                {
                    return base.ExcelStrings.ToString();
                }
                return "Unknown data";
            }
        }
    }
}

