namespace GemBox
{
    using System;
    using System.Collections;

    internal sealed class VisitDirectoryEntryArgs
    {
        private bool continueVisiting;
        private Ole2DirectoryEntry currentEntry;
        private int level;
        private ArrayList levelTags;
        private Ole2DirectoryEntry parentEntry;
        private bool visitChildren;
        private bool visitSiblings;

        internal VisitDirectoryEntryArgs(Ole2DirectoryEntry currentEntry, Ole2DirectoryEntry parentEntry, int level, bool visitSiblings, bool visitChildren, bool continueVisiting, ArrayList levelTags)
        {
            this.currentEntry = currentEntry;
            this.parentEntry = parentEntry;
            this.level = level;
            this.visitSiblings = visitSiblings;
            this.visitChildren = visitChildren;
            this.continueVisiting = continueVisiting;
            this.levelTags = levelTags;
        }

        public bool ContinueVisiting
        {
            get
            {
                return this.continueVisiting;
            }
            set
            {
                this.continueVisiting = value;
            }
        }

        public Ole2DirectoryEntry CurrentEntry
        {
            get
            {
                return this.currentEntry;
            }
        }

        public int Level
        {
            get
            {
                return this.level;
            }
        }

        public ArrayList LevelTags
        {
            get
            {
                return this.levelTags;
            }
        }

        public Ole2DirectoryEntry ParentEntry
        {
            get
            {
                return this.parentEntry;
            }
        }

        public bool VisitChildren
        {
            get
            {
                return this.visitChildren;
            }
            set
            {
                this.visitChildren = value;
            }
        }

        public bool VisitSiblings
        {
            get
            {
                return this.visitSiblings;
            }
            set
            {
                this.visitSiblings = value;
            }
        }
    }
}

