namespace GemBox.Spreadsheet
{
    using System;
    using System.Drawing;

    internal class FillPatternData
    {
        public Color PatternBackgroundColor;
        public int PatternBackgroundColorIndex;
        public Color PatternForegroundColor;
        public int PatternForegroundColorIndex;
        public FillPatternStyle PatternStyle;

        public FillPatternData()
        {
            this.PatternForegroundColorIndex = -1;
            this.PatternBackgroundColorIndex = -1;
            this.PatternStyle = FillPatternStyle.None;
            this.PatternForegroundColor = Color.Black;
            this.PatternBackgroundColor = Color.White;
        }

        public FillPatternData(FillPatternStyle patternStyle, Color patternForegroundColor, Color patternBackgroundColor)
        {
            this.PatternForegroundColorIndex = -1;
            this.PatternBackgroundColorIndex = -1;
            this.PatternStyle = patternStyle;
            this.PatternForegroundColor = patternForegroundColor;
            this.PatternBackgroundColor = patternBackgroundColor;
        }

        public override bool Equals(object obj)
        {
            FillPatternData data = (FillPatternData) obj;
            if (data.PatternStyle != this.PatternStyle)
            {
                return false;
            }
            if (this.PatternStyle != FillPatternStyle.None)
            {
                if (data.PatternForegroundColor.ToArgb() != this.PatternForegroundColor.ToArgb())
                {
                    return false;
                }
                if (this.PatternStyle != FillPatternStyle.Solid)
                {
                    return (data.PatternBackgroundColor.ToArgb() == this.PatternBackgroundColor.ToArgb());
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            int num = 0;
            num ^= this.PatternStyle.GetHashCode();
            if (this.PatternStyle != FillPatternStyle.None)
            {
                num ^= this.PatternForegroundColor.GetHashCode();
                if (this.PatternStyle != FillPatternStyle.Solid)
                {
                    num ^= this.PatternBackgroundColor.GetHashCode();
                }
            }
            return num;
        }
    }
}

