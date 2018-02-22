namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;
    using System.Reflection;

    /// <summary>
    /// Collection of the descriptive names which are used 
    /// to represent cells, ranges of cells, formulas, or constant values.
    /// </summary>		
    /// <remarks>
    /// You can use the labels of columns and rows on a worksheet to refer to the cells within 
    /// those columns and rows. Or you can create descriptive names to represent cells, ranges of cells, 
    /// formulas, or constant values. Labels can be used in formulas that refer to data on the same 
    /// worksheet; if you want to represent a range on another worksheet, use a name.
    /// You can also create 3-D names that represent the same cell or range of cells across multiple worksheets.		
    /// </remarks>
    /// <example>Following code demonstrates how to use formulas and named ranges. It shows next features: 
    /// cell references (both absolute and relative), unary and binary operators, constand operands (integer and floating point),
    /// functions and named cell ranges.
    /// <code lang="Visual Basic">
    /// ws.Cells("A1").Value = 5
    /// ws.Cells("A2").Value = 6
    /// ws.Cells("A3").Value = 10
    /// 
    /// ws.Cells("C1").Formula = "=A1+A2"
    /// ws.Cells("C2").Formula = "=$A$1-A3"
    /// ws.Cells("C3").Formula = "=COUNT(A1:A3)"
    /// ws.Cells("C4").Formula = "=AVERAGE($A$1:$A$3)"
    /// ws.Cells("C5").Formula = "=SUM(A1:A3,2,3)"
    /// ws.Cells("C7").Formula = "= 123 - (-(-(23.5)))"
    /// 
    /// ws.NamedRanges.Add("DataRange", ws.Cells.GetSubrange("A1", "A3"))
    /// ws.Cells("C8").Formula = "=MAX(DataRange)"
    /// 
    /// Dim cr As CellRange = ws.Cells.GetSubrange("B9","C10")
    /// cr.Merged = True
    /// cr.Formula = "=A1*25"
    /// </code>
    /// <code lang="C#">	
    /// ws.Cells["A1"].Value = 5;
    /// ws.Cells["A2"].Value = 6;
    /// ws.Cells["A3"].Value = 10;
    /// 
    /// ws.Cells["C1"].Formula = "=A1+A2";
    /// ws.Cells["C2"].Formula = "=$A$1-A3";
    /// ws.Cells["C3"].Formula = "=COUNT(A1:A3)";
    /// ws.Cells["C4"].Formula = "=AVERAGE($A$1:$A$3)";
    /// ws.Cells["C5"].Formula = "=SUM(A1:A3,2,3)";
    /// ws.Cells["C7"].Formula = "= 123 - (-(-(23.5)))";
    /// 
    /// ws.NamedRanges.Add("DataRange", ws.Cells.GetSubrange("A1", "A3"));
    /// ws.Cells["C8"].Formula = "=MAX(DataRange)";
    /// 
    /// CellRange cr = ws.Cells.GetSubrange("B9", "C10");
    /// cr.Merged = true;
    /// cr.Formula = "=A1*25";
    /// </code>
    /// </example>
    /// <seealso cref="P:GemBox.Spreadsheet.ExcelCell.Formula">ExcelCell.Formula</seealso>
    public class NamedRangeCollection : IEnumerable
    {
        private ArrayList namedRanges;
        /// <summary>
        /// The user-defined names list.
        /// </summary>
        private ArrayList namesList;
        private ExcelWorksheet worksheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GemBox.Spreadsheet.NamedRangeCollection" /> class.
        /// </summary>
        /// <param name="worksheet">The worksheet to initialize NamedRangeCollection.</param>
        internal NamedRangeCollection(ExcelWorksheet worksheet)
        {
            this.namesList = new ArrayList();
            this.namedRanges = new ArrayList();
            this.worksheet = worksheet;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GemBox.Spreadsheet.NamedRangeCollection" /> class.
        /// </summary>
        /// <param name="worksheet">The worksheet to initialize NamedRangeCollection.</param>
        /// <param name="sourceNamedRanges">The source named range collection to initialize NamedRangeCollection.</param>
        internal NamedRangeCollection(ExcelWorksheet worksheet, NamedRangeCollection sourceNamedRanges)
        {
            this.namesList = new ArrayList();
            this.namedRanges = new ArrayList();
            this.worksheet = worksheet;
            for (int i = 0; i < sourceNamedRanges.NamedRanges.Count; i++)
            {
                NamedRange range = sourceNamedRanges.NamedRanges[i] as NamedRange;
                this.Add(range.Options, range.Name, range.Range, range.GlobalName);
            }
        }

        /// <overloads>Adds a new global or local named range.</overloads>
        /// <summary>
        /// Adds a new named range. Named ranges are used to represent cells, ranges of cells,
        /// formulas or constant values.
        /// </summary>		
        /// <param name="name">The user-defined name.</param>
        /// <param name="range">The range to be refered by name.</param>
        /// <remarks>
        /// You can use the labels of columns and rows on a worksheet to refer to the cells within
        /// those columns and rows. Or you can create descriptive names to represent cells, ranges of cells,
        /// formulas, or constant values. Labels can be used in formulas that refer to data on the same
        /// worksheet; if you want to represent a range on another worksheet, use a name.
        /// You can also create 3-D names that represent the same cell or range of cells across multiple worksheets.
        /// </remarks>
        /// <example>Following code demonstrates how to use formulas and named ranges. It shows next features: 
        /// cell references (both absolute and relative), unary and binary operators, constand operands (integer and floating point),
        /// functions and named cell ranges.
        /// <code lang="Visual Basic">
        /// ws.Cells("A1").Value = 5
        /// ws.Cells("A2").Value = 6
        /// ws.Cells("A3").Value = 10
        /// 
        /// ws.Cells("C1").Formula = "=A1+A2"
        /// ws.Cells("C2").Formula = "=$A$1-A3"
        /// ws.Cells("C3").Formula = "=COUNT(A1:A3)"
        /// ws.Cells("C4").Formula = "=AVERAGE($A$1:$A$3)"
        /// ws.Cells("C5").Formula = "=SUM(A1:A3,2,3)"
        /// ws.Cells("C7").Formula = "= 123 - (-(-(23.5)))"
        /// 
        /// ws.NamedRanges.Add("DataRange", ws.Cells.GetSubrange("A1", "A3"))
        /// ws.Cells("C8").Formula = "=MAX(DataRange)"
        /// 
        /// Dim cr As CellRange = ws.Cells.GetSubrange("B9","C10")
        /// cr.Merged = True
        /// cr.Formula = "=A1*25"
        /// </code>
        /// <code lang="C#">	
        /// ws.Cells["A1"].Value = 5;
        /// ws.Cells["A2"].Value = 6;
        /// ws.Cells["A3"].Value = 10;
        /// 
        /// ws.Cells["C1"].Formula = "=A1+A2";
        /// ws.Cells["C2"].Formula = "=$A$1-A3";
        /// ws.Cells["C3"].Formula = "=COUNT(A1:A3)";
        /// ws.Cells["C4"].Formula = "=AVERAGE($A$1:$A$3)";
        /// ws.Cells["C5"].Formula = "=SUM(A1:A3,2,3)";
        /// ws.Cells["C7"].Formula = "= 123 - (-(-(23.5)))";
        /// 
        /// ws.NamedRanges.Add("DataRange", ws.Cells.GetSubrange("A1", "A3"));
        /// ws.Cells["C8"].Formula = "=MAX(DataRange)";
        /// 
        /// CellRange cr = ws.Cells.GetSubrange("B9", "C10");
        /// cr.Merged = true;
        /// cr.Formula = "=A1*25";
        /// </code>
        /// </example>
        /// <seealso cref="P:GemBox.Spreadsheet.ExcelCell.Formula">ExcelCell.Formula</seealso>
        public void Add(string name, CellRange range)
        {
            this.Add(new object[0], name, range, false);
        }

        /// <summary>
        /// Adds a new named range. Named ranges are used to represent cells, ranges of cells,
        /// formulas or constant values.
        /// </summary>		
        /// <param name="name">The user-defined name.</param>
        /// <param name="range">The range to be refered by name.</param>
        /// <param name="globalName">sets the range as global if set to true</param>
        /// <remarks>
        /// You can use the labels of columns and rows on a worksheet to refer to the cells within
        /// those columns and rows. Or you can create descriptive names to represent cells, ranges of cells,
        /// formulas, or constant values. Labels can be used in formulas that refer to data on the same
        /// worksheet; if you want to represent a range on another worksheet, use a name.
        /// You can also create 3-D names that represent the same cell or range of cells across multiple worksheets.
        /// </remarks>
        /// <example>Following code demonstrates how to use formulas and named ranges. It shows next features: 
        /// cell references (both absolute and relative), unary and binary operators, constand operands (integer and floating point),
        /// functions and named cell ranges.
        /// <code lang="Visual Basic">
        /// ws.Cells("A1").Value = 5
        /// ws.Cells("A2").Value = 6
        /// ws.Cells("A3").Value = 10
        /// 
        /// ws.Cells("C1").Formula = "=A1+A2"
        /// ws.Cells("C2").Formula = "=$A$1-A3"
        /// ws.Cells("C3").Formula = "=COUNT(A1:A3)"
        /// ws.Cells("C4").Formula = "=AVERAGE($A$1:$A$3)"
        /// ws.Cells("C5").Formula = "=SUM(A1:A3,2,3)"
        /// ws.Cells("C7").Formula = "= 123 - (-(-(23.5)))"
        /// 
        /// ws.NamedRanges.Add("DataRange", ws.Cells.GetSubrange("A1", "A3"))
        /// ws.Cells("C8").Formula = "=MAX(DataRange)"
        /// 
        /// Dim cr As CellRange = ws.Cells.GetSubrange("B9","C10")
        /// cr.Merged = True
        /// cr.Formula = "=A1*25"
        /// </code>
        /// <code lang="C#">	
        /// ws.Cells["A1"].Value = 5;
        /// ws.Cells["A2"].Value = 6;
        /// ws.Cells["A3"].Value = 10;
        /// 
        /// ws.Cells["C1"].Formula = "=A1+A2";
        /// ws.Cells["C2"].Formula = "=$A$1-A3";
        /// ws.Cells["C3"].Formula = "=COUNT(A1:A3)";
        /// ws.Cells["C4"].Formula = "=AVERAGE($A$1:$A$3)";
        /// ws.Cells["C5"].Formula = "=SUM(A1:A3,2,3)";
        /// ws.Cells["C7"].Formula = "= 123 - (-(-(23.5)))";
        /// 
        /// ws.NamedRanges.Add("DataRange", ws.Cells.GetSubrange("A1", "A3"));
        /// ws.Cells["C8"].Formula = "=MAX(DataRange)";
        /// 
        /// CellRange cr = ws.Cells.GetSubrange("B9", "C10");
        /// cr.Merged = true;
        /// cr.Formula = "=A1*25";
        /// </code>
        /// </example>
        /// <seealso cref="P:GemBox.Spreadsheet.ExcelCell.Formula">ExcelCell.Formula</seealso>
        public void Add(string name, CellRange range, bool globalName)
        {
            this.Add(new object[0], name, range, globalName);
        }

        internal void Add(object[] options, string name, CellRange range, bool globalName)
        {
            this.namedRanges.Add(new NamedRange(this, this.namedRanges.Count, options, name, range, globalName));
            this.namesList.Add(name);
        }

        /// <summary>
        /// Deletes named range at specified index.
        /// </summary>
        /// <param name="index">The specified index.</param>
        internal void DeleteInternal(int index)
        {
            this.namedRanges.RemoveAt(index);
            this.namesList.RemoveAt(index);
        }

        /// <summary>
        /// Returns an enumerator that can iterate through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" />
        /// that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator GetEnumerator()
        {
            return this.namedRanges.GetEnumerator();
        }

        /// <summary>
        /// Gets the number of named ranges contained in the collection.
        /// </summary>
        public int Count
        {
            get
            {
                return this.namedRanges.Count;
            }
        }

        /// <overloads>Gets the <see cref="T:GemBox.Spreadsheet.NamedRange">NamedRange</see> with 
        /// the specified index or name.</overloads>
        /// <summary>
        /// Gets the <see cref="T:GemBox.Spreadsheet.NamedRange">NamedRange</see> at the specified index.
        /// </summary>
        /// <param name="index">Range index.</param>
        public NamedRange this[int index]
        {
            get
            {
                return (this.namedRanges[index] as NamedRange);
            }
        }

        /// <summary>
        /// Gets the <see cref="T:GemBox.Spreadsheet.NamedRange">NamedRange</see> with the specified name.
        /// </summary>
        /// <param name="name">Range name.</param>
        public NamedRange this[string name]
        {
            get
            {
                for (int i = 0; i < this.namedRanges.Count; i++)
                {
                    NamedRange range = this.namedRanges[i] as NamedRange;
                    if (range.Name == name)
                    {
                        return range;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Gets or sets the named cell name list
        /// </summary>
        internal ArrayList NamedRanges
        {
            get
            {
                return this.namedRanges;
            }
        }

        /// <summary>
        /// Gets the user-defined names. You can use these names as shortcuts for ranges, cells, etc.
        /// </summary>
        /// <value>The user-defined names.</value>
        internal string[] Names
        {
            get
            {
                return (string[]) this.namesList.ToArray(typeof(string));
            }
        }

        /// <summary>
        /// Gets or sets the user-defined names list.
        /// </summary>
        internal ArrayList NamesList
        {
            get
            {
                return this.namesList;
            }
        }
    }
}

