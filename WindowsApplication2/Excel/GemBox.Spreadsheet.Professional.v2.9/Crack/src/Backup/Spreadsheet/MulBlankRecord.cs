namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;

    internal class MulBlankRecord : XLSRecord
    {
        private static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("MulBlank");

        public MulBlankRecord(object[] args) : base(staticDescriptor)
        {
            base.InitializeBody(args);
        }

        public MulBlankRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord, IoOperationInfo operationInfo) : base(staticDescriptor, bodyLength, br)
        {
        }

        protected override int GetVariableArraySize(object[] loadedArgs, object[] varArr, int bodySize)
        {
            return ((bodySize - 6) / 2);
        }
    }
}

