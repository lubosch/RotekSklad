namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Xml;

    internal class Utilities
    {
        private Utilities()
        {
        }

        private static bool AttrStrToBool01(string attrStr)
        {
            if (attrStr == "1")
            {
                return true;
            }
            if (attrStr != "0")
            {
                throw new SpreadsheetException("Error: attribute is not in boolean (0/1) format.");
            }
            return false;
        }

        private static float AttrStrToFloat(string attrStr)
        {
            float num;
            try
            {
                num = float.Parse(attrStr, CultureInfo.InvariantCulture);
            }
            catch (OverflowException exception)
            {
                throw new SpreadsheetException("Attribute is larger than float.", exception);
            }
            catch (FormatException exception2)
            {
                throw new SpreadsheetException("Attribute is not in float format.", exception2);
            }
            return num;
        }

        private static int AttrStrToInt32(string attrStr)
        {
            int num;
            try
            {
                num = int.Parse(attrStr);
            }
            catch (OverflowException exception)
            {
                throw new SpreadsheetException("Attribute is larger than Int32.", exception);
            }
            catch (FormatException exception2)
            {
                throw new SpreadsheetException("Attribute is not in Int32 format.", exception2);
            }
            return num;
        }

        public static ushort BoolToUshort(bool boolValue)
        {
            return (boolValue ? ((ushort) 1) : ((ushort) 0));
        }

        public static string ByteArr2HexStr(byte[] byteArr)
        {
            int capacity = Math.Max((byteArr.Length * 3) - 1, 1);
            StringBuilder builder = new StringBuilder(capacity, capacity);
            for (int i = 0; i < byteArr.Length; i++)
            {
                if (i > 0)
                {
                    builder.Append(' ');
                }
                builder.AppendFormat("{0:X2}", byteArr[i]);
            }
            return builder.ToString();
        }

        public static bool Contains(Array arr, object val)
        {
            return (Array.IndexOf(arr, val) != -1);
        }

        public static object[] ConvertBytesToObjectArray(byte[] bytes)
        {
            object[] destinationArray = new object[bytes.Length];
            Array.Copy(bytes, 0, destinationArray, 0, bytes.Length);
            return destinationArray;
        }

        public static byte[] ConvertObjectArrayToBytes(object[] objs)
        {
            byte[] buffer = new byte[objs.Length];
            for (int i = 0; i < objs.Length; i++)
            {
                buffer[i] = (byte) objs[i];
            }
            return buffer;
        }

        public static byte[] ConvertXlsRecordToBytes(XLSRecord record)
        {
            MemoryStream output = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(output);
            record.Write(bw);
            byte[] buffer = output.ToArray();
            output.Close();
            return buffer;
        }

        public static void CopyStream(Stream source, Stream destination)
        {
            int num2;
            int count = 0x1000;
            byte[] buffer = new byte[count];
            while ((num2 = source.Read(buffer, 0, count)) > 0)
            {
                destination.Write(buffer, 0, num2);
            }
            destination.Flush();
        }

        public static void GetAllFilesRecursive(ArrayList list, string path)
        {
            foreach (string str in Directory.GetFiles(path))
            {
                list.Add(str);
            }
            foreach (string str2 in Directory.GetDirectories(path))
            {
                GetAllFilesRecursive(list, str2);
            }
        }

        public static int GetByteArrLengthFromHexStr(string hexStr)
        {
            return ((hexStr.Length / 3) + 1);
        }

        public static string GetExtensionNoDot(string path)
        {
            return Path.GetExtension(path).Remove(0, 1);
        }

        public static bool GetOptAttrBool01(XmlReader reader, string attributeName, bool defaultValue)
        {
            string attribute = reader.GetAttribute(attributeName);
            if (attribute != null)
            {
                return AttrStrToBool01(attribute);
            }
            return defaultValue;
        }

        public static float GetOptAttrFloat(XmlReader reader, string attributeName, float defaultValue)
        {
            string attribute = reader.GetAttribute(attributeName);
            if (attribute != null)
            {
                return AttrStrToFloat(attribute);
            }
            return defaultValue;
        }

        public static int GetOptAttrInt32(XmlReader reader, string attributeName, int defaultValue)
        {
            string attribute = reader.GetAttribute(attributeName);
            if (attribute != null)
            {
                return AttrStrToInt32(attribute);
            }
            return defaultValue;
        }

        public static bool GetReqAttrBool01(XmlReader reader, string attributeName)
        {
            return AttrStrToBool01(GetReqAttrString(reader, attributeName));
        }

        public static float GetReqAttrFloat(XmlReader reader, string attributeName)
        {
            return AttrStrToFloat(GetReqAttrString(reader, attributeName));
        }

        public static int GetReqAttrInt32(XmlReader reader, string attributeName)
        {
            return AttrStrToInt32(GetReqAttrString(reader, attributeName));
        }

        public static string GetReqAttrString(XmlReader reader, string attributeName)
        {
            string attribute = reader.GetAttribute(attributeName);
            if (attribute == null)
            {
                throw new SpreadsheetException("Required attribute \"" + attributeName + "\" is missing.");
            }
            return attribute;
        }

        public static byte[] HexStr2ByteArr(string hexStr)
        {
            int byteArrLengthFromHexStr = GetByteArrLengthFromHexStr(hexStr);
            byte[] buffer = new byte[byteArrLengthFromHexStr];
            for (int i = 0; i < byteArrLengthFromHexStr; i++)
            {
                buffer[i] = byte.Parse(hexStr.Substring(i * 3, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }
            return buffer;
        }

        public static bool IsBitSet(ushort source, ushort mask)
        {
            return ((source & mask) != 0);
        }

        public static bool IsBitSet(uint source, uint mask)
        {
            return ((source & mask) != 0);
        }

        public static string ReadString(bool isUnicode, byte[] rpnBytes, int startIndex, int length)
        {
            if (isUnicode)
            {
                return Encoding.Unicode.GetString(rpnBytes, startIndex, length * 2);
            }
            return Encoding.ASCII.GetString(rpnBytes, startIndex, length);
        }

        public static bool ReadToFollowing(XmlReader reader, string name)
        {
            while (reader.Read())
            {
                if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == name))
                {
                    return true;
                }
            }
            return false;
        }

        public static int RotateLeft(int val, byte count)
        {
            return RotateLeft((uint) val, count).GetHashCode();
        }

        public static uint RotateLeft(uint val, byte count)
        {
            return ((val << count) | (val >> (0x20 - count)));
        }

        public static byte SetBit(byte sourceByte, byte mask, bool value)
        {
            sourceByte = (byte) (sourceByte & ~mask);
            if (value)
            {
                sourceByte = (byte) (sourceByte + mask);
            }
            return sourceByte;
        }

        public static ushort SetBit(ushort source, ushort mask, bool val)
        {
            if (val)
            {
                return (ushort) (source | mask);
            }
            return (ushort) (source & ~mask);
        }

        public static ushort SetBit(ushort source, ushort mask, ushort value)
        {
            source = (ushort) (source & ~mask);
            source = (ushort) (source + ((ushort) (value & mask)));
            return source;
        }

        public static uint SetBit(uint source, uint mask, uint value)
        {
            source &= ~mask;
            source += value & mask;
            return source;
        }

        public static object TryCastingToInt(double num)
        {
            double num2 = num - Math.Floor(num);
            if (((num2 <= 0.0) && (num <= 2147483647.0)) && (num >= -2147483648.0))
            {
                return (int) num;
            }
            return num;
        }
    }
}

