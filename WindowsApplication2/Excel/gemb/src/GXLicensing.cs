namespace GemBox
{
    using System;

    internal class GXLicensing
    {
        private string runtimeKey;
        private bool runtimeKeySet;

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

        private static bool IsValidRuntimeKey(KeyUtilities.LicenseOptions licOpt, int serialRelId, int productRelID)
        {
            if ((licOpt & KeyUtilities.LicenseOptions.FullLicense) == KeyUtilities.LicenseOptions.None)
            {
                return false;
            }
            if ((licOpt & KeyUtilities.LicenseOptions.RuntimeLicense) == KeyUtilities.LicenseOptions.None)
            {
                return false;
            }
            if ((licOpt & KeyUtilities.LicenseOptions.UseMaintenanceNumber) != KeyUtilities.LicenseOptions.None)
            {
                return false;
            }
            if ((licOpt & KeyUtilities.LicenseOptions.UseLockingCode) != KeyUtilities.LicenseOptions.None)
            {
                return false;
            }
            if (serialRelId != productRelID)
            {
                return false;
            }
            return true;
        }

        public void SetLicense(string serialKey)
        {
            string cleanKey = GetCleanKey(serialKey.ToUpper().Trim());
            if (this.runtimeKeySet)
            {
                if (this.runtimeKey != cleanKey)
                {
                    throw new Exception("The serial key can be set only once.");
                }
            }
            else
            {
                this.runtimeKey = cleanKey;
                this.runtimeKeySet = true;
                if (this.runtimeKey == null)
                {
                    throw new Exception("The serial key \"" + serialKey + "\" is not valid. Valid serial key has four groups of four alphanumeric characters, separated with dashes (for example: \"ABC1-A2BC-3ABC-AB4C\"). ");
                }
            }
        }

        public bool ValidateLicense(int productRelID, ref int hashA, ref int hashB)
        {
				//KeyUtilities.LicenseOptions options;
				//int num;
				//string str;
				//string str2;
				//if (this.runtimeKey == null)
				//{
				//    return false;
				//}
				//KeyUtilities.GetSerialLongKeyInfo(this.runtimeKey, out options, out num, out str, out str2, 'E');
				//options |= KeyUtilities.LicenseOptions.RuntimeLicense;
				//if (!IsValidRuntimeKey(options, num, productRelID))
				//{
				//    throw new Exception("The serial key is not valid.");
				//}
				//Random random = new Random();
				//hashA = random.Next(0x6f, 0x3117);
				//hashB = hashA + (0x17 * random.Next(3, 0x704));
            return true;
        }
    }
}

