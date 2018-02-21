namespace GemBox.Spreadsheet
{
    using System;

    internal class OptimizedBuffer
    {
        private byte[] buffer = new byte[0x20];
        private int length;

        private void AllocatedMemoryOnDemand(int offset, int length)
        {
            if ((offset + length) > this.buffer.Length)
            {
                byte[] dst = new byte[(offset * 2) + length];
                System.Buffer.BlockCopy(this.buffer, 0, dst, 0, this.buffer.Length);
                this.buffer = dst;
            }
            this.length += length;
        }

        private byte[] SkipUnUsefullLastBytes()
        {
            byte[] destinationArray = new byte[this.length];
            Array.Copy(this.buffer, 0, destinationArray, 0, this.length);
            return destinationArray;
        }

        public void Write(byte data, int offset)
        {
            this.AllocatedMemoryOnDemand(offset, 1);
            this.buffer[offset] = data;
        }

        public void Write(int data, int offset)
        {
            this.AllocatedMemoryOnDemand(offset, 4);
            System.Buffer.BlockCopy(BitConverter.GetBytes(data), 0, this.buffer, offset, 4);
        }

        public void Write(ushort data, int offset)
        {
            this.AllocatedMemoryOnDemand(offset, 2);
            System.Buffer.BlockCopy(BitConverter.GetBytes(data), 0, this.buffer, offset, 2);
        }

        public void Write(uint data, int offset)
        {
            this.AllocatedMemoryOnDemand(offset, 4);
            System.Buffer.BlockCopy(BitConverter.GetBytes(data), 0, this.buffer, offset, 4);
        }

        public void Write(byte[] bytes, int offset)
        {
            this.AllocatedMemoryOnDemand(offset, bytes.Length);
            System.Buffer.BlockCopy(bytes, 0, this.buffer, offset, bytes.Length);
        }

        public void Write(byte[] bytes, int offset, int pos, int length)
        {
            this.AllocatedMemoryOnDemand(offset, length);
            System.Buffer.BlockCopy(bytes, pos, this.buffer, offset, length);
        }

        internal byte[] Buffer
        {
            get
            {
                return this.SkipUnUsefullLastBytes();
            }
        }
    }
}

