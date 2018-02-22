namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    internal class XLSFileWriter
    {
        private ColorCollection colors;
        private static readonly Color[] defaultColors = new Color[] { Color.FromArgb(0, 0, 0), Color.FromArgb(0xff, 0xff, 0xff), Color.FromArgb(0xff, 0, 0), Color.FromArgb(0, 0xff, 0), Color.FromArgb(0, 0, 0xff), Color.FromArgb(0xff, 0xff, 0), Color.FromArgb(0xff, 0, 0xff), Color.FromArgb(0, 0xff, 0xff) };
        private static AbsXLSRec[] defaultFontRecords;
        private static readonly AbsXLSRec[] defaultNumberFormatRecords = new AbsXLSRec[] { new XLSRecord("Format", "05 00 17 00 00 22 24 22 23 2c 23 23 30 5f 29 3b 5c 28 22 24 22 23 2c 23 23 30 5c 29"), new XLSRecord("Format", "06 00 1c 00 00 22 24 22 23 2c 23 23 30 5f 29 3b 5b 52 65 64 5d 5c 28 22 24 22 23 2c 23 23 30 5c 29"), new XLSRecord("Format", "07 00 1d 00 00 22 24 22 23 2c 23 23 30 2e 30 30 5f 29 3b 5c 28 22 24 22 23 2c 23 23 30 2e 30 30 5c 29"), new XLSRecord("Format", "08 00 22 00 00 22 24 22 23 2c 23 23 30 2e 30 30 5f 29 3b 5b 52 65 64 5d 5c 28 22 24 22 23 2c 23 23 30 2e 30 30 5c 29"), new XLSRecord("Format", "2a 00 32 00 00 5f 28 22 24 22 2a 20 23 2c 23 23 30 5f 29 3b 5f 28 22 24 22 2a 20 5c 28 23 2c 23 23 30 5c 29 3b 5f 28 22 24 22 2a 20 22 2d 22 5f 29 3b 5f 28 40 5f 29"), new XLSRecord("Format", "29 00 29 00 00 5f 28 2a 20 23 2c 23 23 30 5f 29 3b 5f 28 2a 20 5c 28 23 2c 23 23 30 5c 29 3b 5f 28 2a 20 22 2d 22 5f 29 3b 5f 28 40 5f 29"), new XLSRecord("Format", "2c 00 3a 00 00 5f 28 22 24 22 2a 20 23 2c 23 23 30 2e 30 30 5f 29 3b 5f 28 22 24 22 2a 20 5c 28 23 2c 23 23 30 2e 30 30 5c 29 3b 5f 28 22 24 22 2a 20 22 2d 22 3f 3f 5f 29 3b 5f 28 40 5f 29"), new XLSRecord("Format", "2b 00 31 00 00 5f 28 2a 20 23 2c 23 23 30 2e 30 30 5f 29 3b 5f 28 2a 20 5c 28 23 2c 23 23 30 2e 30 30 5c 29 3b 5f 28 2a 20 22 2d 22 3f 3f 5f 29 3b 5f 28 40 5f 29") };
        private static readonly AbsXLSRec[] defaultStyleRecords = new AbsXLSRec[] { new StyleRecord("10 80 03 ff"), new StyleRecord("11 80 06 ff"), new StyleRecord("12 80 04 ff"), new StyleRecord("13 80 07 ff"), new StyleRecord("00 80 00 ff"), new StyleRecord("14 80 05 ff") };
        private static readonly AbsXLSRec[] defaultXFRecords = new AbsXLSRec[] { 
            new XLSRecord("XF", "00 00 00 00 f5 ff 20 00 00 00 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "01 00 00 00 f5 ff 20 00 00 f4 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "01 00 00 00 f5 ff 20 00 00 f4 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "02 00 00 00 f5 ff 20 00 00 f4 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "02 00 00 00 f5 ff 20 00 00 f4 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "00 00 00 00 f5 ff 20 00 00 f4 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "00 00 00 00 f5 ff 20 00 00 f4 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "00 00 00 00 f5 ff 20 00 00 f4 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "00 00 00 00 f5 ff 20 00 00 f4 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "00 00 00 00 f5 ff 20 00 00 f4 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "00 00 00 00 f5 ff 20 00 00 f4 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "00 00 00 00 f5 ff 20 00 00 f4 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "00 00 00 00 f5 ff 20 00 00 f4 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "00 00 00 00 f5 ff 20 00 00 f4 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "00 00 00 00 f5 ff 20 00 00 f4 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "00 00 00 00 01 00 20 00 00 00 00 00 00 00 00 00 00 00 c0 20"), 
            new XLSRecord("XF", "01 00 2b 00 f5 ff 20 00 00 f8 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "01 00 29 00 f5 ff 20 00 00 f8 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "01 00 2c 00 f5 ff 20 00 00 f8 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "01 00 2a 00 f5 ff 20 00 00 f8 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "01 00 09 00 f5 ff 20 00 00 f8 00 00 00 00 00 00 00 00 c0 20")
         };
        private string diagnosticsFileName;
        private ExcelFile excelFile;
        private IndexedHashCollection fonts;
        private const int MaxRKInt = 0x1fffffff;
        private const int MinRKInt = -536870912;
        private NumberFormatCollection numberFormats;
        private AbsXLSRecords records;
        private const int startFontIndex = 5;
        public const int StartFormatIndex = 0xa4;
        private IndexedHashCollection strings;
        private ArrayList usedCellStyleData;

        public XLSFileWriter(ExcelFile excelFile, string diagnosticsFileName)
        {
            this.excelFile = excelFile;
            this.diagnosticsFileName = diagnosticsFileName;
            ExcelFontData fontData = new ExcelFontData(this.excelFile.DefaultFontName, this.excelFile.DefaultFontSize);
            fontData.ColorIndex = defaultColors.Length;
            defaultFontRecords = new AbsXLSRec[] { CreateFontRecord(fontData), CreateFontRecord(fontData), CreateFontRecord(fontData), CreateFontRecord(fontData) };
        }

        private static CellStyle Combine(CellStyle highPriority, CellStyle lowPriority)
        {
            CellStyle style;
            if (highPriority == null)
            {
                return lowPriority;
            }
            if (lowPriority == null)
            {
                return highPriority;
            }
            if (highPriority.Element.IsInCache)
            {
                style = new CellStyle(highPriority, highPriority.Element.ParentCollection);
            }
            else
            {
                style = highPriority;
            }
            style.CopyIfNotUsed(lowPriority);
            return style;
        }

        private AbsXLSRec CreateCellRecord(ushort row, ushort column, ushort styleIndex, object cellValue, CellFormula formula, out ArrayList extraRecords)
        {
            string str;
            CellRecordHeader header = new CellRecordHeader(row, column, styleIndex);
            if (formula != null)
            {
                extraRecords = formula.ExtraFormulaRecords;
                formula.Recalculate();
                return new FormulaRecord(new object[] { header, formula.ResultBytes, formula.Options, (ushort) formula.RpnBytes.Length, formula.RpnBytes });
            }
            extraRecords = null;
            if ((cellValue == null) || (cellValue is DBNull))
            {
                return new XLSRecord("Blank", new object[] { header });
            }
            if (cellValue is Enum)
            {
                cellValue = cellValue.ToString();
            }
            switch (cellValue.GetType().FullName)
            {
                case "System.Byte":
                case "System.SByte":
                case "System.Int16":
                case "System.UInt16":
                    cellValue = Convert.ToInt32(cellValue, CultureInfo.InvariantCulture);
                    break;

                case "System.Int64":
                {
                    long num = (long) cellValue;
                    if ((num >= -536870912L) && (num <= 0x1fffffffL))
                    {
                        cellValue = Convert.ToInt32(num, CultureInfo.InvariantCulture);
                        break;
                    }
                    cellValue = num.ToString(CultureInfo.InvariantCulture);
                    goto Label_03CD;
                }
                case "System.UInt64":
                {
                    ulong num2 = (ulong) cellValue;
                    if (num2 <= 0x1fffffffL)
                    {
                        cellValue = Convert.ToInt32(num2, CultureInfo.InvariantCulture);
                        break;
                    }
                    cellValue = num2.ToString(CultureInfo.InvariantCulture);
                    goto Label_03CD;
                }
                case "System.UInt32":
                {
                    uint num3 = (uint) cellValue;
                    if (num3 <= 0x1fffffff)
                    {
                        cellValue = Convert.ToInt32(num3, CultureInfo.InvariantCulture);
                        break;
                    }
                    cellValue = num3.ToString(CultureInfo.InvariantCulture);
                    goto Label_03CD;
                }
                case "System.Int32":
                    break;

                case "System.Single":
                    return new RKRecord(header, GetRKValue((float) cellValue));

                case "System.Double":
                    goto Label_0323;

                case "System.Boolean":
                    return new XLSRecord("BoolErr", new object[] { header, ((bool) cellValue) ? ((byte) 1) : ((byte) 0), (byte) 0 });

                case "System.Char":
                case "System.Text.StringBuilder":
                    cellValue = cellValue.ToString();
                    goto Label_03CD;

                case "System.Decimal":
                    cellValue = Convert.ToDouble((decimal) cellValue);
                    goto Label_0323;

                case "System.DateTime":
                    cellValue = GetEncodedDateTime((DateTime) cellValue, this.excelFile.Use1904DateSystem);
                    goto Label_0323;

                case "System.String":
                    goto Label_03CD;

                default:
                    throw new Exception("Internal error: Data type not supported as cell value.");
            }
            int val = (int) cellValue;
            if ((val < -536870912) || (val > 0x1fffffff))
            {
                cellValue = val.ToString(CultureInfo.InvariantCulture);
                goto Label_03CD;
            }
            return new RKRecord(header, GetRKValue(val));
        Label_0323:;
            return new XLSRecord("Number", new object[] { header, (double) cellValue });
        Label_03CD:
            str = (string) cellValue;
            return new LabelSSTRecord(header, this.GetSSTIndex(str));
        }

        private AbsXLSRec CreateCellRecordIfNeeded(ExcelWorksheet worksheet, int row, int column, int currentCellsCount, out ArrayList extraRecords)
        {
            MergedCellRange mergedRange;
            object obj2;
            CellStyle style;
            CellFormula formulaInternal;
            bool flag = false;
            if (column < currentCellsCount)
            {
                ExcelCell cell = worksheet.Rows[row].AllocatedCells[column];
                if (cell.HasComment)
                {
                    cell.Comment.SetRow(row);
                    cell.Comment.SetColumn(column);
                }
                mergedRange = (MergedCellRange) cell.MergedRange;
                if (mergedRange != null)
                {
                    flag = true;
                    if ((row == mergedRange.FirstRowIndex) && (column == mergedRange.FirstColumnIndex))
                    {
                        obj2 = mergedRange.Value;
                        formulaInternal = mergedRange.FormulaInternal;
                    }
                    else
                    {
                        obj2 = null;
                        formulaInternal = null;
                    }
                    style = mergedRange.StyleResolved(row, column);
                }
                else
                {
                    obj2 = cell.Value;
                    if (obj2 != null)
                    {
                        flag = true;
                    }
                    formulaInternal = cell.FormulaInternal;
                    if (formulaInternal != null)
                    {
                        flag = true;
                    }
                    if (!cell.IsStyleDefault)
                    {
                        flag = true;
                        style = cell.Style;
                    }
                    else
                    {
                        style = null;
                    }
                }
            }
            else
            {
                if (!worksheet.Rows[row].IsStyleDefault && !worksheet.Columns[column].IsStyleDefault)
                {
                    flag = true;
                }
                mergedRange = null;
                obj2 = null;
                style = null;
                formulaInternal = null;
            }
            if (!flag)
            {
                extraRecords = null;
                return null;
            }
            if (mergedRange == null)
            {
                CellStyle style2;
                CellStyle style3;
                CellStyle style4;
                if (!worksheet.Rows[row].IsStyleDefault)
                {
                    style2 = worksheet.Rows[row].Style;
                }
                else
                {
                    style2 = null;
                }
                if (!worksheet.Columns[column].IsStyleDefault)
                {
                    style3 = worksheet.Columns[column].Style;
                }
                else
                {
                    style3 = null;
                }
                if (this.excelFile.RowColumnResolutionMethod == RowColumnResolutionMethod.RowOverColumn)
                {
                    style4 = Combine(style2, style3);
                }
                else
                {
                    style4 = Combine(style3, style2);
                }
                style = Combine(style, style4);
            }
            SetDefaultDateFormatIfNeeded(obj2, ref style, this.excelFile);
            if ((style != null) && !style.Element.IsInCache)
            {
                this.excelFile.CellStyleCache.EmptyAddQueue();
            }
            int cellStyleIndex = this.GetCellStyleIndex(style);
            return this.CreateCellRecord((ushort) row, (ushort) column, (ushort) cellStyleIndex, obj2, formulaInternal, out extraRecords);
        }

        private void CreateCellStyleIndexes(CellStyleData element)
        {
            CellStyleDataIndexes indexes = new CellStyleDataIndexes();
            indexes.CellStyleIndex = this.usedCellStyleData.Add(element) + defaultXFRecords.Length;
            indexes.FontIndex = this.fonts.Add(element.FontData);
            ExcelFontData data = (ExcelFontData) this.fonts[indexes.FontIndex];
            if (data.ColorIndex == -1)
            {
                data.ColorIndex = this.colors.AddColor(data.Color);
            }
            indexes.FontIndex += 5;
            indexes.PatternBackgroundColorIndex = this.colors.AddColor(element.PatternBackgroundColor);
            indexes.PatternForegroundColorIndex = this.colors.AddColor(element.PatternForegroundColor);
            for (int i = 0; i < 5; i++)
            {
                indexes.BorderColorIndex[i] = this.colors.AddColor(element.BorderColor[i]);
            }
            if (element.NumberFormat.Length == 0)
            {
                indexes.NumberFormatIndex = 0;
            }
            else
            {
                indexes.NumberFormatIndex = this.numberFormats.Add(element.NumberFormat);
            }
            element.Indexes = indexes;
        }

        private static AbsXLSRec CreateFontRecord(ExcelFontData fontData)
        {
            FontOptions options = 0;
            if (fontData.Italic)
            {
                options = (FontOptions) ((ushort) (options | FontOptions.Italic));
            }
            if (fontData.Strikeout)
            {
                options = (FontOptions) ((ushort) (options | FontOptions.Strikeout));
            }
            return new XLSRecord("Font", new object[] { (ushort) fontData.Size, options, (ushort) fontData.ColorIndex, (ushort) fontData.Weight, (ushort) fontData.ScriptPosition, (byte) fontData.UnderlineStyle, new ExcelShortString(fontData.Name) });
        }

        private XLSRecord CreatePaletteRecord()
        {
            object[] objArray = new object[this.colors.Count * 4];
            for (int i = 0; i < this.colors.Count; i++)
            {
                int index = i * 4;
                Color color = (Color) this.colors[i];
                objArray[index] = color.R;
                Color color2 = (Color) this.colors[i];
                objArray[index + 1] = color2.G;
                Color color3 = (Color) this.colors[i];
                objArray[index + 2] = color3.B;
                objArray[index + 3] = (byte) 0;
            }
            return new PaletteRecord(new object[] { (ushort) this.colors.Count, objArray });
        }

        private AbsXLSRec CreateRowRecordIfNeeded(ExcelWorksheet worksheet, int row, int currentFirstColumn, int currentLastColumn)
        {
            ushort cellStyleIndex;
            ExcelRow row2 = worksheet.Rows[row];
            if (((currentLastColumn == -1) && (row2.Height == 0xff)) && (((row2.OutlineLevel == 0) && !row2.Collapsed) && row2.IsStyleDefault))
            {
                return null;
            }
            RowOptions options = (RowOptions)0x100;
            if (row2.Height != 0xff)
            {
                options = (RowOptions) ((ushort) (options | RowOptions.Unsynced));
            }
            if (row2.Collapsed)
            {
                options = (RowOptions) ((ushort) (options | (RowOptions.Default | RowOptions.Collapsed)));
            }
            if ((row2.Collapsed || (row2.Height == 0)) || row2.Hidden)
            {
                options = (RowOptions) ((ushort) (options | RowOptions.Hidden));
            }
            if (!row2.IsStyleDefault)
            {
                options = (RowOptions) ((ushort) (options | RowOptions.GhostDirty));
            }
				options = (RowOptions)(((ushort)options | ((ushort)row2.OutlineLevel)));
            if (row2.IsStyleDefault)
            {
                cellStyleIndex = (ushort) this.GetCellStyleIndex(null);
            }
            else
            {
                cellStyleIndex = (ushort) this.GetCellStyleIndex(row2.Style);
            }
            return new RowRecord((ushort) row, (ushort) currentFirstColumn, (ushort) (currentLastColumn + 1), (ushort) row2.Height, options, cellStyleIndex);
        }

        private AbsXLSRec CreateSSTOrContinueRecord(ref AbsXLSRec sstRecord, ExcelLongStrings excelStrings)
        {
            if (sstRecord == null)
            {
                uint count = (uint) this.strings.Count;
                sstRecord = new SSTRecord(new object[] { count, count, excelStrings });
                return sstRecord;
            }
            return new ContinueRecord(excelStrings);
        }

        private static AbsXLSRec CreateXFRecord(CellStyleData styleData)
        {
            CellStyleDataIndexes indexes = styleData.Indexes;
            XFOptions1 options = 0;
            if (styleData.Locked)
            {
                options = (XFOptions1) ((ushort) (options | XFOptions1.CellLocked));
            }
            if (styleData.FormulaHidden)
            {
                options = (XFOptions1) ((ushort) (options | XFOptions1.FormulaHidden));
            }
            byte num = (byte) (((byte) styleData.VerticalAlignment) << 4);
            if (styleData.WrapText)
            {
                num = (byte) (num | 8);
            }
            num = (byte) (num | ((byte) styleData.HorizontalAlignment));
            int num2 = styleData.Rotation % 0x100;
            if (num2 < 0)
            {
                num2 += 0x100;
            }
            XFOptions2 indent = (XFOptions2) ((ushort) styleData.Indent);
            if (styleData.ShrinkToFit)
            {
                indent = (XFOptions2) ((ushort) (indent | XFOptions2.ShrinkToFit));
            }
            indent = (XFOptions2) ((ushort) (indent | XFOptions2.UsedAttributes));
            ushort num3 = (ushort) styleData.BorderStyle[1];
            num3 = (ushort) (num3 << 4);
            num3 = (ushort) (num3 | ((ushort) styleData.BorderStyle[0]));
            num3 = (ushort) (num3 << 4);
            num3 = (ushort) (num3 | ((ushort) styleData.BorderStyle[3]));
            num3 = (ushort) (num3 << 4);
            num3 = (ushort) (num3 | ((ushort) styleData.BorderStyle[2]));
            ushort num4 = (ushort) indexes.BorderColorIndex[3];
            num4 = (ushort) (num4 << 7);
            num4 = (ushort) (num4 | ((ushort) indexes.BorderColorIndex[2]));
            if ((styleData.BordersUsed & MultipleBorders.DiagonalDown) != MultipleBorders.None)
            {
                num4 = (ushort) (num4 | 0x4000);
            }
            if ((styleData.BordersUsed & MultipleBorders.DiagonalUp) != MultipleBorders.None)
            {
                num4 = (ushort) (num4 | 0x8000);
            }
            uint num5 = ((uint) styleData.PatternStyle) << 5;
            num5 = (uint) (((LineStyle) num5) | styleData.BorderStyle[4]);
            num5 = num5 << 7;
            num5 |= (uint) indexes.BorderColorIndex[4];
            num5 = num5 << 7;
            num5 |= (uint) indexes.BorderColorIndex[1];
            num5 = num5 << 7;
            num5 |= (uint) indexes.BorderColorIndex[0];
            ushort patternBackgroundColorIndex = (ushort) indexes.PatternBackgroundColorIndex;
            patternBackgroundColorIndex = (ushort) (patternBackgroundColorIndex << 7);
            patternBackgroundColorIndex = (ushort) (patternBackgroundColorIndex + ((ushort) indexes.PatternForegroundColorIndex));
            return new XLSRecord("XF", new object[] { (ushort) indexes.FontIndex, (ushort) indexes.NumberFormatIndex, options, num, (byte) num2, (ushort) indent, num3, num4, num5, patternBackgroundColorIndex });
        }

        private int GetCellStyleIndex(CellStyle cellStyle)
        {
            if (cellStyle == null)
            {
                return 0;
            }
            CellStyleData element = cellStyle.Element;
            if (element.Indexes == null)
            {
                this.CreateCellStyleIndexes(element);
            }
            return element.Indexes.CellStyleIndex;
        }

        internal static double GetEncodedDateTime(DateTime dateTime, bool use1904DateSystem)
        {
            DateTime startDate = ExcelFile.GetStartDate(use1904DateSystem);
            TimeSpan span = (TimeSpan) (dateTime - startDate);
            double num = 2 + span.Days;
            return (num + ((((dateTime.Hour * 3600.0) + (dateTime.Minute * 60.0)) + dateTime.Second) / 86400.0));
        }

        private static NameRecord[] GetNameRecords(ExcelFile excelFile)
        {
            ArrayList list = new ArrayList();
            for (int i = 0; i < excelFile.Worksheets.Count; i++)
            {
                ExcelWorksheet worksheet = excelFile.Worksheets[i];
                for (int j = 0; j < worksheet.NamedRanges.Count; j++)
                {
                    NamedRange range = worksheet.NamedRanges[j];
                    NameRecord record = new NameRecord(worksheet);
                    record.Options = range.Options;
                    if (!range.GlobalName)
                    {
                        record.SheetIndex = (ushort) (worksheet.Parent.GetSheetIndex(worksheet.Name) + 1);
                    }
                    else
                    {
                        record.SheetIndex = 0;
                    }
                    record.NameValue = range.Name;
                    record.Worksheets = worksheet.Parent;
                    record.Range = range.Range;
                    record.RpnBytes = Utilities.ConvertBytesToObjectArray(NameRecord.ConvertNameRecordRangeToRpnBytes(worksheet, range.Range, worksheet.Name, excelFile.Worksheets));
                    list.Add(record);
                }
            }
            return (NameRecord[]) list.ToArray(typeof(NameRecord));
        }

        public AbsXLSRecords GetRecords()
        {
            this.Initialize();
            this.excelFile.CellStyleCache.EmptyAddQueue();
            this.WriteRecords();
            this.records.SetRecordAddresses();
            return this.records;
        }

        private static uint GetRKValue(int val)
        {
            uint num = (uint) val;
            num = num << 2;
            return (num | 2);
        }

        private static uint GetRKValue(float val)
        {
            uint num;
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write((double) val);
                    stream.Position = 0L;
                    using (BinaryReader reader = new BinaryReader(stream))
                    {
                        reader.ReadUInt32();
                        num = reader.ReadUInt32();
                    }
                }
            }
            return (num & 0xfffffffc);
        }

        private uint GetSSTIndex(string str)
        {
            return (uint) this.strings.Add(new ExcelLongString(str));
        }

        public static byte[] GetStream(AbsXLSRecords records)
        {
            MemoryStream stream;
            using (stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream, new UnicodeEncoding()))
                {
                    records.Write(writer);
                    stream.Capacity = (int) stream.Length;
                }
            }
            return stream.GetBuffer();
        }

        private void Initialize()
        {
            this.records = new AbsXLSRecords();
            this.usedCellStyleData = new ArrayList();
            this.colors = new ColorCollection();
            foreach (Color color in defaultColors)
            {
                this.colors.AddInternal(color);
            }
            this.numberFormats = new NumberFormatCollection(false);
            this.fonts = new IndexedHashCollection();
            this.strings = new IndexedHashCollection();
            this.strings.Add(new ExcelLongString(this.excelFile.IDText));
        }

        public static object RKValueToObj(uint rkVal)
        {
            double num;
            if ((rkVal & 2) != 0)
            {
                num = rkVal >> 2;
            }
            else
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    using (BinaryWriter writer = new BinaryWriter(stream))
                    {
                        writer.Write((uint) 0);
                        writer.Write((uint) (rkVal & 0xfffffffc));
                        stream.Position = 0L;
                        using (BinaryReader reader = new BinaryReader(stream))
                        {
                            num = reader.ReadDouble();
                        }
                    }
                }
            }
            if ((rkVal & 1) != 0)
            {
                num /= 100.0;
            }
            return Utilities.TryCastingToInt(num);
        }

        internal static void SetDefaultDateFormatIfNeeded(object cellValue, ref CellStyle cellStyle, ExcelFile ef)
        {
            if ((cellValue is DateTime) && (((cellStyle == null) || (cellStyle.NumberFormat == null)) || (cellStyle.NumberFormat.Length == 0)))
            {
                if (cellStyle == null)
                {
                    cellStyle = new CellStyle(ef.CellStyleCache);
                }
                else if (cellStyle.Element.IsInCache)
                {
                    cellStyle = new CellStyle(cellStyle, cellStyle.Element.ParentCollection);
                }
                cellStyle.NumberFormat = "yyyy-m-d hh:mm";
            }
        }

        private static ExcelLongString SplitLongString(ref ExcelLongString remainder, int maxSize)
        {
            int num;
            int length;
            string str = remainder.Str;
            if (remainder.splitSize == -1)
            {
                num = (maxSize - 1) / 2;
                length = -1;
            }
            else
            {
                num = (maxSize - 3) / 2;
                length = str.Length;
            }
            if (num < 1)
            {
                return null;
            }
            remainder = new ExcelLongString(str.Substring(num), -1);
            return new ExcelLongString(str.Substring(0, num), length);
        }

        private static void WriteCellsAndDBCellRecord(AbsXLSRecords records, ArrayList cellRecords, ref DBCellRecord dbCell, IndexRecord indexRecord)
        {
            foreach (AbsXLSRec rec in cellRecords)
            {
                records.Add(rec);
            }
            cellRecords.Clear();
            records.Add(dbCell);
            indexRecord.DBCells.Add(dbCell);
            dbCell = new DBCellRecord();
        }

        private void WriteColumnInfoIfNeeded(AbsXLSRecords records, ExcelWorksheet worksheet, int columnIndex)
        {
            ExcelColumn column = worksheet.Columns[columnIndex];
            if (((column.Width != worksheet.DefaultColumnWidth) || column.Collapsed) || (((column.OutlineLevel != 0) || !column.IsStyleDefault) || column.Hidden))
            {
                ushort cellStyleIndex;
                ColumnInfoOptions options = 0;
                if (column.Hidden)
                {
                    options = (ColumnInfoOptions) ((ushort) (options | ColumnInfoOptions.Hidden));
                }
                if (column.Collapsed)
                {
                    options = (ColumnInfoOptions) ((ushort) (options | ColumnInfoOptions.Collapsed));
                    if (worksheet.ViewOptions.ShowOutlineSymbols && (worksheet.Columns.MaxOutlineLevel > 0))
                    {
                        options = (ColumnInfoOptions) ((ushort) (options | ColumnInfoOptions.Hidden));
                    }
                }
					 options = (ColumnInfoOptions)(((ushort)options | ((ushort)(column.OutlineLevel << 8))));
                if (column.IsStyleDefault)
                {
                    cellStyleIndex = (ushort) this.GetCellStyleIndex(null);
                }
                else
                {
                    cellStyleIndex = (ushort) this.GetCellStyleIndex(column.Style);
                }
                records.Add(new XLSRecord("ColumnInfo", new object[] { (ushort) columnIndex, (ushort) columnIndex, (ushort) column.Width, cellStyleIndex, options }));
            }
        }

        private static void WriteExternsheetSupBookRecords(AbsXLSRecords records, ExcelFile excelFile)
        {
            SupBookRecord record = new SupBookRecord();
            record.SheetsCount = (ushort) excelFile.Worksheets.Count;
            records.Add(record);
            ExternsheetRecord record2 = new ExternsheetRecord();
            record2.SheetIndexes = (ushort[]) excelFile.Worksheets.SheetIndexes.ToArray(typeof(ushort));
            records.Add(record2);
        }

        /// <summary>
        /// Writes the global records.
        /// </summary>
        /// <param name="records">The records.</param>
        /// <param name="worksheetRecords">The worksheet records.</param>
        /// <param name="worksheetNames">The worksheet names.</param>
        private void WriteGlobalRecords(AbsXLSRecords records, ArrayList worksheetRecords, ArrayList worksheetNames)
        {
            ushort num;
            PreservedRecords preservedGlobalRecords = this.excelFile.PreservedGlobalRecords;
            records.Add(new XLSRecord("BOF", new object[] { BOFSubstreamType.WorkbookGlobals }));
            if (preservedGlobalRecords != null)
            {
                preservedGlobalRecords.WriteRecords(records, "WRITEPROT");
                preservedGlobalRecords.WriteRecords(records, "WRITEACCESS");
                preservedGlobalRecords.WriteRecords(records, "FILESHARING");
                preservedGlobalRecords.WriteRecords(records, "CODEPAGE");
            }
            if (preservedGlobalRecords != null)
            {
                if (this.excelFile.HasMacroses)
                {
                    records.Add(new XLSRecord("HASBASIC"));
                }
                preservedGlobalRecords.WriteRecords(records, "WINDOWPROTECT");
            }
            records.Add(new XLSRecord("Protect", new object[] { Utilities.BoolToUshort(this.excelFile.Protected) }));
            if (preservedGlobalRecords != null)
            {
                preservedGlobalRecords.WriteRecords(records, "OBJECTPROTECT");
            }
            records.Add(new XLSRecord("Window1", new object[] { (ushort) 120, (ushort) 120, (ushort) 0x3b1f, (ushort) 0x2454, Window1Options.DisplayHScroll | Window1Options.DisplayVScroll | Window1Options.ShowTabs, (ushort) this.excelFile.Worksheets.GetActiveWorksheetIndex(), (ushort) 0, (ushort) 1, (ushort) 600 }));
            if (preservedGlobalRecords != null)
            {
                preservedGlobalRecords.WriteRecords(records, "HIDEOBJ");
            }
            if (this.excelFile.Use1904DateSystem)
            {
                num = 1;
            }
            else
            {
                num = 0;
            }
            records.Add(new XLSRecord("DATEMODE", new object[] { num }));
            if (preservedGlobalRecords != null)
            {
                preservedGlobalRecords.WriteRecords(records, "PRECISION");
                preservedGlobalRecords.WriteRecords(records, "REFRESHALL");
                preservedGlobalRecords.WriteRecords(records, "BOOKBOOL");
            }
            foreach (AbsXLSRec rec in defaultFontRecords)
            {
                records.Add(rec);
            }
            foreach (ExcelFontData data in this.fonts)
            {
                records.Add(CreateFontRecord(data));
                data.ColorIndex = -1;
            }
            foreach (AbsXLSRec rec2 in defaultNumberFormatRecords)
            {
                records.Add(rec2);
            }
            for (int i = 0xa4; i < this.numberFormats.Count; i++)
            {
                string str = (string) this.numberFormats[i];
                records.Add(new XLSRecord("Format", new object[] { (ushort) i, new ExcelLongString(str) }));
            }
            foreach (AbsXLSRec rec3 in defaultXFRecords)
            {
                records.Add(rec3);
            }
            if (this.usedCellStyleData.Count > 0xf8b)
            {
                throw new Exception(string.Concat(new object[] { "Maximum number of cell styles in XLS file is ", 0xf8b, " and your file uses ", this.usedCellStyleData.Count, " different cell styles." }));
            }
            foreach (CellStyleData data2 in this.usedCellStyleData)
            {
                records.Add(CreateXFRecord(data2));
                data2.Indexes = null;
            }
            foreach (AbsXLSRec rec4 in defaultStyleRecords)
            {
                records.Add(rec4);
            }
            records.Add(this.CreatePaletteRecord());
            if (preservedGlobalRecords != null)
            {
                preservedGlobalRecords.WriteRecords(records, "USESELFS");
            }
            for (int j = 0; j < worksheetRecords.Count; j++)
            {
                string str2 = (string) worksheetNames[j];
                records.Add(new BoundSheetRecord(new ExcelShortString(str2), ((AbsXLSRecords) worksheetRecords[j])[0]));
            }
            if (preservedGlobalRecords != null)
            {
                preservedGlobalRecords.WriteRecords(records, "COUNTRY");
            }
            NameRecord[] nameRecords = GetNameRecords(this.excelFile);
            WriteExternsheetSupBookRecords(records, this.excelFile);
            if (nameRecords.Length > 0)
            {
                for (int k = 0; k < nameRecords.Length; k++)
                {
                    records.Add(nameRecords[k]);
                }
            }
            this.WriteMSODrawingIfNeeded(records);
            AbsXLSRec sstRecord = null;
            ExcelLongStrings excelStrings = new ExcelLongStrings();
            int num5 = 8;
            foreach (ExcelLongString str3 in this.strings)
            {
                ExcelLongString remainder = str3;
                while ((num5 + remainder.Size) > 0x2020)
                {
                    ExcelLongString str5 = SplitLongString(ref remainder, 0x2020 - num5);
                    if (str5 != null)
                    {
                        excelStrings.Strings.Add(str5);
                    }
                    records.Add(this.CreateSSTOrContinueRecord(ref sstRecord, excelStrings));
                    excelStrings = new ExcelLongStrings();
                    num5 = 0;
                }
                excelStrings.Strings.Add(remainder);
                num5 += remainder.Size;
            }
            if (num5 > 0)
            {
                records.Add(this.CreateSSTOrContinueRecord(ref sstRecord, excelStrings));
            }
            records.Add(new ExtSSTRecord(8, 12, sstRecord));
            records.Add(new XLSRecord("EOF"));
        }

        /// <summary>
        /// Writes the MSO drawing if needed.
        /// </summary>
        private void WriteMSODrawingIfNeeded(AbsXLSRecords records)
        {
            if (this.HasShapes)
            {
                MsoDrawingGroupRecord record = new MsoDrawingGroupRecord();
                MsoContainerRecord record2 = ImageRecordsFactory.CreateContainer(MsoType.DggContainer);
                MsofbtDggRecord item = new MsofbtDggRecord();
                item.Parent = record2;
                record2.Add(item);
                item.InitializeByWorksheets(this.excelFile.Worksheets);
                MsoContainerRecord record4 = ImageRecordsFactory.CreateContainer(MsoType.BstoreContainer);
                record4.Parent = record2;
                record2.Add(record4);
                foreach (MsofbtBseRecord record5 in this.excelFile.Worksheets.Pictures)
                {
                    record4.Add(record5);
                }
                record.Parent = record2;
                records.Add(record);
            }
        }

        private void WriteRecords()
        {
            int count = this.excelFile.Worksheets.Count;
            ArrayList worksheetRecords = new ArrayList();
            ArrayList worksheetNames = new ArrayList();
            for (int i = 0; i < count; i++)
            {
                ExcelWorksheet worksheet = this.excelFile.Worksheets[i];
                AbsXLSRecords records = new AbsXLSRecords();
                this.WriteWorksheetRecords(records, worksheet);
                worksheetRecords.Add(records);
                worksheetNames.Add(worksheet.Name);
            }
            this.WriteGlobalRecords(this.records, worksheetRecords, worksheetNames);
            foreach (AbsXLSRecords records2 in worksheetRecords)
            {
                foreach (AbsXLSRec rec in records2)
                {
                    this.records.Add(rec);
                }
            }
        }

        private void WriteWorksheetRecords(AbsXLSRecords records, ExcelWorksheet worksheet)
        {
            int num3;
            ushort num17;
            PreservedRecords preservedWorksheetRecords = worksheet.PreservedWorksheetRecords;
            records.Add(new XLSRecord("BOF", new object[] { BOFSubstreamType.WorksheetDialogSheet }));
            IndexRecord record = new IndexRecord();
            records.Add(record);
            ushort num = (ushort) (worksheet.Rows.MaxOutlineLevel + 1);
            ushort num2 = (ushort) (worksheet.Columns.MaxOutlineLevel + 1);
            if (preservedWorksheetRecords != null)
            {
                preservedWorksheetRecords.WriteRecords(records, "CALCCOUNT");
                preservedWorksheetRecords.WriteRecords(records, "CALCMODE");
                preservedWorksheetRecords.WriteRecords(records, "REFMODE");
                preservedWorksheetRecords.WriteRecords(records, "DELTA");
                preservedWorksheetRecords.WriteRecords(records, "ITERATION");
                preservedWorksheetRecords.WriteRecords(records, "SAVERECALC");
                preservedWorksheetRecords.WriteRecords(records, "PRINTHEADERS");
                preservedWorksheetRecords.WriteRecords(records, "PRINTGRIDLINES");
                preservedWorksheetRecords.WriteRecords(records, "GRIDSET");
            }
            records.Add(new XLSRecord("Guts", new object[] { (ushort) 0x1d, (ushort) 0x1d, num, num2 }));
            records.Add(new XLSRecord("DefaultRowHeight", new object[] { DefaultRowHeightOptions.Unsynced, (ushort) 0xff }));
            records.Add(new XLSRecord("WSBool", new object[] { worksheet.WSBoolOpt }));
            records.Add(new HorizontalPageBreaksRecord(worksheet.HorizontalPageBreaks.GetArgs()));
            records.Add(new VerticalPageBreaksRecord(worksheet.VerticalPageBreaks.GetArgs()));
            if (preservedWorksheetRecords != null)
            {
                preservedWorksheetRecords.WriteRecords(records, "HEADER");
                preservedWorksheetRecords.WriteRecords(records, "FOOTER");
                preservedWorksheetRecords.WriteRecords(records, "HCENTER");
                preservedWorksheetRecords.WriteRecords(records, "VCENTER");
                preservedWorksheetRecords.WriteRecords(records, "LEFTMARGIN");
                preservedWorksheetRecords.WriteRecords(records, "RIGHTMARGIN");
                preservedWorksheetRecords.WriteRecords(records, "TOPMARGIN");
                preservedWorksheetRecords.WriteRecords(records, "BOTTOMMARGIN");
            }
            records.Add(new XLSRecord("SETUP", new object[] { worksheet.paperSize, (ushort) worksheet.scalingFactor, worksheet.startPageNumber, worksheet.fitWorksheetWidthToPages, worksheet.fitWorksheetHeightToPages, worksheet.setupOptions, worksheet.printResolution, worksheet.verticalPrintResolution, worksheet.headerMargin, worksheet.footerMargin, worksheet.numberOfCopies }));
            records.Add(new XLSRecord("Protect", new object[] { worksheet.Protected ? ((ushort) 1) : ((ushort) 0) }));
            if (preservedWorksheetRecords != null)
            {
                preservedWorksheetRecords.WriteRecords(records, "WINDOWPROTECT");
                preservedWorksheetRecords.WriteRecords(records, "OBJECTPROTECT");
                preservedWorksheetRecords.WriteRecords(records, "SCENPROTECT");
                preservedWorksheetRecords.WriteRecords(records, "PASSWORD");
            }
            records.Add(new XLSRecord("DefaultColumnWidth", new object[] { (ushort) (worksheet.DefaultColumnWidth / 0x100) }));
            int count = worksheet.Columns.Count;
            if (count > 0x100)
            {
                throw new SpreadsheetException(string.Concat(new object[] { "Maximum number of columns in XLS file is ", 0x100, " and your file uses ", count, " columns." }));
            }
            for (num3 = 0; num3 < count; num3++)
            {
                this.WriteColumnInfoIfNeeded(records, worksheet, num3);
            }
            if (preservedWorksheetRecords != null)
            {
                preservedWorksheetRecords.WriteRecords(records, "SORT");
            }
            XLSRecord record2 = new XLSRecord("Dimensions", (object[])null);
            records.Add(record2);
            num3 = count - 1;
            while ((num3 >= 0) && worksheet.Columns[num3].IsStyleDefault)
            {
                num3--;
            }
            int num5 = num3;
            int num6 = -1;
            int num7 = -1;
            int num8 = 0x7fffffff;
            int num9 = -2147483648;
            DBCellRecord dbCell = new DBCellRecord();
            ArrayList cellRecords = new ArrayList();
            int num10 = worksheet.Rows.Count;
            if (num10 > 0x10000)
            {
                throw new SpreadsheetException(string.Concat(new object[] { "Maximum number of rows in XLS file is ", 0x10000, " and your file uses ", num10, " rows." }));
            }
            for (int i = 0; i < num10; i++)
            {
                if ((num6 != -1) && (((i - num6) % 0x20) == 0))
                {
                    WriteCellsAndDBCellRecord(records, cellRecords, ref dbCell, record);
                }
                int currentFirstColumn = 0;
                int currentLastColumn = -1;
                int currentCellsCount = worksheet.Rows[i].AllocatedCells.Count;
                AbsXLSRec rec = null;
                if (currentCellsCount > 0x100)
                {
                    throw new SpreadsheetException(string.Concat(new object[] { "Maximum number of columns in XLS file is ", 0x100, " and your file uses ", currentCellsCount, " columns." }));
                }
                for (num3 = 0; (num3 < currentCellsCount) || (num3 <= num5); num3++)
                {
                    ArrayList list2;
                    AbsXLSRec rec2 = this.CreateCellRecordIfNeeded(worksheet, i, num3, currentCellsCount, out list2);
                    if (rec2 != null)
                    {
                        cellRecords.Add(rec2);
                        if (list2 != null)
                        {
                            foreach (XLSRecord record4 in list2)
                            {
                                cellRecords.Add(record4);
                            }
                        }
                        if (currentLastColumn == -1)
                        {
                            currentFirstColumn = num3;
                            rec = rec2;
                        }
                        currentLastColumn = num3;
                    }
                }
                AbsXLSRec rec3 = this.CreateRowRecordIfNeeded(worksheet, i, currentFirstColumn, currentLastColumn);
                if (rec3 != null)
                {
                    records.Add(rec3);
                    if (dbCell.FirstRow == null)
                    {
                        dbCell.FirstRow = rec3;
                    }
                    dbCell.StartingCellsForRows.Add(rec);
                    if (num6 == -1)
                    {
                        num6 = i;
                    }
                    num7 = i;
                }
                if (currentLastColumn != -1)
                {
                    if (currentFirstColumn < num8)
                    {
                        num8 = currentFirstColumn;
                    }
                    if (currentLastColumn > num9)
                    {
                        num9 = currentLastColumn;
                    }
                }
            }
            if ((dbCell.FirstRow != null) || (dbCell.StartingCellsForRows.Count > 0))
            {
                WriteCellsAndDBCellRecord(records, cellRecords, ref dbCell, record);
            }
            if (num6 == -1)
            {
                num6 = num7 = num8 = num9 = 0;
            }
            record.FirstRow = num6;
            record.LastRowPlusOne = num7 + 1;
            if (num8 == 0x7fffffff)
            {
                record2.InitializeDelayed(new object[] { (uint) num6, (uint) (num7 + 1), (ushort) 0, (ushort) 0 });
            }
            else
            {
                record2.InitializeDelayed(new object[] { (uint) num6, (uint) (num7 + 1), (ushort) num8, (ushort) (num9 + 1) });
            }
            worksheet.Shapes.WriteOnDemand(records);
            WorksheetWindowOptions windowOptions = worksheet.windowOptions;
            if (worksheet.Parent.ActiveWorksheet == worksheet)
            {
                windowOptions = (WorksheetWindowOptions) ((ushort) (windowOptions | (WorksheetWindowOptions.SheetVisible | WorksheetWindowOptions.SheetSelected)));
            }
            ushort pageBreakViewZoom = (ushort) worksheet.pageBreakViewZoom;
            if (pageBreakViewZoom == 60)
            {
                pageBreakViewZoom = 0;
            }
            ushort zoom = (ushort) worksheet.zoom;
            if (zoom == 60)
            {
                zoom = 0;
            }
            records.Add(new Window2Record(windowOptions, worksheet.firstVisibleRow, worksheet.firstVisibleColumn, pageBreakViewZoom, zoom));
            if (worksheet.ViewOptions.ShowInPageBreakPreview)
            {
                num17 = (ushort) worksheet.pageBreakViewZoom;
            }
            else
            {
                num17 = (ushort) worksheet.zoom;
            }
            records.Add(new XLSRecord("SCL", new object[] { num17, (ushort) 100 }));
            if (preservedWorksheetRecords != null)
            {
                preservedWorksheetRecords.WriteRecords(records, "PANE");
                preservedWorksheetRecords.WriteRecords(records, "SELECTION");
                preservedWorksheetRecords.WriteRecords(records, "STANDARDWIDTH");
            }
            ArrayList list3 = new ArrayList();
            foreach (MergedCellRange range in worksheet.MergedRanges.Values)
            {
                list3.AddRange(new object[] { (ushort) range.FirstRowIndex, (ushort) range.LastRowIndex, (ushort) range.FirstColumnIndex, (ushort) range.LastColumnIndex });
                if (list3.Count == 0x1000)
                {
                    records.Add(new MergedCellsRecord(new object[] { (ushort) 0x400, (object[]) list3.ToArray(typeof(object)) }));
                    list3.Clear();
                }
            }
            if (list3.Count > 0)
            {
                records.Add(new MergedCellsRecord(new object[] { (ushort) (list3.Count / 4), (object[]) list3.ToArray(typeof(object)) }));
            }
            if (preservedWorksheetRecords != null)
            {
                preservedWorksheetRecords.WriteRecords(records, "LABELRANGES");
                preservedWorksheetRecords.WriteRecords(records, -11);
                preservedWorksheetRecords.WriteRecords(records, "HLINK");
                preservedWorksheetRecords.WriteRecords(records, "QUICKTIP");
                preservedWorksheetRecords.WriteRecords(records, "DVAL");
                preservedWorksheetRecords.WriteRecords(records, "DV");
                preservedWorksheetRecords.WriteRecords(records, "SHEETLAYOUT");
                preservedWorksheetRecords.WriteRecords(records, "SHEETPROTECTION");
                preservedWorksheetRecords.WriteRecords(records, "RANGEPROTECTION");
            }
            records.Add(new XLSRecord("EOF"));
        }

        /// <summary>
        /// Gets a value indicating whether this instance has shapes.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has shapes; otherwise, <c>false</c>.
        /// </value>
        private bool HasShapes
        {
            get
            {
                for (int i = 0; i < this.excelFile.Worksheets.Count; i++)
                {
                    if (this.excelFile.Worksheets[i].HasShape)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        private class ColorCollection : IndexedHashCollection
        {
            public override int Add(object item)
            {
                throw new Exception("Internal error: use AddColor() instead.");
            }

            public int AddColor(Color color)
            {
                if (color.ToArgb() == Color.Black.ToArgb())
                {
                    return XLSFileWriter.defaultColors.Length;
                }
                int num = base.Add(color);
                if (num > 0x38)
                {
                    throw new Exception("Maximum number of colors in XLS file is: " + 0x38);
                }
                return (num + XLSFileWriter.defaultColors.Length);
            }
        }
    }
}

