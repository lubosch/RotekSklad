namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;
    using System.Xml;

    internal abstract class ManualPackageBuilder : PackageBuilderBase
    {
        protected ManualPackageBuilder()
        {
        }

        protected void GetAllParts()
        {
            this.GetPartPaths();
            this.ParseContentTypesXml();
        }

        protected abstract void GetPartPaths();
        private void ParseContentTypesXml()
        {
            string extensionNoDot;
            Hashtable hashtable = new Hashtable();
            XmlTextReader reader = new XmlTextReader(this.GetPart("/[Content_Types].xml"));
            try
            {
                reader.MoveToContent();
                while (reader.Read())
                {
                    string reqAttrString;
                    string str4;
                    if ((reader.NodeType == XmlNodeType.Element) && ((str4 = reader.Name) != null))
                    {
                        if (!(str4 == "Override"))
                        {
                            if (str4 == "Default")
                            {
                                goto Label_0097;
                            }
                        }
                        else
                        {
                            string key = Utilities.GetReqAttrString(reader, "PartName");
                            reqAttrString = Utilities.GetReqAttrString(reader, "ContentType");
                            if (base.Parts.Contains(key))
                            {
                                base.Parts[key] = reqAttrString;
                            }
                        }
                    }
                    continue;
                Label_0097:
                    extensionNoDot = Utilities.GetReqAttrString(reader, "Extension");
                    reqAttrString = Utilities.GetReqAttrString(reader, "ContentType");
                    hashtable[extensionNoDot] = reqAttrString;
                }
            }
            finally
            {
                reader.Close();
            }
            foreach (DictionaryEntry entry in (Hashtable) base.Parts.Clone())
            {
                if (entry.Value == null)
                {
                    extensionNoDot = Utilities.GetExtensionNoDot((string) entry.Key);
                    if (!hashtable.Contains(extensionNoDot))
                    {
                        throw new SpreadsheetException("XLSX part \"" + entry.Key + "\" is of undefined content type.");
                    }
                    base.Parts[entry.Key] = (string) hashtable[extensionNoDot];
                }
            }
        }
    }
}

