using System;

namespace Flapp_BLL.Exceptions.ModelExpections
{
    public class TankkaartException : Exception
    {
        public TankkaartException(string message) : base(message)
        {
        }
    }
}