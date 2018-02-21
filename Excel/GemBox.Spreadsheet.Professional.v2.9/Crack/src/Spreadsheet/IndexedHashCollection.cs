namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;

    internal class IndexedHashCollection : ArrayList
    {
        private Hashtable hashtable = new Hashtable();

        public override int Add(object item)
        {
            object obj2 = this.hashtable[item];
            if (obj2 != null)
            {
                return (int) obj2;
            }
            return this.AddInternal(item);
        }

        protected int AddArrayOnly(object item)
        {
            return base.Add(item);
        }

        public int AddInternal(object item)
        {
            int num = this.AddArrayOnly(item);
            this.hashtable[item] = num;
            return num;
        }
    }
}

