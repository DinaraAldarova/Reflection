using System;
using System.Runtime.Serialization;

namespace Task1Tados.Exceptions
{
    [Serializable]
    internal class ConstructorNotFoundException : MissingMethodException
    {
        public ConstructorNotFoundException()
        {
        }

        public ConstructorNotFoundException(string message) : base(message)
        {
        }
    }
}