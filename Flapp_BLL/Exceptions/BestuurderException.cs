using System;

namespace Flapp_BLL.Exceptions
{
    public class BestuurderException : Exception
    {
        public BestuurderException(string message) : base(message)
        {
        }
    }
}