namespace GemBox.Spreadsheet
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Contains font related settings.
    /// </summary>
    public class ExcelFont
    {
        /// <summary>
        /// Default bold font weight.
        /// </summary>
        /// <seealso cref="P:GemBox.Spreadsheet.ExcelFont.Weight" />
        public const int BoldWeight = 700;
        /// <summary>
        /// Maximum font weight.
        /// </summary>
        /// <seealso cref="P:GemBox.Spreadsheet.ExcelFont.Weight" />
        public const int MaxWeight = 0x3e8;
        /// <summary>
        /// Minimum font weight.
        /// </summary>
        /// <seealso cref="P:GemBox.Spreadsheet.ExcelFont.Weight" />
        public const int MinWeight = 100;
        /// <summary>
        /// Normal font weight.
        /// </summary>
        /// <seealso cref="P:GemBox.Spreadsheet.ExcelFont.Weight" />
        public const int NormalWeight = 400;
        private CellStyle parent;

        internal ExcelFont(CellStyle parent)
        {
            this.parent = parent;
        }

        internal void CopyTo(CellStyle destination)
        {
            destination.Element.FontData = new ExcelFontData(this.parent.Element.FontData);
            destination.UseFlags &= ~CellStyleData.Properties.FontRelated;
            destination.UseFlags |= this.parent.UseFlags & CellStyleData.Properties.FontRelated;
        }

        /// <summary>
        /// Gets or sets font color.
        /// </summary>
        /// <remarks>
        /// Default value for this property is <see cref="P:System.Drawing.Color.Black">Color.Black</see>.
        /// </remarks>
        public System.Drawing.Color Color
        {
            get
            {
                return this.parent.Element.FontData.Color;
            }
            set
            {
                this.parent.BeforeChange();
                this.parent.Element.FontData.Color = value;
                this.parent.UseFlags |= CellStyleData.Properties.FontColor;
            }
        }

        /// <summary>
        /// Gets or sets if the font is italic.
        /// </summary>
        /// <remarks>
        /// Default value of this property is <b>false</b>.
        /// </remarks>
        public bool Italic
        {
            get
            {
                return this.parent.Element.FontData.Italic;
            }
            set
            {
                this.parent.BeforeChange();
                this.parent.Element.FontData.Italic = value;
                this.parent.UseFlags |= CellStyleData.Properties.FontItalic;
            }
        }

        /// <summary>
        /// Gets or sets name of the font.
        /// </summary>
        /// <remarks>
        /// Default value for this property is determined by
        /// <see cref="P:GemBox.Spreadsheet.ExcelFile.DefaultFontName">
        /// GemBox.Spreadsheet.ExcelFile.DefaultFontName</see>.
        /// </remarks>
        public string Name
        {
            get
            {
                return this.parent.Element.FontData.Name;
            }
            set
            {
                this.parent.BeforeChange();
                this.parent.Element.FontData.Name = value;
                this.parent.UseFlags |= CellStyleData.Properties.FontName;
            }
        }

        /// <summary>
        /// Gets or sets font script position.
        /// </summary>
        /// <remarks>
        /// Default value of this property is <see cref="F:GemBox.Spreadsheet.ScriptPosition.Normal">ScriptPosition.Normal</see>.
        /// </remarks>
        public GemBox.Spreadsheet.ScriptPosition ScriptPosition
        {
            get
            {
                return this.parent.Element.FontData.ScriptPosition;
            }
            set
            {
                this.parent.BeforeChange();
                this.parent.Element.FontData.ScriptPosition = value;
                this.parent.UseFlags |= CellStyleData.Properties.FontScriptPosition;
            }
        }

        /// <summary>
        /// Gets or sets font size.
        /// </summary>
        /// <remarks>
        /// <p>Unit is twip (1/20th of a point).</p>
        /// <p>Default value of this property is determined by
        /// <see cref="P:GemBox.Spreadsheet.ExcelFile.DefaultFontSize">
        /// GemBox.Spreadsheet.ExcelFile.DefaultFontSize</see></p>
        /// </remarks>
        public int Size
        {
            get
            {
                return this.parent.Element.FontData.Size;
            }
            set
            {
                this.parent.BeforeChange();
                this.parent.Element.FontData.Size = value;
                this.parent.UseFlags |= CellStyleData.Properties.FontSize;
            }
        }

        /// <summary>
        /// Gets or sets if the font is struck out.
        /// </summary>
        /// <remarks>
        /// Default value of this property is <b>false</b>.
        /// </remarks>
        public bool Strikeout
        {
            get
            {
                return this.parent.Element.FontData.Strikeout;
            }
            set
            {
                this.parent.BeforeChange();
                this.parent.Element.FontData.Strikeout = value;
                this.parent.UseFlags |= CellStyleData.Properties.FontStrikeout;
            }
        }

        /// <summary>
        /// Gets or sets font underlining.
        /// </summary>
        /// <remarks>
        /// Default value of this property is <see cref="F:GemBox.Spreadsheet.UnderlineStyle.None">UnderlineStyle.None</see>.
        /// </remarks>
        public GemBox.Spreadsheet.UnderlineStyle UnderlineStyle
        {
            get
            {
                return this.parent.Element.FontData.UnderlineStyle;
            }
            set
            {
                this.parent.BeforeChange();
                this.parent.Element.FontData.UnderlineStyle = value;
                this.parent.UseFlags |= CellStyleData.Properties.FontUnderlineStyle;
            }
        }

        /// <summary>
        /// Gets or sets font weight (font boldness).
        /// </summary>
        /// <remarks>
        /// <p>Font weight is an integer value between <see cref="F:GemBox.Spreadsheet.ExcelFont.MinWeight">
        /// MinWeight</see> and <see cref="F:GemBox.Spreadsheet.ExcelFont.MaxWeight">MaxWeight</see>.</p>
        /// <p>If you want font to have standard boldness, set this property to
        /// <see cref="F:GemBox.Spreadsheet.ExcelFont.BoldWeight">BoldWeight</see>.</p>
        /// <p>Default value of this property is <see cref="F:GemBox.Spreadsheet.ExcelFont.NormalWeight">NormalWeight</see>.</p>
        /// </remarks>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Thrown if font weight is out of allowed range.</exception>
        public int Weight
        {
            get
            {
                return this.parent.Element.FontData.Weight;
            }
            set
            {
                if ((value < 100) || (value > 0x3e8))
                {
                    throw new ArgumentOutOfRangeException("value", value, "Font weight is out of allowed range.");
                }
                this.parent.BeforeChange();
                this.parent.Element.FontData.Weight = value;
                this.parent.UseFlags |= CellStyleData.Properties.FontWeight;
            }
        }
    }
}

