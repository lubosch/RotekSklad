namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;
    using System.IO;

    internal abstract class PackageBuilderBase : IDisposable
    {
        public Hashtable Parts = new Hashtable();

        protected PackageBuilderBase()
        {
        }

        public abstract Stream CreatePart(string path, string contentType);
        public abstract void Dispose();
        public abstract Stream GetPart(string path);
    }
}

