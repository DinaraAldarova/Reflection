using System;
using System.IO;
using System.Runtime.Serialization;

namespace Task1Tados.Exceptions
{
    [Serializable]
    internal class AssemblyNotFoundException : FileNotFoundException
    {
        public AssemblyNotFoundException()
        {
        }

        public AssemblyNotFoundException(string message) : base(message)
        {
        }
    }
}