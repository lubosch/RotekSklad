namespace GemBox.Spreadsheet
{
    using System;

    [Flags]
    internal enum ColumnInfoOptions : ushort
    {
        Collapsed = 0x1000,
        Hidden = 1
    }
}

