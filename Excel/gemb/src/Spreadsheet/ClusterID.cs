namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;

    internal class ClusterID
    {
        private uint groupId;
        private uint number;

        public ClusterID(BinaryReader reader)
        {
            this.groupId = reader.ReadUInt32();
            this.number = reader.ReadUInt32();
        }

        public ClusterID(uint groupId, uint number)
        {
            this.groupId = groupId;
            this.number = number;
        }

        public byte[] ConvertToBytes()
        {
            byte[] array = new byte[8];
            BitConverter.GetBytes(this.groupId).CopyTo(array, 0);
            BitConverter.GetBytes(this.number).CopyTo(array, 4);
            return array;
        }
    }
}

