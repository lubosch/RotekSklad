namespace GemBox.Spreadsheet
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Base class for row and column collections.
    /// </summary>
    public abstract class ExcelRowColumnCollectionBase : ExcelRowColumnCellCollectionBase
    {
        internal int MaxOutlineLevel;

        /// <summary>
        /// Internal.
        /// </summary>
        /// <param name="parent"></param>
        /// <exclude />
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected ExcelRowColumnCollectionBase(ExcelWorksheet parent) : base(parent)
        {
        }

        internal void DeleteInternal(int index)
        {
            base.Items.RemoveAt(index);
            this.FixAllIndexes(index, -1);
        }

        internal void FixAllIndexes(int index, int offset)
        {
            this.FixItemIndexes(index, offset);
            this.FixMergedRegionsIndexes(index, offset);
            this.FixPageBreaksIndexes(index, offset);
        }

        private void FixItemIndexes(int index, int offset)
        {
            for (int i = index; i < base.Items.Count; i++)
            {
                ExcelColumnRowBase base1 = (ExcelColumnRowBase) base.Items[i];
                base1.Index += offset;
            }
        }

        internal abstract void FixMergedRegionsIndexes(int index, int offset);
        internal abstract void FixPageBreaksIndexes(int index, int offset);
    }
}

