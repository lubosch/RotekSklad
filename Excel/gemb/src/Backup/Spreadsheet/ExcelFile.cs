namespace GemBox.Spreadsheet
{
    using GemBox;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// Excel file contains one or more worksheets (<see cref="T:GemBox.Spreadsheet.ExcelWorksheet">ExcelWorksheet</see>)
    /// and workbook related properties and methods.
    /// </summary>
    /// <seealso cref="T:GemBox.Spreadsheet.ExcelWorksheet" />
    public sealed class ExcelFile
    {
        internal object[] cachedMsoDrawingGroupArguments;
        private CellStyleCachedCollection cellStyleCache;
        private byte[] compObjStream;
        private const string compObjStreamName = "\x0001CompObj";
        private bool csvParseNumbersDuringLoad = true;
        private static readonly char[] csvTypeDelimiters = new char[] { ',', ';', '\t' };
        private byte[] ctlsStream;
        private const string ctlsStreamName = "Ctls";
        private string defaultFontName = "Arial";
        private int defaultFontSize = 200;
        private bool delayFormulaParse = true;
        private byte[] documentSummaryStream;
        private const string documentSummaryStreamName = "\x0005DocumentSummaryInformation";
        private int groupMethodsAffectedCellsLimit = 0x4e20;
        internal int HashFactorA = 0x65;
        internal int HashFactorB = 0x33;
        internal bool HasMacroses;
        internal string IDText = "GemBox.Spreadsheet Professional 2.9 for .NET 2.0, Version=29.3.0.1000";
        private Ole2Storage macrosStorage;
        private const string macrosStorageName = "_VBA_PROJECT_CUR";
        /// <summary>
        /// Maximum number of columns in <see cref="T:GemBox.Spreadsheet.ExcelWorksheet">ExcelWorksheet</see>.
        /// </summary>
        /// <remarks>
        /// If you read/write XLS files, you are also limited by
        /// <see cref="F:GemBox.Spreadsheet.ExcelFile.MaxXlsColumns">ExcelFile.MaxXlsColumns</see>.
        /// </remarks>
        public const int MaxColumns = 0x4000;
        /// <summary>
        /// Maximum number of rows in <see cref="T:GemBox.Spreadsheet.ExcelWorksheet">ExcelWorksheet</see>.
        /// </summary>
        /// <remarks>
        /// If you read/write XLS files, you are also limited by
        /// <see cref="F:GemBox.Spreadsheet.ExcelFile.MaxXlsRows">ExcelFile.MaxXlsRows</see>.
        /// </remarks>
        public const int MaxRows = 0x100000;
        /// <summary>
        /// Maximum number of user-defined cell styles in XLS (BIFF8) file.
        /// </summary>
        public const int MaxXlsCellStyles = 0xf8b;
        /// <summary>
        /// Maximum number of colors in XLS (BIFF8) file.
        /// </summary>
        /// <remarks>
        /// This number includes 8 default colors:
        /// <see cref="P:System.Drawing.Color.Black">Color.Black</see>, 
        /// <see cref="P:System.Drawing.Color.White">Color.White</see>,
        /// <see cref="P:System.Drawing.Color.Red">Color.Red</see>, 
        /// <see cref="P:System.Drawing.Color.Green">Color.Green</see>,
        /// <see cref="P:System.Drawing.Color.Blue">Color.Blue</see>, 
        /// <see cref="P:System.Drawing.Color.Yellow">Color.Yellow</see>,
        /// <see cref="P:System.Drawing.Color.Magenta">Color.Magenta</see> and
        /// <see cref="P:System.Drawing.Color.Cyan">Color.Cyan</see>.
        /// </remarks>
        public const int MaxXlsColors = 0x38;
        /// <summary>
        /// Maximum number of columns in XLS (BIFF8) file.
        /// </summary>
        public const int MaxXlsColumns = 0x100;
        /// <summary>
        /// Maximum number of rows in XLS (BIFF8) file.
        /// </summary>
        public const int MaxXlsRows = 0x10000;
        internal PreservedRecords PreservedGlobalRecords;
        internal PackageBuilderBase preservedXlsx = new EmbeddedTemplateBuilder();
        private bool protectedMbr;
        private GemBox.Spreadsheet.RowColumnResolutionMethod rowColumnResolutionMethod;
        private const string summaryInformationStreamName = "\x0005SummaryInformation";
        private byte[] summaryStream;
        private bool use1904DateSystem;
        private const string workbookStreamName = "Workbook";
        private ExcelWorksheetCollection worksheets;

        /// <summary>
        /// Fired for unexpected situations when reading or writing XLS / CSV files.
        /// </summary>
        public event IoWarningEventHandler IoWarning;

        /// <summary>
        /// Initializes an empty (no worksheets) instance of the ExcelFile class.
        /// </summary>
        /// <remarks>
        /// <p>To add new worksheets to a blank file use <see cref="P:GemBox.Spreadsheet.ExcelFile.Worksheets">
        /// Worksheets</see> property, <see cref="M:GemBox.Spreadsheet.ExcelWorksheetCollection.Add(System.String)">Add</see> method.</p>
        /// <p>To save created file use <see cref="M:GemBox.Spreadsheet.ExcelFile.SaveXls(System.String)">SaveXls(string)</see> / 
        /// <see cref="M:GemBox.Spreadsheet.ExcelFile.SaveXls(System.IO.Stream)">SaveXls(Stream)</see> or 
        /// <see cref="M:GemBox.Spreadsheet.ExcelFile.SaveCsv(System.String,GemBox.Spreadsheet.CsvType)">SaveCsv</see> method.</p>
        /// <p>To read existing file or use existing file as a template use 
        /// <see cref="M:GemBox.Spreadsheet.ExcelFile.LoadXls(System.String,GemBox.Spreadsheet.XlsOptions)">LoadXls(string,XlsOptions)</see> /
        /// <see cref="M:GemBox.Spreadsheet.ExcelFile.LoadXls(System.IO.Stream,GemBox.Spreadsheet.XlsOptions)">LoadXls(Stream,XlsOptions)</see>
        /// or <see cref="M:GemBox.Spreadsheet.ExcelFile.LoadCsv(System.String,GemBox.Spreadsheet.CsvType)">LoadCsv</see> method.</p>
        /// </remarks>
        public ExcelFile()
        {
            this.Initialize();
        }

        internal void CopyDrawings(ExcelFile source)
        {
            PreservedRecords preservedGlobalRecords = source.PreservedGlobalRecords;
            if (preservedGlobalRecords != null)
            {
                if (this.PreservedGlobalRecords == null)
                {
                    this.PreservedGlobalRecords = new PreservedRecords();
                }
                this.PreservedGlobalRecords.CopyRecords(preservedGlobalRecords, "MSODRAWINGGROUP");
            }
        }

        /// <summary>
        /// Internal.  
        /// </summary>
        /// <param name="sourceFileName">Source file name.</param>
        /// <param name="destinationFileName">Destination file name.</param>
        /// <exclude />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void DumpToLowLevelXml(string sourceFileName, string destinationFileName)
        {
            byte[] ss = null;
            AbsXLSRecords records = null;
            Ole2Storage mStorage = null;
            using (FileStream stream = new FileStream(sourceFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                records = ReadStreamHelper(null, stream, false, ref ss, ref ss, false, ref ss, ref ss, ref mStorage, sourceFileName);
            }
            SaveLowLevelXML(records, destinationFileName, sourceFileName);
        }

        internal void ExceptionIfOverAffectedCellsLimit(int cellCount)
        {
            int groupMethodsAffectedCellsLimit = this.groupMethodsAffectedCellsLimit;
            if (cellCount > groupMethodsAffectedCellsLimit)
            {
                throw new InvalidOperationException(string.Concat(new object[] { "You are trying to modify ", cellCount, " cells while ExcelFile.GroupMethodsAffectedCellsLimit is set to ", groupMethodsAffectedCellsLimit, ". If you want to perform this operation, you need to change ExcelFile.GroupMethodsAffectedCellsLimit accordingly." }));
            }
        }

        private void FillChachedMsoRecords(AbsXLSRecords records)
        {
            IEnumerator enumerator = records.GetEnumerator();
            while (enumerator.MoveNext())
            {
                AbsXLSRec current = enumerator.Current as AbsXLSRec;
                if (current.RecordCode == 0xeb)
                {
                    MsoDrawingGroupRecord record = (MsoDrawingGroupRecord) current;
                    this.cachedMsoDrawingGroupArguments = record.arguments;
                    return;
                }
            }
        }

        internal static DateTime GetStartDate(bool use1904)
        {
            if (use1904)
            {
                return new DateTime(0x770, 1, 3);
            }
            return new DateTime(0x76c, 1, 1);
        }

        private byte[] GetStreamData(Ole2Stream stream)
        {
            switch (stream.Name)
            {
                case "Ctls":
                    return this.ctlsStream;

                case "\x0001CompObj":
                    return this.compObjStream;

                case "\x0005SummaryInformation":
                    return this.summaryStream;

                case "\x0005DocumentSummaryInformation":
                    return this.documentSummaryStream;
            }
            throw new Exception("Internal: Bad call to GetStreamData.");
        }

        private void Initialize()
        {
            SpreadsheetInfo.ValidateLicense(this);
            this.worksheets = new ExcelWorksheetCollection(this);
            this.cellStyleCache = new CellStyleCachedCollection(this, QueueSizeFromAffectedCellsLimit(this.groupMethodsAffectedCellsLimit));
        }

        /// <overloads>Loads the existing CSV file or stream.</overloads>
        /// <summary>
        /// Loads the existing stream with specified CSV format.
        /// </summary>
        /// <param name="stream">Input stream.</param>
        /// <param name="csvType">CSV type.</param>
        public void LoadCsv(Stream stream, CsvType csvType)
        {
            this.LoadCsv(stream, csvTypeDelimiters[(int) csvType]);
        }

        /// <summary>
        /// Loads the existing stream, using specified character as a delimiter.
        /// </summary>
        /// <param name="stream">Input stream.</param>
        /// <param name="separator">Separator used for delimiting data values.</param>
        public void LoadCsv(Stream stream, char separator)
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                this.LoadCsvInternal(reader, separator, "Stream");
            }
        }

        /// <summary>
        /// Loads all data using specified StreamReader and CSV Type.
        /// </summary>
        /// <remarks>
        /// Use this overload if you want to use a StreamReader with non-default encoding. 
        /// </remarks>
        /// <param name="sr">Stream reader.</param>
        /// <param name="csvType">CSV type.</param>
        public void LoadCsv(StreamReader sr, CsvType csvType)
        {
            this.LoadCsv(sr, csvTypeDelimiters[(int) csvType]);
        }

        /// <summary>
        /// Loads all data using specified StreamReader and character delimiter.
        /// </summary>
        /// <remarks>
        /// Use this overload if you want to use a StreamReader with non-default encoding. 
        /// </remarks>
        /// <param name="sr">Stream reader.</param>
        /// <param name="separator">Separator used for delimiting data values.</param>
        public void LoadCsv(StreamReader sr, char separator)
        {
            this.LoadCsvInternal(sr, separator, "StreamReader");
        }

        /// <summary>
        /// Loads the existing file with specified CSV format.
        /// </summary>
        /// <param name="fileName">Existing CSV file name (opened for reading).</param>
        /// <param name="csvType">CSV type.</param>
        public void LoadCsv(string fileName, CsvType csvType)
        {
            this.LoadCsv(fileName, csvTypeDelimiters[(int) csvType]);
        }

        /// <summary>
        /// Loads the existing file, using specified character as a delimiter.
        /// </summary>
        /// <param name="fileName">File name.</param>
        /// <param name="separator">Separator used for delimiting data values.</param>
        public void LoadCsv(string fileName, char separator)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                this.LoadCsvInternal(reader, separator, fileName);
            }
        }

        private void LoadCsvInternal(StreamReader sr, char separator, string fileName)
        {
            string[] strArray;
            this.Reset();
            ExcelWorksheet worksheet = this.worksheets.Add("Sheet1");
            for (int i = 0; ReadSplitCsvLine(sr, separator, out strArray); i++)
            {
                for (int j = 0; j < strArray.Length; j++)
                {
                    if (strArray[j].Length > 0)
                    {
                        object obj2;
                        if (this.csvParseNumbersDuringLoad)
                        {
                            obj2 = ParseCSValue(strArray[j]);
                        }
                        else
                        {
                            obj2 = strArray[j];
                        }
                        worksheet.Cells[i, j].Value = obj2;
                    }
                }
            }
        }

        /// <summary>
        /// Loads the existing XLS file from the input stream (preserving MS Excel records).
        /// </summary>
        /// <param name="stream">Input stream.</param>
        public void LoadXls(Stream stream)
        {
            this.LoadXls(stream, XlsOptions.PreserveAll);
        }

        /// <overloads>Loads the existing XLS file or stream.</overloads>
        /// <summary>
        /// Loads the existing XLS file (preserving MS Excel records).
        /// </summary>
        /// <param name="fileName">Existing XLS file name (opened for reading).</param>
        public void LoadXls(string fileName)
        {
            this.LoadXls(fileName, XlsOptions.PreserveAll);
        }

        /// <summary>
        /// Loads the existing XLS file from the input stream (optionally preserving MS Excel records).
        /// </summary>
        /// <remarks>
        /// <p>If the only purpose of loading the file is to read data values and formatting using 
        /// GemBox.Spreadsheet API, you should use <i>xlsOptions</i> set to <see cref="F:GemBox.Spreadsheet.XlsOptions.None">XlsOptions.None</see>
        /// as this will speed up the loading process.</p>
        /// <p>If you load the existing file to use it as template for a new file, you can choose
        /// whether you want to preserve specific MS Excel records not recognized by GemBox.Spreadsheet API.</p>
        /// </remarks>
        /// <param name="stream">Input stream.</param>
        /// <param name="xlsOptions">XLS options.</param>
        public void LoadXls(Stream stream, XlsOptions xlsOptions)
        {
            if (stream == null)
            {
                throw new SpreadsheetException("Stream argument can't be null.");
            }
            this.Reset();
            AbsXLSRecords records = ReadStreamHelper(this, stream, (xlsOptions & XlsOptions.PreserveSummaries) != XlsOptions.None, ref this.summaryStream, ref this.documentSummaryStream, (xlsOptions & XlsOptions.PreserveMacros) != XlsOptions.None, ref this.ctlsStream, ref this.compObjStream, ref this.macrosStorage, "Stream");
            this.FillChachedMsoRecords(records);
            new XLSFileReader(this, xlsOptions, "Stream").ImportRecords(records);
        }

        /// <summary>
        /// Loads the existing XLS file (optionally preserving MS Excel records).
        /// </summary>
        /// <remarks>
        /// <p>If the only purpose of loading the file is to read data values and formatting using 
        /// GemBox.Spreadsheet API, you should use <i>xlsOptions</i> set to <see cref="F:GemBox.Spreadsheet.XlsOptions.None">XlsOptions.None</see>
        /// as this will speed up the loading process.</p>
        /// <p>If you load the existing file to use it as template for a new file, you can choose
        /// whether you want to preserve specific MS Excel records not recognized by GemBox.Spreadsheet API.</p>
        /// </remarks>
        /// <param name="fileName">Existing XLS file name (opened for reading).</param>
        /// <param name="xlsOptions">XLS options.</param>
        public void LoadXls(string fileName, XlsOptions xlsOptions)
        {
            this.Reset();
            AbsXLSRecords records = null;
            using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                records = ReadStreamHelper(this, stream, (xlsOptions & XlsOptions.PreserveSummaries) != XlsOptions.None, ref this.summaryStream, ref this.documentSummaryStream, (xlsOptions & XlsOptions.PreserveMacros) != XlsOptions.None, ref this.ctlsStream, ref this.compObjStream, ref this.macrosStorage, fileName);
            }
            this.FillChachedMsoRecords(records);
            new XLSFileReader(this, xlsOptions, fileName).ImportRecords(records);
        }

        /// <summary>
        /// Loads all parts of XLSX file as separate files from input directory.
        /// </summary>
        /// <remarks>
        /// You need to use external ZIP library to extract all parts from XLSX file.
        /// </remarks>
        /// <param name="path">Path of input directory.</param>
        /// <param name="xlsxOptions">XLS options.</param>
        public void LoadXlsxFromDirectory(string path, XlsxOptions xlsxOptions)
        {
            if (!Directory.Exists(path))
            {
                throw new SpreadsheetException("Input directory \"" + path + "\" does not exist.");
            }
            if (xlsxOptions == XlsxOptions.PreserveMakeCopy)
            {
                throw new NotImplementedException();
            }
            this.LoadXlsxInternal(new DirectoryBuilder(path, FileAccess.Read), path, xlsxOptions);
        }

        private void LoadXlsxInternal(PackageBuilderBase builder, string fileName, XlsxOptions xlsxOptions)
        {
            try
            {
                this.Reset();
                new XlsxReadDirector(builder, this).Construct();
            }
            finally
            {
                if ((xlsxOptions == XlsxOptions.PreserveKeepOpen) || (xlsxOptions == XlsxOptions.PreserveMakeCopy))
                {
                    this.preservedXlsx = builder;
                }
                else
                {
                    builder.Dispose();
                }
            }
        }

        internal static IoWarningEventArgs OnIoWarning(ExcelFile excelFile, string fileName, IoOperation operation, string warningMsg)
        {
            IoWarningEventArgs e = new IoWarningEventArgs(fileName, operation, warningMsg);
            if ((excelFile != null) && (excelFile.IoWarning != null))
            {
                excelFile.IoWarning(null, e);
            }
            return e;
        }

        private static object ParseCSValue(string item)
        {
            double num;
            NumberStyles style = NumberStyles.Float | NumberStyles.AllowThousands;
            if (double.TryParse(item, style, CultureInfo.CurrentCulture, out num))
            {
                return num;
            }
            return item;
        }

        private static int QueueSizeFromAffectedCellsLimit(int affectedCellsLimit)
        {
            return Math.Min(2 * affectedCellsLimit, 0x7a120);
        }

        private static bool ReadSplitCsvLine(StreamReader sr, char separator, out string[] values)
        {
            ArrayList list = new ArrayList();
            StringBuilder builder = new StringBuilder();
            bool flag = true;
            char ch = separator;
            bool flag2 = true;
            string str = null;
            do
            {
                string str2 = sr.ReadLine();
                if (str2 == null)
                {
                    flag2 = false;
                    break;
                }
                str2 = str + str2;
                for (int i = 0; i < str2.Length; i++)
                {
                    char ch2 = str2[i];
                    if (flag)
                    {
                        if (ch2 == separator)
                        {
                            list.Add(builder.ToString());
                            builder = new StringBuilder();
                            ch = ch2;
                        }
                        else if ((ch2 == '"') && (ch == separator))
                        {
                            flag = false;
                            ch = ch2;
                        }
                        else
                        {
                            builder.Append(ch2);
                            if (!char.IsWhiteSpace(ch2))
                            {
                                ch = ch2;
                            }
                        }
                    }
                    else if (ch2 == '"')
                    {
                        if (((i + 1) < str2.Length) && (str2[i + 1] == '"'))
                        {
                            builder.Append('"');
                            i++;
                        }
                        else
                        {
                            flag = true;
                        }
                    }
                    else
                    {
                        builder.Append(ch2);
                    }
                }
                str = "\n";
            }
            while (!flag);
            list.Add(builder.ToString());
            values = (string[]) list.ToArray(typeof(string));
            return flag2;
        }

        private static AbsXLSRecords ReadStreamHelper(ExcelFile excelFile, Stream inputStream, bool readSummaryStreams, ref byte[] ss, ref byte[] dss, bool readMacros, ref byte[] ctls, ref byte[] compObj, ref Ole2Storage mStorage, string fileName)
        {
            MemoryStream input = null;
            using (Ole2CompoundFile file = new Ole2CompoundFile())
            {
                file.Load(inputStream, true);
                foreach (Ole2DirectoryEntry entry in file.Root)
                {
                    Ole2Storage storage;
                    Ole2Stream stream2 = entry as Ole2Stream;
                    if (stream2 == null)
                    {
                        goto Label_00D9;
                    }
                    string name = stream2.Name;
                    if (name != null)
                    {
                        if (!(name == "Workbook"))
                        {
                            if (name == "\x0005SummaryInformation")
                            {
                                goto Label_00A0;
                            }
                            if (name == "\x0005DocumentSummaryInformation")
                            {
                                goto Label_00AD;
                            }
                            if (name == "Ctls")
                            {
                                goto Label_00BB;
                            }
                            if (name == "\x0001CompObj")
                            {
                                goto Label_00CA;
                            }
                        }
                        else
                        {
                            input = new MemoryStream(stream2.GetData());
                        }
                    }
                    continue;
                Label_00A0:
                    if (readSummaryStreams)
                    {
                        ss = stream2.GetData();
                    }
                    continue;
                Label_00AD:
                    if (readSummaryStreams)
                    {
                        dss = stream2.GetData();
                    }
                    continue;
                Label_00BB:
                    if (readMacros)
                    {
                        ctls = stream2.GetData();
                    }
                    continue;
                Label_00CA:
                    if (readMacros)
                    {
                        compObj = stream2.GetData();
                    }
                    continue;
                Label_00D9:
                    storage = entry as Ole2Storage;
                    if ((storage.Name == "_VBA_PROJECT_CUR") && readMacros)
                    {
                        mStorage = storage;
                        mStorage.CacheAllStreams();
                    }
                }
            }
            if (input == null)
            {
                throw new Exception("Provided file is not a valid BIFF8 file. Only XLS files from Excel 97 and on are supported.");
            }
            AbsXLSRecords records = new AbsXLSRecords();
            using (BinaryReader reader = new BinaryReader(input, new UnicodeEncoding()))
            {
                records.Read(reader, new IoOperationInfo(excelFile, fileName, IoOperation.XlsReading));
            }
            input.Close();
            return records;
        }

        private void Reset()
        {
            this.worksheets = new ExcelWorksheetCollection(this);
            this.cachedMsoDrawingGroupArguments = null;
            this.cellStyleCache = new CellStyleCachedCollection(this, QueueSizeFromAffectedCellsLimit(this.groupMethodsAffectedCellsLimit));
            this.protectedMbr = false;
            this.summaryStream = null;
            this.documentSummaryStream = null;
            this.ctlsStream = null;
            this.compObjStream = null;
            this.macrosStorage = null;
            this.PreservedGlobalRecords = null;
            this.use1904DateSystem = false;
            this.preservedXlsx = new EmbeddedTemplateBuilder();
        }

        /// <overloads>Saves all data to a new file or stream in CSV format.</overloads>
        /// <summary>
        /// Saves all data to a stream in a specified CSV format.
        /// </summary>
        /// <param name="stream">Output stream.</param>
        /// <param name="csvType">CSV type.</param>
        public void SaveCsv(Stream stream, CsvType csvType)
        {
            this.SaveCsv(stream, csvTypeDelimiters[(int) csvType]);
        }

        /// <summary>
        /// Saves all data to a stream, using specified character as a delimiter.
        /// </summary>
        /// <param name="stream">Output stream.</param>
        /// <param name="separator">Separator used for delimiting data values.</param>
        public void SaveCsv(Stream stream, char separator)
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                this.SaveCsvInternal(writer, separator, "Stream");
            }
        }

        /// <summary>
        /// Saves all data using specified StreamWriter and CSV Type.
        /// </summary>
        /// <remarks>
        /// Use this overload if you want to use a StreamWriter with non-default encoding. 
        /// Note that .NET Framework adds byte order mark (BOM) to files with non-default 
        /// encoding. MS Excel and other programs may fail to recognize CSV files with 
        /// non-default encoding.
        /// </remarks>
        /// <param name="sw">Stream writer.</param>
        /// <param name="csvType">CSV type.</param>
        public void SaveCsv(StreamWriter sw, CsvType csvType)
        {
            this.SaveCsv(sw, csvTypeDelimiters[(int) csvType]);
        }

        /// <summary>
        /// Saves all data using specified StreamWriter and character delimiter.
        /// </summary>
        /// <remarks>
        /// Use this overload if you want to use a StreamWriter with non-default encoding. 
        /// Note that .NET Framework adds byte order mark (BOM) to files with non-default 
        /// encoding. MS Excel and other programs may fail to recognize CSV files with 
        /// non-default encoding.
        /// </remarks>
        /// <param name="sw">Stream writer.</param>
        /// <param name="separator">Separator used for delimiting data values.</param>
        public void SaveCsv(StreamWriter sw, char separator)
        {
            this.SaveCsvInternal(sw, separator, "StreamWriter");
        }

        /// <summary>
        /// Saves all data to a new file in a specified CSV format.
        /// </summary>
        /// <param name="fileName">File name.</param>
        /// <param name="csvType">CSV type.</param>
        public void SaveCsv(string fileName, CsvType csvType)
        {
            this.SaveCsv(fileName, csvTypeDelimiters[(int) csvType]);
        }

        /// <summary>
        /// Saves all data to a new file, using specified character as a delimiter.
        /// </summary>
        /// <param name="fileName">File name.</param>
        /// <param name="separator">Separator used for delimiting data values.</param>
        public void SaveCsv(string fileName, char separator)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                this.SaveCsvInternal(writer, separator, fileName);
            }
        }

        private void SaveCsvInternal(StreamWriter sw, char separator, string fileName)
        {
            char[] anyOf = new char[] { separator, '"', '\n' };
            ExcelWorksheet activeWorksheet = this.Worksheets.ActiveWorksheet;
            int num = -1;
            int num2 = -1;
            int count = 0;
            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                {
                    count = activeWorksheet.Rows.Count;
                }
                else
                {
                    count = num + 1;
                }
                for (int j = 0; j < count; j++)
                {
                    int num4;
                    if (i == 0)
                    {
                        num4 = activeWorksheet.Rows[j].AllocatedCells.Count;
                    }
                    else
                    {
                        num4 = num2 + 1;
                    }
                    for (int k = 0; k < num4; k++)
                    {
                        ExcelCell cell = activeWorksheet.Cells[j, k];
                        object obj2 = cell.Value;
                        if ((obj2 != null) && !(obj2 is DBNull))
                        {
                            CellRange mergedRange = cell.MergedRange;
                            if ((mergedRange == null) || ((mergedRange.FirstRowIndex == j) && (mergedRange.FirstColumnIndex == k)))
                            {
                                if (i == 0)
                                {
                                    if (k > num2)
                                    {
                                        num2 = k;
                                    }
                                }
                                else
                                {
                                    string str = obj2.ToString();
                                    if (str.IndexOfAny(anyOf) != -1)
                                    {
                                        StringBuilder builder = new StringBuilder(str);
                                        builder.Replace("\"", "\"\"");
                                        str = '"' + builder.ToString() + '"';
                                    }
                                    sw.Write(str);
                                }
                            }
                        }
                        if ((i == 1) && ((k + 1) < num4))
                        {
                            sw.Write(separator);
                        }
                    }
                    if (i == 0)
                    {
                        if (j > num)
                        {
                            num = j;
                        }
                    }
                    else
                    {
                        sw.WriteLine();
                    }
                }
            }
        }

        private static void SaveLowLevelXML(AbsXLSRecords records, string fileName, string sourceFileName)
        {
            StreamWriter writer = new StreamWriter(fileName);
            if (sourceFileName != null)
            {
                writer.WriteLine("<!-- FileName: {0} -->", sourceFileName);
            }
            writer.WriteLine("<ExcelFile>");
            string name = null;
            int index = 0;
            foreach (AbsXLSRec rec in records)
            {
                if (rec.Name == name)
                {
                    index++;
                }
                else
                {
                    index = 0;
                    name = rec.Name;
                }
                writer.WriteLine(rec.GetXMLRecord(index));
            }
            writer.Write("</ExcelFile>");
            writer.Close();
        }

        /// <summary>
        /// Saves all data to an output stream in XLS format.
        /// </summary>
        /// <param name="stream">Output stream.</param>
        public void SaveXls(Stream stream)
        {
            XLSFileWriter xlsWriter = new XLSFileWriter(this, "Stream");
            this.SaveXlsInternal(stream, xlsWriter, "Stream");
        }

        /// <overloads>Saves all data to a file or stream in XLS format.</overloads>
        /// <summary>
        /// Saves all data to a new file in XLS format.
        /// </summary>
        /// <param name="fileName">File name.</param>
        public void SaveXls(string fileName)
        {
            XLSFileWriter xlsWriter = new XLSFileWriter(this, fileName);
            using (FileStream stream = new FileStream(fileName, FileMode.Create))
            {
                this.SaveXlsInternal(stream, xlsWriter, fileName);
            }
        }

        private void SaveXlsInternal(Stream stream, XLSFileWriter xlsWriter, string fileName)
        {
            AbsXLSRecords records = xlsWriter.GetRecords();
            using (Ole2CompoundFile file = new Ole2CompoundFile())
            {
                Ole2Storage root = file.Root;
                root.AddStream("Workbook", XLSFileWriter.GetStream(records));
                if (this.ctlsStream != null)
                {
                    root.AddStream("Ctls", this.ctlsStream.Length, new GetStreamDataHandler(this.GetStreamData));
                }
                if (this.compObjStream != null)
                {
                    root.AddStream("\x0001CompObj", this.compObjStream.Length, new GetStreamDataHandler(this.GetStreamData));
                }
                if (this.summaryStream != null)
                {
                    root.AddStream("\x0005SummaryInformation", this.summaryStream.Length, new GetStreamDataHandler(this.GetStreamData));
                }
                if (this.documentSummaryStream != null)
                {
                    root.AddStream("\x0005DocumentSummaryInformation", this.documentSummaryStream.Length, new GetStreamDataHandler(this.GetStreamData));
                }
                if (this.macrosStorage != null)
                {
                    root.AddStorage("_VBA_PROJECT_CUR").ImportTree(this.macrosStorage, true);
                }
                file.Save(stream);
            }
        }

        private void SaveXlsxInternal(PackageBuilderBase builder, string fileName)
        {
            new XlsxWriteDirector(builder, this).Construct();
        }

        /// <summary>
        /// Saves all parts of XLSX file as separate files to output directory.
        /// </summary>
        /// <remarks>
        /// You need to use external ZIP library to package all created files to XLSX file.
        /// </remarks>
        /// <param name="path">Path of output directory.</param>
        public void SaveXlsxToDirectory(string path)
        {
            using (PackageBuilderBase base2 = new DirectoryBuilder(path, FileAccess.Write))
            {
                this.SaveXlsxInternal(base2, path);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the objects of specified type can be assigned 
        /// to <see cref="P:GemBox.Spreadsheet.ExcelCell.Value">ExcelCell.Value</see> property.
        /// </summary>
        /// <param name="type">Queried type.</param>
        /// <remarks>
        /// Currently supported types are:
        /// <list type="bullet">
        /// <item><description>System.DBNull</description></item>
        /// <item><description>System.Byte</description></item>
        /// <item><description>System.SByte</description></item>
        /// <item><description>System.Int16</description></item>
        /// <item><description>System.UInt16</description></item>
        /// <item><description>System.Int64</description></item>
        /// <item><description>System.UInt64</description></item>
        /// <item><description>System.UInt32</description></item>
        /// <item><description>System.Int32</description></item>
        /// <item><description>System.Single</description></item>
        /// <item><description>System.Double</description></item>
        /// <item><description>System.Boolean</description></item>
        /// <item><description>System.Char</description></item>
        /// <item><description>System.Text.StringBuilder</description></item>
        /// <item><description>System.Decimal</description></item>
        /// <item><description>System.DateTime</description></item>
        /// <item><description>System.String</description></item>
        /// </list>
        /// </remarks>
        /// <returns><b>true</b> if the specified type is supported; otherwise, <b>false</b>.</returns>
        public static bool SupportsType(Type type)
        {
            if (type.IsEnum)
            {
                return true;
            }
            switch (type.FullName)
            {
                case "System.DBNull":
                case "System.Byte":
                case "System.SByte":
                case "System.Int16":
                case "System.UInt16":
                case "System.Int64":
                case "System.UInt64":
                case "System.UInt32":
                case "System.Int32":
                case "System.Single":
                case "System.Double":
                case "System.Boolean":
                case "System.Char":
                case "System.Text.StringBuilder":
                case "System.Decimal":
                case "System.DateTime":
                case "System.String":
                    return true;
            }
            return false;
        }

        internal static void ThrowExceptionForUnsupportedType(Type type)
        {
            if (!SupportsType(type))
            {
                throw new NotSupportedException("Type " + type.Name + " is not supported.");
            }
        }

        internal CellStyleCachedCollection CellStyleCache
        {
            get
            {
                return this.cellStyleCache;
            }
        }

        /// <summary>
        /// Gets or sets whether LoadCsv() methods will try to convert text values to numbers.
        /// </summary>
        /// <remarks>
        /// <p>Default value for this property is <b>true</b>.</p>
        /// <p>All values (including numbers) in CSV files are stored as text. By default, 
        /// any of <see cref="M:GemBox.Spreadsheet.ExcelFile.LoadCsv(System.String,System.Char)">LoadCsv()</see> 
        /// overloads will try to parse text values as numbers and if successful
        /// cell will be filled with <see cref="T:System.Double">Double</see> value. If
        /// you don't want such behaviour (for example, you want IDs like "00935" to remain
        /// strings), set this property to <b>false</b>.</p>
        /// </remarks>
        public bool CsvParseNumbersDuringLoad
        {
            get
            {
                return this.csvParseNumbersDuringLoad;
            }
            set
            {
                this.csvParseNumbersDuringLoad = value;
            }
        }

        /// <summary>
        /// Gets or sets name of the default font used in the workbook.
        /// </summary>
        /// <remarks>
        /// Default value for this property is "Arial".
        /// </remarks>
        public string DefaultFontName
        {
            get
            {
                return this.defaultFontName;
            }
            set
            {
                this.defaultFontName = value;
            }
        }

        /// <summary>
        /// Gets or sets default font size.
        /// </summary>
        /// <remarks>
        /// <p>Unit is twip (1/20th of a point).</p>
        /// <p>Default value of this property is 200.</p>
        /// </remarks>
        public int DefaultFontSize
        {
            get
            {
                return this.defaultFontSize;
            }
            set
            {
                this.defaultFontSize = value;
            }
        }

        /// <summary>
        /// Delays formula parsing until one of SaveXls methods is called.
        /// </summary>
        /// <remarks>
        /// <p>Old XLS format requires all formulas to be parsed and saved to XLS files as special
        /// tokens in RPN (Reverse Polish notation). GemBox.Spreadsheet only knows how to parse limited
        /// set of formulas listed at <see cref="P:GemBox.Spreadsheet.ExcelCell.Formula">ExcelCell.Formula</see>
        /// page. Only listed formulas can be saved to XLS file.</p>
        /// <p>New XLSX (Open XML) format stores formulas as strings and leaves formula parsing to applications
        /// that read XLSX documents. Therefore, ALL formulas are supported when writing/reading XLSX files.</p>
        /// <p>If this property is true, each set of <see cref="P:GemBox.Spreadsheet.ExcelCell.Formula">
        /// ExcelCell.Formula</see> property will just store formula string. When one of
        /// <see cref="M:GemBox.Spreadsheet.ExcelFile.SaveXls(System.String)">ExcelFile.SaveXls</see> methods is called
        /// formulas are parsed and exception is thrown if formula is not supported or in bad format.
        /// When XLSX is saved, formulas are not parsed, they are just saved as strings.
        /// If the formula is in bad format, MS Excel or other application will report an error in formula.
        /// </p>
        /// <p>If this property is false, formula string is parsed every time you set 
        /// <see cref="P:GemBox.Spreadsheet.ExcelCell.Formula">ExcelCell.Formula</see> property. You will
        /// limit formulas to the ones supported by GemBox.Spreadsheet parser and the exception will be
        /// thrown immediately if the formula is not supported or in bad format.</p>
        /// <p>Default value of this property is <b>true</b>.</p>
        /// </remarks>
        public bool DelayFormulaParse
        {
            get
            {
                return this.delayFormulaParse;
            }
            set
            {
                this.delayFormulaParse = value;
            }
        }

        /// <summary>
        /// Maximum number of affected cells in group set methods.
        /// </summary>
        /// <remarks>
        /// If user tries to modify all cells in a group which has more cells than specified limit, exception
        /// will be thrown. This property was introduced to prevent users from accidentally modifying millions
        /// of cells which results in a long delay, a large memory allocation and a big resulting file. You can 
        /// set this limit to value which suits your needs (minimum is 5).
        /// </remarks>
        public int GroupMethodsAffectedCellsLimit
        {
            get
            {
                return this.groupMethodsAffectedCellsLimit;
            }
            set
            {
                if (value < 5)
                {
                    throw new ArgumentOutOfRangeException("value", value, "GroupMethodsAffectedCellsLimit must be larger than 5.");
                }
                this.groupMethodsAffectedCellsLimit = value;
                this.cellStyleCache.AddQueueSize = QueueSizeFromAffectedCellsLimit(value);
            }
        }

        /// <summary>
        /// Gets or sets the workbook protection flag.
        /// </summary>
        /// <remarks>
        /// This property is simply written to Excel file and has no effect on the behavior of this library.
        /// For more information on workbook protection, consult Microsoft Excel documentation.
        /// </remarks>
        /// <seealso cref="P:GemBox.Spreadsheet.ExcelWorksheet.Protected">ExcelWorksheet.Protected</seealso>
        public bool Protected
        {
            get
            {
                return this.protectedMbr;
            }
            set
            {
                this.protectedMbr = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="T:GemBox.Spreadsheet.CellStyle">CellStyle</see> resolution method.
        /// </summary>
        /// <remarks>
        /// <p>
        /// Because of limitations of Microsoft Excel file format, every cell must be written to file with
        /// resolved <see cref="T:GemBox.Spreadsheet.CellStyle">CellStyle</see>. In the case where a cell doesn't 
        /// have specific property set on its <see cref="P:GemBox.Spreadsheet.ExcelCell.Style">Style</see> and that 
        /// property is set on both row and column that contain that cell, a cell will inherit property value 
        /// from row or column <see cref="P:GemBox.Spreadsheet.ExcelColumnRowBase.Style">Style</see>, depending 
        /// on the resolution method.
        /// </p>
        /// <p>
        /// Default value for this property is <see cref="F:GemBox.Spreadsheet.RowColumnResolutionMethod.RowOverColumn">
        /// RowOverColumn</see>.
        /// </p>
        /// </remarks>
        /// <example> Following code will result in a file where cell will have right alignment because same column cell 
        /// style property takes precedence over row cell style property. Note that resolution is property based, in 
        /// other words if column in this case had <see cref="P:GemBox.Spreadsheet.CellStyle.VerticalAlignment">
        /// VerticalAlignment</see> set cell would inherit both <see cref="P:GemBox.Spreadsheet.CellStyle.HorizontalAlignment">
        /// HorizontalAlignment</see> and <see cref="P:GemBox.Spreadsheet.CellStyle.VerticalAlignment">VerticalAlignment</see> 
        /// from row and column. 
        /// <code lang="Visual Basic">
        /// excelFile.RowColumnResolutionMethod = RowColumnResolutionMethod.ColumnOverRow
        /// excelFile.Worksheets(0).Cells("B2").Value = "B2"
        /// excelFile.Worksheets(0).Rows("2").Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        /// excelFile.Worksheets(0).Columns("B").Style.HorizontalAlignment = HorizontalAlignmentStyle.Right
        /// </code>
        /// <code lang="C#">
        /// excelFile.RowColumnResolutionMethod = RowColumnResolutionMethod.ColumnOverRow;
        /// excelFile.Worksheets[0].Cells["B2"].Value = "B2";
        /// excelFile.Worksheets[0].Rows["2"].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
        /// excelFile.Worksheets[0].Columns["B"].Style.HorizontalAlignment = HorizontalAlignmentStyle.Right;
        /// </code> 
        /// </example>
        public GemBox.Spreadsheet.RowColumnResolutionMethod RowColumnResolutionMethod
        {
            get
            {
                return this.rowColumnResolutionMethod;
            }
            set
            {
                this.rowColumnResolutionMethod = value;
            }
        }

        /// <summary>
        /// Gets or sets whether 1904 date system is used.
        /// </summary>
        /// <remarks>
        /// Default value for this property is <b>false</b>.
        /// For more information on 1904 date system, consult Microsoft Excel documentation.
        /// </remarks>
        public bool Use1904DateSystem
        {
            get
            {
                return this.use1904DateSystem;
            }
            set
            {
                this.use1904DateSystem = value;
            }
        }

        /// <summary>
        /// Collection of all worksheets (<see cref="T:GemBox.Spreadsheet.ExcelWorksheet">ExcelWorksheet</see>) in a workbook. 
        /// </summary>
        /// <seealso cref="T:GemBox.Spreadsheet.ExcelWorksheet" />
        public ExcelWorksheetCollection Worksheets
        {
            get
            {
					 //if (this.HashFactorA > this.HashFactorB)
					 //{
					 //    throw new Exception("License is invalid.");
					 //}
                return this.worksheets;
            }
        }
    }
}

