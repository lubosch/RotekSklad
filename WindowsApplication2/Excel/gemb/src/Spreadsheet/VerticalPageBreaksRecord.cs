namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;

    internal class VerticalPageBreaksRecord : XLSRecord
    {
        private static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("VERTICALPAGEBREAKS");

        public VerticalPageBreaksRecord(object[] args) : base(staticDescriptor)
        {
            base.InitializeBody(args);
        }

        public VerticalPageBreaksRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord, IoOperationInfo operationInfo) : base(staticDescriptor, bodyLength, br)
        {
        }

        protected override int GetVariableArraySize(object[] loadedArgs, object[] varArr, int bodySize)
        {
            return (ushort) loadedArgs[0];
        }
    }
}

