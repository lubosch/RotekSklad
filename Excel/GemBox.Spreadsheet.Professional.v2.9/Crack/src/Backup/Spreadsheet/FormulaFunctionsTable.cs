namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;
    using System.Reflection;

    /// <summary>
    /// Hold information about all supported functions.
    /// </summary>
    internal class FormulaFunctionsTable
    {
        private readonly Hashtable codesToFunctions = new Hashtable();
        private static FormulaFunctionsTable instance = new FormulaFunctionsTable();
        private readonly ArrayList names = new ArrayList();
        private readonly Hashtable namesToFunctions = new Hashtable();

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GemBox.Spreadsheet.FormulaFunctionsTable" /> class.
        /// Constructor is private to allow only creation of FormulaFunctionsTable instances only once.
        /// </summary>
        private FormulaFunctionsTable()
        {
            this.AddFunction(0, "COUNT", FormulaTokenCode.Ref1, FormulaTokenClass.Variable);
            this.AddFunction(1, "IF", FormulaTokenCode.Ref2, FormulaTokenClass.Variable);
            this.AddFunction(4, "SUM", FormulaTokenCode.Ref1, FormulaTokenClass.Variable);
            this.AddFunction(5, "AVERAGE", FormulaTokenCode.Ref1, FormulaTokenClass.Variable);
            this.AddFunction(6, "MIN", FormulaTokenCode.Ref1, FormulaTokenClass.Variable);
            this.AddFunction(7, "MAX", FormulaTokenCode.Ref1, FormulaTokenClass.Variable);
            this.AddFunction(8, "ROW", FormulaTokenCode.Ref1, FormulaTokenClass.Variable);
            this.AddFunction(9, "COLUMN", FormulaTokenCode.Ref1, FormulaTokenClass.Variable, 1);
            this.AddFunction(15, "SIN", FormulaTokenCode.Ref2, FormulaTokenClass.Variable, 1);
            this.AddFunction(0x10, "COS", FormulaTokenCode.Ref2, FormulaTokenClass.Variable, 1);
            this.AddFunction(0x13, "PI", FormulaTokenCode.Ref2, FormulaTokenClass.Variable, 0);
            this.AddFunction(20, "SQRT", FormulaTokenCode.Ref2, FormulaTokenClass.Variable, 1);
            this.AddFunction(0x15, "EXP", FormulaTokenCode.Ref2, FormulaTokenClass.Variable, 1);
            this.AddFunction(0x16, "LN", FormulaTokenCode.Ref2, FormulaTokenClass.Variable, 1);
            this.AddFunction(0x18, "ABS", FormulaTokenCode.Ref2, FormulaTokenClass.Variable, 1);
            this.AddFunction(0x19, "INT", FormulaTokenCode.Ref2, FormulaTokenClass.Variable, 1);
            this.AddFunction(0x1a, "SIGN", FormulaTokenCode.Ref2, FormulaTokenClass.Variable, 1);
            this.AddFunction(0x1b, "ROUND", FormulaTokenCode.Ref2, FormulaTokenClass.Variable, 2);
            this.AddFunction(0x1f, "MID", FormulaTokenCode.Ref2, FormulaTokenClass.Variable, 3);
            this.AddFunction(0x20, "LEN", FormulaTokenCode.Ref2, FormulaTokenClass.Variable, 1);
            this.AddFunction(0x21, "VALUE", FormulaTokenCode.Ref2, FormulaTokenClass.Variable, 1);
            this.AddFunction(0x22, "TRUE", FormulaTokenCode.Ref2, FormulaTokenClass.Variable, 0);
            this.AddFunction(0x23, "FALSE", FormulaTokenCode.Ref2, FormulaTokenClass.Variable, 0);
            this.AddFunction(0x24, "AND", FormulaTokenCode.Ref1, FormulaTokenClass.Variable);
            this.AddFunction(0x25, "OR", FormulaTokenCode.Ref1, FormulaTokenClass.Variable);
            this.AddFunction(0x26, "NOT", FormulaTokenCode.Ref2, FormulaTokenClass.Variable, 1);
            this.AddFunction(0x27, "MOD", FormulaTokenCode.Ref2, FormulaTokenClass.Variable, 2);
            this.AddFunction(0x2e, "VAR", FormulaTokenCode.Ref1, FormulaTokenClass.Variable);
            this.AddFunction(0x30, "TEXT", FormulaTokenCode.Ref2, FormulaTokenClass.Variable, 2);
            this.AddFunction(0x3f, "RAND", FormulaTokenCode.Ref2, FormulaTokenClass.Variable, 0);
            this.AddFunction(0x41, "DATE", FormulaTokenCode.Ref2, FormulaTokenClass.Variable, 3);
            this.AddFunction(0x42, "TIME", FormulaTokenCode.Ref2, FormulaTokenClass.Variable, 3);
            this.AddFunction(0x43, "DAY", FormulaTokenCode.Ref2, FormulaTokenClass.Variable, 1);
            this.AddFunction(0x44, "MONTH", FormulaTokenCode.Ref2, FormulaTokenClass.Variable, 1);
            this.AddFunction(0x45, "YEAR", FormulaTokenCode.Ref2, FormulaTokenClass.Variable, 1);
            this.AddFunction(70, "WEEKDAY", FormulaTokenCode.Ref2, FormulaTokenClass.Variable);
            this.AddFunction(0x47, "HOUR", FormulaTokenCode.Ref2, FormulaTokenClass.Variable, 1);
            this.AddFunction(0x48, "MINUTE", FormulaTokenCode.Ref2, FormulaTokenClass.Variable, 1);
            this.AddFunction(0x49, "SECOND", FormulaTokenCode.Ref2, FormulaTokenClass.Variable, 1);
            this.AddFunction(0x4a, "NOW", FormulaTokenCode.Ref2, FormulaTokenClass.Variable, 0);
            this.AddFunction(0x158, "SUBTOTAL", FormulaTokenCode.Ref2, FormulaTokenClass.Variable);
            this.AddFunction(0x65, "HLOOKUP", FormulaTokenCode.Ref2, FormulaTokenClass.Variable);
            IEnumerator enumerator = this.codesToFunctions.Values.GetEnumerator();
            while (enumerator.MoveNext())
            {
                FormulaFunctionInfo current = enumerator.Current as FormulaFunctionInfo;
                this.names.Add(current.Name);
            }
        }

        private void AddFunction(ushort code, string name, FormulaTokenCode argumentCode, FormulaTokenClass returnCode)
        {
            FormulaFunctionInfo info = new FormulaFunctionInfo(code, name, argumentCode, returnCode);
            this.codesToFunctions[code] = info;
            this.namesToFunctions[name] = info;
        }

        private void AddFunction(ushort code, string name, FormulaTokenCode argumentCode, FormulaTokenClass returnCode, byte argumentsCount)
        {
            FormulaFunctionInfo info = new FormulaFunctionInfo(code, name, argumentCode, returnCode, argumentsCount);
            this.codesToFunctions[code] = info;
            this.namesToFunctions[name] = info;
        }

        /// <summary>
        /// Determines whether the specified name is function.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        /// <c>true</c> if the specified name is function; otherwise, <c>false</c>.
        /// </returns>
        public bool IsFunction(string name)
        {
            return (this[name] != null);
        }

        /// <summary>
        /// Gets the static FormulaFunctionsTable instance. Used to be shared between FormulaFunctionsTable' users.
        /// </summary>
        /// <value>The singleton FormulaFunctionTable instance.</value>
        public static FormulaFunctionsTable Instance
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// Gets the <see cref="T:GemBox.Spreadsheet.FormulaFunctionInfo" /> at the specified index.
        /// </summary>
        /// <value><see cref="T:GemBox.Spreadsheet.FormulaFunctionInfo" /> instance</value>
        public FormulaFunctionInfo this[string index]
        {
            get
            {
                return (this.namesToFunctions[index.ToUpper()] as FormulaFunctionInfo);
            }
        }

        /// <summary>
        /// Gets the <see cref="T:GemBox.Spreadsheet.FormulaFunctionInfo" /> at the specified index.
        /// </summary>
        /// <value><see cref="T:GemBox.Spreadsheet.FormulaFunctionInfo" /> instance</value>
        public FormulaFunctionInfo this[ushort index]
        {
            get
            {
                return (this.codesToFunctions[index] as FormulaFunctionInfo);
            }
        }

        /// <summary>
        /// Gets the names of predefined Excel functions.
        /// </summary>
        /// <value>The names of prdefined Excel function.</value>
        public ArrayList Names
        {
            get
            {
                return this.names;
            }
        }
    }
}

