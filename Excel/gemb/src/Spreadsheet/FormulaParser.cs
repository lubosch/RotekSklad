namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;
    using System.Text;

    internal class FormulaParser
    {
        /// <summary>
        /// List of allowed values for boolean primitive type
        /// </summary>
        private static readonly ArrayList boolList = new ArrayList(new string[] { "TRUE", "FALSE" });
        private GemBox.Spreadsheet.Buffer buffer;
        private bool isFunctionArgumentsProcessed;
        private bool isProcentOperatorProcessed;
        private FormulaFunctionInfo lastFunctionInfo;
        private int lastPos;
        private ArrayList tokens = new ArrayList();
        private ExcelWorksheet worksheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GemBox.Spreadsheet.FormulaParser" /> class.
        /// </summary>
        /// <param name="sheet">The excel worksheet.</param>
        public FormulaParser(ExcelWorksheet sheet)
        {
            this.worksheet = sheet;
        }

        private void AddArea(string value)
        {
            this.AddToken(FormulaTokenCode.Area1, value);
        }

        private void AddBoolToken(string boolValue)
        {
            if (this.GetNextOnDemand('('))
            {
                this.Match(')');
                this.AddToken(FormulaTokensFactory.CreateFunctionFromName(boolValue, FormulaTokenClass.Reference, 0));
            }
            else
            {
                this.AddToken(FormulaTokenCode.Bool, bool.Parse(boolValue));
            }
        }

        private void AddCellOrRangeToken(string cellValue)
        {
            if (this.buffer.Peek() == RefFormulaToken.AbsoluteCellMark)
            {
                cellValue = this.GetCell();
            }
            if (this.GetNextOnDemand(':'))
            {
                string cell = this.GetCell();
                if (RefFormulaToken.IsCell(cell))
                {
                    this.AddArea(cellValue + ":" + cell);
                }
                else
                {
                    this.Expected("Area.");
                }
            }
            else
            {
                FormulaTokenCode code = FormulaTokenCode.Ref2;
                this.AddToken(code, cellValue);
            }
        }

        private void AddErrorToken(string errorValue)
        {
            errorValue = '#' + this.buffer.GetNextString(new char[] { '!', '?' }) + this.buffer.GetNext();
            if (!ErrFormulaToken.ErrorsList.Contains(errorValue.ToUpper()))
            {
                this.Expected("Error");
            }
            this.AddToken(FormulaTokenCode.Err, errorValue);
        }

        private void AddExpressionToken()
        {
            this.Expression();
            this.Match(')');
            this.AddToken(FormulaTokenCode.Parentheses);
        }

        private void AddFloatOrIntegerToken(string value)
        {
            if (value.Length != 0)
            {
                double doubleValue = NumbersParser.StrToDouble(value);
                if (NumbersParser.IsUshort(doubleValue))
                {
                    this.AddToken(FormulaTokenCode.Int, (ushort) doubleValue);
                }
                else
                {
                    this.AddToken(FormulaTokenCode.Num, doubleValue);
                }
            }
        }

        private void AddFunctionToken(string functionValue)
        {
            this.isFunctionArgumentsProcessed = true;
            this.lastFunctionInfo = FormulaFunctionsTable.Instance[functionValue];
            byte argumentsCount = this.ArgumentList();
            this.isFunctionArgumentsProcessed = false;
            this.GetNextOnDemand(')');
            FormulaFunctionInfo info = FormulaFunctionsTable.Instance[functionValue];
            byte num2 = info.ArgumentsCount;
            if (num2 != 0xff)
            {
                string str = (num2 == 1) ? " argument." : " arguments.";
                if (argumentsCount != num2)
                {
                    this.NotifyError(string.Concat(new object[] { "Function: ", FormulaFunctionsTable.Instance[info.Code].Name, " expects ", num2, str }));
                }
            }
            this.AddToken(FormulaTokensFactory.CreateFunctionFromName(functionValue, this.lastFunctionInfo.ReturnCode, argumentsCount));
        }

        private void AdditiveExpression()
        {
            this.MultiplicativeExpression();
            while ((this.buffer.Peek() == '+') || (this.buffer.Peek() == '-'))
            {
                char ch = this.buffer.Peek();
                this.buffer.GetNext();
                this.ResetCounter();
                this.MultiplicativeExpression();
                this.ResetCounter("Operand for binary operator.");
                if (ch == '+')
                {
                    this.AddToken(FormulaTokenCode.Add);
                }
                else
                {
                    this.AddToken(FormulaTokenCode.Sub);
                }
            }
        }

        private void AddNamedRange(string namedRange)
        {
            if (this.isFunctionArgumentsProcessed)
            {
                this.AddToken(FormulaTokenCode.Name1, new object[] { namedRange, this.worksheet });
            }
            else
            {
                this.AddToken(FormulaTokenCode.Name2, new object[] { namedRange, this.worksheet });
            }
        }

        private void AddSheetReferenceToken(string sheet)
        {
            string cell = this.GetCell();
            if (cell == string.Empty)
            {
                this.Expected("3d sheet cell reference.");
            }
            sheet = sheet + "!" + cell;
            if (this.GetNextOnDemand(':'))
            {
                string match = this.GetCell();
                if (RefFormulaToken.IsCell(match))
                {
                    FormulaTokenCode code = this.isFunctionArgumentsProcessed ? FormulaTokenCode.Area3d1 : FormulaTokenCode.Area3d2;
                    this.AddToken(code, new object[] { sheet + ":" + match, this.worksheet.Parent });
                }
                else
                {
                    this.Expected("3d area reference.");
                }
            }
            else
            {
                FormulaTokenCode code2 = this.isFunctionArgumentsProcessed ? FormulaTokenCode.Ref3d1 : FormulaTokenCode.Ref3d2;
                this.AddToken(code2, new object[] { sheet, this.worksheet.Parent });
            }
        }

        private void AddStringToken()
        {
            this.AddToken(FormulaTokenCode.Str, this.buffer.GetNextString('"'));
            this.Match('"');
        }

        /// <summary>
        /// Adds the token to the result list.
        /// </summary>
        /// <param name="token">The token to be added.</param>
        public void AddToken(FormulaToken token)
        {
            this.tokens.Add(token);
        }

        /// <summary>
        /// Adds the token to the result list.
        /// </summary>
        /// <param name="code">The code of the token to be added.</param>
        public void AddToken(FormulaTokenCode code)
        {
            this.tokens.Add(FormulaTokensFactory.CreateFromCode(this.worksheet, code));
        }

        /// <summary>
        /// Adds the token to the result list.
        /// </summary>
        /// <param name="code">The code of the token to be added.</param>
        /// <param name="data">The data to be used as the input for formula token delay initialization.</param>
        public void AddToken(FormulaTokenCode code, object data)
        {
            this.AddToken(code, new object[] { data });
        }

        /// <summary>
        /// Adds the token to the result list.
        /// </summary>
        /// <param name="code">The code of the token to be added.</param>
        /// <param name="data">The array of data to be used as the input for formula token delay initialization.</param>
        public void AddToken(FormulaTokenCode code, object[] data)
        {
            FormulaToken token = FormulaTokensFactory.CreateFromCode(this.worksheet, code);
            this.tokens.Add(token);
            token.DelayInitialize(data);
        }

        private byte ArgumentList()
        {
            byte num = 0;
            bool flag = false;
            do
            {
                flag = false;
                this.buffer.SkipWhitespaces();
                int pos = this.buffer.Pos;
                if (this.GetNextOnDemand(','))
                {
                    this.AddToken(FormulaTokenCode.MissArg);
                    num = (byte) (num + 1);
                    flag = true;
                }
                else
                {
                    this.PrimaryExpression();
                    if (this.buffer.Pos > pos)
                    {
                        num = (byte) (num + 1);
                    }
                }
            }
            while (flag || this.GetNextOnDemand(','));
            return num;
        }

        private void ConcatExpression()
        {
            this.AdditiveExpression();
            while (this.GetNextOnDemand('&'))
            {
                this.ResetCounter();
                this.AdditiveExpression();
                this.ResetCounter("Operand for binary operator.");
                this.AddToken(FormulaTokenCode.Concat);
            }
        }

        private void Expected(char what)
        {
            this.Expected(what.ToString());
        }

        private void Expected(string what)
        {
            this.NotifyError("Expected: " + what);
        }

        private void ExponentiationExpression()
        {
            this.PercentExpression();
            while (this.GetNextOnDemand('^'))
            {
                this.ResetCounter();
                this.PercentExpression();
                this.ResetCounter("Operand for binary operator.");
                this.AddToken(FormulaTokenCode.Power);
            }
        }

        private void Expression()
        {
            this.ConcatExpression();
        Label_0006:
            while (this.buffer.Peek() == '=')
            {
                this.buffer.GetNext();
                this.ResetCounter();
                this.ConcatExpression();
                this.ResetCounter("Operand for binary operator.");
                this.AddToken(FormulaTokenCode.Eq);
            }
            if (this.buffer.Peek() == '<')
            {
                char ch = '<';
                this.buffer.GetNext();
                if (((this.buffer.Peek() == '>') || (this.buffer.Peek() == '>')) || (this.buffer.Peek() == '='))
                {
                    ch = this.buffer.Peek();
                    this.buffer.GetNext();
                }
                this.ResetCounter();
                this.ConcatExpression();
                this.ResetCounter("Operand for binary operator.");
                switch (ch)
                {
                    case '=':
                        this.AddToken(FormulaTokenCode.Le);
                        goto Label_0006;

                    case '>':
                        this.AddToken(FormulaTokenCode.Ne);
                        goto Label_0006;
                }
                this.AddToken(FormulaTokenCode.Lt);
                goto Label_0006;
            }
            if (this.buffer.Peek() != '>')
            {
                return;
            }
            char ch2 = '>';
            this.buffer.GetNext();
            if (this.buffer.Peek() == '=')
            {
                ch2 = '=';
                this.buffer.GetNext();
            }
            this.ResetCounter();
            this.ConcatExpression();
            this.ResetCounter("Operand for binary operator.");
            if (ch2 == '=')
            {
                this.AddToken(FormulaTokenCode.Ge);
            }
            else
            {
                this.AddToken(FormulaTokenCode.Gt);
            }
            goto Label_0006;
        }

        private void Formula()
        {
            this.Match('=');
            if (this.GetNextOnDemand('{'))
            {
                this.NotifyError("We don't support array formula.");
            }
            else
            {
                this.PrimaryExpression();
            }
            if (!this.buffer.IsEOF)
            {
                this.Expected("Operand for primary expression.");
            }
        }

        private FormulaTokenCode GetBinaryOperator()
        {
            if (this.buffer.Peek(1) != '@')
            {
                char ch = this.buffer.Peek();
                if (BinaryOperatorFormulaToken.BinaryOperatorsList.Contains(ch.ToString()))
                {
                    FormulaTokenCode code = (FormulaTokenCode) BinaryOperatorFormulaToken.StringsToCodes[ch.ToString()];
                    this.buffer.GetNext();
                    return code;
                }
                if (this.buffer.Peek(1) == '@')
                {
                    return FormulaTokenCode.Empty;
                }
                char ch2 = this.buffer.Peek(1);
                int num = ch + ch2;
                if (BinaryOperatorFormulaToken.BinaryOperatorsList.Contains(num.ToString()))
                {
                    int num2 = ch + ch2;
                    FormulaTokenCode code2 = (FormulaTokenCode) BinaryOperatorFormulaToken.StringsToCodes[num2.ToString()];
                    this.buffer.GetNext();
                    this.buffer.GetNext();
                    return code2;
                }
            }
            return FormulaTokenCode.Empty;
        }

        private string GetCell()
        {
            string match = string.Empty;
            if (this.GetNextOnDemand(RefFormulaToken.AbsoluteCellMark))
            {
                match = RefFormulaToken.AbsoluteCellMark + this.buffer.GetNextString(false);
            }
            else
            {
                match = this.buffer.GetNextString(false);
            }
            if (this.GetNextOnDemand(RefFormulaToken.AbsoluteCellMark, false))
            {
                match = match + RefFormulaToken.AbsoluteCellMark + this.buffer.GetNextString(false);
            }
            if (!RefFormulaToken.IsCell(match))
            {
                this.Expected("Cell.");
            }
            return match;
        }

        private string GetInnerString()
        {
            StringBuilder builder = new StringBuilder();
            while (char.IsLetterOrDigit(this.buffer.Peek()) && !this.buffer.IsEOF)
            {
                builder.Append(this.buffer.GetNext());
            }
            return builder.ToString();
        }

        private ushort GetLastTokenCode()
        {
            if (this.tokens.Count != 0)
            {
                return (this.tokens[this.tokens.Count - 1] as FormulaToken).Code;
            }
            return 0;
        }

        private bool GetNextOnDemand(char match)
        {
            return this.GetNextOnDemand(match, true);
        }

        private bool GetNextOnDemand(char[] matches)
        {
            return (this.buffer.GetNextOnDemand(matches) != '@');
        }

        private bool GetNextOnDemand(char match, bool skipWhitespaces)
        {
            return (this.buffer.GetNextOnDemand(match, skipWhitespaces) != '@');
        }

        private void InitBuffer(string formula)
        {
            this.buffer = new GemBox.Spreadsheet.Buffer(formula);
            this.buffer.SkipWhitespaces();
        }

        private void IntersectionExpression()
        {
            this.ReferenceExpression();
            while ((AreaFormulaToken.IsAreaToken(this.GetLastTokenCode()) && !this.isFunctionArgumentsProcessed) && this.GetNextOnDemand(' ', false))
            {
                this.ResetCounter();
                this.ReferenceExpression();
                this.ResetCounter("Operand for intersect operator.");
                this.AddToken(FormulaTokenCode.Isect);
            }
        }

        private bool IsCell(string cellValue)
        {
            if (this.buffer.Peek() != RefFormulaToken.AbsoluteCellMark)
            {
                return RefFormulaToken.IsCell(cellValue);
            }
            return true;
        }

        private bool IsError()
        {
            return this.GetNextOnDemand('#');
        }

        private static bool IsFloatOrInteger(string floatValue)
        {
            if (floatValue.Length <= 0)
            {
                return false;
            }
            if (!char.IsDigit(floatValue[0]))
            {
                return (floatValue[0] == '.');
            }
            return true;
        }

        private bool IsFunction(string name)
        {
            if ((name == null) || (name.Length == 0))
            {
                return false;
            }
            bool flag = FormulaFunctionsTable.Instance.IsFunction(name);
            if (flag)
            {
                this.Match('(', false);
            }
            return flag;
        }

        private bool IsNamedRange(string namedRange)
        {
            return Utilities.Contains(this.worksheet.NamedRanges.Names, namedRange);
        }

        private bool IsSheetReference(string sheet)
        {
            return (Utilities.Contains(this.worksheet.Parent.SheetNames, sheet) && this.GetNextOnDemand('!'));
        }

        private bool IsString()
        {
            return this.GetNextOnDemand('"');
        }

        private void Match(char match)
        {
            this.Match(match, true);
        }

        private void Match(char match, bool skipWhitespaces)
        {
            if (skipWhitespaces)
            {
                this.buffer.SkipWhitespaces();
            }
            if (this.buffer.Peek() != match)
            {
                this.Expected(match);
            }
            else
            {
                this.buffer.GetNext();
            }
        }

        private void MultiplicativeExpression()
        {
            this.ExponentiationExpression();
            while ((this.buffer.Peek() == '*') || (this.buffer.Peek() == '/'))
            {
                char ch = this.buffer.Peek();
                this.buffer.GetNext();
                this.ResetCounter();
                this.ExponentiationExpression();
                this.ResetCounter("Operand for binary operator.");
                if (ch == '*')
                {
                    this.AddToken(FormulaTokenCode.Mul);
                }
                else
                {
                    this.AddToken(FormulaTokenCode.Div);
                }
            }
        }

        private void NotifyError(string what)
        {
            throw new ArgumentException("Failed to parse: " + this.buffer.Data + ". Error: " + what);
        }

        /// <summary>
        /// Parses the specified string formula.
        /// </summary>
        /// <param name="formula">The string formula.</param>
        /// <returns>formula token array</returns>
        public FormulaToken[] Parse(string formula)
        {
            this.InitBuffer(formula);
            this.Formula();
            return (FormulaToken[]) this.tokens.ToArray(typeof(FormulaToken));
        }

        private void PercentExpression()
        {
            this.UnaryExpression();
            while (this.GetNextOnDemand('%'))
            {
                this.isProcentOperatorProcessed = true;
                this.AddToken(FormulaTokenCode.Percent);
                this.UnaryExpression();
                this.isProcentOperatorProcessed = false;
            }
        }

        private void PrimaryExpression()
        {
            if (this.GetNextOnDemand('('))
            {
                this.Expression();
                this.Match(')');
                this.AddToken(FormulaTokenCode.Parentheses);
                while (this.buffer.Peek() != ',')
                {
                    FormulaTokenCode binaryOperator = this.GetBinaryOperator();
                    if (binaryOperator == FormulaTokenCode.Empty)
                    {
                        return;
                    }
                    this.ResetCounter();
                    this.PrimaryExpression();
                    this.ResetCounter("Operand for binary operator.");
                    this.AddToken(binaryOperator);
                }
            }
            else
            {
                this.Expression();
            }
        }

        private void ProcessReferenceExpressionError(string nextString)
        {
            if (((!this.isFunctionArgumentsProcessed || (nextString.Length != 0)) || (this.buffer.Peek() != ')')) && !this.isProcentOperatorProcessed)
            {
                if (this.buffer.Peek() == '(')
                {
                    this.NotifyError("Unsupported function: " + nextString + ".For list of supported functions consult GemBox.Spreadsheet documentation.");
                }
                else if (this.buffer.Peek() == '@')
                {
                    this.NotifyError("Not expected end of file");
                }
                else if (nextString.Length > 0)
                {
                    this.NotifyError("Not expected: " + nextString);
                }
                else
                {
                    this.NotifyError("Not expected: " + this.buffer.Peek());
                }
            }
        }

        private void ReferenceExpression()
        {
            if (!this.buffer.IsEOF)
            {
                if (this.buffer.Peek() == '{')
                {
                    this.NotifyError("We don't support const array.");
                }
                else if (this.GetNextOnDemand('('))
                {
                    this.AddExpressionToken();
                }
                else
                {
                    string nextString = this.buffer.GetNextString(false);
                    bool flag = boolList.Contains(nextString.ToUpper());
                    if (this.IsNamedRange(nextString))
                    {
                        this.AddNamedRange(nextString);
                    }
                    else if (this.IsSheetReference(nextString))
                    {
                        this.AddSheetReferenceToken(nextString);
                    }
                    else if (flag)
                    {
                        this.AddBoolToken(nextString);
                    }
                    else if (this.IsFunction(nextString))
                    {
                        this.AddFunctionToken(nextString);
                    }
                    else if (this.IsCell(nextString))
                    {
                        this.AddCellOrRangeToken(nextString);
                    }
                    else if (IsFloatOrInteger(nextString))
                    {
                        this.AddFloatOrIntegerToken(nextString);
                    }
                    else if (this.IsString())
                    {
                        this.AddStringToken();
                    }
                    else if (this.IsError())
                    {
                        this.AddErrorToken(nextString);
                    }
                    else
                    {
                        this.ProcessReferenceExpressionError(nextString);
                    }
                }
            }
        }

        private void ResetCounter()
        {
            this.buffer.SkipWhitespaces();
            this.lastPos = this.buffer.Pos;
        }

        private void ResetCounter(string error)
        {
            if (this.buffer.Pos == this.lastPos)
            {
                this.Expected(error);
            }
        }

        private void UnaryExpression()
        {
            if (!this.buffer.IsEOF)
            {
                ArrayList unaryOperators = new ArrayList();
                while (this.UnaryOperator(unaryOperators))
                {
                }
                this.ResetCounter();
                this.UnionExpression();
                if (unaryOperators.Count > 0)
                {
                    this.ResetCounter("Operand for unary operator.");
                }
                unaryOperators.Reverse();
                for (int i = 0; i < unaryOperators.Count; i++)
                {
                    char ch = (char) unaryOperators[i];
                    if (ch == '+')
                    {
                        this.AddToken(FormulaTokenCode.Uplus);
                    }
                    else
                    {
                        this.AddToken(FormulaTokenCode.Uminus);
                    }
                }
            }
        }

        private bool UnaryOperator(ArrayList unaryOperators)
        {
            bool flag = false;
            if (this.GetNextOnDemand('+'))
            {
                unaryOperators.Add('+');
                return true;
            }
            if (this.GetNextOnDemand('-'))
            {
                unaryOperators.Add('-');
                flag = true;
            }
            return flag;
        }

        private void UnionExpression()
        {
            this.IntersectionExpression();
            while ((AreaFormulaToken.IsAreaToken(this.GetLastTokenCode()) && !this.isFunctionArgumentsProcessed) && this.GetNextOnDemand(','))
            {
                this.ResetCounter();
                this.IntersectionExpression();
                this.ResetCounter("Operand for union operator.");
                this.AddToken(FormulaTokenCode.List);
            }
        }
    }
}

