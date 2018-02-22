namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;

    /// <summary>
    /// Formula token for holding binary operator.
    /// </summary>
    internal class BinaryOperatorFormulaToken : FormulaToken
    {
        public static readonly ArrayList BinaryOperatorsList = new ArrayList();
        public static readonly Hashtable CodesToStrings = new Hashtable();
        public static readonly Hashtable StringsToCodes = new Hashtable();

        /// <summary>
        /// Initializes the <see cref="T:GemBox.Spreadsheet.BinaryOperatorFormulaToken" /> class.
        /// </summary>
        static BinaryOperatorFormulaToken()
        {
            CodesToStrings[FormulaTokenCode.Add] = "+";
            CodesToStrings[FormulaTokenCode.Sub] = "-";
            CodesToStrings[FormulaTokenCode.Mul] = "*";
            CodesToStrings[FormulaTokenCode.Div] = "/";
            CodesToStrings[FormulaTokenCode.Power] = "^";
            CodesToStrings[FormulaTokenCode.Concat] = "&";
            CodesToStrings[FormulaTokenCode.Lt] = "<";
            CodesToStrings[FormulaTokenCode.Le] = "<=";
            CodesToStrings[FormulaTokenCode.Eq] = "=";
            CodesToStrings[FormulaTokenCode.Ge] = ">=";
            CodesToStrings[FormulaTokenCode.Gt] = ">";
            CodesToStrings[FormulaTokenCode.Ne] = "<>";
            CodesToStrings[FormulaTokenCode.Isect] = " ";
            CodesToStrings[FormulaTokenCode.List] = ",";
            CodesToStrings[FormulaTokenCode.Range] = ":";
            StringsToCodes["+"] = FormulaTokenCode.Add;
            StringsToCodes["-"] = FormulaTokenCode.Sub;
            StringsToCodes["*"] = FormulaTokenCode.Mul;
            StringsToCodes["/"] = FormulaTokenCode.Div;
            StringsToCodes["^"] = FormulaTokenCode.Power;
            StringsToCodes["&"] = FormulaTokenCode.Concat;
            StringsToCodes["<"] = FormulaTokenCode.Lt;
            StringsToCodes["<="] = FormulaTokenCode.Le;
            StringsToCodes["="] = FormulaTokenCode.Eq;
            StringsToCodes[">="] = FormulaTokenCode.Ge;
            StringsToCodes[">"] = FormulaTokenCode.Gt;
            StringsToCodes["<>"] = FormulaTokenCode.Ne;
            StringsToCodes[" "] = FormulaTokenCode.Isect;
            StringsToCodes[","] = FormulaTokenCode.List;
            StringsToCodes[":"] = FormulaTokenCode.Range;
            BinaryOperatorsList.AddRange(new string[] { "+", "-", "*", "/", "^", "&", "<", "<=", "=", ">", ">=", "<>", " ", ",", ":" });
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GemBox.Spreadsheet.BinaryOperatorFormulaToken" /> class.
        /// </summary>
        /// <param name="code">The FormulaTokenCode code.</param>
        public BinaryOperatorFormulaToken(FormulaTokenCode code) : base(code, 1, FormulaTokenType.Binary)
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

