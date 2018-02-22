namespace GemBox.Spreadsheet
{
    using System;

    /// <summary>
    /// Stores MS Excel display and view related options.
    /// </summary>
    public sealed class ExcelViewOptions
    {
        private ExcelWorksheet parent;

        internal ExcelViewOptions(ExcelWorksheet parent)
        {
            this.parent = parent;
        }

        /// <summary>
        /// Index of the first visible column in the worksheet.
        /// </summary>
        /// <remarks>
        /// Default value for this property is 0.
        /// </remarks>
        public int FirstVisibleColumn
        {
            get
            {
                return this.parent.firstVisibleColumn;
            }
            set
            {
                this.parent.firstVisibleColumn = value;
            }
        }

        /// <summary>
        /// Index of the first visible row in the worksheet.
        /// </summary>
        /// <remarks>
        /// Default value for this property is 0.
        /// </remarks>
        public int FirstVisibleRow
        {
            get
            {
                return this.parent.firstVisibleRow;
            }
            set
            {
                this.parent.firstVisibleRow = value;
            }
        }

        /// <summary>
        /// Gets or sets whether outline column buttons are displayed on the right side of groups.
        /// </summary>
        /// <remarks>
        /// This property is simply written to Excel file and has no effect on behavior of this library.
        /// For more information on worksheet protection, consult Microsoft Excel documentation.
        /// </remarks>
        /// <example> Following code creates two horizontal groups and one vertical group. Horizontal groups have 
        /// outline button above (default is below), while vertical group is collapsed.
        /// <code lang="Visual Basic">
        /// Sub GroupingSample(ByVal ws As ExcelWorksheet)
        /// ws.Cells(0, 0).Value = "Grouping and outline example:"
        /// 
        /// <font color="Green">' Vertical grouping.</font>
        /// ws.Cells(2, 0).Value = "GroupA Start"
        /// ws.Rows(2).OutlineLevel = 1
        /// ws.Cells(3, 0).Value = "A"
        /// ws.Rows(3).OutlineLevel = 1
        /// ws.Cells(4, 1).Value = "GroupB Start"
        /// ws.Rows(4).OutlineLevel = 2
        /// ws.Cells(5, 1).Value = "B"
        /// ws.Rows(5).OutlineLevel = 2
        /// ws.Cells(6, 1).Value = "GroupB End"
        /// ws.Rows(6).OutlineLevel = 2
        /// ws.Cells(7, 0).Value = "GroupA End"
        /// ws.Rows(7).OutlineLevel = 1
        /// <font color="Green">' Put outline row buttons above groups.</font>
        /// ws.ViewOptions.OutlineRowButtonsBelow = False
        /// 
        /// <font color="Green">' Horizontal grouping (collapsed).</font>
        /// ws.Cells("E2").Value = "Gr.C Start"
        /// ws.Columns("E").OutlineLevel = 1
        /// ws.Columns("E").Collapsed = True
        /// ws.Cells("F2").Value = "C"
        /// ws.Columns("F").OutlineLevel = 1
        /// ws.Columns("F").Collapsed = True
        /// ws.Cells("G2").Value = "Gr.C End"
        /// ws.Columns("G").OutlineLevel = 1
        /// ws.Columns("G").Collapsed = True
        /// End Sub
        /// </code>
        /// <code lang="C#">
        /// static void GroupingSample(ExcelWorksheet ws)
        /// {
        /// ws.Cells[0,0].Value = "Grouping and outline example:";
        /// 
        /// <font color="Green">// Vertical grouping.</font>
        /// ws.Cells[2,0].Value = "GroupA Start";
        /// ws.Rows[2].OutlineLevel = 1;
        /// ws.Cells[3,0].Value = "A";
        /// ws.Rows[3].OutlineLevel = 1;
        /// ws.Cells[4,1].Value = "GroupB Start";
        /// ws.Rows[4].OutlineLevel = 2;
        /// ws.Cells[5,1].Value = "B";
        /// ws.Rows[5].OutlineLevel = 2;
        /// ws.Cells[6,1].Value = "GroupB End";
        /// ws.Rows[6].OutlineLevel = 2;
        /// ws.Cells[7,0].Value = "GroupA End";
        /// ws.Rows[7].OutlineLevel = 1;
        /// <font color="Green">// Put outline row buttons above groups.</font>
        /// ws.ViewOptions.OutlineRowButtonsBelow = false;
        /// 
        /// <font color="Green">// Horizontal grouping (collapsed).</font>
        /// ws.Cells["E2"].Value = "Gr.C Start";
        /// ws.Columns["E"].OutlineLevel = 1;
        /// ws.Columns["E"].Collapsed = true;
        /// ws.Cells["F2"].Value = "C";
        /// ws.Columns["F"].OutlineLevel = 1;
        /// ws.Columns["F"].Collapsed = true;
        /// ws.Cells["G2"].Value = "Gr.C End";
        /// ws.Columns["G"].OutlineLevel = 1;
        /// ws.Columns["G"].Collapsed = true;
        /// }
        /// </code>
        /// </example>
        /// <seealso cref="P:GemBox.Spreadsheet.ExcelViewOptions.OutlineRowButtonsBelow">ExcelWorksheet.OutlineRowButtonsBelow</seealso>
        /// <seealso cref="P:GemBox.Spreadsheet.ExcelColumnRowBase.Collapsed" />
        /// <seealso cref="P:GemBox.Spreadsheet.ExcelColumnRowBase.OutlineLevel" />
        public bool OutlineColumnButtonsRight
        {
            get
            {
                return this.parent.GetWSBoolOption(WSBoolOptions.ColGroupRight);
            }
            set
            {
                this.parent.SetWSBoolOption(value, WSBoolOptions.ColGroupRight);
            }
        }

        /// <summary>
        /// Gets or sets whether outline row buttons are displayed below groups.
        /// </summary>
        /// <remarks>
        /// This property is simply written to Excel file and has no effect on behavior of this library.
        /// For more information on worksheet protection, consult Microsoft Excel documentation.
        /// </remarks>
        /// <example> Following code creates two horizontal groups and one vertical group. Horizontal groups have 
        /// outline button above (default is below), while vertical group is collapsed.
        /// <code lang="Visual Basic">
        /// Sub GroupingSample(ByVal ws As ExcelWorksheet)
        /// ws.Cells(0, 0).Value = "Grouping and outline example:"
        /// 
        /// <font color="Green">' Vertical grouping.</font>
        /// ws.Cells(2, 0).Value = "GroupA Start"
        /// ws.Rows(2).OutlineLevel = 1
        /// ws.Cells(3, 0).Value = "A"
        /// ws.Rows(3).OutlineLevel = 1
        /// ws.Cells(4, 1).Value = "GroupB Start"
        /// ws.Rows(4).OutlineLevel = 2
        /// ws.Cells(5, 1).Value = "B"
        /// ws.Rows(5).OutlineLevel = 2
        /// ws.Cells(6, 1).Value = "GroupB End"
        /// ws.Rows(6).OutlineLevel = 2
        /// ws.Cells(7, 0).Value = "GroupA End"
        /// ws.Rows(7).OutlineLevel = 1
        /// <font color="Green">' Put outline row buttons above groups.</font>
        /// ws.ViewOptions.OutlineRowButtonsBelow = False
        /// 
        /// <font color="Green">' Horizontal grouping (collapsed).</font>
        /// ws.Cells("E2").Value = "Gr.C Start"
        /// ws.Columns("E").OutlineLevel = 1
        /// ws.Columns("E").Collapsed = True
        /// ws.Cells("F2").Value = "C"
        /// ws.Columns("F").OutlineLevel = 1
        /// ws.Columns("F").Collapsed = True
        /// ws.Cells("G2").Value = "Gr.C End"
        /// ws.Columns("G").OutlineLevel = 1
        /// ws.Columns("G").Collapsed = True
        /// End Sub
        /// </code>
        /// <code lang="C#">
        /// static void GroupingSample(ExcelWorksheet ws)
        /// {
        /// ws.Cells[0,0].Value = "Grouping and outline example:";
        /// 
        /// <font color="Green">// Vertical grouping.</font>
        /// ws.Cells[2,0].Value = "GroupA Start";
        /// ws.Rows[2].OutlineLevel = 1;
        /// ws.Cells[3,0].Value = "A";
        /// ws.Rows[3].OutlineLevel = 1;
        /// ws.Cells[4,1].Value = "GroupB Start";
        /// ws.Rows[4].OutlineLevel = 2;
        /// ws.Cells[5,1].Value = "B";
        /// ws.Rows[5].OutlineLevel = 2;
        /// ws.Cells[6,1].Value = "GroupB End";
        /// ws.Rows[6].OutlineLevel = 2;
        /// ws.Cells[7,0].Value = "GroupA End";
        /// ws.Rows[7].OutlineLevel = 1;
        /// <font color="Green">// Put outline row buttons above groups.</font>
        /// ws.ViewOptions.OutlineRowButtonsBelow = false;
        /// 
        /// <font color="Green">// Horizontal grouping (collapsed).</font>
        /// ws.Cells["E2"].Value = "Gr.C Start";
        /// ws.Columns["E"].OutlineLevel = 1;
        /// ws.Columns["E"].Collapsed = true;
        /// ws.Cells["F2"].Value = "C";
        /// ws.Columns["F"].OutlineLevel = 1;
        /// ws.Columns["F"].Collapsed = true;
        /// ws.Cells["G2"].Value = "Gr.C End";
        /// ws.Columns["G"].OutlineLevel = 1;
        /// ws.Columns["G"].Collapsed = true;
        /// }
        /// </code>
        /// </example>
        /// <seealso cref="P:GemBox.Spreadsheet.ExcelViewOptions.OutlineColumnButtonsRight">ExcelWorksheet.OutlineColumnButtonsRight</seealso>
        /// <seealso cref="P:GemBox.Spreadsheet.ExcelColumnRowBase.Collapsed" />
        /// <seealso cref="P:GemBox.Spreadsheet.ExcelColumnRowBase.OutlineLevel" />
        public bool OutlineRowButtonsBelow
        {
            get
            {
                return this.parent.GetWSBoolOption(WSBoolOptions.RowGroupBelow);
            }
            set
            {
                this.parent.SetWSBoolOption(value, WSBoolOptions.RowGroupBelow);
            }
        }

        /// <summary>
        /// Magnification factor in page break view.
        /// </summary>
        /// <remarks>
        /// <p>Unit is one percent. Value must be between 10 and 400.</p>
        /// <p>Default value for this property is 60.</p>
        /// </remarks>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Thrown if value is out of 10 to 400 range.</exception>
        public int PageBreakViewZoom
        {
            get
            {
                return this.parent.pageBreakViewZoom;
            }
            set
            {
                if ((value < 10) || (value > 400))
                {
                    throw new ArgumentOutOfRangeException("value", value, "PageBreakViewZoom must be in range from 10 to 400.");
                }
                this.parent.pageBreakViewZoom = value;
            }
        }

        /// <summary>
        /// If true, MS Excel shows columns from right to left.
        /// </summary>
        /// <remarks>
        /// Default value for this property is <b>false</b>.
        /// </remarks>
        public bool ShowColumnsFromRightToLeft
        {
            get
            {
                return this.parent.GetWindowOption(WorksheetWindowOptions.ColumnsFromRightToLeft);
            }
            set
            {
                this.parent.SetWindowOption(value, WorksheetWindowOptions.ColumnsFromRightToLeft);
            }
        }

        /// <summary>
        /// If true, MS Excel shows formulas. Otherwise, formula results are shown. 
        /// </summary>
        /// <remarks>
        /// Default value for this property is <b>false</b>.
        /// </remarks>
        public bool ShowFormulas
        {
            get
            {
                return this.parent.GetWindowOption(WorksheetWindowOptions.ShowFormulas);
            }
            set
            {
                this.parent.SetWindowOption(value, WorksheetWindowOptions.ShowFormulas);
            }
        }

        /// <summary>
        /// If true, MS Excel shows grid lines.
        /// </summary>
        /// <remarks>
        /// Default value for this property is <b>true</b>.
        /// </remarks>
        public bool ShowGridLines
        {
            get
            {
                return this.parent.GetWindowOption(WorksheetWindowOptions.ShowGridLines);
            }
            set
            {
                this.parent.SetWindowOption(value, WorksheetWindowOptions.ShowGridLines);
            }
        }

        /// <summary>
        /// If true, MS Excel shows worksheet in page break preview. Otherwise, normal view is used.
        /// </summary>
        /// <remarks>
        /// Default value for this property is <b>false</b>.
        /// </remarks>
        public bool ShowInPageBreakPreview
        {
            get
            {
                return this.parent.GetWindowOption(WorksheetWindowOptions.ShowInPageBreakPreview);
            }
            set
            {
                this.parent.SetWindowOption(value, WorksheetWindowOptions.ShowInPageBreakPreview);
            }
        }

        /// <summary>
        /// If true, MS Excel shows outline symbols.
        /// </summary>
        /// <remarks>
        /// Default value for this property is <b>true</b>.
        /// </remarks>
        public bool ShowOutlineSymbols
        {
            get
            {
                return this.parent.GetWindowOption(WorksheetWindowOptions.ShowOutlineSymbols);
            }
            set
            {
                this.parent.SetWindowOption(value, WorksheetWindowOptions.ShowOutlineSymbols);
            }
        }

        /// <summary>
        /// If true, MS Excel shows row and column headers.
        /// </summary>
        /// <remarks>
        /// Default value for this property is <b>true</b>.
        /// </remarks>
        public bool ShowSheetHeaders
        {
            get
            {
                return this.parent.GetWindowOption(WorksheetWindowOptions.ShowSheetHeaders);
            }
            set
            {
                this.parent.SetWindowOption(value, WorksheetWindowOptions.ShowSheetHeaders);
            }
        }

        /// <summary>
        /// If true, MS Excel shows zero values. Otherwise, zero values are shown as empty cells.
        /// </summary>
        /// <remarks>
        /// Default value for this property is <b>true</b>.
        /// </remarks>
        public bool ShowZeroValues
        {
            get
            {
                return this.parent.GetWindowOption(WorksheetWindowOptions.ShowZeroValues);
            }
            set
            {
                this.parent.SetWindowOption(value, WorksheetWindowOptions.ShowZeroValues);
            }
        }

        /// <summary>
        /// Magnification factor in normal view.
        /// </summary>
        /// <remarks>
        /// <p>Unit is one percent. Value must be between 10 and 400.</p>
        /// <p>Default value for this property is 100.</p>
        /// </remarks>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Thrown if value is out of 10 to 400 range.</exception>
        public int Zoom
        {
            get
            {
                return this.parent.zoom;
            }
            set
            {
                if ((value < 10) || (value > 400))
                {
                    throw new ArgumentOutOfRangeException("value", value, "Zoom must be in range from 10 to 400.");
                }
                this.parent.zoom = value;
            }
        }
    }
}

