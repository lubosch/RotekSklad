namespace GemBox.Spreadsheet
{
    using System;

    /// <summary>
    /// Underline styles used in <see cref="P:GemBox.Spreadsheet.ExcelFont.UnderlineStyle">ExcelFont.UnderlineStyle</see>.
    /// </summary>
    public enum UnderlineStyle
    {
        /// <summary>
        /// Double underline. Underlines only cell data.
        /// </summary>
        Double = 2,
        /// <summary>
        /// Double accounting underline. Underlines whole cell.
        /// </summary>
        DoubleAccounting = 0x22,
        /// <summary>
        /// No underline.
        /// </summary>
        None = 0,
        /// <summary>
        /// Single underline. Underlines only cell data.
        /// </summary>
        Single = 1,
        /// <summary>
        /// Single accounting underline. Underlines whole cell.
        /// </summary>
        SingleAccounting = 0x21
    }
}

