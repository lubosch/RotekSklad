namespace GemBox
{
    using System;
    using System.Collections;
    using System.IO;

    internal class CompoundFileData
    {
        protected int directoryStreamSID;
        protected int masterAllocationTableSID;
        protected int MATSectorCount;
        protected int SATSectorCount;
        protected ArrayList sectorAllocationTable;
        protected int sectorAllocationTableSID;
        protected int sectorSize;
        protected ArrayList shortSectorAllocationTable;
        protected int shortSectorSize;
        protected MemoryStream shortStreamContainer;
        protected int shortStreamContainerSID;
        protected int shortStreamContainerSize;
        protected int shortTableSID;
        protected int shortThreshold;
        protected int SSATSectorCount;
        protected static readonly byte[] validHeader = new byte[] { 0xd0, 0xcf, 0x11, 0xe0, 0xa1, 0xb1, 0x1a, 0xe1 };

        public static int Padding(int streamSize, int sectorSize)
        {
            return ((SectorCount(streamSize, sectorSize) * sectorSize) - streamSize);
        }

        public static int SectorCount(int streamSize, int sectorSize)
        {
            return (((streamSize + sectorSize) - 1) / sectorSize);
        }

        protected enum DirectoryEntryType : byte
        {
            Empty = 0,
            LockBytes = 3,
            Property = 4,
            RootStorage = 5,
            UserStorage = 1,
            UserStream = 2
        }

        protected enum NodeColor : byte
        {
            Black = 1,
            Red = 0
        }

        protected class RedBlackTreeNode
        {
            public Ole2DirectoryEntry DirectoryEntry;
            public int LeftDID;
            public int MembersDID;
            public int RightDID;
        }

        protected enum SpecialSIDs
        {
            EndOfChain = -2,
            Free = -1,
            MSAT = -4,
            SAT = -3
        }
    }
}

