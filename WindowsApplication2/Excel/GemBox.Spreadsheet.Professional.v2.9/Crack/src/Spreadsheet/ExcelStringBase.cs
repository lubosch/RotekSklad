namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;
    using System.Text;

    internal class ExcelStringBase : BinaryWritable
    {
        public ExcelStringOptions Options;
        public string Str;

        public ExcelStringBase()
        {
        }

        public ExcelStringBase(string str)
        {
            this.Str = str;
            this.Options = ExcelStringOptions.Uncompressed;
        }

        public override bool Equals(object obj)
        {
            ExcelLongString str = (ExcelLongString) obj;
            return ((this.Str == str.Str) && (this.Options == str.Options));
        }

        public string GetFormattedStr()
        {
            StringBuilder builder = new StringBuilder(this.Str);
            for (int i = 0; i < builder.Length; i++)
            {
                char c = builder[i];
                if (!char.IsLetterOrDigit(c) && (c != ' '))
                {
                    builder[i] = 'X';
                }
            }
            return builder.ToString();
        }

        public override int GetHashCode()
        {
            return (this.Str.GetHashCode() ^ this.Options.GetHashCode());
        }

        protected void ReadOptionsAndString(BinaryReader br, ref int remainingSize, ref int charsRemaining)
        {
            ushort richTextRuns = 0;
            uint asianPhonetic = 0;
            this.ReadOptionsAndString(br, ref remainingSize, ref charsRemaining, ref richTextRuns, ref asianPhonetic);
        }

        protected void ReadOptionsAndString(BinaryReader br, ref int remainingSize, ref int charsRemaining, ref ushort richTextRuns, ref uint asianPhonetic)
        {
            int num;
            if ((charsRemaining > 0) || ((richTextRuns == 0) && (asianPhonetic == 0)))
            {
                this.Options = (ExcelStringOptions) br.ReadByte();
                remainingSize--;
            }
            if ((richTextRuns == 0) && (((byte) (this.Options & ExcelStringOptions.RichText)) != 0))
            {
                richTextRuns = br.ReadUInt16();
                remainingSize -= 2;
            }
            if ((asianPhonetic == 0) && (((byte) (this.Options & ExcelStringOptions.AsianPhonetic)) != 0))
            {
                asianPhonetic = br.ReadUInt32();
                remainingSize -= 4;
            }
            char[] chArray = new char[charsRemaining];
            if (((byte) (this.Options & ExcelStringOptions.Uncompressed)) != 0)
            {
                for (num = 0; (charsRemaining > 0) && (remainingSize > 0); num++)
                {
                    chArray[num] = br.ReadChar();
                    remainingSize -= 2;
                    charsRemaining--;
                }
            }
            else
            {
                for (num = 0; (charsRemaining > 0) && (remainingSize > 0); num++)
                {
                    chArray[num] = (char) br.ReadByte();
                    remainingSize--;
                    charsRemaining--;
                }
            }
            this.Str = new string(chArray, 0, chArray.Length - charsRemaining);
            while ((richTextRuns > 0) && (remainingSize > 0))
            {
                br.ReadUInt32();
                remainingSize -= 4;
                richTextRuns = (ushort) (richTextRuns - 1);
            }
            while ((asianPhonetic > 0) && (remainingSize > 0))
            {
                br.ReadByte();
                remainingSize--;
                asianPhonetic--;
            }
        }

        public override void Write(BinaryWriter bw)
        {
            bw.Write((byte) this.Options);
            if (((byte) (this.Options & ExcelStringOptions.Uncompressed)) != 0)
            {
                foreach (char ch in this.Str)
                {
                    bw.Write(ch);
                }
            }
            else
            {
                foreach (char ch2 in this.Str)
                {
                    bw.Write((byte) ch2);
                }
            }
        }

        public override int Size
        {
            get
            {
                int num = 1;
                if (((byte) (this.Options & ExcelStringOptions.Uncompressed)) != 0)
                {
                    return (num + (this.Str.Length * 2));
                }
                return (num + this.Str.Length);
            }
        }
    }
}

