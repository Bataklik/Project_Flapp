using System;

namespace Flapp_BLL.Exceptions.ManagerExceptions
{
    class AdresManagerException : Exception
    {
        public AdresManagerException(string message) : base(message)
        {
        }

        public AdresManagerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
