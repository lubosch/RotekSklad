namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Text;

    internal class XLSFileReader
    {
        private ArrayList blibTypes = new ArrayList();
        private ArrayList cellStylesTable = new ArrayList();
        private Color[] colorsTable;
        internal static object[,] DefaultPalette = new object[,] { 
            { 0, 0, 0, 0 }, { 1, 0xff, 0xff, 0xff }, { 2, 0xff, 0, 0 }, { 3, 0, 0xff, 0 }, { 4, 0, 0, 0xff }, { 5, 0xff, 0xff, 0 }, { 6, 0xff, 0, 0xff }, { 7, 0, 0xff, 0xff }, { 8, 0, 0, 0 }, { 9, 0xff, 0xff, 0xff }, { 10, 0xff, 0, 0 }, { 11, 0, 0xff, 0 }, { 12, 0, 0, 0xff }, { 13, 0xff, 0xff, 0 }, { 14, 0xff, 0, 0xff }, { 15, 0, 0xff, 0xff }, 
            { 0x10, 0x80, 0, 0 }, { 0x11, 0, 0x80, 0 }, { 0x12, 0, 0, 0x80 }, { 0x13, 0x80, 0x80, 0 }, { 20, 0x80, 0, 0x80 }, { 0x15, 0, 0x80, 0x80 }, { 0x16, 0xc0, 0xc0, 0xc0 }, { 0x17, 0x80, 0x80, 0x80 }, { 0x18, 0x99, 0x99, 0xff }, { 0x19, 0x99, 0x33, 0x66 }, { 0x1a, 0xff, 0xff, 0xcc }, { 0x1b, 0xcc, 0xff, 0xff }, { 0x1c, 0x66, 0, 0x66 }, { 0x1d, 0xff, 0x80, 0x80 }, { 30, 0, 0x66, 0xcc }, { 0x1f, 0xcc, 0xcc, 0xff }, 
            { 0x20, 0, 0, 0x80 }, { 0x21, 0xff, 0, 0xff }, { 0x22, 0xff, 0xff, 0 }, { 0x23, 0, 0xff, 0xff }, { 0x24, 0x80, 0, 0x80 }, { 0x25, 0x80, 0, 0 }, { 0x26, 0, 0x80, 0x80 }, { 0x27, 0, 0, 0xff }, { 40, 0, 0xcc, 0xff }, { 0x29, 0xcc, 0xff, 0xff }, { 0x2a, 0xcc, 0xff, 0xcc }, { 0x2b, 0xff, 0xff, 0x99 }, { 0x2c, 0x99, 0xcc, 0xff }, { 0x2d, 0xff, 0x99, 0xcc }, { 0x2e, 0xcc, 0x99, 0xff }, { 0x2f, 0xff, 0xcc, 0x99 }, 
            { 0x30, 0x33, 0x66, 0xff }, { 0x31, 0x33, 0xcc, 0xcc }, { 50, 0x99, 0xcc, 0 }, { 0x33, 0xff, 0xcc, 0 }, { 0x34, 0xff, 0x99, 0 }, { 0x35, 0xff, 0x66, 0 }, { 0x36, 0x66, 0x66, 0x99 }, { 0x37, 150, 150, 150 }, { 0x38, 0, 0x33, 0x66 }, { 0x39, 0x33, 0x99, 0x66 }, { 0x3a, 0, 0x33, 0 }, { 0x3b, 0x33, 0x33, 0 }, { 60, 0x99, 0x33, 0 }, { 0x3d, 0x99, 0x33, 0x66 }, { 0x3e, 0x33, 0x33, 0x99 }, { 0x3f, 0x33, 0x33, 0x33 }, 
            { 0x40, 0, 0, 0 }, { 0x41, 0xff, 0xff, 0xff }
         };
        private string diagnosticsFileName;
        private ExcelFile excelFile;
        private ExcelLongStrings excelStrings;
        private ArrayList fontsTable = new ArrayList();
        private ArrayList imageBoundaries = new ArrayList();
        private ArrayList images = new ArrayList();
        private ArrayList lastCommentTexts = new ArrayList();
        private int nextImage;
        private ArrayList notesArray = new ArrayList();
        private NumberFormatCollection numberFormats = new NumberFormatCollection(true);
        private bool re_set;
        private XlsOptions xlsOptions;

        public XLSFileReader(ExcelFile excelFile, XlsOptions xlsOptions, string diagnosticsFileName)
        {
            this.excelFile = excelFile;
            this.xlsOptions = xlsOptions;
            this.diagnosticsFileName = diagnosticsFileName;
        }

        private void CleanAllIndexes()
        {
            foreach (ExcelFontData data in this.fontsTable)
            {
                if (data != null)
                {
                    data.ColorIndex = -1;
                }
            }
            foreach (CellStyle style in this.cellStylesTable)
            {
                style.Element.Indexes = null;
            }
        }

        private Color ColorIndexToColor(int colorIndex)
        {
            if (colorIndex > (this.colorsTable.Length - 1))
            {
                colorIndex = 0;
            }
            return this.colorsTable[colorIndex];
        }

        private void ConvertColorIndexesToColors()
        {
            if ((this.fontsTable.Count == 0) || (this.cellStylesTable.Count == 0))
            {
                throw new Exception("Internal: fontsTable or cellStylesTable is empty.");
            }
            foreach (ExcelFontData data in this.fontsTable)
            {
                if (data != null)
                {
                    data.Color = this.ColorIndexToColor(data.ColorIndex);
                }
            }
            foreach (CellStyle style in this.cellStylesTable)
            {
                CellStyleData element = style.Element;
                style.Borders[IndividualBorder.Right].LineColor = this.ColorIndexToColor(element.Indexes.BorderColorIndex[3]);
                style.Borders[IndividualBorder.Left].LineColor = this.ColorIndexToColor(element.Indexes.BorderColorIndex[2]);
                style.Borders[IndividualBorder.Bottom].LineColor = this.ColorIndexToColor(element.Indexes.BorderColorIndex[1]);
                style.Borders[IndividualBorder.Top].LineColor = this.ColorIndexToColor(element.Indexes.BorderColorIndex[0]);
                element.BorderColor[4] = this.ColorIndexToColor(element.Indexes.BorderColorIndex[4]);
                element.PatternBackgroundColor = this.ColorIndexToColor(element.Indexes.PatternBackgroundColorIndex);
                element.PatternForegroundColor = this.ColorIndexToColor(element.Indexes.PatternForegroundColorIndex);
            }
        }

        private void FillImagesFromMsoRecords(MsoBaseRecord baseRec)
        {
            if (baseRec is MsoContainerRecord)
            {
                MsoContainerRecord record = (MsoContainerRecord) baseRec;
                for (int i = 0; i < record.Items.Count; i++)
                {
                    this.FillImagesFromMsoRecords(record.Items[i] as MsoBaseRecord);
                }
            }
            else if (baseRec is MsofbtBseRecord)
            {
                MsofbtBseRecord record2 = (MsofbtBseRecord) baseRec;
                if (record2.Picture != null)
                {
                    this.images.Add(record2.Picture.Image);
                    this.blibTypes.Add(record2.BlipType);
                }
            }
        }

        private Rectangle GetImageBoundaryFromMsoRecords(MsoBaseRecord baseRec)
        {
            Rectangle empty = Rectangle.Empty;
            if (baseRec is MsoContainerRecord)
            {
                MsoContainerRecord record = baseRec as MsoContainerRecord;
                for (int i = 0; i < record.Items.Count; i++)
                {
                    empty = this.GetImageBoundaryFromMsoRecords(record.Items[i] as MsoBaseRecord);
                    if (empty != Rectangle.Empty)
                    {
                        return empty;
                    }
                }
                return empty;
            }
            if (baseRec is MsofbtClientAnchorRecord)
            {
                MsofbtClientAnchorRecord record2 = baseRec as MsofbtClientAnchorRecord;
                empty = record2.AbsoluteBoundary;
            }
            return empty;
        }

        public void ImportRecords(AbsXLSRecords records)
        {
            int num = 0;
            int num2 = 0;
            int num4 = 0;
            string[] strArray = null;
            bool flag = false;
            int recordCode = -1;
            int num6 = -1;
            CellFormula formula = null;
            if ((this.xlsOptions & XlsOptions.PreserveGlobalRecords) != XlsOptions.None)
            {
                this.excelFile.PreservedGlobalRecords = new PreservedRecords();
            }
            else
            {
                this.excelFile.PreservedGlobalRecords = null;
            }
            LoadDefaultPalette(ref this.colorsTable);
            foreach (AbsXLSRec rec in records)
            {
                int num3;
                object[] arguments;
                ExcelWorksheet worksheet;
                bool flag2;
                LabelSSTRecord record3;
                RKRecord record4;
                int num9;
                object[] objArray2;
                int num11;
                int num12;
                ushort num13;
                ushort num14;
                object[] objArray3;
                ushort num15;
                int num18;
                string name = rec.Name;
                if (name != "Continue")
                {
                    recordCode = rec.RecordCode;
                }
                switch (name)
                {
                    case "BOF":
                        num++;
                        if ((num == 1) && (num2 > 0))
                        {
                            worksheet = this.excelFile.Worksheets[num2 - 1];
                            if ((this.xlsOptions & XlsOptions.PreserveWorksheetRecords) == XlsOptions.None)
                            {
                                break;
                            }
                            worksheet.PreservedWorksheetRecords = new PreservedRecords();
                        }
                        goto Label_137C;

                    case "EOF":
                        num--;
                        if (num == 0)
                        {
                            if (num2 == 0)
                            {
                                this.ConvertColorIndexesToColors();
                            }
                            if (this.re_set)
                            {
                                this.notesArray.Sort();
                                for (int i = 0; i < this.notesArray.Count; i++)
                                {
                                    TempNote note = (TempNote) this.notesArray[i];
                                    this.excelFile.Worksheets[num2 - 1].Cells[note.row, note.col].Comment.Text = (string) this.lastCommentTexts[i];
                                    this.excelFile.Worksheets[num2 - 1].Cells[note.row, note.col].Comment.IsVisible = note.visiblity;
                                }
                            }
                            this.notesArray.Clear();
                            this.lastCommentTexts.Clear();
                            this.re_set = true;
                            num2++;
                        }
                        goto Label_137C;

                    case "BoundSheet":
                        this.lastCommentTexts = new ArrayList();
                        if ((num == 1) && (num2 == 0))
                        {
                            string str = ((BoundSheetRecord) rec).SheetName.Str;
                            this.excelFile.Worksheets.Add(str);
                        }
                        goto Label_137C;

                    case "SST":
                    {
                        if ((num != 1) || (num2 != 0))
                        {
                            goto Label_137C;
                        }
                        SSTRecord record = (SSTRecord) rec;
                        strArray = new string[record.TotalStringCount];
                        this.excelStrings = record.ExcelStrings;
                        num3 = 0;
                        goto Label_07FF;
                    }
                    case "Palette":
                        if ((num == 1) && (num2 == 0))
                        {
                            this.LoadPalette(rec);
                        }
                        goto Label_137C;

                    case "Format":
                        if ((num == 1) && (num2 == 0))
                        {
                            this.LoadNumberFormat(rec);
                        }
                        goto Label_137C;

                    case "Font":
                        if ((num == 1) && (num2 == 0))
                        {
                            ExcelFontData data = this.LoadFont(rec);
                            this.fontsTable.Add(data);
                            if (this.fontsTable.Count == 4)
                            {
                                this.fontsTable.Add(null);
                            }
                        }
                        goto Label_137C;

                    case "XF":
                        if ((num == 1) && (num2 == 0))
                        {
                            this.cellStylesTable.Add(this.LoadCellStyle(rec));
                        }
                        goto Label_137C;

                    case "HASBASIC":
                        this.excelFile.HasMacroses = true;
                        goto Label_137C;

                    case "DATEMODE":
                        if ((num == 1) && (num2 == 0))
                        {
                            ushort num8 = (ushort) ((XLSRecord) rec).GetArguments()[0];
                            if (num8 == 1)
                            {
                                this.excelFile.Use1904DateSystem = true;
                            }
                        }
                        goto Label_137C;

                    case "WRITEPROT":
                    case "WRITEACCESS":
                    case "FILESHARING":
                    case "CODEPAGE":
                    case "HIDEOBJ":
                    case "PRECISION":
                    case "REFRESHALL":
                    case "BOOKBOOL":
                    case "USESELFS":
                    case "COUNTRY":
                    case "SUPBOOK":
                        if ((((this.xlsOptions & XlsOptions.PreserveGlobalRecords) != XlsOptions.None) && (num == 1)) && (num2 == 0))
                        {
                            this.excelFile.PreservedGlobalRecords.Add((XLSRecord) rec);
                        }
                        goto Label_137C;

                    case "MSODRAWINGGROUP":
                        this.LoadMsoDrawingGroup(rec);
                        goto Label_137C;

                    case "MSODRAWING":
                        this.LoadMsoDrawing(rec, this.excelFile.Worksheets[num2 - 1]);
                        goto Label_137C;

                    case "NAME":
                        this.LoadName(rec);
                        goto Label_137C;

                    case "EXTERNSHEET":
                        this.LoadExternsheet(rec);
                        goto Label_137C;

                    case "WINDOWPROTECT":
                    case "OBJECTPROTECT":
                        if (num == 1)
                        {
                            if (num2 != 0)
                            {
                                goto Label_09C8;
                            }
                            if ((this.xlsOptions & XlsOptions.PreserveGlobalRecords) != XlsOptions.None)
                            {
                                this.excelFile.PreservedGlobalRecords.Add((XLSRecord) rec);
                            }
                        }
                        goto Label_137C;

                    case "NOTE":
                        this.LoadNoteRecord(rec, this.excelFile.Worksheets[num2 - 1]);
                        goto Label_137C;

                    case "Continue":
                    {
                        string[] strArray2;
                        IntPtr ptr;
                        if (num != 1)
                        {
                            goto Label_137C;
                        }
                        ContinueRecord record2 = (ContinueRecord) rec;
                        this.excelStrings = record2.ExcelStrings;
                        if (this.excelStrings == null)
                        {
                            goto Label_0AE0;
                        }
                        if (!flag)
                        {
                            goto Label_0A85;
                        }
                        num4--;
                        (strArray2 = strArray)[(int) (ptr = (IntPtr) num4)] = strArray2[(int) ptr] + ((ExcelLongString) this.excelStrings.Strings[0]).Str;
                        num3 = 1;
                        goto Label_0AAE;
                    }
                    case "Protect":
                        if (num != 1)
                        {
                            goto Label_137C;
                        }
                        if (((ushort) ((XLSRecord) rec).GetArguments()[0]) != 1)
                        {
                            goto Label_0B77;
                        }
                        flag2 = true;
                        goto Label_0B7A;

                    case "ColumnInfo":
                        if ((num == 1) && (num2 > 0))
                        {
                            this.LoadColumnInfo(rec, this.excelFile.Worksheets[num2 - 1]);
                        }
                        goto Label_137C;

                    case "Row":
                        if ((num == 1) && (num2 > 0))
                        {
                            this.LoadRow(rec, this.excelFile.Worksheets[num2 - 1]);
                        }
                        goto Label_137C;

                    case "MergedCells":
                        if ((num == 1) && (num2 > 0))
                        {
                            this.LoadMergedCells(rec, this.excelFile.Worksheets[num2 - 1]);
                        }
                        goto Label_137C;

                    case "WSBool":
                        if ((num == 1) && (num2 > 0))
                        {
                            this.LoadWSBool(rec, this.excelFile.Worksheets[num2 - 1]);
                        }
                        goto Label_137C;

                    case "LabelSST":
                    case "RK":
                    case "Number":
                    case "Blank":
                    case "Formula":
                    case "BoolErr":
                        if ((num == 1) && (num2 > 0))
                        {
                            worksheet = this.excelFile.Worksheets[num2 - 1];
                            switch (name)
                            {
                                case "LabelSST":
                                    goto Label_0D2E;

                                case "RK":
                                    goto Label_0D52;

                                case "Number":
                                    goto Label_0D77;

                                case "Blank":
                                    goto Label_0D9B;

                                case "Formula":
                                    goto Label_0DBC;

                                case "BoolErr":
                                    goto Label_0E1F;
                            }
                            throw new Exception("Internal: missing case in reading code.");
                        }
                        goto Label_137C;

                    case "MulRK":
                    {
                        if ((num != 1) || (num2 <= 0))
                        {
                            goto Label_137C;
                        }
                        worksheet = this.excelFile.Worksheets[num2 - 1];
                        arguments = ((XLSRecord) rec).GetArguments();
                        num9 = (ushort) arguments[0];
                        int num10 = (ushort) arguments[1];
                        objArray2 = (object[]) arguments[2];
                        num11 = (ushort) arguments[3];
                        num12 = num10;
                        num3 = 0;
                        goto Label_0F37;
                    }
                    case "MulBlank":
                        if ((num != 1) || (num2 <= 0))
                        {
                            goto Label_137C;
                        }
                        worksheet = this.excelFile.Worksheets[num2 - 1];
                        arguments = ((XLSRecord) rec).GetArguments();
                        num13 = (ushort) arguments[0];
                        num14 = (ushort) arguments[1];
                        objArray3 = (object[]) arguments[2];
                        num15 = (ushort) arguments[3];
                        num3 = 0;
                        goto Label_0FDC;

                    case "Window2":
                        if ((num == 1) && (num2 > 0))
                        {
                            worksheet = this.excelFile.Worksheets[num2 - 1];
                            Window2Record record5 = (Window2Record) rec;
                            worksheet.windowOptions = record5.window2Options;
                            worksheet.firstVisibleRow = record5.firstRow;
                            worksheet.firstVisibleColumn = record5.firstColumn;
                            int pageBreakViewZoom = record5.pageBreakViewZoom;
                            int zoom = record5.zoom;
                            if (pageBreakViewZoom != 0)
                            {
                                worksheet.pageBreakViewZoom = pageBreakViewZoom;
                            }
                            if (zoom != 0)
                            {
                                worksheet.zoom = zoom;
                            }
                            if (((ushort) (worksheet.windowOptions & WorksheetWindowOptions.SheetSelected)) != 0)
                            {
                                this.excelFile.Worksheets.ActiveWorksheet = this.excelFile.Worksheets[num2 - 1];
                            }
                        }
                        goto Label_137C;

                    case "HORIZONTALPAGEBREAKS":
                    case "VERTICALPAGEBREAKS":
                        if ((num == 1) && (num2 > 0))
                        {
                            worksheet = this.excelFile.Worksheets[num2 - 1];
                            arguments = ((XLSRecord) rec).GetArguments();
                            switch (name)
                            {
                                case "HORIZONTALPAGEBREAKS":
                                    goto Label_1100;

                                case "VERTICALPAGEBREAKS":
                                    goto Label_1113;
                            }
                        }
                        goto Label_137C;

                    case "SCL":
                        if ((num == 1) && (num2 > 0))
                        {
                            worksheet = this.excelFile.Worksheets[num2 - 1];
                            arguments = ((XLSRecord) rec).GetArguments();
                            num18 = (100 * ((ushort) arguments[0])) / ((ushort) arguments[1]);
                            if (!worksheet.ViewOptions.ShowInPageBreakPreview)
                            {
                                goto Label_118B;
                            }
                            worksheet.pageBreakViewZoom = num18;
                        }
                        goto Label_137C;

                    case "SETUP":
                        if ((num == 1) && (num2 > 0))
                        {
                            worksheet = this.excelFile.Worksheets[num2 - 1];
                            arguments = ((XLSRecord) rec).GetArguments();
                            worksheet.paperSize = (ushort) arguments[0];
                            worksheet.scalingFactor = (ushort) arguments[1];
                            worksheet.startPageNumber = (ushort) arguments[2];
                            worksheet.fitWorksheetWidthToPages = (ushort) arguments[3];
                            worksheet.fitWorksheetHeightToPages = (ushort) arguments[4];
                            worksheet.setupOptions = (SetupOptions) arguments[5];
                            worksheet.printResolution = (ushort) arguments[6];
                            worksheet.verticalPrintResolution = (ushort) arguments[7];
                            worksheet.headerMargin = (double) arguments[8];
                            worksheet.footerMargin = (double) arguments[9];
                            worksheet.numberOfCopies = (ushort) arguments[10];
                        }
                        goto Label_137C;

                    case "CALCCOUNT":
                    case "CALCMODE":
                    case "REFMODE":
                    case "DELTA":
                    case "ITERATION":
                    case "SAVERECALC":
                    case "PRINTHEADERS":
                    case "PRINTGRIDLINES":
                    case "GRIDSET":
                    case "HEADER":
                    case "FOOTER":
                    case "HCENTER":
                    case "LEFTMARGIN":
                    case "RIGHTMARGIN":
                    case "TOPMARGIN":
                    case "BOTTOMMARGIN":
                    case "SORT":
                    case "PANE":
                    case "SELECTION":
                    case "STANDARDWIDTH":
                    case "LABELRANGES":
                    case "HLINK":
                    case "QUICKTIP":
                    case "DVAL":
                    case "DV":
                    case "SHEETLAYOUT":
                    case "SHEETPROTECTION":
                    case "RANGEPROTECTION":
                    case "PASSWORD":
                        if ((((this.xlsOptions & XlsOptions.PreserveWorksheetRecords) != XlsOptions.None) && (num == 1)) && (num2 > 0))
                        {
                            worksheet = this.excelFile.Worksheets[num2 - 1];
                            worksheet.PreservedWorksheetRecords.Add((XLSRecord) rec);
                        }
                        goto Label_137C;

                    case "CONDFMT":
                    case "CF":
                        if ((((this.xlsOptions & XlsOptions.PreserveWorksheetRecords) != XlsOptions.None) && (num == 1)) && (num2 > 0))
                        {
                            worksheet = this.excelFile.Worksheets[num2 - 1];
                            worksheet.PreservedWorksheetRecords.Add((XLSRecord) rec, -11);
                        }
                        goto Label_137C;

                    case "SHRFMLA":
                    case "STRING":
                        if ((((this.xlsOptions & XlsOptions.PreserveWorksheetRecords) != XlsOptions.None) && (num == 1)) && (num2 > 0))
                        {
                            if (formula.ExtraFormulaRecords == null)
                            {
                                formula.ExtraFormulaRecords = new ArrayList();
                            }
                            formula.ExtraFormulaRecords.Add((XLSRecord) rec);
                            if (name == "STRING")
                            {
                                arguments = ((XLSRecord) rec).GetArguments();
                                formula.Value = ((ExcelLongString) arguments[0]).Str;
                            }
                        }
                        goto Label_137C;

                    default:
                        goto Label_137C;
                }
                worksheet.PreservedWorksheetRecords = null;
                goto Label_137C;
            Label_07DC:
                strArray[num3] = ((ExcelLongString) this.excelStrings.Strings[num3]).Str;
                num3++;
            Label_07FF:
                if (num3 < this.excelStrings.Strings.Count)
                {
                    goto Label_07DC;
                }
                if (this.excelStrings.CharsRemaining > 0)
                {
                    flag = true;
                }
                num4 = num3;
                goto Label_137C;
            Label_09C8:
                if ((this.xlsOptions & XlsOptions.PreserveWorksheetRecords) != XlsOptions.None)
                {
                    worksheet = this.excelFile.Worksheets[num2 - 1];
                    worksheet.PreservedWorksheetRecords.Add((XLSRecord) rec);
                }
                goto Label_137C;
            Label_0A85:
                num3 = 0;
            Label_0AAE:
                while (num3 < this.excelStrings.Strings.Count)
                {
                    strArray[num3 + num4] = ((ExcelLongString) this.excelStrings.Strings[num3]).Str;
                    num3++;
                }
                if (this.excelStrings.CharsRemaining > 0)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
                num4 += num3;
                goto Label_137C;
            Label_0AE0:
                if (num2 == 0)
                {
                    if ((this.xlsOptions & XlsOptions.PreserveGlobalRecords) != XlsOptions.None)
                    {
                        this.excelFile.PreservedGlobalRecords.Add((XLSRecord) rec, recordCode);
                    }
                }
                else if (((this.xlsOptions & XlsOptions.PreserveWorksheetRecords) != XlsOptions.None) && (num6 == XLSDescriptors.GetByName("TXO").Code))
                {
                    this.LoadTextContinueRecord(rec, this.excelFile.Worksheets[num2 - 1]);
                }
                goto Label_137C;
            Label_0B77:
                flag2 = false;
            Label_0B7A:
                if (num2 == 0)
                {
                    this.excelFile.Protected = flag2;
                }
                else
                {
                    worksheet = this.excelFile.Worksheets[num2 - 1];
                    worksheet.Protected = flag2;
                }
                goto Label_137C;
            Label_0D2E:
                record3 = (LabelSSTRecord) rec;
                CellRecordHeader header = record3.Header;
                object valInternal = strArray[record3.SSTIndex];
                goto Label_0E77;
            Label_0D52:
                record4 = (RKRecord) rec;
                header = record4.Header;
                valInternal = XLSFileWriter.RKValueToObj(record4.Val);
                goto Label_0E77;
            Label_0D77:
                arguments = ((XLSRecord) rec).GetArguments();
                header = (CellRecordHeader) arguments[0];
                valInternal = arguments[1];
                goto Label_0E77;
            Label_0D9B:
                header = (CellRecordHeader) ((XLSRecord) rec).GetArguments()[0];
                valInternal = null;
                goto Label_0E77;
            Label_0DBC:
                arguments = ((XLSRecord) rec).GetArguments();
                header = (CellRecordHeader) arguments[0];
                if ((this.xlsOptions & XlsOptions.PreserveWorksheetRecords) != XlsOptions.None)
                {
                    formula = new CellFormula(this.excelFile.Worksheets[num2 - 1], (object[]) arguments[1], (FormulaOptions) arguments[2], (object[]) arguments[4]);
                    valInternal = formula;
                }
                else
                {
                    valInternal = null;
                }
                goto Label_0E77;
            Label_0E1F:
                arguments = ((XLSRecord) rec).GetArguments();
                header = (CellRecordHeader) arguments[0];
                if (((byte) arguments[2]) == 0)
                {
                    valInternal = CellFormula.DecodeBoolValue((byte) arguments[1]);
                }
                else
                {
                    valInternal = CellFormula.DecodeErrorValue((byte) arguments[1]);
                }
            Label_0E77:
                this.SetCell(worksheet, header.Row, header.Column, header.StyleIndex, valInternal);
                goto Label_137C;
            Label_0F00:
                valInternal = XLSFileWriter.RKValueToObj((uint) objArray2[(num3 * 2) + 1]);
                this.SetCell(worksheet, num9, num12, (ushort) objArray2[num3 * 2], valInternal);
                num3++;
                num12++;
            Label_0F37:
                if (num12 <= num11)
                {
                    goto Label_0F00;
                }
                goto Label_137C;
            Label_0FA3:
                worksheet.Cells[num13, num14].Style = (CellStyle) this.cellStylesTable[(ushort) objArray3[num3]];
                num3++;
                num14 = (ushort) (num14 + 1);
            Label_0FDC:
                if (num14 <= num15)
                {
                    goto Label_0FA3;
                }
                goto Label_137C;
            Label_1100:
                worksheet.HorizontalPageBreaks.LoadArgs(arguments);
                goto Label_137C;
            Label_1113:
                worksheet.VerticalPageBreaks.LoadArgs(arguments);
                goto Label_137C;
            Label_118B:
                worksheet.zoom = num18;
            Label_137C:
                num6 = rec.RecordCode;
            }
            if (this.fontsTable.Count >= 1)
            {
                ExcelFontData data2 = (ExcelFontData) this.fontsTable[0];
                this.excelFile.DefaultFontName = data2.Name;
                this.excelFile.DefaultFontSize = data2.Size;
            }
            this.CleanAllIndexes();
        }

        internal static bool IsDateTime(string numberFormat)
        {
            bool flag = false;
            int num = 0;
            bool flag2 = false;
            StringBuilder builder = null;
            ArrayList list = new ArrayList();
            foreach (char ch in numberFormat)
            {
                switch (ch)
                {
                    case '[':
                        num++;
                        flag2 = false;
                        break;

                    case ']':
                        num--;
                        flag2 = false;
                        break;

                    case '"':
                        flag = !flag;
                        flag2 = false;
                        break;

                    default:
                        if (!flag && (num == 0))
                        {
                            if (char.IsLetter(ch))
                            {
                                if (!flag2)
                                {
                                    flag2 = true;
                                    if (builder != null)
                                    {
                                        list.Add(builder.ToString().ToLower());
                                    }
                                    builder = new StringBuilder();
                                }
                                builder.Append(ch);
                            }
                            else
                            {
                                flag2 = false;
                            }
                        }
                        break;
                }
            }
            if (builder != null)
            {
                list.Add(builder.ToString());
            }
				IEnumerator enumerator = list.GetEnumerator();
             while (enumerator.MoveNext())
             {
                 switch (((string) enumerator.Current))
                 {
                     case "m":
                     case "mm":
                     case "mmm":
                     case "mmmm":
                     case "mmmmm":
                     case "d":
                     case "dd":
                     case "ddd":
                     case "dddd":
                     case "yy":
                     case "yyyy":
                     case "h":
                     case "hh":
                     case "s":
                     case "ss":
                         return true;
                 }
             }
            return false;
        }

        private CellStyle LoadCellStyle(AbsXLSRec record)
        {
            if ((this.fontsTable.Count == 0) || (this.numberFormats.Count == 0))
            {
                throw new Exception("Internal: fontsTable or numberFormats is empty.");
            }
            object[] arguments = ((XLSRecord) record).GetArguments();
            CellStyle style = new CellStyle(this.excelFile);
            CellStyleData element = style.Element;
            element.Indexes = new CellStyleDataIndexes();
            int num = (ushort) arguments[0];
            element.FontData = (ExcelFontData) this.fontsTable[num];
            element.Indexes.NumberFormatIndex = (ushort) arguments[1];
            element.NumberFormat = (string) this.numberFormats[element.Indexes.NumberFormatIndex];
            XFOptions1 options = (XFOptions1) arguments[2];
            if (((ushort) (options & XFOptions1.CellLocked)) != 0)
            {
                element.Locked = true;
            }
            else
            {
                element.Locked = false;
            }
            if (((ushort) (options & XFOptions1.FormulaHidden)) != 0)
            {
                element.FormulaHidden = true;
            }
            byte num2 = (byte) arguments[3];
            element.VerticalAlignment = (VerticalAlignmentStyle) (num2 >> 4);
            if ((num2 & 8) != 0)
            {
                element.WrapText = true;
            }
            element.HorizontalAlignment = ((HorizontalAlignmentStyle) num2) & HorizontalAlignmentStyle.Distributed;
            int num3 = (byte) arguments[4];
            if ((num3 > 90) && (num3 < 0xff))
            {
                element.Rotation = num3 - 0x100;
            }
            else
            {
                element.Rotation = num3;
            }
            XFOptions2 options2 = (XFOptions2) arguments[5];
            element.Indent = (ushort) (options2 & XFOptions2.IndentLevel);
            if (((ushort) (options2 & XFOptions2.ShrinkToFit)) != 0)
            {
                element.ShrinkToFit = true;
            }
            ushort num4 = (ushort) arguments[6];
            style.Borders[IndividualBorder.Left].LineStyle = ((LineStyle) num4) & (LineStyle.SlantDashDot | LineStyle.Medium);
            num4 = (ushort) (num4 >> 4);
            style.Borders[IndividualBorder.Right].LineStyle = ((LineStyle) num4) & (LineStyle.SlantDashDot | LineStyle.Medium);
            num4 = (ushort) (num4 >> 4);
            style.Borders[IndividualBorder.Top].LineStyle = ((LineStyle) num4) & (LineStyle.SlantDashDot | LineStyle.Medium);
            num4 = (ushort) (num4 >> 4);
            style.Borders[IndividualBorder.Bottom].LineStyle = ((LineStyle) num4) & (LineStyle.SlantDashDot | LineStyle.Medium);
            ushort num5 = (ushort) arguments[7];
            if ((num5 & 0x8000) != 0)
            {
                element.BordersUsed |= MultipleBorders.DiagonalUp;
            }
            if ((num5 & 0x4000) != 0)
            {
                element.BordersUsed |= MultipleBorders.DiagonalDown;
            }
            element.Indexes.BorderColorIndex[2] = num5 & 0x3f;
            num5 = (ushort) (num5 >> 7);
            element.Indexes.BorderColorIndex[3] = num5 & 0x3f;
            uint num6 = (uint) arguments[8];
            element.Indexes.BorderColorIndex[0] = ((int) num6) & 0x7f;
            num6 = num6 >> 7;
            element.Indexes.BorderColorIndex[1] = ((int) num6) & 0x7f;
            num6 = num6 >> 7;
            element.Indexes.BorderColorIndex[4] = ((int) num6) & 0x7f;
            num6 = num6 >> 7;
            element.BorderStyle[4] = ((LineStyle) num6) & (LineStyle.SlantDashDot | LineStyle.Medium);
            num6 = num6 >> 5;
            element.PatternStyle = ((FillPatternStyle) num6) & ((FillPatternStyle) 0x3f);
            ushort num7 = (ushort) arguments[9];
            element.Indexes.PatternForegroundColorIndex = num7 & 0x7f;
            num7 = (ushort) (num7 >> 7);
            element.Indexes.PatternBackgroundColorIndex = num7 & 0x7f;
            element.BordersUsed |= MultipleBorders.Outside;
            style.UseFlags = CellStyleData.Properties.All;
            return style;
        }

        private void LoadColumnInfo(AbsXLSRec record, ExcelWorksheet ws)
        {
            object[] arguments = ((XLSRecord) record).GetArguments();
            ushort num = (ushort) arguments[0];
            ushort num2 = (ushort) arguments[1];
            if (num2 > 0xff)
            {
                num2 = 0xff;
            }
            ushort num3 = (ushort) arguments[2];
            ushort num4 = (ushort) arguments[3];
            ColumnInfoOptions options = (ColumnInfoOptions) arguments[4];
            for (int i = num; i <= num2; i++)
            {
                ExcelColumn column = ws.Columns[i];
                column.Width = num3;
                column.Style = (CellStyle) this.cellStylesTable[num4];
                if (((ushort) (options & ColumnInfoOptions.Collapsed)) != 0)
                {
                    column.Collapsed = true;
                }
                if (((ushort) (options & ColumnInfoOptions.Hidden)) != 0)
                {
                    column.Hidden = true;
                }
                column.OutlineLevel = (((ushort) options) >> 8) & 7;
            }
        }

        internal static void LoadDefaultPalette(ref Color[] colorsTable)
        {
            int length = DefaultPalette.GetLength(0);
            colorsTable = new Color[length];
            for (int i = 0; i < length; i++)
            {
                int index = (int) DefaultPalette[i, 0];
                int red = (int) DefaultPalette[i, 1];
                int green = (int) DefaultPalette[i, 2];
                int blue = (int) DefaultPalette[i, 3];
                colorsTable[index] = Color.FromArgb(red, green, blue);
            }
        }

        private void LoadExternsheet(AbsXLSRec record)
        {
            object[] objArray2 = (object[]) ((XLSRecord) record).GetArguments()[1];
            for (int i = 0; i < objArray2.Length; i++)
            {
                ushort sheetIndex = ((SheetIndexes) objArray2[i]).SheetIndex;
                this.excelFile.Worksheets.SheetIndexes.Add(sheetIndex);
            }
        }

        private ExcelFontData LoadFont(AbsXLSRec record)
        {
            object[] arguments = ((XLSRecord) record).GetArguments();
            ExcelFontData data = new ExcelFontData(null, -1);
            data.Size = (ushort) arguments[0];
            FontOptions options = (FontOptions) arguments[1];
            if (((ushort) (options & FontOptions.Italic)) != 0)
            {
                data.Italic = true;
            }
            if (((ushort) (options & FontOptions.Strikeout)) != 0)
            {
                data.Strikeout = true;
            }
            data.ColorIndex = (ushort) arguments[2];
            data.Weight = (ushort) arguments[3];
            data.ScriptPosition = (ScriptPosition) ((ushort) arguments[4]);
            data.UnderlineStyle = (UnderlineStyle) ((byte) arguments[5]);
            data.Name = ((ExcelShortString) arguments[6]).Str;
            return data;
        }

        private void LoadMergedCells(AbsXLSRec record, ExcelWorksheet ws)
        {
            object[] arguments = ((MergedCellsRecord) record).GetArguments();
            ushort num = (ushort) arguments[0];
            object[] objArray2 = (object[]) arguments[1];
            for (int i = 0; i < num; i++)
            {
                ushort firstRow = (ushort) objArray2[i * 4];
                ushort lastRow = (ushort) objArray2[(i * 4) + 1];
                ushort firstColumn = (ushort) objArray2[(i * 4) + 2];
                ushort lastColumn = (ushort) objArray2[(i * 4) + 3];
                ws.Cells.GetSubrangeAbsolute(firstRow, firstColumn, lastRow, lastColumn).Merged = true;
            }
        }

        private void LoadMsoDrawing(AbsXLSRec record, ExcelWorksheet sheet)
        {
            MsoContainerRecord baseRec = ((XLSRecord) record).GetArguments()[0] as MsoContainerRecord;
            Rectangle imageBoundaryFromMsoRecords = this.GetImageBoundaryFromMsoRecords(baseRec);
            if (this.images.Count != 0)
            {
                this.nextImage = ((this.nextImage + 1) > this.images.Count) ? (this.images.Count - 1) : this.nextImage;
                Image image = this.images[this.nextImage] as Image;
                if (image != null)
                {
                    if (imageBoundaryFromMsoRecords == Rectangle.Empty)
                    {
                        sheet.Pictures.Add(image, (MsoBlipType) this.blibTypes[this.nextImage]);
                    }
                    else
                    {
                        sheet.Pictures.Add(image, imageBoundaryFromMsoRecords, (MsoBlipType) this.blibTypes[this.nextImage]);
                    }
                    this.nextImage++;
                }
            }
        }

        private void LoadMsoDrawingGroup(AbsXLSRec record)
        {
            MsoContainerRecord baseRec = this.excelFile.cachedMsoDrawingGroupArguments[0] as MsoContainerRecord;
            this.FillImagesFromMsoRecords(baseRec);
        }

        private void LoadName(AbsXLSRec record)
        {
            object[] arguments = ((XLSRecord) record).GetArguments();
            object[] options = (object[]) arguments[0];
            byte[] rpnBytes = Utilities.ConvertObjectArrayToBytes((object[]) arguments[5]);
            if (rpnBytes.Length != 0)
            {
                byte code = rpnBytes[0];
                CellRange range = null;
                ExcelWorksheet sheet = null;
                if (Ref3dFormulaToken.IsRef3dToken(code) || Area3dFormulaToken.IsArea3dToken(code))
                {
                    FormulaToken token = FormulaTokensFactory.CreateFrom(sheet, rpnBytes, 0);
                    Ref3dFormulaToken token2 = token as Ref3dFormulaToken;
                    if (token2 == null)
                    {
                        Area3dFormulaToken token3 = token as Area3dFormulaToken;
                        ushort num2 = (ushort) this.excelFile.Worksheets.SheetIndexes[token3.ExternsheetIndex];
                        if (num2 == 0xffff)
                        {
                            this.OnNameLoadFail();
                            return;
                        }
                        sheet = this.excelFile.Worksheets[num2];
                        range = new CellRange(sheet, token3.FirstRow, token3.FirstColumn, token3.LastRow, token3.LastColumn);
                    }
                    else
                    {
                        ushort num3 = (ushort) this.excelFile.Worksheets.SheetIndexes[token2.ExternsheetIndex];
                        if (num3 == 0xffff)
                        {
                            this.OnNameLoadFail();
                            return;
                        }
                        sheet = this.excelFile.Worksheets[num3];
                        range = new CellRange(sheet, token2.Row, token2.Column, token2.Row, token2.Column);
                    }
                    bool globalName = false;
                    if (((ushort) arguments[3]) == 0)
                    {
                        globalName = true;
                    }
                    sheet.NamedRanges.Add(options, ((ExcelStringWithoutLength) arguments[4]).Str, range, globalName);
                }
                else
                {
                    this.OnNameLoadFail();
                }
            }
        }

        private void LoadNoteRecord(AbsXLSRec record, ExcelWorksheet sheet)
        {
            object[] arguments = ((XLSRecord) record).GetArguments();
            this.notesArray.Add(new TempNote((ushort) arguments[0], (ushort) arguments[1], ((ushort) arguments[2]) == 2, (ushort) arguments[3]));
        }

        private void LoadNumberFormat(AbsXLSRec record)
        {
            object[] arguments = ((XLSRecord) record).GetArguments();
            int index = (ushort) arguments[0];
            string formatString = ((ExcelLongString) arguments[1]).Str;
            this.numberFormats.SetNumberFormat(index, formatString);
        }

        private void LoadPalette(AbsXLSRec record)
        {
            object[] arguments = ((PaletteRecord) record).GetArguments();
            int num = (ushort) arguments[0];
            object[] objArray2 = (object[]) arguments[1];
            for (int i = 0; i < num; i++)
            {
                int index = i * 4;
                byte red = (byte) objArray2[index];
                byte green = (byte) objArray2[index + 1];
                byte blue = (byte) objArray2[index + 2];
                this.colorsTable[8 + i] = Color.FromArgb(red, green, blue);
            }
        }

        private void LoadRow(AbsXLSRec record, ExcelWorksheet ws)
        {
            RowRecord record2 = (RowRecord) record;
            RowOptions options = record2.Options;
            ExcelRow row = ws.Rows[record2.RowIndex];
            row.Height = record2.RowHeight;
            if (((ushort) (options & (RowOptions.Default | RowOptions.Collapsed))) != 0)
            {
                row.Collapsed = true;
            }
            if (((ushort) (options & RowOptions.GhostDirty)) != 0)
            {
                row.Style = (CellStyle) this.cellStylesTable[record2.StyleIndex];
            }
            if (((ushort) (options & RowOptions.Hidden)) != 0)
            {
                row.Hidden = true;
            }
            row.OutlineLevel = ((int) options) & 7;
        }

        private void LoadTextContinueRecord(AbsXLSRec record, ExcelWorksheet sheet)
        {
            XLSRecord record2 = (XLSRecord) record;
            if ((record2.Body.Length - 1) > 0)
            {
                string str = Encoding.ASCII.GetString(record2.Body, 1, record2.Body.Length - 1);
                this.lastCommentTexts.Add(str);
            }
        }

        private void LoadWSBool(AbsXLSRec record, ExcelWorksheet ws)
        {
            object[] arguments = ((XLSRecord) record).GetArguments();
            ws.WSBoolOpt = (WSBoolOptions) arguments[0];
        }

        private void OnNameLoadFail()
        {
            ExcelFile.OnIoWarning(this.excelFile, this.diagnosticsFileName, IoOperation.XlsReading, "Can't read named range. Maybe file contains area intersection which GemBox.Spreadsheet doesn't support");
        }

        private void SetCell(ExcelWorksheet ws, int rowIndex, int columnIndex, int styleIndex, object valInternal)
        {
            ExcelCell cell = ws.Cells[rowIndex, columnIndex];
            if (styleIndex != 0)
            {
                object obj2;
                CellStyle style = (CellStyle) this.cellStylesTable[styleIndex];
                cell.Style = style;
                if (valInternal is CellFormula)
                {
                    obj2 = ((CellFormula) valInternal).Value;
                }
                else
                {
                    obj2 = valInternal;
                }
                if ((obj2 is double) || (obj2 is int))
                {
                    int numberFormatIndex = style.Element.Indexes.NumberFormatIndex;
                    if (((numberFormatIndex >= 14) && (numberFormatIndex <= 0x16)) || IsDateTime(style.Element.NumberFormat))
                    {
                        if (obj2 is int)
                        {
                            obj2 = (int) obj2;
                        }
                        obj2 = ExcelCell.ConvertExcelNumberToDateTime((double) obj2, this.excelFile.Use1904DateSystem);
                        if (valInternal is CellFormula)
                        {
                            ((CellFormula) valInternal).Value = obj2;
                        }
                        else
                        {
                            valInternal = obj2;
                        }
                    }
                }
            }
            cell.ValueInternal = valInternal;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct TempNote : IComparable
        {
            internal int id;
            internal int row;
            internal int col;
            internal bool visiblity;
            public TempNote(ushort row_, ushort col_, bool vs, ushort id_)
            {
                this.id = id_;
                this.row = row_;
                this.col = col_;
                this.visiblity = vs;
            }

            public int CompareTo(object obj)
            {
                XLSFileReader.TempNote note = (XLSFileReader.TempNote) obj;
                return this.id.CompareTo(note.id);
            }
        }
    }
}

