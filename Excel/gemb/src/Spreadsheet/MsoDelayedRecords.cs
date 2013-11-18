namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;

    internal class MsoDelayedRecords
    {
        private ArrayList delayedRecords = new ArrayList();
        private ArrayList lengths = new ArrayList();
        private int offset;

        public ArrayList DelayedRecords
        {
            get
            {
                return this.delayedRecords;
            }
            set
            {
                this.delayedRecords = value;
            }
        }

        public ArrayList Lengths
        {
            get
            {
                return this.lengths;
            }
            set
            {
                this.lengths = value;
            }
        }

        public int Offset
        {
            get
            {
                return this.offset;
            }
            set
            {
                this.offset = value;
            }
        }
    }
}

