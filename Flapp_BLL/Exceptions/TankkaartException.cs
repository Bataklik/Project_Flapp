using System;

namespace Flapp_BLL.Exceptions
{
    public class TankkaartException : Exception
    {
        public TankkaartException(string message) : base(message)
        {
        }
    }
}