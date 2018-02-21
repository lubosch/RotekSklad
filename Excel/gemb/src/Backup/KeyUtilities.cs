namespace GemBox
{
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;

    internal class KeyUtilities
    {
        private KeyUtilities()
        {
        }

        private static int CharToDigit36(char ch)
        {
            if ((!char.IsDigit(ch) && (ch < 'A')) && (ch > 'Z'))
            {
                throw new ArgumentException("Wrong char value.", "ch");
            }
            if (char.IsDigit(ch))
            {
                return (ch - '0');
            }
            return ((ch - 'A') + 10);
        }

        private static char Digit36ToChar(int digit)
        {
            if (digit > 9)
            {
                return (char) ((0x41 + digit) - 10);
            }
            return (char) (0x30 + digit);
        }

        public static char GetHash(string str)
        {
            int num = 0;
            for (int i = 0; i < str.Length; i++)
            {
                int num3 = CharToDigit36(str[i]);
                if ((i % 2) == 1)
                {
                    int num4 = num3 * 2;
                    num += (num4 / 0x24) + (num4 % 0x24);
                }
                else
                {
                    num += num3;
                }
            }
            return Digit36ToChar(num % 0x24);
        }

        public static void GetSerialLongKeyInfo(string serialLongKey, out LicenseOptions licOptions, out int releaseId, out string maintenanceShortKey, out string lockingShortKey, char keyId)
        {
            GetSerialShortKeyInfo(GetShortKey(serialLongKey, keyId, 0x10), out licOptions, out releaseId, out maintenanceShortKey, out lockingShortKey);
        }

        public static void GetSerialShortKeyInfo(string serialShortKey, out LicenseOptions licOptions, out int releaseId, out string maintenanceShortKey, out string lockingShortKey)
        {
            GXRandom random = new GXRandom(CharToDigit36(serialShortKey[0]) * 0x6d39);
            char[] chArray = new char[serialShortKey.Length - 1];
            for (int i = 0; i < chArray.Length; i++)
            {
                int num3 = CharToDigit36(serialShortKey[i + 1]);
                int num4 = random.Next(0, 0x24);
                chArray[i] = Digit36ToChar(((0x24 + num3) - num4) % 0x24);
            }
            string str = new string(chArray, 0, 1);
            licOptions = (LicenseOptions) StringToNumber(str);
            string str2 = new string(chArray, 1, 2);
            releaseId = StringToNumber(str2);
            maintenanceShortKey = new string(chArray, 3, 3);
            lockingShortKey = new string(chArray, 6, 2);
        }

        public static string GetShortKey(string longKey, char keyId, int requiredLongKeyLength)
        {
            longKey = longKey.ToUpper(CultureInfo.InvariantCulture);
            if (longKey.Length != requiredLongKeyLength)
            {
                throw new Exception(string.Concat(new object[] { "Long key must have ", requiredLongKeyLength, " characters and not ", longKey.Length, " (", longKey, ")." }));
            }
            if (longKey[0] != keyId)
            {
                throw new ArgumentException("Long key must start with '" + keyId + "' character.");
            }
            if (GetHash(longKey.Substring(0, longKey.Length - 1)) != longKey[longKey.Length - 1])
            {
                throw new Exception("Long key has wrong checksum.");
            }
            return longKey.Substring(1, longKey.Length - 2);
        }

        private static int StringToNumber(string str)
        {
            int num = 0;
            for (int i = 0; i < str.Length; i++)
            {
                num *= 0x24;
                num += CharToDigit36(str[i]);
            }
            return num;
        }

        [Flags]
        public enum LicenseOptions
        {
            FullLicense = 8,
            None = 0,
            RuntimeLicense = 4,
            UseLockingCode = 2,
            UseMaintenanceNumber = 1
        }
    }
}

