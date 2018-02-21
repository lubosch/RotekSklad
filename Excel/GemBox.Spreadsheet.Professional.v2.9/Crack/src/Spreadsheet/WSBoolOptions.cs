namespace GemBox.Spreadsheet
{
    using System;

    [Flags]
    internal enum WSBoolOptions : ushort
    {
        AlternateExpEval = 0x4000,
        AlternateForEntry = 0x8000,
        ApplyStyles = 0x20,
        ColGroupRight = 0x80,
        Dialog = 0x10,
        FitToPage = 0x100,
        RowGroupBelow = 0x40,
        ShowAutoBreaks = 1,
        ShowColumnOutline = 0x800,
        ShowRowOutline = 0x400
    }
}

