namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;

    internal class ExcelShortString : ExcelStringBase
    {
        public ExcelShortString(BinaryReader br)
        {
            int remainingSize = 0x2020;
            int charsRemaining = br.ReadByte();
            base.ReadOptionsAndString(br, ref remainingSize, ref charsRemaining);
        }

        public ExcelShortString(string str) : base(str)
        {
        }

        public override string ToString()
        {
            return ("ExcelShortString(" + base.GetFormattedStr() + ")");
        }

        public override void Write(BinaryWriter bw)
        {
            bw.Write((byte) base.Str.Length);
            base.Write(bw);
        }

        public override int Size
        {
            get
            {
                return (1 + base.Size);
            }
        }
    }
}

