namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Text;

    internal class EmbeddedTemplateBuilder : ManualPackageBuilder
    {
        private const string xlsxTemplatePath = "GemBox.Spreadsheet.OpenXml.Template";

        public EmbeddedTemplateBuilder()
        {
            base.GetAllParts();
        }

        public override Stream CreatePart(string path, string contentType)
        {
            throw new NotImplementedException();
        }

        private static string DecodeResourcePath(string standardPath)
        {
            standardPath = standardPath.Replace("/.", "/@.");
            char[] chArray = standardPath.ToCharArray();
            int num = 0;
            for (int i = chArray.Length - 1; (i >= 0) && (chArray[i] != '/'); i--)
            {
                if (chArray[i] == '.')
                {
                    num++;
                    if (num == 2)
                    {
                        chArray[i] = '!';
                        break;
                    }
                }
            }
            standardPath = new string(chArray);
            return ("GemBox.Spreadsheet.OpenXml.Template" + standardPath.Replace('/', '.'));
        }

        public override void Dispose()
        {
        }

        private static string EncodeResourcePath(string resourcePath)
        {
            StringBuilder builder = new StringBuilder();
            string[] strArray = resourcePath.Split(new char[] { '.' });
            int index = 4;
            while (index < (strArray.Length - 1))
            {
                builder.Append("/" + strArray[index]);
                index++;
            }
            builder.Append("." + strArray[index]);
            builder.Replace('!', '.');
            builder.Replace("/@.", "/.");
            return builder.ToString();
        }

        public override Stream GetPart(string path)
        {
            string name = DecodeResourcePath(path);
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(name);
        }

        protected override void GetPartPaths()
        {
            foreach (string str in Assembly.GetExecutingAssembly().GetManifestResourceNames())
            {
                if (str.StartsWith("GemBox.Spreadsheet.OpenXml.Template"))
                {
                    string key = EncodeResourcePath(str);
                    if (key != "/[Content_Types].xml")
                    {
                        base.Parts.Add(key, null);
                    }
                }
            }
        }
    }
}

