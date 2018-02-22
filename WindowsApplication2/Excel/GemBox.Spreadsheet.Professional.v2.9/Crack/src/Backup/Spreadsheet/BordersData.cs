namespace GemBox.Spreadsheet
{
    using System;
    using System.Drawing;

    internal class BordersData
    {
        public Color[] BorderColor;
        public int[] BorderColorIndex;
        public LineStyle[] BorderStyle;
        public MultipleBorders BordersUsed;

        public BordersData()
        {
            this.BorderColorIndex = new int[] { -1, -1, -1, -1, -1 };
            this.BorderColor = new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, Color.Black };
            this.BorderStyle = new LineStyle[5];
            this.BordersUsed = MultipleBorders.None;
        }

        public BordersData(Color[] borderColor, LineStyle[] borderStyle, MultipleBorders bordersUsed)
        {
            this.BorderColorIndex = new int[] { -1, -1, -1, -1, -1 };
            this.BorderColor = borderColor;
            this.BorderStyle = borderStyle;
            this.BordersUsed = bordersUsed;
        }

        public override bool Equals(object obj)
        {
            BordersData data = (BordersData) obj;
            return CellStyleData.AreBordersEqual(data.BorderColor, data.BorderStyle, data.BordersUsed, this.BorderColor, this.BorderStyle, this.BordersUsed);
        }

        public override int GetHashCode()
        {
            return CellStyleData.GetBordersHashCode(this.BorderColor, this.BorderStyle, this.BordersUsed);
        }
    }
}

