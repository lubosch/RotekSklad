namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;

    internal class WeakHashtable
    {
        private Queue addQueue = new Queue();
        public int AddQueueSize;
        protected HashtableElement defaultElement;
        private Hashtable hashtable = new Hashtable();
        private Queue removeQueue = new Queue();

        public WeakHashtable(int addQueueSize)
        {
            this.AddQueueSize = addQueueSize;
        }

        public void Add(HashtableElement el)
        {
            WeekReferenceWithHash key = new WeekReferenceWithHash(this, el);
            this.hashtable.Add(key, key);
            this.EmptyRemoveQueue();
        }

        internal void AddForRemoval(WeekReferenceWithHash weakRef)
        {
            this.removeQueue.Enqueue(weakRef);
        }

        private void EmptyRemoveQueue()
        {
            while (this.removeQueue.Count > 0)
            {
                this.hashtable.Remove((WeekReferenceWithHash) this.removeQueue.Dequeue());
            }
        }

        public HashtableElement Find(HashtableElement el)
        {
            WeekReferenceWithHash hash = new WeekReferenceWithHash(this, el);
            hash = (WeekReferenceWithHash) this.hashtable[hash];
            this.EmptyRemoveQueue();
            if (hash == null)
            {
                return null;
            }
            try
            {
                return (HashtableElement) hash.Target;
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        public Queue AddQueue
        {
            get
            {
                return this.addQueue;
            }
        }

        public HashtableElement DefaultElement
        {
            get
            {
                return this.defaultElement;
            }
        }
    }
}

