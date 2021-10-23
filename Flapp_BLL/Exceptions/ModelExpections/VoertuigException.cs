using System;

namespace Flapp_BLL.Exceptions.ModelExpections
{
    public class VoertuigException : Exception
    {
        public VoertuigException(string message) : base(message)
        {
        }
    }
}