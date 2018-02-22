namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;

    internal class HorizontalPageBreaksRecord : XLSRecord
    {
        private static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("HORIZONTALPAGEBREAKS");

        public HorizontalPageBreaksRecord(object[] args) : base(staticDescriptor)
        {
            base.InitializeBody(args);
        }

        public HorizontalPageBreaksRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord, IoOperationInfo operationInfo) : base(staticDescriptor, bodyLength, br)
        {
        }

        protected override int GetVariableArraySize(object[] loadedArgs, object[] varArr, int bodySize)
        {
            return (ushort) loadedArgs[0];
        }
    }
}

