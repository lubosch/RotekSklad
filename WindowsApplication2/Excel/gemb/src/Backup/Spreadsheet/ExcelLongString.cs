namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;

    internal class ExcelLongString : ExcelStringBase
    {
        internal int splitSize;

        public ExcelLongString(BinaryReader br)
        {
            int remainingSize = 0x2020;
            int charsRemaining = br.ReadUInt16();
            base.ReadOptionsAndString(br, ref remainingSize, ref charsRemaining);
        }

        public ExcelLongString(string str) : base(str)
        {
        }

        public ExcelLongString(string str, int splitSize) : base(str)
        {
            this.splitSize = splitSize;
        }

        public ExcelLongString(BinaryReader br, ref int remainingSize, ref int charsRemaining, ref ushort richTextRunsRemaining, ref uint asianPhoneticRemaining)
        {
            if ((((long) (charsRemaining + richTextRunsRemaining)) + ((long) asianPhoneticRemaining)) == 0L)
            {
                charsRemaining = br.ReadUInt16();
                remainingSize -= 2;
            }
            base.ReadOptionsAndString(br, ref remainingSize, ref charsRemaining, ref richTextRunsRemaining, ref asianPhoneticRemaining);
        }

        public override string ToString()
        {
            return ("ExcelLongString(" + base.GetFormattedStr() + ")");
        }

        public override void Write(BinaryWriter bw)
        {
            if (this.splitSize == 0)
            {
                bw.Write((ushort) base.Str.Length);
            }
            else if (this.splitSize > 0)
            {
                bw.Write((ushort) this.splitSize);
            }
            base.Write(bw);
        }

        public override int Size
        {
            get
            {
                if (this.splitSize == -1)
                {
                    return base.Size;
                }
                return (2 + base.Size);
            }
        }
    }
}

