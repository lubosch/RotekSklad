namespace GemBox.Spreadsheet
{
    using System;
    using System.Data;

    /// <summary>
    /// Excel worksheet is a table with additional properties, identified by a unique name.
    /// </summary>
    /// <remarks>
    /// <p>
    /// Worksheet in Microsoft Excel has limited size. 
    /// Number of rows (<see cref="T:GemBox.Spreadsheet.ExcelRow">ExcelRow</see>) is limited 
    /// to <see cref="F:GemBox.Spreadsheet.ExcelFile.MaxRows">ExcelFile.MaxRows</see>. 
    /// Number of columns (<see cref="T:GemBox.Spreadsheet.ExcelColumn">ExcelColumn</see>) is limited 
    /// to <see cref="F:GemBox.Spreadsheet.ExcelFile.MaxColumns">ExcelFile.MaxColumns</see>. 
    /// A specific cell (<see cref="T:GemBox.Spreadsheet.ExcelCell">ExcelCell</see>) can be accessed either trough 
    /// <see cref="P:GemBox.Spreadsheet.ExcelRow.Cells">ExcelRow.Cells</see>, 
    /// <see cref="P:GemBox.Spreadsheet.ExcelColumn.Cells">ExcelColumn.Cells</see> or 
    /// <see cref="P:GemBox.Spreadsheet.ExcelWorksheet.Cells">ExcelWorksheet.Cells</see> property. 
    /// Whichever property used, there are two distinct methods of getting a cell reference; using <b>name</b>
    /// and using <b>index</b>. For example, full name of cell in top left corner of a worksheet is "A1". Translated
    /// to indexes, same cell would be 0,0 (zero row and zero column). If using 
    /// <see cref="P:GemBox.Spreadsheet.ExcelRow.Cells">ExcelRow.Cells</see> or 
    /// <see cref="P:GemBox.Spreadsheet.ExcelColumn.Cells">ExcelColumn.Cells</see> to access a
    /// specific cell, only partial name or partial index must be used, providing unknown column or row information. 
    /// </p>
    /// </remarks>
    /// <example> Look at following code for cell referencing examples:
    /// <code lang="Visual Basic">
    /// Dim ws As ExcelWorksheet = excelFile.Worksheets.ActiveWorksheet
    /// 
    /// ws.Cells("B2").Value = "Cell B2."
    /// ws.Cells(6, 0).Value = "Cell in row 7 and column A."
    /// 
    /// ws.Rows(2).Cells(0).Value = "Cell in row 3 and column A."
    /// ws.Rows("4").Cells("B").Value = "Cell in row 4 and column B."
    /// 
    /// ws.Columns(2).Cells(4).Value = "Cell in column C and row 5."
    /// ws.Columns("AA").Cells("6").Value = "Cell in AA column and row 6."
    /// </code>
    /// <code lang="C#">
    /// ExcelWorksheet ws = excelFile.Worksheets.ActiveWorksheet;
    /// 
    /// ws.Cells["B2"].Value = "Cell B2.";
    /// ws.Cells[6,0].Value = "Cell in row 7 and column A.";
    /// 
    /// ws.Rows[2].Cells[0].Value = "Cell in row 3 and column A.";
    /// ws.Rows["4"].Cells["B"].Value = "Cell in row 4 and column B.";
    /// 
    /// ws.Columns[2].Cells[4].Value = "Cell in column C and row 5.";
    /// ws.Columns["AA"].Cells["6"].Value = "Cell in AA column and row 6.";
    /// </code>
    /// </example>
    /// <seealso cref="T:GemBox.Spreadsheet.ExcelRow" />
    /// <seealso cref="T:GemBox.Spreadsheet.ExcelColumn" />
    /// <seealso cref="T:GemBox.Spreadsheet.ExcelCell" />
    public sealed class ExcelWorksheet
    {
        private CellRange cells;
        private ExcelColumnCollection columns;
        private int defaultColumnWidth;
        internal int firstVisibleColumn;
        internal int firstVisibleRow;
        internal ushort fitWorksheetHeightToPages;
        internal ushort fitWorksheetWidthToPages;
        internal double footerMargin;
        internal double headerMargin;
        internal HorizontalPageBreakCollection horizontalPageBreaks;
        private MergedCellRanges mergedRanges;
        private string name;
        private NamedRangeCollection namedRanges;
        internal ushort numberOfCopies;
        internal int pageBreakViewZoom;
        internal ushort paperSize;
        private ExcelWorksheetCollection parent;
        private ExcelPictureCollection pictures;
        internal PreservedRecords PreservedWorksheetRecords;
        private ExcelPrintOptions printOptions;
        internal ushort printResolution;
        private bool protectedWorksheet;
        private ExcelRowCollection rows;
        internal int scalingFactor;
        internal SetupOptions setupOptions;
        private ExcelShapeCollection shapes;
        internal ushort startPageNumber;
        internal VerticalPageBreakCollection verticalPageBreaks;
        internal ushort verticalPrintResolution;
        private ExcelViewOptions viewOptions;
        internal WorksheetWindowOptions windowOptions;
        internal WSBoolOptions WSBoolOpt;
        internal int zoom;

        internal ExcelWorksheet(string name, ExcelWorksheetCollection parent)
        {
            this.defaultColumnWidth = 0x924;
            this.pageBreakViewZoom = 60;
            this.zoom = 100;
            this.windowOptions = WorksheetWindowOptions.ShowOutlineSymbols | WorksheetWindowOptions.DefaultGridLineColor | WorksheetWindowOptions.ShowGridLines | WorksheetWindowOptions.ShowZeroValues | WorksheetWindowOptions.ShowSheetHeaders;
            this.scalingFactor = 100;
            this.startPageNumber = 1;
            this.setupOptions = SetupOptions.Portrait;
            this.headerMargin = 0.5;
            this.footerMargin = 0.5;
            this.numberOfCopies = 1;
            this.WSBoolOpt = WSBoolOptions.ShowRowOutline | WSBoolOptions.ShowColumnOutline | WSBoolOptions.ColGroupRight | WSBoolOptions.ShowAutoBreaks | WSBoolOptions.RowGroupBelow;
            this.name = name;
            this.parent = parent;
            this.rows = new ExcelRowCollection(this);
            this.columns = new ExcelColumnCollection(this);
            this.mergedRanges = new MergedCellRanges(this);
            this.horizontalPageBreaks = new HorizontalPageBreakCollection();
            this.verticalPageBreaks = new VerticalPageBreakCollection();
            this.pictures = new ExcelPictureCollection(this);
            this.shapes = new ExcelShapeCollection(this);
            this.printOptions = new ExcelPrintOptions(this);
            this.viewOptions = new ExcelViewOptions(this);
        }

        internal ExcelWorksheet(string name, ExcelWorksheetCollection parent, ExcelWorksheet sourceWorksheet)
        {
            this.defaultColumnWidth = 0x924;
            this.pageBreakViewZoom = 60;
            this.zoom = 100;
            this.windowOptions = WorksheetWindowOptions.ShowOutlineSymbols | WorksheetWindowOptions.DefaultGridLineColor | WorksheetWindowOptions.ShowGridLines | WorksheetWindowOptions.ShowZeroValues | WorksheetWindowOptions.ShowSheetHeaders;
            this.scalingFactor = 100;
            this.startPageNumber = 1;
            this.setupOptions = SetupOptions.Portrait;
            this.headerMargin = 0.5;
            this.footerMargin = 0.5;
            this.numberOfCopies = 1;
            this.WSBoolOpt = WSBoolOptions.ShowRowOutline | WSBoolOptions.ShowColumnOutline | WSBoolOptions.ColGroupRight | WSBoolOptions.ShowAutoBreaks | WSBoolOptions.RowGroupBelow;
            this.name = name;
            this.parent = parent;
            this.protectedWorksheet = sourceWorksheet.protectedWorksheet;
            this.rows = new ExcelRowCollection(this, sourceWorksheet.rows);
            this.columns = new ExcelColumnCollection(this, sourceWorksheet.columns);
            this.defaultColumnWidth = sourceWorksheet.defaultColumnWidth;
            this.mergedRanges = new MergedCellRanges(this, sourceWorksheet.mergedRanges);
            this.WSBoolOpt = sourceWorksheet.WSBoolOpt;
            if (sourceWorksheet.PreservedWorksheetRecords != null)
            {
                this.PreservedWorksheetRecords = new PreservedRecords(sourceWorksheet.PreservedWorksheetRecords);
            }
            this.windowOptions = (WorksheetWindowOptions) ((ushort) (((int) sourceWorksheet.windowOptions) & 0xf9ff));
            this.firstVisibleRow = sourceWorksheet.firstVisibleRow;
            this.firstVisibleColumn = sourceWorksheet.firstVisibleColumn;
            this.pageBreakViewZoom = sourceWorksheet.pageBreakViewZoom;
            this.zoom = sourceWorksheet.zoom;
            this.horizontalPageBreaks = new HorizontalPageBreakCollection(sourceWorksheet.horizontalPageBreaks);
            this.verticalPageBreaks = new VerticalPageBreakCollection(sourceWorksheet.verticalPageBreaks);
            this.paperSize = sourceWorksheet.paperSize;
            this.scalingFactor = sourceWorksheet.scalingFactor;
            this.startPageNumber = sourceWorksheet.startPageNumber;
            this.fitWorksheetWidthToPages = sourceWorksheet.fitWorksheetWidthToPages;
            this.fitWorksheetHeightToPages = sourceWorksheet.fitWorksheetHeightToPages;
            this.setupOptions = sourceWorksheet.setupOptions;
            this.printResolution = sourceWorksheet.printResolution;
            this.verticalPrintResolution = sourceWorksheet.verticalPrintResolution;
            this.headerMargin = sourceWorksheet.headerMargin;
            this.footerMargin = sourceWorksheet.footerMargin;
            this.numberOfCopies = sourceWorksheet.numberOfCopies;
            this.namedRanges = new NamedRangeCollection(this, sourceWorksheet.NamedRanges);
            this.pictures = new ExcelPictureCollection(this, sourceWorksheet.Pictures);
            this.shapes = new ExcelShapeCollection(this, sourceWorksheet.Shapes);
            this.printOptions = new ExcelPrintOptions(this);
            this.viewOptions = new ExcelViewOptions(this);
        }

        /// <summary>
        /// Deletes this worksheet from the workbook.
        /// </summary>
        public void Delete()
        {
            this.parent.DeleteInternal(this);
        }

        internal bool GetWindowOption(WorksheetWindowOptions option)
        {
            return Utilities.IsBitSet((ushort) this.windowOptions, (ushort) option);
        }

        internal bool GetWSBoolOption(WSBoolOptions option)
        {
            return Utilities.IsBitSet((ushort) this.WSBoolOpt, (ushort) option);
        }

        /// <summary>
        /// Inserts a copy of an existing worksheet before the current worksheet.
        /// </summary>
        /// <param name="destinationWorksheetName">Name of the new worksheet.</param>
        /// <param name="sourceWorksheet">Source worksheet.</param>
        /// <returns>Newly created worksheet.</returns>
        public ExcelWorksheet InsertCopy(string destinationWorksheetName, ExcelWorksheet sourceWorksheet)
        {
            return this.parent.InsertCopyInternal(destinationWorksheetName, this.parent.IndexOf(this), sourceWorksheet);
        }

        /// <summary>
        /// Inserts a <see cref="T:System.Data.DataTable">DataTable</see> at the specified position in 
        /// the current worksheet.
        /// </summary>
        /// <param name="dataTable">Source DataTable.</param>
        /// <param name="startCell">Name of start (top-left) cell.</param>
        /// <param name="columnHeaders">True to insert column names above data.</param>
        /// <returns>Number of inserted rows.</returns>
        public int InsertDataTable(DataTable dataTable, string startCell, bool columnHeaders)
        {
            int num;
            int num2;
            CellRange.PositionToRowColumn(startCell, out num, out num2);
            return this.InsertDataTable(dataTable, num, num2, columnHeaders);
        }

        /// <summary>
        /// Inserts a <see cref="T:System.Data.DataTable">DataTable</see> at the specified row and column in 
        /// the current worksheet.
        /// </summary>
        /// <param name="dataTable">Source DataTable.</param>
        /// <param name="startRow">Index of the start row.</param>
        /// <param name="startColumn">Index of the start column.</param>
        /// <param name="columnHeaders">True to insert column names above data.</param>
        /// <returns>Number of inserted rows.</returns>
        public int InsertDataTable(DataTable dataTable, int startRow, int startColumn, bool columnHeaders)
        {
            int num2;
            int num3 = 0;
            if (columnHeaders)
            {
                num2 = 0;
                while (num2 < dataTable.Columns.Count)
                {
                    this.Cells[startRow, startColumn + num2].Value = dataTable.Columns[num2].ColumnName;
                    num2++;
                }
                num3++;
            }
            int num = 0;
            while (num < dataTable.Rows.Count)
            {
                for (num2 = 0; num2 < dataTable.Columns.Count; num2++)
                {
                    this.Cells[(startRow + num3) + num, startColumn + num2].Value = dataTable.Rows[num][num2];
                }
                num++;
            }
            return (num + num3);
        }

        /// <summary>
        /// Inserts an empty worksheet before the current worksheet.
        /// </summary>
        /// <param name="worksheetName">Worksheet name.</param>
        /// <returns>Newly created worksheet.</returns>
        public ExcelWorksheet InsertEmpty(string worksheetName)
        {
            return this.parent.InsertInternal(worksheetName, this.parent.IndexOf(this));
        }

        internal void SetWindowOption(bool val, WorksheetWindowOptions option)
        {
            this.windowOptions = (WorksheetWindowOptions) Utilities.SetBit((ushort) this.windowOptions, (ushort) option, val);
        }

        internal void SetWSBoolOption(bool val, WSBoolOptions option)
        {
            this.WSBoolOpt = (WSBoolOptions) Utilities.SetBit((ushort) this.WSBoolOpt, (ushort) option, val);
        }

        /// <summary>
        /// Gets <see cref="T:GemBox.Spreadsheet.CellRange">CellRange</see> with all the cells 
        /// (<see cref="T:GemBox.Spreadsheet.ExcelCell">ExcelCell</see>) 
        /// in the worksheet.
        /// </summary>
        /// <example> Look at following code for cell referencing examples:
        /// <code lang="Visual Basic">
        /// Dim ws As ExcelWorksheet = excelFile.Worksheets.ActiveWorksheet
        /// 
        /// ws.Cells("B2").Value = "Cell B2."
        /// ws.Cells(6, 0).Value = "Cell in row 7 and column A."
        /// 
        /// ws.Rows(2).Cells(0).Value = "Cell in row 3 and column A."
        /// ws.Rows("4").Cells("B").Value = "Cell in row 4 and column B."
        /// 
        /// ws.Columns(2).Cells(4).Value = "Cell in column C and row 5."
        /// ws.Columns("AA").Cells("6").Value = "Cell in AA column and row 6."
        /// </code>
        /// <code lang="C#">
        /// ExcelWorksheet ws = excelFile.Worksheets.ActiveWorksheet;
        /// 
        /// ws.Cells["B2"].Value = "Cell B2.";
        /// ws.Cells[6,0].Value = "Cell in row 7 and column A.";
        /// 
        /// ws.Rows[2].Cells[0].Value = "Cell in row 3 and column A.";
        /// ws.Rows["4"].Cells["B"].Value = "Cell in row 4 and column B.";
        /// 
        /// ws.Columns[2].Cells[4].Value = "Cell in column C and row 5.";
        /// ws.Columns["AA"].Cells["6"].Value = "Cell in AA column and row 6.";
        /// </code>
        /// </example>
        public CellRange Cells
        {
            get
            {
                if (this.cells == null)
                {
                    this.cells = new CellRange(this);
                }
                return this.cells;
            }
        }

        /// <summary>
        /// Gets collection of all columns (<see cref="T:GemBox.Spreadsheet.ExcelColumn">ExcelColumn</see>) in the worksheet.
        /// </summary>
        public ExcelColumnCollection Columns
        {
            get
            {
                return this.columns;
            }
        }

        /// <summary>
        /// Gets or sets default column width.
        /// </summary>
        /// <remarks>
        /// Unit is 1/256th of the width of the zero character in default font. This value is used as width for columns 
        /// which don't have <see cref="P:GemBox.Spreadsheet.ExcelColumn.Width">ExcelColumn.Width</see> property explicitly set.
        /// </remarks>
        /// <seealso cref="P:GemBox.Spreadsheet.ExcelColumn.Width">ExcelColumn.Width</seealso>
        public int DefaultColumnWidth
        {
            get
            {
                return this.defaultColumnWidth;
            }
            set
            {
                this.defaultColumnWidth = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has shape.
        /// </summary>
        /// <value><c>true</c> if this instance has shape; otherwise, <c>false</c>.</value>
        internal bool HasShape
        {
            get
            {
                return (this.Shapes.Count > 0);
            }
        }

        /// <summary>
        /// Gets collection of all horizontal page breaks 
        /// (<see cref="T:GemBox.Spreadsheet.HorizontalPageBreak">HorizontalPageBreak</see>) in the worksheet.
        /// </summary>
        public HorizontalPageBreakCollection HorizontalPageBreaks
        {
            get
            {
                return this.horizontalPageBreaks;
            }
        }

        internal MergedCellRanges MergedRanges
        {
            get
            {
                return this.mergedRanges;
            }
            set
            {
                this.mergedRanges = value;
            }
        }

        /// <summary>
        /// Gets or sets worksheet name. 
        /// </summary>
        /// <remarks>
        /// If not unique (worksheet with that name already exists in 
        /// <see cref="P:GemBox.Spreadsheet.ExcelFile.Worksheets">ExcelFile.Worksheets</see> collection) exception is thrown.
        /// </remarks>
        /// <exception cref="T:System.ArgumentException">Thrown if worksheet name is not unique.</exception>
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.parent.ExceptionIfNotUnique(value);
                this.name = value;
            }
        }

        /// <summary>
        /// Gets <seealso cref="T:GemBox.Spreadsheet.NamedRangeCollection">NamedRangeCollection</seealso> 
        /// containing descriptive names which are used to represent cells, ranges of cells, 
        /// formulas, or constant values.
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
        public NamedRangeCollection NamedRanges
        {
            get
            {
                if (this.namedRanges == null)
                {
                    this.namedRanges = new NamedRangeCollection(this);
                }
                return this.namedRanges;
            }
        }

        internal ExcelWorksheetCollection Parent
        {
            get
            {
                return this.parent;
            }
        }

        internal ExcelFile ParentExcelFile
        {
            get
            {
                return this.Parent.Parent;
            }
        }

        /// <summary>
        /// Gets <seealso cref="T:GemBox.Spreadsheet.ExcelPictureCollection">ExcelPictureCollection</seealso> containing images.
        /// </summary>
        /// <remarks>
        /// You can insert many popular graphics file formats into your worksheets of
        /// different graphics file formats:
        /// Joint Photographic Experts Group (.jpg),
        /// Microsoft Windows Bitmap (.bmp).
        /// 
        /// Graphics file formats such as:
        /// Enhanced Metafile (.emf),
        /// Windows Metafile (.wmf) 
        /// you can insert into your worksheets and save to destination excel file but you cannot read
        /// the excel files which containt such graphics.
        /// </remarks>
        /// <seealso cref="T:GemBox.Spreadsheet.ExcelPicture">ExcelPicture</seealso>
        /// <example>
        /// Following code demonstrates how to use images. It shows next features: 
        /// <list type="number">
        /// <item> bmp, jpeg loading </item>
        /// <item> bmp, jpeg loading with custom coordinates and dimensions </item>
        /// </list>
        /// <code lang="Visual Basic">
        /// sheet.Pictures.Add( "Image.bmp" ) 
        /// sheet.Pictures.Add( "Image.bmp", New Rectangle(10, 50, 100, 100) )
        /// </code>
        /// <code lang="C#">
        /// sheet.Pictures.Add( "Image.bmp" );
        /// sheet.Pictures.Add( "Image.bmp", new Rectangle( 10, 50, 100, 100 ) );
        /// </code>
        /// </example>
        public ExcelPictureCollection Pictures
        {
            get
            {
                return this.pictures;
            }
        }

        /// <summary>
        /// Contains MS Excel print and print related options.
        /// </summary>
        public ExcelPrintOptions PrintOptions
        {
            get
            {
                return this.printOptions;
            }
        }

        /// <summary>
        /// Gets or sets the worksheet protection flag.
        /// </summary>
        /// <remarks>
        /// This property is simply written to Excel file and has no effect on the behavior of this library.
        /// For more information on worksheet protection, consult Microsoft Excel documentation.
        /// </remarks>
        /// <seealso cref="P:GemBox.Spreadsheet.ExcelFile.Protected">ExcelFile.Protected</seealso>
        public bool Protected
        {
            get
            {
                return this.protectedWorksheet;
            }
            set
            {
                this.protectedWorksheet = value;
            }
        }

        /// <summary>
        /// Gets collection of all rows (<see cref="T:GemBox.Spreadsheet.ExcelRow">ExcelRow</see>) in the worksheet.
        /// </summary>
        public ExcelRowCollection Rows
        {
            get
            {
                return this.rows;
            }
        }

        /// <summary>
        /// Gets the shapes.
        /// </summary>
        /// <value>The shapes.</value>
        internal ExcelShapeCollection Shapes
        {
            get
            {
                return this.shapes;
            }
        }

        /// <summary>
        /// Gets collection of all vertical page breaks 
        /// (<see cref="T:GemBox.Spreadsheet.VerticalPageBreak">VerticalPageBreak</see>) in the worksheet.
        /// </summary>
        public VerticalPageBreakCollection VerticalPageBreaks
        {
            get
            {
                return this.verticalPageBreaks;
            }
        }

        /// <summary>
        /// Contains MS Excel display and view related options.
        /// </summary>
        public ExcelViewOptions ViewOptions
        {
            get
            {
                return this.viewOptions;
            }
        }
    }
}

