namespace GemBox.Spreadsheet
{
    using System;

    /// <summary>
    /// Possible operations that can fire notification events.
    /// </summary>
    /// <remarks>
    /// This enumeration is used as event argument in notification events.
    /// </remarks>
    public enum IoOperation
    {
        XlsReading,
        CsvReading,
        XlsWriting,
        CsvWriting,
        XlsxReading,
        XlsxWriting
    }
}

