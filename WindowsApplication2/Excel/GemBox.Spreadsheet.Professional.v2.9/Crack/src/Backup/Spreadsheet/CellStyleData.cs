namespace GemBox.Spreadsheet
{
    using System;
    using System.Drawing;

    internal class CellStyleData : HashtableElement
    {
        public Color[] BorderColor;
        public LineStyle[] BorderStyle;
        public MultipleBorders BordersUsed;
        public ExcelFontData FontData;
        public bool FormulaHidden;
        public HorizontalAlignmentStyle HorizontalAlignment;
        public int Indent;
        public CellStyleDataIndexes Indexes;
        public bool Locked;
        public string NumberFormat;
        public Color PatternBackgroundColor;
        public Color PatternForegroundColor;
        public FillPatternStyle PatternStyle;
        public int Rotation;
        public bool ShrinkToFit;
        public VerticalAlignmentStyle VerticalAlignment;
        public bool WrapText;

        public CellStyleData(CellStyleCachedCollection parentCollection, bool isDefault, string fontName, int fontSize) : base(parentCollection, isDefault)
        {
            this.VerticalAlignment = VerticalAlignmentStyle.Bottom;
            this.PatternBackgroundColor = Color.White;
            this.PatternForegroundColor = Color.Black;
            this.Locked = true;
            this.NumberFormat = string.Empty;
            this.BorderColor = new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, Color.Black };
            this.BorderStyle = new LineStyle[5];
            this.FontData = new ExcelFontData(fontName, fontSize);
        }

        public static bool AreBordersEqual(Color[] border1Color, LineStyle[] border1Style, MultipleBorders borders1Used, Color[] border2Color, LineStyle[] border2Style, MultipleBorders borders2Used)
        {
            if (borders1Used != borders2Used)
            {
                return false;
            }
            if (borders1Used != MultipleBorders.None)
            {
                for (int i = 0; i < 4; i++)
                {
                    if ((borders1Used & CellBorder.MultipleFromIndividualBorder((IndividualBorder) i)) != MultipleBorders.None)
                    {
                        if (border1Color[i].ToArgb() != border2Color[i].ToArgb())
                        {
                            return false;
                        }
                        if (border1Style[i] != border2Style[i])
                        {
                            return false;
                        }
                    }
                }
                if ((borders1Used & MultipleBorders.Diagonal) != MultipleBorders.None)
                {
                    if (border1Color[4].ToArgb() != border2Color[4].ToArgb())
                    {
                        return false;
                    }
                    if (border1Style[4] != border2Style[4])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override HashtableElement Clone(WeakHashtable parentCollection)
        {
            CellStyleData data = new CellStyleData((CellStyleCachedCollection) parentCollection, false, null, -1);
            data.HorizontalAlignment = this.HorizontalAlignment;
            data.VerticalAlignment = this.VerticalAlignment;
            data.PatternStyle = this.PatternStyle;
            data.PatternBackgroundColor = this.PatternBackgroundColor;
            data.PatternForegroundColor = this.PatternForegroundColor;
            data.Indent = this.Indent;
            data.Rotation = this.Rotation;
            data.Locked = this.Locked;
            data.FormulaHidden = this.FormulaHidden;
            data.WrapText = this.WrapText;
            data.ShrinkToFit = this.ShrinkToFit;
            data.NumberFormat = this.NumberFormat;
            data.FontData = new ExcelFontData(this.FontData);
            data.BorderColor = (Color[]) this.BorderColor.Clone();
            data.BorderStyle = (LineStyle[]) this.BorderStyle.Clone();
            data.BordersUsed = this.BordersUsed;
            return data;
        }

        public override bool Equals(object obj)
        {
            CellStyleData data = (CellStyleData) obj;
            if (((((data.HorizontalAlignment != this.HorizontalAlignment) || (data.VerticalAlignment != this.VerticalAlignment)) || ((data.PatternStyle != this.PatternStyle) || (data.PatternBackgroundColor.ToArgb() != this.PatternBackgroundColor.ToArgb()))) || (((data.PatternForegroundColor.ToArgb() != this.PatternForegroundColor.ToArgb()) || (data.Indent != this.Indent)) || ((data.Rotation != this.Rotation) || (data.Locked != this.Locked)))) || (((data.FormulaHidden != this.FormulaHidden) || (data.WrapText != this.WrapText)) || ((data.ShrinkToFit != this.ShrinkToFit) || (data.NumberFormat != this.NumberFormat))))
            {
                return false;
            }
            if (!data.FontData.Equals(this.FontData))
            {
                return false;
            }
            return AreBordersEqual(data.BorderColor, data.BorderStyle, data.BordersUsed, this.BorderColor, this.BorderStyle, this.BordersUsed);
        }

        public static int GetBordersHashCode(Color[] borderColor, LineStyle[] borderStyle, MultipleBorders bordersUsed)
        {
            int val = 0;
            for (int i = 0; i < 4; i++)
            {
                if ((bordersUsed & CellBorder.MultipleFromIndividualBorder((IndividualBorder) i)) != MultipleBorders.None)
                {
                    val ^= borderStyle[i].GetHashCode();
                    val ^= borderColor[i].GetHashCode();
                    val = Utilities.RotateLeft(val, 5);
                }
            }
            if ((bordersUsed & MultipleBorders.Diagonal) != MultipleBorders.None)
            {
                val ^= borderStyle[4].GetHashCode();
                val ^= borderColor[4].GetHashCode();
            }
            return val;
        }

        public override int GetHashCode()
        {
            int val = 0;
            val ^= this.HorizontalAlignment.GetHashCode();
            val ^= this.VerticalAlignment.GetHashCode();
            val ^= this.PatternStyle.GetHashCode();
            val ^= this.PatternBackgroundColor.GetHashCode();
            val ^= this.PatternForegroundColor.GetHashCode();
            val = Utilities.RotateLeft(val, 8) ^ this.Indent;
            val ^= this.Rotation;
            val ^= this.Locked.GetHashCode();
            val ^= this.FormulaHidden.GetHashCode();
            val ^= this.WrapText.GetHashCode();
            val ^= this.ShrinkToFit.GetHashCode();
            val = Utilities.RotateLeft(val, 8) ^ this.NumberFormat.GetHashCode();
            val ^= this.FontData.GetHashCode();
            val ^= this.BordersUsed.GetHashCode();
            return (val ^ GetBordersHashCode(this.BorderColor, this.BorderStyle, this.BordersUsed));
        }

        [Flags]
        public enum Properties
        {
            All = 0xfffff,
            FontColor = 0x2000,
            FontItalic = 0x10000,
            FontName = 0x1000,
            FontRelated = 0xff000,
            FontScriptPosition = 0x40000,
            FontSize = 0x8000,
            FontStrikeout = 0x20000,
            FontUnderlineStyle = 0x80000,
            FontWeight = 0x4000,
            FormulaHidden = 0x100,
            HorizontalAlignment = 1,
            Indent = 0x20,
            Locked = 0x80,
            None = 0,
            NumberFormat = 0x800,
            PatternBackgroundColor = 8,
            PatternForegroundColor = 0x10,
            PatternRelated = 0x1c,
            PatternStyle = 4,
            Rotation = 0x40,
            ShrinkToFit = 0x400,
            VerticalAlignment = 2,
            WrapText = 0x200
        }
    }
}

