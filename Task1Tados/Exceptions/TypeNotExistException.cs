using System;
using System.Runtime.Serialization;

namespace Task1Tados.Exceptions
{
    [Serializable]
    internal class TypeNotExistException : Exception
    {
        public TypeNotExistException()
        {
        }

        public TypeNotExistException(string message) : base(message)
        {
        }
    }
}