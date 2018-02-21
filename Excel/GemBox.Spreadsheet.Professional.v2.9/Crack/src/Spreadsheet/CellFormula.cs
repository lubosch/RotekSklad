namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;
    using System.Diagnostics;
    using System.Text;

    internal class CellFormula
    {
        private const int bytesCountBeforeTokens = 0x10;
        private bool delayParse;
        internal ArrayList ExtraFormulaRecords;
        private string formula;
        internal FormulaOptions Options;
        internal object[] ResultBytes;
        private string resultString;
        internal object[] RpnBytes;
        private ExcelWorksheet sheet;
        private FormulaToken[] tokens;

        internal CellFormula(string formula, ExcelWorksheet sheet)
        {
            this.formula = formula;
            this.sheet = sheet;
            if (sheet.ParentExcelFile.DelayFormulaParse)
            {
                this.delayParse = true;
            }
            else
            {
                this.Parse(formula, sheet);
            }
            this.SetDefaultResultAndOptions();
        }

        internal CellFormula(ExcelWorksheet sheet, object[] resultBytes, FormulaOptions options, object[] rpnBytes)
        {
            this.sheet = sheet;
            this.ResultBytes = resultBytes;
            this.Options = options;
            this.RpnBytes = rpnBytes;
        }

        private object[] ConvertToBytes(FormulaToken[] tokens)
        {
            ArrayList list = new ArrayList();
            int num = 0;
            for (int i = 0; i < tokens.Length; i++)
            {
                byte[] buffer = tokens[i].ConvertToBytes();
                list.Add(buffer);
                num += buffer.Length;
            }
            object[] destinationArray = new object[num];
            int destinationIndex = 0;
            for (int j = 0; j < list.Count; j++)
            {
                byte[] sourceArray = list[j] as byte[];
                Array.Copy(sourceArray, 0, destinationArray, destinationIndex, sourceArray.Length);
                destinationIndex += sourceArray.Length;
            }
            return destinationArray;
        }

        private string ConvertToString(FormulaToken[] tokens)
        {
            Stack operandsStack = new Stack();
            bool flag = false;
            if (tokens != null)
            {
                this.TokensToString(operandsStack, tokens);
            }
            else
            {
                flag = this.TokenBytesToString(operandsStack);
            }
            Trace.Assert(flag || (operandsStack.Count == 1), "In operands stack must be 1 string: human representation of rpn tokens.");
            Trace.Assert(flag || (operandsStack.Peek().GetType() == typeof(string)), "In operands stack must be 1 string: human representation of rpn tokens.");
            if (operandsStack.Count != 0)
            {
                return operandsStack.Peek().ToString();
            }
            return string.Empty;
        }

        internal static bool DecodeBoolValue(byte val)
        {
            return (val == 1);
        }

        private double DecodeDoubleValue()
        {
            byte[] destinationArray = new byte[8];
            Array.Copy(this.ResultBytes, 0, destinationArray, 0, 8);
            return BitConverter.ToDouble(destinationArray, 0);
        }

        internal static string DecodeErrorValue(byte val)
        {
            switch (val)
            {
                case 0:
                    return "#NULL!";

                case 7:
                    return "#DIV/0!";

                case 15:
                    return "#VALUE!";

                case 0x17:
                    return "#REF!";

                case 0x1d:
                    return "#NAME?";

                case 0x24:
                    return "#NUM!";

                case 0x2a:
                    return "#N/A!";
            }
            return "#ERROR!";
        }

        private object DecodeValue()
        {
            if ((((byte) this.ResultBytes[6]) == 0xff) && (((byte) this.ResultBytes[7]) == 0xff))
            {
                switch (((byte) this.ResultBytes[0]))
                {
                    case 0:
                        return this.resultString;

                    case 1:
                        return DecodeBoolValue((byte) this.ResultBytes[2]);

                    case 2:
                        return DecodeErrorValue((byte) this.ResultBytes[2]);

                    case 3:
                        return null;
                }
            }
            return this.DecodeDoubleValue();
        }

        private void EncodeValue(object val)
        {
            if (val is double)
            {
                BitConverter.GetBytes((double) val).CopyTo(this.ResultBytes, 0);
            }
            else if (val is bool)
            {
                this.ResultBytes[0] = (byte) 1;
                this.ResultBytes[2] = ((bool) val) ? ((byte) 1) : ((byte) 0);
                this.ResultBytes[6] = (byte) 0xff;
                this.ResultBytes[7] = (byte) 0xff;
            }
            else if (val is string)
            {
                if (ErrFormulaToken.ErrorsList.IndexOf(val) != -1)
                {
                    this.ResultBytes[0] = (byte) 2;
                    this.ResultBytes[2] = (byte) ErrFormulaToken.StringsToCodes[val];
                }
                else
                {
                    this.ResultBytes[0] = (byte) 0;
                    this.resultString = (string) val;
                }
                this.ResultBytes[6] = (byte) 0xff;
                this.ResultBytes[7] = (byte) 0xff;
            }
        }

        private void Parse(string formula, ExcelWorksheet sheet)
        {
            this.tokens = new FormulaParser(sheet).Parse(formula);
        }

        private static void ProcessBinaryOperator(FormulaToken token, Stack operandsStack)
        {
            operandsStack.Push((operandsStack.Pop() as string) + token.ToString() + (operandsStack.Pop() as string));
        }

        private static void ProcessFunction(FormulaToken token, Stack operandsStack)
        {
            byte num = (token is FunctionFormulaToken) ? (token as FunctionFormulaToken).ArgumentsCount : (token as FunctionVarFormulaToken).ArgumentsCount;
            if (token is FunctionFormulaToken)
            {
                ushort code = (token as FunctionFormulaToken).Function.Code;
            }
            else
            {
                ushort num1 = (token as FunctionVarFormulaToken).Function.Code;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(token.ToString());
            builder.Append("(");
            string[] strArray = new string[num];
            for (byte i = 0; i < num; i = (byte) (i + 1))
            {
                strArray[i] = operandsStack.Pop() as string;
            }
            for (byte j = num; j > 0; j = (byte) (j - 1))
            {
                string str = strArray[j - 1];
                builder.Append(str);
                if (j != 1)
                {
                    builder.Append(",");
                }
            }
            builder.Append(")");
            operandsStack.Push(builder.ToString());
        }

        private static void ProcessOperand(FormulaToken token, Stack operandsStack)
        {
            operandsStack.Push(token.ToString());
        }

        private static void ProcessToken(FormulaToken token, Stack operandsStack)
        {
            if (token.Type.IsUnary)
            {
                ProcessUnaryOperator(token, operandsStack);
            }
            else if (token.Type.IsBinary)
            {
                ProcessBinaryOperator(token, operandsStack);
            }
            else if (token.Type.IsOperand)
            {
                ProcessOperand(token, operandsStack);
            }
            else if (token.Type.IsFunction)
            {
                ProcessFunction(token, operandsStack);
            }
            else if (!token.Type.IsControl)
            {
                throw new ArgumentException("Invalid RPN token code.");
            }
        }

        private static void ProcessUnaryOperator(FormulaToken token, Stack operandsStack)
        {
            string str = operandsStack.Pop() as string;
            if (token.Code == 20)
            {
                operandsStack.Push(str + token.ToString());
            }
            else if (token.Code == 0x15)
            {
                operandsStack.Push("(" + str + ")");
            }
            else
            {
                operandsStack.Push(token.ToString() + str);
            }
        }

        /// <summary>
        /// Recalculate formula based on saved tokens.
        /// It need to be done for changing some data which can be changed after setting formula
        /// and before saving xls file.
        /// </summary>
        internal void Recalculate()
        {
            if (this.delayParse)
            {
                this.Parse(this.formula, this.sheet);
            }
            if (this.tokens != null)
            {
                this.RpnBytes = this.ConvertToBytes(this.tokens);
            }
        }

        private void SetDefaultResultAndOptions()
        {
            this.Value = 0;
            byte num = 0;
            this.ResultBytes = new object[] { num, num, num, num, num, num, num, num };
            this.Options = FormulaOptions.CalculateOnLoad;
        }

        private bool TokenBytesToString(Stack operandsStack)
        {
            byte[] destinationArray = new byte[this.RpnBytes.Length];
            Array.Copy(this.RpnBytes, 0, destinationArray, 0, this.RpnBytes.Length);
            int startIndex = 0;
            bool flag = false;
            bool flag2 = false;
            while (startIndex < this.RpnBytes.Length)
            {
                FormulaToken token = FormulaTokensFactory.CreateFrom(this.sheet, destinationArray, startIndex);
                if (token.Type.IsControl)
                {
                    flag2 = true;
                }
                else
                {
                    flag = true;
                }
                ProcessToken(token, operandsStack);
                startIndex += token.Size;
            }
            return (!flag && flag2);
        }

        private void TokensToString(Stack operandsStack, FormulaToken[] tokens)
        {
            for (int i = 0; i < tokens.Length; i++)
            {
                FormulaToken token = tokens[i];
                ProcessToken(token, operandsStack);
            }
        }

        internal string Formula
        {
            get
            {
                if (this.formula == null)
                {
                    this.formula = "=" + this.ConvertToString(null);
                }
                return this.formula;
            }
        }

        internal object Value
        {
            get
            {
                return this.DecodeValue();
            }
            set
            {
                this.EncodeValue(value);
            }
        }
    }
}

