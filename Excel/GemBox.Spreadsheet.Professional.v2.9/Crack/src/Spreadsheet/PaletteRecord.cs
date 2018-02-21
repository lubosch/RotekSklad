namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;

    internal class PaletteRecord : XLSRecord
    {
        private static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("Palette");

        public PaletteRecord(object[] args) : base(staticDescriptor)
        {
            base.InitializeBody(args);
        }

        public PaletteRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord, IoOperationInfo operationInfo) : base(staticDescriptor, bodyLength, br)
        {
        }

        protected override int GetVariableArraySize(object[] loadedArgs, object[] varArr, int bodySize)
        {
            return (ushort) loadedArgs[0];
        }
    }
}

