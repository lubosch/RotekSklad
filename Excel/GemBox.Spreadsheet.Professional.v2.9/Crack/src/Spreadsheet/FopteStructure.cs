namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;

    internal class FopteStructure
    {
        private ushort id;
        private bool isComplex;
        private bool isValid;
        private uint uintValue;

        public FopteStructure()
        {
        }

        public FopteStructure(BinaryReader reader)
        {
            ushort num = reader.ReadUInt16();
            this.id = (ushort) (num & 0x3fff);
            this.isValid = (num - 0x4000) == num;
            this.isComplex = (num - 0x8000) == num;
            this.uintValue = reader.ReadUInt32();
        }

        public byte[] ConvertToBytes()
        {
            byte[] array = new byte[6];
            ushort num = (ushort) (((ushort) this.Id) & 0x3fff);
            if (this.IsValid)
            {
                num = (ushort) (num + 0x4000);
            }
            if (this.IsComplex)
            {
                num = (ushort) (num + 0x8000);
            }
            BitConverter.GetBytes(num).CopyTo(array, 0);
            BitConverter.GetBytes(this.UintValue).CopyTo(array, 2);
            return array;
        }

        public MsoOptions Id
        {
            get
            {
                return (MsoOptions) this.id;
            }
            set
            {
                this.id = (ushort) value;
            }
        }

        public bool IsComplex
        {
            get
            {
                return this.isComplex;
            }
            set
            {
                this.isComplex = value;
            }
        }

        public bool IsValid
        {
            get
            {
                return this.isValid;
            }
            set
            {
                this.isValid = value;
            }
        }

        public uint UintValue
        {
            get
            {
                return this.uintValue;
            }
            set
            {
                this.uintValue = value;
            }
        }
    }
}

