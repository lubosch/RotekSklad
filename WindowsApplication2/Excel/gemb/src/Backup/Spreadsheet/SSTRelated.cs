namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;

    internal class SSTRelated : XLSRecord
    {
        public ExcelLongStrings ExcelStrings;

        public SSTRelated(XLSDescriptor descriptor) : base(descriptor)
        {
        }

        public SSTRelated(XLSDescriptor descriptor, int bodySize, BinaryReader br) : base(descriptor, bodySize, br)
        {
        }
    }
}

