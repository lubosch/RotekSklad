namespace GemBox.Spreadsheet
{
    using System;

    [Flags]
    internal enum FormulaOptions : ushort
    {
        All = 11,
        CalculateOnLoad = 2,
        RecalculateAlways = 1,
        SharedFormula = 8
    }
}

