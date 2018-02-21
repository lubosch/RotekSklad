namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;
    using System.Text;

    internal abstract class AbsXLSRec : BinaryWritable
    {
        public int Address;
        public const int HeaderSize = 4;
        public const int MaxBodySize = 0x2020;
        public const int MaxSize = 0x2024;

        protected AbsXLSRec()
        {
        }

        public string GetXMLRecord(int index)
        {
            StringBuilder builder = new StringBuilder();
            string name = this.Name;
            switch (name)
            {
                case "Font":
                case "Format":
                case "XF":
                case "Style":
                {
                    if ((name == "Font") && (index > 3))
                    {
                        index++;
                    }
                    if (name == "Format")
                    {
                        index += 0xa4;
                    }
                    object obj2 = name;
                    name = string.Concat(new object[] { obj2, "(", index, ")" });
                    break;
                }
            }
            builder.AppendFormat("<Record Name=\"{0}\" Code=\"0x{1:X4}\" FormattedBody=\"{2}\" BodySize=\"{3}\" Body=\"{4}\"/>", new object[] { name, this.RecordCode, this.FormattedBody, this.BodySize, Utilities.ByteArr2HexStr(this.Body) });
            return builder.ToString();
        }

        public static AbsXLSRec Read(BinaryReader br, AbsXLSRec previousRecord, IoOperationInfo operationInfo)
        {
            int code = br.ReadUInt16();
            if (code == 0)
            {
                return null;
            }
            int size = br.ReadUInt16();
            XLSDescriptor byCode = XLSDescriptors.GetByCode(code);
            if (!XLSDescriptors.ValidBodySize(byCode, size, false))
            {
                byCode = null;
            }
            if ((byCode != null) && (byCode.Name == "FILEPASS"))
            {
                throw new Exception("Current version of GemBox.Spreadsheet can't read encrypted workbooks. You can use only simple password protection against modifying (set in MS Excel 'Save As' dialog).");
            }
            if ((byCode != null) && (byCode.HandlerClass != "XLSRecord"))
            {
                return XLSDescriptors.StaticCreateInstance(byCode.HandlerClass, size, br, previousRecord, operationInfo);
            }
            return new XLSRecord(code, size, br);
        }

        public override void Write(BinaryWriter bw)
        {
            bw.Write((ushort) this.RecordCode);
            ushort bodySize = (ushort) this.BodySize;
            if (bodySize > 0x2020)
            {
                bw.Write((ushort) 0x2020);
            }
            else
            {
                bw.Write(bodySize);
            }
            this.WriteBody(bw);
        }

        protected abstract void WriteBody(BinaryWriter bw);

        protected internal virtual byte[] Body
        {
            get
            {
                byte[] buffer = new byte[this.Size];
                using (MemoryStream stream = new MemoryStream(buffer))
                {
                    using (BinaryWriter writer = new BinaryWriter(stream, new UnicodeEncoding()))
                    {
                        this.WriteBody(writer);
                        return buffer;
                    }
                }
            }
        }

        protected abstract int BodySize { get; }

        public abstract string FormattedBody { get; }

        public abstract string Name { get; }

        internal abstract int RecordCode { get; }

        public override int Size
        {
            get
            {
                return (4 + this.BodySize);
            }
        }
    }
}

