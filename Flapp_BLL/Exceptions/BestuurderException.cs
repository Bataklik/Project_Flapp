using System;

namespace Flapp_BLL.Exceptions
{
    internal class BestuurderException : Exception
    {
        public BestuurderException(string message) : base(message)
        {
        }
    }
}