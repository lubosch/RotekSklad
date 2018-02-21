namespace GemBox
{
    using System;
    using System.IO;

    internal sealed class Ole2CompoundFile : IDisposable
    {
        internal const ushort ByteOrder = 0xfffe;
        private byte[] notUsed1;
        private byte[] notUsed2;
        private ReadData readData;
        internal ushort RevisionNumber = 0x3e;
        internal Ole2Storage root;
        private byte[] uniqueIdentifier;
        internal ushort VersionNumber = 3;

        public void Close()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            if (this.readData != null)
            {
                if (this.readData.closeInputStream)
                {
                    this.readData.inputStream.Close();
                }
                this.readData = null;
            }
        }

        internal static byte[] GetExtraData(byte[] data, int size)
        {
            if (data != null)
            {
                return data;
            }
            return new byte[size];
        }

        public void Load(Stream stream, bool loadOnDemand)
        {
            if (stream == null)
            {
                throw new CompoundFileException("Stream argument can't be null.");
            }
            if (stream.Length < 0x200L)
            {
                throw new CompoundFileException("Reading error: file is not a valid OLE2 Compound File.");
            }
            this.Dispose();
            ReadData data = new ReadData(this, stream);
            if (loadOnDemand)
            {
                this.readData = data;
            }
            else
            {
                this.Root.CacheAllStreams();
            }
        }

        public void Load(string fileName, bool loadOnDemand)
        {
            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            try
            {
                this.Load(stream, loadOnDemand);
            }
            finally
            {
                if (loadOnDemand)
                {
                    if (this.readData != null)
                    {
                        this.readData.closeInputStream = true;
                    }
                }
                else
                {
                    stream.Close();
                }
            }
        }

        public void Save(Stream stream)
        {
            if (stream == null)
            {
                throw new CompoundFileException("Stream argument can't be null.");
            }
            new WriteData(this, stream);
        }

        public void Save(string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Create))
            {
                this.Save(stream);
            }
        }

        internal static void SetExtraData(byte[] data, int size, ref byte[] variable)
        {
            if (data.Length != size)
            {
                throw new CompoundFileException("Internal error: wrong data size.");
            }
            variable = data;
        }

        internal byte[] NotUsed1
        {
            get
            {
                return GetExtraData(this.notUsed1, 10);
            }
            set
            {
                SetExtraData(value, 10, ref this.notUsed1);
            }
        }

        internal byte[] NotUsed2
        {
            get
            {
                return GetExtraData(this.notUsed2, 4);
            }
            set
            {
                SetExtraData(value, 4, ref this.notUsed2);
            }
        }

        public Ole2Storage Root
        {
            get
            {
                if (this.root == null)
                {
                    this.root = new Ole2Storage("Root Entry");
                }
                return this.root;
            }
        }

        internal byte[] UniqueIdentifier
        {
            get
            {
                return GetExtraData(this.uniqueIdentifier, 0x10);
            }
            set
            {
                SetExtraData(value, 0x10, ref this.uniqueIdentifier);
            }
        }
    }
}

