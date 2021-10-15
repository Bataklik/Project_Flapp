using System;

namespace Flapp_BLL.Exceptions
{
    public class VoertuigException : Exception
    {
        public VoertuigException(string message) : base(message)
        {
        }
    }
}