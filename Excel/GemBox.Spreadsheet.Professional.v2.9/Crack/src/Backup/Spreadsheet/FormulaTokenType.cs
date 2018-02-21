namespace GemBox.Spreadsheet
{
    using System;

    /// <summary>
    /// The set of predefined formula token types
    /// </summary>
    internal enum FormulaTokenType
    {
        Empty,
        Binary,
        Unary,
        Operand,
        Function,
        Control
    }
}

