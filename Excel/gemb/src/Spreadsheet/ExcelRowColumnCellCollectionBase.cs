namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;
    using System.ComponentModel;

    /// <summary>
    /// Base class for row, column and cell collections.
    /// </summary>
    public abstract class ExcelRowColumnCellCollectionBase : IEnumerable
    {
        private ArrayList items;
        private ExcelWorksheet parent;

        /// <summary>
        /// Internal.
        /// </summary>
        /// <param name="parent"></param>
        /// <exclude />
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected ExcelRowColumnCellCollectionBase(ExcelWorksheet parent)
        {
            this.parent = parent;
            this.items = new ArrayList();
        }

        /// <summary>
        /// Returns an enumerator for the <see cref="T:GemBox.Spreadsheet.ExcelRowColumnCellCollectionBase">
        /// ExcelRowColumnCellCollectionBase</see>.
        /// </summary>
        public IEnumerator GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        /// <summary>
        /// Gets the number of currently allocated elements (dynamically changes when worksheet is modified).
        /// </summary>
        public int Count
        {
            get
            {
                return this.items.Count;
            }
        }

        /// <summary>
        /// Internal.
        /// </summary>
        /// <exclude />
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected ArrayList Items
        {
            get
            {
                return this.items;
            }
        }

        internal ExcelWorksheet Parent
        {
            get
            {
                return this.parent;
            }
        }
    }
}

