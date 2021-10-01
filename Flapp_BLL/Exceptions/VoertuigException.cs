using System;

namespace Flapp_BLL.Exceptions
{
    internal class VoertuigException : Exception
    {
        public VoertuigException(string message) : base(message)
        {
        }
    }
}