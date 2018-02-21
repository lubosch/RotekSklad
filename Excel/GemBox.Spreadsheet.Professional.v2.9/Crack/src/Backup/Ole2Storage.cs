namespace GemBox
{
    using System;
    using System.Collections;
    using System.Reflection;

    internal class Ole2Storage : Ole2DirectoryEntry, IEnumerable
    {
        private ArrayList elements;

        internal Ole2Storage(string name) : base(name)
        {
            this.elements = new ArrayList();
        }

        internal void Add(Ole2DirectoryEntry entry)
        {
            this.elements.Add(entry);
        }

        public Ole2Storage AddStorage(string name)
        {
            Ole2Storage storage = new Ole2Storage(name);
            this.elements.Add(storage);
            return storage;
        }

        public Ole2Stream AddStream(string name, byte[] data)
        {
            Ole2Stream stream = new Ole2Stream(name, data);
            this.elements.Add(stream);
            return stream;
        }

        public Ole2Stream AddStream(string name, int size, GetStreamDataHandler getStreamDataMethod)
        {
            Ole2Stream stream = new Ole2Stream(name, size, getStreamDataMethod);
            this.elements.Add(stream);
            return stream;
        }

        public void CacheAllStreams()
        {
            this.VisitAll(new VisitDirectoryEntryHandler(Ole2Storage.CacheStreamData));
        }

        private static void CacheStreamData(VisitDirectoryEntryArgs args)
        {
            Ole2Stream currentEntry = args.CurrentEntry as Ole2Stream;
            if (currentEntry != null)
            {
                currentEntry.LoadData();
            }
        }

        private static void CopyEntry(VisitDirectoryEntryArgs args)
        {
            Ole2Storage storage = (Ole2Storage) args.LevelTags[args.Level - 1];
            Ole2Stream currentEntry = args.CurrentEntry as Ole2Stream;
            if (currentEntry != null)
            {
                SourceStreamHolder holder = new SourceStreamHolder(currentEntry);
                storage.AddStream(currentEntry.Name, currentEntry.Size, new GetStreamDataHandler(holder.CopyStreamData));
            }
            else
            {
                Ole2Storage storage2 = args.CurrentEntry as Ole2Storage;
                args.LevelTags[args.Level] = storage.AddStorage(storage2.Name);
            }
        }

        public IEnumerator GetEnumerator()
        {
            return this.elements.GetEnumerator();
        }

        public void ImportTree(Ole2Storage storage, bool loadOnDemand)
        {
            if (storage == null)
            {
                throw new CompoundFileException("Storage argument can't be null (Nothing).");
            }
            storage.VisitAll(new VisitDirectoryEntryHandler(Ole2Storage.CopyEntry), this);
            if (!loadOnDemand)
            {
                this.CacheAllStreams();
            }
        }

        public void Remove(string name)
        {
            for (int i = 0; i < this.elements.Count; i++)
            {
                if (((Ole2DirectoryEntry) this.elements[i]).Name == name)
                {
                    this.RemoveAt(i);
                    return;
                }
            }
            throw new CompoundFileException("No entry with the specified name.");
        }

        public void RemoveAt(int index)
        {
            this.elements.RemoveAt(index);
        }

        public void VisitAll(VisitDirectoryEntryHandler visitEntryMethod)
        {
            this.VisitAll(visitEntryMethod, null);
        }

        public void VisitAll(VisitDirectoryEntryHandler visitEntryMethod, object rootLevelTag)
        {
            ArrayList levelTags = new ArrayList();
            levelTags.Add(rootLevelTag);
            levelTags.Add(null);
            this.VisitAllHelper(1, visitEntryMethod, levelTags);
        }

        internal bool VisitAllHelper(int level, VisitDirectoryEntryHandler visitEntryMethod, ArrayList levelTags)
        {
            foreach (Ole2DirectoryEntry entry in this)
            {
                VisitDirectoryEntryArgs args = new VisitDirectoryEntryArgs(entry, this, level, true, true, true, ArrayList.FixedSize(levelTags));
                visitEntryMethod(args);
                if (!args.ContinueVisiting)
                {
                    return false;
                }
                if (args.VisitChildren)
                {
                    Ole2Storage storage = entry as Ole2Storage;
                    if (storage != null)
                    {
                        levelTags.Add(null);
                        bool flag = storage.VisitAllHelper(level + 1, visitEntryMethod, levelTags);
                        levelTags.RemoveAt(levelTags.Count - 1);
                        if (!flag)
                        {
                            return false;
                        }
                    }
                }
                if (!args.VisitSiblings)
                {
                    break;
                }
            }
            return true;
        }

        public int Count
        {
            get
            {
                return this.elements.Count;
            }
        }

        public Ole2DirectoryEntry this[string name]
        {
            get
            {
                foreach (Ole2DirectoryEntry entry in this.elements)
                {
                    if (entry.Name == name)
                    {
                        return entry;
                    }
                }
                throw new CompoundFileException("No entry with the specified name.");
            }
        }

        public Ole2DirectoryEntry this[int index]
        {
            get
            {
                return (Ole2DirectoryEntry) this.elements[index];
            }
        }

        private class SourceStreamHolder
        {
            private Ole2Stream sourceStream;

            public SourceStreamHolder(Ole2Stream sourceStream)
            {
                this.sourceStream = sourceStream;
            }

            public byte[] CopyStreamData(Ole2Stream destinationStream)
            {
                return this.sourceStream.GetData();
            }
        }
    }
}

