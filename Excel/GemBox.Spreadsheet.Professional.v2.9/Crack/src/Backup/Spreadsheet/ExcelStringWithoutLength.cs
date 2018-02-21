namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;

    internal class ExcelStringWithoutLength : ExcelStringBase
    {
        public ExcelStringWithoutLength(string str) : base(str)
        {
        }

        public ExcelStringWithoutLength(BinaryReader br, int charCount)
        {
            int remainingSize = 0x2020;
            base.ReadOptionsAndString(br, ref remainingSize, ref charCount);
        }

        public override string ToString()
        {
            return ("ExcelStringWithoutLength(" + base.GetFormattedStr() + ")");
        }
    }
}

