namespace GemBox.Spreadsheet
{
    using System;

    internal abstract class ObjSubRecord
    {
        private ushort length;

        protected ObjSubRecord()
        {
        }

        protected ObjSubRecord(ushort length, byte[] buffer)
        {
            this.length = length;
            this.Read(buffer);
        }

        public virtual byte[] ConvertToBytes()
        {
            byte[] array = new byte[4];
            BitConverter.GetBytes((short) 0x15).CopyTo(array, 0);
            return array;
        }

        public abstract void Read(byte[] buffer);

        public ushort Length
        {
            get
            {
                return this.length;
            }
        }
    }
}

