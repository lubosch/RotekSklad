namespace GemBox.Spreadsheet
{
    using System;

    [Flags]
    internal enum RowOptions : ushort
    {
        Collapsed = 0x10,
        Default = 0,
        GhostDirty = 0x80,
        Hidden = 0x20,
        Unsynced = 0x40
    }
}

