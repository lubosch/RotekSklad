namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;

    internal class SSTRecord : SSTRelated
    {
        private static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("SST");
        public int TotalStringCount;
        public int UniqueStringCount;

        public SSTRecord(object[] args) : base(staticDescriptor)
        {
            base.InitializeDelayed(args);
        }

        public SSTRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord, IoOperationInfo operationInfo) : base(staticDescriptor, bodyLength, br)
        {
            object[] arguments = base.GetArguments();
            this.TotalStringCount = (int) ((uint) arguments[0]);
            this.UniqueStringCount = (int) ((uint) arguments[1]);
            base.ExcelStrings = (ExcelLongStrings) arguments[2];
        }

        protected override int GetVariableArraySize(object[] loadedArgs, object[] varArr, int bodySize)
        {
            return (bodySize - 8);
        }

        public override string FormattedBody
        {
            get
            {
                return string.Concat(new object[] { "TotalStringCount:", this.TotalStringCount, " UniqueStringCount:", this.UniqueStringCount, base.ExcelStrings.ToString() });
            }
        }
    }
}

