namespace GemBox
{
    using System;

    internal class Ole2DirectoryEntry
    {
        private string name;
        private byte[] notUsed;
        private byte[] timeStampCreation;
        private byte[] timeStampModification;
        private byte[] uniqueIdentifier;
        private byte[] userFlags;

        internal Ole2DirectoryEntry(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if ((value == null) || (value.Length == 0))
                {
                    throw new CompoundFileException("Directory entry name can't be empty.");
                }
                if (value.Length > 0x1f)
                {
                    throw new CompoundFileException("Directory entry name can't be larger than 31 character.");
                }
                this.name = value;
            }
        }

        internal byte[] NotUsed
        {
            get
            {
                return Ole2CompoundFile.GetExtraData(this.notUsed, 4);
            }
            set
            {
                Ole2CompoundFile.SetExtraData(value, 4, ref this.notUsed);
            }
        }

        internal byte[] TimeStampCreation
        {
            get
            {
                return Ole2CompoundFile.GetExtraData(this.timeStampCreation, 8);
            }
            set
            {
                Ole2CompoundFile.SetExtraData(value, 8, ref this.timeStampCreation);
            }
        }

        internal byte[] TimeStampModification
        {
            get
            {
                return Ole2CompoundFile.GetExtraData(this.timeStampModification, 8);
            }
            set
            {
                Ole2CompoundFile.SetExtraData(value, 8, ref this.timeStampModification);
            }
        }

        internal byte[] UniqueIdentifier
        {
            get
            {
                return Ole2CompoundFile.GetExtraData(this.uniqueIdentifier, 0x10);
            }
            set
            {
                Ole2CompoundFile.SetExtraData(value, 0x10, ref this.uniqueIdentifier);
            }
        }

        internal byte[] UserFlags
        {
            get
            {
                return Ole2CompoundFile.GetExtraData(this.userFlags, 4);
            }
            set
            {
                Ole2CompoundFile.SetExtraData(value, 4, ref this.userFlags);
            }
        }
    }
}

