namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Text;

    internal class ExcelLongStrings : BinaryWritable
    {
        public uint AsianPhoneticRemaining;
        public int CharsRemaining;
        public ushort RichTextRunsRemaining;
        public ArrayList Strings;

        public ExcelLongStrings()
        {
            this.Strings = new ArrayList();
        }

        public ExcelLongStrings(BinaryReader br, int remainingSize, ExcelLongStrings previousStrings)
        {
            this.Strings = new ArrayList();
            int charsRemaining = 0;
            ushort richTextRunsRemaining = 0;
            uint asianPhoneticRemaining = 0;
            if (previousStrings != null)
            {
                charsRemaining = previousStrings.CharsRemaining;
                richTextRunsRemaining = previousStrings.RichTextRunsRemaining;
                asianPhoneticRemaining = previousStrings.AsianPhoneticRemaining;
            }
            while (remainingSize != 0)
            {
                ExcelLongString str = new ExcelLongString(br, ref remainingSize, ref charsRemaining, ref richTextRunsRemaining, ref asianPhoneticRemaining);
                this.Strings.Add(str);
            }
            this.CharsRemaining = charsRemaining;
            this.RichTextRunsRemaining = richTextRunsRemaining;
            this.AsianPhoneticRemaining = asianPhoneticRemaining;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder("ExcelLongStrings(");
            for (int i = 0; i < this.Strings.Count; i++)
            {
                ExcelLongString str = (ExcelLongString) this.Strings[i];
                if (i > 0)
                {
                    builder.Append(",");
                }
                builder.Append(str.GetFormattedStr());
            }
            builder.Append(")");
            return builder.ToString();
        }

        public override void Write(BinaryWriter bw)
        {
            foreach (ExcelLongString str in this.Strings)
            {
                str.Write(bw);
            }
        }

        public override int Size
        {
            get
            {
                int num = 0;
                foreach (ExcelLongString str in this.Strings)
                {
                    num += str.Size;
                }
                return num;
            }
        }
    }
}

