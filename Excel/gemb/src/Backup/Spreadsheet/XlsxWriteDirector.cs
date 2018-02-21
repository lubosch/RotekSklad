namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Xml;

    internal class XlsxWriteDirector : XlsxDirector
    {
        private IndexedHashCollection borders;
        private ArrayList cellStyles;
        private IndexedHashCollection fillPatterns;
        private IndexedHashCollection fonts;
        private NumberFormatCollection numberFormats;
        private Hashtable sourceParts;
        private IndexedHashCollection strings;

        public XlsxWriteDirector(PackageBuilderBase builder, ExcelFile excelFile) : base(builder, excelFile)
        {
            this.strings = new IndexedHashCollection();
            this.cellStyles = new ArrayList();
            this.fonts = new IndexedHashCollection();
            this.numberFormats = new NumberFormatCollection(false);
            this.fillPatterns = new IndexedHashCollection();
            this.borders = new IndexedHashCollection();
            CellStyleData element = new CellStyleData(excelFile.CellStyleCache, false, "Arial", 200);
            this.CreateCellStyleIndexes(element);
            this.fillPatterns.Add(new FillPatternData(FillPatternStyle.Gray12, Color.Black, Color.White));
        }

        private void AddSheetRels(string[] relPaths, string[] rids)
        {
            string path = "/xl/_rels/workbook.xml.rels";
            Stream part = base.excelFile.preservedXlsx.GetPart(path);
            Stream w = base.builder.CreatePart(path, "application/vnd.openxmlformats-package.relationships+xml");
            this.sourceParts.Remove(path);
            XmlTextReader reader = new XmlTextReader(part);
            try
            {
                XmlTextWriter writer = new XmlTextWriter(w, new UTF8Encoding(false));
                try
                {
                    while (reader.Read())
                    {
                        if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "Relationship"))
                        {
                            if (!(Utilities.GetReqAttrString(reader, "Type") == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet"))
                            {
                                goto Label_00F6;
                            }
                            continue;
                        }
                        if ((reader.NodeType == XmlNodeType.EndElement) && (reader.Name == "Relationships"))
                        {
                            for (int i = 0; i < rids.Length; i++)
                            {
                                writer.WriteStartElement("Relationship");
                                writer.WriteAttributeString("Id", rids[i]);
                                writer.WriteAttributeString("Type", "http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet");
                                writer.WriteAttributeString("Target", relPaths[i]);
                                writer.WriteEndElement();
                            }
                        }
                    Label_00F6:
                        WriteShallowNode(reader, writer);
                    }
                }
                finally
                {
                    writer.Close();
                }
            }
            finally
            {
                reader.Close();
            }
        }

        private void AddSheetsToWorkbookXml(ExcelWorksheetCollection sheets, string[] sheetNames, string[] rids)
        {
            string path = "/xl/workbook.xml";
            Stream part = base.excelFile.preservedXlsx.GetPart(path);
            Stream w = base.builder.CreatePart(path, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml");
            this.sourceParts.Remove(path);
            XmlTextReader reader = new XmlTextReader(part);
            try
            {
                XmlTextWriter writer = new XmlTextWriter(w, new UTF8Encoding(false));
                try
                {
                    while (reader.Read())
                    {
                        if ((reader.NodeType != XmlNodeType.Element) || (reader.Name != "sheet"))
                        {
                            if ((reader.NodeType == XmlNodeType.EndElement) && (reader.Name == "sheets"))
                            {
                                for (int i = 0; i < rids.Length; i++)
                                {
                                    writer.WriteStartElement("sheet");
                                    writer.WriteAttributeString("name", sheets[i].Name);
                                    writer.WriteAttributeString("sheetId", (i + 1).ToString());
                                    writer.WriteAttributeString("r:id", rids[i]);
                                    writer.WriteEndElement();
                                }
                                WriteShallowNode(reader, writer);
                            }
                            else if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "workbookView"))
                            {
                                writer.WriteStartElement("workbookView");
                                writer.WriteAttributeString("activeTab", sheets.GetActiveWorksheetIndex().ToString());
                                writer.WriteEndElement();
                            }
                            else
                            {
                                WriteShallowNode(reader, writer);
                            }
                        }
                    }
                }
                finally
                {
                    writer.Close();
                }
            }
            finally
            {
                reader.Close();
            }
        }

        private static string ColorToRgbString(Color color)
        {
            return color.ToArgb().ToString("X6");
        }

        public override void Construct()
        {
            this.sourceParts = base.excelFile.preservedXlsx.Parts;
            this.WriteWorksheets();
            this.WriteSharedStrings();
            this.WriteStyles();
            this.CopyTemplate();
        }

        private void CopyTemplate()
        {
            foreach (DictionaryEntry entry in this.sourceParts)
            {
                string key = (string) entry.Key;
                string contentType = (string) entry.Value;
                using (Stream stream = base.excelFile.preservedXlsx.GetPart(key))
                {
                    using (Stream stream2 = base.builder.CreatePart(key, contentType))
                    {
                        Utilities.CopyStream(stream, stream2);
                    }
                    continue;
                }
            }
        }

        private void CreateCellArguments(object val, out string typeStr, out string strVal)
        {
            typeStr = null;
            switch (val.GetType().FullName)
            {
                case "System.Byte":
                case "System.SByte":
                case "System.Int16":
                case "System.UInt16":
                case "System.UInt32":
                case "System.Int32":
                    strVal = val.ToString();
                    return;

                case "System.Single":
                    strVal = ((float) val).ToString(CultureInfo.InvariantCulture);
                    return;

                case "System.Double":
                    break;

                case "System.Decimal":
                    strVal = ((decimal) val).ToString(CultureInfo.InvariantCulture);
                    return;

                case "System.Boolean":
                    typeStr = "b";
                    if ((bool) val)
                    {
                        strVal = "1";
                        return;
                    }
                    strVal = "0";
                    return;

                case "System.String":
                    goto Label_0181;

                case "System.DateTime":
                    val = XLSFileWriter.GetEncodedDateTime((DateTime) val, base.excelFile.Use1904DateSystem);
                    break;

                default:
                    val = val.ToString();
                    goto Label_0181;
            }
            strVal = ((double) val).ToString("G17", CultureInfo.InvariantCulture);
            return;
        Label_0181:
            typeStr = "s";
            strVal = this.strings.Add((string) val).ToString();
        }

        private void CreateCellStyleIndexes(CellStyleData element)
        {
            CellStyleDataIndexes indexes = new CellStyleDataIndexes();
            indexes.CellStyleIndex = this.cellStyles.Add(element);
            indexes.FontIndex = this.fonts.Add(element.FontData);
            if (element.NumberFormat.Length == 0)
            {
                indexes.NumberFormatIndex = 0;
            }
            else
            {
                indexes.NumberFormatIndex = this.numberFormats.Add(element.NumberFormat);
            }
            FillPatternData data = new FillPatternData(element.PatternStyle, element.PatternForegroundColor, element.PatternBackgroundColor);
            indexes.FillPatternIndex = this.fillPatterns.Add(data);
            BordersData data2 = new BordersData(element.BorderColor, element.BorderStyle, element.BordersUsed);
            indexes.BordersIndex = this.borders.Add(data2);
            element.Indexes = indexes;
        }

        private static string FillPatternToString(FillPatternStyle fillPattern)
        {
            int num = (int) fillPattern;
            XlsxFillStyle style = (XlsxFillStyle) num;
            return style.ToString();
        }

        private static string FirstCharToLower(string str)
        {
            char[] chArray = str.ToCharArray();
            chArray[0] = char.ToLower(chArray[0]);
            return new string(chArray);
        }

        private int GetCellStyleIndex(CellStyle cellStyle)
        {
            CellStyleData element = cellStyle.Element;
            if (element.Indexes == null)
            {
                this.CreateCellStyleIndexes(element);
            }
            return element.Indexes.CellStyleIndex;
        }

        private void RemoveWorksheetsFromTemplate()
        {
            Hashtable hashtable = new Hashtable();
            foreach (DictionaryEntry entry in this.sourceParts)
            {
                string key = (string) entry.Key;
                string str2 = (string) entry.Value;
                if (str2 != "application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml")
                {
                    hashtable.Add(key, str2);
                }
            }
            this.sourceParts = hashtable;
        }

        private void WriteBorder(XmlTextWriter writer, string name, LineStyle style, Color color)
        {
            writer.WriteStartElement(name);
            writer.WriteAttributeString("style", FirstCharToLower(style.ToString()));
            this.WriteColorElement(writer, "color", color, Color.Black);
            writer.WriteEndElement();
        }

        private void WriteBorders(XmlTextWriter writer)
        {
            writer.WriteStartElement("borders");
            writer.WriteAttributeString("count", this.borders.Count.ToString());
            foreach (BordersData data in this.borders)
            {
                writer.WriteStartElement("border");
                if ((data.BordersUsed & MultipleBorders.DiagonalUp) != MultipleBorders.None)
                {
                    writer.WriteAttributeString("diagonalUp", "1");
                }
                if ((data.BordersUsed & MultipleBorders.DiagonalDown) != MultipleBorders.None)
                {
                    writer.WriteAttributeString("diagonalDown", "1");
                }
                for (int i = 0; i < 4; i++)
                {
                    int index = (i + 2) % 4;
                    IndividualBorder individualBorder = (IndividualBorder) index;
                    if ((data.BordersUsed & CellBorder.MultipleFromIndividualBorder(individualBorder)) != MultipleBorders.None)
                    {
                        this.WriteBorder(writer, individualBorder.ToString().ToLower(), data.BorderStyle[index], data.BorderColor[index]);
                    }
                }
                if ((data.BordersUsed & MultipleBorders.Diagonal) != MultipleBorders.None)
                {
                    this.WriteBorder(writer, "diagonal", data.BorderStyle[4], data.BorderColor[4]);
                }
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        private void WriteCellIfNeeded(ExcelCell cell, int row, int column, XmlTextWriter writer)
        {
            object obj2;
            CellStyle style;
            string formula;
            MergedCellRange mergedRange = (MergedCellRange) cell.MergedRange;
            if (mergedRange != null)
            {
                if ((row == mergedRange.FirstRowIndex) && (column == mergedRange.FirstColumnIndex))
                {
                    obj2 = mergedRange.Value;
                    formula = mergedRange.Formula;
                }
                else
                {
                    obj2 = null;
                    formula = null;
                }
                style = mergedRange.StyleResolved(row, column);
            }
            else
            {
                obj2 = cell.Value;
                if (cell.IsStyleDefault)
                {
                    style = null;
                }
                else
                {
                    style = cell.Style;
                }
                formula = cell.Formula;
            }
            if (((style != null) || (obj2 != null)) || (formula != null))
            {
                XLSFileWriter.SetDefaultDateFormatIfNeeded(obj2, ref style, base.excelFile);
                writer.WriteStartElement("c");
                writer.WriteAttributeString("r", CellRange.RowColumnToPosition(row, column));
                if (style != null)
                {
                    this.WriteCellStyle(style, writer);
                }
                string strVal = null;
                if (obj2 != null)
                {
                    string str3;
                    this.CreateCellArguments(obj2, out str3, out strVal);
                    if (str3 != null)
                    {
                        writer.WriteAttributeString("t", str3);
                    }
                }
                if (formula != null)
                {
                    this.WriteFormula(cell.FormulaInternal, writer);
                }
                if (strVal != null)
                {
                    writer.WriteElementString("v", strVal);
                }
                writer.WriteEndElement();
            }
        }

        private void WriteCellStyle(CellStyle cellStyle, XmlTextWriter writer)
        {
            writer.WriteAttributeString("s", this.GetCellStyleIndex(cellStyle).ToString());
        }

        private void WriteCellStyles(XmlTextWriter writer)
        {
            writer.WriteStartElement("cellStyles");
            writer.WriteAttributeString("count", "1");
            writer.WriteStartElement("cellStyle");
            writer.WriteAttributeString("name", "Normal");
            writer.WriteAttributeString("xfId", "0");
            writer.WriteAttributeString("builtinId", "0");
            writer.WriteEndElement();
            writer.WriteEndElement();
        }

        private void WriteCellStyleXfs(XmlTextWriter writer)
        {
            writer.WriteStartElement("cellStyleXfs");
            writer.WriteAttributeString("count", "1");
            this.WriteEmptyXfElement(writer);
            writer.WriteEndElement();
        }

        private void WriteCellXfs(XmlTextWriter writer)
        {
            writer.WriteStartElement("cellXfs");
            writer.WriteAttributeString("count", this.cellStyles.Count.ToString());
            foreach (CellStyleData data in this.cellStyles)
            {
                this.WriteComplexXfElement(writer, data);
                data.Indexes = null;
            }
            writer.WriteEndElement();
        }

        private void WriteColorElement(XmlTextWriter writer, string elementName, Color color, Color defaultColor)
        {
            if (color.ToArgb() != defaultColor.ToArgb())
            {
                writer.WriteStartElement(elementName);
                writer.WriteAttributeString("rgb", ColorToRgbString(color));
                writer.WriteEndElement();
            }
        }

        private void WriteColumnInfosIfNeeded(ExcelWorksheet ws, XmlTextWriter writer)
        {
            int count = ws.Columns.Count;
            ArrayList list = new ArrayList();
            for (int i = 0; i < count; i++)
            {
                ExcelColumn column = ws.Columns[i];
                if (((column.Width != ws.DefaultColumnWidth) || (column.OutlineLevel > 0)) || column.Collapsed)
                {
                    list.Add(i);
                }
            }
            if (list.Count > 0)
            {
                writer.WriteStartElement("cols");
                foreach (int num3 in list)
                {
                    ExcelColumn item = ws.Columns[num3];
                    writer.WriteStartElement("col");
                    string str = (num3 + 1).ToString();
                    writer.WriteAttributeString("min", str);
                    writer.WriteAttributeString("max", str);
                    writer.WriteAttributeString("width", (((float) item.Width) / 256f).ToString(CultureInfo.InvariantCulture));
                    writer.WriteAttributeString("customWidth", "1");
                    WriteHiddenOutlineCollapsedIfNeeded(writer, item, ws.ViewOptions, ws.Columns.MaxOutlineLevel);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
        }

        private void WriteComplexXfElement(XmlTextWriter writer, CellStyleData csData)
        {
            CellStyleDataIndexes indexes = csData.Indexes;
            writer.WriteStartElement("xf");
            writer.WriteAttributeString("numFmtId", indexes.NumberFormatIndex.ToString());
            writer.WriteAttributeString("fontId", indexes.FontIndex.ToString());
            writer.WriteAttributeString("fillId", indexes.FillPatternIndex.ToString());
            writer.WriteAttributeString("borderId", indexes.BordersIndex.ToString());
            if (indexes.NumberFormatIndex > 0)
            {
                writer.WriteAttributeString("applyNumberFormat", "1");
            }
            if (indexes.FontIndex > 0)
            {
                writer.WriteAttributeString("applyFont", "1");
            }
            if (indexes.FillPatternIndex > 0)
            {
                writer.WriteAttributeString("applyFill", "1");
            }
            if (indexes.BordersIndex > 0)
            {
                writer.WriteAttributeString("applyBorder", "1");
            }
            if ((((csData.HorizontalAlignment != HorizontalAlignmentStyle.General) || (csData.VerticalAlignment != VerticalAlignmentStyle.Bottom)) || ((csData.Indent != 0) || (csData.Rotation != 0))) || (csData.WrapText || csData.ShrinkToFit))
            {
                writer.WriteAttributeString("applyAlignment", "1");
                writer.WriteStartElement("alignment");
                if (csData.HorizontalAlignment != HorizontalAlignmentStyle.General)
                {
                    string str = FirstCharToLower(csData.HorizontalAlignment.ToString());
                    if (str == "centerAcross")
                    {
                        str = "centerContinuous";
                    }
                    writer.WriteAttributeString("horizontal", str);
                }
                if (csData.VerticalAlignment != VerticalAlignmentStyle.Bottom)
                {
                    writer.WriteAttributeString("vertical", FirstCharToLower(csData.VerticalAlignment.ToString()));
                }
                if (csData.Indent != 0)
                {
                    writer.WriteAttributeString("indent", csData.Indent.ToString());
                }
                if (csData.Rotation != 0)
                {
                    int num = csData.Rotation % 0x100;
                    if (num < 0)
                    {
                        num += 0x100;
                    }
                    writer.WriteAttributeString("textRotation", num.ToString());
                }
                if (csData.WrapText)
                {
                    writer.WriteAttributeString("wrapText", "1");
                }
                if (csData.ShrinkToFit)
                {
                    writer.WriteAttributeString("shrinkToFit", "1");
                }
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        private void WriteEmptyXfElement(XmlTextWriter writer)
        {
            writer.WriteStartElement("xf");
            writer.WriteAttributeString("numFmtId", "0");
            writer.WriteAttributeString("fontId", "0");
            writer.WriteAttributeString("fillId", "0");
            writer.WriteAttributeString("borderId", "0");
            writer.WriteEndElement();
        }

        private void WriteFills(XmlTextWriter writer)
        {
            writer.WriteStartElement("fills");
            writer.WriteAttributeString("count", this.fillPatterns.Count.ToString());
            foreach (FillPatternData data in this.fillPatterns)
            {
                writer.WriteStartElement("fill");
                writer.WriteStartElement("patternFill");
                writer.WriteAttributeString("patternType", FillPatternToString(data.PatternStyle));
                if (data.PatternStyle != FillPatternStyle.None)
                {
                    this.WriteColorElement(writer, "fgColor", data.PatternForegroundColor, Color.Black);
                    if (data.PatternStyle != FillPatternStyle.Solid)
                    {
                        this.WriteColorElement(writer, "bgColor", data.PatternBackgroundColor, Color.White);
                    }
                }
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        private void WriteFonts(XmlTextWriter writer)
        {
            writer.WriteStartElement("fonts");
            writer.WriteAttributeString("count", this.fonts.Count.ToString());
            foreach (ExcelFontData data in this.fonts)
            {
                writer.WriteStartElement("font");
                if (data.Weight == 700)
                {
                    writer.WriteStartElement("b");
                    writer.WriteEndElement();
                }
                if (data.Italic)
                {
                    writer.WriteStartElement("i");
                    writer.WriteEndElement();
                }
                if (data.Strikeout)
                {
                    writer.WriteStartElement("strike");
                    writer.WriteEndElement();
                }
                if (data.UnderlineStyle != UnderlineStyle.None)
                {
                    writer.WriteStartElement("u");
                    if (data.UnderlineStyle != UnderlineStyle.Single)
                    {
                        writer.WriteAttributeString("val", FirstCharToLower(data.UnderlineStyle.ToString()));
                    }
                    writer.WriteEndElement();
                }
                if (data.ScriptPosition != ScriptPosition.Normal)
                {
                    writer.WriteStartElement("vertAlign");
                    writer.WriteAttributeString("val", FirstCharToLower(data.ScriptPosition.ToString()));
                    writer.WriteEndElement();
                }
                writer.WriteStartElement("sz");
                writer.WriteAttributeString("val", (((double) data.Size) / 20.0).ToString(CultureInfo.InvariantCulture));
                writer.WriteEndElement();
                this.WriteColorElement(writer, "color", data.Color, Color.Black);
                writer.WriteStartElement("name");
                writer.WriteAttributeString("val", data.Name);
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        private void WriteFormula(CellFormula cellFormula, XmlTextWriter writer)
        {
            string text = cellFormula.Formula.Substring(1);
            writer.WriteStartElement("f");
            writer.WriteAttributeString("ca", "1");
            writer.WriteString(text);
            writer.WriteEndElement();
        }

        private static void WriteHiddenOutlineCollapsedIfNeeded(XmlTextWriter writer, ExcelColumnRowBase item, ExcelViewOptions viewOptions, int maxOutlineLevel)
        {
            bool hidden = item.Hidden;
            if (item.OutlineLevel > 0)
            {
                writer.WriteAttributeString("outlineLevel", item.OutlineLevel.ToString());
            }
            if (item.Collapsed)
            {
                writer.WriteAttributeString("collapsed", "1");
                if (viewOptions.ShowOutlineSymbols && (maxOutlineLevel > 0))
                {
                    hidden = true;
                }
            }
            if (hidden)
            {
                writer.WriteAttributeString("hidden", "1");
            }
        }

        private static void WriteMergedRangesIfNeeded(XmlTextWriter writer, ICollection values)
        {
            if (values.Count > 0)
            {
                writer.WriteStartElement("mergeCells");
                writer.WriteAttributeString("count", values.Count.ToString());
                foreach (MergedCellRange range in values)
                {
                    string str = range.StartPosition + ":" + range.EndPosition;
                    writer.WriteStartElement("mergeCell");
                    writer.WriteAttributeString("ref", str);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
        }

        private void WriteNumberFormats(XmlTextWriter writer)
        {
            int num = this.numberFormats.Count - 0xa4;
            if (num > 0)
            {
                writer.WriteStartElement("numFmts");
                writer.WriteAttributeString("count", num.ToString());
                for (int i = 0xa4; i < this.numberFormats.Count; i++)
                {
                    string str = (string) this.numberFormats[i];
                    writer.WriteStartElement("numFmt");
                    writer.WriteAttributeString("numFmtId", i.ToString());
                    writer.WriteAttributeString("formatCode", str);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
        }

        private static void WriteShallowNode(XmlReader reader, XmlWriter writer)
        {
            switch (reader.NodeType)
            {
                case XmlNodeType.Element:
                    writer.WriteStartElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
                    writer.WriteAttributes(reader, true);
                    if (!reader.IsEmptyElement)
                    {
                        break;
                    }
                    writer.WriteEndElement();
                    return;

                case XmlNodeType.Attribute:
                case XmlNodeType.Entity:
                case XmlNodeType.Document:
                case XmlNodeType.DocumentFragment:
                case XmlNodeType.Notation:
                case XmlNodeType.EndEntity:
                    break;

                case XmlNodeType.Text:
                    writer.WriteString(reader.Value);
                    return;

                case XmlNodeType.CDATA:
                    writer.WriteCData(reader.Value);
                    return;

                case XmlNodeType.EntityReference:
                    writer.WriteEntityRef(reader.Name);
                    return;

                case XmlNodeType.ProcessingInstruction:
                case XmlNodeType.XmlDeclaration:
                    writer.WriteProcessingInstruction(reader.Name, reader.Value);
                    return;

                case XmlNodeType.Comment:
                    writer.WriteComment(reader.Value);
                    return;

                case XmlNodeType.DocumentType:
                    writer.WriteDocType(reader.Name, reader.GetAttribute("PUBLIC"), reader.GetAttribute("SYSTEM"), reader.Value);
                    return;

                case XmlNodeType.Whitespace:
                case XmlNodeType.SignificantWhitespace:
                    writer.WriteWhitespace(reader.Value);
                    return;

                case XmlNodeType.EndElement:
                    writer.WriteFullEndElement();
                    break;

                default:
                    return;
            }
        }

        private void WriteSharedStrings()
        {
            Stream w = base.builder.CreatePart("/xl/sharedStrings.xml", "application/vnd.openxmlformats-officedocument.spreadsheetml.sharedStrings+xml");
            this.sourceParts.Remove("/xl/sharedStrings.xml");
            XmlTextWriter writer = new XmlTextWriter(w, new UTF8Encoding(false));
            try
            {
                writer.WriteStartDocument(true);
                writer.WriteStartElement("sst");
                writer.WriteAttributeString("xmlns", "http://schemas.openxmlformats.org/spreadsheetml/2006/main");
                string str = this.strings.Count.ToString();
                writer.WriteAttributeString("count", str);
                writer.WriteAttributeString("uniqueCount", str);
                foreach (string str2 in this.strings)
                {
                    writer.WriteStartElement("si");
                    writer.WriteElementString("t", str2);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            finally
            {
                writer.Close();
            }
        }

        private void WriteStyles()
        {
            Stream w = base.builder.CreatePart("/xl/styles.xml", "application/vnd.openxmlformats-officedocument.spreadsheetml.styles+xml");
            this.sourceParts.Remove("/xl/styles.xml");
            XmlTextWriter writer = new XmlTextWriter(w, new UTF8Encoding(false));
            try
            {
                writer.WriteStartDocument(true);
                writer.WriteStartElement("styleSheet");
                writer.WriteAttributeString("xmlns", "http://schemas.openxmlformats.org/spreadsheetml/2006/main");
                this.WriteNumberFormats(writer);
                this.WriteFonts(writer);
                this.WriteFills(writer);
                this.WriteBorders(writer);
                this.WriteCellStyleXfs(writer);
                this.WriteCellXfs(writer);
                this.WriteCellStyles(writer);
                this.WriteTableStyles(writer);
                writer.WriteEndElement();
            }
            finally
            {
                writer.Close();
            }
        }

        private void WriteTableStyles(XmlTextWriter writer)
        {
            writer.WriteStartElement("tableStyles");
            writer.WriteAttributeString("count", "0");
            writer.WriteAttributeString("defaultTableStyle", "TableStyleMedium9");
            writer.WriteAttributeString("defaultPivotStyle", "PivotStyleLight16");
            writer.WriteEndElement();
        }

        private void WriteWorksheet(ExcelWorksheet ws, string partName)
        {
            XmlTextWriter writer = new XmlTextWriter(base.builder.CreatePart(partName, "application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml"), new UTF8Encoding(false));
            try
            {
                writer.WriteStartDocument(true);
                writer.WriteStartElement("worksheet");
                writer.WriteAttributeString("xmlns", "http://schemas.openxmlformats.org/spreadsheetml/2006/main");
                writer.WriteAttributeString("xmlns:r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
                if (!ws.ViewOptions.OutlineRowButtonsBelow || !ws.ViewOptions.OutlineColumnButtonsRight)
                {
                    writer.WriteStartElement("sheetPr");
                    writer.WriteStartElement("outlinePr");
                    if (!ws.ViewOptions.OutlineRowButtonsBelow)
                    {
                        writer.WriteAttributeString("summaryBelow", "0");
                    }
                    if (!ws.ViewOptions.OutlineColumnButtonsRight)
                    {
                        writer.WriteAttributeString("summaryRight", "0");
                    }
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
                writer.WriteStartElement("sheetViews");
                writer.WriteStartElement("sheetView");
                if (ws == base.excelFile.Worksheets.ActiveWorksheet)
                {
                    writer.WriteAttributeString("tabSelected", "1");
                }
                writer.WriteAttributeString("workbookViewId", "0");
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteStartElement("sheetFormatPr");
                writer.WriteAttributeString("defaultRowHeight", "15");
                writer.WriteAttributeString("defaultColWidth", "10.32");
                if (ws.Rows.MaxOutlineLevel > 0)
                {
                    writer.WriteAttributeString("outlineLevelRow", ws.Rows.MaxOutlineLevel.ToString());
                }
                if (ws.Columns.MaxOutlineLevel > 0)
                {
                    writer.WriteAttributeString("outlineLevelCol", ws.Rows.MaxOutlineLevel.ToString());
                }
                writer.WriteEndElement();
                this.WriteColumnInfosIfNeeded(ws, writer);
                this.WriteWorksheetData(ws, writer);
                WriteMergedRangesIfNeeded(writer, ws.MergedRanges.Values);
                writer.WriteStartElement("pageMargins");
                writer.WriteAttributeString("left", "0.7");
                writer.WriteAttributeString("right", "0.7");
                writer.WriteAttributeString("top", "0.75");
                writer.WriteAttributeString("bottom", "0.75");
                writer.WriteAttributeString("header", "0.3");
                writer.WriteAttributeString("footer", "0.3");
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
            finally
            {
                writer.Close();
            }
        }

        private void WriteWorksheetData(ExcelWorksheet ws, XmlTextWriter writer)
        {
            writer.WriteStartElement("sheetData");
            int count = ws.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                ExcelRow item = ws.Rows[i];
                ExcelCellCollection allocatedCells = item.AllocatedCells;
                int num3 = allocatedCells.Count;
                writer.WriteStartElement("row");
                writer.WriteAttributeString("r", ExcelRowCollection.RowIndexToName(i));
                if (item.Height != 0xff)
                {
                    writer.WriteAttributeString("ht", (item.Height / 20).ToString());
                    writer.WriteAttributeString("customHeight", "1");
                }
                WriteHiddenOutlineCollapsedIfNeeded(writer, item, ws.ViewOptions, ws.Rows.MaxOutlineLevel);
                for (int j = 0; j < num3; j++)
                {
                    this.WriteCellIfNeeded(allocatedCells[j], i, j, writer);
                }
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        private void WriteWorksheets()
        {
            ExcelWorksheetCollection sheets = base.excelFile.Worksheets;
            int count = sheets.Count;
            string[] sheetNames = new string[count];
            string[] relPaths = new string[count];
            string[] strArray3 = new string[count];
            string[] rids = new string[count];
            for (int i = 0; i < count; i++)
            {
                sheetNames[i] = "Sheet" + (i + 1);
                relPaths[i] = "worksheets/" + sheetNames[i] + ".xml";
                strArray3[i] = "/xl/" + relPaths[i];
                rids[i] = "rIdWs" + i;
                this.WriteWorksheet(sheets[i], strArray3[i]);
            }
            this.RemoveWorksheetsFromTemplate();
            this.AddSheetRels(relPaths, rids);
            this.AddSheetsToWorkbookXml(sheets, sheetNames, rids);
        }
    }
}

