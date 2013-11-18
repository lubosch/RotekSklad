namespace GemBox.Spreadsheet
{
    using System;

    /// <summary>
    /// Represents a named range in the worksheet.
    /// </summary>
    public class NamedRange
    {
        private bool globalName;
        private int index;
        private string name;
        private object[] options;
        private NamedRangeCollection parent;
        private CellRange range;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GemBox.Spreadsheet.NamedRange" /> class.
        /// </summary>
        /// <param name="parent">Parent collection.</param>
        /// <param name="index">Index in the parrent collection.</param>
        /// <param name="options">The options.</param>
        /// <param name="name">The cell range name.</param>
        /// <param name="range">The named cell range.</param>
        /// <param name="globalName">if name is global (=true)</param>
        internal NamedRange(NamedRangeCollection parent, int index, object[] options, string name, CellRange range, bool globalName)
        {
            this.globalName = globalName;
            this.parent = parent;
            this.index = index;
            this.options = options;
            this.name = name;
            this.range = range;
        }

        /// <summary>
        /// Deletes this named range from the named ranges collection.
        /// </summary>
        public void Delete()
        {
            this.parent.DeleteInternal(this.index);
        }

        /// <summary>
        /// gets global flag
        /// </summary>
        /// <value>if this name is global</value>
        public bool GlobalName
        {
            get
            {
                return this.globalName;
            }
        }

        /// <summary>
        /// Gets the named range name.
        /// </summary>
        /// <value>The named range name.</value>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <value>The options.</value>
        internal object[] Options
        {
            get
            {
                return this.options;
            }
        }

        /// <summary>
        /// Gets the named cell range.
        /// </summary>
        /// <value>The named cell range.</value>
        public CellRange Range
        {
            get
            {
                return this.range;
            }
        }
    }
}

