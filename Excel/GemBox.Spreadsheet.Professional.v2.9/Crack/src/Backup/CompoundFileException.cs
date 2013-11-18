namespace GemBox
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    internal class CompoundFileException : Exception
    {
        public CompoundFileException()
        {
        }

        public CompoundFileException(string msg) : base(msg)
        {
        }

        protected CompoundFileException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public CompoundFileException(string msg, Exception inner) : base(msg, inner)
        {
        }
    }
}

