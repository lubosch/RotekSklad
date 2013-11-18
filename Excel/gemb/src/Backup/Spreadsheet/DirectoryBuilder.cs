namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Text;
    using System.Xml;

    internal class DirectoryBuilder : ManualPackageBuilder
    {
        private string baseDirectoryPath;
        private bool createContentTypesXml;

        public DirectoryBuilder(string path, FileAccess access)
        {
            if (!Directory.Exists(path))
            {
                throw new SpreadsheetException("Directory \"" + path + "\" does not exist.");
            }
            path = path.Replace('\\', '/');
            if (path[path.Length - 1] == '/')
            {
                this.baseDirectoryPath = path.Substring(0, path.Length - 1);
            }
            else
            {
                this.baseDirectoryPath = path;
            }
            if (access == FileAccess.Read)
            {
                base.GetAllParts();
            }
            else
            {
                if (access != FileAccess.Write)
                {
                    throw new NotImplementedException();
                }
                this.createContentTypesXml = true;
            }
        }

        public override Stream CreatePart(string path, string contentType)
        {
            string absolutePath = this.GetAbsolutePath(path);
            string directoryName = Path.GetDirectoryName(absolutePath);
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
            base.Parts[path] = contentType;
            return new FileStream(absolutePath, FileMode.Create);
        }

        public override void Dispose()
        {
            if (this.createContentTypesXml)
            {
                Stream w = new FileStream(this.GetAbsolutePath("/[Content_Types].xml"), FileMode.Create);
                Hashtable hashtable = new Hashtable();
                hashtable.Add("rels", "application/vnd.openxmlformats-package.relationships+xml");
                hashtable.Add("xml", "application/xml");
                XmlTextWriter writer = new XmlTextWriter(w, new UTF8Encoding(false));
                try
                {
                    writer.WriteStartDocument(true);
                    writer.WriteStartElement("Types");
                    writer.WriteAttributeString("xmlns", "http://schemas.openxmlformats.org/package/2006/content-types");
                    foreach (DictionaryEntry entry in hashtable)
                    {
                        writer.WriteStartElement("Default");
                        writer.WriteAttributeString("Extension", (string) entry.Key);
                        writer.WriteAttributeString("ContentType", (string) entry.Value);
                        writer.WriteEndElement();
                    }
                    foreach (DictionaryEntry entry2 in base.Parts)
                    {
                        string key = (string) entry2.Key;
                        string str3 = (string) entry2.Value;
                        string extensionNoDot = Utilities.GetExtensionNoDot(key);
                        if (((string) hashtable[extensionNoDot]) != str3)
                        {
                            writer.WriteStartElement("Override");
                            writer.WriteAttributeString("PartName", key);
                            writer.WriteAttributeString("ContentType", str3);
                            writer.WriteEndElement();
                        }
                    }
                    writer.WriteEndElement();
                }
                finally
                {
                    writer.Close();
                }
            }
        }

        private string GetAbsolutePath(string relativePath)
        {
            return (this.baseDirectoryPath + relativePath);
        }

        public override Stream GetPart(string path)
        {
            string absolutePath = this.GetAbsolutePath(path);
            if (!File.Exists(absolutePath))
            {
                throw new SpreadsheetException("Input file \"" + absolutePath + "\" does not exist.");
            }
            return new FileStream(absolutePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        protected override void GetPartPaths()
        {
            ArrayList list = new ArrayList();
            Utilities.GetAllFilesRecursive(list, this.baseDirectoryPath);
            foreach (string str in list)
            {
                string key = str.Substring(this.baseDirectoryPath.Length).Replace('\\', '/');
                if (key != "/[Content_Types].xml")
                {
                    base.Parts.Add(key, null);
                }
            }
        }
    }
}

