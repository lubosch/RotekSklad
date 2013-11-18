namespace GemBox.Spreadsheet
{
    using System;

    internal class UnknownObjSubRecord : ObjSubRecord
    {
        private byte[] data;

        public UnknownObjSubRecord(ushort length, byte[] buffer) : base(length, buffer)
        {
        }

        /// <summary>
        /// Returns binary representation of the subrecord.
        /// </summary>
        /// <returns>Binary representation of the subrecord.</returns>
        public override byte[] ConvertToBytes()
        {
            byte[] array = new byte[base.Length + 4];
            this.data.CopyTo(array, 4);
            BitConverter.GetBytes((short) 0x15).CopyTo(array, 0);
            BitConverter.GetBytes(base.Length).CopyTo(array, 2);
            return array;
        }

        /// <summary>
        /// Reads bye array.
        /// </summary>
        /// <param name="buffer">Array to Read.</param>
        public override void Read(byte[] buffer)
        {
            this.data = new byte[base.Length];
            Array.Copy(buffer, 0, this.data, 0, base.Length);
        }
    }
}

