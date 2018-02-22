namespace GemBox.Spreadsheet
{
    using System;

    /// <summary>
    /// Warning event arguments class used by the event which is raised on warning happens in the excel lite library
    /// </summary>
    public class IoWarningEventArgs : EventArgs
    {
        private string fileName;
        private IoOperation operation;
        private string warningMsg;

        internal IoWarningEventArgs(string fileName, IoOperation operation, string warningMsg)
        {
            this.fileName = fileName;
            this.operation = operation;
            this.warningMsg = warningMsg;
        }

        /// <summary>
        /// Gets the file name passed to the XLS / CSV file reading / writing method.
        /// </summary>
        /// <remarks>
        /// You can use this property to handle different files in a different way inside your event handlers.
        /// </remarks>
        public string FileName
        {
            get
            {
                return this.fileName;
            }
        }

        /// <summary>
        /// Indicates which operation caused this event to fire.
        /// </summary>
        /// <remarks>
        /// You can use this property to handle XLS / CSV or reading / writing operations differently 
        /// inside your event handlers.
        /// </remarks>
        public IoOperation Operation
        {
            get
            {
                return this.operation;
            }
        }

        /// <summary>
        /// Gets the message explaining the specific warning.
        /// </summary>
        public string WarningMsg
        {
            get
            {
                return this.warningMsg;
            }
        }
    }
}

