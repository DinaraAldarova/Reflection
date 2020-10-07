using System;
using System.Runtime.Serialization;

namespace Task1Tados.Exceptions
{
    [Serializable]
    internal class AssemblyHasInvalidFormatException : BadImageFormatException
    {
        public AssemblyHasInvalidFormatException()
        {
        }

        public AssemblyHasInvalidFormatException(string message) : base(message)
        {
        }
    }
}