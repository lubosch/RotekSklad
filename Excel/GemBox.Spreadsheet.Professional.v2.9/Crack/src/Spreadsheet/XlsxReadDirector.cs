namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Xml;

    internal class XlsxReadDirector : XlsxDirector
    {
        private ArrayList borders;
        private ArrayList cellStyles;
        private Color[] colors;
        private ArrayList fillPatterns;
        private ArrayList fonts;
        private NumberFormatCollection numberFormats;
        private ArrayList strings;

        public XlsxReadDirector(PackageBuilderBase builder, ExcelFile excelFile) : base(builder, excelFile)
        {
            this.strings = new ArrayList();
            this.numberFormats = new NumberFormatCollection(true);
            this.fonts = new ArrayList();
            this.fillPatterns = new ArrayList();
            this.borders = new ArrayList();
            this.cellStyles = new ArrayList();
        }

        private Color ColorIndexToColor(int colorIndex)
        {
            if (colorIndex > (this.colors.Length - 1))
            {
                colorIndex = 0;
            }
            return this.colors[colorIndex];
        }

        public override void Construct()
        {
            this.ReadSharedStrings();
            this.ReadStyles();
            this.ReadWorksheets();
        }

        private void ConvertColorIndexIfNeeded(ref int colorIndex, ref Color color, Color defaultColor)
        {
            if (colorIndex != -1)
            {
                if (color.ToArgb() == defaultColor.ToArgb())
                {
                    color = this.ColorIndexToColor(colorIndex);
                }
                colorIndex = -1;
            }
        }

        private void ConvertIndexes()
        {
            foreach (ExcelFontData data in this.fonts)
            {
                this.ConvertColorIndexIfNeeded(ref data.ColorIndex, ref data.Color, Color.Black);
            }
            foreach (FillPatternData data2 in this.fillPatterns)
            {
                this.ConvertColorIndexIfNeeded(ref data2.PatternForegroundColorIndex, ref data2.PatternForegroundColor, Color.Black);
                this.ConvertColorIndexIfNeeded(ref data2.PatternBackgroundColorIndex, ref data2.PatternBackgroundColor, Color.White);
            }
            foreach (BordersData data3 in this.borders)
            {
                for (int i = 0; i < 5; i++)
                {
                    this.ConvertColorIndexIfNeeded(ref data3.BorderColorIndex[i], ref data3.BorderColor[i], Color.Black);
                }
            }
            foreach (CellStyle style in this.cellStyles)
            {
                CellStyleData element = style.Element;
                CellStyleDataIndexes indexes = element.Indexes;
                if (indexes.NumberFormatIndex > -1)
                {
                    element.NumberFormat = (string) this.numberFormats[indexes.NumberFormatIndex];
                }
                if (indexes.FontIndex > -1)
                {
                    element.FontData = (ExcelFontData) this.fonts[indexes.FontIndex];
                }
                if (indexes.FillPatternIndex > -1)
                {
                    FillPatternData data5 = (FillPatternData) this.fillPatterns[indexes.FillPatternIndex];
                    element.PatternStyle = data5.PatternStyle;
                    element.PatternForegroundColor = data5.PatternForegroundColor;
                    element.PatternBackgroundColor = data5.PatternBackgroundColor;
                }
                if (indexes.BordersIndex > -1)
                {
                    BordersData data6 = (BordersData) this.borders[indexes.BordersIndex];
                    element.BorderStyle = data6.BorderStyle;
                    element.BorderColor = data6.BorderColor;
                    element.BordersUsed = data6.BordersUsed;
                }
                element.Indexes = null;
            }
        }

        private static void ReadBorder(XmlTextReader reader, BordersData bordersData, int currentBorder)
        {
            string attribute = reader.GetAttribute("style");
            if ((attribute != null) && (attribute != "none"))
            {
                bordersData.BordersUsed |= CellBorder.MultipleFromIndividualBorder((IndividualBorder) currentBorder);
                bordersData.BorderStyle[currentBorder] = (LineStyle) Enum.Parse(typeof(LineStyle), attribute, true);
            }
        }

        private void ReadBorders(XmlTextReader reader)
        {
            if (!reader.IsEmptyElement)
            {
                BordersData bordersData = new BordersData();
                int currentBorder = -1;
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case "border":
                            {
                                if (reader.GetAttribute("diagonalUp") == "1")
                                {
                                    bordersData.BordersUsed |= MultipleBorders.DiagonalUp;
                                }
                                if (reader.GetAttribute("diagonalDown") == "1")
                                {
                                    bordersData.BordersUsed |= MultipleBorders.DiagonalDown;
                                }
                                continue;
                            }
                            case "left":
                            {
                                currentBorder = 2;
                                ReadBorder(reader, bordersData, currentBorder);
                                continue;
                            }
                            case "right":
                            {
                                currentBorder = 3;
                                ReadBorder(reader, bordersData, currentBorder);
                                continue;
                            }
                            case "top":
                            {
                                currentBorder = 0;
                                ReadBorder(reader, bordersData, currentBorder);
                                continue;
                            }
                            case "bottom":
                            {
                                currentBorder = 1;
                                ReadBorder(reader, bordersData, currentBorder);
                                continue;
                            }
                            case "diagonal":
                            {
                                currentBorder = 4;
                                string attribute = reader.GetAttribute("style");
                                if ((attribute != null) && (attribute != "none"))
                                {
                                    bordersData.BorderStyle[currentBorder] = (LineStyle) Enum.Parse(typeof(LineStyle), attribute, true);
                                }
                                continue;
                            }
                            case "color":
                            {
                                ReadColorElement(reader, ref bordersData.BorderColor[currentBorder], ref bordersData.BorderColorIndex[currentBorder]);
                                continue;
                            }
                        }
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        switch (reader.Name)
                        {
                            case "left":
                            case "right":
                            case "top":
                            case "bottom":
                            case "diagonal":
                            {
                                currentBorder = -1;
                                continue;
                            }
                            case "border":
                            {
                                this.borders.Add(bordersData);
                                bordersData = new BordersData();
                                continue;
                            }
                            case "borders":
                                return;
                        }
                    }
                }
            }
        }

        private void ReadCellXfs(XmlTextReader reader)
        {
            if (!reader.IsEmptyElement)
            {
                CellStyle style = null;
                bool applyAlignment = false;
                while (reader.Read())
                {
                    if (reader.NodeType != XmlNodeType.Element)
                    {
                        goto Label_0069;
                    }
                    string name = reader.Name;
                    if (name != null)
                    {
                        if (!(name == "xf"))
                        {
                            if (name == "alignment")
                            {
                                goto Label_0057;
                            }
                        }
                        else
                        {
                            style = this.ReadXfElement(reader, out applyAlignment);
                            this.cellStyles.Add(style);
                        }
                    }
                    continue;
                Label_0057:
                    if (applyAlignment)
                    {
                        this.ReadXfAlignment(reader, style.Element);
                    }
                    continue;
                Label_0069:
                    if ((reader.NodeType == XmlNodeType.EndElement) && (reader.Name == "cellXfs"))
                    {
                        return;
                    }
                }
            }
        }

        private static void ReadColorElement(XmlReader reader, ref Color color, ref int colorIndex)
        {
            string attribute = reader.GetAttribute("rgb");
            if (attribute != null)
            {
                color = RgbStringToColor(attribute);
            }
            else
            {
                string s = reader.GetAttribute("indexed");
                if (s != null)
                {
                    colorIndex = int.Parse(s);
                }
            }
        }

        private void ReadColors(XmlTextReader reader)
        {
            if (!reader.IsEmptyElement)
            {
                int index = 0;
                while (reader.Read())
                {
                    if ((reader.NodeType == XmlNodeType.EndElement) && (reader.Name == "colors"))
                    {
                        return;
                    }
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "rgbColor"))
                    {
                        string attribute = reader.GetAttribute("rgb");
                        if (attribute != null)
                        {
                            this.colors[index] = RgbStringToColor(attribute);
                            index++;
                        }
                    }
                }
            }
        }

        private void ReadColumnInfos(ExcelWorksheet ws, XmlReader reader)
        {
            if (!reader.IsEmptyElement)
            {
                while (reader.Read())
                {
                    if ((reader.NodeType == XmlNodeType.EndElement) && (reader.Name == "cols"))
                    {
                        return;
                    }
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "col"))
                    {
                        int num4;
                        bool flag2;
                        bool flag3;
                        int num = Utilities.GetReqAttrInt32(reader, "min");
                        int num2 = Utilities.GetReqAttrInt32(reader, "max");
                        float num3 = Utilities.GetOptAttrFloat(reader, "width", 0f);
                        bool flag = Utilities.GetOptAttrBool01(reader, "customWidth", false);
                        ReadHiddenOutlineCollapsed(reader, out num4, out flag2, out flag3);
                        if ((flag || (num4 != -1)) || (flag2 || flag3))
                        {
                            for (int i = num - 1; i <= (num2 - 1); i++)
                            {
                                ExcelColumn excelColumnRow = ws.Columns[i];
                                if (flag)
                                {
                                    excelColumnRow.Width = (int) (num3 * 256f);
                                }
                                SetOutlineCollapsedHiddenIfNeeded(excelColumnRow, num4, flag2, flag3);
                            }
                        }
                    }
                }
            }
        }

        private void ReadFills(XmlTextReader reader)
        {
            if (!reader.IsEmptyElement)
            {
                FillPatternData data = new FillPatternData();
                while (reader.Read())
                {
                    if (reader.NodeType != XmlNodeType.Element)
                    {
                        goto Label_009B;
                    }
                    string name = reader.Name;
                    if (name != null)
                    {
                        if (!(name == "patternFill"))
                        {
                            if (name == "fgColor")
                            {
                                goto Label_0073;
                            }
                            if (name == "bgColor")
                            {
                                goto Label_0087;
                            }
                        }
                        else
                        {
                            string attribute = reader.GetAttribute("patternType");
                            if (attribute != null)
                            {
                                data.PatternStyle = StringToFillPattern(attribute);
                            }
                        }
                    }
                    continue;
                Label_0073:
                    ReadColorElement(reader, ref data.PatternForegroundColor, ref data.PatternForegroundColorIndex);
                    continue;
                Label_0087:
                    ReadColorElement(reader, ref data.PatternBackgroundColor, ref data.PatternBackgroundColorIndex);
                    continue;
                Label_009B:
                    if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        if (reader.Name == "fill")
                        {
                            this.fillPatterns.Add(data);
                            data = new FillPatternData();
                        }
                        if (reader.Name == "fills")
                        {
                            return;
                        }
                    }
                }
            }
        }

        private void ReadFonts(XmlReader reader)
        {
            if (!reader.IsEmptyElement)
            {
                ExcelFontData data = new ExcelFontData(null, -1);
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case "b":
                            {
                                data.Weight = 700;
                                continue;
                            }
                            case "i":
                            {
                                data.Italic = true;
                                continue;
                            }
                            case "strike":
                            {
                                data.Strikeout = true;
                                continue;
                            }
                            case "u":
                            {
                                string attribute = reader.GetAttribute("val");
                                if (attribute != null)
                                {
                                    data.UnderlineStyle = (UnderlineStyle) Enum.Parse(typeof(UnderlineStyle), attribute, true);
                                }
                                continue;
                            }
                            case "vertAlign":
                            {
                                string reqAttrString = Utilities.GetReqAttrString(reader, "val");
                                data.ScriptPosition = (ScriptPosition) Enum.Parse(typeof(ScriptPosition), reqAttrString, true);
                                continue;
                            }
                            case "sz":
                            {
                                data.Size = 20 * Utilities.GetReqAttrInt32(reader, "val");
                                continue;
                            }
                            case "color":
                            {
                                ReadColorElement(reader, ref data.Color, ref data.ColorIndex);
                                continue;
                            }
                            case "name":
                            {
                                data.Name = Utilities.GetReqAttrString(reader, "val");
                                continue;
                            }
                        }
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        if (reader.Name == "font")
                        {
                            if (data.Size == -1)
                            {
                                data.Size = base.excelFile.DefaultFontSize;
                            }
                            if (data.Name == null)
                            {
                                data.Name = base.excelFile.DefaultFontName;
                            }
                            this.fonts.Add(data);
                            data = new ExcelFontData(null, -1);
                        }
                        if (reader.Name == "fonts")
                        {
                            return;
                        }
                    }
                }
                if (this.fonts.Count >= 1)
                {
                    data = (ExcelFontData) this.fonts[0];
                    base.excelFile.DefaultFontName = data.Name;
                    base.excelFile.DefaultFontSize = data.Size;
                }
            }
        }

        private static void ReadHiddenOutlineCollapsed(XmlReader reader, out int outlineLevel, out bool collapsed, out bool hidden)
        {
            outlineLevel = Utilities.GetOptAttrInt32(reader, "outlineLevel", -1);
            collapsed = Utilities.GetOptAttrBool01(reader, "collapsed", false);
            hidden = Utilities.GetOptAttrBool01(reader, "hidden", false);
        }

        private void ReadMergedRanges(ExcelWorksheet ws, XmlTextReader reader)
        {
            if (!reader.IsEmptyElement)
            {
                while (reader.Read())
                {
                    if ((reader.NodeType == XmlNodeType.EndElement) && (reader.Name == "mergeCells"))
                    {
                        return;
                    }
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "mergeCell"))
                    {
                        string[] strArray = Utilities.GetReqAttrString(reader, "ref").Split(new char[] { ':' }, 2);
                        ws.Cells.GetSubrange(strArray[0], strArray[1]).Merged = true;
                    }
                }
            }
        }

        private void ReadNumberFormats(XmlReader reader)
        {
            if (!reader.IsEmptyElement)
            {
                while (reader.Read())
                {
                    if ((reader.NodeType == XmlNodeType.EndElement) && (reader.Name == "numFmts"))
                    {
                        return;
                    }
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "numFmt"))
                    {
                        int index = Utilities.GetReqAttrInt32(reader, "numFmtId");
                        string reqAttrString = Utilities.GetReqAttrString(reader, "formatCode");
                        this.numberFormats.SetNumberFormat(index, reqAttrString);
                    }
                }
            }
        }

        private void ReadSharedStrings()
        {
            if (base.builder.Parts.Contains("/xl/sharedStrings.xml"))
            {
                XmlTextReader reader = new XmlTextReader(base.builder.GetPart("/xl/sharedStrings.xml"));
                try
                {
                    while (Utilities.ReadToFollowing(reader, "t"))
                    {
                        this.strings.Add(reader.ReadElementString());
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
        }

        private void ReadSheetData(ExcelWorksheet ws, XmlReader reader)
        {
            ExcelCell cell = null;
            string str = null;
            string s = null;
            object obj2;
            bool flag;
            string str6;
            int num = -1;
            CellStyle style = null;
            if (reader.IsEmptyElement)
            {
                return;
            }
        Label_0014:
            flag = true;
            if ((reader.NodeType == XmlNodeType.Element) && ((str6 = reader.Name) != null))
            {
                if (!(str6 == "row"))
                {
                    if (str6 == "c")
                    {
                        num = -1;
                        style = null;
                        string attribute = reader.GetAttribute("r");
                        if (attribute != null)
                        {
                            int num5;
                            int num6;
                            CellRange.PositionToRowColumn(attribute, out num5, out num6);
                            cell = ws.Rows[num5].AllocatedCells[num6];
                            str = reader.GetAttribute("t");
                            s = reader.GetAttribute("s");
                            if (s != null)
                            {
                                num = int.Parse(s);
                                style = (CellStyle) this.cellStyles[num];
                                cell.Style = style;
                            }
                        }
                    }
                    else
                    {
                        string str3;
                        if (str6 == "f")
                        {
                            str3 = reader.ReadElementString();
                            flag = false;
                            cell.Formula = "=" + str3;
                        }
                        else
                        {
                            if (str6 == "v")
                            {
                                int num2;
                                str3 = reader.ReadElementString();
                                flag = false;
                                if (str == null)
                                {
                                    str = "n";
                                }
                                switch (str)
                                {
                                    case "s":
                                        num2 = int.Parse(str3);
                                        obj2 = this.strings[num2];
                                        goto Label_02CC;

                                    case "b":
                                        num2 = int.Parse(str3);
                                        if (num2 != 1)
                                        {
                                            if (num2 != 0)
                                            {
                                                throw new SpreadsheetException("Error: boolean value not in correct format (0/1).");
                                            }
                                            obj2 = false;
                                        }
                                        else
                                        {
                                            obj2 = true;
                                        }
                                        goto Label_02CC;

                                    case "n":
                                        obj2 = Utilities.TryCastingToInt(double.Parse(str3, NumberStyles.Float, CultureInfo.InvariantCulture));
                                        goto Label_02CC;

                                    case "e":
                                        obj2 = str3;
                                        goto Label_02CC;

                                    case "str":
                                        obj2 = str3;
                                        goto Label_02CC;

                                    case "inlineStr":
                                        obj2 = null;
                                        goto Label_02CC;
                                }
                                obj2 = null;
                                goto Label_02CC;
                            }
                            if (str6 == "t")
                            {
                                cell.Value = reader.ReadElementString();
                                flag = false;
                            }
                        }
                    }
                }
                else
                {
                    string str4 = reader.GetAttribute("r");
                    if (str4 != null)
                    {
                        int num4;
                        bool flag3;
                        bool flag4;
                        ExcelRow excelColumnRow = ws.Rows[str4];
                        float num3 = Utilities.GetOptAttrFloat(reader, "ht", 0f);
                        if (Utilities.GetOptAttrBool01(reader, "customHeight", false))
                        {
                            excelColumnRow.Height = (int) (num3 * 20f);
                        }
                        ReadHiddenOutlineCollapsed(reader, out num4, out flag3, out flag4);
                        SetOutlineCollapsedHiddenIfNeeded(excelColumnRow, num4, flag3, flag4);
                    }
                }
            }
            goto Label_0355;
        Label_02CC:
            if (((obj2 is double) || (obj2 is int)) && (((num >= 14) && (num <= 0x16)) || ((style != null) && XLSFileReader.IsDateTime(style.Element.NumberFormat))))
            {
                if (obj2 is int)
                {
                    obj2 = (int) obj2;
                }
                obj2 = ExcelCell.ConvertExcelNumberToDateTime((double) obj2, base.excelFile.Use1904DateSystem);
            }
            cell.Value = obj2;
            object obj1 = cell.Value;
        Label_0355:
            if (flag)
            {
                reader.Read();
            }
            if ((reader.NodeType == XmlNodeType.EndElement) && (reader.Name == "sheetData"))
            {
                return;
            }
            goto Label_0014;
        }

        private void ReadSheetProperties(ExcelWorksheet ws, XmlTextReader reader)
        {
            if (!reader.IsEmptyElement)
            {
                while (reader.Read())
                {
                    if ((reader.NodeType == XmlNodeType.EndElement) && (reader.Name == "sheetPr"))
                    {
                        return;
                    }
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "outlinePr"))
                    {
                        ws.ViewOptions.OutlineRowButtonsBelow = Utilities.GetOptAttrBool01(reader, "summaryBelow", ws.ViewOptions.OutlineRowButtonsBelow);
                        ws.ViewOptions.OutlineColumnButtonsRight = Utilities.GetOptAttrBool01(reader, "summaryRight", ws.ViewOptions.OutlineColumnButtonsRight);
                    }
                }
            }
        }

        private Hashtable ReadSheetRels()
        {
            Hashtable hashtable = new Hashtable();
            XmlTextReader reader = new XmlTextReader(base.builder.GetPart("/xl/_rels/workbook.xml.rels"));
            try
            {
                while (Utilities.ReadToFollowing(reader, "Relationship"))
                {
                    if (reader.GetAttribute("Type") == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet")
                    {
                        string reqAttrString = Utilities.GetReqAttrString(reader, "Id");
                        string str2 = Utilities.GetReqAttrString(reader, "Target");
                        hashtable[reqAttrString] = str2;
                    }
                }
            }
            finally
            {
                reader.Close();
            }
            return hashtable;
        }

        private string[] ReadSheetsFromWorkbookXml(ExcelWorksheetCollection sheets, Hashtable sheetRels, out int activeSheet)
        {
            int count = sheetRels.Count;
            ArrayList list = new ArrayList();
            int num = 0;
            activeSheet = 0;
            XmlTextReader reader = new XmlTextReader(base.builder.GetPart("/xl/workbook.xml"));
            try
            {
                reader.MoveToContent();
                while (reader.Read())
                {
                    string str3;
                    string str4;
                    if ((reader.NodeType == XmlNodeType.Element) && ((str4 = reader.Name) != null))
                    {
                        if (!(str4 == "sheet"))
                        {
                            if (str4 == "workbookView")
                            {
                                goto Label_00B5;
                            }
                        }
                        else
                        {
                            string reqAttrString = Utilities.GetReqAttrString(reader, "name");
                            string str2 = Utilities.GetReqAttrString(reader, "r:id");
                            sheets.Add(reqAttrString);
                            list.Add("/xl/" + ((string) sheetRels[str2]));
                            num++;
                        }
                    }
                    continue;
                Label_00B5:
                    str3 = reader.GetAttribute("activeTab");
                    if ((str3 != null) && (str3 != ""))
                    {
                        activeSheet = int.Parse(str3);
                    }
                }
            }
            finally
            {
                reader.Close();
            }
            return (string[]) list.ToArray(typeof(string));
        }

        private void ReadStyles()
        {
            Stream part = base.builder.GetPart("/xl/styles.xml");
            XLSFileReader.LoadDefaultPalette(ref this.colors);
            XmlTextReader reader = new XmlTextReader(part);
            try
            {
                reader.MoveToContent();
                reader.Read();
                while (!reader.EOF)
                {
                    bool flag = true;
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case "numFmts":
                                this.ReadNumberFormats(reader);
                                goto Label_011F;

                            case "fonts":
                                this.ReadFonts(reader);
                                goto Label_011F;

                            case "fills":
                                this.ReadFills(reader);
                                goto Label_011F;

                            case "borders":
                                this.ReadBorders(reader);
                                goto Label_011F;

                            case "cellXfs":
                                this.ReadCellXfs(reader);
                                goto Label_011F;

                            case "colors":
                                this.ReadColors(reader);
                                goto Label_011F;
                        }
                        reader.Skip();
                        flag = false;
                    }
                Label_011F:
                    if (flag)
                    {
                        reader.Read();
                    }
                }
            }
            finally
            {
                reader.Close();
            }
            this.ConvertIndexes();
        }

        private void ReadWorksheet(ExcelWorksheet ws, string sheetPath)
        {
            XmlTextReader reader = new XmlTextReader(base.builder.GetPart(sheetPath));
            try
            {
                reader.MoveToContent();
                reader.Read();
                while (!reader.EOF)
                {
                    bool flag = true;
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        string name = reader.Name;
                        if (name == null)
                        {
                            goto Label_009A;
                        }
                        if (!(name == "sheetPr"))
                        {
                            if (name == "cols")
                            {
                                goto Label_007C;
                            }
                            if (name == "sheetData")
                            {
                                goto Label_0086;
                            }
                            if (name == "mergeCells")
                            {
                                goto Label_0090;
                            }
                            goto Label_009A;
                        }
                        this.ReadSheetProperties(ws, reader);
                    }
                    goto Label_00A2;
                Label_007C:
                    this.ReadColumnInfos(ws, reader);
                    goto Label_00A2;
                Label_0086:
                    this.ReadSheetData(ws, reader);
                    goto Label_00A2;
                Label_0090:
                    this.ReadMergedRanges(ws, reader);
                    goto Label_00A2;
                Label_009A:
                    reader.Skip();
                    flag = false;
                Label_00A2:
                    if (flag)
                    {
                        reader.Read();
                    }
                }
            }
            finally
            {
                reader.Close();
            }
        }

        private void ReadWorksheets()
        {
            int num;
            ExcelWorksheetCollection sheets = base.excelFile.Worksheets;
            Hashtable sheetRels = this.ReadSheetRels();
            string[] strArray = this.ReadSheetsFromWorkbookXml(sheets, sheetRels, out num);
            for (int i = 0; i < strArray.Length; i++)
            {
                this.ReadWorksheet(sheets[i], strArray[i]);
            }
            sheets.ActiveWorksheet = sheets[num];
        }

        private void ReadXfAlignment(XmlTextReader reader, CellStyleData csData)
        {
            string attribute = reader.GetAttribute("horizontal");
            if ((attribute != null) && (attribute != ""))
            {
                if (attribute == "centerContinuous")
                {
                    attribute = "centerAcross";
                }
                csData.HorizontalAlignment = (HorizontalAlignmentStyle) Enum.Parse(typeof(HorizontalAlignmentStyle), attribute, true);
            }
            string str2 = reader.GetAttribute("vertical");
            if ((str2 != null) && (str2 != ""))
            {
                csData.VerticalAlignment = (VerticalAlignmentStyle) Enum.Parse(typeof(VerticalAlignmentStyle), str2, true);
            }
            string s = reader.GetAttribute("indent");
            if ((s != null) && (s != ""))
            {
                csData.Indent = int.Parse(s);
            }
            string str4 = reader.GetAttribute("textRotation");
            if ((str4 != null) && (str4 != ""))
            {
                int num = int.Parse(str4);
                if ((num > 90) && (num < 0xff))
                {
                    csData.Rotation = num - 0x100;
                }
                else
                {
                    csData.Rotation = num;
                }
            }
            if (reader.GetAttribute("wrapText") == "1")
            {
                csData.WrapText = true;
            }
            if (reader.GetAttribute("shrinkToFit") == "1")
            {
                csData.ShrinkToFit = true;
            }
        }

        private CellStyle ReadXfElement(XmlTextReader reader, out bool applyAlignment)
        {
            CellStyle style = new CellStyle(base.excelFile);
            CellStyleData element = style.Element;
            element.Indexes = new CellStyleDataIndexes();
            string attribute = reader.GetAttribute("numFmtId");
            string s = reader.GetAttribute("fontId");
            string str3 = reader.GetAttribute("fillId");
            string str4 = reader.GetAttribute("borderId");
            string str5 = reader.GetAttribute("applyNumberFormat");
            string str6 = reader.GetAttribute("applyFont");
            string str7 = reader.GetAttribute("applyFill");
            string str8 = reader.GetAttribute("applyBorder");
            if ((str5 == "1") && (attribute != null))
            {
                element.Indexes.NumberFormatIndex = int.Parse(attribute);
            }
            if ((str6 == "1") && (s != null))
            {
                element.Indexes.FontIndex = int.Parse(s);
            }
            if ((str7 == "1") && (str3 != null))
            {
                element.Indexes.FillPatternIndex = int.Parse(str3);
            }
            if ((str8 == "1") && (str4 != null))
            {
                element.Indexes.BordersIndex = int.Parse(str4);
            }
            applyAlignment = Utilities.GetOptAttrBool01(reader, "applyAlignment", false);
            return style;
        }

        private static Color RgbStringToColor(string rgbStr)
        {
            return Color.FromArgb(int.Parse(rgbStr, NumberStyles.AllowHexSpecifier));
        }

        private static void SetOutlineCollapsedHiddenIfNeeded(ExcelColumnRowBase excelColumnRow, int outlineLevel, bool collapsed, bool hidden)
        {
            if (outlineLevel != -1)
            {
                excelColumnRow.OutlineLevel = outlineLevel;
            }
            if (collapsed)
            {
                excelColumnRow.Collapsed = true;
            }
            if (hidden)
            {
                excelColumnRow.Hidden = true;
            }
        }

        private static FillPatternStyle StringToFillPattern(string patternTypeStr)
        {
            int num = (int) Enum.Parse(typeof(XlsxFillStyle), patternTypeStr, true);
            return (FillPatternStyle) num;
        }
    }
}

