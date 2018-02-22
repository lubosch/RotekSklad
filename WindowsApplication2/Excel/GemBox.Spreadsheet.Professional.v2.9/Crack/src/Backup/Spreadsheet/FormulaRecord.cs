namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;

    internal class FormulaRecord : XLSRecord
    {
        private static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("Formula");

        public FormulaRecord(object[] args) : base(staticDescriptor)
        {
            base.InitializeBody(args);
        }

        public FormulaRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord, IoOperationInfo operationInfo) : base(staticDescriptor, bodyLength, br)
        {
        }

        protected override int GetVariableArraySize(object[] loadedArgs, object[] varArr, int bodySize)
        {
            if (loadedArgs[1] != null)
            {
                return (ushort) loadedArgs[3];
            }
            return 8;
        }
    }
}

