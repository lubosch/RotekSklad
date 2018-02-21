namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;

    internal sealed class XLSDescriptors
    {
        private static Hashtable code2Descriptor = new Hashtable();
        public static XLSDescriptor[] List = new XLSDescriptor[] { 
            new XLSDescriptor("BOF", 0x809, 0x10, "XLSRecord", new object[] { "00 06", typeof(BOFSubstreamType), "BB 0D", "CC 07", "00 00 00 00", "00 06 00 00" }), new XLSDescriptor("Protect", 0x12, 2, "XLSRecord", new object[] { typeof(ushort) }), new XLSDescriptor("DefaultRowHeight", 0x225, 4, "XLSRecord", new object[] { typeof(DefaultRowHeightOptions), typeof(ushort) }), new XLSDescriptor("DefaultColumnWidth", 0x55, 2, "XLSRecord", new object[] { typeof(ushort) }), new XLSDescriptor("Window1", 0x3d, 0x12, "XLSRecord", new object[] { typeof(ushort), typeof(ushort), typeof(ushort), typeof(ushort), typeof(Window1Options), typeof(ushort), typeof(ushort), typeof(ushort), typeof(ushort) }), new XLSDescriptor("Font", 0x31, -1, "XLSRecord", new object[] { typeof(ushort), typeof(FontOptions), typeof(ushort), typeof(ushort), typeof(ushort), typeof(byte), "00", "00", "00", typeof(ExcelShortString) }), new XLSDescriptor("Format", 0x41e, -1, "XLSRecord", new object[] { typeof(ushort), typeof(ExcelLongString) }), new XLSDescriptor("XF", 0xe0, 20, "XLSRecord", new object[] { typeof(ushort), typeof(ushort), typeof(XFOptions1), typeof(byte), typeof(byte), typeof(ushort), typeof(ushort), typeof(ushort), typeof(uint), typeof(ushort) }), new XLSDescriptor("Style", 0x293, -1, "StyleRecord", new object[] { typeof(ushort), typeof(byte), typeof(byte) }), new XLSDescriptor("Palette", 0x92, -1, "PaletteRecord", new object[] { typeof(ushort), new object[] { typeof(byte), typeof(byte), typeof(byte), typeof(byte) } }), new XLSDescriptor("Index", 0x20b, -1, "IndexRecord", new object[] { "00 00 00 00", typeof(uint), typeof(uint), "00 00 00 00", new object[] { typeof(uint) } }), new XLSDescriptor("ColumnInfo", 0x7d, 12, "XLSRecord", new object[] { typeof(ushort), typeof(ushort), typeof(ushort), typeof(ushort), typeof(ColumnInfoOptions), "00 00" }), new XLSDescriptor("Dimensions", 0x200, 14, "XLSRecord", new object[] { typeof(uint), typeof(uint), typeof(ushort), typeof(ushort), "00 00" }), new XLSDescriptor("Row", 520, 0x10, "RowRecord", new object[] { typeof(ushort), typeof(ushort), typeof(ushort), typeof(ushort), "00 00", "00 00", typeof(RowOptions), typeof(ushort) }), new XLSDescriptor("LabelSST", 0xfd, 10, "LabelSSTRecord", new object[] { typeof(CellRecordHeader), typeof(uint) }), new XLSDescriptor("BoolErr", 0x205, 8, "XLSRecord", new object[] { typeof(CellRecordHeader), typeof(byte), typeof(byte) }), 
            new XLSDescriptor("Blank", 0x201, 6, "XLSRecord", new object[] { typeof(CellRecordHeader) }), new XLSDescriptor("MulBlank", 190, -1, "MulBlankRecord", new object[] { typeof(ushort), typeof(ushort), new object[] { typeof(ushort) }, typeof(ushort) }), new XLSDescriptor("DBCell", 0xd7, -1, "DBCellRecord", new object[] { typeof(uint), new object[] { typeof(ushort) } }), new XLSDescriptor("RK", 0x27e, 10, "RKRecord", new object[] { typeof(CellRecordHeader), typeof(uint) }), new XLSDescriptor("MulRK", 0xbd, -1, "MulRKRecord", new object[] { typeof(ushort), typeof(ushort), new object[] { typeof(ushort), typeof(uint) }, typeof(ushort) }), new XLSDescriptor("Number", 0x203, 14, "XLSRecord", new object[] { typeof(CellRecordHeader), typeof(double) }), new XLSDescriptor("Formula", 6, -1, "FormulaRecord", new object[] { typeof(CellRecordHeader), new object[] { typeof(byte) }, typeof(FormulaOptions), "00 00 00 00", typeof(ushort), new object[] { typeof(byte) } }), new XLSDescriptor("BoundSheet", 0x85, -1, "BoundSheetRecord", new object[] { typeof(uint), typeof(BoundSheetVisibility), typeof(BoundSheetSheetType), typeof(ExcelShortString) }), new XLSDescriptor("SST", 0xfc, -1, "SSTRecord", new object[] { typeof(uint), typeof(uint), typeof(ExcelLongStrings) }), new XLSDescriptor("Continue", 60, -1, "ContinueRecord", new object[] { typeof(ExcelLongStrings) }), new XLSDescriptor("ExtSST", 0xff, -1, "ExtSSTRecord", new object[] { typeof(ushort), new object[] { typeof(uint), typeof(ushort), "00 00" } }), new XLSDescriptor("Window2", 0x23e, -1, "Window2Record", null), new XLSDescriptor("MergedCells", 0xe5, -1, "MergedCellsRecord", new object[] { typeof(ushort), new object[] { typeof(ushort), typeof(ushort), typeof(ushort), typeof(ushort) } }), new XLSDescriptor("WSBool", 0x81, 2, "XLSRecord", new object[] { typeof(WSBoolOptions) }), new XLSDescriptor("Guts", 0x80, 8, "XLSRecord", new object[] { typeof(ushort), typeof(ushort), typeof(ushort), typeof(ushort) }), new XLSDescriptor("HORIZONTALPAGEBREAKS", 0x1b, -1, "HorizontalPageBreaksRecord", new object[] { typeof(ushort), new object[] { typeof(ushort), typeof(ushort), typeof(ushort) } }), 
            new XLSDescriptor("VERTICALPAGEBREAKS", 0x1a, -1, "VerticalPageBreaksRecord", new object[] { typeof(ushort), new object[] { typeof(ushort), typeof(ushort), typeof(ushort) } }), new XLSDescriptor("SCL", 160, 4, "XLSRecord", new object[] { typeof(ushort), typeof(ushort) }), new XLSDescriptor("SETUP", 0xa1, 0x22, "XLSRecord", new object[] { typeof(ushort), typeof(ushort), typeof(ushort), typeof(ushort), typeof(ushort), typeof(SetupOptions), typeof(ushort), typeof(ushort), typeof(double), typeof(double), typeof(ushort) }), new XLSDescriptor("EXTERNSHEET", 0x17, -1, "ExternsheetRecord", new object[] { typeof(ushort), new object[] { typeof(SheetIndexes) } }), new XLSDescriptor("NAME", 0x18, -1, "NameRecord", new object[] { new object[] { typeof(byte) }, typeof(byte), typeof(ushort), "00 00", typeof(ushort), "00 00 00 00", typeof(ExcelStringWithoutLength), new object[] { typeof(byte) } }), new XLSDescriptor("SUPBOOK", 430, 4, "SupBookRecord", new object[] { typeof(ushort), "01 04" }), new XLSDescriptor("EOF", 10, 0, "XLSRecord", null), new XLSDescriptor("MSODRAWING", 0xec, -1, "MsoDrawingRecord", new object[] { typeof(MsoBaseRecord) }), new XLSDescriptor("MSODRAWINGGROUP", 0xeb, -1, "MsoDrawingGroupRecord", new object[] { typeof(MsoBaseRecord) }), new XLSDescriptor("MSOFBTCLIENTDATA", 0xf011, -1, "MsofbtClientDataRecord", null), new XLSDescriptor("MSOFBTCLIENTTEXTBOX", 0xf00d, -1, "MsofbtClientTextBoxRecord", new object[] { new object[] { typeof(byte) } }), new XLSDescriptor("MSOFBTSPGR", 0xf009, -1, "MsofbtSpgrRecord", null), new XLSDescriptor("MSOFBTSPCONTAINER", 0xf004, -1, "MsoContainerRecord", null), new XLSDescriptor("MSOFBTDG", 0xf008, -1, "MsofbtDgRecord", null), new XLSDescriptor("MsofbtOptRecord", 0xf00b, -1, "MsofbtOptRecord", null), new XLSDescriptor("MSOFBTDGCONTAINER", 0xf002, -1, "MsoContainerRecord", null), 
            new XLSDescriptor("MSOFBTCLIENTANCHOR", 0xf010, -1, "MsofbtClientAnchorRecord", new object[] { "00 00 00 00", typeof(uint), typeof(uint), typeof(uint), typeof(uint) }), new XLSDescriptor("MSOFBTSP", 0xf00a, -1, "MsofbtSpRecord", new object[] { typeof(uint), typeof(byte), typeof(byte), "00 00 00 00 00 00" }), new XLSDescriptor("MSOFBTBSTORECONTAINER", 0xf001, -1, "MsoContainerRecord", null), new XLSDescriptor("MSOFBTDGG", 0xf006, -1, "MsofbtDggRecord", null), new XLSDescriptor("MSOFBTDGGCONTAINER", 0xf000, -1, "MsoContainerRecord", null), new XLSDescriptor("MSOFBTSPGRCONTAINER", 0xf003, -1, "MsoContainerRecord", null), new XLSDescriptor("MSOFBTBSE", 0xf007, -1, "MsofbtBseRecord", new object[] { "06", "06", typeof(uint), "01 00 00 00", "00 00 00 00", "00", typeof(byte), "00", "00", typeof(ExcelStringBase), typeof(byte[]) }), new XLSDescriptor("NOTE", 0x1c, -1, "NoteRecord", new object[] { typeof(ushort), typeof(ushort), typeof(ushort), typeof(ushort) }), new XLSDescriptor("OBJ", 0x5d, -1, "ObjRecord", new object[] { new object[] { typeof(byte) } }), new XLSDescriptor("HASBASIC", 0xd3, 0, "XLSRecord", null), new XLSDescriptor("TXO", 0x1b6, -1, "TxoRecord", new object[] { new object[] { typeof(byte) } }), new XLSDescriptor("DATEMODE", 0x22, 2, "XLSRecord", new object[] { typeof(ushort) }), new XLSDescriptor("WRITEPROT", 0x86, -2, "XLSRecord", null), new XLSDescriptor("WRITEACCESS", 0x5c, -2, "XLSRecord", null), new XLSDescriptor("FILESHARING", 0x5b, -2, "XLSRecord", null), new XLSDescriptor("CODEPAGE", 0x42, -2, "XLSRecord", null), 
            new XLSDescriptor("WINDOWPROTECT", 0x19, -2, "XLSRecord", null), new XLSDescriptor("OBJECTPROTECT", 0x63, -2, "XLSRecord", null), new XLSDescriptor("HIDEOBJ", 0x8d, -2, "XLSRecord", null), new XLSDescriptor("PRECISION", 14, -2, "XLSRecord", null), new XLSDescriptor("REFRESHALL", 0x1b7, -2, "XLSRecord", null), new XLSDescriptor("BOOKBOOL", 0xda, -2, "XLSRecord", null), new XLSDescriptor("USESELFS", 0x160, -2, "XLSRecord", null), new XLSDescriptor("COUNTRY", 140, -2, "XLSRecord", null), new XLSDescriptor("CALCCOUNT", 12, -2, "XLSRecord", null), new XLSDescriptor("CALCMODE", 13, -2, "XLSRecord", null), new XLSDescriptor("REFMODE", 15, -2, "XLSRecord", null), new XLSDescriptor("DELTA", 0x10, -2, "XLSRecord", null), new XLSDescriptor("ITERATION", 0x11, -2, "XLSRecord", null), new XLSDescriptor("SAVERECALC", 0x5f, -2, "XLSRecord", null), new XLSDescriptor("PRINTHEADERS", 0x2a, -2, "XLSRecord", null), new XLSDescriptor("PRINTGRIDLINES", 0x2b, -2, "XLSRecord", null), 
            new XLSDescriptor("GRIDSET", 130, -2, "XLSRecord", null), new XLSDescriptor("HEADER", 20, -2, "XLSRecord", null), new XLSDescriptor("FOOTER", 0x15, -2, "XLSRecord", null), new XLSDescriptor("HCENTER", 0x83, -2, "XLSRecord", null), new XLSDescriptor("VCENTER", 0x84, -2, "XLSRecord", null), new XLSDescriptor("LEFTMARGIN", 0x26, -2, "XLSRecord", null), new XLSDescriptor("RIGHTMARGIN", 0x27, -2, "XLSRecord", null), new XLSDescriptor("TOPMARGIN", 40, -2, "XLSRecord", null), new XLSDescriptor("BOTTOMMARGIN", 0x29, -2, "XLSRecord", null), new XLSDescriptor("SORT", 0x90, -2, "XLSRecord", null), new XLSDescriptor("PANE", 0x41, -2, "XLSRecord", null), new XLSDescriptor("SELECTION", 0x1d, -2, "XLSRecord", null), new XLSDescriptor("STANDARDWIDTH", 0x99, -2, "XLSRecord", null), new XLSDescriptor("LABELRANGES", 0x15f, -2, "XLSRecord", null), new XLSDescriptor("CONDFMT", 0x1b0, -2, "XLSRecord", null), new XLSDescriptor("CF", 0x1b1, -2, "XLSRecord", null), 
            new XLSDescriptor("HLINK", 440, -2, "XLSRecord", null), new XLSDescriptor("QUICKTIP", 0x800, -2, "XLSRecord", null), new XLSDescriptor("DVAL", 0x1b2, -2, "XLSRecord", null), new XLSDescriptor("DV", 0x1be, -2, "XLSRecord", null), new XLSDescriptor("SHEETLAYOUT", 0x862, -2, "XLSRecord", null), new XLSDescriptor("SHEETPROTECTION", 0x867, -2, "XLSRecord", null), new XLSDescriptor("RANGEPROTECTION", 0x868, -2, "XLSRecord", null), new XLSDescriptor("SCENPROTECT", 0xdd, -2, "XLSRecord", null), new XLSDescriptor("PASSWORD", 0x13, -2, "XLSRecord", null), new XLSDescriptor("SHRFMLA", 0x4bc, -2, "XLSRecord", null), new XLSDescriptor("STRING", 0x207, -1, "XLSRecord", new object[] { typeof(ExcelLongString) }), new XLSDescriptor("FILEPASS", 0x2f, -2, "XLSRecord", null), new XLSDescriptor("MSODRAWINGSELECTION", 0xed, -2, "XLSRecord", null)
         };
        private static Hashtable name2Descriptor = new Hashtable();

        static XLSDescriptors()
        {
            foreach (XLSDescriptor descriptor in List)
            {
                name2Descriptor.Add(descriptor.Name, descriptor);
                code2Descriptor.Add(descriptor.Code, descriptor);
            }
        }

        private XLSDescriptors()
        {
        }

        public static byte[] Format(object[] format, object[] args)
        {
            MemoryStream stream;
            int currentArgIndex = 0;
            using (stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream, new UnicodeEncoding()))
                {
                    FormatHelper(writer, format, args, ref currentArgIndex);
                    stream.Capacity = (int) stream.Length;
                }
            }
            return stream.GetBuffer();
        }

        private static void FormatHelper(BinaryWriter bw, object[] format, object[] args, ref int currentArgIndex)
        {
            foreach (object obj2 in format)
            {
                if (obj2 is string)
                {
                    bw.Write(Utilities.HexStr2ByteArr((string) obj2));
                }
                else if (obj2 is Type)
                {
                    object obj3 = args[currentArgIndex];
                    Type enumType = (Type) obj2;
                    if (enumType.IsEnum)
                    {
                        enumType = Enum.GetUnderlyingType(enumType);
                    }
                    switch (enumType.FullName)
                    {
                        case "System.Int16":
                            bw.Write((short) obj3);
                            break;

                        case "System.UInt16":
                            bw.Write((ushort) obj3);
                            break;

                        case "System.UInt32":
                            bw.Write((uint) obj3);
                            break;

                        case "System.UInt64":
                            bw.Write((ulong) obj3);
                            break;

                        case "System.Byte":
                            bw.Write((byte) obj3);
                            break;

                        case "System.Char":
                            bw.Write((char) obj3);
                            break;

                        case "System.Single":
                            bw.Write((float) obj3);
                            break;

                        case "System.Double":
                            bw.Write((double) obj3);
                            break;

                        case "GemBox.Spreadsheet.ExcelShortString":
                            ((ExcelShortString) obj3).Write(bw);
                            break;

                        case "GemBox.Spreadsheet.ExcelLongString":
                            ((ExcelLongString) obj3).Write(bw);
                            break;

                        case "GemBox.Spreadsheet.ExcelStringWithoutLength":
                            ((ExcelStringWithoutLength) obj3).Write(bw);
                            break;

                        case "GemBox.Spreadsheet.ExcelLongStrings":
                            ((ExcelLongStrings) obj3).Write(bw);
                            break;

                        case "GemBox.Spreadsheet.CellRecordHeader":
                            ((CellRecordHeader) obj3).Write(bw);
                            break;

                        case "GemBox.Spreadsheet.SheetIndexes":
                            ((SheetIndexes) obj3).Write(bw);
                            break;

                        default:
                            throw new Exception("Internal error: unsupported type in format: " + enumType.FullName);
                    }
                    currentArgIndex++;
                }
                else
                {
                    if (!(obj2 is object[]))
                    {
                        throw new Exception("Internal error: wrong format in descriptor.");
                    }
                    object[] objArray = args[currentArgIndex] as object[];
                    int num = 0;
                    while (num < objArray.Length)
                    {
                        FormatHelper(bw, (object[]) obj2, objArray, ref num);
                    }
                    currentArgIndex++;
                }
            }
        }

        public static XLSDescriptor GetByCode(int code)
        {
            return (XLSDescriptor) code2Descriptor[code];
        }

        public static XLSDescriptor GetByName(string name)
        {
            return (XLSDescriptor) name2Descriptor[name];
        }

        public static object[] Parse(object[] format, byte[] body, VariableArrayCountDelegate vaCount, StringLengthDelegate lastStringLength)
        {
            if (format == null)
            {
                return new object[0];
            }
            object[] loadedArgs = new object[format.Length];
            int currentArgIndex = 0;
            using (MemoryStream stream = new MemoryStream(body))
            {
                using (BinaryReader reader = new BinaryReader(stream, new UnicodeEncoding()))
                {
                    ParseHelper(reader, format, ref loadedArgs, ref currentArgIndex, vaCount, lastStringLength);
                }
            }
            object[] destinationArray = new object[currentArgIndex];
            Array.Copy(loadedArgs, destinationArray, currentArgIndex);
            return destinationArray;
        }

        public static void ParseHelper(BinaryReader br, object[] format, ref object[] loadedArgs, ref int currentArgIndex, VariableArrayCountDelegate vaCount, StringLengthDelegate lastStringLength)
        {
            foreach (object obj2 in format)
            {
                if (obj2 is string)
                {
                    Stream baseStream = br.BaseStream;
                    baseStream.Position += Utilities.GetByteArrLengthFromHexStr((string) obj2);
                }
                else if (obj2 is Type)
                {
                    Type enumType = (Type) obj2;
                    if (enumType.IsEnum)
                    {
                        enumType = Enum.GetUnderlyingType(enumType);
                    }
                    switch (enumType.FullName)
                    {
                        case "System.Int16":
                            loadedArgs[currentArgIndex] = br.ReadInt16();
                            break;

                        case "System.UInt16":
                            loadedArgs[currentArgIndex] = br.ReadUInt16();
                            break;

                        case "System.UInt32":
                            loadedArgs[currentArgIndex] = br.ReadUInt32();
                            break;

                        case "System.UInt64":
                            loadedArgs[currentArgIndex] = br.ReadUInt64();
                            break;

                        case "System.Byte":
                            loadedArgs[currentArgIndex] = br.ReadByte();
                            break;

                        case "System.Char":
                            loadedArgs[currentArgIndex] = br.ReadChar();
                            break;

                        case "System.Single":
                            loadedArgs[currentArgIndex] = br.ReadSingle();
                            break;

                        case "System.Double":
                            loadedArgs[currentArgIndex] = br.ReadDouble();
                            break;

                        case "GemBox.Spreadsheet.ExcelShortString":
                            loadedArgs[currentArgIndex] = new ExcelShortString(br);
                            break;

                        case "GemBox.Spreadsheet.ExcelLongString":
                            loadedArgs[currentArgIndex] = new ExcelLongString(br);
                            break;

                        case "GemBox.Spreadsheet.ExcelStringWithoutLength":
                            loadedArgs[currentArgIndex] = new ExcelStringWithoutLength(br, lastStringLength(loadedArgs));
                            break;

                        case "GemBox.Spreadsheet.ExcelLongStrings":
                        {
                            int remainingSize = vaCount(loadedArgs, null, (int) br.BaseStream.Length);
                            loadedArgs[currentArgIndex] = new ExcelLongStrings(br, remainingSize, null);
                            break;
                        }
                        case "GemBox.Spreadsheet.CellRecordHeader":
                            loadedArgs[currentArgIndex] = new CellRecordHeader(br);
                            break;

                        case "GemBox.Spreadsheet.SheetIndexes":
                            loadedArgs[currentArgIndex] = new SheetIndexes(br);
                            break;

                        case "GemBox.Spreadsheet.MsoBaseRecord":
                            loadedArgs[currentArgIndex] = ImageRecordsFactory.CreateFromReader(br);
                            break;

                        default:
                            throw new Exception("Internal error: unsupported type in format: " + enumType.FullName);
                    }
                    currentArgIndex++;
                }
                else
                {
                    if (!(obj2 is object[]))
                    {
                        throw new Exception("Internal error: wrong format in descriptor.");
                    }
                    object[] varArr = (object[]) obj2;
                    int num2 = vaCount(loadedArgs, varArr, (int) br.BaseStream.Length);
                    object[] objArray2 = new object[varArr.Length * num2];
                    int num3 = 0;
                    for (int i = 0; i < num2; i++)
                    {
                        ParseHelper(br, varArr, ref objArray2, ref num3, vaCount, lastStringLength);
                    }
                    object[] destinationArray = new object[num3];
                    Array.Copy(objArray2, destinationArray, num3);
                    loadedArgs[currentArgIndex] = destinationArray;
                    currentArgIndex++;
                }
            }
        }

        internal static AbsXLSRec StaticCreateInstance(string className, int len, BinaryReader br, AbsXLSRec previousRecord, IoOperationInfo operationInfo)
        {
            switch (className)
            {
                case "StyleRecord":
                    return new StyleRecord(len, br, previousRecord, operationInfo);

                case "PaletteRecord":
                    return new PaletteRecord(len, br, previousRecord, operationInfo);

                case "IndexRecord":
                    return new IndexRecord(len, br, previousRecord, operationInfo);

                case "RowRecord":
                    return new RowRecord(len, br, previousRecord, operationInfo);

                case "LabelSSTRecord":
                    return new LabelSSTRecord(len, br, previousRecord, operationInfo);

                case "MulBlankRecord":
                    return new MulBlankRecord(len, br, previousRecord, operationInfo);

                case "DBCellRecord":
                    return new DBCellRecord(len, br, previousRecord, operationInfo);

                case "RKRecord":
                    return new RKRecord(len, br, previousRecord, operationInfo);

                case "MulRKRecord":
                    return new MulRKRecord(len, br, previousRecord, operationInfo);

                case "FormulaRecord":
                    return new FormulaRecord(len, br, previousRecord, operationInfo);

                case "BoundSheetRecord":
                    return new BoundSheetRecord(len, br, previousRecord, operationInfo);

                case "SSTRecord":
                    return new SSTRecord(len, br, previousRecord, operationInfo);

                case "ContinueRecord":
                    return new ContinueRecord(len, br, previousRecord, operationInfo);

                case "ExtSSTRecord":
                    return new ExtSSTRecord(len, br, previousRecord, operationInfo);

                case "Window2Record":
                    return new Window2Record(len, br, previousRecord, operationInfo);

                case "MergedCellsRecord":
                    return new MergedCellsRecord(len, br, previousRecord, operationInfo);

                case "HorizontalPageBreaksRecord":
                    return new HorizontalPageBreaksRecord(len, br, previousRecord, operationInfo);

                case "VerticalPageBreaksRecord":
                    return new VerticalPageBreaksRecord(len, br, previousRecord, operationInfo);

                case "ExternsheetRecord":
                    return new ExternsheetRecord(len, br, previousRecord, operationInfo);

                case "NameRecord":
                    return new NameRecord(len, br, previousRecord, operationInfo);

                case "SupBookRecord":
                    return new SupBookRecord(len, br, previousRecord, operationInfo);

                case "MsoDrawingRecord":
                    return new MsoDrawingRecord(len, br, previousRecord, operationInfo);

                case "MsoDrawingGroupRecord":
                    return new MsoDrawingGroupRecord(len, br, previousRecord, operationInfo);

                case "NoteRecord":
                    return new NoteRecord(len, br, previousRecord, operationInfo);

                case "ObjRecord":
                    return new ObjRecord(len, br, previousRecord, operationInfo);

                case "TxoRecord":
                    return new TxoRecord(len, br, previousRecord, operationInfo);
            }
            throw new SpreadsheetException("Internal error: record class not listed in switch statement.");
        }

        public static bool ValidBodySize(XLSDescriptor des, int size, bool exception)
        {
            if (((des == null) || !des.IsFixedSize) || (des.BodySize == size))
            {
                return true;
            }
            if (des.Code == 430)
            {
                throw new Exception("External references are currently not supported.");
            }
            string str = string.Concat(new object[] { "Record should have size ", des.BodySize, " and not ", size, "." });
            if (exception)
            {
                throw new Exception("Internal error: " + str);
            }
            return false;
        }

        public delegate byte StringLengthDelegate(object[] loadedArgs);

        public delegate int VariableArrayCountDelegate(object[] loadedArgs, object[] varArr, int bodySize);
    }
}

