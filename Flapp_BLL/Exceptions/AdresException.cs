using System;
using System.Runtime.Serialization;

namespace Flapp_BLL.Models
{
    internal class AdresException : Exception
    {
        public AdresException(string message) : base(message)
        {
        }
    }
}