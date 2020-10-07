using System;
using System.Runtime.Serialization;

namespace Task1Tados.Exceptions
{
    [Serializable]
    internal class TypeIsNotClassException : Exception
    {
        public TypeIsNotClassException()
        {
        }

        public TypeIsNotClassException(string message) : base(message)
        {
        }
    }
}