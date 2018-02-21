namespace GemBox.Spreadsheet
{
    using System;

    internal class XLSDescriptor
    {
        public object[] BodyFormat;
        public int BodySize;
        public int Code;
        public const int ConditionalFormattings = -11;
        public const int DontCare = -2;
        public string HandlerClass;
        public string Name;
        public const int VarSize = -1;
        public const int WorksheetDrawings = -10;

        public XLSDescriptor(string name, int code, int bodySize, string handlerClass, object[] bodyFormat)
        {
            this.Name = name;
            this.Code = code;
            this.BodySize = bodySize;
            this.HandlerClass = handlerClass;
            this.BodyFormat = bodyFormat;
        }

        public bool IsFixedSize
        {
            get
            {
                return (this.BodySize >= 0);
            }
        }
    }
}

