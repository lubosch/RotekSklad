namespace GemBox.Spreadsheet
{
    using System;

    /// <summary>
    /// HashtableElement. All derived classes MUST implement:
    /// 1) HashtableElement Clone()
    /// 2) int GetHashCode()
    /// 3) bool Equals(object obj)
    /// </summary>
    internal class HashtableElement
    {
        private bool isInCache;
        private WeakHashtable parentCollection;

        public HashtableElement(WeakHashtable parentCollection, bool isInCache)
        {
            this.parentCollection = parentCollection;
            this.isInCache = isInCache;
        }

        public virtual HashtableElement Clone(WeakHashtable parentCollection)
        {
            throw new Exception("Internal: Must override Clone() in derived class");
        }

        public override bool Equals(object obj)
        {
            throw new Exception("Internal: Must override Equals(object) in derived class");
        }

        public HashtableElement FindExistingOrAddToCache()
        {
            HashtableElement element = this.parentCollection.Find(this);
            if (element != null)
            {
                return element;
            }
            this.parentCollection.Add(this);
            this.isInCache = true;
            return this;
        }

        public override int GetHashCode()
        {
            throw new Exception("Internal: Must override GetHashCode() in derived class");
        }

        public bool IsInCache
        {
            get
            {
                return this.isInCache;
            }
        }

        public WeakHashtable ParentCollection
        {
            get
            {
                return this.parentCollection;
            }
        }
    }
}

