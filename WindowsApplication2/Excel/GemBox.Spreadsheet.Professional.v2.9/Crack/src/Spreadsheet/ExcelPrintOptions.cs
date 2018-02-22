namespace GemBox.Spreadsheet
{
    using System;

    /// <summary>
    /// Stores MS Excel print and print related options.
    /// </summary>
    public sealed class ExcelPrintOptions
    {
        private ExcelWorksheet parent;

        internal ExcelPrintOptions(ExcelWorksheet parent)
        {
            this.parent = parent;
        }

        /// <summary>
        /// Scaling factor for automatic page breaks.
        /// </summary>
        /// <remarks>
        /// <p>Unit is one percent. Value must be between 10 and 400.</p>
        /// <p>Default value for this property is 100.</p>
        /// <p>MS Excel inserts automatic page breaks depending on this scaling factor. 
        /// Smaller it gets, bigger will be the distance between the two automatic page breaks.</p>
        /// <p>If you set this property, <see cref="P:GemBox.Spreadsheet.ExcelPrintOptions.FitToPage">FitToPage</see>
        /// will automaticaly be set to <b>false</b>.</p>
        /// </remarks>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Thrown if value is out of 10 to 400 range.</exception>
        public int AutomaticPageBreakScalingFactor
        {
            get
            {
                return this.parent.scalingFactor;
            }
            set
            {
                if ((value < 10) || (value > 400))
                {
                    throw new ArgumentOutOfRangeException("value", value, "AutomaticPageBreakScalingFactor must be in range from 10 to 400.");
                }
                this.parent.scalingFactor = value;
                this.parent.setupOptions = (SetupOptions) Utilities.SetBit((ushort) this.parent.setupOptions, (ushort) 4, false);
                this.FitToPage = false;
            }
        }

        /// <summary>
        /// <b>True</b> for printing in draft quality, <b>false</b> otherwise.
        /// </summary>
        /// <remarks>
        /// Default value for this property is <b>false</b>.
        /// </remarks>
        public bool DraftQuality
        {
            get
            {
                return Utilities.IsBitSet((ushort) this.parent.setupOptions, 0x10);
            }
            set
            {
                this.parent.setupOptions = (SetupOptions) Utilities.SetBit((ushort) this.parent.setupOptions, (ushort) 0x10, value);
            }
        }

        /// <summary>
        /// <b>True</b> for using 
        /// <see cref="P:GemBox.Spreadsheet.ExcelPrintOptions.FitWorksheetHeightToPages">FitWorksheetHeightToPages</see>
        /// and <see cref="P:GemBox.Spreadsheet.ExcelPrintOptions.FitWorksheetWidthToPages">FitWorksheetWidthToPages</see>, 
        /// <b>false</b> otherwise.
        /// </summary>
        /// <remarks>
        /// <p>This property determines whether <see cref="P:GemBox.Spreadsheet.ExcelPrintOptions.AutomaticPageBreakScalingFactor">AutomaticPageBreakScalingFactor</see>
        /// or <see cref="P:GemBox.Spreadsheet.ExcelPrintOptions.FitWorksheetHeightToPages">FitWorksheetHeightToPages</see>
        /// and <see cref="P:GemBox.Spreadsheet.ExcelPrintOptions.FitWorksheetWidthToPages">FitWorksheetWidthToPages</see>
        /// will be used in printing.</p>
        /// <p>Default value for this property is <b>false</b>.</p>
        /// </remarks>
        public bool FitToPage
        {
            get
            {
                return this.parent.GetWSBoolOption(WSBoolOptions.FitToPage);
            }
            set
            {
                this.parent.SetWSBoolOption(value, WSBoolOptions.FitToPage);
            }
        }

        /// <summary>
        /// Fit worksheet height to this number of pages (0 = use as many as needed).
        /// </summary>
        /// <remarks>
        /// <p>Default value for this property is 0.</p>
        /// <p>If you set this property, <see cref="P:GemBox.Spreadsheet.ExcelPrintOptions.FitToPage">FitToPage</see>
        /// will automaticaly be set to <b>true</b>.</p>
        /// </remarks>
        public int FitWorksheetHeightToPages
        {
            get
            {
                return this.parent.fitWorksheetHeightToPages;
            }
            set
            {
                this.parent.fitWorksheetHeightToPages = (ushort) value;
                this.FitToPage = true;
            }
        }

        /// <summary>
        /// Fit worksheet width to this number of pages (0 = use as many as needed).
        /// </summary>
        /// <remarks>
        /// <p>Default value for this property is 0.</p>
        /// <p>If you set this property, <see cref="P:GemBox.Spreadsheet.ExcelPrintOptions.FitToPage">FitToPage</see>
        /// will automaticaly be set to <b>true</b>.</p>
        /// </remarks>
        public int FitWorksheetWidthToPages
        {
            get
            {
                return this.parent.fitWorksheetWidthToPages;
            }
            set
            {
                this.parent.fitWorksheetWidthToPages = (ushort) value;
                this.FitToPage = true;
            }
        }

        /// <summary>
        /// Footer margin.
        /// </summary>
        /// <remarks>
        /// Default value for this property is 0.5.
        /// </remarks>
        public double FooterMargin
        {
            get
            {
                return this.parent.footerMargin;
            }
            set
            {
                this.parent.footerMargin = value;
            }
        }

        /// <summary>
        /// Header margin.
        /// </summary>
        /// <remarks>
        /// Default value for this property is 0.5.
        /// </remarks>
        public double HeaderMargin
        {
            get
            {
                return this.parent.headerMargin;
            }
            set
            {
                this.parent.headerMargin = value;
            }
        }

        /// <summary>
        /// Number of copies to print.
        /// </summary>
        /// <remarks>
        /// Default value for this property is 1.
        /// </remarks>
        public int NumberOfCopies
        {
            get
            {
                return this.parent.numberOfCopies;
            }
            set
            {
                this.parent.numberOfCopies = (ushort) value;
                this.parent.setupOptions = (SetupOptions) Utilities.SetBit((ushort) this.parent.setupOptions, (ushort) 4, false);
            }
        }

        /// <summary>
        /// MS Excel specific paper size / type index.
        /// </summary>
        /// <remarks>
        /// <p>
        /// Default value for this property is 0.
        /// </p>
        /// <p>
        /// Following table shows most commonly used values:
        /// </p>
        /// <p>
        /// <FONT face="Arial" size="1">
        /// <table border="1" cellpadding="5" cellspacing="0" ID="Table1">
        /// <tr>
        /// <td><b>Index</b></td>
        /// <td><b>Paper type</b></td>
        /// <td><b>Paper size</b></td>
        /// </tr>
        /// <tr>
        /// <td>0</td>
        /// <td>Undefined</td>
        /// <td>
        /// </td>
        /// </tr>
        /// <TR>
        /// <TD>1</TD>
        /// <TD>Letter</TD>
        /// <TD>8 1/2in × 11in</TD>
        /// </TR>
        /// <TR>
        /// <TD>8</TD>
        /// <TD>A3</TD>
        /// <TD>297mm × 420mm</TD>
        /// </TR>
        /// <TR>
        /// <TD>9</TD>
        /// <TD>A4</TD>
        /// <TD>210mm × 297mm</TD>
        /// </TR>
        /// <TR>
        /// <TD>11</TD>
        /// <TD>A5</TD>
        /// <TD>148mm × 210mm</TD>
        /// </TR>
        /// </table>
        /// </FONT>
        /// </p>
        /// </remarks>
        public int PaperSize
        {
            get
            {
                return this.parent.paperSize;
            }
            set
            {
                this.parent.paperSize = (ushort) value;
                this.parent.setupOptions = (SetupOptions) Utilities.SetBit((ushort) this.parent.setupOptions, (ushort) 4, false);
            }
        }

        /// <summary>
        /// <b>True</b> for portrait orientation, <b>false</b> for landscape orientation.
        /// </summary>
        /// <remarks>
        /// Default value for this property is <b>true</b>.
        /// </remarks>
        public bool Portrait
        {
            get
            {
                return Utilities.IsBitSet((ushort) this.parent.setupOptions, 2);
            }
            set
            {
                this.parent.setupOptions = (SetupOptions) Utilities.SetBit((ushort) this.parent.setupOptions, (ushort) 2, value);
                this.parent.setupOptions = (SetupOptions) Utilities.SetBit((ushort) this.parent.setupOptions, (ushort) 4, false);
                this.parent.setupOptions = (SetupOptions) Utilities.SetBit((ushort) this.parent.setupOptions, (ushort) 0x40, false);
            }
        }

        /// <summary>
        /// <b>True</b> for printing in black and white, <b>false</b> otherwise.
        /// </summary>
        /// <remarks>
        /// Default value for this property is <b>false</b>.
        /// </remarks>
        public bool PrintBlackWhite
        {
            get
            {
                return Utilities.IsBitSet((ushort) this.parent.setupOptions, 8);
            }
            set
            {
                this.parent.setupOptions = (SetupOptions) Utilities.SetBit((ushort) this.parent.setupOptions, (ushort) 8, value);
            }
        }

        /// <summary>
        /// <b>True</b> for printing cell notes, <b>false</b> otherwise.
        /// </summary>
        /// <remarks>
        /// Default value for this property is <b>false</b>.
        /// </remarks>
        public bool PrintCellNotes
        {
            get
            {
                return Utilities.IsBitSet((ushort) this.parent.setupOptions, 0x20);
            }
            set
            {
                this.parent.setupOptions = (SetupOptions) Utilities.SetBit((ushort) this.parent.setupOptions, (ushort) 0x20, value);
            }
        }

        /// <summary>
        /// <b>True</b> for printing notes at end of sheet, <b>false</b> otherwise.
        /// </summary>
        /// <remarks>
        /// Default value for this property is <b>false</b>.
        /// </remarks>
        public bool PrintNotesSheetEnd
        {
            get
            {
                return Utilities.IsBitSet((ushort) this.parent.setupOptions, 0x200);
            }
            set
            {
                this.parent.setupOptions = (SetupOptions) Utilities.SetBit((ushort) this.parent.setupOptions, (ushort) 0x200, value);
            }
        }

        /// <summary>
        /// <b>True</b> for printing pages in rows, <b>false</b> otherwise.
        /// </summary>
        /// <remarks>
        /// Default value for this property is <b>false</b>.
        /// </remarks>
        public bool PrintPagesInRows
        {
            get
            {
                return Utilities.IsBitSet((ushort) this.parent.setupOptions, 1);
            }
            set
            {
                this.parent.setupOptions = (SetupOptions) Utilities.SetBit((ushort) this.parent.setupOptions, (ushort) 1, value);
            }
        }

        /// <summary>
        /// Print resolution in dpi.
        /// </summary>
        /// <remarks>
        /// Default value for this property is 0.
        /// </remarks>
        public int PrintResolution
        {
            get
            {
                return this.parent.printResolution;
            }
            set
            {
                this.parent.printResolution = (ushort) value;
                this.parent.setupOptions = (SetupOptions) Utilities.SetBit((ushort) this.parent.setupOptions, (ushort) 4, false);
            }
        }

        /// <summary>
        /// Start page number.
        /// </summary>
        /// <remarks>
        /// Default value for this property is 1.
        /// </remarks>
        public int StartPageNumber
        {
            get
            {
                return this.parent.startPageNumber;
            }
            set
            {
                this.parent.startPageNumber = (ushort) value;
            }
        }

        /// <summary>
        /// <b>True</b> for using start page number, <b>false</b> otherwise.
        /// </summary>
        /// <remarks>
        /// Default value for this property is <b>false</b>.
        /// </remarks>
        public bool UseStartPageNumber
        {
            get
            {
                return Utilities.IsBitSet((ushort) this.parent.setupOptions, 0x80);
            }
            set
            {
                this.parent.setupOptions = (SetupOptions) Utilities.SetBit((ushort) this.parent.setupOptions, (ushort) 0x80, value);
            }
        }

        /// <summary>
        /// Vertical print resolution in dpi.
        /// </summary>
        /// <remarks>
        /// Default value for this property is 0.
        /// </remarks>
        public int VerticalPrintResolution
        {
            get
            {
                return this.parent.verticalPrintResolution;
            }
            set
            {
                this.parent.verticalPrintResolution = (ushort) value;
            }
        }
    }
}

