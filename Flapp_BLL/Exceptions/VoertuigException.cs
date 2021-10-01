using System;
using System.Runtime.Serialization;

namespace Flapp_BLL.Exceptions
{
    internal class VoertuigException : Exception
    {
        public VoertuigException(string message) : base(message)
        {
        }
    }
}