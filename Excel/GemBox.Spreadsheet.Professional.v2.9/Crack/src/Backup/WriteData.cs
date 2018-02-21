namespace GemBox
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.IO;

    internal class WriteData : CompoundFileData
    {
        private ArrayList nodes = new ArrayList();
        private Ole2CompoundFile ole2File;
        private Stream outputStream;

        public WriteData(Ole2CompoundFile ole2File, Stream outputStream)
        {
            base.sectorSize = 0x200;
            base.shortSectorSize = 0x40;
            base.shortThreshold = 0x1000;
            base.sectorAllocationTable = new ArrayList();
            base.shortSectorAllocationTable = new ArrayList();
            base.shortStreamContainer = new MemoryStream();
            this.ole2File = ole2File;
            this.outputStream = outputStream;
            this.BuildTables();
            BinaryWriter bw = new BinaryWriter(outputStream);
            this.WriteHeader(bw);
            this.ole2File.Root.VisitAll(new VisitDirectoryEntryHandler(this.WriteEntry));
            if (base.shortStreamContainerSize > 0)
            {
                this.WriteAllocationTable(bw, base.shortSectorAllocationTable);
                this.WriteShortSectorContainerStream();
            }
            this.WriteDirectoryStream(bw);
            this.WriteAllocationTable(bw, base.sectorAllocationTable);
            this.WriteMATRemainder(bw);
        }

        private void BuildSATAndMAT()
        {
            int num4;
            int sectorSize = base.sectorSize / 4;
            int num2 = sectorSize - 1;
            int count = base.sectorAllocationTable.Count;
            do
            {
                num4 = count;
                base.SATSectorCount = CompoundFileData.SectorCount(count, sectorSize);
                if (base.SATSectorCount > 0x6d)
                {
                    base.MATSectorCount = CompoundFileData.SectorCount(base.SATSectorCount - 0x6d, num2);
                }
                else
                {
                    base.MATSectorCount = 0;
                }
                count = (base.sectorAllocationTable.Count + base.SATSectorCount) + base.MATSectorCount;
            }
            while (count > num4);
            base.sectorAllocationTableSID = this.FillSAT(base.SATSectorCount, CompoundFileData.SpecialSIDs.SAT);
            if (base.MATSectorCount > 0)
            {
                base.masterAllocationTableSID = this.FillSAT(base.MATSectorCount, CompoundFileData.SpecialSIDs.MSAT);
            }
            else
            {
                base.masterAllocationTableSID = -2;
            }
            if (base.sectorAllocationTable.Count != count)
            {
                throw new CompoundFileException("Internal error: wrong SAT / MAT calculation.");
            }
        }

        private void BuildTables()
        {
            CompoundFileData.RedBlackTreeNode node = new CompoundFileData.RedBlackTreeNode();
            node.DirectoryEntry = this.ole2File.Root;
            node.LeftDID = -1;
            node.RightDID = -1;
            node.MembersDID = -1;
            this.nodes.Add(node);
            this.ole2File.Root.VisitAll(new VisitDirectoryEntryHandler(this.ProcessEntry), node);
            if (base.shortSectorAllocationTable.Count > 0)
            {
                base.shortTableSID = CreateChain(base.shortSectorAllocationTable.Count * 4, base.sectorAllocationTable, base.sectorSize);
                base.SSATSectorCount = base.sectorAllocationTable.Count - base.shortTableSID;
                base.shortStreamContainerSID = CreateChain((int) base.shortStreamContainer.Length, base.sectorAllocationTable, base.sectorSize);
                base.shortStreamContainerSize = (int) base.shortStreamContainer.Length;
            }
            else
            {
                base.shortTableSID = -2;
                base.SSATSectorCount = 0;
                base.shortStreamContainerSID = -2;
                base.shortStreamContainerSize = 0;
            }
            base.directoryStreamSID = CreateChain(this.nodes.Count * 0x80, base.sectorAllocationTable, base.sectorSize);
            this.BuildSATAndMAT();
        }

        private static int CreateChain(int streamSize, ArrayList allocationTable, int sectorSize)
        {
            int count = allocationTable.Count;
            int num2 = CompoundFileData.SectorCount(streamSize, sectorSize);
            for (int i = 0; i < (num2 - 1); i++)
            {
                allocationTable.Add(allocationTable.Count + 1);
            }
            allocationTable.Add(-2);
            return count;
        }

        private int FillSAT(int sectorCount, CompoundFileData.SpecialSIDs fill)
        {
            int count = base.sectorAllocationTable.Count;
            for (int i = 0; i < sectorCount; i++)
            {
                base.sectorAllocationTable.Add(fill);
            }
            return count;
        }

        private void InsertNodeToTree(ref int currentDID, string newNodeName, int newNodeDID)
        {
            if (currentDID == -1)
            {
                currentDID = newNodeDID;
            }
            else
            {
                CompoundFileData.RedBlackTreeNode node = (CompoundFileData.RedBlackTreeNode) this.nodes[currentDID];
                if (NodeNameLess(newNodeName, node.DirectoryEntry.Name))
                {
                    this.InsertNodeToTree(ref node.LeftDID, newNodeName, newNodeDID);
                }
                else
                {
                    this.InsertNodeToTree(ref node.RightDID, newNodeName, newNodeDID);
                }
            }
        }

        private static bool NodeNameLess(string nameA, string nameB)
        {
            if (nameA.Length < nameB.Length)
            {
                return true;
            }
            if (nameA.Length > nameB.Length)
            {
                return false;
            }
            return (CultureInfo.InvariantCulture.CompareInfo.Compare(nameA, nameB, CompareOptions.Ordinal) < 0);
        }

        private void ProcessEntry(VisitDirectoryEntryArgs args)
        {
            CompoundFileData.RedBlackTreeNode node = new CompoundFileData.RedBlackTreeNode();
            node.DirectoryEntry = args.CurrentEntry;
            node.LeftDID = -1;
            node.RightDID = -1;
            node.MembersDID = -1;
            int newNodeDID = this.nodes.Add(node);
            Ole2Stream currentEntry = args.CurrentEntry as Ole2Stream;
            if (currentEntry != null)
            {
                if (currentEntry.Size < base.shortThreshold)
                {
                    currentEntry.WriteSID = CreateChain(currentEntry.Size, base.shortSectorAllocationTable, base.shortSectorSize);
                    WriteOle2Stream(currentEntry, base.shortStreamContainer, base.shortSectorSize);
                }
                else
                {
                    currentEntry.WriteSID = CreateChain(currentEntry.Size, base.sectorAllocationTable, base.sectorSize);
                }
            }
            else
            {
                args.LevelTags[args.Level] = node;
            }
            CompoundFileData.RedBlackTreeNode node2 = (CompoundFileData.RedBlackTreeNode) args.LevelTags[args.Level - 1];
            this.InsertNodeToTree(ref node2.MembersDID, node.DirectoryEntry.Name, newNodeDID);
        }

        private void WriteAllocationTable(BinaryWriter bw, ArrayList allocationTable)
        {
            foreach (int num in allocationTable)
            {
                bw.Write(num);
            }
            int streamSize = allocationTable.Count * 4;
            int num3 = CompoundFileData.Padding(streamSize, base.sectorSize) / 4;
            for (int i = 0; i < num3; i++)
            {
                bw.Write(-1);
            }
        }

        private static void WriteBytes(byte[] data, int size, Stream outStream, int sectorSize)
        {
            outStream.Write(data, 0, size);
            int num = CompoundFileData.Padding(size, sectorSize);
            for (int i = 0; i < num; i++)
            {
                outStream.WriteByte(0);
            }
        }

        private void WriteDirectoryNode(BinaryWriter bw, CompoundFileData.RedBlackTreeNode node)
        {
            CompoundFileData.DirectoryEntryType rootStorage;
            int shortStreamContainerSID;
            int shortStreamContainerSize;
            Ole2DirectoryEntry directoryEntry = node.DirectoryEntry;
            if (directoryEntry == this.ole2File.root)
            {
                rootStorage = CompoundFileData.DirectoryEntryType.RootStorage;
                shortStreamContainerSID = base.shortStreamContainerSID;
                shortStreamContainerSize = base.shortStreamContainerSize;
            }
            else if (directoryEntry is Ole2Storage)
            {
                rootStorage = CompoundFileData.DirectoryEntryType.UserStorage;
                shortStreamContainerSID = 0;
                shortStreamContainerSize = 0;
            }
            else
            {
                if (!(directoryEntry is Ole2Stream))
                {
                    throw new CompoundFileException("Internal error: unrecognized entry type.");
                }
                Ole2Stream stream = (Ole2Stream) node.DirectoryEntry;
                rootStorage = CompoundFileData.DirectoryEntryType.UserStream;
                shortStreamContainerSID = stream.WriteSID;
                shortStreamContainerSize = stream.Size;
            }
            string name = directoryEntry.Name;
            foreach (char ch in name)
            {
                bw.Write((ushort) ch);
            }
            int num3 = 0x20 - name.Length;
            for (int i = 0; i < num3; i++)
            {
                bw.Write((ushort) 0);
            }
            bw.Write((ushort) ((name.Length + 1) * 2));
            bw.Write((byte) rootStorage);
            bw.Write((byte) 0);
            bw.Write(node.LeftDID);
            bw.Write(node.RightDID);
            bw.Write(node.MembersDID);
            bw.Write(directoryEntry.UniqueIdentifier);
            bw.Write(directoryEntry.UserFlags);
            bw.Write(directoryEntry.TimeStampCreation);
            bw.Write(directoryEntry.TimeStampModification);
            bw.Write(shortStreamContainerSID);
            bw.Write(shortStreamContainerSize);
            bw.Write(directoryEntry.NotUsed);
        }

        private void WriteDirectoryStream(BinaryWriter bw)
        {
            foreach (CompoundFileData.RedBlackTreeNode node in this.nodes)
            {
                this.WriteDirectoryNode(bw, node);
            }
            int streamSize = this.nodes.Count * 0x80;
            int num2 = CompoundFileData.Padding(streamSize, base.sectorSize);
            for (int i = 0; i < num2; i++)
            {
                bw.Write((byte) 0);
            }
        }

        private void WriteEntry(VisitDirectoryEntryArgs args)
        {
            Ole2Stream currentEntry = args.CurrentEntry as Ole2Stream;
            if ((currentEntry != null) && (currentEntry.Size >= base.shortThreshold))
            {
                WriteOle2Stream(currentEntry, this.outputStream, base.sectorSize);
            }
        }

        private void WriteHeader(BinaryWriter bw)
        {
            bw.Write(CompoundFileData.validHeader);
            bw.Write(this.ole2File.UniqueIdentifier);
            bw.Write(this.ole2File.RevisionNumber);
            bw.Write(this.ole2File.VersionNumber);
            bw.Write((ushort) 0xfffe);
            base.sectorSize = 0x200;
            bw.Write((ushort) 9);
            base.shortSectorSize = 0x40;
            bw.Write((ushort) 6);
            bw.Write(this.ole2File.NotUsed1);
            bw.Write(base.SATSectorCount);
            bw.Write(base.directoryStreamSID);
            bw.Write(this.ole2File.NotUsed2);
            bw.Write(base.shortThreshold);
            bw.Write(base.shortTableSID);
            bw.Write(base.SSATSectorCount);
            bw.Write(base.masterAllocationTableSID);
            bw.Write(base.MATSectorCount);
            for (int i = 0; i < 0x6d; i++)
            {
                if (i < base.SATSectorCount)
                {
                    bw.Write((int) (base.sectorAllocationTableSID + i));
                }
                else
                {
                    bw.Write(-1);
                }
            }
        }

        private void WriteMATRemainder(BinaryWriter bw)
        {
            int num = (base.sectorSize / 4) - 1;
            int num2 = base.SATSectorCount - 0x6d;
            int num3 = base.sectorAllocationTableSID + 0x6d;
            for (int i = 0; i < base.MATSectorCount; i++)
            {
                for (int j = 0; j < num; j++)
                {
                    if (num2 > 0)
                    {
                        bw.Write(num3);
                        num3++;
                        num2--;
                    }
                    else
                    {
                        bw.Write(-1);
                    }
                }
                if (num2 > 0)
                {
                    bw.Write((int) ((base.masterAllocationTableSID + i) + 1));
                }
                else
                {
                    bw.Write(-2);
                }
            }
        }

        private static void WriteOle2Stream(Ole2Stream ole2stream, Stream outStream, int sectorSize)
        {
            byte[] data = ole2stream.GetData();
            WriteBytes(data, data.Length, outStream, sectorSize);
        }

        private void WriteShortSectorContainerStream()
        {
            WriteBytes(base.shortStreamContainer.GetBuffer(), (int) base.shortStreamContainer.Length, this.outputStream, base.sectorSize);
        }
    }
}

