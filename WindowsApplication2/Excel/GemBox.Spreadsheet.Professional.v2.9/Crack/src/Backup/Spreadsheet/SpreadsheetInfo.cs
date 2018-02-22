namespace GemBox.Spreadsheet
{
    using GemBox;
    using System;

    /// <summary>
    /// Contains static licensing methods (GemBox.Spreadsheet Professional only) and diagnostic 
    /// information about executing GemBox.Spreadsheet assembly.
    /// </summary>
    public sealed class SpreadsheetInfo
    {
        internal const string AssemblyName = "GemBox.Spreadsheet";
        /// <summary>
        /// GemBox.Spreadsheet assembly full version.
        /// </summary>
        public const string FullVersion = "29.3.0.1000";
        internal static int LicenseReleaseID = (2 + int.Parse("29", System.Globalization.CultureInfo.InvariantCulture));
        private static GXLicensing licensing = new GXLicensing();
        private const string Name = "GemBox.Spreadsheet Professional 2.9";
        private const string RevisionStr = "1000";
        /// <summary>
        /// GemBox.Spreadsheet assembly title.
        /// </summary>
        public const string Title = "GemBox.Spreadsheet Professional 2.9 for .NET 2.0";
        private const string TypeStr = "3";
        private const string VersionLong = "2.9";
        private const string VersionShort = "29";

        private SpreadsheetInfo()
        {
        }

        private static string GetCleanKey(string enteredKey)
        {
            if (enteredKey.Length != 0x13)
            {
                return null;
            }
            string[] strArray = enteredKey.Split(new char[] { '-' });
            if (strArray.Length != 4)
            {
                return null;
            }
            foreach (string str in strArray)
            {
                if (str.Length != 4)
                {
                    return null;
                }
            }
            return (strArray[0] + strArray[1] + strArray[2] + strArray[3]);
        }

        /// <summary>
        /// Call this method from your application to set GemBox.Spreadsheet Professional 
        /// serial key.  <b>(Professional version only)</b>
        /// </summary>
        /// <remarks>
        /// <p><b>This method is present only in Professional version.</b></p>
        /// <p>You should call this method before using any other class from 
        /// GemBox.Spreadsheet Professional library. Key can only be set once (if you try second 
        /// key, exception will be thrown). The best place to call
        /// this method is from static constructor of your application's main class.</p>
        /// <p>Valid serial key has four groups of four alphanumeric characters, 
        /// separated with dashes (for example: "ABC1-A2BC-3ABC-AB4C").</p>
        /// </remarks>
        /// <param name="serialKey">Serial key.</param>
		  //public static void SetLicense(string serialKey)
		  //{
		  //    licensing.SetLicense(serialKey);
		  //}

        internal static void ValidateLicense(ExcelFile caller)
        {
				//if (!licensing.ValidateLicense(LicenseReleaseID, ref caller.HashFactorA, ref caller.HashFactorB))
				//{
				//    throw new Exception("License not set. Call SpreadsheetInfo.SetLicense() method before using any other class from GemBox.Spreadsheet Professional library.");
				//}
        }
    }
}

