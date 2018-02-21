namespace GemBox.Spreadsheet
{
    using System;

    internal class NumberFormatCollection : IndexedHashCollection
    {
        private static object[,] builtIn = new object[,] { 
            { 1, "0" }, { 2, "0.00" }, { 3, "#,##0" }, { 4, "#,##0.00" }, { 5, "$#,##0_);($#,##0)" }, { 6, "$#,##0_);[Red]($#,##0)" }, { 7, "$#,##0.00_);($#,##0.00)" }, { 8, "$#,##0.00_);[Red]($#,##0.00)" }, { 9, "0%" }, { 10, "0.00%" }, { 11, "0.00E+00" }, { 12, "# ?/?" }, { 13, "# ??/??" }, { 14, "M/D/YY" }, { 15, "D-MMM-YY" }, { 0x10, "D-MMM" }, 
            { 0x11, "MMM-YY" }, { 0x12, "h:mm AM/PM" }, { 0x13, "h:mm:ss AM/PM" }, { 20, "h:mm" }, { 0x15, "h:mm:ss" }, { 0x16, "M/D/YY h:mm" }, { 0x25, "_(#,##0_);(#,##0)" }, { 0x26, "_(#,##0_);[Red](#,##0)" }, { 0x27, "_(#,##0.00_);(#,##0.00)" }, { 40, "_(#,##0.00_);[Red](#,##0.00)" }, { 0x29, "_($* #,##0_);_($* (#,##0);_($* \"-\"_);_(@_)" }, { 0x2a, "_(* #,##0_);_(* (#,##0);_(* \"-\"_);_(@_)" }, { 0x2b, "_($* #,##0.00_);_($* (#,##0.00);_($* \"-\"??_);_(@_)" }, { 0x2c, "_(* #,##0.00_);_(* (#,##0.00);_(* \"-\"??_);_(@_)" }, { 0x2d, "mm:ss" }, { 0x2e, "[h]:mm:ss" }, 
            { 0x2f, "mm:ss.0" }, { 0x30, "##0.0E+0" }, { 0x31, "@" }
         };

        public NumberFormatCollection(bool readingOnly)
        {
            int num2;
            for (int i = num2 = 0; i < 0xa4; i++)
            {
                if ((num2 < builtIn.GetLength(0)) && (((int) builtIn[num2, 0]) == i))
                {
                    if (readingOnly)
                    {
                        base.AddArrayOnly(builtIn[num2, 1]);
                    }
                    else
                    {
                        base.AddInternal(builtIn[num2, 1]);
                    }
                    num2++;
                }
                else
                {
                    base.AddArrayOnly("");
                }
            }
        }

        /// <summary>
        /// This method is designed to be used ONLY for Excel file reading.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="formatString"></param>
        public void SetNumberFormat(int index, string formatString)
        {
            if (index < this.Count)
            {
                this[index] = formatString;
            }
            else
            {
                for (int i = this.Count; i < index; i++)
                {
                    base.AddArrayOnly("");
                }
                base.AddArrayOnly(formatString);
            }
        }
    }
}

