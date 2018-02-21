namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;

    internal class BoundSheetRecord : XLSRecord
    {
        private AbsXLSRec sheetBOFRecord;
        public ExcelShortString SheetName;
        private static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("BoundSheet");

        public BoundSheetRecord(ExcelShortString sheetName, AbsXLSRec sheetBOFRecord) : base(staticDescriptor)
        {
            base.InitializeBody((byte[]) null);
            this.SheetName = sheetName;
            this.sheetBOFRecord = sheetBOFRecord;
        }

        public BoundSheetRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord, IoOperationInfo operationInfo) : base(staticDescriptor, bodyLength, br)
        {
            this.SheetName = (ExcelShortString) base.GetArguments()[3];
        }

        protected override void InitializeDelayed()
        {
            uint address = (uint) this.sheetBOFRecord.Address;
            base.InitializeDelayed(new object[] { address, BoundSheetVisibility.Visible, BoundSheetSheetType.WorksheetOrDialogSheet, this.SheetName });
        }

        protected override int BodySize
        {
            get
            {
                if (this.Body == null)
                {
                    return (6 + this.SheetName.Size);
                }
                return base.Body.Length;
            }
        }
    }
}

