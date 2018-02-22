namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;

    /// <summary>
    /// Base class for page break collections.
    /// </summary>
    public abstract class PageBreakCollection : IEnumerable
    {
        internal ArrayList items = new ArrayList();

        internal PageBreakCollection()
        {
        }

        internal void Add(PageBreak pb)
        {
            this.items.Add(pb);
        }

        /// <summary>
        /// Removes all page breaks. 
        /// </summary>
        public void Clear()
        {
            this.items.Clear();
        }

        internal object[] GetArgs()
        {
            ArrayList list = new ArrayList();
            foreach (PageBreak @break in this.items)
            {
                list.AddRange(new object[] { (ushort) @break.breakIndex, (ushort) @break.firstLimit, (ushort) @break.lastLimit });
            }
            return new object[] { ((ushort) this.items.Count), ((object[]) list.ToArray(typeof(object))) };
        }

        /// <summary>
        /// Returns an enumerator for the collection.
        /// </summary>
        public IEnumerator GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        internal abstract PageBreak InstanceCreator(int breakIndex, int firstLimit, int lastLimit);
        internal void LoadArgs(object[] args)
        {
            int num = (ushort) args[0];
            object[] objArray = (object[]) args[1];
            for (int i = 0; i < num; i++)
            {
                ushort breakIndex = (ushort) objArray[i * 3];
                ushort firstLimit = (ushort) objArray[(i * 3) + 1];
                ushort lastLimit = (ushort) objArray[(i * 3) + 2];
                this.items.Add(this.InstanceCreator(breakIndex, firstLimit, lastLimit));
            }
        }

        /// <summary>
        /// Removes the page break at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the page break to remove.</param>
        public void RemoveAt(int index)
        {
            this.items.RemoveAt(index);
        }

        /// <summary>
        /// Gets the number of page breaks contained in the collection. 
        /// </summary>
        public int Count
        {
            get
            {
                return this.items.Count;
            }
        }
    }
}

