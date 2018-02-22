namespace GemBox.Spreadsheet
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents errors that can occur in the GemBox.Spreadsheet component. 
    /// </summary>
    [Serializable]
    public class SpreadsheetException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the Exception class. 
        /// </summary>
        public SpreadsheetException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Exception class with a specified error message. 
        /// </summary>
        /// <param name="msg">Message string.</param>
        public SpreadsheetException(string msg) : base(msg)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Exception class with serialized data. 
        /// </summary>
        /// <param name="info">Serialization info.</param>
        /// <param name="context">Serialization context.</param>
        protected SpreadsheetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Exception class with a specified error message 
        /// and a reference to the inner exception that is the cause of this exception. 
        /// </summary>
        /// <param name="msg">Message string.</param>
        /// <param name="inner">Inner exception.</param>
        public SpreadsheetException(string msg, Exception inner) : base(msg, inner)
        {
        }
    }
}

