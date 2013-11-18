namespace GemBox.Spreadsheet
{
    using System;

    /// <summary>
    /// Hold information about function( name, code, expected arguments count. )
    /// </summary>
    internal class FormulaFunctionInfo
    {
        private FormulaTokenCode argumentCode;
        /// <summary>
        /// Arguments count value, by default it is initilized with not fixed( variable ) argument count mark.
        /// </summary>
        private byte argumentsCount;
        private ushort code;
        private string name;
        private FormulaTokenClass returnCode;
        /// <summary>
        /// Is used to the specify for appropriate functins the variable count of arguments
        /// </summary>
        public const byte VariableArgumentAmountMark = 0xff;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GemBox.Spreadsheet.FormulaFunctionInfo" /> class.
        /// </summary>
        /// <param name="code">The function code.</param>
        /// <param name="name">The function name.</param>
        /// <param name="argumentCode">The argument code.</param>
        /// <param name="returnCode">The return code.</param>
        public FormulaFunctionInfo(ushort code, string name, FormulaTokenCode argumentCode, FormulaTokenClass returnCode) : this(code, name, argumentCode, returnCode, 0xff)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GemBox.Spreadsheet.FormulaFunctionInfo" /> class.
        /// </summary>
        /// <param name="code">The function code.</param>
        /// <param name="name">The function name.</param>
        /// <param name="argumentCode">The argument code.</param>
        /// <param name="returnCode">The return code.</param>
        /// <param name="argumentsCount">The function's arguments count.</param>
        public FormulaFunctionInfo(ushort code, string name, FormulaTokenCode argumentCode, FormulaTokenClass returnCode, byte argumentsCount)
        {
            this.argumentsCount = 0xff;
            this.code = code;
            this.name = name;
            this.argumentCode = argumentCode;
            this.returnCode = returnCode;
            this.argumentsCount = argumentsCount;
        }

        /// <summary>
        /// Gets or sets the argument code.
        /// </summary>
        /// <value>The argument code.</value>
        public FormulaTokenCode ArgumentCode
        {
            get
            {
                return this.argumentCode;
            }
        }

        /// <summary>
        /// Arguments count value, by default it is initilized with not fixed( variable ) argument count mark.
        /// </summary>
        public byte ArgumentsCount
        {
            get
            {
                return this.argumentsCount;
            }
        }

        /// <summary>
        /// Gets function code.
        /// </summary>
        /// <value>The function code.</value>
        public ushort Code
        {
            get
            {
                return this.code;
            }
        }

        /// <summary>
        /// Gets a value indicating whether function has fixed argument count.
        /// </summary>
        /// <value>
        /// <c>true</c> if this function has fixed argument count; otherwise, <c>false</c>.
        /// </value>
        public bool IsFixedArgumentCount
        {
            get
            {
                return (this.ArgumentsCount != 0xff);
            }
        }

        /// <summary>
        /// Gets function name.
        /// </summary>
        /// <value>Function name.</value>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Gets the return code.
        /// </summary>
        /// <value>The return code.</value>
        public FormulaTokenClass ReturnCode
        {
            get
            {
                return this.returnCode;
            }
        }
    }
}

