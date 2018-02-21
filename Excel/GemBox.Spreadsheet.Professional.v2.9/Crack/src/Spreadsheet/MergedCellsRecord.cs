namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;

    internal class MergedCellsRecord : XLSRecord
    {
        private static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("MergedCells");

        public MergedCellsRecord(object[] args) : base(staticDescriptor)
        {
            base.InitializeDelayed(args);
        }

        public MergedCellsRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord, IoOperationInfo operationInfo) : base(staticDescriptor, bodyLength, br)
        {
        }

        protected override int GetVariableArraySize(object[] loadedArgs, object[] varArr, int bodySize)
        {
            return (ushort) loadedArgs[0];
        }
    }
}

