namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;

    internal abstract class BinaryWritable
    {
        protected BinaryWritable()
        {
        }

        public abstract void Write(BinaryWriter bw);

        public abstract int Size { get; }
    }
}

