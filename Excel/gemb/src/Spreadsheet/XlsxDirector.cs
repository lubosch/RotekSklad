namespace GemBox.Spreadsheet
{
    using System;

    internal abstract class XlsxDirector
    {
        protected PackageBuilderBase builder;
        protected const string contentTypePrefix = "application/vnd.openxmlformats-";
        internal const string contentTypesXmlPath = "/[Content_Types].xml";
        protected ExcelFile excelFile;
        internal const string relationshipContentType = "application/vnd.openxmlformats-package.relationships+xml";
        protected const string relationshipsSchema = "http://schemas.openxmlformats.org/officeDocument/2006/relationships";
        protected const string schemasPrefix = "http://schemas.openxmlformats.org";
        protected const string sharedStringsXmlPath = "/xl/sharedStrings.xml";
        protected const string sheetRelationshipSchema = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet";
        protected const string stringsContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sharedStrings+xml";
        protected const string stylesXmlContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.styles+xml";
        protected const string stylesXmlPath = "/xl/styles.xml";
        protected const string workbookContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml";
        protected const string workbookXmlPath = "/xl/workbook.xml";
        protected const string workbookXmlRelsPath = "/xl/_rels/workbook.xml.rels";
        protected const string worksheetContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml";
        internal const string xmlContentType = "application/xml";
        internal const string xmlnsContentTypesSchema = "http://schemas.openxmlformats.org/package/2006/content-types";
        protected const string xmlnsMainSchema = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";

        public XlsxDirector(PackageBuilderBase builder, ExcelFile excelFile)
        {
            this.builder = builder;
            this.excelFile = excelFile;
        }

        public abstract void Construct();
    }
}

