namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;

    /// <summary>
    /// Formula token for holding unary operator.
    /// </summary>
    internal class UnaryOperatorFormulaToken : FormulaToken
    {
        public static readonly Hashtable CodesToStrings = new Hashtable();
        public static readonly ArrayList UnaryOperatorsList = new ArrayList();

        /// <summary>
        /// Initializes the <see cref="T:GemBox.Spreadsheet.UnaryOperatorFormulaToken" /> class.
        /// </summary>
        static UnaryOperatorFormulaToken()
        {
            CodesToStrings[FormulaTokenCode.Uplus] = "+";
            CodesToStrings[FormulaTokenCode.Uminus] = "-";
            CodesToStrings[FormulaTokenCode.Percent] = "%";
            CodesToStrings[FormulaTokenCode.Parentheses] = "(";
            UnaryOperatorsList.AddRange(new char[] { '+', '-', '%', '(', ')' });
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GemBox.Spreadsheet.UnaryOperatorFormulaToken" /> class.
        /// </summary>
        /// <param name="code">The code.</param>
        public UnaryOperatorFormulaToken(FormulaTokenCode code) : base(code, 1, FormulaTokenType.Unary)
        {
        }

        /// <summary>
        /// Convert formula token to string representation.
        /// </summary>
        /// <returns>formula token string representation</returns>
        public override string ToString()
        {
            return (CodesToStrings[(FormulaTokenCode) base.Code] as string);
        }
    }
}

