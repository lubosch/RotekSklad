namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;

    internal class CellStyleCachedCollection : WeakHashtable
    {
        public CellStyleCachedCollection(ExcelFile ef, int addQueueSize) : base(addQueueSize)
        {
            base.defaultElement = new CellStyleData(this, true, ef.DefaultFontName, ef.DefaultFontSize);
        }

        public void EmptyAddQueue()
        {
            Queue addQueue = base.AddQueue;
            while (addQueue.Count > 0)
            {
                ((CellStyle) addQueue.Dequeue()).Consolidate();
            }
        }
    }
}

