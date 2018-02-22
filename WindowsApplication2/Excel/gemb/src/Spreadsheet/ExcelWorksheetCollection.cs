namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;
    using System.Reflection;

    /// <summary>
    /// Collection of worksheets (<see cref="T:GemBox.Spreadsheet.ExcelWorksheet">ExcelWorksheet</see>).
    /// </summary>
    /// <seealso cref="T:GemBox.Spreadsheet.ExcelWorksheet" />
    public sealed class ExcelWorksheetCollection : IEnumerable
    {
        private ExcelWorksheet activeWorksheet;
        private ExcelFile parent;
        private ArrayList pictures = new ArrayList();
        private ArrayList sheetIndexes = new ArrayList();
        private ArrayList worksheetArray;

        internal ExcelWorksheetCollection(ExcelFile parent)
        {
            this.parent = parent;
            this.worksheetArray = new ArrayList();
        }

        /// <summary>
        /// Adds an empty worksheet to the end of the collection.
        /// </summary>
        /// <param name="worksheetName">Worksheet name.</param>
        /// <returns>Newly created worksheet.</returns>
        /// <remarks>
        /// If this is the first worksheet added to the collection the 
        /// <see cref="P:GemBox.Spreadsheet.ExcelWorksheetCollection.ActiveWorksheet">ActiveWorksheet</see> is set to this worksheet.
        /// </remarks>
        /// <exception cref="T:System.ArgumentException">Thrown if worksheet name is not unique.</exception>
        public ExcelWorksheet Add(string worksheetName)
        {
            return this.InsertInternal(worksheetName, this.worksheetArray.Count);
        }

        /// <summary>
        /// Adds a copy of an existing worksheet to the end of the collection.
        /// </summary>
        /// <param name="destinationWorksheetName">Name of new worksheet.</param>
        /// <param name="sourceWorksheet">Source worksheet.</param>
        /// <returns>Newly created worksheet.</returns>
        /// <remarks>
        /// If this is the first worksheet added to the collection the 
        /// <see cref="P:GemBox.Spreadsheet.ExcelWorksheetCollection.ActiveWorksheet">ActiveWorksheet</see> is set to this worksheet.
        /// </remarks>
        /// <exception cref="T:System.ArgumentException">Thrown if worksheet name is not unique.</exception>
        public ExcelWorksheet AddCopy(string destinationWorksheetName, ExcelWorksheet sourceWorksheet)
        {
            return this.InsertCopyInternal(destinationWorksheetName, this.worksheetArray.Count, sourceWorksheet);
        }

        internal ushort AddSheetReference(string sheet)
        {
            ushort sheetIndex = this.GetSheetIndex(sheet);
            if (!this.sheetIndexes.Contains(sheetIndex))
            {
                this.sheetIndexes.Add(sheetIndex);
                return (ushort) (this.sheetIndexes.Count - 1);
            }
            for (int i = 0; i < this.sheetIndexes.Count; i++)
            {
                ushort num3 = (ushort) this.sheetIndexes[i];
                if (sheetIndex == num3)
                {
                    return (ushort) i;
                }
            }
            return 0;
        }

        internal void DeleteInternal(ExcelWorksheet ws)
        {
            if (this.activeWorksheet == ws)
            {
                this.activeWorksheet = null;
            }
            ushort sheetIndex = this.GetSheetIndex(ws.Name);
            for (int i = 0; i < this.sheetIndexes.Count; i++)
            {
                ushort num3 = (ushort) this.sheetIndexes[i];
                if (num3 > sheetIndex)
                {
                    this.sheetIndexes[i] = (ushort) (num3 - 1);
                }
            }
            this.worksheetArray.Remove(ws);
        }

        internal void ExceptionIfNotUnique(string worksheetName)
        {
            foreach (ExcelWorksheet worksheet in this.worksheetArray)
            {
                if (worksheet.Name == worksheetName)
                {
                    throw new ArgumentException("Provided worksheet name is not unique.", "worksheetName");
                }
            }
        }

        internal int GetActiveWorksheetIndex()
        {
            if (this.Count == 0)
            {
                throw new Exception("Workbook must contain at least one worksheet. Use ExcelFile.Worksheets.Add() method to create a new worksheet.");
            }
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i] == this.activeWorksheet)
                {
                    return i;
                }
            }
            throw new Exception("Internal: Can't find ActiveWorksheet.");
        }

        /// <summary>
        /// Returns an enumerator for the <see cref="T:GemBox.Spreadsheet.ExcelWorksheetCollection">
        /// ExcelWorksheetCollection</see>.
        /// </summary>
        public IEnumerator GetEnumerator()
        {
            return this.worksheetArray.GetEnumerator();
        }

        internal ushort GetSheetIndex(string sheet)
        {
            ushort num = 0;
            foreach (ExcelWorksheet worksheet in this.worksheetArray)
            {
                if (worksheet.Name == sheet)
                {
                    return num;
                }
                num = (ushort) (num + 1);
            }
            return 0;
        }

        internal int IndexOf(ExcelWorksheet ws)
        {
            return this.worksheetArray.IndexOf(ws);
        }

        internal ExcelWorksheet InsertCopyInternal(string destinationWorksheetName, int position, ExcelWorksheet sourceWorksheet)
        {
            this.ExceptionIfNotUnique(destinationWorksheetName);
            ExcelWorksheet worksheet = new ExcelWorksheet(destinationWorksheetName, this, sourceWorksheet);
            this.worksheetArray.Insert(position, worksheet);
            if (sourceWorksheet.ParentExcelFile != worksheet.ParentExcelFile)
            {
                worksheet.ParentExcelFile.CopyDrawings(sourceWorksheet.ParentExcelFile);
            }
            return worksheet;
        }

        internal ExcelWorksheet InsertInternal(string worksheetName, int position)
        {
            this.ExceptionIfNotUnique(worksheetName);
            ExcelWorksheet worksheet = new ExcelWorksheet(worksheetName, this);
            this.worksheetArray.Insert(position, worksheet);
            return worksheet;
        }

        /// <summary>
        /// Gets or sets active worksheet.
        /// </summary>
        /// <remarks>
        /// Active worksheet is the one selected when file is opened with Microsoft Excel. By default active worksheet 
        /// is the first one added with <see cref="M:GemBox.Spreadsheet.ExcelWorksheetCollection.Add(System.String)">Add</see> method.
        /// </remarks>
        public ExcelWorksheet ActiveWorksheet
        {
            get
            {
                if ((this.activeWorksheet == null) && (this.worksheetArray.Count > 0))
                {
                    this.activeWorksheet = this[0];
                }
                return this.activeWorksheet;
            }
            set
            {
                this.activeWorksheet = value;
            }
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:GemBox.Spreadsheet.ExcelWorksheetCollection">
        /// ExcelWorksheetCollection</see>.
        /// </summary>
        public int Count
        {
            get
            {
                return this.worksheetArray.Count;
            }
        }

        /// <overloads>Gets the worksheet with the specified index or name.</overloads>
        /// <summary>
        /// Gets the worksheet with the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the worksheet.</param>
        public ExcelWorksheet this[int index]
        {
            get
            {
                return (ExcelWorksheet) this.worksheetArray[index];
            }
        }

        /// <summary>
        /// Gets the worksheet with the specified name.
        /// </summary>
        /// <param name="name">The name of the worksheet.</param>
        public ExcelWorksheet this[string name]
        {
            get
            {
                foreach (ExcelWorksheet worksheet in this.worksheetArray)
                {
                    if (worksheet.Name == name)
                    {
                        return worksheet;
                    }
                }
                throw new ArgumentOutOfRangeException("name", name, "No worksheet with specified name.");
            }
        }

        internal ExcelFile Parent
        {
            get
            {
                return this.parent;
            }
        }

        /// <summary>
        /// Get the list of bse records
        /// </summary>
        internal ArrayList Pictures
        {
            get
            {
                return this.pictures;
            }
        }

        /// <summary>
        /// Gets the sheet indexes.
        /// </summary>
        /// <value>The sheet indexes.</value>
        internal ArrayList SheetIndexes
        {
            get
            {
                return this.sheetIndexes;
            }
        }

        /// <summary>
        /// Gets the sheet names.
        /// </summary>
        /// <value>The sheet names.</value>
        internal string[] SheetNames
        {
            get
            {
                string[] strArray = new string[this.worksheetArray.Count];
                int num = 0;
                foreach (ExcelWorksheet worksheet in this)
                {
                    strArray[num++] = worksheet.Name;
                }
                return strArray;
            }
        }
    }
}

