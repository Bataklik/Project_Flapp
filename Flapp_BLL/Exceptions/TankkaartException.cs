using System;

namespace Flapp_BLL.Exceptions
{
    internal class TankkaartException : Exception
    {
        public TankkaartException(string message) : base(message)
        {
        }
    }
}