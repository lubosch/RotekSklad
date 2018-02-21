namespace GemBox.Spreadsheet
{
    using System;

    [Flags]
    internal enum DefaultRowHeightOptions : ushort
    {
        AllZeroHeight = 2,
        SpaceAbove = 4,
        SpaceBelow = 8,
        Unsynced = 1
    }
}

