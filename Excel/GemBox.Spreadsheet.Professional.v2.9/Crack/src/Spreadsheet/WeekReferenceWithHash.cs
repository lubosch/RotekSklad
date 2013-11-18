namespace GemBox.Spreadsheet
{
    using System;

    internal class WeekReferenceWithHash : WeakReference
    {
        private int hash;
        private WeakHashtable parent;

        public WeekReferenceWithHash(WeakHashtable parent, HashtableElement target) : base(target)
        {
            this.parent = parent;
            this.hash = target.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            object target;
            object obj3;
            WeekReferenceWithHash objB = (WeekReferenceWithHash) obj;
            if (object.ReferenceEquals(this, objB))
            {
                return true;
            }
            try
            {
                target = this.Target;
            }
            catch (InvalidOperationException)
            {
                target = null;
            }
            if (target == null)
            {
                this.parent.AddForRemoval(this);
                return false;
            }
            try
            {
                obj3 = objB.Target;
            }
            catch (InvalidOperationException)
            {
                obj3 = null;
            }
            if (obj3 == null)
            {
                this.parent.AddForRemoval(objB);
                return false;
            }
            return target.Equals(obj3);
        }

        public override int GetHashCode()
        {
            return this.hash;
        }
    }
}

