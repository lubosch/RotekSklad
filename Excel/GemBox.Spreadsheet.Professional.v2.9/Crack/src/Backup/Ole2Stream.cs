namespace GemBox
{
    using System;

    internal class Ole2Stream : Ole2DirectoryEntry
    {
        private byte[] data;
        private GetStreamDataHandler getStreamDataMethod;
        internal int ReadSID;
        private int size;
        internal int WriteSID;

        internal Ole2Stream(string name, byte[] data) : base(name)
        {
            this.ReadSID = -1;
            this.WriteSID = -1;
            this.size = data.Length;
            this.data = data;
        }

        internal Ole2Stream(string name, int size, GetStreamDataHandler getStreamDataMethod) : base(name)
        {
            this.ReadSID = -1;
            this.WriteSID = -1;
            this.size = size;
            this.getStreamDataMethod = getStreamDataMethod;
        }

        internal Ole2Stream(string name, int size, int readSID, ReadData readData) : base(name)
        {
            this.ReadSID = -1;
            this.WriteSID = -1;
            this.size = size;
            this.ReadSID = readSID;
            this.getStreamDataMethod = new GetStreamDataHandler(readData.GetStreamData);
        }

        private void ExceptionIfDataWrong(byte[] data)
        {
            if (data.Length != this.size)
            {
                throw new CompoundFileException(string.Concat(new object[] { "Stream data has size of ", data.Length, " bytes, and expected was ", this.size, " bytes." }));
            }
        }

        public byte[] GetData()
        {
            if (this.data != null)
            {
                return this.data;
            }
            if (this.getStreamDataMethod == null)
            {
                throw new CompoundFileException("Internal error: No data or getStreamDataMethod.");
            }
            byte[] data = this.getStreamDataMethod(this);
            this.ExceptionIfDataWrong(data);
            return data;
        }

        internal void LoadData()
        {
            this.data = this.GetData();
            this.getStreamDataMethod = null;
        }

        public void SetData(byte[] data)
        {
            this.size = data.Length;
            this.ReadSID = -1;
            this.data = data;
            this.getStreamDataMethod = null;
        }

        public void SetData(int size, GetStreamDataHandler getStreamDataMethod)
        {
            this.size = size;
            this.ReadSID = -1;
            this.data = null;
            this.getStreamDataMethod = getStreamDataMethod;
        }

        public int Size
        {
            get
            {
                return this.size;
            }
        }
    }
}

