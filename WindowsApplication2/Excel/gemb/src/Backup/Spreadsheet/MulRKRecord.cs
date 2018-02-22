namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;

    internal class MulRKRecord : XLSRecord
    {
        private static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("MulRK");

        public MulRKRecord(object[] args) : base(staticDescriptor)
        {
            base.InitializeBody(args);
        }

        public MulRKRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord, IoOperationInfo operationInfo) : base(staticDescriptor, bodyLength, br)
        {
        }

        protected override int GetVariableArraySize(object[] loadedArgs, object[] varArr, int bodySize)
        {
            return ((bodySize - 6) / 6);
        }
    }
}

