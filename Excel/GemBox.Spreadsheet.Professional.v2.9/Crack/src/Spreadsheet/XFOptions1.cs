namespace GemBox.Spreadsheet
{
    using System;

    [Flags]
    internal enum XFOptions1 : ushort
    {
        CellLocked = 1,
        FormulaHidden = 2,
        Prefix123 = 8,
        StyleXF = 4
    }
}

