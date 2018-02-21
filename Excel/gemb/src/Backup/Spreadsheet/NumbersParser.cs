namespace GemBox.Spreadsheet
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Class used for controling number format
    /// </summary>
    internal class NumbersParser
    {
        /// <summary>
        /// Number format for string conversion
        /// </summary>
        private static NumberFormatInfo formatProvider = new NumberFormatInfo();

        /// <summary>
        /// Initialize object
        /// </summary>
        static NumbersParser()
        {
            formatProvider.NumberDecimalSeparator = ".";
        }

        /// <summary>
        /// Determines whether the specified double value is ushort( integer ).
        /// </summary>
        /// <param name="doubleValue">The double value.</param>
        /// <returns>
        /// <c>true</c> if the specified double value is ushort; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsUshort(double doubleValue)
        {
            return ((doubleValue < 65535.0) && (Math.Ceiling(doubleValue) == doubleValue));
        }

        /// <summary>
        /// Convert string to double.
        /// </summary>
        /// <param name="data">string data.</param>
        /// <returns>double data.</returns>
        public static double StrToDouble(string data)
        {
            double num;
            double.TryParse(data, NumberStyles.Float, (IFormatProvider) formatProvider, out num);
            return num;
        }

        /// <summary>
        /// Converts string to float.
        /// </summary>
        /// <param name="str">strind data.</param>
        /// <returns>flot data.</returns>
        public static float StrToFloat(string str)
        {
            double result = 0.0;
            double.TryParse(str, NumberStyles.Float, (IFormatProvider) formatProvider, out result);
            return (float) result;
        }

        /// <summary>
        /// Converts string to int.
        /// </summary>
        /// <param name="data">string data.</param>
        /// <returns>int data.</returns>
        public static int StrToInt(string data)
        {
            double result = 0.0;
            double.TryParse(data, NumberStyles.Integer, (IFormatProvider) formatProvider, out result);
            return (int) result;
        }

        /// <summary>
        /// Get number format info instance
        /// </summary>
        public static NumberFormatInfo Provider
        {
            get
            {
                return formatProvider;
            }
        }
    }
}

