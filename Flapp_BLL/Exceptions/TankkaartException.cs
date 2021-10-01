using System;
using System.Runtime.Serialization;

namespace Flapp_BLL.Exceptions
{
    [Serializable]
    internal class TankkaartException : Exception
    {
        public TankkaartException()
        {
        }

        public TankkaartException(string message) : base(message)
        {
        }

        public TankkaartException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TankkaartException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}