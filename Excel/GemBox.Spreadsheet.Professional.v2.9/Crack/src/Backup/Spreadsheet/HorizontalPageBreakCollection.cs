namespace GemBox.Spreadsheet
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Collection of horizontal page breaks (<see cref="T:GemBox.Spreadsheet.HorizontalPageBreak">HorizontalPageBreak</see>).
    /// </summary>
    public class HorizontalPageBreakCollection : PageBreakCollection
    {
        internal HorizontalPageBreakCollection()
        {
        }

        internal HorizontalPageBreakCollection(HorizontalPageBreakCollection source)
        {
            foreach (HorizontalPageBreak @break in source.items)
            {
                base.items.Add(new HorizontalPageBreak(@break.Row, @break.FirstColumn, @break.LastColumn));
            }
        }

        /// <overloads>Ads a new horizontal page break.</overloads>
        /// <summary>
        /// Ads a new horizontal page break above the specified row.
        /// </summary>
        /// <param name="row">The zero-based index of the row.</param>
        public void Add(int row)
        {
            base.Add(new HorizontalPageBreak(row, 0, 0x3fff));
        }

        /// <summary>
        /// Ads a new horizontal page break above the specified row and within specified columns.
        /// </summary>
        /// <param name="row">The zero-based index of the row.</param>
        /// <param name="firstColumn">The zero-based index of the first column.</param>
        /// <param name="lastColumn">The zero-based index of the last column.</param>
        public void Add(int row, int firstColumn, int lastColumn)
        {
            base.Add(new HorizontalPageBreak(row, firstColumn, lastColumn));
        }

        internal override PageBreak InstanceCreator(int breakIndex, int firstLimit, int lastLimit)
        {
            return new HorizontalPageBreak(breakIndex, firstLimit, lastLimit);
        }

        /// <summary>
        /// Gets or sets the horizontal page break at the specified index.
        /// </summary>
        public HorizontalPageBreak this[int index]
        {
            get
            {
                return (HorizontalPageBreak) base.items[index];
            }
            set
            {
                base.items[index] = value;
            }
        }
    }
}

