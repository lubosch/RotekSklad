namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;
    using System.Text;

    internal class StyleRecord : XLSRecord
    {
        private static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("Style");

        public StyleRecord(string bodyHex) : base(staticDescriptor.Name, bodyHex)
        {
        }

        public StyleRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord, IoOperationInfo operationInfo) : base(staticDescriptor, bodyLength, br)
        {
        }

        public override string FormattedBody
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                using (MemoryStream stream = new MemoryStream(this.Body))
                {
                    using (BinaryReader reader = new BinaryReader(stream, new UnicodeEncoding()))
                    {
                        ushort num4 = reader.ReadUInt16();
                        int num = num4 & 0xfff;
                        bool flag = (num4 & 0x8000) == 0;
                        builder.Append("indexXF:" + num);
                        builder.Append(" userDefined:" + flag);
                        if (flag)
                        {
                            builder.Append(" name:" + new ExcelShortString(reader).Str);
                        }
                        else
                        {
                            int num2 = reader.ReadByte();
                            int num3 = reader.ReadByte();
                            builder.Append(" builtInID:" + num2);
                            builder.Append(" outlineLevel:" + num3);
                        }
                    }
                }
                return builder.ToString();
            }
        }
    }
}

