namespace GemBox.Spreadsheet
{
    using System;
    using System.Drawing;

    internal class ExcelFontData
    {
        public System.Drawing.Color Color;
        public int ColorIndex;
        public bool Italic;
        public string Name;
        public GemBox.Spreadsheet.ScriptPosition ScriptPosition;
        public int Size;
        public bool Strikeout;
        public GemBox.Spreadsheet.UnderlineStyle UnderlineStyle;
        public int Weight;

        public ExcelFontData(ExcelFontData source)
        {
            this.ColorIndex = -1;
            this.Size = -1;
            this.Color = System.Drawing.Color.Black;
            this.Weight = 400;
            this.Name = source.Name;
            this.Color = source.Color;
            this.Weight = source.Weight;
            this.Size = source.Size;
            this.Italic = source.Italic;
            this.Strikeout = source.Strikeout;
            this.ScriptPosition = source.ScriptPosition;
            this.UnderlineStyle = source.UnderlineStyle;
        }

        public ExcelFontData(string fontName, int fontSize)
        {
            this.ColorIndex = -1;
            this.Size = -1;
            this.Color = System.Drawing.Color.Black;
            this.Weight = 400;
            this.Name = fontName;
            this.Size = fontSize;
        }

        public override bool Equals(object obj)
        {
            ExcelFontData data = (ExcelFontData) obj;
            return (((!(data.Name != this.Name) && (data.Color.ToArgb() == this.Color.ToArgb())) && ((data.Weight == this.Weight) && (data.Size == this.Size))) && (((data.Italic == this.Italic) && (data.Strikeout == this.Strikeout)) && ((data.ScriptPosition == this.ScriptPosition) && (data.UnderlineStyle == this.UnderlineStyle))));
        }

        public override int GetHashCode()
        {
            int val = 0;
            val ^= this.Name.GetHashCode();
            val ^= this.Color.GetHashCode();
            val ^= this.Weight;
            val ^= this.Size;
            val ^= this.Italic.GetHashCode();
            val = Utilities.RotateLeft(val, 6) ^ this.Strikeout.GetHashCode();
            val ^= this.ScriptPosition.GetHashCode();
            return (val ^ this.UnderlineStyle.GetHashCode());
        }
    }
}

