namespace GemBox.Spreadsheet
{
    using System;

    /// <summary>
    /// Formula token for holding 3d reference error.
    /// </summary>
    internal class AreaErr3dFormulaToken : Area3dFormulaToken
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:GemBox.Spreadsheet.AreaErr3dFormulaToken" /> class.
        /// </summary>
        /// <param name="code">The FormulaTokenCode code.</param>
        public AreaErr3dFormulaToken(FormulaTokenCode code) : base(code)
        {
        }

        /// <summary>
        /// Convert formula token to string representation.
        /// </summary>
        /// <returns>formula token string representation</returns>
        public override string ToString()
        {
            return "#REF";
        }
    }
}

