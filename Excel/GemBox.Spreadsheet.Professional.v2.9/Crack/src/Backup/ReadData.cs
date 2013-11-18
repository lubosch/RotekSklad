namespace GemBox
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Runtime.InteropServices;

    internal class ReadData : CompoundFileData
    {
        internal bool closeInputStream;
        internal const string errNotValidOle2 = "Reading error: file is not a valid OLE2 Compound File.";
        internal Stream inputStream;
        private Ole2CompoundFile ole2File;

        public ReadData(Ole2CompoundFile ole2File, Stream inputStream)
        {
            ArrayList list;
            this.ole2File = ole2File;
            this.inputStream = inputStream;
            BinaryReader br = new BinaryReader(inputStream);
            this.ReadHeader(br, out list);
            this.ReadMATRemainder(br, list);
            this.ReadSAT(br, list);
            this.ReadDirectoryStream(br);
            this.ReadSSAT(br);
            this.ReadShortSectorContainerStream(br);
        }

        private static void AddTreeMembers(Ole2Storage storage, ArrayList nodes, int nodeDID)
        {
            CompoundFileData.RedBlackTreeNode node = (CompoundFileData.RedBlackTreeNode) nodes[nodeDID];
            if (node.LeftDID != -1)
            {
                AddTreeMembers(storage, nodes, node.LeftDID);
            }
            storage.Add(node.DirectoryEntry);
            if (node.RightDID != -1)
            {
                AddTreeMembers(storage, nodes, node.RightDID);
            }
        }

        private static int[] GetIDChain(IList list, int ID)
        {
            ArrayList list2 = new ArrayList();
            while (ID != -2)
            {
                list2.Add(ID);
                ID = (int) list[ID];
            }
            return (int[]) list2.ToArray(typeof(int));
        }

        internal byte[] GetStreamData(Ole2Stream stream)
        {
            if (stream.Size < base.shortThreshold)
            {
                return this.GetStreamDataHelper(stream.Size, stream.ReadSID, true, base.shortSectorSize, base.shortStreamContainer, base.shortSectorAllocationTable);
            }
            return this.GetStreamDataHelper(stream.Size, stream.ReadSID, false, base.sectorSize, this.inputStream, base.sectorAllocationTable);
        }

        private byte[] GetStreamDataHelper(int streamSize, int firstSID, bool shortSector, int blockSize, Stream input, ArrayList allocationTable)
        {
            byte[] buffer = new byte[streamSize];
            int offset = 0;
            foreach (int num3 in GetIDChain(allocationTable, firstSID))
            {
                int num2;
                this.SeekSector(input, num3, shortSector);
                if ((offset + blockSize) > streamSize)
                {
                    num2 = streamSize - offset;
                }
                else
                {
                    num2 = blockSize;
                }
                input.Read(buffer, offset, num2);
                offset += num2;
            }
            return buffer;
        }

        private CompoundFileData.RedBlackTreeNode ReadDirectoryNode(BinaryReader br)
        {
            CompoundFileData.RedBlackTreeNode node = null;
            int num;
            char[] chArray = new char[0x20];
            for (num = 0; num < 0x20; num++)
            {
                chArray[num] = (char) br.ReadUInt16();
            }
            num = 0;
            while (num < 0x20)
            {
                if (chArray[num] == '\0')
                {
                    break;
                }
                num++;
            }
            string name = new string(chArray, 0, num);
            br.ReadUInt16();
            CompoundFileData.DirectoryEntryType type = (CompoundFileData.DirectoryEntryType) br.ReadByte();
            br.ReadByte();
            int num2 = br.ReadInt32();
            int num3 = br.ReadInt32();
            int num4 = br.ReadInt32();
            byte[] buffer = br.ReadBytes(0x10);
            byte[] buffer2 = br.ReadBytes(4);
            byte[] buffer3 = br.ReadBytes(8);
            byte[] buffer4 = br.ReadBytes(8);
            int readSID = br.ReadInt32();
            int size = br.ReadInt32();
            byte[] buffer5 = br.ReadBytes(4);
            if (type != CompoundFileData.DirectoryEntryType.Empty)
            {
                Ole2DirectoryEntry root;
                node = new CompoundFileData.RedBlackTreeNode();
                node.LeftDID = num2;
                node.RightDID = num3;
                node.MembersDID = num4;
                switch (type)
                {
                    case CompoundFileData.DirectoryEntryType.UserStorage:
                        root = new Ole2Storage(name);
                        break;

                    case CompoundFileData.DirectoryEntryType.UserStream:
                        root = new Ole2Stream(name, size, readSID, this);
                        break;

                    case CompoundFileData.DirectoryEntryType.RootStorage:
                        base.shortStreamContainerSize = size;
                        base.shortStreamContainerSID = readSID;
                        this.ole2File.root = new Ole2Storage(name);
                        root = this.ole2File.root;
                        break;

                    default:
                        root = null;
                        break;
                }
                root.UniqueIdentifier = buffer;
                root.UserFlags = buffer2;
                root.TimeStampCreation = buffer3;
                root.TimeStampModification = buffer4;
                root.NotUsed = buffer5;
                node.DirectoryEntry = root;
            }
            return node;
        }

        private void ReadDirectoryStream(BinaryReader br)
        {
            int num = base.sectorSize / 0x80;
            ArrayList nodes = new ArrayList();
            foreach (int num2 in GetIDChain(base.sectorAllocationTable, base.directoryStreamSID))
            {
                this.SeekSector(br.BaseStream, num2, false);
                for (int i = 0; i < num; i++)
                {
                    nodes.Add(this.ReadDirectoryNode(br));
                }
            }
            foreach (CompoundFileData.RedBlackTreeNode node in nodes)
            {
                if (((node != null) && (node.DirectoryEntry != null)) && (node.MembersDID != -1))
                {
                    AddTreeMembers((Ole2Storage) node.DirectoryEntry, nodes, node.MembersDID);
                }
            }
        }

        private void ReadHeader(BinaryReader br, out ArrayList masterAllocationTable)
        {
            byte[] buffer = br.ReadBytes(8);
            for (int i = 0; i < 8; i++)
            {
                if (buffer[i] != CompoundFileData.validHeader[i])
                {
                    throw new CompoundFileException("Reading error: file is not a valid OLE2 Compound File.");
                }
            }
            this.ole2File.UniqueIdentifier = br.ReadBytes(0x10);
            this.ole2File.RevisionNumber = br.ReadUInt16();
            this.ole2File.VersionNumber = br.ReadUInt16();
            br.ReadUInt16();
            base.sectorSize = ((int) 1) << br.ReadUInt16();
            base.shortSectorSize = ((int) 1) << br.ReadUInt16();
            this.ole2File.NotUsed1 = br.ReadBytes(10);
            base.SATSectorCount = br.ReadInt32();
            base.directoryStreamSID = br.ReadInt32();
            this.ole2File.NotUsed2 = br.ReadBytes(4);
            base.shortThreshold = br.ReadInt32();
            base.shortTableSID = br.ReadInt32();
            base.SSATSectorCount = br.ReadInt32();
            base.masterAllocationTableSID = br.ReadInt32();
            base.MATSectorCount = br.ReadInt32();
            masterAllocationTable = new ArrayList();
            for (int j = 0; j < 0x6d; j++)
            {
                masterAllocationTable.Add(br.ReadInt32());
            }
        }

        private void ReadMATRemainder(BinaryReader br, ArrayList masterAllocationTable)
        {
            int num = (base.sectorSize / 4) - 1;
            for (int i = base.masterAllocationTableSID; i >= 0; i = br.ReadInt32())
            {
                this.SeekSector(br.BaseStream, i, false);
                for (int j = 0; j < num; j++)
                {
                    masterAllocationTable.Add(br.ReadInt32());
                }
            }
        }

        private void ReadSAT(BinaryReader br, ArrayList masterAllocationTable)
        {
            int num = base.sectorSize / 4;
            base.sectorAllocationTable = new ArrayList();
            foreach (int num2 in masterAllocationTable)
            {
                if (num2 == -1)
                {
                    break;
                }
                this.SeekSector(br.BaseStream, num2, false);
                for (int i = 0; i < num; i++)
                {
                    base.sectorAllocationTable.Add(br.ReadInt32());
                }
            }
        }

        private void ReadShortSectorContainerStream(BinaryReader br)
        {
            base.shortStreamContainer = new MemoryStream(this.GetStreamDataHelper(base.shortStreamContainerSize, base.shortStreamContainerSID, false, base.sectorSize, br.BaseStream, base.sectorAllocationTable));
        }

        private void ReadSSAT(BinaryReader br)
        {
            int num = base.sectorSize / 4;
            base.shortSectorAllocationTable = new ArrayList();
            foreach (int num2 in GetIDChain(base.sectorAllocationTable, base.shortTableSID))
            {
                this.SeekSector(br.BaseStream, num2, false);
                for (int i = 0; i < num; i++)
                {
                    base.shortSectorAllocationTable.Add(br.ReadInt32());
                }
            }
        }

        private long SectorOffset(int SID, bool shortSector)
        {
            if (shortSector)
            {
                return (long) (SID * base.shortSectorSize);
            }
            return (long) (0x200 + (SID * base.sectorSize));
        }

        private void SeekSector(Stream stream, int SID, bool shortSector)
        {
            stream.Seek(this.SectorOffset(SID, shortSector), SeekOrigin.Begin);
        }
    }
}

