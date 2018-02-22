namespace GemBox.Spreadsheet
{
    using System;

    [Flags]
    internal enum SetupOptions : ushort
    {
        DraftQuality = 0x10,
        PaperOrientationInvalid = 0x40,
        Portrait = 2,
        PrintBlackWhite = 8,
        PrintCellNotes = 0x20,
        PrintErrorOptions = 0xc00,
        PrintNotesSheetEnd = 0x200,
        PrintPagesInRows = 1,
        SomeNotInitialised = 4,
        UseStartPageNumber = 0x80
    }
}

