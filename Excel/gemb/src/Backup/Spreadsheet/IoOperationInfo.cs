namespace GemBox.Spreadsheet
{
    using System;

    internal class IoOperationInfo
    {
        private ExcelFile excelFile;
        private string fileName;
        private IoOperation operation;

        public IoOperationInfo(ExcelFile excelFile, string fileName, IoOperation operation)
        {
            this.excelFile = excelFile;
            this.fileName = fileName;
            this.operation = operation;
        }

        public ExcelFile File
        {
            get
            {
                return this.excelFile;
            }
        }

        public string FileName
        {
            get
            {
                return this.fileName;
            }
        }

        public IoOperation Operation
        {
            get
            {
                return this.operation;
            }
        }
    }
}

