﻿namespace CQRS.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    internal class NoResultException : Exception
    {
        public NoResultException()
        {
        }

        public NoResultException(string message) : base(message)
        {
        }

        public NoResultException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoResultException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
