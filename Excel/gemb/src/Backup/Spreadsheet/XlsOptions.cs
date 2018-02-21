namespace GemBox.Spreadsheet
{
    using System;

    /// <summary>
    /// Options specified when reading XLS files.
    /// </summary>
    [Flags]
    public enum XlsOptions
    {
        /// <summary>
        /// Do not preserve records. Only records fully supported by GemBox.Spreadsheet API will be loaded.
        /// </summary>
        None = 0,
        /// <summary>
        /// Preserve all possible information.
        /// </summary>
        PreserveAll = 15,
        /// <summary>
        /// Preserve global (workbook) records.
        /// </summary>
        PreserveGlobalRecords = 1,
        /// <summary>
        /// Preserve macros and VBA code.
        /// </summary>
        PreserveMacros = 8,
        /// <summary>
        /// Preserve summaries.
        /// </summary>
        PreserveSummaries = 4,
        /// <summary>
        /// Preserve worksheet records.
        /// </summary>
        PreserveWorksheetRecords = 2
    }
}

