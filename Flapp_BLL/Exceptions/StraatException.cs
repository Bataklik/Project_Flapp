using System;

namespace Flapp_BLL.Exceptions
{
    internal class StraatException : Exception
    {
        public StraatException(string message) : base(message)
        {
        }
    }
}