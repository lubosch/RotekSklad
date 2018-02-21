namespace GemBox.Spreadsheet
{
    using System;

    /// <summary>
    /// Different borders that can be set on excel cell. Members of this enumeration can't be combined.
    /// </summary>
    /// <seealso cref="T:GemBox.Spreadsheet.MultipleBorders" />
    public enum IndividualBorder
    {
        Top,
        Bottom,
        Left,
        Right,
        DiagonalUp,
        DiagonalDown
    }
}

