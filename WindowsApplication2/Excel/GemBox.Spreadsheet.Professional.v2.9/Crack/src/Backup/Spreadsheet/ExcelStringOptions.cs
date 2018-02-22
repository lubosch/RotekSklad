namespace GemBox.Spreadsheet
{
    using System;

    [Flags]
    internal enum ExcelStringOptions : byte
    {
        AsianPhonetic = 4,
        RichText = 8,
        Uncompressed = 1
    }
}

