namespace GemBox.Spreadsheet
{
    using System;

    internal class CmoObjSubRecord : ObjSubRecord
    {
        private ushort id;

        public CmoObjSubRecord()
        {
        }

        public CmoObjSubRecord(ushort length, byte[] buffer) : base(length, buffer)
        {
        }

        public override byte[] ConvertToBytes()
        {
            byte[] array = new byte[0x16];
            BitConverter.GetBytes((short) 0x15).CopyTo(array, 0);
            BitConverter.GetBytes((short) 0x12).CopyTo(array, 2);
            BitConverter.GetBytes((short) 0x19).CopyTo(array, 4);
            BitConverter.GetBytes(this.id).CopyTo(array, 6);
            BitConverter.GetBytes((ushort) 0).CopyTo(array, 8);
            byte[] buffer3 = new byte[12];
            buffer3.CopyTo(array, 10);
            return array;
        }

        public override void Read(byte[] buffer)
        {
            this.id = BitConverter.ToUInt16(buffer, 2);
        }

        /// <summary>
        /// Object's id.
        /// </summary>
        public ushort ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }
    }
}

